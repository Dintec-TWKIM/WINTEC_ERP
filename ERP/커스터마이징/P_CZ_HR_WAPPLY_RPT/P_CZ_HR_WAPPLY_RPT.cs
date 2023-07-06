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
	public partial class P_CZ_HR_WAPPLY_RPT : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		public string CD_BIZAREA { get; set; }

		public string NO_EMP { get; set; }

		public string NM_EMP { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_HR_WAPPLY_RPT()
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

			dtp신청From.Text= Util.GetToday(-30);
			dtp신청To.Text = Util.GetToday();

			// 상태
			DataTable dt = Util.GetDB_CODE("HR_G000019", true);
		
			for (int i = dt.Rows.Count - 1; i >= 0; i--)
			{
				if (dt.Rows[i]["CODE"].ToString() == "003") dt.Rows.RemoveAt(i);	// 반려 항목은 삭제 (사용안함)
			}

			cbo상태.ValueMember = "CODE";
			cbo상태.DisplayMember = "NAME";
			cbo상태.DataSource = dt;
		}

		private void InitGrid()
		{
			flexL.BeginSetting(1, 1, false);

			flexL.SetCol("CD_COMPANY"	, "회사코드"		, false);
			flexL.SetCol("CD_BIZAREA"	, "사업장코드"	, false);
			flexL.SetCol("NO_PROPOSAL"	, "신청번호"		, false);
			flexL.SetCol("CHK"			, "S"			, 30	, true, CheckTypeEnum.Y_N);
			flexL.SetCol("CD_CONSENT"	, "상태"			, 90	, false);			
			flexL.SetCol("DT_PROPOSAL"	, "신청일자"		, 110	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("NO_EMP"		, "사번"			, 80);
			flexL.SetCol("NM_EMP"		, "성명"			, 100);
			flexL.SetCol("NM_DEPT"		, "부서"			, 110);
			flexL.SetCol("NM_WCODE"		, "구분"			, 90);
			flexL.SetCol("CD_WCODE"		, "근태코드"		, false);
			flexL.SetCol("CD_WTYPE"		, "근태구분"		, false);
			flexL.SetCol("DT_START"		, "시작일자"		, 110	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("DT_CLOSE"		, "종료일자"		, 110	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("DY_PROPOSAL"	, "신청일수"		, 70);
			flexL.SetCol("DC_RMK"		, "비고"			, 400);
			
			flexL.Cols["DY_PROPOSAL"].Format = "0.#";
			flexL.Cols["CD_CONSENT"].TextAlign = TextAlignEnum.CenterCenter;			
			flexL.Cols["NO_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["NM_DEPT"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["NM_WCODE"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["DY_PROPOSAL"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.SetDataMap("CD_CONSENT", Util.GetDB_CODE("HR_G000019"), "CODE", "NAME");

			flexL.SettingVersion = "15.11.18.05";
			flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			btn승인.Click += new EventHandler(btn승인_Click);
			btn승인취소.Click += new EventHandler(btn승인취소_Click);
		}

		protected override void InitPaint()
		{
			txt포커스.Focus();
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DBMgr dbm = new DBMgr(DBConn.iU);
			dbm.Procedure = "SP_CZ_HR_WAPPLYL_RPT_SELECT";
			dbm.AddParameter("CD_COMPANY"	, CD_COMPANY);
			dbm.AddParameter("DT_FROM"		, dtp신청From.Text);
			dbm.AddParameter("DT_TO"		, dtp신청To.Text);
			dbm.AddParameter("NO_EMP"		, cbx사원.QueryWhereIn_Pipe);
			dbm.AddParameter("CD_DEPT"		, cbx부서.QueryWhereIn_Pipe);
			dbm.AddParameter("CD_CONSENT"	, cbo상태.SelectedValue);
			DataTable dt = dbm.GetDataTable();

			flexL.Binding = dt;
			if (!flexL.HasNormalRow) ShowMessage(공통메세지.조건에해당하는내용이없습니다);
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn승인_Click(object sender, EventArgs e)
		{
			if (flexL.DataTable == null) { ShowMessage(공통메세지.선택된자료가없습니다); return; }

			DataRow[] rows = flexL.DataTable.Select("CHK = 'Y'");
			if (rows.Length == 0) { ShowMessage(공통메세지.선택된자료가없습니다); return; }

			MsgControl.ShowMsg("처리중입니다.");

			try
			{
				foreach (DataRow row in rows)
				{
					DBMgr dbm = new DBMgr(DBConn.iU);
					dbm.Procedure = "UP_HR_WAPPLY_BATCH_ACCEPT";
					dbm.AddParameter("P_CD_COMPANY"	, row["CD_COMPANY"]);
					dbm.AddParameter("P_NO_PROPOSAL", row["NO_PROPOSAL"]);
					dbm.AddParameter("P_CD_BIZAREA"	, row["CD_BIZAREA"]);
					dbm.AddParameter("P_CD_WTYPE"	, row["CD_WTYPE"]);
					dbm.AddParameter("P_ID_UPDATE"	, NO_EMP);
					dbm.AddParameter("P_NO_CEMP"	, NO_EMP);
					dbm.AddParameter("P_NM_CEMP"	, NM_EMP);
					dbm.ExecuteNonQuery();
				}

				MsgControl.CloseMsg();
				ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}
			
			OnToolBarSearchButtonClicked(null, null);
		}

		private void btn승인취소_Click(object sender, EventArgs e)
		{
			if (flexL.DataTable == null) { ShowMessage(공통메세지.선택된자료가없습니다); return; }

			DataRow[] rows = flexL.DataTable.Select("CHK = 'Y'");
			if (rows.Length == 0) { ShowMessage(공통메세지.선택된자료가없습니다); return; }

			MsgControl.ShowMsg("처리중입니다.");

			try
			{
				foreach (DataRow row in rows)
				{
					DBMgr dbm = new DBMgr(DBConn.iU);
					dbm.Procedure = "UP_HR_WAPPLY_CANCEL";
					dbm.AddParameter("P_CD_COMPANY"	, row["CD_COMPANY"]);					
					dbm.AddParameter("P_CD_BIZAREA"	, row["CD_BIZAREA"]);
					dbm.AddParameter("P_NO_EMP"		, row["NO_EMP"]);
					dbm.AddParameter("P_NO_PROPOSAL", row["NO_PROPOSAL"]);
					dbm.AddParameter("P_CD_WCODE"	, row["CD_WCODE"]);
					dbm.AddParameter("P_DT_START"	, row["DT_START"]);
					dbm.AddParameter("P_DT_CLOSE"	, row["DT_CLOSE"]);
					dbm.AddParameter("P_CD_CONSENT"	, "004");
					dbm.AddParameter("P_STATE"		, "Y");
					dbm.AddParameter("P_CD_WTYPE"	, row["CD_WTYPE"]);
					dbm.AddParameter("P_ID_UPDATE"	, NO_EMP);
					dbm.ExecuteNonQuery();
				}

				MsgControl.CloseMsg();
				ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}
			
			OnToolBarSearchButtonClicked(null, null);
		}

		#endregion 
	}
}

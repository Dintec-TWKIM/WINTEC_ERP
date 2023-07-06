using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using DzHelpFormLib;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.Util;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;

namespace cz
{
	public partial class P_CZ_MA_HULL_ITEM_MGT : PageBase
	{
		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_MA_HULL_ITEM_MGT()
		{
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
			// 콤보박스 관련
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			// 조회대상
			// 재고코드 없음
			// 재고코드 다름
			// 재고코드 검색안됨
			dt.Rows.Add("CR_SEARCH", "1", "재고코드 없음");
			dt.Rows.Add("CR_SEARCH", "2", "재고코드 다름");
			dt.Rows.Add("CR_SEARCH", "3", "재고코드 검색안됨");

			cboSearchType.DataSource = dt.Select("TYPE = 'CR_SEARCH'").CopyToDataTable();
			cboSearchType.SelectedIndex = 0;

			// 결합대상
			dt.Rows.Add("CR_JOIN1", ""		 , "부품정보");
			dt.Rows.Add("CR_JOIN1", ""		 , "-------");
			dt.Rows.Add("CR_JOIN1", "DRAWING", "도면번호");
			dt.Rows.Add("CR_JOIN1", "UCODE"	 , "U코드");

			cboJoin1.DataSource = dt.Select("TYPE = 'CR_JOIN1'").CopyToDataTable();
			cboJoin1.SelectedIndex = 2;

			dt.Rows.Add("CR_JOIN2", ""		 , "재고정보");
			dt.Rows.Add("CR_JOIN2", ""		 , "--------");
			dt.Rows.Add("CR_JOIN2", "DRAWING", "도면번호");
			dt.Rows.Add("CR_JOIN2", "UCODE"	 , "U코드");

			cboJoin2.DataSource = dt.Select("TYPE = 'CR_JOIN2'").CopyToDataTable();
			cboJoin2.SelectedIndex = 1;

			// 키워드
			dt.Rows.Add("KEYWORD1", "A.NO_IMO"	 , "IMO번호");
			dt.Rows.Add("KEYWORD1", "C.NO_HULL"	 , "호선번호");
			dt.Rows.Add("KEYWORD1", "C.NM_VESSEL", "선명");
			dt.Rows.Add("KEYWORD1", "B.NM_MODEL" , "모델명");
			dt.Rows.Add("KEYWORD1", "B.SERIAL"	 , "일련번호");

			cboKeyword1.DataSource = dt.Select("TYPE = 'KEYWORD1'").CopyToDataTable();
			cboKeyword1.SelectedIndex = 2;

			dt.Rows.Add("KEYWORD2", "A.NO_PLATE", "부품번호");
			dt.Rows.Add("KEYWORD2", "A.NM_PLATE", "부품명");

			cboKeyword2.DataSource = dt.Select("TYPE = 'KEYWORD2'").CopyToDataTable();
			cboKeyword2.SelectedIndex = 1;

			dt.Rows.Add("KEYWORD3", "A.NO_DRAWING", "도면번호");
			dt.Rows.Add("KEYWORD3", "A.UCODE"	  , "U코드");

			cboKeyword3.DataSource = dt.Select("TYPE = 'KEYWORD3'").CopyToDataTable();
			cboKeyword3.SelectedIndex = 0;

			MainGrids = new FlexGrid[] { flexH };
		}

		private void InitGrid()
		{
			// ========== H
			flexH.BeginSetting(2, 1, false);

			flexH.SetCol("CHK"					, "S"			, 30	, true	, CheckTypeEnum.Y_N);
			flexH.SetCol("NO_IMO"				, "IMO번호"		, 70);
			flexH.SetCol("NO_HULL"				, "호선번호"		, 80);
			flexH.SetCol("NO_ENGINE"			, "엔진번호"		, false);
			flexH.SetCol("NM_MODEL"				, "모델명"		, 100);
			flexH.SetCol("SERIAL"				, "일련번호"		, 100);
			flexH.SetCol("NO_PLATE"				, "부품번호"		, 120);
			flexH.SetCol("NM_PLATE"				, "부품명"		, 200);
			flexH.SetCol("CD_ITEM"				, "재고코드"		, 100);
			flexH.SetCol("NO_DRAWING"			, "도면번호"		, 100);
			flexH.SetCol("UCODE"				, "U코드"		, 100);

			flexH.SetCol("CD_ITEM_M"			, "재고코드"		, 100);
			flexH.SetCol("NM_ITEM_M"			, "재고명"		, 200);
			flexH.SetCol("STND_ITEM_M"			, "부품번호"		, 80);
			flexH.SetCol("NO_STND_M"			, "도면번호"		, 100);
			flexH.SetCol("STND_DETAIL_ITEM_M"	, "U코드"		, 100);

			flexH[0, flexH.Cols["NO_IMO"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["NO_HULL"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["NO_ENGINE"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["NM_MODEL"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["SERIAL"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["NO_PLATE"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["NM_PLATE"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["CD_ITEM"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["NO_DRAWING"].Index] = "기부속 / 기자재 정보";
			flexH[0, flexH.Cols["UCODE"].Index] = "기부속 / 기자재 정보";

			flexH[0, flexH.Cols["CD_ITEM_M"].Index] = "재고 정보";
			flexH[0, flexH.Cols["NM_ITEM_M"].Index] = "재고 정보";
			flexH[0, flexH.Cols["STND_ITEM_M"].Index] = "재고 정보";
			flexH[0, flexH.Cols["NO_STND_M"].Index] = "재고 정보";
			flexH[0, flexH.Cols["STND_DETAIL_ITEM_M"].Index] = "재고 정보";
			
			flexH.SettingVersion = "16.12.09.03";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			flexH.Rows[0].Height = 28;
			flexH.Rows[1].Height = 28;
		}

		private void InitEvent()
		{
			//btnConfirm.Click += new EventHandler(btnConfirm_Click);
		}

		protected override void InitPaint()
		{

		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DBMgr dbm = new DBMgr();
			dbm.Procedure = "PS_CZ_MA_HULL_ITEM_MGT";
			dbm.AddParameter("@CR_SEARCH", cboSearchType.SelectedValue);
			dbm.AddParameter("@CR_JOIN1" , cboJoin1.SelectedValue);
			dbm.AddParameter("@CR_JOIN2" , cboJoin2.SelectedValue);
			dbm.AddParameter("@KEY_COL1" , cboKeyword1.SelectedValue);
			dbm.AddParameter("@KEY_COL2" , cboKeyword2.SelectedValue);
			dbm.AddParameter("@KEY_COL3" , cboKeyword3.SelectedValue);
			dbm.AddParameter("@KEY_TEXT1", txtKeyword1.Text);
			dbm.AddParameter("@KEY_TEXT2", txtKeyword2.Text);
			dbm.AddParameter("@KEY_TEXT3", txtKeyword3.Text);

			DataTable dtH = dbm.GetDataTable();
			flexH.Binding = dtH;

			// 라이브서버에 없는 재고코드가 물려있는 아이템
			// 도면번호 하나에 재고코드가 2개 이상
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if (!BeforeSave()) return;
			if (!MsgAndSave(PageActionMode.Save)) return;

			//isNew = false;
			ShowMessage(PageResultMode.SaveGood);
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !base.Verify()) return false;

			try
			{
				DataRow[] row = flexH.DataTable.Select("CHK = 'Y'");
				if (row.Length == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return false;
				}

				string xml = GetTo.Xml(row.CopyToDataTable());

				DBMgr dbm = new DBMgr();
				dbm.Procedure = "PX_CZ_MA_HULL_ITEM_MGT";
				dbm.AddParameter("@XML", xml);
				dbm.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				return Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}

			return true;
		}

		#endregion
	}
}

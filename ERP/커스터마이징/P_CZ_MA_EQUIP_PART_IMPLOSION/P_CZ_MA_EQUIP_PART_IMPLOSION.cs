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
using System.Linq;
using Duzon.Common.Controls;

namespace cz
{
	public partial class P_CZ_MA_EQUIP_PART_IMPLOSION : PageBase
	{
		#region ==================================================================================================== Constructor

		public P_CZ_MA_EQUIP_PART_IMPLOSION()
		{
			StartUp.Certify(this);
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
			MainGrids = new FlexGrid[] { grd라인 };
			grd헤드.DetailGrids = new FlexGrid[] { grd라인 };
		}

		private void InitGrid()
		{
			// ********** 헤드
			grd헤드.BeginSetting(1, 1, false);
				
			grd헤드.SetCol("CD_ITEM"	, "재고코드"	, 90);
			grd헤드.SetCol("NM_ITEM"	, "부품명"	, 400);
			grd헤드.SetCol("CNT"		, "건수"		, 50	, false, typeof(decimal), FormatTpType.QUANTITY);

			grd헤드.SetDefault("20.01.15.02", SumPositionEnum.None);

			// ********** 라인
			grd라인.BeginSetting(1, 1, false);
				
			grd라인.SetCol("NO_IMO"			, "IMO번호"		, 70);
			grd라인.SetCol("CD_VENDOR"		, "매입처"		, 60);
			grd라인.SetCol("NO_TYPE"			, "TYPE"		, 40	, false, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("NO_ITEM"			, "ITEM"		, 40	, false, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("NO_HULL"			, "호선번호"		, 100);
			grd라인.SetCol("NM_VESSEL"		, "선명"			, 140);
			grd라인.SetCol("CD_PARTNER"		, "매출처코드"	, false);
			grd라인.SetCol("LN_PARTNER"		, "매출처"		, 200);
			grd라인.SetCol("NM_SHIP_YARD"	, "조선소"		, 200);
			grd라인.SetCol("CLS_M"			, "중분류"		, 150);
			grd라인.SetCol("CLS_S"			, "소분류"		, 150);
			grd라인.SetCol("DC_RMK1"			, "코드1"		, 100);
			grd라인.SetCol("DC_RMK2"			, "코드2"		, 100);
			grd라인.SetCol("DC_RMK3"			, "코드3"		, 100);
			grd라인.SetCol("DC_RMK4"			, "코드4"		, 100);
			grd라인.SetCol("DC_RMK5"			, "코드5"		, 100);
			grd라인.SetCol("DC_RMK6"			, "코드6"		, 100);
			grd라인.SetCol("DC_RMK7"			, "코드7"		, 100);
			grd라인.SetCol("DC_RMK8"			, "코드8"		, 100);
			grd라인.SetCol("DC_RMK9"			, "코드9"		, 100);
			grd라인.SetCol("DC_RMK10"		, "코드10"		, 100);
			grd라인.SetCol("DC_RMK11"		, "코드11"		, 100);
			grd라인.SetCol("DC_RMK12"		, "코드12"		, 100);
			grd라인.SetCol("DC_RMK13"		, "코드13"		, 100);
			grd라인.SetCol("DC_RMK14"		, "코드14"		, 100);
			grd라인.SetCol("DC_RMK15"		, "코드15"		, 100);

			grd라인.SetCol("CD_ITEM"			, "재고코드(전)"	, 90);
			grd라인.SetCol("CD_ITEM_NEW"		, "재고코드(후)"	, 90);

			grd라인.SetDataMap("CLS_M", GetDb.Code("MA_B000031"), "CODE", "NAME");
			grd라인.SetDataMap("CLS_S", GetDb.Code("MA_B000032"), "CODE", "NAME");

			grd라인.SetDefault("20.01.29.01", SumPositionEnum.None);
		}		

		protected override void InitPaint()
		{
			tbx포커스.Left = -1000;
			lbl건수.TextAlign = ContentAlignment.MiddleLeft;		// 자동 우측정렬이 되버려서 다시 재설정
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			ctb중분류.QueryAfter += Ctb중분류_QueryAfter;
			ctb중분류.QueryBefore += Ctb중분류_QueryBefore;
			ctb소분류.QueryBefore += Ctb소분류_QueryBefore;			
			ctb재고.QueryBefore += Ctb재고_QueryBefore;
			btn재고.Click += Btn재고_Click;

			tbx키워드.KeyDown += Tbx키워드_KeyDown;
			cur건수.KeyDown += Cur건수_KeyDown;

			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;
		}
		
		private void Ctb중분류_QueryAfter(object sender, BpQueryArgs e)
		{
			ctb소분류.Clear();
		}

		private void Ctb중분류_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
			e.HelpParam.P42_CD_FIELD2 = "EQ";
		}

		private void Ctb소분류_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
			e.HelpParam.P42_CD_FIELD2 = ctb중분류.CodeValue;
		}

		private void Ctb재고_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P42_CD_FIELD2 = "009";
		}

		private void Btn재고_Click(object sender, EventArgs e)
		{
			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				grd라인[i, "CD_ITEM_NEW"] = ctb재고.CodeValue;
		}

		private void Tbx키워드_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				if (((TextBoxExt)sender).Text.Trim() == "")
					ShowMessage("검색어를 입력하세요!");
				else
					OnToolBarSearchButtonClicked(null, null);
			}
		}

		private void Cur건수_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				OnToolBarSearchButtonClicked(null, null);
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSearchButtonClicked(sender, e);
			DebugMode debugMode = ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;			
			tbx포커스.Focus();			
			tbx키워드.Text = EngHanConverter.KorToEng(tbx키워드.Text);
			Util.ShowProgress(DD("조회중입니다."));

			// 중분류 자동 세팅
			if (ctb중분류.CodeValue == "" && tbx키워드.Text.Length >= 4)
			{
				string query = @"
SELECT
	CD_SYSDEF AS CODE
,	NM_SYSDEF AS NAME
FROM V_CZ_MA_CODEDTL
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'MA_B000031'
	AND CD_SYSDEF = '" + tbx키워드.Text.Left(4) + "'";

				DataTable dt = DBMgr.GetDataTable(query);

				if (dt.Rows.Count > 0)
				{
					ctb중분류.CodeValue = (string)dt.Rows[0]["CODE"];
					ctb중분류.CodeName = (string)dt.Rows[0]["Name"];
				}
			}

			ToggleColumn();

			// 조회
			DBMgr dbm = new DBMgr
			{
				DebugMode = debugMode
			,	Procedure = "PS_CZ_MA_EQUIP_PART_IMPLOSION_H"
			};
			dbm.AddParameterRange(GetSearchCond("H").Parameters);
			dbm.AddParameter("@CD_ITEM", tbx키워드.Text);

			grd헤드.Binding = dbm.GetDataTable();
			Util.CloseProgress();
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string filter = "";

			// 일치조건 필터링
			filter += "CD_ITEM = '" + grd헤드.GetValue("CD_ITEM") + "'";

			if (grd헤드.DetailQueryNeed)
			{
				DBParameters dbp = GetSearchCond("L");				
				dbp["@CD_ITEM"] = grd헤드.GetValue("CD_ITEM");
				
				DBMgr dbm = new DBMgr
				{
					DebugMode = DebugMode.None
				,	Procedure = "PS_CZ_MA_EQUIP_PART_IMPLOSION_L"
				};
				dbm.AddParameterRange(dbp.Parameters);
				dt = dbm.GetDataTable();
				dt.Columns.Add("CD_ITEM_NEW", typeof(string));
			}

			grd라인.BindingAdd(dt, filter);
		}
		
		private DBParameters GetSearchCond(string mode)
		{
			DBParameters dbp = new DBParameters();
			dbp.Add("@NO_IMO"		, ctb호선.CodeValue);
			dbp.Add("@CD_PARTNER"	, ctb매출처.CodeValue);
			dbp.Add("@CLS_M"		, ctb중분류.CodeValue);
			dbp.Add("@CLS_S"		, ctb소분류.CodeValue);

			if (mode == "H")
			{
				dbp.Add("@CNT_REPEAT", cur건수.DecimalValue);
			}

			return dbp;
		}

		private void ToggleColumn()
		{
			string query = @"
SELECT
	B.CD_FLAG2	AS CODE
,	B.NM_SYSDEF	AS NAME
FROM MA_CODEDTL		AS A WITH(NOLOCK)
JOIN CZ_MA_CODEDTL	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY
WHERE 1 = 1
	AND A.CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND A.CD_FIELD = 'MA_B000031'
	AND A.CD_SYSDEF = '" + ctb중분류.CodeValue + @"'
	AND B.CD_FIELD = 'CZ_MA00033'
	AND A.CD_FLAG2 = B.CD_FLAG1
";

			DataTable dt = DBMgr.GetDataTable(query);

			// 코드관련 필드 숨김
			for (int i = 1; i <= 15; i++)
			{
				grd라인.Cols["DC_RMK" + i].Caption = "코드" + i;
				grd라인.Cols["DC_RMK" + i].Visible = false;
			}

			// 해당 벤더 필드만 보여줌
			foreach (DataRow row in dt.Rows)
			{
				grd라인.Cols[(string)row["CODE"]].Caption = (string)row["NAME"];
				grd라인.Cols[(string)row["CODE"]].Visible = true;				
			}
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSaveButtonClicked(sender, e);
			DebugMode debugMode = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;
			
			// 그리드 검사
			if (!base.Verify())
				return;

			// ********** 저장
			try
			{
				DataTable dtLine = grd라인.GetChanges();
				string lineXml = GetTo.Xml(dtLine);
				DBMgr.ExecuteNonQuery("PX_CZ_MA_EQUIP_PART_IMPLOSION", debugMode, lineXml);
				

				// 마무리
				grd라인.AcceptChanges();

				ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}

		#endregion
	}
}

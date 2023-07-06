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
using DX;

namespace cz
{
	public partial class P_CZ_MA_ITEM_HULL : PageBase
	{
		#region ==================================================================================================== Constructor

		public P_CZ_MA_ITEM_HULL()
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
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");
		
			dt.Rows.Add("키워드", "NO_PLATE"	, "품목코드");
			dt.Rows.Add("키워드", "CD_ITEM"	, "재고코드");
			dt.Rows.Add("키워드", "UCODE"	, "U코드");
			dt.Rows.Add("키워드", "EZCODE"	, "K코드");
			dt.Rows.Add("키워드", "EZCODE2"	, "K코드2");

			cbo키워드.DataBind(dt.Select("TYPE = '키워드'").CopyToDataTable(), true);

			grd헤드.DetailGrids = new FlexGrid[] { grd라인 };
		}

		private void InitGrid()
		{
			// ********** 헤드
			grd헤드.BeginSetting(1, 1, false);
				
			grd헤드.SetCol("NO_PLATE"	, "품목코드"	, 120);			
			grd헤드.SetCol("CD_ITEM"		, "재고코드"	, 80);
			grd헤드.SetCol("UCODE"		, "U코드"	, 120);
			grd헤드.SetCol("EZCODE"		, "K코드"	, 120);
			grd헤드.SetCol("EZCODE2"		, "K코드2"	, 120);
			grd헤드.SetCol("CNT"			, "건수"		, 50	, false, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("NM_PLATE"	, "부품명"	, 200);			

			grd헤드.SetDefault("20.01.13.06", SumPositionEnum.None);

			// ********** 라인
			grd라인.BeginSetting(1, 1, false);
				
			grd라인.SetCol("NO_IMO"			, "IMO번호"		, 70);
			grd라인.SetCol("NO_HULL"			, "호선번호"		, 80);
			grd라인.SetCol("NM_VESSEL"		, "선명"			, 140);
			grd라인.SetCol("CD_PARTNER"		, "운항선사코드"	, false);
			grd라인.SetCol("LN_PARTNER"		, "운항선사"		, 200);
			grd라인.SetCol("NM_SHIP_YARD"	, "조선소"		, 120);

			grd라인.SetCol("CD_ENGINE"		, "엔진유형"		, 70);
			grd라인.SetCol("NM_MODEL"		, "모델명"		, 80);
			grd라인.SetCol("CD_MAKER"		, "제조사"		, 100);
			grd라인.SetCol("CLS_L"			, "대분류"		, 70);
			grd라인.SetCol("CLS_M"			, "중분류"		, 70);
			grd라인.SetCol("CLS_S"			, "소분류"		, 70);
			grd라인.SetCol("SERIAL"			, "일련번호"		, 110);

			grd라인.SetCol("NO_PLATE"		, "부품번호"		, 120);
			grd라인.SetCol("NM_PLATE"		, "부품명"		, 200);
			grd라인.SetCol("CD_ITEM"			, "재고코드"		, 70);
			grd라인.SetCol("NM_ITEM"			, "재고명"		, 200);
			grd라인.SetCol("UNIT"			, "단위"			, 50);
			grd라인.SetCol("UCODE"			, "U코드"		, 100);
			grd라인.SetCol("EZCODE"			, "K코드"		, 100);
			grd라인.SetCol("EZCODE2"			, "K코드2"		, 100);

			grd라인.SetDataMap("CD_ENGINE", GetDb.Code("CZ_MA00009"), "CODE", "NAME");
			grd라인.SetDataMap("CD_MAKER", GetDb.Code("CZ_MA00003"), "CODE", "NAME");
			grd라인.SetDataMap("CLS_L", GetDb.Code("MA_B000030"), "CODE", "NAME");
			grd라인.SetDataMap("CLS_M", GetDb.Code("MA_B000031"), "CODE", "NAME");
			grd라인.SetDataMap("CLS_S", GetDb.Code("MA_B000032"), "CODE", "NAME");
			grd라인.SetDataMap("UNIT", GetDb.Code("MA_B000004"), "CODE", "NAME");
			grd라인.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;

			grd라인.SetDefault("20.01.13.03", SumPositionEnum.None);
		}		

		protected override void InitPaint()
		{
			lbl건수.TextAlign = ContentAlignment.MiddleLeft;		// 자동 우측정렬이 되버려서 다시 재설정

			if (!GetDb.IsAdmin(LoginInfo.EmployeeNo))
			{
				cbo키워드.RemoveItem("EZCODE");
				chkEZ코드.Visible = false;
				grd헤드.Cols.Remove("EZCODE");
				grd라인.Cols.Remove("EZCODE");
				grd라인.Cols.Remove("EZCODE2");
			}

			cbo키워드.SetValue("UCODE");
			Cbo키워드_SelectionChangeCommitted(null, null);
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			cbo키워드.SelectionChangeCommitted += Cbo키워드_SelectionChangeCommitted;
			btn엑셀.Click += Btn엑셀_Click;

			chk품목코드.CheckedChanged += Chk코드_CheckedChanged;
			chk재고코드.CheckedChanged += Chk코드_CheckedChanged;
			chkU코드.CheckedChanged += Chk코드_CheckedChanged;
			chkEZ코드.CheckedChanged += Chk코드_CheckedChanged;

			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;
		}

		private void Cbo키워드_SelectionChangeCommitted(object sender, EventArgs e)
		{
			foreach (Control c in pnl검색조건.Controls)
			{
				if (c is CheckBoxExt o)
				{
					o.Checked = false;

					if (o.GetTag() == cbo키워드.GetValue().Replace("2", ""))
						o.Checked = true;
				}
			}
		}

		private void Chk코드_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxExt chk = (CheckBoxExt)sender;

			if (!chk.Checked)
				return;

			foreach (Control c in pnl검색조건.Controls)
			{
				if (chk != c && c is CheckBoxExt o)
					o.Checked = false;
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DebugMode debugMode = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;

			//  필수값 체크
			if (chkU코드.GetCheckedControl().GetTag() == "")
			{
				ShowMessage("검색조건을 선택하세요.");
				return;
			}

			Util.ShowProgress(DD("기부속 데이터 조회 중입니다."));
			ToggleColumn();

			// 조회
			DBMgr dbm = new DBMgr
			{
				DebugMode = debugMode
			,	Procedure = "PS_CZ_MA_ENGINE_PART_IMPLOSION_H"
			};
			dbm.AddParameterRange(GetSearchCond("H").Parameters);
			dbm.AddParameter("@" + cbo키워드.GetValue(), tbx키워드.Text);
			DataTable dt = dbm.GetDataTable();

			grd헤드.Binding = dt;
			Util.CloseProgress();
		}

		private void Btn엑셀_Click(object sender, EventArgs e)
		{
			DebugMode debugMode = ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;

			//  필수값 체크
			if (chkU코드.GetCheckedControl().GetTag() == "")
			{
				ShowMessage("검색조건을 선택하세요.");
				return;
			}

			OpenFileDialog f = new OpenFileDialog
			{
				Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx"
			};

			if (f.ShowDialog() != DialogResult.OK)
				return;

			ExcelReader excel = new ExcelReader();
			DataTable dtExcel = excel.Read(f.FileName);

			if (dtExcel.Rows.Count == 0)
			{
				ShowMessage("엑셀파일을 읽을 수 없습니다.");
				return;
			}

			string codes = "";
			codes = string.Join(",", dtExcel.AsEnumerable().Select(x => "'" + x[0] + "'").ToArray());

			Util.ShowProgress(DD("기부속 데이터 조회 중입니다."));
			ToggleColumn();

			// 조회
			DBMgr dbm = new DBMgr
			{
				DebugMode = debugMode
			,	Procedure = "PS_CZ_MA_ENGINE_PART_IMPLOSION_H"
			};			
			dbm.AddParameterRange(GetSearchCond("H").Parameters);
			dbm.AddParameter("@" + cbo키워드.GetValue() + "_X", codes);
			
			grd헤드.Binding = dbm.GetDataTable();
			Util.CloseProgress();
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			//string colName = chkU코드.GetCheckedControl().GetTag();
			string colName = cbo키워드.GetValue();

			if (colName == "")
				return;
			
			if (grd헤드.DetailQueryNeed)
			{
				DBParameters dbp = GetSearchCond("L");
				dbp["@NO_PLATE"] = grd헤드.GetValue("NO_PLATE");
				dbp["@CD_ITEM"] = grd헤드.GetValue("CD_ITEM");
				dbp["@UCODE"] = grd헤드.GetValue("UCODE");
				dbp["@EZCODE"] = grd헤드.GetValue("EZCODE");
				dbp["@EZCODE2"] = grd헤드.GetValue("EZCODE2");

				DBMgr dbm = new DBMgr
				{
					DebugMode = DebugMode.None
				,	Procedure = "PS_CZ_MA_ENGINE_PART_IMPLOSION_L"
				};
				dbm.AddParameterRange(dbp.Parameters);
				dt = dbm.GetDataTable();
			}

			if (!GetDb.IsAdmin(LoginInfo.EmployeeNo))
			{
				dt.Columns.RemoveCol("EZCODE");
				dt.Columns.RemoveCol("EZCODE2");
			}

			grd라인.BindingAdd(dt, colName + " = '" + grd헤드.GetValue(colName) + "'");
		}
		
		private DBParameters GetSearchCond(string mode)
		{			
			DBParameters dbp = new DBParameters();
			dbp.Add("@NO_IMO"		, ctx호선번호.CodeValue);
			dbp.Add("@CD_PARTNER"	, ctx운항선사.CodeValue);
			dbp.Add("@YN_UPLOAD"	, chk업로드.GetValue());

			if (mode == "H")
			{
				dbp.Add("@CHK_" + chkU코드.GetCheckedControl().GetTag(), "Y");
				dbp.Add("@CNT_REPEAT", cur건수.DecimalValue);
			}

			return dbp;
		}

		private void ToggleColumn()
		{
			grd헤드.Redraw = false;

			grd헤드.Cols["NO_PLATE"].Visible = false;
			grd헤드.Cols["CD_ITEM"].Visible = false;
			grd헤드.Cols["UCODE"].Visible = false;

			if (chk품목코드.Checked)	grd헤드.Cols["NO_PLATE"].Visible = true;
			if (chk재고코드.Checked)	grd헤드.Cols["CD_ITEM"].Visible = true;
			if (chkU코드.Checked)	grd헤드.Cols["UCODE"].Visible = true;

			if (grd헤드.Cols.Contains("EZCODE"))
			{
				grd헤드.Cols["EZCODE"].Visible = false;
				grd헤드.Cols["EZCODE2"].Visible = false;

				if (cbo키워드.GetValue() == "EZCODE")
					grd헤드.Cols["EZCODE"].Visible = true;
				if (cbo키워드.GetValue() == "EZCODE2")
					grd헤드.Cols["EZCODE2"].Visible = true;
			}

			grd헤드.Redraw = true;
		}

		#endregion
	}
}


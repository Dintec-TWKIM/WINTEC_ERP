using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using DzHelpFormLib;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Windows.Print;

using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Net;

namespace cz
{
	public partial class H_CZ_ADD_ITEM : Duzon.Common.Forms.CommonDialog
	{
		#region ===================================================================================================== Property
		
		public P_CZ_SA_INQ Caller { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_ADD_ITEM()
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
			flexH.DetailGrids = new FlexGrid[] { flexLS, flexLE };
		}

		private void InitGrid()
		{
			// ========== H
			flexH.BeginSetting(1, 1, false);

			flexH.SetCol("NO_IMO"		, "IMO번호"		, false);
			flexH.SetCol("NO_ENGINE"	, "엔빈번호"		, false);
			flexH.SetCol("CD_ENGINE"	, "구분코드"		, false);
			flexH.SetCol("NM_ENGINE"	, "구분"			, 160);
			flexH.SetCol("NM_MAKER"		, "제조사"		, 160);
			flexH.SetCol("NM_MODEL"		, "사양"			, 160);
			flexH.SetCol("CAPACITY"		, "용량"			, 160);
			flexH.SetCol("SERIAL"		, "일련번호"		, 160);
			flexH.SetCol("CLS_L"		, "대분류"		, false);

			flexH.Cols["NM_ENGINE"].TextAlign = TextAlignEnum.CenterCenter;

			flexH.SettingVersion = "16.05.31.04";
			flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// ========== L(Stock)
			flexLS.BeginSetting(1, 1, false);

			flexLS.SetCol("CHK"			, "S"			, 30	, true, CheckTypeEnum.Y_N);
			flexLS.SetCol("NO_IMO"		, "IMO번호"		, false);
			flexLS.SetCol("NO_ENGINE"	, "엔빈번호"		, false);
			flexLS.SetCol("NO_DSP"		, "순번"			, 45	, true);	// EDIT
			flexLS.SetCol("QT"			, "수량"			, 60	, true, typeof(decimal), FormatTpType.QUANTITY);	// EDIT
			flexLS.SetCol("NM_SUBJECT"	, "주제"			, false);
			flexLS.SetCol("NO_PLATE"	, "품목코드"		, 130);
			flexLS.SetCol("NM_PLATE"	, "품목명"		, 297);	
			flexLS.SetCol("UCODE"		, "U코드"		, 100);	
			flexLS.SetCol("UNIT"		, "단위"			, 50);
			flexLS.SetCol("CD_ITEM"		, "재고코드"		, 90);
			flexLS.SetCol("WEIGHT"		, "무게"			, 65	, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			flexLS.SetCol("CLS_L"		, "대분류"		, false);
						
			flexLS.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexLS.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			flexLS.SetDataMap("UNIT", Util.GetDB_CODE("MA_B000004"), "CODE", "NAME");
			flexLS.SetDummyColumn("CHK");

			flexLS.KeyActionEnter = KeyActionEnum.MoveDown;
			flexLS.SettingVersion = "16.05.31.05";
			flexLS.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			// ========== L(Edit)
			flexLE.BeginSetting(1, 1, false);
				 
			flexLE.SetCol("CHK"			, "S"			, 30	, true, CheckTypeEnum.Y_N);
			flexLE.SetCol("NO_IMO"		, "IMO번호"		, false);
			flexLE.SetCol("NO_ENGINE"	, "엔빈번호"		, false);
			flexLE.SetCol("NO_DSP"		, "순번"			, 45	, true);	// EDIT
			flexLE.SetCol("QT"			, "수량"			, 60	, true, typeof(decimal), FormatTpType.QUANTITY);	// EDIT
			flexLE.SetCol("NM_SUBJECT"	, "주제"			, false);
			flexLE.SetCol("NO_PLATE"	, "품목코드"		, 130	, true);	// EDIT
			flexLE.SetCol("NM_PLATE"	, "품목명"		, 297	, true);	// EDIT
			flexLE.SetCol("UCODE"		, "U코드"		, 100	, false);
			flexLE.SetCol("UNIT"		, "단위"			, 50	, true);	// EDIT
			flexLE.SetCol("CD_ITEM"		, "재고코드"		, 90	, true);	// EDIT
			flexLE.SetCol("WEIGHT"		, "무게"			, 65	, 5	, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);	// EDIT	
			flexLE.SetCol("CLS_L"		, "대분류"		, false);
				
			flexLE.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexLE.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			flexLE.SetDataMap("UNIT", Util.GetDB_CODE("MA_B000004"), "CODE", "NAME");
			flexLE.SetDummyColumn("CHK");

			flexLE.KeyActionEnter = KeyActionEnum.MoveDown;
			flexLE.SettingVersion = "16.05.31.05";
			flexLE.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			tbx품목코드.KeyDown += new KeyEventHandler(txt품목코드_KeyDown);
			tab.SelectedIndexChanged += new EventHandler(tab_SelectedIndexChanged);

			btn조회.Click += new EventHandler(btn조회_Click);
			btn저장.Click += new EventHandler(btn저장_Click);
			btn취소.Click += new EventHandler(btn취소_Click);

			btn엑셀양식.Click += new EventHandler(btn엑셀양식_Click);
			btn엑셀업로드.Click += new EventHandler(btn엑셀업로드_Click);
			
			btn추가.Click += new EventHandler(btn추가_Click);
			btn삭제.Click += new EventHandler(btn삭제_Click);
			
			flexH.AfterRowChange += new RangeEventHandler(flexH_AfterRowChange);
			flexLS.ValidateEdit += new ValidateEditEventHandler(flexLS_ValidateEdit);
			flexLE.ValidateEdit += new ValidateEditEventHandler(flexLE_ValidateEdit);
		}

		protected override void InitPaint()
		{
			this.tbx호선번호.Text = Caller.Quotation.선명;

			DataTable dt = DBMgr.GetDataTable("PS_CZ_MA_HULL_ENGINE", Caller.Quotation.Imo번호);
			flexH.Binding = dt;
			tab_SelectedIndexChanged(null, null);

			if (!Certify.IsLive())
			{
				flexH.Cols.Remove("SERIAL");
			}
		}

		#endregion

		#region ==================================================================================================== Event

		private void txt품목코드_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				btn조회_Click(null, null);
			}
		}

		private void tab_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tab.SelectedIndex == 0)
			{
				btn조회.Enabled = true;
				
				btn엑셀양식.Enabled = false;
				btn엑셀업로드.Enabled = false;
				btn추가.Enabled = false;
				btn삭제.Enabled = false;
			}
			else
			{
				btn조회.Enabled = false;

				btn엑셀양식.Enabled = true;
				btn엑셀업로드.Enabled = true;
				btn추가.Enabled = true;
				btn삭제.Enabled = true;
			}
		}

		#endregion

		#region ==================================================================================================== 버튼 이벤트

		private void btn취소_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn엑셀양식_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			if (dlg.ShowDialog() != DialogResult.OK) return;

			string fileName = "ExcelForm_Engine_Item.xls";
			WebClient wc = new WebClient();
			wc.DownloadFile(Global.MainFrame.HostURL + "/shared/CZ/" + fileName, dlg.SelectedPath + @"\" + fileName);
		}

		private void btn엑셀업로드_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlf = new OpenFileDialog();
			dlf.Filter = Global.MainFrame.DD("엑셀 파일") + " (*.xls)|*.xls";

			if (dlf.ShowDialog() != DialogResult.OK) return;

			Excel excel = new Excel();
			DataTable dtEXCEL = excel.StartLoadExcel(dlf.FileName, 0, 3).Select("NO_PLATE <> '' AND NM_PLATE <> ''").CopyToDataTable();
			DataTable dtLE = flexLE.DataTable.Clone();

			for (int i = 0; i < dtEXCEL.Rows.Count; i++)
			{
				DataRow newRow = dtLE.NewRow();
				newRow["CHK"] = "Y";
				newRow["NO_IMO"] = flexH["NO_IMO"];
				newRow["NO_ENGINE"] = flexH["NO_ENGINE"];
				newRow["NM_SUBJECT"] = flexH["NM_MAKER"] + " " + flexH["NM_MODEL"];
				newRow["NO_DSP"] = dtEXCEL.Rows[i]["NO_DSP"];
				newRow["QT"] = dtEXCEL.Rows[i]["QT"];
				newRow["NO_PLATE"] = dtEXCEL.Rows[i]["NO_PLATE"];
				newRow["NM_PLATE"] = dtEXCEL.Rows[i]["NM_PLATE"];
				newRow["UNIT"] = dtEXCEL.Rows[i]["UNIT"];
				newRow["CD_ITEM"] = dtEXCEL.Rows[i]["CD_ITEM"];
				newRow["WEIGHT"] = dtEXCEL.Rows[i]["WEIGHT"];
				newRow["CLS_L"] = flexH["CLS_L"];
				dtLE.Rows.Add(newRow);
			}

			flexLE.BindingAdd(dtLE, "", false);
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filterLS = "NO_ENGINE = " + flexH["NO_ENGINE"] + " AND NO_PLATE LIKE '%" + tbx품목코드.Text + "%'";
			string filterLE = "NO_ENGINE = " + flexH["NO_ENGINE"];

			flexLS.Redraw = false;
			flexLE.Redraw = false;

			DataTable dtLS = null;
			DataTable dtLE = null;

			if (flexH.DetailQueryNeed)
			{
				string query = @"
SELECT
	  'N'		AS CHK
	, NO_IMO	AS NO_IMO
	, NO_ENGINE	AS NO_ENGINE
	, '" + flexH["NM_MAKER"] + " " + flexH["NM_MODEL"] + @"'	AS NM_SUBJECT
	, ''		AS NO_DSP
	, ''		AS QT
	, NO_PLATE	AS NO_PLATE
	, NM_PLATE	AS NM_PLATE	
	, UNIT		AS UNIT
	, A.UCODE		AS UCODE
	, COALESCE(B.CD_ITEM, C.CD_ITEM, A.CD_ITEM)	AS CD_ITEM
	, WEIGHT	AS WEIGHT
	, '" + flexH["CLS_L"] + @"'	AS CLS_L
FROM V_CZ_MA_HULL_ENGINE_ITEM_R2	AS A
LEFT JOIN V_CZ_MA_PITEM				AS B ON B.CD_COMPANY = '" + Caller.Quotation.회사코드 + @"' AND ISNULL(A.UCODE, '') != '' AND A.UCODE = B.UCODE
LEFT JOIN V_CZ_MA_PITEM				AS C ON C.CD_COMPANY = '" + Caller.Quotation.회사코드 + @"' AND ISNULL(A.EZCODE, '') != '' AND A.EZCODE = C.EZCODE
WHERE 1 = 1
	AND NO_IMO = '" + Caller.Quotation.Imo번호 + @"'
	AND NO_ENGINE = " + flexH["NO_ENGINE"];

				dtLS = DBHelper.GetDataTable(query);
				dtLE = dtLS.Clone();
			}

			flexLS.BindingAdd(dtLS, filterLS);
			flexLE.BindingAdd(dtLE, filterLE);
			
			flexLS.Redraw = true;
			flexLE.Redraw = true;
		}

		private void flexLS_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			if (flexLS.Cols[e.Col].Name == "NO_DSP" || flexLS.Cols[e.Col].Name == "QT")
			{
				if (flexLS["NO_DSP"].ToString() != "" || flexLS["QT"].ToString() != "") flexLS["CHK"] = "Y";
				else flexLS["CHK"] = "N";
			}
		}

		private void flexLE_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			if (flexLE.Cols[e.Col].Name == "NO_PLATE")
			{
				DataRow[] row = flexLS.DataTable.Select("NO_ENGINE = '" + flexH["NO_ENGINE"] + "' AND NO_PLATE = '" + flexLE.EditData + "'");

				if (row.Length > 0)
				{
					Global.MainFrame.ShowMessage("이미 기부속 정보에 존재하는 아이템입니다!");
					flexLE["NO_PLATE"] = "";
					flexLE.Col = flexLE.Cols["NO_PLATE"].Index;
				}
			}
		}

		#endregion

		#region ==================================================================================================== Search

		private void btn조회_Click(object sender, EventArgs e)
		{
			flexH_AfterRowChange(null, null);
		}

		#endregion

		#region ==================================================================================================== Add

		private void btn추가_Click(object sender, EventArgs e)
		{
			flexLE.Rows.Add();
			flexLE.Row = flexLE.Rows.Count - 1;
			flexLE["CHK"] = "Y";
			flexLE["NO_IMO"] = flexH["NO_IMO"];
			flexLE["NO_ENGINE"] = flexH["NO_ENGINE"];
			flexLE["NM_SUBJECT"] = flexH["NM_MAKER"] + " " + flexH["NM_MODEL"];
			flexLE["UNIT"] = "PCS";
			flexLE["CLS_L"] = flexH["CLS_L"];
			flexLE.AddFinished();
		}

		#endregion

		#region ==================================================================================================== Save

		private void btn저장_Click(object sender, EventArgs e)
		{
			// 편집탭 기부속 정보 추가
			DataRow[] rows = flexLE.DataTable.Select("CHK = 'Y'");

			if (rows.Length > 0)
			{
				if (Global.MainFrame.ShowMessage("기부속 정보에 등록하시겠습니까?", "QY2") == DialogResult.Yes)
				{
					DataTable dtEDIT = rows.CopyToDataTable();
					string xml = Util.GetTO_Xml(dtEDIT, RowState.Added);
					DBMgr.ExecuteNonQuery("PX_CZ_MA_HULL_ENGINE_ITEM", new object[] { xml });
				}
			}

			// 선택탭 & 편집탭 병합
			DataRow[] rowLS = flexLS.DataTable.Select("CHK = 'Y'");
			DataRow[] rowLE = flexLE.DataTable.Select("CHK = 'Y'");

			DataTable dt = null;
			if (rowLS.Length > 0)
			{
				dt = rowLS.CopyToDataTable();
				if (rowLE.Length > 0) dt.Merge(rowLE.CopyToDataTable());
			}
			else
			{
				if (rowLE.Length > 0) dt = rowLE.CopyToDataTable();
			}

			if (dt == null) return;

			// 부모폼에 추가
			FlexGrid flexL = Caller.그리드라인;

			foreach (DataRow row in dt.Rows)
			{
				flexL.Rows.Add();
				flexL.Row = flexL.Rows.Count - 1;

				flexL["NO_LINE"] = (int)flexL.Aggregate(AggregateEnum.Max, "NO_LINE") + 1;
				flexL["NO_DSP"] = row["NO_DSP"];
				flexL["GRP_ITEM"] = row["CLS_L"];
				flexL["NM_SUBJECT"] = row["NM_SUBJECT"];
				flexL["CD_ITEM_PARTNER"] = row["NO_PLATE"];
				flexL["NM_ITEM_PARTNER"] = row["NM_PLATE"];
				flexL["UNIT"] = row["UNIT"];
				flexL["QT"] = row["QT"];
				flexL["CD_ITEM"] = row["CD_ITEM"];
				flexL["UCODE"] = row["UCODE"];
				flexL["TP_BOM"] = "S";
				flexL["NO_ENGINE"] = row["NO_ENGINE"];
			}

			flexL.Sort(SortFlags.Ascending, flexL.Cols["NO_DSP"].Index);
			Caller.SetGridStyle();

			// 선택탭 체크 해제(재바인딩)
			DataTable dtH = DBMgr.GetDataTable("PS_CZ_MA_HULL_ENGINE", Caller.Quotation.Imo번호);
			flexH.Binding = dtH;

			// 편집탭 초기화
			Util.Clear(flexLE);
		}

		#endregion

		#region ==================================================================================================== Save

		private void btn삭제_Click(object sender, EventArgs e)
		{
			flexLE.Rows.Remove(flexLE.Row);
		}

		#endregion
	}
}


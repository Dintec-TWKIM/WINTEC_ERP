using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
	public partial class P_CZ_MM_CARRY_OUT_HGS_MGT : PageBase
	{
		string CompanyCode;
		string EmpNumber;
		bool SaveGrid;
		
		#region ==================================================================================================== Constructor

		public P_CZ_MM_CARRY_OUT_HGS_MGT()
		{
			InitializeComponent();
			StartUp.Certify(this);

			CompanyCode = LoginInfo.CompanyCode;
			EmpNumber = LoginInfo.UserID;
			SaveGrid = false;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			InitControl();
			InitGrid(grd전체);
			InitGrid(grd편집);
			InitUploadGrid(grd업로드1);
			InitUploadGrid(grd업로드2);
			InitUploadGrid(grd업로드3);
			InitEvent();
		}

		private void InitControl()
		{
			tbx포커스.Left = -1000;
			BindWorkCombo();
			this.MainGrids = new FlexGrid[] { grd편집, grd업로드1 };
		}

		private void InitGrid(FlexGrid flexGrid)
		{
			flexGrid.BeginSetting(2, 1, false);

			flexGrid.SetCol("SEQ"				, "순번"			, 40);
			flexGrid.SetCol("NO_FILE"			, "파일번호"		, 85);
			flexGrid.SetCol("NO_ORDER"			, "공사번호"		, 85);
			flexGrid.SetCol("NM_EMP"			, "발자담당자"	, false);
			flexGrid.SetCol("NO_POR"			, "POR번호"		, 130);
			flexGrid.SetCol("NO_GIR"			, "출하지시번호"	, 85);			
			flexGrid.SetCol("NO_LOG"			, "물류번호"		, false);
			flexGrid.SetCol("NO_ITEM"			, "항번"			, 40);
			flexGrid.SetCol("NO_PLATE"			, "품목코드"		, 130);
			flexGrid.SetCol("NM_PLATE"			, "품목명"		, 240);
			flexGrid.SetCol("UCODE"				, "U코드"		, false);
			flexGrid.SetCol("KCODE"				, "K코드"		, false);
			flexGrid.SetCol("QT_GIR"			, "예약"			, 45	, false, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_OUT"			, "반출"			, 45	, false, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("UM_OUT"			, "단가"			, 82	, false, typeof(decimal), FormatTpType.MONEY);
			
			flexGrid.SetCol("NO_KEY"			, "발주키"		, false);
			flexGrid.SetCol("NO_PO"				, "발주번호"		, 100);
			flexGrid.SetCol("NO_LINE"			, "항번"			, false);
			flexGrid.SetCol("NO_DSP"			, "순번"			, 40);
			flexGrid.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 130);
			flexGrid.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 240);
			flexGrid.SetCol("CD_ITEM"			, "재고코드"		, 80);
			flexGrid.SetCol("QT_PO"				, "발주"			, 45	, false, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_GR"				, "입고"			, 45	, false, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("UM"				, "단가"			, 82	, false, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("CD_LOCATION"		, "로케이션"		, 60);
			flexGrid.SetCol("YN_STOCK"			, "재고"			, 40);

			flexGrid[0, flexGrid.Cols["SEQ"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NO_FILE"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NO_ORDER"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NM_EMP"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NO_POR"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NO_GIR"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NO_LOG"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NO_ITEM"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NO_PLATE"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["NM_PLATE"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["UCODE"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["KCODE"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["QT_GIR"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["QT_OUT"].Index] = "현대글로벌서비스";
			flexGrid[0, flexGrid.Cols["UM_OUT"].Index] = "현대글로벌서비스";

			flexGrid[0, flexGrid.Cols["NO_KEY"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["NO_PO"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["NO_LINE"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["NO_DSP"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["CD_ITEM_PARTNER"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["NM_ITEM_PARTNER"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["CD_ITEM"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["QT_PO"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["QT_GR"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["UM"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["CD_LOCATION"].Index] = "딘텍";
			flexGrid[0, flexGrid.Cols["YN_STOCK"].Index] = "딘텍";

			flexGrid.Cols["SEQ"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NO_ORDER"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NO_POR"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NO_GIR"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["NO_ITEM"].Format = "####.##";
			flexGrid.Cols["NO_ITEM"].TextAlign = TextAlignEnum.CenterCenter;

			flexGrid.Cols["NO_DSP"].Format = "####.##";
			flexGrid.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["CD_LOCATION"].TextAlign = TextAlignEnum.CenterCenter;
			flexGrid.Cols["YN_STOCK"].TextAlign = TextAlignEnum.CenterCenter;

			flexGrid.Cols["NO_POR"].Visible = false;					// 안보여줘도 될것 같으니 숨기자			
			flexGrid.Styles.Add("CHECKED").BackColor = Color.Yellow;	// 선택된 행 스타일 추가
			flexGrid.SetDefault("19.03.20.01", SumPositionEnum.None);
		}

		private void InitUploadGrid(FlexGrid flexGrid)
		{
			flexGrid.BeginSetting(1, 1, false);

			string columns = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			for (int i = 0; i < 20; i++)
				flexGrid.SetCol("COL" + (i + 1).ToString(), columns[i].ToString(), 100, true);

			flexGrid.SetDefault("19.03.20.01", SumPositionEnum.None);
			flexGrid.Rows.Add();
		}

		private void InitEvent()
		{
			btn현대엑셀.Click += new EventHandler(btn현대엑셀_Click);
			btn업데이트.Click += new EventHandler(btn업데이트_Click);
			btn발주검색.Click += new EventHandler(btn발주검색_Click);

			grd전체.DoubleClick += new EventHandler(grd전체_DoubleClick);
			grd편집.DoubleClick += new EventHandler(grd편집_DoubleClick);
			grd편집.KeyDown += new KeyEventHandler(grd편집_KeyDown);
			grd업로드1.KeyDown += new KeyEventHandler(grd업로드_KeyDown);
			grd업로드2.KeyDown += new KeyEventHandler(grd업로드_KeyDown);
			grd업로드3.KeyDown += new KeyEventHandler(grd업로드_KeyDown);
		}
		
		#endregion

		#region ==================================================================================================== Event

		private void timer_Tick(object sender, EventArgs e)
		{
			OnToolBarSearchButtonClicked(null, null);
		}

		#endregion

		#region ==================================================================================================== 버튼이벤트

		private void btn현대엑셀_Click(object sender, EventArgs e)
		{
			bool popupDebug = false;

			if (Control.ModifierKeys == Keys.Control)
				popupDebug = true;
			
			// 엑셀업로드
			try
			{
				DataTable dtExcel;

				if (SaveGrid)
				{
					dtExcel = grd업로드1.GetTableFromGrid();

					// 1행을 컬럼이름으로 변경
					for (int j = 0; j < dtExcel.Columns.Count; j++)
					{
						if (dtExcel.Rows[0][j].ToString() == "")
							break;

						dtExcel.Columns[j].ColumnName = dtExcel.Rows[0][j].ToString();
					}

					// 1행은 삭제
					dtExcel.Rows.RemoveAt(0);
				}
				else
				{
					// 엑셀파일 선택
					OpenFileDialog f = new OpenFileDialog();
					f.Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx";

					if (f.ShowDialog() != DialogResult.OK)
						return;

					// 엑셀 → DataTable 변환
					string fileName = f.FileName;
					ExcelReader excelReader = new ExcelReader();
					dtExcel = excelReader.Read(fileName, 1, 2);
				}
				
				string typeCode = "";
				MsgControl.ShowMsg(DD("엑셀 업로드 중입니다."));

				// 컬럼이름 변경
				if (dtExcel.Columns.Contains("순번"))
				{
					// HGS엑셀
					typeCode = "MAP";
					dtExcel.Columns["순번"].ColumnName = "SEQ";
					dtExcel.Columns["HGS 공사번호"].ColumnName = "NO_ORDER";	// KBS1234567
					dtExcel.Columns["항목번호"].ColumnName = "NO_ITEM";
					dtExcel.Columns["출하지시번호"].ColumnName = "NO_GIR";
					dtExcel.Columns["Plate No"].ColumnName = "NO_PLATE";
					dtExcel.Columns["Description"].ColumnName = "NM_PLATE";
					dtExcel.Columns["불출수량"].ColumnName = "QT_OUT";
					dtExcel.Columns["U-CODE"].ColumnName = "UCODE";
					dtExcel.Columns["POR NO"].ColumnName = "NO_POR";			// KBS1234567-0001001
					dtExcel.Columns["DEALER_PO"].ColumnName = "NO_FILE";
					dtExcel.Columns["출하지시일"].ColumnName = "DT_GIR";
					dtExcel.Columns["불출일자"].ColumnName = "DT_OUT";
					dtExcel.Columns["계약화폐"].ColumnName = "CD_EXCH";
					dtExcel.Columns["수주단가"].ColumnName = "UM_OUT";
					dtExcel.Columns["불출금액"].ColumnName = "AM_OUT";
				}
				else if (dtExcel.Columns.Contains("고객PO"))
				{
					foreach (DataRow row in dtExcel.Select("고객PO IS NULL"))
						row.Delete();

					// HGS + PANTOS VLOOKUP 엑셀
					typeCode = "PAN";
					//dtExcel.Columns["순번"].ColumnName = "SEQ";
					dtExcel.Columns["Parts Code"].ColumnName = "NO_PLATE";
					dtExcel.Columns["Description"].ColumnName = "NM_PLATE";
					dtExcel.Columns["출고지시수량"].ColumnName = "QT_OUT";
					dtExcel.Columns["AS-UCODE"].ColumnName = "UCODE";
					dtExcel.Columns["POR NO"].ColumnName = "NO_POR";			// KBS1234567-0001001
					dtExcel.Columns["고객PO"].ColumnName = "NO_FILE";
					dtExcel.Columns["Unit Price"].ColumnName = "UM_OUT";

					// 
					dtExcel.Columns.Add("SEQ");
					dtExcel.Columns.Add("NO_ORDER");

					for (int i = 0; i < dtExcel.Rows.Count; i++)
					{
						dtExcel.Rows[i]["SEQ"] = i + 1;
						dtExcel.Rows[i]["NO_ORDER"] = dtExcel.Rows[i]["NO_POR"].ToString().Substring(0, 10);
					}
				}
				else
				{
					// 오류발생 해야함
				}

				// ********** 콤마 분리하기 (DB에서 하면 느리니 C#에서 시도)
				
				
				DataTable dtItem = DBMgr.GetDataTable("PX_CZ_MM_CARRY_OUT_HGS_UPLOAD", false, popupDebug, CompanyCode, typeCode, DBNull.Value, EmpNumber, GetTo.Xml(dtExcel));

				//DBMgr dbm = new DBMgr();
				//dbm.Procedure = "PX_CZ_MM_CARRY_OUT_HGS_UPLOAD";
				//dbm.AddParameter("@CD_COMPANY", companyCode);
				//dbm.AddParameter("@NO_FILE", fileNumber);
				//dbm.AddParameter("@NO_LINE", grdHead["NO_LINE"]);

				// 업로드가 끝났으면 조회 한번 해줌
				BindWorkCombo();
				OnToolBarSearchButtonClicked(null, null);

				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}
		
		private void btn업데이트_Click(object sender, EventArgs e)
		{
			DebugMode debugMode = ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.Print;

			string typeCode = cbo반출일자.GetValue().Split(',')[1];
			string workDate = cbo반출일자.GetValue().Split(',')[0];

			DBMgr.GetDataTable("PX_CZ_MM_CARRY_OUT_HGS_UPLOAD", debugMode, CompanyCode, typeCode, workDate, EmpNumber, DBNull.Value);

			OnToolBarSearchButtonClicked(null, null);
		}

		private void btn발주검색_Click(object sender, EventArgs e)
		{
			string fileNumber = grd편집["NO_FILE"].ToString();

			H_CZ_PO_SEARCH f = new H_CZ_PO_SEARCH(fileNumber);

			if (f.ShowDialog() == DialogResult.OK)
			{
				string[] colNames = { "NO_PO", "NO_LINE", "NO_DSP", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "CD_ITEM", "QT_PO", "QT_GR", "UM", "YN_STOCK" };

				foreach (string s in colNames)
					grd편집[s] = f.List[s];

				//grd편집["YN_STOCK"] = "N";
			}
		}

		private void SplitFileNumber(DataTable dtUpload)
		{
			if (dtUpload.Columns.Contains("NO_FILE"))
			{
				dtUpload.Columns.Add("NO_FILE_O", typeof(string));
				dtUpload.Columns.Add("YN_DUMMY", typeof(string));

				while (true)
				{
					DataRow[] rows = dtUpload.Select("NO_FILE LIKE '%,%'", "SEQ");

					if (rows.Length == 0)
						break;

					foreach (DataRow oldRow in rows)
					{
						string oriValue = oldRow["NO_FILE"].ToString();

						// ,로 분리
						string[] s = oriValue.Split(',');

						// 대표 파일번호 (첫번째가 대표 파일임)
						string repValue = s[0].Trim().Split('-')[0];

						// 기존행
						oldRow["NO_FILE"] = repValue;
						oldRow["NO_FILE_O"] = oriValue;

						for (int i = 1; i < s.Length; i++)
						{
							// 연속된 파일번호
							string nowValue = s[i].Trim().Split('-')[0];

							if (nowValue.Length < repValue.Length)
								nowValue = repValue.Remove(repValue.Length - nowValue.Length) + nowValue;

							// 신규행
							DataRow newRow = dtUpload.NewRow();
							newRow.ItemArray = (object[])oldRow.ItemArray.Clone();
							newRow["NO_FILE"] = nowValue;
							newRow["NO_FILE_O"] = oriValue;
							newRow["YN_DUMMY"] = "Y";
							dtUpload.Rows.Add(newRow);
						}
					}
				}

				// 순수 파일 부분만 가져오기
				char[] flag = { '-', '(', '.' };

				// - 분리하기
				foreach (char c in flag)
				{
					DataRow[] rows = dtUpload.Select("NO_FILE LIKE '%" + c + "%'");

					foreach (DataRow row in rows)
					{						
						row["NO_FILE_O"] = row["NO_FILE"];
						row["NO_FILE"] = row["NO_FILE"].ToString().StartsWith("ST") ? row["NO_FILE"] : row["NO_FILE"].ToString().Split(c)[0];
					}
				}
			}
		}

		#endregion

		#region ==================================================================================================== 그리드이벤트

		private void grd전체_DoubleClick(object sender, EventArgs e)
		{
			if (!grd전체.HasNormalRow || grd전체.MouseCol <= 0)
				return;
			
			// 헤더클릭
			if (grd전체.MouseRow < grd전체.Rows.Fixed)
				SetGridStyle();
		}
		
		private void grd편집_DoubleClick(object sender, EventArgs e)
		{
			if (!grd편집.HasNormalRow || grd편집.MouseCol <= 0)
				return;

			// 행클릭
			if (grd편집.MouseRow >= grd편집.Rows.Fixed)
				btn발주검색_Click(null, null);
		}

		private void grd편집_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Delete)
			{
				string[] poCols = { "NO_PO", "NO_LINE", "NO_DSP", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "CD_ITEM", "QT_PO", "QT_GR", "UM", "YN_STOCK" };

				if (grd편집.Row >= grd편집.Rows.Fixed && grd편집.Cols[grd편집.Col].Name.In(poCols))
				{
					foreach (string s in poCols)
						grd편집.DataTable.Select("SEQ = " + grd편집["SEQ"])[0][s] = DBNull.Value;
				}
			}
		}

		private void grd업로드_KeyDown(object sender, KeyEventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			if (flexGrid.Row != flexGrid.Rows.Fixed || flexGrid.Col != flexGrid.Cols.Fixed)
				return;
		
			if (e.KeyData == (Keys.Control | Keys.V))
			{
				string[,] clipboard = Util.GetClipboardValues();
				int index = flexGrid.Row;	// 시작인덱스 저장 (행이 클립보다 많은 경우는 .Row가 안바뀌지만 클립보드가 더 많은 경우에는 .Row가 바뀌므로 미리 저장함)

				for (int i = 0; i < clipboard.GetLength(0); i++)
				{
					int row = index + i;
					int j = 0;

					for (int col = flexGrid.Col; col < flexGrid.Cols.Count; col++)
					{
						// 클립보드 넘어가는 순간 제외
						if (j == clipboard.GetLength(1))
							break;

						flexGrid[row, col] = clipboard[i, j];
						j++;
					}

					// 마지막 행이면 종료
					if (i == clipboard.GetLength(0) - 1)
						break;

					// 클립보드는 아직 남았는데 그리드의 마지막 행인 경우 행 추가
					if (row == flexGrid.Rows.Count - 1)
						flexGrid.Rows.Add();				
				}

				this.ToolBarAddButtonEnabled = true;
				this.ToolBarSaveButtonEnabled = true;
			}
		}

		private void SetGridStyle()
		{
			// 셀병합
			if (grd전체.HasNormalRow)
			{
				grd전체.Clear(ClearFlags.UserData, grd전체.Rows.Fixed, 1, grd전체.Rows.Count - 1, grd전체.Cols.Count - 1);
				grd전체.Merge("SEQ", "SEQ", "NO_FILE", "NO_ORDER", "NM_EMP", "NO_POR", "NO_GIR", "NO_LOG", "NO_ITEM", "NO_PLATE", "NM_PLATE", "UCODE", "KCODE", "QT_GIR", "QT_OUT", "UM_OUT");
				grd전체.Merge("NO_KEY", "NO_PO", "NO_LINE", "NO_DSP", "CD_ITEM_PARTNER", "NM_ITEM_PARTNER", "CD_ITEM", "QT_PO", "QT_GR", "UM", "CD_LOCATION", "YN_STOCK");
			}

			// 선택된놈 표시
			for (int i = grd전체.Rows.Fixed; i < grd전체.Rows.Count; i++)
			{
				if (grd전체[i, "YN_EXAM"].ToString() == "Y")
					grd전체.Rows[i].Style = grd전체.Styles["CHECKED"];
				else
					grd전체.Rows[i].Style = null;
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			string orderCode = "";
			if (rdo구분G.Checked) orderCode = "N";
			if (rdo구분S.Checked) orderCode = "Y";

			DBMgr dbm = new DBMgr();
			//dbm.DebugMode = DebugMode.Popup;
			dbm.Procedure = "PS_CZ_MM_CARRY_OUT_HGS_MGT";
			dbm.DebugMode = DebugMode.Print;
			dbm.AddParameter("@CD_COMPANY", CompanyCode);
			dbm.AddParameter("@CD_TYPE"	  , cbo반출일자.GetValue().Split(',')[1]);
			dbm.AddParameter("@DT_WORK"	  , cbo반출일자.GetValue().Split(',')[0]);
			dbm.AddParameter("@NO_FILE"	  , tbx파일번호.GetValue());
			dbm.AddParameter("@NO_ORDER"  , tbx공사번호.GetValue());
			dbm.AddParameter("@NO_PLATE"  , tbx품목코드.GetValue());
			dbm.AddParameter("@NM_PLATE"  , tbx품목명.GetValue());
			dbm.AddParameter("@YN_STOCK"  , orderCode);

			DataTable dt = dbm.GetDataTable();

			grd전체.Binding = dt;																	// DataBind(dt) 쓰지말자.. 일단 오래 걸리고 병합셀이랑 긴칸이랑 헛갈림			
			grd편집.DataBind(GetTo.DataTable(dt.Select("YN_EDIT = 'Y' OR ISNULL(NO_PO, '') = ''")));	// 요긴 쓰자.. 어짜피 몇줄 안나옴

			SetGridStyle();
			this.ToolBarDeleteButtonEnabled = true;
		}

		#endregion

		#region ==================================================================================================== Add
		
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{			
			grd업로드1.Rows.RemoveRange(grd업로드1.Rows.Fixed, grd업로드1.Rows.Count - grd업로드1.Rows.Fixed);
			grd업로드1.Rows.Add();

			grd업로드2.Rows.RemoveRange(grd업로드2.Rows.Fixed, grd업로드2.Rows.Count - grd업로드2.Rows.Fixed);
			grd업로드2.Rows.Add();

			grd업로드3.Rows.RemoveRange(grd업로드3.Rows.Fixed, grd업로드3.Rows.Count - grd업로드3.Rows.Fixed);
			grd업로드3.Rows.Add();
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab편집)
			{
				DataTable dt = GetTo.DataTable(grd편집.GetChanges().Select("NO_PO <> ''"));

				if (dt == null)
				{
					ShowMessage("변경된 내용이 없습니다.");
					grd편집.AcceptChanges();
					return;
				}

				DBMgr.ExecuteNonQuery("PX_CZ_MM_CARRY_OUT_HGS_MGT", GetTo.Xml(dt));

				grd편집.AcceptChanges();
			}
			else if (tab메인.SelectedTab == tab업로드1 || tab메인.SelectedTab == tab업로드2)
			{
				// ********** 1번 그리드
				DataTable dtUp1 = grd업로드1.GetTableFromGrid();

				// 1행을 컬럼이름으로 변경
				for (int j = 0; j < dtUp1.Columns.Count; j++)
				{
					if (dtUp1.Rows[0][j].ToString() == "")
						break;

					dtUp1.Columns[j].ColumnName = dtUp1.Rows[0][j].ToString();
				}

				// 1행은 삭제
				dtUp1.Rows.RemoveAt(0);

				// PANTOS 엑셀 (from 이종희씨) → 라벨이랑 다를수 있으므로 라벨엑셀도 올려야 함
				dtUp1.Columns["번호"].ColumnName = "SEQ";
				dtUp1.Columns["Ord No"].ColumnName = "NO_FILE";
				dtUp1.Columns["PO 번호"].ColumnName = "NO_POR";
				//dtUp1.Columns["자재코드"].ColumnName = "KCODE";
				//dtUp1.Columns["AS-UCODE"].ColumnName = "UCODE";
				dtUp1.Columns["품목명"].ColumnName = "NM_PLATE";
				//dtUp1.Columns["예약 수량"].ColumnName = "QT_GIR";
				dtUp1.Columns["적용수량"].ColumnName = "QT_OUT";
				dtUp1.Columns["PLATE NO"].ColumnName = "NO_PLATE";
				dtUp1.Columns["공사번호"].ColumnName = "NO_ORDER";
				dtUp1.Columns["Stock Po No"].ColumnName = "NO_LOT";

				// 병합되어 있는 경우가 있으므로 빈값을 채워줌
				DataRow repRow = dtUp1.NewRow();
				//string[] mergedCols = { "NO_FILE", "NO_ORDER", "NO_POR", "NO_PLATE", "NM_PLATE", "UCODE", "KCODE", "QT_GIR" };
				string[] mergedCols = { "NO_FILE", "NO_ORDER", "NO_POR", "NO_PLATE", "NM_PLATE" };

				foreach (DataRow row in dtUp1.Rows)
				{					
					if (GetTo.String(row["NO_FILE"]).Trim() != "")
					{
						foreach (string s in mergedCols)
							repRow[s] = row[s];
					}
					else
					{
						foreach (string s in mergedCols)
							row[s] = repRow[s];
					}
				}

				// 마지막열 삭제
				if (dtUp1.Rows[dtUp1.Rows.Count - 1]["SEQ"].ToString() == "")
				    dtUp1.Rows[dtUp1.Rows.Count - 1].Delete();

				// 파일넘버 분리
				SplitFileNumber(dtUp1);

				// ********** 2번 그리드 => 라벨 자료
				DataTable dtUp2 = grd업로드2.GetTableFromGrid();

				// 1행을 컬럼이름으로 변경
				for (int j = 0; j < dtUp2.Columns.Count; j++)
				{
					if (dtUp2.Rows[0][j].ToString() == "")
						break;

					dtUp2.Columns[j].ColumnName = dtUp2.Rows[0][j].ToString();
				}

				// 1행은 삭제
				dtUp2.Rows.RemoveAt(0);

				// 컬럼이름 변경
				//dtUp2.Columns["번호"].ColumnName = "SEQ";
				dtUp2.Columns["라인"].ColumnName = "SEQ";
				dtUp2.Columns["제조 Lot"].ColumnName = "NO_LOT";
				dtUp2.Columns["피킹수량"].ColumnName = "QT_PICK";

				// ********** 저장
				//string up1Xml = GetTo.Xml(dtUp1, "", "SEQ", "NO_FILE", "NO_FILE_O", "NO_ORDER", "NO_POR", "NO_PLATE", "NM_PLATE", "UCODE", "KCODE", "QT_GIR", "QT_OUT", "NO_LOT");
				string up1Xml = GetTo.Xml(dtUp1, "", "SEQ", "NO_FILE", "NO_FILE_O", "NO_ORDER", "NO_POR", "NO_PLATE", "NM_PLATE", "QT_OUT", "NO_LOT");
				string up2Xml = GetTo.Xml(dtUp2, "", "SEQ", "QT_PICK", "NO_LOT");

				DBMgr.GetDataTable("PX_CZ_MM_CARRY_OUT_HGS_UPLOAD_GRID", true, true, up1Xml, up2Xml);
				//grd업로드1.AcceptChanges();
				//grd업로드2.AcceptChanges();
			}
			else if (tab메인.SelectedTab == tab업로드3)
			{
				// ********** 1번 그리드
				DataTable dtUp = grd업로드3.GetTableFromGrid();

				// 1행을 컬럼이름으로 변경
				for (int j = 0; j < dtUp.Columns.Count; j++)
				{
					if (dtUp.Rows[0][j].ToString() == "")
						break;

					dtUp.Columns[j].ColumnName = dtUp.Rows[0][j].ToString();
				}

				// 1행은 삭제
				dtUp.Rows.RemoveAt(0);

				// PANTOS 엑셀 (from 이종희씨) 통합본 (라인 쪼개지는 것 까지)
				dtUp.Columns["라인"].ColumnName = "SEQ";
				dtUp.Columns["선사 Po No."].ColumnName = "NO_FILE";
				dtUp.Columns["S/O POR번호"].ColumnName = "NO_POR";
				dtUp.Columns["품목명"].ColumnName = "NM_PLATE";
				dtUp.Columns["수량"].ColumnName = "QT_OUT";
				dtUp.Columns["플레이트번호"].ColumnName = "NO_PLATE";
				dtUp.Columns["프로젝트번호"].ColumnName = "NO_ORDER";

				// 마지막열 삭제
				if (dtUp.Rows[dtUp.Rows.Count - 1]["SEQ"].ToString() == "")
					dtUp.Rows[dtUp.Rows.Count - 1].Delete();

				// 파일넘버 분리
				SplitFileNumber(dtUp);

				// ********** 저장
				//string up1Xml = GetTo.Xml(dtUp1, "", "SEQ", "NO_FILE", "NO_FILE_O", "NO_ORDER", "NO_POR", "NO_PLATE", "NM_PLATE", "UCODE", "KCODE", "QT_GIR", "QT_OUT", "NO_LOT");
				string upXml = GetTo.Xml(dtUp, "", "SEQ", "NO_FILE", "NO_FILE_O", "NO_ORDER", "NO_POR", "NO_PLATE", "NM_PLATE", "QT_OUT");

				DBMgr.GetDataTable("PX_CZ_MM_CARRY_OUT_HGS_UPLOAD_GRID", true, true, upXml);
				//grd업로드1.AcceptChanges();
				//grd업로드2.AcceptChanges();
			}

			ShowMessage(PageResultMode.SaveGood);
		}



		#endregion

		#region ==================================================================================================== Delete

		public override void  OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
				return;
			if (EmpNumber != "S-343" && !Util.CheckPW())
				return;

			string query = @"
DELETE FROM CZ_MM_CARRY_OUT_HGSL WHERE CD_COMPANY = @CD_COMPANY AND CD_TYPE = @CD_TYPE AND DT_WORK = @DT_WORK
DELETE FROM CZ_MM_CARRY_OUT_HGSH WHERE CD_COMPANY = @CD_COMPANY AND CD_TYPE = @CD_TYPE AND DT_WORK = @DT_WORK";

			DBMgr dbm = new DBMgr();
			dbm.Query = query;
			dbm.AddParameter("@CD_COMPANY", CompanyCode);
			dbm.AddParameter("@CD_TYPE"	  , cbo반출일자.GetValue().Split(',')[1]);
			dbm.AddParameter("@DT_WORK"	  , cbo반출일자.GetValue().Split(',')[0]);
			dbm.ExecuteNonQuery();

			BindWorkCombo();
			grd편집.Clear();
			grd편집.Clear();

			ShowMessage(PageResultMode.SaveGood);
		}

		#endregion

		private void BindWorkCombo()
		{
			// 반출일자 바인딩, 가장 기본 정보이므로 시작할때 바로 바인딩함
			string query = @"
SELECT
	  DT_WORK + ',' + CD_TYPE	AS CODE
	, CONVERT(NVARCHAR(10), CONVERT(DATE, DT_WORK), 111) + ' '
	+ CASE
		WHEN CD_TYPE = 'MAP' THEN '(일반품)'
		WHEN CD_TYPE = 'PAN' THEN '(재고)'
	  END						AS NAME
FROM CZ_MM_CARRY_OUT_HGSH
GROUP BY DT_WORK, CD_TYPE
ORDER BY DT_WORK DESC";

			DataTable dt = DBMgr.GetDataTable(query);

			cbo반출일자.DataBind(dt, false);

			if (cbo반출일자.Items.Count > 0)
				cbo반출일자.SelectedIndex = 0;
		}
	}
}

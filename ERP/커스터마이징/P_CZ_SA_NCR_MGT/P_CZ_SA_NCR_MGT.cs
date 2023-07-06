using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using DzHelpFormLib;
using Duzon.Windows.Print;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using Duzon.Common.Util;
using System.Net;
using System.IO;

namespace cz
{
	public partial class P_CZ_SA_NCR_MGT : PageBase
	{
		#region ==================================================================================================== Constructor

		public P_CZ_SA_NCR_MGT()
		{
			InitializeComponent();

			StartUp.Certify(this);
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
			// 콤보박스
			DataSet ds = GetDb.Code("CD_FILE", "CZ_MA00005");
			cbo파일번호.DataBind(ds.Tables[0], true);

			//DataTable dt = new DataTable();
			//dt.Columns.Add("CODE");
			//dt.Columns.Add("NAME");

			//dt.Rows.Add("NO_REF"		, "문의번호");
			//dt.Rows.Add("NO_PO_PARTNER"	, "주문번호");
			//dt.Rows.Add("NO_IV"			, "계산서번호");

			//cbo키워드.DataBind(dt, false);
			//cbo키워드.SelectedIndex = 0;

			// 나머지
			tbx포커스.Left = -1000;

			dtp등록일자.StartDateToString = Util.GetToday().Left(4) + "0101";
			dtp등록일자.EndDateToString = Util.GetToday();

			ctx회사코드.CodeValue = LoginInfo.CompanyCode;
			ctx회사코드.CodeName = LoginInfo.CompanyName;

			//grd헤드.DetailGrids = new FlexGrid[] { grd라인 };
			//MainGrids = new FlexGrid[] { grd라인 };

			Grant.CanDelete = false;
			Grant.CanPrint = false;
		}

		private void InitGrid()
		{
			// ---------------------------------------------------------------------------------------------------- 재고납기
			grd재고납기.BeginSetting(2, 1, false);

			grd재고납기.SetCol("CD_COMPANY"		, "회사명"	, false);
			grd재고납기.SetCol("NO_FILE"			, "파일번호"	, 80);
			grd재고납기.SetCol("NM_PARTNER"		, "매출처"	, 230);
			grd재고납기.SetCol("NM_VESSEL"		, "선명"		, 150);			
			grd재고납기.SetCol("DT_QTN"			, "견적일자"	, 80	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grd재고납기.SetCol("NM_EMP"			, "담당자"	, 65);
			grd재고납기.SetCol("ORIGIN"			, "원산지"	, 80);

			grd재고납기.SetCol("NO_LINE"			, "항번"  	, false);
            grd재고납기.SetCol("NO_DSP"			, "순번"		, 40);			
			grd재고납기.SetCol("CD_ITEM_PARTNER"	, "품목코드"	, 140);
			grd재고납기.SetCol("NM_ITEM_PARTNER"	, "품목명"	, 200);
			grd재고납기.SetCol("CD_ITEM"			, "재고코드"	, 80);
			grd재고납기.SetCol("UCODE"			, "U코드"	, false);
			grd재고납기.SetCol("KCODE"			, "K코드"	, false);

			grd재고납기.SetCol("NM_SUPPLIER"		, "매입처"	, 120);
			grd재고납기.SetCol("UNIT_QTN"			, "단위"		, 45);
			grd재고납기.SetCol("QT_QTN"			, "수량"		, 45	, false, typeof(decimal), FormatTpType.QUANTITY);
			grd재고납기.SetCol("LT"				, "납기"		, 45	, false, typeof(decimal), FormatTpType.MONEY);
			grd재고납기.SetCol("QT_AVST"			, "재고"		, 45	, false, typeof(decimal), FormatTpType.QUANTITY);
			grd재고납기.SetCol("QT_AVGR"			, "입고"		, 45	, false, typeof(decimal), FormatTpType.QUANTITY);
			grd재고납기.SetCol("FILE_ICON"		, "첨부"		, 35);
			grd재고납기.SetCol("DC_RMK"			, "비고"		, 150);

			grd재고납기[0, grd재고납기.Cols["QT_QTN"].Index] = "견적수량";
			grd재고납기[0, grd재고납기.Cols["LT"].Index] = "견적수량";

			grd재고납기[0, grd재고납기.Cols["QT_AVST"].Index] = "가용재고수량";
			grd재고납기[0, grd재고납기.Cols["QT_AVGR"].Index] = "가용재고수량";

			grd재고납기.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			grd재고납기.Cols["NO_DSP"].Format = "####.##";
			grd재고납기.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd재고납기.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grd재고납기.Cols["CD_ITEM"].TextAlign = TextAlignEnum.CenterCenter;
			grd재고납기.Cols["UNIT_QTN"].TextAlign = TextAlignEnum.CenterCenter;
			grd재고납기.Cols["FILE_ICON"].ImageAlign = ImageAlignEnum.CenterCenter;
			grd재고납기.SetDataMap("UNIT_QTN", GetDb.Code("MA_B000004"), "CODE", "NAME");
			
			grd재고납기.SetDefault("19.05.01.07", SumPositionEnum.None);

			// ---------------------------------------------------------------------------------------------------- 견적현황표
			grd견적현황표.BeginSetting(2, 1, false);

			grd견적현황표.SetCol("CD_COMPANY"		, "회사명"	, false);
			grd견적현황표.SetCol("NO_FILE"		, "파일번호"	, 80);
			grd견적현황표.SetCol("NM_PARTNER"		, "매출처"	, 230);
			grd견적현황표.SetCol("NM_VESSEL"		, "선명"		, 150);
			grd견적현황표.SetCol("DT_QTN"			, "견적일자"	, 80	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grd견적현황표.SetCol("NM_EMP"			, "담당자"	, 65);
			grd견적현황표.SetCol("CD_EXCH"		, "통화"		, 45);
			grd견적현황표.SetCol("RT_EXCH"		, "환율"		, 55	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grd견적현황표.SetCol("NM_SUPPLIER"	, "매입처"	, 120);
			grd견적현황표.SetCol("AM_EX_P"		, "외화금액"	, 90	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd견적현황표.SetCol("AM_KR_P"		, "원화금액"	, 90	, false, typeof(decimal), FormatTpType.MONEY);
			
			grd견적현황표.SetCol("AM_EX_S"		, "외화금액"	, 90	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd견적현황표.SetCol("AM_KR_S"		, "원화금액"	, 90	, false, typeof(decimal), FormatTpType.MONEY);

			grd견적현황표.SetCol("AM_MARGIN"		, "금액"		, 90	, false, typeof(decimal), FormatTpType.MONEY);
			grd견적현황표.SetCol("RT_MARGIN"		, "율"		, 45	, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grd견적현황표.SetCol("DC_RMK_QTN"		, "비고"		, 250);

			grd견적현황표[0, grd견적현황표.Cols["NM_SUPPLIER"].Index] = "매입";
			grd견적현황표[0, grd견적현황표.Cols["AM_EX_P"].Index] = "매입";
			grd견적현황표[0, grd견적현황표.Cols["AM_KR_P"].Index] = "매입";

			grd견적현황표[0, grd견적현황표.Cols["AM_EX_S"].Index] = "매출";
			grd견적현황표[0, grd견적현황표.Cols["AM_KR_S"].Index] = "매출";

			grd견적현황표[0, grd견적현황표.Cols["AM_MARGIN"].Index] = "이윤";
			grd견적현황표[0, grd견적현황표.Cols["RT_MARGIN"].Index] = "이윤";

			grd견적현황표.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			grd견적현황표.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grd견적현황표.Cols["CD_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			grd견적현황표.SetDataMap("CD_EXCH", GetDb.Code("MA_B000005"), "CODE", "NAME");

			grd견적현황표.SetDefault("19.05.01.07", SumPositionEnum.None);
		}

		protected override void InitPaint()
		{
			tbx파일번호.Focus();
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{			
			tbx파일번호.KeyDown += new KeyEventHandler(tbx검색_KeyDown);
			//tbx키워드.KeyDown += new KeyEventHandler(tbx검색_KeyDown);

			ctx대분류.QueryBefore += new BpQueryHandler(ctx대분류_QueryBefore);
			ctx중분류.QueryBefore += new BpQueryHandler(ctx중분류_QueryBefore);
			ctx대분류.QueryAfter += new BpQueryHandler(ctx대분류_QueryAfter);

			grd재고납기.DoubleClick += new EventHandler(grd재고미적용_DoubleClick);
		}

		private void tbx검색_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				if (((TextBoxExt)sender).Text.Trim() == "")
					ShowMessage("검색어를 입력하세요!");
				else
					OnToolBarSearchButtonClicked(null, null);
			}
		}

		private void ctx대분류_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
		}

		private void ctx중분류_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
			e.HelpParam.P42_CD_FIELD2 = ctx대분류.CodeValue;
		}

		private void ctx대분류_QueryAfter(object sender, BpQueryArgs e)
		{
			ctx중분류.Clear();
		}

		private void grd재고미적용_DoubleClick(object sender, EventArgs e)
		{
            FlexGrid flexGrid = (FlexGrid)sender;
			int row = flexGrid.MouseRow;
			int col = flexGrid.MouseCol;
		
			// 헤더클릭
			if (row < flexGrid.Rows.Fixed)
				return;

			// ********** 첨부파일 열기
			string colName = flexGrid.Cols[flexGrid.Col].Name;

			if (colName == "FILE_ICON")
			{
				string query = @"
SELECT TOP 1
	NM_FILE_REAL
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + flexGrid["CD_COMPANY"] + @"'
	AND NO_KEY = '" + flexGrid["NO_FILE"] + @"'
	AND TP_STEP = '05'
ORDER BY NO_LINE DESC";

				DataTable dt = DBMgr.GetDataTable(query);
				FileMgr.Download_WF(flexGrid["CD_COMPANY"].ToString(), flexGrid["NO_FILE"].ToString(), dt.Rows[0][0].ToString(), true);
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DebugMode debug = Control.ModifierKeys == (Keys.Control | Keys.Alt) ? DebugMode.Popup : DebugMode.None;

			if (ctx회사코드.CodeValue == "")
			{
				ShowMessage(공통메세지._은는필수입력항목입니다, DD("회사코드"));
				return;
			}
			
			Util.ShowProgress(DD("조회중입니다."));
			tbx파일번호.Text = EngHanConverter.KorToEng(tbx파일번호.Text);
			tbx포커스.Focus();

			//// 키워드 파라메타
			//string keyPar = "@" + cbo키워드.GetValue();
			
			// 파일번호
			string fileNumber = tbx파일번호.Text.Trim();
			string fileCode = "";

			if (fileNumber != "")
			{
				cbo파일번호.SetValue("");

				if (fileNumber.Length < 10)
				{
					fileCode = fileNumber;
					fileNumber = "";					
				}
			}
			else
			{
				fileCode = cbo파일번호.GetValue();
			}
			
			// ***** 조회
			if (tab메인.SelectedTab == tab재고납기)
			{
				DBMgr dbm = new DBMgr();
				dbm.DebugMode = debug;
				dbm.Procedure = "PS_CZ_SA_NCR_MGT_STOCK_LT";
				dbm.AddParameter("@CD_COMPANY"	, ctx회사코드.CodeValue);
				dbm.AddParameter("@NO_FILE"		, fileNumber);
				dbm.AddParameter("@CD_FILE"		, fileCode);
				dbm.AddParameter("@DT_F"		, dtp등록일자.StartDateToString);
				dbm.AddParameter("@DT_T"		, dtp등록일자.EndDateToString);

				dbm.AddParameter("@NO_EMP"		, ctx담당자.CodeValue);
				dbm.AddParameter("@CD_PARTNER"	, cbm매출처.QueryWhereIn_WithValueMember);
				dbm.AddParameter("@NO_IMO"		, cbm호선.QueryWhereIn_WithValueMember);
				dbm.AddParameter("@CD_SUPPLIER"	, cbm매입처.QueryWhereIn_WithValueMember);

				dbm.AddParameter("@CLS_L"		, ctx대분류.CodeValue);
				dbm.AddParameter("@CLS_M"		, ctx중분류.CodeValue);

				dbm.AddParameter("@GT_AVST"		, chk가용재고.Checked ? "Y" : "N");
			
				DataTable dt = dbm.GetDataTable();

				foreach (DataRow row in dt.Rows)
					row["NM_SUPPLIER"] = GetTo.CompanyShortName(row["NM_SUPPLIER"]);

				grd재고납기.Binding = dt;
				SetGridStyle();
			}
			else if (tab메인.SelectedTab == tab견적현황표)
			{
				DBMgr dbm = new DBMgr();
				dbm.DebugMode = debug;
				dbm.Procedure = "PS_CZ_SA_NCR_MGT_QTN_STATUS";
				dbm.AddParameter("@CD_COMPANY"	, ctx회사코드.CodeValue);
				dbm.AddParameter("@NO_FILE"		, fileNumber);
				dbm.AddParameter("@CD_FILE"		, fileCode);
				dbm.AddParameter("@DT_F"		, dtp등록일자.StartDateToString);
				dbm.AddParameter("@DT_T"		, dtp등록일자.EndDateToString);

				dbm.AddParameter("@NO_EMP"		, ctx담당자.CodeValue);
				dbm.AddParameter("@CD_PARTNER"	, cbm매출처.QueryWhereIn_WithValueMember);
				dbm.AddParameter("@NO_IMO"		, cbm호선.QueryWhereIn_WithValueMember);
				dbm.AddParameter("@CD_SUPPLIER"	, cbm매입처.QueryWhereIn_WithValueMember);

				dbm.AddParameter("@CLS_L"		, ctx대분류.CodeValue);
				dbm.AddParameter("@CLS_M"		, ctx중분류.CodeValue);
			
				DataTable dt = dbm.GetDataTable();

				foreach (DataRow row in dt.Rows)
				{
					// 매입처 건수에 맞게 변경
					row["NM_SUPPLIER"] = GetTo.CompanyShortName(row["NM_SUPPLIER"]);
					int cnt = GetTo.Int(row["CNT_SUPPLIER"]);
					
					if (cnt > 1)
						row["NM_SUPPLIER"] = row["NM_SUPPLIER"] + " 외 " + (cnt - 1);

					// 이윤율
					decimal 매입 = GetTo.Decimal(row["AM_KR_P"]);
					decimal 매출 = GetTo.Decimal(row["AM_KR_S"]);
					row["AM_MARGIN"] = 매출 - 매입;
					row["RT_MARGIN"] = Calculator.이윤율계산(매입, 매출);
				}

				grd견적현황표.Binding = dt;
			}

			Util.CloseProgress();
		}

		#endregion

		public void SetGridStyle()
		{
			// 첨부파일 아이콘 이미지 추가
			for (int i = grd재고납기.Rows.Fixed; i < grd재고납기.Rows.Count; i++)
			{				
				Image icon = Icons.GetExtension("pdf");
				grd재고납기.SetCellImage(i, grd재고납기.Cols["FILE_ICON"].Index, icon);
			}			
		}
	}
}

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
using System.Collections;
using System.IO;
using System.Linq;

namespace cz
{
	public partial class P_CZ_PU_PO_RPT : PageBase
	{
		DataTable dtHsd;

		#region ===================================================================================================== Property

		public string CD_COMPANY { get; set; }

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_PU_PO_RPT()
		{
			StartUp.Certify(this);
			CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
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
			ctx회사코드.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
			ctx회사코드.CodeName = Global.MainFrame.LoginInfo.CompanyName;

			dtp일자.StartDateToString = Util.GetToday(-30);
			dtp일자.EndDateToString = Util.GetToday();

			Util.SetDB_CODE(cbo매출처그룹, "MA_B000065", true);

			// 검색콤보
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");
			
			dt.Rows.Add("발주번호", "NO_PO", "발주번호");
			dt.Rows.Add("발주번호", "NO_SO", "수주번호");

			// 유형
			DataTable dtTP_FILE = Util.GetDB_CODE("TP_FILE");

			dt.Rows.Add("유형", "", "");
			dt.Rows.Add("유형", "재고", "재고");
			dt.Rows.Add("유형", "", "----");

			foreach (DataRow row in dtTP_FILE.Rows)
			{
				string SO_PREFIX = row["CODE"].ToString();
				dt.Rows.Add("유형", SO_PREFIX, SO_PREFIX);
			}

			// 나머지
			dt.Rows.Add("일자", "PO", "발주일자");
			dt.Rows.Add("일자", "LIMIT", "납기일자");
			dt.Rows.Add("일자", "GR", "입고일자");

			dt.Rows.Add("금액", "", "금액검색");
			dt.Rows.Add("금액", "", "--------");
			dt.Rows.Add("금액", "P", "매입금액");

			dt.Rows.Add("통화", "_EX", "외화");
			dt.Rows.Add("통화", "", "원화");

			dt.Rows.Add("품목코드", "CD_ITEM_PARTNER", "품목코드");
			dt.Rows.Add("품목코드", "CD_ITEM", "재고코드");

			dt.Rows.Add("품목명", "NORMAL", "일반검색");
			dt.Rows.Add("품목명", "SMART", "스마트검색");

			cbo파일번호.DataSource = dt.Select("TYPE = '발주번호'").CopyToDataTable();
			cbo파일번호.SelectedIndex = 0;

			cbo유형.DataSource = dt.Select("TYPE = '유형'").CopyToDataTable();
			cbo유형.SelectedIndex = 0;

			cbo일자.DataSource = dt.Select("TYPE = '일자'").CopyToDataTable();
			cbo일자.SelectedIndex = 0;

			cbo금액.DataSource = dt.Select("TYPE = '금액'").CopyToDataTable();
			cbo금액.SelectedIndex = 0;

			cbo통화.DataSource = dt.Select("TYPE = '통화'").CopyToDataTable();
			cbo통화.SelectedIndex = 0;

			cbo품목코드.DataSource = dt.Select("TYPE = '품목코드'").CopyToDataTable();
			cbo품목코드.SelectedIndex = 0;

			cbo품목명.DataSource = dt.Select("TYPE = '품목명'").CopyToDataTable();
			cbo품목명.SelectedIndex = 0;

			MainGrids = new FlexGrid[] { flexPOL };
			flexPOH.DetailGrids = new FlexGrid[] { flexPOL };
			flexSUPPH.DetailGrids = new FlexGrid[] { flexSUPPL };

			///////////////////////////////////// 보간법 테스트
			DataTable dtSI = new DataTable();
			dtSI.Columns.Add("COL", typeof(string));
			dtSI.Rows.Add("CALL_R");
			dtSI.Rows.Add("CURR_BAL");
			dtSI.Rows.Add("CAPIT_BAL");
			dtSI.Rows.Add("KOSPI");
			dtSI.Rows.Add("DOW_JONES");
			dtSI.Rows.Add("NASDAQ");
			dtSI.Rows.Add("CUS_PR_K");
			dtSI.Rows.Add("CUS_PR_US");
			dtSI.Rows.Add("PROD_PR_K");
			dtSI.Rows.Add("PROD_PR_US");
			dtSI.Rows.Add("INDUST_K");
			dtSI.Rows.Add("INDUST_US");
			dtSI.Rows.Add("WTI");
			dtSI.Rows.Add("US_FED_R");
			dtSI.Rows.Add("EC_R_K");
			dtSI.Rows.Add("EC_R_US");
			dtSI.Rows.Add("GDP_K");
			dtSI.Rows.Add("GDP_US");
			grd보간법헤드.Binding = dtSI;
		}

		private void InitGrid()
		{
			// ==================================================================================================== 발주번호별
			flexPOH.BeginSetting(1, 1, false);
				
			flexPOH.SetCol("NO_PO"		, "발주번호"			, 90);
			flexPOH.SetCol("CD_PARTNER"	, "매입처코드"		, false);
			flexPOH.SetCol("LN_PARTNER"	, "매입처"			, 200);
			flexPOH.SetCol("NO_ORDER"	, "공사번호"			, 110);
			flexPOH.SetCol("DT_PO"		, "발주일자"			, 80	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexPOH.SetCol("DT_LIMIT"	, "납기일자"			, 80	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexPOH.SetCol("NO_EMP"		, "담당자사번"		, false);
			flexPOH.SetCol("NM_EMP"		, "담당자"			, 70);
			flexPOH.SetCol("CD_PURGRP"	, "구매그룹코드"		, false);
			flexPOH.SetCol("NM_PURGRP"	, "구매그룹"			, 90);
			flexPOH.SetCol("CD_CC"		, "C/C코드"			, false);
			flexPOH.SetCol("NM_CC"		, "C/C명"			, 90);
			flexPOH.SetCol("CD_EXCH"	, "통화코드"			, false);
			flexPOH.SetCol("NM_EXCH"	, "통화명"			, 45);
			flexPOH.SetCol("CLS_L"		, "대분류"			, 70);
			flexPOH.SetCol("CLS_M"		, "중분류"			, 70);
			flexPOH.SetCol("CLS_S"		, "소분류"			, 70);
			flexPOH.SetCol("RT_EXCH"	, "환율"				, false);
			flexPOH.SetCol("QT_PO"		, "발주"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexPOH.SetCol("QT_GR"		, "입고"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexPOH.SetCol("QT_IV"		, "정산"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexPOH.SetCol("QT_RT"		, "반품"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);			
			//flexPOH.SetCol("AM_EX_STD"	, "매입견적금액"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexPOH.SetCol("AM_EX"		, "매입금액"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexPOH.SetCol("AM"			, "매입금액(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			//flexPOH.SetCol("RT_DC"		, "이윤율"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			//flexPOH.SetCol("AM_EX_DC"	, "이윤"				, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			//flexPOH.SetCol("AM_KR_DC"	, "이윤(￦)"			, 110	, false	, typeof(decimal), FormatTpType.MONEY);
			flexPOH.SetCol("LT"			, "납기"				, 50	, false	, typeof(decimal), FormatTpType.MONEY);
			flexPOH.SetCol("CD_BUYER"	, "매출처코드"		, false);
			flexPOH.SetCol("LN_BUYER"	, "매출처"			, 200);
			flexPOH.SetCol("NM_BUYER_GRP"	, "매출처그룹"			, 70);
			flexPOH.SetCol("NO_IMO"		, "IMO"				, false);
			flexPOH.SetCol("NM_VESSEL"	, "선명"				, 150);
			flexPOH.SetCol("CD_PJT"		, "프로젝트"			, 100);

			flexPOH.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			flexPOH.Cols["NM_PURGRP"].TextAlign = TextAlignEnum.CenterCenter;
			flexPOH.Cols["NM_CC"].TextAlign = TextAlignEnum.CenterCenter;
			flexPOH.Cols["NM_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			flexPOH.Cols["NM_BUYER_GRP"].TextAlign = TextAlignEnum.CenterCenter;
			flexPOH.SetDataMap("CLS_L", GetDb.Code("MA_B000030"), "CODE", "NAME");
			flexPOH.SetDataMap("CLS_M", GetDb.Code("MA_B000031"), "CODE", "NAME");
			flexPOH.SetDataMap("CLS_S", GetDb.Code("MA_B000032"), "CODE", "NAME");

			flexPOH.SettingVersion = "19.04.01.01";
			flexPOH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flexPOH.SetExceptSumCol("RT_EXCH", "AM_EX", "LT");

			// ==================================================================================================== 매입처별
			flexSUPPH.BeginSetting(1, 1, false);

			flexSUPPH.SetCol("CD_PARTNER"	, "매입처코드"	, false);
			flexSUPPH.SetCol("LN_PARTNER"	, "매입처"		, 150);
			flexSUPPH.SetCol("CNT_PO"		, "발주건수"		, 60	, false	, typeof(decimal), FormatTpType.MONEY);
			flexSUPPH.SetCol("CNT_ITEM"		, "아이템수"		, 60	, false	, typeof(decimal), FormatTpType.MONEY);
			flexSUPPH.SetCol("AM"			, "발주금액(￦)"	, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			flexSUPPH.SettingVersion = "16.05.20.01";
			flexSUPPH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);


			// ==================================================================================================== 라인그리드
			InitGrid_L(flexPOL);
			InitGrid_L(flePARTL);
			InitGrid_L(flexSUPPL);


			// ********** 
			grd보간법헤드.BeginSetting(1, 1, false);
					
			grd보간법헤드.SetCol("COL", "컬럼"	, 100);

			grd보간법헤드.SetDefault("19.06.17.01", SumPositionEnum.None);



			grd보간법라인.BeginSetting(1, 1, false);
						
			grd보간법라인.SetCol("DAY"		, "일자"		, 80);
			grd보간법라인.SetCol("SEQ"		, "순번"		, 40);
			grd보간법라인.SetCol("VALUE"		, "값1"		, 70, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd보간법라인.SetCol("VALUE2"		, "값2"		, 70, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			grd보간법라인.SetDefault("19.06.17.01", SumPositionEnum.None);



			// Test
			grdTest.BeginSetting(1, 1, false);

			grdTest.SetCol("호환 NO_DWG"		, "호환 NO_DWG"	, 100);
			grdTest.SetCol("호환 REV"		, "호환 REV"		, 50);
			grdTest.SetCol("호환 DWG_SEQ"	, "호환 DWG_SEQ"	, 100);
			grdTest.SetCol("재고코드"			, "재고코드"		, 100);
			grdTest.SetCol("품목코드"			, "품목코드"		, 100);
			grdTest.SetCol("요약"			, "요약"			, 40);
			grdTest.SetCol("IMO NO."		, "IMO NO."		, 90);
			grdTest.SetCol("HULL NO."		, "HULL NO."	, 120);
			grdTest.SetCol("SHIP NAME"		, "SHIP NAME"	, 150);
			grdTest.SetCol("PJT NO."		, "PJT NO."		, 100);
			
			//
			grdTest.SetCol("비고1"			, "비고1"		, 100);
			grdTest.SetCol("비고2"			, "비고2"		, 100);

			grdTest.SetDefault("18.12.10.02", SumPositionEnum.None);
		}

		private void InitGrid_L(FlexGrid flexL)
		{
		    flexL.BeginSetting(1, 1, false);

			flexL.SetCol("NO_PO"			, "발주번호"			, 90);
			flexL.SetCol("NO_LINE"			, "발주항번"			, false);
			flexL.SetCol("NO_DSP"			, "순번"				, 40);
			flexL.SetCol("CD_PARTNER"		, "매입처코드"		, false);
			flexL.SetCol("LN_PARTNER"		, "매입처"			, 150);
			flexL.SetCol("NO_ORDER"			, "주문번호"			, 100);
			flexL.SetCol("CD_EXCH"			, "통화코드"			, false);
			flexL.SetCol("NM_EXCH"			, "통화명"			, 45);
			flexL.SetCol("RT_EXCH"			, "환율"				, false);
		    flexL.SetCol("NM_SUBJECT"		, "주제"				, 200);
		    flexL.SetCol("CD_ITEM_PARTNER"	, "품목코드"			, 120);
		    flexL.SetCol("NM_ITEM_PARTNER"	, "품목명"			, 230);
			flexL.SetCol("NM_GRP_ITEM"		, "유형"				, 100);
			flexL.SetCol("CD_ITEM"			, "재고코드"			, 100);
			flexL.SetCol("CLS_L"			, "대분류"			, false);
			flexL.SetCol("CLS_M"			, "중분류"			, false);
			flexL.SetCol("CLS_S"			, "소분류"			, false);
		    flexL.SetCol("NM_UNIT"			, "단위"				, 45);
		    flexL.SetCol("QT_PO"			, "발주"				, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			flexL.SetCol("QT_GR"			, "입고"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("QT_IV"			, "정산"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("QT_RT"			, "반품"				, 60	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("UM_EX_STD"		, "매입견적단가"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("AM_EX_STD"		, "매입견적금액"		, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flexL.SetCol("RT_DC"			, "DC(%)"			, 55	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    flexL.SetCol("UM_EX"			, "매입단가"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    flexL.SetCol("AM_EX"			, "매입금액"			, 110	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
		    flexL.SetCol("UM"				, "매입단가(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);
		    flexL.SetCol("AM"				, "매입금액(￦)"		, 110	, false	, typeof(decimal), FormatTpType.MONEY);		    
		    flexL.SetCol("LT"				, "납기"				, 50	, false	, typeof(decimal), FormatTpType.MONEY);
			flexL.SetCol("DT_LIMIT"			, "납기일자"			, 80	, false	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("DT_EXPECT"		, "확정일자"			, 80	, true	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("DT_EXDATE"		, "반출일자"			, 80	, true	, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flexL.SetCol("DC1"				, "비고1"			, 120);
		    flexL.SetCol("DC2"				, "비고2"			, 230);
			flexL.SetCol("DC4"				, "비고4"			, 300	, true);
			flexL.SetCol("CD_BUYER"			, "매출처코드"		, false);
			flexL.SetCol("LN_BUYER"			, "매출처"			, 200);
			flexL.SetCol("NM_BUYER_GRP"		, "매출처그룹"		, 70);
			flexL.SetCol("NO_IMO"			, "IMO"				, false);
			flexL.SetCol("NM_VESSEL"		, "선명"				, 150);			
			flexL.SetCol("NO_BL"			, "운송장"			, 120);

			flexL.Cols["NO_DSP"].Format = "####.##";
		    flexL.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter; 
			flexL.Cols["NM_EXCH"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["NM_UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			flexL.Cols["NM_BUYER_GRP"].TextAlign = TextAlignEnum.CenterCenter;

		    flexL.SettingVersion = "21.12.01.01";
		    flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flexL.SetExceptSumCol("RT_EXCH", "UM_EX_STD", "RT_DC", "UM_EX", "UM", "LT");

			// H → L 관계에서의 L그리드에서는 특정 항목 보이지 않게 처리
			if (flexL == flexPOL)
			{
				flexL.Cols["NO_PO"].Visible = false;
				flexL.Cols["LN_PARTNER"].Visible = false;
				flexL.Cols["NM_EXCH"].Visible = false;
				flexL.Cols["RT_EXCH"].Visible = false;
				flexL.Cols["LN_BUYER"].Visible = false;
				flexL.Cols["NM_VESSEL"].Visible = false;
			}

			if (flexL == flexSUPPH)
			{
				flexL.Cols["LN_PARTNER"].Visible = false;
			}
		}

		private void InitEvent()
		{
			txt파일번호.KeyDown += new KeyEventHandler(txt파일번호_KeyDown);
			flexPOH.AfterRowChange += new RangeEventHandler(flexPOH_AfterRowChange);
			flexSUPPH.AfterRowChange += new RangeEventHandler(flexSUPPH_AfterRowChange);			

			btn지급정책예외.Click += new EventHandler(btn지급정책예외_Click);
			btn지급정책적용.Click += new EventHandler(btn지급정책적용_Click);
			btn납기등록.Click += new EventHandler(btn납기등록_Click);

			btn기준HSD.Click += new EventHandler(btn기준HSD_Click);
			btn찾기HSD1.Click += new EventHandler(btn찾기HSD1_Click);
			btn찾기HSD2.Click += new EventHandler(btn찾기HSD2_Click);


			ctx대분류.QueryBefore += Ctx대분류_QueryBefore;
			ctx중분류.QueryBefore += Ctx중분류_QueryBefore;
			ctx소분류.QueryBefore += Ctx소분류_QueryBefore;

		}

		private void Ctx대분류_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
			e.HelpParam.P42_CD_FIELD2 = "";
		}

		private void Ctx중분류_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
			e.HelpParam.P42_CD_FIELD2 = ctx대분류.CodeValue;
		}

		private void Ctx소분류_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
			e.HelpParam.P42_CD_FIELD2 = ctx중분류.CodeValue;
		}



		// ------
		private void btn기준HSD_Click(object sender, EventArgs e)
		{
			// 엑셀파일 선택
			OpenFileDialog f = new OpenFileDialog();
			f.Filter = Global.MainFrame.DD("엑셀 파일") + "|*.xls;*.xlsx";

			if (f.ShowDialog() != DialogResult.OK)
				return;

			// 엑셀업로드
			try
			{
				MsgControl.ShowMsg(DD("엑셀 업로드 중입니다."));

				// 엑셀 → DataTable 변환
				string fileName = f.FileName;
				ExcelReader excelReader = new ExcelReader();
				DataTable dtExcel = excelReader.Read(fileName, 1, 2);

				dtExcel.Columns.Add("비고1", typeof(string));
				dtExcel.Columns.Add("비고2", typeof(string));

				//dtHsd
				grdTest.AutoGenerateColumns = true;
				grdTest.Binding = dtExcel;
				//grdTest.DataSource = dtExcel;
				

				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}

		private void btn찾기HSD1_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
				return;

			DirectoryInfo dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);

			// 엑셀업로드
			try
			{
				MsgControl.ShowMsg(DD("엑셀 업로드 중입니다."));

				foreach (FileInfo f in dir.GetFiles())
				{
					// 엑셀 → DataTable 변환
					string fileName = f.FullName;
					ExcelReader excelReader = new ExcelReader();
					DataTable dtExcel = excelReader.Read(fileName, 1, 2);

					//dtHsd
					for (int i = grdTest.Rows.Fixed; i < grdTest.Rows.Count; i++)
					{
						DataRow[] row = dtExcel.Select("재고코드 = '" + grdTest[i, "재고코드"] + "'");

						if (row.Length > 0)
						{
							grdTest[i, "비고1"] = row[0]["비고1"];
							grdTest[i, "비고2"] = row[0]["비고2"];
						}
					}
				}

				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}

		private void btn찾기HSD2_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
				return;

			DirectoryInfo dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
			
			// 엑셀업로드
			try
			{
				MsgControl.ShowMsg(DD("엑셀 업로드 중입니다."));

				foreach (FileInfo f in dir.GetFiles())
				{
					// 엑셀 → DataTable 변환
					string fileName = f.FullName;
					ExcelReader excelReader = new ExcelReader();
					DataSet dsExcel = ExcelReader.ToDataSet(fileName);

					//dtHsd
					for (int i = grdTest.Rows.Fixed; i < grdTest.Rows.Count; i++)
					{
						foreach (DataTable dt in dsExcel.Tables)
						{
							// 컬럼이름을 첫번째 행으로 바꾸기
							for (int j = 0; j < dt.Columns.Count; j++)
							{
								if (dt.Rows[0][j].ToString() != "")
									dt.Columns[j].ColumnName = dt.Rows[0][j].ToString();
								else
									dt.Columns[j].ColumnName = "F" + j.ToString();
							}

							// 찾기
							DataRow[] row = dt.Select("[기준 NO_PJT] = '" + grdTest[i, "PJT NO."] + "' AND [기준 NO_PLATE] = '" + grdTest[i, "품목코드"] + "'");

							if (row.Length > 0)
							{
								grdTest[i, "호환 NO_DWG"] = row[0]["호환 NO_DWG"];
								grdTest[i, "호환 REV"] = row[0]["호환 REV"];
								grdTest[i, "호환 DWG_SEQ"] = row[0]["호환 DWG_SEQ"];

								break;
							}
						}
					}
				}

				MsgControl.CloseMsg();
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				Util.ShowMessage(Util.GetErrorMessage(ex.Message));
			}
		}
		// ------

		protected override void InitPaint()
		{
			//spc매입처.SplitterDistance = 392;
		}

		#endregion

		#region ==================================================================================================== Event

		private void txt파일번호_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				OnToolBarSearchButtonClicked(null, null);
			}
		}

		private void btn지급정책예외_Click(object sender, EventArgs e)
		{
			if (tab.SelectedTab != tabPO)
			{
				ShowMessage("발주번호별 탭에서만 실행 가능합니다.");
				return;
			}

			if (GetTo.String(flexPOH["NO_PO"]) == "")
			{
				ShowMessage("선택된 파일이 없습니다.");
				return;
			}

			string query = @"
UPDATE PU_POH SET
	YN_EDIT_PAYMENT = 'Y'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_PO = '" + flexPOH["NO_PO"] + "'";

			DBMgr.ExecuteNonQuery(query);
			ShowMessage("저장되었습니다.");
		}

		private void btn지급정책적용_Click(object sender, EventArgs e)
		{
			if (tab.SelectedTab != tabPO)
			{
				ShowMessage("발주번호별 탭에서만 실행 가능합니다.");
				return;
			}

			if (GetTo.String(flexPOH["NO_PO"]) == "")
			{
				ShowMessage("선택된 파일이 없습니다.");
				return;
			}

			string query = @"
UPDATE PU_POH SET
	YN_EDIT_PAYMENT = 'N'
WHERE 1 = 1
	AND CD_COMPANY = '" + CD_COMPANY + @"'
	AND NO_PO = '" + flexPOH["NO_PO"] + "'";

			DBMgr.ExecuteNonQuery(query);
			ShowMessage("저장되었습니다.");
		}

		

		private void btn납기등록_Click(object sender, EventArgs e)
		{
			// 파일 다이얼로그
			OpenFileDialog f = new OpenFileDialog();
			f.Filter = Global.MainFrame.DD("모든 Excel 파일") + "|*.xls;*.xlsx";
			if (f.ShowDialog() != DialogResult.OK) return;
			
			// 엑셀 Import
			ExcelReader excel = new ExcelReader();
			DataTable dtExcel = excel.Read(f.FileName, 1, 3);

			if (dtExcel.Rows.Count == 0)
			{
			    ShowMessage("엑셀파일을 읽을 수 없습니다.");
			    return;
			}

			// 필드 이름 변경
			dtExcel.Columns["Parts Code"].ColumnName = "DC1";		// 패키지 비고1 (아이템코드)
			dtExcel.Columns["POR No"].ColumnName = "NO_ORDER";		// 딘텍 필드
			dtExcel.Columns["입고약속일"].ColumnName = "DT_EXPECT";	// 딘텍 필드
			dtExcel.Columns["불출"].ColumnName = "DT_EXDATE";		// 패키지의 공장출고일

			// POR No를 우리 형식에 맞게 변환
			foreach (DataRow row in dtExcel.Rows)
			{
				string s = row["NO_ORDER"].ToString();
				s = s.Substring(0, s.IndexOf("-"));
				row["NO_ORDER"] = s;

				row["DT_EXPECT"] = row["DT_EXPECT"].ToString().Replace("-", "");
				row["DT_EXDATE"] = row["DT_EXDATE"].ToString().Replace("-", "");
			}

			// 저장
			string xml = GetTo.Xml(dtExcel.DefaultView.ToTable(false, "NO_ORDER", "DC1", "DT_EXPECT", "DT_EXDATE"));
			DataTable dt = DBMgr.GetDataTable("PX_CZ_PU_PO_RPT_HGS", xml);

			// 조회
			txt파일번호.Text = dt.Rows[0]["NO_PO"].ToString();
			OnToolBarSearchButtonClicked(null, null);
		}

		#endregion

		#region ==================================================================================================== 그리드 이벤트

		private void flexPOH_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filter = "NO_PO = '" + flexPOH["NO_PO"] + "'";

			if (flexPOH.DetailQueryNeed)
			{
				DBParameters dbp = GetSearchCond();
				dbp["@NO_PO"] = flexPOH["NO_PO"];

				DataTable dt = DBMgr.GetDataTable("PS_CZ_PU_PO_RPT_L", dbp);
				flexPOL.BindingAdd(dt, filter);
			}
			else
			{
				flexPOL.BindingAdd(null, filter);
			}
		}

		private void flexSUPPH_AfterRowChange(object sender, RangeEventArgs e)
		{
			string filter = "CD_PARTNER = '" + flexSUPPH["CD_PARTNER"] + "'";

			if (flexSUPPH.DetailQueryNeed)
			{
				DBParameters dbp = GetSearchCond();
				dbp["CD_PARTNER"] = flexSUPPH["CD_PARTNER"];

				DataTable dt = DBMgr.GetDataTable("PS_CZ_PU_PO_RPT_L", dbp);
				flexSUPPL.BindingAdd(dt, filter);
			}
			else
			{
				flexSUPPL.BindingAdd(null, filter);
			}
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			if (tab.SelectedTab == tabPO)
			{
				flexPOH.Binding = DBMgr.GetDataTable("PS_CZ_PU_PO_RPT_H", GetSearchCond());
			}
			else if (tab.SelectedTab == tabSUPP)
			{
				flexSUPPH.Binding = DBMgr.GetDataTable("PS_CZ_PU_PO_RPT_H", GetSearchCond());
			}
			else if (tab.SelectedTab == tabITEM)
			{
				flePARTL.Binding = DBMgr.GetDataTable("PS_CZ_PU_PO_RPT_L", GetSearchCond());
			}
			else if (tab.SelectedTab == tab보간법)
			{
				string col = grd보간법헤드["COL"].ToString();

				// 범위
				string query = @"
SELECT
	  MIN(SEQ)	AS SEQ_MIN
	, MAX(SEQ)	AS SEQ_MAX
FROM
(
	SELECT
		  ROW_NUMBER() OVER (ORDER BY DAY) AS SEQ
		, DAY
	FROM V_CZ_LN_CURR
) AS A";

				DataTable dtRange = DBMgr.GetDataTable(query);
				int min = GetTo.Int(dtRange.Rows[0]["SEQ_MIN"]);
				int max = GetTo.Int(dtRange.Rows[0]["SEQ_MAX"]);

				// 로우데이터
//                query = @"
//SELECT
//	*
//FROM
//(
//	SELECT
//		  ROW_NUMBER() OVER (ORDER BY DAY) AS RN
//		, DAY
//		, " + col + @"
//	FROM V_CZ_LN_CURR
//) AS A
//WHERE " + col + " != 'NaN'";
				query = @"
SELECT
	  ROW_NUMBER() OVER (ORDER BY DAY) AS SEQ
	, DAY
	, " + col + @"	AS VALUE
FROM V_CZ_LN_CURR
ORDER BY SEQ
";

				DataTable dt = DBMgr.GetDataTable(query);

				// 알려진값 세팅
				var known = new Dictionary<double, double>();

				//foreach (DataRow row in dt.Select("VALUE <> 'NaN'"))
				//    known.Add(GetTo.Double(row["SEQ"]), GetTo.Double(row["VALUE"]));
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					if (dt.Rows[i]["VALUE"].ToString() != "NaN")
						known.Add(GetTo.Double(dt.Rows[i]["SEQ"]), GetTo.Double(dt.Rows[i]["VALUE"]));
				}

                
				// 보간법 스타트
				var scaler = new SplineInterpolator(known);
				var start = known.First().Key;
				var end = known.Last().Key;
				var step = 1;

				dt.Columns.Add("VALUE2", typeof(double));
				//foreach (DataRow row in dt.Select("VALUE = 'NaN'"))
				//{
				//    row["VALUE"] = scaler.GetValue(GetTo.Double(row["SEQ"]));
				//}

				for (var x = start + step; x <= end; x += step)
				{
					var y = scaler.GetValue(x);
					int i = GetTo.Int(x);
					dt.Rows[i - 1]["VALUE2"] = y;				
				}				

				grd보간법라인.Binding = dt;

				DataTable dtXml = dt.Copy();
				dtXml.Columns["VALUE2"].ColumnName = col;

				DBMgr.ExecuteNonQuery("PX_CZ_LN_CURR_INTERPOLATOR", DebugMode.None, GetTo.Xml(dtXml));
			}
		}

		private DBParameters GetSearchCond()
		{
			string NO_FILE_FIELD = GetCon.Value(cbo파일번호);

			string YN_GR = "";
			if (rdo입고N.Checked) YN_GR = "N";
			if (rdo입고Y.Checked) YN_GR = "Y";

			string YN_IV = "";
			if (rdo정산N.Checked) YN_IV = "N";
			if (rdo정산Y.Checked) YN_IV = "Y";

		    DBParameters dbp = new DBParameters();
		    dbp.Add("@MODE"					, tab.SelectedTab.Tag);
		    dbp.Add("@CD_COMPANY"			, ctx회사코드.CodeValue);	
		    dbp.Add("@" + NO_FILE_FIELD		, txt파일번호.Text);
		    dbp.Add("@TP_FILE"				, cbo유형.SelectedValue);
			dbp.Add("CD_PARTNER"			, ctx매입처.CodeValue);
			if (txt파일번호.Text == "")
			{
				dbp.Add("@DT"					, cbo일자.SelectedValue);
				dbp.Add("@DT_F"					, dtp일자.StartDateToString);
				dbp.Add("@DT_T"					, dtp일자.EndDateToString);
			}
		    dbp.Add("@NO_EMP"				, ctx담당자.CodeValue);
		    dbp.Add("@CD_PURGRP"			, ctx구매그룹.CodeValue);
		    dbp.Add("@NO_ORDER"				, txt공사번호.Text);
			dbp.Add("@CD_BUYER"				, ctx매출처.CodeValue);
		    dbp.Add("@CD_BUYER_GRP"			, cbo매출처그룹.SelectedValue);
		    dbp.Add("@NO_IMO"				, ctx호선번호.CodeValue);		   		    
		    dbp.Add("@AM_TYPE"				, cbo금액.SelectedValue);
		    dbp.Add("@AM_F"					, cur금액F.DecimalValue);
		    dbp.Add("@AM_T"					, cur금액T.DecimalValue);
		    dbp.Add("@EXCH_TYPE"			, cbo통화.SelectedValue);
		    dbp.Add("@NM_SUBJECT"			, txt주제.Text);
		    dbp.Add("@CD_ITEM_PARTNER"		, (cbo품목코드.SelectedIndex == 0) ? txt품목코드.Text : "");
		    dbp.Add("@CD_ITEM"				, (cbo품목코드.SelectedIndex == 1) ? txt품목코드.Text : "");
		    dbp.Add("@NM_ITEM_PARTNER"		, (cbo품목명.SelectedIndex == 0) ? txt품목명.Text : "");
		    dbp.Add("@NM_ITEM_PARTNER_SMART", (cbo품목명.SelectedIndex == 1) ? txt품목명.Text : "");
			dbp.Add("@YN_GR"				, YN_GR);
			dbp.Add("@YN_IV"				, YN_IV);
			
			dbp.Add("@CLS_L"				, ctx대분류.CodeValue);
			dbp.Add("@CLS_M"				, ctx중분류.CodeValue);
			dbp.Add("@CLS_S"				, ctx소분류.CodeValue);
			dbp.Add("@NO_BL"				, tbx운송장.Text);

		    return dbp;
		}

		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			string xml = GetTo.Xml(flexPOL.GetChanges().DefaultView.ToTable(false, "NO_PO", "NO_LINE", "DT_EXPECT", "DT_EXDATE", "DC4"));			
			DBMgr.ExecuteNonQuery("PX_CZ_PU_PO_RPT", xml);
			
			ShowMessage(PageResultMode.SaveGood);
		}

		#endregion





	}
}

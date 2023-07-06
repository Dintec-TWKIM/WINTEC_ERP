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
using DX;


namespace cz
{
	public partial class P_CZ_MM_QTIO_RPT : PageBase
	{
		string CompanyCode;
		
		#region ==================================================================================================== Constructor

		public P_CZ_MM_QTIO_RPT()
		{
			InitializeComponent();

			StartUp.Certify(this);
			CompanyCode = LoginInfo.CompanyCode;
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
			dtp조회기간.StartDateToString = DateTime.Now.Year.ToString() + "0101";
			dtp조회기간.EndDateToString = Util.GetToday();

			// 계정구분 기본값
			DataTable dtClass = GetDb.Code("MA_B000010");
			DataRow[] rowClass = dtClass.Select("CODE IN ('009')");	// 재고품, 상품(마스터)
			foreach (DataRow row in rowClass)
				cbm계정구분.AddItem(row["CODE"].ToString(), row["NAME"].ToString());

			// 창고 기본값
			DataTable dtStorage = GetDb.Storage("SL02");
			cbm창고.AddItem(dtStorage.Rows[0]["CD_SL"].ToString(), dtStorage.Rows[0]["NM_SL"].ToString());

			// 검색콤보 
			DataTable dt = new DataTable();
			dt.Columns.Add("TYPE");
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			dt.Rows.Add("재고코드", "" , "유사");
			dt.Rows.Add("재고코드", "X", "엑셀");

			dt.Rows.Add("키워드", "NO_PART"	, "파트번호");
			dt.Rows.Add("키워드", "UCODE"	, "U코드");
			dt.Rows.Add("키워드", "KCODE"	, "K코드");

			cbo재고코드.DataSource = dt.Select("TYPE = '재고코드'").CopyToDataTable();
			cbo재고코드.SelectedIndex = 0;

			cbo키워드.DataSource = dt.Select("TYPE = '키워드'").CopyToDataTable();
			cbo키워드.SelectedIndex = 0;

			grd헤드.DetailGrids = new FlexGrid[] { grd입출고현황, grd출고예약, grd입고예약, grd미입고 };
		}

		private void InitGrid()
		{
			// 바인딩할 창고 정보 (너무 길어서 줄여서 씀)
			DataTable dtSl = GetDb.Storage();
			foreach (DataRow row in dtSl.Rows) row["NM_SL"] = row["NM_SL"].ToString().Split(' ')[0];

			DataTable dtSlFrom = dtSl.Copy();
			DataTable dtSlTo = dtSl.Copy();

			foreach (DataRow row in dtSlFrom.Rows) row["NM_SL"] = row["NM_SL"] + "→";
			foreach (DataRow row in dtSlTo.Rows) row["NM_SL"] = "→" + row["NM_SL"];

			// 기준단가 구분
			DataTable dtBase = new DataTable();
			dtBase.Columns.Add("CODE");
			dtBase.Columns.Add("NAME");
			dtBase.Rows.Add("1", DD("1개월"));
			dtBase.Rows.Add("3", DD("3개월"));
			dtBase.Rows.Add("6", DD("6개월"));
			dtBase.Rows.Add("12", DD("12개월"));
			dtBase.Rows.Add("S", DD("재고"));

			// ********** 헤드
			grd헤드.BeginSetting(2, 1, false);
						
			grd헤드.SetCol("CD_ITEM"		, "재고코드"		, 80);
			grd헤드.SetCol("NM_ITEM"		, "재고명"		, 200);
			grd헤드.SetCol("NO_PART"		, "파트넘버"		, 140);
			grd헤드.SetCol("NO_ITEM"		, "아이템번호"	, false);
			grd헤드.SetCol("UCODE"		, "U코드"		, 100);
			grd헤드.SetCol("KCODE"		, "K코드"		, 100);
			grd헤드.SetCol("DC_OFFER"	, "오퍼"			, 100);
			grd헤드.SetCol("UNIT"		, "단위"			, 45);			
			// 입출고현황
            grd헤드.SetCol("QT_OPEN"		, "기초재고"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
            grd헤드.SetCol("QT_GR"		, "입고"		    , 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
            grd헤드.SetCol("QT_GI"		, "출고"		    , 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
            grd헤드.SetCol("QT_GR_R"		, "입고반품"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
            grd헤드.SetCol("QT_GI_R"		, "출고반품"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("QT_IOSUM"	, "합계"			, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
			// 예약현황
			grd헤드.SetCol("QT_INV"		, "현재고"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("QT_BOOK"		, "출고예약"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
            grd헤드.SetCol("QT_AVST"		, "가용재고"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
            grd헤드.SetCol("QT_NGR"		, "미입고"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
            grd헤드.SetCol("QT_HOLD"		, "입고예약"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd헤드.SetCol("QT_AVGR"		, "가용입고"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
			// 단가
			grd헤드.SetCol("STD_REASON"	, "단가구분"		, 60);
			grd헤드.SetCol("UM_STD"		, "표준단가"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);
			grd헤드.SetCol("UM_STK"		, "재고단가"		, 80	, false	, typeof(decimal), FormatTpType.MONEY);			
			grd헤드.SetCol("AM_STK"		, "재고금액"		, 95	, false	, typeof(decimal), FormatTpType.MONEY);			
			// 비고
			grd헤드.SetCol("DC_RMK_LOG"	, "재고비고"		, 300	, true);

			grd헤드[0, grd헤드.Cols["QT_OPEN"].Index] = "입출고현황";
			grd헤드[0, grd헤드.Cols["QT_GR"].Index] = "입출고현황";
			grd헤드[0, grd헤드.Cols["QT_GI"].Index] = "입출고현황";
			grd헤드[0, grd헤드.Cols["QT_GR_R"].Index] = "입출고현황";
			grd헤드[0, grd헤드.Cols["QT_GI_R"].Index] = "입출고현황";
			grd헤드[0, grd헤드.Cols["QT_IOSUM"].Index] = "입출고현황";

			grd헤드[0, grd헤드.Cols["QT_INV"].Index] = "예약현황";
			grd헤드[0, grd헤드.Cols["QT_BOOK"].Index] = "예약현황";
			grd헤드[0, grd헤드.Cols["QT_AVST"].Index] = "예약현황";
			grd헤드[0, grd헤드.Cols["QT_NGR"].Index] = "예약현황";
			grd헤드[0, grd헤드.Cols["QT_HOLD"].Index] = "예약현황";
			grd헤드[0, grd헤드.Cols["QT_AVGR"].Index] = "예약현황";

			grd헤드[0, grd헤드.Cols["STD_REASON"].Index] = "기준단가";
			grd헤드[0, grd헤드.Cols["UM_STD"].Index] = "기준단가";
			grd헤드[0, grd헤드.Cols["UM_STK"].Index] = "기준단가";
			grd헤드[0, grd헤드.Cols["AM_STK"].Index] = "기준단가";

			grd헤드.Cols["UNIT"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.Cols["STD_REASON"].TextAlign = TextAlignEnum.CenterCenter;
			grd헤드.SetDataMap("UNIT", GetDb.Code("MA_B000004"), "CODE", "NAME");
			grd헤드.SetDataMap("STD_REASON", dtBase, "CODE", "NAME");

			grd헤드.SetDefault("20.11.05.02", SumPositionEnum.Top);
			grd헤드.SetExceptSumCol("UM_STD", "UM_STK");
			grd헤드.SetSumColumnStyle("QT_AVST", "QT_AVGR");

			// ********** 입출고현황
			grd입출고현황.BeginSetting(2, 1, false);
			
			grd입출고현황.SetCol("DT_IO"			, "거래일자"		, 80	, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			grd입출고현황.SetCol("NO_FILE"		, "파일번호"		, 90);
			grd입출고현황.SetCol("QT_OPEN"		, "기초재고"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);

			grd입출고현황.SetCol("CD_QTIOTP_GR"	, "형태"			, 110);
			grd입출고현황.SetCol("QT_GR"			, "수량"			, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd입출고현황.SetCol("UM_GR"			, "단가"			, 75	, false	, typeof(decimal), FormatTpType.MONEY);
			grd입출고현황.SetCol("CD_SL_GR_REF"	, "(창고)"		, 70);
			grd입출고현황.SetCol("CD_SL_GR"		, "창고"			, 70);

			grd입출고현황.SetCol("CD_QTIOTP_GI"	, "형태"			, 110);
			grd입출고현황.SetCol("QT_GI"			, "수량"			, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd입출고현황.SetCol("UM_GI"			, "단가"			, 75	, false	, typeof(decimal), FormatTpType.MONEY);
            grd입출고현황.SetCol("CD_SL_GI"		, "창고"			, 70);
			grd입출고현황.SetCol("CD_SL_GI_REF"	, "(창고)"		, 70);
			grd입출고현황.SetCol("QT_GIR_STOCK"	, "재고"			, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);

			grd입출고현황.SetCol("YN_AM"			, "유상"			, 40	, false	, CheckTypeEnum.Y_N);
			grd입출고현황.SetCol("QT_INV"			, "현재고"		, 57	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd입출고현황.SetCol("NO_MGMT"		, "관리번호"		, 90);
			grd입출고현황.SetCol("NM_EMP"			, "담당자"		, 80);
			grd입출고현황.SetCol("NO_IO"			, "수불번호"		, false);
			grd입출고현황.SetCol("NO_IOLINE"		, "수불항번"		, false);

			grd입출고현황.SetCol("CD_PARTNER"		, "매입/매출코드"	, false);
			grd입출고현황.SetCol("LN_PARTNER"		, "매입/매출처명"	, 150);

			grd입출고현황.SetCol("NM_VESSEL"		, "선명"			, 150);
			grd입출고현황.SetCol("NO_PO_PARTNER"	, "주문번호"		, 150);
			grd입출고현황.SetCol("DT_SO"			, "수주일자"		, 80	, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			grd입출고현황.SetCol("DC_RMK"			, "비고"			, 200);
			grd입출고현황.SetCol("DC1"			, "비고1"		, false);

			grd입출고현황.Cols["NO_FILE"].TextAlign = TextAlignEnum.CenterCenter;
			grd입출고현황.Cols["CD_SL_GR_REF"].TextAlign = TextAlignEnum.CenterCenter;
			grd입출고현황.Cols["CD_SL_GR"].TextAlign = TextAlignEnum.CenterCenter;
			grd입출고현황.Cols["CD_SL_GI"].TextAlign = TextAlignEnum.CenterCenter;
			grd입출고현황.Cols["CD_SL_GI_REF"].TextAlign = TextAlignEnum.CenterCenter;
			grd입출고현황.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
			grd입출고현황.SetDataMap("CD_QTIOTP_GR", GetDb.IOType(), "CD_QTIOTP", "NM_QTIOTP");
			grd입출고현황.SetDataMap("CD_QTIOTP_GI", GetDb.IOType(), "CD_QTIOTP", "NM_QTIOTP");
			grd입출고현황.SetDataMap("CD_SL_GR_REF", dtSlFrom, "CD_SL", "NM_SL");
			grd입출고현황.SetDataMap("CD_SL_GR", dtSl, "CD_SL", "NM_SL");			
			grd입출고현황.SetDataMap("CD_SL_GI", dtSl, "CD_SL", "NM_SL");
			grd입출고현황.SetDataMap("CD_SL_GI_REF", dtSlTo, "CD_SL", "NM_SL");
		
			grd입출고현황[0, grd입출고현황.Cols["CD_QTIOTP_GR"].Index] = "입고";
			grd입출고현황[0, grd입출고현황.Cols["QT_GR"].Index] = "입고";
			grd입출고현황[0, grd입출고현황.Cols["UM_GR"].Index] = "입고";
			grd입출고현황[0, grd입출고현황.Cols["CD_SL_GR_REF"].Index] = "입고";
			grd입출고현황[0, grd입출고현황.Cols["CD_SL_GR"].Index] = "입고";

			grd입출고현황[0, grd입출고현황.Cols["CD_QTIOTP_GI"].Index] = "출고";
			grd입출고현황[0, grd입출고현황.Cols["QT_GI"].Index] = "출고";
			grd입출고현황[0, grd입출고현황.Cols["UM_GI"].Index] = "출고";
            grd입출고현황[0, grd입출고현황.Cols["CD_SL_GI"].Index] = "출고";
			grd입출고현황[0, grd입출고현황.Cols["CD_SL_GI_REF"].Index] = "출고";
			grd입출고현황[0, grd입출고현황.Cols["QT_GIR_STOCK"].Index] = "출고";

			grd입출고현황.SetDefault("21.12.27.01", SumPositionEnum.Top);
			grd입출고현황.SetExceptSumCol("UM_GR", "UM_GI");

			// 컬럼 숨기기 했던것이 저장되어 있기때문에 모든 컬럼 Visible 처리 해줌
			grd입출고현황.Cols["QT_OPEN"].Visible = true;

			grd입출고현황.Cols["CD_QTIOTP_GR"].Visible = true;
			grd입출고현황.Cols["QT_GR"].Visible = true;
			grd입출고현황.Cols["UM_GR"].Visible = true;
			grd입출고현황.Cols["CD_SL_GR_REF"].Visible = false;	// 기본 숨김 처리
			grd입출고현황.Cols["CD_SL_GR"].Visible = true;

			grd입출고현황.Cols["CD_QTIOTP_GI"].Visible = true;
			grd입출고현황.Cols["QT_GI"].Visible = true;
			grd입출고현황.Cols["UM_GI"].Visible = true;
            grd입출고현황.Cols["CD_SL_GI"].Visible = true;
			grd입출고현황.Cols["CD_SL_GI_REF"].Visible = false;	// 기본 숨김 처리
			grd입출고현황.Cols["QT_GIR_STOCK"].Visible = true;

			// ********** 출고예약
			grd출고예약.BeginSetting(1, 1, false);
                
			grd출고예약.SetCol("NO_SO"			, "수주번호"		, 90);
			grd출고예약.SetCol("NO_LINE"			, "항번"  		, false);
            grd출고예약.SetCol("NO_DSP"			, "순번"			, 40);
			grd출고예약.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd출고예약.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			grd출고예약.SetCol("CD_ITEM"			, "재고코드"		, false);
			grd출고예약.SetCol("LN_PARTNER"		, "매출처"		, 180);
			grd출고예약.SetCol("NM_VESSEL"		, "선명"			, 180);
			grd출고예약.SetCol("NO_PO_PARTNER"	, "주문번호"		, 120);

			grd출고예약.SetCol("NM_EMP"	    	, "담당자"		, 80);
			grd출고예약.SetCol("DT_SO"			, "수주일"		, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd출고예약.SetCol("DT_DUEDATE"		, "납기일"		, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd출고예약.SetCol("NM_EMP_BOOK"		, "예약자"		, 80);
			grd출고예약.SetCol("DT_BOOK"			, "예약일"		, 140	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd출고예약.SetCol("QT_SO"			, "수주수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd출고예약.SetCol("QT_BOOK"			, "예약수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			
			grd출고예약.SetCol("NO_GIR"			, "송품협조전"	, 90);
			grd출고예약.SetCol("NO_PACK"			, "포장협조전"	, 90);

			grd출고예약.Cols["NO_SO"].TextAlign = TextAlignEnum.CenterCenter;
			grd출고예약.Cols["NO_DSP"].Format = "####.##";
			grd출고예약.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd출고예약.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
            grd출고예약.Cols["NM_EMP_BOOK"].TextAlign = TextAlignEnum.CenterCenter;
			grd출고예약.Cols["DT_BOOK"].Format = "####/##/## ##:##:##";
			grd출고예약.Cols["NO_GIR"].TextAlign = TextAlignEnum.CenterCenter;
			grd출고예약.Cols["NO_PACK"].TextAlign = TextAlignEnum.CenterCenter;

			grd출고예약.SetDefault("19.04.15.01", SumPositionEnum.Top);

			// ********** 입고예약
			grd입고예약.BeginSetting(1, 1, false);
				
			grd입고예약.SetCol("NO_SO"			, "수주번호"		, 90);
			grd입고예약.SetCol("NO_LINE"			, "항번"		    , false);
            grd입고예약.SetCol("NO_DSP"			, "순번"			, 40);
			grd입고예약.SetCol("CD_ITEM_PARTNER"	, "품목코드"		, 120);
			grd입고예약.SetCol("NM_ITEM_PARTNER"	, "품목명"		, 200);
			grd입고예약.SetCol("CD_ITEM"			, "재고코드"		, false);
			grd입고예약.SetCol("LN_PARTNER"		, "매출처"		, 180);
			grd입고예약.SetCol("NM_VESSEL"		, "선명"			, 180);
			grd입고예약.SetCol("NO_PO_PARTNER"	, "주문번호"		, 120);
	
			grd입고예약.SetCol("NM_EMP"			, "담당자"		, 80);
           	grd입고예약.SetCol("DT_SO"			, "수주일"		, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd입고예약.SetCol("DT_DUEDATE"		, "납기일"		, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd입고예약.SetCol("NM_EMP_HOLD"		, "예약자"		, 80);
			grd입고예약.SetCol("DT_HOLD"			, "예약일"		, 140	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd입고예약.SetCol("QT_SO"			, "수주수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd입고예약.SetCol("QT_HOLD"			, "예약수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);

			grd입고예약.Cols["NO_SO"].TextAlign = TextAlignEnum.CenterCenter;
			grd입고예약.Cols["NO_DSP"].Format = "####.##";
			grd입고예약.Cols["NO_DSP"].TextAlign = TextAlignEnum.CenterCenter;
			grd입고예약.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;
            grd입고예약.Cols["NM_EMP_HOLD"].TextAlign = TextAlignEnum.CenterCenter;
			grd입고예약.Cols["DT_HOLD"].Format = "####/##/## ##:##:##";

			grd입고예약.SetDefault("19.04.15.01", SumPositionEnum.Top);

			// ********** 미입고
			grd미입고.BeginSetting(1, 1, false);
			    
			grd미입고.SetCol("NO_PO"			, "발주번호"		, 90);
			grd미입고.SetCol("NO_LINE"		, "순번"			, 40);
			grd미입고.SetCol("LN_PARTNER"	, "매입처"		, 180);
			grd미입고.SetCol("NO_ORDER"		, "주문번호"		, 120);
			grd미입고.SetCol("NM_EMP"		, "담당자"		, 80);

			grd미입고.SetCol("DT_LIMIT"		, "납기예정일"	, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd미입고.SetCol("DT_AVERAGE"	, "기대납기일"	, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd미입고.SetCol("WEIGHTED_MEAN"	, "평균LT"		, 58);
			grd미입고.SetCol("STD_DEVIATION"	, "표준편차"		, 58);
			grd미입고.SetCol("DT_EXPECT"		, "확약일"		, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);
			grd미입고.SetCol("DT_EXDATE"		, "반출일"		, 80	, false	, typeof(string) , FormatTpType.YEAR_MONTH_DAY);

			grd미입고.SetCol("QT_PO"			, "발주수량"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd미입고.SetCol("QT_NONGR"		, "미입고"		, 60	, false	, typeof(decimal), FormatTpType.QUANTITY);
			grd미입고.SetCol("UM_EX"			, "매입단가"		, 100	, false	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd미입고.SetCol("DC1"			, "비고1"		, 250);
			grd미입고.SetCol("DC2"			, "비고2"		, 250);

			grd미입고.Cols["NO_PO"].TextAlign = TextAlignEnum.CenterCenter;
			grd미입고.Cols["NO_LINE"].Format = "####.##";
			grd미입고.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
			grd미입고.Cols["NM_EMP"].TextAlign = TextAlignEnum.CenterCenter;

			grd미입고.SetDefault("22.01.17.01", SumPositionEnum.Top);

			// 컬럼 스타일 설정
            SetGrid.ColumnStyle(grd헤드, grd헤드.Cols["QT_IOSUM"].Index, GridStyle.Bold);
			SetGrid.ColumnStyle(grd헤드, grd헤드.Cols["QT_INV"].Index, GridStyle.Bold);
			SetGrid.ColumnStyle(grd헤드, grd헤드.Cols["QT_AVST"].Index, GridStyle.Bold, GridStyle.FontBlue);
			SetGrid.ColumnStyle(grd헤드, grd헤드.Cols["QT_AVGR"].Index, GridStyle.Bold, GridStyle.FontBlue);
			SetGrid.ColumnStyle(grd입출고현황, grd입출고현황.Cols["QT_OPEN"].Index, GridStyle.Bold, GridStyle.FontBlue);
			SetGrid.ColumnStyle(grd입출고현황, grd입출고현황.Cols["QT_GR"].Index, GridStyle.Bold, GridStyle.FontBlue);
			SetGrid.ColumnStyle(grd입출고현황, grd입출고현황.Cols["QT_GI"].Index, GridStyle.Bold, GridStyle.FontRed);
			SetGrid.ColumnStyle(grd입출고현황, grd입출고현황.Cols["QT_INV"].Index, GridStyle.Bold, GridStyle.FontGreen);
		}

		protected override void InitPaint()
		{
			
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			cbm계정구분.QueryBefore += new BpQueryHandler(cbm계정구분_QueryBefore);
			cbm창고.QueryBefore += new BpQueryHandler(cmb창고_QueryBefore);
			cbo재고코드.SelectionChangeCommitted += new EventHandler(cbo재고코드_SelectionChangeCommitted);

			tbx재고코드.KeyDown += new KeyEventHandler(tbx검색_KeyDown);
			tbx재고명.KeyDown += new KeyEventHandler(tbx검색_KeyDown);
			tbx키워드.KeyDown += new KeyEventHandler(tbx검색_KeyDown);

			grd헤드.AfterRowChange += new RangeEventHandler(grd헤드_AfterRowChange);
			grd헤드.AfterEdit += Grd헤드_AfterEdit;
		}

		private void Grd헤드_AfterEdit(object sender, RowColEventArgs e)
		{			
			string colName = grd헤드.Cols[e.Col].Name;

			if (colName == "DC_RMK_LOG")
			{
				string query = @"
IF EXISTS (SELECT 1 FROM CZ_MA_PITEM WHERE CD_COMPANY = @CD_COMPANY AND CD_ITEM = @CD_ITEM)
BEGIN
	UPDATE CZ_MA_PITEM SET
		DC_RMK_LOG	= @DC_RMK_LOG 
	,	ID_UPDATE	= @ID_USER
	,	DTS_UPDATE	= NEOE.NOW()
	WHERE 1 = 1
		AND CD_COMPANY = @CD_COMPANY
		AND CD_ITEM = @CD_ITEM
END
ELSE
BEGIN
	INSERT INTO CZ_MA_PITEM
	(
		CD_COMPANY
	,	CD_PLANT
	,	CD_ITEM
	,	DC_RMK_LOG
	,	ID_INSERT
	,	DTS_INSERT
	)
	VALUES
	(
		@CD_COMPANY
	,	@CD_PLANT
	,	@CD_ITEM
	,	@DC_RMK_LOG
	,	@ID_USER
	,	NEOE.NOW()
	)
END";
				SQL sql = new SQL(query, SQLType.Text, SQLDebug.None);
				sql.Parameter.Add2("@CD_COMPANY"	, LoginInfo.CompanyCode);
				sql.Parameter.Add2("@CD_PLANT"		, LoginInfo.CdPlant);
				sql.Parameter.Add2("@CD_ITEM"		, grd헤드["CD_ITEM"]);
				sql.Parameter.Add2("@DC_RMK_LOG"	, grd헤드[colName]);
				sql.Parameter.Add2("@ID_USER"		, LoginInfo.UserID);

				//if (grd헤드[colName].ToString() == "")
				//	sql.Parameter.Add2("@DC_RMK_LOG", SQLEmpty.Value);
				//else
				//	sql.Parameter.Add2("@DC_RMK_LOG", grd헤드[colName]);

				sql.ExecuteNonQuery();

				
			}
		}
		
		private void cbm계정구분_QueryBefore(object sender, BpQueryArgs e)
		{			
			e.HelpParam.P00_CHILD_MODE = "계정구분";

			e.HelpParam.P61_CODE1 = @"
	  CD_SYSDEF AS CODE
	, NM_SYSDEF AS NAME";

			e.HelpParam.P62_CODE2 = @"
MA_CODEDTL WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = @"
WHERE 1 = 1
	AND CD_COMPANY = '" + CompanyCode + @"'
	AND CD_FIELD = 'MA_B000010'
	AND USE_YN = 'Y'
ORDER BY CD_SYSDEF";
		}

		private void cmb창고_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P09_CD_PLANT = LoginInfo.CdPlant;
		}

		private void cbo재고코드_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (GetCon.Value(cbo재고코드) == "X")
			{
				OpenFileDialog f = new OpenFileDialog();
				f.Filter = DD("엑셀 파일") + "|*.xls;*.xlsx";

				if (f.ShowDialog() != DialogResult.OK)
				{
					cbo재고코드.SelectedIndex = 0;
					return;
				}

				ExcelReader excel = new ExcelReader();
				DataTable dtE =  excel.Read(f.FileName);

				if (dtE.Rows.Count == 0)
				{
					ShowMessage("엑셀파일을 읽을 수 없습니다.");
					cbo재고코드.SelectedIndex = 0;
					return;
				}
				
				string CD_ITEM_X = "";
				foreach (DataRow row in dtE.Rows) CD_ITEM_X += ",'" + row[0] + "'";
				CD_ITEM_X = CD_ITEM_X.Substring(1);

				// 조회
				SQL sql = new SQL("PS_CZ_MM_QTIO_RPT_H_R5", SQLType.Procedure, SQLDebug.Print);
				sql.Parameter.Add2("@CD_COMPANY", CompanyCode);
				sql.Parameter.Add2("@DT_F"		, dtp조회기간.StartDateToString);
				sql.Parameter.Add2("@DT_T"		, dtp조회기간.EndDateToString);
				sql.Parameter.Add2("@CD_SL"		, cbm창고.QueryWhereIn_WithValueMember);
				sql.Parameter.Add2("@CD_ITEM_X"	, CD_ITEM_X);
				sql.Parameter.Add2("@YN_ZERO"	, chk재고수량.Checked ? "Y" : "");

				DataTable dtHead = sql.GetDataTable();
				DataTable dtHeadSt = GetDb.JoinStockQuantityR3(CompanyCode, dtHead);

				// 재고금액 계산
				dtHeadSt.Columns.Add("AM_STK", typeof(decimal), "QT_INV * UM_STK");
				grd헤드.Binding = dtHeadSt;
				cbo재고코드.SelectedIndex = 0;
			}
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

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			Util.ShowProgress(DD("조회중입니다."));
			tbx재고코드.Text = EngHanConverter.KorToEng(tbx재고코드.Text);

			string useYn = "";
			if (rdo사용유무Y.Checked) useYn = "Y";
			if (rdo사용유무N.Checked) useYn = "N";

			// 키워드 파라메타
			string keyPar = "@" + cbo키워드.GetValue();

			DBMgr dbm = new DBMgr();
			dbm.DebugMode = DebugMode.Print;
			dbm.Procedure = "PS_CZ_MM_QTIO_RPT_H_R5";
			dbm.AddParameter("@CD_COMPANY"	, CompanyCode);
			dbm.AddParameter("@CLS_ITEM"	, cbm계정구분.QueryWhereIn_WithValueMember);
			dbm.AddParameter("@DT_F"		, dtp조회기간.StartDateToString);
			dbm.AddParameter("@DT_T"		, dtp조회기간.EndDateToString);
			dbm.AddParameter("@GRP_ITEM"	, cbm품목군.QueryWhereIn_WithValueMember);
			dbm.AddParameter("@CD_SL"		, cbm창고.QueryWhereIn_WithValueMember);
			dbm.AddParameter("@CD_ITEM"		, tbx재고코드.Text);
			dbm.AddParameter("@NM_ITEM"		, tbx재고명.Text);
			dbm.AddParameter(keyPar			, tbx키워드.Text);
			dbm.AddParameter("@YN_USE"		, useYn);
			dbm.AddParameter("@YN_ZERO"		, chk재고수량.Checked ? "Y" : "");

			DataTable dtHead = dbm.GetDataTable();
			DataTable dtHeadSt = GetDb.JoinStockQuantityR3(CompanyCode, dtHead);
			
			// 재고금액 계산
			dtHeadSt.Columns.Add("AM_STK", typeof(decimal), "QT_INV * UM_STK");
			grd헤드.Binding = dtHeadSt;
			Util.CloseProgress();

			#region 컬럼 보이기 / 숨기기

			// 모든 컬럼 보였다가 불필요한 컬럼만 숨기기
			grd입출고현황.Cols["QT_OPEN"].Visible = true;

			grd입출고현황.Cols["CD_QTIOTP_GR"].Visible = true;
			grd입출고현황.Cols["QT_GR"].Visible = true;
			grd입출고현황.Cols["UM_GR"].Visible = true;
			grd입출고현황.Cols["CD_SL_GR"].Visible = true;

			grd입출고현황.Cols["CD_QTIOTP_GI"].Visible = true;
			grd입출고현황.Cols["QT_GI"].Visible = true;
			grd입출고현황.Cols["UM_GI"].Visible = true;
			grd입출고현황.Cols["CD_SL_GI"].Visible = true;
			grd입출고현황.Cols["QT_GIR_STOCK"].Visible = true;

			// 입고선택 → 출고컬럼 숨기기
			if (rdo수불유형1.Checked)
			{
				grd입출고현황.Cols["QT_OPEN"].Visible = false;

				grd입출고현황.Cols["CD_QTIOTP_GI"].Visible = false;
				grd입출고현황.Cols["QT_GI"].Visible = false;
				grd입출고현황.Cols["UM_GI"].Visible = false;				
				grd입출고현황.Cols["CD_SL_GI"].Visible = false;
				grd입출고현황.Cols["CD_SL_GI_REF"].Visible = false;
				grd입출고현황.Cols["QT_GIR_STOCK"].Visible = false;
			}

			// 출고선택 → 입고컬럼 숨기기
			if (rdo수불유형2.Checked)
			{
				grd입출고현황.Cols["QT_OPEN"].Visible = false;

				grd입출고현황.Cols["CD_QTIOTP_GR"].Visible = false;
				grd입출고현황.Cols["QT_GR"].Visible = false;
				grd입출고현황.Cols["UM_GR"].Visible = false;
				grd입출고현황.Cols["CD_SL_GR_REF"].Visible = false;
				grd입출고현황.Cols["CD_SL_GR"].Visible = false;				
			}

			#endregion
		}

		private void grd헤드_AfterRowChange(object sender, RangeEventArgs e)
        {
			string itemCode = grd헤드["CD_ITEM"].ToString();
			decimal openQty = GetTo.Decimal(grd헤드["QT_OPEN"]);
			string filter = "CD_ITEM = '" + itemCode + "'";

			if (grd헤드.DetailQueryNeed)
			{
				// ********** 입출고현황
				string ioType = "";
				if (rdo수불유형1.Checked) ioType = "1";
				if (rdo수불유형2.Checked) ioType = "2";

				DBMgr dbm = new DBMgr();
				dbm.DebugMode = DebugMode.Print;
				dbm.Procedure = "PS_CZ_MM_QTIO_RPT_L_4";
				dbm.AddParameter("@CD_COMPANY"	, CompanyCode);
				dbm.AddParameter("@CD_ITEM"		, itemCode);
				dbm.AddParameter("@QT_OPEN"		, openQty);
				dbm.AddParameter("@DT_F"		, dtp조회기간.StartDateToString);
				dbm.AddParameter("@DT_T"		, dtp조회기간.EndDateToString);
				dbm.AddParameter("@CD_SL"		, cbm창고.QueryWhereIn_WithValueMember);
				dbm.AddParameter("@FG_PS"		, ioType);
				DataTable dt입출고현황 = dbm.GetDataTable();

				// 권한 설정
				//if (!AUTH.IsAdmin())
				//{
				//	foreach (DataRow row in dt입출고현황.Select("CD_QTIOTP_GR IN ('160')"))
				//	{
				//		row["CD_PARTNER"] = "";
				//		row["LN_PARTNER"] = "";
				//	}
				//}

				if (!권한.재고입출고현황_매입처())
				{
					foreach (DataRow row in dt입출고현황.Select("CD_QTIOTP_GR IN ('160')"))
					{
						row["CD_PARTNER"] = "";
						row["LN_PARTNER"] = "";
					}
				}

				// 기초재고 추가
				dt입출고현황.Columns.Add("QT_OPEN", typeof(decimal));
				dt입출고현황.Rows.InsertAt(dt입출고현황.NewRow(), 0);
				dt입출고현황.Rows[0]["CD_ITEM"] = itemCode;
				dt입출고현황.Rows[0]["QT_OPEN"] = openQty;
				dt입출고현황.Rows[0]["QT_INV"] = openQty;
				dt입출고현황.Rows[0]["RN_DATE"] = "000000";

				grd입출고현황.BindingAdd(dt입출고현황, filter);

				// 합계행 현재고 수량 변경
				int col = grd입출고현황.Cols["QT_INV"].Index;				
				grd입출고현황[grd입출고현황.Rows.Fixed - 1, col] = grd입출고현황.GetDataDisplay(grd입출고현황.Rows.Count - 1, col);

				// ********** 기타
				DataTable dtBook = DBMgr.GetDataTable("PS_CZ_MM_QTIO_RPT_L_BOOK", CompanyCode, itemCode);
				DataTable dtHold = DBMgr.GetDataTable("PS_CZ_MM_QTIO_RPT_L_HOLD", CompanyCode, itemCode);
				DataTable dtNgr = DBMgr.GetDataTable("PS_CZ_MM_QTIO_RPT_L_NGR", CompanyCode, itemCode);

				// ********** 바인딩
				grd출고예약.BindingAdd(dtBook, filter);
				grd입고예약.BindingAdd(dtHold, filter);
				grd미입고.BindingAdd(dtNgr, filter);
			}
			else
			{
				grd입출고현황.BindingAdd(null, filter);
				
				grd출고예약.BindingAdd(null, filter);
				grd입고예약.BindingAdd(null, filter);
				grd미입고.BindingAdd(null, filter);				
			}
		}

		#endregion
	}
}

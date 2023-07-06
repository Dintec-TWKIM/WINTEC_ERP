using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Duzon.Common.Forms;
using Duzon.Common.BpControls;

using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;

using DX;
using System.Net;
using System.IO;
using System.Diagnostics;
using Duzon.Common.Controls;
using System.Text.RegularExpressions;

namespace cz
{
	public partial class P_CZ_PU_PO_REG : PageBase
	{
		string WorkMessage = "작업 진행 중입니다. \r\n잠시만 기다려주세요!";
		string 파일번호링크 = "";
		bool 신규 = false;

		#region ===================================================================================================== Property
	
		private string 파일번호
		{
			get
			{
				return tbx파일번호.Text;
			}
			set
			{
				tbx파일번호.Text = value;
			}
		}

		private string 회사코드 => LoginInfo.CompanyCode;

		private string 발주번호 => grd헤드["NO_PO"].String();

		private string 매입처코드 => grd헤드["CD_PARTNER"].String();

		public string 통화 => cbo통화.값();

		private decimal 환율 => cur환율.DecimalValue;

		private decimal 과세율 => cbo과세구분.데이터행()["CD_FLAG1"].ToDecimal();

		private int 표시형식 => cbo표시형식.데이터행()["CD_FLAG1"].ToInt();
		
		private int 원화소수자리 => 회사코드.StartsWith("K") ? 0 : 2;

		#endregion

		#region ==================================================================================================== Constructor

		public P_CZ_PU_PO_REG()
		{
			StartUp.Certify(this);
			InitializeComponent();
		}

		public P_CZ_PU_PO_REG(string 파일번호, bool 신규)
		{
			StartUp.Certify(this);
			InitializeComponent();

			this.파일번호링크 = 파일번호;
			this.신규 = 신규;
		}

		#endregion

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			this.페이지초기화();
			tbx파일번호.자동영문();
			tbx파일번호.엔터검색();

			// ********** 편집불가 패널
			pnl매출처.사용(false);
			pnl호선번호.사용(false);
			pnl선명.사용(false);
			pnl발주번호.사용(false);
			cur환율.사용(false);
			pnl지급조건.사용(false);  // 반드시 죽여야함, 사고침, 유령업체에 선지급함

			// ********** 콤보박스
			DataSet ds = 코드.코드관리("MA_B000005", "PU_C000014", "TR_IM00003", "TR_IM00011", "MA_B000046", "CZ_SA00014", "CZ_PU00012", "CZ_PU00013");

			cbo통화.바인딩(ds.Tables[0], false);
			cbo지급조건.바인딩(ds.Tables[1], true);
			cbo선적조건.바인딩(ds.Tables[2], true);
			cbo포장형태.바인딩(ds.Tables[3], true);
			cbo과세구분.바인딩(ds.Tables[4], true);
			cbo표시형식.바인딩(ds.Tables[5], false);
			cbo부대비용.바인딩(코드.매입부대비용(), true);
			
			cbo매입처담당자.ValueMember = "SEQ";
			cbo매입처담당자.DisplayMember = "NM_PTR";

			// 미입고 관리
			cbo납품장소.바인딩(ds.Tables[6], true);
			cbo납품시간.바인딩(ds.Tables[7], true);

			// ********** 그리드
			MainGrids = new FlexGrid[] { grd헤드, grd라인 };
			grd헤드.DetailGrids = new FlexGrid[] { grd라인 };

			InitGrid();
			InitEvent();
		}

		protected override void InitPaint()
		{
			spc헤드.SplitterDistance = 850;

			// 일반모드
			if (파일번호링크 == "")
			{
				// 기본값 설정
				//dtp발주일자.초기화(true);
				//ctx담당자.초기화(true);
				//ctx구매그룹.초기화(true);
			}
			// 링크모드
			else
			{
				파일번호 = 파일번호링크;
				OnToolBarSearchButtonClicked(null, null);
			}

			if (!Certify.IsLive())
			{
				grd헤드.Cols.Remove("FILE_ICON");
				grd헤드.Cols.Remove("NM_FILE");
			}

			spc헤드.Panel2MinSize = 880;
			spc헤드.SplitterDistance = spc헤드.Width - (spc헤드.Panel2MinSize + 10);
		}


		private void Clear()
		{
			// 그리드 (그리드부터 해야 데이터가 다 날라감)
			grd헤드.초기화();
			grd라인.초기화();
			
			// 수주정보			
			tbx파일번호.초기화();
			ctx매출처.초기화(false);
			ctx호선.초기화(false);
			tbx선명.초기화();

			// 발주정보
			tbx발주번호.초기화();
			dtp발주일자.초기화(false);
			ctx담당자.초기화(false);
			ctx구매그룹.초기화(false);
			ctx매입처.초기화();
			cbo매입처담당자.초기화();
			tbx주문번호.초기화();
			cbo통화.초기화(0);
			Cbo통화_SelectionChangeCommitted(null, null);
			ctx발주형태.초기화();
			tbx딜러PO.초기화();
			cbo지급조건.초기화(0);
			tbx인도기간.초기화();
			tbxBL번호.초기화();
			cbo선적조건.초기화(0);
			tbx선적조건.초기화();
			cbo포장형태.초기화(0);
			cbo과세구분.초기화(0);
			cur할인.초기화();
			cur납기.초기화();

			cbo부대비용.초기화(0);
			ctx대행업체.초기화();
			tbx비고.초기화();
			
			// 기타
			신규 = false;
			tbx파일번호.Focus();
			pnl파일번호.사용(true);
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			// ********** 헤드
			grd헤드.세팅시작(1);

			grd헤드.컬럼세팅("CD_PARTNER"	, "매입처코드", false);
			grd헤드.컬럼세팅("LN_PARTNER"	, "매입처"	, 200);
			grd헤드.컬럼세팅("CD_EXCH"	, "통화"		, 45	, 정렬.가운데);
			grd헤드.컬럼세팅("AM_EX"		, "외화금액"	, 85	, 포맷.외화단가);
			grd헤드.컬럼세팅("AM"			, "원화금액"	, 85	, 포맷.원화단가);
			grd헤드.컬럼세팅("VAT"		, "부가세"	, 85	, 포맷.원화단가);
			grd헤드.컬럼세팅("FILE_ICON"	, "첨부"		, 40	, 정렬.가운데);
			grd헤드.컬럼세팅("DT_SEND"	, "발송일"	, 140	, 포맷.날짜시간);

			grd헤드.컬럼세팅("NO_SO"		, "수주번호"	, false);
			grd헤드.컬럼세팅("NO_PO"		, "발주번호"	, false);
			grd헤드.컬럼세팅("DT_PO"		, "발주일자"	, false);
			grd헤드.컬럼세팅("NO_EMP"		, "담당자"	, false);
			grd헤드.컬럼세팅("CD_PURGRP"	, "구매그룹"	, false);
			grd헤드.컬럼세팅("NO_ORDER"	, "주문번호"	, false);
			grd헤드.컬럼세팅("CD_TPPO"	, "발주형태"	, false);
			grd헤드.컬럼세팅("FG_PAYMENT"	, "지급조건"	, false);
			grd헤드.컬럼세팅("TP_PACKING"	, "포장형태"	, false);
			grd헤드.컬럼세팅("FG_TAX"		, "매입과세"	, false);
			grd헤드.컬럼세팅("NM_FILE"	, "첨부파일"	, false);

			grd헤드.데이터맵("CD_EXCH", 코드.통화());
			grd헤드.더미컬럼("AM_EX", "AM", "VAT");		

			grd헤드.패널바인딩(lay헤드);
			grd헤드.패널바인딩(lay미입고관리);
			grd헤드.필수값("NO_PO", "DT_PO", "NO_EMP", "CD_PURGRP", "LN_PARTNER", "NO_ORDER", "CD_EXCH", "CD_TPPO", "FG_PAYMENT", "TP_PACKING", "FG_TAX");
			grd헤드.세팅종료("22.07.12.02", true);
			
			// ********** 라인
			grd라인.BeginSetting(2, 1, false);

			grd라인.SetCol("NO_PO"			, "발주번호"					, false);
			grd라인.SetCol("NO_LINE"			, "항번"						, false);
			grd라인.SetCol("NO_DSP"			, "순번"						, 40	, typeof(decimal), "####.##", TextAlignEnum.CenterCenter);
			grd라인.SetCol("NM_SUBJECT"		, "주제"						, false);
			grd라인.SetCol("CD_ITEM_PARTNER"	, "품목코드"					, 120);
			grd라인.SetCol("NM_ITEM_PARTNER"	, "품목명"					, 300);
			grd라인.SetCol("CD_ITEM"			, "재고코드"					, 100);
			grd라인.SetCol("CD_UNIT_MM"		, "단위"						, 45	, TextAlignEnum.CenterCenter);
			grd라인.SetCol("QT_PO"			, "수량"						, 50	, 6		, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("QT_RAW"			, "수량RAW"					, false);
			grd라인.SetCol("QT_RCV"			, "(입고)"					, 50	, 6		, typeof(decimal), FormatTpType.QUANTITY);
			grd라인.SetCol("UM_EX_E"			, "매입견적단가"	, "외화단가"	, 90	, 11	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("AM_EX_E"			, "매입견적단가"	, "외화금액"	, 90	, 11	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_KR_E"			, "매입견적단가"	, "원화단가"	, 90	, 11	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("AM_KR_E"			, "매입견적단가"	, "원화금액"	, 90	, 11	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("RT_DC"			, "DC\n(%)"					, 55	, 4		, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM_EX"			, "매입단가"		, "외화단가"	, 90	, 11	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("AM_EX"			, "매입단가"		, "외화금액"	, 90	, 11	, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			grd라인.SetCol("UM"				, "매입단가"		, "원화단가"	, 90	, 11	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("AM"				, "매입단가"		, "원화금액"	, 90	, 11	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("VAT"				, "매입단가"		, "부가세"	, 90	, 11	, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("LT"				, "납기"						, 50	, 3		, typeof(decimal), FormatTpType.MONEY);
			grd라인.SetCol("DC_RMK"			, "비고"						, 200);
			grd라인.SetCol("YN_BL"			, "BL"						, 30	, CheckTypeEnum.Y_N);
			grd라인.SetCol("NO_SO"			, "수주번호"					, false);
			grd라인.SetCol("NO_SOLINE"		, "수주항번"					, false);

			grd라인.SetDataMap("CD_UNIT_MM", CODE.코드관리("MA_B000004"));

			grd라인.SetDefault("21.11.15.01", SumPositionEnum.Top);
			grd라인.SetAlternateRow();
			grd라인.SetMalgunGothic();
			grd라인.SetEditColumn("QT_PO", "UM_EX_E", "RT_DC", "UM_EX", "LT", "YN_BL");
			grd라인.SetExceptSumCol("NO_DSP", "UM_EX_E", "UM_KR_E", "RT_DC", "UM_EX", "UM", "LT");
			grd라인.SetSumColumnStyle("AM_EX_E", "AM_EX", "AM");
		}

		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			btn메일발송.Click += Btn메일발송_Click;
			btn일괄변환.Click += Btn일괄변환_Click;
			btn매입처추가.Click += Btn매입처추가_Click;
			btn매입처삭제.Click += Btn매입처삭제_Click;
			btn아이템추가.Click += Btn아이템추가_Click;
			btn아이템삭제.Click += Btn아이템삭제_Click;

			ctx담당자.QueryAfter += Ctx담당자_QueryAfter;
			ctx매입처.QueryAfter += Ctx매입처_QueryAfter;
			cbo매입처담당자.SelectionChangeCommitted += Cbo매입처담당자_SelectionChangeCommitted;
			ctx발주형태.QueryBefore += Ctx발주형태_QueryBefore;

			cbo통화.SelectionChangeCommitted += Cbo통화_SelectionChangeCommitted;
			cbo과세구분.SelectionChangeCommitted += Cbo과세구분_SelectionChangeCommitted;

			btn할인.Click += Btn할인_Click;
			btn납기.Click += Btn납기_Click;
			btn표시형식.Click += Btn표시형식_Click;
			
			btn납기전체.Click += Btn납기전체_Click;
			btn인도기간전체.Click += Btn인도기간전체_Click;
			btn부대비용.Click += Btn부대비용_Click;

			btn주문번호.Click += Btn주문번호_Click;
			btn딜러PO.Click += Btn딜러PO_Click;
			btnBL번호.Click += BtnBL번호_Click;
			btn미입고.Click += Btn미입고_Click;

			grd헤드.AfterRowChange += Grd헤드_AfterRowChange;
			grd헤드.DoubleClick += Grd헤드_DoubleClick;
			grd라인.KeyDown += Grd라인_KeyDown;
			grd라인.AfterEdit += Grd라인_AfterEdit;

			btn테스트.Click += Btn테스트_Click;


			//btn부강
			btn부강유통.Click += Btn부강유통_Click;
		}

		private void Btn부강유통_Click(object sender, EventArgs e)
		{
			엑셀 엑셀 = new 엑셀();
			

			if (엑셀.파일선택())
			{
				try
				{
					// 파일번호가 일치하는지 확인
					string 파일번호 = Regex.Match(엑셀.파일이름, @"(SB|NS)\d{8}").Value;

					if (파일번호 == "")						메시지.경고발생("잘못된 엑셀파일입니다.");
					if (this.파일번호 != 파일번호)				메시지.경고발생("파일번호가 일치하지 않습니다.");
					if (!엑셀.파일이름.발생("부강", "팬월드"))	메시지.경고발생("잘못된 엑셀파일입니다.");

					메시지.작업중("작업중입니다.");

					// ********** 헤더 행 찾기 (양식마다 헤더행 위치가 다름)
					엑셀.헤더행 = 0;
					엑셀.읽기();

					for (int i = 0; i < 엑셀.데이터테이블.Rows.Count; i++)
					{
						if (엑셀.데이터테이블.Rows[i][0].문자() == "NO")
						{
							엑셀.헤더행 = i + 1;
							break;
						}
					}

					// ********** 작업시작
					엑셀.읽기();	// 다시 읽기
					엑셀.데이터테이블.삭제("ISNULL(NO, '') = ''");    // 쓸데없는 행 삭제

					// NO컬럼에서 .빼기
					foreach (DataRow row in 엑셀.데이터테이블.Rows) row["NO"] = row["NO"].문자().Replace(".", "");

					// 팬월드의 경우 영세 컬럼이 없으므로 추가함
					if (!엑셀.데이터테이블.Columns.Contains("영세"))
						엑셀.데이터테이블.컬럼추가("영세");
					
					// 우리 DB 기준으로 컬럼이름 변경 (컬럼이름의 특수문자들이 JSON으로 인식이 안됨)
					엑셀.데이터테이블.Columns["NO"].ColumnName = "NO_DSP";
					엑셀.데이터테이블.Columns["DESCRIPTION"].ColumnName = "NM_ITEM";
					엑셀.데이터테이블.Columns["AMOUNT"].ColumnName = "AM";
					엑셀.데이터테이블.Columns["영세"].ColumnName = "ZERO_TAX";
					엑셀.데이터테이블.컬럼추가("CODE", typeof(string), "");

					// 아이템 루프 돌면서 처리해야 할 것들
					int 부대비용인덱스 = 0;

					foreach (DataRow 행 in 엑셀.데이터테이블.Rows)
					{
						// 금액이 C/S라고 되있는건 0원 처리함(반품) => 무상처리
						if (행["AM"].문자().대문자() == "C/S")
						{
							행["CODE"] = "RETURN";
							행["AM"] = 0;
						}

						// 부대비용 처리
						if (행["NO_DSP"].문자().포함("*", "+"))
						{
							행["CODE"] = "CHARGE";
							행["NO_DSP"] = 90001 + 부대비용인덱스++;							
						}
					}

					// 변환 시작
					디비 db = new 디비("PX_CZ_PU_PO_CHK_BUKANG");
					db.변수.추가("@CD_COMPANY"	, 상수.회사코드);
					db.변수.추가("@NO_FILE"		, 파일번호);
					db.변수.추가("@CD_PARTNER"	, 엑셀.파일이름.발생("부강") ? "11698" : "12928");
					db.변수.추가("@JSON_L"		, 엑셀.데이터테이블.Json("NO_DSP", "NM_ITEM", "AM", "VAT", "ZERO_TAX", "CODE"));
					db.실행();

					// 변환 후 조회
					OnToolBarSearchButtonClicked(null, null);
					메시지.일반알람(공통메세지.자료가정상적으로저장되었습니다);
				}
				catch (Exception ex)
				{
					메시지.오류알람(ex);
				}
			}
		}

		private void Btn테스트_Click(object sender, EventArgs e)
		{
			if (LoginInfo.UserID != "S-343")
				return;

			string query = @"
SELECT * 
FROM MA_FILEINFO
WHERE 1 = 1
	AND CD_COMPANY = 'K200'
	AND ID_MENU = 'P_CZ_FI_IMP_PMT_MNG'
	AND DTS_INSERT >= '2021-01-01'
";

			DataTable dt = SQL.GetDataTable(query);

			foreach (DataRow row in dt.Rows)
			{
				DX.FILE.Download(row["FILE_PATH"] + "/" + row["CD_FILE"] + "/" + row["FILE_NAME"], @"d:\수입신고필증\" + row["FILE_NAME"]);
			}
		}

		private void Btn메일발송_Click(object sender, EventArgs e)
		{
			DataTable dtHead = SQL.GetDataTable("PS_CZ_PU_PO_PRT_H", 회사코드, 발주번호);
			
			if (dtHead.Rows.Count != 1)
				return;

			grd헤드["DT_SEND"] = 메일발송(dtHead, false);
		}
		
		private void Btn일괄변환_Click(object sender, EventArgs e)
		{
			bool boSendMail = false;
			int succCount = 0;
			int skipCount = 0;
			int failCount = 0;

			if (ShowMessage("일괄변환 시 많은 시간이 소요됩니다. 진행 하시겠습니까?", "QY2") != DialogResult.Yes)
				return;

			if (ShowMessage("메일발송을 함께 진행하시겠습니까?", "QY2") == DialogResult.Yes)
				boSendMail = true;

			// 저장 후 진행함
			if (!SaveData())
				return;

			MsgControl.ShowMsg(WorkMessage);

			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			{
				string purchaseNumber = grd헤드[i, "NO_PO"].ToString();
				string partnerCode = grd헤드[i, "CD_PARTNER"].ToString();

				DataTable dtHead = DBMgr.GetDataTable("PS_CZ_PU_PO_PRT_H", 회사코드, purchaseNumber);
				DataTable dtLine = grd라인.DataTable.Select("NO_PO = '" + purchaseNumber + "'").CopyToDataTable();
				
				if (dtHead.Rows.Count == 0 || dtLine.Rows.Count == 0 || partnerCode == "10493")
				{
					skipCount++;
					continue;
				}
				
				// Pdf 저장 및 업로드
				try
				{

					// 발주서 Pdf 인쇄
					if (파일번호.Left(2).In("SB", "NS"))
					{
						grd헤드[i, "NM_FILE"] = 발주서(회사코드, grd헤드[i, "NO_PO"].문자(), false);
						grd헤드.SetCellImage(i, grd헤드.Cols["FILE_ICON"].Index, Icons.GetExtension("pdf"));
					}
					else
					{
						if (회사코드 == "K100")
							grd헤드[i, "NM_FILE"] = 발주서(회사코드, grd헤드[i, "NO_PO"].문자(), false);
						else
							grd헤드[i, "NM_FILE"] = PrintReport(dtHead, dtLine, new H_CZ_PRT_OPTION(), false);

						grd헤드.SetCellImage(i, grd헤드.Cols["FILE_ICON"].Index, Icons.GetExtension("pdf"));
					}

					// 발주서
					//grd헤드[i, "NM_FILE"] = PrintReport(dtHead, dtLine, new H_CZ_PRT_OPTION(), false);
					//grd헤드.SetCellImage(i, grd헤드.Cols["FILE_ICON"].Index, Icons.GetExtension("pdf"));

					// 라벨
					PrintLabel(dtHead, dtLine);

					// 메일 전송
					if (boSendMail)
					{
						grd헤드[i, "DT_SEND"] = 메일발송(dtHead, true);

						if (GetTo.String(grd헤드[i, "DT_SEND"]) != "")
							succCount++;
						else
							failCount++;
					}
				}
				catch (Exception ex)
				{
					ShowMessage(ex.Message);
					break;
				}
			}

			MsgControl.CloseMsg();

			string msg = @"
작업이 완료되었습니다.

발송성공 : " + succCount + @"건
발송예외 : " + skipCount + @"건
발송실패 : " + failCount + "건";

			ShowMessage(msg);

			if (failCount > 0)
				ShowMessage("발송실패 건수가 있습니다. 확인바랍니다.");
		}

		// 매입처/아이템 추가,삭제
		private void Btn매입처추가_Click(object sender, EventArgs e)
		{			
			// 발주번호
			string 발주번호max = grd헤드.DataTable.Compute("MAX(NO_PO)", "").ToString();
			string 발주번호new = 파일번호 + "-" + string.Format("{0:0#}", (발주번호max == "" ? 0 : 발주번호max.Right(2).ToInt()) + 1);

			grd헤드.Rows.Add();
			grd헤드.Row				= grd헤드.Rows.Count - 1;
			grd헤드["NO_SO"]			= 파일번호;
			grd헤드["NO_PO"]			= 발주번호new.ToUpper();
			grd헤드["DT_PO"]			= UTIL.오늘();
			grd헤드["NO_EMP"]		= LoginInfo.UserID;
			grd헤드["NM_EMP"]		= LoginInfo.UserName;
			grd헤드["CD_PURGRP"]		= LoginInfo.PurchaseGroupCode;
			grd헤드["NM_PURGRP"]		= LoginInfo.PurchaseGroupName;
			grd헤드["DELIVERY_TIME"]	= "AS BELOW";
			grd헤드["COND_SHIPMENT"]	= "EXW";
			grd헤드["TP_PACKING"]	= "002";

			grd헤드.AddFinished();
		}

		private void Btn매입처삭제_Click(object sender, EventArgs e)
		{
			if (grd헤드.HasNormalRow)
			{
				// 라인 삭제
				DataRow[] rows = grd라인.DataTable.Select("NO_PO = '" + 발주번호 + "'");
				foreach (DataRow dr in rows)
					dr.Delete();

				// 헤드 삭제
				grd헤드.Rows.Remove(grd헤드.Row);
			}
		}

		private void Btn아이템추가_Click(object sender, EventArgs e)
		{
			// 매입처 확인
			if (매입처코드 == "")
			{
				UTIL.메세지("매입처를 선택하세요");
				return;
			}

			// 현재 발주 Line Data의 필요한 컬럼만 가져오기 
			DataTable dtLine = new DataView(grd라인.DataTable).ToTable(false, "NO_SO", "NO_PO", "NO_LINE", "QT_PO");	// RowFilter 때문에 신규 생성			
			
			// 팝업
			H_CZ_PU_ITEM f = new H_CZ_PU_ITEM(파일번호, GetTo.Xml(dtLine));
			
			if (f.ShowDialog() == DialogResult.OK)
			{
				if (f.Items == null)
					return;

				foreach (DataRow row in f.Items.Rows)
				{
					if (dtLine.Select("NO_PO = '" + 발주번호 + "' AND NO_LINE = " + row["NO_LINE"]).Length > 0)
					{
						ShowMessage("중복 아이템이 존재합니다.");
						return;
					}

					grd라인.Rows.Add();
					grd라인.Row = grd라인.Rows.Count - 1;
					grd라인["NO_PO"] = 발주번호;
					grd라인["NO_LINE"] = row["NO_LINE"];
					grd라인["NO_DSP"] = row["NO_DSP"];
					grd라인["NM_SUBJECT"] = row["NM_SUBJECT"];
					grd라인["CD_ITEM_PARTNER"] = row["CD_ITEM_PARTNER"];
					grd라인["NM_ITEM_PARTNER"] = row["NM_ITEM_PARTNER"];
					grd라인["CD_ITEM"] = row["CD_ITEM"];
					grd라인["CD_UNIT_MM"] = row["CD_UNIT_MM"];
					grd라인["QT_PO"] = row["QT_PO"];
					//grd라인["QT_RAW"] = row["QT_RAW"];
					grd라인["UM_EX_E"] = row["UM_EX_STD"];
					grd라인["AM_EX_E"] = row["AM_EX_STD"];
					grd라인["RT_DC"] = row["RT_DC"];
					grd라인["UM_EX"] = row["UM_EX"];
					grd라인["AM_EX"] = row["AM_EX"];
					grd라인["UM"] = row["UM"];
					grd라인["AM"] = row["AM"];
					grd라인["VAT"] = row["VAT"];
					grd라인["LT"] = row["LT"];
					grd라인["NO_SO"] = row["NO_SO"];
					grd라인["NO_SOLINE"] = row["NO_SOLINE"];
					grd라인.AddFinished();

					행계산(grd라인.Row, "", false);
				}

				합계계산();
			}
		}

		private void Btn아이템삭제_Click(object sender, EventArgs e)
		{
			if (grd라인.HasNormalRow)
				grd라인.Rows.Remove(grd라인.Row);
		}

		private void Ctx담당자_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			if (CODE.영업구매그룹(ctx담당자.CodeValue) is DataTable dt && dt.Rows.Count > 0)
			{
				ctx구매그룹.CodeValue = dt.Rows[0]["CD_PURGRP"].ToString();
				ctx구매그룹.CodeName = dt.Rows[0]["NM_PURGRP"].ToString();
				grd헤드["CD_PURGRP"] = dt.Rows[0]["CD_PURGRP"];				
				grd헤드["NM_PURGRP"] = dt.Rows[0]["NM_PURGRP"];
			}
			else
			{
				ctx구매그룹.CodeValue = "";
				ctx구매그룹.CodeName = "";
				grd헤드["CD_PURGRP"] = "";
				grd헤드["NM_PURGRP"] = "";
			}
		}

		private void Ctx매입처_QueryAfter(object sender, BpQueryArgs e)
		{
			DataTable dt = CODE.거래처(매입처코드);

			grd헤드["SEQ_ATTN"]		= 0;
			grd헤드["CD_EXCH"]		= dt.Rows[0]["CD_EXCH_P"].ToString() == "" ? "000" : dt.Rows[0]["CD_EXCH_P"].ToString();
			grd헤드["CD_TPPO"]		= dt.Rows[0]["CD_TPPO"].ToString();
			grd헤드["NM_TPPO"]		= dt.Rows[0]["NM_TPPO"].ToString();
			grd헤드["FG_PAYMENT"]	= dt.Rows[0]["FG_PAYMENT"];
			grd헤드["FG_TAX"]		= dt.Rows[0]["FG_TAX"];
			grd헤드["CD_AREA"]		= dt.Rows[0]["CD_AREA"];
			grd헤드["TP_DIGIT"]		= grd헤드["CD_EXCH"].ToString() == "000" ? "6" : "3";
			grd헤드.AddFinished();

			Cbo통화_SelectionChangeCommitted(null, null);
			Cbo과세구분_SelectionChangeCommitted(null, null);
		}
		
		private void Cbo매입처담당자_SelectionChangeCommitted(object sender, EventArgs e)
		{
			grd헤드["SEQ_ATTN"] = cbo매입처담당자.값();
		}

		

		private void Ctx발주형태_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P61_CODE1 = "N";
		}

		// ********** 단가에 영향을 미치는 이벤트들
		private void Cbo통화_SelectionChangeCommitted(object sender, EventArgs e)
		{
			cur환율.DecimalValue = cbo통화.값() == "000" ? 1 : CODE.환율(dtp발주일자.Text, cbo통화.값(), "P");
			
			if (grd라인.HasNormalRow)
			{
				UTIL.작업중(WorkMessage);
				grd라인.Redraw = false;

				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
					행계산(i, "", false);
				
				grd라인.Redraw = true;
				UTIL.작업중();
			}

			합계계산();
		}

		private void Cbo과세구분_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (grd라인.HasNormalRow)
			{
				UTIL.작업중(WorkMessage);
				grd라인.Redraw = false;

				for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
					부가세계산(i);
				
				grd라인.Redraw = true;
				UTIL.작업중();
			}

			합계계산();
		}

		private void Btn할인_Click(object sender, EventArgs e)
		{
			UTIL.작업중(WorkMessage);
			grd라인.Redraw = false;

			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (grd라인[i, "NO_LINE"].ToInt() < 90000)
				{
					grd라인[i, "RT_DC"] = cur할인.DecimalValue;
					행계산(i, "RT_DC", false);
				}
			}

			합계계산();
			grd라인.Redraw = true;
			UTIL.작업중();
		}

		private void Btn납기_Click(object sender, EventArgs e)
		{
			UTIL.작업중(WorkMessage);
			grd라인.Redraw = false;

			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
			{
				if (grd라인[i, "NO_LINE"].ToInt() < 90000)
					grd라인[i, "LT"] = cur납기.DecimalValue;
			}

			grd라인.Redraw = true;
			UTIL.작업중();
		}

		private void Btn표시형식_Click(object sender, EventArgs e)
		{
			UTIL.작업중(WorkMessage);
			grd라인.Redraw = false;

			for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
				행계산(i, "", false);

			합계계산();
			grd라인.Redraw = true;
			UTIL.작업중();
		}

		private void Btn납기전체_Click(object sender, EventArgs e)
		{
			UTIL.작업중(WorkMessage);
			grd라인.Redraw = false;

			foreach (DataRow row in grd헤드.DataTable.Rows)
				row["LT"] = cur납기.DecimalValue;

			foreach (DataRow row in grd라인.DataTable.Select("NO_LINE < 90000"))
				row["LT"] = cur납기.DecimalValue;

			grd라인.Redraw = true;
			UTIL.작업중();
		}

		private void Btn인도기간전체_Click(object sender, EventArgs e)
		{
			UTIL.작업중(WorkMessage);
			grd라인.Redraw = false;

			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
				grd헤드[i, "DELIVERY_TIME"] = tbx인도기간.Text;

			grd라인.Redraw = true;
			UTIL.작업중();
		}

		// 기타		
		private void Btn부대비용_Click(object sender, EventArgs e)
		{
			if (cbo부대비용.값() == "")
			{
				UTIL.메세지("부대비용을 선택하세요!");
				//ShowMessage("부대비용을 선택하세요!");
				return;
			}

			// 중복 부대비용 확인
			//if (grdLine.DataTable.Select("NO_PO = '" + PurchaseNumber + "' AND CD_ITEM = '" + cbo부대비용.GetValue() + "'").Length > 0)
			//{
			//    ShowMessage("이미 추가되었습니다.");
			//    return;
			//}

			// 부대비용 추가
			int maxLineNumber = (int)grd라인.Aggregate(AggregateEnum.Max, "NO_LINE");
			maxLineNumber = (maxLineNumber < 90000) ? 90001 : maxLineNumber + 1;

			grd라인.Rows.Add();
			grd라인.Row = grd라인.Rows.Count - 1;
			grd라인["NO_PO"] = 발주번호;
			grd라인["NO_LINE"] = maxLineNumber;
			grd라인["NM_ITEM_PARTNER"] = cbo부대비용.GetText();
			grd라인["CD_ITEM"] = cbo부대비용.GetValue();
			grd라인["NM_ITEM"] = cbo부대비용.GetText();
			grd라인["QT_PO"] = 1;
			grd라인["RT_DC"] = 0;
			grd라인["LT"] = 0;
			grd라인["NO_SO"] = 파일번호;
			grd라인["NO_SOLINE"] = 0;
			grd라인.AddFinished();

			grd라인.Col = grd라인.Cols["UM_EX_E"].Index;
			grd라인.Focus();
		}

		private void Btn주문번호_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("COMPANY");
			dt.Columns.Add("ITEM");
			dt.Rows.Add(회사코드, 발주번호);

			// 쿼리
			string query = @"
UPDATE PU_POH SET NO_ORDER = @NO_ORDER WHERE CD_COMPANY = @CD_COMPANY AND NO_PO = @NO_PO
EXEC PX_CZ_PU_POH_EXT_2 @PO";

			SQL sql = new SQL(query, SQLType.Text);
			sql.Parameter.추가("@CD_COMPANY"	, 회사코드);
			sql.Parameter.추가("@NO_PO"		, 발주번호);
			sql.Parameter.추가("@NO_ORDER"	, tbx주문번호.Text);
			sql.Parameter.추가("@PO"			, "UNIQ", dt);
			sql.ExecuteNonQuery();

			UTIL.메세지(공통메세지.자료가정상적으로저장되었습니다);
		}

		private void Btn딜러PO_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("COMPANY");
			dt.Columns.Add("ITEM");
			dt.Rows.Add(회사코드, 발주번호);
			
			// 쿼리
			string query = @"
UPDATE PU_POH SET DEALER_PO = @DEALER_PO WHERE CD_COMPANY = @CD_COMPANY AND NO_PO = @NO_PO
EXEC PX_CZ_PU_POH_EXT_2 @PO";

			SQL sql = new SQL(query, SQLType.Text);
			sql.Parameter.추가("@CD_COMPANY"	, 회사코드);
			sql.Parameter.추가("@NO_PO"		, 발주번호);
			sql.Parameter.추가("@DEALER_PO"	, tbx딜러PO.Text, true);
			sql.Parameter.추가("@PO"			, "UNIQ", dt);
			sql.ExecuteNonQuery();

			grd헤드.AcceptChanges();
			grd라인.AcceptChanges();
			UTIL.메세지(공통메세지.자료가정상적으로저장되었습니다);
		}

		private void BtnBL번호_Click(object sender, EventArgs e)
		{
			string lines = grd라인.GetCheckedRows("YN_BL").배열<string>("NO_LINE").결합(",");

			// 쿼리
			string query = @"
UPDATE PU_POH SET NO_BL = @NO_BL WHERE CD_COMPANY = @CD_COMPANY AND NO_PO = @NO_PO
UPDATE PU_POL SET YN_BL = 'N' WHERE CD_COMPANY = @CD_COMPANY AND NO_PO = @NO_PO";

			if (lines != "")
				query += @"
UPDATE PU_POL SET YN_BL = 'Y' WHERE CD_COMPANY = @CD_COMPANY AND NO_PO = @NO_PO AND NO_LINE IN (" + lines + ")";

			SQL sql = new SQL(query, SQLType.Text);
			sql.Parameter.추가("@CD_COMPANY"	, 회사코드);
			sql.Parameter.추가("@NO_PO"		, 발주번호);
			sql.Parameter.추가("@NO_BL"		, tbxBL번호.Text, true);
			sql.ExecuteNonQuery();

			grd헤드.AcceptChanges();
			grd라인.AcceptChanges();
			UTIL.메세지(공통메세지.자료가정상적으로저장되었습니다);
		}

		private void Btn미입고_Click(object sender, EventArgs e)
		{
			string query = @"
UPDATE PU_POH SET
	CD_DLV = @CD_DLV
,	DT_DLV = @DT_DLV
,	TM_DLV = @TM_DLV
,	DC_DLV = @DC_DLV
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_PO = @NO_PO";
			
			SQL sql = new SQL(query, SQLType.Text);
			sql.Parameter.추가("@CD_COMPANY", 회사코드);
			sql.Parameter.추가("@NO_PO", 발주번호);
			sql.Parameter.추가("@CD_DLV", cbo납품장소.값(), true);
			sql.Parameter.추가("@DT_DLV", dtp납품일자.Text, true);
			sql.Parameter.추가("@TM_DLV", cbo납품시간.값(), true);
			sql.Parameter.추가("@DC_DLV", tbx납품비고.Text, true);
			sql.ExecuteNonQuery();

			grd헤드.AcceptChanges();
			grd라인.AcceptChanges();
			UTIL.메세지(공통메세지.자료가정상적으로저장되었습니다);
		}

		private void Grd헤드_DoubleClick(object sender, EventArgs e)
		{			
			if (!grd헤드.HasNormalRow || grd헤드.MouseCol <= 0)
			    return;

			// 헤더클릭
			if (grd헤드.MouseRow < grd헤드.Rows.Fixed)
			{
				그리드스타일(grd헤드);
				return;
			}

			// 첨부파일 클릭
			if (grd헤드.Cols[grd헤드.Col].Name == "FILE_ICON")
				FileMgr.Download_WF(회사코드, 파일번호, grd헤드["NM_FILE"].ToString(), true);
		}
		
		private void Grd라인_KeyDown(object sender, KeyEventArgs e)
		{
			// 엑셀 붙여넣기 (매입견적단가 컬럼만 가능)
			//if (e.KeyData == (Keys.Control | Keys.V))
			//{
			//	if (grd라인.Cols[grd라인.Col].Name == "UM_EX_STD")
			//	{
			//		string[,] clipboard = Util.GetClipboardValues();

			//		for (int i = 0; i < clipboard.GetLength(0); i++)
			//		{
			//			grd라인[grd라인.Row + i, grd라인.Col] = clipboard[i, 0];
			//			CalcRow(grd라인.Row + i, "UM_EX_STD");
			//			if (grd라인.Row + i == grd라인.Rows.Count - 1) 
			//				break;
			//		}
			//	}
			//}
		}

		private void Grd라인_AfterEdit(object sender, RowColEventArgs e)
		{
			string colName = grd라인.Cols[e.Col].Name;
			행계산(e.Row, colName, true);
		}

		private void 그리드스타일(FlexGrid flexGrid)
		{
			flexGrid.Redraw = false;

			if (flexGrid == grd헤드)
			{
				for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
				{					
					// 첨부파일 아이콘 (라이브 서버가 아닌 경우 아이콘이 없으므로 체크함)
					if (flexGrid.Cols.Contains("FILE_ICON"))
					{
						string fileName = flexGrid[i, "NM_FILE"].String();

						if (fileName == "")
						{
							flexGrid.SetCellImage(i, flexGrid.Cols["FILE_ICON"].Index, null);
						}
						else
						{
							Image img = 아이콘.확장자(파일.확장자(fileName));
							flexGrid.SetCellImage(i, flexGrid.Cols["FILE_ICON"].Index, img);
						}
					}
				}
			}

			flexGrid.Redraw = true;
		}

		#endregion

		#region ==================================================================================================== Search

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			string query;

			// DY파일인 경우 최근 파일을 불러옴
			if (파일번호.In("DY", "SH", "TY"))
			{
				query = @"
SELECT TOP 1
	NO_PO
FROM PU_POH WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + 회사코드 + @"'
	AND NO_PO LIKE '" + 파일번호 + @"[0-9]%'
ORDER BY NO_PO DESC";

				DataTable dtDy = SQL.GetDataTable(query);
				파일번호 = dtDy.Rows[0][0].String();
			}

			// 대문자 변환
			파일번호 = 파일번호.ToUpper().Trim();

			// 발주번호를 입력했을 경우 파일번호로 변환
			if (파일번호.Length == 13)
				파일번호 = 파일번호.Left(10);

			// ********** 수주 조회
			query = @"
SELECT
	A.NO_SO
,	A.CD_PARTNER
,	B.LN_PARTNER
,	A.NO_IMO
,	C.NO_HULL
,	C.NM_VESSEL
,	A.NO_PO_PARTNER
,	D.CD_FLAG2
FROM	  SA_SOH		AS A WITH(NOLOCK)
JOIN	  MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_PARTNER = B.CD_PARTNER
JOIN	  CZ_MA_HULL	AS C WITH(NOLOCK) ON A.NO_IMO = C.NO_IMO
LEFT JOIN MA_CODEDTL	AS D WITH(NOLOCK) ON A.CD_COMPANY = D.CD_COMPANY AND D.CD_FIELD = 'CZ_SA00023' AND LEFT(A.NO_SO, 2) = D.CD_SYSDEF
WHERE 1 = 1
	AND A.CD_COMPANY = '" + 회사코드 + @"'
	AND A.NO_SO = '" + 파일번호 + "'";

			DataTable dtSo = SQL.GetDataTable(query);

			if (dtSo.Rows.Count == 1)
			{
				ctx매출처.값(dtSo.Rows[0]["CD_PARTNER"]);
				ctx매출처.글(dtSo.Rows[0]["LN_PARTNER"]);
				ctx호선.값(dtSo.Rows[0]["NO_IMO"]);
				ctx호선.글(dtSo.Rows[0]["NO_HULL"]);
				tbx선명.Text = dtSo.Rows[0]["NM_VESSEL"].ToString();

				ToolBarAddButtonEnabled = true;	// 수주만 있는 경우 추가 버튼이 활성화 안되므로 강제 활성화
			}
			else
			{
				UTIL.메세지(공통메세지.선택된자료가없습니다);
				tbx파일번호.Focus();
				return;
			}

			// ********** 발주 조회
			try
			{
				DataTable dtHead;

				if (신규)
				{
					dtHead = SQL.GetDataTable("PS_CZ_PU_PO_REG_H_QTN", 회사코드, 파일번호);
					DataTable dtExch = CODE.환율(UTIL.오늘());

					// 기본값 바인딩
					for (int i = 0; i < dtHead.Rows.Count; i++)
					{
						// 발주일자
						dtHead.Rows[i]["DT_PO"] = UTIL.오늘();

						// 담당자
						dtHead.Rows[i]["NO_EMP"] = LoginInfo.UserID;
						dtHead.Rows[i]["NM_EMP"] = LoginInfo.UserName;

						// 구매그룹
						dtHead.Rows[i]["CD_PURGRP"] = LoginInfo.PurchaseGroupCode;
						dtHead.Rows[i]["NM_PURGRP"] = LoginInfo.PurchaseGroupName;

						// 환율
						DataTable dtExchSel = dtExch.복사("CD_EXCH = '" + dtHead.Rows[i]["CD_EXCH"] + "'");

						if (dtExchSel.Rows.Count == 0)
							throw new Exception("환율 정보가 없습니다.");

						dtHead.Rows[i]["RT_EXCH"] = dtExchSel.Rows[0]["RT_PURCHASE"];
					}
				}
				else
				{
					dtHead = SQL.GetDataTable("PS_CZ_PU_PO_REG_H", 회사코드, 파일번호);

					if (dtHead.Rows.Count == 0)
					{
						UTIL.메세지(공통메세지.선택된자료가없습니다);
						tbx파일번호.Focus();
						return;
					}
				}

				pnl파일번호.사용(false);
				grd헤드.Binding = dtHead;
				그리드스타일(grd헤드);
			}
			catch (Exception ex)
			{
				UTIL.메세지(ex);
			}




			

			//for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			//{
			//	if (edited[i - grd헤드.Rows.Fixed])
			//		grd헤드[i, "EDITED"] = "Y";			// RowState 변경				
			//}

			// ********** 발주체크			
			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			{
				// 신규일 경우는 채인지 먹히게 값 한번 변경해줌
				if (신규)
				{
					grd헤드[i, "NO_SO"] = "";
					grd헤드[i, "NO_SO"] = 파일번호;
				}

				// afterrow 이벤트 발생
				grd헤드.Row = i;
			}

			grd헤드.Row = grd헤드.Rows.Fixed;
		}

		private void Grd헤드_AfterRowChange(object sender, RangeEventArgs e)
		{
			// ********** 매입처담당자 바인딩
			DataTable dt = 코드.거래처담당자(매입처코드, 담당자구분.매입발주);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			dt.Rows[0][cbo매입처담당자.ValueMember] = 0;
			dt.Rows[0][cbo매입처담당자.DisplayMember] = "";
			cbo매입처담당자.바인딩(dt, false);

			// 기본 담당자 설정 : 최초 1회만 기본 설정되고 이후로는 seq_attn 값이 0이 있을거기 때문에 설정 안될꺼임
			if (grd헤드["SEQ_ATTN"].String() == "")
			{				
				DataRow row = dt.Select("", "TP_PTR DESC").데이터행(0);
				if (row != null) cbo매입처담당자.값(row["SEQ"]);

				grd헤드["SEQ_ATTN"] = cbo매입처담당자.값();
			}
			else
			{
				cbo매입처담당자.값(grd헤드["SEQ_ATTN"]);
			}

			// ********** 지급조건 에디트 여부 설정 
			pnl지급조건.사용(grd헤드["YN_EDIT_PAYMENT"].String() == "Y");

			// ********** 라인 바인딩
			grd라인.Redraw = false;
			string 필터 = "NO_PO = '" + 발주번호 + "'";

			if (grd헤드.DetailQueryNeed)
			{
				if (신규)
				{
					// L 바인딩 → 기본값 설정
					DataTable dtLine = SQL.GetDataTable("PS_CZ_PU_PO_REG_L_QTN_2", 회사코드, 파일번호, 매입처코드, 발주번호);	// 헤더는 1도 없는거 쓰고 라인은 _2 써야함
					grd라인.바인딩(dtLine, 필터);

					// 재계산 (환율적용 및 부가세 계산)
					for (int i = grd라인.Rows.Fixed; i < grd라인.Rows.Count; i++)
						행계산(i, "", false);
				}
				else
				{
					DataTable dtLine = SQL.GetDataTable("PS_CZ_PU_PO_REG_L", 회사코드, 발주번호);
					grd라인.바인딩(dtLine, 필터);
				}
			}
			else
			{
				grd라인.바인딩(null, 필터);
			}

			합계계산();
			grd라인.Redraw = true;
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			if (!MsgAndSave(PageActionMode.Search))
				return;

			Clear();
		}
		
		#endregion

		#region ==================================================================================================== Save

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			if (!base.Verify()) return;

			// 아이템 검사
			for (int i = grd헤드.Rows.Fixed; i < grd헤드.Rows.Count; i++)
			{
				if (grd라인.DataTable.Select("NO_PO = '" + grd헤드[i, "NO_PO"] + "'").Length == 0)
				{
					grd헤드.Row = i;
					ShowMessage("발주 아이템이 없습니다.");
					return;
				}
			}

			// 납기 검사
			if (grd라인.DataTable.Select("LT IS NULL").Length > 0)
			{
				ShowMessage("납기를 입력하십시오.");
				return;
			}

			// 저장			
			try
			{
				DataTable dtHead = 신규 ? grd헤드.DataTable : grd헤드.수정데이터테이블();
				DataTable dtLine = grd라인.수정데이터테이블();

				// ********** 경고마스터 확인
				DataTable dtItem = dtLine.Copy();

				// 매입처 추가
				dtItem.Columns.Add("CD_VENDOR", typeof(string));
				foreach (DataRow row in dtItem.Rows)
				{
					if (row.RowState != DataRowState.Deleted)
						row["CD_VENDOR"] = grd헤드.DataTable.Select("NO_PO = '" + row["NO_PO"] + "'")[0]["CD_PARTNER"];
				}

				// 컬럼이름 변경
				dtItem.Columns["NO_PO"].ColumnName = "NO_FILE";
				dtItem.Columns["UM"].ColumnName = "UM_PU";

				// 조회
				WARNING warning = new WARNING(WARNING_TARGET.발주) { 파일구분 = 파일번호, 매출처코드 = ctx매출처.값(), IMO번호 = ctx호선.값(), 아이템 = dtItem, SQLDebug = sqlDebug };				
				warning.조회(true);

				// 경고할 꺼리가 있는 경우만.
				if (warning.경고여부)
				{
					if (warning.ShowDialog() == DialogResult.Cancel || warning.저장불가)
					{
						UTIL.메세지("작업이 취소되었습니다.", 메세지구분.경고알람);
						return;
					}
				}

				// ********** 저장
				UTIL.작업중(WorkMessage);
				
				string headJson = 신규 ? dtHead.Json(DataRowState.Added) : dtHead.Json();
				string lineJson = 신규 ? dtLine.Json(DataRowState.Added) : dtLine.Json();
				
				SQL sql = new SQL("PX_CZ_PU_PO_REG_2", SQLType.Procedure, sqlDebug);
				sql.Parameter.추가("@JSON_H", headJson);
				sql.Parameter.추가("@JSON_L", lineJson);
				sql.ExecuteNonQuery();

				grd헤드.AcceptChanges();
				grd라인.AcceptChanges();
				신규 = false;
				UTIL.메세지(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				UTIL.메세지(ex);
			}
		}

		#endregion

		#region ==================================================================================================== Delete

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes) return;
			if (LoginInfo.UserID != "S-343" && !Util.CheckPW()) return;

			DataTable dt = grd헤드.GetTableFromGrid();
			DBHelper.Save(Util.SetSpInfo(dt, "SP_CZ_PU_POH_REG_XML", "D"));
			 
			Clear();
			ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);			
		}

		#endregion

		#region ==================================================================================================== Print

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			// 프린트 옵션
			H_CZ_PRT_OPTION f = new H_CZ_PRT_OPTION();

			if (Control.ModifierKeys == Keys.Control)
			{
				if (f.ShowDialog() != DialogResult.OK)
					return;
			}

			// 프린트 할 Data 조회
			DataTable dtHead = DBMgr.GetDataTable("PS_CZ_PU_PO_PRT_H", 회사코드, 발주번호);
			DataTable dtLine = grd라인.DataTable.Select("NO_PO = '" + 발주번호 + "'").CopyToDataTable();
			
			if (dtHead.Rows.Count != 1)
			{
				ShowMessage(공통메세지.선택된자료가없습니다);
				return;
			}

			// 프린트 GoGo!!
			try
			{
				// 발주서 Pdf 인쇄
				if (파일번호.Left(2).In("SB", "NS"))
				{
					grd헤드["NM_FILE"] = 발주서(회사코드, 발주번호, true);
					grd헤드.SetCellImage(grd헤드.Row, grd헤드.Cols["FILE_ICON"].Index, Icons.GetExtension("pdf"));
				}
				else
				{
					if (회사코드 == "K100" && 매입처코드 != "08507")
						grd헤드["NM_FILE"] = 발주서(회사코드, 발주번호, true);
					else
						grd헤드["NM_FILE"] = PrintReport(dtHead, dtLine, f, true);

					grd헤드.SetCellImage(grd헤드.Row, grd헤드.Cols["FILE_ICON"].Index, Icons.GetExtension("pdf"));
				}

				// 라벨 Pdf 인쇄
				PrintLabel(dtHead, dtLine);
			}
			catch (Exception ex)
			{
				MsgControl.CloseMsg();
				ShowMessage(ex.Message);
			}			
		}

		private string 발주서(string 회사코드, string 발주번호, bool 실행여부)
		{
			string query = "SELECT CD_PARTNER, DT_PO FROM PU_POH WHERE CD_COMPANY = '" + 회사코드 + "' AND NO_PO = '" + 발주번호 + "'";
			DataTable dt = 디비.결과(query);
			string 매입처코드 = dt.첫행("CD_PARTNER").문자();
			string 발주일자 = dt.첫행("DT_PO").문자();

			// ********** 발주서 다운로드
			// 웹에서 다운로드
			string 다운로드 = 문서.발주서(회사코드, 발주번호);			
			string 저장경로 = Application.StartupPath + @"\temp\";
			string 발주서 = string.Format("{0}_{1}_PORD_{2}.pdf", 파일번호, 매입처코드, 발주일자);
			파일.이름변경(다운로드, 발주서);

			// 워크플로우 파일 저장
			string localFile = 저장경로 + 발주서;
			string serverFile = FileMgr.Upload_WF(회사코드, 파일번호, localFile, false);

			// WORKFLOW DB 저장
			WorkFlow work = new WorkFlow(파일번호, "10");
			work.AddItem(발주번호, 매입처코드, 발주서, serverFile);
			work.Save();

			// ********** 선용일 경우 블라인드 발주서 인쇄
			if (발주번호.왼쪽(2).포함("SB", "NS"))
			{
				다운로드 = 문서.발주서_블라인드(회사코드, 발주번호);
				string 발주서_입고용 = string.Format("{0}_{1}_PORD_{2}_B.pdf", 발주번호, dt.첫행("CD_PARTNER"), dt.첫행("DT_PO"));
				파일.이름변경(다운로드, 발주서_입고용);

				// 워크플로우 파일 저장
				localFile = 저장경로 + 발주서_입고용;
				serverFile = FileMgr.Upload_WF(회사코드, 파일번호, localFile, false);

				// WORKFLOW DB 저장
				WorkFlow 워크2 = new WorkFlow(파일번호, "60");
				워크2.AddItem(발주번호, 매입처코드, 발주서_입고용, serverFile);
				워크2.Save();
			}

			// ********** RPA 큐 추가
			RPA rpa = new RPA() { Process = "PO", FileNumber = 파일번호, PartnerCode = 매입처코드 };
			rpa.AddQueue();

			if (실행여부)
				Process.Start(저장경로 + 발주서);

			return localFile;
		}



		private string PrintReport(DataTable dtHead, DataTable dtLine, H_CZ_PRT_OPTION f, bool showViewer)
		{
			string reportCode;

			if (!f.Inquiry)
			{
				if (파일번호.Left(2).In("SB", "NS"))
					reportCode = "R_CZ_PU_PO_REG_LOG";	// 딘텍 선용인 경우만 씀
				else
					reportCode = "R_CZ_PU_PO_REG";
			}
			else
			{
				reportCode = "R_CZ_SA_QTN_REG_PINQ";
			}

			// 임시조치
			dtLine.Columns.Add("UM_EX_STD", typeof(decimal));
			dtLine.Columns.Add("AM_EX_STD", typeof(decimal));
			foreach (DataRow row in dtLine.Rows)
			{
				row["UM_EX_STD"] = row["UM_EX_E"];
				row["AM_EX_STD"] = row["AM_EX_E"];
			}

			ReportViewer report = new ReportViewer(reportCode, dtHead, dtLine.Copy());
			
			// 조건 정보
			report.SetData("DELIVERY_TIME", dtHead.Rows[0]["DELIVERY_TIME"]);	// 인도기간
			report.SetData("NM_PAYMENT"	  , dtHead.Rows[0]["NM_PAYMENT"]);		// 결제방법
			report.SetData("NM_PACKING"	  , dtHead.Rows[0]["NM_PACKING"]);		// 포장형태
			report.SetData("NM_SHIPMENT"  , dtHead.Rows[0]["NM_SHIPMENT"] + "   " + dtHead.Rows[0]["LOADING"]);	// 선적조건

			report.SetData("DC_ADS_LOG2"	, dtHead.Rows[0]["DC_ADS_LOG2"]);		// 물류센터 붙인 주소
			report.SetData("DC_CONTACT_LOG"	, dtHead.Rows[0]["DC_CONTACT_LOG"]);    // 영업물류담당자
			report.SetData("DC_SHIPBUILDER", dtHead.Rows[0]["DC_SHIPBUILDER"]);    // 영업물류담당자

			// 옵션
			if (f.Inquiry)
			{
				// Inquiry를 위한 정보 추가
				report.SetData("DT_INQ", dtHead.Rows[0]["DT_PO"].ToString());
				report.SetData("NO_FILE", dtHead.Rows[0]["NO_SO"].ToString());
			}

			if (!f.LogAddress)
			{
				report.SetData("DC_ADS_LOG", "");
				report.SetData("NO_TEL_LOG", "");
				report.SetData("NO_FAX_LOG", "");

				report.SetData("DC_ADS_LOG2", "");
				report.SetData("DC_CONTACT_LOG", "");
			}

			if (f.Agency)
			{
				report.SetData("LN_PARTNER"		 , dtHead.Rows[0]["LN_AGENCY"]);
				report.SetData("DC_ADS_H_PARTNER", dtHead.Rows[0]["DC_ADS_H_AGENCY"]);
				report.SetData("DC_ADS_D_PARTNER", dtHead.Rows[0]["DC_ADS_D_AGENCY"]);
				report.SetData("NO_TEL_PARTNER"	 , dtHead.Rows[0]["NO_TEL_AGENCY"]);
				report.SetData("NO_FAX_PARTNER"	 , dtHead.Rows[0]["NO_FAX_AGENCY"]);
				report.SetData("E_MAIL_PARTNER"	 , dtHead.Rows[0]["E_MAIL_AGENCY"]);
			}

			if (f.발주번호인쇄)
			{
				report.SetData("NO_SO", dtHead.Rows[0]["NO_PO"]);
			}

			// 금액
			decimal 견적금액 = GetTo.Decimal(dtLine.Compute("SUM(AM_EX_E)", ""));
			decimal 매입금액 = GetTo.Decimal(dtLine.Compute("SUM(AM_EX)", ""));
			decimal 할인금액 = 매입금액 - 견적금액;
			decimal 할인율 = (견적금액 == 0) ? 0 : Util.Round(할인금액 / 견적금액 * 100, 2);

			report.SetData("AM_EX_Q" , 견적금액);	
			report.SetData("AM_EX_P" , 매입금액);	
			report.SetData("AM_EX_DC", 할인금액);	
			report.SetData("RT_DC"	 , 할인율);

			// 인쇄 팝업
			if (showViewer)
				report.Print();

			// ********** 파일 만들기
			MsgControl.ShowMsg("PDF 저장중입니다. \r\n잠시만 기다려주세요!");

			string purchaseNumber = dtHead.Rows[0]["NO_PO"].ToString();
			string partnerCode = dtHead.Rows[0]["CD_PARTNER"].ToString();
			string purchaseDate = dtHead.Rows[0]["DT_PO"].ToString();

			// Pdf 저장
			string path = Application.StartupPath + @"\temp\";
			string fileName = string.Format("{0}_{1}_PORD_{2}.pdf", 파일번호, partnerCode, purchaseDate);
			report.ConvertPdf(path + fileName);

			// 워크플로우 파일 저장
			string localFile = path + fileName;
			string serverFile = FileMgr.Upload_WF(회사코드, 파일번호, localFile, false);

			// WORKFLOW DB 저장
			WorkFlow work = new WorkFlow(파일번호, "10");
			work.AddItem(purchaseNumber, partnerCode, fileName, serverFile);
			work.Save();

			// ********** RPA 큐 추가
			RPA rpa = new RPA() { Process = "PO", FileNumber = 파일번호, PartnerCode = partnerCode };
			rpa.AddQueue();

			MsgControl.CloseMsg();

			return serverFile;
		}

		private void PrintLabel(DataTable dtHead, DataTable dtLine)
		{			
			// ********** 라벨 출력 조건			
			if (!회사코드.In("K100", "K200"))
				return;

			if (dtHead.Rows[0]["YN_GR_LABEL"].ToString() != "Y")
				return;

			// 라벨 만들기
			string 파일이름 = LABEL.매입처라벨(dtHead.Rows[0]["NO_PO"].ToStr());

			// 워크플로우 파일 저장
			string serverFile = FileMgr.Upload_WF(회사코드, 파일번호, 파일이름, false);
			string purchasNumber = dtHead.Rows[0]["NO_PO"].ToString();
			string partnerCode = dtHead.Rows[0]["CD_PARTNER"].ToString();

			// 워크플로우 DB 저장
			WorkFlow work = new WorkFlow(파일번호, "57"); // 입고라벨
			work.AddItem(purchasNumber, partnerCode, DX.FILE.파일이름(파일이름), serverFile);
			work.Save();

			return;
		}

		#endregion

		#region ==================================================================================================== 계산식

		private void 행계산(int row, string 열이름, bool 합계계산여부)
		{
			if (열이름 == "")
			{
				// 환욜, 표시형식 입력시 계산
				매입견적단가입력(row);
				매입견적금액계산(row);
				매입단가입력(row);
				매입금액계산(row);
			}
			else if (열이름 == "QT_PO")       // 수량
			{
				매입견적금액계산(row);
				매입금액계산(row);
			}
			else if (열이름 == "UM_EX_E")
			{
				매입견적단가입력(row);
				매입견적금액계산(row);
				매입단가계산(row);
				매입금액계산(row);
			}
			else if (열이름 == "RT_DC")
			{
				매입단가계산(row);
				매입금액계산(row);
			}
			else if (열이름 == "UM_EX") 
			{
				매입단가입력(row);
				할인율계산(row);
				매입금액계산(row);
			}

			if (합계계산여부) 합계계산();
		}

		private void 매입견적단가입력(int row)
		{
			decimal 매입견적단가외화	= CALC.반올림(grd라인[row, "UM_EX_E"].ToDecimal(), 표시형식);
			decimal 매입견적단가원화	= CALC.반올림(매입견적단가외화 * 환율, 원화소수자리);

			grd라인[row, "UM_EX_E"]	= 매입견적단가외화;
			grd라인[row, "UM_KR_E"]	= 매입견적단가원화;
		}

		private void 매입견적금액계산(int row)
		{
			decimal 수량				= grd라인[row, "QT_PO"].ToDecimal();
			decimal 매입견적단가외화	= grd라인[row, "UM_EX_E"].ToDecimal();
			decimal 매입견적단가원화	= grd라인[row, "UM_KR_E"].ToDecimal();
			decimal 매입견적금액외화	= 매입견적단가외화 * 수량;
			decimal 매입견적금액원화	= 매입견적단가원화 * 수량;

			grd라인[row, "AM_EX_E"] = 매입견적금액외화;
			grd라인[row, "AM_KR_E"] = 매입견적금액원화;
		}

		private void 할인율계산(int row)
		{
			decimal 매입견적단가외화	= grd라인[row, "UM_EX_E"].ToDecimal();
			decimal 매입단가외화		= grd라인[row, "UM_EX"].ToDecimal();

			grd라인[row, "RT_DC"]	= CALC.할인율계산(매입견적단가외화, 매입단가외화);
		}

		private void 매입단가입력(int row)
		{
			decimal 매입단가외화		= CALC.반올림(grd라인[row, "UM_EX"].ToDecimal(), 표시형식);
			decimal 매입단가원화		= CALC.반올림(매입단가외화 * 환율, 원화소수자리);

			grd라인[row, "UM_EX"]	= 매입단가외화;
			grd라인[row, "UM"]		= 매입단가원화;
		}

		private void 매입단가계산(int row)
		{
			decimal 매입견적단가외화	= grd라인[row, "UM_EX_E"].ToDecimal();
			decimal 할인율			= grd라인[row, "RT_DC"].ToDecimal();
			decimal 매입단가외화		= CALC.반올림(CALC.할인율적용(매입견적단가외화, 할인율), 표시형식);
			decimal 매입단가원화		= CALC.반올림(매입단가외화 * 환율, 원화소수자리);

			grd라인[row, "UM_EX"]	= 매입단가외화;
			grd라인[row, "UM"]		= 매입단가원화;
		}

		private void 매입금액계산(int row)
		{
			decimal 수량				= grd라인[row, "QT_PO"].ToDecimal();
			decimal 매입단가외화		= grd라인[row, "UM_EX"].ToDecimal();
			decimal 매입단가원화		= grd라인[row, "UM"].ToDecimal();
			decimal 매입금액외화		= 매입단가외화 * 수량;
			decimal 매입금액원화		= 매입단가원화 * 수량;
			decimal 부가세			= CALC.반올림(매입금액원화 * 과세율 / 100, 원화소수자리);

			grd라인[row, "AM_EX"]	= 매입금액외화;
			grd라인[row, "AM"]		= 매입금액원화;
			grd라인[row, "VAT"]		= 부가세;
		}

		private void 부가세계산(int row)
		{
			decimal 매입금액원화		= grd라인[row, "AM"].ToDecimal();
			decimal 부가세			= CALC.반올림(매입금액원화 * 과세율 / 100, 원화소수자리);

			grd라인[row, "VAT"]		= 부가세;
		}

		private void 합계계산()
		{
			DataTable dt = grd라인.DataTable.복사("NO_PO = '" + 발주번호 + "'");

			decimal 매입견적금액외화	= dt.Compute("SUM(AM_EX_E)"	, "NO_LINE < 90000").ToDecimal();
			decimal 매입금액외화		= dt.Compute("SUM(AM_EX)"	, "NO_LINE < 90000").ToDecimal();
			decimal 부대비용외화		= dt.Compute("SUM(AM_EX)"	, "NO_LINE > 90000").ToDecimal();
			decimal 할인금액외화		= 매입견적금액외화 - 매입금액외화;
			decimal 매입금액합계외화	= 매입금액외화 + 부대비용외화;

			decimal 매입견적금액원화	= dt.Compute("SUM(AM_KR_E)"	, "NO_LINE < 90000").ToDecimal();
			decimal 매입금액원화		= dt.Compute("SUM(AM)"		, "NO_LINE < 90000").ToDecimal();
			decimal 부대비용원화		= dt.Compute("SUM(AM)"		, "NO_LINE > 90000").ToDecimal();
			decimal 할인금액원화		= 매입견적금액원화 - 매입금액원화;
			decimal 매입금액합계원화	= 매입금액원화 + 부대비용원화;
			
			decimal 할인율			= CALC.할인율계산(매입견적금액원화, 매입금액원화);
			decimal 부가세합계		= dt.Compute("SUM(VAT)", "").ToDecimal();
			
			// 헤드 정보 변경
			grd헤드["AM_EX"]	= 매입금액합계외화;
			grd헤드["AM"]	= 매입금액합계원화;
			grd헤드["VAT"]	= 부가세합계;

			// 라벨 정보 변경
			lbl매입견적금액원화.Text	= string.Format("{0:#,##0.##}", 매입견적금액원화);
			lbl할인금액원화.Text		= string.Format("{0:#,##0.##}", 할인금액원화);
			lbl매입금액원화.Text		= string.Format("{0:#,##0.##}", 매입금액원화);
			lbl할인율.Text			= string.Format("{0:#,##0.##}", 할인율);

			lbl통화.Text				= cbo통화.SelectedText;
			lbl매입견적금액외화.Text	= string.Format("{0:#,##0.##}", 매입견적금액외화);
			lbl할인금액외화.Text		= string.Format("{0:#,##0.##}", 할인금액외화);
			lbl매입금액외화.Text		= string.Format("{0:#,##0.##}", 매입금액외화);
			lbl부대비용외화.Text		= string.Format("{0:#,##0.##}", 부대비용외화);
			lbl매입금액합계외화.Text	= string.Format("{0:#,##0.##}", 매입금액합계외화);
		}

		#endregion

		private string 메일발송(DataTable dtHead, bool boAuto)
		{
			string query = "";

			// 딘텍 담당자 정보
			string empName = dtHead.Rows[0]["NM_EMP"].ToString();
			string empNameEn = dtHead.Rows[0]["EN_EMP"].ToString();
			string telNumber = dtHead.Rows[0]["NO_TEL_EMP"].ToString();
			string faxNumber = dtHead.Rows[0]["NO_FAX_EMP"].ToString();
			string eMail = dtHead.Rows[0]["NO_EMAIL_EMP"].ToString();

			string from = eMail;
			string to = dtHead.Rows[0]["E_MAIL_PARTNER"].ToString();
			string cc = eMail;
			
			// ********** 제목
			string title = "";
			string vessle = dtHead.Rows[0]["NM_VESSEL"] + " / " + dtHead.Rows[0]["NM_SHIP_YARD"] + " " + dtHead.Rows[0]["NO_HULL"] + " (IMO:" + dtHead.Rows[0]["NO_IMO"] + ")";
			string 날짜 = dtHead.첫행("DT_PO").문자().Substring(4, 2) + "/" + dtHead.첫행("DT_PO").문자().Substring(6, 2);
			string 매출처태그 = "";

			// 특정 매입처는 매출처 정보 넣어줌
			if (dtHead.Rows[0]["CD_PARTNER"].문자().포함("00523", "07110", "08075"))
			{
				query = "SELECT LN_PARTNER, NEOE.CODE_NAME(CD_COMPANY, 'MA_B000020', CD_NATION)  FROM MA_PARTNER WHERE CD_COMPANY = '" + 상수.회사코드 + "' AND CD_PARTNER = '" + dtHead.Rows[0]["CD_BUYER"] + "'";
				DataTable dtBuyer = 디비.결과(query);
				매출처태그 = "/" + dtBuyer.첫행(0) + "/" + dtBuyer.첫행(1);
				//dtHead.Rows[0]["NO_IMO"]]
			}


			if		(회사코드 == "K100") title = 날짜 + " DINTEC - ORDER SHEET(" + 파일번호 + ") " + vessle + 매출처태그 + " _" + dtHead.Rows[0]["CD_PARTNER"];
			else if (회사코드 == "K200") title = 날짜 + " DUBHECO - ORDER SHEET(" + 파일번호 + ") " + vessle + 매출처태그 + " _" + dtHead.Rows[0]["CD_PARTNER"];
			else if (회사코드 == "S100") title = 날짜 + "DINTEC - ORDER SHEET(" + 파일번호 + ") " + vessle + 매출처태그 + " _" + dtHead.Rows[0]["CD_PARTNER"];

			

			// 기획실로 보내는 메일
			if (dtHead.Rows[0]["CD_PARTNER"].문자() == "SPO")
				title = 날짜 + "DINTEC - 재고발주요청(" + 파일번호 + ") " + vessle + " _" + dtHead.Rows[0]["CD_PARTNER"];

			// 첨부파일 (발주서, 입고라벨, 발주서(입고용))
			query = @"
SELECT
	NM_FILE_REAL
FROM
(
	SELECT
		  NM_FILE_REAL
		, ROW_NUMBER() OVER (PARTITION BY TP_STEP ORDER BY NO_LINE DESC)	AS RN
	FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
	WHERE 1 = 1
		AND CD_COMPANY = '" + 회사코드 + @"'
		AND NO_KEY = '" + 파일번호 + @"'
		AND NO_KEY_REF = '" + dtHead.Rows[0]["NO_PO"] + @"'
		AND TP_STEP IN ('10', '57', '60')
) AS A
WHERE RN = 1";

			DataTable dtFiles = DBMgr.GetDataTable(query);
			List<string> files = new List<string>();
			
			foreach (DataRow row in dtFiles.Rows)
				files.Add(row[0].ToString());

			// 테크로스만 특별 첨부
			if (dtHead.Rows[0]["CD_PARTNER"].ToString() == "17747")
			{
				DataTable dtTcrs = SQL.GetDataTable("PS_CZ_PU_PO_REG_TCRS", dtHead.Rows[0]["CD_COMPANY"], dtHead.Rows[0]["NO_PO"]);

				string txt = PATH.GetTempPath() + @"\" + dtHead.Rows[0]["NO_PO"] + ".txt";
				System.IO.File.WriteAllText(txt, dtTcrs.Rows[0][0].ToString());
				files.Add(txt + "|LOCAL");			
			}

			// 서명
			string sign = "";

			if (dtHead.Rows[0]["CD_AREA"].ToString() == "100")
				sign = MailSign.GetPOrderKR(empName, telNumber, faxNumber, eMail, 파일번호);
			else
				sign = MailSign.GetPOrderEN(empNameEn, telNumber, faxNumber, eMail, "");

			// 메일발송 팝업
			P_CZ_MA_EMAIL_SUB f = new P_CZ_MA_EMAIL_SUB(from, to, cc, "", title, files.ToArray(), null, sign, 파일번호, dtHead.Rows[0]["CD_PARTNER"].ToString(), boAuto);

			if (f.ShowDialog() == DialogResult.OK)
			{
				// 발송에 성공한 경우 수신자, 발송일 업데이트
				query = @"
DECLARE @DT_SEND	NVARCHAR(14) = NEOE.SF_SYSDATE(GETDATE())

UPDATE PU_POH SET
	  YN_SEND   = 'Y'
	, DT_SEND   = @DT_SEND
	, MAIL_SEND = @MAIL_SEND
WHERE 1 = 1
	AND CD_COMPANY = @CD_COMPANY
	AND NO_PO = @NO_PO

SELECT @DT_SEND";

				DBMgr dbm = new DBMgr();
				dbm.Query = query;
				dbm.AddParameter("@CD_COMPANY", 회사코드);
				dbm.AddParameter("@NO_PO"	  , dtHead.Rows[0]["NO_PO"]);
				dbm.AddParameter("@MAIL_SEND" , f.To);
				DataTable dt = dbm.GetDataTable();

				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}
	}
}


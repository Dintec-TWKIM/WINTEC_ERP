using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Linq;

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
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GemBox.Spreadsheet;
using DX;

namespace cz
{
	/* 견적수량 산정시 단가 계산은 UM_AVG 필드로함
	 * 견적이 나오면 UM_QTN으로 넣고 발주수량 산정시 이 필드 활용
	 * 
	 * 
	 * 
	 * 
	 */
	public partial class P_CZ_PU_PO_TOOL_REG : PageBase
	{
		readonly FreeBinding Header = new FreeBinding();
		bool IsHeaderChanged = false;
		bool IsDeleteKey = false;
		int MaxSeq = 0;

		public P_CZ_PU_PO_TOOL_REG()
		{
			InitializeComponent();
			StartUp.Certify(this);
			this.SetConDefault();
		}

		public P_CZ_PU_PO_TOOL_REG(string toolNumber)
		{
			InitializeComponent();
			StartUp.Certify(this);
			this.SetConDefault();
			tbx도구번호.Text = toolNumber;
		}

		#region ==================================================================================================== Initialize

		protected override void InitLoad()
		{
			tbx도구번호.EnterSearch();
			cbo진행상황.DataBind(BASE.Code("CZ_PU00005"), true);
			cbo진행상황.SetEdit(false);

			DataTable dt = new DataTable();
			dt.Columns.Add("CODE");
			dt.Columns.Add("NAME");

			for (int i = 1; i <= 24; i++)
				dt.Rows.Add(i.ToString(), i + "개월");

			cbo수주개월.DataBind(dt, true);

			tbx견적건수F.NumericFormat();
			tbx견적건수T.NumericFormat();
			tbx매입단가F.NumericFormat();
			tbx매입단가T.NumericFormat();

			if (!spc헤드.Panel2Collapsed)
				Btn확장_Click(null, null);

			MainGrids = new FlexGrid[] { grd견적, grd발주, grd주의사항, grd진행불가, grd기부속 };
			
			InitGrid();
			InitEvent();						
		}

		protected override void InitPaint()
		{
			spc헤드.SplitterDistance = spc헤드.Width - 600;

			// 바인딩 초기화
			DataTable dtHead = SQL.GetDataTable("PS_CZ_PU_PO_TOOLH", "‡", "‡");
			DataTable dtLine = SQL.GetDataTable("PS_CZ_PU_PO_TOOLL", "‡", "‡", 0, "");		

			Header.SetBinding(dtHead, lay헤드);
			Header.ClearAndNewRow();
			grd견적.Binding = dtLine;
			grd발주.Binding = SQL.GetDataTable("PS_CZ_PU_PO_TOOLL_PO", "‡", "‡", "‡", 0);

			// 기본값
			dtp작성일자.SetDefault();
			ctx담당자.SetDefault();
			Header.CurrentRow["DT_TOOL"] = dtp작성일자.Text;
			Header.CurrentRow["NO_EMP"] = ctx담당자.CodeValue;

			// 링크모드
			if (tbx도구번호.Text != "")
				OnToolBarSearchButtonClicked(null, null);
			
			// 주의사항 & 진행불가는 바로 조회하기
			grd주의사항.Binding = SQL.GetDataTable("PS_CZ_PU_PO_TOOL_CHK", LoginInfo.CompanyCode, "C");
			grd진행불가.Binding = SQL.GetDataTable("PS_CZ_PU_PO_TOOL_CHK", LoginInfo.CompanyCode, "N");
			grd기부속.Binding = SQL.GetDataTable("PS_CZ_PU_PO_TOOL_EDIT");

			// 왼쪽정렬 해야할 레이블
			lbl첨부파일.TextAlign = ContentAlignment.MiddleLeft;
		}

		protected override bool IsChanged()
		{
			// 헤더변경 or 그리드변경
			if (IsHeaderChanged || base.IsChanged())
				return true;
			else
				return false;
		}

		private void Clear()
		{			
			Header.ClearAndNewRow();
			IsHeaderChanged = false;

			pnl도구번호.SetEdit(true);
			tbx도구번호.Text = "";  // 별도관리
			ctx담당자.SetDefault();
			dtp작성일자.SetDefault();
			cbo수주개월.SetEdit(true);

			lbl첨부파일.Text = "없음";
			lbl첨부파일.Font = new Font(lbl첨부파일.Font.Name, lbl첨부파일.Font.SizeInPoints, FontStyle.Regular);
			lbl첨부파일.ForeColor = Color.Red;

			grd견적.Clear2();
			grd발주.Clear2();
			grd미선정.Clear2();
			grd호선.Clear2();

			Header.CurrentRow["DT_TOOL"] = dtp작성일자.Text;
			Header.CurrentRow["NO_EMP"] = ctx담당자.CodeValue;

			ShowSummary();
		}

		#endregion

		#region ==================================================================================================== Grid

		private void InitGrid()
		{
			// ********** 견적
			InitLineGrid(grd견적);
			grd견적.SetEditColumn("CD_ITEM", "UCODE", "KCODE", "UM_LAST", "QT_Q_INPUT", "QT_Q_CHECK", "SS", "LT", "LOT", "MOQ", "DC_RMK", "DC_RMK_1");

			// ********** 미선정
			InitLineGrid(grd미선정);
			grd견적.SetEditColumn("DC_RMK");
			grd견적.SetEditColumn("DC_RMK_1");

			// ********** 발주
			grd발주.BeginSetting(2, 1, false);
			
			grd발주.SetCol("SEQ"			, "순번"		, 40	, TextAlignEnum.CenterCenter);
			grd발주.SetCol("SEQ_HGS"		, "순번\n(HGS)", 40	, TextAlignEnum.CenterCenter);
			grd발주.SetCol("CD_ITEM"		, "재고코드"	, 70	, TextAlignEnum.CenterCenter);
			grd발주.SetCol("UCODE"		, "U코드"	, 100	, TextAlignEnum.CenterCenter);
			grd발주.SetCol("KCODE"		, "K코드"	, 130	, TextAlignEnum.CenterCenter);
			grd발주.SetCol("CNT"			, "건수"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("NO_PLATE"	, "품번"		, 120);
			grd발주.SetCol("NM_PLATE"	, "품명"		, 200);			
			
			grd발주.SetCol("CNT_QTN"		, "MAX"		, "견적"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_SO"		, "MAX"		, "수주"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			
			grd발주.SetCol("QT_SO_1M"	, "수주"		, "1M"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_SO_XM"	, "수주"		, "xM"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_SO_6M_ACT", "수주"		, "6M*"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_SO_9M_ACT", "수주"		, "9M*"		, 55	, typeof(decimal), FormatTpType.QUANTITY);

			grd발주.SetCol("QT_AVSUM"	, "재고"		, "가용"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_AVST"		, "재고"		, "재고"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_AVPO"		, "재고"		, "발주"		, 55	, typeof(decimal), FormatTpType.QUANTITY);

			grd발주.SetCol("UM_QTN"		, "단가"		, "견적단가"	, 90	, typeof(decimal), FormatTpType.MONEY);
			grd발주.SetCol("UM_AVG"		, "단가"		, "매입단가"	, 90	, typeof(decimal), FormatTpType.MONEY);
			grd발주.SetCol("RT_AVG"		, "단가"		, "비교"		, 45	, typeof(decimal), "#,##0%"	, TextAlignEnum.RightCenter);
			grd발주.SetCol("UM_LAST"		, "단가"		, "최근단가"	, 90	, typeof(decimal), FormatTpType.MONEY);
			grd발주.SetCol("RT_LAST"		, "단가"		, "비교"		, 45	, typeof(decimal), "#,##0%"	, TextAlignEnum.RightCenter);

			grd발주.SetCol("QT_LACK"		, "부족"		, "수량"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("AM_LACK"		, "부족"		, "금액"		, 90	, typeof(decimal), FormatTpType.MONEY);

			grd발주.SetCol("QT_Q_FINAL"	, "수량"		, "견적"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_P_INPUT"	, "수량"		, "예상"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_P_CHECK"	, "수량"		, "검토"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			grd발주.SetCol("QT_P_FINAL"	, "수량"		, "최종"		, 55	, typeof(decimal), FormatTpType.QUANTITY);

			grd발주.SetCol("AM_P_INPUT"	, "금액"		, "예상"		, 90	, typeof(decimal), FormatTpType.MONEY);
			grd발주.SetCol("AM_P_FINAL"	, "금액"		, "최종"		, 90	, typeof(decimal), FormatTpType.MONEY);

			grd발주.SetCol("LT_QTN"		, "납기"		, "HGS"		, 40	, typeof(decimal), FormatTpType.MONEY);
			grd발주.SetCol("LT_MAX"		, "납기"		, "최대"		, 40	, typeof(decimal), FormatTpType.MONEY);
			grd발주.SetCol("LT_LAST"		, "납기"		, "최근"		, 40	, typeof(decimal), FormatTpType.MONEY);

			grd발주.SetCol("DC_RMK"		, "비고"		, 250);
			grd발주.SetCol("DC_RMK_1"	, "비고1"	, 250);
			grd발주.SetCol("DC_RMK_C"	, "주의사항"	, 120);
			grd발주.SetCol("DC_RMK_N"	, "진행불가"	, 120);
			grd발주.SetCol("YN_BAD"		, "악성"		, 40);
			
			grd발주.Cols["CNT_QTN"].SetColor(Color.Red);
			grd발주.Cols["QT_SO"].SetColor(Color.Red);
			grd발주.Cols["QT_AVSUM"].SetColor(Color.Red);
			grd발주.Cols["QT_P_INPUT"].SetColor(Color.Blue);
			grd발주.Cols["QT_P_CHECK"].SetColor(Color.Red);
			
			grd발주.Cols["CNT_QTN"].SetBold(true);
			grd발주.Cols["QT_SO"].SetBold(true);
			grd발주.Cols["QT_AVSUM"].SetBold(true);
			grd발주.Cols["QT_P_INPUT"].SetBold(true);
			grd발주.Cols["QT_P_CHECK"].SetBold(true);

			grd발주.VerifyNotNull = new string[] { "SEQ_HGS" };
			grd발주.SetDefault("22.05.10.01", SumPositionEnum.None);
			grd발주.SetAlternateRow();
			grd발주.SetMalgunGothic();
			grd발주.Rows.DefaultSize = 41;
			grd발주.Styles.Add("FORE_RED").ForeColor = Color.Red;
			grd발주.Styles.Add("BACK_YELLOW").BackColor = Color.Yellow;
			grd발주.SetEditColumn("SEQ_HGS", "CD_ITEM", "UCODE", "KCODE", "UM_LAST", "QT_P_INPUT", "QT_P_CHECK", "DC_RMK", "DC_RMK_1");

			grd발주.Cols["QT_AVST"].Visible = false;
			grd발주.Cols["QT_AVPO"].Visible = false;

			// ********** 호선
			grd호선.BeginSetting(1, 1, false);

			grd호선.SetCol("NO_IMO"		, "IMO번호"	, 90	, TextAlignEnum.CenterCenter);
			grd호선.SetCol("NO_ENGINE"	, "엔진번호"	, 60	, TextAlignEnum.CenterCenter);
            grd호선.SetCol("NM_MODEL"	, "엔진모델" 	, 90	, TextAlignEnum.CenterCenter);
			grd호선.SetCol("CNT"			, "아이템수"	, 60	, TextAlignEnum.CenterCenter);
			grd호선.SetCol("PIC_HGS"		, "담당자"	, 70	, TextAlignEnum.CenterCenter);
			grd호선.SetCol("TEAM_HGS"	, "소속팀"	, 120	, TextAlignEnum.CenterCenter);

			grd호선.SetDefault("20.10.13.03", SumPositionEnum.None);
			grd호선.SetMalgunGothic();

			// ********** 주의사항
			grd주의사항.BeginSetting(1, 1, false);

			grd주의사항.SetCol("SEQ"			, "순번"		, false);
			grd주의사항.SetCol("CD_ITEM"		, "재고코드"	, 90	, TextAlignEnum.CenterCenter);
			grd주의사항.SetCol("UCODE"		, "U코드"	, 100	, TextAlignEnum.CenterCenter);
			grd주의사항.SetCol("KCODE"		, "K코드"	, 120	, TextAlignEnum.CenterCenter);
			grd주의사항.SetCol("NM_ITEM"		, "재고명"	, 250);
			grd주의사항.SetCol("DC_RMK"		, "비고"		, 1000);
			grd주의사항.SetCol("EMP_INSERT"	, "등록자"	, 100	, TextAlignEnum.CenterCenter);
			grd주의사항.SetCol("DTS_INSERT"	, "등록일"	, 140	, typeof(string), "####/##/## ##:##:##");
			grd주의사항.SetCol("EMP_UPDATE"	, "수정자"	, 100	, TextAlignEnum.CenterCenter);
			grd주의사항.SetCol("DTS_UPDATE"	, "수정일"	, 140	, typeof(string), "####/##/## ##:##:##");

			grd주의사항.SetDefault("21.01.27.02", SumPositionEnum.None);
			grd주의사항.SetEditColumn("CD_ITEM", "UCODE", "KCODE", "DC_RMK");			
			//grd주의사항.SetCopyAndPaste("SEQ");
			grd주의사항.SetMalgunGothic();

			// ********** 진행불가
			grd진행불가.BeginSetting(1, 1, false);

			grd진행불가.SetCol("SEQ"			, "순번"		, false);
			grd진행불가.SetCol("CD_ITEM"		, "재고코드"	, 90	, TextAlignEnum.CenterCenter);
			grd진행불가.SetCol("UCODE"		, "U코드"	, 100	, TextAlignEnum.CenterCenter);
			grd진행불가.SetCol("KCODE"		, "K코드"	, 120	, TextAlignEnum.CenterCenter);
			grd진행불가.SetCol("NM_ITEM"		, "재고명"	, 250);
			grd진행불가.SetCol("DC_RMK"		, "비고"		, 1000);
			grd진행불가.SetCol("EMP_INSERT"	, "등록자"	, 100	, TextAlignEnum.CenterCenter);
			grd진행불가.SetCol("DTS_INSERT"	, "등록일"	, 140	, typeof(string), "####/##/## ##:##:##");
			grd진행불가.SetCol("EMP_UPDATE"	, "수정자"	, 100	, TextAlignEnum.CenterCenter);
			grd진행불가.SetCol("DTS_UPDATE"	, "수정일"	, 140	, typeof(string), "####/##/## ##:##:##");

			grd진행불가.SetDefault("21.01.27.02", SumPositionEnum.None);
			grd진행불가.SetEditColumn("CD_ITEM", "UCODE", "KCODE", "DC_RMK");
			//grd진행불가.SetCopyAndPaste("SEQ");	
			grd진행불가.SetMalgunGothic();

			// ********** 기부속 추가
			grd기부속.BeginSetting(1, 1, false);

			grd기부속.SetCol("NO_IMO"	, "IMO번호"	, 80	, TextAlignEnum.CenterCenter);			
			grd기부속.SetCol("NO_HULL"	, "호선번호"	, 120	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("NM_VESSEL"	, "호선명"	, 200	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("NO_ENGINE"	, "엔진번호"	, 70	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("NM_MODEL"	, "엔진모델"	, 120	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("NO_PLATE"	, "품번"		, 120	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("NM_PLATE"	, "품명"		, 200	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("CD_ITEM"	, "재고코드"	, 90	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("UCODE"		, "U코드"	, 100	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("KCODE"		, "K코드"	, 120	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("NM_ITEM"	, "재고명"	, 250);
			grd기부속.SetCol("DC_RMK"	, "비고"		, 200);
			grd기부속.SetCol("EMP_INSERT", "등록자"	, 100	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("DTS_INSERT", "등록일"	, 140	, typeof(string), "####/##/## ##:##:##");
			grd기부속.SetCol("EMP_UPDATE", "수정자"	, 100	, TextAlignEnum.CenterCenter);
			grd기부속.SetCol("DTS_UPDATE", "수정일"	, 140	, typeof(string), "####/##/## ##:##:##");

			grd기부속.SetDefault("21.10.05.03", SumPositionEnum.None);
			grd기부속.SetEditColumn("NO_IMO", "NO_ENGINE", "NO_PLATE", "NM_PLATE", "CD_ITEM", "UCODE", "KCODE", "DC_RMK");
			//grd진행불가.SetCopyAndPaste("SEQ");		
			grd기부속.SetMalgunGothic();
		}

		private void InitLineGrid(FlexGrid flexGrid)
		{
			flexGrid.BeginSetting(2, 1, false);

			flexGrid.SetCol("CHK"			, "S"		, 30	, true, CheckTypeEnum.Y_N);
			flexGrid.SetCol("SEQ"			, "순번"		, 40	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("SEQ_HGS"		, "순번\n(HGS)", 40	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("CD_ITEM"		, "재고코드"	, 70	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("UCODE"			, "U코드"	, 100	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("KCODE"			, "K코드"	, 130	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NM_ITEM"		, "재고명"	, 200);
			flexGrid.SetCol("NO_PLATE"		, "품번"		, 120);

			flexGrid.SetCol("NO_IMO"		, "IMO번호"	, 65	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NO_ENGINE"		, "엔진번호"	, false);
			flexGrid.SetCol("NM_MODEL"		, "엔진모델"	, 75	, TextAlignEnum.CenterCenter);
						
			flexGrid.SetCol("CNT_QTN"		, "건수"		, "견적"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("CNT_SO"		, "건수"		, "수주"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("CNT_RATE"		, "건수"		, "수주율"	, 45	, typeof(decimal), "#,##0%"	, TextAlignEnum.RightCenter);

			flexGrid.SetCol("QT_QTN"		, "수량"		, "견적"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_SO"			, "수량"		, "수주"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_RATE"		, "수량"		, "수주율"	, 45	, typeof(decimal), "#,##0%"	, TextAlignEnum.RightCenter);

			flexGrid.SetCol("QT_SO_1M"		, "수주"		, "1M"		, 50	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_SO_XM"		, "수주"		, "xM"		, 50	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_SO_6M_ACT"	, "수주"		, "6M*"		, 50	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_SO_9M_ACT"	, "수주"		, "9M*"		, 50	, typeof(decimal), FormatTpType.QUANTITY);

			flexGrid.SetCol("QT_AVSUM"		, "재고"		, "가용"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_AVST"		, "재고"		, "재고"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_AVPO"		, "재고"		, "발주"		, 55	, typeof(decimal), FormatTpType.QUANTITY);
			
			flexGrid.SetCol("UM_AVG"		, "단가"		, "매입단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("UM_STK"		, "단가"		, "재고단가"	, 75	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("UM_LAST"		, "단가"		, "최근단가"	, 75	, typeof(decimal), FormatTpType.MONEY);

			flexGrid.SetCol("QT_NEED"		, "수량"		, "필요"		, 50	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_Q_INPUT"	, "수량"		, "예상"		, 50	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_Q_CHECK"	, "수량"		, "검토"		, 50	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("QT_Q_FINAL"	, "수량"		, "최종"		, 50	, typeof(decimal), FormatTpType.QUANTITY);

			flexGrid.SetCol("AM_Q_INPUT"	, "금액"		, "예상"		, 80	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("AM_Q_FINAL"	, "금액"		, "최종"		, 80	, typeof(decimal), FormatTpType.MONEY);

			flexGrid.SetCol("LT_MAX"		, "납기"		, "최대"		, 40	, typeof(decimal), FormatTpType.MONEY);
			flexGrid.SetCol("LT_LAST"		, "납기"		, "최근"		, 40	, typeof(decimal), FormatTpType.MONEY);

			flexGrid.SetCol("LT"			, "L/T\n(DAY)\n30"		, 40	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("SS"			, "S/S\n(MON)\n1"		, 40	, typeof(decimal), FormatTpType.QUANTITY);			
			flexGrid.SetCol("LOT"			, "LOT\n(MON)\n1"		, 40	, typeof(decimal), FormatTpType.QUANTITY);
			flexGrid.SetCol("MOQ"			, "MOQ\n(PCS)\n1"		, 40	, typeof(decimal), FormatTpType.QUANTITY);
						
			flexGrid.SetCol("DC_RMK"		, "비고"		, 250);
			flexGrid.SetCol("DC_RMK_1"		, "비고1"	, 250);
			flexGrid.SetCol("DC_RMK_C"		, "주의사항"	, 250);
			flexGrid.SetCol("DC_RMK_N"		, "진행불가"	, 120);
			
			flexGrid.SetCol("NO_TOOL_OL"	, "중복문의"	, "도구번호"	, 100	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("NM_EMP_OL"		, "중복문의"	, "담당자"	, 70	, TextAlignEnum.CenterCenter);			
			flexGrid.SetCol("YN_BAD"		, "악성"		, 40	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("YN_ME"			, "ME"		, 40	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("YN_HO"			, "HO"		, 40	, TextAlignEnum.CenterCenter);
			flexGrid.SetCol("YN_HE"			, "HE"		, 40	, TextAlignEnum.CenterCenter);

			flexGrid.SetCol("NO_IMO_ME"		, "ME"		, false);
			flexGrid.SetCol("NO_IMO_HO"		, "HO"		, false);
			flexGrid.SetCol("NO_IMO_HE"		, "HE"		, false);
			
			flexGrid.SetCol("YN_MANUAL"		, "요청"		, false);
			flexGrid.SetCol("YN_SEL"		, "선택"		, false);
						
			flexGrid.Cols["CNT_QTN"].SetColor(Color.Red);
			flexGrid.Cols["QT_SO"].SetColor(Color.Red);
			flexGrid.Cols["QT_AVSUM"].SetColor(Color.Red);
			flexGrid.Cols["QT_Q_INPUT"].SetColor(Color.Blue);
			flexGrid.Cols["QT_Q_CHECK"].SetColor(Color.Red);

			flexGrid.Cols["CNT_QTN"].SetBold(true);
			flexGrid.Cols["QT_SO"].SetBold(true);
			flexGrid.Cols["QT_AVSUM"].SetBold(true);
			flexGrid.Cols["QT_Q_INPUT"].SetBold(true);
			flexGrid.Cols["QT_Q_CHECK"].SetBold(true);
			
			flexGrid.SetDefault("22.05.04.01", SumPositionEnum.None);
			flexGrid.SetAlternateRow();
			flexGrid.SetMalgunGothic();
			flexGrid.Rows.DefaultSize = 41;
			flexGrid.Styles.Add("FORE_RED").ForeColor = Color.Red;
			flexGrid.Styles.Add("BACK_YELLOW").BackColor = Color.Yellow;

			flexGrid.Cols["QT_AVST"].Visible = false;
			flexGrid.Cols["QT_AVPO"].Visible = false;
		}
		
		#endregion

		#region ==================================================================================================== Event

		private void InitEvent()
		{
			Header.ControlValueChanged += Header_ControlValueChanged;
			cbo진행상황.SelectedValueChanged += Cbo진행상황_SelectedValueChanged;
			cbo수주개월.SelectedValueChanged += Cbo수주개월_SelectedValueChanged;

			btn빈도분석.Click += Btn빈도분석_Click;
			btn자동제외.Click += Btn자동제외_Click;

			btn메인엔진.Click += Btn메인엔진_Click;
			btn홀레비.Click += Btn홀레비_Click;

			btn호선검색.Click += Btn호선검색_Click;
			btn호선선택.Click += Btn호선선택_Click;
			
			btn견적작성.Click += Btn견적작성_Click;
			btn품목동기화.Click += Btn품목동기화_Click;
			btn빈도동기화.Click += Btn빈도동기화_Click;

			btn검토요청.Click += Btn검토요청_Click;
			btn검토완료.Click += Btn검토완료_Click;

			btnRPA문의.Click += BtnRPA문의_Click;
			btnRPA발주.Click += BtnRPA발주_Click;

			btn맵스용저장.Click += Btn맵스용저장_Click;
			btn견적첨부.Click += Btn견적첨부_Click;
			btn단가입력.Click += Btn단가입력_Click;

			btn추가.Click += Btn추가_Click;
			btn삭제.Click += Btn삭제_Click;
			btn엑셀.Click += Btn엑셀_Click;
			btn확장.Click += Btn확장_Click;

			lbl첨부파일.DoubleClick += Lbl첨부파일_DoubleClick;
			
			grd견적.AfterEdit += Grd견적_AfterEdit;
			grd견적.PreviewKeyDown += Grd견적_PreviewKeyDown;				
			grd견적.KeyDown += Grd견적_KeyDown;
			grd견적.ValidateEdit += delegate (object sender, ValidateEditEventArgs e) { Grd견적_ValidateEdit(sender, e, false, ""); };

			grd발주.AfterEdit += Grd견적_AfterEdit;
			grd발주.PreviewKeyDown += Grd견적_PreviewKeyDown;

			grd견적.DoubleClick += FlexGrid_DoubleClick;
			grd발주.DoubleClick += FlexGrid_DoubleClick;

			cbo엔진모델.QueryBefore += Cbo엔진모델2_QueryBefore;
			cbo엔진모델.QueryAfter += Cbo엔진모델_QueryAfter;





			grd기부속.AfterEdit += Grd기부속_AfterEdit;

		}

		

		private void Grd기부속_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid grid = (FlexGrid)sender;
			string colName = grid.Cols[e.Col].Name;
			
			if (colName == "NO_IMO")
			{
				DataTable dt = CODE.호선(grid[e.Row, e.Col].ToStr());

				if (dt.Rows.Count == 1)
				{
					grid[e.Row, "NO_HULL"] = dt.Rows[0]["NO_HULL"];
					grid[e.Row, "NM_VESSEL"] = dt.Rows[0]["NM_VESSEL"];
				}
				else
				{
					grid[e.Row, "NO_IMO"] = "";
					grid[e.Row, "NO_HULL"] = "";
					grid[e.Row, "NM_VESSEL"] = "";
				}
			}
			else if (colName == "NO_ENGINE")
			{
				DataTable dt = CODE.호선(grid[e.Row, "NO_IMO"].ToStr(), grid[e.Row, e.Col].ToInt());

				if (dt.Rows.Count == 1)
				{
					grid[e.Row, "NM_MODEL"] = dt.Rows[0]["NM_MODEL"];
				}
				else
				{
					grid[e.Row, "NO_ENGINE"] = "";
					grid[e.Row, "NM_MODEL"] = "";
				}
			}
			else if (colName == "CD_ITEM")
			{
				DataTable dt = CODE.품목(grid[e.Row, e.Col].ToStr());

				if (dt.Rows.Count == 1)
				{
					grid[e.Row, "NM_PLATE"] = dt.Rows[0]["NM_ITEM"];
					grid[e.Row, "NM_ITEM"] = dt.Rows[0]["NM_ITEM"];					
				}
				else
				{
					grid[e.Row, "CD_ITEM"] = "";
					grid[e.Row, "NM_ITEM"] = "";
				}				
			}
		}

		private void Cbo엔진모델_QueryAfter(object sender, BpQueryArgs e)
		{
			Header.CurrentRow["CD_MODEL"] = cbo엔진모델.QueryWhereIn_Pipe;
			Header.CurrentRow["NM_MODEL"] = cbo엔진모델.QueryWhereIn_PipeDisplayMember;

			Header_ControlValueChanged(sender, new FreeBindingArgs(Header.CurrentRow, Header.JobMode));
		}

		private void Cbo엔진모델2_QueryBefore(object sender, BpQueryArgs e)
		{
			e.HelpParam.P00_CHILD_MODE = "엔진모델";

			e.HelpParam.P61_CODE1 = @"
	CD_SYSDEF	AS CODE
,	NM_SYSDEF	AS NAME";

			e.HelpParam.P62_CODE2 = @"
V_CZ_MA_CODEDTL WITH(NOLOCK)";

			e.HelpParam.P63_CODE3 = @"
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND CD_FIELD = 'CZ_PU00007'
ORDER BY CD_SYSDEF";
		}

		private void Btn엑셀_Click(object sender, EventArgs e)
		{
			// 엑셀 양식 다운로드
			if (ModifierKeys == Keys.Control)
			{
				FolderBrowserDialog f = new FolderBrowserDialog();

				if (f.ShowDialog() == DialogResult.OK)
				{					
					string fileName = "ExcelForm_Po_Tool_Qt.xlsx";
					Dintec.FILE.Download("shared/CZ/" + fileName, f.SelectedPath + @"\" + fileName);
					return;
				}
			}

			// 엑셀 업로드
			EXCEL excel = new EXCEL();
			
			if (excel.OpenDialog() != DialogResult.OK)
				return;

			UT.ShowPgb("작업이 진행중입니다.");

			excel.HeaderRowIndex = 1;
			excel.StartDataIndex = 4;
			DataTable dt = excel.Read();

			FlexGrid flexGrid = tab메인.SelectedTab == tab견적 ? grd견적 : grd발주;
			string tag = tab메인.SelectedTab == tab견적 ? "Q" : "P";

			flexGrid.Redraw = false;
			List<string> 중복재고 = new List<string>();

			foreach (DataRow excelRow in dt.Rows)
			{
				//for (DataRow findit = flexGrid.DataTable.Select("CD_ITEM = '" + excelRow["CD_ITEM"] + "'").GetFirstRow(); findit != null; findit = null)
				//{
				//	if (excelRow["QT_INPUT"].ToString() != "") findit["QT_" + tag + "_INPUT"] = excelRow["QT_INPUT"];
				//	if (excelRow["QT_CHECK"].ToString() != "") findit["QT_" + tag + "_CHECK"] = excelRow["QT_CHECK"];

				//	int i = flexGrid.FindRow(excelRow["CD_ITEM"], flexGrid.Rows.Fixed, flexGrid.Cols["CD_ITEM"].Index, false);
				//	Grd견적_AfterEdit(flexGrid, new RowColEventArgs(i, flexGrid.Cols["QT_" + tag + "_CHECK"].Index));
				//}

				DataRow[] findit = flexGrid.DataTable.Select("CD_ITEM = '" + excelRow["CD_ITEM"] + "'");

				if (findit.Length > 0)
				{
					foreach (DataRow row in findit)
					{
						if (excelRow["QT_INPUT"].ToString() != "") row["QT_" + tag + "_INPUT"] = excelRow["QT_INPUT"];
						if (excelRow["QT_CHECK"].ToString() != "") row["QT_" + tag + "_CHECK"] = excelRow["QT_CHECK"];

						int i = flexGrid.FindRow(excelRow["CD_ITEM"], flexGrid.Rows.Fixed, flexGrid.Cols["CD_ITEM"].Index, false);
						Grd견적_AfterEdit(flexGrid, new RowColEventArgs(i, flexGrid.Cols["QT_" + tag + "_CHECK"].Index));
					}

					if (findit.Length > 1)
						중복재고.Add(excelRow["CD_ITEM"].ToString());
				}
			}

			if (중복재고.Count > 0)
				유틸.메세지("중복 항목이 존재합니다.\n" + string.Join(", ", 중복재고));

			flexGrid.Redraw = true;
			UT.ClosePgb();
		}

		private void Lbl첨부파일_DoubleClick(object sender, EventArgs e)
		{
			if (lbl첨부파일.Text != "없음")
			{
				string query = @"SELECT NO_TOOL + '/' + NM_FILE FROM CZ_PU_PO_TOOLH WITH(NOLOCK) WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND NO_TOOL = '" + tbx도구번호.Text + "'";
				DataTable dt = SQL.GetDataTable(query);
				Dintec.FILE.Download("UPLOAD/" + PageID + "/" + dt.Rows[0][0], true);
			}
		}
		
		private void Btn견적첨부_Click(object sender, EventArgs e)
		{
			if (!tbx도구번호.Verify(공통메세지._은는필수입력항목입니다, DD("도구번호")))
				return;

			if (!tbx도구번호.ReadOnly)
			{
				UT.ShowMsg("조회된 내역이 없습니다.");
				return;
			}

			EXCEL excel = new EXCEL();
			if (excel.OpenDialog() != DialogResult.OK)
				return;

			string fileName = Dintec.FILE.Upload(excel.FileName, tbx도구번호.Text);
			string query = @"
UPDATE CZ_PU_PO_TOOLH SET
	NM_FILE = '" + fileName + @"'
,	DC_FILE = '" + Dintec.FILE.GetFileName(excel.FileName) + @"'
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND NO_TOOL = '" + tbx도구번호.Text + "'";

			SQL.ExecuteNonQuery(query);

			lbl첨부파일.Text = Dintec.FILE.GetExtension(fileName).ToUpper().Replace(".", "");
			lbl첨부파일.Font = new Font(lbl첨부파일.Font.Name, lbl첨부파일.Font.SizeInPoints, FontStyle.Underline);
			lbl첨부파일.ForeColor = Color.Blue;

			UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
		}


		private void Btn단가입력_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			// 첨부파일 검색
			string query = @"SELECT	NO_TOOL + '/' + NM_FILE	AS NM_FILE FROM CZ_PU_PO_TOOLH WITH(NOLOCK) WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND NO_TOOL = '" + tbx도구번호.Text + "'";
			DataTable dt = SQL.GetDataTable(query);			

			if (dt.Rows[0][0].ToString() == "")
			{
				UT.ShowMsg("등록된 견적서가 없습니다.");
				return;
			}

			// 현재 상태 저장
			if (!SaveTool(sqlDebug))
				return;

			try
			{ 
				// 첨부파일 다운로드
				EXCEL excel = new EXCEL() { FileName = Dintec.FILE.Download("UPLOAD/" + PageID + "/" + dt.Rows[0][0], false)  };
			
				UT.ShowPgb(DD("분석 중 입니다."));
			
				// ********** Line 파싱
				excel.HeaderRowIndex = 11;
				excel.StartDataIndex = 12;
				DataTable dtLine = excel.Read();

				// 컬럼이름 변경 : 현대 MAPS → 딘텍 ERP
				//dtLine.Columns["출력 순서"].ColumnName = "SEQ_HGS";
				dtLine.Columns["PRT. 항번"].ColumnName = "SEQ_HGS";
				dtLine.Columns["PLATE NO"].ColumnName = "NO_PLATE";
				dtLine.Columns["DESCRIPTION"].ColumnName = "NM_PLATE";
				dtLine.Columns["U CODE NO"].ColumnName = "UCODE";
				dtLine.Columns["DEL."].ColumnName = "LT";
				dtLine.Columns["UNIT PRICE"].ColumnName = "UM";

				// 납기 중 STOCK은 10으로 변환
				foreach (DataRow row in dtLine.Rows)
				{
					if (row["LT"].ToString() == "STOCK")
						row["LT"] = "0";
				}
			
				// 단가 업데이트
				SQL sql = new SQL("PX_CZ_PU_PO_TOOL_UM", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
				sql.Parameter.Add2("@NO_TOOL"	, tbx도구번호.Text);
				sql.Parameter.Add2("@XML"		, dtLine.ToXml());
				sql.ExecuteNonQuery();
			
				// 재조회
				OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void BtnRPA문의_Click(object sender, EventArgs e)
		{
			// HGS 순번따기
			SQL.ExecuteNonQuery("PX_CZ_PU_PO_TOOL_SEQ_HGS", SQLDebug.Print, LoginInfo.CompanyCode, tbx도구번호.Text);

			// PDF 보여주기
			Process.Start(MapsPdf());
			
			if (UT.ShowMsg("RPA 견적 문의를 하시겠습니까?", "QY2") == DialogResult.Yes)
			{
				SetStatus("06");
				SaveTool(SQLDebug.None);

				RPA rpa = new RPA() { RpaCode = "MAPS_INQ_ST", FileNumber = tbx도구번호.Text, PartnerCode = "11823" };
				rpa.AddQueue();
				UT.ShowMsg("RPA 문의되었습니다.");
			}
		}

		private void BtnRPA발주_Click(object sender, EventArgs e)
		{			
			if (UT.ShowMsg("RPA 발주 등록을 하시겠습니까?", "QY2") == DialogResult.Yes)
			{
				SetStatus("10");
				SaveTool(SQLDebug.None);

				RPA rpa = new RPA() { RpaCode = "MAPS_PO_ST", FileNumber = tbx도구번호.Text, PartnerCode = "11823" };
				rpa.AddQueue();
				UT.ShowMsg("RPA 발주되었습니다.");
			}
		}

		private void Btn맵스용저장_Click(object sender, EventArgs e)
		{			
			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + tbx도구번호.Text + @"맵스용\";
			DIR.CreateDirectory(desktopPath);

			string fileName = MapsPdf();
			Dintec.FILE.Copy(fileName, desktopPath + Dintec.FILE.GetFileName(fileName));
			
			UT.ShowMsg("바탕화면에 저장되었습니다.");			
		}

		private string MapsPdf()
		{
			DataSet ds = SQL.GetDataSet("PS_CZ_PU_PO_TOOL_MAPS", LoginInfo.CompanyCode, tbx도구번호.Text);
			DataTable dtSummary = ds.Tables[0];
			DataTable dtLine = ds.Tables[1];

			string fileName = Dintec.FILE.Download("shared/cz/P_CZ_PU_PO_TOOL_REG.xlsx", false);
			SpreadsheetInfo.SetLicense("EAAN-UCCU-1F8C-X668");
			ExcelFile excel = ExcelFile.Load(fileName);
			ExcelWorksheet sheet = excel.Worksheets[0];

			// ********** 리마크
			sheet.Cells[0, 0].Value = dtSummary.Rows[0]["SEQ_STR"];

			// **********라인 바인딩
			int row = 2;

			for (int i = 0; i < dtLine.Rows.Count; i++)
			{
				sheet.Rows.InsertEmpty(row);

				sheet.Cells[row, 0].Value = dtLine.Rows[i]["SEQ_HGS"];
				sheet.Cells[row, 1].Value = dtLine.Rows[i]["NO_IMO"];
				sheet.Cells[row, 2].Value = dtLine.Rows[i]["NO_HULL_HGS"];
				sheet.Cells[row, 3].Value = dtLine.Rows[i]["NO_PLATE"];
				sheet.Cells[row, 4].Value = dtLine.Rows[i]["NM_PLATE"];
				sheet.Cells[row, 5].Value = dtLine.Rows[i]["UCODE"];
				sheet.Cells[row, 6].Value = dtLine.Rows[i]["QT_Q_FINAL"];

				sheet.Cells[row, 0].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;
				sheet.Cells[row, 6].Style.HorizontalAlignment = HorizontalAlignmentStyle.Right;

				sheet.Cells[row, 0].Style.NumberFormat = "#,##0";
				sheet.Cells[row, 6].Style.NumberFormat = "#,##0";

				sheet.Rows[row].AutoFit();
				row++;
			}

			// **********라인 바인딩
			row = 1;

			for (int i = 1; i < dtSummary.Rows.Count; i++)
			{
				sheet.Rows.InsertEmpty(row);
				sheet.Cells[row, 0].Value = dtSummary.Rows[i]["SEQ_STR"];				
				sheet.Rows[row].AutoFit();
				row++;
			}

			// 저장
			//excel.Save(PATH.GetTempPath() + "\\" + tbx도구번호.Text + ".xlsx");
			//return PATH.GetTempPath() + "\\" + tbx도구번호.Text + ".xlsx";
			excel.Save(PATH.GetTempPath() + "\\" + tbx도구번호.Text + ".pdf");
			return PATH.GetTempPath() + "\\" + tbx도구번호.Text + ".pdf";
		}

		
		private void Header_ControlValueChanged(object sender, FreeBindingArgs e)
		{
			// 이전값과 현재값이 같을때는 이벤트를 발생시키지 않는다 (포커스만 변경되는 경우에도 발행하므로 골치 아픔)
			string oldValue = (e.Row.RowState == DataRowState.Added) ? "" : e.Row[sender.GetTag(), DataRowVersion.Original].ToString();
			string newValue = e.Row[sender.GetTag(), DataRowVersion.Current].ToString();

			if (oldValue == newValue)
				return;

			// 저장버튼 활성화
				IsHeaderChanged = true;
			ToolBarSaveButtonEnabled = true;
		}

		private void Cbo진행상황_SelectedValueChanged(object sender, EventArgs e)
		{
			Header.CurrentRow["CD_STATUS"] = cbo진행상황.GetValue();
		}

		private void Cbo수주개월_SelectedValueChanged(object sender, EventArgs e)
		{
			//grd견적.Cols["QT_SO_XM"].Caption = cbo수주개월.GetValue() + "M";
			//throw new NotImplementedException();
			if (cbo수주개월.GetValue() == "")
			{
				grd견적[1, "QT_SO_XM"] = "xM";
				grd발주[1, "QT_SO_XM"] = "xM";
			}
			else
			{
				grd견적[1, "QT_SO_XM"] = cbo수주개월.GetValue() + "M";
				grd발주[1, "QT_SO_XM"] = cbo수주개월.GetValue() + "M";
			}
		}


		private void Btn빈도분석_Click(object sender, EventArgs e)
		{
			if (!cbo수주개월.Verify("수주개월을 입력하세요"))
				return;

			UT.ShowPgb("조회중입니다.");

			SQL sql = new SQL("PS_CZ_PU_PO_TOOL_FREQ", SQLType.Procedure, SQLDebug.Popup);
			sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);			
			sql.Parameter.Add2("@SO_MONTH"	, cbo수주개월.GetValue());
			sql.Parameter.Add2("@FREQ"		, cur빈도.DecimalValue);
			sql.Parameter.Add2("@LONG_LT"	, cur장납기.DecimalValue);			
			sql.Parameter.Add2("@EX_NA"		, chk진행불가.GetValue());
			sql.Parameter.Add2("@EX_DUP"	, chk중복문의.GetValue());
			sql.Parameter.Add2("@EX_BAD"	, chk악성재고.GetValue());
			sql.Parameter.Add2("@EX_ME"		, chk메인엔진.GetValue());
			sql.Parameter.Add2("@EX_HO"		, chk홀레비.GetValue());
			sql.Parameter.Add2("@EX_HE"		, chk힘센.GetValue());
			DataTable dt = sql.GetDataTable();

			// 필요한 컬럼 추가
			dt.Columns.Add("CHK"		, typeof(string));
			dt.Columns.Add("SEQ"		, typeof(int));

			dt.Columns.Add("QT_Q_INPUT"	, typeof(decimal));
			dt.Columns.Add("QT_Q_CHECK"	, typeof(decimal));
			dt.Columns.Add("QT_Q_FINAL"	, typeof(decimal));

			dt.Columns.Add("AM_Q_INPUT"	, typeof(decimal));
			dt.Columns.Add("AM_Q_FINAL"	, typeof(decimal));

			dt.Columns.Add("YN_MANUAL"	, typeof(string));
			dt.Columns.Add("YN_SEL"		, typeof(string));
			dt.Columns.Add("DC_RMK"		, typeof(string));
			dt.Columns.Add("DC_RMK_1"	, typeof(string));

			// 추가 상태로 변경
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				dt.Rows[i].SetAdded();
				dt.Rows[i]["CHK"] = "N";
				dt.Rows[i]["SEQ"] = i + 1;
			}			

			grd견적.Binding = dt;
			SetStatus("01");
			cbo수주개월.SetEdit(false);

			UT.ClosePgb();
		}

		private void Btn자동제외_Click(object sender, EventArgs e)
		{
			UT.ShowPgb("삭제중입니다.");

			grd견적.Redraw = false;
			grd견적.DataTable.Delete("ISNULL(DC_RMK_N, '') <> ''");
			grd견적.DataTable.Delete("ISNULL(NO_TOOL_OL, '') <> ''");
			grd견적.DataTable.Delete("YN_BAD = 'Y'");
			grd견적.DataTable.Delete("YN_ME = 'Y'");
			grd견적.DataTable.Delete("YN_HO = 'Y'");
			grd견적.Redraw = true;

			UT.ClosePgb();
		}

		private void Btn메인엔진_Click(object sender, EventArgs e)
		{
			UT.ShowPgb("조회중입니다.");

			// 초기화
			foreach (DataRow row in grd견적.DataTable.Rows) row["YN_ME"] = "";

			// 조회			
			SQL sql = new SQL("PS_CZ_PU_PO_TOOL_ENGINE", SQLType.Procedure);
			sql.Parameter.Add2(new SqlParameter("@KCODE", SqlDbType.Structured) { TypeName = "KCODE", Value = grd견적.DataTable.Select("ISNULL(KCODE, '') <> ''").ToDataTable(false, "KCODE") });
			sql.Parameter.Add2(new SqlParameter("@UCODE", SqlDbType.Structured) { TypeName = "UCODE", Value = grd견적.DataTable.Select("ISNULL(UCODE, '') <> ''").ToDataTable(false, "UCODE") });
			sql.Parameter.Add2("@FLAG", "ME");

			DataSet ds = sql.GetDataSet();
			DataTable dtK = ds.Tables[1];
			DataTable dtU = ds.Tables[0];

			DataRow[] rowK = dtK.Rows.Count > 0 ? grd견적.DataTable.Select("KCODE IN (" + string.Join(",", dtK.AsEnumerable().Select(x => "'" + x["KCODE"] + "'").ToArray()) + ")") : new DataRow[0];
			DataRow[] rowU = dtU.Rows.Count > 0 ? grd견적.DataTable.Select("UCODE IN (" + string.Join(",", dtU.AsEnumerable().Select(x => "'" + x["UCODE"] + "'").ToArray()) + ")") : new DataRow[0];

			foreach (DataRow row in rowK) row["YN_ME"] = "Y";
			foreach (DataRow row in rowU) row["YN_ME"] = "Y";

			UT.ClosePgb();
		}

		private void Btn홀레비_Click(object sender, EventArgs e)
		{
			UT.ShowPgb("조회중입니다.");

			// 초기화
			foreach (DataRow row in grd견적.DataTable.Rows) row["YN_HO"] = "";

			// 조회
			SQL sql = new SQL("PS_CZ_PU_PO_TOOL_ENGINE", SQLType.Procedure);
			sql.Parameter.Add2(new SqlParameter("@KCODE", SqlDbType.Structured) { TypeName = "KCODE", Value = grd견적.DataTable.Select("ISNULL(KCODE, '') <> ''").ToDataTable(false, "KCODE") });
			sql.Parameter.Add2(new SqlParameter("@UCODE", SqlDbType.Structured) { TypeName = "UCODE", Value = grd견적.DataTable.Select("ISNULL(UCODE, '') <> ''").ToDataTable(false, "UCODE") });
			sql.Parameter.Add2("@FLAG", "HO");

			DataSet ds = sql.GetDataSet();
			DataTable dtK = ds.Tables[1];
			DataTable dtU = ds.Tables[0];

			DataRow[] rowK = dtK.Rows.Count > 0 ? grd견적.DataTable.Select("KCODE IN (" + string.Join(",", dtK.AsEnumerable().Select(x => "'" + x["KCODE"] + "'").ToArray()) + ")") : new DataRow[0];
			DataRow[] rowU = dtU.Rows.Count > 0 ? grd견적.DataTable.Select("UCODE IN (" + string.Join(",", dtU.AsEnumerable().Select(x => "'" + x["UCODE"] + "'").ToArray()) + ")") : new DataRow[0];

			foreach (DataRow row in rowK) row["YN_HO"] = "Y";
			foreach (DataRow row in rowU) row["YN_HO"] = "Y";

			UT.ClosePgb();
		}
		
		private void Btn호선검색_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			bool isTest = ModifierKeys == Keys.Control;

			if (!cbo엔진모델.Verify("엔진모델을 입력하세요"))	return;
			if (!grd견적.Verify("견적항목이 없습니다"))			return;

			UT.ShowPgb("조회중입니다.");

			if (spc헤드.Panel2Collapsed)
				Btn확장_Click(null, null);

			// 첫 선택은 무조건 dummy 호선
			if (!isTest && grd견적.DataTable.Select("ISNULL(NO_IMO, '') <> ''").Length == 0)
			{
				SQL sqlDummy = new SQL("PS_CZ_PU_PO_TOOL_HULL_CNT", SQLType.Procedure, sqlDebug);
				sqlDummy.Parameter.Add2(new SqlParameter("@KCODE", SqlDbType.Structured) { TypeName = "KCODE", Value = grd견적.DataTable.Select("ISNULL(NO_IMO, '') = '' AND ISNULL(KCODE, '') <> ''").ToDataTable(false, "KCODE") });
				sqlDummy.Parameter.Add2(new SqlParameter("@UCODE", SqlDbType.Structured) { TypeName = "UCODE", Value = grd견적.DataTable.Select("ISNULL(NO_IMO, '') = '' AND ISNULL(UCODE, '') <> ''").ToDataTable(false, "UCODE") });
				sqlDummy.Parameter.Add2("@CD_MODEL"	, cbo엔진모델.QueryWhereIn_Pipe);
				sqlDummy.Parameter.Add2("@NO_IMO"	, "HGS0001");
				DataTable dtDummy = sqlDummy.GetDataTable();
				grd호선.Binding = dtDummy;
				
				if (grd호선.HasNormalRow)
				{
					if (grd호선["NO_IMO"].ToString() == "HGS0001")
						Btn호선선택_Click(null, null);
				}								
			}

			// 두번째부터 정상검색
			SQL sql = new SQL("PS_CZ_PU_PO_TOOL_HULL_CNT", SQLType.Procedure, sqlDebug);
			sql.Parameter.Add2(new SqlParameter("@KCODE", SqlDbType.Structured) { TypeName = "KCODE", Value = grd견적.DataTable.Select("ISNULL(NO_IMO, '') = '' AND ISNULL(KCODE, '') <> ''").ToDataTable(false, "KCODE") });
			sql.Parameter.Add2(new SqlParameter("@UCODE", SqlDbType.Structured) { TypeName = "UCODE", Value = grd견적.DataTable.Select("ISNULL(NO_IMO, '') = '' AND ISNULL(UCODE, '') <> ''").ToDataTable(false, "UCODE") });
			sql.Parameter.Add2("@CD_MODEL", cbo엔진모델.QueryWhereIn_Pipe);
			DataTable dt = sql.GetDataTable();
			grd호선.Binding = dt;

			UT.ClosePgb();
		}

		private void Btn호선선택_Click(object sender, EventArgs e)
		{			
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			int cnt = 0;

			if (!spc헤드.Panel2Collapsed)
			{ 
				DataRow[] rows = grd견적.DataTable.Select("ISNULL(NO_IMO, '') = '' AND (ISNULL(KCODE, '') <> '' OR ISNULL(UCODE, '') <> '')");

				SQL sql = new SQL("PS_CZ_PU_PO_TOOL_HULL_SEL", SQLType.Procedure, sqlDebug);
				sql.Parameter.Add2("@NO_IMO"	, grd호선["NO_IMO"]);
				sql.Parameter.Add2("@NO_ENGINE"	, grd호선["NO_ENGINE"]);
				sql.Parameter.Add2("@XML"		, rows.ToDataTable().ToXml("KCODE", "UCODE"));
				DataTable dt = sql.GetDataTable();

				foreach (DataRow row in rows)
				{
					DataRow findit = dt.Select("KCODE = '" + row["KCODE"] + "' AND UCODE = '" + row["UCODE"] + "'").GetFirstRow();

					if (findit != null)
					{
						row["NO_IMO"] = grd호선["NO_IMO"];
						row["NO_ENGINE"] = grd호선["NO_ENGINE"];
						row["NO_PLATE"] = findit["NO_PLATE"];
						cnt++;
					}
				}

				webBrowser1.Add("IMO : " + grd호선["NO_IMO"] + " / " + cnt + " 종 추가<br />");
			}
			else
			{
				UT.ShowPgb("조회중입니다.");
				
				foreach (DataRow row in grd견적.DataTable.Select("CHK = 'Y'"))
				{
					SQL sql = new SQL("PS_CZ_PU_PO_TOOL_HULL_SEL_SGL", SQLType.Procedure, sqlDebug);
					sql.Parameter.Add2("@CD_ITEM"	, row["CD_ITEM"]);
					sql.Parameter.Add2("@UCODE"		, row["UCODE"]);
					sql.Parameter.Add2("@KCODE"		, row["KCODE"]);
					sql.Parameter.Add2("@CD_MODEL"	, cbo엔진모델.QueryWhereIn_Pipe);
					sql.Parameter.Add2("@NO_IMOS"	, string.Join(",", grd견적.DataTable.AsEnumerable().Where(r => r["CHK"].ToString() == "N").Select(r => r["NO_IMO"]).Distinct().ToArray()));
					DataTable dt = sql.GetDataTable();

					if (dt.Rows.Count > 0)
					{
						row["NO_IMO"] = dt.Rows[0]["NO_IMO"];
						row["NO_ENGINE"] = dt.Rows[0]["NO_ENGINE"];
						row["NM_MODEL"] = dt.Rows[0]["NM_MODEL"];
						row["NO_PLATE"] = dt.Rows[0]["NO_PLATE"];
						cnt = 1;

						webBrowser1.Add("IMO : " + row["NO_IMO"] + " / " + cnt + " 종 추가<br />");
					}
				}

				UT.ClosePgb();
			}
			
			ShowSummary();
			SetStatus("02");
		}

		

		private void Btn견적작성_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;
			DataRow[] rows = grd견적.DataTable.Select("ISNULL(NO_IMO, '') <> ''");

			if (rows.Length == 0)
			{
				UT.ShowMsg("선택된 아이템이 없습니다.");
				return;
			}

			try
			{
				UT.ShowPgb("저장중입니다.");

				// 전체를 N으로 만들고 선택된것만 Y로 만들기
				foreach (DataRow r in grd견적.DataTable.Rows)
					r["YN_SEL"] = "N";

				foreach (DataRow r in rows)
					r["YN_SEL"] = "Y";

				SetStatus("03");
				SaveTool(sqlDebug);
				OnToolBarSearchButtonClicked(null, null);

				UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}			
		}

		private void Btn품목동기화_Click(object sender, EventArgs e)
		{
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			try
			{
				UT.ShowPgb("저장중입니다.");

				// 일단 현재상태 저장
				if (!SaveTool(sqlDebug))
					return;

				// 재고코드 동기화 및 재조회
				//SQL.ExecuteNonQuery("PX_CZ_PU_PO_TOOL_SYNC", sqlDebug, LoginInfo.CompanyCode, tbx도구번호.Text, grd견적.DataTable.Select("ISNULL(CD_ITEM, '') = ''").ToXml("SEQ", "KCODE", "UCODE"));
				for (int i = grd견적.Rows.Fixed; i < grd견적.Rows.Count; i++)
				{
					if (grd견적[i, "CHK"].ToString() == "Y")
					{
						// ********** 재고코드 찾기
						string query = @"
SELECT
    CD_ITEM
FROM
(
	SELECT
		CD_ITEM
	,	UCODE
	FROM V_CZ_MA_PITEM_HGS

	UNION

	SELECT
		CD_ITEM
	,	UCODE = VALUE
	FROM V_CZ_MA_PITEM_HGS
	CROSS APPLY STRING_SPLIT(UCODE2, ',')
) AS A
WHERE UCODE != '' AND UCODE = '" + grd견적[i, "UCODE"] + @"'
						
UNION						
SELECT CD_ITEM FROM V_CZ_MA_PITEM_HGS WHERE KCODE != '' AND KCODE = '" + grd견적[i, "KCODE"] + @"'
UNION
SELECT CD_ITEM FROM V_CZ_MA_ALTERNATIVE_ITEM WHERE KCODE != '' AND KCODE = '" + grd견적[i, "KCODE"] + "'";

						DataTable dtCode = SQL.GetDataTable(query);

						if (dtCode.Rows.Count == 1)
						{
							ValidateEditEventArgs eventArgs = new ValidateEditEventArgs(i, grd견적.Cols["CD_ITEM"].Index, CheckEnum.None);
							Grd견적_ValidateEdit(null, eventArgs, true, dtCode.Rows[0]["CD_ITEM"].ToString());
						}
					}
				}

				//OnToolBarSearchButtonClicked(null, null);
				UT.ShowMsg("작업이 완료되었습니다.");

			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void Btn빈도동기화_Click(object sender, EventArgs e)
		{
			//throw new NotImplementedException();

			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			try
			{
				UT.ShowPgb("저장중입니다.");

				// 일단 현재상태 저장
				if (!SaveTool(sqlDebug))
					return;

				//디비.실행("", "PX_CZ_PU_PO_TOOL_FREQ", )				
				SQL.ExecuteNonQuery("PX_CZ_PU_PO_TOOL_FREQ", sqlDebug, LoginInfo.CompanyCode, tbx도구번호.Text);

				UT.ShowMsg("작업이 완료되었습니다.");

			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private void Btn검토요청_Click(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab견적 || tab메인.SelectedTab == tab발주)
			{
				string statusCode = tab메인.SelectedTab == tab견적 ? "04" : "08";
				SetStatus(statusCode);
				SaveTool(SQLDebug.Popup);

				string query = "SELECT CD_SYSDEF FROM V_CZ_MA_CODEDTL WITH(NOLOCK) WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND CD_FIELD = 'CZ_PU00006'";
				string[] recipients = SQL.GetDataTable(query).ToArray<string>("CD_SYSDEF");

				if (LoginInfo.UserID == "S-343")
					recipients = new string[] { "S-343" };

				string msg = @"[재고발주도구 알림]

** " + cbo진행상황.GetText() + @"

도구번호 : " + tbx도구번호.Text + @"

담당자 : " + ctx담당자.CodeName + @"

엔진모델 : " + cbo엔진모델.QueryWhereIn_WithDisplayMember.Replace("'", "") + @"

검토요청 부탁드립니다.";

				MSG.SendMSG(LoginInfo.EmployeeNo, recipients, msg);
				UT.ShowMsg("쪽지 발송이 완료되었습니다.");
			}
		}

		private void Btn검토완료_Click(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab견적 || tab메인.SelectedTab == tab발주)
			{
				string statusCode = tab메인.SelectedTab == tab견적 ? "05" : "09";
				SetStatus(statusCode);
				SaveTool(SQLDebug.Popup);

				string query = "SELECT CD_SYSDEF FROM V_CZ_MA_CODEDTL WITH(NOLOCK) WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND CD_FIELD = 'CZ_PU00006'";
				string[] recipients = SQL.GetDataTable(query).ToArray<string>("CD_SYSDEF");

				if (LoginInfo.UserID == "S-343")
					recipients = new string[] { "S-343" };

				string msg = @"[재고발주도구 알림]

** " + cbo진행상황.GetText() + @"

도구번호 : " + tbx도구번호.Text + @"

담당자 : " + ctx담당자.CodeName + @"

엔진모델 : " + cbo엔진모델.QueryWhereIn_WithDisplayMember.Replace("'", "") + @"

검토완료 되었습니다.";

				MSG.SendMSG(LoginInfo.EmployeeNo, recipients, msg);
				UT.ShowMsg("쪽지 발송이 완료되었습니다.");
			}
		}

		private void Btn추가_Click(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab견적)
			{
				grd견적.Rows.Add();
				grd견적.Row = grd견적.Rows.Count - 1;
				grd견적["CHK"] = "N";
				grd견적["SEQ"] = GetMaxSeq(grd견적) + 1;
				grd견적["CD_ITEM"] = "";	// NULL 로는 저장이 안되므로 공백으로 초기화함
				grd견적["KCODE"] = "";
				grd견적["UCODE"] = "";
				grd견적["NO_TOOL_OL"] = "";
				grd견적["NO_EMP_OL"] = "";
				grd견적["YN_MANUAL"] = "Y";

				// 견적작성 이후 상태에서 추가할 경우 선택필드를 Y로 해줌
				if (cbo진행상황.GetValue().ToInt() >= ("03").ToInt())
					grd견적["YN_SEL"] = "Y";

				grd견적.AddFinished();
			}
			if (tab메인.SelectedTab == tab발주)
			{
				grd발주.Rows.Add();
				grd발주.Row = grd발주.Rows.Count - 1;
				grd발주["CHK"] = "N";
				grd발주["SEQ"] = GetMaxSeq(grd발주) + 1;
				grd발주["CD_ITEM"] = "";  // NULL 로는 저장이 안되므로 공백으로 초기화함
				grd발주["KCODE"] = "";
				grd발주["UCODE"] = "";
				grd발주["NO_TOOL_OL"] = "";
				grd발주["NO_EMP_OL"] = "";
				grd발주["YN_MANUAL"] = "Y";

				// 견적작성 이후 상태에서 추가할 경우 선택필드를 Y로 해줌
				if (cbo진행상황.GetValue().ToInt() >= ("03").ToInt())
					grd발주["YN_SEL"] = "Y";

				grd발주.AddFinished();
			}
			else if (tab메인.SelectedTab == tab주의사항)
			{
				grd주의사항.Rows.Add();
				grd주의사항.Row = grd주의사항.Rows.Count - 1;
				grd주의사항["SEQ"] = (int)grd주의사항.Aggregate(AggregateEnum.Max, "SEQ") + 1;
				grd주의사항.AddFinished();
			}
			else if (tab메인.SelectedTab == tab진행불가)
			{
				grd진행불가.Rows.Add();
				grd진행불가.Row = grd진행불가.Rows.Count - 1;
				grd진행불가["SEQ"] = (int)grd진행불가.Aggregate(AggregateEnum.Max, "SEQ") + 1;
				grd진행불가.AddFinished();
			}
			else if (tab메인.SelectedTab == tab기부속)
			{
				grd기부속.Rows.Add();
				grd기부속.Row = grd기부속.Rows.Count - 1;
				grd기부속.AddFinished();
			}
		}

		private int GetMaxSeq(FlexGrid flexGrid)
		{
			return MaxSeq > (int)flexGrid.Aggregate(AggregateEnum.Max, "SEQ") ? MaxSeq : (int)flexGrid.Aggregate(AggregateEnum.Max, "SEQ");
			//string query = "SELECT MAX(SEQ) FROM CZ_PU_PO_TOOLL WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND NO_TOOL = '" + tbx도구번호.Text + "'";
			//return SQL.GetDataTable(query).Rows[0][0].ToInt();
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			if (tab메인.SelectedTab == tab견적)
			{
				UT.ShowPgb("삭제중입니다.");
				grd견적.Redraw = false;
				grd견적.DataTable.Delete("CHK = 'Y'");
				grd견적.Redraw = true;
				ShowSummary();
				UT.ClosePgb();
			}
			if (tab메인.SelectedTab == tab발주)
			{
				UT.ShowPgb("삭제중입니다.");
				grd발주.Redraw = false;
				grd발주.Rows.Remove(grd발주.Row);
				grd발주.Redraw = true;
				ShowSummary();
				UT.ClosePgb();
			}
			else if (tab메인.SelectedTab == tab주의사항)
			{
				if (grd주의사항.Row < grd주의사항.Rows.Fixed)
				{
					UT.ShowMsg("아이템이 선택되지 않았습니다.");
					return;
				}

				grd주의사항.Rows.Remove(grd주의사항.Row);
			}
			else if (tab메인.SelectedTab == tab진행불가)
			{
				if (grd진행불가.Row < grd진행불가.Rows.Fixed)
				{
					UT.ShowMsg("아이템이 선택되지 않았습니다.");
					return;
				}

				grd진행불가.Rows.Remove(grd진행불가.Row);
			}
			else if (tab메인.SelectedTab == tab기부속)
			{
				if (grd기부속.Row < grd기부속.Rows.Fixed)
				{
					UT.ShowMsg("아이템이 선택되지 않았습니다.");
					return;
				}

				grd기부속.Rows.Remove(grd기부속.Row);
			}
		}

		private void Btn확장_Click(object sender, EventArgs e)
		{
			if (spc헤드.Panel2Collapsed)
			{
				// 접혀 있으면 펼치기
				spc헤드.Panel2Collapsed = false;
				spc헤드.Panel2.Show();
				btn확장.Text = ">";
			}
			else
			{
				// 펼쳐져 있으면 접기
				spc헤드.Panel2Collapsed = true;
				spc헤드.Panel2.Hide();
				btn확장.Text = "<";
			}
		}

		private void Grd견적_AfterEdit(object sender, RowColEventArgs e)
		{
			// 다른데서 호출할수도 있으므로 행 값을 정확히 명시해 주어야함
			FlexGrid flexGrid = (FlexGrid)sender;
			string colName = flexGrid.Cols[e.Col].Name;

			if (colName.StartsWith("QT"))
			{
				string tag = flexGrid == grd견적 ? "Q" : "P";
				string umCol = flexGrid == grd견적 ? "UM_AVG" : "UM_QTN";

				// [검토수량] Delete 키를 누른 경우는 셀의 값을 없앰
				if (IsDeleteKey)
				{
					flexGrid.GetDataRow(flexGrid.Row)[colName] = DBNull.Value;
					IsDeleteKey = false;
				}

				// 최종수량 결정
				if (flexGrid[e.Row, "QT_" + tag + "_INPUT"].ToString() == "" && flexGrid[e.Row, "QT_" + tag + "_CHECK"].ToString() == "")
					flexGrid.GetDataRow(e.Row)["QT_" + tag + "_FINAL"] = DBNull.Value;
				else
					flexGrid[e.Row, "QT_" + tag + "_FINAL"] = flexGrid[e.Row, "QT_" + tag + "_CHECK"].ToString() == "" ? flexGrid[e.Row, "QT_" + tag + "_INPUT"] : flexGrid[e.Row, "QT_" + tag + "_CHECK"];

				// 견적금액 계산
				foreach (string s in new string[] { "INPUT", "FINAL" })
				{
					if (flexGrid[e.Row, "QT_" + tag + "_" + s].ToString() == "")
						flexGrid.GetDataRow(e.Row)["AM_" + tag + "_" + s] = DBNull.Value;
					else
						flexGrid[e.Row, "AM_" + tag + "_" + s] = flexGrid[e.Row, "QT_" + tag + "_" + s].ToInt() * flexGrid[e.Row, umCol].ToDecimal();
				}

				// 합계
				ShowSummary();
			}
			
			else if (colName == "LT")
			{
				if (flexGrid[e.Row, colName].ToInt() > 30)
					flexGrid.SetCellStyle(e.Row, e.Col, "BACK_YELLOW");
				else
				{
					flexGrid.SetCellStyle(e.Row, e.Col, "");
					flexGrid.GetDataRow(e.Row)[colName] = DBNull.Value;
				}
			}
			else if (colName.In("SS", "LOT", "MOQ"))
			{
				if (flexGrid[e.Row, colName].ToInt() > 1)
					flexGrid.SetCellStyle(e.Row, e.Col, "BACK_YELLOW");
				else
				{
					flexGrid.SetCellStyle(e.Row, e.Col, "");
					flexGrid.GetDataRow(e.Row)[colName] = DBNull.Value;
				}
			}
		}		

		private void Grd견적_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			if (flexGrid.Cols[flexGrid.Col].AllowEditing && new Regex("QT_[QP]_CHECK").IsMatch(flexGrid.Cols[flexGrid.Col].Name) && e.KeyData == Keys.Delete)
				IsDeleteKey = true;
		}

		private void Grd견적_KeyDown(object sender, KeyEventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			if (e.KeyData == (Keys.Control | Keys.V))
			{
				// 시작컬럼이 에딧 안되는 컬럼이면 걍 종료
				if (!flexGrid.Cols[flexGrid.Col].AllowEditing)
					return;

				// 클립보드 데이터 가져오기
				string[,] clipboard = Util.GetClipboardValues();

				// 1행만 오면 암것도 안함
				if (clipboard.GetLength(0) == 1)
					return;

				// 시작
				try
				{
					flexGrid.Redraw = false;
					UT.ShowPgb("작업중입니다.");
					int startRow = flexGrid.Row;
					int startCol = flexGrid.Col;

					for (int i = 0; i < clipboard.GetLength(0); i++)
					{
						int row = i + startRow;

						// 마지막행을 넘어가면 행 추가
						if (row > flexGrid.Rows.Count - 1)
							flexGrid.Rows.Add();

						for (int j = 0; j < clipboard.GetLength(1); j++)
						{
							int col = j + startCol;

							// 에딧 가능 컬럼일때까지 스캔
							for (; col < flexGrid.Cols.Count; col++)
							{
								if (flexGrid.Cols[col].AllowEditing)
									break;
							}

							// 마지막 컬럼을 넘어가면 스톱
							if (col > flexGrid.Cols.Count)
								break;

							// 바인딩
							flexGrid["CHK"] = "N";
							flexGrid["SEQ"] = GetMaxSeq(flexGrid) + 1;
							flexGrid["CD_ITEM"] = "";
							flexGrid["KCODE"] = "";
							flexGrid["UCODE"] = "";
							flexGrid["YN_MANUAL"] = "Y";
							flexGrid[row, col] = clipboard[i, j];

							// 이벤트 발생
							ValidateEditEventArgs eventArgs = new ValidateEditEventArgs(row, col, CheckEnum.None);
							Grd견적_ValidateEdit(null, eventArgs, true, clipboard[i, j]);
						}
					}

					UT.ClosePgb();
					flexGrid.Redraw = true;
				}
				catch (Exception ex)
				{
					UT.ShowMsg(ex);
				}
			}
		}

		private void Grd견적_ValidateEdit(object sender, ValidateEditEventArgs e, bool ctrlcv, string editData)
		{			
			string colName = grd견적.Cols[e.Col].Name;
			string oldValue = grd견적.GetData(e.Row, colName).ToString2();
			string newValue = !ctrlcv ? grd견적.EditData : editData;
			
			if (colName == "CD_ITEM")
			{
				newValue = newValue.ToEnglish().ToUpper();
				grd견적[e.Row, colName] = newValue;
				
				if (newValue == "")
				{
					for (int j = 1; j < grd견적.Cols.Count; j++)
						grd견적[e.Row, j] = DBNull.Value;
				}
				else
				{
					// 1개 있는지 아닌지 확인
					string query = @"
SELECT
	CD_ITEM
,	KCODE
,	UCODE
FROM V_CZ_MA_PITEM_HGS WITH(NOLOCK)
WHERE 1 = 1
	AND CD_COMPANY = '" + LoginInfo.CompanyCode + @"'
	AND CD_ITEM = '" + newValue + @"'";

					if (SQL.GetDataTable(query).Rows.Count == 1)
					{
						// 빈도 가져오기
						SQL sql = new SQL("PS_CZ_PU_PO_TOOL_FREQ_SGL", SQLType.Procedure, SQLDebug.Print);
						sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
						sql.Parameter.Add2("@CD_ITEM"	, newValue);
						DataTable dt = sql.GetDataTable();

						for (int j = 1; j < grd견적.Cols.Count; j++)
						{
							for (string s = grd견적.Cols[j].Name; dt.Columns.Contains(s); s = "")
							{
								if (s.In("UCODE", "KCODE"))
								{
									// 기존 데이터가 있을 경우 덮어쓰기 방지
									if (grd견적[e.Row, s].문자() == "")
										grd견적[e.Row, s] = dt.Rows[0][s];
								}
								else
									grd견적[e.Row, s] = dt.Rows[0][s];
							}
						}

						grd견적.SetCellStyle(e.Row, grd견적.Cols["CD_ITEM"].Index, "FORE_RED");
					}
					else
					{
						e.Cancel = true;
						grd견적[e.Row, colName] = oldValue;
						grd견적.Focus();
					}
				}
			}
			else if (colName.In("UCODE", "KCODE"))
			{
				string query = "";

				// ********** U코드, K코드에 해당하는 재고코드가 있는지 확인
				if (colName == "UCODE")
				{
					query = @"
SELECT
    CD_ITEM
FROM
(
	SELECT
		CD_ITEM
	,	UCODE
	FROM V_CZ_MA_PITEM_HGS

	UNION

	SELECT
		CD_ITEM
	,	UCODE = VALUE
	FROM V_CZ_MA_PITEM_HGS
	CROSS APPLY STRING_SPLIT(UCODE2, ',')
) AS A
WHERE UCODE = '" + newValue + "'";
				}
				else
				{
					query = @"
SELECT CD_ITEM FROM V_CZ_MA_PITEM_HGS WHERE KCODE = '" + newValue + @"'
UNION
SELECT CD_ITEM FROM V_CZ_MA_ALTERNATIVE_ITEM WHERE KCODE = '" + newValue + "'";
				}

				DataTable dtCode = SQL.GetDataTable(query);

				// 재고코드를 찾았으면 수행
				if (dtCode.Rows.Count == 1)
				{
					ValidateEditEventArgs eventArgs = new ValidateEditEventArgs(e.Row, grd견적.Cols["CD_ITEM"].Index, CheckEnum.None);
					Grd견적_ValidateEdit(null, eventArgs, true, dtCode.Rows[0]["CD_ITEM"].ToString());
				}
				// 못찾았으면 요기로
				else
				{
					// ********** 빈도 가져오기
					SQL sql = new SQL("PS_CZ_PU_PO_TOOL_FREQ_SGL", SQLType.Procedure, SQLDebug.Print);
					sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
					sql.Parameter.Add2("@" + colName, newValue);
					DataTable dt = sql.GetDataTable();

					for (int j = 1; j < grd견적.Cols.Count; j++)
					{
						for (string s = grd견적.Cols[j].Name; dt.Columns.Contains(s); s = "")
							grd견적[e.Row, s] = dt.Rows[0][s];
					}
				}

				grd견적.SetCellStyle(e.Row, grd견적.Cols[colName].Index, "FORE_RED");				
			}
		}

		private void FlexGrid_DoubleClick(object sender, EventArgs e)
		{
			FlexGrid flexGrid = (FlexGrid)sender;

			// 헤더클릭
			if (flexGrid.MouseRow < flexGrid.Rows.Fixed)
			{
				SetGridStyle(flexGrid);
				return;
			}
		}

		#endregion

		#region ==================================================================================================== Search		

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{			
			base.OnToolBarSearchButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			UT.ShowPgb("조회중입니다.");

			// ********** 주의사항 & 진행불가 (견적에 관계없이 바인딩 우선 해줌, 항상 조회하는 내용이니까)
			grd주의사항.Binding = SQL.GetDataTable("PS_CZ_PU_PO_TOOL_CHK", LoginInfo.CompanyCode, "C");
			grd진행불가.Binding = SQL.GetDataTable("PS_CZ_PU_PO_TOOL_CHK", LoginInfo.CompanyCode, "N");

			// ********** 기부속
			grd기부속.Binding = SQL.GetDataTable("PS_CZ_PU_PO_TOOL_EDIT");

			// ********** 도구 헤더 (견적, 발주 공통)
			if (tbx도구번호.Text.Trim() != "")
			{
				if (tbx도구번호.Text.Length < 5)
				{
					string query = @"
DECLARE @NO_TOOL NVARCHAR(20) = '" + tbx도구번호.Text + @"'
SELECT TOP 1 LEFT(NO_TOOL, LEN(NO_TOOL) - LEN(@NO_TOOL)) + @NO_TOOL FROM CZ_PU_PO_TOOLH WITH(NOLOCK) WHERE NO_TOOL LIKE 'ST%' ORDER BY DTS_INSERT DESC";
					tbx도구번호.Text = SQL.GetDataTable(query).Rows[0][0].ToString();
				}
				
				// ********** 헤더
				DataTable dtHead = SQL.GetDataTable("PS_CZ_PU_PO_TOOLH", LoginInfo.CompanyCode, tbx도구번호.Text);

				if (dtHead.Rows.Count == 0)
				{
					UT.ShowMsg(공통메세지.선택된자료가없습니다);
					Clear();
					tbx도구번호.Focus();
					return;
				}
				
				Header.SetDataTable(dtHead);
				tbx포커스.Focus();
				pnl도구번호.SetEdit(false);
				cbo수주개월.SetEdit(false);
				ToolBarDeleteButtonEnabled = true;

				// 엔진모델 수동 바인딩 (얜 자동 안됨)
				cbo엔진모델.Clear();
				cbo엔진모델.AddItem2(dtHead.Rows[0]["CD_MODEL"].ToString(), dtHead.Rows[0]["NM_MODEL"].ToString());

				// 첨부파일 별도 바인딩
				if (dtHead.Rows[0]["DC_FILE"].ToString() == "")
				{
					lbl첨부파일.Text = "없음";
					lbl첨부파일.Font = new Font(lbl첨부파일.Font.Name, lbl첨부파일.Font.SizeInPoints, FontStyle.Regular);
					lbl첨부파일.ForeColor = Color.Red;
				}
				else
				{
					lbl첨부파일.Text = Dintec.FILE.GetExtension(dtHead.Rows[0]["DC_FILE"].ToString()).ToUpper().Replace(".", "");
					lbl첨부파일.Font = new Font(lbl첨부파일.Font.Name, lbl첨부파일.Font.SizeInPoints, FontStyle.Underline);
					lbl첨부파일.ForeColor = Color.Blue;
				}

				// ********** 라인
				if (tab메인.SelectedTab == tab견적)
				{
					SQL sql = new SQL("PS_CZ_PU_PO_TOOLL", SQLType.Procedure, sqlDebug);
					sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
					sql.Parameter.Add2("@NO_TOOL"	, tbx도구번호.Text);
					sql.Parameter.Add2("@SO_MONTH"	, cbo수주개월.GetValue());
					sql.Parameter.Add2("@YN_SEL"	, "Y");					
					sql.Parameter.Add2("@CNT_QTN_F"	, tbx견적건수F.Text);
					sql.Parameter.Add2("@CNT_QTN_T"	, tbx견적건수T.Text);
					sql.Parameter.Add2("@UM_AVG_F"	, tbx매입단가F.Text);
					sql.Parameter.Add2("@UM_AVG_T"	, tbx매입단가T.Text);
					DataTable dtLine = sql.GetDataTable();

					grd견적.Binding = dtLine;
					SetGridStyle(grd견적);
					ShowSummary();

					// SEQ MAX값을 넣어줌, 그래야 추가할때 MAX값 가져올 수 있음
					string query = "SELECT MAX(SEQ) FROM CZ_PU_PO_TOOLL WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND NO_TOOL = '" + tbx도구번호.Text + "'";
					MaxSeq = SQL.GetDataTable(query).Rows[0][0].ToInt();

					// 임시(IMO에디트 할수있도록)
					query = "SELECT 1 FROM CZ_PU_PO_TOOL_TEMP WHERE NO_TOOL = '" + tbx도구번호.Text + "' AND YN_EDIT = 'Y'";
					if (SQL.GetDataTable(query).Rows.Count == 1)
						grd견적.SetEditColumn("CD_ITEM", "UCODE", "KCODE", "NO_PLATE", "NO_IMO", "NO_ENGINE", "QT_Q_INPUT", "QT_Q_CHECK", "SS", "LT", "LOT", "MOQ", "DC_RMK", "DC_RMK_1");

					// ********** 미선정
					grd미선정.Binding = SQL.GetDataTable("PS_CZ_PU_PO_TOOLL", LoginInfo.CompanyCode, tbx도구번호.Text, cbo수주개월.GetValue(), "N");					
				}
				else if (tab메인.SelectedTab == tab발주)
				{
					SQL sql = new SQL("PS_CZ_PU_PO_TOOLL_PO", SQLType.Procedure, sqlDebug);
					sql.Parameter.Add2("@CD_COMPANY", LoginInfo.CompanyCode);
					sql.Parameter.Add2("@NO_TOOL"	, tbx도구번호.Text);
					//sql.Parameter.Add2("@CD_ENGINE"	, cbo엔진모델2.GetValue("CD_FLAG2"));
					sql.Parameter.Add2("@CNT_QTN_F"	, tbx견적건수F.Text);
					sql.Parameter.Add2("@CNT_QTN_T"	, tbx견적건수T.Text);
					sql.Parameter.Add2("@UM_AVG_F"	, tbx매입단가F.Text);
					sql.Parameter.Add2("@UM_AVG_T"	, tbx매입단가T.Text);
					sql.Parameter.Add2("@CHK_CODE"	, chk코드변경.GetValue());
					DataTable dtLine = sql.GetDataTable();

					grd발주.Binding = dtLine;
					SetGridStyle(grd발주);
					ShowSummary();

					// SEQ MAX값을 넣어줌, 그래야 추가할때 MAX값 가져올 수 있음
					string query = "SELECT MAX(SEQ) FROM CZ_PU_PO_TOOLL WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND NO_TOOL = '" + tbx도구번호.Text + "'";
					MaxSeq = SQL.GetDataTable(query).Rows[0][0].ToInt();
				}
			}

			UT.ClosePgb();
		}

		#endregion

		#region ==================================================================================================== Add

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarAddButtonClicked(sender, e);

			if (!MsgAndSave(PageActionMode.Search))
				return;

			Clear();
		}

		#endregion

		#region ==================================================================================================== Save
		
		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarSaveButtonClicked(sender, e);
			SQLDebug sqlDebug = ModifierKeys == (Keys.Control | Keys.Alt) ? SQLDebug.Popup : SQLDebug.Print;

			try
			{
				UT.ShowPgb("저장중입니다.");

				// 그리드 검사	
				if (!base.Verify())
					return;
				
				// ********** 주의사항탭
				if (!CheckDup(grd주의사항))
					return;
				
				for (DataTable dt = grd주의사항.GetChanges(); dt != null; dt = null)
				{
					dt.Columns.Add("CD_CHK", typeof(string), "'C'");
					SQL.ExecuteNonQuery("PX_CZ_PU_PO_TOOL_CHK", sqlDebug, dt.ToXml());
					grd주의사항.AcceptChanges();
				}

				// ********** 진행불가탭
				if (!CheckDup(grd진행불가))
					return;

				for (DataTable dt = grd진행불가.GetChanges(); dt != null; dt = null)
				{
					dt.Columns.Add("CD_CHK", typeof(string), "'N'");
					SQL.ExecuteNonQuery("PX_CZ_PU_PO_TOOL_CHK", sqlDebug, dt.ToXml());
					grd진행불가.AcceptChanges();
				}

				// ********** 기부속탭
				if (!CheckDup(grd기부속))
					return;

				for (DataTable dt = grd기부속.DataTable; dt != null; dt = null)
				//for (DataTable dt = grd기부속.GetChanges(); dt != null; dt = null)
				{
					SQL.ExecuteNonQuery("PX_CZ_PU_PO_TOOL_EDIT", sqlDebug, dt.ToXml());
					grd기부속.AcceptChanges();
				}

				// ********** 견적탭
				if (!SaveTool(sqlDebug))
					return;

				UT.ShowMsg(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				UT.ShowMsg(ex);
			}
		}

		private bool SaveTool(SQLDebug sqlDebug)
		{
			DataTable dtHead = Header.GetChanges();
			DataTable dtLineQ = grd견적.GetChanges();
			DataTable dtLineP = grd발주.GetChanges();

			// 필수사항 확인
			if (!tbx도구번호.Verify(공통메세지._은는필수입력항목입니다, DD("도구번호"))) return false;
			if (!CheckDup(grd견적)) return false;

			// 저장
			SQL.ExecuteNonQuery("PX_CZ_PU_PO_TOOL_2", sqlDebug, LoginInfo.CompanyCode, tbx도구번호.Text, dtHead.ToXml(), dtLineQ.ToXml());
			SQL.ExecuteNonQuery("PX_CZ_PU_PO_TOOL_2", sqlDebug, LoginInfo.CompanyCode, tbx도구번호.Text, null, dtLineP.ToXml());

			// 추가 모드일 경우 도구번호 바인딩
			if (Header.JobMode == JobModeEnum.추가후수정)
				pnl도구번호.SetEdit(false);

			IsHeaderChanged = false;
			Header.AcceptChanges();
			grd견적.AcceptChanges();
			grd발주.AcceptChanges();

			return true;
		}

		#endregion

		#region ==================================================================================================== Delete

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			base.OnToolBarDeleteButtonClicked(sender, e);

			if (UT.ShowMsg(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes) return;
			if (!Util.CheckPW()) return;
			
			string query = @"
DELETE FROM CZ_PU_PO_TOOLH WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND NO_TOOL = '" + tbx도구번호.Text + @"'
DELETE FROM CZ_PU_PO_TOOLL WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND NO_TOOL = '" + tbx도구번호.Text + "'";

			SQL.ExecuteNonQuery(query);

			Clear();
			UT.ShowMsg(공통메세지.자료가정상적으로삭제되었습니다);
		}

		#endregion

		private void ShowSummary()
		{
			DataRow[] rows = grd견적.DataTable.Select("ISNULL(NO_IMO, '') <> ''");
			DataRow[] rowsPo = grd발주.DataTable.Select();

			lbl호선수.Text = string.Format("{0:#,##0}", rows.Select(r => r.Field<string>("NO_IMO")).Distinct().Count());
			lbl아이템종수.Text = string.Format("{0:#,##0}", rows.Length);
			lbl아이템금액.Text = string.Format("{0:#,##0}", rows.Sum(r => (r.Field<decimal?>("QT_NEED") ?? 0) * (r.Field<decimal?>("UM_AVG") ?? 0)));
			lbl예상금액.Text = string.Format("{0:#,##0}", rows.Sum(r => r.Field<decimal?>("AM_Q_INPUT") ?? 0));
			lbl최종금액.Text = string.Format("{0:#,##0}", rows.Sum(r => r.Field<decimal?>("AM_Q_FINAL") ?? 0));

			lbl발주견적금액.Text = string.Format("{0:#,##0}", rowsPo.Sum(r => (r.Field<decimal?>("QT_Q_FINAL") ?? 0) * (r.Field<decimal?>("UM_QTN") ?? 0)));
			lbl발주예상금액.Text = string.Format("{0:#,##0}", rowsPo.Sum(r => r.Field<decimal?>("AM_P_INPUT") ?? 0));
			lbl발주최종금액.Text = string.Format("{0:#,##0}", rowsPo.Sum(r => r.Field<decimal?>("AM_P_FINAL") ?? 0));
		}

		private void SetGridStyle(FlexGrid flexGrid)
		{
			flexGrid.Redraw = false;

			if (flexGrid == grd견적)
			{
				for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
				{
					// 사용자 추가
					if (flexGrid[i, "YN_MANUAL"].ToString() == "Y")
					{
						flexGrid.SetCellStyle(i, flexGrid.Cols["CD_ITEM"].Index, "FORE_RED");
						flexGrid.SetCellStyle(i, flexGrid.Cols["UCODE"].Index, "FORE_RED");
						flexGrid.SetCellStyle(i, flexGrid.Cols["KCODE"].Index, "FORE_RED");
					}
					else
					{
						flexGrid.SetCellStyle(i, flexGrid.Cols["CD_ITEM"].Index, "");
						flexGrid.SetCellStyle(i, flexGrid.Cols["UCODE"].Index, "");
						flexGrid.SetCellStyle(i, flexGrid.Cols["KCODE"].Index, "");
					}

					// FACTOR
					foreach (string s in new string[] { "SS", "LT", "LOT", "MOQ" })
					{
						if (flexGrid[i, s].ToInt() > 1)
							flexGrid.SetCellStyle(i, flexGrid.Cols[s].Index, "BACK_YELLOW");
						else
							flexGrid.SetCellStyle(i, flexGrid.Cols[s].Index, "");
					}
				}
			}
			else if (flexGrid == grd발주)
			{
				for (int i = flexGrid.Rows.Fixed; i < flexGrid.Rows.Count; i++)
				{
					// UCODE 변경
					if (flexGrid[i, "UCODE"].ToString() != flexGrid[i, "UCODE_OLD"].ToString())
						flexGrid.SetCellStyle(i, flexGrid.Cols["UCODE"].Index, "BACK_YELLOW");
					else
						flexGrid.SetCellStyle(i, flexGrid.Cols["UCODE"].Index, "");

					// NO_PLATE 변경
					if (flexGrid[i, "UCODE"].ToString() != flexGrid[i, "UCODE_OLD"].ToString() && flexGrid[i, "NO_PLATE"].ToString() != flexGrid[i, "NO_PLATE_OLD"].ToString())
						flexGrid.SetCellStyle(i, flexGrid.Cols["NO_PLATE"].Index, "BACK_YELLOW");
					else
						flexGrid.SetCellStyle(i, flexGrid.Cols["NO_PLATE"].Index, "");

					// 단가
					//if (flexGrid[i, "UM_LAST_TAG"].ToString() != cbo엔진모델2.GetValue("CD_FLAG2"))
					//{
					//	flexGrid.SetCellStyle(i, flexGrid.Cols["UM_LAST"].Index, "BACK_YELLOW");
					//	flexGrid.SetCellStyle(i, flexGrid.Cols["RT_LAST"].Index, "BACK_YELLOW");
					//}
					//else
					//{
					//	flexGrid.SetCellStyle(i, flexGrid.Cols["UM_LAST"].Index, "");
					//	flexGrid.SetCellStyle(i, flexGrid.Cols["RT_LAST"].Index, "");
					//}
				}
			}

			flexGrid.Redraw = true;
		}

		private bool CheckDup(FlexGrid flexGrid)
		{
			if (flexGrid.DataTable.Select("ISNULL(CD_ITEM, '') = '' AND ISNULL(KCODE, '') = '' AND ISNULL(UCODE, '') = ''").Length > 0)
			{
				UT.ShowMsg("빈값이 있습니다.");
				return false;
			}

			for (string dup = flexGrid.DataTable.CheckDup("CD_ITEM"); dup != "";)
			{
				UT.ShowMsg("[" + dup + "] 항목이 중복되었습니다");
				return false;
			}

			for (string dup = flexGrid.DataTable.CheckDup("KCODE"); dup != "";)
			{
				UT.ShowMsg("[" + dup + "] 항목이 중복되었습니다");
				return false;
			}

			for (string dup = flexGrid.DataTable.CheckDup("UCODE"); dup != "";)
			{
				UT.ShowMsg("[" + dup + "] 항목이 중복되었습니다");
				return false;
			}

			return true;
		}

		private void SetStatus(string code)
		{
			if (cbo진행상황.GetValue().ToInt() < code.ToInt())
				cbo진행상황.SetValue(code);
		}
	}
}


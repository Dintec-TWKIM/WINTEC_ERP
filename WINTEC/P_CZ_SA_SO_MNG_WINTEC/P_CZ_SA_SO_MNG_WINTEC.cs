using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.BizOn.Erpu.Net.File;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;
using Duzon.Windows.Print;
using DX;
using master;
using sale;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_SO_MNG_WINTEC : PageBase
	{
		#region 초기화 & 전역변수
		private P_CZ_SA_SO_MNG_WINTEC_BIZ _biz = new P_CZ_SA_SO_MNG_WINTEC_BIZ();
		private bool yn_confirm = false;
		private string 버튼유형 = string.Empty;
		private bool b수량권한 = true;
		private bool b단가권한 = true;
		private bool b금액권한 = true;
		private DataTable daou_dtH = null;
		private DataTable daou_dtL = null;

		private bool chkDate
		{
			get
			{
				return Checker.IsValid(this.dtp수주일자, true, this.cbo수주일자.Text);
			}
		}

		public P_CZ_SA_SO_MNG_WINTEC()
		{
			try
			{
				if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
					StartUp.Certify(this);

				this.InitializeComponent();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
		{
			object[] args = e.Args;

			this.InitPaint();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			DataTable dataTable = BASIC.MFG_AUTH("P_SA_SO_MNG");

			if (dataTable.Rows.Count > 0)
			{
				this.b수량권한 = !(dataTable.Rows[0]["YN_QT"].ToString() == "Y");
				this.b단가권한 = !(dataTable.Rows[0]["YN_UM"].ToString() == "Y");
				this.b금액권한 = !(dataTable.Rows[0]["YN_AM"].ToString() == "Y");
			}

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flex수주번호별L, this._flex수주번호별H, this._flex품목별, this._flex현대시리얼번호 };
			this._flex수주번호별H.DetailGrids = new FlexGrid[] { this._flex수주번호별L };
			this._flex품목별.DetailGrids = new FlexGrid[] { this._flex현대시리얼번호 };

			#region 수주번호별

			#region Header
			this._flex수주번호별H.BeginSetting(1, 1, false);

			this._flex수주번호별H.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
			this._flex수주번호별H.SetCol("CD_PARTNER", "거래처코드", false);
			this._flex수주번호별H.SetCol("LN_PARTNER", "거래처명", 120);
			this._flex수주번호별H.SetCol("NO_PO_PARTNER", "거래처PO번호", 100, false);
			this._flex수주번호별H.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호별H.SetCol("NO_SO", "수주번호", 120);

			if (this.b수량권한)
				this._flex수주번호별H.SetCol("QT_SO", "수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex수주번호별H.SetCol("CD_EXCH", "환종", 80);
			this._flex수주번호별H.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

			if (this.b금액권한)
			{
				this._flex수주번호별H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				this._flex수주번호별H.SetCol("AM_WONAMT", "수주원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
				this._flex수주번호별H.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
				this._flex수주번호별H.SetCol("AMVAT_SO", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
			}

			this._flex수주번호별H.SetCol("TXT_USERDEF1", "거래처공정", 100, false);
			this._flex수주번호별H.SetCol("NM_PUR", "구매담당자", 100, false);
			this._flex수주번호별H.SetCol("NM_DESIGN", "설계담당자", 100, false);
			this._flex수주번호별H.SetCol("NM_TAKE", "인수자", 100, false);
			this._flex수주번호별H.SetCol("DC_RMK", "비고", 120, true);
			this._flex수주번호별H.SetCol("DC_RMK1", "비고1", 120, true);
			this._flex수주번호별H.SetCol("NM_SO", "수주형태", 100);
			this._flex수주번호별H.SetCol("NM_TP_VAT", "과세구분", 100);
			this._flex수주번호별H.SetCol("NM_KOR", "수주담당자", 80);
			this._flex수주번호별H.SetCol("NO_LC", "L/C번호", 100, true);
			this._flex수주번호별H.SetCol("DTS_INSERT", "입력일시", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호별H.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
			this._flex수주번호별H.SetCol("NM_ID_INSERT", "입력자", 80);
            this._flex수주번호별H.SetCol("DTS_UPDATE", "수정일시", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수주번호별H.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";
            this._flex수주번호별H.SetCol("DTS_UPDATE1", "수정일시1", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex수주번호별H.Cols["DTS_UPDATE1"].Format = "####/##/##/##:##:##";

			this._flex수주번호별H.SetCol("DTS_SEND_NEW", "신규통보일시", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호별H.Cols["DTS_SEND_NEW"].Format = "####/##/##/##:##:##";

			this._flex수주번호별H.SetCol("DTS_SEND_MODIFY", "변경통보일시", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호별H.Cols["DTS_SEND_MODIFY"].Format = "####/##/##/##:##:##";

			this._flex수주번호별H.SetCol("NM_SALEORG", "영업조직", false);
			this._flex수주번호별H.SetCol("NM_SALEGRP", "영업그룹", false);
			
			this._flex수주번호별H.SetCol("NO_PROJECT", "프로젝트코드", false);
			this._flex수주번호별H.SetCol("NM_PROJECT", "프로젝트명", false);

			this._flex수주번호별H.SetCol("ST_STAT", "전자결재상태", 100, false);
			this._flex수주번호별H.Cols["ST_STAT"].Visible = false;
			
			this._flex수주번호별H.SetCol("FILE_CNT", "첨부파일", false);
			
			this._flex수주번호별H.SetCol("DC_RMK_TEXT", "텍스트비고1", 120, true);
			this._flex수주번호별H.Cols["DC_RMK_TEXT"].Visible = false;

			this._flex수주번호별H.SetCol("NM_NEGO", "입고처명", false);
			
			this._flex수주번호별H.ExtendLastCol = true;
			this._flex수주번호별H.SetDummyColumn("S", "MEMO_CD", "CHECK_PEN");

			this._flex수주번호별H.SettingVersion = "0.0.0.1";
			this._flex수주번호별H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			this._flex수주번호별H.SetExceptSumCol("RT_EXCH");

			this._flex수주번호별H.CellNoteInfo.EnabledCellNote = true;
			this._flex수주번호별H.CellNoteInfo.CategoryID = this.Name;
			this._flex수주번호별H.CellNoteInfo.DisplayColumnForDefaultNote = "NO_SO";
			this._flex수주번호별H.CheckPenInfo.EnabledCheckPen = true;
			#endregion

			#region Line
			this._flex수주번호별L.BeginSetting(1, 1, false);

			this._flex수주번호별L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);

			this._flex수주번호별L.SetCol("STA_SO", "수주상태", 60);
			this._flex수주번호별L.SetCol("CD_ITEM", "품목코드", 100);
			this._flex수주번호별L.SetCol("NM_ITEM", "품목명", 120);
			this._flex수주번호별L.SetCol("NO_DESIGN", "도면번호", 100);
			this._flex수주번호별L.SetCol("STND_ITEM", "규격", 65);
			this._flex수주번호별L.SetCol("STND_DETAIL_ITEM", "세부규격", 65);
			this._flex수주번호별L.SetCol("MAT_ITEM", "재질", false);

			if (this.b수량권한)
				this._flex수주번호별L.SetCol("QT_SO", "수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flex수주번호별L.SetCol("UNIT_SO", "단위", 65);

			this._flex수주번호별L.SetCol("DT_EXPECT", "소요납기(최종)", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호별L.SetCol("DT_DUEDATE", "관리납기", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			if (this.b수량권한)
				this._flex수주번호별L.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

			//진행현황
			this._flex수주번호별L.SetCol("DT_REQGI", "수정소요납기(최종)", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호별L.SetCol("DT_DELAY", "납기지연일수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호별L.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호별L.SetCol("DT_OUT", "최종출고일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex수주번호별L.SetCol("QT_SCORE", "납기준수율점수", 100, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호별L.SetCol("QT_GI", "출하수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호별L.SetCol("QT_NOT_GI", "미출하수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호별L.SetCol("QT_RETURN", "출하반품수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호별L.SetCol("GI_PARTNER", "납품처", false);
			this._flex수주번호별L.SetCol("NM_GI_PARTNER", "납품처명", 120);

			if (this.b단가권한)
				this._flex수주번호별L.SetCol("UM_SO", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

			if (this.b금액권한)
			{
				this._flex수주번호별L.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
				this._flex수주번호별L.SetCol("UMVAT_SO", "단가(부가세포함)", 150, false, typeof(decimal), FormatTpType.MONEY);
				this._flex수주번호별L.Cols["UMVAT_SO"].Visible = false;
				this._flex수주번호별L.SetCol("AM_SO", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				this._flex수주번호별L.SetCol("AM_WONAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
			}

			if (this.b금액권한)
				this._flex수주번호별L.SetCol("AM_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);

			this._flex수주번호별L.SetCol("TXT_USERDEF4", "도장COLOR", 150);
			this._flex수주번호별L.SetCol("TXT_USERDEF3", "자재번호", 150);
			this._flex수주번호별L.SetCol("TXT_USERDEF5", "납품장소", 150, true);
			this._flex수주번호별L.SetCol("TXT_USERDEF6", "호선번호", 100);
			this._flex수주번호별L.SetCol("TXT_USERDEF7", "도면번호(수주)", 100);
			this._flex수주번호별L.SetCol("NO_MODEL", "U-CODE", 150, false);
			this._flex수주번호별L.SetCol("NUM_USERDEF3", "신규제작소요일", 100, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex수주번호별L.SetCol("YN_OPTION", "선급검사여부", 100, false, CheckTypeEnum.Y_N);
			this._flex수주번호별L.SetCol("CD_USERDEF1", "선급검사기관1", 100, false);
			this._flex수주번호별L.SetCol("CD_USERDEF2", "선급검사기관2", 100, false);
			this._flex수주번호별L.SetCol("CD_USERDEF3", "엔진타입", 100, true);
			this._flex수주번호별L.SetCol("DC1", "비고", 100, true);
			this._flex수주번호별L.SetCol("DC2", "변경비고", 100, true);
			this._flex수주번호별L.SetCol("YN_EBOM", "EBOM여부", 60, false, CheckTypeEnum.Y_N);
			this._flex수주번호별L.SetCol("YN_BOM", "BOM여부", 60, false, CheckTypeEnum.Y_N);

			#region 사용안함
			this._flex수주번호별L.SetCol("CD_EXCH", "환종", false);
			this._flex수주번호별L.SetCol("CD_PLANT", "공장", false);

			if (BASIC.GetSAENV("002") == "Y" && this.b단가권한)
			{
				this._flex수주번호별L.SetCol("UM_BASE", "기준단가", false);
				this._flex수주번호별L.SetCol("RT_DSCNT", "할인율", false);
			}

			this._flex수주번호별L.SetCol("TP_BUSI", "거래구분", false);

			this._flex수주번호별L.SetCol("NM_SL", "창고", false);
			this._flex수주번호별L.SetCol("UNIT_IM", "관리단위", false);

			if (this.b수량권한)
				this._flex수주번호별L.SetCol("QT_IM", "관리수량", false);

			this._flex수주번호별L.SetCol("CD_ITEM_PARTNER", "거래처품번", false);
			this._flex수주번호별L.SetCol("NM_ITEM_PARTNER", "거래처품명", false);

			this._flex수주번호별L.SetCol("NM_ID_STA_HST", "이전변경자", false);
			this._flex수주번호별L.SetCol("DTS_STA_HST", "이전변경일", false);
			this._flex수주번호별L.SetCol("NM_ID_UPDATE", "변경자", false);
			this._flex수주번호별L.SetCol("DTS_UPDATE", "변경일", false);

			this._flex수주번호별L.SetCol("NO_PO_PARTNER", "거래처PO번호", false);
			this._flex수주번호별L.SetCol("NO_POLINE_PARTNER", "거래처PO항번", false);

			this._flex수주번호별L.SetCol("FG_USE", "수주용도", false);
			this._flex수주번호별L.SetCol("FG_USE2", "수주용도2", false);

			this._flex수주번호별L.SetCol("NUM_USERDEF1", "사용자정의1", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flex수주번호별L.Cols["NUM_USERDEF1"].Visible = false;
			this._flex수주번호별L.SetCol("NUM_USERDEF2", "사용자정의2", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flex수주번호별L.Cols["NUM_USERDEF2"].Visible = false;
			this._flex수주번호별L.SetCol("NUM_USERDEF5", "사용자정의5", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flex수주번호별L.Cols["NUM_USERDEF5"].Visible = false;
			this._flex수주번호별L.SetCol("NUM_USERDEF6", "사용자정의6", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flex수주번호별L.Cols["NUM_USERDEF6"].Visible = false;
			this._flex수주번호별L.SetCol("TXT_USERDEF1", "TEXT사용자정의1", 150, false);
			this._flex수주번호별L.Cols["TXT_USERDEF1"].Visible = false;
			this._flex수주번호별L.SetCol("TXT_USERDEF2", "TEXT사용자정의2", 150, false);
			this._flex수주번호별L.Cols["TXT_USERDEF2"].Visible = false;

			this._flex수주번호별L.SetCol("CLS_ITEM", "품목계정", false);
			this._flex수주번호별L.SetCol("NO_PROJECT", "프로젝트코드", false);
			this._flex수주번호별L.SetCol("NM_PROJECT", "프로젝트명", false);
			this._flex수주번호별L.SetCol("CD_ITEMGRP", "품목군코드", false);
			this._flex수주번호별L.SetCol("NM_ITEMGRP", "품목군명", false);
			this._flex수주번호별L.SetCol("GRP_MFG", "제품군코드", false);
			this._flex수주번호별L.SetCol("NM_GRP_MFG", "제품군명", false);
			this._flex수주번호별L.SetCol("NO_RELATION", "연동번호", false);
			this._flex수주번호별L.SetCol("SEQ_RELATION", "연동항번", false);

			this._flex수주번호별L.SetCol("NM_CC", "CC명", false);
			#endregion

			this._flex수주번호별L.SetDataMap("CD_USERDEF1", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME"); // 선급검사기관1
			this._flex수주번호별L.SetDataMap("CD_USERDEF2", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME"); // 선급검사기관2
			this._flex수주번호별L.SetDataMap("CD_USERDEF3", MA.GetCode("CZ_WIN0003", true), "CODE", "NAME"); // 엔진타입

			this._flex수주번호별L.SetDummyColumn("S");
			this._flex수주번호별L.SettingVersion = "0.0.0.1";
			this._flex수주번호별L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flex수주번호별L.Styles.Add("납기일자변경").ForeColor = Color.Red;
			this._flex수주번호별L.Styles.Add("납기일자변경").BackColor = Color.White;
			this._flex수주번호별L.Styles.Add("납기일자동일").ForeColor = Color.Black;
			this._flex수주번호별L.Styles.Add("납기일자동일").BackColor = Color.White;

			this._flex수주번호별L.Cols["NM_CC"].Visible = false;
			this._flex수주번호별L.SetExceptSumCol("UM_SO", "UMVAT_SO", "NO_POLINE_PARTNER");
			#endregion

			#endregion

			#region 품목별
			this._flex품목별.BeginSetting(1, 1, false);

			this._flex품목별.SetCol("NO_SO", "수주번호", 100);
			this._flex품목별.SetCol("CD_PARTNER", "거래처코드", 100);
			this._flex품목별.SetCol("LN_PARTNER", "거래처명", 100);
			this._flex품목별.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex품목별.SetCol("CD_USERDEF3", "엔진타입", 100, true);
			this._flex품목별.SetCol("TXT_USERDEF6", "호선번호", 100);
			this._flex품목별.SetCol("CD_ITEM", "품목코드", 100);
			this._flex품목별.SetCol("NM_ITEM", "품목명", 100);
			this._flex품목별.SetCol("NO_DESIGN", "도면번호", 100);
			this._flex품목별.SetCol("NO_PO_PARTNER", "거래처번호", 100);
			this._flex품목별.SetCol("TXT_USERDEF7", "도면번호(수주)", 100, true);
			this._flex품목별.SetCol("DC_RMK_TEXT", "텍스트비고1", 120, true);

			if (this.b수량권한)
			{
				this._flex품목별.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
				this._flex품목별.SetCol("QT_GIR", "의뢰수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
				this._flex품목별.SetCol("QT_GI", "출고수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
				this._flex품목별.SetCol("QT_REMAIN", "미납수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
			}
			
			this._flex품목별.SetCol("DT_EXPECT", "소요납기(최초)", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex품목별.SetCol("DT_DUEDATE", "관리납기", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex품목별.SetCol("DT_REQGI", "수정소요납기(최종)", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex품목별.SetCol("DT_IO", "납품일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex품목별.SetCol("DT_DELAY", "납기지연일수", 100);
			this._flex품목별.SetCol("STA_SO", "진행상태", 100);
			this._flex품목별.SetCol("TXT_USERDEF5", "납품장소", 100, true);
			this._flex품목별.SetCol("NM_EXCH", "통화명", 100);
			this._flex품목별.SetCol("CD_USERDEF1", "선급검사기관1", 100, false);
			this._flex품목별.SetCol("CD_USERDEF2", "선급검사기관2", 100, false);
			this._flex품목별.SetCol("DC_RMK1", "비고1", 120, true);
			this._flex품목별.SetCol("DTS_INSERT", "입력일시", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex품목별.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

			if (this.b단가권한)
				this._flex품목별.SetCol("UM_SO", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

			if (this.b금액권한)
			{
				this._flex품목별.SetCol("AM_SO", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				this._flex품목별.SetCol("AM_WONAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
				this._flex품목별.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
				this._flex품목별.SetCol("AM_SUM", "합계금액", 100, false, typeof(decimal), FormatTpType.MONEY);
			}
			
			this._flex품목별.SetCol("TXT_USERDEF3", "자재번호", 100);
			this._flex품목별.SetCol("NO_LC", "L/C번호", 100, true);
			this._flex품목별.SetCol("YN_EBOM", "EBOM여부", 60, false, CheckTypeEnum.Y_N);
			this._flex품목별.SetCol("YN_BOM", "BOM여부", 60, false, CheckTypeEnum.Y_N);

			this._flex품목별.SetDataMap("CD_USERDEF3", MA.GetCode("CZ_WIN0003", true), "CODE", "NAME"); // 엔진타입
			this._flex품목별.SetDataMap("CD_USERDEF1", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME"); // 선급검사기관1
			this._flex품목별.SetDataMap("CD_USERDEF2", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME"); // 선급검사기관2

			this._flex품목별.SettingVersion = "0.0.0.1";
			this._flex품목별.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flex품목별.Styles.Add("납기일자변경").ForeColor = Color.Red;
			this._flex품목별.Styles.Add("납기일자변경").BackColor = Color.White;
			this._flex품목별.Styles.Add("납기일자동일").ForeColor = Color.Black;
			this._flex품목별.Styles.Add("납기일자동일").BackColor = Color.White;
			#endregion

			#region 시리얼
			this._flex현대시리얼번호.BeginSetting(1, 1, false);

			this._flex현대시리얼번호.SetCol("CD_ITEM", "품목코드", 100, true);
			this._flex현대시리얼번호.SetCol("NM_ITEM", "품목명", 100);
			this._flex현대시리얼번호.SetCol("NO_SERIAL", "시리얼", 100, true);
			this._flex현대시리얼번호.SetCol("NO_ID", "ID번호", 100, true);

			this._flex현대시리얼번호.SettingVersion = "0.0.0.1";
			this._flex현대시리얼번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

        protected override void InitPaint()
		{
			base.InitPaint();

			this.oneGrid1.UseCustomLayout = true;
			this.bpPanelControl1.IsNecessaryCondition = true;
			this.bpPanelControl2.IsNecessaryCondition = true;
			this.oneGrid1.InitCustomLayout();
			
			this.dtp수주일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
			this.dtp수주일자.EndDateToString = Global.MainFrame.GetStringToday;

			this.Page_DataChanged(null, null);

			DataSet comboData = this.GetComboData("N;MA_PLANT", "S;PU_C000016", "S;SA_B000016", "S;MA_B000005", "S;MA_B000065", "S;MA_B000067");

			this.cbo공장.DataSource = comboData.Tables[0];
			this.cbo공장.DisplayMember = "NAME";
			this.cbo공장.ValueMember = "CODE";
			if (comboData.Tables[0] != null && comboData.Tables[0].Rows.Count > 0)
				this.cbo공장.SelectedIndex = 0;

			this.cbo거래구분.DataSource = comboData.Tables[1];
			this.cbo거래구분.DisplayMember = "NAME";
			this.cbo거래구분.ValueMember = "CODE";

			this.cbo수주상태.DataSource = comboData.Tables[2];
			this.cbo수주상태.DisplayMember = "NAME";
			this.cbo수주상태.ValueMember = "CODE";

			SetControl setControl = new SetControl();
			setControl.SetCombobox(this.cbo거래처그룹, MA.GetCode("MA_B000065", true));
			setControl.SetCombobox(this.cbo거래처그룹2, MA.GetCode("MA_B000067", true));
			setControl.SetCombobox(this.cbo전자결재상태, MA.GetCode("SA_B000060", true));
			setControl.SetCombobox(this.cbo배차확정여부, MA.GetCodeUser(new string[] { "R", "O" }, new string[] { "확정", "미확정" }, true));
			setControl.SetCombobox(this.cbo진행상태, MA.GetCodeUser(new string[] { "0", "1", "2" }, new string[] { "의뢰진행", "출하진행", "출하완료" }, true));

			DataTable dataTable = comboData.Tables[0].Clone();
			DataRow row1 = dataTable.NewRow();
			row1["CODE"] = "SO";
			row1["NAME"] = this.DD("수주일자");
			dataTable.Rows.Add(row1);
			DataRow row2 = dataTable.NewRow();
			row2["CODE"] = "DU";
			row2["NAME"] = this.DD("납기일자");
			dataTable.Rows.Add(row2);
			DataRow row3 = dataTable.NewRow();
			row3["CODE"] = "GI";
			row3["NAME"] = this.DD("납품일자");
			dataTable.Rows.Add(row3);
			DataRow row4 = dataTable.NewRow();
			row4["CODE"] = "IP";
			row4["NAME"] = this.DD("입력일자");
			dataTable.Rows.Add(row4);

			this.cbo수주일자.DataSource = dataTable;
			this.cbo수주일자.DisplayMember = "NAME";
			this.cbo수주일자.ValueMember = "CODE";

			this._flex품목별.SetDataMap("STA_SO", comboData.Tables[2], "CODE", "NAME");

			this._flex수주번호별H.SetDataMap("CD_EXCH", comboData.Tables[3], "CODE", "NAME");
			this._flex수주번호별L.SetDataMap("CD_PLANT", comboData.Tables[0], "CODE", "NAME");
			this._flex수주번호별L.SetDataMap("TP_BUSI", comboData.Tables[1], "CODE", "NAME");
			this._flex수주번호별L.SetDataMap("STA_SO", comboData.Tables[2], "CODE", "NAME");
			this._flex수주번호별L.SetDataMap("FG_USE", MA.GetCode("SA_B000057", true), "CODE", "NAME");
			this._flex수주번호별L.SetDataMap("FG_USE2", MA.GetCode("SA_B000063", true), "CODE", "NAME");
			this._flex수주번호별L.SetDataMap("CLS_ITEM", MA.GetCode("MA_B000010"), "CODE", "NAME");
			this._flex수주번호별H.SetDataMap("ST_STAT", MA.GetCode("SA_B000060"), "CODE", "NAME");

			string currentGrantMenu = Global.MainFrame.CurrentGrantMenu;
			if (currentGrantMenu == "B" || currentGrantMenu == "S" || (currentGrantMenu == "C" || currentGrantMenu == "D") || currentGrantMenu == "E")
				this.cbo공장.Enabled = false;

			if (this._biz.GetATP사용여부 == "000")
				this.btnATPCHECK.Visible = false;

			setControl.SetCombobox(this.cbo발주상태, MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "Y(적용)", "N(미적용)" }, true));

			UGrant ugrant = new UGrant();
			ugrant.GrantButtonVisible("P_SA_SO_MNG", "확정", this.btn확정);
			ugrant.GrantButtonVisible("P_SA_SO_MNG", "확정취소", this.btn확정취소);
			ugrant.GrantButtonVisible("P_SA_SO_MNG", "종결", this.btn종결);
			ugrant.GrantButtonVisible("P_SA_SO_MNG", "종결취소", this.btn종결취소);

			this.사용자정의세팅();
		}

		private void InitEvent()
		{
			this.DataChanged += new EventHandler(this.Page_DataChanged);

			this.bpc영업조직.QueryAfter += new BpQueryHandler(this.bpc영업조직_QueryAfter);
			this.bpc영업그룹.QueryBefore += new BpQueryHandler(this.bpc영업그룹_QueryBeforeㅠ);
			this.ctx프로젝트.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.btnATPCHECK.Click += new EventHandler(this.btnATPCHECK_Click);
			this.bpc수주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.btn라인삭제.Click += new EventHandler(this.btn라인삭제_Click);
			this.ctx품목from.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx품목to.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx품목from.QueryAfter += new BpQueryHandler(this.ctx품목from_QueryAfter);
			this.btn확정.Click += new EventHandler(this.btn확정_Click);
			this.btn확정취소.Click += new EventHandler(this.btn확정취소_Click);
			this.btn종결.Click += new EventHandler(this.btn종결_Click);
			this.btn종결취소.Click += new EventHandler(this.btn종결취소_Click);
			this.btn신규수주통보.Click += new EventHandler(this.btn신규수주통보_Click);
            this.btn변경수주통보.Click += new EventHandler(this.btn변경수주통보_Click);
			this.btn검사일정조회.Click += new EventHandler(this.btn검사일정_Click);

            this._flex수주번호별H.StartEdit += new RowColEventHandler(this._flexH_StartEdit);
			this._flex수주번호별H.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
			this._flex수주번호별H.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
			this._flex수주번호별H.HelpClick += new EventHandler(this._flexH_HelpClick);
			this._flex수주번호별H.CellContentChanged += new CellContentEventHandler(this._flexH_CellContentChanged);

			this._flex수주번호별L.StartEdit += new RowColEventHandler(this._flexL_StartEdit);
			this._flex수주번호별L.OwnerDrawCell += _flex수주번호별L_OwnerDrawCell;
			this._flex품목별.OwnerDrawCell += _flex품목별_OwnerDrawCell;

			this._flex품목별.AfterRowChange += new RangeEventHandler(this._flex품목별_AfterRowChange);
            this.btn현대시리얼추가.Click += Btn추가_Click;
            this.btn현대시리얼삭제.Click += Btn삭제_Click;
			this._flexMAN시리얼번호.ValidateEdit += _flexMAN시리얼번호_ValidateEdit;
        }

		private void _flexMAN시리얼번호_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			DataTable dt;
			DataRow drTemp;
			string oldValue, newValue, columnName, query;

			try
			{
				columnName = this._flexMAN시리얼번호.Cols[e.Col].Name;

				oldValue = this._flexMAN시리얼번호[e.Row, e.Col].ToString();
				newValue = this._flexMAN시리얼번호.EditData;

				if (columnName == "IDX") return;
				if (oldValue == newValue) return;

				query = @"SELECT ST.YN_TRUST 
						  FROM CZ_SA_SOL_TRUST_WINTEC ST WITH(NOLOCK)
						  WHERE ST.CD_COMPANY = '{0}'
						  AND ST.NO_SO = '{1}'
					      AND ST.SEQ_SO = '{2}'
						  AND ST.IDX = '{3}'
					      AND ST.CD_ITEM = '{4}'
						  AND ISNULL(ST.YN_TRUST, 'N') = 'Y'";

				dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																this._flexMAN시리얼번호["NO_SO"].ToString(),
																this._flexMAN시리얼번호["SEQ_SO"].ToString(),
																this._flexMAN시리얼번호["IDX"].ToString(),
																columnName));

				if (dt != null && dt.Rows.Count > 0)
				{
					this.ShowMessage("마킹처리되어 있는 번호는 수정할 수 없습니다.");
					e.Cancel = true;
					return;
				}

				dt = new DataTable();

				dt.Columns.Add("CD_COMPANY");
				dt.Columns.Add("NO_SO");
				dt.Columns.Add("SEQ_SO");
				dt.Columns.Add("IDX");
				dt.Columns.Add("CD_ITEM");
				dt.Columns.Add("NO_TRUST");

				drTemp = dt.NewRow();

				drTemp["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				drTemp["NO_SO"] = this._flexMAN시리얼번호["NO_SO"].ToString();
				drTemp["SEQ_SO"] = this._flexMAN시리얼번호["SEQ_SO"].ToString();
				drTemp["IDX"] = this._flexMAN시리얼번호["IDX"].ToString();
				drTemp["CD_ITEM"] = columnName;
				drTemp["NO_TRUST"] = newValue;

				dt.Rows.Add(drTemp);

				if (string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
				{

				}
				else if (!string.IsNullOrEmpty(oldValue) && string.IsNullOrEmpty(newValue))
				{
					drTemp.AcceptChanges();
					drTemp.Delete();
				}
				else if (!string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(newValue))
				{
					drTemp.AcceptChanges();
					drTemp.SetModified();
				}

				if (dt.Rows.Count > 0)
				{
					DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_MNG_TRUST_WINTEC_JSON", new object[] { dt.GetChanges().Json(), Global.MainFrame.LoginInfo.UserID });
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flex수주번호별L_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			try
			{
				if (!this._flex수주번호별L.HasNormalRow) return;
				if (e.Row < this._flex수주번호별L.Rows.Fixed || e.Col < this._flex수주번호별L.Cols.Fixed) return;

				CellStyle cellStyle = this._flex수주번호별L.Rows[e.Row].Style;

				if (D.GetString(this._flex수주번호별L[e.Row, "DT_DUEDATE"]) != string.Empty &&
					D.GetString(this._flex수주번호별L[e.Row, "DT_REQGI"]) != string.Empty &&
					D.GetString(this._flex수주번호별L[e.Row, "DT_DUEDATE"]) != D.GetString(this._flex수주번호별L[e.Row, "DT_REQGI"]))
				{
					if (cellStyle == null || cellStyle.Name != "납기일자변경")
						this._flex수주번호별L.Rows[e.Row].Style = this._flex수주번호별L.Styles["납기일자변경"];
				}
				else
				{
					if (cellStyle == null || cellStyle.Name != "납기일자동일")
						this._flex수주번호별L.Rows[e.Row].Style = this._flex수주번호별L.Styles["납기일자동일"];
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flex품목별_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			try
			{
				if (!this._flex품목별.HasNormalRow) return;
				if (e.Row < this._flex품목별.Rows.Fixed || e.Col < this._flex품목별.Cols.Fixed) return;

				CellStyle cellStyle = this._flex품목별.Rows[e.Row].Style;

				if (D.GetString(this._flex품목별[e.Row, "DT_DUEDATE"]) != string.Empty &&
					D.GetString(this._flex품목별[e.Row, "DT_REQGI"]) != string.Empty &&
					D.GetString(this._flex품목별[e.Row, "DT_DUEDATE"]) != D.GetString(this._flex품목별[e.Row, "DT_REQGI"]))
				{
					if (cellStyle == null || cellStyle.Name != "납기일자변경")
						this._flex품목별.Rows[e.Row].Style = this._flex품목별.Styles["납기일자변경"];
				}
				else
				{
					if (cellStyle == null || cellStyle.Name != "납기일자동일")
						this._flex품목별.Rows[e.Row].Style = this._flex품목별.Styles["납기일자동일"];
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region 메인버튼 이벤트
		private void Page_DataChanged(object sender, EventArgs e)
		{
			if (this._flex수주번호별H.HasNormalRow)
			{
				if (this._flex수주번호별L.HasNormalRow)
					this.ToolBarDeleteButtonEnabled = true;
				else
					this.ToolBarDeleteButtonEnabled = false;
			}

			this.ToolBarAddButtonEnabled = false;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			DataTable dt;
			object[] obj;

			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch() || !this.chkDate)
					return;

				obj = new object[] { this.LoginInfo.CompanyCode,
									 D.GetString(this.cbo공장.SelectedValue),
									 this.dtp수주일자.StartDateToString,
									 this.dtp수주일자.EndDateToString,
									 this.bpc영업그룹.QueryWhereIn_Pipe,
									 this.ctx수주담당자.CodeValue,
									 D.GetString(this.cbo거래구분.SelectedValue),
									 this.ctx거래처.CodeValue,
									 this.bpc수주형태.QueryWhereIn_Pipe,
									 D.GetString(this.cbo수주상태.SelectedValue),
									 this.ctx프로젝트.CodeValue,
									 string.Empty,
									 string.Empty,
									 this.bpc영업조직.QueryWhereIn_Pipe,
									 D.GetString(this.cbo거래처그룹.SelectedValue),
									 D.GetString(this.cbo거래처그룹2.SelectedValue),
									 MA.Login.사원번호,
									 this.txt수주번호.Text,
									 D.GetString(this.cbo전자결재상태.SelectedValue),
									 D.GetString(this.cbo배차확정여부.SelectedValue),
									 D.GetString(this.ctx납품처.CodeValue),
									 D.GetString(this.cbo발주상태.SelectedValue),
									 Global.SystemLanguage.MultiLanguageLpoint,
									 D.GetString(this.cbo진행상태.SelectedValue),
									 D.GetString(this.ctx품목from.CodeValue),
									 D.GetString(this.ctx품목to.CodeValue),
								     this.txt호선번호.Text };

				dt = this._biz.Search(this.cbo수주일자.SelectedValue.ToString(), this.tabControl1.SelectedIndex.ToString(), obj);

				if (this.tabControl1.SelectedIndex == 0)
				{
					this._flex품목별.Binding = dt;

					if (!this._flex품목별.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				else if (this.tabControl1.SelectedIndex == 1)
				{
					this._flex수주번호별H.Binding = dt;

					if (!this._flex수주번호별H.HasNormalRow)
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!this.BeforeDelete())
					return;

				string empty = string.Empty;
				string str = string.Empty;
				DataRow[] dataRowArray1 = this._flex수주번호별H.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					this._flex수주번호별L.Redraw = false;
					this._flex수주번호별H.Redraw = false;

					foreach (DataRow dataRow in dataRowArray1)
					{
						if (empty != dataRow["NO_SO"].ToString())
						{
							empty = dataRow["NO_SO"].ToString();
							str = str + empty + "|";
						}
					}

					foreach (DataRow dataRow in dataRowArray1)
						dataRow.Delete();

					string[] strArray = str.Split('|');

					for (int index = 0; index < strArray.Length; ++index)
					{
						DataRow[] dataRowArray2 = this._flex수주번호별L.DataTable.Select("NO_SO = '" + strArray[index] + "'", "", DataViewRowState.CurrentRows);
						if (dataRowArray2 != null && dataRowArray2.Length > 0)
						{
							foreach (DataRow dataRow in dataRowArray2)
								dataRow.Delete();
						}
					}

					if (Global.MainFrame.ShowMessage("헤더와 라인 모두 일괄삭제됩니다. 삭제하시겠습니까?", "QY2") == DialogResult.Yes)
					{
						if (!this._biz.Delete(str)) return;

						this._flex수주번호별H.AcceptChanges();
						this._flex수주번호별L.AcceptChanges();
					}

					this.OnToolBarSearchButtonClicked(null, null);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
				this.OnToolBarSearchButtonClicked(null, null);
			}
			finally
			{
				this._flex수주번호별H.Redraw = true;
				this._flex수주번호별L.Redraw = true;
			}
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
					return;

				this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다, new string[0]);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData())
				return false;

			DataTable changes1 = this._flex수주번호별H.GetChanges();
			DataTable changes2 = this._flex수주번호별L.GetChanges();
			DataTable changes3 = this._flex품목별.GetChanges();
			DataTable changes4 = this._flex현대시리얼번호.GetChanges();

			if ((changes1 == null || changes1.Rows.Count == 0) && 
				(changes2 == null || changes2.Rows.Count == 0) &&
				(changes3 == null || changes3.Rows.Count == 0) &&
				(changes4 == null || changes4.Rows.Count == 0))
			{
				this.ShowMessage(공통메세지.변경된내용이없습니다, new string[0]);
				return false;
			}

			if ((changes1 != null && changes1.Rows.Count > 0) ||
				(changes2 != null && changes2.Rows.Count > 0) ||
				(changes3 != null && changes3.Rows.Count > 0))
			{
				if (!this._biz.Save(changes1, changes2, changes3, this.버튼유형, this.daou_dtH, this.daou_dtL))
					return false;

				this._flex수주번호별H.AcceptChanges();
				this._flex수주번호별L.AcceptChanges();
				this._flex품목별.AcceptChanges();
			}

			if ((changes4 != null && changes4.Rows.Count > 0))
			{
				if (!this._biz.SaveJson(changes4))
					return false;
				if (!_biz.Save2(changes4)) return false;

				this._flex현대시리얼번호.AcceptChanges();
			}

			this.버튼유형 = "";

			return true;
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if (!this._flex수주번호별L.HasNormalRow || !this.BeforePrint())
					return;

				DataRow[] dataRowArray = this._flex수주번호별L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

				if (dataRowArray.Length < 1)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					DataTable dt = this._biz.Search_Print("XXXXXXX", "0");
					decimal num2 = 0;

					foreach (DataRow dataRow in dataRowArray)
					{
						DataTable dataTable = this._biz.Search_Print(D.GetString(dataRow["NO_SO"]), D.GetString(dataRow["SEQ_SO"]));

						foreach (DataRow row in dataTable.Rows)
							dt.ImportRow(row);
					}

					ReportHelper reportHelper = new ReportHelper("R_SA_SO_MNG", "수주서");

					reportHelper.SetData("공장코드", D.GetString(this.cbo공장.SelectedValue));
					reportHelper.SetData("공장명", D.GetString(this.cbo공장.Text));
					reportHelper.SetData("거래구분", D.GetString(this.cbo거래구분.SelectedValue));
					reportHelper.SetData("거래구분명", D.GetString(this.cbo거래구분.Text));
					reportHelper.SetData("거래처그룹", D.GetString(this.cbo거래처그룹.SelectedValue));
					reportHelper.SetData("거래처그룹명", D.GetString(this.cbo거래처그룹.Text));
					reportHelper.SetData("거래처그룹2", D.GetString(this.cbo거래처그룹2.SelectedValue));
					reportHelper.SetData("거래처그룹명2", D.GetString(this.cbo거래처그룹2.Text));
					reportHelper.SetData("수주상태", D.GetString(this.cbo수주상태.SelectedValue));
					reportHelper.SetData("수주상태명", D.GetString(this.cbo수주상태.Text));
					reportHelper.SetDataTable(dt, 1);
					reportHelper.SetDataTable(dt, 2);
					reportHelper.Print();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 그리드 이벤트
		private void _flexH_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				if (!(this._flex수주번호별H.Cols[e.Col].Name == "S"))
					return;

				DataRow[] dataRowArray = this._flex수주번호별L.DataTable.Select("NO_SO = '" + this._flex수주번호별H[e.Row, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);

				if (this._flex수주번호별H[e.Row, "S"].ToString() == "N")
				{
					for (int row = this._flex수주번호별L.Rows.Fixed; row <= dataRowArray.Length + 1; ++row)
						this._flex수주번호별L.SetCellCheck(row, 1, CheckEnum.Checked);
				}
				else
				{
					for (int row = this._flex수주번호별L.Rows.Fixed; row <= dataRowArray.Length + 1; ++row)
						this._flex수주번호별L.SetCellCheck(row, 1, CheckEnum.Unchecked);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_CheckHeaderClick(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex수주번호별H.HasNormalRow)
					return;

				this._flex수주번호별H.Redraw = false;
				this._flex수주번호별L.Redraw = false;
				this._flex수주번호별H.Row = 1;
				Duzon.ERPU.MF.Common.Common.Setting setting = new Duzon.ERPU.MF.Common.Common.Setting();

				if (this._flex수주번호별H[this._flex수주번호별H.Rows.Fixed, "S"].ToString() == "N" && this._flex수주번호별L[this._flex수주번호별L.Rows.Fixed, "S"].ToString() == "Y")
				{
					for (int index = this._flex수주번호별H.Rows.Fixed; index <= this._flex수주번호별H.Rows.Count - 1; ++index)
					{
						this._flex수주번호별H.Row = index;
						DataRow[] dataRowArray = this._flex수주번호별L.DataTable.Select("NO_SO = '" + this._flex수주번호별H[index, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);

						for (int row = this._flex수주번호별L.Rows.Fixed; row <= dataRowArray.Length + 1; ++row)
							this._flex수주번호별L.SetCellCheck(row, 1, CheckEnum.Unchecked);
					}
				}
				else
				{
					string 멀티수주번호 = string.Empty;

					for (int row = this._flex수주번호별H.Rows.Fixed; row < this._flex수주번호별H.Rows.Count; ++row)
					{
						if (this._flex수주번호별H.DetailQueryNeedByRow(row))
						{
							멀티수주번호 = 멀티수주번호 + D.GetString(this._flex수주번호별H[row, "NO_SO"]) + "|";
							this._flex수주번호별H.SetDetailQueryNeedByRow(row, false);
						}
					}

					this._flex수주번호별L.BindingAdd(this._biz.SearchCheckHeader(멀티수주번호, (object[])new List<string>() { MA.Login.회사코드,
																												             string.Empty,
																												             D.GetString(this.cbo공장.SelectedValue),
																												             D.GetString(this.cbo거래구분.SelectedValue),
																												             D.GetString(this.cbo수주상태.SelectedValue),
																												             this.ctx프로젝트.CodeValue,
																												             string.Empty,
																												             this.dtp수주일자.StartDateToString,
																												             this.dtp수주일자.EndDateToString,
																												             D.GetString(this.cbo수주일자.SelectedValue),
																												             Global.SystemLanguage.MultiLanguageLpoint }.ToArray()), "NO_SO = '" + D.GetString(this._flex수주번호별H["NO_SO"]) + "'");

					for (int row1 = this._flex수주번호별H.Rows.Fixed; row1 <= this._flex수주번호별H.Rows.Count - 1; ++row1)
					{
						this._flex수주번호별H.Row = row1;
						this._flex수주번호별H.SetCellCheck(row1, 1, CheckEnum.Checked);
						DataRow[] dataRowArray = this._flex수주번호별L.DataTable.Select("NO_SO = '" + this._flex수주번호별H[row1, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);

						for (int row2 = this._flex수주번호별L.Rows.Fixed; row2 <= dataRowArray.Length + 1; ++row2)
							this._flex수주번호별L.SetCellCheck(row2, 1, CheckEnum.Checked);
					}
				}
			}
			catch (Exception ex)
			{
				this._flex수주번호별H.Redraw = true;
				this._flex수주번호별L.Redraw = true;
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex수주번호별H.Redraw = true;
				this._flex수주번호별L.Redraw = true;
			}
		}

		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				DataTable dt = (DataTable)null;
				string str1 = this._flex수주번호별H[e.NewRange.r1, "NO_SO"].ToString();
				string str2 = "NO_SO = '" + str1 + "'";

				if (this._flex수주번호별H.DetailQueryNeed)
					dt = this._biz.SearchDetail(new object[] { this.LoginInfo.CompanyCode,
															   str1,
															   this.cbo공장.SelectedValue == null ?  string.Empty :  this.cbo공장.SelectedValue.ToString(),
															   this.cbo거래구분.SelectedValue == null ?  string.Empty :  this.cbo거래구분.SelectedValue.ToString(),
															   this.cbo수주상태.SelectedValue == null ?  string.Empty :  this.cbo수주상태.SelectedValue.ToString(),
															   this.ctx프로젝트.CodeValue,
															   string.Empty,
															   this.dtp수주일자.StartDateToString,
															   this.dtp수주일자.EndDateToString,
															   this.cbo수주일자.SelectedValue == null ?  string.Empty :  this.cbo수주일자.SelectedValue.ToString(),
															   D.GetString(this.cbo발주상태.SelectedValue),
															   Global.SystemLanguage.MultiLanguageLpoint,
															   D.GetString( this.ctx품목from.CodeValue),
															   D.GetString( this.ctx품목to.CodeValue) });

				this._flex수주번호별L.BindingAdd(dt, str2);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex품목별_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataSet ds;
			DataTable dt;

			try
			{
				dt = null;
				ds = null;
				string key1, key2;

				key1 = this._flex품목별["NO_SO"].ToString();
				key2 = this._flex품목별["SEQ_SO"].ToString();
				string filter = "NO_SO = '" + key1 + "'AND SEQ_SO = '" + key2 + "'";

				if (this._flex품목별.DetailQueryNeed)
				{
					dt = this._biz.SearchSerial(new object[] { this.LoginInfo.CompanyCode,
															   key1,
															   key2 });
				}

				this._flex현대시리얼번호.BindingAdd(dt, filter);

				#region MAN 시리얼 번호
				ds = this._biz.SearchTrust(new object[] { this.LoginInfo.CompanyCode,
														  key1,
														  key2,
														  this._flex품목별["CD_ITEM"].ToString() });

				if (ds != null && ds.Tables.Count == 2)
				{
					this._flexMAN시리얼번호.BeginSetting(2, 1, false);
					this._flexMAN시리얼번호.Cols.Count = 1;

					this._flexMAN시리얼번호.SetCol("IDX", "순번", 100);

					foreach (DataRow dr in ds.Tables[1].Rows)
					{
						this._flexMAN시리얼번호.SetCol(dr["CD_ITEM"].ToString(), "시리얼", 100, true);
						this._flexMAN시리얼번호[0, this._flexMAN시리얼번호.Cols[dr["CD_ITEM"].ToString()].Index] = dr["NM_ITEM"].ToString();

						this._flexMAN시리얼번호.SetCol(dr["CD_ITEM"].ToString() + "-1", "ID번호", 100);
						this._flexMAN시리얼번호[0, this._flexMAN시리얼번호.Cols[dr["CD_ITEM"].ToString() + "-1"].Index] = dr["NM_ITEM"].ToString();
					}

					this._flexMAN시리얼번호.AllowCache = false;
					this._flexMAN시리얼번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

					this._flexMAN시리얼번호.Binding = ds.Tables[0];
				}
				else
				{
					this._flexMAN시리얼번호.BeginSetting(2, 1, false);
					this._flexMAN시리얼번호.Cols.Count = 1;

					this._flexMAN시리얼번호.SetCol("IDX", "순번", 100, false);

					this._flexMAN시리얼번호.AllowCache = false;
					this._flexMAN시리얼번호.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

					this._flexMAN시리얼번호.Binding = null;
				}
				#endregion
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_HelpClick(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex수주번호별H.HasNormalRow)
					return;

				if (this._flex수주번호별H.Cols[this._flex수주번호별H.Col].Name == "NO_SO")
				{
					if (this.IsExistPage("P_CZ_SA_SO_WINTEC", false))
						this.UnLoadPage("P_CZ_SA_SO_WINTEC", false);

					this.LoadPageFrom("P_CZ_SA_SO_WINTEC", MA.PageName("P_CZ_SA_SO_WINTEC"), this.Grant, new object[] { D.GetString(this._flex수주번호별H["NO_SO"]) });
				}
				else if (this._flex수주번호별H.Cols[this._flex수주번호별H.Col].Name == "FILE_CNT")
				{
					string str = D.GetString(this._flex수주번호별H["NO_SO"]) + "_" + D.GetString(this._flex수주번호별H["NO_HST"]);
					DataTable dataTable = this._biz.IsFileHelpCheck();

					if (D.GetString(dataTable.Rows[0]["TP_FILESERVER"]) == "0")
					{
						if (new P_MA_FILE_SUB("SA", Global.MainFrame.CurrentPageID, str).ShowDialog() == DialogResult.Cancel)
							return;
					}
					else if (new AttachmentManager(Global.MainFrame.CurrentModule, Global.MainFrame.CurrentPageID, str).ShowDialog() == DialogResult.Cancel)
						return;

					DataTable fileName = this._biz.Get_FileName(str);
					this._flex수주번호별H["FILE_CNT"] = dataTable.Rows.Count == 0 || dataTable == null ? string.Empty : fileName.Rows[0]["FILE_PATH_MNG"];
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_CellContentChanged(object sender, CellContentEventArgs e)
		{
			try
			{
				this._biz.SaveContent(e.ContentType, e.CommandType, D.GetString(this._flex수주번호별H[e.Row, "NO_SO"]), e.SettingValue);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexL_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				switch (this._flex수주번호별L.Cols[e.Col].Name)
				{
					case "S":
						break;
					default:
						if (!(D.GetString(this._flex수주번호별L[e.Row, "STA_SO"]) == "C"))
							break;
						e.Cancel = true;
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 컨트롤 이벤트
		private void btn신규수주통보_Click(object sender, EventArgs e)
		{
            try
            {
                if (Global.MainFrame.ShowMessage("신규 수주통보서를 발송 하시겠습니까?", "QY2") != DialogResult.Yes) return;
                
                this.수주통보(true);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn변경수주통보_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ShowMessage("수주 변경서를 발송 하시겠습니까?", "QY2") != DialogResult.Yes) return;

                this.수주통보(false);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn확정_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex수주번호별L.HasNormalRow)
				{
					this.ShowMessage("확정될 데이터가 없습니다.");
				}
				else
				{
					DataRow[] dataRowArray1 = this._flex수주번호별L.DataTable.Select("S = 'Y'", "NO_SO", DataViewRowState.CurrentRows);
					DataRow[] dataRowArray2 = this._flex수주번호별H.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
					DataRow[] dataRowArray3 = this._flex수주번호별H.DataTable.Select("S = 'Y'", "CD_PARTNER", DataViewRowState.CurrentRows);

					if (dataRowArray1 == null || dataRowArray1.Length == 0)
					{
						this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
					}
					else
					{
						if (this._biz.GetATP사용여부 == "001" && !this.ATP체크로직(true))
							return;

						if (this._biz.GetExcCredit == "300")
						{
							if (!this.거래처환종별체크())
								return;
						}
						else if (!this.거래처별체크(dataRowArray1, "O"))
							return;

						if (!this.IsAgingCheck(dataRowArray1))
							return;

						DataRow[] dataRowArray4 = this._flex수주번호별L.DataTable.Select("S = 'Y'", "NO_SO", DataViewRowState.CurrentRows);
						bool flag = false;
						StringBuilder stringBuilder = new StringBuilder();
						string str3 = "수주번호     항번 상태 수주량    의뢰량   출하량    LC적용량";
						stringBuilder.AppendLine(str3);
						string str4 = "-".PadRight(60, '-');
						stringBuilder.AppendLine(str4);

						foreach (DataRow dataRow1 in dataRowArray4)
						{
							if (dataRow1["STA_SO"].ToString() == "O")
							{
								dataRow1["STA_SO"] = "R";
								this.yn_confirm = true;
							}
							else
							{
								string str1 = dataRow1["NO_SO"].ToString().PadRight(14, ' ');
								string str2 = dataRow1["SEQ_SO"].ToString().PadRight(3, ' ');
								string str5 = dataRow1["STA_SO"].ToString().PadRight(3, ' ');
								string str6 = string.Format("{0:n}", dataRow1["QT_SO"]).PadRight(10, ' ');
								string str7 = string.Format("{0:n}", dataRow1["QT_GI"]).PadRight(10, ' ');
								string str8 = string.Format("{0:n}", dataRow1["QT_GIR"]).PadRight(10, ' ');
								string str9 = string.Format("{0:n}", dataRow1["QT_LC"]).PadRight(10, ' ');
								string str10 = str1 + " " + str2 + " " + str5 + " " + str6 + " " + str8 + " " + str7 + " " + str9;
								stringBuilder.AppendLine(str10);
								flag = true;
							}
						}

						if (flag)
						{
							this.ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n 미정건에 대해서만 [확정]이 가능합니다.  \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", stringBuilder.ToString());
						}

						if (this.yn_confirm && this.BeforeSave())
						{
							if (this.SaveData())
							{
								this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn확정.Text });
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);

				if (!this.yn_confirm)
				{
					foreach (DataRow dataRow in this._flex수주번호별L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows))
					{
						if (dataRow["STA_SO"].ToString() == "R")
							dataRow["STA_SO"] = "O";
					}

					this._flex수주번호별L.AcceptChanges();
					this.Page_DataChanged(null, null);
				}
			}
		}

		private void btn확정취소_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex수주번호별L.HasNormalRow)
				{
					this.ShowMessage("확정될 데이터가 없습니다.");
				}
				else
				{
					DataRow[] dataRowArray = this._flex수주번호별L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

					if (dataRowArray == null || dataRowArray.Length == 0)
					{
						this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
					}
					else
					{
						bool flag = false;
						StringBuilder stringBuilder = new StringBuilder();
						string str1 = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
						stringBuilder.AppendLine(str1);
						string str2 = "-".PadRight(70, '-');
						stringBuilder.AppendLine(str2);

						foreach (DataRow dataRow in dataRowArray)
						{
							if (dataRow["STA_SO"].ToString() == "R" && Convert.ToDecimal(dataRow["QT_GIR"]) == 0 && Convert.ToDecimal(dataRow["QT_LC"]) == 0)
							{
								dataRow["STA_SO"] = "O";
								this.yn_confirm = true;
							}
							else
							{
								string str3 = dataRow["NO_SO"].ToString().PadRight(14, ' ');
								string str4 = dataRow["SEQ_SO"].ToString().PadRight(3, ' ');
								string str5 = dataRow["STA_SO"].ToString().PadRight(3, ' ');
								string str6 = string.Format("{0:n}", dataRow["QT_SO"]).PadRight(10, ' ');
								string str7 = string.Format("{0:n}", dataRow["QT_GI"]).PadRight(10, ' ');
								string str8 = string.Format("{0:n}", dataRow["QT_GIR"]).PadRight(10, ' ');
								string str9 = string.Format("{0:n}", dataRow["QT_LC"]).PadRight(10, ' ');
								string str10 = str3 + " " + str4 + " " + str5 + " " + str6 + " " + str8 + " " + str7 + " " + str9;
								stringBuilder.AppendLine(str10);
								flag = true;
							}
						}

						if (flag)
						{
							this.ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n [확정]한 건에 대해서만 [확정취소]가 가능합니다.  \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", stringBuilder.ToString());
						}

						if (this.yn_confirm && this.BeforeSave() && this.SaveData())
						{
							this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn확정취소.Text });
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);

				if (!this.yn_confirm)
				{
					foreach (DataRow dataRow in this._flex수주번호별L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows))
					{
						if (dataRow["STA_SO"].ToString() == "O")
							dataRow["STA_SO"] = "R";
					}

					this._flex수주번호별L.AcceptChanges();
					this.Page_DataChanged(null, null);
				}
			}
		}

		private void btn종결_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex수주번호별L.HasNormalRow)
				{
					this.ShowMessage("확정될 데이터가 없습니다.");
				}
				else
				{
					DataRow[] dataRowArray1 = this._flex수주번호별L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
					this._flex수주번호별H.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

					if (dataRowArray1 == null || dataRowArray1.Length == 0)
					{
						this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
					}
					else
					{
						bool flag1 = false;
						bool flag2 = false;
						StringBuilder stringBuilder1 = new StringBuilder();
						StringBuilder stringBuilder2 = new StringBuilder();
						string str1 = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
						string str2 = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
						stringBuilder1.AppendLine(str1);
						string str3 = "-".PadRight(60, '-');
						stringBuilder1.AppendLine(str3);
						stringBuilder2.AppendLine(str2);
						string str4 = "-".PadRight(60, '-');
						stringBuilder2.AppendLine(str4);

						foreach (DataRow dataRow1 in dataRowArray1)
						{
							if (dataRow1["STA_SO"].ToString() == "R" || dataRow1["STA_SO"].ToString() == "O")
							{
								dataRow1["STA_SO"] = "C";
								this.yn_confirm = true;
							}
							else
							{
								string str5 = dataRow1["NO_SO"].ToString().PadRight(14, ' ');
								string str6 = dataRow1["SEQ_SO"].ToString().PadRight(3, ' ');
								string str7 = dataRow1["STA_SO"].ToString().PadRight(3, ' ');
								string str8 = string.Format("{0:n}", dataRow1["QT_SO"]).PadRight(10, ' ');
								string str9 = string.Format("{0:n}", dataRow1["QT_GI"]).PadRight(10, ' ');
								string str10 = string.Format("{0:n}", dataRow1["QT_GIR"]).PadRight(10, ' ');
								string str11 = string.Format("{0:n}", dataRow1["QT_LC"]).PadRight(10, ' ');
								string str12 = str5 + " " + str6 + " " + str7 + " " + str8 + " " + str10 + " " + str9 + " " + str11;
								stringBuilder1.AppendLine(str12);
								flag1 = true;
							}
						}

						if (flag1)
						{
							this.ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n [미정]된 건이나 [확정]된 건에 대해서만 [종결]이 가능합니다.  \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", stringBuilder1.ToString());
						}

						if (this.yn_confirm && this.BeforeSave())
						{
							this.버튼유형 = "종결";
							if (this.SaveData())
							{
								this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn종결.Text });
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);

				if (!this.yn_confirm)
				{
					foreach (DataRow dataRow in this._flex수주번호별L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows))
					{
						if (dataRow["STA_SO"].ToString() == "C")
							dataRow["STA_SO"] = "R";
					}

					this._flex수주번호별L.AcceptChanges();
					this.Page_DataChanged(null, null);
				}
			}
		}

		private void btn종결취소_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex수주번호별L.HasNormalRow)
				{
					this.ShowMessage("확정될 데이터가 없습니다.");
				}
				else
				{
					DataRow[] drs = this._flex수주번호별L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

					if (drs == null || drs.Length == 0)
					{
						this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
					}
					else
					{
						bool flag = false;
						StringBuilder stringBuilder = new StringBuilder();
						string str1 = "수주번호       항번 수주상태 수주량    의뢰량   출하량    LC적용량";
						stringBuilder.AppendLine(str1);
						string str2 = "-".PadRight(60, '-');
						stringBuilder.AppendLine(str2);

						foreach (DataRow dataRow in drs)
						{
							if (dataRow["STA_SO"].ToString() == "C")
							{
								dataRow["STA_SO"] = "R";
								this.yn_confirm = true;
							}
							else
							{
								string str3 = dataRow["NO_SO"].ToString().PadRight(14, ' ');
								string str4 = dataRow["SEQ_SO"].ToString().PadRight(3, ' ');
								string str5 = dataRow["STA_SO"].ToString().PadRight(3, ' ');
								string str6 = string.Format("{0:n}", dataRow["QT_SO"]).PadRight(10, ' ');
								string str7 = string.Format("{0:n}", dataRow["QT_GI"]).PadRight(10, ' ');
								string str8 = string.Format("{0:n}", dataRow["QT_GIR"]).PadRight(10, ' ');
								string str9 = string.Format("{0:n}", dataRow["QT_LC"]).PadRight(10, ' ');
								string str10 = str3 + " " + str4 + " " + str5 + " " + str6 + " " + str8 + " " + str7 + " " + str9;
								stringBuilder.AppendLine(str10);
								flag = true;
							}
						}

						if (flag)
						{
							this.ShowDetailMessage("수주 상태를 변경하지 못한 건이 있습니다 \n [종결]건에 대해서만 [종결취소]가 가능합니다.  \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", stringBuilder.ToString());
						}

						if (this.yn_confirm && this.BeforeSave())
						{
							this.버튼유형 = "종결취소";

							if (this.SaveData())
							{
								this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn종결취소.Text });
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);

				if (!this.yn_confirm)
				{
					foreach (DataRow dataRow in this._flex수주번호별L.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows))
					{
						if (dataRow["STA_SO"].ToString() == "R")
							dataRow["STA_SO"] = "C";
					}

					this._flex수주번호별L.AcceptChanges();
					this.Page_DataChanged(null, null);
				}
			}
		}

		private void btnATPCHECK_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex수주번호별H.HasNormalRow || !this._flex수주번호별L.HasNormalRow)
					return;

				DataRow[] dataRowArray = this._flex수주번호별L.DataTable.Select("S = 'Y'", "NO_SO", DataViewRowState.CurrentRows);

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else if (this.ATP체크로직(false))
				{
					this.ShowMessage("납기일에 이상이 없습니다.");
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn라인삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex수주번호별L.HasNormalRow)
					return;

				DataRow[] dataRowArray = this._flex수주번호별L.DataTable.Select("S='Y'", "", DataViewRowState.CurrentRows);

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
				}
				else if (this._flex수주번호별L.DataTable.Select("S='Y' AND STA_SO <> 'O'", "", DataViewRowState.CurrentRows).Length > 0)
				{
					this.ShowMessage("이미 수주확정, 종결 되어 수정, 삭제가 불가능합니다.");
				}
				else
				{
					foreach (DataRow dataRow in dataRowArray)
						dataRow.Delete();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			switch (e.HelpID)
			{
				case HelpID.P_USER:
					if (!(this.ctx프로젝트.UserHelpID == "H_SA_PRJ_SUB"))
						break;
					e.HelpParam.P41_CD_FIELD1 = "프로젝트";
					break;
				case HelpID.P_MA_PITEM_SUB:
					e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
					break;
				case HelpID.P_SA_TPSO_SUB1:
					e.HelpParam.P61_CODE1 = "N";
					e.HelpParam.P62_CODE2 = "Y";
					break;
			}
		}

		private void bpc영업조직_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				this.bpc영업그룹.Clear();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void bpc영업그룹_QueryBeforeㅠ(object sender, BpQueryArgs e)
		{
			try
			{
				if (this.bpc영업조직.IsEmpty())
					return;

				e.HelpParam.P61_CODE1 = this.bpc영업조직.QueryWhereIn_Pipe;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void ctx품목from_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				if (!(this.ctx품목to.CodeValue == ""))
					return;

				this.ctx품목to.CodeValue = this.ctx품목from.CodeValue;
				this.ctx품목to.CodeName = this.ctx품목from.CodeName;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn검사일정_Click(object sender, EventArgs e)
		{
			try
			{
				if (Global.MainFrame.IsExistPage("P_CZ_QU_INSP_SCHEDULE_MNG", false))
					Global.MainFrame.UnLoadPage("P_CZ_QU_INSP_SCHEDULE_MNG", false);

				Global.MainFrame.LoadPageFrom("P_CZ_QU_INSP_SCHEDULE_MNG", Global.MainFrame.DD("검사일정관리"), this.Grant, new object[] { this._flex수주번호별H["NO_SO"].ToString() });
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				this._flex현대시리얼번호.Rows.Add();
				this._flex현대시리얼번호.Row = this._flex현대시리얼번호.Rows.Count - 1;

				this._flex현대시리얼번호["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				this._flex현대시리얼번호["NO_SO"] = this._flex품목별["NO_SO"];
				this._flex현대시리얼번호["SEQ_SO"] = this._flex품목별["SEQ_SO"];
				this._flex현대시리얼번호["CD_ITEM"] = this._flex품목별["CD_ITEM"];
				this._flex현대시리얼번호["NM_ITEM"] = this._flex품목별["NM_ITEM"];

				this._flex현대시리얼번호.AddFinished();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex현대시리얼번호.HasNormalRow) return;

				this._flex현대시리얼번호.Rows.Remove(this._flex현대시리얼번호.Row);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		#endregion

		#region 기타 메소드
		private bool 거래처별체크(DataRow[] dr, string 수주상태)
		{
			string empty = string.Empty;
			string str = string.Empty;

			foreach (DataRow dataRow in dr)
			{
				if (D.GetString(dataRow["STA_SO"]) == 수주상태 && D.GetString(dataRow["TP_BUSI"]) == "001")
				{
					if (empty != D.GetString(dataRow["CD_PARTNER"]))
					{
						empty = D.GetString(dataRow["CD_PARTNER"]);
						str = str + empty + "|";
					}
				}
			}

			string[] strArray = str.Split('|');
			decimal 금액 = 0;

			for (int index = 0; index < strArray.Length - 1; ++index)
			{
				foreach (DataRow dataRow in dr)
				{
					금액 = 금액 + D.GetDecimal(dataRow["AM_WONAMT"]) + D.GetDecimal(dataRow["AM_VAT"]);
				}

				if (!this._biz.CheckCredit(D.GetString(strArray[index]), 금액))
					return false;

				금액 = 0;
			}

			return true;
		}

		private bool 거래처환종별체크()
		{
			DataTable table = this._flex수주번호별L.DataTable.Copy().DefaultView.ToTable(true, "CD_PARTNER", "CD_EXCH", "STA_SO");

			foreach (DataRow row in table.Rows)
			{
				if (!(D.GetString(row["STA_SO"]) != "O"))
				{
					string cdPartner = D.GetString(row["CD_PARTNER"]);
					string cdExch = D.GetString(row["CD_EXCH"]);
					decimal amEx = D.GetDecimal(this._flex수주번호별L.DataTable.Compute("SUM(AM_SO)", "S = 'Y' AND STA_SO = 'O' AND CD_PARTNER ='" + cdPartner + "' AND CD_EXCH = '" + cdExch + "'"));

					if (!this._biz.CheckCreditExec(cdPartner, cdExch, amEx))
						return false;
				}
			}

			return true;
		}

		private bool IsAgingCheck(DataRow[] drs)
		{
			채권연령관리 채권연령관리 = new 채권연령관리();
			DataTable dataTable = this._flex수주번호별L.DataTable.Copy();
			dataTable.DefaultView.RowFilter = "S = 'Y' AND STA_SO = 'O'";
			DataRow[] drs1 = dataTable.DefaultView.ToTable(true, "CD_PARTNER").Select();
			DataTable dtReturn1;
			채권연령관리.채권연령체크(drs1, AgingCheckPoint.수주확정, out dtReturn1);

			if (dtReturn1 == null || dtReturn1.Rows.Count == 0)
				return true;

			P_SA_CUST_CREDIT_CHECK_SUB custCreditCheckSub = new P_SA_CUST_CREDIT_CHECK_SUB(dtReturn1);

			if (custCreditCheckSub.ShowDialog() != DialogResult.OK)
				return false;

			DataTable dtReturn2 = custCreditCheckSub.dtReturn;
			dtReturn2.PrimaryKey = new DataColumn[] { dtReturn2.Columns["CD_PARTNER"] };

			foreach (DataRow dr in drs)
			{
				DataRow dataRow = dtReturn2.Rows.Find(D.GetString(dr["CD_PARTNER"]));

				if (dataRow != null && !(D.GetString(dataRow["S"]) == "Y"))
					dr["S"] = "N";
			}

			return true;
		}

		private bool ATP체크로직(bool 자동체크)
		{
			DataRow[] dataRowArray1 = this._flex수주번호별L.DataTable.DefaultView.ToTable(true, new string[] { "S", "CD_PLANT" }).Select("S = 'Y'");

			if (dataRowArray1.Length > 1)
			{
				this.ShowMessage("두개 이상의 공장이 선택되어 ATP체크가 불가합니다");
				return false;
			}

			ATP atp = new ATP();

			if (atp.ATP환경설정_사용유무(this.LoginInfo.BizAreaCode, D.GetString(dataRowArray1[0]["CD_PLANT"])) == "N")
				return true;

			string str = atp.ATP자동체크_저장로직(D.GetString(dataRowArray1[0]["CD_PLANT"]), "200");

			if (str != "000" && str != "001")
				return true;

			DataTable dataTable = this._flex수주번호별L.DataTable.Copy();
			DataRow[] drs = dataTable.Select("S = 'Y' AND YN_ATP = 'Y' AND STA_SO = 'O'", "", DataViewRowState.CurrentRows);
			DataRow[] dataRowArray2 = dataTable.Select("S = 'Y' AND YN_ATP = 'Y'", "", DataViewRowState.CurrentRows);

			if (drs.Length == 0)
				return true;

			if (drs.Length != dataRowArray2.Length)
			{
				this.ShowMessage("수주상태가 미확정인 건에 대해서만 ATP체크를 합니다.");
			}

			if (drs.Length != dataTable.DefaultView.ToTable(true, "CD_ITEM", "YN_ATP", "STA_SO").Select("YN_ATP = 'Y' AND STA_SO = 'O'").Length && this.ShowMessage("동일품목이 존재 할 경우 정확한 ATP체크를 할 수 없습니다." + Environment.NewLine + "계속 진행하시겠습니까?", "QY2") != DialogResult.Yes)
				return false;

			string s_Message = string.Empty;
			atp.Set메뉴ID = this.PageID;

			if (atp.ATP_Check(drs, out s_Message))
				return true;

			if (!자동체크)
			{
				this.ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", s_Message);
				return false;
			}

			if (str == "000")
				return this.ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요." + Environment.NewLine + "그래도 확정하시겠습니까?", "", s_Message, "QY2") == DialogResult.Yes;

			if (!(str == "001"))
				return true;

			this.ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", s_Message);

			return false;
		}

		private void 사용자정의세팅()
		{
			DataRow[] dataRowArray = MA.GetCode("SA_B000110").Select("CD_FLAG1 = 'CODE'");

			for (int index = 1; index <= dataRowArray.Length; ++index)
			{
				if (index <= 1)
				{
					string str = D.GetString(dataRowArray[index - 1]["NAME"]);
					this._flex수주번호별H.Cols["CD_USERDEF" + D.GetString(index)].Caption = str;
					this._flex수주번호별H.Cols["CD_USERDEF" + D.GetString(index)].Visible = true;
				}
			}


			this._flex수주번호별L.Cols["NUM_USERDEF1"].Visible = false;
			this._flex수주번호별L.Cols["NUM_USERDEF2"].Visible = false;
			this._flex수주번호별L.Cols["NUM_USERDEF3"].Visible = true;
			this._flex수주번호별L.Cols["NUM_USERDEF5"].Visible = false;
			this._flex수주번호별L.Cols["NUM_USERDEF6"].Visible = false;

            //DataTable code1 = MA.GetCode("SA_B000069");

            //for (int index = 1; index <= code1.Rows.Count && index <= 6; ++index)
            //{
            //	string str = D.GetString(code1.Rows[index - 1]["NAME"]);
            //	this._flexL.Cols["NUM_USERDEF" + D.GetString(index)].Caption = str;
            //	this._flexL.Cols["NUM_USERDEF" + D.GetString(index)].Visible = true;
            //}

            for (int index = 1; index <= 2; ++index)
                this._flex수주번호별L.Cols["TXT_USERDEF" + D.GetString(index)].Visible = false;

            DataTable code2 = MA.GetCode("SA_B000112");

            for (int index = 1; index <= code2.Rows.Count && index <= 2; ++index)
            {
                string str = D.GetString(code2.Rows[index - 1]["NAME"]);
                this._flex수주번호별L.Cols["TXT_USERDEF" + D.GetString(index)].Caption = str;
                this._flex수주번호별L.Cols["TXT_USERDEF" + D.GetString(index)].Visible = true;
            }
        }

        private void 수주통보(bool 신규여부)
        {
            string 본문, html, query, 엔진유형;
            DataRow[] dataRowArray;

            try
            {
				if (!this._flex수주번호별H.HasNormalRow) return;

                dataRowArray = this._flex수주번호별H.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (신규여부 == true)
                        본문 = "신규 수주건이 있으니 확인 바랍니다.";
                    else
                        본문 = "수주 변경건이 있으니 확인 바랍니다.";


                    if (신규여부 == true)
                        html = @"<br/><div style='text-align:left; font-weight: bold;'>*** 신규 수주 통보서</div>";
                    else
                        html = @"<br/><div style='text-align:left; font-weight: bold;'>*** 수주 변경 통보서</div>";

                    html += @"<table style='width:100%; border:2px solid black; font-size: 9pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
							    <colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<colgroup width='6.25%' align='center'></colgroup>
								<tbody>
								<tr style='height:30px'>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>수주번호</th>                               
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>POR No.</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>수주일자</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>담당자</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>매출처</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>선급1</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>선급2</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>호선</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>TYPE</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>No.</th>                               
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>제 품 명</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>도면번호</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>수량</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>납기일</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>신규제작소요일</th>";
                    if (신규여부 == true)
                        html += "<th style='border:solid 1px black; text-align:center; background-color:Silver'>비고</th>";
                    else
                        html += "<th style='border:solid 1px black; text-align:center; background-color:Silver'>변경비고</th>";

                    html += "</tr>";

					DataTable dt1 = new DataTable();

					dt1.Columns.Add("NO_SO");
					dt1.Columns.Add("SEQ_SO");
					dt1.Columns.Add("TP_MAIL");
					dt1.Columns.Add("DC1");
					dt1.Columns.Add("DC2");

					foreach (DataRow dr in dataRowArray)
                    {
                        foreach (DataRow dr1 in this._flex수주번호별L.DataTable.Select("S = 'Y' AND NO_SO = '" + dr["NO_SO"].ToString() + "'"))
                        {
							if (!string.IsNullOrEmpty(dr1["CD_USERDEF3"].ToString()))
							{
								query = string.Format(@"SELECT CD.NM_SYSDEF 
														FROM MA_CODEDTL CD WITH(NOLOCK)
														WHERE CD.CD_COMPANY = '{0}' 
														AND CD.CD_FIELD = 'CZ_WIN0003' 
														AND CD.CD_SYSDEF = '{1}'", Global.MainFrame.LoginInfo.CompanyCode,
																				   dr1["CD_USERDEF3"].ToString());

								object obj = DBHelper.ExecuteScalar(query);

								if(obj == null)
								{
									this.ShowMessage(string.Format("엔진유형이 잘못 입력된 건이 있습니다. 수주번호 : ({0})", dr["NO_SO"].ToString()));
									return;
								}
								else
									엔진유형 = obj.ToString();
							}
							else
								엔진유형 = string.Empty;

							html += @"<tr style='height:30px'>
										 <th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_SO"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_PO_PARTNER"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:center; padding-left:10px; font-weight:normal'>" + Util.GetTo_DateStringS(dr["DT_SO"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:center; padding-left:10px; font-weight:normal'>" + dr["NM_KOR"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["LN_PARTNER"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr1["CD_USERDEF1"].ToString() + " </th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr1["CD_USERDEF2"].ToString() + " </th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr1["TXT_USERDEF6"].ToString() + " </th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + 엔진유형 + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:center; padding-left:10px; font-weight:normal'>" + dr1["SEQ_SO"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr1["NM_ITEM"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr1["NO_DESIGN"].ToString() + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:right; padding-right:10px; font-weight:normal'>" + D.GetInt(dr1["QT_SO"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:center; font-weight:normal'>" + Util.GetTo_DateStringS(dr1["DT_DUEDATE"]) + "</th>" + Environment.NewLine +
                                        "<th style='border:solid 1px black; text-align:right; padding-right:10px; font-weight:normal'>" + D.GetInt(dr1["NUM_USERDEF3"]) + " </th>" + Environment.NewLine;
                                        if (신규여부 == true)
                                            html += "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr1["DC1"].ToString() + " </th>" + Environment.NewLine;
                                        else
                                            html += "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr1["DC2"].ToString() + " </th>" + Environment.NewLine;
                            html += "</tr>";

							DataRow dr2 = dt1.NewRow();

							dr2["NO_SO"] = dr1["NO_SO"].ToString();
							dr2["SEQ_SO"] = dr1["SEQ_SO"].ToString();
							dr2["TP_MAIL"] = (신규여부 == true ? "001" : "002");
						    dr2["DC1"] = dr1["DC1"].ToString();
							dr2["DC2"] = dr1["DC2"].ToString();

							dt1.Rows.Add(dr2);


						}
                    }
                    html += @"</tbody>
							</table>";

                    #region 메일발송

                    #region 기본설정
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.SubjectEncoding = Encoding.UTF8;
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    mailMessage.IsBodyHtml = true;
                    #endregion

                    #region 메일정보
                    mailMessage.From = new MailAddress("wintec@dintec.co.kr", "관리자", Encoding.UTF8);

                    query = @"SELECT ME.NO_EMAIL 
FROM MA_CODEDTL MC WITH(NOLOCK)
JOIN MA_EMP ME ON ME.CD_COMPANY = MC.CD_COMPANY AND ME.NO_EMP = MC.CD_FLAG1
WHERE MC.CD_COMPANY = 'W100'
AND MC.CD_FIELD = 'CZ_WIN0006'
AND ISNULL(MC.USE_YN, 'N') = 'Y'";

                    DataTable dt = DBHelper.GetDataTable(query);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["NO_EMAIL"].ToString().Trim() != "")
                            mailMessage.To.Add(new MailAddress(dr["NO_EMAIL"].ToString()));
                    }

					if (신규여부 == true)
                        mailMessage.Subject = "신규 수주 알림";
                    else
                        mailMessage.Subject = "수주 변경 알림";

                    // 본문 html로 변환할 시 <a>태그 앞뒤로 하고 <a>태그 내부는 건드리지 않음
                    string body = "";
                    string bodyA = "";
                    string bodyB = "";
                    string bodyC = "";

                    int index = 본문.IndexOf("<a href=");

                    if (index > 0)
                    {
                        bodyA = 본문.Substring(0, index);
                        bodyB = 본문.Substring(index, 본문.IndexOf("</a>") + 4 - index);
                        bodyC = 본문.Substring(본문.IndexOf("</a>") + 4);

                        body = ""
                            + bodyA.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />")
                            + bodyB
                            + bodyC.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />");
                    }
                    else
                    {
                        body = 본문.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />"); ;
                    }

                    mailMessage.Body = "<div style='font-family:맑은 고딕; font-size:9pt'>" + body + "</div>" + html;
                    #endregion

                    #region 메일보내기
                    SmtpClient smtpClient = new SmtpClient("113.130.254.131", 587);
                    smtpClient.EnableSsl = false;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("wintec@dintec.co.kr", "Mail_123!@#");
					smtpClient.Send(mailMessage);
					#endregion

					DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_MNG_MAIL_SEND_LOG_JSON", new object[] { dt1.Json(), Global.MainFrame.LoginInfo.UserID });

					if (신규여부 == true)
                        Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn신규수주통보.Text);
                    else
                        Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn변경수주통보.Text);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion
    }
}
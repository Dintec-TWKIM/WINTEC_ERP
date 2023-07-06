using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.BizOn.Erpu.Net.File;
using Duzon.Common.BpControls;
using Duzon.Common.ConstLib;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF.EMail;
using Duzon.ERPU.OLD;
using Duzon.ERPU.SA;
using Duzon.ERPU.SA.Common;
using Duzon.ERPU.SA.Custmize;
using Duzon.Windows.Print;
using DzHelpFormLib;
using master;
using pur;
using sale;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using static Duzon.ERPU.MA;

namespace cz
{
	public partial class P_CZ_SA_SO_WINTEC : PageBase
	{
		#region 초기화 & 전역변수 
		private P_CZ_SA_SO_WINTEC_BIZ _biz = new P_CZ_SA_SO_WINTEC_BIZ();
		private FreeBinding _header;
		private CommonFunction _CommFun = new CommonFunction();
		private 수주관리.Config 수주Config = new 수주관리.Config();
		private string _수주상태;
		private string _구분;
		private string _거래구분;
		private string _출하형태;
		private string _매출형태;
		private string _의뢰여부;
		private string _출하여부;
		private string _매출여부;
		private string _수출여부;
		private string _반품여부;
		private bool _헤더수정여부;
		private string _단가적용형태;
		private string so_Price;
		private string disCount_YN;
		private string _수주번호;
		private string _매출자동여부;
		private string _배송여부;
		private string _자동승인여부;
		private bool _헤더만복사;
		private decimal d_SEQ_PROJECT;
		private string s_CD_PJT_ITEM;
		private string s_NM_PJT_ITEM;
		private string s_PJT_ITEM_STND;

		private bool 추가모드여부
		{
			get
			{
				return this._header.JobMode == JobModeEnum.추가후수정;
			}
		}

		private bool 헤더변경여부
		{
			get
			{
				if (this._header.GetChanges() != null && this._flexH.HasNormalRow)
					return true;
				else
					return false;
			}
		}

		private decimal 최대차수
		{
			get
			{
				decimal num = 0;

				for (int index = this._flexH.Rows.Fixed; index < this._flexH.Rows.Count; ++index)
				{
					if (D.GetDecimal(this._flexH[index, "SEQ_SO"]) > num)
						num = D.GetDecimal(this._flexH[index, "SEQ_SO"]);
				}

				return num;
			}
		}

		private bool Chk거래처
		{
			get
			{
				return !Checker.IsEmpty(this.ctx거래처, this.DD("거래처"));
			}
		}

		private bool Chk공장
		{
			get
			{
				return !Checker.IsEmpty(this.cbo공장, this.DD("공장"));
			}
		}

		private bool Chk수주유형
		{
			get
			{
				return !Checker.IsEmpty(this.ctx수주형태, this.lbl수주형태.Text);
			}
		}

		private bool Chk화폐단위
		{
			get
			{
				return !Checker.IsEmpty(this.cbo화폐단위, this.lbl화폐단위.Text);
			}
		}

		private bool Chk단가유형
		{
			get
			{
				return !Checker.IsEmpty(this.cbo단가유형, this.lbl단가유형.Text);
			}
		}

		private bool Chk과세구분
		{
			get
			{
				return !Checker.IsEmpty(this.cbo부가세구분, this.lblVAT구분.Text);
			}
		}

		private bool Chk수주일자
		{
			get
			{
				return Checker.IsValid(this.dtp수주일자, true, this.lbl수주일자.Text);
			}
		}

		private bool Chk영업그룹
		{
			get
			{
				return !Checker.IsEmpty(this.ctx영업그룹, this.DD("영업그룹"));
			}
		}

		private bool Chk담당자
		{
			get
			{
				return !Checker.IsEmpty(this.ctx담당자, this.DD("담당자"));
			}
		}

		private bool Use부가세포함
		{
			get
			{
				return this.수주Config.부가세포함단가사용() && D.GetString(this.cbo부가세.SelectedValue) == "Y";
			}
		}

		private bool Chk매출일자
		{
			get
			{
				return Checker.IsValid(this.dtp매출일자, true, this.lbl매출일자.Text);
			}
		}

		private bool Chk수금예정일자
		{
			get
			{
				return Checker.IsValid(this.dtp수금예정일자, true, this.lbl수금예정일자.Text);
			}
		}

		private enum 메세지
		{
			이미수주확정되어수정삭제가불가능합니다,
		}

		public P_CZ_SA_SO_WINTEC()
		{
			if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
				StartUp.Certify(this);

			InitializeComponent();

			this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
			this.DataChanged += new EventHandler(this.Page_DataChanged);

			this._header = new FreeBinding();
			this._header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
			this._header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

			this.disCount_YN = BASIC.GetSAENV("002");

			this._수주번호 = string.Empty;
		}

		public P_CZ_SA_SO_WINTEC(string noSo)
		  : this()
		{
			this._수주번호 = noSo;
		}

		public P_CZ_SA_SO_WINTEC(PageBaseConst.CallType pageCallType, string idMemo)
		  : this()
		{
			this._수주번호 = this._biz.GetNoSo(idMemo);
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this._flexH.BeginSetting(1, 1, false);

			this._flexH.SetCol("CD_PLANT", "공장", 140, true);
			this._flexH.SetCol("CD_ITEM", "품목코드", 120, true);
			this._flexH.SetCol("NM_ITEM", "품목명", 120, false);
			this._flexH.SetCol("EN_ITEM", "품목명(영)", false);
			this._flexH.SetCol("STND_ITEM", "규격", 65, false);
			this._flexH.SetCol("STND_DETAIL_ITEM", "세부규격", false);
			this._flexH.SetCol("UNIT_SO", "단위", 65, false);

			this._flexH.SetCol("DT_EXPECT", "최초납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("QT_SO", "수량", 60, 17, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("CD_EXCH", "화폐단위", false);

			if (this._biz.Get특수단가적용 == 특수단가적용.중량단가)
			{
				this._flexH.SetCol("UNIT_WEIGHT", "중량단위", 70, false);
				this._flexH.SetCol("WEIGHT", "중량", 60, 17, false, typeof(decimal));
				this._flexH.Cols["WEIGHT"].Format = "#,###,###.####";
				this._flexH.SetCol("UM_OPT", "중량단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			}

			this._flexH.SetCol("NUM_USERDEF1", "사용자정의1", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexH.Cols["NUM_USERDEF1"].Visible = false;
			this._flexH.SetCol("NUM_USERDEF2", "사용자정의2", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexH.Cols["NUM_USERDEF2"].Visible = false;
			this._flexH.SetCol("UM_SO", "단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexH.SetCol("AM_SO", "금액", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_WONAMT", "원화금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("AM_VAT", "부가세", 100, 17, true, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("AMVAT_SO", "합계금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);

			if (this.수주Config.부가세포함단가사용())
				this._flexH.SetCol("UMVAT_SO", "부가세포함단가", 100, 17, true, typeof(decimal), FormatTpType.UNIT_COST);

			if (this.disCount_YN == "Y")
			{
				this._flexH.SetCol("UM_BASE", "기준단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
				this._flexH.SetCol("RT_DSCNT", "할인율", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			}

			this._flexH.SetCol("UNIT_IM", "관리단위", 65, false);

			if (Sa_Global.Two_Unit_Mng)
				this._flexH.SetCol("QT_IM", "관리수량", 65, 17, true, typeof(decimal), FormatTpType.QUANTITY);
			else
				this._flexH.SetCol("QT_IM", "관리수량", 65, 17, false, typeof(decimal), FormatTpType.QUANTITY);

			this._flexH.SetCol("CD_SL", "창고코드", 80, true);
			this._flexH.SetCol("NM_SL", "창고명", 120, false);
			this._flexH.SetCol("TP_ITEM", "품목타입", false);
			this._flexH.SetCol("UNIT_SO_FACT", "수주단위수량", false);
			this._flexH.SetCol("LT_GI", "출하LT", false);
			this._flexH.SetCol("GI_PARTNER", "납품처코드", 120, true);
			this._flexH.SetCol("LN_PARTNER", "납품처명", 200, false);
			this._flexH.SetCol("NO_PROJECT", "프로젝트코드", 120, true);
			this._flexH.SetCol("NM_PROJECT", "프로젝트명", 140, false);
			this._flexH.SetCol("NO_PO_PARTNER", "거래처PO번호", 140, true);
			this._flexH.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100, 3, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("DC1", "비고", 100, true);
			this._flexH.SetCol("DC2", "변경비고", 100, true);
			this._flexH.SetCol("FG_MODEL", "도면구분", 70, false);
			this._flexH.SetCol("FG_USE", "수주용도", 100, true);
			this._flexH.SetCol("NM_MANAGER1", "품목담당자", 70, false);

			if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
			{
				this._flexH.SetCol("UM_INV", "재고단가", 100, 17, false, typeof(decimal), FormatTpType.UNIT_COST);
				this._flexH.SetCol("AM_PROFIT", "예상이익", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
			}

			if (this._biz.Get과세변경유무 == "Y")
			{
				this._flexH.SetCol("TP_VAT", "VAT구분", 80, true);
				this._flexH.SetCol("RT_VAT", "VAT율", 70, 17, false, typeof(decimal), FormatTpType.QUANTITY);
			}

			if (Sa_Global.SoL_CdCc_ModifyYN == "Y")
			{
				this._flexH.SetCol("CD_CC", "코스트 센터", 100, true);
				this._flexH.SetCol("NM_CC", "코스트센터명", 120, false);
			}

			this._flexH.SetCol("NO_IO_MGMT", "관련수불번호", 100);
			this._flexH.SetCol("NO_IOLINE_MGMT", "관련수불라인번호", 100, false, typeof(decimal));

			if (this._biz.WH적용 == "100")
			{
				this._flexH.SetCol("CD_WH", "W/H코드", 100, false);
				this._flexH.SetCol("NM_WH", "W/H명", 100, false);
			}

			Column column = this._flexH.Cols["NO_IO_MGMT"];
			bool 수주반품사용여부;
			this._flexH.Cols["NO_IOLINE_MGMT"].Visible = 수주반품사용여부 = this._biz.수주반품사용여부;
			int num = 수주반품사용여부 ? 1 : 0;
			column.Visible = num != 0;
			this._flexH.SetCol("NO_SO_ORIGINAL", "원천수주번호", 100, false);
			this._flexH.SetCol("SEQ_SO_ORIGINAL", "원천수주항번", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("YN_ATP", "ATP적용여부", false);

			this._flexH.SetCol("SL_QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexH.SetCol("QT_USEINV", "가용재고", 0, false, typeof(decimal), FormatTpType.QUANTITY);


			this._flexH.SetCol("NUM_USERDEF3", "신규제작소요일", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexH.SetCol("NUM_USERDEF4", "사용자정의4", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexH.Cols["NUM_USERDEF4"].Visible = false;
			this._flexH.SetCol("NUM_USERDEF5", "사용자정의5", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexH.Cols["NUM_USERDEF5"].Visible = false;
			this._flexH.SetCol("NUM_USERDEF6", "사용자정의6", 80, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexH.Cols["NUM_USERDEF6"].Visible = false;

			this._flexH.SetCol("TXT_USERDEF1", "TEXT사용자정의1", 150, true);
			this._flexH.Cols["TXT_USERDEF1"].Visible = false;
			
			this._flexH.SetCol("TXT_USERDEF2", "TEXT사용자정의2", 150, true);
			this._flexH.Cols["TXT_USERDEF2"].Visible = false;

			this._flexH.SetCol("TXT_USERDEF3", "자재번호", 100, true);
			this._flexH.SetCol("TXT_USERDEF4", "도장COLOR", 100, true);
			this._flexH.SetCol("TXT_USERDEF5", "납품장소", 100, true);
			this._flexH.SetCol("TXT_USERDEF6", "호선번호", 100, true);
			this._flexH.SetCol("TXT_USERDEF7", "도면번호(수주)", 100, true);

			this._flexH.SetCol("TXT_USERDEF8", "TEXT사용자정의8", 100, true);
			this._flexH.Cols["TXT_USERDEF8"].Visible = false;
			this._flexH.SetCol("TXT_USERDEF9", "TEXT사용자정의9", 100, true);
			this._flexH.Cols["TXT_USERDEF9"].Visible = false;
			this._flexH.SetCol("TXT_USERDEF10", "TEXT사용자정의10", 100, true);
			this._flexH.Cols["TXT_USERDEF10"].Visible = false;
			this._flexH.SetCol("TXT_USERDEF11", "TEXT사용자정의11", 100, true);
			this._flexH.Cols["TXT_USERDEF11"].Visible = false;

			if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y")
			{
				this._flexH.SetCol("SEQ_PROJECT", "UNIT 항번", 100, false, typeof(decimal));
				this._flexH.SetCol("CD_UNIT", "UNIT 코드", 100, true);
				this._flexH.SetCol("NM_UNIT", "UNIT 명", 100, false);
				this._flexH.SetCol("STND_UNIT", "UNIT 규격", 100, false);
			}

			this._flexH.SetCol("TP_IV", "매출형태", 120, false);
			this._flexH.Cols["TP_IV"].Visible = false;
			this._flexH.SetCol("MAT_ITEM", "재질", false);

			if (BASIC.GetMAEXC("SET품 사용유무") == "Y")
			{
				this._flexH.SetCol("CD_ITEM_REF", "SET품목", 120, false);
				this._flexH.SetCol("NM_ITEM_REF", "SET품명", 120, false);
				this._flexH.SetCol("STND_ITEM_REF", "SET규격", 120, false);
			}
			
			if (BASIC.GetMAEXC("배차사용유무") == "Y")
				this._flexH.SetCol("YN_PICKING", "배송여부", 80, true, CheckTypeEnum.Y_N);

			this._flexH.SetCol("FG_USE2", "수주용도2", 100, true);
			this._flexH.SetCol("CLS_ITEM", "품목계정", false);
			this._flexH.SetCol("GRP_ITEM", "품목군", false);
			this._flexH.SetCol("GRP_ITEMNM", "품목군명", false);
			this._flexH.SetCol("GRP_MFG", "제품군", false);
			this._flexH.SetCol("NM_GRP_MFG", "제품군명", false);

			this._flexH.SetCol("CD_USERDEF1", "선급검사기관1", 100, true);
			this._flexH.SetCol("CD_USERDEF2", "선급검사기관2", 100, true);
			this._flexH.SetCol("CD_USERDEF3", "엔진타입", 100, true);

			this._flexH.SetCol("STA_SO1", "수주상태", false);

			if (App.SystemEnv.PMS사용)
			{
				this._flexH.SetCol("ID_MEMO", "PMS ID", 100, false);
				this._flexH.SetCol("CD_WBS", "WBS코드", 100, false);
				this._flexH.SetCol("NO_SHARE", "업무공유번호", 100, false);
				this._flexH.SetCol("NO_ISSUE", "이슈번호", 100, false);
			}

			this._flexH.SetCol("SEQ_SO", "순번", false);
			this._flexH.SetCol("NO_MODEL", "UCODE", 100, false);

			if (this._biz.WH적용 == "100")
			{
				this._flexH.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
																												  "NM_ITEM",
																												  "STND_ITEM",
																												  "STND_DETAIL_ITEM",
																												  "UNIT_SO",
																												  "UNIT_IM",
																												  "TP_ITEM",
																												  "CD_SL",
																												  "NM_SL",
																												  "UNIT_SO_FACT",
																												  "LT_GI",
																												  "WEIGHT",
																												  "UNIT_WEIGHT",
																												  "YN_ATP",
																												  "CUR_ATP_DAY",
																												  "CD_WH",
																												  "NM_WH",
																												  "GRP_MFG",
																												  "NM_GRP_MFG",
																												  "GRP_ITEM",
																												  "GRP_ITEMNM",
																												  "NO_MODEL" }, new string[] { "CD_ITEM",
																																			   "NM_ITEM",
																																			   "STND_ITEM",
																																			   "STND_DETAIL_ITEM",
																																			   "UNIT_SO",
																																			   "UNIT_IM",
																																			   "TP_ITEM",
																																			   "CD_GISL",
																																			   "NM_GISL",
																																			   "UNIT_SO_FACT",
																																			   "LT_GI",
																																			   "WEIGHT",
																																			   "UNIT_WEIGHT",
																																			   "YN_ATP",
																																			   "CUR_ATP_DAY",
																																			   "CD_WH",
																																			   "NM_WH",
																																			   "GRP_MFG",
																																			   "NM_GRP_MFG",
																																			   "GRP_ITEM",
																																			   "GRP_ITEMNM",
																																			   "NO_MODEL" }, ResultMode.SlowMode);

				this._flexH.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL",
																										    "NM_SL",
																										    "CD_WH",
																										    "NM_WH" }, new string[] { "CD_SL",
																																	  "NM_SL",
																																	  "CD_WH",
																																	  "NM_WH" }, ResultMode.SlowMode);
			}
			else
			{
				this._flexH.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
																												  "NM_ITEM",
																												  "STND_ITEM",
																												  "STND_DETAIL_ITEM",
																												  "UNIT_SO",
																												  "UNIT_IM",
																												  "TP_ITEM",
																												  "CD_SL",
																												  "NM_SL",
																												  "UNIT_SO_FACT",
																												  "LT_GI",
																												  "WEIGHT",
																												  "UNIT_WEIGHT",
																												  "YN_ATP",
																												  "CUR_ATP_DAY",
																												  "GRP_MFG",
																												  "NM_GRP_MFG",
																												  "GRP_ITEM",
																												  "GRP_ITEMNM",
																												  "NO_MODEL" }, new string[] { "CD_ITEM",
																																			   "NM_ITEM",
																																			   "STND_ITEM",
																																			   "STND_DETAIL_ITEM",
																																			   "UNIT_SO",
																																			   "UNIT_IM",
																																			   "TP_ITEM",
																																			   "CD_GISL",
																																			   "NM_GISL",
																																			   "UNIT_SO_FACT",
																																			   "LT_GI",
																																			   "WEIGHT",
																																			   "UNIT_WEIGHT",
																																			   "YN_ATP",
																																			   "CUR_ATP_DAY",
																																			   "GRP_MFG",
																																			   "NM_GRP_MFG",
																																			   "GRP_ITEM",
																																			   "GRP_ITEMNM",
																																			   "NO_MODEL" }, ResultMode.SlowMode);

				this._flexH.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, new string[] { "SL_QT_INV",
																																												  "QT_USEINV",
																																												  "QT_EXP",
																																												  "QT_AVA",
																																												  "NO_LOT" }, ResultMode.SlowMode);
			}

			this._flexH.SetCodeHelpCol("GI_PARTNER", HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER", "LN_PARTNER" }, new string[] { "CD_TPPTR", "NM_TPPTR" });
			this._flexH.SetCodeHelpCol("CD_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }, new string[] { "CD_CC", "NM_CC" });

			if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y")
			{
				this._flexH.SetCodeHelpCol("NO_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT",
																											 "NM_PROJECT",
																											 "SEQ_PROJECT",
																											 "CD_UNIT",
																											 "NM_UNIT",
																											 "STND_UNIT" }, new string[] { "NO_PROJECT",
																																		   "NM_PROJECT",
																																		   "SEQ_PROJECT",
																																		   "CD_PJT_ITEM",
																																		   "NM_PJT_ITEM",
																																		   "PJT_ITEM_STND" }, new string[] { "NO_PROJECT",
																																											 "NM_PROJECT",
																																											 "SEQ_PROJECT",
																																											 "CD_UNIT",
																																											 "NM_UNIT",
																																											 "STND_UNIT" }, ResultMode.SlowMode);
				this._flexH.SetCodeHelpCol("CD_UNIT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT",
																										  "NM_PROJECT",
																										  "SEQ_PROJECT",
																										  "CD_UNIT",
																										  "NM_UNIT",
																										  "STND_UNIT" }, new string[] { "NO_PROJECT",
																																	    "NM_PROJECT",
																																	    "SEQ_PROJECT",
																																	    "CD_PJT_ITEM",
																																	    "NM_PJT_ITEM",
																																	    "PJT_ITEM_STND" }, new string[] { "NO_PROJECT",
																																										  "NM_PROJECT",
																																										  "SEQ_PROJECT",
																																										  "CD_UNIT",
																																										  "NM_UNIT",
																																										  "STND_UNIT" }, ResultMode.SlowMode);
			}
			else
				this._flexH.SetCodeHelpCol("NO_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT", "NM_PROJECT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });

			this._flexH.SetCodeHelpCol("NM_MNGD1", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD1", "NM_MNGD1" }, new string[] { "CD_MNGD", "NM_MNGD" });
			this._flexH.SetCodeHelpCol("NM_MNGD2", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD2", "NM_MNGD2" }, new string[] { "CD_MNGD", "NM_MNGD" });
			this._flexH.SetCodeHelpCol("NM_MNGD3", HelpID.P_FI_MNGD_SUB, ShowHelpEnum.Always, new string[] { "CD_MNGD3", "NM_MNGD3" }, new string[] { "CD_MNGD", "NM_MNGD" });

			if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y")
				this._flexH.SetExceptEditCol(new string[] { "NM_ITEM",
														    "STND_ITEM",
														    "UNIT_SO",
														    "UNIT_IM",
														    "FG_MODEL",
														    "NM_SL",
														    "LN_PARTNER",
														    "RT_VAT",
														    "NM_CC",
														    "NM_MANAGER1",
														    "NO_IO_MGMT",
														    "NO_IOLINE_MGMT",
														    "NM_PROJECT",
														    "NO_SO_ORIGINAL",
														    "SEQ_SO_ORIGINAL",
														    "SEQ_PROJECT",
														    "NM_UNIT",
														    "STND_UNIT" });
			else
				this._flexH.SetExceptEditCol(new string[] { "NM_ITEM",
														    "STND_ITEM",
														    "UNIT_SO",
														    "UNIT_IM",
														    "FG_MODEL",
														    "NM_SL",
														    "LN_PARTNER",
														    "RT_VAT",
														    "NM_CC",
														    "NM_MANAGER1",
														    "NO_IO_MGMT",
														    "NO_IOLINE_MGMT",
														    "NM_PROJECT",
														    "NO_SO_ORIGINAL",
														    "SEQ_SO_ORIGINAL",
														    "CD_ITEM_PARTNER",
														    "NM_ITEM_PARTNER" });
			List<string> stringList1 = new List<string>();  
			stringList1.Add("CD_PLANT");
			stringList1.Add("CD_ITEM");
			stringList1.Add("DT_DUEDATE");
			stringList1.Add("DT_REQGI");
			stringList1.Add("TP_VAT");
			stringList1.Add("CD_CC");

			if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y")
				stringList1.Add("SEQ_PROJECT");

			stringList1.Add("TP_IV");
			this._flexH.VerifyNotNull = stringList1.ToArray();
			//this._flexH.VerifyCompare(this._flexH.Cols["QT_SO"], 0, OperatorEnum.Greater);
			//this._flexH.VerifyCompare(this._flexH.Cols["QT_IM"], 0, OperatorEnum.Greater);

			//if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000040") == "100")
			//	this._flexH.VerifyCompare(this._flexH.Cols["UM_SO"], 0, OperatorEnum.Greater);

			//this._flexH.VerifyCompare(this._flexH.Cols["UM_SO"], 0, OperatorEnum.GreaterOrEqual);
			//this._flexH.VerifyCompare(this._flexH.Cols["AM_SO"], 0, OperatorEnum.GreaterOrEqual);
			//this._flexH.VerifyCompare(this._flexH.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);
			this._flexH.SettingVersion = "0.0.0.1";
			this._flexH.EnterKeyAddRow = true;
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			this._flexH.AddMenuSeperator();

			ToolStripMenuItem toolStripMenuItem = this._flexH.AddPopup("관련 현황");
			this._flexH.AddMenuItem(toolStripMenuItem, "현재고조회", this.Menu_Click);
			this._flexH.AddMenuItem(toolStripMenuItem, "창고별재고인쇄", this.Menu_Click);
			List<string> stringList2 = new List<string>();

			stringList2.Add("UM_SO");
			stringList2.Add("RT_VAT");
			stringList2.Add("NUM_USERDEF1");

			if (this.disCount_YN == "Y")
			{
				stringList2.Add("UM_BASE");
				stringList2.Add("RT_DSCNT");
			}

			if (this._biz.Get특수단가적용 == 특수단가적용.중량단가)
			{
				stringList2.Add("WEIGHT");
				stringList2.Add("UM_OPT");
			}

			if (this.수주Config.부가세포함단가사용())
				stringList2.Add("UMVAT_SO");

			if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y")
				stringList2.Add("SEQ_PROJECT");

			this._flexH.SetExceptSumCol(stringList2.ToArray());
			this._flexH.SetDummyColumn(new string[1] { "AM_PROFIT" });
			
			if (BASIC.GetMAEXC("수주등록-단가,금액수정설정") == "100")
			{
				this._flexH.Cols["UM_SO"].AllowEditing = false;
				this._flexH.Cols["AM_SO"].AllowEditing = false;
				this._flexH.Cols["AM_WONAMT"].AllowEditing = false;
				this._flexH.Cols["AM_VAT"].AllowEditing = false;
				this._flexH.Cols["AMVAT_SO"].AllowEditing = false;
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.원그리드적용하기();

			this.dtp수주일자.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			this.dtp수주일자.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
			this.dtp납기일.Text = Global.MainFrame.GetStringToday;

			DataTable code = MA.GetCode("MA_B000040");
			code.PrimaryKey = new DataColumn[] { code.Columns["CODE"] };

			SetControl setControl = new SetControl();
			setControl.SetCombobox(this.cbo화폐단위, MA.GetCode("MA_B000005"));

			string maenv = BASIC.GetMAENV("YN_MFG_AUTH");

			DataTable dataTable;
			dataTable = MA.GetCode("SA_B000021", true);

			setControl.SetCombobox(this.cbo단가유형, dataTable);
			setControl.SetCombobox(this.cbo부가세구분, code);
			setControl.SetCombobox(this.cbo결재방법, MA.GetCode("SA_B000002", true));
			setControl.SetCombobox(this.cbo운송방법, MA.GetCode("TR_IM00008", true));
			setControl.SetCombobox(this.cbo부가세, MA.GetCodeUser(new string[] { "Y", "N" }, new string[] { "포함", "별도" }));
			setControl.SetCombobox(this.cbo반품사유, MA.GetCode("SA_B000064", true));
			setControl.SetCombobox(this.cbo공장, MA.GetCode("MA_PLANT_AUTH"));
			setControl.SetCombobox(this.cbo결제형태, MA.GetCode("TR_IM00004", true));
			setControl.SetCombobox(this.cbo포장형태, MA.GetCode("TR_IM00011", true));
			setControl.SetCombobox(this.cbo운송방법, MA.GetCode("TR_IM00008", true));
			setControl.SetCombobox(this.cbo운송형태, MA.GetCode("TR_IM00009", true));
			setControl.SetCombobox(this.cbo원산지, MA.GetCode("MA_B000020", true));
			setControl.SetCombobox(this.cbo가격조건, MA.GetCode("TR_IM00002", true));
			setControl.SetCombobox(this.cbo계정처리유형, this.GetComboData(new string[] { "N;MA_AISPOSTH;100" }).Tables[0]);
			
			this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;

			if (this._biz.Get과세변경유무 == "Y")
				this._flexH.SetDataMap("TP_VAT", code.Copy(), "CODE", "NAME");

			this._flexH.SetDataMap("CD_PLANT", MA.GetCode("MA_PLANT_AUTH").Copy(), "CODE", "NAME");
			this._flexH.SetDataMap("UNIT_SO", MA.GetCode("MA_B000004", true), "CODE", "NAME");
			this._flexH.SetDataMap("UNIT_IM", MA.GetCode("MA_B000004", true), "CODE", "NAME");
			this._flexH.SetDataMap("FG_USE", MA.GetCode("SA_B000057", true), "CODE", "NAME");

			this._flexH.SetDataMap("FG_USE2", MA.GetCode("SA_B000063", true), "CODE", "NAME");
			this._flexH.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005", true), "CODE", "NAME");
			this._flexH.SetDataMap("TP_IV", this.GetComboData(new string[] { "N;MA_AISPOSTH;100" }).Tables[0], "CODE", "NAME");
			this._flexH.SetDataMap("CLS_ITEM", MA.GetCode("MA_B000010"), "CODE", "NAME");

			this._flexH.SetDataMap("STA_SO1", MA.GetCode("SA_B000016"), "CODE", "NAME");

			this._flexH.SetDataMap("CD_USERDEF1", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME");
			this._flexH.SetDataMap("CD_USERDEF2", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME");
			this._flexH.SetDataMap("CD_USERDEF3", MA.GetCode("CZ_WIN0003", true), "CODE", "NAME");

			this.ctx창고적용.Clear();
			DataSet dataSet = this._biz.Search("#%#%");

			this._header.SetBinding(dataSet.Tables[0], this.tabControl);
			this._header.ClearAndNewRow();
			this._flexH.Binding = dataSet.Tables[1];

			this.화폐단위셋팅();

			this.계산서처리Default셋팅();
			this.ControlVisibleSetting();
		}

		private void InitEvent()
		{
			this._flexH.HelpClick += new EventHandler(this._flex_HelpClick);
			this._flexH.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
			this._flexH.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
			this._flexH.StartEdit += new RowColEventHandler(this._flex_StartEdit);
			this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);

			this.btn견적적용.Click += new EventHandler(this.btn견적적용_Click);
			this.ctx창고적용.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.btn창고적용.Click += new EventHandler(this.btn_apply_Click);
			this.btn거래처PO적용.Click += new EventHandler(this.btn거래처PO적용_Click);
			this.btn납품처적용.Click += new EventHandler(this.btn적용_Click);
			this.btn납기일적용.Click += new EventHandler(this.btn납기일적용_Click);
			this.btn프로젝트적용.Click += new EventHandler(this.btn프로젝트적용_Click);
			this.btn할인율적용.Click += new EventHandler(this.btn할인율적용_Click);
			this.btnBOM적용.Click += new EventHandler(this.btnBOM적용_Click);
			this.btn소요자재추가.Click += new EventHandler(this.btn소요자재추가_Click);
			this.btn소요자재삭제.Click += new EventHandler(this.btn소요자재삭제_Click);
			this.btn품목전개.Click += new EventHandler(this.btn품목전개_Click);
			this.btn결제조건.Click += new EventHandler(this.btn결제조건_Click);
            this.cbo화폐단위.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo부가세구분.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
			this.cbo부가세.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
			this.cbo공장.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
			this.ctx입고처.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.btn계정처리유형변경.Click += new EventHandler(this.btn계정처리유형변경_Click);
			this.cur공급가액.Validated += new EventHandler(this.cur공급가액_Validated);
			this.cur부가세액.Validated += new EventHandler(this.cur부가세액_Validated);

			this.btn담당자정보적용.Click += new EventHandler(this.btn담당자정보적용_Click);
			this.btn메일전송.Click += new EventHandler(this.btn메일전송_Click);
			this.btn영업기회적용.Click += new EventHandler(this.btn영업기회적용_Click);
			this.ctx거래처.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);

			this.ctx거래처.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx수주형태.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx수주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx담당자.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx영업그룹.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx영업그룹.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx프로젝트.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx프로젝트.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx납품처.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx창고적용.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
			this.btn추가.Click += new EventHandler(this.btn추가_Click);
			this.btn엑셀업로드.Click += new EventHandler(this.엑셀업로드_Click);
			this.btn배송지주소.Click += new EventHandler(this.btn배송지주소_Click);
			this.btn상품적용.Click += new EventHandler(this.btn상품적용_Click);
			this.btnATPCHECK.Click += new EventHandler(this.btnATPCHECK_Click);
			this.btn출하적용.Click += new EventHandler(this.출하적용_Click);
			this.btn단가적용.Click += new EventHandler(this.btn단가적용_Click);
			this.btn프로젝트적용1.Click += new EventHandler(this.btn프로젝트적용1_Click);
			this.btn첨부파일.Click += new EventHandler(this.btn첨부파일_Click);
		}
		#endregion

		#region 메인버튼 이벤트
		protected override bool IsChanged()
		{
			if (base.IsChanged())
				return true;
			return this.헤더변경여부;
		}

		private void Page_DataChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.IsChanged())
				{
					this.ToolBarSaveButtonEnabled = true;
					this.btnATPCHECK.Enabled = true;
				}

				if (this.추가모드여부)
				{
					this.btn추가.Enabled = true;
					this.ToolBarDeleteButtonEnabled = false;
				}
				else
					this.ToolBarDeleteButtonEnabled = true;

				if (this._flexH.HasNormalRow)
					return;

				this.dtp수주일자.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _header_ControlValueChanged(object sender, FreeBindingArgs e)
		{
			try
			{
				if (sender == null)
					return;

				if (((Control)sender).Name == this.cur환율.Name)
				{
					this.환율변경();
				}
				else if (((Control)sender).Name == this.dtp수주일자.Name)
				{
					this.수주일자변경();
				}

				if (this.IsChanged())
					this.ToolBarSaveButtonEnabled = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _header_JobModeChanged(object sender, FreeBindingArgs e)
		{
			try
			{
				if (e.JobMode == JobModeEnum.추가후수정)
				{
					this.pnl기본정보.Enabled = true;
					this.pnl부가정보.Enabled = true;
					this.pnl수출정보.Enabled = true;
					this.pnl매출정보.Enabled = true;
					this.txt멀티비고1.ReadOnly = false;
					this.txt멀티비고2.ReadOnly = false;
					this.txt수주번호.Enabled = true;
					this.txt수주번호.Text = string.Empty;
					this._header.CurrentRow["NO_SO"] = string.Empty;
					this._header.CurrentRow["NO_HST"] = 0;
					this.ctx수주형태.Enabled = true;
					this.cbo화폐단위.Enabled = true;
					this.cur환율.Enabled = true;

					if (this._구분 == "복사")
					{
						this.cbo부가세구분.Enabled = false;
						this.cbo부가세.Enabled = false;
					}
					else
					{
						this.cbo부가세구분.Enabled = true;

						if (this.수주Config.부가세포함단가사용())
							this.cbo부가세.Enabled = true;
					}

					this.ToolBarDeleteButtonEnabled = false;

					if (!(BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000030") == "100"))
						return;

					this._flexH.Cols["QT_SO"].AllowEditing = true;
				}
				else
				{
					if (!this._헤더수정여부)
					{
						this.pnl기본정보.Enabled = false;
						this.pnl부가정보.Enabled = false;
						this.txt멀티비고1.ReadOnly = true;
						this.txt멀티비고2.ReadOnly = true;
						this.pnl매출정보.Enabled = false;
					}
					else
					{
						this.pnl기본정보.Enabled = true;
						this.pnl부가정보.Enabled = true;
						this.txt멀티비고1.ReadOnly = false;
						this.txt멀티비고2.ReadOnly = false;
						this.pnl매출정보.Enabled = true;
					}

					this.txt수주번호.Enabled = false;
					this.ctx수주형태.Enabled = false;
					this.cbo부가세구분.Enabled = false;
					this.cbo부가세.Enabled = false;

					if (this._biz.해외적용건존재여부(this.txt수주번호.Text))
						this.pnl수출정보.Enabled = false;
					else
						this.pnl수출정보.Enabled = true;

					DataRow[] dataRowArray = this._flexH.DataTable.Select("STA_SO1 <> 'O'");

					if (dataRowArray == null || dataRowArray.Length == 0)
					{
						this.cbo화폐단위.Enabled = true;
						this.cur환율.Enabled = true;
					}
					else
					{
						this.cbo화폐단위.Enabled = false;
						this.cur환율.Enabled = false;
					}

					if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000030") == "100")
						this._flexH.Cols["QT_SO"].AllowEditing = false;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch())
					return;

				P_SA_SO_SCH_SUB pSaSoSchSub = new P_SA_SO_SCH_SUB();

				if (pSaSoSchSub.ShowDialog() == DialogResult.OK)
				{
					this._수주상태 = pSaSoSchSub.수주상태;
					this._구분 = pSaSoSchSub.구분;
					this._헤더수정여부 = pSaSoSchSub.헤더수정유무;
					this._거래구분 = pSaSoSchSub.거래구분;
					this._출하형태 = pSaSoSchSub.출하형태;
					this._매출형태 = pSaSoSchSub.매출형태;
					this._의뢰여부 = pSaSoSchSub.의뢰여부;
					this._출하여부 = pSaSoSchSub.출하여부;
					this._매출여부 = pSaSoSchSub.매출여부;
					this._수출여부 = pSaSoSchSub.수출여부;
					this._단가적용형태 = pSaSoSchSub.단가적용형태;
					this._헤더만복사 = pSaSoSchSub.헤더만복사;
					this.SearchSo(pSaSoSchSub.수주번호);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeAdd()
		{
			if (!base.BeforeAdd() || !this.MsgAndSave(PageActionMode.Search))
				return false;

			this.tabControl.SelectTab(this.tpg기본정보);

			return true;
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!this.BeforeAdd())
					return;

				this._구분 = string.Empty;
				this._헤더수정여부 = true;
				this.tabControl.Enabled = this.btn견적적용.Enabled = this.btn프로젝트적용1.Enabled = true;
				this._flexH.DataTable.Rows.Clear();
				this._flexH.AcceptChanges();
				this._header.ClearAndNewRow();

				this.계산서처리Default셋팅();
				this.Authority(true);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeDelete()
		{
			return base.BeforeDelete() && this.ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes;
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!this.BeforeDelete() || !this._biz.Delete((this.txt수주번호).Text))
					return;

				this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
				this._header.ClearAndNewRow();
				this._flexH.AcceptChanges();
				this.OnToolBarAddButtonClicked(sender, e);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave() || (!this.FieldCheck(string.Empty) || !this.Chk수주일자 || !this.Chk담당자))
				return false;

			else if (this._biz.Get과세변경유무 == "N" && this._매출자동여부 == "Y" && (!this.Chk매출일자 || !this.Chk수금예정일자))
				return false;

			if (!this.Verify() || !this.ChkBizarea())
				return false;

			new 수주관리.Setting().거래구분에따른과세구분(this._거래구분, D.GetString(this.cbo부가세구분.SelectedValue));

			if (BASIC.GetMAEXC("여신한도") == "200")
			{
				foreach (DataRow row in this._flexH.DataTable.Rows)
				{
					if (row.RowState != DataRowState.Deleted)
					{
						if (D.GetString(row["CD_SL"]) == string.Empty)
						{
							this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("창고") });
							return false;
						}
						if (D.GetString(row["NM_CUST_DLV"]) == string.Empty || D.GetString(row["CD_ZIP"]) == string.Empty || D.GetString(row["ADDR1"]) == string.Empty || D.GetString(row["TP_DLV"]) == string.Empty)
						{
							this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("배송정보") });
							return false;
						}
					}
				}
			}

			foreach (DataRow row in this._flexH.DataTable.Rows)
			{
				if (row.RowState != DataRowState.Deleted)
				{
					if (!this.수주Config.부가세변경() && D.GetString(this.cbo부가세구분.SelectedValue) != D.GetString(row["TP_VAT"]))
					{
						this.ShowMessage("헤더와 라인의 과세구분이 일치하지 않습니다.");
						return false;
					}

					decimal tpvat = BASIC.GetTPVAT(D.GetString(row["TP_VAT"]));

					if (D.GetDecimal(row["RT_VAT"]) != tpvat)
					{
						this.ShowMessage("라인부과세율 정보가 잘못되었습니다. \n\n 라인부과세율 정보를 확인하세요.");
						return false;
					}

					if (D.GetString(row["CD_CC"]) == string.Empty)
					{
						if (this._flexH.Cols["CD_CC"].Caption != string.Empty)
							this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this._flexH.Cols["CD_CC"].Caption });
						else
							this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("C/C코드") });
						return false;
					}
				}
			}
			return (!(this._biz.GetATP사용여부 == "001") || this.ATP체크로직(true)) && (!this.수주Config.결제조건도움창사용() || this.Chk결제조건());
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				this._flexH.Focus();

				if (!this.MsgAndSave(PageActionMode.Save))
					return;

				this.CallSo(this.txt수주번호.Text);
				this.ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!this._flexH.HasNormalRow && !this.추가모드여부)
			{
				this.OnToolBarDeleteButtonClicked(null, null);
				return true;
			}

			if (!this.BeforeSave() || !base.SaveData())
				return false;

			string empty1 = string.Empty;
			string 수주번호;

			if (this.추가모드여부)
			{
				if (D.GetString(this.txt수주번호.Text) == string.Empty)
				{
					수주번호 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "SA", this.Ctrl(), this.dtp수주일자.Text.Substring(0, 6));
					this.txt수주번호.Text = 수주번호;
				}
				else
				{
					if (!this.VerifyNoSo())
						return false;

					수주번호 = this.txt수주번호.Text;
				}

				this._header.CurrentRow["NO_SO"] = 수주번호;
			}
			else
				수주번호 = this.txt수주번호.Text;

			DataTable dtLot = new DataTable();
			DataTable dtSerial = new DataTable();
			DataTable changes1 = this._header.GetChanges();
			DataTable changes2 = this._flexH.GetChanges();
			DataTable changes3 = this._flexL.GetChanges();

			if (this._구분 == "복사")
			{
				string[] strArray = new string[3];
				this._수주상태 = !(D.GetString(this._biz.거래구분(this.ctx수주형태.CodeValue, D.GetString((this.cbo부가세구분).SelectedValue))[2]) == "Y") ? "O" : "R";
			}

			if (changes1 != null)
				changes1.Rows[0]["FG_TRACK"] = "M";

			if (changes1 == null && changes2 == null && changes3 == null)
				return true;

			if (changes2 != null)
			{
				string maexc = BASIC.GetMAEXC("여신한도");

				int num2;
				if (this._수주상태 != "R")
					num2 = 1;
				else
					num2 = 0;

				if (num2 == 0)
				{
					if (this._거래구분 == "001")
					{
						if (this._biz.GetExcCredit == "300")
						{
							if (!this._biz.CheckCreditExec(this.ctx거래처.CodeValue, D.GetString(this.cbo화폐단위.SelectedValue), Unit.원화금액(DataDictionaryTypes.SA, !(D.GetString(this.cbo화폐단위.SelectedValue) == "000") ? D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_SO)", string.Empty)) : D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_WONAMT) + SUM(AM_VAT)", string.Empty)))))
								return false;
						}
						else
						{
							decimal num3 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_WONAMT) + SUM(AM_VAT)", string.Empty));

							if (!this._biz.CheckCredit(this.ctx거래처.CodeValue, Unit.원화금액(DataDictionaryTypes.SA, num3), this._의뢰여부, this._출하여부, ref this._수주상태))
								return false;
						}
					}

					if (!this.IsAgingCheck())
						return false;
				}

				foreach (DataRow row in changes2.Rows)
				{
					if (row.RowState != DataRowState.Deleted && row.RowState == DataRowState.Added)
					{
						if (D.GetString(row["NO_PROJECT"]) == string.Empty)
							row["NO_PROJECT"] = this.ctx프로젝트.CodeValue;

						row["FG_TRACK"] = "SO";
						row["STA_SO1"] = this._수주상태;
					}
				}

				if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000001") == "001")
				{
					StringBuilder stringBuilder = new StringBuilder();
					bool flag = true;
					string str2 = this.DD("항번") + " " + this.DD("품번") + "                 " + this.DD("품목명") + "                               " + this.DD("최저단가") + "       " + this.DD("단가");
					stringBuilder.AppendLine(str2);
					string str3 = "-".PadRight(92, '-');
					stringBuilder.AppendLine(str3);

					foreach (DataRow row in changes2.Rows)
					{
						if (row.RowState != DataRowState.Deleted && D.GetDecimal(row["UM_SO"]) < D.GetDecimal(row["NUM_USERDEF1"]))
						{
							string str4 = D.GetString(row["SEQ_SO"]).PadRight(4, ' ') + " " + D.GetString(row["CD_ITEM"]).PadRight(20, ' ') + " " + D.GetString(row["NM_ITEM"]).PadRight(36, ' ') + " " + D.GetString(D.GetInt(row["NUM_USERDEF1"])).PadRight(14, ' ') + " " + D.GetString(D.GetInt(row["UM_SO"]));
							stringBuilder.AppendLine(str4);
							flag = false;
						}
					}

					if (!flag && this.ShowDetailMessage("수주단가가 최저단가보다 적은 품목이 있습니다. 그래도 저장하시겠습니까?", string.Empty, stringBuilder.ToString(), "QY2") == DialogResult.No)
						return false;
				}
			}

			if (this._출하여부 == "Y" && this._자동승인여부 == "Y")
			{
				for (int index = (this._flexH.Rows).Fixed; index < (this._flexH.Rows).Count; ++index)
				{
					if (D.GetString(this._flexH[index, "CD_SL"]) == string.Empty)
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this._flexH.Cols["CD_SL"].Caption });
						this._flexH.Select(index, (this._flexH.Cols["CD_SL"]).Index);
						return false;
					}
				}

				DataRow[] dataRowArray1 = this._flexH.DataTable.Select("FG_SERNO = '002'");

				if (Duzon.ERPU.MF.Common.Config.MA_ENV.LOT관리 && dataRowArray1 != null && dataRowArray1.Length != 0)
				{
					dtLot = this._biz.dtLot_Schema(dtLot);

					foreach (DataRow dataRow1 in dataRowArray1)
					{
						if (dataRow1.RowState != DataRowState.Deleted)
						{
							DataRow dataRow2 = dtLot.NewRow();

							dataRow2["NO_IO"] = string.Empty;
							dataRow2["NO_IOLINE"] = dataRow1["SEQ_SO"];
							dataRow2["NO_ISURCV"] = string.Empty;
							dataRow2["NO_GIR"] = string.Empty;
							dataRow2["DT_DUEDATE"] = dataRow1["DT_DUEDATE"];
							dataRow2["FG_TRANS"] = this._거래구분;
							dataRow2["CD_QTIOTP"] = this._출하형태;
							dataRow2["NM_QTIOTP"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, this._출하형태 })["NM_QTIOTP"];
							dataRow2["DT_IO"] = this.dtp수주일자.Text;
							dataRow2["CD_SL"] = dataRow1["CD_SL"];
							dataRow2["NM_SL"] = dataRow1["NM_SL"];
							dataRow2["CD_ITEM"] = dataRow1["CD_ITEM"];
							dataRow2["NM_ITEM"] = dataRow1["NM_ITEM"];
							dataRow2["STND_ITEM"] = dataRow1["STND_ITEM"];
							dataRow2["UNIT"] = dataRow1["UNIT_SO"];
							dataRow2["UNIT_IM"] = dataRow1["UNIT_IM"];
							dataRow2["FG_IO"] = !(this._반품여부 == "Y") ? "010" : "041";
							dataRow2["QT_GIR"] = dataRow1["QT_SO"];
							dataRow2["UNIT_SO_FACT"] = dataRow1["UNIT_SO_FACT"];
							dataRow2["QT_GIR_IM"] = dataRow1["QT_IM"];
							dataRow2["QT_IO"] = dataRow1["QT_IM"];
							dataRow2["QT_GOOD_INV"] = dataRow1["QT_IM"];
							dataRow2["CD_PLANT"] = dataRow1["CD_PLANT"];
							dataRow2["CD_PJT"] = dataRow1["NO_PROJECT"];
							dataRow2["NO_PROJECT"] = dataRow1["NO_PROJECT"];
							dataRow2["NM_PROJECT"] = dataRow1["NM_PROJECT"];
							dataRow2["NO_EMP"] = this.ctx담당자.CodeValue;
							dataRow2["NO_LOT"] = "YES";
							dataRow2["NO_SERL"] = "NO";
							dataRow2["NO_PSO_MGMT"] = (this.txt수주번호).Text;
							dataRow2["NO_PSOLINE_MGMT"] = dataRow1["SEQ_SO"];
							dataRow2["NO_IO_MGMT"] = dataRow1["NO_IO_MGMT"];
							dataRow2["NO_IOLINE_MGMT"] = dataRow1["NO_IOLINE_MGMT"];

							dtLot.Rows.Add(dataRow2.ItemArray);
						}
					}

					if (this._반품여부 == "N")
					{
						string[] strArray = new string[] { "N",
														   this.ctx프로젝트.CodeValue,
														   this.ctx프로젝트.CodeName };

						P_PU_LOT_SUB_I pPuLotSubI = new P_PU_LOT_SUB_I(dtLot, strArray);

						if (pPuLotSubI.ShowDialog() != DialogResult.OK)
							return false;

						dtLot = pPuLotSubI.dtL;
						DataRow[] dataRowArray2 = dtLot.Select(string.Empty, "NO_LOT DESC");
					}
					else if (this._biz.수주반품사용여부 && this._반품여부 == "Y")
					{
						P_PU_LOT_SUB_R pPuLotSubR = new P_PU_LOT_SUB_R(dtLot);

						if (pPuLotSubR.ShowDialog() != DialogResult.OK)
							return false;

						dtLot = pPuLotSubR.dtL;
						DataRow[] dataRowArray2 = dtLot.Select(string.Empty, "NO_LOT DESC");
					}

					if (dataRowArray1.Length != 0 && (dtLot == null || dtLot.Rows.Count == 0))
					{
						this.ShowMessage("LOT품목 수불이 발생하였으나 해당 LOT가 생성되지 않았습니다.");
						return false;
					}

					foreach (DataRow dataRow1 in dataRowArray1)
					{
						if (dataRow1.RowState != DataRowState.Deleted)
						{
							decimal num1 = 0;
							string filterExpression;
							string index;

							if (this._반품여부 == "N")
							{
								filterExpression = "출고항번 = " + D.GetDecimal(dataRow1["SEQ_SO"]);
								index = "QT_GOOD_MNG";
							}
							else
							{
								filterExpression = "NO_IOLINE = " + D.GetDecimal(dataRow1["SEQ_SO"]);
								index = "QT_IO";
							}

							foreach (DataRow dataRow2 in dtLot.Select(filterExpression))
							{
								if (dataRow2.RowState != DataRowState.Deleted)
								{
									if (D.GetString(dataRow1["CD_ITEM"]) != D.GetString(dataRow1["CD_ITEM"]))
									{
										this.ShowMessage("LOT품목과 수불품목이 일치하지 않습니다.");
										return false;
									}
									num1 += D.GetDecimal(dataRow2[index]);
								}
							}

							if (num1 != D.GetDecimal(dataRow1["QT_IM"]))
							{
								this.ShowMessage("LOT수량과 수불수량이 일치하지 않습니다.");
								return false;
							}
						}
					}
				}

				DataRow[] dataRowArray3 = this._flexH.DataTable.Select("FG_SERNO = '003'");

				if (Duzon.ERPU.MF.Common.Config.MA_ENV.시리얼사용 && dataRowArray3 != null && dataRowArray3.Length != 0)
				{
					dtSerial = this._biz.dtSerial_Schema(dtSerial);

					foreach (DataRow dataRow1 in dataRowArray3)
					{
						if (dataRow1.RowState != DataRowState.Deleted)
						{
							DataRow dataRow2 = dtSerial.NewRow();
							dataRow2["NO_IO"] = string.Empty;
							dataRow2["NO_IOLINE"] = dataRow1["SEQ_SO"];
							dataRow2["NO_ISURCV"] = string.Empty;
							dataRow2["NO_GIR"] = string.Empty;
							dataRow2["DT_DUEDATE"] = dataRow1["DT_DUEDATE"];
							dataRow2["FG_TRANS"] = this._거래구분;
							dataRow2["CD_QTIOTP"] = this._출하형태;
							dataRow2["NM_QTIOTP"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, this._출하형태 })["NM_QTIOTP"];
							dataRow2["DT_IO"] = this.dtp수주일자.Text;
							dataRow2["CD_SL"] = dataRow1["CD_SL"];
							dataRow2["NM_SL"] = dataRow1["NM_SL"];
							dataRow2["CD_ITEM"] = dataRow1["CD_ITEM"];
							dataRow2["NM_ITEM"] = dataRow1["NM_ITEM"];
							dataRow2["STND_ITEM"] = dataRow1["STND_ITEM"];
							dataRow2["UNIT"] = dataRow1["UNIT_SO"];
							dataRow2["UNIT_IM"] = dataRow1["UNIT_IM"];
							dataRow2["FG_IO"] = !(this._반품여부 == "Y") ? "010" : "041";
							dataRow2["QT_GIR"] = dataRow1["QT_SO"];
							dataRow2["UNIT_SO_FACT"] = dataRow1["UNIT_SO_FACT"];
							dataRow2["QT_GIR_IM"] = dataRow1["QT_IM"];
							dataRow2["QT_IO"] = dataRow1["QT_IM"];
							dataRow2["QT_GOOD_INV"] = dataRow1["QT_IM"];
							dataRow2["CD_PLANT"] = dataRow1["CD_PLANT"];
							dataRow2["CD_PJT"] = dataRow1["NO_PROJECT"];
							dataRow2["NO_PROJECT"] = dataRow1["NO_PROJECT"];
							dataRow2["NM_PROJECT"] = dataRow1["NM_PROJECT"];
							dataRow2["NO_EMP"] = this.ctx담당자.CodeValue;
							dataRow2["NO_LOT"] = "NO";
							dataRow2["NO_SERL"] = "YES";
							dataRow2["NO_PSO_MGMT"] = (this.txt수주번호).Text;
							dataRow2["NO_PSOLINE_MGMT"] = dataRow1["SEQ_SO"];
							dataRow2["NO_IO_MGMT"] = dataRow1["NO_IO_MGMT"];
							dataRow2["NO_IOLINE_MGMT"] = dataRow1["NO_IOLINE_MGMT"];
							dtSerial.Rows.Add(dataRow2.ItemArray);
						}
					}

					if (this._반품여부 == "N")
					{
						P_PU_SERL_SUB_I pPuSerlSubI = new P_PU_SERL_SUB_I(dtSerial);
						pPuSerlSubI.YN_Rev = BASIC.GetMAEXC("납품의뢰등록 시리얼예약-사용유무");

						if ((pPuSerlSubI).ShowDialog() != DialogResult.OK)
							return false;

						dtSerial = pPuSerlSubI.dtL;

						if (dtSerial != null && dtSerial.Rows.Count > 0)
							dtSerial.Columns.Add("CD_PLANT", typeof(string));
					}
					else if (this._biz.수주반품사용여부 && this._반품여부 == "Y")
					{
						P_PU_SERL_SUB_R pPuSerlSubR = new P_PU_SERL_SUB_R(dtSerial);

						if ((pPuSerlSubR).ShowDialog() != DialogResult.OK)
							return false;

						dtSerial = pPuSerlSubR.dtL;
					}

					if (dataRowArray3.Length != 0 && (dtSerial == null || dtSerial.Rows.Count == 0))
					{
						this.ShowMessage("SERIAL품목 수불이 발생하였으나 해당 SERIAL이 생성되지 않았습니다.");
						return false;
					}
				}
			}

			string SERIAL_FG_PS = null;

			if (this._반품여부 == "N")
				SERIAL_FG_PS = "2";
			else if (this._biz.수주반품사용여부 && this._반품여부 == "Y")
				SERIAL_FG_PS = "1";

			string[] strArr = new string[] { 수주번호,
											 this._수주상태,
											 this._거래구분,
											 this._출하형태,
											 this._매출형태,
											 this._의뢰여부,
											 this._출하여부,
											 this._매출여부,
											 this._수출여부,
											 this._구분,
											 this._반품여부,
											 this._매출자동여부,
											 this._자동승인여부 };

			if (!this._biz.Save(changes1, changes2, changes3, dtLot, strArr, dtSerial, SERIAL_FG_PS))
				return false;

			foreach (DataRow row in this._flexH.DataTable.Rows)
			{
				if (row.RowState != DataRowState.Deleted && D.GetString(row["STA_SO1"]) == string.Empty)
					row["STA_SO1"] = this._수주상태;
			}

			if (this._수주상태 == "R")
				this._헤더수정여부 = false;

			this._header.AcceptChanges();
			this._flexH.AcceptChanges();
			this._flexL.AcceptChanges();
			this._구분 = "적용";
			this.Page_DataChanged(null, null);
			this.btnATPCHECK.Enabled = false;
			this.tabControl.SelectTab(this.tpg기본정보);

			return true;
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if (this.추가모드여부)
					return;

				DataRow codeInfo = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx거래처.CodeValue });
				DataRow dataRow1 = this._biz.SerchBizarea(D.GetString(this._flexH[(this._flexH.Rows).Fixed, "CD_PLANT"]));
				DataRow dataRow2 = null;
				DataRow dataRow3 = null;
				DataRow dataRow4 = null;
				DataRow dataRow5 = null;
				DataRow dataRow6 = null;
				DataRow dataRow7 = null;
				DataRow dataRow8 = null;

				if (D.GetString(this.cbo원산지.SelectedValue) != string.Empty)
				{
					DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT NM_SYSDEF_E 
																				FROM MA_CODEDTL WITH(NOLOCK) 
																			    WHERE CD_COMPANY = '{0}' 
																				AND CD_FIELD = 'MA_B000020' 
																				AND CD_SYSDEF = '{1}'", MA.Login.회사코드, D.GetString(this.cbo원산지.SelectedValue)));

					if (dataTable != null && dataTable.Rows.Count > 0)
						dataRow2 = dataTable.Rows[0];
				}

				if (this.ctx거래처.CodeValue != string.Empty)
				{
					DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT NO_TEL1, 
																					   NO_FAX1, 
																					   CD_EMP_PARTNER, 
																					   E_MAIL 
																				FROM MA_PARTNER WITH(NOLOCK) 
																				WHERE CD_COMPANY = '{0}' 
																				AND CD_PARTNER = '{1}'", MA.Login.회사코드, this.ctx거래처.CodeValue));

					if (dataTable != null && dataTable.Rows.Count > 0)
						dataRow6 = dataTable.Rows[0];
				}

				if (this.ctx착하통지처.CodeValue != string.Empty)
				{
					DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT DC_ADS1_H, 
																					   DC_ADS1_D, 
																					   NO_TEL, 
																					   CD_EMP_PARTNER, 
																					   E_MAIL 
																				FROM MA_PARTNER WITH(NOLOCK) 
																				WHERE CD_COMPANY = '{0}' 
																				AND CD_PARTNER = '{1}'", MA.Login.회사코드, this.ctx착하통지처.CodeValue));

					if (dataTable != null && dataTable.Rows.Count > 0)
						dataRow3 = dataTable.Rows[0];
				}

				if (this.ctx운송사.CodeValue != null)
				{
					DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT LN_PARTNER, 
																					   DC_ADS1_H, 
																					   DC_ADS1_D 
																				FROM MA_PARTNER WITH(NOLOCK) 
																				WHERE CD_COMPANY = '{0}' 
																				AND CD_PARTNER = '{1}'", MA.Login.회사코드, this.ctx운송사.CodeValue));

					if (dataTable != null && dataTable.Rows.Count > 0)
						dataRow8 = dataTable.Rows[0];
				}

				if (this.ctx수하인.CodeValue != string.Empty)
					dataRow4 = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx수하인.CodeValue });

				if (this.ctx은행.CodeValue != string.Empty)
					dataRow5 = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx은행.CodeValue });

				if (this.ctx수출자.CodeValue != string.Empty)
					dataRow7 = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx수출자.CodeValue });

				ReportHelper reportHelper1;
				ReportHelper reportHelper2 = reportHelper1 = new ReportHelper("R_SA_SO_RPT_001", "수주서");

				reportHelper2.SetData("NO_SO", (this.txt수주번호).Text);
				reportHelper2.SetData("DT_SO", (this.dtp수주일자).Text);
				reportHelper2.SetData("PARTNER", this.ctx거래처.CodeValue);
				reportHelper2.SetData("CD_PARTNER", this.ctx거래처.CodeName);
				reportHelper2.SetData("CD_SALEGRP", this.ctx영업그룹.CodeName);
				reportHelper2.SetData("NO_KOR", this.ctx담당자.CodeName);
				reportHelper2.SetData("TP_SO", this.ctx수주형태.CodeName);
				reportHelper2.SetData("CD_EXCH", D.GetString((this.cbo화폐단위).SelectedValue));
				reportHelper2.SetData("NM_CD_EXCH", (this.cbo화폐단위).Text);
				reportHelper2.SetData("RT_EXCH", D.GetString(this.cur환율.DecimalValue));
				reportHelper2.SetData("TP_PRICE", (this.cbo단가유형).Text);
				reportHelper2.SetData("NO_PROJECT", this.ctx프로젝트.CodeName);
				reportHelper2.SetData("TP_VAT", (this.cbo부가세구분).Text);
				reportHelper2.SetData("RT_VAT", D.GetString(this.cur부가세율.DecimalValue));
				reportHelper2.SetData("FG_VAT", (this.cbo부가세).Text);
				reportHelper2.SetData("CD_DEPT", D.GetString(this._header.CurrentRow["CD_DEPT"]));
				reportHelper2.SetData("NM_DEPT", D.GetString(this._header.CurrentRow["NM_DEPT"]));

				if ((this.rdo계산서처리일괄).Checked)
					reportHelper2.SetData("FG_TAXP", (this.rdo계산서처리일괄).Text);
				else
					reportHelper2.SetData("FG_TAXP", (this.rdo계산서처리건별).Text);

				reportHelper2.SetData("DC_RMK", (this.txt비고).Text);
				reportHelper2.SetData("FG_BILL", (this.cbo결재방법).Text);
				reportHelper2.SetData("FG_TRANSPORT", (this.cbo운송방법).Text);
				reportHelper2.SetData("NO_CONTRACT", (this.txt계약번호).Text);
				reportHelper2.SetData("NO_PO_PARTNER", (this.txt거래처PO).Text);
				reportHelper2.SetData("NM_CEO", D.GetString(codeInfo["NM_CEO"]));
				reportHelper2.SetData("NO_TEL", D.GetString(codeInfo["NO_TEL"]));
				reportHelper2.SetData("NO_FAX", D.GetString(codeInfo["NO_FAX"]));
				reportHelper2.SetData("NO_COMPANY", D.GetString(codeInfo["NO_COMPANY"]));
				reportHelper2.SetData("NM_PTR", D.GetString(codeInfo["NM_PTR"]));
				reportHelper2.SetData("TP_JOB", D.GetString(codeInfo["TP_JOB"]));
				reportHelper2.SetData("CLS_JOB", D.GetString(codeInfo["CLS_JOB"]));
				reportHelper2.SetData("DC_ADS1_H", D.GetString(codeInfo["DC_ADS1_H"]));
				reportHelper2.SetData("DC_ADS1_D", D.GetString(codeInfo["DC_ADS1_D"]));
				reportHelper2.SetData("DC_ADS1", D.GetString(codeInfo["DC_ADS1_H"]) + " " + D.GetString(codeInfo["DC_ADS1_D"]));
				reportHelper2.SetData("AM_WONAMT_SUM", D.GetString(this._flexH[1, "AM_WONAMT"]));
				reportHelper2.SetData("AM_VAT_SUM", D.GetString(this._flexH[1, "AM_VAT"]));
				reportHelper2.SetData("AMVAT_SO_SUM", D.GetString(this._flexH[1, "AMVAT_SO"]));
				reportHelper2.SetData("NM_BUSI", D.GetString(Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_CODEDTL, new object[] { MA.Login.회사코드, "PU_C000016", this._거래구분 })["NM_SYSDEF"]));
				reportHelper2.SetData("AM_SO_SUM", D.GetString(this._flexH[1, "AM_SO"]));
				reportHelper2.SetData("QT_SO_SUM", D.GetString(this._flexH[1, "QT_SO"]));
				reportHelper2.SetData("NM_EXPORT", this.ctx수출자.CodeName);
				reportHelper2.SetData("NM_PRODUCT", this.ctx제조자.CodeName);
				reportHelper2.SetData("COND_TRANS", this.txt인도조건.Text);
				reportHelper2.SetData("NM_INSPECT", this.txt검사기관.Text);
				reportHelper2.SetData("DESTINATION", this.txt목적지.Text);
				reportHelper2.SetData("NM_COND_PAY", this.cbo결제형태.Text);
				reportHelper2.SetData("COND_DAYS", this.cur결제일.Text);
				reportHelper2.SetData("DT_EXPIRY", this.dtp유효일자.Text);
				reportHelper2.SetData("NM_TP_PACKING", this.cbo포장형태.Text);
				reportHelper2.SetData("NM_TP_TRANSPORT", this.cbo운송형태.Text);
				reportHelper2.SetData("NM_CD_ORIGIN", this.cbo원산지.Text);
				reportHelper2.SetData("PORT_LOADING", this.txt선적항.Text);
				reportHelper2.SetData("PORT_ARRIVER", this.txt도착항.Text);
				reportHelper2.SetData("NM_COND_PRICE", this.cbo가격조건.Text);
				reportHelper2.SetData("NM_NOTIFY", this.ctx착하통지처.CodeName);
				reportHelper2.SetData("NM_CONSIGNEE", this.ctx수하인.CodeName);
				reportHelper2.SetData("DC_RMK_TEXT", this.txt멀티비고1.Text);
				reportHelper2.SetData("DC_RMK_TEXT2", this.txt멀티비고2.Text);
				reportHelper2.SetData("DC_RMK1", this.txt비고1.Text);
				reportHelper2.SetData("NO_BIZAREA", D.GetString(dataRow1["NO_BIZAREA"]));
				reportHelper2.SetData("NM_BIZAREA", D.GetString(dataRow1["NM_BIZAREA"]));
				reportHelper2.SetData("NM_MASTER_BIZAREA", D.GetString(dataRow1["NM_MASTER"]));
				reportHelper2.SetData("ADS_H_BIZAREA", D.GetString(dataRow1["ADS_H"]));
				reportHelper2.SetData("ADS_D_BIZAREA", D.GetString(dataRow1["ADS_D"]));
				reportHelper2.SetData("ADS_BIZAREA", D.GetString(dataRow1["ADS_H"]) + " " + D.GetString(dataRow1["ADS_D"]));
				reportHelper2.SetData("TP_JOB_BIZAREA", D.GetString(dataRow1["TP_JOB"]));
				reportHelper2.SetData("CLS_JOB_BIZAREA", D.GetString(dataRow1["CLS_JOB"]));
				reportHelper2.SetData("NO_TEL_BIZAREA", D.GetString(dataRow1["NO_TEL"]));

				if (dataRow2 != null)
					reportHelper2.SetData("NM_SYSDEF_E", D.GetString(dataRow2["NM_SYSDEF_E"]));

				if (dataRow3 != null)
				{
					reportHelper2.SetData("NOTIFY_DC_ADS1_H", D.GetString(dataRow3["DC_ADS1_H"]));
					reportHelper2.SetData("NOTIFY_DC_ADS1_D", D.GetString(dataRow3["DC_ADS1_D"]));
					reportHelper2.SetData("NOTIFY_NO_TEL", D.GetString(dataRow3["NO_TEL"]));
					reportHelper2.SetData("NOTIFY_CD_EMP_PARTNER", D.GetString(dataRow3["CD_EMP_PARTNER"]));
					reportHelper2.SetData("NOTIFY_E_MAIL", D.GetString(dataRow3["E_MAIL"]));
				}

				if (dataRow4 != null)
				{
					reportHelper2.SetData("CONSIGNEE_DC_ADS1_H", D.GetString(dataRow4["DC_ADS1_H"]));
					reportHelper2.SetData("CONSIGNEE_DC_ADS1_D", D.GetString(dataRow4["DC_ADS1_D"]));
				}

				if (dataRow5 != null)
				{
					reportHelper2.SetData("CD_BANK_SO", this.ctx은행.CodeValue);
					reportHelper2.SetData("NM_BANK_SO", this.ctx은행.CodeName);
					reportHelper2.SetData("BANK_SO_NM_TEXT", D.GetString(dataRow5["NM_TEXT"]));
				}

				if (dataRow6 != null)
				{
					reportHelper2.SetData("NO_TEL1", D.GetString(dataRow6["NO_TEL1"]));
					reportHelper2.SetData("NO_FAX1", D.GetString(dataRow6["NO_FAX1"]));
					reportHelper2.SetData("CD_EMP_PARTNER", D.GetString(dataRow6["CD_EMP_PARTNER"]));
					reportHelper2.SetData("E_MAIL", D.GetString(dataRow6["E_MAIL"]));
				}

				if (dataRow7 != null)
				{
					reportHelper2.SetData("EXPORT_DC_ADS1_H", D.GetString(dataRow7["DC_ADS1_H"]));
					reportHelper2.SetData("EXPORT_DC_ADS1_D", D.GetString(dataRow7["DC_ADS1_D"]));
				}

				if (dataRow8 != null)
				{
					reportHelper2.SetData("NM_TRANSPORT", D.GetString(dataRow8["LN_PARTNER"]));
					reportHelper2.SetData("TRANSPORT_DC_ADS1_H", D.GetString(dataRow8["DC_ADS1_H"]));
					reportHelper2.SetData("TRANSPORT_DC_ADS1_D", D.GetString(dataRow8["DC_ADS1_D"]));
				}

				DataTable dt = this._biz.Search_Print(this.txt수주번호.Text);

				dt.AcceptChanges();
				reportHelper2.SetDataTable(dt, 1);
				reportHelper2.SetDataTable(dt, 2);
				reportHelper2.Print();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
		{
			this._수주번호 = D.GetString(e.Args[0]);
			this.InitPaint();
		}
		#endregion

		#region 그리드 이벤트
		private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			try
			{
				if (D.GetDecimal(this._header.CurrentRow["NO_HST"]) > 0)
				{
					e.Cancel = true;
				}
				else
				{
					if (!this.추가모드여부 && this._flexH.RowState(e.Row) != DataRowState.Added)
					{
						if (D.GetString(this._flexH["STA_SO1"]) != string.Empty && D.GetString(this._flexH["STA_SO1"]) != "O")
						{
							e.Cancel = true;
							return;
						}
					}
					switch (this._flexH.Cols[e.Col].Name)
					{
						case "CD_ITEM":
							if (D.GetString(this._flexH["CD_PLANT"]) == string.Empty)
							{
								this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "공장" });
								e.Cancel = true;
								break;
							}

							if (D.GetDecimal(this._flexH["SEQ_PROJECT"]) != 0)
							{
								this.ShowMessage("프로젝트적용 받은 내용이므로 수정하실 수 없습니다.");
								e.Cancel = true;
								break;
							}

							if (this.disCount_YN == "Y" && this._biz.Get할인율적용 == 수주관리.할인율적용.거래처그룹별_품목군할인율 && !this.Chk거래처)
							{
								e.Cancel = true;
								break;
							}

							e.Parameter.P09_CD_PLANT = D.GetString(this._flexH[e.Row, "CD_PLANT"]);
							break;
						case "CD_SL":
							e.Parameter.P09_CD_PLANT = D.GetString(this._flexH[e.Row, "CD_PLANT"]);
							break;
						case "GI_PARTNER":
							e.Parameter.P14_CD_PARTNER = this.ctx거래처.CodeValue;
							break;
						case "NO_PROJECT":
							if (D.GetDecimal(this._flexH["SEQ_PROJECT"]) != 0)
							{
								this.ShowMessage("프로젝트적용 받은 내용이므로 수정하실 수 없습니다.");
								e.Cancel = true;
								break;
							}

							if (!this.Chk거래처 || !this.Chk영업그룹)
							{
								e.Cancel = true;
								break;
							}

							e.Parameter.P41_CD_FIELD1 = "프로젝트";
							e.Parameter.P14_CD_PARTNER = this.ctx거래처.CodeValue;
							e.Parameter.P63_CODE3 = this.ctx거래처.CodeName;
							e.Parameter.P17_CD_SALEGRP = this.ctx영업그룹.CodeValue;
							e.Parameter.P62_CODE2 = this.ctx영업그룹.CodeName;
							break;
						case "NM_MNGD1":
							e.Parameter.P34_CD_MNG = "A21";
							break;
						case "NM_MNGD2":
							e.Parameter.P34_CD_MNG = "A22";
							break;
						case "NM_MNGD3":
							e.Parameter.P34_CD_MNG = "A25";
							break;
						case "CD_UNIT":
							if (D.GetString(this._flexH["NO_PROJECT"]) != string.Empty)
							{
								e.Parameter.P64_CODE4 = D.GetString(this._flexH["NO_PROJECT"]);
								break;
							}
							break;
						case "NM_USERDEF1":
							e.Parameter.P41_CD_FIELD1 = "MA_B000102";
							break;
						case "NM_USERDEF2":
							e.Parameter.P41_CD_FIELD1 = "MA_B000103";
							break;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
		{
			try
			{
				HelpReturn result = e.Result;

				switch (this._flexH.Cols[e.Col].Name)
				{
					case "CD_ITEM":
						if (e.Result.DialogResult == DialogResult.Cancel)
							break;

						this.품목추가(e.Row, result.Rows, false);
						break;
					case "CD_SL":
						this._flexH["SL_QT_INV"] = BASIC.GetQtInv(D.GetString(this._flexH["CD_PLANT"]), D.GetString(result.Rows[0]["CD_SL"]), D.GetString(this._flexH["CD_ITEM"]), this.dtp수주일자.Text);
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			try
			{
				string str = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
				string editData = ((FlexGrid)sender).EditData;

				if (str.ToUpper() == editData.ToUpper())
					return;

				string name = ((FlexGrid)sender).Cols[e.Col].Name;

				if (this.so_Price == "U" && name == "UM_SO")
				{
					if (D.GetDecimal(this._flexH["UM_SO_ORI"]) == 0)
						this._flexH["UM_SO_ORI"] = D.GetDecimal(str);

					decimal num1 = D.GetDecimal(this._flexH["UM_SO_ORI"]);

					if (D.GetDecimal(editData) < num1)
					{
						this.ShowMessage(string.Format("수정단가가 기준단가보다 작을수 없습니다.\r\n\r\n( 기준단가 : {0}, 수정단가 = {1} )", num1, D.GetDecimal(editData)));
						e.Cancel = true;
						return;
					}
				}

				switch (this._flexH.Cols[e.Col].Name)
				{
					case "CD_PLANT":
						this._flexH["CD_ITEM"] = string.Empty;
						this._flexH["NM_ITEM"] = string.Empty;
						this._flexH["STND_ITEM"] = string.Empty;
						this._flexH["UNIT_SO"] = string.Empty;
						this._flexH["UNIT_IM"] = string.Empty;
						this._flexH["TP_ITEM"] = string.Empty;
						this._flexH["DT_REQGI"] = string.Empty;
						this._flexH["QT_IM"] = 0;
						this._flexH["CD_SL"] = string.Empty;
						this._flexH["NM_SL"] = string.Empty;
						this._flexH["AM_PROFIT"] = 0;
						this.소요자재그리드CLEAR();
						break;
					case "DT_DUEDATE":
						if (!D.StringDate.IsValidDate(editData, false, this._flexH.Cols[name].Caption))
						{
							e.Cancel = true;
							return;
						}

						if (editData != string.Empty)
						{
							if (D.GetDecimal(this.dtp수주일자.Text) > D.GetDecimal(editData))
							{
								this.ShowMessageKor("납기일은 수주일보다 이전일수 없습니다.");
								this._flexH["DT_DUEDATE"] = str;
								this._flexH["DT_REQGI"] = str;
								e.Cancel = true;
								return;
							}
							this._flexH["DT_REQGI"] = new 수주관리.Calc().출하예정일조회(editData, D.GetInt(this._flexH["LT_GI"]));
						}
						break;
					case "DT_REQGI":
						if (!D.StringDate.IsValidDate(editData, false, this._flexH.Cols[name].Caption))
						{
							e.Cancel = true;
							return;
						}

						if (editData != string.Empty && D.GetDecimal(this.dtp수주일자.Text) > D.GetDecimal(editData))
						{
							this.ShowMessageKor("출하예정일은 수주일보다 이전일수 없습니다.");
							this._flexH["DT_REQGI"] = str;
							e.Cancel = true;
							return;
						}
						break;
					case "UM_OPT":
						this.특수단가사용시단가계산(this._flexH.Row);
						this.Calc금액변경(e.Row);
						this.Calc부가세포함(e.Row);

						if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
							this._biz.예상이익(this._flexH, e.Row);

						this.IsInvAmCalc();
						break;
					case "TP_VAT":
						this._flexH["RT_VAT"] = BASIC.GetTPVAT(editData);
						this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) * (D.GetDecimal(this._flexH["RT_VAT"]) / 100));
						this.Calc부가세포함(e.Row);
						this.IsInvAmCalc();
						break;
					case "QT_SO":
						this._flexH["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(editData) * D.GetDecimal(this._flexH["UNIT_SO_FACT"]));
						this._flexH["UNIT_SO_FACT"] = D.GetDecimal(this._flexH["UNIT_SO_FACT"]) == 0 ? 1 : this._flexH["UNIT_SO_FACT"];

						if (this.Use부가세포함)
						{
							this._flexH["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(editData) * D.GetDecimal(this._flexH["UMVAT_SO"]));

							if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
								this._flexH["AM_WONAMT"] = decimal.Round(D.GetDecimal(this._flexH["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(this._flexH["RT_VAT"]))), MidpointRounding.AwayFromZero);
							else
								this._flexH["AM_WONAMT"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, D.GetDecimal(this._flexH["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(this._flexH["RT_VAT"])))));

							this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AMVAT_SO"]) - D.GetDecimal(this._flexH["AM_WONAMT"]));
							this._flexH["AM_SO"] = (this.cur환율.DecimalValue == 0 ? 0 : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) / this.cur환율.DecimalValue));
							this._flexH["UM_SO"] = (D.GetDecimal(this._flexH["QT_SO"]) == 0 ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_SO"]) / D.GetDecimal(this._flexH["QT_SO"])));
						}
						else
						{
							this.Calc금액변경(e.Row);
							this.Calc부가세포함(e.Row);
						}

						if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
							this._biz.예상이익(this._flexH, e.Row);

						this.IsInvAmCalc();
						break;
					case "UM_SO":
						this.Calc금액변경(e.Row);
						this.Calc부가세포함(e.Row);

						if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
							this._biz.예상이익(this._flexH, e.Row);

						this.IsInvAmCalc();
						break;
					case "AM_SO":
						if (D.GetDecimal(this._flexH["QT_SO"]) != 0)
							this._flexH["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(editData) / D.GetDecimal(this._flexH["QT_SO"]));
						else
							this._flexH["UM_SO"] = 0;

						this._flexH["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(editData) * this.cur환율.DecimalValue);
						this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) * (D.GetDecimal(this._flexH["RT_VAT"]) / 100));
						this.Calc부가세포함(e.Row);

						if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
							this._biz.예상이익(this._flexH, e.Row);

						this.IsInvAmCalc();
						break;
					case "RT_DSCNT":
						if (D.GetDecimal(this._flexH["UM_BASE"]) != 0)
							this._flexH["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["UM_BASE"]) - D.GetDecimal(this._flexH["UM_BASE"]) * D.GetDecimal(editData) / 100);
						else
							this._flexH["UM_SO"] = 0;

						this.Calc금액변경(e.Row);
						this.Calc부가세포함(e.Row);

						if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
							this._biz.예상이익(this._flexH, e.Row);

						this.IsInvAmCalc();
						break;
					case "UM_BASE":
						if (D.GetDecimal(this._flexH["RT_DSCNT"]) != 0)
							this._flexH["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(editData) - D.GetDecimal(editData) * D.GetDecimal(this._flexH["RT_DSCNT"]) / 100);
						else
							this._flexH["UM_SO"] = 0;

						this.Calc금액변경(e.Row);
						this.Calc부가세포함(e.Row);

						if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
							this._biz.예상이익(this._flexH, e.Row);

						this.IsInvAmCalc();
						break;
					case "NUM_USERDEF1":
					case "NUM_USERDEF2":
						this.IsInvAmCalc();
						break;
					case "AM_VAT":
						decimal num8 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) * (D.GetDecimal(this._flexH["RT_VAT"]) / 100));

						if (Math.Abs(D.GetDecimal(editData) - num8) > num8 * new decimal(3, 0, 0, false, (byte)1))
						{
							this.ShowMessage("부가세를 원부가세의 (±)30% 초과 수정 할 수 없습니다.");
							this._flexH["AM_VAT"] = D.GetDecimal(str);
							e.Cancel = true;
							return;
						}

						this.Calc부가세포함(e.Row);
						this.IsInvAmCalc();
						break;
					case "UMVAT_SO":
						this._flexH["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["QT_SO"]) * D.GetDecimal(editData));

						if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
							this._flexH["AM_WONAMT"] = decimal.Round(D.GetDecimal(this._flexH["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(this._flexH["RT_VAT"]))), MidpointRounding.AwayFromZero);
						else
							this._flexH["AM_WONAMT"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, D.GetDecimal(this._flexH["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(this._flexH["RT_VAT"])))));

						this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AMVAT_SO"]) - D.GetDecimal(this._flexH["AM_WONAMT"]));
						this._flexH["AM_SO"] = (this.cur환율.DecimalValue == 0 ? 0 : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) / this.cur환율.DecimalValue));
						this._flexH["UM_SO"] = (D.GetDecimal(this._flexH["QT_SO"]) == 0 ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_SO"]) / D.GetDecimal(this._flexH["QT_SO"])));

						this.IsInvAmCalc();
						break;
					case "AM_WONAMT":
						this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) * (D.GetDecimal(this._flexH["RT_VAT"]) / 100));
						this._flexH["AM_SO"] = (this.cur환율.DecimalValue == 0 ? 0 : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) / this.cur환율.DecimalValue));
						this._flexH["UM_SO"] = (D.GetDecimal(this._flexH["QT_SO"]) == 0 ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_SO"]) / D.GetDecimal(this._flexH["QT_SO"])));

						this.Calc부가세포함(e.Row);
						this.IsInvAmCalc();
						break;
					case "CD_USERDEF4":
						DataRow dataRow1 = this._biz.수주유형(this.LoginInfo.CompanyCode, D.GetString(this._flexH["CD_PLANT"]), D.GetString(this._flexH["CD_ITEM"]), D.GetString(this.ctx거래처.CodeValue), D.GetString(this.cbo화폐단위.SelectedValue), D.GetString(this._flexH["CD_USERDEF4"]));

						if (dataRow1 != null)
							this._flexH["UM_SO"] = dataRow1["UM_ITEM"];
						else
							this._flexH["UM_SO"] = "0";

						this.Calc금액변경(e.Row);
						this.Calc부가세포함(e.Row);

						if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
							this._biz.예상이익(this._flexH, e.Row);

						this.IsInvAmCalc();
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_HelpClick(object sender, EventArgs e)
		{
			try
			{
				if (D.GetDecimal(this._header.CurrentRow["NO_HST"]) > 0 || !this.추가모드여부 && (D.GetString(this._flexH["STA_SO1"]) != string.Empty && D.GetString(this._flexH["STA_SO1"]) != "O"))
					return;

				switch (this._flexH.Cols[this._flexH.Col].Name)
				{
					case "UM_SO":
						if (this.so_Price == "Y")
							break;

						if (this.Use부가세포함)
							break;

						P_SA_UM_HISTORY_SUB pSaUmHistorySub1 = new P_SA_UM_HISTORY_SUB(this.ctx거래처.CodeValue, this.ctx거래처.CodeName, this.ctx수주형태.CodeValue, this.ctx수주형태.CodeName, D.GetString(this._flexH["CD_PLANT"]), D.GetString(this._flexH["CD_ITEM"]), D.GetString(this._flexH["NM_ITEM"]), D.GetString(this.cbo화폐단위.SelectedValue));

						if (this._biz.Get특수단가적용 == 특수단가적용.중량단가)
							pSaUmHistorySub1.Set출하기준단가 = false;

						if (pSaUmHistorySub1.ShowDialog() == DialogResult.OK)
						{
							if (this.disCount_YN == "N")
							{
								this._flexH["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, pSaUmHistorySub1.단가);
							}
							else if (this.disCount_YN == "Y")
							{
								this._flexH["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, pSaUmHistorySub1.단가);
								this._flexH["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["UM_BASE"]) - D.GetDecimal(this._flexH["UM_BASE"]) * D.GetDecimal(this._flexH["RT_DSCNT"]) / 100);
							}

							this.Calc금액변경(this._flexH.Row);
							this.Calc부가세포함(this._flexH.Row);
							break;
						}
						break;
					case "UMVAT_SO":
						if (this.so_Price == "Y" || !this.Use부가세포함)
							break;

						P_SA_UM_HISTORY_SUB pSaUmHistorySub2 = new P_SA_UM_HISTORY_SUB(this.ctx거래처.CodeValue, this.ctx거래처.CodeName, this.ctx수주형태.CodeValue, this.ctx수주형태.CodeName, D.GetString(this._flexH["CD_PLANT"]), D.GetString(this._flexH["CD_ITEM"]), D.GetString(this._flexH["NM_ITEM"]), D.GetString(this.cbo화폐단위.SelectedValue));
						pSaUmHistorySub2.Set출하기준단가 = false;
						pSaUmHistorySub2.Set부가세포함단가 = true;

						if ((pSaUmHistorySub2).ShowDialog() == DialogResult.OK)
						{
							this._flexH["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, pSaUmHistorySub2.단가);
							this._flexH["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["QT_SO"]) * D.GetDecimal(this._flexH["UMVAT_SO"]));

							if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
								this._flexH["AM_WONAMT"] = decimal.Round(D.GetDecimal(this._flexH["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(this._flexH["RT_VAT"]))), MidpointRounding.AwayFromZero);
							else
								this._flexH["AM_WONAMT"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, D.GetDecimal(this._flexH["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(this._flexH["RT_VAT"])))));

							this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AMVAT_SO"]) - D.GetDecimal(this._flexH["AM_WONAMT"]));
							this._flexH["AM_SO"] = (this.cur환율.DecimalValue == 0 ? 0 : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) / this.cur환율.DecimalValue));
							this._flexH["UM_SO"] = (D.GetDecimal(this._flexH["QT_SO"]) == 0 ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_SO"]) / D.GetDecimal(this._flexH["QT_SO"])));
							break;
						}
						break;
					case "NO_SO_ORIGINAL":
						P_SA_SO_ORIGINAL_SUB pSaSoOriginalSub = new P_SA_SO_ORIGINAL_SUB(this.ctx거래처.CodeValue, this.ctx거래처.CodeName, D.GetString(this._flexH["CD_ITEM"]), D.GetString(this._flexH["NM_ITEM"]), D.GetString(this._flexH["CD_PLANT"]));

						if (pSaSoOriginalSub.ShowDialog() == DialogResult.OK)
						{
							this._flexH["NO_SO_ORIGINAL"] = D.GetString(pSaSoOriginalSub.원천수주데이터["NO_SO"]);
							this._flexH["SEQ_SO_ORIGINAL"] = D.GetDecimal(pSaSoOriginalSub.원천수주데이터["SEQ_SO"]);
							this._flexH["CD_ITEM"] = D.GetString(pSaSoOriginalSub.원천수주데이터["CD_ITEM"]);
							this._flexH["NM_ITEM"] = D.GetString(pSaSoOriginalSub.원천수주데이터["NM_ITEM"]);
							this._flexH["STND_ITEM"] = D.GetString(pSaSoOriginalSub.원천수주데이터["STND_ITEM"]);
							this._flexH["UNIT_SO"] = D.GetString(pSaSoOriginalSub.원천수주데이터["UNIT_SO"]);
							this._flexH["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(pSaSoOriginalSub.원천수주데이터["QT_SO"]));
							this._flexH["UNIT_IM"] = D.GetString(pSaSoOriginalSub.원천수주데이터["UNIT_IM"]);
							this._flexH["TP_ITEM"] = D.GetString(pSaSoOriginalSub.원천수주데이터["TP_ITEM"]);
							this._flexH["UNIT_SO_FACT"] = D.GetDecimal(pSaSoOriginalSub.원천수주데이터["UNIT_SO_FACT"]);
							this._flexH["LT_GI"] = D.GetDecimal(pSaSoOriginalSub.원천수주데이터["LT_GI"]);
							this._flexH["WEIGHT"] = D.GetDecimal(pSaSoOriginalSub.원천수주데이터["WEIGHT"]);
							this._flexH["UNIT_WEIGHT"] = D.GetString(pSaSoOriginalSub.원천수주데이터["UNIT_WEIGHT"]);
							this._flexH["YN_ATP"] = D.GetString(pSaSoOriginalSub.원천수주데이터["YN_ATP"]);
							this._flexH["CUR_ATP_DAY"] = D.GetDecimal(pSaSoOriginalSub.원천수주데이터["CUR_ATP_DAY"]);
							this._flexH["CD_SL"] = string.Empty;
							this._flexH["NM_SL"] = string.Empty;
							this._flexH["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["QT_SO"]) * (D.GetDecimal(this._flexH["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(this._flexH["UNIT_SO_FACT"])));
							string CD_CC;
							string NM_CC;
							this.CC조회(D.GetString(pSaSoOriginalSub.원천수주데이터["GRP_ITEM"]), D.GetString(this._flexH["CD_ITEM"]), D.GetString(this._flexH["CD_PLANT"]), out CD_CC, out NM_CC);
							this._flexH["CD_CC"] = CD_CC;
							this._flexH["NM_CC"] = NM_CC;
							break;
						}
						break;
					case "CD_ITEM_REF":
						if (BASIC.GetMAEXC("SET품 사용유무") != "Y")
							break;

						H_SA_SET_OPEN_SUB hSaSetOpenSub = new H_SA_SET_OPEN_SUB(D.GetString(this.cbo공장.SelectedValue), this.dtp수주일자.Text, D.GetString(this.cbo화폐단위.SelectedValue));

						if (hSaSetOpenSub.ShowDialog() != DialogResult.OK)
							break;

						this.Set품목셋팅(this._flexH.Rows.Count - 1, hSaSetOpenSub.GetData);
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				if (D.GetDecimal(this._header.CurrentRow["NO_HST"]) > 0)
				{
					e.Cancel = true;
				}
				else
				{
					if (!this.추가모드여부 && this._flexH.RowState(e.Row) != DataRowState.Added)
					{
						if (D.GetString(this._flexH["STA_SO1"]) != string.Empty && D.GetString(this._flexH["STA_SO1"]) != "O")
						{
							e.Cancel = true;
							return;
						}
					}

					switch (this._flexH.Cols[e.Col].Name)
					{
						case "UNIT_WEIGHT":
						case "WEIGHT":
						case "UM_INV":
						case "AM_PRIFT":
							e.Cancel = true;
							return;
						case "UM_SO":
							if (this.Use부가세포함)
							{
								e.Cancel = true;
								return;
							}

							if (this.so_Price == "Y")
							{
								this.ShowMessage("영업단가통제된 영업그룹입니다.");
								e.Cancel = true;
							}
							break;
						case "AM_SO":
							if (this.so_Price == "U")
							{
								e.Cancel = true;
								return;
							}

							if (this.Use부가세포함)
							{
								e.Cancel = true;
								return;
							}

							if (this.so_Price == "Y")
							{
								this.ShowMessage("영업단가통제된 영업그룹입니다.");
								e.Cancel = true;
							}
							break;
						case "AM_VAT":
							if (this.Use부가세포함)
								e.Cancel = true;
							break;
						case "UMVAT_SO":
							if (this.so_Price == "U")
							{
								e.Cancel = true;
								return;
							}

							if (!this.Use부가세포함)
								e.Cancel = true;

							if (this.so_Price == "Y")
							{
								this.ShowMessage("영업단가통제된 영업그룹입니다.");
								e.Cancel = true;
								break;
							}
							break;
						case "CD_ITEM":
							if (D.GetDecimal(this._flexH["SEQ_PROJECT"]) != 0)
							{
								this.ShowMessage("프로젝트적용 받은 내용이므로 수정하실 수 없습니다.");
								e.Cancel = true;
								return;
							}

							if (this._flexL.HasNormalRow)
							{
								this.ShowMessage("품목[" + D.GetString(this._flexH["CD_ITEM"]) + "]에 대한 소요자재가 존재합니다.\n\n소요자재 삭제 후 다시 시도하시기 바랍니다.");
								e.Cancel = true;
								return;
							}
							break;
						case "QT_SO":
							if (D.GetString(this._flexH["FG_USE"]) == "020")
							{
								e.Cancel = true;
								return;
							}
							break;
						case "UM_EX":
							if (this.so_Price == "Y")
							{
								this.ShowMessage("영업단가통제된 영업그룹입니다.");
								e.Cancel = true;
								break;
							}
							break;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Menu_Click(object sender, EventArgs e)
		{
			string cd_item_multi = string.Empty;

			for (int index = this._flexH.Rows.Fixed; index < this._flexH.Rows.Count; ++index)
				cd_item_multi = cd_item_multi + D.GetString(this._flexH[index, "CD_ITEM"]) + "|";

			switch (((ToolStripItem)sender).Name)
			{
				case "현재고조회":
					new P_PU_STOCK_SUB(D.GetString(this._flexH["CD_PLANT"]), cd_item_multi).ShowDialog();
					break;
				case "창고별재고인쇄":
					this.창고별현재고인쇄(cd_item_multi);
					break;
			}
		}
		#endregion

		#region 버튼 이벤트
		private void btn견적적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Chk거래처 || !this.Chk영업그룹 || (!this.Chk수주유형 || !this.Chk화폐단위) || !this.Chk과세구분)
					return;

				bool 헤더비고 = false;
				bool 라인비고 = false;
				DataRow get견적H;
				DataRow[] get견적D;
				DataRow[] dr견적D;

				if (this.일반추가() || this.출하적용건())
				{
					this.ShowMessage("견적적용이 아닌 데이터가 존재합니다.");
					return;
				}

				P_SA_ESTMT_SUB pSaEstmtSub = new P_SA_ESTMT_SUB();
				pSaEstmtSub.Set거래처코드 = this.ctx거래처.CodeValue;
				pSaEstmtSub.Set거래처명 = this.ctx거래처.CodeName;
				pSaEstmtSub.Set영업그룹코드 = this.ctx영업그룹.CodeValue;
				pSaEstmtSub.Set영업그룹명 = this.ctx영업그룹.CodeName;

				if (!this.ctx담당자.IsEmpty())
				{
					pSaEstmtSub.Set담당자코드 = this.ctx담당자.CodeValue;
					pSaEstmtSub.Set담당자명 = this.ctx담당자.CodeName;
				}

				pSaEstmtSub.Set수주형태코드 = this.ctx수주형태.CodeValue;
				pSaEstmtSub.Set수주형태명 = this.ctx수주형태.CodeName;
				pSaEstmtSub.Set부가세구분 = D.GetString(this.cbo부가세구분.SelectedValue);
				pSaEstmtSub.Set환종 = D.GetString(this.cbo화폐단위.SelectedValue);

				if (pSaEstmtSub.ShowDialog() != DialogResult.OK)
					return;

				get견적H = pSaEstmtSub.Get견적H;
				get견적D = pSaEstmtSub.Get견적D;
				dr견적D = pSaEstmtSub.Get견적D_CHK;
				헤더비고 = pSaEstmtSub.Get헤더비고;
				라인비고 = pSaEstmtSub.Get라인비고;

				this.pnl수출정보.Enabled = true;

				if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000030") == "000")
				{
					this.견적H셋팅(get견적H, 헤더비고);
					this.견적품목셋팅(get견적D, 라인비고);
				}
				else
				{
					this.견적품목셋팅(dr견적D, 라인비고);
					(this._flexH.Cols["QT_SO"]).AllowEditing = false;
				}

				this.IsInvAmCalc();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn프로젝트적용1_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.FieldCheck(this.btn프로젝트적용1.Name))
					return;

				if (this.견적적용건())
				{
					this.ShowMessage("견적적용건이 존재합니다.");
				}
				else
				{
					P_SA_SO_PRJ_SUB pSaSoPrjSub = new P_SA_SO_PRJ_SUB(new string[] { this.ctx프로젝트.CodeValue,
																					 this.ctx프로젝트.CodeName,
																					 this.ctx거래처.CodeValue,
																					 this.ctx거래처.CodeName,
																					 D.GetString(this.cbo부가세구분.SelectedValue) });

					DataSet dataSet = new DataSet();
					if (pSaSoPrjSub.ShowDialog() != DialogResult.OK)
						return;

					DataSet returnDataSet = pSaSoPrjSub.ReturnDataSet;
					if (returnDataSet == null || returnDataSet.Tables.Count == 0)
						return;

					DataRow row1 = returnDataSet.Tables[0].Rows[0];
					this.ctx프로젝트.SetCode(D.GetString(row1["NO_PROJECT"]), D.GetString(row1["NM_PROJECT"]));
					this.ctx거래처.SetCode(D.GetString(row1["CD_PARTNER"]), D.GetString(row1["LN_PARTNER"]));
					this.ctx영업그룹.SetCode(D.GetString(row1["CD_SALEGRP"]), D.GetString(row1["NM_SALEGRP"]));
					this.영업그룹변경시셋팅(this.ctx영업그룹.CodeValue);
					string str1 = D.GetString(row1["GI_PARTNER"]);
					string str2 = D.GetString(row1["GN_PARTNER"]);
					this.ctx납품처.CodeValue = str1 == string.Empty ? this.ctx거래처.CodeValue : str1;
					this.ctx납품처.CodeName = str2 == string.Empty ? this.ctx거래처.CodeName : str2;
					this.txt비고.Text = D.GetString(row1["DC_RMK"]);
					this._header.CurrentRow["DC_RMK"] = (this.txt비고).Text;

					if (this._biz.Get프로젝트적용 == "001")
					{
						this.ctx담당자.CodeValue = D.GetString(row1["NO_EMP"]);
						this.ctx담당자.CodeName = D.GetString(row1["NM_KOR"]);
						this._header.CurrentRow["NO_EMP"] = D.GetString(row1["NO_EMP"]);
						(this.txt비고).Text = D.GetString(row1["NM_PROJECT"]);
						this._header.CurrentRow["DC_RMK"] = D.GetString(row1["NM_PROJECT"]);
					}

					if (this.dtp납기일.Text == string.Empty)
						this.dtp납기일.Text = D.GetString(returnDataSet.Tables[1].Rows[0]["DT_DUEDATE"]);

					this._header.CurrentRow["NO_PROJECT"] = D.GetString(row1["NO_PROJECT"]);
					this._header.CurrentRow["NM_PROJECT"] = D.GetString(row1["NM_PROJECT"]);
					this._header.CurrentRow["CD_PARTNER"] = D.GetString(row1["CD_PARTNER"]);
					this._header.CurrentRow["LN_PARTNER"] = D.GetString(row1["LN_PARTNER"]);
					this._header.CurrentRow["CD_SALEGRP"] = D.GetString(row1["CD_SALEGRP"]);
					this._header.CurrentRow["NM_SALEGRP"] = D.GetString(row1["NM_SALEGRP"]);

					if ((D.GetString(row1["FG_VAT"]) != "Y") || (D.GetString(this.cbo부가세.SelectedValue) != "Y"))
					{
						this.cbo부가세.SelectedValue = "N";
						this._header.CurrentRow["FG_VAT"] = "N";
					}

					DataTable dataTable1 = this._flexH.DataTable.Clone();
					decimal num2 = this._flexH.DataView.Table.Rows.Count + 1;

					foreach (DataRow row2 in returnDataSet.Tables[1].Rows)
					{
						if (row2.RowState != DataRowState.Deleted)
						{
							DataRow row3 = dataTable1.NewRow();
							row3["SEQ_SO"] = num2;
							++num2;
							row3["NO_PROJECT"] = D.GetString(row2["NO_PROJECT"]);
							row3["NM_PROJECT"] = D.GetString(row1["NM_PROJECT"]);
							row3["SEQ_PROJECT"] = D.GetString(row2["SEQ_PROJECT"]);
							row3["CD_PLANT"] = D.GetString(row2["CD_PLANT"]);
							row3["CD_ITEM"] = D.GetString(row2["CD_ITEM"]);
							row3["NM_ITEM"] = D.GetString(row2["NM_ITEM"]);
							row3["STND_ITEM"] = D.GetString(row2["STND_ITEM"]);
							row3["UNIT_SO"] = D.GetString(row2["UNIT"]);
							row3["EN_ITEM"] = row2["EN_ITEM"];
							row3["STND_DETAIL_ITEM"] = row2["STND_DETAIL_ITEM"];
							row3["TP_ITEM"] = row2["TP_ITEM"];
							row3["GRP_MFG"] = row2["GRP_MFG"];
							row3["LT_GI"] = D.GetDecimal(row2["LT_GI"]);
							row3["WEIGHT"] = row2["WEIGHT"];
							row3["UNIT_WEIGHT"] = row2["UNIT_WEIGHT"];
							row3["FG_SERNO"] = row2["FG_SERNO"];
							row3["YN_ATP"] = row2["YN_ATP"];
							row3["CUR_ATP_DAY"] = row2["CUR_ATP_DAY"];
							row3["FG_MODEL"] = row2["FG_MODEL"];

							if (D.GetString(row2["DT_DUEDATE"]) == string.Empty)
							{
								if (this.dtp납기일.Text != string.Empty)
								{
									row3["DT_DUEDATE"] = this.dtp납기일.Text;
									row3["DT_REQGI"] = this._CommFun.DateAdd(this.dtp납기일.Text, "D", D.GetInt(row3["LT_GI"]) * -1);
								}
							}
							else
							{
								row3["DT_DUEDATE"] = D.GetString(row2["DT_DUEDATE"]);
								row3["DT_REQGI"] = this._CommFun.DateAdd(D.GetString(row2["DT_DUEDATE"]), "D", D.GetInt(row3["LT_GI"]) * -1);
							}

							row3["DC1"] = D.GetString(row2["DC_RMK9"]);
							string CD_CC;
							string NM_CC;

							if (this.수주Config.수주라인CC설정유형() == 수주관리.수주라인CC설정.프로젝트라인)
							{
								CD_CC = D.GetString(row2["CD_CC"]);
								NM_CC = D.GetString(row2["NM_CC"]);
							}
							else
								this.CC조회(D.GetString(row2["CD_ITEMGRP"]), D.GetString(row2["CD_ITEM"]), D.GetString(row2["CD_PLANT"]), out CD_CC, out NM_CC);

							row3["CD_CC"] = CD_CC;
							row3["NM_CC"] = NM_CC;
							row3["TP_VAT"] = D.GetString((this.cbo부가세구분).SelectedValue);
							row3["RT_VAT"] = this.cur부가세율.DecimalValue;
							row3["GI_PARTNER"] = this.ctx납품처.CodeValue;
							row3["LN_PARTNER"] = this.ctx납품처.CodeName;
							row3["QT_SO"] = D.GetString(row2["QT_PROJECT"]);
							row3["UNIT_SO_FACT"] = D.GetDecimal(row2["UNIT_SO_FACT"]) == 0 ? 1 : row2["UNIT_SO_FACT"];
							row3["UNIT_GI_FACT"] = D.GetDecimal(row2["UNIT_GI_FACT"]) == 0 ? 1 : row2["UNIT_GI_FACT"];
							row3["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row3["QT_SO"]) * (D.GetDecimal(row3["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(row3["UNIT_SO_FACT"])));
							row3["UNIT_IM"] = D.GetString(row2["UNIT_IM"]);

							if (this.Use부가세포함)
							{
								row3["UMVAT_SO"] = row2["UM_WON"];
								row3["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row3["QT_SO"]) * D.GetDecimal(row3["UMVAT_SO"]));
								row3["AM_WONAMT"] = Global.MainFrame.LoginInfo.CompanyLanguage != 0 ? D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, D.GetDecimal(row3["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(row3["RT_VAT"]))))) : decimal.Round(D.GetDecimal(row3["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(row3["RT_VAT"]))), MidpointRounding.AwayFromZero);
								row3["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row3["AMVAT_SO"]) - D.GetDecimal(row3["AM_WONAMT"]));
								row3["AM_SO"] = (this.cur환율.DecimalValue == 0 ? 0 : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row3["AM_WONAMT"]) / this.cur환율.DecimalValue));
								row3["UM_SO"] = (D.GetDecimal(row3["QT_SO"]) == 0 ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row3["AM_SO"]) / D.GetDecimal(row3["QT_SO"])));
							}
							else
							{
								if (this._거래구분 == "001" && D.GetString((this.cbo화폐단위).SelectedValue) == "000")
								{
									row2["UM"] = row2["UM_WON"];
									row2["AM_PROJECT"] = row2["AM_WONAMT"];
								}

								if (this.disCount_YN == "N")
									row3["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["UM"]));

								else if (this.disCount_YN == "Y")
								{
									row3["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["UM"]));
									row3["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row3["UM_BASE"]) - D.GetDecimal(row3["UM_BASE"]) * D.GetDecimal(row3["RT_DSCNT"]) / 100);
								}

								row3["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_PROJECT"]));
								row3["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row3["AM_SO"]) * this.cur환율.DecimalValue);
								row3["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_WONAMT"]) * (D.GetDecimal(row3["RT_VAT"]) / 100));
								row3["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row3["AM_WONAMT"]) + D.GetDecimal(row3["AM_VAT"]));
								row3["UMVAT_SO"] = !(D.GetDecimal(row3["QT_SO"]) != 0) ? Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row3["AMVAT_SO"])) : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row3["AMVAT_SO"]) / D.GetDecimal(row3["QT_SO"]));
							}

							row3["CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);

							if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y")
							{
								row3["CD_UNIT"] = row2["CD_ITEM"];
								row3["NM_UNIT"] = row2["NM_ITEM"];
								row3["STND_UNIT"] = row2["STND_ITEM"];
							}

							row3["TP_IV"] = this._매출형태;
							row3["MAT_ITEM"] = D.GetString(row2["MAT_ITEM"]);

							if (BASIC.GetMAEXC("배차사용유무") == "Y")
								row3["YN_PICKING"] = this._배송여부;

							row3["CLS_ITEM"] = D.GetString(row2["CLS_ITEM"]);

							dataTable1.Rows.Add(row3);
						}
					}

					this._flexH.BindingAdd(dataTable1, string.Empty, false);
					this.Authority(false);

					if (!ConfigSA.SA_EXC.수주라인_VAT수정 && this._flexH.HasNormalRow)
						this.cbo부가세구분.Enabled = false;

					this.IsInvAmCalc();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn단가적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					this._flexH.Redraw = false;
					int num2;

					if (this.추가모드여부)
						num2 = 0;
					else
						num2 = 1;

					if (num2 == 0)
					{
						this.단가변경();
					}
					else
					{
						DataSet dataSet = this._biz.의뢰된건조회(this.txt수주번호.Text);
						DataTable table1 = dataSet.Tables[0];
						DataTable table2 = dataSet.Tables[1];
						StringBuilder stringBuilder = new StringBuilder();
						string str1 = this.DD("품목코드") + "\t " + this.DD("품목명") + "\t";
						stringBuilder.AppendLine(str1);
						string str2 = "-".PadRight(60, '-');
						stringBuilder.AppendLine(str2);
						stringBuilder.AppendLine("의뢰내역.......................");

						foreach (DataRow row in table1.Rows)
						{
							if (row.RowState != DataRowState.Deleted)
							{
								string str3 = D.GetString(row["CD_ITEM"]) + "\t" + D.GetString(row["NM_ITEM"]);
								stringBuilder.AppendLine(str3);
							}
						}

						stringBuilder.AppendLine(Environment.NewLine);
						stringBuilder.AppendLine("의뢰내역되지 않은내역..........");

						foreach (DataRow row in table2.Rows)
						{
							if (row.RowState != DataRowState.Deleted)
							{
								string str3 = D.GetString(row["CD_ITEM"]) + "\t" + D.GetString(row["NM_ITEM"]);
								stringBuilder.AppendLine(str3);
							}
						}

						if (table2.Rows.Count == 0)
						{
							this.ShowMessage("모든 품목이 의뢰가 된 건입니다. 작업을 진행할 수 없습니다.");
							return;
						}

						if (table1.Rows.Count > 0 && this.ShowDetailMessage("의뢰된 내역이 있습니다. 의뢰되지 않은 건에 대해서 계속 진행하시겠습니까?", string.Empty, stringBuilder.ToString(), "QY2") == DialogResult.No)
							return;

						table1.PrimaryKey = new DataColumn[] { table1.Columns["SEQ_SO"] };

						for (int index = 0; index < this._flexH.DataTable.Rows.Count; ++index)
						{
							DataRow row = this._flexH.DataTable.Rows[index];

							if (row.RowState != DataRowState.Deleted && table1.Rows.Find(row["SEQ_SO"]) == null)
								this.단가변경(ref row);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flexH.Redraw = true;
				this._flexH.SumRefresh();
			}
		}

		private void 단가변경()
		{
			for (int index = 0; index < this._flexH.DataTable.Rows.Count; ++index)
			{
				DataRow row = this._flexH.DataTable.Rows[index];

				if (row.RowState != DataRowState.Deleted)
					this.단가변경(ref row);
			}
		}

		private void 단가변경(ref DataRow dr)
		{
			bool flag = false;

			if (this._biz.Get특수단가적용 == 특수단가적용.NONE)
			{
				if (!(this._단가적용형태 != "002") || !(this._단가적용형태 != "003"))
				{
					object um = BASIC.GetUM(D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA");

					if (um != null && um != DBNull.Value)
					{
						decimal num4 = D.GetDecimal(um);

						if (this.Use부가세포함)
						{
							decimal num5 = Unit.원화단가(DataDictionaryTypes.SA, num4);

							if (num5 != D.GetDecimal(dr["UMVAT_SO"]))
							{
								dr["UMVAT_SO"] = num5;
								flag = true;
							}
						}

						decimal num6 = Unit.외화단가(DataDictionaryTypes.SA, num4);

						if (this.disCount_YN == "N")
						{
							if (num6 != D.GetDecimal(dr["UM_SO"]))
							{
								dr["UM_SO"] = num6;
								flag = true;
							}
						}
						else if (this.disCount_YN == "Y" && num6 != D.GetDecimal(dr["UM_BASE"]))
						{
							dr["UM_BASE"] = num6;
							dr["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, num6 - num6 * D.GetDecimal(dr["RT_DSCNT"]) / 100);
							flag = true;
						}
					}
				}
			}

			if (this._biz.Get특수단가적용 == 특수단가적용.중량단가)
			{
				if (!(this._단가적용형태 != "002") || !(this._단가적용형태 != "003"))
				{
					object um = BASIC.GetUM(D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA");

					if (um != null && um != DBNull.Value)
					{
						decimal num4 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(um));

						if (num4 != D.GetDecimal(dr["UM_OPT"]))
						{
							dr["UM_OPT"] = num4;
							flag = true;
						}

						this.특수단가사용시단가계산(ref dr);

						if (this.disCount_YN == "Y")
						{
							decimal num5 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_BASE"]) - D.GetDecimal(dr["UM_BASE"]) * D.GetDecimal(dr["RT_DSCNT"]) / 100);

							if (num5 != D.GetDecimal(dr["UM_SO"]))
							{
								dr["UM_SO"] = num5;
								flag = true;
							}
						}
					}
				}
			}

			if (this._biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
			{
				DataTable dataTable = this._biz.SearchUmFixed(this.ctx거래처.CodeValue, D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_ITEM"]));
				decimal num4 = 0;

				if (dataTable == null || dataTable.Rows.Count == 0)
				{
					if (!(this._단가적용형태 != "002") || !(this._단가적용형태 != "003"))
					{
						object um = BASIC.GetUM(D.GetString(dr["CD_PLANT"]), D.GetString(dr["CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA");

						if (um != null && um != DBNull.Value)
							num4 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(um));
					}
				}
				else
					num4 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataTable.Rows[0]["UM_FIXED"]));

				if (num4 != D.GetDecimal(dr["UM_SO"]))
				{
					dr["UM_SO"] = num4;
					flag = true;
				}
			}

			if (!flag)
				return;

			if (this.Use부가세포함)
			{
				dr["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_SO"]) * D.GetDecimal(dr["UMVAT_SO"]));
				dr["AM_WONAMT"] = Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR ? D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, D.GetDecimal(dr["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(dr["RT_VAT"]))))) : decimal.Round(D.GetDecimal(dr["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(dr["RT_VAT"]))), MidpointRounding.AwayFromZero);
				dr["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]) - D.GetDecimal(dr["AM_WONAMT"]));
				dr["AM_SO"] = (this.cur환율.DecimalValue == 0 ? 0 : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]) / this.cur환율.DecimalValue));
				dr["UM_SO"] = (D.GetDecimal(dr["QT_SO"]) == 0 ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_SO"]) / D.GetDecimal(dr["QT_SO"])));
			}
			else
			{
				dr["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_SO"]) * D.GetDecimal(dr["QT_SO"]));
				dr["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_SO"]) * this.cur환율.DecimalValue);
				dr["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]) * (D.GetDecimal(dr["RT_VAT"]) / 100));
				dr["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["AM_WONAMT"]) + D.GetDecimal(dr["AM_VAT"]));
				dr["UMVAT_SO"] = !(D.GetDecimal(dr["QT_SO"]) != 0) ? Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"])) : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dr["AMVAT_SO"]) / D.GetDecimal(dr["QT_SO"]));
			}

			if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
			{
				수주관리.Calc calc = new 수주관리.Calc();
				dr["AM_PROFIT"] = calc.예상이익계산(D.GetDecimal(dr["QT_SO"]), D.GetDecimal(dr["UM_INV"]), D.GetDecimal(dr["AM_WONAMT"]));
			}
		}

		private void btnATPCHECK_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || !this.ATP체크로직(false))
					return;

				this.ShowMessage("납기일에 이상이 없습니다.");
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void 출하적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Chk수주일자 || !this.Chk거래처 || (!this.Chk수주유형 || !this.Chk화폐단위) || (!this.Chk단가유형 || !this.Chk과세구분) || !this.Chk공장)
					return;

				if (this.일반추가() || this.견적적용건())
				{
					this.ShowMessage("출하적용이 아닌 데이터가 존재합니다.");
				}
				else
				{
					string cdPlant = D.GetString(this.cbo공장.SelectedValue);
					string 공장명 = this.cbo공장.Text;

					if (!this.pnl기본정보.Enabled)
					{
						cdPlant = Global.MainFrame.LoginInfo.CdPlant;
						공장명 = Global.MainFrame.LoginInfo.NmPlant;
					}

					string codeValue = this.ctx거래처.CodeValue;
					string codeName = this.ctx거래처.CodeName;
					string 과세구분코드 = D.GetString(this.cbo부가세구분.SelectedValue);
					string text = this.cbo부가세구분.Text;
					P_SA_GIRR_REG_SUB pSaGirrRegSub = new P_SA_GIRR_REG_SUB(cdPlant, 공장명, codeValue, codeName, 과세구분코드, text);
					pSaGirrRegSub.Set수주등록출하적용 = true;
					pSaGirrRegSub.Set프로젝트사용 = App.SystemEnv.PROJECT사용;

					if (pSaGirrRegSub.ShowDialog() != DialogResult.OK || pSaGirrRegSub.출하테이블.Rows.Count == 0)
						return;

					DataTable dataTable1 = this._flexH.DataTable.Clone();
					decimal num2 = 1;
					string 품목 = Duzon.ERPU.MF.Common.Common.MultiString(pSaGirrRegSub.출하테이블, "CD_ITEM", "|");
					DataTable dataTable2 = BASIC.GetQtInvMulti(품목, this.dtp수주일자.Text);

					dataTable2.PrimaryKey = new DataColumn[] { dataTable2.Columns["CD_PLANT"],
															   dataTable2.Columns["CD_SL"],
															   dataTable2.Columns["CD_ITEM"] };

					foreach (DataRow row1 in pSaGirrRegSub.출하테이블.Rows)
					{
						if (row1.RowState != DataRowState.Deleted)
						{
							DataRow row2 = dataTable1.NewRow();

							row2["SEQ_SO"] = num2;
							++num2;
							row2["NO_PROJECT"] = row1["NO_PROJECT"];
							row2["SEQ_PROJECT"] = row1["SEQ_PROJECT"];
							row2["CD_PLANT"] = row1["CD_PLANT"];
							row2["CD_ITEM"] = row1["CD_ITEM"];
							row2["NM_ITEM"] = row1["NM_ITEM"];
							row2["EN_ITEM"] = row1["EN_ITEM"];
							row2["STND_ITEM"] = row1["STND_ITEM"];
							row2["UNIT_SO"] = row1["UNIT_SO"];
							row2["TP_ITEM"] = row1["TP_ITEM"];
							row2["EN_ITEM"] = row1["EN_ITEM"];
							row2["STND_DETAIL_ITEM"] = row1["STND_DETAIL_ITEM"];
							row2["GRP_MFG"] = row1["GRP_MFG"];
							row2["LT_GI"] = D.GetDecimal(row1["LT_GI"]);
							row2["WEIGHT"] = row1["WEIGHT"];
							row2["UNIT_WEIGHT"] = row1["UNIT_WEIGHT"];
							row2["FG_SERNO"] = row1["FG_SERNO"];
							row2["YN_ATP"] = row1["YN_ATP"];
							row2["CUR_ATP_DAY"] = row1["CUR_ATP_DAY"];
							row2["FG_MODEL"] = row1["FG_MODEL"];
							row2["GI_PARTNER"] = this.ctx납품처.CodeValue;
							row2["LN_PARTNER"] = this.ctx납품처.CodeName;
							row2["CD_SL"] = row1["CD_SL"];
							row2["NM_SL"] = row1["NM_SL"];

							DataRow dataRow = dataTable2.Rows.Find(new object[] { D.GetString(row1["CD_PLANT"]),
																				  D.GetString(row1["CD_SL"]),
																				  D.GetString(row1["CD_ITEM"]) });

							if (dataRow != null)
							{
								row2["SL_QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
							}

							string CD_CC;
							string NM_CC;
							this.CC조회(D.GetString(row1["CD_ITEMGRP"]), D.GetString(row1["CD_ITEM"]), D.GetString(row1["CD_PLANT"]), out CD_CC, out NM_CC);
							row2["CD_CC"] = CD_CC;
							row2["NM_CC"] = NM_CC;
							row2["TP_VAT"] = D.GetString(row1["TP_VAT"]);
							row2["RT_VAT"] = D.GetDecimal(row1["RT_VAT"]);
							row2["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row1["QT_GIR"]));
							row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row1["UM_EX"]));

							if (this.Use부가세포함 && D.GetDecimal(row1["QT_RETURN"]) == 0)
							{
								row2["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["AM_EX_ORIGINAL"]));
								row2["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["AM_ORIGINAL"]));
								row2["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["VAT_ORIGINAL"]));
							}
							else
							{
								row2["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["AM_EX"]));
								row2["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_SO"]) * this.cur환율.DecimalValue);
								row2["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_WONAMT"]) * (D.GetDecimal(row2["RT_VAT"]) / 100));
							}

							row2["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_WONAMT"]) + D.GetDecimal(row2["AM_VAT"]));
							row2["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["AMVAT_SO"]) / D.GetDecimal(row2["QT_SO"]));
							row2["UNIT_SO_FACT"] = (D.GetDecimal(row1["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(row1["UNIT_SO_FACT"]));
							row2["UNIT_GI_FACT"] = (D.GetDecimal(row1["UNIT_GI_FACT"]) == 0 ? 1 : D.GetDecimal(row1["UNIT_GI_FACT"]));
							row2["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row1["QT_GIR_IM"]));
							row2["UNIT_IM"] = row1["UNIT"];

							if (D.GetString(row2["DT_DUEDATE"]) == string.Empty && this.dtp납기일.Text != string.Empty)
							{
								row2["DT_DUEDATE"] = this.dtp납기일.Text;
								row2["DT_REQGI"] = this._CommFun.DateAdd(this.dtp납기일.Text, "D", D.GetInt(row2["LT_GI"]) * -1);
							}

							row2["NO_IO_MGMT"] = row1["NO_IO_MGMT"];
							row2["NO_IOLINE_MGMT"] = row1["NO_IOLINE_MGMT"];
							row2["NO_SO_ORIGINAL"] = row1["NO_SO_MGMT"];
							row2["SEQ_SO_ORIGINAL"] = row1["NO_SOLINE_MGMT"];
							row2["CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
							row2["TP_IV"] = this._매출형태;
							row2["MAT_ITEM"] = row1["MAT_ITEM"];

							if (BASIC.GetMAEXC("SET품 사용유무") == "Y")
							{
								row2["CD_ITEM_REF"] = row1["CD_ITEM_REF"];
								row2["NM_ITEM_REF"] = row1["NM_ITEM"];
								row2["STND_ITEM_REF"] = row1["STND_ITEM_SET"];
							}

							if (BASIC.GetMAEXC("배차사용유무") == "Y")
								row2["YN_PICKING"] = this._배송여부;

							dataTable1.Rows.Add(row2);
						}
					}

					this._flexH.Binding = dataTable1;
					this._flexH.IsDataChanged = true;
					this.ToolBarDeleteButtonEnabled = false;
					this.ToolBarSaveButtonEnabled = true;
					this.Page_DataChanged(null, null);
					this.버튼Enabled(false);

					if (App.SystemEnv.PROJECT사용)
					{
						this.ctx프로젝트.SetCode(D.GetString(pSaGirrRegSub.출하테이블.Rows[pSaGirrRegSub.출하테이블.Rows.Count - 1]["NO_PROJECT"]), D.GetString(pSaGirrRegSub.출하테이블.Rows[pSaGirrRegSub.출하테이블.Rows.Count - 1]["NM_PROJECT"]));
						this._header.CurrentRow["NO_PROJECT"] = this.ctx프로젝트.CodeValue;
						this._header.CurrentRow["NM_PROJECT"] = this.ctx프로젝트.CodeName;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn상품적용_Click(object sender, EventArgs e)
		{
			if (!this.FieldCheck(string.Empty))
				return;

			this.버튼Enabled(false);

			try
			{
				DataTable dataTable1 = new DataTable();

				P_SA_SO_SPITEM_SUB pSaSoSpitemSub = new P_SA_SO_SPITEM_SUB(new object[] { this.dtp수주일자.Text,
																						  this.cur환율.DecimalValue,
																						  this.ctx거래처.CodeValue,
																						  D.GetString(this.cbo단가유형.SelectedValue),
																						  D.GetString(this.cbo화폐단위.SelectedValue),
																						  this._단가적용형태,
																						  this.so_Price,
																						  this.cur부가세율.DecimalValue });

				if (pSaSoSpitemSub.ShowDialog() == DialogResult.OK && pSaSoSpitemSub.ReturnDataTable != null)
				{
					DataTable dataTable2 = pSaSoSpitemSub.ReturnDataTable.Copy();
					DataTable dataTable3 = this._flexH.DataTable.Clone();
					decimal num = this._flexH.DataView.Table.Rows.Count + 1;
					string 품목 = Duzon.ERPU.MF.Common.Common.MultiString(dataTable2, "CD_ITEM", "|");
					DataTable dataTable4 = BASIC.GetQtInvMulti(품목, this.dtp수주일자.Text);
					dataTable4.PrimaryKey = new DataColumn[] { dataTable4.Columns["CD_PLANT"],
															   dataTable4.Columns["CD_SL"],
															   dataTable4.Columns["CD_ITEM"] };

					if (dataTable2 == null || dataTable2.Rows.Count == 0)
						return;

					foreach (DataRow row1 in dataTable2.Rows)
					{
						if (row1.RowState != DataRowState.Deleted)
						{
							DataRow row2 = dataTable3.NewRow();

							row2["SEQ_SO"] = num;
							++num;
							row2["CD_SHOP"] = D.GetString(row1["CD_SHOP"]);
							row2["CD_SPITEM"] = D.GetString(row1["CD_SPITEM"]);
							row2["CD_OPT"] = D.GetString(row1["CD_OPT"]);
							row2["NO_PROJECT"] = this.ctx프로젝트.CodeValue;
							row2["SEQ_PROJECT"] = 0;
							row2["CD_PLANT"] = D.GetString(row1["CD_PLANT"]);
							row2["CD_ITEM"] = D.GetString(row1["CD_ITEM"]);
							row2["NM_ITEM"] = D.GetString(row1["NM_ITEM"]);
							row2["STND_ITEM"] = D.GetString(row1["STND_ITEM"]);
							row2["UNIT_SO"] = D.GetString(row1["UNIT_SO"]);
							row2["TP_ITEM"] = D.GetString(row1["TP_ITEM"]);
							row2["GI_PARTNER"] = this.ctx납품처.CodeValue;
							row2["LN_PARTNER"] = this.ctx납품처.CodeName;
							row2["CD_SL"] = D.GetString(row1["CD_SL"]);
							row2["NM_SL"] = D.GetString(row1["NM_SL"]);
							DataRow dataRow = dataTable4.Rows.Find(new object[] { D.GetString(row1["CD_PLANT"]),
																				  D.GetString(row1["CD_SL"]),
																				  D.GetString(row1["CD_ITEM"]) });

							if (dataRow != null)
								row2["SL_QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);

							string CD_CC;
							string NM_CC;
							this.CC조회(D.GetString(row1["CD_ITEMGRP"]), D.GetString(row1["CD_ITEM"]), D.GetString(row1["CD_PLANT"]), out CD_CC, out NM_CC);
							row2["CD_CC"] = CD_CC;
							row2["NM_CC"] = NM_CC;
							row2["TP_VAT"] = D.GetString(row1["FG_VAT"]);
							row2["RT_VAT"] = D.GetDecimal(row1["RT_VAT"]);
							row2["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row1["QT_SO"]));
							row2["WEIGHT"] = D.GetString(row1["WEIGHT"]);
							row2["UNIT_WEIGHT"] = D.GetString(row1["UNIT_WEIGHT"]);

							if (this.disCount_YN == "N")
								row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row1["UM_SO"]));
							else if (this.disCount_YN == "Y")
							{
								row2["RT_DSCNT"] = D.GetDecimal(row2["RT_DSCNT"]) == 0 ? 1 : row2["RT_DSCNT"];
								row2["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row1["UM_SO"]));
								row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["UM_BASE"]) - D.GetDecimal(row2["UM_BASE"]) * D.GetDecimal(row2["RT_DSCNT"]) / 100);
							}

							row2["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["AM_SO"]));
							row2["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["AM_WONAMT"]));
							row2["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["AM_VAT"]));
							row2["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["AMVAT_SO"]));
							row2["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row1["UMVAT_SO"]));

							row2["UNIT_SO_FACT"] = D.GetDecimal(row1["UNIT_SO_FACT"]) == 0 ? 1 : row1["UNIT_SO_FACT"];
							row2["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row1["QT_IM"]) * (D.GetDecimal(row2["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(row2["UNIT_SO_FACT"])));
							row2["UNIT_IM"] = row1["UNIT_IM"];
							row2["DC1"] = (D.GetString(row1["CD_SPITEM"]) + "/" + D.GetString(row1["NM_SPITEM"]));
							row2["CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
							row2["TP_IV"] = this._매출형태;
							row2["MAT_ITEM"] = row1["MAT_ITEM"];

							if (BASIC.GetMAEXC("배차사용유무") == "Y")
								row2["YN_PICKING"] = this._배송여부;

							dataTable3.Rows.Add(row2);
						}
					}

					this._flexH.BindingAdd(dataTable3, string.Empty, false);

					foreach (DataRow row in this._flexH.DataTable.Rows)
					{
						if (row.RowState != DataRowState.Deleted && (D.GetString(row["DT_DUEDATE"]) == string.Empty && this.dtp납기일.Text != string.Empty))
						{
							row["DT_DUEDATE"] = this.dtp납기일.Text;
							row["DT_REQGI"] = this._CommFun.DateAdd(this.dtp납기일.Text, "D", D.GetInt(row["LT_GI"]) * -1);
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn배송지주소_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flexH.DataTable == null || this._flexH.DataTable.Rows.Count == 0)
					this.ShowMessage("라인을 먼저 입력하세요!");
				else
				{
					DataTable partnerInfoSearch = this._biz.GetPartnerInfoSearch(new object[] { MA.Login.회사코드,
																								this.ctx거래처.CodeValue });

					string[] str = new string[7];
					if (partnerInfoSearch.Rows.Count == 1)
					{
						str[0] = D.GetString(partnerInfoSearch.Rows[0]["CD_PARTNER"]);
						str[1] = D.GetString(partnerInfoSearch.Rows[0]["NO_POST2"]);
						str[2] = D.GetString(partnerInfoSearch.Rows[0]["DC_ADS2_H"]);
						str[3] = D.GetString(partnerInfoSearch.Rows[0]["DC_ADS2_D"]);
						str[4] = D.GetString(partnerInfoSearch.Rows[0]["NO_TEL2"]);
						str[5] = D.GetString(partnerInfoSearch.Rows[0]["CD_EMP_PARTNER"]);
						str[6] = D.GetString(partnerInfoSearch.Rows[0]["NO_HPEMP_PARTNER"]);
					}

					DataTable ReturnDt = this._flexH.DataTable.Clone();

					foreach (DataRow row in this._flexH.DataTable.Rows)
					{
						if (row.RowState != DataRowState.Deleted)
							ReturnDt.Rows.Add(row.ItemArray);
					}

					P_SA_SO_DLV_SUB pSaSoDlvSub = new P_SA_SO_DLV_SUB(ReturnDt, str, "SEQ_SO");

					if (pSaSoDlvSub.ShowDialog() == DialogResult.OK)
					{
						foreach (DataRow row in pSaSoDlvSub.ReturnTable.Rows)
						{
							if (row.RowState != DataRowState.Deleted)
							{
								for (int index = 0; index < this._flexH.DataTable.Rows.Count; ++index)
								{
									if (D.GetString(row["SEQ_SO"]) == D.GetString(this._flexH[index + 2, "SEQ_SO"]))
									{
										this._flexH[index + 2, "NM_CUST_DLV"] = row["NM_CUST_DLV"];
										this._flexH[index + 2, "NO_TEL_D1"] = row["NO_TEL_D1"];
										this._flexH[index + 2, "NO_TEL_D2"] = row["NO_TEL_D2"];
										this._flexH[index + 2, "CD_ZIP"] = D.GetString(row["CD_ZIP"]).Replace("-", string.Empty);
										this._flexH[index + 2, "ADDR1"] = row["ADDR1"];
										this._flexH[index + 2, "ADDR2"] = row["ADDR2"];
										this._flexH[index + 2, "TP_DLV"] = row["TP_DLV"];
										this._flexH[index + 2, "DC_REQ"] = row["DC_REQ"];
										this._flexH[index + 2, "TP_DLV_DUE"] = row["TP_DLV_DUE"];
										this._flexH[index + 2, "NO_ORDER"] = row["NO_ORDER"];
										this._flexH[index + 2, "NM_CUST"] = row["NM_CUST"];
										this._flexH[index + 2, "NO_TEL1"] = row["NO_TEL1"];
										this._flexH[index + 2, "NO_TEL2"] = row["NO_TEL2"];
										this._flexH[index + 2, "DLV_TXT_USERDEF1"] = row["DLV_TXT_USERDEF1"];
										this._flexH[index + 2, "DLV_CD_USERDEF1"] = row["DLV_CD_USERDEF1"];
									}
								}
							}
						}

						this._flexH.IsDataChanged = true;
						this.Page_DataChanged(null, null);
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void 엑셀업로드_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.FieldCheck(string.Empty))
					return;

				string empty1 = string.Empty;
				string empty2 = string.Empty;
				string empty3 = string.Empty;
				string cdPlant = this.LoginInfo.CdPlant;
				string empty4 = string.Empty;
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";

				if (openFileDialog.ShowDialog() != DialogResult.OK)
					return;

				Application.DoEvents();
				DataTable dataTable1 = new Excel().StartLoadExcel(openFileDialog.FileName);

				string str3 = Duzon.ERPU.MF.Common.Common.MultiString(dataTable1.DefaultView.ToTable(true, "CD_ITEM"), "CD_ITEM", "|");
				bool flag1 = false;
				StringBuilder stringBuilder1 = new StringBuilder();
				string str4 = "품목코드";
				stringBuilder1.AppendLine(str4);
				string str5 = "-".PadRight(20, '-');
				stringBuilder1.AppendLine(str5);
				DataTable dataTable3 = this._biz.엑셀(dataTable1.Copy());
				DataTable dataTable4 = this._biz.공장품목(str3, string.Empty);
				dataTable4.PrimaryKey = new DataColumn[] { dataTable4.Columns["CD_PLANT"], dataTable4.Columns["CD_ITEM"] };
				string empty7 = string.Empty;
				DataTable dataTable5 = BASIC.GetQtInvMulti(str3, this.dtp수주일자.Text);
				dataTable5.PrimaryKey = new DataColumn[] { dataTable5.Columns["CD_PLANT"], dataTable5.Columns["CD_SL"], dataTable5.Columns["CD_ITEM"] };
				DataTable dataTable6 = null;

				if (this._biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
				{
					dataTable6 = this._biz.SearchUmFixed(this.ctx거래처.CodeValue, cdPlant, str3);
					dataTable6.PrimaryKey = new DataColumn[] { dataTable6.Columns["CD_ITEM"] };
				}

				DataTable dataTable7 = null;

				if (!this.ctx거래처.IsEmpty() && this.disCount_YN == "Y" && this._biz.Get할인율적용 == 수주관리.할인율적용.거래처그룹별_품목군할인율)
					dataTable7 = this._biz.할인율(cdPlant, this.ctx거래처.CodeValue, dataTable3.Select());

				DataTable dataTable8 = null;
				if (dataTable1.Columns.Contains("NO_PROJECT"))
				{
					dataTable8 = this._biz.Get프로젝트();
					dataTable8.PrimaryKey = new DataColumn[] { dataTable8.Columns["NO_PROJECT"] };
				}

				this._flexH.Redraw = false;
				foreach (DataRow row1 in dataTable3.Rows)
				{
					if (row1.RowState != DataRowState.Deleted)
					{
						string upper = D.GetString(row1["CD_ITEM"]).ToUpper();
						if (upper != string.Empty)
						{
							DataRow dataRow1;
							dataRow1 = dataTable4.Rows.Find(new object[] { D.GetString(row1["CD_PLANT"]), upper });
							bool flag3 = dataRow1 != null;

							if (flag3)
							{
								bool flag4 = dataTable1.Columns.Contains("AM_SO");
								DataRow row2 = this._flexH.DataTable.NewRow();
								row2["CD_ITEM"] = upper;
								row2["NM_ITEM"] = dataRow1["NM_ITEM"];
								row2["STND_ITEM"] = dataRow1["STND_ITEM"];
								row2["UNIT_SO"] = dataRow1["UNIT_SO"];
								row2["UNIT_IM"] = dataRow1["UNIT_IM"];
								row2["TP_ITEM"] = dataRow1["TP_ITEM"];
								row2["EN_ITEM"] = dataRow1["EN_ITEM"];
								row2["STND_DETAIL_ITEM"] = dataRow1["STND_DETAIL_ITEM"];
								row2["GRP_MFG"] = dataRow1["GRP_MFG"];
								row2["WEIGHT"] = dataRow1["WEIGHT"];
								row2["UNIT_WEIGHT"] = dataRow1["UNIT_WEIGHT"];
								row2["YN_ATP"] = dataRow1["YN_ATP"];
								row2["CUR_ATP_DAY"] = dataRow1["CUR_ATP_DAY"];
								row2["FG_MODEL"] = dataRow1["FG_MODEL"];

								if (D.GetString(row1["CD_SL"]) == string.Empty)
								{
									row2["CD_SL"] = dataRow1["CD_SL"];
									row2["NM_SL"] = dataRow1["NM_SL"];
								}
								else
								{
									row2["CD_SL"] = row1["CD_SL"];
									DataRow dataRow2 = BASIC.GetSL(D.GetString(row1["CD_PLANT"]), D.GetString(row1["CD_SL"]));

									if (dataRow2 == null)
									{
										this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { "[" + D.GetString(row1["CD_SL"]) + "] " + this.DD("창고") });
										return;
									}

									row2["NM_SL"] = dataRow2["NM_SL"];
								}

								row2["DT_DUEDATE"] = row1["DT_DUEDATE"];
								row2["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row1["QT_SO"]));
								row2["LT_GI"] = D.GetDecimal(dataRow1["LT_GI"]);
								row2["FG_SERNO"] = dataRow1["FG_SERNO"];

								if (dataTable1.Columns.Contains("GI_PARTNER"))
								{
									string str1 = D.GetString(row1["GI_PARTNER"]);
									row2["GI_PARTNER"] = str1;

									if (str1 != string.Empty)
										row2["LN_PARTNER"] = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER, new object[] { MA.Login.회사코드, str1 })["LN_PARTNER"];
								}
								else
								{
									row2["GI_PARTNER"] = this.ctx납품처.CodeValue;
									row2["LN_PARTNER"] = this.ctx납품처.CodeName;
								}

								row2["UNIT_SO_FACT"] = D.GetDecimal(dataRow1["UNIT_SO_FACT"]) == 0 ? 1 : dataRow1["UNIT_SO_FACT"];
								row2["UNIT_GI_FACT"] = D.GetDecimal(dataRow1["UNIT_GI_FACT"]) == 0 ? 1 : dataRow1["UNIT_GI_FACT"];

								row2["SEQ_SO"] = this.최대차수 + 1;
								row2["CD_PLANT"] = D.GetString(row1["CD_PLANT"]);

								if (D.GetString(row1["DT_DUEDATE"]) != string.Empty)
								{
									row2["DT_DUEDATE"] = row1["DT_DUEDATE"];
									row2["DT_REQGI"] = this._CommFun.DateAdd(D.GetString(row1["DT_DUEDATE"]), "D", D.GetInt(row2["LT_GI"]) * -1);
								}

								DataRow dataRow3 = dataTable5.Rows.Find(new object[] { D.GetString(row1["CD_PLANT"]),
																					   D.GetString(row2["CD_SL"]),
																					   upper });

								if (dataRow3 != null)
								{
									row2["SL_QT_INV"] = D.GetDecimal(dataRow3["QT_INV"]);
								}

								string CD_CC;
								string NM_CC;
								this.CC조회(D.GetString(dataRow1["CD_ITEMGRP"]), D.GetString(row2["CD_ITEM"]), D.GetString(row2["CD_PLANT"]), out CD_CC, out NM_CC);
								row2["CD_CC"] = CD_CC;
								row2["NM_CC"] = NM_CC;
								string str2 = D.GetString(dataRow1["FG_TAX_SA"]).Trim();

								if (this._biz.Get과세변경유무 == "N" || str2 == string.Empty)
								{
									row2["TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
									row2["RT_VAT"] = this.cur부가세율.DecimalValue;
								}
								else
								{
									row2["TP_VAT"] = str2;
									row2["RT_VAT"] = D.GetDecimal(dataRow1["RT_TAX_SA"]);
								}

								row2["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row1["QT_SO"]) * D.GetDecimal(row2["UNIT_SO_FACT"]));

								if (this._구분 == "복사")
									row2["STA_SO1"] = this._수주상태;

								if (dataTable7 != null)
								{
									DataRow dataRow2 = dataTable7.Rows.Find(row2["CD_ITEM"]);
									row2["RT_DSCNT"] = dataRow2 == null ? 0 : dataRow2["DC_RATE"];
								}

								if (this._biz.Get특수단가적용 == 특수단가적용.NONE)
								{
									if (this.disCount_YN == "Y")
									{
										if ((this._단가적용형태 == "002" || this._단가적용형태 == "003") && this.so_Price == "Y")
										{
											row2["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, BASIC.GetUM(D.GetString(row2["CD_PLANT"]), D.GetString(row2["CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA"));
											row2["UM_SO"] = !(D.GetDecimal(row2["RT_DSCNT"]) != 0) ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["UM_BASE"]) - D.GetDecimal(row2["UM_BASE"]) * D.GetDecimal(row2["RT_DSCNT"]) / 100);
										}
										else if (this._단가적용형태 != "002" && this._단가적용형태 != "003" && this.so_Price == "Y")
										{
											row2["UM_BASE"] = 0;
											row2["UM_SO"] = 0;
										}
										else
										{
											row2["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row1["UM_SO"]));
											row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["UM_BASE"]) - D.GetDecimal(row2["UM_BASE"]) * D.GetDecimal(row2["RT_DSCNT"]) / 100);
										}
									}
									else if (this.disCount_YN == "N")
									{
										if ((this._단가적용형태 == "002" || this._단가적용형태 == "003") && this.so_Price == "Y")
										{
											decimal um = BASIC.GetUM(D.GetString(row2["CD_PLANT"]), D.GetString(row2["CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA");
											row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, um);

											if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000001") == "001")
												row2["NUM_USERDEF1"] = D.GetDecimal(row2["UM_SO"]);
										}
										else
											row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row1["UM_SO"]));
									}
								}
								else if (this._biz.Get특수단가적용 == 특수단가적용.중량단가)
								{
									if ((this._단가적용형태 == "002" || this._단가적용형태 == "003") && row2.Table.Columns.Contains("UM_OPT"))
										row2["UM_OPT"] = Unit.외화단가(DataDictionaryTypes.SA, BASIC.GetUM(D.GetString(row2["CD_PLANT"]), D.GetString(row2["CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA"));

									decimal num1 = 0;
									decimal num2 = 0;

									if (row2.Table.Columns.Contains("WEIGHT"))
										num1 = D.GetDecimal(row2["WEIGHT"]);

									if (row2.Table.Columns.Contains("UM_OPT"))
										num2 = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["UM_OPT"]));

									decimal num3 = num1 * num2;

									if (this.disCount_YN == "Y")
									{
										row2["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, num3);
										row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["UM_BASE"]) - D.GetDecimal(row2["UM_BASE"]) * D.GetDecimal(row2["RT_DSCNT"]) / 100);
									}
									else
										row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, num3);
								}
								else if (this._biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
								{
									DataRow dataRow2 = dataTable6.Rows.Find(D.GetString(row2["CD_ITEM"]));
									row2["UM_SO"] = dataRow2 != null ? Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow2["UM_FIXED"])) : Unit.외화단가(DataDictionaryTypes.SA, BASIC.GetUM(D.GetString(row2["CD_PLANT"]), D.GetString(row2["CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA"));
								}
								else if (flag4)
								{
									decimal num1 = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row1["AM_SO"]));
									decimal num2 = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["QT_SO"]));
									decimal num3 = num2 == 0 ? 0 : num1 / num2;
									row2["AM_SO"] = num1;
									row2["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, num3);
									row2["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, num1 * this.cur환율.DecimalValue);
									row2["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_WONAMT"]) * (D.GetDecimal(row2["RT_VAT"]) / 100));
									row2["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_WONAMT"]) + D.GetDecimal(row2["AM_VAT"]));
									row2["UMVAT_SO"] = !(D.GetDecimal(row2["QT_SO"]) != 0) ? Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["AMVAT_SO"])) : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["AMVAT_SO"]) / D.GetDecimal(row2["QT_SO"]));
								}
								else
								{
									row2["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["QT_SO"]) * D.GetDecimal(row2["UM_SO"]));
									row2["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_SO"]) * this.cur환율.DecimalValue);
									row2["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_WONAMT"]) * (D.GetDecimal(row2["RT_VAT"]) / 100));
									row2["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM_WONAMT"]) + D.GetDecimal(row2["AM_VAT"]));
									row2["UMVAT_SO"] = !(D.GetDecimal(row2["QT_SO"]) != 0) ? Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["AMVAT_SO"])) : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["AMVAT_SO"]) / D.GetDecimal(row2["QT_SO"]));
								}

								row2["DC1"] = row1["DC1"];
								row2["DC2"] = row1["DC2"];
								row2["FG_USE"] = row1["FG_USE"];
								row2["TXT_USERDEF1"] = row1["TXT_USERDEF1"];
								row2["TXT_USERDEF2"] = row1["TXT_USERDEF2"];
								row2["NO_PO_PARTNER"] = row1["NO_PO_PARTNER"];
								row2["NO_POLINE_PARTNER"] = row1["NO_POLINE_PARTNER"];
								row2["CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
								row2["TP_IV"] = this._매출형태;
								row2["MAT_ITEM"] = dataRow1["MAT_ITEM"];
								row2["CLS_ITEM"] = dataRow1["CLS_ITEM"];

								if (BASIC.GetMAEXC("배차사용유무") == "Y")
									row2["YN_PICKING"] = this._배송여부;

								if (dataTable3.Columns.Contains("NM_CUST_DLV"))
									row2["NM_CUST_DLV"] = row1["NM_CUST_DLV"];

								if (dataTable3.Columns.Contains("CD_ZIP"))
									row2["CD_ZIP"] = row1["CD_ZIP"];

								if (dataTable3.Columns.Contains("ADDR1"))
									row2["ADDR1"] = row1["ADDR1"];

								if (dataTable3.Columns.Contains("ADDR2"))
									row2["ADDR2"] = row1["ADDR2"];

								if (dataTable3.Columns.Contains("NO_TEL_D1"))
									row2["NO_TEL_D1"] = row1["NO_TEL_D1"];

								if (dataTable3.Columns.Contains("NO_TEL_D2"))
									row2["NO_TEL_D2"] = row1["NO_TEL_D2"];

								if (dataTable3.Columns.Contains("TP_DLV"))
									row2["TP_DLV"] = row1["TP_DLV"];

								if (dataTable3.Columns.Contains("DC_REQ"))
									row2["DC_REQ"] = row1["DC_REQ"];

								if (dataTable3.Columns.Contains("NO_ORDER"))
									row2["NO_ORDER"] = row1["NO_ORDER"];

								if (dataTable3.Columns.Contains("NM_CUST"))
									row2["NM_CUST"] = row1["NM_CUST"];

								if (dataTable3.Columns.Contains("NO_TEL1"))
									row2["NO_TEL1"] = row1["NO_TEL1"];

								if (dataTable3.Columns.Contains("NO_TEL2"))
									row2["NO_TEL2"] = row1["NO_TEL2"];

								if (dataTable3.Columns.Contains("DLV_TXT_USERDEF1"))
									row2["DLV_TXT_USERDEF1"] = row1["DLV_TXT_USERDEF1"];

								if (dataTable3.Columns.Contains("NO_LINK"))
									row2["NO_LINK"] = row1["NO_LINK"];

								if (dataTable3.Columns.Contains("NO_LINE_LINK"))
									row2["NO_LINE_LINK"] = row1["NO_LINE_LINK"];

								if (dataTable1.Columns.Contains("NO_PROJECT"))
								{
									DataRow dataRow2 = dataTable8.Rows.Find(D.GetString(row1["NO_PROJECT"]));

									if (dataRow2 != null)
									{
										row2["NO_PROJECT"] = row1["NO_PROJECT"];
										row2["NM_PROJECT"] = dataRow2["NM_PROJECT"];
									}
									if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y" && dataTable3.Columns.Contains("SEQ_PROJECT"))
									{
										DataTable dataTable2 = this._biz.Search_프로젝트_unit정보(D.GetString(row1["NO_PROJECT"]), D.GetInt(row1["SEQ_PROJECT"]));

										if (dataTable2 == null || dataTable2.Rows.Count == 0)
										{
											this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("프로젝트코드") + ":" + D.GetString(row1["NO_PROJECT"]) + " " + this.DD("항번") + ":" + D.GetString(row1["SEQ_PROJECT"]) });
											return;
										}

										row2["SEQ_PROJECT"] = row1["SEQ_PROJECT"];
										row2["CD_UNIT"] = dataTable2.Rows[0]["CD_UNIT"];
										row2["NM_UNIT"] = dataTable2.Rows[0]["NM_UNIT"];
										row2["STND_UNIT"] = dataTable2.Rows[0]["STND_UNIT"];
									}
								}

								this._flexH.DataTable.Rows.Add(row2);
							}
							else
							{
								stringBuilder1.AppendLine(upper);
								flag1 = true;
							}
						}
					}
				}

				if (flag1)
				{
					this.ShowDetailMessage("엑셀 업로드하는 중에 마스터품목과 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 불일치 목록을 확인하세요!", stringBuilder1.ToString());
				}

				this._flexH.Redraw = true;
				this._flexH.Row = this._flexH.Rows.Count - 1;
				this._flexH.Col = this._flexH.Cols.Fixed;
				this._flexH.Focus();
				this._flexH.SumRefresh();
				this._flexH.IsDataChanged = true;
				this.Page_DataChanged(null, null);
				this.버튼Enabled(false);
				this.IsInvAmCalc();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			finally
			{
				this._flexH.Redraw = true;
			}
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.견적적용건())
				{
					this.ShowMessage("견적적용건이 존재합니다.");
				}
				else if (this.출하적용건())
				{
					this.ShowMessage("출하적용건이 존재합니다.");
				}
				else
				{
					if (!this.FieldCheck(string.Empty))
						return;

					this.버튼Enabled(false);
					this._flexH.Rows.Add();
					this._flexH.Row = this._flexH.Rows.Count - 1;
					this._flexH["SEQ_SO"] = this.최대차수 + 1;
					this._flexH["CD_PLANT"] = this.LoginInfo.CdPlant;
					this._flexH["GI_PARTNER"] = this.ctx납품처.CodeValue;
					this._flexH["LN_PARTNER"] = this.ctx납품처.CodeName;
					this._flexH["QT_SO"] = 0;
					this._flexH["UM_SO"] = 0;
					this._flexH["AM_SO"] = 0;
					this._flexH["AM_VAT"] = 0;
					this._flexH["QT_IM"] = 0;
					this._flexH["NO_PO_PARTNER"] = this.txt거래처PO.Text;

					if (!this.ctx프로젝트.IsEmpty())
					{
						this._flexH["NO_PROJECT"] = this.ctx프로젝트.CodeValue;
						this._flexH["NM_PROJECT"] = this.ctx프로젝트.CodeName;
					}

					if (this.disCount_YN == "Y")
					{
						this._flexH["RT_DSCNT"] = 0;
						this._flexH["UM_BASE"] = 0;
					}

					this._flexH["TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
					this._flexH["RT_VAT"] = this.cur부가세율.DecimalValue;

					if (this._flexH.Rows.Count == 3)
					{
						this._flexH["DT_EXPECT"] = this.dtp납기일.Text;
						this._flexH["DT_DUEDATE"] = this.dtp납기일.Text;
						this._flexH["DT_REQGI"] = this.dtp납기일.Text;
					}
					else if (this._flexH.Rows.Count > 3)
					{
						this._flexH["DT_EXPECT"] = this._flexH[this._flexH.Row - 1, "DT_EXPECT"];
						this._flexH["DT_DUEDATE"] = this._flexH[this._flexH.Row - 1, "DT_DUEDATE"];
						this._flexH["DT_REQGI"] = this._flexH[this._flexH.Row - 1, "DT_REQGI"];
						this._flexH["FG_USE"] = this._flexH[this._flexH.Row - 1, "FG_USE"];
						this._flexH["CD_MNGD1"] = this._flexH[this._flexH.Row - 1, "CD_MNGD1"];
						this._flexH["NM_MNGD1"] = this._flexH[this._flexH.Row - 1, "NM_MNGD1"];
						this._flexH["CD_MNGD2"] = this._flexH[this._flexH.Row - 1, "CD_MNGD2"];
						this._flexH["NM_MNGD2"] = this._flexH[this._flexH.Row - 1, "NM_MNGD2"];
						this._flexH["CD_MNGD3"] = this._flexH[this._flexH.Row - 1, "CD_MNGD3"];
						this._flexH["NM_MNGD3"] = this._flexH[this._flexH.Row - 1, "NM_MNGD3"];
						this._flexH["CD_MNGD4"] = this._flexH[this._flexH.Row - 1, "CD_MNGD4"];
					}

					if (this._구분 != "복사")
						this._flexH["STA_SO1"] = this._수주상태;

					this._flexH["CD_EXCH"] = D.GetString((this.cbo화폐단위).SelectedValue);
					this._flexH["TP_IV"] = this._매출형태;

					if (BASIC.GetMAEXC("배차사용유무") == "Y")
						this._flexH["YN_PICKING"] = this._배송여부;

					this._flexH.Col = this._flexH.Cols.Fixed;
					this._flexH.AddFinished();
					this._flexH.Focus();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || !this.Chk확정여부())
					return;

				this._flexL.RemoveViewAll();
				this._flexH.RemoveItem(this._flexH.Row);

				if (!this._flexH.HasNormalRow && this.추가모드여부)
				{
					this._flexH.AcceptChanges();
					this.ctx수주형태.Enabled = true;
					this.cbo화폐단위.Enabled = true;

					if (D.GetString(this.cbo화폐단위.SelectedValue) != "000")
						this.cur환율.Enabled = true;

					this.cbo부가세구분.Enabled = true;
					this.ctx영업그룹.Enabled = true;
					this.ctx거래처.Enabled = true;

					if (this.수주Config.부가세포함단가사용())
						this.cbo부가세.Enabled = true;
				}

				this.IsInvAmCalc();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn거래처PO적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || (this.txt거래처PO).Text == string.Empty)
					return;

				DataRow[] dataRowArray = !this.추가모드여부 ? this._flexH.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", string.Empty, DataViewRowState.CurrentRows) : this._flexH.DataTable.Select();

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("적용대상") });
				}
				else
				{
					this._flexH.Redraw = false;

					foreach (DataRow dataRow in dataRowArray)
					{
						if (dataRow.RowState != DataRowState.Deleted)
							dataRow["NO_PO_PARTNER"] = (this.txt거래처PO).Text;
					}

					this._flexH.Redraw = true;
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("거래처PO NO") });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flexH.Redraw = true;
			}
		}

		private void btn_apply_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
					return;

				string str = D.GetString(this.cbo공장.SelectedValue);
				string codeValue = this.ctx창고적용.CodeValue;
				DataRow[] dataRowArray = !this.추가모드여부 ? this._flexH.DataTable.Select("(STA_SO1 = '' OR STA_SO1 = 'O' OR STA_SO1 IS NULL) AND CD_PLANT = '" + str + "'", string.Empty, DataViewRowState.CurrentRows) : this._flexH.DataTable.Select("CD_PLANT = '" + str + "'", string.Empty, DataViewRowState.CurrentRows);

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("적용대상") });
				}
				else
				{
					string multiItem = Duzon.ERPU.MF.Common.Common.MultiString(dataRowArray, "CD_ITEM", "|");
					string[] pipes = D.StringConvert.GetPipes(multiItem, 80);
					if (multiItem.Length == 1)
					{
						this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("품목") });
					}
					else
					{
						DataTable dataTable1 = null;

						for (int index = 0; index < pipes.Length; ++index)
						{
							DataTable qtInvMulti = BASIC.GetQtInvMulti(str, codeValue, pipes[index], this.dtp수주일자.Text);

							if (qtInvMulti != null && qtInvMulti.Rows.Count > 0)
							{
								if (dataTable1 == null)
									dataTable1 = qtInvMulti.Clone();

								dataTable1.Merge(qtInvMulti);
							}
							else if (dataTable1 == null || dataTable1.Rows.Count == 0)
								dataTable1 = qtInvMulti.Clone();
						}

						DataTable table1 = dataTable1.DefaultView.ToTable(true, "CD_PLANT", "CD_SL", "CD_ITEM", "QT_INV");
						table1.PrimaryKey = new DataColumn[] { table1.Columns["CD_PLANT"],
															   table1.Columns["CD_SL"],
															   table1.Columns["CD_ITEM"] };
						this._flexH.Redraw = false;

						foreach (DataRow dataRow1 in dataRowArray)
						{
							if (dataRow1.RowState != DataRowState.Deleted)
							{
								dataRow1["CD_SL"] = this.ctx창고적용.CodeValue;
								dataRow1["NM_SL"] = this.ctx창고적용.CodeName;
								dataRow1["CD_WH"] = this.ctxWH.CodeValue;
								dataRow1["NM_WH"] = this.ctxWH.CodeName;
								DataRow dataRow2 = table1.Rows.Find(new object[] { str,
																				   codeValue,
																				   D.GetString(dataRow1["CD_ITEM"]) });
								dataRow1["SL_QT_INV"] = dataRow2 != null ? D.GetDecimal(dataRow2["QT_INV"]) : 0;
							}
						}

						this._flexH.Redraw = true;
						this._flexH.SumRefresh();
						this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn창고적용.Text });
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || this.ctx납품처.CodeValue == string.Empty)
					return;

				DataRow[] dataRowArray = !this.추가모드여부 ? this._flexH.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", string.Empty, DataViewRowState.CurrentRows) : this._flexH.DataTable.Select();

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("적용대상") });
				}
				else
				{
					this._flexH.Redraw = false;

					foreach (DataRow dataRow in dataRowArray)
					{
						if (dataRow.RowState != DataRowState.Deleted)
						{
							dataRow["GI_PARTNER"] = this.ctx납품처.CodeValue;
							dataRow["LN_PARTNER"] = this.ctx납품처.CodeName;
						}
					}

					this._flexH.Redraw = true;
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("납품처적용") });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flexH.Redraw = true;
			}
		}

		private void btnBOM적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || !this.Chk확정여부())
					return;

				this.BOM적용();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void BOM적용()
		{
			DataTable dataTable = this._biz.BOM적용(new List<string>() { MA.Login.회사코드,
																		 D.GetString(this._flexH["CD_PLANT"]),
																		 D.GetString(this._flexH["CD_ITEM"]) }.ToArray());

			if (dataTable == null || dataTable.Rows.Count == 0)
			{
				this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("적용할 BOM") });
			}
			else
			{
				this._flexL.Redraw = false;

				try
				{
					this.소요자재그리드CLEAR();

					foreach (DataRow row in dataTable.Rows)
					{
						if (row.RowState != DataRowState.Deleted)
						{
							this._flexL.Rows.Add();
							this._flexL.Row = this._flexL.Rows.Count - 1;
							this._flexL["NO_SO"] = this.txt수주번호.Text;
							this._flexL["SEQ_SO"] = this._flexH["SEQ_SO"];
							this._flexL["SEQ_SO_LINE"] = D.GetString(row["SEQ_SO_LINE"]);
							this._flexL["CD_MATL"] = D.GetString(row["CD_MATL"]);
							this._flexL["NM_ITEM"] = D.GetString(row["NM_ITEM"]);
							this._flexL["UNIT_IM"] = D.GetString(row["UNIT_IM"]);
							this._flexL["STND_ITEM"] = D.GetString(row["STND_ITEM"]);
							this._flexL["STND_DETAIL_ITEM"] = D.GetString(row["STND_DETAIL_ITEM"]);
							this._flexL["QT_NEED"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_NEED"]) * D.GetDecimal(this._flexH["QT_SO"]));
							this._flexL["QT_NEED_UNIT"] = D.GetString(row["QT_NEED_UNIT"]);
							this._flexL.AddFinished();
						}
					}

					this._flexL.Col = this._flexL.Cols.Fixed;
					this._flexL.Focus();
				}
				finally
				{
					this._flexL.Redraw = true;
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btnBOM적용.Text });
			}
		}

		private void btn소요자재추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Chk확정여부() || !this._flexH.HasNormalRow)
					return;

				if (D.GetString(this._flexH["CD_ITEM"]) == string.Empty || D.GetDecimal(this._flexH["QT_SO"]) == 0)
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("품목과 수량") });
				}
				else
				{
					decimal maxValue = this._flexL.GetMaxValue("SEQ_SO_LINE");
					this._flexL.Rows.Add();
					this._flexL.Row = this._flexL.Rows.Count - 1;
					this._flexL["NO_SO"] = this.txt수주번호.Text;
					this._flexL["SEQ_SO"] = this._flexH["SEQ_SO"];
					this._flexL["SEQ_SO_LINE"] = maxValue + 1;
					this._flexL["QT_NEED"] = 0;
					this._flexL["QT_NEED_UNIT"] = 0;
					this._flexL.AddFinished();
					this._flexL.Col = this._flexL.Cols.Fixed;
					this._flexL.Focus();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn소요자재삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || !this.Chk확정여부() || !this._flexL.HasNormalRow)
					return;

				this._flexL.RemoveItem(this._flexL.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn첨부파일_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
					return;

				if (this._header.CurrentRow.RowState == DataRowState.Added)
				{
					this.ShowMessage("저장 후 파일첨부하여 주십시오");
				}
				else
				{
					string str = D.GetString(this._header.CurrentRow["NO_SO"]) + "_" + D.GetString(this._header.CurrentRow["NO_HST"]);

					if (D.GetString(this._biz.IsFileHelpCheck().Rows[0]["TP_FILESERVER"]) == "0")
					{
						new P_MA_FILE_SUB("SA", Global.MainFrame.CurrentPageID, str).ShowDialog();
					}
					else if (new AttachmentManager(Global.MainFrame.CurrentModule, Global.MainFrame.CurrentPageID, str).ShowDialog() == DialogResult.Cancel)
					{

					};
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn납기일적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || this.dtp납기일.Text == string.Empty)
					return;

				DataRow[] dataRowArray = !this.추가모드여부 ? this._flexH.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", string.Empty, DataViewRowState.CurrentRows) : this._flexH.DataTable.Select();

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("적용대상") });
				}
				else
				{
					this._flexH.Redraw = false;

					foreach (DataRow dataRow in dataRowArray)
					{
						if (dataRow.RowState != DataRowState.Deleted)
						{
							dataRow["DT_DUEDATE"] = this.dtp납기일.Text;
							dataRow["DT_REQGI"] = this._CommFun.DateAdd(this.dtp납기일.Text, "D", D.GetInt(dataRow["LT_GI"]) * -1);
						}
					}

					this._flexH.Redraw = true;
					this._flexH.SumRefresh();
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("납기일적용") });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flexH.Redraw = true;
			}
		}

		private void btn품목전개_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Chk거래처 || !this.Chk영업그룹 || (!this.Chk수주유형 || !this.Chk화폐단위) || !this.Chk과세구분 || !this.Chk적용건(true) || !this.Chk확정여부())
					return;

				P_PU_PO_ITEMEXPSUB pPuPoItemexpsub = new P_PU_PO_ITEMEXPSUB(D.GetString(this.cbo공장.SelectedValue), this.ctx거래처.CodeValue, this.ctx거래처.CodeName, D.GetString(this.cbo단가유형.SelectedValue), this.dtp수주일자.Text, D.GetString(this.cbo화폐단위.SelectedValue));
				pPuPoItemexpsub.SetModule = "002";

				if (pPuPoItemexpsub.ShowDialog() != DialogResult.OK)
					return;

				this.btn추가_Click(null, null);
				this.품목추가(this._flexH.Rows.Count - 1, pPuPoItemexpsub.GetData, true);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn결제조건_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
					return;

				if (this.IsChanged())
				{
					this.ShowMessage("저장 후에 결제조건을 등록 할 수 있습니다.");
				}
				else
				{
					H_SA_SO_PAYCOND_SUB hSaSoPaycondSub = new H_SA_SO_PAYCOND_SUB();
					hSaSoPaycondSub.SetNoSo = this.txt수주번호.Text;
					hSaSoPaycondSub.SetisConfirm = this._biz.IsConfirm(this.txt수주번호.Text);
					hSaSoPaycondSub.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn프로젝트적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || this.ctx프로젝트.IsEmpty())
					return;

				DataRow[] dataRowArray = !this.추가모드여부 ? this._flexH.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", string.Empty, DataViewRowState.CurrentRows) : this._flexH.DataTable.Select();

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("적용대상") });
				}
				else
				{
					this._flexH.Redraw = false;

					foreach (DataRow dataRow in dataRowArray)
					{
						if (dataRow.RowState != DataRowState.Deleted)
						{
							dataRow["NO_PROJECT"] = this.ctx프로젝트.CodeValue;
							dataRow["NM_PROJECT"] = this.ctx프로젝트.CodeName;
							dataRow["SEQ_PROJECT"] = 0;

							if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y")
							{
								dataRow["SEQ_PROJECT"] = this.d_SEQ_PROJECT;
								dataRow["CD_UNIT"] = this.s_CD_PJT_ITEM;
								dataRow["NM_UNIT"] = this.s_NM_PJT_ITEM;
								dataRow["STND_UNIT"] = this.s_PJT_ITEM_STND;
							}
						}
					}

					this._flexH.Redraw = true;
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("프로젝트적용") });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flexH.Redraw = true;
			}
		}

		private void btn할인율적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
					return;

				if (this.cur할인율.DecimalValue == 0)
					return;

				DataRow[] dataRowArray = !this.추가모드여부 ? this._flexH.DataTable.Select("STA_SO1 = '' OR STA_SO1 = 'O'", string.Empty, DataViewRowState.CurrentRows) : this._flexH.DataTable.Select();

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지._이가존재하지않습니다, new string[] { this.DD("적용대상") });
				}
				else
				{
					this._flexH.Redraw = false;

					foreach (DataRow dataRow in dataRowArray)
					{
						if (dataRow.RowState != DataRowState.Deleted)
						{
							dataRow["RT_DSCNT"] = this.cur할인율.DecimalValue;
							dataRow["UM_SO"] = !(D.GetDecimal(dataRow["UM_BASE"]) != 0) ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["UM_BASE"]) - D.GetDecimal(dataRow["UM_BASE"]) * D.GetDecimal(dataRow["RT_DSCNT"]) / 100);
							dataRow["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_SO"]) * D.GetDecimal(dataRow["UM_SO"]));
							dataRow["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_SO"]) * this.cur환율.DecimalValue);
							dataRow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_WONAMT"]) * (D.GetDecimal(dataRow["RT_VAT"]) / 100));
							dataRow["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_WONAMT"]) + D.GetDecimal(dataRow["AM_VAT"]));
							dataRow["UMVAT_SO"] = !(D.GetDecimal(dataRow["QT_SO"]) != 0) ? Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AMVAT_SO"])) : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AMVAT_SO"]) / D.GetDecimal(dataRow["QT_SO"]));

							if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
							{
								수주관리.Calc calc = new 수주관리.Calc();
								dataRow["AM_PROFIT"] = calc.예상이익계산(D.GetDecimal(dataRow["QT_SO"]), D.GetDecimal(dataRow["UM_INV"]), D.GetDecimal(dataRow["AM_WONAMT"]));
							}
						}
					}

					this.IsInvAmCalc();
					this._flexH.SumRefresh();
					this._flexH.Redraw = true;
					this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("할인율 적용") });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn계정처리유형변경_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
					return;

				foreach (DataRow row in this._flexH.DataTable.Rows)
				{
					if (row.RowState != DataRowState.Deleted)
						row["TP_IV"] = D.GetString(this.cbo계정처리유형.SelectedValue);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn담당자정보적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.ctx거래처.CodeValue == string.Empty)
				{
					this.ShowMessage("거래처를 입력해주세요.");
				}
				else
				{
					P_CZ_FI_PARTNERPTR_SUB pFiPartnerptrSub = new P_CZ_FI_PARTNERPTR_SUB(this.ctx거래처.CodeValue);

					if (pFiPartnerptrSub.ShowDialog() != DialogResult.OK)
						return;

					object[] returnValues = pFiPartnerptrSub.ReturnValues;
					if (returnValues == null)
						return;

					this.txt거래처담당자.Text = D.GetString(returnValues[0]);
					this._header.CurrentRow["NM_PTR"] = D.GetString(returnValues[0]);
					this.txt담당자이메일.Text = D.GetString(returnValues[1]);
					this._header.CurrentRow["EX_EMIL"] = D.GetString(returnValues[1]);
					this.txt담당자핸드폰.Text = D.GetString(returnValues[2]);
					this._header.CurrentRow["EX_HP"] = D.GetString(returnValues[2]);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex.Message);
			}
		}

		private void btn메일전송_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.추가모드여부)
					return;

				if (this.IsChanged())
				{
					this.ShowMessage("수정된 사항이 있습니다. 저장 후 메일전송버튼을 클릭하세요.");
				}
				else
				{
					DataRow codeInfo = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx거래처.CodeValue });
					DataRow dataRow1 = this._biz.SerchBizarea(D.GetString(this._flexH[this._flexH.Rows.Fixed, "CD_PLANT"]));
					DataRow dataRow2 = null;
					DataRow dataRow3 = null;
					DataRow dataRow4 = null;
					DataRow dataRow5 = null;
					DataRow dataRow6 = null;
					DataRow dataRow7 = null;

					if (D.GetString(this.cbo원산지.SelectedValue) != string.Empty)
					{
						DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT NM_SYSDEF_E 
																					FROM MA_CODEDTL WITH(NOLOCK) 
																					WHERE CD_COMPANY = '{0}' 
																					AND CD_FIELD = 'MA_B000020' 
																					AND CD_SYSDEF = '{1}'", MA.Login.회사코드, D.GetString(this.cbo원산지.SelectedValue)));

						if (dataTable != null && dataTable.Rows.Count > 0)
							dataRow2 = dataTable.Rows[0];
					}

					if (this.ctx거래처.CodeValue != string.Empty)
					{
						DataTable dataTable = DBHelper.GetDataTable(string.Format(@"SELECT NO_TEL1, 
																						   NO_FAX1, 
																						   CD_EMP_PARTNER, 
																						   E_MAIL 
																					FROM MA_PARTNER WITH(NOLOCK) 
																					WHERE CD_COMPANY = '{0}' 
																					AND CD_PARTNER = '{1}'", MA.Login.회사코드, this.ctx거래처.CodeValue));

						if (dataTable != null && dataTable.Rows.Count > 0)
							dataRow6 = dataTable.Rows[0];
					}

					if (this.ctx착하통지처.CodeValue != string.Empty)
						dataRow3 = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx착하통지처.CodeValue });
					if (this.ctx수하인.CodeValue != string.Empty)
						dataRow4 = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx수하인.CodeValue });
					if (this.ctx은행.CodeValue != string.Empty)
						dataRow5 = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx은행.CodeValue });
					if (this.ctx수출자.CodeValue != string.Empty)
						dataRow7 = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PARTNER_DETAIL, new string[] { MA.Login.회사코드, this.ctx수출자.CodeValue });

					Dictionary<string, string> dictionary = new Dictionary<string, string>();

					dictionary["NO_SO"] = this.txt수주번호.Text;
					dictionary["DT_SO"] = this.dtp수주일자.Text.Substring(0, 4) + "/" + this.dtp수주일자.Text.Substring(4, 2) + "/" + this.dtp수주일자.Text.Substring(6, 2);
					dictionary["PARTNER"] = this.ctx거래처.CodeValue;
					dictionary["CD_PARTNER"] = this.ctx거래처.CodeName;
					dictionary["CD_SALEGRP"] = this.ctx영업그룹.CodeName;
					dictionary["NO_KOR"] = this.ctx담당자.CodeName;
					dictionary["TP_SO"] = this.ctx수주형태.CodeName;
					dictionary["CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
					dictionary["NM_CD_EXCH"] = this.cbo화폐단위.Text;
					dictionary["RT_EXCH"] = this.cur환율.Text;
					dictionary["TP_PRICE"] = this.cbo단가유형.Text;
					dictionary["NO_PROJECT"] = this.ctx프로젝트.CodeName;
					dictionary["TP_VAT"] = this.cbo부가세구분.Text;
					dictionary["RT_VAT"] = this.cur부가세율.Text;
					dictionary["FG_VAT"] = this.cbo부가세.Text;
					dictionary["FG_TAXP"] = this.rdo계산서처리일괄.Checked ? this.rdo계산서처리일괄.Text : this.rdo계산서처리건별.Text;
					dictionary["DC_RMK"] = this.txt비고.Text;
					dictionary["FG_BILL"] = this.cbo결재방법.Text;
					dictionary["FG_TRANSPORT"] = this.cbo운송방법.Text;
					dictionary["NO_CONTRACT"] = this.txt계약번호.Text;
					dictionary["NO_PO_PARTNER"] = this.txt거래처PO.Text;
					dictionary["NM_CEO"] = D.GetString(codeInfo["NM_CEO"]);
					dictionary["NO_TEL"] = D.GetString(codeInfo["NO_TEL"]);
					dictionary["NO_FAX"] = D.GetString(codeInfo["NO_FAX"]);
					dictionary["NO_COMPANY"] = D.GetString(codeInfo["NO_COMPANY"]);
					dictionary["NM_PTR"] = D.GetString(codeInfo["NM_PTR"]);
					dictionary["TP_JOB"] = D.GetString(codeInfo["TP_JOB"]);
					dictionary["CLS_JOB"] = D.GetString(codeInfo["CLS_JOB"]);
					dictionary["DC_ADS1_H"] = D.GetString(codeInfo["DC_ADS1_H"]);
					dictionary["DC_ADS1_D"] = D.GetString(codeInfo["DC_ADS1_D"]);
					dictionary["DC_ADS1"] = D.GetString(codeInfo["DC_ADS1_H"]) + " " + D.GetString(codeInfo["DC_ADS1_D"]);
					dictionary["AM_WONAMT_SUM"] = D.GetString(this._flexH[1, "AM_WONAMT"]);
					dictionary["AM_VAT_SUM"] = D.GetString(this._flexH[1, "AM_VAT"]);
					dictionary["AMVAT_SO_SUM"] = D.GetString(this._flexH[1, "AMVAT_SO"]);
					dictionary["NM_BUSI"] = D.GetString(Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_CODEDTL, new object[] { MA.Login.회사코드, "PU_C000016", this._거래구분 })["NM_SYSDEF"]);
					dictionary["AM_SO_SUM"] = D.GetString(this._flexH[1, "AM_SO"]);
					dictionary["QT_SO_SUM"] = D.GetString(this._flexH[1, "QT_SO"]);
					dictionary["NM_EXPORT"] = this.ctx수출자.CodeName;
					dictionary["NM_PRODUCT"] = this.ctx제조자.CodeName;
					dictionary["COND_TRANS"] = this.txt인도조건.Text;
					dictionary["NM_INSPECT"] = this.txt검사기관.Text;
					dictionary["DESTINATION"] = this.txt목적지.Text;
					dictionary["NM_COND_PAY"] = this.cbo결제형태.Text;
					dictionary["COND_DAYS"] = this.cur결제일.Text;
					dictionary["DT_EXPIRY"] = this.dtp유효일자.Text;
					dictionary["NM_TP_PACKING"] = this.cbo포장형태.Text;
					dictionary["NM_TP_TRANSPORT"] = this.cbo운송형태.Text;
					dictionary["NM_CD_ORIGIN"] = this.cbo원산지.Text;
					dictionary["PORT_LOADING"] = this.txt선적항.Text;
					dictionary["PORT_ARRIVER"] = this.txt도착항.Text;
					dictionary["NM_COND_PRICE"] = this.cbo가격조건.Text;
					dictionary["NM_NOTIFY"] = this.ctx착하통지처.CodeName;
					dictionary["NM_CONSIGNEE"] = this.ctx수하인.CodeName;
					dictionary["DC_RMK_TEXT"] = this.txt멀티비고1.Text;
					dictionary["DC_RMK_TEXT2"] = this.txt멀티비고2.Text;
					dictionary["DC_RMK1"] = this.txt비고1.Text;
					dictionary["NO_BIZAREA"] = D.GetString(dataRow1["NO_BIZAREA"]);
					dictionary["NM_BIZAREA"] = D.GetString(dataRow1["NM_BIZAREA"]);
					dictionary["NM_MASTER_BIZAREA"] = D.GetString(dataRow1["NM_MASTER"]);
					dictionary["ADS_H_BIZAREA"] = D.GetString(dataRow1["ADS_H"]);
					dictionary["ADS_D_BIZAREA"] = D.GetString(dataRow1["ADS_D"]);
					dictionary["ADS_BIZAREA"] = D.GetString(dataRow1["ADS_H"]) + " " + D.GetString(dataRow1["ADS_D"]);
					dictionary["TP_JOB_BIZAREA"] = D.GetString(dataRow1["TP_JOB"]);
					dictionary["CLS_JOB_BIZAREA"] = D.GetString(dataRow1["CLS_JOB"]);
					dictionary["NO_TEL_BIZAREA"] = D.GetString(dataRow1["NO_TEL"]);

					if (dataRow2 != null)
						dictionary["NM_SYSDEF_E"] = D.GetString(dataRow2["NM_SYSDEF_E"]);

					if (dataRow3 != null)
					{
						dictionary["NOTIFY_DC_ADS1_H"] = D.GetString(dataRow3["DC_ADS1_H"]);
						dictionary["NOTIFY_DC_ADS1_D"] = D.GetString(dataRow3["DC_ADS1_D"]);
						dictionary["NOTIFY_NO_TEL"] = D.GetString(dataRow3["NO_TEL"]);
					}

					if (dataRow4 != null)
					{
						dictionary["CONSIGNEE_DC_ADS1_H"] = D.GetString(dataRow4["DC_ADS1_H"]);
						dictionary["CONSIGNEE_DC_ADS1_D"] = D.GetString(dataRow4["DC_ADS1_D"]);
					}

					if (dataRow5 != null)
					{
						dictionary["CD_BANK_SO"] = this.ctx은행.CodeValue;
						dictionary["NM_BANK_SO"] = this.ctx은행.CodeName;
						dictionary["BANK_SO_NM_TEXT"] = D.GetString(dataRow5["NM_TEXT"]);
					}

					if (dataRow6 != null)
					{
						dictionary["NO_TEL1"] = D.GetString(dataRow6["NO_TEL1"]);
						dictionary["NO_FAX1"] = D.GetString(dataRow6["NO_FAX1"]);
						dictionary["CD_EMP_PARTNER"] = D.GetString(dataRow6["CD_EMP_PARTNER"]);
						dictionary["E_MAIL"] = D.GetString(dataRow6["E_MAIL"]);
					}

					if (dataRow7 != null)
					{
						dictionary["EXPORT_DC_ADS1_H"] = D.GetString(dataRow7["DC_ADS1_H"]);
						dictionary["EXPORT_DC_ADS1_D"] = D.GetString(dataRow7["DC_ADS1_D"]);
					}

					DataTable dataTable1 = this._biz.Search_Print(this.txt수주번호.Text);
					ReportHelper reportHelper1;
					ReportHelper reportHelper2 = reportHelper1 = new ReportHelper("R_SA_SO_RPT_001", "수주서");
					reportHelper2.SetDataTable(dataTable1);
					StringBuilder stringBuilder = new StringBuilder();
					string str1 = D.GetString(this.ctx담당자.CodeName) + "/" + D.GetString(this.txt수주번호.Text) + "/" + D.GetString(this.ctx거래처.CodeName) + "수주가 등록되었습니다.";

					foreach (DataRow row in this._flexH.DataTable.Rows)
					{
						if (row.RowState != DataRowState.Deleted)
						{
							string[] strArray1 = new string[] { "품목코드: ",
																D.GetString(row["CD_ITEM"]),
																" / 품목명: ",
																D.GetString(row["NM_ITEM"]),
																" / 규격: ",
																D.GetString(row["STND_ITEM"]),
																" / 단위: ",
																D.GetString(row["UNIT_SO"]),
																" / 수량: ",
																null,
																null,
																null,
																null,
																null,
																null,
																null,
																null,
																null };

							string[] strArray2 = strArray1;
							int index1 = 9;
							decimal num2 = D.GetDecimal(row["QT_SO"]);
							string str2 = num2.ToString((this._flexH.Cols["QT_SO"]).Format);
							strArray2[index1] = str2;
							strArray1[10] = " / 단가: ";
							string[] strArray3 = strArray1;
							int index2 = 11;
							num2 = D.GetDecimal(row["UM_SO"]);
							string str3 = num2.ToString((this._flexH.Cols["UM_SO"]).Format);
							strArray3[index2] = str3;
							strArray1[12] = "/ 금액: ";
							string[] strArray4 = strArray1;
							int index3 = 13;
							num2 = D.GetDecimal(row["AM_SO"]);
							string str4 = num2.ToString((this._flexH.Cols["AM_SO"]).Format);
							strArray4[index3] = str4;
							strArray1[14] = " / 프로젝트코드: ";
							strArray1[15] = D.GetString(row["NO_PROJECT"]);
							strArray1[16] = " / 프로젝트명: ";
							strArray1[17] = D.GetString(row["NM_PROJECT"]);
							string str5 = string.Concat(strArray1);
							stringBuilder.AppendLine(str5);
							stringBuilder.AppendLine("\n\n");
						}
					}

					string[] strArray = new string[] { str1,
													   string.Empty,
													   stringBuilder.ToString() };

					P_MF_EMAIL pMfEmail = new P_MF_EMAIL(new string[] { this.txt수주번호.Text }, "R_SA_SO_RPT_001", new ReportHelper[] { reportHelper2 }, dictionary, "수주서", strArray);
					pMfEmail.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn업무공유_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.추가모드여부 || D.GetString(this._flexH["ID_MEMO"]) == string.Empty)
					return;

				(new P_WS_PM_S_JOBSHARE_SUB1(this, D.GetString(this._flexH["ID_MEMO"]), new object[] { "B01",
																									  string.Empty,
																									  Global.MainFrame.LoginInfo.CompanyCode,
																									  D.GetString(this._flexH["NO_PROJECT"]),
																									  D.GetString(this._flexH["CD_WBS"]),
																									  D.GetString(this._flexH["NO_SHARE"]),
																									  D.GetString(this._flexH["NO_ISSUE"]),
																									  "03" })).ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn영업기회적용_Click(object sender, EventArgs e)
		{
			try
			{
				P_SA_CHANCE_SUB pSaChanceSub = new P_SA_CHANCE_SUB();

				if ((pSaChanceSub).ShowDialog() != DialogResult.OK)
					return;

				DataRow row1 = pSaChanceSub.dtH.Rows[0];
				DataTable dtD = pSaChanceSub.dtD;

				this._header.CurrentRow["CD_CHANCE"] = row1["CD_CHANCE"];
				this._header.CurrentRow["CD_PARTNER"] = D.GetString(row1["CD_PARTNER"]);
				this._header.CurrentRow["LN_PARTNER"] = D.GetString(row1["LN_PARTNER"]);
				this._header.CurrentRow["CD_SALEGRP"] = D.GetString(row1["CD_SALEGRP"]);
				this._header.CurrentRow["NM_SALEGRP"] = D.GetString(row1["NM_SALEGRP"]);
				this._header.CurrentRow["NO_PROJECT"] = D.GetString(row1["NO_PROJECT"]);
				this._header.CurrentRow["NM_PROJECT"] = D.GetString(row1["NM_PROJECT"]);
				this._header.CurrentRow["TP_SO"] = D.GetString(row1["TP_SO"]);
				this._header.CurrentRow["NM_SO"] = D.GetString(row1["NM_SO"]);
				this._header.CurrentRow["NO_EMP"] = D.GetString(row1["NO_EMP"]);
				this._header.CurrentRow["NM_KOR"] = D.GetString(row1["NM_KOR"]);
				this.ctx거래처.SetCode(D.GetString(row1["CD_PARTNER"]), D.GetString(row1["LN_PARTNER"]));
				this.ctx영업그룹.SetCode(D.GetString(row1["CD_SALEGRP"]), D.GetString(row1["NM_SALEGRP"]));
				this.ctx프로젝트.SetCode(D.GetString(row1["NO_PROJECT"]), D.GetString(row1["NM_PROJECT"]));
				this.ctx수주형태.SetCode(D.GetString(row1["TP_SO"]), D.GetString(row1["NM_SO"]));
				this.ctx담당자.SetCode(D.GetString(row1["NO_EMP"]), D.GetString(row1["NM_KOR"]));
				this.수주형태변경시셋팅(D.GetString(row1["TP_SO"]));
				this.영업그룹변경시셋팅(this.ctx영업그룹.CodeValue);
				int num1 = 1;

				foreach (DataRow row2 in dtD.Rows)
				{
					DataRow row3 = this._flexH.DataTable.NewRow();

					row3["SEQ_SO"] = num1++;
					row3["GI_PARTNER"] = this.ctx납품처.CodeValue;
					row3["LN_PARTNER"] = this.ctx납품처.CodeName;
					row3["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row3["UM_SO"] = row3["AM_SO"] = row3["AM_VAT"] = row3["QT_IM"] = 0));

					if (this.disCount_YN == "Y")
					{
						row3["RT_DSCNT"] = 0;
						row3["UM_BASE"] = 0;
					}

					row3["TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
					row3["RT_VAT"] = this.cur부가세율.DecimalValue;
					string CD_CC;
					string NM_CC;
					this.CC조회(D.GetString(row2["GRP_ITEM"]), D.GetString(row2["CD_ITEM"]), D.GetString(row2["CD_PLANT"]), out CD_CC, out NM_CC);
					row3["CD_CC"] = CD_CC;
					row3["NM_CC"] = NM_CC;
					row3["DT_DUEDATE"] = this.dtp납기일.Text;
					row3["DT_REQGI"] = this.dtp납기일.Text;
					row3["STA_SO1"] = this._수주상태;
					row3["CD_PLANT"] = row2["CD_PLANT"];
					row3["CD_ITEM"] = row2["CD_ITEM"];
					row3["NM_ITEM"] = row2["NM_ITEM"];
					row3["STND_ITEM"] = row2["STND_ITEM"];
					row3["UNIT_SO"] = row2["UNIT_SO"];
					row3["TP_ITEM"] = row2["TP_ITEM"];
					row3["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row2["QT"]));
					row3["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(row2["UM"]));
					row3["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM"]));
					row3["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row2["AM"]) * this.cur환율.DecimalValue);
					row3["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row3["AM_WONAMT"]) * (this.cur부가세율.DecimalValue / 100));
					row3["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row3["AM_WONAMT"]) + D.GetDecimal(row3["AM_VAT"]));
					row3["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row3["AMVAT_SO"]) / D.GetDecimal(row3["QT_SO"]));
					row3["UNIT_SO_FACT"] = D.GetDecimal(row2["UNIT_SO_FACT"]) == 0 ? 1 : row2["UNIT_SO_FACT"];
					row3["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row3["QT_SO"]) * D.GetDecimal(row3["UNIT_SO_FACT"]));
					row3["UNIT_IM"] = row2["UNIT_IM"];
					row3["EN_ITEM"] = row2["EN_ITEM"];
					row3["STND_DETAIL_ITEM"] = row2["STND_DETAIL_ITEM"];
					row3["GRP_MFG"] = row2["GRP_MFG"];
					row3["LT_GI"] = D.GetDecimal(row2["LT_GI"]);
					row3["WEIGHT"] = row2["WEIGHT"];
					row3["UNIT_WEIGHT"] = row2["UNIT_WEIGHT"];
					row3["FG_SERNO"] = row2["FG_SERNO"];
					row3["YN_ATP"] = row2["YN_ATP"];
					row3["CUR_ATP_DAY"] = row2["CUR_ATP_DAY"];
					row3["FG_MODEL"] = row2["FG_MODEL"];
					row3["UNIT_SO_FACT"] = (D.GetDecimal(row2["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(row2["UNIT_SO_FACT"]));
					row3["UNIT_GI_FACT"] = (D.GetDecimal(row2["UNIT_GI_FACT"]) == 0 ? 1 : D.GetDecimal(row2["UNIT_GI_FACT"]));
					row3["NO_PROJECT"] = D.GetString(row1["NO_PROJECT"]);
					row3["NM_PROJECT"] = D.GetString(row1["NM_PROJECT"]);
					row3["CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
					row3["TP_IV"] = this._매출형태;
					row3["MAT_ITEM"] = row2["MAT_ITEM"];

					if (BASIC.GetMAEXC("배차사용유무") == "Y")
						row3["YN_PICKING"] = this._배송여부;

					this._flexH.DataTable.Rows.Add(row3);
				}

				this._flexH.SumRefresh();
				this._flexH.Row = this._flexH.Rows.Count - 1;
				this._flexH.IsDataChanged = true;
				this.ToolBarSaveButtonEnabled = true;
				this.Page_DataChanged(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 컨트롤 이벤트
		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			BpCodeTextBox bpCodeTextBox = sender as BpCodeTextBox;
			HelpID helpId = e.HelpID;

			if (helpId <= HelpID.P_MA_SL_SUB)
			{
				if (helpId != HelpID.P_USER)
				{
					if (helpId != HelpID.P_MA_SL_SUB)
						return;

					e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
				}
				else if (bpCodeTextBox.UserHelpID == "H_SA_PRJ_SUB")
				{
					e.HelpParam.P14_CD_PARTNER = this.ctx거래처.CodeValue;
					e.HelpParam.P63_CODE3 = this.ctx거래처.CodeName;
					e.HelpParam.P17_CD_SALEGRP = this.ctx영업그룹.CodeValue;
					e.HelpParam.P62_CODE2 = this.ctx영업그룹.CodeName;
				}
			}
			else if (helpId != HelpID.P_FI_MNGD_SUB)
			{
				switch (helpId - HelpID.P_SA_TPPTR_SUB)
				{
					case 0:
						e.HelpParam.P14_CD_PARTNER = this.ctx거래처.CodeValue;
						break;
					case 1:
						e.HelpParam.P61_CODE1 = "N";
						e.HelpParam.P62_CODE2 = "Y";
						break;
				}
			}
		}

		private void Control_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				BpCodeTextBox bpCodeTextBox = sender as BpCodeTextBox;
				HelpID helpId = e.HelpID;

				if (bpCodeTextBox.Name == this.ctx거래처.Name)
				{
					this.ctx납품처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
					this.ctx납품처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);

					this.ctx입고처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
					this.ctx입고처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);
					this._header.CurrentRow["NO_NEGO"] = this.ctx납품처.CodeValue;

					DataTable dt = DBHelper.GetDataTable(@"SELECT 0 AS CODE,
																  '' AS NAME
														   UNION ALL
														   SELECT FP.SEQ AS CODE,
																  FP.NM_PTR AS NAME
														   FROM FI_PARTNERPTR FP WITH(NOLOCK)
														   WHERE FP.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
														  "AND FP.CD_PARTNER = '" + this.ctx거래처.CodeValue + "'");

					this.cbo설계담당자.DataSource = dt.Copy();
					this.cbo설계담당자.ValueMember = "CODE";
					this.cbo설계담당자.DisplayMember = "NAME";

					this.cbo구매담당자.DataSource = dt.Copy();
					this.cbo구매담당자.ValueMember = "CODE";
					this.cbo구매담당자.DisplayMember = "NAME";

					this.cbo인수자.DataSource = dt.Copy();
					this.cbo인수자.ValueMember = "CODE";
					this.cbo인수자.DisplayMember = "NAME";

					if (BASIC.GetMAEXC("수주등록-여신잔액 표시") == "001")
					{
						this.lbl여신잔액.Visible = this.cur여신잔액.Visible = true;
						this.cur여신잔액.DecimalValue = 여신관리.GET_CREDIT_REMAIN(this.ctx거래처.CodeValue, this.dtp수주일자.Text);
						this.cur여신잔액.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT);
					}

					string maexc1 = BASIC.GetMAEXC("거래처선택-영업그룹적용");
					string maexc2 = BASIC.GetMAEXC("거래처선택-수주형태적용");
					string maexc3 = BASIC.GetMAEXC("거래처선택-담당자적용");
					string maexc4 = BASIC.GetMAEXC("거래처선택-단가유형적용");
					DataRow partner1 = BASIC.GetPartner(e.CodeValue);
					string 영업그룹 = D.GetString(partner1["CD_SALEGRP"]);
					string str1 = D.GetString(partner1["NM_SALEGRP"]);
					string 수주형태 = D.GetString(partner1["TP_SO"]);
					string str2 = D.GetString(partner1["NM_SO"]);
					string str3 = D.GetString(partner1["CD_EMP_SALE"]);
					string str4 = D.GetString(partner1["NM_EMP"]);
					string str5 = D.GetString(partner1["FG_TAXP"]);

					if (maexc2 == "Y" && 수주형태 != this.ctx수주형태.CodeValue)
					{
						this.ctx수주형태.SetCode(수주형태, str2);
						this._header.CurrentRow["TP_SO"] = 수주형태;
						this._header.CurrentRow["NM_SO"] = str2;
						this.수주형태변경시셋팅(수주형태);
					}

					if (maexc1 == "Y" && 영업그룹 != this.ctx영업그룹.CodeValue)
					{
						this.ctx영업그룹.SetCode(영업그룹, str1);
						this._header.CurrentRow["CD_SALEGRP"] = 영업그룹;
						this._header.CurrentRow["NM_SALEGRP"] = str1;
						this.영업그룹변경시셋팅(영업그룹);
					}

					int num1;

					if (!(maexc3 == "Y"))
						num1 = 1;
					else
						num1 = 0;

					if (num1 == 0)
					{
						this.ctx담당자.SetCode(str3, str4);
						this._header.CurrentRow["NO_EMP"] = str3;
						this._header.CurrentRow["NM_KOR"] = str4;
					}

					if (str5 != D.GetString(this._header.CurrentRow["FG_TAXP"]))
					{
						if (str5 == "001")
						{
							this.rdo계산서처리일괄.Checked = true;
							this.rdo계산서처리건별.Checked = false;
						}
						else
						{
							this.rdo계산서처리일괄.Checked = false;
							this.rdo계산서처리건별.Checked = true;
						}
						this._header.CurrentRow["FG_TAXP"] = str5;
					}

					if (maexc4 == "Y" && partner1.Table.Columns.Contains("FG_UM"))
					{
						this.cbo단가유형.SelectedValue = partner1["FG_UM"];
						this._header.CurrentRow["TP_PRICE"] = partner1["FG_UM"];
					}

					if (ConfigSA.SA_EXC.거래처선택_운송방법적용)
						this._header.CurrentRow["FG_TRANSPORT"] = this.cbo운송방법.SelectedValue = partner1["FG_TRANSPORT"];

					string empty1 = string.Empty;
					string empty2 = string.Empty;

					if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
					{
						string str6 = D.GetString(partner1["CD_SL_ISU"]);

						if (str6 != string.Empty)
						{
							DataRow sl = BASIC.GetSL(D.GetString(this.cbo공장.SelectedValue), str6);

							if (sl != null)
								this.ctx창고적용.SetCode(str6, D.GetString(sl["NM_SL"]));
						}
						else
							this.ctx창고적용.Clear();
					}

					this.ctx착하통지처.CodeValue = this.ctx거래처.CodeValue;
					this.ctx착하통지처.CodeName = this.ctx거래처.CodeName;
					this.ctx수하인.CodeValue = this.ctx거래처.CodeValue;
					this.ctx수하인.CodeName = this.ctx거래처.CodeName;
					this._header.CurrentRow["CD_NOTIFY"] = this.ctx거래처.CodeValue;
					this._header.CurrentRow["CD_CONSIGNEE"] = this.ctx거래처.CodeValue;
				}
				else if (bpCodeTextBox.Name == this.ctx입고처.Name)
			    {	
					DataTable dt = DBHelper.GetDataTable(@"SELECT 0 AS CODE,
																  '' AS NAME
														   UNION ALL
														   SELECT FP.SEQ AS CODE,
																  FP.NM_PTR AS NAME
														   FROM FI_PARTNERPTR FP WITH(NOLOCK)
														   WHERE FP.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
														  "AND FP.CD_PARTNER = '" + this.ctx입고처.CodeValue + "'");

					this.cbo인수자.DataSource = dt;
					this.cbo인수자.ValueMember = "CODE";
					this.cbo인수자.DisplayMember = "NAME";
				}
				else if (bpCodeTextBox.Name == this.ctx프로젝트.Name)
				{
					if (BASIC.GetMAEXC("수주등록 - 프로젝트 거래처/영업그룹 동기화") == "Y")
					{
						this.ctx거래처.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
						this.ctx거래처.CodeName = D.GetString(e.HelpReturn.Rows[0]["LN_PARTNER"]);
						this.ctx영업그룹.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_SALEGRP"]);
						this.ctx영업그룹.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_SALEGRP"]);
						this._header.CurrentRow["CD_PARTNER"] = D.GetString(e.HelpReturn.Rows[0]["CD_PARTNER"]);
						this._header.CurrentRow["CD_SALEGRP"] = D.GetString(e.HelpReturn.Rows[0]["CD_SALEGRP"]);
						this.영업그룹변경시셋팅(this.ctx영업그룹.CodeValue);
					}

					if (Duzon.ERPU.MF.Common.Config.MA_ENV.YN_UNIT == "Y")
					{
						this.d_SEQ_PROJECT = D.GetDecimal(e.HelpReturn.Rows[0]["SEQ_PROJECT"]);
						this.s_CD_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["CD_PJT_ITEM"]);
						this.s_NM_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["NM_PJT_ITEM"]);
						this.s_PJT_ITEM_STND = D.GetString(e.HelpReturn.Rows[0]["PJT_ITEM_STND"]);
					}
				}
				else if (bpCodeTextBox.Name == this.ctx영업그룹.Name)
				{
					this.영업그룹변경시셋팅(e.CodeValue);
				}
				else if (bpCodeTextBox.Name == this.ctx수주형태.Name)
				{
					this.수주형태변경시셋팅(e.CodeValue);
				}
				else if (bpCodeTextBox.Name == this.ctx창고적용.Name)
				{
					if (this._biz.WH적용 == "100")
					{
						this.ctxWH.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_WH"]);
						this.ctxWH.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_WH"]);
					}
				}	
				}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_SelectionChangeCommitted(object sender, EventArgs e)
		{
			string name;

			try
			{
				name = ((Control)sender).Name;

				if (name == this.cbo화폐단위.Name)
				{
					this.화폐단위셋팅();
				}
				else if (name == this.cbo부가세구분.Name)
				{
					this.VAT구분셋팅();
				}
				else if (name == this.cbo부가세.Name)
				{
					if (D.GetString(this.cbo부가세.SelectedValue) == "Y" && D.GetString(this.cbo화폐단위.SelectedValue) != "000" && Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
					{
						this.ShowMessage("부가세포함여부는 원화(KRW)인 경우에만 'YES'로 변경 할 수 있습니다.");
						this.cbo부가세.SelectedValue = "N";
						this._header.CurrentRow["FG_VAT"] = "N";
					}
				}
				else if (name == this.cbo공장.Name)
				{
					this.ctx창고적용.CodeValue = string.Empty;
					this.ctx창고적용.CodeName = string.Empty;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 기타 메소드
		private bool FieldCheck(string flag)
		{
			Hashtable hashtable = new Hashtable();

			if (flag != this.btn프로젝트적용1.Name)
			{
				hashtable.Add(this.ctx거래처, this.lbl거래처);
				hashtable.Add(this.ctx영업그룹, this.lbl영업그룹);

				if (App.SystemEnv.PROJECT사용)
					hashtable.Add(this.ctx프로젝트, this.lbl프로젝트);
			}

			hashtable.Add(this.ctx수주형태, this.lbl수주형태);
			hashtable.Add(this.cbo화폐단위, this.lbl화폐단위);
			hashtable.Add(this.cbo단가유형, this.lbl단가유형);
			hashtable.Add(this.cbo부가세구분, this.lblVAT구분);
			hashtable.Add(this.dtp수주일자, this.lbl수주일자);

			return ComFunc.NullCheck(hashtable);
		}

		private void 계산서처리Default셋팅()
		{
			_header.CurrentRow["FG_TAXP"] = "001";
		}

		private void 수주형태변경시셋팅(string 수주형태)
		{
			if (수주형태 == string.Empty)
				return;

			DataRow tpso = BASIC.GetTPSO(수주형태);

			if (D.GetString(tpso["CONF"]) == "Y")
			{
				this._수주상태 = "R";
				this._헤더수정여부 = false;
			}
			else
			{
				this._수주상태 = "O";
				this._헤더수정여부 = true;
			}

			for (int index = this._flexH.Rows.Fixed; index < this._flexH.Rows.Count; ++index)
				this._flexH[index, "STA_SO1"] = this._수주상태;

			this._거래구분 = D.GetString(tpso["TP_BUSI"]);
			this._출하형태 = D.GetString(tpso["TP_GI"]);
			this._매출형태 = D.GetString(tpso["TP_IV"]);
			this._의뢰여부 = D.GetString(tpso["GIR"]);
			this._출하여부 = D.GetString(tpso["GI"]);
			this._매출여부 = D.GetString(tpso["IV"]);
			this._수출여부 = D.GetString(tpso["TRADE"]);
			this._반품여부 = D.GetString(tpso["RET"]);
			this._매출자동여부 = D.GetString(tpso["IV_AUTO"]);
			this._배송여부 = D.GetString(tpso["YN_PICKING"]);
			this._자동승인여부 = D.GetString(tpso["CONF"]);
			string str = D.GetString(tpso["TP_VAT"]);

			if (str == string.Empty)
			{
				this.cbo부가세구분.SelectedValue = "11";
				this._header.CurrentRow["TP_VAT"] = "11";
			}
			else
			{
				this.cbo부가세구분.SelectedValue = str;
				this._header.CurrentRow["TP_VAT"] = str;
			}

			수주관리.Setting setting = new 수주관리.Setting();

			try
			{
				setting.거래구분에따른과세구분(this._거래구분, str);
			}
			catch
			{
				this.cbo부가세구분.SelectedValue = this._header.CurrentRow["TP_VAT"] = setting.거래구분Default과세구분(this._거래구분);
			}

			if (this._거래구분 == "001")
				this.cbo부가세구분.Enabled = true;
			else
				this.cbo부가세구분.Enabled = false;

			this.VAT구분셋팅();

			if (this.수주Config.수주라인CC설정유형() == 수주관리.수주라인CC설정.수주유형 && D.GetString(tpso["CD_CC"]) == string.Empty)
			{
				this.ShowMessage("수주유형에 해당하는 C/C가 설정되지 않았습니다.");
			}

			if (D.GetString(tpso["RET"]) == "N")
			{
				this.btn출하적용.Visible = false;
				this.btn프로젝트적용1.Enabled = true;

				if (this._flexH.Cols["NO_IO_MGMT"].Visible)
				{
					Column column = this._flexH.Cols["NO_IO_MGMT"];
					bool flag;
					this._flexH.Cols["NO_IOLINE_MGMT"].Visible = flag = false;
					int num2 = flag ? 1 : 0;
					column.Visible = num2 != 0;
				}
			}
			else if (this._biz.수주반품사용여부)
			{
				this.btn출하적용.Visible = true;
				this.btn프로젝트적용1.Enabled = false;

				if (!this._flexH.Cols["NO_IO_MGMT"].Visible)
				{
					Column column = this._flexH.Cols["NO_IO_MGMT"];
					bool flag;
					this._flexH.Cols["NO_IOLINE_MGMT"].Visible = flag = true;
					int num2 = flag ? 1 : 0;
					column.Visible = num2 != 0;
				}
			}

			this.IsChkTab5Activate();
			this.cbo계정처리유형.SelectedValue = this._매출형태;
		}

		private void 영업그룹변경시셋팅(string 영업그룹)
		{
			DataRow saleGrp;

			try
			{
				saleGrp = BASIC.GetSaleGrp(영업그룹);
			}
			catch
			{
				return;
			}

			this._단가적용형태 = D.GetString(saleGrp["TP_SALEPRICE"]);
			this.so_Price = D.GetString(saleGrp["SO_PRICE"]);

			if (this._단가적용형태 == "003")
				this.btn품목전개.Visible = true;
			else
				this.btn품목전개.Visible = false;
		}

		private DialogResult ShowMessage(메세지 msg, params object[] paras)
		{
			if (msg == 메세지.이미수주확정되어수정삭제가불가능합니다)
				return this.ShowMessage("SA_M000116");

			return DialogResult.None;
		}

		private void 화폐단위셋팅()
		{
			if (this.cbo화폐단위.SelectedValue == null || D.GetString(this.cbo화폐단위.SelectedValue) != "000")
			{
				if (D.GetString(this.cbo부가세.SelectedValue) == "Y" && Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
				{
					this.ShowMessage("부가세포함여부가 'YES' 일 때에는 원화(KRW)만 선택 할 수 있습니다.");
					this.cbo화폐단위.SelectedValue = "000";
					this._header.CurrentRow["CD_EXCH"] = "000";
				}

				if (MA.기준환율.Option != 기준환율옵션.적용안함)
					this.SetExchageApply();

				if (MA.기준환율.Option != 기준환율옵션.적용_수정불가)
					this.cur환율.Enabled = true;
			}

			if (D.GetString(this.cbo화폐단위.SelectedValue) == "000")
			{
				this.cur환율.DecimalValue = 1;
				this._header.CurrentRow["RT_EXCH"] = 1;
				this.cur환율.Enabled = false;
			}

			this.환율변경();
		}

		private void SetExchageApply()
		{
			decimal num2 = Unit.환율(DataDictionaryTypes.SA, MA.기준환율적용(this.dtp수주일자.Text, D.GetString(this.cbo화폐단위.SelectedValue)));
			this.cur환율.DecimalValue = num2 == 0 ? 1 : num2;
			this._header.CurrentRow["RT_EXCH"] = this.cur환율.DecimalValue;
		}

		private void 환율변경()
		{
			if (!this._flexH.HasNormalRow)
				return;

			this._flexH.Redraw = false;

			try
			{
				for (int idx = this._flexH.Rows.Fixed; idx < this._flexH.Rows.Count; ++idx)
				{
					this._flexH[idx, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AM_SO"]) * this.cur환율.DecimalValue);
					this._flexH[idx, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AM_WONAMT"]) * (D.GetDecimal(this._flexH[idx, "RT_VAT"]) / 100));
					this.Calc부가세포함(idx);

					if (this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
						this._biz.예상이익(this._flexH, idx);

					this._flexH[idx, "CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
				}
			}
			finally
			{
				this._flexH.Redraw = true;
			}
		}

		private void 수주일자변경()
		{
			this._header.CurrentRow["DT_PROCESS"] = this.dtp수주일자.Text;
			this.dtp매출일자.Text = this.dtp수주일자.Text;

			if (D.GetString(this.cbo화폐단위.SelectedValue) == "000" || MA.기준환율.Option == 기준환율옵션.적용안함)
				return;

			decimal DecimalValue = this.cur환율.DecimalValue;
			this.SetExchageApply();

			if (DecimalValue == this.cur환율.DecimalValue)
				return;

			this.환율변경();
		}

		private void VAT구분셋팅()
		{
			if (D.GetString(this.cbo부가세구분.SelectedValue) == string.Empty || this.cbo부가세구분.DataSource == null)
			{
				this.cur부가세율.DecimalValue = 0;
				this._header.CurrentRow["RT_VAT"] = 0;
			}
			else
			{
				DataRow dataRow = ((DataTable)this.cbo부가세구분.DataSource).Rows.Find(this.cbo부가세구분.SelectedValue);
				this.cur부가세율.DecimalValue = D.GetDecimal(dataRow["CD_FLAG1"]);
				this._header.CurrentRow["RT_VAT"] = D.GetDecimal(dataRow["CD_FLAG1"]);
			}
		}

		private string Ctrl()
		{
			string empty = string.Empty;
			return this._반품여부 != "N" ? "16" : "02";
		}

		private void 견적H셋팅(DataRow row견적H, bool 헤더비고)
		{
			this._header.CurrentRow["NO_EST"] = (this.txt통합견적번호.Text = D.GetString(row견적H["NO_EST"]));
			DataRow currentRow1 = this._header.CurrentRow;
			string index1 = "NO_EST_HST";
			decimal num1;
			this.cur통합견적차수.DecimalValue = num1 = D.GetDecimal(row견적H["NO_HST"]);
			decimal local1 = num1;
			currentRow1[index1] = local1;
			this._header.CurrentRow["CD_EXPORT"] = row견적H["CD_EXPORT"];
			this._header.CurrentRow["NM_EXPORT"] = row견적H["NM_EXPORT"];
			this.ctx수출자.SetCode(D.GetString(row견적H["CD_EXPORT"]), D.GetString(row견적H["NM_EXPORT"]));
			this._header.CurrentRow["CD_PRODUCT"] = row견적H["CD_PRODUCT"];
			this._header.CurrentRow["NM_PRODUCT"] = row견적H["NM_PRODUCT"];
			this.ctx제조자.SetCode(D.GetString(row견적H["CD_PRODUCT"]), D.GetString(row견적H["NM_PRODUCT"]));
			this._header.CurrentRow["COND_TRANS"] = this.txt인도조건.Text = D.GetString(row견적H["COND_TRANS"]);
			this._header.CurrentRow["COND_PAY"] = this.cbo결제형태.SelectedValue = D.GetString(row견적H["COND_PAY"]);
			DataRow currentRow2 = this._header.CurrentRow;
			string index2 = "COND_DAYS";
			decimal num2;
			this.cur결제일.DecimalValue = num2 = D.GetDecimal(row견적H["COND_DAYS"]);
			decimal local2 = num2;
			currentRow2[index2] = local2;
			this._header.CurrentRow["TP_PACKING"] = this.cbo포장형태.SelectedValue = D.GetString(row견적H["TP_PACKING"]);
			this._header.CurrentRow["FG_TRANSPORT"] = this.cbo운송방법.SelectedValue = D.GetString(row견적H["TP_TRANS"]);
			this._header.CurrentRow["TP_TRANSPORT"] = this.cbo운송형태.SelectedValue = D.GetString(row견적H["TP_TRANSPORT"]);
			this._header.CurrentRow["NM_INSPECT"] = this.txt검사기관.Text = D.GetString(row견적H["NM_INSPECT"]);
			this._header.CurrentRow["PORT_LOADING"] = this.txt선적항.Text = D.GetString(row견적H["PORT_LOADING"]);
			this._header.CurrentRow["PORT_ARRIVER"] = this.txt도착항.Text = D.GetString(row견적H["PORT_ARRIVER"]);
			this._header.CurrentRow["CD_ORIGIN"] = this.cbo원산지.SelectedValue = D.GetString(row견적H["CD_ORIGIN"]);
			this._header.CurrentRow["DESTINATION"] = this.txt목적지.Text = D.GetString(row견적H["DESTINATION"]);
			this._header.CurrentRow["DT_EXPIRY"] = this.dtp유효일자.Text = D.GetString(row견적H["DT_EXPIRY"]);
			this._header.CurrentRow["COND_PRICE"] = this.cbo가격조건.SelectedValue = D.GetString(row견적H["COND_PRICE"]);

			if (D.GetString(row견적H["DT_DUEDATE"]) != string.Empty)
				this.dtp납기일.Text = D.GetString(row견적H["DT_DUEDATE"]);

			this._header.CurrentRow["FG_BILL"] = this.cbo결재방법.SelectedValue = D.GetString(row견적H["FG_BILL"]);
			this._header.CurrentRow["NO_CONTRACT"] = this.txt계약번호.Text = D.GetString(row견적H["NO_PO"]);
			this.ctx프로젝트.Clear();
			this._header.CurrentRow["CD_NOTIFY"] = this._header.CurrentRow["NM_NOTIFY"] = string.Empty;
			this.ctx착하통지처.Clear();
			this._header.CurrentRow["CD_CONSIGNEE"] = this._header.CurrentRow["NM_CONSIGNEE"] = string.Empty;
			this.ctx수하인.Clear();
			this._header.CurrentRow["DC_RMK_TEXT"] = this.txt멀티비고1.Text = D.GetString(row견적H["DC_RMK_TEXT"]);

			if (!헤더비고)
				return;

			this._header.CurrentRow["DC_RMK"] = D.GetString(row견적H["DC_RMK1"]);
			this.txt비고.Text = D.GetString(row견적H["DC_RMK1"]);
			this._header.CurrentRow["DC_RMK_TEXT"] = D.GetString(row견적H["DC_RMK_TEXT"]);
			this.txt멀티비고1.Text = D.GetString(row견적H["DC_RMK_TEXT"]);
		}

		private void 견적품목셋팅(DataRow[] dr견적D, bool 라인비고)
		{
			if (string.IsNullOrEmpty(this.txt통합견적번호.Text))
				this._flexH.RemoveViewAll();

			int num = 1;

			foreach (DataRow dataRow2 in dr견적D)
			{
				if (dataRow2.RowState != DataRowState.Deleted)
				{
					DataRow row = this._flexH.DataTable.NewRow();

					row["SEQ_SO"] = num++;
					row["GI_PARTNER"] = this.ctx납품처.CodeValue;
					row["LN_PARTNER"] = this.ctx납품처.CodeName;
					row["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["UM_SO"] = row["AM_SO"] = row["AM_VAT"] = row["QT_IM"] = 0));

					if (this.disCount_YN == "Y")
					{
						row["RT_DSCNT"] = 0;
						row["UM_BASE"] = 0;
					}

					row["TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
					row["RT_VAT"] = this.cur부가세율.DecimalValue;
					string CD_CC;
					string NM_CC;
					this.CC조회(D.GetString(dataRow2["CD_ITEMGRP"]), D.GetString(dataRow2["CD_ITEM"]), D.GetString(dataRow2["CD_PLANT"]), out CD_CC, out NM_CC);
					row["CD_CC"] = CD_CC;
					row["NM_CC"] = NM_CC;

					if (D.GetString(dataRow2["DT_DUEDATE"]) != string.Empty)
					{
						row["DT_DUEDATE"] = dataRow2["DT_DUEDATE"];
						row["DT_REQGI"] = dataRow2["DT_DUEDATE"];
					}
					else
					{
						row["DT_DUEDATE"] = this.dtp납기일.Text;
						row["DT_REQGI"] = this.dtp납기일.Text;
					}

					row["DT_REQGI"] = this.dtp납기일.Text;
					row["STA_SO1"] = this._수주상태;
					row["CD_PLANT"] = dataRow2["CD_PLANT"];
					row["CD_ITEM"] = dataRow2["CD_ITEM"];
					row["NM_ITEM"] = dataRow2["NM_ITEM"];
					row["STND_ITEM"] = dataRow2["STND_ITEM"];
					row["UNIT_SO"] = dataRow2["UNIT_SO"];
					row["TP_ITEM"] = dataRow2["TP_ITEM"];
					row["TP_VAT"] = dataRow2["TP_VAT"];

					if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000030") == "000")
					{
						row["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dataRow2["QT_EST"]));
					}
					else
					{
						row["QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dataRow2["SO_QT_EST"]));
						row["NO_EST"] = dataRow2["NO_EST"];
						row["NO_EST_HST"] = dataRow2["NO_HST"];
						row["SEQ_EST"] = dataRow2["SEQ_EST"];
					}

					row["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow2["UM_EST"]));
					row["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow2["AM_EST"]));
					row["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow2["AM_EST"]) * this.cur환율.DecimalValue);
					row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) * (this.cur부가세율.DecimalValue / 100));
					row["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_WONAMT"]) + D.GetDecimal(row["AM_VAT"]));
					row["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(row["AMVAT_SO"]) / D.GetDecimal(row["QT_SO"]));
					row["UNIT_SO_FACT"] = D.GetDecimal(dataRow2["UNIT_SO_FACT"]) == 0 ? 1 : dataRow2["UNIT_SO_FACT"];
					row["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(row["QT_SO"]) * D.GetDecimal(row["UNIT_SO_FACT"]));
					row["UNIT_IM"] = dataRow2["UNIT_IM"];
					row["EN_ITEM"] = dataRow2["EN_ITEM"];
					row["STND_DETAIL_ITEM"] = dataRow2["STND_DETAIL_ITEM"];
					row["GRP_MFG"] = dataRow2["GRP_MFG"];
					row["LT_GI"] = D.GetDecimal(dataRow2["LT_GI"]);
					row["WEIGHT"] = dataRow2["WEIGHT"];
					row["UNIT_WEIGHT"] = dataRow2["UNIT_WEIGHT"];
					row["FG_SERNO"] = dataRow2["FG_SERNO"];
					row["YN_ATP"] = dataRow2["YN_ATP"];
					row["CUR_ATP_DAY"] = dataRow2["CUR_ATP_DAY"];
					row["FG_MODEL"] = dataRow2["FG_MODEL"];
					row["UNIT_SO_FACT"] = (D.GetDecimal(dataRow2["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(dataRow2["UNIT_SO_FACT"]));
					row["UNIT_GI_FACT"] = (D.GetDecimal(dataRow2["UNIT_GI_FACT"]) == 0 ? 1 : D.GetDecimal(dataRow2["UNIT_GI_FACT"]));

					row["CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
					row["TP_IV"] = this._매출형태;
					row["MAT_ITEM"] = dataRow2["MAT_ITEM"];

					if (BASIC.GetMAEXC("배차사용유무") == "Y")
						row["YN_PICKING"] = this._배송여부;

					this._flexH.DataTable.Rows.Add(row);
				}
			}

			this._flexH.SumRefresh();
			this._flexH.Row = this._flexH.Rows.Count - 1;
			this._flexH.IsDataChanged = true;
			this.ToolBarSaveButtonEnabled = true;
			this.Page_DataChanged(null, null);
		}

		private void 품목추가(int idx, DataRow[] dr품목, bool 품목전개)
		{
			bool flag1 = true;
			string str1 = string.Empty;
			string empty1 = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("CD_ITEM".PadRight(25, ' ') + "NM_ITEM");
			stringBuilder.AppendLine("-".PadRight(50, '-'));

			if (this._flexH.Rows.Count == 3 && D.GetString(this._flexH[idx, "DT_DUEDATE"]) == string.Empty)
				str1 = this.dtp납기일.Text;
			else if (this._flexH.Rows.Count >= 3 && D.GetString(this._flexH[idx, "DT_DUEDATE"]) != string.Empty)
				str1 = D.GetString(this._flexH[idx, "DT_DUEDATE"]);

			string str2 = this.dtp수주일자.Text;

			if (D.GetString(this._flexH[idx, "CD_PLANT"]) != string.Empty)
				empty1 = D.GetString(this._flexH[idx, "CD_PLANT"]);

			DataTable dataTable1 = null;
			DataTable dataTable2 = null;

			if (this.disCount_YN == "Y")
			{
				if (this._biz.Get할인율적용 == 수주관리.할인율적용.거래처그룹별_품목군할인율)
					dataTable1 = this._biz.할인율(empty1, this.ctx거래처.CodeValue, dr품목);
				else if (this._biz.Get할인율적용 == 수주관리.할인율적용.한국화장품)
					dataTable1 = new 한국화장품().할인율(dr품목, empty1, this.ctx거래처.CodeValue, str2, D.GetString(this.cbo단가유형.SelectedValue), this.ctx수주형태.CodeValue, this.ctx창고적용.CodeValue);
			}

			DataTable dataTable3 = null;
			if (empty1 != string.Empty && this._biz.Get예상이익산출적용 == 수주관리.예상이익산출.재고단가를원가로산출)
				dataTable3 = this._biz.예상이익(empty1, str2, dr품목);

			string str3 = string.Empty;
			string empty3 = string.Empty;

			if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
			{
				str3 = D.GetString(BASIC.GetPartner(this.ctx거래처.CodeValue)["CD_SL_ISU"]);

				if (str3 != string.Empty)
				{
					DataRow sl = BASIC.GetSL(empty1, str3);
					if (sl == null)
						str3 = empty3 = string.Empty;
					else
						empty3 = D.GetString(sl["NM_SL"]);
				}
			}

			this._flexH.Redraw = false;
			this._flexH.SetDummyColumnAll();
			수주관리.Calc calc = new 수주관리.Calc();
			string str4 = Duzon.ERPU.MF.Common.Common.MultiString(dr품목, "CD_ITEM", "|");
			DataTable dataTable4 = BASIC.GetQtInvMulti(str4, this.dtp수주일자.Text);

			dataTable4.PrimaryKey = new DataColumn[] { dataTable4.Columns["CD_PLANT"],
													   dataTable4.Columns["CD_SL"],
													   dataTable4.Columns["CD_ITEM"] };
			DataTable dataTable6 = null;

			if (this._biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
			{
				dataTable6 = this._biz.SearchUmFixed(this.ctx거래처.CodeValue, empty1, str4);
				dataTable6.PrimaryKey = new DataColumn[] { dataTable6.Columns["CD_ITEM"] };
			}

			foreach (DataRow dataRow1 in dr품목)
			{
				if (dataRow1.RowState != DataRowState.Deleted)
				{
					string CD_CC;
					string NM_CC;

					this._flexH[idx, "CD_PLANT"] = empty1;
					this._flexH[idx, "CD_ITEM"] = dataRow1["CD_ITEM"];
					this._flexH[idx, "NM_ITEM"] = dataRow1["NM_ITEM"];
					this._flexH[idx, "EN_ITEM"] = dataRow1["EN_ITEM"];
					this._flexH[idx, "STND_ITEM"] = dataRow1["STND_ITEM"];
					this._flexH[idx, "STND_DETAIL_ITEM"] = dataRow1["STND_DETAIL_ITEM"];
					this._flexH[idx, "UNIT_SO"] = dataRow1["UNIT_SO"];
					this._flexH[idx, "UNIT_IM"] = dataRow1["UNIT_IM"];
					this._flexH[idx, "TP_ITEM"] = dataRow1["TP_ITEM"];
					this._flexH[idx, "GRP_MFG"] = dataRow1["GRP_MFG"];
					this._flexH[idx, "NM_GRP_MFG"] = dataRow1["NM_GRP_MFG"];
					this._flexH[idx, "LT_GI"] = D.GetDecimal(dataRow1["LT_GI"]);
					this._flexH[idx, "GI_PARTNER"] = this.ctx납품처.CodeValue;
					this._flexH[idx, "LN_PARTNER"] = this.ctx납품처.CodeName;
					this._flexH[idx, "WEIGHT"] = dataRow1["WEIGHT"];
					this._flexH[idx, "UNIT_WEIGHT"] = dataRow1["UNIT_WEIGHT"];
					this._flexH[idx, "NO_PO_PARTNER"] = this.txt거래처PO.Text;
					this._flexH[idx, "FG_SERNO"] = dataRow1["FG_SERNO"];
					this._flexH[idx, "YN_ATP"] = dataRow1["YN_ATP"];
					this._flexH[idx, "CUR_ATP_DAY"] = dataRow1["CUR_ATP_DAY"];
					this._flexH[idx, "FG_MODEL"] = dataRow1["FG_MODEL"];
					this._flexH[idx, "NM_MANAGER1"] = dataRow1["NM_MANAGER1"];
					this._flexH[idx, "UNIT_SO_FACT"] = (D.GetDecimal(dataRow1["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(dataRow1["UNIT_SO_FACT"]));
					this._flexH[idx, "UNIT_GI_FACT"] = (D.GetDecimal(dataRow1["UNIT_GI_FACT"]) == 0 ? 1 : D.GetDecimal(dataRow1["UNIT_GI_FACT"]));
					this._flexH[idx, "TP_IV"] = this._매출형태;
					this._flexH[idx, "MAT_ITEM"] = dataRow1["MAT_ITEM"];
					this._flexH[idx, "CLS_ITEM"] = dataRow1["CLS_ITEM"];
					this._flexH[idx, "GRP_ITEM"] = dataRow1["GRP_ITEM"];
					this._flexH[idx, "GRP_ITEMNM"] = dataRow1["GRP_ITEMNM"];
					this._flexH[idx, "GRP_MFG"] = dataRow1["GRP_MFG"];
					this._flexH[idx, "NM_GRP_MFG"] = dataRow1["NM_GRP_MFG"];
					this._flexH[idx, "CLS_L"] = dataRow1["CLS_L"];
					this._flexH[idx, "CLS_S"] = dataRow1["CLS_S"];
					this._flexH[idx, "NO_MODEL"] = dataRow1["NO_MODEL"];

					this.CC조회(D.GetString(dataRow1["GRP_ITEM"]), D.GetString(this._flexH[idx, "CD_ITEM"]), D.GetString(this._flexH[idx, "CD_PLANT"]), out CD_CC, out NM_CC);
					this._flexH[idx, "CD_CC"] = CD_CC;
					this._flexH[idx, "NM_CC"] = NM_CC;

					if (ConfigSA.SA_EXC.거래처선택_출고창고적용)
					{
						this._flexH[idx, "CD_SL"] = str3;
						this._flexH[idx, "NM_SL"] = empty3;
					}
					else
					{
						this._flexH[idx, "CD_SL"] = dataRow1["CD_GISL"];
						this._flexH[idx, "NM_SL"] = dataRow1["NM_GISL"];
					}

					if (D.GetString(this._flexH[idx, "CD_SL"]) == string.Empty)
					{
						this._flexH[idx, "CD_SL"] = this.ctx창고적용.CodeValue;
						this._flexH[idx, "NM_SL"] = this.ctx창고적용.CodeName;
					}

					DataRow dataRow2 = dataTable4.Rows.Find(new object[] { D.GetString(this._flexH[idx, "CD_PLANT"]),
																			   D.GetString(this._flexH[idx, "CD_SL"]),
																			   D.GetString(this._flexH[idx, "CD_ITEM"]) });

					if (dataRow2 == null)
						this._flexH[idx, "SL_QT_INV"] = 0;
					else
						this._flexH[idx, "SL_QT_INV"] = D.GetDecimal(dataRow2["QT_INV"]);

					if (!this.ctx프로젝트.IsEmpty())
					{
						this._flexH[idx, "NO_PROJECT"] = this.ctx프로젝트.CodeValue;
						this._flexH[idx, "NM_PROJECT"] = this.ctx프로젝트.CodeName;
					}

					if (this._biz.WH적용 == "100")
					{
						this._flexH[idx, "CD_WH"] = dataRow1["CD_WH"];
						this._flexH[idx, "NM_WH"] = dataRow1["NM_WH"];
					}

					string str5 = D.GetString(dataRow1["FG_TAX_SA"]);

					if (this._biz.Get과세변경유무 == "N" || str5 == string.Empty)
					{
						this._flexH[idx, "TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
						this._flexH[idx, "RT_VAT"] = this.cur부가세율.DecimalValue;
					}
					else
					{
						this._flexH[idx, "TP_VAT"] = str5;
						this._flexH[idx, "RT_VAT"] = D.GetDecimal(dataRow1["RT_TAX_SA"]);

						if (D.GetString(this.cbo부가세구분.SelectedValue) != D.GetString(this._flexH[idx, "TP_VAT"]))
							flag1 = false;
					}

					this._flexH[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "QT_SO"]) * (D.GetDecimal(dataRow1["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(dataRow1["UNIT_SO_FACT"])));

					if (str1 != string.Empty)
						this._flexH[idx, "DT_REQGI"] = calc.출하예정일조회(str1, D.GetInt(this._flexH["LT_GI"]));

					if (this._구분 == "복사")
						this._flexH[idx, "STA_SO1"] = this._수주상태;

					if (dataTable1 != null)
					{
						DataRow dataRow3 = dataTable1.Rows.Find(dataRow1["CD_ITEM"]);
						this._flexH[idx, "RT_DSCNT"] = dataRow3 == null ? 0 : dataRow3["DC_RATE"];
					}

					if (dr품목[0].Table.Columns.Contains("CD_CITEM"))
					{
						this._flexH[idx, "QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dataRow1["QT_CALC"]));
						this._flexH[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, calc.관리수량(D.GetDecimal(this._flexH["QT_SO"]), D.GetDecimal(this._flexH["UNIT_SO_FACT"])));
						this._flexH[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow1["UM_SET"]));
						this._flexH[idx, "UM_SO_ORI"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow1["UM_SET"]));
					}
					else
					{
						int num2;
						if (품목전개)
							num2 = 1;
						else
							num2 = 1;

						if (num2 == 0)
						{
							this._flexH[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow1["UM_ITEM"]));
							break;
						}

						if (this._biz.Get특수단가적용 == 특수단가적용.NONE)
						{
							this.일반단가적용(idx, str2);
						}
						else if (this._biz.Get특수단가적용 == 특수단가적용.중량단가)
						{
							if (this._단가적용형태 == "002" || this._단가적용형태 == "003")
								this._flexH[idx, "UM_OPT"] = Unit.외화단가(DataDictionaryTypes.SA, BASIC.GetUM(D.GetString(this._flexH[idx, "CD_PLANT"]), D.GetString(this._flexH[idx, "CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA"));
							this.특수단가사용시단가계산(idx);
						}
						else if (this._biz.Get특수단가적용 == 특수단가적용.거래처별고정단가)
						{
							DataRow dataRow3 = dataTable6.Rows.Find(this._flexH[idx, "CD_ITEM"]);
							if (dataRow3 == null)
								this.일반단가적용(idx, str2);
							else
								this._flexH[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow3["UM_FIXED"]));
						}
					}

					if (this.Use부가세포함)
					{
						this._flexH[idx, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "QT_SO"]) * D.GetDecimal(this._flexH[idx, "UMVAT_SO"]));

						if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
							this._flexH[idx, "AM_WONAMT"] = decimal.Round(D.GetDecimal(this._flexH[idx, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(this._flexH[idx, "RT_VAT"]))), MidpointRounding.AwayFromZero);
						else
							this._flexH[idx, "AM_WONAMT"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, D.GetDecimal(this._flexH[idx, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(this._flexH[idx, "RT_VAT"])))));

						this._flexH[idx, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AMVAT_SO"]) - D.GetDecimal(this._flexH[idx, "AM_WONAMT"]));
						this._flexH[idx, "AM_SO"] = (this.cur환율.DecimalValue == 0 ? 0 : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AM_WONAMT"]) / this.cur환율.DecimalValue));
						this._flexH[idx, "UM_SO"] = (D.GetDecimal(this._flexH[idx, "QT_SO"]) == 0 ? 0 : Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AM_SO"]) / D.GetDecimal(this._flexH[idx, "QT_SO"])));
					}
					else
					{
						this.Calc금액변경(idx);
						this.Calc부가세포함(idx);
					}

					if (dataTable3 != null && dataTable3.Rows.Count > 0)
					{
						DataRow dataRow3 = dataTable3.Rows.Find(dataRow1["CD_ITEM"]);
						this._flexH[idx, "UM_INV"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow3 == null ? 0 : dataRow3["UM_INV"]));
						this._biz.예상이익(this._flexH, idx);
					}

					if (BASIC.GetMAEXC("SET품 사용유무") == "Y" && dr품목[0].Table.Columns.Contains("CD_ITEM"))
					{
						this._flexH[idx, "CD_ITEM_REF"] = dataRow1["CD_ITEM"];
						this._flexH[idx, "NM_ITEM_REF"] = dataRow1["NM_ITEM"];
						this._flexH[idx, "STND_ITEM_REF"] = dataRow1["STND_ITEM_SET"];
					}

					if (BASIC.GetMAEXC("배차사용유무") == "Y")
						this._flexH[idx, "YN_PICKING"] = this._배송여부;

					if (dataTable2 != null)
					{
						DataRow dataRow3 = dataTable2.Rows.Find(dataRow1["CD_ITEM"]);
						this._flexH[idx, "UM_BASE"] = dataRow3 == null ? 0 : dataRow3["UM_BASE"];
						this._flexH[idx, "NUM_USERDEF1"] = dataRow3 == null ? 0 : dataRow3["NUM_USERDEF1"];
						this._flexH[idx, "RT_DSCNT"] = dataRow3 == null ? 0 : dataRow3["RT_DSCNT"];
						this._flexH[idx, "NUM_USERDEF5"] = dataRow3 == null ? 0 : dataRow3["RT_DSCNT"];
						this._flexH[idx, "NUM_USERDEF2"] = dataRow3 == null ? 0 : dataRow3["NUM_USERDEF2"];
						this._flexH[idx, "UM_SO"] = dataRow3 == null ? 0 : dataRow3["NUM_USERDEF2"];
						this._flexH[idx, "NUM_USERDEF6"] = (D.GetDecimal(this._flexH[idx, "NUM_USERDEF2"]) - D.GetDecimal(this._flexH[idx, "NUM_USERDEF4"]));
					}

					this.소요자재그리드CLEAR();
				}
			}

			if (!flag1)
			{
				this.ShowMessage("VAT구분값은 공장품목에 등록된 과세구분(매출)값으로 세팅되었습니다.");
			}

			this._flexH.RemoveDummyColumnAll();
			this._flexH.AddFinished();
			this._flexH.Col = this._flexH.Cols.Fixed;
			this._flexH.Redraw = true;
		}

		private bool 일반추가()
		{
			DataRow[] dataRowArray = this._flexH.DataTable.Select("ISNULL(NO_IO_MGMT, '') = ''", string.Empty, DataViewRowState.CurrentRows);
			return dataRowArray != null && dataRowArray.Length > 0;
		}

		private bool 출하적용건()
		{
			DataRow[] dataRowArray = this._flexH.DataTable.Select("ISNULL(NO_IO_MGMT, '') <> ''", string.Empty, DataViewRowState.CurrentRows);
			return dataRowArray != null && dataRowArray.Length > 0;
		}

		private bool 견적적용건()
		{
			return this.txt통합견적번호.Text != string.Empty;
		}

		private bool Chk적용건(bool ismsg)
		{
			return this.Chk견적적용(ismsg) && this.Chk출하적용(ismsg) && this.Chk사전프로젝트적용(ismsg);
		}

		private bool Chk견적적용(bool ismsg)
		{
			if (this.txt통합견적번호.Text == string.Empty)
				return true;

			if (ismsg)
			{
				this.ShowMessage("견적적용건이 존재합니다.");
			}
			return false;
		}

		private bool Chk출하적용(bool ismsg)
		{
			DataRow[] dataRowArray = this._flexH.DataTable.Select("ISNULL(NO_IO_MGMT, '') <> ''", string.Empty, DataViewRowState.CurrentRows);

			if (dataRowArray == null || dataRowArray.Length <= 0)
				return true;

			if (ismsg)
			{
				this.ShowMessage("출하적용건이 존재합니다.");
			}

			return false;
		}

		private bool Chk사전프로젝트적용(bool ismsg)
		{
			if (!(D.GetString(this._header.CurrentRow["FG_TRACK"]) == "I"))
				return true;

			if (ismsg)
			{
				this.ShowMessage("BASE(간접)사전프로젝트로 인해 등록된 수주데이터는 수정 삭제가 불가능합니다.");
			}

			return false;
		}

		private bool Chk확정여부()
		{
			if (!this.추가모드여부)
			{
				if (D.GetString(this._flexH[this._flexH.Row, "STA_SO1"]) != string.Empty && D.GetString(this._flexH[this._flexH.Row, "STA_SO1"]) != "O")
				{
					this.ShowMessage(메세지.이미수주확정되어수정삭제가불가능합니다);
					return false;
				}
			}

			return true;
		}

		private void 일반단가적용(int idx, string 기준일자)
		{
			if (this._단가적용형태 != "002" && this._단가적용형태 != "003")
				return;

			decimal num = BASIC.GetUM(D.GetString(this._flexH[idx, "CD_PLANT"]), D.GetString(this._flexH[idx, "CD_ITEM"]), this.ctx거래처.CodeValue, this.ctx영업그룹.CodeValue, this.dtp수주일자.Text, D.GetString(this.cbo단가유형.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue), "SA");

			if (this.Use부가세포함)
			{
				this._flexH[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, num);
			}
			else
			{
				if (this.disCount_YN == "N")
				{
					this._flexH[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, num);
					this._flexH[idx, "UM_SO_ORI"] = Unit.외화단가(DataDictionaryTypes.SA, num);
				}
				else if (this.disCount_YN == "Y")
				{
					this._flexH[idx, "UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, num);
					this._flexH[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "UM_BASE"]) - D.GetDecimal(this._flexH[idx, "UM_BASE"]) * D.GetDecimal(this._flexH[idx, "RT_DSCNT"]) / 100);
					this._flexH[idx, "UM_SO_ORI"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "UM_BASE"]) - D.GetDecimal(this._flexH[idx, "UM_BASE"]) * D.GetDecimal(this._flexH[idx, "RT_DSCNT"]) / 100);
					this._flexH[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "UM_BASE"]) - D.GetDecimal(this._flexH[idx, "UM_BASE"]) * D.GetDecimal(this._flexH[idx, "RT_DSCNT"]) / 100);
				}
			}

			if (BASIC.GetMAEXC_Menu("P_SA_SO", "SA_A00000001") == "001")
				this._flexH[idx, "NUM_USERDEF1"] = D.GetDecimal(this._flexH[idx, "UM_SO"]);
		}

		private void 특수단가사용시단가계산(int idx)
		{
			if (this._biz.Get특수단가적용 != 특수단가적용.중량단가)
				return;

			this.중량단가계산(idx);
		}

		private void 중량단가계산(int idx)
		{
			decimal num = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "WEIGHT"]) * D.GetDecimal(this._flexH[idx, "UM_OPT"]));

			if (this.disCount_YN == "Y")
			{
				this._flexH[idx, "UM_BASE"] = num;
				this._flexH[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, num - num * D.GetDecimal(this._flexH[idx, "RT_DSCNT"]) / 100);
			}
			else
				this._flexH[idx, "UM_SO"] = num;
		}

		private void 특수단가사용시단가계산(ref DataRow dr)
		{
			if (this._biz.Get특수단가적용 != 특수단가적용.중량단가)
				return;

			this.중량단가계산(ref dr);
		}

		private void 중량단가계산(ref DataRow dr)
		{
			decimal num = D.GetDecimal(dr["WEIGHT"]) * D.GetDecimal(dr["UM_OPT"]);
			dr["UM_SO"] = num;

			if (!(this.disCount_YN == "Y"))
				return;

			dr["UM_BASE"] = num;
		}

		private void 소요자재그리드CLEAR()
		{
			if (!this._flexL.HasNormalRow)
				return;

			try
			{
				this._flexL.Redraw = false;

				for (int index = this._flexL.Rows.Count - (this._flexL.Rows.Fixed - 1); index >= this._flexL.Rows.Fixed; --index)
					this._flexL.Rows.Remove(index);
			}
			finally
			{
				this._flexL.Redraw = true;
			}
		}

		private void 버튼Enabled(bool check)
		{
			this.ctx수주형태.Enabled = check;
			this.cbo화폐단위.Enabled = check;
			this.cur환율.Enabled = check;
			this.cbo부가세구분.Enabled = check;

			if (this.수주Config.부가세포함단가사용())
				this.cbo부가세.Enabled = check;

			this.ctx프로젝트.Enabled = check;
			this.ctx거래처.Enabled = check;
		}

		private void Authority(bool check)
		{
			this.ctx거래처.Enabled = check;
			this.ctx영업그룹.Enabled = check;
			this.ctx수주형태.Enabled = check;
			this.ctx프로젝트.Enabled = check;
			this.btn프로젝트적용.Enabled = check;
		}

		private void 사용자정의셋팅()
		{
			//this.ColsSetting("NUM_USERDEF", "SA_B000069", 1, 6);
			this._flexH.Cols["NUM_USERDEF3"].Visible = true;

			//this.ColsSetting("TXT_USERDEF", "SA_B000112", 1, 11);
			this._flexH.Cols["TXT_USERDEF3"].Visible = true;
			this._flexH.Cols["TXT_USERDEF4"].Visible = true;
			this._flexH.Cols["TXT_USERDEF5"].Visible = true;
			this._flexH.Cols["TXT_USERDEF6"].Visible = true;

			//this.ColsSetting("CD_USERDEF", "SA_B000124", 1, 2);
			this._flexH.Cols["CD_USERDEF1"].Visible = true;
			this._flexH.Cols["CD_USERDEF2"].Visible = true;
			this._flexH.Cols["CD_USERDEF3"].Visible = true;

			DataTable code = MA.GetCode("SA_B000110");
			DataRow[] dataRowArray1 = code.Select("CD_FLAG1 = 'DATE'");

			for (int index = 1; index <= dataRowArray1.Length; ++index)
			{
				string str = D.GetString(dataRowArray1[index - 1]["NAME"]);

				switch (index)
				{
					case 1:
						this.lbl날짜사용자1.Text = str;
						this.lbl날짜사용자1.Visible = this.dtp날짜사용자1.Visible = true;
						break;
					case 2:
						this.lbl날짜사용자2.Text = str;
						this.lbl날짜사용자2.Visible = this.dtp날짜사용자2.Visible = true;
						break;
				}
			}

			DataRow[] dataRowArray2 = code.Select("CD_FLAG1 = 'TEXT'");

			for (int index = 1; index <= dataRowArray2.Length; ++index)
			{
				string str = D.GetString(dataRowArray2[index - 1]["NAME"]);
				switch (index)
				{
					case 3:
						this.lbl텍스트사용자3.Text = str;
						this.lbl텍스트사용자3.Visible = this.txt텍스트사용자3.Visible = true;
						break;
				}
			}

			DataRow[] dataRowArray4 = code.Select("CD_FLAG1 = 'NUMBER'");
			for (int index = 1; index <= dataRowArray4.Length; ++index)
			{
				string str = D.GetString(dataRowArray4[index - 1]["NAME"]);

				switch (index)
				{
					case 4:
						this.lbl숫자사용자4.Text = str;
						this.lbl숫자사용자4.Visible = this.cur숫자사용자4.Visible = true;
						break;
					case 5:
						this.lbl숫자사용자5.Text = str;
						this.lbl숫자사용자5.Visible = this.cur숫자사용자5.Visible = true;
						break;
				}
			}

			if (code != null && code.Rows.Count != 0)
				return;

			this.tabControl.TabPages.Remove(this.tpg기타);
		}

		private void ColsSetting(string colName, string cdField, int startIdx, int endIdx)
		{
			for (int index = startIdx; index <= endIdx; ++index)
				this._flexH.Cols[colName + D.GetString(index)].Visible = false;

			DataTable code = MA.GetCode(cdField);

			for (int index = startIdx; index <= code.Rows.Count && index <= endIdx; ++index)
			{
				string str = D.GetString(code.Rows[index - 1]["NAME"]);
				this._flexH.Cols[colName + D.GetString(index)].Caption = str;
				this._flexH.Cols[colName + D.GetString(index)].Visible = true;
			}
		}

		private bool ATP체크로직(bool 자동체크)
		{
			if (this._flexH.DataTable.DefaultView.ToTable(true, "CD_PLANT").Rows.Count > 1)
			{
				this.ShowMessage("두개 이상의 공장이 지정되어 ATP체크가 불가합니다.");
				return false;
			}

			ATP atp = new ATP();

			if (atp.ATP환경설정_사용유무(this.LoginInfo.BizAreaCode, D.GetString(this._flexH["CD_PLANT"])) == "N")
				return true;

			string str = atp.ATP자동체크_저장로직(D.GetString(this._flexH["CD_PLANT"]), "100");

			if (str != "000" && str != "001")
				return true;

			DataRow[] dataRowArray = this._flexH.DataTable.Select("YN_ATP = 'Y'", string.Empty, DataViewRowState.CurrentRows);

			if (dataRowArray.Length == 0)
				return true;

			if (dataRowArray.Length != this._flexH.DataTable.DefaultView.ToTable(true, new string[] { "CD_ITEM", "YN_ATP" }).Select("YN_ATP = 'Y'").Length &&
				this.ShowMessage("동일품목이 존재 할 경우 정확한 ATP체크를 할 수 없습니다." + Environment.NewLine + "계속 진행하시겠습니까?", "QY2") != DialogResult.Yes)
				return false;

			string empty = string.Empty;
			atp.Set메뉴ID = this.PageID;
			atp.Set전표번호 = this.txt수주번호.Text;

			if (atp.ATP_Check(dataRowArray, out empty))
				return true;

			if (!자동체크)
			{
				this.ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", empty);
				return false;
			}

			if (str == "000")
				return this.ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요." + Environment.NewLine + "그래도 저장하시겠습니까?", string.Empty, empty, "QY2") == DialogResult.Yes;

			if (str != "001")
				return true;

			this.ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", empty);

			return false;
		}

		private bool IsAgingCheck()
		{
			DataTable dt;
			new 채권연령관리().채권연령체크(this._header.CurrentRow.Table.Select(), (AgingCheckPoint)0, out dt);

			if (dt == null || dt.Rows.Count == 0)
				return true;

			if (new P_SA_CUST_CREDIT_CHECK_SUB(dt).ShowDialog() != DialogResult.OK)
				return false;

			return true;
		}

		private void 창고별현재고인쇄(string cd_item_multi)
		{
			if (this.추가모드여부)
				return;

			ReportHelper reportHelper = new ReportHelper("R_SA_SO_1", "창고별현재고인쇄");
			reportHelper.SetData("NO_SO", this.txt수주번호.Text);
			reportHelper.SetData("DT_SO", this.dtp수주일자.Text);

			DataTable dataTable = this._biz.창고별현재고조회(this.txt수주번호.Text, cd_item_multi);

			reportHelper.SetDataTable(dataTable);
			reportHelper.Print();
		}

		private bool ChkBizarea()
		{
			if (!this.IsChanged())
				return true;

			DataTable table = this._flexH.DataTable.DefaultView.ToTable(true, "CD_PLANT");

			if (table.Rows.Count == 1)
				return true;

			string multiCdPlant = string.Empty;

			foreach (DataRow row in table.Rows)
				multiCdPlant = multiCdPlant + D.GetString(row["CD_PLANT"]) + "|";

			if (this._biz.SearchBizarea(multiCdPlant) <= 1)
				return true;

			this.ShowMessage("공장의 사업장이 동일하지 않으면 저장 할 수 없습니다.");

			return false;
		}

		private bool VerifyNoSo()
		{
			DataTable dataTable = this._biz.SearchNoSo(this.txt수주번호.Text);

			if (dataTable == null || dataTable.Rows.Count == 0)
				return true;

			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(" - NO_SO : " + D.GetString(dataTable.Rows[0]["NO_SO"]));
			stringBuilder.AppendLine(" - DT_SO : " + D.GetDecimal(dataTable.Rows[0]["DT_SO"]).ToString("####/##/##"));
			this.ShowDetailMessage("해당 수주번호는 이미 등록된 건 입니다.", stringBuilder.ToString());

			return false;
		}

		private void CC조회(string 품목군, string 품목, string 공장, out string CD_CC, out string NM_CC)
		{
			string codeValue1 = this.ctx수주형태.CodeValue;
			string codeValue2 = this.ctx영업그룹.CodeValue;
			CD_CC = string.Empty;
			NM_CC = string.Empty;

			switch (this.수주Config.수주라인CC설정유형() - 1)
			{
				case 수주관리.수주라인CC설정.NONE:
					this.수주Config.수주라인CC설정_영업그룹(codeValue2, out CD_CC, out NM_CC);
					break;
				case 수주관리.수주라인CC설정.영업그룹:
					this.수주Config.수주라인CC설정_수주유형(codeValue1, out CD_CC, out NM_CC);

					if (CD_CC != string.Empty)
						break;

					this.수주Config.수주라인CC설정_영업그룹(codeValue2, out CD_CC, out NM_CC);
					break;
				case 수주관리.수주라인CC설정.수주유형:
					this.수주Config.수주라인CC설정_품목군(품목군, out CD_CC, out NM_CC);

					if (CD_CC != string.Empty)
						break;

					this.수주Config.수주라인CC설정_영업그룹(codeValue2, out CD_CC, out NM_CC);
					break;
				case 수주관리.수주라인CC설정.프로젝트라인:
					this.수주Config.수주라인CC설정_품목(품목, 공장, out CD_CC, out NM_CC);

					if (CD_CC != string.Empty)
						break;

					this.수주Config.수주라인CC설정_영업그룹(codeValue2, out CD_CC, out NM_CC);
					break;
			}
		}

		private void Calc금액변경(int idx)
		{
			this._flexH[idx, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "QT_SO"]) * D.GetDecimal(this._flexH[idx, "UM_SO"]));
			this._flexH[idx, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AM_SO"]) * this.cur환율.DecimalValue);
			this._flexH[idx, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AM_WONAMT"]) * (D.GetDecimal(this._flexH[idx, "RT_VAT"]) / 100));
		}

		private void Calc부가세포함(int idx)
		{
			this._flexH[idx, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AM_WONAMT"]) + D.GetDecimal(this._flexH[idx, "AM_VAT"]));

			if (D.GetDecimal(this._flexH[idx, "QT_SO"]) != 0)
				this._flexH[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AMVAT_SO"]) / D.GetDecimal(this._flexH[idx, "QT_SO"]));
			else
				this._flexH[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "AMVAT_SO"]));
		}

		private bool Chk결제조건()
		{
			return new Duzon.ERPU.SA.Common.SA.Check().결제조건별수금체크(this.ctx거래처.CodeValue, this.dtp수주일자.Text);
		}

		private void SearchSo(string noSo)
		{
			DataSet dataSet = this._biz.Search(noSo);

			string query = @"SELECT 0 AS CODE,
							 	    '' AS NAME
							 UNION ALL
							 SELECT FP.SEQ AS CODE,
							 	    FP.NM_PTR AS NAME
							 FROM FI_PARTNERPTR FP WITH(NOLOCK)
							 WHERE FP.CD_COMPANY = '{0}'
						     AND FP.CD_PARTNER = '{1}'";

			DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dataSet.Tables[0].Rows[0]["CD_PARTNER"].ToString()));

			this.cbo설계담당자.DataSource = dt.Copy();
			this.cbo설계담당자.ValueMember = "CODE";
			this.cbo설계담당자.DisplayMember = "NAME";

			this.cbo구매담당자.DataSource = dt.Copy();
			this.cbo구매담당자.ValueMember = "CODE";
			this.cbo구매담당자.DisplayMember = "NAME";

			dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dataSet.Tables[0].Rows[0]["NO_NEGO"].ToString()));

			this.cbo인수자.DataSource = dt;
			this.cbo인수자.ValueMember = "CODE";
			this.cbo인수자.DisplayMember = "NAME";

			this._header.SetDataTable(dataSet.Tables[0]);

			if (this._구분 == "복사")
			{
				this._헤더수정여부 = true;
				this._flexH.DataTable.Rows.Clear();

				if (!this._헤더만복사)
				{
					try
					{
						this._flexH.Redraw = false;

						foreach (DataRow row1 in dataSet.Tables[1].Rows)
						{
							DataRow row2 = this._flexH.DataTable.NewRow();
							Duzon.ERPU.SA.Settng.Data.DataCopy(row1, row2);
							this._flexH.DataTable.Rows.Add(row2);
						}
					}
					finally
					{
						this._flexH.Redraw = true;
					}

					this._flexH.SumRefresh();
					this._flexH.Row = this._flexH.Rows.Count - 1;
					this._flexH.Col = this._flexH.Cols.Fixed;
					this._flexH.Focus();
					this._flexH.IsDataChanged = true;
				}

				this._header.JobMode = JobModeEnum.추가후수정;
				this.ToolBarDeleteButtonEnabled = false;
				this.ToolBarSaveButtonEnabled = this.btn삭제.Enabled = true;

				if (this._biz.수주반품사용여부)
				{
					this.btn출하적용.Enabled = true;

					if (!this._flexH.Cols["NO_IO_MGMT"].Visible)
					{
						Column column = this._flexH.Cols["NO_IO_MGMT"];
						bool flag;
						this._flexH.Cols["NO_IOLINE_MGMT"].Visible = flag = true;
						int num = flag ? 1 : 0;
						column.Visible = num != 0;
					}
				}
			}
			else
			{
				this._flexH.Binding = dataSet.Tables[1];
				this.btn추가.Enabled = true;
			}

			DataRow tpso = BASIC.GetTPSO(D.GetString(dataSet.Tables[0].Rows[0]["TP_SO"]));
			this._매출자동여부 = D.GetString(tpso["IV_AUTO"]);
			this._자동승인여부 = D.GetString(tpso["CONF"]);
			this.IsChkTab5Activate();
			this.IsInvAmCalc();
			this.so_Price = D.GetString(BASIC.GetSaleGrp(D.GetString(this._header.CurrentRow["CD_SALEGRP"]))["SO_PRICE"]);

			if (!(D.GetDecimal(dataSet.Tables[0].Rows[0]["NO_HST"]) > 0))
				return;

			this.ShowMessage("수주이력이 존재합니다.수정하실 수 없습니다.");

			this.tabControl.Enabled = this.btn견적적용.Enabled = this.btn프로젝트적용1.Enabled = false;
		}

		private void CallSo(string noSo)
		{
			bool 헤더수정유무 = false;
			string 단가적용형태 = string.Empty;

			DataRow dataRow = this._biz.SearchSo(noSo, out 헤더수정유무, out 단가적용형태);
			this._구분 = "적용";
			this._헤더수정여부 = 헤더수정유무;
			this._단가적용형태 = 단가적용형태;
			this._수주상태 = D.GetString(dataRow["STA_SO"]);
			this._거래구분 = D.GetString(dataRow["TP_BUSI"]);
			this._출하형태 = D.GetString(dataRow["TP_GI"]);
			this._매출형태 = D.GetString(dataRow["TP_IV"]);
			this._의뢰여부 = D.GetString(dataRow["GIR"]);
			this._출하여부 = D.GetString(dataRow["GI"]);
			this._매출여부 = D.GetString(dataRow["IV"]);
			this._수출여부 = D.GetString(dataRow["TRADE"]);

			this.SearchSo(noSo);
		}

		private void CheckItem(string cdPlant, decimal seqSo, DataRow[] drs, int i)
		{
			DataTable table1 = new DataView(this._flexH.DataTable, "CD_ITEM IS NOT NULL AND CD_ITEM <> '' AND SEQ_SO <> " + seqSo, "SEQ_SO", DataViewRowState.CurrentRows).ToTable();

			if (table1.Rows.Count == 0)
			{
				this.품목추가(i, drs, false);
			}
			else
			{
				DataTable table2 = table1.DefaultView.ToTable(true, new string[] { "CD_PLANT", "CD_ITEM" });
				this.품목추가(i, drs, false);
				bool flag = true;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("CD_ITEM".PadRight(25, ' ') + "NM_ITEM");
				stringBuilder.AppendLine("-".PadRight(50, '-'));
				table2.PrimaryKey = new DataColumn[] { table2.Columns["CD_PLANT"], table2.Columns["CD_ITEM"] };

				foreach (DataRow dr in drs)
				{
					if (table2.Rows.Find(new object[] { cdPlant, D.GetString(dr["CD_ITEM"]) }) != null)
					{
						flag = false;
						stringBuilder.AppendLine(D.GetString(dr["CD_ITEM"]).PadRight(25, ' ') + D.GetString(dr["NM_ITEM"]));
					}
				}

				if (flag)
					return;

				this.ShowDetailMessage("중복된 품목이 존재합니다" + Environment.NewLine + "[더보기] 버튼을 눌러 목록을 확인하세요!! ", stringBuilder.ToString());
			}
		}

		private void ControlVisibleSetting()
		{
			this.사용자정의셋팅();

			if (BASIC.GetMAEXC("여신한도") == "200")
				this.btn엑셀업로드.Visible = this.btn추가.Visible = false;

			this.btn창고적용.Visible = !ConfigSA.SA_EXC.WH정보사용;

			if (MA.기준환율.Option == 기준환율옵션.적용_수정불가)
				this.cur환율.Enabled = false;

			if (this._biz.GetATP사용여부 == "000")
				this.btnATPCHECK.Visible = false;

			if (this.수주Config.부가세포함단가사용())
				this.cbo부가세.Enabled = true;

			if (this.수주Config.결제조건도움창사용())
				this.btn결제조건.Visible = true;

			if (!string.IsNullOrEmpty(this._수주번호))
				this.CallSo(this._수주번호);

			if (App.SystemEnv.PMS사용)
			{
				this.btn업무공유.Visible = true;
				this.btn업무공유.Click += new EventHandler(this.btn업무공유_Click);
			}

			if (BASIC.GetMAEXC("수주등록-상품적용(EC모듈) 표시여부") == "100")
				this.btn상품적용.Visible = false;

			if (BASIC.GetMAEXC("수주등록-영업기회적용사용여부") != "001")
				return;

			this.btn영업기회적용.Visible = true;
		}

		private void IsInvAmCalc()
		{
			if (!this._flexH.HasNormalRow)
				return;

			if (this._biz.Get과세변경유무 == "Y" || this._매출자동여부 == "N")
				return;

			decimal num2 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_WONAMT)", string.Empty));
			this.cur공급가액.DecimalValue = num2;
			this._header.CurrentRow["AM_IV"] = num2;
			this._header.CurrentRow["AM_IV_EX"] = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_SO)", string.Empty));

			decimal num5 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_VAT)", string.Empty));
			this.cur부가세액.DecimalValue = num5;
			this._header.CurrentRow["AM_IV_VAT"] = num5;
		}

		private void cur공급가액_Validated(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
					return;

				if (this._biz.Get과세변경유무 == "Y" || this._매출자동여부 == "N")
					return;

				decimal num2 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_WONAMT)", string.Empty));
				if (this.cur공급가액.DecimalValue != num2)
					this._flexH[this._flexH.Rows.Count - 1, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[this._flexH.Rows.Count - 1, "AM_WONAMT"]) + (this.cur공급가액.DecimalValue - num2));

				this.Calc부가세포함(this._flexH.Rows.Count - 1);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void cur부가세액_Validated(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
					return;

				if (this._biz.Get과세변경유무 == "Y" || this._매출자동여부 == "N")
					return;

				decimal num2 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_VAT)", string.Empty));
				if (this.cur부가세액.DecimalValue != num2)
					this._flexH[this._flexH.Rows.Count - 1, "AM_VAT"] = (D.GetDecimal(this._flexH[this._flexH.Rows.Count - 1, "AM_VAT"]) + (this.cur부가세액.DecimalValue - num2));

				this.Calc부가세포함(this._flexH.Rows.Count - 1);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void IsChkTab5Activate()
		{
			if (this._biz.Get과세변경유무 == "N" && this._매출자동여부 == "Y")
			{
				if (!this.tabControl.TabPages.ContainsKey(this.tpg매출정보.Name))
					this.tabControl.TabPages.Add(this.tpg매출정보);

				this._flexH.Cols["TP_IV"].AllowEditing = true;
				this._flexH.Cols["TP_IV"].Visible = true;
			}
			else
			{
				this.tabControl.TabPages.Remove(this.tpg매출정보);

				this._flexH.Cols["TP_IV"].AllowEditing = false;
				this._flexH.Cols["TP_IV"].Visible = false;
			}
		}

		private void 원그리드적용하기()
		{
			this.pnl기본정보.UseCustomLayout = true;
			this.pnl부가정보.UseCustomLayout = true;
			this.pnl수출정보.UseCustomLayout = true;
			this.pnl기타.UseCustomLayout = true;
			this.pnl매출정보.UseCustomLayout = true;
			this.pnl기본정보.IsSearchControl = false;
			this.pnl부가정보.IsSearchControl = false;
			this.pnl수출정보.IsSearchControl = false;
			this.pnl기타.IsSearchControl = false;
			this.pnl매출정보.IsSearchControl = false;
			this.bpPanelControl1.IsNecessaryCondition = true;
			this.bpPanelControl2.IsNecessaryCondition = true;
			this.bpPanelControl3.IsNecessaryCondition = true;
			this.bpPanelControl4.IsNecessaryCondition = true;
			this.bpPanelControl5.IsNecessaryCondition = true;
			this.bpPanelControl6.IsNecessaryCondition = true;
			this.bpPanelControl7.IsNecessaryCondition = true;
			this.bpPanelControl8.IsNecessaryCondition = true;
			this.bpPanelControl9.IsNecessaryCondition = true;
			this.bpPanelControl10.IsNecessaryCondition = true;
			this.bpPanelControl11.IsNecessaryCondition = true;
			this.bpPanelControl12.IsNecessaryCondition = true;
			this.bpPanelControl57.IsNecessaryCondition = true;
			this.bpPanelControl60.IsNecessaryCondition = true;
			this.bpPanelControl64.IsNecessaryCondition = true;
			this.bpPanelControl65.IsNecessaryCondition = true;
			this.pnl기본정보.InitCustomLayout();
			this.pnl부가정보.InitCustomLayout();
			this.pnl수출정보.InitCustomLayout();
			this.pnl기타.InitCustomLayout();
			this.pnl매출정보.InitCustomLayout();
		}

		private void Set품목셋팅(int idx, DataRow[] drSet품목S)
		{
			try
			{
				bool flag = true;
				수주관리.Calc calc = new 수주관리.Calc();
				this._flexH.Redraw = false;
				this._flexH.SetDummyColumnAll();
				DataTable dataTable = null;
				string empty = string.Empty;

				if (D.GetString(this._flexH[idx, "CD_PLANT"]) != string.Empty)
					empty = D.GetString(this._flexH[idx, "CD_PLANT"]);

				string CD_CC;
				string NM_CC;

				foreach (DataRow dataRow1 in drSet품목S)
				{
					if (flag)
					{
						this._flexH[idx, "SEQ_SO"] = this.최대차수 + 1;
						this._flexH[idx, "GI_PARTNER"] = this.ctx납품처.CodeValue;
						this._flexH[idx, "LN_PARTNER"] = this.ctx납품처.CodeName;

						if (this.disCount_YN == "Y")
						{
							this._flexH[idx, "RT_DSCNT"] = 0;
							this._flexH[idx, "UM_BASE"] = 0;
						}

						this._flexH[idx, "TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
						this._flexH[idx, "RT_VAT"] = this.cur부가세율.DecimalValue;
						this._flexH[idx, "DT_DUEDATE"] = this.dtp납기일.Text;
						this._flexH[idx, "DT_REQGI"] = this.dtp납기일.Text;
						this._flexH[idx, "STA_SO1"] = this._수주상태;
						this._flexH[idx, "CD_PLANT"] = dataRow1["CD_PLANT"];
						this._flexH[idx, "CD_ITEM"] = dataRow1["CD_ITEM"];
						this.CC조회(D.GetString(dataRow1["GRP_ITEM"]), D.GetString(this._flexH[idx, "CD_ITEM"]), D.GetString(this._flexH[idx, "CD_PLANT"]), out CD_CC, out NM_CC);
						this._flexH[idx, "CD_CC"] = CD_CC;
						this._flexH[idx, "NM_CC"] = NM_CC;
						this._flexH[idx, "NM_ITEM"] = dataRow1["NM_ITEM"];
						this._flexH[idx, "STND_ITEM"] = dataRow1["STND_ITEM"];
						this._flexH[idx, "UNIT_SO"] = dataRow1["UNIT_SO"];
						this._flexH[idx, "TP_ITEM"] = dataRow1["TP_ITEM"];
						this._flexH[idx, "TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
						this._flexH[idx, "RT_VAT"] = this.cur부가세율.DecimalValue;
						this._flexH[idx, "QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dataRow1["QT_CALC"]));
						this._flexH[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, calc.관리수량(D.GetDecimal(this._flexH["QT_SO"]), D.GetDecimal(this._flexH["UNIT_SO_FACT"])));
						this._flexH[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow1["UM_SET"]));
						this._flexH[idx, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH[idx, "QT_SO"]) * D.GetDecimal(this._flexH[idx, "UM_SO"]));
						this._flexH[idx, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_SO"]) * this.cur환율.DecimalValue);
						this._flexH[idx, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) * (this.cur부가세율.DecimalValue / 100));
						this._flexH[idx, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) + D.GetDecimal(this._flexH["AM_VAT"]));
						this._flexH[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AMVAT_SO"]) / D.GetDecimal(this._flexH["QT_SO"]));
						this._flexH[idx, "UNIT_SO_FACT"] = D.GetDecimal(dataRow1["UNIT_SO_FACT"]) == 0 ? 1 : dataRow1["UNIT_SO_FACT"];
						this._flexH[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["QT_SO"]) * D.GetDecimal(this._flexH["UNIT_SO_FACT"]));
						this._flexH[idx, "UNIT_IM"] = dataRow1["UNIT_IM"];
						this._flexH[idx, "EN_ITEM"] = dataRow1["EN_ITEM"];
						this._flexH[idx, "STND_DETAIL_ITEM"] = dataRow1["STND_DETAIL_ITEM"];
						this._flexH[idx, "GRP_MFG"] = dataRow1["GRP_MFG"];
						this._flexH[idx, "LT_GI"] = D.GetDecimal(dataRow1["LT_GI"]);
						this._flexH[idx, "WEIGHT"] = dataRow1["WEIGHT"];
						this._flexH[idx, "UNIT_WEIGHT"] = dataRow1["UNIT_WEIGHT"];
						this._flexH[idx, "FG_SERNO"] = dataRow1["FG_SERNO"];
						this._flexH[idx, "YN_ATP"] = dataRow1["YN_ATP"];
						this._flexH[idx, "CUR_ATP_DAY"] = dataRow1["CUR_ATP_DAY"];
						this._flexH[idx, "FG_MODEL"] = dataRow1["FG_MODEL"];
						this._flexH[idx, "UNIT_GI_FACT"] = (D.GetDecimal(dataRow1["UNIT_GI_FACT"]) == 0 ? 1 : D.GetDecimal(dataRow1["UNIT_GI_FACT"]));
						this._flexH[idx, "CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
						this._flexH[idx, "TP_IV"] = "002";
						this._flexH[idx, "MAT_ITEM"] = dataRow1["MAT_ITEM"];
						this._flexH[idx, "CD_ITEM_REF"] = dataRow1["CD_ITEM"];
						this._flexH[idx, "NM_ITEM_REF"] = dataRow1["NM_ITEM"];
						this._flexH[idx, "STND_ITEM_REF"] = dataRow1["STND_ITEM_SET"];
						this._flexH[idx, "YN_PICKING"] = this._배송여부;

						if (dataTable != null)
						{
							DataRow dataRow2 = dataTable.Rows.Find(dataRow1["CD_ITEM"]);
							this._flexH[idx, "UM_BASE"] = dataRow2 == null ? 0 : dataRow2["UM_BASE"];
							this._flexH[idx, "NUM_USERDEF1"] = dataRow2 == null ? 0 : dataRow2["NUM_USERDEF1"];
							this._flexH[idx, "RT_DSCNT"] = dataRow2 == null ? 0 : dataRow2["RT_DSCNT"];
							this._flexH[idx, "NUM_USERDEF5"] = dataRow2 == null ? 0 : dataRow2["RT_DSCNT"];
							this._flexH[idx, "NUM_USERDEF2"] = dataRow2 == null ? 0 : dataRow2["NUM_USERDEF2"];
							this._flexH[idx, "UM_SO"] = dataRow2 == null ? 0 : dataRow2["NUM_USERDEF2"];
							this._flexH[idx, "NUM_USERDEF6"] = (D.GetDecimal(this._flexH[idx, "NUM_USERDEF2"]) - D.GetDecimal(this._flexH[idx, "NUM_USERDEF4"]));
							this.Calc금액변경(idx);
							this.Calc부가세포함(idx);
						}

						flag = false;
					}
					else
					{
						this._flexH.Rows.Add();
						this._flexH.Row = this._flexH.Rows.Count - 1;
						this._flexH["SEQ_SO"] = this.최대차수 + 1;
						this._flexH["GI_PARTNER"] = this.ctx납품처.CodeValue;
						this._flexH["LN_PARTNER"] = this.ctx납품처.CodeName;

						if (this.disCount_YN == "Y")
						{
							this._flexH["RT_DSCNT"] = 0;
							this._flexH["UM_BASE"] = 0;
						}

						this._flexH["TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
						this._flexH["RT_VAT"] = this.cur부가세율.DecimalValue;
						this._flexH["DT_DUEDATE"] = this.dtp납기일.Text;
						this._flexH["DT_REQGI"] = this.dtp납기일.Text;
						this._flexH["STA_SO1"] = this._수주상태;
						this._flexH["CD_PLANT"] = dataRow1["CD_PLANT"];
						this._flexH["CD_ITEM"] = dataRow1["CD_ITEM"];
						this.CC조회(D.GetString(dataRow1["GRP_ITEM"]), D.GetString(this._flexH["CD_ITEM"]), D.GetString(this._flexH["CD_PLANT"]), out CD_CC, out NM_CC);
						this._flexH["CD_CC"] = CD_CC;
						this._flexH["NM_CC"] = NM_CC;
						this._flexH["NM_ITEM"] = dataRow1["NM_ITEM"];
						this._flexH["STND_ITEM"] = dataRow1["STND_ITEM"];
						this._flexH["UNIT_SO"] = dataRow1["UNIT_SO"];
						this._flexH["TP_ITEM"] = dataRow1["TP_ITEM"];
						this._flexH["TP_VAT"] = D.GetString(this.cbo부가세구분.SelectedValue);
						this._flexH["RT_VAT"] = this.cur부가세율.DecimalValue;
						this._flexH["QT_SO"] = D.GetDecimal(dataRow1["QT_CALC"]);
						this._flexH["QT_IM"] = calc.관리수량(D.GetDecimal(this._flexH["QT_SO"]), D.GetDecimal(this._flexH["UNIT_SO_FACT"]));
						this._flexH["UM_SO"] = D.GetDecimal(dataRow1["UM_SET"]);
						this._flexH["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["QT_SO"]) * D.GetDecimal(this._flexH["UM_SO"]));
						this._flexH["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_SO"]) * this.cur환율.DecimalValue);
						this._flexH["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) * (this.cur부가세율.DecimalValue / 100));
						this._flexH["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AM_WONAMT"]) + D.GetDecimal(this._flexH["AM_VAT"]));
						this._flexH["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["AMVAT_SO"]) / D.GetDecimal(this._flexH["QT_SO"]));
						this._flexH["UNIT_SO_FACT"] = D.GetDecimal(dataRow1["UNIT_SO_FACT"]) == 0 ? 1 : dataRow1["UNIT_SO_FACT"];
						this._flexH["QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexH["QT_SO"]) * D.GetDecimal(this._flexH["UNIT_SO_FACT"]));
						this._flexH["UNIT_IM"] = dataRow1["UNIT_IM"];
						this._flexH["EN_ITEM"] = dataRow1["EN_ITEM"];
						this._flexH["STND_DETAIL_ITEM"] = dataRow1["STND_DETAIL_ITEM"];
						this._flexH["GRP_MFG"] = dataRow1["GRP_MFG"];
						this._flexH["LT_GI"] = D.GetDecimal(dataRow1["LT_GI"]);
						this._flexH["WEIGHT"] = dataRow1["WEIGHT"];
						this._flexH["UNIT_WEIGHT"] = dataRow1["UNIT_WEIGHT"];
						this._flexH["FG_SERNO"] = dataRow1["FG_SERNO"];
						this._flexH["YN_ATP"] = dataRow1["YN_ATP"];
						this._flexH["CUR_ATP_DAY"] = dataRow1["CUR_ATP_DAY"];
						this._flexH["FG_MODEL"] = dataRow1["FG_MODEL"];
						this._flexH["UNIT_GI_FACT"] = (D.GetDecimal(dataRow1["UNIT_GI_FACT"]) == 0 ? 1 : D.GetDecimal(dataRow1["UNIT_GI_FACT"]));
						this._flexH["CD_EXCH"] = D.GetString(this.cbo화폐단위.SelectedValue);
						this._flexH["TP_IV"] = "002";
						this._flexH["MAT_ITEM"] = dataRow1["MAT_ITEM"];
						this._flexH["CD_ITEM_REF"] = dataRow1["CD_ITEM"];
						this._flexH["NM_ITEM_REF"] = dataRow1["NM_ITEM"];
						this._flexH["STND_ITEM_REF"] = dataRow1["STND_ITEM_SET"];
						this._flexH["YN_PICKING"] = this._배송여부;

						if (dataTable != null)
						{
							DataRow dataRow2 = dataTable.Rows.Find(dataRow1["CD_ITEM"]);
							this._flexH["UM_BASE"] = dataRow2 == null ? 0 : dataRow2["UM_BASE"];
							this._flexH["NUM_USERDEF1"] = dataRow2 == null ? 0 : dataRow2["NUM_USERDEF1"];
							this._flexH["RT_DSCNT"] = dataRow2 == null ? 0 : dataRow2["RT_DSCNT"];
							this._flexH["NUM_USERDEF5"] = dataRow2 == null ? 0 : dataRow2["RT_DSCNT"];
							this._flexH["NUM_USERDEF2"] = dataRow2 == null ? 0 : dataRow2["NUM_USERDEF2"];
							this._flexH["UM_SO"] = dataRow2 == null ? 0 : dataRow2["NUM_USERDEF2"];
							this._flexH["NUM_USERDEF6"] = (D.GetDecimal(this._flexH["NUM_USERDEF2"]) - D.GetDecimal(this._flexH["NUM_USERDEF4"]));
							this.Calc금액변경(this._flexH.Row);
							this.Calc부가세포함(this._flexH.Row);
						}
					}
				}

				this._flexH.RemoveDummyColumnAll();
				this._flexH.AddFinished();
				this._flexH.Col = this._flexH.Cols.Fixed;
				this._flexH.Redraw = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion
	}
}
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.ConstLib;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.PU.Common;
using Duzon.Windows.Print;
using DzHelpFormLib;
using master;
using pur;
using Dintec;

namespace cz
{
	public partial class P_CZ_PU_STS_REG : PageBase
	{
		private P_CZ_PU_STS_REG_BIZ _biz = new P_CZ_PU_STS_REG_BIZ();
		private DataTable _dtH = null;
		private FreeBinding _header = null;
		private DataTable _dt_pjt = null;
		public string MNG_LOT = string.Empty;
		public string MNG_SERIAL = string.Empty;
		internal string m_sys_set_SL = string.Empty;
		private string fg_sub = string.Empty;
		private DataTable _dtReqDataREQ = new DataTable();
		private bool 프로젝트사용 = false;
		private bool PJT형여부 = false;
		public string _no_io = string.Empty;
		public string _cd_pjt = string.Empty;
		private Decimal d_SEQ_PROJECT = 0;
		private string s_CD_PJT_ITEM = string.Empty;
		private string s_NM_PJT_ITEM = string.Empty;
		private string s_PJT_ITEM_STND = string.Empty;

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
				bool flag = this._header.GetChanges() != null;
				if (flag && this._header.JobMode == JobModeEnum.추가후수정 && !this._flex.HasNormalRow)
					flag = false;
				return flag;
			}
		}

		public P_CZ_PU_STS_REG()
		{
			try
			{
                StartUp.Certify(this);
				this.InitializeComponent();

				this.MainGrids = new FlexGrid[] { this._flex };
				this._header = new FreeBinding();
				this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
				this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public P_CZ_PU_STS_REG(string p_no_io, string p_cd_pjt)
		{
			try
			{
                StartUp.Certify(this);
				this.InitializeComponent();

				this.MainGrids = new FlexGrid[] { this._flex };
				this._header = new FreeBinding();
				this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
				this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);
				this._no_io = p_no_io;
				this._cd_pjt = p_cd_pjt;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public P_CZ_PU_STS_REG(PageBaseConst.CallType pageCallType, string idMemo)
			: this()
		{
            StartUp.Certify(this);
			DataTable noIo = this._biz.GetNoIO(idMemo);
			this.str수불번호 = D.GetString(noIo.Rows[0]["NO_IO"]);
			this.str공장코드 = D.GetString(noIo.Rows[0]["CD_PLANT"]);
		}

		public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
		{
			object[] args = e.Args;
			this._no_io = D.GetString(args[0]);
			this._cd_pjt = D.GetString(args[1]);
			this.InitPaint();
		}

		private void _header_JobModeChanged(object sender, FreeBindingArgs e)
		{
			try
			{
				if (e.JobMode == JobModeEnum.추가후수정)
				{
					this._header.SetControlEnabled(true);
					this.txt비고.Enabled = true;
				}
				else
				{
					this._header.SetControlEnabled(false);
					this.txt비고.Enabled = true;
				}
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
				this.Page_DataChanged(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.MNG_LOT = this._biz.Search_LOT().Rows[0]["MNG_LOT"].ToString();
			this.MNG_SERIAL = this._biz.Search_SERIAL();
			this.m_sys_set_SL = BASIC.GetMAEXC("S/L간이동처리-창고권한통제");
			this.프로젝트사용 = Config.MA_ENV.프로젝트사용;
			this.PJT형여부 = Config.MA_ENV.PJT형여부 == "Y";

			if (Global.MainFrame.ServerKeyCommon == "ASAN" || Global.MainFrame.ServerKeyCommon == "ASUNG")
			{
				this.btnD2계획적용.Visible = true;
				this.btn수주적용.Visible = true;
			}
			
			if (App.SystemEnv.PMS사용)
				this.btn업무공유.Visible = true;
			
			if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON") || Global.MainFrame.ServerKeyCommon.Contains("DZSQL"))
			{
				this.btn출고증요청적용.Visible = true;
			}

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("CD_ITEM", "품목코드", 120, true);
			this._flex.SetCol("NM_ITEM", "품목명", 150, false);
			this._flex.SetCol("QT_GOOD_INV", "이동수량", 120, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_UNIT_PO", "이동수량(발주단위)", false);
			this._flex.SetCol("출고창고", "출고창고", 100, true);
			this._flex.SetCol("입고창고", "입고창고", 100, true);
			this._flex.SetCol("DC_RMK", "라인비고", 150, true);
			this._flex.SetCol("DC_RMK1", "라인비고1", 150, true);

			this._flex.SetCol("STND_ITEM", "규격", false);
			this._flex.SetCol("UNIT_IM", "단위", false);
			this._flex.SetCol("UNIT_PO", "발주단위", false);
			this._flex.SetCol("NM_ITEMGRP", "품목군", false);
			this._flex.SetCol("FG_SERNO", "S/N,LOT관리", false);
			this._flex.SetCol("NO_LOT", "LOT여부", false);
			this._flex.SetCol("QT_GOODS", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("CD_ZONE", "LOCATION", 120, false);
            this._flex.SetCol("CD_PROJECT", "프로젝트", 120, true, typeof(string));
            this._flex.SetCol("NM_PROJECT", "프로젝트명", 120, false, typeof(string));

			if (this.프로젝트사용)
			{
				if (this.PJT형여부)
				{
					if (!App.SystemEnv.PMS사용)
					{
						this._flex.SetCol("NO_WBS", "WBS번호", 100, false, typeof(string));
					}
					else
					{
						this._flex.SetCol("CD_CSTR", "CBS품목코드", 110, false, typeof(string));
						this._flex.SetCol("DL_CSTR", "CBS내역코드", 80, false, typeof(string));
						this._flex.SetCol("NM_CSTR", "CBS항목명", 140, false, typeof(string));
						this._flex.SetCol("SIZE_CSTR", "CBS규격", 140, false, typeof(string));
						this._flex.SetCol("UNIT_CSTR", "CBS단위", 110, false, typeof(string));
						this._flex.SetCol("QTY_ACT", "CBS예산수량", 120, false, typeof(decimal), FormatTpType.QUANTITY);
						this._flex.SetCol("UNT_ACT", "CBS예산단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
						this._flex.SetCol("AMT_ACT", "CBS예산금액", 100, false, typeof(decimal), FormatTpType.MONEY);
					}

					this._flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 100, false, typeof(decimal));
					this._flex.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트품목", 120, true, typeof(string));
					this._flex.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트품목명", 120, false, typeof(string));
					this._flex.SetCol("STND_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트품목규격", 100, false, typeof(string));
					this._flex.SetCol("NO_CBS", "CBS번호", 100, false, typeof(string));
				}
			}

			this._flex.SetCol("NO_ISURCV", "요청번호", false);
			this._flex.SetCol("NO_PSO_MGMT", "작업지시번호", false);
			this._flex.SetCol("BARCODE", "BARCODE", false);
			this._flex.SetCol("QT_GIREQ", "요청수량", false);
			this._flex.SetCol("FG_SLQC", "검사(이동)", false);
			this._flex.SetCol("CLS_ITEM", "계정구분", false);
			this._flex.SetCol("NO_EMP", "요청자", false);
			this._flex.SetCol("요청자", "요청자명", false);
			this._flex.SetCol("요청부서", "요청부서", false);
			this._flex.SetCol("FG_GUBUN", "요청구분", false);
			this._flex.SetCol("LN_PARTNER", "외주거래처", false);
			this._flex.SetCol("NM_MAKER", "MAKER", false);
			this._flex.SetCol("NM_CLS_L", "대분류", false);
			this._flex.SetCol("NM_CLS_M", "중분류", false);
			this._flex.SetCol("NM_CLS_S", "소분류", false);
			this._flex.SetCol("GI_PARTNER", "납품처코드", false);
			this._flex.SetCol("LN_GI_PARTNER", "납품처명", false);

			if (Global.MainFrame.ServerKeyCommon.Contains("HANSU"))
			{
				this._flex.SetCol("CD_ITEM_PARTNER", "전용코드", 100, false);
				this._flex.SetCol("NM_ITEM_PARTNER", "전용품명", 100, false);
				this._flex.SetCol("CD_PACK", "포장단위", 100, false);
				this._flex.SetCol("NM_PACK", "포장단위명", 100, false);
				this._flex.SetCol("CD_PART", "납품부서코드", 100, false);
				this._flex.SetCol("NM_PART", "납품부서명", 100, false);
				this._flex.SetCol("YN_TEST_RPT", "성적서여부", 100, false);
				this._flex.SetCol("DT_DELIVERY", "납기요구일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				this._flex.SetCol("CD_TRANSPORT", "운송방법", 100, false);
				this._flex.SetCol("DC_DESTINATION", "목적지", 100, false);
				this._flex.SetCol("DC_RMK_REQ", "요청비고", 200, 100, false, typeof(string));
				this._flex.SetCol("CD_PARTNER", "매출처코드", 100, false);
				this._flex.SetCol("PRIOR_GUBUN", "선입구분", 100, false);
				this._flex.SetCol("NUM_USERDEF1", "선입수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
				this._flex.Cols["LN_PARTNER"].Caption = "매출처";
			}
			else
			{
				this._flex.SetCol("DT_DELIVERY_IO", "납기일", false);
				this._flex.SetCol("NM_CUST_DLV", "수취인(배송지정보)", false);
				this._flex.SetCol("NO_TEL_D1", "전화(배송지정보)", false);
				this._flex.SetCol("NO_TEL_D2", "이동전화(배송지정보)", false);
				this._flex.SetCol("ADDR1", "주소1(배송지정보)", false);
				this._flex.SetCol("ADDR2", "주소2(배송지정보)", false);
				this._flex.SetCol("TP_DLV", "배송방법(배송지정보)", false);
				this._flex.SetCol("DC_REQ", "비고(배송지정보)", false);
			}

			if (Global.MainFrame.ServerKeyCommon == "CNP" || Global.MainFrame.ServerKeyCommon == "SQL_")
			{
				this._flex.SetCol("AM_REQ", "요청금액", 100, false, typeof(decimal), FormatTpType.MONEY);
				this._flex.SetCol("UM_REQ", "요청단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
			}

			if (Global.MainFrame.ServerKeyCommon == "SANSUNG")
			{
				this._flex.SetCol("NUM_STND_ITEM_1", "입수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
				this._flex.SetCol("QT_BOX", "박스수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
				this._flex.SetCol("QT_SAP", "낱개", 100, false, typeof(decimal), FormatTpType.QUANTITY);
				this._flex.SetCol("QT_BOX_RE", "잔량박스수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			}

			this._flex.SetCol("STND_DETAIL_ITEM", "세부규격", false);
			this._flex.SetCol("MAT_ITEM", "재질", false);
			this._flex.SetCol("NM_GRP_MFG", "제품군", false);
			this._flex.SetCol("FG_TRACK", "적용구분", false);
			this._flex.Cols["FG_TRACK"].AllowEditing = false;
			this._flex.SetCol("NO_PO_PARTNER", "거래처PO번호", false);
			this._flex.SetCol("DC1_SOL", "수주라인비고", false);
			this._flex.SetDummyColumn("S");

			if (Config.MA_ENV.YN_UNIT == "Y")
				this._flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_IM", "UNIT_PO", "NO_LOT", "CD_ZONE", "NM_ITEMGRP", "FG_SERNO", "QT_GOODS", "FG_SLQC", "NM_PROJECT", "SEQ_PROJECT", "NM_PJT_ITEM", "STND_UNIT", "GI_PARTNER", "LN_GI_PARTNER");
			else
				this._flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_IM", "UNIT_PO", "NO_LOT", "CD_ZONE", "NM_ITEMGRP", "FG_SERNO", "QT_GOODS", "FG_SLQC", "GI_PARTNER", "LN_GI_PARTNER");
			
			this._flex.SettingVersion = "1.0.2.1";
			
			if (this.프로젝트사용 && Global.MainFrame.ServerKeyCommon.ToUpper() != "YWD")
			{
				if (Config.MA_ENV.YN_UNIT == "Y")
					this._flex.VerifyNotNull = new string[] { "CD_ITEM",
															  "출고창고",
															  "입고창고",
															  "CD_PROJECT",
															  "SEQ_PROJECT" };
				else
					this._flex.VerifyNotNull = new string[] { "CD_ITEM",
															  "출고창고",
															  "입고창고",
															  "CD_PROJECT" };
				this._flex.SetExceptSumCol("SEQ_PROJECT");
			}
			else
				this._flex.VerifyNotNull = new string[] { "CD_ITEM",
														  "출고창고",
														  "입고창고" };

			Config.UserColumnSetting.InitGrid_UserMenu(this._flex, this.PageID, true);
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flex.SetCodeHelpCol("CD_ITEM", "H_CZ_MA_CUSTOMIZE_SUB", ShowHelpEnum.Always, new string[] { "CD_ITEM",
																											  "NM_ITEM",
																											  "STND_ITEM",
																											  "UNIT_IM",
																											  "UNIT_PO",
																											  "CD_ZONE",
																											  "CLS_ITEM",
																											  "NO_DESIGN",
																											  "NM_CLS_L",
																											  "NM_CLS_M",
																											  "NM_CLS_S" }, new string[] { "CD_ITEM",
																																		   "NM_ITEM",
																																		   "STND_ITEM",
																																		   "NM_UNIT_IM",
																																		   "NM_UNIT_PO",
																																		   "CD_ZONE",
																																		   "CLS_ITEM",
																																		   "NO_DESIGN",
																																		   "NM_CLS_L",
																																		   "NM_CLS_M",
																																		   "NM_CLS_S" });

			this._flex.SetCodeHelpCol("출고창고", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "출고창고" }, new string[] { "CD_SL", "NM_SL" }, ResultMode.SlowMode);
			this._flex.SetCodeHelpCol("입고창고", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL_REF", "입고창고" }, new string[] { "CD_SL", "NM_SL" }, ResultMode.SlowMode);
			this._flex.SetCodeHelpCol("NO_EMP", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "요청자", "요청부서" }, new string[] { "NO_EMP", "NM_KOR", "NM_DEPT" }, ResultMode.SlowMode);
			
			if (Config.MA_ENV.YN_UNIT == "Y")
			{
				this._flex.SetCodeHelpCol("CD_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PROJECT",
																											"NM_PROJECT",
																											"SEQ_PROJECT",
																											"CD_PJT_ITEM",
																											"NM_PJT_ITEM",
																											"STND_UNIT" }, new string[] { "NO_PROJECT",
																																		  "NM_PROJECT",
																																		  "SEQ_PROJECT",
																																		  "CD_PJT_ITEM",
																																		  "NM_PJT_ITEM",
																																		  "PJT_ITEM_STND" }, new string[] { "CD_PROJECT",
																																											"NM_PROJECT",
																																											"SEQ_PROJECT",
																																											"CD_PJT_ITEM",
																																											"NM_PJT_ITEM",
																																											"STND_UNIT" }, ResultMode.FastMode);
				this._flex.SetCodeHelpCol("CD_PJT_ITEM", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PROJECT",
																											 "NM_PROJECT",
																											 "SEQ_PROJECT",
																											 "CD_PJT_ITEM",
																											 "NM_PJT_ITEM",
																											 "STND_UNIT" }, new string[] { "NO_PROJECT",
																																		   "NM_PROJECT",
																																		   "SEQ_PROJECT",
																																		   "CD_PJT_ITEM",
																																		   "NM_PJT_ITEM",
																																		   "PJT_ITEM_STND" }, new string[] { "CD_PROJECT",
																																											 "NM_PROJECT",
																																											 "SEQ_PROJECT",
																																											 "CD_PJT_ITEM",
																																											 "NM_PJT_ITEM",
																																											 "STND_UNIT" }, ResultMode.FastMode);
			}
			else
				this._flex.SetCodeHelpCol("CD_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PROJECT", "NM_PROJECT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });
			
			this._flex.VerifyAutoDelete = new string[] { "CD_ITEM" };
			this._flex.VerifyCompare(this._flex.Cols["QT_GOOD_INV"], 0, OperatorEnum.Greater);
			this._flex.DisableNumberColumnSort();
			
			this.의뢰번호Visible(this.fg_sub);
		}

		private void InitEvent()
		{
			this.btn업무공유.Click += new EventHandler(this.btn업무공유_Click);
			this.btn재고적용.Click += new EventHandler(this.btn재고적용_Click);
			this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
			this.btn추가.Click += new EventHandler(this.btn추가_Click);
			this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
			this.btn입고창고적용.Click += new EventHandler(this.btn창고적용_Click);
			this.btn출고창고적용.Click += new EventHandler(this.btn창고적용_Click);
			this.btn프로젝트적용.Click += new EventHandler(this.btn창고적용_Click);
			this.btn외주의뢰적용.Click += new EventHandler(this.btn외주의뢰적용_Click);
			this.btn요청적용.Click += new EventHandler(this.btn요청적용_Click);
			this.btn생산의뢰적용.Click += new EventHandler(this.btn생산의뢰적용_Click);
			this.btnBOM적용.Click += new EventHandler(this.Control_Click);
			this.btn지시소요량.Click += new EventHandler(this.Control_Click);
			this.btn수주적용.Click += new EventHandler(this.btn수주적용_Click);
			this.btn출고증요청적용.Click += new EventHandler(this.btn출고증요청적용_Click);
			this.btn입고적용.Click += new EventHandler(this.btn입고적용_Click);
			this.btnD2계획적용.Click += new EventHandler(this.btnD2계획적용_Click);
            this.btn출고반품적용.Click += new EventHandler(this.btn출고반품적용_Click);

			this.ctx입고창고.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx입고창고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx수불형태.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx수불형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx출고창고.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx출고창고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx프로젝트.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);

			this.cbo공장.KeyDown += new KeyEventHandler(this.Control_KeyDown);
			this.txt비고.KeyDown += new KeyEventHandler(this.Control_KeyDown);

			this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
			this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
			this._flex.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
			this._flex.AddRow += new EventHandler(this.btn추가_Click);
			this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			DataSet comboData = this.GetComboData("NC;MA_PLANT", "N;MA_B000010", "S;PU_C000080", "S;SA_Z_HS001", "N;EC_0000002");
			this.cbo공장.DataSource = comboData.Tables[0];
			this.cbo공장.DisplayMember = "NAME";
			this.cbo공장.ValueMember = "CODE";
			this._flex.SetDataMap("CLS_ITEM", comboData.Tables[1], "CODE", "NAME");
			this._flex.SetDataMap("FG_GUBUN", comboData.Tables[2], "CODE", "NAME");
			this._flex.SetDataMap("FG_TRACK", MA.GetCodeUser(new string[] { "BOM", 
																			"PR",
																			"SU",
																			"IO",
																			"GIR",
																			"INV",
                                                                            "GIRT"}, new string[] { "BOM적용",
																									"생산의뢰적용",
																									"외주의뢰적용",
																									"입고적용",
																									"요청적용",
																									"재고적용",
                                                                                                    "출고반품적용" }, 1 != 0), "CODE", "NAME");
			if (Global.MainFrame.ServerKeyCommon.Contains("HANSU"))
				this._flex.SetDataMap("PRIOR_GUBUN", comboData.Tables[3], "CODE", "NAME");
			else
				this._flex.SetDataMap("TP_DLV", comboData.Tables[4], "CODE", "NAME");

			this.oneGrid1.UseCustomLayout = true;
			this.bpPanelControl1.IsNecessaryCondition = true;
			this.bpPanelControl2.IsNecessaryCondition = true;
			this.bpPanelControl3.IsNecessaryCondition = true;
			this.bpPanelControl4.IsNecessaryCondition = true;
			this.bpPanelControl7.IsNecessaryCondition = true;
			this.oneGrid1.IsSearchControl = false;
			this.oneGrid1.InitCustomLayout();

			if (D.GetString(this.str수불번호) != "")
				this.Reload(this.str수불번호, this.str공장코드);
			else
			{
				this.Initial_Binding();
				this.ToolBarPrintButtonEnabled = false;
				if (this.서버키_TEST포함("WONBONG"))
					this.btn지시소요량.Visible = true;
				if (this._no_io != string.Empty)
					this.OnToolBarSearchButtonClicked(null, null);
				this.수불형태Default셋팅();
			}
		}

		private void Initial_Binding()
		{
			DataSet dataSet = this._biz.Initial_DataSet();
			this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
			this._header.ClearAndNewRow();
			this._flex.Binding = dataSet.Tables[1];
		}

		private void Page_DataChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this.IsChanged())
					return;
				this.ToolBarSaveButtonEnabled = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void InitControlEvent(Control panel)
		{
			foreach (Control control in (ArrangedElementCollection)panel.Controls)
			{
				if (control is TextBoxExt)
					control.Validated += new EventHandler(this.Control_Validated);
				else if (control is DatePicker)
				{
					control.Validated += new EventHandler(this.Control_Validated);
					((DatePicker)control).CalendarClosed += new EventHandler(this.Control_Validated);
				}
				else if (control is BpCodeTextBox)
				{
					((BpCodeTextBox)control).QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
					((BpCodeTextBox)control).QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
					((BpCodeTextBox)control).CodeChanged += new EventHandler(this.Control_CodeChanged);
				}
				else if (control is DropDownComboBox)
					((ComboBox)control).SelectionChangeCommitted += new EventHandler(this.Control_Validated);
				else if (control is CurrencyTextBox)
					control.Validated += new EventHandler(this.Control_Validated);
				else if (control is PanelExt)
					this.InitControlEvent(control);
			}
		}

		protected override bool BeforeSearch()
		{
			if (!base.BeforeSearch()) return false;
            if (string.IsNullOrEmpty(this.cbo공장.SelectedValue.ToString()))
            {
                this.ShowMessage("공장을 먼저 선택하십시오");
                this.cbo공장.Focus();
                return false;
            }

			return true;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;
				P_PU_STS_SUB pPuStsSub;

				if (this.프로젝트사용)
					pPuStsSub = new P_PU_STS_SUB(D.GetString(this.ctx출고창고.CodeValue), 
												 D.GetString(this.ctx출고창고.CodeName),
												 this.ctx담당자.CodeValue,
												 this.ctx담당자.CodeName,
												 this.cbo공장.SelectedValue.ToString(),
												 new object[] { D.GetString(this.ctx프로젝트.CodeValue),
																D.GetString(this.ctx프로젝트.CodeName) });
				else
					pPuStsSub = new P_PU_STS_SUB(D.GetString(this.ctx출고창고.CodeValue),
												 D.GetString(this.ctx출고창고.CodeName),
												 this.ctx담당자.CodeValue,
												 this.ctx담당자.CodeName,
												 this.cbo공장.SelectedValue.ToString());

				DialogResult dialogResult = DialogResult.OK;
				string NO_IO = string.Empty;
				string CD_PJT = string.Empty;
				
				if (this._no_io == string.Empty)
				{
					dialogResult = pPuStsSub.ShowDialog();
					
					if (dialogResult == DialogResult.OK)
					{
						NO_IO = pPuStsSub.m_NO_IO;
						CD_PJT = pPuStsSub.m_CD_PJT == null ? string.Empty : pPuStsSub.m_CD_PJT;
					}
				}
				else
				{
					NO_IO = this._no_io;
					CD_PJT = this._cd_pjt;
					this._no_io = string.Empty;
					this._cd_pjt = string.Empty;
				}

				if (dialogResult == DialogResult.OK)
				{
					DataSet dataSet = this._biz.Search(NO_IO, D.GetString(this.cbo공장.SelectedValue), CD_PJT);
					this._header.SetDataTable(dataSet.Tables[0]);
					this._flex.Binding = dataSet.Tables[1];
					
					if (this._flex["NO_EMP"].ToString() == string.Empty)
					{
						this._flex["NO_EMP"] = this.ctx담당자.CodeValue;
						
						if (Global.MainFrame.ServerKeyCommon != "THINK")
							this._flex["요청자"] = this.ctx담당자.CodeName;
						
						this._flex.AcceptChanges();
					}

					this.ctx출고창고.CodeValue = dataSet.Tables[0].Rows[0]["CD_SL"].ToString();
					this.ctx출고창고.CodeName = dataSet.Tables[0].Rows[0]["OUT_SL"].ToString();
					this.btn추가.Enabled = true;
					this.btn삭제.Enabled = true;
					this.btn요청적용.Enabled = false;
					this.btn외주의뢰적용.Enabled = false;
					this.btn생산의뢰적용.Enabled = false;
					this.ctx출고창고.Enabled = false;
					this.ctx입고창고.Enabled = false;

					if (Global.MainFrame.ServerKeyCommon == "TOKIMEC")
						this.btn추가.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeAdd()
		{
			return base.BeforeAdd() && this.MsgAndSave(PageActionMode.Search);
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				if (!this.BeforeAdd()) return;

				string currentGrantMenu = this.CurrentGrantMenu;
				Debug.Assert(this._header.CurrentRow != null);
				Debug.Assert(this._flex.DataTable != null);
				this._flex.DataTable.Rows.Clear();
				this._flex.AcceptChanges();
				this._header.ClearAndNewRow();
				this.btn추가.Enabled = true;
				this.btn삭제.Enabled = true;
				this.btn요청적용.Enabled = true;
				this.btn외주의뢰적용.Enabled = true;
				this.btn생산의뢰적용.Enabled = true;
				this.btn엑셀업로드.Enabled = true;
				this.ctx출고창고.Enabled = true;
				this.ctx입고창고.Enabled = true;
				this.ToolBarPrintButtonEnabled = false;
				this.dtp이동일.Enabled = true;
				this.ctx수불형태.Enabled = true;
				this.cbo공장.Enabled = true;

				if (!currentGrantMenu.Equals("E"))
					this.ctx담당자.Enabled = true;
				
				this.fg_sub = "";
				this.의뢰번호Visible(this.fg_sub);
				this.수불형태Default셋팅();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeDelete()
		{
			if (!base.BeforeDelete()) return false;

			if (D.GetString(this._header.CurrentRow["NO_QC"]) != "")
			{
				this.ShowMessage("검사처리 항목은 삭제할 수 없습니다. 검사번호 : " + D.GetString(this._header.CurrentRow["NO_QC"]));
				return false;
			}

			if (Global.MainFrame.ServerKeyCommon.Contains("PIOLINK"))
			{
				string detail = string.Empty;
				
				foreach (DataRow row in this._flex.DataTable.Rows)
				{
					DataTable dataTable = this._biz.SaveCheck(D.GetString(row["NO_IO"]), D.GetString(row["NO_IOLINE"]));
					if (dataTable != null && dataTable.Rows.Count > 0)
						detail = detail + "\n수불유형 : " + D.GetString(dataTable.Rows[0]["NM_QTIOTP"]) + " &     수불번호 : " + D.GetString(dataTable.Rows[0]["NO_IO"]);
				}
				
				if (detail != string.Empty && this.ShowDetailMessage("해당시리얼의 출고이력이 존재합니다. 삭제하시겠습니까?\n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", "", detail, "QY2") != DialogResult.Yes)
					return false;
			}

			return true;
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (Global.MainFrame.ShowMessage("삭제하시겠습니까?", "QY2") == DialogResult.Yes && this.BeforeDelete())
				{
					this._biz.Delete(this.txt수불번호.Text);
					this._header.AcceptChanges();
					this._flex.AcceptChanges();
					this.OnToolBarAddButtonClicked(sender, e);
					this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSaveButtonClicked(sender, e);

				if (!this.MsgAndSave(PageActionMode.Save)) return;

				this.ShowMessage(PageResultMode.SaveGood);
				
				if (App.SystemEnv.PMS사용)
					this.Reload(this.txt수불번호.Text, D.GetString(this.cbo공장.SelectedValue));
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
			
			decimal num1 = -1;
			
			if (this.추가모드여부)
			{
				string seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "12", this.dtp이동일.Text.Substring(0, 6));
				this._header.CurrentRow["NO_IO"] = seq;

				foreach (DataRow row in this._flex.DataTable.Rows)
				{
					num1 += 2;
					row["NO_IO"] = seq;
					row["NO_IOLINE"] = num1;
					row["FG_IO"] = "022";
					
					if (row["CD_SL"].ToString() == string.Empty)
					{
						row["CD_SL"] = D.GetString(this.ctx출고창고.CodeValue);
						row["출고창고"] = D.GetString(this.ctx출고창고.CodeName);
					}
					
					if (row["CD_SL_REF"].ToString() == string.Empty)
					{
						row["CD_SL_REF"] = this.ctx입고창고.CodeValue;
						row["입고창고"] = this.ctx입고창고.CodeName;
					}
					
					if (row["NO_EMP"].ToString() == string.Empty)
					{
						row["NO_EMP"] = this.ctx담당자.CodeValue;
						
						if (Global.MainFrame.ServerKeyCommon != "THINK")
							row["요청자"] = this.ctx담당자.CodeName;
					}
				}

				this.txt수불번호.Text = seq;
				this._header.CurrentRow["YN_RETURN"] = "N";

				if (Global.MainFrame.ServerKeyCommon.Contains("DONGWOON"))
				{
					if (D.GetString(this._header.CurrentRow["CD_PARTNER"]) == null || D.GetString(this._header.CurrentRow["CD_PARTNER"]) == "")
						this._header.CurrentRow["CD_PARTNER"] = "";
				}
				else
					this._header.CurrentRow["CD_PARTNER"] = "";
				
				this._header.CurrentRow["GI_PARTNER"] = "";
			}
			if (!this.추가모드여부)
			{
				string str = D.GetString(this.txt수불번호.Text);
				decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");

				foreach (DataRow row in this._flex.DataTable.Rows)
				{
					if (row.RowState == DataRowState.Added)
					{
						row["NO_IO"] = str;
						row["NO_IOLINE"] = (maxValue + 2);
						maxValue += 2;
						row["FG_IO"] = "022";
					}
				}
			}

			DataTable changes1 = this._header.GetChanges();
			DataTable changes2 = this._flex.GetChanges();
			string empty = string.Empty;
			
			if (changes1 == null && changes2 == null) return true;
			
			if (Global.MainFrame.ServerKeyCommon.Contains("ANJUN"))
			{
				string NO_ISURCV = string.Empty;

				foreach (DataRow row in changes2.Rows)
				{
					if (row.RowState != DataRowState.Deleted)
						NO_ISURCV = NO_ISURCV + D.GetString(row["NO_ISURCV"]) + "|";
				}

				if (NO_ISURCV != string.Empty)
				{
					DataTable dataTable = this._biz.YN_Pallet(NO_ISURCV);

					if (dataTable != null)
					{
						if (D.GetString(dataTable.Rows[0]["YN_PALLET"]) == "E")
						{
							int num2 = (int)this.ShowMessage("출고요청건중 적용구분이 다른데이터가 존재합니다.");
							return false;
						}

						empty = D.GetString(dataTable.Rows[0]["YN_PALLET"]);
					}
				}
			}

			DataTable dtLOT = null;
			
			if (string.Compare(this.MNG_LOT, "Y") == 0 && changes2 != null)
			{
				changes2.Columns.Add("NM_SL", typeof(string));
				DataRow[] dataRowArray = changes2.Select("NO_LOT = 'YES'", "", DataViewRowState.Added);
				DataTable dt = changes2.Clone();

				if (dataRowArray.Length > 0)
				{
					foreach (DataRow row in dataRowArray)
					{
						row["NM_SL"] = row["출고창고"];
						dt.ImportRow(row);
					}

					P_PU_LOT_SUB_I pPuLotSubI;

					if (Global.MainFrame.ServerKeyCommon.Contains("ANJUN"))
						pPuLotSubI = new P_PU_LOT_SUB_I(dt, new string[] { "N",
																		   "P_PU_STS_REG",
																		   empty });
					else
						pPuLotSubI = new P_PU_LOT_SUB_I(dt, new string[] { "N",
																		   "P_PU_STS_REG" });

					if (pPuLotSubI.ShowDialog() == DialogResult.OK)
					{
						dtLOT = pPuLotSubI.dtL;
						dtLOT.Columns.Add("입고창고", typeof(string));
						
						for (int index = 0; index < changes2.Rows.Count; ++index)
						{
							foreach (DataRow dataRow in dtLOT.Select(" 출고항번 = " + changes2.Rows[index]["NO_IOLINE"].ToString().Trim(), "", DataViewRowState.CurrentRows))
							{
								dataRow["입고창고"] = changes2.Rows[index]["CD_SL_REF"].ToString().Trim();
								dataRow["수불구분"] = "022";
							}
						}
					}
					else
					{
						if (this.추가모드여부)
						{
							this.txt수불번호.Text = "";
							this._header.CurrentRow["NO_IO"] = "";
						}

						return false;
					}
				}
			}

			if (dtLOT != null)
			{
				foreach (DataRow row in dtLOT.Rows)
				{
					row.AcceptChanges();
					row.SetAdded();
				}
			}
			
			DataTable dtSERL = null;
			
			if (string.Compare(this.MNG_SERIAL, "Y") == 0 && changes2 != null)
			{
				DataRow[] dataRowArray = changes2.Select("NO_SERL = 'YES'", "", DataViewRowState.Added);
				DataTable dt = changes2.Clone();

				if (dataRowArray.Length > 0)
				{
					foreach (DataRow row in dataRowArray)
						dt.ImportRow(row);
					
					P_PU_SERL_SUB_I pPuSerlSubI = new P_PU_SERL_SUB_I(dt);
					
					if (pPuSerlSubI.ShowDialog() == DialogResult.OK)
					{
						dtSERL = pPuSerlSubI.dtL;
					}
					else
					{
						if (this.추가모드여부)
						{
							this.txt수불번호.Text = "";
							this._header.CurrentRow["NO_IO"] = "";
						}
						return false;
					}
				}
			}

			DataTable dt_location = null;

			if (Config.MA_ENV.YN_LOCATION == "Y")
			{
				bool b_return = false;
				DataTable dt = changes2.Clone().Copy();
				
				foreach (DataRow dataRow in changes2.Select())
					dt.LoadDataRow(dataRow.ItemArray, true);
				
				dt_location = P_OPEN_SUBWINDOWS.P_MA_LOCATION_I_R_SUB(dt, out b_return);
				
				if (!b_return)
					return false;
			}

			DataRow[] dataRowArray1 = this._flex.DataTable.Select("FG_SLQC = 'Y'");
			DataTable dtQCl = this._flex.DataTable.Clone();
			
			foreach (DataRow row in dataRowArray1)
				dtQCl.ImportRow(row);
			
			this._biz.biz_cd_plant = D.GetString(this.cbo공장.SelectedValue);
			
			if (!this._biz.Save(changes1, changes2, dtLOT, dtSERL, dt_location, dtQCl))
				return false;
			
			this._header.AcceptChanges();
			this._flex.AcceptChanges();
			this.btn추가.Enabled = true;
			this.ctx출고창고.Enabled = false;
			this.ctx입고창고.Enabled = false;
			this.btn요청적용.Enabled = false;
			
			if (Global.MainFrame.ServerKeyCommon == "TOKIMEC")
				this.btn추가.Enabled = false;
			
			return true;
		}

		protected override bool BeforeSave()
		{
			if (!base.BeforeSave() || !this.Save_Check()) return false;
			if (!this._flex.HasNormalRow) return false;

            bool 누락건존재여부 = false;

            foreach (DataRow dr in this._flex.DataTable.Select(string.Empty, string.Empty, DataViewRowState.Added))
            {
                if (dr["CD_SL_REF"].ToString() == "SL02")
                {
                    this.ShowMessage("저장 로케이션으로는 창고 이동을 진행 할 수 없습니다.\n(입고등록(딘텍)화면 또는 계정대체등록(딘텍) 화면을 사용 하세요)");
                    return false;
                }

				if (dr["CD_SL"].ToString() != "SL01" && dr["CD_SL_REF"].ToString() == "SCRAP")
				{
					this.ShowMessage("폐기재고 S/L으로는 창고 이동을 진행 할 수 없습니다.\n(재고자산폐기관리 화면을 사용 하세요)");
					return false;
				}

				if (string.IsNullOrEmpty(dr["NO_IO_MGMT"].ToString()))
                {
                    if ((dr["CD_SL"].ToString() == "SL01" || dr["CD_SL"].ToString() == "SL02") &&
                        (dr["CD_SL_REF"].ToString() == "LOSS" || dr["CD_SL_REF"].ToString() == "SCRAP"))
                    {
                        누락건존재여부 = true;
                        continue;
                    }
                    else
                    {
                        this.ShowMessage("참조번호가 누락된 건이 있습니다.");
                        return false;
                    }
                }
            }

            if (누락건존재여부)
            {
                if (ShowMessage("참조번호가 누락된 건이 있습니다. 진행 하시겠습니까?\n(입고적용, 출고반품적용 할 수 없는 경우만 진행)", "QY2") != DialogResult.Yes)
                    return false;
            }

			return true;
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if (this.추가모드여부) return;

				DataTable dataTable1 = this._biz.Search_요청(this.txt수불번호.Text);
				string str1 = dataTable1.Rows.Count > 0 ? dataTable1.Rows[0]["NO_GIREQ"].ToString() : "";
				string str2 = dataTable1.Rows.Count > 0 ? dataTable1.Rows[0]["DT_GIREQ"].ToString() : "";
				string str3 = dataTable1.Rows.Count > 0 ? dataTable1.Rows[0]["NM_DEPT"].ToString() : "";
				string str4 = dataTable1.Rows.Count > 0 ? dataTable1.Rows[0]["NM_KOR"].ToString() : "";
				DataTable dataTable2 = new DataTable();
				DataTable table = this._biz.dt_print(this.txt수불번호.Text, Global.MainFrame.ServerKeyCommon).Tables[0];
				ReportHelper reportHelper = new ReportHelper("R_PU_STS_REG2_0", "S/L 이동전표");
				reportHelper.SetData("수불번호", this.txt수불번호.Text);
				reportHelper.SetData("이동일자", this.dtp이동일.Text.Substring(0, 4) + "/" + this.dtp이동일.Text.Substring(4, 2) + "/" + this.dtp이동일.Text.Substring(6, 2));
				reportHelper.SetData("공장코드", this.cbo공장.SelectedValue.ToString());
				reportHelper.SetData("공장명", this.cbo공장.Text);
				reportHelper.SetData("출고창고코드", D.GetString(this.ctx출고창고.CodeValue));
				reportHelper.SetData("출고창고명", D.GetString(this.ctx출고창고.CodeName));
				reportHelper.SetData("입고창고코드", this.ctx입고창고.CodeValue.ToString());
				reportHelper.SetData("입고창고명", this.ctx입고창고.CodeName.ToString());
				reportHelper.SetData("사원코드", this.ctx담당자.CodeValue);
				reportHelper.SetData("사원명", this.ctx담당자.CodeName);
				reportHelper.SetData("수불형태", this.ctx수불형태.CodeName);
				reportHelper.SetData("부서코드", this._header.CurrentRow["CD_DEPT"].ToString());
				reportHelper.SetData("부서명", this._header.CurrentRow["NM_DEPT"].ToString());
				reportHelper.SetData("비고", this.txt비고.Text);
				reportHelper.SetData("요청번호", str1);
				reportHelper.SetData("요청일자", str2);
				reportHelper.SetData("요청부서", str3);
				reportHelper.SetData("요청자", str4);

				if (table != null && table.Rows.Count > 0)
				{
					reportHelper.SetData("작업지시번호", D.GetString(table.Rows[0]["NO_WO"]));
					reportHelper.SetData("작업지시품목", D.GetString(table.Rows[0]["WO_CD_ITEM"]));
					reportHelper.SetData("작업지시품목명", D.GetString(table.Rows[0]["WO_NM_ITEM"]));
					reportHelper.SetData("작업지시품목규격", D.GetString(table.Rows[0]["WO_STND_ITEM"]));
					reportHelper.SetData("작업지시수량", D.GetString(table.Rows[0]["WO_QT_ITEM"]));
				}

				reportHelper.SetDataTable(table);
				reportHelper.Print();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			try
			{
				Settings1.Default.CD_QTIOTP = D.GetString(this.ctx수불형태.CodeValue);
				Settings1.Default.NM_QTIOTP = D.GetString(this.ctx수불형태.CodeName);
				Settings1.Default.Save();

				return base.OnToolBarExitButtonClicked(sender, e);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
				return false;
			}
		}

		private void SetHeaderBinding()
		{
			this.SetBindingToControl((Control)this.oneGrid1, this._dtH.Rows[0]);
		}

		private void SetBindingToControl(Control panel, DataRow row)
		{
			foreach (Control control in (ArrangedElementCollection)panel.Controls)
			{
				if (control is TextBoxExt)
					control.Text = row == null ? string.Empty : row[control.Tag.ToString()].ToString();
				else if (control is DatePicker)
					control.Text = row == null ? string.Empty : row[control.Tag.ToString()].ToString();
				else if (control is DropDownComboBox)
					((ListControl)control).SelectedValue = row == null ? string.Empty : row[control.Tag.ToString()].ToString();
				else if (control is BpCodeTextBox)
				{
					if (row == null)
					{
						((BpControlBase)control).IsEmpty();
					}
					else
					{
						string[] strArray = control.Tag.ToString().Split(Convert.ToChar(";"));
						((BpCodeTextBox)control).SetCodeValue(row[strArray[0]].ToString());
						((BpCodeTextBox)control).SetCodeName(row[strArray[1]].ToString());
					}
				}
				else if (control is CurrencyTextBox)
				{
					if (row == null)
						((CurrencyTextBox)control).DecimalValue = 0;
					else
						((CurrencyTextBox)control).DecimalValue = row[control.Tag.ToString()] == null ? 0 : Convert.ToDecimal(row[control.Tag.ToString()]);
				}
				else if (control is PanelExt)
					this.SetBindingToControl(control, row);
			}
		}

		private void Control_Validated(object sender, EventArgs e)
		{
			try
			{
				if ((Control)sender is TextBoxExt)
				{
					if (!((TextBoxExt)sender).Modified)
						return;
				}
				else if ((Control)sender is DatePicker)
				{
					if (!((DatePicker)sender).Modified)
						return;
				}
				else if ((Control)sender is DropDownComboBox)
				{
					if (!((DropDownComboBox)sender).Modified)
						return;
				}
				else if ((Control)sender is CurrencyTextBox && !((NumberTextBoxBase)sender).Modified)
					return;

				this.SetBindingToDataRow(sender, this._dtH.Rows[0]);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void SetBindingToDataRow(object sender, DataRow row)
		{
			if (row == null) return;

			if ((Control)sender is TextBoxExt)
				row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
			else if ((Control)sender is DatePicker)
				row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;
			else if ((Control)sender is DropDownComboBox)
				row[((Control)sender).Tag.ToString()] = ((ListControl)sender).SelectedValue;
			else if ((Control)sender is CurrencyTextBox)
				row[((Control)sender).Tag.ToString()] = ((Control)sender).Text;

			this.ToolBarSaveButtonEnabled = true;
		}

		private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			try
			{
				switch (this._flex.Cols[e.Col].Name)
				{
					case "CD_ITEM":
						if (D.GetString(this._flex["NO_IO"]) != string.Empty)
						{
							e.Cancel = true;
							break;
						}

						e.Parameter.UserParams = "공장품목;H_CZ_MA_CUSTOMIZE_SUB";
						e.Parameter.P11_ID_MENU = "H_MA_PITEM_SUB";
						e.Parameter.P21_FG_MODULE = "N";
						e.Parameter.P92_DETAIL_SEARCH_CODE = D.GetString(this._flex["CD_ITEM"]);
						break;
					case "출고창고":
						e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
						e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
						int num1 = this.m_sys_set_SL == "000" ? 0 : (!(this.m_sys_set_SL == "001") ? 1 : 0);
						e.Parameter.P07_NO_EMP = num1 != 0 ? (string)null : Global.MainFrame.LoginInfo.EmployeeNo;
						break;
					case "입고창고":
						e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
						e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
						int num2 = this.m_sys_set_SL == "000" ? 0 : (!(this.m_sys_set_SL == "002") ? 1 : 0);
						e.Parameter.P07_NO_EMP = num2 != 0 ? (string)null : Global.MainFrame.LoginInfo.EmployeeNo;
						break;
					case "CD_PJT_ITEM":
						if (D.GetString(this._flex["CD_PROJECT"]) != string.Empty)
						{
							e.Parameter.P64_CODE4 = D.GetString(this._flex["CD_PROJECT"]);
							break;
						}
						break;
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
				FlexGrid flexGrid = sender as FlexGrid;
				HelpReturn result = e.Result;
				DataTable dataTable = flexGrid.DataTable;

				switch (this._flex.Cols[e.Col].Name)
				{
					case "CD_ITEM":
						if (e.Result.DialogResult == DialogResult.Cancel)
							break;

						Decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
						bool flag2 = true;
						this._flex.Redraw = false;
						
						foreach (DataRow row in result.Rows)
						{
							int index;

							if (flag2)
							{
								index = e.Row;
								this._flex[e.Row, "CD_ITEM"] = row["CD_ITEM"];
								this._flex[e.Row, "NM_ITEM"] = row["NM_ITEM"];
								this._flex[e.Row, "STND_ITEM"] = row["STND_ITEM"];
								this._flex[e.Row, "UNIT_IM"] = row["UNIT_IM"];
								this._flex[e.Row, "UNIT_PO"] = row["UNIT_PO"];
								this._flex[e.Row, "CD_ZONE"] = row["CD_ZONE"];
								this._flex[e.Row, "NM_ITEMGRP"] = row["NM_ITEMGRP"];
								this._flex[e.Row, "CLS_ITEM"] = row["CLS_ITEM"];
								this._flex[e.Row, "NM_MAKER"] = row["NM_MAKER"];
								this._flex[e.Row, "NO_DESIGN"] = row["NO_DESIGN"];
								this._flex[e.Row, "NM_CLS_L"] = row["NM_CLS_L"];
								this._flex[e.Row, "NM_CLS_M"] = row["NM_CLS_M"];
								this._flex[e.Row, "NM_CLS_S"] = row["NM_CLS_S"];
								flag2 = false;
							}
							else
							{
								this._flex.Rows.Add();
								this._flex.Row = this._flex.Rows.Count - 1;
								this._flex["CD_ITEM"] = row["CD_ITEM"];
								this._flex["NM_ITEM"] = row["NM_ITEM"];
								this._flex["STND_ITEM"] = row["STND_ITEM"];
								this._flex["UNIT_IM"] = row["UNIT_IM"];
								this._flex["UNIT_PO"] = row["UNIT_PO"];
								this._flex["CD_ZONE"] = row["CD_ZONE"];
								this._flex["NM_ITEMGRP"] = row["NM_ITEMGRP"];
								this._flex["CLS_ITEM"] = row["CLS_ITEM"];
								this._flex["NM_MAKER"] = row["NM_MAKER"];
								this._flex["NO_DESIGN"] = row["NO_DESIGN"];
								this._flex[e.Row, "NM_CLS_L"] = row["NM_CLS_L"];
								this._flex[e.Row, "NM_CLS_M"] = row["NM_CLS_M"];
								this._flex[e.Row, "NM_CLS_S"] = row["NM_CLS_S"];
								index = this._flex.Rows.Count - 1;
							}

							this._flex["FG_SERNO"] = !(D.GetString(row["FG_SERNO"]) == "002") ? (!(D.GetString(row["FG_SERNO"]) == "003") ? this.DD("미관리") : "S/N") : "LOT";
							this._flex[index, "CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
							this._flex[index, "CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
							this._flex[index, "DT_IO"] = this._header.CurrentRow["DT_IO"];
							this._flex[index, "YN_RETURN"] = "N";
							this._flex[index, "YN_AM"] = this._header.CurrentRow["YN_AM"];
							this._flex[index, "S"] = "N";
							
							if (this.MNG_LOT == "Y" && D.GetString(row["FG_SERNO"]) == "002")
							{
								this._flex[index, "NO_LOT"] = "YES";
							}
							else
							{
								this._flex[index, "NO_LOT"] = "NO";
								int num = !(this.MNG_SERIAL == "Y") ? 1 : (!(D.GetString(row["FG_SERNO"]) == "003") ? 1 : 0);
								this._flex[index, "NO_SERL"] = num != 0 ? "NO" : "YES";
							}

							this._flex[index, "NM_QTIOTP"] = this.ctx수불형태.CodeName;
							this._flex[index, "NO_ISURCV"] = "";
							this._flex[index, "NO_ISURCVLINE"] = 0;

							if (D.GetString(this.ctx출고창고.CodeValue) != string.Empty)
							{
								this._flex[index, "CD_SL"] = D.GetString(this.ctx출고창고.CodeValue);
								this._flex[index, "출고창고"] = D.GetString(this.ctx출고창고.CodeName);
							}
							else
							{
								this._flex[index, "CD_SL"] = row["CD_GISL"];
								this._flex[index, "출고창고"] = row["NM_GISL"];
							}

							if (D.GetString(this.ctx입고창고.CodeValue) != string.Empty)
							{
								this._flex[index, "CD_SL_REF"] = this.ctx입고창고.CodeValue.ToString();
								this._flex[index, "입고창고"] = this.ctx입고창고.CodeName.ToString();
							}
							else
							{
								this._flex[index, "CD_SL_REF"] = row["CD_SL"];
								this._flex[index, "입고창고"] = row["NM_SL"];
							}

							this._flex[index, "FG_SLQC"] = !(D.GetString(this._flex["CD_SL_REF"]) != string.Empty) ? this._biz.FG_SLQC("ITEM", D.GetString(row["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue), "") : (!(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), D.GetString(this._flex["CD_SL_REF"])) == "Y") ? "N" : this._biz.FG_SLQC("ITEM", D.GetString(row["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue), ""));
							Decimal num2 = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[index, "CD_SL"]), D.GetString(this._flex[index, "CD_ITEM"]), D.GetString(this._flex[index, "CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[index, "SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[index, "CD_SL"]), D.GetString(this._flex[index, "CD_ITEM"]));
							this._flex[index, "QT_GOODS"] = D.GetDecimal(num2);
							this._flex[index, "BARCODE"] = D.GetString(row["BARCODE"]);
							this._flex[index, "UNIT_PO_FACT"] = D.GetString(row["UNIT_PO_FACT"]);
							
							if (Global.MainFrame.ServerKeyCommon == "SANSUNG")
								this._flex[index, "NUM_STND_ITEM_1"] = D.GetDecimal(row["NUM_STND_ITEM_1"]);
							
							this._flex[index, "STND_DETAIL_ITEM"] = D.GetString(row["STND_DETAIL_ITEM"]);
							this._flex[index, "MAT_ITEM"] = D.GetString(row["MAT_ITEM"]);
							this._flex[index, "NM_GRP_MFG"] = D.GetString(row["NM_GRP_MFG"]);
							maxValue += 2;
						}

						this._flex.Select(e.Row, this._flex.Cols.Fixed);
						this._flex.Redraw = true;

						break;
					case "출고창고":
						if (this._flex.RowState() != DataRowState.Added)
						{
							e.Cancel = true;
							break;
						}

						this._flex["CD_SL"] = e.Result.Rows[0]["CD_SL"];
						this._flex["출고창고"] = e.Result.Rows[0]["NM_SL"];
						this._flex["QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
						break;
					case "입고창고":
						if (this._flex.RowState() != DataRowState.Added)
						{
							e.Cancel = true;
							break;
						}

						this._flex["CD_SL_REF"] = e.Result.Rows[0]["CD_SL"];
						this._flex["입고창고"] = e.Result.Rows[0]["NM_SL"];
						this._flex["FG_SLQC"] = !(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), D.GetString(this._flex["CD_SL_REF"])) == "Y") ? "N" : this._biz.FG_SLQC("ITEM", D.GetString(this._flex["CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue), "");
						break;
					case "CD_SL":
						if (this._flex.RowState() != DataRowState.Added)
						{
							e.Cancel = true;
							break;
						}

						this._flex["CD_PROJECT"] = e.Result.Rows[0]["NO_PROJECT"];
						this._flex["NM_PROJECT"] = e.Result.Rows[0]["NM_PROJECT"];
						this._flex["QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Grid_HelpClick(object sender, EventArgs e)
		{
			try
			{
				FlexGrid flexGrid = sender as FlexGrid;
				if (flexGrid == null) return;

				switch (flexGrid.Cols[flexGrid.Col].Name)
				{
					case "CD_SL":
						P_MA_SL_AUTH_SUB pMaSlAuthSub = new P_MA_SL_AUTH_SUB();
						
						if (pMaSlAuthSub.ShowDialog() == DialogResult.OK)
						{
							flexGrid["CD_SL"] = pMaSlAuthSub.returnParams[0];
							flexGrid["NM_SL"] = pMaSlAuthSub.returnParams[1];
							break;
						}
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
			if (this._flex.Cols[e.Col].Name != "S" && D.GetString(this._header.CurrentRow["NO_QC"]) != "")
			{
				this.ShowMessage("검사처리 항목은 수정 할 수 없습니다. 검사번호 : " + D.GetString(this._header.CurrentRow["NO_QC"]));
				e.Cancel = true;
			}
			else
			{
				switch (this._flex.Cols[e.Col].Name)
				{
					case "QT_GIREQ":
						e.Cancel = true;
						break;
					case "CD_ITEM":
						if (!(D.GetString(this._flex["NO_IO"]) != string.Empty))
							break;

						e.Cancel = true;
						break;
					case "입고창고":
					case "출고창고":
						if (this._flex.RowState() == DataRowState.Added)
							break;

						e.Cancel = true;
						break;
				}
			}
		}

		private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			try
			{
				FlexGrid flexGrid = sender as FlexGrid;
				if (flexGrid == null) return;

				string str = flexGrid.GetData(e.Row, e.Col).ToString();
				string editData = flexGrid.EditData;
				
				if (this.txt수불번호.Text != "" && str.ToUpper() == editData.ToUpper())
					return;
				
				if (flexGrid.Cols[e.Col].Name == "QT_GOOD_INV")
				{
					if (string.Compare(this.MNG_LOT, "Y") == 0 && flexGrid["NO_LOT"].ToString() == "YES" && flexGrid.RowState() != DataRowState.Added)
					{
						this.ShowMessage(" LOT처리대상 - 저장된 이후로는 총수량 수정이 불가합니다. ");

						if (flexGrid.Editor != null)
							flexGrid.Editor.Text = str;

						flexGrid["QT_GOOD_INV"] = str;
					}
					else
					{
						flexGrid["QT_GOOD_INV"] = flexGrid.CDecimal(editData);
						flexGrid["QT_GOOD_OLD"] = flexGrid.CDecimal(str);

						if (Global.MainFrame.ServerKeyCommon == "SAMWON" && D.GetDecimal(flexGrid["QT_UNIT_PO"]) != 0)
							return;
						
						flexGrid["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", flexGrid.CDecimal(editData), D.GetDecimal(flexGrid["UNIT_PO_FACT"]));
						
						if (Global.MainFrame.ServerKeyCommon == "SANSUNG")
						{
							if (D.GetDecimal(flexGrid["NUM_STND_ITEM_1"]) != 0)
								flexGrid["QT_BOX"] = Math.Floor(D.GetDecimal(editData) / D.GetDecimal(flexGrid["NUM_STND_ITEM_1"]));

							flexGrid["QT_SAP"] = (D.GetDecimal(editData) - D.GetDecimal(flexGrid["NUM_STND_ITEM_1"]) * D.GetDecimal(flexGrid["QT_BOX"]));
							flexGrid["QT_BOX_RE"] = (D.GetDecimal(flexGrid["QT_SAP"]) > 0) ? 1 : 0;
						}
					}
				}
				else if (flexGrid.Cols[e.Col].Name == "QT_UNIT_PO")
				{
					if (string.Compare(this.MNG_LOT, "Y") == 0 && flexGrid["NO_LOT"].ToString() == "YES" && flexGrid.RowState() != DataRowState.Added)
					{
						this.ShowMessage(" LOT처리대상 - 저장된 이후로는 총수량 수정이 불가합니다. ");

						if (flexGrid.Editor != null)
							flexGrid.Editor.Text = str;
						
						flexGrid["QT_UNIT_PO"] = str;
					}
					else
					{
						if (Global.MainFrame.ServerKeyCommon == "SAMWON" && D.GetDecimal(flexGrid["QT_GOOD_INV"]) != 0) return;

						flexGrid["QT_GOOD_INV"] = this.calc이동수량("QT_UNIT_PO", flexGrid.CDecimal(editData), D.GetDecimal(flexGrid["UNIT_PO_FACT"]));
						
						if (Global.MainFrame.ServerKeyCommon == "SANSUNG")
						{
							if (D.GetDecimal(flexGrid["NUM_STND_ITEM_1"]) != 0)
								flexGrid["QT_BOX"] = Math.Floor(D.GetDecimal(flexGrid["QT_GOOD_INV"]) / D.GetDecimal(flexGrid["NUM_STND_ITEM_1"]));
							
							flexGrid["QT_SAP"] = (D.GetDecimal(flexGrid["QT_GOOD_INV"]) - D.GetDecimal(flexGrid["NUM_STND_ITEM_1"]) * D.GetDecimal(flexGrid["QT_BOX"]));
							flexGrid["QT_BOX_RE"] = (D.GetDecimal(flexGrid["QT_SAP"]) > 0) ? 1 : 0;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				Control control = (Control)sender;
				switch (e.HelpID)
				{
					case HelpID.P_MA_SL_SUB:
						if (control.Name == this.ctx출고창고.Name || control.Name == this.ctx입고창고.Name)
						{
							if (this.cbo공장.SelectedValue != null && this.cbo공장.SelectedValue.ToString() != "")
							{
								e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();

								if (this.m_sys_set_SL == "000")
									e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
								else if (this.m_sys_set_SL == "001" && control.Name == this.ctx출고창고.Name)
								{
									e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
								}
								else
								{
									int num = !(this.m_sys_set_SL == "002") ? 1 : (!(control.Name == this.ctx입고창고.Name) ? 1 : 0);
									e.HelpParam.P07_NO_EMP = num != 0 ? (string)null : Global.MainFrame.LoginInfo.EmployeeNo;
								}
								break;
							}

							this.ShowMessage("공장을 먼저 선택하십시오");
							this.cbo공장.Focus();
							e.QueryCancel = true;
							break;
						}
						break;
					case HelpID.P_PU_EJTP_SUB:
						e.HelpParam.P61_CODE1 = "022|";
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_QueryAfter(object sender, BpQueryArgs e)
		{
			string name;

			try
			{
				if (e.DialogResult != DialogResult.OK) return;

				BpCodeTextBox bpCodeTextBox = sender as BpCodeTextBox;
				if (bpCodeTextBox == null) return;

				DataRow[] rows = e.HelpReturn.Rows;
				name = ((Control)sender).Name;

				if (name == this.ctx담당자.Name)
				{
					this._header.CurrentRow["CD_DEPT"] = rows[0]["CD_DEPT"];
				}
				else if (name == this.ctx수불형태.Name)
				{
					if (rows[0]["FG_IO"].ToString() == "022")
					{
						this._header.CurrentRow["YN_AM"] = e.HelpReturn.Rows[0]["YN_AM"];
					}
					else
					{
						this.ShowMessage("입력하신 입고형태(" + e.CodeName + "[" + e.CodeValue + "])가 화면에서 지정할 수 없는 수불형태입니다. 다시 입력해주십시오");
						bpCodeTextBox.CodeValue = "";
						bpCodeTextBox.CodeName = "";
						bpCodeTextBox.Focus();
					}
				}
				else if (name == this.ctx프로젝트.Name)
				{
					if (Config.MA_ENV.YN_UNIT == "Y")
					{
						this.d_SEQ_PROJECT = D.GetDecimal(e.HelpReturn.Rows[0]["SEQ_PROJECT"]);
						this.s_CD_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["CD_PJT_ITEM"]);
						this.s_NM_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["NM_PJT_ITEM"]);
						this.s_PJT_ITEM_STND = D.GetString(e.HelpReturn.Rows[0]["PJT_ITEM_STND"]);
					}
				}

				if (this._dtH != null && this._dtH.Rows.Count > 0)
				{
					string[] strArray = ((Control)sender).Tag.ToString().Split(';');
					this._dtH.Rows[0][strArray[0].ToString()] = e.CodeValue;
					this._dtH.Rows[0][strArray[1].ToString()] = e.CodeName;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_CodeChanged(object sender, EventArgs e)
		{
			try
			{
				BpCodeTextBox bpCodeTextBox = sender as BpCodeTextBox;
				if (bpCodeTextBox == null || !(bpCodeTextBox.CodeValue == "") || (this._dtH == null || this._dtH.Rows.Count <= 0)) return;

				string[] strArray = ((Control)sender).Tag.ToString().Split(';');
				this._dtH.Rows[0][strArray[0].ToString()] = string.Empty;
				this._dtH.Rows[0][strArray[1].ToString()] = string.Empty;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (((Control)sender).Name == this.txt비고.Name)
				{
					if (e.KeyData == Keys.Return || e.KeyData == Keys.Tab)
						this.btn추가_Click(null, null);
				}
				else
				{
					if (e.KeyData == Keys.Return)
						SendKeys.SendWait("{TAB}");
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn요청적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Feild_Check("요청")) return;

				P_PU_GIREQ_SUB3 pPuGireqSuB3 = new P_PU_GIREQ_SUB3(this.cbo공장.SelectedValue.ToString(),
																   this.ctx수불형태.CodeValue,
																   this.ctx수불형태.CodeName,
																   this._flex.DataTable);
				
				if (pPuGireqSuB3.ShowDialog() == DialogResult.OK)
				{
					this._flex.DataTable.Clear();
					DataTable dtL = pPuGireqSuB3.dtL;
					string[] strValue = pPuGireqSuB3.str_value;
					this.ctx출고창고.CodeValue = dtL.Rows[0]["CD_SL"].ToString();
					this.ctx출고창고.CodeName = dtL.Rows[0]["NM_SL"].ToString();
					this.ctx입고창고.CodeValue = dtL.Rows[0]["CD_GRSL"].ToString();
					this.ctx입고창고.CodeName = dtL.Rows[0]["NM_GRSL"].ToString();
					DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PU_GIREQ_SUB3");

					if (dtL.Rows.Count > 0)
					{
						this._flex.Redraw = false;
						
						for (int index = 0; index < dtL.Rows.Count; ++index)
						{
							this._flex.Rows.Add();
							this._flex.Row = this._flex.Rows.Count - 1;
							this._flex["CD_ITEM"] = dtL.Rows[index]["CD_ITEM"].ToString();
							this._flex["NM_ITEM"] = dtL.Rows[index]["NM_ITEM"].ToString();
							this._flex["STND_ITEM"] = dtL.Rows[index]["STND_ITEM"].ToString();
							this._flex["UNIT_IM"] = dtL.Rows[index]["UNIT_IM"].ToString();
							this._flex["UNIT_PO"] = dtL.Rows[index]["UNIT_PO"].ToString();
							this._flex["NO_IOLINE"] = this._flex.Row != this._flex.Rows.Fixed ? (this._flex.GetMaxValue("NO_IOLINE") + 2) : 1;
							this._flex["NO_LOT"] = dtL.Rows[index]["NO_LOT"].ToString();
							this._flex["NO_SERL"] = dtL.Rows[index]["NO_SERL"].ToString();
							this._flex["QT_GOOD_INV"] = D.GetDecimal(dtL.Rows[index]["QT"]);
							this._flex["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", D.GetDecimal(dtL.Rows[index]["QT"]), D.GetDecimal(dtL.Rows[index]["UNIT_PO_FACT"]));
							this._flex["NO_ISURCV"] = dtL.Rows[index]["NO_GIREQ"].ToString();
							this._flex["NO_ISURCVLINE"] = dtL.Rows[index]["NO_LINE"].ToString();
							this._flex["CD_SL"] = dtL.Rows[index]["CD_SL"].ToString();
							this._flex["CD_SL_REF"] = dtL.Rows[index]["CD_GRSL"].ToString();
							this._flex["출고창고"] = dtL.Rows[index]["NM_SL"].ToString();
							this._flex["입고창고"] = dtL.Rows[index]["NM_GRSL"].ToString();
							this._flex["CD_PARTNER"] = dtL.Rows[index]["CD_PARTNER"].ToString();
							this._flex["NO_EMP"] = dtL.Rows[index]["NO_EMP"].ToString();
							this._flex["요청자"] = dtL.Rows[index]["NM_KOR"].ToString();
							this._flex["요청부서"] = dtL.Rows[index]["NM_DEPT"].ToString();
							this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
							this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
							this._flex["DT_IO"] = this._header.CurrentRow["DT_IO"];
							this._flex["YN_RETURN"] = "N";
							this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"];
							this._flex["NO_ISURCV"] = dtL.Rows[index]["NO_GIREQ"].ToString();
							this._flex["CD_ZONE"] = D.GetString(dtL.Rows[index]["CD_ZONE"]);
							this._flex["CLS_ITEM"] = D.GetString(dtL.Rows[index]["CLS_ITEM"]);
							this._flex["NM_MAKER"] = D.GetString(dtL.Rows[index]["NM_MAKER"]);
							this._flex["NO_DESIGN"] = D.GetString(dtL.Rows[index]["NO_DESIGN"]);
							this._flex["FG_SLQC"] = !(this.ctx입고창고.CodeName == "") ? (!(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), this.ctx입고창고.CodeValue) == "Y") ? "N" : D.GetString(dtL.Rows[index]["FG_SLQC"])) : D.GetString(dtL.Rows[index]["FG_SLQC"]);
							
							if (strValue[0] == "Y" && D.GetString(dtL.Rows[index]["DC50_PO"]) != "")
							{
								this.txt비고.Text = D.GetString(dtL.Rows[index]["DC50_PO"]);
								this._header.CurrentRow["DC_RMK"] = D.GetString(dtL.Rows[index]["DC50_PO"]);
							}
							
							this._flex["NM_ITEMGRP"] = dtL.Rows[index]["NM_ITEMGRP"].ToString();
							this._flex["FG_SERNO"] = dtL.Rows[index]["FG_SERNO"].ToString();
							this._flex["NO_MREQ"] = D.GetString(dtL.Rows[index]["NO_MREQ"]);
							this._flex["NO_MREQLINE"] = D.GetString(dtL.Rows[index]["NO_MREQLINE"]);
							this._flex["BARCODE"] = D.GetString(dtL.Rows[index]["BARCODE"]);
							this._flex["QT_GIREQ"] = D.GetDecimal(dtL.Rows[index]["QT"]);
							this._flex["FG_GUBUN"] = D.GetString(dtL.Rows[index]["FG_GUBUN"]);
							this._flex["NO_TRACK"] = D.GetString(dtL.Rows[index]["NO_SO"]);
							this._flex["NO_TRACK_LINE"] = D.GetString(dtL.Rows[index]["SEQ_SO"]);
							
							if (this.프로젝트사용)
								this.Only_Pjt(dtL.Rows[index]);
							
							this._flex["QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
							this._flex["GI_PARTNER"] = dtL.Rows[index]["GI_PARTNER"];
							this._flex["LN_GI_PARTNER"] = dtL.Rows[index]["LN_GI_PARTNER"];
							
							if (Global.MainFrame.ServerKeyCommon.Contains("HANSU"))
							{
								this._flex["CD_ITEM_PARTNER"] = dtL.Rows[index]["CD_ITEM_PARTNER"];
								this._flex["NM_ITEM_PARTNER"] = dtL.Rows[index]["NM_ITEM_PARTNER"];
								this._flex["CD_PACK"] = dtL.Rows[index]["CD_PACK"];
								this._flex["NM_PACK"] = dtL.Rows[index]["NM_PACK"];
								this._flex["CD_TRANSPORT"] = dtL.Rows[index]["CD_TRANSPORT"];
								this._flex["CD_PART"] = dtL.Rows[index]["CD_PART"];
								this._flex["NM_PART"] = dtL.Rows[index]["NM_PART"];
								this._flex["YN_TEST_RPT"] = dtL.Rows[index]["YN_TEST_RPT"];
								this._flex["DC_DESTINATION"] = dtL.Rows[index]["DC_DESTINATION"];
								this._flex["DC_RMK_REQ"] = dtL.Rows[index]["DC_RMK"];
								this._flex["DT_DELIVERY"] = dtL.Rows[index]["DT_DELIVERY"];
								this._flex["NUM_USERDEF1"] = dtL.Rows[index]["NUM_USERDEF1"];
								this._flex["CD_SALEGRP"] = dtL.Rows[index]["CD_SALEGRP"];
								this._flex["LN_PARTNER"] = dtL.Rows[index]["LN_PARTNER"];
								this._flex["PRIOR_GUBUN"] = dtL.Rows[index]["PRIOR_GUBUN"];
							}
							else
							{
								this._flex["NM_CUST_DLV"] = dtL.Rows[index]["NM_CUST_DLV"];
								this._flex["NO_TEL_D1"] = dtL.Rows[index]["NO_TEL_D1"];
								this._flex["NO_TEL_D2"] = dtL.Rows[index]["NO_TEL_D2"];
								this._flex["ADDR1"] = dtL.Rows[index]["ADDR1"];
								this._flex["ADDR2"] = dtL.Rows[index]["ADDR2"];
								this._flex["TP_DLV"] = dtL.Rows[index]["TP_DLV"];
								this._flex["DC_REQ"] = dtL.Rows[index]["DC_REQ"];
							}

							if (App.SystemEnv.PMS사용)
							{
								this._flex["CD_CSTR"] = dtL.Rows[index]["CD_CSTR"];
								this._flex["DL_CSTR"] = dtL.Rows[index]["DL_CSTR"];
								this._flex["NM_CSTR"] = dtL.Rows[index]["NM_CSTR"];
								this._flex["SIZE_CSTR"] = dtL.Rows[index]["SIZE_CSTR"];
								this._flex["UNIT_CSTR"] = dtL.Rows[index]["UNIT_CSTR"];
								this._flex["QTY_ACT"] = dtL.Rows[index]["QTY_ACT"];
								this._flex["UNT_ACT"] = dtL.Rows[index]["UNT_ACT"];
								this._flex["AMT_ACT"] = dtL.Rows[index]["AMT_ACT"];
							}
							
							if (Global.MainFrame.ServerKeyCommon == "CNP" || Global.MainFrame.ServerKeyCommon == "SQL_")
							{
								this._flex["AM_REQ"] = dtL.Rows[index]["AM"];
								this._flex["UM_REQ"] = dtL.Rows[index]["UM"];
							}
							
							if (Global.MainFrame.ServerKeyCommon == "SANSUNG")
							{
								this._flex["NUM_STND_ITEM_1"] = dtL.Rows[index]["NUM_STND_ITEM_1"];
								if (D.GetDecimal(this._flex["NUM_STND_ITEM_1"]) != 0)
									this._flex["QT_BOX"] = Math.Floor(D.GetDecimal(this._flex["QT_GOOD_INV"]) / D.GetDecimal(this._flex["NUM_STND_ITEM_1"]));
								this._flex["QT_SAP"] = (D.GetDecimal(this._flex["QT_GOOD_INV"]) - D.GetDecimal(this._flex["NUM_STND_ITEM_1"]) * D.GetDecimal(this._flex["QT_BOX"]));
								this._flex["QT_BOX_RE"] = (D.GetDecimal(this._flex["QT_SAP"]) > 0) ? 1 : 0;
							}
							
							this._flex["STND_DETAIL_ITEM"] = dtL.Rows[index]["STND_DETAIL_ITEM"];
							this._flex["MAT_ITEM"] = dtL.Rows[index]["MAT_ITEM"];
							this._flex["NM_GRP_MFG"] = dtL.Rows[index]["NM_GRP_MFG"];
							this._flex["DT_DELIVERY_IO"] = D.GetString(dtL.Rows[index]["DT_DELIVERY"]);
							this._flex["FG_TRACK"] = "GIR";
							this._flex["NO_PO_PARTNER"] = dtL.Rows[index]["NO_PO_PARTNER"];
							this._flex["DC1_SOL"] = dtL.Rows[index]["DC1_SOL"];
							this._flex["UNIT_PO_FACT"] = dtL.Rows[index]["UNIT_PO_FACT"];
							Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, dtL.Rows[index], this._flex, this._flex.Row);
							this._flex.AddFinished();
						}

						this.fg_sub = "요청";
						this._flex.Redraw = true;
					}

					this.ctx출고창고.Enabled = false;
					this.ctx입고창고.Enabled = false;
					
					if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
					{
						this.dtp이동일.Enabled = false;
						this.ctx수불형태.Enabled = false;
						this.cbo공장.Enabled = false;
						this.ctx담당자.Enabled = false;
					}

					if (Global.MainFrame.ServerKeyCommon == "TOKIMEC")
						this.btn추가.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn외주의뢰적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Feild_Check("외주")) return;

				P_PU_SGIREQ_SUB3 pPuSgireqSuB3 = new P_PU_SGIREQ_SUB3(D.GetString(this.cbo공장.SelectedValue),
																	  D.GetString(this.ctx출고창고.CodeValue),
																	  D.GetString(this.ctx출고창고.CodeName),
																	  this.ctx입고창고.CodeValue,
																	  this.ctx입고창고.CodeName,
																	  this._flex.DataTable);
				if (pPuSgireqSuB3.ShowDialog() == DialogResult.OK)
				{
					this._flex.DataTable.Clear();
					DataTable dtL = pPuSgireqSuB3.dtL;

					if (dtL.Rows.Count > 0)
					{
						this._flex.Redraw = false;

						for (int index = 0; index < dtL.Rows.Count; ++index)
						{
							this._flex.Rows.Add();
							this._flex.Row = this._flex.Rows.Count - 1;
							this._flex["CD_ITEM"] = dtL.Rows[index]["CD_ITEM"].ToString();
							this._flex["NM_ITEM"] = dtL.Rows[index]["NM_ITEM"].ToString();
							this._flex["STND_ITEM"] = dtL.Rows[index]["STND_ITEM"].ToString();
							this._flex["UNIT_IM"] = dtL.Rows[index]["UNIT_IM"].ToString();
							this._flex["UNIT_PO"] = dtL.Rows[index]["UNIT_PO"].ToString();
							this._flex["NO_LOT"] = dtL.Rows[index]["NO_LOT"].ToString();
							this._flex["NO_SERL"] = dtL.Rows[index]["NO_SERL"].ToString();
							this._flex["NO_IOLINE"] = this._flex.Row != this._flex.Rows.Fixed ? (this._flex.GetMaxValue("NO_IOLINE") + 2) : 1;
							this._flex["QT_GOOD_INV"] = this._flex.CDecimal(dtL.Rows[index]["QT_REQ"].ToString());
							this._flex["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", this._flex.CDecimal(dtL.Rows[index]["QT_REQ"].ToString()), this._flex.CDecimal(dtL.Rows[index]["UNIT_PO_FACT"].ToString()));
							this._flex["NO_ISURCV"] = dtL.Rows[index]["NO_GIREQ"].ToString();
							this._flex["NO_ISURCVLINE"] = dtL.Rows[index]["NO_LINE"].ToString();
							this._flex["NO_PSO_MGMT"] = dtL.Rows[index]["NO_PSO_MGMT"].ToString();
							this._flex["NO_PSOLINE_MGMT"] = dtL.Rows[index]["NO_PSOLINE_MGMT"];
							this._flex["CD_SL"] = dtL.Rows[index]["CD_SL"].ToString();
							this._flex["CD_SL_REF"] = dtL.Rows[index]["CD_GRSL"].ToString();
							this._flex["출고창고"] = dtL.Rows[index]["출고창고"].ToString();
							this._flex["입고창고"] = dtL.Rows[index]["입고창고"].ToString();
							this._flex["NO_EMP"] = dtL.Rows[index]["NO_EMP"].ToString();
							this._flex["요청자"] = dtL.Rows[index]["NM_KOR"].ToString();
							this._flex["요청부서"] = dtL.Rows[index]["NM_DEPT"].ToString();
							this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
							this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
							this._flex["DT_IO"] = this._header.CurrentRow["DT_IO"];
							this._flex["YN_RETURN"] = "N";
							this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"];
							this._flex["FG_SERNO"] = dtL.Rows[index]["FG_SERNO"].ToString();
							this._flex["NM_ITEMGRP"] = dtL.Rows[index]["NM_ITEMGRP"].ToString();
							this._flex["QT_GIREQ"] = this._flex.CDecimal(dtL.Rows[index]["QT_GIREQ"].ToString());
							this._flex["BARCODE"] = D.GetString(dtL.Rows[index]["BARCODE"]);
							this._flex["CD_ZONE"] = D.GetString(dtL.Rows[index]["CD_ZONE"]);
							this._flex["CLS_ITEM"] = D.GetString(dtL.Rows[index]["CLS_ITEM"]);
							this._flex["LN_PARTNER"] = D.GetString(dtL.Rows[index]["LN_PARTNER"]);
							this._flex["NM_MAKER"] = D.GetString(dtL.Rows[index]["NM_MAKER"]);
							this._flex["FG_SLQC"] = !(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), dtL.Rows[index]["입고창고"].ToString()) == "Y") ? "N" : D.GetString(dtL.Rows[index]["FG_SLQC"]);
							
							if (this.프로젝트사용)
								this.Only_Pjt(dtL.Rows[index]);
							
							this._flex["QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
							
							if (App.SystemEnv.PMS사용)
							{
								this._flex["CD_CSTR"] = dtL.Rows[index]["CD_CSTR"];
								this._flex["DL_CSTR"] = dtL.Rows[index]["DL_CSTR"];
								this._flex["NM_CSTR"] = dtL.Rows[index]["NM_CSTR"];
								this._flex["SIZE_CSTR"] = dtL.Rows[index]["SIZE_CSTR"];
								this._flex["UNIT_CSTR"] = dtL.Rows[index]["UNIT_CSTR"];
								this._flex["QTY_ACT"] = dtL.Rows[index]["QTY_ACT"];
								this._flex["UNT_ACT"] = dtL.Rows[index]["UNT_ACT"];
								this._flex["AMT_ACT"] = dtL.Rows[index]["AMT_ACT"];
							}

							this._flex["STND_DETAIL_ITEM"] = D.GetString(dtL.Rows[index]["STND_DETAIL_ITEM"]);
							this._flex["MAT_ITEM"] = D.GetString(dtL.Rows[index]["MAT_ITEM"]);
							this._flex["NM_GRP_MFG"] = D.GetString(dtL.Rows[index]["NM_GRP_MFG"]);
							this._flex["FG_TRACK"] = "SU";
							this._flex.AddFinished();
						}

						this.fg_sub = "외주";
						this._flex.Redraw = true;
					}

					this.ctx출고창고.Enabled = true;
					this.ctx입고창고.Enabled = true;
					
					if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
					{
						this.dtp이동일.Enabled = false;
						this.ctx수불형태.Enabled = false;
						this.cbo공장.Enabled = false;
						this.ctx담당자.Enabled = false;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn생산의뢰적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Feild_Check("생산")) return;

				P_PU_WGIREQ02_SUB pPuWgireQ02Sub = new P_PU_WGIREQ02_SUB(new string[] { this.cbo공장.SelectedValue.ToString(),
																						this.cbo공장.Text,
																						this.ctx출고창고.CodeValue,
																						this.ctx출고창고.CodeName,
																						this.ctx입고창고.CodeValue,
																						this.ctx입고창고.CodeName });

				if (pPuWgireQ02Sub.ShowDialog() == DialogResult.OK)
				{
					this._flex.DataTable.Clear();
					DataRow[] returnDataRowArr = pPuWgireQ02Sub.ReturnDataRowArr;
					this._flex.Redraw = false;
					
					foreach (DataRow dr in returnDataRowArr)
					{
						this._flex.Rows.Add();
						this._flex.Row = this._flex.Rows.Count - 1;
						this._flex["CD_ITEM"] = dr["CD_ITEM"].ToString();
						this._flex["NM_ITEM"] = dr["NM_ITEM"].ToString();
						this._flex["STND_ITEM"] = dr["STND_ITEM"].ToString();
						this._flex["UNIT_IM"] = dr["UNIT_MO"].ToString();
						this._flex["UNIT_PO"] = dr["UNIT_PO"].ToString();
						this._flex["NO_LOT"] = D.GetString(dr["NO_LOT"]);
						this._flex["NO_SERL"] = D.GetString(dr["NO_SERL"]);
						this._flex["NO_IOLINE"] = this._flex.Row != this._flex.Rows.Fixed ? (this._flex.GetMaxValue("NO_IOLINE") + 2) : 1;
						this._flex["QT_GOOD_INV"] = this._flex.CDecimal(dr["QT_REQ"].ToString());
						this._flex["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", this._flex.CDecimal(dr["QT_REQ"].ToString()), this._flex.CDecimal(dr["UNIT_PO_FACT"].ToString()));
						this._flex["NO_ISURCV"] = dr["NO_GIREQ"].ToString();
						this._flex["NO_ISURCVLINE"] = dr["NO_LINE"].ToString();
						this._flex["NO_PSO_MGMT"] = dr["NO_PSO_MGMT"].ToString();
						this._flex["NO_PSOLINE_MGMT"] = dr["NO_PSOLINE_MGMT"].ToString();
						this._flex["CD_SL"] = dr["CD_SL"].ToString();
						this._flex["CD_SL_REF"] = dr["CD_GRSL"].ToString();
						this._flex["출고창고"] = dr["출고창고"].ToString();
						this._flex["입고창고"] = dr["입고창고"].ToString();
						this._flex["NO_EMP"] = dr["NO_EMP"].ToString();
						this._flex["요청자"] = dr["NM_KOR"].ToString();
						this._flex["요청부서"] = dr["NM_DEPT"].ToString();
						this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
						this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
						this._flex["DT_IO"] = this._header.CurrentRow["DT_IO"];
						this._flex["YN_RETURN"] = "N";
						this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"];
						this._flex["FG_SERNO"] = dr["FG_SERNO"].ToString();
						this._flex["NM_ITEMGRP"] = dr["NM_ITEMGRP"].ToString();
						this._flex["BARCODE"] = D.GetString(dr["BARCODE"]);
						this._flex["CD_ZONE"] = D.GetString(dr["CD_ZONE"]);
						this._flex["CLS_ITEM"] = D.GetString(dr["CLS_ITEM"]);
						this._flex["NM_MAKER"] = D.GetString(dr["NM_MAKER"]);
						this._flex["QT_GIREQ"] = D.GetString(dr["QT_GIREQ"]);
						this._flex["FG_SLQC"] = !(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), dr["입고창고"].ToString()) == "Y") ? "N" : D.GetString(dr["FG_SLQC"]);
						
						if (this.프로젝트사용)
							this.Only_Pjt(dr);
						
						this._flex["QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
						this._flex["DC_RMK"] = D.GetString(dr["DC_RMK"]);
						this._flex["NM_CLS_L"] = D.GetString(dr["NM_CLS_L"]);
						this._flex["NM_CLS_M"] = D.GetString(dr["NM_CLS_M"]);
						this._flex["NM_CLS_S"] = D.GetString(dr["NM_CLS_S"]);
						
						if (App.SystemEnv.PMS사용)
						{
							this._flex["CD_CSTR"] = dr["CD_CSTR"];
							this._flex["DL_CSTR"] = dr["DL_CSTR"];
							this._flex["NM_CSTR"] = dr["NM_CSTR"];
							this._flex["SIZE_CSTR"] = dr["SIZE_CSTR"];
							this._flex["UNIT_CSTR"] = dr["UNIT_CSTR"];
							this._flex["QTY_ACT"] = dr["QTY_ACT"];
							this._flex["UNT_ACT"] = dr["UNT_ACT"];
							this._flex["AMT_ACT"] = dr["AMT_ACT"];
						}
						
						this._flex["STND_DETAIL_ITEM"] = D.GetString(dr["STND_DETAIL_ITEM"]);
						this._flex["MAT_ITEM"] = D.GetString(dr["MAT_ITEM"]);
						this._flex["NM_GRP_MFG"] = D.GetString(dr["NM_GRP_MFG"]);
						this._flex["FG_TRACK"] = "PR";
						this._flex.AddFinished();
					}

					this.fg_sub = "생산";
					this.의뢰번호Visible(this.fg_sub);
					this.ctx출고창고.Enabled = true;
					this.ctx입고창고.Enabled = true;
					
					if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
					{
						this.dtp이동일.Enabled = false;
						this.ctx수불형태.Enabled = false;
						this.cbo공장.Enabled = false;
						this.ctx담당자.Enabled = false;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex.Redraw = true;
			}
		}

		private void btnD2계획적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Feild_Check("요청")) return;

				P_PU_D2_PLAN_SUB pPuD2PlanSub = new P_PU_D2_PLAN_SUB();
				
				if (pPuD2PlanSub.ShowDialog() == DialogResult.OK)
				{
					this._flex.DataTable.Clear();
					this.Grid_BOMAfter(pPuD2PlanSub.dt_reutrn, "CD_ITEM");
					
					if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
					{
						this.dtp이동일.Enabled = false;
						this.ctx수불형태.Enabled = false;
						this.cbo공장.Enabled = false;
						this.ctx담당자.Enabled = false;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn출고증요청적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Feild_Check("요청")) return;
				P_PU_GIREQ_SUB3 pPuGireqSuB3 = new P_PU_GIREQ_SUB3(this.cbo공장.SelectedValue.ToString(),
																   this.ctx수불형태.CodeValue,
																   this.ctx수불형태.CodeName,
																   this._flex.DataTable,
																   "DONGWOON2");
				
				if (pPuGireqSuB3.ShowDialog() == DialogResult.OK)
				{
					this._flex.DataTable.Clear();
					DataTable dtL = pPuGireqSuB3.dtL;
					string[] strValue = pPuGireqSuB3.str_value;
					this.ctx출고창고.CodeValue = dtL.Rows[0]["CD_SL"].ToString();
					this.ctx출고창고.CodeName = dtL.Rows[0]["NM_SL"].ToString();
					this.ctx입고창고.CodeValue = dtL.Rows[0]["CD_GRSL"].ToString();
					this.ctx입고창고.CodeName = dtL.Rows[0]["NM_GRSL"].ToString();
					DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PU_GIREQ_SUB3");

					if (dtL.Rows.Count > 0)
					{
						this._flex.Redraw = false;
						
						for (int index = 0; index < dtL.Rows.Count; ++index)
						{
							this._flex.Rows.Add();
							this._flex.Row = this._flex.Rows.Count - 1;
							this._flex["CD_ITEM"] = dtL.Rows[index]["CD_ITEM"].ToString();
							this._flex["NM_ITEM"] = dtL.Rows[index]["NM_ITEM"].ToString();
							this._flex["STND_ITEM"] = dtL.Rows[index]["STND_ITEM"].ToString();
							this._flex["UNIT_IM"] = dtL.Rows[index]["UNIT_IM"].ToString();
							this._flex["UNIT_PO"] = dtL.Rows[index]["UNIT_PO"].ToString();
							this._flex["NO_IOLINE"] = this._flex.Row != this._flex.Rows.Fixed ? (this._flex.GetMaxValue("NO_IOLINE") + 2) : 1;
							this._flex["NO_LOT"] = dtL.Rows[index]["NO_LOT"].ToString();
							this._flex["NO_SERL"] = dtL.Rows[index]["NO_SERL"].ToString();
							this._flex["QT_GOOD_INV"] = D.GetDecimal(dtL.Rows[index]["QT"]);
							this._flex["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", D.GetDecimal(dtL.Rows[index]["QT"]), D.GetDecimal(dtL.Rows[index]["UNIT_PO_FACT"]));
							this._flex["NO_ISURCV"] = "";
							this._flex["NO_ISURCVLINE"] = "0";
							this._flex["CD_USERDEF1_QTIO"] = dtL.Rows[index]["NO_GIREQ"].ToString();
							this._flex["NUM_USERDEF1_QTIO"] = dtL.Rows[index]["NO_LINE"].ToString();
							this._flex["CD_SL"] = dtL.Rows[index]["CD_SL"].ToString();
							this._flex["CD_SL_REF"] = dtL.Rows[index]["CD_GRSL"].ToString();
							this._flex["출고창고"] = dtL.Rows[index]["NM_SL"].ToString();
							this._flex["입고창고"] = dtL.Rows[index]["NM_GRSL"].ToString();
							this._flex["CD_PARTNER"] = dtL.Rows[index]["CD_PARTNER"].ToString();
							this._flex["NO_EMP"] = dtL.Rows[index]["NO_EMP"].ToString();
							this._flex["요청자"] = dtL.Rows[index]["NM_KOR"].ToString();
							this._flex["요청부서"] = dtL.Rows[index]["NM_DEPT"].ToString();
							this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
							this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
							this._flex["DT_IO"] = this._header.CurrentRow["DT_IO"];
							this._flex["YN_RETURN"] = "N";
							this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"];
							this._flex["CD_ZONE"] = D.GetString(dtL.Rows[index]["CD_ZONE"]);
							this._flex["CLS_ITEM"] = D.GetString(dtL.Rows[index]["CLS_ITEM"]);
							this._flex["NM_MAKER"] = D.GetString(dtL.Rows[index]["NM_MAKER"]);
							this._flex["NO_DESIGN"] = D.GetString(dtL.Rows[index]["NO_DESIGN"]);
							this._flex["FG_SLQC"] = !(this.ctx입고창고.CodeName == "") ? (!(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), this.ctx입고창고.CodeValue) == "Y") ? "N" : D.GetString(dtL.Rows[index]["FG_SLQC"])) : D.GetString(dtL.Rows[index]["FG_SLQC"]);
							
							if (strValue[0] == "Y" && D.GetString(dtL.Rows[index]["DC50_PO"]) != "")
							{
								this.txt비고.Text = D.GetString(dtL.Rows[index]["DC50_PO"]);
								this._header.CurrentRow["DC_RMK"] = D.GetString(dtL.Rows[index]["DC50_PO"]);
							}
							
							this._flex["NM_ITEMGRP"] = dtL.Rows[index]["NM_ITEMGRP"].ToString();
							this._flex["FG_SERNO"] = dtL.Rows[index]["FG_SERNO"].ToString();
							this._flex["NO_MREQ"] = D.GetString(dtL.Rows[index]["NO_MREQ"]);
							this._flex["NO_MREQLINE"] = D.GetString(dtL.Rows[index]["NO_MREQLINE"]);
							this._flex["BARCODE"] = D.GetString(dtL.Rows[index]["BARCODE"]);
							this._flex["QT_GIREQ"] = D.GetDecimal(dtL.Rows[index]["QT"]);
							this._flex["FG_GUBUN"] = D.GetString(dtL.Rows[index]["FG_GUBUN"]);
							this._flex["NO_TRACK"] = D.GetString(dtL.Rows[index]["NO_SO"]);
							this._flex["NO_TRACK_LINE"] = D.GetString(dtL.Rows[index]["SEQ_SO"]);
							
							if (this.프로젝트사용)
								this.Only_Pjt(dtL.Rows[index]);
							
							this._flex["QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
							this._flex["GI_PARTNER"] = dtL.Rows[index]["GI_PARTNER"];
							this._flex["LN_GI_PARTNER"] = dtL.Rows[index]["LN_GI_PARTNER"];
							this._flex["NM_CUST_DLV"] = dtL.Rows[index]["NM_CUST_DLV"];
							this._flex["NO_TEL_D1"] = dtL.Rows[index]["NO_TEL_D1"];
							this._flex["NO_TEL_D2"] = dtL.Rows[index]["NO_TEL_D2"];
							this._flex["ADDR1"] = dtL.Rows[index]["ADDR1"];
							this._flex["ADDR2"] = dtL.Rows[index]["ADDR2"];
							this._flex["TP_DLV"] = dtL.Rows[index]["TP_DLV"];
							this._flex["DC_REQ"] = dtL.Rows[index]["DC_REQ"];
							this._flex["STND_DETAIL_ITEM"] = dtL.Rows[index]["STND_DETAIL_ITEM"];
							this._flex["MAT_ITEM"] = dtL.Rows[index]["MAT_ITEM"];
							this._flex["NM_GRP_MFG"] = dtL.Rows[index]["NM_GRP_MFG"];
							this._flex["DT_DELIVERY_IO"] = D.GetString(dtL.Rows[index]["DT_DELIVERY"]);
							this._flex["UNIT_PO_FACT"] = dtL.Rows[index]["UNIT_PO_FACT"];
							Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, dtL.Rows[index], this._flex, this._flex.Row);
							this._flex.AddFinished();
						}

						this.fg_sub = "출고증요청";
						this._flex.Redraw = true;
					}

					this.ctx출고창고.Enabled = false;
					this.ctx입고창고.Enabled = false;
					
					if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
					{
						this.dtp이동일.Enabled = false;
						this.ctx수불형태.Enabled = false;
						this.cbo공장.Enabled = false;
						this.ctx담당자.Enabled = false;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn수주적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Feild_Check("요청")) return;

				P_PU_PO_SO_SUB pPuPoSoSub = new P_PU_PO_SO_SUB(new object[] { D.GetString(this.cbo공장.SelectedValue),
																			  D.GetString( this.cbo공장.Text),
																			  "SL",
																			  this._flex.DataTable });

				if (pPuPoSoSub.ShowDialog() == DialogResult.OK)
				{
					this._flex.DataTable.Clear();
					this.Grid_BOMAfter(pPuPoSoSub.수주데이터, "CD_ITEM");
					this.ToolBarDeleteButtonEnabled = false;

					if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
					{
						this.dtp이동일.Enabled = false;
						this.ctx수불형태.Enabled = false;
						this.cbo공장.Enabled = false;
						this.ctx담당자.Enabled = false;
					}
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
				if (this.추가모드여부 || D.GetString(this._flex["ID_MEMO"]) == string.Empty) return;

				new P_WS_PM_S_JOBSHARE_SUB1(this, D.GetString(this._flex["ID_MEMO"]), new object[] { "A15",
																									 "",
																									 Global.MainFrame.LoginInfo.CompanyCode,
																									 D.GetString(this._flex["CD_PROJECT"]),
																									 D.GetString(this._flex["CD_WBS"]),
																									 D.GetString(this._flex["NO_SHARE"]),
																									 D.GetString(this._flex["NO_ISSUE"]),
																									 "04" }).ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.btn추가.Enabled || !this.Feild_Check("추가")) return;

				this._flex.Rows.Add();
				this._flex.Row = this._flex.Rows.Count - 1;
				this._flex["S"] = "N";
				this._flex["CD_ITEM"] = "";
				this._flex["QT_GOOD_INV"] = 0;
				this._flex["QT_REJECT_INV"] = 0;
				this._flex["NO_ISURCV"] = "";
				this._flex["NO_ISURCVLINE"] = 0;
				
				if (D.GetString(this.ctx출고창고.CodeValue) != string.Empty)
				{
					this._flex["CD_SL"] = D.GetString(this.ctx출고창고.CodeValue);
					this._flex["출고창고"] = D.GetString(this.ctx출고창고.CodeName);
				}
				
				if (this.ctx입고창고.CodeValue.ToString() != string.Empty)
				{
					this._flex["CD_SL_REF"] = this.ctx입고창고.CodeValue.ToString();
					this._flex["입고창고"] = this.ctx입고창고.CodeName.ToString();
				}
				
				if (this.PJT형여부)
					this._flex["SEQ_PROJECT"] = 0;
				
				this._flex.AddFinished();
				this._flex.Col = this._flex.Cols.Fixed;
				this._flex.Focus();
				this.ToolBarSaveButtonEnabled = true;
				this.ToolBarAddButtonEnabled = true;
				this.dtp이동일.Enabled = false;
				this.btn삭제.Enabled = true;
				this.btn요청적용.Enabled = false;
				this.btn외주의뢰적용.Enabled = false;
				this.btn생산의뢰적용.Enabled = false;
				this.ctx수불형태.Enabled = false;
				this.cbo공장.Enabled = false;
				this.ctx담당자.Enabled = false;
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
				if (D.GetString(this._header.CurrentRow["NO_QC"]) != "")
				{
					this.ShowMessage("검사처리 항목은 삭제할 수 없습니다. 검사번호 : " + D.GetString(this._header.CurrentRow["NO_QC"]));
				}
				else
				{
					DataRow[] dataRowArray = this._flex.DataTable.Select("S ='Y'");
					if (dataRowArray == null || dataRowArray.Length <= 0)
					{
						this.ShowMessage(공통메세지.선택된자료가없습니다);
					}
					else
					{
						if (Global.MainFrame.ServerKeyCommon.Contains("PIOLINK"))
						{
							for (int index = this._flex.Rows.Count - 1; index >= this._flex.Rows.Fixed; --index)
							{
								if (D.GetString(this._flex[index, "NO_IO"]) != string.Empty && D.GetString(this._flex[index, "S"]) == "Y")
								{
									DataTable dataTable = this._biz.SaveCheck(D.GetString(this._flex[index, "NO_IO"]), D.GetString(this._flex[index, "NO_IOLINE"]));
									if (dataTable != null && dataTable.Rows.Count > 0)
									{
										if (this.ShowMessage("해당시리얼의 출고이력이 존재합니다. \n" + ("(수불유형 : " + D.GetString(dataTable.Rows[0]["NM_QTIOTP"]) + ", 수불번호 : " + D.GetString(dataTable.Rows[0]["NO_IO"]) + ")") + " 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
											return;
									}
								}
							}
						}

						this._flex.Redraw = false;
						
						if (dataRowArray != null & dataRowArray.Length > 0)
						{
							for (int index = 0; index < dataRowArray.Length; ++index)
								dataRowArray[index].Delete();
						}

						this._flex.Redraw = true;
						
						if (this._flex.DataView.Count <= 0)
						{
							this.btn추가.Enabled = true;
							this.btn요청적용.Enabled = true;
							this.btn외주의뢰적용.Enabled = true;
							this.btn생산의뢰적용.Enabled = true;
							this.btn삭제.Enabled = false;
							this.ToolBarSaveButtonEnabled = false;
							this.dtp이동일.Enabled = true;
							this.ctx수불형태.Enabled = true;
							this.cbo공장.Enabled = true;
							this.ctx담당자.Enabled = true;
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
				this._flex.Redraw = true;
			}
		}

		private void btn엑셀업로드_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.Feild_Check("요청")) return;

				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
				
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string fileName = openFileDialog.FileName;
					string empty1 = string.Empty;
					string sPipe = string.Empty;
					DataTable dataTable1 = new Excel().StartLoadExcel(fileName);
					int num1 = this._flex.Rows.Count - this._flex.Rows.Fixed;
					MsgControl.ShowMsg(" 엑셀 업로드 중입니다. \n잠시만 기다려주세요!");
					this._flex.Redraw = false;
					DataTable dataTable2 = new DataTable();
					DataTable dataTable3 = new DataTable();
					DataTable dataTable4 = this._flex.DataTable;
					bool flag1 = true;
					string multi_sl = string.Empty;
					DataTable dataTable5 = null;
					string str1 = string.Empty;
					bool flag2 = true;
					string str2 = string.Empty;
					string empty2 = string.Empty;
					string NO_KEY = string.Empty;
					bool flag3 = dataTable1.Columns.Contains("CD_ITEM_PART");
					bool flag4 = dataTable1.Columns.Contains("NO_PROJECT");
					bool flag5 = dataTable1.Columns.Contains("SEQ_PROJECT");

					if (flag3)
					{
						foreach (DataRow row in dataTable1.Rows)
						{
							if (!(D.GetString(row["CD_ITEM_PART"]) == string.Empty))
							{
								if (!dataTable1.Columns.Contains("CD_PARTNER") || D.GetString(row["CD_PARTNER"]) == string.Empty)
								{
									int num3 = (int)this.ShowMessage(this.DD("엑셀자료에 거래처가 없는 건이 있습니다.\n"));
									this._flex.Redraw = true;
									return;
								}
								multi_sl = multi_sl + D.GetString(row["CD_ITEM_PART"]) + "|";
							}
						}

						if (!dataTable1.Columns.Contains("CD_ITEM"))
							dataTable1.Columns.Add("CD_ITEM");
						
						dataTable5 = this._biz.Search_ITEMPART(multi_sl);
						
						if (dataTable5 == null || dataTable5.Rows.Count == 0)
						{
							this.ShowMessage(this.DD("거래처 품목을 확인 하세요.\n"));
							this._flex.Redraw = true;
							return;
						}
					}

					foreach (DataRow row in dataTable1.Rows)
					{
						if (flag4)
							NO_KEY = NO_KEY + D.GetString(row["NO_PROJECT"]) + "|";

						if (flag3)
						{
							DataRow[] dataRowArray = dataTable5.Select("CD_ITEM_PART='" + D.GetString(row["CD_ITEM_PART"]) + "' AND CD_PARTNER ='" + D.GetString(row["CD_PARTNER"]) + "'");
							
							if (dataRowArray == null || dataRowArray.Length == 0)
							{
								flag1 = false;
								str1 = str1 + "\n거래처품목코드 : " + D.GetString(row["CD_ITEM_PART"]) + " &     거래처 : " + D.GetString(row["CD_PARTNER"]);
								continue;
							}
							
							row["CD_ITEM"] = D.GetString(dataRowArray[0]["CD_ITEM"]);
						}

						if (empty1 != row["CD_ITEM"].ToString())
						{
							empty1 = row["CD_ITEM"].ToString();
							sPipe = sPipe + empty1 + "|";
						}
					}

					string[] pipes = D.StringConvert.GetPipes(sPipe, 200);
					
					for (int index = 0; index < pipes.Length; ++index)
					{
						DataTable dataTable6 = this._biz.Item_List_search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 this.cbo공장.SelectedValue,
																						 pipes[index],
																						 Global.SystemLanguage.MultiLanguageLpoint });
						if (index == 0)
							dataTable3 = dataTable6.Copy();
						else if (index > 0)
						{
							foreach (DataRow row in dataTable6.Rows)
								dataTable3.LoadDataRow(row.ItemArray, true);
						}
					}

					DataTable dataTable7 = this._biz.CD_SL_search(D.GetString(this.cbo공장.SelectedValue));
					this._dt_pjt = this._biz.SearchPJT(NO_KEY);
					StringBuilder stringBuilder = new StringBuilder();
					string str3 = "품목코드 \t  프로젝트  \t  출고창고 \t 입고창고";
					stringBuilder.AppendLine(str3);
					string str4 = "-".PadRight(80, '-');
					stringBuilder.AppendLine(str4);
					bool flag6 = false;
					bool flag7 = false;
					bool flag8 = dataTable1.Columns.Contains("DC_RMK");
					bool flag9 = dataTable1.Columns.Contains("DC_RMK1");
					Decimal num4 = this._flex.GetMaxValue("NO_IOLINE") == 0 ? -1 : this._flex.GetMaxValue("NO_IOLINE");
					
					foreach (DataRow row1 in dataTable1.Rows)
					{
						if (!(D.GetString(row1["CD_ITEM"]) == ""))
						{
							DataRow[] dataRowArray1 = dataTable3.Select("CD_ITEM = '" + D.GetString(row1["CD_ITEM"]) + "'");
							DataRow[] dataRowArray2 = dataTable7.Select("CD_SL = '" + D.GetString(row1["GI_SL"]) + "'");
							DataRow[] dataRowArray3 = dataTable7.Select("CD_SL = '" + D.GetString(row1["GR_SL"]) + "'");
							DataRow[] dataRowArray4 = null;

							if (flag4 && D.GetString(row1["NO_PROJECT"]) != "")
							{
								DataRow[] dataRowArray5 = null;
								
								if (Config.MA_ENV.YN_UNIT != "Y")
									dataRowArray5 = this._dt_pjt.Select("NO_PROJECT = '" + D.GetString(row1["NO_PROJECT"]) + "'");
								
								if (Config.MA_ENV.YN_UNIT == "Y" && flag5)
								{
									dataRowArray4 = this._dt_pjt.Select("NO_PROJECT = '" + D.GetString(row1["NO_PROJECT"]) + "' AND SEQ_PROJECT = " + D.GetDecimal(row1["SEQ_PROJECT"]) + " AND CD_PJT_ITEM ='" + D.GetString(row1["CD_UNIT"]) + "'");
									
									if (dataRowArray4 == null || dataRowArray4.Length == 0)
									{
										str2 = str2 + "\n프로젝트번호 : " + D.GetString(row1["NO_PROJECT"]) + " &     UNIT 항번 : " + D.GetString(row1["SEQ_PROJECT"]) + " &     UNIT 코드 : " + D.GetString(row1["CD_UNIT"]);
										flag2 = false;
									}
								}
								else if (dataRowArray5 == null || dataRowArray5.Length == 0)
								{
									str2 = str2 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]) + " &     PJT번호 : " + D.GetString(row1["NO_PROJECT"]);
									flag2 = false;
									continue;
								}
							}

							if (dataRowArray1.Length > 0 && dataRowArray2.Length > 0 && dataRowArray3.Length > 0)
							{
								num4 += 2;
								DataRow row2 = dataTable4.NewRow();
								row2["S"] = "N";
								row2["NO_IO"] = D.GetString(this.txt수불번호.Text);
								row2["NO_IOLINE"] = num4;
								row2["CD_ITEM"] = row1["CD_ITEM"].ToString().Trim().ToUpper();
								row2["NM_ITEM"] = D.GetString(dataRowArray1[0]["NM_ITEM"]);
								row2["STND_ITEM"] = dataRowArray1[0]["STND_ITEM"];
								row2["UNIT_IM"] = dataRowArray1[0]["UNIT_IM"];
								row2["UNIT_PO"] = dataRowArray1[0]["UNIT_PO"];
								row2["NM_MAKER"] = dataRowArray1[0]["NM_MAKER"];
								row2["QT_GOOD_INV"] = D.GetString(row1["QT_GOOD_INV"]);
								row2["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", D.GetDecimal(row1["QT_GOOD_INV"]), D.GetDecimal(dataRowArray1[0]["UNIT_PO_FACT"]));
								row2["CD_SL"] = D.GetString(row1["GI_SL"]).ToUpper();
								row2["출고창고"] = D.GetString(dataRowArray2[0]["NM_SL"]);
								this.ctx출고창고.CodeValue = D.GetString(row1["GI_SL"]);
								this.ctx출고창고.CodeName = D.GetString(dataRowArray2[0]["NM_SL"]);
								row2["CD_SL_REF"] = D.GetString(row1["GR_SL"]).ToUpper();
								row2["입고창고"] = D.GetString(dataRowArray3[0]["NM_SL"]);
								row2["FG_SLQC"] = !(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), D.GetString(row1["GR_SL"])) == "Y") ? "N" : D.GetString(dataRowArray1[0]["FG_SLQC"]);
								row2["FG_IO"] = "022";
								row2["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
								row2["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
								row2["DT_IO"] = this._header.CurrentRow["DT_IO"];
								row2["YN_RETURN"] = "N";
								row2["YN_AM"] = this._header.CurrentRow["YN_AM"];

								if (this.MNG_LOT == "Y" && dataRowArray1[0]["FG_SERNO"].ToString() == "002")
								{
									row2["NO_LOT"] = "YES";
									row2["FG_SERNO"] = "LOT";
								}
								else
								{
									row2["NO_LOT"] = "NO";
									row2["FG_SERNO"] = "미관리";

									if (this.MNG_SERIAL == "Y" && dataRowArray1[0]["FG_SERNO"].ToString() == "003")
									{
										row2["NO_SERL"] = "YES";
										row2["FG_SERNO"] = "S/N";
									}
									else
										row2["NO_SERL"] = "NO";
								}

								row2["NM_QTIOTP"] = this.ctx수불형태.CodeName;
								row2["NO_ISURCV"] = "";
								row2["NO_ISURCVLINE"] = 0;

								if (flag8)
									row2["DC_RMK"] = row1["DC_RMK"];
								
								if (flag9)
									row2["DC_RMK1"] = row1["DC_RMK1"];
								
								if (Config.MA_ENV.YN_UNIT != "Y")
								{
									if (!flag4)
									{
										row2["CD_PROJECT"] = this.ctx프로젝트.CodeValue;
										row2["NM_PROJECT"] = this.ctx프로젝트.CodeName;
									}
									else if (flag4 && this.ChkData_PJT(D.GetString(row1["NO_PROJECT"]), ref empty2))
									{
										row2["CD_PROJECT"] = D.GetString(row1["NO_PROJECT"]);
										row2["NM_PROJECT"] = empty2;
									}
								}

								if (dataRowArray4 != null && dataRowArray4.Length > 0)
								{
									row2["SEQ_PROJECT"] = dataRowArray4[0]["SEQ_PROJECT"];
									row2["CD_PJT_ITEM"] = dataRowArray4[0]["CD_PJT_ITEM"];
									row2["NM_PJT_ITEM"] = dataRowArray4[0]["NM_PJT_ITEM"];
									row2["STND_UNIT"] = dataRowArray4[0]["PJT_ITEM_STND"];
									row2["NO_PROJECT"] = D.GetString(dataRowArray4[0]["NO_PROJECT"]);
									row2["NM_PROJECT"] = D.GetString(dataRowArray4[0]["NM_PROJECT"]);
								}

								if (Global.MainFrame.ServerKeyCommon == "DAEKHON")
								{
									string str5 = ComFunc.MasterSearch("MA_PARTNER_NM", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									   D.GetString(row1["CD_USERDEF1_QTIO"]) });
									if (str5 != string.Empty)
									{
										row2["CD_USERDEF1_QTIO"] = D.GetString(row1["CD_USERDEF1_QTIO"]);
										row2["NM_USERDEF1_QTIO"] = str5;
									}

									string str6 = ComFunc.MasterSearch("MA_EMP", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																								D.GetString(row1["CD_USERDEF2_QTIO"]) });
									if (str6 != string.Empty)
									{
										row2["CD_USERDEF2_QTIO"] = D.GetString(row1["CD_USERDEF2_QTIO"]);
										row2["NM_USERDEF2_QTIO"] = str6;
									}

									row2["DATE_USERDEF1_QTIO"] = D.GetString(row1["DATE_USERDEF1_QTIO"]);
								}

								dataTable4.Rows.Add(row2);
							}
							else
							{
								string str5 = row1["CD_ITEM"].ToString().PadRight(10, ' ');
								string str6 = D.GetString(row1["GI_SL"]);
								string str7 = D.GetString(row1["GR_SL"]);
								string str8 = "";

								if (flag4)
									str8 = D.GetString(row1["NO_PROJECT"]);

								string str9 = str5 + " \t " + str8 + " \t" + str6 + " \t" + str7 + " \n";
								stringBuilder.AppendLine(str9);
								
								if (dataRowArray1.Length == 0)
									flag6 = true;
								
								if (dataRowArray2.Length == 0 || dataRowArray3.Length == 0)
									flag7 = true;
							}
						}
					}

					if (flag6 || flag7 || flag1)
					{
						MsgControl.CloseMsg();

						if (!flag1)
							this.ShowDetailMessage("엑셀 업로드하는 중에 [거래처품목]과 불일치하는 항목들이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str1.ToString());
						else if (flag6)
							this.ShowDetailMessage("엑셀 업로드하는 중에 [공장품목]에 불일치 항목들이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", D.GetString(stringBuilder));
						else if (flag7)
							this.ShowDetailMessage("엑셀 업로드하는 중에 [창고]에 불일치 항목들이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", D.GetString(stringBuilder));
						else if (!flag2)
							this.ShowDetailMessage("엑셀 업로드하는 중에 [PJT]에 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str2.ToString());
					}

					MsgControl.CloseMsg();
					this._flex.AddFinished();
					
					if (this._flex.HasNormalRow)
					{
						this._flex.Col = this._flex.Cols.Fixed;
						this._flex.Row = this._flex.Rows.Fixed;
						this._flex.Focus();
					}
					
					this.ToolBarSaveButtonEnabled = true;
					this.ToolBarAddButtonEnabled = true;
					this.dtp이동일.Enabled = false;
					this.btn삭제.Enabled = true;
					this.btn요청적용.Enabled = false;
					this.btn외주의뢰적용.Enabled = false;
					this.btn생산의뢰적용.Enabled = false;
					this.ctx수불형태.Enabled = false;
					this.cbo공장.Enabled = false;
					this.ctx담당자.Enabled = false;
					this.ShowMessage("엑셀 작업을 완료하였습니다.");
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				this._flex.Redraw = true;
			}
		}

        private void btn출고반품적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead(sender)) return;

                P_CZ_PU_STS_REG_SUB dialog = new P_CZ_PU_STS_REG_SUB("041");
                if (dialog.ShowDialog() == DialogResult.OK && dialog.LineData.Rows.Count > 0)
                {
                    this._flex.Redraw = false;

                    for (int index = 0; index < dialog.LineData.Rows.Count; ++index)
                    {
                        DataRow row = dialog.LineData.Rows[index];

                        this._flex.Rows.Add();
                        this._flex.Row = this._flex.Rows.Count - 1;

                        this._flex["S"] = "N";
                        this._flex["CD_ITEM"] = row["CD_ITEM"].ToString();
                        this._flex["NM_ITEM"] = row["NM_ITEM"].ToString();
                        //this._flex["STND_ITEM"] = row["STND_ITEM"].ToString();
                        this._flex["UNIT_IM"] = row["UNIT_IM"].ToString();
                        //this._flex["UNIT_PO"] = row["UNIT_PO"].ToString();
                        this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                        //this._flex["NO_LOT"] = row["FG_LOT"].ToString();
                        //this._flex["NO_SERL"] = row["NO_SERL"].ToString();
                        this._flex["QT_GOOD_INV"] = D.GetDecimal(row["QT_GOOD_INV"]);
                        //this._flex["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", D.GetDecimal(row["QT_GOOD_INV"]), D.GetDecimal(row["UNIT_PO_FACT"]));
                        this._flex["QT_REJECT_INV"] = 0;
                        this._flex["DT_IO"] = this.dtp이동일.Text;
                        this._flex["요청자"] = row["NM_KOR"];
                        this._flex["NO_EMP"] = row["NO_EMP"];
                        this._flex["요청부서"] = row["NM_DEPT"];
                        this._flex["출고창고"] = row["NM_SL"];
                        this._flex["CD_SL"] = row["CD_SL"];
                        this._flex["NM_QTIOTP"] = this.ctx수불형태.CodeName;
                        this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                        //this._flex["NO_PSO_MGMT"] = row["NO_WO"];
                        //this._flex["NO_PSOLINE_MGMT"] = 0;
                        this._flex["YN_RETURN"] = "N";
                        //this._flex["CD_WCOP"] = row["CD_WCOP"].ToString();
                        //this._flex["NM_WC"] = row["NM_WC"].ToString();
                        //this._flex["NM_OP"] = row["NM_OP"].ToString();
                        //this._flex["NM_WORKITEM"] = row["NM_WORKITEM"].ToString();
                        //this._flex["CD_WORKITEM"] = row["CD_WORKITEM"].ToString();
                        this._flex["NO_IO_MGMT"] = row["NO_IO"].ToString();
                        this._flex["NO_IOLINE"] = D.GetDecimal(row["NO_IOLINE"]);
                        this._flex["NO_IOLINE_MGMT"] = D.GetDecimal(row["NO_IOLINE"]);
                        //this._flex["QT_INV"] = D.GetDecimal(row["QT_INV"]);
                        this._flex["CD_PROJECT"] = row["CD_PJT"];
                        this._flex["NM_PROJECT"] = row["NM_PJT"];

                        //if (Config.MA_ENV.PJT형여부 == "Y")
                        //{
                        //    this._flex["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                        //    this._flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        //    this._flex["NM_PJT_ITEM"] = row["PJT_NM_ITEM"];
                        //    this._flex["PJT_STND_ITEM"] = row["PJT_STND_ITEM"];
                        //    this._flex["NO_WBS"] = row["NO_WBS"];
                        //    this._flex["NO_CBS"] = row["NO_CBS"];
                        //}

                        this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"];
                        //this._flex["QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
                        //this._flex["BARCODE"] = D.GetString(row["BARCODE"]);
                        //this._flex["CD_ZONE"] = D.GetString(row["CD_ZONE"]);
                        //this._flex["FG_SLQC"] = D.GetString(row["FG_SLQC"]);
                        //this._flex["CLS_ITEM"] = D.GetString(row["CLS_ITEM"]);
                        //this._flex["NM_MAKER"] = D.GetString(row["NM_MAKER"]);

                        //if (App.SystemEnv.PMS사용)
                        //{
                        //    this._flex["CD_CSTR"] = row["CD_CSTR"];
                        //    this._flex["DL_CSTR"] = row["DL_CSTR"];
                        //    this._flex["NM_CSTR"] = row["NM_CSTR"];
                        //    this._flex["SIZE_CSTR"] = row["SIZE_CSTR"];
                        //    this._flex["UNIT_CSTR"] = row["UNIT_CSTR"];
                        //    this._flex["QTY_ACT"] = row["QTY_ACT"];
                        //    this._flex["UNT_ACT"] = row["UNT_ACT"];
                        //    this._flex["AMT_ACT"] = row["AMT_ACT"];
                        //}

                        //this._flex["STND_DETAIL_ITEM"] = D.GetString(row["STND_DETAIL_ITEM"]);
                        //this._flex["MAT_ITEM"] = D.GetString(row["MAT_ITEM"]);
                        //this._flex["NM_GRP_MFG"] = D.GetString(row["NM_GRP_MFG"]);
                        this._flex["FG_TRACK"] = "GIRT";

                        this._flex.AddFinished();
                        this._flex.Col = this._flex.Cols.Fixed;
                    }

                    this._flex.Redraw = true;

                    this.btn추가.Enabled = false;
                    this.btn삭제.Enabled = true;
                    this.dtp이동일.Enabled = false;
                    this.ctx수불형태.Enabled = false;
                    this.cbo공장.Enabled = false;
                    this.ctx담당자.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
            }
        }

		private bool ChkData_PJT(string cd_pjt, ref string p_nm_pjt)
		{
			if (this._dt_pjt == null || this._dt_pjt.Rows.Count < 1)
				return false;

			DataRow[] dataRowArray = this._dt_pjt.Select("NO_PROJECT = '" + cd_pjt + "'");
			
			if (dataRowArray == null || dataRowArray.Length < 1)
				return false;
			
			p_nm_pjt = D.GetString(dataRowArray[0]["NM_PROJECT"]);
			
			return true;
		}

		private bool Feild_Check(string pstr_gubun)
		{
			if (this.dtp이동일.Text == string.Empty)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl이동일.Text });
				return false;
			}

			if (this.ctx수불형태.CodeValue == string.Empty)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수불형태.Text });
				return false;
			}

			if (D.GetString(this._header.CurrentRow["CD_PLANT"]) == string.Empty)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
				return false;
			}

			if (this.ctx담당자.CodeValue == string.Empty)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자.Text });
				return false;
			}

			if (pstr_gubun == "지시소요량")
			{
				if (D.GetString(this.ctx출고창고.CodeName) == string.Empty)
				{
					this.ShowMessage("출고창고를 지정하십시요");
					this.ctx출고창고.Focus();
					return false;
				}

				if (D.GetString(this.ctx입고창고.CodeName) == string.Empty)
				{
					this.ShowMessage("입고창고를 지정하십시요");
					this.ctx입고창고.Focus();
					return false;
				}
			}

			return true;
		}

		private bool Save_Check()
		{
			if (!Global.MainFrame.ServerKeyCommon.Contains("LNPCOS"))
			{
				if (this._header.CurrentRow["CD_SL"].ToString() != string.Empty && this._header.CurrentRow["CD_SL_REF"].ToString() != string.Empty && string.Compare(this._header.CurrentRow["CD_SL"].ToString(), this._header.CurrentRow["CD_SL_REF"].ToString()) == 0)
				{
					this.ShowMessage("같은 창고끼리는 이동 할 수 없습니다.");
					return false;
				}

				for (int index = this._flex.Rows.Fixed; index < this._flex.Rows.Count; ++index)
				{
					if (string.Compare(this._flex[index, "CD_SL"].ToString(), this._flex[index, "CD_SL_REF"].ToString()) == 0)
					{
						this.ShowMessage("같은 창고끼리는 이동 할 수 없습니다.");
						return false;
					}
				}
			}

			if (this.PJT형여부 && this._flex.HasNormalRow)
			{
				for (int index = this._flex.Rows.Fixed; index < this._flex.Rows.Count; ++index)
				{
					if (D.GetString(this._flex[index, "CD_PROJECT"]) == string.Empty)
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("프로젝트") });
						return false;
					}
				}
			}

			return true;
		}

		public void Reload(string NO_IO, string CD_PLANT)
		{
			try
			{
				DataSet dataSet = this._biz.Search(NO_IO, CD_PLANT, "");
				this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
				this._header.SetDataTable(dataSet.Tables[0]);
				this._flex.Binding = dataSet.Tables[1];
				this._header.AcceptChanges();
				this._flex.AcceptChanges();
				this._header.SetControlEnabled(false);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool IsChanged()
		{
			if (base.IsChanged()) return true;

			return this.헤더변경여부;
		}

		private void btn창고적용_Click(object sender, EventArgs e)
		{
			string name;

			try
			{
				string index1 = string.Empty;
				string index2 = string.Empty;
				string str1 = string.Empty;
				string str2 = string.Empty;

				name = ((Control)sender).Name;

				if (name == this.btn출고창고적용.Name)
				{
					index1 = "CD_SL";
					index2 = "출고창고";
					str1 = D.GetString(this.ctx출고창고.CodeValue);
					str2 = this.ctx출고창고.CodeName;
				}
				else if (name == this.btn입고창고적용.Name)
				{
					index1 = "CD_SL_REF";
					index2 = "입고창고";
					str1 = this.ctx입고창고.CodeValue;
					str2 = this.ctx입고창고.CodeName;
				}
				else if (name == this.btn프로젝트적용.Name)
				{
					index1 = "CD_PROJECT";
					index2 = "NM_PROJECT";
					str1 = this.ctx프로젝트.CodeValue;
					str2 = this.ctx프로젝트.CodeName;
				}

				if (this._flex.RowState() != DataRowState.Added)
				{
					this.ShowMessage(this.DD("수정할수없습니다."));
				}
				else
				{
					this._flex.Redraw = false;

					for (int index3 = this._flex.Rows.Fixed; index3 < this._flex.Rows.Count; ++index3)
					{
						if (!(D.GetString(this._flex[index3, "S"]) == "N") && !(D.GetString(this._flex[index3, "S"]) == ""))
						{
							this._flex[index3, index1] = str1;

							if (index2 != string.Empty)
								this._flex[index3, index2] = str2;

							if (name == this.btn입고창고적용.Name)
								this._flex[index3, "FG_SLQC"] = !(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), this.ctx입고창고.CodeValue) != "Y") ? this._biz.FG_SLQC("ITEM", D.GetString(this._flex[index3, "CD_ITEM"]), D.GetString(this.cbo공장.SelectedValue), "") : "N";
							else if (name == this.btn출고창고적용.Name)
							{
								if (D.GetString(this._flex[index3, "CD_ITEM"]) != "")
								{
									Decimal num3 = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[index3, "CD_SL"]), D.GetString(this._flex[index3, "CD_ITEM"]), D.GetString(this._flex[index3, "CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[index3, "SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[index3, "CD_SL"]), D.GetString(this._flex[index3, "CD_ITEM"]));
									this._flex[index3, "QT_GOODS"] = D.GetDecimal(num3);
								}
								else
									this._flex[index3, "QT_GOODS"] = 0;
							}
							else if (name == this.btn프로젝트적용.Name)
							{
								if (D.GetString(this._flex[index3, "CD_ITEM"]) != "")
								{
									Decimal num3 = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[index3, "CD_SL"]), D.GetString(this._flex[index3, "CD_ITEM"]), D.GetString(this._flex[index3, "CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[index3, "SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[index3, "CD_SL"]), D.GetString(this._flex[index3, "CD_ITEM"]));
									this._flex[index3, "QT_GOODS"] = D.GetDecimal(num3);
								}
								else
									this._flex[index3, "QT_GOODS"] = 0;

								if (Config.MA_ENV.YN_UNIT == "Y")
								{
									this._flex[index3, "SEQ_PROJECT"] = this.d_SEQ_PROJECT;
									this._flex[index3, "CD_PJT_ITEM"] = this.s_CD_PJT_ITEM;
									this._flex[index3, "NM_PJT_ITEM"] = this.s_NM_PJT_ITEM;
									this._flex[index3, "STND_UNIT"] = this.s_PJT_ITEM_STND;
								}
							}
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
				this._flex.Redraw = true;
			}
		}

		private bool 서버키(string pstr_서버키)
		{
			return Global.MainFrame.ServerKeyCommon == pstr_서버키;
		}

		private bool 서버키_TEST포함(string pstr_서버키)
		{
			return Global.MainFrame.ServerKeyCommon == pstr_서버키 || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_" || Global.MainFrame.ServerKeyCommon == "SQL_108";
		}

		private void bp_CD_SL_Search(object sender, SearchEventArgs e)
		{
			try
			{
				P_MA_SL_AUTH_SUB pMaSlAuthSub = new P_MA_SL_AUTH_SUB(this.cbo공장.SelectedValue.ToString(), Global.MainFrame.LoginInfo.EmployeeNo);
				if (pMaSlAuthSub.ShowDialog() != DialogResult.OK) return;

				this.ctx출고창고.CodeValue = pMaSlAuthSub.returnParams[0];
				this.ctx출고창고.CodeName = pMaSlAuthSub.returnParams[1];
				
				foreach (DataRow row in this._flex.DataTable.Rows)
				{
					row["CD_SL"] = D.GetString(this.ctx출고창고.CodeValue);
					row["출고창고"] = D.GetString(this.ctx출고창고.CodeName);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Control_Click(object sender, EventArgs e)
		{
			string name;

			try
			{
				name = ((Control)sender).Name;

				if (name == this.btnBOM적용.Name)
					this.APP_BOM();
				else if (name == this.btn지시소요량.Name)
					this.APP_PRWO();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void APP_BOM()
		{
			if (this.ctx수불형태.CodeValue == "")
				Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "수불형태" });
			else
			{
				P_PU_GIREQ_BOM_SUB pPuGireqBomSub = new P_PU_GIREQ_BOM_SUB(D.GetString(this.cbo공장.SelectedValue), this.dtp이동일.Text);
				if (pPuGireqBomSub.ShowDialog() != DialogResult.OK) return;

				this.Grid_BOMAfter(pPuGireqBomSub.dt_return, "BOM");
				
				if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
				{
					this.ToolBarDeleteButtonEnabled = false;
					this.dtp이동일.Enabled = false;
					this.ctx수불형태.Enabled = false;
					this.cbo공장.Enabled = false;
					this.ctx담당자.Enabled = false;
				}
				else
					this.ToolBarDeleteButtonEnabled = true;
			}
		}

		private void APP_PRWO()
		{
			try
			{
				if (!this.Feild_Check(this.btn지시소요량.Text)) return;

				DataTable dataTable = this._flex.DataTable;
				object[] objArray = new object[] { D.GetString(this.cbo공장.SelectedValue),
												   D.GetString( this.cbo공장.Text),
												   "SL" };

				P_PU_PRWO_RELEASE_SUB puPrwoReleaseSub = new P_PU_PRWO_RELEASE_SUB(D.GetString(this.cbo공장.SelectedValue), this.ctx출고창고.CodeValue, this.ctx입고창고.CodeValue);
				if (puPrwoReleaseSub.ShowDialog() == DialogResult.OK)
				{
					this._flex.DataTable.Clear();
					this.Grid_PRWO(puPrwoReleaseSub.dt_reutrn);
					this.ToolBarDeleteButtonEnabled = false;

					if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
					{
						this.dtp이동일.Enabled = false;
						this.ctx수불형태.Enabled = false;
						this.cbo공장.Enabled = false;
						this.ctx담당자.Enabled = false;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Grid_BOMAfter(DataTable dt_return, string gubun)
		{
			try
			{
				DataTable dataTable = this._flex.DataTable;
				Decimal maxValue = this._flex.GetMaxValue("NO_ISURCVLINE");
				this._flex.Redraw = false;
				CommonFunction commonFunction = new CommonFunction();

				foreach (DataRow row1 in dt_return.Rows)
				{
					DataRow row2 = dataTable.NewRow();

					if (gubun == "CD_MATL" || gubun == "BOM")
					{
						row2["CD_ITEM"] = row1["CD_MATL"];
						row2["NM_ITEM"] = row1["NM_ITEM_MATL"];
						row2["STND_ITEM"] = row1["STND_ITEM_MATL"];
						row2["UNIT_IM"] = row1["UNIT_IM_MATL"];
						row2["QT_GOOD_INV"] = row1["QT_ITEM_NET"];
					}
					else
					{
						row2["CD_ITEM"] = row1["CD_ITEM"];
						row2["NM_ITEM"] = row1["NM_ITEM"];
						row2["STND_ITEM"] = row1["STND_ITEM"];
						row2["UNIT_IM"] = row1["UNIT_IM"];
						row2["QT_GOOD_INV"] = row1["QT_POREQ_IM"];
					}

					row2["UNIT_PO"] = row1["UNIT_PO"];
					row2["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", D.GetDecimal(row2["QT_GOOD_INV"]), D.GetDecimal(row1["UNIT_PO_FACT"]));
					row2["출고창고"] = this.ctx출고창고.CodeName;
					row2["입고창고"] = this.ctx입고창고.CodeName;
					
					if (gubun == "BOM")
					{
						row2["NO_LOT"] = row1["FG_SERNO1"].ToString() == "002" ? "YES" : "NO";
						row2["NO_SERL"] = row1["FG_SERNO1"].ToString() == "003" ? "YES" : "NO";
						row2["FG_TRACK"] = "BOM";
					}
					else
					{
						row2["NO_LOT"] = row1["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
						row2["NO_SERL"] = row1["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
					}

					row2["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
					row2["CD_QTIOTP"] = this.ctx수불형태.CodeValue;
					row2["NM_QTIOTP"] = this.ctx수불형태.CodeName;
					row2["DT_IO"] = this.dtp이동일.Text;
					row2["YN_RETURN"] = "N";
					row2["YN_AM"] = this._header.CurrentRow["YN_AM"];
					row2["FG_SERNO"] = row1["FG_SN_LOT"].ToString();
					row2["NM_ITEMGRP"] = row1["GRP_ITEMNM"].ToString();
					row2["BARCODE"] = D.GetString(row1["BARCODE"]);
					row2["CD_ZONE"] = D.GetString(row1["CD_ZONE"]);
					row2["CLS_ITEM"] = D.GetString(row1["CLS_ITEM"]);
					row2["NM_MAKER"] = D.GetString(row1["NM_MAKER"]);
					row2["FG_SLQC"] = !(this.ctx입고창고.CodeName == "") ? (!(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), this.ctx입고창고.CodeValue) == "Y") ? "N" : D.GetString(row1["FG_SLQC"])) : D.GetString(row1["FG_SLQC"]);
					
					if (row1.Table.Columns.Contains("NO_WO"))
					{
						row2["NO_PSO_MGMT"] = row1["NO_WO"];
						row2["NO_PSOLINE_MGMT"] = row1["NO_WOLINE"];
					}
					
					dataTable.Rows.Add(row2);
					
					if (this.ctx출고창고.CodeValue != "")
					{
						this._flex[this._flex.Rows.Count - 1, "CD_SL"] = this.ctx출고창고.CodeValue;
						this._flex[this._flex.Rows.Count - 1, "NM_SL"] = this.ctx출고창고.CodeName;
					}
					
					this._flex[this._flex.Rows.Count - 1, "QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_SL"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_ITEM"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[this._flex.Rows.Count - 1, "SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_SL"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_ITEM"]))));
					++maxValue;
				}

				this._flex.Select(this._flex.Rows.Count - 1, this._flex.Cols.Fixed);
				this._flex.Redraw = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Grid_PRWO(DataTable dt_return)
		{
			try
			{
				DataTable dataTable = this._flex.DataTable;
				Decimal maxValue = this._flex.GetMaxValue("NO_ISURCVLINE");
				this._flex.Redraw = false;
				CommonFunction commonFunction = new CommonFunction();

				foreach (DataRow row1 in dt_return.Rows)
				{
					DataRow row2 = dataTable.NewRow();

					row2["CD_ITEM"] = row1["CD_MATL"];
					row2["NM_ITEM"] = row1["NM_ITEM_MATL"];
					row2["STND_ITEM"] = row1["STND_ITEM_MATL"];
					row2["UNIT_IM"] = row1["UNIT_IM_MATL"];
					row2["QT_GOOD_INV"] = row1["QT_ITEM_NET"];
					row2["UNIT_PO"] = row1["UNIT_PO"];
					row2["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", D.GetDecimal(row2["QT_GOOD_INV"]), D.GetDecimal(row1["UNIT_PO_FACT"]));
					row2["출고창고"] = this.ctx출고창고.CodeName;
					row2["입고창고"] = this.ctx입고창고.CodeName;
					row2["NO_LOT"] = row1["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
					row2["NO_SERL"] = row1["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
					row2["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
					row2["CD_QTIOTP"] = this.ctx수불형태.CodeValue;
					row2["NM_QTIOTP"] = this.ctx수불형태.CodeName;
					row2["DT_IO"] = this.dtp이동일.Text;
					row2["YN_RETURN"] = "N";
					row2["YN_AM"] = this._header.CurrentRow["YN_AM"];
					row2["FG_SERNO"] = row1["FG_SN_LOT"].ToString();
					row2["NM_ITEMGRP"] = row1["GRP_ITEMNM"].ToString();
					row2["BARCODE"] = D.GetString(row1["BARCODE"]);
					row2["CD_ZONE"] = D.GetString(row1["CD_ZONE"]);
					row2["CLS_ITEM"] = D.GetString(row1["CLS_ITEM"]);
					row2["NM_MAKER"] = D.GetString(row1["NM_MAKER"]);
					row2["FG_SLQC"] = !(this.ctx입고창고.CodeName == "") ? (!(this._biz.FG_SLQC("CD_SL", "", D.GetString(this.cbo공장.SelectedValue), this.ctx입고창고.CodeValue) == "Y") ? "N" : D.GetString(row1["FG_SLQC"])) : D.GetString(row1["FG_SLQC"]);
					
					if (row1.Table.Columns.Contains("NO_WO"))
					{
						row2["NO_PSO_MGMT"] = row1["NO_WO"];
						row2["NO_PSOLINE_MGMT"] = row1["NO_WOLINE"];
					}
					
					dataTable.Rows.Add(row2);
					
					if (this.ctx출고창고.CodeValue != "")
					{
						this._flex[this._flex.Rows.Count - 1, "CD_SL"] = this.ctx출고창고.CodeValue;
						this._flex[this._flex.Rows.Count - 1, "NM_SL"] = this.ctx출고창고.CodeName;
					}

					this._flex[this._flex.Rows.Count - 1, "QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_SL"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_ITEM"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[this._flex.Rows.Count - 1, "SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_SL"]), D.GetString(this._flex[this._flex.Rows.Count - 1, "CD_ITEM"]))));
					++maxValue;
				}

				this._flex.Select(this._flex.Rows.Count - 1, this._flex.Cols.Fixed);
				this._flex.Redraw = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn재고적용_Click(object sender, EventArgs e)
		{
			if (!this.CheckFieldHead(sender)) return;

			P_PU_GIREQ_SUB5 pPuGireqSuB5 = new P_PU_GIREQ_SUB5(this.cbo공장.SelectedValue.ToString(), 
															   this.ctx출고창고.CodeValue,
															   this.ctx출고창고.CodeName,
															   this.dtp이동일.Text);
			
			if (pPuGireqSuB5.ShowDialog() != DialogResult.OK) return;

			this.Grid_StockAfter(pPuGireqSuB5._dtL);

			if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
			{
				this.btn요청적용.Enabled = false;
				this.btnBOM적용.Enabled = false;
				this.btn외주의뢰적용.Enabled = false;
				this.btn생산의뢰적용.Enabled = false;
				this.btn엑셀업로드.Enabled = false;
				this.btn추가.Enabled = false;
			}
		}

		private bool CheckFieldHead(object sender)
		{
			try
			{
				if (this.ctx수불형태.CodeValue.ToString() == "")
				{
					this.ctx수불형태.Focus();
					this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl수불형태.Text });
					return false;
				}

				if (this.ctx담당자.CodeValue.ToString() == "")
				{
					this.ctx담당자.Focus();
					this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자.Text });
					return false;
				}

				if (this.dtp이동일.Value.ToString() == "")
				{
					this.dtp이동일.Focus();
					this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl이동일.Text });
					return false;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return true;
		}

		private void Grid_StockAfter(DataTable _dtL)
		{
			try
			{
				DataTable dataTable = this._flex.DataTable;
				Decimal maxValue = this._flex.GetMaxValue("NO_ISURCVLINE");
				this._flex.Redraw = false;
				CommonFunction commonFunction = new CommonFunction();

				foreach (DataRow row1 in _dtL.Rows)
				{
					DataRow row2 = dataTable.NewRow();
					row2["CD_ITEM"] = row1["CD_ITEM"];
					row2["NM_ITEM"] = row1["NM_ITEM"];
					row2["STND_ITEM"] = row1["STND_ITEM"];
					row2["UNIT_IM"] = row1["UNIT_IM"];
					row2["UNIT_PO"] = row1["UNIT_PO"];
					row2["NO_LOT"] = row1["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
					row2["NO_SERL"] = row1["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
					row2["QT_GOOD_INV"] = row1["QT_INV"];
					row2["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", D.GetDecimal(row1["QT_INV"]), D.GetDecimal(row1["UNIT_PO_FACT"]));
					row2["QT_GOODS"] = row1["QT_INV"];
					row2["CD_SL"] = row1["CD_SL"] != null ? row1["CD_SL"] : this.ctx출고창고.CodeValue;
					row2["출고창고"] = row1["NM_SL"] != null ? row1["NM_SL"] : this.ctx출고창고.CodeName;

					if (D.GetString(this.ctx입고창고.CodeValue) != string.Empty)
					{
						row2["CD_SL_REF"] = this.ctx입고창고.CodeValue;
						row2["입고창고"] = this.ctx입고창고.CodeName;
					}

					row2["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
					row2["DT_IO"] = this.dtp이동일.Text;
					row2["CD_QTIOTP"] = this.ctx수불형태.CodeValue;
					row2["NM_QTIOTP"] = this.ctx수불형태.CodeName;
					row2["YN_RETURN"] = "N";
					row2["YN_AM"] = this._header.CurrentRow["YN_AM"];
					row2["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
					row2["FG_SLQC"] = D.GetString(row1["FG_SLQC"]);
					row2["NM_MAKER"] = D.GetString(row1["NM_MAKER"]);

					if (_dtL.Columns.Contains("CD_PJT"))
					{
						row2["CD_PROJECT"] = row1["CD_PJT"];
						row2["NM_PROJECT"] = row1["NM_PJT"];

						if (Config.MA_ENV.PJT형여부 == "Y")
						{
							row2["SEQ_PROJECT"] = row1["SEQ_PROJECT"];
							row2["CD_PJT_ITEM"] = row1["CD_PJT_ITEM"];
							row2["NM_PJT_ITEM"] = row1["NM_PJT_ITEM"];
							row2["STND_UNIT"] = D.GetString(row1["PJT_STND_ITEM"]);
						}
					}

					row2["STND_DETAIL_ITEM"] = D.GetString(row1["STND_DETAIL_ITEM"]);
					row2["MAT_ITEM"] = D.GetString(row1["MAT_ITEM"]);
					row2["NM_GRP_MFG"] = D.GetString(row1["NM_GRP_MFG"]);
					row2["FG_TRACK"] = "INV";

					dataTable.Rows.Add(row2);
					++maxValue;
				}

				this._flex.Select(this._flex.Rows.Count - 1, this._flex.Cols.Fixed);
				this._flex.AddFinished();
				this._flex.Redraw = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Only_Pjt(DataRow dr)
		{
			this._flex["CD_PROJECT"] = dr["CD_PJT"];
			this._flex["NM_PROJECT"] = dr["NM_PJT"];

			if (!this.PJT형여부) return;
			
			this._flex["SEQ_PROJECT"] = dr["SEQ_PROJECT"];
			this._flex["CD_PJT_ITEM"] = dr["CD_PJT_ITEM"];
			this._flex["NM_PJT_ITEM"] = dr["NM_PJT_ITEM"];
			this._flex["NO_WBS"] = dr["NO_WBS"];
			this._flex["NO_CBS"] = dr["NO_CBS"];
		}

		private void btn입고적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.CheckFieldHead(sender)) return;

                P_CZ_PU_STS_REG_SUB dialog = new P_CZ_PU_STS_REG_SUB("001");
                if (dialog.ShowDialog() == DialogResult.OK && dialog.LineData.Rows.Count > 0)
				{
                    this.입고적용그리드라인추가(dialog.LineData);
					this.btn추가.Enabled = false;
					this.btn삭제.Enabled = true;
					this.dtp이동일.Enabled = false;
					this.ctx수불형태.Enabled = false;
					this.cbo공장.Enabled = false;
					this.ctx담당자.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void 입고적용그리드라인추가(DataTable pdt_Line)
		{
			try
			{
				this._flex.Redraw = false;

				for (int index = 0; index < pdt_Line.Rows.Count; ++index)
				{
					DataRow row = pdt_Line.Rows[index];

					this._flex.Rows.Add();
					this._flex.Row = this._flex.Rows.Count - 1;
					
                    this._flex["S"] = "N";
					this._flex["CD_ITEM"] = row["CD_ITEM"].ToString();
					this._flex["NM_ITEM"] = row["NM_ITEM"].ToString();
                    //this._flex["STND_ITEM"] = row["STND_ITEM"].ToString();
					this._flex["UNIT_IM"] = row["UNIT_IM"].ToString();
                    //this._flex["UNIT_PO"] = row["UNIT_PO"].ToString();
					this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                    //this._flex["NO_LOT"] = row["FG_LOT"].ToString();
                    //this._flex["NO_SERL"] = row["NO_SERL"].ToString();
					this._flex["QT_GOOD_INV"] = D.GetDecimal(row["QT_GOOD_INV"]);
                    //this._flex["QT_UNIT_PO"] = this.calc이동수량("QT_GOOD_INV", D.GetDecimal(row["QT_GOOD_INV"]), D.GetDecimal(row["UNIT_PO_FACT"]));
					this._flex["QT_REJECT_INV"] = 0;
					this._flex["DT_IO"] = this.dtp이동일.Text;
					this._flex["요청자"] = row["NM_KOR"];
					this._flex["NO_EMP"] = row["NO_EMP"];
					this._flex["요청부서"] = row["NM_DEPT"];
					this._flex["출고창고"] = row["NM_SL"];
					this._flex["CD_SL"] = row["CD_SL"];
					this._flex["NM_QTIOTP"] = this.ctx수불형태.CodeName;
					this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                    //this._flex["NO_PSO_MGMT"] = row["NO_WO"];
                    //this._flex["NO_PSOLINE_MGMT"] = 0;
					this._flex["YN_RETURN"] = "N";
                    //this._flex["CD_WCOP"] = row["CD_WCOP"].ToString();
                    //this._flex["NM_WC"] = row["NM_WC"].ToString();
                    //this._flex["NM_OP"] = row["NM_OP"].ToString();
                    //this._flex["NM_WORKITEM"] = row["NM_WORKITEM"].ToString();
                    //this._flex["CD_WORKITEM"] = row["CD_WORKITEM"].ToString();
					this._flex["NO_IO_MGMT"] = row["NO_IO"].ToString();
					this._flex["NO_IOLINE"] = D.GetDecimal(row["NO_IOLINE"]);
					this._flex["NO_IOLINE_MGMT"] = D.GetDecimal(row["NO_IOLINE"]);
                    //this._flex["QT_INV"] = D.GetDecimal(row["QT_INV"]);
					this._flex["CD_PROJECT"] = row["CD_PJT"];
					this._flex["NM_PROJECT"] = row["NM_PJT"];

                    //if (Config.MA_ENV.PJT형여부 == "Y")
                    //{
                    //    this._flex["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                    //    this._flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                    //    this._flex["NM_PJT_ITEM"] = row["PJT_NM_ITEM"];
                    //    this._flex["PJT_STND_ITEM"] = row["PJT_STND_ITEM"];
                    //    this._flex["NO_WBS"] = row["NO_WBS"];
                    //    this._flex["NO_CBS"] = row["NO_CBS"];
                    //}

					this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"];
                    //this._flex["QT_GOODS"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PROJECT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
                    //this._flex["BARCODE"] = D.GetString(row["BARCODE"]);
                    //this._flex["CD_ZONE"] = D.GetString(row["CD_ZONE"]);
                    //this._flex["FG_SLQC"] = D.GetString(row["FG_SLQC"]);
                    //this._flex["CLS_ITEM"] = D.GetString(row["CLS_ITEM"]);
                    //this._flex["NM_MAKER"] = D.GetString(row["NM_MAKER"]);

                    //if (App.SystemEnv.PMS사용)
                    //{
                    //    this._flex["CD_CSTR"] = row["CD_CSTR"];
                    //    this._flex["DL_CSTR"] = row["DL_CSTR"];
                    //    this._flex["NM_CSTR"] = row["NM_CSTR"];
                    //    this._flex["SIZE_CSTR"] = row["SIZE_CSTR"];
                    //    this._flex["UNIT_CSTR"] = row["UNIT_CSTR"];
                    //    this._flex["QTY_ACT"] = row["QTY_ACT"];
                    //    this._flex["UNT_ACT"] = row["UNT_ACT"];
                    //    this._flex["AMT_ACT"] = row["AMT_ACT"];
                    //}

                    //this._flex["STND_DETAIL_ITEM"] = D.GetString(row["STND_DETAIL_ITEM"]);
                    //this._flex["MAT_ITEM"] = D.GetString(row["MAT_ITEM"]);
                    //this._flex["NM_GRP_MFG"] = D.GetString(row["NM_GRP_MFG"]);
                    this._flex["FG_TRACK"] = "IO";

					this._flex.AddFinished();
					this._flex.Col = this._flex.Cols.Fixed;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				this._flex.SumRefresh();
				this._flex.Redraw = true;
			}
		}

		private Decimal calc이동수량(string col, Decimal val, Decimal val2)
		{
			Decimal num = 0;
			if (val2 == 0)
				val2 = 1;
			if (col == "QT_GOOD_INV")
				num = Unit.수량(DataDictionaryTypes.PU, val / val2);
			else if (col == "QT_UNIT_PO")
				num = Unit.수량(DataDictionaryTypes.PU, val * val2);
			return num;
		}

		private void tb_DT_IO_DateChanged(object sender, EventArgs e)
		{
			try
			{
				this._flex.Redraw = false;

				if (this._flex.HasNormalRow)
				{
					foreach (DataRow dataRow in this._flex.DataTable.Select())
						dataRow["DT_IO"] = this.dtp이동일.Text;
				}

				this._flex.Redraw = true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
				this._flex.Redraw = true;
			}
		}

		private void 의뢰번호Visible(string fg_sub)
		{
			if (fg_sub == "생산")
				this._flex.Cols["NO_ISURCV"].Visible = this._flex.Cols["NO_PSO_MGMT"].Visible = true;
			else
				this._flex.Cols["NO_ISURCV"].Visible = this._flex.Cols["NO_PSO_MGMT"].Visible = false;
		}

		private void 수불형태Default셋팅()
		{
			if (Settings1.Default.CD_QTIOTP.ToString() == string.Empty) return;

			this.ctx수불형태.CodeValue = Settings1.Default.CD_QTIOTP;
			this.ctx수불형태.CodeName = Settings1.Default.NM_QTIOTP;
			this._header.CurrentRow["CD_QTIOTP"] = this.ctx수불형태.CodeValue;
			this._header.CurrentRow["NM_QTIOTP"] = this.ctx수불형태.CodeName;
			this._header.CurrentRow["YN_AM"] = D.GetString(Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new object[] { MA.Login.회사코드, Settings1.Default.CD_QTIOTP })["YN_AM"]);
		}
	}
}
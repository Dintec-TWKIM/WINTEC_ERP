using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using Duzon.ERPU.SA;
using DX;
using Parsing.Parser;
using sale;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using static Duzon.ERPU.MA;

namespace cz
{
	public partial class P_CZ_SA_SO_BATCH_WINTEC : PageBase
	{
		#region 초기화 & 전역변수
		private CommonFunction _CommFun = new CommonFunction();
		private P_CZ_SA_SO_BATCH_WINTEC_BIZ _biz = new P_CZ_SA_SO_BATCH_WINTEC_BIZ();
		private string soStatus;
		private string tp_Gi;
		private string tp_Busi;
		private string tp_Iv;
		private string _의뢰여부;
		private string _출하여부;
		private string _매출여부;
		private string trade;
		private string tp_SalePrice;
		private string so_Price;
		private DataTable defaultSchema;
		private string disCount_YN;
		private string cd_CC;
		private string nm_CC;
		private string str동기화;
		private string _매출자동여부;

		public P_CZ_SA_SO_BATCH_WINTEC()
		{
			try
			{
				if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
					StartUp.Certify(this);

				this.InitializeComponent();
				
				this.MainGrids = new FlexGrid[] { this._flex };
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			DataTable dataTable = this._biz.search_EnvMng();

			if (dataTable.Rows.Count > 0 && (dataTable.Rows[0]["FG_TP"] != DBNull.Value && dataTable.Rows[0]["FG_TP"].ToString().Trim() != string.Empty))
				this.disCount_YN = dataTable.Select("FG_TP = '002'")[0]["CD_TP"].ToString();

			this.str동기화 = BASIC.GetMAEXC("프로젝트 거래처/영업그룹 동기화");

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this._flex.BeginSetting(1, 1, true);

			this._flex.SetCol("S", "선택", false);
			this._flex.SetCol("NO_SO", "수주번호", 120, false);
			this._flex.SetCol("SEQ_SO", "수주항번", 120, false);
			this._flex.SetCol("DT_SO", "수주일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("CD_EXCH", "통화코드", 80, true);
			this._flex.SetCol("NM_EXCH", "통화명", 80, false);

			if (MA.기준환율.Option == 기준환율옵션.적용_수정불가)
				this._flex.SetCol("RT_EXCH", "환율", 80, false);
			else
				this._flex.SetCol("RT_EXCH", "환율", 80, true);
			
			this._flex.SetCol("CD_PARTNER", "거래처", 120, true);
			this._flex.SetCol("LN_PARTNER", "거래처명", 120, false);

			this._flex.SetCol("CD_USERDEF3", "엔진타입", 100, true);
			this._flex.SetCol("TXT_USERDEF1", "거래처공정", 100, true);
			this._flex.SetCol("NUM_USERDEF2", "구매담당자", 100, true);
			this._flex.SetCol("NUM_USERDEF3", "설계담당자", 100, true);
			this._flex.SetCol("CD_USERDEF1", "선급검사기관1", 100, true);
			this._flex.SetCol("CD_USERDEF2", "선급검사기관2", 100, true);
			this._flex.SetCol("NO_NEGO", "입고처", 100, true);
			this._flex.SetCol("NM_NEGO", "입고처명", 100, false);
			this._flex.SetCol("NUM_USERDEF1", "인수자", 100, true);
			this._flex.SetCol("CD_ITEM", "품목코드", 120, true);
			this._flex.SetCol("NM_ITEM", "품목명", 120, false);
			this._flex.SetCol("STND_ITEM", "규격", 65, false);
			this._flex.SetCol("UNIT_SO", "단위", 65, false);
			this._flex.SetCol("CD_PLANT", "공장", 140, true);
			this._flex.SetCol("DT_EXPECT", "최초납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("DT_REQGI", "출하예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("QT_SO", "수량", 60, 17, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("UM_SO", "단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flex.SetCol("AM_SO", "금액", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flex.SetCol("AM_WONAMT", "원화금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
			this._flex.SetCol("AM_VAT", "부가세", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
			this._flex.SetCol("AMVAT_SO", "합계금액", 120, 17, false, typeof(decimal), FormatTpType.MONEY);
			this._flex.SetCol("UNIT_IM", "관리단위", 65, false);
			this._flex.SetCol("QT_IM", "관리수량", 65, 17, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("CD_SL", "창고코드", 80, true);
			this._flex.SetCol("NM_SL", "창고명", 120, false);
			this._flex.SetCol("TP_ITEM", "품목타입", false);
			this._flex.SetCol("UNIT_SO_FACT", "수주단위수량", false);
			this._flex.SetCol("LT_GI", "출하LT", false);
			this._flex.SetCol("GI_PARTNER", "납품처코드", 120, true);
			this._flex.SetCol("GN_PARTNER", "납품처명", 200, false);
			this._flex.SetCol("DC_RMK", "헤더비고", 150, true);
			this._flex.SetCol("DC_RMK1", "헤더비고1", 150, true);
			this._flex.SetCol("DC1", "비고", 150, true);
			this._flex.SetCol("DC2", "변경비고", 150, true);

			this._flex.SetCol("TXT_USERDEF3", "자재번호", 150, true);
			this._flex.SetCol("TXT_USERDEF4", "도장COLOR", 150, true);
			this._flex.SetCol("TXT_USERDEF5", "납품장소", 150, true);
			this._flex.SetCol("TXT_USERDEF6", "호선번호", 150, true);
			this._flex.SetCol("TXT_USERDEF7", "도면번호", 150, true);

			if (Sa_Global.Sol_TpVat_ModifyYN == "Y")
			{
				this._flex.SetCol("TP_VAT", "VAT구분", 80, true);
				this._flex.SetCol("RT_VAT", "VAT율", 70, 17, false, typeof(decimal), FormatTpType.QUANTITY);
			}

			if (Sa_Global.SoL_CdCc_ModifyYN == "Y")
			{
				this._flex.SetCol("CD_CC", "코스트센터", 100, true);
				this._flex.SetCol("NM_CC", "코스트센터명", 120, false);
			}

			if (this.disCount_YN == "Y")
			{
				this._flex.SetCol("RT_DSCNT", "할인율", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
				this._flex.SetCol("UM_BASE", "기준단가", 100, 15, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			}

			if (this.str동기화 == "N")
			{
				this._flex.SetCol("NO_PROJECT", "프로젝트", 100, true);
				this._flex.SetCol("NM_PROJECT", "프로젝트명", 120, false);
			}

			this._flex.SetCol("CD_ITEM_PARTNER", "거래처품번", 120, true);
			this._flex.SetCol("NM_ITEM_PARTNER", "발주품번", 150, false);
			this._flex.SetCol("NO_PO_PARTNER", "거래처PO번호", 140, true);
			this._flex.SetCol("NO_POLINE_PARTNER", "거래처PO항번", 100, 3, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("FG_USE", "수주용도", false);
			this._flex.SetCol("SOL_TXT_USERDEF1", "TEXT사용자정의1", 150, true);
			this._flex.Cols["SOL_TXT_USERDEF1"].Visible = false;
			this._flex.SetCol("SOL_TXT_USERDEF2", "TEXT사용자정의2", 150, true);
			this._flex.Cols["SOL_TXT_USERDEF2"].Visible = false;

			this._flex.SetCodeHelpCol("CD_EXCH", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_EXCH", "NM_EXCH" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
			this._flex.SetCodeHelpCol("NO_NEGO", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "NO_NEGO", "NM_NEGO" }, new string[] { "CD_PARTNER", "LN_PARTNER" });

			this._flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
																											 "NM_ITEM",
																											 "STND_ITEM",
																											 "UNIT_SO",
																											 "UNIT_IM",
																											 "TP_ITEM",
																											 "CD_SL",
																											 "NM_SL",
																											 "UNIT_SO_FACT",
																											 "LT_GI" }, new string[] { "CD_ITEM",
																																	   "NM_ITEM",
																																	   "STND_ITEM",
																																	   "UNIT_SO",
																																	   "UNIT_IM",
																																	   "TP_ITEM",
																																	   "CD_GISL",
																																	   "NM_GISL",
																																	   "UNIT_SO_FACT",
																																	   "LT_GI" }, ResultMode.SlowMode);

			this._flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, ResultMode.SlowMode);
			this._flex.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
			this._flex.SetCodeHelpCol("GI_PARTNER", HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER", "GN_PARTNER" }, new string[] { "CD_TPPTR", "NM_TPPTR" });
			this._flex.SetCodeHelpCol("CD_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }, new string[] { "CD_CC", "NM_CC" }); 
			this._flex.SetCodeHelpCol("NO_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT", "NM_PROJECT" }, new string[] { "NO_PROJECT", "NM_PROJECT" }); 

			this._flex.SetCodeHelpCol("CD_ITEM_PARTNER", HelpID.P_MA_CPITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM_PARTNER",
																													  "NM_ITEM_PARTNER",
																													  "CD_ITEM",
																													  "NM_ITEM",
																													  "STND_ITEM",
																													  "UNIT_SO",
																													  "UNIT_IM",
																													  "TP_ITEM",
																													  "CD_SL",
																													  "NM_SL",
																													  "UNIT_SO_FACT",
																													  "LT_GI" }, new string[] { "NM_ITEM_PARTNER",
																																			    "CD_ITEM_PARTNER",
																																			    "CD_ITEM",
																																			    "NM_ITEM",
																																			    "STND_ITEM",
																																			    "UNIT_SO",
																																			    "UNIT_IM",
																																			    "TP_ITEM",
																																			    "CD_GISL",
																																			    "NM_GISL",
																																			    "UNIT_SO_FACT",
																																			    "LT_GI" }, ResultMode.SlowMode);

			this._flex.SetExceptEditCol(new string[] { "NO_SO",
													   "SEQ_SO",
													   "LN_PARTNER",
													   "NM_ITEM",
													   "STND_ITEM",
													   "UNIT_SO",
													   "UNIT_IM",
													   "AM_WONAMT",
													   "AMVAT_SO",
													   "NM_SL",
													   "UNIT_SO_FACT",
													   "GN_PARTNER",
													   "RT_VAT",
													   "NM_CC" });

			this._flex.VerifyNotNull = new string[] { "CD_PARTNER",
													  "CD_PLANT",
													  "CD_ITEM",
													  "DT_DUEDATE",
													  "DT_REQGI",
													  "TP_VAT",
													  "CD_CC" };

			this._flex.VerifyCompare(this._flex.Cols["QT_SO"], 0, OperatorEnum.Greater);
			this._flex.VerifyCompare(this._flex.Cols["UM_SO"], 0, OperatorEnum.GreaterOrEqual);
			this._flex.VerifyCompare(this._flex.Cols["AM_SO"], 0, OperatorEnum.GreaterOrEqual);
			this._flex.VerifyCompare(this._flex.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);
			this._flex.VerifyCompare(this._flex.Cols["QT_IM"], 0, OperatorEnum.Greater);

			this._flex.SetDummyColumn(new string[] { "S" });

			this._flex.SettingVersion = "0.0.0.1";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			if (this.disCount_YN == "Y")
				this._flex.SetExceptSumCol(new string[] { "UM_SO",
														  "UM_BASE",
														  "RT_DSCNT",
														  "RT_VAT" });
			else
				this._flex.SetExceptSumCol(new string[] { "UM_SO",
														  "RT_VAT" });
		}

		protected override void InitPaint()
		{
			this.oneGrid1.UseCustomLayout = true;
			this.oneGrid1.IsSearchControl = false;
			this.bpPanelControl2.IsNecessaryCondition = true;
			this.bpPanelControl3.IsNecessaryCondition = true;
			this.bpPanelControl4.IsNecessaryCondition = true;
			this.bpPanelControl6.IsNecessaryCondition = true;
			this.bpPanelControl8.IsNecessaryCondition = true;
			this.oneGrid1.InitCustomLayout();

			DataSet comboData = this.GetComboData(new string[] { "N;MA_B000005",
																 "S;SA_B000021",
																 "N;MA_CODEDTL_005;MA_B000040",
																 "N;MA_B000004",
																 "N;MA_PLANT" });

			this.cbo단가유형.DataSource = comboData.Tables[1];
			this.cbo단가유형.DisplayMember = "NAME";
			this.cbo단가유형.ValueMember = "CODE";

			new SetControl().SetCombobox(this.cbo단가적용, MA.GetCode("SA_B000021"));

			comboData.Tables[2].PrimaryKey = new DataColumn[] { comboData.Tables[2].Columns["CODE"] };

			this.cboVAT구분.DataSource = comboData.Tables[2];
			this.cboVAT구분.DisplayMember = "NAME";
			this.cboVAT구분.ValueMember = "CODE";

			if (Sa_Global.Sol_TpVat_ModifyYN == "Y")
				this._flex.SetDataMap("TP_VAT", comboData.Tables[2].Copy(), "CODE", "NAME");

			this._flex.SetDataMap("UNIT_SO", comboData.Tables[3], "CODE", "NAME");
			this._flex.SetDataMap("UNIT_IM", comboData.Tables[3], "CODE", "NAME");
			this._flex.SetDataMap("CD_PLANT", comboData.Tables[4], "CODE", "NAME");

			this.ctx영업그룹.CodeValue = string.Empty;
			this.ctx영업그룹.CodeName = string.Empty;
			this.ctx수주형태.CodeValue = string.Empty;
			this.ctx수주형태.CodeName = string.Empty;
			this.cur부가세율.DecimalValue = 10;
			this.ctx담당자.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
			this.ctx담당자.CodeName = Global.MainFrame.LoginInfo.EmployeeName;
			this.defaultSchema = this.getSchema("DEFAULT_SCHEMA");
			this._flex.Binding = this.defaultSchema;

			DataRow tpso1 = BASIC.GetTPSO(this.ctx수주형태.CodeValue);
			this._매출자동여부 = D.GetString(tpso1["IV_AUTO"]);
			this.tp_Gi = D.GetString(tpso1["TP_GI"]);
			this.tp_Busi = D.GetString(tpso1["TP_BUSI"]);
			this.tp_Iv = D.GetString(tpso1["TP_IV"]);
			this._의뢰여부 = D.GetString(tpso1["GIR"]);
			this._출하여부 = D.GetString(tpso1["GI"]);
			this._매출여부 = D.GetString(tpso1["IV"]);
			this.trade = D.GetString(tpso1["TRADE"]);

			if (Sa_Global.IVL_CdCc == "001")
			{
				DataRow tpso2 = Sa_ComFunc.GetTPSO(new object[] { this.LoginInfo.CompanyCode, this.ctx수주형태.CodeValue });
				this.cd_CC = D.GetString(tpso2["CD_CC"]);
				this.nm_CC = D.GetString(tpso2["NM_CC"]);
			}
			else if (Sa_Global.IVL_CdCc == "000")
			{
				object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode, this.ctx영업그룹.CodeValue };
				this.so_Price = this._biz.GetSaleOrgUmCheck(objArray);
				this.cd_CC = ComFunc.MasterSearch("MA_CC", objArray);
				objArray[1] = this.cd_CC;
				this.nm_CC = ComFunc.MasterSearch("MA_CC_NAME", objArray);
			}

			this._flex.SetDataMap("FG_USE", MA.GetCode("SA_B000057", true), "CODE", "NAME");

			this._flex.SetDataMap("CD_USERDEF1", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME"); // 선급검사기관1
			this._flex.SetDataMap("CD_USERDEF2", MA.GetCode("CZ_WIN0001", true), "CODE", "NAME"); // 선급검사기관2
			this._flex.SetDataMap("CD_USERDEF3", MA.GetCode("CZ_WIN0003", true), "CODE", "NAME"); // 엔진타입
		}

		private void InitEvent()
		{
			this.btn단가적용.Click += new EventHandler(this.btn단가적용_Click);
			this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
			this.btn추가.Click += new EventHandler(this.btn추가_Click);
			this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
			this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
			this.btn자동등록.Click += Btn자동등록_Click;
			
			this.cboVAT구분.SelectedIndexChanged += new EventHandler(this.cboVAT구분_SelectedIndexChanged);

			this.ctx수주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

			this.ctx영업그룹.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx수주형태.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);

			this.ctx수주형태.TextChanged += new EventHandler(this.ctx수주형태_TextChanged);
			
			this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
			this._flex.DoubleClick += new EventHandler(this._flex_DoubleClick);
			this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
			this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
			this._flex.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
			this._flex.AddRow += new EventHandler(this.btn추가_Click);
		}
		#endregion

		#region 메인버튼 이벤트
		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarAddButtonClicked(sender, e);

				this._flex.DataTable.Rows.Clear();
				this._flex.AcceptChanges();
				this.InitPaint();
				this.Authority(true);
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

				if (!this.SaveData()) return;

				this.ShowMessage(" 저장되었습니다. \n\n 저장된 데이터는 수주관리에서 조회하실 수 있습니다.");
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			if (!base.SaveData() || !this.ErrorCheck() || !this.Verify())
				return false;

			int num1 = 0;
			int num2 = 0;
			int num3 = 0;
			object[] objArray1 = new object[3];

			foreach (DataRow row in this._flex.DataTable.Rows)
			{
				if (row.RowState != DataRowState.Deleted)
				{
					if (row["TP_VAT"].ToString() == string.Empty)
						++num1;

					objArray1[0] = this.LoginInfo.CompanyCode;
					objArray1[1] = this.ctx수주형태.CodeValue;
					objArray1[2] = row["TP_VAT"].ToString();
					decimal num5 = D.GetDecimal(Sa_ComFunc.GetTpBusi(objArray1)[1].ToString());

					if (D.GetDecimal(row["RT_VAT"]) != num5)
						++num2;

					if (row["CD_CC"].ToString() == string.Empty)
						++num3;
				}
			}

			if (num1 != 0)
			{
				this.ShowMessage("라인과세는 필수입력항목입니다. \n\n 라인과세 입력을 확인하세요.");
				return false;
			}

			if (num2 != 0)
			{
				this.ShowMessage("라인부과세율 정보가 잘못되었습니다. \n\n 라인부과세율 정보를 확인하세요.");
				return false;
			}

			if (num3 != 0)
			{
				this.ShowMessage("Cost Center 는 필수입력항목입니다. \n\n Cost Center 입력을 확인하세요.");
				return false;
			}

			try
			{
				D.GetString(Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PLANT, new object[] { MA.Login.회사코드, D.GetString(this._flex.DataView.ToTable().Rows[0]["CD_PLANT"]) })["CD_BIZAREA"]);
				string empty = string.Empty;
				decimal num5 = 0;
				DataTable calculater = this.GetCalculater(this._flex.DataView.ToTable(), "Y", string.Empty);
				DataTable schema1 = this.getSchema("SOH");
				DataTable schema2 = this.getSchema("SOL");

				string 그룹조건, 수주형태, VAT구분;
				decimal 부가세율;

				수주형태 = this.ctx수주형태.CodeValue;
				VAT구분 = this.cboVAT구분.SelectedValue.ToString();
				부가세율 = this.cur부가세율.DecimalValue;

				foreach (DataRow row1 in calculater.Rows)
				{
					if (string.IsNullOrEmpty(row1["NO_SO"].ToString()))
						그룹조건 = "CD_PARTNER = '" + D.GetString(row1["CD_PARTNER"]) + "' AND ISNULL(DC_RMK, '') = '" + D.GetString(row1["DC_RMK"]) + "' AND ISNULL(DC_RMK1, '') = '" + D.GetString(row1["DC_RMK1"]) + "' AND CD_BIZAREA = '" + D.GetString(row1["CD_BIZAREA"]) + "'";
					else
						그룹조건 = "NO_SO = '" + row1["NO_SO"].ToString() + "'";

					if (calculater.Columns.Contains("PAYMENT"))
					{
						if (row1["CD_EXCH"].ToString() == "000")
							수주형태 = "1160";
						else
							수주형태 = "1100";

						switch (row1["PAYMENT"].ToString())
						{
							case "D":
							case "G":
								VAT구분 = "11";
								부가세율 = 10;
								break;
							case "X":
								VAT구분 = "14";
								부가세율 = 0;
								break;
							default:
								VAT구분 = this.cboVAT구분.SelectedValue.ToString();
								부가세율 = this.cur부가세율.DecimalValue;
								break;
						}
					}
					else
					{
						수주형태 = this.ctx수주형태.CodeValue;
						VAT구분 = this.cboVAT구분.SelectedValue.ToString();
						부가세율 = this.cur부가세율.DecimalValue;
					}

					row1["CD_BIZAREA"] = D.GetString(Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MA_PLANT, new object[] { MA.Login.회사코드, D.GetString(row1["CD_PLANT"]) })["CD_BIZAREA"]);
					DataRow[] dataRowArray;

					dataRowArray = schema1.Select(그룹조건);

					if (dataRowArray.Length == 0)
					{
						DataRow row2 = schema1.NewRow();

						string 수주번호;

						if (string.IsNullOrEmpty(row1["NO_SO"].ToString()))
							수주번호 = this.GetSeq(this.LoginInfo.CompanyCode, "SA", "02", row1["DT_SO"].ToString().Substring(0, 6)).ToString();
						else
							수주번호 = row1["NO_SO"].ToString();

						row1["NO_SO"] = 수주번호;
						row2["NO_SO"] = 수주번호;
						row2["NO_HST"] = num5;
						row2["CD_BIZAREA"] = row1["CD_BIZAREA"];
						row2["DT_SO"] = row1["DT_SO"];
						row2["CD_PARTNER"] = row1["CD_PARTNER"];
						row2["CD_SALEGRP"] = this.ctx영업그룹.CodeValue;
						row2["NO_EMP"] = this.ctx담당자.CodeValue;
						row2["TP_SO"] = 수주형태;
						row2["CD_EXCH"] = row1["CD_EXCH"];
						row2["RT_EXCH"] = row1["RT_EXCH"];
						row2["TP_PRICE"] = this.cbo단가유형.SelectedValue.ToString();
						row2["NO_PROJECT"] = row1["NO_PROJECT"];
						row2["TP_VAT"] = VAT구분;
						row2["RT_VAT"] = 부가세율;
						row2["VATRATE"] = 부가세율;
						row2["FG_VAT"] = "N";
						row2["FG_TAXP"] = "001";
						row2["DC_RMK"] = row1["DC_RMK"];
						row2["DC_RMK1"] = row1["DC_RMK1"];
						row2["FG_BILL"] = string.Empty;
						row2["FG_TRANSPORT"] = string.Empty;
						row2["NO_CONTRACT"] = string.Empty;
						row2["RMA_REASON"] = string.Empty;
						row2["NO_PO_PARTNER"] = row1["NO_PO_PARTNER"];
						row2["FG_TRACK"] = "ME";

						row2["TXT_USERDEF1"] = row1["TXT_USERDEF1"];
						row2["NUM_USERDEF1"] = row1["NUM_USERDEF1"];
						row2["NUM_USERDEF2"] = row1["NUM_USERDEF2"];
						row2["NUM_USERDEF3"] = row1["NUM_USERDEF3"];
						row2["NO_NEGO"] = row1["NO_NEGO"];

						object[] objArray2 = new object[] { this.LoginInfo.CompanyCode, 수주형태, VAT구분 };
						row2["STA_SO"] = !(this._biz.GetTpBusi(objArray2)[2].ToString() == "Y") ? "O" : "R";
						this.soStatus = row2["STA_SO"].ToString();
						row1["STA_SO"] = row2["STA_SO"];

						schema1.Rows.Add(row2);
					}
					else
					{
						row1["NO_SO"] = D.GetString(dataRowArray[0]["NO_SO"]);
						row1["STA_SO"] = D.GetString(dataRowArray[0]["STA_SO"]);
					}
				}

				foreach (DataRow row1 in schema1.Rows)
				{
					decimal num6 = 1;
					DataRow[] dataRowArray;

					dataRowArray = calculater.Select("NO_SO = '" + D.GetString(row1["NO_SO"]) + "'");

					foreach (DataRow dataRow in dataRowArray)
					{
						DataRow row2 = schema2.NewRow();

						row2["NO_SO"] = row1["NO_SO"];
						row2["NO_HST"] = num5;
						row2["SEQ_SO"] = num6;
						row2["CD_PLANT"] = dataRow["CD_PLANT"];
						row2["CD_ITEM"] = dataRow["CD_ITEM"];
						row2["UNIT_SO"] = dataRow["UNIT_SO"];
						row2["DT_EXPECT"] = dataRow["DT_EXPECT"];
						row2["DT_DUEDATE"] = dataRow["DT_DUEDATE"];
						row2["DT_REQGI"] = dataRow["DT_REQGI"];
						row2["QT_SO"] = dataRow["QT_SO"];
						row2["UM_SO"] = dataRow["UM_SO"];
						row2["AM_SO"] = dataRow["AM_SO"];
						row2["AM_WONAMT"] = dataRow["AM_WONAMT"];
						row2["AM_VAT"] = dataRow["AM_VAT"];
						row2["UNIT_IM"] = dataRow["UNIT_IM"];
						row2["QT_IM"] = dataRow["QT_IM"];
						row2["CD_SL"] = dataRow["CD_SL"];
						row2["TP_ITEM"] = string.Empty;
						row2["STA_SO"] = this.soStatus;
						row2["TP_BUSI"] = dataRow["TP_BUSI"];
						row2["TP_GI"] = dataRow["TP_GI"];
						row2["TP_IV"] = dataRow["TP_IV"];
						row2["GIR"] = dataRow["GIR"];
						row2["GI"] = dataRow["GI"];
						row2["IV"] = dataRow["IV"];
						row2["TRADE"] = dataRow["TRADE"];
						row2["CD_CC"] = dataRow["CD_CC"];
						row2["TP_VAT"] = dataRow["TP_VAT"];
						row2["RT_VAT"] = dataRow["RT_VAT"];
						row2["GI_PARTNER"] = dataRow["GI_PARTNER"];
						row2["NO_PROJECT"] = dataRow["NO_PROJECT"];
						row2["SEQ_PROJECT"] = 0;
						row2["UMVAT_SO"] = dataRow["UMVAT_SO"];
						row2["AMVAT_SO"] = dataRow["AMVAT_SO"];
						row2["DC1"] = dataRow["DC1"];
						row2["DC2"] = dataRow["DC2"];
						row2["NO_PO_PARTNER"] = dataRow["NO_PO_PARTNER"];
						row2["NO_POLINE_PARTNER"] = dataRow["NO_POLINE_PARTNER"];
						row2["NO_RELATION"] = dataRow["NO_RELATION"];
						row2["SEQ_RELATION"] = dataRow["SEQ_RELATION"];
						row2["NUM_USERDEF1"] = dataRow["NUM_USERDEF1"];
						row2["FG_TRACK"] = "SO";

						if (this.disCount_YN == "Y")
						{
							row2["RT_DSCNT"] = dataRow["RT_DSCNT"];
							row2["UM_BASE"] = dataRow["UM_BASE"];
						}

						row2["FG_USE"] = dataRow["FG_USE"];

						row2["TXT_USERDEF3"] = dataRow["TXT_USERDEF3"];
						row2["TXT_USERDEF4"] = dataRow["TXT_USERDEF4"];
						row2["TXT_USERDEF5"] = dataRow["TXT_USERDEF5"];
						row2["TXT_USERDEF6"] = dataRow["TXT_USERDEF6"];
						row2["TXT_USERDEF7"] = dataRow["TXT_USERDEF7"];

						row2["YN_OPTION"] = dataRow["YN_OPTION"];
						row2["CD_USERDEF1"] = dataRow["CD_USERDEF1"];
						row2["CD_USERDEF2"] = dataRow["CD_USERDEF2"];
						row2["CD_USERDEF3"] = dataRow["CD_USERDEF3"];

						schema2.Rows.Add(row2);
						++num6;
					}
				}

				if (this._biz.Get과세변경유무 == "N" && this._매출자동여부 == "Y")
				{
					foreach (DataRow row in schema1.Rows)
					{
						row["DT_PROCESS"] = row["DT_SO"];
						row["DT_RCP_RSV"] = row["DT_SO"];
						row["AM_IV"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(schema2.Compute("SUM(AM_WONAMT)", "NO_SO = '" + D.GetString(row["NO_SO"]) + "'")));
						row["AM_IV_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(schema2.Compute("SUM(AM_SO)", "NO_SO = '" + D.GetString(row["NO_SO"]) + "'")));
						row["AM_IV_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(schema2.Compute("SUM(AM_VAT)", "NO_SO = '" + D.GetString(row["NO_SO"]) + "'")));
					}
				}

				if (!this._biz.Save(schema1, schema2, this._매출자동여부))
					return false;

				this.OnToolBarAddButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
				return false;
			}
			return true;
		}

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			return base.OnToolBarExitButtonClicked(sender, e);
		}
		#endregion

		#region 컨트롤 이벤트
		private void btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this.ErrorCheck()) return;

				this._flex.Rows.Add();
				this._flex.Row = this._flex.Rows.Count - 1;

				this._flex["CD_PLANT"] = this.LoginInfo.CdPlant;
				this._flex["QT_SO"] = 0;
				this._flex["UM_SO"] = 0;
				this._flex["AM_SO"] = 0;
				this._flex["AM_VAT"] = 0;
				this._flex["QT_IM"] = 0;

				if (this.disCount_YN == "Y")
				{
					this._flex["RT_DSCNT"] = 0;
					this._flex["UM_BASE"] = 0;
				}

				this._flex["TP_VAT"] = this.cboVAT구분.SelectedValue == null ? string.Empty : this.cboVAT구분.SelectedValue.ToString();
				this._flex["RT_VAT"] = this.cur부가세율.DecimalValue;
				this._flex["CD_CC"] = this.cd_CC;
				this._flex["NM_CC"] = this.nm_CC;

				this._flex.Focus();
				this.btn삭제.Enabled = true;
				this.Authority(false);
				this.ToolBarSaveButtonEnabled = true;
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
				if (!this._flex.HasNormalRow) return;

				this._flex.Rows.Remove(this._flex.Row);

				if (!this._flex.HasNormalRow)
				{
					this.btn삭제.Enabled = false;
					this.Authority(true);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
		{
			try
			{
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				if (dlg.ShowDialog() != DialogResult.OK) return;

				string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_일괄수주등록(윈텍)_" + Global.MainFrame.GetStringToday + ".xls";
				string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_SO_BATCH_WINTEC.xls";

				System.Net.WebClient client = new System.Net.WebClient();
				client.DownloadFile(serverPath, localPath);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn엑셀업로드_Click(object sender, EventArgs e)
		{
			if (!this.ErrorCheck())
				return;

			DataTable CheckTable = null;
			DataTable dataTable = null;
			string 엑셀구분 = "단가포함";

			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";

				if (openFileDialog.ShowDialog() != DialogResult.OK)
					return;

				DataTable dt = new Excel().StartLoadExcel(openFileDialog.FileName, 0, 3);

				if (dt.Columns.Contains("AM_SO"))
					엑셀구분 = "금액포함";
				else if (!dt.Columns.Contains("AM_SO") && !dt.Columns.Contains("UM_SO"))
					엑셀구분 = "단가금액미포함";

				DataTable excelSchema = this.getExcelSchema(dt);
				bool b = false;

				if (excelSchema != null && excelSchema.Rows.Count > 0)
					CheckTable = this.SO_PK_Check(excelSchema, out b);

				if (!b)
					return;

				if (CheckTable != null && CheckTable.Rows.Count > 0)
					dataTable = this.GetCalculater(CheckTable, "N", 엑셀구분);

				this._flex.Binding = dataTable;
				this.Authority(false);
				this.btn추가.Enabled = true;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn단가적용_Click(object sender, EventArgs e)
		{
			try
			{
				DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
				}
				else
				{
					foreach (DataRow dataRow in dataRowArray)
					{
						if (dataRow.RowState != DataRowState.Deleted)
						{
							decimal um = BASIC.GetUM(D.GetString(dataRow["CD_PLANT"]), D.GetString(dataRow["CD_ITEM"]), D.GetString(dataRow["CD_PARTNER"]), this.ctx영업그룹.CodeValue, dataRow["DT_SO"].ToString(), D.GetString(this.cbo단가적용.SelectedValue), dataRow["CD_EXCH"].ToString(), "SA");
							dataRow["UM_SO"] = um;
							dataRow["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_SO"]) * D.GetDecimal(um));
							dataRow["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_SO"]) * D.GetDecimal(dataRow["RT_EXCH"]));
							dataRow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_WONAMT"]) * (D.GetDecimal(dataRow["RT_VAT"]) / 100));
							dataRow["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_WONAMT"]) + D.GetDecimal(dataRow["AM_VAT"]));
							dataRow["UMVAT_SO"] = !(D.GetDecimal(dataRow["QT_SO"]) != 0) ? D.GetDecimal(dataRow["AMVAT_SO"]) : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AMVAT_SO"]) / D.GetDecimal(dataRow["QT_SO"]));
						}
					}

					this.ShowMessage("적용되었습니다.");
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn자동등록_Click(object sender, EventArgs e)
		{
			DataTable CheckTable = null;
			DataTable dataTable = null;
			DataTable dt, dt1, dt2, dt3, tmpDt;
			DataTable dt신규, dt단가변경, dt나머지변경, dt품목변경;
			List<string> 신규List, 단가변경List, 나머지변경List, 품목변경List;
			string 엑셀구분 = "단가포함";
			string query, key, 오류메시지, 매출처;

			try
			{
				OrderParser order = new OrderParser();
				string msg = order.Parse();

				if (msg != "성공")
				{
					this.ShowMessage("파싱 실패 했습니다.");
					return;
				}
				
				dt = order.Item.Copy();

				#region 필수컬럼추가
				if (!dt.Columns.Contains("CD_PLANT"))
					dt.Columns.Add("CD_PLANT");
				if (!dt.Columns.Contains("GI_PARTNER"))
					dt.Columns.Add("GI_PARTNER");
				if (!dt.Columns.Contains("CD_SL"))
					dt.Columns.Add("CD_SL");
				if (!dt.Columns.Contains("DT_EXPECT"))
					dt.Columns.Add("DT_EXPECT");
				if (!dt.Columns.Contains("DT_REQGI"))
					dt.Columns.Add("DT_REQGI");
				if (!dt.Columns.Contains("DC_RMK"))
					dt.Columns.Add("DC_RMK");
				if (!dt.Columns.Contains("DC_RMK1"))
					dt.Columns.Add("DC_RMK1");
				if (!dt.Columns.Contains("NO_PROJECT"))
					dt.Columns.Add("NO_PROJECT");
				if (!dt.Columns.Contains("TXT_USERDEF1"))
					dt.Columns.Add("TXT_USERDEF1");
				if (!dt.Columns.Contains("NUM_USERDEF1"))
					dt.Columns.Add("NUM_USERDEF1");
				if (!dt.Columns.Contains("NUM_USERDEF2"))
					dt.Columns.Add("NUM_USERDEF2");
				if (!dt.Columns.Contains("NUM_USERDEF3"))
					dt.Columns.Add("NUM_USERDEF3");
				if (!dt.Columns.Contains("NO_NEGO"))
					dt.Columns.Add("NO_NEGO");
				if (!dt.Columns.Contains("DC1"))
					dt.Columns.Add("DC1");
				if (!dt.Columns.Contains("DC2"))
					dt.Columns.Add("DC2");
				if (!dt.Columns.Contains("TXT_USERDEF5"))
					dt.Columns.Add("TXT_USERDEF5");
				if (!dt.Columns.Contains("YN_OPTION"))
					dt.Columns.Add("YN_OPTION");
				if (!dt.Columns.Contains("TXT_USERDEF7"))
					dt.Columns.Add("TXT_USERDEF7");
				#endregion

				dt1 = dt.Clone();

				dt신규 = new DataTable();
				dt단가변경 = new DataTable();
				dt나머지변경 = new DataTable();
				dt품목변경 = new DataTable();

				신규List = new List<string>();
				단가변경List = new List<string>();
				나머지변경List = new List<string>();
				품목변경List = new List<string>();

				this.ctx영업그룹.CodeValue = "060300";
				this.ctx영업그룹.CodeName = "영업부";

				this.ctx수주형태.CodeValue = "1160";
				this.ctx수주형태.CodeName = "국내수주_원화";

				this.ctx담당자.CodeValue = "S2104";
				this.ctx담당자.CodeName = "김종연";

				DataRow tpso1 = BASIC.GetTPSO(this.ctx수주형태.CodeValue);
				this._매출자동여부 = D.GetString(tpso1["IV_AUTO"]);
				this.tp_Gi = D.GetString(tpso1["TP_GI"]);
				this.tp_Busi = D.GetString(tpso1["TP_BUSI"]);
				this.tp_Iv = D.GetString(tpso1["TP_IV"]);
				this._의뢰여부 = D.GetString(tpso1["GIR"]);
				this._출하여부 = D.GetString(tpso1["GI"]);
				this._매출여부 = D.GetString(tpso1["IV"]);
				this.trade = D.GetString(tpso1["TRADE"]);

				if (Sa_Global.IVL_CdCc == "001")
				{
					DataRow tpso2 = Sa_ComFunc.GetTPSO(new object[] { this.LoginInfo.CompanyCode, this.ctx수주형태.CodeValue });
					this.cd_CC = D.GetString(tpso2["CD_CC"]);
					this.nm_CC = D.GetString(tpso2["NM_CC"]);
				}
				else if (Sa_Global.IVL_CdCc == "000")
				{
					object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode, this.ctx영업그룹.CodeValue };
					this.so_Price = this._biz.GetSaleOrgUmCheck(objArray);
					this.cd_CC = ComFunc.MasterSearch("MA_CC", objArray);
					objArray[1] = this.cd_CC;
					this.nm_CC = ComFunc.MasterSearch("MA_CC_NAME", objArray);
				}

				this.cbo단가유형.SelectedValue = "001";

				this.cboVAT구분.SelectedValue = "11";
				this.cur부가세율.DecimalValue = 10;

				this.cd_CC = ComFunc.MasterSearch("MA_CC", new string[] { Global.MainFrame.LoginInfo.CompanyCode, "060300" });
				this.nm_CC = ComFunc.MasterSearch("MA_CC_NAME", new string[] { Global.MainFrame.LoginInfo.CompanyCode, this.cd_CC });

				오류메시지 = string.Empty;

				if (dt != null && dt.Rows.Count > 0)
					매출처 = dt.Rows[0]["CD_PARTNER"].ToString();
				else
					매출처 = string.Empty;

				if (매출처 == "STX" && dt.Rows[0]["YN_FLAG"].ToString() == "Y")
				{
					dt3 = dt.Clone();

					foreach (DataRow dr in ComFunc.getGridGroupBy(dt, new string[] { "NO_SO", "NO_PO_PARTNER", "LINE_NO" }, true).Rows)
					{
						string 차수 = dt.Compute("MAX(NO_HST)", string.Format("NO_SO = '{0}' AND NO_PO_PARTNER = '{1}' AND LINE_NO = '{2}'", dr["NO_SO"].ToString(),
																																			 dr["NO_PO_PARTNER"].ToString(),
																																			 dr["LINE_NO"].ToString())).ToString();

						foreach(DataRow dr1 in dt.Select(string.Format("NO_SO = '{0}' AND NO_PO_PARTNER = '{1}' AND LINE_NO = '{2}' AND NO_HST = '{3}'", dr["NO_SO"].ToString(),
																																						 dr["NO_PO_PARTNER"].ToString(),
																																						 dr["LINE_NO"].ToString(),
																																						 차수)))
						{
							dt3.ImportRow(dr1);
						}
					}
				}
				else
				{
					dt3 = dt;
				}

				foreach (DataRow dr in dt3.Rows)
				{
					if (dr["CD_PARTNER"].ToString() == "HD현대중공업" || 
						dr["CD_PARTNER"].ToString() == "HD현대글로벌서비스")
					{
						query = @"SELECT SH.NO_SO,
	   SH.RT_EXCH,
	   SL.SEQ_SO,
	   SL.CD_ITEM,
	   SL.TXT_USERDEF7,
	   SL.QT_SO,
	   SL.DT_REQGI,
	   SL.UM_SO,
	   SL.RT_VAT,
	   SL.STA_SO,
	   (CASE WHEN SL.QT_GIR > 0 THEN 'Y' ELSE 'N' END) AS YN_GIR
FROM SA_SOH SH WITH(NOLOCK)
JOIN SA_SOL SL WITH(NOLOCK) ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
WHERE SH.CD_COMPANY = '{0}'
AND SH.NO_SO = '{1}'";
						query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["NO_SO"].ToString());
					}
					else if (dr["CD_PARTNER"].ToString() == "STX")
					{
						query = @"SELECT SH.NO_SO,
	   SH.TXT_USERDEF1,
	   SH.RT_EXCH,
	   SL.SEQ_SO,
	   SL.CD_ITEM,
	   SL.TXT_USERDEF7,
	   SL.QT_SO,
	   SL.DT_REQGI,
	   SL.UM_SO,
	   SL.RT_VAT,
	   SL.STA_SO,
	   (CASE WHEN SL.QT_GIR > 0 THEN 'Y' ELSE 'N' END) AS YN_GIR
FROM SA_SOH SH WITH(NOLOCK)
JOIN SA_SOL SL WITH(NOLOCK) ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
WHERE SH.CD_COMPANY = '{0}'
AND SH.NO_SO LIKE '{1}%'
AND SH.NO_PO_PARTNER = '{2}'
AND SH.TXT_USERDEF1 = '{3}'";
						query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["NO_SO"].ToString(), dr["NO_PO_PARTNER"].ToString(), dr["LINE_NO"].ToString());
					}
					else
						return;
					
					dt2 = DBHelper.GetDataTable(query);

					if (dt2 != null && dt2.Rows.Count > 0)
					{
						if (dr["CD_PARTNER"].ToString() != "STX" || dr["YN_FLAG"].ToString() == "Y")
						{
							#region 변경수주
							if (dt2.Rows.Count > 1)
							{
								오류메시지 += "한 수주건에 하나이상의 품목이 등록되어 있습니다. " + dr["NO_SO"].ToString() + Environment.NewLine;
								continue;
							}

							foreach (DataRow dr1 in dt2.Rows)
							{
								key = dr1["NO_SO"].ToString() + "|" + dr1["SEQ_SO"].ToString();

								#region 단가변경
								if (D.GetDecimal(dr1["UM_SO"]) != D.GetDecimal(dr["UM_SO"]) &&
									dr1["STA_SO"].ToString() != "C")
								{
									if (dr1["YN_GIR"].ToString() == "Y")
									{
										오류메시지 += "납품의뢰된 수주건이 변경 되었습니다. " + dr["NO_SO"].ToString() + Environment.NewLine;
										continue;
									}

									decimal AM_SO = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_SO"]) * D.GetDecimal(dr["UM_SO"]));
									decimal AM_WONAMT = Unit.원화금액(DataDictionaryTypes.SA, AM_SO * D.GetDecimal(dr1["RT_EXCH"]));
									decimal AM_VAT = Unit.원화금액(DataDictionaryTypes.SA, AM_WONAMT * (D.GetDecimal(dr1["RT_VAT"]) / 100));
									decimal AMVAT_SO = Unit.원화금액(DataDictionaryTypes.SA, AM_WONAMT + AM_VAT);
									decimal UMVAT_SO = Unit.원화단가(DataDictionaryTypes.SA, AMVAT_SO / D.GetDecimal(dr["QT_SO"]));

									query = @"UPDATE SL
SET SL.UM_SO = '{3}',
    SL.AM_SO = '{4}',
	SL.AM_WONAMT = '{5}',
    SL.AM_VAT = '{6}',
	SL.AMVAT_SO = '{7}',
	SL.UMVAT_SO = '{8}',
	SL.DC2 = LEFT('{9}' + ' ' + ISNULL(SL.DC2, ''), 200)
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

									query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																 dr1["NO_SO"].ToString(),
																 dr1["SEQ_SO"].ToString(),
																 D.GetDecimal(dr["UM_SO"]),
																 AM_SO,
																 AM_WONAMT,
																 AM_VAT,
																 AMVAT_SO,
																 UMVAT_SO,
																 string.Format("단가변경 : {0} -> {1}", Util.GetTO_Money(dr1["UM_SO"]), Util.GetTO_Money(D.GetDecimal(dr["UM_SO"]))));

									DBHelper.ExecuteScalar(query);

									if (!단가변경List.Contains(key))
										단가변경List.Add(key);
								}
								#endregion

								#region 품목변경
								string 도면번호 = string.Empty;

								if (dr["CD_PARTNER"].ToString() == "HD현대중공업" ||
									dr["CD_PARTNER"].ToString() == "HD현대글로벌서비스")
								{
									dr["TXT_USERDEF7"] = dr["CD_ITEM"].ToString();
									query = @"SELECT MD.CD_ITEM 
FROM CZ_MA_DRAWING_WINTEC MD WITH(NOLOCK)
WHERE MD.CD_COMPANY = '{0}' 
AND REPLACE(REPLACE(MD.NO_DRAWING, '-', ''), '.', '') = '{1}'
AND EXISTS (SELECT 1 
			FROM MA_PITEM MI WITH(NOLOCK)
			WHERE MI.CD_COMPANY = MD.CD_COMPANY
			AND MI.CD_PLANT = MD.CD_PLANT
			AND MI.CD_ITEM = MD.CD_ITEM
			AND MI.YN_USE = 'Y')";

									if (dr["CD_ITEM"].ToString().Substring(0, 1) == "K")
									{
										도면번호 = dr["CD_ITEM"].ToString();
										도면번호 = 도면번호.Substring(1, 도면번호.Length - 1).Replace("-", string.Empty).Replace(".", string.Empty);
									}
									else if (dr["CD_ITEM"].ToString().EndsWith("-T"))
									{
										도면번호 = dr["CD_ITEM"].ToString();
										도면번호 = 도면번호.Substring(0, 도면번호.Length - 2).Replace("-", string.Empty).Replace(".", string.Empty);
									}
									else
										도면번호 = dr["CD_ITEM"].ToString().Replace("-", string.Empty).Replace(".", string.Empty);

									query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 도면번호);
								}
								else if (dr["CD_PARTNER"].ToString() == "STX")
								{
									dr["TXT_USERDEF7"] = dr["CD_ITEM"].ToString();
									query = @"SELECT MD.CD_ITEM 
FROM CZ_MA_DRAWING_WINTEC MD WITH(NOLOCK)
WHERE MD.CD_COMPANY = '{0}' 
AND MD.NO_DRAWING = '{1}'
AND EXISTS (SELECT 1 
			FROM MA_PITEM MI WITH(NOLOCK)
			WHERE MI.CD_COMPANY = MD.CD_COMPANY
			AND MI.CD_PLANT = MD.CD_PLANT
			AND MI.CD_ITEM = MD.CD_ITEM
			AND MI.YN_USE = 'Y')";

									if (dr["CD_ITEM"].ToString().Split('.').Length > 1)
										도면번호 = dr["CD_ITEM"].ToString().Split('.')[0] + "." + dr["CD_ITEM"].ToString().Split('.')[1].PadLeft(2, '0');
									else
										도면번호 = dr["CD_ITEM"].ToString();

									query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 도면번호);
								}

								tmpDt = DBHelper.GetDataTable(query);

								string CD_ITEM = string.Empty;

								if (dt2 != null && tmpDt.Rows.Count > 0)
									CD_ITEM = tmpDt.Rows[0]["CD_ITEM"].ToString();
								else
								{
									if (dr["CD_PARTNER"].ToString() == "HD현대중공업" || dr["CD_PARTNER"].ToString() == "HD현대글로벌서비스")
										오류메시지 += "품목코드를 찾을 수 없습니다. " + dr["NO_SO"].ToString() + ":" + dr["CD_ITEM"].ToString() + Environment.NewLine;
									else if (dr["CD_PARTNER"].ToString() == "STX")
										오류메시지 += "품목코드를 찾을 수 없습니다. " + dr["NO_SO"].ToString() + ":" + 도면번호 + Environment.NewLine;
									continue;
								}

								if (dr1["CD_ITEM"].ToString() != CD_ITEM)
								{
									if (dr1["YN_GIR"].ToString() == "Y")
									{
										오류메시지 += "납품의뢰된 수주건이 변경 되었습니다. " + dr["NO_SO"].ToString() + Environment.NewLine;
										continue;
									}

									query = @"UPDATE SL
SET SL.CD_ITEM = '{3}',
    SL.DC2 = LEFT('{4}' + ' ' + ISNULL(SL.DC2, ''), 200)
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

									query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																 dr1["NO_SO"].ToString(),
																 dr1["SEQ_SO"].ToString(),
																 CD_ITEM,
																 string.Format("품목변경 : {0} -> {1}", dr1["CD_ITEM"].ToString(),
																										CD_ITEM));

									DBHelper.ExecuteScalar(query);

									if (!품목변경List.Contains(key))
										품목변경List.Add(key);
								}

								if (dr1["TXT_USERDEF7"].ToString() != dr["TXT_USERDEF7"].ToString() && string.IsNullOrEmpty(dr1["TXT_USERDEF7"].ToString()) && string.IsNullOrEmpty(dr["TXT_USERDEF7"].ToString()))
								{
									if (dr1["YN_GIR"].ToString() == "Y")
									{
										오류메시지 += "납품의뢰된 수주건이 변경 되었습니다. " + dr["NO_SO"].ToString() + Environment.NewLine;
										continue;
									}

									query = @"UPDATE SL
SET SL.TXT_USERDEF7 = '{3}',
    SL.DC2 = LEFT('{4}' + ' ' + ISNULL(SL.DC2, ''), 200)
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

									query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																 dr1["NO_SO"].ToString(),
																 dr1["SEQ_SO"].ToString(),
																 dr["TXT_USERDEF7"].ToString(),
																 string.Format("도면확정 : {0} -> {1}", dr1["TXT_USERDEF7"].ToString(),
																									    dr["TXT_USERDEF7"].ToString()));

									DBHelper.ExecuteScalar(query);

									if (!나머지변경List.Contains(key))
										나머지변경List.Add(key);
								}
								#endregion

								#region 수량변경
								if (D.GetDecimal(dr1["QT_SO"]) != D.GetDecimal(dr["QT_SO"]))
								{
									if (dr1["YN_GIR"].ToString() == "Y")
									{
										오류메시지 += "납품의뢰된 수주건이 변경 되었습니다. " + dr["NO_SO"].ToString() + Environment.NewLine;
										continue;
									}

									decimal AM_SO = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dr["UM_SO"]) * D.GetDecimal(dr["QT_SO"]));
									decimal AM_WONAMT = Unit.원화금액(DataDictionaryTypes.SA, AM_SO * D.GetDecimal(dr1["RT_EXCH"]));
									decimal AM_VAT = Unit.원화금액(DataDictionaryTypes.SA, AM_WONAMT * (D.GetDecimal(dr1["RT_VAT"]) / 100));
									decimal QT_IM = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dr["QT_SO"]));
									decimal AMVAT_SO = Unit.원화금액(DataDictionaryTypes.SA, AM_WONAMT + AM_VAT);
									decimal UMVAT_SO = Unit.원화단가(DataDictionaryTypes.SA, AMVAT_SO / D.GetDecimal(dr["QT_SO"]));

									query = @"UPDATE SL
SET SL.AM_SO = '{3}',
	SL.AM_WONAMT = '{4}',
    SL.AM_VAT = '{5}',
	SL.QT_SO = '{6}',
	SL.QT_IM = '{7}',
	SL.AMVAT_SO = '{8}',
	SL.UMVAT_SO = '{9}',
	SL.DC2 = LEFT('{10}' + ' ' + ISNULL(SL.DC2, ''), 200)
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

									query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																 dr1["NO_SO"].ToString(),
																 dr1["SEQ_SO"].ToString(),
																 AM_SO,
																 AM_WONAMT,
																 AM_VAT,
																 D.GetDecimal(dr["QT_SO"]),
																 QT_IM,
																 AMVAT_SO,
																 UMVAT_SO,
																 string.Format("수량변경 : {0} -> {1}", D.GetInt(dr1["QT_SO"]), D.GetInt(dr["QT_SO"])));

									DBHelper.ExecuteScalar(query);

									if (!나머지변경List.Contains(key))
										나머지변경List.Add(key);
								}
								#endregion

								#region 납기요구일변경
								if (dr["CD_PARTNER"].ToString() == "HD현대중공업" ||
									dr["CD_PARTNER"].ToString() == "HD현대글로벌서비스")
								{
									if (dr1["DT_REQGI"].ToString() != dr["DT_DUEDATE2"].ToString())
									{
										query = @"UPDATE SL
SET SL.DT_REQGI = '{3}',
    SL.DC2 = LEFT('{4}' + ' ' + ISNULL(SL.DC2, ''), 200)
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

										query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	 dr1["NO_SO"].ToString(),
																	 dr1["SEQ_SO"].ToString(),
																	 dr["DT_DUEDATE2"].ToString(),
																	 string.Format("납기일변경 : {0} -> {1}", Util.GetTo_DateStringS(dr1["DT_REQGI"]), Util.GetTo_DateStringS(dr["DT_DUEDATE2"])));

										DBHelper.ExecuteScalar(query);

										if (!나머지변경List.Contains(key))
											나머지변경List.Add(key);
									}
								}
								else if (dr["CD_PARTNER"].ToString() == "STX")
								{
									if (dr1["DT_REQGI"].ToString() != dr["DT_DUEDATE"].ToString())
									{
										query = @"UPDATE SL
SET SL.DT_REQGI = '{3}',
    SL.DC2 = LEFT('{4}' + ' ' + ISNULL(SL.DC2, ''), 200)
FROM SA_SOL SL
WHERE SL.CD_COMPANY = '{0}'
AND SL.NO_SO = '{1}'
AND SL.SEQ_SO = '{2}'";

										query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	 dr1["NO_SO"].ToString(),
																	 dr1["SEQ_SO"].ToString(),
																	 dr["DT_DUEDATE"].ToString(),
																	 string.Format("납기일변경 : {0} -> {1}", Util.GetTo_DateStringS(dr1["DT_REQGI"]), Util.GetTo_DateStringS(dr["DT_DUEDATE"])));

										DBHelper.ExecuteScalar(query);

										if (!나머지변경List.Contains(key))
											나머지변경List.Add(key);
									}
								}
								#endregion
							}
							#endregion
						}
					}
					else
					{
						#region 신규수주
						if (dr["CD_PARTNER"].ToString() == "HD현대중공업" || 
							dr["CD_PARTNER"].ToString() == "HD현대글로벌서비스")
						{
							#region 현대중공업, 현대글로벌서비스
							dr["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
							dr["CD_SL"] = string.Empty;
							dr["DT_EXPECT"] = dr["DT_DUEDATE3"];
							dr["DT_DUEDATE"] = dr["DT_DUEDATE3"];
							dr["DT_REQGI"] = dr["DT_DUEDATE2"];
							dr["DC_RMK"] = "자동생성건";

							if (dr["CD_PARTNER"].ToString() == "HD현대중공업")
							{
								dr["CD_PARTNER"] = "17252";
								dr["GI_PARTNER"] = "17252";
							}
							else if (dr["CD_PARTNER"].ToString() == "HD현대글로벌서비스")
							{
								dr["CD_PARTNER"] = "11823";
								dr["GI_PARTNER"] = "11823";
							}

							dr["TXT_USERDEF7"] = dr["CD_ITEM"].ToString();

							#region 품목코드
							query = @"SELECT MD.CD_ITEM 
FROM CZ_MA_DRAWING_WINTEC MD WITH(NOLOCK)
WHERE MD.CD_COMPANY = '{0}' 
AND REPLACE(REPLACE(MD.NO_DRAWING, '-', ''), '.', '') = '{1}'
AND EXISTS (SELECT 1 
			FROM MA_PITEM MI WITH(NOLOCK)
			WHERE MI.CD_COMPANY = MD.CD_COMPANY
			AND MI.CD_PLANT = MD.CD_PLANT
			AND MI.CD_ITEM = MD.CD_ITEM
			AND MI.YN_USE = 'Y')";

							string 도면번호 = string.Empty;

							if (dr["CD_ITEM"].ToString().Substring(0, 1) == "K")
							{
								도면번호 = dr["CD_ITEM"].ToString();
							    도면번호 = 도면번호.Substring(1, 도면번호.Length -1).Replace("-", string.Empty).Replace(".", string.Empty);
							}
							else if (dr["CD_ITEM"].ToString().EndsWith("-T"))
							{
								도면번호 = dr["CD_ITEM"].ToString();
								도면번호 = 도면번호.Substring(0, 도면번호.Length - 2).Replace("-", string.Empty).Replace(".", string.Empty);
							}
							else
								도면번호 = dr["CD_ITEM"].ToString().Replace("-", string.Empty).Replace(".", string.Empty);

							query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 도면번호);

							dt2 = DBHelper.GetDataTable(query);

							if (dt2 != null && dt2.Rows.Count > 0)
								dr["CD_ITEM"] = dt2.Rows[0]["CD_ITEM"];
							else
							{
								오류메시지 += "품목코드를 찾을 수 없습니다. " + dr["NO_SO"].ToString() + ":" + dr["CD_ITEM"].ToString() + Environment.NewLine;
								continue;
							}
							#endregion

							#region 통화코드
							query = @"SELECT CD_SYSDEF AS CD_EXCH
FROM MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_FIELD = 'MA_B000005'
AND NM_SYSDEF = '{1}'";

							query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["CD_EXCH"].ToString());

							dt2 = DBHelper.GetDataTable(query);

							if (dt2 != null && dt2.Rows.Count > 0)
								dr["CD_EXCH"] = dt2.Rows[0]["CD_EXCH"];
							else
							{
								오류메시지 += "통화코드를 찾을 수 없습니다. " + dr["NO_SO"].ToString() + ":" + dr["CD_EXCH"].ToString() + Environment.NewLine;
								continue;
							}
							#endregion

							if (!string.IsNullOrEmpty(dr["CD_USERDEF3"].ToString()))
							{
								#region 엔진타입
								string 엔진타입 = dr["CD_USERDEF3"].ToString();
								엔진타입 = (엔진타입.Substring(엔진타입.Length - 1, 1) == "-" ? 엔진타입.Substring(0, 엔진타입.Length - 1) : 엔진타입);

								query = @"SELECT CD_SYSDEF AS CD_USERDEF3
FROM MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_FIELD = 'CZ_WIN0003'
AND NM_SYSDEF = '{1}'";

								query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 엔진타입);

								dt2 = DBHelper.GetDataTable(query);

								if (dt2 != null && dt2.Rows.Count > 0)
									dr["CD_USERDEF3"] = dt2.Rows[0]["CD_USERDEF3"];
								else
								{
									query = @"SELECT ISNULL(MAX(CD_SYSDEF), 0) + 1 AS CD_SYSDEF
FROM MA_CODEDTL
WHERE CD_COMPANY = '{0}'
AND CD_FIELD = 'CZ_WIN0003'";

									dr["CD_USERDEF3"] = DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

									query = @"INSERT INTO MA_CODEDTL
(
	CD_COMPANY,
	CD_FIELD,
	CD_SYSDEF,
	FG1_SYSCODE,
	NM_SYSDEF,
	USE_YN,
	ID_INSERT,
	DTS_INSERT
)
VALUES
(
	'{0}',
	'CZ_WIN0003',
	'{1}',
	'N',
	'{2}',
	'Y',
	'AUTO',
	NEOE.SF_SYSDATE(GETDATE())
)";
									DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																				dr["CD_USERDEF3"].ToString(),
																				엔진타입));
								}
								#endregion
							}
							#endregion
						}
						else if (dr["CD_PARTNER"].ToString() == "STX")
						{
							#region STX
							dr["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;
							dr["CD_SL"] = string.Empty;
							dr["DT_REQGI"] = dr["DT_DUEDATE"];
							dr["DC_RMK"] = "자동생성건";

							dr["CD_PARTNER"] = "00102";
							dr["GI_PARTNER"] = "00102";

							dr["TXT_USERDEF1"] = dr["LINE_NO"].ToString();
							dr["TXT_USERDEF7"] = dr["CD_ITEM"].ToString();
							dr["TXT_USERDEF5"] = "STX중공업";

							int suffix = 0, suffix1 = 0;

							if (dt1.AsEnumerable().Where(x => x["NO_SO"].ToString().StartsWith(dr["NO_SO"].ToString())).Count() > 0)
							{
								suffix = dt1.AsEnumerable().Where(x => x["NO_SO"].ToString().StartsWith(dr["NO_SO"].ToString()))
														   .Max(x => Convert.ToInt32(x["NO_SO"].ToString().Split('-')[1].Replace("W", string.Empty)));
							}
							
							query = @"SELECT MAX(CONVERT(INT, REPLACE((SUBSTRING(NO_SO, CHARINDEX('-', NO_SO), LEN(NO_SO))), '-W', ''))) AS SUFFIX
									  FROM SA_SOH SH
									  WHERE SH.CD_COMPANY = '{0}'
									  AND SH.CD_PARTNER = '{1}'
									  AND SH.NO_SO LIKE '{2}%'
									  AND SH.NO_SO LIKE '%-W%'";

							query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, "00102", dr["NO_SO"].ToString());

							dt2 = DBHelper.GetDataTable(query);

							if (dt2 != null && dt2.Rows.Count > 0)
								suffix1 = Convert.ToInt32(dt2.Rows[0]["SUFFIX"] == DBNull.Value ? 0 : dt2.Rows[0]["SUFFIX"]);

						    if (suffix >= suffix1)
								dr["NO_SO"] = dr["NO_SO"].ToString() + "-W" + (suffix + 1).ToString("D2");
							else
								dr["NO_SO"] = dr["NO_SO"].ToString() + "-W" + (suffix1 + 1).ToString("D2");

							#region 품목코드
							query = @"SELECT MD.CD_ITEM 
FROM CZ_MA_DRAWING_WINTEC MD WITH(NOLOCK)
WHERE MD.CD_COMPANY = '{0}' 
AND MD.NO_DRAWING = '{1}'
AND EXISTS (SELECT 1 
			FROM MA_PITEM MI WITH(NOLOCK)
			WHERE MI.CD_COMPANY = MD.CD_COMPANY
			AND MI.CD_PLANT = MD.CD_PLANT
			AND MI.CD_ITEM = MD.CD_ITEM
			AND MI.YN_USE = 'Y')";

							string 도면번호 = string.Empty;

							if (dr["CD_ITEM"].ToString().Split('.').Length > 1)
								도면번호 = dr["CD_ITEM"].ToString().Split('.')[0] + "." + dr["CD_ITEM"].ToString().Split('.')[1].PadLeft(2, '0');
							else
								도면번호 = dr["CD_ITEM"].ToString();

							query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 도면번호);

							dt2 = DBHelper.GetDataTable(query);

							if (dt2 != null && dt2.Rows.Count > 0)
								dr["CD_ITEM"] = dt2.Rows[0]["CD_ITEM"];
							else
							{
								오류메시지 += "품목코드를 찾을 수 없습니다. " + dr["NO_SO"].ToString() + ":" + 도면번호 + Environment.NewLine;
								continue;
							}
							#endregion

							#region 통화코드
							query = @"SELECT CD_SYSDEF AS CD_EXCH
FROM MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_FIELD = 'MA_B000005'
AND NM_SYSDEF = '{1}'";

							query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["CD_EXCH"].ToString());

							dt2 = DBHelper.GetDataTable(query);

							if (dt2 != null && dt2.Rows.Count > 0)
								dr["CD_EXCH"] = dt2.Rows[0]["CD_EXCH"];
							else
							{
								오류메시지 += "통화코드를 찾을 수 없습니다. " + dr["NO_SO"].ToString() + ":" + dr["CD_EXCH"].ToString() + Environment.NewLine;
								continue;
							}
							#endregion

							#endregion
						}
						#endregion

						dt1.ImportRow(dr);

						if (!신규List.Contains(dr["NO_SO"].ToString()))
							신규List.Add(dr["NO_SO"].ToString());
					}
				}

				query = @"SELECT SH.NO_SO,
	   SL.SEQ_SO,
	   SH.NO_PO_PARTNER,
	   SH.DT_SO,
	   ME.NM_KOR,
	   MP.LN_PARTNER,
	   SL.TXT_USERDEF6,
	   CD.NM_SYSDEF AS NM_ENGINE,
	   MI.NM_ITEM,
	   MI.NO_DESIGN,
	   SL.QT_SO,
	   SL.UM_SO,
	   SL.DT_DUEDATE,
	   SL.NUM_USERDEF3,
	   SL.DC1,
	   SL.DC2,
	   SL.CD_USERDEF1,
	   SL.CD_USERDEF2
FROM SA_SOH SH WITH(NOLOCK)
JOIN SA_SOL SL WITH(NOLOCK) ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
LEFT JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = SH.CD_COMPANY AND ME.NO_EMP = SH.NO_EMP
LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN MA_PITEM MI WITH(NOLOCK) ON MI.CD_COMPANY = SL.CD_COMPANY AND MI.CD_PLANT = SL.CD_PLANT AND MI.CD_ITEM = SL.CD_ITEM
LEFT JOIN MA_CODEDTL CD WITH(NOLOCK) ON CD.CD_COMPANY = SL.CD_COMPANY AND CD.CD_FIELD = 'CZ_WIN0003' AND CD.CD_SYSDEF = SL.CD_USERDEF3
WHERE SH.CD_COMPANY = '{0}'
AND SH.NO_SO = '{1}'
AND ('{2}' = '' OR SL.SEQ_SO = '{2}')";

				tmpDt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, string.Empty, string.Empty));

				dt신규 = tmpDt.Clone();
				dt단가변경 = tmpDt.Clone();
				dt나머지변경 = tmpDt.Clone();
				dt품목변경 = tmpDt.Clone();

				if (단가변경List.Count > 0)
				{
					foreach (string 수주번호 in 단가변경List)
					{
						string[] 수주 = 수주번호.Split('|');
						tmpDt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 수주[0].ToString(), 수주[1].ToString()));

						foreach (DataRow dr in tmpDt.Rows)
							dt단가변경.ImportRow(dr);
					}

					this.수주통보("002", dt단가변경);
				}

				if (품목변경List.Count > 0)
				{
					foreach (string 수주번호 in 품목변경List)
					{
						string[] 수주 = 수주번호.Split('|');
						tmpDt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 수주[0].ToString(), 수주[1].ToString()));

						foreach (DataRow dr in tmpDt.Rows)
							dt품목변경.ImportRow(dr);
					}

					this.수주통보("004", dt품목변경);
				}

				if (나머지변경List.Count > 0)
				{
					foreach (string 수주번호 in 나머지변경List)
					{
						string[] 수주 = 수주번호.Split('|');
						tmpDt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 수주[0].ToString(), 수주[1].ToString()));

						foreach (DataRow dr in tmpDt.Rows)
							dt나머지변경.ImportRow(dr);
					}

					this.수주통보("003", dt나머지변경);
				}

				if (신규List.Count > 0)
				{
					if (dt1.Columns.Contains("AM_SO"))
						엑셀구분 = "금액포함";
					else if (!dt1.Columns.Contains("AM_SO") && !dt1.Columns.Contains("UM_SO"))
						엑셀구분 = "단가금액미포함";

					DataTable excelSchema = this.getExcelSchema(dt1);
					bool b = false;

					if (excelSchema != null && excelSchema.Rows.Count > 0)
						CheckTable = this.SO_PK_Check(excelSchema, out b);

					if (!b)
						return;

					if (CheckTable != null && CheckTable.Rows.Count > 0)
						dataTable = this.GetCalculater(CheckTable, "N", 엑셀구분);

					this._flex.Binding = dataTable;
					this.Authority(false);
					this.btn추가.Enabled = true;

					if (!this.SaveData()) return;

					foreach (string 수주번호 in 신규List)
					{
						tmpDt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 수주번호, string.Empty));

						foreach (DataRow dr in tmpDt.Rows)
							dt신규.ImportRow(dr);
					}

					if (dt신규.Rows.Count > 0)
						this.수주통보("001", dt신규);
				}

				if (!string.IsNullOrEmpty(오류메시지))
					this.메일발송("taewan.kim@dintec.co.kr;khkim@dintec.co.kr;jykim@win-tec.co.kr;srkim@win-tec.co.kr;hsjo@win-tec.co.kr;bcpark@win-tec.co.kr;scyang@win-tec.co.kr", "자동수주등록 오류",
								 오류메시지 + Environment.NewLine + string.Format("매출처 : {0} => 신규 : {1}건, 단가변경 : {2}건, 나머지변경 : {3}건, 품목변경 : {4}건", 매출처,
																																									   신규List.Count.ToString(),
																																									   단가변경List.Count.ToString(),
																																									   나머지변경List.Count.ToString(),
																																									   품목변경List.Count.ToString()), string.Empty);
				else
					this.메일발송("taewan.kim@dintec.co.kr;khkim@dintec.co.kr", "자동수주등록 결과", string.Format("매출처 : {0} => 신규 : {1}건, 단가변경 : {2}건, 나머지변경 : {3}건, 품목변경 : {4}건", 매출처,
																																																	   신규List.Count.ToString(),
																																																	   단가변경List.Count.ToString(),
																																																	   나머지변경List.Count.ToString(),
																																																	   품목변경List.Count.ToString()), string.Empty);

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn자동등록.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void ctx수주형태_TextChanged(object sender, EventArgs e)
		{
			try
			{
				string[] tpBusi = this._biz.GetTpBusi(new object[] { this.LoginInfo.CompanyCode, this.ctx수주형태.CodeValue.ToString(), this.cboVAT구분.SelectedValue.ToString() });

				if (tpBusi[0] == "001")
				{
					this.cboVAT구분.SelectedValue = "11";
					this.cboVAT구분.Enabled = true;
				}
				else if (tpBusi[0] == "002" || tpBusi[0] == "003")
				{
					this.cboVAT구분.SelectedValue = "14";
					this.cboVAT구분.Enabled = false;
				}
				else if (tpBusi[0] == "004" || tpBusi[0] == "005")
				{
					this.cboVAT구분.SelectedValue = "15";
					this.cboVAT구분.Enabled = false;
				}
				else
				{
					this.ShowMessage("수주유형에 해당하는 해당 거래구분이 존재하지 않습니다.");
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void cboVAT구분_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.cboVAT구분.SelectedValue == null || this.cboVAT구분.SelectedValue.ToString() == string.Empty || this.cboVAT구분.DataSource == null)
			{
				this.cur부가세율.DecimalValue = 0;
			}
			else
			{
				DataRow dataRow = ((DataTable)this.cboVAT구분.DataSource).Rows.Find(this.cboVAT구분.SelectedValue);

				if (dataRow != null)
					this.cur부가세율.DecimalValue = D.GetDecimal(dataRow["CD_FLAG1"]);
				else
					this.cur부가세율.DecimalValue = 0;
			}
		}

		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			HelpID helpId = e.HelpID;

			if (helpId != HelpID.P_USER)
			{
				if (helpId != HelpID.P_SA_TPSO_SUB)
					return;

				e.HelpParam.P61_CODE1 = "N";
				e.HelpParam.P62_CODE2 = "Y";
			}
		}

		private void Control_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				HelpID helpId = e.HelpID;
				string name = ((Control)sender).Name;

				if (name == this.ctx수주형태.Name)
				{
					this.soStatus = !(e.HelpReturn.Rows[0]["CONF"].ToString() == "Y") ? "O" : "R";

					if (this._flex.DataTable != null)
					{
						foreach (DataRow row in this._flex.DataTable.Rows)
						{
							if (row.RowState != DataRowState.Deleted)
								row["STA_SO"] = this.soStatus;
						}
					}

					this.tp_Gi = e.HelpReturn.Rows[0]["TP_GI"].ToString();
					this.tp_Busi = e.HelpReturn.Rows[0]["TP_BUSI"].ToString();
					this.tp_Iv = e.HelpReturn.Rows[0]["TP_IV"].ToString();
					this._의뢰여부 = e.HelpReturn.Rows[0]["GIR"].ToString();
					this._출하여부 = e.HelpReturn.Rows[0]["GI"].ToString();
					this._매출여부 = e.HelpReturn.Rows[0]["IV"].ToString();
					this.trade = e.HelpReturn.Rows[0]["TRADE"].ToString();
					this._매출자동여부 = D.GetString(BASIC.GetTPSO(e.CodeValue)["IV_AUTO"]);

					if (!(Sa_Global.IVL_CdCc == "001"))
						return;

					DataRow tpso = Sa_ComFunc.GetTPSO(new object[] { this.LoginInfo.CompanyCode, e.HelpReturn.Rows[0]["TP_SO"].ToString() });

					this.cd_CC = D.GetString(tpso["CD_CC"].ToString());
					this.nm_CC = D.GetString(tpso["NM_CC"].ToString());
				}
				else if (name == this.ctx영업그룹.Name)
				{
					this.tp_SalePrice = e.HelpReturn.Rows[0]["TP_SALEPRICE"].ToString();

					object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode, e.HelpReturn.Rows[0]["CD_SALEGRP"].ToString() };

					this.so_Price = this._biz.GetSaleOrgUmCheck(objArray);

					if (!(Sa_Global.IVL_CdCc == "000"))
						return;

					this.cd_CC = ComFunc.MasterSearch("MA_CC", objArray);
					objArray[1] = this.cd_CC;
					this.nm_CC = ComFunc.MasterSearch("MA_CC_NAME", objArray);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 그리드이벤트
		private void _flex_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				switch (this._flex.Cols[e.Col].Name)
				{
					case "CD_ITEM":
						if (!(D.GetString(this._flex["CD_ITEM_PARTNER"]) != string.Empty))
							break;

						e.Cancel = true;

						break;
					case "UM_SO":
					case "AM_SO":
						if (!(this.so_Price == "Y"))
							break;

						this.ShowMessage("영업단가통제된 영업그룹입니다.");
						e.Cancel = true;

						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex.HasNormalRow || (!(this._flex.Cols[this._flex.Col].Name == "UM_SO") || !(this._flex["STA_SO"].ToString() == "") && !(this._flex["STA_SO"].ToString() == "O") || this.so_Price == "Y"))
					return;

				P_SA_UM_HISTORY_SUB pSaUmHistorySub = new P_SA_UM_HISTORY_SUB(this._flex["CD_PARTNER"].ToString(), this._flex["LN_PARTNER"].ToString(), this.ctx수주형태.CodeValue, this.ctx수주형태.CodeName, this._flex["CD_PLANT"].ToString(), this._flex["CD_ITEM"].ToString(), this._flex["NM_ITEM"].ToString(), this._flex["CD_EXCH"].ToString());
				if (pSaUmHistorySub.ShowDialog() == DialogResult.OK)
				{
					if (this.disCount_YN == "N")
					{
						this._flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, pSaUmHistorySub.단가);
					}
					else
					{
						this._flex["UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, pSaUmHistorySub.단가);
						this._flex["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex["UM_BASE"]) - D.GetDecimal(this._flex["UM_BASE"]) * D.GetDecimal(this._flex["RT_DSCNT"]) / 100);
					}

					this._flex["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["QT_SO"]) * D.GetDecimal(this._flex["UM_SO"]));
					this._flex["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_SO"]) * D.GetDecimal(this._flex["RT_EXCH"]));
					this._flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_WONAMT"]) * (D.GetDecimal(this._flex["RT_VAT"]) / 100));
					this._flex["AMVAT_SO"] = (D.GetDecimal(this._flex["AM_WONAMT"]) + D.GetDecimal(this._flex["AM_VAT"]));

					if (D.GetDecimal(this._flex["QT_SO"]) != 0)
						this._flex["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AMVAT_SO"]) / D.GetDecimal(this._flex["QT_SO"]));
					else
						this._flex["UMVAT_SO"] = D.GetDecimal(this._flex["AMVAT_SO"]);
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
				string str = ((FlexGrid)sender)[e.Row, e.Col].ToString();
				string editData = ((Dass.FlexGrid.FlexGrid)sender).EditData;

				if (str.ToUpper() == editData.ToUpper())
					return;

				string name = ((FlexGrid)sender).Cols[e.Col].Name;

				switch (this._flex.Cols[e.Col].Name)
				{
					case "CD_PLANT":
						this._flex[e.Row, "CD_ITEM"] = string.Empty;
						this._flex[e.Row, "NM_ITEM"] = string.Empty;
						this._flex[e.Row, "STND_ITEM"] = string.Empty;
						this._flex[e.Row, "UNIT_SO"] = string.Empty;
						this._flex[e.Row, "UNIT_IM"] = string.Empty;
						this._flex[e.Row, "TP_ITEM"] = string.Empty;
						this._flex[e.Row, "DT_REQGI"] = string.Empty;
						this._flex[e.Row, "QT_IM"] = 0;
						this._flex[e.Row, "CD_SL"] = string.Empty;
						this._flex[e.Row, "NM_SL"] = string.Empty;
						break;
					case "DT_SO":
						if (this._flex[e.Row, "CD_EXCH"].ToString() == "000")
							this._flex[e.Row, "RT_EXCH"] = 1;
						else
						{
							if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
								this._flex[e.Row, "RT_EXCH"] = MA.기준환율적용(this._flex[e.Row, "DT_SO"].ToString(), this._flex[e.Row, "CD_EXCH"].ToString());
						}

						this._flex[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_SO"]) * D.GetDecimal(this._flex[e.Row, "RT_EXCH"]));
						this._flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
						this._flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) + D.GetDecimal(this._flex[e.Row, "AM_VAT"]));

						if (D.GetDecimal(this._flex[e.Row, "QT_SO"]) != 0)
							this._flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]) / D.GetDecimal(this._flex[e.Row, "QT_SO"]));
						else
							this._flex[e.Row, "UMVAT_SO"] = D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]);
						break;
					case "DT_DUEDATE":
						if (editData.Trim().Length != 8)
						{
							this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);

							if (this._flex.Editor != null)
								this._flex.Editor.Text = string.Empty;

							e.Cancel = true;
							break;
						}

						if (!this._flex.IsDate(name))
						{
							this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);

							if (this._flex.Editor != null)
								this._flex.Editor.Text = string.Empty;

							e.Cancel = true;
							break;
						}

						if (this._flex[e.Row, "LT_GI"] != DBNull.Value || this._flex[e.Row, "LT_GI"] != null)
						{
							this._flex[e.Row, "DT_REQGI"] = this._CommFun.DateAdd(editData, "D", D.GetInt(this._flex[e.Row, "LT_GI"]) * -1);
							break;
						}

						break;
					case "DT_REQGI":
						if (editData.Trim().Length != 8)
						{
							this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);

							if (this._flex.Editor != null)
								this._flex.Editor.Text = string.Empty;

							e.Cancel = true;
							break;
						}

						if (!this._flex.IsDate(name))
						{
							this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);

							if (this._flex.Editor != null)
								this._flex.Editor.Text = string.Empty;

							e.Cancel = true;
							break;
						}
						break;
					case "TP_VAT":
						object[] objArray = new object[] { this.LoginInfo.CompanyCode, this.ctx수주형태.CodeValue, editData };
						this._flex[e.Row, "RT_VAT"] = D.GetDecimal(Sa_ComFunc.GetTpBusi(objArray)[1].ToString());
						this._flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
						this._flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) + D.GetDecimal(this._flex[e.Row, "AM_VAT"]));

						if (D.GetDecimal(editData) != 0)
						{
							this._flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]) / D.GetDecimal(this._flex[e.Row, "QT_SO"]));
							break;
						}

						this._flex[e.Row, "UMVAT_SO"] = D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]);
						break;
					case "QT_SO":
						this._flex[e.Row, "UNIT_SO_FACT"] = D.GetDecimal(this._flex[e.Row, "UNIT_SO_FACT"]) == 0 ? 1 : this._flex[e.Row, "UNIT_SO_FACT"];
						this._flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "UM_SO"]) * D.GetDecimal(editData));
						this._flex[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_SO"]) * D.GetDecimal(this._flex[e.Row, "RT_EXCH"]));
						this._flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
						this._flex[e.Row, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(editData) * D.GetDecimal(this._flex[e.Row, "UNIT_SO_FACT"]));
						this._flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) + D.GetDecimal(this._flex[e.Row, "AM_VAT"]));

						if (D.GetDecimal(editData) != 0)
						{
							this._flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "UMVAT_SO"]) / D.GetDecimal(this._flex[e.Row, "QT_SO"]));
							break;
						}

						this._flex[e.Row, "UMVAT_SO"] = D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]);
						break;
					case "UM_SO":
						this._flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "QT_SO"]) * D.GetDecimal(editData));
						this._flex[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_SO"]) * D.GetDecimal(this._flex[e.Row, "RT_EXCH"]));
						this._flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
						this._flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) + D.GetDecimal(this._flex[e.Row, "AM_VAT"]));

						if (D.GetDecimal(this._flex[e.Row, "QT_SO"]) != 0)
						{
							this._flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]) / D.GetDecimal(this._flex[e.Row, "QT_SO"]));
							break;
						}

						this._flex[e.Row, "UMVAT_SO"] = D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]);
						break;
					case "AM_SO":
						if (D.GetDecimal(this._flex[e.Row, "QT_SO"]) != 0)
							this._flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(editData) / D.GetDecimal(this._flex[e.Row, "QT_SO"]));
						else
							this._flex[e.Row, "UM_SO"] = 0;

						this._flex[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(editData) * D.GetDecimal(this._flex[e.Row, "RT_EXCH"]));
						this._flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
						this._flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) + D.GetDecimal(this._flex[e.Row, "AM_VAT"]));

						if (D.GetDecimal(this._flex[e.Row, "QT_SO"]) != 0)
						{
							this._flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]) / D.GetDecimal(this._flex[e.Row, "QT_SO"]));
							break;
						}

						this._flex[e.Row, "UMVAT_SO"] = D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]);
						break;
					case "RT_DSCNT":
						if (D.GetDecimal(this._flex[e.Row, "UM_BASE"]) != 0)
							this._flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "UM_BASE"]) - D.GetDecimal(this._flex[e.Row, "UM_BASE"]) * D.GetDecimal(editData) / 100);
						else
							this._flex[e.Row, "UM_SO"] = 0;

						this._flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "QT_SO"]) * D.GetDecimal(this._flex[e.Row, "UM_SO"]));
						this._flex[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_SO"]) * D.GetDecimal(this._flex[e.Row, "RT_EXCH"]));
						this._flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
						this._flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) + D.GetDecimal(this._flex[e.Row, "AM_VAT"]));

						if (D.GetDecimal(this._flex[e.Row, "QT_SO"]) != 0)
						{
							this._flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]) / D.GetDecimal(this._flex[e.Row, "QT_SO"]));
							break;
						}

						this._flex[e.Row, "UMVAT_SO"] = D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]);
						break;
					case "UM_BASE":
						if (D.GetDecimal(this._flex[e.Row, "RT_DSCNT"]) != 0)
							this._flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(editData) - D.GetDecimal(editData) * D.GetDecimal(this._flex[e.Row, "RT_DSCNT"]) / 100);
						else
							this._flex[e.Row, "UM_SO"] = 0;

						this._flex[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "QT_SO"]) * D.GetDecimal(this._flex[e.Row, "UM_SO"]));
						this._flex[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_SO"]) * D.GetDecimal(this._flex[e.Row, "RT_EXCH"]));
						this._flex[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
						this._flex[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AM_WONAMT"]) + D.GetDecimal(this._flex[e.Row, "AM_VAT"]));

						if (D.GetDecimal(this._flex[e.Row, "QT_SO"]) != 0)
						{
							this._flex[e.Row, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]) / D.GetDecimal(this._flex[e.Row, "QT_SO"]));
							break;
						}

						this._flex[e.Row, "UMVAT_SO"] = D.GetDecimal(this._flex[e.Row, "AMVAT_SO"]);
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			try
			{
				if (this._flex["STA_SO"] != null && this._flex["STA_SO"].ToString() != "" && this._flex["STA_SO"].ToString() != "O")
				{
					e.Cancel = true;
				}
				else
				{
					switch (this._flex.Cols[e.Col].Name)
					{
						case "CD_EXCH":
							e.Parameter.P41_CD_FIELD1 = "MA_B000005";
							break;
						case "CD_ITEM":
							if (D.GetString(this._flex["CD_ITEM_PARTNER"]) != string.Empty)
								e.Cancel = true;

							if (this._flex[this._flex.Row, "CD_PLANT"].ToString().Equals(""))
							{
								e.Cancel = true;
								break;
							}

							e.Parameter.P09_CD_PLANT = this._flex["CD_PLANT"].ToString();
							break;
						case "CD_SL":
							if (this._flex[this._flex.Row, "CD_PLANT"].ToString().Equals(""))
							{
								e.Cancel = true;
								break;
							}

							e.Parameter.P09_CD_PLANT = this._flex["CD_PLANT"].ToString();
							break;
						case "GI_PARTNER":
							e.Parameter.P14_CD_PARTNER = this._flex["CD_PARTNER"].ToString();
							break;
						case "CD_ITEM_PARTNER":
							string str = D.GetString(this._flex["CD_PARTNER"]);
							if (str == string.Empty)
							{
								this.ShowMessage((공통메세지)3, new string[] { this.DD("거래처") });
								e.Cancel = true;
							}

							e.Parameter.P14_CD_PARTNER = str;
							e.Parameter.P09_CD_PLANT = D.GetString(this._flex["CD_PLANT"]);
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
			DataTable dt;

			try
			{
				HelpReturn result = e.Result;
				string str = "";
				object[] objArray = new object[] { this.LoginInfo.CompanyCode,
												   "",
												   this._flex["CD_PARTNER"].ToString(),
												   this.cbo단가유형.SelectedValue.ToString(),
												   this._flex["CD_EXCH"].ToString(),
												   this.tp_SalePrice,
												   this._flex["DT_SO"].ToString() };

				switch (this._flex.Cols[e.Col].Name)
				{
					case "CD_EXCH":
						if (e.Result.CodeValue == "000")
							this._flex["RT_EXCH"] = 1;
						else
						{
							if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
								this._flex["RT_EXCH"] = MA.기준환율적용(this._flex["DT_SO"].ToString(), e.Result.CodeValue);
						}

						this._flex["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_SO"]) * D.GetDecimal(this._flex["RT_EXCH"]));
						this._flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_WONAMT"]) * (D.GetDecimal(this._flex["RT_VAT"]) / 100));
						this._flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_WONAMT"]) + D.GetDecimal(this._flex["AM_VAT"]));

						if (D.GetDecimal(this._flex[e.Row, "QT_SO"]) != 0)
							this._flex["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AMVAT_SO"]) / D.GetDecimal(this._flex["QT_SO"]));
						else
							this._flex["UMVAT_SO"] = D.GetDecimal(this._flex["AMVAT_SO"]);
						break;
					case "CD_ITEM":
						if (e.Result.DialogResult == DialogResult.Cancel)
							break;

						if (this._flex.Rows.Count > 2 && this._flex["DT_DUEDATE"].ToString() != "")
							str = this._flex["DT_DUEDATE"].ToString();

						int num = 0;
						this._flex.Redraw = false;

						foreach (DataRow row in result.Rows)
						{
							int count = this._flex.Rows.Count;

							if (num > 0)
							{
								this.btn추가_Click(sender, e);
								this._flex.Row = count;
							}

							this._flex[this._flex.Row, "CD_ITEM"] = row["CD_ITEM"];
							this._flex[this._flex.Row, "NM_ITEM"] = row["NM_ITEM"];
							this._flex[this._flex.Row, "STND_ITEM"] = row["STND_ITEM"];
							this._flex[this._flex.Row, "UNIT_SO"] = row["UNIT_SO"];
							this._flex[this._flex.Row, "UNIT_IM"] = row["UNIT_IM"];
							this._flex[this._flex.Row, "TP_ITEM"] = row["TP_ITEM"];
							this._flex[this._flex.Row, "CD_SL"] = row["CD_GISL"];
							this._flex[this._flex.Row, "NM_SL"] = row["NM_GISL"];
							this._flex[this._flex.Row, "LT_GI"] = D.GetDecimal(row["LT_GI"]);
							this._flex[this._flex.Row, "CD_CC"] = this.cd_CC;
							this._flex[this._flex.Row, "NM_CC"] = this.nm_CC;
							this._flex[this._flex.Row, "TP_VAT"] = (this.cboVAT구분).SelectedValue.ToString();
							this._flex[this._flex.Row, "RT_VAT"] = D.GetDecimal(this._flex[e.Row, "RT_VAT"]);
							this._flex[this._flex.Row, "UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];
							this._flex[this._flex.Row, "GRP_ITEM"] = row["GRP_ITEM"];
							this._flex[this._flex.Row, "CLS_L"] = row["CLS_L"];
							this._flex[this._flex.Row, "CLS_S"] = row["CLS_S"];
							this._flex[this._flex.Row, "NUM_STND_ITEM_1"] = row["NUM_STND_ITEM_1"];
							this._flex[this._flex.Row, "NUM_STND_ITEM_2"] = row["NUM_STND_ITEM_2"];
							this._flex[this._flex.Row, "NUM_STND_ITEM_3"] = row["NUM_STND_ITEM_3"];
							this._flex[this._flex.Row, "NUM_STND_ITEM_4"] = row["NUM_STND_ITEM_4"];
							this._flex[this._flex.Row, "NUM_STND_ITEM_5"] = row["NUM_STND_ITEM_5"];

							if (str != "")
								this._flex[this._flex.Row, "DT_REQGI"] = this._CommFun.DateAdd(str, "D", Convert.ToInt32(this._flex[this._flex.Row, "LT_GI"]) * -1);

							if (this.tp_SalePrice == "002" || this.tp_SalePrice == "003")
							{
								objArray[1] = row["CD_ITEM"];

								if (this.disCount_YN == "N")
									this._flex[this._flex.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, this._biz.UmSearch(objArray));
								else if (this.disCount_YN == "Y")
								{
									this._flex[this._flex.Row, "UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, this._biz.UmSearch(objArray));
									this._flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "UM_BASE"]) - D.GetDecimal(this._flex[e.Row, "UM_BASE"]) * D.GetDecimal(this._flex[e.Row, "RT_DSCNT"]) / 100);
								}
							}

							if (num == 0)
							{
								this._flex["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["UM_SO"]) * D.GetDecimal(this._flex["QT_SO"]));
								this._flex["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_SO"]) * D.GetDecimal(this._flex["RT_EXCH"]));
								this._flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
								this._flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_WONAMT"]) + D.GetDecimal(this._flex["AM_VAT"]));

								if (D.GetDecimal(this._flex["QT_SO"]) != 0)
									this._flex["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AMVAT_SO"]) / D.GetDecimal(this._flex["QT_SO"]));
								else
									this._flex["UMVAT_SO"] = D.GetDecimal(this._flex["AMVAT_SO"]);

								this.btn삭제.Enabled = true;
							}

							++num;
						}

						this._flex.Redraw = true;
						break;
					case "CD_ITEM_PARTNER":
						if (e.Result.DialogResult == DialogResult.Cancel)
							break;

						if (this._flex.Rows.Count > 2 && this._flex["DT_DUEDATE"].ToString() != "")
							str = this._flex["DT_DUEDATE"].ToString();

						this._flex.Redraw = false;
						string empty1 = string.Empty;
						string empty2 = string.Empty;

						foreach (DataRow row in e.Result.Rows)
						{
							if (e.Result.Rows[0] != row)
							{
								this.btn추가_Click(sender, e);
								this._flex[this._flex.Row, "CD_PARTNER"] = empty1;
								this._flex[this._flex.Row, "LN_PARTNER"] = empty2;
							}
							else
							{
								empty1 = D.GetString(this._flex[this._flex.Row, "CD_PARTNER"]);
								empty2 = D.GetString(this._flex[this._flex.Row, "LN_PARTNER"]);
							}

							this._flex[this._flex.Row, "CD_ITEM_PARTNER"] = row["CD_ITEM_PARTNER"];
							this._flex[this._flex.Row, "NM_ITEM_PARTNER"] = row["NM_ITEM_PARTNER"];
							this._flex[this._flex.Row, "CD_ITEM"] = row["CD_ITEM"];
							this._flex[this._flex.Row, "NM_ITEM"] = row["NM_ITEM"];
							this._flex[this._flex.Row, "STND_ITEM"] = row["STND_ITEM"];
							this._flex[this._flex.Row, "UNIT_SO"] = row["UNIT_SO"];
							this._flex[this._flex.Row, "UNIT_IM"] = row["UNIT_IM"];
							this._flex[this._flex.Row, "TP_ITEM"] = row["TP_ITEM"];
							this._flex[this._flex.Row, "CD_SL"] = row["CD_GISL"];
							this._flex[this._flex.Row, "NM_SL"] = row["NM_GISL"];
							this._flex[this._flex.Row, "LT_GI"] = D.GetDecimal(row["LT_GI"]);
							this._flex[this._flex.Row, "CD_CC"] = this.cd_CC;
							this._flex[this._flex.Row, "NM_CC"] = this.nm_CC;
							this._flex[this._flex.Row, "TP_VAT"] = this.cboVAT구분.SelectedValue.ToString();
							this._flex[this._flex.Row, "RT_VAT"] = D.GetDecimal(this._flex[e.Row, "RT_VAT"]);
							this._flex[this._flex.Row, "UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];

							if (str != "")
								this._flex[this._flex.Row, "DT_REQGI"] = this._CommFun.DateAdd(str, "D", Convert.ToInt32(this._flex[this._flex.Row, "LT_GI"]) * -1);

							if (this.tp_SalePrice == "002" || this.tp_SalePrice == "003")
							{
								objArray[1] = row["CD_ITEM"];

								if (this.disCount_YN == "N")
									this._flex[this._flex.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, this._biz.UmSearch(objArray));
								else if (this.disCount_YN == "Y")
								{
									this._flex[this._flex.Row, "UM_BASE"] = Unit.외화단가(DataDictionaryTypes.SA, this._biz.UmSearch(objArray));
									this._flex[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex[e.Row, "UM_BASE"]) - D.GetDecimal(this._flex[e.Row, "UM_BASE"]) * D.GetDecimal(this._flex[e.Row, "RT_DSCNT"]) / 100);
								}
							}

							this._flex["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["UM_SO"]) * D.GetDecimal(this._flex["QT_SO"]));
							this._flex["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_SO"]) * D.GetDecimal(this._flex["RT_EXCH"]));
							this._flex["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_WONAMT"]) * (D.GetDecimal(this._flex[e.Row, "RT_VAT"]) / 100));
							this._flex["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AM_WONAMT"]) + D.GetDecimal(this._flex["AM_VAT"]));

							if (D.GetDecimal(this._flex["QT_SO"]) != 0)
								this._flex["UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flex["AMVAT_SO"]) / D.GetDecimal(this._flex["QT_SO"]));
							else
								this._flex["UMVAT_SO"] = D.GetDecimal(this._flex["AMVAT_SO"]);
						}

						this._flex.Redraw = true;
						break;
					case "CD_PARTNER":
						dt = DBHelper.GetDataTable(@"SELECT 0 AS CODE,
														    '' AS NAME
													 UNION ALL
													 SELECT FP.SEQ AS CODE,
														    FP.NM_PTR AS NAME
													 FROM FI_PARTNERPTR FP WITH(NOLOCK)
													 WHERE FP.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
													"AND FP.CD_PARTNER = '" + e.Result.CodeValue + "'");

						this._flex.SetDataMap("NUM_USERDEF2", dt.Copy(), "CODE", "NAME"); // 구매담당자
						this._flex.SetDataMap("NUM_USERDEF3", dt.Copy(), "CODE", "NAME"); // 설계담당자
						break;
					case "NO_NEGO":
						dt = DBHelper.GetDataTable(@"SELECT 0 AS CODE,
														    '' AS NAME
													 UNION ALL
													 SELECT FP.SEQ AS CODE,
														    FP.NM_PTR AS NAME
													 FROM FI_PARTNERPTR FP WITH(NOLOCK)
													 WHERE FP.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
													"AND FP.CD_PARTNER = '" + e.Result.CodeValue + "'");

						this._flex.SetDataMap("NUM_USERDEF1", dt, "CODE", "NAME"); // 인수자
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region 기타메소드
		private void Authority(bool check)
		{
			this.ctx담당자.Enabled = check;
			this.ctx수주형태.Enabled = check;
			this.ctx영업그룹.Enabled = check;
			this.cboVAT구분.Enabled = check;
			this.cbo단가유형.Enabled = check;
		}

		private bool ErrorCheck()
		{
			Hashtable hashtable = new Hashtable();

			hashtable.Add(this.ctx영업그룹, this.lbl영업그룹);
			hashtable.Add(this.ctx담당자, this.lbl담당자);
			hashtable.Add(this.ctx수주형태, this.lbl수주형태);
			hashtable.Add(this.cbo단가유형, this.lbl단가유형);
			hashtable.Add(this.cboVAT구분, this.lblVAT구분);

			return ComFunc.NullCheck(hashtable);
		}

		private DataTable SO_PK_Check(DataTable dt_Excel, out bool b)
		{
			DataTable dataTable1 = dt_Excel.Clone();
			DataTable dataTable2 = null;
			string multi_item = string.Empty;
			object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode, Global.SystemLanguage.MultiLanguageLpoint };

			try
			{
				DataSet dataSet = this._biz.SO_PK_Check(objArray);
				DataTable dataTable3 = null;

				if (this.str동기화 == "N")
				{
					dataTable2 = this._biz.Get_dt프로젝트();
					dataTable2.PrimaryKey = new DataColumn[] { dataTable2.Columns["NO_PROJECT"] };
				}

				if (!dt_Excel.Columns.Contains("CD_BIZAREA"))
					dt_Excel.Columns.Add("CD_BIZAREA", typeof(string));

				if (dt_Excel.Columns.Contains("CD_ITEM_PARTNER"))
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("거래처" + "\t " + "거래처품목");
					stringBuilder.AppendLine("-".PadRight(30, '-'));
					DataTable table1 = dt_Excel.DefaultView.ToTable(true, "CD_PARTNER");
					bool flag = true;

					foreach (DataRow row in table1.Rows)
					{
						DataTable table2 = dt_Excel.Clone();
						DataView defaultView = dt_Excel.DefaultView;
						int num = 1;
						string[] strArray = new string[] { "CD_PARTNER", "CD_ITEM_PARTNER" };

						foreach (string pipe in D.StringConvert.GetPipes(Duzon.ERPU.MF.Common.Common.MultiString(defaultView.ToTable(num != 0, strArray).Select("CD_PARTNER = '" + D.GetString(row["CD_PARTNER"]) + "'"), "CD_ITEM_PARTNER", "|"), 160))
						{
							DataTable table3 = this._biz.SearchCpItem(D.GetString(row["CD_PARTNER"]), pipe);
							table2.Merge(table3);
						}

						if (dataTable3 == null)
							dataTable3 = table2;
						else
							dataTable3.Merge(table2);
					}

					dataTable3.PrimaryKey = new DataColumn[] { dataTable3.Columns["CD_PARTNER"], dataTable3.Columns["CD_ITEM_PARTNER"] };

					foreach (DataRow row in dt_Excel.Rows)
					{
						if (D.GetString(row["CD_ITEM_PARTNER"]) == string.Empty)
						{
							this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "거래처품번" });
							b = false;
							return dataTable1;
						}

						DataRow dataRow = dataTable3.Rows.Find(new object[] { D.GetString(row["CD_PARTNER"]), D.GetString(row["CD_ITEM_PARTNER"]) });

						if (dataRow == null)
						{
							stringBuilder.AppendLine(D.GetString(row["CD_PARTNER"]) + "\t" + D.GetString(row["CD_ITEM_PARTNER"]));
							flag = false;
						}
						else
						{
							row["CD_ITEM"] = D.GetString(dataRow["CD_ITEM"]);
							row["NM_ITEM_PARTNER"] = D.GetString(dataRow["NM_ITEM_PARTNER"]);
						}
					}

					if (!flag)
					{
						this.ShowDetailMessage("해당 거래처에 존재하지 않는 거래처 품목이 존재합니다.", D.GetString(stringBuilder));
						b = false;
						return dataTable1;
					}
				}
				else
					dt_Excel.Columns.Add("CD_ITEM_PARTNER", typeof(string));

				if (!dt_Excel.Columns.Contains("NO_RELATION"))
					dt_Excel.Columns.Add("NO_RELATION", typeof(string));

				if (!dt_Excel.Columns.Contains("SEQ_RELATION"))
					dt_Excel.Columns.Add("SEQ_RELATION", typeof(string));

				if (!dt_Excel.Columns.Contains("RT_EXCH"))
					dt_Excel.Columns.Add("RT_EXCH", typeof(decimal));

				foreach (DataRow row in dt_Excel.Rows)
					multi_item = multi_item + D.GetString(row["CD_ITEM"]) + "|";

				DataTable dataTable4 = this._biz.품목정보(multi_item);

				foreach (DataRow row in dt_Excel.Rows)
				{
					if (D.GetString(row["CD_ITEM"]) == string.Empty)
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "품목코드" });
						b = false;
						return dataTable1;
					}

					if (row["CD_PLANT"] == null || row["CD_PLANT"].ToString() == string.Empty)
						row["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;

					DataRow[] dataRowArray1 = dataTable4.Select("CD_PLANT = '" + row["CD_PLANT"].ToString() + "' AND CD_ITEM = '" + row["CD_ITEM"].ToString() + "'");

					if (dataRowArray1.Length == 0)
					{
						this.ShowMessage("품목(" + row["CD_ITEM"].ToString() + ")은 존재하지 않는 품목입니다.");
						b = false;
						return dataTable1;
					}

					if (row["CD_EXCH"].ToString() == "000")
						row["RT_EXCH"] = 1;
					else
					{
						if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
							row["RT_EXCH"] = MA.기준환율적용(row["DT_SO"].ToString(), row["CD_EXCH"].ToString());
					}

					row["NM_ITEM"] = dataRowArray1[0]["NM_ITEM"].ToString();
					row["STND_ITEM"] = dataRowArray1[0]["STND_ITEM"].ToString();
					row["UNIT_SO"] = dataRowArray1[0]["UNIT_SO"].ToString();
					row["UNIT_IM"] = dataRowArray1[0]["UNIT_IM"].ToString();
					row["UNIT_SO_FACT"] = (D.GetDecimal(dataRowArray1[0]["UNIT_SO_FACT"]) == 0) ? 1 : D.GetDecimal(dataRowArray1[0]["UNIT_SO_FACT"]);

					if (D.GetString(row["CD_PARTNER"]) == string.Empty)
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "거래처코드" });
						b = false;
						return dataTable1;
					}

					DataRow[] dataRowArray2 = dataSet.Tables[1].Select("CD_PARTNER = '" + D.GetString(row["CD_PARTNER"]) + "'");

					if (dataRowArray2.Length == 0)
					{
						this.ShowMessage("거래처(" + D.GetString(row["CD_PARTNER"]) + ")은 존재하지 않는 거래처입니다.");
						b = false;
						return dataTable1;
					}

					row["LN_PARTNER"] = D.GetString(dataRowArray2[0]["LN_PARTNER"]);

					if (row["GI_PARTNER"].ToString() != string.Empty)
					{
						DataRow[] dataRowArray3 = dataSet.Tables[1].Select("CD_PARTNER = '" + row["GI_PARTNER"].ToString() + "'");

						if (dataRowArray3.Length == 0)
						{
							this.ShowMessage("납품처(" + D.GetString(row["GI_PARTNER"]) + ")은 존재하지 않는 납품처입니다.");
							b = false;
							return dataTable1;
						}

						row["GN_PARTNER"] = D.GetString(dataRowArray3[0]["LN_PARTNER"]);
					}
					if (row["CD_SL"].ToString() != string.Empty)
					{
						DataRow[] dataRowArray3 = dataSet.Tables[2].Select("CD_SL = '" + row["CD_SL"].ToString() + "'");

						if (dataRowArray3.Length == 0 && row["CD_SL"].ToString() != string.Empty)
						{
							this.ShowMessage("창고(" + D.GetString(row["CD_SL"]) + ")은 존재하지 않는 창고입니다.");
							b = false;
							return dataTable1;
						}

						row["NM_SL"] = D.GetString(dataRowArray3[0]["NM_SL"]);
					}
					if (this.str동기화 == "N" && !(D.GetString(row["NO_PROJECT"]) == string.Empty))
					{
						DataRow dataRow = dataTable2.Rows.Find(row["NO_PROJECT"]);

						if (dataRow == null)
						{
							this.ShowMessage("프로젝트(" + D.GetString(row["NO_PROJECT"]) + ")은 존재하지 않는 프로젝트입니다.");
							b = false;
							return dataTable1;
						}

						row["NM_PROJECT"] = D.GetString(dataRow["NM_PROJECT"]);
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			b = true;
			return dt_Excel;
		}

		private DataTable GetCalculater(DataTable CheckTable, string SaveYN, string 엑셀구분)
		{
			string 수주형태, VAT구분;
			decimal 부가세율;

			try
			{
				foreach (DataRow dataRow in CheckTable.Select("", "", DataViewRowState.CurrentRows))
				{
					if (dataRow["CD_PLANT"] == null || dataRow["CD_PLANT"].ToString() == string.Empty)
						dataRow["CD_PLANT"] = Global.MainFrame.LoginInfo.CdPlant;

					if (CheckTable.Columns.Contains("PAYMENT"))
					{
						if (dataRow["CD_EXCH"].ToString() == "000")
							수주형태 = "1160";
						else
							수주형태 = "1100";

						switch (dataRow["PAYMENT"].ToString())
						{
							case "D":
							case "G":
								VAT구분 = "11";
								부가세율 = 10;
								break;
							case "X":
								VAT구분 = "14";
								부가세율 = 0;
								break;
							default:
								VAT구분 = this.cboVAT구분.SelectedValue.ToString();
								부가세율 = this.cur부가세율.DecimalValue;
								break;
						}
					}
					else
					{
						수주형태 = this.ctx수주형태.CodeValue;
						VAT구분 = this.cboVAT구분.SelectedValue.ToString();
						부가세율 = this.cur부가세율.DecimalValue;
					}

					DataRow tpso1 = BASIC.GetTPSO(수주형태);
					
					dataRow["TP_GI"] = D.GetString(tpso1["TP_GI"]);
					dataRow["TP_BUSI"] = D.GetString(tpso1["TP_BUSI"]);
					dataRow["TP_IV"] = D.GetString(tpso1["TP_IV"]);
					dataRow["GIR"] = D.GetString(tpso1["GIR"]);
					dataRow["GI"] = D.GetString(tpso1["GI"]);
					dataRow["IV"] = D.GetString(tpso1["IV"]);
					dataRow["TRADE"] = D.GetString(tpso1["TRADE"]);

					dataRow["CD_CC"] = this.cd_CC;
					dataRow["NM_CC"] = this.nm_CC;
					dataRow["TP_VAT"] = VAT구분;
					dataRow["RT_VAT"] = 부가세율;
					dataRow["SEQ_PROJECT"] = 0;

					if (SaveYN == "N")
					{
						if (엑셀구분 == "단가포함")
						{
							if (this.disCount_YN == "Y")
								dataRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["UM_BASE"]) - D.GetDecimal(dataRow["UM_BASE"]) * D.GetDecimal(dataRow["RT_DSCNT"]) / 100);

							dataRow["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_SO"]) * D.GetDecimal(dataRow["UM_SO"]));
						}
						else if (엑셀구분 == "금액포함")
							dataRow["UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_SO"]) > 0 ? decimal.Round(D.GetDecimal(dataRow["AM_SO"]) / D.GetDecimal(dataRow["QT_SO"]), 4, MidpointRounding.AwayFromZero) : 0);
						else if (엑셀구분 == "단가금액미포함")
						{
							dataRow["UM_SO"] = BASIC.GetUM(D.GetString(dataRow["CD_PLANT"]), D.GetString(dataRow["CD_ITEM"]), D.GetString(dataRow["CD_PARTNER"]), this.ctx영업그룹.CodeValue, dataRow["DT_SO"].ToString(), D.GetString(this.cbo단가유형.SelectedValue), dataRow["CD_EXCH"].ToString(), "SA");
							dataRow["AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_SO"]) * D.GetDecimal(dataRow["UM_SO"]));
						}

						dataRow["AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_SO"]) * D.GetDecimal(dataRow["RT_EXCH"]));
						dataRow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_WONAMT"]) * (D.GetDecimal(dataRow["RT_VAT"]) / 100));
					}

					dataRow["AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_WONAMT"]) + D.GetDecimal(dataRow["AM_VAT"]));
					dataRow["UMVAT_SO"] = !(D.GetDecimal(dataRow["QT_SO"]) != 0) ? D.GetDecimal(dataRow["AMVAT_SO"]) : Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AMVAT_SO"]) / D.GetDecimal(dataRow["QT_SO"]));
					int num2 = dataRow["UNIT_SO_FACT"] == null ? 0 : (!(dataRow["UNIT_SO_FACT"].ToString() == string.Empty) ? 1 : 0);
					dataRow["QT_IM"] = num2 != 0 ? Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_SO"]) * D.GetDecimal(dataRow["UNIT_SO_FACT"])) : D.GetDecimal(dataRow["QT_SO"]);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			return CheckTable;
		}

		private DataTable getSchema(string param)
		{
			DataTable dataTable = new DataTable();
			try
			{
				if (param == "SOH")
				{
					dataTable.Columns.Add("NO_SO", typeof(string));
					dataTable.Columns.Add("NO_HST", typeof(decimal));
					dataTable.Columns.Add("CD_BIZAREA", typeof(string));
					dataTable.Columns.Add("DT_SO", typeof(string));
					dataTable.Columns.Add("CD_PARTNER", typeof(string));
					dataTable.Columns.Add("CD_SALEGRP", typeof(string));
					dataTable.Columns.Add("NO_EMP", typeof(string));
					dataTable.Columns.Add("TP_SO", typeof(string));
					dataTable.Columns.Add("CD_EXCH", typeof(string));
					dataTable.Columns.Add("NM_EXCH", typeof(string));
					dataTable.Columns.Add("RT_EXCH", typeof(decimal));
					dataTable.Columns.Add("TP_PRICE", typeof(string));
					dataTable.Columns.Add("NO_PROJECT", typeof(string));
					dataTable.Columns.Add("TP_VAT", typeof(string));
					dataTable.Columns.Add("RT_VAT", typeof(decimal));
					dataTable.Columns.Add("FG_VAT", typeof(string));
					dataTable.Columns.Add("VATRATE", typeof(string));
					dataTable.Columns.Add("FG_TAXP", typeof(string));
					dataTable.Columns.Add("DC_RMK", typeof(string));
					dataTable.Columns.Add("DC_RMK1", typeof(string));
					dataTable.Columns.Add("FG_BILL", typeof(string));
					dataTable.Columns.Add("FG_TRANSPORT", typeof(string));
					dataTable.Columns.Add("NO_CONTRACT", typeof(string));
					dataTable.Columns.Add("STA_SO", typeof(string));
					dataTable.Columns.Add("FG_TRACK", typeof(string));
					dataTable.Columns.Add("NO_PO_PARTNER", typeof(string));
					dataTable.Columns.Add("RMA_REASON", typeof(string));

					dataTable.Columns.Add("TXT_USERDEF1", typeof(string));
					dataTable.Columns.Add("NUM_USERDEF2", typeof(decimal));
					dataTable.Columns.Add("NUM_USERDEF3", typeof(decimal));
					dataTable.Columns.Add("NUM_USERDEF1", typeof(decimal));
					dataTable.Columns.Add("NO_NEGO", typeof(string));

					if (this._biz.Get과세변경유무 == "N" && this._매출자동여부 == "Y")
					{
						dataTable.Columns.Add("DT_PROCESS", typeof(string));
						dataTable.Columns.Add("DT_RCP_RSV", typeof(string));
						dataTable.Columns.Add("FG_AR_EXC", typeof(string));
						dataTable.Columns.Add("AM_IV", typeof(decimal));
						dataTable.Columns.Add("AM_IV_EX", typeof(decimal));
						dataTable.Columns.Add("AM_IV_VAT", typeof(decimal));
						dataTable.Columns.Add("NM_PTR", typeof(string));
						dataTable.Columns.Add("EX_EMIL", typeof(string));
						dataTable.Columns.Add("EX_HP", typeof(string));
					}
				}
				else if (param == "SOL")
				{
					dataTable.Columns.Add("S", typeof(string));
					dataTable.Columns["S"].DefaultValue = "N";
					dataTable.Columns.Add("NO_SO", typeof(string));
					dataTable.Columns.Add("NO_HST", typeof(decimal));
					dataTable.Columns.Add("SEQ_SO", typeof(decimal));
					dataTable.Columns.Add("CD_PLANT", typeof(string));
					dataTable.Columns.Add("CD_ITEM", typeof(string));
					dataTable.Columns.Add("UNIT_SO", typeof(string));
					dataTable.Columns.Add("DT_EXPECT", typeof(string));
					dataTable.Columns.Add("DT_DUEDATE", typeof(string));
					dataTable.Columns.Add("DT_REQGI", typeof(string));
					dataTable.Columns.Add("QT_SO", typeof(decimal));
					dataTable.Columns.Add("UM_SO", typeof(decimal));
					dataTable.Columns.Add("AM_SO", typeof(decimal));
					dataTable.Columns.Add("AM_WONAMT", typeof(decimal));
					dataTable.Columns.Add("AM_VAT", typeof(decimal));
					dataTable.Columns.Add("NO_PROJECT", typeof(string));
					dataTable.Columns.Add("SEQ_PROJECT", typeof(decimal));
					dataTable.Columns.Add("UNIT_IM", typeof(string));
					dataTable.Columns.Add("QT_IM", typeof(decimal));
					dataTable.Columns.Add("CD_SL", typeof(string));
					dataTable.Columns.Add("TP_ITEM", typeof(string));
					dataTable.Columns.Add("STA_SO", typeof(string));
					dataTable.Columns.Add("TP_BUSI", typeof(string));
					dataTable.Columns.Add("TP_GI", typeof(string));
					dataTable.Columns.Add("TP_IV", typeof(string));
					dataTable.Columns.Add("GIR", typeof(string));
					dataTable.Columns.Add("GI", typeof(string));
					dataTable.Columns.Add("IV", typeof(string));
					dataTable.Columns.Add("TRADE", typeof(string));
					dataTable.Columns.Add("TP_VAT", typeof(string));
					dataTable.Columns.Add("RT_VAT", typeof(decimal));
					dataTable.Columns.Add("GI_PARTNER", typeof(string));
					dataTable.Columns.Add("CD_ITEM_PARTNER", typeof(string));
					dataTable.Columns.Add("NM_ITEM_PARTNER", typeof(string));
					dataTable.Columns.Add("DC1", typeof(string));
					dataTable.Columns.Add("DC2", typeof(string));
					dataTable.Columns.Add("UMVAT_SO", typeof(decimal));
					dataTable.Columns.Add("AMVAT_SO", typeof(decimal));
					dataTable.Columns.Add("CD_SHOP", typeof(string));
					dataTable.Columns.Add("CD_SPITEM", typeof(string));
					dataTable.Columns.Add("CD_OPT", typeof(string));
					dataTable.Columns.Add("RT_DSCNT", typeof(decimal));
					dataTable.Columns.Add("UM_BASE", typeof(decimal));
					dataTable.Columns.Add("FG_USE", typeof(string));
					dataTable.Columns.Add("CD_CC", typeof(string));
					dataTable.Columns.Add("NM_CUST_DLV", typeof(string));
					dataTable.Columns.Add("CD_ZIP", typeof(string));
					dataTable.Columns.Add("ADDR1", typeof(string));
					dataTable.Columns.Add("ADDR2", typeof(string));
					dataTable.Columns.Add("NO_TEL_D1", typeof(string));
					dataTable.Columns.Add("NO_TEL_D2", typeof(string));
					dataTable.Columns.Add("TP_DLV", typeof(string));
					dataTable.Columns.Add("TP_DLV_DUE", typeof(string));
					dataTable.Columns.Add("DC_REQ", typeof(string));
					dataTable.Columns.Add("FG_TRACK", typeof(string));
					dataTable.Columns.Add("NO_ORDER", typeof(string));
					dataTable.Columns.Add("NM_CUST", typeof(string));
					dataTable.Columns.Add("NO_TEL1", typeof(string));
					dataTable.Columns.Add("NO_TEL2", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF1", typeof(string));
					dataTable.Columns.Add("NO_PO_PARTNER", typeof(string));
					dataTable.Columns.Add("NO_POLINE_PARTNER", typeof(decimal));
					dataTable.Columns.Add("NO_RELATION", typeof(string));
					dataTable.Columns.Add("SEQ_RELATION", typeof(decimal));
					dataTable.Columns.Add("GRP_ITEM", typeof(string));
					dataTable.Columns.Add("CLS_L", typeof(string));
					dataTable.Columns.Add("CLS_S", typeof(string));
					dataTable.Columns.Add("NUM_STND_ITEM_1", typeof(decimal));
					dataTable.Columns.Add("NUM_STND_ITEM_2", typeof(decimal));
					dataTable.Columns.Add("NUM_STND_ITEM_3", typeof(decimal));
					dataTable.Columns.Add("NUM_STND_ITEM_4", typeof(decimal));
					dataTable.Columns.Add("NUM_STND_ITEM_5", typeof(decimal));
					dataTable.Columns.Add("NUM_USERDEF1", typeof(decimal));
					dataTable.Columns.Add("NUM_USERDEF2", typeof(decimal));
					dataTable.Columns.Add("CD_MNGD1", typeof(string));
					dataTable.Columns.Add("CD_MNGD2", typeof(string));
					dataTable.Columns.Add("CD_MNGD3", typeof(string));
					dataTable.Columns.Add("CD_MNGD4", typeof(string));
					dataTable.Columns.Add("SOL_TXT_USERDEF1", typeof(string));
					dataTable.Columns.Add("SOL_TXT_USERDEF2", typeof(string));

					dataTable.Columns.Add("TXT_USERDEF3", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF4", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF5", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF6", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF7", typeof(string));

					dataTable.Columns.Add("YN_OPTION", typeof(string));
					dataTable.Columns["YN_OPTION"].DefaultValue = "N";
					dataTable.Columns.Add("CD_USERDEF1", typeof(string));
					dataTable.Columns.Add("CD_USERDEF2", typeof(string));
					dataTable.Columns.Add("CD_USERDEF3", typeof(string));
				}
				else if (param == "DEFAULT_SCHEMA")
				{
					dataTable.Columns.Add("S", typeof(string));
					dataTable.Columns["S"].DefaultValue = "N";
					dataTable.Columns.Add("NO_SO", typeof(string));
					dataTable.Columns.Add("NO_HST", typeof(decimal));
					dataTable.Columns.Add("CD_BIZAREA", typeof(string));
					dataTable.Columns.Add("DT_SO", typeof(string));
					dataTable.Columns.Add("CD_PARTNER", typeof(string));
					dataTable.Columns.Add("CD_SALEGRP", typeof(string));
					dataTable.Columns.Add("NO_EMP", typeof(string));
					dataTable.Columns.Add("TP_SO", typeof(string));
					dataTable.Columns.Add("CD_EXCH", typeof(string));
					dataTable.Columns.Add("NM_EXCH", typeof(string));
					dataTable.Columns.Add("RT_EXCH", typeof(decimal));
					dataTable.Columns.Add("TP_PRICE", typeof(string));
					dataTable.Columns.Add("NO_PROJECT", typeof(string));
					dataTable.Columns.Add("SEQ_PROJECT", typeof(decimal));
					dataTable.Columns.Add("TP_VAT", typeof(string));
					dataTable.Columns.Add("RT_VAT", typeof(decimal));
					dataTable.Columns.Add("FG_VAT", typeof(string));
					dataTable.Columns.Add("VATRATE", typeof(string));
					dataTable.Columns.Add("FG_TAXP", typeof(string));
					dataTable.Columns.Add("DC_RMK", typeof(string));
					dataTable.Columns.Add("DC_RMK1", typeof(string));
					dataTable.Columns.Add("FG_BILL", typeof(string));
					dataTable.Columns.Add("FG_TRANSPORT", typeof(string));
					dataTable.Columns.Add("NO_CONTRACT", typeof(string));
					dataTable.Columns.Add("STA_SO", typeof(string));
					dataTable.Columns.Add("FG_TRACK", typeof(string));
					dataTable.Columns.Add("SEQ_SO", typeof(decimal));
					dataTable.Columns.Add("CD_PLANT", typeof(string));
					dataTable.Columns.Add("CD_ITEM", typeof(string));
					dataTable.Columns.Add("UNIT_SO", typeof(string));
					dataTable.Columns.Add("DT_EXPECT", typeof(string));
					dataTable.Columns.Add("DT_DUEDATE", typeof(string));
					dataTable.Columns.Add("DT_REQGI", typeof(string));
					dataTable.Columns.Add("QT_SO", typeof(decimal));
					dataTable.Columns.Add("UM_SO", typeof(decimal));
					dataTable.Columns.Add("AM_SO", typeof(decimal));
					dataTable.Columns.Add("AM_WONAMT", typeof(decimal));
					dataTable.Columns.Add("AM_VAT", typeof(decimal));
					dataTable.Columns.Add("UNIT_IM", typeof(string));
					dataTable.Columns.Add("QT_IM", typeof(decimal));
					dataTable.Columns.Add("CD_SL", typeof(string));
					dataTable.Columns.Add("TP_ITEM", typeof(string));
					dataTable.Columns.Add("TP_BUSI", typeof(string));
					dataTable.Columns.Add("TP_GI", typeof(string));
					dataTable.Columns.Add("TP_IV", typeof(string));
					dataTable.Columns.Add("GIR", typeof(string));
					dataTable.Columns.Add("GI", typeof(string));
					dataTable.Columns.Add("IV", typeof(string));
					dataTable.Columns.Add("TRADE", typeof(string));
					dataTable.Columns.Add("GI_PARTNER", typeof(string));
					dataTable.Columns.Add("CD_ITEM_PARTNER", typeof(string));
					dataTable.Columns.Add("NM_ITEM_PARTNER", typeof(string));
					dataTable.Columns.Add("DC1", typeof(string));
					dataTable.Columns.Add("DC2", typeof(string));
					dataTable.Columns.Add("UMVAT_SO", typeof(decimal));
					dataTable.Columns.Add("AMVAT_SO", typeof(decimal));
					dataTable.Columns.Add("LN_PARTNER", typeof(string));
					dataTable.Columns.Add("NM_ITEM", typeof(string));
					dataTable.Columns.Add("STND_ITEM", typeof(string));
					dataTable.Columns.Add("NM_SL", typeof(string));
					dataTable.Columns.Add("UNIT_SO_FACT", typeof(decimal));
					dataTable.Columns.Add("LT_GI", typeof(decimal));
					dataTable.Columns.Add("GN_PARTNER", typeof(string));
					dataTable.Columns.Add("NO_PO_PARTNER", typeof(string));
					dataTable.Columns.Add("CD_SHOP", typeof(string));
					dataTable.Columns.Add("CD_SPITEM", typeof(string));
					dataTable.Columns.Add("CD_OPT", typeof(string));
					dataTable.Columns.Add("RT_DSCNT", typeof(decimal));
					dataTable.Columns.Add("UM_BASE", typeof(decimal));
					dataTable.Columns.Add("FG_USE", typeof(string));
					dataTable.Columns.Add("CD_CC", typeof(string));
					dataTable.Columns.Add("NM_CC", typeof(string));
					dataTable.Columns.Add("NM_CUST_DLV", typeof(string));
					dataTable.Columns.Add("CD_ZIP", typeof(string));
					dataTable.Columns.Add("ADDR1", typeof(string));
					dataTable.Columns.Add("ADDR2", typeof(string));
					dataTable.Columns.Add("NO_TEL_D1", typeof(string));
					dataTable.Columns.Add("NO_TEL_D2", typeof(string));
					dataTable.Columns.Add("TP_DLV", typeof(string));
					dataTable.Columns.Add("TP_DLV_DUE", typeof(string));
					dataTable.Columns.Add("DC_REQ", typeof(string));
					dataTable.Columns.Add("NM_PROJECT", typeof(string));
					dataTable.Columns.Add("NO_POLINE_PARTNER", typeof(decimal));
					dataTable.Columns.Add("NO_RELATION", typeof(string));
					dataTable.Columns.Add("SEQ_RELATION", typeof(decimal));
					dataTable.Columns.Add("GRP_ITEM", typeof(string));
					dataTable.Columns.Add("CLS_L", typeof(string));
					dataTable.Columns.Add("CLS_S", typeof(string));
					dataTable.Columns.Add("NUM_STND_ITEM_1", typeof(decimal));
					dataTable.Columns.Add("NUM_STND_ITEM_2", typeof(decimal));
					dataTable.Columns.Add("NUM_STND_ITEM_3", typeof(decimal));
					dataTable.Columns.Add("NUM_STND_ITEM_4", typeof(decimal));
					dataTable.Columns.Add("NUM_STND_ITEM_5", typeof(decimal));
					dataTable.Columns.Add("NUM_USERDEF1", typeof(decimal));
					dataTable.Columns.Add("NUM_USERDEF2", typeof(decimal));
					dataTable.Columns.Add("CD_MNGD1", typeof(string));
					dataTable.Columns.Add("CD_MNGD2", typeof(string));
					dataTable.Columns.Add("CD_MNGD3", typeof(string));
					dataTable.Columns.Add("CD_MNGD4", typeof(string));
					dataTable.Columns.Add("SOL_TXT_USERDEF1", typeof(string));
					dataTable.Columns.Add("SOL_TXT_USERDEF2", typeof(string));

					dataTable.Columns.Add("TXT_USERDEF1", typeof(string));
					dataTable.Columns.Add("NUM_USERDEF3", typeof(decimal));
					dataTable.Columns.Add("NO_NEGO", typeof(string));
					dataTable.Columns.Add("NM_NEGO", typeof(string));

					dataTable.Columns.Add("TXT_USERDEF3", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF4", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF5", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF6", typeof(string));
					dataTable.Columns.Add("TXT_USERDEF7", typeof(string));

					dataTable.Columns.Add("YN_OPTION", typeof(string));
					dataTable.Columns["YN_OPTION"].DefaultValue = "N";
					dataTable.Columns.Add("CD_USERDEF1", typeof(string));
					dataTable.Columns.Add("CD_USERDEF2", typeof(string));
					dataTable.Columns.Add("CD_USERDEF3", typeof(string));
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			return dataTable;
		}

		private DataTable getExcelSchema(DataTable dt)
		{
			try
			{
				dt.Columns.Add("S", typeof(string));
				dt.Columns["S"].DefaultValue = "N";
				dt.Columns.Add("SEQ_SO", typeof(decimal));
				dt.Columns.Add("LN_PARTNER", typeof(string));
				dt.Columns.Add("NM_NEGO", typeof(string));
				dt.Columns.Add("NM_ITEM", typeof(string));
				dt.Columns.Add("STND_ITEM", typeof(string));
				dt.Columns.Add("UNIT_SO", typeof(string));
				dt.Columns.Add("AM_SO", typeof(decimal));
				dt.Columns.Add("AM_WONAMT", typeof(decimal));
				dt.Columns.Add("AM_VAT", typeof(decimal));
				dt.Columns.Add("AMVAT_SO", typeof(decimal));
				dt.Columns.Add("UNIT_IM", typeof(string));
				dt.Columns.Add("QT_IM", typeof(decimal));
				dt.Columns.Add("NM_SL", typeof(string));
				dt.Columns.Add("TP_ITEM", typeof(string));
				dt.Columns.Add("UNIT_SO_FACT", typeof(string));
				dt.Columns.Add("LT_GI", typeof(string));
				dt.Columns.Add("GN_PARTNER", typeof(string));

				dt.Columns.Add("TP_BUSI", typeof(string));
				dt.Columns.Add("TP_GI", typeof(string));
				dt.Columns.Add("TP_IV", typeof(string));
				dt.Columns.Add("GIR", typeof(string));
				dt.Columns.Add("GI", typeof(string));
				dt.Columns.Add("IV", typeof(string));
				dt.Columns.Add("TRADE", typeof(string));
				dt.Columns.Add("UMVAT_SO", typeof(decimal));
				dt.Columns.Add("SEQ_PROJECT", typeof(decimal));
				dt.Columns.Add("RT_DSCNT", typeof(decimal));
				dt.Columns.Add("UM_BASE", typeof(decimal));
				dt.Columns.Add("TP_VAT", typeof(string));
				dt.Columns.Add("RT_VAT", typeof(decimal));

				//if (Sa_Global.Sol_TpVat_ModifyYN == "Y")
				//{
				//	dt.Columns.Add("TP_VAT", typeof(string));
				//	dt.Columns.Add("RT_VAT", typeof(string));
				//}

				dt.Columns.Add("CD_CC", typeof(string));
				dt.Columns.Add("NM_CC", typeof(string));

				if (this.disCount_YN == "Y")
				{
					dt.Columns.Add("RT_DSCNT", typeof(string));
					dt.Columns.Add("UM_BASE", typeof(string));
				}

				dt.Columns.Add("NM_PROJECT", typeof(string));

				dt.Columns.Add("NM_ITEM_PARTNER", typeof(string));
				dt.Columns.Add("NO_POLINE_PARTNER", typeof(string));
				dt.Columns.Add("FG_USE", typeof(string));
				dt.Columns.Add("SOL_TXT_USERDEF1", typeof(string));
				dt.Columns.Add("SOL_TXT_USERDEF2", typeof(string));
				dt.Columns.Add("STA_SO", typeof(string));
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			return dt;
		}

		private void 수주통보(string 유형, DataTable dt)
		{
			string 본문, html;

			try
			{
				switch(유형)
				{
					case "001":
						본문 = "신규 수주건이 있으니 확인 바랍니다.";
						break;
					case "002":
						본문 = "단가 변경건이 있으니 확인 바랍니다.";
						break;
					case "003":
						본문 = "수주 변경건이 있으니 확인 바랍니다.";
						break;
					case "004":
						본문 = "품목 변경건이 있으니 확인 바랍니다.";
						break;
					default:
						return;
				}

				switch (유형)
				{
					case "001":
						html = @"<br/><div style='text-align:left; font-weight: bold;'>*** 신규 수주 통보서</div>";
						break;
					case "002":
						html = @"<br/><div style='text-align:left; font-weight: bold;'>*** 단가 변경 통보서</div>";
						break;
					case "003":
						html = @"<br/><div style='text-align:left; font-weight: bold;'>*** 수주 변경 통보서</div>";
						break;
					case "004":
						html = @"<br/><div style='text-align:left; font-weight: bold;'>*** 품목 변경 통보서</div>";
						break;
					default:
						return;
				}

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

				switch (유형)
				{
					case "001":
						html += "<th style='border:solid 1px black; text-align:center; background-color:Silver'>비고</th>";
						break;
					case "002":
						html += "<th style='border:solid 1px black; text-align:center; background-color:Silver'>변경비고</th>";
						break;
					case "003":
						html += "<th style='border:solid 1px black; text-align:center; background-color:Silver'>변경비고</th>";
						break;
					case "004":
						html += "<th style='border:solid 1px black; text-align:center; background-color:Silver'>변경비고</th>";
						break;
					default:
						return;
				}

				html += "</tr>";

				DataTable dt1 = new DataTable();

				dt1.Columns.Add("NO_SO");
				dt1.Columns.Add("SEQ_SO");
				dt1.Columns.Add("TP_MAIL");
				dt1.Columns.Add("DC1");
				dt1.Columns.Add("DC2");

				foreach (DataRow dr in dt.Rows)
				{
					html += @"<tr style='height:30px'>
							   <th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_SO"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_PO_PARTNER"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:center; padding-left:10px; font-weight:normal'>" + Util.GetTo_DateStringS(dr["DT_SO"]) + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:center; padding-left:10px; font-weight:normal'>" + dr["NM_KOR"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["LN_PARTNER"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["CD_USERDEF1"].ToString() + " </th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["CD_USERDEF2"].ToString() + " </th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["TXT_USERDEF6"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["NM_ENGINE"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:center; padding-left:10px; font-weight:normal'>" + dr["SEQ_SO"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["NM_ITEM"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_DESIGN"].ToString() + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:right; padding-right:10px; font-weight:normal'>" + D.GetInt(dr["QT_SO"]) + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:center; font-weight:normal'>" + Util.GetTo_DateStringS(dr["DT_DUEDATE"]) + "</th>" + Environment.NewLine +
							  "<th style='border:solid 1px black; text-align:right; padding-right:10px; font-weight:normal'>" + D.GetInt(dr["NUM_USERDEF3"]) + " </th>" + Environment.NewLine;
					
					switch (유형)
					{
						case "001":
							html += "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["DC1"].ToString() + " </th>" + Environment.NewLine;
							break;
						case "002":
							html += "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["DC2"].ToString() + " </th>" + Environment.NewLine;
							break;
						case "003":
							html += "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["DC2"].ToString() + " </th>" + Environment.NewLine;
							break;
						case "004":
							html += "<th style='border:solid 1px black; text-align:left; padding-left:10px; font-weight:normal'>" + dr["DC2"].ToString() + " </th>" + Environment.NewLine;
							break;
						default:
							return;
					}

					html += "</tr>";

					DataRow dr2 = dt1.NewRow();

					dr2["NO_SO"] = dr["NO_SO"].ToString();
					dr2["SEQ_SO"] = dr["SEQ_SO"].ToString();
					dr2["TP_MAIL"] = (유형 == "001" ? "001" : "002");
					dr2["DC1"] = dr["DC1"].ToString();
					dr2["DC2"] = dr["DC2"].ToString();

					dt1.Rows.Add(dr2);
				}
				html += @"</tbody>
							</table>";

				switch (유형)
				{
					case "001":
						this.메일발송("executive@win-tec.co.kr", "신규 수주 알림", 본문, html);
						DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_MNG_MAIL_SEND_LOG_JSON", new object[] { dt1.Json(), Global.MainFrame.LoginInfo.UserID });
						break;
					case "002":
						this.메일발송("sales@win-tec.co.kr", "단가 변경 알림", 본문, html);
						DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_MNG_MAIL_SEND_LOG_JSON", new object[] { dt1.Json(), Global.MainFrame.LoginInfo.UserID });
						break;
					case "003":
						this.메일발송("notice@win-tec.co.kr", "수주 변경 알림", 본문, html);
						DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_MNG_MAIL_SEND_LOG_JSON", new object[] { dt1.Json(), Global.MainFrame.LoginInfo.UserID });
						break;
					case "004":
						this.메일발송("notice@win-tec.co.kr", "품목 변경 알림", 본문, html);
						DBHelper.ExecuteNonQuery("SP_CZ_SA_SO_MNG_MAIL_SEND_LOG_JSON", new object[] { dt1.Json(), Global.MainFrame.LoginInfo.UserID });
						break;
					default:
						return;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void 메일발송(string 받는사람, string 제목, string 본문, string html)
		{
			try
			{
				#region 기본설정
				MailMessage mailMessage = new MailMessage();
				mailMessage.SubjectEncoding = Encoding.UTF8;
				mailMessage.BodyEncoding = Encoding.UTF8;
				mailMessage.IsBodyHtml = true;
				#endregion

				#region 메일정보
				mailMessage.From = new MailAddress("wintec@dintec.co.kr", "관리자", Encoding.UTF8);

				foreach (string 메일주소 in 받는사람.Split(';'))
					mailMessage.To.Add(new MailAddress(메일주소));

				mailMessage.Subject = 제목;

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
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion
	}
}
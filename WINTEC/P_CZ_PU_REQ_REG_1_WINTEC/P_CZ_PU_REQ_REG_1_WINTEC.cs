using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF.EMail;
using Duzon.Windows.Print;
using DzHelpFormLib;
using master;
using pur;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using trade;

namespace cz
{
	public partial class P_CZ_PU_REQ_REG_1_WINTEC : PageBase
	{
		private P_CZ_PU_REQ_REG_1_WINTEC_BIZ _biz = new P_CZ_PU_REQ_REG_1_WINTEC_BIZ();
		private FreeBinding _header = new FreeBinding();
		private bool _isChagePossible;
		public string MNG_LOT = string.Empty;
		public string MNG_SERIAL = string.Empty;
		private string m_sEnv = "N";
		private string m_sEnv_IQC = "N";
		private string m_sPJT재고사용 = "000";
		private bool m_bPJT사용 = false;
		private string m_YN_special = "N";
		private string strNO_RCV;
		private decimal NO_RCV_LINE;
		private string strSOURCE;
		private string m_Elec_app = "000";
		private string m_Mail = "N";
		private string m_YN_SU = "000";
		private string FG_IO_SU;
		private string CD_QTIOTP_SU;
		private bool b단가권한 = true;
		private bool b금액권한 = true;
		private string s_vat_fictitious = BASIC.GetMAEXC("발주등록(공장)-의제부가세적용");
		private bool bStandard = false;

		public P_CZ_PU_REQ_REG_1_WINTEC()
	  : this("", 0M, "")
		{
		}

		public P_CZ_PU_REQ_REG_1_WINTEC(string strNO_RCV, decimal NO_RCV_LINE, string strSOURCE)
		{
			try
			{
				this.InitializeComponent();
				this.strNO_RCV = strNO_RCV;
				this.NO_RCV_LINE = NO_RCV_LINE;
				this.strSOURCE = strSOURCE;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
		{
			object[] args = e.Args;
			this.strNO_RCV = D.GetString(args[0]);
			this.NO_RCV_LINE = D.GetDecimal(args[1]);
			this.strSOURCE = D.GetString(args[2]);
			this.InitPaint();
		}

		private void ControlEnabledDisable(bool enable)
		{
			if (MA.ServerKey(false, "WJIS", "THV") && this.추가모드여부)
				this.tb_DT_RCV.Enabled = true;
			else
				this.tb_DT_RCV.Enabled = enable;
			this.cbo공장.Enabled = enable;
			this.cbo_TRANS.Enabled = enable;
			this.tb_NM_PARTNER.Enabled = enable;
			this.tb_NO_EMP.Enabled = enable;
			this.cb_Yn_AutoRcv.Enabled = enable;
			this.cbo_PROCESS.Enabled = false;
			this.cbo_TRANS.Enabled = enable;
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			DataTable dataTable = BASIC.MFG_AUTH("P_PU_REQ_REG_1");
			if (dataTable.Rows.Count > 0)
			{
				this.b단가권한 = !(dataTable.Rows[0]["YN_UM"].ToString() == "Y");
				this.b금액권한 = !(dataTable.Rows[0]["YN_AM"].ToString() == "Y");
			}
			this.MA_EXC_SETTING();

			this.InitGrid();
			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();
			this.m_sEnv_IQC = ComFunc.전용코드("수입검사-사용구분");
			if (this.m_sEnv_IQC == "Y")
				this.btn수입검사적용.Visible = true;
			this.m_sPJT재고사용 = ComFunc.전용코드("프로젝트재고사용");
			this.m_bPJT사용 = App.SystemEnv.PROJECT사용;
			this.m_YN_special = ComFunc.전용코드("특채수량 사용여부");
			this.Initial_Binding();
			this.InitControl();
			this.MNG_LOT = this._biz.Search_LOT().Rows[0]["MNG_LOT"].ToString();
			this.MNG_SERIAL = this._biz.Search_SERIAL().Rows[0]["YN_SERIAL"].ToString();
			this.btnLocalLC.Enabled = false;
			this.btn통관적용.Enabled = false;
			this._isChagePossible = true;
			this.tb_NO_EMP.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
			this.tb_NO_EMP.CodeName = Global.MainFrame.LoginInfo.EmployeeName;
			this.tb_DT_RCV.Text = Global.MainFrame.GetStringToday;
			if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA" || this.MainFrameInterface.ServerKey == "DZSQL")
				this.btn점포검수적용.Visible = true;
			if (this.m_YN_SU == "100")
				this._flexD.Binding = this._biz.Search_MATL("ASDFASDF");
			if (D.GetString(this.strNO_RCV) != string.Empty)
			{
				DataSet dataSet = !(this.strSOURCE == "화면이동") ? this._biz.Search(this.strNO_RCV, "Y") : this._biz.Search(this.strNO_RCV, "N");
				this._header.SetDataTable(dataSet.Tables[0]);
				if (this.strSOURCE != "화면이동")
					this.txt_NOIO.Text = string.Empty;
				if (dataSet != null && dataSet.Tables.Count > 1)
				{
					this.Button_Enabled(dataSet.Tables[0], dataSet.Tables[1]);
					this._flex.Binding = dataSet.Tables[1];
					if (!this._flex.HasNormalRow && !this._header.CurrentRow.IsNull(0))
					{
						this.ShowMessage(PageResultMode.SearchNoData);
					}
					this._header.AcceptChanges();
					this._flex.AcceptChanges();
					this.ControlEnabledDisable(false);
				}
			}
			this.원그리드적용하기();
		}

		private void MA_EXC_SETTING()
		{
			try
			{
				this.m_Elec_app = BASIC.GetMAEXC("구매입고처리-업체별프로세스선택");
				if (this.m_Elec_app == "100")
				{
					this.bpPanelControl6.Visible = true;
					this.lbl입고번호.Visible = false;
				}
				else
				{
					this.bpPanelControl6.Visible = false;
					this.lbl입고번호.Visible = true;
				}
				this.m_Mail = BASIC.GetMAEXC("구매입고처리-메일전송설정");
				if (this.m_Mail == "Y")
					this.btn메일전송.Visible = true;
				this.m_sEnv = this._biz.EnvSearch();
				if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_REQ_REG_1")
					this.m_sEnv = "Y";
				this.m_YN_SU = BASIC.GetMAEXC("구매발주-외주유무");
				if (this.m_YN_SU == "100")
					this.InitGridD();
				else
					this.tab.TabPages.RemoveAt(1);
				if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
					this.bStandard = true;
				this.bpPanelControl9.Visible = this.bpPanelControl16.Visible = false;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void InitControl()
		{
			DataSet comboData = this.GetComboData("N;TR_IM00006", "NC;MA_PLANT", "N;PU_C000025", "N;PU_C000016", "N;PU_C000005", "N;MA_B000010", "N;MA_B000046", "N;MA_B000141", "N;MA_B000005");
			this.cbo_PROCESS.DataSource = comboData.Tables[0].DefaultView;
			this.cbo_PROCESS.DisplayMember = "NAME";
			this.cbo_PROCESS.ValueMember = "CODE";
			this.cbo공장.DataSource = comboData.Tables[1].DefaultView;
			this.cbo공장.DisplayMember = "NAME";
			this.cbo공장.ValueMember = "CODE";
			if (this.cbo공장.Items.Count > 0)
			{
				if (this.LoginInfo.CdPlant != "")
					this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
				this.cbo공장.SelectedIndex = 0;
				this._header.CurrentRow["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
			}
			this.cbo_TRANS.DataSource = comboData.Tables[3].DefaultView;
			this.cbo_TRANS.DisplayMember = "NAME";
			this.cbo_TRANS.ValueMember = "CODE";
			this.cb_Yn_AutoRcv.DataSource = comboData.Tables[2].DefaultView;
			this.cb_Yn_AutoRcv.DisplayMember = "NAME";
			this.cb_Yn_AutoRcv.ValueMember = "CODE";
			this.cbo_NM_EXCH.DataSource = comboData.Tables[8].DefaultView;
			this.cbo_NM_EXCH.DisplayMember = "NAME";
			this.cbo_NM_EXCH.ValueMember = "CODE";
			this.cbo_NM_EXCH.SelectedValue = "000";
			this._flex.SetDataMap("TP_UM_TAX", comboData.Tables[4], "CODE", "NAME");
			this._flex.SetDataMap("CLS_ITEM", comboData.Tables[5], "CODE", "NAME");
			this._flex.SetDataMap("FG_TAX", comboData.Tables[6], "CODE", "NAME");
			this._flex.SetDataMap("CD_EXCH", comboData.Tables[8], "CODE", "NAME");
			this.cbo_PROCESS.Enabled = false;
			this.txt_NOIO.Text = string.Empty;
			if (Global.MainFrame.ServerKeyCommon == "CARGOTEC" || Global.MainFrame.ServerKeyCommon == "ANJUN" || Global.MainFrame.ServerKeyCommon == "TELCON")
			{
				this.chk바코드사용유무.Visible = true;
				if (Settings1.Default.chk_barcode_use == "Y")
					this.chk바코드사용유무.Checked = true;
				else
					this.chk바코드사용유무.Checked = false;
				this.bpPanelControl10.Visible = true;
				this.bpPanelControl15.Visible = true;
			}
			else
			{
				if (Global.MainFrame.LoginInfo.CdPlant == Settings1.Default.cd_plant && !string.IsNullOrEmpty(Settings1.Default.cd_sl_apply))
				{
					this.bp_CD_SL.CodeValue = Settings1.Default.cd_sl_apply;
					this.bp_CD_SL.CodeName = Settings1.Default.nm_sl_apply;
				}
				this.chk바코드사용유무.Visible = false;
				this.chk바코드사용유무.Checked = false;
				this.bpPanelControl10.Visible = false;
				this.bpPanelControl15.Visible = false;
				this.txt바코드.Focus();
			}
			if (!(Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_REQ_REG_1"))
				return;
			DataTable dataTable = comboData.Tables[7].Copy();
			DataRow[] dataRowArray1 = dataTable.Select("CODE = '001'");
			if (dataRowArray1.Length > 0)
			{
				this._flex.Cols["NUM_STND_ITEM_1"].Caption = D.GetString(dataRowArray1[0]["NAME"]);
				this._flex.Cols["NUM_STND_ITEM_1"].Visible = true;
				this._flex.Cols["NUM_STND_ITEM_1"].DataType = typeof(decimal);
				this._flex.Cols["NUM_STND_ITEM_1"].Format = "#,###,##0.####";
				this._flex.Cols["NUM_STND_ITEM_1"].AllowEditing = false;
			}
			DataRow[] dataRowArray2 = dataTable.Select("CODE = '002'");
			if (dataRowArray2.Length > 0)
			{
				this._flex.Cols["NUM_STND_ITEM_2"].Caption = D.GetString(dataRowArray2[0]["NAME"]);
				this._flex.Cols["NUM_STND_ITEM_2"].Visible = true;
				this._flex.Cols["NUM_STND_ITEM_2"].DataType = typeof(decimal);
				this._flex.Cols["NUM_STND_ITEM_2"].Format = "#,###,##0.####";
				this._flex.Cols["NUM_STND_ITEM_2"].AllowEditing = false;
			}
			DataRow[] dataRowArray3 = dataTable.Select("CODE = '003'");
			if (dataRowArray3.Length > 0)
			{
				this._flex.Cols["NUM_STND_ITEM_3"].Caption = D.GetString(dataRowArray3[0]["NAME"]);
				this._flex.Cols["NUM_STND_ITEM_3"].Visible = true;
				this._flex.Cols["NUM_STND_ITEM_3"].DataType = typeof(decimal);
				this._flex.Cols["NUM_STND_ITEM_3"].Format = "#,###,##0.####";
				this._flex.Cols["NUM_STND_ITEM_3"].AllowEditing = false;
			}
			DataRow[] dataRowArray4 = dataTable.Select("CODE = '004'");
			if (dataRowArray4.Length > 0)
			{
				this._flex.Cols["NUM_STND_ITEM_4"].Caption = D.GetString(dataRowArray4[0]["NAME"]);
				this._flex.Cols["NUM_STND_ITEM_4"].Visible = true;
				this._flex.Cols["NUM_STND_ITEM_4"].DataType = typeof(decimal);
				this._flex.Cols["NUM_STND_ITEM_4"].Format = "#,###,##0.####";
				this._flex.Cols["NUM_STND_ITEM_4"].AllowEditing = false;
			}
			DataRow[] dataRowArray5 = dataTable.Select("CODE = '005'");
			if (dataRowArray5.Length > 0)
			{
				this._flex.Cols["NUM_STND_ITEM_5"].Caption = D.GetString(dataRowArray5[0]["NAME"]);
				this._flex.Cols["NUM_STND_ITEM_5"].Visible = true;
				this._flex.Cols["NUM_STND_ITEM_5"].DataType = typeof(decimal);
				this._flex.Cols["NUM_STND_ITEM_5"].Format = "#,###,##0.####";
				this._flex.Cols["NUM_STND_ITEM_5"].AllowEditing = false;
			}
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flex };

			this._flex.BeginSetting(1, 1, false);
			this._flex.SetDummyColumn("S");
			this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("CD_ITEM", "품목코드", 110, false);
			this._flex.SetCol("NM_ITEM", "품목명", 130, false);
			this._flex.SetCol("STND_ITEM", "규격", 70, false);
			this._flex.SetCol("CD_UNIT_MM", "발주단위", 70, false);
			this._flex.SetCol("CD_ZONE", "저장위치", 100, false);
			this._flex.SetCol("QT_REQ_MM", "입고량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.Cols["QT_REQ_MM"].Format = "#,###,###.####";
            if (this.bStandard)
			{
				this._flex.SetCol("UM_WEIGHT", "중량단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
				this._flex.SetCol("TOT_WEIGHT", "총중량", 100, true, typeof(decimal));
				this._flex.Cols["TOT_WEIGHT"].Format = "###,###,###.#";
			}
			int num1;
			if (!this.b단가권한)
				num1 = !MA.ServerKey(false, "KMI") ? 1 : 0;
			else
				num1 = 0;
			if (num1 == 0)
				this._flex.SetCol("UM_EX_PO", "단가", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			int num2;
			if (!this.b금액권한)
				num2 = !MA.ServerKey(false, "KMI") ? 1 : 0;
			else
				num2 = 0;
			if (num2 == 0)
			{
				this._flex.SetCol("AM_EXREQ", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				bool colEditing = false;
				if (Global.MainFrame.ServerKeyCommon == "HSGP")
					colEditing = true;
				this._flex.SetCol("AM_REQ", "원화금액", 100, colEditing, typeof(decimal), FormatTpType.MONEY);
				this._flex.SetCol("VAT", "부가세", 90, false, typeof(decimal), FormatTpType.MONEY);
				this._flex.SetCol("AM_TOTAL", "총금액", 80, true, typeof(decimal), FormatTpType.MONEY);
				if (MA.ServerKey(false, "WONIL", "DAEJOOKC", "MEERE"))
					this._flex.Cols["VAT"].AllowEditing = true;
			}
			if (Global.MainFrame.ServerKeyCommon == "HOTEL")
			{
				this._flex.Cols["UM_EX_PO"].AllowEditing = false;
				this._flex.Cols["AM_EXREQ"].AllowEditing = false;
				this._flex.Cols["AM_TOTAL"].AllowEditing = false;
			}
			if (MA.ServerKey(false, "KMI"))
			{
				this._flex.Cols["UM_EX_PO"].AllowEditing = this.b단가권한;
				this._flex.Cols["AM_EXREQ"].AllowEditing = this.b금액권한;
				this._flex.Cols["AM_TOTAL"].AllowEditing = this.b금액권한;
			}
			this._flex.SetCol("PI_PARTNER", "주거래처", false);
			this._flex.SetCol("PI_LN_PARTNER", "주거래처명", false);
			this._flex.SetCol("TP_UM_TAX", "부가세여부", 70, false);
			this._flex.SetCol("DT_LIMIT", "납기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("YN_LOT", "LOT여부", 60);
			this._flex.SetCol("YN_INSP", "검사", 50, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("CD_SL", "창고코드", 60, true);
			this._flex.SetCol("NM_SL", "창고명", 120, true);
			this._flex.SetCol("NO_PO", "발주번호", 100, false);
			this._flex.SetCol("NM_KOR", "담당자", 80, false);
			this._flex.SetCol("NM_FG_RCV", "입고형태", 100, false);
			this._flex.SetCol("NM_FG_POST", "발주상태", 100, false);
			this._flex.SetCol("CD_PJT", "PROJECT코드", 100, false);
			this._flex.SetCol("NM_PROJECT", "PROJECT", 100, false);
			this._flex.SetCol("NM_PURGRP", "구매그룹", 100, false);
			this._flex.SetCol("UNIT_IM", "관리단위", 90, false);
			this._flex.SetCol("NO_BL", "BL번호", 120, false);
			this._flex.SetCol("QT_REQ", "관리수량", 120, this.m_sEnv == "Y", typeof(decimal), FormatTpType.QUANTITY);
            this._flex.Cols["QT_REQ"].Format = "#,###,###.####";
            this._flex.SetCol("RT_EXCH", "환율", 120, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flex.SetCol("DC_RMK", "의뢰라인비고1", 150, 200, true);
			this._flex.SetCol("DC_RMK2", "의뢰라인비고2", 150, 40, true);
			this._flex.SetCol("DT_PLAN", "납기예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			if (this.m_Elec_app == "100")
			{
				this._flex.Rows[0]["DC_RMK"] = "문서번호";
				this._flex.Rows[0]["DC_RMK2"] = "입고번호";
			}
			if (Config.MA_ENV.PJT형여부 == "Y")
			{
				this._flex.SetCol("NO_CBS", "CBS번호", 140, false, typeof(string));
				if (!App.SystemEnv.PMS사용)
				{
					this._flex.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
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
				this._flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
				this._flex.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
				this._flex.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
				this._flex.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
				this._flex.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
			}
			this._flex.SetCol("REV_QT_PASS", "검수수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("REV_QT_REV_MM", "납품계획수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("CD_ITEM_ORIGIN", "관련품목", 140, false, typeof(string));
			this._flex.SetCol("NM_ITEM_ORIGIN", "관련품목명", 140, false, typeof(string));
			this._flex.SetCol("STND_ITEM_ORIGIN", "관련품목규격", 140, false, typeof(string));
			this._flex.SetCol("GI_PARTNER", "납품처코드", 140, false, typeof(string));
			this._flex.SetCol("NM_GI_PARTER", "납품처명", 140, false, typeof(string));
			this._flex.SetCol("CD_EXCH", "환종", 140, false, typeof(string));
			this._flex.SetCol("NO_REV", "가입고번호", 120, false, typeof(string));
			this._flex.SetCol("NO_REVLINE", "가입고항번", 120, false, typeof(decimal));
			if (this.MainFrameInterface.ServerKeyCommon == "WOORIERP")
			{
				DataTable code = MA.GetCode("PU_Z000007", false);
				if (code != null && code.Rows.Count != 0)
				{
					foreach (DataRow row in code.Rows)
					{
						string colCaptionDD = D.GetString(row["CD_FLAG1"]) == "" ? D.GetString(row["NAME"]) : D.GetString(row["CD_FLAG1"]);
						this._flex.SetCol(D.GetString(row["NAME"]), colCaptionDD, 100, false, typeof(string));
					}
				}
			}
			if (this.MainFrameInterface.ServerKeyCommon == "ICDERPU")
			{
				this._flex.SetCol("DATE_USERDEF1", "입고예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				this._flex.Cols["DC_RMK2"].Caption = "창고(적재장소)";
			}
			if (this.MainFrameInterface.ServerKeyCommon == "WINPLUS")
			{
				this._flex.SetCol("NUM_USERDEF1", "가로", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				this._flex.SetCol("NUM_USERDEF2", "세로", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			}
			if (this.MainFrameInterface.ServerKeyCommon == "UNIPOINT")
			{
				this._flex.SetCol("CD_PARTNER_PJT", "프로젝트 거래처코드", 100, false, typeof(string));
				this._flex.SetCol("LN_PARTNER_PJT", "프로젝트 거래처", 100, false, typeof(string));
				this._flex.SetCol("NO_EMP_PJT", "프로젝트 담당자코드", 100, false, typeof(string));
				this._flex.SetCol("NM_KOR_PJT", "프로젝트 담당자", 100, false, typeof(string));
				this._flex.SetCol("END_USER", "프로젝트 END USER", 100, false, typeof(string));
			}
			this._flex.SetCol("CLS_ITEM", "품목계정", 140, false, typeof(string));
			this._flex.SetCol("MAT_ITEM", "재질", 140, false, typeof(string));
			this._flex.SetCol("FG_TAX", "과세구분", false);
			this._flex.SetCol("NM_MAKER", "MAKER", 100, false);
			this._flex.Cols["FG_TAX"].AllowEditing = false;
			this._flex.SetCol("CD_PARTNER", "CD_PARTNER", 0, false, typeof(string));
			this._flex.Cols["CD_PARTNER"].Visible = false;
			if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_REQ_REG_1")
			{
				this._flex.SetCol("CLS_L", "대분류코드", 80, false);
				this._flex.SetCol("NM_CLS_L", "대분류", 140, false, typeof(string));
				this._flex.SetCol("NUM_STND_ITEM_1", "NUM_STND_ITEM_1", false);
				this._flex.SetCol("NUM_STND_ITEM_2", "NUM_STND_ITEM_2", false);
				this._flex.SetCol("NUM_STND_ITEM_3", "NUM_STND_ITEM_3", false);
				this._flex.SetCol("NUM_STND_ITEM_4", "NUM_STND_ITEM_4", false);
				this._flex.SetCol("NUM_STND_ITEM_5", "NUM_STND_ITEM_5", false);
				this._flex.SetCol("UM_WEIGHT", "단중", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
				this._flex.SetCol("UM_EX", "단가(재고단위)", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			}
			this._flex.SetCol("STND_DETAIL_ITEM", "세부규격", 140, false, typeof(string));
			this._flex.SetCol("NO_DESIGN", "품목도면번호", 100, false, typeof(string));
			this._flex.SetCol("NM_TPPO", "발주형태", 100, false, typeof(string));
			if (Global.MainFrame.ServerKeyCommon.Contains("KSCC") || Global.MainFrame.ServerKeyCommon.Contains("DZORA"))
			{
				this._flex.SetCol("CD_BUDGET", "예산단위코드", 150, false);
				this._flex.SetCol("NM_BUDGET", "예산단위명", 150, false);
				this._flex.SetCol("CD_BGACCT", "예산계정코드", 150, false);
				this._flex.SetCol("NM_BGACCT", "예산계정명", 150, false);
				this._flex.SetCol("CD_BIZPLAN", "사업계획", 150, false, typeof(string));
				this._flex.SetCol("NM_BIZPLAN", "사업계획명", 150, false, typeof(string));
				this._flex.SetCol("CD_ACCT", "회계계정코드", 100, false);
				this._flex.SetCol("NM_ACCT", "회계계정명", 100, false);
				this._flex.SetCol("NM_KOR_PR", "요청담당자", 100, false);
				this._flex.SetCol("CD_USERDEF5_IO", "자산유형확인생략", 100, true, CheckTypeEnum.Y_N);
			}
			if (BASIC.GetMAEXC("구매입고-의뢰LOT 자동적용") == "100")
			{
				if (Global.MainFrame.ServerKeyCommon == "MAIIM")
				{
					this._flex.SetCol("NO_LOT", "LOT번호(마임)", 120, false);
					this._flex.SetCol("NM_USERDEF1_RCV", "LOT번호(업체)", 120, false);
				}
				else
				{
					this._flex.SetCol("NO_LOT", "LOT번호", 100, false);
					this._flex.SetCol("DT_LIMIT_LOT", "유효일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				}
			}
			if (!this.bStandard)
			{
				this._flex.SetCol("WEIGHT", "단위중량", 80, 17, false, typeof(decimal), FormatTpType.MONEY);
				this._flex.SetCol("QT_WEIGHT", "총중량", 80, 17, false, typeof(decimal), FormatTpType.MONEY);
			}
			this._flex.SetCol("GRP_ITEM", "품목군코드", 100, false);
			this._flex.SetCol("NM_ITEMGRP", "품목군명", 100, false);
			this._flex.SetCol("NO_TO", "통관번호", 120, false);
			this._flex.SetCol("TP_ITEM", "품목타입코드", false);
			this._flex.SetCol("NM_TP_ITEM", "품목타입", 100, false);
			this._flex.SetCol("DC1_POL", "발주라인비고1", false);
			this._flex.SetCol("NO_POLINE", "발주항번", false);
			if (Global.MainFrame.ServerKeyCommon.Contains("CNCINTER"))
				this._flex.SetCol("CD_USERDEF2_PI", "멸균정보", 100, false);
			if (Global.MainFrame.ServerKeyCommon.Contains("HIOKI"))
				this.setCol_HIOKI();
			this._flex.SetCol("EN_ITEM", "품목명(영)", false);
			if (App.SystemEnv.PROJECT사용)
			{
				if (Config.MA_ENV.YN_UNIT == "Y")
					this._flex.VerifyNotNull = new string[] { "CD_ITEM",
															  "DT_LIMIT",
															  "NM_SL",
															  "NM_PROJECT",
															  "SEQ_PROJECT" };
				else
					this._flex.VerifyNotNull = new string[] { "CD_ITEM",
															  "DT_LIMIT",
															  "NM_SL",
															  "NM_PROJECT" };
			}
			else
				this._flex.VerifyNotNull = new string[] { "CD_ITEM",
														  "DT_LIMIT",
														  "NM_SL" };
			Config.UserColumnSetting.InitGrid_UserMenu(this._flex, this.PageID, true);
			this._flex.SettingVersion = "1.0.0.4";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			if (Config.MA_ENV.YN_UNIT == "Y")
				this._flex.SetExceptSumCol("UM_EX_PO", "SEQ_PROJECT");
			else if (this.bStandard)
				this._flex.SetExceptSumCol("UM_EX_PO", "UM_WEIGHT");
			else
				this._flex.SetExceptSumCol("UM_EX_PO");
			this._flex.VerifyCompare(this._flex.Cols["QT_REQ_MM"], 0, OperatorEnum.Greater);
			this._flex.SetExceptEditCol("CD_ITEM", "NM_ITEM", "STND_ITEM", "CD_UNIT_MM", "UNIT", "NM_SL", "NO_PO", "NM_KOR", "NM_FG_RCV", "NM_FG_POST", "NM_PROJECT", "UNIT_IM");
			this._flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }
																					  , new string[] { "CD_SL", "NM_SL" }
																					  , new string[] { "CD_SL", "NM_SL" });
			this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._Grid_BeforeCodeHelp);
			this._flex.ValidateEdit += new ValidateEditEventHandler(this._Grid_ValidateEdit);
			this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
			this._flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex.UseMultySorting = true;
		}

		private void InitGridD()
		{
			this._flexD.BeginSetting(1, 1, false);
			this._flexD.SetCol("CD_ITEM", "의뢰품목", 100, 20, false);
			this._flexD.SetCol("NM_ITEM_ITEM", "품목명", 140, 50, false);
			this._flexD.SetCol("STND_ITEM_ITEM", "규격", 120, 50, false);
			this._flexD.SetCol("UNIT_IM_ITEM", "단위", 40, 3, false);
			this._flexD.SetCol("CD_MATL", "자재코드", 100, 20, false);
			this._flexD.SetCol("NM_ITEM", "자재명", 140, 50, false);
			this._flexD.SetCol("STND_ITEM", "규격", 120, 50, false);
			this._flexD.SetCol("UNIT_IM", "단위", 40, 3, false);
			this._flexD.SetCol("QT_NEED", "출고의뢰수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flexD.SetCol("NO_PO", "발주번호", 100, 20, false);
			this._flexD.SetCol("NO_POLINE", "발주항번", 60, 5, false);
			this._flexD.SetCol("NO_PO_MAL_LINE", "사급자재항번", 80, 5, false);
			this._flexD.SetCol("CD_SL", "창고코드", 80, 7, true, typeof(string));
			this._flexD.SetCol("NM_SL", "창고명", 120, false, typeof(string));
			if (Config.MA_ENV.프로젝트사용)
			{
				this._flexD.SetCol("CD_PJT", "프로젝트", 140, 100, true, typeof(string));
				this._flexD.SetCol("NM_PJT", "프로젝트명", 140, 100, false, typeof(string));
				this._flexD.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
			}
			if (Config.MA_ENV.PJT형여부 == "Y")
			{
				this._flexD.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
				this._flexD.SetCol("PJT_NM_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
				this._flexD.SetCol("PJT_STND_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
			}
			this._flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			this._flexD.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }
																					   , new string[] { "CD_SL", "NM_SL" }
																					   , new string[] { "CD_SL", "NM_SL" });
			if (Config.MA_ENV.YN_UNIT == "Y")
			{
				this._flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
																										 "NM_PJT",
		  																								 "SEQ_PROJECT",
		  																								 "CD_PJT_ITEM",
		  																								 "PJT_NM_ITEM",
		  																								 "PJT_STND_ITEM" }
																						, new string[] { "NO_PROJECT",
		  																								 "NM_PROJECT",
		  																								 "SEQ_PROJECT",
		  																								 "CD_PJT_ITEM",
		  																								 "NM_PJT_ITEM",
		  																								 "PJT_ITEM_STND" }
																						, new string[] { "CD_PJT",
		  																								 "NM_PROJECT",
		  																								 "SEQ_PROJECT",
		  																								 "CD_PJT_ITEM",
		  																								 "PJT_NM_ITEM",
																								 		 "PJT_STND_ITEM" }, ResultMode.FastMode);
				this._flexD.SetCodeHelpCol("CD_PJT_ITEM", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
																											  "NM_PROJECT",
																											  "SEQ_PROJECT",
																											  "CD_PJT_ITEM",
																											  "PJT_NM_ITEM",
																											  "PJT_STND_ITEM" }
																							 , new string[] { "NO_PROJECT",
																											  "NM_PROJECT",
																											  "SEQ_PROJECT",
																											  "CD_PJT_ITEM",
																											  "NM_PJT_ITEM",
																											  "PJT_ITEM_STND" }
																							 , new string[] { "CD_PJT",
																											  "NM_PROJECT",
																											  "SEQ_PROJECT",
																											  "CD_PJT_ITEM",
																											  "PJT_NM_ITEM",
																											  "PJT_STND_ITEM" }, ResultMode.FastMode); 
			}
			else
				this._flexD.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PROJECT" }
																						, new string[] { "NO_PROJECT", "NM_PROJECT" });
			this._flexD.Cols["NO_POLINE"].TextAlign = TextAlignEnum.CenterCenter;
			this._flexD.Cols["NO_PO_MAL_LINE"].TextAlign = TextAlignEnum.CenterCenter;
		}

		private void InitEvent()
		{
			this.DataChanged += new EventHandler(this.Page_DataChanged);
			this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);
			this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);

			this._flexD.StartEdit += new RowColEventHandler(this._flex_StartEdit);
			this._flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._Grid_BeforeCodeHelp);
			this._flexD.ValidateEdit += new ValidateEditEventHandler(this._Grid_ValidateEdit);

			this.tab.SelectedIndexChanged += new EventHandler(this.tab_SelectedIndexChanged);
			this.cbo_TRANS.SelectedIndexChanged += new EventHandler(this.cbo_TRANS_SelectionChangeCommitted);
			this.cbo_PROCESS.SelectedIndexChanged += new EventHandler(this.cbo_PROCESS_SelectionChangeCommitted);
			this.txt바코드.KeyPress += new KeyPressEventHandler(this.txt바코드_KeyPress);
			this.chk바코드사용유무.CheckedChanged += new EventHandler(this.chk_barcode_use_CheckedChanged);
			this.btn입고번호적용.Click += new EventHandler(this.btn_Appet_Click);
			this.btn삭제.Click += new EventHandler(this.m_btnDel_Click);
			this.btn통관적용.Click += new EventHandler(this.통관적용_Click);
			this.btnLocalLC.Click += new EventHandler(this.Local_LC_Click);
			this.btn발주적용.Click += new EventHandler(this.발주적용_Click);
			this.btn가입고적용.Click += new EventHandler(this.m_btn_Rev_Click);
			this.btn수입검사적용.Click += new EventHandler(this.b_IQC_Apply_Click);
		}

		private void Initial_Binding()
		{
			DataSet dataSet = this._biz.Initial_DataSet();
			this._header.SetBinding(dataSet.Tables[0], this.panel_Head);
			this._header.ClearAndNewRow();
			this._flex.Binding = dataSet.Tables[1];
		}

		protected override bool SaveData()
		{
			string text = this.tb_NoIsuRcv.Text;
			string no_ioseq = "";
			if (this.추가모드여부)
			{
				string seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "04", this.tb_DT_RCV.Text.Substring(0, 6));
				no_ioseq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "06", this.tb_DT_RCV.Text.Substring(0, 6));
				if (seq == "" || seq == null)
					return false;
				this._header.CurrentRow["NO_RCV"] = seq;
				this._header.CurrentRow["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
				if (this._flex.HasNormalRow)
				{
					foreach (DataRow row in this._flex.DataTable.Rows)
					{
						if (Convert.ToDecimal(row["QT_REQ"]) == 0M)
						{
							this.ShowMessage("수불 수량이 0이 있습니다.");
							return false;
						}
						row["NO_RCV"] = seq;
						row["NO_IO"] = no_ioseq;
						row["DT_IO"] = this._header.CurrentRow["DT_REQ"];
						row["FG_IO"] = "001";
						row["CD_QTIOTP"] = row["FG_RCV"];
						row["NM_QTIOTP"] = row["NM_FG_RCV"];
						row["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
					}
				}
				this.tb_NoIsuRcv.Text = seq;
			}
			else if (this._flex.HasNormalRow)
			{
				DataRow[] dataRowArray1 = this._flex.DataTable.Select("ISNULL(NO_RCV,'') = '' OR ISNULL(NO_IO,'') = ''", "", DataViewRowState.Added);
				DataRow[] dataRowArray2 = this._flex.DataTable.Select("ISNULL(NO_IO,'') <> ''");
				if (dataRowArray2 == null || dataRowArray2.Length < 1)
				{
					this.ShowMessage("저장된 내용중 수불번호가 없습니다. 확인 바랍니다.");
					return false;
				}
				foreach (DataRow dataRow in dataRowArray1)
				{
					if (dataRow.RowState == DataRowState.Added)
					{
						dataRow["NO_RCV"] = this._header.CurrentRow["NO_RCV"];
						dataRow["NO_IO"] = dataRowArray2[0]["NO_IO"];
						dataRow["DT_IO"] = this._header.CurrentRow["DT_REQ"];
						dataRow["FG_IO"] = "001";
						dataRow["CD_QTIOTP"] = dataRow["FG_RCV"];
						dataRow["NM_QTIOTP"] = dataRow["NM_FG_RCV"];
						dataRow["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
					}
				}
			}
			if (this._header.CurrentRow != null && this._flex.HasNormalRow && this._header.CurrentRow["FG_RCV"].ToString() == "")
				this._header.CurrentRow["FG_RCV"] = this._flex.DataTable.Rows[0]["FG_RCV"].ToString();
			DataTable changes1 = this._header.GetChanges();
			DataTable changes2 = this._flex.GetChanges();
			DataTable dtLOT = null;
			DataTable dtSERL = null;
			if (changes1 == null && changes2 == null)
			{
				this.ShowMessage(공통메세지.변경된내용이없습니다);
				return false;
			}
			if (changes2 != null && changes2.Rows.Count > 0)
			{
				DataRow[] dataRowArray3 = changes2.Select("(QT_REQ_MM * RATE_EXCHG) <> QT_REQ", "");
				if (this.m_sEnv == "Y" && dataRowArray3 != null && dataRowArray3.Length > 0)
				{
					P_PU_REQCHK_SUB pPuReqchkSub = new P_PU_REQCHK_SUB(this.MainFrameInterface, changes2);
					if (pPuReqchkSub.ShowDialog((IWin32Window)this) == DialogResult.Cancel)
						return false;
					DataTable gdtReturn = pPuReqchkSub.gdt_return;
					if (gdtReturn != null && gdtReturn.Rows.Count > 0)
					{
						for (int index1 = 0; index1 < gdtReturn.Rows.Count; ++index1)
						{
							for (int index2 = 0; index2 < this._flex.DataTable.Rows.Count; ++index2)
							{
								if (this._flex.DataTable.Rows[index2]["NO_RCV"].ToString() == gdtReturn.Rows[index1]["NO_RCV"].ToString() && this._flex.DataTable.Rows[index2]["NO_LINE"].ToString() == gdtReturn.Rows[index1]["NO_LINE"].ToString())
								{
									this._flex.DataTable.Rows[index2]["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, Convert.ToDecimal(this._flex.DataTable.Rows[index2]["QT_REQ_MM"]) * Convert.ToDecimal(this._flex.DataTable.Rows[index2]["RATE_EXCHG"]));
									break;
								}
							}
						}
						changes2 = this._flex.GetChanges();
					}
				}
				if (changes2 != null)
				{
					changes2.Clone();
					DataTable table = new DataView(changes2, "YN_LOT = 'YES'", "", DataViewRowState.CurrentRows).ToTable();
					if (table.Rows.Count > 0)
					{
						if (this.m_sPJT재고사용 == "100" && this.m_bPJT사용)
						{
							dtLOT = table;
							dtLOT.Columns.Add("FG_PS", typeof(string), "1");
							dtLOT.Columns.Add("QT_IO", typeof(decimal));
							dtLOT.Columns.Add("NO_IOLINE2", typeof(decimal), "0");
							dtLOT.Columns.Add("DC_LOTRMK", typeof(string), "");
							dtLOT.Columns.Remove("YN_RETURN");
							foreach (DataRow row in dtLOT.Rows)
							{
								row["NO_LOT"] = row["CD_PJT"];
								row["QT_IO"] = row["QT_REQ"];
							}
						}
						else if (this.서버키("SRPACK") && (D.GetString(this.cbo_TRANS.SelectedValue) == "004" || D.GetString(this.cbo_TRANS.SelectedValue) == "005"))
						{
							P_PU_Z_SRPACK_LOT_SUB_R puZSrpackLotSubR = new P_PU_Z_SRPACK_LOT_SUB_R(table);
							if (puZSrpackLotSubR.ShowDialog((IWin32Window)this) != DialogResult.OK)
								return false;
							dtLOT = puZSrpackLotSubR.dtL;
							for (int index = 1; index <= 20; ++index)
								dtLOT.Columns.Add("CD_MNG" + index, typeof(string), "");
						}
						else
						{
                            P_CZ_PU_LOT_SUB_R pCzPuLotSubR;
							if (this.서버키("SATREC"))
							{
								string[] strArray = new string[] { this.tb_DT_RCV.Text };
                                pCzPuLotSubR = new P_CZ_PU_LOT_SUB_R(table, strArray);
							}
							else
                                pCzPuLotSubR = new P_CZ_PU_LOT_SUB_R(table);
							if (pCzPuLotSubR.ShowDialog((IWin32Window)this) != DialogResult.OK)
								return false;
							dtLOT = pCzPuLotSubR.dtL;
						}
					}
				}
				if (string.Compare(this.MNG_SERIAL, "Y") == 0 && changes2 != null)
				{
					DataRow[] dataRowArray4 = changes2.Select("NO_SERL = 'YES'", "", DataViewRowState.Added);
					DataTable dt = changes2.Clone();
					if (dataRowArray4.Length > 0)
					{
						foreach (DataRow row in dataRowArray4)
							dt.ImportRow(row);
						P_PU_SERL_SUB_R pPuSerlSubR = new P_PU_SERL_SUB_R(dt);
						if (pPuSerlSubR.ShowDialog((IWin32Window)this) != DialogResult.OK)
							return false;
						dtSERL = pPuSerlSubR.dtL;
					}
				}
			}
			DataTable dtLOCATION = null;
			if (Config.MA_ENV.YN_LOCATION == "Y")
			{
				bool b_return = false;
				DataTable dt = changes2.Clone().Copy();
				foreach (DataRow dataRow in changes2.Select())
					dt.LoadDataRow(dataRow.ItemArray, true);
				if (dt.Rows.Count > 0)
				{
					dtLOCATION = P_OPEN_SUBWINDOWS.P_MA_LOCATION_R_SUB(dt, out b_return);
					if (!b_return)
						return false;
				}
			}
			DataTable dtHH = null;
			DataTable dataTable = null;
			if (this.m_YN_SU == "100")
			{
				dataTable = this._flexD.GetChanges();
				if (dataTable != null && dataTable.Rows.Count != 0)
				{
					string str;
					string NO_IO;
					if (this.추가모드여부)
					{
						str = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "15", this.tb_DT_RCV.Text.Substring(0, 6));
						NO_IO = no_ioseq;
					}
					else
					{
						NO_IO = this._biz.getNO_IO_MGMT(this.tb_NoIsuRcv.Text);
						str = this._biz.getNO_IO(NO_IO);
						if (str == string.Empty)
							str = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "15", this.tb_DT_RCV.Text.Substring(0, 6));
					}
					foreach (DataRow row in dataTable.Rows)
					{
						if (D.GetString(row["CD_SL"]) == string.Empty)
						{
							this.ShowMessage(공통메세지._은는필수입력항목입니다, "창고");
							return false;
						}
						row["NO_IO"] = str;
						row["NO_IO_MGMT"] = NO_IO;
					}
					foreach (DataRow row in new DataView(dataTable, "YN_PARTNER_SL = 'Y'", "", DataViewRowState.CurrentRows).ToTable(true, "CD_PLANT", "CD_PARTNER", "CD_SL").Rows)
					{
						string cdSl = this._biz.getCD_SL(D.GetString(row["CD_PARTNER"]), D.GetString(row["CD_PLANT"]));
						if (D.GetString(row["CD_SL"]) != cdSl)
						{
							this.ShowMessage(공통메세지._와_은같아야합니다, this.DD("출고창고"), this.DD("외주거래처별창고"));
							return false;
						}
					}
					if (this.추가모드여부)
					{
						dtHH = dataTable.Clone();
						dtHH.ImportRow(dataTable.Rows[0]);
					}
				}
			}
			if (!this._biz.Save(changes1, changes2, dtLOT, no_ioseq, dtSERL, D.GetString(this.cbo공장.SelectedValue), D.GetString(this.tb_NoIsuRcv.Text), dtLOCATION, this.m_YN_special, dtHH, dataTable))
				return false;
			this._header.AcceptChanges();
			this._flex.AcceptChanges();
			if (this.m_YN_SU == "100")
				this._flexD.AcceptChanges();
			if (this._flex.HasNormalRow)
				this.txt_NOIO.Text = D.GetString(this._flex.DataTable.Rows[0]["NO_IO"]);
			this.btn발주적용.Enabled = false;
			this.btnLocalLC.Enabled = false;
			this.btn통관적용.Enabled = false;
			this.ControlEnabledDisable(false);
			return true;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeSearch())
					return;
				P_PU_REQ_SUB pPuReqSub = new P_PU_REQ_SUB(this.tb_NM_PARTNER.CodeValue.ToString(), D.GetString(this.cbo공장.SelectedValue), this.tb_NO_EMP.CodeValue.ToString(), "Y");
				if (pPuReqSub.ShowDialog() == DialogResult.OK)
				{
					DataSet dataSet = this._biz.Search(pPuReqSub.m_NO_RCV, "Y");
					this._header.SetDataTable(dataSet.Tables[0]);
					this.txt_NOIO.Text = string.Empty;
					if (dataSet != null && dataSet.Tables.Count > 1)
					{
						this.Button_Enabled(dataSet.Tables[0], dataSet.Tables[1]);
						this._flex.Binding = dataSet.Tables[1];
						if (!this._flex.HasNormalRow && !this._header.CurrentRow.IsNull(0))
						{
							this.ShowMessage(PageResultMode.SearchNoData);
						}
						if (this.m_YN_SU == "100")
						{
							this._flexD.Binding = this._biz.Search_MATL(this._biz.getNO_IO_MGMT(this.tb_NoIsuRcv.Text));
							this._flexD.RowFilter = "NO_PO = '" + D.GetString(this._flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flex["NO_POLINE"]) + "' ";
						}
						this._header.AcceptChanges();
						this._flex.AcceptChanges();
						this.ControlEnabledDisable(false);
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Button_Enabled(DataTable ldt_head, DataTable ldt_line)
		{
			if (ldt_head == null || ldt_head.Rows.Count <= 0)
				return;
			DataRow row = ldt_head.Rows[0];
			DataRow[] dataRowArray = ldt_line.Select("QT_GR_MM > 0");
			this._isChagePossible = dataRowArray == null || dataRowArray.Length <= 0;
			if (!this._isChagePossible)
			{
				this.btn발주적용.Enabled = false;
				this.btn통관적용.Enabled = false;
				this.btnLocalLC.Enabled = false;
			}
			if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "001" || ldt_head.Rows[0]["FG_TRANS"].ToString() == "002")
			{
				this.cbo_PROCESS.Enabled = false;
				this.btn발주적용.Enabled = true;
				this.btn통관적용.Enabled = false;
				this.btnLocalLC.Enabled = false;
			}
			else if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "003")
			{
				this.cbo_PROCESS.Enabled = true;
				if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
				{
					this.btn발주적용.Enabled = false;
					this.btn통관적용.Enabled = false;
					this.btnLocalLC.Enabled = true;
				}
				if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "002")
				{
					this.btn발주적용.Enabled = true;
					this.btn통관적용.Enabled = false;
					this.btnLocalLC.Enabled = false;
				}
			}
			else if (ldt_head.Rows[0]["FG_TRANS"].ToString() == "004" || ldt_head.Rows[0]["FG_TRANS"].ToString() == "005")
			{
				this.cbo_PROCESS.Enabled = true;
				if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
				{
					this.btn발주적용.Enabled = false;
					this.btnLocalLC.Enabled = false;
					this.btn통관적용.Enabled = true;
				}
			}
			if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "001")
			{
				if (this.cbo_TRANS.SelectedValue.ToString() == "002")
				{
					this.btn발주적용.Enabled = true;
					this.btn통관적용.Enabled = false;
				}
				if (this.cbo_TRANS.SelectedValue.ToString() == "003")
				{
					this.btn발주적용.Enabled = false;
					this.btn통관적용.Enabled = false;
					this.btnLocalLC.Enabled = true;
				}
				if (this.cbo_TRANS.SelectedValue.ToString() == "004" || this.cbo_TRANS.SelectedValue.ToString() == "005")
				{
					this.btn발주적용.Enabled = false;
					this.btn통관적용.Enabled = true;
					this.btnLocalLC.Enabled = false;
				}
			}
			if (ldt_head.Rows[0]["FG_PROCESS"].ToString() == "002")
			{
				if (this.cbo_TRANS.SelectedValue.ToString() == "002")
				{
					this.btn발주적용.Enabled = true;
					this.btn통관적용.Enabled = false;
				}
				if (this.cbo_TRANS.SelectedValue.ToString() == "003")
				{
					this.btn발주적용.Enabled = true;
					this.btn통관적용.Enabled = false;
					this.btnLocalLC.Enabled = false;
				}
			}
		}

		public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeAdd())
					return;
				Debug.Assert(this._header.CurrentRow != null);
				Debug.Assert(this._flex.DataTable != null);
				this._flex.DataTable.Rows.Clear();
				this._flex.AcceptChanges();
				this._header.ClearAndNewRow();
				this.InitControl();
				this.ControlEnabledDisable(true);
				this.cbo_TRANS_SelectionChangeCommitted(null, null);
				if (this.cbo_TRANS.SelectedValue.ToString() == "001")
					this.cbo_PROCESS.Enabled = false;
				if (this.m_YN_SU == "100")
				{
					this._flexD.DataTable.Rows.Clear();
					this._flexD.AcceptChanges();
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
				if (!this.BeforeDelete() || MessageBoxEx.Show(this.GetMessageDictionaryItem("MA_M000103"), this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes || this._flex == null || !this._flex.HasNormalRow)
					return;
				string str1 = this._header.CurrentRow["NO_RCV"].ToString();
				string NO_IO = this._flex[this._flex.Rows.Fixed, "NO_IO"].ToString();
				string str2 = D.GetString(this.tb_NoIsuRcv.Text);
				string str3 = string.Empty;
				if (this.m_YN_SU == "100")
					str3 = this._biz.getNO_IO(NO_IO);
				this._biz.Delete(new object[] { str1, NO_IO, this.LoginInfo.CompanyCode, str3 }
							   , new object[] { this.LoginInfo.CompanyCode, str2 });
				this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
				this.OnToolBarAddButtonClicked(sender, e);
				this.ControlEnabledDisable(true);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool BeforeSave() => base.BeforeSave() && this.Verify();

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!this.BeforeSave() || !this.FieldCheck() || !this.IsChanged())
					return;
				this.ToolBarSaveButtonEnabled = false;
				if (this.MsgAndSave(PageActionMode.Save))
				{
					this.ShowMessage(PageResultMode.SaveGood);
					if (!this._flex.HasNormalRow)
						this.OnToolBarAddButtonClicked(null, null);
					else
						this.Button_Enabled(this._header.CurrentRow.Table, this._flex.DataTable);
				}
				else
					this.ToolBarSaveButtonEnabled = true;
				if (this.chk바코드사용유무.Checked)
					this.txt바코드.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
				this.ToolBarSaveButtonEnabled = true;
			}
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				this.SetPrint(true);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void SetPrint(bool check)
		{
			if (!this._flex.HasNormalRow)
			{
				this.ShowMessage(공통메세지._이가존재하지않습니다, "데이터");
			}
			else if (this.tb_NoIsuRcv.Text == "" || this.tb_NoIsuRcv.Text == null)
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl의뢰번호.Text);
			}
			else
			{
				ReportHelper reportHelper = new ReportHelper("R_P_PU_REQ_REG_1", "구매입고처리");
				Dictionary<string, string> dic = new Dictionary<string, string>();
				dic["의뢰번호"] = D.GetString(this._flex["NO_RCV"]);
				dic["입고일자"] = D.GetString(this.tb_DT_RCV.Text);
				dic["거래처코드"] = D.GetString(this.tb_NM_PARTNER.CodeValue);
				dic["거래처명"] = D.GetString(this.tb_NM_PARTNER.CodeName);
				dic["담당자코드"] = D.GetString(this.tb_NO_EMP.CodeValue);
				dic["담당자명"] = D.GetString(this.tb_NO_EMP.CodeName);
				dic["비고"] = D.GetString(this._header.CurrentRow["DC_RMK"].ToString());
				dic["거래구분"] = D.GetString(this._header.CurrentRow["NM_EXCH"].ToString());
				dic["입고번호"] = D.GetString(this._flex["NO_IO"]);
				foreach (string key in dic.Keys)
					reportHelper.SetData(key, dic[key]);
				DataTable dt = this._biz.Search_Print(D.GetString(this._flex["NO_RCV"]));
				reportHelper.SetDataTable(dt);
				reportHelper.가로출력();
				if (check)
				{
					reportHelper.Print();
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					string str1 = D.GetString(this.tb_NO_EMP.CodeName) + "/" + D.GetString(this.tb_NoIsuRcv.Text) + "/" + D.GetString(this._flex[this._flex.Rows.Fixed, "CD_PJT"]) + "의 구매입고가 등록되었습니다.";
					string[] str_histext = new string[3];
					DataTable dataTable;
					if (Global.MainFrame.ServerKeyCommon == "ICDERPU")
					{
						foreach (DataRow row in this._flex.DataTable.Rows)
						{
							string str2 = "품목코드: " + D.GetString(row["CD_ITEM"]) + " / 품목명: " + D.GetString(row["NM_ITEM"]) + " / 규격: " + D.GetString(row["STND_ITEM"]) + " / 단위: " + D.GetString(row["UNIT_IM"]) + " / 수량: " + D.GetDecimal(row["QT_REQ_MM"]).ToString("#,###,##0.####") + " / 프로젝트코드: " + D.GetString(row["CD_PJT"]) + "/ 프로젝트명: " + D.GetString(row["NM_PROJECT"]);
							stringBuilder.AppendLine(str2);
							stringBuilder.AppendLine("\n\n");
						}
						str_histext[0] = str1;
						str_histext[2] = stringBuilder.ToString();
						dataTable = this._biz.getMail_Adress_ICD(this._flex.DataTable);
						if (dataTable != null && dataTable.Rows.Count != 0)
						{
							foreach (DataRow row in dataTable.Rows)
							{
								string[] strArray;
								string str3 = (strArray = str_histext)[1] + D.GetString(row["NM_KOR"]) + "|" + D.GetString(row["NO_EMAIL"]) + "|N?";
								strArray[1] = str3;
							}
						}
					}
					else
					{
						foreach (DataRow row in this._flex.DataTable.Rows)
						{
							string[] strArray1 = new string[] { "품목코드: ", D.GetString(row["CD_ITEM"]),
																" / 품목명: ", D.GetString(row["NM_ITEM"]),
																" / 규격: ", D.GetString(row["STND_ITEM"]),
																" / 단위: ", D.GetString(row["UNIT_IM"]),
																" / 수량: ", null,
																null, null,
																null, null,
																null, null,
																null, null };
							string[] strArray2 = strArray1;
							decimal num3 = D.GetDecimal(row["QT_REQ_MM"]);
							string str4 = num3.ToString("#,###,##0.####");
							strArray2[9] = str4;
							strArray1[10] = " / 단가: ";
							string[] strArray3 = strArray1;
							num3 = D.GetDecimal(row["UM_EX_PO"]);
							string str5 = num3.ToString("#,###,##0.####");
							strArray3[11] = str5;
							strArray1[12] = " / 금액: ";
							string[] strArray4 = strArray1;
							num3 = D.GetDecimal(row["AM_EXREQ"]);
							string str6 = num3.ToString("#,###,##0.####");
							strArray4[13] = str6;
							strArray1[14] = " / 프로젝트코드: ";
							strArray1[15] = D.GetString(row["CD_PJT"]);
							strArray1[16] = "/ 프로젝트명: ";
							strArray1[17] = D.GetString(row["NM_PROJECT"]);
							string str7 = string.Concat(strArray1);
							stringBuilder.AppendLine(str7);
							stringBuilder.AppendLine("\n\n");
						}
						str_histext[0] = str1;
						str_histext[1] = Settings1.Default.email_add;
						str_histext[2] = stringBuilder.ToString();
						dataTable = this._biz.getMail_Adress(this._flex.DataTable.DefaultView.ToTable(true, "NO_PO"));
					}
					if (dataTable != null && dataTable.Rows.Count != 0)
					{
						foreach (DataRow row in dataTable.DefaultView.ToTable(true, "NO_EMP", "NM_KOR", "NO_EMAIL").Rows)
						{
							string[] strArray;
							string str8 = (strArray = str_histext)[1] + D.GetString(row["NM_KOR"]) + "|" + D.GetString(row["NO_EMAIL"]) + "|N?";
							strArray[1] = str8;
						}
					}
					P_MF_EMAIL pMfEmail = new P_MF_EMAIL(new string[] { D.GetString( this.tb_NM_PARTNER.CodeValue) }, "R_P_PU_REQ_REG_1", new ReportHelper[] { reportHelper }, dic, "구매입고처리", str_histext);
					pMfEmail.ShowDialog();
					Settings1.Default.email_add = pMfEmail._str_rt_data[0];
					Settings1.Default.Save();
				}
			}
		}

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			try
			{
				Settings1.Default.cd_sl_apply = this.bp_CD_SL.CodeValue;
				Settings1.Default.nm_sl_apply = this.bp_CD_SL.CodeName;
				Settings1.Default.cd_plant = this.cbo공장.SelectedValue.ToString();
				Settings1.Default.Save();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			return true;
		}

        private void 발주적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Cheak_For_btn())
                    return;
                string FG_TRANS = this.cbo_TRANS.SelectedValue.ToString();
                string CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                string codeValue = this.tb_NM_PARTNER.CodeValue;
                string codeName = this.tb_NM_PARTNER.CodeName;
                P_PU_REQPO_SUB pPuReqpoSub;
                if (Global.MainFrame.ServerKeyCommon == "KORAVL")
                    pPuReqpoSub = new P_PU_REQPO_SUB(FG_TRANS, CD_PLANT, codeValue, codeName, this._flex.DataTable, this.tb_NO_EMP.CodeValue, this.tb_NO_EMP.CodeName);
                else
                    pPuReqpoSub = new P_PU_REQPO_SUB(new object[] { 0, 1, 2, 3, 6, 7 }
                                                   , new object[] { FG_TRANS, CD_PLANT, codeValue, codeName, Global.MainFrame.ServerKeyCommon == "ASUNG" ? D.GetString(this.tb_DT_RCV.Text) : "", this._flex.DataTable });
                this.btn통관적용.Enabled = false;
                if (pPuReqpoSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    this.cbo_TRANS.Enabled = false;
                    foreach (DataColumn column in this._flex.DataTable.Clone().Columns)
                    {
                        if (column.DataType == typeof(decimal))
                            column.DefaultValue = 0;
                    }
                    this.InserGridtAddREQ(pPuReqpoSub.gdt_return);
                    this.ControlEnabledDisable(false);
                    if (Global.MainFrame.ServerKeyCommon == "WGSK")
                    {
                        this.tb_DC.Text = pPuReqpoSub.gdt_return.Rows[0]["DC50_PO_H"].ToString();
                        this._header.CurrentRow["DC_RMK"] = pPuReqpoSub.gdt_return.Rows[0]["DC50_PO_H"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InserGridtAddREQ(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                this._flex.Redraw = false;
                string NO_PO_MULTI = string.Empty;
                bool flag = pdt_Line.Columns.Contains("EN_ITEM");
                DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PU_REQPO_SUB");
                if (Global.MainFrame.ServerKeyCommon.Contains("AMICOGEN") && pdt_Line.Rows.Count > 0 && this.tb_DT_RCV.Enabled)
                {
                    string str = D.GetString(pdt_Line.Compute("MAX(DT_LIMIT)", ""));
                    this._header.CurrentRow["DT_REQ"] = str;
                    this.tb_DT_RCV.Text = str;
                }
                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    DataRow row = pdt_Line.Rows[index];
                    ++maxValue;
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex["NO_LINE"] = maxValue;
                    this._flex["NO_IOLINE"] = maxValue;
                    this._flex["CD_ITEM"] = row["CD_ITEM"];
                    this._flex["NM_ITEM"] = row["NM_ITEM"];
                    this._flex["STND_ITEM"] = row["STND_ITEM"];
                    if (row["CD_UNIT_MM"] == null)
                        row["CD_UNIT_MM"] = "";
                    this._flex["CD_UNIT_MM"] = row["CD_UNIT_MM"];
                    this._flex["UNIT_IM"] = row["UNIT_IM"];
                    this._flex["RATE_EXCHG"] = row["RATE_EXCHG"];
                    this._flex["RT_VAT"] = row["RT_VAT"];
                    this._flex["DT_LIMIT"] = row["DT_LIMIT"];
                    if (row["CD_ZONE"] == null)
                        row["CD_ZONE"] = "";
                    this._flex["CD_ZONE"] = row["CD_ZONE"];
                    this._flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));
                    this._flex["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));
                    this._flex["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flex["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flex["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flex["QT_REJECTION"] = 0;
                    this._flex["YN_INSP"] = "N";
                    if (row["FG_IQC"].ToString() == "Y")
                        this._flex["YN_INSP"] = "Y";
                    this._flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));
                    this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                    this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));
                    this._flex["CD_PJT"] = row["CD_PJT"];
                    this._flex["CD_PURGRP"] = row["CD_PURGRP"];
                    this._flex["NO_PO"] = row["NO_PO"];
                    this._flex["NO_POLINE"] = row["NO_LINE"];
                    this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(row["JAN_AM"]));
                    this._flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(row["VAT"].ToString()));
                    this._flex["QT_GR_MM"] = 0;
                    this._flex["RT_CUSTOMS"] = 0;
                    this._flex["YN_RETURN"] = row["YN_RETURN"];
                    this._header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];
                    this._flex["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    this._flex["YN_PURCHASE"] = row["FG_PURCHASE"].ToString() != "" ? "Y" : "N";
                    this._flex["FG_POST"] = row["FG_POST"];
                    this._flex["NM_FG_POST"] = row["NM_FG_POST"];
                    this._flex["FG_RCV"] = row["FG_RCV"];
                    this._flex["NM_FG_RCV"] = row["NM_QTIOTP"];
                    this._flex["FG_TRANS"] = row["FG_TRANS"];
                    this._flex["FG_TAX"] = D.GetString(row["FG_TAX"]);
                    this._flex["CD_EXCH"] = row["CD_EXCH"];
                    this._header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];
                    this._flex["RT_EXCH"] = !(D.GetDecimal(row["RT_EXCH"]) == 0M) || !(D.GetString(row["CD_EXCH"]) == "000") ? D.GetDecimal(row["RT_EXCH"]) : 1;
                    this._flex["NO_IO_MGMT"] = "";
                    this._flex["NO_IOLINE_MGMT"] = 0;
                    this._flex["NO_PO_MGMT"] = "";
                    this._flex["NO_POLINE_MGMT"] = 0;
                    this._flex["NO_TO"] = "";
                    this._flex["NO_TO_LINE"] = 0;
                    this._flex["CD_SL"] = row["CD_SL"];
                    this._flex["NM_SL"] = row["NM_SL"];
                    this._flex["NO_LC"] = "";
                    this._flex["NO_LCLINE"] = 0;
                    this._flex["FG_TAXP"] = row["FG_TAXP"];
                    this._flex["NO_EMP"] = D.GetString(this.tb_NO_EMP.CodeValue);
                    this._flex["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flex["YN_AM"] = row["YN_AM"];
                    this._header.CurrentRow["YN_AM"] = row["YN_AM"];
                    this._flex["VAT_CLS"] = 0;
                    this._flex["YN_AUTORCV"] = row["YN_AUTORCV"];
                    this._flex["YN_REQ"] = row["YN_RCV"];
                    this._flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));
                    this._flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(row["JAN_AM"]));
                    this._flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(row["JAN_AM"]) + this._flex.CDecimal(row["VAT"]));
                    this._flex["AM_EXRCV"] = 0;
                    this._flex["AM_RCV"] = 0;
                    this._flex["NM_SYSDEF"] = row["NM_SYSDEF"];
                    this._flex["NM_KOR"] = row["NM_KOR"];
                    this._flex["YN_LOT"] = row["NO_LOT"];
                    this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"].ToString();
                    if (row["CD_EXCH"].ToString() != "")
                        this.tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    this._flex["TP_UM_TAX"] = row["TP_UM_TAX"];
                    this._flex["PO_PRICE"] = row["PO_PRICE"];
                    this._flex["NM_PURGRP"] = row["NM_PURGRP"];
                    this._flex["NO_SERL"] = row["NO_SERL"];
                    this._flex["DC_RMK"] = row["DC1"];
                    if (BASIC.GetMAEXC("구매입고처리-업체별프로세스선택") == "100")
                        this._flex["DC_RMK2"] = D.GetString(this.txt관리번호.Text);
                    else if (Global.MainFrame.ServerKeyCommon != "SHINKI")
                        this._flex["DC_RMK2"] = row["DC2"];
                    this._flex["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));
                    this._flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_EX"]));
                    this._flex["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(row["JAN_AM"]));
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flex["NO_WBS"] = row["NO_WBS"];
                        this._flex["NO_CBS"] = row["NO_CBS"];
                        this._flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                        this._flex["NO_PJT_DESIGN"] = row["NO_PJT_DESIGN"];
                    }
                    if (pdt_Line.Columns.Contains("CD_ITEM_ORIGIN"))
                    {
                        this._flex["CD_ITEM_ORIGIN"] = row["CD_ITEM_ORIGIN"];
                        this._flex["NM_ITEM_ORIGIN"] = row["NM_ITEM_ORIGIN"];
                        this._flex["STND_ITEM_ORIGIN"] = row["STND_ITEM_ORIGIN"];
                    }
                    this._flex["CD_PARTNER"] = D.GetString(row["CD_PARTNER"]);
                    this._flex["DT_REQ"] = D.GetString(this._header.CurrentRow["DT_REQ"]);
                    this._flex["GI_PARTNER"] = D.GetString(row["GI_PARTNER"]);
                    this._flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                    this._flex["PI_PARTNER"] = D.GetString(row["PI_PARTNER"]);
                    this._flex["PI_LN_PARTNER"] = row["PI_LN_PARTNER"];
                    if (this.MainFrameInterface.ServerKeyCommon == "WOORIERP")
                    {
                        this._flex["NM_USERDEF1"] = row["NM_USERDEF1"];
                        this._flex["NM_USERDEF2"] = row["NM_USERDEF2"];
                    }
                    NO_PO_MULTI = NO_PO_MULTI + D.GetString(row["NO_PO"]) + D.GetString(row["NO_LINE"]) + "|";
                    if (pdt_Line.Columns.Contains("DT_PLAN"))
                        this._flex["DT_PLAN"] = D.GetString(row["DT_PLAN"]);
                    this._flex["CLS_ITEM"] = row["CLS_ITEM"];
                    if (this.MainFrameInterface.ServerKeyCommon == "ICDERPU")
                    {
                        this._flex["DATE_USERDEF1"] = row["DATE_USERDEF1"];
                        this._flex["DC_RMK2"] = row["NM_USERDEF2"];
                    }
                    if (this.MainFrameInterface.ServerKeyCommon == "WINPLUS")
                    {
                        this._flex["NUM_USERDEF1"] = row["NUM_USERDEF1"];
                        this._flex["NUM_USERDEF2"] = row["NUM_USERDEF2"];
                    }
                    if (this.bStandard)
                    {
                        this._flex["UM_WEIGHT"] = row["UM_WEIGHT"];
                        this._flex["TOT_WEIGHT"] = row["TOT_WEIGHT"];
                        this._flex["WEIGHT"] = row["WEIGHT"];
                    }
                    if (this.MainFrameInterface.ServerKeyCommon == "UNIPOINT")
                    {
                        this._flex["CD_PARTNER_PJT"] = row["CD_PARTNER_PJT"];
                        this._flex["LN_PARTNER_PJT"] = row["LN_PARTNER_PJT"];
                        this._flex["NO_EMP_PJT"] = row["NO_EMP_PJT"];
                        this._flex["NM_KOR_PJT"] = row["NM_KOR_PJT"];
                        this._flex["END_USER"] = row["END_USER"];
                    }
                    this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                    this._flex["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                    this._flex["NO_DESIGN"] = row["NO_DESIGN"];
                    this._flex["NM_TPPO"] = row["NM_TPPO"];
                    if (flag)
                        this._flex["EN_ITEM"] = row["EN_ITEM"];
                    if (App.SystemEnv.PMS사용)
                    {
                        this._flex["CD_CSTR"] = row["CD_CSTR"];
                        this._flex["DL_CSTR"] = row["DL_CSTR"];
                        this._flex["NM_CSTR"] = row["NM_CSTR"];
                        this._flex["SIZE_CSTR"] = row["SIZE_CSTR"];
                        this._flex["UNIT_CSTR"] = row["UNIT_CSTR"];
                        this._flex["QTY_ACT"] = row["QTY_ACT"];
                        this._flex["UNT_ACT"] = row["UNT_ACT"];
                        this._flex["AMT_ACT"] = row["AMT_ACT"];
                    }
                    if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_REQ_REG_1")
                    {
                        this._flex["NUM_STND_ITEM_1"] = row["NUM_STND_ITEM_1"];
                        this._flex["NUM_STND_ITEM_2"] = row["NUM_STND_ITEM_2"];
                        this._flex["NUM_STND_ITEM_3"] = row["NUM_STND_ITEM_3"];
                        this._flex["NUM_STND_ITEM_4"] = row["NUM_STND_ITEM_4"];
                        this._flex["NUM_STND_ITEM_5"] = row["NUM_STND_ITEM_5"];
                        this._flex["CLS_L"] = row["CLS_L"];
                        this._flex["NM_CLS_L"] = row["NM_CLS_L"];
                        this._flex["UM_WEIGHT"] = row["RT_PO"];
                    }
                    if (this.MainFrameInterface.ServerKeyCommon == "KSCC")
                    {
                        this._flex["CD_BUDGET"] = row["CD_BUDGET"];
                        this._flex["NM_BUDGET"] = row["NM_BUDGET"];
                        this._flex["CD_BGACCT"] = row["CD_BGACCT"];
                        this._flex["NM_BGACCT"] = row["NM_BGACCT"];
                        this._flex["CD_BIZPLAN"] = row["CD_BIZPLAN"];
                        this._flex["NM_BIZPLAN"] = row["NM_BIZPLAN"];
                        this._flex["CD_ACCT"] = row["CD_ACCT"];
                        this._flex["NM_ACCT"] = row["NM_ACCT"];
                        this._flex["NM_ACCT"] = row["NM_ACCT"];
                        this._flex["NM_KOR_PR"] = row["REQ_NM_KOR"];
                        this._flex["CD_USERDEF1_RCV"] = row["CD_USERDEF1_PO"];
                        this._flex["CD_USERDEF2_RCV"] = row["CD_USERDEF2_PO"];
                        this._flex["NM_USERDEF1_RCV"] = row["NM_USERDEF1"];
                        this._flex["NM_USERDEF2_RCV"] = row["NM_USERDEF2"];
                    }
                    if (Global.MainFrame.ServerKeyCommon.Contains("HIOKI"))
                    {
                        this._flex["NM_USERDEF1_RCV"] = row["CD_USERDEF1_PO"];
                        this._flex["NM_USERDEF2_RCV"] = row["CD_USERDEF2_PO"];
                        this._flex["NM_USERDEF3_IO"] = row["NM_USERDEF3_PO"];
                        this._flex["CD_USERDEF1_RCV"] = row["DC50_PO"];
                        this._flex["CD_USERDEF2_RCV"] = row["NM_USERDEF1_PO"];
                        this._flex["CD_USERDEF3_IO"] = row["NM_USERDEF2_PO"];
                        this._flex["CD_USERDEF4_IO"] = row["TXT_USERDEF1_PO"];
                        this._flex["CD_USERDEF5_IO"] = row["TXT_USERDEF2_PO"];
                        this._flex["CD_USERDEF6_IO"] = row["DC4"];
                        this._flex["TXT_USERDEF1_IO"] = row["NM_USERDEF4_PO"];
                        this._flex["DC_RMK2_IO"] = row["NM_USERDEF5_PO"];
                    }
                    if (Global.MainFrame.ServerKeyCommon.Contains("CNCINTER"))
                        this._flex["CD_USERDEF2_PI"] = row["CD_USERDEF2_PI"];
                    if (!this.bStandard)
                    {
                        this._flex["WEIGHT"] = row["WEIGHT"];
                        this._flex["QT_WEIGHT"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]) * D.GetDecimal(row["WEIGHT"]));
                    }
                    this._flex["GRP_ITEM"] = row["GRP_ITEM"];
                    this._flex["NM_ITEMGRP"] = row["NM_ITEMGRP"];
                    this._flex["TP_ITEM"] = row["TP_ITEM"];
                    this._flex["NM_TP_ITEM"] = row["NM_TP_ITEM"];
                    this._flex["DC1_POL"] = row["DC1"];
                    this._flex["NM_MAKER"] = row["NM_MAKER"];
                    Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, row, this._flex, this._flex.Row);
                    if (MA.ServerKey(false, "FDAMK"))
                        this.setCalBoxEa_FDAMK("QT_REQ_MM", this._flex.Row);
                }
                this._header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                this.tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                this._flex.Redraw = true;
                if (this.m_YN_SU == "100")
                    this.SET_FLEXD(NO_PO_MULTI);
                if (Global.MainFrame.ServerKeyCommon == "WONIL")
                    this.tb_DC.Text = D.GetString(pdt_Line.Rows[0]["DC50_PO_H"]);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 통관적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Cheak_For_btn())
                    return;
                P_TR_REQLC_SUB pTrReqlcSub = new P_TR_REQLC_SUB(D.GetString(this.cbo공장.SelectedValue), this.cbo공장.Text, this.tb_NM_PARTNER.CodeValue.ToString(), this.tb_NM_PARTNER.CodeName, this.cbo_TRANS.SelectedValue.ToString());
                if (((Form)pTrReqlcSub).ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    this.InserGridtAdd_TO(pTrReqlcSub.통관적용dt, pTrReqlcSub.check_app);
                    this.ToolBarDeleteButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InserGridtAdd_TO(DataTable pdt_Line, bool check)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                this._flex.Redraw = false;
                string NO_PO_MULTI = string.Empty;
                DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_TR_REQLC_SUB");
                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    DataRow row = pdt_Line.Rows[index];
                    ++maxValue;
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex[this._flex.Row, "NO_RCV"] = this.tb_NoIsuRcv.Text;
                    this._flex[this._flex.Row, "NO_LINE"] = maxValue;
                    this._flex[this._flex.Row, "NO_IOLINE"] = maxValue;
                    this._flex[this._flex.Row, "YN_LOT"] = row["NO_LOT"].ToString();
                    this._flex[this._flex.Row, "CD_ITEM"] = row["CD_ITEM"].ToString();
                    this._flex[this._flex.Row, "NM_ITEM"] = row["NM_ITEM"].ToString();
                    this._flex[this._flex.Row, "STND_ITEM"] = row["STND_ITEM"].ToString();
                    if (row["CD_UNIT_MM"].ToString() == null)
                        row["CD_UNIT_MM"] = "";
                    this._flex[this._flex.Row, "CD_UNIT_MM"] = row["CD_UNIT_MM"].ToString();
                    this._flex[this._flex.Row, "UNIT_IM"] = row["UNIT_IM"].ToString();
                    if (D.GetDecimal(row["RATE_EXCHG"]) == 0M)
                        this._flex[this._flex.Row, "RATE_EXCHG"] = "1";
                    else
                        this._flex[this._flex.Row, "RATE_EXCHG"] = row["RATE_EXCHG"];
                    this._flex["DT_LIMIT"] = !(BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100") ? row["DT_TO"] : row["DT_LIMIT"];
                    if (row["CD_ZONE"] == null)
                        row["CD_ZONE"] = "";
                    this._flex["CD_ZONE"] = row["CD_ZONE"];
                    this._flex[this._flex.Row, "YN_INSP"] = "N";
                    if (row["FG_IQC"].ToString() != "")
                    {
                        if (row["FG_IQC"].ToString() == "Y")
                            this._flex[this._flex.Row, "YN_INSP"] = "Y";
                        else
                            this._flex[this._flex.Row, "YN_INSP"] = "N";
                    }
                    this._flex[this._flex.Row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flex[this._flex.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flex[this._flex.Row, "QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));
                    this._flex[this._flex.Row, "QT_PASS"] = 0;
                    this._flex[this._flex.Row, "QT_REJECTION"] = 0;
                    this._flex[this._flex.Row, "QT_GR"] = 0;
                    this._flex[this._flex.Row, "QT_GR_MM"] = 0;
                    this._flex[this._flex.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));
                    this._flex[this._flex.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                    this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));
                    this._flex[this._flex.Row, "CD_PJT"] = row["CD_PJT"].ToString();
                    this._flex[this._flex.Row, "CD_PURGRP"] = row["CD_PURGRP"].ToString();
                    this._flex[this._flex.Row, "NO_PO"] = "";
                    this._flex[this._flex.Row, "NO_POLINE"] = 0;
                    this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_TO"]));
                    this._flex[this._flex.Row, "UM"] = (this._flex.CDecimal(row["QT_REQ"]) == 0M ? 0M : Unit.원화단가(DataDictionaryTypes.PU, this._flex.CDecimal(row["AM_TO"]) / this._flex.CDecimal(row["QT_REQ"])));
                    this._flex[this._flex.Row, "VAT"] = 0;
                    this._flex[this._flex.Row, "RT_CUSTOMS"] = 0;
                    this._flex[this._flex.Row, "YN_RETURN"] = "N";
                    this._header.CurrentRow["YN_RETURN"] = "N";
                    this._flex[this._flex.Row, "FG_TPPURCHASE"] = row["FG_TPPURCHASE"].ToString();
                    if (row["FG_TPPURCHASE"].ToString() != string.Empty)
                        this._flex[this._flex.Row, "YN_PURCHASE"] = "Y";
                    else
                        this._flex[this._flex.Row, "YN_PURCHASE"] = "N";
                    this._flex[this._flex.Row, "FG_POST"] = "";
                    this._flex[this._flex.Row, "NM_FG_POST"] = "";
                    this._flex[this._flex.Row, "FG_RCV"] = row["CD_QTIOTP"].ToString();
                    this._flex[this._flex.Row, "NM_FG_RCV"] = row["NM_QTIOTP"].ToString();
                    this._flex[this._flex.Row, "FG_TRANS"] = row["FG_LC"].ToString();
                    this._flex[this._flex.Row, "FG_TAX"] = "23";
                    this._flex[this._flex.Row, "CD_EXCH"] = row["CD_EXCH"].ToString();
                    this._header.CurrentRow["CD_EXCH"] = row["CD_EXCH"].ToString();
                    this._flex[this._flex.Row, "RT_EXCH"] = row["RT_EXCH_BL"].ToString();
                    this._flex[this._flex.Row, "NO_SERL"] = row["NO_SERL"];
                    this._flex[this._flex.Row, "NO_IO_MGMT"] = "";
                    this._flex[this._flex.Row, "NO_IOLINE_MGMT"] = 0;
                    this._flex[this._flex.Row, "NO_PO"] = row["NO_PO"].ToString();
                    this._flex[this._flex.Row, "NO_POLINE"] = row["NO_POLINE"].ToString();
                    this._flex[this._flex.Row, "NO_TO"] = row["NO_TO"].ToString();
                    this._flex[this._flex.Row, "NO_TO_LINE"] = row["NO_LINE"].ToString();
                    this._flex[this._flex.Row, "NO_LC"] = row["NO_LC"].ToString();
                    this._flex[this._flex.Row, "NO_LCLINE"] = row["NO_LCLINE"].ToString();
                    this._flex[this._flex.Row, "CD_SL"] = row["CD_SL"].ToString();
                    this._flex[this._flex.Row, "NM_SL"] = row["NM_SL"].ToString();
                    this._flex[this._flex.Row, "FG_TAXP"] = "001";
                    this._flex[this._flex.Row, "NO_EMP"] = D.GetString(this.tb_NO_EMP.CodeValue);
                    this._flex[this._flex.Row, "NM_PROJECT"] = row["NM_PROJECT"].ToString();
                    this._flex[this._flex.Row, "YN_AM"] = this._flex[this._flex.Row, "YN_PURCHASE"].ToString();
                    this._header.CurrentRow["YN_AM"] = this._flex[this._flex.Row, "YN_PURCHASE"].ToString();
                    this._flex[this._flex.Row, "VAT_CLS"] = 0;
                    this._flex[this._flex.Row, "YN_AUTORCV"] = row["YN_AUTORCV"].ToString();
                    this._flex[this._flex.Row, "YN_REQ"] = "Y";
                    this._flex[this._flex.Row, "AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX_TO"].ToString()));
                    this._flex[this._flex.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_TO"].ToString()));
                    this._flex[this._flex.Row, "AM_EXRCV"] = 0;
                    this._flex[this._flex.Row, "AM_RCV"] = 0;
                    this._flex[this._flex.Row, "NO_BL"] = row["NO_BL"];
                    this._flex[this._flex.Row, "NO_BLLINE"] = row["NO_BLLINE"];
                    this._flex[this._flex.Row, "NM_SYSDEF"] = row["NM_SYSDEF"].ToString();
                    this._flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[this._flex.Row, "AM_REQ"]) + this._flex.CDecimal(this._flex[this._flex.Row, "VAT"]));
                    if (row["CD_EXCH"].ToString() != "")
                        this.tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    if (this._flex[this._flex.Row, "RT_SPEC"] == DBNull.Value)
                    {
                        if (this._flex.DataTable.Columns["RT_SPEC"].DataType.FullName == "System.String")
                            this._flex[this._flex.Row, "RT_SPEC"] = string.Empty;
                        else
                            this._flex[this._flex.Row, "RT_SPEC"] = 0;
                    }
                    if (this._flex[this._flex.Row, "DC_RMK"] == DBNull.Value)
                        this._flex[this._flex.Row, "DC_RMK"] = string.Empty;
                    if (Global.MainFrame.ServerKeyCommon.Contains("MICHANG"))
                        this._flex[this._flex.Row, "DC_RMK"] = D.GetString(row["REMARK"]);
                    this._flex[this._flex.Row, "CD_PLANT"] = this._header.CurrentRow["CD_PLANT"].ToString();
                    this._flex[this._flex.Row, "REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flex[this._flex.Row, "REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX_TO"].ToString()));
                    this._flex[this._flex.Row, "REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_TO"].ToString()));
                    this._flex[this._flex.Row, "TP_UM_TAX"] = row["TP_UM_TAX"];
                    this._flex["GI_PARTNER"] = row["GI_PARTNER"];
                    this._flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flex[this._flex.Row, "CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flex[this._flex.Row, "NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flex[this._flex.Row, "PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flex[this._flex.Row, "SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                        this._flex[this._flex.Row, "NO_PJT_DESIGN"] = row["NO_PJT_DESIGN"];
                        this._flex[this._flex.Row, "NO_CBS"] = row["NO_CBS"];
                        this._flex[this._flex.Row, "NO_WBS"] = row["NO_WBS"];
                    }
                    this._flex["NO_SERL"] = row["NO_SERL"];
                    if (pdt_Line.Columns.Contains("DT_PLAN"))
                        this._flex["DT_PLAN"] = row["DT_PLAN"];
                    this._flex["CLS_ITEM"] = row["CLS_ITEM"];
                    NO_PO_MULTI = NO_PO_MULTI + D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";
                    if (check)
                    {
                        this._flex[this._flex.Row, "DC_RMK"] = row["DC1"].ToString();
                        this._flex[this._flex.Row, "DC_RMK2"] = row["DC2"].ToString();
                    }
                    this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                    this._flex["NO_DESIGN"] = row["NO_DESIGN"];
                    this._flex["NM_TPPO"] = row["NM_TPPO"];
                    this._flex["NM_MAKER"] = row["NM_MAKER"];
                    if (App.SystemEnv.PMS사용)
                    {
                        this._flex["CD_CSTR"] = row["CD_CSTR"];
                        this._flex["DL_CSTR"] = row["DL_CSTR"];
                        this._flex["NM_CSTR"] = row["NM_CSTR"];
                        this._flex["SIZE_CSTR"] = row["SIZE_CSTR"];
                        this._flex["UNIT_CSTR"] = row["UNIT_CSTR"];
                        this._flex["QTY_ACT"] = row["QTY_ACT"];
                        this._flex["UNT_ACT"] = row["UNT_ACT"];
                        this._flex["AMT_ACT"] = row["AMT_ACT"];
                    }
                    if (!this.bStandard)
                    {
                        this._flex["WEIGHT"] = row["WEIGHT"];
                        this._flex["QT_WEIGHT"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]) * D.GetDecimal(row["WEIGHT"]));
                    }
                    if (Global.MainFrame.ServerKeyCommon.Contains("HIOKI"))
                    {
                        this._flex["NM_USERDEF1_RCV"] = row["POL_CD_USERDEF1"];
                        this._flex["NM_USERDEF2_RCV"] = row["POL_CD_USERDEF2"];
                        this._flex["NM_USERDEF3_IO"] = row["POL_NM_USERDEF3"];
                        this._flex["CD_USERDEF1_RCV"] = row["DC50_PO"];
                        this._flex["CD_USERDEF2_RCV"] = row["POL_NM_USERDEF1"];
                        this._flex["CD_USERDEF3_IO"] = row["POL_NM_USERDEF2"];
                        this._flex["CD_USERDEF4_IO"] = row["POL_TXT_USERDEF1"];
                        this._flex["CD_USERDEF5_IO"] = row["POL_TXT_USERDEF2"];
                        this._flex["CD_USERDEF6_IO"] = row["DC4"];
                        this._flex["TXT_USERDEF1_IO"] = row["POL_NM_USERDEF4"];
                        this._flex["DC_RMK2_IO"] = row["POL_NM_USERDEF5"];
                    }
                    if (Global.MainFrame.ServerKeyCommon.Contains("CNCINTER"))
                        this._flex["CD_USERDEF2_PI"] = row["CD_USERDEF2_PI"];
                    this._flex["GRP_ITEM"] = row["GRP_ITEM"];
                    this._flex["NM_ITEMGRP"] = row["NM_ITEMGRP"];
                    this._flex["TP_ITEM"] = row["TP_ITEM"];
                    this._flex["NM_TP_ITEM"] = row["NM_TP_ITEM"];
                    this._flex["DC1_POL"] = row["DC1"];
                    this._flex["CD_PARTNER"] = D.GetString(row["CD_PARTNER"]);
                    Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, row, this._flex, this._flex.Row);
                }
                this._header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                this.tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                this._flex.Redraw = true;
                if (this.m_YN_SU == "100")
                    this.SET_FLEXD(NO_PO_MULTI);
                this.ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Local_LC_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Cheak_For_btn())
                    return;
                P_PU_REQLC_SUB pPuReqlcSub = new P_PU_REQLC_SUB(this.MainFrameInterface, (DataTable)null, D.GetString(this.cbo공장.SelectedValue));
                pPuReqlcSub.g_cdPartner = this.tb_NM_PARTNER.CodeValue.ToString();
                pPuReqlcSub.g_nmPartner = this.tb_NM_PARTNER.CodeName;
                if (pPuReqlcSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                    this.InserGridtAddLC(pPuReqlcSub.gdt_return);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InserGridtAddLC(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                this._flex.Redraw = false;
                string NO_PO_MULTI = string.Empty;
                DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PU_REQLC_SUB");
                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    DataRow row = pdt_Line.Rows[index];
                    ++maxValue;
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex[this._flex.Row, "NO_LINE"] = maxValue;
                    this._flex[this._flex.Row, "NO_IOLINE"] = maxValue;
                    this._flex[this._flex.Row, "YN_LOT"] = row["NO_LOT"].ToString();
                    this._flex[this._flex.Row, "CD_ITEM"] = row["CD_ITEM"].ToString();
                    this._flex[this._flex.Row, "NM_ITEM"] = row["NM_ITEM"].ToString();
                    this._flex[this._flex.Row, "STND_ITEM"] = row["STND_ITEM"].ToString();
                    if (row["CD_UNIT_MM"].ToString() == null)
                        row["CD_UNIT_MM"] = "";
                    this._flex[this._flex.Row, "CD_UNIT_MM"] = row["CD_UNIT_MM"].ToString();
                    this._flex[this._flex.Row, "UNIT_IM"] = row["UNIT_IM"].ToString();
                    this._flex[this._flex.Row, "RATE_EXCHG"] = row["RATE_EXCHG"];
                    this._flex["DT_LIMIT"] = !(BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100") ? row["DT_DELIVERY"] : row["DT_LIMIT"];
                    this._flex[this._flex.Row, "QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]));
                    this._flex[this._flex.Row, "RT_VAT"] = "0";
                    this._flex[this._flex.Row, "QT_REAL"] = 0;
                    this._flex[this._flex.Row, "YN_INSP"] = "N";
                    if (row["FG_IQC"].ToString() != "" && row["FG_IQC"].ToString() == "Y")
                        this._flex[this._flex.Row, "YN_INSP"] = "Y";
                    this._flex[this._flex.Row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flex[this._flex.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ"]));
                    this._flex[this._flex.Row, "QT_PASS"] = 0;
                    this._flex[this._flex.Row, "QT_REJECTION"] = 0;
                    this._flex[this._flex.Row, "QT_GR"] = 0;
                    this._flex[this._flex.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));
                    this._flex[this._flex.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                    this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));
                    this._flex[this._flex.Row, "CD_PJT"] = row["CD_PJT"].ToString();
                    this._flex[this._flex.Row, "CD_PURGRP"] = row["CD_PURGRP"].ToString();
                    this._flex[this._flex.Row, "NO_PO"] = row["NO_PO"].ToString();
                    this._flex[this._flex.Row, "NO_POLINE"] = row["NO_LINE_PO"];
                    this._flex[this._flex.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM"]));
                    this._flex[this._flex.Row, "VAT"] = 0;
                    this._flex[this._flex.Row, "QT_GR_MM"] = 0;
                    this._flex[this._flex.Row, "RT_CUSTOMS"] = 0;
                    this._flex[this._flex.Row, "YN_RETURN"] = "N";
                    this._header.CurrentRow["YN_RETURN"] = "N";
                    this._flex[this._flex.Row, "FG_TPPURCHASE"] = row["FG_TPPURCHASE"].ToString();
                    if (row["FG_TPPURCHASE"].ToString() != string.Empty)
                        this._flex[this._flex.Row, "YN_PURCHASE"] = "Y";
                    else
                        this._flex[this._flex.Row, "YN_PURCHASE"] = "N";
                    this._flex[this._flex.Row, "FG_RCV"] = row["CD_QTIOTP"].ToString();
                    this._flex[this._flex.Row, "NM_FG_RCV"] = row["NM_QTIOTP"].ToString();
                    this._flex[this._flex.Row, "FG_TRANS"] = row["FG_LC"].ToString();
                    this._flex[this._flex.Row, "FG_TAX"] = "23";
                    this._flex[this._flex.Row, "CD_EXCH"] = row["CD_EXCH"].ToString();
                    this._header.CurrentRow["CD_EXCH"] = row["CD_EXCH"].ToString();
                    if (D.GetDecimal(row["RT_EXCH"]) == 0M && D.GetString(row["CD_EXCH"]) == "000")
                        this._flex[this._flex.Row, "RT_EXCH"] = 1;
                    else
                        this._flex[this._flex.Row, "RT_EXCH"] = D.GetDecimal(row["RT_EXCH"]);
                    this._flex[this._flex.Row, "NO_IO_MGMT"] = "";
                    this._flex[this._flex.Row, "NO_IOLINE_MGMT"] = 0;
                    this._flex[this._flex.Row, "NO_PO_MGMT"] = "";
                    this._flex[this._flex.Row, "NO_POLINE_MGMT"] = 0;
                    this._flex[this._flex.Row, "NO_TO"] = "";
                    this._flex[this._flex.Row, "NO_TO_LINE"] = 0;
                    this._flex[this._flex.Row, "CD_SL"] = row["CD_SL"].ToString();
                    this._flex[this._flex.Row, "NM_SL"] = row["NM_SL"].ToString();
                    this._flex[this._flex.Row, "NO_LC"] = row["NO_LC"].ToString();
                    this._flex[this._flex.Row, "NO_LCLINE"] = row["NO_LINE"];
                    this._flex[this._flex.Row, "FG_TAXP"] = "001";
                    this._flex[this._flex.Row, "NO_EMP"] = D.GetString(this.tb_NO_EMP.CodeValue);
                    this._flex[this._flex.Row, "NM_PROJECT"] = row["NM_PJT"].ToString();
                    this._flex[this._flex.Row, "YN_AM"] = row["YN_AM"].ToString();
                    this._header.CurrentRow["YN_AM"] = row["YN_AM"].ToString();
                    this._flex[this._flex.Row, "VAT_CLS"] = 0;
                    this._flex[this._flex.Row, "YN_AUTORCV"] = row["YN_AUTORCV"].ToString();
                    this._flex[this._flex.Row, "YN_REQ"] = "Y";
                    this._flex[this._flex.Row, "AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));
                    this._flex[this._flex.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(row["AM"]));
                    this._flex[this._flex.Row, "AM_EXRCV"] = 0;
                    this._flex[this._flex.Row, "AM_RCV"] = 0;
                    this._flex[this._flex.Row, "NM_SYSDEF"] = row["NM_SYSDEF"].ToString();
                    this._flex[this._flex.Row, "NM_KOR"] = row["NM_KOR"].ToString();
                    if (row["CD_EXCH"].ToString() != "")
                        this.tb_CD_EXCH.Text = row["NM_SYSDEF"].ToString();
                    this._flex[this._flex.Row, "CD_PLANT"] = this._header.CurrentRow["CD_PLANT"].ToString();
                    this._flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(row["AM"]) + this._flex.CDecimal(this._flex[this._flex.Row, "VAT"]));
                    this._flex["GI_PARTNER"] = row["GI_PARTNER"];
                    this._flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                        this._flex["NO_PJT_DESIGN"] = row["NO_PJT_DESIGN"];
                        this._flex["NO_WBS"] = D.GetDecimal(row["NO_WBS"]);
                        this._flex["NO_CBS"] = row["NO_CBS"];
                    }
                    this._flex[this._flex.Row, "NO_SERL"] = row["NO_SERL"].ToString();
                    if (pdt_Line.Columns.Contains("DT_PLAN"))
                        this._flex["DT_PLAN"] = row["DT_PLAN"];
                    this._flex["CLS_ITEM"] = row["CLS_ITEM"];
                    this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                    this._flex["NO_DESIGN"] = row["NO_DESIGN"];
                    this._flex["NM_TPPO"] = row["NM_TPPO"];
                    NO_PO_MULTI = NO_PO_MULTI + D.GetString(row["NO_PO"]) + D.GetString(row["NO_LINE_PO"]) + "|";
                    if (App.SystemEnv.PMS사용)
                    {
                        this._flex["CD_CSTR"] = row["CD_CSTR"];
                        this._flex["DL_CSTR"] = row["DL_CSTR"];
                        this._flex["NM_CSTR"] = row["NM_CSTR"];
                        this._flex["SIZE_CSTR"] = row["SIZE_CSTR"];
                        this._flex["UNIT_CSTR"] = row["UNIT_CSTR"];
                        this._flex["QTY_ACT"] = row["QTY_ACT"];
                        this._flex["UNT_ACT"] = row["UNT_ACT"];
                        this._flex["AMT_ACT"] = row["AMT_ACT"];
                    }
                    if (!this.bStandard)
                    {
                        this._flex["WEIGHT"] = row["WEIGHT"];
                        this._flex["QT_WEIGHT"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REQ_MM"]) * D.GetDecimal(row["WEIGHT"]));
                    }
                    this._flex["GRP_ITEM"] = row["GRP_ITEM"];
                    this._flex["NM_ITEMGRP"] = row["NM_ITEMGRP"];
                    this._flex["TP_ITEM"] = row["TP_ITEM"];
                    this._flex["NM_TP_ITEM"] = row["NM_TP_ITEM"];
                    this._flex["DC1_POL"] = row["DC1_POL"];
                    this._flex["CD_PARTNER"] = D.GetString(row["CD_PARTNER"]);
                    this._flex["NM_MAKER"] = D.GetString(row["NM_MAKER"]);
                    Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, row, this._flex, this._flex.Row);
                }
                this._header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                this.tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                if (this.m_YN_SU == "100")
                    this.SET_FLEXD(NO_PO_MULTI);
                this._flex.Redraw = true;
                this.ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void m_btn_Rev_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Cheak_For_btn())
                    return;
                string codeValue = this.tb_NM_PARTNER.CodeValue;
                string codeName = this.tb_NM_PARTNER.CodeName;
                string FG_TRANS = this.cbo_TRANS.SelectedValue.ToString();
                P_PU_REV_SUB pPuRevSub = new P_PU_REV_SUB(D.GetString(this.cbo공장.SelectedValue), codeValue, codeName, this._flex.DataTable, FG_TRANS, D.GetString(this.cbo_PROCESS.SelectedValue));
                this.btn통관적용.Enabled = false;
                if (pPuRevSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    this.cbo_TRANS.Enabled = false;
                    foreach (DataColumn column in this._flex.DataTable.Clone().Columns)
                    {
                        if (column.DataType == typeof(decimal))
                            column.DefaultValue = 0;
                    }
                    this.InserGridtAddREV(pPuRevSub.gdt_return, "REV", false);
                    this.ToolBarDeleteButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InserGridtAddREV(DataTable pdt_Line, string strApp, bool chk_rmk)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                decimal val1 = 1M;
                decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                this._flex.Redraw = false;
                bool flag = pdt_Line.Columns.Contains("JAN_QT_PASS");
                string NO_PO_MULTI = string.Empty;
                string pageIdSub = "P_PU_REQ_IQC_SUB";
                if (strApp == "REV")
                    pageIdSub = "P_PU_REV_SUB";
                DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, pageIdSub);
                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    DataRow row = pdt_Line.Rows[index];
                    ++maxValue;
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex["NO_LINE"] = maxValue;
                    this._flex["NO_IOLINE"] = maxValue;
                    this._flex["CD_ITEM"] = row["CD_ITEM"];
                    this._flex["NM_ITEM"] = row["NM_ITEM"];
                    this._flex["STND_ITEM"] = row["STND_ITEM"];
                    if (row["UNIT_IM"] == null)
                        row["UNIT_IM"] = "";
                    if (D.GetDecimal(row["UNIT_PO_FACT"]) != 0M)
                        val1 = D.GetDecimal(row["UNIT_PO_FACT"]);
                    this._flex["YN_LOT"] = row["NO_LOT"];
                    this._flex["CD_UNIT_MM"] = row["UNIT_PO"];
                    this._flex["UNIT_IM"] = row["UNIT_IM"];
                    this._flex["RATE_EXCHG"] = val1;
                    this._flex["RT_VAT"] = row["RT_VAT"];
                    this._flex["DT_LIMIT"] = !(BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100") ? row["DT_REV"] : row["DT_LIMIT"];
                    this._flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]) / val1);
                    this._flex["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    this._flex["QT_REJECTION"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_BAD"]));
                    this._flex["YN_INSP"] = "N";
                    if (this.m_sEnv_IQC == "N" && D.GetString(row["FG_IQC"]) == "Y" && strApp == "REV")
                        this._flex["YN_INSP"] = "Y";
                    if (Global.MainFrame.ServerKeyCommon == "NOFMCT" && strApp == "IQC")
                        this._flex["YN_INSP"] = "Y";
                    this._flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));
                    this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                    this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["CD_PJT"] = row["CD_PJT"];
                    this._flex["CD_PURGRP"] = row["CD_PURGRP"];
                    this._flex["NO_PO"] = row["NO_PO"];
                    this._flex["NO_POLINE"] = row["NO_POLINE"];
                    this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex.CDecimal(row["JAN_AM"])));
                    this._flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex.CDecimal(row["VAT_REV"].ToString())));
                    this._flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex["AM"]) + this._flex.CDecimal(this._flex["VAT"]));
                    this._flex["QT_GR_MM"] = 0;
                    this._flex["RT_CUSTOMS"] = 0;
                    this._flex["YN_RETURN"] = "N";
                    this._header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];
                    this._flex["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    this._flex["YN_PURCHASE"] = row["YN_PURCHASE"].ToString();
                    this._flex["FG_POST"] = row["FG_POST"];
                    this._flex["FG_RCV"] = row["FG_RCV"];
                    this._flex["FG_TRANS"] = row["FG_TRANS"];
                    this._flex["FG_TAX"] = D.GetString(row["FG_TAX"]);
                    this._flex["CD_EXCH"] = row["CD_EXCH"];
                    this._header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];
                    this._flex["RT_EXCH"] = !(D.GetDecimal(row["RT_EXCH"]) == 0M) || !(D.GetString(row["CD_EXCH"]) == "000") ? D.GetDecimal(row["RT_EXCH"]) : 1;
                    if (row["CD_EXCH"].ToString() != "")
                        this.tb_CD_EXCH.Text = row["NM_EXCH"].ToString();
                    this._flex["NO_IO_MGMT"] = "";
                    this._flex["NO_IOLINE_MGMT"] = 0;
                    this._flex["NO_PO_MGMT"] = "";
                    this._flex["NO_POLINE_MGMT"] = 0;
                    this._flex["NO_TO"] = "";
                    this._flex["NO_TO_LINE"] = 0;
                    this._flex["CD_SL"] = row["CD_SL"];
                    this._flex["NM_SL"] = row["NM_SL"];
                    this._flex["FG_TAXP"] = row["FG_TAXP"];
                    this._flex["NO_EMP"] = D.GetString(this.tb_NO_EMP.CodeValue);
                    this._flex["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flex["YN_AM"] = row["YN_AM"];
                    this._header.CurrentRow["YN_AM"] = row["YN_AM"];
                    this._flex["VAT_CLS"] = 0;
                    this._flex["YN_AUTORCV"] = row["YN_AUTORCV"];
                    this._flex["YN_REQ"] = row["YN_RCV"];
                    this._flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM"]));
                    this._flex["AM_EXRCV"] = 0;
                    this._flex["AM_RCV"] = 0;
                    this._flex["NM_SYSDEF"] = "";
                    this._flex["NO_REV"] = row["NO_REV"];
                    this._flex["NO_REVLINE"] = row["NO_REVLINE"];
                    this._flex["NM_KOR"] = row["NM_KOR"];
                    this._flex["NM_FG_RCV"] = row["NM_FG_RCV"];
                    this._flex["NM_FG_POST"] = row["NM_FG_POST"];
                    this._flex["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"].ToString();
                    this._flex["NO_TO"] = row["NO_TO"].ToString();
                    this._flex["NO_TO_LINE"] = row["NO_TOLINE"];
                    this._flex["CD_ZONE"] = row["CD_ZONE"];
                    if (D.GetString(this.cbo_TRANS.SelectedValue) == "003")
                    {
                        this._flex["NO_LC"] = row["NO_LC_LOCAL"].ToString();
                        this._flex["NO_LCLINE"] = row["NO_LCLINE_LOCAL"];
                    }
                    else
                    {
                        this._flex["NO_LC"] = row["NO_LC"].ToString();
                        this._flex["NO_LCLINE"] = row["NO_LCLINE"];
                    }
                    this._flex["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(row["QT_REV_VAL"]), val1));
                    this._flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex.CDecimal(row["JAN_AM"])));
                    this._flex["REV_QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    this._flex["REV_QT_REV_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flex["NO_WBS"] = row["NO_WBS"];
                        this._flex["NO_CBS"] = row["NO_CBS"];
                        this._flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                        this._flex["NO_PJT_DESIGN"] = row["NO_PJT_DESIGN"];
                    }
                    if (Global.MainFrame.ServerKey.Contains("CNP"))
                        this._flex["PO_PRICE"] = row["PO_PRICE"];
                    if (pdt_Line.Columns.Contains("NM_PURGRP"))
                        this._flex["NM_PURGRP"] = row["NM_PURGRP"];
                    if (this._flex.DataTable.Columns.Contains("GI_PARTNER"))
                    {
                        this._flex["GI_PARTNER"] = row["GI_PARTNER"];
                        this._flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                    }
                    if (this.m_YN_special == "Y" && flag)
                    {
                        this._flex["JAN_QT_PASS"] = row["JAN_QT_PASS"];
                        this._flex["JAN_QT_SPECIAL"] = row["JAN_QT_SPECIAL"];
                        this._flex["FG_SPECIAL"] = row["OB_PUT"];
                    }
                    if (this.MainFrameInterface.ServerKeyCommon == "WOORIERP" || this.MainFrameInterface.ServerKeyCommon == "DZSQL" || this.MainFrameInterface.ServerKeyCommon == "SQL_")
                    {
                        this._flex["NM_USERDEF1"] = row["NM_USERDEF1"];
                        this._flex["NM_USERDEF2"] = row["NM_USERDEF2"];
                    }
                    this._flex["NO_SERL"] = row["NO_SERL"];
                    if (this._flex.DataTable.Columns.Contains("DT_PLAN"))
                        this._flex["DT_PLAN"] = row["DT_PLAN"];
                    this._flex["CLS_ITEM"] = row["CLS_ITEM"];
                    this._flex["TP_UM_TAX"] = row["TP_UM_TAX"];
                    NO_PO_MULTI = NO_PO_MULTI + D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";
                    if (this.MainFrameInterface.ServerKeyCommon == "ICDERPU" || this.MainFrameInterface.ServerKeyCommon == "DZSQL" || this.MainFrameInterface.ServerKeyCommon == "SQL_")
                    {
                        this._flex["DATE_USERDEF1"] = row["DATE_USERDEF1"];
                        this._flex["DC_RMK2"] = row["NM_USERDEF2"];
                    }
                    this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                    this._flex["NO_DESIGN"] = row["NO_DESIGN"];
                    this._flex["NM_TPPO"] = row["NM_TPPO"];
                    if (chk_rmk)
                        this._flex["DC_RMK"] = row["DC_RMK"];
                    if (App.SystemEnv.PMS사용)
                    {
                        this._flex["CD_CSTR"] = row["CD_CSTR"];
                        this._flex["DL_CSTR"] = row["DL_CSTR"];
                        this._flex["NM_CSTR"] = row["NM_CSTR"];
                        this._flex["SIZE_CSTR"] = row["SIZE_CSTR"];
                        this._flex["UNIT_CSTR"] = row["UNIT_CSTR"];
                        this._flex["QTY_ACT"] = row["QTY_ACT"];
                        this._flex["UNT_ACT"] = row["UNT_ACT"];
                        this._flex["AMT_ACT"] = row["AMT_ACT"];
                    }
                    this._flex["NO_LOT"] = row["NO_LOT_REV"];
                    this._flex["DT_LIMIT_LOT"] = row["DT_LIMIT_LOT"];
                    if (!this.bStandard)
                    {
                        this._flex["WEIGHT"] = row["WEIGHT"];
                        this._flex["QT_WEIGHT"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex["QT_REQ_MM"]) * D.GetDecimal(this._flex["WEIGHT"]));
                    }
                    if (Global.MainFrame.ServerKeyCommon == "MAIIM")
                    {
                        this._flex["NO_LOT"] = row["TXT_USERDEF1"];
                        this._flex["NM_USERDEF1_RCV"] = row["TXT_USERDEF2"];
                    }
                    this._flex["GRP_ITEM"] = row["GRP_ITEM"];
                    this._flex["NM_ITEMGRP"] = row["NM_ITEMGRP"];
                    this._flex["TP_ITEM"] = row["TP_ITEM"];
                    this._flex["NM_TP_ITEM"] = row["NM_TP_ITEM"];
                    this._flex["DC1_POL"] = row["DC1_POL"];
                    this._flex["CD_PARTNER"] = D.GetString(row["CD_PARTNER"]);
                    this._flex["NM_MAKER"] = D.GetString(row["NM_MAKER"]);
                    if (MA.ServerKey(false, "THV"))
                    {
                        this._flex["CD_THV1"] = (D.GetString(row["NO_REV"]) + "-" + D.GetString(row["NO_REVLINE"]));
                        this._flex["CD_THV2"] = row["DC_RMK"];
                        this._flex["CD_THV3"] = row["DT_LIMIT_LOT"];
                        this._flex["CD_THV4"] = row["DC_RMK2"];
                    }
                    Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, row, this._flex, this._flex.Row);
                }
                this._header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                this.tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                this._flex.Redraw = true;
                if (this.m_YN_SU == "100")
                    this.SET_FLEXD(NO_PO_MULTI);
                this.ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InsertGrid_REV_BARCODE(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                decimal val1 = 1M;
                decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                this._flex.Redraw = false;
                bool flag = pdt_Line.Columns.Contains("JAN_QT_PASS");
                string NO_PO_MULTI = string.Empty;
                if (Global.MainFrame.ServerKeyCommon != "TELCON")
                {
                    this.tb_DT_RCV.Text = D.GetString(pdt_Line.Rows[0]["DT_REV"]);
                    this._header.CurrentRow["DT_REQ"] = this.tb_DT_RCV.Text;
                }
                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    DataRow row = pdt_Line.Rows[index];
                    ++maxValue;
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex["NO_LINE"] = maxValue;
                    this._flex["NO_IOLINE"] = maxValue;
                    this._flex["CD_ITEM"] = row["CD_ITEM"];
                    this._flex["NM_ITEM"] = row["NM_ITEM"];
                    this._flex["STND_ITEM"] = row["STND_ITEM"];
                    if (row["UNIT_IM"] == null)
                        row["UNIT_IM"] = "";
                    if (D.GetDecimal(row["UNIT_PO_FACT"]) != 0M)
                        val1 = D.GetDecimal(row["UNIT_PO_FACT"]);
                    this._flex["YN_LOT"] = row["NO_LOT"];
                    this._flex["CD_UNIT_MM"] = row["UNIT_PO"];
                    this._flex["UNIT_IM"] = row["UNIT_IM"];
                    this._flex["RATE_EXCHG"] = val1;
                    this._flex["RT_VAT"] = row["RT_VAT"];
                    this._flex["DT_LIMIT"] = !(BASIC.GetMAEXC_Menu("P_PU_REQ_REG_1", "PU_A00000007") == "100") ? row["DT_REV"] : row["DT_LIMIT"];
                    this._flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]) / val1);
                    this._flex["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    this._flex["QT_REJECTION"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_BAD"]));
                    this._flex["YN_INSP"] = "N";
                    this._flex["YN_INSP"] = "N";
                    this._flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));
                    this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                    this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["CD_PJT"] = row["CD_PJT"];
                    this._flex["CD_PURGRP"] = row["CD_PURGRP"];
                    this._flex["NO_PO"] = row["NO_PO"];
                    this._flex["NO_POLINE"] = row["NO_POLINE"];
                    this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex.CDecimal(row["JAN_AM"])));
                    this._flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex.CDecimal(row["VAT_REV"].ToString())));
                    this._flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex["AM"]) + this._flex.CDecimal(this._flex["VAT"]));
                    this._flex["QT_GR_MM"] = 0;
                    this._flex["RT_CUSTOMS"] = 0;
                    this._flex["YN_RETURN"] = "N";
                    this._header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];
                    this._flex["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    this._flex["YN_PURCHASE"] = row["YN_PURCHASE"].ToString();
                    this._flex["FG_POST"] = row["FG_POST"];
                    this._flex["FG_RCV"] = row["FG_RCV"];
                    this._flex["FG_TRANS"] = row["FG_TRANS"];
                    this._flex["FG_TAX"] = D.GetString(row["FG_TAX"]);
                    this._flex["CD_EXCH"] = row["CD_EXCH"];
                    this._header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];
                    this._flex["RT_EXCH"] = !(D.GetDecimal(row["RT_EXCH"]) == 0M) || !(D.GetString(row["CD_EXCH"]) == "000") ? D.GetDecimal(row["RT_EXCH"]) : 1;
                    this._flex["NO_IO_MGMT"] = "";
                    this._flex["NO_IOLINE_MGMT"] = 0;
                    this._flex["NO_PO_MGMT"] = "";
                    this._flex["NO_POLINE_MGMT"] = 0;
                    this._flex["NO_TO"] = "";
                    this._flex["NO_TO_LINE"] = 0;
                    this._flex["CD_SL"] = row["CD_SL"];
                    this._flex["NM_SL"] = row["NM_SL"];
                    this._flex["FG_TAXP"] = row["FG_TAXP"];
                    this._flex["NO_EMP"] = D.GetString(this.tb_NO_EMP.CodeValue);
                    this._flex["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flex["YN_AM"] = row["YN_AM"];
                    this._header.CurrentRow["YN_AM"] = row["YN_AM"];
                    this._flex["VAT_CLS"] = 0;
                    this._flex["YN_AUTORCV"] = row["YN_AUTORCV"];
                    this._flex["YN_REQ"] = row["YN_RCV"];
                    this._flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM"]));
                    this._flex["AM_EXRCV"] = 0;
                    this._flex["AM_RCV"] = 0;
                    this._flex["NM_SYSDEF"] = "";
                    this._flex["NO_REV"] = row["NO_REV"];
                    this._flex["NO_REVLINE"] = row["NO_REVLINE"];
                    this._flex["NM_KOR"] = row["NM_KOR"];
                    this._flex["NM_FG_RCV"] = row["NM_FG_RCV"];
                    this._flex["NM_FG_POST"] = row["NM_FG_POST"];
                    this._flex["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flex["CD_PLANT"] = row["CD_PLANT"];
                    this._header.CurrentRow["CD_PLANT"] = row["CD_PLANT"];
                    this.cbo공장.SelectedValue = row["CD_PLANT"];
                    this._flex["NO_TO"] = row["NO_TO"].ToString();
                    this._flex["NO_TO_LINE"] = row["NO_TOLINE"];
                    this._flex["CD_ZONE"] = row["CD_ZONE"];
                    if (D.GetString(this.cbo_TRANS.SelectedValue) == "003")
                    {
                        this._flex["NO_LC"] = row["NO_LC_LOCAL"].ToString();
                        this._flex["NO_LCLINE"] = row["NO_LCLINE_LOCAL"];
                    }
                    else
                    {
                        this._flex["NO_LC"] = row["NO_LC"].ToString();
                        this._flex["NO_LCLINE"] = row["NO_LCLINE"];
                    }
                    this._flex["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(row["QT_REV_VAL"]), val1));
                    this._flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex.CDecimal(row["JAN_AM"])));
                    this._flex["REV_QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    this._flex["REV_QT_REV_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flex["NO_WBS"] = row["NO_WBS"];
                        this._flex["NO_CBS"] = row["NO_CBS"];
                        this._flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }
                    if (pdt_Line.Columns.Contains("NM_PURGRP"))
                        this._flex["NM_PURGRP"] = row["NM_PURGRP"];
                    if (this._flex.DataTable.Columns.Contains("GI_PARTNER"))
                    {
                        this._flex["GI_PARTNER"] = row["GI_PARTNER"];
                        this._flex["NM_GI_PARTER"] = row["NM_GI_PARTER"];
                    }
                    if (this.m_YN_special == "Y" && flag)
                    {
                        this._flex["JAN_QT_PASS"] = row["JAN_QT_PASS"];
                        this._flex["JAN_QT_SPECIAL"] = row["JAN_QT_SPECIAL"];
                        this._flex["FG_SPECIAL"] = row["OB_PUT"];
                    }
                    if (this.MainFrameInterface.ServerKeyCommon == "WOORIERP" || this.MainFrameInterface.ServerKeyCommon == "DZSQL" || this.MainFrameInterface.ServerKeyCommon == "SQL_")
                    {
                        this._flex["NM_USERDEF1"] = row["NM_USERDEF1"];
                        this._flex["NM_USERDEF2"] = row["NM_USERDEF2"];
                    }
                    this._flex["NO_SERL"] = row["NO_SERL"];
                    this._flex["DT_PLAN"] = row["DT_PLAN"];
                    this._flex["CLS_ITEM"] = row["CLS_ITEM"];
                    if (!this.bStandard)
                    {
                        this._flex["WEIGHT"] = row["WEIGHT"];
                        this._flex["QT_WEIGHT"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex["QT_REQ_MM"]) * D.GetDecimal(this._flex["WEIGHT"]));
                    }
                    NO_PO_MULTI = NO_PO_MULTI + D.GetString(row["NO_PO"]) + D.GetString(row["NO_POLINE"]) + "|";
                }
                this._header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                this.tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                this._flex.Redraw = true;
                if (this.m_YN_SU == "100")
                    this.SET_FLEXD(NO_PO_MULTI);
                this.ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void b_IQC_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Cheak_For_btn())
                    return;
                P_PU_REQ_IQC_SUB pPuReqIqcSub = new P_PU_REQ_IQC_SUB(D.GetString(this.cbo공장.SelectedValue), this.tb_NM_PARTNER.CodeValue, this.tb_NM_PARTNER.CodeName, this._flex.DataTable, this.cbo_TRANS.SelectedValue.ToString(), D.GetString(this.cbo_PROCESS.SelectedValue));
                if (pPuReqIqcSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    this.cbo_TRANS.Enabled = false;
                    foreach (DataColumn column in this._flex.DataTable.Clone().Columns)
                    {
                        if (column.DataType == typeof(decimal))
                            column.DefaultValue = 0;
                    }
                    this.InserGridtAddREV(pPuReqIqcSub.gdt_return, "IQC", pPuReqIqcSub.수입검사비고);
                    this.ControlEnabledDisable(false);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InserGridtAddREV_CHOSUNHOTELBA(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                decimal val1 = 1M;
                decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                this._flex.Redraw = false;
                bool flag = pdt_Line.Columns.Contains("JAN_QT_PASS");
                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    DataRow row = pdt_Line.Rows[index];
                    ++maxValue;
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex["NO_LINE"] = maxValue;
                    this._flex["NO_IOLINE"] = maxValue;
                    this._flex["CD_ITEM"] = row["CD_ITEM"];
                    this._flex["NM_ITEM"] = row["NM_ITEM"];
                    this._flex["STND_ITEM"] = row["STND_ITEM"];
                    if (row["UNIT_IM"] == null)
                        row["UNIT_IM"] = "";
                    if (D.GetDecimal(row["UNIT_PO_FACT"]) != 0M)
                        val1 = D.GetDecimal(row["UNIT_PO_FACT"]);
                    this._flex["YN_LOT"] = row["NO_LOT"];
                    this._flex["CD_UNIT_MM"] = row["UNIT_PO"];
                    this._flex["UNIT_IM"] = row["UNIT_IM"];
                    this._flex["RATE_EXCHG"] = val1;
                    this._flex["RT_VAT"] = row["RT_VAT"];
                    this._flex["DT_LIMIT"] = row["DT_REV"];
                    this._flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]) / val1);
                    this._flex["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    this._flex["QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    this._flex["QT_REJECTION"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_BAD"]));
                    this._flex["YN_INSP"] = "N";
                    this._flex["YN_INSP"] = "N";
                    this._flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PO"]));
                    this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                    this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["CD_PJT"] = row["CD_PJT"];
                    this._flex["CD_PURGRP"] = row["CD_PURGRP"];
                    this._flex["NO_PO"] = row["NO_PO"];
                    this._flex["NO_POLINE"] = row["NO_POLINE"];
                    this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM"]));
                    this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex.CDecimal(row["JAN_AM"])));
                    this._flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex.CDecimal(row["VAT_REV"].ToString())));
                    this._flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex["AM"]) + this._flex.CDecimal(this._flex["VAT"]));
                    this._flex["QT_GR_MM"] = 0;
                    this._flex["RT_CUSTOMS"] = 0;
                    this._flex["YN_RETURN"] = "N";
                    this._header.CurrentRow["YN_RETURN"] = row["YN_RETURN"];
                    this._flex["FG_TPPURCHASE"] = row["FG_PURCHASE"];
                    this._flex["YN_PURCHASE"] = row["YN_PURCHASE"].ToString();
                    this._flex["FG_POST"] = row["FG_POST"];
                    this._flex["FG_RCV"] = row["FG_RCV"];
                    this._flex["FG_TRANS"] = row["FG_TRANS"];
                    this._flex["FG_TAX"] = D.GetString(row["FG_TAX"]);
                    this._flex["CD_EXCH"] = row["CD_EXCH"];
                    this._header.CurrentRow["CD_EXCH"] = row["CD_EXCH"];
                    this._flex["RT_EXCH"] = !(D.GetDecimal(row["RT_EXCH"]) == 0M) || !(D.GetString(row["CD_EXCH"]) == "000") ? D.GetDecimal(row["RT_EXCH"]) : 1;
                    this._flex["NO_IO_MGMT"] = "";
                    this._flex["NO_IOLINE_MGMT"] = 0;
                    this._flex["NO_PO_MGMT"] = "";
                    this._flex["NO_POLINE_MGMT"] = 0;
                    this._flex["NO_TO"] = "";
                    this._flex["NO_TO_LINE"] = 0;
                    this._flex["CD_SL"] = row["CD_SL"];
                    this._flex["NM_SL"] = row["NM_SL"];
                    this._flex["FG_TAXP"] = row["FG_TAXP"];
                    this._flex["NO_EMP"] = D.GetString(this.tb_NO_EMP.CodeValue);
                    this._flex["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flex["YN_AM"] = row["YN_AM"];
                    this._header.CurrentRow["YN_AM"] = row["YN_AM"];
                    this._flex["VAT_CLS"] = 0;
                    this._flex["YN_AUTORCV"] = row["YN_AUTORCV"];
                    this._flex["YN_REQ"] = row["YN_RCV"];
                    this._flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM"]));
                    this._flex["AM_EXRCV"] = 0;
                    this._flex["AM_RCV"] = 0;
                    this._flex["NM_SYSDEF"] = "";
                    this._flex["NO_REV"] = row["NO_REV"];
                    this._flex["NO_REVLINE"] = row["NO_REVLINE"];
                    this._flex["NM_KOR"] = row["NM_KOR"];
                    this._flex["NM_FG_RCV"] = row["NM_FG_RCV"];
                    this._flex["NM_FG_POST"] = row["NM_FG_POST"];
                    this._flex["NM_PROJECT"] = row["NM_PROJECT"];
                    this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"].ToString();
                    this._flex["NO_TO"] = row["NO_TO"].ToString();
                    this._flex["NO_TO_LINE"] = row["NO_TOLINE"];
                    this._flex["CD_ZONE"] = row["CD_ZONE"];
                    if (D.GetString(this.cbo_TRANS.SelectedValue) == "003")
                    {
                        this._flex["NO_LC"] = row["NO_LC_LOCAL"].ToString();
                        this._flex["NO_LCLINE"] = row["NO_LCLINE_LOCAL"];
                    }
                    else
                    {
                        this._flex["NO_LC"] = row["NO_LC"].ToString();
                        this._flex["NO_LCLINE"] = row["NO_LCLINE"];
                    }
                    this._flex["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(row["QT_REV_VAL"]), val1));
                    this._flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["JAN_AM_REV"]));
                    this._flex["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex.CDecimal(row["JAN_AM"])));
                    this._flex["REV_QT_PASS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_PASS"]));
                    this._flex["REV_QT_REV_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_REV_VAL"]));
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flex["CD_PJT_ITEM"] = row["CD_PJT_ITEM"];
                        this._flex["NM_PJT_ITEM"] = row["NM_PJT_ITEM"];
                        this._flex["PJT_ITEM_STND"] = row["PJT_ITEM_STND"];
                        this._flex["NO_WBS"] = row["NO_WBS"];
                        this._flex["NO_CBS"] = row["NO_CBS"];
                        this._flex["SEQ_PROJECT"] = D.GetDecimal(row["SEQ_PROJECT"]);
                    }
                    if (pdt_Line.Columns.Contains("NM_PURGRP"))
                        this._flex["NM_PURGRP"] = row["NM_PURGRP"];
                    if (this.m_YN_special == "Y" && flag)
                    {
                        this._flex["JAN_QT_PASS"] = row["JAN_QT_PASS"];
                        this._flex["JAN_QT_SPECIAL"] = row["JAN_QT_SPECIAL"];
                        this._flex["FG_SPECIAL"] = row["OB_PUT"];
                    }
                    this._flex["NO_SERL"] = row["NO_SERL"];
                    this._flex["DT_PLAN"] = row["DT_PLAN"];
                    this._flex["CLS_ITEM"] = row["CLS_ITEM"];
                    this._flex["TP_UM_TAX"] = row["TP_UM_TAX"];
                    this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                }
                this._header.CurrentRow["CD_PARTNER"] = pdt_Line.Rows[0]["CD_PARTNER"];
                this.tb_NM_PARTNER.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                this._flex.Redraw = true;
                this.ControlEnabledDisable(false);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnMAIL알림_Click(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(this.tb_NoIsuRcv.Text) == string.Empty)
                    return;
                this.SetPrint(false);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void m_btnDel_Click(object sender, EventArgs e)
        {
            if (!this._flex.HasNormalRow)
                return;
            if (MA.ServerKey(true, "GIGAVIS", "MDIK", "DAEJOOKC"))
            {
                DataRow[] dataRowArray = this._flex.DataTable.Select(" S = 'Y' ", "", DataViewRowState.CurrentRows);
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                foreach (DataRow dataRow1 in dataRowArray)
                {
                    if (this._flex.CDecimal(dataRow1["QT_CLS"]) != 0M)
                    {
                        this.ShowMessage("마감된 데이터입니다. 삭제할 수 없습니다.");
                        return;
                    }
                    if (this.m_YN_SU == "100")
                    {
                        DataTable dataTable = this._flexD.DataTable;
                        string filterExpression = "NO_PO ='" + D.GetString(dataRow1["NO_PO"]) + "' AND NO_POLINE='" + D.GetString(dataRow1["NO_POLINE"]) + "'";
                        foreach (DataRow dataRow2 in dataTable.Select(filterExpression))
                            dataRow2.Delete();
                    }
                    dataRow1.Delete();
                }
            }
            else
            {
                if (this._flex.CDecimal(this._flex[this._flex.Row, "QT_CLS"]) != 0M)
                {
                    this.ShowMessage("마감된 데이터입니다. 삭제할 수 없습니다.");
                    return;
                }
                string str1 = D.GetString(this._flex["NO_PO"]);
                string str2 = D.GetString(this._flex["NO_POLINE"]);
                if (this.m_YN_SU == "100")
                {
                    DataTable dataTable = this._flexD.DataTable;
                    string filterExpression = "NO_PO ='" + str1 + "' AND NO_POLINE='" + str2 + "'";
                    foreach (DataRow dataRow in dataTable.Select(filterExpression))
                        dataRow.Delete();
                }
                this._flex.Rows.Remove(this._flex.Row);
            }
            if (!this._flex.HasNormalRow)
                this.ControlEnabledDisable(true);
            this.ToolBarSaveButtonEnabled = true;
        }

        private void btn_Appet_Click(object sender, EventArgs e)
        {
            string columnName1 = string.Empty;
            string columnName2 = string.Empty;
            string str1 = string.Empty;
            string str2 = string.Empty;
            if (!this.추가모드여부)
                return;
            switch (((Control)sender).Name)
            {
                case "btn_SL_Accept":
                    if (!this.추가모드여부)
                    {
                        this.ShowMessage("이미입고되어수정불가합니다");
                        return;
                    }
                    columnName1 = "CD_SL";
                    columnName2 = "NM_SL";
                    str1 = this.bp_CD_SL.CodeValue;
                    str2 = this.bp_CD_SL.CodeName;
                    break;
                case "btn적용_관리번호":
                    columnName1 = "DC_RMK2";
                    str1 = D.GetString(this.txt관리번호.Text);
                    break;
            }
            DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
            for (int index = 0; index < dataRowArray.Length; ++index)
            {
                dataRowArray[index][columnName1] = str1;
                if (columnName2 != string.Empty)
                    dataRowArray[index][columnName2] = str2;
            }
        }

        private void btn환정보적용_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (D.GetString(dataRow["CD_EXCH"]) == D.GetString(this.cbo_NM_EXCH.SelectedValue))
                        {
                            dataRow["RT_EXCH"] = this.tb_NM_EXCH.DecimalValue;
                            dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["AM_EX"]) * this.tb_NM_EXCH.DecimalValue);
                            dataRow["UM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]) * this.tb_NM_EXCH.DecimalValue);
                            dataRow["AM_REQ"] = dataRow["AM"];
                            dataRow["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["AM"]) * D.GetDecimal(dataRow["RT_VAT"]) * 0.01M);
                            dataRow["AM_TOTAL"] = (D.GetDecimal(dataRow["AM"]) + D.GetDecimal(dataRow["VAT"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo_NM_EXCH_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(this.cbo_NM_EXCH.SelectedValue) == "000")
                {
                    this.tb_NM_EXCH.DecimalValue = 1M;
                    this.tb_NM_EXCH.Enabled = false;
                    this.btn환정보적용.Enabled = false;
                }
                else if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                {
                    this.tb_NM_EXCH.Enabled = false;
                    this.btn환정보적용.Enabled = false;
                }
                else
                {
                    this.tb_NM_EXCH.Enabled = true;
                    this.btn환정보적용.Enabled = true;
                    decimal num = 0M;
                    if (this.tb_DT_RCV.Text != string.Empty && MA.기준환율.Option != MA.기준환율옵션.적용안함)
                        num = MA.기준환율적용(this.tb_DT_RCV.Text, D.GetString(this.cbo_NM_EXCH.SelectedValue.ToString()));
                    this.tb_NM_EXCH.DecimalValue = num;
                }
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
                if (this.tb_NoIsuRcv.Text == string.Empty)
                    return;
                string str = D.GetString((this.tb_NoIsuRcv.Text + "_" + Global.MainFrame.LoginInfo.CompanyCode));
                if (((Form)new P_MA_FILE_SUB("PU", Global.MainFrame.CurrentPageID, str)).ShowDialog((IWin32Window)this) == DialogResult.Cancel)
                    ;
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

        private void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            if (!this.서버키("CNP"))
                return;
            this.btn발주적용.Enabled = this.btn통관적용.Enabled = false;
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

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = !(((Control)sender).Name == "_flex") ? this._flexD : this._flex;
                if (this._flex.Cols[e.Col].Name == "S")
                    return;
                if (!this.추가모드여부 && flexGrid.RowState() != DataRowState.Added)
                {
                    this.ShowMessage("이미입고되어수정불가합니다");
                    e.Cancel = true;
                }
                if (D.GetString(this._flex["PO_PRICE"]) == "Y" && (this._flex.Cols[e.Col].Name == "AM_EXREQ" || this._flex.Cols[e.Col].Name == "AM_TOTAL" || this._flex.Cols[e.Col].Name == "UM_EX_PO"))
                {
                    this.ShowMessage("구매단가통제된 구매그룹입니다.");
                    e.Cancel = true;
                }
                if (D.GetString(this._flex["TP_UM_TAX"]) == "001")
                {
                    if (this._flex.Cols[e.Col].Name == "AM_EXREQ" || this._flex.Cols[e.Col].Name == "AM_REQ" || this._flex.Cols[e.Col].Name == "VAT")
                        e.Cancel = true;
                }
                else
                {
                    if (this._flex.Cols[e.Col].Name == "AM_TOTAL")
                        e.Cancel = true;
                    int num;
                    if (this._flex.Cols[e.Col].Name == "VAT")
                        num = MA.ServerKey(false, "WONIL", "DAEJOOKC") ? 1 : 0;
                    else
                        num = 1;
                    if (num == 0)
                        e.Cancel = true;
                }
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "UM_EX_PO":
                        if (this._flex["FG_TRANS"].ToString() == "004" || this._flex["FG_TRANS"].ToString() == "005")
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
                if (this.m_YN_special == "Y" && (this._flex.Cols[e.Col].Name == "AM_TOTAL" || this._flex.Cols[e.Col].Name == "AM_EXREQ" || this._flex.Cols[e.Col].Name == "QT_REQ_MM" || this._flex.Cols[e.Col].Name == "UM_EX_PO"))
                {
                    e.Cancel = true;
                }
                else
                {
                    if (!this.bStandard || !(D.GetDecimal(this._flex["UM_WEIGHT"]) > 0M) || !(this._flex.Cols[e.Col].Name == "UM_EX_PO") && !(this._flex.Cols[e.Col].Name == "AM_TOTAL") && !(this._flex.Cols[e.Col].Name == "UM_EX") && !(this._flex.Cols[e.Col].Name == "AM_EXREQ") && !(this._flex.Cols[e.Col].Name == "AM_REQ"))
                        return;
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _Grid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string val = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                string editData = ((FlexGrid)sender).EditData;
                decimal d = D.GetDecimal(this._flex["RT_VAT"]) == 0M ? 0M : D.GetDecimal(this._flex["RT_VAT"]) * 0.01M;
                decimal 부가세율 = D.GetDecimal(this._flex["RT_VAT"]) == 0M ? 0M : D.GetDecimal(this._flex["RT_VAT"]);
                decimal 환율 = D.GetDecimal(this._flex[e.Row, "RT_EXCH"]);
                string str = D.GetString(this._flex[e.Row, "FG_TAX"]);
                bool 부가세포함 = D.GetString(this._flex[e.Row, "TP_UM_TAX"]) == "001";
                decimal 외화금액 = 0M;
                decimal num1 = D.GetDecimal(this._flex[e.Row, "RATE_EXCHG"]);
                decimal 부가세 = 0M;
                if (Global.MainFrame.CurrentPageID == "P_PU_Z_JONGHAP_REQ_REG_1")
                    num1 = D.GetDecimal(this._flex[e.Row, "UM_WEIGHT"]);
                if (this._flex.AllowEditing && this._flex.GetData(e.Row, e.Col).ToString() != this._flex.EditData)
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "QT_REQ_MM":
                            this._flex[e.Row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, this._flex.CDecimal(editData) * num1);
                            decimal 원화금액1 = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "UM_EX_PO"]) * D.GetDecimal(this._flex.EditData) * 환율);
                            Calc.GetAmt(D.GetDecimal(this._flex.EditData), D.GetDecimal(this._flex[this._flex.Row, "UM_EX_PO"]), 환율, str, 부가세율, 모듈.PUR, 부가세포함, out 외화금액, out 원화금액1, out 부가세);
                            if (!부가세포함 && D.GetDecimal(this._flex["REV_QT_REQ_MM"].ToString()) == D.GetDecimal(this._flex["QT_REQ_MM"].ToString()))
                            {
                                외화금액 = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[e.Row, "REV_AM_EXREQ"].ToString()));
                                원화금액1 = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[e.Row, "REV_AM_REQ"].ToString()));
                            }
                            this._flex[e.Row, "AM_REQ"] = 원화금액1;
                            this._flex[e.Row, "AM"] = 원화금액1;
                            this._flex[e.Row, "AM_EXREQ"] = 외화금액;
                            this._flex[e.Row, "AM_EX"] = 외화금액;
                            this._flex[e.Row, "VAT"] = 부가세;
                            this._flex[e.Row, "AM_TOTAL"] = Calc.합계금액(원화금액1, 부가세);
                            this._flex[e.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[e.Row, "QT_REQ"]));
                            if (!this.bStandard)
                                this._flex[e.Row, "QT_WEIGHT"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(editData) * D.GetDecimal(this._flex[e.Row, "WEIGHT"]));
                            this.calcAM(e.Row, 0M, D.GetDecimal(editData));
                            if (MA.ServerKey(false, "FDAMK"))
                                this.setCalBoxEa_FDAMK("QT_REQ_MM", e.Row);
                            if (this.m_YN_SU == "100")
                            {
                                DataRow[] dataRowArray = this._flexD.DataTable.Select("NO_PO = '" + D.GetString(this._flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flex["NO_POLINE"]) + "'", "", DataViewRowState.CurrentRows);
                                if (dataRowArray == null || dataRowArray.Length == 0)
                                    break;
                                foreach (DataRow dataRow in dataRowArray)
                                    dataRow["QT_NEED"] = (D.GetDecimal(dataRow["QT_NEED_UNIT"]) * D.GetDecimal(editData));
                                break;
                            }
                            break;
                        case "UM_EX_PO":
                            decimal 수량 = D.GetDecimal(this._flex[this._flex.Row, "QT_REQ_MM"]);
                            if (수량 == 0M)
                                break;
                            decimal 원화금액2 = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex.EditData) * 수량 * 환율);
                            Calc.GetAmt(수량, D.GetDecimal(this._flex.EditData), 환율, str, 부가세율, 모듈.PUR, 부가세포함, out 외화금액, out 원화금액2, out 부가세);
                            this._flex[e.Row, "AM_EXREQ"] = 외화금액;
                            this._flex[e.Row, "AM_EX"] = 외화금액;
                            this._flex[e.Row, "AM_REQ"] = 원화금액2;
                            this._flex[e.Row, "AM"] = 원화금액2;
                            this._flex[e.Row, "VAT"] = 부가세;
                            this._flex[e.Row, "AM_TOTAL"] = Calc.합계금액(원화금액2, 부가세);
                            this._flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex.EditData) / num1);
                            this._flex[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex.EditData) / num1 * 환율);
                            break;
                        case "AM_EXREQ":
                            this._flex[e.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(editData) * this._flex.CDecimal(this._flex[this._flex.Row, "RT_EXCH"].ToString()));
                            this._flex[e.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[this._flex.Row, "AM_EXREQ"].ToString()) * this._flex.CDecimal(this._flex[this._flex.Row, "RT_EXCH"].ToString()));
                            this._flex[e.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[this._flex.Row, "AM_EXREQ"].ToString()));
                            this._flex[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[this._flex.Row, "AM_REQ"].ToString()) * d);
                            this._flex[e.Row, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[this._flex.Row, "AM_REQ"].ToString()) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex.CDecimal(this._flex[this._flex.Row, "VAT"].ToString()))));
                            this._flex[e.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[this._flex.Row, "AM_EXREQ"].ToString()) / this._flex.CDecimal(this._flex[this._flex.Row, "QT_REQ_MM"].ToString()));
                            this._flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[this._flex.Row, "AM_EXREQ"].ToString()) / this._flex.CDecimal(this._flex[this._flex.Row, "QT_REQ"].ToString()));
                            this._flex[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[this._flex.Row, "AM_REQ"].ToString()) / this._flex.CDecimal(this._flex[this._flex.Row, "QT_REQ"].ToString()));
                            break;
                        case "QT_REQ":
                            if (this._flex.CDouble(editData) != this._flex.CDouble(this._flex[e.Row, "QT_REQ_MM"]) && Global.MainFrame.ShowMessage("의뢰량과 관리수량이 다릅니다. 계속 입력하시겠습니까?", "QY2") != DialogResult.Yes)
                            {
                                ((FlexGrid)sender)["QT_REQ"] = ((C1FlexGridBase)sender).GetData(e.Row, e.Col);
                                break;
                            }
                            this.calcAM(e.Row, 0M, D.GetDecimal(editData));
                            if (this.m_YN_SU == "100")
                            {
                                DataRow[] dataRowArray = this._flexD.DataTable.Select("NO_PO = '" + D.GetString(this._flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flex["NO_POLINE"]) + "'", "", DataViewRowState.CurrentRows);
                                if (dataRowArray == null || dataRowArray.Length == 0)
                                    break;
                                foreach (DataRow dataRow in dataRowArray)
                                    dataRow["QT_NEED"] = (D.GetDecimal(dataRow["QT_NEED_UNIT"]) * D.GetDecimal(editData));
                                break;
                            }
                            break;
                        case "AM_REQ":
                            this._flex[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(editData) * d);
                            this._flex[e.Row, "AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) + D.GetDecimal(this._flex[e.Row, "VAT"]));
                            this._flex[e.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData));
                            break;
                        case "AM_TOTAL":
                            if (D.GetDecimal(this._flex[e.Row, "QT_REQ_MM"]) == 0M)
                                break;
                            decimal num2 = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex[e.Row, "UM_EX_PO"]));
                            if (부가세포함)
                            {
                                decimal num3 = D.GetDecimal(this._flex.EditData);
                                decimal num4 = !this.의제매입여부(str) || !(this.s_vat_fictitious == "100") ? (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR ? decimal.Round(num3 / ++d * d, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num3 / ++d * d)) : Unit.원화금액(DataDictionaryTypes.PU, num3 * d);
                                this._flex[e.Row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, num3 - num4);
                                this._flex[e.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, num3 - num4);
                                this._flex[e.Row, "AM_EXREQ"] = Unit.원화금액(DataDictionaryTypes.PU, (num3 - num4) / 환율);
                                this._flex[e.Row, "AM_EX"] = Unit.원화금액(DataDictionaryTypes.PU, (num3 - num4) / 환율);
                                this._flex[e.Row, "VAT"] = num4;
                                this._flex[e.Row, "UM_EX_PO"] = num2;
                                this._flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, num2 / num1);
                                this._flex[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, num2 / num1 * 환율);
                                break;
                            }
                            break;
                        case "UM_WEIGHT":
                            this.calcAM(e.Row, D.GetDecimal(this._flex[e.Row, "TOT_WEIGHT"]), 0M);
                            break;
                        case "TOT_WEIGHT":
                            this.calcAM(e.Row, D.GetDecimal(editData), 0M);
                            break;
                        case "VAT":
                            if (Global.MainFrame.ServerKeyCommon.Contains("WONIL"))
                            {
                                decimal num5 = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["AM_REQ"]) * d);
                                if (Math.Abs(D.GetDecimal(editData) - num5) > num5 * 0.3M)
                                {
                                    this.ShowMessage("부가세를 원부가세의 (±)30% 초과 수정 할 수 없습니다.");
                                    this._flex["VAT"] = D.GetDecimal(val);
                                    e.Cancel = true;
                                    break;
                                }
                                this._flex["AM_TOTAL"] = (D.GetDecimal(this._flex["AM_REQ"]) + Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData)));
                            }
                            if (MA.ServerKey(false, "DAEJOOKC", "MEERE"))
                            {
                                if (Math.Abs(Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["AM"]) * d) - D.GetDecimal(editData)) > 100M)
                                {
                                    this.ShowMessage("과세율에 해당하는 부가세 금액 표준오차보다 초과합니다.");
                                    this._flex["VAT"] = D.GetDecimal(val);
                                    e.Cancel = true;
                                    break;
                                }
                                break;
                            }
                            break;
                        case "NUM_USERDEF1_IO":
                            if (MA.ServerKey(false, "FDAMK"))
                            {
                                this.setCalBoxEa_FDAMK("NUM_USERDEF1_IO", e.Row);
                                break;
                            }
                            break;
                        case "NUM_USERDEF2_IO":
                            if (MA.ServerKey(false, "FDAMK"))
                            {
                                this.setCalBoxEa_FDAMK("NUM_USERDEF2_IO", e.Row);
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

        private void _Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch ((!(((Control)sender).Name == "_flex") ? this._flexD : this._flex).Cols[e.Col].Name)
                {
                    case "NM_SL":
                        e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        break;
                    case "CD_SL":
                        e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!(this.m_YN_SU == "100"))
                    return;
                this._flexD.RowFilter = "NO_PO = '" + D.GetString(this._flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flex["NO_POLINE"]) + "' ";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void txt바코드_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this._flex.HasNormalRow)
                    return;
                if (!this.chk바코드사용유무.Checked)
                {
                    this.ShowMessage("바코드사용여부를 체크 해주시기 바랍니다.");
                }
                else
                {
                    if (!(D.GetString(this.txt바코드.Text) != string.Empty) || e.KeyChar != '\r')
                        return;
                    if (!this.Cheak_For_btn())
                    {
                        this.txt바코드.Text = string.Empty;
                    }
                    else
                    {
                        if (Global.MainFrame.ServerKeyCommon == "TELCON")
                        {
                            string str = this._biz.strTelcon(this.txt바코드.Text);
                            object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this.cbo_TRANS.SelectedValue,
                                                               this.cbo_PROCESS.SelectedValue,
                                                               this.txt바코드.Text };
                            if (str == "NOT")
                            {
                                this.ShowMessage("해당가입고번호가 없거나 거래구분, L/C기준이 잘못지정되었습니다." + this.txt바코드.Text);
                                this.txt바코드.Text = "";
                                return;
                            }
                            DataTable pdt_Line = !(str == "Y") ? this._biz.search_barcode_rev(objArray) : this._biz.search_barcode_iqc(objArray);
                            if (pdt_Line == null || pdt_Line.Rows.Count == 0)
                            {
                                this.ShowMessage("해당가입고번호가 없거나 거래구분, L/C기준이 잘못지정되었습니다." + this.txt바코드.Text);
                                this.txt바코드.Text = "";
                                return;
                            }
                            this.InsertGrid_REV_BARCODE(pdt_Line);
                        }
                        else
                        {
                            string[] strArray = this.txt바코드.Text.Split('+');
                            if (strArray.Length != 2)
                                return;
                            DataTable pdt_Line = this._biz.search_barcode_rev(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                             this.cbo_TRANS.SelectedValue,
                                                                                             this.cbo_PROCESS.SelectedValue,
                                                                                             D.GetString(strArray[1]) });
                            if (pdt_Line == null || pdt_Line.Rows.Count == 0)
                            {
                                this.ShowMessage("해당가입고번호가 없거나 거래구분, L/C기준이 잘못지정되었습니다.");
                                return;
                            }
                            this.InsertGrid_REV_BARCODE(pdt_Line);
                        }
                        this.btn통관적용.Enabled = false;
                        this.cbo_TRANS.Enabled = false;
                        this.ToolBarDeleteButtonEnabled = false;
                        Settings1.Default.chk_barcode_use = !this.chk바코드사용유무.Checked ? "N" : "Y";
                        Settings1.Default.Save();
                        this.txt바코드.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpCodeTextBox_QueryBefore(object sender, BpQueryArgs e)
        {
            if (e.HelpID != HelpID.P_MA_SL_SUB)
                return;
            if (D.GetString(this.cbo공장.SelectedValue) == "")
            {
                this.ShowMessage("PU_M000070");
                this.cbo공장.Focus();
                e.QueryCancel = true;
            }
            else
                e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
        }

        private void tb_NO_EMP_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "tb_NO_EMP":
                        this._header.CurrentRow["CD_DEPT"] = e.HelpReturn.Rows[0]["CD_DEPT"];
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo공장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.bp_CD_SL.CodeValue = "";
                this.bp_CD_SL.CodeName = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                this._flexD.RowFilter = "NO_PO = '" + D.GetString(this._flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flex["NO_POLINE"]) + "' ";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void chk_barcode_use_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chk바코드사용유무.Checked)
                return;
            this.txt바코드.Focus();
        }

        private void Control_KeyEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
                SendKeys.SendWait("{TAB}");
            if (!(((Control)sender).Name == "cbo_TRANS"))
                return;
            this.cbo_TRANS_SelectionChangeCommitted(sender, (EventArgs)e);
        }

        private bool FieldCheck()
        {
            try
            {
                if (D.GetString(this.cbo공장.SelectedValue) == "")
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장명.Text);
                    this.cbo공장.Focus();
                    return false;
                }
                if (this.tb_NM_PARTNER.CodeValue.Trim() == "")
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처명.Text);
                    this.tb_NM_PARTNER.Focus();
                    return false;
                }
                if (this.tb_DT_RCV.Text.Trim() == "")
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl입고일자.Text);
                    this.tb_DT_RCV.Focus();
                    return false;
                }
                if (this.tb_NO_EMP.CodeValue.Trim() == "")
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl담당자.Text);
                    this.tb_NO_EMP.Focus();
                    return false;
                }
                if (this.cbo_TRANS.SelectedValue.ToString() == "" || this.cbo_TRANS.SelectedIndex.ToString() == "")
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래구분.Text);
                    this.cbo_TRANS.Focus();
                    return false;
                }
                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        private bool Cheak_For_btn()
        {
            if (this.cbo공장.SelectedValue == null || D.GetString(this.cbo공장.SelectedValue) == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장명.Text);
                return false;
            }
            if (!(this.tb_NO_EMP.CodeValue.ToString() == ""))
                return true;
            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl담당자.Text);
            return false;
        }

        protected override bool IsChanged() => base.IsChanged() || this.헤더변경여부;

        private void cbo_TRANS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.cbo_TRANS.SelectedValue.ToString() == "001")
            {
                this.cbo_PROCESS.Enabled = false;
                this.btn발주적용.Enabled = true;
                this.btn통관적용.Enabled = false;
                this.btnLocalLC.Enabled = false;
            }
            if (this.cbo_TRANS.SelectedValue.ToString() == "002")
            {
                this.cbo_PROCESS.Enabled = false;
                this.btn발주적용.Enabled = true;
                this.btn통관적용.Enabled = false;
                this.btnLocalLC.Enabled = false;
                this.cbo_PROCESS_SelectionChangeCommitted(sender, e);
            }
            if (this.cbo_TRANS.SelectedValue.ToString() == "003")
            {
                this.cbo_PROCESS.Enabled = true;
                if (this.cbo_PROCESS.SelectedValue.ToString() == "001")
                {
                    this.btn발주적용.Enabled = false;
                    this.btn통관적용.Enabled = false;
                    this.btnLocalLC.Enabled = true;
                }
                if (this.cbo_PROCESS.SelectedValue.ToString() == "002")
                {
                    this.btn발주적용.Enabled = true;
                    this.btn통관적용.Enabled = false;
                    this.btnLocalLC.Enabled = false;
                }
                this.cbo_PROCESS_SelectionChangeCommitted(sender, e);
            }
            if (this.cbo_TRANS.SelectedValue.ToString() == "004" || this.cbo_TRANS.SelectedValue.ToString() == "005")
            {
                this.cbo_PROCESS.Enabled = true;
                if (this.cbo_PROCESS.SelectedValue.ToString() == "001")
                {
                    this.btn발주적용.Enabled = false;
                    this.btnLocalLC.Enabled = false;
                    this.btn통관적용.Enabled = true;
                }
                this.cbo_PROCESS_SelectionChangeCommitted(sender, e);
            }
            if (!this.서버키("CNP") || !(this.m_sEnv_IQC == "Y"))
                return;
            this.btn발주적용.Enabled = this.btn통관적용.Enabled = false;
        }

        private void cbo_PROCESS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.cbo_PROCESS.SelectedValue.ToString() == "001")
            {
                if (this.cbo_TRANS.SelectedValue.ToString() == "002")
                {
                    this.btn발주적용.Enabled = true;
                    this.btn통관적용.Enabled = false;
                }
                if (this.cbo_TRANS.SelectedValue.ToString() == "003")
                {
                    this.btn발주적용.Enabled = false;
                    this.btn통관적용.Enabled = false;
                    this.btnLocalLC.Enabled = true;
                }
                if (this.cbo_TRANS.SelectedValue.ToString() == "004" || this.cbo_TRANS.SelectedValue.ToString() == "005")
                {
                    this.btn발주적용.Enabled = false;
                    this.btn통관적용.Enabled = true;
                    this.btnLocalLC.Enabled = false;
                }
            }
            if (!(this.cbo_PROCESS.SelectedValue.ToString() == "002"))
                return;
            if (this.cbo_TRANS.SelectedValue.ToString() == "002")
            {
                this.btn발주적용.Enabled = true;
                this.btn통관적용.Enabled = false;
            }
            if (this.cbo_TRANS.SelectedValue.ToString() == "003")
            {
                this.btn발주적용.Enabled = true;
                this.btn통관적용.Enabled = false;
                this.btnLocalLC.Enabled = false;
            }
        }

        private bool 서버키(string pstr_서버키) => Global.MainFrame.ServerKeyCommon == pstr_서버키;

        private bool 서버키_TEST포함(string pstr_서버키) => Global.MainFrame.ServerKeyCommon == pstr_서버키 || Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_" || Global.MainFrame.ServerKeyCommon == "SQL_108";

        private void calcAM(int row, decimal TOT_WEIGHT, decimal QT_REQ)
        {
            if (!this.bStandard || !(Global.MainFrame.ServerKey == "SINJINSM") || !(D.GetDecimal(this._flex[row, "UM_WEIGHT"]) != 0M))
                return;
            if (TOT_WEIGHT == 0M)
            {
                this._flex[row, nameof(TOT_WEIGHT)] = Math.Round(D.GetDecimal(this._flex[row, "WEIGHT"]) * QT_REQ, 1);
                TOT_WEIGHT = D.GetDecimal(this._flex[row, nameof(TOT_WEIGHT)]);
            }
            decimal val1 = D.GetDecimal(this._flex[row, "RATE_EXCHG"]);
            this._flex[row, "AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(TOT_WEIGHT * D.GetDecimal(this._flex[row, "UM_WEIGHT"])));
            this._flex[row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, Math.Round(TOT_WEIGHT * D.GetDecimal(this._flex[row, "UM_WEIGHT"])));
            this._flex[row, "UM_EX"] = !(D.GetDecimal(this._flex[row, "QT_REQ_MM"]) != 0M) ? 0 : UDecimal.Getdivision(D.GetDecimal(this._flex[row, "AM_EXREQ"]) / D.GetDecimal(this._flex[row, "QT_REQ_MM"]), val1);
            this._flex[row, "AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "AM_EXREQ"]) * D.GetDecimal(this._flex[row, "RT_EXCH"]));
            this._flex[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "AM_EXREQ"]) * D.GetDecimal(this._flex[row, "RT_EXCH"]));
            this._flex[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[row, "UM_EX"]) * val1);
            decimal 부가세율 = D.GetDecimal(this._flex["RT_VAT"]) == 0M ? 0M : D.GetDecimal(this._flex["RT_VAT"]);
            bool 부가세포함 = D.GetString(this._flex[row, "TP_UM_TAX"]) == "001";
            decimal 원화금액 = 0M;
            decimal 외화금액 = 0M;
            decimal 부가세 = 0M;
            Calc.GetAmt(D.GetDecimal(this._flex[row, "QT_REQ_MM"]), D.GetDecimal(this._flex[row, "UM_EX_PO"]), D.GetDecimal(this._flex[row, "RT_EXCH"]), D.GetString(this._flex[row, "FG_TAX"]), 부가세율, 모듈.PUR, 부가세포함, out 외화금액, out 원화금액, out 부가세);
            this._flex[row, "AM_REQ"] = 원화금액;
            this._flex[row, "AM"] = 원화금액;
            this._flex[row, "AM_EXREQ"] = 외화금액;
            this._flex[row, "AM_EX"] = 외화금액;
            this._flex[row, "VAT"] = 부가세;
            this._flex[row, "AM_TOTAL"] = (D.GetDecimal(this._flex[row, "VAT"]) + D.GetDecimal(this._flex[row, "AM_REQ"]));
        }

        private void 부가세계산(DataRow row)
        {
            decimal num1 = 0M;
            decimal d = D.GetDecimal(row["RT_VAT"]) == 0M ? 0M : D.GetDecimal(row["RT_VAT"]) / 100M;
            decimal num2 = D.GetDecimal(row["QT_REQ_MM"]);
            decimal val = D.GetDecimal(row["UM_EX_PO"]);
            decimal num3 = D.GetDecimal(this._flex["RT_EXCH"]) == 0M ? 1M : D.GetDecimal(this._flex["RT_EXCH"]);
            if (num2 == 0M)
                return;
            decimal num4;
            decimal num5;
            decimal num6;
            if (D.GetString(row["TP_UM_TAX"]) == "001")
            {
                decimal num7 = Decimal.Round(num2 * val * num3, MidpointRounding.AwayFromZero);
                num4 = !(this.s_vat_fictitious == "100") ? Decimal.Round(num7 / ++d * d, MidpointRounding.AwayFromZero) : Unit.원화금액(DataDictionaryTypes.PU, num7 * d);
                num5 = Unit.원화금액(DataDictionaryTypes.PU, num7 - num4);
                num6 = Unit.외화금액(DataDictionaryTypes.PU, num5 / num3);
            }
            else
            {
                num6 = Unit.외화금액(DataDictionaryTypes.PU, num2 * val);
                num5 = Unit.원화금액(DataDictionaryTypes.PU, num2 * val * num3);
                num4 = Unit.원화금액(DataDictionaryTypes.PU, num5 * d);
                num1 = Unit.원화금액(DataDictionaryTypes.PU, num5 + num4);
            }
            row["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, val);
            row["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, val / (D.GetDecimal(row["RATE_EXCHG"]) == 0M ? 1M : D.GetDecimal(row["RATE_EXCHG"])));
            row["UM"] = Unit.원화단가(DataDictionaryTypes.PU, val / (D.GetDecimal(row["RATE_EXCHG"]) == 0M ? 1M : D.GetDecimal(row["RATE_EXCHG"])) * num3);
            row["AM_EXREQ"] = num6;
            row["AM_EX"] = num6;
            row["AM_REQ"] = num5;
            row["AM"] = num5;
            row["VAT"] = num4;
            row["AM_TOTAL"] = (num5 + num4);
            this._flex.SumRefresh();
        }

        private void SET_FLEXD(string NO_PO_MULTI)
        {
            try
            {
                if (NO_PO_MULTI == string.Empty)
                    return;
                DataTable dataTable = this._biz.Search_MATL(NO_PO_MULTI, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                        "",
                                                                                        D.GetString(this.tb_NM_PARTNER.CodeValue),
                                                                                        D.GetString(this.cbo공장.SelectedValue) });
                string filter1 = "NO_PO = '" + D.GetString(this._flex["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flex["NO_POLINE"]) + "' ";
                decimal num1 = D.GetDecimal(this._flexD.DataTable.Compute("MAX(NO_IOLINE)", filter1));
                if (dataTable == null || dataTable.Rows.Count == 0)
                    return;
                foreach (DataRow row in dataTable.Rows)
                {
                    this._flexD.Rows.Add();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    string filter2 = "NO_PO = '" + D.GetString(row["NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(row["NO_POLINE"]) + "' ";
                    decimal num2 = D.GetDecimal(this._flex.DataTable.Compute("MAX(QT_REQ_MM)", filter2));
                    this._flexD["CD_ITEM"] = row["CD_ITEM"];
                    this._flexD["NM_ITEM_ITEM"] = row["NM_ITEM_ITEM"];
                    this._flexD["STND_ITEM"] = row["STND_ITEM"];
                    this._flexD["STND_ITEM_ITEM"] = row["STND_ITEM_ITEM"];
                    this._flexD["UNIT_IM_ITEM"] = row["UNIT_IM_ITEM"];
                    this._flexD["CD_MATL"] = row["CD_MATL"];
                    this._flexD["NM_ITEM"] = row["NM_ITEM"];
                    this._flexD["STND_ITEM"] = row["STND_ITEM"];
                    this._flexD["UNIT_IM"] = row["UNIT_IM"];
                    this._flexD["QT_NEED"] = (D.GetDecimal(row["QT_NEED_UNIT"]) * num2);
                    this._flexD["NO_PO"] = row["NO_PO"];
                    this._flexD["NO_POLINE"] = row["NO_POLINE"];
                    this._flexD["NO_PO_MAL_LINE"] = row["NO_PO_MAL_LINE"];
                    this._flexD["CD_SL"] = row["CD_SL"];
                    this._flexD["NM_SL"] = row["NM_SL"];
                    this._flexD["NO_IOLINE"] = ++num1;
                    this._flexD["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                    this._flexD["DT_IO"] = D.GetString(this.tb_DT_RCV.Text);
                    this._flexD["NO_PSO_MGMT"] = row["NO_PO"];
                    this._flexD["NO_PSOLINE_MGMT"] = row["NO_POLINE"];
                    this._flexD["CD_PARTNER"] = D.GetString(this.tb_NM_PARTNER.CodeValue);
                    this._flexD["QT_IO"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_NEED"]));
                    this._flexD["NO_EMP"] = D.GetString(this.tb_NO_EMP.CodeValue);
                    this._flexD["FG_IO"] = row["FG_IO"];
                    this._flexD["CD_QTIOTP"] = row["CD_QTIOTP"];
                    this._flexD["FG_TRANS"] = row["FG_TRANS"];
                    this._flexD["GI_PARTNER"] = D.GetString(this.tb_NM_PARTNER.CodeValue);
                    this._flexD["YN_RETURN"] = row["YN_RETURN"];
                    this._flexD["CD_DEPT"] = row["CD_DEPT"];
                    this._flexD["NO_IOLINE_MGMT"] = D.GetDecimal(this._flex.DataTable.Compute("MAX(NO_LINE)", filter2));
                    this._flexD["QT_NEED_UNIT"] = row["QT_NEED_UNIT"];
                    this._flexD["YN_PARTNER_SL"] = row["YN_PARTNER_SL"];
                }
                this._flexD.AddFinished();
                this._flexD.Col = this._flexD.Cols.Fixed;
                this._flexD.RowFilter = filter1;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetButtonDisp(RoundedButton[] p_button, int p_X)
        {
            int num = 1;
            int x = p_X;
            for (int index = 0; index < p_button.Length; ++index)
            {
                if (p_button[index].Visible)
                {
                    int width = p_button[index].Width;
                    x = x - num - width;
                    p_button[index].SetBounds(x, p_button[index].Location.Y, p_button[index].Width, p_button[index].Height);
                }
            }
        }

        private void setCol_HIOKI()
        {
            try
            {
                string[] strArray = new string[] { "NM_USERDEF1_RCV",
                                                   "NM_USERDEF2_RCV",
                                                   "NM_USERDEF3_IO",
                                                   "CD_USERDEF1_RCV",
                                                   "CD_USERDEF2_RCV",
                                                   "CD_USERDEF3_IO",
                                                   "CD_USERDEF4_IO",
                                                   "CD_USERDEF5_IO",
                                                   "CD_USERDEF6_IO",
                                                   "TXT_USERDEF1_IO",
                                                   "DC_RMK2_IO" };
                for (int index = 1; index <= strArray.Length; ++index)
                {
                    if (this._flex.Cols.Contains(strArray[index - 1]))
                    {
                        this._flex.Cols[strArray[index - 1]].Caption = this.DD("TEXT" + index.ToString());
                        this._flex.Cols.Move(this._flex.Cols[strArray[index - 1]].Index, this._flex.Cols.Count - 1);
                    }
                    else
                        this._flex.SetCol(strArray[index - 1], this.DD("TEXT" + index.ToString()), 100, true);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void setCalBoxEa_FDAMK(string strFlag, int row)
        {
            if (D.GetDecimal(this._flex[row, "NUM_USERDEF3_IO"]) <= 0M)
                return;
            if (strFlag == "NUM_USERDEF1_IO")
                this._flex[row, "QT_REQ_MM"] = (D.GetDecimal(this._flex[row, "NUM_USERDEF3_IO"]) * D.GetDecimal(this._flex[row, "NUM_USERDEF1_IO"]) + D.GetDecimal(this._flex[row, "NUM_USERDEF2_IO"]));
            else if (strFlag == "NUM_USERDEF2_IO")
                this._flex[row, "QT_REQ_MM"] = !(D.GetDecimal(this._flex[row, "NUM_USERDEF2_IO"]) > D.GetDecimal(this._flex[row, "NUM_USERDEF3_IO"])) ? (D.GetDecimal(this._flex[row, "NUM_USERDEF3_IO"]) * D.GetDecimal(this._flex[row, "NUM_USERDEF1_IO"]) + D.GetDecimal(this._flex[row, "NUM_USERDEF2_IO"])) : D.GetDecimal(this._flex[row, "NUM_USERDEF2_IO"]);
            decimal num1 = Math.Truncate(D.GetDecimal(this._flex[row, "QT_REQ_MM"]) / D.GetDecimal(this._flex[row, "NUM_USERDEF3_IO"]));
            if (strFlag != "NUM_USERDEF1_IO")
                this._flex[row, "NUM_USERDEF1_IO"] = num1;
            this._flex[row, "NUM_USERDEF2_IO"] = (D.GetDecimal(this._flex[row, "QT_REQ_MM"]) - num1 * D.GetDecimal(this._flex[row, "NUM_USERDEF3_IO"]));
            if (!(strFlag != "QT_REQ_MM"))
                return;
            decimal num2 = D.GetDecimal(this._flex[row, "RT_VAT"]) == 0M ? 0M : D.GetDecimal(this._flex[row, "RT_VAT"]) * 0.01M;
            decimal 부가세율 = D.GetDecimal(this._flex[row, "RT_VAT"]) == 0M ? 0M : D.GetDecimal(this._flex[row, "RT_VAT"]);
            decimal 환율 = D.GetDecimal(this._flex[row, "RT_EXCH"]);
            string 부가세매입구분 = D.GetString(this._flex[row, "FG_TAX"]);
            bool 부가세포함 = D.GetString(this._flex[row, "TP_UM_TAX"]) == "001";
            decimal 외화금액 = 0M;
            D.GetDecimal(this._flex[row, "RATE_EXCHG"]);
            decimal 부가세 = 0M;
            this._flex[row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[row, "QT_REQ_MM"]) * D.GetDecimal(this._flex[row, "RATE_EXCHG"]));
            this._flex[row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "QT_REQ"]));
            decimal 원화금액 = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "UM_EX_PO"]) * D.GetDecimal(this._flex.EditData) * 환율);
            Calc.GetAmt(D.GetDecimal(this._flex[row, "QT_REQ_MM"]), D.GetDecimal(this._flex[row, "UM_EX_PO"]), 환율, 부가세매입구분, 부가세율, 모듈.PUR, 부가세포함, out 외화금액, out 원화금액, out 부가세);
            if (!부가세포함 && D.GetDecimal(this._flex[row, "REV_QT_REQ_MM"].ToString()) == D.GetDecimal(this._flex[row, "QT_REQ_MM"].ToString()))
            {
                외화금액 = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "REV_AM_EXREQ"].ToString()));
                원화금액 = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "REV_AM_REQ"].ToString()));
            }
            this._flex[row, "AM_REQ"] = 원화금액;
            this._flex[row, "AM"] = 원화금액;
            this._flex[row, "AM_EXREQ"] = 외화금액;
            this._flex[row, "AM_EX"] = 외화금액;
            this._flex[row, "VAT"] = 부가세;
            this._flex[row, "AM_TOTAL"] = Calc.합계금액(원화금액, 부가세);
            this._flex[row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "QT_REQ"]));
            if (!this.bStandard)
                this._flex[row, "QT_WEIGHT"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "QT_REQ_MM"]) * D.GetDecimal(this._flex[row, "WEIGHT"]));
            this.calcAM(row, 0M, D.GetDecimal(this._flex[row, "QT_REQ_MM"]));
            if (this.m_YN_SU == "100")
            {
                DataRow[] dataRowArray = this._flexD.DataTable.Select("NO_PO = '" + D.GetString(this._flex[row, "NO_PO"]) + "' AND NO_POLINE = '" + D.GetString(this._flex[row, "NO_POLINE"]) + "'", "", DataViewRowState.CurrentRows);
                if (dataRowArray == null || dataRowArray.Length == 0)
                    return;
                foreach (DataRow dataRow in dataRowArray)
                    dataRow["QT_NEED"] = (D.GetDecimal(dataRow["QT_NEED_UNIT"]) * D.GetDecimal(this._flex[row, "QT_REQ_MM"]));
            }
        }

        private bool 추가모드여부 => this._header.JobMode == JobModeEnum.추가후수정;

        private bool 헤더변경여부
        {
            get
            {
                bool 헤더변경여부 = this._header.GetChanges() != null;
                if (헤더변경여부 && this._header.JobMode == JobModeEnum.추가후수정 && !this._flex.HasNormalRow)
                    헤더변경여부 = false;
                return 헤더변경여부;
            }
        }

        private bool 의제매입여부(string ps_taxp) => ps_taxp == "27" || ps_taxp == "28" || ps_taxp == "29" || ps_taxp == "30" || ps_taxp == "32" || ps_taxp == "33" || ps_taxp == "34" || ps_taxp == "35" || ps_taxp == "36" || ps_taxp == "40" || ps_taxp == "41" || ps_taxp == "42" || ps_taxp == "48" || ps_taxp == "49";

        private void 원그리드적용하기()
        {
            Size size1 = this.oneGrid1.Size;
            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.bpPanelControl2.IsNecessaryCondition = true;
            this.bpPanelControl3.IsNecessaryCondition = true;
            this.bpPanelControl4.IsNecessaryCondition = true;
            this.bpPanelControl7.IsNecessaryCondition = true;
            this.bpPanelControl11.IsNecessaryCondition = true;
            this.bpPanelControl12.IsNecessaryCondition = true;
            this.oneGrid1.IsSearchControl = false;
            this.oneGrid1.InitCustomLayout();
            Size size2 = this.oneGrid1.Size;
            Size size3 = this.panel_Head.Size;
            size3.Height += size2.Height - size1.Height;
            this.panel_Head.Size = size3;
        }

        private void btnCompanyUse_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tb_NoIsuRcv.Text == "")
                    return;
                string[] strArray = new string[] { this.DD("진행"),
                                                   this.DD("종결"),
                                                   this.DD("미상신"),
                                                   this.DD("취소"),
                                                   this.DD("반려") };
                int st_stat = (int)this._biz.GetFI_GWDOCU(this._header.CurrentRow["NO_RCV"].ToString())[0];
                bool flag1 = true;
                switch (st_stat)
                {
                    case -1:
                        st_stat = 4;
                        goto default;
                    case 999:
                        bool flag2 = this.save_TF(st_stat);
                        string str1 = this._header.CurrentRow["NO_RCV"].ToString();
                        DataTable dataTable = Global.MainFrame.FillDataTable("SELECT DOC_NO FROM FI_GWDOCU WHERE CD_COMPANY='" + Global.MainFrame.LoginInfo.CompanyCode + "' AND NO_DOCU ='" + str1 + "'");
                        string str2 = "";
                        if (dataTable != null && dataTable.Rows.Count > 0)
                            str2 = D.GetString(dataTable.Rows[0]["DOC_NO"]);
                        if (this._flexD.Rows.Count > 0 && flag2)
                            this._biz.GW_insert(this._header.CurrentRow, "", "11", "");
                        Process.Start("IExplore.exe", "http://hinet.kahp.or.kr/Appr/Gateway/openApprKAHP.on21?FORMTYPE=IU010&LOGINID=" + Global.MainFrame.LoginInfo.EmployeeNo + "&ERPNO=" + str1 + "&GWAPPRDOCNO=" + str2);
                        break;
                    default:
                        this.ShowMessage("전자결제 @ 중 입니다.", strArray[st_stat]);
                        flag1 = false;
                        goto case 999;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool save_TF(int st_stat)
        {
            bool flag = false;
            if (st_stat == 4)
                st_stat = -1;
            if (st_stat == 0 || st_stat == 1)
                flag = false;
            else if (st_stat == -1 || st_stat == 2 || st_stat == 3 || st_stat == 999)
                flag = true;
            return flag;
        }
    }
}

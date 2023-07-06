using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Erpu.Net.File;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using Duzon.ERPU.PU.Common;
using Duzon.Windows.Print;
using DzHelpFormLib;
using pur;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PU_CGI_REG_WINTEC : PageBase
	{
        private P_CZ_PU_CGI_REG_WINTEC_BIZ _biz = new P_CZ_PU_CGI_REG_WINTEC_BIZ();
        private FreeBinding _header = new FreeBinding();
        public string MNG_LOT = string.Empty;
        public string MNG_SERIAL = string.Empty;
        private DataTable DT_SL = null;
        private DataTable _dt_partner = null;
        private DataTable _dt_gi_prt = null;
        private DataTable _dtReqDataREQ = new DataTable();
        private string rt_dt_io;
        private string rt_cd_plant;
        private string rt_no_io;
        private string fg_gubun;
        private string rt_fg_cls = string.Empty;
        private bool bUmSetting = false;
        private bool bStandard = false;
        private decimal d_SEQ_PROJECT = 0M;
        private string s_CD_PJT_ITEM = string.Empty;
        private string s_NM_PJT_ITEM = string.Empty;
        private string s_PJT_ITEM_STND = string.Empty;
        private string bUseinv = "000";
        private ComUser _USER = null;

        public P_CZ_PU_CGI_REG_WINTEC()
		{
			try
			{
                InitializeComponent();
            }
			catch (Exception ex)
			{
                this.MsgEnd(ex);
			}
        }

        public P_CZ_PU_CGI_REG_WINTEC( string ps_fg_gubun, string pg_no_io, string ps_dt_io, string ps_cd_plant, string ps_fg_cls)
        {
            try
            {
                this.InitializeComponent();

                this.fg_gubun = ps_fg_gubun;
                this.rt_no_io = pg_no_io;
                this.rt_dt_io = ps_dt_io;
                this.rt_cd_plant = ps_cd_plant;
                this.rt_fg_cls = ps_fg_cls;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public P_CZ_PU_CGI_REG_WINTEC(string 수불번호)
        {
            try
            {
                this.InitializeComponent();

                this.rt_no_io = 수불번호;
                this.fg_gubun = "재고자산수불부";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnCallExistingPageMethod(object sender, PageEventArgs e)
        {
            this.rt_no_io = D.GetString(e.Args[0]);
            this.fg_gubun = "재고자산수불부";
            this.InitPaint();
        }

        private void _header_JobModeChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                if (e.JobMode == JobModeEnum.조회후수정)
                {
                    this.SetButtonState(false);
                    this.btn첨부파일.Enabled = true;
                }
                else
                {
                    this.txt수불번호.Enabled = true;
                    this.btn추가.Enabled = true;
                    this.btn삭제.Enabled = true;
                    this.btn첨부파일.Enabled = false;
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
                if (sender == null)
                    return;
                switch (((Control)sender).Name)
                {
                    case "cbo_FG_SUBSTITUTE":
                        this.ChangeFgTpio();
                        break;
                }
                this.Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarSaveButtonEnabled = this.IsChanged();
                if (!this.추가모드여부)
                    return;
                this.SetButtonState(!this.IsChanged());
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool IsChanged() => base.IsChanged() || this.헤더변경여부;

        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();

                if (MA.ServerKey(false, "STRAFFIC"))
                    this._USER = new ComUser();
                if (BASIC.GetMAEXC_Menu("P_PU_CGI_REG", "PU_A00000027") == "100")
                    this.bUseinv = "100";
                this.InitGrid();
                this.MNG_LOT = this._biz.Search_LOT().Rows[0]["MNG_LOT"].ToString();
                this.MNG_SERIAL = this._biz.Search_SERIAL().Rows[0]["YN_SERIAL"].ToString();
                if (string.Compare(this.MNG_SERIAL, "Y") == 0)
                    this.btn시리얼정보.Visible = true;
                if (BASIC.GetMAEXC_Menu("P_PU_CGI_REG", "PU_A00000026") == "001")
                {
                    this.bUmSetting = true;
                    this.btn단가우선순위.Visible = true;
                    this.cbo단가유형.Visible = true;
                    this.lbl단가유형.Visible = true;
                }
                if (Config.MA_ENV.PJT형여부 == "Y")
                    this.btn프로젝트.Visible = true;
                if (BASIC.GetMAEXC("공장품목등록-규격형") == "100")
                    this.bStandard = true;
                this.InitEvent();
                if (!(Global.MainFrame.ServerKeyCommon == "ABOV"))
                    return;
                SetControl setControl = new SetControl();
                this.lbl환종.Visible = true;
                this.cbo환종.Visible = true;
                this.cur환종.Visible = true;
                this.cbo환종.SelectionChangeCommitted += new EventHandler(this.cbo_exch_SelectionChangeCommitted);
                setControl.SetCombobox(this.cbo환종, MA.GetCode("MA_B000005"));
                this.tb_DT_IO.TextChanged += new EventHandler(this.tb_DT_IO_TextChanged);
                this.Set환율();
                this.btn환종적용.Click += new EventHandler(this.btn_exch_app_Click);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitControl()
        {
            try
            {
                DataSet comboData = this.GetComboData("NC;MA_PLANT", "NC;PU_C000021", "S;MA_B000004", "N;PU_C000001", "S;MA_B000066", "S;PU_C000080", "N;MA_B000005");
                this.cbo공장.DataSource = comboData.Tables[0];
                this.cbo공장.DisplayMember = "NAME";
                this.cbo공장.ValueMember = "CODE";
                if (D.GetString(Global.MainFrame.LoginInfo.CdPlant) != "")
                    this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
                else
                    this.cbo공장.SelectedIndex = 0;
                this._header.CurrentRow["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                this.tb_DT_IO.Mask = this.MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                this.tb_DT_IO.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
                this.tb_DT_IO.Text = this.MainFrameInterface.GetStringToday;
                DataTable code = MF.GetCode("NC;PU_C000021");
                if (Global.MainFrame.ServerKeyCommon == "DKTH")
                {
                    foreach (DataRow dataRow in MF.GetCode("PU_C000021").Select("ISNULL(CD_FLAG1,'') <> '1'"))
                        code.LoadDataRow(dataRow.ItemArray, true);
                }
                else
                {
                    foreach (DataRow dataRow in MF.GetCode("PU_C000021").Select("CD_FLAG1 = '2' OR ISNULL(CD_FLAG1,'') = ''"))
                        code.LoadDataRow(dataRow.ItemArray, true);
                }
                this.cbo대체유형.DataSource = code;
                this.cbo대체유형.DisplayMember = "NAME";
                this.cbo대체유형.ValueMember = "CODE";
                if (code.Rows.Count > 0)
                {
                    this.cbo대체유형.SelectedIndex = 0;
                    this._header.CurrentRow["FG_GI"] = this.cbo대체유형.SelectedValue.ToString();
                }
                this.cbo단가유형.DataSource = comboData.Tables[3];
                this.cbo단가유형.DisplayMember = "NAME";
                this.cbo단가유형.ValueMember = "CODE";
                this.cbo환종.DataSource = comboData.Tables[6];
                this.cbo환종.DisplayMember = "NAME";
                this.cbo환종.ValueMember = "CODE";
                this._flex.SetDataMap("UNIT_PO", comboData.Tables[2], "CODE", "NAME");
                this._flex.SetDataMap("UNIT_IM", comboData.Tables[2], "CODE", "NAME");
                this._flex.SetDataMap("GRP_MFG", comboData.Tables[4], "CODE", "NAME");
                this._flex.SetDataMap("FG_GUBUN", comboData.Tables[5], "CODE", "NAME");
                if (Global.MainFrame.ServerKeyCommon == "EDIYA")
                    this.bpPanelControl4.Visible = false;
                UGrant ugrant = new UGrant();
                this.btnBOM적용.Visible = ugrant.GrantButton("P_PU_CGI_REG", "BOM");
                this.btn입고적용.Visible = ugrant.GrantButton("P_PU_CGI_REG", "IN");
                this.btn재고적용.Visible = ugrant.GrantButton("P_PU_CGI_REG", "STOCK");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_ITEM", "품목코드", 120, true);
            this._flex.SetCol("NM_ITEM", "품목명", 150, false);
            this._flex.SetCol("NO_DESIGN", "품목도면번호", 100, false);
            this._flex.SetCol("STND_ITEM", "규격", 80, false);
            this._flex.SetCol("UNIT_IM", "재고단위", 100, false);
            this._flex.SetCol("UNIT_PO", "발주단위", 100, false);
            this._flex.SetCol("NM_ITEMGRP", "품목군", 100, false);
            this._flex.SetCol("FG_SERNO", "S/N,LOT관리", 100, false);
            this._flex.SetCol("NO_LOT", "LOT여부", 100, false);
            this._flex.SetCol("NO_SERL", "S/N여부", 70, false);
            this._flex.SetCol("QT_GOOD_INV", "양품수량(재고)", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_UNIT_MM", "양품수량(발주단위)", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("CD_SL", "창고", 80, true);
            this._flex.SetCol("NM_SL", "창고명", 100, false);
            if (Global.MainFrame.ServerKeyCommon == "KIHA")
                this._flex.SetCol("QT_EMP", "인원", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("NO_ISURCV", "요청번호", 120, true);
            this._flex.SetCol("CD_PJT", "프로젝트코드", 120, true);
            this._flex.SetCol("NM_PROJECT", "프로젝트명", 120, false);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flex.SetCol("NO_CBS", "CBS번호", 140, true, typeof(string));
                this._flex.SetCol("NO_WBS", "WBS번호", 140, false, typeof(string));
                this._flex.SetCol("NO_LINE_PJTBOM", "프로젝트BOM항번", 140, false, typeof(decimal));
                this._flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트 항번", 100, false);
                this._flex.SetCol("CD_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 120, true);
                this._flex.SetCol("NM_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 120, false);
                this._flex.SetCol("NO_DESIGN_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 품목도면번호", 140, false, typeof(string));
                this._flex.SetCol("STND_UNIT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 120, false);
                if (!App.SystemEnv.PMS사용)
                {
                    this._flex.SetCol("CD_COST", "원가코드", 140, false, typeof(string));
                    this._flex.SetCol("NM_COST", "원가명", 140, false, typeof(string));
                }
            }
            this._flex.SetCol("GIR_NM_DEPT", "요청부서", 120, false);
            this._flex.SetCol("GIR_NM_KOR", "요청자", 120, false);
            this._flex.SetCol("UM_EX", "단가", 120, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flex.SetCol("UM_EX_PSO", "단가(수배단위)", 120, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flex.SetCol("AM", "원화금액", 120, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_EX", "외화금액", 120, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            if (this.bUseinv == "000")
                this._flex.SetCol("QT_INV", "현재고", 120, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("DC_RMK", "비고", 100, true, typeof(string));
            this._flex.SetCol("BARCODE", "BARCODE", 100, false);
            this._flex.SetCol("CD_CC", "C/C코드", 100, true, typeof(string));
            this._flex.SetCol("NM_CC", "C/C명", 100, true, typeof(string));
            this._flex.SetCol("STND_DETAIL_ITEM", "세부규격", 80, false);
            if (Global.MainFrame.ServerKeyCommon == "DAEJOOKC")
            {
                this._flex.SetCol("CD_USERDEF2", "설비코드", 100, false);
                this._flex.SetCol("NM_USERDEF1", "설비명", 100, false);
            }
            if (Global.MainFrame.ServerKeyCommon == "EDIYA" || BASIC.GetMAEXC_Menu("P_PU_CGI_REG", "PU_A00000022") == "100" || Global.MainFrame.ServerKeyCommon == "119" || Global.MainFrame.ServerKeyCommon == "49")
            {
                this._flex.SetCol("CD_PARTNER", "거래처", 100, true);
                this._flex.SetCol("LN_PARTNER", "거래처명", 100, true);
                this._flex.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }
                                                                                                    , new string[] { "CD_PARTNER", "LN_PARTNER" }, ResultMode.FastMode);
            }
            this._flex.SetCol("CD_ZONE", "품목 Location ", false);
            this._flex.SetCol("GRP_MFG", "제품군", 80, false);
            this._flex.SetCol("MAT_ITEM", "재질", 80, false);
            this._flex.SetCol("NM_PARTNER", "주거래처", 80, false);
            if (MA.ServerKey(false, "KMI"))
                this._flex.SetCol("QT_REMAIN", "잔여재고", 120, false, CheckTypeEnum.NONE, true, 17, typeof(decimal), FormatTpType.QUANTITY);
            else
                this._flex.SetCol("QT_REMAIN", "잔여재고", 120, false, CheckTypeEnum.NONE, false, 17, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("FG_GUBUN", "요청구분", 100, false);
            if (!this.bStandard)
            {
                this._flex.SetCol("WEIGHT", "단위중량", 100, false, CheckTypeEnum.NONE, false, 21, typeof(decimal), FormatTpType.MONEY);
                this._flex.SetCol("QT_WEIGHT", "총중량", 100, false, CheckTypeEnum.NONE, false, 21, typeof(decimal), FormatTpType.MONEY);
            }
            if (Global.MainFrame.ServerKeyCommon == "TSBK")
            {
                this._flex.SetCol("TXT_USERDEF1_QTIO", "LOT번호", 100, true);
                this._flex.SetCol("TXT_USERDEF2_QTIO", "파렛트 번호", 100, true);
            }
            this._flex.SetCol("EN_ITEM", "품목명(영)", false);
            this._flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                             "NM_ITEM",
                                                                                                             "STND_ITEM",
                                                                                                             "UNIT_IM",
                                                                                                             "NO_LOT",
                                                                                                             "CD_SL",
                                                                                                             "NM_SL",
                                                                                                             "NM_ITEMGRP",
                                                                                                             "UNIT_PO" }
                                                                                            , new string[] { "CD_ITEM",
                                                                                                             "NM_ITEM",
                                                                                                             "STND_ITEM",
                                                                                                             "UNIT_IM",
                                                                                                             "FG_LOTNO",
                                                                                                             "CD_SL",
                                                                                                             "NM_SL",
                                                                                                             "GRP_ITEMNM",
                                                                                                             "UNIT_PO" }, ResultMode.SlowMode);
            this._flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }
                                                                                      , new string[] { "CD_SL", "NM_SL" });
            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                this._flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                        "NM_PROJECT",
                                                                                                        "SEQ_PROJECT",
                                                                                                        "CD_UNIT",
                                                                                                        "NM_UNIT",
                                                                                                        "STND_UNIT" }
                                                                                       , new string[] { "NO_PROJECT",
                                                                                                        "NM_PROJECT",
                                                                                                        "SEQ_PROJECT",
                                                                                                        "CD_PJT_ITEM",
                                                                                                        "NM_PJT_ITEM",
                                                                                                        "PJT_ITEM_STND" }
                                                                                       , new string[] { "CD_PJT",
                                                                                                        "NM_PROJECT",
                                                                                                        "SEQ_PROJECT",
                                                                                                        "CD_UNIT",
                                                                                                        "NM_UNIT",
                                                                                                        "STND_UNIT" }, ResultMode.FastMode);
                this._flex.SetCodeHelpCol("CD_UNIT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                         "NM_PROJECT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_UNIT",
                                                                                                         "NM_UNIT",
                                                                                                         "STND_UNIT" }
                                                                                        , new string[] { "NO_PROJECT",
                                                                                                         "NM_PROJECT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_PJT_ITEM",
                                                                                                         "NM_PJT_ITEM",
                                                                                                         "PJT_ITEM_STND" }
                                                                                        , new string[] { "CD_PJT",
                                                                                                         "NM_PROJECT",
                                                                                                         "SEQ_PROJECT",
                                                                                                         "CD_UNIT",
                                                                                                         "NM_UNIT",
                                                                                                         "STND_UNIT" }, ResultMode.FastMode);
            }
            else
                this._flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT", "NM_PROJECT" }
                                                                                       , new string[] { "NO_PROJECT", "NM_PROJECT" });
            this._flex.SetCodeHelpCol("CD_CC", HelpID.P_MA_CC_SUB, ShowHelpEnum.Always, new string[] { "CD_CC", "NM_CC" }
                                                                                      , new string[] { "CD_CC", "NM_CC" });
            this._flex.SetCodeHelpCol("NO_CBS", "H_PM_CBS_SUB", ShowHelpEnum.Always, new string[] { "NO_CBS", "CD_COST", "NM_COST" }
                                                                                   , new string[] { "NO_CBS", "CD_COST", "NM_COST" });
            this._flex.SetDummyColumn("S", "QT_TOTAL", "QT_GOOD", "QT_REJECT", "QT_INV");
            this._flex.SetExceptEditCol("NO_LOT", "NO_SERL", "FG_SERNO", "NM_ITEM", "STND_ITEM", "UNIT_IM", "NM_SL", "NO_ISURCV", "UM_EX", "AM", "QT_INV", "GRP_ITEMNM");
            this._flex.VerifyAutoDelete = new string[] { "CD_ITEM" };
            if (Global.MainFrame.ServerKeyCommon != "YWD" && (Config.MA_ENV.프로젝트사용 || Config.MA_ENV.PJT형여부 == "Y"))
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                    this._flex.VerifyNotNull = new string[] { "CD_ITEM",
                                                              "CD_SL",
                                                              "NM_SL",
                                                              "CD_PJT",
                                                              "SEQ_PROJECT" };
                else if (BASIC.GetMAEXC_Menu("P_PU_CGI_REG", "PU_A00000012") == "100")
                    this._flex.VerifyNotNull = new string[] { "CD_ITEM",
                                                              "CD_SL",
                                                              "NM_SL",
                                                              "CD_PJT",
                                                              "CD_CC" };
                else
                    this._flex.VerifyNotNull = new string[] { "CD_ITEM",
                                                              "CD_SL",
                                                              "NM_SL",
                                                              "CD_PJT" };
            }
            else if (BASIC.GetMAEXC_Menu("P_PU_CGI_REG", "PU_A00000012") == "100")
                this._flex.VerifyNotNull = new string[] { "CD_ITEM",
                                                          "CD_SL",
                                                          "NM_SL",
                                                          "CD_CC" };
            else
                this._flex.VerifyNotNull = new string[] { "CD_ITEM",
                                                          "CD_SL",
                                                          "NM_SL" };
            if (this._USER != null)
                this._USER.SetCol(this._flex);
            Config.UserColumnSetting.InitGrid_UserMenu(this._flex, this.PageID, true);
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex.VerifyCompare(this._flex.Cols["QT_GOOD_INV"], 0, OperatorEnum.Greater);
            this._flex.VerifyCompare(this._flex.Cols["QT_GOOD_INV"], 0, OperatorEnum.Greater);
            this._flex.AddMyMenu = true;
            this._flex.EnterKeyAddRow = true;
            this._flex.AddMenuSeperator();
            this._flex.AddMenuItem(this._flex.AddPopup("관련 현황"), "현재고조회", new EventHandler(this.Menu_Click));
            ToolStripMenuItem parent = this._flex.AddPopup("엑셀관리");
            this._flex.AddMenuItem(parent, "파일생성", new EventHandler(this.Menu_Click));
            this._flex.AddMenuItem(parent, "파일업로드", new EventHandler(this.Menu_Click));
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();
                DataSet dataSet = this._biz.Search(string.Empty);
                this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
                this._header.ClearAndNewRow();
                this.InitControl();
                this.bp_CD_SL.CodeValue = string.Empty;
                this.bp_CD_SL.CodeName = string.Empty;
                this.수불형태Default셋팅();
                this._flex.Binding = dataSet.Tables[1];
                if (this.fg_gubun == "재고자산수불부")
                    this.화면이동();
                this.tb_DT_IO.Focus();
                this.oneGrid1.UseCustomLayout = true;
                this.bpPanelControl1.IsNecessaryCondition = true;
                this.bpPanelControl2.IsNecessaryCondition = true;
                this.bpPanelControl3.IsNecessaryCondition = true;
                this.bpPanelControl6.IsNecessaryCondition = true;
                this.oneGrid1.IsSearchControl = false;
                this.oneGrid1.InitCustomLayout();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);
            this._header.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
            this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);

            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flex.AfterCodeHelp += new AfterCodeHelpEventHandler(this.Grid_AfterCodeHelp);
            this._flex.ValidateEdit += new ValidateEditEventHandler(this.Grid_ValidateEdit);
            this._flex.AddRow += new EventHandler(this.btn_insert_Click);

            this.cbo공장.SelectionChangeCommitted += new EventHandler(this.DropDownComboBox_SelectionChangeCommitted);
            this.btn프로젝트.Click += new EventHandler(this.btn프로젝트_Click);
            this.ctx담당자.QueryAfter += new BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            this.btn시리얼정보.Click += new EventHandler(this.m_btnSerlView_Click);
            this.btn재고적용.Click += new EventHandler(this.Appstock_Click);
            this.btn엑셀업로드.Click += new EventHandler(this._btn엑셀_Click);
            this.btn삭제.Click += new EventHandler(this.btn_delete_Click);
            this.btn추가.Click += new EventHandler(this.btn_insert_Click);
            this.ctx수불형태.CodeChanged += new EventHandler(this.tb_CD_QTIO_CodeChanged);
            this.ctx수불형태.QueryAfter += new BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            this.ctx수불형태.QueryBefore += new BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            this.cbo공장.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.cbo대체유형.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.txt비고.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.btnCC적용.Click += new EventHandler(this.btn_AppCC_Click);
            this.btn_창고적용.Click += new EventHandler(this.btn_창고적용_Click);
            this.btn프로젝트적용.Click += new EventHandler(this.btn_프로젝트적용_Click);
            this.btnBOM적용.Click += new EventHandler(this.btn_APP_Click);
            this.btn입고적용.Click += new EventHandler(this.btn입고적용_Click);
            this.btn첨부파일.Click += new EventHandler(this.btn_FILE_UPLOAD_Click);
            this.btn단가우선순위.Click += new EventHandler(this.btn_UM_APP_Click);
            this.btn요청적용.Click += new EventHandler(this.btn_RE_PR_Click);
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData())
                    return false;
                if ((this._flex == null || this._flex.Rows.Count <= 0) && this._header.CurrentRow.RowState == DataRowState.Added)
                {
                    this.ShowMessage(공통메세지.변경된내용이없습니다, "");
                    return false;
                }
                if (Global.MainFrame.ServerKeyCommon.Contains("JCK") && D.GetString(this.cbo대체유형.SelectedValue) == "000")
                {
                    this.ShowMessage("계정대체 유형이 ** 아래 유형 꼭 선택 **은 저장할 수 없습니다.");
                    return false;
                }
                string seq = D.GetString(this.txt수불번호.Text);
                if (this.추가모드여부)
                {
                    if (seq == "")
                        seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "19", this.tb_DT_IO.Text.Substring(0, 6));
                    this._header.CurrentRow["NO_IO"] = seq;
                    if (this._flex.HasNormalRow)
                    {
                        int num = 0;
                        foreach (DataRow row in this._flex.DataTable.Rows)
                        {
                            row["NO_IO"] = seq;
                            row["NO_IOLINE"] = ++num;
                            row["CD_EXCH"] = D.GetString(this.cbo환종.SelectedValue) == string.Empty ? "000" : D.GetString(this.cbo환종.SelectedValue);
                            row["RT_EXCH"] = (D.GetDecimal(this.cur환종.DecimalValue) == 0M ? 1M : D.GetDecimal(this.cur환종.DecimalValue));
                        }
                    }
                }
                if (D.GetString(this.bp_CD_SL.CodeValue) != "")
                {
                    for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
                    {
                        if (this._flex[row, "CD_SL"].ToString() == string.Empty || this._flex[row, "CD_SL"].ToString() == null)
                        {
                            this._flex[row, "CD_SL"] = D.GetString(this.bp_CD_SL.CodeValue);
                            this._flex[row, "NM_SL"] = D.GetString(this.bp_CD_SL.CodeName);
                            this._flex[row, "CD_EXCH"] = D.GetString(this.cbo환종.SelectedValue) == string.Empty ? "000" : D.GetString(this.cbo환종.SelectedValue);
                            this._flex[row, "RT_EXCH"] = (D.GetDecimal(this.cur환종.DecimalValue) == 0M ? 1M : D.GetDecimal(this.cur환종.DecimalValue));
                        }
                    }
                }
                if (Global.MainFrame.ServerKey == "FJK")
                {
                    foreach (DataRow dataRow in this._flex.DataTable.Select("", "", DataViewRowState.CurrentRows))
                        dataRow["FG_TPIO"] = this._header.CurrentRow["FG_GI"].ToString();
                }
                if (!this.추가모드여부)
                {
                    seq = D.GetString(this.txt수불번호.Text);
                    decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
                    foreach (DataRow row in this._flex.DataTable.Rows)
                    {
                        if (row.RowState == DataRowState.Added)
                        {
                            row["NO_IO"] = seq;
                            ++maxValue;
                            row["NO_IOLINE"] = maxValue;
                        }
                    }
                }
                if (BASIC.GetMAEXC_Menu("P_PU_CGI_REG", "PU_A00000022") == "100")
                    this._header.CurrentRow["CD_PARTNER"] = "";
                DataTable changes1 = this._header.GetChanges();
                DataTable changes2 = this._flex.GetChanges();
                DataTable dtLOT = null;
                DataTable dtSERL = null;
                if (changes2 != null)
                {
                    DataRow[] dataRowArray = changes2.Select("NO_LOT = 'YES'", "", DataViewRowState.Added);
                    DataTable dt = changes2.Clone();
                    if (dataRowArray.Length > 0)
                    {
                        foreach (DataRow row in dataRowArray)
                            dt.ImportRow(row);
                        if (MA.ServerKey(false, "MANYO"))
                        {
                            P_PU_Z_MANYO_LOT_SUB_I pPuZManyoLotSubI = new P_PU_Z_MANYO_LOT_SUB_I(dt, "N");
                            if (pPuZManyoLotSubI.ShowDialog((IWin32Window)this) == DialogResult.OK)
                            {
                                dtLOT = pPuZManyoLotSubI.dtL;
                                foreach (DataRow row in dtLOT.Rows)
                                    row["FG_IO"] = this._header.CurrentRow["FG_IO"];
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
                        else
                        {
                            P_PU_LOT_SUB_I pPuLotSubI = new P_PU_LOT_SUB_I(dt, "N");
                            if (pPuLotSubI.ShowDialog((IWin32Window)this) == DialogResult.OK)
                            {
                                dtLOT = pPuLotSubI.dtL;
                                foreach (DataRow row in dtLOT.Rows)
                                    row["FG_IO"] = this._header.CurrentRow["FG_IO"];
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
                }
                if (string.Compare(this.MNG_SERIAL, "Y") == 0 && changes2 != null)
                {
                    DataRow[] dataRowArray = changes2.Select("NO_SERL = 'YES'", "", DataViewRowState.Added);
                    DataTable dt = changes2.Clone();
                    if (dataRowArray.Length > 0)
                    {
                        foreach (DataRow row in dataRowArray)
                            dt.ImportRow(row);
                        P_PU_SERL_SUB_I pPuSerlSubI = new P_PU_SERL_SUB_I(dt);
                        if (pPuSerlSubI.ShowDialog((IWin32Window)this) == DialogResult.OK)
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
                DataTable dtLOCATION = null;
                if (Config.MA_ENV.YN_LOCATION == "Y")
                {
                    bool b_return = false;
                    DataTable dt = changes2.Clone().Copy();
                    foreach (DataRow dataRow in changes2.Select())
                        dt.LoadDataRow(dataRow.ItemArray, true);
                    dtLOCATION = P_OPEN_SUBWINDOWS.P_MA_LOCATION_I_SUB(dt, out b_return);
                    if (!b_return)
                        return false;
                }
                if (!this._biz.Save(changes1, changes2, dtLOT, dtSERL, dtLOCATION))
                    return false;
                if (this.추가모드여부)
                    this.txt수불번호.Text = seq;
                this._header.AcceptChanges();
                this._flex.AcceptChanges();
                if (MA.ServerKey(false, "GALAXIA", "DAEJOOKC", "SEEGENE"))
                    this.cbo대체유형.Enabled = true;
                else
                    this.cbo대체유형.Enabled = false;
                this.tb_DT_IO.Enabled = false;
                this.tb_DT_IO.Focus();
                return true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return false;
        }

        public bool Check() => ComFunc.NullCheck(new Hashtable()
        {
            {
                this.cbo공장,
                this.lbl공장
            }
        });

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch() || !this.Check())
                    return;
                this.tb_DT_IO.Focus();
                DataSet pds_Combo = new DataSet();
                P_PU_CGI_SUB pPuCgiSub = new P_PU_CGI_SUB(this.MainFrameInterface, D.GetString(this.bp_CD_SL.CodeValue), D.GetString(this.bp_CD_SL.CodeName), this.ctx담당자.CodeValue, this.ctx담당자.CodeName, pds_Combo, D.GetString(this.cbo공장.SelectedValue));
                if (pPuCgiSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    DataSet dataSet = this._biz.Search(pPuCgiSub.m_SelecedRow["NO_IO"].ToString());
                    this._header.SetDataTable(dataSet.Tables[0]);
                    this._flex.Binding = dataSet.Tables[1];
                    this.ctx프로젝트.CodeValue = D.GetString(dataSet.Tables[0].Rows[0]["CD_PJT"]);
                    this.ctx프로젝트.CodeName = D.GetString(dataSet.Tables[0].Rows[0]["NM_PROJECT"]);
                    this.bp_CD_SL.CodeValue = this._flex.DataTable.Rows[0]["CD_SL"].ToString();
                    this.bp_CD_SL.CodeName = this._flex.DataTable.Rows[0]["NM_SL"].ToString();
                    if (this._flex.DataTable.Rows.Count > 0)
                    {
                        this.btn추가.Enabled = true;
                        this.btn요청적용.Enabled = false;
                    }
                    else
                    {
                        this.btn추가.Enabled = true;
                        this.btn요청적용.Enabled = true;
                    }
                    this.fg_gubun = string.Empty;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.ShowStatusBarMessage(0);
            }
        }

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete())
                return false;
            string maexc = BASIC.GetMAEXC("POP연동");
            if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까, this.PageName) != DialogResult.Yes)
                return false;
            string str = Global.MainFrame.LoginInfo.CompanyCode.ToString();
            if (maexc != "000")
            {
                DataTable dataTable = DBHelper.GetDataTable("SELECT NO_POP FROM MM_QTIOH WHERE CD_COMPANY = '" + str + "' AND NO_IO = '" + this.txt수불번호.Text + "'");
                if (dataTable.Rows.Count != 0 && D.GetString(dataTable.Rows[0]["NO_POP"]) != string.Empty && this.ShowMessage("타 시스템으로부터 연동된 처리건입니다. 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                    return false;
            }
            if (MA.ServerKey(false, "AUTON"))
            {
                DataTable dataTable = DBHelper.GetDataTable("SELECT NO_POP FROM MM_QTIO WHERE CD_COMPANY = '" + str + "' AND NO_IO = '" + this.txt수불번호.Text + "'");
                if (dataTable.Rows.Count != 0 && !this.chk_POP_AUTON(dataTable.Select()))
                    return false;
            }
            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete())
                    return;
                this._biz.Delete(this.txt수불번호.Text);
                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                this._header.AcceptChanges();
                this._flex.AcceptChanges();
                this.OnToolBarAddButtonClicked(sender, e);
                this.btn요청적용.Enabled = true;
                this.btn삭제.Enabled = false;
                this.tb_DT_IO.Enabled = true;
                this.SetButtonState(true);
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
                if (!this.BeforeSave())
                    return;
                this.ToolBarSaveButtonEnabled = false;
                if (this.MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(PageResultMode.SaveGood);
                    this.SetButtonState(false);
                    this.btn삭제.Enabled = true;
                    this.btn요청적용.Enabled = false;
                }
                else
                    this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                this.ToolBarSaveButtonEnabled = true;
            }
        }

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave())
                return false;
            if (!this._flex.HasNormalRow)
            {
                this.ShowMessage(메세지.라인테이터가한건이상존재해야저장할수있습니다, "");
                return false;
            }
            if (!this.CheckFieldHead(null))
                return false;
            return (!MA.ServerKey(false, "KYOTECH") || this.ChkAM_KYOTECH()) && (this._USER == null || this._USER.SaveCheck(this._flex, this.cbo대체유형, "")) && this.Verify();
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeAdd())
                    return;
                this._flex.DataTable.Rows.Clear();
                this._flex.AcceptChanges();
                this._header.ClearAndNewRow();
                this.bp_CD_SL.Clear();
                this.ctx프로젝트.Clear();
                this.ctxCC.Clear();
                this.btn요청적용.Enabled = true;
                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = false;
                this.btn요청적용.Enabled = true;
                this.SetButtonState(true);
                this.InitControl();
                this.수불형태Default셋팅();
            }
            finally
            {
                this._flex.Focus();
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (this.추가모드여부)
                    return;
                string systemCode;
                string objectName;
                if (this.MNG_LOT == "N")
                {
                    systemCode = "R_PU_CGI_REG_0";
                    objectName = "계정대체출고전표";
                }
                else
                {
                    systemCode = "R_PU_CGI_REG_1";
                    objectName = "계정대체출고전표-LOT사용";
                }
                ReportHelper reportHelper = new ReportHelper(systemCode, objectName);
                reportHelper.SetData("전표번호", this.txt수불번호.Text);
                reportHelper.SetData("출고일자", this.tb_DT_IO.Text.Substring(0, 4) + "/" + this.tb_DT_IO.Text.Substring(4, 2) + "/" + this.tb_DT_IO.Text.Substring(6, 2));
                reportHelper.SetData("공장코드", D.GetString(this.cbo공장.SelectedValue));
                reportHelper.SetData("공장명", this.cbo공장.Text);
                reportHelper.SetData("사원코드", this.ctx담당자.CodeValue);
                reportHelper.SetData("사원명", this.ctx담당자.CodeName);
                reportHelper.SetData("수불구분", this.ctx수불형태.CodeName);
                reportHelper.SetData("거래처코드", this._header.CurrentRow["CD_PARTNER"].ToString());
                reportHelper.SetData("거래처명", this._header.CurrentRow["LN_PARTNER"].ToString());
                reportHelper.SetData("부서코드", this._header.CurrentRow["CD_DEPT"].ToString());
                reportHelper.SetData("부서명", this._header.CurrentRow["NM_DEPT"].ToString());
                reportHelper.SetData("대체유형", this.cbo대체유형.Text);
                reportHelper.SetData("PROJECT", this.ctx프로젝트.CodeValue);
                reportHelper.SetData("비고", this.txt비고.Text);
                DataTable dt = new DataTable();
                if (Global.MainFrame.ServerKeyCommon == "KINTEC")
                    dt = this._biz.Search(this.txt수불번호.Text).Tables[1];
                else if (this.MNG_LOT == "N")
                    dt = this._flex.DataTable;
                else if (this.MNG_LOT == "Y")
                    dt = this._biz.SearchPrint(this.txt수불번호.Text).Tables[0];
                if (Global.MainFrame.ServerKeyCommon == "ALOE")
                    dt = this._biz.SearchPrint(this.txt수불번호.Text).Tables[0];
                else if (Global.MainFrame.ServerKeyCommon == "YDGLS")
                    dt = this._biz.SearchPrint(this.txt수불번호.Text).Tables[0];
                DataSet dataSet = this._biz.SEARCH_PARTNER_DATA(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                               D.GetString(this.cbo공장.SelectedValue),
                                                                               this._header.CurrentRow["CD_PARTNER"].ToString() });
                if (dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    string str = dataSet.Tables[0].Rows[0]["NO_COMPANY"].ToString();
                    if (str.Length == 10)
                        str = str.Substring(0, 3) + "-" + str.Substring(3, 2) + "-" + str.Substring(5, 5);
                    reportHelper.SetData("거래처_등록번호", str);
                    reportHelper.SetData("거래처_상호", dataSet.Tables[0].Rows[0]["LN_PARTNER"].ToString());
                    reportHelper.SetData("거래처_성명", dataSet.Tables[0].Rows[0]["NM_CEO"].ToString());
                    reportHelper.SetData("거래처_주소", dataSet.Tables[0].Rows[0]["PARTNER_ADS"].ToString());
                    reportHelper.SetData("거래처_전화번호", dataSet.Tables[0].Rows[0]["NO_TEL"].ToString());
                    reportHelper.SetData("거래처_FAX", dataSet.Tables[0].Rows[0]["NO_FAX"].ToString());
                    reportHelper.SetData("거래처_업태", dataSet.Tables[0].Rows[0]["TP_JOB"].ToString());
                    reportHelper.SetData("거래처_종목", dataSet.Tables[0].Rows[0]["CLS_JOB"].ToString());
                }
                if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                {
                    string str = dataSet.Tables[1].Rows[0]["NO_BIZAREA"].ToString();
                    if (str.Length == 10)
                        str = str.Substring(0, 3) + "-" + str.Substring(3, 2) + "-" + str.Substring(5, 5);
                    reportHelper.SetData("사업장_등록번호", str);
                    reportHelper.SetData("사업장_상호", dataSet.Tables[1].Rows[0]["NM_BIZAREA"].ToString());
                    reportHelper.SetData("사업장_성명", dataSet.Tables[1].Rows[0]["NM_MASTER"].ToString());
                    reportHelper.SetData("사업장_주소", dataSet.Tables[1].Rows[0]["BIZ_ADS"].ToString());
                    reportHelper.SetData("사업장_업태", dataSet.Tables[1].Rows[0]["TP_JOB"].ToString());
                    reportHelper.SetData("사업장_종목", dataSet.Tables[1].Rows[0]["CLS_JOB"].ToString());
                }
                reportHelper.SetDataTable(dt);
                reportHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            int num = string.Compare(this.MNG_LOT, "Y") != 0 ? 1 : (this._flex.Cols[e.Col].Name == "QT_GOOD_INV" || this._flex.Cols[e.Col].Name == "QT_UNIT_MM" ? 0 : (!(this._flex.Cols[e.Col].Name == "CD_SL") ? 1 : 0));
            e.Cancel = num == 0 && !this._flex.IsAddedRow(this._flex.Row, true);
            switch (this._flex.Cols[e.Col].Name)
            {
                case "S":
                    e.Cancel = false;
                    break;
                case "CD_ITEM":
                    if (this._flex.RowState() == DataRowState.Added && !(D.GetString(this._flex["NO_ISURCV"]) != ""))
                        break;
                    e.Cancel = true;
                    break;
                case "CD_PJT":
                case "CD_UNIT":
                    e.Cancel = false;
                    break;
                case "SEQ_PROJECT":
                    e.Cancel = true;
                    break;
                case "NO_CBS":
                    if (!(D.GetDecimal(this._flex["NO_LINE_PJTBOM"]) > 0M))
                        break;
                    e.Cancel = true;
                    break;
            }
        }

        private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                decimal num1;
                if (e.StartEditCancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    switch (e.Parameter.HelpID)
                    {
                        case HelpID.P_USER:
                            if (D.GetString(this._flex["CD_PJT"]) != string.Empty)
                                e.Parameter.P64_CODE4 = D.GetString(this._flex["CD_PJT"]);
                            if (this._flex.Cols[e.Col].Name == "NO_CBS")
                            {
                                if (D.GetDecimal(this._flex["NO_LINE_PJTBOM"]) > 0M)
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                if (D.GetString(this._flex["CD_PJT"]) == string.Empty)
                                {
                                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("프로젝트"));
                                    e.Cancel = true;
                                }
                                e.Parameter.P61_CODE1 = "|";
                                e.Parameter.UserParams = D.GetString(this._flex["CD_PJT"]) + "|";
                                break;
                            }
                            break;
                        case HelpID.P_MA_PITEM_SUB1:
                            if (this._flex.RowState() != DataRowState.Added)
                            {
                                e.Cancel = true;
                                break;
                            }
                            if (D.GetString(this.cbo공장.SelectedValue) == "")
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                                this.cbo공장.Focus();
                                break;
                            }
                            e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                            e.Parameter.ResultMode = ResultMode.SlowMode;
                            num1 = 0M;
                            this._flex["QT_INV"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex["CD_SL"]), D.GetString(this._flex["CD_ITEM"]))));
                            break;
                        case HelpID.P_MA_SL_SUB:
                            if (this._flex.RowState() != DataRowState.Added)
                            {
                                e.Cancel = true;
                                break;
                            }
                            if (D.GetString(this.cbo공장.SelectedValue).Equals(""))
                            {
                                e.Cancel = true;
                            }
                            else
                            {
                                e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                                e.Parameter.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                                if (D.GetString(this._flex["CD_ITEM"]) != "")
                                {
                                    num1 = 0M;
                                    this._flex["QT_INV"] = D.GetDecimal((!(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(e.EditValue), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(e.EditValue), D.GetString(this._flex["CD_ITEM"]))));
                                }
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

        private void Grid_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                bool flag1 = true;
                DataTable dataTable = this._flex.DataTable;
                HelpReturn result = e.Result;
                DataRow[] rows = result.Rows;
                bool flag2 = false;
                decimal num;
                switch (e.Result.HelpID)
                {
                    case HelpID.P_USER:
                        if (this._flex.Cols[e.Col].Name == "CD_PJT" && this._USER != null)
                        {
                            this._USER.Grid_AfterCodeHelp(this._flex, e.Result.CodeValue);
                            break;
                        }
                        break;
                    case HelpID.P_MA_PITEM_SUB1:
                        if (result.DialogResult != DialogResult.OK)
                            break;
                        this._flex.Redraw = false;
                        if (this._flex.Row == 1)
                            this._flex.Row = 2;
                        int row1 = this._flex.Row;
                        foreach (DataRow row2 in result.Rows)
                        {
                            if (!this._flex[this._flex.Rows.Count - 1, "CD_ITEM"].ToString().Equals("") || flag1)
                            {
                                if (flag1)
                                    this._flex.RemoveItem(e.Row);
                                DataRow row3 = dataTable.NewRow();
                                dataTable.Rows.Add(row3);
                            }
                            this._flex[row1, "CD_ITEM"] = row2["CD_ITEM"];
                            this._flex[row1, "NM_ITEM"] = row2["NM_ITEM"];
                            this._flex[row1, "STND_ITEM"] = row2["STND_ITEM"];
                            this._flex[row1, "UNIT_IM"] = row2["UNIT_IM"];
                            this._flex[row1, "BARCODE"] = row2["BARCODE"];
                            this._flex[row1, "UNIT_PO"] = row2["UNIT_PO"];
                            this._flex[row1, "NO_LOT"] = row2["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
                            if (row2["FG_SERNO"].ToString() == "002")
                                this._flex[row1, "FG_SERNO"] = "LOT";
                            this._flex[row1, "NO_SERL"] = row2["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
                            if (row2["FG_SERNO"].ToString() == "003")
                                this._flex[row1, "FG_SERNO"] = "S/N";
                            if (row2["FG_SERNO"].ToString() == "001")
                                this._flex[row1, "FG_SERNO"] = "미관리";
                            this._flex[row1, "NM_ITEMGRP"] = row2["GRP_ITEMNM"];
                            if (D.GetString(this.bp_CD_SL.CodeValue) != "")
                            {
                                this._flex[row1, "CD_SL"] = D.GetString(this.bp_CD_SL.CodeValue);
                                this._flex[row1, "NM_SL"] = D.GetString(this.bp_CD_SL.CodeName);
                            }
                            else
                            {
                                this._flex[row1, "CD_SL"] = D.GetString(row2["CD_GISL"]);
                                this._flex[row1, "NM_SL"] = D.GetString(row2["NM_GISL"]);
                            }
                            this._flex[row1, "NO_IO"] = this._header.CurrentRow["NO_IO"];
                            this._flex[row1, "YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                            this._flex[row1, "CD_PLANT"] = this.cbo공장.SelectedValue;
                            this._flex[row1, "DT_IO"] = this._header.CurrentRow["DT_IO"];
                            this._flex[row1, "NO_EMP"] = this._header.CurrentRow["NO_EMP"];
                            this._flex[row1, "CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                            this._flex[row1, "NM_QTIOTP"] = this._header.CurrentRow["NM_QTIOTP"];
                            this._flex[row1, "CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"];
                            this._flex[row1, "LN_PARTNER"] = this._header.CurrentRow["LN_PARTNER"];
                            this._flex[row1, "YN_AM"] = this._header.CurrentRow["YN_AM"];
                            this._flex[row1, "CD_PJT"] = this.ctx프로젝트.CodeValue;
                            this._flex[row1, "NM_PROJECT"] = this.ctx프로젝트.CodeName;
                            this._flex[row1, "FG_TPIO"] = this._header.CurrentRow["FG_GI"];
                            this._flex[row1, "FG_IO"] = this._header.CurrentRow["FG_IO"];
                            this._flex[row1, "FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                            this._flex[row1, "UNIT_PO_FACT"] = (D.GetDecimal(row2["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row2["UNIT_PO_FACT"]));
                            this._flex[row1, "CD_ZONE"] = D.GetString(row2["CD_ZONE"]);
                            this._flex[row1, "STND_DETAIL_ITEM"] = row2["STND_DETAIL_ITEM"];
                            this._flex[row1, "WEIGHT"] = D.GetDecimal(row2["WEIGHT"]);
                            this._flex[row1, "MAT_ITEM"] = row2["MAT_ITEM"];
                            this._flex[row1, "GRP_MFG"] = row2["GRP_MFG"];
                            this._flex[row1, "NM_PARTNER"] = row2["LN_PARTNER"];
                            this._flex[row1, "EN_ITEM"] = row2["EN_ITEM"];
                            num = 0M;
                            decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[row1, "CD_SL"]), D.GetString(this._flex[row1, "CD_ITEM"]), D.GetString(this._flex[row1, "CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[row1, "SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[row1, "CD_SL"]), D.GetString(this._flex[row1, "CD_ITEM"]));
                            this._flex[row1, "QT_INV"] = D.GetDecimal(val);
                            this._flex[row1, "QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row1, "QT_GOOD_INV"]) * D.GetDecimal(row2["WEIGHT"]));
                            this._flex[row1, "QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(val) - D.GetDecimal(this._flex[row1, "QT_GOOD_INV"]));
                            if (this._USER != null)
                                this._USER.setApp(this._flex, ComUser.Flag.indexFocus, row1, this._USER.FG_PJT1);
                            flag2 = true;
                            flag1 = false;
                            ++row1;
                        }
                        if (this.bUmSetting)
                            this.SETUM();
                        this._flex.Select(e.Row, this._flex.Cols.Fixed);
                        this._flex.Redraw = true;
                        this.Page_DataChanged(null, null);
                        this.btn요청적용.Enabled = true;
                        this.btn삭제.Enabled = true;
                        break;
                    case HelpID.P_MA_SL_SUB:
                        if (D.GetString(this._flex["CD_ITEM"]) != "")
                        {
                            this._flex["QT_INV"] = BASICPU.Item_PINVN(D.GetString(this.cbo공장.SelectedValue), D.GetString(e.Result.CodeValue), D.GetString(this._flex["CD_ITEM"]));
                            num = 0M;
                            decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(e.Result.CodeValue), D.GetString(this._flex["CD_ITEM"]), D.GetString(this._flex["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(e.Result.CodeValue), D.GetString(this._flex["CD_ITEM"]));
                            this._flex["QT_INV"] = D.GetDecimal(val);
                            this._flex["QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(val) - D.GetDecimal(this._flex["QT_GOOD_INV"]));
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

        private void Grid_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string targetValue = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                string editData = ((FlexGrid)sender).EditData;
                decimal num1 = 0M;
                if (!this.추가모드여부 && targetValue.ToUpper() == editData.ToUpper())
                    return;
                decimal num2 = Unit.환율(DataDictionaryTypes.PU, D.GetDecimal(this.cur환종.DecimalValue) == 0M ? 1M : D.GetDecimal(this.cur환종.DecimalValue));
                if (((C1FlexGridBase)sender).Cols[e.Col].Name == "QT_GOOD_INV")
                {
                    this._flex[this._flex.Row, "QT_GOOD_INV"] = this._flex.CDecimal(editData);
                    this._flex[this._flex.Row, "QT_GOOD_OLD"] = this._flex.CDecimal(targetValue);
                    this._flex[this._flex.Row, "QT_REJECT_OLD"] = this._flex[this._flex.Row, "QT_REJECT_INV"];
                    decimal num3 = D.GetDecimal(editData) + D.GetDecimal(this._flex["QT_REJECT_INV"]);
                    this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX"]) * num3 * num2);
                    this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX"]) * num3);
                    if (MA.ServerKey(false, "SAMWON"))
                    {
                        if (D.GetDecimal(this._flex[this._flex.Row, "QT_UNIT_MM"]) != 0M)
                            return;
                        if (D.GetDecimal(this._flex["NUM_USERDEF1"]) == 0M)
                            this._flex["NUM_USERDEF1"] = (D.GetDecimal(this._flex["WEIGHT"]) == 0M ? 0M : Math.Round(this._flex.CDecimal(editData) / D.GetDecimal(this._flex["WEIGHT"]), 0, MidpointRounding.AwayFromZero));
                    }
                    this._flex[this._flex.Row, "QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(editData) / (D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"])));
                    this._flex[this._flex.Row, "QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) * D.GetDecimal(this._flex[this._flex.Row, "WEIGHT"]));
                    this._flex[this._flex.Row, "QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_INV"]) - D.GetDecimal(editData));
                }
                if (((C1FlexGridBase)sender).Cols[e.Col].Name == "QT_REMAIN")
                    this._flex[this._flex.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_INV"]) - D.GetDecimal(editData));
                if (((C1FlexGridBase)sender).Cols[e.Col].Name == "QT_UNIT_MM")
                {
                    this._flex[e.Row, "QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(editData));
                    num1 = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(editData) * (D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"])));
                    if (MA.ServerKey(true, "SAMWON"))
                    {
                        if (D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]) != 0M)
                            return;
                        if (D.GetDecimal(this._flex["NUM_USERDEF1"]) == 0M)
                            this._flex["NUM_USERDEF1"] = (D.GetDecimal(this._flex["WEIGHT"]) == 0M ? 0M : Math.Round(this._flex.CDecimal(editData) / D.GetDecimal(this._flex["WEIGHT"]), 0, MidpointRounding.AwayFromZero));
                    }
                    this._flex[this._flex.Row, "QT_GOOD_OLD"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]));
                    this._flex[this._flex.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(editData) * (D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"])));
                    this._flex[this._flex.Row, "QT_REJECT_OLD"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_REJECT_INV"]));
                    decimal num4 = D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]) + D.GetDecimal(this._flex["QT_REJECT_INV"]);
                    this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX"]) * num4 * num2);
                    this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX"]) * num4);
                    this._flex[this._flex.Row, "QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]) * D.GetDecimal(this._flex[this._flex.Row, "WEIGHT"]));
                    this._flex[this._flex.Row, "QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_INV"]) - D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]));
                }
                else if (((C1FlexGridBase)sender).Cols[e.Col].Name == "UM_EX_PSO")
                {
                    decimal num5 = D.GetDecimal(this._flex["QT_GOOD_INV"]) + D.GetDecimal(this._flex["QT_REJECT_INV"]);
                    decimal val1 = D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"]);
                    decimal val = UDecimal.Getdivision(D.GetDecimal(editData), val1);
                    this._flex[this._flex.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, val);
                    this._flex[this._flex.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, val * num2);
                    this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, val * num5 * num2);
                    this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, val * num5);
                }
                else if (((C1FlexGridBase)sender).Cols[e.Col].Name == "AM_EX")
                {
                    decimal val1 = D.GetDecimal(this._flex["QT_GOOD_INV"]) + D.GetDecimal(this._flex["QT_REJECT_INV"]);
                    decimal val2 = D.GetDecimal(editData);
                    decimal val3 = UDecimal.Getdivision(val2, val1);
                    this._flex[this._flex.Row, "UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, Unit.외화단가(DataDictionaryTypes.PU, val3 * D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"])));
                    this._flex[this._flex.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, val3 * num2);
                    this._flex[this._flex.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, val3);
                    this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, val2 * num2);
                    this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, val2);
                }
                else if (((C1FlexGridBase)sender).Cols[e.Col].Name == "NUM_USERDEF1")
                {
                    if (MA.ServerKey(true, "SAMWON") && D.GetDecimal(this._flex["QT_GOOD_INV"]) == 0M && D.GetDecimal(this._flex["QT_UNIT_MM"]) == 0M)
                    {
                        this._flex[this._flex.Row, "QT_GOOD_OLD"] = this._flex[this._flex.Row, "QT_GOOD_INV"];
                        this._flex[this._flex.Row, "QT_GOOD_INV"] = Math.Round(D.GetDecimal(this._flex[this._flex.Row, "NUM_USERDEF1"]) * D.GetDecimal(this._flex[this._flex.Row, "WEIGHT"]), 0, MidpointRounding.AwayFromZero);
                        this._flex[this._flex.Row, "QT_REJECT_OLD"] = this._flex[this._flex.Row, "QT_REJECT_INV"];
                        decimal num6 = D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]) + D.GetDecimal(this._flex[this._flex.Row, "QT_REJECT_INV"]);
                        this._flex[this._flex.Row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX"]) * num6);
                        this._flex[this._flex.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex["UM_EX"]) * num6);
                        this._flex[this._flex.Row, "QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex["QT_GOOD_INV"]) / (D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(this._flex[this._flex.Row, "UNIT_PO_FACT"])));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool SetITEM(string ps_cditem)
        {
            try
            {
                if (ps_cditem != "")
                {
                    this._flex[this._flex.Row, "QT_TOTAL"] = 0;
                    this._flex[this._flex.Row, "QT_GOOD"] = 0;
                    this._flex[this._flex.Row, "QT_REJECT"] = 0;
                    DataTable dataTable = this._biz.ItemInfo_Search(new object[] { ps_cditem,
                                                                                   D.GetString(this.cbo공장.SelectedValue),
                                                                                   this.LoginInfo.CompanyCode,
                                                                                   Global.SystemLanguage.MultiLanguageLpoint });
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        this._flex[this._flex.Row, "NO_LOT"] = dataTable.Rows[0]["NO_LOT"].ToString();
                        this._flex[this._flex.Row, "QT_TOTAL"] = (decimal.Parse(dataTable.Rows[0]["QT_GOOD"].ToString()) + decimal.Parse(dataTable.Rows[0]["QT_REJECT"].ToString()));
                        this._flex[this._flex.Row, "QT_GOOD"] = dataTable.Rows[0]["QT_GOOD"];
                        this._flex[this._flex.Row, "QT_REJECT"] = dataTable.Rows[0]["QT_REJECT"];
                        return true;
                    }
                }
                else
                {
                    this._flex[this._flex.Row, "NM_ITEM"] = "";
                    this._flex[this._flex.Row, "STND_ITEM"] = "";
                    this._flex[this._flex.Row, "UNIT_IM"] = "";
                    this._flex[this._flex.Row, "NO_LOT"] = "";
                    this._flex[this._flex.Row, "QT_TOTAL"] = 0;
                    this._flex[this._flex.Row, "QT_GOOD"] = 0;
                    this._flex[this._flex.Row, "QT_REJECT"] = 0;
                    this._flex[this._flex.Row, "CD_SL"] = "";
                    this._flex[this._flex.Row, "NM_SL"] = "";
                    this._flex[this._flex.Row, "GRP_ITEM"] = "";
                    this._flex[this._flex.Row, "GRP_ITEMNM"] = "";
                }
                return false;
            }
            catch
            {
                this._flex[this._flex.Row, "NM_ITEM"] = "";
                this._flex[this._flex.Row, "STND_ITEM"] = "";
                this._flex[this._flex.Row, "UNIT_IM"] = "";
                this._flex[this._flex.Row, "NO_LOT"] = "";
                this._flex[this._flex.Row, "QT_TOTAL"] = 0;
                this._flex[this._flex.Row, "QT_GOOD"] = 0;
                this._flex[this._flex.Row, "QT_REJECT"] = 0;
                this._flex[this._flex.Row, "CD_SL"] = "";
                this._flex[this._flex.Row, "NM_SL"] = "";
                this._flex[this._flex.Row, "GRP_ITEM"] = "";
                this._flex[this._flex.Row, "GRP_ITEMNM"] = "";
            }
            return false;
        }

        private void Control_KeyEvent(object sender, KeyEventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "tb_DC":
                    if (e.KeyData != Keys.Return && e.KeyData != Keys.Tab)
                        break;
                    this.btn_insert_Click(null, null);
                    break;
                default:
                    if (e.KeyData != Keys.Return)
                        break;
                    SendKeys.SendWait("{TAB}");
                    break;
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.btn추가.Enabled || !this.CheckFieldHead(sender))
                    return;
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex["NO_IO"] = this.txt수불번호.Text;
                this._flex["S"] = "N";
                this._flex["YN_RETURN"] = D.GetString(this._header.CurrentRow["YN_RETURN"]);
                this._flex["CD_PLANT"] = D.GetString(this.cbo공장.SelectedValue);
                this._flex["DT_IO"] = this._header.CurrentRow["DT_IO"].ToString();
                this._flex["NO_EMP"] = this._header.CurrentRow["NO_EMP"].ToString();
                this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"].ToString();
                this._flex["NM_QTIOTP"] = this._header.CurrentRow["NM_QTIOTP"].ToString();
                this._flex["CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"].ToString();
                this._flex["LN_PARTNER"] = this._header.CurrentRow["LN_PARTNER"].ToString();
                this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"].ToString();
                this._flex["CD_PJT"] = this.ctx프로젝트.CodeValue;
                this._flex["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                this._flex["FG_TPIO"] = this._header.CurrentRow["FG_GI"].ToString();
                this._flex["FG_IO"] = this._header.CurrentRow["FG_IO"].ToString();
                if (D.GetString(this.bp_CD_SL.CodeValue) != string.Empty)
                {
                    this._flex["CD_SL"] = D.GetString(this.bp_CD_SL.CodeValue);
                    this._flex["NM_SL"] = D.GetString(this.bp_CD_SL.CodeName);
                }
                if (this._USER != null)
                    this._USER.setApp(this._flex, ComUser.Flag.currentFocus, 0, this._USER.FG_PJT1);
                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.Focus();
                this.btn요청적용.Enabled = false;
                this.btn삭제.Enabled = true;
                this.cbo공장.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = this._flex.DataTable.Select("S ='Y'");
                if (rows == null || rows.Length <= 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (MA.ServerKey(false, "AUTON") && !this.chk_POP_AUTON(rows))
                        return;
                    this._flex.Redraw = false;
                    for (int index = this._flex.Rows.Count - 1; index >= this._flex.Rows.Fixed; --index)
                    {
                        if (this._flex[index, "S"].ToString() == "Y")
                            this._flex.Rows.Remove(index);
                    }
                    this._flex.Redraw = true;
                    if (!this._flex.HasNormalRow && this.txt수불번호.Text == string.Empty)
                    {
                        this.btn추가.Enabled = true;
                        this.btn요청적용.Enabled = true;
                        this.btn삭제.Enabled = false;
                        this.cbo대체유형.Enabled = true;
                        this.cbo공장.Enabled = true;
                        this.ToolBarSaveButtonEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_RE_PR_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.Rows.Count > 0)
                {
                    for (int index = this._flex.Rows.Count - 1; index >= this._flex.Rows.Fixed; --index)
                        this._flex.Rows.Remove(index);
                    this._flex.Redraw = true;
                    if (!this._flex.HasNormalRow && this.txt수불번호.Text == string.Empty)
                    {
                        this.btn추가.Enabled = true;
                        this.btn삭제.Enabled = false;
                        this.cbo대체유형.Enabled = true;
                        this.cbo공장.Enabled = true;
                        this.ToolBarSaveButtonEnabled = false;
                    }
                }
                if (!this.CheckFieldHead(sender))
                    return;
                P_PU_GIREQ_SUB2 pPuGireqSuB2 = new P_PU_GIREQ_SUB2(this._flex.DataTable, D.GetString(this.cbo공장.SelectedValue), this.ctx담당자.CodeValue, this.ctx담당자.CodeName, this.ctx수불형태.CodeValue, this.ctx수불형태.CodeName, D.GetString(this.cbo대체유형.SelectedValue), this.ctx거래처.GetCodeValue(), this.ctx거래처.GetCodeName());
                if (pPuGireqSuB2.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    if (pPuGireqSuB2.gdt_returnH.Rows.Count > 0)
                    {
                        this.txt비고.Text = pPuGireqSuB2.gdt_returnH.Rows[0]["DC50_PO"].ToString().Trim();
                        if (pPuGireqSuB2.gdt_returnH.Rows[0]["CD_PARTNER"].ToString().Trim() != "")
                        {
                            this.ctx거래처.CodeValue = pPuGireqSuB2.gdt_returnH.Rows[0]["CD_PARTNER"].ToString().Trim();
                            this.ctx거래처.CodeName = pPuGireqSuB2.gdt_returnH.Rows[0]["LN_PARTNER"].ToString().Trim();
                            this._header.CurrentRow["CD_PARTNER"] = this.ctx거래처.CodeValue;
                            this._header.CurrentRow["LN_PARTNER"] = this.ctx거래처.CodeName;
                        }
                        this._header.CurrentRow["DC_RMK"] = this.txt비고.Text;
                        this.ctx수불형태.CodeValue = pPuGireqSuB2.gdt_returnH.Rows[0]["FG_GIREQ"].ToString().Trim();
                        this.cbo대체유형.SelectedValue = pPuGireqSuB2.gdt_returnH.Rows[0]["FG_GI"].ToString().Trim();
                        this._header.CurrentRow["CD_QTIOTP"] = pPuGireqSuB2.gdt_returnH.Rows[0]["FG_GIREQ"].ToString().Trim();
                        this._header.CurrentRow["FG_GI"] = pPuGireqSuB2.gdt_returnH.Rows[0]["FG_GI"].ToString().Trim();
                        this._header.CurrentRow["YN_AM"] = pPuGireqSuB2.gdt_returnH.Rows[0]["YN_AM"].ToString().Trim();
                        this.ctx요청부서.CodeValue = D.GetString(pPuGireqSuB2.gdt_returnH.Rows[0]["CD_DEPT"]);
                        this.ctx요청부서.CodeName = D.GetString(pPuGireqSuB2.gdt_returnH.Rows[0]["NM_DEPT"]);
                        this._header.CurrentRow["CD_DEPT_REQ"] = this.ctx요청부서.CodeValue;
                        this.cbo공장.SelectedValue = pPuGireqSuB2.gdt_returnH.Rows[0]["CD_PLANT"].ToString().Trim();
                        this._header.CurrentRow["CD_PLANT"] = this.cbo공장.SelectedValue;
                    }
                    this.InserGridtAdd(pPuGireqSuB2.gdt_return, pPuGireqSuB2.bChk_Rmk);
                    if (this._flex.Rows.Count < 1)
                        this.btn추가.Enabled = true;
                    else
                        this.btn추가.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void Menu_Click(object sender, EventArgs e)
        {
            switch (((ToolStripItem)sender).Name)
            {
                case "현재고조회":
                    string cd_item_multi = "";
                    for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
                        cd_item_multi = cd_item_multi + D.GetString(this._flex[row, "CD_ITEM"]) + "|";
                    new P_PU_STOCK_SUB(D.GetString(this.cbo공장.SelectedValue), cd_item_multi).ShowDialog((IWin32Window)this);
                    break;
                case "파일생성":
                    this._flex.ExportToExcel(false, true, true);
                    break;
                case "파일업로드":
                    this.엑셀업로드();
                    break;
            }
        }

        private void _btn엑셀_Click(object sender, EventArgs e)
        {
            if (!this.CheckFieldHead(sender))
                return;
            this.엑셀업로드();
        }

        private void btn_FILE_UPLOAD_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(this.txt수불번호.Text != ""))
                    return;
                string fileCode = D.GetString((this.txt수불번호.Text + "_" + Global.MainFrame.LoginInfo.CompanyCode));
                new AttachmentManager(Global.MainFrame.CurrentModule, Global.MainFrame.CurrentPageID, fileCode).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void tb_CD_QTIO_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is BpCodeTextBox bpCodeTextBox) || !(bpCodeTextBox.CodeValue == ""))
                    return;
                this._header.CurrentRow["FG_IO"] = "";
                this._header.CurrentRow["FG_TRANS"] = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 수불형태Default셋팅()
        {
            if (this._biz.Get설정("회사코드") != MA.Login.회사코드)
                return;
            string str1 = this._biz.Get설정("수불형태코드");
            string codename = this._biz.Get설정("수불형태명");
            if (str1 == string.Empty)
                return;
            string str2 = this._biz.Get설정("YN_AM");
            if (str2 == string.Empty)
            {
                this.ctx수불형태.SetCode(string.Empty, string.Empty);
                this.ctx수불형태.Focus();
            }
            else
            {
                this.ctx수불형태.SetCode(str1, codename);
                this._header.CurrentRow["FG_GI"] = this.cbo대체유형.SelectedValue.ToString();
                this._header.CurrentRow["CD_QTIOTP"] = this.ctx수불형태.CodeValue;
                this._header.CurrentRow["NM_QTIOTP"] = this.ctx수불형태.CodeName;
                this._header.CurrentRow["YN_AM"] = str2;
                this._header.CurrentRow["FG_IO"] = this._biz.Get설정("FG_IO");
                if (!(str1 != "") || !(D.GetString(this._header.CurrentRow["FG_IO"]).Trim() == ""))
                    return;
                DataTable fgIo = this._biz.Get_FG_IO(str1);
                if (fgIo.Rows.Count == 1)
                    this._header.CurrentRow["FG_IO"] = D.GetString(fgIo.Rows[0][0]);
            }
        }

        private void DropDownComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "cbo_CD_PLANT":
                        this.bp_CD_SL.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn프로젝트_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead(sender))
                    return;
                P_PJT_WBS_CBS_SUB pPjtWbsCbsSub = new P_PJT_WBS_CBS_SUB("", "MM_QTIO");
                if (pPjtWbsCbsSub.ShowDialog() == DialogResult.OK)
                {
                    DataTable returnDt = pPjtWbsCbsSub.Return_dt;
                    if (returnDt == null)
                        return;
                    decimal maxValue = this._flex.GetMaxValue("NO_IOLINE");
                    decimal val = 0M;
                    DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PJT_WBS_CBS_SUB");
                    this._flex.Redraw = false;
                    foreach (DataRow row in returnDt.Rows)
                    {
                        this._flex.Rows.Add();
                        int num = this._flex.Rows.Count - 1;
                        ++maxValue;
                        this._flex[num, "S"] = "N";
                        this._flex[num, "CD_ITEM"] = row["CD_MATL"];
                        this._flex[num, "NM_ITEM"] = row["NM_MATL"];
                        this._flex[num, "STND_ITEM"] = row["STND_ITEM"];
                        this._flex[num, "UNIT_IM"] = row["UNIT_IM"];
                        this._flex[num, "UNIT_PO"] = row["UNIT_PO"];
                        this._flex[num, "NO_LOT"] = row["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
                        this._flex[num, "NO_SERL"] = row["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
                        this._flex[num, "CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                        this._flex[num, "GRP_MFG"] = row["GRP_MFG"];
                        this._flex[num, "NM_ITEMGRP"] = row["NM_ITEMGRP"];
                        this._flex[num, "MAT_ITEM"] = row["MAT_ITEM"];
                        this._flex[num, "STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                        this._flex[num, "NM_PARTNER"] = row["LN_PARTNER"];
                        this._flex[num, "FG_SERNO"] = !(D.GetString(row["FG_SERNO"]) == "002") ? (!(D.GetString(row["FG_SERNO"]) == "003") ? this.DD("미관리") : "S/N") : "LOT";
                        this._flex[num, "NM_QTIOTP"] = this.ctx수불형태.CodeName;
                        this._flex[num, "UNIT_PO_FACT"] = (D.GetDecimal(row["UNIT_PO_FACT_ITEM"]) == 0M ? 1M : D.GetDecimal(row["UNIT_PO_FACT_ITEM"]));
                        this._flex[num, "CD_PJT"] = row["NO_PROJECT"];
                        this._flex[num, "NM_PROJECT"] = row["NM_PROJECT"];
                        this._flex[num, "SEQ_PROJECT"] = row["SEQ_PROJECT"];
                        this._flex[num, "CD_UNIT"] = row["CD_ITEM"];
                        this._flex[num, "NM_UNIT"] = row["NM_ITEM"];
                        this._flex[num, "STND_UNIT"] = row["STND_ITEM_ITEM"];
                        this._flex[num, "NO_WBS"] = row["NO_WBS"];
                        this._flex[num, "NO_CBS"] = row["NO_CBS"];
                        this._flex[num, "CD_COST"] = row["CD_COST"];
                        this._flex[num, "NM_COST"] = row["NM_COST"];
                        this._flex[num, "NO_LINE_PJTBOM"] = row["NO_LINE_PJTBOM"];
                        if (this.bp_CD_SL.CodeValue != string.Empty)
                        {
                            this._flex[num, "CD_SL"] = this.bp_CD_SL.CodeValue;
                            this._flex[num, "NM_SL"] = this.bp_CD_SL.CodeName;
                            val = BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(this._flex[num, "CD_SL"]), D.GetString(this._flex[num, "CD_ITEM"]), D.GetString(this._flex[num, "CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex[num, "SEQ_PROJECT"]) : 0M);
                            this._flex[num, "QT_INV"] = D.GetDecimal(val);
                        }
                        this._flex[num, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_NEED"]));
                        this._flex[num, "QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_NEED"]) / D.GetDecimal(this._flex[num, "UNIT_PO_FACT"]));
                        this._flex[num, "QT_REJECT_INV"] = 0;
                        this._flex[num, "CD_EXCH"] = "000";
                        this._flex[num, "RT_EXCH"] = 1;
                        this._flex[num, "NO_IO"] = this._header.CurrentRow["NO_IO"];
                        this._flex[num, "NO_IOLINE"] = maxValue;
                        this._flex[num, "YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                        this._flex[num, "CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                        this._flex[num, "DT_IO"] = this._header.CurrentRow["DT_IO"];
                        this._flex[num, "NO_EMP"] = this._header.CurrentRow["NO_EMP"];
                        this._flex[num, "CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                        this._flex[num, "CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"];
                        this._flex[num, "YN_AM"] = this._header.CurrentRow["YN_AM"];
                        this._flex[num, "FG_IO"] = this._header.CurrentRow["FG_IO"];
                        this._flex[num, "FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                        this._flex[num, "FG_PS"] = "1";
                        this._flex[num, "FG_TPIO"] = D.GetString(this.cbo대체유형.SelectedValue);
                        this._flex[num, "WEIGHT"] = row["WEIGHT"];
                        this._flex[num, "QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[num, "QT_GOOD_INV"]) * D.GetDecimal(row["WEIGHT"]));
                        this._flex[num, "QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(val) - D.GetDecimal(row["QT_NEED"]));
                        Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, row, this._flex, num);
                        this._flex.AddFinished();
                        this._flex.Col = this._flex.Cols.Fixed;
                        this._flex.Focus();
                    }
                    this._flex.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this._flex.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        private void cbo_exch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
                    this.cur환종.DecimalValue = MA.기준환율적용(this.tb_DT_IO.Text, D.GetString(this.cbo환종.SelectedValue.ToString()));
                if (D.GetString(this.cbo환종.SelectedValue.ToString()) == "000")
                    this.cur환종.DecimalValue = 1M;
                this.Set환율();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void tb_DT_IO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.cbo_exch_SelectionChangeCommitted(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetButtonState(bool pb_state)
        {
            this.tb_DT_IO.Enabled = this.cbo공장.Enabled = this.ctx거래처.Enabled = this.ctx담당자.Enabled = this.ctx수불형태.Enabled = this.txt수불번호.Enabled = this.ctx요청부서.Enabled = pb_state;
            if (MA.ServerKey(false, "GALAXIA", "DAEJOOKC", "SEEGENE"))
                this.cbo대체유형.Enabled = true;
            else
                this.cbo대체유형.Enabled = pb_state;
            if (BASIC.GetMAEXC_Menu("P_PU_CGI_REG", "PU_A00000022") == "100")
                this.ctx거래처.Enabled = true;
            this.cbo환종.Enabled = this.cur환종.Enabled = this.btn환종적용.Enabled = pb_state;
        }

        private bool CheckFieldHead(object sender)
        {
            try
            {
                if (this.tb_DT_IO.MaskEditBox.ClipText == "")
                {
                    this.tb_DT_IO.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl출고일.Text);
                    return false;
                }
                if (this.ctx수불형태.CodeValue.ToString() == "")
                {
                    this.ctx수불형태.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl수불형태.Text);
                    return false;
                }
                if (D.GetString(this.cbo공장.SelectedValue) == "")
                {
                    this.cbo공장.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                    return false;
                }
                if (D.GetString(this.cbo대체유형.SelectedValue) == "" || D.GetString(this._header.CurrentRow["FG_GI"]) == string.Empty)
                {
                    if (sender != null && sender.ToString().Contains("요청적용"))
                        return true;
                    this.cbo대체유형.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl대체유형.Text);
                    return false;
                }
                if (this.ctx수불형태.CodeValue == "")
                {
                    this.ctx수불형태.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl수불형태.Text);
                    return false;
                }
                if (this.cbo대체유형.Text == "")
                {
                    this.ctx수불형태.Focus();
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl대체유형.Text);
                    return false;
                }
                if (MA.ServerKey(false, "HAATZ"))
                {
                    if (this.ctx거래처.CodeValue == "")
                    {
                        this.ctx거래처.Focus();
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl거래처.Text);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return true;
        }

        private void InserGridtAdd(DataTable pdt_Line, bool bChk_Rmk)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0)
                    return;
                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = true;
                this.cbo공장.Enabled = false;
                this.cbo대체유형.Enabled = true;
                this._flex.Redraw = false;
                decimal num = Unit.환율(DataDictionaryTypes.PU, D.GetDecimal(this.cur환종.DecimalValue) == 0M ? 1M : D.GetDecimal(this.cur환종.DecimalValue));
                DataTable userColumnInfo = Config.UserColumnSetting.GetUSerColumnInfo(this.PageID, "P_PU_GIREQ_SUB2");
                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    DataRow row = pdt_Line.Rows[index];
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex["S"] = "N";
                    this._flex["NO_IO"] = this.txt수불번호.Text;
                    this._flex["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"].ToString();
                    this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"].ToString();
                    this._flex["DT_IO"] = this._header.CurrentRow["DT_IO"].ToString();
                    this._flex["NO_EMP"] = this._header.CurrentRow["NO_EMP"].ToString();
                    this._flex["CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"].ToString();
                    this._flex["LN_PARTNER"] = this._header.CurrentRow["LN_PARTNER"].ToString();
                    this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"].ToString();
                    this._flex["CD_PJT"] = this.ctx프로젝트.CodeValue;
                    this._flex["FG_TPIO"] = this._header.CurrentRow["FG_GI"].ToString();
                    this._flex["FG_IO"] = this._header.CurrentRow["FG_IO"].ToString();
                    this._flex["CD_ITEM"] = row["CD_ITEM"].ToString();
                    this._flex["NM_ITEM"] = row["NM_ITEM"].ToString();
                    this._flex["NO_DESIGN"] = row["NO_DESIGN"].ToString();
                    this._flex["STND_ITEM"] = row["STND_ITEM"].ToString();
                    this._flex["UNIT_IM"] = row["UNIT_IM"].ToString();
                    this._flex["UNIT_PO"] = row["UNIT_PO"].ToString();
                    this._flex["NO_LOT"] = row["FG_LOT"].ToString();
                    this._flex["NO_SERL"] = row["FG_SERL"].ToString();
                    this._flex["FG_SERNO"] = !(D.GetString(row["FG_SERNO"]) == "002") ? (!(D.GetString(row["FG_SERNO"]) == "003") ? this.DD("미관리") : this.DD("S/N")) : this.DD("LOT");
                    this._flex["CD_SL"] = row["CD_SL"].ToString();
                    this._flex["NM_SL"] = row["NM_SL"].ToString();
                    this._flex["QT_GOOD_INV"] = row["QT_GIREQ"];
                    this._flex["UNIT_PO_FACT"] = row["UNIT_PO_FACT"];
                    this._flex["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex["QT_GOOD_INV"]) / (D.GetDecimal(row["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row["UNIT_PO_FACT"])));
                    this._flex["QT_REJECT_INV"] = 0;
                    this._flex["NO_ISURCV"] = row["NO_GIREQ"].ToString();
                    this._flex["NO_ISURCVLINE"] = row["NO_LINE"];
                    this._flex["NO_IO"] = this.txt수불번호.Text;
                    this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                    this._flex["NM_QTIOTP"] = this._header.CurrentRow["NM_QTIOTP"];
                    this._flex["GIR_NO_EMP"] = row["NO_EMP"];
                    this._flex["GIR_NM_KOR"] = row["NM_KOR"];
                    this._flex["GIR_CD_DEPT"] = row["CD_DEPT"];
                    this._flex["GIR_NM_DEPT"] = row["NM_DEPT"];
                    this._flex["CD_CC"] = row["CD_CC"].ToString();
                    this._flex["NM_CC"] = row["NM_CC"].ToString();
                    this._flex["CD_PJT"] = row["CD_PJT"];
                    this._flex["NM_PROJECT"] = row["NM_PJT"];
                    if (pdt_Line.Columns.Contains("SEQ_PROJECT"))
                        this._flex["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                    if (this.bUseinv == "000")
                    {
                        decimal val;
                        if (Config.MA_ENV.PJT형여부 == "N")
                        {
                            val = BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row["CD_SL"]), D.GetString(row["CD_ITEM"]));
                            if (Config.MA_ENV.YN_UNIT == "Y")
                            {
                                this._flex["CD_UNIT"] = row["CD_PJT_ITEM"];
                                this._flex["NM_UNIT"] = row["PJT_NM_ITEM"];
                                this._flex["NO_DESIGN_UNIT"] = row["NO_PJT_DESIGN"];
                                this._flex["STND_UNIT"] = row["PJT_STND_ITEM"];
                            }
                        }
                        else
                            val = BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row["CD_SL"]), D.GetString(row["CD_ITEM"]), D.GetString(row["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(this._flex["SEQ_PROJECT"]) : 0M);
                        this._flex["QT_INV"] = D.GetDecimal(val);
                        this._flex["QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(val) - D.GetDecimal(row["QT_GIREQ"]));
                    }
                    this._flex["NM_ITEMGRP"] = row["GRP_ITEMNM"];
                    this._flex["BARCODE"] = row["BARCODE"];
                    this._flex["CD_ZONE"] = row["CD_ZONE"];
                    this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]) * num);
                    this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX"]));
                    this._flex["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row["UM_EX_PSO"]));
                    this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]) * num);
                    this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row["AM_EX"]));
                    this._flex["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"].ToString();
                    this._flex["WEIGHT"] = row["WEIGHT"];
                    this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                    this._flex["GRP_MFG"] = row["GRP_MFG"];
                    this._flex["NM_PARTNER"] = row["NM_PARTNER"];
                    this._flex["QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["QT_GIREQ"]) * D.GetDecimal(row["WEIGHT"]));
                    this._flex["FG_GUBUN"] = row["FG_GUBUN"];
                    this._flex["EN_ITEM"] = row["EN_ITEM"];
                    if (Global.MainFrame.ServerKeyCommon == "DAEJOOKC")
                    {
                        this._flex["CD_USERDEF2"] = row["CD_USERDEF1"];
                        this._flex["NM_USERDEF1"] = row["NM_USERDEF1"];
                    }
                    else if (this._USER != null)
                        this._USER.setApp(this._flex, ComUser.Flag.currentFocus, 0, row["FG_PJT1"].ToString());
                    if (bChk_Rmk)
                        this._flex["DC_RMK"] = row["DC_RMK"];
                    Config.UserColumnSetting.ApplyUserColumn(userColumnInfo, row, this._flex, this._flex.Row);
                }
                if (this.bUmSetting)
                    this.SETUM();
                this._flex.Focus();
                this.ToolBarSaveButtonEnabled = true;
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

        private void ShowMessage(메세지 msg, params string[] paras)
        {
            switch (msg)
            {
                case 메세지.삭제할수없습니다:
                    int num1 = (int)this.ShowMessage("MA_M000094");
                    break;
                case 메세지.라인테이터가한건이상존재해야저장할수있습니다:
                    this.ShowMessage("WK1_013");
                    break;
                case 메세지.변경된내용이없습니다:
                    this.ShowMessage("MA_M000017");
                    break;
            }
        }

        private void OnBpCodeTextBox_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult != DialogResult.OK)
                    return;
                DataRow[] rows = e.HelpReturn.Rows;
                switch (e.HelpID)
                {
                    case HelpID.P_USER:
                        switch (e.ControlName)
                        {
                            case "bp_Project":
                                if (Config.MA_ENV.YN_UNIT == "Y")
                                {
                                    this.d_SEQ_PROJECT = D.GetDecimal(e.HelpReturn.Rows[0]["SEQ_PROJECT"]);
                                    this.s_CD_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["CD_PJT_ITEM"]);
                                    this.s_NM_PJT_ITEM = D.GetString(e.HelpReturn.Rows[0]["NM_PJT_ITEM"]);
                                    this.s_PJT_ITEM_STND = D.GetString(e.HelpReturn.Rows[0]["PJT_ITEM_STND"]);
                                }
                                if (this._USER != null)
                                {
                                    this._USER.Control_AfterCodeHelp(this._flex, e.HelpReturn.CodeValue, this.cbo대체유형);
                                    break;
                                }
                                break;
                        }
                        break;
                    case HelpID.P_MA_EMP_SUB:
                        this._header.CurrentRow["CD_DEPT"] = rows[0]["CD_DEPT"].ToString();
                        break;
                    case HelpID.P_PU_EJTP_SUB:
                        this._header.CurrentRow["FG_IO"] = e.HelpReturn.Rows[0]["FG_IO"];
                        this._header.CurrentRow["FG_TRANS"] = e.HelpReturn.Rows[0]["FG_TRANS"];
                        this._header.CurrentRow["YN_AM"] = e.HelpReturn.Rows[0]["YN_AM"];
                        this._biz.Set설정("회사코드", MA.Login.회사코드);
                        this._biz.Set설정("수불형태코드", this.ctx수불형태.CodeValue);
                        this._biz.Set설정("수불형태명", this.ctx수불형태.CodeName);
                        this._biz.Set설정("FG_IO", D.GetString(this._header.CurrentRow["FG_IO"]));
                        this._biz.Set설정("YN_AM", D.GetString(this._header.CurrentRow["YN_AM"]));
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpCodeTextBox_QueryBefore(object sender, BpQueryArgs e)
        {
            BpCodeTextBox bpCodeTextBox = sender as BpCodeTextBox;
            switch (e.HelpID)
            {
                case HelpID.P_USER:
                    if (!(bpCodeTextBox.UserHelpID == "H_SA_PRJ_SUB"))
                        break;
                    e.HelpParam.P41_CD_FIELD1 = "프로젝트";
                    break;
                case HelpID.P_MA_SL_SUB:
                    if (this.cbo공장.SelectedValue == null || D.GetString(this.cbo공장.SelectedValue) == "")
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                        this.cbo공장.Focus();
                        e.QueryCancel = true;
                        break;
                    }
                    e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                    e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                    break;
                case HelpID.P_PU_EJTP_SUB:
                    e.HelpParam.P61_CODE1 = "012|";
                    if (!(Global.MainFrame.ServerKeyCommon == "DKTH"))
                        break;
                    e.HelpParam.P62_CODE2 = "400";
                    break;
            }
        }

        private void Set환율()
        {
            if (D.GetString(this.cbo환종.SelectedValue) == "000")
            {
                this.cur환종.DecimalValue = 1M;
                this.cur환종.Enabled = false;
            }
            else if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                this.cur환종.Enabled = false;
            else
                this.cur환종.Enabled = true;
        }

        private void 엑셀업로드()
        {
            try
            {
                string NO_KEY1 = string.Empty;
                bool flag1 = false;
                string empty1 = string.Empty;
                bool flag2 = false;
                string str1 = string.Empty;
                string empty2 = string.Empty;
                string empty3 = string.Empty;
                string empty4 = string.Empty;
                string empty5 = string.Empty;
                string empty6 = string.Empty;
                string empty7 = string.Empty;
                string empty8 = string.Empty;
                string empty9 = string.Empty;
                string empty10 = string.Empty;
                string empty11 = string.Empty;
                string NO_KEY2 = string.Empty;
                string multi_sl = string.Empty;
                string NO_KEY3 = string.Empty;
                string NO_KEY4 = string.Empty;
                bool flag3 = true;
                bool flag4 = true;
                bool flag5 = true;
                bool flag6 = true;
                string str2 = string.Empty;
                string str3 = string.Empty;
                string str4 = string.Empty;
                string str5 = string.Empty;
                decimal val1 = 0M;
                decimal num1 = Unit.환율(DataDictionaryTypes.PU, D.GetDecimal(this.cur환종.DecimalValue) == 0M ? 1M : D.GetDecimal(this.cur환종.DecimalValue));
                string strFG_PJG1 = "";
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                Application.DoEvents();
                DataTable dataTable1 = new Excel().StartLoadExcel(openFileDialog.FileName);
                string empty12 = string.Empty;
                string empty13 = string.Empty;
                string empty14 = string.Empty;
                this._flex.Redraw = false;
                bool flag7 = dataTable1.Columns.Contains("CD_PJT");
                bool flag8 = dataTable1.Columns.Contains("CD_SL");
                bool flag9 = dataTable1.Columns.Contains("DC_RMK");
                bool flag10 = dataTable1.Columns.Contains("CD_CC");
                bool flag11 = dataTable1.Columns.Contains("CD_PARTNER");
                bool flag12 = dataTable1.Columns.Contains("GI_PARTNER");
                bool flag13 = dataTable1.Columns.Contains("SEQ_PROJECT");
                bool flag14 = dataTable1.Columns.Contains("NO_LOT");
                bool flag15 = dataTable1.Columns.Contains("TXT_USERDEF1_QTIO");
                bool flag16 = dataTable1.Columns.Contains("TXT_USERDEF2_QTIO");
                foreach (DataRow row in dataTable1.Rows)
                {
                    NO_KEY1 = NO_KEY1 + row["CD_ITEM"].ToString().Trim().ToUpper() + "|";
                    if (flag7)
                        NO_KEY2 = NO_KEY2 + D.GetString(row["CD_PJT"]) + "|";
                    if (flag8)
                    {
                        if (D.GetString(row["CD_SL"]) == string.Empty)
                            row["CD_SL"] = D.GetString(this.bp_CD_SL.CodeValue);
                        multi_sl = multi_sl + D.GetString(row["CD_SL"]).ToUpper() + "|";
                    }
                    if (flag11 && D.GetString(row["CD_PARTNER"]) != string.Empty)
                        NO_KEY3 = NO_KEY3 + D.GetString(row["CD_PARTNER"]) + "|";
                    if (flag12 && D.GetString(row["GI_PARTNER"]) != string.Empty)
                        NO_KEY4 = NO_KEY4 + D.GetString(row["GI_PARTNER"]) + "|";
                }
                StringBuilder stringBuilder = new StringBuilder();
                string str6 = this.DD("품목코드");
                stringBuilder.AppendLine(str6);
                string str7 = "-".PadRight(80, '-');
                stringBuilder.AppendLine(str7);
                DataTable dataTable2 = this._biz.SearchPJT(NO_KEY2);
                DataTable dataTable3 = this._biz.SearchItem(D.GetString(this.cbo공장.SelectedValue), NO_KEY1);
                DataTable dataTable4 = this._biz.SearchQTinv(D.GetString(this.cbo공장.SelectedValue), NO_KEY1);
                this._dt_partner = this._biz.SearchPARTNER(NO_KEY3);
                this._dt_gi_prt = this._biz.SearchPARTNER(NO_KEY4);
                this.DT_SL = this._biz.SearchSL(D.GetString(this.cbo공장.SelectedValue), multi_sl);
                bool flag17 = dataTable1.Columns.Contains("QT_GOOD_INV");
                bool flag18 = dataTable1.Columns.Contains("QT_UNIT_MM");
                foreach (DataRow row1 in dataTable1.Rows)
                {
                    if (row1["CD_ITEM"].ToString().Trim() != null && !(row1["CD_ITEM"].ToString().Trim() == string.Empty) && !(row1["CD_ITEM"].ToString().Trim() == ""))
                    {
                        foreach (DataRow row2 in dataTable3.Rows)
                        {
                            if (row1["CD_ITEM"].ToString().Trim().ToUpper() == row2["CD_ITEM"].ToString())
                            {
                                flag2 = true;
                                str1 = row1["CD_ITEM"].ToString().Trim().ToUpper();
                                empty4 = row2["NM_ITEM"].ToString();
                                empty3 = row2["STND_ITEM"].ToString();
                                empty5 = row2["UNIT_IM"].ToString();
                                empty1 = row2["FG_SERNO"].ToString();
                                empty7 = row2["GRP_ITEMNM"].ToString();
                                val1 = D.GetDecimal(row2["UNIT_PO_FACT"]);
                                empty6 = row2["BARCODE"].ToString();
                                empty9 = row2["UNIT_PO"].ToString();
                                empty10 = row2["CD_GISL"].ToString();
                                empty11 = row2["NM_GISL"].ToString();
                                break;
                            }
                            flag2 = false;
                        }
                        DataRow[] dataRowArray1 = null;
                        if (flag7 && D.GetString(row1["CD_PJT"]) != "")
                        {
                            DataRow[] dataRowArray2 = dataTable2.Select("NO_PROJECT = '" + D.GetString(row1["CD_PJT"]) + "'");
                            if (dataRowArray2 == null || dataRowArray2.Length == 0)
                            {
                                str2 = str2 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]) + " &     프로젝트번호 : " + D.GetString(row1["CD_PJT"]);
                                flag3 = false;
                                continue;
                            }
                            if (Config.MA_ENV.YN_UNIT == "Y" && flag13)
                            {
                                dataRowArray1 = dataTable2.Select("NO_PROJECT = '" + D.GetString(row1["CD_PJT"]) + "' AND SEQ_PROJECT = " + D.GetDecimal(row1["SEQ_PROJECT"]) + " AND CD_PJT_ITEM ='" + D.GetString(row1["CD_UNIT"]) + "'");
                                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                                {
                                    str2 = str2 + "\n프로젝트번호 : " + D.GetString(row1["CD_PJT"]) + " &     UNIT 항번 : " + D.GetString(row1["SEQ_PROJECT"]) + " &     UNIT 코드 : " + D.GetString(row1["CD_UNIT"]);
                                    flag3 = false;
                                    continue;
                                }
                            }
                            empty8 = D.GetString(dataRowArray2[0]["NM_PROJECT"]);
                            strFG_PJG1 = D.GetString(dataRowArray2[0]["FG_PJT1"]);
                        }
                        if (flag8 && D.GetString(row1["CD_SL"]) != "")
                        {
                            DataRow[] dataRowArray3 = null;
                            if (this.DT_SL != null)
                                dataRowArray3 = this.DT_SL.Select("CD_SL = '" + D.GetString(row1["CD_SL"]) + "'");
                            if (dataRowArray3 == null || dataRowArray3.Length == 0)
                            {
                                str3 = str3 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]) + " &     창고코드 : " + D.GetString(row1["CD_SL"]);
                                flag4 = false;
                                continue;
                            }
                        }
                        if (flag11 && D.GetString(row1["CD_PARTNER"]) != "")
                        {
                            DataRow[] dataRowArray4 = null;
                            if (this._dt_partner != null)
                                dataRowArray4 = this._dt_partner.Select("CD_PARTNER = '" + D.GetString(row1["CD_PARTNER"]) + "'");
                            if (dataRowArray4 == null || dataRowArray4.Length == 0)
                            {
                                str4 = str4 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]) + " &     거래처코드 : " + D.GetString(row1["CD_PARTNER"]);
                                flag5 = false;
                                continue;
                            }
                        }
                        if (flag12 && D.GetString(row1["GI_PARTNER"]) != "")
                        {
                            DataRow[] dataRowArray5 = null;
                            if (this._dt_gi_prt != null)
                                dataRowArray5 = this._dt_gi_prt.Select("CD_PARTNER = '" + D.GetString(row1["GI_PARTNER"]) + "'");
                            if (dataRowArray5 == null || dataRowArray5.Length == 0)
                            {
                                str5 = str5 + "\n품목코드 : " + D.GetString(row1["CD_ITEM"]) + " &     납품처코드 : " + D.GetString(row1["GI_PARTNER"]);
                                flag6 = false;
                                continue;
                            }
                        }
                        if (flag2)
                        {
                            DataRow row3 = this._flex.DataTable.NewRow();
                            row3["CD_ITEM"] = row1["CD_ITEM"].ToString().Trim().ToUpper();
                            row3["NM_ITEM"] = empty4;
                            row3["STND_ITEM"] = empty3;
                            row3["UNIT_IM"] = empty5;
                            row3["BARCODE"] = empty6;
                            row3["UNIT_PO"] = empty9;
                            row3["NO_LOT"] = empty1 == "002" ? "YES" : "NO";
                            if (empty1 == "002")
                                row3["FG_SERNO"] = "LOT";
                            row3["NO_SERL"] = empty1 == "003" ? "YES" : "NO";
                            if (empty1 == "003")
                                row3["FG_SERNO"] = "S/N";
                            if (empty1 == "001")
                                row3["FG_SERNO"] = "미관리";
                            if (!flag8)
                            {
                                row3["CD_SL"] = D.GetString(this.bp_CD_SL.CodeValue);
                                row3["NM_SL"] = D.GetString(this.bp_CD_SL.CodeName);
                            }
                            row3["UNIT_PO_FACT"] = val1;
                            if (flag17 && D.GetDecimal(row1["QT_GOOD_INV"]) > 0M)
                            {
                                row3["QT_GOOD_INV"] = row1["QT_GOOD_INV"];
                                row3["QT_UNIT_MM"] = !(val1 == 0M) ? Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row3["QT_GOOD_INV"]) / val1) : row3["QT_GOOD_INV"];
                            }
                            else if (flag18 && D.GetDecimal(row1["QT_UNIT_MM"]) > 0M)
                            {
                                row3["QT_UNIT_MM"] = row1["QT_UNIT_MM"];
                                row3["QT_GOOD_INV"] = !(val1 == 0M) ? Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row3["QT_UNIT_MM"]) * val1) : row3["QT_UNIT_MM"];
                            }
                            else
                            {
                                row3["QT_GOOD_INV"] = 0;
                                row3["QT_UNIT_MM"] = 0;
                            }
                            row3["NO_IO"] = this._header.CurrentRow["NO_IO"];
                            row3["YN_RETURN"] = this._header.CurrentRow["YN_RETURN"];
                            row3["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                            row3["DT_IO"] = this._header.CurrentRow["DT_IO"];
                            row3["NO_EMP"] = this._header.CurrentRow["NO_EMP"];
                            row3["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                            row3["NM_QTIOTP"] = this._header.CurrentRow["NM_QTIOTP"];
                            row3["YN_AM"] = this._header.CurrentRow["YN_AM"];
                            row3["CD_PJT"] = this.ctx프로젝트.CodeValue;
                            row3["FG_TPIO"] = this._header.CurrentRow["FG_GI"];
                            row3["FG_IO"] = this._header.CurrentRow["FG_IO"];
                            if (flag7)
                            {
                                row3["CD_PJT"] = D.GetString(row1["CD_PJT"]);
                                row3["NM_PROJECT"] = empty8;
                                if (this._USER != null)
                                    this._USER.setApp(row3, strFG_PJG1);
                            }
                            if (dataRowArray1 != null && dataRowArray1.Length > 0)
                            {
                                row3["SEQ_PROJECT"] = dataRowArray1[0]["SEQ_PROJECT"];
                                row3["CD_UNIT"] = dataRowArray1[0]["CD_PJT_ITEM"];
                                row3["NM_UNIT"] = dataRowArray1[0]["NM_PJT_ITEM"];
                                row3["STND_UNIT"] = dataRowArray1[0]["PJT_ITEM_STND"];
                                row3["CD_PJT"] = D.GetString(dataRowArray1[0]["NO_PROJECT"]);
                                row3["NM_PROJECT"] = D.GetString(dataRowArray1[0]["NM_PROJECT"]);
                            }
                            if (flag8 && this.ChkData_SL(D.GetString(row1["CD_SL"]), ref empty12))
                            {
                                row3["CD_SL"] = D.GetString(row1["CD_SL"]).ToUpper();
                                row3["NM_SL"] = empty12;
                            }
                            else if (flag8 && D.GetString(row1["CD_SL"]) == "" && empty10 != "")
                            {
                                if (MA.ServerKey(false, "DAELT"))
                                {
                                    row3["CD_SL"] = empty10;
                                    row3["NM_SL"] = empty11;
                                }
                            }
                            if (!flag11)
                                row3["CD_PARTNER"] = this._header.CurrentRow["CD_PARTNER"];
                            else if (flag11 && this.ChkData_PARTNER(D.GetString(row1["CD_PARTNER"]), ref empty13))
                                row3["CD_PARTNER"] = D.GetString(row1["CD_PARTNER"]);
                            if (flag12 && flag12 && this.ChkData_GI_PRT(D.GetString(row1["GI_PARTNER"]), ref empty14))
                                row3["GI_PARTNER"] = D.GetString(row1["GI_PARTNER"]);
                            if (flag9)
                                row3["DC_RMK"] = D.GetString(row1["DC_RMK"]);
                            if (flag10)
                                row3["CD_CC"] = D.GetString(row1["CD_CC"]);
                            if (dataTable1.Columns.Contains("UM_EX_PSO"))
                            {
                                row3["UM_EX_PSO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(row1["UM_EX_PSO"]));
                                decimal val = UDecimal.Getdivision(D.GetDecimal(row1["UM_EX_PSO"]), val1);
                                row3["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, val);
                                row3["UM"] = Unit.원화단가(DataDictionaryTypes.PU, val * num1);
                                row3["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row1["QT_GOOD_INV"]) * val * num1);
                                row3["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(row1["QT_GOOD_INV"]) * val * 1M);
                            }
                            decimal val2 = 0M;
                            DataRow[] dataRowArray6;
                            if (Config.MA_ENV.PJT형여부 == "N")
                                dataRowArray6 = dataTable4.Select("CD_PLANT = '" + D.GetString(this._header.CurrentRow["CD_PLANT"]) + "' AND CD_SL = '" + D.GetString(row3["CD_SL"]) + "' AND CD_ITEM = '" + D.GetString(row3["CD_ITEM"]) + "'");
                            else
                                dataRowArray6 = dataTable4.Select("CD_PLANT = '" + D.GetString(this._header.CurrentRow["CD_PLANT"]) + "' AND CD_SL = '" + D.GetString(row3["CD_SL"]) + "' AND CD_ITEM = '" + D.GetString(row3["CD_ITEM"]) + "' AND CD_PJT = '" + D.GetString(row3["CD_PJT"]) + "' AND SEQ_PROJECT = " + (Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(row3["SEQ_PROJECT"]) : 0M));
                            if (dataRowArray6 != null && dataRowArray6.Length > 0)
                                val2 = D.GetDecimal(dataRowArray6[0]["QT_INV"]);
                            row3["QT_INV"] = D.GetDecimal(val2);
                            row3["NM_ITEMGRP"] = empty7;
                            row3["QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(val2) - D.GetDecimal(row3["QT_GOOD_INV"]));
                            if (flag14)
                                row3["CD_USERDEF1"] = D.GetString(row1["NO_LOT"]);
                            if (flag15)
                                row3["TXT_USERDEF1_QTIO"] = D.GetString(row1["TXT_USERDEF1_QTIO"]);
                            if (flag16)
                                row3["TXT_USERDEF2_QTIO"] = D.GetString(row1["TXT_USERDEF2_QTIO"]);
                            this._flex.DataTable.Rows.Add(row3);
                        }
                        else
                        {
                            string upper = row1["CD_ITEM"].ToString().PadRight(10, ' ').Trim().ToUpper();
                            stringBuilder.AppendLine(upper);
                            flag1 = true;
                        }
                    }
                }
                if (flag1)
                {
                    this.ShowDetailMessage("엑셀 업로드하는 중에 마스터품목과 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                }
                if (!flag3)
                {
                    this.ShowDetailMessage("엑셀 업로드하는 중에 프로젝트의 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str2.ToString());
                }
                if (!flag4)
                {
                    this.ShowDetailMessage("엑셀 업로드하는 중에 창고에 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str3.ToString());
                }
                if (!flag5)
                {
                    this.ShowDetailMessage("엑셀 업로드하는 중에 거래처에 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str4.ToString());
                }
                if (!flag6)
                {
                    this.ShowDetailMessage("엑셀 업로드하는 중에 납품처에 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", str5.ToString());
                }
                this._flex.Redraw = true;
                this._flex.Select(this._flex.DataTable.Rows.Count, this._flex.Cols.Fixed);
                this._flex.IsDataChanged = true;
                this.Page_DataChanged(null, null);
                this.btn요청적용.Enabled = true;
                this.btn삭제.Enabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool ChkData_SL(string cd_sl, ref string p_nm_sl)
        {
            if (this.DT_SL == null || this.DT_SL.Rows.Count < 1)
                return false;
            DataRow[] dataRowArray = this.DT_SL.Select("CD_SL = '" + cd_sl + "'");
            if (dataRowArray == null || dataRowArray.Length < 1)
                return false;
            p_nm_sl = D.GetString(dataRowArray[0]["NM_SL"]);
            return true;
        }

        private bool ChkData_PARTNER(string cd_partner, ref string p_ln_partner)
        {
            if (this._dt_partner == null || this._dt_partner.Rows.Count < 1)
                return false;
            DataRow[] dataRowArray = this._dt_partner.Select("CD_PARTNER = '" + cd_partner + "'");
            if (dataRowArray == null || dataRowArray.Length < 1)
                return false;
            p_ln_partner = D.GetString(dataRowArray[0]["LN_PARTNER"]);
            return true;
        }

        private bool ChkData_GI_PRT(string gi_partner, ref string p_ln_gi_prt)
        {
            if (this._dt_gi_prt == null || this._dt_gi_prt.Rows.Count < 1)
                return false;
            DataRow[] dataRowArray = this._dt_gi_prt.Select("CD_PARTNER = '" + gi_partner + "'");
            if (dataRowArray == null || dataRowArray.Length < 1)
                return false;
            p_ln_gi_prt = D.GetString(dataRowArray[0]["LN_PARTNER"]);
            return true;
        }

        private void ChangeFgTpio()
        {
            for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
                this._flex[row, "FG_TPIO"] = D.GetString(this._header.CurrentRow["FG_GI"]);
        }

        public bool chk_POP_AUTON(DataRow[] rows)
        {
            foreach (DataRow row in rows)
            {
                if (D.GetString(row["NO_POP"]) != string.Empty)
                {
                    this.ShowMessage("해당 건을 삭제할 수 없습니다. 출하관리(오토앤) 메뉴에서 출하취소 처리 해야합니다.");
                    return false;
                }
            }
            return true;
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

        private bool 등록여부(string 품목코드)
        {
            for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
            {
                if (this._flex[row, "CD_ITEM"].Equals(품목코드))
                    return true;
            }
            return false;
        }

        private bool Chk환종 => !Checker.IsEmpty(this.cbo환종, this.DD("환종"));

        private void m_btnSerlView_Click(object sender, EventArgs e)
        {
            DataTable dataTable = this._flex.DataTable;
            if (string.Compare(this.MNG_SERIAL, "Y") != 0 || dataTable == null)
                return;
            DataRow[] dataRowArray = dataTable.Select("NO_SERL = 'YES'", "");
            DataTable dt = dataTable.Clone();
            if (dataRowArray.Length > 0)
            {
                foreach (DataRow row in dataRowArray)
                    dt.ImportRow(row);
                new P_PU_SERL_SUB_I(dt).ShowDialog((IWin32Window)this);
            }
        }

        private void btn_프로젝트적용_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray.Length == 0)
                {
                    int num1 = (int)this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow row in dataRowArray)
                    {
                        row["CD_PJT"] = this.ctx프로젝트.CodeValue;
                        row["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                        if (Config.MA_ENV.YN_UNIT == "Y")
                        {
                            row["SEQ_PROJECT"] = this.d_SEQ_PROJECT;
                            row["CD_UNIT"] = this.s_CD_PJT_ITEM;
                            row["NM_UNIT"] = this.s_NM_PJT_ITEM;
                            row["STND_UNIT"] = this.s_PJT_ITEM_STND;
                        }
                        if (this._USER != null)
                            this._USER.setApp(row, this._USER.FG_PJT1);
                    }
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn프로젝트적용.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_창고적용_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (dataRow.RowState == DataRowState.Added)
                        {
                            dataRow["CD_SL"] = D.GetString(this.bp_CD_SL.CodeValue);
                            dataRow["NM_SL"] = D.GetString(this.bp_CD_SL.CodeName);
                            decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(dataRow["CD_SL"]), D.GetString(dataRow["CD_ITEM"]), D.GetString(dataRow["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(dataRow["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(dataRow["CD_SL"]), D.GetString(dataRow["CD_ITEM"]));
                            dataRow["QT_INV"] = D.GetDecimal(val);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_AppCC_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
            if (dataRowArray.Length == 0)
            {
                this.ShowMessage(공통메세지.선택된자료가없습니다);
            }
            else
            {
                foreach (DataRow dataRow in dataRowArray)
                {
                    dataRow["CD_CC"] = this.ctxCC.CodeValue;
                    dataRow["NM_CC"] = this.ctxCC.CodeName;
                }
            }
        }

        private void btn_APP_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "btn_BOM":
                        if (this.ctx수불형태.CodeValue != "" && this.cbo대체유형.Text != "")
                        {
                            this.APP_BOM();
                            break;
                        }
                        if (this.ctx수불형태.CodeValue == "")
                        {
                            this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl수불형태.Text);
                            break;
                        }
                        if (!(this.cbo대체유형.Text == ""))
                            break;
                        int num1 = (int)this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl대체유형.Text);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void APP_BOM()
        {
            P_PU_GIREQ_BOM_SUB pPuGireqBomSub = new P_PU_GIREQ_BOM_SUB(D.GetString(this.cbo공장.SelectedValue), this.tb_DT_IO.Text);
            if (pPuGireqBomSub.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            this.Grid_BOMAfter(pPuGireqBomSub.dt_return);
            if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
            {
                this.ToolBarDeleteButtonEnabled = false;
                this.ToolBarSaveButtonEnabled = true;
            }
            else
            {
                this.ToolBarDeleteButtonEnabled = true;
                this.ToolBarSaveButtonEnabled = false;
            }
        }

        private void Grid_BOMAfter(DataTable dt_return)
        {
            try
            {
                DataTable dataTable = this._flex.DataTable;
                bool flag = true;
                decimal maxValue = this._flex.GetMaxValue("NO_ISURCVLINE");
                this._flex.Redraw = false;
                CommonFunction commonFunction = new CommonFunction();
                foreach (DataRow row1 in dt_return.Rows)
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["CD_ITEM"] = row1["CD_MATL"];
                    row2["NM_ITEM"] = row1["NM_ITEM_MATL"];
                    row2["STND_ITEM"] = row1["STND_ITEM_MATL"];
                    row2["UNIT_IM"] = row1["UNIT_IM_MATL"];
                    row2["UNIT_PO"] = row1["UNIT_PO"];
                    row2["CD_SL"] = this.bp_CD_SL.CodeValue;
                    row2["NM_SL"] = this.bp_CD_SL.CodeName;
                    row2["CD_PJT"] = this.ctx프로젝트.CodeValue;
                    row2["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                    row2["NO_LOT"] = row1["FG_SERNO1"].ToString() == "002" ? "YES" : "NO";
                    row2["NO_SERL"] = row1["FG_SERNO1"].ToString() == "003" ? "YES" : "NO";
                    row2["FG_SERNO"] = !(D.GetString(row1["FG_SERNO1"]) == "002") ? (!(D.GetString(row1["FG_SERNO1"]) == "003") ? this.DD("미관리") : this.DD("S/N")) : this.DD("LOT");
                    row2["DT_IO"] = D.GetString(this._header.CurrentRow["DT_IO"]);
                    row2["CD_QTIOTP"] = D.GetString(this._header.CurrentRow["CD_QTIOTP"]);
                    row2["NM_QTIOTP"] = D.GetString(this._header.CurrentRow["NM_QTIOTP"]);
                    row2["YN_RETURN"] = D.GetString(this._header.CurrentRow["YN_RETURN"]);
                    row2["YN_AM"] = D.GetString(this._header.CurrentRow["YN_AM"]);
                    row2["CD_PLANT"] = D.GetString(this._header.CurrentRow["CD_PLANT"]);
                    row2["QT_GOOD_INV"] = row1["QT_ITEM_NET"];
                    row2["CD_SL"] = row1["CD_GISL"];
                    row2["NM_SL"] = row1["NM_GISL"];
                    row2["CD_PARTNER"] = D.GetString(this._header.CurrentRow["CD_PARTNER"]);
                    row2["FG_TPIO"] = D.GetString(this._header.CurrentRow["FG_GI"]);
                    row2["FG_IO"] = D.GetString(this._header.CurrentRow["FG_IO"]);
                    row2["NO_EMP"] = D.GetString(this._header.CurrentRow["NO_EMP"]);
                    decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row2["CD_SL"]), D.GetString(row2["CD_ITEM"]), D.GetString(row2["CD_PJT"]), Config.MA_ENV.YN_UNIT == "Y" ? D.GetDecimal(row2["SEQ_PROJECT"]) : 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row2["CD_SL"]), D.GetString(row2["CD_ITEM"]));
                    row2["QT_INV"] = D.GetDecimal(val);
                    row2["NM_ITEMGRP"] = row1["GRP_ITEMNM"];
                    row2["BARCODE"] = row1["BARCODE"];
                    ComFunc.GetTableSearch("MA_PITEM", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this._header.CurrentRow["CD_PLANT"].ToString(),
                                                                      row1["CD_MATL"].ToString() });
                    if (this._header.CurrentRow["FG_IO"].ToString() == "011")
                    {
                        row2["CD_WC"] = this._header.CurrentRow["CD_WC"];
                        row2["CD_WCOP"] = this._header.CurrentRow["CD_WCOP"];
                        row2["NM_WC"] = this._header.CurrentRow["NM_WC"];
                        row2["NM_OP"] = this._header.CurrentRow["NM_OP"];
                    }
                    row2["UNIT_PO_FACT"] = row1["UNIT_PO_FACT"];
                    if (this._flex.CDecimal(row1["UNIT_PO_FACT"]) == 0M)
                        row2["UNIT_PO_FACT"] = 1;
                    row2["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row2["QT_GOOD_INV"]) / D.GetDecimal(row2["UNIT_PO_FACT"]));
                    row2["QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(val) - D.GetDecimal(row1["QT_ITEM_NET"]));
                    dataTable.Rows.Add(row2);
                    ++maxValue;
                    flag = false;
                }
                this._flex.Select(this._flex.Rows.Count - 1, this._flex.Cols.Fixed);
                if (this.bUmSetting)
                    this.SETUM();
                this.SetButtonState(false);
                this._flex.Redraw = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Appstock_Click(object sender, EventArgs e)
        {
            if (!this.CheckFieldHead(sender))
                return;
            P_PU_GIREQ_SUB5 pPuGireqSuB5 = new P_PU_GIREQ_SUB5(this.cbo공장.SelectedValue.ToString(), this.bp_CD_SL.CodeValue, this.bp_CD_SL.CodeName, this.tb_DT_IO.Text);
            if (pPuGireqSuB5.ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            this.Grid_StockAfter(pPuGireqSuB5._dtL);
            if (this._flex.DataTable != null && this._flex.DataTable.Rows.Count > 0)
            {
                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = true;
                this.cbo대체유형.Enabled = true;
                this.cbo공장.Enabled = true;
                this.ToolBarSaveButtonEnabled = true;
            }
        }

        private void Grid_StockAfter(DataTable _dtL)
        {
            try
            {
                DataTable dataTable = this._flex.DataTable;
                bool flag = true;
                decimal maxValue = this._flex.GetMaxValue("NO_ISURCVLINE");
                this._flex.Redraw = false;
                CommonFunction commonFunction = new CommonFunction();
                foreach (DataRow row1 in _dtL.Rows)
                {
                    DataRow row2 = dataTable.NewRow();
                    row2["CD_ITEM"] = row1["CD_ITEM"];
                    row2["NM_ITEM"] = row1["NM_ITEM"];
                    if (row1.Table.Columns.Contains("NO_DESIGN"))
                        row2["NO_DESIGN"] = row1["NO_DESIGN"];
                    row2["STND_ITEM"] = row1["STND_ITEM"];
                    row2["UNIT_IM"] = row1["UNIT_IM"];
                    row2["UNIT_PO"] = row1["UNIT_PO"];
                    row2["NO_LOT"] = row1["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
                    row2["NO_SERL"] = row1["FG_SERNO"].ToString() == "003" ? "YES" : "NO";
                    row2["FG_SERNO"] = !(D.GetString(row1["FG_SERNO"]) == "002") ? (!(D.GetString(row1["FG_SERNO"]) == "003") ? this.DD("미관리") : this.DD("S/N")) : this.DD("LOT");
                    row2["QT_GOOD_INV"] = row1["QT_INV"];
                    row2["CD_SL"] = row1["CD_SL"] != null ? row1["CD_SL"] : this.bp_CD_SL.CodeValue;
                    row2["NM_SL"] = row1["NM_SL"] != null ? row1["NM_SL"] : this.bp_CD_SL.CodeName;
                    row2["CD_PJT"] = this.ctx프로젝트.CodeValue;
                    row2["CD_PLANT"] = D.GetString(this._header.CurrentRow["CD_PLANT"]);
                    row2["DT_IO"] = D.GetString(this._header.CurrentRow["DT_IO"]);
                    row2["CD_QTIOTP"] = D.GetString(this._header.CurrentRow["CD_QTIOTP"]);
                    row2["NM_QTIOTP"] = D.GetString(this._header.CurrentRow["NM_QTIOTP"]);
                    row2["YN_RETURN"] = D.GetString(this._header.CurrentRow["YN_RETURN"]);
                    row2["YN_AM"] = D.GetString(this._header.CurrentRow["YN_AM"]);
                    row2["CD_PLANT"] = D.GetString(this._header.CurrentRow["CD_PLANT"]);
                    row2["CD_PARTNER"] = D.GetString(this._header.CurrentRow["CD_PARTNER"]);
                    row2["FG_TPIO"] = D.GetString(this._header.CurrentRow["FG_GI"]);
                    row2["FG_IO"] = D.GetString(this._header.CurrentRow["FG_IO"]);
                    row2["NO_EMP"] = D.GetString(this._header.CurrentRow["NO_EMP"]);
                    row2["UNIT_PO_FACT"] = (D.GetDecimal(row1["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row1["UNIT_PO_FACT"]));
                    row2["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row2["QT_GOOD_INV"]) / D.GetDecimal(row2["UNIT_PO_FACT"]));
                    decimal val = !(Config.MA_ENV.PJT형여부 == "N") ? BASICPU.Item_PINVN_PJT(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row2["CD_SL"]), D.GetString(row2["CD_ITEM"]), D.GetString(row2["CD_PJT"]), 0M) : BASICPU.Item_PINVN(D.GetString(this._header.CurrentRow["CD_PLANT"]), D.GetString(row2["CD_SL"]), D.GetString(row2["CD_ITEM"]));
                    row2["QT_INV"] = D.GetDecimal(val);
                    if (_dtL.Columns.Contains("CD_PJT") && Config.MA_ENV.PJT형여부 == "Y")
                    {
                        row2["CD_PJT"] = D.GetString(row1["CD_PJT"]);
                        row2["NM_PROJECT"] = D.GetString(row1["NM_PJT"]);
                        if (_dtL.Columns.Contains("SEQ_PROJECT"))
                            row2["SEQ_PROJECT"] = D.GetString(row1["SEQ_PROJECT"]);
                        if (_dtL.Columns.Contains("CD_PJT_ITEM"))
                        {
                            row2["CD_UNIT"] = D.GetString(row1["CD_PJT_ITEM"]);
                            row2["NM_UNIT"] = D.GetString(row1["NM_PJT_ITEM"]);
                            if (row1.Table.Columns.Contains("NO_PJT_DESIGN"))
                                row2["NO_DESIGN_UNIT"] = D.GetString(row1["NO_PJT_DESIGN"]);
                            row2["STND_UNIT"] = D.GetString(row1["PJT_STND_ITEM"]);
                        }
                    }
                    else
                    {
                        row2["CD_PJT"] = this.ctx프로젝트.CodeValue;
                        row2["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                    }
                    row2["NM_ITEMGRP"] = row1["GRP_ITEMNM"];
                    row2["BARCODE"] = row1["BARCODE"];
                    row2["STND_DETAIL_ITEM"] = row1["STND_DETAIL_ITEM"];
                    row2["QT_REMAIN"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(val) - D.GetDecimal(row1["QT_INV"]));
                    dataTable.Rows.Add(row2);
                    ++maxValue;
                    flag = false;
                }
                this._flex.Select(this._flex.Rows.Count - 1, this._flex.Cols.Fixed);
                this.SetButtonState(false);
                this._flex.Redraw = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn입고적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead(sender))
                    return;
                P_PU_WGI_GR_SUB pPuWgiGrSub = new P_PU_WGI_GR_SUB(this.MainFrameInterface, new object[] { D.GetString(this.cbo공장.SelectedValue) }, "", "", "계정대체출고등록");
                if (pPuWgiGrSub.ShowDialog((IWin32Window)this) == DialogResult.OK && pPuWgiGrSub.dtL.Rows.Count > 0)
                {
                    this.입고적용그리드라인추가(pPuWgiGrSub.dtL);
                    this.btn추가.Enabled = false;
                    this.btn삭제.Enabled = true;
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
                    this._flex["NO_DESIGN"] = row["NO_DESIGN"].ToString();
                    this._flex["STND_ITEM"] = row["STND_ITEM"].ToString();
                    this._flex["UNIT_IM"] = row["UNIT_IM"].ToString();
                    this._flex["UNIT_PO"] = row["UNIT_PO"].ToString();
                    this._flex["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"];
                    this._flex["NO_LOT"] = row["FG_LOT"].ToString();
                    this._flex["QT_GOOD_INV"] = D.GetDecimal(row["QT_GOOD_INV"]);
                    this._flex["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex["QT_GOOD_INV"]) / (D.GetDecimal(row["UNIT_PO_FACT"]) == 0M ? 1M : D.GetDecimal(row["UNIT_PO_FACT"])));
                    this._flex["QT_REJECT_INV"] = 0;
                    this._flex["DT_IO"] = D.GetString(this.tb_DT_IO.Text);
                    this._flex["NM_KOR"] = this._header.CurrentRow["NM_KOR"];
                    this._flex["NO_EMP"] = this._header.CurrentRow["NO_EMP"];
                    this._flex["입고창고"] = row["GR_NM_SL"];
                    this._flex["CD_SL_REF"] = row["GR_CD_SL"];
                    this._flex["NM_QTIOTP"] = this.ctx수불형태.CodeName;
                    this._flex["CD_QTIOTP"] = this._header.CurrentRow["CD_QTIOTP"];
                    this._flex["NO_PSO_MGMT"] = row["NO_WO"];
                    this._flex["NO_PSOLINE_MGMT"] = 0;
                    this._flex["YN_RETURN"] = "N";
                    this._flex["CD_WCOP"] = row["CD_WCOP"].ToString();
                    this._flex["NM_WC"] = row["NM_WC"].ToString();
                    this._flex["NM_OP"] = row["NM_OP"].ToString();
                    this._flex["NM_WORKITEM"] = row["NM_WORKITEM"].ToString();
                    this._flex["CD_WORKITEM"] = row["CD_WORKITEM"].ToString();
                    if (row["FG_IO_APP"].ToString() != "007")
                    {
                        this._flex["NO_IO_MGMT"] = row["NO_IO"].ToString();
                        this._flex["NO_IOLINE"] = D.GetDecimal(row["NO_IOLINE"]);
                        this._flex["NO_IOLINE_MGMT"] = D.GetDecimal(row["NO_IOLINE"]);
                    }
                    this._flex["QT_INV"] = D.GetDecimal(row["QT_INV"]);
                    this._flex["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                    if (Config.MA_ENV.PJT형여부 == "Y")
                    {
                        this._flex["CD_UNIT"] = row["CD_PJT_ITEM"];
                        this._flex["NM_UNIT"] = row["PJT_NM_ITEM"];
                        this._flex["NO_DESIGN_UNIT"] = row["NO_PJT_DESIGN"];
                        this._flex["STND_UNIT"] = row["PJT_STND_ITEM"];
                        this._flex["NO_WBS"] = row["NO_WBS"];
                        this._flex["NO_CBS"] = row["NO_CBS"];
                        this._flex["SEQ_PROJECT"] = row["SEQ_PROJECT"];
                    }
                    this._flex["CD_PJT"] = row["CD_PJT"];
                    this._flex["NM_PROJECT"] = row["NM_PJT"];
                    this._flex["BARCODE"] = row["BARCODE"];
                    this._flex["YN_AM"] = this._header.CurrentRow["YN_AM"].ToString();
                    this._flex["FG_TPIO"] = D.GetString(this._header.CurrentRow["FG_GI"]);
                    this._flex["FG_IO"] = D.GetString(this._header.CurrentRow["FG_IO"]);
                    this._flex["NO_LOT"] = row["FG_LOT"];
                    this._flex["NO_SERL"] = row["NO_SERL"];
                    this._flex["FG_SERNO"] = !(D.GetString(row["FG_SERNO"]) == "002") ? (!(D.GetString(row["FG_SERNO"]) == "003") ? this.DD("미관리") : this.DD("S/N")) : this.DD("LOT");
                    if (BASIC.GetMAEXC_Menu("P_PU_CGI_REG", "PU_A00000022") == "100")
                    {
                        this._flex["CD_PARTNER"] = row["CD_PARTNER"];
                        this._flex["LN_PARTNER"] = row["LN_PARTNER"];
                    }
                    this._flex["WEIGHT"] = row["WEIGHT"];
                    this._flex["NM_ITEMGRP"] = row["NM_ITEMGRP"];
                    this._flex["MAT_ITEM"] = row["MAT_ITEM"];
                    this._flex["GRP_MFG"] = row["GRP_MFG"];
                    this._flex["STND_DETAIL_ITEM"] = row["STND_DETAIL_ITEM"];
                    this._flex["NM_PARTNER"] = row["NM_PARTNER"];
                    this._flex["QT_WEIGHT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(row["QT_GOOD_INV"]) * D.GetDecimal(row["WEIGHT"]));
                    this._flex.AddFinished();
                    this._flex.Col = this._flex.Cols.Fixed;
                }
                if (this.bUmSetting)
                    this.SETUM();
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                this._flex.Redraw = true;
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex.SumRefresh();
                this._flex.Redraw = true;
            }
        }

        private void btn_exch_app_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || !this.Chk환종)
                    return;
                string str = D.GetString(this.cbo환종.SelectedValue);
                decimal num1 = Unit.환율(DataDictionaryTypes.PU, this.cur환종.DecimalValue);
                if (str == "000")
                {
                    this.ShowMessage("KRW는 변경하실 수 없습니다.");
                }
                else if (num1 == 0M)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("환율"));
                }
                else
                {
                    DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y' AND CD_EXCH ='" + D.GetString(this.cbo환종.SelectedValue) + "'");
                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    }
                    else
                    {
                        foreach (DataRow dataRow in dataRowArray)
                        {
                            dataRow["RT_EXCH"] = num1;
                            dataRow["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]) * num1);
                            dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM"]) * D.GetDecimal(dataRow["QT_GOOD_INV"]));
                        }
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("환율변경"));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 화면이동()
        {
            DataSet dataSet = this._biz.Search(this.rt_no_io);
            if (dataSet.Tables[0].Rows.Count <= 0)
                return;
            this._flex.Redraw = false;
            this._header.SetDataTable(dataSet.Tables[0]);
            this._flex.Binding = dataSet.Tables[1];
            this.ctx프로젝트.CodeValue = D.GetString(dataSet.Tables[0].Rows[0]["CD_PJT"]);
            this.ctx프로젝트.CodeName = D.GetString(dataSet.Tables[0].Rows[0]["NM_PROJECT"]);
            this.bp_CD_SL.CodeValue = this._flex.DataTable.Rows[0]["CD_SL"].ToString();
            this.bp_CD_SL.CodeName = this._flex.DataTable.Rows[0]["NM_SL"].ToString();
            if (this._flex.DataTable.Rows.Count > 0)
            {
                this.btn추가.Enabled = true;
                this.btn요청적용.Enabled = false;
            }
            else
            {
                this.btn추가.Enabled = true;
                this.btn요청적용.Enabled = true;
            }
            this._flex.Redraw = true;
        }

        private void btn_UM_APP_Click(object sender, EventArgs e)
        {
            if (!this._flex.HasNormalRow && !this.bUmSetting)
                return;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            P_PU_UM_PRIORITIZE_PO_SUB umPrioritizePoSub = new P_PU_UM_PRIORITIZE_PO_SUB(S_CGI_REG.Default.CGI_UM_SETTING);
            if (umPrioritizePoSub.ShowDialog() != DialogResult.OK)
                return;
            S_CGI_REG.Default.CGI_UM_SETTING = umPrioritizePoSub.Rtn_stting;
            S_CGI_REG.Default.Save();
        }

        private void SETUM()
        {
            // ISSUE: unable to decompile the method.
        }

        public bool ChkAM_KYOTECH()
        {
            decimal num = 0M;
            DataTable amFromCodeKyotech = this._biz.getAmFromCode_KYOTECH();
            string str = string.Empty;
            if (amFromCodeKyotech != null && amFromCodeKyotech.Rows.Count > 0)
                num = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(amFromCodeKyotech.Rows[0]["CD_FLAG1"]));
            DataRow[] dataRowArray = this._flex.DataTable.Select("ISNULL(AM, 0) >= " + num, "", DataViewRowState.CurrentRows);
            foreach (DataRow dataRow in dataRowArray)
                str = str + "\n품목코드 : " + D.GetString(dataRow["CD_ITEM"]) + "     & 출고금액 : " + D.GetDecimal(dataRow["AM"]).ToString("###,###,###,##0.####") + "     & 기준금액 : " + num.ToString("###,###,###,##0.####");
            return dataRowArray == null || dataRowArray.Length <= 0 || this.ShowDetailMessage(this.DD("출고금액이 기준금액보다 크거나 같은 데이터가 존재합니다..\v그대로 저장하시겠습니까?"), "", str.ToString(), "QY2") == DialogResult.Yes;
        }
    }
}

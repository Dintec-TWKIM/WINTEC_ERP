using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using DzHelpFormLib;
using pur;
using sale;
using Dintec;

namespace cz
{
    public partial class P_CZ_PU_REQR_REG : PageBase
    {
        #region 초기화 & 전역변수
        private P_CZ_PU_REQR_REG_BIZ _biz = null;
        private FreeBinding _header = null;
        private bool _isChagePossible = true;
        private string 매입유무 = string.Empty;
        private string m_sEnv_FG_TAX = "000";
        private string PJT형사용 = string.Empty;
        private string UNIT사용 = string.Empty;
        private string 전용설정 = "000";
        private string m_sEnv = string.Empty;
        private string 반품발주사용여부 = BASIC.GetMAEXC("반품발주사용여부");
        private string gyn_purchase;
        private string gfg_tppurchase;
        private string strNO_RCV;
        private Decimal NO_RCV_LINE;
        private string strSOURCE;
        private string 처리구분 = "001";
        private string 단가유형 = "001";

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

        public P_CZ_PU_REQR_REG()
        {
            try
            {
                StartUp.Certify(this);
                this.InitializeComponent();
                this.MainGrids = new FlexGrid[] { this._flex };
                this.DataChanged += new EventHandler(this.Page_DataChanged);
                this._header = new FreeBinding();
                this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);
                this.MA_ENV_환경설정();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void MA_ENV_환경설정()
        {
            this.PJT형사용 = Config.MA_ENV.PJT형여부;
            this.UNIT사용 = Config.MA_ENV.YN_UNIT;
        }

        public P_CZ_PU_REQR_REG(string strNO_RCV, Decimal NO_RCV_LINE, string strSOURCE)
        {
            try
            {
                StartUp.Certify(this);
                this.InitializeComponent();
                this.strNO_RCV = strNO_RCV;
                this.NO_RCV_LINE = NO_RCV_LINE;
                this.strSOURCE = strSOURCE;
                this.MainGrids = new FlexGrid[] { this._flex };
                this.DataChanged += new EventHandler(this.Page_DataChanged);
                this._header = new FreeBinding();
                this._header.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ControlEnabledDisable(bool state)
        {
            //this.txt의뢰번호.Enabled = state;
            this.ctx매입처.Enabled = state;
            //this.ctx반품형태.Enabled = state;
            //this.ctx담당자.Enabled = state;
            //this.cbo과세구분.Enabled = state;
            //this.cbo거래구분.Enabled = state;
            //this.cbo공장.Enabled = state;
            //this.dtp의뢰일자.Enabled = state;
            //this.cbo통화명.Enabled = state;
            //this.cur환율.Enabled = state;
            //this.cbo과세구분.Enabled = state;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_PU_REQR_REG_BIZ();
            this.m_sEnv = this._biz.EnvSearch();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.txt발주번호.KeyDown += new KeyEventHandler(this.txt발주번호_KeyDown);

            this.btn추가.Click += new EventHandler(this.btn추가_Click);
            this.btn삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn입고적용.Click += new EventHandler(this.btn입고적용_Click);
            this.btn발주적용.Click += new EventHandler(this.btn발주적용_Click);
            this.btn납기일자적용.Click += new EventHandler(this.btn납기일자적용_Click);

            this.dtp의뢰일자.Validated += new EventHandler(this.dtp의뢰일자_Validated);
            this.cbo통화명.Leave += new EventHandler(this.cbo통화명_Leave);

            this.cbo공장.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.cbo거래구분.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.cbo과세구분.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.cur환율.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.cbo통화명.KeyDown += new KeyEventHandler(this.Control_KeyEvent);
            this.txt비고.KeyDown += new KeyEventHandler(this.Control_KeyEvent);

            this.cbo거래구분.SelectionChangeCommitted += new EventHandler(this.cbo거래구분_SelectionChangeCommitted);
            this.cbo과세구분.SelectionChangeCommitted += new EventHandler(this.cbo과세구분_SelectionChangeCommitted);
            this.cbo통화명.SelectionChangeCommitted += new EventHandler(this.cbo통화명_SelectionChangeCommitted);

            this.ctx반품형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx담당자.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx반품형태.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);

            this._flex.DoubleClick += new EventHandler(this._flex_DoubleClick);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet dataSet = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                              string.Empty });
            this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
            this._header.ClearAndNewRow();
            this._flex.Binding = dataSet.Tables[1];

            this.InitCombo();
            this.InitControl();

            this._isChagePossible = true;
            DataTable partnerCodeSearch = this._biz.GetPartnerCodeSearch();

            //if (partnerCodeSearch.Rows.Count > 0 && (partnerCodeSearch.Rows[0]["CD_EXC"] != DBNull.Value && partnerCodeSearch.Rows[0]["CD_EXC"].ToString().Trim() != string.Empty))
            //    this.전용설정 = partnerCodeSearch.Rows[0]["CD_EXC"].ToString().Trim();

            //if (this.전용설정 == "100")
            //this.btn추가.Visible = false;

            this.btn추가.Visible = false;

            if (this.반품발주사용여부 == "Y")
                this.btn발주적용.Visible = true;

            this.m_sEnv_FG_TAX = ComFunc.전용코드("과세구분설정");
            this._flex.SetDataMap("TP_UM_TAX", this.GetComboData("N;PU_C000005").Tables[0], "CODE", "NAME");
            this.dtp납기일자.Mask = this.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp납기일자.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
            this.dtp납기일자.Text = this.MainFrameInterface.GetStringToday;
            this.화면이동();

            this.oneGrid1.UseCustomLayout = true;
            this.setNecessaryCondition(new object[] { this.bpPanelControl15.Name });
            this.oneGrid1.IsSearchControl = false;
            this.oneGrid1.InitCustomLayout();
        }

        private void setNecessaryCondition(object[] obj)
        {
            try
            {
                bool flag = true;
                List<Control> controlList = this.oneGrid1.GetControlList();

                for (int index1 = 0; index1 < controlList.Count; ++index1)
                {
                    BpPanelControl bpPanelControl = (BpPanelControl)controlList[index1];

                    for (int index2 = 0; index2 < obj.Length; ++index2)
                    {
                        if (bpPanelControl.Name != D.GetString(obj[index2]))
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }

                    bpPanelControl.IsNecessaryCondition = flag;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 화면이동()
        {
            if (!(D.GetString(this.strNO_RCV) != string.Empty)) return;

            DataSet dataSet = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                              this.strNO_RCV });
            this._header.SetDataTable(dataSet.Tables[0]);

            if (dataSet != null && dataSet.Tables.Count > 1)
            {
                DataTable ldt_head = dataSet.Tables[0];
                DataTable ldt_line = dataSet.Tables[1];
                this._flex.Binding = dataSet.Tables[1];
                this.Button_Enabled(ldt_head, ldt_line);

                if (!this._flex.HasNormalRow && !this._header.CurrentRow.IsNull(0))
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }

                this._header.AcceptChanges();
                this._flex.AcceptChanges();
                this.ControlEnabledDisable(false);
                //this.ctx구매그룹.Enabled = false;
            }
        }

        private void Button_Enabled(DataTable ldt_head, DataTable ldt_line)
        {
            DataRow[] dataRowArray1 = ldt_line.Select("QT_GR_MM > 0");
            this._isChagePossible = dataRowArray1 == null || dataRowArray1.Length <= 0;

            if (!this._isChagePossible)
            {
                this.btn입고적용.Enabled = false;
                this.btn추가.Enabled = false;
                this.btn삭제.Enabled = false;
            }
            else
            {
                DataRow[] dataRowArray2 = ldt_line.Select("NO_IOLINE_MGMT > 0");

                if (dataRowArray2 != null && dataRowArray2.Length > 0)
                {
                    this.btn입고적용.Enabled = true;
                    this.btn추가.Enabled = false;
                }
                else
                {
                    this.btn입고적용.Enabled = false;
                    this.btn추가.Enabled = true;
                }

                this.btn삭제.Enabled = true;
            }
        }

        private void InitCombo()
        {
            DataSet comboData = this.GetComboData("N;MA_PLANT", "N;PU_C000016", "N;MA_B000005", "N;MA_B000046");

            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";

            if (comboData.Tables[0].Rows.Count > 0 && this.cbo공장.SelectedValue == null)
            {
                this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
                this._header.CurrentRow["CD_PLANT"] = this.cbo공장.SelectedValue;
            }

            this.cbo통화명.DataSource = comboData.Tables[2];
            this.cbo통화명.DisplayMember = "NAME";
            this.cbo통화명.ValueMember = "CODE";

            this.cbo과세구분.DataSource = comboData.Tables[3];
            this.cbo과세구분.DisplayMember = "NAME";
            this.cbo과세구분.ValueMember = "CODE";
            this._flex.SetDataMap("FG_TAX", comboData.Tables[3], "CODE", "NAME");

            this.ctx구매그룹.SetCodeValue(Global.MainFrame.LoginInfo.PurchaseGroupCode);
            this._header.CurrentRow["CD_PURGRP"] = this.ctx구매그룹.GetCodeValue();
            this.ctx구매그룹.SetCodeName(Global.MainFrame.LoginInfo.PurchaseGroupName);

            this.cbo거래구분.DataSource = comboData.Tables[1];
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";
        }

        private void InitControl()
        {
            this.cur환율.Text = "1";
            //this.cur환율.Enabled = false;
            this.SettingTbTax(D.GetString(this.cbo과세구분.SelectedValue));

            this.dtp납기일자.Text = this.MainFrameInterface.GetStringToday;
        }

        private void SettingTbTax(string ps_taxp)
        {
            try
            {
                DataSet comboData = this.GetComboData("S;MA_CODEDTL_003");

                if (ps_taxp != null && ps_taxp != "")
                {
                    DataRow[] dataRowArray = comboData.Tables[0].Select(" CODE = '" + ps_taxp + "'");

                    if (dataRowArray != null && dataRowArray.Length > 0)
                        this._header.CurrentRow["VAT_RATE"] = this._flex.CDouble(dataRowArray[0]["VAT_RATE"]);
                }

                this._header.CurrentRow["FG_TAX"] = D.GetString(this.cbo과세구분.SelectedValue);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_ITEM", "품목코드", 110, false);
            this._flex.SetCol("NM_ITEM", "품목명", 130, false);
            this._flex.SetCol("STND_ITEM", "규격", 70, false);
            this._flex.SetCol("UNIT_IM", "재고단위", 70, false);
            this._flex.SetCol("NO_LOT", "LOT번호", 70, false);
            this._flex.SetCol("DT_LIMIT", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("CD_SL", "창고코드", 80, false);
            this._flex.SetCol("NM_SL", "창고명", 120, false);
            this._flex.SetCol("FG_TAX", "과세구분", 70, false);
            this._flex.SetCol("CD_UNIT_MM", "수배단위", 70, false);
            this._flex.SetCol("QT_REQ_MM", "의뢰량", 120, true, typeof(Decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("UM_EX_PO", "단가", 100, false, typeof(Decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("QT_REQ", "관리수량", 120, false, typeof(Decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("UM_EX", "관리단가", 100, false, typeof(Decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flex.SetCol("AM_EX", "금액", 100, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM", "원화금액", 100, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("VAT", "부가세", 100, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("NO_IO_MGMT", "수불번호", 150, false);
            this._flex.SetCol("NO_PO_MGMT", "발주번호", 150, false);
            this._flex.SetCol("CD_PJT", "프로젝트코드", 150, true);
            this._flex.SetCol("NM_PROJECT", "프로젝트명", 150, false);
            this._flex.SetCol("DC_RMK", "비고", 150, true);
            this._flex.SetCol("TP_UM_TAX", "부가세여부", 70, false);
            this._flex.SetCol("AM_TOTAL", "총금액", 100, 17, false, typeof(Decimal), FormatTpType.MONEY);

            if (App.SystemEnv.PROJECT사용)
                this._flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(Decimal));

            if (this.PJT형사용 == "Y")
            {
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
                    this._flex.SetCol("QTY_ACT", "CBS예산수량", 120, false, typeof(Decimal), FormatTpType.QUANTITY);
                    this._flex.SetCol("UNT_ACT", "CBS예산단가", 100, false, typeof(Decimal), FormatTpType.UNIT_COST);
                    this._flex.SetCol("AMT_ACT", "CBS예산금액", 100, false, typeof(Decimal), FormatTpType.MONEY);
                }
                this._flex.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flex.SetCol("PJT_NM_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flex.SetCol("PJT_STND_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flex.SetCol("NO_CBS", "CBS번호", 140, false, typeof(string));
            }

            if (App.SystemEnv.PROJECT사용)
            {
                if (Config.MA_ENV.YN_UNIT == "Y")
                    this._flex.VerifyNotNull = new string[] { "CD_ITEM",
                                                              "DT_LIMIT",
                                                              "NM_PROJECT",
                                                              "SEQ_PROJECT" };
                else
                    this._flex.VerifyNotNull = new string[] { "CD_ITEM",
                                                              "DT_LIMIT",
                                                              "NM_PROJECT" };
            }
            else
                this._flex.VerifyNotNull = new string[] { "CD_ITEM",
                                                          "DT_LIMIT" };

            this._flex.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT_IM", "CD_UNIT_MM", "NO_LOT", "NM_SL", "NO_IO_MGMT", "NO_PO_MGMT");
            this._flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                             "NM_ITEM",
                                                                                                             "STND_ITEM",
                                                                                                             "UNIT_IM" }, new string[] { "CD_ITEM",
                                                                                                                                         "NM_ITEM",
                                                                                                                                         "STND_ITEM",
                                                                                                                                         "UNIT_IM" }, ResultMode.SlowMode);
            this._flex.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL",
                                                                                                       "NM_SL" }, new string[] { "CD_SL",
                                                                                                                                 "NM_SL" });
            if (this.UNIT사용 == "Y")
                this._flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                        "NM_PROJECT",
                                                                                                        "CD_PJT_ITEM",
                                                                                                        "PJT_NM_ITEM",
                                                                                                        "PJT_STND_ITEM",
                                                                                                        "SEQ_PROJECT" }, new string[] { "NO_PROJECT",
                                                                                                                                        "NM_PROJECT",
                                                                                                                                        "CD_PJT_ITEM",
                                                                                                                                        "NM_PJT_ITEM",
                                                                                                                                        "PJT_ITEM_STND",
                                                                                                                                        "SEQ_PROJECT" });
            else
                this._flex.SetCodeHelpCol("CD_PJT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "CD_PJT",
                                                                                                        "NM_PROJECT" }, new string[] { "NO_PROJECT",
                                                                                                                                       "NM_PROJECT" });
            this._flex.VerifyAutoDelete = new string[] { "CD_ITEM",
                                                         "NM_ITEM" };

            this._flex.SettingVersion = "1.0.0.3";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            this._flex.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex.AddRow += new EventHandler(this.btn추가_Click);
            this._flex.VerifyCompare(this._flex.Cols["QT_REQ_MM"], 0, OperatorEnum.Greater);
        }
        #endregion

        #region 메인버튼 이벤트
        protected override bool IsChanged()
        {
            if (base.IsChanged()) return true;

            return this.헤더변경여부;
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

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsChanged()) return;

                this.ToolBarSaveButtonEnabled = true;
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

                P_PU_REQR_SUB pPuReqrSub = new P_PU_REQR_SUB(this.MainFrameInterface);

                if (pPuReqrSub.ShowDialog() == DialogResult.OK)
                {
                    DataSet dataSet = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                      pPuReqrSub.m_NoRcv });
                    this._header.SetDataTable(dataSet.Tables[0]);

                    if (dataSet != null && dataSet.Tables.Count > 1)
                    {
                        DataTable ldt_head = dataSet.Tables[0];
                        DataTable ldt_line = dataSet.Tables[1];
                        this._flex.Binding = dataSet.Tables[1];
                        this.Button_Enabled(ldt_head, ldt_line);

                        if (!this._flex.HasNormalRow && !this._header.CurrentRow.IsNull(0))
                        {
                            this.ShowMessage(PageResultMode.SearchNoData);
                        }

                        this._header.AcceptChanges();
                        this._flex.AcceptChanges();
                        this.ControlEnabledDisable(false);
                        //this.ctx구매그룹.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd()) return;

                Debug.Assert(this._header.CurrentRow != null);
                Debug.Assert(this._flex.DataTable != null);
                this._flex.DataTable.Rows.Clear();
                this._flex.AcceptChanges();
                this._header.ClearAndNewRow();
                this.InitControl();
                this.ControlEnabledDisable(true);
                this.Button_Enabled(this._header.CurrentRow.Table, this._flex.DataTable);
                //this.ctx구매그룹.Enabled = true;

                if (this.LoginInfo.CdPlant.ToString() != "")
                    this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant.ToString();

                this.btn입고적용.Enabled = true;
                this.ctx구매그룹.SetCodeValue(Global.MainFrame.LoginInfo.PurchaseGroupCode);
                this.ctx구매그룹.SetCodeName(Global.MainFrame.LoginInfo.PurchaseGroupName);
                this._header.CurrentRow["CD_PURGRP"] = this.ctx구매그룹.GetCodeValue();
                this._header.CurrentRow["FG_UM"] = this.단가유형;
                this.cbo과세구분.SelectedIndex = 0;
                this.SettingTbTax(this.cbo과세구분.SelectedValue.ToString());
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

                if (!this.BeforeDelete() || !this._flex.HasNormalRow)
                    return;

                this._biz.Delete(new object[] { this._header.CurrentRow["NO_RCV"].ToString(),
                                                this.LoginInfo.CompanyCode });

                this.OnToolBarAddButtonClicked(sender, e);
                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave() || !this.Check_OrderApp_Able())
                return false;

            if (this._flex.HasNormalRow)
                return true;

            this.OnToolBarDeleteButtonClicked(null, null);

            return false;
        }

        private bool Check_OrderApp_Able()
        {
            if (D.GetString(this.dtp의뢰일자.Text) == "")
            {
                this.ShowMessage("WK1_004", this.lbl의뢰일자.Text);
                return false;
            }

            if (D.GetString(this.cbo공장.SelectedValue) == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
                return false;
            }

            if (D.GetString(this.ctx구매그룹.CodeValue) == "")
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl구매그룹.Text });
                return false;
            }

            if (D.GetString(this.ctx매입처.CodeValue) == "")
            {
                this.MainFrameInterface.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl매입처.Text });
                return false;
            }

            if (D.GetString(this.ctx반품형태.CodeValue) == "")
            {
                this.MainFrameInterface.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl반품형태.Text });
                return false;
            }

            if (D.GetString(this.cbo거래구분.SelectedValue) == "")
            {
                this.MainFrameInterface.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl거래구분.Text });
                return false;
            }

            if (D.GetString(this.ctx담당자.CodeValue) == "")
            {
                this.MainFrameInterface.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자.Text });
                return false;
            }

            if (D.GetString(this.cbo과세구분.SelectedValue) == "")
            {
                this.MainFrameInterface.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl과세구분.Text });
                return false;
            }

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.IsChanged() || !this.MsgAndSave(PageActionMode.Save))
                    return;

                this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;
                if (!this.DetailCheck()) return false;

                if (this.추가모드여부)
                {
                    string str = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "05", this.dtp의뢰일자.Text.Substring(0, 6));
                    this._header.CurrentRow["NO_RCV"] = str;
                    this.txt의뢰번호.Text = str;
                    this._header.CurrentRow["YN_RETURN"] = "Y";
                    this._header.CurrentRow["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                }

                this.Insert_flex_from_Header(this._flex.DataTable);
                DataTable changes1 = this._header.GetChanges();
                DataTable changes2 = this._flex.GetChanges();

                if (changes1 == null && changes2 == null) return true;

                string detail = "";

                if (changes2 != null)
                {
                    foreach (DataRow dataRow in changes2.Select())
                    {
                        Decimal num = D.GetDecimal(dataRow["RATE_EXCHG"]);

                        if (num == 0)
                            num = 1;

                        if ((D.GetDecimal(dataRow["QT_REQ_MM"]) * num).ToString("#,###,###,###.###") != D.GetDecimal(dataRow["QT_REQ"]).ToString("#,###,###,###.###"))
                            detail = detail + D.GetString(dataRow["CD_ITEM"]) + "\n";
                    }

                    if (detail != "" && this.ShowDetailMessage("(의뢰량(재고수량) x 환산수량) 과 관리수량(수배수량)의 값이 불일치 하는건이 존재합니다. 그대로 진행하시겠습니까?", "", detail, "QY2") != DialogResult.Yes)
                        return false;
                }

                if (!this._biz.Save(changes1, changes2))
                    return false;

                this._header.AcceptChanges();
                this._flex.AcceptChanges();
                this.btn삭제.Enabled = true;

                return true;
            }
            catch (coDbException ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
                return false;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
                return false;
            }
        }

        private void Insert_flex_from_Header(DataTable dtL)
        {
            foreach (DataRow dataRow in dtL.Select())
            {
                dataRow["NO_RCV"] = this._header.CurrentRow["NO_RCV"];
                dataRow["YN_RETURN"] = "Y";
                dataRow["CD_EXCH"] = this._header.CurrentRow["CD_EXCH"];
                dataRow["RT_EXCH"] = this._header.CurrentRow["RT_EXCH"];
                dataRow["YN_PURCHASE"] = this._header.CurrentRow["YN_PURCHASE"];
                dataRow["FG_TPPURCHASE"] = this._header.CurrentRow["FG_TPPURCHASE"];
                dataRow["CD_PURGRP"] = this._header.CurrentRow["CD_PURGRP"];
                dataRow["FG_RCV"] = this._header.CurrentRow["FG_RCV"];
                dataRow["FG_TRANS"] = this._header.CurrentRow["FG_TRANS"];
                dataRow["FG_TAXP"] = 처리구분;
                dataRow["NO_EMP"] = this._header.CurrentRow["NO_EMP"];

                if (dataRow["FG_TAX"].ToString() == string.Empty)
                    dataRow["FG_TAX"] = this._header.CurrentRow["FG_TAX"];
            }
        }

        private bool DetailCheck()
        {
            foreach (DataRow dataRow in this._flex.DataTable.Select("", "", DataViewRowState.CurrentRows))
            {
                if (D.GetString(dataRow["CD_SL"]) == string.Empty)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "창고" });
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 컨트롤 이벤트
        private void txt발주번호_KeyDown (object sender, KeyEventArgs e)
        {
            string query;
            DataTable dt;

            try
            {
                if (e.KeyCode != Keys.Enter) return;

                query = @"SELECT PH.CD_PARTNER,
	                      	     MP.LN_PARTNER,
	                      	     PH.CD_PURGRP,
	                      	     PG.NM_PURGRP,
	                      	     PH.FG_TRANS,
	                      	     PH.FG_TAX,
	                      	     PH.CD_EXCH 
	                      FROM PU_POH PH WITH(NOLOCK)
	                      JOIN (SELECT PL.CD_COMPANY, PL.NO_PO 
	                      	    FROM PU_POL PL WITH(NOLOCK)
                                WHERE ISNULL(PL.QT_REQ, 0) > 0
	                      	    GROUP BY PL.CD_COMPANY, PL.NO_PO) PL
	                      ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
	                      LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = PH.CD_COMPANY AND MP.CD_PARTNER = PH.CD_PARTNER
	                      LEFT JOIN MA_PURGRP PG WITH(NOLOCK) ON PG.CD_COMPANY = PH.CD_COMPANY AND PG.CD_PURGRP = PH.CD_PURGRP
	                      WHERE PH.CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND PH.NO_PO = '" + this.txt발주번호.Text + "'";

                dt = DBHelper.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    this.ctx매입처.CodeValue = D.GetString(dt.Rows[0]["CD_PARTNER"]);
                    this.ctx매입처.CodeName = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                    this._header.CurrentRow["CD_PARTNER"] = this.ctx매입처.CodeValue;
                    this._header.CurrentRow["LN_PARTNER"] = this.ctx매입처.CodeName;

                    this.ctx구매그룹.CodeValue = D.GetString(dt.Rows[0]["CD_PURGRP"]);
                    this.ctx구매그룹.CodeName = D.GetString(dt.Rows[0]["NM_PURGRP"]);
                    this._header.CurrentRow["CD_PURGRP"] = this.ctx구매그룹.CodeValue;
                    this._header.CurrentRow["NM_PURGRP"] = this.ctx구매그룹.CodeName;

                    this.cbo거래구분.SelectedValue = D.GetString(dt.Rows[0]["FG_TRANS"]);
                    this._header.CurrentRow["FG_TRANS"] = this.cbo거래구분.SelectedValue;

                    this.cbo거래구분_SelectionChangeCommitted(null, null);

                    this.cbo과세구분.SelectedValue = D.GetString(dt.Rows[0]["FG_TAX"]);
                    this._header.CurrentRow["FG_TAX"] = this.cbo거래구분.SelectedValue;

                    this.cbo과세구분_SelectionChangeCommitted(null, null);

                    this.cbo통화명.SelectedValue = D.GetString(dt.Rows[0]["CD_EXCH"]);
                    this._header.CurrentRow["CD_EXCH"] = cbo통화명.SelectedValue;

                    this.cbo통화명_SelectionChangeCommitted(null, null);
                }   
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
                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = true;

                if (!this.Check_OrderApp_Able())
                    return;

                P_CZ_PU_REQR_REG_SUB dialog = new P_CZ_PU_REQR_REG_SUB(this.txt발주번호.Text);

                if (dialog.ShowDialog() == DialogResult.OK)
                    this.InserGridtAddREQ(dialog.입고테이블);
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

                Decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                double num1 = 0.0;
                this._flex.Redraw = false;

                for (int index1 = 0; index1 < pdt_Line.Rows.Count; ++index1)
                {
                    ++maxValue;
                    DataRow dataRow = pdt_Line.Rows[index1];
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex["NO_RCV"] = this.txt의뢰번호.Text;
                    this._flex["NO_LINE"] = maxValue;
                    this._flex["CD_ITEM"] = dataRow["CD_ITEM"];
                    this._flex["NM_ITEM"] = dataRow["NM_ITEM"];
                    this._flex["STND_ITEM"] = dataRow["STND_ITEM"];
                    this._flex["UNIT_IM"] = dataRow["UNIT_IM"];
                    this._flex["CD_UNIT_MM"] = dataRow["CD_UNIT_MM"];
                    this._flex["DT_LIMIT"] = this.MainFrameInterface.GetStringToday;
                    this._flex["NO_LOT"] = dataRow["NO_LOT"];
                    this._flex["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_IO"]));
                    this._flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_IO_MM"]));
                    this._flex["RATE_EXCHG"] = dataRow["RATE_EXCHG"];

                    if (this.PJT형사용 == "Y")
                    {
                        this._flex["SEQ_PROJECT"] = dataRow["SEQ_PROJECT"];
                        this._flex["CD_PJT_ITEM"] = dataRow["CD_PJT_ITEM"];
                        this._flex["PJT_NM_ITEM"] = dataRow["PJT_NM_ITEM"];
                        this._flex["PJT_STND_ITEM"] = dataRow["PJT_STND_ITEM"];
                        this._flex["NO_WBS"] = dataRow["NO_WBS"];
                        this._flex["NO_CBS"] = dataRow["NO_CBS"];
                    }

                    if (App.SystemEnv.PMS사용)
                    {
                        this._flex["CD_CSTR"] = dataRow["CD_CSTR"];
                        this._flex["DL_CSTR"] = dataRow["DL_CSTR"];
                        this._flex["NM_CSTR"] = dataRow["NM_CSTR"];
                        this._flex["SIZE_CSTR"] = dataRow["SIZE_CSTR"];
                        this._flex["UNIT_CSTR"] = dataRow["UNIT_CSTR"];
                        this._flex["QTY_ACT"] = dataRow["QTY_ACT"];
                        this._flex["UNT_ACT"] = dataRow["UNT_ACT"];
                        this._flex["AMT_ACT"] = dataRow["AMT_ACT"];
                    }

                    if (this.txt유무환구분.Text == "N")
                    {
                        this._flex["UM_EX"] = 0;
                        this._flex["AM_EX"] = 0;
                        this._flex["UM"] = 0;
                        this._flex["AM"] = 0;
                        this._flex["VAT"] = 0;
                    }
                    else
                    {
                        double num2 = this._flex.CDouble(dataRow["UM_EX"].ToString()) * this._flex.CDouble(dataRow["QT_IO"].ToString());
                        double num3 = this._flex.CDouble(dataRow["UM_EX"].ToString());
                        FlexGrid flexGrid1 = this._flex;
                        Decimal decimalValue = this.cur환율.DecimalValue;
                        string str1 = decimalValue.ToString();
                        double num4 = flexGrid1.CDouble(str1);
                        double num5 = num3 * num4;
                        Decimal num6 = D.GetDecimal(dataRow["RATE_EXCHG"]) == 0 ? 1 : D.GetDecimal(dataRow["RATE_EXCHG"]);

                        if (this.m_sEnv_FG_TAX == "000" || string.IsNullOrEmpty(dataRow["FG_TAX"].ToString()))
                        {
                            this._flex["FG_TAX"] = D.GetString(this._header.CurrentRow["FG_TAX"]);
                            this._flex["RATE_VAT"] = D.GetString(this._header.CurrentRow["VAT_RATE"]);
                        }
                        else
                        {
                            this._flex["FG_TAX"] = D.GetString(dataRow["FG_TAX"]);
                            this._flex["RATE_VAT"] = D.GetString(dataRow["RATE_VAT"]);
                        }

                        if (D.GetString(dataRow["TP_UM_TAX"]) == "001")
                        {
                            num1 = this._flex.CDouble(this._flex["RATE_VAT"]) / 100.0;
                            this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]));
                            this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["AM_EX"]));
                            this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM"]));
                            this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["AM"]));
                            this._flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]) * num6);
                            this._flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["VAT"]));
                        }
                        else
                        {
                            double num7 = this._flex.CDouble(this._flex["RATE_VAT"]) / 100.0;
                            this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]));
                            this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(num2));
                            this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(num5));
                            FlexGrid flexGrid2 = this._flex;
                            string index2 = "AM";
                            int num8 = 3;
                            double num9 = num2;
                            FlexGrid flexGrid3 = this._flex;
                            decimalValue = this.cur환율.DecimalValue;
                            string str2 = decimalValue.ToString();
                            double num10 = flexGrid3.CDouble(str2);
                            Decimal decimal1 = D.GetDecimal((num9 * num10));
                            flexGrid2[index2] = (System.ValueType)Unit.원화금액((DataDictionaryTypes)num8, decimal1);
                            this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex["QT_REQ"]) == 0 ? 0 : this._flex.CDecimal(this._flex["AM"]) / this._flex.CDecimal(this._flex["QT_REQ"]));
                            this._flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal((D.GetDecimal(dataRow["UM_EX"]) * num6)));
                            FlexGrid flexGrid4 = this._flex;
                            string index3 = "VAT";
                            int num11 = 3;
                            double num12 = num2;
                            FlexGrid flexGrid5 = this._flex;
                            decimalValue = this.cur환율.DecimalValue;
                            string str3 = decimalValue.ToString();
                            double num13 = flexGrid5.CDouble(str3);
                            Decimal decimal2 = D.GetDecimal((num12 * num13 * num7));
                            flexGrid4[index3] = Unit.원화금액((DataDictionaryTypes)num11, decimal2);
                        }

                        this._flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["AM"]) + D.GetDecimal(dataRow["VAT"]));
                    }

                    this._flex["TP_UM_TAX"] = D.GetString(dataRow["TP_UM_TAX"]);
                    this._flex["CD_PJT"] = dataRow["CD_PJT"];
                    this._flex["NM_PROJECT"] = dataRow["NM_PROJECT"];
                    this._flex["NO_PO_MGMT"] = dataRow["NO_PSO_MGMT"];
                    this._flex["NO_POLINE_MGMT"] = dataRow["NO_PSOLINE_MGMT"];
                    this._flex["CD_PURGRP"] = dataRow["CD_GROUP"];
                    this._flex["NO_IOLINE_MGMT"] = dataRow["NO_IOLINE"];
                    this._flex["NO_IO_MGMT"] = dataRow["NO_IO"];
                    this._flex["CD_SL"] = dataRow["CD_SL"];
                    this._flex["NM_SL"] = dataRow["NM_SL"];
                    this._flex["NO_PO"] = "";
                    this._flex["NO_POLINE"] = 0;
                    this._flex["RT_EXCH"] = !(D.GetDecimal(dataRow["RT_EXCH"]) == 0) || !(D.GetString(this._header.CurrentRow["CD_EXCH"]) == "000") ? D.GetDecimal(dataRow["RT_EXCH"]) : 1;
                }

                this._flex.Redraw = true;
                this.ControlEnabledDisable(false);
                //this.ctx구매그룹.Enabled = true;
                this.btn추가.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn발주적용_Click(object sender, EventArgs e)
        {
            try
            {
                this.btn추가.Enabled = true;
                this.btn삭제.Enabled = true;

                if (!this.Check_OrderApp_Able()) return;

                P_PU_REQPO_SUB2 pPuReqpoSuB2 = new P_PU_REQPO_SUB2(D.GetString(this.cbo거래구분.SelectedValue), D.GetString(this.cbo공장.SelectedValue), this.ctx매입처.CodeValue, this.ctx매입처.CodeName, this._flex.DataTable, D.GetString(this.ctx구매그룹.CodeValue), D.GetString(this.ctx구매그룹.CodeName), D.GetString(this.ctx반품형태.CodeValue), D.GetString(this.ctx담당자.CodeValue), D.GetString(this.ctx담당자.CodeName), D.GetString(this.cbo과세구분.SelectedValue));
                this.btn입고적용.Enabled = false;

                if (pPuReqpoSuB2.ShowDialog() == DialogResult.OK)
                {
                    //this.cbo거래구분.Enabled = false;

                    foreach (DataColumn dataColumn in this._flex.DataTable.Clone().Columns)
                    {
                        if (dataColumn.DataType == typeof(Decimal))
                            dataColumn.DefaultValue = 0;
                    }

                    this.InserGridPOtAddREQ(pPuReqpoSuB2.gdt_return);
                    this.ToolBarDeleteButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InserGridPOtAddREQ(DataTable pdt_Line)
        {
            try
            {
                if (pdt_Line == null || pdt_Line.Rows.Count <= 0) return;

                Decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                this._flex.Redraw = false;

                for (int index = 0; index < pdt_Line.Rows.Count; ++index)
                {
                    ++maxValue;
                    DataRow dataRow = pdt_Line.Rows[index];
                    this._flex.Rows.Add();
                    this._flex.Row = this._flex.Rows.Count - 1;
                    this._flex["NO_LINE"] = maxValue;
                    this._flex["NO_IOLINE"] = maxValue;
                    this._flex["CD_ITEM"] = dataRow["CD_ITEM"];
                    this._flex["NM_ITEM"] = dataRow["NM_ITEM"];
                    this._flex["STND_ITEM"] = dataRow["STND_ITEM"];

                    if (dataRow["CD_UNIT_MM"] == null)
                        dataRow["CD_UNIT_MM"] = "";

                    this._flex["CD_UNIT_MM"] = dataRow["CD_UNIT_MM"];
                    this._flex["UNIT_IM"] = dataRow["UNIT_IM"];
                    this._flex["RATE_EXCHG"] = dataRow["RATE_EXCHG"];
                    this._flex["RATE_VAT"] = dataRow["RT_VAT"];
                    this._flex["DT_LIMIT"] = dataRow["DT_LIMIT"];
                    this._flex["QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_REQ_MM"]));
                    this._flex["QT_REAL"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_REQ_MM"]));
                    this._flex["QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_REQ"]));
                    this._flex["QT_PASS"] = 0;
                    this._flex["QT_REJECTION"] = 0;
                    this._flex["YN_INSP"] = "N";
                    this._flex["YN_AM"] = dataRow["YN_AM"];

                    if (D.GetString(this.txt유무환구분.Text) == "N")
                    {
                        this._flex["UM_EX"] = 0;
                        this._flex["AM_EX"] = 0;
                        this._flex["UM"] = 0;
                        this._flex["AM"] = 0;
                        this._flex["VAT"] = 0;
                        this._flex["AM_TOTAL"] = 0;
                    }
                    else
                    {
                        this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX"]));
                        this._flex["AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["JAN_AM_EX"]));
                        this._flex["UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM"]));
                        this._flex["AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["JAN_AM"]));
                        this._flex["UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_EX_PO"]));
                        this._flex["VAT"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(dataRow["VAT"].ToString()));
                        this._flex["AM_TOTAL"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["JAN_AM"]) + D.GetDecimal(dataRow["VAT"]));
                    }

                    if (App.SystemEnv.PMS사용)
                    {
                        this._flex["CD_CSTR"] = dataRow["CD_CSTR"];
                        this._flex["DL_CSTR"] = dataRow["DL_CSTR"];
                        this._flex["NM_CSTR"] = dataRow["NM_CSTR"];
                        this._flex["SIZE_CSTR"] = dataRow["SIZE_CSTR"];
                        this._flex["UNIT_CSTR"] = dataRow["UNIT_CSTR"];
                        this._flex["QTY_ACT"] = dataRow["QTY_ACT"];
                        this._flex["UNT_ACT"] = dataRow["UNT_ACT"];
                        this._flex["AMT_ACT"] = dataRow["AMT_ACT"];
                    }

                    this._flex["CD_PJT"] = dataRow["CD_PJT"];
                    this._flex["CD_PURGRP"] = dataRow["CD_PURGRP"];
                    this._flex["NO_PO"] = dataRow["NO_PO"];
                    this._flex["NO_PO_MGMT"] = dataRow["NO_PO"];
                    this._flex["NO_POLINE"] = dataRow["NO_LINE"];
                    this._flex["AM_EXRCV"] = 0;
                    this._flex["AM_RCV"] = 0;
                    this._flex["QT_GR_MM"] = 0;
                    this._flex["RT_CUSTOMS"] = 0;
                    this._flex["YN_RETURN"] = dataRow["YN_RETURN"];
                    this._flex["FG_TPPURCHASE"] = dataRow["FG_PURCHASE"];
                    this._flex["YN_PURCHASE"] = dataRow["FG_PURCHASE"].ToString() != "" ? "Y" : "N";
                    this._flex["FG_POST"] = dataRow["FG_POST"];
                    this._flex["NM_FG_POST"] = dataRow["NM_FG_POST"];
                    this._flex["FG_RCV"] = dataRow["FG_RCV"];
                    this._flex["NM_FG_RCV"] = dataRow["NM_QTIOTP"];
                    this._flex["FG_TRANS"] = dataRow["FG_TRANS"];
                    this._flex["FG_TAX"] = D.GetString(dataRow["FG_TAX"]);
                    this._flex["CD_EXCH"] = dataRow["CD_EXCH"];
                    this._header.CurrentRow["CD_EXCH"] = dataRow["CD_EXCH"];
                    this._flex["RT_EXCH"] = !(D.GetDecimal(dataRow["RT_EXCH"]) == 0) || !(D.GetString(dataRow["CD_EXCH"]) == "000") ? D.GetDecimal(dataRow["RT_EXCH"]) : 1;
                    this._flex["NO_LOT"] = dataRow["NO_LOT"];
                    this._flex["NO_IO_MGMT"] = "";
                    this._flex["NO_IOLINE_MGMT"] = 0;
                    this._flex["NO_POLINE_MGMT"] = 0;
                    this._flex["NO_PO"] = dataRow["NO_PO"];
                    this._flex["NO_POLIN"] = dataRow["NO_LINE"];
                    this._flex["NO_TO"] = "";
                    this._flex["NO_TO_LINE"] = 0;
                    this._flex["CD_SL"] = dataRow["CD_SL"];
                    this._flex["NM_SL"] = dataRow["NM_SL"];
                    this._flex["NO_LC"] = "";
                    this._flex["NO_LCLINE"] = 0;
                    this._flex["FG_TAXP"] = 처리구분;
                    this._flex["NO_EMP"] = dataRow["NO_EMP"];
                    this._flex["CD_PJT"] = dataRow["CD_PJT"];
                    this._flex["NM_PROJECT"] = dataRow["NM_PROJECT"];
                    this._flex["VAT_CLS"] = 0;
                    this._flex["YN_AUTORCV"] = dataRow["YN_AUTORCV"];
                    this._flex["YN_REQ"] = dataRow["YN_RCV"];
                    this._flex["NM_SYSDEF"] = dataRow["NM_SYSDEF"];
                    this._flex["NM_KOR"] = dataRow["NM_KOR"];

                    if (dataRow["CD_EXCH"].ToString() != "")
                        this.cbo통화명.Text = dataRow["NM_SYSDEF"].ToString();

                    this._flex["PO_PRICE"] = dataRow["PO_PRICE"];
                    this._flex["NM_PURGRP"] = dataRow["NM_PURGRP"];
                    this._flex["AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["JAN_AM_EX"]));
                    this._flex["AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(dataRow["JAN_AM"]));
                    this._flex["REV_QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_REQ_MM"]));
                    this._flex["REV_AM_EXREQ"] = Unit.외화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["JAN_AM_EX"]));
                    this._flex["REV_AM_REQ"] = Unit.원화금액(DataDictionaryTypes.PU, this._flex.CDecimal(dataRow["JAN_AM"]));
                    this._flex["TP_UM_TAX"] = D.GetString(dataRow["TP_UM_TAX"]);
                }

                this.ctx매입처.SetCode(pdt_Line.Rows[0]["CD_PARTNER"].ToString(), pdt_Line.Rows[0]["LN_PARTNER"].ToString());
                this._flex.Redraw = true;
                this.ControlEnabledDisable(false);
                this.btn추가.Enabled = false;
                //this.ctx구매그룹.Enabled = true;
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
                if (!this.Check_OrderApp_Able() || !this.btn추가.Enabled) return;

                Decimal num = (D.GetDecimal(this._flex.GetMaxValue("NO_LINE")) + 1);
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex[this._flex.Row, "NO_RCV"] = this.txt의뢰번호.Text.ToString();
                this._flex[this._flex.Row, "NO_LINE"] = num;
                this._flex[this._flex.Row, "QT_REQ"] = 0;
                this._flex[this._flex.Row, "QT_GR"] = 0;
                this._flex[this._flex.Row, "UM_EX"] = 0;
                this._flex[this._flex.Row, "AM_EX"] = 0;
                this._flex[this._flex.Row, "UM"] = 0;
                this._flex[this._flex.Row, "AM"] = 0;
                this._flex[this._flex.Row, "VAT"] = 0;
                this._flex[this._flex.Row, "NO_IO_MGMT"] = "";
                this._flex[this._flex.Row, "NO_TO_LINE"] = 0;
                this._flex[this._flex.Row, "NO_LCLINE"] = 0;
                this._flex[this._flex.Row, "NO_POLINE_MGMT"] = 0;
                this._flex[this._flex.Row, "NO_IOLINE_MGMT"] = 0;
                this._flex[this._flex.Row, "CD_PURGRP"] = this._header.CurrentRow["CD_PURGRP"];
                this._flex[this._flex.Row, "NO_PO"] = "";
                this._flex[this._flex.Row, "NO_POLINE"] = 0;
                this._flex[this._flex.Row, "FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                this._flex[this._flex.Row, "RATE_VAT"] = this._header.CurrentRow["VAT_RATE"];

                if (this._flex.Rows.Count - this._flex.Rows.Fixed == 1)
                    this._flex[this._flex.Row, "DT_LIMIT"] = "";
                else
                    this._flex[this._flex.Row, "DT_LIMIT"] = this._flex[this._flex.Row - 1, "DT_LIMIT"];

                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this.ControlEnabledDisable(false);
                this.btn입고적용.Enabled = false;
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
                    this.ControlEnabledDisable(true);
                    //this.ctx구매그룹.Enabled = true;
                    this.btn추가.Enabled = true;
                    this.btn입고적용.Enabled = true;
                }

                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_KeyEvent(object sender, KeyEventArgs e)
        {
            string name = ((Control)sender).Name;

            if (name == this.txt비고.Text)
            {
                if (e.KeyData != Keys.Return && e.KeyData != Keys.Tab) return;
                this.btn추가_Click(null, null);
            }
            else
            {
                if (e.KeyData != Keys.Return) return;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult != DialogResult.OK) return;

                DataRow[] rows = e.HelpReturn.Rows;
                switch (e.HelpID)
                {
                    case HelpID.P_MA_EMP_SUB:
                        this._header.CurrentRow["CD_DEPT"] = e.HelpReturn.Rows[0]["CD_DEPT"].ToString();
                        break;
                    case HelpID.P_PU_EJTP_SUB:
                        this.gyn_purchase = e.HelpReturn.Rows[0]["YN_PURCHASE"].ToString();
                        this.gfg_tppurchase = e.HelpReturn.Rows[0]["FG_TPPURCHASE"].ToString();
                        string str = e.HelpReturn.Rows[0]["YN_AM"].ToString();
                        this._header.CurrentRow["YN_PURCHASE"] = e.HelpReturn.Rows[0]["YN_PURCHASE"].ToString();
                        this._header.CurrentRow["FG_TPPURCHASE"] = e.HelpReturn.Rows[0]["FG_TPPURCHASE"].ToString();
                        this._header.CurrentRow["YN_AM"] = e.HelpReturn.Rows[0]["YN_AM"].ToString();

                        //if (this.cbo거래구분.SelectedValue.ToString() == "001")
                        //    this.cbo과세구분.Enabled = true;
                        //else
                        //    this.cbo과세구분.Enabled = false;

                        if (str == "Y")
                        {
                            this.txt유무환구분.Text = "Y";
                            this._header.CurrentRow["YN_AM"] = "Y";

                            //if (this.txt유무환구분.Text == "Y")
                            //{
                            //    this.cbo과세구분.Enabled = true;
                            //    break;
                            //}
                            break;
                        }

                        this.txt유무환구분.Text = "N";
                        this._header.CurrentRow["YN_AM"] = "N";
                        break;
                    case HelpID.P_PU_TPPO_SUB:
                        this._header.CurrentRow["YN_RETURN"] = e.HelpReturn.Rows[0]["YN_RETURN"];
                        break;
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
                case HelpID.P_PU_EJTP_SUB:
                    e.HelpParam.P61_CODE1 = "030|";
                    if (this.반품발주사용여부 == "Y")
                        e.HelpParam.P41_CD_FIELD1 = "Y";
                    break;
            }
        }

        private void cbo과세구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.SettingTbTax(this.cbo과세구분.SelectedValue.ToString());
        }

        private void cbo거래구분_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.cbo거래구분.SelectedValue.ToString() == "001")
            {
                this._header.CurrentRow["FG_TAX"] = "21";
                this.cbo과세구분.SelectedValue = "21";
                //this.cbo과세구분.Enabled = true;
            }
            else
            {
                this._header.CurrentRow["FG_TAX"] = "23";
                this.cbo과세구분.SelectedValue = "23";

                //if (D.GetString(this.ctx반품형태.CodeValue) == "140")
                //    this.cbo과세구분.Enabled = false;
                //else
                //    this.cbo과세구분.Enabled = true;
            }

            this.cbo과세구분_SelectionChangeCommitted(null, null);
        }

        private void dtp의뢰일자_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this.dtp의뢰일자.Modified || this.dtp의뢰일자.IsValidated) return;

                this.ShowMessage(공통메세지.입력형식이올바르지않습니다);

                this.dtp의뢰일자.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo통화명_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo통화명.SelectedValue.ToString() == "000")
                {
                    this.cur환율.Text = "1.0";
                    this._header.CurrentRow["RT_EXCH"] = "1";
                    //this.cur환율.Enabled = false;
                }
                else
                {
                    Decimal num = 0;

                    if (this.dtp의뢰일자.Text != string.Empty && MA.기준환율.Option != MA.기준환율옵션.적용안함)
                        num = MA.기준환율적용(this.dtp의뢰일자.Text, D.GetString(this.cbo통화명.SelectedValue.ToString()));

                    this.cur환율.Text = num.ToString();
                    this._header.CurrentRow["RT_EXCH"] = num;

                    //if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    //    this.cur환율.Enabled = false;
                    //else
                    //    this.cur환율.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo통화명_Leave(object sender, EventArgs e)
        {
            try
            {
                this.SetGridMoneyChange(null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetGridMoneyChange(ValidateEditEventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                string @string = D.GetString(this._flex[e.Row, "FG_TAX"]);
                Decimal decimal1 = D.GetDecimal(this._flex[e.Row, "RATE_VAT"]);
                D.GetDecimal(this._flex[e.Row, "RATE_EXCHG"]);
                bool 부가세포함 = D.GetString(this._flex[e.Row, "TP_UM_TAX"]) == "001";
                Decimal decimal2 = D.GetDecimal(this._flex[e.Row, "QT_REQ_MM"]);
                Decimal decimal3 = D.GetDecimal(this._flex[e.Row, "UM_EX_PO"]);
                Decimal decimalValue = this.cur환율.DecimalValue;

                if (decimal2 == 0) return;

                Decimal 원화금액 = Unit.원화금액(DataDictionaryTypes.PU, Math.Round(decimal2 * decimal3 * decimalValue));
                Decimal 외화금액;
                Decimal 부가세;
                Calc.GetAmt(decimal2, decimal3, decimalValue, @string, decimal1, 모듈.PUR, 부가세포함, out 외화금액, out 원화금액, out 부가세);

                this._flex[e.Row, "VAT"] = 부가세;
                this._flex[e.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, 원화금액 / decimalValue);
                this._flex[e.Row, "AM"] = 원화금액;
                this._flex[e.Row, "AM_TOTAL"] = Calc.합계금액(원화금액, 부가세);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn납기일자적용_Click(object sender, EventArgs e)
        {
            string str1 = string.Empty;
            string str2 = string.Empty;
            string index1 = "DT_LIMIT";
            string text = this.dtp납기일자.Text;
            DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");

            if (dataRowArray.Length == 0 || dataRowArray == null)
            {
                this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
            }
            else
            {
                for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
                    dataRowArray[index2][index1] = text;
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;

                if (flexGrid == null) return;

                switch (flexGrid.Cols[flexGrid.Col].Name)
                {
                    case "CD_PJT":
                        H_SA_PRJ_SUB hSaPrjSub = new H_SA_PRJ_SUB();

                        if (hSaPrjSub.ShowDialog() == DialogResult.OK)
                        {
                            flexGrid["CD_PJT"] = hSaPrjSub.ReturnValue[0];
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
            try
            {
                if (this._flex.Cols[e.Col].Name.Equals("CD_ITEM") && this._flex[this._flex.Row, "NO_IO_MGMT"].ToString().Trim() != "")
                    e.Cancel = true;

                if (this._flex.Cols[e.Col].Name == "QT_REQ" && this.m_sEnv.Equals("N"))
                    e.Cancel = true;

                if (D.GetString(this._flex["TP_UM_TAX"]) == "001")
                {
                    if (!(this._flex.Cols[e.Col].Name == "AM_EX") && !(this._flex.Cols[e.Col].Name == "AM") && !(this._flex.Cols[e.Col].Name == "VAT"))
                        return;

                    e.Cancel = true;
                }
                else if (this._flex.Cols[e.Col].Name == "AM_TOTAL")
                    e.Cancel = true;
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
                if (!this._flex.HasNormalRow || this._flex["CD_ITEM"].ToString() == "" || ((FlexGrid)sender).HelpColName != "UM_EX")
                    return;

                P_PU_UM_HISTORY pPuUmHistory = new P_PU_UM_HISTORY(this.ctx매입처.CodeValue.ToString(), this.ctx매입처.CodeName, this._flex["CD_ITEM"].ToString(), this._flex["NM_ITEM"].ToString(), "", "", this.cbo통화명.SelectedValue.ToString(), this._header.CurrentRow["FG_TRANS"].ToString());

                if (pPuUmHistory.ShowDialog((IWin32Window)this) == DialogResult.OK && pPuUmHistory.UM != "")
                    this._flex["UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(pPuUmHistory.UM));
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
                int col = this._flex.Col;
                int row = this._flex.Row;
                string str = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                string editData = ((FlexGrid)sender).EditData;
                Decimal num2;
                Decimal 외화금액;

                if (this._flex.AllowEditing && this._flex.GetData(e.Row, e.Col).ToString() != this._flex.EditData)
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "QT_REQ":
                            if (this._flex.CDouble(this._flex[row, "QT_REQ"].ToString()) == 0.0)
                            {
                                int num3 = (int)this.ShowMessage("SU_M000012");
                                break;
                            }

                            double num4 = this._flex.CDouble(this._flex.EditData.Trim());
                            double num5 = this._flex.CDouble(this._flex[row, "RATE_EXCHG"].ToString());

                            if (num5 == 0.0)
                                num5 = 1.0;

                            this._flex[row, "QT_REQ_MM"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal((num4 / num5)));
                            this.SetGridMoneyChange(e);
                            break;
                        case "QT_REQ_MM":
                            if (this._flex.CDouble(this._flex[row, "QT_REQ_MM"].ToString()) == 0.0)
                            {
                                int num3 = (int)this.ShowMessage("SU_M000012");
                                break;
                            }

                            double num6 = this._flex.CDouble(this._flex.EditData.Trim());
                            double num7 = this._flex.CDouble(this._flex[row, "RATE_EXCHG"].ToString());

                            if (num7 == 0.0)
                                num7 = 1.0;

                            this._flex[row, "QT_REQ"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal((num6 * num7)));
                            this.SetGridMoneyChange(e);
                            break;
                        case "UM_EX":
                            double num8 = this._flex.CDouble(this._flex.EditData.Trim());
                            double num9 = this._flex.CDouble(this._flex[row, "RATE_EXCHG"].ToString());

                            if (num9 == 0.0)
                                num9 = 1.0;

                            this._flex[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal((num8 * num9)));
                            this._flex[row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(num8) * D.GetDecimal(this._flex["RT_EXCH"]));
                            this.SetGridMoneyChange(e);
                            break;
                        case "UM_EX_PO":
                            double num10 = this._flex.CDouble(this._flex.EditData.Trim());
                            double num11 = this._flex.CDouble(this._flex[row, "RATE_EXCHG"].ToString());

                            if (num11 == 0.0)
                                num11 = 1.0;

                            this._flex[row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal((num10 / num11)));
                            this._flex[row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "UM_EX"]) * D.GetDecimal(this._flex["RT_EXCH"]));
                            this.SetGridMoneyChange(e);
                            break;
                        case "AM_EX":
                            Decimal decimal1 = D.GetDecimal(this._flex[row, "QT_REQ_MM"]);
                            double num12 = this._flex.CDouble(this._flex.EditData.Trim());
                            double num13 = this._flex.CDouble(this.cur환율.DecimalValue.ToString());
                            double num14 = this._flex.CDouble(this._flex[row, "RATE_VAT"]) == 0.0 ? 0.0 : this._flex.CDouble(this._flex[row, "RATE_VAT"]) / 100.0;
                            double num15 = this._flex.CDouble(this._flex[e.Row, "QT_REQ"].ToString());

                            if (num15 == 0.0)
                                num15 = 1.0;

                            this._flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex.CDouble((num12 / num15))));
                            this._flex[e.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(D.GetDecimal(num12), decimal1));
                            this._flex[e.Row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(this._flex.CDouble((num12 / num15 * num13))));
                            this._flex[row, "AM"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal((num12 * num13)));
                            Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(num12) * D.GetDecimal(num13));
                            this._flex[row, "VAT"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(this._flex[row, "AM"]) * D.GetDecimal(num14));
                            this._flex[row, "AM_TOTAL"] = Calc.합계금액(D.GetDecimal(this._flex[row, "AM"]), D.GetDecimal(this._flex[row, "VAT"]));
                            break;
                        case "CD_PJT":
                            if (this._flex[e.Row, "CD_PJT"].ToString() == string.Empty)
                            {
                                this._flex["CD_PJT"] = string.Empty;
                                this._flex["NM_PROJECT"] = string.Empty;
                                break;
                            }

                            DataSet dataSet = this._biz.ProjectSearch(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     this._flex[e.Row, "CD_PJT"].ToString() });

                            if (dataSet != null && dataSet.Tables[0].Rows.Count == 0)
                            {
                                this._flex[e.Row, "CD_PJT"] = string.Empty;
                                this._flex[e.Row, "NM_PROJECT"] = string.Empty;
                                this._flex_HelpClick(sender, (EventArgs)e);
                            }
                            else if (dataSet != null && dataSet.Tables[0].Rows.Count == 1)
                            {
                                this._flex[e.Row, "CD_PJT"] = dataSet.Tables[0].Rows[0]["NO_PROJECT"].ToString();
                                this._flex[e.Row, "NM_PROJECT"] = dataSet.Tables[0].Rows[0]["NM_PROJECT"].ToString();
                            }
                            else if (dataSet != null && dataSet.Tables[0].Rows.Count > 1)
                            {
                                P_SA_PRJ_SUB pSaPrjSub = new P_SA_PRJ_SUB(new string[] { this.ctx매입처.CodeValue,
                                                                                         this.ctx매입처.CodeName,
                                                                                         this.ctx구매그룹.CodeValue,
                                                                                         this.ctx구매그룹.CodeName,
                                                                                         "NOT_END",
                                                                                         this._flex[e.Row, "CD_PJT"].ToString() });
                                if (pSaPrjSub.ShowDialog() == DialogResult.OK)
                                {
                                    this._flex[e.Row, "CD_PJT"] = pSaPrjSub.returnParams[0];
                                    this._flex[e.Row, "NM_PROJECT"] = pSaPrjSub.returnParams[4];
                                }
                            }
                            break;
                        case "FG_TAX":
                            if (editData == "")
                            {
                                this._flex.DataTable.Rows[e.Row]["RATE_VAT"] = 0;
                                this._flex.DataTable.Rows[e.Row]["VAT"] = 0;
                                break;
                            }

                            if (this.의제매입여부(editData))
                            {
                                this._flex[e.Row, "RATE_VAT"] = 0;
                                this._flex[e.Row, "VAT"] = 0;
                            }
                            else
                            {
                                DataTable tableSearch = ComFunc.GetTableSearch("MA_CODEDTL", new object[] { this.LoginInfo.CompanyCode,
                                                                                                            "MA_B000046",
                                                                                                            editData });

                                if (tableSearch.Rows.Count > 0 && tableSearch.Rows[0]["CD_FLAG1"].ToString() != string.Empty)
                                {
                                    Decimal num3 = Convert.ToDecimal(tableSearch.Rows[0]["CD_FLAG1"]);
                                    this._flex[e.Row, "RATE_VAT"] = num3;
                                }
                                else
                                    this._flex[e.Row, "VAT"] = 0;
                            }

                            string string1 = D.GetString(this._flex[e.Row, "FG_TAX"]);
                            Decimal decimal2 = D.GetDecimal(this._flex[e.Row, "RATE_VAT"]);
                            num2 = D.GetDecimal(this._flex[e.Row, "RATE_EXCHG"]) == 0 ? 1 : D.GetDecimal(this._flex[e.Row, "RATE_EXCHG"]);
                            bool 부가세포함1 = D.GetString(this._flex[e.Row, "TP_UM_TAX"]) == "001";
                            Decimal decimal3 = D.GetDecimal(this._flex[e.Row, "QT_REQ_MM"]);
                            Decimal decimal4 = D.GetDecimal(this._flex[e.Row, "UM_EX_PO"]);
                            Decimal decimalValue1 = this.cur환율.DecimalValue;

                            if (decimal3 == 0) break;

                            Decimal 원화금액1 = Unit.원화금액(DataDictionaryTypes.PU, Math.Round(decimal3 * decimal4 * decimalValue1));
                            Decimal 부가세1;
                            Calc.GetAmt(decimal3, decimal4, decimalValue1, string1, decimal2, 모듈.PUR, 부가세포함1, out 외화금액, out 원화금액1, out 부가세1);
                            this._flex[e.Row, "VAT"] = 부가세1;
                            this._flex[e.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, 원화금액1 / decimalValue1);
                            this._flex[e.Row, "AM"] = 원화금액1;
                            this._flex[e.Row, "AM_TOTAL"] = Calc.합계금액(원화금액1, 부가세1);
                            break;
                        case "DT_LIMIT":
                            if (!D.StringDate.IsValidDate(editData, false, this._flex.Cols["DT_LIMIT"].Caption))
                            {
                                this._flex["DT_LIMIT"] = str;
                                this._flex.Select(e.Row, e.Col);
                                break;
                            }
                            break;
                        case "TP_UM_TAX":
                            string string2 = D.GetString(this._flex[e.Row, "FG_TAX"]);
                            Decimal decimal5 = D.GetDecimal(this._flex[e.Row, "RATE_VAT"]);
                            num2 = D.GetDecimal(this._flex[e.Row, "RATE_EXCHG"]) == 0 ? 1 : D.GetDecimal(this._flex[e.Row, "RATE_EXCHG"]);
                            bool 부가세포함2 = D.GetString(this._flex[e.Row, "TP_UM_TAX"]) == "001";
                            Decimal decimal6 = D.GetDecimal(this._flex[e.Row, "QT_REQ_MM"]);
                            Decimal decimal7 = D.GetDecimal(this._flex[e.Row, "UM_EX_PO"]);
                            Decimal decimalValue2 = this.cur환율.DecimalValue;

                            if (decimal6 == 0) break;

                            Decimal 원화금액2 = Unit.원화금액(DataDictionaryTypes.PU, Math.Round(decimal6 * decimal7 * decimalValue2));
                            Decimal 부가세2;
                            Calc.GetAmt(decimal6, decimal7, decimalValue2, string2, decimal5, 모듈.PUR, 부가세포함2, out 외화금액, out 원화금액2, out 부가세2);
                            this._flex[e.Row, "VAT"] = 부가세2;
                            this._flex[e.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, 원화금액2 / decimalValue2);
                            this._flex[e.Row, "AM"] = 원화금액2;
                            this._flex[e.Row, "AM_TOTAL"] = Calc.합계금액(원화금액2, 부가세2);
                            break;
                        case "AM_TOTAL":
                            string string3 = D.GetString(this._flex[e.Row, "FG_TAX"]);
                            Decimal decimal8 = D.GetDecimal(this._flex[e.Row, "RATE_VAT"]);
                            Decimal val1 = D.GetDecimal(this._flex[e.Row, "RATE_EXCHG"]) == 0 ? 1 : D.GetDecimal(this._flex[e.Row, "RATE_EXCHG"]);
                            bool 부가세포함3 = D.GetString(this._flex[e.Row, "TP_UM_TAX"]) == "001";
                            Decimal decimal9 = D.GetDecimal(this._flex[e.Row, "QT_REQ_MM"]);
                            Decimal num16 = UDecimal.Getdivision(D.GetDecimal(this._flex[e.Row, "AM_TOTAL"]), D.GetDecimal(this._flex[e.Row, "QT_REQ_MM"]));
                            Decimal decimalValue3 = this.cur환율.DecimalValue;

                            if (decimal9 == 0) break;

                            Decimal 원화금액3 = Unit.원화금액(DataDictionaryTypes.PU, Math.Round(decimal9 * num16 * decimalValue3));
                            Decimal 부가세3;
                            Calc.GetAmt(decimal9, num16, decimalValue3, string3, decimal8, 모듈.PUR, 부가세포함3, out 외화금액, out 원화금액3, out 부가세3);
                            this._flex[e.Row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, num16);
                            this._flex[e.Row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, UDecimal.Getdivision(num16, val1));
                            this._flex[e.Row, "VAT"] = 부가세3;
                            this._flex[e.Row, "AM_EX"] = Unit.외화금액(DataDictionaryTypes.PU, 원화금액3 / decimalValue3);
                            this._flex[e.Row, "AM"] = 원화금액3;
                            this._flex[e.Row, "AM_TOTAL"] = Calc.합계금액(원화금액3, 부가세3);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 의제매입여부(string ps_taxp)
        {
            return ps_taxp == "27" || ps_taxp == "28" || (ps_taxp == "29" || ps_taxp == "30") || (ps_taxp == "32" || ps_taxp == "33" || (ps_taxp == "34" || ps_taxp == "35")) || (ps_taxp == "36" || ps_taxp == "40" || (ps_taxp == "41" || ps_taxp == "42") || ps_taxp == "48") || ps_taxp == "49";
        }

        private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                HelpReturn helpReturn = e.Result;
                DataTable dataTable = flexGrid.DataTable;
                bool flag = true;
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (e.Result.DialogResult == DialogResult.Cancel)
                            break;
                        Decimal maxValue = this._flex.GetMaxValue("NO_LINE");
                        this._flex.Redraw = false;

                        foreach (DataRow dataRow in helpReturn.Rows)
                        {
                            int row;

                            if (flag)
                            {
                                row = e.Row;
                                flag = false;
                            }
                            else
                            {
                                this._flex.Rows.Add();
                                this._flex.Row = this._flex.Rows.Count - 1;
                                row = this._flex.Row;
                                this._flex[row, "NO_LINE"] = maxValue;
                            }

                            this._flex[row, "CD_ITEM"] = dataRow["CD_ITEM"];
                            this._flex[row, "NM_ITEM"] = dataRow["NM_ITEM"];
                            this._flex[row, "STND_ITEM"] = dataRow["STND_ITEM"];
                            this._flex[row, "UNIT_IM"] = dataRow["UNIT_IM"];
                            this._flex[row, "NO_RCV"] = this.txt의뢰번호.Text;
                            this._flex[row, "QT_REQ"] = 0;
                            this._flex[row, "QT_REQ_MM"] = 0;
                            this._flex[row, "CD_UNIT_MM"] = dataRow["UNIT_PO"];
                            this._flex[row, "DT_LIMIT"] = this.dtp납기일자.Text;
                            this._flex[row, "QT_GR"] = 0;
                            this._flex[row, "UM_EX"] = 0;
                            this._flex[row, "AM_EX"] = 0;
                            this._flex[row, "UM"] = 0;
                            this._flex[row, "AM"] = 0;
                            this._flex[row, "VAT"] = 0;
                            this._flex[row, "NO_IO_MGMT"] = "";
                            this._flex[row, "NO_TO_LINE"] = 0;
                            this._flex[row, "NO_LCLINE"] = 0;
                            this._flex[row, "NO_POLINE_MGMT"] = 0;
                            this._flex[row, "NO_IOLINE_MGMT"] = 0;
                            this._flex[row, "CD_PURGRP"] = this._header.CurrentRow["CD_PURGRP"];
                            this._flex[row, "RATE_EXCHG"] = D.GetDecimal(dataRow["UNIT_PO_FACT"]) == 0 ? 1 : dataRow["UNIT_PO_FACT"];
                            this._flex[row, "NO_LOT"] = e.Result.Rows[0]["FG_SERNO"].ToString() == "002" ? "YES" : "NO";
                            this._flex[row, "NO_PO"] = "";
                            this._flex[row, "NO_POLINE"] = 0;
                            this._flex[row, "CD_SL"] = dataRow["CD_SL"];
                            this._flex[row, "NM_SL"] = dataRow["NM_SL"];
                            this._flex[row, "TP_UM_TAX"] = "002";
                            object[] m_obj = new object[] { dataRow["CD_ITEM"],
                                                            this.cbo공장.SelectedValue.ToString(),
                                                            Global.MainFrame.LoginInfo.CompanyCode,
                                                            this._header.CurrentRow["FG_UM"],
                                                            this._header.CurrentRow["CD_EXCH"],
                                                            this._header.CurrentRow["DT_REQ"],
                                                            this._header.CurrentRow["CD_PARTNER"],
                                                            this._header.CurrentRow["CD_PURGRP"],
                                                            "PU" };
                            ++maxValue;
                            this.품목단가정보구하기(m_obj, row);
                        }

                        this._flex.Select(e.Row, this._flex.Cols.Fixed);
                        this._flex.Redraw = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 품목단가정보구하기(object[] m_obj, int row)
        {
            DataSet dataSet = this._biz.ItemInfo_Search(m_obj);

            if (dataSet == null) return;

            if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
            {
                if (this.m_sEnv_FG_TAX == "000" || string.IsNullOrEmpty(dataSet.Tables[1].Rows[0]["FG_TAX_PU"].ToString()))
                {
                    this._flex[row, "FG_TAX"] = this._header.CurrentRow["FG_TAX"];
                    this._flex[row, "RATE_VAT"] = this._header.CurrentRow["VAT_RATE"];
                }
                else
                {
                    this._flex[row, "FG_TAX"] = dataSet.Tables[1].Rows[0]["FG_TAX_PU"];
                    this._flex[row, "RATE_VAT"] = dataSet.Tables[1].Rows[0]["RATE_VAT"];
                }

                if (D.GetDecimal(dataSet.Tables[1].Rows[0]["RATE_EXCHG"]) != 0)
                    this._flex[row, "RATE_EXCHG"] = dataSet.Tables[1].Rows[0]["RATE_EXCHG"];
            }

            if (this._header.CurrentRow["YN_AM"].Equals("Y") && dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
            {
                if (D.GetDecimal(dataSet.Tables[0].Rows[0]["UM_ITEM"]) > 0 && D.GetDecimal(this._flex[row, "RATE_EXCHG"]) > 0)
                {
                    this._flex[row, "UM_EX"] = Unit.외화단가(DataDictionaryTypes.PU, this._flex.CDecimal(dataSet.Tables[0].Rows[0]["UM_ITEM"].ToString()) / this._flex.CDecimal(this._flex[row, "RATE_EXCHG"].ToString()));
                    this._flex[row, "UM_EX_PO"] = Unit.외화단가(DataDictionaryTypes.PU, this._flex.CDecimal(dataSet.Tables[0].Rows[0]["UM_ITEM"].ToString()));
                }
                else
                {
                    this._flex[row, "UM_EX"] = 0;
                    this._flex[row, "UM_EX_PO"] = 0;
                }

                this._flex[row, "UM"] = Unit.원화단가(DataDictionaryTypes.PU, this._flex.CDecimal(this._flex[row, "UM_EX"].ToString()) * this._flex.CDecimal(this.cur환율.Text.Trim()));
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (this._flex["NO_IO_MGMT"].ToString().Trim() != "")
                            e.Cancel = true;
                        e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        break;
                    case "CD_SL":
                        e.Parameter.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드

        #endregion

        

     

        

        

        

        

        

        

        

        
    }
}

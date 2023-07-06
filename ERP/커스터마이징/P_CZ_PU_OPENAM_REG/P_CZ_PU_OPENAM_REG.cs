using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using DzHelpFormLib;
using pur;
using System.Data.OleDb;
using Dintec;

namespace cz
{
    public partial class P_CZ_PU_OPENAM_REG : PageBase
    {
        #region 초기화 & 전역변수
        private P_CZ_PU_OPENAM_REG_BIZ _biz;
        private FreeBinding _header경리재고;
        private FreeBinding _header자재재고;
        private string 공장코드;
        private string 기준년월;

        public P_CZ_PU_OPENAM_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flex경리재고, 
                                              this._flex자재재고, 
                                              this._flexLOT };

            this._header경리재고 = new FreeBinding();
            this._header자재재고 = new FreeBinding();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_PU_OPENAM_REG_BIZ();

            this.MA_EXC_SETTING();
            this.InitGrid();
            this.InitEvent();
        }

        private void MA_EXC_SETTING()
        {
            if (!Config.MA_ENV.LOT관리) return;

            if (ComFunc.전용코드("기초재고등록-LOT이월사용유무") == "000")
            {
                this.btn추가LOT.Enabled = false;
                this.btn삭제LOT.Enabled = false;
            }
        }

        private void InitGrid()
        {
            #region 경리재고
            this._flex경리재고.BeginSetting(1, 1, false);

            this._flex경리재고.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex경리재고.SetCol("CD_ITEM", "품목코드", 120, true);
            this._flex경리재고.SetCol("NM_ITEM", "품목명", 150);
            this._flex경리재고.SetCol("STND_ITEM", "규격", 80);
            this._flex경리재고.SetCol("UNIT_IM", "재고단위", 80);
            this._flex경리재고.SetCol("QT_BAS", "기초재고", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex경리재고.SetCol("UM_BAS", "기초재고단가", 120, true, typeof(decimal), FormatTpType.UNIT_COST);
            this._flex경리재고.SetCol("AM_BAS", "기초재고금액", 120, true, typeof(decimal), FormatTpType.MONEY);
            this._flex경리재고.SetCol("CLS_ITEM", "계정코드", false);
            this._flex경리재고.SetCol("NM_CLSITEM", "계정", 100);
            this._flex경리재고.SetCol("CD_PLANT", "공장코드", false);
            this._flex경리재고.SetCol("YM_STANDARD", "년월", false);

            this._flex경리재고.SetDummyColumn("S");

            this._flex경리재고.SettingVersion = "0.0.0.1";
            this._flex경리재고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex경리재고.SetExceptSumCol("UM_BAS");
            this._flex경리재고.EnabledHeaderCheck = true;
            this._flex경리재고.SetCodeHelpCol("CD_ITEM", "H_CZ_MA_CUSTOMIZE_SUB", ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                                      "NM_ITEM",
                                                                                                                      "UNIT_IM",
                                                                                                                      "CLS_ITEM",
                                                                                                                      "NM_CLSITEM" }, new string[] { "CD_ITEM",
                                                                                                                                                     "NM_ITEM",
                                                                                                                                                     "UNIT_IM",
                                                                                                                                                     "CLS_ITEM",
                                                                                                                                                     "NM_CLS_ITEM" });
            this._flex경리재고.VerifyPrimaryKey = new string[] { "CD_ITEM" };
            this._flex경리재고.VerifyNotNull = new string[] { "CD_ITEM" };
            this._flex경리재고.VerifyDuplicate = new string[] { "CD_ITEM" };
            this._flex경리재고.VerifyAutoDelete = new string[] { "CD_ITEM" };
            #endregion

            #region 자재재고
            this._flex자재재고.BeginSetting(1, 1, false);

            this._flex자재재고.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);

            if (Config.MA_ENV.LOT관리)
                this._flex자재재고.SetCol("NO_LOT", "LOT", 50, false, typeof(string));
            
            this._flex자재재고.SetCol("CD_ITEM", "품목코드", 120, true);
            this._flex자재재고.SetCol("NM_ITEM", "품목명", 150);
            this._flex자재재고.SetCol("STND_ITEM", "규격", 80);
            this._flex자재재고.SetCol("UNIT_IM", "재고단위", 80);
            this._flex자재재고.SetCol("QT_GOOD_INV", "양품재고", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex자재재고.SetCol("CLS_ITEM", "계정코드", false);
            this._flex자재재고.SetCol("NM_CLSITEM", "계정", 100);
            this._flex자재재고.SetCol("CD_PLANT", "공장코드", false);
            this._flex자재재고.SetCol("YM_STANDARD", "년월", false);
            this._flex자재재고.SetCol("CD_SL", "창고코드", false);
            this._flex자재재고.SetCol("NM_SL", "창고명", false);

            this._flex자재재고.SetDummyColumn("S");

            this._flex자재재고.SettingVersion = "0.0.0.1";
            this._flex자재재고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flex자재재고.SetCodeHelpCol("CD_ITEM", "H_CZ_MA_CUSTOMIZE_SUB", ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                                      "NM_ITEM",
                                                                                                                      "UNIT_IM",
                                                                                                                      "CLS_ITEM",
                                                                                                                      "NM_CLSITEM",
                                                                                                                      "FG_SERNO" }, new string[] { "CD_ITEM",
                                                                                                                                                   "NM_ITEM",
                                                                                                                                                   "UNIT_IM",
                                                                                                                                                   "CLS_ITEM",
                                                                                                                                                   "NM_CLS_ITEM",
                                                                                                                                                   "FG_SERNO" });
            this._flex자재재고.VerifyPrimaryKey = new string[] { "CD_ITEM" };
            this._flex자재재고.VerifyNotNull = new string[] { "CD_ITEM" };
            this._flex자재재고.VerifyDuplicate = new string[] { "CD_ITEM" };
            this._flex자재재고.VerifyAutoDelete = new string[] { "CD_ITEM" };
            #endregion

            #region LOT
            this._flexLOT.BeginSetting(1, 1, false);

            this._flexLOT.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexLOT.SetCol("NO_LOT", "LOT번호", 120, true);
            this._flexLOT.SetCol("QT_GOOD_INV", "양품재고", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexLOT.SetCol("DC_RMK", "비고", 120, true);

            foreach (DataRow dataRow in MA.GetCode("PU_C000079").Rows)
                this._flexLOT.SetCol("CD_MNG" + (object)D.GetInt(dataRow["CODE"]), D.GetString(dataRow["NAME"]), 100, true);

            this._flexLOT.SetDummyColumn("S");

            this._flexLOT.SettingVersion = "0.0.0.1";
            this._flexLOT.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexLOT.VerifyPrimaryKey = new string[] { "CD_ITEM", "NO_LOT" };
            this._flexLOT.VerifyNotNull = new string[] { "CD_ITEM", "NO_LOT" };
            this._flexLOT.VerifyDuplicate = new string[] { "CD_ITEM", "NO_LOT" };
            this._flexLOT.VerifyAutoDelete = new string[] { "CD_ITEM", "NO_LOT" };
            #endregion
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            #region 경리재고
            this._header경리재고.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
            this._header경리재고.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);

            this.ctx품목경리재고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx담당자경리재고.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            
            this.txt비고경리재고.KeyDown += new KeyEventHandler(this.Text_KeyDown);

            this.dtp입력일자경리재고.CalendarClosed += new EventHandler(this.Control_CalendarClosed);
            this.dtp입력일자경리재고.Validated += new EventHandler(this.Control_Validated);

            this.cbo공장경리재고.KeyDown += new KeyEventHandler(this.Combo_KeyDown);
            this.cbo계정구분경리재고.SelectedIndexChanged += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo계정구분경리재고.KeyDown += new KeyEventHandler(this.Combo_KeyDown);

            this.btn엑셀양식다운로드경리재고.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
            this.btn엑셀업로드경리재고.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn품목전개경리재고.Click += new EventHandler(this.btn품목전개경리재고_Click);
            this.btn기초자재적용경리재고.Click += new EventHandler(this.btn기초자재적용경리재고_Click);
            this.btn추가경리재고.Click += new EventHandler(this.btn추가경리재고_Click);
            this.btn삭제경리재고.Click += new EventHandler(this.btn삭제경리재고_Click);

            this._flex경리재고.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex경리재고.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
            this._flex경리재고.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex경리재고.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            #endregion

            #region 자재재고
            this._header자재재고.JobModeChanged += new FreeBindingEventHandler(this._header_JobModeChanged);
            this._header자재재고.ControlValueChanged += new FreeBindingEventHandler(this._header_ControlValueChanged);

            this.cbo공장자재재고.KeyDown += new KeyEventHandler(this.Combo_KeyDown);
            this.cbo계정구분자재재고.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
            this.cbo계정구분자재재고.KeyDown += new KeyEventHandler(this.Combo_KeyDown);

            this.ctx창고자재재고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx품목자재재고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx담당자자재재고.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            
            this.dtp입력일자자재재고.CalendarClosed += new EventHandler(this.Control_CalendarClosed);
            this.dtp입력일자자재재고.Validated += new EventHandler(this.Control_Validated);

            this.txt비고자재재고.KeyDown += new KeyEventHandler(this.Text_KeyDown);

            this.btn엑셀양식다운로드자재재고.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
            this.btn엑셀업로드자재재고.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn품목전개자재재고.Click += new EventHandler(this.btn품목전개자재재고_Click);
            this.btn자재재고이월.Click += new EventHandler(this.btn자재재고이월_Click);
            this.btn추가자재재고.Click += new EventHandler(this.btn추가자재재고_Click);
            this.btn삭제자재재고.Click += new EventHandler(this.btn삭제자재재고_Click);

            this._flex자재재고.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex자재재고.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
            this._flex자재재고.AfterRowChange += new RangeEventHandler(this._flex자재재고_AfterRowChange);
            this._flex자재재고.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            #endregion

            #region LOT
            this.btnLOT재고이월.Click += new EventHandler(this.btnLOT재고이월_Click);
            this.btn추가LOT.Click += new EventHandler(this.btn추가LOT_Click);
            this.btn삭제LOT.Click += new EventHandler(this.btn삭제LOT_Click);

            this._flexLOT.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flexLOT.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp입력일자경리재고.Text = Global.MainFrame.GetStringToday;
            this.dtp입력일자자재재고.Text = Global.MainFrame.GetStringToday;

            DataSet comboData = this.GetComboData(new string[] { "S;MA_B000010",
                                                                 "SC;MA_PLANT" });

            this.cbo계정구분경리재고.DataSource = comboData.Tables[0].Copy();
            this.cbo계정구분경리재고.DisplayMember = "NAME";
            this.cbo계정구분경리재고.ValueMember = "CODE";

            this.cbo계정구분자재재고.DataSource = comboData.Tables[0].Copy();
            this.cbo계정구분자재재고.DisplayMember = "NAME";
            this.cbo계정구분자재재고.ValueMember = "CODE";

            this.cbo공장경리재고.DataSource = comboData.Tables[1].Copy();
            this.cbo공장경리재고.DisplayMember = "NAME";
            this.cbo공장경리재고.ValueMember = "CODE";

            this.cbo공장자재재고.DataSource = comboData.Tables[1].Copy();
            this.cbo공장자재재고.DisplayMember = "NAME";
            this.cbo공장자재재고.ValueMember = "CODE";

            DataSet dataSet1 = this._biz.Search경리재고(new object[] { Global.MainFrame.LoginInfo.CompanyCode, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
            
            this._header경리재고.SetBinding(dataSet1.Tables[0], this.oneGrid1);
            this._header경리재고.ClearAndNewRow();
            this._header경리재고.AcceptChanges();

            this._flex경리재고.Binding = dataSet1.Tables[1];
            this._flex경리재고.AcceptChanges();

            DataSet dataSet2 = this._biz.Search자재재고(new object[] { Global.MainFrame.LoginInfo.CompanyCode, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
            
            this._header자재재고.SetBinding(dataSet2.Tables[0], this.oneGrid2);
            this._header자재재고.ClearAndNewRow();
            this._header자재재고.AcceptChanges();
            
            this._flex자재재고.Binding = dataSet2.Tables[1];
            this._flex자재재고.AcceptChanges();

            if (Config.MA_ENV.LOT관리)
            {
                this._flexLOT.Binding = this._biz.SearchLOT(new object[] { Global.MainFrame.LoginInfo.CompanyCode, string.Empty, string.Empty, string.Empty, string.Empty });
                this._flexLOT.AcceptChanges();
            }

            this.SetToolBarButtonState(true, true, false, false, false);
            
            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid2.UseCustomLayout = true;
            this.setNecessaryCondition(new object[] { this.bpPanelControl5.Name }, this.oneGrid1);
            this.setNecessaryCondition(new object[] { this.bpPanelControl12.Name }, this.oneGrid2);
            this.oneGrid1.InitCustomLayout();
            this.oneGrid2.UseCustomLayout = true;
        }

        private void setNecessaryCondition(object[] obj, OneGrid _OneGrid)
        {
            try
            {
                bool flag = true;
                List<Control> controlList = _OneGrid.GetControlList();
                for (int index1 = 0; index1 < controlList.Count; ++index1)
                {
                    BpPanelControl bpPanelControl = (BpPanelControl)controlList[index1];
                    for (int index2 = 0; index2 < obj.Length; ++index2)
                    {
                        if ((bpPanelControl).Name != D.GetString(obj[index2]))
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
        #endregion
        
        #region 메인버튼 이벤트
        private bool Check()
        {
            if (this.tabControl1.SelectedIndex != 0 ||
                this.dtp기준년도경리재고.Text != "" &&
                this.dtp기준년도경리재고.Text != string.Empty)
                return true;

            base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl기준년도경리재고.Text });

            this.dtp기준년도경리재고.Focus();

            return false;
        }

        private bool 재고평가체크()
        {
            try
            {
                if (this.tabControl1.SelectedIndex == 0)
                {
                    this.공장코드 = D.GetString(this.cbo공장경리재고.SelectedValue);
                    this.기준년월 = (this.dtp기준년도경리재고.Text + "00");
                }
                else
                {
                    this.공장코드 = D.GetString(this.cbo공장자재재고.SelectedValue);
                    this.기준년월 = (this.dtp기준년도자재재고.Text + "00");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }

            return true;
        }

        private bool ChkLOT수량()
        {
            string str = string.Empty;
            Decimal num1 = 0;
            DataRow[] dataRowArray1 = this._flex자재재고.DataTable.Select("NO_LOT = 'YES'");
            DataTable dataTable = this._flex자재재고.DataTable.Clone();

            if (dataRowArray1.Length > 0)
            {
                foreach (DataRow row in dataRowArray1)
                    dataTable.ImportRow(row);
            }

            for (int index1 = 0; index1 < dataTable.Rows.Count; ++index1)
            {
                str = string.Empty;
                Decimal @decimal = D.GetDecimal(dataTable.Rows[index1]["QT_GOOD_INV"]);
                DataRow[] dataRowArray2 = this._flexLOT.DataTable.Select("CD_ITEM      = '" + dataTable.Rows[index1]["CD_ITEM"] + "' AND YM_STANDARD  = '" + dataTable.Rows[index1]["YM_STANDARD"] + "' AND CD_SL        = '" + dataTable.Rows[index1]["CD_SL"] + "' AND CD_PLANT     = '" + dataTable.Rows[index1]["CD_PLANT"] + "' ");

                if (dataRowArray2 != null && dataRowArray2.Length > 0)
                {
                    for (int index2 = 0; index2 < dataRowArray2.Length; ++index2)
                        num1 += D.GetDecimal(dataRowArray2[index2]["QT_GOOD_INV"]);
                    if (@decimal != num1)
                    {
                        base.ShowMessage("재고수량와 LOT재고수량이 동일하지 않습니다.\n품목코드:" + dataTable.Rows[index1]["CD_ITEM"] + "    재고수량:" + @decimal + "    LOT재고수량:" + num1);
                        return false;
                    }
                }

                num1 = 0;
            }

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.Check() || !this.BeforeSearch())
                    return;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    P_PU_OPENAM_SUB pPuOpenamSub = new P_PU_OPENAM_SUB(this.cbo공장경리재고.SelectedValue == null ? "" : this.cbo공장경리재고.SelectedValue.ToString());
                    if (pPuOpenamSub.ShowDialog() == DialogResult.OK)
                    {
                        DataSet dataSet = this._biz.Search경리재고(new object [] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                   pPuOpenamSub.CD_PLANT, 
                                                                                   (pPuOpenamSub.YY_AIS + "00"), 
                                                                                   pPuOpenamSub.NO_EMP,
                                                                                   D.GetString(this.cbo계정구분경리재고.SelectedValue),
                                                                                   D.GetString(this.ctx품목경리재고.CodeValue) });

                        this._header경리재고.SetDataTable(dataSet.Tables[0]);
                        this._header경리재고.AcceptChanges();

                        this._flex경리재고.Binding = dataSet.Tables[1];
                        this._flex경리재고.AcceptChanges();
                    }
                }
                else
                {
                    P_PU_OPENQT_SUB pPuOpenqtSub = new P_PU_OPENQT_SUB();
                    if (pPuOpenqtSub.ShowDialog() == DialogResult.OK)
                    {
                        string CD_PLANT = pPuOpenqtSub.m_SelecedRow["CD_PLANT"].ToString();
                        string str = pPuOpenqtSub.m_SelecedRow["YY_AIS"].ToString();
                        string CD_SL = pPuOpenqtSub.m_SelecedRow["CD_SL"].ToString();
                        string CD_DEPT = pPuOpenqtSub.m_SelecedRow["CD_DEPT"].ToString();
                        string NO_EMP = pPuOpenqtSub.m_SelecedRow["NO_EMP"].ToString();

                        DataSet dataSet = this._biz.Search자재재고(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                  CD_PLANT, 
                                                                                  (str + "00"), 
                                                                                  CD_SL, 
                                                                                  CD_DEPT, 
                                                                                  NO_EMP,
                                                                                  D.GetString(this.cbo계정구분자재재고.SelectedValue),
                                                                                  D.GetString(this.ctx품목자재재고.CodeValue) });
                        this._header자재재고.SetDataTable(dataSet.Tables[0]);
                        this._header경리재고.AcceptChanges();

                        this._flex자재재고.Binding = dataSet.Tables[1];
                        this._flex자재재고.AcceptChanges();

                        if (Config.MA_ENV.LOT관리)
                        {
                            this._flexLOT.Binding = this._biz.SearchLOT(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                                       CD_PLANT,
                                                                                       (str + "00"),
                                                                                       CD_SL,
                                                                                       string.Empty });
                            this._flexLOT.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeAdd()
        {
            return base.BeforeAdd() && this.MsgAndSave(PageActionMode.Search) && this.재고평가체크();
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!base.BeforeAdd()) return;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    this._flex경리재고.DataTable.Rows.Clear();
                    this._flex경리재고.AcceptChanges();

                    this._header경리재고.ClearAndNewRow();

                    this.cbo공장경리재고.Focus();
                }
                else
                {
                    this._flex자재재고.DataTable.Rows.Clear();
                    this._flex자재재고.AcceptChanges();

                    if (Config.MA_ENV.LOT관리)
                    {
                        this._flexLOT.DataTable.Rows.Clear();
                        this._flexLOT.AcceptChanges();
                    }

                    this._header자재재고.ClearAndNewRow();

                    this.cbo공장자재재고.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            return base.BeforeDelete() && this.재고평가체크() && base.ShowMessage("등록된 기초정보가 모두 삭제됩니다. 자료를 삭제하시겠습니까?", "QY2") == DialogResult.Yes;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!base.BeforeDelete())
                    return;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    if (!this._flex경리재고.HasNormalRow) return;

                    this._biz.Delete경리재고(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                            this.cbo공장경리재고.SelectedValue.ToString(),
                                                            (this.dtp기준년도경리재고.Text + "00") });

                    this._flex경리재고.DataTable.Rows.Clear();
                    this._flex경리재고.AcceptChanges();

                    this._header경리재고.ClearAndNewRow();
                    this._header경리재고.AcceptChanges();

                    this.cbo공장경리재고.Focus();
                }
                else
                {
                    if (!this._flex자재재고.HasNormalRow) return;

                    this._biz.Delete자재재고(new object[] { (this.dtp기준년도자재재고.Text + "00"),
                                                            this.ctx창고자재재고.CodeValue,
                                                            D.GetString(this.cbo공장자재재고.SelectedValue), 
                                                            Global.MainFrame.LoginInfo.CompanyCode });

                    this._flex자재재고.DataTable.Rows.Clear();
                    this._flex자재재고.AcceptChanges();

                    if (Config.MA_ENV.LOT관리)
                    {
                        this._flexLOT.DataTable.Rows.Clear();
                        this._flexLOT.AcceptChanges();
                    }

                    this._header자재재고.ClearAndNewRow();
                    this._header자재재고.AcceptChanges();

                    this.cbo공장자재재고.Focus();
                }

                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
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

                if (!this.MsgAndSave(PageActionMode.Save))
                    return;

                base.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify() || !this.BeforeSave()) return false;

            if (this.tabControl1.SelectedIndex == 0)
            {
                if (!this._flex경리재고.HasNormalRow) return false;
                if (this._header경리재고.GetChanges() == null && !this._flex경리재고.IsDataChanged) return false;

                this._header경리재고.CurrentRow["YM_STANDARD"] = (this.dtp기준년도경리재고.Text + "00");
                this._header경리재고.CurrentRow["YM_FSTANDARD"] = (this.dtp기준년도경리재고.Text + "00");

                if (!this._biz.Save경리재고(this._header경리재고.GetChanges(), this._flex경리재고.GetChanges()))
                    return false;

                this._header경리재고.AcceptChanges();
                this._flex경리재고.AcceptChanges();
            }
            else
            {
                if (!this.재고평가체크() || (Config.MA_ENV.LOT관리 && !this.ChkLOT수량())) return false;

                if (this._header자재재고.GetChanges() == null && !this._flex자재재고.IsDataChanged && !this._flexLOT.IsDataChanged) return false;
                if (!this._flex자재재고.HasNormalRow || (Config.MA_ENV.LOT관리 && !this._flexLOT.HasNormalRow)) return false;

                this._header자재재고.CurrentRow["YM_STANDARD"] = (this.dtp기준년도자재재고.Text + "00");

                if (!this._biz.Save자재재고(this._header자재재고.GetChanges(), this._flex자재재고.GetChanges(), this._flexLOT.GetChanges()))
                    return false;

                this._header자재재고.AcceptChanges();
                this._flex자재재고.AcceptChanges();
                this._flexLOT.AcceptChanges();
            }

            return true;
        }
        #endregion

        #region 공통 이벤트
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (base.IsChanged())
                    this.ToolBarSaveButtonEnabled = true;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    if (this._header경리재고.JobMode == JobModeEnum.조회후수정 && this._flex경리재고.HasNormalRow)
                        this.ToolBarDeleteButtonEnabled = true;
                }
                else
                {
                    if (this._header자재재고.JobMode == JobModeEnum.조회후수정 && this._flex경리재고.HasNormalRow)
                        this.ToolBarDeleteButtonEnabled = true;
                }
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
                #region 경리재고
                if (this._header경리재고.JobMode == JobModeEnum.추가후수정)
                {
                    if (!this._flex경리재고.HasNormalRow)
                    {
                        this.cbo공장경리재고.Enabled = true;
                        this.dtp기준년도경리재고.Enabled = true;
                        this.dtp입력일자경리재고.Enabled = true;
                        this.ctx담당자경리재고.Enabled = true;
                        this.btn기초자재적용경리재고.Enabled = true;
                        this.btn품목전개경리재고.Enabled = true;
                    }
                    else
                    {
                        this.cbo공장경리재고.Enabled = false;
                        this.dtp기준년도경리재고.Enabled = false;
                        this.dtp입력일자경리재고.Enabled = false;
                        this.ctx담당자경리재고.Enabled = false;
                        this.btn기초자재적용경리재고.Enabled = false;
                        this.btn품목전개경리재고.Enabled = false;
                    }
                }
                else
                {
                    this.cbo공장경리재고.Enabled = false;
                    this.dtp기준년도경리재고.Enabled = false;
                    this.dtp입력일자경리재고.Enabled = false;
                    this.ctx담당자경리재고.Enabled = false;
                    this.btn기초자재적용경리재고.Enabled = false;
                    this.btn품목전개경리재고.Enabled = false;
                }
                #endregion

                #region 자재재고
                if (this._header자재재고.JobMode == JobModeEnum.추가후수정)
                {
                    if (!this._flex자재재고.HasNormalRow)
                    {
                        this.cbo공장자재재고.Enabled = true;
                        this.dtp기준년도자재재고.Enabled = true;
                        this.dtp입력일자자재재고.Enabled = true;
                        this.ctx창고자재재고.Enabled = true;
                        this.ctx담당자자재재고.Enabled = true;
                        this.btn품목전개자재재고.Enabled = true;
                    }
                    else
                    {
                        this.cbo공장자재재고.Enabled = false;
                        this.dtp기준년도자재재고.Enabled = false;
                        this.dtp입력일자자재재고.Enabled = false;
                        this.ctx창고자재재고.Enabled = false;
                        this.ctx담당자자재재고.Enabled = false;
                        this.btn품목전개자재재고.Enabled = false;
                    }
                }
                else
                {
                    this.cbo공장자재재고.Enabled = false;
                    this.dtp기준년도자재재고.Enabled = false;
                    this.dtp입력일자자재재고.Enabled = false;
                    this.ctx창고자재재고.Enabled = false;
                    this.ctx담당자자재재고.Enabled = false;
                    this.btn품목전개자재재고.Enabled = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (name == this.dtp기준년도경리재고.Name)
                {
                    this._header경리재고.CurrentRow["YM_STANDARD"] = (this.dtp기준년도경리재고.Text + "00");
                    this._header경리재고.CurrentRow["YM_FSTANDARD"] = (this.dtp기준년도경리재고.Text + "00");
                }
                else if (name == this.dtp기준년도자재재고.Name)
                {
                    this._header자재재고.CurrentRow["YM_STANDARD"] = (this.dtp기준년도자재재고.Text + "00");
                }

                this.Page_DataChanged(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            string name;

            try
            {
                name = ((Control)sender).Name;

                if (string.IsNullOrEmpty(D.GetString(this.cbo공장자재재고.SelectedValue)))
                {
                    base.ShowMessage("PU_M000070");
                    this.cbo공장자재재고.Focus();
                    e.QueryCancel = true;
                    return;
                }
                else
                {
                    e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장자재재고.SelectedValue);
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
                DataRow[] rows = e.HelpReturn.Rows;
                name = ((Control)sender).Name;

                if (e.HelpID != HelpID.P_MA_EMP_SUB) return;

                if (name == this.ctx담당자경리재고.Name)
                {
                    this._header경리재고.CurrentRow["CD_DEPT"] = rows[0]["CD_DEPT"].ToString();
                }
                else
                {
                    if (name != this.ctx담당자자재재고.Name) return;

                    this._header자재재고.CurrentRow["CD_DEPT"] = rows[0]["CD_DEPT"].ToString();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Combo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Return)
                return;

            SendKeys.SendWait("{TAB}");
        }

        private void Text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.ToString() == "Enter" || e.KeyData.ToString() == "Down")
            {
                SendKeys.SendWait("{TAB}");
            }
            else
            {
                if (e.KeyData.ToString() != "Up")
                    return;

                SendKeys.SendWait("+{TAB}");
            }
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            DatePicker datePicker;

            try
            {
                datePicker = (DatePicker)sender;

                if (!datePicker.Modified || datePicker.IsValidated)
                    return;

                base.ShowMessage(공통메세지.입력형식이올바르지않습니다);

                datePicker.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_CalendarClosed(object sender, EventArgs e)
        {
            try
            {
                this.Control_Validated(sender, e);
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

                if (name == this.cbo계정구분경리재고.Name)
                {
                    if (this._flex경리재고.DataSource == null || this._flex경리재고.DataTable.Rows.Count <= 0)
                        return;

                    string str1 = string.Empty;

                    if (this.cbo계정구분경리재고.SelectedValue != DBNull.Value &&
                        this.cbo계정구분경리재고.SelectedValue.ToString() != "" &&
                        this.cbo계정구분경리재고.SelectedValue.ToString() != string.Empty)
                    {
                        str1 = "CLS_ITEM = '" + this.cbo계정구분경리재고.SelectedValue.ToString() + "'";
                    }

                    this._flex경리재고.DataView.RowFilter = str1;
                }
                else if (name == this.cbo계정구분자재재고.Name)
                {
                    if (this._flex자재재고.DataSource == null || this._flex자재재고.DataTable.Rows.Count <= 0)
                        return;

                    string str2 = string.Empty;

                    if (this.cbo계정구분자재재고.SelectedValue != DBNull.Value &&
                        this.cbo계정구분자재재고.SelectedValue.ToString() != "" &&
                        this.cbo계정구분자재재고.SelectedValue.ToString() != string.Empty)
                        str2 = "CLS_ITEM = '" + this.cbo계정구분자재재고.SelectedValue.ToString() + "'";

                    this._flex자재재고.DataView.RowFilter = str2;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
            string name, localPath, serverPath;
            FlexGrid grid;

            try
            {
                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                name = ((Control)sender).Name;

                if (name == this.btn엑셀양식다운로드경리재고.Name)
                {
                    localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_기초재고등록_경리재고_" + Global.MainFrame.GetStringToday + ".xls";
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_PU_OPENAM.xls";
                    grid = this._flex경리재고;
                }
                else
                {
                    localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_기초재고등록_자재재고_" + Global.MainFrame.GetStringToday + ".xls";
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_PU_OPENQT.xls";
                    grid = this._flex자재재고;
                }

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                if (grid.HasNormalRow)
                {
                    if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                        bState = false;
                }

                ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.\r\n'Microsoft Office 2007'인 경우 반드시 통합문서(97~2003)로 저장하세요.");

                if (bState == false) return;

                // 확장명 XLS (Excel 97~2003 용)
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + localPath + @";Extended Properties=Excel 8.0;";

                conn = new OleDbConnection(strConn);
                conn.Open();

                OleDbCommand Cmd = null;
                OleDbDataAdapter OleDBAdap = null;

                string sTableName = string.Empty;

                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                DataSet ds = new DataSet();

                // 엑셀의 1번째 시트만 데이터 만들어 주면 되므로 한 루프후 끝내면 됩니다.
                foreach (DataRow dr in dtSchema.Rows)
                {
                    OleDBAdap = new OleDbDataAdapter(dr["TABLE_NAME"].ToString(), conn);

                    OleDBAdap.SelectCommand.CommandType = System.Data.CommandType.TableDirect;
                    OleDBAdap.AcceptChangesDuringFill = false;

                    sTableName = dr["TABLE_NAME"].ToString().Replace("$", String.Empty).Replace("'", String.Empty);

                    if (dr["TABLE_NAME"].ToString().Contains("$"))
                        OleDBAdap.Fill(ds, sTableName);
                    break;
                }

                StringBuilder FldsInfo = new StringBuilder();
                StringBuilder Flds = new StringBuilder();

                // Create Field(s) String : 현재 테이블의 Field 명 생성
                foreach (DataColumn Column in ds.Tables[0].Columns)
                {
                    if (FldsInfo.Length > 0)
                    {
                        FldsInfo.Append(",");
                        Flds.Append(",");
                    }

                    FldsInfo.Append("[" + Column.ColumnName.Replace("'", "''") + "] NVARCHAR(4000)");
                    Flds.Append(Column.ColumnName.Replace("'", "''"));
                }

                // Insert Data
                foreach (DataRow dr in grid.DataTable.Rows)
                {
                    StringBuilder Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (!grid.DataTable.Columns.Contains(Column.ColumnName)) continue;

                        if (Values.Length > 0) Values.Append(",");
                        Values.Append("'" + dr[Column.ColumnName].ToString().Replace("'", "''") + "'");
                    }

                    Cmd = new OleDbCommand(
                        "INSERT INTO [" + sTableName + "$]" +
                        "(" + Flds.ToString() + ") " +
                        "VALUES (" + Values.ToString() + ")",
                        conn);
                    Cmd.ExecuteNonQuery();
                }

                bState = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.FieldCheckHead()) return;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    if (this._header경리재고.JobMode == JobModeEnum.추가후수정 &&
                        this._biz.경리재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                              this.cbo공장경리재고.SelectedValue.ToString(),
                                                              this.dtp기준년도경리재고.Text + "00" }) != "0")
                    {
                        base.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }
                else
                {
                    if (this._header자재재고.JobMode == JobModeEnum.추가후수정 &&
                         this._biz.자재재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               D.GetString(this.cbo공장자재재고.SelectedValue),
                                                               (this.dtp기준년도자재재고.Text + "00"),
                                                               this.ctx창고자재재고.CodeValue }) != "0")
                    {
                        base.ShowMessage(공통메세지.이미등록된자료가있습니다);
                        return;
                    }
                }

                this.엑셀업로드();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 엑셀업로드()
        {
            try
            {
                string NO_KEY = string.Empty;
                bool flag1 = false;
                string str1 = string.Empty;
                string str2 = string.Empty;
                string str3 = string.Empty;
                string str4 = string.Empty;
                string str5 = string.Empty;
                string str6 = string.Empty;
                string str7 = string.Empty;
                string str8 = string.Empty;
                string str9 = string.Empty;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                FlexGrid flexGrid;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    flexGrid = this._flex경리재고;
                    this.공장코드 = D.GetString(this.cbo공장경리재고.SelectedValue);
                    this.기준년월 = (this.dtp기준년도경리재고.Text + "00");
                }
                else
                {
                    flexGrid = this._flex자재재고;
                    this.공장코드 = D.GetString(this.cbo공장자재재고.SelectedValue);
                    this.기준년월 = (this.dtp기준년도자재재고.Text + "00");

                    if (Config.MA_ENV.LOT관리)
                    {
                        this.엑셀업로드_LOT();
                        return;
                    }
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Duzon.Common.Util.Excel excel = new Duzon.Common.Util.Excel();
                    DataTable dataTable1 = excel.StartLoadExcel(openFileDialog.FileName, 0, 3);

                    foreach (DataRow dataRow in dataTable1.Rows)
                        NO_KEY += D.GetString(dataRow["CD_ITEM"]) + "|";

                    DataTable dataTable2 = this._biz.엑셀업로드아이템조회(this.공장코드, NO_KEY);
                    StringBuilder stringBuilder = new StringBuilder();
                    string str10 = this.DD("품목코드");
                    stringBuilder.AppendLine(str10);
                    string str11 = "-".PadRight(80, '-');
                    stringBuilder.AppendLine(str11);
                    DataTable dataTable3 = flexGrid.DataTable;

                    if (!dataTable1.Columns.Contains("CD_ITEM"))
                        return;

                    foreach (DataRow dataRow in dataTable1.Rows)
                    {   
                        if (dataRow["CD_ITEM"].ToString().Trim() != null && dataRow["CD_ITEM"].ToString().Trim() != string.Empty && dataRow["CD_ITEM"].ToString().Trim() != "")
                        {
                            DataRow[] dataRowArray1 = dataTable2.Select("CD_ITEM = '" + D.GetString(dataRow["CD_ITEM"]) + "'");
                            if (dataRowArray1 != null && dataRowArray1.Length > 0)
                            {
                                DataRow[] dataRowArray2 = dataTable3.Select("CD_ITEM = '" + D.GetString(dataRow["CD_ITEM"]) + "'");
                                if (dataRowArray2 != null && dataRowArray2.Length > 0)
                                {
                                    if (this.tabControl1.SelectedIndex == 0)
                                    {
                                        dataRowArray2[0]["QT_BAS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_BAS"]));
                                        dataRowArray2[0]["UM_BAS"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_BAS"]));
                                        dataRowArray2[0]["AM_BAS"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["AM_BAS"]));
                                    }
                                    else
                                    {
                                        dataRowArray2[0]["CD_SL"] = this.ctx창고자재재고.CodeValue;
                                        dataRowArray2[0]["QT_GOOD_INV"] = (D.GetDecimal(dataRowArray2[0]["QT_GOOD_INV"]) + Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_GOOD_INV"])));
                                    }
                                }
                                else
                                {
                                    DataRow row = flexGrid.DataTable.NewRow();

                                    row["CD_ITEM"] = dataRow["CD_ITEM"].ToString();
                                    row["NM_ITEM"] = D.GetString(dataRowArray1[0]["NM_ITEM"]);
                                    row["STND_ITEM"] = D.GetString(dataRowArray1[0]["STND_ITEM"]);
                                    row["UNIT_IM"] = D.GetString(dataRowArray1[0]["UNIT_IM"]);
                                    row["CLS_ITEM"] = D.GetString(dataRowArray1[0]["CLS_ITEM"]);
                                    row["NM_CLSITEM"] = D.GetString(dataRowArray1[0]["NM_CLS_ITEM"]);
                                    row["CD_PLANT"] = this.공장코드;
                                    row["YM_STANDARD"] = this.기준년월;

                                    if (this.tabControl1.SelectedIndex == 0)
                                    {
                                        row["QT_BAS"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_BAS"]));
                                        row["UM_BAS"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(dataRow["UM_BAS"]));
                                        row["AM_BAS"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(dataRow["AM_BAS"]));
                                    }
                                    else
                                    {
                                        row["CD_SL"] = this.ctx창고자재재고.CodeValue;
                                        row["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_GOOD_INV"]));
                                    }

                                    flexGrid.DataTable.Rows.Add(row);
                                }
                            }
                            else
                            {
                                string str12 = dataRow["CD_ITEM"].ToString().PadRight(10, ' ').Trim();
                                stringBuilder.AppendLine(str12);
                                flag1 = true;
                            }
                        }
                    }

                    if (flag1)
                    {
                        this.ShowDetailMessage("엑셀 업로드하는 중에 마스터품목, 창고와 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                    }

                    flexGrid.IsDataChanged = true;
                    this._header_JobModeChanged(null, null);
                    this.Page_DataChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex경리재고.Redraw = true;
                this._flex자재재고.Redraw = true;
            }
        }

        private void 엑셀업로드_LOT()
        {
            try
            {
                string NO_KEY = string.Empty;
                bool flag1 = false;
                string str1 = string.Empty;
                string str2 = string.Empty;
                string str3 = string.Empty;
                string str4 = string.Empty;
                string str5 = string.Empty;
                string str6 = string.Empty;
                string str7 = string.Empty;
                string str8 = string.Empty;
                string str9 = string.Empty;
                string str10 = string.Empty;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                FlexGrid flexGrid1 = this._flex자재재고;
                FlexGrid flexGrid2 = this._flexLOT;
                this.공장코드 = D.GetString(this.cbo공장자재재고.SelectedValue);
                this.기준년월 = (this.dtp기준년도자재재고.Text + "00");

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DataTable dataTable1 = new Excel().StartLoadExcel(openFileDialog.FileName);
                    if (!dataTable1.Columns.Contains("CD_ITEM"))
                        return;

                    foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable1.Rows)
                        NO_KEY = NO_KEY + D.GetString(dataRow["CD_ITEM"]) + "|";

                    DataTable dataTable2 = this._biz.엑셀업로드아이템조회(this.공장코드, NO_KEY);
                    StringBuilder stringBuilder = new StringBuilder();
                    string str11 = this.DD("품목코드");
                    stringBuilder.AppendLine(str11);
                    string str12 = "-".PadRight(80, '-');
                    stringBuilder.AppendLine(str12);
                    string str13 = string.Empty;
                    DataTable dataTable3 = this._flex자재재고.DataTable;
                    bool flag3 = dataTable1.Columns.Contains("DT_LIMIT");

                    foreach (DataRow dataRow in dataTable1.Rows)
                    {
                        if (dataRow["CD_ITEM"].ToString().Trim() != null && dataRow["CD_ITEM"].ToString().Trim() != string.Empty && dataRow["CD_ITEM"].ToString().Trim() != "")
                        {
                            DataRow[] dataRowArray1 = dataTable2.Select("CD_ITEM = '" + D.GetString(dataRow["CD_ITEM"]) + "'");
                            if (dataRowArray1 != null && dataRowArray1.Length > 0)
                            {
                                DataRow[] dataRowArray2 = dataTable3.Select("CD_ITEM = '" + D.GetString(dataRow["CD_ITEM"]) + "'");
                                if (dataRowArray2 != null && dataRowArray2.Length > 0)
                                {
                                    dataRowArray2[0]["QT_GOOD_INV"] = (D.GetDecimal(dataRowArray2[0]["QT_GOOD_INV"]) + Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_GOOD_INV"])));
                                }
                                else
                                {
                                    DataRow row = flexGrid1.DataTable.NewRow();

                                    row["CD_ITEM"] = dataRow["CD_ITEM"].ToString();
                                    row["NM_ITEM"] = D.GetString(dataRowArray1[0]["NM_ITEM"]);
                                    row["STND_ITEM"] = D.GetString(dataRowArray1[0]["STND_ITEM"]);
                                    row["UNIT_IM"] = D.GetString(dataRowArray1[0]["UNIT_IM"]);
                                    row["CD_PLANT"] = this.공장코드;
                                    row["YM_STANDARD"] = this.기준년월;
                                    row["CD_SL"] = this.ctx창고자재재고.CodeValue;
                                    row["CLS_ITEM"] = D.GetString(dataRowArray1[0]["CLS_ITEM"]);
                                    row["NM_CLSITEM"] = D.GetString(dataRowArray1[0]["NM_CLS_ITEM"]);
                                    row["NO_LOT"] = D.GetString(dataRowArray1[0]["NO_LOT"]);
                                    row["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_GOOD_INV"]));

                                    flexGrid1.DataTable.Rows.Add(row);
                                }
                                if (D.GetString(dataRowArray1[0]["NO_LOT"]) == "YES")
                                {
                                    DataRow row = flexGrid2.DataTable.NewRow();

                                    row["CD_ITEM"] = dataRow["CD_ITEM"];
                                    row["CD_PLANT"] = this.공장코드;
                                    row["YM_STANDARD"] = this.기준년월;
                                    row["CD_SL"] = this.ctx창고자재재고.CodeValue;
                                    row["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataRow["QT_GOOD_INV"]));
                                    row["NO_LOT"] = dataRow["NO_LOT"];
                                    row["QT_REJECT_INV"] = 0;
                                    row["QT_INSP_INV"] = 0;
                                    row["NO_IO"] = this.GetSeq(this.LoginInfo.CompanyCode, "PU", "36", this.MainFrameInterface.GetStringToday.Substring(0, 6).Trim());
                                    row["CD_MNG1"] = dataRow["CD_MNG1"];
                                    row["CD_MNG2"] = dataRow["CD_MNG2"];
                                    row["CD_MNG3"] = dataRow["CD_MNG3"];
                                    row["CD_MNG4"] = dataRow["CD_MNG4"];
                                    row["CD_MNG5"] = dataRow["CD_MNG5"];
                                    row["CD_MNG6"] = dataRow["CD_MNG6"];
                                    row["CD_MNG7"] = dataRow["CD_MNG7"];
                                    row["CD_MNG8"] = dataRow["CD_MNG8"];
                                    row["CD_MNG9"] = dataRow["CD_MNG9"];
                                    row["CD_MNG10"] = dataRow["CD_MNG10"];
                                    row["CD_MNG11"] = dataRow["CD_MNG11"];
                                    row["CD_MNG12"] = dataRow["CD_MNG12"];
                                    row["CD_MNG13"] = dataRow["CD_MNG13"];
                                    row["CD_MNG14"] = dataRow["CD_MNG14"];
                                    row["CD_MNG15"] = dataRow["CD_MNG15"];
                                    row["CD_MNG16"] = dataRow["CD_MNG16"];
                                    row["CD_MNG17"] = dataRow["CD_MNG17"];
                                    row["CD_MNG18"] = dataRow["CD_MNG18"];
                                    row["CD_MNG19"] = dataRow["CD_MNG19"];
                                    row["CD_MNG20"] = dataRow["CD_MNG20"];

                                    if (flag3)
                                        row["DT_LIMIT"] = dataRow["DT_LIMIT"];
                                    if (dataTable1.Columns.Contains("DC_RMK"))
                                        row["DC_RMK"] = dataRow["DC_RMK"];

                                    flexGrid2.DataTable.Rows.Add(row);
                                }
                            }
                            else
                            {
                                string str14 = dataRow["CD_ITEM"].ToString().PadRight(10, ' ').Trim();
                                stringBuilder.AppendLine(str14);
                                flag1 = true;
                            }
                        }
                    }
                    if (flag1)
                    {
                        this.ShowDetailMessage("엑셀 업로드하는 중에 마스터품목, 창고와 불일치하는 항목들이 있습니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex자재재고.Redraw = true;
                this._flexLOT.Redraw = true;
            }
        }
        #endregion

        #region 경리재고 이벤트
        private void btn추가경리재고_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.FieldCheckHead())
                    return;

                if (this._header경리재고.JobMode == JobModeEnum.추가후수정 &&
                    this._biz.경리재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                          this.cbo공장경리재고.SelectedValue.ToString(),
                                                          (this.dtp기준년도경리재고.Text + "00") }) != "0")
                {
                    base.ShowMessage(공통메세지.이미등록된자료가있습니다);
                }
                else
                {
                    this._flex경리재고.Rows.Add();
                    this._flex경리재고.Row = this._flex경리재고.Rows.Count - 1;
                    this._flex경리재고[this._flex경리재고.Row, "CD_PLANT"] = this.cbo공장경리재고.SelectedValue.ToString();
                    this._flex경리재고[this._flex경리재고.Row, "YM_STANDARD"] = (this.dtp기준년도경리재고.Text + "00");
                    this._flex경리재고.AddFinished();
                    this._flex경리재고.Col = this._flex경리재고.Cols.Fixed;
                    this._header_JobModeChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn삭제경리재고_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray = this._flex경리재고.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    base.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    this._flex경리재고.Redraw = false;

                    for (int index = this._flex경리재고.Rows.Count - 1; index >= this._flex경리재고.Rows.Fixed; --index)
                    {
                        if (this._flex경리재고[index, "S"].ToString() == "Y")
                            this._flex경리재고.Rows.Remove(index);
                    }

                    this._flex경리재고.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn기초자재적용경리재고_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._biz.경리재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                          this.cbo공장경리재고.SelectedValue.ToString(),
                                                          (this.dtp기준년도경리재고.Text + "00") }) != "0")
                {
                    base.ShowMessage(공통메세지.이미등록된자료가있습니다);
                }
                else
                {
                    DataTable dataTable1 = this._biz.경리재고기초자재적용(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                         D.GetString(this.cbo공장경리재고.SelectedValue),
                                                                                         (this.dtp기준년도경리재고.Text + "00"),
                                                                                         D.GetString(this.cbo계정구분경리재고.SelectedValue),
                                                                                         this.ctx품목경리재고.CodeValue });

                    if (dataTable1.Rows.Count == 0)
                    {
                        base.ShowMessage(PageResultMode.SearchNoData);
                    }
                    else
                    {
                        this._header경리재고.JobMode = JobModeEnum.추가후수정;
                        this._flex경리재고.Redraw = false;

                        foreach (DataRow dr in dataTable1.Rows)
                        {
                            dr.SetAdded();
                        }

                        this._flex경리재고.Binding = dataTable1;
                        this._flex경리재고.Redraw = true;
                        this._header_JobModeChanged(null, null);
                        this.Page_DataChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn품목전개경리재고_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._biz.경리재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                          this.cbo공장경리재고.SelectedValue.ToString(),
                                                          (this.dtp기준년도경리재고.Text + "00") }) != "0")
                {
                    this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                }
                else
                {
                    DataTable dataTable1 = this._biz.품목전개(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                                             D.GetString(this.cbo공장경리재고.SelectedValue),
                                                                             (this.dtp기준년도경리재고.Text + "00"),
                                                                             string.Empty,
                                                                             D.GetString(this.cbo계정구분경리재고.SelectedValue),
                                                                             this.ctx품목경리재고.CodeValue });
                    if (dataTable1.Rows.Count == 0)
                    {
                        base.ShowMessage(PageResultMode.SearchNoData);
                    }
                    else
                    {
                        this._header경리재고.JobMode = JobModeEnum.추가후수정;
                        this._flex경리재고.Redraw = false;

                        foreach (DataRow dr in dataTable1.Rows)
                        {
                            dr.SetAdded();
                        }

                        this._flex경리재고.Binding = dataTable1;
                        this._flex경리재고.Redraw = true;
                        this._header_JobModeChanged(null, null);
                        this.Page_DataChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 자재재고 이벤트
        private void btn추가자재재고_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.FieldCheckHead())
                    return;

                if (this._header자재재고.JobMode == JobModeEnum.추가후수정 &&
                    this._biz.자재재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          D.GetString(this.cbo공장자재재고.SelectedValue),
                                                          (this.dtp기준년도자재재고.Text + "00"),
                                                          this.ctx창고자재재고.CodeValue }) != "0")
                {
                    base.ShowMessage(공통메세지.이미등록된자료가있습니다);
                }
                else
                {
                    this._flex자재재고.Rows.Add();
                    this._flex자재재고.Row = this._flex자재재고.Rows.Count - 1;
                    this._flex자재재고[this._flex자재재고.Row, "CD_PLANT"] = D.GetString(this.cbo공장자재재고.SelectedValue);
                    this._flex자재재고[this._flex자재재고.Row, "CD_SL"] = this.ctx창고자재재고.CodeValue;
                    this._flex자재재고[this._flex자재재고.Row, "YM_STANDARD"] = (this.dtp기준년도자재재고.Text + "00");
                    this._flex자재재고.AddFinished();
                    this._flex자재재고.Col = this._flex자재재고.Cols.Fixed;
                    this._header_JobModeChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn삭제자재재고_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray1 = this._flex자재재고.DataTable.Select("S = 'Y'");

                if (Config.MA_ENV.LOT관리)
                {
                    foreach (DataRow row in dataRowArray1)
                    {
                        DataRow[] dataRowArray2 = this._flexLOT.DataTable.Select("CD_ITEM ='" + D.GetString(row["CD_ITEM"]) + "'");
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            base.ShowMessage(this.DD("LOT번호가 존재합니다."));
                            this._flex자재재고.Select(this._flex자재재고.DataTable.Rows.IndexOf(row) + this._flex자재재고.Rows.Fixed, 1);
                            return;
                        }
                    }
                }
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    base.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    this._flex자재재고.Redraw = false;

                    for (int index = this._flex자재재고.Rows.Count - 1; index >= this._flex자재재고.Rows.Fixed; --index)
                    {
                        if (this._flex자재재고[index, "S"].ToString() == "Y")
                            this._flex자재재고.Rows.Remove(index);
                    }

                    this._flex자재재고.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn자재재고이월_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo공장자재재고.SelectedValue == DBNull.Value ||
                    D.GetString(this.cbo공장자재재고.SelectedValue) == "" ||
                    D.GetString(this.cbo공장자재재고.SelectedValue) == string.Empty)
                {
                    base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장자재재고.Text });
                    this.cbo공장자재재고.Focus();
                }
                else if (this.dtp입력일자자재재고.Text == "" || this.dtp입력일자자재재고.Text == string.Empty)
                {
                    base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl입력일자자재재고.Text });
                    this.dtp입력일자자재재고.Focus();
                }
                else if (this.ctx담당자자재재고.CodeValue == "" ||
                         this.ctx담당자자재재고.CodeValue == string.Empty)
                {
                    base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자자재재고.Text });
                    this.ctx담당자자재재고.Focus();
                }

                new P_PU_OPENAM_REG_SUB(this.MainFrameInterface,
                                        D.GetString(this.cbo공장자재재고.SelectedValue),
                                        this._header자재재고.CurrentRow["CD_DEPT"].ToString(),
                                        this.ctx담당자자재재고.CodeValue.ToString(),
                                        this.dtp입력일자자재재고.Text).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn품목전개자재재고_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.FieldCheckHead()) return;

                if (this._biz.자재재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          D.GetString(this.cbo공장자재재고.SelectedValue),
                                                          (this.dtp기준년도자재재고.Text + "00"),
                                                          this.ctx창고자재재고.CodeValue }) != "0")
                {
                    base.ShowMessage(공통메세지.이미등록된자료가있습니다);
                }
                else
                {
                    DataTable dataTable1 = this._biz.품목전개(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             D.GetString(this.cbo공장자재재고.SelectedValue),
                                                                             (this.dtp기준년도자재재고.Text + "00"),
                                                                             this.ctx창고자재재고.CodeValue,
                                                                             D.GetString(this.cbo계정구분자재재고.SelectedValue),
                                                                             this.ctx품목경리재고.CodeValue });

                    if (dataTable1.Rows.Count == 0)
                    {
                        base.ShowMessage(PageResultMode.SearchNoData);
                    }
                    else
                    {
                        this._header자재재고.JobMode = JobModeEnum.추가후수정;
                        this._flex자재재고.Redraw = false;

                        foreach (DataRow dr in dataTable1.Rows)
                        {
                            dr.SetAdded();
                        }

                        this._flex자재재고.Binding = dataTable1;
                        this._flex자재재고.Redraw = true;
                        this._header_JobModeChanged(null, null);
                        this.Page_DataChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }
        #endregion

        #region LOT 이벤트
        private void btn추가LOT_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.FieldCheckHead()) return;

                if (this._header자재재고.JobMode == JobModeEnum.추가후수정 &&
                    this._biz.자재재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          D.GetString(this.cbo공장자재재고.SelectedValue),
                                                          (this.dtp기준년도자재재고.Text + "00"),
                                                          this.ctx창고자재재고.CodeValue }) != "0")
                {
                    base.ShowMessage(공통메세지.이미등록된자료가있습니다);
                }
                else if (D.GetString(this._flex자재재고[this._flex자재재고.Row, "NO_LOT"]) != "YES")
                {
                    base.ShowMessage("해당품목은 LOT품이 아닙니다.다시 확인해주세요");
                }
                else
                {
                    this._flexLOT.Rows.Add();

                    this._flexLOT.Row = (this._flexLOT.Rows.Count - 1);
                    this._flexLOT[this._flexLOT.Row, "CD_PLANT"] = D.GetString(this.cbo공장자재재고.SelectedValue);
                    this._flexLOT[this._flexLOT.Row, "CD_SL"] = this.ctx창고자재재고.CodeValue;
                    this._flexLOT[this._flexLOT.Row, "YM_STANDARD"] = (this.dtp기준년도자재재고.Text + "00");
                    this._flexLOT[this._flexLOT.Row, "CD_ITEM"] = this._flex자재재고[this._flex자재재고.Row, "CD_ITEM"];
                    this._flexLOT[this._flexLOT.Row, "QT_REJECT_INV"] = 0;
                    this._flexLOT[this._flexLOT.Row, "QT_INSP_INV"] = 0;
                    this._flexLOT[this._flexLOT.Row, "NO_IO"] = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "36", this.MainFrameInterface.GetStringToday.Substring(0, 6).Trim());
                    this._flexLOT.AddFinished();
                    this._flexLOT.Col = this._flexLOT.Cols.Fixed;

                    this._header_JobModeChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn삭제LOT_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray = this._flexLOT.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    base.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    this._flexLOT.Redraw = false;

                    for (int index = this._flexLOT.Rows.Count - 1; index >= this._flexLOT.Rows.Fixed; --index)
                    {
                        if (this._flexLOT[index, "S"].ToString() == "Y")
                            this._flexLOT.Rows.Remove(index);
                    }

                    this.SUM_LOT_QT();
                    this.ToolBarSaveButtonEnabled = this._flexLOT.IsDataChanged;
                    this._flexLOT.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnLOT재고이월_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.FieldCheckHead()) return;

                if (this._biz.자재재고확인(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                          D.GetString(this.cbo공장자재재고.SelectedValue),
                                                          (this.dtp기준년도자재재고.Text + "00"),
                                                          this.ctx창고자재재고.CodeValue }) != "0")
                {
                    base.ShowMessage(공통메세지.이미등록된자료가있습니다);
                }
                else
                {
                    DataTable dataTable1 = this._biz.품목전개(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             D.GetString(this.cbo공장자재재고.SelectedValue),
                                                                             (this.dtp기준년도자재재고.Text + "00"),
                                                                             this.ctx창고자재재고.CodeValue,
                                                                             D.GetString(this.cbo계정구분자재재고.SelectedValue) });
                    if (dataTable1.Rows.Count == 0)
                    {
                        base.ShowMessage(PageResultMode.SearchNoData);
                    }
                    else
                    {
                        this._header자재재고.JobMode = JobModeEnum.추가후수정;
                        this._flex자재재고.DataTable.Rows.Clear();
                        DataTable dataTable2 = this._flex자재재고.DataTable;
                        this._flex자재재고.Redraw = false;

                        for (int index = 0; index < dataTable1.Rows.Count; ++index)
                        {
                            DataRow row = dataTable2.NewRow();

                            row["CD_PLANT"] = D.GetString(this.cbo공장자재재고.SelectedValue);
                            row["CD_SL"] = this.ctx창고자재재고.CodeValue;
                            row["YM_STANDARD"] = ((this.dtp기준년도자재재고).Text + "00");
                            row["S"] = dataTable1.Rows[index]["S"].ToString();
                            row["CD_ITEM"] = dataTable1.Rows[index]["CD_ITEM"].ToString();
                            row["NM_ITEM"] = dataTable1.Rows[index]["NM_ITEM"].ToString();
                            row["STND_ITEM"] = dataTable1.Rows[index]["STND_ITEM"].ToString();
                            row["UNIT_IM"] = dataTable1.Rows[index]["UNIT_IM"].ToString();
                            row["CLS_ITEM"] = dataTable1.Rows[index]["CLS_ITEM"].ToString();
                            row["NM_CLSITEM"] = dataTable1.Rows[index]["NM_CLSITEM"].ToString();
                            row["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataTable1.Rows[index]["QT_BAS"]));
                            row["QT_TRANS_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(dataTable1.Rows[index]["QT_TRANS_INV"]));
                            row["NO_LOT"] = dataTable1.Rows[index]["NO_LOT"].ToString();

                            dataTable2.Rows.Add(row);
                        }

                        this._flex자재재고.Redraw = true;
                        this._header_JobModeChanged(null, null);
                        this.Page_DataChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            FlexGrid grid;

            try
            {
                if (e.Parameter.HelpID == HelpID.P_USER)
                {
                    grid = ((FlexGrid)sender);

                    e.Parameter.UserParams = "공장품목;H_CZ_MA_CUSTOMIZE_SUB";
                    e.Parameter.P11_ID_MENU = "H_MA_PITEM_SUB";
                    e.Parameter.P21_FG_MODULE = "N";
                    e.Parameter.P92_DETAIL_SEARCH_CODE = D.GetString(grid["CD_ITEM"]);
                    e.Parameter.MultiHelp = true;
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
                HelpReturn helpReturn = e.Result;
                FlexGrid flexGrid = sender as FlexGrid;
                bool flag = true;

                if (e.Result.DialogResult == DialogResult.Cancel)
                    return;

                foreach (DataRow dataRow in helpReturn.Rows)
                {
                    if (flag)
                    {
                        flexGrid["CD_ITEM"] = dataRow["CD_ITEM"];
                        flexGrid["NM_ITEM"] = dataRow["NM_ITEM"];
                        flexGrid["STND_ITEM"] = dataRow["STND_ITEM"];
                        flexGrid["UNIT_IM"] = dataRow["UNIT_IM"];
                        flexGrid["CLS_ITEM"] = dataRow["CLS_ITEM"];
                        flexGrid["NM_CLSITEM"] = dataRow["NM_CLS_ITEM"];

                        if (flexGrid.Name == this._flex자재재고.Name)
                        {
                            if (D.GetString(dataRow["FG_SERNO"]) == "002")
                                flexGrid["NO_LOT"] = "YES";
                            else
                                flexGrid["NO_LOT"] = "NO";
                        }

                        flag = false;
                    }
                    else
                    {
                        if (flexGrid.Name == this._flex경리재고.Name)
                            this.btn추가경리재고_Click(null, null);
                        else if (flexGrid.Name == this._flex자재재고.Name)
                            this.btn추가자재재고_Click(null, null);

                        flexGrid["CD_ITEM"] = dataRow["CD_ITEM"];
                        flexGrid["NM_ITEM"] = dataRow["NM_ITEM"];
                        flexGrid["STND_ITEM"] = dataRow["STND_ITEM"];
                        flexGrid["UNIT_IM"] = dataRow["UNIT_IM"];
                        flexGrid["CLS_ITEM"] = dataRow["CLS_ITEM"];
                        flexGrid["NM_CLSITEM"] = dataRow["NM_CLS_ITEM"];

                        if ((flexGrid).Name == this._flex자재재고.Name)
                        {
                            if (D.GetString(dataRow["FG_SERNO"]) == "002")
                                flexGrid["NO_LOT"] = "YES";
                            else
                                flexGrid["NO_LOT"] = "NO";
                        }
                    }
                }

                flexGrid.RemoveDummyColumnAll();
                flexGrid.AddFinished();

                flexGrid.Col = flexGrid.Cols.Fixed;
                flexGrid.Redraw = true;
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
                string str = ((C1FlexGridBase)sender)[e.Row, e.Col].ToString();
                string editData = ((FlexGrid)sender).EditData;

                if (str.ToUpper() == editData.ToUpper())
                    return;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    if (this._flex경리재고.Cols[e.Col].Name == "S")
                        return;

                    switch (this._flex경리재고.Cols[e.Col].Name)
                    {
                        case "QT_BAS":
                            this._flex경리재고[e.Row, "AM_BAS"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) * D.GetDecimal(this._flex경리재고[e.Row, "UM_BAS"]));
                            break;
                        case "UM_BAS":
                            this._flex경리재고[e.Row, "AM_BAS"] = Unit.원화금액(DataDictionaryTypes.PU, D.GetDecimal(editData) * D.GetDecimal(this._flex경리재고[e.Row, "QT_BAS"]));
                            break;
                        case "AM_BAS":
                            if (D.GetDecimal(this._flex경리재고[e.Row, "QT_BAS"]) != 0)
                            {
                                this._flex경리재고[e.Row, "UM_BAS"] = Unit.원화단가(DataDictionaryTypes.PU, D.GetDecimal(editData) / D.GetDecimal(this._flex경리재고[e.Row, "QT_BAS"]));
                                break;
                            }
                            else
                            {
                                base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { "수량" });
                                this._flex경리재고[e.Row, "AM_BAS"] = 0;
                                break;
                            }
                    }
                }
                if (this.tabControl1.SelectedIndex == 1)
                {
                    if (this._flexLOT.Cols[e.Col].Name.Contains("CD_MNG"))
                    {
                        if (!(str != editData))
                            return;

                        this.ToolBarSaveButtonEnabled = true;
                    }
                    else
                    {
                        switch (this._flexLOT.Cols[e.Col].Name)
                        {
                            case "QT_GOOD_INV":
                                this.SUM_LOT_QT();
                                break;
                        }
                    }
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
                FlexGrid flexGrid = (FlexGrid)sender;

                if (this.tabControl1.SelectedIndex == 0)
                {
                    if (!this.재고평가체크())
                        e.Cancel = true;
                }

                if (this.tabControl1.SelectedIndex != 1)
                    return;

                if (flexGrid.Name == this._flexLOT.Name)
                {
                    if (this._flexLOT.Cols[e.Col].Name == "NO_LOT" && this._flexLOT.RowState() != DataRowState.Added)
                        e.Cancel = true;
                    if (ComFunc.전용코드("기초재고등록-LOT이월사용유무") == "000")
                    {
                        e.Cancel = true;
                        return;
                    }
                    else
                        return;
                }
                else if (flexGrid.Name == this._flex자재재고.Name)
                {
                    if (Config.MA_ENV.LOT관리 &&
                        this._flex자재재고.Cols[e.Col].Name == "QT_GOOD_INV" &&
                        D.GetString(this._flex자재재고["NO_LOT"]) == "YES")
                    {
                        e.Cancel = true;
                        return;
                    }
                    else
                        return;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex자재재고_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flex자재재고.IsBindingEnd ||
                    !this._flex자재재고.HasNormalRow ||
                    this._flexLOT.DataSource == null ||
                    ((CellRange)e.OldRange).r1 == ((CellRange)e.NewRange).r1)
                    return;

                this._flexLOT.RowFilter = "CD_ITEM = '" + D.GetString(this._flex자재재고["CD_ITEM"]) + "' AND ISNULL(CD_SL,'') = '" + D.GetString(this._flex자재재고["CD_SL"]) + "'";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {

            }
        }
        #endregion

        #region 기타 메소드
        private bool FieldCheckHead()
        {
            try
            {
                if (this.tabControl1.SelectedIndex == 0)
                {
                    if (this.cbo공장경리재고.SelectedValue == DBNull.Value ||
                        this.cbo공장경리재고.SelectedValue.ToString() == "" ||
                        this.cbo공장경리재고.SelectedValue.ToString() == string.Empty)
                    {
                        base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장경리재고.Text });
                        this.cbo공장경리재고.Focus();
                        return false;
                    }
                    else if (this.dtp기준년도경리재고.Text == "" ||
                             this.dtp기준년도경리재고.Text == string.Empty)
                    {
                        base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl기준년도경리재고.Text });
                        this.dtp기준년도경리재고.Focus();
                        return false;
                    }
                    else if (this.dtp입력일자경리재고.Text == "" ||
                             this.dtp입력일자경리재고.Text == string.Empty)
                    {
                        base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl입력일자경리재고.Text });
                        this.dtp입력일자경리재고.Focus();
                        return false;
                    }
                    else if (this.ctx담당자경리재고.CodeValue == "" || this.ctx담당자경리재고.CodeValue == string.Empty)
                    {
                        base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자경리재고.Text });
                        this.ctx담당자경리재고.Focus();
                        return false;
                    }
                }
                else if (this.cbo공장자재재고.SelectedValue == DBNull.Value ||
                         D.GetString(this.cbo공장자재재고.SelectedValue) == "" ||
                         D.GetString(this.cbo공장자재재고.SelectedValue) == string.Empty)
                {
                    base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장자재재고.Text });
                    this.cbo공장자재재고.Focus();
                    return false;
                }
                else if (this.dtp기준년도자재재고.Text == "" ||
                         this.dtp기준년도자재재고.Text == string.Empty)
                {
                    base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl기준년도자재재고.Text });
                    this.dtp기준년도경리재고.Focus();
                    return false;
                }
                else if (this.dtp입력일자자재재고.Text == "" ||
                         this.dtp입력일자자재재고.Text == string.Empty)
                {
                    base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl입력일자자재재고.Text });
                    this.dtp입력일자자재재고.Focus();
                    return false;
                }
                else if (this.ctx창고자재재고.CodeValue == "" ||
                         this.ctx창고자재재고.CodeValue == string.Empty)
                {
                    base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl창고자재재고.Text });
                    this.ctx창고자재재고.Focus();
                    return false;
                }
                else if (this.ctx담당자자재재고.CodeValue == "" ||
                         this.ctx담당자자재재고.CodeValue == string.Empty)
                {
                    base.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl담당자자재재고.Text });
                    this.ctx담당자자재재고.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex, this.PageName);
            }

            return true;
        }

        private void SUM_LOT_QT()
        {
            try
            {
                Decimal num = 0;

                this._flexLOT.Redraw = false;

                if (this._flexLOT.DataView.Count > 0)
                {
                    for (int @fixed = this._flexLOT.Rows.Fixed; @fixed < this._flexLOT.Rows.Count; ++@fixed)
                        num += D.GetDecimal(this._flexLOT[@fixed, "QT_GOOD_INV"]);

                    this._flex자재재고[this._flex자재재고.Row, "QT_GOOD_INV"] = num;
                }

                this._flexLOT.Redraw = true;
            }
            catch
            {

            }
        }
        #endregion
    }
}

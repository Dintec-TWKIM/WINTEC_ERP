using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using Duzon.Windows.Print;
using DzHelpFormLib;
using pur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PU_PI_REG : PageBase
	{
        private P_PU_PI_REG_BIZ _biz = new P_PU_PI_REG_BIZ();
	    private FreeBinding _header;
        private DataSet _ds_combo;
        private string _cddept;
        private OpenFileDialog m_FileDlg = new OpenFileDialog();

        public P_CZ_PU_PI_REG()
		{
			try
			{
				InitializeComponent();
				this._header = new FreeBinding();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(2, 1, false);
            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_ITEM", "품목", 120, true);
            this._flex.SetCol("NM_ITEM", "품목명", 150);
            this._flex.SetCol("STND_ITEM", "규격", 80);
            this._flex.SetCol("BARCODE", "BARCODE", 80);
            this._flex.SetCol("UNIT_IM", "재고단위", 60);
            this._flex.SetCol("NM_CLS_ITEM", "계정구분", 60);
            this._flex.SetCol("QT_GOOD_INV", "장부수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_GOOD_INV2", "실사수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_CHA", "차이수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            if (this.프로젝트사용)
            {
                this._flex.SetCol("CD_PJT", "프로젝트", 120, false, typeof(string));
                this._flex.SetCol("NM_PROJECT", "프로젝트명", 120, false, typeof(string));
                if (this.PJT형여부)
                {
                    this._flex.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                    this._flex.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                    this._flex.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                    this._flex.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                }
            }
            this._flex.SetCol("NO_DESIGN", "도면번호", 120, false);
            if (Global.MainFrame.ServerKey == "WJIS" || Global.MainFrame.ServerKey == "DZSQL")
                this._flex.SetCol("LOTSIZE", "LOTSIZE", 120, true);
            else
                this._flex.SetCol("LOTSIZE", "LOTSIZE", 120, false);
            this._flex.SetCol("GRP_ITEM", "품목군코드", 100);
            this._flex.SetCol("NM_ITEMGRP", "품목군명", 140);
            this._flex.SetCol("GRP_MFG", "제품군코드", 100);
            this._flex.SetCol("NM_GRP_MFG", "제품군명", 140);
            this._flex.SetCol("STND_DETAIL_ITEM", "세부규격", 120);
            this._flex.SetCol("MAT_ITEM", "재질", 120);
            this._flex.SetCol("NM_CLS_L", "대분류", 120);
            this._flex.SetCol("NM_CLS_M", "중분류", 120);
            this._flex.SetCol("NM_CLS_S", "소분류", 120);
            this._flex.SetCol("LN_PARTNER", "주거래처", 120);
            this._flex.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM",
                                                                                                             "NM_ITEM",
                                                                                                             "STND_ITEM",
                                                                                                             "UNIT_IM" },
                                                                                              new string[] { "CD_ITEM",
                                                                                                             "NM_ITEM",
                                                                                                             "STND_ITEM",
                                                                                                             "UNIT_IM" }, ResultMode.SlowMode);
            this._flex.SettingVersion = "2023.3.16.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            if (Global.MainFrame.ServerKey == "WJIS" || Global.MainFrame.ServerKey == "DZSQL")
                this._flex.SetDummyColumn("LOTSIZE");
            this._flex.VerifyAutoDelete = new string[] { "CD_ITEM" };
            if (!this.PJT형여부)
                this._flex.VerifyDuplicate = new string[] { "CD_ITEM" };
            this._flex.SetDummyColumn("S");
            this._flex[0, "QT_GOOD_INV"] = this.DD("양품재고");
            this._flex[0, "QT_GOOD_INV2"] = this.DD("양품재고");
            this._flex[0, "QT_CHA"] = this.DD("양품재고");
            this._flex[1, "QT_GOOD_INV"] = this.DD("장부수량");
            this._flex[1, "QT_GOOD_INV2"] = this.DD("실사수량");
            this._flex[1, "QT_CHA"] = this.DD("차이수량");
            this._flex.SetHeaderMerge();
            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
        }

        private void InitEvent()
		{
            this.btn삭제.Click += new EventHandler(this.btn_삭제Click);
            this.btn추가.Click += new EventHandler(this.btn_추가Click);
            this.ctx담당자.QueryAfter += new BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            this.ctx창고.QueryBefore += new BpQueryHandler(this.OnBpCodeTextBox_QueryBefore);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.엑셀_Click);
            this.btn품목전개.Click += new EventHandler(this.btn품목전개_Click);
            this.btn업체전용.Click += new EventHandler(this.btn업체전용_Click);

            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flex_AfterCodeHelp);
            this._flex.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
        }

        private void InitCombo()
        {
            try
            {
                this._ds_combo = this.GetComboData("NC;MA_PLANT", "S;MA_B000010");
                this.cbo공장.DataSource = this._ds_combo.Tables[0];
                this.cbo공장.DisplayMember = "NAME";
                this.cbo공장.ValueMember = "CODE";
                this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
                new SetControl().SetCombobox(this.cbo계정구분, Duzon.ERPU.MF.MF.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.품목.품목계정, true));
                if (Global.MainFrame.ServerKeyCommon == "ONEGENE")
                {
                    this.btn업체전용.Visible = true;
                    this.btn업체전용.Text = "MES재고적용";
                }
                else
                {
                    if (!(Global.MainFrame.ServerKeyCommon == "MACROGEN"))
                        return;
                    this.chk품목사용여부.Visible = true;
                }
            }
            catch (coDbException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void InitPaint()
		{
			base.InitPaint();
            this.InitCombo();
            DataSet dataSet = this._biz.Search(Global.MainFrame.LoginInfo.CompanyCode, "@#$%", "", "", "", "", "", "");
            this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
            this._header.ClearAndNewRow();
            this._flex.Binding = dataSet.Tables[1];
            this.InitializeControlDataValue();
            this.chk프로젝트유무.Checked = Settings1.Default.chk_yn_pjt;
            if (this.프로젝트사용)
            {
                this.ctx프로젝트.Enabled = true;
                this.btn적용.Enabled = true;
                if (!this.PJT형여부)
                    this.chk프로젝트유무.Enabled = true;
            }
            else
                this.chk프로젝트유무.Checked = false;
            this.oneGrid1.UseCustomLayout = true;
            this.setNecessaryCondition(new object[0], this.oneGrid1);
            this.oneGrid1.IsSearchControl = false;
            this.oneGrid1.InitCustomLayout();
        }
        private void Search(string p_NO_SV)
        {
            DataSet dataSet = this._biz.Search(this.MainFrameInterface.LoginInfo.CompanyCode, p_NO_SV, D.GetString(this.cbo계정구분.SelectedValue), this.chkA.Checked ? "A" : "", this.chkB.Checked ? "B" : "", this.chkC.Checked ? "C" : "", this.ctx프로젝트.CodeValue, this.PJT형여부 ? "Y" : "N");
            this._header.SetDataTable(dataSet.Tables[0]);
            this._flex.Binding = dataSet.Tables[1];
            if (!(Config.MA_ENV.YN_UNIT != "Y"))
                return;
            this.btn추가.Enabled = this._flex.HasNormalRow;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                if (!this.chkA.Checked && !this.chkB.Checked && !this.chkC.Checked)
                {
                    this.ShowMessage("PU_M000101");
                }
                else
                {
                    string str1 = "";
                    if (this.chkA.Checked)
                        str1 += "'A'";
                    if (this.chkB.Checked)
                        str1 = !this.chkA.Checked ? str1 + "'B'" : str1 + ", 'B' ";
                    if (this.chkC.Checked)
                    {
                        string str2 = !this.chkA.Checked && !this.chkB.Checked ? str1 + "'C'" : str1 + ", 'C' ";
                    }
                    P_PU_PI_SUB pPuPiSub = new P_PU_PI_SUB(this.MainFrameInterface, this._ds_combo.Tables[0]);
                    if (pPuPiSub.ShowDialog(this) == DialogResult.OK)
                        this.Search(pPuPiSub.m_NO_SV);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeAdd() => base.BeforeAdd() && this.MsgAndSave(PageActionMode.Search);

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeAdd())
                    return;
                this._flex.DataTable.Rows.Clear();
                this._flex.AcceptChanges();
                this._header.ClearAndNewRow();
                this.InitializeControlDataValue();
                this.btn추가.Enabled = false;
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
                if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까) == DialogResult.Yes)
                {
                    if (this.txt실사번호.Text != "")
                    {
                        if (this._biz.Delete(new object[] { this.txt실사번호.Text,
                                                            this.MainFrameInterface.LoginInfo.CompanyCode }).Result)
                        {
                            this.ShowMessage("자료가 정상적으로 삭제되었습니다.");
                        }
                        else
                        {
                            this.ShowMessage("작업을 정상적으로 처리하지 못했습니다.");
                            return;
                        }
                    }
                    this.OnToolBarAddButtonClicked(null, null);
                    this.InitializeControlDataValue();
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
                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;
                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                this.btn추가.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSave()
        {
            if (!this.CheckFieldHead())
                return false;
            if (this._flex.HasNormalRow)
                return true;
            this.OnToolBarDeleteButtonClicked(null, null);
            return false;
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData() || !this.Verify())
                    return false;
                if (this.추가모드여부)
                {
                    string seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "21", this.dtp실사일자.Text.Substring(0, 6));
                    this._header.CurrentRow["NO_SV"] = seq;
                    this._header.CurrentRow["CD_COMPANY"] = this.LoginInfo.CompanyCode;
                    this._header.CurrentRow["DT_SV"] = this.dtp실사일자.Text;
                    this._header.CurrentRow["CD_DEPT"] = this._cddept;
                    this._header.CurrentRow["NO_EMP"] = this.ctx담당자.CodeValue;
                    for (int row = this._flex.Rows.Fixed; row < this._flex.Rows.Count; ++row)
                        this._flex[row, "NO_SV"] = seq;
                    this.txt실사번호.Text = seq;
                }
                DataTable changes1 = this._header.GetChanges();
                DataTable changes2 = this._flex.GetChanges();
                if (changes1 == null && changes2 == null)
                    return true;
                ResultData[] resultDataArray = this._biz.Save(changes1, changes2);
                if (!resultDataArray[0].Result || !resultDataArray[1].Result)
                    return false;
                this._header.AcceptChanges();
                this._flex.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return false;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            if (this.Z_Print())
                return;
            ReportHelper reportHelper = new ReportHelper("R_PU_PI_REG_0", "재고실사 현품표");
            reportHelper.SetData("실사일자", this.dtp실사일자.Text.Substring(0, 4) + "/" + this.dtp실사일자.Text.Substring(4, 2) + "/" + this.dtp실사일자.Text.Substring(6, 2));
            reportHelper.SetData("창고코드", this.ctx창고.CodeValue);
            reportHelper.SetData("창고명", this.ctx창고.CodeName);
            reportHelper.SetDataTable(this._flex.DataTable);
            reportHelper.Print();
        }

        private void btn품목전개_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.CheckFieldHead())
                    return;
                string p_NO_SV = this._biz.재고실사등록여부(this.ctx창고.CodeValue, this.dtp실사일자.MaskEditBox.ClipText);
                if (p_NO_SV != string.Empty && this.ShowMessage("해당월에 재고실사번호가 있습니다. 내역을 보시겠습니까", "QY2") == DialogResult.Yes)
                {
                    this.Search(p_NO_SV);
                }
                else
                {
                    DataSet dataSet = this._biz.품목전개(new object[] { this.ctx창고.CodeValue,
                                                                       D.GetString(this.cbo공장.SelectedValue),
                                                                       this.MainFrameInterface.LoginInfo.CompanyCode,
                                                                       this.dtp실사일자.MaskEditBox.ClipText,
                                                                       this._header.CurrentRow["QT_YN"],
                                                                       "N",
                                                                       D.GetString(this.cbo계정구분.SelectedValue),
                                                                       "",
                                                                       this.chk프로젝트유무.Checked ?  "Y" :  "N",
                                                                       Global.SystemLanguage.MultiLanguageLpoint,
                                                                       this.chk품목사용여부.Checked ?  "Y" :  "N" });
                    if (dataSet != null && dataSet.Tables.Count > 0)
                    {
                        DataTable table = dataSet.Tables[0];
                        if (table != null && table.Rows.Count > 0)
                        {
                            this._flex.Redraw = false;
                            this._flex.DataTable.Clear();
                            DataTable dataTable = this._flex.DataTable;
                            for (int index = 0; index < table.Rows.Count; ++index)
                            {
                                DataRow row = dataTable.NewRow();
                                row["CD_COMPANY"] = this.LoginInfo.CompanyCode;
                                row["NO_SV"] = this.txt실사번호.Text;
                                row["CD_ITEM"] = table.Rows[index]["CD_ITEM"].ToString();
                                row["NM_ITEM"] = table.Rows[index]["NM_ITEM"].ToString();
                                row["STND_ITEM"] = table.Rows[index]["STND_ITEM"].ToString();
                                row["UNIT_IM"] = table.Rows[index]["UNIT_IM"].ToString();
                                row["CLS_ITEM"] = table.Rows[index]["CLS_ITEM"].ToString();
                                row["NM_CLS_ITEM"] = table.Rows[index]["NM_CLS_ITEM"].ToString();
                                row["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(table.Rows[index]["QT_GOOD_INV"]));
                                row["QT_GOOD_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(table.Rows[index]["QT_GOOD_INV2"]));
                                row["QT_CHA"] = (Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"])) - Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV2"])));
                                row["QT_REJECT_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(table.Rows[index]["QT_REJECT_INV"]));
                                row["QT_REJECT_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(table.Rows[index]["QT_REJECT_INV2"]));
                                row["QT_TRANS_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(table.Rows[index]["QT_TRANS_INV"]));
                                row["QT_TRANS_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(table.Rows[index]["QT_TRANS_INV2"]));
                                row["QT_INSP_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(table.Rows[index]["QT_INSP_INV"]));
                                row["QT_INSP_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(table.Rows[index]["QT_INSP_INV2"]));
                                row["YN_BLOCK"] = "N";
                                row["CD_PJT"] = table.Rows[index]["CD_PJT"];
                                row["NM_PROJECT"] = table.Rows[index]["NM_PROJECT"];
                                row["SEQ_PROJECT"] = table.Rows[index]["SEQ_PROJECT"];
                                row["CD_PJT_ITEM"] = table.Rows[index]["CD_PJT_ITEM"];
                                row["NM_PJT_ITEM"] = table.Rows[index]["NM_PJT_ITEM"];
                                row["PJT_ITEM_STND"] = table.Rows[index]["PJT_ITEM_STND"];
                                row["NO_DESIGN"] = table.Rows[index]["NO_DESIGN"];
                                row["LOTSIZE"] = table.Rows[index]["LOTSIZE"];
                                row["GRP_ITEM"] = table.Rows[index]["GRP_ITEM"];
                                row["NM_ITEMGRP"] = table.Rows[index]["NM_ITEMGRP"];
                                row["GRP_MFG"] = table.Rows[index]["GRP_MFG"];
                                row["NM_GRP_MFG"] = table.Rows[index]["NM_GRP_MFG"];
                                row["STND_DETAIL_ITEM"] = table.Rows[index]["STND_DETAIL_ITEM"];
                                row["MAT_ITEM"] = table.Rows[index]["MAT_ITEM"];
                                row["NM_CLS_L"] = table.Rows[index]["NM_CLS_L"];
                                row["NM_CLS_M"] = table.Rows[index]["NM_CLS_M"];
                                row["NM_CLS_S"] = table.Rows[index]["NM_CLS_S"];
                                row["LN_PARTNER"] = table.Rows[index]["LN_PARTNER"];
                                dataTable.Rows.Add(row);
                            }
                            this._flex.AddFinished();
                            this._flex.Col = this._flex.Cols.Fixed;
                            this._flex.Select(this._flex.Rows.Fixed, this._flex.Cols.Fixed);
                            this._flex.Redraw = true;
                            this._flex.Focus();
                            this.ToolBarSearchButtonEnabled = true;
                            this.ToolBarSaveButtonEnabled = true;
                            this.ToolBarAddButtonEnabled = true;
                            if (Config.MA_ENV.YN_UNIT != "Y")
                                this.btn추가.Enabled = true;
                            Settings1.Default.chk_yn_pjt = this.PJT형여부 || this.chk프로젝트유무.Checked;
                            Settings1.Default.Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitializeControlDataValue()
        {
            this.txt실사번호.Text = "";
            this.dtp실사일자.Text = this.MainFrameInterface.GetStringToday;
            this.ctx창고.CodeName = "";
            this.ctx창고.CodeValue = "";
            this._cddept = this.LoginInfo.DeptCode;
            this.ctx담당자.CodeName = this.LoginInfo.EmployeeName;
            this.ctx담당자.CodeValue = this.LoginInfo.EmployeeNo;
            this.ctx프로젝트.SetCode("", "");
            this.txt비고.Text = "";
            this.btn품목전개.Enabled = true;
            this.ToolBarSearchButtonEnabled = true;
            this._flex.DataTable.Clear();
        }

        private bool CheckFieldHead()
        {
            try
            {
                if (this.Lot사용여부 && Settings1.Default.chk_use_lot != "Y" && this.ShowMessage("LOT 재고실사는 적용할 수 없는 메뉴입니다\nLOT재고실사관리 메뉴를 이용하십시오\n계속 하시겠습니까?", "QY2") == DialogResult.Yes)
                    Settings1.Default.chk_use_lot = "Y";
                if (this.dtp실사일자.MaskEditBox.ClipText == "")
                {
                    this.dtp실사일자.Focus();
                    this.ShowMessage("WK1_004", this.lbl실사일자.Text);
                    return false;
                }
                if (D.GetString(this.cbo공장.SelectedValue) == "")
                {
                    this.cbo공장.Focus();
                    this.ShowMessage("WK1_004", this.lbl공장.Text);
                    return false;
                }
                if (this.ctx창고.CodeValue.ToString() == "")
                {
                    this.ctx창고.Focus();
                    this.ShowMessage("WK1_004", this.lbl창고.Text);
                    return false;
                }
                if (this.ctx담당자.CodeValue.ToString() == "")
                {
                    this.ctx담당자.Focus();
                    this.ShowMessage("WK1_004", this.lbl담당자.Text);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return true;
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

        private void OnBpCodeTextBox_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult != DialogResult.OK)
                    return;
                DataRow[] rows = e.HelpReturn.Rows;
                if (e.HelpID == HelpID.P_MA_EMP_SUB)
                    this._cddept = rows[0]["CD_DEPT"].ToString();
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
                if (D.GetString(this._flex[this._flex.Row, "NO_BALANCE"]) != "")
                {
                    e.Cancel = true;
                }
                else
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "CD_ITEM":
                            if (this._flex.RowState() != DataRowState.Added)
                            {
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

        private bool 추가모드여부 => this._header.JobMode == JobModeEnum.추가후수정;

        private bool 프로젝트사용 => Config.MA_ENV.프로젝트사용;

        private bool PJT형여부 => Config.MA_ENV.PJT형여부 == "Y";

        private bool Lot사용여부 => Global.MainFrame.LoginInfo.MngLot == "Y";

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

        private void 검증_Message(string p_검증항목, ref StringBuilder p_검증리스트)
        {
            string empty = string.Empty;
            p_검증리스트.AppendLine(p_검증항목);
            string str = "-".PadRight(80, '-');
            p_검증리스트.AppendLine(str);
        }

        private bool Chk_ExcelData(DataTable dt_Excel)
        {
            string[] strArray = new string[] { "CD_ITEM",
                                               "QT_GOOD_INV",
                                               "QT_GOOD_INV2" };
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (!dt_Excel.Columns.Contains(strArray[index]))
                {
                    this.ShowMessage("컬럼명 [@] 이 엑셀에 존재하지 않습니다.", strArray[index]);
                    return false;
                }
            }
            return true;
        }

        private DataTable Get_ExcelData_ITEM(DataTable dt_Excel)
        {
            string sPipe = string.Empty;
            DataTable table = dt_Excel.DefaultView.ToTable(true, "CD_ITEM");
            if (table.Rows.Count < 1)
                return null;
            foreach (DataRow row in table.Rows)
                sPipe = sPipe + row["CD_ITEM"] + "|";
            string[] pipes = D.StringConvert.GetPipes(sPipe, 200);
            DataTable excelDataItem = null;
            for (int index = 0; index < pipes.Length; ++index)
            {
                DataTable itemInfo = this._biz.Get_ITEMInfo(pipes[index], D.GetString(this.cbo공장.SelectedValue));
                if (itemInfo != null && itemInfo.Rows.Count > 0)
                {
                    if (excelDataItem == null)
                        excelDataItem = itemInfo.Clone();
                    excelDataItem.Merge(itemInfo);
                }
            }
            return excelDataItem;
        }

        private DataTable Get_ExcelData_PJT(DataTable dt_Excel, string column)
        {
            string sPipe = string.Empty;
            DataTable table = dt_Excel.DefaultView.ToTable(true, column);
            if (table.Rows.Count < 1)
                return null;
            foreach (DataRow row in table.Rows)
                sPipe = sPipe + row[column] + "|";
            string[] pipes = D.StringConvert.GetPipes(sPipe, 200);
            DataTable excelDataPjt = null;
            for (int index = 0; index < pipes.Length; ++index)
            {
                DataTable pjtInfo = this._biz.Get_PJTInfo(D.GetString(pipes[index]));
                if (pjtInfo != null && pjtInfo.Rows.Count > 0)
                {
                    if (excelDataPjt == null)
                        excelDataPjt = pjtInfo.Clone();
                    excelDataPjt.Merge(pjtInfo);
                }
            }
            return excelDataPjt;
        }

        private void 엑셀_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder p_검증리스트1 = new StringBuilder();
                StringBuilder p_검증리스트2 = new StringBuilder();
                bool flag1 = false;
                bool flag2 = false;
                DataTable dataTable1 = null;
                DataTable dataTable2 = new DataTable();
                this.m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";
                if (this.m_FileDlg.ShowDialog() == DialogResult.OK)
                {
                    DataTable dt_Excel = new Excel().StartLoadExcel(this.m_FileDlg.FileName);
                    if (dt_Excel.Rows.Count < 1 || !this.Chk_ExcelData(dt_Excel))
                        return;
                    DataTable excelDataItem = this.Get_ExcelData_ITEM(dt_Excel);
                    if (excelDataItem == null)
                        return;
                    if (dt_Excel.Columns.Contains("CD_PJT"))
                        dataTable1 = this.Get_ExcelData_PJT(dt_Excel, "CD_PJT");
                    this._flex.Redraw = false;
                    this.검증_Message("마스터품목코드", ref p_검증리스트1);
                    this.검증_Message(".\n\n프로젝트", ref p_검증리스트2);
                    foreach (DataRow row in dt_Excel.Rows)
                    {
                        if (!(D.GetString(row["CD_ITEM"]) == string.Empty))
                        {
                            this._flex.Rows.Add();
                            this._flex.Row = this._flex.Rows.Count - 1;
                            this._flex["CD_COMPANY"] = this.LoginInfo.CompanyCode;
                            this._flex["NO_SV"] = this.txt실사번호.Text;
                            DataRow[] dataRowArray1 = excelDataItem.Select("CD_ITEM = '" + D.GetString(row["CD_ITEM"]) + "'");
                            if (dataRowArray1.Length > 0)
                            {
                                this._flex["CD_ITEM"] = D.GetString(row["CD_ITEM"]).ToUpper();
                                this._flex["NM_ITEM"] = dataRowArray1[0]["NM_ITEM"];
                                this._flex["STND_ITEM"] = dataRowArray1[0]["STND_ITEM"];
                                this._flex["UNIT_IM"] = dataRowArray1[0]["UNIT_IM"];
                                this._flex["CLS_ITEM"] = dataRowArray1[0]["CLS_ITEM"];
                                this._flex["NM_CLS_ITEM"] = dataRowArray1[0]["NM_CLS_ITEM"];
                                this._flex["GRP_ITEM"] = dataRowArray1[0]["GRP_ITEM"];
                                this._flex["NM_ITEMGRP"] = dataRowArray1[0]["GRP_ITEMNM"];
                                this._flex["GRP_MFG"] = dataRowArray1[0]["GRP_MFG"];
                                this._flex["NM_GRP_MFG"] = dataRowArray1[0]["NM_GRPMFG"];
                                this._flex["STND_DETAIL_ITEM"] = dataRowArray1[0]["STND_DETAIL_ITEM"];
                                this._flex["MAT_ITEM"] = dataRowArray1[0]["MAT_ITEM"];
                                this._flex["NM_CLS_L"] = dataRowArray1[0]["NM_CLS_L"];
                                this._flex["NM_CLS_M"] = dataRowArray1[0]["NM_CLS_M"];
                                this._flex["NM_CLS_S"] = dataRowArray1[0]["NM_CLS_S"];
                                this._flex["LN_PARTNER"] = dataRowArray1[0]["LN_PARTNER"];
                            }
                            else
                            {
                                p_검증리스트1.AppendLine(D.GetString(row["CD_ITEM"]).PadRight(10, ' '));
                                flag1 = true;
                            }
                            if (dt_Excel.Columns.Contains("CD_PJT"))
                            {
                                if (dataTable1 != null && dataTable1.Rows.Count > 0)
                                {
                                    DataRow[] dataRowArray3 = dataTable1.Select("CD_PJT = '" + D.GetString(row["CD_PJT"]) + "'");
                                    if (dataRowArray3 != null && dataRowArray3.Length > 0)
                                    {
                                        this._flex["CD_PJT"] = D.GetString(row["CD_PJT"]);
                                        this._flex["NM_PROJECT"] = dataRowArray3[0]["NM_PROJECT"];
                                        this._flex["SEQ_PROJECT"] = dataRowArray3[0]["SEQ_PROJECT"];
                                    }
                                    else if (D.GetString(row["CD_PJT"]) != string.Empty && (dataTable1 == null || dataTable1.Rows.Count < 1 || dataRowArray3 == null || dataRowArray3.Length < 1))
                                    {
                                        p_검증리스트2.AppendLine(D.GetString(row["CD_PJT"]).PadRight(15, ' '));
                                        flag2 = true;
                                    }
                                }
                            }
                            else
                            {
                                this._flex["CD_PJT"] = "";
                                this._flex["NM_PROJECT"] = "";
                                this._flex["SEQ_PROJECT"] = 0;
                            }
                            this._flex["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_GOOD_INV"]));
                            this._flex["QT_GOOD_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(row["QT_GOOD_INV2"]));
                            this._flex["QT_CHA"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]) - D.GetDecimal(this._flex["QT_GOOD_INV2"]));
                            this._flex["QT_REJECT_INV"] = 0;
                            this._flex["QT_REJECT_INV2"] = 0;
                            this._flex["QT_TRANS_INV"] = 0;
                            this._flex["QT_TRANS_INV2"] = 0;
                            this._flex["QT_INSP_INV"] = 0;
                            this._flex["QT_INSP_INV2"] = 0;
                            this._flex["YN_BLOCK"] = "N";
                            this._flex.AddFinished();
                            this._flex.Col = this._flex.Cols.Fixed;
                        }
                    }
                    if (flag1 || flag2)
                    {
                        this.ShowDetailMessage("엑셀 업로드하는 공장마스터품목,프로젝트 중 불일치하는 항목들이 있습니다.▼ 버튼을 눌러서 목록을 확인하세요!", (flag1 ? D.GetString(p_검증리스트1) : "") + (flag2 ? D.GetString(p_검증리스트2) : ""));
                    }
                    this._flex.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_추가Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.HasNormalRow && D.GetString(this._flex[this._flex.Row, "NO_BALANCE"]) != "" || !this.CheckFieldHead())
                    return;
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex[this._flex.Row, "CD_COMPANY"] = this.LoginInfo.CompanyCode;
                this._flex[this._flex.Row, "NO_SV"] = this.txt실사번호.Text;
                this._flex[this._flex.Row, "YN_BLOCK"] = "N";
                this._flex[this._flex.Row, "CD_PJT"] = this.ctx프로젝트.CodeValue;
                this._flex[this._flex.Row, "NM_PROJECT"] = this.ctx프로젝트.CodeName;
                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_삭제Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRowArray1 = this._flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray1 == null || dataRowArray1.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flex.DataTable.Select("S = 'Y' AND ISNULL(NO_BALANCE,'') <> '' ", "", DataViewRowState.CurrentRows);
                    if (dataRowArray2 != null && dataRowArray2.Length > 0)
                    {
                        this.ShowMessage(this.DD("재고조정관리에 적용된 실사건입니다.") + "( " + D.GetString(dataRowArray2[0]["NO_BALANCE"]) + " )");
                    }
                    else
                    {
                        this._flex.Redraw = false;
                        foreach (DataRow dataRow in dataRowArray1)
                            dataRow.Delete();
                        this._flex.Redraw = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this._flex.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.StartEditCancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "CD_ITEM":
                            if (D.GetString(this._header.CurrentRow["CD_PLANT"]) == string.Empty || this._flex.RowState() != DataRowState.Added)
                            {
                                e.Cancel = true;
                                break;
                            }
                            e.Parameter.P01_CD_COMPANY = this.LoginInfo.CompanyCode;
                            e.Parameter.P09_CD_PLANT = D.GetString(this._header.CurrentRow["CD_PLANT"]);
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
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (e.Result.DialogResult == DialogResult.Cancel)
                            break;
                        bool flag = true;
                        foreach (DataRow row in result.Rows)
                        {
                            if (!flag)
                            {
                                this._flex.Rows.Add();
                                this._flex.Row = this._flex.Rows.Count - 1;
                            }
                            this._flex[this._flex.Row, "CD_COMPANY"] = this.LoginInfo.CompanyCode;
                            this._flex[this._flex.Row, "NO_SV"] = this.txt실사번호.Text;
                            this._flex[this._flex.Row, "YN_BLOCK"] = "N";
                            this._flex[this._flex.Row, "CD_PJT"] = this.ctx프로젝트.CodeValue;
                            this._flex[this._flex.Row, "NM_PROJECT"] = this.ctx프로젝트.CodeName;
                            this._flex[this._flex.Row, "CD_ITEM"] = row["CD_ITEM"];
                            this._flex[this._flex.Row, "NM_ITEM"] = row["NM_ITEM"];
                            this._flex[this._flex.Row, "STND_ITEM"] = row["STND_ITEM"];
                            this._flex[this._flex.Row, "UNIT_IM"] = row["UNIT_IM"];
                            this._flex[this._flex.Row, "CLS_ITEM"] = row["CLS_ITEM"];
                            this._flex[this._flex.Row, "NM_CLS_ITEM"] = row["CLS_ITEMNM"];
                            DataTable qtinv = this._biz.Get_QTINV(new object[] { D.GetString( this.ctx창고.CodeValue),
                                                                                 D.GetString(this.cbo공장.SelectedValue),
                                                                                 Global.MainFrame.LoginInfo.CompanyCode,
                                                                                 this.dtp실사일자.Text,
                                                                                 D.GetString( this.ctx프로젝트.CodeValue),
                                                                                 D.GetString(row["CD_ITEM"]),
                                                                                 Global.SystemLanguage.MultiLanguageLpoint });
                            if (qtinv != null && qtinv.Rows.Count != 0)
                            {
                                this._flex[this._flex.Row, "QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(qtinv.Rows[0]["QT_GOOD_INV"]));
                                this._flex[this._flex.Row, "QT_GOOD_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(qtinv.Rows[0]["QT_GOOD_INV2"]));
                                this._flex[this._flex.Row, "QT_CHA"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV"]) - D.GetDecimal(this._flex[this._flex.Row, "QT_GOOD_INV2"]));
                                this._flex[this._flex.Row, "QT_REJECT_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(qtinv.Rows[0]["QT_REJECT_INV"]));
                                this._flex[this._flex.Row, "QT_REJECT_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(qtinv.Rows[0]["QT_REJECT_INV2"]));
                                this._flex[this._flex.Row, "QT_TRANS_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(qtinv.Rows[0]["QT_TRANS_INV"]));
                                this._flex[this._flex.Row, "QT_TRANS_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(qtinv.Rows[0]["QT_TRANS_INV2"]));
                                this._flex[this._flex.Row, "QT_INSP_INV"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(qtinv.Rows[0]["QT_INSP_INV"]));
                                this._flex[this._flex.Row, "QT_INSP_INV2"] = Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(qtinv.Rows[0]["QT_INSP_INV2"]));
                            }
                            else
                            {
                                this._flex[this._flex.Row, "QT_GOOD_INV"] = 0;
                                this._flex[this._flex.Row, "QT_GOOD_INV2"] = 0;
                                this._flex[this._flex.Row, "QT_CHA"] = 0;
                                this._flex[this._flex.Row, "QT_REJECT_INV"] = 0;
                                this._flex[this._flex.Row, "QT_REJECT_INV2"] = 0;
                                this._flex[this._flex.Row, "QT_TRANS_INV"] = 0;
                                this._flex[this._flex.Row, "QT_TRANS_INV2"] = 0;
                                this._flex[this._flex.Row, "QT_INSP_INV"] = 0;
                                this._flex[this._flex.Row, "QT_INSP_INV2"] = 0;
                            }
                            this._flex.AddFinished();
                            flag = false;
                        }
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
                string str = D.GetString(this._flex.GetData(e.Row, e.Col));
                string editData = this._flex.EditData;
                if (str.ToUpper() == editData.ToUpper())
                    return;
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "QT_GOOD_INV":
                        this._flex[e.Row, "QT_CHA"] = (Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[e.Row, "QT_GOOD_INV"])) - Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[e.Row, "QT_GOOD_INV2"])));
                        break;
                    case "QT_GOOD_INV2":
                        this._flex[e.Row, "QT_CHA"] = (Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[e.Row, "QT_GOOD_INV"])) - Unit.수량(DataDictionaryTypes.PU, D.GetDecimal(this._flex[e.Row, "QT_GOOD_INV2"])));
                        break;
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
                if (!this._flex.HasNormalRow || this.ctx프로젝트.CodeName == "")
                    return;
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        dataRow["CD_PJT"] = this.ctx프로젝트.CodeValue;
                        dataRow["NM_PROJECT"] = this.ctx프로젝트.CodeName;
                    }
                    this.ShowMessage("적용작업을완료하였습니다");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn업체전용_Click(object sender, EventArgs e)
        {
            try
            {
                if (((Control)sender).Text == "MES재고적용")
                {
                    if (this.dtp실사일자.Text == "")
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, "실사일자");
                    }
                    else if (this._cddept == "")
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, "부서");
                    }
                    else if (this.ctx담당자.CodeValue == "")
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, "담당자");
                    }
                    else if (this._biz.SaveMES((object[])new string[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                        this.dtp실사일자.Text,
                                                                        this._cddept,
                                                                        this.ctx담당자.CodeValue,
                                                                        this.txt비고.Text,
                                                                        Global.MainFrame.LoginInfo.UserID }))
                    {
                        this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool Z_Print()
        {
            if (!(Global.MainFrame.ServerKey == "WJIS") && !(Global.MainFrame.ServerKey == "DZSQL"))
                return false;
            DataRow[] dataRowArray = this._flex.DataTable.Select("S='Y'");
            if (dataRowArray == null || dataRowArray.Length == 0)
            {
                this.ShowMessage(공통메세지.선택된자료가없습니다);
                return true;
            }
            ReportHelper reportHelper = new ReportHelper("R_PU_PI_REG_0", "재고실사 현품표");
            DataTable dt = this._flex.DataTable.Clone();
            foreach (DataRow dataRow in dataRowArray)
            {
                int num3 = D.GetInt(dataRow["QT_GOOD_INV2"]) / D.GetInt(dataRow["LOTSIZE"]);
                int num4 = D.GetInt(dataRow["QT_GOOD_INV2"]) % D.GetInt(dataRow["LOTSIZE"]);
                for (int index = 0; index <= num3; ++index)
                {
                    DataRow row = dt.NewRow();
                    if (index != num3 || num4 != 0)
                    {
                        foreach (DataColumn column in dt.Columns)
                            row[column.ColumnName] = dataRow[column.ColumnName];
                        row["QT_GOOD_INV2"] = D.GetInt(dataRow["LOTSIZE"]);
                        if (index == num3)
                            row["QT_GOOD_INV2"] = num4;
                        dt.Rows.Add(row);
                    }
                    else
                        break;
                }
            }
            reportHelper.SetDataTable(dt);
            reportHelper.Print();
            return true;
        }
    }
}

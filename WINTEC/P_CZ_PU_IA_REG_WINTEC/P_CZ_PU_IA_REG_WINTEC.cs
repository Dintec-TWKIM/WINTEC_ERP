using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU.MF.Common;
using DzHelpFormLib;
using pur;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PU_IA_REG_WINTEC : PageBase
	{
        private P_CZ_PU_IA_REG_WINTEC_BIZ _biz = new P_CZ_PU_IA_REG_WINTEC_BIZ();
        private FreeBinding _header = null;
        private string _page_state = "Added";
        private string _cddept = "";
        public P_CZ_PU_IA_REG_WINTEC()
		{
            try
            {
                this.InitializeComponent();
                
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

            this._flex.BeginSetting(2, 1, true);
            this._flex.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_ITEM", "품목", 120);
            this._flex.SetCol("NM_ITEM", "품목명", 150);
            this._flex.SetCol("STND_ITEM", "규격", 80);
            this._flex.SetCol("UNIT_IM", "재고단위", 60);
            this._flex.SetCol("QT_GOOD_INV", "장부수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_GOOD_INV2", "실사수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("QT_GOOD", "조정수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("NO_GIREQ", "요청번호", 120, false);
            this._flex.SetCol("NO_SV", "실사번호", 120, false);
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
            this._flex.SetDummyColumn("S");
            this._flex.SetDummyColumn("NO_GIREQ");
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flex[0, "QT_GOOD_INV"] = this.DD("양품재고");
            this._flex[0, "QT_GOOD_INV2"] = this.DD("양품재고");
            this._flex[0, "QT_GOOD"] = this.DD("양품재고");
            this._flex[1, "QT_GOOD_INV"] = this.DD("장부수량");
            this._flex[1, "QT_GOOD_INV2"] = this.DD("실사수량");
            this._flex[1, "QT_GOOD"] = this.DD("조정수량");
            this._flex.SetHeaderMerge();
        }

        private void InitEvent()
        {
            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);

            this.ctx담당자.QueryAfter += new BpQueryHandler(this.OnBpCodeTextBox_QueryAfter);
            this.cboFGUM.SelectionChangeCommitted += new EventHandler(this.cbo_FG_UM_SelectionChangeCommitted);
            this.btn_DT_DY.Click += new EventHandler(this.btn_DT_DY_Click);
            this.btn반영취소.Click += new EventHandler(this.btn_APL_CANCEL_Click);
            this.btn실사적용.Click += new EventHandler(this.btn_APL_PI_Click);
            this.btn요청반영.Click += new EventHandler(this.btn_APL_IO_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            DataSet dataSet = this._biz.Search("");
            this._header.SetBinding(dataSet.Tables[0], this.oneGrid1);
            this._header.ClearAndNewRow();
            DataTable dataTable = this.GetComboData("N;PU_C000001").Tables[0].Clone();
            DataRow row1 = dataTable.NewRow();
            row1["CODE"] = "";
            row1["NAME"] = "";
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["CODE"] = "1";
            row2["NAME"] = this.DD("재고단가");
            dataTable.Rows.Add(row2);
            DataRow row3 = dataTable.NewRow();
            row3["CODE"] = "2";
            row3["NAME"] = this.DD("최근매입단가");
            dataTable.Rows.Add(row3);
            this.cboFGUM.DataSource = dataTable;
            this.cboFGUM.DisplayMember = "NAME";
            this.cboFGUM.ValueMember = "CODE";
            this._flex.Binding = dataSet.Tables[1];
            this.InitializeControlDataValue();
            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.bpPanelControl2.IsNecessaryCondition = true;
            this.bpPanelControl3.IsNecessaryCondition = true;
            this.bpPanelControl6.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                P_PU_IA_SUB pPuIaSub = new P_PU_IA_SUB(this.MainFrameInterface);
                if (pPuIaSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    this.ToolBarSaveButtonEnabled = true;
                    this.ToolBarAddButtonEnabled = true;
                    this.ToolBarDeleteButtonEnabled = true;
                    this.RealSearch(pPuIaSub.m_SelecedRow["NO_BALANCE"].ToString());
                    this.btn반영취소.Enabled = true;
                    this.btn요청반영.Enabled = true;
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
                    if (this._page_state == "Modified" && this.txt조정번호.Text != "")
                    {
                        if (this._biz.Delete(new object[] { this.txt조정번호.Text,
                                                            this.MainFrameInterface.LoginInfo.CompanyCode }).Result)
                        {
                            this.ShowMessage("IK1_002");
                        }
                        else
                        {
                            this.ShowMessage("EK1_002");
                            return;
                        }
                    }
                    this.OnToolBarAddButtonClicked(null, null);
                    this.ToolBarSearchButtonEnabled = true;
                    this.ToolBarSaveButtonEnabled = false;
                    this.ToolBarDeleteButtonEnabled = false;
                    this.btn실사적용.Enabled = true;
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
                this.ToolBarAddButtonEnabled = true;
                this.ToolBarDeleteButtonEnabled = true;
                this.ToolBarSaveButtonEnabled = false;
                this.ToolBarPrintButtonEnabled = true;
                this.ToolBarSearchButtonEnabled = true;
                this.btn실사적용.Enabled = false;
                this.btn요청반영.Enabled = true;
                this.btn반영취소.Enabled = true;
                this.RealSearch(this.txt조정번호.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            try
            {
                if (!base.SaveData() || !this.BeforeSave() || !this.CheckFieldHead())
                    return false;
                if (this._page_state == "Added")
                {
                    string seq = (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "22", this.dtp조정일자.Text.Substring(0, 6));
                    this._header.CurrentRow["NO_BALANCE"] = seq;
                    this._header.CurrentRow["DT_SV"] = this.dtp조정일자.Text;
                    this._header.CurrentRow["CD_DEPT"] = this._cddept;
                    foreach (DataRow row in (InternalDataCollectionBase)this._flex.DataTable.Rows)
                        row["NO_BALANCE"] = seq;
                    this.txt조정번호.Text = seq;
                }
                DataTable changes1 = this._header.GetChanges();
                DataTable changes2 = this._flex.GetChanges();
                if (changes1 == null && changes2 == null)
                    return true;
                string empty = string.Empty;
                string p_FG_UM = this.cboFGUM.SelectedValue.ToString();
                if (p_FG_UM == "1")
                {
                    p_FG_UM = this.tbDTTO.Text.Replace("/", "").Replace("_", "");
                    if (p_FG_UM == string.Empty)
                    {
                        this.ShowMessage("입고 단가적용이 재고단가일 경우 평가월을 선택해야 합니다. ");
                        this.tbDTFROM.Focus();
                        return false;
                    }
                }
                ResultData[] resultDataArray = this._biz.Save(changes1, changes2, p_FG_UM);
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

        private void RealSearch(string p_no_balance)
        {
            DataSet dataSet = this._biz.Search(p_no_balance);
            this._header.SetDataTable(dataSet.Tables[0]);
            this._flex.Binding = dataSet.Tables[1];
            this._page_state = "Modified";
        }

        private void btn_APL_PI_Click(object sender, EventArgs e)
        {
            try
            {
                P_PU_PIA_SUB pPuPiaSub = new P_PU_PIA_SUB(this.MainFrameInterface);
                if (pPuPiaSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    this._cddept = pPuPiaSub.m_SelecedRow["CD_DEPT"].ToString();
                    this.txt공장.Text = pPuPiaSub.m_SelecedRow["NM_PLANT"].ToString();
                    this.txt공장.Tag = pPuPiaSub.m_SelecedRow["CD_PLANT"].ToString();
                    this.txt창고.Text = pPuPiaSub.m_SelecedRow["NM_SL"].ToString();
                    this.txt창고.Tag = pPuPiaSub.m_SelecedRow["CD_SL"].ToString();
                    this.ctx담당자.CodeName = pPuPiaSub.m_SelecedRow["NM_KOR"].ToString();
                    this.ctx담당자.CodeValue = pPuPiaSub.m_SelecedRow["NO_EMP"].ToString();
                    this._header.CurrentRow["CD_DEPT"] = this._cddept;
                    this._header.CurrentRow["CD_PLANT"] = pPuPiaSub.m_SelecedRow["CD_PLANT"].ToString();
                    this._header.CurrentRow["CD_SL"] = pPuPiaSub.m_SelecedRow["CD_SL"].ToString();
                    this._header.CurrentRow["NO_EMP"] = pPuPiaSub.m_SelecedRow["NO_EMP"].ToString();
                    this.ToolBarSearchButtonEnabled = false;
                    this.ToolBarSaveButtonEnabled = true;
                    this.ToolBarAddButtonEnabled = true;
                    this.ToolBarDeleteButtonEnabled = true;
                    this.btn반영취소.Enabled = false;
                    this.btn요청반영.Enabled = false;
                    this._page_state = "Added";
                    DataTable dataTable = this._biz.실사적용(new object[] { this.MainFrameInterface.LoginInfo.CompanyCode,
                                                                           pPuPiaSub.m_SelecedRow["NO_SV"].ToString(),
                                                                           Global.SystemLanguage.MultiLanguageLpoint });
                    if (dataTable == null || dataTable.Rows.Count <= 0)
                        return;
                    this._flex.Binding = dataTable;
                    this._flex.Redraw = false;
                    for (int index = 0; index < this._flex.DataTable.Rows.Count; ++index)
                        this._flex.DataTable.Rows[index].SetAdded();
                    this._flex.Redraw = true;
                    this._flex.IsDataChanged = true;
                    this.ToolBarSaveButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InDataHeadValue(string ps_fg_gireq, string ps_fg_gi, string fg_ps)
        {
            this.ds_Ty1.Tables[2].Clear();
            DataRow row1 = this.ds_Ty1.Tables[2].NewRow();
            row1["NO_IO"] = "";
            this.ds_Ty1.Tables[2].Rows.Add(row1);
            if (this._page_state == "Modified")
                this.ds_Ty1.Tables[2].AcceptChanges();
            this.ds_Ty1.Tables[2].BeginInit();
            DataRow row2 = this.ds_Ty1.Tables[2].Rows[0];
            row2["DT_IO"] = this.dtp조정일자.MaskEditBox.ClipText;
            row2["NO_EMP"] = this.ctx담당자.CodeValue.ToString();
            row2["CD_PARTNER"] = "";
            row2["DC_RMK"] = this.txt비고.Text;
            row2["CD_PLANT"] = this._header.CurrentRow["CD_PLANT"].ToString();
            row2["CD_SL"] = this._header.CurrentRow["CD_SL"].ToString();
            row2["CD_DEPT"] = this._cddept;
            row2["CD_QTIOTP"] = "555";
            row2["CD_PJT"] = "";
            row2["YN_RETURN"] = "N";
            row2["FG_TRANS"] = "001";
            row2["GI_PARTNER"] = "";
            row2["YN_AM"] = "Y";
            row2["FG_PS"] = fg_ps;
            row2["NO_BALANCE"] = this.txt조정번호.Text;
            row2["FG_GIREQ"] = ps_fg_gireq;
            row2["FG_GI"] = ps_fg_gi;
            row2["FG_MODULE"] = fg_ps == "1" ? "PU_GR" : "PU";
            this.ds_Ty1.Tables[2].EndInit();
        }

        private void btn_APL_IO_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable ldt_return1 = this._flex.DataTable.Clone();
                DataTable ldt_return2 = this._flex.DataTable.Clone();
                DataRow[] ldr_Rows = this._flex.DataTable.Select("S ='Y'");
                if (ldr_Rows == null || ldr_Rows.Length <= 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    for (int index = 0; index < ldr_Rows.Length; ++index)
                    {
                        if (ldr_Rows[index]["NO_GIREQ"].ToString().Trim() != "")
                        {
                            this.ShowMessage("PU_M000084");
                            return;
                        }
                        if (this._flex.CDecimal(ldr_Rows[index]["QT_GOOD"].ToString()) < 0M)
                            ldt_return1.ImportRow(ldr_Rows[index]);
                        if (this._flex.CDecimal(ldr_Rows[index]["QT_GOOD"].ToString()) > 0M)
                            ldt_return2.ImportRow(ldr_Rows[index]);
                    }
                    if (ldt_return1 != null && ldt_return1.Rows.Count > 0)
                        this.set_Gireq_Save_data(ldt_return1, "2", ldr_Rows);
                    if (ldt_return2 != null && ldt_return2.Rows.Count > 0)
                    {
                        if (BASIC.GetMAEXC_Menu("P_PU_IA_REG", "PU_A00000051") == "100")
                        {
                            this.set_Gireq_Save_data(ldt_return2, "1", ldr_Rows);
                        }
                        else
                        {
                            object[] args = new object[] { this._header.CurrentRow["CD_PLANT"].ToString(),
                                                           this._header.CurrentRow["NO_EMP"].ToString(),
                                                           this._header.CurrentRow["NM_KOR"].ToString(),
                                                           this._header.CurrentRow["CD_SL"].ToString(),
                                                           this._header.CurrentRow["NM_SL"].ToString(),
                                                           this._header.CurrentRow["DT_SV"].ToString(),
                                                           this._flex.DataTable.Select("S ='Y' AND QT_GOOD > 0") };
                            if (Global.MainFrame.IsExistPage("P_CZ_PU_ITR_REG_WINTEC", false))
                                this.UnLoadPage("P_CZ_PU_ITR_REG_WINTEC", false);
                            this.LoadPageFrom("P_CZ_PU_ITR_REG_WINTEC", "계정대체입고", this.Grant, args);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_APL_CANCEL_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.DataView.Count <= 0)
                    return;
                DataTable dataTable = this._flex.DataTable.Clone();
                DataRow[] dataRowArray1 = this._flex.DataTable.Select("S ='Y'");
                if (dataRowArray1 == null || dataRowArray1.Length <= 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    for (int index = 0; index < dataRowArray1.Length; ++index)
                    {
                        if (dataRowArray1[index]["NO_GIREQ"].ToString().Trim() == "")
                        {
                            this.ShowMessage("요청적용 되지 않은 것이 선택되었습니다.");
                            return;
                        }
                        dataTable.ImportRow(dataRowArray1[index]);
                    }
                    DataTable dt = dataTable.Copy();
                    for (int index1 = 0; index1 < dt.Rows.Count; ++index1)
                    {
                        DataRow[] dataRowArray2 = dt.Select(" NO_GIREQ ='" + dt.Rows[index1]["NO_GIREQ"].ToString() + "'");
                        if (dataRowArray2 != null && dataRowArray2.Length > 1)
                        {
                            for (int index2 = 1; index2 < dataRowArray2.Length; ++index2)
                                dataRowArray2[index2].Delete();
                            dt.AcceptChanges();
                        }
                    }
                    if (this.ShowMessage("PU_M000031", "QY3") == DialogResult.Yes)
                    {
                        if (this._biz.요청적용취소(dt, this.txt조정번호.Text).Result)
                        {
                            for (int index3 = 0; index3 < dt.Rows.Count; ++index3)
                            {
                                DataRow[] dataRowArray3 = this._flex.DataTable.Select("NO_GIREQ ='" + dt.Rows[index3]["NO_GIREQ"].ToString().Trim() + "'");
                                if (dataRowArray3 != null && dataRowArray3.Length > 0)
                                {
                                    for (int index4 = 0; index4 < dataRowArray3.Length; ++index4)
                                    {
                                        dataRowArray3[index4].BeginEdit();
                                        dataRowArray3[index4]["NO_GIREQ"] = "";
                                        dataRowArray3[index4]["NO_GIREQLINE"] = 0;
                                        dataRowArray3[index4].EndEdit();
                                    }
                                }
                            }
                            this._flex.DataTable.AcceptChanges();
                            this.ShowMessage("반영취소가 성공적으로 수행 되었습니다.");
                            this.btn반영취소.Enabled = true;
                            this.btn요청반영.Enabled = true;
                        }
                        else
                        {
                            this.ShowMessage("반영취소작업이 실패 했습니다. ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_DT_DY_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._header.CurrentRow["CD_PLANT"].ToString() == string.Empty)
                {
                    this.ShowMessage("조회후 선택 하세요");
                }
                else
                {
                    P_PU_EVAL_SUB pPuEvalSub = new P_PU_EVAL_SUB(this.MainFrameInterface, this._header.CurrentRow["CD_PLANT"].ToString());
                    if (pPuEvalSub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                    {
                        this.tbDTFROM.Text = pPuEvalSub.m_SelecedRow["YM_FSTANDARD"].ToString();
                        this.tbDTTO.Text = pPuEvalSub.m_SelecedRow["YM_STANDARD"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void set_Gireq_Save_data(DataTable ldt_return, string fg_ps, DataRow[] ldr_Rows)
        {
            try
            {
                object[] objArray = new object[2];
                if (fg_ps == "1")
                {
                    objArray[0] = "1";
                    objArray[1] = "입고요청등록 수불/대체 도움창";
                }
                P_PU_IA_REQSUB pPuIaReqsub = new P_PU_IA_REQSUB(this.MainFrameInterface, objArray);
                if (pPuIaReqsub.ShowDialog((IWin32Window)this) == DialogResult.OK)
                {
                    this.InDataHeadValue(pPuIaReqsub.gs_FG_GIREQ, pPuIaReqsub.gs_FG_GI, fg_ps);
                    string empty = string.Empty;
                    string str = !(fg_ps == "1") ? (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "08", this.dtp조정일자.Text.Substring(0, 6)) : (string)this.GetSeq(this.LoginInfo.CompanyCode, "PU", "47", this.dtp조정일자.Text.Substring(0, 6));
                    this.ds_Ty1.Tables[2].Rows[0].BeginEdit();
                    this.ds_Ty1.Tables[2].Rows[0]["NO_GIREQ"] = str;
                    this.ds_Ty1.Tables[2].Rows[0].EndEdit();
                    for (int index = 0; index < ldt_return.Rows.Count; ++index)
                    {
                        ldt_return.Rows[index].BeginEdit();
                        ldt_return.Rows[index]["NO_GIREQ"] = str;
                        ldt_return.Rows[index]["NO_GIREQLINE"] = (index + 1);
                        ldt_return.Rows[index].EndEdit();
                    }
                    if (!this._biz.요청적용(this.ds_Ty1.Tables[2], ldt_return, this.txt조정번호.Text, this.ctx담당자.CodeValue, !(this._biz.strGetAPPRO(pPuIaReqsub.gs_FG_GIREQ) == "N") ? "R" : "O"))
                    {
                        this.ShowMessage("요청적용에 실패 했습니다.");
                    }
                    else
                    {
                        for (int index = 0; index < ldr_Rows.Length; ++index)
                        {
                            if (this._flex.CDecimal(ldr_Rows[index]["QT_GOOD"].ToString()) < 0M && fg_ps == "2")
                            {
                                ldr_Rows[index].BeginEdit();
                                ldr_Rows[index]["NO_GIREQ"] = str;
                                ldr_Rows[index]["NO_GIREQLINE"] = (index + 1);
                                ldr_Rows[index].EndEdit();
                                ldr_Rows[index].AcceptChanges();
                            }
                            else if (this._flex.CDecimal(ldr_Rows[index]["QT_GOOD"].ToString()) > 0M && fg_ps == "1")
                            {
                                ldr_Rows[index].BeginEdit();
                                ldr_Rows[index]["NO_GIREQ"] = str;
                                ldr_Rows[index]["NO_GIREQLINE"] = (index + 1);
                                ldr_Rows[index].EndEdit();
                                ldr_Rows[index].AcceptChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool CheckFieldHead()
        {
            try
            {
                if (this.dtp조정일자.MaskEditBox.Text == "")
                {
                    this.dtp조정일자.Focus();
                    this.ShowMessage("WK1_004", this.lbl조정일자.Text);
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

        private void InitializeControlDataValue()
        {
            try
            {
                this.txt조정번호.Text = "";
                this.dtp조정일자.Text = this.MainFrameInterface.GetStringToday;
                this.txt창고.Text = "";
                this.txt공장.Text = "";
                this._cddept = this.LoginInfo.DeptCode;
                this.ctx담당자.CodeName = this.LoginInfo.EmployeeName;
                this.ctx담당자.CodeValue = this.LoginInfo.EmployeeNo;
                this.txt비고.Text = "";
                this.btn실사적용.Enabled = true;
                this.btn요청반영.Enabled = false;
                this.btn반영취소.Enabled = false;
                this.btn_DT_DY.Enabled = false;
                this.tbDTFROM.Text = "";
                this.tbDTTO.Text = "";
                this._page_state = "Added";
                this._flex.DataTable.Clear();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpCodeTextBox_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult != DialogResult.OK)
                    return;
                DataRow[] rows = e.HelpReturn.Rows;
                if (e.HelpID == Duzon.Common.Forms.Help.HelpID.P_MA_EMP_SUB)
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
                FlexGrid flexGrid = (FlexGrid)sender;
                if (flexGrid.Cols[e.Col].Name == "S")
                    return;
                if (!this.추가모드여부)
                    e.Cancel = true;
                if (flexGrid[e.Row, "NO_GIREQ"].ToString() != "" && flexGrid[e.Row, "NO_GIREQ"].ToString() != null)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 추가모드여부 => this._header.JobMode == JobModeEnum.추가후수정;

        private void cbo_FG_UM_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (this.ActiveControl.Name)
                {
                    case "cbo_FG_UM":
                        if (this.cboFGUM.SelectedValue.ToString() == "1")
                            this.btn_DT_DY.Enabled = true;
                        else
                            this.btn_DT_DY.Enabled = false;
                        this.ToolBarAddButtonEnabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool 프로젝트사용 => Config.MA_ENV.프로젝트사용;

        private bool PJT형여부 => Config.MA_ENV.PJT형여부 == "Y";
    }
}

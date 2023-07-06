using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Dintec;

namespace cz
{
    public partial class P_CZ_PR_MATCHING_REG : PageBase
    {
        P_CZ_PR_MATCHING_REG_BIZ _biz = new P_CZ_PR_MATCHING_REG_BIZ();
        int row좌측선택, row중간선택, row우측선택;

        public P_CZ_PR_MATCHING_REG()
        {
			if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
				StartUp.Certify(this);

			InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = this.GetComboData(new string[] { "NC;MA_PLANT" });

            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.DisplayMember = "NAME";

            this.cbo현합상태.DataSource = MA.GetCode("CZ_WIN0009", true);
            this.cbo현합상태.ValueMember = "CODE";
            this.cbo현합상태.DisplayMember = "NAME";

            this.splitContainer1.SplitterDistance = 370;

            this.btn조립.Enabled = false;
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex현합 };

            #region 좌측
            this._flex좌측.BeginSetting(1, 1, false);

            this._flex좌측.SetCol("YN_USE", "사용여부", false);
            this._flex좌측.SetCol("NO_WO", "작업지시번호", false);
            this._flex좌측.SetCol("CD_ITEM", "모품목", false);
            this._flex좌측.SetCol("CD_PITEM", "자품목", false);
            this._flex좌측.SetCol("NO_ID", "일련번호", 100);
            this._flex좌측.SetCol("NUM_P1", "측정치1", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex좌측.SetCol("NUM_P2", "측정치2", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex좌측.SetCol("NUM_P3", "측정치3", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex좌측.SetCol("NUM_MIN", "최소값", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex좌측.SettingVersion = "0.0.0.1";
            this._flex좌측.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex좌측.Styles.Add("미사용").BackColor = Color.Yellow;
            this._flex좌측.Styles.Add("사용").BackColor = Color.White;

            this._flex좌측.AddMyMenu = true;
            this._flex좌측.AddMenuSeperator();
            this._flex좌측.AddMenuItem("개별판매처리_좌측", new EventHandler(this.ContextMenuItem_Click));
            #endregion

            #region 중간
            this._flex중간.BeginSetting(1, 1, false);

            this._flex중간.SetCol("YN_USE", "사용여부", false);
            this._flex중간.SetCol("NO_WO", "작업지시번호", false);
            this._flex중간.SetCol("CD_ITEM", "모품목", false);
            this._flex중간.SetCol("CD_PITEM", "자품목", false);
            this._flex중간.SetCol("NO_ID", "일련번호", 100);
            this._flex중간.SetCol("NUM_P1_OUT", "측정치1", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex중간.SetCol("NUM_P2_OUT", "측정치2", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex중간.SetCol("NUM_P3_OUT", "측정치3", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex중간.SetCol("NUM_MAX", "최대값", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex중간.SetCol("NUM_P1_IN", "측정치1", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex중간.SetCol("NUM_P2_IN", "측정치2", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex중간.SetCol("NUM_P3_IN", "측정치3", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex중간.SetCol("NUM_MIN", "최소값", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex중간.SettingVersion = "0.0.0.1";
            this._flex중간.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex중간.Styles.Add("미사용").BackColor = Color.Yellow;
            this._flex중간.Styles.Add("사용").BackColor = Color.White;

            this._flex중간.AddMyMenu = true;
            this._flex중간.AddMenuSeperator();
            this._flex중간.AddMenuItem("개별판매처리_중간", new EventHandler(this.ContextMenuItem_Click));
            #endregion

            #region 우측
            this._flex우측.BeginSetting(1, 1, false);

            this._flex우측.SetCol("YN_USE", "사용여부", false);
            this._flex우측.SetCol("NO_WO", "작업지시번호", false);
            this._flex우측.SetCol("CD_ITEM", "모품목", false);
            this._flex우측.SetCol("CD_PITEM", "자품목", false);
            this._flex우측.SetCol("NO_ID", "일련번호", 100);
            this._flex우측.SetCol("NUM_P1", "측정치1", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex우측.SetCol("NUM_P2", "측정치2", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex우측.SetCol("NUM_P3", "측정치3", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex우측.SetCol("NUM_MAX", "최대값", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex우측.SettingVersion = "0.0.0.1";
            this._flex우측.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex우측.Styles.Add("미사용").BackColor = Color.Yellow;
            this._flex우측.Styles.Add("사용").BackColor = Color.White;

            this._flex우측.AddMyMenu = true;
            this._flex우측.AddMenuSeperator();
            this._flex우측.AddMenuItem("개별판매처리_우측", new EventHandler(this.ContextMenuItem_Click));
            #endregion

            #region 완성
            this._flex현합.BeginSetting(2, 1, false);

            this._flex현합.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex현합.SetCol("STA_MATCHING", "현합상태", 100);
            this._flex현합.SetCol("CD_ITEM", "모품목", false);

            this._flex현합.SetCol("NO_WO_L", "작업지시번호", false);
            this._flex현합.SetCol("CD_PITEM_L ", "자품목", false);
            this._flex현합.SetCol("NO_ID_L", "일련번호", 100);
            this._flex현합.SetCol("NUM_P1_L", "측정치1", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_P2_L", "측정치2", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_P3_L", "측정치3", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_MIN_L", "최소값", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex현합.SetCol("NUM_C1", "클리어런스1", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex현합.SetCol("NO_WO_C", "작업지시번호", false);
            this._flex현합.SetCol("CD_PITEM_C ", "자품목", false);
            this._flex현합.SetCol("NO_ID_C", "일련번호", 100);
            this._flex현합.SetCol("NUM_P1_OUT_C", "측정치1", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_P2_OUT_C", "측정치2", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_P3_OUT_C", "측정치3", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_MAX_C", "최대값", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex현합.SetCol("NUM_P1_IN_C", "측정치1", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_P2_IN_C", "측정치2", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_P3_IN_C", "측정치3", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_MIN_C", "최소값", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex현합.SetCol("NUM_C2", "클리어런스2", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex현합.SetCol("NO_WO_R", "작업지시번호", false);
            this._flex현합.SetCol("CD_PITEM_R ", "자품목", false);
            this._flex현합.SetCol("NO_ID_R", "일련번호", 100);
            this._flex현합.SetCol("NUM_P1_R", "측정치1", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_P2_R", "측정치2", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_P3_R", "측정치3", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_MAX_R", "최대값", 100, false, typeof(decimal), FormatTpType.RATE);

            this._flex현합.SetCol("NUM_MATCHING_SIZE1", "현합치수1", 100, true, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NUM_MATCHING_SIZE2", "현합치수2", 100, true, typeof(decimal), FormatTpType.RATE);
            this._flex현합.SetCol("NO_SO", "수주번호", 100, true);
            this._flex현합.SetCol("DC_RMK", "비고", 100, true);

            this.품목명설정("좌측품목", "중간품목", "우측품목");

            this._flex현합.SetDataMap("STA_MATCHING", MA.GetCode("CZ_WIN0009", false), "CODE", "NAME");
            this._flex현합.AddDummyColumn("S");

            this._flex현합.ExtendLastCol = true;

            this._flex현합.SettingVersion = "0.0.0.1";
            this._flex현합.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex현합.Styles.Add("기존").BackColor = Color.White;
            this._flex현합.Styles.Add("신규").BackColor = Color.Yellow;
            #endregion
        }

        private void InitEvent()
        {
            this.ctx현합품목.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx현합품목.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx좌측품목.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx중간품목.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx우측품목.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this.btn현합품목등록.Click += new EventHandler(this.btn현합품목등록_Click);
            this.btn해체리스트.Click += new EventHandler(this.btn해체리스트_Click);
            this.btn조립.Click += new EventHandler(this.btn조립_Click);
            this.btn진행.Click += new EventHandler(this.btn진행_Click);
            this.btn완료.Click += new EventHandler(this.btn완료_Click);
            this.btn해체.Click += new EventHandler(this.btn해체_Click);
            this.btn수주번호적용.Click += new EventHandler(this.btn수주번호적용_Click);
            this.btn비고적용.Click += new EventHandler(this.btn비고적용_Click);

            this._flex좌측.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex중간.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex우측.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);

            this._flex좌측.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
            this._flex중간.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
            this._flex우측.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
            this._flex현합.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);

            this._flex현합.ValidateEdit += new ValidateEditEventHandler(this._flex현합_ValidateEdit);
            this._flex현합.AfterEdit += new RowColEventHandler(this._flex현합_AfterEdit);
            this._flex현합.CheckHeaderClick += new EventHandler(this._flex현합_CheckHeaderClick);
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (string.IsNullOrEmpty(this.ctx현합품목.CodeValue))
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl현합품목.Text);
                return false;
            }

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataSet ds;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                ds = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.cbo공장.SelectedValue.ToString(),
                                                     this.ctx현합품목.CodeValue,
                                                     this.ctx좌측품목.CodeValue,
                                                     this.ctx중간품목.CodeValue,
                                                     this.ctx우측품목.CodeValue,
                                                     this.cbo현합상태.SelectedValue.ToString() });

                if (ds == null)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                else
                {
                    this._flex좌측.Binding = ds.Tables[0];
                    this._flex중간.Binding = ds.Tables[1];
                    this._flex우측.Binding = ds.Tables[2];
                    this._flex현합.Binding = ds.Tables[3];

                    this.btn조립.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            DataRow row좌측, row중간, row우측, row완성;
            decimal clearance1, clearance2;

            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!string.IsNullOrEmpty(this.ctx좌측품목.CodeValue.ToString()) &&
                    !string.IsNullOrEmpty(this.ctx중간품목.CodeValue.ToString()) &&
                    !string.IsNullOrEmpty(this.ctx우측품목.CodeValue.ToString()) &&
                    this._flex좌측.Rows.Count > 0 &&
                    this._flex중간.Rows.Count > 0 &&
                    this._flex우측.Rows.Count > 0)
                {
                    row좌측 = this._flex좌측.GetDataRow(this._flex좌측.Row);
                    row중간 = this._flex중간.GetDataRow(this._flex중간.Row);
                    row우측 = this._flex우측.GetDataRow(this._flex우측.Row);

                    if (row좌측 != null && row중간 != null && row우측 != null)
                    {
                        if (row좌측["YN_USE"].ToString() == "Y" ||
                            row중간["YN_USE"].ToString() == "Y" ||
                            row우측["YN_USE"].ToString() == "Y")
                        {
                            this.ShowMessage("이미 사용된 품목이 선택되어 있습니다.");
                            return;
                        }

                        clearance1 = (D.GetDecimal(row좌측["NUM_MIN"]) - D.GetDecimal(row중간["NUM_MAX"]));
                        clearance2 = (D.GetDecimal(row중간["NUM_MIN"]) - D.GetDecimal(row우측["NUM_MAX"]));

                        row완성 = this._flex현합.DataTable.NewRow();
                        this.완성품생성(ref row완성, row좌측, row중간, row우측, clearance1, clearance2, "002");
                        this._flex현합.DataTable.Rows.Add(row완성);

                        row좌측["YN_USE"] = "Y";
                        row중간["YN_USE"] = "Y";
                        row우측["YN_USE"] = "Y";

                        this.ToolBarDeleteButtonEnabled = true;
                        this.ToolBarSaveButtonEnabled = true;
                    }
                }
                else if (!string.IsNullOrEmpty(this.ctx좌측품목.CodeValue.ToString()) &&
                         !string.IsNullOrEmpty(this.ctx중간품목.CodeValue.ToString()) &&
                         this._flex좌측.Rows.Count > 0 &&
                         this._flex중간.Rows.Count > 0)
                {
                    row좌측 = this._flex좌측.GetDataRow(this._flex좌측.Row);
                    row중간 = this._flex중간.GetDataRow(this._flex중간.Row);

                    if (row좌측 != null && row중간 != null)
                    {
                        if (row좌측["YN_USE"].ToString() == "Y" ||
                            row중간["YN_USE"].ToString() == "Y")
                        {
                            this.ShowMessage("이미 사용된 품목이 선택되어 있습니다.");
                            return;
                        }

                        clearance1 = (D.GetDecimal(row좌측["NUM_MIN"]) - D.GetDecimal(row중간["NUM_MAX"]));
                        
                        row완성 = this._flex현합.DataTable.NewRow();
                        this.완성품생성(ref row완성, row좌측, row중간, null, clearance1, 0, "002");
                        this._flex현합.DataTable.Rows.Add(row완성);

                        row좌측["YN_USE"] = "Y";
                        row중간["YN_USE"] = "Y";

                        this.ToolBarDeleteButtonEnabled = true;
                        this.ToolBarSaveButtonEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex현합.HasNormalRow) return;
                if (this._flex현합.Row < 0) return;

                this._flex현합.RemoveItem(this._flex현합.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData()) return false;
            if (!this._flex현합.IsDataChanged) return false;
            if (!this._biz.Save(D.GetString(this.cbo공장.SelectedValue), this._flex현합.GetChanges())) return false;

            this._flex현합.AcceptChanges();

            return true;
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = (FlexGrid)sender;

                string colname = grid.Cols[e.Col].Name;
                string oldValue = D.GetString(grid.GetData(e.Row, e.Col));
                string newValue = grid.EditData;

                if (colname == "S")
                {
                    if (grid["YN_USE"].ToString() == "Y")
                    {
                        this.ShowMessage("이미 사용된 품목 입니다.");
                        grid["S"] = oldValue;
                        e.Cancel = true;
                        return;
                    }
                    else if (newValue == "Y")
                    {
                        if (grid.Name == this._flex좌측.Name)
                            row좌측선택 = e.Row;
                        else if (grid.Name == this._flex중간.Name)
                            row중간선택 = e.Row;
                        else if (grid.Name == this._flex우측.Name)
                            row우측선택 = e.Row;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                if (!grid.HasNormalRow) return;

                CellStyle cellStyle = grid.Rows[e.Row].Style;

                if (grid.Name == this._flex좌측.Name ||
                    grid.Name == this._flex중간.Name ||
                    grid.Name == this._flex우측.Name)
                {
                    switch (D.GetString(grid[e.Row, "YN_USE"]))
                    {
                        case "N":
                            if (cellStyle == null || cellStyle.Name != "미사용")
                                grid.Rows[e.Row].Style = grid.Styles["미사용"];
                            break;
                        case "Y":
                            if (cellStyle == null || cellStyle.Name != "사용")
                                grid.Rows[e.Row].Style = grid.Styles["사용"];
                            break;
                    }
                }
                else if (grid.Name == this._flex현합.Name)
                {
                    switch (D.GetString(grid[e.Row, "STA_MATCHING"]))
                    {
                        case "001":
                        case "002":
                            if (cellStyle == null || cellStyle.Name != "신규")
                                grid.Rows[e.Row].Style = grid.Styles["신규"];
                            break;
                        default:
                            if (cellStyle == null || cellStyle.Name != "기존")
                                grid.Rows[e.Row].Style = grid.Styles["기존"];
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex현합_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;
            string name;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;

                name = grid.Cols[e.Col].Name;

                if (name == "NO_SO")
                {
                    DataTable dt = DBHelper.GetDataTable(@"SELECT * 
                                                           FROM SA_SOH SH WITH(NOLOCK)
                                                           WHERE SH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                          "AND SH.NO_SO = '" + this._flex현합["NO_SO"].ToString() + "'");

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.ShowMessage("수주건이 존재하지 않습니다.");
                        this._flex현합["NO_SO"] = string.Empty;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex현합_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid grid;
            string name;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;

                name = grid.Cols[e.Col].Name;
                
                if (name == "S")
                {
                    this.cur선택수량.DecimalValue = this._flex현합.DataTable.Select("S = 'Y'").Length;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex현합_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                this.cur선택수량.DecimalValue = this._flex현합.DataTable.Select("S = 'Y'").Length;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ContextMenuItem_Click(object sender, EventArgs e)
        {
            string query;
            FlexGrid grid;

            try
            {
                ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
                if (toolStripMenuItem == null) return;

                query = @"INSERT INTO CZ_PR_MATCHING_DEACTIVATE
                          (CD_COMPANY, CD_PLANT, NO_ID, STA_DEACTIVATE, NO_WO, CD_ITEM, CD_PITEM, NUM_P1_OUT, NUM_P2_OUT, NUM_P3_OUT, NUM_P1_IN, NUM_P2_IN, NUM_P3_IN, ID_INSERT, DTS_INSERT)
                          VALUES
                          ('{0}', '{1}', '{2}', '005', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', NEOE.SF_SYSDATE(GETDATE()))";

                switch(toolStripMenuItem.Name)
                {
                    case "개별판매처리_좌측":
                        grid = this._flex좌측;
                        query = string.Format(query, new string[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                    Global.MainFrame.LoginInfo.CdPlant,
                                                                    this._flex좌측["NO_ID"].ToString(),
                                                                    this._flex좌측["NO_WO"].ToString(),
                                                                    this._flex좌측["CD_ITEM"].ToString(),
                                                                    this._flex좌측["CD_PITEM"].ToString(),
                                                                    this._flex좌측["NUM_P1"].ToString(),
                                                                    this._flex좌측["NUM_P2"].ToString(),
                                                                    this._flex좌측["NUM_P3"].ToString(),
                                                                    "0",
                                                                    "0",
                                                                    "0",
                                                                    Global.MainFrame.LoginInfo.UserID});
                        break;
                    case "개별판매처리_중간":
                        grid = this._flex중간;
                        query = string.Format(query, new string[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                    Global.MainFrame.LoginInfo.CdPlant,
                                                                    this._flex중간["NO_ID"].ToString(),
                                                                    this._flex중간["NO_WO"].ToString(),
                                                                    this._flex중간["CD_ITEM"].ToString(),
                                                                    this._flex중간["CD_PITEM"].ToString(),
                                                                    this._flex중간["NUM_P1_OUT"].ToString(),
                                                                    this._flex중간["NUM_P2_OUT"].ToString(),
                                                                    this._flex중간["NUM_P3_OUT"].ToString(),
                                                                    this._flex중간["NUM_P1_IN"].ToString(),
                                                                    this._flex중간["NUM_P2_IN"].ToString(),
                                                                    this._flex중간["NUM_P3_IN"].ToString(),
                                                                    Global.MainFrame.LoginInfo.UserID});
                        break;
                    case "개별판매처리_우측":
                        grid = this._flex우측;
                        query = string.Format(query, new string[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                    Global.MainFrame.LoginInfo.CdPlant,
                                                                    this._flex우측["NO_ID"].ToString(),
                                                                    this._flex우측["NO_WO"].ToString(),
                                                                    this._flex우측["CD_ITEM"].ToString(),
                                                                    this._flex우측["CD_PITEM"].ToString(),
                                                                    "0",
                                                                    "0",
                                                                    "0",
                                                                    this._flex우측["NUM_P1"].ToString(),
                                                                    this._flex우측["NUM_P2"].ToString(),
                                                                    this._flex우측["NUM_P3"].ToString(),
                                                                    Global.MainFrame.LoginInfo.UserID});
                        break;
                    default:
                        return;
                }

                DBHelper.ExecuteScalar(query);

                grid.RemoveItem(grid.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn현합품목등록_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_PR_MATCHING_ITEM_SUB dialog = new P_CZ_PR_MATCHING_ITEM_SUB();
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn해체리스트_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_PR_MATCHING_DEACTIVATE_SUB dialog = new P_CZ_PR_MATCHING_DEACTIVATE_SUB();
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn조립_Click(object sender, EventArgs e)
        {

            List<DataRow> list좌측, list중간, list우측;
            decimal clearance1, clearance2, min, max, avg좌측, avg중간1, avg중간2, avg우측;
            int total좌측, total중간, total우측, index;
            
            try
            {
                if (this.cur현합수량.DecimalValue == 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, new string[] { this.lbl현합수량.Text, "0" });
                    return;
                }

                Stopwatch time = new Stopwatch();
                time.Start();

                this.btn조립.Enabled = false;
                DataTable dt완성 = this._flex현합.DataTable.Copy();
                
                if (!string.IsNullOrEmpty(this.ctx좌측품목.CodeValue.ToString()) &&
                    !string.IsNullOrEmpty(this.ctx중간품목.CodeValue.ToString()) &&
                    !string.IsNullOrEmpty(this.ctx우측품목.CodeValue.ToString()) &&
                    this._flex좌측.Rows.Count > 0 &&
                    this._flex중간.Rows.Count > 0 &&
                    this._flex우측.Rows.Count > 0)
                {
                    list좌측 = this._flex좌측.DataTable.AsEnumerable()
                                                       .Where(x => x["YN_USE"].ToString() == "N")
                                                       .ToList<DataRow>();
                    list중간 = this._flex중간.DataTable.AsEnumerable()
                                                       .Where(x => x["YN_USE"].ToString() == "N")
                                                       .ToList<DataRow>();
                    list우측 = this._flex우측.DataTable.AsEnumerable()
                                                       .Where(x => x["YN_USE"].ToString() == "N")
                                                       .ToList<DataRow>();

                    total좌측 = list좌측.Count;
                    total중간 = list중간.Count;
                    total우측 = list우측.Count;

                    avg좌측 = list좌측.Average(x => D.GetDecimal(x["NUM_MIN"]));
                    avg중간1 = list중간.Average(x => D.GetDecimal(x["NUM_MAX"]));
                    avg중간2 = list중간.Average(x => D.GetDecimal(x["NUM_MIN"]));
                    avg우측 = list우측.Average(x => D.GetDecimal(x["NUM_MAX"]));

                    Dictionary<DataRow, DataRow> dicTemp = new Dictionary<DataRow, DataRow>();

                    #region 3개품목
                    if (total좌측 <= total중간 && total좌측 <= total우측)
                    {
                        #region 좌측
                        index = 0;
                        foreach (DataRow dr좌측 in list좌측.OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MIN"]) - avg좌측)))
                        {
                            MsgControl.ShowMsg("1단계 진행중 @/@" + Environment.NewLine +
                                               "경과시간 : @", new string[] { D.GetString(++index), 
                                                                              total좌측.ToString(),
                                                                              time.Elapsed.ToString() });

                            min = (D.GetDecimal(dr좌측["NUM_MIN"]) - this.cur사양1최대값.DecimalValue);
                            max = (D.GetDecimal(dr좌측["NUM_MIN"]) - this.cur사양1최소값.DecimalValue);

                            DataRow dr중간 = list중간.Where(x => D.GetDecimal(x["NUM_MAX"]) >= min &&
                                                                 D.GetDecimal(x["NUM_MAX"]) <= max)
                                                     .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MAX"]) - avg중간1))
                                                     .FirstOrDefault();

                            if (dr중간 != null)
                            {
                                list중간.Remove(dr중간);
                                dicTemp.Add(dr좌측, dr중간);
                            }
                        }

                        index = 0;
                        foreach (KeyValuePair<DataRow, DataRow> kvpTemp in dicTemp.OrderByDescending(x => Math.Abs(D.GetDecimal(x.Value["NUM_MIN"]) - avg중간2)))
                        {
                            if (dt완성.Rows.Count >= this.cur현합수량.DecimalValue) break;

                            MsgControl.ShowMsg("2단계 진행중 @/@" + Environment.NewLine +
                                               "경과시간 : @", new string[] { D.GetString(++index), 
                                                                              dicTemp.Count.ToString(),
                                                                              time.Elapsed.ToString() });

                            min = (D.GetDecimal(kvpTemp.Value["NUM_MIN"]) - this.cur사양2최대값.DecimalValue);
                            max = (D.GetDecimal(kvpTemp.Value["NUM_MIN"]) - this.cur사양2최소값.DecimalValue);

                            DataRow dr우측 = list우측.Where(x => D.GetDecimal(x["NUM_MAX"]) >= min &&
                                                                 D.GetDecimal(x["NUM_MAX"]) <= max)
                                                     .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MAX"]) - avg우측))
                                                     .FirstOrDefault();

                            if (dr우측 != null)
                            {
                                list우측.Remove(dr우측);

                                clearance1 = (D.GetDecimal(kvpTemp.Key["NUM_MIN"]) - D.GetDecimal(kvpTemp.Value["NUM_MAX"]));
                                clearance2 = (D.GetDecimal(kvpTemp.Value["NUM_MIN"]) - D.GetDecimal(dr우측["NUM_MAX"]));

                                DataRow dr완성 = dt완성.NewRow();
                                this.완성품생성(ref dr완성, kvpTemp.Key, kvpTemp.Value, dr우측, clearance1, clearance2, "001");
                                dt완성.Rows.Add(dr완성);

                                kvpTemp.Key["YN_USE"] = "Y";
                                kvpTemp.Value["YN_USE"] = "Y";
                                dr우측["YN_USE"] = "Y";
                            }
                        }
                        #endregion
                    }
                    else if (total중간 <= total좌측 && total중간 <= total우측)
                    {
                        if (total좌측 <= total우측)
                        {
                            #region 중좌우
                            index = 0;
                            foreach (DataRow dr중간 in list중간.OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MAX"]) - avg중간1)))
                            {
                                MsgControl.ShowMsg("1단계 진행중 @/@" + Environment.NewLine +
                                                   "경과시간 : @", new string[] { D.GetString(++index), 
                                                                                  total중간.ToString(),
                                                                                  time.Elapsed.ToString() });

                                min = (D.GetDecimal(dr중간["NUM_MAX"]) + this.cur사양1최소값.DecimalValue);
                                max = (D.GetDecimal(dr중간["NUM_MAX"]) + this.cur사양1최대값.DecimalValue);

                                DataRow dr좌측 = list좌측.Where(x => D.GetDecimal(x["NUM_MIN"]) >= min &&
                                                                     D.GetDecimal(x["NUM_MIN"]) <= max)
                                                         .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MIN"]) - avg좌측))
                                                         .FirstOrDefault();

                                if (dr좌측 != null)
                                {
                                    list좌측.Remove(dr좌측);
                                    dicTemp.Add(dr중간, dr좌측);
                                }
                            }

                            index = 0;
                            foreach (KeyValuePair<DataRow, DataRow> kvpTemp in dicTemp.OrderByDescending(x => Math.Abs(D.GetDecimal(x.Key["NUM_MIN"]) - avg중간2)))
                            {
                                if (dt완성.Rows.Count >= this.cur현합수량.DecimalValue) break;

                                MsgControl.ShowMsg("2단계 진행중 @/@" + Environment.NewLine +
                                                   "경과시간 : @", new string[] { D.GetString(++index), 
                                                                                  dicTemp.Count.ToString(),
                                                                                  time.Elapsed.ToString() });

                                min = (D.GetDecimal(kvpTemp.Key["NUM_MIN"]) - this.cur사양2최대값.DecimalValue);
                                max = (D.GetDecimal(kvpTemp.Key["NUM_MIN"]) - this.cur사양2최소값.DecimalValue);

                                DataRow dr우측 = list우측.Where(x => D.GetDecimal(x["NUM_MAX"]) >= min &&
                                                                     D.GetDecimal(x["NUM_MAX"]) <= max)
                                                         .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MAX"]) - avg우측))
                                                         .FirstOrDefault();

                                if (dr우측 != null)
                                {
                                    list우측.Remove(dr우측);

                                    clearance1 = (D.GetDecimal(kvpTemp.Value["NUM_MIN"]) - D.GetDecimal(kvpTemp.Key["NUM_MAX"]));
                                    clearance2 = (D.GetDecimal(kvpTemp.Key["NUM_MIN"]) - D.GetDecimal(dr우측["NUM_MAX"]));

                                    DataRow dr완성 = dt완성.NewRow();
                                    this.완성품생성(ref dr완성, kvpTemp.Value, kvpTemp.Key, dr우측, clearance1, clearance2, "001");
                                    dt완성.Rows.Add(dr완성);

                                    kvpTemp.Value["YN_USE"] = "Y";
                                    kvpTemp.Key["YN_USE"] = "Y";
                                    dr우측["YN_USE"] = "Y";
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region 중우좌
                            index = 0;
                            foreach (DataRow dr중간 in list중간.OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MIN"]) - avg중간2)))
                            {
                                MsgControl.ShowMsg("1단계 진행중 @/@" + Environment.NewLine +
                                                   "경과시간 : @", new string[] { D.GetString(++index), 
                                                                                  total중간.ToString(),
                                                                                  time.Elapsed.ToString() });

                                min = (D.GetDecimal(dr중간["NUM_MIN"]) - this.cur사양2최대값.DecimalValue);
                                max = (D.GetDecimal(dr중간["NUM_MIN"]) - this.cur사양2최소값.DecimalValue);

                                DataRow dr우측 = list우측.Where(x => D.GetDecimal(x["NUM_MAX"]) >= min &&
                                                                     D.GetDecimal(x["NUM_MAX"]) <= max)
                                                         .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MAX"]) - avg우측))
                                                         .FirstOrDefault();

                                if (dr우측 != null)
                                {
                                    list우측.Remove(dr우측);
                                    dicTemp.Add(dr중간, dr우측);
                                }
                            }

                            index = 0;
                            foreach (KeyValuePair<DataRow, DataRow> kvpTemp in dicTemp.OrderByDescending(x => Math.Abs(D.GetDecimal(x.Key["NUM_MAX"]) - avg중간1)))
                            {
                                if (dt완성.Rows.Count >= this.cur현합수량.DecimalValue) break;

                                MsgControl.ShowMsg("2단계 진행중 @/@" + Environment.NewLine +
                                                   "경과시간 : @", new string[] { D.GetString(++index), 
                                                                                  dicTemp.Count.ToString(),
                                                                                  time.Elapsed.ToString() });

                                min = (D.GetDecimal(kvpTemp.Key["NUM_MAX"]) + this.cur사양1최소값.DecimalValue);
                                max = (D.GetDecimal(kvpTemp.Key["NUM_MAX"]) + this.cur사양1최대값.DecimalValue);

                                DataRow dr좌측 = list좌측.Where(x => D.GetDecimal(x["NUM_MIN"]) >= min &&
                                                                     D.GetDecimal(x["NUM_MIN"]) <= max)
                                                         .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MIN"]) - avg좌측))
                                                         .FirstOrDefault();

                                if (dr좌측 != null)
                                {
                                    list좌측.Remove(dr좌측);

                                    clearance1 = (D.GetDecimal(dr좌측["NUM_MIN"]) - D.GetDecimal(kvpTemp.Key["NUM_MAX"]));
                                    clearance2 = (D.GetDecimal(kvpTemp.Key["NUM_MIN"]) - D.GetDecimal(kvpTemp.Value["NUM_MAX"]));

                                    DataRow dr완성 = dt완성.NewRow();
                                    this.완성품생성(ref dr완성, dr좌측, kvpTemp.Key, kvpTemp.Value, clearance1, clearance2, "001");
                                    dt완성.Rows.Add(dr완성);

                                    dr좌측["YN_USE"] = "Y";
                                    kvpTemp.Key["YN_USE"] = "Y";
                                    kvpTemp.Value["YN_USE"] = "Y";
                                }
                            }
                            #endregion
                        }
                    }
                    else if (total우측 <= total좌측 && total우측 <= total중간)
                    {
                        #region 우측
                        index = 0;
                        foreach (DataRow dr우측 in list우측.OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MAX"]) - avg우측)))
                        {
                            MsgControl.ShowMsg("1단계 진행중 @/@" + Environment.NewLine +
                                               "경과시간 : @", new string[] { D.GetString(++index), 
                                                                              total우측.ToString(),
                                                                              time.Elapsed.ToString() });

                            min = (D.GetDecimal(dr우측["NUM_MAX"]) + this.cur사양2최소값.DecimalValue);
                            max = (D.GetDecimal(dr우측["NUM_MAX"]) + this.cur사양2최대값.DecimalValue);

                            DataRow dr중간 = list중간.Where(x => D.GetDecimal(x["NUM_MIN"]) >= min &&
                                                                 D.GetDecimal(x["NUM_MIN"]) <= max)
                                                     .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MIN"]) - avg중간2))
                                                     .FirstOrDefault();

                            if (dr중간 != null)
                            {
                                list중간.Remove(dr중간);
                                dicTemp.Add(dr우측, dr중간);
                            }
                        }

                        index = 0;
                        foreach (KeyValuePair<DataRow, DataRow> kvpTemp in dicTemp.OrderByDescending(x => Math.Abs(D.GetDecimal(x.Value["NUM_MAX"]) - avg중간1)))
                        {
                            if (dt완성.Rows.Count >= this.cur현합수량.DecimalValue) break;

                            MsgControl.ShowMsg("2단계 진행중 @/@" + Environment.NewLine +
                                               "경과시간 : @", new string[] { D.GetString(++index), 
                                                                              dicTemp.Count.ToString(),
                                                                              time.Elapsed.ToString() });

                            min = (D.GetDecimal(kvpTemp.Value["NUM_MAX"]) + this.cur사양1최소값.DecimalValue);
                            max = (D.GetDecimal(kvpTemp.Value["NUM_MAX"]) + this.cur사양1최대값.DecimalValue);

                            DataRow dr좌측 = list좌측.Where(x => D.GetDecimal(x["NUM_MIN"]) >= min &&
                                                                 D.GetDecimal(x["NUM_MIN"]) <= max)
                                                     .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MIN"]) - avg좌측))
                                                     .FirstOrDefault();

                            if (dr좌측 != null)
                            {
                                list좌측.Remove(dr좌측);

                                clearance1 = (D.GetDecimal(dr좌측["NUM_MIN"]) - D.GetDecimal(kvpTemp.Value["NUM_MAX"]));
                                clearance2 = (D.GetDecimal(kvpTemp.Value["NUM_MIN"]) - D.GetDecimal(kvpTemp.Key["NUM_MAX"]));

                                DataRow dr완성 = dt완성.NewRow();
                                this.완성품생성(ref dr완성, dr좌측, kvpTemp.Value, kvpTemp.Key, clearance1, clearance2, "001");
                                dt완성.Rows.Add(dr완성);

                                dr좌측["YN_USE"] = "Y";
                                kvpTemp.Value["YN_USE"] = "Y";
                                kvpTemp.Key["YN_USE"] = "Y";
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (!string.IsNullOrEmpty(this.ctx좌측품목.CodeValue.ToString()) &&
                         !string.IsNullOrEmpty(this.ctx중간품목.CodeValue.ToString()) &&
                         this._flex좌측.Rows.Count > 0 &&
                         this._flex중간.Rows.Count > 0)
                {
                    list좌측 = this._flex좌측.DataTable.AsEnumerable()
                                                       .Where(x => x["YN_USE"].ToString() == "N")
                                                       .ToList<DataRow>();
                    list중간 = this._flex중간.DataTable.AsEnumerable()
                                                       .Where(x => x["YN_USE"].ToString() == "N")
                                                       .ToList<DataRow>();

                    total좌측 = list좌측.Count;
                    total중간 = list중간.Count;

                    avg좌측 = list좌측.Average(x => D.GetDecimal(x["NUM_MIN"]));
                    avg중간1 = list중간.Average(x => D.GetDecimal(x["NUM_MAX"]));
                    avg중간2 = list중간.Average(x => D.GetDecimal(x["NUM_MIN"]));

                    #region 2개품목
                    if (total좌측 <= total중간 && false)
                    {
                        #region 좌측
                        index = 0;
                        foreach (DataRow dr좌측 in list좌측.OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MIN"]) - avg좌측)))
                        {
                            if (dt완성.Rows.Count >= this.cur현합수량.DecimalValue) break;

                            MsgControl.ShowMsg("진행중 @/@" + Environment.NewLine +
                                               "경과시간 : @", new string[] { D.GetString(++index), 
                                                                              total좌측.ToString(),
                                                                              time.Elapsed.ToString() });

                            min = (D.GetDecimal(dr좌측["NUM_MIN"]) - this.cur사양1최대값.DecimalValue);
                            max = (D.GetDecimal(dr좌측["NUM_MIN"]) - this.cur사양1최소값.DecimalValue);

                            DataRow dr중간 = list중간.Where(x => D.GetDecimal(x["NUM_MAX"]) >= min &&
                                                                 D.GetDecimal(x["NUM_MAX"]) <= max)
                                                     .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MAX"]) - avg중간1))
                                                     .FirstOrDefault();

                            if (dr중간 != null)
                            {
                                list중간.Remove(dr중간);

                                clearance1 = (D.GetDecimal(dr좌측["NUM_MIN"]) - D.GetDecimal(dr중간["NUM_MAX"]));

                                DataRow dr완성 = dt완성.NewRow();
                                this.완성품생성(ref dr완성, dr좌측, dr중간, null, clearance1, 0, "001");
                                dt완성.Rows.Add(dr완성);

                                dr좌측["YN_USE"] = "Y";
                                dr중간["YN_USE"] = "Y";
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 중간
                        index = 0;
                        foreach (DataRow dr중간 in list중간.OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MAX"]) - avg중간1)))
                        {
                            if (dt완성.Rows.Count >= this.cur현합수량.DecimalValue) break;

                            MsgControl.ShowMsg("진행중 @/@" + Environment.NewLine +
                                               "경과시간 : @", new string[] { D.GetString(++index), 
                                                                              total중간.ToString(),
                                                                              time.Elapsed.ToString() });

                            min = (D.GetDecimal(dr중간["NUM_MAX"]) + this.cur사양1최소값.DecimalValue);
                            max = (D.GetDecimal(dr중간["NUM_MAX"]) + this.cur사양1최대값.DecimalValue);

                            DataRow dr좌측 = list좌측.Where(x => D.GetDecimal(x["NUM_MIN"]) >= min &&
                                                                 D.GetDecimal(x["NUM_MIN"]) <= max)
                                                     .OrderByDescending(x => Math.Abs(D.GetDecimal(x["NUM_MIN"]) - avg좌측))
                                                     .FirstOrDefault();

                            if (dr좌측 != null)
                            {
                                list좌측.Remove(dr좌측);

                                clearance1 = (D.GetDecimal(dr좌측["NUM_MIN"]) - D.GetDecimal(dr중간["NUM_MAX"]));

                                DataRow dr완성 = dt완성.NewRow();
                                this.완성품생성(ref dr완성, dr좌측, dr중간, null, clearance1, 0, "001");
                                dt완성.Rows.Add(dr완성);

                                dr좌측["YN_USE"] = "Y";
                                dr중간["YN_USE"] = "Y";
                            }
                        }
                        #endregion
                    }
                    #endregion
                }

                this._flex현합.Binding = dt완성;

                time.Stop();
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

        private void btn진행_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex현합.DataTable.Select("S = 'Y' AND STA_MATCHING <> '001' AND STA_MATCHING <> '002'").Length > 0)
                {
                    this.ShowMessage("신규상태가 아닌 건이 선택되어 있습니다.");
                    return;
                }

                foreach (DataRow dr in this._flex현합.DataTable.Select("S = 'Y'"))
                {
                    dr["STA_MATCHING"] = "003";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn완료_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex현합.DataTable.Select("S = 'Y' AND STA_MATCHING <> '003'").Length > 0)
                {
                    this.ShowMessage("진행상태가 아닌 건이 선택되어 있습니다.");
                    return;
                }

                foreach (DataRow dr in this._flex현합.DataTable.Select("S = 'Y'"))
                {
                    dr["STA_MATCHING"] = "004";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn해체_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex현합.DataTable.Select("S = 'Y' AND STA_MATCHING <> '003' AND STA_MATCHING <> '004'").Length > 0)
                {
                    this.ShowMessage("진행, 완료상태가 아닌 건이 선택되어 있습니다.");
                    return;
                }

                if (this._flex현합.DataTable.Select("S = 'Y'").Any(x => x.RowState == DataRowState.Added))
                {
                    this.ShowMessage("저장되지 않은 항목이 선택 되어 있습니다.\n저장 후 진행 하시기 바랍니다.");
                    return;
                }

                foreach (DataRow dr in this._flex현합.DataTable.Select("S = 'Y'"))
                {
                    dr["STA_MATCHING"] = "005";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn수주번호적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt수주번호.Text))
                {
                    this.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl수주번호.Text);
                    return;
                }

                DataTable dt = DBHelper.GetDataTable(@"SELECT * 
                                                       FROM SA_SOH SH WITH(NOLOCK)
                                                       WHERE SH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                      "AND SH.NO_SO = '" + this.txt수주번호.Text + "'");

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage("수주건이 존재하지 않습니다.");
                    return;
                }

                foreach (DataRow dr in this._flex현합.DataTable.Select("S = 'Y'"))
                {
                    dr["NO_SO"] = this.txt수주번호.Text;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn비고적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt비고.Text))
                {
                    this.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl비고.Text);
                    return;
                }

                foreach (DataRow dr in this._flex현합.DataTable.Select("S = 'Y'"))
                {
                    dr["DC_RMK"] = this.txt비고.Text;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 완성품생성(ref DataRow dr완성, DataRow dr좌측, DataRow dr중간, DataRow dr우측, decimal Clearance1, decimal Clearance2, string state)
        {
            try
            {
                dr완성["STA_MATCHING"] = state;
                dr완성["CD_ITEM"] = D.GetString(dr좌측["CD_ITEM"]);

                dr완성["NO_WO_L"] = D.GetString(dr좌측["NO_WO"]);
                dr완성["CD_PITEM_L"] = D.GetString(dr좌측["CD_PITEM"]);
                dr완성["NO_ID_L"] = D.GetString(dr좌측["NO_ID"]);

                dr완성["NUM_P1_L"] = D.GetDecimal(dr좌측["NUM_P1"]);
                dr완성["NUM_P2_L"] = D.GetDecimal(dr좌측["NUM_P2"]);
                dr완성["NUM_P3_L"] = D.GetDecimal(dr좌측["NUM_P3"]);
                dr완성["NUM_MIN_L"] = D.GetDecimal(dr좌측["NUM_MIN"]);

                dr완성["NUM_C1"] = Clearance1;

                dr완성["NO_WO_C"] = D.GetString(dr중간["NO_WO"]);
                dr완성["CD_PITEM_C"] = D.GetString(dr중간["CD_PITEM"]);
                dr완성["NO_ID_C"] = D.GetString(dr중간["NO_ID"]);

                dr완성["NUM_P1_OUT_C"] = D.GetDecimal(dr중간["NUM_P1_OUT"]);
                dr완성["NUM_P2_OUT_C"] = D.GetDecimal(dr중간["NUM_P2_OUT"]);
                dr완성["NUM_P3_OUT_C"] = D.GetDecimal(dr중간["NUM_P3_OUT"]);
                dr완성["NUM_MAX_C"] = D.GetDecimal(dr중간["NUM_MAX"]);

                dr완성["NUM_P1_IN_C"] = D.GetDecimal(dr중간["NUM_P1_IN"]);
                dr완성["NUM_P2_IN_C"] = D.GetDecimal(dr중간["NUM_P2_IN"]);
                dr완성["NUM_P3_IN_C"] = D.GetDecimal(dr중간["NUM_P3_IN"]);
                dr완성["NUM_MIN_C"] = D.GetDecimal(dr중간["NUM_MIN"]);

                if (dr우측 != null)
                {
                    dr완성["NUM_C2"] = Clearance2;

                    dr완성["NO_WO_R"] = D.GetString(dr우측["NO_WO"]);
                    dr완성["CD_PITEM_R"] = D.GetString(dr우측["CD_PITEM"]);
                    dr완성["NO_ID_R"] = D.GetString(dr우측["NO_ID"]);

                    dr완성["NUM_P1_R"] = D.GetDecimal(dr우측["NUM_P1"]);
                    dr완성["NUM_P2_R"] = D.GetDecimal(dr우측["NUM_P2"]);
                    dr완성["NUM_P3_R"] = D.GetDecimal(dr우측["NUM_P3"]);
                    dr완성["NUM_MAX_R"] = D.GetDecimal(dr우측["NUM_MAX"]);
                }
                else
                {
                    dr완성["NO_ID_R"] = "NONE";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 품목명설정(string 좌측품목, string 중간품목, string 우측품목)
        {
            try
            {
                this._flex현합[0, this._flex현합.Cols["NO_ID_L"].Index] = 좌측품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P1_L"].Index] = 좌측품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P2_L"].Index] = 좌측품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P3_L"].Index] = 좌측품목;
                this._flex현합[0, this._flex현합.Cols["NUM_MIN_L"].Index] = 좌측품목;

                this._flex현합[0, this._flex현합.Cols["NO_ID_C"].Index] = 중간품목;

                this._flex현합[0, this._flex현합.Cols["NUM_P1_OUT_C"].Index] = 중간품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P2_OUT_C"].Index] = 중간품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P3_OUT_C"].Index] = 중간품목;
                this._flex현합[0, this._flex현합.Cols["NUM_MAX_C"].Index] = 중간품목;

                this._flex현합[0, this._flex현합.Cols["NUM_P1_IN_C"].Index] = 중간품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P2_IN_C"].Index] = 중간품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P3_IN_C"].Index] = 중간품목;
                this._flex현합[0, this._flex현합.Cols["NUM_MIN_C"].Index] = 중간품목;

                this._flex현합[0, this._flex현합.Cols["NO_ID_R"].Index] = 우측품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P1_R"].Index] = 우측품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P2_R"].Index] = 우측품목;
                this._flex현합[0, this._flex현합.Cols["NUM_P3_R"].Index] = 우측품목;
                this._flex현합[0, this._flex현합.Cols["NUM_MAX_R"].Index] = 우측품목;
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
                if (string.IsNullOrEmpty(D.GetString(this.cbo공장.SelectedValue)))
                {
                    this.ShowMessage(공통메세지._자료가선택되어있지않습니다, new string[] { this.lbl공장.Text });

                    this.cbo공장.Focus();
                    e.QueryCancel = true;
                }
                else
                {
                    e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable("SP_CZ_PR_MATCHING_ITEM_S", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                D.GetString(this.cbo공장.SelectedValue),
                                                                                                this.ctx현합품목.CodeValue });

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ctx현합품목.CodeValue = null;
                    this.ctx현합품목.CodeName = null;

                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    this.ctx좌측품목.CodeValue = D.GetString(dt.Rows[0]["CD_PITEM_L"]);
                    this.ctx좌측품목.CodeName = D.GetString(dt.Rows[0]["NM_PITEM_L"]);
                    this.ctx중간품목.CodeValue = D.GetString(dt.Rows[0]["CD_PITEM_C"]);
                    this.ctx중간품목.CodeName = D.GetString(dt.Rows[0]["NM_PITEM_C"]);
                    this.ctx우측품목.CodeValue = D.GetString(dt.Rows[0]["CD_PITEM_R"]);
                    this.ctx우측품목.CodeName = D.GetString(dt.Rows[0]["NM_PITEM_R"]);

                    this.pnl좌측.TitleText = this.ctx좌측품목.CodeName;
                    this.pnl중간.TitleText = this.ctx중간품목.CodeName;
                    this.pnl우측.TitleText = this.ctx우측품목.CodeName;
                    this.pnl현합.TitleText = this.ctx현합품목.CodeName;

                    this.cur사양1최소값.DecimalValue = D.GetDecimal(dt.Rows[0]["NUM_SPEC1_MIN"]);
                    this.cur사양1최대값.DecimalValue = D.GetDecimal(dt.Rows[0]["NUM_SPEC1_MAX"]);
                    this.cur사양2최소값.DecimalValue = D.GetDecimal(dt.Rows[0]["NUM_SPEC2_MIN"]);
                    this.cur사양2최대값.DecimalValue = D.GetDecimal(dt.Rows[0]["NUM_SPEC2_MAX"]);

                    this.품목명설정(this.ctx좌측품목.CodeName, this.ctx중간품목.CodeName, this.ctx우측품목.CodeName);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}
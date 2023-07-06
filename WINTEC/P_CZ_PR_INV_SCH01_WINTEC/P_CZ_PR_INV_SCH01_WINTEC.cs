using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.Windows.Print;
using System;
using System.Data;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PR_INV_SCH01_WINTEC : PageBase
    {
        private P_CZ_PR_INV_SCH01_WINTEC_BIZ _biz = new P_CZ_PR_INV_SCH01_WINTEC_BIZ();
        private string 구역구분;

        public P_CZ_PR_INV_SCH01_WINTEC()
        {
            try
            {
                this.InitializeComponent();
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

        protected override void InitPaint()
        {
            base.InitPaint();
            this.oneGrid1.UseCustomLayout = true;
            this.bpP_Plant.IsNecessaryCondition = true;
            this.bpP_Date.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            DataSet comboData = this.GetComboData(new string[] { "NC;MA_PLANT" });
            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
            else if (this.cbo공장.Items.Count > 0)
                this.cbo공장.SelectedIndex = 0;
            this.dtp작업지시기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-6).ToString("yyyyMMdd");
            this.dtp작업지시기간.EndDateToString = Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd");
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex1, this._flex2, this._flex3, this._flex4, this._flex5, this._flex6 };

            this._flex1.BeginSetting(1, 1, false);
            this._flex1.SetCol("CD_OP", "OP", 40, false);
            this._flex1.SetCol("NM_OP", "공정명", 100, false);
            this._flex1.SetCol("NM_WC", "작업장명", 100, false);
            this._flex1.SetCol("QT_WIP", "재공재고", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex1.SetCol("DC_RMK", "비고", 100, true);
            this._flex1.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex2.BeginSetting(1, 1, false);
            this._flex2.SetCol("CD_OP", "OP", 40, false);
            this._flex2.SetCol("NM_OP", "공정명", 100, false);
            this._flex2.SetCol("NM_WC", "작업장명", 100, false);
            this._flex2.SetCol("QT_WIP", "재공재고", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex2.SetCol("DC_RMK", "비고", 100, true);
            this._flex2.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex3.BeginSetting(1, 1, false);
            this._flex3.SetCol("CD_OP", "OP", 40, false);
            this._flex3.SetCol("NM_OP", "공정명", 100, false);
            this._flex3.SetCol("NM_WC", "작업장명", 100, false);
            this._flex3.SetCol("QT_WIP", "재공재고", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex3.SetCol("DC_RMK", "비고", 100, true);
            this._flex3.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex4.BeginSetting(1, 1, false);
            this._flex4.SetCol("CD_OP", "OP", 40, false);
            this._flex4.SetCol("NM_OP", "공정명", 100, false);
            this._flex4.SetCol("NM_WC", "작업장명", 100, false);
            this._flex4.SetCol("QT_WIP", "재공재고", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex4.SetCol("DC_RMK", "비고", 100, true);
            this._flex4.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex5.BeginSetting(1, 1, false);
            this._flex5.SetCol("CD_OP", "OP", 40, false);
            this._flex5.SetCol("NM_OP", "공정명", 100, false);
            this._flex5.SetCol("NM_WC", "작업장명", 100, false);
            this._flex5.SetCol("QT_WIP", "재공재고", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex5.SetCol("DC_RMK", "비고", 100, true);
            this._flex5.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex6.BeginSetting(1, 1, false);
            this._flex6.SetCol("CD_OP", "OP", 40, false);
            this._flex6.SetCol("NM_OP", "공정명", 100, false);
            this._flex6.SetCol("NM_WC", "작업장명", 100, false);
            this._flex6.SetCol("QT_WIP", "재공재고", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex6.SetCol("DC_RMK", "비고", 100, true);
            this._flex6.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex재고1.BeginSetting(1, 1, false);
            this._flex재고1.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex재고1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex재고2.BeginSetting(1, 1, false);
            this._flex재고2.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex재고2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex재고3.BeginSetting(1, 1, false);
            this._flex재고3.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex재고3.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex재고4.BeginSetting(1, 1, false);
            this._flex재고4.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex재고4.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex재고5.BeginSetting(1, 1, false);
            this._flex재고5.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex재고5.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex재고6.BeginSetting(1, 1, false);
            this._flex재고6.SetCol("NO_DESIGN", "도면번호", 100, false);
            this._flex재고6.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitGrid_New(DataTable dtCross, DataTable dtSL)
        {
            int ordinal;
            string expression;
            switch (구역구분)
            {
                case "1":
                    this._flex재고1.AllowCache = false;
                    this._flex재고1.Cols.Count = 2;
                    this._flex재고1.BeginSetting(1, 1, false);
                    ordinal = dtCross.Columns["UNIT_IM"].Ordinal;
                    expression = "";
                    for (int index = 0; index < dtSL.Rows.Count; ++index)
                    {
                        int num = dtSL.Rows[index]["NM_SL"].ToString().Length * 18;
                        this._flex재고1.SetCol(index.ToString() + "_QT_INV", dtSL.Rows[index]["NM_SL"].ToString(), num + 36, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                        expression = expression + "[" + index + "_QT_INV]";
                        if (index < dtSL.Rows.Count - 1)
                            expression += " + ";
                    }
                    dtCross.AcceptChanges();
                    foreach (DataRow row in dtCross.Rows)
                    {
                        bool flag = true;
                        for (int columnIndex = ordinal + 1; columnIndex < dtCross.Columns.Count; ++columnIndex)
                        {
                            if (flag)
                                flag = flag && D.GetDecimal(row[columnIndex]) == 0M;
                            if (row[columnIndex] == DBNull.Value)
                                row[columnIndex] = 0M;
                        }
                        if (flag)
                            row.Delete();
                    }
                    dtCross.Columns.Add("QT_SUM", typeof(decimal), expression);
                    this._flex재고1.SetCol("QT_SUM", "합계", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고1.Cols.Frozen = this._flex재고1.Cols.Fixed;
                    this._flex재고1.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    this._flex재고1.Redraw = false;
                    this._flex재고1.Redraw = true;
                    this._flex재고1.AllowCache = true;
                    for (int index = this._flex재고1.Cols.Fixed; index < this._flex재고1.Cols.Count; ++index)
                        this._flex재고1.SetCellStyle(0, index, this._flex재고1.Styles.Fixed);
                    break;

                case "2":
                    this._flex재고2.AllowCache = false;
                    this._flex재고2.Cols.Count = 2;
                    this._flex재고2.BeginSetting(1, 1, false);
                    ordinal = dtCross.Columns["UNIT_IM"].Ordinal;
                    expression = "";
                    for (int index = 0; index < dtSL.Rows.Count; ++index)
                    {
                        int num = dtSL.Rows[index]["NM_SL"].ToString().Length * 18;
                        this._flex재고2.SetCol(index.ToString() + "_QT_INV", dtSL.Rows[index]["NM_SL"].ToString(), num + 36, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                        expression = expression + "[" + index + "_QT_INV]";
                        if (index < dtSL.Rows.Count - 1)
                            expression += " + ";
                    }
                    dtCross.AcceptChanges();
                    foreach (DataRow row in dtCross.Rows)
                    {
                        bool flag = true;
                        for (int columnIndex = ordinal + 1; columnIndex < dtCross.Columns.Count; ++columnIndex)
                        {
                            if (flag)
                                flag = flag && D.GetDecimal(row[columnIndex]) == 0M;
                            if (row[columnIndex] == DBNull.Value)
                                row[columnIndex] = 0M;
                        }
                        if (flag)
                            row.Delete();
                    }
                    dtCross.Columns.Add("QT_SUM", typeof(decimal), expression);
                    this._flex재고2.SetCol("QT_SUM", "합계", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고2.Cols.Frozen = this._flex재고2.Cols.Fixed;
                    this._flex재고2.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    this._flex재고2.Redraw = false;
                    this._flex재고2.Redraw = true;
                    this._flex재고2.AllowCache = true;
                    for (int index = this._flex재고2.Cols.Fixed; index < this._flex재고2.Cols.Count; ++index)
                        this._flex재고2.SetCellStyle(0, index, this._flex재고2.Styles.Fixed);
                    break;

                case "3":
                    this._flex재고3.AllowCache = false;
                    this._flex재고3.Cols.Count = 2;
                    this._flex재고3.BeginSetting(1, 1, false);
                    ordinal = dtCross.Columns["UNIT_IM"].Ordinal;
                    expression = "";
                    for (int index = 0; index < dtSL.Rows.Count; ++index)
                    {
                        int num = dtSL.Rows[index]["NM_SL"].ToString().Length * 18;
                        this._flex재고3.SetCol(index.ToString() + "_QT_INV", dtSL.Rows[index]["NM_SL"].ToString(), num + 36, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                        expression = expression + "[" + index + "_QT_INV]";
                        if (index < dtSL.Rows.Count - 1)
                            expression += " + ";
                    }
                    dtCross.AcceptChanges();
                    foreach (DataRow row in dtCross.Rows)
                    {
                        bool flag = true;
                        for (int columnIndex = ordinal + 1; columnIndex < dtCross.Columns.Count; ++columnIndex)
                        {
                            if (flag)
                                flag = flag && D.GetDecimal(row[columnIndex]) == 0M;
                            if (row[columnIndex] == DBNull.Value)
                                row[columnIndex] = 0M;
                        }
                        if (flag)
                            row.Delete();
                    }
                    dtCross.Columns.Add("QT_SUM", typeof(decimal), expression);
                    this._flex재고3.SetCol("QT_SUM", "합계", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고3.Cols.Frozen = this._flex재고3.Cols.Fixed;
                    this._flex재고3.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    this._flex재고3.Redraw = false;
                    this._flex재고3.Redraw = true;
                    this._flex재고3.AllowCache = true;
                    for (int index = this._flex재고3.Cols.Fixed; index < this._flex재고3.Cols.Count; ++index)
                        this._flex재고3.SetCellStyle(0, index, this._flex재고3.Styles.Fixed);
                    break;

                case "4":
                    this._flex재고4.AllowCache = false;
                    this._flex재고4.Cols.Count = 2;
                    this._flex재고4.BeginSetting(1, 1, false);
                    ordinal = dtCross.Columns["UNIT_IM"].Ordinal;
                    expression = "";
                    for (int index = 0; index < dtSL.Rows.Count; ++index)
                    {
                        int num = dtSL.Rows[index]["NM_SL"].ToString().Length * 18;
                        this._flex재고4.SetCol(index.ToString() + "_QT_INV", dtSL.Rows[index]["NM_SL"].ToString(), num + 36, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                        expression = expression + "[" + index + "_QT_INV]";
                        if (index < dtSL.Rows.Count - 1)
                            expression += " + ";
                    }
                    dtCross.AcceptChanges();
                    foreach (DataRow row in dtCross.Rows)
                    {
                        bool flag = true;
                        for (int columnIndex = ordinal + 1; columnIndex < dtCross.Columns.Count; ++columnIndex)
                        {
                            if (flag)
                                flag = flag && D.GetDecimal(row[columnIndex]) == 0M;
                            if (row[columnIndex] == DBNull.Value)
                                row[columnIndex] = 0M;
                        }
                        if (flag)
                            row.Delete();
                    }
                    dtCross.Columns.Add("QT_SUM", typeof(decimal), expression);
                    this._flex재고4.SetCol("QT_SUM", "합계", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고4.Cols.Frozen = this._flex재고4.Cols.Fixed;
                    this._flex재고4.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    this._flex재고4.Redraw = false;
                    this._flex재고4.Redraw = true;
                    this._flex재고4.AllowCache = true;
                    for (int index = this._flex재고4.Cols.Fixed; index < this._flex재고4.Cols.Count; ++index)
                        this._flex재고4.SetCellStyle(0, index, this._flex재고4.Styles.Fixed);
                    break;

                case "5":
                    this._flex재고5.AllowCache = false;
                    this._flex재고5.Cols.Count = 2;
                    this._flex재고5.BeginSetting(1, 1, false);
                    ordinal = dtCross.Columns["UNIT_IM"].Ordinal;
                    expression = "";
                    for (int index = 0; index < dtSL.Rows.Count; ++index)
                    {
                        int num = dtSL.Rows[index]["NM_SL"].ToString().Length * 18;
                        this._flex재고5.SetCol(index.ToString() + "_QT_INV", dtSL.Rows[index]["NM_SL"].ToString(), num + 36, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                        expression = expression + "[" + index + "_QT_INV]";
                        if (index < dtSL.Rows.Count - 1)
                            expression += " + ";
                    }
                    dtCross.AcceptChanges();
                    foreach (DataRow row in dtCross.Rows)
                    {
                        bool flag = true;
                        for (int columnIndex = ordinal + 1; columnIndex < dtCross.Columns.Count; ++columnIndex)
                        {
                            if (flag)
                                flag = flag && D.GetDecimal(row[columnIndex]) == 0M;
                            if (row[columnIndex] == DBNull.Value)
                                row[columnIndex] = 0M;
                        }
                        if (flag)
                            row.Delete();
                    }
                    dtCross.Columns.Add("QT_SUM", typeof(decimal), expression);
                    this._flex재고5.SetCol("QT_SUM", "합계", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고5.Cols.Frozen = this._flex재고5.Cols.Fixed;
                    this._flex재고5.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    this._flex재고5.Redraw = false;
                    this._flex재고5.Redraw = true;
                    this._flex재고5.AllowCache = true;
                    for (int index = this._flex재고5.Cols.Fixed; index < this._flex재고5.Cols.Count; ++index)
                        this._flex재고5.SetCellStyle(0, index, this._flex재고5.Styles.Fixed);
                    break;

                case "6":
                    this._flex재고6.AllowCache = false;
                    this._flex재고6.Cols.Count = 2;
                    this._flex재고6.BeginSetting(1, 1, false);
                    ordinal = dtCross.Columns["UNIT_IM"].Ordinal;
                    expression = "";
                    for (int index = 0; index < dtSL.Rows.Count; ++index)
                    {
                        int num = dtSL.Rows[index]["NM_SL"].ToString().Length * 18;
                        this._flex재고6.SetCol(index.ToString() + "_QT_INV", dtSL.Rows[index]["NM_SL"].ToString(), num + 36, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                        expression = expression + "[" + index + "_QT_INV]";
                        if (index < dtSL.Rows.Count - 1)
                            expression += " + ";
                    }
                    dtCross.AcceptChanges();
                    foreach (DataRow row in dtCross.Rows)
                    {
                        bool flag = true;
                        for (int columnIndex = ordinal + 1; columnIndex < dtCross.Columns.Count; ++columnIndex)
                        {
                            if (flag)
                                flag = flag && D.GetDecimal(row[columnIndex]) == 0M;
                            if (row[columnIndex] == DBNull.Value)
                                row[columnIndex] = 0M;
                        }
                        if (flag)
                            row.Delete();
                    }
                    dtCross.Columns.Add("QT_SUM", typeof(decimal), expression);
                    this._flex재고6.SetCol("QT_SUM", "합계", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flex재고6.Cols.Frozen = this._flex재고6.Cols.Fixed;
                    this._flex재고6.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                    this._flex재고6.Redraw = false;
                    this._flex재고6.Redraw = true;
                    this._flex재고6.AllowCache = true;
                    for (int index = this._flex재고6.Cols.Fixed; index < this._flex재고6.Cols.Count; ++index)
                        this._flex재고6.SetCellStyle(0, index, this._flex재고6.Styles.Fixed);
                    break;
            }
            
        }


        private void InitEvent()
        {
            this.ctx품목1.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx품목1.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx품목2.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx품목2.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx품목3.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx품목3.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx품목4.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx품목4.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx품목5.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx품목5.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx품목6.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.ctx품목6.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);

            this.ctx품목1.CodeChanged += new EventHandler(this.ctx품목_CodeChanged);
            this.ctx품목2.CodeChanged += new EventHandler(this.ctx품목_CodeChanged);
            this.ctx품목3.CodeChanged += new EventHandler(this.ctx품목_CodeChanged);
            this.ctx품목4.CodeChanged += new EventHandler(this.ctx품목_CodeChanged);
            this.ctx품목5.CodeChanged += new EventHandler(this.ctx품목_CodeChanged);
            this.ctx품목6.CodeChanged += new EventHandler(this.ctx품목_CodeChanged);
        }
        private void ctx품목_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                BpCodeTextBox ctx = (BpCodeTextBox)sender;
                string str = "";
                if (!this.BeforeSearch())
                    return;
                if (ctx == ctx품목1)
                {
                    구역구분 = "1";
                    str = this.ctx품목1.CodeValue;
                }
                else if (ctx == ctx품목2)
                {
                    구역구분 = "2";
                    str = this.ctx품목2.CodeValue;
                }
                else if (ctx == ctx품목3)
                {
                    구역구분 = "3";
                    str = this.ctx품목3.CodeValue;
                }
                else if (ctx == ctx품목4)
                {
                    구역구분 = "4";
                    str = this.ctx품목4.CodeValue;
                }
                else if (ctx == ctx품목5)
                {
                    구역구분 = "5";
                    str = this.ctx품목5.CodeValue;
                }
                else if (ctx == ctx품목6)
                {
                    구역구분 = "6";
                    str = this.ctx품목6.CodeValue;
                }
                if (!공정재고조회(구역구분, str))
                {
                    if (!창고재고조회(구역구분, str))
                    {
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    }
                    else
                    {
                        this.ShowMessage("공정재공재고 정보가 없습니다.");
                    }
                }
                else
                {
                    if (!창고재고조회(구역구분, str))
                    {
                        this.ShowMessage("창고재고 정보가 없습니다.");
                    }
                }


            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 공정재고조회(string 구역구분, string str)
        {
            DataTable dt;
            dt = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                 this.cbo공장.SelectedValue.ToString(),
                                                 this.dtp작업지시기간.StartDateToString,
                                                 this.dtp작업지시기간.EndDateToString,
                                                 str });
            if (dt == null || dt.Rows.Count == 0)
            {
                switch (구역구분)
                {
                    case "1":
                        this._flex1.Binding = null;
                        break;
                    case "2":
                        this._flex2.Binding = null;
                        break;
                    case "3":
                        this._flex3.Binding = null;
                        break;
                    case "4":
                        this._flex4.Binding = null;
                        break;
                    case "5":
                        this._flex5.Binding = null;
                        break;
                    case "6":
                        this._flex6.Binding = null;
                        break;
                }
                return false;
            }
            else
            {
                switch (구역구분)
                {
                    case "1":
                        this._flex1.Binding = dt;
                        break;
                    case "2":
                        this._flex2.Binding = dt;
                        break;
                    case "3":
                        this._flex3.Binding = dt;
                        break;
                    case "4":
                        this._flex4.Binding = dt;
                        break;
                    case "5":
                        this._flex5.Binding = dt;
                        break;
                    case "6":
                        this._flex6.Binding = dt;
                        break;
                }
                return true;
            }
        }

        private bool 창고재고조회(string 구역구분, string str)
        {
            DataTable dt;
            dt = this._biz.Search2(new object[] { this.LoginInfo.CompanyCode,
                                                  this.cbo공장.SelectedValue.ToString(),
                                                  Global.MainFrame.GetStringToday,
                                                  str });
            if (dt == null || dt.Rows.Count == 0)
            {
                switch (구역구분)
                {
                    case "1":
                        this._flex재고1.Binding = null;
                        break;
                    case "2":
                        this._flex재고2.Binding = null;
                        break;
                    case "3":
                        this._flex재고3.Binding = null;
                        break;
                    case "4":
                        this._flex재고4.Binding = null;
                        break;
                    case "5":
                        this._flex재고5.Binding = null;
                        break;
                    case "6":
                        this._flex재고6.Binding = null;
                        break;
                }
                return false;
            }
            else
            {
                DataTable dataTable = this.CrossSetting(dt);
                dataTable.AcceptChanges();
                switch (구역구분)
                {
                    case "1":
                        this._flex재고1.Binding = dataTable;
                        break;
                    case "2":
                        this._flex재고2.Binding = dataTable;
                        break;
                    case "3":
                        this._flex재고3.Binding = dataTable;
                        break;
                    case "4":
                        this._flex재고4.Binding = dataTable;
                        break;
                    case "5":
                        this._flex재고5.Binding = dataTable;
                        break;
                    case "6":
                        this._flex재고6.Binding = dataTable;
                        break;
                }
                return true;
            }
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_MA_PITEM_SUB)
                {
                    if (!this.공장선택여부)
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
                        this.cbo공장.Focus();
                        e.QueryCancel = true;
                    }
                    else
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult == DialogResult.OK)
                {
                    BpCodeTextBox bpCodeTextBox = sender as BpCodeTextBox;
                    if (sender == null || e.HelpID != HelpID.P_MA_PITEM_SUB)
                        return;
                    bpCodeTextBox.CodeValue = e.CodeValue;
                    bpCodeTextBox.CodeName = e.CodeName;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }


        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;
            if (!this.공장선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });
                this.cbo공장.Focus();
                return false;
            }
            return Checker.IsValid(this.dtp작업지시기간, false, this.lbl작업지시기간.Text);
        }

        private void OnControlValidated(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is DatePicker datePicker) || datePicker.Text == "" || datePicker.IsValidated)
                    return;
                this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                datePicker.Text = "";
                datePicker.Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public bool 공장선택여부 => this.cbo공장.SelectedValue != null && !(this.cbo공장.SelectedValue.ToString() == "");

        private DataTable CrossSetting(DataTable dtData)
        {
            DataTable table1 = dtData.DefaultView.ToTable(true, "CD_SL", "NM_SL");
            for (int index = 0; index < table1.Rows.Count; ++index)
            {
                DataRow row = dtData.NewRow();
                row["CD_SL"] = table1.Rows[index]["CD_SL"];
                row["NUM"] = 0;
                row["CD_ITEM"] = "";
                row["NM_SL"] = table1.Rows[index]["NM_SL"];
                dtData.Rows.Add(row);
            }
            string Sort = "NUM, CD_ITEM, CD_SL";
            DataTable table2 = new DataView(dtData, "", Sort, DataViewRowState.CurrentRows).ToTable();
            Cross_Report crossReport = new Cross_Report();
            crossReport.GroupColumnName = "CD_ITEM";
            crossReport.HeaderColumnCode = "CD_SL";
            crossReport.HeaderColumnName = "CD_SL";
            crossReport.RepeatColumnName = "CD_SL";
            crossReport.HeaderRepeatColumnName = "QT_INV";
            crossReport.ItemColumnName = "CD_ITEM";
            foreach (DataColumn column in dtData.Columns)
            {
                if (column.ColumnName != "CD_ITEM" && column.ColumnName != "CD_SL" && column.ColumnName != "NM_SL" && column.ColumnName != "QT_INV")
                    crossReport.ItemColumnNameMore = column.ColumnName;
            }
            crossReport.AvgColumn = false;
            crossReport.SourceTable = table2.Copy();
            DataTable report = crossReport.CreateReport();
            this.InitGrid_New(report, new DataView(table1, "", "CD_SL", DataViewRowState.CurrentRows).ToTable());
            report.Rows.RemoveAt(0);
            return report;
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
            if (!base.SaveData() || !base.Verify()) return false;
            DataTable change1 = this._flex1.GetChanges();
            DataTable change2 = this._flex2.GetChanges();
            DataTable change3 = this._flex3.GetChanges();
            DataTable change4 = this._flex4.GetChanges();
            DataTable change5 = this._flex5.GetChanges();
            DataTable change6 = this._flex6.GetChanges();

            if (change1 == null && change2 == null && change3 == null && change4 == null && change5 == null && change6 == null)
                return true;

            if (!this._biz.Save(change1, change2, change3, change4, change5, change6))
                return false;

            this._flex1.AcceptChanges();
            this._flex2.AcceptChanges();
            this._flex3.AcceptChanges();
            this._flex4.AcceptChanges();
            this._flex5.AcceptChanges();
            this._flex6.AcceptChanges();

            return true;
        }
    }
}

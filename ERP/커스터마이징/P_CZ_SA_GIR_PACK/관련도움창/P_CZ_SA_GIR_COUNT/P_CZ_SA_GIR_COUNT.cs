using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.BpControls;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_GIR_COUNT : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_SA_GIR_COUNT_BIZ _biz = new P_CZ_SA_GIR_COUNT_BIZ();
        private bool _포장업무협조전;

        public P_CZ_SA_GIR_COUNT(bool 포장업무협조전)
        {
            InitializeComponent();

            this._포장업무협조전 = 포장업무협조전;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("DT_COMPLETE", "완료예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("QT_TOTAL", "전체건수", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex.Styles.Add("강조").ForeColor = Color.Red;
            this._flex.Styles.Add("강조").BackColor = Color.White;
            this._flex.Styles.Add("그외").ForeColor = Color.Black;
            this._flex.Styles.Add("그외").BackColor = Color.White;
        }

        private void InitEvent()
        {
            this._flex.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataTable dt = this._biz.협조전진행현황조회(this._포장업무협조전);

            this._flex.Binding = dt;
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;
                if (e.Row < this._flex.Rows.Fixed || e.Col < this._flex.Cols.Fixed) return;

                CellStyle cellStyle = this._flex.Rows[e.Row].Style;

                if (D.GetDecimal(this._flex[e.Row, "IDX"]) == 2)
                {
                    if (cellStyle == null || cellStyle.Name != "강조")
                        this._flex.Rows[e.Row].Style = this._flex.Styles["강조"];
                }
                else
                {
                    if (cellStyle == null || cellStyle.Name != "그외")
                        this._flex.Rows[e.Row].Style = this._flex.Styles["그외"];
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

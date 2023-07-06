using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_MA_PARTNER_EXCEL_SUB : Duzon.Common.Forms.CommonDialog
    {
        private DataTable dt = null;
        private bool bState = false;

        public P_CZ_MA_PARTNER_EXCEL_SUB()
        {
            InitializeComponent();
        }

        public P_CZ_MA_PARTNER_EXCEL_SUB(DataTable dt, bool bState)
            : this()
        {
            this.dt = dt;
            this.bState = bState;
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

            if (!this.bState)
            {
                this._flex.SetCol("ERRORLINE", "에러라인", 100, false);
                this._flex.SetCol("ERRORMSG", "설명", 700, false);
            }
            else
            {
                this._flex.SetCol("CD_COMPANY", "회사코드", 100, false);
                this._flex.SetCol("CD_PARTNER", "거래처코드", 700, false);
            }

            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.None, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this._flex.HelpClick += new EventHandler(this._flex_HelpClick);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.ColumnStyleSetting();
            this._flex.Binding = this.dt;
            this.GridColumnColorSetting();
        }

        private void ColumnStyleSetting()
        {
            this._flex.Styles.Add("SUB1").ForeColor = Color.Red;
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void _flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || this.bState) return;

                string @string = D.GetString(this._flex["ERRORMSG"]);
                Global.MainFrame.ShowDetailMessage("오류행 정보입니다.\n세부내역은 [더보기] 버튼을 눌러서 확인 하세요", @string);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void GridColumnColorSetting()
        {
            for (int @fixed = this._flex.Rows.Fixed; @fixed < this._flex.Rows.Count; ++@fixed)
                this._flex.SetCellStyle(@fixed, this._flex.Cols.Fixed, this._flex.Styles["SUB1"]);
        }
    }
}

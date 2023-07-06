using System;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;

namespace cz
{
    public partial class P_CZ_HR_PEVALU_DLG : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_HR_PEVALU_BIZ _biz = new P_CZ_HR_PEVALU_BIZ();
        private string 사업장 = null;
        private string 사번 = null;
        private string 이름 = null;


        public P_CZ_HR_PEVALU_DLG()
        {
            this.InitializeComponent();
        }

        public P_CZ_HR_PEVALU_DLG(string _사업장, string _사번, string _이름)
        {
            this.InitializeComponent();
            this.사업장 = _사업장;
            this.사번 = _사번;
            this.이름 = _이름;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex1.BeginSetting(1, 1, false);
            
            this._flex1.SetCol("NM_EDU", "교육명", 100, false);
            this._flex1.SetCol("DT_FROM", "시작일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex1.SetCol("DT_TO", "종료일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex1.SetCol("DC_EDU", "교육목적", 120, false);
            this._flex1.SetCol("NM_LOC", "교육장소", 120, false);
            this._flex1.SetCol("PT_EDU", "점수", 80, false, typeof(Decimal));
            this._flex1.SetCol("NM_PASS", "이수여부", 80);
            
            this._flex1.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex2.BeginSetting(1, 1, false);
            
            this._flex2.SetCol("SECTION", "구분", 80, false);
            this._flex2.SetCol("GRD", "포상구분", 80, false);
            this._flex2.SetCol("NM_PDI", "포상/징계명", 150, false);
            this._flex2.SetCol("DT_PDI", "포상/징계일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex2.SetCol("DC_PDI", "포상/징계내역", 80, false);
            this._flex2.SetCol("PAY", "포상/징계금액", 80, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex2.SetCol("DC_RMK", "비고", 120);
            
            this._flex2.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.dtp시작.Text = Global.MainFrame.GetStringDetailToday.Substring(0, 4) + "01";
            this.dtp종료.Text = Global.MainFrame.GetStringDetailToday.Substring(0, 6);
            this.txt사번.Text = this.사번;
            this.txt성명.Text = this.이름;
        }

        private void Search()
        {
            if (this._flex1.DataTable != null)
                this._flex1.DataTable.Rows.Clear();

            this._flex1.Binding = this._biz.Search1(new object[] { this.사업장,
                                                                   this.사번,
                                                                   this.dtp시작.Text,
                                                                   this.dtp종료.Text });

            this._flex2.Binding = this._biz.Search2(new object[] { this.사업장,
                                                                   this.사번,
                                                                   this.dtp시작.Text,
                                                                   this.dtp종료.Text });
            
            if (this._flex1.HasNormalRow)
                this._flex1.Focus();
            
            if (this._flex1.HasNormalRow || this._flex2.HasNormalRow)
                return;
            
            Global.MainFrame.ShowMessage("CZ_저장된 데이터가 없습니다.");
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this.Search();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
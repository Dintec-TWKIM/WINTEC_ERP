using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Data;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_MA_PITEM_COMPANY_TRANS_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_MA_PITEM_WINTEC_BIZ _biz = new P_CZ_MA_PITEM_WINTEC_BIZ();
        private string _CD_PLANT = string.Empty;
        private string _NM_PLANT = string.Empty;

        public P_CZ_MA_PITEM_COMPANY_TRANS_SUB() => this.InitializeComponent();

        public P_CZ_MA_PITEM_COMPANY_TRANS_SUB(object[] args)
        {
            this._CD_PLANT = D.GetString(args[0]);
            this._NM_PLANT = D.GetString(args[1]);
            this.InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this._txtCdCompany_F.Text = Global.MainFrame.LoginInfo.CompanyCode;
            this._txtCdPlant_F.Text = this._CD_PLANT + "(" + this._NM_PLANT + ")";
            this.BeforeSearch();

            this._btnSave.Click += new EventHandler(this._btnSave_Click);
            this.btn확인.Click += new EventHandler(this.btn확인_Click);
            this._btnDel.Click += new EventHandler(this._btnDel_Click);
            this._btnAdd.Click += new EventHandler(this._btnAdd_Click);
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            bool flag = false;
            this._flex.AcceptChanges();
            DataTable dataTable = this._flex.DataTable;
            if (dataTable != null)
                flag = this._biz.SaveCompanyTrans(dataTable);
            if (!flag)
                return;
            int num = (int)Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Yes;
            }
            catch (Exception ex)
            {
                Global.MainFrame.ShowErrorMessage((object)ex.Message, "전용코드등록 >> 확인버튼클릭");
            }
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex["CD_COMPANY_F"] = (object)Global.MainFrame.LoginInfo.CompanyCode;
                this._flex["CD_PLANT_F"] = (object)this._CD_PLANT;
                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
            }
            catch (Exception ex)
            {
                Global.MainFrame.ShowErrorMessage((object)ex.Message, "전용코드등록 >> 확인버튼클릭");
            }
        }

        private void _btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                {
                    int num = (int)Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                    this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                Global.MainFrame.ShowErrorMessage((object)ex.Message, "전용코드등록 >> 확인버튼클릭");
            }
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, true);
            this._flex.SetCol("FG_TRANS", "전송유무", 50, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_COMPANY_T", "회사", 80);
            this._flex.SetCol("NM_COMPANY_T", "회사명", 120);
            this._flex.SetCol("CD_PLANT_T", "공장", 80);
            this._flex.SetCol("NM_PLANT_T", "공장명", 120);
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void BeforeSearch()
        {
            try
            {
                this._flex.Binding = (object)this._biz.GetCompanyTrans(Global.MainFrame.LoginInfo.CompanyCode, this._CD_PLANT);
            }
            catch (Exception ex)
            {
                Global.MainFrame.ShowErrorMessage((object)ex.Message, "에러");
            }
        }
    }
}

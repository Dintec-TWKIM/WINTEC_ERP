using System;
using System.Data;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using System.Windows.Forms;
using Duzon.ERPU;
using Duzon.Windows.Print;
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_IVMNG_INVOICE : PageBase
    {
        P_CZ_SA_IVMNG_INVOICE_BIZ _biz = new P_CZ_SA_IVMNG_INVOICE_BIZ();

        public P_CZ_SA_IVMNG_INVOICE()
        {
            StartUp.Certify(this);
            InitializeComponent();
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

            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_INVOICE", "번호", 100);
            this._flex.SetCol("NO_IV", "매출번호", 100, true);
            this._flex.SetCol("NO_IO", "출고번호", 100, true);
            this._flex.SetCol("DC_COMPANY", "회사명", 300, true);
            this._flex.SetCol("DC_ADDRESS", "주소", 400, true);
            this._flex.SetCol("DC_TEL", "전화번호", 200, true);
            this._flex.SetCol("CD_NATION", "국가코드", 60, true);
            this._flex.SetCol("NM_NATION", "국가명", 100);
            this._flex.SetCol("NM_CITY", "도시", 100, true);
            this._flex.SetCol("CD_POSTAL", "우편번호", 100, true);

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex.SetDummyColumn(new string[] { "S" });
        }

        private void InitEvent()
        {
            this.txt번호.KeyDown += new KeyEventHandler(this.txt번호_KeyDown);

            this.btnDHL발송.Click += BtnDHL발송_Click;
            this.btnDHL픽업.Click += BtnDHL픽업_Click;
        }

        private void BtnDHL픽업_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        DataTable dt = this._flex.DataTable.Select("NO_INVOICE = '" + dr["NO_INVOICE"].ToString() + "'").CopyToDataTable();

                        string result = DHL_xml.dhlPickUp(dt);

                        if (result != "SUCCESS")
                        {
                            this.ShowMessage(dr["NO_INVOICE"].ToString() + " : " + result);
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

        private void BtnDHL발송_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach(DataRow dr in dataRowArray)
                    {
                        DataTable dt = this._flex.DataTable.Select("NO_INVOICE = '" + dr["NO_INVOICE"].ToString() + "'").CopyToDataTable();

                        string result = DHL_xml.DHLShipmentValidationService_D(dt);

                        if (result != "SUCCESS")
                        {
                            this.ShowMessage(dr["NO_INVOICE"].ToString() + " : " + result);
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

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     string.Empty });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void txt번호_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                if (string.IsNullOrEmpty(this.txt번호.Text))
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl번호.Text);
                    return;
                }

                if (this._flex.HasNormalRow && this._flex.DataTable.Select("NO_INVOICE = '" + this.txt번호.Text + "'").Length > 0)
                {
                    this.ShowMessage(공통메세지._의값이중복되었습니다, this.txt번호.Text);
                    return;
                }

                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.txt번호.Text });

                if (dt == null || dt.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }

                if (this.chkDHL발송자동선택.Checked == true)
                    dt.Rows[0]["S"] = "Y";

                this._flex.DataTable.ImportRow(dt.Rows[0]);
                this._flex.Row = this._flex.Rows.Count - 1;

                this.ToolBarDeleteButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.txt번호.Text = string.Empty;
                this.txt번호.Focus();
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;

                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.Focus();
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
                if (!this._flex.HasNormalRow) return;

                this._flex.GetDataRow(this._flex.Row).Delete();
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
            if (this._flex.IsDataChanged == false) return false;

            if (!_biz.Save(this._flex.GetChanges())) return false;

            this._flex.AcceptChanges();

            return true;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            ReportHelper reportHelper;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!this._flex.HasNormalRow) return;

                this._flex.DataTable.AcceptChanges();

                reportHelper = new ReportHelper("R_CZ_SA_IVMNG_INVOICE", "인보이스발송주소");
                reportHelper.SetDataTable(this._flex.DataTable, 1);
                reportHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

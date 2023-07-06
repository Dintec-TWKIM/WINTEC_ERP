using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_HR_PEVALU_OBJECT : PageBase
    {
        private P_CZ_HR_PEVALU_OBJECT_BIZ _biz = new P_CZ_HR_PEVALU_OBJECT_BIZ();
        private string 마감여부 = null;

        public P_CZ_HR_PEVALU_OBJECT()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flex };
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitOneGrid();
            this.InitGrid();
            this.InitEvent();
        }

        private void InitOneGrid()
        {
            this.oneGrid1.UseCustomLayout = false;
            this.oneGrid1.IsSearchControl = false;
            this.oneGrid1.InitCustomLayout();
            this.bppnl평가코드.IsNecessaryCondition = true;
            this.bppnl평가유형.IsNecessaryCondition = true;
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("CD_EVOTYPE", "순번", 40, false);
            this._flex.SetCol("CD_OTASK", "목표(과업)코드", 80, false);
            this._flex.SetCol("NM_OTASK", "목표(과업)명", 120, false);
            this._flex.SetCol("DC_DOBJECT", "세부목표", 200, true);
            this._flex.SetCol("CD_SCALE", "평가척도", 80, true);
            this._flex.SetCol("RT_WEIGHT", "가중치(%)", 80, true);
            this._flex.Cols["RT_WEIGHT"].Format = "##0.#0";
            this._flex.SetCodeHelpCol("CD_OTASK", "H_CZ_HR_PEVALU_OBJECT_SUB", ShowHelpEnum.Always, new string[] { "CD_OTASK", "NM_OTASK" }, new string[] { "CODE1", "NAME1" });

            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.bpc평가코드.QueryBefore += new BpQueryHandler(this.bpc평가코드_QueryBefore);
            this.bpc평가코드.QueryAfter += new BpQueryHandler(this.bpc평가코드_QueryAfter);

            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
        }

        protected override bool BeforeSearch()
        {
            if (this.bpc평가코드.IsEmpty())
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("평가코드"));
                this.bpc평가코드.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(D.GetString(this.cbo평가유형.SelectedValue)))
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.DD("평가유형"));
                this.cbo평가유형.Focus();
                return false;
            }
            else
                return base.BeforeSearch();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch())
                    return;

                string codeValue = this.bpc평가코드.CodeValue;
                string 평가유형 = this.cbo평가유형.SelectedValue.ToString();
                
                if (this._flex.DataTable != null)
                    this._flex.DataTable.Rows.Clear();
                
                this._flex.Binding = this._biz.Search(codeValue, 평가유형);
                
                if (!this._flex.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (this.마감여부 == "Y")
                {
                    this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
                }
                else
                {
                    DataTable dataTable = DBHelper.GetDataTable(@"SELECT DT_OFROM AS OFROM,
                                                                         DT_OTO AS OTO
                                                                  FROM HR_PEVALU_SCHE WITH(NOLOCK)
                                                                  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                 "AND CD_EVALU = '" + D.GetString(this.bpc평가코드.CodeValue) + "'");

                    string string1 = D.GetString(dataTable.Rows[0]["OFROM"]);
                    string string2 = D.GetString(dataTable.Rows[0]["OTO"]);
                    string getStringToday = Global.MainFrame.GetStringToday;
                    if (D.GetDecimal(getStringToday) < D.GetDecimal(string1))
                    {
                        this.ShowMessage("CZ_목표등록기간 전입니다.");
                    }
                    else if (D.GetDecimal(getStringToday) > D.GetDecimal(string2))
                    {
                        this.ShowMessage("CZ_목표등록기간이 만료되었습니다.");
                    }
                    else
                    {
                        this._flex.Rows.Add();
                        this._flex.Row = this._flex.Rows.Count - 1;
                        this._flex[this._flex.Row, "CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                        this._flex[this._flex.Row, "CD_EVALU"] = this.bpc평가코드.CodeValue;
                        this._flex[this._flex.Row, "CD_EVTYPE"] = this.cbo평가유형.SelectedValue.ToString();
                        this._flex[this._flex.Row, "CD_EVOTYPE"] = (this._flex.Rows.Count - 1);
                        this._flex[this._flex.Row, "NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
                        this._flex[this._flex.Row, "YN_APP"] = "N";
                    }
                }
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
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this._flex.HasNormalRow)
                    return;
                if (this.마감여부 == "Y")
                {
                    this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
                }
                else
                    this._flex.Rows.Remove(this._flex.Row);
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

                if (!this.DoContinue())
                    return;
                if (!this.IsChanged((string)null))
                {
                    this.ShowMessage("IK1_013");
                    this.ToolBarSaveButtonEnabled = false;
                }
                else if (this.MsgAndSave(false, false))
                {
                    this.ShowMessage("IK1_001");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool DoContinue()
        {
            if (this._flex.Editor != null)
                return this._flex.FinishEditing(false);

            if (this._flex.DataTable.Select("ISNULL(RT_WEIGHT, 0) = 0").Length > 0)
            {
                this.ShowMessage("가중치가 0인 행이 존재 합니다.");
                return false;
            }
            
            return true;
        }

        private bool IsChanged(string gubun)
        {
            try
            {
                if (gubun == null)
                    return this._flex.IsDataChanged;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool MsgAndSave(bool displayDialog, bool isExit)
        {
            try
            {
                if (!this.IsChanged((string)null))
                    return true;
                bool flag = false;
                if (!displayDialog)
                {
                    if (this.IsChanged((string)null))
                        flag = this.Save();
                    return flag;
                }
                if (isExit)
                {
                    switch (this.ShowMessage("QY3_002"))
                    {
                        case DialogResult.No:
                            return true;
                        case DialogResult.Cancel:
                            return false;
                    }
                }
                else if (this.ShowMessage("QY2_001") == DialogResult.No)
                    return true;
                Application.DoEvents();
                if (this.IsChanged((string)null))
                    flag = this.Save();
                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Save()
        {
            try
            {
                if (!this.IsChanged((string)null))
                    return true;
                if (!this._biz.Save(this._flex.GetChanges()))
                    return false;
                this._flex.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void bpc평가코드_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P00_CHILD_MODE = "인사평가 코드도움창";
                e.HelpParam.P61_CODE1 = "CD_EVALU AS CODE, NM_EVALU AS NAME, YM_EVALU AS YM, YN_CLOSE AS YNCLOSE ";
                e.HelpParam.P62_CODE2 = "HR_PEVALU_SCHE WITH(NOLOCK) WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'";
                e.HelpParam.P63_CODE3 = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void bpc평가코드_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.bpc평가코드.CodeValue = e.HelpReturn.Rows[0]["CODE"].ToString();
                this.bpc평가코드.CodeName = e.HelpReturn.Rows[0]["NAME"].ToString();
                this.마감여부 = e.HelpReturn.Rows[0]["YNCLOSE"].ToString();
                string str1 = string.Empty;
                DataTable dataTable = DBHelper.GetDataTable(@"SELECT CD_HCODE AS CODE,
                                                                     NM_HCODE AS NAME
                                                              FROM HR_PEVALU_CODE WITH(NOLOCK)
                                                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                            @"AND CD_FIELD = '100'
                                                              AND CD_EVALU = '" + e.HelpReturn.Rows[0]["CODE"].ToString() + "'" + Environment.NewLine +
                                                            @"AND YN_USE = 'Y'
                                                              AND YN_OBJECT = 'Y'");
                
                if (dataTable.Rows.Count == 0 || dataTable == null)
                {
                    this.ShowMessage("CZ_목표평가로 선택된 평가유형이 없습니다. 목표평가여부를 체크해주세요.");
                }
                else
                {
                    this.cbo평가유형.DataSource = dataTable;
                    this.cbo평가유형.DisplayMember = "NAME";
                    this.cbo평가유형.ValueMember = "CODE";
                }
                
                string str2 = string.Empty;
                this._flex.SetDataMap("CD_SCALE", DBHelper.GetDataTable(@"SELECT CD_HCODE AS CODE,
                                                                                 NM_HCODE AS NAME
                                                                          FROM HR_PEVALU_CODE WITH(NOLOCK) 
                                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                         "AND CD_EVALU = '" + e.HelpReturn.Rows[0]["CODE"].ToString() + "'" + Environment.NewLine +
                                                                         "AND CD_FIELD = '400'"), "CODE", "NAME");
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
                if (this.마감여부 == "Y")
                {
                    this.ShowMessage("CZ_마감된 @ 입니다.", this.DD("평가코드"));
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.UserParams = "목표등록 도움창;H_CZ_HR_PEVALU_OBJECT_SUB";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

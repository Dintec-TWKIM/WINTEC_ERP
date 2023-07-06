using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_LINK_MES_REG : PageBase
    {       
        private P_CZ_PR_LINK_MES_REG_BIZ _biz = new P_CZ_PR_LINK_MES_REG_BIZ();

        private bool Chk_PLANT
        {
            get
            {
                return !Checker.IsEmpty((Control)this.cbo공장, this.lbl공장.Text);
            }
        }

        private bool Chk_DT
        {
            get
            {
                return !Checker.IsEmpty((Control)this.dtp기간, this.lbl기간.Text);
            }
        }

        public P_CZ_PR_LINK_MES_REG()
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

            this.Grant.CanAdd = false;
            this.Grant.CanDelete = false;
            this.Grant.CanPrint = false;
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex };

            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_PLANT", "공장코드", false);
            this._flex.SetCol("NO_MES", "MES번호", 100);
            this._flex.SetCol("DT_WORK", "실적일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_EMP", "담당자코드", false);
            this._flex.SetCol("NM_KOR", "담당자", 100);
            this._flex.SetCol("NO_WO", "작업지시번호", 100);
            this._flex.SetCol("NO_WORK", "작업실적번호", 100);
            this._flex.SetCol("CD_ITEM", "품목코드", 100, 20);
            this._flex.SetCol("NM_ITEM", "품목명", 140);
            this._flex.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex.SetCol("STND_ITEM", "규격", 120);
            this._flex.SetCol("UNIT_MO", "단위", 40);
            this._flex.SetCol("QT_WORK", "실적수량", 75, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("YN_END", "처리여부", 60);
            this._flex.SetCol("CD_OP", "o.p.", false);
            this._flex.SetCol("CD_WC", "작업장코드", false);
            this._flex.SetCol("NM_WC", "작업장", 100);
            this._flex.SetCol("CD_WCOP", "공정코드", false);
            this._flex.SetCol("NM_OP", "공정", 100);
            this._flex.SetCol("CD_SL_IN", "입고창고", 80, true);
            this._flex.SetCol("NM_SL_IN", "입고창고명", 80);
            this._flex.SetCol("CD_EQUIP", "설비코드", 100);
            this._flex.SetCol("NM_EQUIP", "설비명", 100);
            this._flex.SetCol("DTS_INSERT", "입력일시", 150, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";

            this._flex.SetCodeHelpCol("CD_SL_IN", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL_IN", "NM_SL_IN" }, new string[] { "CD_SL", "NM_SL" });
            this._flex.VerifyNotNull = new string[] { "CD_PLANT", "NO_MES", "QT_WORK" };
            this._flex.VerifyCompare(this._flex.Cols["QT_WORK"], (object)0, OperatorEnum.Greater);
            this._flex.SetDummyColumn("S");

            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.SingleColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn연동적용.Click += new EventHandler(this.btn연동적용_Click);
            this.btn연동삭제.Click += new EventHandler(this.btn연동삭제_Click);
            this.btn표준경로일괄적용.Click += new EventHandler(this.btn표준경로일괄적용_Click);
            this.btn입고창고적용.Click += new EventHandler(this.btn입고창고적용_Click);
            this.btn이력조회.Click += new EventHandler(this.btn이력조회_Click);

            this.ctx품목From.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx품목From.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx품목To.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx입고창고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx작업장.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo공장, MA.GetCode("MA_PLANT_AUTH"));
            setControl.SetCombobox(this.cbo처리여부, MA.GetCode("YESNO"));

            this.cbo공장.SelectedValue = (object)this.LoginInfo.CdPlant;
            this.cbo처리여부.SelectedValue = (object)"N";
            this.dtp기간.StartDateToString = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp기간.EndDateToString = this.MainFrameInterface.GetStringToday;

            this._flex.SetDataMap("UNIT_MO", MA.GetCode(MF.코드.MASTER.단위), "CODE", "NAME");
            this._flex.SetDataMap("YN_END", MA.GetCode("YESNO"), "CODE", "NAME");

            this.SettingControl();

            this.oneGrid1.UseCustomLayout = true;
            this.bpPnl공장.IsNecessaryCondition = true;
            this.bpPnl기간.IsNecessaryCondition = true;
            this.bpPnl처리여부.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();

            UGrant ugrant = new UGrant();
            ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "ADMIN", this.btn연동적용, true);
            ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "ADMIN", this.btn연동삭제, true);
        }

        protected override bool BeforeSearch()
        {
            return base.BeforeSearch() && (this.Chk_PLANT && this.Chk_DT);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                     this.cbo공장.SelectedValue.ToString(),
                                                                     this.dtp기간.StartDateToString,
                                                                     this.dtp기간.EndDateToString,
                                                                     this.cbo처리여부.SelectedValue.ToString(),
                                                                     this.ctx품목From.CodeValue,
                                                                     this.ctx품목To.CodeValue,
                                                                     this.txt작업지시번호.Text,
                                                                     this.ctx작업장.CodeValue });

                if (!this._flex.HasNormalRow)
                    this.ShowMessage(PageResultMode.SearchNoData);
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
                if (!this.Verify() || !this.BeforeSave()) return;

                if (this.MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(PageResultMode.SaveGood);
                    this.OnToolBarSearchButtonClicked(null, null);
                }
                else
                {
                    this.ShowMessage(PageResultMode.SaveFail);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!this.Verify()) return false;

            DataTable changes = this._flex.DataTable.GetChanges();
            
            if (changes == null || changes.Rows.Count < 1 || !this._biz.Save(changes, "U"))
                return false;
            
            this._flex.AcceptChanges();
            
            return true;
        }

        private void btn연동적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                
                if (dataRowArray.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flex.DataTable.Select("S = 'Y' AND YN_END = 'Y'").Length > 0)
                {
                    this.ShowMessage("연동처리가 완료된 건이 있습니다.");
                }
                else
                {
                    DataTable dt = this._flex.DataTable.Clone();

                    foreach (DataRow dataRow in dataRowArray)
                        dt.Rows.Add(dataRow.ItemArray);
                    
                    if (this._biz.Save(dt, "I"))
                    {
                        this._flex.AcceptChanges();
                        this.ShowMessage(PageResultMode.SaveGood);
                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                    else
                    {
                        this.ShowMessage(PageResultMode.SaveFail);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn연동삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                
                if (dataRowArray.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flex.DataTable.Select("S = 'Y' AND YN_END = 'N'").Length > 0)
                {
                    this.ShowMessage("연동처리가 미완료된 건이 있습니다.");
                }
                else
                {
                    DataTable dt = this._flex.DataTable.Clone();

                    foreach (DataRow dataRow in dataRowArray)
                        dt.Rows.Add(dataRow.ItemArray);
                    
                    if (this._biz.Save(dt, "D"))
                    {
                        this._flex.AcceptChanges();
                        this.ShowMessage(PageResultMode.DeleteGood);
                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                    else
                    {
                        this.ShowMessage(PageResultMode.DeleteFail);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn표준경로일괄적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                DataRow[] drs = this._flex.DataTable.Select("S = 'Y'");
                
                if (drs.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flex.DataTable.Select("S = 'Y' AND YN_END = 'Y'").Length > 0)
                {
                    this.ShowMessage("연동처리가 완료된 건이 있습니다.");
                }
                else
                {
                    string str = Common.MultiString(drs, "CD_ITEM", "|");

                    DataTable dataTable = this._biz.SearchWCOP(new object[] { this.LoginInfo.CompanyCode,
                                                                              D.GetString(drs[0]["CD_PLANT"]),
                                                                              str });

                    if (dataTable.Rows.Count < 1)
                    {
                        this.ShowMessage("경로유형이 표준인 데이터가 없습니다.");
                    }
                    else
                    {
                        this._flex.Redraw = false;

                        foreach (DataRow dataRow in drs)
                        {
                            if (D.GetString(dataRow["YN_WO"]) == "Y" &&
                                D.GetString(dataRow["NO_WO"]) == string.Empty &&
                                (D.GetString(dataRow["CD_WC"]) == string.Empty &&
                                 D.GetString(dataRow["CD_OP"]) == string.Empty &&
                                 D.GetString(dataRow["CD_WCOP"]) == string.Empty))
                            {
                                DataRow[] dataRowArray = dataTable.Select("CD_ITEM = '" + D.GetString(dataRow["CD_ITEM"]) + "'");

                                if (dataRowArray.Length >= 1)
                                {
                                    dataRow["CD_WC"] = D.GetString(dataRowArray[0]["CD_WC"]);
                                    dataRow["NM_WC"] = D.GetString(dataRowArray[0]["NM_WC"]);
                                    dataRow["CD_OP"] = D.GetString(dataRowArray[0]["CD_OP"]);
                                    dataRow["CD_WCOP"] = D.GetString(dataRowArray[0]["CD_WCOP"]);
                                    dataRow["NM_OP"] = D.GetString(dataRowArray[0]["NM_OP"]);
                                }
                            }
                        }

                        this.OnToolBarSaveButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
            }
        }

        private void btn입고창고적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || string.IsNullOrEmpty(this.ctx입고창고.CodeValue))
                    return;

                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                
                if (dataRowArray.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flex.DataTable.Select("S = 'Y' AND YN_END = 'Y'").Length > 0)
                {
                    this.ShowMessage("연동처리가 완료된 건이 있습니다.");
                }
                else
                {
                    this._flex.Redraw = false;

                    foreach (DataRow dataRow in dataRowArray)
                    {
                        dataRow["CD_SL_IN"] = this.ctx입고창고.CodeValue;
                        dataRow["NM_SL_IN"] = this.ctx입고창고.CodeName;
                    }

                    this.OnToolBarSaveButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
            }
        }

        private void btn이력조회_Click(object sender, EventArgs e)
        {
            try
            {
                new P_CZ_PR_LINK_MES_REG_SUB(this.cbo공장.SelectedValue.ToString(),
                                             this.dtp기간.StartDateToString,
                                             this.dtp기간.EndDateToString).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnMES10_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Chk_DT || !new MES_Login(true).getMesOrcvH(this.dtp기간.StartDateToString, this.dtp기간.EndDateToString))
                    return;

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btnMES10.Text);
                this.OnToolBarSearchButtonClicked(null, null);
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
                switch (e.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB:
                    case HelpID.P_MA_SL_SUB:
                        if (!this.Chk_PLANT)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        break;
                    case HelpID.P_MA_WC_SUB:
                        if (!this.Chk_PLANT)
                        {
                            e.QueryCancel = true;
                            break;
                        }
                        e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue.ToString();
                        break;
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
                this.ctx품목To.SetCode(this.ctx품목From.CodeValue, this.ctx품목From.CodeName);
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
                if (!this._flex.HasNormalRow) return;

                if (D.GetString(this._flex["YN_END"]) == "Y")
                {
                    e.Cancel = true;
                }
                else
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "CD_SL_IN":
                            if (!this.Chk_PLANT)
                            {
                                e.Cancel = true;
                                break;
                            }
                            e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SettingControl()
        {
            if (BASIC.GetMAEXC("MES10_연동_사용유무") == "100")
            {
                this.btnMES10.Visible = true;
                this.btnMES10.Click += new EventHandler(this.btnMES10_Click);
            }
        }
    }
}

using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_IV_RPT : PageBase
    {
        P_CZ_SA_IV_RPT_BIZ _biz;

        public P_CZ_SA_IV_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_SA_IV_RPT_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL, this._flexR };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NO_IV", "계산서번호", 100);
            this._flexH.SetCol("NO_DOCU", "전표번호", 100);
            this._flexH.SetCol("LN_PARTNER", "매출처", 100);
            this._flexH.SetCol("DT_PROCESS", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_ACCT", "회계일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_CC", "C/C명", 100);
            this._flexH.SetCol("AM_SO", "수주금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SA_IV", "매출금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PO", "발주금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PU_IV", "매입금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_CHARGE", "매입비용", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            if (this.PageID == "P_CZ_SA_IV_BASE")
            {
                this._flexH.SetCol("AM_SA_IV_BASE", "매출원가", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_PO_PJT", "발주금액(프로젝트)", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("RT_PROFIT", "이윤율", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            }

            this._flexH.SetCol("ST_DOCU", "전표상태", 80);
            this._flexH.SetCol("NO_SO", "수주번호", false);

            this._flexH.SetDataMap("ST_DOCU", Global.MainFrame.GetComboDataCombine("N;FI_J000003"), "CODE", "NAME");

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (this.PageID == "P_CZ_SA_IV_BASE")
                this._flexH.SetExceptSumCol("RT_PROFIT");
            #endregion

            #region Left
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NO_LINE", "항번", 40);
            this._flexL.SetCol("NO_DSP", "순번", 40);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 120);
            this._flexL.SetCol("QT_IV", "매출수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UM_SO", "수주단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("UM_IV", "매출단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_SO", "수주금액", 110, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_IV", "매출금액", 110, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NO_SO", "수주번호", 80);
            this._flexL.SetCol("NO_SOLINE", "수주항번", 80);
            this._flexL.SetCol("CD_PJT", "프로젝트", 80);

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            
            this._flexL.SetExceptSumCol("UM_SO", "UM_IV");
            #endregion

            #region Right
            this._flexR.BeginSetting(1, 1, false);

            this._flexR.SetCol("NO_LINE", "항번", 40);
            this._flexR.SetCol("NO_DSP", "순번", 40);
            this._flexR.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flexR.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
            this._flexR.SetCol("CD_ITEM", "품목코드", 100);
            this._flexR.SetCol("NM_ITEM", "품목명", 120);
            this._flexR.SetCol("QT_IV", "매입수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexR.SetCol("UM_PO", "발주단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexR.SetCol("UM_IV", "매입단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexR.SetCol("AM_PO", "발주금액", 110, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexR.SetCol("AM_IV", "매입금액", 110, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexR.SetCol("AM_CHARGE", "매입비용", 110, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexR.SetCol("NO_PO", "발주번호", 80);
            this._flexR.SetCol("NO_POLINE", "발주항번", 80);

            this._flexR.SettingVersion = "0.0.0.1";
            this._flexR.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexR.SetExceptSumCol("UM_PO", "UM_IV");
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();
                
                this.cbo조회일자.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002" }, new string[] { this.DD("매출일자"), this.DD("회계일자"), this.DD("수금일자") });
                this.cbo조회일자.ValueMember = "CODE";
                this.cbo조회일자.DisplayMember = "NAME";
                
                this.dtp조회일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
                this.dtp조회일자.StartDate = Global.MainFrame.GetDateTimeToday().AddYears(-1);
                this.dtp조회일자.EndDate = Global.MainFrame.GetDateTimeToday();

                if (this.PageID == "P_CZ_SA_IV_RPT")
                {
                    this.cbo조회일자.SelectedValue = "000";
                    this.chk매출계정만표시.Checked = false;
                }
                else
                {
                    this.cbo조회일자.SelectedValue = "001";
                    this.chk매출계정만표시.Checked = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                if (this.PageID == "P_CZ_SA_IV_RPT")
                {
                    this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                          D.GetString(this.cbo조회일자.SelectedValue),
                                                                          this.dtp조회일자.StartDateToString,
                                                                          this.dtp조회일자.EndDateToString,
                                                                          this.ctxCC.CodeValue,
                                                                          this.bpc매출처.QueryWhereIn_Pipe,
                                                                          this.txt계산서번호.Text,
                                                                          (this.chk전표승인.Checked == true ? "Y" : "N"),
                                                                          (this.chk비용포함.Checked == true ? "Y" : "N"),
                                                                          (this.chk매출계정만표시.Checked == true ? "Y" : "N") });
                }
                else if (this.PageID == "P_CZ_SA_IV_BASE")
                {
                    this._flexH.Binding = this._biz.SearchBase(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                              D.GetString(this.cbo조회일자.SelectedValue),
                                                                              this.dtp조회일자.StartDateToString,
                                                                              this.dtp조회일자.EndDateToString,
                                                                              this.ctxCC.CodeValue,
                                                                              this.bpc매출처.QueryWhereIn_Pipe,
                                                                              this.txt계산서번호.Text,
                                                                              (this.chk전표승인.Checked == true ? "Y" : "N"),
                                                                              (this.chk비용포함.Checked == true ? "Y" : "N"),
                                                                              (this.chk매출계정만표시.Checked == true ? "Y" : "N") });
                }

                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dtL, dtR;
            string filter;

            try
            {
                filter = "NO_IV = '" + D.GetString(this._flexH["NO_IV"]) + "' AND CD_CC = '" + D.GetString(this._flexH["CD_CC"]) + "'";

                dtL = null;
                dtR = null;

                if (this._flexH.DetailQueryNeed == true)
                {
                    dtL = this._biz.SearchL(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                           D.GetString(this._flexH["NO_IV"]),
                                                           D.GetString(this._flexH["CD_CC"]),
                                                           (this.chk전표승인.Checked == true ? "Y" : "N"),
                                                           (this.chk비용포함.Checked == true ? "Y" : "N"),
                                                           (this.chk매출계정만표시.Checked == true ? "Y" : "N") });

                    dtR = this._biz.SearchR(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                           D.GetString(this._flexH["NO_IV"]),
                                                           D.GetString(this._flexH["CD_CC"]),
                                                           (this.chk전표승인.Checked == true ? "Y" : "N"),
                                                           (this.chk비용포함.Checked == true ? "Y" : "N") });
                }

                this._flexL.BindingAdd(dtL, filter);
                this._flexR.BindingAdd(dtR, filter);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.Forms.Help;
using Dintec;

namespace cz
{
    public partial class P_CZ_MA_HULL_DOM_RPT : PageBase
    {
        P_CZ_MA_HULL_DOM_RPT_BIZ _biz = new P_CZ_MA_HULL_DOM_RPT_BIZ();

        public P_CZ_MA_HULL_DOM_RPT()
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

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo국가.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000020");
            this.cbo국가.ValueMember = "CODE";
            this.cbo국가.DisplayMember = "NAME";

            this.cbo조회기간.DataSource = MA.GetCodeUser(new string[] { "000", "001" }, new string[] { "견적일자", "수주일자" });
            this.cbo조회기간.ValueMember = "CODE";
            this.cbo조회기간.DisplayMember = "NAME";

            this.dtp조회기간.StartDate = this.MainFrameInterface.GetDateTimeToday().AddYears(-1);
            this.dtp조회기간.EndDate = this.MainFrameInterface.GetDateTimeToday();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("YN_DOM", "국내기항여부", 100, false, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_IMO", "IMO번호", 60);
            this._flexH.SetCol("NM_VESSEL", "호선명", 150);
            this._flexH.SetCol("DC_SHIPBUILDER", "조선소", 150);
            this._flexH.SetCol("LN_PARTNER", "운항선사", 150);
            this._flexH.SetCol("NM_NATION", "국가", 80);
            this._flexH.SetCol("NM_TP_SHIP", "호선유형", 100);
            this._flexH.SetCol("DT_QTN", "최근거래일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_GIR", "최근협조전일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DTS_ETRYPT", "최근국내입항일자", 200);
            this._flexH.SetCol("NM_PORT", "입항항", 80);
            this._flexH.SetCol("NM_AGENT", "대리점", 80, true);
            this._flexH.SetCol("NM_PUR", "선식구매처", 80, true);
            this._flexH.SetCol("QT_QTN", "견적건수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_SO", "수주건수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("AM_QTN", "견적금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_STOCK", "재고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PO", "발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PROFIT", "이윤", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("RT_SO", "수주율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flexH.SetCol("RT_PROFIT", "이윤율", 100, false, typeof(decimal), FormatTpType.RATE);
            this._flexH.SetCol("DC_RMK_DOM", "비고", 150, true);

            this._flexH.ExtendLastCol = true;

            this._flexH.SetCodeHelpCol("NM_AGENT", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_AGENT", "NM_AGENT" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });
            this._flexH.SetCodeHelpCol("NM_PUR", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_PUR", "NM_PUR" }, new string[] { "CD_SYSDEF", "NM_SYSDEF" });

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NO_SO", "수주번호", 80);
            this._flexL.SetCol("NM_ITEMGRP", "품목유형", 80);
            this._flexL.SetCol("NO_DSP", "순번", 60);
            this._flexL.SetCol("CD_ITEM", "품목코드", 80);
            this._flexL.SetCol("NM_ITEM", "품목명", 80);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 80);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 80);
            this._flexL.SetCol("NM_MAIN_CATEGORY", "의뢰구분(중)", 80);
            this._flexL.SetCol("NM_SUB_CATEGORY", "의뢰구분(소)", 80);
            this._flexL.SetCol("UNIT_SO", "단위", 60);
            this._flexL.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NM_EXCH", "통화명", 80);
            this._flexL.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("UM_EX_P", "매입단가(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("UM_KR_P", "매입단가(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_P", "매입금액(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_KR_P", "매입금액(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("UM_EX_S", "매출단가(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("UM_KR_S", "매출단가(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_S", "매출금액(외화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_KR_S", "매출금액(원화)", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexL.SetExceptSumCol("RT_EXCH", "UM_EX_P", "UM_KR_P", "UM_EX_S", "UM_KR_S");
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexH_BeforeCodeHelp);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
        }

        private void _flexH_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                switch (grid.Cols[e.Col].Name)
                {
                    case "NM_AGENT":
                        e.Parameter.P41_CD_FIELD1 = "CZ_MA00020";
                        break;
                    case "NM_PUR":
                        e.Parameter.P41_CD_FIELD1 = "CZ_MA00019";
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt = null;
            string key, filter;

            try
            {
                key = D.GetString(this._flexH["NO_IMO"]);
                filter = "NO_IMO ='" + key + "'";

                if (this._flexH.DetailQueryNeed)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   key,
                                                               this.cbo조회기간.SelectedValue.ToString(),
                                                               this.dtp조회기간.StartDateToString,
                                                               this.dtp조회기간.EndDateToString });
                }

                this._flexL.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.cbo조회기간.SelectedValue.ToString(),
                                                                      this.dtp조회기간.StartDateToString,
                                                                      this.dtp조회기간.EndDateToString,
                                                                      this.ctx호선.CodeValue,
                                                                      this.ctx운항선사.CodeValue,
                                                                      this.cbo국가.SelectedValue,
                                                                      this.txt조선소.Text,
                                                                      (this.chk국내기항선박.Checked == true ? "Y" : "N") });

                if (!this._flexH.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flexH.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flexH.GetChanges())) return false;

            this._flexH.AcceptChanges();

            return true;
        }
    }
}

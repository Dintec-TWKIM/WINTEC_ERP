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
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_IV_BASE : PageBase
    {
        P_CZ_SA_IV_BASE_BIZ _biz = new P_CZ_SA_IV_BASE_BIZ();

        public P_CZ_SA_IV_BASE()
        {
			StartUp.Certify(this);
			InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("ST_DOCU", "전표상태", 80);
            this._flexH.SetCol("QT_IV", "계산서건수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("NO_PROJECT", "프로젝트", 100);
            this._flexH.SetCol("NO_IV", "계산서번호", 100);
            this._flexH.SetCol("NO_DOCU", "전표번호", 100);
            this._flexH.SetCol("DT_PROCESS", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_ACCT", "회계일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("LN_PARTNER", "매출처", 100);
            this._flexH.SetCol("CD_CC_IV", "C/C(매출)", false);
            this._flexH.SetCol("NM_CC", "C/C명", 100);
            this._flexH.SetCol("AM_SO", "수주금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SA_IV", "매출금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SA_CHARGE", "매출비용", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PO", "발주금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PU_IV", "매입금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PU_CHARGE", "매입비용", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_STOCK", "재고금액", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SA_TOTAL", "매출합계", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PU_TOTAL", "매입합계", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PROFIT", "이윤", 150, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("RT_PROFIT", "이윤율", 60, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SetDataMap("ST_DOCU", Global.MainFrame.GetComboDataCombine("N;FI_J000003"), "CODE", "NAME");

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.SetExceptSumCol("RT_PROFIT");
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo조회기간.DataSource = MA.GetCodeUser(new string[] { "000", "001" }, new string[] { this.DD("매출일자"), this.DD("회계일자") });
            this.cbo조회기간.ValueMember = "CODE";
            this.cbo조회기간.DisplayMember = "NAME";

            this.cbo조회기간.SelectedValue = "001";

            this.dtp조회기간.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp조회기간.EndDate = Global.MainFrame.GetDateTimeToday();

            this.chk전표승인.Checked = true;
            this.chk매출계정만표시.Checked = true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      D.GetString(this.cbo조회기간.SelectedValue),
                                                                      this.dtp조회기간.StartDateToString,
                                                                      this.dtp조회기간.EndDateToString,
                                                                      this.ctxCC.CodeValue,
                                                                      this.ctx매출처.CodeValue,
                                                                      this.txt프로젝트.Text,
                                                                      this.txt계산서번호.Text,
                                                                      (this.chk전표승인.Checked ? "Y" : "N"),
                                                                      (this.chk매출계정만표시.Checked ? "Y" : "N") });

                if (!this._flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

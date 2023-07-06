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
    public partial class P_CZ_SA_IV_PJT_RPT : PageBase
    {
        P_CZ_SA_IV_PJT_RPT_BIZ _biz = new P_CZ_SA_IV_PJT_RPT_BIZ();

        public P_CZ_SA_IV_PJT_RPT()
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
            this.MainGrids = new FlexGrid[] { this._flex전체 };
            this._flex전체.DetailGrids = new FlexGrid[] { this._flex매입, this._flex매출 };

            #region 전체
            this._flex전체.BeginSetting(1, 1, false);

            this._flex전체.SetCol("NO_PROJECT", "프로젝트번호", 100);
            this._flex전체.SetCol("NM_PROJECT", "프로젝트명", false);
            this._flex전체.SetCol("NM_SO", "수주유형", 100);
            this._flex전체.SetCol("CD_PARTNER", "매출처코드", false);
            this._flex전체.SetCol("LN_PARTNER", "매출처", 100);
            this._flex전체.SetCol("NM_VESSEL", "호선", 100);
            this._flex전체.SetCol("CD_SALEGRP", "영업그룹코드", false);
            this._flex전체.SetCol("NM_SALEGRP", "영업그룹", 100);
            this._flex전체.SetCol("CD_CC", "코스트센터코드", false);
            this._flex전체.SetCol("NM_CC", "코스트센터", 100);
            this._flex전체.SetCol("NO_EMP", "담당자", false);
            this._flex전체.SetCol("NM_KOR", "담당자명", 100);
            this._flex전체.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex전체.SetCol("DT_CHANGE", "계약일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex전체.SetCol("DT_ACCT_SA", "회계일자(매출)", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex전체.SetCol("DT_ACCT_PU", "회계일자(매입)", 110, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex전체.SetCol("DT_ACCT_DIFF", "소요일", 60);
            this._flex전체.SetCol("AM_SO", "수주금액", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("AM_PO", "발주금액", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("AM_SA_IV", "매출금액", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("AM_PU_IV", "매입금액", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("AM_SALES", "매출금액(전표)", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("AM_PURCHASE", "매입금액(전표)", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("AM_PROFIT", "부대수익", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("AM_CHARGE", "부대비용", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("AM_TOT_PROFIT", "이윤", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex전체.SetCol("RT_PROFIT", "이윤율", 110, false, typeof(decimal), FormatTpType.RATE);
            this._flex전체.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex전체.SetCol("QT_OUT", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex전체.SetCol("STA_OUT", "출고상태", 80);
            this._flex전체.SetCol("DC_RMK_TEXT2", "물류비고", 100);
            this._flex전체.SetCol("DC_RMK1", "커미션비고", 100);
            this._flex전체.SetCol("DC_RMK_CONTRACT", "수통비고", 100, true);
            this._flex전체.SetCol("DC_RMK2", "매출비고", 100);
            this._flex전체.SetCol("CD_RMK", "미출고비고", 100);
            this._flex전체.SetCol("DC_RMK", "미출고상세비고", 100);
            this._flex전체.SetCol("DC_RMK_PJT", "프로젝트비고", 100, true);

            string query = @"SELECT CD_SYSDEF,
                                    NM_SYSDEF 
                             FROM MA_CODEDTL WITH(NOLOCK) 
                             WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                           @"AND CD_FIELD = 'CZ_MA00010' 
                             AND CD_FLAG1 = '19'";

            DataTable dt = DBHelper.GetDataTable(query);
            dt.Rows.InsertAt(dt.NewRow(), 0);

            this._flex전체.SetDataMap("CD_RMK", dt, "CD_SYSDEF", "NM_SYSDEF");

            this._flex전체.ExtendLastCol = true;

            this._flex전체.SettingVersion = "0.0.0.1";
            this._flex전체.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex전체.SetExceptSumCol("RT_PROFIT");
            #endregion

            #region 매입
            this._flex매입.BeginSetting(1, 1, false);

            this._flex매입.SetCol("NO_DOCU", "전표번호", 100);
            this._flex매입.SetCol("NO_DOLINE", "전표라인", 40);
            this._flex매입.SetCol("DT_ACCT", "회계일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex매입.SetCol("NM_DRCR", "차대", 40);
            this._flex매입.SetCol("CD_ACCT", "계정코드", 80);
            this._flex매입.SetCol("NM_ACCT", "계정명", 100);
            this._flex매입.SetCol("NM_NOTE", "적요", 100);
            this._flex매입.SetCol("AM_CR", "대변", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex매입.SetCol("AM_DR", "차변", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex매입.SetCol("NM_ST_DOCU", "전표상태", 40);
            this._flex매입.SetCol("NO_MDOCU", "관리번호", 80);

            this._flex매입.SettingVersion = "0.0.0.1";
            this._flex매입.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 매출
            this._flex매출.BeginSetting(1, 1, false);

            this._flex매출.SetCol("NO_DOCU", "전표번호", 100);
            this._flex매출.SetCol("NO_DOLINE", "전표라인", 40);
            this._flex매출.SetCol("DT_ACCT", "회계일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex매출.SetCol("NM_DRCR", "차대", 40);
            this._flex매출.SetCol("CD_ACCT", "계정코드", 80);
            this._flex매출.SetCol("NM_ACCT", "계정명", 100);
            this._flex매출.SetCol("NM_NOTE", "적요", 100);
            this._flex매출.SetCol("AM_CR", "대변", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex매출.SetCol("AM_DR", "차변", 110, false, typeof(decimal), FormatTpType.MONEY);
            this._flex매출.SetCol("NM_ST_DOCU", "전표상태", 40);
            this._flex매출.SetCol("NO_MDOCU", "관리번호", 80);

            this._flex매출.SettingVersion = "0.0.0.1";
            this._flex매출.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this._flex전체.AfterRowChange += new RangeEventHandler(this._flex전체_AfterRowChange);

            this._flex매입.DoubleClick += new EventHandler(this._flex_DoubleClick);
            this._flex매출.DoubleClick += new EventHandler(this._flex_DoubleClick);
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.dtp계약일자.StartDate = this.MainFrameInterface.GetDateTimeToday().AddMonths(-6);
                this.dtp계약일자.EndDate = this.MainFrameInterface.GetDateTimeToday();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this._flex전체.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         this.dtp계약일자.StartDateToString,
                                                                         this.dtp계약일자.EndDateToString,
                                                                         this.ctx매출처.CodeValue,
                                                                         this.ctx담당자.CodeValue,
                                                                         this.txt프로젝트번호.Text,
                                                                         this.bpc코스트센터.QueryWhereIn_Pipe,
                                                                         this.cur소요일.DecimalValue,
                                                                         (this.chk무상공급제외.Checked == true ? "Y" : "N"),
                                                                         (this.chk수주마감제외.Checked == true ? "Y" : "N") });

                if (!this._flex전체.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
            if (this._flex전체.IsDataChanged == false ) return false;

            DataTable dt전체 = this._flex전체.GetChanges();

            if (!_biz.SaveData(dt전체)) return false;

            this._flex전체.AcceptChanges();

            return true;
        }

        private void _flex전체_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt매입, dt매출;
            string filter;

            try
            {
                filter = "CD_PJT = '" + D.GetString(this._flex전체["NO_PROJECT"]) + "'";

                dt매입 = null;
                dt매출 = null;

                if (this._flex전체.DetailQueryNeed == true)
                {
                    dt매입 = this._biz.Search매입(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 D.GetString(this._flex전체["NO_PROJECT"]) });

                    dt매출 = this._biz.Search매출(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 D.GetString(this._flex전체["NO_PROJECT"]) });
                }

                this._flex매입.BindingAdd(dt매입, filter);
                this._flex매출.BindingAdd(dt매출, filter);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                if (string.IsNullOrEmpty(D.GetString(grid["NO_DOCU"]))) return;

                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(grid["NO_DOCU"]),
                                                                                                                                 "1",
                                                                                                                                 D.GetString(grid["CD_PC"]),
                                                                                                                                 Global.MainFrame.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

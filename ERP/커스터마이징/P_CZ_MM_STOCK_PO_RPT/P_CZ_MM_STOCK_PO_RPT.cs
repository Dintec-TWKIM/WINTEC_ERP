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
using Duzon.ERPU.MF;
using Duzon.Common.BpControls;
using Duzon.Common.Forms.Help;
using Dintec;
using Duzon.ERPU.Grant;

namespace cz
{
    public partial class P_CZ_MM_STOCK_PO_RPT : PageBase
    {
        P_CZ_MM_STOCK_PO_RPT_BIZ _biz = new P_CZ_MM_STOCK_PO_RPT_BIZ();
        DataTable dt기자재발주, dt기자재재고금액, dt현대발주;
        private bool 내부접속여부, isEzcode;

        public P_CZ_MM_STOCK_PO_RPT()
        {
			StartUp.Certify(this);
            this.내부접속여부 = Util.CertifyIP();
            InitializeComponent();

            DataTable dt = DBHelper.GetDataTable(@"SELECT * 
                                                   FROM CZ_MA_CODEDTL WITH(NOLOCK)
                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                 @"AND CD_FIELD = 'CZ_MA00030'
                                                   AND YN_USE = 'Y'
                                                   AND CD_SYSDEF = '" + Global.MainFrame.LoginInfo.EmployeeNo + "'");

            if (dt != null && dt.Rows.Count > 0)
                this.isEzcode = true;
            else
                this.isEzcode = false;
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

            webBrowserExt1.IsWebBrowserContextMenuEnabled = false;
            webBrowserExt1.ScriptErrorsSuppressed = true;
            webBrowserExt1.AllowWebBrowserDrop = false;
        }

        private void InitGrid()
        {
            #region 기자재재고

            #region Header
            this._flex기자재재고H.BeginSetting(1, 1, false);

            this._flex기자재재고H.SetCol("NM_CLS_M", "중분류", 100);
            this._flex기자재재고H.SetCol("AM_REMAIN", "가용금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex기자재재고H.SetCol("AM_PO", "예상발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex기자재재고H.SettingVersion = "0.0.0.1";
            this._flex기자재재고H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flex기자재재고L.BeginSetting(1, 1, false);

            this._flex기자재재고L.SetCol("NM_CLS_M", "중분류", 100);
            this._flex기자재재고L.SetCol("CD_ITEM", "재고코드", 100);
            this._flex기자재재고L.SetCol("NM_UNIT_IM", "단위", 100);
            this._flex기자재재고L.SetCol("QT_FILE_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_ITEM_QTN", "견적수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_FILE_SO", "수주건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_ITEM_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_NOT_IN", "미입고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_BOOK", "출고예약", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_HOLD", "입고예약", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_SO_MONTH", "수주수량(1개월)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_SO_MONTH1", "예상수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_REMAIN", "가용수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_REMAIN1", "가용발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("QT_NEED_ROUND", "필요수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex기자재재고L.SetCol("STAND_PRC", "재고단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex기자재재고L.SetCol("AM_PO", "예상발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex기자재재고L.SetCol("TP_UM", "단가유형", 100);

            this._flex기자재재고L.SettingVersion = "0.0.0.1";
            this._flex기자재재고L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex기자재재고L.SetExceptSumCol("STAND_PRC");
            #endregion

            #endregion

            #region 현대재고

            #region Header
            this._flex현대재고H.BeginSetting(1, 1, false);

            this._flex현대재고H.SetCol("NM_CLS_L", "대분류", 100);
            this._flex현대재고H.SetCol("NM_CLS_S", "소분류", 100);
            this._flex현대재고H.SetCol("AM_PO", "예상발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex현대재고H.SettingVersion = "0.0.0.1";
            this._flex현대재고H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flex현대재고L.BeginSetting(1, 1, false);

            this._flex현대재고L.SetCol("NM_CLS_L", "대분류", 100);
            this._flex현대재고L.SetCol("NM_CLS_M", "중분류", false);
            this._flex현대재고L.SetCol("NM_CLS_S1", "소분류", 100);
            this._flex현대재고L.SetCol("NO_IMO", "IMO번호", 100);
            this._flex현대재고L.SetCol("NO_ENGINE", "엔진번호", 100);
            this._flex현대재고L.SetCol("CD_SPEC", "U코드", 100);

            if (this.isEzcode && this.내부접속여부 && Certify.IsLive())
                this._flex현대재고L.SetCol("EZCODE", "EZ코드", 100);

            this._flex현대재고L.SetCol("NO_PLATE", "품목코드", 100);
            this._flex현대재고L.SetCol("NM_PLATE", "품목명", 100);
            this._flex현대재고L.SetCol("CD_ITEM", "재고코드", 100);
            this._flex현대재고L.SetCol("QT_QTN", "견적건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_NOT_IN", "미입고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_BOOK", "출고예약", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_HOLD", "입고예약", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_SO_MONTH", "수주수량(1개월)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_SO_MONTH1", "예상수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_REMAIN", "가용수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("QT_NEED_ROUND", "필요수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex현대재고L.SetCol("UM_KR_P_AVG", "평균매입단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex현대재고L.SetCol("AM_PO", "예상발주금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex현대재고L.SettingVersion = "0.0.0.2";
            this._flex현대재고L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex현대재고L.SetExceptSumCol("UM_KR_P_AVG");
            #endregion

            #endregion
        }

        private void InitEvent()
        {
            this.ctx중분류.QueryBefore += new BpQueryHandler(this.ctx중분류_QueryBefore);

            this._flex기자재재고H.AfterRowChange += new RangeEventHandler(this._flex기자재재고H_AfterRowChange);
            this._flex현대재고H.AfterRowChange += new RangeEventHandler(this._flex현대재고H_AfterRowChange);

            this.btn기자재조회.Click += new EventHandler(this.btn기자재조회_Click);
            this.btn현대조회.Click += new EventHandler(this.btn현대조회_Click);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                if (this.tpgMain.SelectedIndex == 0)
                {
                    webBrowserExt1.Navigate("https://app.powerbi.com/view?r=eyJrIjoiMGJhMjQ4ZmQtYmU0Yi00YmMyLTg5ZGItNGQzNTY5YmRmY2VlIiwidCI6IjA5YWEyYTU3LWY0ZjktNGU0Ni05YzJiLTllODkzYzJlYzI3MiIsImMiOjEwfQ%3D%3D");
                }
                else if (this.tpgMain.SelectedIndex == 1)
                {
                    this.dt기자재발주 = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         this.cur빈도수.DecimalValue,
                                                                         this.ctx중분류.CodeValue });

                    this.dt기자재재고금액 = this._biz.Search3(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             this.ctx중분류.CodeValue });
                    
                    this.btn기자재조회_Click(null, null);
                }
                else if (this.tpgMain.SelectedIndex == 2)
                {
                    this.dt현대발주 = this._biz.Search2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                       this.cur호선수.DecimalValue,
                                                                       this.txt엔진모델.Text });

                    if (!this.isEzcode || !this.내부접속여부 || !Certify.IsLive())
                        this.dt현대발주.Columns.Remove("EZCODE");

                    this.btn현대조회_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex기자재재고H_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this._flex기자재재고L.RowFilter = "NM_CLS_M = '" + this._flex기자재재고H["NM_CLS_M"] + "'";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex현대재고H_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this._flex현대재고L.RowFilter = "NM_CLS_L = '" + this._flex현대재고H["NM_CLS_L"] + "' AND NM_CLS_S = '" + this._flex현대재고H["NM_CLS_S"] + "'";
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn기자재조회_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                foreach (DataRow dr in this.dt기자재발주.Rows)
                {
                    dr["QT_SO_MONTH1"] = (D.GetDecimal(dr["QT_SO_MONTH"]) * this.cur기자재개월수.DecimalValue);
                    dr["QT_NEED"] = D.GetDecimal(dr["QT_SO_MONTH1"]) - D.GetDecimal(dr["QT_REMAIN"]);
                    dr["QT_NEED_ROUND"] = Decimal.Round(D.GetDecimal(dr["QT_NEED"]), 0, MidpointRounding.AwayFromZero);
                    dr["AM_PO"] = Decimal.Round(D.GetDecimal(dr["QT_NEED_ROUND"]) * D.GetDecimal(dr["STAND_PRC"]), 0, MidpointRounding.AwayFromZero);
                }

                dataRowArray = this.dt기자재발주.Select("QT_NEED >= 0");

                DataTable dt = new DataTable();
                dt.Columns.Add("NM_CLS_M");
                dt.Columns.Add("AM_REMAIN", typeof(decimal));
                dt.Columns.Add("AM_PO", typeof(decimal));

                foreach (DataRow dr in this.dt기자재재고금액.Rows)
                {
                    DataRow dr1 = dt.NewRow();

                    dr1["NM_CLS_M"] = dr["NM_CLS_M"];
                    dr1["AM_REMAIN"] = dr["AM_REMAIN"];
                    dr1["AM_PO"] = dataRowArray.Where(x => x["CLS_M"].ToString() == dr["CLS_M"].ToString())
                                               .Sum(x => D.GetDecimal(x["AM_PO"]));

                    dt.Rows.Add(dr1);
                }

                this._flex기자재재고H.Binding = dt;
                this._flex기자재재고L.Binding = dataRowArray.CopyToDataTable();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn현대조회_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                foreach (DataRow dr in this.dt현대발주.Rows)
                {
                    dr["QT_SO_MONTH1"] = (D.GetDecimal(dr["QT_SO_MONTH"]) * this.cur현대개월수.DecimalValue);
                    dr["QT_NEED"] = D.GetDecimal(dr["QT_SO_MONTH1"]) - D.GetDecimal(dr["QT_REMAIN"]);
                    dr["QT_NEED_ROUND"] = Decimal.Round(D.GetDecimal(dr["QT_NEED"]), 0, MidpointRounding.AwayFromZero);
                    dr["AM_PO"] = Decimal.Round(D.GetDecimal(dr["QT_NEED_ROUND"]) * D.GetDecimal(dr["UM_KR_P_AVG"]), 0, MidpointRounding.AwayFromZero);
                }

                dataRowArray = this.dt현대발주.Select("QT_NEED >= 0");

                DataTable dt = new DataTable();
                dt.Columns.Add("NM_CLS_L");
                dt.Columns.Add("NM_CLS_S");
                dt.Columns.Add("AM_PO", typeof(decimal));

                foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray, new string[] { "NM_CLS_L", "NM_CLS_S" }, true).Rows)
                {
                    DataRow dr1 = dt.NewRow();

                    dr1["NM_CLS_L"] = dr["NM_CLS_L"];
                    dr1["NM_CLS_S"] = dr["NM_CLS_S"];
                    dr1["AM_PO"] = dataRowArray.Where(x => x["NM_CLS_L"].ToString() == dr["NM_CLS_L"].ToString() &&
                                                           x["NM_CLS_S"].ToString() == dr["NM_CLS_S"].ToString())
                                               .Sum(x => D.GetDecimal(x["AM_PO"]));

                    dt.Rows.Add(dr1);
                }

                this._flex현대재고H.Binding = dt;
                this._flex현대재고L.Binding = dataRowArray.CopyToDataTable();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx중분류_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

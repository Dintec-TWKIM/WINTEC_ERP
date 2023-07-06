using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_CLAIM_SUB : CommonDialog
    {
        #region 생성자 & 초기화
        P_CZ_SA_CLAIM_SUB_BIZ _biz = new P_CZ_SA_CLAIM_SUB_BIZ();

        public DataRow GetDataHeader
        {
            get
            {
                return this._flexH.GetDataRow(this._flexH.Row);
            }
        }

        public DataRow[] GetDataLine
        {
            get
            {
                return this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
            }
        }

        public bool 수량일괄적용
        {
            get
            {
                return this.chk수량일괄적용.Checked;
            }
        }

        public P_CZ_SA_CLAIM_SUB()
        {
            InitializeComponent();
        }

        public P_CZ_SA_CLAIM_SUB(string 수주번호)
        {
            InitializeComponent();

            this.txt수주번호S.Text = 수주번호;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.DoubleClick += new EventHandler(this._flexH_DoubleClick);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.pnl검색.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.pnl검색.InitCustomLayout();

            this.dtp수주일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
            this.dtp수주일자.EndDateToString = Global.MainFrame.GetStringToday;
        }

        private void InitGrid()
        {
            #region Master Grid
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NO_SO", "수주번호", 100);
            this._flexH.SetCol("LN_PARTNER", "매출처", 150);
            this._flexH.SetCol("NM_VESSEL", "호선명", 150);
            this._flexH.SetCol("NM_SALES_EMP", "담당자", 60);
            this._flexH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Detail Grid
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("NO_DSP", "순번", 40);
            this._flexL.SetCol("NM_SUBJECT", "주제", false);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 120);
            this._flexL.SetCol("UNIT_SO", "단위", 40);
            this._flexL.SetCol("QT_SO", "수량", 40, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NM_CLS_ITEM", "계정구분", 100);
            this._flexL.SetCol("LN_PARTNER", "매입처", 120);
            this._flexL.SetCol("UM_PO", "발주단가", 80, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexL.SetCol("AM_PO", "발주금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("UM_STOCK", "재고단가", 80, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexL.SetCol("AM_STOCK", "재고금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("UM_SO", "수주단가", 80, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexL.SetCol("AM_SO", "수주금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("LT", "발주납기", 60);
            this._flexH.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flexL.SetDummyColumn("S");

            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }
        #endregion
        
        #region 버튼 이벤트
        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (Checker.IsValid(dtp수주일자, true, lbl수주일자.Text) == false) return;

                DataTable dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this.txt수주번호S.Text,
                                                               this.dtp수주일자.StartDateToString,
                                                               this.dtp수주일자.EndDateToString });
                this._flexH.Binding = dt;

                if (!this._flexH.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                if (this.GetDataLine.Length > 0)
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;

                string Key = _flexH["NO_SO"].ToString();

                dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                           Global.MainFrame.LoginInfo.Language,
                                                           Key });

                this._flexL.Binding = dt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;
                if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;

                this.btn적용_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion
    }
}

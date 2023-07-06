using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_QTNSO_RPT : PageBase
    {
        #region 생성자 & 전역변수
        P_CZ_SA_QTNSO_RPT_BIZ _biz;

        private enum 현황유형
        {
            견적현황파일 = 0,
            견적현황품목 = 1,
            수주현황파일 = 2,
            수주현황품목 = 3
        }

        public P_CZ_SA_QTNSO_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            this._biz = new P_CZ_SA_QTNSO_RPT_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.cbo일자유형.DataSource = MA.GetCodeUser(new string[] { "000", "001", "002" }, new string[] { this.DD("접수일자"), this.DD("견적일자"), this.DD("수주일자") });
                this.cbo일자유형.ValueMember = "CODE";
                this.cbo일자유형.DisplayMember = "NAME";

                this.dtp일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
                this.dtp일자.EndDate = Global.MainFrame.GetDateTimeToday();

                this.cbo회사.DataSource = this.GetComboDataCombine("N;CZ_SA00029");
                this.cbo회사.ValueMember = "CODE";
                this.cbo회사.DisplayMember = "NAME";

                this.cbo회사.SelectedValueChanged += new EventHandler(this.cbo회사_SelectedValueChanged);
                this.cbo회사_SelectedValueChanged(null, null);

                this.cbo금액유형.DataSource = MA.GetCodeUser(new string[] { "", "", "000", "001", "002", "003" }, new string[] { this.DD("금액검색"), "--------", this.DD("매입금액"), this.DD("견적금액"), this.DD("수주금액"), this.DD("이윤") });
                this.cbo금액유형.ValueMember = "CODE";
                this.cbo금액유형.DisplayMember = "NAME";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            try
            {
                this.MainGrids = new FlexGrid[] { this._견적현황파일H, this._견적현황파일L,
                                                  this._견적현황품목H,
                                                  this._수주현황파일H, this._수주현황파일L,
                                                  this._수주현황품목H };

                this._견적현황파일H.DetailGrids = new FlexGrid[] { this._견적현황파일L };
                this._수주현황파일H.DetailGrids = new FlexGrid[] { this._수주현황파일L };

                #region 견적현황(파일)
                this.SetHeaderGrid(this._견적현황파일H);
                this.SetLineGrid(this._견적현황파일L);
                #endregion

                #region 견적현황(품목)
                this.SetLineGrid(this._견적현황품목H);
                #endregion

                #region 수주현황(파일)
                this.SetHeaderGrid(this._수주현황파일H);
                this.SetLineGrid(this._수주현황파일L);
                #endregion

                #region 수주현황(품목)
                this.SetLineGrid(this._수주현황품목H);
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SetHeaderGrid(FlexGrid grid)
        {
            try
            {
                grid.BeginSetting(1, 1, false);

                grid.SetCol("FILE_NO", "파일번호", 80);
                grid.SetCol("BUYER_NAME", "매출처", 120);
                grid.SetCol("VESSEL_NAME", "호선", 120);
                grid.SetCol("INQ_NO", "문의번호", 80);
                grid.SetCol("ORDER_NO", "주문번호", 80);
                grid.SetCol("INQ_DATE", "접수일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                grid.SetCol("QTN_DATE", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                grid.SetCol("SO_DATE", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                grid.SetCol("CURRENCY", "통화명", 60);
                grid.SetCol("EX_RATE", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
                grid.SetCol("POR_AMOUNT", "매입금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("QTN_EX", "견적외화금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("QTN_AMOUNT", "견적금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("SO_EX", "수주외화금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("SO_AMOUNT", "수주금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("MAR_AMOUNT", "이윤", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("MAR_RATE", "이윤율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

                grid.SettingVersion = "0.0.0.1";
                grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                grid.SetExceptSumCol("EX_RATE", "MAR_RATE");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void SetLineGrid(FlexGrid grid)
        {
            try
            {
                grid.BeginSetting(1, 1, false);

                if (grid.Name == this._견적현황품목H.Name || grid.Name == this._수주현황품목H.Name)
                {
                    grid.SetCol("FILE_NO", "파일번호", 80);
                    grid.SetCol("BUYER_NAME", "매출처", 120);
                    grid.SetCol("VESSEL_NAME", "호선", 120);
                    grid.SetCol("INQ_NO", "문의번호", 80);
                    grid.SetCol("ORDER_NO", "주문번호", 80);
                }
                
                grid.SetCol("SUBJECT_NAME", "주제", 120);
                grid.SetCol("ITEM_CODE", "품목코드", 120);
                grid.SetCol("DESCRIPTION", "품목명", 120);
                grid.SetCol("QTY", "수량", 80);
                grid.SetCol("UNIT", "단위", 80);
                grid.SetCol("P_PRICE", "매입단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("P_AMOUNT", "매입외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("PW_AMOUNT", "매입원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("Q_PRICE", "매출단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("Q_AMOUNT", "매출외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                grid.SetCol("QW_AMOUNT", "매출원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                grid.SetCol("SUPPLIER_NAME", "매입처", 120);
                grid.SetCol("S_CLASS", "재고사용", 40, false, CheckTypeEnum.Y_N);
                grid.SetCol("STOCK_CODE", "재고코드", 80);
                grid.SetCol("LT", "L/T", 40);
                grid.SetCol("INV_NO", "계산서번호", 80);

                grid.SettingVersion = "0.0.0.1";
                grid.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this.ctx매출처.QueryBefore += new BpQueryHandler(this.QueryBefore);
            this.ctx매입처.QueryBefore += new BpQueryHandler(this.QueryBefore);
            this.ctx호선.QueryBefore += new BpQueryHandler(this.QueryBefore);

            this._견적현황파일H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._수주현황파일H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
        }
        #endregion

        #region 메인버튼이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                DataTable dt = null;

                dt = _biz.Search(new object[] { this.cbo회사.SelectedValue,
                                                this.tabControl.SelectedIndex,
                                                this.cbo일자유형.SelectedValue,
                                                this.dtp일자.StartDateToString,
                                                this.dtp일자.EndDateToString,
                                                this.cbo금액유형.SelectedValue,
                                                this.cur금액From.DecimalValue,
                                                this.cur금액To.DecimalValue,
                                                this.ctx매출처.CodeValue,
                                                this.ctx매입처.CodeValue,
                                                this.ctx호선.CodeValue,
                                                this.cbo담당자.SelectedValue,
                                                this.cbo지역코드.SelectedValue,
                                                this.cbo국가코드.SelectedValue,
                                                this.txt파일번호.Text.ToUpper(),
                                                this.txt주제.Text.ToUpper(),
                                                this.txt품목코드.Text.ToUpper(),
                                                this.txt품목명.Text.ToUpper(),
                                                this.txt문의번호.Text.ToUpper(),
                                                this.txt주문번호.Text.ToUpper() });

                switch (this.tabControl.SelectedIndex)
                {
                    case (int)현황유형.견적현황파일:
                        this._견적현황파일H.Binding = dt;
                        break;
                    case (int)현황유형.견적현황품목:
                        this._견적현황품목H.Binding = dt;
                        break;
                    case (int)현황유형.수주현황파일:
                        this._수주현황파일H.Binding = dt;
                        break;
                    case (int)현황유형.수주현황품목:
                        this._수주현황품목H.Binding = dt;
                        break;
                }

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
                    return;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드이벤트
        private void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, filter;
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                dt = null;
                key = grid["FILE_NO"].ToString();
                filter = "FILE_NO = '" + key + "'";

                if (grid.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { this.cbo회사.SelectedValue,
                                                               this.tabControl.SelectedIndex, 
                                                               key,
                                                               this.ctx매입처.CodeValue,
                                                               this.txt주제.Text,
                                                               this.txt품목코드.Text,
                                                               this.txt품목명.Text });
                }

                grid.DetailGrids[0].BindingAdd(dt, filter);
                grid.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 기타이벤트
        private void cbo회사_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.cbo담당자.DataSource = this._biz.담당자정보(D.GetString(this.cbo회사.SelectedValue));
                this.cbo담당자.ValueMember = "CODE";
                this.cbo담당자.DisplayMember = "NAME";

                this.cbo지역코드.DataSource = this._biz.지역코드정보(D.GetString(this.cbo회사.SelectedValue));
                this.cbo지역코드.ValueMember = "CODE";
                this.cbo지역코드.DisplayMember = "NAME";

                this.cbo국가코드.DataSource = this._biz.국가코드정보(D.GetString(this.cbo회사.SelectedValue));
                this.cbo국가코드.ValueMember = "CODE";
                this.cbo국가코드.DisplayMember = "NAME";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void QueryBefore(object sender, BpQueryArgs e)
        {
            string tablePrefix;

            try
            {
                e.HelpParam.P01_CD_COMPANY = D.GetString(this.cbo회사.SelectedValue);
                e.HelpParam.P21_FG_MODULE = "Y";

                if (D.GetString(this.cbo회사.SelectedValue) == "002")
                    tablePrefix = "R";
                else
                    tablePrefix = "T";

                if (e.ControlName == this.ctx매출처.Name || e.ControlName == this.ctx매입처.Name)
                {
                    e.HelpParam.P34_CD_MNG = @"SELECT COMP_CODE AS CODE,
                                                      COMP_NAME AS NAME" + Environment.NewLine +
                                              "FROM " + tablePrefix + "_BC002" + Environment.NewLine +
                                              "WHERE (NVL('{0}', '') = '' OR COMP_CODE LIKE '%{0}%' OR COMP_NAME LIKE '%{0}%')" + Environment.NewLine +
                                              "ORDER BY COMP_CODE";
                }
                else if (e.ControlName == this.ctx호선.Name)
                {
                    e.HelpParam.P34_CD_MNG = @"SELECT HULL_NO AS CODE,
                                                      VESSEL_NAME AS NAME" + Environment.NewLine +
                                              "FROM " + tablePrefix + "_BP001" + Environment.NewLine +
                                              "WHERE (NVL('{0}', '') = '' OR HULL_NO LIKE '%{0}%' OR VESSEL_NAME LIKE '%{0}%')" + Environment.NewLine +
                                              "ORDER BY HULL_NO";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion
    }
}

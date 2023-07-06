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
using Duzon.Common.BpControls;
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_VSSL_ETRYND : PageBase
    {
        P_CZ_SA_VSSL_ETRYND_BIZ _biz = new P_CZ_SA_VSSL_ETRYND_BIZ();

        public P_CZ_SA_VSSL_ETRYND()
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

            this.dtp입항일자.StartDate = this.MainFrameInterface.GetDateTimeToday();
            this.dtp입항일자.EndDate = this.MainFrameInterface.GetDateTimeToday().AddMonths(3);
        }

        private void InitGrid()
        {
            #region 입출항정보
            
            this._flex입출항정보H.DetailGrids = new FlexGrid[] { this._flex입출항정보L };

            #region Header
            this._flex입출항정보H.BeginSetting(1, 1, false);

            this._flex입출항정보H.SetCol("CD_PRTAG", "항구청코드", false);
            this._flex입출항정보H.SetCol("NM_PRTAG", "항구청명", 100);
            this._flex입출항정보H.SetCol("DT_ETRYPT_YEAR", "입항년도", 100);
            this._flex입출항정보H.SetCol("CNT_ETRYPT", "입항횟수", 100);
            this._flex입출항정보H.SetCol("CLSGN", "호출부호", 100);
            this._flex입출항정보H.SetCol("NM_VSSL", "선박명", 100);
            this._flex입출항정보H.SetCol("NM_VSSL_NLTY", "선박국가명", 100);
            this._flex입출항정보H.SetCol("NM_VSSL_KND", "선박종류명", 100);
            this._flex입출항정보H.SetCol("CD_ETRYPT_PURPS", "입항목적코드", false);
            this._flex입출항정보H.SetCol("NM_ETRYPT_PURPS", "입항목적명", 100);
            this._flex입출항정보H.SetCol("CD_FRST_DPMPRT_NAT_PRT", "최초출항지국가항구코드", false);
            this._flex입출항정보H.SetCol("NM_FRST_DPMPRT_PRT", "최초출항지항구명", 120);
            this._flex입출항정보H.SetCol("CD_PRVS_DPMPRT_NAT_PRT", "전출항지국가항구코드", false);
            this._flex입출항정보H.SetCol("NM_PRVS_DPMPRT_PRT", "전출항지항구명", 120);
            this._flex입출항정보H.SetCol("CD_NX_INPT_NAT_PRT", "차항지국가항구코드", false);
            this._flex입출항정보H.SetCol("NM_NX_INPT_PRT", "차출항지항구명", 120);
            this._flex입출항정보H.SetCol("CD_DSTN_NAT_PRT", "목적지국가항구코드", false);
            this._flex입출항정보H.SetCol("NM_DSTN_PRT", "목적지항구명", 120);
            this._flex입출항정보H.SetCol("DTS_ETRYPT", "입항", 200, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex입출항정보H.SetCol("DTS_TKOFF", "출항", 200, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex입출항정보H.Cols["DTS_ETRYPT"].Format = "####/##/## ##:##:##";
            this._flex입출항정보H.Cols["DTS_TKOFF"].Format = "####/##/## ##:##:##";

            this._flex입출항정보H.SettingVersion = "0.0.0.1";
            this._flex입출항정보H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flex입출항정보L.BeginSetting(1, 1, false);

            this._flex입출항정보L.SetCol("NM_ETRYND", "입출항구분명", 100);
            this._flex입출항정보L.SetCol("DTS_ETRYPT", "입항일시", 200, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex입출항정보L.SetCol("DTS_TKOFF", "출항일시", 200, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex입출항정보L.SetCol("NM_IBOBPRT", "내외항구분명", 100);
            this._flex입출항정보L.SetCol("CD_LAIDUPFCLTY", "계선시설코드", false);
            this._flex입출항정보L.SetCol("CD_LAIDUPFCLTY_SUB", "계선시설서브코드", false);
            this._flex입출항정보L.SetCol("NM_LAIDUPFCLTY", "계선시설명", 100);
            this._flex입출항정보L.SetCol("YN_PILTG", "도선유무", 80, false, CheckTypeEnum.Y_N);
            this._flex입출항정보L.SetCol("CD_LDADNG_FRGHT_CL", "화물명세코드", false);
            this._flex입출항정보L.SetCol("NM_LDADNG_FRGHT_CL", "화물명세", 150);
            this._flex입출항정보L.SetCol("TON_LDADNG", "적재톤수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex입출항정보L.SetCol("TON_TRNPDT", "환적톤수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex입출항정보L.SetCol("TON_LANDNG_FRGHT", "양하화물톤", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex입출항정보L.SetCol("TON_LD_FRGHT", "적하화물톤", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex입출항정보L.SetCol("GRTG", "총톤수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex입출항정보L.SetCol("INTRL_GRTG", "국제총톤수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex입출항정보L.SetCol("NM_SATMN_ENTRPS", "신고업체명", 100);
            this._flex입출항정보L.SetCol("CNT_CREW", "선원수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex입출항정보L.SetCol("CNT_KORAN_CREW", "한국인선원수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex입출항정보L.SetCol("CNT_FRGNR_CREW", "외국인선원수", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flex입출항정보L.Cols["DTS_ETRYPT"].Format = "####/##/## ##:##:##";
            this._flex입출항정보L.Cols["DTS_TKOFF"].Format = "####/##/## ##:##:##";

            this._flex입출항정보L.SettingVersion = "0.0.0.1";
            this._flex입출항정보L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #endregion

            #region 호선별정보
            this._flex호선별정보H.DetailGrids = new FlexGrid[] { this._flex호선별정보L };

            #region Header
            this._flex호선별정보H.BeginSetting(1, 1, false);

            this._flex호선별정보H.SetCol("CLSGN", "호출부호", 100);
            this._flex호선별정보H.SetCol("NM_VSSL", "선박명", 100);
            this._flex호선별정보H.SetCol("NM_VSSL_NLTY", "선박국가명", 100);
            this._flex호선별정보H.SetCol("NM_VSSL_KND", "선박종류명", 100);
            this._flex호선별정보H.SetCol("GRTG", "총톤수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex호선별정보H.SetCol("CNT_CREW", "선원수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex호선별정보H.SetCol("QT_ETRYPT", "국내기항횟수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex호선별정보H.SetCol("DTS_ETRYPT", "최근입항일자", 200, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex호선별정보H.SetCol("NO_IMO", "IMO번호", 100);
            this._flex호선별정보H.SetCol("QT_INQ", "문의건수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex호선별정보H.SetCol("DT_INQ", "최근문의일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex호선별정보H.Cols["DTS_ETRYPT"].Format = "####/##/## ##:##:##";

            this._flex호선별정보H.SettingVersion = "0.0.0.1";
            this._flex호선별정보H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flex호선별정보L.BeginSetting(1, 1, false);

            this._flex호선별정보L.SetCol("NM_PRTAG", "항구청명", 100);
            this._flex호선별정보L.SetCol("DT_ETRYPT_YEAR", "입항년도", 100);
            this._flex호선별정보L.SetCol("CNT_ETRYPT", "입항횟수", 100);
            this._flex호선별정보L.SetCol("NM_ETRYPT_PURPS", "입항목적명", 100);
            this._flex호선별정보L.SetCol("NM_FRST_DPMPRT_PRT", "최초출항지항구명", 100);
            this._flex호선별정보L.SetCol("NM_PRVS_DPMPRT_PRT", "전출항지항구명", 100);
            this._flex호선별정보L.SetCol("NM_NX_INPT_PRT", "차출항지항구명", 100);
            this._flex호선별정보L.SetCol("NM_DSTN_PRT", "목적지항구명", 100);
            this._flex호선별정보L.SetCol("DTS_ETRYPT", "입항", 200, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex호선별정보L.SetCol("DTS_TKOFF", "출항", 200, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flex호선별정보L.Cols["DTS_ETRYPT"].Format = "####/##/## ##:##:##";
            this._flex호선별정보L.Cols["DTS_TKOFF"].Format = "####/##/## ##:##:##";

            this._flex호선별정보L.SettingVersion = "0.0.0.1";
            this._flex호선별정보L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #endregion
        }

        private void InitEvent()
        {
            this._flex입출항정보H.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flex호선별정보H.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);

            this.bpc항만청.QueryBefore += new BpQueryHandler(this.bpc항만청_QueryBefore);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            DataTable dt = null;
            FlexGrid grid = null;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                switch (this.tabControl1.SelectedIndex)
                {
                    case 0:
                        dt = this._biz.Search(new object[] { this.bpc항만청.QueryWhereIn_Pipe,    
                                                             this.dtp입항일자.StartDateToString,
                                                             this.dtp입항일자.EndDateToString,
                                                             this.bpc호선.QueryWhereIn_Pipe,
                                                             this.txt선박명.Text,
                                                             this.txtCallSign.Text });
                        grid = this._flex입출항정보H;
                        break;
                    case 1:
                        dt = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                              this.bpc항만청.QueryWhereIn_Pipe,    
                                                              this.dtp입항일자.StartDateToString,
                                                              this.dtp입항일자.EndDateToString,
                                                              this.bpc호선.QueryWhereIn_Pipe,
                                                              this.txt선박명.Text,
                                                              this.txtCallSign.Text });
                        grid = this._flex호선별정보H;
                        break;
                }

                grid.Binding = dt;

                if (!grid.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string 항구청코드, 입항년도, 입항횟수, 호출부호, 선박명, filter;

            try
            {
                dt = null;

                switch (this.tabControl1.SelectedIndex)
                {
                    case 0:
                        항구청코드 = this._flex입출항정보H["CD_PRTAG"].ToString();
                        입항년도 = this._flex입출항정보H["DT_ETRYPT_YEAR"].ToString();
                        입항횟수 = this._flex입출항정보H["CNT_ETRYPT"].ToString();
                        호출부호 = this._flex입출항정보H["CLSGN"].ToString();
                        선박명 = this._flex입출항정보H["NM_VSSL"].ToString();

                        filter = "CD_PRTAG = '" + 항구청코드 + 
                                 "' AND DT_ETRYPT_YEAR = '" + 입항년도 + 
                                 "' AND CNT_ETRYPT = '" + 입항횟수 + 
                                 "' AND CLSGN = '" + 호출부호 + 
                                 "' AND NM_VSSL = '" + 선박명 + "'";

                        if (this._flex입출항정보H.DetailQueryNeed == true)
                        {
                            dt = this._biz.SearchDetail(new object[] { 항구청코드,
                                                                       입항년도,
                                                                       입항횟수,
                                                                       호출부호,
                                                                       선박명 });
                        }

                        this._flex입출항정보L.BindingAdd(dt, filter);
                        break;
                    case 1:
                        호출부호 = this._flex호선별정보H["CLSGN"].ToString();
                        선박명 = this._flex호선별정보H["NM_VSSL"].ToString();

                        filter = "CLSGN = '" + 호출부호 +
                                 "' AND NM_VSSL = '" + 선박명 + "'";

                        if (this._flex호선별정보H.DetailQueryNeed == true)
                        {
                            dt = this._biz.SearchDetail1(new object[] { 호출부호,
                                                                        선박명,
                                                                        this.bpc항만청.QueryWhereIn_Pipe,    
                                                                        this.dtp입항일자.StartDateToString,
                                                                        this.dtp입항일자.EndDateToString   });
                        }

                        this._flex호선별정보L.BindingAdd(dt, filter);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void bpc항만청_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = "CZ_MA00025";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

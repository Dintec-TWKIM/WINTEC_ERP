using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using Duzon.ERPU;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Forms.Help.Forms;
using Duzon.Common.Util;

using Duzon.BizOn.Windows.Forms;




namespace cz
{
    [UserHelpDescription("기본도움창", "기본도움창을 가져옵니다.", "CODE", "NAME")]

    public partial class H_CZ_HELP_CAR_PRICE : HelpBase, IHelp
    {
        public H_CZ_HELP_CAR_PRICE()
        {
            InitializeComponent();
        }

        public H_CZ_HELP_CAR_PRICE(HelpParam helpParam)
            : base(helpParam)
        {
            InitializeComponent();
            base.SetIHelp = this as IHelp;
            InitEvent();
            InitGrid();

            SetDefault(base.Get타이틀명, flex, btn확인, btn검색, btn취소, txt검색);//페이지 컨트롤들을 해당페이지와 연결
            helpParam.QueryAction = QueryAction.RealTime;//조회할때마다 서버에서 가져옴
        }

        #region -> 초기화

        private void InitEvent()
        {
            txt검색.KeyDown += new KeyEventHandler(검색_KeyDown);
        }

        /// <summary>
        ///그리드 초기화 
        /// </summary>
        private void InitGrid()
        {
            ArrayList list = new ArrayList();
            //list.Add(new object[] { "CODE", "코드", 80 });
            //list.Add(new object[] { "NAME", "코드명", 170 });


            switch (base.GetHelpID)
            {
                case "H_CAR_EQUIP_SUB":
                    list.Add(new object[] { "CAR_NO", "차량번호", 80 });
                    list.Add(new object[] { "NM_CAR", "차량종류", 170 });
                    list.Add(new object[] { "NM_KOR", "운전자", 170 });
                    list.Add(new object[] { "NM_COMPANY", "상호", 170 });
                    break;


                case "H_CZ_DELIVERY_FARE_SUB":
                    list.Add(new object[] { "TY_FARE", "코드", 80 });

                    list.Add(new object[] { "UM_FARE", "운송비", 170 });
                    list.Add(new object[] { "IF_ROUT", "운송내역", 80 });
                    //포멧지정시 씀
                    //flex.Cols["UM_FARE"].DataType = typeof(int);
                    //flex.Cols["UM_FARE"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    // flex.Cols["UM_FARE"].Format = "#,###";

                    break;

                case "H_CZ_CAR_EMERGE":
                    list.Add(new object[] { "NO_EMP", "운전자", 80 });
                    list.Add(new object[] { "CD_PARTNER", "상호", 170 });
                    list.Add(new object[] { "LN_PARTNER", "상호명", 170 });
                    list.Add(new object[] { "PWHP", "파워텔", 170 });
                    list.Add(new object[] { "NO_HP", "휴대폰번호", 170 });
                    list.Add(new object[] { "NO_TEL", "전화번호", 170 });
                    list.Add(new object[] { "PWHP", "파워텔", 170 });
                    list.Add(new object[] { "CD_COMPANY", "회사", 170 });

                    break;

                case "H_CZ_CAR_EMP":
                    list.Add(new object[] { "NO_EMP", "운전자", 80 });
                    list.Add(new object[] { "NM_KOR", "운전자코드", 170 });
                    list.Add(new object[] { "NM_COMPANY", "회사명", 170 });

                    break;


                case "H_CZ_CAR_PRICE_S":
                    list.Add(new object[] { "NM_SHIPPER", "화주", 200 });
                    list.Add(new object[] { "NM_REAL_LOADING_PLACE", "실상차지", 150 });
                    list.Add(new object[] { "NM_LOADING_PLACE", "상차지", 150 });
                    list.Add(new object[] { "NM_ALIGHT_PLACE", "하차지", 150 });
                    list.Add(new object[] { "REQ_PRICE", "청구단가", 170 });
                    list.Add(new object[] { "REQ_UNIT_PRICE", "청구 대당단가", 170 });
                    list.Add(new object[] { "PROVIDE_PRICE", "지급단가", 170 });
                    list.Add(new object[] { "UNIT_PRICE", "지급 대당단가", 170 });
                    list.Add(new object[] { "ESTIMATE_PRICE", "계근비", 170 });
                    //list.Add(new object[] { "FREIGHT_CHARGE_A", "운임A", 170 });
                    //list.Add(new object[] { "FREIGHT_CHARGE_B", "운임B", 170 });
                    list.Add(new object[] { "STANDARD_PLACE", "상하차기준", 170 });

                    //flex.Cols["NM_SHIPPER"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    //flex.Cols["NM_REAL_LOADING_PLACE"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    //flex.Cols["NM_LOADING_PLACE"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                    //flex.Cols["NM_ALIGHT_PLACE"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                    //flex.Cols["REQ_PRICE"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    //flex.Cols["PROVIDE_PRICE"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    //flex.Cols["UNIT_PRICE"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                    


                    break;

                default:
                    //설정
                    list.Add(new object[] { "CODE", "코드", 80 });
                    list.Add(new object[] { "NAME", "코드명", 170 });
                    break;
            }

            flex.Cols.Frozen = 1;
            base.InitGrid(flex, list);  //그리드는 PageBase의 그리드와 다르다   

        }

        #endregion

        #region -> 컨트롤이벤트

        void 검색_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    //Enter키 누를시 재조회
                    case Keys.Enter:
                        SetAfterSearch(RefreshSearch());
                        flex.Focus();
                        break;
                    //Esc키 누를시 폼닫음
                    case Keys.Escape:
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region-> IHelp 멤버

        /// <summary>
        /// DB 연결부분
        /// </summary>
        /// <param name="검색"></param>
        /// <returns></returns>
        /// 
        public override DataTable OnCodeSearch(string content)
        {
            //object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode, D.GetString(base._HelpParam.get_P09_CD_PLANT()), D.GetString(this.txt_SEARCH.Text), this.bp_DT_FROM.Text, this.bp_DT_TO.Text, D.GetString(this.bpc구매그룹.get_QueryWhereIn_Pipe()), D.GetString(this.bpc담당자.get_QueryWhereIn_Pipe()) };
            //object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode, "1000", D.GetString(this.txt검색.Text) };
            //return DBHelper.GetDataTable("UP_H_PU_HELP_BG_NO_PO", objArray);

            string _CompanyCode = MA.Login.회사코드;

            DataTable dt = null;
            switch (base.GetHelpID)
            {
                case "H_CAR_EQUIP_SUB":
                    //dt = DBHelper.GetDataTable("UP_CZ_DELIVERY_MAN_SELECT", new object[] { _CompanyCode, "1000",GetListParam[0], "" });
                    dt = DBHelper.GetDataTable("UP_CZ_CAR_EQUIP_SUB_S", new object[] { _CompanyCode, content });
                    break;

                case "H_CZ_CAR_EMERGE":
                    //dt = DBHelper.GetDataTable("UP_CZ_DELIVERY_MAN_SELECT", new object[] { _CompanyCode, "1000",GetListParam[0], "" });
                    dt = DBHelper.GetDataTable("UP_CZ_CAR_EMEGRNCY_DETAIL_S", new object[] { content });
                    break;

                case "H_CZ_CAR_EMP":
                    //dt = DBHelper.GetDataTable("UP_CZ_DELIVERY_MAN_SELECT", new object[] { _CompanyCode, "1000",GetListParam[0], "" });
                    dt = DBHelper.GetDataTable("UP_CZ_EQUIP_EMP_S", new object[] { content });
                    break;

                case "H_CZ_CODE_DTL":
                    //dt = DBHelper.GetDataTable("UP_CZ_DELIVERY_MAN_SELECT", new object[] { _CompanyCode, "1000",GetListParam[0], "" });
                    dt = DBHelper.GetDataTable("UP_CZ_EQUIP_CODEDTL", new object[] { content });
                    break;

                case "H_CZ_CAR_PRICE_S":

                    //dt = DBHelper.GetDataTable("UP_CZ_DELIVERY_MAN_SELECT", new object[] { _CompanyCode, "1000",GetListParam[0], "" });
                    dt = DBHelper.GetDataTable("UP_CZ_CAR_PRICE_POP_S", new object[] { content });
                    break;



                default:
                    break;
                //case "H_CZ_HELP01_2":
                //    //설정
                //    break;
            }

            return dt;


        }

        protected override DataTable Search()
        {
            /*if (GetListParam[0] != "")
            {
                this.txt검색.Text = GetListParam[0];
            }*/
            return this.OnCodeSearch(this.txt검색.Text);
        }

        protected override DataTable RefreshSearch()
        {
            return this.Search();
        }



        public DataTable SetDetail(string 검색)
        {
            string _CompanyCode = MA.Login.회사코드;

            DataTable dt = null;
            switch (base.GetHelpID)
            {
                case "H_CZ_CAR_PRICE_S":
                    //dt = DBHelper.GetDataTable("UP_CZ_DELIVERY_MAN_SELECT", new object[] { _CompanyCode, "1000",GetListParam[0], "" });
                    dt = DBHelper.GetDataTable("UP_CZ_CAR_PRICE_POP_S", new object[] { _CompanyCode, "1000", 검색 });
                    break;

                //case "H_CZ_HELP01_2":
                //    //설정
                //    break;
            }

            return dt;
        }

        public string Get검색
        {
            get { return txt검색.Text; }
        }

        #endregion

    }
}
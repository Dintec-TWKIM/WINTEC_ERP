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

    public partial class H_CZ_HELP_SL : HelpBase, IHelp
    {
        public H_CZ_HELP_SL()
        {
            InitializeComponent();
        }

        public H_CZ_HELP_SL(HelpParam helpParam)
            : base(helpParam)
        {
            InitializeComponent();
            base.SetIHelp = this as IHelp;
            InitEvent();
            InitGrid();

            SetDefault(base.Get타이틀명, flex, btn확인, btn검색, btn취소);//페이지 컨트롤들을 해당페이지와 연결
            helpParam.QueryAction = QueryAction.RealTime;//조회할때마다 서버에서 가져옴

        }


        #region -> 초기화

        private void InitEvent()
        {
            //txt검색.KeyDown += new KeyEventHandler(검색_KeyDown);
        }

        /// <summary>
        ///그리드 초기화 
        /// </summary>
        private void InitGrid()
        {
            ArrayList list = new ArrayList();

            switch (base.GetHelpID)
            {

                case "H_CZ_SL":
                    list.Add(new object[] { "CD_SL", "창고 코드", 80 });
                    list.Add(new object[] { "NM_SL", "창고명", 170 });
                    break;

                default:
                    //설정
                    list.Add(new object[] { "CODE", "코드", 80 });
                    list.Add(new object[] { "NAME", "코드명", 170 });
                    break;
            }

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


                case "H_CZ_SL":
                    dt = DBHelper.GetDataTable("UP_CZ_SL_S", new object[] { _CompanyCode,   content });
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

            //this.txt검색.Text = this._HelpParam.UserParams; // this._helpParam.get_P92_DETAIL_SEARCH_CODE();
            //this.txt검색.Text = "";
            this.txt공장.Text = GetListParam[1];


            return this.OnCodeSearch(this.txt공장.Text);
        }

        protected override DataTable RefreshSearch()
        {
            return this.Search();
        }



        public DataTable SetDetail(string 검색)
        {
            string _CompanyCode = MA.Login.회사코드;

            DataTable dt = null;
            //switch (base.GetHelpID)
            //{
            //    case "H_CZ_DELIVERY_MAN_SUB":
            //        //dt = DBHelper.GetDataTable("UP_CZ_DELIVERY_MAN_SELECT", new object[] { _CompanyCode, "1000",GetListParam[0], "" });
            //        dt = DBHelper.GetDataTable("UP_CZ_DELIVERY_MAN_SUB_SELECT", new object[] { _CompanyCode, "1000", 검색 });
            //        break;
            //    case "H_CZ_DELIVERY_VEHICLE_SUB":
            //        //dt = DBHelper.GetDataTable("UP_CZ_VEHICLE_SHINKI_SELECT", new object[] { GetListParam[0], GetListParam[1], 검색 });
            //        dt = DBHelper.GetDataTable("UP_CZ_VEHICLE_SHINKI_SUB_SELECT", new object[] { _CompanyCode, "1000", 검색 });
            //        break;
            //    //case "H_CZ_HELP01_2":
            //    //    //설정
            //    //    break;
            //}

            return dt;
        }

        public string Get검색
        {
            get { return txt공장.Text; }
        }

        #endregion

    }
}

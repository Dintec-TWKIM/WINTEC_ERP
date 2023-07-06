using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;

namespace cz
{
    [UserHelpDescription("CODE", "NAME", "CODE", "NAME")]
    public partial class H_CZ_MA_CODE_SUB : HelpBase, IHelp
    {
        string 회사코드, 코드, 언어, 화면;

        public H_CZ_MA_CODE_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_MA_CODE_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();
            base.SetIHelp = this as IHelp;
            
            if (string.IsNullOrEmpty(helpParam.P01_CD_COMPANY))
                this.회사코드 = Global.MainFrame.LoginInfo.CompanyCode;
            else
                this.회사코드 = helpParam.P01_CD_COMPANY;

            this.화면 = helpParam.P11_ID_MENU;

            this.코드 = helpParam.P41_CD_FIELD1;
            this.언어 = helpParam.P42_CD_FIELD2;
            
            this.txt검색.Text = helpParam.P92_DETAIL_SEARCH_CODE;

            this.InitGrid();

            base.SetDefault(base.Get타이틀명, this._flex, this.btn확인, this.btn검색, this.btn취소, this.btn멀티검색, this.txt검색); //페이지 컨트롤들을 해당페이지와 연결
            helpParam.QueryAction = QueryAction.RealTime; //조회할때마다 서버에서 가져옴
        }

        #region -> 초기화
        /// <summary>
        /// 그리드 초기화 
        /// </summary>
        private void InitGrid()
        {
            ArrayList list = new ArrayList();

            if (this.화면 == "P_CZ_MA_HULL")
            {
                list.Add(new object[] { "CODE", Global.MainFrame.DD("코드"), 80 });
                list.Add(new object[] { "NAME", Global.MainFrame.DD("이름"), 80 });
                list.Add(new object[] { "CD_FLAG2", Global.MainFrame.DD("관련2"), 80 });
            }
            else
            {
                list.Add(new object[] { "CODE", Global.MainFrame.DD("코드"), 80 });
                list.Add(new object[] { "NAME", Global.MainFrame.DD("이름"), 80 });
            }

            _flex.Cols.Frozen = 1;
            base.InitGrid(_flex, list);  //그리드는 PageBase의 그리드와 다르다   

        }
        #endregion

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;
            
            dt = DBHelper.GetDataTable("SP_H_CZ_MA_CODE_SUB", new object[] { this.회사코드,
                                                                             this.코드,
                                                                             this.언어,
                                                                             this.화면,
                                                                             this._HelpParam.P61_CODE1,
                                                                             검색 });

            if (dt == null || dt.Rows.Count == 0)
            {
                Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                return dt;
            }

            return dt.DefaultView.ToTable();
        }

        public string Get검색
        {
            get { return this.txt검색.Text; }
        }
    }
}
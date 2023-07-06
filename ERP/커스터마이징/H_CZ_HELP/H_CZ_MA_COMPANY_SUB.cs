using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;

namespace cz
{
    public partial class H_CZ_MA_COMPANY_SUB : HelpBase, IHelp
    {
        public H_CZ_MA_COMPANY_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_MA_COMPANY_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();
            base.SetIHelp = this as IHelp;
            
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

            list.Add(new object[] { "CD_COMPANY", Global.MainFrame.DD("회사코드"), 80 });
            list.Add(new object[] { "NM_COMPANY", Global.MainFrame.DD("회사명"), 80 });

            _flex.Cols.Frozen = 1;
            base.InitGrid(_flex, list);  //그리드는 PageBase의 그리드와 다르다   

        }
        #endregion

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;
            
            dt = DBHelper.GetDataTable("SP_H_CZ_MA_COMPANY_SUB", new object[] { Global.MainFrame.LoginInfo.CompanyCode, 검색 });

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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;

namespace cz
{
    [UserHelpDescription("계산서번호", "계산서번호", "NO_IV", "NO_IV")]
    public partial class H_CZ_SA_IV_SUB : HelpBase, IHelp
    {
        public H_CZ_SA_IV_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_SA_IV_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();

            base.SetIHelp = this as IHelp;

            this.InitGrid();

            this.txt계산서번호.Text = helpParam.P92_DETAIL_SEARCH_CODE;

            base.SetDefault(base.Get타이틀명, this._flex, this.btn확인, this.btn검색, this.btn취소, this.btn멀티검색, this.txt계산서번호); //페이지 컨트롤들을 해당페이지와 연결
            helpParam.QueryAction = QueryAction.RealTime; //조회할때마다 서버에서 가져옴
        }

        #region -> 초기화
        /// <summary>
        /// 그리드 초기화 
        /// </summary>
        private void InitGrid()
        {
            ArrayList list = new ArrayList();

            list.Add(new object[] { "NO_IV", Global.MainFrame.DD("계산서번호"), 100 });
            list.Add(new object[] { "DT_PROCESS", Global.MainFrame.DD("발행일자"), 80 });
            list.Add(new object[] { "NM_TRANS", Global.MainFrame.DD("거래구분"), 80 });
            list.Add(new object[] { "NM_EXCH", Global.MainFrame.DD("통화명"), 60 });
            list.Add(new object[] { "LN_PARTNER", Global.MainFrame.DD("매출처명"), 80 });
            list.Add(new object[] { "LN_BILL_PARTNER", Global.MainFrame.DD("수금처명"), 80 });

            this._flex.Cols.Frozen = 1;
            base.InitGrid(this._flex, list);  //그리드는 PageBase의 그리드와 다르다
        }
        #endregion

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;

            dt = DBHelper.GetDataTable("SP_H_CZ_SA_IV_SUB", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           Global.MainFrame.LoginInfo.Language,
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
            get { return this.txt계산서번호.Text; }
        }
    }
}

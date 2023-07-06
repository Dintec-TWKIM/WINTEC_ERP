using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms.Help;
using System.Collections;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    [UserHelpDescription("거래처코드", "거래처명", "CD_PARTNER", "NM_PARTNER")]
    public partial class H_CZ_SA_PARTNER_SUB : HelpBase, IHelp
    {
        string 검색타입;
        string 회사;

        public H_CZ_SA_PARTNER_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_SA_PARTNER_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();

            base.SetIHelp = this as IHelp;

            switch (helpParam.P00_CHILD_MODE)
            {
                case "001":
                case "002":
                    this.검색타입 = helpParam.P00_CHILD_MODE;
                    this.txt파일번호.Text = helpParam.P61_CODE1;
                    break;
                default:
                    this.검색타입 = "000";
                    break;
            }

            this.txt검색.Text = helpParam.P92_DETAIL_SEARCH_CODE;
            this.회사 = helpParam.P62_CODE2;

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

            list.Add(new object[] { "CD_PARTNER", Global.MainFrame.DD("거래처코드"), 120 });
            list.Add(new object[] { "NM_PARTNER", Global.MainFrame.DD("거래처명"), 80 });

            base.InitGrid(this._flex, list);  //그리드는 PageBase의 그리드와 다르다
        }
        #endregion

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;

            List<object> paramList = new List<object>();
            paramList.Add(this.회사);
            paramList.Add(검색타입);
            paramList.Add(this.txt파일번호.Text);
            paramList.Add(검색);

            dt = DBHelper.GetDataTable("SP_H_CZ_SA_PARTNER_SUB", paramList.ToArray());

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

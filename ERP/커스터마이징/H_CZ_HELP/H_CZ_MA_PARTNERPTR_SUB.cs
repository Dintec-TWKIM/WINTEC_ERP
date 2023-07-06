using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;

namespace cz
{
    [UserHelpDescription("순번", "담당자명", "SEQ", "NM_PTR")]
    public partial class H_CZ_MA_PARTNERPTR_SUB : HelpBase, IHelp
    {
        public H_CZ_MA_PARTNERPTR_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_MA_PARTNERPTR_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();

            base.SetIHelp = this as IHelp;
            this.InitGrid();

            if (helpParam.P35_CD_MNGD == "Y")
                this.ctx거래처.ReadOnly = ReadOnly.None;
            else
                this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;

            this.ctx거래처.CodeValue = helpParam.P14_CD_PARTNER;
            this.ctx거래처.CodeName = helpParam.P34_CD_MNG;

            this.txt검색.Text = helpParam.P92_DETAIL_SEARCH_CODE;

            base.SetDefault(base.Get타이틀명, this._flex, this.btn확인, this.btn검색, this.btn취소, this.btn멀티검색, this.txt검색); //페이지 컨트롤들을 해당페이지와 연결
            helpParam.QueryAction = QueryAction.RealTime; //조회할때마다 서버에서 가져옴
        }

        private void InitGrid()
        {
            ArrayList list = new ArrayList();

            list.Add(new object[] { "LN_PARTNER", Global.MainFrame.DD("거래처명"), 120 });
            list.Add(new object[] { "SEQ", Global.MainFrame.DD("순번"), 80 });
            list.Add(new object[] { "NM_PTR", Global.MainFrame.DD("담당자명"), 100 });
            list.Add(new object[] { "EN_PTR", Global.MainFrame.DD("담당자명(영)"), 100 });
            list.Add(new object[] { "NM_DEPT", Global.MainFrame.DD("부서"), 100 });
            list.Add(new object[] { "NM_DUTY_RESP", Global.MainFrame.DD("직책"), 100 });
            list.Add(new object[] { "EN_DUTY_RESP", Global.MainFrame.DD("직책(영)"), 100 });
            list.Add(new object[] { "NO_HP", Global.MainFrame.DD("휴대폰번호"), 100 });
            list.Add(new object[] { "NM_EMAIL", Global.MainFrame.DD("메일주소"), 100 });
            list.Add(new object[] { "NO_FAX", Global.MainFrame.DD("팩스번호"), 100 });
            list.Add(new object[] { "NO_TEL", Global.MainFrame.DD("전화번호"), 100 });
            list.Add(new object[] { "CLIENT_NOTE", Global.MainFrame.DD("비고"), 100 });

            _flex.Cols.Frozen = 1;
            base.InitGrid(_flex, list);  //그리드는 PageBase의 그리드와 다르다   
        }

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;
            
            dt = DBHelper.GetDataTable("SP_H_CZ_FI_PARTNERPTR_SUB", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   this.ctx거래처.CodeValue,
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

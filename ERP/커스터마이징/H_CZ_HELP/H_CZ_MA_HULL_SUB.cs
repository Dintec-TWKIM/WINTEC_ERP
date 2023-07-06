using System;
using System.Collections;
using System.Data;
using System.Linq;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;

namespace cz
{
    [UserHelpDescription("IMO번호", "호선번호", "NO_IMO", "NO_HULL")]
    public partial class H_CZ_MA_HULL_SUB : HelpBase, IHelp
    {
        public H_CZ_MA_HULL_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_MA_HULL_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();
            base.SetIHelp = this as IHelp;
            this.InitGrid();
            this.InitEvent();

            if (helpParam.P35_CD_MNGD == "Y")
                this.ctx운항선사.ReadOnly = ReadOnly.None;
            else
                this.ctx운항선사.ReadOnly = ReadOnly.TotalReadOnly;

            this.ctx운항선사.CodeValue = helpParam.P14_CD_PARTNER;
            this.ctx운항선사.CodeName = helpParam.P34_CD_MNG;

            this.txt검색.Text = helpParam.P92_DETAIL_SEARCH_CODE;

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

            list.Add(new object[] { "NO_IMO", Global.MainFrame.DD("IMO번호"), 80 });
            list.Add(new object[] { "NO_HULL", Global.MainFrame.DD("호선번호"), 80 });
            list.Add(new object[] { "NM_VESSEL", Global.MainFrame.DD("호선명"), 170 });
            list.Add(new object[] { "LN_PARTNER", Global.MainFrame.DD("운항선사"), 170 });

            this._flex.Cols.Frozen = 1;
            base.InitGrid(this._flex, list);  //그리드는 PageBase의 그리드와 다르다   

        }

        private void InitEvent()
        {
            this.btn선택해제.Click += new EventHandler(this.btn선택해제_Click);
        }
        #endregion

        private void btn선택해제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;
                if (!this._flex.DataTable.Columns.Contains("S")) return;

                this._flex.Redraw = false;

                foreach (DataRow dr in this._flex.DataTable.AsEnumerable().Where(x => Convert.ToBoolean(x["S"]) == true))
                    dr["S"] = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
            }
        }

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;
            
            dt = DBHelper.GetDataTable("SP_H_CZ_MA_HULL_SUB", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             this.ctx운항선사.CodeValue,
                                                                             검색 });

            if (dt == null || dt.Rows.Count == 0)
            {
                Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                return dt;
            }

            return dt;
        }

        public string Get검색
        {
            get { return this.txt검색.Text; }
        }
    }
}

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
    public partial class H_CZ_HR_PEVALU_OBJECT_SUB : HelpBase, IHelp
    {
        public H_CZ_HR_PEVALU_OBJECT_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_HR_PEVALU_OBJECT_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();
            base.SetIHelp = this as IHelp;

            this.cbo목표구분.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_HR00001");
            this.cbo목표구분.ValueMember = "CODE";
            this.cbo목표구분.DisplayMember = "NAME";

            this.cbo담당구분.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_HR00003");
            this.cbo담당구분.ValueMember = "CODE";
            this.cbo담당구분.DisplayMember = "NAME";

            this.InitGrid();

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

            list.Add(new object[] { "CODE1", Global.MainFrame.DD("목표코드"), 80 });
            list.Add(new object[] { "NAME2", Global.MainFrame.DD("목표구분"), 80 });
            list.Add(new object[] { "NAME3", Global.MainFrame.DD("담당구분"), 80 });
            list.Add(new object[] { "NAME1", Global.MainFrame.DD("목표"), 170 });

            _flex.Cols.Frozen = 1;
            base.InitGrid(_flex, list);  //그리드는 PageBase의 그리드와 다르다   

        }
        #endregion

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;
            
            dt = DBHelper.GetDataTable("SP_H_CZ_HR_PEVALU_OBJECT_SUB", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                      Global.MainFrame.LoginInfo.Language,
                                                                                      D.GetString(this.cbo목표구분.SelectedValue),
                                                                                      D.GetString(this.cbo담당구분.SelectedValue),
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

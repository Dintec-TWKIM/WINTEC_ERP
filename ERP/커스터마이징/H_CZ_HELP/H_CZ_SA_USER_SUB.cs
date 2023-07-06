using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Common.Forms.Help;
using System.Collections;

namespace cz
{
    [UserHelpDescription("사용자ID", "이름", "ID_USER", "NM_USER")]
    public partial class H_CZ_SA_USER_SUB : HelpBase, IHelp
    {
        public H_CZ_SA_USER_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_SA_USER_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();

            base.SetIHelp = this as IHelp;

            this.cbo재직구분.DataSource = Global.MainFrame.GetComboDataCombine("S;HR_H000014");
            this.cbo재직구분.ValueMember = "CODE";
            this.cbo재직구분.DisplayMember = "NAME";

            this.cbo재직구분.SelectedValue = "001";

            this.cbo담당업무.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_SA00024");
            this.cbo담당업무.ValueMember = "CODE";
            this.cbo담당업무.DisplayMember = "NAME";

            this.cbo담당업무.SelectedValue = helpParam.P61_CODE1;

            if (string.IsNullOrEmpty(helpParam.P41_CD_FIELD1))
            {
                this.bpc회사.AddItem(Global.MainFrame.LoginInfo.CompanyCode, Global.MainFrame.LoginInfo.CompanyName);
                this.bpc회사.SelectedValue = Global.MainFrame.LoginInfo.CompanyCode;
            }
            else
            {
                this.bpc회사.AddItem(helpParam.P41_CD_FIELD1, helpParam.P42_CD_FIELD2);
                this.bpc회사.SelectedValue = helpParam.P41_CD_FIELD1;
            }

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

            list.Add(new object[] { "NM_COMPANY", Global.MainFrame.DD("회사명"), 120 });
            list.Add(new object[] { "NM_TP_BIZ", Global.MainFrame.DD("담당업무"), 80 });
            list.Add(new object[] { "ID_USER", Global.MainFrame.DD("사용자ID"), 80 });
            list.Add(new object[] { "NM_USER", Global.MainFrame.DD("이름"), 60 });
            
            base.InitGrid(this._flex, list);  //그리드는 PageBase의 그리드와 다르다
        }
        #endregion

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;

            dt = DBHelper.GetDataTable("SP_H_CZ_SA_USER_SUB", new object[] { this.bpc회사.QueryWhereIn_Pipe,
                                                                             Global.MainFrame.LoginInfo.Language,
                                                                             this.cbo담당업무.SelectedValue,
                                                                             this.cbo재직구분.SelectedValue,
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

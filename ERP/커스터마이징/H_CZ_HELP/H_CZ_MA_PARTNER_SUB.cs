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
using C1.Win.C1FlexGrid;

namespace cz
{
    public partial class H_CZ_MA_PARTNER_SUB : HelpBase, IHelp
    {
        public H_CZ_MA_PARTNER_SUB()
        {
            this.InitializeComponent();
        }

        public H_CZ_MA_PARTNER_SUB(HelpParam helpParam)
            : base(helpParam)
        {
            this.InitializeComponent();
            base.SetIHelp = this as IHelp;
            
            this.InitEvent();
            this.InitGrid();

            this.txt검색.Text = helpParam.P92_DETAIL_SEARCH_CODE;

            base.SetDefault(base.Get타이틀명, this._flex, this.btn확인, this.btn검색, this.btn취소, this.btn멀티검색, this.txt검색); //페이지 컨트롤들을 해당페이지와 연결
            helpParam.QueryAction = QueryAction.RealTime; //조회할때마다 서버에서 가져옴
        }

        #region -> 초기화
        protected override void InitPaint()
        {
            base.InitPaint();

            this.cbo거래처구분.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000001");
            this.cbo거래처구분.ValueMember = "CODE";
            this.cbo거래처구분.DisplayMember = "NAME";

            this.cbo거래처분류.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000003");
            this.cbo거래처분류.ValueMember = "CODE";
            this.cbo거래처분류.DisplayMember = "NAME";

            this.cbo지역구분.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000062");
            this.cbo지역구분.ValueMember = "CODE";
            this.cbo지역구분.DisplayMember = "NAME";

            this.cbo국가.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000020");
            this.cbo국가.ValueMember = "CODE";
            this.cbo국가.DisplayMember = "NAME";

            this.cbo사용유무.DataSource = MA.GetCode("사용여부", true);
            this.cbo사용유무.ValueMember = "CODE";
            this.cbo사용유무.DisplayMember = "NAME";

            this.cbo사용유무.SelectedValue = "Y";
        }

        private void InitEvent()
        {
            this._flex.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
        }
        
        /// <summary>
        /// 그리드 초기화 
        /// </summary>
        private void InitGrid()
        {
            ArrayList list = new ArrayList();

            list.Add(new object[] { "CD_PARTNER", Global.MainFrame.DD("거래처코드"), 80 });
            list.Add(new object[] { "CLS_PARTNER", Global.MainFrame.DD("거래처분류코드"), 0 });
            list.Add(new object[] { "NM_CLS_PARTNER", Global.MainFrame.DD("거래처분류"), 80 });
            list.Add(new object[] { "NM_FG_PARTNER", Global.MainFrame.DD("거래처구분"), 100 });
            list.Add(new object[] { "LN_PARTNER", Global.MainFrame.DD("거래처명"), 120 });

            base.InitGrid(_flex, list);  //그리드는 PageBase의 그리드와 다르다

            this._flex.Styles.Add("매입처").ForeColor = Color.Blue;
            this._flex.Styles.Add("매입처").BackColor = Color.White;
            this._flex.Styles.Add("매출처").ForeColor = Color.Red;
            this._flex.Styles.Add("매출처").BackColor = Color.White;
            this._flex.Styles.Add("매입매출").ForeColor = Color.Green;
            this._flex.Styles.Add("매입매출").BackColor = Color.White;
            this._flex.Styles.Add("포워더").ForeColor = Color.Orange;
            this._flex.Styles.Add("포워더").BackColor = Color.White;
            this._flex.Styles.Add("관리").ForeColor = Color.Navy;
            this._flex.Styles.Add("관리").BackColor = Color.White;
            this._flex.Styles.Add("기타").ForeColor = Color.Purple;
            this._flex.Styles.Add("기타").BackColor = Color.White;
            this._flex.Styles.Add("없음").ForeColor = Color.Black;
            this._flex.Styles.Add("없음").BackColor = Color.White;
        }
        #endregion
        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow) return;

                CellStyle cellStyle = this._flex.Rows[e.Row].Style;

                switch (D.GetString(this._flex[e.Row, "CLS_PARTNER"]))
                {
                    case "005":
                        if (cellStyle == null || cellStyle.Name != "매입처")
                            this._flex.Rows[e.Row].Style = this._flex.Styles["매입처"];
                        break;
                    case "006":
                        if (cellStyle == null || cellStyle.Name != "매출처")
                            this._flex.Rows[e.Row].Style = this._flex.Styles["매출처"];
                        break;
                    case "007":
                        if (cellStyle == null || cellStyle.Name != "매입매출")
                            this._flex.Rows[e.Row].Style = this._flex.Styles["매입매출"];
                        break;
                    case "008":
                        if (cellStyle == null || cellStyle.Name != "포워더")
                            this._flex.Rows[e.Row].Style = this._flex.Styles["포워더"];
                        break;
                    case "009":
                        if (cellStyle == null || cellStyle.Name != "관리")
                            this._flex.Rows[e.Row].Style = this._flex.Styles["관리"];
                        break;
                    case "010":
                        if (cellStyle == null || cellStyle.Name != "기타")
                            this._flex.Rows[e.Row].Style = this._flex.Styles["기타"];
                        break;
                    default:
                        if (cellStyle == null || cellStyle.Name != "없음")
                            this._flex.Rows[e.Row].Style = this._flex.Styles["없음"];
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public DataTable SetDetail(string 검색)
        {
            DataTable dt = null;
            List<object> paramList = new List<object>();

            paramList.Add(Global.MainFrame.LoginInfo.CompanyCode);
            paramList.Add(D.GetString(this.cbo거래처구분.SelectedValue));
            paramList.Add(D.GetString(this.cbo거래처분류.SelectedValue));
            paramList.Add(D.GetString(this.cbo지역구분.SelectedValue));
            paramList.Add(D.GetString(this.cbo국가.SelectedValue));
            paramList.Add(D.GetString(this.cbo사용유무.SelectedValue));
            paramList.Add(검색);

            dt = DBHelper.GetDataTable("SP_H_CZ_MA_PARTNER_SUB", paramList.ToArray());

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

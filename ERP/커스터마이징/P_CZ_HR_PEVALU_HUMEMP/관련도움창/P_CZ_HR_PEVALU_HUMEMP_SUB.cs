using System;
using Duzon.ERPU;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_HR_PEVALU_HUMEMP_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_HR_PEVALU_HUMEMP_BIZ _biz = new P_CZ_HR_PEVALU_HUMEMP_BIZ();
        private string 복사할코드 = null;
        private string 유형할코드 = null;
        private string 그룹할코드 = null;
        private string 차수할코드 = null;

        public P_CZ_HR_PEVALU_HUMEMP_SUB()
        {
            InitializeComponent();
        }

        public P_CZ_HR_PEVALU_HUMEMP_SUB(string _복사할코드, string _유형할코드, string _그룹할코드, string _차수할코드)
        {
            this.InitializeComponent();
            this.복사할코드 = _복사할코드;
            this.유형할코드 = _유형할코드;
            this.그룹할코드 = _그룹할코드;
            this.차수할코드 = _차수할코드;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitEvent();
            this.Search();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("CD_EVALU", "평가코드", 80, false);
            this._flex.SetCol("NM_EVALU", "평가명", 80, false);
            this._flex.SetCol("NM_EVTYPE", "평가유형", 80, false);
            this._flex.SetCol("NM_GROUP", "평가그룹", 80, false);
            this._flex.SetCol("NM_EVNUMBER", "평가차수", 80, false);
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btn복사.Click += new EventHandler(this.btn복사_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
        }

        private void Search()
        {
            if (this._flex.DataTable != null)
                this._flex.DataTable.Rows.Clear();
            this._flex.Binding = this._biz.SearchSub();
            if (!this._flex.HasNormalRow)
                return;
            this._flex.Focus();
        }

        private void btn복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                string string1 = D.GetString(this._flex[this._flex.Row, "CD_EVALU"]);
                string string2 = D.GetString(this._flex[this._flex.Row, "CD_EVTYPE"]);
                string string3 = D.GetString(this._flex[this._flex.Row, "CD_GROUP"]);
                string string4 = D.GetString(this._flex[this._flex.Row, "CD_EVNUMBER"]);
                Global.MainFrame.ExecSp("UP_HR_PEVALU_HUMEMP_SUB_U", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                    string1,
                                                                                    string2,
                                                                                    string3,
                                                                                    string4,
                                                                                    this.복사할코드,
                                                                                    this.유형할코드,
                                                                                    this.그룹할코드,
                                                                                    this.차수할코드,
                                                                                    Global.MainFrame.LoginInfo.UserID });
                Global.MainFrame.ShowMessage("CZ_@ 을(를) 완료하였습니다.", Global.MainFrame.DD("복사"));

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn취소_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

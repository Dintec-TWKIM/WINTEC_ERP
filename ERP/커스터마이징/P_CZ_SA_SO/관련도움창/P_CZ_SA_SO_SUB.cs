using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_SO_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_SA_SO_SUB_BIZ _biz = new P_CZ_SA_SO_SUB_BIZ();
        private string _구분;

        public string 수주번호
        {
            get
            {
                return this._flexH["NO_SO"].ToString();
            }
        }

        public string 구분
        {
            get
            {
                return this._구분;
            }
        }

        public string 수주상태
        {
            get
            {
                return this._flexH["STA_SO"].ToString();
            }
        }

        public bool 헤더수정유무
        {
            get
            {
                foreach (DataRow dataRow in this._flexL.DataTable.Select("NO_SO = '" + this._flexH["NO_SO"].ToString() + "'", string.Empty, DataViewRowState.CurrentRows))
                {
                    if (dataRow["STA_SO"].ToString() != "O")
                        return false;
                }
                return true;
            }
        }

        public string 출고형태
        {
            get
            {
                return this._flexL["TP_GI"].ToString();
            }
        }

        public string 거래구분
        {
            get
            {
                return this._flexL["TP_BUSI"].ToString();
            }
        }

        public string 매출형태
        {
            get
            {
                return this._flexL["TP_IV"].ToString();
            }
        }

        public string 의뢰여부
        {
            get
            {
                return this._flexL["GIR"].ToString();
            }
        }

        public string 출고여부
        {
            get
            {
                return this._flexL["GI"].ToString();
            }
        }

        public string 매출여부
        {
            get
            {
                return this._flexL["IV"].ToString();
            }
        }

        public string 수출여부
        {
            get
            {
                return this._flexL["TRADE"].ToString();
            }
        }

        public string 단가적용형태
        {
            get
            {
                return this._flexH["TP_SALEPRICE"].ToString();
            }
        }

        public bool 헤더만복사
        {
            get
            {
                return this.chk헤더만복사.Checked;
            }
        }

        public P_CZ_SA_SO_SUB()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public P_CZ_SA_SO_SUB(string 프로젝트)
            : this()
        {
            try
            {
                this.txt프로젝트.Text = 프로젝트;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitEvent()
        {
            this.ctx청구유형.QueryBefore += new BpQueryHandler(this.ctx수주유형_QueryBefore);

            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn복사.Click += new EventHandler(this.btn복사_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);

            this._flexH.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.HelpClick += new EventHandler(this._flexH_HelpClick);
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NO_SO", "청구번호", 120);
            this._flexH.SetCol("NM_PROJECT", "프로젝트", 100);
            this._flexH.SetCol("DT_SO", "청구일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_SO", "청구유형", 100);
            this._flexH.SetCol("LN_PARTNER", "매출처", 100);
            this._flexH.SetCol("NM_SALEGRP", "영업그룹", 100);
            this._flexH.SetCol("NM_KOR", "담당자", 100);
            this._flexH.SetCol("DC_RMK", "비고", 180);

            this._flexH.ExtendLastCol = true;

            this._flexH.SettingVersion = "1.0.0.0";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexH.LoadUserCache("P_CZ_SA_SO_SUB_flexH");
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("CD_ITEM", "품목코드", 120);
            this._flexL.SetCol("NM_ITEM", "품목명", 120);
            this._flexL.SetCol("STND_ITEM", "규격", 65);
            this._flexL.SetCol("UNIT_SO", "단위", 65);
            this._flexL.SetCol("QT_SO", "수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UM_SO", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_SO", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_WONAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_VAT", "부가세", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DT_REQGI", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);


            this._flexL.SettingVersion = "1.0.0.0";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexL.LoadUserCache("P_CZ_SA_SO_SUB_flexL");
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp청구일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-3);
            this.dtp청구일자.EndDate = Global.MainFrame.GetDateTimeToday();

            if (!string.IsNullOrEmpty(this.txt프로젝트.Text))
            {
                this.TitleText = Global.MainFrame.DD("이중청구확인");

                this.dtp청구일자.StartDateToString = "20160101";

                this.chk헤더만복사.Visible = false;
                this.btn조회.Visible = false;
                this.btn복사.Visible = false;
                this.btn적용.Text = Global.MainFrame.DD("재청구");
                this.btn취소.Text = Global.MainFrame.DD("청구취소");

                this.btn조회_Click(null, null);

                if (!this._flexH.HasNormalRow)
                    this.DialogResult = DialogResult.OK;
                else
                    Global.MainFrame.ShowMessage("해당 프로젝트로 등록 된 청구 건이 있습니다.\n청구 내역 확인 하시고, [재청구] 또는 [청구취소] 버튼을 눌러주세요.");
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.dtp청구일자.StartDateToString,
                                                                      this.dtp청구일자.EndDateToString,
                                                                      this.ctx영업그룹.CodeValue,
                                                                      this.ctx담당자.CodeValue,
                                                                      this.ctx매출처.CodeValue,
                                                                      this.ctx청구유형.CodeValue,
                                                                      this.txt프로젝트.Text,
                                                                      this.txt청구번호.Text,
                                                                      Global.MainFrame.LoginInfo.EmployeeNo });

                if (!this._flexH.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                this._구분 = "복사";
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                this._구분 = "적용";
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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this._flexH.SaveUserCache("P_CZ_SA_SO_SUB_flexH");
            this._flexL.SaveUserCache("P_CZ_SA_SO_SUB_flexL");
        }

        private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.btn복사.Enabled = false;
                    this.btn적용.Enabled = false;
                }
                else
                {
                    this.btn복사.Enabled = true;
                    this.btn적용.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt = null;
            string key, filter;

            try
            {
                key = this._flexH["NO_SO"].ToString();
                filter = "NO_SO = '" + key + "'";

                if (this._flexH.DetailQueryNeed)
                    dt = this._biz.SearchDetail(key);

                this._flexL.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_HelpClick(object sender, EventArgs e)
        {
            try
            {
                this.btn적용_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx수주유형_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.ControlName == this.ctx청구유형.Name)
                {
                    e.HelpParam.P61_CODE1 = "N";
                    e.HelpParam.P62_CODE2 = "Y";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
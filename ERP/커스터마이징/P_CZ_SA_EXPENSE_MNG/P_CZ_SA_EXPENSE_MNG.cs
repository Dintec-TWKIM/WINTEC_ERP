using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.Common.BpControls;
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_EXPENSE_MNG : PageBase
    {
        P_CZ_SA_EXPENSE_MNG_BIZ _biz = new P_CZ_SA_EXPENSE_MNG_BIZ();

        public P_CZ_SA_EXPENSE_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.MainGrids = new FlexGrid[] { this._flex };

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("NO_IV", "계산서번호", 100);
            this._flex.SetCol("NO_LINE", "항번", 40);
            this._flex.SetCol("DT_PROCESS", "발생일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("CD_PJT", "프로젝트", 100);
            this._flex.SetCol("LN_PARTNER", "거래처", 100);
            this._flex.SetCol("NM_KOR", "담당자", 60);
            this._flex.SetCol("CD_ITEM", "품목코드", false);
            this._flex.SetCol("NM_ITEM", "품목명", 120);
            this._flex.SetCol("AM_EX_CLS", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_CLS", "금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("TXT_USERDEF1", "비고", 180, true);
            this._flex.SetCol("NM_INSERT", "등록자", false);
            this._flex.SetCol("DTS_INSERT", "등록일자", false);

            this._flex.ExtendLastCol = true;

            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.ctx비용코드.QueryBefore += new BpQueryHandler(this.ctx비용코드_QueryBefore);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp일자.EndDateToString = Global.MainFrame.GetStringToday;

            this.cbo구분.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "매입", "매출" });
            this.cbo구분.ValueMember = "CODE";
            this.cbo구분.DisplayMember = "NAME";

            this.ctx비용코드.CodeValue = "SD9003";
            this.ctx비용코드.CodeName = "PRICE ADJUSTMENT_GOODS";
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.dtp일자.StartDateToString,
                                                                     this.dtp일자.EndDateToString,
                                                                     this.cbo구분.SelectedValue,
                                                                     this.ctx비용코드.CodeValue,
                                                                     this.ctx거래처.CodeValue,
                                                                     this.txt프로젝트.Text,
                                                                     this.ctx영업담당자.CodeValue,
                                                                     (this.chk10000원이상.Checked == true ? "Y" : "N") });

                if (!_flex.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex.IsDataChanged == false) return false;

            DataTable dt = this._flex.GetChanges();

            if (!_biz.Save(dt, D.GetString(this.cbo구분.SelectedValue))) return false;

            this._flex.AcceptChanges();

            return true;
        }

        private void ctx비용코드_QueryBefore(object sender, BpQueryArgs e)
        {
            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB:
                    e.HelpParam.P09_CD_PLANT = "001";
                    e.HelpParam.P42_CD_FIELD2 = "010";
                    break;
            }
        }
    }
}

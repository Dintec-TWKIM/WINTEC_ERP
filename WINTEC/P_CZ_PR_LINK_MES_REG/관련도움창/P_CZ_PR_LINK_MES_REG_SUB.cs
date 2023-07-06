using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;

namespace cz
{
	public partial class P_CZ_PR_LINK_MES_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_PR_LINK_MES_REG_BIZ _biz = new P_CZ_PR_LINK_MES_REG_BIZ();
        private string _CD_PLANT = string.Empty;
        private string _DT_FROM = string.Empty;
        private string _DT_TO = string.Empty;        

        public P_CZ_PR_LINK_MES_REG_SUB(string CD_PLANT, string DT_FROM, string DT_TO)
        {
            this.InitializeComponent();
            this._CD_PLANT = CD_PLANT;
            this._DT_FROM = DT_FROM;
            this._DT_TO = DT_TO;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitControl();

            this.btn조회.Click += new EventHandler(this.btn조회_Click);
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("STA", "STA", 60, false);
            this._flex.SetCol("DTS_STA", "DTS_STA", 200, false);
            this._flex.SetCol("CD_PLANT", "공장코드", false);
            this._flex.SetCol("NO_MES", "MES번호", 100, false);
            this._flex.SetCol("DT_WORK", "실적일", 80, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NO_EMP", "담당자코드", false);
            this._flex.SetCol("NM_KOR", "담당자", 100, false);
            this._flex.SetCol("NO_WO", "작업지시번호", 100, false);
            this._flex.SetCol("NO_WORK", "작업실적번호", 100, false);
            this._flex.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flex.SetCol("NM_ITEM", "품목명", 140, false);
            this._flex.SetCol("STND_ITEM", "규격", 120, false);
            this._flex.SetCol("UNIT_MO", "단위", 40, false);
            this._flex.SetCol("QT_WORK", "실적수량", 75, false, typeof(Decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("YN_END", "처리여부", 60, false);
            this._flex.SetCol("CD_OP", "o.p.", false);
            this._flex.SetCol("CD_WC", "작업장코드", false);
            this._flex.SetCol("NM_WC", "작업장", 100, false);
            this._flex.SetCol("CD_WCOP", "공정코드", false);
            this._flex.SetCol("NM_OP", "공정", 100, false);
            this._flex.SetCol("CD_SL_IN", "입고창고", 80, false);
            this._flex.SetCol("NM_SL_IN", "입고창고명", 80, false);
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitControl()
        {
            try
            {
                new SetControl().SetCombobox(this.cbo공장, MA.GetCode("MA_PLANT_AUTH"));
                this.cbo공장.SelectedValue = (object)this._CD_PLANT;
                this.per기간.StartDateToString = this._DT_FROM;
                this.per기간.EndDateToString = this._DT_TO;
                this._flex.SetDataMap("CD_PLANT", MA.GetCode("MA_PLANT_AUTH"), "CODE", "NAME");
                this._flex.SetDataMap("UNIT_MO", MA.GetCode(Duzon.ERPU.MF.MF.코드.MASTER.단위), "CODE", "NAME");
                this._flex.SetDataMap("YN_END", MA.GetCode("YESNO"), "CODE", "NAME");
                this._flex.SetDataMap("STA", MA.GetCodeUser(new string[3]
                {
          "I",
          "U",
          "D"
                }, new string[3] { "Insert", "Update", "Delete" }), "CODE", "NAME");
                this._flex.Cols["DTS_STA"].Format = "####/##/## ##:##:##";
                this._flex.SetStringFormatCol(new string[1]
                {
          "DTS_STA"
                });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flex.Binding = (object)this._biz.SearchSub(new object[7]
                {
          (object) Global.MainFrame.LoginInfo.CompanyCode,
          (object) D.GetString(this.cbo공장.SelectedValue),
          (object) this.per기간.StartDateToString,
          (object) this.per기간.EndDateToString,
          this.chk_Insert.Checked ? (object) "I" : (object) "X",
          this.chk_Update.Checked ? (object) "U" : (object) "X",
          this.chk_Delete.Checked ? (object) "D" : (object) "X"
                });
                if (this._flex.HasNormalRow)
                    return;
                int num = (int)Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}
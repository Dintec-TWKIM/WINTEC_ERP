using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_FI_CARD_DELETE_KMB_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
        private DataTable _dtDelete;

        public P_FI_CARD_DELETE_KMB_SUB()
        {
            this.InitializeComponent();
        }

        public P_FI_CARD_DELETE_KMB_SUB(DataTable dtDelete)
        {
            this.InitializeComponent();
            this._dtDelete = dtDelete;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("ACCT_NO", "카드번호", 100, false);
            this._flex.SetCol("NM_DEPT", "관리부서", 120);
            this._flex.SetCol("NM_OWNER", "소유자", 100);
            this._flex.SetCol("TRADE_DATE", "승인일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("TRADE_TIME", "승인시간", 100);
            this._flex.Cols["TRADE_TIME"].Format = "00:00:00";
            this._flex.Cols["TRADE_TIME"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.SetCol("CLIENT_NOTE", "사용내역", false);
            this._flex.SetCol("TRADE_PLACE", "가맹점", 150);
            this._flex.SetCol("MERC_ADDR", "가맹점주소", 200);
            this._flex.Cols["MERC_ADDR"].Visible = false;
            this._flex.SetCol("ADMIN_AMT", "승인금액", 120, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("MCC_CODE_NAME", "업종", 100);
            this._flex.SetCol("S_IDNO", "사업자등록번호", 120);
            this._flex.Cols["S_IDNO"].Format = "000-00-00000";
            this._flex.Cols["S_IDNO"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.SetCol("SUPPLY_AMT", "공급가액", 120, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("VAT_AMT", "부가세", 120, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("SERVICE_CHARGE", "봉사료", 120, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SettingVersion = "1.2";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitEvent()
        {
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.Search();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ShowMessage("법인카드데이터가 삭제됩니다.\n삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;
                if (this._biz.DataDelete(this._dtDelete))
                {
                    Global.MainFrame.ShowMessage("법인카드데이터가 정상적으로 삭제되었습니다.");
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Search()
        {
            this._flex.Binding = (object)this._dtDelete;
            if (this._flex.HasNormalRow)
                return;
            Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
        }
    }
}
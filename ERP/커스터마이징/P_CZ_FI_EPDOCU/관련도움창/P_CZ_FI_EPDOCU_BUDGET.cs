using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_EPDOCU_BUDGET : Duzon.Common.Forms.CommonDialog
    {
        private Decimal _실행예산;
        private Decimal _집행실적;
        private Decimal _집행신청;
        
        public P_CZ_FI_EPDOCU_BUDGET()
        {
            this.InitializeComponent();
        }

        public P_CZ_FI_EPDOCU_BUDGET(Decimal 실행예산, Decimal 집행실적, Decimal 집행신청)
        {
            this.InitializeComponent();
            this._실행예산 = 실행예산;
            this._집행실적 = 집행실적;
            this._집행신청 = 집행신청;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.cur실행예산.DecimalValue = this._실행예산;
            this.cur집행실적.DecimalValue = this._집행실적;
            this.cur집행신청.DecimalValue = this._집행신청;
            this.cur집행율.DecimalValue = !(D.GetDecimal((object)this._실행예산) == new Decimal(0)) ? (this._집행실적 + this._집행신청) / this._실행예산 * new Decimal(100) : new Decimal(0);
            this.cur잔여예산.DecimalValue = this._실행예산 - this._집행실적 + this._집행신청;
            if (!(this.cur잔여예산.DecimalValue < new Decimal(0)))
                return;
            this.txt메세지.Text = "예산액을 초과하였습니다.";
        }
    }
}
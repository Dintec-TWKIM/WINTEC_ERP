using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_FI_EPDOCU_ETAXDT : Duzon.Common.Forms.CommonDialog
    {
        private string[] 발행일자 = new string[2];
        private string 발행구분 = "N";

        public string[] GetResult
        {
            get
            {
                return this.발행일자;
            }
        }

        public string Get발행구분
        {
            get
            {
                return this.발행구분;
            }
        }

        public P_CZ_FI_EPDOCU_ETAXDT()
        {
            this.InitializeComponent();
            this.dt발행년월.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH, FormatFgType.INSERT);
            this.dt발행년월.ToDayDate = Global.MainFrame.GetDateTimeToday();
            this.dt발행년월.Text = Global.MainFrame.GetStringFirstDayInMonth;
            this.m_FDT.Text = "01";
            this.m_TDT.Text = Global.MainFrame.GetStringToday.Substring(6, 2);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            new SetControl().SetCombobox(this.cbo발행구분, MA.GetCodeUser(new string[] { "N", "ALL" }, new string[] { Global.MainFrame.DD("Bill36524발행분"), Global.MainFrame.DD("전체발행분") }));
            this.cbo발행구분.SelectedValue = "N";
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            this.발행일자[0] = this.dt발행년월.Text + this.m_FDT.Text;
            this.발행일자[1] = this.dt발행년월.Text + this.m_TDT.Text;
            this.발행구분 = this.cbo발행구분.SelectedValue.ToString();
        }
    }
}
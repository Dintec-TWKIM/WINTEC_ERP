using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.ERPU;
using Duzon.Common.Forms;
using Duzon.ERPU.OLD;

namespace cz
{
    public partial class P_CZ_MA_EXCHANGE_SUB : Duzon.Common.Forms.CommonDialog
    {
        private bool 생성년월체크
        {
            get
            {
                return Checker.IsValid(this.dtpFROM, this.dtpTO, true, Global.MainFrame.DD("생성년월FROM"), Global.MainFrame.DD("생성년월TO"));
            }
        }

        public P_CZ_MA_EXCHANGE_SUB(string Date, string NoSeq)
        {
            this.InitializeComponent();
            this.dtpBasic.Text = Date;
            this.txtNoSeq.Text = NoSeq;
        }
        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btnCopy.Click += new EventHandler(this.BtnCopy_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            DateTime dateTime1 = new CommonFunction().GetDateTime(this.dtpBasic.Text);
            dateTime1 = dateTime1.AddDays(1.0);
            DateTime dateTime2 = new DateTime(dateTime1.Year, dateTime1.Month, 1);
            dateTime2 = dateTime2.AddMonths(1);
            DateTime dateTime3 = dateTime2.AddDays(-1.0);
            this.dtpFROM.Mask = this.dtpTO.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.MA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            this.dtpFROM.ToDayDate = dateTime1;
            this.dtpTO.ToDayDate = dateTime3;
            this.dtpFROM.Text = D.GetString(dateTime1.Year) + D.GetString(dateTime1.Month).PadLeft(2, '0') + D.GetString(dateTime1.Day).PadLeft(2, '0');
            this.dtpTO.Text = D.GetString(dateTime3.Year) + D.GetString(dateTime3.Month).PadLeft(2, '0') + D.GetString(dateTime3.Day).PadLeft(2, '0');
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.생성년월체크) return;

                MsgControl.ShowMsg("환율관리정보 복사 작업 중입니다..");
                DBHelper.ExecuteScalar("SP_CZ_MA_EXCHANGE_COPY", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             Global.MainFrame.LoginInfo.UserID,
                                                                             this.txtNoSeq.Text,
                                                                             this.dtpBasic.Text,
                                                                             this.dtpFROM.Text,
                                                                             this.dtpTO.Text });
                MsgControl.CloseMsg();
                Global.MainFrame.ShowMessage("환율관리정보 복사 작업을 완료했습니다.");
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }
    }
}

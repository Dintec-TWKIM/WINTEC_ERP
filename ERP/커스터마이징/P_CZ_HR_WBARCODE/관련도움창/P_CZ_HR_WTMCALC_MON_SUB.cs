using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.OLD;
using System;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_HR_WTMCALC_MON_SUB : Duzon.Common.Forms.CommonDialog
    {
        private string _barcodeType;

        public P_CZ_HR_WTMCALC_MON_SUB()
        {
            this.InitializeComponent();
            string empty = string.Empty;
            string str = DateTime.Now.ToString("yyyyMM");
            ((Control)this._btn확인).Click += new EventHandler(this._btn확인_Click);
            CommonFunction commonFunction = new CommonFunction();
            if (!(str != string.Empty))
                return;
            ((Control)this._dp기간FROM).Text = str + "01";
            ((Control)this._dp기간TO).Text = commonFunction.GetLastDayDateText(str + "01");
        }

        public P_CZ_HR_WTMCALC_MON_SUB(string barcodeType)
          : this()
        {
            this._barcodeType = barcodeType;
        }

        private void _btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.귀속일자입력체크)
                    return;
                switch (this._barcodeType)
                {
                    case "002":
                        if ((Global.MainFrame).ShowMessage("Caps 데이터 읽기 작업을 진행하시겠습니까?", "QY2") == DialogResult.Yes)
                        {
                            MsgControl.ShowMsg("작업중 입니다. \n잠시만 기다려 주십시요.");
                            object[] objArray = new object[4]
                            {
                (Global.MainFrame).LoginInfo.CompanyCode,
                (this._dp기간FROM).Text,
                (this._dp기간TO).Text,
                MA.Login.사용자아이디
                            };
                            (Global.MainFrame).ExecSp("UP_HR_WBARCODE_CAPS", objArray);
                        }
                        MsgControl.CloseMsg();
                        break;
                    case "003":
                        if ((Global.MainFrame).ShowMessage("Secom 데이터 읽기 작업을 진행하시겠습니까?", "QY2") == DialogResult.Yes)
                        {
                            MsgControl.ShowMsg("작업중 입니다. \n잠시만 기다려 주십시요.");
                            object[] objArray = new object[4]
                            {
                (Global.MainFrame).LoginInfo.CompanyCode,
                (this._dp기간FROM).Text,
                (this._dp기간TO).Text,
                MA.Login.사용자아이디
                            };
                            (Global.MainFrame).ExecSp("UP_HR_WBARCODE_SECOM", objArray);
                        }
                        MsgControl.CloseMsg();
                        break;
                    case "DOMINO":
                        if ((Global.MainFrame).ShowMessage("데이터 읽기 작업을 진행하시겠습니까?", "QY2") == DialogResult.Yes)
                        {
                            MsgControl.ShowMsg("작업중 입니다. \n잠시만 기다려 주십시요.");
                            object[] objArray = new object[5]
                            {
                (Global.MainFrame).LoginInfo.CompanyCode,
                "2",
                (this._dp기간FROM).Text,
                (this._dp기간TO).Text,
                MA.Login.사용자아이디
                            };
                            (Global.MainFrame).ExecSp("UP_HR_WBARCODE_INSERT_DOMINO", objArray);
                            break;
                        }
                        break;
                    default:
                        if ((Global.MainFrame).ShowMessage("FPMS 데이터 읽기 작업을 진행하시겠습니까?", "QY2") == DialogResult.Yes)
                        {
                            MsgControl.ShowMsg("작업중 입니다. \n잠시만 기다려 주십시요.");
                            (Global.MainFrame).ExecSp("UP_HR_WBARCODE_AVACO", (object[])new string[3]
                            {
                (Global.MainFrame).LoginInfo.CompanyCode,
                (this._dp기간FROM).Text,
                (this._dp기간TO).Text
                            });
                        }
                        MsgControl.CloseMsg();
                        break;
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                (Global.MainFrame).MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private bool 귀속일자입력체크
        {
            get
            {
                if (((Control)this._dp기간FROM).Text == string.Empty)
                {
                    int num = (int)(Global.MainFrame).ShowMessage((공통메세지)3, new string[1]
                    {
            (this._lbl귀속일자).Text
                    });
                    ((Control)this._dp기간FROM).Focus();
                    return false;
                }
                if (!(((Control)this._dp기간TO).Text == string.Empty))
                    return true;
                int num1 = (int)(Global.MainFrame).ShowMessage((공통메세지)3, new string[1]
                {
          (this._lbl귀속일자).Text
                });
                ((Control)this._dp기간TO).Focus();
                return false;
            }
        }
    }
}

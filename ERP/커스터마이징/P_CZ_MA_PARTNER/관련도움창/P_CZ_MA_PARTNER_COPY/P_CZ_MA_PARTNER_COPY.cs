using System;
using System.Windows.Forms;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_MA_PARTNER_COPY : Duzon.Common.Forms.CommonDialog
    {
        private string 회사코드, 회사명;

        public P_CZ_MA_PARTNER_COPY()
        {
            InitializeComponent();
        }

        public P_CZ_MA_PARTNER_COPY(string 회사코드, string 회사명, string 거래처코드, string 거래처명)
        {
            InitializeComponent();

            this.회사코드 = 회사코드;
            this.회사명 = 회사명;
            this.ctx거래처.CodeValue = 거래처코드;
            this.ctx거래처.CodeName = 거래처명;
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
        }

        private void InitEvent()
        {
            this.btn동기화.Click += new EventHandler(this.btn동기화_Click);
            this.btn종료.Click += new EventHandler(this.btn종료_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.ctx원본회사.CodeValue = this.회사코드;
            this.ctx원본회사.CodeName = this.회사명;

            if (this.회사코드 != "K100")
                this.bpc대상회사.AddItem("K100", "(주)딘텍");
            if (this.회사코드 != "K200")
                this.bpc대상회사.AddItem("K200", "(주)두베코");
            if (this.회사코드 != "S100")
                this.bpc대상회사.AddItem("S100", "DINTEC SINGAPORE PTE.LTD.");
            if (this.회사코드 != "W100")
                this.bpc대상회사.AddItem("W100", "(주)윈윈인텍");
            if (this.회사코드 != "TEST")
                this.bpc대상회사.AddItem("TEST", "(주)딘텍_테스트");
        }

        private void btn동기화_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ShowMessage("동기화 하시겠습니까 ?", "QY2") != DialogResult.Yes)
                    return;

                DBHelper.ExecuteScalar("SP_CZ_MA_PARTNER_COPY_I", new object[] { this.ctx거래처.CodeValue,
                                                                                 this.ctx원본회사.CodeValue,
                                                                                 this.bpc대상회사.QueryWhereIn_Pipe,
                                                                                 Global.MainFrame.LoginInfo.UserID });

                Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, "동기화");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn종료_Click(object sender, EventArgs e)
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

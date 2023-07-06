using System;
using System.Windows.Forms;

namespace pur
{
    public partial class P_PU_IVMNG_ETAX36524D_OPTION_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_PU_IVMNG_ETAX36524D_OPTION_SUB()
        {
            InitializeComponent();
        }

        //폼 로딩할때 저장된 셋팅값읽어다가 실행한다.
        private void P_SA_ETRADESEND_REG_OPTION_SUB_Load(object sender, EventArgs e)
        {
            #region 1.내역표시
            if (Settings.Default.Tx_내역표시구분 == "1")    //임의
            {
                rdo_Tx_All.Checked = false;
                rdo_Tx_UserText.Checked = true; 
            }
            else //if (Settings.Default.내역표시 == "0")    //모두
            {
                rdo_Tx_All.Checked = true;
                rdo_Tx_UserText.Checked = false;
            }

            txt_Tx_UserText.Text = Settings.Default.Tx_내역표시_Text;
            #endregion

            #region 2.품목표시
            if (Settings.Default.Tx_품목표시구분 == "0")    //표시안함
            {
                rdo_Tx_Item_None.Checked = true;
                rdo_Tx_Item_Kor.Checked = false;
                rdo_Tx_Item_Eng.Checked = false;
            }
            else if (Settings.Default.Tx_품목표시구분 == "1")   //기본코드
            {
                rdo_Tx_Item_None.Checked = false;
                rdo_Tx_Item_Kor.Checked = true;
                rdo_Tx_Item_Eng.Checked = false;
            }
            else if (Settings.Default.Tx_품목표시구분 == "2")   //영문명
            {
                rdo_Tx_Item_None.Checked = false;
                rdo_Tx_Item_Kor.Checked = false;
                rdo_Tx_Item_Eng.Checked = true;
            }
            
            #endregion
        }

        //화면 닫힐때 this.FormClosing 이벤트를 통해서 
        //셋팅값 저장해주거나 저장버튼을 통해서 저장한다.
        private void btn_Save_Click(object sender, EventArgs e)
        {
            //Settings.Default.매출옵션 = Convert.ToInt32(textBox1.Text);
            //Settings.Default.
            //Settings.Default.Save();

            string option = string.Empty;
            if (rdo_Tx_UserText.Checked == true)
                option = "1";   //임의
            else //if (rdo_ALL.Checked == true)
                option = "0";   //모두

            string opt_품목표시 = string.Empty;
            if (rdo_Tx_Item_None.Checked == true)
                opt_품목표시 = "0"; //표시안함
            else if (rdo_Tx_Item_Kor.Checked == true)
                opt_품목표시 = "1"; //기본코드
            else if (rdo_Tx_Item_Eng.Checked == true)
                opt_품목표시 = "2"; //영문명
          
            Settings.Default.Tx_내역표시구분 = option;
            Settings.Default.Tx_내역표시_Text = txt_Tx_UserText.Text;
            Settings.Default.Tx_품목표시구분 = opt_품목표시;

            //꼭 저장을 해줘야 한다.
            Settings.Default.Save();

            this.DialogResult = DialogResult.OK;
        }

        #region -> 결과값 리턴해줄 속성값
        public string[] 출력옵션
        {
            get
            {
                string[] Param = new string[12];

                Param[0] = Chk(rdo_Tx_All.Checked);
                Param[1] = Chk(rdo_Tx_UserText.Checked);
                Param[2] = txt_Tx_UserText.Text;

                Param[3] = Chk(rdo_Tx_Item_None.Checked);
                Param[4] = Chk(rdo_Tx_Item_Kor.Checked);
                Param[5] = Chk(rdo_Tx_Item_Eng.Checked);
                return Param;
            }
        }

        private string Chk(bool b)
        { 
            string chk = string.Empty;

            if (b == true)
                chk = "Y";
            else
                chk = "N";

            return chk;
        }
        #endregion 
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.Common.Util;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
namespace cz
{
    public partial class P_CZ_PARTNER_INTRT_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_PARTNER_INTRT_SUB()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            InitEvent();
        }
        protected override void InitPaint()
        {
            base.InitPaint();

            //dtp월.Mask = dtpTO.Mask = GetFormatDescription(DataDictionaryTypes.PR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);

            dtp조회월.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dtp대상월.Text = Global.MainFrame.GetStringYearMonth;
            //dtpTO.Text = Global.MainFrame.GetStringToday;

            SetControl str = new SetControl();
            //str.SetCombobox(cbo01, MF.GetCode(MF.코드.생산.공정경로번호, true));  // ModuleHelper에 정의된 코드가 있으면..
            str.SetCombobox(cbo공장, MF.GetCode("MA_PLANT", true));           // ModuleHelper에 정의된 코드가 없으면..
        }

        void InitEvent()
        {

            btn확인.Click += new EventHandler(btn확인_Click);
            // btn프로모션.Click += new EventHandler(btn프로모션_Click);
        }


        void btn확인_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Chk월) return;

                object[] args;
                DBHelper.ExecuteNonQuery("UP_CZ_LHE_UCOST_COPY", new object[] { MA.Login.회사코드, D.GetString(cbo공장.SelectedValue), D.GetString(dtp조회월.Text), D.GetString(dtp대상월.Text) }, out args);
                //NewNoPromo = D.GetString(args[0]);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        bool Chk월
        {
            get
            {
                if (!Checker.IsEmpty(dtp조회월) && !Checker.IsEmpty(dtp대상월))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }



    }
}

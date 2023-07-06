//********************************************************************
// ��   ��   �� : ���ȸ ## 
// ��   ��   �� : 
// ��   ��   �� : ������/������(2006-07-12)
// ��   ��   �� : ��������
// �� ��  �� �� : �������
// ����ý��۸� : ���⼱�����
// �� �� ��  �� : 
// ������Ʈ  �� : P_TR_EXBL
//********************************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;

using Duzon.Common.Forms;
using Duzon.Common.Controls;
using Duzon.Common.Util;

namespace trade
{
     public class P_TR_EXBL_OLD : Duzon.Common.Forms.PageBase
    {
        #region �� ���� ���� ����â �κ�

        /// <summary>
        /// ���� ���� ����â 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_btnBLText_Click(object sender, System.EventArgs e)
        {
            try
            {
                //DataRow row = ((DataRowView)this.m_comFgBL.SelectedItem).Row;
                //string ls_fg = this.m_comFgBL.text;

                //row = ((DataRowView)this.m_comCdCurrency.SelectedItem).Row;
                //string ls_currency = row["NAME"].ToString();

                trade.P_TR_EXBL_SUB obj = new trade.P_TR_EXBL_SUB(this.MainFrameInterface, this.m_HeadTable, cbo��������.Text, cbo��ȭ.Text);

                if (obj.ShowDialog() == DialogResult.OK)
                {
                }
                obj.Dispose();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region �� �Ǹ� ��� �κ�

        /// <summary>
        /// �Ǹ� ��� ����â
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_btnInputCost_Click(object sender, System.EventArgs e)
        {
            try
            {
                string msg = null;

                if (!this.m_IsPageActivated)
                    return;

                if (this.m_HeadTable == null)
                    return;

                this.m_Manager.Position = 0;

                DataTable ldt_table = this.m_HeadTable.GetChanges();
                if (ldt_table != null)
                {
                    // TR_M000036
                    // �۾��Ͻ� �ڷḦ ���� �����ϼž� �մϴ�. ����Ͻðڽ��ϱ�?"
                    msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000036");
                    DialogResult result = MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (!this.Save())
                            return;
                    }
                    else if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                // ���߻�����, ������ȣ, ��ǥ����, ��ǥ������ڵ�, ��ǥ������, 
                // �μ��ڵ�, �μ���, ������ڵ� ,����ڸ�, C/C �ڵ�, C/C��
                string[] ls_args = new string[11];
                ls_args[0] = "����";
                ls_args[1] = this.m_HeadTable.Rows[0]["NO_BL"].ToString();
                ls_args[2] = this.dtp��ǥ����.Text;	// ��ǥ����
                ls_args[3] = bpc�����.CodeValue.ToString();
                ls_args[4] = this.bpc�����.CodeName.ToString();
                ls_args[5] = "";
                ls_args[6] = "";
                ls_args[7] = this.bpc�����.CodeValue.ToString();;
                ls_args[8] = this.bpc�����.CodeName.ToString(); ;
                ls_args[9] = "";
                ls_args[10] = "";

                //public P_TR_EXCOST(string[] p_args, DataTable p_originTable)

                object[] args = new Object[3];
                args[0] = ls_args;
                args[1] = this.m_HeadTable;
                args[2] = 4;	// ���� : 1 , L/C : 1, 3 : ���, 4 : ����

                // Main �� ��� �ִ��� Ȯ������ ��� ������ ������ �����ϰ� �׾� ������ �׳� ���Ͻ��ѹ�����.
                if (this.MainFrameInterface.IsExistPage("P_TR_EXCOST", false))
                {
                    //- Ư�� ������ �ݱ�
                    this.UnLoadPage("P_TR_EXCOST", false);
                }

                string ls_LinePageName = this.MainFrameInterface.GetDataDictionaryItem("TRE", "INPUT_COST");

                this.LoadPageFrom("P_TR_EXCOST", ls_LinePageName, this.Grant, args);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            switch (e.ControlName)
            {
                case "bpCD_ACCT":
                    e.HelpParam.P33_TP_ACSTATS = "2";
                    break;
            }
        }

    }
}
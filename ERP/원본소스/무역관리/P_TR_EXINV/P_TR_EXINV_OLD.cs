//********************************************************************
// ��   ��   �� : 
// ��   ��   �� : 2006-06-12
// ��   ��   �� : ����
// �� ��  �� �� : ��������
// ����ý��۸� : �������
// �� �� ��  �� : ������
// ������Ʈ  �� : P_TR_EXINV
//********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Util;
using Duzon.Common.Forms;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;

namespace trade
{
	public class P_TR_EXINV_OLD : Duzon.Common.Forms.PageBase
	{
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

				if(!this.m_IsPageActivated)
					return;

				if(this.m_HeadTable == null)
					return;

				this.m_Manager.Position = 0;

				DataTable ldt_table = this.m_HeadTable.GetChanges();
				if(ldt_table != null)
				{
					// TR_M000036
					// �۾��Ͻ� �ڷḦ ���� �����ϼž� �մϴ�. ����Ͻðڽ��ϱ�?"
					msg = this.MainFrameInterface.GetMessageDictionaryItem("TR_M000036");
					DialogResult result = MessageBoxEx.Show(msg, this.PageName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

					if(result == DialogResult.Yes)
					{
						if(!this.Save())
							return;
					}
					else if(result == DialogResult.No)
					{
						return;
					}
				}

				// ���߻�����, ������ȣ, ��ǥ����, ��ǥ������ڵ�,
				// �μ��ڵ�, �μ���, ������ڵ� ,����ڸ�, C/C �ڵ�, C/C��
				string[] ls_args = new string[11];
				ls_args[0] = "����";
				ls_args[1] = this.m_HeadTable.Rows[0]["NO_INV"].ToString();
                ls_args[2] = this.dtp��������.Text;	// ��������
				ls_args[3] = this.m_CdBizarea;
				ls_args[4] = "";
				ls_args[5] = "";
				ls_args[6] = this.m_CdEmp;
				ls_args[7] = this.m_NmEmp;
				ls_args[8] = "";
				ls_args[9] = "";

				//public P_TR_EXCOST(string[] p_args, DataTable p_originTable)

				object[] args = new Object[3];
				args[0] = ls_args;
				args[1] = this.m_HeadTable;
				args[2] = 2;	// ���� : 1 , L/C : 1
			
				// Main �� ��� �ִ��� Ȯ������ ��� ������ ������ �����ϰ� �׾� ������ �׳� ���Ͻ��ѹ�����.
				if(this.MainFrameInterface.IsExistPage("P_TR_EXCOST", false))
				{
					//- Ư�� ������ �ݱ�
					this.UnLoadPage("P_TR_EXCOST", false);
				}

				string ls_LinePageName = this.MainFrameInterface.GetDataDictionaryItem("TRE", "INPUT_COST");
				
				this.LoadPageFrom("P_TR_EXCOST", ls_LinePageName, this.Grant, args);
			}
			catch(Exception ex)
			{
				this.MainFrameInterface.ShowErrorMessage(ex, this.PageName);
			}
		}

		#endregion
    }
}

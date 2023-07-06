//********************************************************************
// 작   성   자 : 김봉회 ## 
// 작   성   일 : 
// 수   성   자 : 심현주/김정근(2006-07-12)
// 모   듈   명 : 무역관리
// 시 스  템 명 : 수출관리
// 서브시스템명 : 수출선적등록
// 페 이 지  명 : 
// 프로젝트  명 : P_TR_EXBL
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
        #region ● 선적 내역 도움창 부분

        /// <summary>
        /// 선적 내역 도움창 
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

                trade.P_TR_EXBL_SUB obj = new trade.P_TR_EXBL_SUB(this.MainFrameInterface, this.m_HeadTable, cbo선적구분.Text, cbo통화.Text);

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

        #region ● 판매 경비 부분

        /// <summary>
        /// 판매 경비 도움창
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
                    // 작업하신 자료를 먼저 저장하셔야 합니다. 계속하시겠습니까?"
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

                // 경비발생구분, 관리번호, 기표일자, 기표사업장코드, 기표사업장명, 
                // 부서코드, 부서명, 담당자코드 ,담당자명, C/C 코드, C/C명
                string[] ls_args = new string[11];
                ls_args[0] = "선적";
                ls_args[1] = this.m_HeadTable.Rows[0]["NO_BL"].ToString();
                ls_args[2] = this.dtp기표일자.Text;	// 기표일자
                ls_args[3] = bpc사업장.CodeValue.ToString();
                ls_args[4] = this.bpc사업장.CodeName.ToString();
                ls_args[5] = "";
                ls_args[6] = "";
                ls_args[7] = this.bpc담당자.CodeValue.ToString();;
                ls_args[8] = this.bpc담당자.CodeName.ToString(); ;
                ls_args[9] = "";
                ls_args[10] = "";

                //public P_TR_EXCOST(string[] p_args, DataTable p_originTable)

                object[] args = new Object[3];
                args[0] = ls_args;
                args[1] = this.m_HeadTable;
                args[2] = 4;	// 송장 : 1 , L/C : 1, 3 : 통관, 4 : 선적

                // Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
                if (this.MainFrameInterface.IsExistPage("P_TR_EXCOST", false))
                {
                    //- 특정 페이지 닫기
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
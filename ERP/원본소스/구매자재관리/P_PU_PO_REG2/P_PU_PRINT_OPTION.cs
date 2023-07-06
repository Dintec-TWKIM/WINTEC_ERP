
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using C1.Win.C1FlexGrid;
using Duzon.Common.Forms.Help;
using Dass.FlexGrid;

using Duzon.ERPU;


/// <summary>
///********************************************************************
/// 작   성   자 : 신미란
/// 작   성   일 : 2010.11.09
/// 모   듈   명 : 회계
/// 시 스  템 명 : 회계관리
/// 서브시스템명 : 업체전용
/// 페 이 지  명 : 휴일등록
/// 수   정   자 : 
///********************************************************************


namespace pur   
{
    public partial class P_PU_PRINT_OPTION : Duzon.Common.Forms.CommonDialog
    {
        #region ♣ 멤버필드
        string _parent_dt_po = string.Empty;
        private string _gstr_return = string.Empty;

        #endregion

        #region ♣ 초기화
        #region -> 생성자
        public P_PU_PRINT_OPTION(string ls_dt_po)
        {
            InitializeComponent();
            _parent_dt_po = ls_dt_po;
        }
        #endregion

        #region -> InitLoad
        protected override void InitLoad()
        {
            base.InitLoad();
            tb_DT_PO.Text = _parent_dt_po;

        }
        #endregion

       
        #endregion

        #region ♣ Event

        #region -> 반영 
        private void m_btn_apply_Click(object sender, EventArgs e)
        {
            try
            {
                _gstr_return = tb_DT_PO.Text;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> 취소
        private void m_btn_exit_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
        }
        #endregion

        #endregion

        #region ♣ Method


        public string gstr_return
        {
            get { return _gstr_return; }
        }


        #endregion



    }
}


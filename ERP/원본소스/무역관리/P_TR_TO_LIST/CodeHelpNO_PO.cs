using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using C1.Win.C1FlexGrid;
using System.Collections;
using Duzon.Common.Forms.Help.Forms;
using Duzon.Common.Forms.Help;
using Dass.FlexGrid;
using Duzon.ERPU;

namespace trade
{
    public partial class CodeHelpNO_PO : Duzon.Common.Forms.CommonDialog
    {
        private bool _isPainted = false;

        // IMainFrame
        private Duzon.Common.Forms.IMainFrame m_imain;

        private DataRow Row;
        public DataRow m_Row
        {
            get { return Row; }
            set { Row = value; }
        }

        public CodeHelpNO_PO()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(Page_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(Page_Paint);
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
            LoadData();

        }
        public void LoadData()
        {
            string str = @"select NO_PO 
                           FROM tr_bl_iml
                           WHERE CD_COMPANY  = '{0}'
                           ORDER BY NO_PO";

            string Qry = string.Format(str, Global.MainFrame.LoginInfo.CompanyCode);
            DataTable dt = DBHelper.GetDataTable(Qry);
            _flex.Binding = dt;

        }
        public object[] m_return = new object[1];

        /// <summary>
        /// 페이지 로드 이벤트 핸들러(화면 초기화 작업)
        /// </summary>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
              //  this.Enabled = false;

                // 그리드 컨트롤을 초기화 한다.
               // InitGrid();
                //InitControl();
               // Application.DoEvents();
            }
            catch (Exception ex)
            {
               // this.m_imain.MsgEnd(ex);
            }
        }

        /// <summary>
        ///  그리드 생성
        /// </summary>
        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            _flex.SetCol("NO_PO", "항번", 200, false);
            _flex.Cols["NO_PO"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.SettingVersion = "1.0.0.1";
            _flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            // 그리드 이벤트 선언
           // _flex.HelpClick += new System.EventHandler(this.OnShowHelp);
            //			_flex.ValidateEdit			+= new C1.Win.C1FlexGrid.ValidateEditEventHandler(_flex_ValidateEdit);
           // _flex.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(_flex_BeforeRowColChange);
            //			_flex.StartEdit				+= new C1.Win.C1FlexGrid.RowColEventHandler(_flex_StartEdit);
           // _flex.ChangeEdit += new System.EventHandler(_flex_ChangeEdit);

        }

        private void Page_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (this._isPainted)
                return;

            try
            {
                this._isPainted = true;
                this.Enabled = true; //페이지 전체 활성

                //m_txtFrPeriod.Text = DateTime.Now.AddMonths(-3).ToShortDateString();//m_imain.GetStringToday.Substring(0,6) + "01";
                //m_txtToPeriod.Text = m_imain.GetStringToday.Substring(0, 8);
                //m_txtFrPeriod.Focus();

                //m_txtTpBusi.Text = g_sNmTpBusi;
                //m_txtAmBill.Text = g_sAmBill;
                //m_txtCdIvpartner.Text = g_sNmBiPartner;
                //m_txtCdBillpartner.Text = g_sNmIvPartner;
                //m_txtAmRcpd.Text = "0";//g_sAmBan;//
                //m_txtNoProject.Text = g_sNmPJT;

            }
            catch (Exception ex)
            {
                this.m_imain.MsgEnd(ex);
            }

        }

        /// <summary>
        /// 확인버튼 눌렸을때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn확인_Click(object sender, EventArgs e)
        {
            Row = _flex.GetDataRow(_flex.Row);
            DialogResult = DialogResult.OK;

        }

        /// <summary>
        /// 취소버튼 눌렸을때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn취소_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        //검색에서 입력하고 Enter키 눌렀을때
        private void txt검색_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                DataTable dt = null;
                if (txt검색.Text.Trim() == string.Empty)
                {
                    InitLoad();
                }
                else if( Search(txt검색.Text) == true)
                {
                     dt =  _flex.DataTable.Clone();
                    dt.Rows.Add(m_Row.ItemArray);
                    _flex.Binding = dt;
                }
            }
        }


        //코드도움에서 검색 또는 bp계정코드에서 입력이 들어왔을때
        internal bool Search(string SearchData)
        {
            bool bRet = false;
            string str = @"select NO_PO
                           FROM tr_bl_iml
                           WHERE CD_COMPANY  = '{0}' AND 
                                (  NO_PO like '%{1}%' 
                                )
                           ORDER BY NO_PO";
            string Qry = string.Format(str, Global.MainFrame.LoginInfo.CompanyCode,SearchData);

            DataTable dt = DBHelper.GetDataTable(Qry);

            if (dt.Rows.Count > 0)
            {
                bRet = true;
                Row = dt.Rows[0];
            }

            return bRet;
        }
    }
}

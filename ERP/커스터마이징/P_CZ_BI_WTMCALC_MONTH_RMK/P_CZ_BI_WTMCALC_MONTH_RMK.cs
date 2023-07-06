using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Duzon.Common.Forms;
using DzHelpFormLib;
using Duzon.Common.BpControls;
using Duzon.ERPU;
using Duzon.Common.Forms.Help;
using System.Windows.Forms;
using Duzon.ERPU.Grant;
using Dintec;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;


namespace cz
{
    public partial class P_CZ_BI_WTMCALC_MONTH_RMK : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_BI_WTMCALC_MONTH_RMK_BIZ _biz = new P_CZ_BI_WTMCALC_MONTH_RMK_BIZ();

        public string CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
        public string NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo.ToString();

        DataTable dTItem = null;

        public P_CZ_BI_WTMCALC_MONTH_RMK()
        {
            InitializeComponent();

            dtp반영월.Text = DateTime.Now.ToString("yyyyMM");

            btn조회_Click(null, null);
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();
        }

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, true);

            _flex.SetCol("DATE_MONTH", "반영월", false);
            _flex.SetCol("CD_COMPANY", "회사", false);
            _flex.SetCol("NO_EMP", "사번", 120);
            _flex.SetCol("NM_KOR", "이름", 120);
            _flex.SetCol("NM_DEPT", "부서명", 120);
            _flex.SetCol("RMK", "비고", 200);

            _flex.Cols["NO_EMP"].TextAlign = TextAlignEnum.CenterCenter;
            _flex.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;

            _flex.KeyActionEnter = KeyActionEnum.MoveDown;
            _flex.SettingVersion = "18.09.13.03";
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flex.AllowEditing = true;
        }

        private void InitEvent()
        { 
            btn저장.Click += new EventHandler(btn저장_Click);
            btn조회.Click += new EventHandler(btn조회_Click);
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = this._flex.GetChanges();

                if (!_biz.Save(dt))
                {
                    //return false;
                }

                this._flex.AcceptChanges();

                this._flex.Focus();

                Util.ShowMessage("저장 완료");
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            string datemon = dtp반영월.Text;

            dTItem = null;

            dTItem = DBMgr.GetDataTable("PS_CZ_BI_WTMCALC_MONTH_RMK_S", new object[] { CD_COMPANY, datemon, NO_EMP.Substring(0,1) });

            _flex.Redraw = false;
            _flex.Binding = dTItem;
            _flex.Redraw = true;
        }
    }
}

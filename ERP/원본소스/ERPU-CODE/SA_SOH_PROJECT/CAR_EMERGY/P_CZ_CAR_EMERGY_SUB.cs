using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_CAR_EMERGY_SUB : Duzon.Common.Forms.CommonDialog
    {
        public P_CZ_CAR_EMERGY_SUB()
        {
            InitializeComponent();
        }

        #region ♣ 초기화

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGrid();
            InitEvent();
        }

        void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            _flex.SetCol("CAR_NO", "공장코드", 100, false);
            _flex.SetCol("CAR_NAME", "공장", 100, false);
            _flex.SetCol("NAME", "요청일", 100, false);

            _flex.LoadUserCache("P_CZ_CAR_EMERGY_SUB_flex");
            _flex.EndSetting(Dass.FlexGrid.GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, Dass.FlexGrid.SumPositionEnum.None);

            _flex.SettingVersion = "1.0.0.1";

        }

        protected override void InitPaint()
        {
            base.InitPaint();

            btn조회_Click(null, null);
        }

        void InitEvent()
        {
            btn조회.Click += new EventHandler(btn조회_Click);
            btn적용.Click += new EventHandler(btn적용_Click);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _flex.SaveUserCache("P_CZ_CAR_EMERGY_SUB_flex");
        }

        #endregion

        #region ♣ 화면내버튼이벤트

        void btn조회_Click(object sender, EventArgs e)
        {
            try
            {

                List<object> list = new List<object>();
                list.Add(MA.Login.회사코드);
                list.Add(ctx차량정보.Text);
                list.Add(ctx차량명.Text);

                DataTable dt = DBHelper.GetDataTable("UP_CZ_CAR_MGT_S", list.ToArray());

                _flex.Binding = dt;

                if (!_flex.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flex.HasNormalRow) return;

                DataRow[] ldr_temp = _flex.DataTable.Select("S = 'Y'");

                if (ldr_temp != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else return;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion
    }
}

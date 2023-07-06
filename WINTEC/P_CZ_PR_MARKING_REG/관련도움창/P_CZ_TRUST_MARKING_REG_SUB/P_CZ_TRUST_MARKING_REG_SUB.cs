using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_TRUST_MARKING_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        P_CZ_TRUST_MARKING_REG_SUB_BIZ _biz = new P_CZ_TRUST_MARKING_REG_SUB_BIZ();

        public P_CZ_TRUST_MARKING_REG_SUB()
        {
            InitializeComponent();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_SO", "수주번호", 100);
            this._flex.SetCol("SEQ_SO", "순번", 100);
            this._flex.SetCol("CD_ITEM", "품목코드", 100);
            this._flex.SetCol("NM_ITEM", "품목명", 100);
            this._flex.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex.SetCol("NO_SERIAL", "시리얼", 100);
            this._flex.SetCol("NO_ID", "ID번호", 100);

            this._flex.AddDummyColumn("S");
            this._flex.SettingVersion = "0.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp실적일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.dtp실적일자.EndDateToString = Global.MainFrame.GetStringToday;
        }

        private void InitEvent()
        {
            this.btn조회.Click += btn조회_Click;
            this.btn제거.Click += btn제거_Click;
            this.btn저장.Click += btn저장_Click;

            this._flex.BeforeCodeHelp += _flex_BeforeCodeHelp;
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P09_CD_PLANT = Global.MainFrame.LoginInfo.CdPlant;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.dtp실적일자.StartDateToString,
                                                     this.dtp실적일자.EndDateToString,
                                                     this.txt수주번호.Text.ToString() });

                this._flex.Binding = dt;

                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn제거_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            try
            {
                if (!this._flex.HasNormalRow) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0) return;
                if (Global.MainFrame.ShowMessage("선택된 TRUST마킹 이력을 삭제 하시겠습니까?", "QY2") != DialogResult.Yes) return;

                foreach (DataRow dr in dataRowArray)
                {
                    dr.Delete();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn저장_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._biz.Save(this._flex.GetChanges())) return;

                this._flex.AcceptChanges();

                Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

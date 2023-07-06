using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_FI_Z_HAATZ_CARDCLOSE_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz;
        
        public P_FI_Z_HAATZ_CARDCLOSE_SUB()
        {
            this.InitializeComponent();
            this._biz = new P_CZ_FI_CARD_TEMP_VAT_BIZ();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitEvent();
            this.InitOneGrid();
            this.InitGird();
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn저장.Click += new EventHandler(this.btn저장_Click);
            this.btn종료.Click += new EventHandler(this.btn종료_Click);
        }

        private void InitOneGrid()
        {
            this.oneGrid1.UseCustomLayout = true;
            this.bppnl마감연도.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
        }

        private void InitGird()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("YM_CLOSE", "월", 100, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flex.SetCol("TP_CLOSE", "마감여부", 100, true);
            this._flex.SetCol("ID_USER", "최종마감자", false);
            this._flex.SetCol("NM_USER", "최종마감자", 120, false);
            this._flex.SetCol("DT_CLOSE", "최종마감일", 120, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.UseMultySorting = true;
            this._flex.ExtendLastCol = true;
            this._flex.VerifyPrimaryKey = new string[1]
            {
        "YM_CLOSE"
            };
            this._flex.VerifyNotNull = new string[1] { "YM_CLOSE" };
            this._flex.SettingVersion = "1.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        protected override void InitPaint()
        {
            this.dtp마감연도.Text = Global.MainFrame.GetStringYearMonth.Substring(0, 4);
            this._flex.SetDataMap("TP_CLOSE", MA.GetCodeUser(new string[2]
            {
        "0",
        "1"
            }, new string[2] { "미마감", "마감" }, false), "CODE", "NAME");
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.마감연도체크)
                    return;
                this._flex.Binding = (object)this._biz.Search_CARDCLOSE_SUB(new List<object>()
        {
          (object) MA.Login.회사코드,
          (object) this.dtp마감연도.Text
        }.ToArray());
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
                if (!this._biz.Save_CARDCLOSE_SUB(this._flex.DataTable))
                    return;
                int num = (int)Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn종료_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool 마감연도체크 => Checker.IsValid(this.dtp마감연도, true, "마감연도");
    }
}

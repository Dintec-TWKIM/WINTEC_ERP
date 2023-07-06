using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_FI_Z_SSCARD_MAPPING_SUB : Duzon.Common.Forms.CommonDialog
    {
        private Hashtable ht = new Hashtable();
        private P_CZ_FI_CARD_TEMP_VAT_BIZ _biz;
        
        public P_FI_Z_SSCARD_MAPPING_SUB()
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
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);
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
            this._flex.SetCol("CD_ACCT", "계정코드", false);
            this._flex.SetCol("NM_ACCT", "계정명", 120, false);
            this._flex.SetCol("DT_ACCT", "회계일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("CD_PC", "회계단위", false);
            this._flex.SetCol("NO_DOCU", "전표번호", 100, false);
            this._flex.SetCol("NO_DOLINE", "라인번호", 70, false);
            this._flex.SetCol("NM_NOTE", "적요", 200, false);
            this._flex.SetCol("CD_PARTNER", "거래처", false);
            this._flex.SetCol("LN_PARTNER", "거래처", 100, false);
            this._flex.SetCol("AM_AMT", "금액", 100, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.UseMultySorting = true;
            this._flex.ExtendLastCol = true;
            this._flex.SettingVersion = "1.0.0.1";
            this._flex.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        protected override void InitPaint()
        {
            this.dtp회계일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.dtp회계일자.EndDateToString = Global.MainFrame.GetStringToday;
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.회계단위체크)
                    return;
                this._flex.Binding = (object)this._biz.Search_MAPPING_SUB(new List<object>()
        {
          (object) MA.Login.회사코드,
          (object) this.bpt회계단위.CodeValue,
          (object) this.dtp회계일자.StartDateToString,
          (object) this.dtp회계일자.EndDateToString,
          D.GetNull((object) this.bpc거래처.QueryWhereIn_Pipe),
          (object) this.txt전표번호.Text,
          (object) this.cur금액.DecimalValue
        }.ToArray());
                if (!this._flex.HasNormalRow)
                {
                    int num = (int)Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                this.ht.Add((object)"회계단위", this._flex[this._flex.Row, "CD_PC"]);
                this.ht.Add((object)"전표번호", this._flex[this._flex.Row, "NO_DOCU"]);
                this.ht.Add((object)"라인번호", this._flex[this._flex.Row, "NO_DOLINE"]);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn취소_Click(object sender, EventArgs e)
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

        private bool 회계단위체크 => Checker.IsValid(this.bpt회계단위, true, "회계단위");

        internal Hashtable GetHashtable => this.ht;
    }
}
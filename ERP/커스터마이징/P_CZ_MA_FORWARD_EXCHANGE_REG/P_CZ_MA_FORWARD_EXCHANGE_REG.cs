using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Dintec;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_MA_FORWARD_EXCHANGE_REG : PageBase
    {
        P_CZ_MA_FORWARD_EXCHANGE_REG_BIZ _biz = new P_CZ_MA_FORWARD_EXCHANGE_REG_BIZ();
        DataTable dt은행;

        public P_CZ_MA_FORWARD_EXCHANGE_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flex };
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("CD_BANK", "은행", 80, true);
            this._flex.SetCol("DT_CONTRACT", "체결일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_EXCH", "선물환일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_MONTH", "월(선물환)", 80);
            this._flex.SetCol("RT_EXCH", "선물환율", 80, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("AM_EX", "매입량", 100, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM", "매입원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("SWAP", "스왑", 60, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("COMMISSION", "수수료", 60, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("RT_EVALUATION", "평가환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("AM_PL", "평가손익(원화)", 100, false, typeof(decimal), FormatTpType.MONEY);

            this._flex.Cols["RT_EXCH"].Format = "#,##0.00";
            this._flex.Cols["RT_EVALUATION"].Format = "#,##0.00";
            this._flex.Cols["SWAP"].Format = "#,##0.00";
            this._flex.Cols["COMMISSION"].Format = "#,##0.00";

            this._flex.VerifyPrimaryKey = new string[] { "CD_BANK", "DT_EXCH" };

            this._flex.SettingVersion = "1.0.0.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        private void InitEvent()
        {
            this._flex.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
            this._flex.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);
        }

        protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                dt은행 = Global.MainFrame.GetComboDataCombine("S;MA_CODEDTL2;CZ_MA00008");

                this.cbo은행.DataSource = dt은행.Copy();
                this.cbo은행.ValueMember = "CODE";
                this.cbo은행.DisplayMember = "NAME";

                this._flex.SetDataMap("CD_BANK", dt은행.Copy(), "CODE", "NAME");

                this.dtp선물환일자.StartDateToString = Global.MainFrame.GetDateTimeToday().Year.ToString() + "0101";
                this.dtp선물환일자.EndDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(6).ToString("yyyyMMdd");

                this.dtp체결일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).Year.ToString() + "0101";
                this.dtp체결일자.EndDateToString = Global.MainFrame.GetStringToday;

                this.dtp선물환월.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH, FormatFgType.INSERT);
                //this.dtp선물환월.Text = Global.MainFrame.GetStringFirstDayInMonth;
                this.dtp선물환월.ToDayDate = Global.MainFrame.GetDateTimeToday();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.dtp선물환일자.StartDateToString,
                                                                     this.dtp선물환일자.EndDateToString,
                                                                     this.dtp체결일자.StartDateToString,
                                                                     this.dtp체결일자.EndDateToString,
                                                                     this.dtp선물환월.Text,
                                                                     this.cbo은행.SelectedValue,
                                                                     (this.chk선물일자미도래.Checked == true ? "Y" : "N") });

                if (!this._flex.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!BeforeAdd()) return;

                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;

                this._flex.AddFinished();
                this._flex.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;
                
                this._flex.Rows.Remove(this._flex.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex.IsDataChanged == false) return false;

            DataTable dt = this._flex.GetChanges();

            if (!_biz.Save(dt)) return false;

            this._flex.AcceptChanges();

            return true;
        }

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;

            try
            {
                flexGrid = (sender as FlexGrid);
                if (flexGrid == null) return;

                switch (flexGrid.Cols[e.Col].Name)
                {
                    case "CD_BANK":
                        flexGrid["COMMISSION"] = this.dt은행.Select("CODE = '" + D.GetString(flexGrid["CD_BANK"]) + "'")[0][2];
                        break;
                    case "RT_EXCH":
                        flexGrid["AM"] = (D.GetDecimal(flexGrid["RT_EXCH"]) * D.GetDecimal(flexGrid["AM_EX"]));
                        break;
                    case "AM_EX":
                        flexGrid["AM"] = (D.GetDecimal(flexGrid["RT_EXCH"]) * D.GetDecimal(flexGrid["AM_EX"]));
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            try
            {
                this.평균계산();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void 평균계산()
        {
            DataTable dt;

            try
            {
                dt = this._flex.DataTable.Copy();

                decimal am = D.GetDecimal(dt.Compute("SUM(AM)", string.Empty));
                decimal amEx = D.GetDecimal(dt.Compute("SUM(AM_EX)", string.Empty));

                if (amEx > 0)
                    this._flex[this._flex.Rows.Fixed - 1, "RT_EXCH"] = string.Format("{0:" + this._flex.Cols["RT_EXCH"].Format + "}", decimal.Divide(am, amEx));

                this._flex[this._flex.Rows.Fixed - 1, "SWAP"] = string.Format("{0:" + this._flex.Cols["SWAP"].Format + "}", D.GetDecimal(dt.Compute("AVG(SWAP)", string.Empty)));
                this._flex[this._flex.Rows.Fixed - 1, "COMMISSION"] = string.Format("{0:" + this._flex.Cols["COMMISSION"].Format + "}", D.GetDecimal(dt.Compute("AVG(COMMISSION)", string.Empty)));
                this._flex[this._flex.Rows.Fixed - 1, "RT_EVALUATION"] = string.Format("{0:" + this._flex.Cols["RT_EXCH"].Format + "}", D.GetDecimal(dt.Compute("AVG(RT_EVALUATION)", string.Empty)));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

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
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_PACK_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_SA_PACK_REG_SUB_BIZ _biz;

        public DataRow ReturnHeaderData
        {
            get
            {
                if(this._flexH.HasNormalRow == false)
                {
                    return null;
                }
                else
                {
                    return this._flexH.GetDataRow(this._flexH.Row);
                }
            }
        }

        public DataRow[] ReturnLineData
        {
            get
            {
                if (this._flexL.HasNormalRow == false)
                {
                    return new DataRow[0];
                }
                else
                {
                    return this._flexL.DataTable.Select("NO_GIR = '" + this._flexH["NO_GIR"] + "'");
                }
            }
        }

        public P_CZ_SA_PACK_REG_SUB()
        {
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_CZ_SA_PACK_REG_SUB_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.ctx회사.CodeValue = Global.MainFrame.LoginInfo.CompanyCode;
            this.ctx회사.CodeName = Global.MainFrame.LoginInfo.CompanyName;

            this.dtp의뢰일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp의뢰일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp의뢰일자.EndDate = Global.MainFrame.GetDateTimeToday();
        }

        private void InitGrid()
        {
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NO_GIR", "의뢰번호", 80);
            this._flexH.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_PARTNER", "매출처", 200);
            this._flexH.SetCol("NM_VESSEL", "호선", 200);

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("SEQ_GIR", "의뢰항번", 80);
            this._flexL.SetCol("NO_FILE", "파일번호", 80);
            this._flexL.SetCol("NO_DSP", "파일항번", 80);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 100);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품목코드", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품목명", 100);
            this._flexL.SetCol("QT_GIR", "의뢰수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.DoubleClick += new EventHandler(this._flexH_DoubleClick);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = _biz.Search(new object[] { this.ctx회사.CodeValue,
                                                this.txt의뢰번호.Text,
                                                this.dtp의뢰일자.StartDateToString,
                                                this.dtp의뢰일자.EndDateToString });
                this._flexH.Binding = dt;

                if (!this._flexH.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string filter;

            try
            {
                if (this._flexH.HasNormalRow == false) return;

                filter = "NO_GIR = '" + this._flexH["NO_GIR"] + "'";
                dt = null;

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { this.ctx회사.CodeValue,
                                                               D.GetString(this._flexH["NO_GIR"]) });
                }

                this._flexL.BindingAdd(dt, filter);
                this._flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flexH.HasNormalRow == false) return;
                if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

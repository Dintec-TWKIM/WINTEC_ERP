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
using Dintec;

namespace cz
{
    public partial class P_CZ_PU_PACK_MNG : PageBase
    {
        P_CZ_PU_PACK_MNG_BIZ _biz = new P_CZ_PU_PACK_MNG_BIZ();

        public P_CZ_PU_PACK_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp포장일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
            this.dtp포장일자.EndDateToString = Global.MainFrame.GetStringToday;
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NM_COMPANY", "회사", 100);
            this._flexH.SetCol("NO_PO", "발주번호", 100);
            this._flexH.SetCol("NO_PACK", "포장번호", 80);
            this._flexH.SetCol("DT_PACK", "포장일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_TYPE", "포장유형", 80);
            this._flexH.SetCol("NM_SIZE", "포장사이즈", 100);
            this._flexH.SetCol("WEIGHT_GROSS", "총중량", 60, false, typeof(decimal));
            this._flexH.SetCol("WEIGHT_NET", "순중량", 60, false, typeof(decimal));
            this._flexH.SetCol("SIZE_X", "가로", 60, false, typeof(decimal));
            this._flexH.SetCol("SIZE_Y", "세로", 60, false, typeof(decimal));
            this._flexH.SetCol("SIZE_Z", "높이", 60, false, typeof(decimal));
            this._flexH.SetCol("CD_LOCATION", "로케이션", 60);
            this._flexH.SetCol("NM_INSERT", "등록자", 60);
            this._flexH.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_UPDATE", "수정자", 60);
            this._flexH.SetCol("DTS_UPDATE", "수정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DC_RMK", "비고", 150, true);

            this._flexH.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
            this._flexH.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";

            this._flexH.ExtendLastCol = true;
            this._flexH.SetDummyColumn(new string[] { "S" });
            
            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NO_LINE", "발주항번", 80);
            this._flexL.SetCol("NO_FILE", "파일번호", 100);
            this._flexL.SetCol("NO_DSP", "순번", 60);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품목코드", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품목명", 100);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 100);
            this._flexL.SetCol("QT_PACK", "포장수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
        }
        #endregion
        
        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this._flexH.Binding = this._biz.Search(new object[] { this.ctx회사.CodeValue,
                                                                      this.dtp포장일자.StartDateToString,
                                                                      this.dtp포장일자.EndDateToString,
                                                                      this.txt발주번호.Text });

                if (!this._flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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

                this._flexH.Rows.Remove(this._flexH.Row);
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
            try
            {
                if (!base.SaveData() || !base.Verify()) return false;
                if (this._flexH.IsDataChanged == false) return false;

                DataTable dtH = this._flexH.GetChanges();

                if (!this._biz.Save(dtH)) return false;

                this._flexH.AcceptChanges();

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }
        #endregion

        #region 그리드 이벤트
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dtL;
            string key1, key, key2, filter;

            try
            {
                if (this._flexH.HasNormalRow == false) return;


                key = D.GetString(this._flexH["CD_COMPANY"]);
                key1 = D.GetString(this._flexH["NO_PO"]);
                key2 = D.GetString(this._flexH["NO_PACK"]);
                
                filter = "CD_COMPANY = '" + key + "' AND NO_PO = '" + key1 + "' AND NO_PACK = '" + key2 + "'";
                dtL = null;

                if (this._flexH.DetailQueryNeed == true)
                {
                    dtL = this._biz.SearchDetail(new object[] { key,
                                                                key1,
                                                                key2 });
                }

                this._flexL.BindingAdd(dtL, filter);
                this._flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion
    }
}

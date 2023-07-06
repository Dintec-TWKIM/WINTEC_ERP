using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dass.FlexGrid;
using Duzon.ERPU.Grant;
using Duzon.Common.Forms.Help;
using Dintec;

namespace cz
{
    public partial class P_CZ_SA_UNINV_MNG : PageBase
    {
        P_CZ_SA_UNINV_MNG_BIZ _biz = new P_CZ_SA_UNINV_MNG_BIZ();

        public P_CZ_SA_UNINV_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();
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

            this.dtp수주일자.StartDateToString = MainFrameInterface.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp수주일자.EndDateToString = MainFrameInterface.GetStringToday;
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            UGrant ugrant = new UGrant();
            bool 수정권한 = ugrant.GrantButton(Global.MainFrame.CurrentPageID, "YN_AM");

            #region Header
            this._flexH.BeginSetting(2, 1, false);

            this._flexH.SetCol("NO_SO", "수주번호", 80);
            this._flexH.SetCol("NO_PO_PARTNER", "매출처발주번호", 80);
            this._flexH.SetCol("NO_CLAIM", "클레임번호", 80);
            this._flexH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_EMP", "담당자", 60);
            this._flexH.SetCol("TP_SO", "수주유형", 100, 수정권한);
            this._flexH.SetCol("LN_PARTNER", "매출처", 100);
            this._flexH.SetCol("NM_VESSEL", "호선명", 100);

            this._flexH.SetCol("AM_EX_SO", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_SO", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SetCol("AM_EX_OUT", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_OUT", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_IV", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_IV", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_FREE", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_FREE", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SetCol("AM_EX_REMAIN", "외화잔액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_REMAIN", "원화잔액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SetCol("AM_EX_AD", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_AD", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SetCol("AM_EX_RETURN", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_RETURN", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_IV_RETURN", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_IV_RETURN", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_RETURN_FREE", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_RETURN_FREE", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH.SetCol("QT_SO", "수주", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_OUT", "출고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_RETURN", "반품", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_IV", "매출", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_IV_RETURN", "반품매출", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            
            this._flexH.SetCol("DC_RMK1", "커미션", 100);
            this._flexH.SetCol("DC_RMK", "수통비고", 100);

            this._flexH[0, this._flexH.Cols["AM_EX_SO"].Index] = "수주";
            this._flexH[0, this._flexH.Cols["AM_SO"].Index] = "수주";

            this._flexH[0, this._flexH.Cols["AM_EX_OUT"].Index] = "출고";
            this._flexH[0, this._flexH.Cols["AM_OUT"].Index] = "출고";
            this._flexH[0, this._flexH.Cols["AM_EX_IV"].Index] = "매출";
            this._flexH[0, this._flexH.Cols["AM_IV"].Index] = "매출";
            this._flexH[0, this._flexH.Cols["AM_EX_FREE"].Index] = "무상공급";
            this._flexH[0, this._flexH.Cols["AM_FREE"].Index] = "무상공급";
            
            this._flexH[0, this._flexH.Cols["AM_EX_AD"].Index] = "선발행";
            this._flexH[0, this._flexH.Cols["AM_AD"].Index] = "선발행";

            this._flexH[0, this._flexH.Cols["AM_EX_RETURN"].Index] = "반품";
            this._flexH[0, this._flexH.Cols["AM_RETURN"].Index] = "반품";
            this._flexH[0, this._flexH.Cols["AM_EX_IV_RETURN"].Index] = "반품매출";
            this._flexH[0, this._flexH.Cols["AM_IV_RETURN"].Index] = "반품매출";
            this._flexH[0, this._flexH.Cols["AM_EX_RETURN_FREE"].Index] = "무상반품";
            this._flexH[0, this._flexH.Cols["AM_RETURN_FREE"].Index] = "무상반품";

            this._flexH[0, this._flexH.Cols["QT_SO"].Index] = "수량";
            this._flexH[0, this._flexH.Cols["QT_OUT"].Index] = "수량";
            this._flexH[0, this._flexH.Cols["QT_RETURN"].Index] = "수량";
            this._flexH[0, this._flexH.Cols["QT_IV"].Index] = "수량";
            this._flexH[0, this._flexH.Cols["QT_IV_RETURN"].Index] = "수량";

            this._flexH.ExtendLastCol = true;

            this._flexH.SetDataMap("TP_SO", DBHelper.GetDataTable(@"SELECT TP_SO AS CODE,
	                                                                       NM_SO AS NAME
                                                                    FROM SA_TPSO
                                                                    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                   "AND TP_SO IN ('2000', '2200', '3000', '3200', '2300', '3300')"), "CODE", "NAME");

            this._flexH.SettingVersion = "1.0.0.0";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flexL.BeginSetting(2, 1, false);

            if (수정권한)
                this._flexL.SetCol("YN_AM", "청구여부", 40, 수정권한, CheckTypeEnum.Y_N);
            
            this._flexL.SetCol("NO_IO", "출고번호", 80);
            this._flexL.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DT_DELAY", "경과일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NO_IOLINE", "출고항번", 80);
            this._flexL.SetCol("SEQ_SO", "수주항번", 80);
            this._flexL.SetCol("NO_DSP", "순번", 80);
            this._flexL.SetCol("CD_ITEM", "품목코드", 80);
            this._flexL.SetCol("NM_ITEM", "품목명", 80);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 80);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 80);
            this._flexL.SetCol("TP_GI", "수주", 120, 수정권한);
            this._flexL.SetCol("NM_QTIOTP", "출고", 100);

            this._flexL.SetCol("AM_EX_SO", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_SO", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexL.SetCol("AM_EX_OUT", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_OUT", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_IV", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_IV", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_FREE", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_FREE", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexL.SetCol("AM_EX_REMAIN", "외화잔액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_REMAIN", "원화잔액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexL.SetCol("AM_EX_RETURN", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_RETURN", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_IV_RETURN", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_IV_RETURN", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_RETURN_FREE", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_RETURN_FREE", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            
            this._flexL.SetCol("QT_SO", "수주", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_OUT", "출고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_RETURN", "반품", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_IV", "매출", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_IV_RETURN", "반품매출", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexL.SetCol("DC2", "수주비고", 100, 수정권한);
            this._flexL.SetCol("DC_RMK1", "비고1", 100, 수정권한);

            this._flexL[0, this._flexL.Cols["TP_GI"].Index] = "출고형태";
            this._flexL[0, this._flexL.Cols["NM_QTIOTP"].Index] = "출고형태";
            
            this._flexL[0, this._flexL.Cols["AM_EX_SO"].Index] = "수주";
            this._flexL[0, this._flexL.Cols["AM_SO"].Index] = "수주";

            this._flexL[0, this._flexL.Cols["AM_EX_OUT"].Index] = "출고";
            this._flexL[0, this._flexL.Cols["AM_OUT"].Index] = "출고";
            this._flexL[0, this._flexL.Cols["AM_EX_IV"].Index] = "매출";
            this._flexL[0, this._flexL.Cols["AM_IV"].Index] = "매출";
            this._flexL[0, this._flexL.Cols["AM_EX_FREE"].Index] = "무상공급";
            this._flexL[0, this._flexL.Cols["AM_FREE"].Index] = "무상공급";

            this._flexL[0, this._flexL.Cols["AM_EX_RETURN"].Index] = "반품";
            this._flexL[0, this._flexL.Cols["AM_RETURN"].Index] = "반품";
            this._flexL[0, this._flexL.Cols["AM_EX_IV_RETURN"].Index] = "반품매출";
            this._flexL[0, this._flexL.Cols["AM_IV_RETURN"].Index] = "반품매출";
            this._flexL[0, this._flexL.Cols["AM_EX_RETURN_FREE"].Index] = "무상반품";
            this._flexL[0, this._flexL.Cols["AM_RETURN_FREE"].Index] = "무상반품";

            this._flexL[0, this._flexL.Cols["QT_SO"].Index] = "수량";
            this._flexL[0, this._flexL.Cols["QT_OUT"].Index] = "수량";
            this._flexL[0, this._flexL.Cols["QT_RETURN"].Index] = "수량";
            this._flexL[0, this._flexL.Cols["QT_IV"].Index] = "수량";
            this._flexL[0, this._flexL.Cols["QT_IV_RETURN"].Index] = "수량";

            this._flexL.ExtendLastCol = true;
            this._flexL.EnabledHeaderCheck = false;

            this._flexL.SetDataMap("TP_GI", DBHelper.GetDataTable(@"SELECT CD_QTIOTP AS CODE,
	                                                                       NM_QTIOTP AS NAME 
                                                                    FROM MM_EJTP
                                                                    WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                   "AND CD_QTIOTP IN ('200', '210', '220', '230', '240', '241', '250', '251')"), "CODE", "NAME");

            this._flexL.SettingVersion = "1.0.0.0";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexL.SetExceptSumCol("DT_DELAY");
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (!Checker.IsValid(this.dtp수주일자, true, this.lbl수주일자.Text)) return false;

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.txt수주번호.Text,
                                                                      this.dtp수주일자.StartDateToString,
                                                                      this.dtp수주일자.EndDateToString,
                                                                      this.txt매출처발주번호.Text,
                                                                      this.ctx매출처.CodeValue,
                                                                      this.ctx호선.CodeValue,
                                                                      (this.chk미매출.Checked == true ? "Y" : "N"),
                                                                      (this.chk미청구.Checked == true ? "Y" : "N"),
                                                                      (this.chk무상공급.Checked == true ? "Y" : "N"),
                                                                      (this.chk잔액0제외.Checked == true ? "Y" : "N") });

                if (!_flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
            if (this._flexH.IsDataChanged == false && this._flexL.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flexH.GetChanges(), this._flexL.GetChanges())) return false;

            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();

            return true;
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt = null;
            string key, filter;

            try
            {
                key = D.GetString(this._flexH["NO_SO"]);
                filter = "NO_SO ='" + key + "'";

                if (this._flexH.DetailQueryNeed)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               key });
                }

                this._flexL.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string colname = this._flexL.Cols[e.Col].Name;
                string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
                string newValue = ((FlexGrid)sender).EditData;

                switch (colname)
                {
                    case "YN_AM":
                        if (string.IsNullOrEmpty(D.GetString(this._flexL["NO_IO"])))
                        {
                            this._flexL[e.Row, "YN_AM"] = D.GetDecimal(oldValue);
                            this.ShowMessage("출고되지 않은 품목 입니다.");
                            e.Cancel = true;
                            return;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}

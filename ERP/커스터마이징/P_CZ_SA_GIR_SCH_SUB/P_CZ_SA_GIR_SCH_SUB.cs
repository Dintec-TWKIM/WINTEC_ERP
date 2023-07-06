using System;
using System.Collections;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;

namespace cz
{
    public partial class P_CZ_SA_GIR_SCH_SUB : CommonDialog
    {
        private P_CZ_SA_GIR_SCH_SUB_BIZ _biz = new P_CZ_SA_GIR_SCH_SUB_BIZ();
        private string _fgIo = string.Empty;
        private bool 포장여부;

        public string[] returnParams
        {
            get
            {
                string[] strArray = new string[9];
                strArray[0] = D.GetString(this._flexH["NO_GIR"]);
                strArray[1] = D.GetString(this._flexH["CD_PLANT"]);
                return strArray;
            }
        }

        public string SetFgIo
        {
            set
            {
                this._fgIo = value;
            }
        }

        public P_CZ_SA_GIR_SCH_SUB(string YN_RETURN, bool 포장여부)
        {
            try
            {
                this.InitializeComponent();

                this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
                this.txt반품여부.Text = YN_RETURN;
                this.포장여부 = 포장여부;

                if (this.포장여부 == true)
                    this.TitleText = "포장업무협조전조회";
                else
                    this.TitleText = "물류업무협조전조회";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NO_GIR", "의뢰번호", 100);
            this._flexH.SetCol("STA_GIR", "진행상태", 80);
            this._flexH.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("LN_PARTNER", "매출처", 100);
            this._flexH.SetCol("NM_KOR", "의뢰자", 100);
            this._flexH.SetCol("CD_PLANT", "공장", 100);
            this._flexH.SetCol("TP_BUSI", "거래구분", 80);
            this._flexH.SetCol("NM_RMK", "협조내용", 160);
            this._flexH.SetCol("DC_RMK", "상세요청", 160);

            this._flexH.SetDataMap("STA_GIR", Global.MainFrame.GetComboDataCombine("N;CZ_SA00030"), "CODE", "NAME");

            this._flexH.ExtendLastCol = true;
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexH.LoadUserCache("P_SA_GIR_SCH_SUB_flexH");
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("CD_ITEM", "품목코드", 120);
            this._flexL.SetCol("NM_ITEM", "품목명", 120);
            this._flexL.SetCol("STND_ITEM", "규격", 65);
            this._flexL.SetCol("QT_GIR", "수량", 65, false, typeof(Decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UNIT", "단위", 65);

            if (this._fgIo == string.Empty)
            {
                this._flexL.SetCol("UM", "단가", 100, false, typeof(Decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexL.SetCol("AM_GIR", "금액", 100, false, typeof(Decimal), FormatTpType.FOREIGN_MONEY);
                this._flexL.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(Decimal), FormatTpType.MONEY);
                this._flexL.SetCol("AM_VAT", "부가세", 100, false, typeof(Decimal), FormatTpType.MONEY);
            }

            this._flexL.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(Decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("STA_GIR", "의뢰상태", 60);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 150, false);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 150, false);
            this._flexL.SetCol("DT_REQGI", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DT_DUEDATE", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NO_SO", "수주번호", 100);
            this._flexL.SetCol("NO_PROJECT", "프로젝트", 100, false);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", 100, false);

            this._flexL.SettingVersion = "1.0.0.2";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            
            if (this._fgIo == string.Empty)
                this._flexL.SetExceptSumCol("UM");

            this._flexL.LoadUserCache("P_SA_GIR_SCH_SUB_flexL");
            #endregion
            
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = Global.MainFrame.GetComboData("S;MA_PLANT", "S;PU_C000016", "S;SA_B000016");
            
            this.cbo출고공장.DataSource = comboData.Tables[0];
            this.cbo출고공장.DisplayMember = "NAME";
            this.cbo출고공장.ValueMember = "CODE";
            
            this._flexH.SetDataMap("CD_PLANT", comboData.Tables[0], "CODE", "NAME");
            this._flexH.SetDataMap("TP_BUSI", comboData.Tables[1], "CODE", "NAME");
            this._flexL.SetDataMap("STA_GIR", comboData.Tables[2], "CODE", "NAME");

            this.dtp의뢰일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
            this.dtp의뢰일자.EndDateToString = Global.MainFrame.GetStringToday;
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);

            this.ctx프로젝트.QueryBefore += new BpQueryHandler(this.ctx프로젝트_QueryBefore);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.DoubleClick += new EventHandler(this._flexH_DoubleClick);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.txt협조전번호.Text,
                                                                      D.GetString(this.cbo출고공장.SelectedValue),
                                                                      this.ctx매출처.CodeValue,
                                                                      this.dtp의뢰일자.StartDateToString,
                                                                      this.dtp의뢰일자.EndDateToString,
                                                                      this.txt반품여부.Text,
                                                                      this._fgIo,
                                                                      this.ctx프로젝트.CodeValue,
                                                                      Global.MainFrame.LoginInfo.EmployeeNo,
                                                                      (this.포장여부 == true ? "Y" : "N") });
                if (!this._flexH.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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
                if (!this._flexL.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
            this._flexH.SaveUserCache("P_SA_GIR_SCH_SUB_flexH");
            this._flexL.SaveUserCache("P_SA_GIR_SCH_SUB_flexL");
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;

                string @string = D.GetString(this._flexH[e.NewRange.r1, "NO_GIR"]);
                string filter = "NO_GIR = '" + @string + "'";

                if (this._flexH.DetailQueryNeed)
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode, 
                                                               @string, 
                                                               (this.포장여부 == true ? "Y" : "N") });
                
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
                if (!this._flexH.HasNormalRow) return;
                if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;
                    
                this.btn적용_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx프로젝트_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                e.HelpParam.P41_CD_FIELD1 = "프로젝트";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
    }
}

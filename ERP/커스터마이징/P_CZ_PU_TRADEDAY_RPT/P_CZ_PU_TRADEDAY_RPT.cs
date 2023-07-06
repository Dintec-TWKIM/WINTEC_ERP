using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace cz
{
    public partial class P_CZ_PU_TRADEDAY_RPT : PageBase
    {
        #region ♣ 생성자 & 변수 선언
        private P_CZ_PU_TRADEDAY_RPT_BIZ _biz;
        private bool is재고자산수불부;

        public P_CZ_PU_TRADEDAY_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }
        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        protected override void InitLoad()
        {
            this.is재고자산수불부 = (this.PageID == "P_CZ_PU_TRADEDAY_RPT");

            this.InitGridH();
            this.InitGridL();
            this.InitEvent();
            this._biz = new P_CZ_PU_TRADEDAY_RPT_BIZ();
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.InitCustomLayout();

            DataSet comboData = this.GetComboData(new string[2] { "NC;MA_PLANT", "S;MA_B000010" });

            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.DisplayMember = "NAME";

            this.cbo계정구분.DataSource = comboData.Tables[1];
            this.cbo계정구분.ValueMember = "CODE";
            this.cbo계정구분.DisplayMember = "NAME";

            this.cbo계정구분.SelectedValue = "009";

            this.dtp조회기간.StartDateToString = Global.MainFrame.GetDateTimeToday().ToString("yyyy0101");
            this.dtp조회기간.EndDateToString = Global.MainFrame.GetStringToday;

            this.chk프로젝트제외.Checked = !(Config.MA_ENV.PJT형여부 == "Y") && Settings1.Default.프로젝트제외;
            this.chk프로젝트제외.Visible = !(Config.MA_ENV.PJT형여부 == "Y");

            if (this.is재고자산수불부)
                this.chk현재고0제외.Visible = false;
            else
            {
                this.chk창고제외.Visible = false;
                this.chk프로젝트제외.Visible = false;
                this.chk품목사용체크된것만.Visible = false;
                this.chk창고이동제외.Visible = false;
            }
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this.ctx재고코드From.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx재고코드From.CodeChanged += new EventHandler(this.ctx재고코드From_CodeChanged);
            this.ctx재고코드To.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.cbo창고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
        }

        #region ♣ InitGrid : 그리드 초기화

        private void InitGridH()
        {
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("CD_ITEM", "품목코드", 100);
            this._flexH.SetCol("NM_ITEM", "품목명", 160);
            this._flexH.SetCol("STND_ITEM", "규격", 140);
            this._flexH.SetCol("UNIT_IM", "단위", 40);

            if (this.is재고자산수불부)
            {
                this._flexH.SetCol("STND_DETAIL_ITEM", "세부규격", false);
                this._flexH.SetCol("MAT_ITEM", "재질", false);
            }

            this._flexH.SetCol("NM_ITEMGRP", "품목군", 140);
            this._flexH.SetCol("STAND_PRC", "표준원가", 70, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            if (this.is재고자산수불부)
            {
                this._flexH.SetCol("FG_SERNO", "S/N,LOT관리", false);
                this._flexH.SetCol("NM_MANAGER1", "관리자1", false);
                this._flexH.SetCol("NM_MANAGER2", "관리자2", false);    
            }
            else
            {
                this._flexH.SetCol("QT_INV", "현재고", 70, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexH.SetCol("DT_IO", "최종입고일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            }
            
            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        private void InitGridL()
        {
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("DT_IO", "거래일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NM_FGTRANS", "거래구분", 70);
            this._flexL.SetCol("NM_QTIOTP", "수불형태", 70);
            this._flexL.SetCol("YN_AM", "유상", 40, false, CheckTypeEnum.Y_N);
            this._flexL.SetCol("UM_STOCK", "표준단가", 70, false, typeof(decimal), FormatTpType.MONEY); //추가
            
            if (this.is재고자산수불부)
            {
                this._flexL.SetCol("QT_OPEN", "기초재고", 70, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("UM_OPEN", "기초재고단가", 90, false, typeof(decimal), FormatTpType.MONEY); //추가
            }
            
            this._flexL.SetCol("QT_IN", "입고", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UM_IN", "입고단가", 70, false, typeof(decimal), FormatTpType.MONEY); //추가

            if (this.is재고자산수불부)
            {
                this._flexL.SetCol("QT_OUT", "출고", 70, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("UM_OUT", "출고단가", 70, false, typeof(decimal), FormatTpType.MONEY); //추가
                this._flexL.SetCol("QT_GOOD_INV", "기말재고 양품재고", 140, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("QT_REJECT_INV", "기말재고 불량재고", false);
                this._flexL.SetCol("QT_INSP_INV", "기말재고 검사재고", false);
                this._flexL.SetCol("QT_TRANS_INV", "기말재고 이동중재고", false);
            }
            
            this._flexL.SetCol("NO_IO", "수불번호", 100);
            this._flexL.SetCol("CD_PJT", "프로젝트코드", 140);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", false);

            if (this.is재고자산수불부)
            {
                this._flexL.SetCol("CD_PARTNER", "매입/매출처코드", false);
                this._flexL.SetCol("LN_PARTNER", "매입/매출처명", 140);

                this._flexL.SetCol("CD_SL", "창고코드", false);
                this._flexL.SetCol("NM_SL", "창고명", false);
                this._flexL.SetCol("NO_BL", "BL번호", false);

                if (Config.MA_ENV.PJT형여부 == "Y")
                {
                    this._flexL.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", false);
                    this._flexL.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", false);
                    this._flexL.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", false);
                    this._flexL.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", false);
                }

                this._flexL.SetCol("DC_RMK_LINE", "비고", false);
            }
            else
            {
                this._flexL.SetCol("CD_PARTNER", "매입처코드", false);
                this._flexL.SetCol("LN_PARTNER", "매입처명", 140);
            }
            
            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            this._flexL.SetExceptSumCol("UM_STOCK", "UM_OPEN", "UM_IN", "UM_OUT", "QT_GOOD_INV", "QT_REJECT_INV", "QT_INSP_INV", "QT_TRANS_INV");
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 클릭

        #region ♣ 조회버튼 클릭

        protected override bool BeforeSearch()
        {
            return base.BeforeSearch() && this.Chk조회기간;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!base.BeforeSearch() || !this.Chk재고코드)
                    return;

                if (this.is재고자산수불부 == true)
                {
                    if (this.chk창고제외.Checked)
                    {
                        this._flexL.Cols["CD_SL"].Width = 0;
                        this._flexL.Cols["NM_SL"].Width = 0;
                    }
                    else
                    {
                        this._flexL.Cols["CD_SL"].Width = 100;
                        this._flexL.Cols["NM_SL"].Width = 140;
                    }

                    if (this.chk프로젝트제외.Checked)
                    {
                        this._flexL.Cols["NM_PROJECT"].Width = 0;

                        if (Config.MA_ENV.PJT형여부 == "Y")
                        {
                            this._flexL.Cols["SEQ_PROJECT"].Width = 0;
                            this._flexL.Cols["CD_PJT_ITEM"].Width = 0;
                            this._flexL.Cols["NM_PJT_ITEM"].Width = 0;
                            this._flexL.Cols["PJT_ITEM_STND"].Width = 0;
                        }
                    }
                    else
                    {
                        this._flexL.Cols["CD_PJT"].Width = 140;
                        this._flexL.Cols["NM_PROJECT"].Width = 140;

                        if (Config.MA_ENV.PJT형여부 == "Y")
                        {
                            this._flexL.Cols["SEQ_PROJECT"].Width = 120;
                            this._flexL.Cols["CD_PJT_ITEM"].Width = 140;
                            this._flexL.Cols["NM_PJT_ITEM"].Width = 140;
                            this._flexL.Cols["PJT_ITEM_STND"].Width = 140;
                        }
                    }
                }
                
                MsgControl.ShowMsg("조회중입니다. \r\n잠시만 기다려주세요!");

                this._flexH.Redraw = false;
                this._flexL.Redraw = false;

                this._flexH.Binding = this._biz.Search(new object[] { D.GetString(this.cbo공장.SelectedValue),
                                                                      Global.MainFrame.LoginInfo.CompanyCode,
                                                                      Global.MainFrame.LoginInfo.Language,
                                                                      this.ctx재고코드From.CodeValue,
                                                                      this.ctx재고코드To.CodeValue,
                                                                      this.cbo창고.QueryWhereIn_Pipe,
                                                                      this.dtp조회기간.StartDateToString,
                                                                      this.dtp조회기간.EndDateToString,
                                                                      D.GetString(this.cbo계정구분.SelectedValue),
                                                                      (this.chk품목사용체크된것만.Checked ? "Y" : "N"),
                                                                      (this.chk창고이동제외.Checked ? "Y" : "N"),
                                                                      (this.chk현재고0제외.Checked ? "Y" : "N"),
                                                                      this.ctx프로젝트.CodeValue,
                                                                      this.ctx품목군.CodeValue }, this.is재고자산수불부);

                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);

                    if (this.is재고자산수불부 == true)
                    {
                        this._flexL.Rows[1]["QT_GOOD_INV"] = string.Empty;
                        this._flexL.Rows[1]["QT_REJECT_INV"] = string.Empty;
                        this._flexL.Rows[1]["QT_INSP_INV"] = string.Empty;
                        this._flexL.Rows[1]["QT_TRANS_INV"] = string.Empty;
                    }
                }

                if (this.is재고자산수불부 == true)
                {
                    Settings1.Default.프로젝트제외 = this.chk프로젝트제외.Checked;
                    Settings1.Default.Save();
                }
            }
            catch (Exception ex)
            {
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;
                this.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;
            }
        }

        #endregion

        #region ♣ 닫기버튼클릭

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeExit())
                    return false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return base.OnToolBarExitButtonClicked(sender, e);
        }
        
        #endregion

        #endregion

        #region ♣ 화면내버튼 클릭
        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch(e.HelpID)
                {
                    case HelpID.P_MA_CODE_SUB1:
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000010";
                        break;
                    default:
                        if (string.IsNullOrEmpty(D.GetString(this.cbo공장.SelectedValue)))
                        {
                            this.ShowMessage(공통메세지._자료가선택되어있지않습니다, new string[] { this.lbl공장.Text });

                            this.cbo공장.Focus();
                            e.QueryCancel = true;
                        }
                        else
                        {
                            e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        }
                        break;
                }                
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx재고코드From_CodeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ctx재고코드To.SetCode(this.ctx재고코드From.CodeValue, this.ctx재고코드From.CodeName);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region ♣ 그리드 이벤트

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            FlexGrid flexGrid;
            DataTable dataTable;

            try
            {
                flexGrid = sender as FlexGrid;

                if (this._flexH["CD_ITEM"] == null) return;
                string filterExpression = "CD_ITEM = '" + this._flexH["CD_ITEM"].ToString() + "'";
                dataTable = null;
               
                if (this._flexH.DetailQueryNeed)
                {
                    dataTable = this._biz.SearchDetail(new object[] { D.GetString(this.cbo공장.SelectedValue),
                                                                      Global.MainFrame.LoginInfo.CompanyCode,
                                                                      Global.MainFrame.LoginInfo.Language,
                                                                      D.GetString(this._flexH["CD_ITEM"]),
                                                                      this.cbo창고.QueryWhereIn_Pipe,
                                                                      this.dtp조회기간.StartDateToString,
                                                                      this.dtp조회기간.EndDateToString,
                                                                      (this.chk창고이동제외.Checked ? "Y" : "N"),
                                                                      this.ctx프로젝트.CodeValue,
                                                                      (!this.chk창고제외.Checked ? "N" : "Y"),
                                                                      (!this.chk프로젝트제외.Checked ? "N" : "Y"),
                                                                      this.ctx품목군.CodeValue }, this.is재고자산수불부);

                    if (this.is재고자산수불부)
                    {
                        if (dataTable == null || dataTable.Rows.Count == 0)
                        {
                            this._flexL.RowFilter = filterExpression;
                            this._flexL.Rows[1]["QT_GOOD_INV"] = string.Empty;
                            this._flexL.Rows[1]["QT_REJECT_INV"] = string.Empty;
                            this._flexL.Rows[1]["QT_INSP_INV"] = string.Empty;
                            this._flexL.Rows[1]["QT_TRANS_INV"] = string.Empty;
                            return;
                        }

                        string str1 = "";

                        for (int index = 0; index < dataTable.Rows.Count; ++index)
                        {
                            string str2 = !(Config.MA_ENV.YN_UNIT == "Y") ? (!(Config.MA_ENV.PJT형여부 == "Y") ? D.GetString(dataTable.Rows[index]["CD_SL"]) : D.GetString(dataTable.Rows[index]["CD_SL"]) + D.GetString(dataTable.Rows[index]["CD_PJT"])) : D.GetString(dataTable.Rows[index]["CD_SL"]) + D.GetString(dataTable.Rows[index]["CD_PJT"]) + D.GetString(dataTable.Rows[index]["SEQ_PROJECT"]);

                            if (index != 0)
                                str1 = !(Config.MA_ENV.YN_UNIT == "Y") ? (!(Config.MA_ENV.PJT형여부 == "Y") ? D.GetString(dataTable.Rows[index - 1]["CD_SL"]) : D.GetString(dataTable.Rows[index - 1]["CD_SL"]) + D.GetString(dataTable.Rows[index - 1]["CD_PJT"])) : D.GetString(dataTable.Rows[index - 1]["CD_SL"]) + D.GetString(dataTable.Rows[index - 1]["CD_PJT"]) + D.GetString(dataTable.Rows[index - 1]["SEQ_PROJECT"]);

                            if (index != 0 && str2 == str1)
                            {
                                dataTable.Rows[index]["QT_GOOD_INV"] = D.GetDecimal(dataTable.Rows[index - 1]["QT_GOOD_INV"]) + D.GetDecimal(dataTable.Rows[index]["QT_IN"]) - D.GetDecimal(dataTable.Rows[index]["QT_OUT"]);
                                dataTable.Rows[index]["QT_REJECT_INV"] = D.GetDecimal(dataTable.Rows[index - 1]["QT_REJECT_INV"]) + D.GetDecimal(dataTable.Rows[index]["QT_REJECT_INV"]);
                                dataTable.Rows[index]["QT_INSP_INV"] = D.GetDecimal(dataTable.Rows[index - 1]["QT_INSP_INV"]) + D.GetDecimal(dataTable.Rows[index]["QT_INSP_INV"]);
                                dataTable.Rows[index]["QT_TRANS_INV"] = D.GetDecimal(dataTable.Rows[index - 1]["QT_TRANS_INV"]) + D.GetDecimal(dataTable.Rows[index]["QT_TRANS_INV"]);
                            }
                            else
                                dataTable.Rows[index]["QT_GOOD_INV"] = D.GetDecimal(dataTable.Rows[index]["QT_OPEN"]) + D.GetDecimal(dataTable.Rows[index]["QT_IN"]) - D.GetDecimal(dataTable.Rows[index]["QT_OUT"]);
                        }

                        dataTable.AcceptChanges();
                    }
                }

                this._flexL.BindingAdd(dataTable, filterExpression);

                if (this.is재고자산수불부 && flexGrid.DetailGrids[0].Rows.Count > 0)
                {
                    string formatDescription = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.QUANTITY, FormatFgType.INSERT);

                    this._flexL.Rows[1]["QT_GOOD_INV"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(QT_OPEN) + SUM(QT_IN) - SUM(QT_OUT)", filterExpression)).ToString(formatDescription);
                    this._flexL.Rows[1]["QT_REJECT_INV"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(QT_REJECT_INV)", filterExpression)).ToString(formatDescription);
                    this._flexL.Rows[1]["QT_INSP_INV"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(QT_INSP_INV)", filterExpression)).ToString(formatDescription);
                    this._flexL.Rows[1]["QT_TRANS_INV"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(QT_TRANS_INV)", filterExpression)).ToString(formatDescription);
                }
            }
            catch (Exception ex)
            {
                this._flexL.Redraw = true;
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 기타

        #endregion

        #region ♣ 기타 Property
        private bool Chk조회기간
        {
            get
            {
                return Checker.IsValid(this.dtp조회기간, true, this.DD("조회기간"));
            }
        }

        private bool Chk재고코드
        {
            get
            {
                return Checker.IsValid(this.ctx재고코드From, this.ctx재고코드To, false, this.DD("재고코드From"), this.DD("재고코드To"));
            }
        }
        #endregion
    }
}

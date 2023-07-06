using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace cz
{
    public partial class P_CZ_PU_PO_SUB2 : Duzon.Common.Forms.CommonDialog
    {
        private P_CZ_PU_PO_SUB2_BIZ _biz = new P_CZ_PU_PO_SUB2_BIZ();
        private string _m_btnType = string.Empty;
        private string _m_NO_PO = string.Empty;

        public string m_NO_PO
        {
            get
            {
                return this._m_NO_PO;
            }
        }

        public string m_btnType
        {
            get
            {
                return this._m_btnType;
            }
        }

        public P_CZ_PU_PO_SUB2()
        {
            this.InitializeComponent();
        }

        public P_CZ_PU_PO_SUB2(string 프로젝트)
        {
            this.InitializeComponent();

            this.txt프로젝트.Text = 프로젝트;
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
            this._flexH.DetailGrids = new FlexGrid[] { this._flexD };
            
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NO_PO", "발주번호", 100);
            this._flexH.SetCol("LN_PARTNER", "거래처", 140);
            this._flexH.SetCol("DT_PO", "발주일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_PURGRP", "구매그룹", 120);
            this._flexH.SetCol("NM_TPPO", "발주형태", 120);
            this._flexH.SetCol("DC50_PO", "비고", 200);
            this._flexH.SetCol("NM_KOR", "담당자", 100);

            this._flexH.WhenRowChangeThenGetDetail = true;

            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Detail
            this._flexD.BeginSetting(1, 1, false);

            this._flexD.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexD.SetCol("NM_ITEM", "품목명", 140);
            this._flexD.SetCol("STND_ITEM", "규격", 120);
            this._flexD.SetCol("CD_UNIT_MM", "발주단위", 60);
            this._flexD.SetCol("QT_PO_MM", "발주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("UM_EX_PO", "발주단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("AM_EX", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("AM", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexD.SetCol("CD_PJT", "프로젝트", 100);
            this._flexD.SetCol("DC1", "비고1", 100);
            this._flexD.SetCol("DC2", "비고2", 100);
            this._flexD.SetCol("DC3", "비고3", 100);
            this._flexD.SetCol("NM_CLS_ITEM", "계정", 120);
            this._flexD.SetCol("STND_DETAIL_ITEM", "세부규격", 120);
            this._flexD.SetCol("DT_LIMIT", "납기일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flexD.EnabledHeaderCheck = false;

            this._flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = Global.MainFrame.GetComboData("SC;MA_PLANT", 
                                                              "N;MA_B000004");

            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;

            this._flexD.SetDataMap("CD_UNIT_MM", comboData.Tables[1].Copy(), "CODE", "NAME");

            this.dtp지급일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-3);
            this.dtp지급일자.EndDate = Global.MainFrame.GetDateTimeToday();
            
            if (!string.IsNullOrEmpty(this.txt프로젝트.Text))
            {
                this.TitleText = Global.MainFrame.DD("이중지급확인");

                this.dtp지급일자.StartDateToString = "20160101";

                this.btn조회.Visible = false;
                this.btn복사.Visible = false;
                this.btn적용.Text = Global.MainFrame.DD("재지급");
                this.btn취소.Text = Global.MainFrame.DD("지급취소");

                this.btn조회_Click(null, null);

                if (!this._flexH.HasNormalRow)
                    this.DialogResult = DialogResult.OK;
                else
                    Global.MainFrame.ShowMessage("해당 프로젝트로 등록 된 지급 건이 있습니다.\n지급 내역 확인 하시고, [재지급] 또는 [지급취소] 버튼을 눌러주세요.");
            }
        }

        private void InitEvent()
        {
            this._flexH.HelpClick += new EventHandler(this._flexM_HelpClick);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);

            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn복사.Click += new EventHandler(this.btn복사_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.ctx발주유형.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.FieldCheck()) return;

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.dtp지급일자.StartDateToString, 
                                                                      this.dtp지급일자.EndDateToString,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      this.ctx매입처.CodeValue,
                                                                      this.txt프로젝트.Text,
                                                                      this.ctx구매그룹.QueryWhereIn_Pipe,
                                                                      this.ctx발주유형.QueryWhereIn_Pipe,
                                                                      this.ctx담당자.CodeValue,
                                                                      this.txt지급번호.Text });
                
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
                if (!this._flexH.IsBindingEnd)
                    return;
                
                if (this._flexH.DataTable.Rows.Count > 0 && this._flexH.Row > 0)
                {
                    this._m_NO_PO = this._flexH[this._flexH.Row, "NO_PO"].ToString();
                    this._m_btnType = "OK";
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.IsBindingEnd)
                    return;
                
                if (this._flexD.DataIndex(this._flexD.Row) >= 0)
                {
                    this._m_NO_PO = this._flexD.DataView[this._flexD.Row - 1]["NO_PO"].ToString();
                    this._m_btnType = "COPY";
                    this.DialogResult = DialogResult.OK;
                }
                else
                    this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flexH.IsBindingEnd || !this._flexH.HasNormalRow)
                    return;
                
                DataTable dt = null;
                string filter = "NO_PO= '" + D.GetString(this._flexH["NO_PO"]) + "'";
                
                if (this._flexH.DetailQueryNeed)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               this._flexH["NO_PO"].ToString() });
                }
                    
                this._flexD.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexM_HelpClick(object sender, EventArgs e)
        {
            try
            {
                this.btn적용_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.ControlName.ToString() == this.ctx발주유형.Name)
                    e.HelpParam.P61_CODE1 = "N";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool FieldCheck()
        {
            if (!(this.cbo공장.SelectedItem.ToString() == string.Empty))
                return Checker.IsValid(this.dtp지급일자, true, "일자");

            Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장.Text });

            return false;
        }
    }
}
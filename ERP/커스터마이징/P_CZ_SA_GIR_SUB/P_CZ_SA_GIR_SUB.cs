using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Dintec;
using System.Drawing;
using Duzon.ERPU.Grant;
using Duzon.Common.Forms.Help;
using Duzon.Common.BpControls;

namespace cz
{
    /// <summary>
    /// 
    /// </summary>
    public partial class P_CZ_SA_GIR_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region -> 멤버필드
        private P_CZ_SA_GIR_SUB_BIZ _biz = new P_CZ_SA_GIR_SUB_BIZ();
        private DataTable _dt = new DataTable();
        private bool _포장여부;
        private DataTable 선택아이템;

        public DataRow[] 수주데이터
        {
            get { return this._flexH.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows); }
        }

        public DataTable 수주아이템데이터
        {
            get { return new DataView(this._flexL.DataTable, "S = 'Y'", string.Empty, DataViewRowState.CurrentRows).ToTable(); }
        }

        private string 입고상태
        {
            get
            {
                if (this.rdo입고.Checked == true) return "001";
                else if (this.rdo미입고.Checked == true) return "002";
                else return "003";
            }
        }

        private string 출고상태
        {
            get
            {
                if (this.rdo출고.Checked == true) return "001";
                else if (this.rdo미출고.Checked == true) return "002";
                else return "003";
            }
        }

        private string 의뢰수량
        {
            get
            {
                if (this.rdo전체수량.Checked == true) return "001";
                else return "002";
            }
        }
        #endregion

        #region -> 초기화
        public P_CZ_SA_GIR_SUB(string 매출처코드, string 매출처명, string IMO번호, string 호선번호, string 호선명, bool 포장여부, DataTable 선택아이템)
        {
            try
            {
                InitializeComponent();
                this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

                this.ctx매출처.CodeValue = 매출처코드;
                this.ctx매출처.CodeName = 매출처명;

                this.ctx호선번호.CodeValue = IMO번호;
                this.ctx호선번호.CodeName = 호선번호;

                this.txt호선명.Text = 호선명;

                this._포장여부 = 포장여부;

                this.선택아이템 = 선택아이템;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();

                this.InitGrid();
                this.InitEvent();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        
        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = Global.MainFrame.GetComboData("N;MA_PLANT", "S;PU_C000016");

            this.cbo날짜유형.DataSource = MA.GetCodeUser(new string[3] { "SO", "IN", "DU" }, new string[3] { Global.MainFrame.DD("수주일자"), Global.MainFrame.DD("입고일자"), Global.MainFrame.DD("납기일자") });
            this.cbo날짜유형.DisplayMember = "NAME";
            this.cbo날짜유형.ValueMember = "CODE";

            this.dtp검색일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            this.dtp검색일자.StartDate = Global.MainFrame.GetDateTimeFirstMonth.AddMonths(-12);
            this.dtp검색일자.EndDate = Global.MainFrame.GetDateTimeToday();

            this.cbo공장.DataSource = comboData.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";

            this.cbo거래구분.DataSource = comboData.Tables[1];
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";

            if (this._포장여부 == true)
                this.bpPanelControl6.Visible = true;
        }

        private void InitGrid()
        {
            #region Header Grid
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_SO", "수주번호", 100);
            this._flexH.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
            this._flexH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_VESSEL", "호선명", 120); //호선명 추가 !!
            this._flexH.SetCol("CD_PARTNER", "매출처코드", false);
            this._flexH.SetCol("LN_PARTNER", "매출처명", 100);
            this._flexH.SetCol("NM_EXCH", "통화명", 60);
            this._flexH.SetCol("NM_KOR", "담당자", 80);  
            this._flexH.SetCol("NM_SO", "수주형태", 100);
            this._flexH.SetCol("NM_SALEGRP", "영업그룹", false);                     
            this._flexH.SetCol("NM_PROJECT", "프로젝트명", false);
            this._flexH.SetCol("DC_RMK", "비고", false);
            this._flexH.SetCol("DC_RMK1", "비고1", false);
            this._flexH.SetCol("SN_PARTNER", "매출처약칭", false);

            this._flexH.SetDummyColumn("S");

            this._flexH.ExtendLastCol = true;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.SettingVersion = "1.0.0.0";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexH.LoadUserCache("P_CZ_SA_GIR_SUB_flexH");
            #endregion

            #region Detail Grid
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("NO_SO", "수주번호", false);
            this._flexL.SetCol("SEQ_SO", "수주항번", 40);
            this._flexL.SetCol("NO_DSP", "순번", 40);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100); //ITEM CODE 필드 추가 !!
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 120); //DESCRIPTION 필드 추가 !!
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 120);
            this._flexL.SetCol("STND_ITEM", "규격", false);
            this._flexL.SetCol("UNIT_SO", "단위", false);
            this._flexL.SetCol("QT_SO", "수주수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_IN", "입고수량", 60, false, typeof(decimal), FormatTpType.QUANTITY); //입고수량 필드 추가!!
            this._flexL.SetCol("QT_GIR", "의뢰수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_INV", "현재고", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_STOCK_REMAIN", "재고출고대기수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_TAX", "납부수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_REQ", "요청수량", 60, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("YN_PACK", "포장여부", 60, false, CheckTypeEnum.Y_N);
            this._flexL.SetCol("QT_PACK_REMAIN", "포장진행수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_GIR_REMAIN", "출고진행수량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_REMAIN", "수주잔량", 60, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("DT_DUEDATE", "납기요구일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY); //입고일자 필드 추가!!
            this._flexL.SetCol("DT_REQGI", "출고예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            this._flexL.SetCol("NM_EXCH", "통화명", 60);
            this._flexL.SetCol("UM", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_GIR", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            if (this._포장여부 == false)
                this._flexL.SetCol("NM_SL", "출고창고", 80, true);
            this._flexL.SetCol("NO_LOCATION", "로케이션", 80);
            this._flexL.SetCol("UNIT", "관리단위", false);
            this._flexL.SetCol("QT_REQ_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NM_SUPPLIER", "매입처", 120);
            this._flexL.SetCol("GI_PARTNER", "납품처", false);
            this._flexL.SetCol("NM_GI_PARTNER", "납품처명", 120, false);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", false);
            this._flexL.SetCol("NO_PO_PARTNER", "매출처발주번호", 80);
            this._flexL.SetCol("NO_POLINE_PARTNER", "매출처발주항번", false);
            this._flexL.SetCol("DC_RMK3", "수주라인비고1", false);
            this._flexL.SetCol("DC_RMK4", "수주라인비고2", false);
            this._flexL.SetCol("NO_PROJECT", "프로젝트코드", false);
            this._flexL.SetCol("YN_PARTIAL_DELV", "부분송품여부", 80, false, CheckTypeEnum.Y_N);
            this._flexL.SetCol("YN_CONFIRM", "청구호선여부", 80, false, CheckTypeEnum.Y_N);

            this._flexL.SetDummyColumn("S");

            this._flexL.SetCodeHelpCol("NM_SL", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CODE", "NAME" });

            this._flexL.SettingVersion = "1.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexL.SetExceptSumCol("UM");

            this._flexL.Styles.Add("미납부").ForeColor = Color.Black;
            this._flexL.Styles.Add("미납부").BackColor = Color.White;
            this._flexL.Styles.Add("납부").ForeColor = Color.Red;
            this._flexL.Styles.Add("납부").BackColor = Color.Yellow;

            this._flexL.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);

            this._flexL.LoadUserCache("P_CZ_SA_GIR_SUB_flexL");
            #endregion
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);

            this.ctx출고창고.QueryBefore += new BpQueryHandler(this.ctx출고창고_QueryBefore);
            this.btn출고창고.Click += new EventHandler(this.btn출고창고_Click);

            this.txt수주번호.KeyDown += new KeyEventHandler(this.txt수주번호_KeyDown);

            this._flexH.StartEdit += new RowColEventHandler(this._flexH_StartEdit);
            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);

            this._flexL.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            this._flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexL_BeforeCodeHelp);
        }
        #endregion

        #region -> 화면내버튼 클릭
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //사용자그리드셋팅 기능 : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.SaveUserCache("P_CZ_SA_GIR_SUB_flexH");
            _flexL.SaveUserCache("P_CZ_SA_GIR_SUB_flexL");
        }

        

        private void btn조회_Click(object sender, System.EventArgs e)
        {
            DataTable dt;

            try
            {
                dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                     this.dtp검색일자.StartDateToString,
                                                     this.dtp검색일자.EndDateToString,
                                                     this.cbo공장.SelectedValue,
                                                     this.cbo거래구분.SelectedValue,
                                                     this.ctx매출처.CodeValue,
                                                     this.ctx영업담당자.CodeValue, 
                                                     this.cbo날짜유형.SelectedValue,
                                                     this.txt수주번호.Text,
                                                     this.ctx호선번호.CodeValue,
                                                     this.ctx매입처.CodeValue,
                                                     this.입고상태,
                                                     this.출고상태,
                                                     this._포장여부 == true ? "Y" : "N" });

                this._flexH.Binding = dt;

                if (!this._flexH.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn적용_Click(object sender, System.EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flexL.HasNormalRow) return;

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (this._포장여부 == false && this._flexL.DataTable.Select("S = 'Y' AND ISNULL(CD_SL, '') = ''").Length > 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, Global.MainFrame.DD("출고창고"));
                        return;
                    }

                    if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(DT_DUEDATE, '') = ''").Length > 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, Global.MainFrame.DD("납기요구일"));
                        return;
                    }

                    if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(DT_REQGI, '') = ''").Length > 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, Global.MainFrame.DD("출고예정일"));
                        return;
                    }

                    if (this._포장여부 == false &&
                        this.ctx매출처.CodeValue != "10776" && // OCYAN OIL SERVICES LTD. (레디인포 메일 발송시 DN 번호 명기된 커머셜 인보이스 보내야 하는 업체)
                        this.ctx매출처.CodeValue != "12938" && // AROONA DRILLING LTD
                        this.ctx매출처.CodeValue != "07168" && // GOE Petrol Sanayi ve Ticaret Ltd.Şti.
                        this.ctx매출처.CodeValue != "11710" && // SAIPEM SA 
                        this.ctx매출처.CodeValue != "10508" && // TECHNIP
                        this._flexL.DataTable.Select("S = 'Y' AND ISNULL(QT_PACK_REMAIN, 0) > 0").Length > 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, Global.MainFrame.DD("포장진행수량"), "0");
                        return;
                    }

                    if (this._포장여부 == false && this._flexL.DataTable.Select("S = 'Y' AND ISNULL(YN_PARTIAL_DELV, 'N') = 'N' AND ISNULL(QT_GIR_REMAIN, 0) > 0").Length > 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, Global.MainFrame.DD("출고진행수량"), "0");
                        return;
                    }

                    if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(QT_REQ, 0) <= 0").Length > 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, Global.MainFrame.DD("요청수량"), "0");
                        return;
                    }

                    if (this._포장여부 == false && this._flexL.DataTable.Select("S = 'Y' AND ISNULL(DT_IN, '') = 'STOCK' AND ISNULL(QT_REQ, 0) > (ISNULL(QT_INV, 0) + ISNULL(QT_STOCK_REMAIN, 0))").Length > 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, Global.MainFrame.DD("요청수량"), Global.MainFrame.DD("현재고"));
                        return;
                    }

                    if (!중복체크(dataRowArray, new string[] { "CD_PARTNER" }, "매출처")) return;
                    if (!중복체크(dataRowArray, new string[] { "TP_BUSI" }, "거래구분")) return;
                    if (!중복체크(dataRowArray, new string[] { "TP_GI" }, "출고형태")) return;
                    if (!중복체크(dataRowArray, new string[] { "CD_EXCH" }, "통화명")) return;
                    if (!중복체크(dataRowArray, new string[] { "TP_VAT" }, "과세구분")) return;

                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn취소_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void txt수주번호_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txt수주번호.Text))
                        this._flexH.RowFilter = string.Empty;
                    else
                        this._flexH.RowFilter = "NO_SO LIKE'%" + txt수주번호.Text + "%'";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx출고창고_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID == HelpID.P_MA_TABLE_SUB)
                {
                    e.HelpParam.P00_CHILD_MODE = "출고창고";

                    e.HelpParam.P61_CODE1 = @"MS.CD_SL AS CODE,
	                                              MS.NM_SL AS NAME";
                    e.HelpParam.P62_CODE2 = @"MA_SL MS WITH(NOLOCK)";
                    e.HelpParam.P63_CODE3 = "WHERE MS.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                            "AND MS.CD_SL IN ('SL01', 'VL01', 'VL02')";
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn출고창고_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            
            try
            {
                if (!this._flexL.HasNormalRow) return;

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        if (dr["DT_IN"].ToString() == "STOCK" || dr["DT_IN"].ToString() == "CHARGE") 
                            continue;

                        dr["CD_SL"] = this.ctx출고창고.CodeValue;
                        dr["NM_SL"] = this.ctx출고창고.CodeName;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> 그리드 이벤
        private void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                DataRow[] dr = _flexL.DataTable.Select("NO_SO = '" + _flexH[e.Row, "NO_SO"].ToString() + "'", "", DataViewRowState.CurrentRows);

                if (_flexH[e.Row, "S"].ToString() == "N") //클릭하는 순간은 N이므로
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                        _flexL.SetCellCheck(i, _flexL.Cols["S"].Index, CheckEnum.Checked);
                }
                else
                {
                    for (int i = _flexL.Rows.Fixed; i <= dr.Length + 1; i++)
                    {
                        if (_flexL.RowState(i) == DataRowState.Deleted) continue;

                        _flexL.SetCellCheck(i, _flexL.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;

                string Key = _flexH["NO_SO"].ToString();
                string Filter = "NO_SO = '" + Key + "'";

                if (this._flexH.DetailQueryNeed)
                {
                    dt = this._biz.SearchDetail(new object[]{ Global.MainFrame.LoginInfo.CompanyCode,
                                                              Key,
                                                              this.dtp검색일자.StartDateToString,
                                                              this.dtp검색일자.EndDateToString,
                                                              this.cbo공장.SelectedValue,
                                                              this.cbo거래구분.SelectedValue,
                                                              this.cbo날짜유형.SelectedValue,
                                                              this.ctx매입처.CodeValue,
                                                              this.입고상태,
                                                              this.출고상태,
                                                              (this._포장여부 == true ? "Y" : "N"),
                                                              this.의뢰수량 });
                    this.선택아이템제거(dt);
                }

                this._flexL.BindingAdd(dt, Filter);
                this._flexH.DetailQueryNeed = false;
                this.셀병합();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid grid = sender as FlexGrid;
                if (grid == null) return;

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                switch (grid.Cols[e.Col].Name)
                {
                    case "S":
                        grid["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                        if (grid.Name == this._flexL.Name)
                        {
                            DataRow[] drArr = grid.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                            if (drArr.Length != 0)
                                _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                            else
                                _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                        break;
                    case "QT_REQ":
                        if (this._포장여부 == true)
                        {
                            if (D.GetDecimal(grid["QT_REQ"]) > D.GetDecimal(grid["QT_SO"]))
                            {
                                grid["QT_REQ"] = D.GetString(grid.GetData(e.Row, e.Col));
                                Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, Global.MainFrame.DD("요청수량"), Global.MainFrame.DD("수주수량"));
                                break;
                            }
                        }
                        else
                        {
                            if (D.GetDecimal(grid["QT_REQ"]) > D.GetDecimal(grid["QT_REMAIN"]))
                            {
                                grid["QT_REQ"] = D.GetString(grid.GetData(e.Row, e.Col));
                                Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, Global.MainFrame.DD("요청수량"), Global.MainFrame.DD("수주잔량"));
                                break;
                            }
                        }

                        if (D.GetString(grid["DT_IN"]) == "STOCK")
                        {
                            if (D.GetDecimal(grid["QT_REQ"]) > D.GetDecimal(grid["QT_IN"]))
                            {
                                grid["QT_REQ"] = D.GetString(grid.GetData(e.Row, e.Col));
                                Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, Global.MainFrame.DD("요청수량"), Global.MainFrame.DD("재고수량"));
                                break;
                            }

                            if (this._포장여부 == false && D.GetDecimal(grid["QT_REQ"]) > (D.GetDecimal(grid["QT_INV"]) + D.GetDecimal(grid["QT_STOCK_REMAIN"])))
                            {
                                grid["QT_REQ"] = D.GetString(grid.GetData(e.Row, e.Col));
                                Global.MainFrame.ShowMessage(공통메세지._은_보다작거나같아야합니다, Global.MainFrame.DD("요청수량"), Global.MainFrame.DD("현재고"));
                                break;
                            }
                        }

                        grid["UNIT_SO_FACT"] = (D.GetDecimal(grid["UNIT_SO_FACT"]) == 0 ? 1 : D.GetDecimal(grid["UNIT_SO_FACT"]));
                        grid["AM_GIR"] = this.외화계산(D.GetDecimal(grid["QT_REQ"]) * D.GetDecimal(grid["UM"]));
                        grid["AM_GIRAMT"] = this.원화계산(D.GetDecimal(grid["AM_GIR"]) * D.GetDecimal(grid["RT_EXCH"]));
                        grid["AM_VAT"] =  this.원화계산(D.GetDecimal(grid["AM_GIRAMT"]) * (D.GetDecimal(grid["RT_VAT"]) / 100));
                        grid["AMT"] = this.원화계산(D.GetDecimal(grid["AM_GIRAMT"]) + D.GetDecimal(grid["AM_VAT"]));
                        grid["QT_REQ_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(grid["QT_REQ"]) * D.GetDecimal(grid["UNIT_SO_FACT"]));
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                switch (flex.Name)
                {
                    case "_flexH":  //상단 그리드 Header Click 이벤트

                        //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                        _flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                        if (!_flexH.HasNormalRow || !_flexL.HasNormalRow) return;

                        for (int h = 0; h < _flexH.Rows.Count - 1; h++)
                        {
                            _flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                            for (int i = _flexL.Rows.Fixed; i < _flexL.Rows.Count; i++)
                            {
                                if (_flexL.RowState(i) == DataRowState.Deleted) continue;

                                _flexL[i, "S"] = _flexH["S"].ToString();
                            }
                        }
                        break;

                    case "_flexL":  //하단 그리드 Header Click 이벤트
                        if (!_flexL.HasNormalRow) return;

                        _flexH["S"] = _flexL["S"].ToString();

                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            FlexGrid flex;

            try
            {
                flex = ((FlexGrid)sender);

                if (e.Row < flex.Rows.Fixed || e.Col < flex.Cols.Fixed) return;
                if (flex.Cols[e.Col].Name != "QT_TAX") return;

                CellStyle style = flex.GetCellStyle(e.Row, e.Col);

                if (style == null)
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_TAX"]) > 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["납부"]);
                    else
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["미납부"]);
                }
                else if (style.Name == "미납부")
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_TAX"]) > 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["납부"]);
                }
                else if (style.Name == "납부")
                {
                    if (D.GetDecimal(flex.Rows[e.Row]["QT_TAX"]) == 0)
                        flex.SetCellStyle(e.Row, e.Col, flex.Styles["미납부"]);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);

                if (grid["DT_IN"].ToString() == "STOCK" || grid["DT_IN"].ToString() == "CHARGE")
                {
                    e.Cancel = true;
                    return;
                }
                
                switch (grid.Cols[e.Col].Name)
                {
                    case "NM_SL":
                        e.Parameter.P00_CHILD_MODE = "출고창고";

                        e.Parameter.P61_CODE1 = @"MS.CD_SL AS CODE,
	                                              MS.NM_SL AS NAME";
                        e.Parameter.P62_CODE2 = @"MA_SL MS WITH(NOLOCK)";
                        e.Parameter.P63_CODE3 = "WHERE MS.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                "AND MS.CD_SL IN ('SL01', 'VL01', 'VL02')";
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> 기타 메소드
        private void 선택아이템제거(DataTable dt)
        {
            string filterExpression;
            DataRow[] dataRowArray;

            try
            {
                if (dt == null || dt.Rows.Count <= 0 || (this.선택아이템 == null || this.선택아이템.Rows.Count <= 0))
                    return;

                foreach (DataRow dr in this.선택아이템.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted) continue;

                    filterExpression = "NO_SO = '" + D.GetString(dr["NO_SO"]) + "' AND SEQ_SO = '" + D.GetString(dr["SEQ_SO"]) + "'";
                    dataRowArray = dt.Select(filterExpression);

                    if (dataRowArray != null)
                    {
                        for (int i = 0; i < dataRowArray.Length; ++i)
                        {
                            dataRowArray[i].Delete();
                        }       
                    }
                }

                dt.AcceptChanges();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool 중복체크(DataRow[] dr, string[] str_Filter, string ColName)
        {
            DataTable dt = ComFunc.getGridGroupBy(dr, str_Filter, true);

            if (dt.Rows.Count != 1)
            {
                Global.MainFrame.ShowMessage(공통메세지._의값이중복되었습니다, Global.MainFrame.DD(ColName));
                return false;
            }

            return true;
        }

        private void 셀병합()
        {
            CellRange cellRange;
            string key;

            try
            {
                if (this._flexL.HasNormalRow == false) return;

                this._flexL.Redraw = false;

                for (int row = this._flexL.Rows.Fixed; row < this._flexL.Rows.Count; row++)
                {
                    key = D.GetString(this._flexL.GetData(row, "SEQ_SO"));

                    foreach(Column column in this._flexL.Cols)
                    {
                        switch (column.Name)
                        {
                            case "S":
                                continue;
                            case "QT_SO":
                                continue;
                            case "QT_IN":
                                continue;
                            case "QT_REQ":
                                continue;
                            case "YN_PACK":
                                continue;
                            case "QT_REMAIN":
                                continue;
                            case "DT_IN":
                                continue;
                            case "AM_GIR":
                                continue;
                            case "AM_GIRAMT":
                                continue;
                            case "QT_REQ_IM":
                                continue;
                            case "NO_LOCATION":
                                continue;
                        }

                        cellRange = this._flexL.GetCellRange(row, column.Name, row, column.Name);
                        cellRange.UserData = key + "_" + column.Name;
                    }
                }

                this._flexL.DoMerge();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexL.Redraw = true;
            }
        }

        private decimal 원화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
                else
                    result = Decimal.Round(value, 0, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        private decimal 외화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }
        #endregion
    }
}

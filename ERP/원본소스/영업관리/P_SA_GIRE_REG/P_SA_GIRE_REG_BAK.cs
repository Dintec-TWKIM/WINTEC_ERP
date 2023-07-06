using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Diagnostics;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Common.Controls;
using System.Text;

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using DzHelpFormLib;
using Duzon.Common.Forms.Help;

namespace sale
{
    // **************************************
    // 작   성   자 : 허성철
    // 재 작  성 일 : 2007-03-11
    // 모   듈   명 : 영업
    // 시 스  템 명 : 영업관리
    // 서브시스템명 : 출하관리
    // 페 이 지  명 : 출하반품등록
    // 프로젝트  명 : P_SA_GIRE_REG
    // **************************************
    public partial class P_SA_GIRE_REG : Duzon.Common.Forms.PageBase
    {
        #region ★ 멤버필드

        private P_SA_GIRE_REG_BIZ _biz = new P_SA_GIRE_REG_BIZ();
        private FreeBinding _header = new FreeBinding();
        string _부서 = Global.MainFrame.LoginInfo.DeptCode;

        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_SA_GIRE_REG()
        {
            try
            {
                InitializeComponent();

                this.MainGrids = new FlexGrid[] { _flexH, _flexL };
                this.DataChanged += new EventHandler(Page_DataChanged);
                _header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            InitGridH();
            InitGridL();
            _flexH.DetailGrids = new FlexGrid[] { _flexL };
        }

        #endregion

        #region -> InitGridH

        void InitGridH()
        {
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("NO_GIR", "의뢰번호", 100);
            _flexH.SetCol("CD_PLANT", "공장", 80);
            _flexH.SetCol("NM_PLANT", "공장명", 120);
            _flexH.SetCol("CD_PARTNER", "거래처", 80);
            _flexH.SetCol("LN_PARTNER", "거래처명", 120);
            _flexH.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("DC_RMK", "비고", 200);
            _flexH.ExtendLastCol = true;
            _flexH.EnabledHeaderCheck = false;
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flexH.AfterEdit += new RowColEventHandler(_flexH_AfterEdit);
            _flexH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitGridL

        private void InitGridL()
        {
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexL.SetCol("CD_ITEM", "품목코드", 80);
            _flexL.SetCol("NM_ITEM", "품목명", 120);
            _flexL.SetCol("STND_ITEM", "규격", 80);
            _flexL.SetCol("FG_TRANS", "거래구분", 80);
            _flexL.SetCol("CD_SL", "창고코드", 80, true);
            _flexL.SetCol("NM_SL", "창고명", 120);
            _flexL.SetCol("NM_QTIOTP", "출하형태", 100);
            _flexL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_GIR_IM", "관리수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_UNIT_MM", "출하수량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_IO", "출하관리수량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UNIT", "단위", 80);
            //_flexL.SetCol("UM_EX", "단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            //_flexL.SetCol("AM_EX", "금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            //_flexL.SetCol("UM", "원화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            //_flexL.SetCol("AM", "원화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            //_flexL.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("CD_UNIT_MM", "관리단위", 80);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 80);
            _flexL.EnabledHeaderCheck = false;
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexL.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" });
            _flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
            _flexL.StartEdit += new RowColEventHandler(_flex_StartEdit);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
            _flexL.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            this.maskedEditBox1.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            this.maskedEditBox1.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
            this.maskedEditBox1.Text = this.MainFrameInterface.GetStringFirstDayInMonth;

            this.maskedEditBox2.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            this.maskedEditBox2.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
            this.maskedEditBox2.Text = this.MainFrameInterface.GetStringToday;

            this.maskedEditBox3.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
            this.maskedEditBox3.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

            rb_not_pc.Checked = true;
            btn_apply_good.Enabled = false;

            DataSet g_dsCombo = this.GetComboData("N;MA_PLANT", "S;PU_C000016");

            // 공장 콤보
            cb_cd_plant.DataSource = g_dsCombo.Tables[0];
            cb_cd_plant.DisplayMember = "NAME";
            cb_cd_plant.ValueMember = "CODE";

            _flexL.SetDataMap("FG_TRANS", g_dsCombo.Tables[1], "CODE", "NAME");  //거래구분

            // 프리폼 초기화
            object[] obj = new object[10];

            obj[0] = string.Empty;  //회사코드
            obj[1] = string.Empty;  //의뢰번호
            obj[2] = string.Empty;  //의뢰기간FROM
            obj[3] = string.Empty;  //의뢰기간TO
            obj[4] = string.Empty;  //공장
            obj[5] = string.Empty;  //거래처
            obj[6] = string.Empty;  //납품처
            obj[7] = string.Empty;  //출하형태
            obj[8] = string.Empty;  //처리상태가 미처리 상태 즉, 반품여부가 "N" 인것
            obj[9] = string.Empty;  //운송방법

            DataSet ds = _biz.Search(obj);

            _header.SetBinding(ds.Tables[0], panel5);
            _header.ClearAndNewRow();
            _flexH.Binding = ds.Tables[1];
            _flexL.Binding = ds.Tables[2];
        }

        #endregion

        #endregion

        #region ★ 메인버튼 이벤트

        #region -> 조회조건체크

        private bool Field_Check()
        {
            //의뢰일자 시작일
            if (maskedEditBox1.Text == "" || maskedEditBox1.Text == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_dt_req.Text);
                maskedEditBox1.Focus();
                return false;
            }
            //의뢰일자 종료일
            if (maskedEditBox2.Text == "" || maskedEditBox2.Text == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_dt_req.Text);
                maskedEditBox2.Focus();
                return false;
            }
            //공장
            if (cb_cd_plant.SelectedValue == null || cb_cd_plant.SelectedValue.ToString() == "" || cb_cd_plant.SelectedValue.ToString() == string.Empty)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, lb_gl_plant2.Text);
                cb_cd_plant.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region -> 출하등록체크

        private bool FieldCheckgrreg()
        {
            //출고일자 
            if (maskedEditBox3.Text == "" || maskedEditBox3.Text == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lb_dt_rcv.Text);
                this.maskedEditBox3.Focus();
                return false;
            }
            //담당자
            if (bpNo_Emp.CodeValue == null || bpNo_Emp.CodeValue.ToString() == "" || bpNo_Emp.CodeValue.ToString() == string.Empty)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, lb_no_emp2.Text);
                bpNo_Emp.Focus();
                return false;
            }
            
            return true;
        }

        #endregion

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!base.SaveData()) return false;

            if (!FieldCheckgrreg()) return false;

            if (!Verify_Grid(_flexL)) return false;

            DataRow[] ldrchk = _flexL.DataTable.Select("S = 'Y' AND NO_ISURCV = '" + _flexH[_flexH.Row, "NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);

            if (ldrchk == null || ldrchk.Length == 0)
            {
                ShowMessage(공통메세지.선택된자료가없습니다);
                return false;
            }
            string NO_IO = string.Empty;
            if (tb_no_gr.Text == string.Empty || tb_no_gr.Text == "")
            {
                NO_IO = (string)GetSeq(LoginInfo.CompanyCode, "SA", "09", maskedEditBox3.Text.Substring(0, 6));
                tb_no_gr.Text = NO_IO;
            }
            else
                NO_IO = tb_no_gr.Text;
            _header.CurrentRow["NO_IO"] = NO_IO;

            DataTable dtL = _flexL.DataTable.Clone();
            decimal i = 1;
            foreach (DataRow row in ldrchk)
            {
                if (row["CD_SL"] == null || row["CD_SL"].ToString() == "" || row["CD_SL"].ToString() == string.Empty)
                {
                    ShowMessage(공통메세지._은는필수입력항목입니다, DD("창고"));
                    _flexL.Select(_flexL.Rows.Fixed, "CD_SL");
                    _flexL.Focus();
                    return false;
                }

                if (_flexH.CDecimal(row["QT_UNIT_MM"]) <= 0)
                {
                    ShowMessage(공통메세지._은_보다커야합니다, DD("출하수량"), "0");
                    _flexL.Select(_flexL.Rows.Fixed, "QT_UNIT_MM");
                    _flexL.Focus();
                    return false;
                }

                row["NO_IOLINE"] = i++; 
                row["NO_IO"] = NO_IO;
                row["DT_IO"] = maskedEditBox3.Text;
                row["FG_IO"] = "041";

                dtL.ImportRow(row);
            }

            DataTable dtH = _header.GetChanges();

            if (dtH == null && dtL == null)
                return true;

            
            //LOT 추가효~ 2008.09.04
            DataTable dt_Qtio = null; /*MM_QTIO에 들어갈 데이터는 라인수에 상관없이 1개이기 때문에 1줄만 IMPORT해준다*/
            DataTable dtLOT = null;
            dt_Qtio = dtL.Clone();
            dt_Qtio.ImportRow(dtL.Rows[0]);

            if (String.Compare(Global.MainFrame.LoginInfo.MngLot, "Y") == 0 && dtL != null)
            {
                DataRow[] DR = dtL.Select("NO_LOT = 'YES'");

                if (DR.Length > 0)
                {
                    DataTable _dtLOT = dtL.Copy();

                    if (_dtLOT.Rows.Count > 0)
                    {
                        //foreach (DataRow drLOT in DR)
                        //{
                        //    _dtLOT.ImportRow(drLOT);
                        //}

                        pur.P_PU_LOT_SUB_R m_dlg = new pur.P_PU_LOT_SUB_R(_dtLOT);

                        if (m_dlg.ShowDialog(this) == DialogResult.OK)
                            dtLOT = m_dlg.dtL;
                        else
                            return false;
                    }
                }
            }

            bool bSuccess = _biz.Save(dt_Qtio, dtLOT, dtH, dtL, NO_IO, cb_cd_plant.SelectedValue.ToString(), _부서);
            if (!bSuccess) return false;

            _flexH.AcceptChanges();
            _flexL.AcceptChanges();

            return true;
        }

        #endregion

        #region -> 조회버튼 클릭

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                if (!Field_Check()) return;

                //DataSet g_dsData = _biz.Search(maskedEditBox1.Text.ToString(), 
                //                               maskedEditBox2.Text.ToString(), 
                //                               cb_cd_plant.SelectedValue == null ? string.Empty : cb_cd_plant.SelectedValue.ToString(), 
                //                               bpNm_Partner.CodeValue, 
                //                               bpNm_Partner2.CodeValue, 
                //                               bpTpGi.CodeValue);
                //_flexH.Binding = g_dsData.Tables[1];

                object[] obj = new object[10];

                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;                                                    //회사코드
                obj[1] = string.Empty;                                                                              //의뢰번호
                obj[2] = maskedEditBox1.Text;                                                                       //의뢰기간FROM
                obj[3] = maskedEditBox2.Text;                                                                       //의뢰기간TO
                obj[4] = cb_cd_plant.SelectedValue == null ? string.Empty : cb_cd_plant.SelectedValue.ToString();   //공장
                obj[5] = bpNm_Partner.CodeValue;                                                                    //거래처
                obj[6] = bpNm_Partner2.CodeValue;                                                                   //납품처
                obj[7] = bpTpGi.CodeValue;                                                                          //출하형태
                obj[8] = "Y";                                                                                       //처리상태가 처리 상태 즉, 반품여부가 "Y" 인것
                obj[9] = string.Empty;                                                                              //운송방법

                DataSet ds = _biz.Search(obj);
                _flexH.Binding = ds.Tables[1];

                if (!_flexH.HasNormalRow)
                    ShowMessage(PageResultMode.SearchNoData);
                else
                {
                    _flexL.IsDataChanged = true;
                    Page_DataChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 추가버튼 클릭

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeAdd()) return;

                _flexH.DataTable.Rows.Clear();
                _flexL.DataTable.Rows.Clear();
                _flexH.AcceptChanges();
                _flexL.AcceptChanges();
                _header.ClearAndNewRow();
                Page_DataChanged(null, null);
                _header_ControlValueChanged(bpNm_Sl2, null);
                maskedEditBox1.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장버튼 클릭

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    ShowMessage(공통메세지.자료가정상적으로저장되었습니다);

                    //DataSet g_dsData = _biz.Search(maskedEditBox1.Text.ToString(), 
                    //                               maskedEditBox2.Text.ToString(), 
                    //                               cb_cd_plant.SelectedValue == null ? string.Empty : cb_cd_plant.SelectedValue.ToString(), 
                    //                               bpNm_Partner.CodeValue, 
                    //                               bpNm_Partner2.CodeValue, 
                    //                               bpTpGi.CodeValue);
                    //_flexH.Binding = g_dsData.Tables[1];

                    object[] obj = new object[10];

                    obj[0] = Global.MainFrame.LoginInfo.CompanyCode;                                                    //회사코드
                    obj[1] = string.Empty;                                                                              //의뢰번호
                    obj[2] = maskedEditBox1.Text;                                                                       //의뢰기간FROM
                    obj[3] = maskedEditBox2.Text;                                                                       //의뢰기간TO
                    obj[4] = cb_cd_plant.SelectedValue == null ? string.Empty : cb_cd_plant.SelectedValue.ToString();   //공장
                    obj[5] = bpNm_Partner.CodeValue;                                                                    //거래처
                    obj[6] = bpNm_Partner2.CodeValue;                                                                   //납품처
                    obj[7] = bpTpGi.CodeValue;                                                                          //출하형태
                    obj[8] = "Y";                                                                                       //처리상태가 처리 상태 즉, 반품여부가 "Y" 인것
                    obj[9] = string.Empty;                                                                              //운송방법

                    DataSet ds = _biz.Search(obj);
                    _flexH.Binding = ds.Tables[1];

                    if (_flexH.HasNormalRow)
                    {
                        _flexL.IsDataChanged = true;
                        Page_DataChanged(null, null);
                    }
                    tb_no_gr.Text = string.Empty;
                    _header.CurrentRow["NO_IO"] = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ★ 화면내버튼 클릭

        #region -> 적용버튼 클릭

        private void btn_apply_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] ldrchk = _flexL.DataTable.Select("S = 'Y' AND NO_ISURCV = '" + _flexH[_flexH.Row, "NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);

                if (ldrchk == null || ldrchk.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flexL.Redraw = false;
                foreach (DataRow row in ldrchk)
                {
                    row["CD_SL"] = bpNm_Sl2.CodeValue;
                    row["NM_SL"] = bpNm_Sl2.CodeName;
                }
                _flexL.Redraw = true;

                ShowMessage(공통메세지._작업을완료하였습니다, btn_apply.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 양품적용버튼 클릭

        private void btn_apply_good_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] ldrchk = _flexL.DataTable.Select("S = 'Y' AND NO_ISURCV = '" + _flexH[_flexH.Row, "NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);

                if (ldrchk == null || ldrchk.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _flexL.Redraw = false;
                foreach (DataRow row in ldrchk)
                {
                    row["QT_UNIT_MM"] = row["QT_GIR"];

                    if (_flexH.CDecimal(row["UNIT_SO_FACT"]) == 0)
                        row["QT_IO"] = _flexH.CDecimal(row["QT_UNIT_MM"]);             //출하수량
                    else
                        row["QT_IO"] = _flexH.CDecimal(row["QT_UNIT_MM"]) * _flexH.CDecimal(row["UNIT_SO_FACT"]);             //출하수량

                    row["QT_GOOD_INV"] = row["QT_IO"];                                  //양품재고

                    row["VAT"] = Decimal.Truncate(_flexH.CDecimal(row["AM"]) * (_flexH.CDecimal(row["RT_VAT"]) / 100));   //부가세

                    if (_flexH.CDecimal(row["QT_IO"]) == 0)
                    {
                        row["UM_EX"] = 0;
                        row["UM"] = 0;
                    }
                    else
                    {
                        row["UM_EX"] = _flexH.CDecimal(row["AM_EX"]) / _flexH.CDecimal(row["QT_IO"]);
                        row["UM"] = _flexH.CDecimal(row["AM"]) / _flexH.CDecimal(row["QT_IO"]);
                    }
                }
                _flexL.Redraw = true;

                ShowMessage(공통메세지._작업을완료하였습니다, btn_apply_good.Text);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }


        #endregion

        #endregion

        #region ★ 그리드 이벤트

        #region -> _flex_StartEdit

        void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                switch (flex.Name)
                {
                    case "_flexH":
                        if (_flexH.Cols[e.Col].Name == "S")
                        {
                            for (int i = _flexH.Rows.Fixed; i < _flexH.Rows.Count; i++)
                            {
                                if (_flexH.GetCellCheck(i, _flexH.Cols["S"].Index) == CheckEnum.Checked)
                                    _flexH.SetCellCheck(i, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                            }
                        }
                        break;
                    case "_flexL":
                        if (_flexL.Cols[e.Col].Name == "S")
                        {
                            if (_flexH[_flexH.Row, "S"].ToString() == "N")
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        break;
                }

                _flexL.IsDataChanged = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexH_AfterEdit

        void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                DataRow[] dr = _flexL.DataTable.Select("NO_ISURCV = '" + _flexH[e.Row, "NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);

                if (_flexH[e.Row, "S"].ToString() == "Y") //클릭하는 순간은 N이므로
                {
                    for (int q = _flexL.Rows.Fixed; q <= dr.Length; q++)
                        _flexL.SetCellCheck(q, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int q = _flexL.Rows.Fixed; q <= dr.Length; q++)
                        _flexL.SetCellCheck(q, 1, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

        }

        #endregion

        #region -> _flex_AfterRowChange

        void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;
                switch (flex.Name)
                {
                    case "_flexH":
                        DataSet ds = null;
                        DataTable dt = null;
                        string Key = _flexH[e.NewRange.r1, "NO_GIR"].ToString();
                        string Filter = "NO_ISURCV = '" + Key + "'";
                        if (_flexH.DetailQueryNeed)
                        {
                            // 프리폼 초기화
                            object[] obj = new object[10];

                            obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사코드
                            obj[1] = Key;                                       //의뢰번호
                            obj[2] = string.Empty;                              //의뢰기간FROM
                            obj[3] = string.Empty;                              //의뢰기간TO
                            obj[4] = string.Empty;                              //공장
                            obj[5] = string.Empty;                              //거래처
                            obj[6] = bpNm_Partner2.CodeValue;                   //납품처
                            obj[7] = bpTpGi.CodeValue;                          //출하형태
                            obj[8] = "Y";                                       //처리상태가 처리 상태 즉, 반품여부가 "Y" 인것
                            obj[9] = string.Empty;                              //운송방법

                            ds = _biz.Search(obj);
                            dt = ds.Tables[2];

                            //dt = _biz.SearchDetail(Key, bpNm_Partner2.CodeValue, bpTpGi.CodeValue);
                        }
                        _flexL.BindingAdd(dt, Filter);
                        _flexL.SetDummyColumn("S");
                        _flexL.SetDummyColumn("CD_SL");
                        _flexL.SetDummyColumn("NM_SL");
                        _flexL.SetDummyColumn("QT_UNIT_MM");
                        _flexL.SetDummyColumn("QT_IO");
                        _flexH.DetailQueryNeed = false;

                        DataRow[] dr = _flexL.DataTable.Select("NO_ISURCV = '" + _flexH[_flexH.Row, "NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);

                        if (_flexH[_flexH.Row, "S"].ToString() == "N")
                        {
                            for (int i = _flexL.Rows.Fixed; i <= dr.Length; i++)
                                _flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                        }
                        break;
                    case "_flexL":
                        m_tbCd_Plant.Text = _flexL[_flexL.Row, "LN_PARTNER"].ToString();		// 납품처
                        m_tbNo_io.Text = _flexL[_flexL.Row, "NO_ISURCV"].ToString();				// 수주번호
                        m_tbNo_Lc.Text = _flexL[_flexL.Row, "NO_LC"].ToString();				// LC번호
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_ValidateEdit

        void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                string newValue = ((FlexGrid)sender).EditData;

                if (oldValue.ToUpper() == newValue.ToUpper()) return;

                switch (_flexL.Cols[e.Col].Name)
                {
                    case "QT_UNIT_MM":
                        if (_flexH.CDecimal(newValue) > _flexH.CDecimal(_flexL["QT_GIR"]))
                            _flexL["QT_UNIT_MM"] = _flexL["QT_GIR"];

                        if (_flexH.CDecimal(_flexL["UNIT_SO_FACT"]) == 0)
                            _flexL["QT_IO"] = _flexH.CDecimal(_flexL["QT_UNIT_MM"]);
                        else
                            _flexL["QT_IO"] = _flexH.CDecimal(_flexL["QT_UNIT_MM"]) * _flexH.CDecimal(_flexL["UNIT_SO_FACT"]);

                        _flexL["QT_GOOD_INV"] = _flexH.CDecimal(_flexL["QT_IO"]);

                        if (_flexH.CDecimal(newValue) != _flexH.CDecimal(_flexL["QT_GIR"]))
                        {
                            _flexL["AM_EX"] = _flexH.CDecimal(_flexL["UM_EX_PSO"]) * _flexH.CDecimal(newValue);
                            _flexL["AM"] = Decimal.Truncate(_flexH.CDecimal(_flexL["AM_EX"]) * _flexH.CDecimal(_flexL["RT_EXCH"]));
                        }

                        _flexL["VAT"] = Decimal.Truncate(_flexH.CDecimal(_flexL["AM"]) * (_flexH.CDecimal(_flexL["RT_VAT"]) / 100));

                        if (_flexH.CDecimal(_flexL["QT_IO"]) == 0)
                        {
                            _flexL["UM_EX"] = 0;
                            _flexL["UM"] = 0;
                        }
                        else
                        {
                            _flexL["UM_EX"] = _flexH.CDecimal(_flexL["AM_EX"]) / _flexH.CDecimal(_flexL["QT_IO"]);
                            _flexL["UM"] = _flexH.CDecimal(_flexL["AM"]) / _flexH.CDecimal(_flexL["QT_IO"]);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_BeforeCodeHelp

        void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P09_CD_PLANT = _flexL[_flexL.Row, "CD_PLANT"].ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        #endregion

        #endregion

        #region ★ 기타 이벤트

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            if (_flexL.HasNormalRow)
            {
                ToolBarSaveButtonEnabled = true;
                btn_apply_good.Enabled = true;
                cb_cd_plant.Enabled = false;
            }
            else
            {
                ToolBarSaveButtonEnabled = false;
                btn_apply_good.Enabled = false;
                cb_cd_plant.Enabled = true;
                m_tbCd_Plant.Text = string.Empty;
                m_tbNo_io.Text = string.Empty;
                m_tbNo_Lc.Text = string.Empty;
                _flexL.RowFilter = string.Empty;
            }
        }

        #endregion

        #region -> _header_ControlValueChanged

        void _header_ControlValueChanged(object sender, FreeBindingArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "bpNm_Sl2":
                        if (bpNm_Sl2.CodeValue != string.Empty && bpNm_Sl2.CodeValue != null)
                            btn_apply.Enabled = true;
                        else
                            btn_apply.Enabled = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        #endregion

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {

            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                    if (!_flexL.HasNormalRow)
                    {
                        e.QueryCancel = true;
                        return;
                    }
                    e.HelpParam.P09_CD_PLANT = _flexH[_flexH.Row, "CD_PLANT"].ToString();
                    break;
                case Duzon.Common.Forms.Help.HelpID.P_PU_EJTP_SUB:
                    e.HelpParam.P61_CODE1 = "010|041|042|";
                    break;
            }
        }

        #endregion

        #region -> Control_QueryAfter

        private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "bpNo_Emp":
                        _부서 = e.HelpReturn.Rows[0]["CD_DEPT"].ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> rb_do_pc_CheckedChanged

        private void rb_do_pc_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (rb_do_pc.Checked == true)
                {
                    object[] args = new Object[6];

                    args[0] = maskedEditBox3.Text;                  //출하일자
                    args[1] = cb_cd_plant.SelectedValue.ToString(); //공장
                    args[2] = bpNm_Partner.CodeValue;               //거래처코드
                    args[3] = bpNm_Partner.CodeName;                //거래처명
                    args[4] = bpNo_Emp.CodeValue;                   //사번
                    args[5] = bpNo_Emp.CodeName;                    //이름

                    // Main 이 살아 있는지 확인한후 살아 있으면 저장을 실행하고 죽어 있으면 그냥 리턴시켜버린다.
                    if (this.MainFrameInterface.IsExistPage("P_SA_GIM_REG", false))
                        this.UnLoadPage("P_SA_GIM_REG", false); //- 특정 페이지 닫기

                    string ls_LinePageName = DD("출하관리");
                    bool isComplete = this.LoadPageFrom("P_SA_GIM_REG", ls_LinePageName, this.Grant, args);
                    if (!isComplete) ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);

                    rb_do_pc.Checked = false;
                    rb_not_pc.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }


        #endregion

        #region -> 콤보박스 키이벤트

        private void CommonComboBox_KeyEvent(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                System.Windows.Forms.SendKeys.SendWait("{TAB}");
        }

        #endregion

        #endregion

        #region ★ 기타 메소드

        #region -> IsChanged

        protected override bool IsChanged()
        {
            if (_flexL.IsDataChanged == true)
                return true;

            return false;
        }

        #endregion

        #endregion
    }
}

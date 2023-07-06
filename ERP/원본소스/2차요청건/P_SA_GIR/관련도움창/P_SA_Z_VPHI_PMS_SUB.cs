using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;

namespace sale
{
    /// <summary>
    /// 작   성   자 : 심재희
    /// 작   성   일 : 
    /// 
    /// 모   듈   명 : 영업
    /// 시 스 템  명 : 영업관리
    /// 서브시스템명 :
    /// 페 이 지  명 : PMS 도움창
    /// 프로젝트  명 : P_SA_Z_VPHI_PMS_SUB
    /// 수   정   일 : 
    /// 2013.02.13 : M20130213066 : 행 선택시 인덱스 에러 발생
    /// 2013.02.19 : D20130212172 : INVOICE번호 조회조건 추가
    /// 2013.02.21 : D20130206022 : INVOICE번호 컬럼추가
    /// </summary>
    public partial class P_SA_Z_VPHI_PMS_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region -> 멤버필드

        bool is정상수주 = true;

        private P_SA_Z_VPHI_PMS_SUB_BIZ _biz = new P_SA_Z_VPHI_PMS_SUB_BIZ();
        DataTable _dt = new DataTable();

        #endregion

        #region -> 초기화

        #region -> 생성자
        public P_SA_Z_VPHI_PMS_SUB(string 거래처코드, string 거래처명, string 출하공장코드, string 출하공장명, string 거래구분코드, string 거래구분명)
		{
            try
            {
                InitializeComponent();

                ctx공장.CodeValue = 출하공장코드;
                ctx공장.CodeName = 출하공장명;
                ctx거래구분.CodeValue = 거래구분코드;
                ctx거래구분.CodeName = 거래구분명;
                bp거래처.CodeValue = 거래처코드;
                bp거래처.CodeName = 거래처명;

                if (bp거래처.CodeValue != string.Empty)
                {
                    bp거래처.Enabled = false;
                    bp거래처.ReadOnly = Duzon.Common.Forms.Help.ReadOnly.TotalReadOnly;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> InitLoad
        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();
                InitGridH();
                InitGridL();
                _flexH.DetailGrids = new FlexGrid[] { _flexL };
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> InitGridH
        private void InitGridH()
        {
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexH.SetCol("NO_PMS", "PACKING번호", 140);
            _flexH.SetCol("DT_PMS", "PACKING일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("NO_SO", "수주번호", 140);
            _flexH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("NM_SO", "수주형태", 100);
            _flexH.SetCol("CD_EXCH", "환종", 80);
            _flexH.SetCol("NM_SALEGRP", "영업그룹", 80);
            _flexH.SetCol("NM_KOR", "담당자", 80);
            _flexH.SetCol("CD_PARTNER", "거래처코드", 100);
            _flexH.SetCol("LN_PARTNER", "거래처명", 100);
            //_flexH.SetCol("NM_PROJECT", "프로젝트명", 100);
            //_flexH.SetCol("DC_RMK", "비고", 200);
            //_flexH.SetCol("DC_RMK1", "비고1", 200);
            _flexH.ExtendLastCol = true;
            _flexH.EnabledHeaderCheck = true;
            _flexH.SettingVersion = "1.0.0.0";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.StartEdit += new RowColEventHandler(_flexH_StartEdit);
            _flexH.AfterRowChange += new RangeEventHandler(_flexH_AfterRowChange);
            _flexH.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexH.LoadUserCache("P_SA_Z_VPHI_PMS_SUB_flexH");
        }
        #endregion

        #region -> InitGridL
        private void InitGridL()
        {
            _flexL.BeginSetting(1, 1, false);
            _flexL.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            _flexL.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            _flexL.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);
            _flexL.SetCol("CD_ITEM", "품목코드", 100);
            _flexL.SetCol("NM_ITEM", "품목명", 120);
            _flexL.SetCol("STND_ITEM", "규격", 65);
            _flexL.SetCol("UNIT_SO", "단위", 65);
            //_flexL.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            //_flexL.SetCol("DT_REQGI", "출하예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            //_flexL.SetCol("QT_SO", "수주수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_GIR", "Packing 수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            //_flexL.SetCol("REQ_QT_GIR", "수주잔량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UM", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_GIR", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("AM_GIRAMT", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("NO_INVOCIE", "INVOCIE번호", 120, false);
            //_flexL.SetCol("NM_SL", "창고", 120, false);
            //_flexL.SetCol("UNIT", "관리단위", 65, false);
            //_flexL.SetCol("QT_GIR_IM", "관리수량", 65, false, typeof(decimal), FormatTpType.QUANTITY);
            //_flexL.SetCol("GI_PARTNER", "납품처", 120, false);
            //_flexL.SetCol("LN_PARTNER", "납품처명", 120, false);
            _flexL.SettingVersion = "1.0.0.0";
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexL.CheckHeaderClick += new EventHandler(_flex_CheckHeaderClick);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flexL_ValidateEdit);
            _flexL.SetExceptSumCol("UM");
            _flexL.LoadUserCache("P_SA_Z_VPHI_PMS_SUB_flexL");
        } 
        #endregion

        #region -> InitPaint
        protected override void InitPaint()
        {
            base.InitPaint();

            dtp일자from.Text = Global.MainFrame.GetStringFirstDayInMonth;
            dtp일자to.Text = Global.MainFrame.GetStringToday;

            SetControl str = new SetControl();
            str.SetCombobox(cboPACKING구분, MA.GetCodeUser(new string[] { "ERP", "MES", "PACK" }, new string[] { "ERP", "MES", "가패킹" },true));
            _flexH.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005"), "CODE", "NAME");
        }
        #endregion

        #endregion

        #region -> 화면내버튼 클릭

        #region -> 조회버튼클릭
        private void OnSearchButtonClicked(object sender, System.EventArgs e)
        {
            try
            {
                if (!Chk일자) return;

                List<object> list = new List<object>();
                list.Add(Global.MainFrame.LoginInfo.CompanyCode);
                list.Add(dtp일자from.Text);
                list.Add(dtp일자to.Text);
                list.Add(ctx공장.CodeValue);
                list.Add(ctx거래구분.CodeValue);
                list.Add(bp거래처.CodeValue);
                list.Add(txtPMS번호.Text);
                list.Add(D.GetString(cboPACKING구분.SelectedValue));
                list.Add(txt인보이스번호.Text);
                list.Add(MA.Login.사원번호);

                _flexH.Binding = _biz.Search(list.ToArray());

                if (!_flexH.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion
        
        #region -> 적용버튼클릭

        #region -> 적용

        private void OnApply(object sender, System.EventArgs e)
        {
            try
            {
                if (!_flexL.HasNormalRow) return;

                DataRow[] dr = _flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dr == null || dr.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                _dt = _flexL.DataTable.Clone();

                if (!중복체크(dr, new string[] { "CD_PARTNER" }, "거래처")) return;
                if (!중복체크(dr, new string[] { "CD_EXCH" }, "환종")) return;
                if (!중복체크(dr, new string[] { "TP_PRICE_IMSI" }, "단가유형")) return;

                if (is정상수주 == false && !중복체크(dr, new string[] { "CD_SALEGRP" }, "영업그룹")) return;

                foreach (DataRow row in dr)
                {
                    if (D.GetDecimal(row["CHK_QT_GIR"]) != 0)
                    {
                        row["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_GIRAMT"]) * (D.GetDecimal(row["RT_VAT"]) / 100)); //부가세
                        row["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(row["AM_GIRAMT"]) + D.GetDecimal(row["AM_VAT"])); //합계금액(원화금액 + 부가세) 지금 쿼리가 잘못되어 있음 적용시 재계산
                    }
                    _dt.ImportRow(row);
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 리턴데이터
        public DataTable 수주데이터
        {
            get { return _dt; }
        }
        #endregion

        #endregion

        #region -> OnClosed(화면이 닫힐때)
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
             
            //사용자그리드셋팅 기능 : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.SaveUserCache("P_SA_Z_VPHI_PMS_SUB_flexH");
            _flexL.SaveUserCache("P_SA_Z_VPHI_PMS_SUB_flexL");
        }
        #endregion 

        #endregion

        #region -> 그리드 이벤트

        #region -> _StartEdit

        #region -> _flexH_StartEdit

        private void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                DataRow[] dr = _flexL.DataTable.Select("NO_SO = '" + _flexH[e.Row, "NO_SO"].ToString() + "' AND NO_PMS = '" + _flexH[e.Row, "NO_PMS"].ToString() + "'", "", DataViewRowState.CurrentRows);

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

        #endregion

        #endregion

        #region -> _flexH_AfterRowChange

        private void _flexH_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            try
            {
                DataTable dt = null;

                string Key = _flexH[e.NewRange.r1, "NO_PMS"].ToString();
                string Filter = "NO_PMS = '" + Key + "'";

                if (_flexH.DetailQueryNeed)
                {
                    object[] obj_Detail = new object[] {
                        Global.MainFrame.LoginInfo.CompanyCode, 
                        Key, 
                        ctx공장.CodeValue, 
                        ctx거래구분.CodeValue, 
                        dtp일자from.Text, 
                        dtp일자to.Text
                    };

                    dt = _biz.SearchDetail(obj_Detail);
                }
                _flexL.BindingAdd(dt, Filter);
                _flexH.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flexL_ValidateEdit

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                switch (_flex.Cols[e.Col].Name)
                {
                    case "S":

                        _flex["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                        if (_flex.Name == "_flexL")
                        {
                            DataRow[] drArr = _flex.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                            if (drArr.Length != 0)
                                _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                            else
                                _flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> _flex_CheckHeaderClick

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

        #endregion

        #endregion

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

        public bool Set정상수주 { set { is정상수주 = value; } }
        bool Chk일자 { get { return Checker.IsValid(dtp일자from, dtp일자to, true, lbl일자.Text + "from", lbl일자.Text + "to"); } }
    }
}

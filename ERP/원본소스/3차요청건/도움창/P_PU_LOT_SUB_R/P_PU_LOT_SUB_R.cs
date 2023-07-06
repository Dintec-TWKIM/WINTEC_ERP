using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

namespace pur
{
    public partial class P_PU_LOT_SUB_R : Duzon.Common.Forms.CommonDialog
    {
        #region ★ 멤버필드

        private P_PU_LOT_SUB_R_BIZ _biz = new P_PU_LOT_SUB_R_BIZ();
        private OpenFileDialog m_FileDlg = new OpenFileDialog();
        private DataTable _dt_EXCEL = null;
        private bool ExcelChk = true;
        public DataTable _dt = null;
        public DataTable _dtL = null;
        string _m_lot_use = BASIC.GetMAEXC("LOT유효일자관리여부");
        string _m_lot_set = BASIC.GetMAEXC("LOT도움창설정");
        string _FG_PS = string.Empty;
        string _pageid = string.Empty;
        string[] _value = new string[]{}; //생성자값
        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_PU_LOT_SUB_R(DataTable dt)
        {
            InitializeComponent();

            _dt = dt;

            //영업에서 판매출하 반품일 경우에만 출고LOT 내역 도움창을 보여준다.
            if (_dt.Rows[0]["FG_IO"].ToString() != "041")
            {
                btn_출고LOT.Visible= false;
            }
        }

        public P_PU_LOT_SUB_R(DataTable dt, string[] value)
        {
            InitializeComponent();

            _dt = dt;

            //영업에서 판매출하 반품일 경우에만 출고LOT 내역 도움창을 보여준다.
            if (_dt.Rows[0]["FG_IO"].ToString() != "041")
            {
                btn_출고LOT.Visible = false;
            }

            _value = value; //생성자값 1:LOT디폴트번호
        }

        #endregion

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();
            InitGridM();
            InitGridD();

            _flexM.Cols["NO_IO_MGMT"].Visible = false;
            _flexM.Cols["NO_IOLINE_MGMT"].Visible = false;
            _flexM.Cols["FG_IO"].Visible = false;
            _flexM.Cols["CD_QTIOTP"].Visible = false;

            _flexM.DetailGrids = new FlexGrid[] { _flexD };

            //DataRow row = Duzon.ERPU.MF.Common.CodeSearch.GetCodeInfo(MasterSearch.MM_EJTP, new string[] { MA.Login.회사코드, D.GetString(_dt.Rows[0]["CD_QTIOTP"]) });
            //_FG_PS = D.GetString(row["TP_QTIO"]);

            DataTable dt_D = _biz.Search_Detail("");

            _flexD.Binding = dt_D;
            _flexM.Binding = _dt;
            _dtL = dt_D;

            Set_default_linedata(_dt);
        }

        private void Set_default_linedata(DataTable dt)
        {
            try
            {
                if (_flexD.DataTable == null) return;


                foreach (DataRow dr in dt.Rows)
                {
                    string Filter = "NO_IO = '" + D.GetString(dr["NO_IO"]) + "' AND NO_IOLINE = " + D.GetString(dr["NO_IOLINE"]) + "";
                    
                    if (_flexD.DataTable.Select(Filter).Length != 0) continue;
                
                    _flexD.Rows.Add();
                    _flexD.Row = _flexD.Rows.Count - 1;
                    _flexD["S"] = "N";
                    _flexD["CD_ITEM"] = dr["CD_ITEM"];
                    _flexD["NO_IO"] = dr["NO_IO"];
                    _flexD["NO_IOLINE"] = dr["NO_IOLINE"];
                    _flexD["DT_IO"] = dr["DT_IO"];
                    _flexD["FG_IO"] = dr["FG_IO"];
                    _flexD["CD_QTIOTP"] = dr["CD_QTIOTP"];
                    _flexD["CD_SL"] = dr["CD_SL"];
                    if (_flexD.Rows.Count == 2)
                        _flexD["QT_IO"] = dr["QT_GOOD_INV"];
                    else
                        _flexD["QT_IO"] = 0;

                    _flexD["FG_PS"] = "1";


                    if (_value.Length > 0 && _flexD.DataTable.Select(Filter).Length == 0)
                    {
                        _flexD["NO_LOT"] = D.GetString(_value[0]);
                        _flexD["QT_IO"] = dr["QT_GOOD_INV"];
                    }
                    else
                    {
                        _flexD["NO_LOT"] = "";
                        _flexD["QT_IO"] = dr["QT_GOOD_INV"];
                    }


                    _flexD.AddFinished();
                    _flexD.Col = _flexD.Cols.Fixed;
                    _flexD.Focus();
                }
            }
            catch(Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> InitGridM

        private void InitGridM()
        {
            _flexM.BeginSetting(1, 1, false);
            _flexM.SetCol("NO_IO_MGMT", "", 120);
            _flexM.SetCol("NO_IOLINE_MGMT", "", 120);
            _flexM.SetCol("FG_IO", "", 120);
            _flexM.SetCol("CD_QTIOTP", "", 120);
            _flexM.SetCol("DT_IO", "수불일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("NM_QTIOTP", "수불형태", 120);
            _flexM.SetCol("NO_IOLINE", "입고항번", 120); //추가 20081230 EXCEL적용시 동일한 품목을 별도 구분하기위함)
            _flexM.SetCol("CD_ITEM", "품목코드", 120);
            _flexM.SetCol("NM_ITEM", "품목명", 120);
            _flexM.SetCol("UNIT_IM", "단위", 80);
            _flexM.SetCol("STND_ITEM", "규격", 120);
            _flexM.SetCol("QT_GOOD_INV", "처리수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexM.SetCol("CD_SL", "창고코드", 120);
            _flexM.SetCol("NM_SL", "창고명", 120);
            _flexM.SetCol("CD_PJT", "프로젝트코드", 120);
            _flexM.SetCol("NM_PROJECT", "프로젝트명", 120);
            _flexM.SetCol("UM_EX", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexM.SetCol("AM_EX", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexM.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
          
            _flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexM.AfterRowChange += new RangeEventHandler(_flexM_AfterRowChange);
            _flexM.LoadUserCache("P_PU_LOT_SUB_flexM");
        }

        #endregion      

        #region -> InitGridD

        private void InitGridD()
        {
            _flexD.BeginSetting(1, 1, true);
            _flexD.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            _flexD.SetCol("NO_LOT", "LOT번호", 360, true);
            _flexD.SetCol("QT_IO", "처리수량", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            if (_m_lot_use == "Y") //시스템 유효일자 여부에 따라 컬럼 보여줌
            {
                _flexD.SetCol("DT_LIMIT", "유효일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            }
            _flexD.SetCol("DC_LOTRMK", "LOT비고", 100, true);
            _flexD.Cols["QT_IO"].Format = "#,###.##";

            DataTable LOT관리항목DT = MA.GetCode("PU_C000079");

            foreach (DataRow row in LOT관리항목DT.Rows)
            {
                int cnt = D.GetInt(row["CODE"]);
                string column = "CD_MNG" + cnt;
                _flexD.SetCol(column, D.GetString(row["NAME"]), 100);
            }

            _flexD.SetDummyColumn("S");
            _flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexD.ValidateEdit += new ValidateEditEventHandler(_flexD_ValidateEdit);
            if (_m_lot_set != "001")
                _flexD.VerifyAutoDelete = new string[] { "NO_LOT" };

            _flexD.LoadUserCache("P_PU_LOT_SUB_flexD");
        }

        #endregion
 
        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            if (_pageid == "P_SA_GI_SWITCH_YN_AM")
                Set_Line();

            if (Global.MainFrame.ServerKeyCommon == "MAKUS" /*|| Global.MainFrame.ServerKeyCommon == "DZSQL" || Global.MainFrame.ServerKeyCommon == "SQL_"*/)
                btn_Seq.Visible = true;

            txt_Serial.Focus();
        }

        #endregion

        #endregion

        #region ★ 버튼 이벤트

        #region -> 추가

        private void 추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (_flexD.DataTable == null) return;

                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - 1;
                _flexD["S"] = "N";
                _flexD["CD_ITEM"] = _flexM[_flexM.Row, "CD_ITEM"];
                _flexD["NO_IO"] = _flexM[_flexM.Row, "NO_IO"];
                _flexD["NO_IOLINE"] = _flexM[_flexM.Row, "NO_IOLINE"];
                _flexD["DT_IO"] = _flexM[_flexM.Row, "DT_IO"];
                _flexD["FG_IO"] = _flexM[_flexM.Row, "FG_IO"];
                _flexD["CD_QTIOTP"] = _flexM[_flexM.Row, "CD_QTIOTP"];
                _flexD["CD_SL"] = _flexM[_flexM.Row, "CD_SL"];
                if (_flexD.Rows.Count == 2)
                    _flexD["QT_IO"] = _flexM[_flexM.Row, "QT_GOOD_INV"];
                else
                _flexD["QT_IO"] = 0;
                
                _flexD["FG_PS"] = "1";

                if (_value.Length > 0 && _flexD.DataView.Count == 1)
                {
                    _flexD["NO_LOT"] = D.GetString(_value[0]);
                    _flexD["QT_IO"] = _flexM[_flexM.Row, "QT_GOOD_INV"];
                }
                else
                    _flexD["NO_LOT"] = "";

                _flexD.AddFinished();
                _flexD.Col = _flexD.Cols.Fixed;
                _flexD.Focus();

            }
            catch (Exception ex)
            {
                 Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 삭제

        private void 삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexD.HasNormalRow) return;

                try
                {
                    if (!_flexD.HasNormalRow) return;

                    DataRow[] rows = _flexD.DataTable.Select("S ='Y'");

                    if (rows.Length == 0)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                        return;
                    }
                    //if (rows != null & rows.Length > 0)
                    //{
                    //    for (int i = 0; i < rows.Length; i++)
                    //    {
                    //        rows[i].Delete();
                    //    }
                    //}
                    _flexD.Redraw = false;

                    for (int r = _flexD.Rows.Count - 1; r >= _flexD.Rows.Fixed; r--)
                    {
                        if (_flexD[r, "S"].ToString() == "Y")
                        {
                            _flexD.Rows.Remove(r);
                        }
                    }
                    _flexD.Redraw = true;
                }
                catch (Exception ex)
                {
                    Global.MainFrame.MsgEnd(ex);
                }

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 확인

        private void 확인_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (!_flexD.IsBindingEnd) return;


                if (!check_save()) return;

                string _NO_IO = "";
                string _NO_IOLINE = "";
                decimal _QT_GOOD = 0;
                for (int i = 1; i < _flexM.Rows.Count; i++)
                {
                    _NO_IO = _flexM[i, "NO_IO"].ToString();
                    _NO_IOLINE = _flexM[i, "NO_IOLINE"].ToString();

                    _QT_GOOD = 0;

                    DataRow[] DR = _flexD.DataTable.Select("NO_IO = '" + _NO_IO + "' AND NO_IOLINE = " + _NO_IOLINE + " ");
                    foreach (DataRow myDRV in DR)
                    {
                        //if (_flexM.CDecimal(myDRV["NO"].ToString()) == 0)
                        //{

                        //}
                        if ( _flexM.CDecimal(myDRV["QT_IO"].ToString()) == 0)
                        {
                            Global.MainFrame.ShowMessage("LOT수량은 0보다 커야합니다!");
                            return;
                        }
                        _QT_GOOD = _QT_GOOD + _flexM.CDecimal(myDRV["QT_IO"].ToString());
                    }

                    if (_flexM.CDecimal(_flexM[i, "QT_GOOD_INV"]) != _QT_GOOD)
                    {
                        Global.MainFrame.ShowMessage("품목코드 '" + _flexM[i, "CD_ITEM"].ToString() + "' 의 처리수량 = [" + _flexM.CDecimal(_flexM[i, "QT_GOOD_INV"])
                            + "] 과 LOT수량합 = [" + _QT_GOOD + "] 이 일치하지 않습니다!");

                        
                        return;
                    }
                }

                DataTable dtL = _flexD.GetChanges();

                if (dtL == null) return;

                _dtL = dtL;

                this.DialogResult = DialogResult.OK;
            }
            catch (coDbException ex)
            {
                Global.MainFrame.ShowErrorMessage(ex, "");
                return;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool check_save()
        {
            try
            {
                if (!_flexD.Verify())
                    return false;


                foreach (DataRow dr_D in _flexD.DataTable.Select())
                {
                    if (D.GetString(dr_D["NO_LOT"]) == "")
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "LOT NO");
                        return false;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return false;
            }

        }

        #endregion

        #region -> 종료

        private void 종료_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 적용

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_flexM.HasNormalRow)
                    return;

                DialogResult result;

                DataRow[] drs = _flexD.DataTable.Select("NO_LOT <> ''", "", DataViewRowState.CurrentRows);
                if (drs.Length > 0)
                {
                    result = MessageBox.Show("이미 LOT적용된 상태입니다.추가 내역은 삭제 후 다시 적용됩니다." + Environment.NewLine +
                        "적용하시겠습니까?", "일괄 LOT번호 적용", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    result = MessageBox.Show("각 항번별 입고수량과 입력하신 LOT번호 동일하게 적용됩니다." + Environment.NewLine +
                        "적용하시겠습니까?", "일괄 LOT번호 적용", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }

                if (result == DialogResult.OK)
                {
                    _flexD.Redraw = false;

                    _flexM.Row = 1; // 맨처음row에자동위치하도록해야틀어지지않는다.

                    for (int h = 0; h < _flexM.Rows.Count - 1; h++)
                    {
                        _flexM.Row = h + 1; //Row가순차적으로변경되면서RowChange() 이벤트를자동실행하게유도한다.

                        //for (int i = _flexD.Rows.Fixed; i < _flexD.Rows.Count; i++)
                        //{
                        //    if (_flexD.RowState(i) == DataRowState.Deleted) continue;

                        //    _flexD[i, "NO_LOT"] = txt_Serial.Text;
                        //}

                        for (int i = _flexD.Rows.Count - 1; i >= _flexD.Rows.Fixed; i--)
                        {
                            if (_flexD.RowState(i) == DataRowState.Deleted) continue;

                            if (i > 1)
                            {
                                _flexD.Rows.Remove(i);
                                continue;
                            }
                            _flexD[i, "NO_LOT"] = txt_Serial.Text;
                            _flexD[i, "QT_IO"] = D.GetDecimal(_flexM[h + 1, "QT_GOOD_INV"]);
                        }
                    }
                    _flexD.Redraw = true;
                    Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, "적용");
                    //btn_프로젝트적용.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 출고LOT 내역 도움창
        private void btn_출고LOT_Click(object sender, EventArgs e)
        {
            string[] str = new string[3];
            str[0] = _flexM["NO_IO_MGMT"].ToString();
            str[1] = _flexM["NO_IOLINE_MGMT"].ToString();
            str[2] = _flexM["CD_ITEM"].ToString();

            sale.P_SA_GI_LOT_SUB dlg = new sale.P_SA_GI_LOT_SUB(str);

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _flexD["NO_LOT"] = dlg.sReturn;
            }
        }
        #endregion

        #region -> 엑셀업로드

        private void _btn엑셀_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExcelChk == true)
                {
                    Duzon.Common.Util.Excel excel = null;

                    m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                    foreach (DataRow dr in _flexM.DataTable.Rows)
                    {
                        //DataRow[] rowCnt = _flexM.DataTable.Select("CD_ITEM = '" + dr["CD_ITEM"].ToString() + "' ", "", DataViewRowState.CurrentRows);
                        DataRow[] rowCnt = _flexM.DataTable.Select(" NO_IOLINE = " + dr["NO_IOLINE"].ToString() + " ", "", DataViewRowState.CurrentRows);

                        if (rowCnt.Length > 1)
                        {
                            //Global.MainFrame.ShowMessage("품목이 중복되어 엑셀 업로드가 불가합니다.");
                            Global.MainFrame.ShowMessage("출고항번이 중복되어 엑셀 업로드가 불가합니다.");
                            return;
                        }
                    }

                    if (m_FileDlg.ShowDialog() == DialogResult.OK)
                    {
                        Application.DoEvents();

                        string FileName = m_FileDlg.FileName;
                        string NO_ITEM = string.Empty; string MULTI_ITEM = string.Empty;
                        string NO_PARTNER = string.Empty; string MULTI_PTN = string.Empty;
                        bool 검증여부 = false; bool 수량적합 = false;
                        bool 품목적합 = false; string 적합거래처품목 = string.Empty;
                        string 거래처 = string.Empty; string 거래처명 = string.Empty;
                        bool UniqEnable = false;

                        excel = new Duzon.Common.Util.Excel();
                        _dt_EXCEL = excel.StartLoadExcel(FileName);
                        int j = _flexD.Rows.Count - _flexD.Rows.Fixed;

                        _flexD.Redraw = false;
                        _flexD.EmptyRowFilter();

                        DataTable _dt엑셀 = _biz.엑셀(_dt_EXCEL);
                        DataTable dt엑셀 = _dt엑셀.Clone();

                        dt엑셀.Columns["CD_ITEM"].DataType = typeof(string);
                        dt엑셀.Columns["NO_LOT"].DataType = typeof(string);
                        if (dt엑셀.Columns.Contains("QT_IO"))
                            dt엑셀.Columns["QT_IO"].DataType = typeof(decimal);
                        if (dt엑셀.Columns.Contains("NO_IOLINE"))
                            dt엑셀.Columns["NO_IOLINE"].DataType = typeof(decimal);

                        dt엑셀.Columns["CD_MNG1"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG2"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG3"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG4"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG5"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG6"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG7"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG8"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG9"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG10"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG11"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG12"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG13"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG14"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG15"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG16"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG17"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG18"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG19"].DataType = typeof(string);
                        dt엑셀.Columns["CD_MNG20"].DataType = typeof(string);

                        foreach (DataRow dr in _dt엑셀.Rows)
                        {
                            dt엑셀.Rows.Add(dr.ItemArray);
                        }

                        DataTable dt엑셀품목마스터검증 = dt엑셀.Clone();
                        DataTable dt엑셀품목중복체크 = dt엑셀.Clone();

                        DataRow NewRowItem;
                        StringBuilder 검증리스트_품목 = new StringBuilder();

                        string msg = "입고항번     품목코드     LOT번호   수 량"; //수정 출고항번추가 
                        검증리스트_품목.AppendLine(msg);

                        msg = "-".PadRight(60, '-');
                        검증리스트_품목.AppendLine(msg);

                        //검증로직 추가  20081230 
                        #region -> 엑셀 Data 검증 ( 품목 / 항번 존재여부 체크 )

                        foreach (DataRow row in dt엑셀.Rows)
                        {
                            if (row["CD_ITEM"].ToString().Trim() == null || row["CD_ITEM"].ToString().Trim() == string.Empty || row["CD_ITEM"].ToString().Trim() == "") { continue; }
                            if (row["NO_IOLINE"].ToString().Trim() == null || row["NO_IOLINE"].ToString().Trim() == string.Empty || row["NO_IOLINE"].ToString().Trim() == "") { continue; }

                            string strFilter = " NO_IOLINE = " + row["NO_IOLINE"].ToString().Trim() + " AND CD_ITEM = '" + row["CD_ITEM"].ToString().Trim() + "' ";

                            DataRow[] drChk = _dt.Select(strFilter, "", DataViewRowState.CurrentRows);

                            if (drChk.Length > 0)
                                UniqEnable = true;
                            else UniqEnable = false;

                            if (UniqEnable == false)
                            {
                                string NO_IOLINE = row["NO_IOLINE"].ToString().PadRight(10, ' '); //추가 20081230
                                string CD_ITEM = row["CD_ITEM"].ToString().PadRight(10, ' ');
                                string NO_LOT = row["NO_LOT"].ToString().PadRight(10, ' ');
                                string QT_IO = row["QT_IO"].ToString();

                                string msg2 = NO_IOLINE + " " + CD_ITEM + " " + NO_LOT + " " + QT_IO;

                                검증리스트_품목.AppendLine(msg2);
                                검증여부 = true;
                            }
                        }

                        if (검증여부)
                        {
                            Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 존재하지안는 (항번/품목)이 존재합니다. \n " +
                            " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_품목.ToString());
                            _flexD.RowFilter = "NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " "; //라인항번으로 대치
                            _flexD.Redraw = true;
                            return;
                        }

                        #endregion

                        /* *********************************************************************************************** */

                        //Global.MainFrame.ShowMessage("1"); //////////////

                        #region -> 엑셀 Data 검증 ( 품목 중복 체크 )

                        DataTable dt엑셀Chk = dt엑셀.Copy();

                        foreach (DataRow row in dt엑셀.Rows)
                        {
                            if (row["CD_ITEM"].ToString().Trim() == null || row["CD_ITEM"].ToString().Trim() == string.Empty || row["CD_ITEM"].ToString().Trim() == "") { continue; }
                            if (row["NO_IOLINE"].ToString().Trim() == null || row["NO_IOLINE"].ToString().Trim() == string.Empty || row["NO_IOLINE"].ToString().Trim() == "") { continue; }

                            // string strFilter = " CD_ITEM = '" + row["CD_ITEM"].ToString().Trim() + "' AND NO_LOT = '" + row["NO_LOT"].ToString().Trim() + "' ";
                            //입고항번추가
                            string strFilter = " NO_IOLINE = " + row["NO_IOLINE"].ToString().Trim() + " AND CD_ITEM = '" + row["CD_ITEM"].ToString().Trim() + "' AND NO_LOT = '" + row["NO_LOT"].ToString().Trim() + "' ";

                            DataRow[] drChk = dt엑셀Chk.Select(strFilter, "", DataViewRowState.CurrentRows);

                            if (drChk.Length == 1)
                                UniqEnable = true;
                            else UniqEnable = false;

                            if (UniqEnable == true)
                            {
                                NewRowItem = dt엑셀품목중복체크.NewRow();

                                NewRowItem["S"] = "Y";
                                NewRowItem["CD_ITEM"] = row["CD_ITEM"];
                                NewRowItem["NO_IO"] = "";
                                NewRowItem["NO_IOLINE"] = row["NO_IOLINE"]; //수정 (추가)
                                NewRowItem["DT_IO"] = "";
                                NewRowItem["FG_IO"] = "";
                                NewRowItem["CD_QTIOTP"] = "";
                                NewRowItem["CD_SL"] = "";
                                NewRowItem["QT_IO"] = Convert.ToDecimal(row["QT_IO"]);
                                NewRowItem["FG_PS"] = "1";
                                NewRowItem["NO_LOT"] = D.GetString(row["NO_LOT"]);

                                NewRowItem["CD_MNG1"] = row["CD_MNG1"].ToString().Trim();
                                NewRowItem["CD_MNG2"] = row["CD_MNG2"].ToString().Trim();
                                NewRowItem["CD_MNG3"] = row["CD_MNG3"].ToString().Trim();
                                NewRowItem["CD_MNG4"] = row["CD_MNG4"].ToString().Trim();
                                NewRowItem["CD_MNG5"] = row["CD_MNG5"].ToString().Trim();
                                NewRowItem["CD_MNG6"] = row["CD_MNG6"].ToString().Trim();
                                NewRowItem["CD_MNG7"] = row["CD_MNG7"].ToString().Trim();
                                NewRowItem["CD_MNG8"] = row["CD_MNG8"].ToString().Trim();
                                NewRowItem["CD_MNG9"] = row["CD_MNG9"].ToString().Trim();
                                NewRowItem["CD_MNG10"] = row["CD_MNG10"].ToString().Trim();

                                NewRowItem["CD_MNG11"] = row["CD_MNG11"].ToString().Trim();
                                NewRowItem["CD_MNG12"] = row["CD_MNG12"].ToString().Trim();
                                NewRowItem["CD_MNG13"] = row["CD_MNG13"].ToString().Trim();
                                NewRowItem["CD_MNG14"] = row["CD_MNG14"].ToString().Trim();
                                NewRowItem["CD_MNG15"] = row["CD_MNG15"].ToString().Trim();
                                NewRowItem["CD_MNG16"] = row["CD_MNG16"].ToString().Trim();
                                NewRowItem["CD_MNG17"] = row["CD_MNG17"].ToString().Trim();
                                NewRowItem["CD_MNG18"] = row["CD_MNG18"].ToString().Trim();
                                NewRowItem["CD_MNG19"] = row["CD_MNG19"].ToString().Trim();
                                NewRowItem["CD_MNG20"] = row["CD_MNG20"].ToString().Trim();

                                dt엑셀품목중복체크.Rows.Add(NewRowItem);
                                품목적합 = false;

                                NO_ITEM = NewRowItem["CD_ITEM"].ToString();
                                MULTI_ITEM += NO_ITEM + "|";
                            }
                            else
                            {
                                //항번추가
                                string NO_IOLINE = row["NO_IOLINE"].ToString().PadRight(10, ' '); //추가 20081230
                                string CD_ITEM = row["CD_ITEM"].ToString().PadRight(10, ' ');
                                string NO_LOT = row["NO_LOT"].ToString().PadRight(10, ' ');
                                string QT_IO = row["QT_IO"].ToString();

                                string msg2 = NO_IOLINE + " " + CD_ITEM + " " + NO_LOT + " " + QT_IO;

                                검증리스트_품목.AppendLine(msg2);
                                검증여부 = true;
                            }
                        }

                        if (검증여부)
                        {
                            Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 중복되는 (항번/품목)과 LOT가 존재합니다. \n " +
                            " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_품목.ToString());
                            _flexD.RowFilter = "NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " "; //라인항번으로 대치
                            _flexD.Redraw = true;
                            return;

                        }

                        #endregion

                        /* *********************************************************************************************** */

                        #region -> 엑셀 Data 검증 ( 품목 마스터 체크 )


                        //Global.MainFrame.ShowMessage("2"); //////////////

                        DataTable MasterItemDt = _biz.ExcelSearch(MULTI_ITEM, "ITEM", D.GetString(_flexM["CD_PLANT"]));
                        검증여부 = false;


                        foreach (DataRow row in dt엑셀품목중복체크.Rows)
                        {
                            if (row["CD_ITEM"].ToString().Trim() == null || row["CD_ITEM"].ToString().Trim() == string.Empty || row["CD_ITEM"].ToString().Trim() == "") { continue; }
                            if (row["NO_IOLINE"].ToString().Trim() == null || row["NO_IOLINE"].ToString().Trim() == string.Empty || row["NO_IOLINE"].ToString().Trim() == "") { continue; }

                            foreach (DataRow drItem in MasterItemDt.Rows)
                            {
                                if (row["CD_ITEM"].ToString().Trim() == drItem["CD_ITEM"].ToString().Trim())
                                { 품목적합 = true; break; }
                                else 품목적합 = false;
                            }

                            if (품목적합 == true)
                            {
                                NewRowItem = dt엑셀품목마스터검증.NewRow();

                                NewRowItem["S"] = row["S"].ToString();
                                NewRowItem["CD_ITEM"] = row["CD_ITEM"].ToString().Trim();
                                NewRowItem["NO_IO"] = "";
                                NewRowItem["NO_IOLINE"] = row["NO_IOLINE"].ToString().Trim(); //수정 20081230
                                NewRowItem["DT_IO"] = "";
                                NewRowItem["FG_IO"] = "";
                                NewRowItem["CD_QTIOTP"] = "";
                                NewRowItem["CD_SL"] = "";
                                NewRowItem["QT_IO"] = row["QT_IO"];
                                NewRowItem["FG_PS"] = row["FG_PS"].ToString();
                                NewRowItem["NO_LOT"] = D.GetString(row["NO_LOT"]);

                                NewRowItem["CD_MNG1"] = row["CD_MNG1"].ToString().Trim();
                                NewRowItem["CD_MNG2"] = row["CD_MNG2"].ToString().Trim();
                                NewRowItem["CD_MNG3"] = row["CD_MNG3"].ToString().Trim();
                                NewRowItem["CD_MNG4"] = row["CD_MNG4"].ToString().Trim();
                                NewRowItem["CD_MNG5"] = row["CD_MNG5"].ToString().Trim();
                                NewRowItem["CD_MNG6"] = row["CD_MNG6"].ToString().Trim();
                                NewRowItem["CD_MNG7"] = row["CD_MNG7"].ToString().Trim();
                                NewRowItem["CD_MNG8"] = row["CD_MNG8"].ToString().Trim();
                                NewRowItem["CD_MNG9"] = row["CD_MNG9"].ToString().Trim();
                                NewRowItem["CD_MNG10"] = row["CD_MNG10"].ToString().Trim();

                                NewRowItem["CD_MNG11"] = row["CD_MNG11"].ToString().Trim();
                                NewRowItem["CD_MNG12"] = row["CD_MNG12"].ToString().Trim();
                                NewRowItem["CD_MNG13"] = row["CD_MNG13"].ToString().Trim();
                                NewRowItem["CD_MNG14"] = row["CD_MNG14"].ToString().Trim();
                                NewRowItem["CD_MNG15"] = row["CD_MNG15"].ToString().Trim();
                                NewRowItem["CD_MNG16"] = row["CD_MNG16"].ToString().Trim();
                                NewRowItem["CD_MNG17"] = row["CD_MNG17"].ToString().Trim();
                                NewRowItem["CD_MNG18"] = row["CD_MNG18"].ToString().Trim();
                                NewRowItem["CD_MNG19"] = row["CD_MNG19"].ToString().Trim();
                                NewRowItem["CD_MNG20"] = row["CD_MNG20"].ToString().Trim();

                                dt엑셀품목마스터검증.Rows.Add(NewRowItem);
                                품목적합 = false;
                            }
                            else
                            {
                                //항번추가
                                string NO_IOLINE = row["NO_IOLINE"].ToString().PadRight(10, ' ');
                                string CD_ITEM = row["CD_ITEM"].ToString().PadRight(10, ' ');
                                string NO_LOT = row["NO_LOT"].ToString().PadRight(10, ' ');
                                string QT_IO = row["QT_IO"].ToString();
                                string msg2 = NO_IOLINE + " " + CD_ITEM + " " + NO_LOT + " " + QT_IO;


                                검증리스트_품목.AppendLine(msg2);
                                검증여부 = true;
                            }
                        }

                        if (검증여부)
                        {
                            Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 마스터품목과 불일치 항목들이 존재합니다. \n " +
                            " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_품목.ToString());
                            _flexD.RowFilter = "NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " "; //라인항번으로 대치
                            _flexD.Redraw = true;
                            return;
                        }

                        #endregion

                        //Global.MainFrame.ShowMessage("3"); //////////////

                        /* *********************************************************************************************** */

                        #region -> 마지막 엑셀 Data 검증 ( Flex 라인상의 기본키 중복 체크 )

                        검증여부 = false;

                        //_dt.PrimaryKey = new DataColumn[] { _dt.Columns["CD_ITEM"] };
                        _dt.PrimaryKey = new DataColumn[] { _dt.Columns["NO_IOLINE"] }; //수정 20081230

                        //Global.MainFrame.ShowMessage("4"); //////////////

                        for (int i = 0; i < dt엑셀품목마스터검증.Rows.Count; i++)
                        {
                            if (dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim() == null || dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim() == string.Empty || dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim() == "") { continue; }
                            if (dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim() == null || dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim() == string.Empty || dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim() == "") { continue; }


                            //Global.MainFrame.ShowMessage("5-0-"+ i.ToString()); //////////////

                            if (Convert.ToDecimal(dt엑셀품목마스터검증.Rows[i]["QT_IO"]) > 0)
                                수량적합 = true;
                            else
                                수량적합 = false;

                            if (수량적합 == true)
                            {
                                //DataRow dr = _dt.Rows.Find(dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim());
                                DataRow dr = _dt.Rows.Find(dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim());
                                DataRow dr1 = _flexD.DataTable.NewRow();

                                dr1["S"] = dt엑셀품목마스터검증.Rows[i]["S"].ToString().Trim();
                                dr1["CD_ITEM"] = dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim();
                                dr1["NO_IO"] = dr["NO_IO"].ToString().Trim();
                                //dr1["NO_IOLINE"] = dr["NO_LINE"].ToString();

                                dr1["NO_IOLINE"] = dr["NO_IOLINE"].ToString();

                                dr1["DT_IO"] = dr["DT_IO"].ToString().Trim();
                                dr1["FG_IO"] = dr["FG_IO"].ToString().Trim();
                                dr1["CD_QTIOTP"] = dr["CD_QTIOTP"].ToString().Trim();
                                dr1["CD_SL"] = dr["CD_SL"].ToString().Trim();
                                dr1["QT_IO"] = dt엑셀품목마스터검증.Rows[i]["QT_IO"];
                                dr1["FG_PS"] = dt엑셀품목마스터검증.Rows[i]["FG_PS"].ToString().Trim();
                                dr1["NO_LOT"] = D.GetString(dt엑셀품목마스터검증.Rows[i]["NO_LOT"]);

                                dr1["CD_MNG1"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG1"].ToString().Trim();
                                dr1["CD_MNG2"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG2"].ToString().Trim();
                                dr1["CD_MNG3"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG3"].ToString().Trim();
                                dr1["CD_MNG4"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG4"].ToString().Trim();
                                dr1["CD_MNG5"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG5"].ToString().Trim();
                                dr1["CD_MNG6"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG6"].ToString().Trim();
                                dr1["CD_MNG7"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG7"].ToString().Trim();
                                dr1["CD_MNG8"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG8"].ToString().Trim();
                                dr1["CD_MNG9"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG9"].ToString().Trim();
                                dr1["CD_MNG10"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG10"].ToString().Trim();

                                dr1["CD_MNG11"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG11"].ToString().Trim();
                                dr1["CD_MNG12"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG12"].ToString().Trim();
                                dr1["CD_MNG13"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG13"].ToString().Trim();
                                dr1["CD_MNG14"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG14"].ToString().Trim();
                                dr1["CD_MNG15"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG15"].ToString().Trim();
                                dr1["CD_MNG16"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG16"].ToString().Trim();
                                dr1["CD_MNG17"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG17"].ToString().Trim();
                                dr1["CD_MNG18"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG18"].ToString().Trim();
                                dr1["CD_MNG19"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG19"].ToString().Trim();
                                dr1["CD_MNG20"] = dt엑셀품목마스터검증.Rows[i]["CD_MNG20"].ToString().Trim();

                                _flexD.DataTable.Rows.Add(dr1);
                            }
                            else
                            {
                                //항번추가
                                string NO_IOLINE = dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim().PadRight(10, ' ');
                                string CD_ITEM = dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim().PadRight(10, ' ');
                                string NO_LOT = dt엑셀품목마스터검증.Rows[i]["NO_LOT"].ToString().Trim().PadRight(10, ' ');
                                string QT_IO = dt엑셀품목마스터검증.Rows[i]["QT_IO"].ToString().Trim();
                                string msg2 = NO_IOLINE + " " + CD_ITEM + " " + NO_LOT + " " + QT_IO;

                                검증리스트_품목.AppendLine(msg2);
                                검증여부 = true;
                            }
                        }

                        #endregion

                        /* *********************************************************************************************** */

                        if (검증여부)
                        {
                            Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 부적절한 수량이 포함된 항목들이 존재합니다. \n " +
                           " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_품목.ToString());
                        }

                        Global.MainFrame.ShowMessage("엑셀 작업을 완료하였습니다. 확인버튼을 눌러주세요!");

                        if (!_flexD.HasNormalRow)
                        {
                            ExcelChk = false;
                            _btn엑셀.Text = "전체삭제";
                        }

                        //도움창 open시에 라인추가가 되어있어서.. 엑셀업로드시 빈칸 한줄을 건건히 지워야하는 불편함이 있어서.... 
                        //no_lot 가 null인걸 제외한다....

                        DataTable dtBing = _flexD.DataTable.Clone();

                        foreach (DataRow row in _flexD.DataTable.Rows)
                        {
                            if (D.GetString(row["NO_LOT"]) != string.Empty)
                            {
                                dtBing.ImportRow(row);
                            }
                        }

                        _flexD.Binding = dtBing;

                        _flexM.RowFilter = "";
                        //_flexD.RowFilter = "CD_ITEM = '" + _flexM[_flexM.Row, "CD_ITEM"].ToString() + "'";
                        _flexD.RowFilter = "NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " "; //라인항번으로 대치

                        _flexD.Redraw = true;

                    }
                }   //      if (ExcelChk == true)
                else
                {
                    DeleteAll();  // 전체삭제기능 활성화
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                _flexD.Redraw = true;
            }
        }

        #endregion

        #region -> 자동채번 (주)매커스전용 lot가 날짜+시퀀스(5자리) 경우에만 사용가능
        private void btn_Seq_Click(object sender, EventArgs e)
        {
            try
            {
                _flexD.Redraw = false;

                for (int r = _flexD.DataTable.Rows.Count - 1; r >= 0; r--)
                    _flexD.DataTable.Rows.RemoveAt(r);

                foreach (DataRow row in _flexM.DataTable.Rows)
                {
                    string sNoLot = _biz.GetMaxLot(D.GetString(row["CD_ITEM"]));
                    string sNoLotCopy = sNoLot;
                    decimal dSEQ = 0;
                    string sSEQ = "";

                    if (sNoLot == "")
                    {
                        sNoLotCopy = D.GetString(row["DT_IO"]);
                        dSEQ++;
                    }
                    else
                    {
                        sNoLot = sNoLot.PadRight(8, '0');
                        sNoLotCopy = sNoLot.Remove(8, sNoLot.Length - 8);

                        if (sNoLotCopy == D.GetString(_flexM["DT_IO"]))
                        {
                            sSEQ = sNoLot.Remove(0, 8);
                            dSEQ = D.GetDecimal(sSEQ) + 1;

                        }
                        else
                        {
                            sNoLotCopy = D.GetString(row["DT_IO"]);
                            dSEQ++;
                        }


                        sNoLot = sNoLotCopy + D.GetString(dSEQ).PadLeft(5, '0');
                    }

                        DataRow[] rowCHECK = _flexD.DataTable.Select("CD_ITEM = '" + D.GetString(row["CD_ITEM"]) + "'");

                        _flexD.Rows.Add();
                        _flexD.Row = _flexD.Rows.Count - 1;
                        _flexD["S"] = "N";
                        _flexD["CD_ITEM"] = row["CD_ITEM"];
                        _flexD["NO_IO"] = row["NO_IO"];
                        _flexD["NO_IOLINE"] = row["NO_IOLINE"];
                        _flexD["DT_IO"] = row["DT_IO"];
                        _flexD["FG_IO"] = row["FG_IO"];
                        _flexD["CD_QTIOTP"] = row["CD_QTIOTP"];
                        _flexD["CD_SL"] = row["CD_SL"];
                        _flexD["QT_IO"] = row["QT_GOOD_INV"];
                        _flexD["FG_PS"] = "1";

                        if (rowCHECK.Length > 0 && rowCHECK != null)
                        {
                            string no_lot = D.GetString(_flexD.DataTable.Compute("MAX(NO_LOT)","CD_ITEM = '" + D.GetString(row["CD_ITEM"]) + "'"));

                            dSEQ = D.GetDecimal(no_lot.Remove(0,8)) + 1;
  
                        }

                        _flexD["NO_LOT"] = sNoLotCopy + D.GetString(dSEQ).PadLeft(5, '0');
                      

                        _flexD.AddFinished();
                        _flexD.Col = _flexD.Cols.Fixed;
                        _flexD.Focus();
             
                    
                }

                _flexD.Redraw = true;
            }
            catch (Exception ex)
            {
                _flexD.Redraw = true;
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region  ★ 그리드 이벤트

        #region -> _flexM_AfterRowChange

        private void _flexM_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            if (!_flexM.IsBindingEnd || !_flexM.HasNormalRow) return;

            DataTable dt = null;

            string Filter = "NO_IO = '" + _flexM[_flexM.Row, "NO_IO"].ToString() + "' AND NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + "";

            if (_flexM.DetailQueryNeed)
            {
                dt = _biz.Search_Detail(_flexM[_flexM.Row, "NO_IO"].ToString());
            }

            _flexD.BindingAdd(dt, Filter);
            _flexM.DetailQueryNeed = false;

            if (_pageid != "P_SA_GI_SWITCH_YN_AM")
            {
                if (_flexD.DataView.Count < 1)
                    추가_Click(null, null);
            }
        }

        #endregion

        #region -> _flexD_ValidateEdit

        private void _flexD_ValidateEdit(object sender, C1.Win.C1FlexGrid.ValidateEditEventArgs e)
        {
            try
            {
                string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
                string newValue = ((FlexGrid)sender).EditData;

                //if (!추가모드여부)
                //    if (oldValue.ToUpper() == newValue.ToUpper()) return;

                if (_flexD.GetData(e.Row, e.Col).ToString() != _flexD.EditData)
                {
                    switch (_flexD.Cols[e.Col].Name)
                    {
                        case "QT_IO":
                            //if (_flex[_flex.Row, "NO_REQ"].ToString() != "" && _flex[_flex.Row, "NO_REQ"] != null)
                            //{
                            //    if (_flex.CDecimal(newValue) > _flex.CDecimal(_flex[_flex.Row, "QT_REQ"]))
                            //    {
                            //        this.ShowMessage("입고량이 의뢰량을 넘을 수 없습니다!");
                            //        _flex[_flex.Row, "QT_GOOD_INV"] = _flex.CDecimal(oldValue);
                            //        return;
                            //    }
                            //}
                            _flexD[_flexD.Row, "QT_IO"] = _flexD.CDecimal(newValue);
                            _flexD[_flexD.Row, "QT_IO_OLD"] = _flexD.CDecimal(oldValue);

                            break;

                        default:
                            break;
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

        #region  ♣ 기타 메소드

        #region -> DeleteAll

        private void DeleteAll()
        {
            if (!_flexD.HasNormalRow)
                return;

            _flexD.Redraw = false;

            DataRow[] dr = _flexD.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

            foreach (DataRow row in dr)
            {
                row.Delete();
            }

            _flexD.Redraw = true;

            _btn엑셀.Text = "엑셀업로드";

            ExcelChk = true;

        }

        #endregion

        #region -> Set_Line

        private void Set_Line()
        {
            if (_flexM.DataTable == null) return;

            string Multi_No_io_mgmt = string.Empty;

            foreach (DataRow row in _flexM.DataTable.Rows)
                Multi_No_io_mgmt += D.GetString(row["NO_IO_MGMT"]) + "|";

            DataTable dt = _biz.dt_SER_MGMT(Multi_No_io_mgmt);

            foreach (DataRow row in _flexM.DataTable.Rows)
            {
                DataRow[] rows = dt.Select("NO_IO_MGMT = '" + D.GetString(row["NO_IO_MGMT"]) + "' AND NO_IOLINE_MGMT = " + D.GetDecimal(row["NO_IOLINE_MGMT"]) + "");

                foreach (DataRow rowFind in rows)
                {
                    _flexD.Rows.Add();
                    _flexD.Row = _flexD.Rows.Count - 1;
                    _flexD["S"] = "N";
                    _flexD["CD_ITEM"] = D.GetString(row["CD_ITEM"]);
                    _flexD["NO_IO"] = D.GetString(row["NO_IO"]);
                    _flexD["NO_IOLINE"] = D.GetDecimal(row["NO_IOLINE"]);
                    _flexD["DT_IO"] = D.GetString(row["DT_IO"]);
                    _flexD["FG_IO"] = D.GetString(row["FG_IO"]);
                    _flexD["CD_QTIOTP"] = D.GetString(row["CD_QTIOTP"]);
                    _flexD["CD_SL"] = D.GetString(row["CD_SL"]);
                    _flexD["QT_IO"] = D.GetDecimal(row["QT_GOOD_INV"]);
                    _flexD["FG_PS"] = "1";
                    _flexD["NO_LOT"] = D.GetString(rowFind["NO_LOT"]);
                    _flexD.AddFinished();
                }
            }
        }

        #endregion

        #region -> 종료
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _flexM.SaveUserCache("P_PU_LOT_SUB_flexM");
            _flexD.SaveUserCache("P_PU_LOT_SUB_flexD");
        }
        #endregion
        #endregion

        public DataTable dtL { get { return _dtL; } }
        public string SetPageId { set { _pageid = value; } }

    }
}
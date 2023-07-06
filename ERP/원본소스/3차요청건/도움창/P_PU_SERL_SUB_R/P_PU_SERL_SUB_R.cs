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

// 2010.07.05 - 안종호 -  시리얼 수량 통제 설정 추가
namespace pur
{
    public partial class P_PU_SERL_SUB_R : Duzon.Common.Forms.CommonDialog
    {
        #region ★ 멤버필드

        private P_PU_SERL_SUB_R_BIZ _biz = new P_PU_SERL_SUB_R_BIZ();
        private OpenFileDialog m_FileDlg = new OpenFileDialog();
        public Duzon.Common.Forms.IMainFrame _MainFrameInterface;
        private DataTable _dt_EXCEL = null;
        private bool ExcelChk = true;
        public DataTable _dt = null;
        public DataTable _dtL = null;
        private string Searial_Ctl= "000";  //시리얼 수량통제설정
        string strModule = "";
        string[] CD_MNG = new string[20];   //관리항목-컬럼명
        string _pageid = string.Empty;
        DataTable _dtH = null;
        string _YnSave = string.Empty;

        #endregion

        #region ★ 초기화

        #region -> 생성자

        public P_PU_SERL_SUB_R(DataTable dt) : this(dt, "") { }

        public P_PU_SERL_SUB_R(DataTable dt, string strModule)
        {
            InitializeComponent();

            _dt = dt;

            _dt.Columns.Add("QT_SERIAL_COUNT", typeof(decimal)); //하단시리얼갯수확인용

            this.strModule = strModule;
        }

        #endregion

        #region -> InitLoad
        protected override void InitLoad()
        {
            base.InitLoad();

            //관리항목-컬럼명 :다이렉트로 가져오기
            _dtH = MA.GetCode("PU_C000036");

            _dtH.Columns.Add("CD_MNG_NM", typeof(string), ""); //컬럼코드


            foreach (DataRow dr in _dtH.Rows)
            {
                for (int i = 0; i < CD_MNG.Length; i++)
                {
                    if (D.GetDecimal(dr["CODE"]) == i + 1)
                    {  
                        CD_MNG[i] = dr["NAME"].ToString();
                        dr["CD_MNG_NM"] = "CD_MNG" + (i + 1).ToString(); //관리항목과 컬럼코드 맵핑하기위해
                    }
                }
            }

            InitGridM();
            InitGridD();

            _flexM.Cols["NO_IO_MGMT"].Visible = false;
            _flexM.Cols["NO_IOLINE_MGMT"].Visible = false;
            _flexM.Cols["FG_IO"].Visible = false;
            _flexM.Cols["CD_QTIOTP"].Visible = false;

            _flexM.DetailGrids = new FlexGrid[] { _flexD };

            Searial_Ctl = BASIC.GetMAEXC("시리얼등록시_수량제한여부");//수량제한 여부 값

            //DataTable dt_D = _biz.Search_Detail("");
            DataTable dt_D = _biz.Search_Detail("", 0);

            _flexD.Binding = dt_D;
            _flexM.Binding = _dt;
            _dtL = dt_D;

            if (_pageid == "P_SA_GI_SWITCH_YN_AM")
                Set_Line();

            //아래 QT_SERIAL_COUNT를 알기위한 고육지책.._flexD detail queryNeed mode임 

            _flexD.Redraw = false;
            _flexM.Redraw = false;

            for (int i = 1; i < _flexM.Rows.Count; i++)
            {
                _flexM.Row = i;
            }

            foreach (DataRow row in _dt.Rows)
            {
                Decimal serl_cnt = 0m;
                serl_cnt = D.GetDecimal(_flexD.DataTable.Select("NO_IO = '" + row["NO_IO"].ToString() + "' AND NO_IOLINE = " + row["NO_IOLINE"].ToString() + " ", "", DataViewRowState.CurrentRows).Length);
                row["QT_SERIAL_COUNT"] = serl_cnt;
            }

            _flexD.Redraw = true;
            _flexM.Redraw = true;

            if (_YnSave == "Y")
            {
                roundedButton2.Enabled = false;
            }
            else if (_YnSave == "N")
            {
                //btn_confirm.Enabled = false;
                _btn엑셀.Enabled = false;
                roundedButton1.Enabled = false;
                roundedButton2.Enabled = false;
                btn적용.Enabled = false;
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
            _flexM.SetCol("NO_IO", "수불번호", 120);
            _flexM.SetCol("NO_IOLINE", "수불항번", 120); //추가 20081230 EXCEL적용시 동일한 품목을 별도 구분하기위함)
            _flexM.SetCol("DT_IO", "수불일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexM.SetCol("NM_QTIOTP", "수불형태", 120);
            _flexM.SetCol("CD_ITEM", "품목코드", 120);
            _flexM.SetCol("NM_ITEM", "품목명", 120);
            _flexM.SetCol("UNIT_IM", "단위", 80);
            _flexM.SetCol("STND_ITEM", "규격", 120);
            _flexM.SetCol("QT_GOOD_INV", "처리수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexM.SetCol("CD_SL", "창고코드", 120);
            _flexM.SetCol("NM_SL", "창고명", 120);

            _flexM.SetCol("QT_SERIAL_COUNT", "등록시리얼갯수", 120, false, typeof(decimal)); //

            _flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexM.AfterRowChange += new RangeEventHandler(_flexM_AfterRowChange);
            _flexM.LoadUserCache("P_PU_SERL_SUB_R_flexM");
        }

        #endregion

        #region -> InitGridD

        private void InitGridD()
        {
            _flexD.BeginSetting(1, 1, false);
            _flexD.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            //_flexD.SetCol("NO_LOT", "LOT번호", 120, true);
            //_flexD.SetCol("QT_IO", "처리수량", 120, true, typeof(decimal), FormatTpType.QUANTITY);
            //_flexD.Cols["QT_IO"].Format = "#,###.##";
            _flexD.SetCol("NO_SERIAL", "시리얼번호", 120, true);

            // 소스 가독성 및 생산성 고려해서 위와같이 변경
            for (int i = 0; i < CD_MNG.Length; i++)
            {
                if (CD_MNG[i] != string.Empty)
                {
                    if (Global.MainFrame.ServerKeyCommon.Contains("GIGAVIS") && i + 1 == 20)
                       _flexD.SetCol("CD_MNG" + (i + 1).ToString(), CD_MNG[i], 120, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                   else
                    _flexD.SetCol("CD_MNG" + (i + 1).ToString(), CD_MNG[i], 120, true);
                }
            }

           // _flexD.NewRowEditable = true;
           // _flexD.EnterKeyAddRow = true;
            if (Global.MainFrame.ServerKeyCommon == "KISERP" && _flexD.Cols.Contains("CD_MNG15"))
                _flexD.Cols["CD_MNG15"].AllowEditing = false;

            _flexD.SetDummyColumn("S");
            _flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexD.ValidateEdit += new ValidateEditEventHandler(_flexD_ValidateEdit);
            _flexD.StartEdit += new RowColEventHandler(_flexD_Grid_StartEdit);
            _flexD.KeyDownEdit += new KeyEditEventHandler(_flexD_KeyDownEdit);
            _flexD.LoadUserCache("P_PU_SERL_SUB_R_flexD");
        }

       

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet lds_Result = Global.MainFrame.GetComboData("N;PU_C000036");

            // 관리항목				
            cbo_CD_MNG.DataSource = lds_Result.Tables[0];
            cbo_CD_MNG.DisplayMember = "NAME";
            cbo_CD_MNG.ValueMember = "CODE";
        }

        #endregion

        #endregion

        #region ★ 버튼이벤트

        #region -> 추가

        private void 추가_Click(object sender, EventArgs e)
        {
            try
            
            {

               string type = sender.GetType().Name;
                _flexD.Redraw = false;

                if (type == "FlexGrid") // 그리드에서 엔터키로 들어왔을때
                {
                    LINE_ADD();//빈라인 생성
                    _flexD.Select(_flexD.Rows.Count - 1, "NO_SERIAL");
                    _flexD.Focus();
                    //_flexD.
                }
                else //추가버튼 클릭으로 들어왔을때
                {
                    if (D.GetString(txt_NO_H.Text) != "" && D.GetString(txt_NO_D.Text) != "" && D.GetString(txt_qt.Text) != "")
                    {
                        Auto_LINE_ADD(); //라인자동생성
                       
                    }
                    else
                    {
                        LINE_ADD();//빈라인 생성
                    }
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

        #region -> btn적용 클릭 이벤트
        // 2011/01/06 조형우 적용버튼 추가
        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.MainFrame.ServerKeyCommon == "KISERP" && D.GetString(cbo_CD_MNG.SelectedIndex + 1) == "15")
                    return;

                _flexD.Redraw = false;

                DataTable dt = _flexD.GetCheckedRows("S");

                if (dt != null)
                {
                    for (int i = 0; i < _flexD.DataTable.Rows.Count; i++)
                    {
                        if (_flexD.DataTable.Rows[i]["S"].ToString() == "Y")
                        {
                            _flexD.DataTable.Rows[i]["CD_MNG" + D.GetString(cbo_CD_MNG.SelectedIndex + 1)] = txt_MNG.Text;
                        }
                    }
                    Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, "적용");
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

        #region -> 빈라인생성 메소드
        private void LINE_ADD()
        {
            if (_flexD.DataTable == null) return;

            _flexD.Rows.Add();
            _flexD.Row = _flexD.Rows.Count - 1;
            _flexD["S"] = "N";
            _flexD["CD_ITEM"] = _flexM[_flexM.Row, "CD_ITEM"];
            _flexD["NO_IO"] = _flexM[_flexM.Row, "NO_IO"];
            _flexD["NO_IOLINE"] = _flexM[_flexM.Row, "NO_IOLINE"];
            //_flexD["DT_IO"] = _flexM[_flexM.Row, "DT_IO"];

            _flexD["FG_IO"] = _flexM[_flexM.Row, "FG_IO"];
            _flexD["CD_QTIOTP"] = _flexM[_flexM.Row, "CD_QTIOTP"];

            //_flexD["CD_SL"] = _flexM[_flexM.Row, "CD_SL"];
            //if (_flexD.Rows.Count == 2)
            //    _flexD["QT_IO"] = _flexM[_flexM.Row, "QT_GOOD_INV"];
            //else
            //    _flexD["QT_IO"] = 0;
            //_flexD["FG_PS"] = "1";

            //_flexD["NO_LOT"] = "";

            if (Global.MainFrame.ServerKeyCommon == "KISERP")
            {
                _flexD["CD_MNG15"] = Math.Truncate(D.GetDecimal(_flexM[_flexM.Row, "UM"]));
            }

            _flexD.AddFinished();
            _flexD.Select(_flexD.Rows.Count - 1, "NO_SERIAL");
            _flexD.Focus();

            Decimal serl_cnt = D.GetDecimal(_flexD.DataTable.Select("NO_IO = '" + _flexM[_flexM.Row, "NO_IO"].ToString() + "' AND NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " ", "", DataViewRowState.CurrentRows).Length);
            _flexM[_flexM.Row, "QT_SERIAL_COUNT"] = serl_cnt;
        }
        #endregion

        #region -> 라인자동생성 메소드
        private void Auto_LINE_ADD()
        {
            if (_flexD.DataTable == null) return;

            string colname_mng = Search_CD_MNG(D.GetString(cbo_CD_MNG.SelectedValue)); // 관리항목 콤보박승에서 그리드 컬럼명 뽑아오기

            int detail_count = D.GetInt(txt_NO_D.Text); //카운트를 숫자형으로 변환한것
            string str_count = string.Empty; //최종 입력될 카운트
            int serl_qt = D.GetInt(txt_qt.Text);
            for (int i = 0; i < serl_qt; i++)
            {
                if (txt_NO_D.Text.Trim().Length < D.GetString(detail_count).Length) //자동생성되는 카운트가 기본푸맷보다 크면 메세지 리턴
                {
                    Global.MainFrame.ShowMessage(공통메세지._와_은같아야합니다, new string[] { " '" + D.GetString(txt_NO_H.Text) + D.GetString(txt_NO_D.Text) + "' 문자 길이", "생성된 시리얼 번호 문자길이" });
                    break;
                }

                str_count = count_write(detail_count); //수량 앞에 '0' 으로 채워넣기

                if (_flexD.DataTable.Select("NO_SERIAL = '" + D.GetString(txt_NO_H.Text) + str_count + "'").Length > 0) //시리얼 중복체크
                {
                    Global.MainFrame.ShowMessage(공통메세지._의값이중복되었습니다, "시리얼번호"); 
                    break;
                }

                _flexD.Rows.Add();
                _flexD.Row = _flexD.Rows.Count - 1;
                _flexD["S"] = "N";
                _flexD["CD_ITEM"] = _flexM[_flexM.Row, "CD_ITEM"];
                _flexD["NO_IO"] = _flexM[_flexM.Row, "NO_IO"];
                _flexD["NO_IOLINE"] = _flexM[_flexM.Row, "NO_IOLINE"];

                _flexD["FG_IO"] = _flexM[_flexM.Row, "FG_IO"];
                _flexD["CD_QTIOTP"] = _flexM[_flexM.Row, "CD_QTIOTP"];

                _flexD["NO_SERIAL"] = D.GetString(txt_NO_H.Text) + str_count + D.GetString(txt_no_L.Text); 
                _flexD[colname_mng] = txt_MNG.Text;


                if (Global.MainFrame.ServerKeyCommon == "KISERP")
                {
                    _flexD["CD_MNG15"] = Math.Truncate(D.GetDecimal(_flexM[_flexM.Row, "UM"]));
                }
                detail_count++;
         
            }
            _flexD.AddFinished();
            _flexD.Col = _flexD.Cols.Fixed;
            _flexD.Focus();
            
            Decimal serl_cnt = D.GetDecimal(_flexD.DataTable.Select("NO_IO = '" + _flexM[_flexM.Row, "NO_IO"].ToString() + "' AND NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " ", "", DataViewRowState.CurrentRows).Length);
            _flexM[_flexM.Row, "QT_SERIAL_COUNT"] = serl_cnt;
        }
        #endregion

        #region -> 숫자 앞에 '0' 채우는 함수
        private string count_write(int detail_count)
        {
            string str_count = string.Empty;

            for (int y = 0; y < D.GetString(txt_NO_D.Text).Length - D.GetString(detail_count).Length; y++)
                str_count += "0";

            str_count = str_count + D.GetString(detail_count);

            return str_count;
        }
        #endregion

        #region -> 관리항목 컬럼 코드 찾기
        private string Search_CD_MNG(string code) //관리항목 컬럼 코드 찾기
        {
            string column_code = string.Empty;

            switch (code)
            {
                case "001":
                    column_code = "CD_MNG1";
                    break;
                case "002":
                    column_code = "CD_MNG2";
                    break;
                case "003":
                    column_code = "CD_MNG3";
                    break;
                case "004":
                    column_code = "CD_MNG4";
                    break;
                case "005":
                    column_code = "CD_MNG5";
                    break;
                case "006":
                    column_code = "CD_MNG6";
                    break;
                case "007":
                    column_code = "CD_MNG7";
                    break;
                case "008":
                    column_code = "CD_MNG8";
                    break;
                case "009":
                    column_code = "CD_MNG9";
                    break;
                case "010":
                    column_code = "CD_MNG10";
                    break;
                case "011":
                    column_code = "CD_MNG11";
                    break;
                case "012":
                    column_code = "CD_MNG12";
                    break;
                case "013":
                    column_code = "CD_MNG13";
                    break;
                case "014":
                    column_code = "CD_MNG14";
                    break;
                case "015":
                    column_code = "CD_MNG15";
                    break;
                case "016":
                    column_code = "CD_MNG16";
                    break;
                case "017":
                    column_code = "CD_MNG17";
                    break;
                case "018":
                    column_code = "CD_MNG18";
                    break;
                case "019":
                    column_code = "CD_MNG19";
                    break;
                case "020":
                    column_code = "CD_MNG20";
                    break;
            }
            return column_code;
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
                    _flexD.Redraw = false;

                    DataRow[] rows = _flexD.DataTable.Select("S ='Y'");

                    if (rows != null & rows.Length > 0)
                    {
                        for (int i = 0; i < rows.Length; i++)
                        {
                            rows[i].Delete();
                        }
                    }
                    else
                    {
                        Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    }

                    Decimal serl_cnt = 0m;

                    foreach (DataRow row in _flexM.DataTable.Rows)
                    {
                        serl_cnt = D.GetDecimal(_flexD.DataTable.Select("NO_IO = '" + row["NO_IO"].ToString() + "' AND NO_IOLINE = " + row["NO_IOLINE"].ToString() + " ", "", DataViewRowState.CurrentRows).Length);
                        row["QT_SERIAL_COUNT"] = serl_cnt;
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

                string _NO_IO = "";
                string _NO_IOLINE = "";
                decimal _QT_GOOD = 0;

                Boolean 다시안묻기 = false;

                for (int i = _flexM.Rows.Fixed; i < _flexM.Rows.Count; i++)
                {
                    _NO_IO = D.GetString(_flexM[i, "NO_IO"]);
                    _NO_IOLINE = D.GetString(_flexM[i, "NO_IOLINE"]);
                     DataRow[] DR = _flexD.DataTable.Select("NO_IO = '" + _NO_IO + "' AND NO_IOLINE = '" + _NO_IOLINE + "'");
                    _QT_GOOD = 0;
 
                    foreach (DataRow myDRV in DR)
                    {

                        if (Convert.ToDecimal(_flexD.DataTable.Compute("Count(NO_SERIAL)", "NO_IO = '" + D.GetString(myDRV["NO_IO"]) + "' AND NO_IOLINE = '" + D.GetString(myDRV["NO_IOLINE"]) + "' AND NO_SERIAL = '" + D.GetString(myDRV["NO_SERIAL"]) + "'")) > 1)
                        {
                            Global.MainFrame.ShowMessage("현재 내역에 반복 입력된 시리얼번호입니다. :" + myDRV["NO_SERIAL"].ToString().Trim());
                            _flexM.Select(i, "CD_ITEM");
                            _flexM.Focus();
                            return;
                        }
                        _QT_GOOD = _QT_GOOD + 1; //시리얼은 수량개념이 없슴 _flexM.CDecimal(myDRV["QT_IO"].ToString());

                        if (Use서버키)
                        {
                            string 품목코드 = D.GetString(_flexM[i, "CD_ITEM"]);
                            String 시리얼 = "";
                            if (D.GetString(myDRV["NO_SERIAL"]).Contains(품목코드 + " "))
                                continue;

                            시리얼 = D.GetString(품목코드 + " " + D.GetString(myDRV["NO_SERIAL"]));

                            myDRV["NO_SERIAL"] = D.GetString(시리얼);
                        }
                    }

                    if (D.GetInt(_flexM[i, "QT_GOOD_INV"]) < _QT_GOOD)
                    {
                        string msg = "품목코드 '" + _flexM[i, "CD_ITEM"].ToString() + "' 의 처리수량 [" + D.GetInt(_flexM[i, "QT_GOOD_INV"])
                                                + "] 보다 시리얼갯수합 [" + _QT_GOOD + "] 이 많습니다! ";
                        Global.MainFrame.ShowMessage(msg);
                        return;
                    }
                    else if (Searial_Ctl == "100" &&  D.GetInt(_flexM[i, "QT_GOOD_INV"]) > _QT_GOOD) // 시리얼 미달 통제설정을 사용할경우에만 사용됨
                    {
                        string msg = "품목코드 '" + _flexM[i, "CD_ITEM"].ToString() + "' 의 처리수량 [" + D.GetInt(_flexM[i, "QT_GOOD_INV"])
                                                + "] 보다 시리얼갯수합 = [" + _QT_GOOD + "] 이 적습니다.! ";
                        Global.MainFrame.ShowMessage(msg);
                        return;
                    }

                    if (다시안묻기 == false)
                    {
                        if (D.GetInt(_flexM[i, "QT_GOOD_INV"]) != _QT_GOOD)
                        {
                            DialogResult result;

                            string msg = "품목코드 '" + _flexM[i, "CD_ITEM"].ToString() + "' 의 처리수량 = [" + D.GetInt(_flexM[i, "QT_GOOD_INV"])
                                         + "] 과 시리얼갯수합 = [" + _QT_GOOD + "] 이 일치하지 않습니다! \r\n"
                                         + "그대로 진행하시겠습니까? (이후 같은 사항은 더이상 chk안함)";

                            result = Global.MainFrame.ShowMessage(msg, "QY2");

                            if (result == DialogResult.Yes)
                                다시안묻기 = true;
                            else
                                return;
                        } 
                    }
                }
               
                DataTable dtL = _flexD.GetChanges();

                if (_YnSave == "Y" && dtL != null )
                {
                    bool save = _biz.Save(dtL, D.GetString(_flexM["CD_PLANT"]));
                    
                    if (!save) return;

                    Global.MainFrame.ShowMessage("저장이 완료되었습니다.");
                }
                
                //if (dtL == null) return;
                _flexD.AcceptChanges();
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

        //#region -> 저장
        //private void btn저장_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!_flexD.HasNormalRow) return;

        //        string _NO_IO = "";
        //        string _NO_IOLINE = "";
        //        decimal _QT_GOOD = 0;

        //        Boolean 다시안묻기 = false;

        //        for (int i = _flexM.Rows.Fixed; i < _flexM.Rows.Count; i++)
        //        {
        //            _NO_IO = D.GetString(_flexM[i, "NO_IO"]);
        //            _NO_IOLINE = D.GetString(_flexM[i, "NO_IOLINE"]);
        //            DataRow[] DR = _flexD.DataTable.Select("NO_IO = '" + _NO_IO + "' AND NO_IOLINE = '" + _NO_IOLINE + "'");
        //            _QT_GOOD = 0;

        //            foreach (DataRow myDRV in DR)
        //            {

        //                if (Convert.ToDecimal(_flexD.DataTable.Compute("Count(NO_SERIAL)", "NO_IO = '" + D.GetString(myDRV["NO_IO"]) + "' AND NO_IOLINE = '" + D.GetString(myDRV["NO_IOLINE"]) + "' AND NO_SERIAL = '" + D.GetString(myDRV["NO_SERIAL"]) + "'")) > 1)
        //                {
        //                    Global.MainFrame.ShowMessage("현재 내역에 반복 입력된 시리얼번호입니다. :" + myDRV["NO_SERIAL"].ToString().Trim());
        //                    _flexM.Select(i, "CD_ITEM");
        //                    _flexM.Focus();
        //                    return;
        //                }
        //                _QT_GOOD = _QT_GOOD + 1; //시리얼은 수량개념이 없슴 _flexM.CDecimal(myDRV["QT_IO"].ToString());

        //                if (Use서버키)
        //                {
        //                    string 품목코드 = D.GetString(_flexM[i, "CD_ITEM"]);
        //                    String 시리얼 = "";
        //                    if (D.GetString(myDRV["NO_SERIAL"]).Contains(품목코드 + " "))
        //                        continue;

        //                    시리얼 = D.GetString(품목코드 + " " + D.GetString(myDRV["NO_SERIAL"]));

        //                    myDRV["NO_SERIAL"] = D.GetString(시리얼);
        //                }
        //            }

        //            if (D.GetInt(_flexM[i, "QT_GOOD_INV"]) < _QT_GOOD)
        //            {
        //                string msg = "품목코드 '" + _flexM[i, "CD_ITEM"].ToString() + "' 의 처리수량 [" + D.GetInt(_flexM[i, "QT_GOOD_INV"])
        //                                        + "] 보다 시리얼갯수합 [" + _QT_GOOD + "] 이 많습니다! ";
        //                Global.MainFrame.ShowMessage(msg);
        //                return;
        //            }
        //            else if (Searial_Ctl == "100" && D.GetInt(_flexM[i, "QT_GOOD_INV"]) > _QT_GOOD) // 시리얼 미달 통제설정을 사용할경우에만 사용됨
        //            {
        //                string msg = "품목코드 '" + _flexM[i, "CD_ITEM"].ToString() + "' 의 처리수량 [" + D.GetInt(_flexM[i, "QT_GOOD_INV"])
        //                                        + "] 보다 시리얼갯수합 = [" + _QT_GOOD + "] 이 적습니다.! ";
        //                Global.MainFrame.ShowMessage(msg);
        //                return;
        //            }

        //            if (다시안묻기 == false)
        //            {
        //                if (D.GetInt(_flexM[i, "QT_GOOD_INV"]) != _QT_GOOD)
        //                {
        //                    DialogResult result;

        //                    string msg = "품목코드 '" + _flexM[i, "CD_ITEM"].ToString() + "' 의 처리수량 = [" + D.GetInt(_flexM[i, "QT_GOOD_INV"])
        //                                 + "] 과 시리얼갯수합 = [" + _QT_GOOD + "] 이 일치하지 않습니다! \r\n"
        //                                 + "그대로 진행하시겠습니까? (이후 같은 사항은 더이상 chk안함)";

        //                    result = Global.MainFrame.ShowMessage(msg, "QY2");

        //                    if (result == DialogResult.Yes)
        //                        다시안묻기 = true;
        //                    else
        //                        return;
        //                }
        //            }
        //        }

        //        DataTable dtL = _flexD.GetChanges();

        //        bool save = _biz.Save(dtL, _CdPlant);

        //        if (!save) return;

        //        Global.MainFrame.ShowMessage("저장이 완료되었습니다.");

        //        _flexD.AcceptChanges();
   
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.MainFrame.MsgEnd(ex);
        //    }
            
        //} 
        //#endregion
        #endregion

        #region ★ 기타 메소드

        private void _flexM_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            if (!_flexM.IsBindingEnd || !_flexM.HasNormalRow) return;

            DataTable dt = null;

            string Filter = string.Empty;

            Filter = "NO_IO= '" + _flexM[_flexM.Row, "NO_IO"].ToString() + "' AND NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + ""; //AND NO_LOT = '" + _flexM[_flexM.Row, "NO_LOT"].ToString() + "'";

            if (_flexM.DetailQueryNeed)
            {
                //dt = _biz.Search_Detail(_flexM[_flexM.Row, "NO_IO"].ToString());
                dt = _biz.Search_Detail(_flexM[_flexM.Row, "NO_IO"].ToString(), D.GetDecimal(_flexM[_flexM.Row, "NO_IOLINE"]));
          
            } 
            _flexD.BindingAdd(dt, Filter);
            //_flexM.DetailQueryNeed = false; 필요없다함 
        }

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

        private void Set_Line()
        {
            string Multi_No_io_mgmt = string.Empty;

            foreach (DataRow row in _flexM.DataTable.Rows)
                Multi_No_io_mgmt += D.GetString(row["NO_IO_MGMT"]) + "|";

            DataTable dt = _biz.dt_LOT_MGMT(Multi_No_io_mgmt);

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
                    _flexD["FG_IO"] = D.GetString(row["FG_IO"]);
                    _flexD["CD_QTIOTP"] = D.GetString(row["CD_QTIOTP"]);
                    _flexD["NO_SERIAL"] = D.GetString(rowFind["NO_SERIAL"]);
                    _flexD.AddFinished();
                }
            }
        }

        public DataTable dtL
        {
            get { return _dtL; }
        }


        #endregion

        #region ★ 엑셀업로드 클릭
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
                        DataRow[] rowCnt = _flexM.DataTable.Select(" (NO_IO IS NULL OR NO_IO = '" + dr["NO_IO"].ToString() + "') AND  NO_IOLINE = " + dr["NO_IOLINE"].ToString() + " ", "", DataViewRowState.CurrentRows);

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

                        dt엑셀.Columns["NO_IO"].DataType = typeof(string);
                        dt엑셀.Columns["CD_ITEM"].DataType = typeof(string);
                        dt엑셀.Columns["NO_SERIAL"].DataType = typeof(string);

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

                        string msg = "입고번호            입고항번     품목코드     시리얼번호            "; //수정 출고항번추가 
                        검증리스트_품목.AppendLine(msg);

                        msg = "-".PadRight(60, '-');
                        검증리스트_품목.AppendLine(msg);

                        //검증로직 추가  20081230 
                        #region -> 엑셀 Data 검증 ( 품목 / 항번 존재여부 체크 )

                        foreach (DataRow row in dt엑셀.Rows)
                        {
                            if (row["NO_IO"].ToString().Trim() == null || row["NO_IO"].ToString().Trim() == string.Empty || row["NO_IO"].ToString().Trim() == "") { continue; }
                            if (row["CD_ITEM"].ToString().Trim() == null || row["CD_ITEM"].ToString().Trim() == string.Empty || row["CD_ITEM"].ToString().Trim() == "") { continue; }
                            if (row["NO_IOLINE"].ToString().Trim() == null || row["NO_IOLINE"].ToString().Trim() == string.Empty || row["NO_IOLINE"].ToString().Trim() == "") { continue; }

                            string strFilter = " (NO_IO IS NULL OR NO_IO = '" + row["NO_IO"].ToString().Trim() + "') AND NO_IOLINE = " + row["NO_IOLINE"].ToString().Trim() + " AND CD_ITEM = '" + row["CD_ITEM"].ToString().Trim() + "' ";

                            DataRow[] drChk = _dt.Select(strFilter, "", DataViewRowState.CurrentRows);

                            if (drChk.Length > 0)
                                UniqEnable = true;
                            else UniqEnable = false;

                            if (UniqEnable == false)
                            {
                                string NO_IO = row["NO_IO"].ToString().PadRight(20, ' '); //추가 20081230
                                string NO_IOLINE = row["NO_IOLINE"].ToString().PadRight(10, ' '); //추가 20081230
                                string CD_ITEM = row["CD_ITEM"].ToString().PadRight(20, ' ');
                                string NO_SERIAL = row["NO_SERIAL"].ToString().PadRight(20, ' ');
                                //string QT_IO = row["QT_IO"].ToString();

                                string msg2 = NO_IO + " " + NO_IOLINE + " " + CD_ITEM + " " + NO_SERIAL;// +" " + QT_IO;

                                검증리스트_품목.AppendLine(msg2);
                                검증여부 = true;
                            }
                        }

                        if (검증여부)
                        {
                            Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 존재하지안는 (항번/품목)이 존재합니다. \n " +
                            " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_품목.ToString());
                            _flexD.RowFilter = "(NO_IO IS NULL OR NO_IO = '" + _flexM[_flexM.Row, "NO_IO"].ToString() + "') AND " + "NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " "; //라인항번으로 대치
                            _flexD.Redraw = true;
                            return;
                        }

                        #endregion

                        /* *********************************************************************************************** */

                        //Global.MainFrame.ShowMessage("1"); //////////////

                        #region -> 엑셀 Data 검증 ( 품목 중복 체크 )

                        foreach (DataRow row in dt엑셀.Rows)
                        {
                            if (strModule != "PR" && (row["NO_IO"].ToString().Trim() == null || row["NO_IO"].ToString().Trim() == string.Empty || row["NO_IO"].ToString().Trim() == "")) { continue; }
                            if (row["CD_ITEM"].ToString().Trim() == null || row["CD_ITEM"].ToString().Trim() == string.Empty || row["CD_ITEM"].ToString().Trim() == "") { continue; }
                            if (row["NO_IOLINE"].ToString().Trim() == null || row["NO_IOLINE"].ToString().Trim() == string.Empty || row["NO_IOLINE"].ToString().Trim() == "") { continue; }

                            // string strFilter = " CD_ITEM = '" + row["CD_ITEM"].ToString().Trim() + "' AND NO_LOT = '" + row["NO_LOT"].ToString().Trim() + "' ";
                            //입고항번추가
                            string strFilter = " (NO_IO IS NULL OR NO_IO = '" + row["NO_IO"].ToString().Trim() + "') AND NO_IOLINE = " + row["NO_IOLINE"].ToString().Trim() + " AND CD_ITEM = '" + row["CD_ITEM"].ToString().Trim() + "' AND NO_SERIAL = '" + row["NO_SERIAL"].ToString().Trim() + "' ";

                            DataRow[] drChk = dt엑셀.Select(strFilter, "", DataViewRowState.CurrentRows);

                            if (drChk.Length == 1)
                                UniqEnable = true;
                            else UniqEnable = false;

                            if (UniqEnable == true)
                            {
                                NewRowItem = dt엑셀품목중복체크.NewRow();

                                NewRowItem["S"] = "Y";
                                NewRowItem["CD_ITEM"] = row["CD_ITEM"];
                                NewRowItem["NO_IO"] = row["NO_IO"]; //"";
                                NewRowItem["NO_IOLINE"] = row["NO_IOLINE"]; //수정 (추가)
                                //NewRowItem["DT_IO"] = "";
                                NewRowItem["FG_IO"] = "";
                                NewRowItem["CD_QTIOTP"] = "";
                                //NewRowItem["CD_SL"] = "";
                                //NewRowItem["QT_IO"] = Convert.ToDecimal(row["QT_IO"]);
                                //NewRowItem["FG_PS"] = "1";
                                NewRowItem["NO_SERIAL"] = row["NO_SERIAL"];

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

                                if (Global.MainFrame.ServerKeyCommon == "KISERP")
                                    NewRowItem["CD_MNG15"] = Math.Truncate(D.GetDecimal(_flexM[_flexM.Row, "UM"]));
                                else
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
                                string NO_IO = row["NO_IO"].ToString().PadRight(20, ' '); //추가 20081230
                                string NO_IOLINE = row["NO_IOLINE"].ToString().PadRight(10, ' '); //추가 20081230
                                string CD_ITEM = row["CD_ITEM"].ToString().PadRight(20, ' ');
                                string NO_SERIAL = row["NO_SERIAL"].ToString().PadRight(20, ' ');
                                //string QT_IO = row["QT_IO"].ToString();

                                string msg2 = NO_IO + " " + NO_IOLINE + " " + CD_ITEM + " " + NO_SERIAL; // +" " + QT_IO;

                                검증리스트_품목.AppendLine(msg2);
                                검증여부 = true;
                            }
                        }

                        if (검증여부)
                        {
                            Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 중복되는 (항번/품목)과 시리얼번호가 존재합니다. \n " +
                            " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_품목.ToString());
                            _flexD.RowFilter = "(NO_IO IS NULL OR NO_IO = '" + _flexM[_flexM.Row, "NO_IO"].ToString() + "') AND NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " "; //라인항번으로 대치
                            _flexD.Redraw = true;
                            return;

                        }

                        #endregion

                        /* *********************************************************************************************** */

                        #region -> 엑셀 Data 검증 ( 품목 마스터 체크 )


                        //Global.MainFrame.ShowMessage("2"); //////////////

                        DataTable MasterItemDt = _biz.ExcelSearch(MULTI_ITEM, D.GetString(_flexM["CD_PLANT"]), "ITEM");
                        검증여부 = false;


                        foreach (DataRow row in dt엑셀품목중복체크.Rows)
                        {
                            if (strModule != "PR" && (row["NO_IO"].ToString().Trim() == null || row["NO_IO"].ToString().Trim() == string.Empty || row["NO_IO"].ToString().Trim() == "")) { continue; }
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
                                NewRowItem["NO_IO"] = row["NO_IO"].ToString().Trim();  //"";
                                NewRowItem["NO_IOLINE"] = row["NO_IOLINE"].ToString().Trim(); //수정 20081230
                                //NewRowItem["DT_IO"] = "";
                                NewRowItem["FG_IO"] = "";
                                NewRowItem["CD_QTIOTP"] = "";
                                //NewRowItem["CD_SL"] = "";
                                //NewRowItem["QT_IO"] = row["QT_IO"];
                                //NewRowItem["FG_PS"] = row["FG_PS"].ToString();
                                NewRowItem["NO_SERIAL"] = row["NO_SERIAL"].ToString();

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
                                string NO_IO = row["NO_IO"].ToString().PadRight(20, ' ');
                                string NO_IOLINE = row["NO_IOLINE"].ToString().PadRight(10, ' ');
                                string CD_ITEM = row["CD_ITEM"].ToString().PadRight(20, ' ');
                                string NO_SERIAL = row["NO_SERIAL"].ToString().PadRight(20, ' ');
                                //string QT_IO = row["QT_IO"].ToString();
                                string msg2 = NO_IO + " " + NO_IOLINE + " " + CD_ITEM + " " + NO_SERIAL; // +" " + QT_IO;


                                검증리스트_품목.AppendLine(msg2);
                                검증여부 = true;
                            }
                        }

                        if (검증여부)
                        {
                            Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 마스터품목과 불일치 항목들이 존재합니다. \n " +
                            " \n ▼ 버튼을 눌러서 목록을 확인하세요!", 검증리스트_품목.ToString());
                            _flexD.RowFilter = "(NO_IO IS NULL OR NO_IO = '" + _flexM[_flexM.Row, "NO_IO"].ToString() + "') AND NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " "; //라인항번으로 대치
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
                            if (strModule != "PR" && (dt엑셀품목마스터검증.Rows[i]["NO_IO"].ToString().Trim() == null || dt엑셀품목마스터검증.Rows[i]["NO_IO"].ToString().Trim() == string.Empty || dt엑셀품목마스터검증.Rows[i]["NO_IO"].ToString().Trim() == "")) { continue; }
                            if (dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim() == null || dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim() == string.Empty || dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim() == "") { continue; }
                            if (dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim() == null || dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim() == string.Empty || dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim() == "") { continue; }


                            //Global.MainFrame.ShowMessage("5-0-"+ i.ToString()); //////////////

                            //if (Convert.ToDecimal(dt엑셀품목마스터검증.Rows[i]["QT_IO"]) > 0)
                                  수량적합 = true;
                            //else
                            //    수량적합 = false;

                            if (수량적합 == true)
                            {
                                //DataRow dr = _dt.Rows.Find(dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim());
                                DataRow dr = _dt.Rows.Find(dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim());
                                DataRow dr1 = _flexD.DataTable.NewRow();

                                dr1["S"]       = dt엑셀품목마스터검증.Rows[i]["S"].ToString().Trim();
                                dr1["CD_ITEM"] = dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim();
                                //dr1["NO_IO"]   = dr["NO_IO"].ToString().Trim();
                                dr1["NO_IO"]     = dr["NO_IO"].ToString().Trim();
                                //dr1["NO_IOLINE"] = dr["NO_LINE"].ToString();
                                dr1["NO_IOLINE"] = dr["NO_IOLINE"].ToString();

                                //dr1["DT_IO"] = dr["DT_IO"].ToString().Trim();
                                dr1["FG_IO"]     = dr["FG_IO"].ToString().Trim();
                                dr1["CD_QTIOTP"] = dr["CD_QTIOTP"].ToString().Trim();
                                //dr1["CD_SL"] = dr["CD_SL"].ToString().Trim();
                                //dr1["QT_IO"] = dt엑셀품목마스터검증.Rows[i]["QT_IO"];
                                //dr1["FG_PS"] = dt엑셀품목마스터검증.Rows[i]["FG_PS"].ToString().Trim();
                                dr1["NO_SERIAL"] = dt엑셀품목마스터검증.Rows[i]["NO_SERIAL"].ToString().Trim();

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

                                Decimal serl_cnt = D.GetDecimal(_flexD.DataTable.Select("(NO_IO IS NULL OR NO_IO = '" + dr["NO_IO"].ToString() + "') AND NO_IOLINE = " + dr["NO_IOLINE"].ToString() + " ", "", DataViewRowState.CurrentRows).Length);
                                dr["QT_SERIAL_COUNT"] = serl_cnt;
                            }
                            else
                            {
                                //항번추가
                                string NO_IO = dt엑셀품목마스터검증.Rows[i]["NO_IO"].ToString().Trim().PadRight(20, ' ');
                                string NO_IOLINE = dt엑셀품목마스터검증.Rows[i]["NO_IOLINE"].ToString().Trim().PadRight(10, ' ');
                                string CD_ITEM = dt엑셀품목마스터검증.Rows[i]["CD_ITEM"].ToString().Trim().PadRight(10, ' ');
                                string NO_SERIAL = dt엑셀품목마스터검증.Rows[i]["NO_SERIAL"].ToString().Trim().PadRight(10, ' ');
                                //string QT_IO = dt엑셀품목마스터검증.Rows[i]["QT_IO"].ToString().Trim();
                                string msg2 = NO_IO + " " + NO_IOLINE + " " + CD_ITEM + " " + NO_SERIAL; // +" " + QT_IO;

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

                        _flexM.RowFilter = "";
                        //_flexD.RowFilter = "CD_ITEM = '" + _flexM[_flexM.Row, "CD_ITEM"].ToString() + "'";
                        _flexD.RowFilter = "(NO_IO IS NULL OR NO_IO = '" + _flexM[_flexM.Row, "NO_IO"].ToString() + "') AND NO_IOLINE = " + _flexM[_flexM.Row, "NO_IOLINE"].ToString() + " "; //라인항번으로 대치
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

        #region ★ 기타 이벤트

        #region  -> 전체삭제기능 활성화
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

        #region -> 종료 이벤트
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _flexM.SaveUserCache("P_PU_SERL_SUB_R_flexM");
            _flexD.SaveUserCache("P_PU_SERL_SUB_R_flexD");
        }
        #endregion

        #region -> 키 KeyDownEdit 이벤트


        void _flexD_KeyDownEdit(object sender, KeyEditEventArgs e)
        {
            if (_flexD.HelpColName == "NO_SERIAL")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    추가_Click(sender, null);
                }
            }
        }
        #endregion

        #region -> StartEdit

        private void _flexD_Grid_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;

                if (ColName != "S")
                {
                    if (BASIC.GetMAEXC("시리얼입출고도움창_관리항목수정여부설정") == "100")//이형준대리님 요청으로인한 수정사항 관련1값에 IN이나 널값을 넣어주면 입고창에서만 수정되게 하고, OUT이면 출고에서만 수정되게 하는로직
                    {

                        DataRow[] DRows = _dtH.Select("CD_MNG_NM = '" + ColName + "'");
                        if (DRows.Length > 0)
                        {
                            if (D.GetString(DRows[0]["CD_FLAG1"]) == "OUT")
                            {
                                e.Cancel = true;
                                return;
                            }
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion
        
        bool Use서버키
        {
            get { if (Global.MainFrame.ServerKeyCommon.ToUpper() == "MDSTEC") return true; return false; }
        }

         #endregion

        public string SetPageId { set { _pageid = value; } }
        public string SetYnSave { set { _YnSave = value; } }


    }
}
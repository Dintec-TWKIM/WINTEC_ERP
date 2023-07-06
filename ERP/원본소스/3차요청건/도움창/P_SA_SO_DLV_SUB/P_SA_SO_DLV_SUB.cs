using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace sale
{
    public partial class P_SA_SO_DLV_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region ♣ 생성자 & 변수 선언

        P_SA_SO_DLV_SUB_BIZ _biz = new P_SA_SO_DLV_SUB_BIZ();

        private DataSet ds = null;
        private DataTable ReturnDt = null;
        private string partner = "";
        private string str항번컬럼 = "";
        private string _Str사용자정의코드1 = string.Empty; 
        private bool _YnModify = false;
        private string _NO_SO = ""; 
        public P_SA_SO_DLV_SUB()
        {
            try
            {
                InitializeComponent();
                InitGrid();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public P_SA_SO_DLV_SUB(DataTable ReturnDt)
        {
            try
            {
                InitializeComponent();

                InitGrid();

                _flex.Binding = ReturnDt;

            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public P_SA_SO_DLV_SUB(DataTable ReturnDt, string[] str) : this(ReturnDt, str, "") { }

        public P_SA_SO_DLV_SUB(DataTable ReturnDt, string[] str, string str항번컬럼)
        {
            try
            {
                InitializeComponent();

                this.str항번컬럼 = str항번컬럼;

                InitGrid();

                partner = str[0];
                txt_CD_ZIP.Text = str[1];
                txt_ADDR1.Text = str[2];
                txt_ADDR2.Text = str[3];
                txt_NO_TEL_D1.Text = str[4];
                txt_NM_CUST_DLV.Text = str[5];
                txt_NO_TEL_D2.Text = str[6];

                _flex.Binding = ReturnDt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public P_SA_SO_DLV_SUB(DataTable ReturnDt, string[] str, string str항번컬럼, bool YnModify, string NO_SO)
        {
            try
            {
                InitializeComponent();

                this.str항번컬럼 = str항번컬럼;
                _YnModify = YnModify;
                _NO_SO = NO_SO;
                InitGrid();

                partner = str[0];
                txt_CD_ZIP.Text = str[1];
                txt_ADDR1.Text = str[2];
                txt_ADDR2.Text = str[3];
                txt_NO_TEL_D1.Text = str[4];
                txt_NM_CUST_DLV.Text = str[5];
                txt_NO_TEL_D2.Text = str[6];

                if (_YnModify)
                {
                    _flex.Binding = _biz.조회(_NO_SO);
                }
                else _flex.Binding = ReturnDt;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, true);
            _flex.SetCol(str항번컬럼 != "" ? str항번컬럼 : "SEQ_SO", "항번", 40, false);
            _flex.SetCol("CD_ITEM", "품목코드", 100, false, typeof(string));
            _flex.SetCol("NM_ITEM", "품목명", 100, false);
            _flex.SetCol("STND_ITEM", "규격", 70, false);
            _flex.SetCol("UNIT_SO", "단위", 80, 3, false);
            _flex.SetCol("QT_SO", "수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            _flex.SetCol("NM_CUST_DLV", "수취인", 100, 30, true);
            _flex.SetCol("NO_TEL_D1", "전화", 100, 20, true);
            _flex.SetCol("NO_TEL_D2", "이동전화", 100, 20, true);
            _flex.SetCol("CD_ZIP", "우편번호", 90, 6, true, typeof(string));
            _flex.SetCol("ADDR1", "주소1", 150, 300, true);
            _flex.SetCol("ADDR2", "주소2", 150, 200, true);
            _flex.SetCol("TP_DLV", "배송방법", 100, 3, true);
            _flex.SetCol("TP_DLV_DUE", "납품방법", 100, 4, true);
            _flex.SetCol("DC_REQ", "비고", 100, true);
            _flex.SetCol("NO_ORDER", "주문번호", false);
            _flex.SetCol("NM_CUST", "주문자", false);
            _flex.SetCol("NO_TEL1", "주문자전화번호", false);
            _flex.SetCol("NO_TEL2", "주문자이동전화번호", false);
            _flex.SetCol("DLV_TXT_USERDEF1", "사용자정의텍스트1", 100);
            _flex.SetCol("DLV_CD_USERDEF1", "사용자정의코드1", 100);

            _flex.StartEdit += new RowColEventHandler(_flex_StartEdit);

            _flex.SetExceptEditCol(str항번컬럼 != "" ? str항번컬럼 : "SEQ_SO", "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "QT_SO");
            
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SIMMONS")
                _flex.VerifyNotNull = new string[] { "NM_CUST_DLV", "CD_ZIP", "ADDR1" };
            else
                _flex.VerifyNotNull = new string[] { "NM_CUST_DLV", "CD_ZIP", "ADDR1", "TP_DLV" };

            _flex.SettingVersion = "1.0.0.2";

            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            _flex.Cols["DLV_TXT_USERDEF1"].Visible = false;
            _flex.Cols["DLV_CD_USERDEF1"].Visible = false;

            _flex.LoadUserCache("P_SA_SO_DLV_SUB__flex");

            //그리드에서 사용자 정의 도움창 띠우는 걸껄~
            //_flex.HelpClick += new EventHandler(_flex_HelpClick);
            _flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
        }
        #endregion

        #region -> InitPaint : 프리폼 초기화
        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {
            oneGrid1.UseCustomLayout = true;
            bpPanelControl1.IsNecessaryCondition = true;
            bpPanelControl2.IsNecessaryCondition = true;
            bpPanelControl3.IsNecessaryCondition = true;
            bpPanelControl4.IsNecessaryCondition = true;
            bpPanelControl6.IsNecessaryCondition = true;
            bpPanelControl7.IsNecessaryCondition = true;
            bpPanelControl8.IsNecessaryCondition = true;
            oneGrid1.InitCustomLayout();

            // 0: 배송방법(EC_0000002)
            ds = Global.MainFrame.GetComboData("S;EC_0000002", "S;SA_B000056");
            cbo_TP_DLV.DataSource = ds.Tables[0];
            cbo_TP_DLV.DisplayMember = "NAME";
            cbo_TP_DLV.ValueMember = "CODE";

            _flex.SetDataMap("TP_DLV", ds.Tables[0], "CODE", "NAME");
            _flex.SetDataMap("TP_DLV_DUE", ds.Tables[1], "CODE", "NAME");

            //str항번컬럼 이 없으면 엑셀upload를 사용할수가 없다.
            if (str항번컬럼 == "")
            {
                _flex.Cols[str항번컬럼 != "" ? str항번컬럼 : "SEQ_SO"].Visible = false;
                _flex.Cols[str항번컬럼 != "" ? str항번컬럼 : "SEQ_SO"].Width = 0;
                btn엑셀업로드.Visible = false;
            }

            //사용자정의 컬럼 캡션 셋팅
            ColsSetting("DLV_TXT_USERDEF", "SA_B000117", 1, 1);

            사용자정의셋팅();

           if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SIMMONS")
               cbo사용자정의코드1.SelectedValue = _Str사용자정의코드1;
        }

        private void 사용자정의셋팅()
        {
            DataTable dtUserDefine = MA.GetCode("SA_B000118");
            DataRow[] drDLV_CD_USERDEF1 = dtUserDefine.Select("CD_FLAG1 = 'DLV_CD_USERDEF1'");

            if (drDLV_CD_USERDEF1 == null || drDLV_CD_USERDEF1.Length == 0)
                _flex.Cols["DLV_CD_USERDEF1"].Visible = false;
            else
            {
                _flex.Cols["DLV_CD_USERDEF1"].Caption = D.GetString(drDLV_CD_USERDEF1[0]["NAME"]);
                _flex.Cols["DLV_CD_USERDEF1"].Visible = true;
                lbl사용자정의코드1.Visible = cbo사용자정의코드1.Visible = true;
                lbl사용자정의코드1.Text = D.GetString(drDLV_CD_USERDEF1[0]["NAME"]);

                //헤더사용자정의세팅(콤보박스컨트롤)
                SetControl str = new SetControl();
                DataTable dtCode1 = MA.GetCode("SA_B000119", true);
                DataTable dtCodeDtl1 = dtCode1.Clone();
                foreach (DataRow row in dtCode1.Select("CD_FLAG1 = 'DLV_CD_USERDEF1'"))
                    dtCodeDtl1.ImportRow(row);
                str.SetCombobox(cbo사용자정의코드1, dtCodeDtl1);
                _flex.SetDataMap("DLV_CD_USERDEF1", dtCodeDtl1, "CODE", "NAME");
            }
        }
        #endregion

        #endregion

        #region ♣ 그리드 이벤트 / 메소드

        #region -> 그리드 HelpClick 이벤트(_flex_BeforeCodeHelp)

        //private void _flex_HelpClick(object sender, EventArgs e)
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (_flex.Cols[_flex.Col].Name == "CD_ZIP")
                {
                    object dlg = Global.MainFrame.LoadHelpWindow("P_MA_POST", new object[] { Global.MainFrame, "" });

                    if (!_flex.HasNormalRow) return;

                    if (((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
                    {
                        if (dlg is IHelpWindow)
                        {
                            object[] Return = (object[])((IHelpWindow)dlg).ReturnValues;
                            string cd_zip = Return[0].ToString() + Return[1].ToString();
                            string addr1 = Return[2].ToString();
                            string addr2 = Return[3].ToString();

                            _flex["CD_ZIP"] = cd_zip;
                            _flex["ADDR1"] = addr1;
                            _flex["ADDR2"] = addr2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (_YnModify)
                {
                    if (D.GetString(_flex["STA_SO1"]) == "C")
                    {
                        Global.MainFrame.ShowMessage("수주가 진행 혹은 종결 이므로 수정 할 수 없습니다.");
                        e.Cancel = true;
                        return;
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

        #region ♣ 화면 내 버튼클릭 이벤트

        #region -> 검색 버튼 클릭 이벤트(btn_Search_Click)

        private void btn_Search_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = null;

                object[] obj = new object[8];
                obj[0] = Global.MainFrame.LoginInfo.CompanyCode;    //회사
                obj[1] = D.GetString(partner);                                   //거래처코드
                obj[2] = txt_NM_CUST_DLV.Text;
                obj[3] = txt_CD_ZIP.Text.Replace("-", "").Replace("_", "");
                obj[4] = txt_ADDR1.Text;
                obj[5] = txt_ADDR2.Text;
                obj[6] = txt_NO_TEL_D1.Text;
                obj[7] = txt_NO_TEL_D2.Text;

                dt = _biz.GetPartnerSearch(obj);

                if (dt != null && dt.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else if (dt != null && dt.Rows.Count == 1)
                {
                    txt_NM_CUST_DLV.Text = dt.Rows[0]["NM_CUST_DLV"].ToString();
                    txt_CD_ZIP.Text = dt.Rows[0]["CD_ZIP"].ToString();
                    txt_ADDR1.Text = dt.Rows[0]["ADDR1"].ToString();
                    txt_ADDR2.Text = dt.Rows[0]["ADDR2"].ToString();
                    txt_NO_TEL_D1.Text = dt.Rows[0]["NO_TEL_D1"].ToString();
                    txt_NO_TEL_D2.Text = dt.Rows[0]["NO_TEL_D2"].ToString();
                    cbo_TP_DLV.SelectedValue = dt.Rows[0]["TP_DLV"].ToString();
                }
                else if (dt != null && dt.Rows.Count > 1)
                {
                    P_SA_SO_DLV_SUB_SELECT dlg = new P_SA_SO_DLV_SUB_SELECT(dt);

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        txt_NM_CUST_DLV.Text = dlg.returnParams[0];
                        txt_CD_ZIP.Text = dlg.returnParams[1];
                        txt_ADDR1.Text = dlg.returnParams[2];
                        txt_ADDR2.Text = dlg.returnParams[3];
                        txt_NO_TEL_D1.Text = dlg.returnParams[4];
                        txt_NO_TEL_D2.Text = dlg.returnParams[5];
                        cbo_TP_DLV.SelectedValue = dlg.returnParams[6];
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> 일괄적용 버튼클릭 이벤트(btn_Apply_Click)

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (_flex.DataTable == null || _flex.DataTable.Rows.Count == 0) return;
                
                if (_YnModify)
                {
                    DataRow[] row = _flex.DataTable.Select("STA_SO1 = 'C'");
                    if (row.Length > 0)
                    {
                        Global.MainFrame.ShowMessage("수주가 진행 혹은 종결 건이 존재하여 적용 할 수 없습니다.");
                        return;
                    }
                }

                foreach (DataRow dr in _flex.DataTable.Rows)
                {
                    dr["NM_CUST_DLV"] = txt_NM_CUST_DLV.Text;
                    dr["NO_TEL_D1"] = txt_NO_TEL_D1.Text;
                    dr["NO_TEL_D2"] = txt_NO_TEL_D2.Text;
                    dr["CD_ZIP"] = txt_CD_ZIP.Text.Replace("-", "");
                    dr["ADDR1"] = txt_ADDR1.Text;
                    dr["ADDR2"] = txt_ADDR2.Text;
                    dr["TP_DLV"] = cbo_TP_DLV.SelectedValue == null ? "" : cbo_TP_DLV.SelectedValue.ToString();
                    dr["DC_REQ"] = txt_DC_REQ.Text;
                    dr["DLV_CD_USERDEF1"] = D.GetString(cbo사용자정의코드1.SelectedValue);
                }

                Global.MainFrame.ShowMessage("일괄적용 되었습니다");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> 확인버튼 클릭 이벤트(btn_Ok_Click)

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (_YnModify)
                {
                    if (!_flex.HasNormalRow) return;

                    DataTable dt = _flex.GetChanges();

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        Global.MainFrame.ShowMessage("변경된 내용이 없습니다.");
                        return;
                    }

                    if (!_flex.Verify()) return;

                    bool result = _biz.Save(dt);

                    if (!result) return;

                    _flex.AcceptChanges();

                    Global.MainFrame.ShowMessage("저장이 완료되었습니다.");

                }
                else
                {
                    int cnt = 0;

                    foreach (DataRow dr in _flex.DataTable.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted) continue;
                        if (Global.MainFrame.ServerKeyCommon.ToUpper() == "SIMMONS")
                        {
                            if (dr["NM_CUST_DLV"].ToString() == "" || dr["CD_ZIP"].ToString() == "" || dr["ADDR1"].ToString() == "")
                                cnt++;
                        }
                        else
                        {
                            if (dr["NM_CUST_DLV"].ToString() == "" || dr["CD_ZIP"].ToString() == "" || dr["ADDR1"].ToString() == "" || dr["TP_DLV"].ToString() == "")
                                cnt++;
                        }
                    }

                    if (cnt != 0)
                    {
                        Global.MainFrame.ShowMessage("배송정보는 필수입력항목입니다. " + Environment.NewLine + " 배송정보 입력을 확인하세요.");
                        return;
                    }
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        #endregion

        #region -> btn엑셀업로드_Click

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (_YnModify)
                {
                    DataRow[] row = _flex.DataTable.Select("STA_SO1 = 'C'");
                    if (row.Length > 0)
                        Global.MainFrame.ShowMessage("수주가 진행 혹은 종결 건이 존재하여 업로드 할 수 없습니다.");
                    return;
                }
                openFileDialogUploadExcel.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (openFileDialogUploadExcel.ShowDialog() == DialogResult.OK)
                {
                    Duzon.Common.Util.Excel excel = null;

                    excel = new Duzon.Common.Util.Excel();

                    DataTable dt = excel.StartLoadExcel(openFileDialogUploadExcel.FileName, 0, 2);

                    StringBuilder sbErrorList = new StringBuilder();

                    #region -> 엑셀에서 누락된 컬럼이 있는지 체크한다.

                    if (!dt.Columns.Contains("SEQ_SO"))
                    {
                        sbErrorList.AppendLine("컬럼명 [SEQ_SO] 이 엑셀에 존재하지 않습니다. 컬럼명 [SEQ_SO]는 항번을 의미합니다.");
                    }

                    if (!dt.Columns.Contains("NM_CUST_DLV"))
                    {
                        sbErrorList.AppendLine("컬럼명 [NM_CUST_DLV] 이 엑셀에 존재하지 않습니다. 컬럼명 [NM_CUST_DLV]는 수취인을 의미합니다.");
                    }

                    if (!dt.Columns.Contains("NO_TEL_D1"))
                    {
                        sbErrorList.AppendLine("컬럼명 [NO_TEL_D1] 이 엑셀에 존재하지 않습니다. 컬럼명 [NO_TEL_D1]는 전화를 의미합니다.");
                    }

                    if (!dt.Columns.Contains("NO_TEL_D2"))
                    {
                        sbErrorList.AppendLine("컬럼명 [NO_TEL_D2] 이 엑셀에 존재하지 않습니다. 컬럼명 [NO_TEL_D2]는 이동전화를 의미합니다.");
                    }

                    if (!dt.Columns.Contains("CD_ZIP"))
                    {
                        sbErrorList.AppendLine("컬럼명 [CD_ZIP] 이 엑셀에 존재하지 않습니다. 컬럼명 [CD_ZIP]는 우편번호를 의미합니다.");
                    }

                    if (!dt.Columns.Contains("ADDR1"))
                    {
                        sbErrorList.AppendLine("컬럼명 [ADDR1] 이 엑셀에 존재하지 않습니다. 컬럼명 [ADDR1]는 주소1을 의미합니다.");
                    }

                    if (!dt.Columns.Contains("ADDR2"))
                    {
                        sbErrorList.AppendLine("컬럼명 [ADDR2] 이 엑셀에 존재하지 않습니다. 컬럼명 [ADDR2]는 주소2을 의미합니다.");
                    }

                    if (!dt.Columns.Contains("TP_DLV"))
                    {
                        sbErrorList.AppendLine("컬럼명 [TP_DLV] 이 엑셀에 존재하지 않습니다. 컬럼명 [TP_DLV]는 배송방법을 의미합니다.");
                    }

                    if (!dt.Columns.Contains("TP_DLV_DUE"))
                    {
                        sbErrorList.AppendLine("컬럼명 [TP_DLV_DUE] 이 엑셀에 존재하지 않습니다. 컬럼명 [TP_DLV_DUE]는 납품방법을 의미합니다.");
                    }

                    if (!dt.Columns.Contains("DC_REQ"))
                    {
                        sbErrorList.AppendLine("컬럼명 [DC_REQ] 이 엑셀에 존재하지 않습니다. 컬럼명 [DC_REQ]는 비고를 의미합니다.");
                    }

                    if (sbErrorList.Length > 0)
                    {
                        Global.MainFrame.ShowDetailMessage("존재하지 않거나 잘못된 컬럼 목록입니다.", sbErrorList.ToString());
                        return;
                    }

                    #endregion

                    #region -> EXCEL UPLOAD 한 DataTable을 새로운 DataTable 에 넣는다.(명확히 컬럼타입을 지정하기 위해서)

                    //엑셀 테이블을 만든다.(그 이유는 명확히 컬럼타입을 지정하기 위해서)
                    DataTable dtExcel = new DataTable();
                    dtExcel.Columns.Add("SEQ_SO", typeof(decimal));
                    dtExcel.Columns.Add("NM_CUST_DLV", typeof(string));
                    dtExcel.Columns.Add("NO_TEL_D1", typeof(string));
                    dtExcel.Columns.Add("NO_TEL_D2", typeof(string));
                    dtExcel.Columns.Add("CD_ZIP", typeof(string));
                    dtExcel.Columns.Add("ADDR1", typeof(string));
                    dtExcel.Columns.Add("ADDR2", typeof(string));
                    dtExcel.Columns.Add("TP_DLV", typeof(string));
                    dtExcel.Columns.Add("TP_DLV_DUE", typeof(string));
                    dtExcel.Columns.Add("DC_REQ", typeof(string));

                    //만든 엑셀 테이블에 엑셀에서 받은 테입블의 내용을 넣는다.
                    foreach (DataRow dr in dt.Rows)
                    {
                        DataRow drExcel = dtExcel.NewRow();
                        drExcel["SEQ_SO"] = _flex.CDecimal(dr["SEQ_SO"]);
                        drExcel["NM_CUST_DLV"] = dr["NM_CUST_DLV"].ToString();
                        drExcel["NO_TEL_D1"] = dr["NO_TEL_D1"].ToString();
                        drExcel["NO_TEL_D2"] = dr["NO_TEL_D2"].ToString();
                        drExcel["CD_ZIP"] = dr["CD_ZIP"].ToString();
                        drExcel["ADDR1"] = dr["ADDR1"].ToString();
                        drExcel["ADDR2"] = dr["ADDR2"].ToString();
                        drExcel["TP_DLV"] = dr["TP_DLV"].ToString();
                        drExcel["TP_DLV_DUE"] = dr["TP_DLV_DUE"].ToString();
                        drExcel["DC_REQ"] = dr["DC_REQ"].ToString();
                        dtExcel.Rows.Add(drExcel);
                    }

                    #endregion

                    #region -> 배송방법과 납품방법이 잘못된 부분을 찾아낸다.

                    DataTable dt배송방법 = ds.Tables[0].Copy();
                    DataTable dt납품방법 = ds.Tables[1].Copy();

                    dt배송방법.PrimaryKey = new DataColumn[] { dt배송방법.Columns["CODE"] };
                    dt납품방법.PrimaryKey = new DataColumn[] { dt납품방법.Columns["CODE"] };

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        DataRow dr배송방법 = dt배송방법.Rows.Find(dr["TP_DLV"].ToString());
                        if (dr배송방법 == null)
                            sbErrorList.AppendLine("컬럼명 [TP_DLV] 에 잘못된 코드값이 들어갔습니다. 컬럼명 [TP_DLV]는 배송방법을 의미합니다. 잘못된 코드값은 '" + dr["TP_DLV"].ToString() + "' 입니다.");

                        DataRow dr납품방법 = dt납품방법.Rows.Find(dr["TP_DLV_DUE"].ToString());
                        if (dr납품방법 == null)
                            sbErrorList.AppendLine("컬럼명 [TP_DLV_DUE] 에 잘못된 코드값이 들어갔습니다. 컬럼명 [TP_DLV_DUE]는 납품방법을 의미합니다. 잘못된 코드값은 '" + dr["TP_DLV_DUE"].ToString() + "' 입니다.");
                    }

                    if (sbErrorList.Length > 0)
                    {
                        Global.MainFrame.ShowDetailMessage("배송방법 또는 납품방법의 코드가 잘못된 목록입니다.", sbErrorList.ToString());
                        return;
                    }

                    #endregion

                    #region -> 매치가 되지않는 항번이 있는 부분을 찾아낸다.

                    DataTable dtFelx = _flex.DataTable.Copy();

                    dtFelx.PrimaryKey = new DataColumn[] { dtFelx.Columns[str항번컬럼] };

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        DataRow drSEQ = dtFelx.Rows.Find(dr["SEQ_SO"]);
                        if (drSEQ == null)
                            sbErrorList.AppendLine("컬럼명 [SEQ_SO] 에 존재하지 않는 항번이 있습니다. 컬럼명 [SEQ_SO]는 항번을 의미합니다. 잘못된 항번은 '" + dr["SEQ_SO"].ToString() + "' 입니다.");
                    }

                    if (sbErrorList.Length > 0)
                    {
                        Global.MainFrame.ShowDetailMessage("그리드에 매치가 되지 않는 항번이 들어있는 목록입니다.", sbErrorList.ToString());
                        return;
                    }

                    #endregion

                    DataColumn[] dcPrimary = _flex.DataTable.PrimaryKey;        //primary key 임시저장

                    _flex.DataTable.PrimaryKey = new DataColumn[] { _flex.DataTable.Columns[str항번컬럼] };

                    _flex.Redraw = false;

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        DataRow drSEQ = _flex.DataTable.Rows.Find(dr["SEQ_SO"]);
                        drSEQ["NM_CUST_DLV"] = dr["NM_CUST_DLV"].ToString();
                        drSEQ["NO_TEL_D1"] = dr["NO_TEL_D1"].ToString();
                        drSEQ["NO_TEL_D2"] = dr["NO_TEL_D2"].ToString();
                        drSEQ["CD_ZIP"] = dr["CD_ZIP"].ToString();
                        drSEQ["ADDR1"] = dr["ADDR1"].ToString();
                        drSEQ["ADDR2"] = dr["ADDR2"].ToString();
                        drSEQ["TP_DLV"] = dr["TP_DLV"].ToString();
                        if (drSEQ.Table.Columns.Contains("TP_DLV_DUE"))
                            drSEQ["TP_DLV_DUE"] = dr["TP_DLV_DUE"].ToString();
                        if (drSEQ.Table.Columns.Contains("DC_REQ"))
                            drSEQ["DC_REQ"] = dr["DC_REQ"].ToString();
                    }

                    _flex.DataTable.PrimaryKey = dcPrimary;     //primary key 원상복귀
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                _flex.Redraw = true;
            }
        }

        #endregion

        #region -> 우편번호 도움창(btn_Zip_Click)

        private void btn_Zip_Click(object sender, EventArgs e)
        {
            try
            {
                object dlg = Global.MainFrame.LoadHelpWindow("P_MA_POST", new object[] { Global.MainFrame, "" });

                if (_flex.DataTable == null || _flex.DataTable.Rows.Count == 0) return;

                if (((Duzon.Common.Forms.CommonDialog)dlg).ShowDialog() == DialogResult.OK)
                {
                    if (dlg is IHelpWindow)
                    {
                        object[] Return = (object[])((IHelpWindow)dlg).ReturnValues;
                        string cd_zip = Return[0].ToString() + Return[1].ToString();
                        string addr1 = Return[2].ToString();
                        string addr2 = Return[3].ToString();

                        txt_CD_ZIP.Text = cd_zip.Replace("-", "");
                        txt_ADDR1.Text = addr1;
                        txt_ADDR2.Text = addr2;
                        txt_ADDR2.Focus();
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

        #region ♣ 기타 이벤트 / 메소드
        #region -> 사용자정의 컬럼 캡션 셋팅

        private void ColsSetting(string colName, string cdField, int startIdx, int endIdx)
        {
            for (int i = startIdx; i <= endIdx; i++)
            {
                _flex.Cols[colName + D.GetString(i)].Visible = false;
            }
            DataTable dt = MA.GetCode(cdField);
            for (int i = startIdx; (i <= dt.Rows.Count && i <= endIdx); i++)
            {
                string Name = D.GetString(dt.Rows[i - 1]["NAME"]);
                _flex.Cols[colName + D.GetString(i)].Caption = Name;
                _flex.Cols[colName + D.GetString(i)].Visible = true;
            }
        }
        #endregion

        #region -> ErrorCheck
        public bool ErrorCheck()
        {
            return true;
        }
        #endregion

        #endregion

        #region ♣ 결과값 리턴해줄 속성값
        public DataTable ReturnTable
        {
            get
            {
                ReturnDt = _flex.DataTable;
                return ReturnDt;
            }
        } 
        #endregion  

        #region ♣ OnClosed
        
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _flex.SaveUserCache("P_SA_SO_DLV_SUB__flex");
        }

        #endregion

        #region ♣ 속성
        public bool YnModify { set { _YnModify = value; } }
        public string NO_SO { set { _NO_SO = value; } }
        
        public string Str사용자정의코드1 { set { _Str사용자정의코드1 = value; } } 
        #endregion
    }
}

using System;
using System.Data;

using Duzon.Common.Forms;
using Dintec;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;



namespace cz
{
    public partial class P_CZ_HR_CONTRACT_REG : PageBase
    {

        #region 생성자
        P_CZ_HR_CONTRACT_REG_BIZ _biz = new P_CZ_HR_CONTRACT_REG_BIZ();

        public string CD_COMPANY = string.Empty;
        public string NM_COMPANY = string.Empty;
        public string NO_EMP = string.Empty;
        public string NM_KOR = string.Empty;
        public string NM_CC = string.Empty;
        public string NM_DUTY_RANK = string.Empty;
        public string ST_STAT = string.Empty;
        public string OBJECT_NAME = string.Empty;
        public string NM_CONTRACT = string.Empty;
        public string CONTRACT_EMP = string.Empty;
        public string CONTRACT_DEPT = string.Empty;
        public string SEQ = string.Empty;
        public string CONTRACT_DEPT_NM = string.Empty;
        public string CONTRACT_EMP_NM = string.Empty;
        public string CONTRACT_DEPT_NM2 = string.Empty;
        public string CONTRACT_DEPT2 = string.Empty;
        public string NM_TITLE = string.Empty;
        public string FILE_PATH_CODE = string.Empty;
        public string FILE_PATH_CODE2 = string.Empty;
        public bool companyCheck = false;


        public bool RESET = false;

        #endregion 생성자


        #region 초기화
        public P_CZ_HR_CONTRACT_REG()
        {
            StartUp.Certify(this);
            CD_COMPANY = Global.MainFrame.LoginInfo.CompanyCode;
            NM_COMPANY = Global.MainFrame.LoginInfo.CompanyName;
            NO_EMP = Global.MainFrame.LoginInfo.UserID;
            NM_KOR = Global.MainFrame.LoginInfo.UserName;
            NM_CC = Global.MainFrame.LoginInfo.DeptName;

            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
            this.InitControl();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH };

            _flexH.BeginSetting(1, 1, false);

            _flexH.SetCol("NM_TITLE", "제목", false);
            _flexH.SetCol("NM_CONTRACT", "계약명", 250);
            _flexH.SetCol("NM_CONTRACT_COMPANY1", "계약주체1", 100);
            _flexH.SetCol("NM_CONTRACT_COMPANY2", "계약주체2", 100);
            _flexH.SetCol("DT_FROM", "계약기간", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("DT_TO", "계약기간", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("YN_AUTO", "자동연장여부", 100, false, CheckTypeEnum.Y_N);
            _flexH.SetCol("CONTRACT_DEPT", "원본보관부서코드", false);
            _flexH.SetCol("CONTRACT_DEPT2", "계약부서코드", false);
            _flexH.SetCol("CONTRACT_EMP", "담당자코드", false);
            _flexH.SetCol("CONTRACT_DEPT_NM2", "계약부서", 80);
            _flexH.SetCol("CONTRACT_EMP_NM", "계약담당자", 80);
            _flexH.SetCol("CONTRACT_DEPT_NM", "원본보관부서", 100);
            _flexH.SetCol("NO_DOCU", "문서번호", false);
            _flexH.SetCol("DC_RMK", "특이사항", 350);
            _flexH.SetCol("FILE_PATH_MNG", "첨부파일", false);
            _flexH.SetCol("FILE_PATH_CODE", "첨부파일코드", false);
            _flexH.SetCol("CD_COMPANY", "회사코드", false);
            _flexH.SetCol("NM_COMPANY", "회사명", false);
            _flexH.SetCol("NM_DEPT", "부서", false);
            _flexH.SetCol("NM_DUTY_RANK", "직급", false);
            _flexH.SetCol("NM_KOR", "등록자", 80, false);
            _flexH.SetCol("DT_REG", "등록일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("ST_STAT", "결재", false);
            _flexH.SetCol("NM_SYSDEF", "결재", 80, false);

            _flexH.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_DEPT"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_DUTY_RANK"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_KOR"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["DT_REG"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_CONTRACT_COMPANY1"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["NM_CONTRACT_COMPANY2"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["DT_FROM"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["DT_TO"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["CONTRACT_DEPT_NM"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["CONTRACT_DEPT_NM2"].TextAlign = TextAlignEnum.CenterCenter;
            _flexH.Cols["CONTRACT_EMP_NM"].TextAlign = TextAlignEnum.CenterCenter;

            _flexH.Cols["NM_SYSDEF"].TextAlign = TextAlignEnum.CenterCenter;

            this._flexH.SetOneGridBinding(new object[] { this.tbx계약명 }, this.oneL);

            this._flexH.SetBindningCheckBox(this.chk자동연장, "Y", "N");

            _flexH.SettingVersion = "0.0.0.2";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }


        private void InitEvent()
        {
            btn파일보기.Click += new EventHandler(btn파일보기_Click);
            //btn삭제.Click += new EventHandler(btn초기화_Click);
            btn삭제.Click += new EventHandler(btn삭제_Click);
            btn전자결재.Click += new EventHandler(btn전자결재_Click);
            btn초기화s.Click += new EventHandler(btn초기화s_Click);
            //cbx원본보관부서.TextChanged += new EventHandler(cbx원본보관부서_TextChanged);

            _flexH.Click += new EventHandler(_flexH_Click);
            dtp계약기간.CalendarDateChanged += new Duzon.Common.Controls.CalendarDateChangedEventHandler(dtp계약기간_CalendarDateChanged);
        }

        private void InitControl()
        {
            dtp계약일자S.StartDateToString = Util.GetToday(-365);
            dtp계약일자S.EndDateToString = Util.GetToday(+365);

            dtp계약기간.StartDateToString = Util.GetToday(-30);
            dtp계약기간.EndDateToString = Util.GetToday(+365);

            //cbx원본보관부서.Enabled = true;

            if (CD_COMPANY.Equals("K200") || CD_COMPANY.Equals("S100") || CD_COMPANY.Equals("TEST"))
            {
                txt원본보관부서.Visible = true;
                txt계약부서.Visible = true;
                txt계약부서s.Visible = true;
                cbx계약부서.Visible = false;

                txt계약부서.Text = NM_COMPANY;
                txt계약부서s.Text = NM_COMPANY;

                companyCheck = false;
            }
            else
            {
                txt원본보관부서.Visible = true;
                txt계약부서.Visible = false;
                txt계약부서s.Visible = false;
                cbx계약부서.Visible = true;

                companyCheck = true;
            }
        }

        #endregion 초기화


        #region 조회
        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSearch()) return;

                if (companyCheck)
                {
                    this._flexH.Binding = this._biz.Search(new object[] { //복수선택을 select에 사용 할때는 항상 변수 크기에 유의할 것!
                                                                      NO_EMP,
                                                                      dtp계약일자S.StartDateToString,
                                                                      dtp계약일자S.EndDateToString,
                                                                      txt계약명s.Text,
                                                                      cbx계약부서s.QueryWhereIn_Pipe,
                                                                      cbx계약담당자s.QueryWhereIn_Pipe
                                                                      //cbx원본보관부서s.QueryWhereIn_Pipe
                                                                      //,cbx회사.QueryWhereIn_Pipe,
                                                                       });
                }
                else
                {
                    this._flexH.Binding = this._biz.Search2(new object[] { //복수선택을 select에 사용 할때는 항상 변수 크기에 유의할 것!
                                                                      NO_EMP,
                                                                      dtp계약일자S.StartDateToString,
                                                                      dtp계약일자S.EndDateToString,
                                                                      txt계약명s.Text,
                                                                      txt계약부서s.Text,
                                                                      cbx계약담당자s.QueryWhereIn_Pipe
                                                                      //cbx원본보관부서s.QueryWhereIn_Pipe
                                                                      //,cbx회사.QueryWhereIn_Pipe,
                                                                       });
                }
                    
                

                if (!_flexH.HasNormalRow)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    SEQ = _flexH["SEQ"].ToString();
                    FILE_PATH_CODE = _flexH["FILE_PATH_CODE"].ToString();
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 조회


        #region 추가
        protected override bool BeforeAdd()
        {
            return base.BeforeAdd();
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this._flexH.Rows.Add();
                this._flexH.Row = _flexH.Rows.Count - 1;

                this._flexH["CD_COMPANY"] = CD_COMPANY;
                this._flexH["NO_EMP"] = NO_EMP;
                this._flexH["DT_REG"] = Util.GetToday();
                this._flexH["NM_KOR"] = NM_KOR;

                //cbx원본보관부서.Enabled = true;
                //cbx원본보관부서s.Enabled = true;

                if (companyCheck)
                {
                    //cbx원본보관부서.Enabled = true;

                    //if (!cbx원본보관부서.Enabled)
                    //{
                    //    this._flexH["CONTRACT_DEPT"] = cbx원본보관부서s.CodeValues[0].ToString();
                    //    this._flexH["CONTRACT_DEPT_NM"] = cbx원본보관부서s.CodeNames[0].ToString();
                    //}

                    if (!cbx계약부서.Enabled)
                    {
                        this._flexH["CONTRACT_DEPT2"] = cbx계약부서s.CodeValues[0].ToString();
                        this._flexH["CONTRACT_DEPT_NM2"] = cbx계약부서s.CodeNames[0].ToString();
                    }

                    if (!cbx계약담당자.Enabled)
                    {
                        this._flexH["CONTRACT_EMP"] = cbx계약담당자s.CodeValues[0].ToString();
                        this._flexH["CONTRACT_EMP_NM"] = cbx계약담당자s.CodeNames[0].ToString();
                    }
                }
                else
                {
                    //if (!cbx원본보관부서.Enabled)
                    //{
                    //    this._flexH["CONTRACT_DEPT"] = cbx원본보관부서s.CodeValues[0].ToString();
                    //    this._flexH["CONTRACT_DEPT_NM"] = cbx원본보관부서s.CodeNames[0].ToString();
                    //}

                    this._flexH["CONTRACT_DEPT2"] = txt계약부서s.Text;
                    this._flexH["CONTRACT_DEPT_NM2"] = txt계약부서s.Text;

                    if (!cbx계약담당자.Enabled)
                    {
                        this._flexH["CONTRACT_EMP"] = cbx계약담당자s.CodeValues[0].ToString();
                        this._flexH["CONTRACT_EMP_NM"] = cbx계약담당자s.CodeNames[0].ToString();
                    }
                }

                this._flexH.AddFinished();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion 추가


        #region 저장

        protected override bool BeforeSave()
        {
            if (!base.BeforeSave()) return false;

            return true;
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave()) return;

                if (this.MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            this._flexH["CONTRACT_DEPT"] = txt원본보관부서.Text;

            if (!base.SaveData() || !this.Verify())
                return false;

            if (this._flexH.IsDataChanged == false) return false;

            if (_flexH.HasNormalRow)
            {
                if (string.IsNullOrEmpty(this._flexH["NM_CONTRACT"].ToString()))
                {
                    Util.ShowMessage("계약명을 입력하세요.");
                    return false;
                }

                if (string.IsNullOrEmpty(this._flexH["DT_FROM"].ToString()) || string.IsNullOrEmpty(this._flexH["DT_TO"].ToString()))
                {
                    Util.ShowMessage("계약기간을 입력하세요.");
                    return false;
                }

                if (string.IsNullOrEmpty(this._flexH["NM_CONTRACT_COMPANY1"].ToString()) || string.IsNullOrEmpty(this._flexH["NM_CONTRACT_COMPANY2"].ToString()))
                {
                    Util.ShowMessage("계약주체를 입력하세요.");
                    return false;
                }


                if (string.IsNullOrEmpty(this._flexH["CONTRACT_DEPT_NM2"].ToString()))
                {
                    Util.ShowMessage("계약부서를 입력하세요.");
                    return false;
                }

                if (string.IsNullOrEmpty(this._flexH["CONTRACT_EMP_NM"].ToString()))
                {
                    Util.ShowMessage("계약담당자를 입력하세요.");
                    return false;
                }


                if (string.IsNullOrEmpty(this._flexH["CONTRACT_DEPT_NM"].ToString()))
                {
                    Util.ShowMessage("원본보관부서를 입력하세요");
                    return false;
                }

                if (string.IsNullOrEmpty(_flexH["YN_AUTO"].ToString()))
                {
                    _flexH["YN_AUTO"] = "N";
                }

                NM_KOR = _flexH["NM_KOR"].ToString();
                NM_CONTRACT = _flexH["NM_CONTRACT"].ToString();

                OBJECT_NAME = NM_KOR + "_" + NM_CONTRACT + "_" + Util.GetToday().Substring(0, 6) + "_계약서";

                string query = "SELECT * FROM CZ_HR_CONTRACT_REG WHERE CONTRACT_DEPT = '" + txt원본보관부서.Text + "' AND NM_CONTRACT = '" + tbx계약명.Text + "' AND CONTRACT_EMP = '" + cbx계약담당자.CodeValue + "'";
                //string query = "SELECT * FROM CZ_HR_CONTRACT_REG WHERE NM_CONTRACT = '" + tbx계약명.Text + "' AND CONTRACT_EMP = '" + cbx계약담당자.CodeValue + "'";
                DataTable dtDB = DBMgr.GetDataTable(query);
                DataTable dtTB = _flexH.DataTable;

                this._flexH["NM_TITLE"] = OBJECT_NAME;


                if (ST_STAT.Equals("2") || string.IsNullOrEmpty(ST_STAT))
                {
                    string file_path_code_ = _flexH["FILE_PATH_CODE"].ToString();
                }
                else
                {
                    Util.ShowMessage("결재 진행 중이므로 수정할 수 없습니다.");
                    return false;
                }
            }

            DataTable dt = this._flexH.GetChanges();
            this._biz.Save(dt);
            this._flexH.AcceptChanges();

            OnToolBarSearchButtonClicked(null, null);
            //btn초기화_Click(null, null);
            return true;
        }

        #endregion 저장


        public void btn삭제_Click(object sender, EventArgs e)
        {
            try
            {
                string ST_STAT = _flexH["ST_STAT"].ToString();

                if (ST_STAT == "0") { ShowMessage("결재 진행중인 문서입니다!"); return; }
               if (ST_STAT == "1") { ShowMessage("결재 완료된 문서입니다!"); return; }

                //if (!this.BeforeDelete() || !this._flexH.HasNormalRow) return;
                //if (!this._flexH.HasNormalRow)
                //    return;

                //this._flexH.Rows.Remove(this._flexH.Row);

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #region 삭제
        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flexH.HasNormalRow) return;

                this._flexH.Rows.Remove(this._flexH.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion 삭제


        #region 버튼

        #region 첨부파일
        private void btn파일보기_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;

            try
            {
                FILE_PATH_CODE = _flexH["FILE_PATH_CODE"].ToString();

                if (!string.IsNullOrEmpty(FILE_PATH_CODE))
                {
                    if (!string.IsNullOrEmpty(tbx계약명.Text))
                    {
                        if (!this._flexH.HasNormalRow)
                        {
                            this.ShowMessage(공통메세지.선택된자료가없습니다);
                        }
                        else
                        {
                            //    if (string.IsNullOrEmpty(NM_CONTRACT) || string.IsNullOrEmpty(CONTRACT_DEPT) || string.IsNullOrEmpty(CONTRACT_EMP))
                            //    {
                            //        Util.ShowMessage("필수 항목을 입력해주세요.");
                            //    }

                            if (string.IsNullOrEmpty(this._flexH["NM_CONTRACT"].ToString()) || string.IsNullOrEmpty(this._flexH["CONTRACT_DEPT"].ToString()) || string.IsNullOrEmpty(this._flexH["CONTRACT_EMP"].ToString()))
                            {
                                Util.ShowMessage("필수 항목을 입력해주세요.");
                            }
                            else
                            {
                                P_CZ_MA_FILE_SUB m_dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "P_CZ_HR_CONTRACT_REG", FILE_PATH_CODE, "P_CZ_HR_CONTRACT_REG");
                                m_dlg.ShowDialog(this);
                                _flexH_Click(null,null);
                            }
                        }
                    }
                    else
                    {
                        Util.ShowMessage("계약명을 입력하세요.");
                    }
                }
                else
                {
                    Util.ShowMessage("저장 후 첨부파일을 등록해주세요.");
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 첨부파일

        #region 모드변경
        public void btn초기화_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cbx원본보관부서.Enabled)
                //    cbx원본보관부서.Clear();

                if (cbx계약부서.Enabled)
                    cbx계약부서.Clear();


                if (cbx계약담당자.Enabled)
                    cbx계약담당자.Clear();
            
                    RESET = true;                       //초기화 시 연결된 ROW 변경 여부
                    tbx계약명.Text = "";
                    tbx계약주체1.Text = "";
                    tbx계약주체2.Text = "";
                    tbx비고.Text = "";
                    chk자동연장.Checked = false;
                    tbx첨부파일.Text = "";
                    //btn파일보기.Enabled = false;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 모드변경

        #region 초기화
        public void btn초기화s_Click(object sender, EventArgs e)
        {
            //if (cbx원본보관부서.Enabled)
            //    cbx원본보관부서.Clear();

            if (cbx계약부서.Enabled)
                cbx계약부서.Clear();


            if (cbx계약담당자.Enabled)
                cbx계약담당자.Clear();

            RESET = true;                       //초기화 시 연결된 ROW 변경 여부
            tbx계약명.Text = "";
            tbx계약주체1.Text = "";
            tbx계약주체2.Text = "";
            tbx비고.Text = "";
            chk자동연장.Checked = false;
            tbx첨부파일.Text = "";
            btn파일보기.Enabled = false;
        }
        #endregion 초기화

        #region 날짜동기화
        public void dtp계약기간_CalendarDateChanged(object sender, EventArgs e)
        {
            try
            {
                this._flexH["DT_FROM"] = dtp계약기간.StartDateToString;
                this._flexH["DT_TO"] = dtp계약기간.EndDateToString;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 날짜동기화

        #region 전자결재


        public void btn전자결재_Click(object sender, EventArgs e)
        {
            try
            {

                NM_CONTRACT = _flexH["NM_CONTRACT"].ToString();
                SEQ = _flexH["SEQ"].ToString();

                if (!string.IsNullOrEmpty(tbx첨부파일.Text))
                {
                    if (!string.IsNullOrEmpty(tbx첨부파일.Text))
                    {
                        // 결재 상태 체크
                        string query = @"
SELECT
    A.NO_DOCU, B.ST_STAT
FROM	  CZ_HR_CONTRACT_REG	AS A
LEFT JOIN FI_GWDOCU			AS B ON A.NO_DOCU = B.NO_DOCU
WHERE 1 = 1
    AND A.SEQ = '" + SEQ + "'";

                        DataTable dt = DBMgr.GetDataTable(query);
                        string NO_DOCU = dt.Rows[0]["NO_DOCU"].ToString();
                        string ST_STAT = dt.Rows[0]["ST_STAT"].ToString();

                        if (ST_STAT == "0") { ShowMessage("결재 진행중인 문서입니다!"); return; }
                        if (ST_STAT == "1") { ShowMessage("결재 완료된 문서입니다!"); return; }

                        // html 만들기
                        string html = @"
<div class='header'>
  ※ 계약서 등록
</div>
<table width='100%'>
  <tr>
  <th width='30%' style='text-align: center;'>계약명</th>
  <td colspan='2'  style='text-align: left; padding-left:10px'>" + NM_CONTRACT + @"</td>
</tr>";

                        html += @"
  <tr>
    <th width='30%' style='text-align:center'>계약주체1</th>
    <td colspan='2' style='text-align:left; padding-left:10px' >" + tbx계약주체1.Text + @"</td>
  </tr>

<tr>
    <th width='30%' style='text-align:center'>계약주체2</th>
    <td colspan='2' style='text-align:left; padding-left:10px'>" + tbx계약주체2.Text + @"</td>
  </tr>

<tr>
    <th style='text-align:center' width='30%'>계약기간</th>
    <td style='text-align:left; padding-left:10px'>" + dtp계약기간.StartDateToString.Substring(0, 4) + "년" + dtp계약기간.StartDateToString.Substring(4, 2) + "월" + dtp계약기간.StartDateToString.Substring(6, 2) + "일  ~  " + dtp계약기간.EndDateToString.Substring(0, 4) + "년" + dtp계약기간.EndDateToString.Substring(4, 2) + "월" + dtp계약기간.EndDateToString.Substring(6, 2) + "일" + @"</td>
  </tr>


<tr>
    <th style='text-align:center' width='30%'>자동연장</th>
    <td colspan='2' style='text-align:left; padding-left:10px'>" + _flexH["YN_AUTO"].ToString() + @"</td>
  </tr>


<tr>
    <th style='text-align:center' width='30%'>원본보관부서</th>
    <td colspan='2' style='text-align:left; padding-left:10px'>" + txt원본보관부서.Text + @"</td>
  </tr>


<tr>
    <th style='text-align:center' width='30%'>계약부서</th>
    <td colspan='2' style='text-align:left; padding-left:10px'>" + cbx계약부서.CodeName + @"</td>
  </tr>


<tr>
    <th style='text-align:center' width='30%'>담당자</th>
    <td colspan='2' style='text-align:left; padding-left:10px'>" + cbx계약담당자.CodeName + @"</td>
  </tr>
";

                        DataTable dtL = _flexH.DataTable;
                        html += @"
  
  <tr>
    <td colspan='2' class='rmk' style='padding-left:10px' height = '50px'>" + tbx비고.Text + @" </td>
  </tr>
</table>";

                        // html 업데이트 및 전자결재 팝업			
                        query = @"
UPDATE CZ_HR_CONTRACT_REG SET
    NO_DOCU = '@NO_DOCU'
WHERE 1 = 1
    AND SEQ = '" + SEQ + "'";


                        GroupWare.Save(NM_CONTRACT, html, NO_DOCU, 1013, query, true);
                    }
                    else
                    {
                        Util.ShowMessage("관련 첨부파일을 등록하세요");
                    }
                }
                else
                {
                    Util.ShowMessage("첨부파일을 등록하세요.");
                }

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion 전자결재

        #region click
        public void _flexH_Click(object sender, EventArgs e)
        {
            try
            {
                dtp계약기간.StartDateToString = this._flexH["DT_FROM"].ToString();
                dtp계약기간.EndDateToString = this._flexH["DT_TO"].ToString();
                string companyStr = this._flexH["CD_COMPANY"].ToString();
                //SEQ = _flexH["SEQ"].ToString();
                FILE_PATH_CODE = _flexH["FILE_PATH_CODE"].ToString();

                FILE_PATH_CODE2 = this._biz.SearchFileInfo(companyStr, FILE_PATH_CODE);

                if (!string.IsNullOrEmpty(FILE_PATH_CODE2))
                {
                    tbx첨부파일.Text = FILE_PATH_CODE2;
                }
                else
                {
                    tbx첨부파일.Text = string.Empty;
                }


            }
            catch (Exception ex)
            {

            }
        }
        #endregion click

        #region 
        public void cbx원본보관부서_TextChanged(object sender, EventArgs e)
        {
            //if (!cbx원본보관부서.Enabled)
            //{
            //    CONTRACT_DEPT = cbx원본보관부서.GetCodeValue();
            //    CONTRACT_DEPT_NM = cbx원본보관부서.GetCodeName();
            //}

            if (!cbx계약부서.Enabled)
            {
                CONTRACT_DEPT2 = cbx계약부서.GetCodeValue();
                CONTRACT_DEPT_NM2 = cbx계약부서.GetCodeName();
            }

            if (!cbx계약담당자.Enabled)
            {
                CONTRACT_EMP_NM = cbx계약담당자.GetCodeName();
                CONTRACT_EMP = cbx계약담당자.GetCodeValue();
            }

        }
        #endregion

        #endregion 버튼
    }
}

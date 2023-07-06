using System;
using System.Data;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.Common.BpControls;

namespace cz
{
    public partial class P_CZ_SA_GIM_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region 전역변수 & 생성자
        private string _fileCode;
        private bool _반품여부;
        
        public P_CZ_SA_GIM_REG_SUB(string 회사, string 회사명, string 협조전번호)
        {
            InitializeComponent();

            this.ctx회사.CodeValue = 회사;
            this.ctx회사.CodeName = 회사명;

            this.txt협조전번호.Text = 협조전번호;
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitEvent();
        }

        protected override void InitPaint()
        {
            DataSet ds;

            try
            {
                base.InitPaint();

                this.dtp선적일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                this.dtp물류업무완료일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
                this.dtp포장업무완료일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);

                ds = Global.MainFrame.GetComboData(new string[] { "S;CZ_SA00006", "S;CZ_SA00020" });

                this.cbo물류MainCategory.DataSource = ds.Tables[0];
                this.cbo물류MainCategory.DisplayMember = "NAME";
                this.cbo물류MainCategory.ValueMember = "CODE";

                this.cbo포장MainCategory.DataSource = ds.Tables[1];
                this.cbo포장MainCategory.DisplayMember = "NAME";
                this.cbo포장MainCategory.ValueMember = "CODE";

                this.cbo협조전유형.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { Global.MainFrame.DD("물류"), Global.MainFrame.DD("포장") }, true);
                this.cbo협조전유형.DisplayMember = "NAME";
                this.cbo협조전유형.ValueMember = "CODE";

                if (!string.IsNullOrEmpty(this.txt협조전번호.Text))
                    this.btn조회_Click(null, null);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }   
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn등록.Click += new EventHandler(this.btn등록_Click);
            this.btn닫기.Click += new EventHandler(this.btn닫기_Click);
            this.btn인수증.Click += new EventHandler(this.btn인수증_Click);
            this.btn기타파일.Click += new EventHandler(this.btn기타파일_Click);

            this.txt협조전번호.KeyDown += new KeyEventHandler(this.txt협조전번호_KeyDown);
            this.ctx회사.QueryAfter += new BpQueryHandler(this.ctx회사_QueryAfter);
        }

        private void InitControl()
        {
            this.txt출고번호.Text = string.Empty;
            this.txt호선명.Text = string.Empty;

            this.dtp선적일자.Text = string.Empty;

            this.cbo수주번호.DataSource = null;

            this.cbo협조전유형.SelectedValue = string.Empty;

            this.dtp물류업무완료일자.Enabled = true;
            this.ctx물류담당자.ReadOnly = ReadOnly.None;

            this.dtp포장업무완료일자.Enabled = true;
            this.ctx포장담당자.ReadOnly = ReadOnly.None;

            this.cbo물류MainCategory.SelectedValue = string.Empty;
            this.cbo물류SubCategory.DataSource = null;

            this.cbo포장MainCategory.SelectedValue = string.Empty;
            this.cbo포장SubCategory.DataSource = null;

            this.dtp물류업무완료일자.Text = string.Empty;
            this.ctx물류담당자.CodeValue = string.Empty;
            this.ctx물류담당자.CodeName = string.Empty;

            this.dtp포장업무완료일자.Text = string.Empty;
            this.ctx포장담당자.CodeValue = string.Empty;
            this.ctx포장담당자.CodeName = string.Empty;

            this.txt인수증.Text = string.Empty;
            this.txt기타파일.Text = string.Empty;

            this.btn인수증.Enabled = false;
            this.btn기타파일.Enabled = false;
            this.btn등록.Enabled = false;
            this._fileCode = string.Empty;
        }
        #endregion

        #region 버튼 이벤트
        private void txt협조전번호_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.btn조회_Click(sender, e);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ctx회사_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                this.txt협조전번호.Text = this.협조전Prefix();
                this.InitControl();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txt협조전번호.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl협조전번호.Text });
                }
                else if (string.IsNullOrEmpty(this.ctx회사.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl회사.Text });
                }
                else
                {
                    if (this.협조전존재여부확인(true, false) == true)
                    {
                        this.cbo협조전유형.SelectedValue = "001";
                        this.협조전정보설정();
                    }
                    else if (this.협조전존재여부확인(false, false) == true)
                    {
                        this.cbo협조전유형.SelectedValue = "002";
                        this.협조전정보설정();
                    }
                    else
                    {
                        this.InitControl();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn인수증_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._fileCode)) 
                    return;
                else if (string.IsNullOrEmpty(this.ctx회사.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl회사.Text });
                    return;
                }

                P_CZ_MA_FILE_SUB m_dlg = new P_CZ_MA_FILE_SUB(this.ctx회사.CodeValue, "SA", "P_CZ_SA_GIM_REG", this._fileCode, "P_CZ_SA_GIM_REG" + "/" + this.ctx회사.CodeValue + "/" + this.dtp의뢰일자.Text.Substring(0, 4));

                if (m_dlg.ShowDialog(this) == DialogResult.Cancel) return;

                this.인수증정보갱신();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn기타파일_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._fileCode))
                    return;
                else if (string.IsNullOrEmpty(this.ctx회사.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl회사.Text });
                    return;
                }

                P_CZ_MA_FILE_SUB m_dlg = new P_CZ_MA_FILE_SUB(this.ctx회사.CodeValue, "SA", "P_CZ_SA_GIM_REG", this._fileCode + "_ETC", "P_CZ_SA_GIM_REG" + "/" + this.ctx회사.CodeValue + "/" + this.dtp의뢰일자.Text.Substring(0, 4));

                if (m_dlg.ShowDialog(this) == DialogResult.Cancel) return;

                this.기타파일정보갱신();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn등록_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                if (string.IsNullOrEmpty(this.txt협조전번호.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl협조전번호.Text });
                }
                else if (string.IsNullOrEmpty(this.ctx회사.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl회사.Text });
                }
                else if (this.매출등록여부확인())
                {
                    Global.MainFrame.ShowMessage("매출등록된 건은 수정할 수 없습니다.");
                }
				else if (this.dtp선적일자.Value > Global.MainFrame.GetDateTimeToday())
				{
					Global.MainFrame.ShowMessage("선적일자는 현재일자를 초과해서 입력 할 수 없습니다.");
				}
                else if (this.dtp선적일자.Value <= Global.MainFrame.GetDateTimeToday().AddYears(-1))
				{
                    Global.MainFrame.ShowMessage("선적일자는 현재일자 기준으로 1년 이전 일자를 입력할 수 없습니다.");
                }
                else if (Global.MainFrame.LoginInfo.CompanyCode != "S100" &&
                         !Global.MainFrame.LoginInfo.GroupID.Contains("ADM") &&
                         DBHelper.GetDataTable(string.Format(@"SELECT OL.NO_IO 
                                                               FROM MM_QTIO OL 
                                                               WHERE OL.CD_COMPANY = '{0}' 
                                                               AND OL.NO_IO = '{1}' 
                                                               AND OL.FG_TRANS = '001'", Global.MainFrame.LoginInfo.CompanyCode, this.txt출고번호.Text)).Rows.Count == 0 &&
                         !string.IsNullOrEmpty(this.dtp선적일자.Text) &&
                         !this._반품여부 &&
                         ((Convert.ToDateTime(this.dtp선적일자.Text.Substring(0, 4) + "-" + this.dtp선적일자.Text.ToString().Substring(4, 2) + "-01") <= Convert.ToDateTime(DateTime.Now.AddMonths(-2).ToString("yyyy-MM") + "-01")) ||
                          (DateTime.Now.Day >= 16 && (Convert.ToDateTime(this.dtp선적일자.Text.Substring(0, 4) + "-" + this.dtp선적일자.Text.Substring(4, 2) + "-01") <= Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-01")))))
                {
                    Global.MainFrame.ShowMessage("마감 완료건으로 정산담당자에게 연락 바랍니다.");
                }
                else
                {
                    if (this.cbo협조전유형.SelectedValue.Equals("001"))
                    {
                        if (this.협조전존재여부확인(true, true) == false)
                        {
                            query = @"INSERT INTO CZ_SA_GIRH_WORK_DETAIL
                                      (CD_COMPANY, NO_GIR, ID_INSERT, DTS_INSERT)
                                      VALUES" +
                                     "('" + this.ctx회사.CodeValue + "', '" + this.txt협조전번호.Text + "', '" + Global.MainFrame.LoginInfo.EmployeeNo + "', NEOE.SF_SYSDATE(GETDATE()))";

                            Global.MainFrame.ExecuteScalar(query);
                        }

                        query = "UPDATE CZ_SA_GIRH_WORK_DETAIL " +
                                "SET DT_LOADING = '" + this.dtp선적일자.Text + "', " +
                                "DT_WORKING = '" + this.dtp물류업무완료일자.Text + "', " +
                                "NO_WORK_EMP = '" + this.ctx물류담당자.CodeValue + "', " +
                                "DT_PACKING = '" + this.dtp포장업무완료일자.Text + "', " +
                                "NO_PACK_EMP = '" + this.ctx포장담당자.CodeValue + "' " +
                                "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "' " +
                                "AND NO_GIR = '" + this.txt협조전번호.Text + "'";

                        Global.MainFrame.ExecuteScalar(query);
                    }
                    else
                    {
                        if (this.협조전존재여부확인(false, true) == false)
                        {
                            query = @"INSERT INTO CZ_SA_GIRH_PACK_DETAIL
                                      (CD_COMPANY, NO_GIR, ID_INSERT, DTS_INSERT)
                                      VALUES" +
                                     "('" + this.ctx회사.CodeValue + "', '" + this.txt협조전번호.Text + "', '" + Global.MainFrame.LoginInfo.EmployeeNo + "', NEOE.SF_SYSDATE(GETDATE()))";

                            Global.MainFrame.ExecuteScalar(query);
                        }

                        query = "UPDATE CZ_SA_GIRH_PACK_DETAIL " +
                                "SET DT_PACKING = '" + this.dtp포장업무완료일자.Text + "', " +
                                "NO_PACK_EMP = '" + this.ctx포장담당자.CodeValue + "' " +
                                "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "' " +
                                "AND NO_GIR = '" + this.txt협조전번호.Text + "'";

                        Global.MainFrame.ExecuteScalar(query);
                    }

                    this.InitControl();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn닫기_Click(object sender, EventArgs e)
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
        #endregion

        #region 메소드
        private void 인수증정보갱신()
        {
            string query;

            try
            {
                query = @"SELECT MAX(FILE_NAME) + '(' + CONVERT(VARCHAR, COUNT(1)) + ')' FILE_PATH_MNG
                          FROM MA_FILEINFO WITH(NOLOCK)" + Environment.NewLine +
                         "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "' " + Environment.NewLine +
                        @"AND CD_MODULE = 'SA'
                          AND ID_MENU = 'P_CZ_SA_GIM_REG'
                          AND CD_FILE = '" + this._fileCode + "'";

                this.txt인수증.Text = D.GetString(Global.MainFrame.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 기타파일정보갱신()
        {
            string query;

            try
            {
                query = @"SELECT MAX(FILE_NAME) + '(' + CONVERT(VARCHAR, COUNT(1)) + ')' FILE_PATH_MNG
                          FROM MA_FILEINFO WITH(NOLOCK)" + Environment.NewLine +
                         "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "' " + Environment.NewLine +
                        @"AND CD_MODULE = 'SA'
                          AND ID_MENU = 'P_CZ_SA_GIM_REG'
                          AND CD_FILE = '" + this._fileCode + "_ETC" + "'";

                this.txt기타파일.Text = D.GetString(Global.MainFrame.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Set물류SubCategory(string mainCategory)
        {
            if (string.IsNullOrEmpty(mainCategory) == false)
                this.cbo물류SubCategory.DataSource = Global.MainFrame.GetComboDataCombine("S;" + MA.GetCode("CZ_SA00006").Select("CODE = '" + mainCategory + "'")[0].ItemArray[3].ToString());
            else
                this.cbo물류SubCategory.DataSource = null;

            this.cbo물류SubCategory.DisplayMember = "NAME";
            this.cbo물류SubCategory.ValueMember = "CODE";
        }

        private void Set포장SubCategory(string 포장Category)
        {
            if (string.IsNullOrEmpty(포장Category) == false)
                this.cbo포장SubCategory.DataSource = Global.MainFrame.GetComboDataCombine("S;" + MA.GetCode("CZ_SA00020").Select("CODE = '" + 포장Category + "'")[0].ItemArray[3].ToString());
            else
                this.cbo포장SubCategory.DataSource = null;

            this.cbo포장SubCategory.DisplayMember = "NAME";
            this.cbo포장SubCategory.ValueMember = "CODE";
        }

        private bool 협조전존재여부확인(bool 물류업무여부, bool isDetail)
        {
            string query;

            try
            {
                if (물류업무여부 == true)
                {
                    if (isDetail == true)
                        query = "SELECT COUNT(*) FROM CZ_SA_GIRH_WORK_DETAIL WITH(NOLOCK) ";
                    else
                        query = "SELECT COUNT(*) FROM SA_GIRH WITH(NOLOCK) ";
                }
                else
                {
                    if (isDetail == true)
                        query = "SELECT COUNT(*) FROM CZ_SA_GIRH_PACK_DETAIL WITH(NOLOCK) ";
                    else
                        query = "SELECT COUNT(*) FROM CZ_SA_GIRH_PACK WITH(NOLOCK) ";
                }

                query += "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "' " +
                         "AND NO_GIR = '" + this.txt협조전번호.Text + "'";

                if (D.GetDecimal(Global.MainFrame.ExecuteScalar(query)) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private bool 매출등록여부확인()
        {
            string query;

            try
            {
                query = @"SELECT 1 
                          FROM MM_QTIO WITH(NOLOCK) " + 
                         "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "' " +
                         "AND NO_ISURCV = '" + this.txt협조전번호.Text + "' " +
                        @"AND FG_PS = 2
                          AND QT_CLS > 0";

                if (D.GetDecimal(Global.MainFrame.ExecuteScalar(query)) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return false;
        }

        private void 협조전정보설정()
        {
            DataTable dt;
            string query;

            try
            {
                if (this.cbo협조전유형.SelectedValue.Equals("001"))
                {
                    #region 물류업무협조전
                    query = @"SELECT MQ.NO_IO,
                                     MH.NM_VESSEL,
                                     WD.CD_MAIN_CATEGORY, 
                                     WD.CD_SUB_CATEGORY, 
                                     WD.DT_LOADING, 
                                     SUBSTRING(WD.DT_WORKING, 1, 8) AS DT_WORKING,
                                     SUBSTRING(WD.DT_PACKING, 1, 8) AS DT_PACKING,
                                     WD.NO_WORK_EMP,
                                     WD.NO_PACK_EMP,
                                     ME.NM_KOR AS NM_WORK_EMP,
                                     ME1.NM_KOR AS NM_PACK_EMP,
                                     MQ.FG_TRANS,
                                     GH.DT_GIR,
                                     ISNULL(OH.YN_RETURN, 'N') AS YN_RETURN
                              FROM MM_QTIO MQ WITH(NOLOCK)
                              LEFT JOIN MM_QTIOH OH WITH(NOLOCK) ON OH.CD_COMPANY = MQ.CD_COMPANY AND OH.NO_IO = MQ.NO_IO
                              LEFT JOIN CZ_SA_GIRH_WORK_DETAIL WD WITH(NOLOCK) ON WD.CD_COMPANY = MQ.CD_COMPANY AND WD.NO_GIR = MQ.NO_ISURCV
                              LEFT JOIN SA_GIRH GH WITH(NOLOCK) ON GH.CD_COMPANY = WD.CD_COMPANY AND GH.NO_GIR = WD.NO_GIR
                              LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = WD.NO_IMO
                              LEFT JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = WD.CD_COMPANY AND ME.NO_EMP = WD.NO_WORK_EMP
                              LEFT JOIN MA_EMP ME1 WITH(NOLOCK) ON ME1.CD_COMPANY = WD.CD_COMPANY AND ME1.NO_EMP = WD.NO_PACK_EMP" + Environment.NewLine +
                             "WHERE MQ.CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
                             "AND MQ.FG_PS = '2'" + Environment.NewLine +
                             "AND MQ.NO_ISURCV = '" + this.txt협조전번호.Text + "'";
                    #endregion
                }
                else
                {
                    #region 포장업무협조전
                    query = @"SELECT MH.NM_VESSEL,
                                     PD.CD_PACK_CATEGORY,
                                     PD.CD_SUB_CATEGORY,
                                     SUBSTRING(PD.DT_PACKING, 1, 8) AS DT_PACKING,
                                     PD.NO_PACK_EMP,
                                     ME.NM_KOR AS NM_PACK_EMP,
                                     GH.DT_GIR
                              FROM CZ_SA_GIRH_PACK_DETAIL PD WITH(NOLOCK)
                              LEFT JOIN CZ_SA_GIRH_PACK GH WITH(NOLOCK) ON GH.CD_COMPANY = PD.CD_COMPANY AND GH.NO_GIR = PD.NO_GIR
                              LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = PD.NO_IMO
                              LEFT JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = PD.CD_COMPANY AND ME.NO_EMP = PD.NO_PACK_EMP" + Environment.NewLine +
                             "WHERE PD.CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
                             "AND PD.NO_GIR = '" + this.txt협조전번호.Text + "'";
                    #endregion
                }

                dt = Global.MainFrame.FillDataTable(query);

                if (dt.Rows.Count > 0)
                {
                    if (this.cbo협조전유형.SelectedValue.Equals("001"))
                    {
                        #region 물류업무협조전
                        this._반품여부 = (dt.Rows[0]["YN_RETURN"].ToString() == "Y" ? true : false);

                        this.dtp물류업무완료일자.Enabled = true;
                        this.ctx물류담당자.ReadOnly = ReadOnly.None;
                        this.dtp포장업무완료일자.Enabled = true;
                        this.ctx포장담당자.ReadOnly = ReadOnly.None;

                        this.txt출고번호.Text = dt.Rows[0]["NO_IO"].ToString();
                        this.dtp의뢰일자.Text = dt.Rows[0]["DT_GIR"].ToString();
                        this.dtp선적일자.Text = dt.Rows[0]["DT_LOADING"].ToString();

                        this.cbo물류MainCategory.SelectedValue = dt.Rows[0]["CD_MAIN_CATEGORY"].ToString();
                        this.Set물류SubCategory(this.cbo물류MainCategory.SelectedValue.ToString());
                        this.cbo물류SubCategory.SelectedValue = dt.Rows[0]["CD_SUB_CATEGORY"].ToString();

                        this.cbo포장MainCategory.SelectedValue = string.Empty;
                        this.cbo포장SubCategory.DataSource = null;

                        this.dtp물류업무완료일자.Text = dt.Rows[0]["DT_WORKING"].ToString();
                        this.ctx물류담당자.CodeValue = dt.Rows[0]["NO_WORK_EMP"].ToString();
                        this.ctx물류담당자.CodeName = dt.Rows[0]["NM_WORK_EMP"].ToString();

                        this.dtp포장업무완료일자.Text = dt.Rows[0]["DT_PACKING"].ToString();
                        this.ctx포장담당자.CodeValue = dt.Rows[0]["NO_PACK_EMP"].ToString();
                        this.ctx포장담당자.CodeName = dt.Rows[0]["NM_PACK_EMP"].ToString();
                        #endregion
                    }
                    else
                    {
                        #region 포장업무협조전
                        this._반품여부 = false;

                        this.cbo협조전유형.SelectedValue = "002";

                        this.dtp포장업무완료일자.Enabled = true;
                        this.ctx포장담당자.ReadOnly = ReadOnly.None;
                        this.dtp물류업무완료일자.Enabled = false;
                        this.ctx물류담당자.ReadOnly = ReadOnly.TotalReadOnly;

                        this.dtp의뢰일자.Text = dt.Rows[0]["DT_GIR"].ToString();
                        this.dtp선적일자.Enabled = false;

                        this.txt출고번호.Text = string.Empty;
                        this.dtp선적일자.Text = string.Empty;

                        this.cbo물류MainCategory.SelectedValue = string.Empty;
                        this.cbo물류SubCategory.DataSource = null;

                        this.cbo포장MainCategory.SelectedValue = dt.Rows[0]["CD_PACK_CATEGORY"].ToString();
                        this.Set포장SubCategory(this.cbo포장MainCategory.SelectedValue.ToString());
                        this.cbo포장SubCategory.SelectedValue = dt.Rows[0]["CD_SUB_CATEGORY"].ToString();

                        this.dtp물류업무완료일자.Text = string.Empty;
                        this.ctx물류담당자.CodeValue = string.Empty;
                        this.ctx물류담당자.CodeName = string.Empty;

                        this.dtp포장업무완료일자.Text = dt.Rows[0]["DT_PACKING"].ToString();
                        this.ctx포장담당자.CodeValue = dt.Rows[0]["NO_PACK_EMP"].ToString();
                        this.ctx포장담당자.CodeName = dt.Rows[0]["NM_PACK_EMP"].ToString();
                        #endregion
                    }

                    this.txt호선명.Text = dt.Rows[0]["NM_VESSEL"].ToString();

                    this.수주번호설정();

                    this.btn인수증.Enabled = true;
                    this.btn기타파일.Enabled = true;
                    this.btn등록.Enabled = true;

                    this._fileCode = D.GetString(this.txt협조전번호.Text + "_" + this.ctx회사.CodeValue); //파일 PK설정
                    this.인수증정보갱신();
                    this.기타파일정보갱신();
                }
                else
                    this.InitControl();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 수주번호설정()
        {
            DataTable dt;
            string query;

            try
            {
                if (this.cbo협조전유형.SelectedValue.Equals("001"))
                {
                    query = @"SELECT DISTINCT NO_SO AS CODE,
                                     NO_SO AS NAME
                              FROM SA_GIRL WITH(NOLOCK)
                              WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
                             "AND NO_GIR = '" + this.txt협조전번호.Text + "'";
                }
                else
                {
                    query = @"SELECT DISTINCT NO_SO AS CODE,
                                     NO_SO AS NAME
                              FROM CZ_SA_GIRL_PACK WITH(NOLOCK)
                              WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
                             "AND NO_GIR = '" + this.txt협조전번호.Text + "'";
                }

                dt = Global.MainFrame.FillDataTable(query);

                this.cbo수주번호.DataSource = dt;
                this.cbo수주번호.DisplayMember = "NAME";
                this.cbo수주번호.ValueMember = "CODE";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private string 협조전Prefix()
        {
            string query;
            
            try
            {
                query = @"SELECT CD_CTRL 
                          FROM MA_DOCUCTRL WITH(NOLOCK)
                          WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
                        @"AND CD_MODULE = 'SA'
                          AND CD_CLASS = '03'";

                return D.GetString(DBHelper.ExecuteScalar(query)) + Global.MainFrame.GetStringToday.Substring(2, 4);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return string.Empty;
        }
        #endregion
    }
}
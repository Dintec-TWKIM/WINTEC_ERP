using System;
using System.Data;
using System.Text;
using System.Web;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;

namespace cz
{
    public partial class P_CZ_SA_MEETING_MEMO_MNG : PageBase
    {
        #region 전역변수 및 초기화
        P_CZ_SA_MEETING_MEMO_MNG_BIZ _biz = new P_CZ_SA_MEETING_MEMO_MNG_BIZ();
        P_CZ_SA_MEETING_MEMO_MNG_GW _gw = new P_CZ_SA_MEETING_MEMO_MNG_GW();
        int _추가순번;

        public P_CZ_SA_MEETING_MEMO_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        public P_CZ_SA_MEETING_MEMO_MNG(string 미팅번호)
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.txt미팅번호.Text = 미팅번호;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex미팅메모, this._flex참석자내부, this._flex참석자외부 };
            this._flex미팅메모.DetailGrids = new FlexGrid[] { this._flex참석자내부, this._flex참석자외부 };

            #region 미팅메모
            this._flex미팅메모.BeginSetting(1, 1, false);

            this._flex미팅메모.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex미팅메모.SetCol("NM_GW_STAT", "결재상태", 80);
            this._flex미팅메모.SetCol("ID_GW_DOCU", "문서번호", false);
            this._flex미팅메모.SetCol("NO_MEETING", "미팅번호", 80);
            this._flex미팅메모.SetCol("NM_INSERT", "작성자", 80);
            this._flex미팅메모.SetCol("LN_PARTNER", "거래처명", 100);
            this._flex미팅메모.SetCol("DT_MEETING", "미팅일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex미팅메모.SetCol("DC_LOCATION", "장소", 100);
            this._flex미팅메모.SetCol("DC_SUBJECT", "주제", 100);
            this._flex미팅메모.SetCol("DC_PURPOSE", "목적", 100);
            this._flex미팅메모.SetCol("DC_MEETING", "미팅내용", 100);
            
            this._flex미팅메모.SetOneGridBinding(null, new IUParentControl[] { this.pnl기본정보, this.pnl미팅내용 });
            this._flex미팅메모.ExtendLastCol = true;

            this._flex미팅메모.SetDummyColumn("S");

            this._flex미팅메모.SettingVersion = "0.0.0.1";
            this._flex미팅메모.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 참석자(외부)
            this._flex참석자외부.BeginSetting(1, 1, false);

            this._flex참석자외부.SetCol("NO_ATTENDEE", "번호", false);
            this._flex참석자외부.SetCol("NM_ATTENDEE", "이름", 100, true);
            this._flex참석자외부.SetCol("NM_DEPT", "부서", 80);
            this._flex참석자외부.SetCol("NM_DUTY_RESP", "직책", 80);
            
            this._flex참석자외부.SetCodeHelpCol("NM_ATTENDEE", "H_CZ_MA_PARTNERPTR_SUB", ShowHelpEnum.Always, new string[] { "NO_ATTENDEE", "NM_ATTENDEE" }, new string[] { "SEQ", "NM_PTR" });
            this._flex참석자외부.VerifyPrimaryKey = new string[] { "NO_ATTENDEE" };

            this._flex참석자외부.ExtendLastCol = true;

            this._flex참석자외부.SettingVersion = "0.0.0.1";
            this._flex참석자외부.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 참석자(내부)
            this._flex참석자내부.BeginSetting(1, 1, false);

            this._flex참석자내부.SetCol("NM_COMPANY", "회사", 80);
            this._flex참석자내부.SetCol("NO_ATTENDEE", "사번", 80);
            this._flex참석자내부.SetCol("NM_ATTENDEE", "이름", 100, true);
            this._flex참석자내부.SetCol("NM_DEPT", "부서", 80);
            this._flex참석자내부.SetCol("NM_DUTY_RANK", "직위", 80);

            this._flex참석자내부.SetCodeHelpCol("NM_ATTENDEE", "H_CZ_MA_CUSTOMIZE_SUB", ShowHelpEnum.Always, new string[] { "CD_COMPANY_ATTENDEE", "NM_COMPANY", "NO_ATTENDEE", "NM_ATTENDEE", "NM_DEPT", "NM_DUTY_RANK" }, new string[] { "CD_COMPANY", "NM_COMPANY", "NO_EMP", "NM_KOR", "NM_DEPT", "NM_DUTY_RANK" });
            this._flex참석자내부.VerifyPrimaryKey = new string[] { "NO_ATTENDEE" };

            this._flex참석자내부.ExtendLastCol = true;

            this._flex참석자내부.SettingVersion = "0.0.0.1";
            this._flex참석자내부.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp일자S.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp일자S.EndDateToString = Global.MainFrame.GetStringToday;

            this.ctx작성자.CodeValue = Global.MainFrame.LoginInfo.UserID;
            this.ctx작성자.CodeName = Global.MainFrame.LoginInfo.UserName;

            this.split미팅메모.SplitterDistance = 769;
            this.split참석자.SplitterDistance = 423;

            this.InitControl(false);

            if (!string.IsNullOrEmpty(this.txt미팅번호.Text))
                this.OnToolBarSearchButtonClicked(null, null);
        }

        private void InitEvent()
        {
            this.btn전자결재.Click += new EventHandler(this.btn전자결재_Click);
            this.btn문서보기.Click += new EventHandler(this.btn문서보기_Click);

            this.btn참석자외부추가.Click += new EventHandler(this.btn참석자추가_Click);
            this.btn참석자외부삭제.Click += new EventHandler(this.btn참석자삭제_Click);
            this.btn참석자내부추가.Click += new EventHandler(this.btn참석자추가_Click);
            this.btn참석자내부삭제.Click += new EventHandler(this.btn참석자삭제_Click);

            this._flex미팅메모.AfterRowChange += new RangeEventHandler(this._flex미팅메모_AfterRowChange);
            this._flex참석자외부.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex참석자외부_BeforeCodeHelp);
            this._flex참석자내부.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex참석자내부_BeforeCodeHelp);
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this._flex미팅메모.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                             this.dtp일자S.StartDateToString,
                                                                             this.dtp일자S.EndDateToString,
                                                                             this.ctx거래처S.CodeValue,
                                                                             this.txt미팅번호.Text,
                                                                             this.ctx작성자.CodeValue });

                if (!this._flex미팅메모.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            DataRow newRow;

            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!BeforeAdd()) return;

                newRow = this._flex미팅메모.DataTable.NewRow();

                newRow["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                newRow["NO_MEETING"] = this._추가순번.ToString();
                newRow["DT_MEETING"] = Global.MainFrame.GetStringToday;
                newRow["DC_TIME"] = Global.MainFrame.GetDateTimeToday().ToLongTimeString();
                newRow["DC_MEETING"] = "전자결재 가이드 라인 (결재 상신 후 삭제 요망) ========================================================";
                newRow["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                this._flex미팅메모.DataTable.Rows.Add(newRow);

                this._추가순번++;
                this._flex미팅메모.Row = this._flex미팅메모.Rows.Count - 1;

                this.dtp미팅일자.Text = D.GetString(newRow["DT_MEETING"]);
                this.txt시간.Text = D.GetString(newRow["DC_TIME"]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            string 결재상태, query;
            DataTable dt;

            try
            {
                if (this._flex미팅메모["ID_INSERT"].ToString() != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("본인이 작성한 건만 삭제 가능 합니다.");
                    return false;
                }

                결재상태 = D.GetString(this._flex미팅메모["CD_GW_STAT"]);

                if (결재상태 == "0" || 결재상태 == "1")
                {
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", D.GetString(this._flex미팅메모["NM_GW_STAT"]));
                    return false;
                }

                query = @"SELECT 1 
                          FROM CZ_SA_BUSINESS_TRIP_MEETING BT
                          WHERE BT.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND BT.NO_MEETING = '" + this._flex미팅메모["NO_MEETING"].ToString() + "'";

                dt = DBHelper.GetDataTable(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.ShowMessage("출장보고서에 연동된 항목 입니다.\n출장보고서에서 해당 미팅 메모를 삭제하고 삭제 하시기 바랍니다.");
                    return false;
                }

                if (!Util.CheckPW()) return false;

                return base.BeforeDelete();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex미팅메모.HasNormalRow) return;

                this._flex미팅메모.Rows.Remove(this._flex미팅메모.Row);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                    ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            DataRow[] dataRowArray;
            string 미팅번호;
            bool isRefesh = false;

            try
            {
                if (!base.SaveData() || !base.Verify()) return false;

                if (this._flex미팅메모.IsDataChanged == false &&
                    this._flex참석자외부.IsDataChanged == false &&
                    this._flex참석자내부.IsDataChanged == false) return false;

                foreach (DataRow dr in this._flex미팅메모.DataTable.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        isRefesh = true;

                        미팅번호 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "CZ", "09", Global.MainFrame.GetStringToday.Substring(2, 2));

                        #region 참석자외부
                        dataRowArray = this._flex참석자외부.DataTable.Select("NO_MEETING = '" + D.GetString(dr["NO_MEETING"]) + "'");

                        this._flex참석자외부.Redraw = false;

                        foreach (DataRow dr1 in dataRowArray)
                            dr1["NO_MEETING"] = 미팅번호;

                        this._flex참석자외부.Redraw = true;
                        #endregion

                        #region 참석자내부
                        dataRowArray = this._flex참석자내부.DataTable.Select("NO_MEETING = '" + D.GetString(dr["NO_MEETING"]) + "'");

                        this._flex참석자내부.Redraw = false;

                        foreach (DataRow dr1 in dataRowArray)
                            dr1["NO_MEETING"] = 미팅번호;

                        this._flex참석자내부.Redraw = true;
                        #endregion

                        dr["NO_MEETING"] = 미팅번호;
                    }
                }

                if (!this._biz.Save(this._flex미팅메모.GetChanges(),
                                    this._flex참석자외부.GetChanges(),
                                    this._flex참석자내부.GetChanges())) return false;

                this._flex미팅메모.AcceptChanges();
                this._flex참석자외부.AcceptChanges();
                this._flex참석자내부.AcceptChanges();

                this._추가순번 = 0;

                if (isRefesh)
                    this.OnToolBarSearchButtonClicked(null, null);

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex참석자외부.Redraw = true;
                this._flex참석자내부.Redraw = true;
            }

            return false;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (this._flex미팅메모.HasNormalRow == false) return;

                dataRowArray = this._flex미팅메모.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (this._flex미팅메모.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.Added).Length > 0)
                    {
                        this.ShowMessage("저장되지 않은 건이 선택되어 있습니다.");
                        return;
                    }

                    this._gw.문서보기(dataRowArray);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void btn문서보기_Click(object sender, EventArgs e)
        {
            string strURL, key;

            try 
            {
                if (!this._flex미팅메모.HasNormalRow) return;
                if (this._flex미팅메모["ID_INSERT"].ToString() != Global.MainFrame.LoginInfo.UserID && this._flex미팅메모["CD_GW_STAT"].ToString() != "1")
                {
                    this.ShowMessage("문서작성자가 본인이거나 결재상태가 승인 상태인 문서만 확인 가능 합니다.");
                    return;
                }

                key = (MA.Login.회사코드 + "-" +D.GetString(this._flex미팅메모["NO_GW_DOCU"]));

                strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                                                  + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                                                  + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                                                  + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                                                  + "&login_id=" + this._flex미팅메모["ID_WRITE"].ToString();

                P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(strURL);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn전자결재_Click(object sender, EventArgs e)
        {
            string 전자결재번호, query;
            DataRow[] dataRowArray;

            try
            {
                if (this._flex미팅메모.HasNormalRow == false) return;

                dataRowArray = this._flex미팅메모.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    if (this._flex미팅메모.DataTable.Select("S = 'Y' AND ID_INSERT <> '" + Global.MainFrame.LoginInfo.UserID + "'", string.Empty).Length > 0)
                    {
                        this.ShowMessage("본인이 작성하지 않은 건이 선택되어 있습니다.");
                        return;
                    }

                    if (this._flex미팅메모.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.Added).Length > 0)
                    {
                        this.ShowMessage("저장되지 않은 건이 선택되어 있습니다.");
                        return;
                    }

                    전자결재번호 = D.GetString(this._flex미팅메모.DataTable.Compute("MAX(NO_MEETING)", "S = 'Y'"));

                    if (this._gw.전자결재(전자결재번호, dataRowArray))
                    {
                        foreach (DataRow dr in dataRowArray)
                        {
                            query = @"UPDATE CZ_SA_MEETING_MEMO
                                      SET NO_GW_DOCU = '" + 전자결재번호 + "'" + Environment.NewLine +
                                     "WHERE CD_COMPANY = '" + D.GetString(dr["CD_COMPANY"]) + "'" + Environment.NewLine +
                                     "AND NO_MEETING = '" + D.GetString(dr["NO_MEETING"]) + "'";

                            DBHelper.ExecuteScalar(query);
                        }

                        this.OnToolBarSearchButtonClicked(null, null);
                        this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn참석자추가_Click(object sender, EventArgs e)
        {
            DataRow newRow;

            try
            {
                if (((Control)sender).Name == this.btn참석자외부추가.Name)
                {
                    if (string.IsNullOrEmpty(D.GetString(this._flex미팅메모["CD_PARTNER"])))
                    {
                        this.ShowMessage("거래처를 지정해야 합니다.");
                        return;
                    }

                    newRow = this._flex참석자외부.DataTable.NewRow();

                    newRow["CD_COMPANY"] = D.GetString(this._flex미팅메모["CD_COMPANY"]);
                    newRow["NO_MEETING"] = D.GetString(this._flex미팅메모["NO_MEETING"]);
                    newRow["TP_INOUT"] = "OUT";
                    newRow["CD_PARTNER"] = D.GetString(this._flex미팅메모["CD_PARTNER"]);
                    newRow["NO_INDEX"] = (D.GetDecimal(this._flex참석자외부.DataTable.Compute("MAX(NO_INDEX)", string.Empty)) + 1);

                    this._flex참석자외부.DataTable.Rows.Add(newRow);
                    this._flex참석자외부.Row = this._flex참석자외부.Rows.Count - 1;

                    if (this._flex참석자외부.HasNormalRow && this._flex참석자외부.Rows.Count > 0)
                        this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;
                    else
                        this.ctx거래처.ReadOnly = ReadOnly.None;
                }
                else
                {
                    newRow = this._flex참석자내부.DataTable.NewRow();

                    newRow["CD_COMPANY"] = D.GetString(this._flex미팅메모["CD_COMPANY"]);
                    newRow["NO_MEETING"] = D.GetString(this._flex미팅메모["NO_MEETING"]);
                    newRow["TP_INOUT"] = "IN";
                    newRow["NO_INDEX"] = (D.GetDecimal(this._flex참석자내부.DataTable.Compute("MAX(NO_INDEX)", string.Empty)) + 1);

                    this._flex참석자내부.DataTable.Rows.Add(newRow);
                    this._flex참석자내부.Row = this._flex참석자내부.Rows.Count - 1;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn참석자삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (((Control)sender).Name == this.btn참석자외부삭제.Name)
                {
                    if (!this._flex참석자외부.HasNormalRow) return;
                    this._flex참석자외부.Rows.Remove(this._flex참석자외부.Row);

                    if (this._flex참석자외부.HasNormalRow && this._flex참석자외부.Rows.Count > 0)
                        this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;
                    else
                        this.ctx거래처.ReadOnly = ReadOnly.None;
                }
                else
                {
                    if (!this._flex참석자내부.HasNormalRow) return;
                    this._flex참석자내부.Rows.Remove(this._flex참석자내부.Row);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flex미팅메모_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataSet ds;
            string key, filter;

            try
            {
                ds = null;
                key = this._flex미팅메모["NO_MEETING"].ToString();
                filter = "NO_MEETING = '" + key + "'";

                if (this._flex미팅메모["ID_INSERT"].ToString() == Global.MainFrame.LoginInfo.UserID &&
                    this._flex미팅메모["CD_GW_STAT"].ToString() != "0" &&
                    this._flex미팅메모["CD_GW_STAT"].ToString() != "1")
                {
                    this.InitControl(true);
                }
                else
                {
                    this.InitControl(false);

                    if (this._flex미팅메모["ID_INSERT"].ToString() == Global.MainFrame.LoginInfo.UserID ||
                        this._flex미팅메모["CD_GW_STAT"].ToString() == "1")
                        this.btn문서보기.Enabled = true;
                }

                if (this._flex미팅메모.DetailQueryNeed == true)
                {
                    ds = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               key,
                                                               D.GetString(this._flex미팅메모["CD_PARTNER"]) });
                }

                if (ds != null && ds.Tables.Count == 2)
                {
                    this._flex참석자외부.BindingAdd(ds.Tables[0], filter);
                    this._flex참석자내부.BindingAdd(ds.Tables[1], filter);
                }
                else
                {
                    this._flex참석자외부.BindingAdd(null, filter);
                    this._flex참석자내부.BindingAdd(null, filter);
                }
                
                if (this._flex참석자외부.HasNormalRow && this._flex참석자외부.Rows.Count > 0)
                    this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;
                else
                    this.ctx거래처.ReadOnly = ReadOnly.None;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex참석자외부_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(D.GetString(this._flex미팅메모["CD_PARTNER"]))) 
                    return;

                e.Parameter.UserParams = "참석자외부;H_CZ_MA_PARTNERPTR_SUB";
                e.Parameter.P14_CD_PARTNER = D.GetString(this._flex미팅메모["CD_PARTNER"]);
                e.Parameter.P34_CD_MNG = D.GetString(this._flex미팅메모["LN_PARTNER"]);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex참석자내부_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID == HelpID.P_USER)
                {
                    e.Parameter.UserParams = "참석자내부;H_CZ_MA_CUSTOMIZE_SUB";
                    e.Parameter.P11_ID_MENU = "H_MA_EMP_SUB";
                    e.Parameter.P21_FG_MODULE = "N";
                    e.Parameter.MultiHelp = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void InitControl(bool isEnabled)
        {
            this.btn전자결재.Enabled = isEnabled;
            this.btn문서보기.Enabled = isEnabled;

            if (isEnabled)
                this.ctx거래처.ReadOnly = ReadOnly.None;
            else
                this.ctx거래처.ReadOnly = ReadOnly.TotalReadOnly;

            this.dtp미팅일자.Enabled = isEnabled;
            this.txt시간.ReadOnly = !isEnabled;
            this.txt장소.ReadOnly = !isEnabled;
            this.txt주제.ReadOnly = !isEnabled;
            this.txt목적.ReadOnly = !isEnabled;

            this.btn참석자외부추가.Enabled = isEnabled;
            this.btn참석자외부삭제.Enabled = isEnabled;
            this.btn참석자내부추가.Enabled = isEnabled;
            this.btn참석자내부삭제.Enabled = isEnabled;

            this._flex참석자외부.Cols["NM_ATTENDEE"].AllowEditing = isEnabled;
            this._flex참석자내부.Cols["NM_ATTENDEE"].AllowEditing = isEnabled;

            this.txt미팅내용.ReadOnly = !isEnabled;
        }
        #endregion
    }
}
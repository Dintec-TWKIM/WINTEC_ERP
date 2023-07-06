using System;
using System.Data;
using System.Text;
using System.Web;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.Erpiu.ComponentModel;
using System.Windows.Forms;
using Duzon.Common.Forms.Help;
using System.Text.RegularExpressions;

namespace cz
{
    public partial class P_CZ_SA_BUSINESS_TRIP_MNG : PageBase
    {
        #region 전역변수 및 초기화
        P_CZ_SA_BUSINESS_TRIP_MNG_BIZ _biz = new P_CZ_SA_BUSINESS_TRIP_MNG_BIZ();
        P_CZ_SA_BUSINESS_TRIP_MNG_GW _gw = new P_CZ_SA_BUSINESS_TRIP_MNG_GW();

        int _추가순번;

        public P_CZ_SA_BUSINESS_TRIP_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp출장일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
            this.dtp출장일자.EndDateToString = Global.MainFrame.GetStringToday;

            this.ctx작성자.CodeValue = Global.MainFrame.LoginInfo.UserID;
            this.ctx작성자.CodeName = Global.MainFrame.LoginInfo.UserName;

            this.split출장보고서.SplitterDistance = 759;
            this.split기본정보.SplitterDistance = 150;
            this.split서론.SplitterDistance = 50;
            this.split경비.SplitterDistance = 100;
            this.split미팅메모.SplitterDistance = 632;

            this.InitControl(false);
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex출장보고서, this._flex출장자, this._flex출장일정, this._flex출장경비, this._flex미팅메모 };
            this._flex출장보고서.DetailGrids = new FlexGrid[] { this._flex출장자, this._flex출장일정, this._flex출장경비, this._flex미팅메모 };

            #region 출장보고서
            this._flex출장보고서.BeginSetting(1, 1, false);

            this._flex출장보고서.SetCol("NM_GW_STAT", "결재상태", 80);
            this._flex출장보고서.SetCol("ID_GW_DOCU", "문서번호", false);
            this._flex출장보고서.SetCol("NO_BIZ_TRIP", "출장번호", 80);
            this._flex출장보고서.SetCol("NM_INSERT", "작성자", 80);
            this._flex출장보고서.SetCol("DT_START", "출장시작", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출장보고서.SetCol("DT_END", "출장종료", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출장보고서.SetCol("DC_DATE", "기간비고", 80);
            this._flex출장보고서.SetCol("DC_LOCATION", "출장지", 80);
            this._flex출장보고서.SetCol("DC_PURPOSE", "출장목적", 80);
            this._flex출장보고서.SetCol("DC_START", "서론", 80);
            this._flex출장보고서.SetCol("DC_END", "결론", 80);

            this._flex출장보고서.SetOneGridBinding(null, new IUParentControl[] { this.pnl기본정보, this.pnl출장목적, this.pnl서론, this.pnl결론 });
            this._flex출장보고서.ExtendLastCol = true;

            this._flex출장보고서.SettingVersion = "0.0.0.1";
            this._flex출장보고서.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 출장일정
            this._flex출장일정.BeginSetting(1, 1, true);

            this._flex출장일정.SetCol("DT_FROM", "시작일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출장일정.SetCol("DT_TO", "종료일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex출장일정.SetCol("DC_SCHEDULE", "내용", 80);

            this._flex출장일정.VerifyNotNull = new string[] { "DT_FROM", "DT_TO" };
            this._flex출장일정.ExtendLastCol = true;

            this._flex출장일정.SettingVersion = "0.0.0.1";
            this._flex출장일정.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 출장자
            this._flex출장자.BeginSetting(1, 1, false);

            this._flex출장자.SetCol("NM_COMPANY", "회사", 80);
            this._flex출장자.SetCol("NO_EMP", "사번", 80);
            this._flex출장자.SetCol("NM_KOR", "이름", 80, true);
            this._flex출장자.SetCol("NM_DEPT", "부서", 80);
            this._flex출장자.SetCol("NM_DUTY_RANK", "직위", 80);

            this._flex출장자.SetCodeHelpCol("NM_KOR", "H_CZ_MA_CUSTOMIZE_SUB", ShowHelpEnum.Always, new string[] { "CD_COMPANY_EMP", "NM_COMPANY", "NO_EMP", "NM_KOR", "NM_DEPT", "NM_DUTY_RANK" }, new string[] { "CD_COMPANY", "NM_COMPANY", "NO_EMP", "NM_KOR", "NM_DEPT", "NM_DUTY_RANK" });
            this._flex출장자.VerifyPrimaryKey = new string[] { "NO_EMP" };

            this._flex출장자.SettingVersion = "0.0.0.1";
            this._flex출장자.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 출장경비
            this._flex출장경비.BeginSetting(1, 1, true);

            this._flex출장경비.SetCol("TP_EXPENSE", "구분", 80);
            this._flex출장경비.SetCol("AM_EXPENSE", "비용", 80, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex출장경비.SetCol("DC_EXPENSE", "내용", 80);

            this._flex출장경비.VerifyNotNull = new string[] { "TP_EXPENSE" };
            this._flex출장경비.ExtendLastCol = true;

            this._flex출장경비.SetDataMap("TP_EXPENSE", Global.MainFrame.GetComboDataCombine("N;CZ_SA00037"), "CODE", "NAME");

            this._flex출장경비.SettingVersion = "0.0.0.1";
            this._flex출장경비.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 미팅메모
            this._flex미팅메모.BeginSetting(1, 1, false);

            this._flex미팅메모.SetCol("NO_MEETING", "미팅번호", 80, true);
            this._flex미팅메모.SetCol("LN_PARTNER", "거래처명", 100);
            this._flex미팅메모.SetCol("DT_MEETING", "미팅일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex미팅메모.SetCol("DC_LOCATION", "장소", 100);
            this._flex미팅메모.SetCol("DC_SUBJECT", "주제", 100);
            this._flex미팅메모.SetCol("DC_PURPOSE", "목적", 100);
            this._flex미팅메모.SetCol("DC_MEETING", "미팅내용", 100);

            this._flex미팅메모.SetCodeHelpCol("NO_MEETING", "H_CZ_MA_CUSTOMIZE_SUB", ShowHelpEnum.Always, new string[] { "NO_MEETING", "CD_PARTNER", "LN_PARTNER", "DT_MEETING", "DC_TIME", "DC_SUBJECT", "DC_PURPOSE", "DC_MEETING" }, new string[] { "NO_MEETING", "CD_PARTNER", "LN_PARTNER", "DT_MEETING", "DC_TIME", "DC_SUBJECT", "DC_PURPOSE", "DC_MEETING" });
            this._flex미팅메모.VerifyNotNull = new string[] { "NO_MEETING" };
            this._flex미팅메모.ExtendLastCol = true;

            this._flex미팅메모.SettingVersion = "0.0.0.1";
            this._flex미팅메모.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.btn전자결재.Click += new EventHandler(this.btn전자결재_Click);
            this.btn문서보기.Click += new EventHandler(this.btn문서보기_Click);

            this.btn출장일정추가.Click += new EventHandler(this.btn추가_Click);
            this.btn출장일정삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn출장자추가.Click += new EventHandler(this.btn추가_Click);
            this.btn출장자삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn출장경비추가.Click += new EventHandler(this.btn추가_Click);
            this.btn출장경비삭제.Click += new EventHandler(this.btn삭제_Click);
            this.btn미팅메모추가.Click += new EventHandler(this.btn추가_Click);
            this.btn미팅메모삭제.Click += new EventHandler(this.btn삭제_Click);

            this._flex출장보고서.AfterRowChange += new RangeEventHandler(this._flex출장보고서_AfterRowChange);
            this._flex출장자.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex출장자_BeforeCodeHelp);
            this._flex미팅메모.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex미팅메모_BeforeCodeHelp);
            this._flex미팅메모.DoubleClick += new EventHandler(this._flex미팅메모_DoubleClick);
            this._flex출장일정.AfterEdit += new RowColEventHandler(this._flex출장일정_AfterEdit);
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                this._flex출장보고서.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                               this.dtp출장일자.StartDateToString,
                                                                               this.dtp출장일자.EndDateToString,
                                                                               this.ctx작성자.CodeValue });

                if (!this._flex출장보고서.HasNormalRow)
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

                newRow = this._flex출장보고서.DataTable.NewRow();

                newRow["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                newRow["NO_BIZ_TRIP"] = this._추가순번.ToString();
                newRow["DT_START"] = Global.MainFrame.GetStringToday;
                newRow["DT_END"] = Global.MainFrame.GetStringToday;
                newRow["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                this._flex출장보고서.DataTable.Rows.Add(newRow);

                this._추가순번++;
                this._flex출장보고서.Row = this._flex출장보고서.Rows.Count - 1;

                this.dtp출장시작.Text = D.GetString(newRow["DT_START"]);
                this.dtp출장종료.Text = D.GetString(newRow["DT_END"]);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override bool BeforeDelete()
        {
            string 결재상태;

            try
            {
                if (this._flex출장보고서["ID_INSERT"].ToString() != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("본인이 작성한 건만 삭제 가능 합니다.");
                    return false;
                }

                결재상태 = D.GetString(this._flex출장보고서["CD_GW_STAT"]);

                if (결재상태 == "0" || 결재상태 == "1")
                {
                    this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", D.GetString(this._flex출장보고서["NM_GW_STAT"]));
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

                if (!this.BeforeDelete() || !this._flex출장보고서.HasNormalRow) return;
                
                this._flex출장보고서.Rows.Remove(this._flex출장보고서.Row);
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
            string 출장번호;
            bool isRefresh = false;

            try
            {
                if (!base.SaveData() || !base.Verify()) return false;

                if (this._flex출장보고서.IsDataChanged == false &&
                    this._flex출장일정.IsDataChanged == false &&
                    this._flex출장자.IsDataChanged == false &&
                    this._flex출장경비.IsDataChanged == false &&
                    this._flex미팅메모.IsDataChanged == false) return false;

                foreach (DataRow dr in this._flex출장보고서.DataTable.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        isRefresh = true;

                        출장번호 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "CZ", "10", Global.MainFrame.GetStringToday.Substring(2, 2));

                        #region 출장일정
                        dataRowArray = this._flex출장일정.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'");

                        this._flex출장일정.Redraw = false;

                        foreach (DataRow dr1 in dataRowArray)
                            dr1["NO_BIZ_TRIP"] = 출장번호;

                        this._flex출장일정.Redraw = true;
                        #endregion

                        #region 출장자
                        dataRowArray = this._flex출장자.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'");

                        this._flex출장자.Redraw = false;

                        foreach (DataRow dr1 in dataRowArray)
                            dr1["NO_BIZ_TRIP"] = 출장번호;

                        this._flex출장자.Redraw = true;
                        #endregion

                        #region 출장경비
                        dataRowArray = this._flex출장경비.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'");

                        this._flex출장경비.Redraw = false;

                        foreach (DataRow dr1 in dataRowArray)
                            dr1["NO_BIZ_TRIP"] = 출장번호;

                        this._flex출장경비.Redraw = true;
                        #endregion

                        #region 미팅메모
                        dataRowArray = this._flex미팅메모.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'");

                        this._flex미팅메모.Redraw = false;

                        foreach (DataRow dr1 in dataRowArray)
                            dr1["NO_BIZ_TRIP"] = 출장번호;

                        this._flex미팅메모.Redraw = true;
                        #endregion

                        dr["NO_BIZ_TRIP"] = 출장번호;
                    }

                    if (dr.RowState == DataRowState.Added || dr.RowState == DataRowState.Modified)
                    {
                        dr["DC_END"] = this.StripHtml(dr["DC_END"].ToString());
                    }
                }

                if (!this._biz.Save(this._flex출장보고서.GetChanges(),
                                    this._flex출장일정.GetChanges(),
                                    this._flex출장자.GetChanges(),
                                    this._flex출장경비.GetChanges(),
                                    this._flex미팅메모.GetChanges())) return false;

                this._flex출장보고서.AcceptChanges();
                this._flex출장일정.AcceptChanges();
                this._flex출장자.AcceptChanges();
                this._flex출장경비.AcceptChanges();
                this._flex미팅메모.AcceptChanges();

                this._추가순번 = 0;

                if (isRefresh)
                    this.OnToolBarSearchButtonClicked(null, null);

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                this._flex출장일정.Redraw = true;
                this._flex출장자.Redraw = true;
                this._flex출장경비.Redraw = true;
                this._flex미팅메모.Redraw = true;
            }

            return false;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            DataRow dr;

            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (this._flex출장보고서.HasNormalRow == false) return;
                if (this._flex출장보고서.RowState() == DataRowState.Added)
                {
                    this.ShowMessage("저장 후 다시 시도 하세요.");
                    return;
                }

                dr = this._flex출장보고서.GetDataRow(this._flex출장보고서.Row);

                this._gw.문서보기(dr,
                                  this._flex출장자.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'"),
                                  this._flex출장일정.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'"),
                                  this._flex출장경비,
                                  this._flex미팅메모.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'"));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void btn전자결재_Click(object sender, EventArgs e)
        {
            DataRow dr;

            try
            {
                if (this._flex출장보고서.HasNormalRow == false) return;

                if (this._flex출장보고서["ID_INSERT"].ToString() != Global.MainFrame.LoginInfo.UserID)
                {
                    this.ShowMessage("본인이 작성한 건만 전자결재 가능 합니다.");
                    return;
                }

                if (this._flex출장보고서.RowState() == DataRowState.Added)
                {
                    this.ShowMessage("저장 후 다시 시도 하세요.");
                    return;
                }

                dr = this._flex출장보고서.GetDataRow(this._flex출장보고서.Row);


                if (this._gw.전자결재(dr,
                                      this._flex출장자.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'"),
                                      this._flex출장일정.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'"),
                                      this._flex출장경비,
                                      this._flex미팅메모.DataTable.Select("NO_BIZ_TRIP = '" + D.GetString(dr["NO_BIZ_TRIP"]) + "'")))
                {
                    this.OnToolBarSearchButtonClicked(null, null);
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn문서보기_Click(object sender, EventArgs e)
        {
            string strURL, key;

            try
            {
                if (!this._flex출장보고서.HasNormalRow) return;
                if (this._flex출장보고서["ID_INSERT"].ToString() != Global.MainFrame.LoginInfo.UserID && this._flex출장보고서["CD_GW_STAT"].ToString() != "1")
                {
                    this.ShowMessage("문서작성자가 본인이거나 결재상태가 승인 상태인 문서만 확인 가능 합니다.");
                    return;
                }

                key = (MA.Login.회사코드 + "-" + D.GetString(this._flex출장보고서["NO_BIZ_TRIP"]));
                
                strURL = "http://gw.dintec.co.kr" + "/kor_webroot/src/cm/tims/index.aspx"
                                                  + "?cd_company=" + GroupWare.GetERP_CD_COMPANY()
                                                  + "&cd_pc=" + GroupWare.GetERP_CD_PC()
                                                  + "&no_docu=" + HttpUtility.UrlEncode(key, Encoding.UTF8)
                                                  + "&login_id=" + this._flex출장보고서["ID_WRITE"].ToString();

                P_CZ_MA_HTML_VIEWER dialog = new P_CZ_MA_HTML_VIEWER(strURL);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn추가_Click(object sender, EventArgs e)
        {
            DataRow newRow;
            string name;

            try
            {
                if (!this._flex출장보고서.HasNormalRow) return;

                name = ((Control)sender).Name;

                if (name == this.btn출장일정추가.Name)
                {
                    this.btn출장일정추가.Enabled = false;

                    newRow = this._flex출장일정.DataTable.NewRow();

                    newRow["CD_COMPANY"] = D.GetString(this._flex출장보고서["CD_COMPANY"]);
                    newRow["NO_BIZ_TRIP"] = D.GetString(this._flex출장보고서["NO_BIZ_TRIP"]);
                    newRow["NO_INDEX"] = (D.GetDecimal(this._flex출장일정.DataTable.Compute("MAX(NO_INDEX)", string.Format("NO_BIZ_TRIP = '{0}'", D.GetString(this._flex출장보고서["NO_BIZ_TRIP"])))) + 1);

                    this._flex출장일정.DataTable.Rows.Add(newRow);
                    this._flex출장일정.Row = this._flex출장일정.Rows.Count - 1;

                    this.btn출장일정추가.Enabled = true;
                }
                else if (name == this.btn출장자추가.Name)
                {
                    this.btn출장자추가.Enabled = false;

                    newRow = this._flex출장자.DataTable.NewRow();

                    newRow["CD_COMPANY"] = D.GetString(this._flex출장보고서["CD_COMPANY"]);
                    newRow["NO_BIZ_TRIP"] = D.GetString(this._flex출장보고서["NO_BIZ_TRIP"]);
                    newRow["NO_INDEX"] = (D.GetDecimal(this._flex출장자.DataTable.Compute("MAX(NO_INDEX)", string.Format("NO_BIZ_TRIP = '{0}'", D.GetString(this._flex출장보고서["NO_BIZ_TRIP"])))) + 1);

                    this._flex출장자.DataTable.Rows.Add(newRow);
                    this._flex출장자.Row = this._flex출장자.Rows.Count - 1;

                    this.btn출장자추가.Enabled = true;
                }
                else if (name == this.btn출장경비추가.Name)
                {
                    this.btn출장경비추가.Enabled = false;

                    newRow = this._flex출장경비.DataTable.NewRow();

                    newRow["CD_COMPANY"] = D.GetString(this._flex출장보고서["CD_COMPANY"]);
                    newRow["NO_BIZ_TRIP"] = D.GetString(this._flex출장보고서["NO_BIZ_TRIP"]);
                    newRow["NO_INDEX"] = (D.GetDecimal(this._flex출장경비.DataTable.Compute("MAX(NO_INDEX)", string.Format("NO_BIZ_TRIP = '{0}'", D.GetString(this._flex출장보고서["NO_BIZ_TRIP"])))) + 1);

                    this._flex출장경비.DataTable.Rows.Add(newRow);
                    this._flex출장경비.Row = this._flex출장경비.Rows.Count - 1;

                    this.btn출장경비추가.Enabled = true;
                }
                else if (name == this.btn미팅메모추가.Name)
                {
                    this.btn미팅메모추가.Enabled = false;

                    newRow = this._flex미팅메모.DataTable.NewRow();

                    newRow["CD_COMPANY"] = D.GetString(this._flex출장보고서["CD_COMPANY"]);
                    newRow["NO_BIZ_TRIP"] = D.GetString(this._flex출장보고서["NO_BIZ_TRIP"]);
                    newRow["NO_INDEX"] = (D.GetDecimal(this._flex미팅메모.DataTable.Compute("MAX(NO_INDEX)", string.Format("NO_BIZ_TRIP = '{0}'", D.GetString(this._flex출장보고서["NO_BIZ_TRIP"])))) + 1);

                    this._flex미팅메모.DataTable.Rows.Add(newRow);
                    this._flex미팅메모.Row = this._flex미팅메모.Rows.Count - 1;

                    this.btn미팅메모추가.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void btn삭제_Click(object sender, EventArgs e)
        {
            string name;

            try
            {
                if (!this._flex출장보고서.HasNormalRow) return;

                name = ((Control)sender).Name;

                if (name == this.btn출장일정삭제.Name)
                {
                    if (!this._flex출장일정.HasNormalRow) return;
                    this._flex출장일정.Rows.Remove(this._flex출장일정.Row);
                }
                else if (name == this.btn출장자삭제.Name)
                {
                    if (!this._flex출장자.HasNormalRow) return;
                    this._flex출장자.Rows.Remove(this._flex출장자.Row);
                }
                else if (name == this.btn출장경비삭제.Name)
                {
                    if (!this._flex출장경비.HasNormalRow) return;
                    this._flex출장경비.Rows.Remove(this._flex출장경비.Row);
                }
                else if (name == this.btn미팅메모삭제.Name)
                {
                    if (!this._flex미팅메모.HasNormalRow) return;
                    this._flex미팅메모.Rows.Remove(this._flex미팅메모.Row);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flex출장보고서_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataSet ds;
            string key, filter;

            try
            {
                ds = null;
                key = this._flex출장보고서["NO_BIZ_TRIP"].ToString();
                filter = "NO_BIZ_TRIP = '" + key + "'";

                if (this._flex출장보고서["ID_INSERT"].ToString() == Global.MainFrame.LoginInfo.UserID &&
                    this._flex출장보고서["CD_GW_STAT"].ToString() != "0" &&
                    this._flex출장보고서["CD_GW_STAT"].ToString() != "1")
                {
                    this.InitControl(true);
                }
                else
                {
                    this.InitControl(false);

                    if (this._flex출장보고서["ID_INSERT"].ToString() == Global.MainFrame.LoginInfo.UserID || 
                        this._flex출장보고서["CD_GW_STAT"].ToString() == "1")
                        this.btn문서보기.Enabled = true;                    
                }

                if (this._flex출장보고서.DetailQueryNeed == true)
                {
                    ds = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               key });
                }

                if (ds != null && ds.Tables.Count == 4)
                {
                    this._flex출장일정.BindingAdd(ds.Tables[0], filter);
                    this._flex출장자.BindingAdd(ds.Tables[1], filter);
                    this._flex출장경비.BindingAdd(ds.Tables[2], filter);
                    this._flex미팅메모.BindingAdd(ds.Tables[3], filter);
                }
                else
                {
                    this._flex출장일정.BindingAdd(null, filter);
                    this._flex출장자.BindingAdd(null, filter);
                    this._flex출장경비.BindingAdd(null, filter);
                    this._flex미팅메모.BindingAdd(null, filter);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex출장자_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID == HelpID.P_USER)
                {
                    e.Parameter.UserParams = "출장자;H_CZ_MA_CUSTOMIZE_SUB";
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

        private void _flex미팅메모_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID == HelpID.P_USER)
                {
                    e.Parameter.UserParams = "미팅메모;H_CZ_MA_CUSTOMIZE_SUB";
                    e.Parameter.P11_ID_MENU = "H_SA_MEETING_MEMO_SUB";
                    e.Parameter.P21_FG_MODULE = "N";
                    e.Parameter.MultiHelp = false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex미팅메모_DoubleClick(object sender, EventArgs e)
        {
            string pageId, pageName;

            try
            {
                if (this._flex미팅메모.HasNormalRow == false) return;
                if (this._flex미팅메모.MouseRow < this._flex미팅메모.Rows.Fixed) return;

                pageId = "P_CZ_SA_MEETING_MEMO_MNG";
                pageName = "미팅메모";

                if (this.IsExistPage(pageId, false))
                    this.UnLoadPage(pageId, false);

                this.LoadPageFrom(pageId, pageName, this.Grant, new object[] { D.GetString(this._flex미팅메모["NO_MEETING"]) });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex출장일정_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flex출장일정.Cols[e.Col].Name == "DT_FROM")
                    this._flex출장일정[e.Row, "DT_TO"] = this._flex출장일정[e.Row, "DT_FROM"];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        public string StripHtml(string Html)
        {
            string output;
            //get rid of HTML tags
            output = Regex.Replace(Html, "<[^>]*>", string.Empty);
            //get rid of multiple blank lines
            output = Regex.Replace(output, @"^\s*$\n", string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline);
            return output;
        }

        private void InitControl(bool isEnabled)
        {
            this.btn전자결재.Enabled = isEnabled;
            this.btn문서보기.Enabled = isEnabled;

            this.dtp출장시작.Enabled = isEnabled;
            this.dtp출장종료.Enabled = isEnabled;
            this.txt출장기간.ReadOnly = !isEnabled;
            this.txt출장지.ReadOnly = !isEnabled;
            this.txt출장목적.ReadOnly = !isEnabled;

            this.btn출장자추가.Enabled = isEnabled;
            this.btn출장자삭제.Enabled = isEnabled;
            this.btn출장일정추가.Enabled = isEnabled;
            this.btn출장일정삭제.Enabled = isEnabled;
            this._flex출장자.Cols["NM_KOR"].AllowEditing = isEnabled;
            this._flex출장일정.Cols["DT_FROM"].AllowEditing = isEnabled;
            this._flex출장일정.Cols["DT_TO"].AllowEditing = isEnabled;
            this._flex출장일정.Cols["DC_SCHEDULE"].AllowEditing = isEnabled;

            this.btn미팅메모추가.Enabled = isEnabled;
            this.btn미팅메모삭제.Enabled = isEnabled;
            this._flex미팅메모.Cols["NO_MEETING"].AllowEditing = isEnabled;

            this.txt서론.ReadOnly = !isEnabled;

            this.btn출장경비추가.Enabled = isEnabled;
            this.btn출장경비삭제.Enabled = isEnabled;
            this._flex출장경비.Cols["TP_EXPENSE"].AllowEditing = isEnabled;
            this._flex출장경비.Cols["AM_EXPENSE"].AllowEditing = isEnabled;
            this._flex출장경비.Cols["DC_EXPENSE"].AllowEditing = isEnabled;

            this.txt결론.ReadOnly = !isEnabled;
        }
        #endregion
    }
}

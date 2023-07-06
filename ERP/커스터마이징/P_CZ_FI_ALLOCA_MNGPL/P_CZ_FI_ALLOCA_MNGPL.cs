using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using account;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.FI;
using Duzon.Windows.Print;
using Dintec;

namespace cz
{
    public partial class P_CZ_FI_ALLOCA_MNGPL : PageBase
    {
        private P_CZ_FI_ALLOCA_MNGPL_BIZ _biz = new P_CZ_FI_ALLOCA_MNGPL_BIZ();

        private bool 양식구분체크
        {
            get
            {
                return !Checker.IsEmpty(this.cbo양식명, "양식명");
            }
        }

        private bool 계정레벨체크
        {
            get
            {
                return !Checker.IsEmpty(this.cbo계정레벨, "계정레벨");
            }
        }

        private bool 사용언어체크
        {
            get
            {
                return !Checker.IsEmpty(this.cbo사용언어, "사용언어");
            }
        }

        private bool 기준일자체크
        {
            get
            {
                return Checker.IsValid(this.dtp당기기준일자, true, "기간");
            }
        }

        private bool 화폐단위체크
        {
            get
            {
                return !Checker.IsEmpty(this.cbo화폐단위, "화폐단위");
            }
        }

        private bool 관리내역체크
        {
            get
            {
                return !Checker.IsEmpty(this.bpc관리내역, "관리내역");
            }
        }

        public P_CZ_FI_ALLOCA_MNGPL()
        {
            StartUp.Certify(this);
            this.InitializeComponent();
            this.MainGrids = new FlexGrid[] { this._flex };
            this.DataChanged += new EventHandler(this.Page_DataChanged);
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = this.ToolBarDeleteButtonEnabled = this.ToolBarSaveButtonEnabled = false;
                this.ToolBarPrintButtonEnabled = this._flex.HasNormalRow;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitGrid();
            this.InitOneGrid();
        }

        private void InitOneGrid()
        {
            this.oneGrid.UseCustomLayout = true;
            this.oneGrid.InitCustomLayout();

            this.bppnl당기기준일자.IsNecessaryCondition = true;
            this.bppnl양식명.IsNecessaryCondition = true;
            this.bppnl계정레벨.IsNecessaryCondition = true;
            this.bppnl관리내역.IsNecessaryCondition = true;
            this.bppnl단위구분.IsNecessaryCondition = true;
            this.bppnl배부구분.IsNecessaryCondition = true;
            this.bppnl사용언어.IsNecessaryCondition = true;
            this.bppnl화폐단위.IsNecessaryCondition = true;
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(2, 1, false);

            this._flex.SetCol("TP_GRPCAL", "", false);
            this._flex.SetCol("GOOD_CD_GOOD", "", false);
            this._flex.SetCol("CD_PACCT", "계정코드", false);
            this._flex.SetCol("NM_PACCT", "계정명", 150);

            this._flex.AllowCache = false;
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.InitControl();
        }

        private void InitControl()
        {
            ControlSetting controlSetting = new ControlSetting(this.PageID);

            controlSetting.SetControl양식구분(this.cbo양식명);
            controlSetting.SetControl계정레벨(this.cbo계정레벨);
            controlSetting.SetControl사용언어(this.cbo사용언어);
            controlSetting.SetControl화폐단위(this.cbo화폐단위);

            MA.GetCodeUser(new string[] { D.GetString( 공통.기간구분.월.GetHashCode()),
                                          D.GetString( 공통.기간구분.분기.GetHashCode()),
                                          D.GetString( 공통.기간구분.반기.GetHashCode()) }, new string[] { 공통.기간구분.월.ToString(),
                                                                                                           공통.기간구분.분기.ToString(),
                                                                                                           공통.기간구분.반기.ToString() });
            SetControl setControl = new SetControl();
            setControl.SetCombobox(this.cbo단위구분, MA.GetCode("FI_J000023"));
            setControl.SetCombobox(this.cbo배부구분, MA.GetCode("FI_J000072"));

            new YmdSetting().SetYmd(this.dtp당기기준일자, this.dtp전기기준일자, YmdSetting.메뉴구분.손익계산서);
        }

        protected override bool BeforeSearch()
        {
            return base.BeforeSearch() && this.양식구분체크 && (this.계정레벨체크 && this.사용언어체크) && (this.기준일자체크 && this.화폐단위체크 && this.관리내역체크);
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            string query;

            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                P_FI_PL_BASE pFiPlBase = new P_FI_PL_BASE(this.PageID, D.GetString(this.cbo양식명.SelectedValue), D.GetString(this.cbo사용언어.SelectedValue), D.GetString(this.cbo계정레벨.SelectedValue));

                MsgControl.ShowMsg("자료를 조회하는 중입니다.\n잠시만 기다려 주세요.");

                DataSet dataSet = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  D.GetString(this.cbo양식명.SelectedValue),
                                                                  this.dtp전기기준일자.StartDateToString,
                                                                  this.dtp전기기준일자.EndDateToString,
                                                                  this.dtp당기기준일자.StartDateToString,
                                                                  this.dtp당기기준일자.EndDateToString,
                                                                  this.cbo단위구분.SelectedValue,
                                                                  this.bpc관리내역.QueryWhereIn_Pipe,
                                                                  this.cbo배부구분.SelectedValue,
                                                                  (this.chk배부단위표시.Checked == true ? "Y" : "N"),
                                                                  (this.chk배부단위그룹표시.Checked == true ? "Y" : "N") });

                MsgControl.ShowMsg("관리내역 세팅 중입니다.\n잠시만 기다려 주세요.");

                string[] strArray1 = this.bpc관리내역.QueryWhereIn_Pipe.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string[] strArray2 = this.bpc관리내역.QueryWhereIn_PipeDisplayMember.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                Dictionary<string, string> dictionary3 = new Dictionary<string, string>();

                for (int index = 0; index < strArray1.Length; ++index)
                    dictionary2.Add(strArray1[index], strArray2[index]);

                if (this.chk단위필터.Checked)
                {
                    string empty1 = string.Empty;
                    string empty2 = string.Empty;
                    string empty3 = string.Empty;

                    for (int index = 0; index < dataSet.Tables[1].Rows.Count; ++index)
                    {
                        DataRow[] dataRowArray = pFiPlBase.GetDtAcct.Select(string.Format("CD_ACCT = '{0}'", dataSet.Tables[1].Rows[index]["CD_ACCT"].ToString()));

                        if (D.GetString(this.cbo계정레벨.SelectedValue) == "4" && dataRowArray.Length == 0)
                        {
                            string filterExpression = "CD_ACCT_PIPE LIKE '%" + dataSet.Tables[1].Rows[index]["CD_ACCT"].ToString() + "%'";
                            dataRowArray = pFiPlBase.GetDtAcct.Select(filterExpression);
                        }

                        if (dataRowArray.Length > 0 && dictionary2.ContainsKey(dataSet.Tables[1].Rows[index]["CD_CODE"].ToString()) && (!dictionary1.ContainsKey(dataSet.Tables[1].Rows[index]["CD_CODE"].ToString()) && dictionary2.TryGetValue(dataSet.Tables[1].Rows[index]["CD_CODE"].ToString(), out empty1)))
                            dictionary1.Add(dataSet.Tables[1].Rows[index]["CD_CODE"].ToString(), empty1);
                    }
                }
                else
                    dictionary1 = dictionary2;

                if (this.chk배부단위그룹표시.Checked)
                {
                    query = @"SELECT AH.CD_ALLOCAGRP,
                          	     AH.NM_ALLOCAGRP,
                          	     AD.CD_ALLOCAUNIT 
                          FROM FI_ALLOCAGRPD AD WITH(NOLOCK) 
                          JOIN FI_ALLOCAGRP AH WITH(NOLOCK)  ON AH.CD_COMPANY = AD.CD_COMPANY AND AH.TP_UNIT = AD.TP_UNIT AND AH.CD_ALLOCAGRP = AD.CD_ALLOCAGRP
                          WHERE AD.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND AD.TP_UNIT = '" + this.cbo단위구분.SelectedValue + "'";

                    DataTable dt = DBHelper.GetDataTable(query);
                    DataRow dr;
                    
                    foreach (string 관리내역 in dictionary1.Keys)
                    {
                        dr = dt.Select("CD_ALLOCAUNIT = '" + 관리내역 + "'")[0];

                        if (!dictionary3.ContainsKey(D.GetString(dr["CD_ALLOCAGRP"])))
                            dictionary3.Add(D.GetString(dr["CD_ALLOCAGRP"]), D.GetString(dr["NM_ALLOCAGRP"]));
                    }
                }

                MsgControl.ShowMsg("데이터 세팅 중입니다.\n잠시만 기다려 주세요.");

                Setting setting = new Setting(dictionary1, dictionary3, this.chk배부단위표시.Checked, this.chk배부단위그룹표시.Checked, this.chk증감율표시.Checked);
                setting.SetDtAcct = pFiPlBase.GetDtAcct;
                setting.SetDsData = dataSet;
                setting.SetTable();

                DataSetting dataSetting = new DataSetting(this.PageID, D.GetString(this.cbo계정레벨.SelectedValue), D.GetString(this.cbo화폐단위.SelectedValue));
                DataTable getDt = setting.GetDt;
                dataSetting.Set기준Col = "NM_PACCT";

                List<string> stringList = new List<string>();
                stringList.Add("AM_SUM");
                stringList.Add("AM_SUM_BEFORE");

                if (this.chk배부단위그룹표시.Checked)
                {
                    foreach (string key in dictionary3.Keys)
                    {
                        stringList.Add("AM_GRP_" + key);
                        stringList.Add("AM_GRP_" + key + "_BEFORE");
                    }
                }

                if (this.chk배부단위표시.Checked)
                {
                    foreach (string key in dictionary1.Keys)
                    {
                        stringList.Add("AM_CUR_" + key);
                        stringList.Add("AM_CUR_" + key + "_BEFORE");
                    }
                }
                
                stringList.Add("AM_ETC");
                stringList.Add("AM_ETC_BEFORE");

                dataSetting.Set금액Cols = stringList.ToArray();
                dataSetting.SetTable(getDt);
                setting.SetModify(getDt);
                getDt.AcceptChanges();

                GridSetting.SetUnVisible재고내역(getDt);

                this.SetGridCols(dictionary1, dictionary3, this.chk배부단위표시.Checked, this.chk배부단위그룹표시.Checked);
                this._flex.Binding = getDt;
                this.Set단위필터(this.chk단위필터.Checked);

                MsgControl.ShowMsg("그리드 세팅 중입니다.\n잠시만 기다려 주세요.");

                GridSetting gridSetting = new GridSetting(this.PageID);
                this.SetStyleAsLocal(this._flex);
                //gridSetting.SetStyle(this._flex);

                MsgControl.CloseMsg();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
                MsgControl.CloseMsg();
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        public void SetStyleAsLocal(FlexGrid flex)
        {
            if (!flex.Cols.Contains("TP_GRPCAL"))
                throw new Exception("개발자는 그리드에 TP_GRPCAL 컬럼을 반드로 지정해야 합니다.");

            if (!flex.Cols.Contains("GOOD_CD_GOOD"))
                throw new Exception("개발자는 그리드에  GOOD_CD_GOOD 컬럼을 반드로 지정해야 합니다.");

            flex.Styles.Add("BOLD").Font = new Font(flex.Styles.Normal.Font, FontStyle.Bold);
            flex.Styles.Add("RED").ForeColor = Color.Red;
            flex.Styles.Add("BLUE").ForeColor = Color.Blue;

            for (int index = flex.Rows.Fixed; index < flex.Rows.Count; ++index)
            {
                if (D.GetInt(flex[index, "TP_GRPCAL"]) != FG_GRPCAL.계정.GetHashCode())
                    flex.Rows[index].Style = flex.Styles["BOLD"];

                switch (D.GetInt(flex[index, "GOOD_CD_GOOD"]))
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        flex.Rows[index].Style = flex.Styles["BLUE"];
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        flex.Rows[index].Style = flex.Styles["RED"];
                        break;
                }
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                if (this.chk단위필터.Checked)
                    this.Set인쇄단위필터();

                DataTable tableFromGrid = this._flex.GetTableFromGrid();
                List<string> stringList1 = new List<string>();
                List<string> stringList2 = new List<string>();

                for (int index = this._flex.Cols["AM_SUM"].Index + 1; index <= this._flex.Cols.Count - 1; ++index)
                    stringList1.Add("[" + this._flex.Cols[index].Caption + "] " + D.GetString(this._flex.Rows[1][index]));

                string[] strArray = new string[2] { "@NM_PACCT", "@AM_SUM" };
                string[] cols1 = new string[7] { "@T1", "@T2", "@T3", "@T4", "@T5", "@T6", "@T7" };
                string[] array = stringList1.ToArray();
                int startColIndex = tableFromGrid.Columns["AM_SUM"].Ordinal + 1;
                int endColIndex = tableFromGrid.Columns["AM_SUM"].Ordinal + array.Length;
                string[] cols2 = new string[7] { "@D1", "@D2", "@D3", "@D4", "@D5", "@D6", "@D7" };

                ReportHelper reportHelper = new ReportHelper("R_FI_ALLOCA_MNGPL_0", this.PageName, true);

                reportHelper.SetData("배부단위별 손익현황", this.DD("배부단위별 손익현황"));
                reportHelper.SetData("기간F", this.dtp당기기준일자.StartDateToString);
                reportHelper.SetData("기간T", this.dtp당기기준일자.EndDateToString);

                reportHelper.DynamicFixedCols = strArray;
                reportHelper.DynamicCaptions(cols1, array, startColIndex, endColIndex);
                reportHelper.DynamicDatas(cols2);

                reportHelper.SetDataTable(tableFromGrid);

                reportHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void bpc관리항목_CodeChanged(object sender, EventArgs e)
        {
            this.bpc관리내역.ChangeType(this.cbo단위구분.SelectedValue.ToString());
        }

        private void bpc관리내역_QueryBefore(object sender, BpQueryArgs e)
        {
            e.HelpParam.P41_CD_FIELD1 = this.cbo단위구분.SelectedValue.ToString();
            e.HelpParam.P42_CD_FIELD2 = "001";
            e.HelpParam.P43_CD_FIELD3 = "Y";
        }

        private void SetGridCols(Dictionary<string, string> 관리내역리스트, Dictionary<string, string> 관리내역리스트1, bool 배부단위표시, bool 배부단위그룹표시)
        {
            this._flex.BeginSetting(2, 1, false);
            this._flex.Cols.Count = 5;

            this._flex.SetCol("AM_SUM", "당기", 150, false, typeof(decimal), FormatTpType.MONEY);
            this._flex[0, "AM_SUM"] = "합계";

            this._flex.SetCol("AM_SUM_BEFORE", "전기", 150, false, typeof(decimal), FormatTpType.MONEY);
            this._flex[0, "AM_SUM_BEFORE"] = "합계";

            this._flex.SetCol("AM_SUM_DIFF", "증감액", 150, false, typeof(decimal), FormatTpType.MONEY);
            this._flex[0, "AM_SUM_DIFF"] = "합계";

            if (this.chk증감율표시.Checked)
            {
                this._flex.SetCol("AM_SUM_RATE", "증감율(%)", 150, false, typeof(decimal), FormatTpType.MONEY);
                this._flex[0, "AM_SUM_RATE"] = "합계";
            }

            if (배부단위그룹표시)
            {
                foreach (string key in 관리내역리스트1.Keys)
                {
                    this._flex.SetCol(("AM_GRP_" + key), "당기", 110, false, typeof(decimal), FormatTpType.MONEY);
                    this._flex[0, ("AM_GRP_" + key)] = 관리내역리스트1[key] + "그룹(" + key + ")";

                    this._flex.SetCol(("AM_GRP_" + key + "_BEFORE"), "전기", 110, false, typeof(decimal), FormatTpType.MONEY);
                    this._flex[0, ("AM_GRP_" + key + "_BEFORE")] = 관리내역리스트1[key] + "그룹(" + key + ")";

                    this._flex.SetCol(("AM_GRP_" + key + "_DIFF"), "증감액", 110, false, typeof(decimal), FormatTpType.MONEY);
                    this._flex[0, ("AM_GRP_" + key + "_DIFF")] = 관리내역리스트1[key] + "그룹(" + key + ")";

                    if (this.chk증감율표시.Checked)
                    {
                        this._flex.SetCol(("AM_GRP_" + key + "_RATE"), "증감율(%)", 110, false, typeof(decimal), FormatTpType.MONEY);
                        this._flex[0, ("AM_GRP_" + key + "_RATE")] = 관리내역리스트1[key] + "그룹(" + key + ")";
                    }
                }    
            }

            if (배부단위표시)
            {
                foreach (string key in 관리내역리스트.Keys)
                {
                    this._flex.SetCol(("AM_CUR_" + key), "당기", 110, false, typeof(decimal), FormatTpType.MONEY);
                    this._flex[0, ("AM_CUR_" + key)] = 관리내역리스트[key] + "(" + key + ")";

                    this._flex.SetCol(("AM_CUR_" + key + "_BEFORE"), "전기", 110, false, typeof(decimal), FormatTpType.MONEY);
                    this._flex[0, ("AM_CUR_" + key + "_BEFORE")] = 관리내역리스트[key] + "(" + key + ")";

                    this._flex.SetCol(("AM_CUR_" + key + "_DIFF"), "증감액", 110, false, typeof(decimal), FormatTpType.MONEY);
                    this._flex[0, ("AM_CUR_" + key + "_DIFF")] = 관리내역리스트[key] + "(" + key + ")";

                    if (this.chk증감율표시.Checked)
                    {
                        this._flex.SetCol(("AM_CUR_" + key + "_RATE"), "증감율(%)", 110, false, typeof(decimal), FormatTpType.MONEY);
                        this._flex[0, ("AM_CUR_" + key + "_RATE")] = 관리내역리스트[key] + "(" + key + ")";
                    }
                }
            }
            
            this._flex.SetCol("AM_ETC", "당기", 130, false, typeof(decimal), FormatTpType.MONEY);
            this._flex[0, "AM_ETC"] = "기타";

            this._flex.SetCol("AM_ETC_BEFORE", "전기", 130, false, typeof(decimal), FormatTpType.MONEY);
            this._flex[0, "AM_ETC_BEFORE"] = "기타";

            this._flex.SetCol("AM_ETC_DIFF", "증감액", 130, false, typeof(decimal), FormatTpType.MONEY);
            this._flex[0, "AM_ETC_DIFF"] = "기타";

            if (this.chk증감율표시.Checked)
            {
                this._flex.SetCol("AM_ETC_RATE", "증감율(%)", 130, false, typeof(decimal), FormatTpType.MONEY);
                this._flex[0, "AM_ETC_RATE"] = "기타";
            }

            this._flex.AllowCache = false;
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flex.Cols.Fixed = 1;
        }

        private void Set단위필터(bool is단위필터)
        {
            if (!is단위필터) return;

            decimal num1 = 0;

            for (int index1 = this._flex.Cols["AM_SUM"].Index; index1 < this._flex.Cols.Count; ++index1)
            {
                decimal num2 = 0;

                for (int index2 = this._flex.Rows.Fixed; index2 < this._flex.Rows.Count; ++index2)
                {
                    if (this._flex[index2, index1] != null && this._flex[index2, index1] != DBNull.Value && !(this._flex[index2, index1].ToString() == ""))
                        num2 += Math.Abs(Decimal.Parse(this._flex[index2, index1].ToString()));
                }

                if (num2 == 0)
                    this._flex.Cols[index1].Visible = false;
                else
                    this._flex.Cols[index1].Visible = true;

                num1 = num2;
            }
        }

        private void Set인쇄단위필터()
        {
            for (int index = this._flex.Cols["AM_SUM"].Index + 1; index < this._flex.Cols.Count; ++index)
            {
                if (!this._flex.Cols[index].Visible)
                {
                    this._flex.Cols.Remove(this._flex.Cols[index]);
                    --index;
                }
            }
        }
    }
}
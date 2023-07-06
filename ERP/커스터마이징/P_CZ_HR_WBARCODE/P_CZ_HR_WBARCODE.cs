using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.OLD;
using human;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace cz
{
    public partial class P_CZ_HR_WBARCODE : PageBase
    {
        P_CZ_HR_WBARCODE_BIZ _biz = new P_CZ_HR_WBARCODE_BIZ();

        #region 초기화 & 전역변수
        public P_CZ_HR_WBARCODE()
        {
            StartUp.Certify(this);
            this.InitializeComponent();

            this.tabControl.TabPages.Remove(this.tpg리더기설정);
            this.tabControl3.TabPages.Remove(this.tpg승인일반);

            UGrant ugrant = new UGrant();
            ugrant.GrantButtonEnble("P_CZ_HR_WBARCODE", "CONFIRM", this.btn승인, true);

            if (this.btn승인.Enabled == false)
                tabControl2.TabPages.Remove(this.tpg승인대기);

            if (!ugrant.GrantButton("P_CZ_HR_WBARCODE", "EMP_CARD"))
                this.tabControl.TabPages.Remove(this.tpg사원카드번호연결);
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex사원카드번호연결, this._flex승인일반, this._flex승인Pivot, this._flex승인대기 };

            #region 사원번호카드연결
            this._flex사원카드번호연결.BeginSetting(1, 1, true);

            this._flex사원카드번호연결.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex사원카드번호연결.SetCol("NM_DEPT", "부서명", 100, false);
            this._flex사원카드번호연결.SetCol("NO_EMP", "사번", 90, false);
            this._flex사원카드번호연결.SetCol("NM_KOR", "성명", 100, false);
            this._flex사원카드번호연결.SetCol("NO_CARD", "카드번호", 100);
            this._flex사원카드번호연결.SetCol("DT_START", "시작일자", 90, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex사원카드번호연결.SetCol("DT_CLOSE", "종료일자", 90, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex사원카드번호연결.SetCol("YN_CARD", "카드사용유무", 90, true, CheckTypeEnum.Y_N);

            this._flex사원카드번호연결.Cols["DT_START"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex사원카드번호연결.Cols["DT_CLOSE"].TextAlign = TextAlignEnum.CenterCenter;

            this._flex사원카드번호연결.SetDummyColumn(new string[] { "S" });

            this._flex사원카드번호연결.SettingVersion = "0.0.0.1";
            this._flex사원카드번호연결.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 승인대기
            this._flex승인대기.BeginSetting(1, 1, false);

            this._flex승인대기.SetCol("S", "S", 40, true, CheckTypeEnum.Y_N);
            this._flex승인대기.SetCol("DT_WORK", "근태일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex승인대기.SetCol("NO_EMP", "사번", 90);
            this._flex승인대기.SetCol("NM_KOR", "성명", 100);
            this._flex승인대기.SetCol("TM_001", "출근", 80, true, typeof(string));
            this._flex승인대기.SetCol("TM_002", "퇴근", 80, true, typeof(string));
            this._flex승인대기.SetCol("TM_003", "외출", false);
            this._flex승인대기.SetCol("TM_004", "복귀", false);
            this._flex승인대기.SetCol("YN_CONFIRM", "승인여부", 60, false, CheckTypeEnum.Y_N);

            this._flex승인대기.Cols["TM_001"].Format = "00:00:00";
            this._flex승인대기.Cols["TM_001"].EditMask = "00:00:00";
            this._flex승인대기.Cols["TM_001"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인대기.SetStringFormatCol("TM_001");
            this._flex승인대기.SetNoMaskSaveCol("TM_001");

            this._flex승인대기.Cols["TM_002"].Format = "00:00:00";
            this._flex승인대기.Cols["TM_002"].EditMask = "00:00:00";
            this._flex승인대기.Cols["TM_002"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인대기.SetStringFormatCol("TM_002");
            this._flex승인대기.SetNoMaskSaveCol("TM_002");

            this._flex승인대기.Cols["TM_003"].Format = "00:00:00";
            this._flex승인대기.Cols["TM_003"].EditMask = "00:00:00";
            this._flex승인대기.Cols["TM_003"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인대기.SetStringFormatCol("TM_003");
            this._flex승인대기.SetNoMaskSaveCol("TM_003");

            this._flex승인대기.Cols["TM_004"].Format = "00:00:00";
            this._flex승인대기.Cols["TM_004"].EditMask = "00:00:00";
            this._flex승인대기.Cols["TM_004"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인대기.SetStringFormatCol("TM_004");
            this._flex승인대기.SetNoMaskSaveCol("TM_004");

            this._flex승인대기.SetDummyColumn(new string[] { "S" });

            this._flex승인대기.SettingVersion = "0.0.0.1";
            this._flex승인대기.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 숭인일반
            this._flex승인일반.BeginSetting(1, 1, false);

            this._flex승인일반.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex승인일반.SetCol("DT_WORK", "근태일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex승인일반.SetCol("FG1_HOLIDAY", "구분", 70);
            this._flex승인일반.SetCol("TP_WEEK", "요일", 70);
            this._flex승인일반.SetCol("NO_CARD", "카드번호", 100, true);
            this._flex승인일반.SetCol("NO_EMP", "사번", 90);
            this._flex승인일반.SetCol("NM_KOR", "성명", 100);
            this._flex승인일반.SetCol("CD_WCODE", "근태", 80, true);
            this._flex승인일반.SetCol("TM_CARD", "근무시간", 80, true, typeof(string));
            this._flex승인일반.SetCol("NM_UPDATE", "수정자", 100);
            this._flex승인일반.SetCol("DTSUPDATE", "수정일시", 100);

            this._flex승인일반.Cols["TM_CARD"].Format = "00:00:00";
            this._flex승인일반.Cols["TM_CARD"].EditMask = "00:00:00";
            this._flex승인일반.Cols["TM_CARD"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인일반.Cols["DTSUPDATE"].TextAlign = TextAlignEnum.CenterCenter;

            this._flex승인일반.SetStringFormatCol("TM_CARD");
            this._flex승인일반.SetNoMaskSaveCol("TM_CARD");
            this._flex승인일반.SetDummyColumn(new string[1] { "S" });

            this._flex승인일반.SettingVersion = "0.0.0.1";
            this._flex승인일반.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 승인Pivot
            this._flex승인Pivot.BeginSetting(1, 1, false);

            this._flex승인Pivot.SetCol("DT_WORK", "근태일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex승인Pivot.SetCol("NO_EMP", "사번", 90);
            this._flex승인Pivot.SetCol("NM_KOR", "성명", 100);
            this._flex승인Pivot.SetCol("TM_001", "출근", 80, this.btn승인.Enabled, typeof(string));
            this._flex승인Pivot.SetCol("TM_002", "퇴근", 80, this.btn승인.Enabled, typeof(string));
			this._flex승인Pivot.SetCol("TM_003", "외출", false);
			this._flex승인Pivot.SetCol("TM_004", "복귀", false);

			this._flex승인Pivot.Cols["TM_001"].Format = "00:00:00";
            this._flex승인Pivot.Cols["TM_001"].EditMask = "00:00:00";
            this._flex승인Pivot.Cols["TM_001"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인Pivot.SetStringFormatCol("TM_001");
            this._flex승인Pivot.SetNoMaskSaveCol("TM_001");

            this._flex승인Pivot.Cols["TM_002"].Format = "00:00:00";
            this._flex승인Pivot.Cols["TM_002"].EditMask = "00:00:00";
            this._flex승인Pivot.Cols["TM_002"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인Pivot.SetStringFormatCol("TM_002");
            this._flex승인Pivot.SetNoMaskSaveCol("TM_002");

            this._flex승인Pivot.Cols["TM_003"].Format = "00:00:00";
            this._flex승인Pivot.Cols["TM_003"].EditMask = "00:00:00";
            this._flex승인Pivot.Cols["TM_003"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인Pivot.SetStringFormatCol("TM_003");
            this._flex승인Pivot.SetNoMaskSaveCol("TM_003");

            this._flex승인Pivot.Cols["TM_004"].Format = "00:00:00";
            this._flex승인Pivot.Cols["TM_004"].EditMask = "00:00:00";
            this._flex승인Pivot.Cols["TM_004"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex승인Pivot.SetStringFormatCol("TM_004");
            this._flex승인Pivot.SetNoMaskSaveCol("TM_004");

            this._flex승인Pivot.SettingVersion = "0.0.0.1";
            this._flex승인Pivot.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.bpc사업장.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.bpc사업장1.QueryAfter += new BpQueryHandler(this.OnBpControl_QueryAfter);
            this.btnCaps데이터읽기.Click += new EventHandler(this.btnCaps데이터읽기_Click);
            this.btnSecom데이터읽기.Click += new EventHandler(this.btnSecom데이터읽기_Click);
            this.cbo리더기종류.SelectionChangeCommitted += new EventHandler(this.cbo리더기종류_SelectionChangeCommitted);
            this.tabControl.SelectedIndexChanged += new EventHandler(this.tabControl_SelectedIndexChanged);
            this.txt근태코드연결복귀.Validated += new EventHandler(this.OnControlValidated);
            this.txt근태코드연결퇴근.Validated += new EventHandler(this.OnControlValidated);
            this.txt근태코드연결외출.Validated += new EventHandler(this.OnControlValidated);
            this.txt근태코드연결출근.Validated += new EventHandler(this.OnControlValidated);
            this.rdoFILE사용.Click += new EventHandler(this.OnControlValidated);
            this.rdo리더기사용.Click += new EventHandler(this.OnControlValidated);
            this.rdoText일반.Click += new EventHandler(this.OnControlValidated);
            this.rdoText탭으로분리.Click += new EventHandler(this.OnControlValidated);
            this.chk근태코드존재.Click += new EventHandler(this.OnControlValidated);
            this.cur기기번호탭분리위치.Validated += new EventHandler(this.OnControlValidated);
            this.cur근태탭분리위치.Validated += new EventHandler(this.OnControlValidated);
            this.cur카드번호탭분리위치.Validated += new EventHandler(this.OnControlValidated);
            this.cur근무시간탭분리위치.Validated += new EventHandler(this.OnControlValidated);
            this.cur근태일자탭분리위치.Validated += new EventHandler(this.OnControlValidated);
            this.cur기기번호자릿수.Validated += new EventHandler(this.OnControlValidated);
            this.cur근태자릿수.Validated += new EventHandler(this.OnControlValidated);
            this.cur카드번호자릿수.Validated += new EventHandler(this.OnControlValidated);
            this.cur근무시간자릿수.Validated += new EventHandler(this.OnControlValidated);
            this.cur근태일자자릿수.Validated += new EventHandler(this.OnControlValidated);
            this.txt기기번호시작포인트.Validated += new EventHandler(this.OnControlValidated);
            this.txt근태시작포인트.Validated += new EventHandler(this.OnControlValidated);
            this.txt카드번호시작포인트.Validated += new EventHandler(this.OnControlValidated);
            this.txt근무시간시작포인트.Validated += new EventHandler(this.OnControlValidated);
            this.txt근태일자시작포인트.Validated += new EventHandler(this.OnControlValidated);
            this.msk출근출근.Validated += new EventHandler(this.OnControlValidated);
            this.msk출근퇴근.Validated += new EventHandler(this.OnControlValidated);
            this.chk시간.Click += new EventHandler(this.OnControlValidated);
            this.nud연결대수.ValueChanged += new EventHandler(this.OnControlValidated);
            this.cbo리더기종류.SelectionChangeCommitted += new EventHandler(this.OnControlValidated);
            this.cbo통신속도.SelectionChangeCommitted += new EventHandler(this.OnControlValidated);
            this.cbo포트지정.SelectionChangeCommitted += new EventHandler(this.OnControlValidated);
            this.txtFORMAT지정1.Validated += new EventHandler(this.OnControlValidated);
            this.txtFORMAT지정.Validated += new EventHandler(this.OnControlValidated);
            this.btn카드정보올리기.Click += new EventHandler(this.btn카드정보올리기_Click);
            this.btn사번복사.Click += new EventHandler(this.btn사번복사_Click);
            this.bpc부서.QueryBefore += new BpQueryHandler(this.OnBpControl_Before);
            this.btnFPMS데이터읽기.Click += new EventHandler(this.btnFPMS데이터읽기_Click);
            this.btn데이터읽기.Click += new EventHandler(this.btn데이터읽기_Click);
            this.bpc부서1.QueryBefore += (new BpQueryHandler(this.OnBpControl_Before));
            this.msk퇴근퇴근.Validated += new EventHandler(this.OnControlValidated);
			this.btn승인.Click += Btn승인_Click;

            this._flex사원카드번호연결.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);
            this._flex사원카드번호연결.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flex승인일반.AfterDataRefresh += new ListChangedEventHandler(this._flex_AfterDataRefresh);
        }

        private void Btn승인_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow[] dataRowArray;
            string query;

            try
            {
                dataRowArray = this._flex승인대기.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
				{
                    query = @"SELECT 1 
							  FROM HR_WBARCODE BC WITH(NOLOCK)
							  WHERE BC.CD_COMPANY = '{0}'
							  AND BC.NO_CARD = '{1}'
							  AND BC.DT_WORK = '{2}'";

                    foreach(DataRow dr in dataRowArray)
					{
                        if (D.GetDecimal(dr["TM_001"]) >= 240000 ||
                            D.GetDecimal(dr["TM_002"]) >= 240000 ||
                            D.GetDecimal(dr["TM_003"]) >= 240000 ||
                            D.GetDecimal(dr["TM_004"]) >= 240000)
						{
                            this.ShowMessage("24시 넘어가는 건이 있습니다.");
                            return;
                        }

                        DateTime result;
                        if (!string.IsNullOrEmpty(dr["TM_001"].ToString()) && !DateTime.TryParseExact(dr["TM_001"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                        {
                            this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                            return;
                        }
                        else if (!string.IsNullOrEmpty(dr["TM_002"].ToString()) && !DateTime.TryParseExact(dr["TM_002"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                        {
                            this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                            return;
                        }
                        else if (!string.IsNullOrEmpty(dr["TM_003"].ToString()) && !DateTime.TryParseExact(dr["TM_003"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                        {
                            this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                            return;
                        }
                        else if (!string.IsNullOrEmpty(dr["TM_004"].ToString()) && !DateTime.TryParseExact(dr["TM_004"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                        {
                            this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                            return;
                        }

                        dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                        dr["NO_CARD"].ToString(),
                                                                        dr["DT_WORK"].ToString()));

                        if (dt != null && dt.Rows.Count > 0)
						{
                            this.ShowMessage("승인된 건이 선택되어 있습니다.");
                            return;
                        }
                    }

                    foreach (DataRow dr in dataRowArray)
					{
                        if (!string.IsNullOrEmpty(dr["TM_001"].ToString()))
						{
                            DBHelper.ExecuteNonQuery("SP_CZ_HR_WBARCODE_CONFIRM", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                 dr["NO_CARD"].ToString(),
                                                                                                 dr["DT_WORK"].ToString(),
                                                                                                 dr["TM_001"].ToString(),
                                                                                                 "001",
                                                                                                 Global.MainFrame.LoginInfo.UserID });
                        }

                        if (!string.IsNullOrEmpty(dr["TM_002"].ToString()))
                        {
                            DBHelper.ExecuteNonQuery("SP_CZ_HR_WBARCODE_CONFIRM", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                 dr["NO_CARD"].ToString(),
                                                                                                 dr["DT_WORK"].ToString(),
                                                                                                 dr["TM_002"].ToString(),
                                                                                                 "002",
                                                                                                 Global.MainFrame.LoginInfo.UserID });
                        }
                            
                        if (!string.IsNullOrEmpty(dr["TM_003"].ToString()))
						{
                            DBHelper.ExecuteNonQuery("SP_CZ_HR_WBARCODE_CONFIRM", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                 dr["NO_CARD"].ToString(),
                                                                                                 dr["DT_WORK"].ToString(),
                                                                                                 dr["TM_003"].ToString(),
                                                                                                 "003",
                                                                                                 Global.MainFrame.LoginInfo.UserID });
                        }
                        
                        if (!string.IsNullOrEmpty(dr["TM_004"].ToString()))
						{
                            DBHelper.ExecuteNonQuery("SP_CZ_HR_WBARCODE_CONFIRM", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                 dr["NO_CARD"].ToString(),
                                                                                                 dr["DT_WORK"].ToString(),
                                                                                                 dr["TM_004"].ToString(),
                                                                                                 "004",
                                                                                                 Global.MainFrame.LoginInfo.UserID });
                        }
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn승인.Text);
				}
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.InitCustomLayout();
            this.bppnl입사일자.IsNecessaryCondition = true;
            this.oneGrid2.UseCustomLayout = true;
            this.oneGrid2.InitCustomLayout();
            this.bppnl년월일.IsNecessaryCondition = true;

            string qurey = @"SELECT MIN(DT_ENTER)
                             FROM MA_EMP WITH(NOLOCK)
                             WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'" + Environment.NewLine +
                            "AND DT_ENTER <> '00000000' GROUP BY LEN(DT_ENTER) HAVING LEN(DT_ENTER) > 0";
            
            this.cur근태일자자릿수.Mask = "#,###";
            this.cur근무시간자릿수.Mask = "#,###";
            this.cur카드번호자릿수.Mask = "#,###";
            this.cur근태자릿수.Mask = "#,###";
            this.cur기기번호자릿수.Mask = "#,###";
            this.dtp입사일자.StartDateToString = D.GetString(DBHelper.ExecuteScalar(qurey));
            this.dtp입사일자.EndDateToString = this.MainFrameInterface.GetStringToday;
            this.dtp년월일.StartDateToString = Global.MainFrame.GetDateTimeToday().AddDays(-2).ToString("yyyyMMdd");
            this.dtp년월일.EndDateToString = this.MainFrameInterface.GetStringToday;

            DataSet comboData = this.GetComboData(new string[] { "S;HR_H000014",
                                                                 "N;HR_G000007",
                                                                 "N;HR_G000006",
                                                                 "N;HR_G000011" });

            this.cbo재직구분.DataSource = comboData.Tables[0];
            this.cbo재직구분.ValueMember = "CODE";
            this.cbo재직구분.DisplayMember = "NAME";

            new SetControl().SetCombobox(this.cbo리더기종류, Duzon.ERPU.HR.HR.FUNC.GetDataTableCode(new string[] { "S1 Barcode Reader", 
                                                                                                                   "Caps Barcode Reader",
                                                                                                                   "Secom Barcode Reader" }, false));

            this._flex승인일반.SetDataMap("FG1_HOLIDAY", comboData.Tables[1], "CODE", "NAME");
            this._flex승인일반.SetDataMap("TP_WEEK", comboData.Tables[2], "CODE", "NAME");
            this._flex승인일반.SetDataMap("CD_WCODE", comboData.Tables[3], "CODE", "NAME");

            this.btnFPMS데이터읽기.Visible = false;
            this.btn데이터읽기.Visible = false;
        }
        #endregion

        #region 메인버튼 이벤트
        protected override bool BeforeSearch()
        {
            try
            {
                if (this.tabControl.SelectedTab == this.tpg사원카드번호연결)
                {
                    return Checker.IsValid(this.dtp입사일자, true, this.DD("입사일자"));
                }
                else if (this.tabControl.SelectedTab == this.tpg데이터조회)
                {
                    if (!this.bpc사업장1.IsEmpty())
                        return Checker.IsValid(this.dtp년월일, true, this.DD("년월일"));

                    this.ShowMessage("WK1_004", this.lbl사업장1.Text);
                    this.bpc사업장1.Focus();

                    return false;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.DoContinue() || !this.BeforeSearch() || !this.MsgAndSave(true, false))
                    return;
                
                string name = this.tabControl.SelectedTab.Name;

                if (name == this.tpg리더기설정.Name)
                {
                    #region 리더기 설정
                    this.XmlNodeSearch();
                    this.ToolBarAddButtonEnabled = false;
                    this.ToolBarDeleteButtonEnabled = false;
                    #endregion
                }
                else if (name == this.tpg사원카드번호연결.Name)
                {
                    #region 사원번호카드연결
                    if (this._flex사원카드번호연결.DataTable != null)
                        this._flex사원카드번호연결.DataTable.Rows.Clear();

                    DataTable dataTable = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                          this.bpc사업장.QueryWhereIn_Pipe,
                                                                          this.bpc부서.QueryWhereIn_Pipe,
                                                                          this.cbo재직구분.SelectedValue.ToString(),
                                                                          this.dtp입사일자.StartDateToString,
                                                                          this.dtp입사일자.EndDateToString,
                                                                          this.LoginInfo.UserID,
                                                                          Global.SystemLanguage.MultiLanguageLpoint });

                    dataTable.Columns.Add("NO_SEQ", Type.GetType("System.Decimal"));
                    dataTable.Columns["NO_SEQ"].DefaultValue = 0;
                    dataTable.AcceptChanges();

                    this._flex사원카드번호연결.Binding = dataTable;

                    if (!this._flex사원카드번호연결.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);

                    this.btn카드정보올리기.Enabled = true;
                    this.btn사번복사.Enabled = true;
                    #endregion
                }
                else if (name == this.tpg데이터조회.Name)
                {
                    #region 데이터 조회
                    if (this._flex승인일반.DataTable != null)
                        this._flex승인일반.DataTable.Rows.Clear();

                    this._flex승인일반.Binding = this._biz.Search1(new object[] { this.LoginInfo.CompanyCode,
                                                                                    this.bpc사업장1.QueryWhereIn_Pipe,
                                                                                    this.bpc부서1.QueryWhereIn_Pipe,
                                                                                    this.bpc사원.QueryWhereIn_Pipe,
                                                                                    this.dtp년월일.StartDateToString,
                                                                                    this.dtp년월일.EndDateToString,
                                                                                    Global.SystemLanguage.MultiLanguageLpoint });
                    #endregion

                    #region 데이터 조회1
                    if (this._flex승인Pivot.DataTable != null)
                        this._flex승인Pivot.DataTable.Rows.Clear();

                    this._flex승인Pivot.Binding = this._biz.Search2(new object[] { this.LoginInfo.CompanyCode,
                                                                                     this.bpc사업장1.QueryWhereIn_Pipe,
                                                                                     this.bpc부서1.QueryWhereIn_Pipe,
                                                                                     this.bpc사원.QueryWhereIn_Pipe,
                                                                                     this.dtp년월일.StartDateToString,
                                                                                     this.dtp년월일.EndDateToString });

                    #endregion

                    #region 데이터 조회2
                    if (this._flex승인대기.DataTable != null)
                        this._flex승인대기.DataTable.Rows.Clear();

                    this._flex승인대기.Binding = this._biz.Search3(new object[] { this.LoginInfo.CompanyCode,
                                                                                     this.bpc사업장1.QueryWhereIn_Pipe,
                                                                                     this.bpc부서1.QueryWhereIn_Pipe,
                                                                                     this.bpc사원.QueryWhereIn_Pipe,
                                                                                     this.dtp년월일.StartDateToString,
                                                                                     this.dtp년월일.EndDateToString });

                    #endregion

                    if (!this._flex승인일반.HasNormalRow && !this._flex승인Pivot.HasNormalRow && !this._flex승인대기.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                string name = this.tabControl.SelectedTab.Name;

                if (name == this.tpg데이터조회.Name)
                {
                    this._flex승인일반.Rows.Add();
                    this._flex승인일반.Row = _flex승인일반.Rows.Count - 1;

                    this._flex승인일반.AddFinished();
                    this._flex승인일반.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                string name = this.tabControl.SelectedTab.Name;

                if (name == this.tpg사원카드번호연결.Name)
                {
                    #region 사원카드번호연결
                    if (!this._flex사원카드번호연결.HasNormalRow) return;

                    DataRow[] dataRowArray1 = this._flex사원카드번호연결.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);
                    if (dataRowArray1 == null || dataRowArray1.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                        return;
                    }

                    this._flex사원카드번호연결.Redraw = false;

                    foreach (DataRow dataRow in dataRowArray1)
                    {
                        dataRow.Delete();
                    }

                    this._flex사원카드번호연결.Redraw = true;
                    #endregion
                }
                else if (name == this.tpg데이터조회.Name)
                {
                    #region 데이터조회
                    if (!this._flex승인일반.HasNormalRow) return;

                    DataRow[] dataRowArray2 = this._flex승인일반.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);
                    if (dataRowArray2 == null || dataRowArray2.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다);
                        return;
                    }

                    this._flex승인일반.Redraw = false;

                    foreach (DataRow dataRow in dataRowArray2)
                    {
                        dataRow.Delete();
                    }

                    this._flex승인일반.Redraw = true;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		protected override bool BeforeSave()
		{
            foreach (DataRow row in this._flex승인Pivot.DataTable.Rows)
            {
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    if (D.GetDecimal(row["TM_001"]) >= 240000 ||
                        D.GetDecimal(row["TM_002"]) >= 240000 ||
                        D.GetDecimal(row["TM_003"]) >= 240000 ||
                        D.GetDecimal(row["TM_004"]) >= 240000)
                    {
                        this.ShowMessage("24시 넘어가는 건이 있습니다.");
                        return false;
                    }

                    DateTime result;
                    if (!string.IsNullOrEmpty(row["TM_001"].ToString()) && !DateTime.TryParseExact(row["TM_001"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
					{
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                    else if (!string.IsNullOrEmpty(row["TM_002"].ToString()) && !DateTime.TryParseExact(row["TM_002"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                    {
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                    else if (!string.IsNullOrEmpty(row["TM_003"].ToString()) && !DateTime.TryParseExact(row["TM_003"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                    {
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                    else if (!string.IsNullOrEmpty(row["TM_004"].ToString()) && !DateTime.TryParseExact(row["TM_004"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                    {
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                }
            }

            foreach (DataRow row in this._flex승인대기.DataTable.Rows)
            {
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    if (D.GetDecimal(row["TM_001"]) >= 240000 ||
                        D.GetDecimal(row["TM_002"]) >= 240000 ||
                        D.GetDecimal(row["TM_003"]) >= 240000 ||
                        D.GetDecimal(row["TM_004"]) >= 240000)
                    {
                        this.ShowMessage("24시 넘어가는 건이 있습니다.");
                        return false;
                    }

                    DateTime result;
                    if (!string.IsNullOrEmpty(row["TM_001"].ToString()) && !DateTime.TryParseExact(row["TM_001"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                    {
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                    else if (!string.IsNullOrEmpty(row["TM_002"].ToString()) && !DateTime.TryParseExact(row["TM_002"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                    {
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                    else if (!string.IsNullOrEmpty(row["TM_003"].ToString()) && !DateTime.TryParseExact(row["TM_003"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                    {
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                    else if (!string.IsNullOrEmpty(row["TM_004"].ToString()) && !DateTime.TryParseExact(row["TM_004"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                    {
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                }
            }

            foreach (DataRow row in this._flex승인일반.DataTable.Rows)
            {
                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                {
                    if (D.GetDecimal(row["TM_CARD"]) >= 240000)
                    {
                        this.ShowMessage("24시 넘어가는 건이 있습니다.");
                        return false;
                    }

                    DateTime result;
                    if (!string.IsNullOrEmpty(row["TM_CARD"].ToString()) && !DateTime.TryParseExact(row["TM_CARD"].ToString(), "HHmmss", null, System.Globalization.DateTimeStyles.None, out result))
                    {
                        this.ShowMessage("시간형식에 맞지 않는 건이 있습니다.");
                        return false;
                    }
                }
            }

            return base.BeforeSave();
		}

		public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.BeforeSave()) return;

                if (!this.DoContinue()) return;

                if (!this.IsChanged(null))
                {
                    this.ShowMessage("IK1_013");
                    this.ToolBarSaveButtonEnabled = false;
                }
                else if (this.MsgAndSave(false, false))
                {
                    this.ShowMessage("IK1_001");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool IsChanged(string gubun)
        {
            try
            {
                if (gubun == null)
                    return this.isChanged || this._flex사원카드번호연결.IsDataChanged || this._flex승인일반.IsDataChanged || this._flex승인Pivot.IsDataChanged || this._flex승인대기.IsDataChanged;
                if (gubun == "001")
                    return this.isChanged;
                if (gubun == "002")
                    return this._flex사원카드번호연결.IsDataChanged;
                if (gubun == "003")
                    return this._flex승인일반.IsDataChanged || this._flex승인Pivot.IsDataChanged || this._flex승인대기.IsDataChanged;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return false;
        }

        private bool MsgAndSave(bool displayDialog, bool isExit)
        {
            bool flag = false;

            try
            {
                if (!this.CanSave) return true;
                if (!this.IsChanged(null)) return true;

                if (!displayDialog)
                {
                    if (this.IsChanged(null))
                        flag = this.Save();
                    return flag;
                }

                if (isExit)
                {
                    this._flex사원카드번호연결.Redraw = false;
                    this._flex승인일반.Redraw = false;

                    DialogResult dialogResult = this.ShowMessage("QY3_002");
                    
                    if (dialogResult == DialogResult.No)
                    {
                        if (this.IsChanged("001"))
                            this.isChanged = false;

                        if (this.IsChanged("002"))
                            this._flex사원카드번호연결.DataTable.RejectChanges();

                        if (this.IsChanged("003"))
                            this._flex승인일반.DataTable.RejectChanges();

                        this.ToolBarSaveButtonEnabled = false;

                        return true;
                    }

                    this._flex사원카드번호연결.Redraw = true;
                    this._flex승인일반.Redraw = true;

                    if (dialogResult == DialogResult.Cancel)
                        return false;
                }
                else if (this.ShowMessage("QY2_001") == DialogResult.No)
                {
                    if (this.IsChanged("001"))
                        this.isChanged = false;

                    if (this.IsChanged("002"))
                        this._flex사원카드번호연결.DataTable.RejectChanges();

                    if (this.IsChanged("003"))
                        this._flex승인일반.DataTable.RejectChanges();

                    this.ToolBarSaveButtonEnabled = false;

                    return true;
                }

                if (this.IsChanged(null))
                    flag = this.Save();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return flag;
        }

        private bool Save()
        {
            try
            {
                if (!this.IsChanged(null)) return true;

                if (this.IsChanged("001"))
                {
                    this.SaveBarcodeFile();
                    this.isChanged = false;
                    this.ToolBarSaveButtonEnabled = false;
                }
                else if (this.IsChanged("002"))
                {
                    DataTable dt = this._flex사원카드번호연결.DataTable.GetChanges(DataRowState.Modified);

                    if (dt != null && dt.Rows.Count > 0 && this._biz.Save(dt))
                    {
                        this._flex사원카드번호연결.AcceptChanges();
                        return true;
                    }
                }
                else if (this.IsChanged("003"))
                {
                    DataTable dt = this._flex승인일반.DataTable.GetChanges();

                    if (dt != null && dt.Rows.Count > 0 && this._biz.Save1(dt))
                    {
                        foreach (DataRow row in this._flex승인일반.DataTable.Rows)
                        {
                            if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                            {
                                row["OLD_TM_CARD"] = row["TM_CARD"];
                            }
                        }

                        this._flex승인일반.AcceptChanges();
                        return true;
                    }

                    dt = this._flex승인Pivot.DataTable.GetChanges();

                    if (dt != null && dt.Rows.Count > 0 && this._biz.Save2(dt))
                    {
                        foreach (DataRow row in this._flex승인Pivot.DataTable.Rows)
                        {
                            row["TM_001_OLD"] = row["TM_001"];
                            row["TM_002_OLD"] = row["TM_002"];
                            row["TM_003_OLD"] = row["TM_003"];
                            row["TM_004_OLD"] = row["TM_004"];
                        }

                        this._flex승인Pivot.AcceptChanges();
                        return true;
                    }

                    dt = this._flex승인대기.DataTable.GetChanges();

                    if (dt != null && dt.Rows.Count > 0 && this._biz.Save3(dt))
                    {
                        foreach (DataRow row in this._flex승인대기.DataTable.Rows)
                        {
                            row["TM_001_OLD"] = row["TM_001"];
                            row["TM_002_OLD"] = row["TM_002"];
                            row["TM_003_OLD"] = row["TM_003"];
                            row["TM_004_OLD"] = row["TM_004"];
                        }

                        this._flex승인대기.AcceptChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }

            return false;
        }

        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.DoContinue())
                    return false;
                if (!this.MsgAndSave(true, true))
                    return false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            return true;
        }
        #endregion

        #region 컨트롤 이벤트
        private void btn카드정보올리기_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex사원카드번호연결.DataTable == null || !this.DoContinue() || ((bool)BasicInfo.ActiveDialog || !this.MsgAndSave(true, false)))
                    return;
                DataRow[] dataRowArray = this._flex사원카드번호연결.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray == null || dataRowArray.Length == 0)
                    this.ShowMessage("IK1_007");
                else
                {
                    this._flex사원카드번호연결.DataTable.TableName = "Table1";
                    DataView dv = new DataView();
                    dv.Table = this._flex사원카드번호연결.DataTable.Copy();
                    int num2 = 1;
                    foreach (DataRowView dataRowView in dv)
                    {
                        if (dataRowView["S"].ToString() == "Y")
                        {
                            dataRowView.BeginEdit();
                            dataRowView["NO_SEQ"] = num2;
                            dataRowView.EndEdit();
                        }
                        ++num2;
                    }
                    if (((Form)new P_HR_WBARCODE_SUB(this.MainFrameInterface, dv)).ShowDialog() == DialogResult.OK)
                        base.OnToolBarSearchButtonClicked(sender, e);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn사번복사_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex사원카드번호연결.DataTable == null || !this._flex사원카드번호연결.HasNormalRow || this.ShowMessage("HR_M500099", "QY2") == DialogResult.No)
                    return;

                for (int index = this._flex사원카드번호연결.Rows.Fixed; index < this._flex사원카드번호연결.Rows.Count; ++index)
                    this._flex사원카드번호연결[index, "NO_CARD"] = this._flex사원카드번호연결[index, "NO_EMP"];
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn데이터읽기_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MainFrameInterface.ServerKey == "SSGFOOD")
                {
                    if (this.tabControl.SelectedTab.Name == this.tpg데이터조회.Name)
                        DBHelper.ExecuteScalar("UP_HR_WBARCODE_SELECT_NEWWORLD");

                    this.ShowMessage("IK1_004", ((Control)sender).Text);
                }

                if (this.MainFrameInterface.ServerKey == "DOMINO" || this.MainFrameInterface.ServerKey == "DOMINO2")
                {
                    if (((Form)new P_CZ_HR_WTMCALC_MON_SUB("DOMINO")).ShowDialog() != DialogResult.OK)
                        ;
                }
                else
                {
                    if (this._flex승인일반.DataTable == null || !this.rdoFILE사용.Checked)
                        return;

                    if (this.rdoText탭으로분리.Checked)
                    {
                        if (this.cur근태일자탭분리위치.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl근태일자.Text + ", " + this.lbl탭분리위치.Text);
                            this.cur근태일자탭분리위치.Focus();
                            return;
                        }
                        if (this.cur근무시간탭분리위치.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl근무시간.Text + ", " + this.lbl탭분리위치.Text);
                            this.cur근무시간탭분리위치.Focus();
                            return;
                        }
                        if (this.cur카드번호탭분리위치.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl카드번호.Text + ", " + this.lbl탭분리위치.Text);
                            this.cur카드번호탭분리위치.Focus();
                            return;
                        }
                        if (this.chk근태코드존재.Checked && this.cur근태탭분리위치.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl근태.Text + ", " + this.lbl탭분리위치.Text);
                            this.cur근태탭분리위치.Focus();
                            return;
                        }
                        if (this.cur기기번호탭분리위치.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl기기번호.Text + ", " + this.lbl탭분리위치.Text);
                            this.cur기기번호탭분리위치.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (this.txt근태일자시작포인트.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl근태일자.Text + ", " + this.lbl시작포인트.Text);
                            this.txt카드번호시작포인트.Focus();
                            return;
                        }
                        if (this.cur근태일자자릿수.DecimalValue == 0)
                        {
                            this.ShowMessage("WK1_002", this.lbl근태일자.Text + ", " + this.lbl자릿수.Text);
                            this.cur근태일자자릿수.Focus();
                            return;
                        }
                        if (this.txt근무시간시작포인트.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl근무시간.Text + ", " + this.lbl시작포인트.Text);
                            this.txt근무시간시작포인트.Focus();
                            return;
                        }
                        if (this.cur근무시간자릿수.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl근무시간.Text + ", " + this.lbl자릿수.Text);
                            this.cur근무시간자릿수.Focus();
                            return;
                        }
                        if (this.txt카드번호시작포인트.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl카드번호.Text + ", " + this.lbl시작포인트.Text);
                            this.txt근무시간시작포인트.Focus();
                            return;
                        }
                        if (this.cur카드번호자릿수.DecimalValue == 0)
                        {
                            this.ShowMessage("WK1_002", this.lbl카드번호.Text + ", " + this.lbl시작포인트.Text);
                            this.cur카드번호자릿수.Focus();
                            return;
                        }
                        if (this.chk근태코드존재.Checked)
                        {
                            if (this.txt근태시작포인트.Text == string.Empty)
                            {
                                this.ShowMessage("WK1_002", this.lbl근태.Text + ", " + this.lbl시작포인트.Text);
                                this.txt근태시작포인트.Focus();
                                return;
                            }
                            if (this.cur근태자릿수.DecimalValue == 0)
                            {
                                this.ShowMessage("WK1_002", this.lbl근태.Text + ", " + this.lbl시작포인트.Text);
                                this.cur근태자릿수.Focus();
                                return;
                            }
                        }
                        if (this.txt기기번호시작포인트.Text == string.Empty)
                        {
                            this.ShowMessage("WK1_002", this.lbl기기번호.Text + ", " + this.lbl시작포인트.Text);
                            this.txt기기번호시작포인트.Focus();
                            return;
                        }
                        if (this.cur기기번호자릿수.DecimalValue == 0)
                        {
                            this.ShowMessage("WK1_002", this.lbl기기번호.Text + ", " + this.lbl시작포인트.Text);
                            this.cur기기번호자릿수.Focus();
                            return;
                        }
                    }

                    DataRow[] dataRowArray1 = null;

                    if (this._flex승인일반.HasNormalRow)
                        dataRowArray1 = this._flex승인일반.DataTable.Select("S = 'Y'");

                    string str1;
                    if (dataRowArray1 == null || dataRowArray1.Length == 0)
                        str1 = this.ShowMessage(this.DD("기 등록된 데이터를 삭제 후 다시 반영하시겠습니까?\n아니오를 선택시 기 등록된 데이터는 변경되지 않고 신규데이터만 반영 됩니다."), "QY2") != DialogResult.Yes ? "SKIP&INSERT" : "DELETE&INSERT";
                    else
                    {
                        if (this.ShowMessage(this.DD("신규 리더기 데이터가 반영되며\n체크된 데이터는 삭제 후 다시 반영됩니다.\n계속하시겠습니까?"), "QY2") != DialogResult.Yes)
                            return;
                        str1 = "CHKDELETE&INSERT";
                    }

                    string empty1 = string.Empty;
                    string empty2 = string.Empty;
                    string empty3 = string.Empty;
                    string empty4 = string.Empty;
                    string empty5 = string.Empty;

                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.InitialDirectory = "C:\\BarcodeData";
                        openFileDialog.RestoreDirectory = true;

                        if (MA.ServerKey(false, new string[] { "DONGBO" }))
                            openFileDialog.Filter = "Data Files(*.txt)| *.txt| Data Files(*.dat)| *.dat";
                        else
                            openFileDialog.Filter = "Data Files(*.txt)| *.txt";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            StreamReader streamReader = File.OpenText(openFileDialog.FileName);
                            MsgControl.ShowMsg("데이터 자료를 읽고 있습니다.\n잠시만 기다려 주십시요.");
                            DataTable dataTable = this._flex승인일반.DataTable.Copy();

                            switch (str1)
                            {
                                case "DELETE&INSERT":
                                    IEnumerator enumerator = dataTable.Rows.GetEnumerator();
                                    try
                                    {
                                        while (enumerator.MoveNext())
                                            ((DataRow)enumerator.Current).Delete();
                                        break;
                                    }
                                    finally
                                    {
                                        IDisposable disposable = enumerator as IDisposable;
                                        if (disposable != null)
                                            disposable.Dispose();
                                    }
                                case "CHKDELETE&INSERT":
                                    foreach (DataRow dataRow in dataTable.Select("S = 'Y'"))
                                        dataRow.Delete();
                                    break;
                            }

                            string psData;

                            while ((psData = streamReader.ReadLine()) != null)
                            {
                                string str2;
                                string str3;
                                string str4;
                                string str5;

                                if (this.rdoText탭으로분리.Checked)
                                {
                                    str2 = this.DataTabSplit(Convert.ToInt32((this.cur카드번호탭분리위치).Text), psData);
                                    str3 = this.DataTabSplit(Convert.ToInt32((this.cur근태일자탭분리위치).Text), psData);
                                    str4 = this.DataTabSplit(Convert.ToInt32((this.cur근무시간탭분리위치).Text), psData);
                                    str5 = this.DataTabSplit((this.cur근태탭분리위치).Text == string.Empty ? 0 : Convert.ToInt32((this.cur근태탭분리위치).Text), psData);
                                }
                                else
                                {
                                    str2 = this.DataPointSplit(Convert.ToInt32((this.txt카드번호시작포인트).Text), Convert.ToInt32(this.cur카드번호자릿수.DecimalValue), psData);
                                    str3 = this.DataPointSplit(Convert.ToInt32((this.txt근태일자시작포인트).Text), Convert.ToInt32(this.cur근태일자자릿수.DecimalValue), psData);
                                    str4 = this.DataPointSplit(Convert.ToInt32((this.txt근무시간시작포인트).Text), Convert.ToInt32(this.cur근무시간자릿수.DecimalValue), psData);
                                    str5 = !((this.txt근태시작포인트).Text == string.Empty) && !(this.cur근태자릿수.DecimalValue == 0) ? this.DataPointSplit(Convert.ToInt32((this.txt근태시작포인트).Text), Convert.ToInt32(this.cur근태자릿수.DecimalValue), psData) : string.Empty;
                                }

                                if (this.chk근태코드존재.Checked)
                                {
                                    if (str5 == this.txt근태코드연결출근.Text)
                                        str5 = "001";
                                    else if (str5 == this.txt근태코드연결퇴근.Text)
                                        str5 = "002";
                                    else if (str5 == this.txt근태코드연결외출.Text)
                                        str5 = "003";
                                    else if (str5 == this.txt근태코드연결복귀.Text)
                                        str5 = "004";
                                }

                                DataRow[] dataRowArray2 = dataTable.Select("NO_CARD = '" + str2 + "' AND DT_WORK = '" + str3 + "' AND TM_CARD = '" + str4 + "'", "", DataViewRowState.CurrentRows);
                                if (dataRowArray2 == null || dataRowArray2.Length == 0)
                                {
                                    DataRow row = dataTable.NewRow();
                                    row["NO_CARD"] = str2;
                                    row["DT_WORK"] = str3;
                                    row["TM_CARD"] = str4.PadRight(6, Convert.ToChar("0"));
                                    row["CD_WCODE"] = str5;
                                    dataTable.Rows.Add(row);
                                }
                            }
                            try
                            {
                                streamReader.Close();
                                streamReader.Dispose();
                            }
                            catch
                            {
                            }

                            MsgControl.CloseMsg();
                            this._flex승인일반.Binding = dataTable;
                            this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { "데이터 읽기" });
                            MsgControl.ShowMsg("자료를 저장중 입니다.\n잠시만 기다려 주십시요.");

                            if (this.Save())
                            {
                                MsgControl.CloseMsg();
                                base.OnToolBarSearchButtonClicked(sender, e);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex승인일반.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        private void btnFPMS데이터읽기_Click(object sender, EventArgs e)
        {
            if (((Form)new P_CZ_HR_WTMCALC_MON_SUB(D.GetString(this.cbo리더기종류.SelectedValue))).ShowDialog() != DialogResult.OK)
                ;
        }

        private void btnCaps데이터읽기_Click(object sender, EventArgs e)
        {
            try
            {
                if (((Form)new P_CZ_HR_WTMCALC_MON_SUB(D.GetString(this.cbo리더기종류.SelectedValue))).ShowDialog() != DialogResult.OK)
                    ;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnSecom데이터읽기_Click(object sender, EventArgs e)
        {
            try
            {
                if (((Form)new P_CZ_HR_WTMCALC_MON_SUB(D.GetString(this.cbo리더기종류.SelectedValue))).ShowDialog() != DialogResult.OK)
                    ;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_AfterDataRefresh(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (this.IsChanged((string)null))
                    this.ToolBarSaveButtonEnabled = true;
                else
                    this.ToolBarSaveButtonEnabled = false;

                string name = this.tabControl.SelectedTab.Name;

                if (name == this.tpg리더기설정.Name)
                {
                    this.ToolBarAddButtonEnabled = false;
                    this.ToolBarDeleteButtonEnabled = false;
                }
                else if (name == this.tpg사원카드번호연결.Name)
                {
                    if (!this._flex사원카드번호연결.HasNormalRow)
                        this.ToolBarDeleteButtonEnabled = false;
                    this.ToolBarDeleteButtonEnabled = true;
                }
                else if (name == this.tpg데이터조회.Name)
                {
                    if (!this._flex승인일반.HasNormalRow)
                        this.ToolBarDeleteButtonEnabled = false;
                    this.ToolBarDeleteButtonEnabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (e.Checkbox == 0)
                {
                    string str = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                    string editData = ((Dass.FlexGrid.FlexGrid)sender).EditData;
                    if (str == editData)
                        return;

                    string name = ((Control)sender).Name;

                    if (name == _flex사원카드번호연결.Name)
                    {
                        if (this._flex사원카드번호연결.Cols[e.Col].Name == "DT_START" || this._flex사원카드번호연결.Cols[e.Col].Name == "DT_CLOSE")
                        {
                            if (!this._flex사원카드번호연결.IsDate(this._flex사원카드번호연결.Cols[e.Col].Name))
                            {
                                this.ShowMessage("WK1_003", this._flex사원카드번호연결.Cols[e.Col].Caption);
                                this._flex사원카드번호연결[e.Row, e.Col] = string.Empty;
                                if (this._flex사원카드번호연결.Editor != null)
                                    this._flex사원카드번호연결.Editor.Text = string.Empty;
                                e.Cancel = true;
                            }
                            if (this._flex사원카드번호연결[e.Row, "DT_START"].ToString() != string.Empty && this._flex사원카드번호연결[e.Row, "DT_CLOSE"].ToString() != string.Empty && new CommonFunction().DiffDate(this._flex사원카드번호연결[e.Row, "DT_START"].ToString(), this._flex사원카드번호연결[e.Row, "DT_CLOSE"].ToString()) > 0)
                            {
                                this.ShowMessage("WK1_007");
                                this._flex사원카드번호연결["DT_START"] = string.Empty;
                                this._flex사원카드번호연결["DT_CLOSE"] = string.Empty;
                                if (this._flex사원카드번호연결.Editor != null)
                                    this._flex사원카드번호연결.Editor.Text = string.Empty;
                                e.Cancel = true;
                                return;
                            }
                            return;
                        }
                        if (this._flex사원카드번호연결.Cols[e.Col].Name == "NO_CARD" && (editData != null && editData != string.Empty && editData != ""))
                        {
                            DataTable dataTable = DBHelper.GetDataTable("UP_HR_WBARCODE_CHECK", new object[] { this.LoginInfo.CompanyCode,
                                                                                                               editData });
                            if (dataTable != null && dataTable.Rows.Count > 0)
                            {
                                this.ShowMessage("카드번호가 중복됩니다.");
                                this._flex사원카드번호연결.Editor.Text = string.Empty;
                                e.Cancel = true;
                                return;
                            }
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo리더기종류_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                switch (D.GetString(this.cbo리더기종류.SelectedValue))
                {
                    case "001":
                        this.btnFPMS데이터읽기.Visible = false;
                        if ((Global.MainFrame.ServerKey == "LGDISP" || Global.MainFrame.ServerKey == "LGDISP2") && this.LoginInfo.CompanyCode == "4000" || (this.LoginInfo.CompanyCode == "9400" || Global.MainFrame.ServerKey == "LHE" || Global.MainFrame.ServerKey == "DZSQL") || Global.MainFrame.ServerKey == "SQL_108")
                            this.btnFPMS데이터읽기.Visible = true;
                        this.btnCaps데이터읽기.Visible = false;
                        this.btnSecom데이터읽기.Visible = false;
                        break;
                    case "002":
                        this.btnFPMS데이터읽기.Visible = false;
                        this.btnCaps데이터읽기.Visible = true;
                        this.btnSecom데이터읽기.Visible = false;
                        break;
                    case "003":
                        this.btnFPMS데이터읽기.Visible = false;
                        this.btnCaps데이터읽기.Visible = false;
                        this.btnSecom데이터읽기.Visible = true;
                        this.btnSecom데이터읽기.Location = this.btnCaps데이터읽기.Location;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnControlCheck(object sender, EventArgs e)
        {
            try
            {
                if (!((DatePicker)sender).Modified || ((Control)sender).Text == string.Empty || ((DatePicker)sender).IsValidated)
                    return;
                this.ShowMessage("WK1_003", (this.lbl입사일자).Text);
                ((Control)sender).Text = string.Empty;
                ((Control)sender).Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnControlValidated(object sender, EventArgs e)
        {
            try
            {
                switch ((sender).GetType().Name)
                {
                    case "CheckBoxExt":
                        if (((Control)sender).Name == "CHK1_WORK")
                        {
                            if (this.chk근태코드존재.Checked)
                            {
                                this.txt근태코드연결출근.Enabled = true;
                                this.txt근태코드연결퇴근.Enabled = true;
                                this.txt근태코드연결외출.Enabled = true;
                                this.txt근태코드연결복귀.Enabled = true;
                            }
                            else
                            {
                                this.txt근태코드연결출근.Enabled = false;
                                this.txt근태코드연결퇴근.Enabled = false;
                                this.txt근태코드연결외출.Enabled = false;
                                this.txt근태코드연결복귀.Enabled = false;
                            }
                            break;
                        }
                        break;
                    case "DropDownComboBox":
                        if (!((DropDownComboBox)sender).Modified)
                            return;
                        if (((Control)sender).Name == this.cbo리더기종류.Name)
                        {
                            if (this.cbo리더기종류.SelectedIndex == 0 || this.cbo리더기종류.SelectedIndex == 1)
                            {
                                this.rdoText일반.Checked = true;
                                this.rdoText탭으로분리.Checked = false;
                            }
                            else if (this.cbo리더기종류.SelectedIndex == 2)
                            {
                                this.rdoText일반.Checked = false;
                                this.rdoText탭으로분리.Checked = true;
                            }
                            if (D.GetString(this.cbo리더기종류.SelectedValue) == "001" || D.GetString(this.cbo리더기종류.SelectedValue) == "002")
                                this.btn카드정보올리기.Visible = true;
                            else
                                this.btn카드정보올리기.Visible = false;
                            break;
                        }
                        break;
                    case "RadioButtonExt":
                        string name = ((Control)sender).Name;

                        if (name == rdo리더기사용.Name)
                        {
                            this.OnControlsEnabled(true);
                            this.OnTextControlEnabled(false);
                            this.OnTabPositionControlsEnabled(false);
                            this.chk근태코드존재.Enabled = false;
                            this.rdoText일반.Enabled = false;
                            this.rdoText탭으로분리.Enabled = false;
                        }
                        else if (name == rdoFILE사용.Name)
                        {
                            this.cbo리더기종류.SelectedIndex = 0;
                            this.OnControlsEnabled(false);
                            if (this.rdoText일반.Checked)
                            {
                                this.OnTextControlEnabled(true);
                                this.OnTabPositionControlsEnabled(false);
                            }
                            else if (this.rdoText탭으로분리.Checked)
                            {
                                this.OnTextControlEnabled(false);
                                this.OnTabPositionControlsEnabled(true);
                            }
                            this.chk근태코드존재.Enabled = true;
                            this.rdoText일반.Enabled = true;
                            this.rdoText탭으로분리.Enabled = true;
                        }
                        else if (name == rdoText일반.Name)
                        {
                            this.OnTextControlEnabled(true);
                            this.OnTabPositionControlsEnabled(false);
                        }
                        else if (name == rdoText탭으로분리.Name)
                        {
                            this.OnTextControlEnabled(false);
                            this.OnTabPositionControlsEnabled(true);
                        }
                        break;
                    case "TextBoxExt":
                        if (!((TextBoxExt)sender).Modified)
                            return;
                        break;
                    case "CurrencyTextBox":
                        if (!((NumberTextBoxBase)sender).Modified)
                            return;
                        break;
                    case "NumericUpDownExt":
                        if (!((NumericUpDownExt)sender).Modified)
                            return;
                        break;
                    case "MaskedEditBox":
                        if (!((MaskedEditBox)sender).Modified)
                            return;
                        break;
                }

                this.isChanged = true;
                this.ToolBarSaveButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.MsgAndSave(true, false))
                    return;

                string name = this.tabControl.SelectedTab.Name;

                if (name == this.tpg리더기설정.Name)
                {
                    this.XmlNodeSearch();
                    this.ToolBarAddButtonEnabled = false;
                    this.ToolBarDeleteButtonEnabled = false;
                }
                else
                    this.XmlNodeSearch();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_Before(object sender, BpQueryArgs e)
        {
            try
            {
                string name = ((Control)sender).Name;

                if (name == bpc부서.Name)
                {
                    if (!this.bpc사업장.IsEmpty())
                        e.HelpParam.P26_AUTH_BIZAREA = this.bpc사업장.QueryWhereIn_Pipe;
                }
                else if (name == bpc부서1.Name)
                {
                    if (this.bpc사업장1.IsEmpty())
                    {
                        this.ShowMessage("WK1_004", this.lbl사업장1.Text);
                        this.bpc사업장1.Focus();
                        e.QueryCancel = true;
                        return;
                    }

                    e.HelpParam.P26_AUTH_BIZAREA = this.bpc사업장1.QueryWhereIn_Pipe;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.DialogResult != DialogResult.OK)
                    return;

                string name = ((Control)sender).Name;

                if (name == bpc사업장.Name)
                    this.bpc부서.Clear();
                else if (name == bpc사업장1.Name)
                    this.bpc부서1.Clear();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private void XmlNodeSearch()
        {
            try
            {
                Debug.WriteLine("서버정보불러오기시작");
                this.OpenServerFile();
                string filename = Application.StartupPath + "\\download\\Barcode" + "\\" + (Global.MainFrame.LoginInfo.CompanyCode + "_Barcode.xml");
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filename);
                xmlDocument.GetElementsByTagName("READER_USES");
                xmlDocument.GetElementsByTagName("FILE_USES");
                XmlNodeList elementsByTagName1 = xmlDocument.GetElementsByTagName("TM_START");
                XmlNodeList elementsByTagName2 = xmlDocument.GetElementsByTagName("TM_CLOSE");
                XmlNodeList elementsByTagName3 = xmlDocument.GetElementsByTagName("TM_OUT");
                XmlNodeList elementsByTagName4 = xmlDocument.GetElementsByTagName("TM_IN");
                XmlNodeList elementsByTagName5 = xmlDocument.GetElementsByTagName("WCODE_ALLIVE");
                XmlNodeList elementsByTagName6 = xmlDocument.GetElementsByTagName("TXT_FORMAT");
                XmlNodeList elementsByTagName7 = xmlDocument.GetElementsByTagName("FILEDB");
                XmlNodeList elementsByTagName8 = xmlDocument.GetElementsByTagName("PORT");
                XmlNodeList elementsByTagName9 = xmlDocument.GetElementsByTagName("CONNECT");
                XmlNodeList elementsByTagName10 = xmlDocument.GetElementsByTagName("CONNECT_COUNT");
                XmlNodeList elementsByTagName11 = xmlDocument.GetElementsByTagName("READER_COLLECTION");
                XmlNodeList elementsByTagName12 = xmlDocument.GetElementsByTagName("FORMAT");
                XmlNodeList elementsByTagName13 = xmlDocument.GetElementsByTagName("WCODEDATE");
                XmlNodeList elementsByTagName14 = xmlDocument.GetElementsByTagName("WCODEDATE_LENTH");
                XmlNodeList elementsByTagName15 = xmlDocument.GetElementsByTagName("WORKTIME");
                XmlNodeList elementsByTagName16 = xmlDocument.GetElementsByTagName("WORKTIME_LENTH");
                XmlNodeList elementsByTagName17 = xmlDocument.GetElementsByTagName("CARDNUMBER");
                XmlNodeList elementsByTagName18 = xmlDocument.GetElementsByTagName("CARDNUMBER_LENTH");
                XmlNodeList elementsByTagName19 = xmlDocument.GetElementsByTagName("WCODE");
                XmlNodeList elementsByTagName20 = xmlDocument.GetElementsByTagName("WCODE_LENTH");
                XmlNodeList elementsByTagName21 = xmlDocument.GetElementsByTagName("MACHINE");
                XmlNodeList elementsByTagName22 = xmlDocument.GetElementsByTagName("MACHINE_LENTH");
                XmlNodeList elementsByTagName23 = xmlDocument.GetElementsByTagName("WCODEDATE_TABPOSITION");
                XmlNodeList elementsByTagName24 = xmlDocument.GetElementsByTagName("WORKTIME_TABPOSITION");
                XmlNodeList elementsByTagName25 = xmlDocument.GetElementsByTagName("CARDNUMBER_TABPOSITION");
                XmlNodeList elementsByTagName26 = xmlDocument.GetElementsByTagName("WCODE_TABPOSITION");
                XmlNodeList elementsByTagName27 = xmlDocument.GetElementsByTagName("MACHINE_TABPOSITION");
                XmlNodeList elementsByTagName28 = xmlDocument.GetElementsByTagName("TM_INOUT");
                XmlNodeList elementsByTagName29 = xmlDocument.GetElementsByTagName("TM_IND");
                XmlNodeList elementsByTagName30 = xmlDocument.GetElementsByTagName("TM_OUTD");
                xmlDocument.GetElementsByTagName("TM_CHK");
                this.rdo리더기사용.Checked = false;
                this.rdoFILE사용.Checked = true;
                this.chk근태코드존재.Checked = Convert.ToBoolean(elementsByTagName5[0].InnerText);
                this.rdoText일반.Checked = Convert.ToBoolean(elementsByTagName6[0].InnerText);
                this.rdoText탭으로분리.Checked = Convert.ToBoolean(elementsByTagName7[0].InnerText);
                this.txt근태코드연결출근.Text = elementsByTagName1[0].InnerText;
                this.txt근태코드연결퇴근.Text = elementsByTagName2[0].InnerText;
                this.txt근태코드연결외출.Text = elementsByTagName3[0].InnerText;
                this.txt근태코드연결복귀.Text = elementsByTagName4[0].InnerText;
                this.txt근태일자시작포인트.Text = elementsByTagName13[0].InnerText;
                this.cur근태일자자릿수.Text = elementsByTagName14[0].InnerText;
                this.cur근태일자탭분리위치.Text = elementsByTagName23[0].InnerText;
                this.txt근무시간시작포인트.Text = elementsByTagName15[0].InnerText;
                this.cur근무시간자릿수.Text = elementsByTagName16[0].InnerText;
                this.cur근무시간탭분리위치.Text = elementsByTagName24[0].InnerText;
                this.txt카드번호시작포인트.Text = elementsByTagName17[0].InnerText;
                this.cur카드번호자릿수.Text = elementsByTagName18[0].InnerText;
                this.cur카드번호탭분리위치.Text = elementsByTagName25[0].InnerText;
                this.txt근태시작포인트.Text = elementsByTagName19[0].InnerText;
                this.cur근태자릿수.Text = elementsByTagName20[0].InnerText;
                this.cur근태탭분리위치.Text = elementsByTagName26[0].InnerText;
                this.txt기기번호시작포인트.Text = elementsByTagName21[0].InnerText;
                this.cur기기번호자릿수.Text = elementsByTagName22[0].InnerText;
                this.cur기기번호탭분리위치.Text = elementsByTagName27[0].InnerText;
                this.cbo포트지정.Text = elementsByTagName8[0].InnerText;
                this.cbo통신속도.Text = elementsByTagName9[0].InnerText;
                this.nud연결대수.Value = Convert.ToDecimal(elementsByTagName10[0].InnerText);
                this.cbo리더기종류.SelectedValue = elementsByTagName11[0].InnerText;
                this.txtFORMAT지정1.Text = elementsByTagName12[0].InnerText;
                if (elementsByTagName28[0] != null)
                    this.msk출근퇴근.Text = elementsByTagName28[0].InnerText;
                if (elementsByTagName29[0] != null)
                    this.msk출근출근.Text = elementsByTagName29[0].InnerText;
                if (elementsByTagName30[0] != null)
                    this.msk퇴근퇴근.Text = elementsByTagName30[0].InnerText;
                if (this.rdo리더기사용.Checked)
                {
                    this.OnControlsEnabled(true);
                    this.OnTextControlEnabled(false);
                    this.OnTabPositionControlsEnabled(false);
                    this.chk근태코드존재.Enabled = false;
                    this.rdoText일반.Enabled = false;
                    this.rdoText탭으로분리.Enabled = false;
                    this.btn데이터읽기.Enabled = false;
                }
                else if (this.rdoFILE사용.Checked)
                {
                    this.OnControlsEnabled(false);
                    this.btn데이터읽기.Enabled = true;
                    
                    if (this.chk근태코드존재.Checked)
                    {
                        this.txt근태코드연결출근.Enabled = true;
                        this.txt근태코드연결퇴근.Enabled = true;
                        this.txt근태코드연결외출.Enabled = true;
                        this.txt근태코드연결복귀.Enabled = true;
                    }
                    else
                    {
                        this.txt근태코드연결출근.Enabled = false;
                        this.txt근태코드연결퇴근.Enabled = false;
                        this.txt근태코드연결외출.Enabled = false;
                        this.txt근태코드연결복귀.Enabled = false;
                    }

                    if (this.rdoText일반.Checked)
                    {
                        this.OnTextControlEnabled(true);
                        this.OnTabPositionControlsEnabled(false);
                    }
                    else if (this.rdoText탭으로분리.Checked)
                    {
                        this.OnTextControlEnabled(false);
                        this.OnTabPositionControlsEnabled(true);
                    }

                    this.chk근태코드존재.Enabled = true;
                    this.rdoText일반.Enabled = true;
                    this.rdoText탭으로분리.Enabled = true;
                }

                if (D.GetString(this.cbo리더기종류.SelectedValue) == "001")
                {
                    this.btn카드정보올리기.Visible = true;
                    this.btnCaps데이터읽기.Visible = false;
                    this.btnSecom데이터읽기.Visible = false;
                }
                else if (D.GetString(this.cbo리더기종류.SelectedValue) == "002")
                {
                    this.btn카드정보올리기.Visible = false;
                    this.btnCaps데이터읽기.Visible = true;
                    this.btnSecom데이터읽기.Visible = false;
                }
                else if (D.GetString(this.cbo리더기종류.SelectedValue) == "003")
                {
                    this.btn카드정보올리기.Visible = false;
                    this.btnCaps데이터읽기.Visible = false;
                    this.btnSecom데이터읽기.Visible = true;
                }

                this.ToolBarSaveButtonEnabled = false;
                this.isChanged = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateFileInfo()
        {
            try
            {
                string path = Application.StartupPath + "\\download\\Barcode";
                string str1 = Global.MainFrame.LoginInfo.CompanyCode + "_Barcode.xml";
                string str2 = path + "\\" + str1;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (File.Exists(str2))
                    File.Delete(str2);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(str2, Encoding.UTF8);
                xmlTextWriter.Formatting = Formatting.Indented;
                xmlTextWriter.WriteStartDocument(true);
                xmlTextWriter.WriteStartElement("READERGI", "www.alpineskihouse.com");
                xmlTextWriter.WriteStartElement("ReaderSetUp", "www.alpineskihouse.com");
                xmlTextWriter.WriteStartElement("READER_USES");
                xmlTextWriter.WriteString(Convert.ToString(this.rdo리더기사용.Checked));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("FILE_USES");
                xmlTextWriter.WriteString(Convert.ToString(this.rdoFILE사용.Checked));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WcodeConnect", "www.alpineskihouse.com");
                xmlTextWriter.WriteStartElement("TM_START");
                xmlTextWriter.WriteString(this.txt근태코드연결출근.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("TM_CLOSE");
                xmlTextWriter.WriteString(this.txt근태코드연결퇴근.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("TM_OUT");
                xmlTextWriter.WriteString(this.txt근태코드연결외출.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("TM_IN");
                xmlTextWriter.WriteString(this.txt근태코드연결복귀.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("FileFormat", "www.alpineskihouse.com");
                xmlTextWriter.WriteStartElement("WCODE_ALLIVE");
                xmlTextWriter.WriteString(Convert.ToString(this.chk근태코드존재.Checked));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("TXT_FORMAT");
                xmlTextWriter.WriteString(Convert.ToString(this.rdoText일반.Checked));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("FILEDB");
                xmlTextWriter.WriteString(Convert.ToString(this.rdoText탭으로분리.Checked));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("ReadergiFormat", "www.alpineskihouse.com");
                xmlTextWriter.WriteStartElement("PORT");
                xmlTextWriter.WriteString(Convert.ToString(this.cbo포트지정.Text));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("CONNECT");
                xmlTextWriter.WriteString(Convert.ToString(this.cbo통신속도.Text));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("CONNECT_COUNT");
                xmlTextWriter.WriteString(Convert.ToString(this.nud연결대수.Value));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("READER_COLLECTION");
                xmlTextWriter.WriteString(D.GetString(this.cbo리더기종류.SelectedValue));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("FORMAT");
                xmlTextWriter.WriteString(this.txtFORMAT지정1.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("FileFormat", "www.alpineskihouse.com");
                xmlTextWriter.WriteStartElement("WCODEDATE");
                xmlTextWriter.WriteString(this.txt근태일자시작포인트.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WCODEDATE_LENTH");
                xmlTextWriter.WriteString(this.cur근태일자자릿수.ClipText);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WORKTIME");
                xmlTextWriter.WriteString(this.txt근무시간시작포인트.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WORKTIME_LENTH");
                xmlTextWriter.WriteString(this.cur근무시간자릿수.ClipText);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("CARDNUMBER");
                xmlTextWriter.WriteString(this.txt카드번호시작포인트.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("CARDNUMBER_LENTH");
                xmlTextWriter.WriteString(this.cur카드번호자릿수.ClipText);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WCODE");
                xmlTextWriter.WriteString(this.txt근태시작포인트.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WCODE_LENTH");
                xmlTextWriter.WriteString(this.cur근태자릿수.ClipText);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("MACHINE");
                xmlTextWriter.WriteString(this.txt기기번호시작포인트.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("MACHINE_LENTH");
                xmlTextWriter.WriteString(this.cur기기번호자릿수.ClipText);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WCODEDATE_TABPOSITION");
                xmlTextWriter.WriteString(this.cur근태일자탭분리위치.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WORKTIME_TABPOSITION");
                xmlTextWriter.WriteString(this.cur근무시간탭분리위치.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("CARDNUMBER_TABPOSITION");
                xmlTextWriter.WriteString(this.cur카드번호탭분리위치.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("WCODE_TABPOSITION");
                xmlTextWriter.WriteString(this.cur근태탭분리위치.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("MACHINE_TABPOSITION");
                xmlTextWriter.WriteString(this.cur기기번호탭분리위치.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("BARCODE_TIME", "www.alpineskihouse.com");
                xmlTextWriter.WriteStartElement("TM_INOUT");
                xmlTextWriter.WriteString(this.msk출근퇴근.Text.Replace("__", ""));
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("TM_IND");
                xmlTextWriter.WriteString(this.msk출근출근.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("TM_OUTD");
                xmlTextWriter.WriteString(this.msk퇴근퇴근.Text);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("TM_CHK");
                if (this.chk시간.Checked)
                    xmlTextWriter.WriteString("Y");
                else
                    xmlTextWriter.WriteString("N");
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("Modified", "www.alpineskihouse.com");
                xmlTextWriter.WriteStartElement("ID_UPDATE");
                xmlTextWriter.WriteString(Global.MainFrame.LoginInfo.UserID);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteStartElement("DTS_UPDATE");
                xmlTextWriter.WriteString(Global.MainFrame.GetStringDetailToday);
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndElement();
                xmlTextWriter.WriteEndDocument();
                xmlTextWriter.Flush();
                xmlTextWriter.Close();
                this.ShowMessage("바코드설정 파일이 생성되었습니다.");
            }
            catch (XmlException ex)
            {
                this.MsgEnd((Exception)ex);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OpenServerFile()
        {
            string path = Application.StartupPath + "\\download\\Barcode";
            string str1 = Global.MainFrame.LoginInfo.CompanyCode + "_Barcode.xml";
            string str2 = "Barcode.xml";
            string fileName = path + "\\" + str1;
            string hostUrl = this.MainFrameInterface.HostURL;
            string str3 = "/shared/Barcode";
            string str4 = hostUrl + str3 + "\\" + str1;
            WebClient webClient = new WebClient();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
                directoryInfo.Create();
            try
            {
                Debug.WriteLine("서버다운로드 : " + hostUrl + str3 + "/" + str1);
                webClient.DownloadFile(hostUrl + str3 + "\\" + str1, fileName);
            }
            catch
            {
                Debug.WriteLine("회사별파일이 없습니다. 기존파일을  불러옵니다.");
                try
                {
                    Debug.WriteLine("서버다운로드 : " + hostUrl + str3 + "/" + str2);
                    webClient.DownloadFile(hostUrl + str3 + "\\" + str2, fileName);
                }
                catch
                {
                    Debug.WriteLine("서버다운로드 에러시 루틴");
                    MsgControl.ShowMsg("바코드설정 파일이 존재하지 않습니다.\n파일을 생성합니다.");
                    Thread.Sleep(500);
                    this.CreateFileInfo();
                    string address = this.MainFrameInterface.HostURL + "/admin/FileUploader.aspx" + "?File=" + str1 + "&Dir=" + str3 + "&Action=etc&date=20050817";
                    Console.WriteLine("\nResponse Received.The contents of the file uploaded are: \n{0}", Encoding.ASCII.GetString(webClient.UploadFile(address, "POST", fileName)));
                    this.ShowMessage("HR_M500154");
                }
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        private void SaveBarcodeFile()
        {
            this.CreateFileInfo();
            string str1 = Application.StartupPath + "\\download\\Barcode";
            string str2 = Global.MainFrame.LoginInfo.CompanyCode + "_Barcode.xml";
            string fileName = str1 + "\\" + str2;
            string str3 = "/shared/Barcode";
            Console.WriteLine("\nResponse Received.The contents of the file uploaded are: \n{0}", Encoding.ASCII.GetString(new WebClient().UploadFile(this.MainFrameInterface.HostURL + "/admin/FileUploader.aspx" + "?File=" + str2 + "&Dir=" + str3 + "&Action=etc&date=20050817", "POST", fileName)));
            this.ShowMessage("HR_M500155");
        }

        private bool DoContinue()
        {
            if (this._flex사원카드번호연결.Editor != null)
                return this._flex사원카드번호연결.FinishEditing(false);
            if (this._flex승인일반.Editor != null)
                return this._flex승인일반.FinishEditing(false);
            return true;
        }

        private void OnControlsEnabled(bool boolen)
        {
            this.txt근태코드연결출근.Enabled = boolen;
            this.txt근태코드연결퇴근.Enabled = boolen;
            this.txt근태코드연결외출.Enabled = boolen;
            this.txt근태코드연결복귀.Enabled = boolen;
            this.cbo포트지정.Enabled = boolen;
            this.cbo통신속도.Enabled = boolen;
            this.nud연결대수.Enabled = boolen;
            this.txtFORMAT지정1.Enabled = boolen;
            this.txtFORMAT지정.Enabled = boolen;
        }

        private void OnTextControlEnabled(bool boolentxt)
        {
            this.txt근태일자시작포인트.Enabled = boolentxt;
            this.cur근태일자자릿수.Enabled = boolentxt;
            this.txt근무시간시작포인트.Enabled = boolentxt;
            this.cur근무시간자릿수.Enabled = boolentxt;
            this.txt카드번호시작포인트.Enabled = boolentxt;
            this.cur카드번호자릿수.Enabled = boolentxt;
            this.txt근태시작포인트.Enabled = boolentxt;
            this.cur근태자릿수.Enabled = boolentxt;
            this.txt기기번호시작포인트.Enabled = boolentxt;
            this.cur기기번호자릿수.Enabled = boolentxt;
        }

        private void OnTabPositionControlsEnabled(bool boolenvalue)
        {
            this.cur근태일자탭분리위치.Enabled = boolenvalue;
            this.cur근무시간탭분리위치.Enabled = boolenvalue;
            this.cur카드번호탭분리위치.Enabled = boolenvalue;
            this.cur근태탭분리위치.Enabled = boolenvalue;
            this.cur기기번호탭분리위치.Enabled = boolenvalue;
        }

        public static string NextToken(StreamReader stream)
        {
            try
            {
                int num = stream.Read();
                string str = "";
                for (; num != -1 && num != 124; num = stream.Read())
                    str += num.ToString();
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateRow(DataRow row, string psCdWCode)
        {
            try
            {
                if (!(row["CD_WCODE"].ToString() != psCdWCode))
                    return;
                row.BeginEdit();
                row["CD_WCODE"] = psCdWCode;
                row.EndEdit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string DataTabSplit(int piTabIndex, string psData)
        {
            try
            {
                string str = string.Empty;
                for (int index = piTabIndex; index > 0; --index)
                {
                    int length = psData.IndexOf("\t", 0, psData.Length);
                    if (length < 0)
                    {
                        str = psData;
                    }
                    else
                    {
                        str = psData.Substring(0, length);
                        psData = psData.Remove(0, length + 1);
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string DataPointSplit(int start, int to, string psData)
        {
            try
            {
                string str = psData.PadRight(start + to, ' ');
                --start;
                if (str.Length < start || str.Length < to)
                    return string.Empty;
                return str.Substring(start, to).Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ReaderSetting(bool values)
        {
            try
            {
                this.cbo포트지정.Enabled = values;
                this.cbo통신속도.Enabled = values;
                this.nud연결대수.Enabled = values;
                this.txtFORMAT지정.Enabled = values;
                this.txtFORMAT지정1.Enabled = values;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
using Duzon.BizOn.Erpu.Security;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Controls.DatePickerComponent;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.Net.Mail;
using DX;
using human;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_HR_PPSRPT : PageBase
    {
        private P_CZ_HR_PPSRPT_BIZ _biz;
        private DataTable _dt양식;
        private DataTable _dt사원정보;
        private DataTable _dt급여;
        private DataTable _dt근태;
        private DataTable _dt인적;
        private DataTable _dt대출;
        private DataTable _dtPay = new DataTable();
        private DataTable _dtWork = new DataTable();
        private Dass.FlexGrid.FlexGrid _flex = new Dass.FlexGrid.FlexGrid();
        private string 암호선택 = string.Empty;

        public P_CZ_HR_PPSRPT()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this.InitOneGrid();
            this._biz = new P_CZ_HR_PPSRPT_BIZ();
            this.InitEvent();
        }

        private void InitOneGrid()
        {
            this.oneGrid1.UseCustomLayout = true;
            this.oneGrid1.InitCustomLayout();
            this.bppnl귀속년월.IsNecessaryCondition = true;
            this.bppnl양식구분.IsNecessaryCondition = true;
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp귀속년월.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.HR, FormatTpType.YEAR_MONTH, FormatFgType.INSERT);
            this.dtp귀속년월.ToDayDate = Global.MainFrame.GetDateTimeToday();
            this.dtp귀속년월.Text = Global.MainFrame.GetStringYearMonth;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            if (MA.ServerKey(false, "CHILD", "CHILD2"))
            {
                string[] strArray = Duzon.ERPU.HR.HR.PAY.급여양식디폴트조회("B", MA.Login.사업장코드 + "|");
                this.ctx양식구분.CodeValue = strArray[0];
                this.ctx양식구분.CodeName = strArray[1];
            }
            else
            {
                string[] strArray = Duzon.ERPU.HR.HR.PAY.급여양식디폴트조회("B");
                this.ctx양식구분.CodeValue = strArray[0];
                this.ctx양식구분.CodeName = strArray[1];
            }
            this.cbo급여구분.DataSource = this.GetComboData("S;HR_P000014").Tables[0];
            this.cbo급여구분.ValueMember = "CODE";
            this.cbo급여구분.DisplayMember = "NAME";
            this.지급일자셋팅();
            this.dtp순번.Value = 1M;

            this._web.IsWebBrowserContextMenuEnabled = false;
            this._web.ScriptErrorsSuppressed = true;
            this._web.AllowWebBrowserDrop = false;
        }

        private void InitEvent()
        {
            this.dtp귀속년월.Validated += new EventHandler(this.Control_Validated);
            this.dtp귀속년월.UpDownClick += new UpDownClickEventHandler(this.Control_Validated);
            this.ctx양식구분.QueryBefore += new BpQueryHandler(this.bpc양식구분_QueryBefore);
            this.btn지급일자.Click += new EventHandler(this.btnYm_Click);
        }

        protected override bool BeforeSearch()
        {
            return base.BeforeSearch() && this.귀속년월입력체크 && this.사원입력체크;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch())
                    return;
                if (this._dt양식 != null)
                    this._dt양식.Rows.Clear();
                if (this._dt사원정보 != null)
                    this._dt사원정보.Rows.Clear();
                if (this._dt급여 != null)
                    this._dt급여.Rows.Clear();
                if (this._dt근태 != null)
                    this._dt근태.Rows.Clear();
                if (this._dt인적 != null)
                    this._dt인적.Rows.Clear();
                if (this._dt대출 != null)
                    this._dt대출.Rows.Clear();
                DataSet dataSet = this._biz.Search(Global.MainFrame.LoginInfo.CompanyCode, this.ctx사원.CodeValue, this.dtp귀속년월.Text, this.cbo지급일자.SelectedValue == null ? string.Empty : this.cbo지급일자.SelectedValue.ToString(), this.ctx양식구분.CodeValue, this.cbo급여구분.SelectedValue == null ? "" : this.cbo급여구분.SelectedValue.ToString(), int.Parse(this.dtp순번.Value.ToString()), Global.SystemLanguage.MultiLanguageLpoint);
                this._dt양식 = dataSet.Tables[0];
                this._dt사원정보 = dataSet.Tables[1];
                this._dt사원정보 = this.개인정보복호화(this._dt사원정보, "NO_RES", DataType.Jumin);
                this.암호선택 = this._biz.급여메일암호선택(Global.MainFrame.LoginInfo.CompanyCode);
                foreach (DataRow row in (InternalDataCollectionBase)this._dt사원정보.Rows)
                {
                    if (this.암호선택 == "1")
                        row["NO_RES"] = row["NO_RES"].ToString().Substring(6, 7);
                    else if (this.암호선택 == "2")
                        row["NO_RES"] = row["NO_RES"].ToString().Substring(0, 6);
                }
                this._dt급여 = dataSet.Tables[2];
                this._dt근태 = !MA.ServerKey(false, "ETEC") || !(MA.Login.회사코드 == "ETEC") ? dataSet.Tables[3] : this._biz.Search_tm_etec(Global.MainFrame.LoginInfo.CompanyCode, this.ctx사원.CodeValue, this.dtp귀속년월.Text, this.ctx양식구분.CodeValue, int.Parse(this.dtp순번.Value.ToString()));
                this._dt인적 = dataSet.Tables[4];
                this._dt대출 = dataSet.Tables[5];
                this._dtPay = this._dt급여.Clone();
                this._dtWork = this._dt근태.Clone();
                DataRow[] drPay = this._dt양식.Select("TP_PDREPORT = '001'", "CD_RSEQ ASC", DataViewRowState.CurrentRows);
                DataRow[] drDeduct = this._dt양식.Select("TP_PDREPORT = '002'", "CD_RSEQ ASC", DataViewRowState.CurrentRows);
                DataRow[] drWork = this._dt양식.Select("TP_PDREPORT = '003'", "CD_RSEQ ASC", DataViewRowState.CurrentRows);
                this._dtPay.ImportRow(this._dt급여.Rows[0]);
                this.SettingDataPay(this._dtPay.Rows[this._dtPay.Rows.Count - 1], this._dt급여.Rows[0], drPay, drDeduct);
                this._dtWork.ImportRow(this._dt근태.Rows[0]);
                this.SettingDataWork(this._dtWork.Rows[this._dtWork.Rows.Count - 1], this._dt근태.Rows[0], drWork);
                this._web.Focus();
                if (dataSet.Tables[1].Rows.Count == 0)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                    this.ToolBarPrintButtonEnabled = false;
                }
                else
                {
                    this.ToolBarPrintButtonEnabled = true;
                    this.메일보내기(dataSet.Tables[0], dataSet.Tables[1], this._dtPay, this._dtWork, this._dt대출);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
                //RegistryKey oKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", true);
                //oKey.SetValue("footer", string.Empty);
                //oKey.SetValue("header", string.Empty);
                //oKey.Close();

                //this._web.ShowPrintPreviewDialog();

                파일.실행(문서.급여명세서(Global.MainFrame.LoginInfo.CompanyCode, this.dtp귀속년월.Text, this.ctx사원.CodeValue));

                #region Old
                //if (this._dt사원정보 == null || this._dt사원정보.Rows.Count == 0)
                //	return;
                //switch (this._biz.GetPrintType())
                //{
                //	case "MRD":
                //		this.PrintMRD();
                //		break;
                //	case "RDF":
                //		this.PrintRDF();
                //		break;
                //}
                #endregion
            }
            catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private DataTable 개인정보복호화(DataTable Dt, string ColName, DataType type) => new DataSourceEncrypter().Decrypting(new DataSourceEncryptParameter(Dt)
        {
            Columns = {
        new DataSourceEncryptColumn(ColName, type)
      }
        }) as DataTable;

        private void SettingDataPay(DataRow Pay, DataRow Org, DataRow[] drPay, DataRow[] drDeduct)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            int num1 = 1;
            int num2 = 1;
            int num3;
            for (num3 = 1; num3 <= 80; ++num3)
            {
                string columnName1 = "PAY_" + num3.ToString().PadLeft(2, Convert.ToChar("0"));
                Pay[columnName1] = 0;
                string columnName2 = "PAY_FORM_" + num3.ToString().PadLeft(2, Convert.ToChar("0"));
                Pay[columnName2] = string.Empty;
            }
            for (num3 = 1; num3 <= 80; ++num3)
            {
                string columnName1 = "DEDU_" + num3.ToString().PadLeft(2, Convert.ToChar("0"));
                Pay[columnName1] = 0;
                string columnName2 = "DEDU_FORM_" + num3.ToString().PadLeft(2, Convert.ToChar("0"));
                Pay[columnName2] = string.Empty;
            }
            Pay["TOTPAY"] = 0;
            Pay["TOTDEDUCT"] = 0;
            Pay["GPAY"] = 0;
            foreach (DataRow dataRow in drPay)
            {
                string columnName1 = "PAY_" + num1.ToString().PadLeft(2, Convert.ToChar("0"));
                string columnName2 = "PAY_" + dataRow["CD_REPORT"].ToString().Substring(1, 2);
                Pay[columnName1] = Org[columnName2];
                string columnName3 = "PAY_FORM_" + num1.ToString().PadLeft(2, Convert.ToChar("0"));
                string columnName4 = "PAY_FORM_" + dataRow["CD_REPORT"].ToString().Substring(1, 2);
                Pay[columnName3] = Org[columnName4];
                ++num1;
            }
            foreach (DataRow dataRow in drDeduct)
            {
                string columnName1 = "DEDU_" + num2.ToString().PadLeft(2, Convert.ToChar("0"));
                string columnName2 = "DEDU_" + dataRow["CD_REPORT"].ToString().Substring(1, 2);
                Pay[columnName1] = Org[columnName2];
                string columnName3 = "DEDU_FORM_" + num2.ToString().PadLeft(2, Convert.ToChar("0"));
                string columnName4 = "DEDU_FORM_" + dataRow["CD_REPORT"].ToString().Substring(1, 2);
                Pay[columnName3] = Org[columnName4];
                ++num2;
            }
            Pay["TOTPAY"] = Org["TOTPAY"];
            Pay["TOTDEDUCT"] = Org["TOTDEDUCT"];
            Pay["GPAY"] = Org["GPAY"];
        }

        private void SettingDataWork(DataRow Work, DataRow Org, DataRow[] drWork)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            int num1 = 1;
            for (int index = 1; index <= 40; ++index)
            {
                string columnName = "TM_" + index.ToString().PadLeft(2, Convert.ToChar("0"));
                Work[columnName] = 0;
                ++num1;
            }
            int num2 = 1;
            foreach (DataRow dataRow in drWork)
            {
                string columnName1 = "TM_" + num2.ToString().PadLeft(2, Convert.ToChar("0"));
                string columnName2 = "TM_" + dataRow["CD_REPORT"].ToString().Substring(1, 2);
                Work[columnName1] = Org[columnName2];
                ++num2;
            }
        }

        private void 메일보내기(
          DataTable dt양식,
          DataTable dt사원정보,
          DataTable dt급여,
          DataTable dt근태,
          DataTable dt대출)
        {
            UFileGenerator ufileGenerator = null;
            try
            {
				#region Old
				//string newValue1 = this.dtp귀속년월.Text.Substring(0, 4) + this.DD("년") + this.dtp귀속년월.Text.Substring(4, 2) + this.DD("월") + " " + this.DD("명세서");
				//string newValue2 = string.Empty;
				//switch (dt사원정보.Rows[0]["TP_PAY"].ToString())
				//{
				//	case "001":
				//		newValue2 = this.DD("급여");
				//		break;
				//	case "002":
				//		newValue2 = this.DD("상여");
				//		break;
				//	case "003":
				//		newValue2 = this.DD("년월차");
				//		break;
				//	case "004":
				//		newValue2 = this.DD("소급");
				//		break;
				//}
				//string empty1 = string.Empty;
				//string empty2 = string.Empty;
				//int num1 = 1;
				//DataRow[] dataRowArray1 = dt양식.Select("TP_PDREPORT = '001'", "", DataViewRowState.CurrentRows);
				//DataRow[] dataRowArray2 = dt양식.Select("TP_PDREPORT = '002'", "", DataViewRowState.CurrentRows);
				//DataRow[] dataRowArray3 = dt양식.Select("TP_PDREPORT = '003'", "", DataViewRowState.CurrentRows);
				//DataRow row = dt급여.Rows[0];
				//DataRow dataRow1 = (DataRow)null;
				//if (dt근태.Rows.Count > 0)
				//	dataRow1 = dt근태.Rows[0];
				//bool flag = this._biz.급여메일인증사용여부();
				//string empty3 = string.Empty;
				//string template;
				//if (MA.ServerKey(false, "KRCC"))
				//{
				//	template = "pay01_KRCC.html";
				//	if (!flag)
				//		template = "pay02_KRCC.html";
				//}
				//else
				//{
				//	template = !MA.ServerKey(false, "DOMINO", "DOMINO2") ? "pay01_ppsrpt.html" : "pay01_DOMINO.html";
				//	if (!flag)
				//		template = "pay02_ppsrpt.html";
				//}
				//ufileGenerator = new UFileGenerator(Global.MainFrame.HostURL, "pay", template);
				//ufileGenerator.MaxCount = 1;
				//ufileGenerator.ClearDirectoryFile();
				//ufileGenerator.Start();
				//ufileGenerator.Replace("@YM", newValue1);
				//ufileGenerator.Replace("@NMPAY", newValue2);
				//ufileGenerator.Replace("@NM_COMPANY", Global.MainFrame.LoginInfo.CompanyName);
				//ufileGenerator.Replace("@NM_DEPT", dt사원정보.Rows[0]["NM_DEPT"].ToString());
				//ufileGenerator.Replace("@NO_EMP", Global.MainFrame.LoginInfo.EmployeeNo);
				//ufileGenerator.Replace("@NM_EMP", Global.MainFrame.LoginInfo.EmployeeName);
				//ufileGenerator.Replace("@PWD", dt사원정보.Rows[0]["NO_RES"].ToString());
				//ufileGenerator.Replace("@DT_PAY", this.날짜포맷으로(dt사원정보.Rows[0]["DT_PAY"].ToString()));
				//if (this.암호선택 == "1")
				//	ufileGenerator.Replace("@PASS_TYPE", this.DD("뒤의 7자리"));
				//else if (this.암호선택 == "2")
				//	ufileGenerator.Replace("@PASS_TYPE", this.DD("앞의 6자리"));
				//foreach (DataRow dataRow2 in dataRowArray1)
				//{
				//	if (num1 <= 80)
				//	{
				//		string columnName1 = "PAY_FORM_" + num1.ToString().PadLeft(2, '0');
				//		ufileGenerator.Replace("@T" + num1.ToString().PadLeft(2, '0') + "F", row[columnName1].ToString());
				//		string columnName2 = "PAY_" + num1.ToString().PadLeft(2, '0');
				//		ufileGenerator.Replace("@T" + num1.ToString().PadLeft(2, '0'), dataRow2["NM_REPORT"].ToString(), string.Format("{0:#,###}", this._flex.CDecimal(row[columnName2].ToString())));
				//		++num1;
				//	}
				//	else
				//		break;
				//}
				//int num2 = 1;
				//foreach (DataRow dataRow2 in dataRowArray2)
				//{
				//	if (num2 <= 80)
				//	{
				//		string columnName1 = "DEDU_FORM_" + num2.ToString().PadLeft(2, '0');
				//		ufileGenerator.Replace("@Q" + num2.ToString().PadLeft(2, '0') + "F", row[columnName1].ToString());
				//		string columnName2 = "DEDU_" + num2.ToString().PadLeft(2, '0');
				//		ufileGenerator.Replace("@Q" + num2.ToString().PadLeft(2, '0'), dataRow2["NM_REPORT"].ToString(), string.Format("{0:#,###}", this._flex.CDecimal(row[columnName2])));
				//		++num2;
				//	}
				//	else
				//		break;
				//}
				//int num3 = 1;
				//if (dataRow1 != null)
				//{
				//	foreach (DataRow dataRow2 in dataRowArray3)
				//	{
				//		if (num3 <= 40)
				//		{
				//			string columnName = "TM_" + num3.ToString().PadLeft(2, '0');
				//			ufileGenerator.Replace("@R" + num3.ToString().PadLeft(2, '0'), dataRow2["NM_REPORT"].ToString(), this.숫자를문자로(this._flex.CDecimal(dataRow1[columnName])));
				//			++num3;
				//		}
				//		else
				//			break;
				//	}
				//}
				//ufileGenerator.Replace("@E03", this.DD("지급총액"), string.Format("{0:#,###}", this._flex.CDecimal(row["TOTPAY"])));
				//ufileGenerator.Replace("@E04", this.DD("공제총액"), string.Format("{0:#,###}", this._flex.CDecimal(row["TOTDEDUCT"])));
				//ufileGenerator.Replace("@E06", this.DD("차감지급액"), string.Format("{0:#,###}", this._flex.CDecimal(row["GPAY"])));
				//DataRow[] dataRowArray4 = this._dt대출.Select("", "");
				//for (int index = 1; index <= 4; ++index)
				//{
				//	if (dataRowArray4.Length >= index)
				//	{
				//		ufileGenerator.Replace("@NM_LEND_" + this.순번을문자로(index), this.공백셋팅(Duzon.ERPU.D.GetString(dataRowArray4[index - 1]["NM_LEND"])));
				//		ufileGenerator.Replace("@AM_REPAY_" + this.순번을문자로(index), this.공백셋팅(this.숫자를문자로(dataRowArray4[index - 1]["AM_REPAY"])));
				//		ufileGenerator.Replace("@AM_INTERST_" + this.순번을문자로(index), this.공백셋팅(this.숫자를문자로(dataRowArray4[index - 1]["AM_INTERST"])));
				//		ufileGenerator.Replace("@AM_BALANCE_" + this.순번을문자로(index), this.공백셋팅(this.숫자를문자로(dataRowArray4[index - 1]["AM_BALANCE"])));
				//		ufileGenerator.Replace("@DT_REND_" + this.순번을문자로(index), this.공백셋팅(this.날짜포맷으로(dataRowArray4[index - 1]["DT_REND"])));
				//	}
				//	else
				//	{
				//		ufileGenerator.Replace("@NM_LEND_" + this.순번을문자로(index), "&nbsp;");
				//		ufileGenerator.Replace("@AM_REPAY_" + this.순번을문자로(index), "&nbsp;");
				//		ufileGenerator.Replace("@AM_INTERST_" + this.순번을문자로(index), "&nbsp;");
				//		ufileGenerator.Replace("@AM_BALANCE_" + this.순번을문자로(index), "&nbsp;");
				//		ufileGenerator.Replace("@DT_REND_" + this.순번을문자로(index), "&nbsp;");
				//	}
				//}
				//ufileGenerator.Replace("@DC_RMK1", dt사원정보.Rows[0]["DC_RMK1"].ToString());
				//ufileGenerator.Replace("@DC_RMK2", dt사원정보.Rows[0]["DC_RMK2"].ToString());
				//ufileGenerator.Replace("@DC_RMK3", dt사원정보.Rows[0]["DC_RMK3"].ToString());
				//for (int index = 1; index <= 42; ++index)
				//{
				//	string oldValue1 = "@T" + index.ToString().PadLeft(2, '0');
				//	ufileGenerator.Replace(oldValue1 + "F", "", "");
				//	ufileGenerator.Replace(oldValue1, "", "");
				//	string oldValue2 = "@Q" + index.ToString().PadLeft(2, '0');
				//	ufileGenerator.Replace(oldValue2 + "F", "", "");
				//	ufileGenerator.Replace(oldValue2, "", "");
				//	if (index <= 18)
				//	{
				//		string oldValue3 = "@R" + index.ToString().PadLeft(2, '0');
				//		ufileGenerator.Replace(oldValue3, "", "");
				//	}
				//	if (index <= 6)
				//	{
				//		string oldValue3 = "@E" + index.ToString().PadLeft(2, '0');
				//		ufileGenerator.Replace(oldValue3, "", "");
				//	}
				//}
				//string urlString = ufileGenerator.End();
				//ufileGenerator.EndGenerate();
				//ufileGenerator = (UFileGenerator)null;
				//this._web.Navigate(urlString);
				#endregion

				this._web.Navigate(string.Format("http://dint.ec/doc/paystub/{0}/{1}/{2}", Global.MainFrame.LoginInfo.CompanyCode, this.dtp귀속년월.Text, this.ctx사원.CodeValue));

            }
            catch (Exception ex)
            {
                ufileGenerator?.EndGenerate();
                this.MsgEnd(ex);
            }
            finally
            {
                ufileGenerator?.EndGenerate();
            }
        }

        private string 순번을문자로(int index) => Duzon.ERPU.D.GetString(index).PadLeft(2, '0');

        private string 숫자를문자로(object value) => string.Format("{0:#,###.####}", Duzon.ERPU.D.GetDecimal(value));

        private string 날짜포맷으로(object value)
        {
            string str = string.Empty;
            if (Duzon.ERPU.D.GetDecimal(value) != 0M)
                str = string.Format("{0:####-##-##}", Duzon.ERPU.D.GetDecimal(value));
            return str;
        }

        private string 주민번호로(object value)
        {
            string str = string.Empty;
            if (Duzon.ERPU.D.GetDecimal(value) != 0M)
                str = string.Format("{0:######-#######}", Duzon.ERPU.D.GetDecimal(value));
            return str;
        }

        private string 공백셋팅(string value)
        {
            string empty = string.Empty;
            return !(value == string.Empty) ? value : "&nbsp;";
        }

        private void ShowMessage(P_CZ_HR_PPSRPT.메세지 msg, params string[] param)
        {
            if (msg != P_CZ_HR_PPSRPT.메세지._설정되지않았습니다)
                return;
            this.ShowMessage("HR_M500159", param[0]);
        }

        private DataTable 출력데이터셋팅()
        {
            DataTable dataTable = this.스키마();
            DataRow row1 = dataTable.NewRow();
            row1["CD_BIZAREA"] = this._dt사원정보.Rows[0]["CD_BIZAREA"];
            row1["NM_BIZAREA"] = this._dt사원정보.Rows[0]["NM_BIZAREA"];
            row1["CD_DEPT"] = this._dt사원정보.Rows[0]["CD_DEPT"];
            row1["NM_DEPT"] = this._dt사원정보.Rows[0]["NM_DEPT"];
            row1["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
            row1["NM_KOR"] = Global.MainFrame.LoginInfo.EmployeeName;
            row1["NM_DUTY_WORK"] = this._dt사원정보.Rows[0]["NM_DUTY_WORK"];
            row1["CD_DUTY_WORK"] = this._dt사원정보.Rows[0]["CD_DUTY_WORK"];
            row1["NM_DUTY_STEP"] = this._dt사원정보.Rows[0]["NM_DUTY_STEP"];
            row1["CD_DUTY_STEP"] = this._dt사원정보.Rows[0]["CD_DUTY_STEP"];
            row1["CD_PAY_STEP"] = this._dt사원정보.Rows[0]["CD_PAY_STEP"];
            row1["NO_RES"] = this._dt사원정보.Rows[0]["NO_RES"];
            row1["NO_EMAIL"] = this._dt사원정보.Rows[0]["NO_EMAIL"];
            row1["DT_PAY"] = this._dt사원정보.Rows[0]["DT_PAY"];
            row1["DT_ENTER"] = this._dt사원정보.Rows[0]["DT_ENTER"];
            row1["DT_GENTER"] = this._dt사원정보.Rows[0]["DT_GENTER"];
            row1["DT_ENTER_P"] = this._dt사원정보.Rows[0]["DT_ENTER_P"];
            row1["DC_RMK3"] = this._dt사원정보.Rows[0]["DC_RMK3"];
            row1["DT_BIRTH"] = this._dt사원정보.Rows[0]["DT_BIRTH"];
            string empty = string.Empty;
            for (int index = 1; index <= 14; ++index)
            {
                string columnName = "EMP_INFO_" + index.ToString().PadLeft(2, '0');
                switch (index)
                {
                    case 1:
                        row1[columnName] = this._dt사원정보.Rows[0]["NM_DEPT"];
                        break;
                    case 2:
                        row1[columnName] = this._dt사원정보.Rows[0]["NO_EMP"];
                        break;
                    case 3:
                        row1[columnName] = this._dt사원정보.Rows[0]["NM_KOR"];
                        break;
                    default:
                        row1[columnName] = this._dt사원정보.Rows[0]["INJUCK_NAME" + Convert.ToString(index - 3)];
                        break;
                }
            }
            for (int index = 1; index <= 11; ++index)
            {
                string columnName = "INJUCK_CODE" + index.ToString();
                row1[columnName] = this._dt사원정보.Rows[0][columnName];
            }
            int num1;
            for (num1 = 1; num1 <= 11; ++num1)
            {
                Duzon.ERPU.D.GetString(this._dt사원정보.Rows[0]["INJUCK_CODE" + num1.ToString()]);
                string str = Duzon.ERPU.D.GetString(this._dt사원정보.Rows[0]["RHEADER_CODE" + num1.ToString()]);
                if (str == string.Empty)
                    str = "ZZZ";
                string columnName = "INJUCK_NAME" + num1.ToString();
                switch (str)
                {
                    case "004":
                    case "011":
                    case "019":
                    case "024":
                    case "027":
                    case "028":
                    case "029":
                    case "030":
                        row1[columnName] = Duzon.ERPU.D.GetString(this.날짜포맷으로(this._dt사원정보.Rows[0][columnName]));
                        break;
                    case "007":
                    case "008":
                    case "009":
                    case "010":
                    case "015":
                    case "023":
                    case "093":
                        row1[columnName] = Duzon.ERPU.D.GetString(this.숫자를문자로(this._dt사원정보.Rows[0][columnName]));
                        break;
                    case "012":
                        object obj = Duzon.ERPU.UEncryption.UEncryption.DecryptionData(this._dt사원정보.Rows[0][columnName], DataType.Jumin);
                        row1[columnName] = Duzon.ERPU.D.GetString(this.주민번호로(obj));
                        break;
                    default:
                        int num2 = str.Substring(0, 1) == "1" ? 0 : (!(str.Substring(0, 1) == "2") ? 1 : 0);
                        row1[columnName] = num2 != 0 ? Duzon.ERPU.D.GetString(this._dt사원정보.Rows[0][columnName]) : Duzon.ERPU.D.GetString(this.숫자를문자로(this._dt사원정보.Rows[0][columnName]));
                        break;
                }
            }
            for (num1 = 1; num1 <= 40; ++num1)
            {
                string columnName = "TM_" + num1.ToString().PadLeft(2, '0');
                row1[columnName] = this._dtWork.Rows[0][columnName];
            }
            for (num1 = 1; num1 <= 80; ++num1)
            {
                string columnName1 = "PAY_" + num1.ToString().PadLeft(2, '0');
                row1[columnName1] = this._dtPay.Rows[0][columnName1];
                string columnName2 = "PAY_FORM_" + num1.ToString().PadLeft(2, '0');
                row1[columnName2] = this._dtPay.Rows[0][columnName2];
            }
            row1["TOTPAY"] = this._dt급여.Rows[0]["TOTPAY"];
            for (num1 = 1; num1 <= 80; ++num1)
            {
                string columnName1 = "DEDU_" + num1.ToString().PadLeft(2, '0');
                row1[columnName1] = this._dtPay.Rows[0][columnName1];
                string columnName2 = "DEDU_FORM_" + num1.ToString().PadLeft(2, '0');
                row1[columnName2] = this._dtPay.Rows[0][columnName2];
            }
            row1["TOTDEDUCT"] = this._dtPay.Rows[0]["TOTDEDUCT"];
            row1["GPAY"] = this._dtPay.Rows[0]["GPAY"];
            int num3 = 1;
            foreach (DataRow row2 in (InternalDataCollectionBase)this._dt대출.Rows)
            {
                if (num3 <= 10)
                {
                    string columnName1 = "NM_LEND_" + num3.ToString().PadLeft(2, '0');
                    row1[columnName1] = row2["NM_LEND"].ToString();
                    string columnName2 = "AM_REPAY_" + num3.ToString().PadLeft(2, '0');
                    row1[columnName2] = row2["AM_REPAY"].ToString();
                    string columnName3 = "AM_INTERST_" + num3.ToString().PadLeft(2, '0');
                    row1[columnName3] = row2["AM_INTERST"].ToString();
                    string columnName4 = "AM_BALANCE_" + num3.ToString().PadLeft(2, '0');
                    row1[columnName4] = row2["AM_BALANCE"].ToString();
                    string columnName5 = "DT_REND_" + num3.ToString().PadLeft(2, '0');
                    row1[columnName5] = row2["DT_REND"].ToString();
                    ++num3;
                }
                else
                    break;
            }
            dataTable.Rows.Add(row1);
            return dataTable;
        }

        private DataTable 스키마()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CD_BIZAREA", typeof(string));
            dataTable.Columns.Add("NM_BIZAREA", typeof(string));
            dataTable.Columns.Add("CD_DEPT", typeof(string));
            dataTable.Columns.Add("NM_DEPT", typeof(string));
            dataTable.Columns.Add("NO_EMP", typeof(string));
            dataTable.Columns.Add("NM_KOR", typeof(string));
            dataTable.Columns.Add("CD_DUTY_WORK", typeof(string));
            dataTable.Columns.Add("NM_DUTY_WORK", typeof(string));
            dataTable.Columns.Add("CD_DUTY_STEP", typeof(string));
            dataTable.Columns.Add("NM_DUTY_STEP", typeof(string));
            dataTable.Columns.Add("CD_PAY_STEP", typeof(string));
            dataTable.Columns.Add("NO_RES", typeof(string));
            dataTable.Columns.Add("NO_EMAIL", typeof(string));
            dataTable.Columns.Add("DT_PAY", typeof(string));
            dataTable.Columns.Add("DT_ENTER", typeof(string));
            dataTable.Columns.Add("DT_GENTER", typeof(string));
            dataTable.Columns.Add("DT_ENTER_P", typeof(string));
            dataTable.Columns.Add("DC_RMK3", typeof(string));
            dataTable.Columns.Add("DT_BIRTH", typeof(string));
            string empty = string.Empty;
            int num;
            for (num = 1; num <= 14; ++num)
            {
                string columnName = "EMP_INFO_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName, typeof(string));
            }
            for (num = 1; num <= 11; ++num)
            {
                string columnName = "INJUCK_CODE" + num.ToString();
                dataTable.Columns.Add(columnName, typeof(string));
            }
            for (num = 1; num <= 11; ++num)
            {
                string columnName = "INJUCK_NAME" + num.ToString();
                dataTable.Columns.Add(columnName, typeof(string));
            }
            for (num = 1; num <= 40; ++num)
            {
                string columnName = "TM_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName, typeof(Decimal));
            }
            for (num = 1; num <= 80; ++num)
            {
                string columnName1 = "PAY_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName1, typeof(Decimal));
                string columnName2 = "PAY_FORM_" + Duzon.ERPU.D.GetString(num).PadLeft(2, '0');
                dataTable.Columns.Add(columnName2, typeof(string));
            }
            dataTable.Columns.Add("TOTPAY", typeof(Decimal));
            for (num = 1; num <= 80; ++num)
            {
                string columnName1 = "DEDU_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName1, typeof(Decimal));
                string columnName2 = "DEDU_FORM_" + Duzon.ERPU.D.GetString(num).PadLeft(2, '0');
                dataTable.Columns.Add(columnName2, typeof(string));
            }
            dataTable.Columns.Add("TOTDEDUCT", typeof(Decimal));
            dataTable.Columns.Add("GPAY", typeof(Decimal));
            for (num = 1; num <= 10; ++num)
            {
                string columnName1 = "CD_LEND_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName1, typeof(string));
                string columnName2 = "NM_LEND_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName2, typeof(string));
                string columnName3 = "DT_REND_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName3, typeof(string));
                string columnName4 = "AM_REPAY_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName4, typeof(Decimal));
                string columnName5 = "AM_INTERST_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName5, typeof(Decimal));
                string columnName6 = "AM_BALANCE_" + num.ToString().PadLeft(2, '0');
                dataTable.Columns.Add(columnName6, typeof(Decimal));
            }
            foreach (DataColumn column in (InternalDataCollectionBase)dataTable.Columns)
            {
                if (column.DataType == typeof(Decimal))
                    column.DefaultValue = 0;
            }
            return dataTable;
        }

        private void 지급일자셋팅()
        {
            if (this.dtp귀속년월.Text == string.Empty)
                this.cbo지급일자.DataSource = null;
            DataTable dataTable = this._biz.지급일자조회(Global.MainFrame.LoginInfo.CompanyCode, this.ctx사원.CodeValue, this.dtp귀속년월.Text);
            this.cbo지급일자.DataSource = dataTable;
            this.cbo지급일자.ValueMember = "CODE";
            this.cbo지급일자.DisplayMember = "NAME";
            if (dataTable == null || dataTable.Rows.Count <= 0)
                return;
            this.cbo지급일자.SelectedIndex = 0;
        }

        private void PrintRDF() => new PrintRDF("R_HR_PPERRPT_0", "", false, this.출력데이터셋팅(), this.출력파라미터셋팅()).ShowDialog();

        private DataTable 출력파라미터셋팅()
        {
            DataTable dtParam = new DataTable();
            dtParam.Columns.Add("ParamCode");
            dtParam.Columns.Add("ParamName");
            this.SetDynamicParam(dtParam, "지급");
            this.SetDynamicParam(dtParam, "공제");
            this.SetDynamicParam(dtParam, "근태");
            this.SetFixedParam(dtParam);
            return dtParam;
        }

        private void SetFixedParam(DataTable dtParam)
        {
            this.AddData(dtParam, "TITLE_P1", this.ctx양식구분.CodeName);
            this.AddData(dtParam, "YM_P1", this.dtp귀속년월.Text.Substring(0, 4) + this.DD("년") + this.dtp귀속년월.Text.Substring(4, 2) + this.DD("월"));
            this.AddData(dtParam, "COMPANY_P1", this.MainFrameInterface.LoginInfo.CompanyName);
            this.AddData(dtParam, "TXT103_PAY_P", this.DD("TXT103_PAY"));
            this.AddData(dtParam, "COMPANY_P", this.DD("COMPANY"));
            this.AddData(dtParam, "ITEMPAY_P", this.DD("ITEMPAY"));
            this.AddData(dtParam, "ITEMDEDUCT_P", this.DD("ITEMDEDUCT"));
            this.AddData(dtParam, "WINFO_P", this.DD("WINFO"));
            this.AddData(dtParam, "TOT_P", this.DD("TOT"));
            this.AddData(dtParam, "TOTPAY_P", this.DD("TOTPAY"));
            this.AddData(dtParam, "TOTDEDUCT_P", this.DD("TOTDEDUCT"));
            this.AddData(dtParam, "GPAY_P", this.DD("GPAY"));
            this.AddData(dtParam, "DC_RMK1_P1", this._dt사원정보.Rows[0]["DC_RMK1"].ToString());
            this.AddData(dtParam, "DC_RMK2_P1", this._dt사원정보.Rows[0]["DC_RMK2"].ToString());
            for (int index = 1; index <= this._dt인적.Rows.Count && index <= 10; ++index)
            {
                string code = "EMP_INFO_" + index.ToString().PadLeft(2, Convert.ToChar("0")) + "_P";
                string empty = string.Empty;
                string name;
                switch (Duzon.ERPU.D.GetString(this._dt인적.Rows[index - 1]["CD_RHEADER"]))
                {
                    case "031":
                    case "032":
                    case "033":
                    case "034":
                    case "035":
                    case "036":
                        name = this.DD(Duzon.ERPU.D.GetString(this._dt인적.Rows[index - 1]["NM_RHEADER"])) + " :";
                        break;
                    default:
                        name = Duzon.ERPU.D.GetString(this._dt인적.Rows[index - 1]["NM_RHEADER"]) + " :";
                        break;
                }
                this.AddData(dtParam, code, name);
            }
        }

        private void SetDynamicParam(DataTable dtParam, string type)
        {
            string filterExpression = string.Empty;
            string str = string.Empty;
            if (type == "지급")
            {
                filterExpression = "TP_PDREPORT = '001'";
                str = "PAY_";
            }
            else if (type == "공제")
            {
                filterExpression = "TP_PDREPORT = '002'";
                str = "DEDU_";
            }
            else if (type == "근태")
            {
                filterExpression = "TP_PDREPORT = '003'";
                str = "WINFO_";
            }
            DataRow[] dataRowArray = this._dt양식.Select(filterExpression, "CD_RSEQ ASC", DataViewRowState.CurrentRows);
            if (dataRowArray.Length <= 0)
                return;
            for (int index = 1; index <= dataRowArray.Length; ++index)
            {
                string code = str + index.ToString().PadLeft(2, Convert.ToChar("0")) + "_P";
                string name = Duzon.ERPU.D.GetString(dataRowArray[index - 1]["NM_REPORT"]);
                this.AddData(dtParam, code, name);
            }
        }

        private void AddData(DataTable dtParam, string code, string name)
        {
            DataRow row = dtParam.NewRow();
            row["ParamCode"] = code;
            row["ParamName"] = name;
            dtParam.Rows.Add(row);
        }

        private void PrintMRD()
        {
            string[] code = new string[130];
            int index1 = 0;
            for (int index2 = 1; index2 <= 40; ++index2)
            {
                code[index1] = "PAY_" + index2.ToString().PadLeft(2, Convert.ToChar("0")) + "_P";
                ++index1;
            }
            for (int index2 = 1; index2 <= 40; ++index2)
            {
                code[index1] = "DEDU_" + index2.ToString().PadLeft(2, Convert.ToChar("0")) + "_P";
                ++index1;
            }
            for (int index2 = 1; index2 <= 18; ++index2)
            {
                code[index1] = "WINFO_" + index2.ToString().PadLeft(2, Convert.ToChar("0")) + "_P";
                ++index1;
            }
            code[98] = "TITLE_P1";
            code[99] = "YM_P1";
            code[100] = "COMPANY_P1";
            code[101] = "TXT103_PAY_P";
            code[102] = "COMPANY_P";
            code[103] = "CD_DEPT_P";
            code[104] = "NO_EMP_P";
            code[105] = "NAME_P";
            code[106] = "MESSRS_P";
            code[107] = "ITEMPAY_P";
            code[108] = "ITEMDEDUCT_P";
            code[109] = "WINFO_P";
            code[110] = "TOT_P";
            code[111] = "TOTPAY_P";
            code[112] = "TOTDEDUCT_P";
            code[113] = "GPAY_P";
            code[114] = "DC_RMK1_P1";
            code[115] = "DC_RMK2_P1";
            int index3 = 116;
            for (int index2 = 1; index2 <= 14; ++index2)
            {
                code[index3] = "EMP_INFO_" + index2.ToString().PadLeft(2, Convert.ToChar("0")) + "_P";
                ++index3;
            }
            string[] name = new string[code.Length];
            int index4 = 0;
            DataRow[] dataRowArray1 = this._dt양식.Select("TP_PDREPORT = '001'", "CD_RSEQ ASC", DataViewRowState.CurrentRows);
            for (int index2 = 0; index2 < dataRowArray1.Length && index2 <= 40; ++index2)
            {
                name[index4] = dataRowArray1[index2]["NM_REPORT"].ToString();
                ++index4;
            }
            int index5 = 40;
            DataRow[] dataRowArray2 = this._dt양식.Select("TP_PDREPORT = '002'", "CD_RSEQ ASC", DataViewRowState.CurrentRows);
            for (int index2 = 0; index2 < dataRowArray2.Length && index2 <= 40; ++index2)
            {
                name[index5] = dataRowArray2[index2]["NM_REPORT"].ToString();
                ++index5;
            }
            int index6 = 80;
            DataRow[] dataRowArray3 = this._dt양식.Select("TP_PDREPORT = '003'", "CD_RSEQ ASC", DataViewRowState.CurrentRows);
            for (int index2 = 0; index2 < dataRowArray3.Length && index6 != 97; ++index2)
            {
                name[index6] = dataRowArray3[index2]["NM_REPORT"].ToString();
                ++index6;
            }
            name[98] = this.ctx양식구분.CodeName;
            name[99] = this.dtp귀속년월.Text.Substring(0, 4) + this.DD("년") + this.dtp귀속년월.Text.Substring(4, 2) + this.DD("월");
            name[100] = this.MainFrameInterface.LoginInfo.CompanyName;
            for (int index2 = 101; index2 <= 113; ++index2)
                name[index2] = this.DD(code[index2].Substring(0, code[index2].Length - 2));
            name[114] = this._dt사원정보.Rows[0]["DC_RMK1"].ToString();
            name[115] = this._dt사원정보.Rows[0]["DC_RMK2"].ToString();
            name[116] = this.DD("부서");
            name[117] = this.DD("사번");
            name[118] = this.DD("성명");
            for (int index2 = 0; index2 < this._dt인적.Rows.Count && index2 != 10; ++index2)
                name[119 + index2] = this._dt인적.Rows[index2]["NM_RHEADER"].ToString();
            DataTable dataTable = this.출력데이터셋팅();
            ReportDesigner reportDesigner = new ReportDesigner(this.MainFrameInterface, 0);
            reportDesigner.SetDataDictionary(code, name);
            reportDesigner.Start("R_HR_PPERRPT_0", new DataTable[1]
            {
        dataTable
            });
        }

        private void bpc양식구분_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (MA.ServerKey(false, "CHILD", "CHILD2"))
                    this.ctx양식구분.UserParams = this.DD("양식구분 도움창") + ";H_HR_PREFORM_D2;B;" + MA.Login.사업장코드 + "|";
                else
                    this.ctx양식구분.UserParams = this.DD("양식구분 도움창") + ";H_HR_PREFORM_D;B";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this.dtp귀속년월.Modified)
                    return;
                if (this.dtp귀속년월.Text != string.Empty && !this.dtp귀속년월.IsValidated)
                {
                    this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
                    this.dtp귀속년월.Text = string.Empty;
                }
                this.지급일자셋팅();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_UpDown(object sender, UpDownClickEventArgs args)
        {
            try
            {
                if (!(args.OldValue != args.NewValue))
                    return;
                this.Control_Validated(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnYm_Click(object sender, EventArgs e)
        {
            try
            {
                P_HR_SYEAR pHrSyear = new P_HR_SYEAR(this.MainFrameInterface, "P_HR_PPSRPT", new string[] { this.dtp귀속년월.Text });
                if (pHrSyear.ShowDialog() != DialogResult.OK)
                    return;
                DataRow getRow = pHrSyear.GetRow;
                this.dtp귀속년월.Text = D.GetString(getRow["YM"]);
                this.dtp순번.Value = D.GetDecimal(getRow["NO_SEQ"]);
                this.cbo급여구분.SelectedValue = D.GetString(getRow["TP_PAY"]);
                this.cbo지급일자.SelectedValue = D.GetString(getRow["DT_PAY"]);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool 귀속년월입력체크
        {
            get
            {
                if (!(this.dtp귀속년월.Text == string.Empty))
                    return true;
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl귀속년월.Text);
                this.dtp귀속년월.Focus();
                return false;
            }
        }

        private bool 사원입력체크
        {
            get
            {
                if (!(this.ctx사원.Text == string.Empty))
                    return true;
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl사원.Text);
                this.ctx사원.Focus();
                return false;
            }
        }

        private enum 메세지
        {
            _설정되지않았습니다,
        }
    }
}

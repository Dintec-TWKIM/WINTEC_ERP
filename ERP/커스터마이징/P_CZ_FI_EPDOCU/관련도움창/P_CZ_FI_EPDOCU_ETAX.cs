using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.FI;
using DZEBANKSDKLib;

namespace cz
{
    public partial class P_CZ_FI_EPDOCU_ETAX : Duzon.Common.Forms.CommonDialog
    {
        public static string 서버키 = Global.MainFrame.ServerKey.ToUpper();
        public static bool is거래처자동등록 = false;
        public DzBill365Api axDzBill365Api1 = new DzBill365Api();
        private DataTable _dt선택 = null;
        private DataTable 거래처코드 = null;
        private CellStyle s = null;
        private CellStyle s2 = null;
        private P_CZ_FI_EPDOCU_BIZ _biz;
        private string 회계단위;
        private string 결의부서;
        private string 결의사원;
        private string ETAXID;
        private string ETAXPWD;

        private bool Chk사업장
        {
            get
            {
                return !Checker.IsEmpty((Control)this.m_bp사업장, this.lbl사업장.Text);
            }
        }

        public P_CZ_FI_EPDOCU_ETAX(object[] obj)
        {
            this.회계단위 = D.GetString(obj[0]);
            this.결의부서 = D.GetString(obj[1]);
            this.결의사원 = D.GetString(obj[2]);
            this.InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            this._biz = new P_CZ_FI_EPDOCU_BIZ();
            this.InitGrid();
            this.s = this._flex.Styles.Add("RED");
            this.s.ForeColor = Color.Red;
            this.s2 = this._flex.Styles.Add("BLACK");
            this.s2.ForeColor = Color.Black;
        }

        private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("CD_EPCODE", "결의코드", 60, true);
            this._flex.SetCol("NM_EPCODE", "결의내역", 100, false);
            this._flex.SetCol("CD_EVID", "증빙코드", 60, true);
            this._flex.SetCol("NM_EVID", "증빙유형", 100, false);
            this._flex.SetCol("FG_IO", "과세구분", false);
            this._flex.SetCol("YMD_WRITE", "발행일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("CD_PARTNER", "거래처", 80, false);
            this._flex.SetCol("NM_PARTNER", "거래처명", 100, false);
            this._flex.SetCol("NO_VIEW_COMPANY", "사업자등록번호", 90);
            this._flex.Cols["NO_VIEW_COMPANY"].TextAlign = TextAlignEnum.CenterCenter;
            this._flex.SetCol("AM", "공급가액", 100, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_VAT", "부가세", 100, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_SUM", "합계", 100, false, typeof(Decimal), FormatTpType.MONEY);
            this._flex.SetCol("TP_DOCU", "처리여부", 70, false);
            this._flex.SetCol("NM_CEO", "대표자명", false);
            this._flex.SetCol("ADDR1", "주소", false);
            this._flex.SetCol("ADDR2", "상세주소", false);
            this._flex.SetCodeHelpCol("CD_EPCODE", "H_FI_EPCODE_HELP", ShowHelpEnum.Always, new string[2]
      {
        "CD_EPCODE",
        "NM_EPCODE"
      }, new string[2]
      {
        "CODE",
        "NAME"
      });
            this._flex.SetCodeHelpCol("CD_EVID", "H_FI_HELP01", ShowHelpEnum.Always, new string[2]
      {
        "CD_EVID",
        "NM_EVID"
      }, new string[2]
      {
        "CODE",
        "NAME"
      });
            this._flex.ExtendLastCol = true;
            this._flex.SetDummyColumn("S");
            this._flex.SettingVersion = "1.0.0.4";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flex.StartEdit += new RowColEventHandler(this.Flex_StartEdit);
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Flex_BeforeCodeHelp);
            this._flex.SelectionMode = SelectionModeEnum.ListBox;
            this._flex.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow || (e.Row < this._flex.Rows.Fixed || e.Row >= this._flex.Rows.Count))
                    return;
                int num = e.Col < this._flex.Cols["CD_EPCODE"].Index ? 1 : (e.Col > this._flex.Cols["AM_SUM"].Index ? 1 : 0);
                e.Style = num != 0 ? this._flex.Styles["BLACK"] : (!(D.GetString(this._flex[e.Row, "CD_PARTNER"]) == string.Empty) ? this._flex.Styles["BLACK"] : this._flex.Styles["RED"]);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            this.m_bp사업장.CodeValue = Global.MainFrame.LoginInfo.BizAreaCode;
            this.m_bp사업장.CodeName = Global.MainFrame.LoginInfo.BizAreaName;
            this.m_DT.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.m_DT.EndDateToString = Global.MainFrame.GetStringToday;
            DataTable code = FI.GetCode("FI_C000002", true);
            new SetControl().SetCombobox(this.m_cbo처리구분, code);
            this.m_cbo처리구분.SelectedValue = "0";
            this._flex.SetDataMap("TP_DOCU", code, "CODE", "NAME");
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CODE", typeof(string));
            dataTable.Columns.Add("NAME", typeof(string));
            dataTable.NewRow();
            DataRow row1 = dataTable.NewRow();
            row1["CODE"] = "1";
            row1["NAME"] = "매입";
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["CODE"] = "2";
            row2["NAME"] = "매출";
            dataTable.Rows.Add(row2);
            this.m_cbo과세구분.DataSource = dataTable;
            this.m_cbo과세구분.DisplayMember = "NAME";
            this.m_cbo과세구분.ValueMember = "CODE";
            this.m_cbo과세구분.SelectedValue = "2";
        }

        private void Flex_StartEdit(object sender, RowColEventArgs e)
        {
            if (!(D.GetString(this._flex[e.Row, "TP_DOCU"]) == "1"))
                return;
            e.Cancel = true;
        }

        private void Flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (D.GetString(this._flex["TP_DOCU"]) == "1")
                    e.Cancel = true;
                switch (this._flex.Cols[e.Col].Name)
                {
                    case "CD_EPCODE":
                        e.Parameter.UserParams = "결의코드도움창;H_FI_EPCODE_HELP;;" + D.GetString(this._flex[e.Row, "FG_IO"]);
                        break;
                    case "CD_EVID":
                        e.Parameter.UserParams = "증빙유형도움창;H_FI_EVID;" + D.GetString(this._flex[e.Row, "FG_IO"]);
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_AfterSort(object sender, SortColEventArgs e)
        {
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Chk사업장)
                    return;
                DataTable dataTable = DBHelper.GetDataTable("UP_FI_EPDOCU_ETAX_DWNLOAD_S", new object[5]
        {
           Global.MainFrame.LoginInfo.CompanyCode,
           this.m_bp사업장.CodeValue,
           this.m_DT.StartDateToString,
           this.m_DT.EndDateToString,
           D.GetString(this.m_cbo과세구분.SelectedValue)
        });
                for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
                {
                    if (D.GetString(dataTable.Rows[index]["FG_IO"]) == "1")
                    {
                        if (D.GetString(dataTable.Rows[index]["NM_SENDER_SYS"]) == "U" && D.GetString(dataTable.Rows[index]["YN_TURN"]) != "Y")
                            dataTable.Rows[index].Delete();
                    }
                    else if (D.GetString(dataTable.Rows[index]["FG_IO"]) == "2" && D.GetString(dataTable.Rows[index]["NM_SENDER_SYS"]) == "U")
                        dataTable.Rows[index].Delete();
                }
                this._flex.Binding = string.IsNullOrEmpty(D.GetString(this.m_cbo처리구분.SelectedValue)) ? dataTable : new DataView(dataTable, "TP_DOCU = '" + D.GetString(this.m_cbo처리구분.SelectedValue) + "'", "", DataViewRowState.CurrentRows).ToTable();
                if (!this._flex.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                this._flex.AcceptChanges();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn결의코드_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                    return;
                HelpReturn helpReturn = (HelpReturn)new PageBase().ShowHelp(new HelpParam(HelpID.P_USER)
                {
                    UserCodeName = "NAME",
                    UserCodeValue = "CODE",
                    UserHelpID = "H_FI_EPCODE_HELP",
                    UserParams = ("결의내역도움창;H_FI_EPCODE_HELP;;" + D.GetString(dataRowArray[0]["FG_IO"]))
                });
                if (helpReturn.DialogResult != DialogResult.OK || helpReturn.DataTable.Rows.Count == 0)
                    return;
                foreach (DataRow dataRow in dataRowArray)
                {
                    if (!(D.GetString(dataRow["TP_DOCU"]) == "1"))
                    {
                        dataRow["CD_EPCODE"] = helpReturn.CodeValue;
                        dataRow["NM_EPCODE"] = helpReturn.CodeName;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn증빙유형_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flex.HasNormalRow)
                    return;
                DataRow[] dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                    return;
                HelpReturn helpReturn = (HelpReturn)new PageBase().ShowHelp(new HelpParam(HelpID.P_USER)
                {
                    UserCodeName = "NAME",
                    UserCodeValue = "CODE",
                    UserHelpID = "H_FI_HELP01",
                    UserParams = ("증빙유형도움창;H_FI_EVID;" + D.GetString(dataRowArray[0]["FG_IO"]))
                });
                if (helpReturn.DialogResult != DialogResult.OK || helpReturn.DataTable.Rows.Count == 0)
                    return;
                foreach (DataRow dataRow in dataRowArray)
                {
                    if (!(D.GetString(dataRow["TP_DOCU"]) == "1"))
                    {
                        dataRow["CD_EVID"] = helpReturn.CodeValue;
                        dataRow["NM_EVID"] = helpReturn.CodeName;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = true;
                if (!this._flex.HasNormalRow)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this._dt선택 = new DataView(this._flex.DataTable, "S = 'Y' AND TP_DOCU = '0'", "", DataViewRowState.CurrentRows).ToTable();
                    if (this._dt선택 == null || this._dt선택.Rows.Count == 0)
                        this.DialogResult = DialogResult.OK;
                    DataTable dataTable = DBHelper.GetDataTable(@"SELECT CD_ENV
                                                                  FROM MA_ENVD WITH(NOLOCK)
                                                                  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND TP_ENV = 'YN_AUTO_PARTNER'");
                    foreach (DataRow dataRow in (InternalDataCollectionBase)this._dt선택.Rows)
                    {
                        if (string.IsNullOrEmpty(D.GetString(dataRow["CD_EPCODE"])))
                        {
                            Global.MainFrame.ShowMessage("결의코드를 등록하셔야합니다.");
                            flag = false;
                            break;
                        }
                        if (string.IsNullOrEmpty(D.GetString(dataRow["CD_EVID"])))
                        {
                            Global.MainFrame.ShowMessage("증빙코드를 등록하셔야합니다.");
                            flag = false;
                            break;
                        }
                        if (D.GetString(dataTable.Rows[0][0]) == "Y")
                            P_CZ_FI_EPDOCU_ETAX.is거래처자동등록 = true;
                        else if (string.IsNullOrEmpty(D.GetString(dataRow["CD_PARTNER"])))
                        {
                            Global.MainFrame.ShowMessage("CZ_사업자등록번호[@]에 해당하는 거래처를 등록하셔야합니다.", D.GetString(dataRow["NO_VIEW_COMPANY"]));
                            flag = false;
                            break;
                        }
                    }
                    if (!flag)
                        return;
                    if (P_CZ_FI_EPDOCU_ETAX.is거래처자동등록)
                    {
                        if (new DataView(this._dt선택, "NO_COMPANY = '8888888888' ", "NM_PARTNER", DataViewRowState.CurrentRows).ToTable().Rows.Count > 0)
                        {
                            for (int index = 0; index < this._flex.DataTable.Rows.Count; ++index)
                            {
                                if (D.GetString(this._flex.DataTable.Rows[index]["S"]) == "Y" && D.GetString(this._flex.DataTable.Rows[index]["NO_COMPANY"]) == "8888888888")
                                    this._flex.DataTable.Rows[index]["S"] = "N";
                            }
                            Global.MainFrame.ShowMessage("CZ_개인사업자등록인 사업자등록번호[8888888888]에 해당하는 거래처를 등록하셔야합니다.");
                            this._flex.AcceptChanges();
                            return;
                        }
                        string str1 = DBHelper.GetDataTable(@"SELECT MAX(CD_PARTNER)
                                                              FROM MA_PARTNER WITH(NOLOCK)
                                                              WHERE CD_COMPANY = '" + D.GetString(MA.Login.회사코드) + "' ").Rows[0][0].ToString();
                        if (string.IsNullOrEmpty(str1))
                            str1 = "0";
                        string str2 = string.Empty;
                        string str3 = str1;
                        for (int startIndex = str1.Length - 1; startIndex >= 0; --startIndex)
                        {
                            if (char.IsNumber(str1[startIndex]))
                            {
                                str2 = str1[startIndex] + str2;
                                str3 = str3.Remove(startIndex, 1);
                            }
                            else
                            {
                                if (startIndex != str1.Length - 1)
                                {
                                    str1.Remove(startIndex + 1);
                                    break;
                                }
                                break;
                            }
                        }
                        string str4 = str3;
                        int @int = D.GetInt(str2);
                        this.거래처코드 = new DataView(this._dt선택, "CD_PARTNER IS NULL OR CD_PARTNER = ''", "NM_PARTNER", DataViewRowState.CurrentRows).ToTable().DefaultView.ToTable(1 != 0, "CD_PARTNER", "NM_PARTNER", "LN_PARTNER", "NO_COMPANY", "NM_CEO", "ADDR1", "ADDR2");
                        foreach (DataRow dataRow in (InternalDataCollectionBase)this.거래처코드.Rows)
                        {
                            dataRow["LN_PARTNER"] = D.GetString(dataRow["NM_PARTNER"]);
                            dataRow["CD_PARTNER"] = (str4 + Convert.ToString(++@int).PadLeft(str2.Length, '0'));
                        }
                        this.Save거래처();
                    }
                    if (this.Save())
                        this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn데이타_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.Chk사업장)
                    return;
                P_CZ_FI_EPDOCU_ETAXDT pFiEpdocuEtaxdt = new P_CZ_FI_EPDOCU_ETAXDT();
                if (pFiEpdocuEtaxdt.ShowDialog() != DialogResult.OK)
                    return;
                string[] getResult = pFiEpdocuEtaxdt.GetResult;
                string get발행구분 = pFiEpdocuEtaxdt.Get발행구분;
                MsgControl.ShowMsg("BILL36524 사이트에 접속중입니다..");
                DataTable loginInfo = this.GetLoginInfo(this.m_bp사업장.CodeValue);
                if (loginInfo.Rows.Count == 0)
                {
                    MsgControl.CloseMsg();
                    Global.MainFrame.ShowMessage("로긴정보가 없습니다.");
                }
                else
                {
                    this.axDzBill365Api1.ConnectionURL = "http://www.bill36524.com";
                    this.axDzBill365Api1.EnableCertification = 1;
                    this.axDzBill365Api1.EnableUnique_NO_SENDER_PK = 1;
                    if (loginInfo.Rows.Count > 0)
                    {
                        this.ETAXID = loginInfo.Rows[0]["ID_36524"].ToString();
                        this.ETAXPWD = loginInfo.Rows[0]["PW_36524"].ToString();
                        if (loginInfo.Rows[0]["GUBUN_36524"].ToString() == "TEST")
                            this.axDzBill365Api1.ConnectionURL = "http://test.bill36524.com";
                    }
                    if (this.axDzBill365Api1.InitApi() == 0)
                    {
                        MsgControl.CloseMsg();
                        Global.MainFrame.ShowMessage("36524 초기화에 실패했습니다.");
                    }
                    else
                    {
                        if (!this.ETaxLogin())
                            return;
                        MsgControl.CloseMsg();
                        MsgControl.ShowMsg("잠시만 기다려주세요. 데이타를 다운로드중입니다.");
                        if (this.ETaxDownLoad(getResult, get발행구분))
                            this.btn조회_Click(null, (EventArgs)null);
                        MsgControl.CloseMsg();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        internal bool Save()
        {
            DataTable dataTable = this._dt선택.Copy();
            dataTable.Columns.Add("CD_PC", typeof(string));
            dataTable.Columns.Add("CD_BIZAREA", typeof(string));
            dataTable.Columns.Add("CD_WDEPT", typeof(string));
            dataTable.Columns.Add("ID_WRITE", typeof(string));
            dataTable.Columns.Add("ROW_ID", typeof(string));
            string str = this._biz.순번();
            int num = 1;
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
            {
                dataRow["CD_PC"] = this.회계단위;
                dataRow["CD_BIZAREA"] = this.m_bp사업장.CodeValue;
                dataRow["CD_WDEPT"] = this.결의부서;
                dataRow["ID_WRITE"] = this.결의사원;
                dataRow["ROW_ID"] = (str + D.GetString(num).PadLeft(20 - str.Length, '0'));
                if (P_CZ_FI_EPDOCU_ETAX.is거래처자동등록 && D.GetString(dataRow["CD_PARTNER"]) == "")
                {
                    dataRow["LN_PARTNER"] = D.GetString(dataRow["NM_PARTNER"]);
                    dataRow["CD_PARTNER"] = D.GetString(this.거래처코드.Select("NM_PARTNER = '" + dataRow["LN_PARTNER"] + "' ")[0][0]);
                }
                ++num;
            }
            return DBHelper.Save(new SpInfo()
            {
                DataValue = dataTable,
                DataState = DataValueState.Added,
                CompanyID = MA.Login.회사코드,
                UserID = MA.Login.사용자아이디,
                SpNameInsert = "UP_FI_EPDOCU_ETAX_I",
                SpParamsInsert = new string[12]
        {
          "CD_COMPANY",
          "CD_PC",
          "CD_BIZAREA",
          "CD_WDEPT",
          "ID_WRITE",
          "CD_EPCODE",
          "CD_EVID",
          "NO_TAX",
          "CD_PARTNER",
          "NO_COMPANY",
          "ID_INSERT",
          "ROW_ID"
        }
            });
        }

        private bool Save거래처()
        {
            DataTable dataTable = this.거래처코드.Copy();
            return DBHelper.Save(new SpInfo()
            {
                DataValue = dataTable,
                DataState = DataValueState.Added,
                CompanyID = MA.Login.회사코드,
                UserID = MA.Login.사용자아이디,
                SpNameInsert = "UP_MA_PARTNER_AUTO_I",
                SpParamsInsert = new string[8]
        {
          "CD_COMPANY",
          "CD_PARTNER",
          "LN_PARTNER",
          "NO_COMPANY",
          "NM_CEO",
          "ADDR1",
          "ADDR2",
          "ID_INSERT"
        }
            });
        }

        internal DataTable GetLoginInfo(string 사업장)
        {
            string query = "SELECT ID_36524, PW_36524, DC_LOCATION, TP_RECEIPT, GUBUN_36524, SITE_GUBUN FROM MA_STPORT WHERE CD_BIZAREA = '" + 사업장 + "' AND CD_COMPANY = '" + MA.Login.회사코드 + "' AND CD_MODULE = 'SA'";
            return Global.MainFrame.FillDataTable(query);
        }

        private bool ETaxLogin()
        {
            DzDataRow pRow = new DzDataRow();
            pRow.AddNew("ID", this.ETAXID);
            pRow.AddNew("PASSWD", this.ETAXPWD);
            DzDataSet dzDataSet = this.axDzBill365Api1.Login(pRow);
            if (dzDataSet == null)
                return false;
            string str1 = dzDataSet.GetDzItem("Result").GetDzItem("Header").GetDzItem("RESULT").Value;
            string str2 = dzDataSet.GetDzItem("Result").GetDzItem("Header").GetDzItem("RESULT_MSG").Value;
            if (!(str1 != "00000"))
                return true;
            MessageBox.Show("로그인실패 [" + str2 + "]");
            return false;
        }

        private bool ETaxDownLoad(string[] 발행일자, string 발행구분)
        {
            DataTable dataTable = DBHelper.GetDataTable("UP_FI_TB_TAXDOWN_S", new object[2]
      {
         발행일자[0],
         발행일자[1]
      });
            string str1 = 발행구분;
            DzDataRow pr = new DzDataRow();
            pr.AddNew("SC_TAXDATE_ST", 발행일자[0]);
            pr.AddNew("SC_TAXDATE_ED", 발행일자[1]);
            pr.AddNew("AGENT_YN", str1);
            DzDataSet dzDataSet = this.axDzBill365Api1.SearchTaxAccountList(pr);
            if (dzDataSet == null)
                return false;
            string str2 = dzDataSet.GetDzItem("Result").GetDzItem("Header").GetDzItem("RESULT").Value;
            string str3 = dzDataSet.GetDzItem("Result").GetDzItem("Header").GetDzItem("RESULT_MSG").Value;
            if (str2 != "00000")
            {
                MessageBox.Show("실패 [" + str3 + "]");
                return false;
            }
            DzDataTable dzItem = dzDataSet.GetDzItem("TB_TAX");
            int count = dzItem.Count;
            for (int index = 1; index <= count; ++index)
            {
                string str4 = dzItem.GetDzItem(index).GetDzItem("NO_TAX").Value;
                if (!string.IsNullOrEmpty(str4))
                {
                    DataRow[] dataRowArray = dataTable.Select("NO_TAX = '" + str4 + "'");
                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        DataRow row = dataTable.NewRow();
                        row["NO"] = "1";
                        row["NO_TAX"] = dzItem.GetDzItem(index).GetDzItem("NO_TAX").Value;
                        row["NO_CUST"] = dzItem.GetDzItem(index).GetDzItem("NO_CUST").Value;
                        row["NO_USER"] = dzItem.GetDzItem(index).GetDzItem("NO_USER").Value;
                        row["YMD_WRITE"] = dzItem.GetDzItem(index).GetDzItem("YMD_WRITE").Value;
                        row["FG_FINAL"] = dzItem.GetDzItem(index).GetDzItem("FG_FINAL").Value;
                        row["FG_PC"] = dzItem.GetDzItem(index).GetDzItem("FG_PC").Value;
                        row["FG_IO"] = dzItem.GetDzItem(index).GetDzItem("FG_IO").Value;
                        row["FG_VAT"] = dzItem.GetDzItem(index).GetDzItem("FG_VAT").Value;
                        row["FG_BILL"] = dzItem.GetDzItem(index).GetDzItem("FG_BILL").Value;
                        row["YN_CSMT"] = dzItem.GetDzItem(index).GetDzItem("YN_CSMT").Value;
                        row["YN_TURN"] = dzItem.GetDzItem(index).GetDzItem("YN_TURN").Value;
                        row["SELL_NO_BIZ"] = dzItem.GetDzItem(index).GetDzItem("SELL_NO_BIZ").Value;
                        row["SELL_NM_CORP"] = dzItem.GetDzItem(index).GetDzItem("SELL_NM_CORP").Value;
                        row["SELL_NM_CEO"] = dzItem.GetDzItem(index).GetDzItem("SELL_NM_CEO").Value;
                        row["BUY_NO_BIZ"] = dzItem.GetDzItem(index).GetDzItem("BUY_NO_BIZ").Value;
                        row["BUY_NM_CORP"] = dzItem.GetDzItem(index).GetDzItem("BUY_NM_CORP").Value;
                        row["BUY_NM_CEO"] = dzItem.GetDzItem(index).GetDzItem("BUY_NM_CEO").Value;
                        row["AM"] = D.GetDecimal(dzItem.GetDzItem(index).GetDzItem("AM").Value);
                        row["AM_VAT"] = D.GetDecimal(dzItem.GetDzItem(index).GetDzItem("AM_VAT").Value);
                        row["DC_RMK"] = dzItem.GetDzItem(index).GetDzItem("DC_RMK").Value;
                        row["AMT"] = D.GetDecimal(dzItem.GetDzItem(index).GetDzItem("AMT").Value);
                        row["NO_ISS"] = dzItem.GetDzItem(index).GetDzItem("NO_ISS").Value;
                        row["YN_ISS"] = dzItem.GetDzItem(index).GetDzItem("YN_ISS").Value;
                        row["YMD_ISS"] = dzItem.GetDzItem(index).GetDzItem("YMD_ISS").Value;
                        row["YN_SIGNS"] = dzItem.GetDzItem(index).GetDzItem("YN_SIGNS").Value;
                        row["NO_ISS"] = dzItem.GetDzItem(index).GetDzItem("NO_ISS").Value;
                        row["NO_SENDER_PK"] = dzItem.GetDzItem(index).GetDzItem("NO_SENDER_PK").Value;
                        row["NM_SENDER_PK"] = dzItem.GetDzItem(index).GetDzItem("NM_SENDER_PK").Value;
                        row["NM_SENDER_SYS"] = dzItem.GetDzItem(index).GetDzItem("NM_SENDER_SYS").Value;
                        row["SELL_ADDR1"] = dzItem.GetDzItem(index).GetDzItem("SELL_ADDR1").Value;
                        row["SELL_ADDR2"] = dzItem.GetDzItem(index).GetDzItem("SELL_ADDR2").Value;
                        row["BUY_ADDR1"] = dzItem.GetDzItem(index).GetDzItem("BUY_ADDR1").Value;
                        row["BUY_ADDR2"] = dzItem.GetDzItem(index).GetDzItem("BUY_ADDR2").Value;
                        dataTable.Rows.Add(row);
                    }
                    else if (D.GetString(dataRowArray[0]["FG_FINAL"]) != dzItem.GetDzItem(index).GetDzItem("FG_FINAL").Value)
                        dataRowArray[0]["FG_FINAL"] = dzItem.GetDzItem(index).GetDzItem("FG_FINAL").Value;
                }
            }
            DataTable changes = dataTable.GetChanges();
            if (changes != null)
            {
                if (!DBHelper.Save(new SpInfo()
                {
                    DataValue = changes,
                    CompanyID = MA.Login.회사코드,
                    UserID = MA.Login.사용자아이디,
                    SpNameInsert = "UP_FI_TB_TAXDOWN_I",
                    SpParamsInsert = new string[33]
          {
            "NO",
            "NO_TAX",
            "NO_CUST",
            "NO_USER",
            "YMD_WRITE",
            "FG_FINAL",
            "FG_PC",
            "FG_IO",
            "FG_VAT",
            "FG_BILL",
            "YN_CSMT",
            "YN_TURN",
            "SELL_NO_BIZ",
            "SELL_NM_CORP",
            "SELL_NM_CEO",
            "BUY_NO_BIZ",
            "BUY_NM_CORP",
            "BUY_NM_CEO",
            "AM",
            "AM_VAT",
            "DC_RMK",
            "AMT",
            "NO_ISS",
            "YN_ISS",
            "YMD_ISS",
            "YN_SIGNS",
            "NO_SENDER_PK",
            "NM_SENDER_PK",
            "NM_SENDER_SYS",
            "SELL_ADDR1",
            "SELL_ADDR2",
            "BUY_ADDR1",
            "BUY_ADDR2"
          },
                    SpNameUpdate = "UP_FI_TB_TAXDOWN_U",
                    SpParamsUpdate = new string[2]
          {
            "NO_TAX",
            "FG_FINAL"
          }
                }))
                    return false;
            }
            return true;
        }

        public bool SaveExcelData(DataTable table)
        {
            bool flag = true;
            OleDbConnection connection = (OleDbConnection)null;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return false;
                connection = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"{0}\";Extended Properties=\"Excel 8.0;HDR=NO\"", openFileDialog.FileName));
                connection.Open();
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                foreach (DataColumn dataColumn in (InternalDataCollectionBase)table.Columns)
                {
                    DataRow dataRow = (DataRow)null;
                    if (table.Rows.Count > 0)
                        dataRow = table.Rows[0];
                    if (stringBuilder1.Length > 0)
                        stringBuilder1.Append(",");
                    stringBuilder1.Append("[" + dataColumn.ColumnName.Replace("'", "''") + "] CHAR(255)");
                }
                new OleDbCommand("CREATE TABLE Sheet1(" + stringBuilder1.ToString() + ")", connection).ExecuteNonQuery();
                for (int index = 0; index < table.Rows.Count; ++index)
                {
                    DataRow dataRow = table.Rows[index];
                    StringBuilder stringBuilder3 = new StringBuilder();
                    StringBuilder stringBuilder4 = new StringBuilder();
                    int num = 0;
                    foreach (DataColumn dataColumn in (InternalDataCollectionBase)table.Columns)
                    {
                        if (stringBuilder3.Length > 0)
                        {
                            stringBuilder3.Append(",");
                            stringBuilder4.Append(",");
                        }
                        stringBuilder3.Append("F" + (++num).ToString());
                        stringBuilder4.Append("'" + dataRow[dataColumn.ColumnName].ToString().Replace("'", "''") + "'");
                    }
                    new OleDbCommand("INSERT INTO [Sheet1$] (" + stringBuilder3.ToString() + ") VALUES(" + stringBuilder4.ToString() + ")", connection).ExecuteNonQuery();
                }
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
            return flag;
        }
    }
}
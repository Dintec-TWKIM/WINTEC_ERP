using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.OLD;
using Duzon.ERPU.PR;
using master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
	{
	public partial class P_CZ_PR_WORK_SUB2 : Duzon.Common.Forms.CommonDialog, IHelpWindow
    {
        private string _cdPlant = "";
        private string _cdReject = "";
        private string _nmReject = "";
        private string _cdResource = "";
        private string _nmResource = "";
        private decimal _qtReject = 0;
        private string strNO_SFT = "";
        private DataRow[] _main_row = null;
        private DataRow[] _id_row = null;
        private bool bRejectFilter = false;
        private object[] _return = new object[2];
        private IMainFrame _mf = null;
        private DataTable _dtWorkSub = null;
        private P_CZ_PR_WORK_SUB2_BIZ _biz = new P_CZ_PR_WORK_SUB2_BIZ();
        private bool b_Excel = true;
        public P_CZ_PR_WORK_SUB2(DataRow[] main_row, IMainFrame mainFrame, string nm_plant, decimal qt_reject)
        {
            this.InitializeComponent();
            if (main_row.Length <= 0)
                return;
            this._mf = mainFrame;
            this.txt공장.Text = nm_plant;
            this._cdPlant = main_row[0]["CD_PLANT"].ToString();
            this.txt작업지시번호.Text = main_row[0]["NO_WO"].ToString();
            this.txtWC.Text = main_row[0]["CD_WC"].ToString();
            this.txtOP.Text = main_row[0]["CD_OP"].ToString();
            this.m_txtCdItem.Text = main_row[0]["CD_ITEM"].ToString();
            this.m_txtNmItem.Text = main_row[0]["NM_ITEM"].ToString();
            this.m_txtStndItem.Text = main_row[0]["STND_ITEM"].ToString();
            this.m_txtUnitIm.Text = main_row[0]["UNIT_IM"].ToString();
            this.txt불량수량.Text = qt_reject.ToString();
            this._qtReject = qt_reject;
            this._main_row = main_row;
            this.Load += new EventHandler(this.OnPageLoad);
        }

        public P_CZ_PR_WORK_SUB2(DataRow[] main_row, IMainFrame mainFrame, string nm_plant, decimal qt_reject, string nmItem, string stndItem, string unitItem)
        {
            this.InitializeComponent();
            if (main_row.Length <= 0)
                return;
            this._mf = mainFrame;
            this.txt공장.Text = nm_plant;
            this._cdPlant = main_row[0]["CD_PLANT"].ToString();
            this.txt작업지시번호.Text = main_row[0]["NO_WO"].ToString();
            this.txtWC.Text = main_row[0]["CD_WC"].ToString();
            this.txtOP.Text = main_row[0]["CD_OP"].ToString();
            this.m_txtCdItem.Text = main_row[0]["CD_ITEM"].ToString();
            this.m_txtNmItem.Text = nmItem;
            this.m_txtStndItem.Text = stndItem;
            this.m_txtUnitIm.Text = unitItem;
            this.txt불량수량.Text = qt_reject.ToString();
            this._qtReject = qt_reject;
            this._main_row = main_row;
            this.Load += new EventHandler(this.OnPageLoad);
        }

        public P_CZ_PR_WORK_SUB2(DataRow[] main_row, IMainFrame mainFrame, string nm_plant, decimal qt_reject, string nmItem, string stndItem, string unitItem, string strNO_SFT, DataRow[] id_row)
        {
            this.InitializeComponent();
            if (main_row.Length <= 0)
                return;
            this._mf = mainFrame;
            this.txt공장.Text = nm_plant;
            this._cdPlant = main_row[0]["CD_PLANT"].ToString();
            this.txt작업지시번호.Text = main_row[0]["NO_WO"].ToString();
            this.txtWC.Text = main_row[0]["CD_WC"].ToString();
            this.txtOP.Text = main_row[0]["CD_OP"].ToString();
            this.m_txtCdItem.Text = main_row[0]["CD_ITEM"].ToString();
            this.m_txtNmItem.Text = nmItem;
            this.m_txtStndItem.Text = stndItem;
            this.m_txtUnitIm.Text = unitItem;
            this.txt불량수량.Text = qt_reject.ToString();
            this._qtReject = qt_reject;
            this.strNO_SFT = strNO_SFT;
            this._main_row = main_row;
            this._id_row = id_row;
            this.Load += new EventHandler(this.OnPageLoad);
        }

        public P_CZ_PR_WORK_SUB2(DataRow[] main_row, IMainFrame mainFrame, string nm_plant, decimal qt_reject, string nmItem, string stndItem, string unitItem, string strNO_SFT, string strNO_LOT)
        {
            this.InitializeComponent();
            if (main_row.Length <= 0)
                return;
            this._mf = mainFrame;
            this.txt공장.Text = nm_plant;
            this._cdPlant = main_row[0]["CD_PLANT"].ToString();
            this.txt작업지시번호.Text = main_row[0]["NO_WO"].ToString();
            this.txtWC.Text = main_row[0]["CD_WC"].ToString();
            this.txtOP.Text = main_row[0]["CD_OP"].ToString();
            this.m_txtCdItem.Text = main_row[0]["CD_ITEM"].ToString();
            this.m_txtNmItem.Text = nmItem;
            this.m_txtStndItem.Text = stndItem;
            this.m_txtUnitIm.Text = unitItem;
            this.txt불량수량.Text = qt_reject.ToString();
            this._qtReject = qt_reject;
            this.strNO_SFT = strNO_SFT;
            this.panelExt1.Visible = this.txtLOT번호.Visible = true;
            this.txtLOT번호.Text = strNO_LOT;
            this._main_row = main_row;
            this.Load += new EventHandler(this.OnPageLoad);
        }

        private void OnPageLoad(object sender, EventArgs e)
        {
            try
            {
                this.InitEvent();
                this.InitGrid();
                this.InitControl();
                this.WorkSubSearch();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

		private void InitEvent()
		{
            this.btn확인.Click += new EventHandler(this.Btn확인_Click);
            this.btn취소.Click += new EventHandler(this.Btn취소_Click);
            this.btn적용.Click += new EventHandler(this.Btn적용_Click);

            this.ctx불량원인.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
            this.ctx불량종류.QueryBefore += new BpQueryHandler(this.OnBpControl_QueryBefore);
        }

		private void InitGrid()
        {
            this._flex.BeginSetting(1, 1, false);
            this._flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_ID", "ID번호", 100);
            this._flex.SetCol("CD_REJECT", "불량코드", 90, 7, true);
            this._flex.SetCol("NM_REJECT", "불량종류", 90, true);
            this._flex.SetCol("CD_RESOURCE", "원인코드", 90, 10, true);
            this._flex.SetCol("NM_RESOURCE", "불량원인", 90, true);
            this._flex.SetCol("QT_REJECT", "불량수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex.SetCol("NO_SFT", "SFT", 60, false);
            this._flex.SetCol("DC_RMK", "비고", 160, 100, true);
            DataSet plantCfg = Pr_ComFunc.Get_Plant_Cfg(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                       this._cdPlant,
                                                                       Global.SystemLanguage.MultiLanguageLpoint });
            if (plantCfg != null && plantCfg.Tables.Count > 1 && D.GetString(plantCfg.Tables[1].Rows[0]["YN_AUTOBAD"]) == "Y" && (D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "000" || D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "001"))
            {
                this._flex.SetCol("CHK", "불량처리", 100, true, CheckTypeEnum.Y_N);
                this._flex.SetCol("CD_SL_BAD", "공정불량품창고", 100, true);
                this._flex.SetCol("NM_SL_BAD", "공정불량품창고명", 120, false);
                this._flex.SetCol("NO_LOT", "LOT번호", 100, true);
            }
            this._flex.SetCodeHelpCol("CD_REJECT", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "CD_REJECT", "NM_REJECT" }
                                                                                             , new string[] { "CODE", "NAME" }
                                                                                             , new string[] { "CD_REJECT", "NM_REJECT" }, ResultMode.SlowMode);
            this._flex.SetCodeHelpCol("CD_RESOURCE", HelpID.P_MA_TABLE_SUB, ShowHelpEnum.Always, new string[] { "CD_RESOURCE", "NM_RESOURCE" }
                                                                                                , new string[] { "CODE", "NAME" }
                                                                                                , new string[] { "CD_RESOURCE", "NM_RESOURCE" }, ResultMode.SlowMode);
            this._flex.VerifyCompare(this._flex.Cols["QT_WORK"], 0, OperatorEnum.Greater);
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);
            this._flex.SetCodeHelpCol("CD_SL_BAD", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL_BAD", "NM_SL_BAD" }, new string[] { "CD_SL", "NM_SL" }, ResultMode.SlowMode);
            this._flex.Cols["NO_SFT"].Visible = false;
            this._flex.Cols["QT_REJECT"].Visible = false;
            this._flex.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex.KeyPress += new KeyPressEventHandler(this._flex_KeyPress);
            this._flex.LoadUserCache("P_CZ_PR_WORK_SUB2_flex");
            this._flex.AddMyMenu = true;
            this._flex.AddMenuSeperator();
            this._flex.AddDummyColumn("S");
            ToolStripMenuItem parent = this._flex.AddPopup(Global.MainFrame.DD("엑셀관리"));
            this._flex.AddMenuItem(parent, Global.MainFrame.DD("엑셀양식다운로드"), new EventHandler(this._flex_Menu_Click));
            this._flex.AddMenuItem(parent, Global.MainFrame.DD("엑셀업로드"), new EventHandler(this._flex_Menu_Click));
            this._flex.EnterKeyAddRow = true;
        }

        private void InitControl()
        {
            object[] objArray = new object[]{ Global.MainFrame.LoginInfo.CompanyCode, "QU_2000007", null };
            string[] strArray = new string[] { "TSE" };
            objArray[2] = !MA.ServerKey(false, strArray) ? "" : this.txtWC.Text;
            DataTable comboData1 = this._biz.GetComboData(objArray);
            if (comboData1 != null && comboData1.Rows.Count != 0)
			{
                DataRow[] dataRowArray1 = comboData1.Select("CODE = 'O01'");
                this._cdReject = D.GetString(dataRowArray1[0]["CODE"]);
                this._nmReject = D.GetString(dataRowArray1[0]["NAME"]);
            }   
            objArray[0] = Global.MainFrame.LoginInfo.CompanyCode;
            objArray[1] = "QU_2000009";
            objArray[2] = "";
            DataTable comboData2 = this._biz.GetComboData(objArray);
            if (comboData2 != null && comboData2.Rows.Count != 0)
			{
                DataRow[] dataRowArray2 = comboData1.Select("CODE = 'O01'");
                this._cdResource = D.GetString(dataRowArray2[0]["CODE"]);
                this._nmResource = D.GetString(dataRowArray2[0]["NAME"]);
            }
            this._flex.ShowButtons = ShowButtonsEnum.Always;
            if (Global.MainFrame.GetComboData("S;QU_2000007", "S;MA_CODEDTL2;QU_2000009").Tables[1].Select("ISNULL(CD_FLAG1, '') <> ''").Length == 0)
                return;
            this.bRejectFilter = true;
        }

        private void WorkSubSearch()
        {
            this._dtWorkSub = new DataTable();
            this._dtWorkSub.Columns.Add("CD_COMPANY");
            this._dtWorkSub.Columns.Add("S");
            this._dtWorkSub.Columns.Add("NO_ID", typeof(string));
            this._dtWorkSub.Columns.Add("NO_WO");
            this._dtWorkSub.Columns.Add("NO_WORK");
            this._dtWorkSub.Columns.Add("NO_LINE", typeof(decimal));
            this._dtWorkSub.Columns.Add("CD_REJECT");
            this._dtWorkSub.Columns.Add("NM_REJECT");
            this._dtWorkSub.Columns.Add("CD_RESOURCE");
            this._dtWorkSub.Columns.Add("NM_RESOURCE");
            this._dtWorkSub.Columns.Add("TM_WORK");
            this._dtWorkSub.Columns.Add("QT_WORK", Type.GetType("System.Decimal"));
            this._dtWorkSub.Columns.Add("QT_REJECT", Type.GetType("System.Decimal"));
            this._dtWorkSub.Columns.Add("NO_SFT");
            this._dtWorkSub.Columns.Add("CHK");
            this._dtWorkSub.Columns.Add("DC_RMK");
            this._dtWorkSub.Columns.Add("CD_SL_BAD");
            this._dtWorkSub.Columns.Add("NM_SL_BAD");
            this._dtWorkSub.Columns.Add("NO_LOT");
            this._dtWorkSub.Columns.Add("CD_MNG1", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG2", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG3", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG4", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG5", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG6", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG7", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG8", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG9", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG10", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG11", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG12", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG13", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG14", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG15", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG16", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG17", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG18", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG19", typeof(string));
            this._dtWorkSub.Columns.Add("CD_MNG20", typeof(string));
            this._flex.Binding = this._dtWorkSub;

            foreach (DataRow dr in _id_row)
			{
                if (this._flex.HasNormalRow && !this.InsCheckTotReject())
                    return;
                this._flex.Rows.Add();
                this._flex.Row = this._flex.Rows.Count - 1;
                this._flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex["S"] = "N";
                this._flex["NO_ID"] = dr["NO_ID"].ToString();
                this._flex["NO_WO"] = this.txt작업지시번호.Text;
                this._flex["NO_WORK"] = "";
                decimal num = D.GetDecimal(this._flex.DataTable.Compute("MAX(NO_LINE)", ""));
                this._flex["NO_LINE"] = ++num;
                this._flex["CD_REJECT"] = this._cdReject;
                this._flex["NM_REJECT"] = this._nmReject;
                this._flex["CD_RESOURCE"] = this._cdResource;
                this._flex["NM_RESOURCE"] = this._nmResource;
                this._flex["TM_WORK"] = "";
                this._flex["QT_WORK"] = 0;
                this._flex["QT_REJECT"] = 1;
                this._flex["NO_SFT"] = this.strNO_SFT;
                DataSet plantCfg = Pr_ComFunc.Get_Plant_Cfg(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           this._cdPlant,
                                                                           Global.SystemLanguage.MultiLanguageLpoint });
                if (plantCfg != null && plantCfg.Tables.Count > 1 && D.GetString(plantCfg.Tables[1].Rows[0]["YN_AUTOBAD"]) == "Y" && (D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "000" || D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "001"))
                {
                    this._flex["CHK"] = "Y";
                    this._flex["NO_LOT"] = this.txtLOT번호.Text;
                    this._flex["CD_SL_BAD"] = "";
                    this._flex["NM_SL_BAD"] = "";
                    DataTable badCdSlSearch = this._biz.Get_Bad_Cd_SL_Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                            this._cdPlant,
                                                                                            this.txtWC.Text,
                                                                                            Global.SystemLanguage.MultiLanguageLpoint });
                    if ((badCdSlSearch != null || badCdSlSearch.Rows.Count > 0) && (this._main_row != null || this._main_row.Length > 0))
                    {
                        DataRow[] dataRowArray = badCdSlSearch.Select("CD_WCOP = '" + D.GetString(this._main_row[0]["CD_WCOP"]) + "'");
                        if (dataRowArray.Length > 0)
                        {
                            this._flex["CD_SL_BAD"] = dataRowArray[0]["CD_SL_BAD"].ToString();
                            this._flex["NM_SL_BAD"] = dataRowArray[0]["NM_SL_BAD"].ToString();
                        }
                    }
                }
                this._flex.AddFinished();
                this._flex.Col = this._flex.Cols.Fixed;
                this._flex.Focus();
            }
        }

        private bool CheckTotReject()
        {
            decimal num1 = 0M;
            object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                               Global.MainFrame.LoginInfo.CdPlant };
            decimal num2 = D.GetDecimal(this.txt불량수량.Text);
            if (this._flex.HasNormalRow)
            {
                for (int index = 0; index < this._dtWorkSub.Rows.Count; ++index)
                {
                    if (D.GetDecimal(this._dtWorkSub.Rows[index]["QT_REJECT"]) < 0M)
                    {
                        MessageBoxEx.Show(this._mf.GetMessageDictionaryItem("PR_M000018"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this._flex.Focus();
                        this._flex.Select(index + 2, "QT_REJECT");
                        return false;
                    }
                    num1 += D.GetDecimal(this._dtWorkSub.Rows[index]["QT_REJECT"]);
                    if (num2 < num1)
                    {
                        MessageBoxEx.Show(this._mf.GetMessageDictionaryItem("PR_M000019"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return false;
                    }
                    if (this._dtWorkSub.Rows[index]["CHK"].ToString().Trim().Equals("Y") && this._dtWorkSub.Rows[index]["CD_SL_BAD"].ToString() == string.Empty)
                    {
                        Global.MainFrame.ShowMessage("불량처리인 경우 공정불량창고는 필수 항목입니다");
                        return false;
                    }
                }
                if (num1 != D.GetDecimal(this.txt불량수량.Text))
                {
                    MessageBoxEx.Show(this._mf.GetMessageDictionaryItem("PR_M000019"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this._flex.Focus();
                    this._flex.Select(this._dtWorkSub.Rows.Count, "QT_REJECT");
                    return false;
                }
            }
            return true;
        }

        private bool InsCheckTotReject()
        {
            decimal num1 = 0M;
            if (this._flex.HasNormalRow)
            {
                for (int index = 0; index < this._dtWorkSub.Rows.Count; ++index)
                {
                    if (D.GetDecimal(this._dtWorkSub.Rows[index]["QT_REJECT"]) < 0M)
                    {
                        MessageBoxEx.Show(this._mf.GetMessageDictionaryItem("PR_M000018"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this._flex.Focus();
                        this._flex.Select(index + 2, "QT_REJECT");
                        return false;
                    }
                    num1 += D.GetDecimal(this._dtWorkSub.Rows[index]["QT_REJECT"]);
                }
                if (num1 >= D.GetDecimal(this.txt불량수량.Text))
                {
                    MessageBoxEx.Show(this._mf.GetMessageDictionaryItem("PR_M000020"), "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
            }
            return true;
        }

        private bool DoContinue() => this._flex.Editor == null || this._flex.FinishEditing(false);

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
			try
			{
                switch (e.HelpID)
				{
                    case HelpID.P_MA_TABLE_SUB:
                        e.HelpParam.P00_CHILD_MODE = "불량종류";
                        e.HelpParam.P61_CODE1 = "RTRIM(CD_SYSDEF) AS CODE, NM_SYSDEF AS NAME";
                        e.HelpParam.P62_CODE2 = "DZSN_MA_CODEDTL";
                        e.HelpParam.P63_CODE3 = string.Format(@"WHERE CD_COMPANY = '{0}'
AND CD_FIELD = 'QU_2000007'
AND ISNULL(USE_YN, '') = 'Y'", Global.MainFrame.LoginInfo.CompanyCode);
                        break;
                    case HelpID.P_MA_TABLE_SUB1:
                        e.HelpParam.P00_CHILD_MODE = "불량원인";
                        e.HelpParam.P61_CODE1 = "RTRIM(CD_SYSDEF) AS CODE, NM_SYSDEF AS NAME";
                        e.HelpParam.P62_CODE2 = "DZSN_MA_CODEDTL";
                        e.HelpParam.P63_CODE3 = string.Format(@"WHERE CD_COMPANY = '{0}'
AND CD_FIELD = 'QU_2000009'
AND ISNULL(USE_YN, '') = 'Y'", Global.MainFrame.LoginInfo.CompanyCode);
                        break;
                }
			}
            catch (Exception ex)
			{
                Global.MainFrame.MsgEnd(ex);
			}
        }

        private void Btn확인_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.HasNormalRow)
                {
                    for (int index = 0; index < this._dtWorkSub.Rows.Count; ++index)
                    {
                        if (D.GetDecimal(this._dtWorkSub.Rows[index]["QT_REJECT"]) <= 0M)
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, Global.MainFrame.DD("불량수량"), "0");
                            this._flex.Focus();
                            this._flex.Select(index + 2, "QT_REJECT");
                            return;
                        }
                        if (D.GetDecimal(this._dtWorkSub.Rows[index]["NO_LINE"]) <= 0M)
                        {
                            Global.MainFrame.ShowMessage("라인 추가시 필수항목이 누락된 데이터가 존재합니다. 취소후 다시 진행하세요.");
                            this._flex.Focus();
                            this._flex.Select(index + 2, "QT_REJECT");
                            return;
                        }
                        if (D.GetString(this._dtWorkSub.Rows[index]["NO_WO"]) == string.Empty)
                        {
                            Global.MainFrame.ShowMessage("라인 추가시 필수항목이 누락된 데이터가 존재합니다. 취소후 다시 진행하세요.");
                            this._flex.Focus();
                            this._flex.Select(index + 2, "QT_REJECT");
                            return;
                        }
                    }
                    if (!this.CheckTotReject())
                        return;
                    this.DialogResult = DialogResult.OK;
                    this._dtWorkSub.AcceptChanges();
                    this._return[0] = this._dtWorkSub;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void Btn취소_Click(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;

        private void Btn적용_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
			try
			{
                if (!this._flex.HasNormalRow) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0) return;

                foreach (DataRow dr in dataRowArray)
				{
                    dr["CD_REJECT"] = this.ctx불량종류.CodeValue;
                    dr["NM_REJECT"] = this.ctx불량종류.CodeName;
                    dr["CD_RESOURCE"] = this.ctx불량원인.CodeValue;
                    dr["NM_RESOURCE"] = this.ctx불량원인.CodeName;
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
                if (!(sender is FlexGrid flexGrid))
                    return;
                switch (flexGrid.Cols[flexGrid.Col].Name)
                {
                    case "NO_LOT":
                        e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (!(sender is FlexGrid flexGrid))
                    return;
                switch (flexGrid.Cols[flexGrid.Col].Name)
                {
                    case "CD_SL_BAD":
                        e.Parameter.P09_CD_PLANT = this._cdPlant;
                        break;
                }
                if (e.Parameter.HelpID == HelpID.P_MA_TABLE_SUB)
                {
                    switch (flexGrid.Cols[flexGrid.Col].Name)
                    {
                        case "CD_REJECT":
                            e.Parameter.P61_CODE1 = "CD_SYSDEF CODE, NM_SYSDEF NAME, CD_FLAG1";
                            e.Parameter.P62_CODE2 = "MA_CODEDTL";
                            e.Parameter.P63_CODE3 = "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                        AND CD_FIELD = 'QU_2000007' --불량원인\r\n                                        AND USE_YN = 'Y' ";
                            if (this.bRejectFilter)
                            {
                                HelpParam parameter = e.Parameter;
                                parameter.P63_CODE3 = parameter.P63_CODE3 + "AND CD_FLAG1  = '" + flexGrid["CD_REJECT"].ToString() + "' ";
                            }
                            e.Parameter.P64_CODE4 = "ORDER BY CD_SYSDEF ASC ";
                            break;
                        case "CD_RESOURCE":
                            e.Parameter.P61_CODE1 = "CD_SYSDEF CODE, NM_SYSDEF NAME, CD_FLAG1";
                            e.Parameter.P62_CODE2 = "MA_CODEDTL";
                            e.Parameter.P63_CODE3 = "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'\r\n                                        AND CD_FIELD = 'QU_2000009' --불량원인\r\n                                        AND USE_YN = 'Y' ";
                            if (this.bRejectFilter)
                            {
                                HelpParam parameter = e.Parameter;
                                parameter.P63_CODE3 = parameter.P63_CODE3 + "AND CD_FLAG1  = '" + flexGrid["CD_RESOURCE"].ToString() + "' ";
                            }
                            e.Parameter.P64_CODE4 = "ORDER BY CD_SYSDEF ASC ";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != '\t' || this.btn확인.Focus())
                    return;
                this.btn확인.Select();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool onValidateChk(object sender)
        {
            if (!(sender is Dass.FlexGrid.FlexGrid flexGrid))
                return false;
            switch (flexGrid.Cols[flexGrid.Col].Name)
            {
                case "QT_REJECT":
                    if (flexGrid.CDecimal(flexGrid.EditData) < 1M)
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은_보다커야합니다, Global.MainFrame.DD("불량코드"), "0");
                        flexGrid.Editor.Text = "0";
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void _flex_Menu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is ToolStripMenuItem toolStripMenuItem))
                    return;
                if (toolStripMenuItem.Name == Global.MainFrame.DD("엑셀양식다운로드"))
                {
                    ExcelDN_Convert.PopUpSetting(nameof(P_CZ_PR_WORK_SUB2));
                }
                else
                {
                    Excel excel = new Excel();
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls";
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;
                    Application.DoEvents();
                    string fileName = openFileDialog.FileName;
                    DataTable dt_Excel1 = excel.StartLoadExcel(fileName);
                    if (!dt_Excel1.Columns.Contains("NM_SL_BAD"))
                        dt_Excel1.Columns.Add("NM_SL_BAD", typeof(string));
                    DataTable dt_Excel2 = this.Verify_Excel_CD_SL_BAD(dt_Excel1);
                    if (!this.b_Excel)
                        return;
                    DataTable dt_Excel3 = this.Verify_Excel_Code(dt_Excel2, "CD_REJECT", "QU_2000007");
                    if (!this.b_Excel)
                        return;
                    DataTable dataTable = this.Verify_Excel_Code(dt_Excel3, "CD_RESOURCE", "QU_2000009");
                    if (!this.b_Excel)
                        return;
                    DataRow[] dataRowArray = dataTable.Select("ISNULL(QT_REJECT, 0) <> 0");
                    DataSet plantCfg = Pr_ComFunc.Get_Plant_Cfg(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                               this._cdPlant,
                                                                               Global.SystemLanguage.MultiLanguageLpoint });
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (D.GetDecimal(this._flex[this._flex.RowSel, "QT_REJECT"]) != 0M)
                        {
                            this._flex.Rows.Add();
                            this._flex.Row = this._flex.Rows.Count - 1;
                        }
                        this._flex["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                        this._flex["NO_WO"] = this.txt작업지시번호.Text;
                        this._flex["NO_WORK"] = "";
                        decimal num = D.GetDecimal(this._flex.DataTable.Compute("MAX(NO_LINE)", ""));
                        this._flex["NO_LINE"] = ++num;
                        this._flex["CD_REJECT"] = D.GetString(dataRow["CD_REJECT"]);
                        this._flex["CD_RESOURCE"] = D.GetString(dataRow["CD_RESOURCE"]);
                        this._flex["NM_RESOURCE"] = D.GetString(dataRow["NM_RESOURCE"]);
                        this._flex["TM_WORK"] = "";
                        this._flex["QT_WORK"] = 0;
                        this._flex["QT_REJECT"] = D.GetDecimal(dataRow["QT_REJECT"]);
                        this._flex["NO_SFT"] = this.strNO_SFT;
                        this._flex["DC_RMK"] = D.GetString(dataRow["DC_RMK"]);
                        if (plantCfg != null && plantCfg.Tables.Count > 1 && D.GetString(plantCfg.Tables[1].Rows[0]["YN_AUTOBAD"]) == "Y" && (D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "000" || D.GetString(plantCfg.Tables[1].Rows[0]["FG_AUTOBAD"]) == "001"))
                        {
                            this._flex["CHK"] = "Y";
                            this._flex["NO_LOT"] = D.GetString(dataRow["NO_LOT"]);
                            this._flex["CD_SL_BAD"] = D.GetString(dataRow["CD_SL_BAD"]);
                            this._flex["NM_SL_BAD"] = D.GetString(dataRow["NM_SL_BAD"]);
                        }
                        this._flex.AddFinished();
                        this._flex.Col = this._flex.Cols.Fixed;
                        this._flex.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this._flex.SaveUserCache("P_CZ_PR_WORK_SUB2_flex");
        }

        private DataTable Verify_Excel_Code(
          DataTable dt_Excel,
          string ColumnName,
          string CD_FIELD)
        {
            DataTable dataTable = this._biz.SearchCode(CD_FIELD, ColumnName);
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[ColumnName] };
            foreach (DataRow row in dt_Excel.Rows)
            {
                DataRow dataRow = dataTable.Rows.Find(new object[] { D.GetString(row[ColumnName]) });
                if (dataRow != null)
                {
                    if (ColumnName == "CD_RESOURCE")
                    {
                        if (D.GetString(row["NM_RESOURCE"]) != D.GetString(dataRow["NM_SYSDEF"]))
                        {
                            Global.MainFrame.ShowMessage("엑셀의 원인코드명[" + D.GetString(row["NM_RESOURCE"]) + "]\n코드관리 구분코드명[" + D.GetString(dataRow["NM_SYSDEF"]) + "]\n두 개가 서로 일치하지 않습니다.", "EK1");
                            this.b_Excel = false;
                            break;
                        }
                        row["NM_RESOURCE"] = D.GetString(dataRow["NM_SYSDEF"]);
                    }
                    else if (D.GetString(row["NM_REJECT"]) != D.GetString(dataRow["NM_SYSDEF"]))
                    {
                        Global.MainFrame.ShowMessage("엑셀의 불량명[" + D.GetString(row["NM_REJECT"]) + "]\n코드관리 구분코드명[" + D.GetString(dataRow["NM_SYSDEF"]) + "]\n두 개가 서로 일치하지 않습니다.", "EK1");
                        this.b_Excel = false;
                        break;
                    }
                    this.b_Excel = true;
                }
                else
                {
                    string empty = string.Empty;
                    string str = !(ColumnName == "CD_RESOURCE") ? "불량코드" : "원인코드";
                    Global.MainFrame.ShowMessage(str + "[" + D.GetString(row[ColumnName]) + "]은(는) 코드관리 값과 일치하지 않습니다.", "EK1");
                    this.b_Excel = false;
                    break;
                }
            }
            dt_Excel.AcceptChanges();
            return dt_Excel;
        }

        private DataTable Verify_Excel_CD_SL_BAD(DataTable dt_Excel)
        {
            DataTable dataTable = this._biz.SearchCD_SL(this._cdPlant);
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["CD_SL_BAD"] };
            foreach (DataRow row in dt_Excel.Rows)
            {
                DataRow dataRow = dataTable.Rows.Find(new object[] { D.GetString(row["CD_SL_BAD"]) });
                if (dataRow != null)
                {
                    row["NM_SL_BAD"] = D.GetString(dataRow["NM_SL"]);
                    this.b_Excel = true;
                }
                else
                {
                    Global.MainFrame.ShowMessage("공정불량품창고[" + D.GetString(row["CD_SL_BAD"]) + "]은(는) S/L정보 값과 일치하지 않습니다.", "EK1");
                    this.b_Excel = false;
                    break;
                }
            }
            dt_Excel.AcceptChanges();
            return dt_Excel;
        }

        object[] IHelpWindow.ReturnValues => this._return;

    }
}

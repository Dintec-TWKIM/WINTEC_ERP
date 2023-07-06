using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.OLD;
using sale;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PU_LOT_SUB_R : Duzon.Common.Forms.CommonDialog
	{
        private P_CZ_PU_LOT_SUB_R_BIZ _biz = new P_CZ_PU_LOT_SUB_R_BIZ();
        private OpenFileDialog m_FileDlg = new OpenFileDialog();
        private DataTable _dt_EXCEL = null;
        private bool ExcelChk = true;
        public DataTable _dt = null;
        public DataTable _dtL = null;
        private string _m_lot_use = BASIC.GetMAEXC("LOT유효일자관리여부");
        private string _m_lot_set = BASIC.GetMAEXC("LOT도움창설정");
        private string _FG_PS = string.Empty;
        private string _pageid = string.Empty;
        private string[] _value = new string[0];
        private string _m_app_lot = BASIC.GetMAEXC("구매입고-의뢰LOT 자동적용");

        public P_CZ_PU_LOT_SUB_R(DataTable dt)
		{
			this.InitializeComponent();

            this._dt = dt;
            if (!(this._dt.Rows[0]["FG_IO"].ToString() != "041"))
                return;
            this.btn출고LOT내역.Visible = false;
        }

        public P_CZ_PU_LOT_SUB_R(DataTable dt, string[] value)
        {
            this.InitializeComponent();
            this._dt = dt;
            if (this._dt.Rows[0]["FG_IO"].ToString() != "041")
                this.btn출고LOT내역.Visible = false;
            this._value = value;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();

            if (MA.ServerKey(false, "AXT"))
            {
                if (!this.chk_BARCODE(this._dt))
                {
                    Global.MainFrame.ShowMessage("BARCODE가 등록 안된 품목이 존재합니다.");
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }
            }
            if (MA.ServerKey(false, "KYOTECH"))
            {
                if (Global.MainFrame.CurrentPageID == "P_PU_GR_REG" || Global.MainFrame.CurrentPageID == "P_PU_ITR_REG" || Global.MainFrame.CurrentPageID == "P_SA_GIRE_REG")
                    this.setDataMap_KYOTECH();
                if (Global.MainFrame.CurrentPageID == "P_PU_GR_REG")
                    this._dt = this.getMergeDT_KYOTECH(this._dt);
            }
            this._flexM.Cols["NO_IO_MGMT"].Visible = false;
            this._flexM.Cols["NO_IOLINE_MGMT"].Visible = false;
            this._flexM.Cols["FG_IO"].Visible = false;
            this._flexM.Cols["CD_QTIOTP"].Visible = false;
            this._flexM.DetailGrids = new FlexGrid[] { this._flexD };
            DataTable dataTable = this._biz.Search_Detail("");
            this._flexD.Binding = dataTable;
            this._flexM.Binding = this._dt;
            this._dtL = dataTable;
            this.Set_default_linedata(this._dt);
        }

		private void Set_default_linedata(DataTable dt)
        {
            try
            {
                if (this._flexD.DataTable == null)
                    return;
                foreach (DataRow row in dt.Rows)
                {
                    string filterExpression = "NO_IO = '" + D.GetString(row["NO_IO"]) + "' AND NO_IOLINE = " + D.GetString(row["NO_IOLINE"]);
                    if (this._flexD.DataTable.Select(filterExpression).Length != 0)
                    {
                        if (this._m_app_lot == "100" && !(Global.MainFrame.CurrentPageID == "P_PU_SGR_REG") && !(Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG"))
                        {
                            if (dt.Columns.Contains("NO_LOT"))
                                this._flexD["NO_LOT"] = row["NO_LOT"];
                            if (dt.Columns.Contains("DT_LIMIT_LOT"))
                                this._flexD["DT_LIMIT"] = D.GetString(this._flexD["NO_LOT"]) == "" ? "" : row["DT_LIMIT_LOT"];
                        }
                        if ((Global.MainFrame.CurrentPageID == "P_PU_SGR_REG" || Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG") && Global.MainFrame.ServerKeyCommon == "SANSUNG")
                        {
                            this._flexD["NO_LOT"] = row["NO_LOT_SAN"];
                            this._flexD["DT_LIMIT"] = row["DT_LIMIT_SAN"];
                        }
                        else if (Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG" && Global.MainFrame.ServerKeyCommon.Contains("DAIKIN"))
                        {
                            this._flexD["NO_LOT"] = row["NO_LOT_SAN"];
                        }
                        else
                        {
                            if (dt.Columns.Contains("NO_LOT_SAN"))
                                this._flexD["NO_LOT"] = row["NO_LOT_SAN"];
                            if (dt.Columns.Contains("DT_LIMIT_SAN"))
                                this._flexD["DT_LIMIT"] = D.GetString(this._flexD["NO_LOT"]) == "" ? "" : row["DT_LIMIT_SAN"];
                        }
                        if (Global.MainFrame.ServerKeyCommon == "MAIIM" && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
                            this._flexD["CD_MNG3"] = row["NM_USERDEF1_RCV"];
                        if (MA.ServerKey(false, "CSFOOD") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1" && (Global.MainFrame.LoginInfo.CompanyCode == "2000" || Global.MainFrame.LoginInfo.CompanyCode == "TEST2"))
                        {
                            this._flexD["NO_LOT"] = row["NO_IO"];
                            this._flexD["CD_MNG1"] = row["CD_USERDEF1_RCV"];
                            this._flexD["CD_MNG2"] = row["NM_USERDEF1_RCV"];
                            this._flexD["CD_MNG3"] = row["CD_USERDEF2_RCV"];
                            this._flexD["CD_MNG4"] = row["NM_USERDEF2_RCV"];
                            this._flexD["CD_MNG5"] = row["CD_USERDEF3_IO"];
                            this._flexD["CD_MNG6"] = row["NM_USERDEF3_IO"];
                            this._flexD["CD_MNG7"] = row["CD_USERDEF4_IO"];
                            this._flexD["CD_MNG8"] = row["NM_USERDEF4_IO"];
                        }
                        if (MA.ServerKey(false, "DAOU") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
                            this.setMNG_DAOU(D.GetString(row["NO_PO"]), D.GetDecimal(row["NO_POLINE"]));
                        if (MA.ServerKey(false, "SANSUNG") && this._flexD.Rows.Count == 1 + this._flexD.Rows.Fixed && Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG" && D.GetString(row["CD_USERDEF2_ITEM"]) == "01" && (D.GetString(row["CLS_ITEM"]) == "003" || D.GetString(row["CLS_ITEM"]) == "004"))
                            this._flexD["NO_LOT"] = (D.GetString(row["NO_ISURCV"]) + "-" + D.GetString(row["NO_ISURCVLINE"]));
                    }
                    else
                    {
                        this._flexD.Rows.Add();
                        this._flexD.Row = this._flexD.Rows.Count - 1;
                        this._flexD["S"] = "N";
                        this._flexD["CD_ITEM"] = row["CD_ITEM"];
                        this._flexD["NO_IO"] = row["NO_IO"];
                        this._flexD["NO_IOLINE"] = row["NO_IOLINE"];
                        this._flexD["DT_IO"] = row["DT_IO"];
                        this._flexD["FG_IO"] = row["FG_IO"];
                        this._flexD["CD_QTIOTP"] = row["CD_QTIOTP"];
                        this._flexD["CD_SL"] = row["CD_SL"];
                        this._flexD["QT_IO"] = this._flexD.Rows.Count != 2 ? 0 : row["QT_GOOD_INV"];
                        this._flexD["FG_PS"] = "1";
                        if (this._value.Length > 0 && this._flexD.DataTable.Select(filterExpression).Length == 0)
                        {
                            this._flexD["NO_LOT"] = D.GetString(this._value[0]);
                            this._flexD["QT_IO"] = row["QT_GOOD_INV"];
                        }
                        else
                        {
                            if (this._m_app_lot == "100" && !(Global.MainFrame.CurrentPageID == "P_PU_SGR_REG") && !(Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG"))
                            {
                                if (dt.Columns.Contains("NO_LOT"))
                                    this._flexD["NO_LOT"] = row["NO_LOT"];
                                if (dt.Columns.Contains("DT_LIMIT_LOT"))
                                    this._flexD["DT_LIMIT"] = D.GetString(this._flexD["NO_LOT"]) == "" ? "" : row["DT_LIMIT_LOT"];
                            }
                            else
                                this._flexD["NO_LOT"] = "";
                            this._flexD["QT_IO"] = row["QT_GOOD_INV"];
                        }
                        if ((Global.MainFrame.CurrentPageID == "P_PU_SGR_REG" || Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG") && Global.MainFrame.ServerKeyCommon == "SANSUNG")
                        {
                            this._flexD["NO_LOT"] = row["NO_LOT_SAN"];
                            this._flexD["DT_LIMIT"] = row["DT_LIMIT_SAN"];
                        }
                        else if (Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG" && Global.MainFrame.ServerKeyCommon.Contains("DAIKIN"))
                        {
                            this._flexD["NO_LOT"] = row["NO_LOT_SAN"];
                        }
                        else
                        {
                            if (dt.Columns.Contains("NO_LOT_SAN"))
                                this._flexD["NO_LOT"] = row["NO_LOT_SAN"];
                            if (dt.Columns.Contains("DT_LIMIT_SAN"))
                                this._flexD["DT_LIMIT"] = D.GetString(this._flexD["NO_LOT"]) == "" ? "" : row["DT_LIMIT_SAN"];
                        }
                        if (Global.MainFrame.ServerKeyCommon == "MAIIM" && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
                            this._flexD["CD_MNG3"] = row["NM_USERDEF1_RCV"];
                        if (MA.ServerKey(false, "CSFOOD") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1" && (Global.MainFrame.LoginInfo.CompanyCode == "2000" || Global.MainFrame.LoginInfo.CompanyCode == "TEST2"))
                        {
                            this._flexD["NO_LOT"] = row["NO_IO"];
                            this._flexD["CD_MNG1"] = row["CD_USERDEF1_RCV"];
                            this._flexD["CD_MNG2"] = row["NM_USERDEF1_RCV"];
                            this._flexD["CD_MNG3"] = row["CD_USERDEF2_RCV"];
                            this._flexD["CD_MNG4"] = row["NM_USERDEF2_RCV"];
                            this._flexD["CD_MNG5"] = row["CD_USERDEF3_IO"];
                            this._flexD["CD_MNG6"] = row["NM_USERDEF3_IO"];
                            this._flexD["CD_MNG7"] = row["CD_USERDEF4_IO"];
                            this._flexD["CD_MNG8"] = row["NM_USERDEF4_IO"];
                        }
                        if (Global.MainFrame.ServerKeyCommon == "YWD" && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
                            this._flexD["NO_LOT"] = row["DC_RMK"];
                        if (MA.ServerKey(false, "DAOU") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
                            this.setMNG_DAOU(D.GetString(row["NO_PO"]), D.GetDecimal(row["NO_POLINE"]));
                        if (MA.ServerKey(false, "AXT"))
                        {
                            this._flexD["NO_LOT"] = this.get_BARCODE_AXT(D.GetString(row["CD_PLANT"]), D.GetString(row["CD_ITEM"]), D.GetString(row["DT_IO"]), D.GetString(row["AXT_Z_BARCODE"]));
                            this._flexD["QT_IO"] = 1;
                            this.calcQT(0M);
                        }
                        if (MA.ServerKey(false, "DAIWA") && (Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1" || Global.MainFrame.CurrentPageID == "P_PU_GR_REG"))
                            this._flexD["NO_LOT"] = this.get_LOT_DAIWA(D.GetString(row["CD_ITEM"]), D.GetString(row["DT_IO"]), D.GetString(row["CD_PARTNER"]));
                        if (MA.ServerKey(false, "ANYONE"))
                            this._flexD["CD_MNG5"] = row["DT_IO"];
                        if (MA.ServerKey(false, "SANSUNG") && this._flexD.Rows.Count == 1 + this._flexD.Rows.Fixed && Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG" && D.GetString(row["CD_USERDEF2_ITEM"]) == "01" && (D.GetString(row["CLS_ITEM"]) == "003" || D.GetString(row["CLS_ITEM"]) == "004"))
                            this._flexD["NO_LOT"] = (D.GetString(row["NO_ISURCV"]) + "-" + D.GetString(row["NO_ISURCVLINE"]));
                        this._flexD.AddFinished();
                        this._flexD.Col = this._flexD.Cols.Fixed;
                        this._flexD.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
			#region _flexM
			this._flexM.BeginSetting(1, 1, false);
            this._flexM.SetCol("NO_IO_MGMT", "", 120);
            this._flexM.SetCol("NO_IOLINE_MGMT", "", 120);
            this._flexM.SetCol("FG_IO", "", 120);
            this._flexM.SetCol("CD_QTIOTP", "", 120);
            this._flexM.SetCol("DT_IO", "수불일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexM.SetCol("NM_QTIOTP", "수불형태", 120);
            this._flexM.SetCol("NO_IOLINE", "입고항번", 120);
            this._flexM.SetCol("CD_ITEM", "품목코드", 120);
            this._flexM.SetCol("NM_ITEM", "품목명", 120);
            this._flexM.SetCol("UNIT_IM", "단위", 80);
            this._flexM.SetCol("STND_ITEM", "규격", 120);
            this._flexM.SetCol("QT_GOOD_INV", "처리수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexM.Cols["QT_GOOD_INV"].Format = "#,###,###.####";
            this._flexM.SetCol("CD_SL", "창고코드", 120);
            this._flexM.SetCol("NM_SL", "창고명", 120);
            this._flexM.SetCol("CD_PJT", "프로젝트코드", 120);
            this._flexM.SetCol("NM_PROJECT", "프로젝트명", 120);
            this._flexM.SetCol("UM_EX", "단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexM.SetCol("AM_EX", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexM.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            if (Global.MainFrame.ServerKeyCommon == "MAIIM" && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
            {
                this._flexM.SetCol("NO_LOT", "LOT번호(마임)", 120, false);
                this._flexM.SetCol("NM_USERDEF1_RCV", "LOT번호(업체)", 120, false);
            }
            if (MA.ServerKey(false, "THV") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
            {
                this._flexM.SetCol("CD_THV1", "가입고번호", 100);
                this._flexM.SetCol("CD_THV2", "함량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexM.SetCol("CD_THV3", "유효일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexM.SetCol("CD_THV4", "제조사LOT번호", 100, false);
            }
            if (MA.ServerKey(false, "KYOTECH") && Global.MainFrame.CurrentPageID == "P_PU_GR_REG")
                this.setHeaderGrid_KYOTECH();
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexM.LoadUserCache("P_PU_LOT_SUB_flexM");
            #endregion

            #region _flexD
            this._flexD.BeginSetting(1, 1, true);
            this._flexD.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexD.SetCol("NO_LOT", "LOT번호", 100, true);
            if (!MA.ServerKey(false, "AXT"))
			{
                this._flexD.SetCol("QT_IO", "처리수량", 120, true, typeof(decimal), FormatTpType.QUANTITY);
                this._flexD.Cols["QT_IO"].Format = "#,###,###.####";
            }                                
            if (this._m_lot_use == "Y" || this._m_lot_use == "100")
                this._flexD.SetCol("DT_LIMIT", "유효일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexD.SetCol("DC_LOTRMK", "LOT비고", 100, true);
            DataTable code = MA.GetCode("PU_C000079");
            foreach (DataRow row in code.Rows)
            {
                int cnt = D.GetInt(row["CODE"]);
                string str = "CD_MNG" + cnt;
                if (Global.MainFrame.ServerKeyCommon.Contains("KUMBI") && cnt == 2)
                    this._flexD.SetCol(str, D.GetString(row["NAME"]), 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                else if (MA.ServerKey(false, "THV") && cnt == 1)
                    this._flexD.SetCol(str, D.GetString(row["NAME"]), 100, true, typeof(decimal), FormatTpType.RATE);
                else if (MA.ServerKey(false, "KYOTECH") && (Global.MainFrame.CurrentPageID == "P_PU_GR_REG" || Global.MainFrame.CurrentPageID == "P_PU_ITR_REG" || Global.MainFrame.CurrentPageID == "P_SA_GIRE_REG"))
                    this.setGrid_MNG_KYOTECH(cnt, str, row);
                else if (Global.MainFrame.ServerKeyCommon == "YHPLA" && cnt == 1)
                    this._flexD.SetCol(str, D.GetString(row["NAME"]), 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                else
                    this._flexD.SetCol(str, D.GetString(row["NAME"]), 100);
                if (Global.MainFrame.ServerKeyCommon == "KUMBI" && this._flexD.Cols.Contains("CD_MNG19") && this._flexD.Cols.Contains("CD_MNG20"))
                {
                    this._flexD.SetCodeHelpCol("CD_MNG19", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, new string[] { "CD_MNG19", "CD_MNG20" }, 
                                                                                                      new string[] { "CD_SYSDEF", "NM_SYSDEF" });
                    this._flexD.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flexD_BeforeCodeHelp);
                    this._flexD.SetExceptEditCol("CD_MNG20");
                }
            }
            this._flexD.SetDummyColumn("S");
            this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            List<string> stringList = new List<string>();
            if (this._m_lot_set != "001" && this._m_lot_set != "002")
                this._flexD.VerifyAutoDelete = new string[] { "NO_LOT" };
            if (this._m_lot_use == "100")
                stringList.Add("DT_LIMIT");
            if (MA.ServerKey(false, "CSFOOD") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1" && (Global.MainFrame.LoginInfo.CompanyCode == "2000" || Global.MainFrame.LoginInfo.CompanyCode == "TEST2"))
                this.setEnableColumn();
            int num;
            if (!MA.ServerKey(false, "AXT"))
                num = !MA.ServerKey(false, "DAIWA") ? 1 : (Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1" ? 0 : (!(Global.MainFrame.CurrentPageID == "P_PU_GR_REG") ? 1 : 0));
            else
                num = 0;
            if (num == 0)
                this._flexD.SetExceptEditCol("NO_LOT");
            if (MA.ServerKey(false, "THV") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
                stringList.Add("DT_LIMIT");
            else if (Global.MainFrame.ServerKeyCommon == "KYOTECH" && (Global.MainFrame.CurrentPageID == "P_PU_GR_REG" || Global.MainFrame.CurrentPageID == "P_PU_ITR_REG" || Global.MainFrame.CurrentPageID == "P_SA_GIRE_REG"))
            {
                foreach (DataRow row in code.Rows)
                {
                    if (D.GetString(row["CD_FLAG1"]) == "Y")
                        stringList.Add("CD_MNG" + D.GetInt(row["CODE"]));
                }
            }
            this._flexD.VerifyNotNull = stringList.ToArray();
            this._flexD.LoadUserCache("P_PU_LOT_SUB_flexD");
            #endregion
        }

        private void InitEvent()
        {
            this._flexM.AfterRowChange += new RangeEventHandler(this._flexM_AfterRowChange);
            this._flexD.ValidateEdit += new ValidateEditEventHandler(this._flexD_ValidateEdit);

            this.btn종료.Click += new EventHandler(this.종료_Click);
            this.btn확인.Click += new EventHandler(this.확인_Click);
            this.btn삭제.Click += new EventHandler(this.삭제_Click);
            this.btn추가.Click += new EventHandler(this.추가_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
            this.btn출고LOT내역.Click += new EventHandler(this.btn출고LOT내역_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();
            if (this._pageid == "P_SA_GI_SWITCH_YN_AM")
                this.Set_Line();
            if (Global.MainFrame.ServerKeyCommon == "MAKUS" || Global.MainFrame.ServerKeyCommon == "AMOS")
            {
                this.btn자동채번.Visible = true;
                this.btn자동채번.Click += new EventHandler(this.btm자동채번_Click);
            }
            if (Global.MainFrame.ServerKeyCommon == "KEUNDAN" && (Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1" || Global.MainFrame.CurrentPageID == "P_PU_ITR_REG"))
            {
                this.btn자동채번.Visible = true;
                this.btn자동채번.Click += new EventHandler(this.btm자동채번_KEUNDAN_Click);
            }
        }

        private void 추가_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexD.DataTable == null)
                    return;
                this.btn추가.Enabled = false;
                this.line_add();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this.btn추가.Enabled = true;
            }
        }

        private void 삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                if (this._flexD.DataTable.Select("S ='Y'").Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    this._flexD.Redraw = false;
                    for (int index = this._flexD.Rows.Count - 1; index >= this._flexD.Rows.Fixed; --index)
                    {
                        if (this._flexD[index, "S"].ToString() == "Y")
                            this._flexD.Rows.Remove(index);
                    }
                    this._flexD.Redraw = true;
                    this.calcQT(0M);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 확인_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.IsBindingEnd || !this.check_save())
                    return;
                for (int row = 1; row < this._flexM.Rows.Count; ++row)
                {
                    string str1 = this._flexM[row, "NO_IO"].ToString();
                    string str2 = this._flexM[row, "NO_IOLINE"].ToString();
                    decimal num1 = 0M;
                    DataTable dataTable = this._flexD.DataTable;
                    string filterExpression = "NO_IO = '" + str1 + "' AND NO_IOLINE = " + str2 + " ";
                    foreach (DataRow dataRow in dataTable.Select(filterExpression))
                    {
                        if (this._flexM.CDecimal(dataRow["QT_IO"].ToString()) == 0M)
                        {
                            Global.MainFrame.ShowMessage("LOT수량은 0보다 커야합니다!");
                            return;
                        }
                        num1 += this._flexM.CDecimal(dataRow["QT_IO"].ToString());
                    }
                    if (this._flexM.CDecimal(this._flexM[row, "QT_GOOD_INV"]) != num1)
                    {
                        Global.MainFrame.ShowMessage("품목코드 '" + this._flexM[row, "CD_ITEM"].ToString() + "' 의 처리수량 = [" + this._flexM.CDecimal(this._flexM[row, "QT_GOOD_INV"]) + "] 과 LOT수량합 = [" + num1 + "] 이 일치하지 않습니다!");
                        return;
                    }
                }
                DataTable changes = this._flexD.GetChanges();
                if (changes == null)
                    return;
                this._dtL = changes;
                this.DialogResult = DialogResult.OK;
            }
            catch (coDbException ex)
            {
                Global.MainFrame.ShowErrorMessage(ex, "");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool check_save()
        {
            try
            {
                if (!this._flexD.Verify())
                    return false;
                foreach (DataRow dataRow in this._flexD.DataTable.Select())
                {
                    if (D.GetString(dataRow["NO_LOT"]) == "")
                    {
                        Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, "LOT NO");
                        return false;
                    }
                    dataRow["NO_LOT"] = D.GetString(dataRow["NO_LOT"]);
                    int num1;
                    if (!Global.MainFrame.ServerKeyCommon.Contains("KEUNDAN") || !(D.GetString(this._flexM["FG_IO"]) == "001") && !(D.GetString(this._flexM["FG_IO"]) == "007"))
                        num1 = !MA.ServerKey(false, "THV") ? 1 : (!(Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1") ? 1 : 0);
                    else
                        num1 = 0;
                    if (num1 == 0)
                    {
                        string str = "";
                        DataRow[] dataRowArray = this._flexM.DataTable.Select("NO_IO = '" + D.GetString(dataRow["NO_IO"]) + "' AND NO_IOLINE = '" + D.GetString(dataRow["NO_IOLINE"]) + "'");
                        if (dataRowArray != null && dataRowArray.Length > 0)
                            str = D.GetString(dataRowArray[0]["CLS_ITEM"]);
                        if (D.GetString(dataRow["CD_MNG1"]) == "" && str == "001")
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, Global.MainFrame.DD("관리항목1"));
                            return false;
                        }
                        if (Global.MainFrame.ServerKeyCommon.Contains("KEUNDAN") && (D.GetString(this._flexM["FG_IO"]) == "001" || D.GetString(this._flexM["FG_IO"]) == "007") && D.GetString(dataRow["DT_LIMIT"]) == "" && this._m_lot_use == "Y" && str == "001")
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, Global.MainFrame.DD("유효일자"));
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return false;
            }
        }

        private void 종료_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn출고LOT내역_Click(object sender, EventArgs e)
        {
            string[] strArray = new string[4]
            {
        this._flexM["NO_IO_MGMT"].ToString(),
        this._flexM["NO_IOLINE_MGMT"].ToString(),
        this._flexM["CD_ITEM"].ToString(),
        null
            };
            if (MA.ServerKey(false, "THV"))
                strArray[3] = this._flexM["CD_PARTNER"].ToString();
            P_SA_GI_LOT_SUB pSaGiLotSub = new P_SA_GI_LOT_SUB(strArray);
            int num = 0;
            if (((Form)pSaGiLotSub).ShowDialog((IWin32Window)this) != DialogResult.OK)
                return;
            this._flexD["NO_LOT"] = pSaGiLotSub.sReturn;
            if (MA.ServerKey(false, "CSFOOD") && (Global.MainFrame.LoginInfo.CompanyCode == "2000" || Global.MainFrame.LoginInfo.CompanyCode == "TEST2"))
            {
                foreach (string val in pSaGiLotSub.sReturn_MNG)
                {
                    if (num != 8)
                    {
                        this._flexD["CD_MNG" + D.GetString((num + 1))] = D.GetString(val);
                        ++num;
                    }
                    else
                        break;
                }
            }
            if (Global.MainFrame.ServerKeyCommon.Contains("KYOTECH"))
            {
                foreach (string val in pSaGiLotSub.sReturn_MNG)
                {
                    if (num != 10)
                        this._flexD["CD_MNG" + D.GetString((num + 1))] = D.GetString(val);
                    ++num;
                }
            }
        }

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ExcelChk)
                {
                    this.m_FileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";
                    foreach (DataRow row in this._flexM.DataTable.Rows)
                    {
                        if (this._flexM.DataTable.Select(" NO_IOLINE = " + row["NO_IOLINE"].ToString() + " ", "", DataViewRowState.CurrentRows).Length > 1)
                        {
                            Global.MainFrame.ShowMessage("출고항번이 중복되어 엑셀 업로드가 불가합니다.");
                            return;
                        }
                    }
                    if (this.m_FileDlg.ShowDialog() == DialogResult.OK)
                    {
                        Application.DoEvents();
                        string fileName = this.m_FileDlg.FileName;
                        string empty1 = string.Empty;
                        string 멀티품목코드 = string.Empty;
                        string empty2 = string.Empty;
                        string empty3 = string.Empty;
                        bool flag1 = false;
                        bool flag2 = false;
                        string empty4 = string.Empty;
                        string empty5 = string.Empty;
                        string empty6 = string.Empty;
                        bool flag3 = false;
                        bool flag4 = false;
                        this._dt_EXCEL = new Excel().StartLoadExcel(fileName);
                        int num1 = this._flexD.Rows.Count - this._flexD.Rows.Fixed;
                        this._flexD.Redraw = false;
                        this._flexD.EmptyRowFilter();
                        DataTable dataTable1 = this._biz.엑셀(this._dt_EXCEL);
                        DataTable dataTable2 = dataTable1.Clone();
                        dataTable2.Columns["CD_ITEM"].DataType = typeof(string);
                        dataTable2.Columns["NO_LOT"].DataType = typeof(string);
                        if (dataTable2.Columns.Contains("QT_IO"))
                            dataTable2.Columns["QT_IO"].DataType = typeof(decimal);
                        if (dataTable2.Columns.Contains("NO_IOLINE"))
                            dataTable2.Columns["NO_IOLINE"].DataType = typeof(decimal);
                        dataTable2.Columns["CD_MNG1"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG2"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG3"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG4"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG5"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG6"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG7"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG8"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG9"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG10"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG11"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG12"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG13"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG14"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG15"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG16"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG17"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG18"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG19"].DataType = typeof(string);
                        dataTable2.Columns["CD_MNG20"].DataType = typeof(string);
                        if (dataTable2.Columns.Contains("DC_LOTRMK"))
                        {
                            dataTable2.Columns["DC_LOTRMK"].DataType = typeof(string);
                            flag3 = true;
                        }
                        if (dataTable2.Columns.Contains("DT_LIMIT"))
                            flag4 = true;
                        foreach (DataRow row in dataTable1.Rows)
                            dataTable2.Rows.Add(row.ItemArray);
                        DataTable dataTable3 = dataTable2.Clone();
                        DataTable dataTable4 = dataTable2.Clone();
                        StringBuilder stringBuilder = new StringBuilder();
                        string str1 = "입고항번     품목코드     LOT번호   수 량";
                        stringBuilder.AppendLine(str1);
                        string str2 = "-".PadRight(60, '-');
                        stringBuilder.AppendLine(str2);
                        foreach (DataRow row in dataTable2.Rows)
                        {
                            if (row["CD_ITEM"].ToString().Trim() != null && !(row["CD_ITEM"].ToString().Trim() == string.Empty) && !(row["CD_ITEM"].ToString().Trim() == "") && row["NO_IOLINE"].ToString().Trim() != null && !(row["NO_IOLINE"].ToString().Trim() == string.Empty) && !(row["NO_IOLINE"].ToString().Trim() == ""))
                            {
                                if (this._dt.Select(" NO_IOLINE = " + row["NO_IOLINE"].ToString().Trim() + " AND CD_ITEM = '" + row["CD_ITEM"].ToString().Trim() + "' ", "", DataViewRowState.CurrentRows).Length <= 0)
                                {
                                    string str3 = row["NO_IOLINE"].ToString().PadRight(10, ' ') + " " + row["CD_ITEM"].ToString().PadRight(10, ' ') + " " + row["NO_LOT"].ToString().PadRight(10, ' ') + " " + row["QT_IO"].ToString();
                                    stringBuilder.AppendLine(str3);
                                    flag1 = true;
                                }
                            }
                        }
                        if (flag1)
                        {
                            Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 존재하지안는 (항번/품목)이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                            this._flexD.RowFilter = "NO_IOLINE = " + this._flexM[this._flexM.Row, "NO_IOLINE"].ToString() + " ";
                            this._flexD.Redraw = true;
                        }
                        else
                        {
                            DataTable dataTable5 = dataTable2.Copy();
                            foreach (DataRow row1 in dataTable2.Rows)
                            {
                                if (row1["CD_ITEM"].ToString().Trim() != null && !(row1["CD_ITEM"].ToString().Trim() == string.Empty) && !(row1["CD_ITEM"].ToString().Trim() == "") && row1["NO_IOLINE"].ToString().Trim() != null && !(row1["NO_IOLINE"].ToString().Trim() == string.Empty) && !(row1["NO_IOLINE"].ToString().Trim() == ""))
                                {
                                    string filterExpression = " NO_IOLINE = " + row1["NO_IOLINE"].ToString().Trim() + " AND CD_ITEM = '" + row1["CD_ITEM"].ToString().Trim() + "' AND NO_LOT = '" + row1["NO_LOT"].ToString().Trim() + "' ";
                                    if (dataTable5.Select(filterExpression, "", DataViewRowState.CurrentRows).Length == 1)
                                    {
                                        DataRow row2 = dataTable4.NewRow();
                                        row2["S"] = "Y";
                                        row2["CD_ITEM"] = row1["CD_ITEM"];
                                        row2["NO_IO"] = "";
                                        row2["NO_IOLINE"] = row1["NO_IOLINE"];
                                        row2["DT_IO"] = "";
                                        row2["FG_IO"] = "";
                                        row2["CD_QTIOTP"] = "";
                                        row2["CD_SL"] = "";
                                        row2["QT_IO"] = Convert.ToDecimal(row1["QT_IO"]);
                                        row2["FG_PS"] = "1";
                                        row2["NO_LOT"] = D.GetString(row1["NO_LOT"]);
                                        row2["CD_MNG1"] = row1["CD_MNG1"].ToString().Trim();
                                        row2["CD_MNG2"] = row1["CD_MNG2"].ToString().Trim();
                                        row2["CD_MNG3"] = row1["CD_MNG3"].ToString().Trim();
                                        row2["CD_MNG4"] = row1["CD_MNG4"].ToString().Trim();
                                        row2["CD_MNG5"] = row1["CD_MNG5"].ToString().Trim();
                                        row2["CD_MNG6"] = row1["CD_MNG6"].ToString().Trim();
                                        row2["CD_MNG7"] = row1["CD_MNG7"].ToString().Trim();
                                        row2["CD_MNG8"] = row1["CD_MNG8"].ToString().Trim();
                                        row2["CD_MNG9"] = row1["CD_MNG9"].ToString().Trim();
                                        row2["CD_MNG10"] = row1["CD_MNG10"].ToString().Trim();
                                        row2["CD_MNG11"] = row1["CD_MNG11"].ToString().Trim();
                                        row2["CD_MNG12"] = row1["CD_MNG12"].ToString().Trim();
                                        row2["CD_MNG13"] = row1["CD_MNG13"].ToString().Trim();
                                        row2["CD_MNG14"] = row1["CD_MNG14"].ToString().Trim();
                                        row2["CD_MNG15"] = row1["CD_MNG15"].ToString().Trim();
                                        row2["CD_MNG16"] = row1["CD_MNG16"].ToString().Trim();
                                        row2["CD_MNG17"] = row1["CD_MNG17"].ToString().Trim();
                                        row2["CD_MNG18"] = row1["CD_MNG18"].ToString().Trim();
                                        row2["CD_MNG19"] = row1["CD_MNG19"].ToString().Trim();
                                        row2["CD_MNG20"] = row1["CD_MNG20"].ToString().Trim();
                                        if (flag4)
                                            row2["DT_LIMIT"] = row1["DT_LIMIT"].ToString().Trim();
                                        if (flag3)
                                            row2["DC_LOTRMK"] = row1["DC_LOTRMK"].ToString().Trim();
                                        dataTable4.Rows.Add(row2);
                                        flag2 = false;
                                        string str4 = row2["CD_ITEM"].ToString();
                                        멀티품목코드 = 멀티품목코드 + str4 + "|";
                                    }
                                    else
                                    {
                                        string str5 = row1["NO_IOLINE"].ToString().PadRight(10, ' ') + " " + row1["CD_ITEM"].ToString().PadRight(10, ' ') + " " + row1["NO_LOT"].ToString().PadRight(10, ' ') + " " + row1["QT_IO"].ToString();
                                        stringBuilder.AppendLine(str5);
                                        flag1 = true;
                                    }
                                }
                            }
                            if (flag1)
                            {
                                Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 중복되는 (항번/품목)과 LOT가 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                                this._flexD.RowFilter = "NO_IOLINE = " + this._flexM[this._flexM.Row, "NO_IOLINE"].ToString() + " ";
                                this._flexD.Redraw = true;
                            }
                            else
                            {
                                DataTable dataTable6 = this._biz.ExcelSearch(멀티품목코드, "ITEM", D.GetString(this._flexM["CD_PLANT"]));
                                bool flag5 = false;
                                foreach (DataRow row3 in dataTable4.Rows)
                                {
                                    if (row3["CD_ITEM"].ToString().Trim() != null && !(row3["CD_ITEM"].ToString().Trim() == string.Empty) && !(row3["CD_ITEM"].ToString().Trim() == "") && row3["NO_IOLINE"].ToString().Trim() != null && !(row3["NO_IOLINE"].ToString().Trim() == string.Empty) && !(row3["NO_IOLINE"].ToString().Trim() == ""))
                                    {
                                        foreach (DataRow row4 in dataTable6.Rows)
                                        {
                                            if (row3["CD_ITEM"].ToString().Trim() == row4["CD_ITEM"].ToString().Trim())
                                            {
                                                flag2 = true;
                                                break;
                                            }
                                            flag2 = false;
                                        }
                                        if (flag2)
                                        {
                                            DataRow row5 = dataTable3.NewRow();
                                            row5["S"] = row3["S"].ToString();
                                            row5["CD_ITEM"] = row3["CD_ITEM"].ToString().Trim();
                                            row5["NO_IO"] = "";
                                            row5["NO_IOLINE"] = row3["NO_IOLINE"].ToString().Trim();
                                            row5["DT_IO"] = "";
                                            row5["FG_IO"] = "";
                                            row5["CD_QTIOTP"] = "";
                                            row5["CD_SL"] = "";
                                            row5["QT_IO"] = row3["QT_IO"];
                                            row5["FG_PS"] = row3["FG_PS"].ToString();
                                            row5["NO_LOT"] = D.GetString(row3["NO_LOT"]);
                                            row5["CD_MNG1"] = row3["CD_MNG1"].ToString().Trim();
                                            row5["CD_MNG2"] = row3["CD_MNG2"].ToString().Trim();
                                            row5["CD_MNG3"] = row3["CD_MNG3"].ToString().Trim();
                                            row5["CD_MNG4"] = row3["CD_MNG4"].ToString().Trim();
                                            row5["CD_MNG5"] = row3["CD_MNG5"].ToString().Trim();
                                            row5["CD_MNG6"] = row3["CD_MNG6"].ToString().Trim();
                                            row5["CD_MNG7"] = row3["CD_MNG7"].ToString().Trim();
                                            row5["CD_MNG8"] = row3["CD_MNG8"].ToString().Trim();
                                            row5["CD_MNG9"] = row3["CD_MNG9"].ToString().Trim();
                                            row5["CD_MNG10"] = row3["CD_MNG10"].ToString().Trim();
                                            row5["CD_MNG11"] = row3["CD_MNG11"].ToString().Trim();
                                            row5["CD_MNG12"] = row3["CD_MNG12"].ToString().Trim();
                                            row5["CD_MNG13"] = row3["CD_MNG13"].ToString().Trim();
                                            row5["CD_MNG14"] = row3["CD_MNG14"].ToString().Trim();
                                            row5["CD_MNG15"] = row3["CD_MNG15"].ToString().Trim();
                                            row5["CD_MNG16"] = row3["CD_MNG16"].ToString().Trim();
                                            row5["CD_MNG17"] = row3["CD_MNG17"].ToString().Trim();
                                            row5["CD_MNG18"] = row3["CD_MNG18"].ToString().Trim();
                                            row5["CD_MNG19"] = row3["CD_MNG19"].ToString().Trim();
                                            row5["CD_MNG20"] = row3["CD_MNG20"].ToString().Trim();
                                            if (flag4)
                                                row5["DT_LIMIT"] = row3["DT_LIMIT"].ToString().Trim();
                                            if (flag3)
                                                row5["DC_LOTRMK"] = row3["DC_LOTRMK"].ToString().Trim();
                                            dataTable3.Rows.Add(row5);
                                            flag2 = false;
                                        }
                                        else
                                        {
                                            string str6 = row3["NO_IOLINE"].ToString().PadRight(10, ' ') + " " + row3["CD_ITEM"].ToString().PadRight(10, ' ') + " " + row3["NO_LOT"].ToString().PadRight(10, ' ') + " " + row3["QT_IO"].ToString();
                                            stringBuilder.AppendLine(str6);
                                            flag5 = true;
                                        }
                                    }
                                }
                                if (flag5)
                                {
                                    Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 마스터품목과 불일치 항목들이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                                    this._flexD.RowFilter = "NO_IOLINE = " + this._flexM[this._flexM.Row, "NO_IOLINE"].ToString() + " ";
                                    this._flexD.Redraw = true;
                                }
                                else
                                {
                                    bool flag6 = false;
                                    this._dt.PrimaryKey = new DataColumn[] { this._dt.Columns["NO_IOLINE"] };
                                    for (int index = 0; index < dataTable3.Rows.Count; ++index)
                                    {
                                        if (dataTable3.Rows[index]["CD_ITEM"].ToString().Trim() != null && !(dataTable3.Rows[index]["CD_ITEM"].ToString().Trim() == string.Empty) && !(dataTable3.Rows[index]["CD_ITEM"].ToString().Trim() == "") && dataTable3.Rows[index]["NO_IOLINE"].ToString().Trim() != null && !(dataTable3.Rows[index]["NO_IOLINE"].ToString().Trim() == string.Empty) && !(dataTable3.Rows[index]["NO_IOLINE"].ToString().Trim() == ""))
                                        {
                                            if (Convert.ToDecimal(dataTable3.Rows[index]["QT_IO"]) > 0M)
                                            {
                                                DataRow dataRow = this._dt.Rows.Find(dataTable3.Rows[index]["NO_IOLINE"].ToString().Trim());
                                                DataRow row = this._flexD.DataTable.NewRow();
                                                row["S"] = dataTable3.Rows[index]["S"].ToString().Trim();
                                                row["CD_ITEM"] = dataTable3.Rows[index]["CD_ITEM"].ToString().Trim();
                                                row["NO_IO"] = dataRow["NO_IO"].ToString().Trim();
                                                row["NO_IOLINE"] = dataRow["NO_IOLINE"].ToString();
                                                row["DT_IO"] = dataRow["DT_IO"].ToString().Trim();
                                                row["FG_IO"] = dataRow["FG_IO"].ToString().Trim();
                                                row["CD_QTIOTP"] = dataRow["CD_QTIOTP"].ToString().Trim();
                                                row["CD_SL"] = dataRow["CD_SL"].ToString().Trim();
                                                row["QT_IO"] = dataTable3.Rows[index]["QT_IO"];
                                                row["FG_PS"] = dataTable3.Rows[index]["FG_PS"].ToString().Trim();
                                                row["NO_LOT"] = D.GetString(dataTable3.Rows[index]["NO_LOT"]);
                                                row["CD_MNG1"] = dataTable3.Rows[index]["CD_MNG1"].ToString().Trim();
                                                row["CD_MNG2"] = dataTable3.Rows[index]["CD_MNG2"].ToString().Trim();
                                                row["CD_MNG3"] = dataTable3.Rows[index]["CD_MNG3"].ToString().Trim();
                                                row["CD_MNG4"] = dataTable3.Rows[index]["CD_MNG4"].ToString().Trim();
                                                row["CD_MNG5"] = dataTable3.Rows[index]["CD_MNG5"].ToString().Trim();
                                                row["CD_MNG6"] = dataTable3.Rows[index]["CD_MNG6"].ToString().Trim();
                                                row["CD_MNG7"] = dataTable3.Rows[index]["CD_MNG7"].ToString().Trim();
                                                row["CD_MNG8"] = dataTable3.Rows[index]["CD_MNG8"].ToString().Trim();
                                                row["CD_MNG9"] = dataTable3.Rows[index]["CD_MNG9"].ToString().Trim();
                                                row["CD_MNG10"] = dataTable3.Rows[index]["CD_MNG10"].ToString().Trim();
                                                row["CD_MNG11"] = dataTable3.Rows[index]["CD_MNG11"].ToString().Trim();
                                                row["CD_MNG12"] = dataTable3.Rows[index]["CD_MNG12"].ToString().Trim();
                                                row["CD_MNG13"] = dataTable3.Rows[index]["CD_MNG13"].ToString().Trim();
                                                row["CD_MNG14"] = dataTable3.Rows[index]["CD_MNG14"].ToString().Trim();
                                                row["CD_MNG15"] = dataTable3.Rows[index]["CD_MNG15"].ToString().Trim();
                                                row["CD_MNG16"] = dataTable3.Rows[index]["CD_MNG16"].ToString().Trim();
                                                row["CD_MNG17"] = dataTable3.Rows[index]["CD_MNG17"].ToString().Trim();
                                                row["CD_MNG18"] = dataTable3.Rows[index]["CD_MNG18"].ToString().Trim();
                                                row["CD_MNG19"] = dataTable3.Rows[index]["CD_MNG19"].ToString().Trim();
                                                row["CD_MNG20"] = dataTable3.Rows[index]["CD_MNG20"].ToString().Trim();
                                                if (flag3)
                                                    row["DC_LOTRMK"] = dataTable3.Rows[index]["DC_LOTRMK"].ToString().Trim();
                                                if (Global.MainFrame.ServerKeyCommon.Contains("KUMBI"))
                                                    row["DT_LIMIT"] = this.setDtLimit(D.GetString(row["CD_MNG2"]), "EXECL", D.GetString(row["CD_ITEM"]));
                                                if (flag4)
                                                    row["DT_LIMIT"] = dataTable3.Rows[index]["DT_LIMIT"];
                                                this._flexD.DataTable.Rows.Add(row);
                                            }
                                            else
                                            {
                                                string str7 = dataTable3.Rows[index]["NO_IOLINE"].ToString().Trim().PadRight(10, ' ') + " " + dataTable3.Rows[index]["CD_ITEM"].ToString().Trim().PadRight(10, ' ') + " " + dataTable3.Rows[index]["NO_LOT"].ToString().Trim().PadRight(10, ' ') + " " + dataTable3.Rows[index]["QT_IO"].ToString().Trim();
                                                stringBuilder.AppendLine(str7);
                                                flag6 = true;
                                            }
                                        }
                                    }
                                    if (flag6)
                                    {
                                        Global.MainFrame.ShowDetailMessage("엑셀 업로드하는 중에 부적절한 수량이 포함된 항목들이 존재합니다. \n  \n ▼ 버튼을 눌러서 목록을 확인하세요!", stringBuilder.ToString());
                                    }
                                    Global.MainFrame.ShowMessage("엑셀 작업을 완료하였습니다. 확인버튼을 눌러주세요!");
                                    if (!this._flexD.HasNormalRow)
                                    {
                                        this.ExcelChk = false;
                                        this.btn엑셀업로드.Text = "전체삭제";
                                    }
                                    DataTable dataTable7 = this._flexD.DataTable.Clone();
                                    foreach (DataRow row in this._flexD.DataTable.Rows)
                                    {
                                        if (D.GetString(row["NO_LOT"]) != string.Empty)
                                            dataTable7.ImportRow(row);
                                    }
                                    this._flexD.Binding = dataTable7;
                                    this._flexM.RowFilter = "";
                                    this._flexD.RowFilter = "NO_IOLINE = " + this._flexM[this._flexM.Row, "NO_IOLINE"].ToString() + " ";
                                    this._flexD.Redraw = true;
                                }
                            }
                        }
                    }
                }
                else
                    this.DeleteAll();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexD.Redraw = true;
            }
        }

        private void btm자동채번_Click(object sender, EventArgs e)
        {
            try
            {
                this._flexD.Redraw = false;
                for (int index = this._flexD.DataTable.Rows.Count - 1; index >= 0; --index)
                    this._flexD.DataTable.Rows.RemoveAt(index);
                foreach (DataRow row in this._flexM.DataTable.Rows)
                {
                    string str1 = string.Empty;
                    string str2 = "";
                    if (Global.MainFrame.ServerKeyCommon == "MAKUS")
                    {
                        str1 = this._biz.GetMaxLot(D.GetString(row["CD_ITEM"]), "MAKUS", "", "");
                        str2 = "CD_ITEM = '" + D.GetString(row["CD_ITEM"]) + "'";
                    }
                    else if (Global.MainFrame.ServerKeyCommon == "AMOS")
                        str1 = this._biz.GetMaxLot(D.GetString(row["CD_ITEM"]), "AMOS", D.GetString(row["CD_PLANT"]), D.GetString(row["DT_IO"]));
                    decimal d = 0M;
                    string str3;
                    decimal val;
                    if (str1 == "")
                    {
                        str3 = D.GetString(row["DT_IO"]);
                        val = ++d;
                    }
                    else
                    {
                        string str4 = str1.PadRight(8, '0');
                        str3 = str4.Remove(8, str4.Length - 8);
                        if (str3 == D.GetString(this._flexM["DT_IO"]))
                        {
                            decimal temp1 = D.GetDecimal(str4.Remove(0, 8));
                            val = ++temp1;
                        }
                        else
                        {
                            str3 = D.GetString(row["DT_IO"]);
                            val = ++d;
                        }
                        string str5 = str3 + D.GetString(val).PadLeft(5, '0');
                    }
                    DataRow[] dataRowArray = this._flexD.DataTable.Select(str2);
                    this._flexD.Rows.Add();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    this._flexD["S"] = "N";
                    this._flexD["CD_ITEM"] = row["CD_ITEM"];
                    this._flexD["NO_IO"] = row["NO_IO"];
                    this._flexD["NO_IOLINE"] = row["NO_IOLINE"];
                    this._flexD["DT_IO"] = row["DT_IO"];
                    this._flexD["FG_IO"] = row["FG_IO"];
                    this._flexD["CD_QTIOTP"] = row["CD_QTIOTP"];
                    this._flexD["CD_SL"] = row["CD_SL"];
                    this._flexD["QT_IO"] = row["QT_GOOD_INV"];
                    this._flexD["FG_PS"] = "1";
                    if (dataRowArray.Length > 0 && dataRowArray != null)
					{
                        decimal temp2 = D.GetDecimal(D.GetString(this._flexD.DataTable.Compute("MAX(NO_LOT)", str2)).Remove(0, 8));
                        val = ++temp2;
                    }
                        
                    this._flexD["NO_LOT"] = (str3 + D.GetString(val).PadLeft(5, '0'));
                    this._flexD.AddFinished();
                    this._flexD.Col = this._flexD.Cols.Fixed;
                    this._flexD.Focus();
                }
                this._flexD.Redraw = true;
            }
            catch (Exception ex)
            {
                this._flexD.Redraw = true;
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btm자동채번_KEUNDAN_Click(object sender, EventArgs e)
        {
            try
            {
                this._flexD.Redraw = false;
                decimal val = 0M;
                string str1 = "";
                foreach (DataRow row in this._flexD.DataTable.Rows)
                    row["NO_LOT"] = "";
                foreach (DataRow dataRow in this._flexD.DataTable.Select("", "NO_IOLINE"))
                {
                    DataRow[] dataRowArray = this._flexM.DataTable.Select("NO_IO = '" + D.GetString(dataRow["NO_IO"]) + "' AND NO_IOLINE = '" + D.GetString(dataRow["NO_IOLINE"]) + "'");
                    if (dataRowArray != null && dataRowArray.Length > 0)
                        str1 = D.GetString(dataRowArray[0]["CLS_ITEM"]);
                    if ((str1 == "001" || str1 == "002" || !(Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")) && (!(str1 != "001") || !(Global.MainFrame.CurrentPageID == "P_PU_ITR_REG")))
                    {
                        string maxLot = this._biz.GetMaxLot("", "KEUNDAN", D.GetString(this._flexM["CD_PLANT"]), "");
                        string str2 = Global.MainFrame.GetStringToday.ToString().Substring(2, 4);
                        string str3;
                        if (maxLot == "")
                        {
                            str3 = str2;
                            ++val;
                        }
                        else
                        {
                            string str4 = maxLot.PadRight(4, '0');
                            str3 = str4.Remove(4, str4.Length - 4);
                            if (str3 == str2)
                            {
                                string str5 = D.GetString(this._flexD.DataTable.Compute("MAX(NO_LOT)", ""));
                                decimal temp1 = D.GetDecimal(!(str5 == "") ? str5.Remove(0, 4) : str4.Remove(0, 4));
                                val = ++temp1;
                            }
                            else
                            {
                                str3 = str2;
                                ++val;
                            }
                            string str6 = str3 + D.GetString(val).PadLeft(5, '0');
                        }
                        dataRow["NO_LOT"] = (str3 + D.GetString(val).PadLeft(5, '0'));
                    }
                }
                this._flexD.Redraw = true;
            }
            catch (Exception ex)
            {
                this._flexD.Redraw = true;
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            if (!this._flexM.IsBindingEnd || !this._flexM.HasNormalRow)
                return;
            DataTable dt = null;
            string filter = "NO_IO = '" + this._flexM[this._flexM.Row, "NO_IO"].ToString() + "' AND NO_IOLINE = " + this._flexM[this._flexM.Row, "NO_IOLINE"].ToString();
            if (this._flexM.DetailQueryNeed)
                dt = this._biz.Search_Detail(this._flexM[this._flexM.Row, "NO_IO"].ToString());
            this._flexD.BindingAdd(dt, filter);
            this._flexM.DetailQueryNeed = false;
            if (this._pageid != "P_SA_GI_SWITCH_YN_AM" && this._flexD.DataView.Count < 1)
                this.추가_Click(null, null);
            this.calcQT(0M);
        }

        private void _flexD_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                string str = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                string editData = ((FlexGrid)sender).EditData;
                if (!(this._flexD.GetData(e.Row, e.Col).ToString() != this._flexD.EditData))
                    return;
                switch (this._flexD.Cols[e.Col].Name)
                {
                    case "QT_IO":
                        this._flexD[this._flexD.Row, "QT_IO"] = this._flexD.CDecimal(editData);
                        this._flexD[this._flexD.Row, "QT_IO_OLD"] = this._flexD.CDecimal(str);
                        if (MA.ServerKey(false, "KYOTECH"))
                            this._flexD[this._flexD.Row, "CD_MNG11"] = this._flexD.CDecimal(editData);
                        this.calcQT(D.GetDecimal(editData) - D.GetDecimal(str));
                        break;
                    case "CD_MNG1":
                        break;
                    case "CD_MNG2":
                        if (Global.MainFrame.ServerKeyCommon.Contains("KUMBI"))
                        {
                            this.setDtLimit(editData, "GRID", D.GetString(this._flexD["CD_ITEM"]));
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexD_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (this._flexD.Cols[e.Col].Name)
                {
                    case "CD_MNG19":
                        e.Parameter.P41_CD_FIELD1 = "CZ_KCMA019";
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void DeleteAll()
        {
            if (!this._flexD.HasNormalRow)
                return;
            this._flexD.Redraw = false;
            foreach (DataRow dataRow in this._flexD.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows))
                dataRow.Delete();
            this._flexD.Redraw = true;
            this.btn엑셀업로드.Text = "엑셀업로드";
            this.ExcelChk = true;
        }

        private void Set_Line()
        {
            if (this._flexM.DataTable == null)
                return;
            string NO_IO_MGMT = string.Empty;
            foreach (DataRow row in this._flexM.DataTable.Rows)
                NO_IO_MGMT = NO_IO_MGMT + D.GetString(row["NO_IO_MGMT"]) + "|";
            DataTable dataTable1 = this._biz.dt_SER_MGMT(NO_IO_MGMT);
            foreach (DataRow row in this._flexM.DataTable.Rows)
            {
                DataTable dataTable2 = dataTable1;
                string filterExpression = "NO_IO_MGMT = '" + D.GetString(row["NO_IO_MGMT"]) + "' AND NO_IOLINE_MGMT = " + D.GetDecimal(row["NO_IOLINE_MGMT"]);
                foreach (DataRow dataRow in dataTable2.Select(filterExpression))
                {
                    this._flexD.Rows.Add();
                    this._flexD.Row = this._flexD.Rows.Count - 1;
                    this._flexD["S"] = "N";
                    this._flexD["CD_ITEM"] = D.GetString(row["CD_ITEM"]);
                    this._flexD["NO_IO"] = D.GetString(row["NO_IO"]);
                    this._flexD["NO_IOLINE"] = D.GetDecimal(row["NO_IOLINE"]);
                    this._flexD["DT_IO"] = D.GetString(row["DT_IO"]);
                    this._flexD["FG_IO"] = D.GetString(row["FG_IO"]);
                    this._flexD["CD_QTIOTP"] = D.GetString(row["CD_QTIOTP"]);
                    this._flexD["CD_SL"] = D.GetString(row["CD_SL"]);
                    this._flexD["QT_IO"] = D.GetDecimal(row["QT_GOOD_INV"]);
                    this._flexD["FG_PS"] = "1";
                    this._flexD["NO_LOT"] = D.GetString(dataRow["NO_LOT"]);
                    this._flexD.AddFinished();
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this._flexM.SaveUserCache("P_PU_LOT_SUB_flexM");
            this._flexD.SaveUserCache("P_PU_LOT_SUB_flexD");
        }

        private void calcQT(decimal qt_sub)
        {
            try
            {
                this.cur잔량.DecimalValue = D.GetDecimal(this._flexM["QT_GOOD_INV"]) - (D.GetDecimal(this._flexD.DataTable.Compute("SUM(QT_IO)", "NO_IO = '" + D.GetString(this._flexM["NO_IO"]) + "' AND NO_IOLINE = " + D.GetString(this._flexM["NO_IOLINE"]))) + qt_sub);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void line_add()
        {
            try
            {
                this._flexD.Rows.Add();
                this._flexD.Row = this._flexD.Rows.Count - 1;
                this._flexD["S"] = "N";
                this._flexD["CD_ITEM"] = this._flexM[this._flexM.Row, "CD_ITEM"];
                this._flexD["NO_IO"] = this._flexM[this._flexM.Row, "NO_IO"];
                this._flexD["NO_IOLINE"] = this._flexM[this._flexM.Row, "NO_IOLINE"];
                this._flexD["DT_IO"] = this._flexM[this._flexM.Row, "DT_IO"];
                this._flexD["FG_IO"] = this._flexM[this._flexM.Row, "FG_IO"];
                this._flexD["CD_QTIOTP"] = this._flexM[this._flexM.Row, "CD_QTIOTP"];
                this._flexD["CD_SL"] = this._flexM[this._flexM.Row, "CD_SL"];
                this._flexD["QT_IO"] = this._flexD.Rows.Count != 2 ? 0 : this._flexM[this._flexM.Row, "QT_GOOD_INV"];
                this._flexD["FG_PS"] = "1";
                if (this._value.Length > 0 && this._flexD.DataView.Count == 1)
                {
                    this._flexD["NO_LOT"] = D.GetString(this._value[0]);
                    this._flexD["QT_IO"] = this._flexM[this._flexM.Row, "QT_GOOD_INV"];
                }
                else
                    this._flexD["NO_LOT"] = "";
                if (Global.MainFrame.ServerKeyCommon.Contains("DAEKHON") && (D.GetString(this._flexM[this._flexM.Row, "FG_IO"]) == "001" || D.GetString(this._flexM[this._flexM.Row, "FG_IO"]) == "030"))
                {
                    if (this._flexM.DataTable.Columns.Contains("CD_USERDEF1_RCV"))
                        this._flexD["CD_MNG1"] = this._flexM[this._flexM.Row, "CD_USERDEF1_RCV"];
                    if (this._flexM.DataTable.Columns.Contains("CD_USERDEF2_RCV"))
                        this._flexD["CD_MNG3"] = this._flexM[this._flexM.Row, "CD_USERDEF2_RCV"];
                    if (this._flexM.DataTable.Columns.Contains("NM_USERDEF1_RCV"))
                        this._flexD["CD_MNG2"] = this._flexM[this._flexM.Row, "NM_USERDEF1_RCV"];
                    if (this._flexM.DataTable.Columns.Contains("NM_USERDEF2_RCV"))
                        this._flexD["CD_MNG4"] = this._flexM[this._flexM.Row, "NM_USERDEF2_RCV"];
                    if (this._flexM.DataTable.Columns.Contains("DATE_USERDEF1_RCV"))
                        this._flexD["CD_MNG5"] = this._flexM[this._flexM.Row, "DATE_USERDEF1_RCV"];
                    if (this._flexM.DataTable.Columns.Contains("DATE_USERDEF1"))
                        this._flexD["CD_MNG5"] = this._flexM[this._flexM.Row, "DATE_USERDEF1"];
                }
                if (MA.ServerKey(false, "CSFOOD") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1" && (Global.MainFrame.LoginInfo.CompanyCode == "2000" || Global.MainFrame.LoginInfo.CompanyCode == "TEST2"))
                {
                    this._flexD["NO_LOT"] = this._flexM[this._flexM.Row, "NO_IO"];
                    this._flexD["CD_MNG1"] = this._flexM[this._flexM.Row, "CD_USERDEF1_RCV"];
                    this._flexD["CD_MNG2"] = this._flexM[this._flexM.Row, "NM_USERDEF1_RCV"];
                    this._flexD["CD_MNG3"] = this._flexM[this._flexM.Row, "CD_USERDEF2_RCV"];
                    this._flexD["CD_MNG4"] = this._flexM[this._flexM.Row, "NM_USERDEF2_RCV"];
                    this._flexD["CD_MNG5"] = this._flexM[this._flexM.Row, "CD_USERDEF3_IO"];
                    this._flexD["CD_MNG6"] = this._flexM[this._flexM.Row, "NM_USERDEF3_IO"];
                    this._flexD["CD_MNG7"] = this._flexM[this._flexM.Row, "CD_USERDEF4_IO"];
                    this._flexD["CD_MNG8"] = this._flexM[this._flexM.Row, "NM_USERDEF4_IO"];
                }
                if (Global.MainFrame.ServerKeyCommon == "YWD" && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
                    this._flexD["NO_LOT"] = this._flexM[this._flexM.Row, "DC_RMK"];
                if (MA.ServerKey(false, "DAOU") && Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1")
                    this.setMNG_DAOU(D.GetString(this._flexM[this._flexM.Row, "NO_PO"]), D.GetDecimal(this._flexM[this._flexM.Row, "NO_POLINE"]));
                if (MA.ServerKey(false, "AXT"))
                {
                    this._flexD["NO_LOT"] = this.get_BARCODE_AXT(D.GetString(this._flexM[this._flexM.Row, "CD_PLANT"]), D.GetString(this._flexM[this._flexM.Row, "CD_ITEM"]), D.GetString(this._flexM[this._flexM.Row, "DT_IO"]), D.GetString(this._flexM[this._flexM.Row, "AXT_Z_BARCODE"]));
                    this._flexD["QT_IO"] = 1;
                    this.calcQT(0M);
                }
                if (MA.ServerKey(false, "THV"))
                {
                    if (this._flexM.DataTable.Columns.Contains("CD_THV1") && this._flexD.Rows.Count == 1 + this._flexD.Rows.Fixed)
                        this._flexD["NO_LOT"] = this._flexM[this._flexM.Row, "CD_THV1"];
                    if (this._flexM.DataTable.Columns.Contains("CD_THV2"))
                        this._flexD["CD_MNG1"] = this._flexM[this._flexM.Row, "CD_THV2"];
                    if (this._flexM.DataTable.Columns.Contains("CD_THV3"))
                        this._flexD["DT_LIMIT"] = this._flexM[this._flexM.Row, "CD_THV3"];
                    if (this._flexM.DataTable.Columns.Contains("CD_THV4"))
                        this._flexD["DC_LOTRMK"] = this._flexM[this._flexM.Row, "CD_THV4"];
                }
                if (MA.ServerKey(false, "DAIWA") && (Global.MainFrame.CurrentPageID == "P_PU_REQ_REG_1" || Global.MainFrame.CurrentPageID == "P_PU_GR_REG"))
                    this._flexD["NO_LOT"] = this.get_LOT_DAIWA(D.GetString(this._flexM[this._flexM.Row, "CD_ITEM"]), D.GetString(this._flexM[this._flexM.Row, "DT_IO"]), D.GetString(this._flexM[this._flexM.Row, "CD_PARTNER"]));
                if (MA.ServerKey(false, "ANYONE"))
                    this._flexD["CD_MNG5"] = this._flexM[this._flexM.Row, "DT_IO"];
                if (MA.ServerKey(false, "KYOTECH"))
                {
                    if (Global.MainFrame.CurrentPageID == "P_PU_GR_REG")
                        this.setData_MNG_KYOTECH(this._flexD.GetDataRow(this._flexD.Row), this._flexM.GetDataRow(this._flexM.Row));
                    if (Global.MainFrame.CurrentPageID == "P_PU_GR_REG" || Global.MainFrame.CurrentPageID == "P_PU_ITR_REG" || Global.MainFrame.CurrentPageID == "P_SA_GIRE_REG")
                    {
                        if (D.GetString(this._flexD["NO_LOT"]) == string.Empty)
                            this._flexD["NO_LOT"] = ((string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "75", D.GetString(this._flexD["DT_IO"]))).Substring(3);
                        this._flexD["CD_MNG11"] = this._flexD["QT_IO"];
                    }
                }
                if (MA.ServerKey(false, "SANSUNG") && this._flexD.Rows.Count == 1 + this._flexD.Rows.Fixed && Global.MainFrame.CurrentPageID == "P_PU_SGRBF_REG" && D.GetString(this._flexM[this._flexM.Row, "CD_USERDEF2_ITEM"]) == "01" && (D.GetString(this._flexM[this._flexM.Row, "CLS_ITEM"]) == "003" || D.GetString(this._flexM[this._flexM.Row, "CLS_ITEM"]) == "004"))
                    this._flexD["NO_LOT"] = (D.GetString(this._flexM[this._flexM.Row, "NO_ISURCV"]) + "-" + D.GetString(this._flexM[this._flexM.Row, "NO_ISURCVLINE"]));
                if (Global.MainFrame.ServerKeyCommon == "YHPLA" && Global.MainFrame.CurrentPageID == "P_PU_GR_REG")
                    this._flexD["CD_MNG1"] = this._flexM[this._flexM.Row, "CD_USERDEF3_RCV"];
                this._flexD.AddFinished();
                this._flexD.Col = this._flexD.Cols.Fixed;
                this._flexD.Focus();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private string setDtLimit(string strCdMng2, string strFlag, string strCD_ITEM)
        {
            try
            {
                if (!this._flexD.HasNormalRow && strFlag == "GRID")
                    return "";
                string cd_plant = D.GetString(this._flexM["CD_PLANT"]);
                string str1 = "";
                string str2 = "";
                if (strCdMng2 != "")
                {
                    string val = this._biz.Getdt(strCD_ITEM, cd_plant);
                    if (val != "")
                    {
                        DateTime exact = DateTime.ParseExact(strCdMng2, "yyyyMMdd", CultureInfo.InvariantCulture);
                        DateTime dateTime = exact.AddMonths(D.GetInt(val));
                        exact = DateTime.ParseExact(dateTime.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture);
                        dateTime = exact.AddDays(-1.0);
                        string str3 = dateTime.ToString("yyyyMMdd");
                        if (strFlag == "GRID")
                            this._flexD["DT_LIMIT"] = str3;
                        else
                            str2 = str3;
                        str1 = "";
                    }
                }
                return str2;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                return "";
            }
        }

        private void setEnableColumn()
        {
            try
            {
                List<string> stringList = new List<string>();
                for (int index = 1; index <= 8; ++index)
                {
                    if (this._flexD.Cols.Contains("CD_MNG" + index))
                        stringList.Add("CD_MNG" + index);
                }
                this._flexD.SetExceptEditCol(stringList.ToArray());
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void setMNG_DAOU(string NO_PO, decimal NO_POLINE)
        {
            try
            {
                if (!(D.GetString(NO_PO) != ""))
                    return;
                DataTable solDaou = this._biz.getSOL_DAOU(new object[] { Global.MainFrame.LoginInfo.CompanyCode.ToString(),
                                                                         NO_PO,
                                                                         NO_POLINE });
                if (solDaou != null && solDaou.Rows.Count > 0)
                {
                    this._flexD["NO_LOT"] = solDaou.Rows[0]["TXT_USERDEF1"];
                    this._flexD["CD_MNG1"] = solDaou.Rows[0]["TXT_USERDEF2"];
                    this._flexD["CD_MNG2"] = solDaou.Rows[0]["TXT_USERDEF3"];
                    this._flexD["CD_MNG3"] = solDaou.Rows[0]["TXT_USERDEF4"];
                    this._flexD["CD_MNG4"] = solDaou.Rows[0]["TXT_USERDEF5"];
                    this._flexD["CD_MNG5"] = solDaou.Rows[0]["NO_SO"];
                    this._flexD["CD_MNG6"] = solDaou.Rows[0]["LN_PARTNER"];
                    this._flexD["CD_MNG7"] = solDaou.Rows[0]["NM_ENDUSER"];
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private string get_BARCODE_AXT(string CD_PLANT, string CD_ITEM, string DT_IO, string BARCODE)
        {
            string seq = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "B1", DT_IO);
            string str1 = seq.Substring(5, 2).Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D").Replace("5", "E").Replace("6", "F").Replace("7", "G").Replace("8", "H").Replace("9", "I").Replace("0", "J");
            string str2 = seq.Substring(7, 2).Replace("01", "A").Replace("02", "B").Replace("03", "C").Replace("04", "D").Replace("05", "E").Replace("06", "F").Replace("07", "G").Replace("08", "H").Replace("09", "I").Replace("10", "J").Replace("11", "K").Replace("12", "L");
            string str3 = seq.Substring(9, 2).Replace("1", "A").Replace("2", "B").Replace("3", "C").Replace("4", "D").Replace("5", "E").Replace("6", "F").Replace("7", "G").Replace("8", "H").Replace("9", "I").Replace("0", "J");
            return BARCODE + str1 + str2 + str3 + seq.Substring(11, 4);
        }

        private bool chk_BARCODE(DataTable dt)
        {
            string NO_KEY = string.Empty;
            dt.Columns.Add("AXT_Z_BARCODE", typeof(string));
            foreach (DataRow row in dt.Rows)
                NO_KEY = NO_KEY + D.GetString(row["CD_ITEM"]).ToString().Trim() + "|";
            DataTable dataTable = this._biz.SearchItem(D.GetString(dt.Rows[0]["CD_PLANT"]), NO_KEY);
            foreach (DataRow row1 in dt.Rows)
            {
                foreach (DataRow row2 in dataTable.Rows)
                {
                    if (row1["CD_ITEM"].ToString().Trim() == row2["CD_ITEM"].ToString())
                        row1["AXT_Z_BARCODE"] = row2["BARCODE"];
                }
                if (D.GetString(row1["AXT_Z_BARCODE"]) == "")
                    return false;
            }
            return true;
        }

        private string get_LOT_DAIWA(string CD_ITEM, string DT_IO, string CD_PARTNER)
        {
            string lotDaiwa = string.Empty;
            string str = D.GetString(this._flexD.DataTable.Compute("MAX(NO_LOT)", "CD_ITEM = '" + CD_ITEM + "'"));
            if (str == "")
            {
                str = D.GetString(this._biz.getMAX_LOT_DAIWA(CD_ITEM, CD_PARTNER, DT_IO.Substring(2, 6)).Rows[0]["NO_LOT"]);
                if (str == "")
                    lotDaiwa = CD_PARTNER + "_" + DT_IO.Substring(2, 6) + "_001";
            }
            if (lotDaiwa == "")
                lotDaiwa = str.Substring(0, str.Length - 3) + D.GetString((D.GetInt(str.Substring(str.Length - 3, 3)) + 1)).PadLeft(3, '0');
            return lotDaiwa;
        }

        public DataTable getMergeDT_KYOTECH(DataTable dt)
        {
            DataTable dt_merge = dt.Clone();
            DataSet defDataKyotech = this._biz.getDefData_KYOTECH();
            foreach (DataTable table in defDataKyotech.Tables)
            {
                foreach (DataColumn column in table.Columns)
                    dt_merge.Columns.Add(column.ColumnName, column.DataType);
            }
            return dt.AsEnumerable().GroupJoin((IEnumerable<DataRow>)defDataKyotech.Tables[0].AsEnumerable(), (Func<DataRow, string>)(L => L.Field<string>("CD_STND_ITEM_1_POL")), (Func<DataRow, string>)(PTN => PTN.Field<string>("CD_SA_PARTNER")), (L, p) =>
            {
                var mergeDtKyotech = new { L = L, p = p };
                return mergeDtKyotech;
            }).SelectMany(_param0 => _param0.p.DefaultIfEmpty<DataRow>(), (_param0, PTN) =>
            {
                var mergeDtKyotech = new
                {
                    h__TransparentIdentifier0 = _param0,
                    PTN = PTN
                };
                return mergeDtKyotech;
            }).GroupJoin((IEnumerable<DataRow>)defDataKyotech.Tables[1].AsEnumerable(), _param0 => _param0.h__TransparentIdentifier0.L.Field<string>("CD_STND_ITEM_2_POL"), (Func<DataRow, string>)(EMP => EMP.Field<string>("NO_SA_EMP")), (_param0, e) =>
            {
                var mergeDtKyotech = new
                {
                    h__TransparentIdentifier1 = _param0,
                    e = e
                };
                return mergeDtKyotech;
            }).SelectMany(_param0 => _param0.e.DefaultIfEmpty<DataRow>(), (_param0, EMP) =>
            {
                var mergeDtKyotech = new
                {
                    h__TransparentIdentifier2 = _param0,
                    EMP = EMP
                };
                return mergeDtKyotech;
            }).GroupJoin((IEnumerable<DataRow>)defDataKyotech.Tables[2].AsEnumerable(), _param0 => _param0.h__TransparentIdentifier2.h__TransparentIdentifier1.h__TransparentIdentifier0.L.Field<string>("CD_STND_ITEM_3_POL"), (Func<DataRow, string>)(GRP => GRP.Field<string>("CD_SA_SALEGRP")), (_param0, g) =>
            {
                var mergeDtKyotech = new
                {
                    h__TransparentIdentifier3 = _param0,
                    g = g
                };
                return mergeDtKyotech;
            }).SelectMany(_param0 => _param0.g.DefaultIfEmpty<DataRow>(), (_param1, GRP) => dt_merge.LoadDataRow(((IEnumerable<object>)_param1.h__TransparentIdentifier3.h__TransparentIdentifier2.h__TransparentIdentifier1.h__TransparentIdentifier0.L.ItemArray).Concat<object>(((IEnumerable<object>)_param1.h__TransparentIdentifier3.h__TransparentIdentifier2.h__TransparentIdentifier1.PTN.ItemArray).Concat<object>(((IEnumerable<object>)_param1.h__TransparentIdentifier3.EMP.ItemArray).Concat<object>((IEnumerable<object>)GRP.ItemArray))).ToArray<object>(), false)).CopyToDataTable<DataRow>();
        }

        public void setGrid_MNG_KYOTECH(int cnt, string column, DataRow row)
        {
            try
            {
                switch (cnt)
                {
                    case 10:
                        this._flexD.SetCol(column, D.GetString(row["NAME"]), 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                        goto case 11;
                    case 11:
                        if (cnt == 2 && this._flexD.Cols.Contains("CD_MNG1") && this._flexD.Cols.Contains("CD_MNG2"))
                            this._flexD.SetCodeHelpCol("CD_MNG1", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_MNG1", "CD_MNG2" }
                                                                                                              , new string[] { "CD_PARTNER", "LN_PARTNER" });
                        this._flexD.SetExceptEditCol("CD_MNG2");
                        if (cnt == 5 && this._flexD.Cols.Contains("CD_MNG4") && this._flexD.Cols.Contains("CD_MNG5"))
                            this._flexD.SetCodeHelpCol("CD_MNG4", HelpID.P_MA_SALEGRP_SUB, ShowHelpEnum.Always, new string[] { "CD_MNG4", "CD_MNG5" }
                                                                                                              , new string[] { "CD_SALEGRP", "NM_SALEGRP" });
                        this._flexD.SetExceptEditCol("CD_MNG5");
                        if (cnt != 7 || !this._flexD.Cols.Contains("CD_MNG6") || !this._flexD.Cols.Contains("CD_MNG7"))
                            break;
                        this._flexD.SetCodeHelpCol("CD_MNG6", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "CD_MNG6", "CD_MNG7" }
                                                                                                      , new string[] { "NO_EMP", "NM_KOR" });
                        break;
                    case 12:
                        this._flexD.SetCol(column, D.GetString(row["NAME"]), 100, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
                        goto case 11;
                    default:
                        this._flexD.SetCol(column, D.GetString(row["NAME"]), 100);
                        goto case 11;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public void setData_MNG_KYOTECH(DataRow drL, DataRow drH)
        {
            try
            {
                object[] objArray = new object[] { "CD_STND_ITEM_1_POL",
                                                   "LN_SA_PARTNER",
                                                   "CD_STND_ITEM_4_POL",
                                                   "CD_STND_ITEM_3_POL",
                                                   "NM_SA_SALEGRP",
                                                   "CD_STND_ITEM_2_POL",
                                                   "NM_SA_KOR",
                                                   "CD_USERDEF1_POL",
                                                   "CD_USERDEF2_POL",
                                                   "NUM_STND_ITEM_2_POL",
                                                   "QT_PO",
                                                   "NUM_STND_ITEM_1_POL" };
                for (int index = 0; index < objArray.Length; ++index)
                    drL["CD_MNG" + (index + 1)] = drH[D.GetString(objArray[index])];
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public void setHeaderGrid_KYOTECH()
        {
            try
            {
                this._flexM.SetCol("NO_PO", "발주번호", 100, false);
                this._flexM.SetCol("NO_POLINE", "발주항번", 100, false);
                this._flexM.SetCol("CD_STND_ITEM_1_POL", "매출거래처코드", 100, false);
                this._flexM.SetCol("LN_SA_PARTNER", "매출거래처명", 100, false);
                this._flexM.SetCol("CD_STND_ITEM_4_POL", "매출거래환종", 100, false);
                this._flexM.SetCol("CD_STND_ITEM_3_POL", "매출영업그룹", 100, false);
                this._flexM.SetCol("NM_SA_SALEGRP", "매출영업그룹명", 100, false);
                this._flexM.SetCol("CD_STND_ITEM_2_POL", "매출영업담당자", 100, false);
                this._flexM.SetCol("NM_SA_KOR", "매출영업담당자명", 100, false);
                this._flexM.SetCol("CD_USERDEF1_POL", "매출거래구분", 100, false);
                this._flexM.SetCol("CD_USERDEF2_POL", "매출과세구분", 100, false);
                this._flexM.SetCol("NUM_STND_ITEM_2_POL", "매출단가(외화)", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexM.SetCol("QT_PO", "매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexM.SetCol("NUM_STND_ITEM_1_POL", "매출거래환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public void setDataMap_KYOTECH()
        {
            try
            {
                if (Global.MainFrame.CurrentPageID == "P_PU_GR_REG")
                {
                    this._flexM.SetDataMap("CD_STND_ITEM_4_POL", MA.GetCode("MA_B000005", true), "CODE", "NAME");
                    this._flexM.SetDataMap("CD_USERDEF1_POL", MA.GetCode("PU_C000016", true), "CODE", "NAME");
                    this._flexM.SetDataMap("CD_USERDEF2_POL", MA.GetCode("MA_B000040", true), "CODE", "NAME");
                }
                this._flexD.SetDataMap("CD_MNG3", MA.GetCode("MA_B000005", true), "CODE", "NAME");
                this._flexD.SetDataMap("CD_MNG8", MA.GetCode("PU_C000016", true), "CODE", "NAME");
                this._flexD.SetDataMap("CD_MNG9", MA.GetCode("MA_B000040", true), "CODE", "NAME");
                this._flexD.SetExceptEditCol("CD_MNG2", "CD_MNG5", "CD_MNG7", "CD_MNG11");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public DataTable dtL => this._dtL;

        public string SetPageId
        {
            set => this._pageid = value;
        }
    }
}

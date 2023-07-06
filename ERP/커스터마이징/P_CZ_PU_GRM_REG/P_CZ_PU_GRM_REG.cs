using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Erpu.Net.File;
using Duzon.Common.BpControls;
using Duzon.Common.ConstLib;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.Windows.OneControls;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.PU.Common;
using Duzon.Windows.Print;
using MSCommLib;
using pur;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PU_GRM_REG : PageBase
    {
        private P_CZ_PU_GRM_REG_BIZ _biz = new P_CZ_PU_GRM_REG_BIZ();
        private string strNO_IO;
        private string strCD_PLANT;
        private string strIS_CLOSED;
        private string dt_start;
        private string dt_end;
        private string cd_plant = (string)null;
        private string rt_no_io;
        private string fg_gubun;
        private string rt_fg_cls = string.Empty;
        private string strCD_PJT;
        private string strNM_PJT = string.Empty;
        private string strExcFile = "";
        private bool b단가권한 = true;
        private bool b금액권한 = true;
        private bool b수량권한 = true;
        private string _반품발주 = BASIC.GetMAEXC("반품발주사용여부");
        public P_CZ_PU_GRM_REG()
        {
            try
            {
                this.InitializeComponent();
                this.MainGrids = new FlexGrid[] { this._flexM, this._flexD };
                this.DataChanged += new EventHandler(this.Page_DataChanged);
                this._flexM.WhenRowChangeThenGetDetail = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        public P_CZ_PU_GRM_REG( string sdt_dt,
                                string edt_dt,
                                string ps_cd_partner,
                                string ps_nm_partner,
                                string ps_cd_gi_plant,
                                string ps_nm_gi_plant,
                                string ps_cdsl,
                                string ps_nmsl,
                                string ps_noemp,
                                string ps_nmkr)
        {
            try
            {
                this.InitializeComponent();
                this.MainGrids = new FlexGrid[] { this._flexM, this._flexD };
                this.DataChanged += new EventHandler(this.Page_DataChanged);
                this._flexM.WhenRowChangeThenGetDetail = true;
                this.dt_start = sdt_dt;
                this.dt_end = edt_dt;
                this.cd_plant = ps_cd_gi_plant;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        public P_CZ_PU_GRM_REG(PageBaseConst.CallType pageCallType, string idMemo) : this()
        {
            DataTable noIo = this._biz.GetNoIo(idMemo);
            this.strIS_CLOSED = !(D.GetDecimal(noIo.Rows[0]["QT_CLS"]) == 0M) ? "002" : "001";
            this.strNO_IO = D.GetString(noIo.Rows[0]["NO_IO"]);
            this.strCD_PLANT = D.GetString(noIo.Rows[0]["CD_PLANT"]);
        }

        public P_CZ_PU_GRM_REG(string[] obj, string pPageID) : this()
        {
            this.fg_gubun = pPageID;
            if (!(this.fg_gubun == "P_SA_Z_JONGHAP_STATUS_02"))
                return;
            this.dt_start = obj[0].ToString();
            this.dt_end = obj[1].ToString();
            this.strCD_PJT = obj[2].ToString();
            this.strNM_PJT = obj[3].ToString();
            this.rt_fg_cls = obj[4].ToString();
        }

        protected override void InitLoad()
        {
            base.InitLoad();
            DataTable dataTable = BASIC.MFG_AUTH(nameof(P_CZ_PU_GRM_REG));
            if (dataTable.Rows.Count > 0)
            {
                this.b단가권한 = !(dataTable.Rows[0]["YN_UM"].ToString() == "Y");
                this.b금액권한 = !(dataTable.Rows[0]["YN_AM"].ToString() == "Y");
                this.b수량권한 = !(dataTable.Rows[0]["YN_QT"].ToString() == "Y");
            }
            this.InitGrid();
            this.InitEvent();

            if (string.Compare(this._biz.Search_SERIAL().Rows[0]["YN_SERIAL"].ToString(), "N") == 0)
                (this.btn시리얼등록).Visible = false;
            this.서버키enable();
        }

        private void InitGrid()
        {
            this._flexM.DetailGrids = new FlexGrid[] { this._flexD };
            this._flexM.WhenRowChangeThenGetDetail = true;

            #region GridM
            this._flexM.BeginSetting(1, 1, false);
            
            this._flexM.SetCol("S", "CHOOSE", 50, true, CheckTypeEnum.Y_N);
            this._flexM.SetCol("NM_COMPANY", "회사", 70);
            this._flexM.SetCol("NO_IO", "수불번호", 90);
            this._flexM.SetCol("DT_IO", "수불일", 90, false, typeof(string), (FormatTpType)7);
            this._flexM.SetCol("CD_PARTNER", "거래처코드", 80);
            this._flexM.SetCol("LN_PARTNER", "거래처", 120);
            this._flexM.SetCol("NM_YN_RETURN", "입고구분", 80);
            this._flexM.SetCol("YN_AM", "유무환구분", 80);
            this._flexM.SetCol("NM_PLANT", "공장", 120);
            this._flexM.SetCol("NM_KOR", "담당자", 120);
            this._flexM.SetCol("DC_RMK", "의뢰비고1", 120, true);
            this._flexM.SetCol("FILE_PATH_MNG", "첨부파일", 200, true);
            this._flexM.SetCol("TXT_USERDEF1", "송장번호", 120);
            this._flexM.SetCol("QT_ITEM", "종수", 40, false, typeof(decimal));

			this._flexM.SetDummyColumn(new string[] { "S" });
            this._flexM.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            if (this.strExcFile == "100" || this.strExcFile == "200")
            {
                this._flexM.DoubleClick += new EventHandler(this._flex_DoubleClick);
                this._flexM.SetExceptEditCol(new string[] { "FILE_PATH_MNG" });
            }
            else
            {
                this._flexM.HelpClick += new EventHandler(this._flex_DoubleClick);
                ToolStripMenuItem toolStripMenuItem = this._flexM.AddPopup("첨부파일");
                this._flexM.AddMenuItem(toolStripMenuItem, "다운로드", new EventHandler(this.File_Download));
                this._flexM.AddMenuItem(toolStripMenuItem, "삭제", new EventHandler(this.File_Delete));
            }
            #endregion

            #region GridD
            this._flexD.BeginSetting(1, 1, false);
            this._flexD.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);
            this._flexD.SetCol("CD_ITEM", "품목코드", 100);
            this._flexD.SetCol("NM_ITEM", "품목명", 150);
            this._flexD.SetCol("GRP_ITEM", "품목군코드", 80);
            this._flexD.SetCol("NM_ITEMGRP", "품목군명", 120);
            this._flexD.SetCol("GRP_MFG", "제품군코드", 80);
            this._flexD.SetCol("NM_GRP_MFG", "제품군명", 120);
            this._flexD.SetCol("STND_ITEM", "규격", 100);
            this._flexD.SetCol("CD_UNIT_MM", "발주단위", 60);
            this._flexD.SetCol("NM_SL", "창고명", 120);
            this._flexD.SetCol("NO_LOT", "LOT여부", 60);
            this._flexD.SetCol("NO_SERL", "S/N여부", 60);
            if (this.b수량권한)
                this._flexD.SetCol("QT_UNIT_MM", "수배수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("UNIT_IM", "재고단위", 60);
            if (this.b수량권한)
                this._flexD.SetCol("QT_GOOD_INV", "양품수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            if (this.b금액권한)
                this._flexD.SetCol("AM", "금액", 150, false, typeof(decimal), FormatTpType.MONEY);
            if (this.b수량권한)
                this._flexD.SetCol("QT_CLS", "마감수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexD.SetCol("NO_BL", "BL번호", 120);
            this._flexD.SetCol("NO_ISURCV", "의뢰번호", 100);
            this._flexD.SetCol("NO_PSO_MGMT", "발주번호", 100);
            this._flexD.SetCol("CD_PJT", "요청프로젝트", 120, false, typeof(string));
            this._flexD.SetCol("NM_PJT", "요청프로젝트명", 120, false, typeof(string));
            this._flexD.SetCol("CD_PJT_GR", "프로젝트", 120, false, typeof(string));
            this._flexD.SetCol("NM_PJT_GR", "프로젝트명", 120, false, typeof(string));
            if (this.b금액권한)
                this._flexD.SetCol("AM_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexD.SetCol("NM_CD_EXCH", "환종", 80);
            this._flexD.SetCol("NM_QTIOTP", "입고형태", 120);
            this._flexD.SetCol("NM_FG_TRANS", "거래구분", 80);
            this._flexD.SetCol("NM_WH", "W/H", 80);
            if (this.b단가권한)
                this._flexD.SetCol("UM_EX", "단가(재고단위)", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("NO_REV", "가입고번호", 100, false);
            if (this.b단가권한)
                this._flexD.SetCol("UM_EX_PSO", "단가(발주단위)", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexD.SetCol("DC1", "발주라인비고1", 100, false);
            this._flexD.SetCol("NO_DESIGN", "품목도면번호", 100, false);
            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                if (App.SystemEnv.PMS사용)
                {
                    this._flexD.SetCol("CD_CSTR", "CBS품목코드", 110, false, typeof(string));
                    this._flexD.SetCol("DL_CSTR", "CBS내역코드", 80, false, typeof(string));
                    this._flexD.SetCol("NM_CSTR", "CBS항목명", 140, false, typeof(string));
                    this._flexD.SetCol("SIZE_CSTR", "CBS규격", 140, false, typeof(string));
                    this._flexD.SetCol("UNIT_CSTR", "CBS단위", 110, false, typeof(string));
                    this._flexD.SetCol("QTY_ACT", "CBS예산수량", 120, false, typeof(decimal), FormatTpType.QUANTITY);
                    this._flexD.SetCol("UNT_ACT", "CBS예산단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
                    this._flexD.SetCol("AMT_ACT", "CBS예산금액", 100, false, typeof(decimal), FormatTpType.MONEY);
                }
                this._flexD.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexD.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexD.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
                this._flexD.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
                this._flexD.SetCol("NO_PJT_DESIGN", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 도면번호" : "프로젝트 도면번호", 140, false, typeof(string));
            }
            this._flexD.SetCol("STND_DETAIL_ITEM", "세부규격", 80);
            this._flexD.SetCol("MAT_ITEM", "재질", 80);
            this._flexD.SetCol("DC_RMK", "의뢰라인비고", 100);
            this._flexD.SetCol("NO_IOLINE", "수불항번", 100, false, typeof(decimal));
            this._flexD.SetCol("NM_MAKER", "MAKER", 100, false, typeof(string));
            if (Global.MainFrame.ServerKeyCommon == "SBPN")
                this._flexD.SetCol("AM_TRAN", "운반비금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            if (Global.MainFrame.ServerKeyCommon == "MHIK")
            {
                this._flexD.SetCol("TXT_USERDEF4", "전용발주번호", 120);
                this._flexD.Cols.Move(this._flexD.Cols["TXT_USERDEF4"].Index, 1);
            }
            if (MA.ServerKey(false, new string[1] { "WSMETAL" }))
                this._flexD.SetCol("DC_RMK_IO", "비고", 100, true);
            this._flexD.SetCol("EN_ITEM", "품목명(영)", false);
            this._flexD.NewRowEditable = false;
            this._flexD.EnterKeyAddRow = false;
            Config.UserColumnSetting.InitGrid_UserMenu(this._flexD, this.PageID, true);
            this._flexD.SetDummyColumn(new string[] { "S" });
            this._flexD.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexD.SetExceptSumCol(new string[] { "RT_EXCH", "NO_IOLINE" });

            #endregion
        }

        private void InitEvent()
        {
            this._flexM.AfterRowChange += _flexM_AfterRowChange;
            this._flexM.AfterEdit += _flexM_AfterEdit;
            this._flexM.CheckHeaderClick += this._flex_CheckHeaderClick;
            this._flexD.ValidateEdit += _flexD_ValidateEdit;
            this._flexD.CheckHeaderClick += this._flex_CheckHeaderClick;

            this.ctx창고.QueryBefore += OnBpCodeTextBox_QueryBefore;
            this.cbo공장.KeyDown += this.Control_KeyEvent;
            this.cbo입고구분.KeyDown += this.Control_KeyEvent;
            this.cbo거래구분.KeyDown += this.Control_KeyEvent;
            this.cbo마감구분.SelectionChangeCommitted += this.cbo마감구분_SelectionChangeCommitted;
            this.cbo마감구분.KeyDown += this.Control_KeyEvent;
            this.ctx입고형태.QueryBefore += OnBpCodeTextBox_QueryBefore;
            this.cbo발주형태.QueryBefore += OnBpCodeTextBox_QueryBefore;
            this.btn시리얼등록.Click += this.btn_시리얼등록_Click;
            this.btn시리얼출력.Click += this.btn_시리얼출력_Click;
            this.btnWBS.Click += this.btnWBS_Click;
        }
        private void _flexM_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flexM[this._flexM.Row, "S"].ToString() == "Y")
                {
                    for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                        this._flexD.SetCellCheck(index, 1, CheckEnum.Checked);
                }
                else
                {
                    for (int index = this._flexD.Rows.Fixed; index < this._flexD.Rows.Count; ++index)
                        this._flexD.SetCellCheck(index, 1, CheckEnum.Unchecked);
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

            DataSet comboData = this.GetComboData(new string[] { "N;MA_PLANT", "S;PU_C000016", "S;PU_C000027"});
            this.cbo공장.DataSource = comboData.Tables[0].Copy();
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            this.cbo거래구분.DataSource = comboData.Tables[1];
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";
            this.cbo입고구분.DataSource = comboData.Tables[2];
            this.cbo입고구분.DisplayMember = "NAME";
            this.cbo입고구분.ValueMember = "CODE";
            this.cbo마감구분.DataSource = MA.GetCodeUser(new string[] { "", "001", "002" }, new string[] { "", "미처리", "처리" });
            this.cbo마감구분.DisplayMember = "NAME";
            this.cbo마감구분.ValueMember = "CODE";

            this.dtp기간.StartDateToString = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp기간.EndDateToString = this.MainFrameInterface.GetStringToday;
            this.ctx회사.AddItem(this.LoginInfo.CompanyCode, this.LoginInfo.CompanyName);


            if (this.fg_gubun == "P_SA_Z_JONGHAP_STATUS_02")
            {
                this.dtp기간.StartDateToString = this.dt_start;
                this.dtp기간.EndDateToString = this.dt_end;
                this.ctx프로젝트.CodeValue = this.strCD_PJT;
                this.ctx프로젝트.CodeName = this.strNM_PJT;
                this.cbo마감구분.SelectedValue = this.rt_fg_cls;
                this.cbo공장.SelectedIndex = 0;
            }
            else if (this.dt_start != null && this.dt_start != string.Empty)
            {
                this.dtp기간.StartDateToString = this.dt_start;
                this.dtp기간.EndDateToString = this.dt_end;
                this.cbo공장.SelectedValue = this.cd_plant;
                if (this.fg_gubun == "재고자산수불부")
                    this.cbo마감구분.SelectedValue = this.rt_fg_cls;
                base.OnToolBarSearchButtonClicked(null, null);
            }
            this.strExcFile = BASIC.GetMAEXC_Menu(nameof(P_CZ_PU_GRM_REG), "PU_A00000001");

            if (D.GetString(this.strNO_IO) != "")
                base.OnToolBarSearchButtonClicked(null, null);
            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            if (!App.SystemEnv.PMS사용)
                return;
            this.btnWBS.Visible = true;
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
         {
            try
            {
                if (!this.Field_Check())
                    return;
                string startDateToString = this.dtp기간.StartDateToString;
                string endDateToString = this.dtp기간.EndDateToString;
                string CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                string queryWhereInPipe1 = this.ctx창고.QueryWhereIn_Pipe;
                string CD_PARTNER = this.ctx거래처명.CodeValue.ToString();
                string FG_TRANS = D.GetString(this.cbo거래구분.SelectedValue);
                string FG_RETURN = D.GetString(this.cbo입고구분.SelectedValue);
                string queryWhereInPipe2 = this.ctx입고형태.QueryWhereIn_Pipe;
                string NO_EMP = this.ctx담당자.CodeValue.ToString();
                string IS_CLOSED = D.GetString(this.cbo마감구분.SelectedValue);
                string codeValue = this.ctx프로젝트.CodeValue;
                string text1 = this.txt전용발주번호.Text;
                string queryWhereInPipe3 = this.cbo발주형태.QueryWhereIn_Pipe;
                string text2 = this.txt수불번호.Text;
                DataTable dataTable = !(D.GetString(this.strNO_IO) != "") ? this._biz.Search_Main(this.ctx회사.QueryWhereIn_Pipe, startDateToString, endDateToString, CD_PLANT, queryWhereInPipe1, CD_PARTNER, FG_TRANS, FG_RETURN, queryWhereInPipe2, NO_EMP, IS_CLOSED, this.rt_no_io, codeValue, text1, queryWhereInPipe3, text2) : this._biz.Search_Main(this.ctx회사.QueryWhereIn_Pipe, "09990101", "29991231", this.strCD_PLANT, queryWhereInPipe1, CD_PARTNER, FG_TRANS, FG_RETURN, queryWhereInPipe2, NO_EMP, this.strIS_CLOSED, this.strNO_IO, codeValue, text1, queryWhereInPipe3, text2);
                this.cbo마감구분_SelectionChangeCommitted(null, null);
                this._flexM.Binding = dataTable;
                if (!this._flexM.HasNormalRow)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
                this.fg_gubun = string.Empty;
                this.rt_no_io = string.Empty;
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
                if (!this.BeforeDelete())
                    return;

                this.ShowMessage("입고등록(딘텍)에서 삭제부탁드리겠습니다");

                // 구매입고 관리에서 삭제 안되고 입고등록에서 삭제하게 변경
                //DataRow[] dataRowArray1 = this._flexD.DataTable.Select("S ='Y'");
                //if (dataRowArray1 == null || dataRowArray1.Length < 1)
                //{
                //    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                //}
                //else
                //{
                //    this._flexM.Redraw = false;
                //    this._flexD.Redraw = false;
                //    MsgControl.ShowMsg(" 삭제대상 데이타 체크중입니다. \r\n잠시만 기다려주세요!");
                //    bool flag1 = false;
                //    StringBuilder stringBuilder = new StringBuilder();
                //    string str1 = "마감번호     항번 거래처      품목코드    품목명       규격         마감수량   입고번호     입고항번";
                //    stringBuilder.AppendLine(str1);
                //    string str2 = "-".PadRight(60, '-');
                //    stringBuilder.AppendLine(str2);
                //    foreach (DataRow dataRow in dataRowArray1)
                //    {
                //        if (Convert.ToDecimal(dataRow["QT_CLS"]) > 0M)
                //        {
                //            DataTable dataTable = Global.MainFrame.FillDataTable("SELECT NO_IV, NO_LINE, LN_PARTNER FROM PU_IVL A LEFT OUTER JOIN MA_PARTNER B ON A.CD_PARTNER = B.CD_PARTNER AND A.CD_COMPANY = B.CD_COMPANY WHERE A.CD_COMPANY = '" + dataRow["CD_COMPANY"].ToString() + "' AND NO_IO = '" + dataRow["NO_IO"].ToString() + "' AND NO_IOLINE = '" + dataRow["NO_IOLINE"].ToString() + "' ");
                //            string str3 = dataTable.Rows[0]["NO_IV"].ToString().PadRight(14, ' ') + " " + dataTable.Rows[0]["NO_LINE"].ToString().PadRight(3, ' ') + " " + dataTable.Rows[0]["LN_PARTNER"].ToString().PadRight(10, ' ') + " " + dataRow["CD_ITEM"].ToString().PadRight(8, ' ') + " " + dataRow["NM_ITEM"].ToString().PadRight(10, ' ') + " " + dataRow["STND_ITEM"].ToString().PadRight(10, ' ') + " " + string.Format("{0:n}", dataRow["QT_CLS"]).PadRight(10, ' ') + " " + dataRow["NO_IO"].ToString().PadRight(14, ' ') + " " + dataRow["NO_IOLINE"].ToString().PadRight(3, ' ');
                //            stringBuilder.AppendLine(str3);
                //            flag1 = true;
                //        }
                //    }
                //    this._flexM.Redraw = true;
                //    this._flexD.Redraw = true;
                //    MsgControl.CloseMsg();
                //    if (flag1)
                //    {
                //        this.ShowDetailMessage("이미 마감이 이루어져 변경할 수 없는 건이 있습니다 \n 미마감건에 대해서만 삭제가능합니다.  \n ▼ 버튼을 눌러서 입고 목록을 확인하세요!", stringBuilder.ToString());
                //    }
                //    else
                //    {
                //        string maexc = BASIC.GetMAEXC("POP연동");
                //        bool flag2 = false;
                //        DataTable dtH = this._flexM.DataTable.Clone();
                //        DataTable dtD = this._flexD.DataTable.Clone();
                //        if (Global.MainFrame.ShowMessage("삭제하시겠습니까?", "QY2") == DialogResult.Yes)
                //        {
                //            if (dataRowArray1 != null && dataRowArray1.Length > 0)
                //            {
                //                for (int index = this._flexM.Rows.Count - 1; index >= this._flexM.Rows.Fixed; --index)
                //                {
                //                    if (this._flexM[index, "S"].ToString() == "Y")
                //                    {
                //                        DataRow[] dataRowArray2 = this._flexM.DataTable.Select("NO_IO = '" + D.GetString(this._flexM[index, "NO_IO"]) + "'");
                //                        dtH.ImportRow(dataRowArray2[0]);
                //                        if (D.GetString(this._flexM.DataTable.Rows[index - 1]["NO_POP"]) != string.Empty)
                //                            flag2 = true;
                //                    }
                //                    else
                //                    {
                //                        foreach (DataRow row in this._flexD.DataTable.Select(string.Format("NO_IO = '{0}'", this._flexM[index, "NO_IO"].ToString())))
                //                        {
                //                            if (row["S"].Equals("Y"))
                //                            {
                //                                dtD.ImportRow(row);
                //                                if (D.GetString(row["NO_POP"]) != string.Empty)
                //                                    flag2 = true;
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //            if (maexc != "000" && flag2 && this.ShowMessage("타 시스템으로부터 연동된 처리건입니다. 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                //                return;
                //            if (this._biz.Delete(dtH, dtD))
                //            {
                //                this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다, new string[0]);
                //            }
                //            base.OnToolBarSearchButtonClicked(sender, e);
                //        }
                //    }
                //}



            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave() || !this.MsgAndSave(PageActionMode.Save))
                    return;
                this.ShowMessage(PageResultMode.SaveGood);
                base.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            DataTable changes1 = this._flexM.GetChanges();
            DataTable changes2 = this._flexD.GetChanges();
            if (changes1 == null && changes2 == null)
            {
                this.ShowMessage(공통메세지.변경된내용이없습니다, new string[0]);
                return false;
            }
            bool flag = this._biz.Save(changes1, (DataTable)null, D.GetString(this.cbo공장.SelectedValue), changes2);
            if (!flag)
                return false;
            this._flexM.AcceptChanges();
            return flag;
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                string str1 = "R_PU_GRM_REG_0";
                string str2 = "입고관리";
                string str3 = "NO_IO";
                string str4 = "";
                DataRow[] dataRowArray = this._flexM.DataTable.Select("S ='Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    for (int index = this._flexM.Rows.Fixed; index < this._flexM.Rows.Count; ++index)
                    {
                        if (this._flexM[index, "S"].ToString() == "Y")
                            str4 = str4 + this._flexM[index, str3].ToString() + "|";
                    }
                    if (!this._flexM.HasNormalRow)
                        return;
                    if (ConfigPU.PU_EXC.시리얼_라벨출력 == "N" || ConfigPU.PU_EXC.시리얼_라벨출력 == "")
                    {
                        ReportHelper reportHelper = new ReportHelper(str1, str2);
                        reportHelper.가로출력();
                        DataTable dataTable = new P_CZ_PU_GRM_REG_BIZ().Search_Print(new object[] { this.LoginInfo.CompanyCode,
                                                                                                    this.dtp기간.StartDateToString,
                                                                                                    this.dtp기간.EndDateToString,
                                                                                                    D.GetString(this.cbo공장.SelectedValue),
                                                                                                    this.ctx창고.QueryWhereIn_Pipe,
                                                                                                    this.ctx거래처명.CodeValue.ToString(),
                                                                                                    this.cbo거래구분.SelectedValue.ToString(),
                                                                                                    this.cbo입고구분.SelectedValue.ToString(),
                                                                                                    this.ctx입고형태.QueryWhereIn_Pipe,
                                                                                                    this.ctx담당자.CodeValue.ToString(),
                                                                                                    this.cbo마감구분.SelectedValue.ToString(),
                                                                                                    str4,
                                                                                                    this.ctx프로젝트.CodeValue,
                                                                                                    Global.SystemLanguage.MultiLanguageLpoint.ToString(),
                                                                                                    this.cbo발주형태.QueryWhereIn_Pipe });
                        reportHelper.SetData("기간from", this.dtp기간.StartDateToString);
                        reportHelper.SetData("기간to", this.dtp기간.EndDateToString);
                        reportHelper.SetData("공장명", this.cbo공장.SelectedText.ToString());
                        reportHelper.SetData("거래처명", this.ctx거래처명.CodeName.ToString());
                        reportHelper.SetData("거래구분", this.cbo거래구분.SelectedText.ToString());
                        reportHelper.SetData("입고구분", this.cbo입고구분.SelectedText.ToString());
                        reportHelper.SetData("입고형태", this.ctx입고형태.CodeName.ToString());
                        reportHelper.SetData("마감구분", this.cbo마감구분.SelectedText.ToString());
                        reportHelper.SetData("담당자", this.ctx담당자.CodeName.ToString());
                        reportHelper.SetDataTable(dataTable);
                        reportHelper.Print();
                    }
                    else
                        BASICPU.Serial_Print(str4);
                    this._flexM.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flexM.IsBindingEnd || !this._flexM.HasNormalRow)
                    return;
                DataTable dataTable = null;
                string str = "NO_IO= '" + this._flexM[this._flexM.Row, "NO_IO"].ToString() + "'";
                string IS_CLOSED = this.cbo마감구분.SelectedValue.ToString();
                string codeValue = this.ctx프로젝트.CodeValue;
                string text = this.txt전용발주번호.Text;
                string queryWhereInPipe = this.cbo발주형태.QueryWhereIn_Pipe;
                if (this._flexM.DetailQueryNeed)
                {
                    if (D.GetString(this.strNO_IO) != "")
                    {
                        dataTable = this._biz.Search_Detail(this._flexM["CD_COMPANY"].ToString(),this._flexM[this._flexM.Row, "NO_IO"].ToString(), this.strIS_CLOSED, codeValue, text, queryWhereInPipe);
                        this.strNO_IO = "";
                    }
					else
                        dataTable = this._biz.Search_Detail(this._flexM["CD_COMPANY"].ToString(),this._flexM[this._flexM.Row, "NO_IO"].ToString(), IS_CLOSED, codeValue, text, queryWhereInPipe);
                }
                this._flexD.BindingAdd(dataTable, str);
                this._flexM.DetailQueryNeed = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private bool Field_Check()
        {
            if (this.dtp기간.StartDateToString.Trim() == "")
            {
                this.MainFrameInterface.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl기간.Text });
                this.dtp기간.Focus();
                return false;
            }
            if (this.dtp기간.EndDateToString.Trim() == "")
            {
                this.MainFrameInterface.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl기간.Text });
                this.dtp기간.Focus();
                return false;
            }
            if (this.cbo공장.SelectedValue != null && !(D.GetString(this.cbo공장.SelectedValue) == ""))
                return true;
            this.MainFrameInterface.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl공장명.Text });
            this.dtp기간.Focus();
            return false;
        }

        private void OnControl_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!((DatePicker)sender).Modified || ((DatePicker)sender).IsValidated)
                    return;
                this.ShowMessage(공통메세지.입력형식이올바르지않습니다, new string[0]);
                ((Control)sender).Focus();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnControl_CalendarClosed(object sender, EventArgs e)
        {
            try
            {
                this.OnControl_Validated(sender, e);
                if (this.dtp기간.StartDateToString.Trim().Length != 8 || this.dtp기간.EndDateToString.Trim().Length != 8 || !(new DateTime(Convert.ToInt32(this.dtp기간.StartDateToString.Substring(0, 4)), Convert.ToInt32(this.dtp기간.StartDateToString.Substring(4, 2)), Convert.ToInt32(this.dtp기간.StartDateToString.Substring(6, 2))) > new DateTime(Convert.ToInt32(this.dtp기간.EndDateToString.Substring(0, 4)), Convert.ToInt32(this.dtp기간.EndDateToString.Substring(4, 2)), Convert.ToInt32(this.dtp기간.EndDateToString.Substring(6, 2)))))
                    return;
                this.dtp기간.StartDateToString = "";
                this.dtp기간.EndDateToString = "";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpCodeTextBox_QueryBefore(object sender, BpQueryArgs e)
        {
            HelpID helpId = e.HelpID;
            if (helpId != HelpID.P_MA_SL_SUB)
            {
                if (helpId != HelpID.P_PU_EJTP_SUB)
                {
                    if (helpId != HelpID.P_PU_TPPO_SUB1)
                        return;
                    e.HelpParam.P61_CODE1 = "N";
                    if (!(this._반품발주 == "Y"))
                        return;
                    e.HelpParam.P41_CD_FIELD1 = "Y";
                }
                else
                    e.HelpParam.P61_CODE1 = "001|030|031|";
            }
            else if (D.GetString(this.cbo공장.SelectedValue) == "")
            {
                this.ShowMessage("PU_M000070");
            }
            else
                e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
        }

        private void cbo마감구분_SelectionChangeCommitted(object sender, EventArgs e) => this.Page_DataChanged(null, null);

        private void Page_DataChanged(object sender, EventArgs e)
        {
            this.ToolBarAddButtonEnabled = false;
            this.ToolBarDeleteButtonEnabled = this.cbo마감구분.SelectedValue.ToString() == "001";
        }

        private void Control_KeyEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Return)
                return;
            SendKeys.SendWait("{TAB}");
        }

        private void btn_시리얼등록_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                DataTable dtSERL = null;
                DataRow[] dataRowArray = this._flexD.DataTable.DefaultView.ToTable().Select("NO_SERL = 'YES'");
                DataTable dt = this._flexD.DataTable.Clone();
                if (dataRowArray.Length > 0)
                {
                    foreach (DataRow row in dataRowArray)
                        dt.ImportRow(row);
                    if (D.GetString(this._flexM["YN_RETURN"]) == "N")
                    {
                        P_PU_SERL_SUB_R pPuSerlSubR = new P_PU_SERL_SUB_R(dt);
                        if (pPuSerlSubR.ShowDialog(this) != DialogResult.OK)
                            return;
                        dtSERL = pPuSerlSubR.dtL;
                    }
                    else
                    {
                        P_PU_SERL_SUB_I pPuSerlSubI = new P_PU_SERL_SUB_I(dt);
                        if (pPuSerlSubI.ShowDialog(this) != DialogResult.OK)
                            return;
                        dtSERL = pPuSerlSubI.dtL;
                    }
                }
                if (dtSERL == null || !this._biz.Save(null, dtSERL, D.GetString(this.cbo공장.SelectedValue), null))
                    return;
                this.ShowMessage("시리얼이 정상적으로 등록되었습니다.");
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn_시리얼출력_Click(object sender, EventArgs e)
        {
            MSCommClass msCommClass = new MSCommClass();
            ASCIIEncoding asciiEncoding = new ASCIIEncoding();
            int length1 = "".Length;
            int num1 = 0;
            long num2 = 0;
            string str1 = "";
            List<object> objectList1 = new List<object>();
            List<object> objectList2 = new List<object>();
            List<object> objectList3 = new List<object>();
            msCommClass.CommPort = (short)1;
            msCommClass.Settings = "9600,n,8,1";
            msCommClass.PortOpen = true;
            try
            {
                if (!this._flexD.HasNormalRow)
                    return;
                foreach (DataRow row in DBHelper.GetDataTable("UP_PU_PO_SERIAL_PRT", new object[] { MA.Login.회사코드,
                                                                                                    this._flexM[( this._flexM).Row, "NO_IO"].ToString(),
                                                                                                    Global.SystemLanguage.MultiLanguageLpoint.ToString() }).Rows)
                {
                    string str2 = row["NO_SERIAL"].ToString();
                    string str3 = row["NM_ITEM"].ToString();
                    int num3 = 0;
                    int num4 = 0;
                    int startIndex1 = 0;
                    objectList1.Clear();
                    objectList2.Clear();
                    objectList3.Clear();
                    int length2 = str3.Length;
                    msCommClass.Output = "{C|}";
                    for (int startIndex2 = 0; startIndex2 < length2; ++startIndex2)
                    {
                        char ch = Convert.ToChar(str3.Substring(startIndex2, 1));
                        if (Convert.ToInt32(ch) > 0 && Convert.ToInt32(ch) < 128)
                            objectList3.Add("N");
                        else
                            objectList3.Add("Y");
                    }
                    for (int index = 0; index < length2; ++index)
                    {
                        if (index == 0)
                            str1 = objectList3[index].ToString();
                        string str4 = objectList3[index].ToString();
                        if (str1 == str4)
                        {
                            ++num4;
                            if (index == length2 - 1)
                            {
                                ++num3;
                                objectList1.Add(num4);
                                objectList2.Add(str4);
                            }
                        }
                        else
                        {
                            ++num3;
                            objectList1.Add(num4);
                            objectList2.Add(str1);
                            num4 = 1;
                        }
                        str1 = objectList3[index].ToString();
                    }
                    msCommClass.Output = "{D0130,0550,0100|}";
                    for (int index = 0; index < num3; ++index)
                    {
                        string str5 = objectList2[index].ToString();
                        int int32 = Convert.ToInt32(objectList1[index]);
                        if (index > 0)
                        {
                            startIndex1 = num1;
                        }
                        else
                        {
                            num2 = 60L;
                            num1 = 0;
                        }
                        string str6 = str3.Substring(startIndex1, int32);
                        if (str5 == "N")
                        {
                            msCommClass.Output = ("{PV04;" + num2.ToString("0000") + ",0020,0020,0020,A,-006,00,B,+0000000000|}");
                            msCommClass.Output = ("{RV04;" + str6 + "|}");
                        }
                        else
                        {
                            msCommClass.Output = ("{PC001;" + num2.ToString("0000") + ",0025,10,20,51,+00,00,B|}");
                            msCommClass.Output = ("{RC001;" + str6 + "|}");
                        }
                        num1 = int32 + num1;
                        if (str5 == "N")
                            num2 += (long)(int32 * 15);
                        else
                            num2 += (long)(int32 / 2 * 15);
                    }
                    msCommClass.Output = "{PV04;0060,0050,0020,0020,A,-006,00,B,+0000000000|}";
                    msCommClass.Output = ("{RV04;" + str2 + "|}");
                    msCommClass.Output = "{XB01;0040,0055,C,1,02,0,0045,+0000000000,000,0,01|}";
                    msCommClass.Output = ("{RB01;" + str2 + "|}");
                    msCommClass.Output = "{XS;I,0001,0002C5101|}";
                }
                msCommClass.PortOpen = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnWBS_Click(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(this._flexD["ID_MEMO"]) == string.Empty)
                    return;
                object[] objArray = new object[] { "A06",
                                                   "",
                                                   Global.MainFrame.LoginInfo.CompanyCode,
                                                   D.GetString(this._flexD["CD_PJT"]),
                                                   D.GetString(this._flexD["CD_WBS"]),
                                                   D.GetString(this._flexD["NO_SHARE"]),
                                                   D.GetString(this._flexD["NO_ISSUE"]),
                                                   "04" };
                new P_WS_PM_S_JOBSHARE_SUB1(this, D.GetString(this._flexD["ID_MEMO"]), objArray).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 서버키enable()
        {
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "MDSTEC")
                this.btn시리얼출력.Visible = true;
            if (!(Global.MainFrame.ServerKeyCommon == "MHIK"))
                return;
            this.lbl전용발주번호.Visible = true;
            this.txt전용발주번호.Visible = true;
            this.bpPanelControl13.Visible = true;
            this.oneGridItem5.Visible = true;
            this.oneGrid1.Size = new Size(1054, 131);
        }

        private void _flexD_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (!(this._flexD.GetData(e.Row, e.Col).ToString() != this._flexD.EditData))
                    return;
                switch (this._flexD.Cols[e.Col].Name)
                {
                    case "S":
                        bool flag = false;
                        for (int index = this._flexD.Rows.Fixed; index < this._flexD.DataView.Count + this._flexD.Rows.Fixed; ++index)
                        {
                            if (this._flexD.EditData != "Y" || e.Row != index && this._flexD[index, "S"].Equals("N"))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            this._flexM["S"] = "N";
                            break;
                        }
                        break;
                }
            }
            catch
            {
            }
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                switch ((sender as FlexGrid).Name)
                {
                    case "_flexM":
                        this._flexM.Row = 1;
                        if (!this._flexM.HasNormalRow || !this._flexD.HasNormalRow)
                            break;
                        for (int index1 = 0; index1 < this._flexM.Rows.Count - 1; ++index1)
                        {
                            this._flexM.Row = index1 + 1;
                            for (int index2 = this._flexD.Rows.Fixed; index2 < this._flexD.Rows.Count; ++index2)
                            {
                                if (this._flexD.RowState(index2) != DataRowState.Deleted)
                                    this._flexD[index2, "S"] = D.GetString(this._flexM["S"]);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                switch (this._flexM.Cols[this._flexM.Col].Name.ToString())
                {
                    case "NO_IO":
                        if (new P_PU_GRM_REG_SUB(new object[] { D.GetString(this._flexD["NO_IO"]) }).ShowDialog(this) == DialogResult.Cancel)
                            break;
                        break;
                    case "FILE_PATH_MNG":
                        if (this.strExcFile == "100" || this.strExcFile == "200")
                        {
                            string NO_IO = D.GetString(this._flexM["NO_IO"]);
                            string ID_MENU = Global.MainFrame.CurrentPageID;
                            if (this.strExcFile == "200")
                            {
                                DataTable noRcv = this._biz.GetNoRCV(NO_IO);
                                if (noRcv != null && noRcv.Rows.Count > 0)
                                    NO_IO = D.GetString((noRcv.Rows[0]["NO_RCV"].ToString() + "_" + Global.MainFrame.LoginInfo.CompanyCode));
                                ID_MENU = "P_PU_REQ_REG_1";
                            }
                            new AttachmentManager(Global.MainFrame.CurrentModule, ID_MENU, NO_IO).ShowDialog(this);
                            DataTable fileName = this._biz.Get_FileName(Global.MainFrame.LoginInfo.CompanyCode, NO_IO, ID_MENU);
                            this._flexM["FILE_PATH_MNG"] = fileName == null || fileName.Rows.Count == 0 ? string.Empty : fileName.Rows[0]["FILE_PATH_MNG"];
                            this._flexM.AcceptChanges();
                            break;
                        }
                        this._flexH_DoubleClick(null, null);
                        break;
                }
            }
            catch
            {
            }
        }

        public void _flexH_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flexM.Cols[this._flexM.Col].Name == "FILE_PATH_MNG")
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!openFileDialog.SafeFileName.Contains(this._flexM["NO_IO"].ToString()))
                        {
                            this.ShowMessage("첨부하려고하는 파일명과 수불번호가 일치하여야 합니다.");
                        }
                        else
                        {
                            ComFunc.UpLoad(this.MainFrameInterface.HostURL, openFileDialog.SafeFileName, openFileDialog.FileName, "/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/GI", "etc", "20090819");
                            this._flexM["FILE_PATH_MNG"] = openFileDialog.SafeFileName;
                            this._flexM.AcceptChanges();
                            base.SaveData();
                            this.ShowMessage("업로드완료");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void File_Download(object sender, EventArgs e)
        {
            try
            {
                if (this._flexM["FILE_PATH_MNG"].ToString() == string.Empty || this._flexM.Cols[this._flexM.Col].Name != "FILE_PATH_MNG")
                    return;
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string str = folderBrowserDialog.SelectedPath + "\\" + this._flexM["FILE_PATH_MNG"].ToString();
                    ComFunc.DownLoad(this.MainFrameInterface.HostURL + ("/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/GI") + "\\" + this._flexM["FILE_PATH_MNG"].ToString(), str);
                    this.ShowMessage("다운로드완료");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public void File_Delete(object sender, EventArgs e)
        {
            try
            {
                if (this._flexM["FILE_PATH_MNG"].ToString() == string.Empty || this._flexM.Cols[this._flexM.Col].Name != "FILE_PATH_MNG" || Global.MainFrame.ShowMessage("첨부파일을 삭제하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;
                ComFunc.DeleteFile(this.MainFrameInterface.HostURL, "/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/GI", this._flexM["FILE_PATH_MNG"].ToString());
                this.ShowMessage("파일 삭제 완료");
                this._flexM["FILE_PATH_MNG"] = string.Empty;
                base.SaveData();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

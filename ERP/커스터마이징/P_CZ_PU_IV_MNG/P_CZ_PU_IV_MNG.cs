using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Web;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.MF.EMail;
using Duzon.ERPU.OLD;
using Duzon.Windows.Print;
using pur;

namespace cz
{
    public partial class P_CZ_PU_IV_MNG : PageBase
    {
        #region 전역변수 & 초기화
        private P_CZ_PU_IV_MNG_BIZ _biz;

        private object[] param;
        private string 작성일자;
        private string 매입번호;
        private string 지급관리통제설정;
        private string 전자결재사용구분;

        public P_CZ_PU_IV_MNG()
        {
            StartUp.Certify(this);

            this.InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flexH };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
        }

        public P_CZ_PU_IV_MNG(object[] param)
            : this()
        {
            this.param = param;
        }

        public P_CZ_PU_IV_MNG(string 작성일자, string 매입번호)
            : this()
        {
            this.작성일자 = 작성일자;
            this.매입번호 = 매입번호;
        }

        protected override void InitLoad()
        {
            this.MA_EXC_SET();

            this._biz = new P_CZ_PU_IV_MNG_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        private void MA_EXC_SET()
        {
            if (ComFunc.전용코드("매입관리-메일기능사용") == "100")
                this.btn메일전송.Visible = true;
            else
                this.btn메일전송.Visible = false;

            this.지급관리통제설정 = BASIC.GetMAEXC("거래처정보등록-회계지급관리사용여부");

            if (BASIC.GetMAEXC("업체별프로세스") == "002")
            {
                this.btn관리구분배부.Visible = true;
                this.btn관리구분배부취소.Visible = true;
            }
            else
            {
                this.btn관리구분배부.Visible = false;
                this.btn관리구분배부취소.Visible = false;
            }

            this.전자결재사용구분 = BASIC.GetMAEXC("전자결재-사용구분");

            if (this.전자결재사용구분 != "000" && BASIC.GetMAEXC("전자결재메뉴별사용여부-매입관리") == "100")
                this.btn전자결재.Visible = true;
            else
                this.btn전자결재.Visible = false;

            if (App.SystemEnv.PMS사용)
                this.btn업무공유.Visible = true;
            else
                this.btn업무공유.Visible = false;

            if (Global.MainFrame.LoginInfo.CompanyCode == "W100" && Global.MainFrame.LoginInfo.UserID == "S2103")
                this.btnCC업데이트.Visible = true;
            else
                this.btnCC업데이트.Visible = false;
        }

        private void InitEvent()
        {
            this.btn관리구분배부취소.Click += new EventHandler(this.btn관리구분배부취소_Click);
            this.btn관리구분배부.Click += new EventHandler(this.btn관리구분배부_Click);
            this.btn메일전송.Click += new EventHandler(this.btn메일전송_Click);
            this.btn전자결재.Click += new EventHandler(this.btn전자결재_Click);
            this.btn발행옵션.Click += new EventHandler(this.btn발행옵션_Click);
            this.btn전표처리취소.Click += new EventHandler(this.btn전표처리취소_Click);
            this.btn미결전표처리.Click += new EventHandler(this.btn미결전표처리_Click);
			this.btn전표출력.Click += Btn전표출력_Click;
			this.btn첨부파일.Click += Btn첨부파일_Click;
            this.btn업무공유.Click += new EventHandler(this.btn업무공유_Click);
            this.btn선지급정리.Click += new EventHandler(this.btn선지급정리_Click);
            this.btnCC업데이트.Click += new EventHandler(this.btnCC업데이트_Click);
            this.cbo전표처리.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.cbo거래구분.KeyDown += new KeyEventHandler(this.Control_KeyDown);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.StartEdit += new RowColEventHandler(this.Grid_StartEdit);
            this._flexH.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this.Grid_BeforeCodeHelp);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flexH_ValidateEdit);
            this._flexH.DoubleClick += new EventHandler(this.Grid_DoubleClick);
            this._flexH.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flexH_OwnerDrawCell);
            this._flexL.CellContentChanged += new CellContentEventHandler(this._flexL_CellContentChanged);
        }

		protected override void InitPaint()
        {
            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl2.IsNecessaryCondition = true;
            this.bpPanelControl6.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();

            DataSet comboData1 = this.GetComboData(new string[] { "S;PU_C000016",
                                                                  "S;YESNO",
                                                                  "N;FI_J000002",
                                                                  "N;MA_B000095"});

            this.cbo거래구분.DataSource = comboData1.Tables[0];
            this.cbo거래구분.DisplayMember = "NAME";
            this.cbo거래구분.ValueMember = "CODE";

            this.cbo전표처리.DataSource = comboData1.Tables[1].Copy();
            this.cbo전표처리.DisplayMember = "NAME";
            this.cbo전표처리.ValueMember = "CODE";

            this.cbo반제여부.DataSource = comboData1.Tables[1].Copy();
            this.cbo반제여부.DisplayMember = "NAME";
            this.cbo반제여부.ValueMember = "CODE";

            this._flexH.SetDataMap("YN_JEONJA", comboData1.Tables[3], "CODE", "NAME");

            if (Certify.IsLive())
                this._flexH.SetDataMap("CD_DOCU", comboData1.Tables[2], "CODE", "NAME");
            else
            {
                this.btn발행옵션.Visible = false;
                this.btn미결전표처리.Visible = false;
                this.btn전표처리취소.Visible = false;
                this.btn선지급정리.Visible = false;
                this.bpPanelControl5.Visible = false;
                this.bpPanelControl10.Visible = false;
            }

            //if (this.지급관리통제설정 == "N")
            //    this._flexH.SetDataMap("FG_PAYBILL", Global.MainFrame.GetComboDataCombine("N;PU_C000044"), "CODE", "NAME");
            //else
            //{
            //    DataTable payList = ComFunc.GetPayList();
            //    if (payList != null)
            //        this._flexH.SetDataMap("FG_PAYBILL", payList, "CODE", "NAME");
            //}

            if (this.param != null)
            {
                this.ctx담당자.CodeValue = D.GetString(this.param[0]);
                this.ctx담당자.CodeName = D.GetString(this.param[1]);
                this.cbo거래구분.SelectedValue = D.GetString(this.param[2]);
                this.dtp처리일자.StartDateToString = D.GetString(this.param[3]);
                this.dtp처리일자.EndDateToString = D.GetString(this.param[3]);

                this.OnToolBarSearchButtonClicked(null, null);
            }
            else if (!string.IsNullOrEmpty(this.매입번호))
			{
                this.dtp처리일자.StartDateToString = this.작성일자;
                this.dtp처리일자.EndDateToString = this.MainFrameInterface.GetStringToday;

                this.txt매입번호.Text = this.매입번호;

                this.OnToolBarSearchButtonClicked(null, null);
            }
            else
            {
                this.dtp처리일자.StartDateToString = this.MainFrameInterface.GetStringFirstDayInMonth;
                this.dtp처리일자.EndDateToString = this.MainFrameInterface.GetStringToday;
            }
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_IV", "매입번호", 120);
            this._flexH.SetCol("DT_PROCESS", "처리일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("CD_PARTNER", "거래처코드", 80);
            this._flexH.SetCol("LN_PARTNER", "거래처명", 120);
            this._flexH.SetCol("NO_COMPANY", "사업자번호", 120);
            this._flexH.SetCol("NM_TRANS", "거래구분", 80);
            this._flexH.SetCol("NM_TAX", "과세구분", 80);
            this._flexH.SetCol("NM_EXCH", "환종", 80);
            this._flexH.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexH.SetCol("AM_EX", "외화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_K", "원화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("VAT_TAX", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_TOTAL", "합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_ADPAY", "선지급\n외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_ADPAY", "선지급\n원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            
            if (Certify.IsLive())
            {
                this._flexH.SetCol("TP_AIS", "처리상태", 80);
                this._flexH.SetCol("CD_DOCU", "전표유형", 80);
                this._flexH.SetCol("NO_DOCU", "전표번호", 120);
                this._flexH.SetCol("AM_EXDOCU", "전표외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_DOCU", "전표원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_EXBAN_ADPAY", "선지급반제전표\n외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_BAN_ADPAY", "선지급반제전표\n원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_EXBAN", "반제전표\n외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexH.SetCol("AM_BAN", "반제전표\n원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                //this._flexH.SetCol("FG_PAYBILL", "지급조건", 80, true, typeof(string));
                this._flexH.SetCol("DT_PAY_PREARRANGED", "지급예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                this._flexH.SetCol("DT_DUE", "만기일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                //this._flexH.SetCol("FG_AD_YN", "관리구분저장여부", 120);
                this._flexH.SetCol("YN_JEONJA", "계산서발행형태", 100, true);

                this._flexH.SetCol("NM_DEPT", "마감부서", 100);
                this._flexH.SetCol("NM_EMP", "마감담당자", 100);
                this._flexH.SetCol("DC_RMK", "비고", 200);
            }

            if (this.LoginInfo.CompanyLanguage == Language.KR)
            {
                this._flexH.Cols["NO_COMPANY"].Format = "###-##-#####";
                this._flexH.SetStringFormatCol("NO_COMPANY");
            }

            this._flexH.VerifyNotNull = new string[] { "NO_COMPANY" };

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexH.SetExceptSumCol(new string[] { "RT_EXCH" });
            this._flexH.SetDummyColumn(new string[] { "S" });

            this._flexH.Styles.Add("YELLOW").BackColor = Color.Yellow;
            this._flexH.Styles.Add("WHITE").BackColor = Color.White;
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NO_LINE", "항번", 40);
            this._flexL.SetCol("LN_PARTNER", "매입처", 120);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 100);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 100);
            this._flexL.SetCol("NM_TPPO", "발주유형", 80);
            this._flexL.SetCol("NM_CLS_ITEM", "품목계정", 100);
            this._flexL.SetCol("UNIT", "단위", 80);
            this._flexL.SetCol("QT_CLS", "수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NM_EXCH", "통화명", 80);
            this._flexL.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("UM_EX_CLS", "외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("UM_ITEM_CLS", "단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexL.SetCol("AM_EX_CLS", "외화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_CLS", "원화금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_TOTAL", "합계", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_ADPAY", "선지급외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_ADPAY", "선지급원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("CD_PJT", "프로젝트", 120);
            this._flexL.SetCol("NO_PO", "발주번호", 120);
            this._flexL.SetCol("NO_ORDER", "견적번호", 120);
            this._flexL.SetCol("NO_IO", "입고번호", 120);
            this._flexL.SetCol("DT_IO", "입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NM_CC", "C/C명", 100);
            this._flexL.SetCol("NM_PURGRP", "구매그룹", 120);
            this._flexL.SetCol("NM_EMP", "발주담당자", 120);

            Config.UserColumnSetting.InitGrid_UserMenu(this._flexL, this.PageID, true);

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexL.SetExceptSumCol(new string[] { "RT_EXCH",
                                                       "MEMO_CD",
                                                       "CHECK_PEN",
                                                       "UM_ITEM_CLS",
                                                       "UM_EX_CLS" });

            this._flexL.CellNoteInfo.EnabledCellNote = true;
            this._flexL.CellNoteInfo.CategoryID = this.Name;
            this._flexL.CellNoteInfo.DisplayColumnForDefaultNote = "NM_ITEM";
            this._flexL.CheckPenInfo.EnabledCheckPen = true;
            #endregion
        }
        #endregion

        #region 메인버튼 이벤트
        protected override bool BeforeSearch()
        {
            return base.BeforeSearch();
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;
                if (!this.FieldCheck()) return;

                DataTable dt = null;

                int 기간 = Math.Abs((this.dtp처리일자.StartDate - this.dtp처리일자.EndDate).Days);

                string startDate, endDate;
                DataTable tmpDataTable;

                for (int i = 0; i <= 기간 / 365; i++)
                {
                    startDate = this.dtp처리일자.StartDate.AddDays(i * 365).ToString("yyyyMMdd");

                    if (기간 >= (i + 1) * 365)
                        endDate = this.dtp처리일자.StartDate.AddDays(((i + 1) * 365) - 1).ToString("yyyyMMdd");
                    else
                        endDate = this.dtp처리일자.EndDateToString;

                    MsgControl.ShowMsg("조회 중 입니다. 잠시만 기다려 주세요.\n조회기간 (@ ~ @)", new string[] { Util.GetTo_DateStringS(startDate), Util.GetTo_DateStringS(endDate) });

                    tmpDataTable = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                   startDate,
                                                                   endDate,
                                                                   this.ctx매입처.CodeValue,
                                                                   D.GetString(this.cbo거래구분.SelectedValue),
                                                                   D.GetString(this.cbo전표처리.SelectedValue),
                                                                   this.ctx담당부서.CodeValue,
                                                                   this.ctx담당자.CodeValue,
                                                                   this.txt매입번호.Text,
                                                                   this.txt발주번호.Text,
                                                                   this.txt프로젝트번호.Text,
                                                                   D.GetString(this.cbo반제여부.SelectedValue) });

                    if (i == 0)
                        dt = tmpDataTable;
                    else
                        dt.Merge(tmpDataTable);
                }

                if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
                    dt.DefaultView.Sort = "DTS_INSERT DESC";
                else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                    dt.DefaultView.Sort = "NO_IV ASC";
                
                this._flexH.Binding = dt;

                if (!this._flexH.HasNormalRow)
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                MsgControl.CloseMsg();
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
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !this.Verify()) return false;

            DataTable changes = this._flexH.GetChanges();

            foreach (DataRow dataRow in changes.Rows)
            {
                if (DBNull.Value == dataRow["VAT_TAX"])
                    return false;
            }

            if (changes == null || !this._biz.Save(changes))
                return false;

            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();

            return true;
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray1 == null || dataRowArray1.Length < 1)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S = 'Y' AND TP_AIS ='Y'");

                if (dataRowArray2 != null && dataRowArray2.Length > 0)
                {
                    this.ShowMessage("PU_M000094");
                    return;
                }

                if (BASIC.GetMAEXC("업체별프로세스") == "002")
                {
                    DataRow[] dataRowArray3 = this._flexH.DataTable.Select("S = 'Y' AND FG_AD_YN ='Y'");
                    if (dataRowArray3 != null && dataRowArray3.Length > 0)
                    {
                        this.ShowMessage(this.DD("관리구분배부된 자료가존재합니다."));
                        return;
                    }
                }
                else
				{
                    string query = @"SELECT 1 
FROM CZ_PU_ADPAYMENT_BILL_D WITH(NOLOCK)
WHERE CD_COMPANY = '{0}' 
AND NO_IV = '{1}'";

                    foreach (DataRow dr in dataRowArray1)
                    {
                        DataTable dt = DBHelper.GetDataTable(string.Format(query, this.LoginInfo.CompanyCode, dr["NO_IV"].ToString()));

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.ShowMessage("선지급정리된 건이 선택되어 있습니다.");
                            return;
                        }
                    }

                    if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes)
                        return;

                    foreach (DataRow dr in dataRowArray1)
                    {
                        this._biz.Delete(new object[] { dr["NO_IV"].ToString(), this.LoginInfo.CompanyCode });
                    }

                    this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
                this.OnToolBarSearchButtonClicked(null, null);
            }
        }

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarPrintButtonClicked(sender, e);

                string Multikey = "";
                DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    for (int @fixed = this._flexH.Rows.Fixed; @fixed < this._flexH.Rows.Count; ++@fixed)
                    {
                        if (this._flexH[@fixed, "S"].ToString() == "Y")
                            Multikey = Multikey + this._flexH[@fixed, "NO_IV"].ToString() + "|";
                    }

                    if (!this._flexH.HasNormalRow)
                        return;

                    Dictionary<string, string> 헤더데이터 = new Dictionary<string, string>();
                    헤더데이터["처리일자"] = this.dtp처리일자.StartDateToString.Substring(0, 4) + "/" + this.dtp처리일자.StartDateToString.Substring(4, 2) + "/" + this.dtp처리일자.StartDateToString.Substring(6, 2) + " ~ " + this.dtp처리일자.EndDateToString.Substring(0, 4) + "/" + this.dtp처리일자.EndDateToString.Substring(4, 2) + "/" + this.dtp처리일자.EndDateToString.Substring(6, 2);
                    헤더데이터["담당부서코드"] = this.ctx담당자.CodeName;
                    헤더데이터["담당부서명"] = this.ctx담당부서.CodeName;
                    헤더데이터["거래처코드"] = this.ctx매입처.CodeValue;
                    헤더데이터["거래처명"] = this.ctx매입처.CodeName;
                    헤더데이터["거래구분"] = (this.cbo거래구분).Text;
                    헤더데이터["전표처리"] = (this.cbo전표처리).Text;
                    헤더데이터["담당자코드"] = this.ctx담당자.CodeValue;
                    헤더데이터["담당자명"] = (this.ctx담당자.CodeName).ToString();
                    new P_CZ_PU_IV_MNG_PRINT("R_PU_IV_MNG_0", "매입관리", true, this._biz.Print(this.MainFrameInterface.LoginInfo.CompanyCode, Multikey), 헤더데이터).ShowPrintDialog();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void btn미결전표처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                string str1 = string.Empty;

                if (this.ToolBarSaveButtonEnabled)
                {
                    this.ShowMessage(this.DD("변경된 사항이 있습니다. 저장 후에 다시 처리 해야 합니다."));
                    return;
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");
                    if (dataRowArray1 == null || dataRowArray1.Length <= 0)
                    {
                        this.ShowMessage("IK1_007");
                        return;
                    }
                    else
                    {
                        DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S = 'Y' AND TP_AIS ='Y'");
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            this.ShowMessage("이미 전표처리된 건이 포함되어 있습니다");
                            return;
                        }
                        else
                        {
                            if (BASIC.GetMAEXC("업체별프로세스") == "002")
                            {
                                DataRow[] dataRowArray3 = this._flexH.DataTable.Select("S = 'Y' AND FG_AD_YN ='N'");
                                if (dataRowArray3 != null && dataRowArray3.Length > 0)
                                {
                                    this.ShowMessage(this.DD("관리구분배부 안된 자료가 존재합니다."));
                                    return;
                                }
                            }

                            DataRow[] dataRowArray4 = this._flexH.DataTable.Select("S = 'Y' AND TP_AIS = 'N' AND  AM_K = 0 AND VAT_TAX = 0 ");
                            if (dataRowArray4 != null && dataRowArray4.Length > 0 && MessageBox.Show("공급금액/부가세가 0인 항목이 있습니다.\r\n당 항목은 전표처리표시를 하되 회계전표를 만들지않습니다.\r\n계속하시겠습니까?", "0값처리", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
                                return;

                            bool flag = false;

                            foreach (DataRow dataRow in dataRowArray1)
                            {
                                if (!this._biz.미결전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           D.GetString(dataRow["NO_IV"]),
                                                                           "210",
                                                                           Settings.Default.Tx_내역표시구분,
                                                                           Settings.Default.Tx_내역표시_Text,
                                                                           Settings.Default.Tx_품목표시구분,
                                                                           Global.MainFrame.LoginInfo.EmployeeNo }))
                                {
                                    this.ShowMessage("@(@) 의 전표처리가 정상적으로 처리되지 않았습니다.", new string[] { this.DD("매입번호"), dataRow["NO_IV"].ToString() }, "IK1");
                                    return;
                                }
                                else if (D.GetDecimal(dataRow["AM_EX_ADPAY"]) > 0)
                                    flag = true;

                                #region 전표승인
                                if (Global.MainFrame.LoginInfo.CompanyCode == "W100")
                                    continue;

                                if (dataRow["CD_PARTNER"].ToString() == "20505" || dataRow["CD_PARTNER"].ToString() == "08488")
                                    continue;

                                if (Global.MainFrame.LoginInfo.CompanyCode == "K100" && dataRow["FG_TRANS"].ToString() != "001")
                                    continue;

                                string query = string.Format(@"SELECT FD.NO_DOCU,
       FD.CD_PC
FROM PU_IVH IH WITH(NOLOCK)
LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
				  FD.NO_DOCU,
				  FD.CD_PC
           FROM FI_DOCU FD WITH(NOLOCK)
		   GROUP BY FD.CD_COMPANY, FD.NO_MDOCU, 
				    FD.NO_DOCU,
					FD.CD_PC) FD
ON FD.CD_COMPANY = IH.CD_COMPANY AND FD.NO_MDOCU = IH.NO_IV
WHERE IH.CD_COMPANY = '{0}'
AND IH.NO_IV = '{1}'", Global.MainFrame.LoginInfo.CompanyCode, dataRow["NO_IV"].ToString());

                                DataTable dt = DBHelper.GetDataTable(query);

                                string 전표번호 = dt.Rows[0]["NO_DOCU"].ToString();
                                string 회계단위 = dt.Rows[0]["CD_PC"].ToString();

                                object[] obj = new object[1];
                                DBHelper.ExecuteNonQuery("UP_FI_DOCU_CREATE_SEQ4", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                  회계단위,
                                                                                                  "FI04",
                                                                                                  dataRow["DT_PROCESS"].ToString() }, out obj);

                                decimal 회계번호 = D.GetDecimal(obj[0]);

                                DBHelper.ExecuteNonQuery("UP_FI_DOCUAPP_UPDATE", new object[] { 전표번호,
                                                                                                회계단위,
                                                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                dataRow["DT_PROCESS"].ToString(),
                                                                                                회계번호,
                                                                                                "2",
                                                                                                Global.MainFrame.LoginInfo.UserID,
                                                                                                Global.MainFrame.LoginInfo.UserID });
                                #endregion
                            }

							if (flag)
                            {
                                this.ShowMessage("전표가 처리되었습니다.\n선지급 정리 대상 건이 포함되어 있기 때문에 정리 화면으로 자동 링크 됩니다.");
                                this.btn선지급정리_Click(null, null);
                            }
                            else
                                this.ShowMessage("전표가 처리되었습니다.");

                            this.OnToolBarSearchButtonClicked(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전표처리취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray1 == null || dataRowArray1.Length <= 0)
                {
                    this.ShowMessage("IK1_007");
                    return;
                }
                else
                {
                    DataRow[] dataRowArray2 = this._flexH.DataTable.Select("S = 'Y' AND TP_AIS ='N'");
                    if (dataRowArray2 != null && dataRowArray2.Length > 0)
                    {
                        this.ShowMessage("이미 전표취소된 건이 포함되어 있습니다");
                        return;
                    }
                    else
                    {
                        DataTable dataTable = this._flexH.DataTable.Clone();
                        for (int index = 0; index < dataRowArray1.Length; ++index)
                            dataTable.ImportRow(dataRowArray1[index]);

                        bool flag = false;

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            flag = this._biz.미결전표취소(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         "210",
                                                                         D.GetString(dataRow["NO_IV"]),
                                                                         Global.MainFrame.LoginInfo.UserID });
                        }

                        if (flag)
                            this.ShowMessage("전표가 취소되었습니다");

                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn메일전송_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                string Multikey = "";

                DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in this._flexH.DataTable.Select("S = 'Y'"))
                    {
                        Multikey = Multikey + dr["NO_IV"].ToString() + "|";
                    }

                    DataSet dataSet = this._biz.Print(this.MainFrameInterface.LoginInfo.CompanyCode, Multikey);
                    ReportHelper reportHelper = new ReportHelper("R_PU_IV_MNG_0", "매입관리");
                    reportHelper.가로출력();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic["처리일자"] = this.dtp처리일자.StartDateToString.Substring(0, 4) + "/" + this.dtp처리일자.StartDateToString.Substring(4, 2) + "/" + this.dtp처리일자.StartDateToString.Substring(6, 2) + " ~ " + this.dtp처리일자.EndDateToString.Substring(0, 4) + "/" + this.dtp처리일자.EndDateToString.Substring(4, 2) + "/" + this.dtp처리일자.EndDateToString.Substring(6, 2);
                    dic["담당부서코드"] = this.ctx담당자.CodeName;
                    dic["담당부서명"] = this.ctx담당부서.CodeName;
                    dic["거래처코드"] = this.ctx매입처.CodeValue;
                    dic["거래처명"] = this.ctx매입처.CodeName;
                    dic["거래구분"] = this.cbo거래구분.Text;
                    dic["전표처리"] = this.cbo전표처리.Text;
                    dic["담당자코드"] = this.ctx담당자.CodeValue;
                    dic["담당자명"] = this.ctx담당자.CodeName.ToString();

                    foreach (string index in dic.Keys)
                        reportHelper.SetData(index, dic[index]);
                    
                    string[] strArray = null;
                    string str = string.Empty;
                    
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        if (!dictionary.ContainsKey(D.GetString(dataRow["CD_PARTNER"])))
                        {
                            dictionary[D.GetString(dataRow["CD_PARTNER"])] = D.GetString(dataRow["CD_PARTNER"]);
                            str = str + D.GetString(dataRow["CD_PARTNER"]) + "|";
                        }
                        strArray = str.Split('|');
                    }
                    
                    string pkcol_name = "NO_IV";
                    new P_MF_EMAIL_NOMULTI("R_PU_IV_MNG_0", new ReportHelper[1] { reportHelper }, dic, "매입관리", pkcol_name, dataSet.Tables[0]).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn관리구분배부_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                DataTable dtH = this._flexH.DataTable.Clone().Copy();
                DataTable dtL = this._flexL.DataTable.Clone().Copy();
                
                foreach (DataRow dataRow1 in this._flexH.DataTable.Select("NO_IV = '" + D.GetString(this._flexH["NO_IV"]) + "'"))
                {
                    foreach (DataRow dataRow2 in this._flexL.DataTable.Select("NO_IV = '" + D.GetString(this._flexH["NO_IV"]) + "'"))
                        dtL.LoadDataRow(dataRow2.ItemArray, true);
                    dtH.LoadDataRow(dataRow1.ItemArray, true);
                }
                
                if (new P_PU_IV_AD_DIST_SUB(dtH, dtL, D.GetString(this._flexH["FG_AD_YN"])).ShowDialog() == DialogResult.OK)
                    this._flexH["FG_AD_YN"] = "Y";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn관리구분배부취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                if (D.GetString(this._flexH["TP_AIS"]) == "Y")
                {
                    this.ShowMessage("이미 전표처리된 건이 포함되어 있습니다");
                }
                else if (D.GetString(this._flexH["FG_AD_YN"]) == "Y")
                {
                    this._biz.관리구분배부취소(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                            D.GetString(this._flexH["NO_IV"]) });
                    this._flexH["FG_AD_YN"] = "N";
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전자결재_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSave() || !this._flexL.HasNormalRow || !this._flexH.HasNormalRow) return;

                DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray1.Length != 1 || dataRowArray1 == null)
                {
                    this.ShowMessage(this.DD("한개의 매입건을 선택해 주십시오."));
                }
                else if (BASIC.GetMAEXC_Menu("P_PU_IV_MNG", "PU_A00000001") == "001" && D.GetString(dataRowArray1[0]["NO_DOCU"]) == string.Empty)
                {
                    this.ShowMessage(this.DD("전표처리 되지 않은 건이 있습니다."));
                }
                else
                {
                    string @string = D.GetString(dataRowArray1[0]["NO_IV"]);
                    DataRow[] dataRowArray2 = this._flexL.DataTable.Select("NO_IV = '" + @string + "'");
                    bool flag1 = true;
                    
                    string[] strArray = new string[] { this.DD("진행"),
                                                       this.DD("종결"),
                                                       this.DD("미상신"),
                                                       this.DD("취소"),
                                                       this.DD("반려") };

                    int st_stat = this._biz.GetFI_GWDOCU(@string);

                    if (st_stat != 999)
                    {
                        if (st_stat == -1)
                            st_stat = 4;
                        
                        this.ShowMessage("전자결제 @중 입니다.", strArray[st_stat]);
                        
                        if (st_stat == 4)
                            st_stat = -1;
                    }

                    P_CZ_PU_IV_MNG_GW pPuIvMngGw = new P_CZ_PU_IV_MNG_GW();
                    
                    bool flag3 = pPuIvMngGw.save_TF(st_stat);
                    string[] gwSearch = pPuIvMngGw.getGwSearch(this.전자결재사용구분, dataRowArray1, dataRowArray2);
                    
                    if (gwSearch == null || gwSearch.Length == 0)
                        return;
                    
                    if (this._flexL.Rows.Count > 0 && flag3)
                        flag1 = this._biz.전자결재_실제사용(dataRowArray1[0], gwSearch[0], gwSearch[1], gwSearch[3]);
                    
                    if (!flag1)
                        return;
                    
                    string str = "cd_company=" + this.LoginInfo.CompanyCode + "&cd_pc=" + this.LoginInfo.CdPc + "&no_docu=" + HttpUtility.UrlEncode(@string, Encoding.UTF8) + "&login_id=" + this.LoginInfo.EmployeeNo;
                    Process.Start("msedge.exe", gwSearch[2] + str);
                    this.ShowMessage("전자결재 처리가 완료 되었습니다.");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn업무공유_Click(object sender, EventArgs e)
        {
            try
            {
                if (D.GetString(this._flexL["ID_MEMO"]) == string.Empty) return;

                //(new P_WS_PM_S_JOBSHARE_SUB1(this, D.GetString(this._flexL["ID_MEMO"]), new object[] { "A07",
                //                                                                                       "",
                //                                                                                       ( Global.MainFrame).LoginInfo.CompanyCode,
                //                                                                                       D.GetString(this._flexL["CD_PJT"]),
                //                                                                                       D.GetString(this._flexL["CD_WBS"]),
                //                                                                                       D.GetString(this._flexL["NO_SHARE"]),
                //                                                                                       D.GetString(this._flexL["NO_ISSUE"]),
                //                                                                                       "04" })).ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn선지급정리_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.IsExistPage("P_CZ_PU_ADPAYMENT_BILL", false))
                    this.UnLoadPage("P_CZ_PU_ADPAYMENT_BILL", false);

                this.LoadPageFrom("P_CZ_PU_ADPAYMENT_BILL", this.DD("선지급정리"), Grant, new object[] { new object[] { D.GetString(this._flexH["CD_PARTNER"]),
                                                                                                                        D.GetString(this._flexH["LN_PARTNER"]),
                                                                                                                        D.GetString(this._flexH["FG_TRANS"]),
                                                                                                                        D.GetString(this._flexH["DT_PROCESS"]) } });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn발행옵션_Click(object sender, EventArgs e)
        {
            try
            {
                new P_CZ_PU_IVMNG_ETAX36524D_OPTION_SUB().ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.KeyData).ToString() == "Enter"))
                return;
            SendKeys.SendWait("{TAB}");
        }

        private void Btn전표출력_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray, dataRowArray1;

            try
            {
                if (!this._flexH.HasNormalRow) return;
                if (string.IsNullOrEmpty(this._flexH["NO_DOCU"].ToString()))
                {
                    this.ShowMessage("전표처리가 되어 있지 않은 건 입니다.");
                    return;
                }


                dataRowArray = this._flexH.DataTable.Select("S = 'Y'", "NO_DOCU ASC");
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    dataRowArray1 = this._flexH.DataTable.Select("ISNULL(NO_DOCU, '') = '' AND S ='Y'");
                    if (dataRowArray1 != null && dataRowArray1.Length > 0)
                    {
                        this.ShowMessage("CZ_전표 처리되지 않은 건이 포함되어 있습니다.");
                    }
                    else
                    {
                        DataTable dt = this._biz.전표출력(string.Empty);

                        foreach (DataRow dr in dataRowArray)
                        {
                            DataTable dt1 = this._biz.전표출력(dr["NO_DOCU"].ToString());
                            dt.Merge(dt1);
                        }

                        ReportHelper reportHelper = Util.SetReportHelper(string.Format("R_CZ_PU_IV_MNG_1_{0}", Global.MainFrame.LoginInfo.CompanyCode), "매입관리-전표출력", Global.MainFrame.LoginInfo.CompanyCode);

                        reportHelper.SetDataTable("FI_DOCU", dt);

                        reportHelper.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn첨부파일_Click(object sender, EventArgs e)
        {
            try
            {
                P_CZ_MA_FILE_SUB dialog = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "P_CZ_PU_IV_MNG", this._flexH["NO_IV"].ToString(), "P_CZ_PU_IV_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + this._flexH["DT_PROCESS"].ToString().Substring(0, 4));
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btnCC업데이트_Click(object sender, EventArgs e)
        {
            string query;
			try
			{
                query = @"UPDATE P
SET P.CD_CC = M.CD_CC
FROM PU_POL P
JOIN MA_PITEM M ON P.CD_COMPANY = M.CD_COMPANY AND P.CD_ITEM = M.CD_ITEM
WHERE P.CD_COMPANY = 'W100'
AND NOT EXISTS (SELECT 1
				FROM PU_IVL IV
				WHERE IV.CD_COMPANY = P.CD_COMPANY
				AND IV.NO_PO = P.NO_PO
                AND IV.NO_POLINE = P.NO_LINE)

UPDATE Q
SET Q.CD_GROUP = M.CD_CC
FROM MM_QTIO Q
JOIN MA_PITEM M ON Q.CD_COMPANY = M.CD_COMPANY AND Q.CD_ITEM = M.CD_ITEM
WHERE Q.CD_COMPANY = 'W100'
AND NOT EXISTS (SELECT 1
				FROM PU_IVL IV
				WHERE IV.CD_COMPANY = Q.CD_COMPANY
				AND IV.NO_IO = Q.NO_IO
                AND IV.NO_IOLINE = Q.NO_IOLINE)
";
                DBHelper.ExecuteScalar(query);
                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btnCC업데이트.Text);
            }
            catch (Exception ex)
			{
                this.MsgEnd(ex);
			}
        }
        #endregion

        #region 그리드 이벤트
        private void Grid_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flexH.Cols[e.Col].Name == "S")
                    e.Cancel = false;
                else if (this._flexH[(int)e.Row, "TP_AIS"].ToString().Trim() == "Y")
                    e.Cancel = true;
                else
                {
                    if (!(this._flexH.Cols[(int)e.Col].Name == "TXT_USERDEF1") || this._flexH["NM_TAX"].Equals("현금영수증"))
                        return;

                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID != HelpID.P_MA_PARTNER_SUB || D.GetString(this._flexH["TP_AIS"]) != "Y")
                    return;

                e.Cancel = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Grid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!Certify.IsLive() || this._flexH.Cols[this._flexH.Col].Name != "NO_DOCU" || string.IsNullOrEmpty(D.GetString(this._flexH["NO_DOCU"])))
                    return;

                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexH["NO_DOCU"]),
                                                                                                                                 "1",
                                                                                                                                 Global.MainFrame.LoginInfo.CdPc,
                                                                                                                                 Global.MainFrame.LoginInfo.CompanyCode });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            string filter;
            DataTable dt = null;

            try
            {
                filter = "NO_IV = '" + this._flexH["NO_IV"].ToString() + "'";

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new string[] { this.LoginInfo.CompanyCode, 
                                                               this._flexH["NO_IV"].ToString(),
                                                               this.txt발주번호.Text,
                                                               this.txt프로젝트번호.Text });
                }

                this._flexL.BindingAdd(dt, filter);

                if (D.GetString(this._flexH["TP_AIS"]) == "Y")
                {
                    this.btn전표처리취소.Enabled = true;
                    this.btn미결전표처리.Enabled = false;
                }
                else
                {
                    this.btn전표처리취소.Enabled = false;
                    this.btn미결전표처리.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;

            try
            {
                grid = ((FlexGrid)sender);
                
                Decimal decimal1 = D.GetDecimal(grid.GetData(e.Row, e.Col).ToString());
                Decimal decimal2 = D.GetDecimal(grid.EditData);
                Decimal num1 = decimal1 + 99;
                Decimal num2 = decimal1 - 99;
                Math.Abs(decimal1 - decimal2);
                CommonFunction commonFunction = new CommonFunction();

                if (!this._flexH.AllowEditing || this._flexH.GetData(e.Row, e.Col).ToString() == this._flexH.EditData)
                    return;

                switch (this._flexH.Cols[e.Col].Name)
                {
                    case "FG_PAYBILL":
                        string str1 = string.Empty;
                        string str2 = string.Empty;

                        if (this.지급관리통제설정 == "Y")
                        {
                            Decimal decimal3 = D.GetDecimal(this._flexH["AM_K"]);
                            str2 = ComFunc.만기예정일자(this.dtp처리일자.StartDateToString, decimal3, D.GetString(this._flexH.EditData), "1");

                            if (str2 != string.Empty)
                            {
                                this._flexH["DT_PAY_PREARRANGED"] = str2;
                                this._flexH["DT_DUE"] = str2;
                            }

                            str1 = ComFunc.만기예정일자(this.dtp처리일자.StartDateToString, decimal3, D.GetString(this._flexH.EditData), "2");
                            if (str1 != string.Empty)
                                this._flexH["DT_DUE"] = str1;
                        }

                        if (this.지급관리통제설정 == "N" || str2 == string.Empty || str1 == string.Empty)
                        {
                            string str3 = commonFunction.DateAdd(this.dtp처리일자.StartDateToString, "D", Convert.ToInt16(this._flexH["DT_PAY_PREARRANGED"].ToString()));
                            if (str2 == string.Empty)
                                this._flexH["DT_PAY_PREARRANGED"] = str3;
                            if (str1 == string.Empty)
                                this._flexH["DT_DUE"] = this._flexH["DT_PAY_PREARRANGED"];
                            break;
                        }
                        else
                            break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row < this._flexH.Rows.Fixed || e.Col < this._flexH.Cols.Fixed)
                return;

            CellStyle style = this._flexH.Rows[e.Row].Style;
            
            if (style == null)
            {
                if (D.GetString(this._flexH[e.Row, "TP_AIS"]) == "Y")
                    this._flexH.Rows[e.Row].Style = this._flexH.Styles["YELLOW"];
                else
                    this._flexH.Rows[e.Row].Style = this._flexH.Styles["WHITE"];
            }
            else if (style.Name == "YELLOW")
            {
                if (!(D.GetString(this._flexH[e.Row, "TP_AIS"]) != "Y"))
                    return;
                
                this._flexH.Rows[e.Row].Style = this._flexH.Styles["WHITE"];
            }
            else
            {
                if (style.Name != "WHITE" || D.GetString(this._flexH[e.Row, "TP_AIS"]) != "Y")
                    return;
                this._flexH.Rows[e.Row].Style = this._flexH.Styles["YELLOW"];
            }
        }

        private void _flexL_CellContentChanged(object sender, CellContentEventArgs e)
        {
            try
            {
                this._biz.SaveContent(e.ContentType, e.CommandType, D.GetString(this._flexL[e.Row, "NO_IV"]), D.GetDecimal(this._flexL[e.Row, "NO_LINE"]), e.SettingValue);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private bool FieldCheck()
        {
            if (this.dtp처리일자.StartDateToString == "")
            {
                this.ShowMessage("WK1_004", this.lbl처리일자.Text);
                this.dtp처리일자.Focus();
                return false;
            }
            else if (this.dtp처리일자.EndDateToString == "")
            {
                this.ShowMessage("WK1_004", this.lbl처리일자.Text);
                this.dtp처리일자.Focus();
                return false;
            }

            return true;
        }
        #endregion
    }
}

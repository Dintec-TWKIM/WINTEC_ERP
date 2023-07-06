using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.Windows.Print;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_PU_ADPAYMENT_MNG : PageBase
    {
        private P_CZ_PU_ADPAYMENT_MNG_BIZ _biz = new P_CZ_PU_ADPAYMENT_MNG_BIZ();
        private P_CZ_PU_ADPAYMENT_MNG_GW _gw = new P_CZ_PU_ADPAYMENT_MNG_GW();

        #region 생성자 & 초기화
        public P_CZ_PU_ADPAYMENT_MNG()
        {
            StartUp.Certify(this);
            InitializeComponent();

            this.MainGrids = new FlexGrid[] { this._flexH };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
        }

        public P_CZ_PU_ADPAYMENT_MNG(string 발주번호)
        {
			StartUp.Certify(this);
			InitializeComponent();

            this.txt발주번호.Text = 발주번호;
            this.MainGrids = new FlexGrid[] { this._flexH };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
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

            this.InitControl();
        }

        private void InitControl()
        {
            try
            {
                DataSet comboData = Global.MainFrame.GetComboData(new string[] { "N;PU_C000016",
                                                                                 "N;MA_B000095",
                                                                                 "N;PU_C000078" });

                this.cbo거래구분.DataSource = comboData.Tables[0];
                this.cbo거래구분.DisplayMember = "NAME";
                this.cbo거래구분.ValueMember = "CODE";

                this.cbo전표처리.DataSource = MA.GetCodeUser(new string[] { "N", "Y" }, new string[] { "N", "Y" });
                this.cbo전표처리.DisplayMember = "NAME";
                this.cbo전표처리.ValueMember = "CODE";

                this.cbo반제여부.DataSource = MA.GetCodeUser(new string[] { "000", "001" }, new string[] { "반제", "미반제" }, true);
                this.cbo반제여부.DisplayMember = "NAME";
                this.cbo반제여부.ValueMember = "CODE";

                this.dtp회계일자.Text = Global.MainFrame.GetStringToday;
                
                this.dtp발주일.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
                this.dtp발주일.EndDateToString = Global.MainFrame.GetStringToday;

                this._flexH.SetDataMap("YN_JEONJA", comboData.Tables[1], "CODE", "NAME");
                this._flexH.SetDataMap("PO_CONDITION", comboData.Tables[2], "CODE", "NAME");

                this.btn전자결재.Enabled = false;
                this.btn회계전표취소.Enabled = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(2, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NM_GW_STATUS", "결재상태", 60, false);
            this._flexH.SetCol("NO_ADPAY", "선지급번호", 100, false);
            this._flexH.SetCol("NO_PO", "발주번호", 100, false);
			this._flexH.SetCol("NM_KOR", "담당자", 80, false);
			this._flexH.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_ADPAY", "작성일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_ACCT", "회계일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_SUPPLIER", "매입처", 100, 20, false);
            this._flexH.SetCol("NM_FG_TRANS", "거래구분", 80, 20, false);
            this._flexH.SetCol("NM_TPPO", "발주유형", 80, false);
            this._flexH.SetCol("NM_EXCH", "통화명", 60, false);
            this._flexH.SetCol("RT_EXCH", "환율", 80, 17, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);

            #region 발주
            this._flexH.SetCol("AM_EX_PO", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PO", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("RT_VAT", "부가세율", false);
            this._flexH.SetCol("VAT_EX", "외화부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("VAT", "원화부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_PO_TOTAL", "총외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PO_TOTAL", "총원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH[0, this._flexH.Cols["AM_EX_PO"].Index] = this.DD("발주");
            this._flexH[0, this._flexH.Cols["AM_PO"].Index] = this.DD("발주");
            this._flexH[0, this._flexH.Cols["RT_VAT"].Index] = this.DD("발주");
            this._flexH[0, this._flexH.Cols["VAT_EX"].Index] = this.DD("발주");
            this._flexH[0, this._flexH.Cols["VAT"].Index] = this.DD("발주");
            this._flexH[0, this._flexH.Cols["AM_EX_PO_TOTAL"].Index] = this.DD("발주");
            this._flexH[0, this._flexH.Cols["AM_PO_TOTAL"].Index] = this.DD("발주");
            #endregion

            #region 선지급
            this._flexH.SetCol("AM_EX_ADPAY", "누적외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_ADPAY", "누적원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_REMAIN", "잔여외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_REMAIN", "잔여원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_PRE", "요청외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PRE", "요청원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH[0, this._flexH.Cols["AM_EX_ADPAY"].Index] = this.DD("선지급");
            this._flexH[0, this._flexH.Cols["AM_ADPAY"].Index] = this.DD("선지급");
            this._flexH[0, this._flexH.Cols["AM_EX_REMAIN"].Index] = this.DD("선지급");
            this._flexH[0, this._flexH.Cols["AM_REMAIN"].Index] = this.DD("선지급");
            this._flexH[0, this._flexH.Cols["AM_EX_PRE"].Index] = this.DD("선지급");
            this._flexH[0, this._flexH.Cols["AM_PRE"].Index] = this.DD("선지급");
            #endregion

            #region 선지급전표
            this._flexH.SetCol("NO_DOCU", "전표번호", 100, false);
            this._flexH.SetCol("AM_EX_DOCU", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_DOCU", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH[0, this._flexH.Cols["NO_DOCU"].Index] = this.DD("선지급전표");
            this._flexH[0, this._flexH.Cols["AM_EX_DOCU"].Index] = this.DD("선지급전표");
            this._flexH[0, this._flexH.Cols["AM_DOCU"].Index] = this.DD("선지급전표");
            #endregion

            #region 선지급반제전표
            this._flexH.SetCol("AM_EX_BAN", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_BAN", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH[0, this._flexH.Cols["AM_EX_BAN"].Index] = this.DD("선지급반제전표");
            this._flexH[0, this._flexH.Cols["AM_BAN"].Index] = this.DD("선지급반제전표");
            #endregion

            #region 매입
            this._flexH.SetCol("AM_EX_IV", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_IV", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH[0, this._flexH.Cols["AM_EX_IV"].Index] = this.DD("매입");
            this._flexH[0, this._flexH.Cols["AM_IV"].Index] = this.DD("매입");
            #endregion

            #region 매입전표
            this._flexH.SetCol("AM_DOCU_EX_IV", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_DOCU_IV", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH[0, this._flexH.Cols["AM_DOCU_EX_IV"].Index] = this.DD("매입전표");
            this._flexH[0, this._flexH.Cols["AM_DOCU_IV"].Index] = this.DD("매입전표");
            #endregion

            #region 매입반제전표
            this._flexH.SetCol("AM_BAN_EX_IV", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_BAN_IV", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexH[0, this._flexH.Cols["AM_BAN_EX_IV"].Index] = this.DD("매입반제전표");
            this._flexH[0, this._flexH.Cols["AM_BAN_IV"].Index] = this.DD("매입반제전표");
            #endregion

            this._flexH.SetCol("YN_JEONJA", "계산서발행형태", 140, false);
            this._flexH.SetCol("DT_PAY_SCHEDULE", "자금예정일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("PO_CONDITION", "구매거래조건", 140, false);
            this._flexH.SetCol("QT_IN", "입고수량", false);
            this._flexH.SetCol("APP_DOC_ID", "문서번호", false);
            this._flexH.SetCol("TXT_USERDEF1", "선지급비고", 100, true);
            this._flexH.SetCol("YN_FILE", "파일첨부여부", 100);
            this._flexH.SetCol("DC_RESULT", "자동처리결과", 100);

            this._flexH.SettingVersion = "0.0.0.2";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.Cols.Frozen = 4;
            this._flexH.SetExceptSumCol("RT_EXCH");
            this._flexH.SetDummyColumn(new string[] { "S" });
            this._flexH.VerifyNotNull = new string[] { "DT_ACCT" };
            #endregion

            #region Line
            this._flexL.BeginSetting(2, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            this._flexL.SetCol("NM_ITEM", "품목명", 140, false);
            this._flexL.SetCol("STND_ITEM", "규격", 100, 20, false);
            this._flexL.SetCol("UNIT_IM", "단위", 140, false);

            #region 수량
            this._flexL.SetCol("QT_PO", "발주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_ADPAY", "누적수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_REMAIN", "잔여수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_PRE", "요청수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexL[0, this._flexL.Cols["QT_PO"].Index] = this.DD("수량");
            this._flexL[0, this._flexL.Cols["QT_ADPAY"].Index] = this.DD("수량");
            this._flexL[0, this._flexL.Cols["QT_REMAIN"].Index] = this.DD("수량");
            this._flexL[0, this._flexL.Cols["QT_PRE"].Index] = this.DD("수량");

            this._flexL.Cols["QT_PO"].Format = "###,##0.##";
            this._flexL.Cols["QT_ADPAY"].Format = "###,##0.##";
            this._flexL.Cols["QT_REMAIN"].Format = "###,##0.##";
            this._flexL.Cols["QT_PRE"].Format = "###,##0.##";
            #endregion

            #region 외화금액
            this._flexL.SetCol("AM_EX", "발주외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_ADPAY", "누적외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_REMAIN", "잔여외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_PRE", "요청외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexL[0, this._flexL.Cols["AM_EX"].Index] = this.DD("외화금액");
            this._flexL[0, this._flexL.Cols["AM_EX_ADPAY"].Index] = this.DD("외화금액");
            this._flexL[0, this._flexL.Cols["AM_EX_REMAIN"].Index] = this.DD("외화금액");
            this._flexL[0, this._flexL.Cols["AM_EX_PRE"].Index] = this.DD("외화금액");
            #endregion

            #region 원화금액
            this._flexL.SetCol("AM", "발주원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_ADPAY", "누적원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_REMAIN", "잔여원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_PRE", "요청원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexL[0, this._flexL.Cols["AM"].Index] = this.DD("원화금액");
            this._flexL[0, this._flexL.Cols["AM_ADPAY"].Index] = this.DD("원화금액");
            this._flexL[0, this._flexL.Cols["AM_REMAIN"].Index] = this.DD("원화금액");
            this._flexL[0, this._flexL.Cols["AM_PRE"].Index] = this.DD("원화금액");
            #endregion

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexL.SetDummyColumn(new string[] { "S" });
            #endregion
        }

        private void InitEvent()
        {
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.btn전자결재.Click += new EventHandler(this.btn전자결재_Click);
            this.btn회계전표처리.Click += new EventHandler(this.btn회계전표처리_Click);
            this.btn회계전표취소.Click += new EventHandler(this.btn회계전표취소_Click);

            this.ctx발주유형.QueryBefore += new BpQueryHandler(this.ctx발주유형_QueryBefore);
            
            this.btn회계일자.Click += new EventHandler(this.btn회계일자_Click);
            this.btn선지급금액비율.Click += new EventHandler(this.btn선지급금액비율_Click);
            this.btn선지급금액적용.Click += new EventHandler(this.btn선지급금액적용_Click);
			this.btn자동.Click += Btn자동_Click;
			this.btn첨부파일.Click += Btn첨부파일_Click;

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);
            this._flexH.MouseDoubleClick += new MouseEventHandler(this._flexH_MouseDoubleClick);

            //this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
            this._flexL.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
        }

        private void Btn첨부파일_Click(object sender, EventArgs e)
        {
            DataTable dt;
            string query;

            try
            {
                P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "MA", "P_CZ_PU_ADPAYMENT_MNG", D.GetString(this._flexH["NO_PO"]), "P_CZ_PU_ADPAYMENT_MNG" + "/" + Global.MainFrame.LoginInfo.CompanyCode);
                dlg.ShowDialog(this);

                query = @"SELECT 1 
						  FROM MA_FILEINFO MF
						  WHERE MF.CD_COMPANY = '{0}'
						  AND MF.CD_MODULE = 'MA'
						  AND MF.ID_MENU = 'P_CZ_PU_ADPAYMENT_MNG'
						  AND MF.CD_FILE = '{1}'";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, D.GetString(this._flexH["NO_PO"])));

                if (dt != null && dt.Rows.Count > 0)
                    this._flexH["YN_FILE"] = "Y";
                else
                    this._flexH["YN_FILE"] = "N";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn자동_Click(object sender, EventArgs e)
        {
            DataTable dt;
            DataRow[] dataRowArray, dataRowArray1;
            string query;

            try
            {
                if (!this._flexH.HasNormalRow || !this.Verify_Grid(this._flexH)) return;

                this.btn회계전표처리.Enabled = false;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray.Length == 0 || dataRowArray == null)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    int index = 0;
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        dataRow["DC_RESULT"] = string.Empty;

                        if (string.IsNullOrEmpty(dataRow["DT_ACCT"].ToString()))
						{
                            dataRow["DC_RESULT"] = "회계일자가 지정되어 있지 않습니다.";
                            this.ShowMessage("오류 발생 ! 자동처리결과 컬럼의 오류 내용을 확인하세요.");
                            return;
						}

                        if (dataRow["YN_FILE"].ToString() != "Y")
						{
                            dataRow["DC_RESULT"] = "첨부파일이 등록되지 않은 건이 선택되어 있습니다.";
                            this.ShowMessage("오류 발생 ! 자동처리결과 컬럼의 오류 내용을 확인하세요.");
                            return;
                        }

                        if (!string.IsNullOrEmpty(dataRow["NO_DOCU"].ToString()))
                        {
                            dataRow["DC_RESULT"] = "이미 전표처리 된 건이 선택되어 있습니다.";
                            this.ShowMessage("오류 발생 ! 자동처리결과 컬럼의 오류 내용을 확인하세요.");
                            return;
                        }

                        if (string.IsNullOrEmpty(dataRow["TXT_USERDEF1"].ToString()))
						{
                            query = @"SELECT DATEDIFF(MONTH, MAX(IH.DT_PROCESS), GETDATE()) AS QT_MONTH
FROM PU_IVH IH WITH(NOLOCK)
WHERE IH.CD_COMPANY = '{0}'
AND IH.CD_PARTNER = '{1}'";

                            dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode, dataRow["CD_PARTNER"].ToString() }));

                            if (dt == null || dt.Rows.Count == 0)
                            {
                                dataRow["DC_RESULT"] = "매입 이력이 없습니다.\n사업자등록번호 및 계좌정보 재확인 및 결과 리마크하여 주세요";
                                this.ShowMessage("오류 발생 ! 자동처리결과 컬럼의 오류 내용을 확인하세요.");
                                return;
                            }
                            else if (D.GetInt(dt.Rows[0]["QT_MONTH"]) >= 6)
                            {
                                dataRow["DC_RESULT"] = "최근 6개월 매입 이력이 없습니다.\n사업자등록번호 및 계좌정보 재확인 및 결과 리마크하여 주세요";
                                this.ShowMessage("오류 발생 ! 자동처리결과 컬럼의 오류 내용을 확인하세요.");
                                return;
                            }
                        }

                        #region 전표처리
                        dataRowArray1 = this._flexL.DataTable.Select("NO_PO = '" + D.GetString(dataRow["NO_PO"]) + "' AND QT_PRE > 0");

                        if (dataRowArray1 == null || dataRowArray1.Length == 0)
						{
                            dataRow["DC_RESULT"] = "선지급금액이 없습니다.";
                            this.ShowMessage("오류 발생 ! 자동처리결과 컬럼의 오류 내용을 확인하세요.");
                            return;
						}

                        DataTable dtL = this._flexL.DataTable.Clone();

                        decimal num2 = 0;
                        string str = (string)this.GetSeq(this.LoginInfo.CompanyCode, "CZ", "02", Global.MainFrame.GetStringToday.Substring(0, 6));

                        dataRow["NO_ADPAY"] = str;

                        foreach (DataRow row in dataRowArray1)
                        {
                            row["NO_ADPAY"] = dataRow["NO_ADPAY"];
                            row["NO_ADPAYLINE"] = ++num2;
                            row["DT_ADPAY"] = Global.MainFrame.GetStringToday;
                            row["CD_EXCH"] = dataRow["CD_EXCH"];
                            row["RT_EXCH"] = dataRow["RT_EXCH"];
                            row["YN_JEONJA"] = D.GetString(dataRow["YN_JEONJA"]);
                            row["TP_AIS"] = "Y";
                            row["DT_PAY_SCHEDULE"] = D.GetString(dataRow["DT_PAY_SCHEDULE"]);
                            row["PO_CONDITION"] = D.GetString(dataRow["PO_CONDITION"]);
                            row["DT_ACCT"] = D.GetString(dataRow["DT_ACCT"]);

                            dtL.ImportRow(row);
                        }

                        DataTable dtF = new DataTable();
                        dtF.Columns.Add("NO_PO");
                        dtF.Columns.Add("NO_ADPAY");
                        dtF.Columns.Add("NO_MODULE");
                        dtF.Columns.Add("TXT_USERDEF1");

                        dtF.Rows.Add(new object[] { dataRow["NO_PO"], dataRow["NO_ADPAY"], dataRow["NO_MODULE"], dataRow["TXT_USERDEF1"] });

                        bool flag = this._biz.Save(dtL, dtF);

                        query = @"SELECT PA.DT_ADPAY, 
       FD.NO_DOCU,
	   FD.CD_PC
FROM PU_ADPAYMENT PA WITH(NOLOCK)
LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU, FD.NO_DOCU, FD.CD_PC
		   FROM FI_DOCU FD WITH(NOLOCK)
		   GROUP BY FD.CD_COMPANY, FD.NO_MDOCU, FD.NO_DOCU, FD.CD_PC) FD 
ON FD.CD_COMPANY = PA.CD_COMPANY AND FD.NO_MDOCU = PA.NO_ADPAY
WHERE PA.CD_COMPANY = '{0}'
AND PA.NO_ADPAY = '{1}'";

                        dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dataRow["NO_ADPAY"].ToString()));

                        dataRow["NO_DOCU"] = dt.Rows[0]["NO_DOCU"].ToString();
                        dataRow["CD_PC"] = dt.Rows[0]["CD_PC"].ToString();
                        dataRow["DT_ADPAY"] = dt.Rows[0]["DT_ADPAY"].ToString();
                        #endregion

                        #region 전표승인
                        if (flag == true && (Global.MainFrame.LoginInfo.StDocuApp == "2" || Global.MainFrame.LoginInfo.StDocuApp == "3"))
                        {
                            object[] obj = new object[1];
                            DBHelper.ExecuteNonQuery("UP_FI_DOCU_CREATE_SEQ4", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                              dataRow["CD_PC"].ToString(),
                                                                                              "FI04",
                                                                                              dataRow["DT_ACCT"].ToString() }, out obj);

                            decimal 회계번호 = D.GetDecimal(obj[0]);

                            DBHelper.ExecuteNonQuery("UP_FI_DOCUAPP_UPDATE", new object[] { dataRow["NO_DOCU"].ToString(),
                                                                                            dataRow["CD_PC"].ToString(),
                                                                                            Global.MainFrame.LoginInfo.CompanyCode,
                                                                                            dataRow["DT_ACCT"].ToString(),
                                                                                            회계번호,
                                                                                            "2",
                                                                                            Global.MainFrame.LoginInfo.UserID,
                                                                                            Global.MainFrame.LoginInfo.UserID });
                        }
                        #endregion

                        #region 전자결재
                        if (string.IsNullOrEmpty(dataRow["NO_DOCU"].ToString()))
                        {
                            dataRow["DC_RESULT"] = "전표처리가 되지 않았습니다.";
                            this.ShowMessage("오류 발생 ! 자동처리결과 컬럼의 오류 내용을 확인하세요.");
                            return;
                        }

                        dataRow["AM_EX_ADPAY"] = D.GetDecimal(dataRow["AM_EX_ADPAY"]) + D.GetDecimal(dataRow["AM_EX_PRE"]);
                        dataRow["AM_ADPAY"] = D.GetDecimal(dataRow["AM_ADPAY"]) + D.GetDecimal(dataRow["AM_PRE"]);
                        dataRow["AM_EX_REMAIN"] = D.GetDecimal(dataRow["AM_EX_REMAIN"]) - D.GetDecimal(dataRow["AM_EX_PRE"]);
                        dataRow["AM_REMAIN"] = D.GetDecimal(dataRow["AM_REMAIN"]) - D.GetDecimal(dataRow["AM_PRE"]);

                        this._gw.전자결재_자동(dataRow);
                        #endregion

                        dataRow["DC_RESULT"] = "정상 처리 되었습니다.";
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn자동.Text);
                }
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
        #endregion

        #region Main 버튼 Event
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      Global.MainFrame.LoginInfo.Language,
                                                                      this.txt발주번호.Text,
                                                                      this.dtp발주일.StartDateToString,
                                                                      this.dtp발주일.EndDateToString,
                                                                      D.GetString(this.ctx구매그룹.CodeValue),
                                                                      D.GetString(this.cbo거래구분.SelectedValue),
                                                                      D.GetString(this.ctx매입처.CodeValue),
                                                                      D.GetString(this.ctx담당자.CodeValue),
                                                                      D.GetString(this.cbo전표처리.SelectedValue),
                                                                      D.GetString(this.ctx발주유형.CodeValue),
                                                                      this.cbo반제여부.SelectedValue });
                if (!this._flexH.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }

                if (D.GetString(this.cbo전표처리.SelectedValue) == "N")
                {
                    this.btn전자결재.Enabled = false;
                    this.btn회계전표처리.Enabled = true;
                    this.btn회계전표취소.Enabled = false;
                    this.btn자동.Enabled = true;
                    this.btn첨부파일.Enabled = true;
                }
                else
                {
                    this.btn전자결재.Enabled = true;
                    this.btn회계전표처리.Enabled = false;
                    this.btn회계전표취소.Enabled = true;
                    this.btn자동.Enabled = false;
                    this.btn첨부파일.Enabled = false;
                }

                this._flexH_AfterRowChange(null, null);
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
                base.OnToolBarPrintButtonClicked(sender, e);

                if (!this._flexH.HasNormalRow) return;

                this._gw.미리보기(this._flexH.GetDataRow(this._flexH.Row));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region Grid Event
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt = null;
            string key, filter;

            if (D.GetString(this.cbo전표처리.SelectedValue) == "Y")
            {
                if (D.GetDecimal(this._flexH["QT_IN"]) > 0)
                    this.btn회계전표취소.Enabled = false;
                else
                    this.btn회계전표취소.Enabled = true;
            }

            if (D.GetString(this.cbo전표처리.SelectedValue) == "N")
            {
                key = D.GetString(this._flexH["NO_PO"]);
                filter = "NO_PO ='" + key + "'";
            }
            else
            {
                key = D.GetString(this._flexH["NO_ADPAY"]);
                filter = "NO_ADPAY ='" + key + "'";
            }

            if (this._flexH.DetailQueryNeed)
            {
                dt = this._biz.DetailSearch(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                           D.GetString(this.cbo전표처리.SelectedValue),
                                                           key });
            }

            this._flexL.BindingAdd(dt, filter);
        }

        private void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flexH["S"].ToString() == "Y") //클릭하는 순간은 N이므로
                {
                    for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        this._flexL.SetCellCheck(i, this._flexL.Cols["S"].Index, CheckEnum.Checked);
                }
                else
                {
                    for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        this._flexL.SetCellCheck(i, this._flexL.Cols["S"].Index, CheckEnum.Unchecked);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexH_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this._flexH.HasNormalRow == false) return;
            if (this._flexH.MouseRow < this._flexH.Rows.Fixed) return;
            if (this._flexH.ColSel != this._flexH.Cols["NO_DOCU"].Index) return;
            if (string.IsNullOrEmpty(this._flexH["NO_DOCU"].ToString())) return;

            this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexH["NO_DOCU"]),
                                                                                                                             "1",
                                                                                                                             D.GetString(this._flexH["CD_PC"]),
                                                                                                                             Global.MainFrame.LoginInfo.CompanyCode });
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                if (flex.Name == this._flexH.Name)
                {
                    if (!this._flexH.HasNormalRow) return;

                    //데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
                    this._flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                    if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

                    for (int h = 0; h < this._flexH.Rows.Count - 1; h++)
                    {
                        this._flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                        for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                        {
                            if (this._flexL.RowState(i) == DataRowState.Deleted) continue;

                            this._flexL[i, "S"] = D.GetString(this._flexH["S"]);
                        }
                    }
                }
                else
                {
                    if (!this._flexL.HasNormalRow) return;

                    this._flexH["S"] = D.GetString(this._flexL["S"]);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (this._flexL.GetData(e.Row, e.Col).ToString() != this._flexL.EditData)
                {
                    switch (this._flexL.Cols[e.Col].Name)
                    {
                        case "S":
                            bool flag = false;
                            for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.DataView.Count + this._flexL.Rows.Fixed; ++@fixed)
                            {
                                if (this._flexL.EditData != "Y" || e.Row != @fixed && this._flexL[@fixed, "S"].Equals("N"))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                this._flexH["S"] = "Y";
                                break;
                            }
                            this._flexH["S"] = "N";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 Event
        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetToolBarButtonState(true, false, false, false, true);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn전자결재_Click(object sender, EventArgs e)
        {
            DataRow dr;

            try
            {
                dr = this._flexH.GetDataRow(this._flexH.Row);

                if (string.IsNullOrEmpty(dr["NO_DOCU"].ToString()))
                {
                    this.ShowMessage("전표처리가 되지 않은 건이 선택 되었습니다.");
                    return;
                }
                
                if (this._gw.전자결재(dr))
                {
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn전자결재.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn회계전표처리_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray, dataRowArray1;

            try
            {
                if (!this._flexH.HasNormalRow || !this.Verify_Grid(this._flexH)) return;

                this.btn회계전표처리.Enabled = false;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray.Length == 0 || dataRowArray == null)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataTable dtL = this._flexL.DataTable.Clone();

                    foreach (DataRow dataRow in dataRowArray)
                    {
                        decimal num2 = 0;
                        string str = (string)this.GetSeq(this.LoginInfo.CompanyCode, "CZ", "02", Global.MainFrame.GetStringToday.Substring(0, 6));

                        dataRow["NO_ADPAY"] = str;

                        dataRowArray1 = this._flexL.DataTable.Select("NO_PO = '" + D.GetString(dataRow["NO_PO"]) + "'");
                        foreach (DataRow row in dataRowArray1)
                        {
                            if (D.GetDecimal(row["QT_PRE"]) > 0)
                            {
                                row["NO_ADPAY"] = dataRow["NO_ADPAY"];
                                row["NO_ADPAYLINE"] = ++num2;
                                row["DT_ADPAY"] = Global.MainFrame.GetStringToday;
                                row["CD_EXCH"] = dataRow["CD_EXCH"];
                                row["RT_EXCH"] = dataRow["RT_EXCH"];
                                row["YN_JEONJA"] = D.GetString(dataRow["YN_JEONJA"]);
                                row["TP_AIS"] = "Y";
                                row["DT_PAY_SCHEDULE"] = D.GetString(dataRow["DT_PAY_SCHEDULE"]);
                                row["PO_CONDITION"] = D.GetString(dataRow["PO_CONDITION"]);
                                row["DT_ACCT"] = D.GetString(dataRow["DT_ACCT"]);

                                dtL.ImportRow(row);
                            }
                        }
                    }

                    bool flag = false;

                    if (dtL.Rows.Count != 0 && dtL != null)
                    {
                        DataTable dtF = new DataView(this._flexH.DataTable, "S = 'Y'", string.Empty, DataViewRowState.CurrentRows).ToTable(false, "NO_PO", "NO_ADPAY", "NO_MODULE", "TXT_USERDEF1");
                        flag = this._biz.Save(dtL, dtF);

                        #region 전표승인
                        if (flag == true && (Global.MainFrame.LoginInfo.StDocuApp == "2" || Global.MainFrame.LoginInfo.StDocuApp == "3"))
                        {
                            string query = @"SELECT FD.NO_DOCU,
	   FD.CD_PC
FROM PU_ADPAYMENT PA WITH(NOLOCK)
LEFT JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU, FD.NO_DOCU, FD.CD_PC
		   FROM FI_DOCU FD WITH(NOLOCK)
		   GROUP BY FD.CD_COMPANY, FD.NO_MDOCU, FD.NO_DOCU, FD.CD_PC) FD 
ON FD.CD_COMPANY = PA.CD_COMPANY AND FD.NO_MDOCU = PA.NO_ADPAY
WHERE PA.CD_COMPANY = '{0}'
AND PA.NO_ADPAY = '{1}'";

                            foreach (DataRow dr in dataRowArray)
							{
                                DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["NO_ADPAY"].ToString()));

                                object[] obj = new object[1];
                                DBHelper.ExecuteNonQuery("UP_FI_DOCU_CREATE_SEQ4", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                  dt.Rows[0]["CD_PC"].ToString(),
                                                                                                  "FI04",
                                                                                                  dr["DT_ACCT"].ToString() }, out obj);

                                decimal 회계번호 = D.GetDecimal(obj[0]);

                                DBHelper.ExecuteNonQuery("UP_FI_DOCUAPP_UPDATE", new object[] { dt.Rows[0]["NO_DOCU"].ToString(),
                                                                                                dt.Rows[0]["CD_PC"].ToString(),
                                                                                                Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                dr["DT_ACCT"].ToString(),
                                                                                                회계번호,
                                                                                                "2",
                                                                                                Global.MainFrame.LoginInfo.UserID,
                                                                                                Global.MainFrame.LoginInfo.UserID });
                            }   
                        }
                        #endregion
                    }

					if (flag)
                    {
                        this.ShowMessage("전표가 처리되었습니다");
                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn회계전표처리.Enabled = true;
            }
        }

        private void btn회계전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                this.btn회계전표취소.Enabled = false;

                DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray.Length == 0 || dataRowArray == null)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataTable dataTable = this._flexH.DataTable.Clone();
                    foreach (DataRow row in dataRowArray)
                    {
                        if (D.GetString(row["CD_GW_STATUS"]) == "0" || D.GetString(row["CD_GW_STATUS"]) == "1")
                        {
                            this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", D.GetString(row["NM_GW_STATUS"]));
                            return;
                        }

                        row["NO_EMP"] = this.LoginInfo.EmployeeNo;
                        dataTable.ImportRow(row);
                    }

                    bool flag = false;

                    if (dataTable.Rows.Count != 0 && dataTable != null)
                        flag = this._biz.Delete(dataTable.DefaultView.ToTable(1 != 0, "NO_ADPAY", "NO_EMP", "NO_MODULE"));

                    if (flag)
                    {
                        this.ShowMessage("전표가 취소되었습니다");
                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn회계전표취소.Enabled = true;
            }
        }

        private void btn회계일자_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow || D.GetString(this.cbo전표처리.SelectedValue) != "N") return;

                DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        dataRow["DT_ACCT"] = this.dtp회계일자.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn선지급금액비율_Click(object sender, EventArgs e)
        {
            decimal 선지급비율, 요청수량헤더, 요청수량라인, 요청외화금액헤더, 요청외화금액라인, 요청원화금액헤더, 요청원화금액라인, 적용수량, 적용외화금액, 적용원화금액, 대상수량, 대상외화금액, 대상원화금액;
            string filter;
            DataRow[] dataRowArray, dataRowArray1;
            DataRow dataRow;

            try
            {
                if (!this._flexL.HasNormalRow || D.GetString(this.cbo전표처리.SelectedValue) != "N") return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray.Length == 0 || dataRowArray == null)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow drH in dataRowArray)
                    {
                        filter = "NO_PO = '" + D.GetString(drH["NO_PO"]) + "' AND S = 'Y'";

                        대상수량 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(QT_REMAIN)", filter));
                        대상외화금액 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_REMAIN)", filter));
                        대상원화금액 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_REMAIN)", filter));

                        선지급비율 = (this.cur선지급금액비율.DecimalValue / 100);
                        요청수량헤더 = Decimal.Round(대상수량 * 선지급비율, 4, MidpointRounding.AwayFromZero);

                        if (D.GetString(drH["CD_EXCH"]) == "000")
                            요청외화금액헤더 = this.원화계산(대상외화금액 * 선지급비율);
                        else
                            요청외화금액헤더 = this.외화계산(대상외화금액 * 선지급비율);

                        요청원화금액헤더 = this.원화계산(대상원화금액 * 선지급비율);

                        drH["AM_EX_PRE"] = 요청외화금액헤더;
                        drH["AM_PRE"] = 요청원화금액헤더;

                        적용수량 = 0;
                        적용외화금액 = 0;
                        적용원화금액 = 0;

                        dataRowArray1 = this._flexL.DataTable.Select("NO_PO = '" + D.GetString(drH["NO_PO"]) + "' AND S = 'Y'", "AM_EX_REMAIN");

                        foreach (DataRow drD in dataRowArray1)
                        {
                            요청수량라인 = (D.GetDecimal(drD["QT_REMAIN"]) * 선지급비율);
                            
                            if (D.GetString(drH["CD_EXCH"]) == "000")
                                요청외화금액라인 = this.원화계산(D.GetDecimal(drD["AM_EX_REMAIN"]) * 선지급비율);
                            else
                                요청외화금액라인 = this.외화계산(D.GetDecimal(drD["AM_EX_REMAIN"]) * 선지급비율);

                            요청원화금액라인 = this.원화계산(D.GetDecimal(drD["AM_REMAIN"]) * 선지급비율);

                            drD["QT_PRE"] = 요청수량라인;
                            적용수량 += 요청수량라인;

                            drD["AM_EX_PRE"] = 요청외화금액라인;
                            적용외화금액 += 요청외화금액라인;

                            drD["AM_PRE"] = 요청원화금액라인;
                            적용원화금액 += 요청원화금액라인;
                        }

                        dataRow = dataRowArray1[dataRowArray1.Length - 1];

                        if (적용수량 != 요청수량헤더)
                            dataRow["QT_PRE"] = (D.GetDecimal(dataRow["QT_PRE"]) + (요청수량헤더 - 적용수량));

                        if (적용외화금액 != 요청외화금액헤더)
                            dataRow["AM_EX_PRE"] = (D.GetDecimal(dataRow["AM_EX_PRE"]) + (요청외화금액헤더 - 적용외화금액));
                        
                        if (적용원화금액 != 요청원화금액헤더)
                            dataRow["AM_PRE"] = (D.GetDecimal(dataRow["AM_PRE"]) + (요청원화금액헤더 - 적용원화금액));
                    }

                    this._flexL.SumRefresh();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn선지급금액적용_Click(object sender, EventArgs e)
        {
            decimal 선지급비율, 대상수량, 대상외화금액, 대상원화금액, 요청수량헤더, 요청수량라인, 요청외화금액헤더, 요청외화금액라인, 요청원화금액헤더, 요청원화금액라인, 적용수량, 적용외화금액, 적용원화금액;
            string filter;
            DataRow[] dataRowArray, dataRowArray1;
            DataRow dataRow;

            try
            {
                if (!this._flexL.HasNormalRow || D.GetString(this.cbo전표처리.SelectedValue) != "N") return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
                if (dataRowArray.Length == 0 || dataRowArray == null)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow drH in dataRowArray)
                    {
                        filter = "NO_PO = '" + D.GetString(drH["NO_PO"]) + "' AND S = 'Y'";

                        대상수량 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(QT_REMAIN)", filter));
                        대상외화금액 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_REMAIN)", filter));
                        대상원화금액 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_REMAIN)", filter));

                        if (this.cur선지급금액.DecimalValue > 대상원화금액)
                        {
                            this.ShowMessage(공통메세지._은_보다작거나같아야합니다, this.DD("적용금액"), this.DD("대상금액"));
                            continue;
                        }

                        선지급비율 = (this.cur선지급금액.DecimalValue / 대상원화금액);

                        요청수량헤더 = Decimal.Round(대상수량 * 선지급비율, 4, MidpointRounding.AwayFromZero);

                        if (D.GetString(drH["CD_EXCH"]) == "000")
                            요청외화금액헤더 = this.원화계산(대상외화금액 * 선지급비율);
                        else
                            요청외화금액헤더 = this.외화계산(대상외화금액 * 선지급비율);

                        요청원화금액헤더 = this.원화계산(대상원화금액 * 선지급비율);

                        drH["AM_EX_PRE"] = 요청외화금액헤더;
                        drH["AM_PRE"] = 요청원화금액헤더;

                        적용수량 = 0;
                        적용외화금액 = 0;
                        적용원화금액 = 0;

                        dataRowArray1 = this._flexL.DataTable.Select(filter, "AM_EX_REMAIN");

                        foreach (DataRow drD in dataRowArray1)
                        {
                            요청수량라인 = (D.GetDecimal(drD["QT_REMAIN"]) * 선지급비율);

                            if (D.GetString(drH["CD_EXCH"]) == "000")
                                요청외화금액라인 = this.원화계산(D.GetDecimal(drD["AM_EX_REMAIN"]) * 선지급비율);
                            else
                                요청외화금액라인 = this.외화계산(D.GetDecimal(drD["AM_EX_REMAIN"]) * 선지급비율);

                            요청원화금액라인 = this.원화계산(D.GetDecimal(drD["AM_REMAIN"]) * 선지급비율);

                            drD["QT_PRE"] = 요청수량라인;
                            적용수량 += 요청수량라인;

                            drD["AM_EX_PRE"] = 요청외화금액라인;
                            적용외화금액 += 요청외화금액라인;

                            drD["AM_PRE"] = 요청원화금액라인;
                            적용원화금액 += 요청원화금액라인;
                        }

                        dataRow = dataRowArray1[dataRowArray1.Length - 1];

                        if (적용수량 != 요청수량헤더)
                            dataRow["QT_PRE"] = (D.GetDecimal(dataRow["QT_PRE"]) + (요청수량헤더 - 적용수량));

                        if (적용외화금액 != 요청외화금액헤더)
                            dataRow["AM_EX_PRE"] = (D.GetDecimal(dataRow["AM_EX_PRE"]) + (요청외화금액헤더 - 적용외화금액));

                        if (적용원화금액 != 요청원화금액헤더)
                            dataRow["AM_PRE"] = (D.GetDecimal(dataRow["AM_PRE"]) + (요청원화금액헤더 - 적용원화금액));
                    }

                    this._flexL.SumRefresh();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void ctx발주유형_QueryBefore(object sender, BpQueryArgs e)
        {
            if (e.HelpID != HelpID.P_PU_TPPO_SUB) return;

            e.HelpParam.P61_CODE1 = "N";
        }

        private decimal 원화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                if (Global.MainFrame.LoginInfo.CompanyCode == "S100")
                    result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
                else
                    result = Decimal.Round(value, 0, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }

        private decimal 외화계산(decimal value)
        {
            decimal result = 0;

            try
            {
                result = Decimal.Round(value, 2, MidpointRounding.AwayFromZero);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }

            return result;
        }
        #endregion
    }
}

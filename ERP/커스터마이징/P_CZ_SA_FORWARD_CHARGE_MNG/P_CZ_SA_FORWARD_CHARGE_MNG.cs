using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_FORWARD_CHARGE_MNG : PageBase
    {
        P_CZ_SA_FORWARD_CHARGE_MNG_BIZ _biz = new P_CZ_SA_FORWARD_CHARGE_MNG_BIZ();

        public P_CZ_SA_FORWARD_CHARGE_MNG()
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

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

            #region Header
            this._flexH.BeginSetting(2, 1, false);

            this._flexH.SetCol("CD_PARTNER", "포워더코드", 100, true);
            this._flexH.SetCol("LN_PARTNER", "포워더명", 200);
            this._flexH.SetCol("TP_DELIVERY", "운송구분", 100, true);
            this._flexH.SetCol("DT_MONTH", "정산년월", 100, true, typeof(string), FormatTpType.YEAR_MONTH);
            
            this._flexH.SetCol("NO_DOCU", "전표번호", 100);
            this._flexH.SetCol("AM_DOCU", "전표금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexH.SetCol("AM_BAN", "반제금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flexH.SetCol("NO_DOCU_TAX", "전표번호", 100);
            this._flexH.SetCol("AM_DOCU_TAX", "전표금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexH.SetCol("AM_BAN_TAX", "반제금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flexH.SetCol("DC_RMK", "비고", 100, true);

            this._flexH[0, this._flexH.Cols["NO_DOCU"].Index] = "운송비";
            this._flexH[0, this._flexH.Cols["AM_DOCU"].Index] = "운송비";
            this._flexH[0, this._flexH.Cols["AM_BAN"].Index] = "운송비";

            this._flexH[0, this._flexH.Cols["NO_DOCU_TAX"].Index] = "통관비";
            this._flexH[0, this._flexH.Cols["AM_DOCU_TAX"].Index] = "통관비";
            this._flexH[0, this._flexH.Cols["AM_BAN_TAX"].Index] = "통관비";

            this._flexH.SetDataMap("TP_DELIVERY", MA.GetCode("CZ_SA00047", false), "CODE", "NAME");

            this._flexH.SettingVersion = "0.0.0.2";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

            this._flexH.SetCodeHelpCol("CD_PARTNER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_PARTNER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
            #endregion

            #region Line
            this._flexL.BeginSetting(2, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("DT_FORWARD", "포워딩일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("NO_BL", "B/L번호", 100, true);
            this._flexL.SetCol("NO_REF", "참조번호", 100, true);
            this._flexL.SetCol("NO_IO", "출고번호", 100, true);
            this._flexL.SetCol("AM_IO", "출고금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flexL.SetCol("AM_CHARGE", "운임금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_ETC", "기타금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_VAT_WEEK", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_WEEK", "금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_WEEK_TOTAL", "합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flexL.SetCol("AM_CHARGE_MONTH", "운임금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_ETC_MONTH", "기타금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_VAT_MONTH", "부가세", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_VAT_TARGET", "과세표준액", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_WFG", "창고료", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_MONTH", "금액", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_MONTH_TOTAL", "합계", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_TAX_MONTH", "통관비", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flexL.SetCol("AM_FULL", "대상금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("RT_DC", "포워더할인율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("RT_DC_IV", "청구할인율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("AM_IV", "청구금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_TAX", "통관비", 100, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flexL.SetCol("AM_CLS", "전체", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_CLS_AUTO", "자동청구", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_DIFF", "차액", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);

            this._flexL.SetCol("DT_TAX_MONTH", "통관비정산년월", false);
            this._flexL.SetCol("NM_NOTE", "적요", 100, true);
            this._flexL.SetCol("NO_DOCU", "전표번호", 100, false);
            this._flexL.SetCol("DC_RMK", "비고", 100, true);

            this._flexL.SetCol("WEIGHT", "무게", false);
            this._flexL.SetCol("ZONE", "Zone", 100, false);
            this._flexL.SetCol("RT_FSC", "유류할증료", false);
            this._flexL.SetCol("AM_ZONE", "단가", false);
            this._flexL.SetCol("SEQ", "순번", false);

            this._flexL[0, this._flexL.Cols["AM_CHARGE"].Index] = "주간";
            this._flexL[0, this._flexL.Cols["AM_ETC"].Index] = "주간";
            this._flexL[0, this._flexL.Cols["AM_VAT_WEEK"].Index] = "주간";
            this._flexL[0, this._flexL.Cols["AM_WEEK"].Index] = "주간";
            this._flexL[0, this._flexL.Cols["AM_WEEK_TOTAL"].Index] = "주간";

            this._flexL[0, this._flexL.Cols["AM_CHARGE_MONTH"].Index] = "월말";
            this._flexL[0, this._flexL.Cols["AM_ETC_MONTH"].Index] = "월말";
            this._flexL[0, this._flexL.Cols["AM_VAT_MONTH"].Index] = "월말";
            this._flexL[0, this._flexL.Cols["AM_VAT_TARGET"].Index] = "월말";
            this._flexL[0, this._flexL.Cols["AM_WFG"].Index] = "월말";
            this._flexL[0, this._flexL.Cols["AM_MONTH"].Index] = "월말";
            this._flexL[0, this._flexL.Cols["AM_MONTH_TOTAL"].Index] = "월말";
            this._flexL[0, this._flexL.Cols["AM_TAX_MONTH"].Index] = "월말";

            this._flexL[0, this._flexL.Cols["AM_FULL"].Index] = "청구";
            this._flexL[0, this._flexL.Cols["RT_DC"].Index] = "청구";
            this._flexL[0, this._flexL.Cols["RT_DC_IV"].Index] = "청구";
            this._flexL[0, this._flexL.Cols["AM_TAX"].Index] = "청구";
            this._flexL[0, this._flexL.Cols["AM_IV"].Index] = "청구";

            this._flexL[0, this._flexL.Cols["AM_CLS"].Index] = "매출";
            this._flexL[0, this._flexL.Cols["AM_CLS_AUTO"].Index] = "매출";
            this._flexL[0, this._flexL.Cols["AM_DIFF"].Index] = "매출";

            this._flexL.SetDummyColumn(new string[] { "S" });

            this._flexL.SettingVersion = "0.0.0.4";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexL.Styles.Add("YELLOW").BackColor = Color.Yellow;
            this._flexL.Styles.Add("ORANGE").BackColor = Color.Orange;
            this._flexL.Styles.Add("WHITE").BackColor = Color.White;
            #endregion
        }

        private void InitEvent()
        {
            this._flexH.AfterRowChange += _flexH_AfterRowChange;
            this._flexH.MouseDoubleClick += _flexH_MouseDoubleClick;
            this._flexL.OwnerDrawCell += _flexL_OwnerDrawCell;
			this._flexL.AfterEdit += _flexL_AfterEdit;
			this._flexL.MouseDoubleClick += _flexL_MouseDoubleClick;

            this.btn추가.Click += Btn추가_Click;
            this.btn제거.Click += Btn제거_Click;

            this.btn엑셀양식다운로드.Click += Btn엑셀양식다운로드_Click;
            this.btn엑셀양식다운로드통관.Click += Btn엑셀양식다운로드_Click;
            this.btn엑셀업로드주간.Click += Btn엑셀업로드_Click;
            this.btn엑셀업로드월말.Click += Btn엑셀업로드_Click;

            this.btn회계전표처리.Click += Btn회계전표처리_Click;
            this.btn회계전표처리통관비.Click += Btn회계전표처리통관비_Click;
            this.btn회계전표취소.Click += Btn회계전표취소_Click;
            this.btn회계전표취소통관비.Click += Btn회계전표취소통관비_Click;
			this.btn회계전표처리L.Click += Btn회계전표처리L_Click;
			this.btn회계전표취소L.Click += Btn회계전표취소L_Click;

			this.btn파일번호조회.Click += Btn파일번호조회_Click;
        }

        private void Btn파일번호조회_Click(object sender, EventArgs e)
        {
            DataTable dt;
            string query;
            
            try
            {
                if (string.IsNullOrEmpty(this.txt송장번호.Text))
				{
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl송장번호.Text);
                    return;
				}

                query = @";WITH A AS
(
    SELECT OL.CD_PJT 
    FROM MM_QTIOH OH WITH(NOLOCK)
    JOIN (SELECT OL.CD_COMPANY, OL.NO_IO, OL.CD_PJT
          FROM MM_QTIO OL WITH(NOLOCK)
          WHERE OL.FG_PS = '1'
          GROUP BY OL.CD_COMPANY, OL.NO_IO, OL.CD_PJT) OL
    ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
    WHERE OH.CD_COMPANY = '{0}'
    AND OH.TXT_USERDEF1 LIKE '{1}%'
    GROUP BY OL.CD_PJT
),
B AS
(
    SELECT SH.NO_SO,
           (CASE WHEN TP.RET = 'Y' THEN -ISNULL(SL.AM_SO, 0) ELSE ISNULL(SL.AM_SO, 0) END) AS AM_SO,
           SB.AM_STOCK,
           PL.AM_PO
    FROM SA_SOH SH WITH(NOLOCK)
    JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
                 SUM(SL.AM_WONAMT) AS AM_SO
          FROM SA_SOL SL WITH(NOLOCK)
          GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
    ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN (SELECT SB.CD_COMPANY, SB.NO_FILE,
    		   	      ISNULL(SUM(SB.UM_KR * SB.QT_STOCK), 0) AS AM_STOCK
    		   FROM CZ_SA_STOCK_BOOK SB WITH(NOLOCK)
    		   GROUP BY SB.CD_COMPANY, SB.NO_FILE) SB
    ON SB.CD_COMPANY = SH.CD_COMPANY AND SB.NO_FILE = SH.NO_SO
    LEFT JOIN (SELECT PH.CD_COMPANY, PH.CD_PJT,
    		   	      ISNULL(SUM(PL.AM), 0) AS AM_PO 
    		   FROM PU_POH PH WITH(NOLOCK)
    		   JOIN PU_POL PL WITH(NOLOCK) ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
    		   WHERE PH.CD_TPPO NOT IN ('1300', '1400', '2300', '2400')
    		   GROUP BY PH.CD_COMPANY, PH.CD_PJT) PL
    ON PL.CD_COMPANY = SH.CD_COMPANY AND PL.CD_PJT = SH.NO_SO
    LEFT JOIN SA_TPSO TP WITH(NOLOCK) ON TP.CD_COMPANY = SH.CD_COMPANY AND TP.TP_SO = SH.TP_SO
    WHERE SH.CD_COMPANY = '{0}'
    AND SH.NO_SO IN (SELECT A.CD_PJT FROM A)
),
C AS
(
    SELECT B.NO_SO,
           (ISNULL(B.AM_SO, 0) - (ISNULL(B.AM_PO, 0) + ISNULL(B.AM_STOCK, 0))) AS AM_PROFIT
    FROM B
),
D AS
(
    SELECT C.NO_SO,
           ROW_NUMBER() OVER (PARTITION BY NULL ORDER BY C.AM_PROFIT DESC) AS IDX
    FROM C
)
SELECT D.NO_SO
FROM D
WHERE D.IDX = 1";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.txt송장번호.Text));

                if (dt == null || dt.Rows.Count == 0)
				{
                    this.txt수주번호.Text = string.Empty;
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                    this.txt수주번호.Text = dt.Rows[0]["NO_SO"].ToString();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.splitContainer1.SplitterDistance = 202;

            this.dtp정산년월.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH, FormatFgType.INSERT);
            this.dtp정산년월.Text = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp정산년월.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

            this.cbo운송구분.DataSource = MA.GetCode("CZ_SA00047", true);
            this.cbo운송구분.DisplayMember = "NAME";
            this.cbo운송구분.ValueMember = "CODE";
        }

        private void _flexL_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this._flexL.HasNormalRow == false) return;
                if (this._flexL.MouseRow < this._flexL.Rows.Fixed) return;

                if (this._flexL.ColSel == this._flexL.Cols["NO_DOCU"].Index)
                {
                    if (string.IsNullOrEmpty(this._flexL["NO_DOCU"].ToString()))
                        return;

                    this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexL["NO_DOCU"]),
                                                                                                                                     "1",
                                                                                                                                     D.GetString(this._flexL["CD_PC"]),
                                                                                                                                     Global.MainFrame.LoginInfo.CompanyCode });
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flexL_AfterEdit(object sender, RowColEventArgs e)
		{
            DataTable dt;
            FlexGrid flexGrid;
            string query;

            try
            {
                flexGrid = (sender as FlexGrid);
                if (flexGrid == null) return;

                if (this._flexH["TP_DELIVERY"].ToString() == "F2F")
                {
                    #region F2F
                    if (flexGrid.Cols[e.Col].Name == "NO_REF")
                    {
                        if (string.IsNullOrEmpty(flexGrid["NO_REF"].ToString()))
                            return;

                        if (this._flexH["CD_PARTNER"].ToString() == "12448")
						{
                            #region 페더럴익스프레스 코리아 유한회사(FEDEX)
                            query = @"SELECT OH.NO_IO 
FROM MM_QTIOH OH WITH(NOLOCK)
WHERE OH.CD_COMPANY = '{0}'
AND OH.NO_IO = '{1}'
AND ISNULL(OH.YN_RETURN, 'N') = 'N'";

                            dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, flexGrid["NO_REF"].ToString()));
                            
                            if (dt.Rows.Count == 0)
                            {
                                this.ShowMessage("존재하지 않는 출고번호 입니다.");
                                return;
                            }
                            else
                                flexGrid["NO_IO"] = dt.Rows[0]["NO_IO"].ToString();
                            #endregion
                        }
						else
						{
                            query = @"SELECT OL.NO_IO 
FROM MM_QTIOH OH WITH(NOLOCK)
JOIN MM_QTIO OL WITH(NOLOCK) ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
WHERE OL.CD_COMPANY = '{0}'
AND OL.NO_PSO_MGMT = '{1}'
AND ISNULL(OH.YN_RETURN, 'N') = 'N'
AND OL.YN_PURSALE = 'Y'
GROUP BY OL.NO_IO";

                            dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, flexGrid["NO_REF"].ToString()));
                            string 출고번호 = string.Empty;

                            if (dt.Rows.Count == 0)
                            {
                                this.ShowMessage("수주번호에 해당하는 출고번호가 없습니다.");
                                return;
                            }
                            else if (dt.Rows.Count > 1)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    출고번호 = 출고번호 + dr["NO_IO"].ToString() + ",";
                                }

                                this.ShowMessage(string.Format("수주번호에 해당하는 출고번호가 여러개 입니다.\n출고번호 : {0}", 출고번호));
                                return;
                            }
                            else
                                flexGrid["NO_IO"] = dt.Rows[0]["NO_IO"].ToString();
                        }
                    }

                    if (flexGrid.Cols[e.Col].Name == "AM_CHARGE_MONTH")
                        flexGrid["AM_MONTH"] = D.GetDecimal(flexGrid["AM_CHARGE_MONTH"]);
                    #endregion
                }
				else if (this._flexH["TP_DELIVERY"].ToString() == "BWH" || 
                         this._flexH["TP_DELIVERY"].ToString() == "SP1" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TRK" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TRK1" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TRK2" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TEB1" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TEB3" ||
                         this._flexH["TP_DELIVERY"].ToString() == "INB" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TEB" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TEB2" ||
                         this._flexH["TP_DELIVERY"].ToString() == "SHIP" ||
                         (this._flexH["TP_DELIVERY"].ToString() == "AIR" && this._flexH["CD_PARTNER"].ToString() == "12448"))
				{
                    #region 보세창고료, 선적대행-단건, 용차-월말, 용차-단건(입,출고), 택배-월말, 택배-월초, 택배-단건(입,출고), 수입, FEDEX 항송
                    if (flexGrid.Cols[e.Col].Name == "AM_CHARGE_MONTH")
                    {
                        if (this._flexH["TP_DELIVERY"].ToString() != "INB" &&
                            this._flexH["CD_PARTNER"].ToString() != "99999" &&
                            this._flexH["CD_PARTNER"].ToString() != "12448")
                        {
                            flexGrid["AM_VAT_MONTH"] = D.GetDecimal(flexGrid["AM_CHARGE_MONTH"]) * ((decimal)0.1);
                            flexGrid["AM_VAT_TARGET"] = D.GetDecimal(flexGrid["AM_CHARGE_MONTH"]);
                        }

                        flexGrid["AM_MONTH"] = D.GetDecimal(flexGrid["AM_CHARGE_MONTH"]);
                    }

                    if (flexGrid.Cols[e.Col].Name == "AM_CHARGE_MONTH" || 
                        flexGrid.Cols[e.Col].Name == "NO_REF")
					{
                        if (string.IsNullOrEmpty(flexGrid["NO_REF"].ToString()))
                            return;

                        string 수주번호 = string.Empty;

                        query = @"CREATE TABLE #A (NO_VALUE NVARCHAR(20))

INSERT INTO #A
SELECT TRIM(VALUE) 
FROM STRING_SPLIT('{1}', ' ')

;WITH A AS
(
    SELECT SH.NO_SO 
    FROM SA_SOH SH WITH(NOLOCK)
    WHERE SH.CD_COMPANY = '{0}' 
    AND SH.NO_SO IN (SELECT NO_VALUE FROM #A)
    UNION
    SELECT OL.CD_PJT 
    FROM MM_QTIOH OH WITH(NOLOCK)
    JOIN (SELECT OL.CD_COMPANY, OL.NO_IO, OL.CD_PJT
          FROM MM_QTIO OL WITH(NOLOCK)
          WHERE OL.FG_PS = '1'
          GROUP BY OL.CD_COMPANY, OL.NO_IO, OL.CD_PJT) OL
    ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
    WHERE OH.CD_COMPANY = '{0}'
    AND OH.TXT_USERDEF1 LIKE '{1}%'
    GROUP BY OL.CD_PJT
    UNION
    SELECT GL.NO_SO
    FROM CZ_SA_GIRH_PACK GH WITH(NOLOCK)
    JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR, GL.NO_SO 
          FROM CZ_SA_GIRL_PACK GL WITH(NOLOCK)
          GROUP BY GL.CD_COMPANY, GL.NO_GIR, GL.NO_SO) GL
    ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
    WHERE GH.CD_COMPANY = '{0}'
    AND GH.NO_GIR IN (SELECT NO_VALUE FROM #A)
    GROUP BY GL.NO_SO
    UNION
    SELECT OL.NO_PSO_MGMT AS NO_SO
    FROM MM_QTIO OL WITH(NOLOCK)
    WHERE OL.CD_COMPANY = '{0}'
    AND (OL.NO_IO IN (SELECT NO_VALUE FROM #A) OR OL.NO_ISURCV IN (SELECT NO_VALUE FROM #A))
    GROUP BY OL.NO_PSO_MGMT
),
B AS
(
    SELECT SH.NO_SO,
           (CASE WHEN TP.RET = 'Y' THEN -ISNULL(SL.AM_SO, 0) ELSE ISNULL(SL.AM_SO, 0) END) AS AM_SO,
           SB.AM_STOCK,
           PL.AM_PO
    FROM SA_SOH SH WITH(NOLOCK)
    JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
                 SUM(SL.AM_WONAMT) AS AM_SO
          FROM SA_SOL SL WITH(NOLOCK)
          GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
    ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN (SELECT SB.CD_COMPANY, SB.NO_FILE,
    		   	      ISNULL(SUM(SB.UM_KR * SB.QT_STOCK), 0) AS AM_STOCK
    		   FROM CZ_SA_STOCK_BOOK SB WITH(NOLOCK)
    		   GROUP BY SB.CD_COMPANY, SB.NO_FILE) SB
    ON SB.CD_COMPANY = SH.CD_COMPANY AND SB.NO_FILE = SH.NO_SO
    LEFT JOIN (SELECT PH.CD_COMPANY, PH.CD_PJT,
    		   	      ISNULL(SUM(PL.AM), 0) AS AM_PO 
    		   FROM PU_POH PH WITH(NOLOCK)
    		   JOIN PU_POL PL WITH(NOLOCK) ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
    		   WHERE PH.CD_TPPO NOT IN ('1300', '1400', '2300', '2400')
    		   GROUP BY PH.CD_COMPANY, PH.CD_PJT) PL
    ON PL.CD_COMPANY = SH.CD_COMPANY AND PL.CD_PJT = SH.NO_SO
    LEFT JOIN SA_TPSO TP WITH(NOLOCK) ON TP.CD_COMPANY = SH.CD_COMPANY AND TP.TP_SO = SH.TP_SO
    WHERE SH.CD_COMPANY = '{0}'
    AND SH.NO_SO IN (SELECT NO_SO FROM A)
),
C AS
(
    SELECT B.NO_SO,
           (ISNULL(B.AM_SO, 0) - (ISNULL(B.AM_PO, 0) + ISNULL(B.AM_STOCK, 0))) AS AM_PROFIT,
           (CASE WHEN ISNULL(B.AM_SO, 0) = 0 THEN 0 
    										 ELSE ROUND(((1 - ((ISNULL(B.AM_PO, 0) + ISNULL(B.AM_STOCK, 0)) / ISNULL(B.AM_SO, 0))) * 100), 2) END) AS RT_PROFIT
    FROM B
),
D AS
(
    SELECT C.NO_SO, C.AM_PROFIT, C.RT_PROFIT,
           ROW_NUMBER() OVER (PARTITION BY NULL ORDER BY C.AM_PROFIT DESC) AS IDX
    FROM C
)
SELECT D.NO_SO, D.AM_PROFIT, D.RT_PROFIT
FROM D
WHERE D.IDX = 1

DROP TABLE #A";

                        dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, flexGrid["NO_REF"].ToString()));

                        if (dt == null || dt.Rows.Count == 0)
                        {
                            this.ShowMessage("유효하지 않은 수주번호 입니다. 참조번호 : " + flexGrid["NO_REF"].ToString());
                            return;
                        }
                        else
                        {
                            flexGrid["NO_IO"] = dt.Rows[0]["NO_SO"].ToString();
                            flexGrid["NM_NOTE"] = ((D.GetDecimal(dt.Rows[0]["AM_PROFIT"]) < D.GetDecimal(flexGrid["AM_CHARGE_MONTH"]) ? "★ " : "") +
                                                   "이윤 : " + D.GetDecimal(dt.Rows[0]["AM_PROFIT"]).ToString("N0") +
                                                   "(" + D.GetDecimal(dt.Rows[0]["RT_PROFIT"]).ToString("N2") + "%) ");
                        }
                    }
                    #endregion
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

            if (this._flexH.ColSel == this._flexH.Cols["NO_DOCU"].Index) 
            {
                if (string.IsNullOrEmpty(this._flexH["NO_DOCU"].ToString())) 
                    return;

                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexH["NO_DOCU"]),
                                                                                                                                 "1",
                                                                                                                                 D.GetString(this._flexH["CD_PC"]),
                                                                                                                                 Global.MainFrame.LoginInfo.CompanyCode });
            }
            else if (this._flexH.ColSel == this._flexH.Cols["NO_DOCU_TAX"].Index)
            {
                if (string.IsNullOrEmpty(this._flexH["NO_DOCU_TAX"].ToString())) 
                    return;

                this.CallOtherPageMethod("P_FI_DOCU", "전표입력(" + this.PageName + ")", "P_FI_DOCU", this.Grant, new object[] { D.GetString(this._flexH["NO_DOCU_TAX"]),
                                                                                                                                 "1",
                                                                                                                                 D.GetString(this._flexH["CD_PC_TAX"]),
                                                                                                                                 Global.MainFrame.LoginInfo.CompanyCode });
            }
        }

        private void Btn회계전표처리_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                this.btn회계전표처리.Enabled = false;

                if (this._flexL.DataTable.Select(string.Format(@"CD_PARTNER = '{0}' AND TP_DELIVERY = '{1}' AND DT_MONTH = '{2}' AND AM_IO < 0 AND NM_NOTE NOT IN ('선물', '서류')", this._flexH["CD_PARTNER"].ToString(), 
                                                                                                                                                                                     this._flexH["TP_DELIVERY"].ToString(), 
                                                                                                                                                                                     this._flexH["DT_MONTH"].ToString())).Length > 0)
                {
                    this.ShowMessage("출고번호가 잘못 지정되어 있는 건이 존재 합니다.");
                    return;
                }

                if (this._flexH["TP_DELIVERY"].ToString() == "TEB" || 
                    this._flexH["TP_DELIVERY"].ToString() == "TEB2" ||
                    this._flexH["TP_DELIVERY"].ToString() == "QCK" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TRK" ||
                    this._flexH["TP_DELIVERY"].ToString() == "SHIP" ||
                    (this._flexH["TP_DELIVERY"].ToString() == "AIR" && this._flexH["CD_PARTNER"].ToString() == "12448"))
				{
                    if (this._flexH["TP_DELIVERY"].ToString() == "TEB" && 
                        this._flexH["CD_PARTNER"].ToString() == "02436")
					{
                        this._biz.회계전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                             this._flexH["CD_PARTNER"].ToString(),
                                                             this._flexH["TP_DELIVERY"].ToString(),
                                                             this._flexH["DT_MONTH"].ToString(),
                                                             string.Empty,
                                                             "D05",
                                                             Global.MainFrame.LoginInfo.UserID });
                    }
                    else
					{
                        this._biz.회계전표처리택배(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this._flexH["CD_PARTNER"].ToString(),
                                                                 this._flexH["TP_DELIVERY"].ToString(),
                                                                 this._flexH["DT_MONTH"].ToString(),
                                                                 string.Empty,
                                                                 "D09",
                                                                 Global.MainFrame.LoginInfo.UserID });
                    }
                    
                }
                else
				{
                    this._biz.회계전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                         this._flexH["CD_PARTNER"].ToString(),
                                                         this._flexH["TP_DELIVERY"].ToString(),
                                                         this._flexH["DT_MONTH"].ToString(),
                                                         string.Empty,
                                                         "D05",
                                                         Global.MainFrame.LoginInfo.UserID });
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn회계전표처리.Text);
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

        private void Btn회계전표취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                this.btn회계전표취소.Enabled = false;

                if (this._flexH["TP_DELIVERY"].ToString() == "TEB" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TEB2" ||
                    this._flexH["TP_DELIVERY"].ToString() == "QCK" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TRK" ||
                    this._flexH["TP_DELIVERY"].ToString() == "SHIP" ||
                    (this._flexH["TP_DELIVERY"].ToString() == "AIR" && this._flexH["CD_PARTNER"].ToString() == "12448"))
                {
                    if (this._flexH["TP_DELIVERY"].ToString() == "TEB" &&
                        this._flexH["CD_PARTNER"].ToString() == "02436")
                    {
                        this._biz.회계전표취소("D05", this._flexH["NO_MDOCU"].ToString());
                    }
                    else
                    {
                        this._biz.회계전표취소("D09", this._flexH["NO_MDOCU"].ToString());
                    }
                }
                else
                {
                    this._biz.회계전표취소("D05", this._flexH["NO_MDOCU"].ToString());
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn회계전표취소.Text);
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

        private void Btn회계전표처리L_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            DataTable dt;
            string query, key;

            try
            {
                if (!this._flexL.HasNormalRow) return;

                if (this._flexH["YN_MULTI"].ToString() == "Y")
                {
                    if (Global.MainFrame.ShowMessage("다중 전표처리 하시겠습니까?", "QY2") != DialogResult.Yes)
                        return;
                }
                else
                {
                    if (Global.MainFrame.ShowMessage("건별 전표처리 하시겠습니까?", "QY2") != DialogResult.Yes)
                        return;
                }

                if (this._flexH["TP_DELIVERY"].ToString() != "F2F" &&
                    this._flexH["TP_DELIVERY"].ToString() != "BWH" &&
                    this._flexH["TP_DELIVERY"].ToString() != "SP1" &&
                    this._flexH["TP_DELIVERY"].ToString() != "TRK1" &&
                    this._flexH["TP_DELIVERY"].ToString() != "TRK2" &&
                    this._flexH["TP_DELIVERY"].ToString() != "INB" &&
                    this._flexH["TP_DELIVERY"].ToString() != "TEB1" &&
                    this._flexH["TP_DELIVERY"].ToString() != "TEB3" &&
                    this._flexH["TP_DELIVERY"].ToString() != "SHIP" &&
                    (this._flexH["TP_DELIVERY"].ToString() != "AIR" || this._flexH["CD_PARTNER"].ToString() != "12448"))
                {
                    this.ShowMessage("전표처리 대상 유형이 아닙니다.");
                    return;
                }

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

                if (dataRowArray == null && dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (this._flexL.DataTable.Select("S = 'Y' AND AM_IO < 0").Length > 0)
                {
                    this.ShowMessage("출고번호가 잘못 지정된 건이 있습니다.");
                    return;
                }
                else if (this._flexL.DataTable.Select("S = 'Y' AND SEQ <= 0").Length > 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, "순번", "0");
                    return;
                }
                else
                {
                    this.btn회계전표처리L.Enabled = false;

                    if (this._flexH["YN_MULTI"].ToString() == "Y")
                    {
                        #region 여러건 하나의 전표로 처리
                        key = this.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "CZ", "14", Global.MainFrame.GetStringToday.Substring(0, 6)).ToString();

                        foreach (DataRow dr in dataRowArray)
                        {
                            query = @"SELECT FD.NO_DOCU 
FROM CZ_SA_FORWARD_CHARGE_L FL WITH(NOLOCK)
JOIN (SELECT FD.CD_COMPANY, FD.NO_MDOCU,
             MAX(FD.NO_DOCU) AS NO_DOCU
      FROM FI_DOCU FD WITH(NOLOCK)
      GROUP BY FD.CD_COMPANY, FD.NO_MDOCU) FD
ON FD.CD_COMPANY = FL.CD_COMPANY AND FD.NO_MDOCU = FL.NO_MDOCU
WHERE FL.CD_COMPANY = '{0}'
AND FL.CD_PARTNER = '{1}'
AND FL.TP_DELIVERY = '{2}'
AND FL.DT_MONTH = '{3}'
AND FL.SEQ = {4}";

                            dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                            dr["CD_PARTNER"].ToString(),
                                                                            dr["TP_DELIVERY"].ToString(),
                                                                            dr["DT_MONTH"].ToString(),
                                                                            dr["SEQ"].ToString()));

                            if (dt != null && dt.Rows.Count > 0)
							{
                                this.ShowMessage(string.Format("전표처리된 건이 선택되어 있습니다. 전표번호 : {0}", dt.Rows[0]["NO_DOCU"].ToString()));
                                return;
							}
                            else
							{
                                query = @"UPDATE CZ_SA_FORWARD_CHARGE_L
SET NO_MDOCU = '{5}'
WHERE CD_COMPANY = '{0}'
AND CD_PARTNER = '{1}'
AND TP_DELIVERY = '{2}'
AND DT_MONTH = '{3}'
AND SEQ = {4}";

                                DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                            dr["CD_PARTNER"].ToString(),
                                                                            dr["TP_DELIVERY"].ToString(),
                                                                            dr["DT_MONTH"].ToString(),
                                                                            dr["SEQ"].ToString(),
                                                                            key));
                            }
                        }

                        if (this._flexH["TP_DELIVERY"].ToString() == "F2F")
                        {
                            this._biz.회계전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 this._flexH["CD_PARTNER"].ToString(),
                                                                 this._flexH["TP_DELIVERY"].ToString(),
                                                                 this._flexH["DT_MONTH"].ToString(),
                                                                 key,
                                                                 "D05",
                                                                 Global.MainFrame.LoginInfo.UserID });
                        }
                        else if (this._flexH["TP_DELIVERY"].ToString() == "BWH" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "SP1" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TRK1" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TRK2" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "INB" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TEB1" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TEB3" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "SHIP" ||
                                 (this._flexH["TP_DELIVERY"].ToString() == "AIR" && this._flexH["CD_PARTNER"].ToString() == "12448"))
                        {
                            this._biz.회계전표처리택배(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this._flexH["CD_PARTNER"].ToString(),
                                                                     this._flexH["TP_DELIVERY"].ToString(),
                                                                     this._flexH["DT_MONTH"].ToString(),
                                                                     key,
                                                                     "D09",
                                                                     Global.MainFrame.LoginInfo.UserID });
                        }
                        #endregion
                    }
                    else
                    {
                        #region 건별 전표 처리
                        query = @"UPDATE CZ_SA_FORWARD_CHARGE_L
SET NO_MDOCU = '{5}'
WHERE CD_COMPANY = '{0}'
AND CD_PARTNER = '{1}'
AND TP_DELIVERY = '{2}'
AND DT_MONTH = '{3}'
AND SEQ = {4}";

                        foreach (DataRow dr in dataRowArray)
                        {
                            key = dr["CD_PARTNER"].ToString() + dr["TP_DELIVERY"].ToString() + dr["DT_MONTH"].ToString() + "-" + dr["SEQ"].ToString();

                            DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                        dr["CD_PARTNER"].ToString(),
                                                                        dr["TP_DELIVERY"].ToString(),
                                                                        dr["DT_MONTH"].ToString(),
                                                                        dr["SEQ"].ToString(),
                                                                        key));

                            if (this._flexH["TP_DELIVERY"].ToString() == "F2F")
                            {
                                this._biz.회계전표처리(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     dr["CD_PARTNER"].ToString(),
                                                                     dr["TP_DELIVERY"].ToString(),
                                                                     dr["DT_MONTH"].ToString(),
                                                                     key,
                                                                     "D05",
                                                                     Global.MainFrame.LoginInfo.UserID });
                            }
                            else if (this._flexH["TP_DELIVERY"].ToString() == "BWH" ||
                                     this._flexH["TP_DELIVERY"].ToString() == "SP1" ||
                                     this._flexH["TP_DELIVERY"].ToString() == "TRK1" ||
                                     this._flexH["TP_DELIVERY"].ToString() == "TRK2" ||
                                     this._flexH["TP_DELIVERY"].ToString() == "INB" ||
                                     this._flexH["TP_DELIVERY"].ToString() == "TEB1" ||
                                     this._flexH["TP_DELIVERY"].ToString() == "TEB3" ||
                                     this._flexH["TP_DELIVERY"].ToString() == "SHIP" ||
                                     (this._flexH["TP_DELIVERY"].ToString() == "AIR" && this._flexH["CD_PARTNER"].ToString() == "12448"))
                            {
                                this._biz.회계전표처리택배(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         dr["CD_PARTNER"].ToString(),
                                                                         dr["TP_DELIVERY"].ToString(),
                                                                         dr["DT_MONTH"].ToString(),
                                                                         key,
                                                                         "D09",
                                                                         Global.MainFrame.LoginInfo.UserID });
                            }
                        }
                        #endregion
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn회계전표처리.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn회계전표처리L.Enabled = true;
            }
        }

        private void Btn회계전표취소L_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flexL.HasNormalRow) return;
                if (Global.MainFrame.ShowMessage("선택된 건 전표 취소 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                this.btn회계전표취소L.Enabled = false;

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

                if (dataRowArray == null && dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
				{
                    foreach (DataRow dr in dataRowArray)
					{
                        if (this._flexL["TP_DELIVERY"].ToString() == "F2F")
                            this._biz.회계전표취소("D05", dr["NO_MDOCU"].ToString());
                        else if (this._flexL["TP_DELIVERY"].ToString() == "BWH" ||
                                 this._flexL["TP_DELIVERY"].ToString() == "SP1" ||
                                 this._flexL["TP_DELIVERY"].ToString() == "TRK1" ||
                                 this._flexL["TP_DELIVERY"].ToString() == "TRK2" ||
                                 this._flexL["TP_DELIVERY"].ToString() == "INB" ||
                                 this._flexL["TP_DELIVERY"].ToString() == "TEB1" ||
                                 this._flexL["TP_DELIVERY"].ToString() == "TEB3" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "SHIP" ||
                                 (this._flexL["TP_DELIVERY"].ToString() == "AIR" && this._flexL["CD_PARTNER"].ToString() == "12448"))
                            this._biz.회계전표취소("D09", dr["NO_MDOCU"].ToString());
                    }
				}

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn회계전표취소L.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn회계전표취소L.Enabled = true;
            }
        }

        private void Btn회계전표처리통관비_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                this.btn회계전표처리통관비.Enabled = false;

                this._biz.회계전표처리통관비(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                            this.dtp정산년월.Text,
                                                            "D06",
                                                            Global.MainFrame.LoginInfo.UserID });

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn회계전표처리통관비.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn회계전표처리통관비.Enabled = true;
            }
        }

        private void Btn회계전표취소통관비_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                this.btn회계전표취소통관비.Enabled = false;

                this._biz.회계전표취소("D06", this._flexH["NO_MDOCU_TAX"].ToString());

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn회계전표취소통관비.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this.btn회계전표취소통관비.Enabled = true;
            }
        }

        private void Btn엑셀업로드_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            DataRow[] dataRowArray;
            DataRow dataRow;
            string query, 오류메시지 = string.Empty;
            int index;
            
            try
            {
                if (!this._flexH.HasNormalRow) return;

                #region btn엑셀업로드
                fileDlg.Filter = "엑셀 파일 (*.xlsx)|*.xlsx";

                if (fileDlg.ShowDialog() != DialogResult.OK) return;

                this._flexL.Redraw = false;

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileDlg.FileName + ";Extended Properties=Excel 12.0;";

                OleDbConnection con = new OleDbConnection(constr);
                con.Open();

                DataTable dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                OleDbCommand oconn;

                if (((Control)sender).Name == this.btn엑셀업로드주간.Name)
                    oconn = new OleDbCommand(@"Select * From [주간양식$]", con);
                else
                    oconn = new OleDbCommand(@"Select * From [월말양식$]", con);

                OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                DataTable dtExcel = new DataTable();

                sda.Fill(dtExcel);
                con.Close();

                for (int i = 0; i < 2; i++)
                {
                    dtExcel.Rows[i].Delete();
                }

                dtExcel.AcceptChanges();

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk, argsPkNm;

                if (((Control)sender).Name == this.btn엑셀업로드월말.Name &&
                    dtExcel.Columns.Contains("NO_IO")) // 통관비
				{
                    argsPk = new string[] { "NO_IO" };
                    argsPkNm = new string[] { DD("B/L번호") };
                }
                else
				{
                    if (this._flexH["TP_DELIVERY"].ToString() == "AIR" && 
                        this._flexH["CD_PARTNER"].ToString() != "12448")
                    {
                        argsPk = new string[] { "NO_BL", "NO_REF" };
                        argsPkNm = new string[] { DD("B/L번호"), DD("참조번호") };
                    }
                    else
                    {
                        argsPk = new string[] { "NO_REF" };
                        argsPkNm = new string[] { DD("참조번호") };
                    }
                }

                for (int i = 0; i < argsPk.Length; i++)
                {
                    if (!dtExcel.Columns.Contains(argsPk[i]))
                    {
                        this.ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new object[] { argsPk[i], argsPkNm[i] });
                        return;
                    }
                }

                if (((Control)sender).Name == this.btn엑셀업로드주간.Name)
                {
                    #region 주간
                    query = @"SELECT TOP 1
	   SL.WEIGHT,
	   PH.RT_FSC,
       ROUND(SL.AM_ZONE{0} + (SL.AM_ZONE{0} * (PH.RT_FSC / 100)), 0) AS AM_ZONE,
	   (CASE WHEN SL.WEIGHT > 30 THEN ROUND(SL.AM_ZONE{0} * {2}, 0) ELSE SL.AM_ZONE{0} END) AS AM_CHARGE, 
       (CASE WHEN SL.WEIGHT > 30 THEN ROUND(ROUND(SL.AM_ZONE{0} + (SL.AM_ZONE{0} * (PH.RT_FSC / 100)), 0) * {2}, 0) 
                                 ELSE ROUND(SL.AM_ZONE{0} + (SL.AM_ZONE{0} * (PH.RT_FSC / 100)), 0) END) AS AM_FULL 
FROM CZ_MA_TARIFF_DHL_PAY_H PH WITH(NOLOCK)
JOIN CZ_MA_TARIFF_DHL_STD_L SL WITH(NOLOCK) ON SL.CD_COMPANY = PH.CD_COMPANY AND SL.DT_YEAR = SUBSTRING(PH.DT_MONTH, 1, 4) AND SL.TP_TARIFF = '{3}'
WHERE PH.CD_COMPANY = '{4}'
AND PH.DT_MONTH = '{1}'
AND SL.WEIGHT <= {2}
ORDER BY SL.WEIGHT DESC";

                    // 데이터 읽으면서 해당하는 값 셋팅
                    index = 0;
                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { (++index).ToString(), dtExcel.Rows.Count.ToString() });

                        dataRowArray = this._flexL.DataTable.Select("CD_PARTNER = '" + this._flexH["CD_PARTNER"].ToString() +
                                                                    "' AND TP_DELIVERY = '" + this._flexH["TP_DELIVERY"].ToString() +
                                                                    "' AND DT_MONTH = '" + this._flexH["DT_MONTH"].ToString() +
                                                                    "' AND NO_BL = '" + dr["NO_BL"].ToString() +
                                                                    "' AND NO_REF = '" + dr["NO_REF"].ToString() + "'");

                        if (dataRowArray.Length > 0)
                        {
                            오류메시지 += "해당하는 건이 이미 추가되어 있습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                            continue;
                        }

                        dataRow = this._flexL.DataTable.NewRow();

                        dataRow["CD_PARTNER"] = this._flexH["CD_PARTNER"].ToString();
                        dataRow["TP_DELIVERY"] = this._flexH["TP_DELIVERY"].ToString();
                        dataRow["DT_MONTH"] = this._flexH["DT_MONTH"].ToString();
                        dataRow["SEQ"] = this.SeqMax();
                        
                        dataRow["NO_BL"] = dr["NO_BL"].ToString();
                        dataRow["NO_REF"] = dr["NO_REF"].ToString();
                        
                        if (dataRow["CD_PARTNER"].ToString() == "01107")
                        {
                            dataRow["NO_IO"] = dr["NO_REF"].ToString();
                            
                            if (dataRow["TP_DELIVERY"].ToString() == "AIR")
                            {
                                #region DHL 항송
                                DateTime dtForward;

                                if (DateTime.TryParseExact(dr["DT_FORWARD"].ToString(), "dd/MM/yyyy", null, DateTimeStyles.None, out dtForward))
                                    dataRow["DT_FORWARD"] = dtForward.ToString("yyyyMMdd");
                                else if (DateTime.TryParse(dr["DT_FORWARD"].ToString(), out dtForward))
                                    dataRow["DT_FORWARD"] = dtForward.ToString("yyyyMMdd");
                                else if (DateTime.TryParseExact(dr["DT_FORWARD"].ToString(), "yyyy-MM-dd", null, DateTimeStyles.None, out dtForward))
                                    dataRow["DT_FORWARD"] = dtForward.ToString("yyyyMMdd");
                                else
                                {
                                    오류메시지 += "지원하지 않는 날짜형식 입니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                    continue;
                                }
                                
                                dataRow["WEIGHT"] = dr["WEIGHT"].ToString().Trim();
                                dataRow["ZONE"] = dr["ZONE"];

                                string query1 = string.Format(query, dr["ZONE"].ToString(),
                                                                     dataRow["DT_FORWARD"].ToString().Substring(0, 6),
                                                                     dataRow["WEIGHT"].ToString(),
                                                                     "001",
                                                                     Global.MainFrame.LoginInfo.CompanyCode);

                                DataTable dt = DBHelper.GetDataTable(query1);

                                if (dt == null || dt.Rows.Count == 0)
                                {
                                    오류메시지 += "조건에 해당하는 일반 가격표가 없습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                    continue;
                                }

                                query1 = string.Format(query, dr["ZONE"].ToString(),
                                                              dataRow["DT_FORWARD"].ToString().Substring(0, 6),
                                                              dataRow["WEIGHT"].ToString(),
                                                              "002",
                                                              Global.MainFrame.LoginInfo.CompanyCode);

                                DataTable dt1 = DBHelper.GetDataTable(query1);

                                if (dt1 == null || dt1.Rows.Count == 0)
                                {
                                    오류메시지 += "조건에 해당하는 할인 가격표가 없습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                    continue;
                                }

                                dataRow["RT_FSC"] = dt1.Rows[0]["RT_FSC"];
                                dataRow["AM_ZONE"] = dt1.Rows[0]["AM_ZONE"];

                                dataRow["AM_CHARGE"] = dt1.Rows[0]["AM_CHARGE"];
                                dataRow["AM_ETC"] = Convert.ToDecimal(dt1.Rows[0]["AM_FULL"]) - Convert.ToDecimal(dt1.Rows[0]["AM_CHARGE"]);
                                dataRow["AM_VAT_WEEK"] = 0;
                                dataRow["AM_WEEK"] = dt1.Rows[0]["AM_FULL"];

                                dataRow["AM_FULL"] = dt.Rows[0]["AM_FULL"];
                                dataRow["RT_DC"] = 0;
                                dataRow["RT_DC_IV"] = 0.2;
                                dataRow["AM_TAX"] = 0;
                                dataRow["AM_IV"] = Math.Round(Convert.ToDecimal(dataRow["AM_FULL"]) * (1 - Convert.ToDecimal(dataRow["RT_DC_IV"])), MidpointRounding.AwayFromZero);
                                #endregion
                            }
                        }
                        else if (dataRow["CD_PARTNER"].ToString() == "08624")
                        {
                            dataRow["NO_IO"] = dr["NO_REF"].ToString();
                            dataRow["DC_RMK"] = dr["DC_RMK"].ToString().Replace("\n", Environment.NewLine);
                            dr["AM_VAT"] = Math.Round(Convert.ToDecimal(DBNull.Value.Equals(dr["AM_VAT"]) ? 0 : dr["AM_VAT"]), MidpointRounding.AwayFromZero);

                            if (dataRow["TP_DELIVERY"].ToString() == "AIR")
                            {
                                #region TPO 항송
                                dataRow["DT_FORWARD"] = Global.MainFrame.GetStringToday;

                                dataRow["AM_CHARGE"] = Convert.ToDecimal(DBNull.Value.Equals(dr["AM_AF"]) || dr["AM_AF"].ToString() == "-" ? 0 : dr["AM_AF"]);
                                dataRow["AM_ETC"] = Convert.ToDecimal(DBNull.Value.Equals(dr["AM_FC"]) || dr["AM_FC"].ToString() == "-" ? 0 : dr["AM_FC"]) +
                                                    Convert.ToDecimal(DBNull.Value.Equals(dr["AM_SC"]) || dr["AM_SC"].ToString() == "-" ? 0 : dr["AM_SC"]) +
                                                    Convert.ToDecimal(DBNull.Value.Equals(dr["AM_PICKUP"]) || dr["AM_PICKUP"].ToString() == "-" ? 0 : dr["AM_PICKUP"]) +
                                                    Convert.ToDecimal(DBNull.Value.Equals(dr["AM_HANDLING"]) || dr["AM_HANDLING"].ToString() == "-" ? 0 : dr["AM_HANDLING"]);
                                dataRow["AM_VAT_WEEK"] = Convert.ToDecimal(dr["AM_VAT"]);
                                dataRow["AM_WEEK"] = Convert.ToDecimal(dataRow["AM_CHARGE"]) + Convert.ToDecimal(dataRow["AM_ETC"]);

                                if (!string.IsNullOrEmpty(dr["RT_DC"].ToString()))
                                {
                                    if (dr["RT_DC"].ToString().Contains("%"))
                                        dataRow["RT_DC"] = (Convert.ToDecimal(dr["RT_DC"].ToString().Replace("%", string.Empty)) / 100);
                                    else
                                        dataRow["RT_DC"] = Convert.ToDecimal(dr["RT_DC"].ToString());
                                }
                                else
                                    dataRow["RT_DC"] = 0;

                                dataRow["AM_FULL"] = Convert.ToDecimal(dataRow["AM_CHARGE"]) / (1 - Convert.ToDecimal(dataRow["RT_DC"]));

                                dataRow["RT_DC_IV"] = decimal.Multiply(Convert.ToDecimal(dataRow["RT_DC"]), (decimal)0.5);
                                dataRow["AM_TAX"] = 10000;
                                dataRow["AM_IV"] = Math.Round(Convert.ToDecimal(dataRow["AM_FULL"]) * (1 - Convert.ToDecimal(dataRow["RT_DC_IV"])) + Convert.ToDecimal(dataRow["AM_ETC"]), MidpointRounding.AwayFromZero);
                                #endregion
                            }
                            else if (dataRow["TP_DELIVERY"].ToString() == "SEA")
                            {
                                #region TPO 해송
                                dataRow["DT_FORWARD"] = Convert.ToDateTime(dr["DT_FORWARD"]).ToString("yyyyMMdd");

                                dataRow["AM_CHARGE"] = Convert.ToDecimal(DBNull.Value.Equals(dr["AM_OF"]) || dr["AM_OF"].ToString() == "-" ? 0 : dr["AM_OF"]);
                                dataRow["AM_ETC"] = Convert.ToDecimal(DBNull.Value.Equals(dr["AM_WFG"]) || dr["AM_WFG"].ToString() == "-" ? 0 : dr["AM_WFG"]) +
                                                    Convert.ToDecimal(DBNull.Value.Equals(dr["AM_THC"]) || dr["AM_THC"].ToString() == "-" ? 0 : dr["AM_THC"]) +
                                                    Convert.ToDecimal(DBNull.Value.Equals(dr["AM_CFS"]) || dr["AM_CFS"].ToString() == "-" ? 0 : dr["AM_CFS"]) +
                                                    Convert.ToDecimal(DBNull.Value.Equals(dr["AM_DOC"]) || dr["AM_DOC"].ToString() == "-" ? 0 : dr["AM_DOC"]) +
                                                    Convert.ToDecimal(DBNull.Value.Equals(dr["AM_OTHER"]) || dr["AM_OTHER"].ToString() == "-" ? 0 : dr["AM_OTHER"]) +
                                                    Convert.ToDecimal(DBNull.Value.Equals(dr["AM_OTHER1"]) || dr["AM_OTHER1"].ToString() == "-" ? 0 : dr["AM_OTHER1"]);
                                dataRow["AM_VAT_WEEK"] = Convert.ToDecimal(DBNull.Value.Equals(dr["AM_VAT"]) ? 0 : dr["AM_VAT"]);
                                dataRow["AM_WEEK"] = Convert.ToDecimal(dataRow["AM_CHARGE"]) + Convert.ToDecimal(dataRow["AM_ETC"]);

                                dataRow["AM_FULL"] = Convert.ToDecimal(dataRow["AM_CHARGE"]);
                                dataRow["RT_DC"] = 0;
                                dataRow["RT_DC_IV"] = 0;
                                dataRow["AM_TAX"] = 10000;
                                dataRow["AM_IV"] = Math.Round(Convert.ToDecimal(dataRow["AM_FULL"]) * (1 - Convert.ToDecimal(dataRow["RT_DC_IV"])) + Convert.ToDecimal(dataRow["AM_ETC"]), MidpointRounding.AwayFromZero);
                                #endregion
                            }
                        }
                        else if (dataRow["CD_PARTNER"].ToString() == "02436")
                        {
                            dataRow["NO_IO"] = dr["NO_REF"].ToString();
                            dataRow["DC_RMK"] = dr["DC_RMK"].ToString().Replace("\n", Environment.NewLine);

                            decimal vat = 0;
                            if (decimal.TryParse(dr["AM_VAT"].ToString(), out vat))
                                dr["AM_VAT"] = Math.Round(vat, MidpointRounding.AwayFromZero);
                            else
                                dr["AM_VAT"] = vat;

                            if (dataRow["TP_DELIVERY"].ToString() == "AIR")
                            {
                                #region 쉥커 항송
                                dataRow["DT_FORWARD"] = Convert.ToDateTime(dr["DT_FORWARD"]).ToString("yyyyMMdd");

                                dataRow["AM_CHARGE"] = Convert.ToDecimal(dr["AM_AF"]);
                                dataRow["AM_ETC"] = Convert.ToDecimal(dr["AM_SECURITY"].ToString() == string.Empty ? 0 : dr["AM_SECURITY"]) +
                                                    Convert.ToDecimal(dr["AM_FS"].ToString() == string.Empty ? 0 : dr["AM_FS"]) +
                                                    Convert.ToDecimal(dr["AM_HC"].ToString() == string.Empty ? 0 : dr["AM_HC"]) +
                                                    Convert.ToDecimal(dr["AM_PUP"].ToString() == string.Empty ? 0 : dr["AM_PUP"]) +
                                                    Convert.ToDecimal(dr["AM_DG"].ToString() == string.Empty ? 0 : dr["AM_DG"]) +
                                                    Convert.ToDecimal(dr["AM_OTHER"].ToString() == string.Empty ? 0 : dr["AM_OTHER"]);
                                dataRow["AM_VAT_WEEK"] = vat;
                                dataRow["AM_WEEK"] = Convert.ToDecimal(dr["AM_AF"]) + Convert.ToDecimal(dataRow["AM_ETC"]);

                                if (dr["RT_DC"].ToString().Trim() != "NDC" &&
                                    dr["RT_DC"].ToString().Trim() != "NCD" &&
                                    !string.IsNullOrEmpty(dr["RT_DC"].ToString()))
                                    dataRow["RT_DC"] = (Convert.ToDecimal(dr["RT_DC"].ToString().Replace("%", string.Empty)) / 100);
                                else
                                    dataRow["RT_DC"] = 0;

                                dataRow["AM_FULL"] = Convert.ToDecimal(dataRow["AM_CHARGE"]) / (1 - Convert.ToDecimal(dataRow["RT_DC"]));
                                dataRow["RT_DC_IV"] = decimal.Multiply(Convert.ToDecimal(dataRow["RT_DC"]), (decimal)0.5);
                                dataRow["AM_TAX"] = 10000;
                                dataRow["AM_IV"] = Math.Round(Convert.ToDecimal(dataRow["AM_FULL"]) * (1 - Convert.ToDecimal(dataRow["RT_DC_IV"])) + Convert.ToDecimal(dataRow["AM_ETC"]), MidpointRounding.AwayFromZero);
                                #endregion
                            }
                            else if (dataRow["TP_DELIVERY"].ToString() == "SEA")
                            {
                                #region 쉥커 해송
                                dataRow["DT_FORWARD"] = Global.MainFrame.GetStringToday;

                                dataRow["AM_CHARGE"] = Convert.ToDecimal(dr["AM_OF"]);
                                dataRow["AM_ETC"] = (Convert.ToDecimal(dr["AM_TOTAL"]) - vat) - Convert.ToDecimal(dr["AM_OF"]);
                                dataRow["AM_VAT_WEEK"] = vat;
                                dataRow["AM_WEEK"] = Convert.ToDecimal(dr["AM_OF"]) + Convert.ToDecimal(dataRow["AM_ETC"]);

                                dataRow["AM_FULL"] = Convert.ToDecimal(dr["AM_OF"]);
                                dataRow["RT_DC"] = 0;
                                dataRow["RT_DC_IV"] = 0;
                                dataRow["AM_TAX"] = 10000;
                                dataRow["AM_IV"] = Math.Round(Convert.ToDecimal(dataRow["AM_FULL"]) * (1 - Convert.ToDecimal(dataRow["RT_DC_IV"])) + Convert.ToDecimal(dataRow["AM_ETC"]), MidpointRounding.AwayFromZero);
                                #endregion
                            }
                        }
                        else if (dataRow["CD_PARTNER"].ToString() == "15464")
						{
                            dataRow["NO_IO"] = dr["NO_REF"].ToString();
                            dataRow["DC_RMK"] = dr["DC_RMK"].ToString().Replace("\n", Environment.NewLine);

                            dr["AM_VAT"] = Math.Round(Convert.ToDecimal(dr["AM_VAT"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty)), MidpointRounding.AwayFromZero);

                            if (dataRow["TP_DELIVERY"].ToString() == "AIR")
                            {
                                #region SR 항송
                                dataRow["DT_FORWARD"] = Convert.ToDateTime(dr["DT_FORWARD"]).ToString("yyyyMMdd");

                                dataRow["AM_CHARGE"] = Convert.ToDecimal(dr["AM_AF"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                decimal amEtc = 0;

                                if (dr["AM_FSC"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_FSC"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_CGC"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_CGC"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_CUSTOMS"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_CUSTOMS"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_PICKUP"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_PICKUP"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_HANDLING"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_HANDLING"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                dataRow["AM_ETC"] = amEtc;
                                dataRow["AM_VAT_WEEK"] = Convert.ToDecimal(dr["AM_VAT"]);
                                dataRow["AM_WEEK"] = Convert.ToDecimal(dr["AM_AF"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty)) + Convert.ToDecimal(dataRow["AM_ETC"]);

                                if (!string.IsNullOrEmpty(dr["RT_DC"].ToString()))
                                {
                                    if (dr["RT_DC"].ToString().Contains("%"))
                                        dataRow["RT_DC"] = (Convert.ToDecimal(dr["RT_DC"].ToString().Replace("%", string.Empty)) / 100);
                                    else
                                        dataRow["RT_DC"] = Convert.ToDecimal(dr["RT_DC"].ToString());
								}   
                                else
                                    dataRow["RT_DC"] = 0;

                                dataRow["AM_FULL"] = Convert.ToDecimal(dataRow["AM_CHARGE"]) / (1 - Convert.ToDecimal(dataRow["RT_DC"]));
                                dataRow["RT_DC_IV"] = decimal.Multiply(Convert.ToDecimal(dataRow["RT_DC"]), (decimal)0.5);
                                dataRow["AM_TAX"] = 10000;
                                dataRow["AM_IV"] = Math.Round(Convert.ToDecimal(dataRow["AM_FULL"]) * (1 - Convert.ToDecimal(dataRow["RT_DC_IV"])) + Convert.ToDecimal(dataRow["AM_ETC"]), MidpointRounding.AwayFromZero);
                                #endregion
                            }
                            else if (dataRow["TP_DELIVERY"].ToString() == "SEA")
							{
                                #region SR 해송
                                dataRow["DT_FORWARD"] = Convert.ToDateTime(dr["DT_FORWARD"]).ToString("yyyyMMdd");

                                dataRow["AM_CHARGE"] = Convert.ToDecimal(dr["AM_OF"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                decimal amEtc = 0;

                                if (dr["AM_CFS"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_CFS"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_THC"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_THC"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_DRG"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_DRG"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_WFG_PFS"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_WFG_PFS"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_DOC"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_DOC"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_PICKUP"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_PICKUP"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                if (dr["AM_HANDLING"].ToString() != string.Empty)
                                    amEtc += Convert.ToDecimal(dr["AM_HANDLING"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                dataRow["AM_ETC"] = amEtc;
                                dataRow["AM_VAT_WEEK"] = Convert.ToDecimal(dr["AM_VAT"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                dataRow["AM_WEEK"] = Convert.ToDecimal(dr["AM_OF"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty)) + Convert.ToDecimal(dataRow["AM_ETC"]);

                                dataRow["AM_FULL"] = Convert.ToDecimal(dr["AM_OF"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                dataRow["RT_DC"] = 0;
                                dataRow["RT_DC_IV"] = 0;
                                dataRow["AM_TAX"] = 10000;
                                dataRow["AM_IV"] = Math.Round(Convert.ToDecimal(dataRow["AM_FULL"]) * (1 - Convert.ToDecimal(dataRow["RT_DC_IV"])) + Convert.ToDecimal(dataRow["AM_ETC"]), MidpointRounding.AwayFromZero);
                                #endregion
                            }
						}

                        dataRow["AM_WEEK_TOTAL"] = (Convert.ToDecimal(dataRow["AM_VAT_WEEK"]) + Convert.ToDecimal(dataRow["AM_WEEK"]));

                        this._flexL.DataTable.Rows.Add(dataRow);
                    }
                    #endregion
                }
                else
                {
                    #region 월말
                    // 데이터 읽으면서 해당하는 값 셋팅
                    index = 0;
                    string 여러건존재 = string.Empty, 해당건없음 = string.Empty;

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { (++index).ToString(), dtExcel.Rows.Count.ToString() });
                        if (this._flexH["TP_DELIVERY"].ToString() == "TEB" && this._flexH["CD_PARTNER"].ToString() == "02436")
                        {
                            #region 쉥커 픽업
                            dataRow = this._flexL.DataTable.NewRow();

                            dataRow["CD_PARTNER"] = this._flexH["CD_PARTNER"].ToString();
                            dataRow["TP_DELIVERY"] = this._flexH["TP_DELIVERY"].ToString();
                            dataRow["DT_MONTH"] = this._flexH["DT_MONTH"].ToString();
                            dataRow["SEQ"] = this.SeqMax();

                            dataRow["NO_BL"] = "";
                            dataRow["NO_REF"] = dr["NO_REF"].ToString();
                            dataRow["NO_IO"] = dr["NO_REF"].ToString();

                            DateTime dtForward;

                            if (DateTime.TryParse(dr["DT_FORWARD"].ToString(), out dtForward))
                                dataRow["DT_FORWARD"] = dtForward.ToString("yyyyMMdd");

                            dataRow["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_CHARGE"].ToString() == string.Empty ? 0 : dr["AM_CHARGE"]);

                            dataRow["AM_MONTH"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                            dataRow["AM_VAT_TARGET"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                            dataRow["AM_VAT_MONTH"] = Convert.ToDecimal(dr["AM_VAT"].ToString() == string.Empty ? 0 : dr["AM_VAT"]);

                            dataRow["ZONE"] = 1;

                            this._flexL.DataTable.Rows.Add(dataRow);
                            #endregion
                        }
                        else if (this._flexH["TP_DELIVERY"].ToString() == "TEB" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TEB1" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TEB2" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TEB3" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "QCK" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TRK" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TRK1" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "TRK2" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "BWH" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "F2F" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "INB" ||
                                 this._flexH["TP_DELIVERY"].ToString() == "SP1" ||
                                 (this._flexH["TP_DELIVERY"].ToString() == "SHIP" && this._flexH["CD_PARTNER"].ToString() != "07548") ||
                                 (this._flexH["TP_DELIVERY"].ToString() == "AIR" && this._flexH["CD_PARTNER"].ToString() == "12448"))

                        {
                            #region 택배-월초, 택배-단건(입,출고), 택배-월말, 퀵서비스, 용차-월말, 용차-단건(입,출고), 보세창고료, F2F, 수입, 선적대행-단건, 선적대행-월말, FEDEX 항송
                            dataRow = this._flexL.DataTable.NewRow();

                            dataRow["CD_PARTNER"] = this._flexH["CD_PARTNER"].ToString();
                            dataRow["TP_DELIVERY"] = this._flexH["TP_DELIVERY"].ToString();
                            dataRow["DT_MONTH"] = this._flexH["DT_MONTH"].ToString();
                            dataRow["SEQ"] = this.SeqMax();

                            dataRow["NO_BL"] = "";
                            dataRow["NO_REF"] = dr["NO_REF"].ToString();

                            if (this._flexH["CD_PARTNER"].ToString() == "17373" ||
                                this._flexH["CD_PARTNER"].ToString() == "17186" ||
                                this._flexH["CD_PARTNER"].ToString() == "17358" ||
                                this._flexH["CD_PARTNER"].ToString() == "17593" ||
                                this._flexH["CD_PARTNER"].ToString() == "17299" ||
                                this._flexH["CD_PARTNER"].ToString() == "12101" ||
                                this._flexH["CD_PARTNER"].ToString() == "11889" ||
                                this._flexH["CD_PARTNER"].ToString() == "18260")
                            {
                                #region 부가세 미포함
                                dataRow["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_CHARGE"].ToString() == string.Empty ? 0 : dr["AM_CHARGE"]);

                                dataRow["AM_MONTH"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                                dataRow["AM_VAT_TARGET"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                                dataRow["AM_VAT_MONTH"] = (Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]) * Convert.ToDecimal(0.1));
                                #endregion
                            }
                            else if (this._flexH["CD_PARTNER"].ToString() == "99999" ||
                                     this._flexH["CD_PARTNER"].ToString() == "12448" ||
                                     this._flexH["TP_DELIVERY"].ToString() == "INB")
                            {
                                #region 부가세 없음
                                dataRow["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_CHARGE"].ToString() == string.Empty ? 0 : dr["AM_CHARGE"]);

                                dataRow["AM_MONTH"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                                #endregion
                            }
                            else
                            {
                                #region 부가세포함
                                dataRow["AM_CHARGE_MONTH"] = Decimal.Round(((Convert.ToDecimal(dr["AM_CHARGE"].ToString() == string.Empty ? 0 : dr["AM_CHARGE"])) / 11) * 10);

                                dataRow["AM_MONTH"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                                dataRow["AM_VAT_TARGET"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                                dataRow["AM_VAT_MONTH"] = Decimal.Round((Convert.ToDecimal(dr["AM_CHARGE"].ToString() == string.Empty ? 0 : dr["AM_CHARGE"])) / 11);
                                #endregion
                            }

                            if (DBNull.Value.Equals(dr["ZONE"]))
                            {
                                오류메시지 += "발송/도착 구분이 잘못 입력 되었습니다. 참조번호 : " + dataRow["NO_REF"].ToString() + Environment.NewLine;
                                continue;
                            }
                            else if (dr["ZONE"].ToString() == "1")
							{
                                #region 발송
                                query = @"SELECT MC.CD_CC 
                                          FROM MA_CC MC WITH(NOLOCK)
                                          WHERE MC.CD_COMPANY = '{0}'
                                          AND MC.CD_CC = '{1}'";

                                DataTable dt1 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dataRow["NO_REF"]));

                                if (dt1 != null && dt1.Rows.Count > 0)
                                    dataRow["NO_IO"] = dataRow["NO_REF"].ToString();
                                else
								{
                                    query = @"CREATE TABLE #A (NO_VALUE NVARCHAR(20))

INSERT INTO #A
SELECT TRIM(VALUE) 
FROM STRING_SPLIT('{1}', ' ')

;WITH A AS
(
    SELECT OL.CD_COMPANY,
           OL.NO_PSO_MGMT AS NO_SO
    FROM MM_QTIO OL WITH(NOLOCK)
    WHERE OL.CD_COMPANY = '{0}'
    AND (OL.NO_IO IN (SELECT NO_VALUE FROM #A) OR OL.NO_ISURCV IN (SELECT NO_VALUE FROM #A))
    GROUP BY OL.CD_COMPANY, OL.NO_PSO_MGMT
    UNION
    SELECT SH.CD_COMPANY,
           SH.NO_SO 
    FROM SA_SOH SH WITH(NOLOCK)
    WHERE SH.CD_COMPANY = '{0}'
    AND SH.NO_SO = '{1}'
),
B AS
(
    SELECT OL.NO_SO,
           (CASE WHEN TP.RET = 'Y' THEN -ISNULL(SL.AM_SO, 0) ELSE ISNULL(SL.AM_SO, 0) END) AS AM_SO,
           SB.AM_STOCK,
           PL.AM_PO 
    FROM A OL
    JOIN SA_SOH SH WITH(NOLOCK) ON SH.CD_COMPANY = OL.CD_COMPANY AND SH.NO_SO = OL.NO_SO
    JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
                 SUM(SL.AM_WONAMT) AS AM_SO
          FROM SA_SOL SL WITH(NOLOCK)
          GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
    ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN (SELECT SB.CD_COMPANY, SB.NO_FILE,
    		   	      ISNULL(SUM(SB.UM_KR * SB.QT_STOCK), 0) AS AM_STOCK
    		   FROM CZ_SA_STOCK_BOOK SB WITH(NOLOCK)
    		   GROUP BY SB.CD_COMPANY, SB.NO_FILE) SB
    ON SB.CD_COMPANY = SH.CD_COMPANY AND SB.NO_FILE = SH.NO_SO
    LEFT JOIN (SELECT PH.CD_COMPANY, PH.CD_PJT,
    		   	      ISNULL(SUM(PL.AM), 0) AS AM_PO 
    		   FROM PU_POH PH WITH(NOLOCK)
    		   JOIN PU_POL PL WITH(NOLOCK) ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
    		   WHERE PH.CD_TPPO NOT IN ('1300', '1400', '2300', '2400')
    		   GROUP BY PH.CD_COMPANY, PH.CD_PJT) PL
    ON PL.CD_COMPANY = SH.CD_COMPANY AND PL.CD_PJT = SH.NO_SO
    LEFT JOIN SA_TPSO TP WITH(NOLOCK) ON TP.CD_COMPANY = SH.CD_COMPANY AND TP.TP_SO = SH.TP_SO

),
C AS
(
    SELECT B.NO_SO,
           B.AM_SO,
           ROW_NUMBER() OVER (PARTITION BY NULL ORDER BY B.AM_SO DESC) AS IDX,
           (ISNULL(B.AM_SO, 0) - (ISNULL(B.AM_PO, 0) + ISNULL(B.AM_STOCK, 0))) AS AM_PROFIT,
           (CASE WHEN ISNULL(B.AM_SO, 0) = 0 THEN 0 
    						   ELSE ROUND(((1 - ((ISNULL(B.AM_PO, 0) + ISNULL(B.AM_STOCK, 0)) / ISNULL(B.AM_SO, 0))) * 100), 2) END) AS RT_PROFIT
    FROM B
)
SELECT C.NO_SO,
       C.AM_PROFIT,
       C.RT_PROFIT
FROM C
WHERE C.IDX = 1 

DROP TABLE #A";
                                    dt1 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dataRow["NO_REF"]));

                                    if (dt1 == null || dt1.Rows.Count == 0)
                                    {
                                        오류메시지 += "유효하지 않은 참조번호 입니다. 참조번호 : " + dataRow["NO_REF"].ToString() + Environment.NewLine;
                                        continue;
                                    }
                                    else
                                    {
                                        dataRow["NO_IO"] = dt1.Rows[0]["NO_SO"].ToString();
                                        dataRow["NM_NOTE"] = ((D.GetDecimal(dt1.Rows[0]["AM_PROFIT"]) < D.GetDecimal(dataRow["AM_CHARGE_MONTH"]) ? "★ " : "") +
                                                              "이윤 : " + D.GetDecimal(dt1.Rows[0]["AM_PROFIT"]).ToString("N0") + 
                                                              "(" + D.GetDecimal(dt1.Rows[0]["RT_PROFIT"]).ToString("N2") + "%) ");
                                    }
                                }
                                #endregion
                            }
							else if (dr["ZONE"].ToString() == "2")
							{
                                #region 도착
                                query = @"SELECT PH.NO_PO 
                                          FROM PU_POH PH WITH(NOLOCK)
                                          WHERE PH.CD_COMPANY = '{0}'
                                          AND PH.NO_PO = '{1}'
                                          UNION
                                          SELECT MC.CD_CC 
                                          FROM MA_CC MC WITH(NOLOCK)
                                          WHERE MC.CD_COMPANY = '{0}'
                                          AND MC.CD_CC = '{1}'";

                                DataTable dt1 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dataRow["NO_REF"]));

                                if (dt1 != null && dt1.Rows.Count > 0)
                                    dataRow["NO_IO"] = dataRow["NO_REF"].ToString();
                                else
								{
                                    query = @"CREATE TABLE #A (NO_VALUE NVARCHAR(20))

INSERT INTO #A
SELECT TRIM(VALUE) 
FROM STRING_SPLIT('{1}', ' ')

;WITH A AS
(
    SELECT SH.NO_SO 
    FROM SA_SOH SH WITH(NOLOCK)
    WHERE SH.CD_COMPANY = '{0}' 
    AND SH.NO_SO IN (SELECT NO_VALUE FROM #A)
    UNION
    SELECT OL.CD_PJT 
    FROM MM_QTIOH OH WITH(NOLOCK)
    JOIN (SELECT OL.CD_COMPANY, OL.NO_IO, OL.CD_PJT
          FROM MM_QTIO OL WITH(NOLOCK)
          WHERE OL.FG_PS = '1'
          GROUP BY OL.CD_COMPANY, OL.NO_IO, OL.CD_PJT) OL
    ON OL.CD_COMPANY = OH.CD_COMPANY AND OL.NO_IO = OH.NO_IO
    WHERE OH.CD_COMPANY = '{0}'
    AND OH.TXT_USERDEF1 LIKE '{1}%'
    GROUP BY OL.CD_PJT
    UNION
    SELECT GL.NO_SO
    FROM CZ_SA_GIRH_PACK GH WITH(NOLOCK)
    JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR, GL.NO_SO 
          FROM CZ_SA_GIRL_PACK GL WITH(NOLOCK)
          GROUP BY GL.CD_COMPANY, GL.NO_GIR, GL.NO_SO) GL
    ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
    WHERE GH.CD_COMPANY = '{0}'
    AND GH.NO_GIR IN (SELECT NO_VALUE FROM #A)
    GROUP BY GL.NO_SO
),
B AS
(
    SELECT SH.NO_SO,
           (CASE WHEN TP.RET = 'Y' THEN -ISNULL(SL.AM_SO, 0) ELSE ISNULL(SL.AM_SO, 0) END) AS AM_SO,
           SB.AM_STOCK,
           PL.AM_PO
    FROM SA_SOH SH WITH(NOLOCK)
    JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
                 SUM(SL.AM_WONAMT) AS AM_SO
          FROM SA_SOL SL WITH(NOLOCK)
          GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
    ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN (SELECT SB.CD_COMPANY, SB.NO_FILE,
    		   	      ISNULL(SUM(SB.UM_KR * SB.QT_STOCK), 0) AS AM_STOCK
    		   FROM CZ_SA_STOCK_BOOK SB WITH(NOLOCK)
    		   GROUP BY SB.CD_COMPANY, SB.NO_FILE) SB
    ON SB.CD_COMPANY = SH.CD_COMPANY AND SB.NO_FILE = SH.NO_SO
    LEFT JOIN (SELECT PH.CD_COMPANY, PH.CD_PJT,
    		   	      ISNULL(SUM(PL.AM), 0) AS AM_PO 
    		   FROM PU_POH PH WITH(NOLOCK)
    		   JOIN PU_POL PL WITH(NOLOCK) ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
    		   WHERE PH.CD_TPPO NOT IN ('1300', '1400', '2300', '2400')
    		   GROUP BY PH.CD_COMPANY, PH.CD_PJT) PL
    ON PL.CD_COMPANY = SH.CD_COMPANY AND PL.CD_PJT = SH.NO_SO
    LEFT JOIN SA_TPSO TP WITH(NOLOCK) ON TP.CD_COMPANY = SH.CD_COMPANY AND TP.TP_SO = SH.TP_SO
    WHERE SH.CD_COMPANY = '{0}'
    AND SH.NO_SO IN (SELECT NO_SO FROM A)
),
C AS
(
    SELECT B.NO_SO,
           (ISNULL(B.AM_SO, 0) - (ISNULL(B.AM_PO, 0) + ISNULL(B.AM_STOCK, 0))) AS AM_PROFIT,
           (CASE WHEN ISNULL(B.AM_SO, 0) = 0 THEN 0 
    										 ELSE ROUND(((1 - ((ISNULL(B.AM_PO, 0) + ISNULL(B.AM_STOCK, 0)) / ISNULL(B.AM_SO, 0))) * 100), 2) END) AS RT_PROFIT
    FROM B
),
D AS
(
    SELECT C.NO_SO, C.AM_PROFIT, C.RT_PROFIT,
           ROW_NUMBER() OVER (PARTITION BY NULL ORDER BY C.AM_PROFIT DESC) AS IDX
    FROM C
)
SELECT D.NO_SO, D.AM_PROFIT, D.RT_PROFIT
FROM D
WHERE D.IDX = 1

DROP TABLE #A";

                                    dt1 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dataRow["NO_REF"]));

                                    if (dt1 == null || dt1.Rows.Count == 0)
                                    {
                                        오류메시지 += "유효하지 않은 수주번호 입니다. 참조번호 : " + dataRow["NO_REF"].ToString() + Environment.NewLine;
                                        continue;
                                    }
                                    else
                                    {
                                        dataRow["NO_IO"] = dt1.Rows[0]["NO_SO"].ToString();
                                        dataRow["NM_NOTE"] = ((D.GetDecimal(dt1.Rows[0]["AM_PROFIT"]) < D.GetDecimal(dataRow["AM_CHARGE_MONTH"]) ? "★ " : "") + 
                                                              "이윤 : " + D.GetDecimal(dt1.Rows[0]["AM_PROFIT"]).ToString("N0") + 
                                                              "(" + D.GetDecimal(dt1.Rows[0]["RT_PROFIT"]).ToString("N2") + "%) ");
                                    }
                                }
                                #endregion
                            }
							else
							{
                                오류메시지 += "발송/도착 구분이 잘못 입력 되었습니다. 참조번호 : " + dataRow["NO_REF"].ToString() + Environment.NewLine;
                                continue;
                            }

                            dataRow["ZONE"] = Convert.ToInt32(dr["ZONE"]);

                            switch (this._flexH["TP_DELIVERY"].ToString())
							{
                                case "TEB":
                                case "TEB1":
                                case "TEB2":
                                case "TEB3":
                                    dataRow["NM_NOTE"] += "택배비";
                                    break;
                                case "QCK":
                                    dataRow["NM_NOTE"] += "퀵서비스";
                                    break;
                                case "TRK":
                                case "TRK1":
                                case "TRK2":
                                    dataRow["NM_NOTE"] += "용차";
                                    break;
                                case "SHIP":
                                case "SP1":
                                    dataRow["NM_NOTE"] += "선적대행";
                                    break;
                                case "BWH":
                                    dataRow["NM_NOTE"] += "보세창고료";
                                    break;
                                case "F2F":
                                    dataRow["NM_NOTE"] += "F2F";
                                    break;
                                case "INB":
                                    dataRow["NM_NOTE"] += "수입";
                                    break;
                                case "AIR":
                                    dataRow["NM_NOTE"] += "항송";
                                    break;
                            }

                            this._flexL.DataTable.Rows.Add(dataRow);
                            #endregion
                        }
                        else if (this._flexH["TP_DELIVERY"].ToString() == "SHIP")
						{
							#region 선적대행
                            if (this._flexH["CD_PARTNER"].ToString() == "07548")
							{
                                query = @"SELECT A.NO_GIR, GL.NO_SO 
FROM (SELECT TRIM(REPLACE(VALUE, CHAR(13), '')) AS NO_GIR
      FROM STRING_SPLIT('{1}', CHAR(10))
      WHERE ISNULL(VALUE, '') != '') A
LEFT JOIN (SELECT GL.NO_GIR, GL.NO_SO 
           FROM SA_GIRL GL WITH(NOLOCK)
           WHERE GL.CD_COMPANY = '{0}'
           GROUP BY GL.NO_GIR, GL.NO_SO
           UNION ALL
           SELECT GL.NO_GIR, GL.NO_SO 
           FROM CZ_SA_GIRL_PACK GL WITH(NOLOCK)
           WHERE GL.CD_COMPANY = '{0}'
           GROUP BY GL.NO_GIR, GL.NO_SO
           UNION ALL
           SELECT SH.NO_SO AS NO_GIR, SH.NO_SO 
           FROM SA_SOH SH WITH(NOLOCK)
           WHERE SH.CD_COMPANY = '{0}') GL
ON GL.NO_GIR = A.NO_GIR
GROUP BY A.NO_GIR, GL.NO_SO";

                                DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr["NO_REF"].ToString()));
                                
                                if (dt == null || dt.Rows.Count == 0 || dt.Select("NO_SO IS NULL").Length > 0)
                                {
                                    오류메시지 += "유효하지 않은 협조전 번호가 존재합니다." + dr["NO_REF"].ToString() + Environment.NewLine;
                                    continue;
                                }

                                int count = 0; 
                                decimal sum = 0;

                                foreach (DataRow dr1 in dt.Rows)
                                {
                                    count++;

                                    dataRow = this._flexL.DataTable.NewRow();

                                    dataRow["CD_PARTNER"] = this._flexH["CD_PARTNER"].ToString();
                                    dataRow["TP_DELIVERY"] = this._flexH["TP_DELIVERY"].ToString();
                                    dataRow["DT_MONTH"] = this._flexH["DT_MONTH"].ToString();
                                    dataRow["SEQ"] = this.SeqMax();

                                    dataRow["NO_BL"] = "";
                                    dataRow["NO_REF"] = dr1["NO_GIR"].ToString();

                                    query = @"CREATE TABLE #A (NO_VALUE NVARCHAR(20))

INSERT INTO #A
SELECT TRIM(VALUE) 
FROM STRING_SPLIT('{1}', ' ')

;WITH A AS
(
    SELECT SH.NO_SO,
           (CASE WHEN TP.RET = 'Y' THEN -ISNULL(SL.AM_SO, 0) ELSE ISNULL(SL.AM_SO, 0) END) AS AM_SO,
           SB.AM_STOCK,
           PL.AM_PO
    FROM SA_SOH SH WITH(NOLOCK)
    JOIN (SELECT SL.CD_COMPANY, SL.NO_SO,
                 SUM(SL.AM_WONAMT) AS AM_SO
          FROM SA_SOL SL WITH(NOLOCK)
          GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
    ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
    LEFT JOIN (SELECT SB.CD_COMPANY, SB.NO_FILE,
    		   	      ISNULL(SUM(SB.UM_KR * SB.QT_STOCK), 0) AS AM_STOCK
    		   FROM CZ_SA_STOCK_BOOK SB WITH(NOLOCK)
    		   GROUP BY SB.CD_COMPANY, SB.NO_FILE) SB
    ON SB.CD_COMPANY = SH.CD_COMPANY AND SB.NO_FILE = SH.NO_SO
    LEFT JOIN (SELECT PH.CD_COMPANY, PH.CD_PJT,
    		   	      ISNULL(SUM(PL.AM), 0) AS AM_PO 
    		   FROM PU_POH PH WITH(NOLOCK)
    		   JOIN PU_POL PL WITH(NOLOCK) ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_PO = PH.NO_PO
    		   WHERE PH.CD_TPPO NOT IN ('1300', '1400', '2300', '2400')
    		   GROUP BY PH.CD_COMPANY, PH.CD_PJT) PL
    ON PL.CD_COMPANY = SH.CD_COMPANY AND PL.CD_PJT = SH.NO_SO
    LEFT JOIN SA_TPSO TP WITH(NOLOCK) ON TP.CD_COMPANY = SH.CD_COMPANY AND TP.TP_SO = SH.TP_SO
    WHERE SH.CD_COMPANY = '{0}'
    AND SH.NO_SO IN (SELECT NO_VALUE FROM #A)
),
B AS
(
    SELECT A.NO_SO,
           (ISNULL(A.AM_SO, 0) - (ISNULL(A.AM_PO, 0) + ISNULL(A.AM_STOCK, 0))) AS AM_PROFIT,
           (CASE WHEN ISNULL(A.AM_SO, 0) = 0 THEN 0 
    										 ELSE ROUND(((1 - ((ISNULL(A.AM_PO, 0) + ISNULL(A.AM_STOCK, 0)) / ISNULL(A.AM_SO, 0))) * 100), 2) END) AS RT_PROFIT
    FROM A
),
C AS
(
    SELECT B.NO_SO, B.AM_PROFIT, B.RT_PROFIT,
           ROW_NUMBER() OVER (PARTITION BY NULL ORDER BY B.AM_PROFIT DESC) AS IDX
    FROM B
)
SELECT C.NO_SO, C.AM_PROFIT, C.RT_PROFIT
FROM C
WHERE C.IDX = 1

DROP TABLE #A";

                                    DataTable dt1 = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, dr1["NO_SO"].ToString()));

                                    dataRow["NO_IO"] = dt1.Rows[0]["NO_SO"].ToString();
                                    
                                    dataRow["AM_CHARGE_MONTH"] = (dr["AM_CHARGE"].ToString() == string.Empty ? 0 : Decimal.Round((D.GetDecimal(dr["AM_CHARGE"]) / dt.Rows.Count), 0, MidpointRounding.AwayFromZero));

                                    sum += D.GetInt(dataRow["AM_CHARGE_MONTH"]);

                                    if (count == dt.Rows.Count)
                                        dataRow["AM_CHARGE_MONTH"] = D.GetDecimal(dataRow["AM_CHARGE_MONTH"]) + (D.GetDecimal(dr["AM_CHARGE"]) - sum);
									    
                                    dataRow["AM_MONTH"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                                    dataRow["AM_VAT_TARGET"] = Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]);
                                    dataRow["AM_VAT_MONTH"] = Decimal.Round(Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]) * Convert.ToDecimal(0.1), 0, MidpointRounding.AwayFromZero);

                                    dataRow["NM_NOTE"] = ((D.GetDecimal(dt1.Rows[0]["AM_PROFIT"]) < D.GetDecimal(dataRow["AM_CHARGE_MONTH"]) ? "★ " : "") +
                                                          "이윤 : " + D.GetDecimal(dt1.Rows[0]["AM_PROFIT"]).ToString("N0") +
                                                          "(" + D.GetDecimal(dt1.Rows[0]["RT_PROFIT"]).ToString("N2") + "%) ");

                                    dataRow["ZONE"] = 1;

                                    dataRow["NM_NOTE"] += "선적대행";

                                    this._flexL.DataTable.Rows.Add(dataRow);
                                }
                            }
							#endregion
						}
						else if (this._flexH["TP_DELIVERY"].ToString() == "ETC")
                        {
                            if (this._flexH["CD_PARTNER"].ToString() == "05112")
							{
                                #region 통관비
                                query = @"SELECT FC.NO_IO 
                                          FROM CZ_SA_FORWARD_CHARGE_L FC WITH(NOLOCK)
                                          WHERE FC.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                         "AND FC.NO_IO = '" + dr["NO_REF"].ToString() + "'";

                                DataTable dt = DBHelper.GetDataTable(query);
                                decimal amTaxMonth = dtExcel.Compute("SUM(AM_TAX_MONTH)", "NO_REF = '" + dr["NO_REF"].ToString() + "'").ToDecimal();

                                if (dt != null && dt.Rows.Count == 1)
                                {
									query = @"UPDATE FC
                                              SET FC.AM_TAX_MONTH = {0},
                                                  FC.DT_TAX_MONTH = '{1}',
                                                  FC.SEQ_TAX_MONTH = {4}
                                              FROM CZ_SA_FORWARD_CHARGE_L FC WITH(NOLOCK)
                                              WHERE FC.CD_COMPANY = '{2}'
                                              AND FC.NO_IO = '{3}'";

									DBHelper.ExecuteScalar(string.Format(query, amTaxMonth,
																				this.dtp정산년월.Text,
																				Global.MainFrame.LoginInfo.CompanyCode,
																				dr["NO_REF"].ToString(),
																				index));
								}
                                else
                                {
                                    dataRow = this._flexL.DataTable.NewRow();

                                    dataRow["CD_PARTNER"] = this._flexH["CD_PARTNER"].ToString();
                                    dataRow["TP_DELIVERY"] = this._flexH["TP_DELIVERY"].ToString();
                                    dataRow["DT_MONTH"] = this._flexH["DT_MONTH"].ToString();
                                    dataRow["SEQ"] = this.SeqMax();

                                    dataRow["NO_BL"] = dr["NO_BL"].ToString();
                                    dataRow["NO_REF"] = dr["NO_REF"].ToString();
                                    dataRow["NO_IO"] = dr["NO_REF"].ToString();

                                    if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
                                        dataRow["DT_FORWARD"] = dr["DT_FORWARD"].ToString();
                                    else
                                        dataRow["DT_FORWARD"] = Convert.ToDateTime(dr["DT_FORWARD"]).ToString("yyyyMMdd");

                                    dataRow["AM_TAX_MONTH"] = amTaxMonth;
                                    dataRow["DT_TAX_MONTH"] = this.dtp정산년월.Text;
                                    dataRow["SEQ_TAX_MONTH"] = index;

                                    if (dt != null && dt.Rows.Count > 1)
                                        dataRow["DC_RMK"] = "여러건존재";
                                    else
                                        dataRow["DC_RMK"] = "해당건 없음";

                                    this._flexL.DataTable.Rows.Add(dataRow);

                                }
                                #endregion
                            }
                        }
                        else if (this._flexH["CD_PARTNER"].ToString() == "01107")
						{
                            if (dr["NM_NOTE"].ToString() == "서류" || 
                                dr["NM_NOTE"].ToString() == "선물")
							{
                                #region DHL 서류, 선물
                                dataRow = this._flexL.DataTable.NewRow();

                                dataRow["CD_PARTNER"] = this._flexH["CD_PARTNER"].ToString();
                                dataRow["TP_DELIVERY"] = this._flexH["TP_DELIVERY"].ToString();
                                dataRow["DT_MONTH"] = this._flexH["DT_MONTH"].ToString();
                                dataRow["SEQ"] = this.SeqMax();

                                dataRow["NO_BL"] = dr["NO_BL"].ToString();
                                dataRow["NO_REF"] = dr["NO_REF"].ToString();
                                dataRow["NO_IO"] = dr["NO_REF"].ToString();

                                dataRow["DT_FORWARD"] = Convert.ToDateTime(dr["DT_FORWARD"]).ToString("yyyyMMdd");

                                dataRow["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_CHARGE1"].ToString() == string.Empty ? 0 : dr["AM_CHARGE1"]);
                                dataRow["AM_ETC_MONTH"] = (Convert.ToDecimal(dr["AM_CHARGE3"].ToString() == string.Empty ? 0 : dr["AM_CHARGE3"])
                                                           + Convert.ToDecimal(dr["AM_CHARGE2"].ToString() == string.Empty ? 0 : dr["AM_CHARGE2"])
                                                           + Convert.ToDecimal(dr["AM_CHARGE4"].ToString() == string.Empty ? 0 : dr["AM_CHARGE4"])
                                                           + Convert.ToDecimal(dr["AM_CHARGE5"].ToString() == string.Empty ? 0 : dr["AM_CHARGE5"])
                                                           + Convert.ToDecimal(dr["AM_CHARGE6"].ToString() == string.Empty ? 0 : dr["AM_CHARGE6"])
                                                           - Convert.ToDecimal(dr["AM_DISCOUNT1"].ToString() == string.Empty ? 0 : dr["AM_DISCOUNT1"])
                                                           - Convert.ToDecimal(dr["AM_DISCOUNT2"].ToString() == string.Empty ? 0 : dr["AM_DISCOUNT2"]));
                                dataRow["AM_VAT_MONTH"] = 0;
                                dataRow["AM_MONTH"] = Convert.ToDecimal(dr["AM_TOTAL"]);

                                if (Convert.ToDecimal(dataRow["AM_CHARGE_MONTH"]) + Convert.ToDecimal(dataRow["AM_ETC_MONTH"]) != Convert.ToDecimal(dataRow["AM_MONTH"]))
                                {
                                    오류메시지 += "운임 및 기타금액 합계와 총금액이 일치하지 않습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                    continue;
                                }

                                dataRow["NM_NOTE"] = dr["NM_NOTE"].ToString();

                                this._flexL.DataTable.Rows.Add(dataRow);
                                #endregion
                            }
                            else
							{
                                #region DHL 항송
                                dataRowArray = this._flexL.DataTable.Select("CD_PARTNER = '" + this._flexH["CD_PARTNER"].ToString() +
                                                                            "' AND TP_DELIVERY = '" + this._flexH["TP_DELIVERY"].ToString() +
                                                                            "' AND DT_MONTH = '" + this._flexH["DT_MONTH"].ToString() +
                                                                            "' AND NO_BL = '" + dr["NO_BL"].ToString() +
                                                                            "' AND NO_REF = '" + dr["NO_REF"].ToString().Replace("'", "''") + "'");

                                if (dataRowArray == null || dataRowArray.Length == 0)
                                {
                                    오류메시지 += "해당하는 건을 찾을수 없습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                    continue;
                                }
                                else if (dataRowArray.Length > 1)
                                {
                                    오류메시지 += "해당하는 건이 여러건 있습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                    continue;
                                }

                                dataRowArray[0]["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_CHARGE1"].ToString() == string.Empty ? 0 : dr["AM_CHARGE1"]);
                                dataRowArray[0]["AM_ETC_MONTH"] = (Convert.ToDecimal(dr["AM_CHARGE3"].ToString() == string.Empty ? 0 : dr["AM_CHARGE3"])
                                                                   + Convert.ToDecimal(dr["AM_CHARGE2"].ToString() == string.Empty ? 0 : dr["AM_CHARGE2"])
                                                                   + Convert.ToDecimal(dr["AM_CHARGE4"].ToString() == string.Empty ? 0 : dr["AM_CHARGE4"])
                                                                   + Convert.ToDecimal(dr["AM_CHARGE5"].ToString() == string.Empty ? 0 : dr["AM_CHARGE5"])
                                                                   + Convert.ToDecimal(dr["AM_CHARGE6"].ToString() == string.Empty ? 0 : dr["AM_CHARGE6"])
                                                                   - Convert.ToDecimal(dr["AM_DISCOUNT1"].ToString() == string.Empty ? 0 : dr["AM_DISCOUNT1"])
                                                                   - Convert.ToDecimal(dr["AM_DISCOUNT2"].ToString() == string.Empty ? 0 : dr["AM_DISCOUNT2"]));
                                dataRowArray[0]["AM_VAT_MONTH"] = 0;
                                dataRowArray[0]["AM_MONTH"] = Convert.ToDecimal(dr["AM_TOTAL"]);

                                if (Convert.ToDecimal(dataRowArray[0]["AM_CHARGE_MONTH"]) + Convert.ToDecimal(dataRowArray[0]["AM_ETC_MONTH"]) != Convert.ToDecimal(dataRowArray[0]["AM_MONTH"]))
                                {
                                    오류메시지 += "운임 및 기타금액 합계와 총금액이 일치하지 않습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                    continue;
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            dataRowArray = this._flexL.DataTable.Select("CD_PARTNER = '" + this._flexH["CD_PARTNER"].ToString() +
                                                                        "' AND TP_DELIVERY = '" + this._flexH["TP_DELIVERY"].ToString() +
                                                                        "' AND DT_MONTH = '" + this._flexH["DT_MONTH"].ToString() +
                                                                        "' AND NO_BL = '" + dr["NO_BL"].ToString().Trim() +
                                                                        "' AND NO_REF = '" + dr["NO_REF"].ToString().Trim() + "'");

                            if (dataRowArray == null || dataRowArray.Length == 0)
                            {
                                오류메시지 += "해당하는 건을 찾을수 없습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                continue;
                            }
                            else if (dataRowArray.Length > 1)
                            {
                                오류메시지 += "해당하는 건이 여러건 있습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                continue;
                            }
                            else
                            {
                                if (this._flexH["CD_PARTNER"].ToString() == "08624")
                                {
                                    dr["AM_VAT"] = Math.Round(Convert.ToDecimal((DBNull.Value.Equals(dr["AM_VAT"]) || dr["AM_VAT"].ToString() == "-") ? 0 : dr["AM_VAT"]), MidpointRounding.AwayFromZero);
                                    dr["AM_TOTAL"] = Math.Round(Convert.ToDecimal(dr["AM_TOTAL"]), MidpointRounding.AwayFromZero);

                                    if (this._flexH["TP_DELIVERY"].ToString() == "AIR")
                                    {
                                        #region TPO 항송
                                        dataRowArray[0]["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_AF"]);
                                        dataRowArray[0]["AM_ETC_MONTH"] = Convert.ToDecimal((DBNull.Value.Equals(dr["AM_FC"]) || dr["AM_FC"].ToString() == "-") ? 0 : dr["AM_FC"]) +
                                                                          Convert.ToDecimal((DBNull.Value.Equals(dr["AM_SC"]) || dr["AM_SC"].ToString() == "-") ? 0 : dr["AM_SC"]) +
                                                                          Convert.ToDecimal((DBNull.Value.Equals(dr["AM_PICKUP"]) || dr["AM_PICKUP"].ToString() == "-") ? 0 : dr["AM_PICKUP"]) +
                                                                          Convert.ToDecimal((DBNull.Value.Equals(dr["AM_HANDLING"]) || dr["AM_HANDLING"].ToString() == "-") ? 0 : dr["AM_HANDLING"]);

                                        dataRowArray[0]["AM_VAT_MONTH"] = Convert.ToDecimal(dr["AM_VAT"]);
                                        dataRowArray[0]["AM_VAT_TARGET"] = Convert.ToDecimal((DBNull.Value.Equals(dr["AM_HANDLING"]) || dr["AM_HANDLING"].ToString() == "-") ? 0 : dr["AM_HANDLING"]);
                                        dataRowArray[0]["AM_MONTH"] = Convert.ToDecimal(dr["AM_TOTAL"]) - Convert.ToDecimal(dr["AM_VAT"]);

                                        if (Convert.ToDecimal(dataRowArray[0]["AM_CHARGE_MONTH"]) + Convert.ToDecimal(dataRowArray[0]["AM_ETC_MONTH"]) != Convert.ToDecimal(dataRowArray[0]["AM_MONTH"]))
                                        {
                                            오류메시지 += "운임 및 기타금액 합계와 총금액이 일치하지 않습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                            continue;
                                        }
                                        #endregion
                                    }
                                    else if (this._flexH["TP_DELIVERY"].ToString() == "SEA")
                                    {
                                        #region TPO 해송
                                        dataRowArray[0]["AM_CHARGE_MONTH"] = Convert.ToDecimal((DBNull.Value.Equals(dr["AM_OF"]) || dr["AM_OF"].ToString() == "-") ? 0 : dr["AM_OF"]);
                                        dataRowArray[0]["AM_ETC_MONTH"] = Convert.ToDecimal((DBNull.Value.Equals(dr["AM_WFG"]) || dr["AM_WFG"].ToString() == "-") ? 0 : dr["AM_WFG"]) +
                                                                          Convert.ToDecimal((DBNull.Value.Equals(dr["AM_THC"]) || dr["AM_THC"].ToString() == "-") ? 0 : dr["AM_THC"]) +
                                                                          Convert.ToDecimal((DBNull.Value.Equals(dr["AM_CFS"]) || dr["AM_CFS"].ToString() == "-") ? 0 : dr["AM_CFS"]) +
                                                                          Convert.ToDecimal((DBNull.Value.Equals(dr["AM_DOC"]) || dr["AM_DOC"].ToString() == "-") ? 0 : dr["AM_DOC"]) +
                                                                          Convert.ToDecimal((DBNull.Value.Equals(dr["AM_OTHER"]) || dr["AM_OTHER"].ToString() == "-") ? 0 : dr["AM_OTHER"]) +
                                                                          Convert.ToDecimal((DBNull.Value.Equals(dr["AM_OTHER1"]) || dr["AM_OTHER1"].ToString() == "-") ? 0 : dr["AM_OTHER1"]);

                                        dataRowArray[0]["AM_WFG"] = Convert.ToDecimal((DBNull.Value.Equals(dr["AM_WFG"]) || dr["AM_WFG"].ToString() == "-") ? 0 : dr["AM_WFG"]);

                                        dataRowArray[0]["AM_VAT_MONTH"] = Convert.ToDecimal(dr["AM_VAT"]);
                                        dataRowArray[0]["AM_VAT_TARGET"] = Convert.ToDecimal((DBNull.Value.Equals(dr["AM_OTHER"]) || dr["AM_OTHER"].ToString() == "-") ? 0 : dr["AM_OTHER"]);
                                        dataRowArray[0]["AM_MONTH"] = Convert.ToDecimal(dr["AM_TOTAL"]) - Convert.ToDecimal(dr["AM_VAT"]);

                                        if (Convert.ToDecimal(dataRowArray[0]["AM_CHARGE_MONTH"]) + Convert.ToDecimal(dataRowArray[0]["AM_ETC_MONTH"]) != Convert.ToDecimal(dataRowArray[0]["AM_MONTH"]))
                                        {
                                            오류메시지 += "운임 및 기타금액 합계와 총금액이 일치하지 않습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                            continue;
                                        }
                                        #endregion
                                    }
                                }
                                else if (this._flexH["CD_PARTNER"].ToString() == "02436")
                                {
                                    dr["AM_VAT"] = Math.Round(Convert.ToDecimal(dr["AM_VAT"]), MidpointRounding.AwayFromZero);

                                    if (this._flexH["TP_DELIVERY"].ToString() == "AIR")
                                    {
                                        #region 쉥커 항송
                                        dataRowArray[0]["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_AF"]);
                                        dataRowArray[0]["AM_ETC_MONTH"] = Convert.ToDecimal(dr["AM_SECURITY"].ToString() == string.Empty ? 0 : dr["AM_SECURITY"]) +
                                                                          Convert.ToDecimal(dr["AM_FS"].ToString() == string.Empty ? 0 : dr["AM_FS"]) +
                                                                          Convert.ToDecimal(dr["AM_HC"].ToString() == string.Empty ? 0 : dr["AM_HC"]) +
                                                                          Convert.ToDecimal(dr["AM_PUP"].ToString() == string.Empty ? 0 : dr["AM_PUP"]) +
                                                                          Convert.ToDecimal(dr["AM_DG"].ToString() == string.Empty ? 0 : dr["AM_DG"]) +
                                                                          Convert.ToDecimal(dr["AM_OTHER"].ToString() == string.Empty ? 0 : dr["AM_OTHER"]);

                                        dataRowArray[0]["AM_VAT_MONTH"] = Convert.ToDecimal(dr["AM_VAT"]);
                                        dataRowArray[0]["AM_VAT_TARGET"] = Convert.ToDecimal(dr["AM_HC"].ToString() == string.Empty ? 0 : dr["AM_HC"]) +
                                                                           Convert.ToDecimal(dr["AM_PUP"].ToString() == string.Empty ? 0 : dr["AM_PUP"]) +
                                                                           Convert.ToDecimal(dr["AM_DG"].ToString() == string.Empty ? 0 : dr["AM_DG"]);
                                        dataRowArray[0]["AM_MONTH"] = Convert.ToDecimal(dr["AM_TOTAL"]);

                                        if (Convert.ToDecimal(dataRowArray[0]["AM_CHARGE_MONTH"]) + Convert.ToDecimal(dataRowArray[0]["AM_ETC_MONTH"]) != Convert.ToDecimal(dataRowArray[0]["AM_MONTH"]))
                                        {
                                            오류메시지 += "운임 및 기타금액 합계와 총금액이 일치하지 않습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                            continue;
                                        }
                                        #endregion
                                    }
                                    else if (this._flexH["TP_DELIVERY"].ToString() == "SEA")
                                    {
                                        #region 쉥커 해송
                                        dataRowArray[0]["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_OF"]);
                                        dataRowArray[0]["AM_ETC_MONTH"] = (Convert.ToDecimal(dr["AM_TOTAL"]) - Convert.ToDecimal(dr["AM_VAT"])) - Convert.ToDecimal(dr["AM_OF"]);

                                        dataRowArray[0]["AM_WFG"] = Convert.ToDecimal(dr["AM_WFG"]);

                                        dataRowArray[0]["AM_VAT_MONTH"] = Convert.ToDecimal(dr["AM_VAT"]);
                                        dataRowArray[0]["AM_VAT_TARGET"] = Convert.ToDecimal(dr["AM_VAT_TARGET"]);
                                        dataRowArray[0]["AM_MONTH"] = Convert.ToDecimal(dr["AM_TOTAL"]) - Convert.ToDecimal(dr["AM_VAT"]);

                                        if (Convert.ToDecimal(dataRowArray[0]["AM_CHARGE_MONTH"]) + Convert.ToDecimal(dataRowArray[0]["AM_ETC_MONTH"]) != Convert.ToDecimal(dataRowArray[0]["AM_MONTH"]))
                                        {
                                            오류메시지 += "운임 및 기타금액 합계와 총금액이 일치하지 않습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                            continue;
                                        }
                                        #endregion
                                    }
                                }
                                else if (this._flexH["CD_PARTNER"].ToString() == "15464")
                                {
                                    if (this._flexH["TP_DELIVERY"].ToString() == "AIR")
                                    {
                                        #region SR 항송
                                        dataRowArray[0]["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_AF"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                        decimal amEtc = 0;

                                        if (dr["AM_FSC"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_FSC"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_CGC"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_CGC"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_CUSTOMS"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_CUSTOMS"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_PICKUP"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_PICKUP"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_HANDLING"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_HANDLING"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                        dataRowArray[0]["AM_ETC_MONTH"] = amEtc;

                                        if (string.IsNullOrEmpty(dr["AM_VAT"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty)))
                                            dataRowArray[0]["AM_VAT_MONTH"] = 0;
                                        else
                                            dataRowArray[0]["AM_VAT_MONTH"] = Convert.ToDecimal(dr["AM_VAT"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                        decimal vatTarget = 0;

                                        if (dr["AM_PICKUP"].ToString() != string.Empty)
                                            vatTarget += Convert.ToDecimal(dr["AM_PICKUP"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_HANDLING"].ToString() != string.Empty)
                                            vatTarget += Convert.ToDecimal(dr["AM_HANDLING"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                        dataRowArray[0]["AM_VAT_TARGET"] = vatTarget;
                                        dataRowArray[0]["AM_MONTH"] = Convert.ToDecimal(dr["AM_TOTAL"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty)) - Convert.ToDecimal(dataRowArray[0]["AM_VAT_MONTH"]);

                                        if (Convert.ToDecimal(dataRowArray[0]["AM_CHARGE_MONTH"]) + Convert.ToDecimal(dataRowArray[0]["AM_ETC_MONTH"]) != Convert.ToDecimal(dataRowArray[0]["AM_MONTH"]))
                                        {
                                            오류메시지 += "운임 및 기타금액 합계와 총금액이 일치하지 않습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                            continue;
                                        }
                                        #endregion
                                    }
                                    else if (this._flexH["TP_DELIVERY"].ToString() == "SEA")
                                    {
                                        #region SR 해송
                                        dataRowArray[0]["AM_CHARGE_MONTH"] = Convert.ToDecimal(dr["AM_OF"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                        decimal amEtc = 0;

                                        if (dr["AM_CFS"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_CFS"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_THC"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_THC"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_DRG"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_DRG"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_WFG_PFS"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_WFG_PFS"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_DOC"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_DOC"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_PICKUP"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_PICKUP"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_HANDLING"].ToString() != string.Empty)
                                            amEtc += Convert.ToDecimal(dr["AM_HANDLING"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                        dataRowArray[0]["AM_ETC_MONTH"] = amEtc;

                                        dataRowArray[0]["AM_VAT_MONTH"] = Convert.ToDecimal(dr["AM_VAT"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                        decimal vatTarget = 0;

                                        if (dr["AM_DRG"].ToString() != string.Empty)
                                            vatTarget += Convert.ToDecimal(dr["AM_DRG"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));
                                        if (dr["AM_HANDLING"].ToString() != string.Empty)
                                            vatTarget += Convert.ToDecimal(dr["AM_HANDLING"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty));

                                        dataRowArray[0]["AM_VAT_TARGET"] = vatTarget;

                                        dataRowArray[0]["AM_MONTH"] = Convert.ToDecimal(dr["AM_TOTAL"].ToString().Replace(",", string.Empty).Replace("₩", string.Empty)) - Convert.ToDecimal(dataRowArray[0]["AM_VAT_MONTH"]);

                                        if (Convert.ToDecimal(dataRowArray[0]["AM_CHARGE_MONTH"]) + Convert.ToDecimal(dataRowArray[0]["AM_ETC_MONTH"]) != Convert.ToDecimal(dataRowArray[0]["AM_MONTH"]))
                                        {
                                            오류메시지 += "운임 및 기타금액 합계와 총금액이 일치하지 않습니다. B/L번호 : " + dr["NO_BL"].ToString() + " 참조번호 : " + dr["NO_REF"].ToString() + Environment.NewLine;
                                            continue;
                                        }
                                        #endregion
                                    }
                                }

                                dataRowArray[0]["AM_MONTH_TOTAL"] = (Convert.ToDecimal(dataRowArray[0]["AM_VAT_MONTH"]) + Convert.ToDecimal(dataRowArray[0]["AM_MONTH"]));
                            }
                        }   
                    }

                    if (dtExcel.Columns.Contains("NO_IO"))
					{
                        if (!string.IsNullOrEmpty(여러건존재))
						{
                            this.ShowDetailMessage("해당출고번호가 여러개 존재 합니다. [더보기] 버튼을 눌러서 확인 하세요.", 여러건존재);
                        }

                        if (!string.IsNullOrEmpty(해당건없음))
                        {
                            this.ShowDetailMessage("해당하는 건을 찾을수 없습니다. [더보기] 버튼을 눌러서 확인 하세요.", 해당건없음);
                        }
                    }
                    #endregion
                }

                if (!string.IsNullOrEmpty(오류메시지))
				{
                    this.ShowDetailMessage("엑셀업로드 중 오류가 발생 했습니다. [더보기] 버튼을 눌러서 확인 하세요.", 오류메시지);
                }

                MsgControl.CloseMsg();

                if (this._flexL.HasNormalRow)
                {
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                    this.ToolBarSaveButtonEnabled = true;
                }
                else
                {
                    this.ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                }

                this._flexL.Redraw = true;
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexL.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        private void Btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
            string localPath = string.Empty, 
                   serverPath = string.Empty;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                if (((Control)sender).Name == this.btn엑셀양식다운로드.Name)
                {
                    if (this._flexH["CD_PARTNER"].ToString() == "01107" &&
                        this._flexH["TP_DELIVERY"].ToString() == "AIR")
                    {
                        #region DHL 항송
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_DHL_항송_" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_DHL.xlsx";
                        #endregion
                    }
                    else if (this._flexH["CD_PARTNER"].ToString() == "08624" &&
                             this._flexH["TP_DELIVERY"].ToString() == "AIR")
                    {
                        #region TPO 항송
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_TPO_항송_" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TPO_AIR.xlsx";
                        #endregion
                    }
                    else if (this._flexH["CD_PARTNER"].ToString() == "08624" &&
                             this._flexH["TP_DELIVERY"].ToString() == "SEA")
                    {
                        #region TPO 해송
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_TPO_해송_" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TPO_SEA.xlsx";
                        #endregion
                    }
                    else if (this._flexH["CD_PARTNER"].ToString() == "02436" &&
                             this._flexH["TP_DELIVERY"].ToString() == "AIR")
                    {
                        #region 쉥커 항송
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_쉥커_항송" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_SKR_AIR.xlsx";
                        #endregion
                    }
                    else if (this._flexH["CD_PARTNER"].ToString() == "02436" &&
                             this._flexH["TP_DELIVERY"].ToString() == "SEA")
                    {
                        #region 쉥커 해송
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_쉥커_해송" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_SKR_SEA.xlsx";
                        #endregion
                    }
                    else if (this._flexH["CD_PARTNER"].ToString() == "15464" &&
                             this._flexH["TP_DELIVERY"].ToString() == "AIR")
                    {
                        #region SR 항송
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_SR_항송" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_SR_AIR.xlsx";
                        #endregion
                    }
                    else if (this._flexH["CD_PARTNER"].ToString() == "15464" &&
                             this._flexH["TP_DELIVERY"].ToString() == "SEA")
                    {
                        #region SR 해송
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_SR_해송" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_SR_SEA.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "TEB")
                    {
                        if (this._flexH["CD_PARTNER"].ToString() == "02436")
						{
                            #region 쉥커 픽업
                            localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_쉥커_픽업" + Global.MainFrame.GetStringToday + ".xlsx";
                            serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_SKR_TEB.xlsx";
                            #endregion
                        }
                        else
						{
                            #region 택배 - 월초
                            localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_택배-월초" + Global.MainFrame.GetStringToday + ".xlsx";
                            serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                            #endregion
                        }
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "TEB1")
					{
                        #region 택배 - 단건(출고)
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_택배-단건" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
					else if (this._flexH["TP_DELIVERY"].ToString() == "TEB2")
					{
                        #region 택배 - 월말
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_택배-월말" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "TEB3")
                    {
                        #region 택배 - 단건(입고)
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_택배-월말" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "QCK")
                    {
                        #region 퀵서비스
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_퀵서비스" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "TRK")
                    {
                        #region 용차-월말
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_용차_월말" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "TRK1")
                    {
                        #region 용차-단건(출고)
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_용차_단건_출고" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "TRK2")
                    {
                        #region 용차-단건(입고)
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_용차_단건_입고" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "SHIP")
					{
                        #region 선적대행-월말
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_선적대행_월말" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "SP1")
                    {
                        #region 선적대행-단건
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_선적대행_단건" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "BWH")
                    {
                        #region 보세창고료
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_보세창고료" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "F2F")
                    {
                        #region F2F
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_F2F" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["TP_DELIVERY"].ToString() == "INB")
                    {
                        #region 수입
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_수입" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }
                    else if (this._flexH["CD_PARTNER"].ToString() == "12448" &&
                             this._flexH["TP_DELIVERY"].ToString() == "AIR")
					{
                        #region FEDEX 항송
                        localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_FEDEX_항송" + Global.MainFrame.GetStringToday + ".xlsx";
                        serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TEB.xlsx";
                        #endregion
                    }

                    if (string.IsNullOrEmpty(serverPath) || string.IsNullOrEmpty(localPath))
                    {
                        this.ShowMessage("엑셀양식이 없는 포워더 입니다.");
                        return;
                    }
                }
                else
                {
                    #region 통관비
                    localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_포워딩비용관리_통관비" + Global.MainFrame.GetStringToday + ".xlsx";
                    serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_FORWARD_CHARGE_MNG_TAX.xlsx";
                    #endregion
                }

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                this.ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.");
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void Btn추가_Click(object sender, EventArgs e)
        {
            try
            {
                this.btn추가.Enabled = false;

                this._flexL.Rows.Add();
                this._flexL.Row = this._flexL.Rows.Count - 1;

                this._flexL["CD_PARTNER"] = this._flexH["CD_PARTNER"].ToString();
                this._flexL["TP_DELIVERY"] = this._flexH["TP_DELIVERY"].ToString();
                this._flexL["DT_MONTH"] = this._flexH["DT_MONTH"].ToString();
                this._flexL["SEQ"] = this.SeqMax();

                if (this._flexH["TP_DELIVERY"].ToString() == "BWH" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TRK2" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TEB3" ||
                    this._flexH["TP_DELIVERY"].ToString() == "INB")
				{
                    this._flexL["NO_BL"] = string.Empty;
                    this._flexL["ZONE"] = 2;
                }
                else if (this._flexH["TP_DELIVERY"].ToString() == "SP1" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TRK1" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TEB1" ||
                         this._flexH["TP_DELIVERY"].ToString() == "F2F" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TEB" ||
                         this._flexH["TP_DELIVERY"].ToString() == "TEB2" ||
                         (this._flexH["TP_DELIVERY"].ToString() == "AIR" && this._flexH["CD_PARTNER"].ToString() == "12448"))
				{
                    this._flexL["NO_BL"] = string.Empty;
                    this._flexL["ZONE"] = 1;
                }
                
                this._flexL.Col = this._flexL.Cols.Fixed;
                this._flexL.AddFinished();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
			{
                this.btn추가.Enabled = true;
			}
        }

        private void Btn제거_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (this.ShowMessage("선택된 데이터를 삭제 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

                if (dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    this._flexL.Redraw = false;

                    foreach (DataRow dr in dataRowArray)
                    {
                        dr.Delete();
                    }

                    this._flexL.Redraw = true;
                }

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn제거.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexL.Redraw = true;
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, key1, key2, filter;

            try
            {
                dt = null;
                key = this._flexH["CD_PARTNER"].ToString();
                key1 = this._flexH["TP_DELIVERY"].ToString();
                key2 = this._flexH["DT_MONTH"].ToString();
                filter = "CD_PARTNER = '" + key + "' AND TP_DELIVERY = '" + key1 + "' AND DT_MONTH = '" + key2 + "'";

                if (this._flexH["TP_DELIVERY"].ToString() == "TEB" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TEB2" ||
                    this._flexH["TP_DELIVERY"].ToString() == "QCK" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TRK" ||
                    this._flexH["TP_DELIVERY"].ToString() == "F2F" ||
                    this._flexH["TP_DELIVERY"].ToString() == "BWH" ||
                    this._flexH["TP_DELIVERY"].ToString() == "SP1" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TRK1" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TRK2" ||
                    this._flexH["TP_DELIVERY"].ToString() == "INB" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TEB1" ||
                    this._flexH["TP_DELIVERY"].ToString() == "TEB3" ||
                    (this._flexH["TP_DELIVERY"].ToString() == "AIR" && this._flexH["CD_PARTNER"].ToString() == "12448"))
                {
                    this._flexL.Cols["AM_CHARGE_MONTH"].AllowEditing = true;
                    this._flexL.Cols["ZONE"].AllowEditing = true;
                }
                else
				{
                    this._flexL.Cols["AM_CHARGE_MONTH"].AllowEditing = false;
                    this._flexL.Cols["ZONE"].AllowEditing = false;
                }

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchLine(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                             key,
                                                             key1,
                                                             key2,
                                                             this.txt출고번호.Text });
                }

                this._flexL.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!BeforeSearch()) return;

                this._flexH.Binding = this._biz.SearchHeader(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                            this.ctx포워더.CodeValue,
                                                                            this.cbo운송구분.SelectedValue.ToString(),
                                                                            this.dtp정산년월.Text,
                                                                            this.txt출고번호.Text });

                if (!this._flexH.HasNormalRow)
                {
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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

                if (!BeforeAdd()) return;

                this._flexH.Rows.Add();
                this._flexH.Row = _flexH.Rows.Count - 1;

                this._flexH.AddFinished();
                this._flexH.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flexH.HasNormalRow) return;

                if (this._flexL.DataTable.Select("CD_PARTNER = '" + this._flexH["CD_PARTNER"].ToString() +
                                                 "' AND TP_DELIVERY = '" + this._flexH["TP_DELIVERY"].ToString() +
                                                 "' AND DT_MONTH = '" + this._flexH["DT_MONTH"].ToString() + "'").Length > 0)
                {
                    this.ShowMessage("선택한 포워더에 해당하는 데이터가 존재 합니다.");
                    return;
                }

                this._flexH.Rows.Remove(this._flexH.Row);
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
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flexH.IsDataChanged == false && this._flexL.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flexH.GetChanges(), this._flexL.GetChanges())) return false;

            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();

            return true;
        }

        private Decimal SeqMax()
        {
            Decimal num = 1, num1 = 1;
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(SEQ) AS SEQ 
                                                          FROM CZ_SA_FORWARD_CHARGE_L WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                         "AND CD_PARTNER = '" + this._flexH["CD_PARTNER"].ToString() + "'" + Environment.NewLine +
                                                         "AND TP_DELIVERY = '" + this._flexH["TP_DELIVERY"].ToString() + "'" + Environment.NewLine +
                                                         "AND DT_MONTH = '" + this._flexH["DT_MONTH"].ToString() + "'");

            if (dataTable != null && dataTable.Rows.Count != 0)
                num = (D.GetDecimal(dataTable.Rows[0]["SEQ"]) + 1);

            num1 = D.GetDecimal(this._flexL.DataTable.Compute("MAX(SEQ)", "CD_PARTNER = '" + this._flexH["CD_PARTNER"].ToString() + "'" + Environment.NewLine + 
                                                                          "AND TP_DELIVERY = '" + this._flexH["TP_DELIVERY"].ToString() + "'" + Environment.NewLine +
                                                                          "AND DT_MONTH = '" + this._flexH["DT_MONTH"].ToString() + "'")) + 1;

            if (num <= num1)
                num = num1;

            return num;
        }

        private void _flexL_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            FlexGrid grid = (FlexGrid)sender;

            if (e.Row < grid.Rows.Fixed || e.Col < grid.Cols.Fixed)
                return;


            if (grid[e.Row, "TP_DELIVERY"].ToString() == "ETC" || 
                grid[e.Row, "TP_DELIVERY"].ToString() == "TEB" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "TEB2" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "QCK" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "TRK" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "SHIP" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "F2F" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "BWH" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "SP1" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "TRK1" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "TRK2" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "INB" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "TEB1" ||
                grid[e.Row, "TP_DELIVERY"].ToString() == "TEB3" ||
                (grid[e.Row, "TP_DELIVERY"].ToString() == "AIR" && grid[e.Row, "CD_PARTNER"].ToString() == "12448"))
                return;

            CellStyle style = grid.Rows[e.Row].Style;

            decimal column1, column2, column3, column4;

            if (this._flexH["CD_PARTNER"].ToString() == "01107")
            {
                column1 = Convert.ToDecimal(string.IsNullOrEmpty(grid[e.Row, "AM_CHARGE"].ToString()) ? 0 : grid[e.Row, "AM_CHARGE"]);
                column2 = Convert.ToDecimal(string.IsNullOrEmpty(grid[e.Row, "AM_CHARGE_MONTH"].ToString()) ? 0 : grid[e.Row, "AM_CHARGE_MONTH"]);
            }
            else
            {
                column1 = Convert.ToDecimal(string.IsNullOrEmpty(grid[e.Row, "AM_WEEK_TOTAL"].ToString()) ? 0 : grid[e.Row, "AM_WEEK_TOTAL"]);
                column2 = Convert.ToDecimal(string.IsNullOrEmpty(grid[e.Row, "AM_MONTH_TOTAL"].ToString()) ? 0 : grid[e.Row, "AM_MONTH_TOTAL"]);
            }

            column3 = Convert.ToDecimal(string.IsNullOrEmpty(grid[e.Row, "AM_TAX"].ToString()) ? 0 : grid[e.Row, "AM_TAX"]);
            column4 = Convert.ToDecimal(string.IsNullOrEmpty(grid[e.Row, "AM_TAX_MONTH"].ToString()) ? 0 : grid[e.Row, "AM_TAX_MONTH"]);

            if (column2 > 0 && column1 != column2)
                grid.Rows[e.Row].Style = grid.Styles["YELLOW"];
            else if (column4 > 0 && column3 != column4)
                grid.Rows[e.Row].Style = grid.Styles["ORANGE"];
            else
                grid.Rows[e.Row].Style = grid.Styles["WHITE"];
        }
    }
}

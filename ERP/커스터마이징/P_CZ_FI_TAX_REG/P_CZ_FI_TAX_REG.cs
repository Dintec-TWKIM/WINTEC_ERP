using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Common.Util;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Parsing.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_FI_TAX_REG : PageBase
    {
        private P_CZ_FI_TAX_REG_BIZ _biz;

        private string 부가세신고여부
        {
            get
            {
                if (this.rdo부가세전체.Checked == true)
                    return "A";
                else if (this.rdo부가세신고.Checked == true)
                    return "S";
                else
                    return "M";
            }
        }

        public P_CZ_FI_TAX_REG()
        {
            StartUp.Certify(this);
            InitializeComponent();

            _biz = new P_CZ_FI_TAX_REG_BIZ();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flex, this._flexD };
            this._flexH.DetailGrids = new FlexGrid[] { this._flexL, this._flexD };

            #region 대량등록

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("YN_CHECK", "확인여부", 100, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_TAX", "신고번호", 100);
            this._flexH.SetCol("DT_TAX", "신고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_LOADING", "선적일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("CD_EXCH", "통화명", 100);
            this._flexH.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_IO", "외화금액(출고)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_EX_IV", "외화금액(매출)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PAY", "결재금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_DIFF", "외화차액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_TAX_EX", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_TAX", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("NO_REF", "송품장부호", 100);
            this._flexH.SetCol("NO_IMPORT", "수입신고번호", 100);
            this._flexH.SetCol("TP_GUBUN", "거래구분", 100);
            this._flexH.SetCol("DC_IMPORT", "원상태수출비고", 100, true);
            this._flexH.SetCol("DC_TAX", "신고인기재란", 100);
            this._flexH.SetCol("DC_RMK", "비고", 100, true);

            this._flexH.SetDummyColumn(new string[] { "S" });

            this._flexH.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid4 });

            this._flexH.SettingVersion = "0.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.SetStringFormatCol2("NO_TAX", "AAA-AA-AAAAAAAAAA");
            #endregion

            #region Line
            this._flexL.BeginSetting(2, 1, false);

            this._flexL.SetCol("NO_IV", "계산서번호", 100);
            this._flexL.SetCol("NO_IO", "입고번호", 100);
            this._flexL.SetCol("NO_SO", "수주번호", 100);
            this._flexL.SetCol("AM_EX_IO", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_IO", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NM_EXCH_IV", "통화명", 100);
            this._flexL.SetCol("RT_EXCH_IV", "환율", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX_IV", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_IV", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NM_EXCH", "통화명", 100);
            this._flexL.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_EX", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flexL[0, this._flexL.Cols["AM_EX_IO"].Index] = "출고";
            this._flexL[0, this._flexL.Cols["AM_IO"].Index] = "출고";

            this._flexL[0, this._flexL.Cols["NM_EXCH_IV"].Index] = "매출";
            this._flexL[0, this._flexL.Cols["RT_EXCH_IV"].Index] = "매출";
            this._flexL[0, this._flexL.Cols["AM_EX_IV"].Index] = "매출";
            this._flexL[0, this._flexL.Cols["AM_IV"].Index] = "매출";

            this._flexL[0, this._flexL.Cols["NM_EXCH"].Index] = "신고";
            this._flexL[0, this._flexL.Cols["RT_EXCH"].Index] = "신고";
            this._flexL[0, this._flexL.Cols["AM_EX"].Index] = "신고";
            this._flexL[0, this._flexL.Cols["AM"].Index] = "신고";

            this._flexL.SettingVersion = "0.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Detail
            this._flexD.BeginSetting(1, 1, false);

            this._flexD.SetCol("NO_IO", "출고번호", 100);
            this._flexD.SetCol("DC_RMK", "비고", 100);

            this._flexD.SettingVersion = "0.0.0.1";
            this._flexD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion

            #region 건별등록
            this._flex.BeginSetting(1, 1, false);

            this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flex.SetCol("NO_IV", "계산서번호", 100);
            this._flex.SetCol("NO_IO", "출고번호", 100);
            this._flex.SetCol("NO_SO", "수주번호", 100);
            this._flex.SetCol("DT_PROCESS", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_VAT", "부가세신고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_VESSEL", "호선명", 120);
            this._flex.SetCol("NM_PARTNER", "매출처명", 120);
            this._flex.SetCol("NO_TO", "수출신고번호", 110, true);
            this._flex.SetCol("DT_TO", "신고일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("DT_SHIPPING", "선적일자", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex.SetCol("NM_EXCH_IV", "통화명", 60, false);
            this._flex.SetCol("RT_EXCH_IV", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("AM_IV", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_EX_IV", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("AM_EX_DIF", "외화차액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex.SetCol("NM_EXCH_TAX", "통화명", 60, true);
            this._flex.SetCol("RT_EXCH_TAX", "환율", 60, true, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex.SetCol("AM_TAX", "원화금액", 80, true, typeof(decimal), FormatTpType.MONEY);
            this._flex.SetCol("AM_EX_TAX", "외화금액", 80, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex.SetDummyColumn(new string[] { "S" });

            this._flex.SetCodeHelpCol("NM_EXCH_TAX", HelpID.P_MA_CODE_SUB, ShowHelpEnum.Always, "CD_EXCH_TAX", "NM_EXCH_TAX", ResultMode.FastMode);

            this._flex.SetOneGridBinding(new object[] { this.txt계산서번호, this.txt수주번호 }, new IUParentControl[] { this.one일반정보, this.one계산서정보, this.one수출신고정보 });

            this._flex.SettingVersion = "1.0";
            this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex.SetExceptSumCol(new string[] { "RT_EXCH_IV", "RT_EXCH_TAX" });

            this._flex.Cols.Frozen = 3;
            this._flex.SetStringFormatCol2("NO_TO", "AAA-AA-AAAAAAAAAA");

            this._flex.Styles.Add("일치").BackColor = Color.White;
            this._flex.Styles.Add("불일치").BackColor = Color.Yellow;
            #endregion
        }

        private void InitEvent()
        {
			this.btn수출이행내역업로드.Click += Btn수출이행내역업로드_Click;
            this.btn엑셀양식다운로드.Click += new EventHandler(this.btn엑셀양식다운로드_Click);
            this.btn엑셀업로드.Click += new EventHandler(this.btn엑셀업로드_Click);
			this.btn출고추가.Click += Btn출고추가_Click;
			this.btn출고삭제.Click += Btn출고삭제_Click;
			this.btn출고번호찾기.Click += Btn출고번호찾기_Click;
			this.btn자동등록.Click += Btn자동등록_Click;
			this.btn수출면장파싱.Click += Btn수출면장파싱_Click;

            this.btn수출신고번호A.Click += new EventHandler(this.btn일괄적용_Click);
            this.btn수출신고일자A.Click += new EventHandler(this.btn일괄적용_Click);
            this.btn선적일자A.Click += new EventHandler(this.btn일괄적용_Click);

			this._flexH.AfterRowChange += _flexH_AfterRowChange;

            this._flex.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
            this._flex.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flex.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
            this._flex.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);

            this.ctx통화명T.QueryBefore += new BpQueryHandler(this.ctx통화명T_QueryBefore);
        }

		private void Btn수출면장파싱_Click(object sender, EventArgs e)
		{
            DataTable dt;
            List<string> tmpList;
            int index;
            string query, 신고번호, refNo, text;

            try
            {
				UNIPASSParser parsing = new UNIPASSParser();

				parsing.Parse();

				this._biz.SaveParsing(parsing.Item);

                index = 0;
                foreach (DataRow dr in parsing.Item.Rows)
				{
                    MsgControl.ShowMsg("파싱 중 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(parsing.Item.Rows.Count) });

                    신고번호 = dr["NO_TAX"].ToString().Replace("-", "");

                    query = @"SELECT ISNULL(MAX(NO_REF), '') AS NO_REF 
FROM CZ_FI_TAX_EXPORT WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND NO_TAX = '{1}'";

                    refNo = DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode, 신고번호 })).ToString();

                    text = refNo + " " + dr["DC_TAX"].ToString();

                    if (string.IsNullOrEmpty(text))
                        continue;

                    #region 수입신고, 분할증명서 번호 찾기
                    query = @"UPDATE CZ_FI_TAX_EXPORT
SET DC_IMPORT = '{2}',
    ID_UPDATE = '{3}',
    DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = '{0}'
AND NO_TAX = '{1}'";

                    string 수입신고비고 = string.Empty;

                    if (text.Contains("수입") || text.Contains("원상태"))
                    {
                        foreach (Match match in Regex.Matches(text, "[0-9]{5}-?[0-9]{2}-?[0-9A-Z]{6,7}"))
                        {
                            수입신고비고 += match.Value + " ";
                        }
                    }
                    else if (text.Contains("분할") || text.Contains("원상태"))
                    {
                        foreach (Match match in Regex.Matches(text, "[0-9]{3}-?[0-9]{2}-?[0-9A-Z]{6,7}"))
                        {
                            수입신고비고 += match.Value + " ";
                        }
                    }

                    DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           신고번호,
                                                                           수입신고비고.Trim(),
                                                                           Global.MainFrame.LoginInfo.UserID }));
                    #endregion

                    #region 출고번호 찾기
                    text = text.Replace("D0", "DO");
                    text = Regex.Replace(text, "[^0-9]", " ");

                    tmpList = text.Split(" ".ToCharArray())
                                  .Where(x => !string.IsNullOrEmpty(x))
                                  .Where(x => x.Length == 8)
                                  .Distinct()
                                  .ToList<string>();

                    query = @"SELECT DISTINCT OL.NO_IO 
FROM MM_QTIO OL WITH(NOLOCK)
WHERE OL.CD_COMPANY = '{0}'
AND RIGHT(OL.NO_IO, LEN('{1}')) = '{1}'
AND FG_PS = '2'
AND YN_PURSALE IS NOT NULL";

                    foreach (string 출고번호 in tmpList)
                    {
                        dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 출고번호));

                        if (dt.Rows.Count > 1) continue;

                        foreach (DataRow dr1 in dt.Rows)
                        {
                            DBHelper.ExecuteNonQuery("SP_CZ_FI_TAX_EXPORTD_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                              신고번호,
                                                                                              dr1["NO_IO"].ToString(),
                                                                                              Global.MainFrame.LoginInfo.UserID });
                        }
                    }
                    #endregion
                }

                query = @"SELECT TE.NO_TAX
                          FROM (SELECT TE.NO_TAX,
                                       TE.CD_COMPANY,
                                       MAX(TE.AM_PAY) AS AM_PAY
                                FROM CZ_FI_TAX_EXPORT TE WITH(NOLOCK)
                                WHERE ISNULL(TE.YN_CHECK, 'N') = 'N'
                                GROUP BY TE.CD_COMPANY, TE.NO_TAX) TE
                          LEFT JOIN (SELECT FT.CD_COMPANY, FT.NO_TO,
                                            SUM(IL.AM_EX_IO) AS AM_EX_IO
                                     FROM CZ_FI_TAX FT WITH(NOLOCK)
                                     LEFT JOIN(SELECT IL.CD_COMPANY, IL.NO_IV, IL.NO_IO, IL.NO_SO,
                                                       SUM(CASE WHEN ISNULL(IL.YN_RETURN, 'N') = 'Y' THEN - OL.AM_EX ELSE OL.AM_EX END) AS AM_EX_IO
                                                FROM SA_IVL IL WITH(NOLOCK)
                                                JOIN MM_QTIO OL WITH(NOLOCK) ON OL.CD_COMPANY = IL.CD_COMPANY AND OL.NO_IO = IL.NO_IO AND OL.NO_IOLINE = IL.NO_IOLINE AND IL.QT_CLS > 0
                                                GROUP BY IL.CD_COMPANY, IL.NO_IV, IL.NO_IO, IL.NO_SO) IL
                                     ON IL.CD_COMPANY = FT.CD_COMPANY AND IL.NO_IV = FT.NO_IV AND IL.NO_IO = FT.NO_IO AND IL.NO_SO = FT.NO_SO
                                     GROUP BY FT.CD_COMPANY, FT.NO_TO) FT
                          ON FT.CD_COMPANY = TE.CD_COMPANY AND FT.NO_TO = TE.NO_TAX
                          WHERE TE.CD_COMPANY = '{0}'
                          AND ABS(ISNULL(FT.AM_EX_IO, 0) -ISNULL(TE.AM_PAY, 0)) > 0";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode));

                index = 0;
                foreach (DataRow dr in dt.Rows)
				{
                    MsgControl.ShowMsg("자동등록 중 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dt.Rows.Count) });

                    DBHelper.ExecuteNonQuery("SP_CZ_FI_TAX_EXPORT_AUTO", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                        dr["NO_TAX"].ToString(),
                                                                                        Global.MainFrame.LoginInfo.UserID });
                }

                MsgControl.CloseMsg();
                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn수출면장파싱.Text);
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

        private void Btn출고번호찾기_Click(object sender, EventArgs e)
		{
            DataTable dt;
            DataRow[] dataRowArray;
            List<string> tmpList;
            string query, text;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    this._flexD.Redraw = false;

                    int index = 0;

                    foreach (DataRow dr in dataRowArray)
					{
                        MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        text = dr["NO_REF"].ToString() + " " + dr["DC_TAX"].ToString();
                        
                        if (string.IsNullOrEmpty(text))
                            continue;

                        text = text.Replace(Environment.NewLine, string.Empty);

                        #region 수입신고, 분할증명서 번호 찾기
                        query = @"UPDATE CZ_FI_TAX_EXPORT
SET DC_IMPORT = '{2}',
    ID_UPDATE = '{3}',
    DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = '{0}'
AND NO_TAX = '{1}'";

                        string 수입신고비고 = string.Empty;
                        string tmpText = text;

                        if (tmpText.Contains("수입") || tmpText.Contains("원상태"))
                        {
                            foreach (Match match in Regex.Matches(tmpText, "[0-9]{5}-?[0-9]{2}-?[0-9A-Z]{6,7}"))
                            {
                                수입신고비고 += match.Value + " ";
                            }

                            tmpText = Regex.Replace(tmpText, "[0-9]{5}-?[0-9]{2}-?[0-9A-Z]{6,7}", string.Empty);
                        }
                        
                        if (tmpText.Contains("분할") || tmpText.Contains("원상태"))
                        {
                            foreach (Match match in Regex.Matches(tmpText, "[0-9]{3}-?[0-9]{2}-?[0-9A-Z]{6,7}"))
                            {
                                수입신고비고 += match.Value + " ";
                            }

                            tmpText = Regex.Replace(tmpText, "[0-9]{3}-?[0-9]{2}-?[0-9A-Z]{6,7}", string.Empty);
                        }

                        DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                   dr["NO_TAX"].ToString(),
                                                                                   수입신고비고.Trim(),
                                                                                   Global.MainFrame.LoginInfo.UserID }));
                        #endregion

                        #region 출고번호 찾기
                        text = text.Replace("D0", "DO");
                        text = Regex.Replace(text, "[^0-9]", " ");

                        tmpList = text.Split(" ".ToCharArray())
                                      .Where(x => !string.IsNullOrEmpty(x))
                                      .Where(x => x.Length == 8)
                                      .Distinct()
                                      .ToList<string>();

                        query = @"SELECT DISTINCT OL.NO_IO 
FROM MM_QTIO OL WITH(NOLOCK)
WHERE OL.CD_COMPANY = '{0}'
AND RIGHT(OL.NO_IO, LEN('{1}')) = '{1}'
AND FG_PS = '2'
AND YN_PURSALE IS NOT NULL";

                        foreach (string 출고번호 in tmpList)
                        {
                            dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 출고번호));

                            if (dt.Rows.Count > 1) continue;

                            foreach (DataRow dr1 in dt.Rows)
                            {
                                DBHelper.ExecuteNonQuery("SP_CZ_FI_TAX_EXPORTD_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                  dr["NO_TAX"].ToString(),
                                                                                                  dr1["NO_IO"].ToString(),
                                                                                                  Global.MainFrame.LoginInfo.UserID });
                            }
                        }
                        #endregion
                    }

                    this._flexD.Redraw = true;
                    MsgControl.CloseMsg();

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn출고번호찾기.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
			{
                this._flexD.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

		private void Btn자동등록_Click(object sender, EventArgs e)
		{
            DataRow[] dataRowArray;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
				{
                    foreach (DataRow dr in dataRowArray)
					{
                        DBHelper.ExecuteNonQuery("SP_CZ_FI_TAX_EXPORT_AUTO", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                            dr["NO_TAX"].ToString(),
                                                                                            Global.MainFrame.LoginInfo.UserID });
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn자동등록.Text);
				}
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void Btn출고삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexD.HasNormalRow) return;

                this._flexD.RemoveItem(this._flexD.Row);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn출고추가_Click(object sender, EventArgs e)
		{
            DataTable dt;
            string query;

            try
            {
                if (!this._flexH.HasNormalRow) return;
                if (string.IsNullOrEmpty(this.txt출고번호A.Text)) return;
                if (this._flexD.DataTable.Select(string.Format("NO_TO = '{0}' AND NO_IO = '{1}'", this._flexH["NO_TAX"].ToString(), this.txt출고번호A.Text)).Length > 0)
				{
                    this.ShowMessage(공통메세지.이미등록된자료가있습니다);
                    return;
				}

                query = @"SELECT 1
FROM MM_QTIO WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND NO_IO = '{1}'
AND FG_PS = '2'
AND YN_PURSALE IS NOT NULL";

                dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                                this.txt출고번호A.Text));

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지._이가존재하지않습니다, this.lbl출고번호A.Text);
                    return;
                }

                this._flexD.Rows.Add();
                this._flexD.Row = this._flexD.Rows.Count - 1;

                this._flexD["NO_TO"] = this._flexH["NO_TAX"].ToString();
                this._flexD["NO_IO"] = this.txt출고번호A.Text;
                this._flexD["DC_RMK"] = "추가";

                this._flexD.AddFinished();
                this._flexD.Col = this._flexD.Cols.Fixed;
                this._flexD.Focus();

                this.txt출고번호A.Text = string.Empty;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		private void Btn수출이행내역업로드_Click(object sender, EventArgs e)
        {
            OleDbConnection con = null;
            DataTable dtSchema, dtExcel;
            OleDbCommand oconn;
            OleDbDataAdapter sda;
            string fileName;

            try
            {
                if (!Directory.Exists("C:\\UNIPASS")) return;
                
                DirectoryInfo dir = new DirectoryInfo("C:\\UNIPASS");

                if (dir.GetFiles("*.xlsx").Length == 0)
                    return;

                fileName = dir.GetFiles("*.xlsx")[0].FullName;

                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";Extended Properties=Excel 12.0;";

                con = new OleDbConnection(constr);
                con.Open();

                dtSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                oconn = new OleDbCommand(@"Select * From [수출이행내역기간조회$]", con);
                sda = new OleDbDataAdapter(oconn);
                dtExcel = new DataTable();

                sda.Fill(dtExcel);

                dtExcel.Rows[0].Delete();
                dtExcel.AcceptChanges();

                if (this._biz.SaveExcel(dtExcel))
                    this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn수출이행내역업로드.Text });
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt, dtD;
            string key, filter;

            try
            {
                dt = null;
                dtD = null;
                key = this._flexH["NO_TAX"].ToString();
                filter = "NO_TO = '" + key + "'";

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchExportL(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                key });

                    dtD = this._biz.SearchExportD(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 key });
                }

                this._flexL.BindingAdd(dt, filter);
                this._flexD.BindingAdd(dtD, filter);

                this.txt출고번호A.Text = string.Empty;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

		protected override void OnDataChanged(object sender, EventArgs e)
		{
			base.OnDataChanged(sender, e);

			this.btn엑셀양식다운로드.Enabled = true;
			this.btn엑셀업로드.Enabled = true;

			this.SetToolBarButtonState(this.ToolBarAddButtonEnabled, false, false, this.ToolBarSaveButtonEnabled, false);
		}

		protected override void InitPaint()
        {
            try
            {
                base.InitPaint();

                this.splitContainer1.SplitterDistance = 1049;
                this.splitContainer2.SplitterDistance = 294;

                this.dtp수출신고일자S.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
                this.dtp수출신고일자S.EndDateToString = Global.MainFrame.GetStringToday;

                this.dtp선적일자T.Mask = this.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);

                this.dtp수출신고일자A.Mask = this.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
                this.dtp수출신고일자A.Text = Global.MainFrame.GetStringToday;
                this.dtp선적일자A.Mask = this.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
                this.dtp선적일자A.Text = Global.MainFrame.GetStringToday;

                this.btn엑셀양식다운로드.Enabled = false;
                this.btn엑셀업로드.Enabled = false;
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

                if (this.tabControl1.SelectedTab == this.tpg자동등록)
				{
                    this._flexH.Binding = this._biz.SearchExportH(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                 this.dtp수출신고일자S.StartDateToString,
                                                                                 this.dtp수출신고일자S.EndDateToString,
                                                                                 this.dtp선적일자B.StartDateToString,
                                                                                 this.dtp선적일자B.EndDateToString,
                                                                                 this.cur외화차액.DecimalValue,
                                                                                 (this.chk확인건제외.Checked == true ? "Y" : "N"),
                                                                                 (this.chk원상태수출건.Checked == true ? "Y" : "N"),
                                                                                 this.txt수출신고번호S.Text });

                    if (!this._flexH.HasNormalRow)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    }
                }
                else if (this.tabControl1.SelectedTab == this.tpg수동등록)
				{
                    this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         Global.MainFrame.LoginInfo.Language,
                                                                         this.부가세신고여부,
                                                                         this.dtp선적일자S.StartDateToString,
                                                                         this.dtp선적일자S.EndDateToString,
                                                                         this.dtp매출일자S.StartDateToString,
                                                                         this.dtp매출일자S.EndDateToString,
                                                                         this.txt출고번호S.Text,
                                                                         this.txt수주번호S.Text });

                    if (!this._flex.HasNormalRow)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    }
                }
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
                base.OnToolBarSaveButtonClicked(sender, e);

                if (!this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!this.BeforeSave() || !base.SaveData()) return false;
            if (this._flexH.IsDataChanged == false &&
                this._flex.IsDataChanged == false &&
                this._flexD.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flexH.GetChanges(), this._flex.GetChanges(), this._flexD.GetChanges()))
                return false;

            this._flexH.AcceptChanges();
            this._flex.AcceptChanges();
            this._flexD.AcceptChanges();

            return true;
        }

        private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                if (e.Row < this._flex.Rows.Fixed || e.Col < this._flex.Cols.Fixed) return;

                if (this._flex.Cols[e.Col].Name == "AM_EX_DIF")
                {
                    this._flex.Rows[e.Row][e.Col] = (D.GetDecimal(this._flex.Rows[e.Row]["AM_EX_IV"]) - D.GetDecimal(this._flex.Rows[e.Row]["AM_EX_TAX"]));
                }

                if (this._flex.Cols[e.Col].Name != "NM_EXCH_TAX"
                    && this._flex.Cols[e.Col].Name != "RT_EXCH_TAX"
                    && this._flex.Cols[e.Col].Name != "AM_EX_TAX"
                    && this._flex.Cols[e.Col].Name != "AM_TAX")
                    return;

                CellStyle style = this._flex.GetCellStyle(e.Row, e.Col);

                if (style == null)
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "NM_EXCH_TAX":
                            if (D.GetString(this._flex.Rows[e.Row]["NM_EXCH_IV"]) != D.GetString(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["불일치"]);
                            else
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["일치"]);
                            return;
                        case "RT_EXCH_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["RT_EXCH_IV"]) != D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["불일치"]);
                            else
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["일치"]);
                            return;
                        case "AM_EX_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["AM_EX_IV"]) != D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["불일치"]);
                            else
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["일치"]);
                            return;
                        case "AM_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["AM_IV"]) != D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["불일치"]);
                            else
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["일치"]);
                            return;
                        default:
                            return;
                    }
                }
                else if (style.Name == "불일치")
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "NM_EXCH_TAX":
                            if (D.GetString(this._flex.Rows[e.Row]["NM_EXCH_IV"]) == D.GetString(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["일치"]);
                            return;
                        case "RT_EXCH_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["RT_EXCH_IV"]) == D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["일치"]);
                            return;
                        case "AM_EX_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["AM_EX_IV"]) == D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["일치"]);
                            return;
                        case "AM_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["AM_IV"]) == D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["일치"]);
                            return;
                        default:
                            return;
                    }
                }
                else if (style.Name == "일치")
                {
                    switch (this._flex.Cols[e.Col].Name)
                    {
                        case "NM_EXCH_TAX":
                            if (D.GetString(this._flex.Rows[e.Row]["NM_EXCH_IV"]) != D.GetString(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["불일치"]);
                            return;
                        case "RT_EXCH_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["RT_EXCH_IV"]) != D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["불일치"]);
                            return;
                        case "AM_EX_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["AM_EX_IV"]) != D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["불일치"]);
                            return;
                        case "AM_TAX":
                            if (D.GetDecimal(this._flex.Rows[e.Row]["AM_IV"]) != D.GetDecimal(this._flex.Rows[e.Row][e.Col]))
                                this._flex.SetCellStyle(e.Row, e.Col, this._flex.Styles["불일치"]);
                            return;
                        default:
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            e.Parameter.P41_CD_FIELD1 = "MA_B000005";
        }

        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (this._flex.HasNormalRow == false) return;

                this.선택합계계산();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void _flex_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (this._flex.HasNormalRow == false) return;
                if (this._flex.Cols[e.Col].Name != "S") return;

                this.선택합계계산();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void ctx통화명T_QueryBefore(object sender, BpQueryArgs e)
        {
            e.HelpParam.P41_CD_FIELD1 = "MA_B000005";
        }

        private void btn엑셀양식다운로드_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            try
            {
                bool bState = true;
                FolderBrowserDialog dlg = new FolderBrowserDialog();
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string localPath = dlg.SelectedPath + "\\" + "엑셀업로드양식_수출신고정보등록_" + Global.MainFrame.GetStringToday + ".xls";
                string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_FI_TAX_REG.xls";

                System.Net.WebClient client = new System.Net.WebClient();
                client.DownloadFile(serverPath, localPath);

                if (_flex.HasNormalRow)
                {
                    if (ShowMessage("기본데이터가 있습니다. UPDATE하시겠습니까?", "QY2") != DialogResult.Yes)
                        bState = false;
                }

                ShowMessage("4번째 줄부터 저장됩니다. 4번째 줄부터 입력하세요.\r\n'Microsoft Office 2007'인 경우 반드시 통합문서(97~2003)로 저장하세요.");

                if (bState == false) return;

                // 확장명 XLS (Excel 97~2003 용)
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + localPath + @";Extended Properties=Excel 8.0;";

                conn = new OleDbConnection(strConn);
                conn.Open();

                OleDbCommand Cmd = null;
                OleDbDataAdapter OleDBAdap = null;

                string sTableName = string.Empty;

                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                DataSet ds = new DataSet();

                // 엑셀의 1번째 시트만 데이터 만들어 주면 되므로 한 루프후 끝내면 됩니다.
                foreach (DataRow DR in dtSchema.Rows)
                {
                    OleDBAdap = new OleDbDataAdapter(DR["TABLE_NAME"].ToString(), conn);

                    OleDBAdap.SelectCommand.CommandType = System.Data.CommandType.TableDirect;
                    OleDBAdap.AcceptChangesDuringFill = false;

                    sTableName = DR["TABLE_NAME"].ToString().Replace("$", String.Empty).Replace("'", String.Empty);

                    if (DR["TABLE_NAME"].ToString().Contains("$"))
                        OleDBAdap.Fill(ds, sTableName);
                    break;
                }

                StringBuilder FldsInfo = new StringBuilder();
                StringBuilder Flds = new StringBuilder();

                // Create Field(s) String : 현재 테이블의 Field 명 생성
                foreach (DataColumn Column in ds.Tables[0].Columns)
                {
                    if (FldsInfo.Length > 0)
                    {
                        FldsInfo.Append(",");
                        Flds.Append(",");
                    }

                    FldsInfo.Append("[" + Column.ColumnName.Replace("'", "''") + "] NVARCHAR(4000)");
                    Flds.Append(Column.ColumnName.Replace("'", "''"));
                }

                DataTable dt_Copy = this._flex.DataTable.DefaultView.ToTable();

                // Insert Data
                foreach (DataRow DR in dt_Copy.Rows)
                {
                    StringBuilder Values = new StringBuilder();

                    foreach (DataColumn Column in ds.Tables[0].Columns)
                    {
                        if (!dt_Copy.Columns.Contains(Column.ColumnName)) continue;

                        if (Values.Length > 0) Values.Append(",");
                        Values.Append("'" + DR[Column.ColumnName].ToString().Replace("'", "''") + "'");
                    }

                    Cmd = new OleDbCommand(
                        "INSERT INTO [" + sTableName + "$]" +
                        "(" + Flds.ToString() + ") " +
                        "VALUES (" + Values.ToString() + ")",
                        conn);
                    Cmd.ExecuteNonQuery();
                }

                bState = true;
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

        private void btn엑셀업로드_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg;
            DataRow[] drs, drs1;
            int index;

            try
            {
                #region btn엑셀업로드
                fileDlg = new OpenFileDialog();
                fileDlg.Filter = "엑셀 파일 (*.xls)|*.xls";

                if (fileDlg.ShowDialog() != DialogResult.OK) return;

                Application.DoEvents();

                this._flex.Redraw = false;

                string FileName = fileDlg.FileName;

                Excel excel = new Excel();
                DataTable dtExcel = null;
                dtExcel = excel.StartLoadExcel(FileName, 0, 3); // 3번째 라인부터 저장

                // 필요한 컬럼 존재 유무 파악
                string[] argsPk = new string[] { "NO_IO" };
                string[] argsPkNm = new string[] { "출고번호" };

                for (int i = 0; i < argsPk.Length; i++)
                {
                    if (!dtExcel.Columns.Contains(argsPk[i]))
                    {
                        ShowMessage("필수항목이 존재하지 않습니다. -> @ : @", new string[] { argsPk[i], argsPkNm[i] });
                        return;
                    }
                }

                // 데이터 읽으면서 해당하는 값 셋팅
                DataTable dt = MA.GetCode("MA_B000005", false);
                index = 0;
                foreach (DataRow dr in dtExcel.Rows)
                {
                    MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dtExcel.Rows.Count) });

                    if (string.IsNullOrEmpty(D.GetString(dr["NO_SO"])))
                        drs = this._flex.DataTable.Select("NO_IO = '" + D.GetString(dr["NO_IO"]) + "'");
                    else
                        drs = this._flex.DataTable.Select("NO_IO = '" + D.GetString(dr["NO_IO"]) + "' AND NO_SO = '" + D.GetString(dr["NO_SO"]) + "'");

                    foreach (DataRow dr1 in drs)
                    {
                        dr1["NO_TO"] = D.GetString(dr["NO_TO"]).Replace("-", "");
                        dr1["DT_TO"] = D.GetString(dr["DT_TO"]);
                        dr1["DT_SHIPPING"] = D.GetString(dr["DT_SHIPPING"]);

                        drs1 = dt.Select("NAME = '" + D.GetString(dr["NM_EXCH_TAX"]) + "'");
                        if(drs1.Length > 0)
                        {
                            dr1["CD_EXCH_TAX"] = drs1[0][0];
                            dr1["NM_EXCH_TAX"] = D.GetString(dr["NM_EXCH_TAX"]);
                        }
                        
                        dr1["RT_EXCH_TAX"] = D.GetString(dr["RT_EXCH_TAX"]);
                        dr1["AM_EX_TAX"] = D.GetString(dr["AM_EX_TAX"]);
                        dr1["AM_TAX"] = D.GetString(dr["AM_TAX"]);
                        dr1["DC_RMK"] = D.GetString(dr["DC_RMK"]);
                    }
                }
                MsgControl.CloseMsg();

                if (this._flex.HasNormalRow)
                {
                    ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                    ToolBarSaveButtonEnabled = true;
                }
                else
                {
                    ShowMessage("엑셀업로드 작업을 완료하였습니다.");
                }

                this._flex.Redraw = true;
                #endregion
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
            {
                this._flex.Redraw = true;
                MsgControl.CloseMsg();
            }
        }

        private void btn일괄적용_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            string controlName, columnName, value;

            try
            {
                if (this._flex.HasNormalRow == false) return;

                dataRowArray = this._flex.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    controlName = ((Control)sender).Name;
                    columnName = string.Empty;
                    value = string.Empty;

                    if (controlName == this.btn수출신고번호A.Name)
                    {
                        columnName = "NO_TO";
                        value = this.txt수출신고번호A.Text;
                    }
                    else if (controlName == this.btn수출신고일자A.Name)
                    {
                        columnName = "DT_TO";
                        value = this.dtp수출신고일자A.Text;
                    }
                    else if (controlName == this.btn선적일자A.Name)
                    {
                        columnName = "DT_SHIPPING";
                        value = this.dtp선적일자A.Text;
                    }

                    if (string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(value)) return;

                    foreach (DataRow dr in dataRowArray)
                    {
                        dr[columnName] = value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 선택합계계산()
        {
            try
            {
                this.cur외화선택합계.DecimalValue = D.GetDecimal(this._flex.DataTable.Compute("SUM(AM_EX_IV)", "S = 'Y'"));
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.Common.Forms.Help;

namespace cz
{
    public partial class P_CZ_FI_IMP_PMT_REG : Duzon.Common.Forms.CommonDialog
    {
        #region 생성자 & 전역변수
        P_CZ_FI_IMP_PMT_REG_BIZ _biz;
        private string _이전값;

        private bool 필수값체크
        {
            get
            {
                if (this.txt수입면장번호.Text == "_____-__-_______")
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl수입면장번호.Text);
                    this.txt수입면장번호.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(this.txtBL번호.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lblBL번호.Text);
                    this.txtBL번호.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(this.dtp신고일자.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl신고일자.Text);
                    this.dtp신고일자.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(this.dtp납부기한.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl납부기한.Text);
                    this.dtp납부기한.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(this.ctx납부처.CodeValue))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl납부처.Text);
                    this.ctx납부처.Focus();
                    return false;
                }

                return true;
            }
        }

        public P_CZ_FI_IMP_PMT_REG()
        {
            InitializeComponent();
        }
        #endregion

        #region 초기화
        protected override void InitLoad()
        {
            base.InitLoad();

            this._biz = new P_CZ_FI_IMP_PMT_REG_BIZ();

            this.InitGrid();
            this.InitEvent();
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp발주일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp발주일자.StartDate = Global.MainFrame.GetDateTimeToday().AddMonths(-1);
            this.dtp발주일자.EndDate = Global.MainFrame.GetDateTimeToday();

            this.dtp신고일자.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.FI, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp신고일자.Text = Global.MainFrame.GetStringToday;

            this.btn초기화_Click(null, null);
        }

        private void InitGrid()
        {
            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NM_SUPPLIER", "매입처명", 200);
            this._flexH.SetCol("NO_PO", "발주번호", 100);
            this._flexH.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_KOR_PO", "담당자", 60);
            this._flexH.SetCol("AM_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexH.SetCol("AM_TAX", "관세금액", 80, false, typeof(decimal), FormatTpType.MONEY);

            this._flexH.SetDummyColumn(new string[] { "S" });
            this._flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("NO_RAN", "란번호", 80, true);
            this._flexL.SetCol("NO_DSP", "순번", 60, false);
            this._flexL.SetCol("NO_POLINE", "발주항번", 80, false);
            this._flexL.SetCol("NO_SO", "수주번호", 80, false);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 100);
            this._flexL.SetCol("CD_ITEM", "품목코드", 60);
            this._flexL.SetCol("NM_ITEM", "품목명", 80);
            this._flexL.SetCol("UNIT_SO", "단위", 40);
            this._flexL.SetCol("QT_PO_MM", " 발주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM", "원화금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_TAX", "관세금액", 80, true, typeof(decimal), FormatTpType.MONEY);

            this._flexL.SetDummyColumn(new string[] { "S" });
            this._flexL.KeyActionEnter = KeyActionEnum.MoveDown;

            this._flexL.SettingVersion = "1.0.0.1";
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexL.SetExceptSumCol(new string[] { "UM_EX" });
            #endregion
        }

        private void InitEvent()
        {
            this.cbo란번호.DropDown += new EventHandler(this.rdo란번호_DropDown);

            this.btn세액검증.Click += new EventHandler(this.btn세액검증_Click);
            this.btn관세적용.Click += new EventHandler(this.btn관세적용_Click);
            this.btn관세분배.Click += new EventHandler(this.btn관세금액분배_Click);
            this.btn부가세적용.Click += new EventHandler(this.btn부가세적용_Click);

            this.btn수입면장적용.Click += new EventHandler(this.btn수입면장적용_Click);
            this.btn첨부파일등록.Click += new EventHandler(this.btn첨부파일등록_Click);
            this.btn일괄적용.Click += new EventHandler(this.btn일괄적용_Click);
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn초기화.Click += new EventHandler(this.btn초기화_Click);
            this.btn등록.Click += new EventHandler(this.btn등록_Click);
            this.btn취소.Click += new EventHandler(this.btn취소_Click);

            this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);
            this._flexH.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
            this._flexL.AfterEdit += new RowColEventHandler(this._flexL_AfterEdit);

            this._flexL.KeyDown += new KeyEventHandler(this._flexL_KeyDown);
            this._flexL.KeyDownEdit += new KeyEditEventHandler(this._flexL_KeyDownEdit);
            this._flexL.AfterRowChange += new RangeEventHandler(this._flexL_AfterRowChange);
        }
        #endregion

        #region 그리드 이벤트
        private void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            DataTable dt;
            string key, filter;

            try
            {
                key = this._flexH["NO_PO"].ToString();
                filter = "NO_PO = '" + key + "'";

                if (this._flexH["S"].ToString() == "Y") //클릭하는 순간은 N이므로
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               key,
                                                               Global.MainFrame.LoginInfo.UserID });

                    this._flexL.Redraw = false;

                    foreach (DataRow dr in dt.Rows)
                    {
                        this._flexL.DataTable.ImportRow(dr);
                    }

                    this._flexL.Redraw = true;
                }
                else
                {
                    this._flexL.Redraw = false;

                    foreach (DataRow dr in this._flexL.DataTable.Select(filter))
                    {
                        dr.Delete();
                    }

                    this._flexL.Redraw = true;

                    this._flexH["AM_TAX"] = 0;
                }

                this._flexL.SumRefresh();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexL.Redraw = true;
            }
        }

        private void _flexH_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = sender as FlexGrid;

                if (flex.Name == this._flexH.Name)
                {
                    if (!this._flexH.HasNormalRow) return;

                    for (int i = this._flexH.Rows.Fixed; i < this._flexH.Rows.Count; i++)
                    {
                        this._flexH.Row = i;
                        this._flexH_AfterEdit(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid grid;
            string filter;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;
                if (grid.Cols[e.Col].Name != "AM_TAX") return;
                
                filter = "NO_PO = '" + D.GetString(this._flexL["NO_PO"]) + "'";
                this._flexH.DataTable.Select(filter)[0]["AM_TAX"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_TAX)", filter));

                this._flexH.SumRefresh();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter) return;
                if (this._flexL.Cols[_flexL.Col].Name != "NO_RAN") return;
                if ((this._flexL.Rows.Count - 1) == this._flexL.Row) return;

                this._이전값 = D.GetString(this._flexL["NO_RAN"]);
                this._flexL.Row = this._flexL.Row + 1;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_KeyDownEdit(object sender, KeyEditEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter) return;
                if (this._flexL.Cols[_flexL.Col].Name != "NO_RAN") return;
                if ((this._flexL.Rows.Count - 1) == this._flexL.Row) return;

                this._이전값 = D.GetString(this._flexL["NO_RAN"]);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexL_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this._이전값)) return;

                this._flexL["NO_RAN"] = this._이전값;
                this._이전값 = string.Empty;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void btn수입면장적용_Click(object sender, EventArgs e)
        {
            string query;
            DataTable dt;

            try
            {
                query = @"SELECT FH.NO_BL,
                                 FH.DT_IMPORT,
                                 FH.DT_LIMIT,
                                 FH.CD_PAYMENT,
                                 MP.LN_PARTNER AS NM_PAYMENT,
                                 ISNULL(FH.AM_VAT_BASE, 0) AS AM_VAT_BASE,
                                 ISNULL(FH.AM_VAT, 0) AS AM_VAT,
                                 MF.NAME + '(' + MF.CNT + ')' AS FILE_PATH_MNG
                          FROM CZ_FI_IMP_PMTH FH WITH(NOLOCK)
                          LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = FH.CD_COMPANY AND MP.CD_PARTNER = FH.CD_PAYMENT
                          LEFT JOIN (SELECT CD_COMPANY,
                          				    CD_FILE,
                          				    MAX(FILE_NAME) AS NAME,
                          				    CONVERT(VARCHAR, COUNT(1)) AS CNT 
                          		     FROM MA_FILEINFO WITH(NOLOCK)
                          		     WHERE CD_MODULE = 'FI'
                          		     AND ID_MENU = 'P_CZ_FI_IMP_PMT_MNG'
                          		     GROUP BY CD_COMPANY, CD_MODULE, ID_MENU, CD_FILE) MF 
                          ON MF.CD_COMPANY = FH.CD_COMPANY AND MF.CD_FILE = FH.NO_IMPORT + '_' + FH.CD_COMPANY
                          WHERE FH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND FH.NO_IMPORT = '" + this.txt수입면장번호.ClipText + "'";

                dt = DBHelper.GetDataTable(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.txt첨부파일.Text = D.GetString(dt.Rows[0]["FILE_PATH_MNG"]);
                    this.txtBL번호.Text = D.GetString(dt.Rows[0]["NO_BL"]);
                    this.dtp신고일자.Text = D.GetString(dt.Rows[0]["DT_IMPORT"]);
                    this.dtp납부기한.Text = D.GetString(dt.Rows[0]["DT_LIMIT"]);
                    this.ctx납부처.CodeValue = D.GetString(dt.Rows[0]["CD_PAYMENT"]);
                    this.ctx납부처.CodeName = D.GetString(dt.Rows[0]["NM_PAYMENT"]);
                    this.cur부가세과세표준.DecimalValue = D.GetDecimal(dt.Rows[0]["AM_VAT_BASE"]);
                    this.cur부가세.DecimalValue = D.GetDecimal(dt.Rows[0]["AM_VAT"]);

                    this.txt수입면장번호.ReadOnly = true;
                    this.txtBL번호.ReadOnly = true;
                    this.dtp신고일자.Enabled = false;
                    this.dtp납부기한.Enabled = false;
                    this.ctx납부처.ReadOnly = ReadOnly.TotalReadOnly;
                    this.cur부가세과세표준.ReadOnly = true;
                    this.cur부가세.ReadOnly = true;
                    this.btn부가세적용.Enabled = false;
                }
                else
                {
                    this.txt첨부파일.Text = string.Empty;
                    this.txtBL번호.Text = string.Empty;
                    this.dtp신고일자.Text = Global.MainFrame.GetStringToday;
                    this.dtp납부기한.Text = Global.MainFrame.GetStringToday;
                    this.ctx납부처.CodeValue = string.Empty;
                    this.ctx납부처.CodeName = string.Empty;
                    this.cur부가세과세표준.DecimalValue = 0;
                    this.cur부가세.DecimalValue = 0;

                    this.txt수입면장번호.ReadOnly = false;
                    this.txtBL번호.ReadOnly = false;
                    this.dtp신고일자.Enabled = true;
                    this.dtp납부기한.Enabled = true;
                    this.ctx납부처.ReadOnly = ReadOnly.None;
                    this.cur부가세과세표준.ReadOnly = false;
                    this.cur부가세.ReadOnly = false;
                    this.btn부가세적용.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn첨부파일등록_Click(object sender, EventArgs e)
        {
            string fileCode, query;
            DataTable dt;

            try
            {
                query = @"SELECT 1 
                          FROM CZ_FI_IMP_PMTH
                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                         "AND NO_IMPORT = '" + this.txt수입면장번호.ClipText + "'";
                dt = DBHelper.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    Global.MainFrame.ShowMessage("등록되어 있지 않은 수입면장번호 입니다.\n등록 후 다시 시도 해주세요.");
                    return;
                }

                fileCode = this.txt수입면장번호.ClipText + "_" + Global.MainFrame.LoginInfo.CompanyCode;
                P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(Global.MainFrame.LoginInfo.CompanyCode, "FI", "P_CZ_FI_IMP_PMT_MNG", fileCode, "P_CZ_FI_IMP_PMT_MNG");
                dlg.ShowDialog(this);

                query = @"SELECT MAX(FILE_NAME) + '(' + CONVERT(VARCHAR, COUNT(1)) + ')' FILE_PATH_MNG
                              FROM MA_FILEINFO WITH(NOLOCK)
                              WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                        @"AND CD_MODULE = 'FI'
                              AND ID_MENU = 'P_CZ_FI_IMP_PMT_MNG'
                              AND CD_FILE = '" + fileCode + "'";

                this.txt첨부파일.Text = D.GetString(Global.MainFrame.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void rdo란번호_DropDown(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                this.cbo란번호.DataSource = ComFunc.getGridGroupBy(this._flexL.DataTable.Select("S = 'Y' AND ISNULL(NO_RAN, '') <> ''"), new string[] { "NO_RAN" }, true);
                this.cbo란번호.DisplayMember = "NO_RAN";
                this.cbo란번호.ValueMember = "NO_RAN";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn세액검증_Click(object sender, EventArgs e)
        {
            decimal 선택외화금액;

            try
            {
                if (this._flexL.HasNormalRow == false) return;
                if (this.cur환율.DecimalValue == 0)
                {
                    Global.MainFrame.ShowMessage("환율이 입력되지 않았습니다.");
                    return;
                }

                선택외화금액 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX)", "S = 'Y'"));

                this.cur과세가격.DecimalValue = ((선택외화금액 * this.cur환율.DecimalValue) + this.cur운임보험료.DecimalValue);
                this.cur관세.DecimalValue = (this.cur과세가격.DecimalValue * new Decimal(0.08));
                this.cur부가세과세표준계산.DecimalValue = (this.cur과세가격.DecimalValue + this.cur관세.DecimalValue);
                this.cur부가세계산.DecimalValue = (this.cur부가세과세표준계산.DecimalValue * new Decimal(0.1));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        
        private void btn관세적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cur관세.DecimalValue == 0)
                {
                    Global.MainFrame.ShowMessage("관세가 입력되지 않았습니다.");
                    return;
                }

                this.cur관세분배.DecimalValue = this.cur관세.DecimalValue;
                this.btn관세금액분배_Click(sender, e);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn관세금액분배_Click(object sender, EventArgs e)
        {
            decimal 선택외화금액;
            DataRow[] dataRowArray;
            string filter;

            try
            {
                if (this._flexL.HasNormalRow == false) return;
                if (string.IsNullOrEmpty(D.GetString(this.cbo란번호.SelectedValue)))
                {
                    Global.MainFrame.ShowMessage("CZ_란번호가 선택되어 있지 않습니다.");
                    return;
                }
                if (this.cur관세분배.DecimalValue == 0)
                {
                    Global.MainFrame.ShowMessage("CZ_관세가 입력되지 않았습니다.");
                    return;
                }
                
                filter = ("S = 'Y' AND NO_RAN = '" + this.cbo란번호.SelectedValue + "'");
                dataRowArray = this._flexL.DataTable.Select(filter);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    선택외화금액 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX)", filter));
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr["AM_TAX"] = ((D.GetDecimal(dr["AM_EX"]) / 선택외화금액) * this.cur관세분배.DecimalValue);
                    }

                    DataTable dt = ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_PO" }, true);

                    foreach(DataRow dr in dt.Rows)
                    {
                        filter = "NO_PO = '" + D.GetString(dr["NO_PO"]) + "'";
                        this._flexH.DataTable.Select(filter)[0]["AM_TAX"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_TAX)", filter));
                    }

                    this._flexL.SumRefresh();
                    this._flexH.SumRefresh();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn부가세적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cur부가세과세표준계산.DecimalValue == 0)
                {
                    Global.MainFrame.ShowMessage("부가세 과세표준 금액이 입력되지 않았습니다.");
                    return;
                }

                if (this.cur부가세계산.DecimalValue == 0)
                {
                    Global.MainFrame.ShowMessage("부가세가 입력되지 않았습니다.");
                    return;
                }

                this.cur부가세과세표준.DecimalValue = this.cur부가세과세표준계산.DecimalValue;
                this.cur부가세.DecimalValue = this.cur부가세계산.DecimalValue;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn일괄적용_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (this._flexL.HasNormalRow == false) return;
                if (string.IsNullOrEmpty(this.txt란번호.Text))
                {
                    Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl란번호.Text);
                    return;
                }

                dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                    {
                        dr["NO_RAN"] = this.txt란번호.Text;
                    }

                    //for (int i = this._flexL.Rows.Fixed; i < this._flexL.Rows.Count; i++)
                    //{
                    //    this._flexL.SetCellCheck(i, this._flexL.Cols["S"].Index, CheckEnum.Unchecked);
                    //}
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.dtp발주일자.StartDateToString,
                                                                      this.dtp발주일자.EndDateToString,
                                                                      this.ctx매입처.CodeValue,
                                                                      this.txt발주번호.Text,
                                                                      Global.MainFrame.LoginInfo.UserID });

                if (!this._flexH.HasNormalRow)
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);

                this._flexL.Binding = this._biz.SearchDetail(new object[] { string.Empty,
                                                                            string.Empty,
                                                                            string.Empty,
                                                                            Global.MainFrame.LoginInfo.UserID });
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn초기화_Click(object sender, EventArgs e)
        {
            try
            {
                this.txt수입면장번호.Text = string.Empty;
                this.txt첨부파일.Text = string.Empty;
                this.txtBL번호.Text = string.Empty;
                this.dtp신고일자.Text = Global.MainFrame.GetStringToday;
                this.dtp납부기한.Text = Global.MainFrame.GetStringToday;
                this.ctx납부처.CodeValue = string.Empty;
                this.ctx납부처.CodeName = string.Empty;
                this.cur부가세과세표준.DecimalValue = 0;
                this.cur부가세.DecimalValue = 0;

                this.cur환율.DecimalValue = 0;
                this.cur운임보험료.DecimalValue = 0;
                this.cur과세가격.DecimalValue = 0;
                this.cur부가세과세표준계산.DecimalValue = 0;
                this.cur관세.DecimalValue = 0;
                this.cur부가세계산.DecimalValue = 0;
                this.cur관세분배.DecimalValue = 0;
                this.cbo란번호.SelectedValue = string.Empty;
                this.txt란번호.Text = string.Empty;

                this.txt수입면장번호.ReadOnly = false;
                this.txtBL번호.ReadOnly = true;
                this.dtp신고일자.Enabled = false;
                this.dtp납부기한.Enabled = false;
                this.ctx납부처.ReadOnly = ReadOnly.TotalReadOnly;
                this.cur부가세과세표준.ReadOnly = true;
                this.cur부가세.ReadOnly = true;
                this.btn부가세적용.Enabled = false;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn등록_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            DataTable dtH, dtL;

            try
            {
                if (this.필수값체크 == false) return;

                dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (!중복체크(dataRowArray, new string[] { "NM_SUPPLIER" }, "매입처")) return;

                    dtH = new DataView(this._flexH.DataTable, "S = 'Y'", "", DataViewRowState.CurrentRows).ToTable();
                    dtL = new DataView(this._flexL.DataTable, "S = 'Y'", "", DataViewRowState.CurrentRows).ToTable();

                    this._biz.SaveData(dtH, dtL, new object[] { this.txt수입면장번호.Text.Replace("-", ""),
                                                                this.txtBL번호.Text,
                                                                this.dtp신고일자.Text,
                                                                this.ctx납부처.CodeValue,
                                                                this.dtp납부기한.Text,
                                                                this.cur부가세과세표준.DecimalValue,
                                                                this.cur부가세.DecimalValue });
                    
                    this.btn조회_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn취소_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private bool 중복체크(DataRow[] dr, string[] str_Filter, string ColName)
        {
            DataTable dt = ComFunc.getGridGroupBy(dr, str_Filter, true);

            if (dt.Rows.Count != 1)
            {
                Global.MainFrame.ShowMessage(공통메세지._의값이중복되었습니다, Global.MainFrame.DD(ColName));
                return false;
            }

            return true;
        }
        #endregion
    }
}

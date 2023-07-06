using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_WO_SIMULATION : PageBase
    {
        P_CZ_PR_WO_SIMULATION_BIZ _biz = new P_CZ_PR_WO_SIMULATION_BIZ();

        private string _조달구분;

        public P_CZ_PR_WO_SIMULATION()
        {
            if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
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
            this._flex조달요청L.DetailGrids = new FlexGrid[] { this._flex조달요청LD };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("LN_PARTNER", "거래처명", 100);
            this._flexH.SetCol("NO_PO_PARTNER", "거래처발주번호", 100);
            this._flexH.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_ENGINE", "엔진타입", 100);
            this._flexH.SetCol("CD_ITEM", "품목코드", false);
            this._flexH.SetCol("NM_ITEM", "품목명", 100);
            this._flexH.SetCol("NO_HULL", "호선", 100);
            this._flexH.SetCol("NO_SO", "수주번호", 100);
            this._flexH.SetCol("NO_DESIGN", "도면번호", 100);
            this._flexH.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("UNIT_SO", "단위", 100);
            this._flexH.SetCol("NM_CLASS", "선급", 100);
            this._flexH.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("QT_GIR", "의뢰수량", false);
            this._flexH.SetCol("QT_GI", "출고수량", false);
            this._flexH.SetCol("TP_WO", "작업지시상태", 100);
            this._flexH.SetCol("TP_PR", "구매요청상태", 100);
            this._flexH.SetCol("TP_SU_PR", "외주발주상태", 100);
            this._flexH.SetCol("YN_ASN", "공정경로BOM", false);

            this._flexH.SetDataMap("TP_WO", MA.GetCodeUser(new string[] { "000", "001", "002", "003" }, new string[] { "대상없음", "미진행", "진행중", "완료" }), "CODE", "NAME");
            this._flexH.SetDataMap("TP_PR", MA.GetCodeUser(new string[] { "000", "001", "002", "003" }, new string[] { "대상없음", "미진행", "진행중", "완료" }), "CODE", "NAME");
            this._flexH.SetDataMap("TP_SU_PR", MA.GetCodeUser(new string[] { "000", "001", "002", "003" }, new string[] { "대상없음", "미진행", "진행중", "완료" }), "CODE", "NAME");

            this._flexH.SettingVersion = "0.0.0.2";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.Styles.Add("완료").BackColor = Color.Gray;
            this._flexH.Styles.Add("완료").ForeColor = Color.Black;
            this._flexH.Styles.Add("미완료").BackColor = Color.White;
            this._flexH.Styles.Add("미완료").ForeColor = Color.Black;
            #endregion

            #region 조달요청

            #region Line
            this._flex조달요청L.BeginSetting(1, 1, false);

            this._flex조달요청L.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex조달요청L.SetCol("LEVEL", "레벨", false);
            this._flex조달요청L.SetCol("STND_ITEM", "규격", 100);
            this._flex조달요청L.SetCol("CD_MATL", "품목코드", false);
            this._flex조달요청L.SetCol("NM_ITEM", "품목명", 100);
            this._flex조달요청L.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex조달요청L.SetCol("NM_TP_PROC", "조달구분", false);
            this._flex조달요청L.SetCol("QT_SO", "수주수량", false);
            this._flex조달요청L.SetCol("QT_GIR", "의뢰수량", false);
            this._flex조달요청L.SetCol("QT_GI", "출하수량", false);
            this._flex조달요청L.SetCol("QT_BOM", "BOM수량", false);
            this._flex조달요청L.SetCol("QT_NEED", "필요수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청L.SetCol("QT_REMAIN", "가용수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청L.SetCol("QT_INV", "현재고", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청L.SetCol("QT_ADD", "추가수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청L.SetCol("QT_APPLY", "지시수량", false);
            this._flex조달요청L.SetCol("QT_PR", "구매요청수량", false);
            this._flex조달요청L.SetCol("QT_SU_PR", "외주요청수량", false);
            this._flex조달요청L.SetCol("QT_STOCK", "재고수량", false);
            this._flex조달요청L.SetCol("NM_UNIT", "단위", false);
            this._flex조달요청L.SetCol("NM_OP", "현재공정", 100);
            this._flex조달요청L.SetCol("NO_WO", "지시번호", 100);
            this._flex조달요청L.SetCol("DT_REL", "지시일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex조달요청L.SetCol("NO_PR", "구매요청번호", false);
            this._flex조달요청L.SetCol("NO_SU_PR", "외주요청번호", false);
            this._flex조달요청L.SetCol("NO_GIREQ", "재고이동번호", false);
            this._flex조달요청L.SetCol("YN_ASN", "공정경로BOM", false);

            this._flex조달요청L.SetDummyColumn(new string[] { "S" });

            this._flex조달요청L.SettingVersion = "0.0.0.2";
            this._flex조달요청L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line Detail
            this._flex조달요청LD.BeginSetting(1, 1, false);

            this._flex조달요청LD.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
            this._flex조달요청LD.SetCol("LN_PARTNER", "거래처명", 100);
            this._flex조달요청LD.SetCol("NO_PO_PARTNER", "거래처발주번호", 100);
            this._flex조달요청LD.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex조달요청LD.SetCol("NM_ENGINE", "엔진타입", 100);
            this._flex조달요청LD.SetCol("NM_ITEM", "품목명", 100);
            this._flex조달요청LD.SetCol("NO_HULL", "호선", 100);
            this._flex조달요청LD.SetCol("NO_SO_FROM", "수주번호", 100);
            this._flex조달요청LD.SetCol("SEQ_SO_FROM", "수주항번", false);
            this._flex조달요청LD.SetCol("CD_ITEM_FROM", "품목코드", false);
            this._flex조달요청LD.SetCol("NO_DESIGN", "도면번호", 100);
            this._flex조달요청LD.SetCol("LEVEL", "레벨", false);
            this._flex조달요청LD.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_GIR", "의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_GI", "출하수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_BOM", "BOM수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_NEED", "필요수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_APPLY", "지시수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_PR", "구매요청수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_SU_PR", "외주요청수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_REQ", "요청수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("QT_STOCK_REQ", "요청수량(재고)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex조달요청LD.SetCol("UNIT_SO", "단위", 100);
            this._flex조달요청LD.SetCol("NM_CLASS", "선급", 100);
            this._flex조달요청LD.SetCol("DT_DUEDATE", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex조달요청LD.SetCol("NO_WO", "지시번호", false);
            this._flex조달요청LD.SetCol("NO_WO_SO", "지시수주번호", false);
            this._flex조달요청LD.SetCol("NO_PR", "구매요청번호", false);
            this._flex조달요청LD.SetCol("NO_SU_PR", "외주요청번호", false);

            this._flex조달요청LD.SetDummyColumn(new string[] { "S" });

            this._flex조달요청LD.SettingVersion = "0.0.0.3";
            this._flex조달요청LD.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion
        }

        private void InitEvent()
        {
			this._flex조달요청L.AfterRowChange += _flex조달요청L_AfterRowChange;
			this._flex조달요청LD.AfterEdit += _flex조달요청LD_AfterEdit;

			this._flexH.OwnerDrawCell += _flexH_OwnerDrawCell;

            this.btn수량산출.Click += Btn수량산출_Click;
			this.btn조달요청.Click += Btn조달요청_Click;
			this.btn요청삭제.Click += Btn요청삭제_Click;
			this.btn요청포함.Click += Btn요청포함_Click;
            this.btn요청제외.Click += Btn요청제외_Click;
			this.btn재고할당.Click += Btn재고할당_Click;
			this.btn재고할당취소.Click += Btn재고할당취소_Click;
			this.btn자동요청.Click += Btn자동요청_Click;

			this.btn데이터정리.Click += Btn데이터정리_Click;
		}

        private void Btn자동요청_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                if (this.chk요청포함기본.Checked == false)
				{
                    this.ShowMessage("요청포함기본 옵션이 선택되어 있어야 자동요청 가능합니다.");
                    return;
				}

                if (Global.MainFrame.ShowMessage("자동요청 처리 하시겠습니까?", "QY2") != DialogResult.Yes)
                    return;

                #region 수량산출

                #region 초기화
                this._flex조달요청L.ClearData();
                this._flex조달요청LD.ClearData();
                #endregion

                this._조달구분 = this.cbo조달구분.SelectedValue.ToString();

                this._flex조달요청L.Binding = this._biz.SearchLine(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  this._flexH["NO_SO"].ToString(),
                                                                                  this._flexH["CD_ITEM"].ToString(),
                                                                                  this.dtp납기일자.StartDateToString,
                                                                                  this.dtp납기일자.EndDateToString,
                                                                                  (this.chk의뢰제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk출하제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk종결제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk작업지시제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk구매요청제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk외주요청제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk재고할당제외.Checked == true ? "Y" : "N"),
                                                                                  this._조달구분,
                                                                                  this.cbo경로유형.SelectedValue.ToString() });
                #endregion

                #region 수량등록
                int index = 0;
                this._flex조달요청LD.Redraw = false;
                this._flex조달요청L.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

                for (int h = 0; h < this._flex조달요청L.Rows.Count - 1; h++)
                {
                    MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(this._flex조달요청L.DataTable.Rows.Count) });

                    this._flex조달요청L.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.
                }

                this._flex조달요청LD.Redraw = true;
                #endregion
            }
			catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
            finally
			{
                this._flex조달요청LD.Redraw = true;
                MsgControl.CloseMsg();
			}
        }

        private void Btn재고할당_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flex조달요청LD.DataTable.Select("S = 'Y'", "DT_DUEDATE ASC");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    int index = 0;
                    foreach (DataRow dr in dataRowArray)
                        dr["QT_STOCK_REQ"] = 0;

                    index = 0;
                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        string filter = string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}' AND CD_MATL = '{2}'", this._flex조달요청L["NO_SO"].ToString(), this._flex조달요청L["CD_ITEM"].ToString(), this._flex조달요청L["CD_MATL"].ToString());
                        int 재고수량합 = D.GetInt(this._flex조달요청LD.DataTable.Compute("SUM(QT_STOCK_REQ)", filter));
                        int 현재고 = D.GetInt(this._flex조달요청L["QT_INV"]);
                        int 할당예정수량 = D.GetInt(dr["QT_REQ_OLD"]) - D.GetInt(dr["QT_REQ"]);

						if ((재고수량합 + 할당예정수량) > 현재고)
						{
							dr["QT_STOCK_REQ"] = (현재고 - 재고수량합);
							break;
						}
						else
							dr["QT_STOCK_REQ"] = 할당예정수량;
                    }

                    this._flex조달요청LD.SumRefresh();

                    this.재고수량갱신();

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn재고할당.Text);
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

        private void Btn재고할당취소_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex조달요청LD.HasNormalRow) return;

                dataRowArray = this._flex조달요청LD.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
				{
                    int index = 0;
                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        dr["QT_STOCK_REQ"] = 0;
                    }

                    this._flex조달요청LD.SumRefresh();

                    this.재고수량갱신();

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn재고할당취소.Text);
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

        private void _flex조달요청LD_AfterEdit(object sender, RowColEventArgs e)
		{
            try
            {
                if (this._flex조달요청LD.Cols[e.Col].Name == "QT_STOCK_REQ")
                {
                    string filter = string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}' AND CD_MATL = '{2}'", this._flex조달요청L["NO_SO"].ToString(), this._flex조달요청L["CD_ITEM"].ToString(), this._flex조달요청L["CD_MATL"].ToString());
                    int 재고수량합 = D.GetInt(this._flex조달요청LD.DataTable.Compute("SUM(QT_STOCK_REQ)", filter));
                    int 현재고 = D.GetInt(this._flex조달요청L["QT_INV"]);
                    int 필요수량 = D.GetInt(this._flex조달요청LD["QT_NEED"]);
                    int 요청수량 = D.GetInt(this._flex조달요청LD["QT_REQ"]);
                    int 재고수량 = D.GetInt(this._flex조달요청LD["QT_STOCK_REQ"]);

                    if (재고수량합 > 현재고)
                    {
                        this.ShowMessage(string.Format("요청수량(재고) 합이 현재고보다 많습니다.\n현재고 : {0}, 재고수량 합 : {1}", new object[] { 현재고, 재고수량합 }));
                        this._flex조달요청LD["QT_STOCK_REQ"] = 0;
                        this.재고수량갱신();
                    }
                    else if (재고수량 > (필요수량 - 요청수량))
					{
                        this.ShowMessage(string.Format("요청수량(재고)이 필요수량 - 요청수량보다 많습니다.\n필요수량 - 요청수량 : {0}, 재고수량 : {1}", new object[] { (필요수량 - 요청수량), 재고수량 }));
                        this._flex조달요청LD["QT_STOCK_REQ"] = 0;
                        this.재고수량갱신();
                    }
                    else
                        this.재고수량갱신();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;
                if (e.Row < this._flexH.Rows.Fixed || e.Col < this._flexH.Cols.Fixed)
                    return;

                CellStyle cellStyle = this._flexH.Rows[e.Row].Style;

                if ((this._flexH[e.Row, "TP_WO"].ToString() == "000" || this._flexH[e.Row, "TP_WO"].ToString() == "003") &&
                    (this._flexH[e.Row, "TP_PR"].ToString() == "000" || this._flexH[e.Row, "TP_PR"].ToString() == "003") &&
                    (this._flexH[e.Row, "TP_SU_PR"].ToString() == "000" || this._flexH[e.Row, "TP_SU_PR"].ToString() == "003"))
                {
                    if (cellStyle == null || cellStyle.Name != "완료")
                        this._flexH.Rows[e.Row].Style = this._flexH.Styles["완료"];
                }
                else
                {
                    if (cellStyle == null || cellStyle.Name != "미완료")
                        this._flexH.Rows[e.Row].Style = this._flexH.Styles["미완료"];
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

            this.cbo조회일자.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { "수주일자", "납기일자" });
            this.cbo조회일자.DisplayMember = "NAME";
            this.cbo조회일자.ValueMember = "CODE";
            this.cbo조회일자.SelectedValue = "002";

            this.cbo작업지시상태.DataSource = MA.GetCodeUser(new string[] { "", "000", "001", "002", "003" }, new string[] { "", "대상없음", "미진행", "진행중", "완료" });
            this.cbo작업지시상태.DisplayMember = "NAME";
            this.cbo작업지시상태.ValueMember = "CODE";

            this.cbo구매요청상태.DataSource = MA.GetCodeUser(new string[] { "", "000", "001", "002", "003" }, new string[] { "", "대상없음", "미진행", "진행중", "완료" });
            this.cbo구매요청상태.DisplayMember = "NAME";
            this.cbo구매요청상태.ValueMember = "CODE";

            this.cbo외주요청상태.DataSource = MA.GetCodeUser(new string[] { "", "000", "001", "002", "003" }, new string[] { "", "대상없음", "미진행", "진행중", "완료" });
            this.cbo외주요청상태.DisplayMember = "NAME";
            this.cbo외주요청상태.ValueMember = "CODE";

            this.dtp조회일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
            this.dtp조회일자.EndDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(3).ToString("yyyyMMdd");

            this.dtp납기일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
            this.dtp납기일자.EndDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(3).ToString("yyyyMMdd");

            this.cbo공장.DataSource = Global.MainFrame.GetComboDataCombine("N;MA_PLANT");
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";

            this.cbo경로유형.DataSource = DBHelper.GetDataTable("UP_PR_PATN_ROUT_ALL_S2", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                                         Global.SystemLanguage.MultiLanguageLpoint });
            this.cbo경로유형.DisplayMember = "NAME";
            this.cbo경로유형.ValueMember = "CODE";

            this.cbo조달구분.DataSource = DBHelper.GetDataTable(string.Format(@"SELECT '' AS CODE, '' AS NAME
                                                                                UNION ALL
                                                                                SELECT MC.CD_SYSDEF AS CODE,
                                                                                       MC.NM_SYSDEF AS NAME
                                                                                FROM MA_CODEDTL MC
                                                                                WHERE MC.CD_COMPANY = '{0}'
                                                                                AND MC.CD_FIELD = 'MA_B000009'
                                                                                AND MC.CD_SYSDEF IN ('M', 'P', 'S')", Global.MainFrame.LoginInfo.CompanyCode));
            this.cbo조달구분.DisplayMember = "NAME";
            this.cbo조달구분.ValueMember = "CODE";

            this.splitContainer1.SplitterDistance = 1192;
        }

        private void _flex조달요청L_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, key1, key2, filter;

            try
            {
                dt = null;
                key = this._flex조달요청L["NO_SO"].ToString();
                key1 = this._flex조달요청L["CD_ITEM"].ToString();
                key2 = this._flex조달요청L["CD_MATL"].ToString();
                filter = string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}' AND CD_MATL = '{2}'", key, key1, key2);

                if (this._flex조달요청L.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchLineDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                   key,
                                                                   key1,
                                                                   key2,
                                                                   this.dtp납기일자.StartDateToString,
                                                                   this.dtp납기일자.EndDateToString,
                                                                   (this.chk의뢰제외.Checked == true ? "Y" : "N"),
                                                                   (this.chk출하제외.Checked == true ? "Y" : "N"),
                                                                   (this.chk종결제외.Checked == true ? "Y" : "N"),
                                                                   (this.chk작업지시제외.Checked == true ? "Y" : "N"),
                                                                   (this.chk구매요청제외.Checked == true ? "Y" : "N"),
                                                                   (this.chk외주요청제외.Checked == true ? "Y" : "N"),
                                                                   (this.chk재고할당제외.Checked == true ? "Y" : "N"),
                                                                   this._조달구분,
                                                                   this.cbo경로유형.SelectedValue.ToString(),
                                                                   (this.chk요청포함기본.Checked == true ? "Y" : "N") });
                }

                this._flex조달요청LD.BindingAdd(dt, filter);

                this.요청수량갱신();
                this.재고수량갱신();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.cbo조회일자.SelectedValue.ToString(),
                                                                      this.dtp조회일자.StartDateToString,
                                                                      this.dtp조회일자.EndDateToString,
                                                                      this.txt수주번호.Text,
                                                                      this.txt품목코드.Text,
                                                                      (this.chk의뢰제외S.Checked == true ? "Y" : "N"),
                                                                      (this.chk출하제외S.Checked == true ? "Y" : "N"),
                                                                      (this.chk종결제외S.Checked == true ? "Y" : "N"),
                                                                      this.cbo작업지시상태.SelectedValue.ToString(),
                                                                      this.cbo구매요청상태.SelectedValue.ToString(),
                                                                      this.cbo외주요청상태.SelectedValue.ToString(),
                                                                      this.cbo조달구분.SelectedValue.ToString(),
                                                                      this.cbo경로유형.SelectedValue.ToString() });

                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void Btn수량산출_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                #region 초기화
                this._flex조달요청L.ClearData();
                this._flex조달요청LD.ClearData();
                #endregion

                this._조달구분 = this.cbo조달구분.SelectedValue.ToString();

                this._flex조달요청L.Binding = this._biz.SearchLine(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                  this._flexH["NO_SO"].ToString(),
                                                                                  this._flexH["CD_ITEM"].ToString(),
                                                                                  this.dtp납기일자.StartDateToString,
                                                                                  this.dtp납기일자.EndDateToString,
                                                                                  (this.chk의뢰제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk출하제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk종결제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk작업지시제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk구매요청제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk외주요청제외.Checked == true ? "Y" : "N"),
                                                                                  (this.chk재고할당제외.Checked == true ? "Y" : "N"),
                                                                                  this._조달구분,
                                                                                  this.cbo경로유형.SelectedValue.ToString() });
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

		private void Btn조달요청_Click(object sender, EventArgs e)
		{
			DataTable dt수주추적, tmpDt, dt구매요청H, dt구매요청L, dt외주요청H, dt외주요청L, dt요청추적, dt재고사용;
			DataRow[] dataRowArray, dataRowArray1;
			string 작업지시번호, 구매요청번호, 외주요청번호;
            decimal 요청수량 = 0, 추가수량 = 0, 재고수량 = 0, 최종수량 = 0;
            int 구매요청라인, 외주요청라인;

            try
			{
                if (!this._flexH.HasNormalRow) return;

                this.btn조달요청.Enabled = false;

				if (string.IsNullOrEmpty(this.cbo공장.SelectedValue.ToString()))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
					return;
				}

				if (string.IsNullOrEmpty(this.ctx담당자.CodeValue))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl담당자.Text);
					return;
				}

                DataRow dataRow = this._flexH.GetDataRow(this._flexH.RowSel);
                
                #region 사전데이터 확인
                if (this._조달구분 == "M" || this._조달구분 == "S" || string.IsNullOrEmpty(this._조달구분))
                {
                    if (dataRow["TP_PROC"].ToString() == "M" && dataRow["YN_ASN"].ToString() == "N")
                    {
                        this.ShowMessage("공정경로 BOM이 등록되어 있지 않습니다. [" + dataRow["CD_ITEM"].ToString() + "]");
                        return;
                    }

                    if (dataRow["TP_PROC"].ToString() == "S")
                    {
                        this.ShowMessage("외주발주요청은 지원되지 않습니다. [" + dataRow["CD_ITEM"].ToString() + "]");
                        return;
                    }

                    dataRowArray = this._flex조달요청L.DataTable.Select(string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}'", dataRow["NO_SO"].ToString(),
                                                                                                                           dataRow["CD_ITEM"].ToString()));

                    foreach (DataRow dr in dataRowArray)
                    {
                        dataRowArray1 = this._flex조달요청LD.DataTable.Select(string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}' AND CD_MATL = '{2}'", dr["NO_SO"].ToString(),
                                                                                                                                                     dr["CD_ITEM"].ToString(),
                                                                                                                                                     dr["CD_MATL"].ToString()));

                        요청수량 = dataRowArray1.Sum(x => Convert.ToDecimal(x["QT_REQ"]));
                        재고수량 = dataRowArray1.Sum(x => Convert.ToDecimal(x["QT_STOCK_REQ"]));
                        추가수량 = Convert.ToDecimal(dr["QT_ADD"]);
                        최종수량 = 요청수량 + 추가수량;

                        if (최종수량 > 0)
						{
                            if (dr["TP_PROC"].ToString() == "M" && dr["YN_ASN"].ToString() == "N")
                            {
                                this.ShowMessage("공정경로 BOM이 등록되어 있지 않습니다. [" + dr["CD_MATL"].ToString() + "]");
                                return;
                            }

                            if (dr["TP_PROC"].ToString() == "S")
                            {
                                this.ShowMessage("외주발주요청은 지원되지 않습니다. [" + dr["CD_MATL"].ToString() + "]");
                                return;
                            }
                        }

						if (재고수량 > 0)
						{
							string query = @"SELECT MY.CD_COMPANY, MY.CD_ITEM, 
	   (SUM(MY.QT_GOOD_OPEN + MY.QT_REJECT_OPEN + MY.QT_INSP_OPEN + MY.QT_TRANS_OPEN) 
       + SUM(MY.QT_GOOD_GR + MY.QT_REJECT_GR + MY.QT_INSP_GR + MY.QT_TRANS_GR) 
       - SUM(MY.QT_GOOD_GI + MY.QT_REJECT_GI + MY.QT_INSP_GI + MY.QT_TRANS_GI)) AS QT_INV
FROM MM_PINVN MY WITH(NOLOCK)
WHERE MY.CD_COMPANY = '{0}' 
AND MY.P_YR = SUBSTRING(CONVERT(NVARCHAR(8), GETDATE(), 112), 1, 4)
AND MY.CD_SL = 'SL_STND'
AND MY.CD_ITEM = '{1}'
GROUP BY MY.CD_COMPANY, MY.CD_ITEM";

							DataTable dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode, dr["CD_MATL"].ToString() }));

							if (dt == null || dt.Rows.Count == 0)
							{
								this.ShowMessage(string.Format("사용가능한 재고수량이 부족 합니다.\n{0} : 0개 사용가능, {1}개 사용요청", dr["CD_MATL"].ToString(), Convert.ToInt32(재고수량).ToString()));
								return;
							}
							else if (재고수량 > D.GetInt(dt.Rows[0]["QT_INV"]))
							{
								this.ShowMessage(string.Format("사용가능한 재고수량이 부족 합니다.\n{0} : {1}개 사용가능, {2}개 사용요청", dr["CD_MATL"].ToString(), D.GetInt(dt.Rows[0]["QT_INV"]).ToString(), Convert.ToInt32(재고수량).ToString()));
								return;
							}
						}
					}
                }
                else
				{
                    dataRowArray = this._flex조달요청L.DataTable.Select(string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}'", dataRow["NO_SO"].ToString(),
                                                                                                                           dataRow["CD_ITEM"].ToString()));
                }
                #endregion

                dt수주추적 = null;
                dt요청추적 = null;
                dt구매요청H = null;
                dt구매요청L = null;
                dt외주요청H = null;
                dt외주요청L = null;
                dt재고사용 = null;

                구매요청번호 = string.Empty;
                구매요청라인 = 0;
                외주요청번호 = string.Empty;
                외주요청라인 = 0;

                #region 하위품목
                int index = 0;
                foreach (DataRow dr in dataRowArray.OrderByDescending(x => x["LEVEL"].ToString()))
                {
                    MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                    dataRowArray1 = this._flex조달요청LD.DataTable.Select(string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}' AND CD_MATL = '{2}'", dr["NO_SO"].ToString(),
                                                                                                                                                 dr["CD_ITEM"].ToString(),
                                                                                                                                                 dr["CD_MATL"].ToString()));

                    요청수량 = dataRowArray1.Sum(x => Convert.ToDecimal(x["QT_REQ"]));
                    재고수량 = dataRowArray1.Sum(x => Convert.ToDecimal(x["QT_STOCK_REQ"]));
                    추가수량 = Convert.ToDecimal(dr["QT_ADD"]);
                    최종수량 = 요청수량 + 추가수량;

                    if (최종수량 > 0)
                    {
                        if ((this._조달구분 == "M" || string.IsNullOrEmpty(this._조달구분)) &&
                            dr["TP_PROC"].ToString() == "M")
                        {
                            #region 작업지시등록
                            string 수주번호, 수주항번;

                            if (dataRowArray1.Where(x => Convert.ToDecimal(x["QT_REQ"]) > 0 &&
                                                         x["NO_SO_FROM"].ToString() == dr["NO_SO"].ToString() && x["SEQ_SO_FROM"].ToString() == dr["SEQ_SO"].ToString()).Count() > 0)
                            {
                                수주번호 = dr["NO_SO"].ToString();
                                수주항번 = dr["SEQ_SO"].ToString();
                            }
                            else if (dataRowArray1.Where(x => Convert.ToDecimal(x["QT_REQ"]) > 0).Count() > 0)
                            {
                                DataRow dr1 = dataRowArray1.Where(x => Convert.ToDecimal(x["QT_REQ"]) > 0).FirstOrDefault();

                                수주번호 = dr1["NO_SO_FROM"].ToString();
                                수주항번 = dr1["SEQ_SO_FROM"].ToString();
                            }
                            else
							{
                                수주번호 = string.Empty;
                                수주항번 = "0";
                            }

                            tmpDt = this._biz.작업지시등록(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                         this.cbo공장.SelectedValue.ToString(),
                                                                         this.cbo경로유형.SelectedValue.ToString(),
                                                                         dr["CD_MATL"].ToString(),
                                                                         Global.MainFrame.GetStringToday,
                                                                         최종수량,
                                                                         this.ctx담당자.CodeValue,
                                                                         수주번호,
                                                                         수주항번,
                                                                         Global.MainFrame.LoginInfo.UserID });

                            if (tmpDt == null || tmpDt.Rows.Count == 0)
                            {
                                this.ShowMessage("작업지시번호가 생성되지 않았습니다.");
                                return;
                            }
                            else
                                작업지시번호 = tmpDt.Rows[0]["NO_WO"].ToString();

                            if (dt수주추적 == null)
							{
                                dt수주추적 = new DataTable();
                                dt수주추적.Columns.Add("CD_COMPANY");
                                dt수주추적.Columns.Add("CD_PLANT");
                                dt수주추적.Columns.Add("NO_SO");
                                dt수주추적.Columns.Add("NO_LINE");
                                dt수주추적.Columns.Add("NO_WO");
                                dt수주추적.Columns.Add("CD_ITEM");
                                dt수주추적.Columns.Add("QT_APPLY");
                                dt수주추적.Columns.Add("ID_INSERT");
                            }

                            dt수주추적.Rows.Clear();

                            foreach (DataRow dr1 in dataRowArray1)
                            {
                                if (Convert.ToDecimal(dr1["QT_REQ"].ToString()) == 0)
                                    continue;

                                DataRow dr2 = dt수주추적.NewRow();

                                dr2["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                                dr2["CD_PLANT"] = this.cbo공장.SelectedValue;
                                dr2["NO_SO"] = dr1["NO_SO_FROM"].ToString();
                                dr2["NO_LINE"] = dr1["SEQ_SO_FROM"].ToString();
                                dr2["NO_WO"] = 작업지시번호;
                                dr2["CD_ITEM"] = dr1["CD_MATL"].ToString();
                                dr2["QT_APPLY"] = dr1["QT_REQ"].ToString();
                                dr2["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                                dt수주추적.Rows.Add(dr2);
                            }

                            if (!this._biz.수주추적정보등록(dt수주추적))
                            {
                                this.ShowMessage("수주추적정보등록 실패");
                                return;
                            }

                            this._biz.작업지시RELEASE(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.cbo공장.SelectedValue.ToString(),
                                                                     작업지시번호,
                                                                     Global.MainFrame.GetStringToday,
                                                                     최종수량,
                                                                     Global.MainFrame.LoginInfo.UserID });
                            #endregion
                        }
                        else if ((this._조달구분 == "P" || string.IsNullOrEmpty(this._조달구분)) &&
                                 dr["TP_PROC"].ToString() == "P")
                        {
                            #region 구매요청등록
                            if (dt구매요청H == null)
							{
                                dt구매요청H = new DataTable();
                                dt구매요청H.Columns.Add("CD_COMPANY");
                                dt구매요청H.Columns.Add("CD_PLANT");
                                dt구매요청H.Columns.Add("NO_PR");
                                dt구매요청H.Columns.Add("CD_DEPT");
                                dt구매요청H.Columns.Add("DT_PR");
                                dt구매요청H.Columns.Add("NO_EMP");
                                dt구매요청H.Columns.Add("CD_PJT");
                                dt구매요청H.Columns.Add("FG_PR_TP");
                                dt구매요청H.Columns.Add("NO_PRTYPE");
                                dt구매요청H.Columns.Add("DC_RMK");

                                구매요청번호 = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PU", "01", Global.MainFrame.GetStringToday.Substring(0, 6));

                                DataRow newRow = dt구매요청H.NewRow();

                                newRow["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                                newRow["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                                newRow["NO_PR"] = 구매요청번호;
                                newRow["CD_DEPT"] = Global.MainFrame.LoginInfo.DeptCode;
                                newRow["DT_PR"] = Global.MainFrame.GetStringToday;
                                newRow["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
                                newRow["FG_PR_TP"] = "008"; //PU_C000007 => 008 : 자재소요

                                dt구매요청H.Rows.Add(newRow);

                                dt구매요청L = new DataTable();
                                dt구매요청L.Columns.Add("NO_PR");
                                dt구매요청L.Columns.Add("NO_PRLINE");
                                dt구매요청L.Columns.Add("CD_COMPANY");
                                dt구매요청L.Columns.Add("CD_PLANT");
                                dt구매요청L.Columns.Add("CD_PURGRP");
                                dt구매요청L.Columns.Add("CD_ITEM");
                                dt구매요청L.Columns.Add("DT_LIMIT");
                                dt구매요청L.Columns.Add("QT_PR");
                                dt구매요청L.Columns.Add("QT_PO");
                                dt구매요청L.Columns.Add("QT_PO_MM");
                                dt구매요청L.Columns.Add("FG_PRCON");
                                dt구매요청L.Columns.Add("CD_SL");
                                dt구매요청L.Columns.Add("FG_POST");
                                dt구매요청L.Columns.Add("NO_SO");
                                dt구매요청L.Columns.Add("NO_SOLINE");
                                dt구매요청L.Columns.Add("RT_PO");
                            }

                            DataRow dr1 = dt구매요청L.NewRow();

                            dr1["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                            dr1["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                            dr1["CD_PURGRP"] = Global.MainFrame.LoginInfo.PurchaseGroupCode;
                            dr1["NO_PR"] = 구매요청번호;
                            dr1["NO_PRLINE"] = ++구매요청라인;
                            dr1["CD_ITEM"] = dr["CD_MATL"].ToString();
                            dr1["DT_LIMIT"] = Global.MainFrame.GetStringToday;
                            dr1["QT_PR"] = 최종수량;
                            dr1["QT_PO"] = 0;
                            dr1["FG_PRCON"] = string.Empty;
                            dr1["CD_SL"] = "SL_RAW";
                            dr1["FG_POST"] = "O";
                            dr1["QT_PO_MM"] = (최종수량 / D.GetDecimal(dr["UNIT_FACT"]));
                            dr1["RT_PO"] = D.GetDecimal(dr["UNIT_FACT"]);

                            dt구매요청L.Rows.Add(dr1);

                            if (dt요청추적 == null)
							{
                                dt요청추적 = new DataTable();
                                dt요청추적.Columns.Add("CD_COMPANY");
                                dt요청추적.Columns.Add("CD_PLANT");
                                dt요청추적.Columns.Add("NO_SO");
                                dt요청추적.Columns.Add("NO_LINE");
                                dt요청추적.Columns.Add("NO_PR");
                                dt요청추적.Columns.Add("NO_PRLINE");
                                dt요청추적.Columns.Add("CD_ITEM");
                                dt요청추적.Columns.Add("QT_PR");
                                dt요청추적.Columns.Add("ID_INSERT");
                            }
                            
                            dt요청추적.Rows.Clear();

                            foreach (DataRow dr2 in dataRowArray1)
                            {
                                if (Convert.ToDecimal(dr2["QT_REQ"].ToString()) == 0)
                                    continue;

                                DataRow dr3 = dt요청추적.NewRow();

                                dr3["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                                dr3["CD_PLANT"] = this.cbo공장.SelectedValue;
                                dr3["NO_SO"] = dr2["NO_SO_FROM"].ToString();
                                dr3["NO_LINE"] = dr2["SEQ_SO_FROM"].ToString();
                                dr3["NO_PR"] = 구매요청번호;
                                dr3["NO_PRLINE"] = 구매요청라인;
                                dr3["CD_ITEM"] = dr2["CD_MATL"].ToString();
                                dr3["QT_PR"] = dr2["QT_REQ"].ToString();
                                dr3["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                                dt요청추적.Rows.Add(dr3);
                            }

                            if (!this._biz.구매요청추적정보등록(dt요청추적))
                            {
                                this.ShowMessage("구매요청추적정보등록 실패");
                                return;
                            }
                            #endregion
                        }
                        else if ((this._조달구분 == "S" || string.IsNullOrEmpty(this._조달구분)) &&
                                 dr["TP_PROC"].ToString() == "S")
                        {
                            #region 외주요청등록
                            if (dt외주요청H == null)
							{
                                dt외주요청H = new DataTable();
                                dt외주요청H.Columns.Add("CD_COMPANY");
                                dt외주요청H.Columns.Add("CD_PLANT");
                                dt외주요청H.Columns.Add("NO_PR");
                                dt외주요청H.Columns.Add("DT_PR");
                                dt외주요청H.Columns.Add("NO_EMP");
                                dt외주요청H.Columns.Add("CD_DEPT");
                                dt외주요청H.Columns.Add("CD_PURGRP");
                                dt외주요청H.Columns.Add("FG_PR");
                                dt외주요청H.Columns.Add("CD_PJT");
                                dt외주요청H.Columns.Add("DC_RMK");

                                외주요청번호 = (string)Global.MainFrame.GetSeq(Global.MainFrame.LoginInfo.CompanyCode, "PR", "30", Global.MainFrame.GetStringToday.Substring(0, 6));

                                DataRow newRow = dt외주요청H.NewRow();

                                newRow["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                                newRow["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                                newRow["NO_PR"] = 외주요청번호;
                                newRow["DT_PR"] = Global.MainFrame.GetStringToday;
                                newRow["NO_EMP"] = Global.MainFrame.LoginInfo.EmployeeNo;
                                newRow["CD_DEPT"] = Global.MainFrame.LoginInfo.DeptCode;
                                newRow["CD_PURGRP"] = Global.MainFrame.LoginInfo.PurchaseGroupCode;
                                newRow["FG_PR"] = "MRQ"; //SU_0000003 => MRQ : 자재소요

                                dt외주요청H.Rows.Add(newRow);

                                dt외주요청L = new DataTable();
                                dt외주요청L.Columns.Add("CD_COMPANY");
                                dt외주요청L.Columns.Add("CD_PLANT");
                                dt외주요청L.Columns.Add("NO_PR");
                                dt외주요청L.Columns.Add("NO_LINE");
                                dt외주요청L.Columns.Add("DT_DLV");
                                dt외주요청L.Columns.Add("CD_ITEM");
                                dt외주요청L.Columns.Add("QT_PR");
                                dt외주요청L.Columns.Add("QT_PO");
                                dt외주요청L.Columns.Add("CD_PARTNER");
                                dt외주요청L.Columns.Add("ST_PROC");
                                dt외주요청L.Columns.Add("NO_SO");
                                dt외주요청L.Columns.Add("NO_SOLINE");
                                dt외주요청L.Columns.Add("DC_RMK");
                            }

                            DataRow dr1 = dt외주요청L.NewRow();

                            dr1["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                            dr1["CD_PLANT"] = this.cbo공장.SelectedValue.ToString();
                            dr1["NO_PR"] = 외주요청번호;
                            dr1["NO_LINE"] = ++외주요청라인;
                            dr1["DT_DLV"] = Global.MainFrame.GetStringToday;
                            dr1["CD_ITEM"] = dr["CD_MATL"].ToString();
                            dr1["QT_PR"] = 최종수량;
                            dr1["QT_PO"] = 0;
                            dr1["ST_PROC"] = "O";

                            dt외주요청L.Rows.Add(dr1);

                            if (dt요청추적 == null)
							{
                                dt요청추적 = new DataTable();
                                dt요청추적.Columns.Add("CD_COMPANY");
                                dt요청추적.Columns.Add("CD_PLANT");
                                dt요청추적.Columns.Add("NO_SO");
                                dt요청추적.Columns.Add("NO_LINE");
                                dt요청추적.Columns.Add("NO_PR");
                                dt요청추적.Columns.Add("NO_PRLINE");
                                dt요청추적.Columns.Add("CD_ITEM");
                                dt요청추적.Columns.Add("QT_PR");
                                dt요청추적.Columns.Add("ID_INSERT");
                            }

                            dt요청추적.Rows.Clear();

                            foreach (DataRow dr2 in dataRowArray1)
                            {
                                if (Convert.ToDecimal(dr2["QT_REQ"].ToString()) == 0)
                                    continue;

                                DataRow dr3 = dt요청추적.NewRow();

                                dr3["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                                dr3["CD_PLANT"] = this.cbo공장.SelectedValue;
                                dr3["NO_SO"] = dr2["NO_SO_FROM"].ToString();
                                dr3["NO_LINE"] = dr2["SEQ_SO_FROM"].ToString();
                                dr3["NO_PR"] = 구매요청번호;
                                dr3["NO_PRLINE"] = 구매요청라인;
                                dr3["CD_ITEM"] = dr2["CD_MATL"].ToString();
                                dr3["QT_PR"] = dr2["QT_REQ"].ToString();
                                dr3["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

                                dt요청추적.Rows.Add(dr3);
                            }

                            if (!this._biz.외주요청추적정보등록(dt요청추적))
                            {
                                this.ShowMessage("외주요청추적정보등록 실패");
                                return;
                            }
                            #endregion
                        }
                    }

                    if (재고수량 > 0)
					{
						#region 재고사용
                        if (dt재고사용 == null)
						{
                            dt재고사용 = new DataTable();
                            dt재고사용.Columns.Add("CD_ITEM");
                            dt재고사용.Columns.Add("QT_GIREQ");
                            dt재고사용.Columns.Add("NO_SO");
                            dt재고사용.Columns.Add("SEQ_SO");
						}

                        foreach (DataRow dr1 in dataRowArray1.Where(x => D.GetDecimal(x["QT_STOCK_REQ"]) > 0))
						{
                            DataRow dr2 = dt재고사용.NewRow();

                            dr2["CD_ITEM"] = dr1["CD_MATL"];
                            dr2["QT_GIREQ"] = dr1["QT_STOCK_REQ"];
                            dr2["NO_SO"] = dr1["NO_SO_FROM"];
                            dr2["SEQ_SO"] = dr1["SEQ_SO_FROM"];

                            dt재고사용.Rows.Add(dr2);
                        }
                        #endregion
                    }
				}
                #endregion

                #region 구매요청등록
                if ((this._조달구분 == "P" || string.IsNullOrEmpty(this._조달구분)) &&
                    dt구매요청H != null && dt구매요청H.Rows.Count > 0)
                    this._biz.구매요청등록(dt구매요청H, dt구매요청L);
                #endregion

                #region 외주요청등록
                if ((this._조달구분 == "S" || string.IsNullOrEmpty(this._조달구분)) &&
                    dt외주요청H != null && dt외주요청H.Rows.Count > 0)
                    this._biz.외주요청등록(dt외주요청H, dt외주요청L);
                #endregion

                #region 재고사용
                if (dt재고사용 != null && dt재고사용.Rows.Count > 0)
                    this._biz.재고이동등록(this.cbo공장.SelectedValue.ToString(), dt재고사용);
                #endregion

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn조달요청.Text);
                this.Btn수량산출_Click(null, null);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
                this.btn조달요청.Enabled = true;
            }
		}

		private void Btn요청삭제_Click(object sender, EventArgs e)
		{
			DataTable dt요청라인;
            DataRow[] dataRowArray;
            string 구매요청삭제, 외주요청삭제;

			try
			{
                if (!this._flexH.HasNormalRow) return;

                this.btn요청삭제.Enabled = false;

                if (this.ShowMessage("선택된 품목의 조달요청 관련된 내용이 모두 삭제됩니다. 계속하시겠습니까?", "EY2") != DialogResult.Yes)
                    return;

                dataRowArray = this._flex조달요청L.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
				}
                else
				{
                    #region 사전데이터 확인
                    foreach (DataRow dr in dataRowArray)
                    {
                        #region 하위품목
                        if ((this._조달구분 == "M" || string.IsNullOrEmpty(this._조달구분)) &&
                            dr["TP_PROC"].ToString() == "M" &&
                            !string.IsNullOrEmpty(dr["NO_WO"].ToString()))
                        {
                            #region 작업지시
                            if (D.GetString(dr["FG_AUTO"]) != "005" && (D.GetString(dr["ST_WO"]) == "R" || D.GetString(dr["ST_WO"]) == "S" || D.GetString(dr["ST_WO"]) == "C"))
                            {
                                this.ShowMessage("작업지시상태가 RELEASE 이후인 건이 포함되어 있습니다. [" + dr["CD_MATL"].ToString() + "]");
                                return;
                            }

                            if (D.GetString(dr["FG_AUTO"]) == "005" && (D.GetString(dr["ST_WO"]) == "S" || D.GetString(dr["ST_WO"]) == "C"))
                            {
                                this.ShowMessage("작업지시상태가 시작 이후인 건이 포함되어 있습니다. [" + dr["CD_MATL"].ToString() + "]");
                                return;
                            }

                            if (dr["YN_INPUT"].ToString() == "Y")
                            {
                                this.ShowMessage("생산투입된 건이 선택되어 있습니다. [" + dr["CD_MATL"].ToString() + "]");
                                return;
                            }
                            #endregion
                        }
                        else if ((this._조달구분 == "P" || string.IsNullOrEmpty(this._조달구분)) &&
                                 dr["TP_PROC"].ToString() == "P" &&
                                 !string.IsNullOrEmpty(dr["NO_PR"].ToString()))
                        {
                            #region 구매요청
                            if (dr["ST_PRST"].ToString() == "R" || dr["ST_PRST"].ToString() == "T")
                            {
                                this.ShowMessage("라인에 확정, 확정중인 건이 존재합니다. [" + dr["CD_MATL"].ToString() + "]");
                                return;
                            }
                            #endregion
                        }
                        else if ((this._조달구분 == "S" || string.IsNullOrEmpty(this._조달구분)) &&
                                 dr["TP_PROC"].ToString() == "S" &&
                                 !string.IsNullOrEmpty(dr["NO_SU_PR"].ToString()))
                        {
                            #region 외주요청
                            if (dr["ST_PROC"].ToString() == "R" || dr["ST_PROC"].ToString() == "T")
                            {
                                this.ShowMessage("라인에 확정, 확정중인 건이 존재합니다. [" + dr["CD_MATL"].ToString() + "]");
                                return;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion

                    구매요청삭제 = @"SELECT NO_PR, NO_PRLINE 
                                    FROM PU_PRL WITH(NOLOCK)
                                    WHERE CD_COMPANY = '{0}'
                                    AND CD_PLANT = '{1}'
                                    AND NO_PR = '{2}'";

                    외주요청삭제 = @"SELECT NO_PR, NO_LINE 
                                    FROM SU_PRL WITH(NOLOCK)
                                    WHERE CD_COMPANY = '{0}'
                                    AND CD_PLANT = '{1}'
                                    AND NO_PR = '{2}'";

                    int index = 0;
                    foreach (DataRow dr in dataRowArray)
                    {
                        MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });

                        if ((this._조달구분 == "M" || string.IsNullOrEmpty(this._조달구분)) &&
                            dr["TP_PROC"].ToString() == "M" &&
                            !string.IsNullOrEmpty(dr["NO_WO"].ToString()))
                        {
                            #region 작업지시삭제
                            this._biz.작업지시삭제(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  dr["CD_PLANT"].ToString(),
                                                                  dr["NO_WO"].ToString() });
                            #endregion
                        }
                        else if ((this._조달구분 == "P" || string.IsNullOrEmpty(this._조달구분)) &&
                                 dr["TP_PROC"].ToString() == "P" &&
                                 !string.IsNullOrEmpty(dr["NO_PR"].ToString()))
                        {
                            #region 구매요청삭제
                            dt요청라인 = DBHelper.GetDataTable(string.Format(구매요청삭제, Global.MainFrame.LoginInfo.CompanyCode,
                                                                                          this.cbo공장.SelectedValue.ToString(),
                                                                                          dr["NO_PR"].ToString()));

                            int index1 = 0;
                            foreach (DataRow dr1 in dt요청라인.Rows)
                            {
                                MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index1), D.GetString(dt요청라인.Rows.Count) });

                                this._biz.구매요청삭제(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.cbo공장.SelectedValue.ToString(),
                                                                      dr1["NO_PR"].ToString(),
                                                                      dr1["NO_PRLINE"].ToString() });
                            }
                            #endregion
                        }
                        else if ((this._조달구분 == "S" || string.IsNullOrEmpty(this._조달구분)) &&
                                 dr["TP_PROC"].ToString() == "S" &&
                                 !string.IsNullOrEmpty(dr["NO_SU_PR"].ToString()))
                        {
                            #region 외주요청삭제
                            dt요청라인 = DBHelper.GetDataTable(string.Format(외주요청삭제, Global.MainFrame.LoginInfo.CompanyCode,
                                                                                          this.cbo공장.SelectedValue.ToString(),
                                                                                          dr["NO_SU_PR"].ToString()));

                            int index1 = 0;
                            foreach (DataRow dr1 in dt요청라인.Rows)
                            {
                                MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index1), D.GetString(dt요청라인.Rows.Count) });

                                this._biz.외주요청삭제(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                     this.cbo공장.SelectedValue.ToString(),
                                                                     dr1["NO_PR"].ToString(),
                                                                     dr1["NO_LINE"].ToString() });
                            }
                            #endregion
                        }

                        if (!string.IsNullOrEmpty(dr["NO_GIREQ"].ToString()))
                        {
                            #region 재고이동삭제
                            this._biz.재고이동삭제(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                 dr["NO_GIREQ"].ToString() });
                            #endregion
                        }
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn요청삭제.Text);
                    this.Btn수량산출_Click(null, null);
                }
            }
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
            finally
			{
                MsgControl.CloseMsg();
                this.btn요청삭제.Enabled = true;
            }
		}

        private void Btn요청포함_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex조달요청LD.HasNormalRow) return;

                dataRowArray = this._flex조달요청LD.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
                {
                    foreach (DataRow dr in dataRowArray)
                        dr["QT_REQ"] = (D.GetDecimal(dr["QT_REQ_OLD"]) - D.GetDecimal(dr["QT_STOCK_REQ"]));

                    this._flex조달요청LD.SumRefresh();

                    this.요청수량갱신();

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn요청포함.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn요청제외_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flex조달요청LD.HasNormalRow) return;

                dataRowArray = this._flex조달요청LD.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else
				{
                    foreach (DataRow dr in dataRowArray)
					{
                        dr["QT_REQ"] = 0;
					}

                    this._flex조달요청LD.SumRefresh();

                    this.요청수량갱신();

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn요청제외.Text);
				}
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 요청수량갱신()
        {
            string filter;
            int 요청수량;

            try
            {
                filter = string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}' AND CD_MATL = '{2}'", this._flex조달요청L["NO_SO"].ToString(),
                                                                                                this._flex조달요청L["CD_ITEM"].ToString(),
                                                                                                this._flex조달요청L["CD_MATL"].ToString());
                요청수량 = D.GetInt(this._flex조달요청LD.DataTable.Compute("SUM(QT_REQ)", filter));

                this.lbl요청수량.Text = string.Format("요청수량 : {0}", 요청수량.ToString());
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void 재고수량갱신()
        {
            string filter;
            int 재고수량;

            try
            {
                filter = string.Format("NO_SO = '{0}' AND CD_ITEM = '{1}' AND CD_MATL = '{2}'", this._flex조달요청L["NO_SO"].ToString(),
                                                                                                this._flex조달요청L["CD_ITEM"].ToString(),
                                                                                                this._flex조달요청L["CD_MATL"].ToString());
                재고수량 = D.GetInt(this._flex조달요청LD.DataTable.Compute("SUM(QT_STOCK_REQ)", filter));

                this.lbl재고수량.Text = string.Format("재고수량 : {0}", 재고수량.ToString());
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn데이터정리_Click(object sender, EventArgs e)
        {
            try
            {
                bool result = this._biz.데이터정리(new object[] { Global.MainFrame.LoginInfo.CompanyCode });

                if (result)
                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn데이터정리.Text);
                else
                    this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
    }
}

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU.MF;

using Duzon.ERPU;
namespace trade
{
    public partial class P_TR_EXBL_MNG : PageBase
    {
        #region ♣ 초기화 이벤트 / 메소드

        P_TR_EXBL_MNG_BIZ _biz = new P_TR_EXBL_MNG_BIZ();
        bool bGridrowChanging = false;

        string str사업장 = "";
        string str기표기간시작 = "";
        string str기표기간끝 = "";
        string str거래처 = "";
        string str영업그룹 = "";
        string str담당자 = "";
        string str전표처리여부YN = "";
        string str프로젝트 = "";

        Image file_Icon = null;

        public P_TR_EXBL_MNG()
        {
            InitializeComponent();
        }

        #endregion

        #region ♣ 컨트롤이벤트

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_TR_EXBL_MNG_BIZ();

            //PageBase가 변경된 내역을 반영 해주며 저장 시점에 PageAndSave의 Save 속성을 이용해서 SaveData() 함수를 호출한다.
            MainGrids = new FlexGrid[] { _flexH };

            _flexH.DetailGrids = new FlexGrid[] { _flexL };

            DataChanged += new EventHandler(Page_DataChanged);

            InitGridH();
            InitGridL();

            file_Icon = imageList1.Images[0];
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            InitControl();

            dtp기표기간FR.Mask = GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtp기표기간FR.Text = MainFrameInterface.GetStringFirstDayInMonth;
            dtp기표기간FR.ToDayDate = MainFrameInterface.GetDateTimeToday();

            dtp기표기간TO.Mask = GetFormatDescription(DataDictionaryTypes.TR, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dtp기표기간TO.Text = MainFrameInterface.GetStringToday;
            dtp기표기간TO.ToDayDate = MainFrameInterface.GetDateTimeToday();

            ToolBarDeleteButtonEnabled = true;
            ToolBarSaveButtonEnabled = true;
        }

        #endregion

        #region -> InitContol

        private void InitControl()
        {
            //1. 사업장, 2. 선적조건, 3. 지불조건(결제형태), 4. L/C구분, 5. 화폐단위, 6. 선적구분, 7. 도착국, 8. YN구분
            DataSet g_dsCombo = GetComboData("NC;MA_BIZAREA", "S;TR_IM00003", "S;TR_IM00004", "S;TR_IM00005", "S;MA_B000005", "S;TR_IM00016", "S;MA_B000020", "S;MA_B000057");

            //cbo사업장.DataSource = g_dsCombo.Tables[0];
            //cbo사업장.DisplayMember = "NAME";
            ////cbo사업장.ValueMember = "CODE";
            //cbo사업장.SelectedValue = Global.MainFrame.LoginInfo.BizAreaCode;

            cbo전표처리여부.DataSource = g_dsCombo.Tables[7];
            cbo전표처리여부.DisplayMember = "NAME";
            cbo전표처리여부.ValueMember = "CODE";

            _flexH.SetDataMap("COND_SHIPMENT", g_dsCombo.Tables[1], "CODE", "NAME");
            _flexH.SetDataMap("COND_PAY", g_dsCombo.Tables[2], "CODE", "NAME");
            _flexH.SetDataMap("FG_LC", g_dsCombo.Tables[3], "CODE", "NAME");
            _flexH.SetDataMap("CD_EXCH", g_dsCombo.Tables[4], "CODE", "NAME");
            _flexH.SetDataMap("FG_BL", g_dsCombo.Tables[5], "CODE", "NAME");
            _flexH.SetDataMap("PORT_NATION", g_dsCombo.Tables[6], "CODE", "NAME");
        }

        #endregion

        #region -> InitGridH

        private void InitGridH()
        {
            _flexH.BeginSetting(1, 1, false);

            _flexH.SetCol("CHK", "선택", 35, true, CheckTypeEnum.Y_N);

            _flexH.SetCol("NO_BL", "선적번호", 100);
            _flexH.SetCol("YN_SLIP", "전표처리상태", 90, false);
            _flexH.SetCol("NO_DOCU", "전표번호", 100);   //전표번호, PMIS : D20110901028,  2011-09-01, 최승애 추가함. 
            _flexH.SetCol("FG_BL", "선적구분", 80);
            _flexH.SetCol("NM_SALEGRP", "영업그룹", 120);
            _flexH.SetCol("NM_BIZAREA", "사업장", 120);
            _flexH.SetCol("NM_KOR", "담당자", 80);

            _flexH.SetCol("DT_BALLOT", "기표일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("DT_LOADING", "선적일자", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("DT_ARRIVAL", "도착예정일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexH.SetCol("CD_PARTNER", "거래처코드", 70, false);
            _flexH.SetCol("LN_PARTNER", "거래처명", 140, false);
            _flexH.SetCol("CD_EXPORT", "수출자코드", 140);
            _flexH.SetCol("NM_EXPORT", "수출자명", 140);

            _flexH.SetCol("NM_SHIP_CORP", "선사", 140);
            _flexH.SetCol("CD_EXCH", "통화", 40);
            _flexH.SetCol("RT_EXCH", "기표환율", 90, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            _flexH.SetCol("AM_EX", "외화금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexH.SetCol("COND_SHIPMENT", "선적조건", 100);

            _flexH.SetCol("AM", "기표금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flexH.SetCol("COND_PAY", "결재형태", 80);
            _flexH.SetCol("COND_DAYS", "결재형태일", 80);
            //_flexH.SetCol("YN_SLIP", "전표처리", 60);   2번 겹쳐져서 주석처리함. 2011-09-01, 최승애

            _flexH.SetCol("DT_PAYABLE", "결재만기일", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            _flexH.SetCol("FG_LC", "LC구분", 80);
            _flexH.SetCol("NM_VESSEL", "VESSEL명", 120);
            _flexH.SetCol("PORT_LOADING", "선적지", 120);
            _flexH.SetCol("PORT_ARRIVER", "도착지", 120);
            _flexH.SetCol("PORT_NATION", "도착국", 120);
            _flexH.SetCol("AM_EXNEGO", "NEGO외화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexH.SetCol("AM_NEGO", "NEGO원화금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            _flexH.SetCol("NO_INV", "송장번호", 120);
            _flexH.SetCol("NO_TO", "통관번호", 120);
            _flexH.SetCol("NO_PO_PARTNER", "거래처PO번호", 200, false);        //2012.06.04 최창종 추가
            _flexH.SetCol("REMARK1", "비고1", 120);
            _flexH.SetCol("REMARK2", "비고2", 120);
            _flexH.SetCol("REMARK3", "비고3", 120);
            _flexH.SetCol("FILE_PATH_MNG", "첨부파일", 200, true);

            //_flexH.SetExceptEditCol("YN_SLIP");


            _flexH.SettingVersion = "1.5";
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            _flexH.SetExceptSumCol(new string[] { "RT_EXCH" });

            _flexH.BeforeRowChange += new RangeEventHandler(_flexH_BeforeRowChange);
            _flexH.AfterRowChange += new RangeEventHandler(_flex_AfterRowChange);
            _flexH.DoubleClick += new EventHandler(_flexH_DoubleClick);
            _flexH.ValidateEdit += new ValidateEditEventHandler(_flexH_ValidateEdit);
            _flexH.AfterEdit += new RowColEventHandler(_flexH_AfterEdit);

            

            _flexH.SetDummyColumn("CHK");

            _flexH.Cols["YN_SLIP"].TextAlign = TextAlignEnum.RightCenter;
            _flexH.Cols["COND_DAYS"].TextAlign = TextAlignEnum.RightCenter;
            _flexH.Cols["YN_SLIP"].TextAlign = TextAlignEnum.CenterCenter;

            //메뉴추가
            _flexH.AddMenuSeperator();
            ToolStripMenuItem parent = _flexH.AddPopup("첨부파일");
            _flexH.AddMenuItem(parent, "다운로드", File_Download);
            _flexH.AddMenuItem(parent, "삭제", File_Delete);
        }

        void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            //2012.07.11 최창종 추가 : 전표처리상태에 따른 전표버튼 활성화 여부

            if (_flexH.Cols[e.Col].Name != "CHK")
                return;

            DataRow[] drs = _flexH.DataTable.Select("CHK = 'Y'");
            string sYnSlip = "001"; //001:버튼 비활성화, 002:전표발행 활성화, 003:전표발행취소 활성화
            foreach (DataRow dr in drs)
            {
                if (sYnSlip == "001")
                {
                    if (D.GetString(dr["YN_SLIP"]) == "N")
                    {
                        sYnSlip = "002";
                    }
                    else
                    {
                        sYnSlip = "003";
                    }
                }
                else if (sYnSlip == "002")
                {
                    if (D.GetString(dr["YN_SLIP"]) == "Y")
                    {
                        sYnSlip = "001";
                        break;
                    }
                }
                else if (sYnSlip == "003")
                {
                    if (D.GetString(dr["YN_SLIP"]) == "N")
                    {
                        sYnSlip = "001";
                        break;
                    }
                }
            }

            if (sYnSlip == "001")
            {
                btn전표발행.Enabled = false;
                btn전표발행취소.Enabled = false;

            }
            else if (sYnSlip == "002")
            {
                btn전표발행.Enabled = true;
                btn전표발행취소.Enabled = false;
            }
            else if (sYnSlip == "003")
            {
                btn전표발행.Enabled = false;
                btn전표발행취소.Enabled = true;
            }
        }

        void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            
        } 
        #endregion

        #region -> InitGridL

        private void InitGridL()
        {
            _flexL.BeginSetting(1, 1, false);

            _flexL.SetCol("NM_PLANT", "공장", 140);
            _flexL.SetCol("CD_ITEM", "품목코드", 100, 20, false);
            _flexL.SetCol("NM_ITEM", "품목명", 140);
            _flexL.SetCol("STND_ITEM", "규격", 120);
            _flexL.SetCol("UNIT_SO", "단위", 40);
            _flexL.SetCol("QT_SO", "수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UM_SO", "단가", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_EXSO", "금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("QT_INVENT", "재고단위수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("UM_INVENT", "재고단위단가", 120, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            _flexL.SetCol("AM_INVENT", "재고단위금액", 120, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            _flexL.SetCol("DT_DELIVERY", "납기일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("QT_CLS", "마감수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("AM_CLS", "마감금액", 100, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("NO_PO", "수주번호", 100);
            _flexL.SetCol("NO_LINE_PO", "수주항번", 60);
            _flexL.SetCol("NO_LC", "LC번호", 120);
            _flexL.SetCol("NO_LINE_LC", "LC항번", 60);
            _flexL.SetCol("NO_QTIO", "출고번호", 100);
            _flexL.SetCol("NO_LINE_QTIO", "출고항번", 60);
            _flexL.SetCol("NO_INV", "송장번호", 120);
            _flexL.SetCol("NO_LINE", "송장항번", 60);
            _flexL.SetCol("NO_TO", "통관번호", 120);
            _flexL.SetCol("DT_TO", "통관일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("DT_DECLARE", "신고일", 70, 8, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            _flexL.SetCol("NO_BL", "선적번호", 120);
            _flexL.SetCol("NO_PROJECT", "프로젝트코드", 100);
            _flexL.SetCol("NM_PROJECT", "프로젝트명", 120);


            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flexL.SetExceptSumCol("UM_SO", "UM_INVENT");

            _flexL.Cols["NO_LINE_QTIO"].TextAlign = TextAlignEnum.CenterCenter;
            _flexL.Cols["NO_LINE_PO"].TextAlign = TextAlignEnum.CenterCenter;
            _flexL.Cols["NO_LINE_LC"].TextAlign = TextAlignEnum.CenterCenter;
            _flexL.Cols["NO_LINE"].TextAlign = TextAlignEnum.CenterCenter;
        }

        #endregion

        #region -> Page_DataChanged

        void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsChanged())
                    ToolBarSaveButtonEnabled = true;

                ToolBarDeleteButtonEnabled = _flexH.HasNormalRow;

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트 / 메소드

        #region -> BeforeSearch Override

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;

            if (!사업장선택여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, label2.Text);
                bpBizarea.Focus();
                return false;
            }

            if (!신고기간시작등록여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, label5.Text);
                dtp기표기간FR.Focus();
                return false;
            }

            if (!신고기간끝등록여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, label5.Text);
                dtp기표기간TO.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region -> 조회

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                btn전표발행.Enabled = false;
                btn전표발행취소.Enabled = false;

                RealSearch();

                if (!_flexH.HasNormalRow)
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        #endregion

        #region -> 실제조회구문(RealSearch)

        public bool RealSearch()
        {
            if (!BeforeSearch()) return false;

            str사업장 = bpBizarea.SelectedValue == null ? string.Empty : bpBizarea.QueryWhereIn_Pipe.ToString();        //2012.05.11 : 사업장 멀티선택
            //str사업장 = cbo사업장.SelectedValue.ToString();
            str기표기간시작 = dtp기표기간FR.Text;
            str기표기간끝 = dtp기표기간TO.Text;
            str거래처 = bpc거래처.CodeValue;
            str영업그룹 = bpc영업그룹.CodeValue;
            str담당자 = bpc담당자.CodeValue;
            str전표처리여부YN = cbo전표처리여부.SelectedValue.ToString();
            str프로젝트 = bp프로젝트.CodeValue;

            object[] obj = new object[] { LoginInfo.CompanyCode, 
                str사업장, 
                str기표기간시작, 
                str기표기간끝, 
                str거래처, 
                str영업그룹, 
                str담당자, 
                str전표처리여부YN,
                str프로젝트
            };

            DataTable dt = _biz.search(obj);

            _flexH.Binding = dt;

            int i_dr = _flexH.Rows.Fixed;
            foreach (DataRow dr in _flexH.DataTable.Rows)
            {
                if (dr["FILE_PATH_MNG"].ToString() != string.Empty)
                    _flexH.SetCellImage(i_dr, _flexH.Cols["FILE_PATH_MNG"].Index, file_Icon);

                i_dr++;
            }

            return true;
        }

        #endregion

        #region -> BeforeDelete Override

        protected override bool BeforeDelete()
        {
            if (!base.BeforeDelete()) return false;

            if (!_flexH.HasNormalRow) return false;

            return true;
        }

        #endregion

        #region -> 삭제

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeDelete()) return;

                DialogResult result = ShowMessage(공통메세지.자료를삭제하시겠습니까);
                if (result == DialogResult.Yes)
                {
                    DataRow[] drs = _flexH.DataTable.Select("CHK = 'Y'");

                    if (drs.Length == 0)
                    {
                        ShowMessage(공통메세지.선택된자료가없습니다);
                        return;
                    }

                    DataTable dt = _flexH.DataTable.Clone();
                    foreach (DataRow dr in drs)
                    {
                        dt.Rows.Add(dr.ItemArray);
                    }

                    bool bTrue = _biz.delete(dt);

                    if (bTrue)
                    {
                        RealSearch();
                        ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 저장버튼 클릭

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforeSave()) return;

                if (MsgAndSave(PageActionMode.Save))
                {
                    this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #region -> SaveData

        protected override bool SaveData()
        {
            if (!base.SaveData())
                return false;

            DataTable dt_H = null;

            dt_H = new DataTable();
            dt_H = _flexH.GetChanges();

            if ((dt_H == null || dt_H.Rows.Count == 0))
                return true;

            bool bSuccess = _biz.Save(dt_H);
            if (!bSuccess) return false;

            _flexH.AcceptChanges();

            OnToolBarSearchButtonClicked(null, null);

            return true;
        }

        #endregion

        #endregion

        #region -> 전표발행 버튼 클릭이벤트(btn전표발행_Click)

        private void btn전표발행_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drs = _flexH.DataTable.Select("CHK = 'Y'");

                if (drs.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DataTable dt = _flexH.DataTable.Clone();
                foreach (DataRow dr in drs)
                {
                    dt.Rows.Add(dr.ItemArray);
                }

                string 선적번호들 = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    선적번호들 += "'" + dt.Rows[i]["NO_BL"].ToString() + "'";
                    if (i < dt.Rows.Count - 1)
                    {
                        선적번호들 += ", ";
                    }
                }

                //string strSearchQuery = "SELECT DISTINCT NO_DOCU, CD_MNG FROM FI_DOCU WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND CD_MNG IN (" + 선적번호들 + ") ";
                string strSearchQuery = "SELECT DISTINCT NO_DOCU, CD_MNG FROM FI_DOCU WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND NO_MDOCU IN (" + 선적번호들 + ") ";

                DataTable dtSelect = Global.MainFrame.FillDataTable(strSearchQuery);

                if (dtSelect.Rows.Count > 0)
                {
                    ShowMessage("체크된 건중에 이미 전표처리된건이 @건이 있습니다.",  dtSelect.Rows.Count.ToString() );
                    return;
                }

                bool bTrue = _biz.TransDocu(dt);

                if (bTrue)
                {
                    RealSearch();
                    ShowMessage("전표처리가 완료되었습니다");
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 전표발행취소 버튼 클릭이벤트(btn전표발행취소_Click)

        private void btn전표발행취소_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drs = _flexH.DataTable.Select("CHK = 'Y'");

                if (drs.Length == 0)
                {
                    ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }

                DataTable dt = _flexH.DataTable.Clone();
                foreach (DataRow dr in drs)
                {
                    dt.Rows.Add(dr.ItemArray);
                }

                string 선적번호들 = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    선적번호들 += "'" + dt.Rows[i]["NO_BL"].ToString() + "'";
                    if (i < dt.Rows.Count - 1)
                    {
                        선적번호들 += ", ";
                    }
                }

                string strSearchQuery = "SELECT DISTINCT NO_DOCU, CD_MNG FROM FI_DOCU WHERE CD_COMPANY = '" + LoginInfo.CompanyCode + "' AND CD_MNG IN (" + 선적번호들 + ") ";

                DataTable dtSelect = Global.MainFrame.FillDataTable(strSearchQuery);

                if (dtSelect.Rows.Count != dt.Rows.Count)
                {
                    ShowMessage("체크된 건중에 이미 전표처리 되지 않은건이 있습니다.");
                    return;
                }

                bool bTrue = _biz.CancelTransDocu(dt);

                if (bTrue)
                {
                    RealSearch();
                    ShowMessage("전표취소가 완료되었습니다");
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 그리드 이벤트 / 메소드

        #region -> 그리드 행변경전 체크 이벤트(_flexH_BeforeRowChange)

        void _flexH_BeforeRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!bGridrowChanging)
                    e.Cancel = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 그리드 행변경 이벤트(_flex_AfterRowChange)

        void _flex_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                ToolBarSearchButtonEnabled = false;
                bGridrowChanging = false;

                FlexGrid _flex = sender as FlexGrid;
                if (sender == null) return;

                ChangeFilter(ref _flex);
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
            {
                ToolBarSearchButtonEnabled = true;
                bGridrowChanging = true;
            }
        }

        #endregion

        #region -> 하단 그리드의 Filter 변경 함수(ChangeFilter)

        private void ChangeFilter(ref FlexGrid _flex)
        {
            DataTable dt = new DataTable();

            string filter = "NO_BL = '" + _flex["NO_BL"].ToString() + "' ";

            object[] obj = new object[] { LoginInfo.CompanyCode, 
                str사업장, 
                str기표기간시작, 
                str기표기간끝, 
                str거래처, 
                str영업그룹, 
                str담당자, 
                str전표처리여부YN, 
                _flex["NO_BL"].ToString()
                };

            if (_flex.DetailQueryNeed)
                dt = _biz.SearchDetail(obj);

            _flex.DetailGrids[0].BindingAdd(dt, filter);
            _flex.DetailQueryNeed = false;
        }

        #endregion

        #region -> 첨부파일 업로드 / 다운로드

        #region 첨부파일 업로드
        public void _flexH_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flex = (FlexGrid)sender;
                if (_flexH.Cols[_flexH.Col].Name == "FILE_PATH_MNG")
                {
                    OpenFileDialog dlg = new OpenFileDialog();

                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        if (!dlg.SafeFileName.Contains(_flexH["NO_BL"].ToString()))
                        {
                          //  MessageBox.Show("첨부하려고하는 파일명과 선적번호가 일치하여야 합니다.");
                            ShowMessage("첨부하려고하는 파일명과 선적번호가 일치하여야 합니다.");
                            return;
                        }

                        // 서버에 파일을 업로드 한다.
                        string serveraddress = this.MainFrameInterface.HostURL;     //http://202.167.215.108/ERP-U
                        string filename = dlg.SafeFileName;                             //bcl.log
                        string localpath = dlg.FileName;                                //C:\bcl.log
                        string serverdir = "/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/EXBL"; //shared/MF_File_Mng/1130/GI
                        string action = "etc";
                        string date = "20090819";

                        ComFunc.UpLoad(serveraddress, filename, localpath, serverdir, action, date);

                        // DB 에 파일 이름을 저장할 수 있게 한다. 
                        _flexH["FILE_PATH_MNG"] = dlg.SafeFileName;

                        _flexH.AcceptChanges();

                        SaveData();
                        ShowMessage("업로드완료");
                    }
                }
                else if (flex.Cols[_flexH.Col].Name == "NO_DOCU")       //2012.04.19 최창종 전표발행메뉴로 이동
                {
                    string strNoDcu = flex[flex.Row, flex.Col].ToString();
                    if (strNoDcu != string.Empty)
                    {
                        object[] args = {
                                       strNoDcu, //-- 전표번호
                                       "1", //-- 회계번호(모르면1)
                                       "", //-- 회계단위
                                       Global.MainFrame.LoginInfo.CompanyCode //--회사코드
                                };

                        CallOtherPageMethod("P_FI_DOCU", "전표입력(" + PageName + ")", "P_FI_DOCU", Grant, args);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);

            }
        }
        #endregion

        #region 첨부파일 다운로드
        public void File_Download(object sender, EventArgs e)
        {
            try
            {
                if (_flexH["FILE_PATH_MNG"].ToString() == string.Empty || _flexH.Cols[_flexH.Col].Name != "FILE_PATH_MNG")
                    return;

                string localPath = string.Empty;
                string serverPath = string.Empty;

                FolderBrowserDialog fb_dlg = new FolderBrowserDialog();
                DialogResult result = fb_dlg.ShowDialog();

                //C:\Documents and Settings\Administrator\바탕 화면\1\GIZ20090800019.zip
                if (result == DialogResult.OK) //확인 버튼이 눌렸을 때 작동할 코드
                {
                    localPath = fb_dlg.SelectedPath + "\\" + _flexH["FILE_PATH_MNG"].ToString();

                    string serveraddress = this.MainFrameInterface.HostURL;     //http://202.167.215.108/ERP-U
                    string serverdir = "/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/EXBL";

                    //http://202.167.215.108/ERP-U/shared/MF_File_Mng/GIZ20090800019.zip
                    serverPath = serveraddress + serverdir + "\\" + _flexH["FILE_PATH_MNG"].ToString();

                    ComFunc.DownLoad(serverPath, localPath);
                    ShowMessage("다운로드완료");
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 첨부파일 삭제
        public void File_Delete(object sender, EventArgs e)
        {
            try
            {
                if (_flexH["FILE_PATH_MNG"].ToString() == string.Empty || _flexH.Cols[_flexH.Col].Name != "FILE_PATH_MNG")
                    return;

                if (Global.MainFrame.ShowMessage("첨부파일을 삭제하시겠습니까?", "QY2") == DialogResult.Yes)
                {

                    // 서버에 파일을 업로드 한다.
                    string hosturl = this.MainFrameInterface.HostURL;
                    string serverdir = "/shared/MF_File_Mng/" + Global.MainFrame.LoginInfo.CompanyCode + "/EXBL";
                    string filename = _flexH["FILE_PATH_MNG"].ToString();

                    ComFunc.DeleteFile(hosturl, serverdir, filename);
                    ShowMessage("파일 삭제 완료");

                    // DB 에 파일 이름을 삭제할 수 있게 한다. 
                    _flexH["FILE_PATH_MNG"] = string.Empty;
                    SaveData();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #endregion

        #region -> Control_QueryBefore

        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;

            switch (e.HelpID)
            {
                case HelpID.P_USER:
                    if (bp_Control.UserHelpID == "H_SA_PRJ_SUB")
                    {
                        e.HelpParam.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값
                    }
                    break;
            }
        }

        #endregion

        #region ♣ 속성

        public bool 사업장선택여부
        {
            get
            {
                //if (cbo사업장.SelectedValue == null || cbo사업장.SelectedValue.ToString() == "")  // 2012.05.10 : 사업장 멀티선택
                if (bpBizarea.CodeValues == null || bpBizarea.CodeValues.Length == 0)
                    return false;
                return true;
            }
        }

        public bool 신고기간시작등록여부
        {
            get
            {
                if (dtp기표기간FR.Text == "")
                    return false;
                return true;
            }
        }

        public bool 신고기간끝등록여부
        {
            get
            {
                if (dtp기표기간TO.Text == "")
                    return false;
                return true;
            }
        }

        #endregion
    }
}
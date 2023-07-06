using System;
using System.Data;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using System.Drawing;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;

// 사용하지 않는 화면 색깔 넣는 부분을 따로 만들 려고 했는데 통제설정에서 구분값으로 해결하게되서 이화면은 사용하지 않습니다...
/**************************************************/
/* 페이지명 : 창고별재고현황2(색깔있는것)영우디지털 전용                    */
/* 모듈   : 구매자재                              */
/* 작성자 :                                       */
/* 수정자 :                                       */
/* 수정일 :                                       */
// 
// 2009.12.28 - 안종호 - ABC구분해서 색깔 넣는부분 넣기       */
// 2013.04.17 : D20130409052 : 필드추가
/**************************************************/
namespace pur
{
    public partial class P_PU_INV_SCH2 : Duzon.Common.Forms.PageBase
    {
        #region ♣ 생성자 & 변수 선언

        private P_PU_INV_SCH2_BIZ _biz;

        public P_PU_INV_SCH2()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region ♣ 초기화 이벤트 / 메소드

        #region -> InitLoad

        protected override void InitLoad()
        {
            base.InitLoad();

            _biz = new P_PU_INV_SCH2_BIZ();

            InitGrid();
            InitEvent();
        }

        private void InitEvent()
        {
            bpc창고.QueryBefore += new BpQueryHandler(bpc창고_QueryBefore);
        }

        void bpc창고_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (!Chk공장)
                {
                    return;
                }
                e.HelpParam.P09_CD_PLANT = D.GetString(cbo_CD_PLANT.SelectedValue);
            }
            catch (Exception Ex)
            {
                MsgEnd(Ex);
            }
        }

        #endregion

        #region -> InitGrid

        private void InitGrid()
        {
            _flex.BeginSetting(1, 1, false);
            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
        }

        private void InitGrid_New(DataTable dtCross)
        {
            _flex.Cols.Count = _flex.Cols.Fixed;
            _flex.BeginSetting(1, 1, false); //추가할때 에디팅 true, false
            _flex.SetCol("NM_CLS_L", "대분류", 120);
            _flex.SetCol("NM_CLS_M", "중분류", 120);
            _flex.SetCol("NM_CLS_S", "소분류", 120);
            _flex.SetCol("CD_ITEM", "품목", 100, 20);
            _flex.SetCol("NM_ITEM", "품목명", 140, 50);
            _flex.SetCol("STND_DETAIL_ITEM", "세부규격", false);
            _flex.SetCol("MAT_ITEM", "재질", false);
            _flex.SetCol("CLS_ITEM", "계정구분", 100);
            _flex.SetCol("NM_KOR1", "관리자1", 100);
            _flex.SetCol("NM_KOR2", "관리자2", 100);
            _flex.SetCol("STND_ITEM", "규격", 120, 50);
            _flex.SetCol("FG_ABC", "FG_ABC", false);
        
            _flex.SetCol("UNIT_IM", "단위", 40);

            int start = dtCross.Columns["UNIT_IM"].Ordinal;// UNIT_IM에서 FG_ABC로 바꾸었음. 제일 끝 컬럼을 찍어줘야하므로

            string strExpression = "";

            for (int i = start + 1; i < dtCross.Columns.Count; i++)
            {
                int iColumnLength = dtCross.Columns[i].ColumnName.Length * 18;
                iColumnLength = iColumnLength < 90 ? 90 : iColumnLength;
                _flex.SetCol(dtCross.Columns[i].ColumnName, dtCross.Columns[i].Caption, iColumnLength, 17, false, typeof(decimal), FormatTpType.QUANTITY);
                strExpression += "[" + dtCross.Columns[i].ColumnName + "]";
                if ( i < dtCross.Columns.Count - 1)
                    strExpression += " + ";
            }

            //CorssTab으로 변경하면서 null 값으로 바뀐 값들을 0으로 변경하여 준다.(합계컬럼을 위해서)
            foreach (DataRow dr in dtCross.Rows)
            {
                for (int i = start + 1; i < dtCross.Columns.Count; i++)
                {
                    if (dr[i] == DBNull.Value)
                        dr[i] = 0m;
                }
            }

            dtCross.Columns.Add("QT_SUM", typeof(decimal), strExpression);

            _flex.SetCol("QT_SUM", "합계", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);

            _flex.Cols.Frozen = _flex.Cols.Fixed;

            _flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            //_flex.AfterSort += new SortColEventHandler(_flex_AfterSort);
            
            for (int c = _flex.Cols.Fixed; c < _flex.Cols.Count; c++)
                _flex.SetCellStyle(0, c, _flex.Styles.Fixed);
        }

        #endregion

        #region -> InitPaint

        protected override void InitPaint()
        {
            base.InitPaint();

            //1. 공장, 2. 계정구분, 3. ABC구분
            DataSet ds = GetComboData("NC;MA_PLANT", "S;MA_B000010", "S;MA_B000060");

            cbo_CD_PLANT.DataSource = ds.Tables[0];
            cbo_CD_PLANT.DisplayMember = "NAME";
            cbo_CD_PLANT.ValueMember = "CODE";

            //DataTable dt = ds.Tables[1].Clone();

            //// 계정구분
            //// '001' : 원자재, '002' : 부자재, '003' : 제품, '004' : 반제품, '005' : 상품, '009' : 저장품
            //// '006' : 소모품, '007' : 용역, '008' : 공사, 
            //DataRow[] drs = ds.Tables[1].Select("CODE IN ('','001','002','003','004','005','009')");

            //foreach (DataRow dr in drs)
            //    dt.ImportRow(dr);

            cboFG_ACCT.DataSource = ds.Tables[1];
            cboFG_ACCT.DisplayMember = "NAME";
            cboFG_ACCT.ValueMember = "CODE";

            cboFG_ABC.DataSource = ds.Tables[2];
            cboFG_ABC.DisplayMember = "NAME";
            cboFG_ABC.ValueMember = "CODE";

            dp기준일자.Mask = MainFrameInterface.GetFormatDescription(DataDictionaryTypes.PU, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            dp기준일자.ToDayDate = MainFrameInterface.GetDateTimeToday();
            dp기준일자.Text = MainFrameInterface.GetStringToday;
        }

        #endregion

        #endregion

        #region ♣ 메인버튼 이벤트/메소드

        #region -> BeforeSearch Override

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch())
                return false;

            if (!공장선택여부)
            {
                ShowMessage(공통메세지._은는필수입력항목입니다, m_lblCd_Plant.Text);
                cbo_CD_PLANT.Focus();
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
                if (!BeforeSearch()) return;

                object[] obj = new object[] { 
                    LoginInfo.CompanyCode, 
                    cbo_CD_PLANT.SelectedValue.ToString(), 
                    dp기준일자.Text, 
                    chk_품목사용체크된건만.Checked ? "Y" : "N", 
                    cboFG_ACCT.SelectedValue.ToString(), 
                    bp_CD_ITEM_START.CodeValue, 
                    bp_CD_ITEM_END.CodeValue, 
                    cboFG_ABC.SelectedValue.ToString(), 
                    bp_ITEM_GROUP.CodeValue,
                    bp_CLS_L.CodeValue,
                    bp_CLS_M.CodeValue,
                    bp_CLS_S.CodeValue,
                    bpc창고.QueryWhereIn_Pipe,
                    "N",
                    ""
                };

                DataTable dt = _biz.search(obj);

                if (dt == null || dt.Rows.Count == 0)
                {
                    _flex.Binding = null;
                    ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    return;
                }

                DataTable dtCross = _flex.GetCrossTable(dt, "NM_SL", "QT_INV");

                dtCross.DefaultView.Sort = "CD_ITEM";

                InitGrid_New(dtCross);

                dtCross.AcceptChanges();

                _flex.Binding = dtCross;

                colorstart();//컬러넣는부분

            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 인쇄

        public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!BeforePrint()) return;
                if (!_flex.HasNormalRow) return;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #endregion

        #region ♣ 도움창 이벤트 / 메소드

        #region -> 도움창 Clear 이벤트(OnBpControl_CodeChanged)

        private void OnBpControl_CodeChanged(object sender, System.EventArgs e)
        {
            try
            {
                ;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 호출전 세팅 이벤트(OnBpControl_QueryBefore)

        private void OnBpControl_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB:     // 공장품목 도움창(싱글)
                        e.HelpParam.P09_CD_PLANT = cbo_CD_PLANT.SelectedValue.ToString();
                        break;
                    case HelpID.P_MA_CODE_SUB:      // 대,중,소분류 도움창(싱글)
                        switch (e.ControlName)
                        {
                            case "bp_CLS_L":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000030";
                                break;
                            case "bp_CLS_M":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000031";
                                e.HelpParam.P42_CD_FIELD2 = PitemCls.CdFlag1(bp_CLS_L.CodeValue);
                                break;
                            case "bp_CLS_S":
                                e.HelpParam.P41_CD_FIELD1 = "MA_B000032";
                                e.HelpParam.P42_CD_FIELD2 = PitemCls.CdFlag1(bp_CLS_M.CodeValue);
                                break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> 도움창 호출후 변경 이벤트(OnBpControl_QueryAfter)

        private void OnBpControl_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            try
            {
                BpCodeTextBox bpControl = sender as BpCodeTextBox;
                if (bpControl == null) return;

                if (e.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    switch (bpControl.Name)
                    {
                        case "bp_CD_ITEM_START":
                            bp_CD_ITEM_START.CodeValue = e.CodeValue;
                            bp_CD_ITEM_START.CodeName = e.CodeName;
                            //조회조건에 앞품목을 선택하면 뒤 품목도 자동 입력되도록 수정(2011/01/13, by smjung)
                            bp_CD_ITEM_END.CodeValue = e.CodeValue;
                            bp_CD_ITEM_END.CodeName = e.CodeName;
                            break;
                        case "bp_CD_ITEM_END":
                            bp_CD_ITEM_END.CodeValue = e.CodeValue;
                            bp_CD_ITEM_END.CodeName = e.CodeName;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        #endregion

        #region -> GetColor ABC 구분해서 색깔넣는부분

        private void colorstart() //컬러넣는 for문
        {
            for (int i = _flex.Rows.Fixed; i < _flex.Rows.Count; i++) //색깔넣는부분
            {
                string iLevel = _flex.Rows[i]["FG_ABC"].ToString().Trim();


                if (iLevel != "")
                {

                    CellRange cr3 = _flex.GetCellRange(i, 1, i, _flex.Cols.Count - 1);//범위설정
                    cr3.Style = _flex.Styles.Add(iLevel);
                    cr3.Style.BackColor = GetColor(iLevel);//배경색
                    cr3.Style.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);//글자색깔
                }
            }
        }

        private Color GetColor(string iLevel) // 색깔넣는부분
        {
            Color clr = new Color();
            switch (iLevel)
            {
                case "A":
                    clr = Color.AntiqueWhite;
                    break;
                case "B":
                    clr = Color.LightSalmon;
                    break;
                case "C":
                    clr = Color.Silver;
                    break;
            }
            return clr;
        }

        #endregion

        #endregion

        #region ♣ 속성

        public bool 공장선택여부
        {
            get
            {
                if (cbo_CD_PLANT.SelectedValue == null || cbo_CD_PLANT.SelectedValue.ToString() == "")
                    return false;
                return true;
            }
        }

        private bool Chk공장
        {
            get
            {
                return !Checker.IsEmpty(cbo_CD_PLANT, DD("공장"));
            }
        }
        
        #endregion

        private void _flex_AfterSort(object sender, SortColEventArgs e)
        {
            colorstart();
        }

        private void _flex_BeforeSort(object sender, SortColEventArgs e)
        {

        }
    }
}

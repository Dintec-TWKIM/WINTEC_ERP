using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.Common.Forms.Help;

namespace sale
{
    public partial class P_SA_SO_SPITEM_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region -> 생성자 & 변수 선언
        private P_SA_SO_SPITEM_SUB_BIZ _biz = new P_SA_SO_SPITEM_SUB_BIZ(); 
        
        //리턴해줄 DataTable
        private DataTable ReturnDt = null;
        //재전개 때문에 받아놓을 DataTable
        private DataTable RefreshDt = null;

        private string Base_Dt = string.Empty;
        private decimal rt_Exch = 1;
        private string _거래처 = string.Empty;
        private string _단가유형 = string.Empty;
        private string _환종 = string.Empty;
        private string _단가적용형태 = string.Empty;
        private string so_Price = string.Empty;
        private decimal _부가세율 = 1;
        private string _공장 = "";
        private string _공장활성화여부 = "Y";

        public P_SA_SO_SPITEM_SUB(object[] obj)
        {
            try
            {
                InitializeComponent();

                Base_Dt = D.GetString(obj[0]);      //수주일자를 기준으로 상품의 품목들을 끌고 온다.
                rt_Exch = D.GetDecimal(obj[1]);     //환율을 가져와서 단가를 보여준다.
                _거래처 = D.GetString(obj[2]);
                _단가유형 = D.GetString(obj[3]);
                _환종 = D.GetString(obj[4]);
                _단가적용형태 = D.GetString(obj[5]);
                so_Price = D.GetString(obj[6]);
                _부가세율 = D.GetDecimal(obj[7]);
                if (obj.Length > 8)
                    _공장 = D.GetString(obj[8]);
                if (obj.Length > 9)
                    _공장활성화여부 = D.GetString(obj[9]);

                InitGrid();

                _flexH.DetailGrids = new FlexGrid[] { _flexL };
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        } 
        #endregion

        #region -> 초기화 이벤트 / 메소드

        #region -> InitGrid : 그리드 초기화
        private void InitGrid()
        { 
            #region 도움창 헤더그리드
            _flexH.BeginSetting(1, 1, false);
            _flexH.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            _flexH.SetDummyColumn("S");       // 클릭하고 저장하거나, 출력하면 상태가 원래 Changed 속성으로 바뀌면서 툴바가 활성화 되버린다.
                                              // 이런 현상을 막고 종료할때 메세지 묻는 현상을 방지 하기 위해 COM에 선언된 SetDummyColumn을 선언한다. 
            _flexH.SetCol("CD_SHOP", "접수유형코드", 120, false);
            _flexH.SetCol("NM_SHOP", "접수유형명", 120, false);
            _flexH.SetCol("CD_SPITEM", "상품코드", false);
            _flexH.SetCol("NM_SPITEM", "상품명", 120, false);
            _flexH.SetCol("CD_OPT", "옵션코드", 120, false);
            _flexH.SetCol("NM_OPT", "옵션명", 120, 100, false);
            //_flexH.SetCol("FG_VAT", "과세구분", 120, false);
            _flexH.SetCol("UM_SUPPLY_SUM", "공급단가", 100, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);// 공급단가
            _flexH.SetCol("UM_SALE_SUM", "판매단가", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);// 판매단가
            _flexH.SetCol("QTY", "수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);// 수량
            _flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            _flexH.ValidateEdit += new ValidateEditEventHandler(_flexH_ValidateEdit);
            _flexH.AfterRowChange +=new RangeEventHandler(_flexH_AfterRowChange);

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.LoadUserCache("P_SA_SO_SPITEM_SUB_flexH");
            #endregion

            #region 도움창 라인그리드
            _flexL.BeginSetting(1, 1, false);
            //_flexL.SetCol("S", "S", 30, true, CheckTypeEnum.Y_N);
            //_flexL.SetDummyColumn("S"); // 클릭하고 저장하거나, 출력하면 상태가 원래 Changed 속성으로 바뀌면서 툴바가 활성화 되버린다.
            //                              // 이런 현상을 막고 종료할때 메세지 묻는 현상을 방지 하기 위해 COM에 선언된 SetDummyColumn을 선언한다. 
            _flexL.SetCol("CD_PLANT", "공장", 100, false);// 공장
            _flexL.SetCol("CD_ITEM", "품목코드", 100, true);// 품번
            _flexL.SetCol("NM_ITEM", "품목명", 100, false);// 품명
            _flexL.SetCol("STND_ITEM", "규격", 70, false);// 규격
            _flexL.SetCol("UNIT", "단위", 80, 3, false);// 단위
            _flexL.SetCol("CD_SL", "창고코드", 80, true);
            _flexL.SetCol("NM_SL", "창고명", 120, false);
            _flexL.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            _flexL.SetCol("QT_SO", "수량", 90, 17, false, typeof(decimal), FormatTpType.QUANTITY);// 수량
            _flexL.SetCol("UM_SO", "공급단가", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);     // 판매단가
            _flexL.SetCol("AM_SO", "공급금액", 100, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);         // 판매금액
            _flexL.SetCol("AM_WONAMT", "원화금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);            // 원화금액
            _flexL.SetCol("UMVAT_SO", "판매단가", 100, 17, false, typeof(decimal), FormatTpType.MONEY);              // 공급단가
            _flexL.SetCol("AMVAT_SO", "판매금액", 100, 17, false, typeof(decimal), FormatTpType.MONEY);              // 공급금액
            _flexL.SetCol("AM_VAT", "부가세", 100, 17, false, typeof(decimal), FormatTpType.MONEY);
            _flexL.SetCol("RT_VAT", "부가세율", 0, 0, false, typeof(decimal), FormatTpType.MONEY);

            _flexL.SetCodeHelpCol("CD_ITEM", Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_SL", "NM_SL", "UNIT_SO_FACT" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "UNIT_IM", "TP_ITEM", "CD_GISL", "NM_GISL", "UNIT_SO_FACT" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flexL.SetCodeHelpCol("CD_SL", Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
            _flexL.VerifyCompare(_flexL.Cols["QT_SO"], 0, OperatorEnum.Greater);
            _flexL.VerifyCompare(_flexL.Cols["UM_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_SO"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.VerifyCompare(_flexL.Cols["AM_VAT"], 0, OperatorEnum.GreaterOrEqual);
            _flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            _flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flexL_BeforeCodeHelp);
            _flexL.AfterCodeHelp += new AfterCodeHelpEventHandler(_flexL_AfterCodeHelp);
            _flexL.ValidateEdit += new ValidateEditEventHandler(_flexL_ValidateEdit);

            //헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.
            _flexL.SetExceptSumCol("UM_SO", "UMVAT_SO");

            //사용자그리드셋팅 기능 : 반듯이 EndSetting 다음에 코딩해줘야한다. : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexL.LoadUserCache("P_SA_SO_SPITEM_SUB_flexL");
            #endregion
        } 
        #endregion

        #region -> InitPaint : 프리폼 초기화
        //PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
        //Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
        //뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
        protected override void InitPaint()
        {
            DataSet g_dsCombo = Global.MainFrame.GetComboData("NC;MA_PLANT", "S;MA_B000040");

            //공장
            cbo_Plant.DataSource = g_dsCombo.Tables[0];
            cbo_Plant.DisplayMember = "NAME";
            cbo_Plant.ValueMember = "CODE";
            _flexL.SetDataMap("CD_PLANT", g_dsCombo.Tables[0].Copy(), "CODE", "NAME");

            if (_공장 != "")
            {
                DataRow[] drs = g_dsCombo.Tables[0].Select("CODE = '" + _공장 + "' ");
                if (drs.Length == 1)
                {
                    cbo_Plant.SelectedValue = _공장;

                    if (_공장활성화여부 != "Y")
                        cbo_Plant.Enabled = false;
                }
            }

            //과세구분
            //_flexH.SetDataMap("FG_VAT", g_dsCombo.Tables[1], "CODE", "NAME");

            txt_SPITEM_Search.Text = string.Empty;
            bp_Pitem.CodeValue = string.Empty;
            bp_Pitem.CodeName = string.Empty;
            bp_CdSl.CodeValue = string.Empty;
            bp_CdSl.CodeName = string.Empty;

            //자동조회 막기
            //object[] obj = new object[5];
            //obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
            //obj[1] = cbo_SPTYPE.SelectedValue == null ? string.Empty : cbo_SPTYPE.SelectedValue.ToString();
            //obj[2] = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
            //obj[3] = Base_Dt;
            //obj[4] = rt_Exch;
            //DataSet ds = _biz.Search(obj);

            //foreach (DataRow dr in ds.Tables[1].Rows)
            //{
            //    dr["AM_SO"] = Decimal.Round(D.GetDecimal(dr["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(dr["RT_VAT"]))));
            //    dr["AM_VAT"] = D.GetDecimal(dr["AMVAT_SO"]) - D.GetDecimal(dr["AM_SO"]);
            //    dr["AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(dr["AM_SO"]) * rt_Exch);
            //    dr["UM_SO"] = D.GetDecimal(dr["AM_SO"]) / (D.GetDecimal(dr["QT_SO"]) == 0 ? 1 : D.GetDecimal(dr["QT_SO"]));
            //}

            //_flexH.Binding = ds.Tables[0];
            //_flexL.Binding = ds.Tables[1];
            //_flexL.RowFilter = "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'";

            //if(ds != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count != 0)  
            //    RefreshDt = ds.Tables[1].Copy();
        }
        #endregion

        #endregion 
 
        #region -> 버튼클릭 EVENT 
        private void btn_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    #region ♣ 상품검색 버튼클릭
                    case "btn_SPTYPE_Search":

                        if (txt_SPITEM_Search.Text == string.Empty) return;
                        if (!_flexH.HasNormalRow) return;

                        //_flexH.RowSel : 현재 선택되어 있는 즉, 포커스가 찍혀져 있는 로우의 idx를 가져온다.
                        for (int idx = _flexH.RowSel - 1; idx < _flexH.DataTable.Rows.Count; idx++)
                        {
                            if (D.GetString(_flexH[idx + 1, "CD_SPITEM"]).Contains(txt_SPITEM_Search.Text) ||
                                D.GetString(_flexH[idx + 1, "NM_SPITEM"]).Contains(txt_SPITEM_Search.Text) ||
                                D.GetString(_flexH[idx + 1, "CD_OPT"]).Contains(txt_SPITEM_Search.Text) ||
                                D.GetString(_flexH[idx + 1, "NM_OPT"]).Contains(txt_SPITEM_Search.Text))
                            {
                                _flexH.Select(idx + 1, _flexH.Cols["CD_SPITEM"].Index);
                                _flexH.Focus();
                                break;
                            }

                            if (idx + 1 == _flexH.DataTable.Rows.Count)
                            {
                                Global.MainFrame.ShowMessage("검색된 값이 없습니다!");
                                break;
                            }
                        }

                        break;

                    #endregion

                    #region ♣ 품목검색 버튼클릭
                    case "btn_Pitem_Search":

                        if (bp_Pitem.CodeValue == string.Empty) return;
                        if (!_flexH.HasNormalRow) return;

                        //_flexL.RowSel : 현재 선택되어 있는 즉, 포커스가 찍혀져 있는 로우의 idx를 가져온다.
                        int idx_Cnt = 1;
                        foreach (DataRow dr_Item in _flexH.DataTable.Rows)
                        {
                            //헤더에 포커스
                            _flexH.Select(idx_Cnt, _flexH.Cols["CD_SPITEM"].Index);
                            _flexH.Focus();

                            _flexL.RowFilter = "ISNULL(CD_SPITEM, '') = '" + D.GetString(dr_Item["CD_SPITEM"]) + "'";
                            for (int idx_Item = 0; idx_Item < _flexL.DataView.Count; idx_Item++)
                            {
                                if (D.GetString(_flexL[idx_Item + 1, "CD_ITEM"]) == bp_Pitem.CodeValue)
                                { 
                                    //라인에 포커스 (헤더에 있는 컬럼을 보여줘야 Error 이 안남)
                                    _flexL.Select(idx_Item + 1, _flexH.Cols["CD_SPITEM"].Index);
                                    _flexL.Focus();
                                    return;
                                } 
                            }

                            //헤더의 모든 루프를 돌아서 라인의 모든 행을 검색했을때 없으면 Exception 처리 
                            //헤더를 루프돈 숫자와 헤더숫자와 동일하면 모든 검색 루프를 돌았다.
                            if (idx_Cnt == _flexH.DataTable.Rows.Count)
                            {
                                Global.MainFrame.ShowMessage("검색된 값이 없습니다!");
                                break;
                            }
                            idx_Cnt++;

                        }

                        break;

                    #endregion

                    #region ♣ 조회 버튼클릭
                    case "btn_Search":

                        if (MA.ServerKey(true, new string[] { "NIKON" }))
                        {
                            if (ctx접수유형.IsEmpty())
                            {
                                Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, lbl_접수유형.Text);
                                return;
                            }
                        }

                        object[] obj = new object[11];
                        obj[0] = Global.MainFrame.LoginInfo.CompanyCode;
                        obj[1] = ctx접수유형.CodeValue;
                        obj[2] = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
                        obj[3] = Base_Dt;
                        obj[4] = rt_Exch;
                        obj[5] = _단가적용형태;
                        obj[6] = _거래처;
                        obj[7] = _단가유형;
                        obj[8] = _환종;
                        obj[9] = _부가세율;
                        obj[10] = bp_Pitem.CodeValue;

                        DataSet ds = _biz.Search(obj);

                        DataTable dtH = ds.Tables[0];
                        DataTable dtL = ds.Tables[1];

                        foreach (DataRow rowH in dtH.Rows)
                        {
                            string Filter = "ISNULL(CD_SHOP, '')   = '" + D.GetString(rowH["CD_SHOP"])   + "' AND " +
                                            "ISNULL(CD_SPITEM, '') = '" + D.GetString(rowH["CD_SPITEM"]) + "' AND " +
                                            "ISNULL(CD_OPT, '')    = '" + D.GetString(rowH["CD_OPT"])    + "'";
                            DataRow[] drs = dtL.Select(Filter);

                            decimal 총판매단가_BASE = decimal.Truncate(D.GetDecimal(rowH["UM_SALE_SUM_1"])); //상품코드등록에서 등록된 판매단가의 합 
                            decimal 총공급단가 = decimal.Truncate(D.GetDecimal(rowH["UM_SUPPLY_SUM"]));
                            decimal 총판매단가 = decimal.Truncate(D.GetDecimal(rowH["UM_SALE_SUM"]));
                            decimal 판매배부sum = 0m, 공급배부sum = 0m, Max금액 = 0m;
                            int i = 0, idx_Max금액 = 0; ;

                            foreach (DataRow rowL in drs)
                            {
                                rowL["AMVAT_SO"] = 총판매단가_BASE == 0 ? 0 : (decimal.Round(D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]) / 총판매단가_BASE * 총판매단가));
                                rowL["AM_SO"] = 총판매단가_BASE == 0 ? 0 : decimal.Round(D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]) / 총판매단가_BASE * 총공급단가);
                                rowL["UMVAT_SO"] = decimal.Round(D.GetDecimal(rowL["QT_SO"]) == 0 ? 0 : (D.GetDecimal(rowL["AMVAT_SO"]) / D.GetDecimal(rowL["QT_SO"])), 4, MidpointRounding.AwayFromZero);
                                rowL["UM_SO"] = decimal.Round(D.GetDecimal(rowL["QT_SO"]) == 0 ? 0 : (D.GetDecimal(rowL["AM_SO"]) / D.GetDecimal(rowL["QT_SO"])), 4, MidpointRounding.AwayFromZero);
                                rowL["AM_WONAMT"] = decimal.Truncate(D.GetDecimal(rowL["AM_SO"]) * rt_Exch);
                                rowL["AM_VAT"] = D.GetDecimal(rowL["AMVAT_SO"]) - D.GetDecimal(rowL["AM_WONAMT"]);
                                판매배부sum += D.GetDecimal(rowL["AMVAT_SO"]);
                                공급배부sum += D.GetDecimal(rowL["AM_SO"]);

                                if (Max금액 < D.GetDecimal(rowL["AMVAT_SO"]))
                                {
                                    Max금액 = D.GetDecimal(rowL["AMVAT_SO"]);
                                    idx_Max금액 = i;
                                }
                                i++;
                            }

                            //System.Diagnostics.Debug.WriteLine("idx_Max금액 -> " + D.GetString(idx_Max금액) + "drs : " + D.GetString(drs.Length));

                            if (drs.Length > 0)
                            {
                                drs[idx_Max금액]["AMVAT_SO"] = D.GetDecimal(drs[idx_Max금액]["AMVAT_SO"]) + (총판매단가 - 판매배부sum);
                                drs[idx_Max금액]["AM_SO"] = D.GetDecimal(drs[idx_Max금액]["AM_SO"]) + (총공급단가 - 공급배부sum);
                                drs[idx_Max금액]["AM_WONAMT"] = decimal.Truncate(D.GetDecimal(drs[idx_Max금액]["AM_SO"]) * rt_Exch);
                                drs[idx_Max금액]["AM_VAT"] = D.GetDecimal(drs[idx_Max금액]["AMVAT_SO"]) - D.GetDecimal(drs[idx_Max금액]["AM_WONAMT"]);
                            }
                            
                            /*foreach (DataRow rowL in drL)
                            {
                                RowCount++;

                                if (RowCount != drL.Length)
                                {
                                    percent = D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]) / 총판매단가;
                                    rowL["UMVAT_SO"] = Decimal.Round(percent * D.GetDecimal(rowH["UM_SALE_SUM"]));
                                    rowL["AMVAT_SO"] = D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]);
                                    rowL["AM_SO"] = Decimal.Round(D.GetDecimal(rowL["AMVAT_SO"]) * (100 / (100 + D.GetDecimal(rowL["RT_VAT"]))));//총공급단가 * percent;

                                    판매배부sum += D.GetDecimal(rowL["UMVAT_SO"]);
                                    공급배부sum += D.GetDecimal(rowL["AM_SO"]);
                                }
                                else
                                {
                                    rowL["UMVAT_SO"] = D.GetDecimal(rowH["UM_SALE_SUM"]) - 판매배부sum;
                                    rowL["AMVAT_SO"] = D.GetDecimal(rowL["UMVAT_SO"]) * D.GetDecimal(rowL["QT_SO"]);
                                    rowL["AM_SO"] = 총공급단가 - 공급배부sum;
                                }

                                rowL["AM_VAT"] = D.GetDecimal(rowL["AMVAT_SO"]) - D.GetDecimal(rowL["AM_SO"]);
                                rowL["AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(rowL["AM_SO"]) * rt_Exch);
                                rowL["UM_SO"] = D.GetDecimal(rowL["AM_SO"]) / (D.GetDecimal(rowL["QT_SO"]) == 0 ? 1 : D.GetDecimal(rowL["QT_SO"]));
                            }*/
                        }

                        RefreshDt = dtL.Copy();

                        _flexH.Binding = dtH;
                        _flexL.Binding = dtL;

                        if (ds == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                        {
                            btn_Apply01.Enabled = false;
                            Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                            return;
                        }
                        else
                            btn_Apply01.Enabled = true;

                        //_flexL.RowFilter = "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'";
                        _flexL.RowFilter = "ISNULL(CD_SHOP, '') = '" + D.GetString(_flexH["CD_SHOP"]) + "' AND " +
                                           "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "' AND "+
                                           "ISNULL(CD_OPT, '') = '" + D.GetString(_flexH["CD_OPT"]) + "'";

                        break;
                    #endregion 

                    #region ♣ 창고적용 버튼클릭
                    case "btn_Apply02":

                        if (bp_CdSl.CodeValue == string.Empty) return;
                        if (_flexL == null || _flexL.DataTable == null || _flexL.DataTable.Rows.Count == 0)
                        {
                            Global.MainFrame.ShowMessage("조회 후 적용 하시기 바랍니다.");
                            return;
                        }

                        //창고를 일괄변경한다.
                        _flexL.Redraw = false;

                        DataRow[] rows = _flexL.DataTable.Select("ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'");

                        foreach (DataRow row in rows)
                        {
                            row["CD_SL"] = bp_CdSl.CodeValue;
                            row["NM_SL"] = bp_CdSl.CodeName;
                        }
                        _flexL.Redraw = true;

                        Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, btn_Apply02.Text);

                        break;
                    #endregion

                    #region ♣ 재전개 버튼클릭
                    case "btn_ReSearch":

                        for (int idx_DelRow = _flexL.DataView.Count + 2; idx_DelRow > 2; idx_DelRow--)
                        {
                            _flexL.RemoveItem(_flexL.RowSel);
                        }

                        DataTable dt_Refresh = RefreshDt.Copy();
                        DataRow[] drs_ReFresh = dt_Refresh.Select("ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'");
                        if (drs_ReFresh == null || drs_ReFresh.Length == 0) return;
                        foreach (DataRow dr_AddRow in drs_ReFresh)
                        {
                            _flexL.DataTable.ImportRow(dr_AddRow);
                        }

                        _flexL.RowFilter = "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "'";

                        //라인에 포커스 (헤더에 있는 컬럼을 보여줘야 Error 이 안남)
                        _flexL.Select(2, _flexH.Cols["CD_SPITEM"].Index);
                        _flexL.Focus();

                        break;
                    #endregion

                    #region ♣ 라인추가 버튼클릭
                    case "btn_AddRow":
                        _flexL.Rows.Add();
                        _flexL.Row = _flexL.Rows.Count - 1;
                        _flexL["CD_SHOP"] = D.GetString(_flexH["CD_SHOP"]);
                        _flexL["CD_SPITEM"] = D.GetString(_flexH["CD_SPITEM"]);
                        _flexL["CD_OPT"] = D.GetString(_flexH["CD_OPT"]);
                        _flexL["FG_VAT"] = D.GetString(_flexH["FG_VAT"]);
                        _flexL["RT_VAT"] = D.GetDecimal(_flexH["RT_VAT"]);
                        _flexL["CD_PLANT"] = cbo_Plant.SelectedValue.ToString();
                        _flexL["QT_SO"] = 0;
                        _flexL["UM_SO"] = 0;
                        _flexL["AM_SO"] = 0;
                        _flexL["UMVAT_SO"] = 0;
                        _flexL["AMVAT_SO"] = 0;
                        _flexL["AM_WONAMT"] = 0;
                        _flexL["AM_VAT"] = 0;
                        _flexL.Col = _flexL.Cols.Fixed;
                        _flexL.AddFinished();
                        _flexL.Focus();

                        break;
                    #endregion

                    #region ♣ 라인삭제 버튼클릭
                    case "btn_DelRow":

                    if(_flexL.HasNormalRow)
                        _flexL.RemoveItem(_flexL.Row);

                        break;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> Control_QueryBefore
        private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
        {
            BpCodeTextBox bp_Control = sender as BpCodeTextBox;

            switch (e.HelpID)
            {
                case Duzon.Common.Forms.Help.HelpID.P_MA_PITEM_SUB:
                case Duzon.Common.Forms.Help.HelpID.P_MA_SL_SUB:
                    e.HelpParam.P09_CD_PLANT = cbo_Plant.SelectedValue == null ? string.Empty : cbo_Plant.SelectedValue.ToString();
                    break;
                case HelpID.P_USER:
                    ctx접수유형.UserParams = "접수유형도움창;H_EC_SPTYPE";
                    break;
            }
        }

        #endregion

        #region -> 그리드 EVENT 

        #region -> ValidateEdit Event

        #region -> _flexH_ValidateEdit
        private void _flexH_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            string oldValue = ((FlexGrid)sender).GetData(e.Row, e.Col).ToString();
            string newValue = ((FlexGrid)sender).EditData;

            if (oldValue.ToUpper() == newValue.ToUpper()) return;

            try
            {
                decimal 총판매단가_BASE = 0m, 총판매단가 = 0m, 총공급단가 = 0m;
                decimal 판매배부sum = 0m, 공급배부sum = 0m, Max금액 = 0m;
                int idx_Max금액 = 0;

                switch (_flexH.Cols[e.Col].Name)
                {
                    case "UM_SALE_SUM":

                        총판매단가_BASE = decimal.Truncate(D.GetDecimal(oldValue)); //상품코드등록에서 등록된 판매단가의 합 
                        총판매단가 = decimal.Truncate(D.GetDecimal(newValue) * (D.GetDecimal(_flexH[e.Row, "QTY"])));
                        _flexH[e.Row, "UM_SUPPLY_SUM"] = D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * (100 / (100 + _부가세율) * (D.GetDecimal(_flexH[e.Row, "QTY"])));
                        총공급단가 = decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SUPPLY_SUM"]));

                        for (int idx = 2; idx < _flexL.DataView.Count + 2; idx++)
                        {
                            _flexL[idx, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, 총판매단가_BASE == 0 ? 0 : (decimal.Round(D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) / 총판매단가_BASE * 총판매단가)));
                            _flexL[idx, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, 총판매단가_BASE == 0 ? 0 : (decimal.Round(D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) / 총판매단가_BASE * 총공급단가)));
                            _flexL[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 0 : (decimal.Round(D.GetDecimal(_flexL[idx, "AMVAT_SO"]) / D.GetDecimal(_flexL[idx, "QT_SO"]), 4, MidpointRounding.AwayFromZero)));
                            _flexL[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 0 : (decimal.Round(D.GetDecimal(_flexL[idx, "AM_SO"]) / D.GetDecimal(_flexL[idx, "QT_SO"]), 4 , MidpointRounding.AwayFromZero)));
                            _flexL[idx, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch));
                            _flexL[idx, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx, "AM_WONAMT"]));
                            판매배부sum += D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                            공급배부sum += D.GetDecimal(_flexL[idx, "AM_SO"]);

                            if (Max금액 < D.GetDecimal(_flexL[idx, "AMVAT_SO"]))
                            {
                                Max금액 = D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                                idx_Max금액 = idx;
                            }
                        }

                        _flexL[idx_Max금액, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AMVAT_SO"]) + (총판매단가 - 판매배부sum));
                        _flexL[idx_Max금액, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AM_SO"]) + (총공급단가 - 공급배부sum));
                        _flexL[idx_Max금액, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AM_SO"]) * rt_Exch);
                        _flexL[idx_Max금액, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx_Max금액, "AM_WONAMT"]));

                        //---------------------------------------------------------------------------------------

                        /*for (int idx = 2; idx < _flexL.DataView.Count + 2; idx++)
                        {
                            if (D.GetDecimal(_flexL[idx, "UMVAT_SO"]) == 0) continue;

                            _flexH[e.Row, "UM_SUPPLY_SUM"] = D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * (100 / (100 + _부가세율));

                            if (idx != rowCount)
                            {
                                percent = D.GetDecimal(_flexL[idx, "UMVAT_SO"]) / D.GetDecimal(oldValue);
                                _flexL[idx, "UMVAT_SO"] = Decimal.Round(percent * D.GetDecimal(newValue));
                                _flexL[idx, "AMVAT_SO"] = D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]);
                                _flexL[idx, "AM_SO"] = Decimal.Round(D.GetDecimal(_flexL[idx, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flexL[idx, "RT_VAT"]))));

                                판매배부sum += D.GetDecimal(_flexL[idx, "UMVAT_SO"]);
                                공급배부sum += D.GetDecimal(_flexL[idx, "AM_SO"]);
                            }
                            else
                            {
                                _flexL[idx, "UMVAT_SO"] = D.GetDecimal(newValue) - 판매배부sum;
                                _flexL[idx, "AMVAT_SO"] = D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]);
                                _flexL[idx, "AM_SO"] = D.GetDecimal(_flexH["UM_SUPPLY_SUM"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) - 공급배부sum;
                            }

                            _flexL[idx, "AM_VAT"] = D.GetDecimal(_flexL[idx, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx, "AM_SO"]);
                            _flexL[idx, "AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch);
                            _flexL[idx, "UM_SO"] = D.GetDecimal(_flexL[idx, "AM_SO"]) / (D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flexL[idx, "QT_SO"]));
                        }*/
                        break;

                    case "UM_SUPPLY_SUM":

                        총판매단가_BASE = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * (D.GetDecimal(_flexH[e.Row, "QTY"])))); //재계산 되기 전의 판매단가의 합 
                        _flexH[e.Row, "UM_SALE_SUM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(newValue) * ((100 + _부가세율) / 100));
                        총판매단가 = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * (D.GetDecimal(_flexH[e.Row, "QTY"]))));
                        총공급단가 = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(newValue) * (D.GetDecimal(_flexH[e.Row, "QTY"]))));

                        for (int idx = 2; idx < _flexL.DataView.Count + 2; idx++)
                        {
                            _flexL[idx, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, 총판매단가_BASE == 0 ? 0 : decimal.Round(D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) / 총판매단가_BASE * 총판매단가));
                            _flexL[idx, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, 총판매단가_BASE == 0 ? 0 : decimal.Round(D.GetDecimal(_flexL[idx, "UMVAT_SO"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) / 총판매단가_BASE * 총공급단가));
                            _flexL[idx, "UMVAT_SO"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 0 : decimal.Round(D.GetDecimal(_flexL[idx, "AMVAT_SO"]) / D.GetDecimal(_flexL[idx, "QT_SO"]), 4, MidpointRounding.AwayFromZero));
                            _flexL[idx, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) == 0 ? 0 : decimal.Round(D.GetDecimal(_flexL[idx, "AM_SO"]) / D.GetDecimal(_flexL[idx, "QT_SO"]), 4, MidpointRounding.AwayFromZero));
                            _flexL[idx, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch));
                            _flexL[idx, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx, "AM_WONAMT"]));
                            판매배부sum += D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                            공급배부sum += D.GetDecimal(_flexL[idx, "AM_SO"]);

                            if (Max금액 < D.GetDecimal(_flexL[idx, "AMVAT_SO"]))
                            {
                                Max금액 = D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                                idx_Max금액 = idx;
                            }
                        }

                        _flexL[idx_Max금액, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AMVAT_SO"]) + (총판매단가 - 판매배부sum));
                        _flexL[idx_Max금액, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AM_SO"]) + (총공급단가 - 공급배부sum));
                        _flexL[idx_Max금액, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexL[idx_Max금액, "AM_SO"]) * rt_Exch));
                        _flexL[idx_Max금액, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx_Max금액, "AM_WONAMT"]));
                        break;

                    case "QTY":

                        총판매단가 = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * D.GetDecimal(_flexH["QTY"])));
                        총공급단가 = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexH[e.Row, "UM_SUPPLY_SUM"]) * D.GetDecimal(_flexH["QTY"])));

                        for (int idx = 2; idx < _flexL.DataView.Count + 2; idx++)
                        {
                            _flexL[idx, "UNIT_SO_FACT"] = D.GetDecimal(_flexL[idx, "UNIT_SO_FACT"]) == 0 ? 1 : _flexL[idx, "UNIT_SO_FACT"];
                            _flexL[idx, "QT_SO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_BASE_SO"]) * D.GetDecimal(_flexH["QTY"]));
                            _flexL[idx, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_BASE_SO"]) * D.GetDecimal(_flexH["QTY"]) * D.GetDecimal(_flexL[idx, "UNIT_SO_FACT"]));

                            decimal new판매금액 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) * D.GetDecimal(_flexL[idx, "UMVAT_SO"]));
                            decimal new공급금액 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx, "QT_SO"]) * D.GetDecimal(_flexL[idx, "UM_SO"]));
                            //----------------------------------------
                            _flexL[idx, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, new판매금액);
                            _flexL[idx, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, new공급금액);
                            _flexL[idx, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, new판매금액 - new공급금액);
                            _flexL[idx, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch));

                            판매배부sum += new판매금액;
                            공급배부sum += new공급금액;

                            if (Max금액 < D.GetDecimal(_flexL[idx, "AMVAT_SO"]))
                            {
                                Max금액 = D.GetDecimal(_flexL[idx, "AMVAT_SO"]);
                                idx_Max금액 = idx;
                            }
                        }

                        _flexL[idx_Max금액, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AMVAT_SO"]) + (총판매단가 - 판매배부sum));
                        _flexL[idx_Max금액, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AM_SO"]) + (총공급단가 - 공급배부sum));
                        _flexL[idx_Max금액, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, decimal.Truncate(D.GetDecimal(_flexL[idx_Max금액, "AM_SO"]) * rt_Exch));
                        _flexL[idx_Max금액, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[idx_Max금액, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx_Max금액, "AM_WONAMT"]));
                            
                            //----------------------------------------
                            /*if (idx != rowCount)
                            {
                                _flexL[idx, "AMVAT_SO"] = new판매금액;
                                _flexL[idx, "AM_SO"] = new공급금액;
                                _flexL[idx, "AM_VAT"] = new판매금액 - new공급금액;
                                _flexL[idx, "AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch);

                                판매배부sum += new판매금액;
                                공급배부sum += new공급금액;
                            }
                            else
                            {
                                _flexL[idx, "AMVAT_SO"] = D.GetDecimal(_flexH[e.Row, "UM_SALE_SUM"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) - 판매배부sum;
                                _flexL[idx, "AM_SO"] = D.GetDecimal(_flexH[e.Row, "UM_SUPPLY_SUM"]) * D.GetDecimal(_flexL[idx, "QT_SO"]) - 공급배부sum;
                                _flexL[idx, "AM_VAT"] = D.GetDecimal(_flexL[idx, "AMVAT_SO"]) - D.GetDecimal(_flexL[idx, "AM_SO"]);
                                _flexL[idx, "AM_WONAMT"] = Decimal.Truncate(D.GetDecimal(_flexL[idx, "AM_SO"]) * rt_Exch);
                            }
                        }*/
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        
        #endregion

        #region -> _flexL_ValidateEdit
        private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                switch (_flexL.Cols[e.Col].Name)
                {
                    case "QT_SO":

                        _flexL[e.Row, "UNIT_SO_FACT"] = D.GetDecimal(_flexL[e.Row, "UNIT_SO_FACT"]) == 0 ? 1 : _flexL[e.Row, "UNIT_SO_FACT"];
                        _flexL[e.Row, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "QT_SO"]) * D.GetDecimal(_flexL[e.Row, "UNIT_SO_FACT"]));
                        _flexL[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "UMVAT_SO"]) * D.GetDecimal(_flexL[e.Row, "QT_SO"]));
                        _flexL[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, Decimal.Round(D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flexL[e.Row, "RT_VAT"])))));
                        _flexL[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flexL[e.Row, "AM_SO"]));
                        _flexL[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flexL[e.Row, "AM_SO"]) * rt_Exch));
                        _flexL[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AM_SO"]) / (D.GetDecimal(_flexL[e.Row, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flexL[e.Row, "QT_SO"])));

                        break;
                    case "UMVAT_SO": 
                        _flexL[e.Row, "AMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "QT_SO"]) * D.GetDecimal(_flexL[e.Row, "UMVAT_SO"]));
                        _flexL[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, Decimal.Round(D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flexL[e.Row, "RT_VAT"])))));
                        _flexL[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flexL[e.Row, "AM_SO"]));
                        _flexL[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flexL[e.Row, "AM_SO"]) * rt_Exch));
                        _flexL[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AM_SO"]) / (D.GetDecimal(_flexL[e.Row, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flexL[e.Row, "QT_SO"])));

                        break;
                    case "AMVAT_SO": 
                        if (D.GetDecimal(_flexL[e.Row, "QT_SO"]) != 0)
                            _flexL[e.Row, "UMVAT_SO"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) / D.GetDecimal(_flexL[e.Row, "QT_SO"]));
                        else
                            _flexL[e.Row, "UMVAT_SO"] = 0;

                        _flexL[e.Row, "AM_SO"] = Unit.외화금액(DataDictionaryTypes.SA, Decimal.Round(D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) * (100 / (100 + D.GetDecimal(_flexL[e.Row, "RT_VAT"])))));
                        _flexL[e.Row, "AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AMVAT_SO"]) - D.GetDecimal(_flexL[e.Row, "AM_SO"]));
                        _flexL[e.Row, "AM_WONAMT"] = Unit.원화금액(DataDictionaryTypes.SA, Decimal.Truncate(D.GetDecimal(_flexL[e.Row, "AM_SO"]) * rt_Exch));
                        _flexL[e.Row, "UM_SO"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "AM_SO"]) / (D.GetDecimal(_flexL[e.Row, "QT_SO"]) == 0 ? 1 : D.GetDecimal(_flexL[e.Row, "QT_SO"])));

                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region -> _flexH_AfterRowChange
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {  
            try
            {
                _flexL.RowFilter = "ISNULL(CD_SHOP, '') = '" + D.GetString(_flexH["CD_SHOP"]) + "' AND " +
                                   "ISNULL(CD_SPITEM, '') = '" + D.GetString(_flexH["CD_SPITEM"]) + "' AND " +
                                   "ISNULL(CD_OPT, '') = '" + D.GetString(_flexH["CD_OPT"]) + "'";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> _flexL_BeforeCodeHelp
        private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                switch (_flexL.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                    case "CD_SL":
                        e.Parameter.P09_CD_PLANT = _flexL[e.Row, "CD_PLANT"].ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> _flexL_AfterCodeHelp
        private void _flexL_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                HelpReturn helpReturn = e.Result;
                string 공장 = string.Empty;

                switch (_flexL.Cols[e.Col].Name)
                {
                    case "CD_ITEM":
                        if (e.Result.DialogResult == DialogResult.Cancel) return;

                        if (_flexL[e.Row, "CD_PLANT"].ToString() != string.Empty)
                            공장 = _flexL[e.Row, "CD_PLANT"].ToString();

                        _flexL.Redraw = false;
                        _flexL.SetDummyColumnAll();
                        foreach (DataRow row in helpReturn.Rows)
                        {
                            _flexL[e.Row, "CD_ITEM"] = row["CD_ITEM"];
                            _flexL[e.Row, "NM_ITEM"] = row["NM_ITEM"];
                            _flexL[e.Row, "STND_ITEM"] = row["STND_ITEM"];
                            _flexL[e.Row, "CD_SL"] = row["CD_GISL"];
                            _flexL[e.Row, "NM_SL"] = row["NM_GISL"];

                            _flexL[e.Row, "UNIT_SO"] = row["UNIT_SO"];
                            _flexL[e.Row, "UNIT_IM"] = row["UNIT_IM"];
                            _flexL[e.Row, "TP_ITEM"] = row["TP_ITEM"];

                            _flexL[e.Row, "UNIT_SO_FACT"] = D.GetDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];
                            _flexL[e.Row, "QT_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexL[e.Row, "QT_SO"]) * D.GetDecimal(row["UNIT_SO_FACT"]));
                        }
                        _flexL.RemoveDummyColumnAll();
                        _flexL.AddFinished();
                        _flexL.Col = _flexL.Cols.Fixed;
                        _flexL.Redraw = true;
                        break; 
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #endregion

        #region -> 결과값 리턴해줄 속성값 
        public DataTable ReturnDataTable
        {
            get
            {
                DataRow[] drs = _flexH.DataTable.Select("S = 'Y'");

                if (drs == null || drs.Length == 0) return null;

                ReturnDt = _flexL.DataTable.Clone();
                foreach (DataRow dr in drs)
                {
                    DataRow[] drs_Line = _flexL.DataTable.Select("ISNULL(CD_SPITEM, '') = '" + D.GetString(dr["CD_SPITEM"]) + "'");

                    foreach (DataRow dr_L in drs_Line)
                    {
                        DataRow row = ReturnDt.NewRow();
                        row = dr_L;
                        ReturnDt.Rows.Add(row.ItemArray);
                    }
                }

                return ReturnDt;
            }
        } 
        #endregion

        #region -> OnClosed(화면이 닫힐때)
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //사용자그리드셋팅 기능 : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.SaveUserCache("P_SA_SO_SPITEM_SUB_flexH");
            _flexL.SaveUserCache("P_SA_SO_SPITEM_SUB_flexL");
        }
        #endregion
    }
}
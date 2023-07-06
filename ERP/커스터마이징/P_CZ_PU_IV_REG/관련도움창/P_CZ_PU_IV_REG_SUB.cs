using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Util;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;

namespace cz
{
    public partial class P_CZ_PU_IV_REG_SUB : Duzon.Common.Forms.CommonDialog
    {
        #region -> 멤버필드
        private P_CZ_PU_IV_REG_SUB_BIZ _biz = new P_CZ_PU_IV_REG_SUB_BIZ();
        DataTable returnDt = new DataTable();
        private string 거래구분;
        private string 매입일자;
        public decimal 부가세 = 0;
        public decimal 외화금액 = 0;
        public decimal 원화금액 = 0;
        #endregion

        #region -> 초기화
        public P_CZ_PU_IV_REG_SUB(string 거래구분, string 매입일자)
        {
            this.InitializeComponent();

            this.거래구분 = 거래구분;
            this.매입일자 = 매입일자;
        }

        protected override void InitLoad()
        {
            try
            {
                base.InitLoad();

                this.InitGridH();
                this.InitGridL();
                this.InitEvent();

                this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void InitGridH()
        {
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("YN_CONFIRM", "확인요청", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_IO", "입고번호", 100);
            this._flexH.SetCol("NO_PO", "발주번호", 100); //발주번호 필드 추가 !! 
            this._flexH.SetCol("NO_ORDER", "견적번호", 100);
            this._flexH.SetCol("DT_IO", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("CD_PARTNER", "매입처", false);
            this._flexH.SetCol("LN_PARTNER", "매입처명", 130);
            this._flexH.SetCol("NM_VESSEL", "호선명", 150);
            this._flexH.SetCol("AM_PO_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_PO", "원화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_TOT", "총금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_ADPAY", "선지급금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("YN_TRANS", "이체여부", 60, false, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NM_PO_EMP", "발주담당자", 80); //담당자 필드 추가 !! 
            this._flexH.SetCol("NM_IO_EMP", "입고담당자", 80);
            //this._flexH.SetCol("NM_TRANS", "거래구분", 80);
            this._flexH.SetCol("DC_RMK", "비고", 150);

            this._flexH.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid3 });

            this._flexH.SetDummyColumn(new string[] { "S", "YN_CONFIRM" });
            this._flexH.ExtendLastCol = true;
            this._flexH.EnabledHeaderCheck = false;
            this._flexH.SettingVersion = "1.0.0.0";
            this._flexH.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.LoadUserCache("P_CZ_PU_IV_REG_SUB_flexH");
        }

        private void InitGridL()
        {
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("NO_PO", "발주번호", 80, false);
            this._flexL.SetCol("NO_DSP", "순번", 40);
            this._flexL.SetCol("NO_POLINE", "발주항번", 40);
            this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100); //아이템코드 필드 추가 !!
            this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 100); // 디스크립션 필드 추가!!
            this._flexL.SetCol("CD_ITEM", "품목코드", 100);
            this._flexL.SetCol("NM_ITEM", "품목명", 120);
            this._flexL.SetCol("STND_ITEM", "규격", 120);
            this._flexL.SetCol("UNIT_IM", "단위", 80);
            this._flexL.SetCol("QT_INV", "입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_IV", " 적용수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_INV_CLS", "매입잔량(관리)", false);
            this._flexL.SetCol("QT_CLS", "적용수량(관리)", false);
            this._flexL.SetCol("NM_EXCH", "통화명", 60);
            this._flexL.SetCol("RT_EXCH", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flexL.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("UM", "원화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
            this._flexL.SetCol("AM_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_IV", "원화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("VAT_IV", "부가세", 70, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_TOTAL", "총금액", 90, 17, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("AM_ADPAY", "선지급금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexL.SetCol("NM_TP_UM_TAX", "부가세여부", false);
            this._flexL.SetCol("NM_QTIOTP", "수불형태", 100);
            this._flexL.SetCol("PI_PARTNER", "주거래처코드", false);
            this._flexL.SetCol("PI_LN_PARTNER", "주거래처명", false);
            this._flexL.SetCol("GI_PARTNER", "납품처코드", false);
            this._flexL.SetCol("GI_LN_PARTNER", "납품처명", false);
            this._flexL.SetCol("NO_APP", "품의번호", false);
            this._flexL.SetCol("CD_PJT", "프로젝트코드", false);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", 100);
            this._flexL.SetCol("NM_TPPO", "발주유형", 100);
            this._flexL.SetCol("NM_TAX", "과세구분", 100, false);
            this._flexL.SetCol("FG_PAYMENT", "지급조건(발주)", false);

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexL.SetCol("CD_PJT_ITEM", "프로젝트 품목코드", false);
                this._flexL.SetCol("NM_PJT_ITEM", "프로젝트 품목명", false);
                this._flexL.SetCol("PJT_ITEM_STND", "프로젝트 품목규격", false);
                this._flexL.SetCol("NO_WBS", "WBS번호", false);
                this._flexL.SetCol("NO_CBS", "CBS번호", false);
                this._flexL.SetCol("CD_ACTIVITY", "ACTIVITY 코드", false);
                this._flexL.SetCol("NM_ACTIVITY", "ACTIVITY", false);
                this._flexL.SetCol("CD_COST", "원가코드", false);
                this._flexL.SetCol("NM_COST", "원가명", false);
            }

            this._flexL.SetCol("DC_RMK1", "비고1", false);
            this._flexL.SetCol("DC_RMK2", "비고2", false);
            this._flexL.SetCol("NM_PURGRP", "구매그룹", 100);
            this._flexL.SetCol("CD_CC", "CC코드", false);
            this._flexL.SetCol("NM_CC", "CC명", 80, false);
            this._flexL.SetCol("NM_KOR", "발주담당자", 80);
            this._flexL.SetCol("CD_SL", "창고코드", false);
            this._flexL.SetCol("NM_SL", "창고명", false);
            this._flexL.SetCol("FG_UM", "단가유형", false);
            this._flexL.SetCol("CD_PJTGRP", "프로젝트그룹코드", false);
            this._flexL.SetCol("NM_PJTGRP", "프로젝트그룹명", false);
            this._flexL.SetCol("DC1", "비고1", 100);
            this._flexL.SetCol("DC2", "비고2", 100);

            this._flexL.SetDummyColumn(new string[] { "S" });
            this._flexL.EnabledHeaderCheck = false;
            this._flexL.SettingVersion = "1.0.0.0";
            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            this._flexL.SetExceptSumCol(new string[] { "RT_EXCH" });
            this._flexL.LoadUserCache("P_CZ_PU_IV_REG_SUB_flexL");
        }

        private void InitEvent()
        {
            this.btn조회.Click += new EventHandler(this.btn조회_Click);
            this.btn전체선택.Click += new EventHandler(this.btn전체선택_Click);
            this.btn전체해제.Click += new EventHandler(this.btn전체해제_Click);
            this.btn적용.Click += new EventHandler(this.btn적용_Click);
            this.btn환율변경.Click += new EventHandler(this.btn환율변경_Click);
            this.btn부대비용적용.Click += new EventHandler(this.btn부대비용적용_Click);
            this.btn부대비용제거.Click += new EventHandler(this.btn부대비용제거_Click);
			this.btn확인요청.Click += Btn확인요청_Click;

            this.cur외화부대비용.Leave += new EventHandler(this.cur원화부대비용계산);
            this.cur원화부대비용.Leave += new EventHandler(this.cur외화부대비용계산);
            this.cur부대비용환율.TextChanged += new EventHandler(this.cur원화부대비용계산);

            this.cbo통화명.SelectionChangeCommitted += new EventHandler(this.SelectionChangeCommitted);
            this.cbo부대비용항목.SelectionChangeCommitted += new EventHandler(this.cbo부대비용항목_SelectionChangeCommitted);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.StartEdit += new RowColEventHandler(this._flexH_StartEdit);
            this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);

            this._flexL.AfterEdit += new RowColEventHandler(this._flexL_AfterEdit);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);
        }

        private void Btn확인요청_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;
            List<string> 수신자;
            string contents;

            try
            {
                if (!this._flexH.HasNormalRow)
                    return;

                dataRowArray = this._flexH.DataTable.Select("YN_CONFIRM = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flexH.DataTable.Select("YN_CONFIRM = 'Y' AND ISNULL(DC_RMK_CONFIRM, '') = ''").Length > 0)
                {
                    Global.MainFrame.ShowMessage("요청내용이 입력되지 않은 건이 선택되어 있습니다.");
                    return;
                }
                else if (this._flexH.DataTable.Select("YN_CONFIRM = 'Y' AND ISNULL(AM_EX, 0) = 0").Length > 0)
                {
                    Global.MainFrame.ShowMessage("대상금액이 입력되지 않은 건이 선택되어 있습니다.");
                    return;
                }
                else if (this._flexH.DataTable.Select("YN_CONFIRM = 'Y' AND ISNULL(AM_PO_EX, 0) >= ISNULL(AM_EX, 0)").Length > 0)
                {
                    Global.MainFrame.ShowMessage("차액이 같거나 마이너스인 건은 확인 요청할 수 없습니다.");
                    return;
                }
                else
                {
                    contents = @"** 세금계산서 확인 요청

매입처명 : {0}
파일번호 : {1}
입고금액 : {2}
계산서금액 : {3} 

{4}

확인 후 워크플로우 -> 계산서확인 단계를 통해서 회신 바랍니다. 

※ 본 쪽지는 발신전용 입니다.";

                    foreach (DataRow dr in dataRowArray)
                    {
                        DBHelper.ExecuteNonQuery("SP_CZ_PU_IV_CONFIRM_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                         "001",
                                                                                         string.Empty,
                                                                                         dr["NO_IO"].ToString(),
                                                                                         dr["NO_PO"].ToString(),
                                                                                         dr["DT_END"].ToString(),
                                                                                         dr["AM_EX"].ToString(),
                                                                                         dr["AM_PO_EX"].ToString(),
                                                                                         dr["NO_EMP"].ToString(),
                                                                                         dr["DC_RMK_CONFIRM"].ToString(),
                                                                                         Global.MainFrame.LoginInfo.UserID });

                        수신자 = new List<string>();
                        수신자.Add(dr["NO_EMP"].ToString());
                        
                        if (dr["NO_PO"].ToString().Contains("ST") && dr["CD_CC"].ToString() == "010900")
                            수신자.Add("S-495");

                        수신자.Add(Global.MainFrame.LoginInfo.UserID);

                        Messenger.SendMSG(수신자.ToArray(), string.Format(contents, dr["LN_PARTNER"].ToString(),
                                                                                    dr["NO_SO"].ToString(),
                                                                                    D.GetDecimal(dr["AM_PO_EX"].ToString()).ToString("N"),
                                                                                    D.GetDecimal(dr["AM_EX"].ToString()).ToString("N"),
                                                                                    dr["DC_RMK_CONFIRM"].ToString()));

                        dr["YN_CONFIRM"] = "N";
                    }

                    Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn확인요청.Text);
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            DataSet ds;

            try
            {
                base.InitPaint();

                this.dtp입고기간.StartDateToString = "20170101";
                this.dtp입고기간.EndDateToString = Global.MainFrame.GetStringToday;

                ds = Global.MainFrame.GetComboData(new string[] { "N;PU_C000016",
                                                                  "N;MA_B000005",
                                                                  "N;PU_C000001",
                                                                  "N;PU_C000014" });

                this.cbo거래구분.DataSource = ds.Tables[0];
                this.cbo거래구분.DisplayMember = "NAME";
                this.cbo거래구분.ValueMember = "CODE";

                this.cbo거래구분.SelectedValue = this.거래구분;

                this.cbo부대비용항목.DataSource = Global.MainFrame.FillDataTable(@"SELECT CD_ITEM, NM_ITEM, STND_ITEM, UNIT_IM 
                                                                                   FROM MA_PITEM WITH(NOLOCK)
                                                                                   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                                 @"AND CD_ITEM LIKE 'SD%'
                                                                                   UNION ALL
                                                                                   SELECT CD_ITEM, NM_ITEM, STND_ITEM, UNIT_IM 
                                                                                   FROM MA_PITEM WITH(NOLOCK)" + Environment.NewLine +
                                                                                  "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                                                  "AND CD_ITEM IN ('ADM005', 'ADM006', 'ADM007')");
                this.cbo부대비용항목.DisplayMember = "NM_ITEM";
                this.cbo부대비용항목.ValueMember = "CD_ITEM";

                if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                    this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                    this.cbo부대비용항목.SelectedValue.ToString() == "ADM007")
                {
                    this.cur외화부대비용.ReadOnly = true;
                    this.cur원화부대비용.ReadOnly = false;
                }
                else
                {
                    this.cur외화부대비용.ReadOnly = false;
                    this.cur원화부대비용.ReadOnly = true;
                }

                this.cbo부대비용통화명.DataSource = ds.Tables[1].Copy();
                this.cbo부대비용통화명.DisplayMember = "NAME";
                this.cbo부대비용통화명.ValueMember = "CODE";

                this.cbo통화명.DataSource = ds.Tables[1].Copy();
                this.cbo통화명.DisplayMember = "NAME";
                this.cbo통화명.ValueMember = "CODE";

                this._flexL.SetDataMap("FG_UM", ds.Tables[2], "CODE", "NAME");
                this._flexL.SetDataMap("FG_PAYMENT", ds.Tables[3], "CODE", "NAME");
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        #endregion

        #region -> 화면내버튼 클릭
        private void btn조회_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dataSet = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                  this.dtp입고기간.StartDateToString,
                                                                  this.dtp입고기간.EndDateToString,
                                                                  this.ctx매입처.CodeValue,
                                                                  this.txt발주번호.Text,
                                                                  this.txt입고번호.Text,
                                                                  D.GetString(this.cbo거래구분.SelectedValue) });

                this._flexH.Binding = dataSet.Tables[0];
                this._flexL.Binding = dataSet.Tables[1];
                this._flexL.EmptyRowFilter();
                this._flexH_AfterRowChange(null, null);

                if (!this._flexH.HasNormalRow)
                {
                    Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }

                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn전체선택_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                MsgControl.ShowMsg("전체선택 작업 중 입니다.\n잠시만 기다려 주세요.");

                this._flexH.Redraw = false;
                this._flexL.Redraw = false;

                for (int h = this._flexH.Rows.Fixed; h < this._flexH.Rows.Count; h++)
                {
                    MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h - 1), D.GetString(this._flexH.Rows.Count - 2) });

                    this._flexH.Row = h; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                    this._flexH[h, "S"] = "Y";
                    this._flexH.SetCellCheck(h, this._flexH.Cols["S"].Index, CheckEnum.Checked);

                    for (int l = this._flexL.Rows.Fixed; l < this._flexL.Rows.Count; l++)
                    {
                        this._flexL[l, "S"] = "Y";
                        this._flexL.SetCellCheck(l, this._flexL.Cols["S"].Index, CheckEnum.Checked);
                    }
                }

                this._flexH.Redraw = true;
                this._flexL.Redraw = true;

                MsgControl.CloseMsg();
                Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { Global.MainFrame.DD("전체선택") });

                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;

                MsgControl.CloseMsg();
            }
        }

        private void btn전체해제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

                MsgControl.ShowMsg("전체해제 작업 중 입니다.\n잠시만 기다려 주세요.");

                this._flexH.Redraw = false;
                this._flexL.Redraw = false;

                for (int h = this._flexH.Rows.Fixed; h < this._flexH.Rows.Count; h++)
                {
                    MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h - 1), D.GetString(this._flexH.Rows.Count - 2) });

                    this._flexH.Row = h; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.

                    this._flexH[h, "S"] = "N";
                    this._flexH.SetCellCheck(h, this._flexH.Cols["S"].Index, CheckEnum.Unchecked);

                    for (int l = this._flexL.Rows.Fixed; l < this._flexL.Rows.Count; l++)
                    {
                        this._flexL[l, "S"] = "N";
                        this._flexL.SetCellCheck(l, this._flexL.Cols["S"].Index, CheckEnum.Unchecked);
                    }
                }

                this._flexH.Redraw = true;
                this._flexL.Redraw = true;

                MsgControl.CloseMsg();
                Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { Global.MainFrame.DD("전체해제") });

                this._flexH_AfterRowChange(null, null);

                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;

                MsgControl.CloseMsg();
            }
        }

        private void btn적용_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.IsBindingEnd) return;

                DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
                }
                else if (this._flexL.DataTable.Select("S = 'Y' AND CD_PARTNER NOT IN ('11698', '17221')", "", DataViewRowState.CurrentRows).Length > 0 && 
                         ComFunc.getGridGroupBy(dataRowArray, new string[] { "RT_EXCH" }, 1 != 0).Rows.Count > 1) // 부강유통, 케이엠에스는 환율 다르더라도 적용 가능 하도록 수정
                {
                    Global.MainFrame.ShowMessage("환율이 동일하여야 합니다");
                    return;
                }
                else
                {
                    this.returnDt = this._flexL.DataTable.Clone();

                    foreach (DataRow row in dataRowArray)
                    {
                        if (((DataTable)this.cbo부대비용항목.DataSource).Select("CD_ITEM = '" + row["CD_ITEM"] + "'").Length > 0 && row.RowState == DataRowState.Added)
                        {
                            row["QT_INV"] = 0;
                            row["QT_IV"] = 0;
                            row["QT_INV_CLS"] = 0;
                            row["QT_CLS"] = 0;
                        }

                        row.AcceptChanges();

                        this.returnDt.ImportRow(row);
                    }

                    DataView defaultView = this.returnDt.DefaultView;
                    defaultView.Sort = "NO_IO  ASC , NO_IOLINE  ASC";
                    this.returnDt = defaultView.ToTable();

                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn환율변경_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexH.HasNormalRow == false || this._flexL.HasNormalRow == false) return;

                this._flexH.Redraw = false;
                this._flexL.Redraw = false;

                foreach (DataRow dr in this._flexH.DataTable.Select("S = 'Y'"))
                {
                    foreach (DataRow dr1 in this._flexL.DataTable.Select("S = 'Y' AND NO_IO = '" + D.GetString(dr["NO_IO"]) + "'"))
                    {
                        dr1["CD_EXCH"] = this.cbo통화명.SelectedValue;
                        dr1["NM_EXCH"] = this.cbo통화명.Text;
                        dr1["RT_EXCH"] = this.cur환율.DecimalValue;
                        dr1["CHG_RTEXCH"] = "Y";

                        dr1["UM"] = this.원화계산(D.GetDecimal(dr1["UM_EX"]) * D.GetDecimal(dr1["RT_EXCH"]));
                        dr1["AM_IV"] = this.원화계산(D.GetDecimal(dr1["AM_EX"]) * D.GetDecimal(dr1["RT_EXCH"]));
                        dr1["VAT_IV"] = this.원화계산(D.GetDecimal(dr1["AM_IV"]) * (D.GetDecimal(dr1["TAX_RATE"]) == 0 ? 0 : D.GetDecimal(dr1["TAX_RATE"]) / 100));
                        dr1["AM_TOTAL"] = this.원화계산((D.GetDecimal(dr1["AM_IV"]) + D.GetDecimal(dr1["VAT_IV"])));
                    }

                    dr["AM_PO"] = this._flexL.DataTable.Compute("SUM(AM_IV)", "NO_IO = '" + D.GetString(dr["NO_IO"]) + "'");
                    dr["VAT"] = this._flexL.DataTable.Compute("SUM(VAT_IV)", "NO_IO = '" + D.GetString(dr["NO_IO"]) + "'");
                    dr["AM_TOT"] = (D.GetDecimal(dr["AM_PO"]) + D.GetDecimal(dr["VAT"]));
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            finally
            {
                this._flexH.Redraw = true;
                this._flexL.Redraw = true;

                this._flexH.SumRefresh();
                this._flexL.SumRefresh();

                this.선택금액계산();
            }
        }

        private void btn부대비용적용_Click(object sender, EventArgs e)
        {
            DataRow dr입고항목, dr부대비용항목, dr부대비용;
            string filter;

            try
            {
                if (!this._flexL.HasNormalRow) return;
                if (!this._flexH.HasNormalRow && !this._flexL.HasNormalRow) return;

                filter = "NO_IO = '" + this._flexH["NO_IO"].ToString() + "' AND FG_TAX = '" + this._flexH["FG_TAX"].ToString() + "'";
                filter += " AND NO_PO = '" + D.GetString(this.cbo부대비용발주번호.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(this.cbo부대비용항목.SelectedValue) + "'";

                if (this._flexL.DataTable.Select(filter).Length > 0)
                {
                    #region 부대비용수정
                    dr부대비용 = this._flexL.DataTable.Select(filter)[0];

                    dr부대비용["UM_EX"] = this.외화계산(this.cur외화부대비용.DecimalValue);
                    dr부대비용["AM_EX"] = this.외화계산(D.GetDecimal(dr부대비용["QT_INV_CLS"]) * D.GetDecimal(dr부대비용["UM_EX"]));

                    if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM007")
                        dr부대비용["UM"] = this.원화계산(this.cur원화부대비용.DecimalValue);
                    else
                        dr부대비용["UM"] = this.원화계산(D.GetDecimal(dr부대비용["UM_EX"]) * D.GetDecimal(dr부대비용["RT_EXCH"]));
                    
                    dr부대비용["AM_IV"] = this.원화계산(D.GetDecimal(dr부대비용["QT_INV_CLS"]) * D.GetDecimal(dr부대비용["UM"]));

                    if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM006")
					{
                        dr부대비용["VAT_IV"] = this.원화계산(0);
                    }
                    else
					{
                        if (D.GetDecimal(dr부대비용["TAX_RATE"]) != 0)
                            dr부대비용["VAT_IV"] = this.원화계산(D.GetDecimal(dr부대비용["AM_IV"]) * D.GetDecimal(dr부대비용["TAX_RATE"]) / 100);
                        else
                            dr부대비용["VAT_IV"] = this.원화계산(0);
                    }
                    
                    dr부대비용["AM_TOTAL"] = this.원화계산((D.GetDecimal(dr부대비용["AM_IV"]) + D.GetDecimal(dr부대비용["VAT_IV"])) - D.GetDecimal(dr부대비용["AM_ADPAY"]));
                    #endregion
                }
                else
                {
                    #region 부대비용추가
                    filter = "NO_IO = '" + this._flexH["NO_IO"].ToString() + "' AND FG_TAX = '" + this._flexH["FG_TAX"].ToString() + "'";
                    filter += " AND NO_PO = '" + D.GetString(this.cbo부대비용발주번호.SelectedValue) + "'";
                    filter += " AND NO_IOLINE = '" + D.GetString(this._flexL.DataTable.Compute("MAX(NO_IOLINE)", filter)) + "'";

                    dr입고항목 = this._flexL.DataTable.Select(filter)[0];
                    dr부대비용항목 = ((DataRowView)this.cbo부대비용항목.SelectedItem).Row;

                    this._flexL.Rows.Add();

                    this._flexL.Row = this._flexL.Rows.Count - 1;

                    this._flexL["S"] = "Y";
                    this._flexL["NO_IO"] = this._flexH["NO_IO"];
                    this._flexL["NO_IOLINE"] = D.GetString(dr입고항목["NO_IOLINE"]);
                    this._flexL["DT_IO"] = this._flexH["DT_IO"];
                    this._flexL["FG_TAX"] = this._flexH["FG_TAX"];

                    this._flexL["NO_PO"] = D.GetString(this.cbo부대비용발주번호.SelectedValue);
                    this._flexL["NO_POLINE"] = D.GetString(dr입고항목["NO_POLINE"]);
                    this._flexL["CD_ITEM"] = D.GetString(dr부대비용항목["CD_ITEM"]);
                    this._flexL["NM_ITEM"] = D.GetString(dr부대비용항목["NM_ITEM"]);
                    this._flexL["STND_ITEM"] = D.GetString(dr부대비용항목["STND_ITEM"]);
                    this._flexL["UNIT_IM"] = D.GetString(dr부대비용항목["UNIT_IM"]);
                    this._flexL["CD_PARTNER"] = D.GetString(dr입고항목["CD_PARTNER"]);
                    this._flexL["LN_PARTNER"] = D.GetString(dr입고항목["LN_PARTNER"]);
                    this._flexL["CD_DOCU"] = D.GetString(dr입고항목["CD_DOCU"]);
                    this._flexL["TP_UM_TAX"] = D.GetString(dr입고항목["TP_UM_TAX"]);
                    this._flexL["NM_TP_UM_TAX"] = D.GetString(dr입고항목["NM_TP_UM_TAX"]);
                    this._flexL["NM_TAX"] = D.GetString(dr입고항목["NM_TAX"]);
                    this._flexL["CD_QTIOTP"] = D.GetString(dr입고항목["CD_QTIOTP"]);
                    this._flexL["NM_QTIOTP"] = D.GetString(dr입고항목["NM_QTIOTP"]);
                    this._flexL["FG_UM"] = D.GetString(dr입고항목["FG_UM"]);
                    this._flexL["NO_EMP"] = D.GetString(dr입고항목["NO_EMP"]);
                    this._flexL["NM_KOR"] = D.GetString(dr입고항목["NM_KOR"]);
                    this._flexL["CD_CC"] = D.GetString(dr입고항목["CD_CC"]);
                    this._flexL["NM_CC"] = D.GetString(dr입고항목["NM_CC"]);
                    this._flexL["CD_PLANT"] = D.GetString(dr입고항목["CD_PLANT"]);
                    this._flexL["CD_GROUP"] = D.GetString(dr입고항목["CD_GROUP"]);
                    this._flexL["FG_TPPURCHASE"] = D.GetString(dr입고항목["FG_TPPURCHASE"]);
                    this._flexL["YN_RETURN"] = D.GetString(dr입고항목["YN_RETURN"]);
                    this._flexL["NO_LC"] = D.GetString(dr입고항목["NO_LC"]);
                    this._flexL["CD_PJT"] = D.GetString(dr입고항목["CD_PJT"]);
                    this._flexL["NM_PROJECT"] = D.GetString(dr입고항목["NM_PROJECT"]);
                    this._flexL["NM_TPPO"] = D.GetString(dr입고항목["NM_TPPO"]);
                    this._flexL["NM_PURGRP"] = D.GetString(dr입고항목["NM_PURGRP"]);

                    #region 금액계산
                    this._flexL["NM_EXCH"] = cbo부대비용통화명.Text;
                    this._flexL["CD_EXCH"] = cbo부대비용통화명.SelectedValue;
                    this._flexL["RT_EXCH"] = cur부대비용환율.DecimalValue;

                    this._flexL["QT_INV"] = 1;
                    this._flexL["QT_IV"] = 1;
                    this._flexL["QT_INV_CLS"] = 1;
                    this._flexL["QT_CLS"] = 1;

                    this._flexL["AM_ADPAY"] = 0;

                    this._flexL["UM_EX"] = this.외화계산(this.cur외화부대비용.DecimalValue);
                    this._flexL["AM_EX"] = this.외화계산(D.GetDecimal(this._flexL["QT_INV_CLS"]) * D.GetDecimal(this._flexL["UM_EX"]));

                    if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM007")
                        this._flexL["UM"] = this.원화계산(this.cur원화부대비용.DecimalValue);
                    else
                        this._flexL["UM"] = this.원화계산(D.GetDecimal(this._flexL["UM_EX"]) * D.GetDecimal(this._flexL["RT_EXCH"]));
                    
                    this._flexL["AM_IV"] = this.원화계산(D.GetDecimal(this._flexL["QT_INV_CLS"]) * D.GetDecimal(this._flexL["UM"]));

                    if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                        this.cbo부대비용항목.SelectedValue.ToString() == "ADM006")
                    {
                        this._flexL["TAX_RATE"] = 0;
                        this._flexL["VAT_IV"] = this.원화계산(0);
                    }
                    else
					{
                        this._flexL["TAX_RATE"] = D.GetDecimal(dr입고항목["TAX_RATE"].ToString());

                        if (D.GetDecimal(this._flexL["TAX_RATE"]) != 0)
                            this._flexL["VAT_IV"] = this.원화계산(D.GetDecimal(this._flexL["AM_IV"]) * D.GetDecimal(this._flexL["TAX_RATE"]) / 100);
                        else
                            this._flexL["VAT_IV"] = this.원화계산(0);
                    }

                    this._flexL["AM_TOTAL"] = this.원화계산((D.GetDecimal(this._flexL["AM_IV"]) + D.GetDecimal(this._flexL["VAT_IV"])) - D.GetDecimal(this._flexL["AM_ADPAY"]));
                    #endregion

                    this._flexL.Col = this._flexL.Cols.Fixed;
                    this._flexL.AddFinished();
                    this._flexL.Focus();
                    #endregion
                }

                this.SetHeaderAmt(this._flexH["NO_IO"].ToString(), this._flexH["FG_TAX"].ToString());

                this._flexH.SumRefresh();
                this._flexL.SumRefresh();
                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void btn부대비용제거_Click(object sender, EventArgs e)
        {
            string filter;
            DataRow[] dataRowArray;

            try
            {
                filter = "NO_IO = '" + this._flexH["NO_IO"].ToString() + "' AND FG_TAX = '" + this._flexH["FG_TAX"].ToString() + "'";
                filter += " AND NO_PO = '" + D.GetString(this.cbo부대비용발주번호.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(this.cbo부대비용항목.SelectedValue) + "'";

                dataRowArray = this._flexL.DataTable.Select(filter, string.Empty, DataViewRowState.Added);

                if (dataRowArray.Length > 0)
                {
                    dataRowArray[0].Delete();
                    this.SetHeaderAmt(D.GetString(this._flexH["NO_IO"]), D.GetString(this._flexH["FG_TAX"]));

                    this._flexH.SumRefresh();
                    this._flexL.SumRefresh();
                    this.선택금액계산();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void cur원화부대비용계산(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                    this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                    this.cbo부대비용항목.SelectedValue.ToString() == "ADM007") return;

                this.cur원화부대비용.DecimalValue = this.원화계산(this.cur외화부대비용.DecimalValue * this.cur부대비용환율.DecimalValue);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void cur외화부대비용계산(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo부대비용항목.SelectedValue.ToString() != "ADM005" &&
                    this.cbo부대비용항목.SelectedValue.ToString() != "ADM006" &&
                    this.cbo부대비용항목.SelectedValue.ToString() != "ADM007") return;

                this.cur외화부대비용.DecimalValue = 0;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //사용자그리드셋팅 기능 : 반듯이 파라미터변수는 Page명 + Grid명 으로 한다.
            _flexH.SaveUserCache("P_CZ_PU_IV_REG_SUB_flexH");
            _flexL.SaveUserCache("P_CZ_PU_IV_REG_SUB_flexL");
        }

        private void SetHeaderAmt(string NO_IO, string FG_TAX)
        {
            string filterExpression = "NO_IO = '" + NO_IO + "' AND FG_TAX = '" + FG_TAX + "'";

            DataRow[] dataRowArray = this._flexH.DataTable.Select(filterExpression, "", DataViewRowState.CurrentRows);
            if (dataRowArray == null || dataRowArray.Length == 0) return;

            DataRow dataRow1 = dataRowArray[0];
            dataRow1["AM_PO_EX"] = this._flexL.DataTable.Compute("SUM(AM_EX)", filterExpression);
            dataRow1["AM_PO"] = this._flexL.DataTable.Compute("SUM(AM_IV)", filterExpression);
            dataRow1["VAT"] = this._flexL.DataTable.Compute("SUM(VAT_IV)", filterExpression);
            dataRow1["AM_TOT"] = (D.GetDecimal(dataRow1["AM_PO"]) + D.GetDecimal(dataRow1["VAT"]));
        }

        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (((Control)sender).Name == this.cbo통화명.Name)
            {
                this.cur환율.Text = this._biz.환율(this.매입일자, this.cbo통화명.SelectedValue.ToString()).ToString();
            }
        }

        private void cbo부대비용항목_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.cbo부대비용항목.SelectedValue.ToString() == "ADM005" ||
                this.cbo부대비용항목.SelectedValue.ToString() == "ADM006" ||
                this.cbo부대비용항목.SelectedValue.ToString() == "ADM007")
            {
                this.cur외화부대비용.ReadOnly = true;
                this.cur원화부대비용.ReadOnly = false;
            }
            else
            {
                this.cur외화부대비용.ReadOnly = false;
                this.cur원화부대비용.ReadOnly = true;
            }
        }
        #endregion

        #region -> 리턴데이터
        public DataTable 리턴데이터
        {
            get { return returnDt; }
        }
        #endregion

        #region -> 그리드 이벤트
        private void _flexH_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                this._flexL.DataTable.Select("NO_IO= '" + this._flexH[this._flexH.Row, "NO_IO"].ToString() + "' AND CD_PARTNER= '" + this._flexH[this._flexH.Row, "CD_PARTNER"].ToString() + "' AND  FG_TAX = ISNULL('" + this._flexH[this._flexH.Row, "FG_TAX"].ToString() + "','')", "", DataViewRowState.CurrentRows);

                if (this._flexH[e.Row, "S"].ToString() == "N")
                {
                    this._flexL.Redraw = false;
                    for (int i = this._flexL.Rows.Fixed; i < this._flexL.DataView.Count + this._flexL.Rows.Fixed; ++i)
                        this._flexL.SetCellCheck(i, 1, CheckEnum.Checked);
                    this._flexL.Redraw = true;
                }
                else
                {
                    this._flexL.Redraw = false;
                    for (int i = this._flexL.Rows.Fixed; i < this._flexL.DataView.Count + this._flexL.Rows.Fixed; ++i)
                        this._flexL.SetCellCheck(i, 1, CheckEnum.Unchecked);
                    this._flexL.Redraw = true;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_AfterEdit(object sender, RowColEventArgs e)
        {
            string 통제설정_총마감금액;

            try
            {
                통제설정_총마감금액 = BASIC.GetMAEXC("매입등록-총마감금액지정");

                if (통제설정_총마감금액 == "100" || 통제설정_총마감금액 == "200")
                {
                    DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
                    if (dataRowArray.Length == 0)
                    {
                        return;
                    }
                    if (ComFunc.getGridGroupBy(dataRowArray, new string[1] { "CD_PARTNER" }, 1 != 0).Rows.Count > 1)
                    {
                        this._flexH[this._flexH.Row, "S"] = "N";
                        Global.MainFrame.ShowMessage("CZ_하나의 @만 선택할 수 있습니다.", Global.MainFrame.DD("매입처"));
                    }
                }

                for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
                    this._flexL.SetData(@fixed, 1, D.GetString(this._flexH[this._flexH.Row, "S"]));

                this.선택금액계산();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            DataTable dt;
            string filter;

            try
            {
                if (!this._flexH.IsBindingEnd || !this._flexH.HasNormalRow)
                {
                    this._flexL.EmptyRowFilter();
                }
                else
                {
                    string key = this._flexH["NO_IO"].ToString();
                    string key1 = D.GetString(this._flexH["FG_TAX"]);
                    filter = "NO_IO = '" + key + "' AND ISNULL(FG_TAX,'') = '" + key1 + "'";

                    this._flexL.RowFilter = filter;

                    if (this._flexL.HasNormalRow == true)
                    {
                        dt = ComFunc.getGridGroupBy(this._flexL.DataTable.Select(this._flexL.RowFilter), new string[] { "NO_PO", "CD_EXCH", "RT_EXCH" }, true);
                        dt.Columns.Add("NM_DISPLAY");

                        foreach (DataRow dr in dt.Rows)
                        {
                            filter = "NO_IO = '" + D.GetString(this._flexH["NO_IO"]) + "' AND NO_PO = '" + D.GetString(dr["NO_PO"]) + "'";
                            dr["NM_DISPLAY"] = D.GetString(dr["NO_PO"]) + " (" + string.Format("{0:" + this._flexL.Cols["AM_TOTAL"].Format + "}", D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_TOTAL)", filter))) + ")";
                        }

                        this.cbo부대비용발주번호.DataSource = dt;
                        this.cbo부대비용발주번호.DisplayMember = "NM_DISPLAY";
                        this.cbo부대비용발주번호.ValueMember = "NO_PO";

                        if (this.cbo부대비용발주번호.SelectedItem != null)
                        {
                            this.cbo부대비용통화명.SelectedValue = D.GetString(((DataRowView)(this.cbo부대비용발주번호.SelectedItem)).Row["CD_EXCH"]);
                            this.cur부대비용환율.DecimalValue = D.GetDecimal(((DataRowView)(this.cbo부대비용발주번호.SelectedItem)).Row["RT_EXCH"]);
                        }
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
            try
            {
                if (this._flexL.Cols[e.Col].Name != "S") return;

                this.선택금액계산();
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
                FlexGrid _flex = sender as FlexGrid;
                if (_flex == null) return;

                string ColName = ((FlexGrid)sender).Cols[e.Col].Name;
                string @string = D.GetString(this._flexL.GetData(e.Row, e.Col));

                switch (_flex.Cols[e.Col].Name)
                {
                    case "S":
                        _flex["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";

                        if (_flex.Name == "_flexL")
                        {
                            DataRow[] drArr = _flex.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows);

                            if (drArr.Length != 0)
                                this._flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Checked);
                            else
                                this._flexH.SetCellCheck(_flexH.Row, _flexH.Cols["S"].Index, CheckEnum.Unchecked);
                        }
                        break;
                    case "QT_IV":
                        if (!this.CheckRowDataQT(double.Parse(this._flexL[this._flexL.Row, "QT_INV"].ToString()), double.Parse(this._flexL.EditData)))
                        {
                            Global.MainFrame.ShowMessage("PU_M000081");
                            e.Cancel = true;
                            break;
                        }

                        if (D.GetString(this._flexL["YN_RETURN"]) == "Y" && D.GetDecimal(this._flexL.EditData) > 0)
                        {
                            Global.MainFrame.ShowMessage("CZ_@ 은(는) 0보다 작거나 같아야 합니다.", Global.MainFrame.DD("반품수량"));
                            this._flexL["QT_IV"] = D.GetDecimal(@string);
                            e.Cancel = true;
                            break;
                        }

                        this.ChangeQT_IV(D.GetDecimal(this._flexL.EditData));
                        this.선택금액계산();
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private bool CheckRowDataQT(double oldQT, double newQT)
        {
            try
            {
                if (this._flexL[this._flexL.Row, "YN_RETURN"].ToString().Trim() == "Y")
                {
                    if (newQT < oldQT)
                        return false;
                }
                else if (newQT > oldQT)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            return true;
        }

        private void ChangeQT_IV(Decimal 적용수량)
        {
            try
            {
                Decimal num = 적용수량 * D.GetDecimal(this._flexL["RATE_EXCHG"]);
                Decimal decimal1 = D.GetDecimal(this._flexL["UM_EX"]);
                Decimal decimal2 = D.GetDecimal(this._flexL["RT_EXCH"]);
                string @string = D.GetString(this._flexL["FG_TAX"]);
                bool 부가세포함 = D.GetString(this._flexL["TP_UM_TAX"]) == "001";
                Decimal decimal3 = D.GetDecimal(this._flexL["TAX_RATE"]);
                Duzon.ERPU.MF.Common.Calc.GetAmt(적용수량, decimal1, decimal2, @string, decimal3, 모듈.PUR, 부가세포함, out this.외화금액, out this.원화금액, out this.부가세);
                this._flexL["VAT_IV"] = this.부가세;
                this._flexL["AM_EX"] = this.외화금액;
                this._flexL["AM_IV"] = this.원화금액;
                this._flexL["AM_TOTAL"] = this.원화계산((this.원화금액 + this.부가세) - D.GetDecimal(this._flexL["AM_ADPAY"]));
                this._flexL["QT_CLS"] = num;
                this.ChangeHeadAM_K();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void ChangeHeadAM_K()
        {
            try
            {
                this._flexH[this._flexH.Row, "AM_PO"] = this.원화계산((decimal)this._flexL.DataTable.Compute("SUM(AM_IV)", "NO_IO= '" + this._flexH["NO_IO"].ToString().Trim() + "' AND CD_PARTNER= '" + this._flexH["CD_PARTNER"].ToString().Trim() + "' AND FG_TAX ='" + this._flexH["FG_TAX"].ToString().Trim() + "'"));
                this._flexH[this._flexH.Row, "VAT"] = this.원화계산((decimal)this._flexL.DataTable.Compute("SUM(VAT_IV)", "NO_IO= '" + this._flexH["NO_IO"].ToString().Trim() + "' AND CD_PARTNER= '" + this._flexH["CD_PARTNER"].ToString().Trim() + "' AND FG_TAX ='" + this._flexH["FG_TAX"].ToString().Trim() + "'"));
                this._flexH[this._flexH.Row, "AM_TOT"] = this.원화계산(D.GetDecimal(this._flexH[this._flexH.Row, "AM_PO"]) + D.GetDecimal(this._flexH[this._flexH.Row, "VAT"]));

                this._flexL["UM"] = this.원화계산(D.GetDecimal(this._flexL["UM_EX"]) * D.GetDecimal(this._flexL["RT_EXCH"]));
            }
            catch (coDbException ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        private void 선택금액계산()
        {
            try
            {
                this.cur외화합계.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX)", "S = 'Y'"));
                this.cur원화합계.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_IV)", "S = 'Y'"));
                this.cur부가세합계.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(VAT_IV)", "S = 'Y'"));
                this.cur총합계.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_TOTAL)", "S = 'Y'"));
                this.cur총수량.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(QT_IV)", "S = 'Y'"));
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
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
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA;
using Duzon.ERPU.SA.Common;
using DzHelpFormLib;
using pur;
using sale;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_GI_WINTEC : PageBase
    {
        private P_CZ_SA_GI_WINTEC_BIZ _biz = new P_CZ_SA_GI_WINTEC_BIZ();
        private string MNG_LOT = Global.MainFrame.LoginInfo.MngLot;
        private string MNG_SERIAL = string.Empty;
        private string CD_DEPT = string.Empty;
        private string two_Unit_Mng = string.Empty;
        private bool qtso_AddAllowYN = false;
        private string Am_Recalc = "000";
        private bool bStorageLocation = true;
        private bool bStorageLocation2 = true;
        private FreeBinding _header = new FreeBinding();
        private 수주관리.Config 수주Config = new 수주관리.Config();
        private bool is중국고객부가세포함단가사용여부 = false;
        private bool chkClick = false;
        private string str금액계산사용여부 = ComFunc.전용코드("출하등록-금액 계산 사용여부");

        public P_CZ_SA_GI_WINTEC()
        {
            try
            {
                InitializeComponent();

                this.bStorageLocation = BASIC.GetMAEXC("W/H 정보사용") == "100";
                this.bStorageLocation2 = !(BASIC.GetMAEXC("S/L입출고도움창_사용여부") == "N");
                this.is중국고객부가세포함단가사용여부 = this.중국고객부가세포함단가사용여부();
                if (MA.ServerKey(false, new string[] { "COSMOCO" }) || Global.MainFrame.ServerKeyCommon.Contains("KYOTECH"))
                    this.bStorageLocation2 = false;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
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

            #region _flexH
            this._flexH.BeginSetting(1, 1, false);
            this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_GIR", "의뢰번호", 100);
            this._flexH.SetCol("NO_SO", "수주번호", 100);
            if (MA.ServerKey(false, new string[] { "GIT" }))
                this._flexH.SetCol("GW_ST_STAT", "승인처리", 80);
            this._flexH.SetCol("CD_PLANT", "공장", 80);
            this._flexH.SetCol("NM_PLANT", "공장명", 120);
            this._flexH.SetCol("CD_PARTNER", "거래처", 80);
            this._flexH.SetCol("LN_PARTNER", "거래처명", 120);
            this._flexH.SetCol("DT_GIR", "의뢰일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NO_GI_EMP", "출하의뢰자", 80);
            this._flexH.SetCol("NM_KOR", "출하의뢰자명", 100);
            this._flexH.SetCol("DC_RMK", "비고", 200);
            this._flexH.SetCol("DC_RMK1", "비고1", 200);
            this._flexH.SetCol("DC_RMK2", "비고2", 200);
            this._flexH.SetCol("SN_PARTNER", "거래처명(약칭)", false);
            this._flexH.ExtendLastCol = true;
            this._flexH.EnabledHeaderCheck = true;
            this._flexH.IsUseShiftMultiCheck = false;
            this._flexH.SettingVersion = "1.0.0.1";
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region _flexL
            this._flexL.BeginSetting(1, 1, false);
            this._flexL.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
            if (MA.ServerKey(false, new string[] { "EMDM", "EMDM2" }))
                this._flexL.SetCol("DC2", "제번번호", 100, false);
            this._flexL.SetCol("CD_ITEM", "품목코드", 80);
            this._flexL.SetCol("NM_ITEM", "품목명", 120);
            this._flexL.SetCol("EN_ITEM", "품목명(영)", false);
            this._flexL.SetCol("STND_ITEM", "규격", 80);
            this._flexL.SetCol("STND_DETAIL_ITEM", "세부규격", 100);
            this._flexL.SetCol("NO_LOT", "LOT여부", 80);
            this._flexL.SetCol("DT_DUEDATE", "납기일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("FG_TRANSPORT", "운송방법", 120);
            this._flexL.SetCol("FG_TRANS", "거래구분", 80);
            if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("CELKR"))
                this._flexL.SetCol("TXT_USERDEF1", "송장번호", 100, true);
            if (this._biz.Get출하등록_검사 == "200")
                this._flexL.SetCol("YN_INSPECT", "검사여부", 50, true, CheckTypeEnum.Y_N);
            this._flexL.SetCol("CD_SL", "창고코드", 80, true);
            this._flexL.SetCol("NM_SL", "창고명", 120);
            this._flexL.SetCol("QT_INV", "현재고", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NM_QTIOTP", "출하형태", 100);
            this._flexL.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_GIR_IM", "관리수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            if (MA.ServerKey(false, new string[] { "WONIL" }))
                this._flexL.SetCol("QT_UNIT_MM", "출하수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            else
                this._flexL.SetCol("QT_UNIT_MM", "출하수량", 90, true, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_IO", "출하관리수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("NO_PROJECT", "프로젝트코드", 80, false);
            this._flexL.SetCol("NM_PROJECT", "프로젝트명", 80, false);
            this._flexL.SetCol("UNIT", "단위", 80);
            this._flexL.SetCol("CD_UNIT_MM", "관리단위", 80);
            this._flexL.SetCol("CD_ITEM_PARTNER", "거래처품번", 150, false);
            this._flexL.SetCol("NM_ITEM_PARTNER", "거래처품명", 150, false);
            this._flexL.SetCol("NO_GIR", "의뢰번호", 100);
            if (MA.ServerKey(false, new string[] { "DNCOMPANY" }))
                this._flexL.SetCol("DC_RMK", "택배번호", 150, true);
            else
                this._flexL.SetCol("DC_RMK", "비고", 150, true);
            this._flexL.SetCol("NM_SALEGRP", "영업그룹", 80, false);
            this._flexL.SetCol("GI_PARTNER", "납품처코드", 80, false);
            this._flexL.SetCol("LN_PARTNER", "납품처명", 80, false);
            this._flexL.SetCol("FG_SERNO", "LOT/SN", 80);
            this._flexL.SetCol("NM_ITEMGRP", "품목군", 100);
            if (ComFunc.전용코드("출하등록-단가금액통제") == "N")
            {
                this._flexL.SetCol("UM_EX_PSO", "단가", 90, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
                this._flexL.SetCol("AM_EX", "외화금액", 90, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
                this._flexL.SetCol("AM", "원화금액", 90, false, typeof(decimal), FormatTpType.MONEY);
            }
            if (this.bStorageLocation)
            {
                this._flexL.SetCol("CD_WH", "W/H코드", 80, false);
                this._flexL.SetCol("NM_WH", "W/H코드", 100, false);
            }
            this._flexL.SetCol("CD_ZONE", "LOCATION", 100, false);
            this._flexL.SetCol("NM_GRP_MFG", "제품군", 100, false);
            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                this._flexL.SetCol("CD_PARTNER_GRP", "거래처그룹", 150, false);
                this._flexL.SetCol("CD_PARTNER_GRP_2", "거래처그룹2", 150, false);
                this._flexL.SetCol("CD_USERDEF1", "사용자정의", 150, false);
                this._flexL.SetCol("CD_USERDEF2", "사용자정의2", 150, false);
            }
            if (Config.MA_ENV.YN_UNIT == "Y")
            {
                this._flexL.SetCol("SEQ_PROJECT", "UNIT 항번", 120);
                this._flexL.SetCol("CD_UNIT", "UNIT 코드", 120);
                this._flexL.SetCol("NM_UNIT", "UNIT 명", 120);
                this._flexL.SetCol("STND_UNIT", "UNIT 규격", 100);
            }
            this._flexL.SetCol("NO_PO_PARTNER", "거래처P/O번호", 120);
            this._flexL.SetCol("NO_POLINE_PARTNER", "거래처P/O항번", 120);
            this._flexL.SetCol("NO_ISURCV", "의뢰번호", false);
            this._flexL.SetCol("NO_ISURCVLINE", "의뢰항번", false);
            this._flexL.SetCol("MAT_ITEM", "재질", false);
            this._flexL.SetCol("CLS_ITEM", "품목계정", false);
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CNP")
            {
                this._flexL.SetCol("QTIO_CD_USERDEF1", "송장번호", 120, true);
                this._flexL.SetCol("QTIO_CD_USERDEF2", "택배사", 120, true);
            }
            if (this.수주Config.부가세포함단가사용())
            {
                this._flexL.SetCol("TP_UM_TAX", "부가세여부", 90, false);
                this._flexL.SetCol("UMVAT_GI", "부가세포함단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            }
            if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("DZSQL") || Global.MainFrame.ServerKeyCommon.ToUpper().Contains("TAESIN"))
                this._flexL.SetCol("SO_COST", "수주단가", 100, false, typeof(decimal), FormatTpType.UNIT_COST);
            this._flexL.SetCol("DC_RMK1", "비고2", 150, true);
            this._flexL.SetCol("DC_RMK2", "비고3", 150, true);
            if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("COSMOCO"))
            {
                this._flexL.SetCol("DC2", "이름", 100, false);
                this._flexL.SetCol("TXT_USERDEF1", "전화번호", 100, false);
                this._flexL.SetCol("TXT_USERDEF2", "주소", 100, false);
            }
            if (MA.ServerKey(false, new string[] { "NOVAREX" }))
            {
                this._flexL.SetCol("QTIO_CD_USERDEF1", "차량유형", 120, true);
                this._flexL.SetCol("QTIO_CD_USERDEF2", "배송구분", 120, true);
                this._flexL.SetCol("QTIO_CD_USERDEF3", "배송업체", 120, true);
            }
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "KOINO")
                this._flexL.SetCol("ITEM_NUM_USERDEF1", "대박스(수량)", 100, false);
            if (Global.MainFrame.ServerKeyCommon == "SPLT")
            {
                this._flexL.SetCol("WEIGHT", "중량", 100, false);
                this._flexL.SetCol("TOTAL_WEIGHT_GIR", "총중량(의뢰)", 100, false, typeof(decimal));
                this._flexL.SetCol("TOTAL_WEIGHT_QTIO", "총중량(출하)", 100, false, typeof(decimal));
            }
            if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("HMRFOOD"))
            {
                this._flexL.SetCol("QT_WEIGHT_GIR", "포장단위수량(의뢰)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
                this._flexL.SetCol("QT_WEIGHT_QTIO", "포장단위수량(출하)", 100, false, typeof(decimal));
                this._flexL.SetCol("UNIT_WEIGHT", "포장단위", 100, false);
                (this._flexL.Cols["QT_WEIGHT_QTIO"]).Format = this._flexL.Cols["QT_WEIGHT_QTIO"].EditMask = "#,###,###,###,##0";
            }
            if (MA.ServerKey(false, new string[] { "SFNB" }))
            {
                this._flexL.SetCol("GIRL_CD_USERDEF2", "납품의뢰상태", 100, false);
                this._flexL.SetDataMap("GIRL_CD_USERDEF2", MA.GetCode("SA_B000020", true), "CODE", "NAME");
            }
            if (MA.ServerKey(false, new string[] { "SUNGBO" }))
            {
                this._flexL.SetCol("GL_CD_USERDEF1", "배송구분", 100, false);
                this._flexL.SetCol("GL_NM_USERDEF1", "배송구분명", 100, false);
                this._flexL.SetCol("GL_TXT_USERDEF1", "차량번호", 100, false);
            }
            if (MA.ServerKey(false, new string[] { "DAOU" }))
            {
                this._flexL.SetCol("TXT_USERDEF6", "ENDUSER", 100, false);
                this._flexL.SetCol("NM_ENDUSER", "ENDUSER명", 100, false);
            }
            if (MA.ServerKey(false, new string[] { "CYMT" }))
            {
                this._flexL.SetCol("NM_USERDEF1", "LOTNO(전)", 100, true);
                this._flexL.SetCol("NM_USERDEF2", "LOTNO(현)", 100, true);
            }
            if (MA.ServerKey(false, new string[] { "ANYONE" }))
            {
                this._flexL.SetCol("GL_CD_USERDEF1", "배송방법", 100, false);
                this._flexL.SetCol("GL_TXT_USERDEF1", "화물지점", 100, false);
                this._flexL.SetCol("DC_ADS1_H", "주소", 100, false);
                this._flexL.SetCol("NO_TEL1", "연락처", 100, false);
                this._flexL.SetCol("QT_ROLL", "의뢰수량(ROLL)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            }
            if (MA.ServerKey(true, new string[] { "DHF2" }))
            {
                this._flexL.SetCol("WEIGHT", "중량", 100, false);
                this._flexL.SetCol("UNIT_WEIGHT", "중량단위", 100, false);
                this._flexL.SetCol("GI_WEIGHT", "출하중량", 100, false);
                (this._flexL.Cols["WEIGHT"]).Format = "#,###,###.####";
                (this._flexL.Cols["GI_WEIGHT"]).Format = "#,###,###.####";
            }
            if (MA.ServerKey(false, new string[] { "NOFMCT" }))
                this._flexL.SetCol("TOTAL_WEIGHT_GIR", "출하중량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            if (MA.ServerKey(false, new string[] { "SHTP" }))
                this._flexL.SetCol("SOL_NUM_USERDEF2", "주문매수", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            if (this.str금액계산사용여부 == "001")
            {
                this._flexL.SetCol("CD_EXCH", "환종", 80, false);
                this._flexL.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
                this._flexL.SetCol("VAT", "부가세", 80, false, typeof(decimal), FormatTpType.MONEY);
                this._flexL.SetCol("AMT", "원화총금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            }
            this._flexL.VerifyPrimaryKey = new string[] { "NO_ISURCV", "NO_ISURCVLINE" };
            Config.UserColumnSetting.InitGrid_UserMenu(this._flexL, this.PageID, true);
            this._flexL.EnabledHeaderCheck = true;
            this._flexL.SettingVersion = "1.1.5.3";
            Config.UserColumnSetting.InitGrid_UserMenu(this._flexL, this.PageID, true);
            this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            if (this.bStorageLocation)
                this._flexL.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL",
                                                                                                            "NM_SL",
                                                                                                            "CD_WH",
                                                                                                            "NM_WH" },
                                                                                             new string[] { "CD_SL",
                                                                                                            "NM_SL",
                                                                                                            "CD_WH",
                                                                                                            "NM_WH" },
                                                                                             new string[] { "CD_SL",
                                                                                                            "NM_SL",
                                                                                                            "CD_WH",
                                                                                                            "NM_WH",
                                                                                                            "QT_INV" });
            else
                this._flexL.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL",
                                                                                                            "NM_SL" },
                                                                                             new string[] { "CD_SL",
                                                                                                            "NM_SL" },
                                                                                             new string[] { "CD_SL",
                                                                                                            "NM_SL",
                                                                                                            "QT_INV" });
            this._flexL.AddMyMenu = true;
            this._flexL.AddMenuSeperator();
            this._flexL.AddMenuItem(this._flexL.AddPopup(this.DD("관련 현황")), this.DD("현재고조회"), new EventHandler(this.Menu_Click));
            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
                this.oneGrid2.Visible = true;
            if (MA.ServerKey(false, new string[] { "DSTECH" }))
                this.lbl납품의뢰번호.Visible = this.txt고객납품의뢰번호.Visible = true;
            if (MA.ServerKey(true, new string[] { "ANYONE" }))
            {
                this.lbl납품의뢰번호.Visible = this.txt고객납품의뢰번호.Visible = true;
                this.lbl납품의뢰번호.Text = "물류비";
            }
            if (Config.MA_ENV.YN_UNIT == "Y")
                this._flexL.SetExceptSumCol(new string[] { "UM_EX_PSO",
                                                           "SEQ_PROJECT",
                                                           "UMVAT_GI" });
            else
                this._flexL.SetExceptSumCol(new string[] { "UM_EX_PSO",
                                                           "UMVAT_GI" });
            #endregion
        }

        protected override void InitPaint()
        {
            SetControl setControl = new SetControl();
            this.oneGrid1.UseCustomLayout = this.oneGrid2.UseCustomLayout = this.oneGrid3.UseCustomLayout = this.oneGrid4.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = this.bpPanelControl16.IsNecessaryCondition = this.bpPanelControl23.IsNecessaryCondition = this.bpPanelControl24.IsNecessaryCondition = this.bpPanelControl25.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            this.oneGrid2.InitCustomLayout();
            this.oneGrid3.InitCustomLayout();
            this.oneGrid4.InitCustomLayout();
            if (Global.MainFrame.ServerKeyCommon == "CHOSUNHOTELBA")
            {
                DataSet comboData = this.GetComboData(new string[] { "S;MA_B000065", "S;MA_B000067", "S;MA_B000102", "S;MA_B000103" });
                this.cbo거래처그룹.DataSource = comboData.Tables[0];
                this.cbo거래처그룹.DisplayMember = "NAME";
                this.cbo거래처그룹.ValueMember = "CODE";
                this.cbo거래처그룹2.DataSource = comboData.Tables[1];
                this.cbo거래처그룹2.DisplayMember = "NAME";
                this.cbo거래처그룹2.ValueMember = "CODE";
                this.cbo사용자정의.DataSource = comboData.Tables[2];
                this.cbo사용자정의.DisplayMember = "NAME";
                this.cbo사용자정의.ValueMember = "CODE";
                this.cbo사용자정의2.DataSource = comboData.Tables[3];
                this.cbo사용자정의2.DisplayMember = "NAME";
                this.cbo사용자정의2.ValueMember = "CODE";
                if (this._flexL.Cols.Contains("CD_PARTNER_GRP"))
                    this._flexL.SetDataMap("CD_PARTNER_GRP", comboData.Tables[0].Copy(), "CODE", "NAME");
                if (this._flexL.Cols.Contains("CD_PARTNER_GRP_2"))
                    this._flexL.SetDataMap("CD_PARTNER_GRP_2", comboData.Tables[1].Copy(), "CODE", "NAME");
                if (this._flexL.Cols.Contains("CD_USERDEF1"))
                    this._flexL.SetDataMap("CD_USERDEF1", comboData.Tables[2].Copy(), "CODE", "NAME");
                if (this._flexL.Cols.Contains("CD_USERDEF2"))
                    this._flexL.SetDataMap("CD_USERDEF2", comboData.Tables[3].Copy(), "CODE", "NAME");
            }
            if (MA.ServerKey(false, new string[] { "NOVAREX" }))
            {
                this._flexL.SetDataMap("QTIO_CD_USERDEF1", MA.GetCode("SA_X001901", true), "CODE", "NAME");
                this._flexL.SetDataMap("QTIO_CD_USERDEF2", MA.GetCode("SA_X001902", true), "CODE", "NAME");
                this._flexL.SetDataMap("QTIO_CD_USERDEF3", MA.GetCode("SA_X001903", true), "CODE", "NAME");
            }
            if (MA.ServerKey(false, new string[] { "THV" }))
            {
                this.ctx영업그룹.Visible = false;
                this.bpc영업그룹.Visible = true;
            }
            if (MA.ServerKey(false, new string[] { "SUNGBO" }))
            {
                this.cbo배송구분.Visible = this.txt차량번호.Visible = true;
                this.cbo배송구분.DataSource = MA.GetCode("SA_X001902", true);
                this.cbo배송구분.DisplayMember = "NAME";
                this.cbo배송구분.ValueMember = "CODE";
            }
            else
            {
                this.oneGridItem12.Visible = false;
                this.oneGrid1.Size = new Size(1054, 132);
            }
            if (MA.ServerKey(false, new string[] { "ANYONE" }))
                this._flexL.SetDataMap("GL_CD_USERDEF1", MA.GetCode("SA_X000101", true), "CODE", "NAME");
            this.통제조회();
            this.그리드통제적용();
            DataSet comboData1 = this.GetComboData(new string[] { "N;MA_PLANT",
                                                                  "S;TR_IM00008",
                                                                  "S;PU_C000016" });
            this.cbo공장.DataSource = comboData1.Tables[0];
            this.cbo공장.DisplayMember = "NAME";
            this.cbo공장.ValueMember = "CODE";
            this.cbo공장.SelectedValue = Global.MainFrame.LoginInfo.CdPlant;
            this.cbo운송방법.DataSource = comboData1.Tables[1];
            this.cbo운송방법.DisplayMember = "NAME";
            this.cbo운송방법.ValueMember = "CODE";
            this.cbo운송방법.SelectedIndex = 0;
            DataTable dataTable = comboData1.Tables[0].Clone();
            DataRow row1 = dataTable.NewRow();
            row1["CODE"] = "GI";
            row1["NAME"] = this.DD("의뢰일자");
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["CODE"] = "DU";
            row2["NAME"] = this.DD("납기일자");
            dataTable.Rows.Add(row2);
            DataRow row3 = dataTable.NewRow();
            row3["CODE"] = "RQ";
            row3["NAME"] = this.DD("출하예정일");
            dataTable.Rows.Add(row3);
            this.cbo일자구분.DataSource = dataTable;
            this.cbo일자구분.DisplayMember = "NAME";
            this.cbo일자구분.ValueMember = "CODE";
            if (MA.ServerKey(false, new string[] { "NOVAREX" }))
                this.cbo일자구분.SelectedValue = "RQ";
            this._flexL.SetDataMap("FG_TRANS", comboData1.Tables[2], "CODE", "NAME");
            DataTable code = MA.GetCode("MA_B000015");
            DataRow[] dataRowArray = code.Select("CODE = '001'", "", DataViewRowState.CurrentRows);
            if (dataRowArray.Length == 1)
                dataRowArray[0]["NAME"] = "";
            this._flexL.SetDataMap("FG_SERNO", code, "CODE", "NAME");
            this._flexL.SetDataMap("CLS_ITEM", MA.GetCode("MA_B000010"), "CODE", "NAME");
            this.dtp일자.StartDateToString = Global.MainFrame.GetStringFirstDayInMonth;
            this.ctx거래처.CodeValue = string.Empty;
            this.ctx거래처.CodeName = string.Empty;
            this.ctx납품처.CodeValue = string.Empty;
            this.ctx납품처.CodeName = string.Empty;
            this.ctx출하형태.CodeValue = string.Empty;
            this.ctx출하형태.CodeName = string.Empty;
            this.rdo_Not.Checked = true;
            this.rdo_Yes.Checked = false;
            this.ctx프로젝트.CodeValue = string.Empty;
            this.ctx프로젝트.CodeName = string.Empty;
            this.CD_DEPT = Global.MainFrame.LoginInfo.DeptCode;
            this.ctx창고.CodeValue = string.Empty;
            this.ctx창고.CodeName = string.Empty;
            this.txt_DcRmk.Text = string.Empty;
            this.ctx영업그룹.CodeValue = string.Empty;
            this.ctx영업그룹.CodeName = string.Empty;
            if (Settings.Default.auto_No == "GIR")
            {
                this.rdo의뢰번호별.Checked = true;
                this.rdo거래처별.Checked = false;
            }
            else if (Settings.Default.auto_No == "PARTNER")
            {
                this.rdo의뢰번호별.Checked = false;
                this.rdo거래처별.Checked = true;
            }
            this.chk헤더비고적용여부.Checked = Settings.Default.의뢰비고적용여부;
            this.txt납품처.Text = string.Empty;
            this.txt수주번호.Text = string.Empty;
            this.txtLC번호.Text = string.Empty;
            this.btn_apply.Enabled = false;
            this.btn양품적용.Enabled = false;
            this._header.SetBinding(new DataTable()
            {
                Columns = { { "NO_IO", typeof (string) },
                            { "DT_IO", typeof (string) },
                            { "NO_EMP", typeof (string) },
                            { "NM_KOR", typeof (string) },
                            { "CD_SL", typeof (string) },
                            { "NM_SL", typeof (string) },
                            { "DC_RMK", typeof (string) } }
            }, this.oneGrid3);
            this._header.ClearAndNewRow();
            this.Auth();
            this.dt출하일자.Text = Global.MainFrame.GetStringToday;
            this.ctx담당자.CodeValue = Global.MainFrame.LoginInfo.EmployeeNo;
            this.ctx담당자.CodeName = Global.MainFrame.LoginInfo.EmployeeName;
            if (MA.ServerKey(false, new string[] { "COSMOCO" }))
                this.btn_apply.Visible = true;
            else
                this.btn_apply.Visible = !ConfigSA.SA_EXC.WH정보사용;
            if (Global.MainFrame.ServerKeyCommon.ToUpper() == "CNP")
                this._flexL.SetDataMap("QTIO_CD_USERDEF2", MA.GetCode("CZ_CNP_007"), "CODE", "NAME");
            if (Global.MainFrame.CurrentPageID == "P_SA_Z_WONIL_GI")
            {
                this.chk헤더비고적용여부.Checked = true;
                this.chk헤더비고적용여부.Enabled = false;
                this.ctx출하의뢰자.CodeValue = null;
                this.ctx출하의뢰자.CodeName = null;
            }
            if (!(this.str금액계산사용여부 == "001"))
                return;

            this.bpPanelControl33.Visible = this.bpPanelControl34.Visible = true;
            setControl.SetCombobox(this.cbo환종, MA.GetCode("MA_B000005"));
            this._flexL.SetDataMap("CD_EXCH", MA.GetCode("MA_B000005"), "CODE", "NAME");
        }
        private void 통제조회()
        {
            this.MNG_SERIAL = this._biz.search_SERIAL(new object[] { Global.MainFrame.LoginInfo.CompanyCode });
            DataTable dataTable = this._biz.search_EnvMng();
            if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["FG_TP"] != DBNull.Value && dataTable.Rows[0]["FG_TP"].ToString().Trim() != string.Empty)
                this.two_Unit_Mng = dataTable.Select("FG_TP = '001'")[0]["CD_TP"].ToString();
            this.qtso_AddAllowYN = Sa_Global.Qtso_AddAllowYN;
            this.Am_Recalc = Sa_Global.AM_ReCalc;
        }
        private void 그리드통제적용()
        {
            if (this.two_Unit_Mng != "N")
                (this._flexL.Cols["QT_IO"]).AllowEditing = true;
            if (!App.SystemEnv.PROJECT사용)
                return;
            this._flexL.VerifyNotNull = new string[] { "NO_PROJECT" };
        }
        private void Auth()
        {
            this.ToolBarDeleteButtonEnabled = false;
            this.ToolBarPrintButtonEnabled = false;
        }
        private bool FieldCheck(string Flag)
        {
            Hashtable hashtable = new Hashtable();
            if (Flag == "SEARCH")
            {
                LabelExt labelExt = new LabelExt();
                hashtable.Add(this.cbo공장, this.lbl공장);
            }
            else if (Flag == "SAVE")
            {
                hashtable.Add(this.dt출하일자, this.lbl출하일자);
                hashtable.Add(this.ctx담당자, this.lbl담당자);
            }
            return ComFunc.NullCheck(hashtable);
        }
        private void InitEvent()
        {

            this.cbo공장.SelectionChangeCommitted += new EventHandler(this.cbo공장_SelectionChangeCommitted);

            this.cbo공장.KeyDown += new KeyEventHandler(this.CommonComboBox_KeyEvent);

            this.ctx출하형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc의뢰창고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx제품군.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx프로젝트.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc수주형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx품목.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.bpc생산파트.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
            this.ctx창고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

            this.ctx담당자.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
            this.ctx창고.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);

            this.cbo운송방법.KeyDown += new KeyEventHandler(this.CommonComboBox_KeyEvent);
            this.rdo_Yes.CheckedChanged += new EventHandler(this.rdo_Yes_CheckedChanged);
            this.ctx창고.Leave += new EventHandler(this.ctx창고_Leave);
            this.ctx창고.Validated += new EventHandler(this.ctx창고_Validated);
            this.btn_apply.Click += new EventHandler(this.btn_apply_Click);
            this.btn환율변경.Click += new EventHandler(this.btn환율변경_Click);
            this.btn환종변경.Click += new EventHandler(this.btn환종변경_Click);
            this.cbo환종.Click += new EventHandler(this.btn환종변경_Click);
            this.btn양품적용.Click += new EventHandler(this.btn_apply_good_Click);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
            this._flexH.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flexL.AfterRowChange += new RangeEventHandler(this._flexL_AfterRowChange);
            this._flexL.BeforeCodeHelp += new BeforeCodeHelpEventHandler(this._flex_BeforeCodeHelp);
            this._flexL.CheckHeaderClick += new EventHandler(this._flex_CheckHeaderClick);
            this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flexL.DoubleClick += new EventHandler(this._flex_DoubleClick);
            this._flexL.AfterCodeHelp += new AfterCodeHelpEventHandler(this._flexL_AfterCodeHelp);
            this._flexL.StartEdit += new RowColEventHandler(this._flex_StartEdit);

            this.cbo환종.SelectionChangeCommitted += new EventHandler(this.cbo환종_SelectionChangeCommitted);

            this.dtp일자.TextChanged += new EventHandler(this.dtp일자_TextChanged);
        }
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.FieldCheck("SEARCH") || !this.Chk일자)
                    return;
                string empty = string.Empty;
                string str = !MA.ServerKey(false, new string[] { "THV" }) ? this.ctx영업그룹.CodeValue : this.bpc영업그룹.QueryWhereIn_Pipe;
                DataTable dataTable = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                      this.dtp일자.StartDateToString,
                                                                      this.dtp일자.EndDateToString,
                                                                      this.cbo공장.SelectedValue == null ? string.Empty : this.cbo공장.SelectedValue.ToString(),
                                                                      this.ctx거래처.CodeValue,
                                                                      this.ctx납품처.CodeValue,
                                                                      this.ctx출하형태.CodeValue,
                                                                      "N",
                                                                      D.GetString(this.cbo운송방법.SelectedValue),
                                                                      D.GetString(this.ctx출하의뢰자.CodeValue),
                                                                      D.GetString(this.cbo일자구분.SelectedValue),
                                                                      D.GetString(str),
                                                                      D.GetString(this.bpc수주형태.QueryWhereIn_Pipe),
                                                                      D.GetString(this.ctx프로젝트.CodeValue),
                                                                      D.GetString(this.ctx제품군.CodeValue),
                                                                      D.GetString(this.bpc의뢰창고.QueryWhereIn_Pipe),
                                                                      D.GetString(this.cbo거래처그룹.SelectedValue),
                                                                      D.GetString(this.cbo거래처그룹2.SelectedValue),
                                                                      D.GetString(this.cbo사용자정의.SelectedValue),
                                                                      D.GetString(this.cbo사용자정의2.SelectedValue),
                                                                      MA.Login.사원번호,
                                                                      this.bpc생산파트.QueryWhereIn_Pipe,
                                                                      this.ctx품목.CodeValue,
                                                                      Global.SystemLanguage.MultiLanguageLpoint,
                                                                      Global.MainFrame.ServerKey != "SUNGBO" ? string.Empty : D.GetString(this.cbo배송구분.SelectedValue),
                                                                      Global.MainFrame.ServerKey != "SUNGBO" ? string.Empty : this.txt차량번호.Text,
                                                                      !(Global.MainFrame.ServerKey != "DSTECH") || !(Global.MainFrame.ServerKey != "ANYONE") ? this.txt고객납품의뢰번호.Text : string.Empty,
                                                                      D.GetString(this.txt수주번호2.Text)}, D.GetString(this.cbo일자구분.SelectedValue));
                dataTable.DefaultView.Sort = "NO_GIR ASC";
                this._flexH.Binding = dataTable.DefaultView.ToTable();
                if (!this._flexH.HasNormalRow)
                {
                    this.btn_apply.Enabled = false;
                    this.btn양품적용.Enabled = false;
                    this.Auth();
                    this.ShowMessage(PageResultMode.SearchNoData);
                }
              this.btn양품적용.Enabled = true;
                this.Auth();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.InitPaint();
                this.dtp일자.Focus();
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
                if (!this.chkClick)
                {
                    this.chkClick = true;
                    this._flexL.Focus();
                    if (!this.FieldCheck("SAVE") || !this.Verify())
                        return;
                    DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray == null || dataRowArray.Length == 0)
                        return;
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (dataRow.RowState != DataRowState.Deleted)
                        {
                            if ((!this.bStorageLocation || !this.bStorageLocation2) && D.GetString(dataRow["CD_SL"]) == string.Empty)
                            {
                                this.ShowMessage(" 창고는 필수입력항목입니다. \n\n 창고를 확인하세요.");
                                return;
                            }
                            if (this._flexL.CDecimal(dataRow["QT_UNIT_MM"]) <= 0M)
                            {
                                this.ShowMessage(공통메세지._은_보다커야합니다, new string[] { this.DD("출하수량"), "0" });
                                this._flexL.Select(this._flexL.Rows.Fixed, "QT_UNIT_MM");
                                this._flexL.Focus();
                                return;
                            }
                            if (MA.ServerKey(false, new string[] { "CELKR" }) && D.GetString(dataRow["TXT_USERDEF1"]) == "")
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("송장번호") });
                                return;
                            }
                        }
                    }
                    if (this.SaveData())
                    {
                        this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다, new string[0]);
                        base.OnToolBarSearchButtonClicked(sender, e);
                    }
                    this.chkClick = false;
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                this.chkClick = false;
            }
            finally
            {
                this.chkClick = false;
            }
        }
        protected override bool SaveData()
        {
            try
            {
                DataTable dt_H1 = this._flexL.DataTable.Clone();
                DataRow[] dataRowArray1 = this._flexL.DataTable.Select("S = 'Y' AND ISNULL(QT_IO, 0) <> ISNULL(QT_UNIT_MM, 0) * ISNULL(UNIT_SO_FACT, 1)", "", DataViewRowState.CurrentRows);
                if (dataRowArray1 != null && dataRowArray1.Length != 0)
                {
                    if (this.two_Unit_Mng == "N")
                    {
                        this.ShowMessage("수배수량과 관리수량이 일치하지 않아서 저장할 수 없습니다.");
                    }
                    foreach (DataRow row in dataRowArray1)
                        dt_H1.ImportRow(row);
                    P_SA_TWO_UNIT_EX_SUB pSaTwoUnitExSub = new P_SA_TWO_UNIT_EX_SUB(dt_H1, this.two_Unit_Mng);
                    if (pSaTwoUnitExSub.ShowDialog() != DialogResult.OK)
                        return false;
                    dt_H1.Rows.Clear();
                    if (pSaTwoUnitExSub.returnDt != null && pSaTwoUnitExSub.returnDt.Rows.Count != 0)
                    {
                        foreach (DataRow row in pSaTwoUnitExSub.returnDt.Rows)
                            dt_H1.ImportRow(row);
                    }
                }
                if (dt_H1 != null && dt_H1.Rows.Count != 0)
                {
                    foreach (DataRow row1 in dt_H1.Rows)
                    {
                        foreach (DataRow row2 in this._flexL.DataTable.Rows)
                        {
                            if (row1["NO_ISURCV"].ToString() == row2["NO_ISURCV"].ToString() && D.GetDecimal(row1["NO_ISURCVLINE"].ToString()) == D.GetDecimal(row2["NO_ISURCVLINE"].ToString()))
                                row2["S"] = "N";
                            DataRow[] dataRowArray2 = this._flexL.DataTable.Select("S = 'Y' AND NO_ISURCV = '" + row1["NO_ISURCV"].ToString() + "'");
                            if (dataRowArray2 == null || dataRowArray2.Length == 0)
                            {
                                DataRow[] dataRowArray3 = this._flexH.DataTable.Select("S = 'Y' AND NO_GIR = '" + row1["NO_ISURCV"].ToString() + "'");
                                if (row1["NO_ISURCV"].ToString() == dataRowArray3[0]["NO_GIR"].ToString())
                                    dataRowArray3[0]["S"] = "N";
                            }
                        }
                    }
                }
                DataRow[] dataRowArray4 = this._flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                DataRow[] dataRowArray5 = this._flexL.DataTable.Select("S = 'Y' AND NO_LOT = 'YES'", "", DataViewRowState.CurrentRows);
                DataRow[] dataRowArray6 = this._flexL.DataTable.Select("S = 'Y' AND NO_SERL = 'YES'", "", DataViewRowState.CurrentRows);
                if (dataRowArray4 == null || dataRowArray4.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                    return false;
                }
                if (this.MNG_LOT == "Y" && dataRowArray4.Length > 1 && (dataRowArray5 == null || dataRowArray5.Length > 0))
                {
                    this.ShowMessage(" 여러개의 의뢰번호에 해당하는 LOT품목이 여러건이 존재하여 \n\n 일괄 작업을 수행할 수 없습니다. \n\n 한건씩 처리하시기 바랍니다.");
                    return false;
                }
                if (this.MNG_SERIAL == "Y" && dataRowArray4.Length > 1 && (dataRowArray6 == null || dataRowArray6.Length > 0))
                {
                    this.ShowMessage(" 여러개의 의뢰번호에 해당하는 SERIAL품목이 여러건이 존재하여 \n\n 일괄 작업을 수행할 수 없습니다. \n\n 한건씩 처리하시기 바랍니다.");
                    return false;
                }
                DataTable dataTable1 = new DataTable();
                dataTable1.Columns.Add("CD_PARTNER", typeof(string));
                dataTable1.Columns.Add("CD_PARTNER_NAME", typeof(string));
                dataTable1.Columns.Add("AM_SUM", typeof(decimal));
                foreach (DataRow dataRow in this._flexL.DataTable.Select("S = 'Y' AND ST_STAT <> '1'"))
                {
                    DataRow[] dataRowArray8 = dataTable1.Select("CD_PARTNER = '" + dataRow["CD_PARTNER"].ToString() + "'");
                    if (dataRowArray8.Length == 0)
                    {
                        DataRow row = dataTable1.NewRow();
                        row["CD_PARTNER"] = dataRow["CD_PARTNER"].ToString();
                        row["CD_PARTNER_NAME"] = dataRow["CD_PARTNER_NAME"].ToString();
                        row["AM_SUM"] = this._flexL.CDecimal(dataRow["AM"]) + this._flexL.CDecimal(dataRow["VAT"]);
                        dataTable1.Rows.Add(row);
                    }
                    else
                        dataRowArray8[0]["AM_SUM"] = this._flexL.CDecimal(dataRowArray8[0]["AM_SUM"]) + this._flexL.CDecimal(dataRow["AM"]) + this._flexL.CDecimal(dataRow["VAT"]);
                }
                DataTable dt_H2 = new DataTable();
                dt_H2.Columns.Add("S", typeof(string));
                dt_H2.Columns.Add("CD_PARTNER", typeof(string));
                dt_H2.Columns.Add("CD_PARTNER_NAME", typeof(string));
                dt_H2.Columns.Add("CREDIT_TOT", typeof(decimal));
                dt_H2.Columns.Add("MISU_REMAIN", typeof(decimal));
                dt_H2.Columns.Add("CREDIT_RAMAIN", typeof(decimal));
                dt_H2.Columns.Add("AM_SUM", typeof(decimal));
                dt_H2.Columns.Add("EX_CONTENT", typeof(string));
                DataTable dt_L1 = new DataTable();
                dt_L1.Columns.Add("S", typeof(string));
                dt_L1.Columns.Add("CD_PARTNER", typeof(string));
                dt_L1.Columns.Add("CD_PARTNER_NAME", typeof(string));
                dt_L1.Columns.Add("CREDIT_TOT", typeof(decimal));
                dt_L1.Columns.Add("MISU_REMAIN", typeof(decimal));
                dt_L1.Columns.Add("CREDIT_RAMAIN", typeof(decimal));
                dt_L1.Columns.Add("AM_SUM", typeof(decimal));
                dt_L1.Columns.Add("EX_CONTENT", typeof(string));
                string empty1 = string.Empty;
                string empty2 = string.Empty;
                foreach (DataRow row in dataTable1.Rows)
                {
                    DataRow ex_Dr = dt_H2.NewRow();
                    DataRow dataRow = this._biz.CheckCredit(row["CD_PARTNER"].ToString(), row["CD_PARTNER_NAME"].ToString(), this._flexL.CDecimal(row["AM_SUM"]), ex_Dr);
                    if (dataRow != null)
                    {
                        if (dataRow["EX_CONTENT"].ToString() == "WARNING")
                            dt_H2.Rows.Add(dataRow.ItemArray);
                        else if (dataRow["EX_CONTENT"].ToString() == "ERROR")
                            dt_L1.Rows.Add(dataRow.ItemArray);
                    }
                }
                if ((dt_H2 != null || dt_L1 != null) && (dt_H2.Rows.Count != 0 || dt_L1.Rows.Count != 0))
                {
                    P_SA_CREDIT_SUB pSaCreditSub = new P_SA_CREDIT_SUB(dt_H2, dt_L1);
                    if (pSaCreditSub.ShowDialog() != DialogResult.OK)
                        return false;
                    if (pSaCreditSub.returnDt != null && pSaCreditSub.returnDt.Rows.Count != 0)
                    {
                        foreach (DataRow row in pSaCreditSub.returnDt.Rows)
                            dt_L1.ImportRow(row);
                    }
                }
                if (dt_L1 != null && dt_L1.Rows.Count != 0)
                {
                    foreach (DataRow row3 in dt_L1.Rows)
                    {
                        foreach (DataRow row4 in this._flexH.DataTable.Rows)
                        {
                            if (row3["CD_PARTNER"].ToString() == row4["CD_PARTNER"].ToString())
                                row4["S"] = "N";
                        }
                        foreach (DataRow row5 in this._flexL.DataTable.Rows)
                        {
                            if (row3["CD_PARTNER"].ToString() == row5["CD_PARTNER"].ToString())
                                row5["S"] = "N";
                        }
                    }
                }
                DataRow[] dataRowArray9 = this._flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                if (dataRowArray9 == null || dataRowArray9.Length == 0)
                {
                    this.ShowMessage("여신체크로 인해 출하시킬 수 있는 출하내역이 존재하지 않습니다.");
                    return false;
                }
                DataTable dt_LOT = null;
                DataTable dt_SERIAL = null;
                DataTable dt_ASN = null;
                DataTable dt_H3 = this._flexH.DataTable.Clone();
                DataTable dt_L2 = this._flexL.DataTable.Clone();
                string empty3 = string.Empty;
                T.SetDefaultValue(dt_H3);
                T.SetDefaultValue(dt_L2);
                DataRow[] dataRowArray10;
                if (this.rdo의뢰번호별.Checked)
                    dataRowArray10 = this._flexH.DataView.ToTable(true, "NO_GIR", "S").Select("S = 'Y'");
                else
                    dataRowArray10 = this._flexH.DataView.ToTable(true, "CD_PARTNER", "S").Select("S = 'Y'");
                DataTable dataTable2 = dataRowArray10[0].Table.Clone();
                dataTable2.PrimaryKey = new DataColumn[] { dataTable2.Columns[0] };
                List<string> stringList1 = new List<string>();
                foreach (DataRow dataRow in dataRowArray10)
                {
                    stringList1.Add(this.MainFrameInterface.GetStringYearMonth);
                    dataTable2.Rows.Add(dataRow.ItemArray);
                }
                string str1 = string.Empty;
                foreach (string str2 in stringList1.ToArray())
                    str1 = str1 + str2 + "|";
                List<string> stringList2 = new List<string>();
                foreach (string pipe in D.StringConvert.GetPipes(str1, 500))
                {
                    char[] separator = new char[] { '|' };
                    foreach (DataRow dataRow in (DataRow[])this.GetSeq(this.LoginInfo.CompanyCode, "SA", "07", pipe.Split(separator, StringSplitOptions.RemoveEmptyEntries)))
                        stringList2.Add(D.GetString(dataRow["DOCU_NO"]));
                }
                int num1 = 0;
                foreach (DataRow row6 in dataRowArray9)
                {
                    if ((!this.rdo의뢰번호별.Checked ? dt_H3.Select("S = 'Y' AND CD_PARTNER = '" + row6["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows) : dt_H3.Select("S = 'Y' AND NO_GIR = '" + row6["NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows)).Length == 0)
                    {
                        string str3 = stringList2[num1++].ToString();
                        decimal num2 = 1M;
                        DataRow[] dataRowArray11 = !this.rdo의뢰번호별.Checked ? this._flexL.DataTable.Select("S = 'Y' AND CD_PARTNER = '" + row6["CD_PARTNER"].ToString() + "'", "", DataViewRowState.CurrentRows) : this._flexL.DataTable.Select("S = 'Y' AND NO_GIR = '" + row6["NO_GIR"].ToString() + "'", "", DataViewRowState.CurrentRows);
                        foreach (DataRow row7 in dataRowArray11)
                        {
                            row7["NO_IO"] = str3;
                            row7["NO_IOLINE"] = num2++;
                            row7["DT_IO"] = this.dt출하일자.Text;
                            if (MA.ServerKey(false, new string[] { "COSMOCO" }))
                            {
                                DataTable dataTable3 = DBHelper.GetDataTable(string.Format("SELECT TOP 1 ME.FG_IO FROM SA_SOH A LEFT OUTER JOIN SA_TPSO ST ON A.CD_COMPANY = ST.CD_COMPANY AND ST.TP_SO = A.TP_SO LEFT OUTER JOIN MM_EJTP ME ON ST.CD_COMPANY = ME.CD_COMPANY AND ST.TP_GI = ME.CD_QTIOTP WHERE A.CD_COMPANY = '{0}' AND A.NO_SO = '{1}'", MA.Login.회사코드, D.GetString(row7["NO_PSO_MGMT"])));
                                if (dataTable3 != null && dataTable3.Rows.Count > 0)
                                    row7["FG_IO"] = D.GetString(dataTable3.Rows[0]["FG_IO"]);
                            }
                            else
                                row7["FG_IO"] = "010";
                            dt_L2.ImportRow(row7);
                        }
                        row6["NO_IO"] = str3;
                        row6["FG_TRANS"] = dataRowArray11[0]["FG_TRANS"].ToString();
                        int num3 = !this.rdo의뢰번호별.Checked ? 1 : (!(BASIC.GetMAEXC("출하등록-출하담당자와 출하의뢰자 동기화") == "001") ? 1 : 0);
                        row6["NO_EMP"] = num3 != 0 ? this.ctx담당자.CodeValue : row6["NO_GI_EMP"];
                        row6["CD_DEPT"] = this.CD_DEPT;
                        row6["DT_IO"] = this.dt출하일자.Text;
                        row6["YN_RETURN"] = "N";
                        if (!this.chk헤더비고적용여부.Checked)
                            row6["DC_RMK"] = this.txt_DcRmk.Text;
                        dt_H3.ImportRow(row6);
                        if (str3 == string.Empty)
                        {
                            this.ShowMessage("출하번호가 존재하지 않습니다.");
                            return false;
                        }
                    }
                }
                if (this.bStorageLocation && this.bStorageLocation2)
                {
                    DataTable dt = null;
                    if (this._flexL.DataTable != null)
                    {
                        dt = this._flexL.DataTable.Clone();
                        foreach (DataRow dataRow in this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows))
                            dt.Rows.Add(dataRow.ItemArray);
                    }
                    P_SA_SL_SUB_I pSaSlSubI = new P_SA_SL_SUB_I(dt, this.two_Unit_Mng, this.qtso_AddAllowYN, this.Am_Recalc);
                    if (pSaSlSubI.ShowDialog(this) != DialogResult.OK)
                        return false;
                    dt_L2 = pSaSlSubI.dtL;
                }
                if (string.Compare(this.MNG_LOT, "Y") == 0 && dt_L2 != null)
                {
                    DataRow[] dataRowArray12 = dt_L2.Select("NO_LOT = 'YES'");
                    if (dataRowArray12.Length > 0)
                    {
                        DataTable dataTable4 = dt_L2.Clone();
                        foreach (DataRow dataRow in dataRowArray12)
                            dataTable4.Rows.Add(dataRow.ItemArray);
                        DataTable dt = dataTable4.Copy();
                        string[] strArray = new string[] { "N",
                                                           string.Empty,
                                                           string.Empty };
                        if (MA.ServerKey(false, new string[] { "WINPLUS" }))
                        {
                            P_SA_Z_WINPLUS_LOT_SUB_I saZWinplusLotSubI = new P_SA_Z_WINPLUS_LOT_SUB_I(dt, strArray);
                            if (saZWinplusLotSubI.ShowDialog(this) != DialogResult.OK)
                                return false;
                            dt_LOT = saZWinplusLotSubI.dtL;
                        }
                        else if (MA.ServerKey(false, new string[] { "COSMO" }))
                        {
                            P_SA_Z_COSMO_LOT_SUB_I pSaZCosmoLotSubI = new P_SA_Z_COSMO_LOT_SUB_I(dt, strArray);
                            if (pSaZCosmoLotSubI.ShowDialog(this) != DialogResult.OK)
                                return false;
                            dt_LOT = pSaZCosmoLotSubI.dtL;
                        }
                        else if (MA.ServerKey(false, new string[] { "MANYO" }))
                        {
                            P_PU_Z_MANYO_LOT_SUB_I pPuZManyoLotSubI = new P_PU_Z_MANYO_LOT_SUB_I(dt, strArray);
                            if (pPuZManyoLotSubI.ShowDialog(this) != DialogResult.OK)
                                return false;
                            dt_LOT = pPuZManyoLotSubI.dtL;
                        }
                        else
                        {
                            P_PU_LOT_SUB_I pPuLotSubI = new P_PU_LOT_SUB_I(dt, strArray);
                            if (pPuLotSubI.ShowDialog(this) != DialogResult.OK)
                                return false;
                            dt_LOT = pPuLotSubI.dtL;
                        }
                    }
                }
                if (string.Compare(this.MNG_SERIAL, "Y") == 0 && dt_L2 != null)
                {
                    DataRow[] dataRowArray13 = dt_L2.Select("NO_SERL = 'YES'");
                    if (dataRowArray13.Length > 0)
                    {
                        DataTable dataTable5 = dt_L2.Clone();
                        foreach (DataRow dataRow in dataRowArray13)
                            dataTable5.Rows.Add(dataRow.ItemArray);
                        P_PU_SERL_SUB_I pPuSerlSubI = new P_PU_SERL_SUB_I(dataTable5.Copy());
                        pPuSerlSubI.YN_Rev = BASIC.GetMAEXC("납품의뢰등록 시리얼예약-사용유무");
                        if (pPuSerlSubI.ShowDialog(this) != DialogResult.OK)
                            return false;
                        dt_SERIAL = pPuSerlSubI.dtL;
                        if (dt_SERIAL != null && dt_SERIAL.Rows.Count > 0)
                            dt_SERIAL.Columns.Add("CD_PLANT", typeof(string));
                    }
                }
                DataRow[] dataRowArray14 = this._flexL.DataTable.Select("S = 'Y' AND NO_LOT = 'YES'", "", DataViewRowState.CurrentRows);
                if (this.MNG_LOT == "Y" && dataRowArray14.Length != 0 && (dt_LOT == null || dt_LOT.Rows.Count == 0))
                {
                    this.ShowMessage("LOT 품목 수불이 발생하였으나 해당 LOT가 생성되지 않았습니다.");
                    return false;
                }
                if (dt_LOT != null)
                {
                    DataTable dataTable6 = dt_LOT.Clone();
                    foreach (DataRow row8 in (InternalDataCollectionBase)dt_LOT.Rows)
                    {
                        DataRow[] dataRowArray15 = dataTable6.Select("출고번호 = '" + row8["출고번호"].ToString() + "' AND 출고항번 = '" + this._flexL.CDecimal(row8["출고항번"]) + "'");
                        if (dataRowArray15.Length == 0)
                        {
                            DataRow row9 = dataTable6.NewRow();
                            row9["출고번호"] = row8["출고번호"].ToString();
                            row9["출고항번"] = this._flexL.CDecimal(row8["출고항번"]);
                            row9["CD_ITEM"] = row8["CD_ITEM"];
                            row9["QT_GOOD_MNG"] = this._flexL.CDecimal(row8["QT_GOOD_MNG"]);
                            dataTable6.Rows.Add(row9);
                        }
                        else
                            dataRowArray15[0]["QT_GOOD_MNG"] = this._flexL.CDecimal(dataRowArray15[0]["QT_GOOD_MNG"]) + this._flexL.CDecimal(row8["QT_GOOD_MNG"]);
                    }
                    int num6 = 0;
                    int num7 = 0;
                    foreach (DataRow dataRow in dataRowArray14)
                    {
                        foreach (DataRow row in (InternalDataCollectionBase)dataTable6.Rows)
                        {
                            if (dataRow["NO_IO"].ToString() == row["출고번호"].ToString() && dataRow["NO_IOLINE"].ToString() == row["출고항번"].ToString())
                            {
                                if (dataRow["CD_ITEM"].ToString().Trim() != row["CD_ITEM"].ToString().Trim())
                                    ++num6;
                                if (dataRow["CD_ITEM"].ToString() == row["CD_ITEM"].ToString() && this._flexL.CDecimal(dataRow["QT_IO"]) != this._flexL.CDecimal(row["QT_GOOD_MNG"]))
                                    ++num7;
                            }
                        }
                    }
                    if (num6 > 0)
                    {
                        this.ShowMessage("LOT항번과 수불항번이 일치하지 않거나 품목이 일치하지 않습니다.");
                        return false;
                    }
                    if (num7 > 0)
                    {
                        this.ShowMessage("LOT수량과 수불수량이 일치하지 않습니다.");
                        return false;
                    }
                }
                DataTable dtLocation = null;
                if (Config.MA_ENV.YN_LOCATION == "Y")
                {
                    bool flag = false;
                    DataTable dataTable7 = dt_L2.Clone().Copy();
                    foreach (DataRow dataRow in dt_L2.Select())
                        dataTable7.LoadDataRow(dataRow.ItemArray, true);
                    dtLocation = P_OPEN_SUBWINDOWS.P_MA_LOCATION_I_SUB(dataTable7, out flag);
                    if (!flag)
                        return false;
                }
                if (MA.ServerKey(false, new string[1] { "ANJUN" }))
                {
                    DataRow[] dataRowArray16 = dt_L2.Select("FG_TRANS IN ('004', '005') AND PITEM_CD_USERDEF2 = 'Y' AND CD_USERDEF1 = 'Y'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray16.Length > 0)
                    {
                        DataTable dt = dt_L2.Clone();
                        foreach (DataRow dataRow in dataRowArray16)
                            dt.Rows.Add(dataRow.ItemArray);
                        P_SA_Z_ANJUN_GI_ASN_SUB saZAnjunGiAsnSub = new P_SA_Z_ANJUN_GI_ASN_SUB(dt);
                        if (saZAnjunGiAsnSub.ShowDialog(this) == DialogResult.OK)
                            dt_ASN = saZAnjunGiAsnSub.dtL;
                    }
                }
                if (!this._biz.Save(dt_H3, dt_L2, dtLocation, dt_LOT, dt_SERIAL, this.dt출하일자.Text, dt_ASN))
                    return false;
                this._flexH.AcceptChanges();
                this._flexL.AcceptChanges();
                return true;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            return false;
        }
        private void btn_apply_good_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else if (!this._flexL.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                }
                else
                {
                    DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                    }
                    else
                    {
                        MsgControl.ShowMsg("양품적용 중입니다. \n잠시만 기다려주세요!");
                        if (dataRowArray != null && dataRowArray.Length > 0)
                        {
                            foreach (DataRow dataRow in dataRowArray)
                            {
                                dataRow["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_GIR"]));
                                dataRow["QT_IO"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_GIR_IM"]));
                                dataRow["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_IO"]));
                                dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["ORG_AM"]));
                                dataRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["ORG_AM_EX"]));
                                dataRow["RT_EXCH"] = Unit.환율(DataDictionaryTypes.SA, D.GetDecimal(dataRow["RT_EXCH"]));
                                dataRow["AM_VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_VAT"]));
                                if (D.GetDecimal(dataRow["CHK_QT_GI"]) != 0M)
                                {
                                    if (D.GetString(dataRow["TP_UM_TAX"]) != "Y")
                                    {
                                        if (this.Am_Recalc == "000")
                                        {
                                            if (this.str금액계산사용여부 == "001")
                                            {
                                                dataRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["UM_EX_PSO"]) * D.GetDecimal(dataRow["QT_UNIT_MM"]));
                                                dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]));
                                                dataRow["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) * (D.GetDecimal(dataRow["RT_VAT"]) / 100M));
                                            }
                                            else
                                            {
                                                dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["ORG_AM"]));
                                                dataRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["ORG_AM_EX"]));
                                                dataRow["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) * (D.GetDecimal(dataRow["RT_VAT"]) / 100M));
                                            }
                                        }
                                        else
                                        {
                                            dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["ORG_AM"]));
                                            dataRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["ORG_AM_EX"]));
                                            dataRow["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) * (D.GetDecimal(dataRow["RT_VAT"]) / 100M));
                                        }
                                        decimal num4 = D.GetDecimal(dataRow["AM"]) + D.GetDecimal(dataRow["VAT"]);
                                        dataRow["UMVAT_GI"] = Unit.원화단가(DataDictionaryTypes.SA, num4 / D.GetDecimal(dataRow["QT_UNIT_MM"]));
                                    }
                                    else
                                    {
                                        decimal num5 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["QT_UNIT_MM"]) * D.GetDecimal(dataRow["UMVAT_GI"]));
                                        dataRow["AM"] = Global.MainFrame.LoginInfo.CompanyLanguage != 0 ? D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, num5 * (100M / (100M + D.GetDecimal(dataRow["RT_VAT"]))))) : decimal.Round(num5 * (100M / (100M + D.GetDecimal(dataRow["RT_VAT"]))), MidpointRounding.AwayFromZero);
                                        dataRow["VAT"] = (num5 - D.GetDecimal(dataRow["AM"]));
                                        dataRow["AM_EX"] = (D.GetDecimal(dataRow["RT_EXCH"]) == 0M ? 0M : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) / D.GetDecimal(dataRow["RT_EXCH"])));
                                    }
                                }
                                else
                                {
                                    dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["ORG_AM"]));
                                    dataRow["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["ORG_AM_EX"]));
                                    dataRow["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_VAT"]));
                                }
                                dataRow["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_EX"]) / D.GetDecimal(dataRow["QT_IO"]));
                                dataRow["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) / D.GetDecimal(dataRow["QT_IO"]));
                                if (D.GetString(dataRow["TP_UM_TAX"]) != "Y" && (this.Am_Recalc == "001" || D.GetDecimal(dataRow["CHK_QT_GI"]) == 0M))
                                {
                                    dataRow["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["UM_EX_PSO"]));
                                    dataRow["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["UM_EX_PSO"]) * D.GetDecimal(dataRow["RT_EXCH"]));
                                }
                                if (this.str금액계산사용여부 == "001")
                                    dataRow["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) + D.GetDecimal(dataRow["VAT"]));
                                this._flexL.SumRefresh();
                            }
                        }
                        MsgControl.CloseMsg();
                        this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn양품적용.Text });
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void Control_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                if (e.HelpID <= HelpID.P_MA_PITEM_SUB)
                {
                    switch (e.HelpID)
                    {
                        case HelpID.P_MA_CODE_SUB:
                            e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
                            break;
                        case HelpID.P_MA_CODE_SUB1:
                            e.HelpParam.P41_CD_FIELD1 = "MA_B000082";
                            break;
                        default:
                            if (e.HelpID != HelpID.P_MA_PITEM_SUB)
                                break;
                            e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue == null ? string.Empty : this.cbo공장.SelectedValue.ToString();
                            break;
                    }
                }
                else
                {
                    switch (e.HelpID)
                    {
                        case HelpID.P_MA_SL_SUB:
                            e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue == null ? string.Empty : this.cbo공장.SelectedValue.ToString();
                            break;
                        case HelpID.P_MA_SL_SUB1:
                            e.HelpParam.P09_CD_PLANT = this.cbo공장.SelectedValue == null ? string.Empty : this.cbo공장.SelectedValue.ToString();
                            break;
                        default:
                            if (e.HelpID != HelpID.P_PU_EJTP_SUB)
                            {
                                if (e.HelpID != HelpID.P_SA_TPSO_SUB1)
                                    break;
                                e.HelpParam.P61_CODE1 = "N";
                                e.HelpParam.P62_CODE2 = "Y";
                                break;
                            }
                            e.HelpParam.P61_CODE1 = "010|041|042|";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void Control_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                switch (((Control)sender).Name)
                {
                    case "bp_Emp":
                        this.CD_DEPT = e.HelpReturn.Rows[0]["CD_DEPT"].ToString();
                        break;
                    case "ctx창고":
                        if (this.bStorageLocation)
                        {
                            this.bp_WH.CodeValue = D.GetString(e.HelpReturn.Rows[0]["CD_WH"]);
                            this.bp_WH.CodeName = D.GetString(e.HelpReturn.Rows[0]["NM_WH"]);
                            break;
                        }
                        break;
                }
                if (this.ctx창고.CodeValue == string.Empty)
                    this.btn_apply.Enabled = false;
                else
                    this.btn_apply.Enabled = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void btn_apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._flexL == null || this._flexL.DataTable == null || this._flexL.DataTable.Rows.Count == 0)
                {
                    this.ShowMessage("조회 후 적용 하시기 바랍니다.");
                }
                else
                {
                    DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);
                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        this.ShowMessage(공통메세지.선택된자료가없습니다, new string[0]);
                    }
                    else
                    {
                        this._flexL.Redraw = false;
                        foreach (DataRow dataRow in dataRowArray)
                        {
                            dataRow["CD_SL"] = this.ctx창고.CodeValue;
                            dataRow["NM_SL"] = this.ctx창고.CodeName;
                            if (this.bStorageLocation)
                            {
                                dataRow["CD_WH"] = this.bp_WH.CodeValue;
                                dataRow["NM_WH"] = this.bp_WH.CodeName;
                            }
                        }
                        string strCD_ITEMS = "";
                        DataTable dataTable1 = new DataTable()
                        {
                            Columns = { "CD_ITEM" }
                        };
                        dataTable1.PrimaryKey = new DataColumn[] { dataTable1.Columns["CD_ITEM"] };
                        foreach (DataRow row in this._flexL.DataTable.Rows)
                        {
                            if (dataTable1.Rows.Find(D.GetString(row["CD_ITEM"])) == null)
                            {
                                strCD_ITEMS = strCD_ITEMS + D.GetString(row["CD_ITEM"]) + "|";
                                dataTable1.Rows.Add(D.GetString(row["CD_ITEM"]));
                            }
                        }
                        DataTable dataTable2 = this.SearchInv(strCD_ITEMS, D.GetString(this.ctx창고.CodeValue));
                        foreach (DataRow row in this._flexL.DataTable.Rows)
                        {
                            DataRow dataRow;
                            if (Config.MA_ENV.PJT형여부 == "N")
                                dataRow = dataTable2.Rows.Find(new object[] { D.GetString(row["CD_ITEM"]),
                                                                              D.GetString(row["CD_SL"]) });
                            else
                                dataRow = dataTable2.Rows.Find(new object[] { D.GetString(row["CD_ITEM"]),
                                                                              D.GetString(row["CD_SL"]),
                                                                              D.GetString(row["NO_PROJECT"]) });
                            if (dataRow != null)
                            {
                                if (row.RowState == DataRowState.Unchanged)
                                {
                                    row["QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
                                    row.AcceptChanges();
                                }
                                else
                                    row["QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
                            }
                        }
                        this._flexL.SumRefresh();
                        this._flexL.Redraw = true;
                        this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.btn_apply.Text });
                    }
                }
            }
            catch (Exception ex)
            {
                this._flexL.Redraw = true;
                this.MsgEnd(ex);
            }
            finally
            {
                this._flexL.Redraw = true;
            }
        }
        private void ctx창고_Leave(object sender, EventArgs e)
        {
            if (this.ctx창고.CodeValue == string.Empty)
                this.btn_apply.Enabled = false;
            else
                this.btn_apply.Enabled = true;
        }
        private void ctx창고_Validated(object sender, EventArgs e)
        {
            if (this.ctx창고.CodeValue == string.Empty)
                this.btn_apply.Enabled = false;
            else
                this.btn_apply.Enabled = true;
        }
        private void CommonComboBox_KeyEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Return)
                return;
            SendKeys.SendWait("{TAB}");
        }
        public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
        {
            if (this.rdo의뢰번호별.Checked)
                Settings.Default.auto_No = "GIR";
            if (this.rdo거래처별.Checked)
                Settings.Default.auto_No = "PARTNER";
            Settings.Default.의뢰비고적용여부 = this.chk헤더비고적용여부.Checked;
            Settings.Default.Save();
            return base.OnToolBarExitButtonClicked(sender, e);
        }
        private void rdo_Yes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.rdo_Yes.Checked)
                    return;
                object[] objArray = new object[] { this.dt출하일자.Text,
                                                   this.cbo공장.SelectedValue == null ? string.Empty : this.cbo공장.SelectedValue.ToString(),
                                                   this.ctx거래처.CodeValue,
                                                   this.ctx거래처.CodeName,
                                                   this.ctx담당자.CodeValue,
                                                   this.ctx담당자.CodeName };
                if (this.MainFrameInterface.IsExistPage("P_SA_GIM_REG", false))
                    this.UnLoadPage("P_SA_GIM_REG", false);
                if (!this.LoadPageFrom("P_SA_GIM_REG", MA.PageName("P_SA_GIM_REG"), this.Grant, objArray))
                {
                    this.ShowMessage(공통메세지.작업을정상적으로처리하지못했습니다, new string[0]);
                }
                this.rdo_Yes.Checked = false;
                this.rdo_Not.Checked = true;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void cbo공장_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.Plant_Clear();
            base.OnToolBarSearchButtonClicked(sender, e);
        }
        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                    return;
                DataTable dataTable1 = null;
                string str1 = this._flexH[e.NewRange.r1, "NO_GIR"].ToString();
                string str2 = "NO_GIR = '" + str1 + "'";
                string empty = string.Empty;
                string str3 = !MA.ServerKey(false, new string[] { "THV" }) ? this.ctx영업그룹.CodeValue : this.bpc영업그룹.QueryWhereIn_Pipe;
                if (this._flexH.DetailQueryNeed)
                {
                    DataTable dataTable2 = this._biz.SearchDetail(new object[] { this.LoginInfo.CompanyCode,
                                                                                 str1,
                                                                                 this.dtp일자.StartDateToString,
                                                                                 this.dtp일자.EndDateToString,
                                                                                 string.Empty,
                                                                                 string.Empty,
                                                                                 this.ctx납품처.CodeValue,
                                                                                 this.ctx출하형태.CodeValue,
                                                                                 "N",
                                                                                 D.GetString(this.cbo운송방법.SelectedValue),
                                                                                 this.ctx출하의뢰자.CodeValue,
                                                                                 D.GetString(this.cbo일자구분.SelectedValue),
                                                                                 str3,
                                                                                 this.bpc수주형태.QueryWhereIn_Pipe,
                                                                                 this.ctx프로젝트.CodeValue,
                                                                                 D.GetString(this.ctx제품군.CodeValue),
                                                                                 D.GetString(this.bpc의뢰창고.QueryWhereIn_Pipe),
                                                                                 D.GetString(this.cbo거래처그룹.SelectedValue),
                                                                                 D.GetString(this.cbo거래처그룹2.SelectedValue),
                                                                                 D.GetString(this.cbo사용자정의.SelectedValue),
                                                                                 D.GetString(this.cbo사용자정의2.SelectedValue),
                                                                                 MA.Login.사원번호,
                                                                                 this.bpc생산파트.QueryWhereIn_Pipe,
                                                                                 this.ctx품목.CodeValue,
                                                                                 Global.SystemLanguage.MultiLanguageLpoint,
                                                                                 Global.MainFrame.ServerKey != "SUNGBO" ?  string.Empty :  D.GetString(this.cbo배송구분.SelectedValue),
                                                                                 Global.MainFrame.ServerKey != "SUNGBO" ?  string.Empty :  this.txt차량번호.Text,
                                                                                 Global.MainFrame.ServerKey != "DSTECH" || Global.MainFrame.ServerKey != "ANYONE" ? string.Empty : this.txt고객납품의뢰번호.Text }, D.GetString(this.cbo일자구분.SelectedValue));
                    if (MA.ServerKey(false, new string[] { "SFNB",
                                                           "THV",
                                                           "DAOU" }))
                    {
                        string strCD_ITEMS = "";
                        DataTable dataTable3 = new DataTable()
                        {
                            Columns = { "CD_ITEM" }
                        };
                        dataTable3.PrimaryKey = new DataColumn[] { dataTable3.Columns["CD_ITEM"] };
                        foreach (DataRow row in dataTable2.Rows)
                        {
                            if (dataTable3.Rows.Find(D.GetString(row["CD_ITEM"])) == null)
                            {
                                strCD_ITEMS = strCD_ITEMS + D.GetString(row["CD_ITEM"]) + "|";
                                dataTable3.Rows.Add(D.GetString(row["CD_ITEM"]));
                            }
                        }
                        DataTable dataTable4 = this.SearchInv(strCD_ITEMS, "");
                        foreach (DataRow row in dataTable2.Rows)
                        {
                            DataRow dataRow;
                            if (Config.MA_ENV.PJT형여부 == "N")
                                dataRow = dataTable4.Rows.Find(new object[] { D.GetString(row["CD_ITEM"]),
                                                                              D.GetString(row["CD_SL"]) });
                            else
                                dataRow = dataTable4.Rows.Find(new object[] { D.GetString(row["CD_ITEM"]),
                                                                              D.GetString(row["CD_SL"]),
                                                                              D.GetString(row["NO_PROJECT"]) });
                            if (dataRow != null)
                            {
                                if (row.RowState == DataRowState.Unchanged)
                                {
                                    row["QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
                                    row.AcceptChanges();
                                }
                                else
                                    row["QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
                            }
                        }
                    }
                    dataTable2.DefaultView.Sort = "NO_GIR ASC, SEQ_GIR ASC";
                    dataTable1 = dataTable2.DefaultView.ToTable();
                }
                this._flexL.BindingAdd(dataTable1, str2);
                this._flexL.SetDummyColumn(new string[] { "S",
                                                          "CD_SL",
                                                          "NM_SL",
                                                          "QT_UNIT_MM",
                                                          "QT_IO" });
                this._flexH.DetailQueryNeed = false;
                new Common.Setting().GridCheck(this._flexL, "S='Y'", true);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void _flexL_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                this.txt납품처.Text = this._flexL[this._flexL.Row, "LN_PARTNER"].ToString();
                this.txt수주번호.Text = this._flexL[this._flexL.Row, "NO_PSO_MGMT"].ToString();
                this.txtLC번호.Text = this._flexL[this._flexL.Row, "NO_LC"].ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                e.Parameter.P09_CD_PLANT = this._flexL[this._flexL.Row, "CD_PLANT"].ToString();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void _flexL_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
        {
            try
            {
                if (e.Result.HelpID == HelpID.P_MA_SL_SUB)
                {
                    DataTable dataTable = this.SearchInv(D.GetString(this._flexL[e.Row, "CD_ITEM"]) + "|", D.GetString((object)e.Result.CodeValue));
                    DataRow dataRow;
                    if (Config.MA_ENV.PJT형여부 == "N")
                        dataRow = dataTable.Rows.Find(new object[] { D.GetString(this._flexL[e.Row, "CD_ITEM"]),
                                                                     D.GetString(e.Result.CodeValue) });
                    else
                        dataRow = dataTable.Rows.Find(new object[] { D.GetString(this._flexL[e.Row, "CD_ITEM"]),
                                                                     D.GetString((object) e.Result.CodeValue),
                                                                     D.GetString(this._flexL[e.Row, "NO_PROJECT"]) });
                    if (dataRow == null)
                        return;
                    if (this._flexL.DataView[this._flexL.Rows.Count - this._flexL.Rows.Fixed - 1].Row.RowState == DataRowState.Unchanged)
                    {
                        this._flexL[e.Row, "QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
                        this._flexL.DataView[this._flexL.Rows.Count - this._flexL.Rows.Fixed - 1].Row.AcceptChanges();
                    }
                    else
                        this._flexL[e.Row, "QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void _flex_CheckHeaderClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow)
                    return;
                FlexGrid flexGrid = sender as FlexGrid;
                switch (flexGrid.Name)
                {
                    case "_flexH":
                        Common.Setting setting = new Common.Setting();
                        string empty = string.Empty;
                        string str1 = !MA.ServerKey(false, new string[] { "THV" }) ? this.ctx영업그룹.CodeValue : this.bpc영업그룹.QueryWhereIn_Pipe;
                        if (this._flexH.GetCellCheck(this._flexH.Row, (this._flexH.Cols["S"]).Index) == CheckEnum.Unchecked)
                        {
                            setting.GridCheck(this._flexL, "S = 'Y'", false);
                            return;
                        }
                        string 멀티의뢰번호 = string.Empty;
                        for (int index = this._flexH.Rows.Fixed; index < this._flexH.Rows.Count; ++index)
                        {
                            if (this._flexH.DetailQueryNeedByRow(index))
                            {
                                멀티의뢰번호 = 멀티의뢰번호 + D.GetString(this._flexH[index, "NO_GIR"]) + "|";
                                this._flexH.SetDetailQueryNeedByRow(index, false);
                            }
                        }
                        if (멀티의뢰번호 == string.Empty)
                        {
                            setting.GridCheck(this._flexL, "S = 'N'", true);
                            return;
                        }
                        object[] objArray = new object[] { this.LoginInfo.CompanyCode,
                                                           "",
                                                           this.dtp일자.StartDateToString,
                                                           this.dtp일자.EndDateToString,
                                                           string.Empty,
                                                           string.Empty,
                                                           this.ctx납품처.CodeValue,
                                                           this.ctx출하형태.CodeValue,
                                                           "N",
                                                           D.GetString(this.cbo운송방법.SelectedValue),
                                                           D.GetString(this.ctx출하의뢰자.CodeValue),
                                                           D.GetString(this.cbo일자구분.SelectedValue),
                                                           D.GetString(str1),
                                                           D.GetString(this.bpc수주형태.QueryWhereIn_Pipe),
                                                           D.GetString(this.ctx프로젝트.CodeValue),
                                                           D.GetString(this.ctx제품군.CodeValue),
                                                           D.GetString(this.bpc의뢰창고.QueryWhereIn_Pipe),
                                                           D.GetString(this.cbo거래처그룹.SelectedValue),
                                                           D.GetString(this.cbo거래처그룹2.SelectedValue),
                                                           D.GetString(this.cbo사용자정의.SelectedValue),
                                                           D.GetString(this.cbo사용자정의2.SelectedValue),
                                                           MA.Login.사원번호,
                                                           this.bpc생산파트.QueryWhereIn_Pipe,
                                                           this.ctx품목.CodeValue,
                                                           Global.SystemLanguage.MultiLanguageLpoint,
                                                           Global.MainFrame.ServerKey != "SUNGBO" ?  string.Empty : D.GetString(this.cbo배송구분.SelectedValue),
                                                           Global.MainFrame.ServerKey != "SUNGBO" ?  string.Empty : this.txt차량번호.Text };
                        DataTable dataTable1 = this._biz.SearchCheckHeader(멀티의뢰번호, objArray);
                        string strCD_ITEMS = "";
                        DataTable dataTable2 = new DataTable()
                        {
                            Columns = { "CD_ITEM" }
                        };
                        dataTable2.PrimaryKey = new DataColumn[] { dataTable2.Columns["CD_ITEM"] };
                        foreach (DataRow row in dataTable1.Rows)
                        {
                            if (dataTable2.Rows.Find(D.GetString(row["CD_ITEM"])) == null)
                            {
                                strCD_ITEMS = strCD_ITEMS + D.GetString(row["CD_ITEM"]) + "|";
                                dataTable2.Rows.Add(D.GetString(row["CD_ITEM"]));
                            }
                        }
                        DataTable dataTable3 = this.SearchInv(strCD_ITEMS, "");
                        foreach (DataRow row in dataTable1.Rows)
                        {
                            DataRow dataRow;
                            if (Config.MA_ENV.PJT형여부 == "N")
                                dataRow = dataTable3.Rows.Find(new object[] { D.GetString(row["CD_ITEM"]),
                                                                              D.GetString(row["CD_SL"]) });
                            else
                                dataRow = dataTable3.Rows.Find(new object[] { D.GetString(row["CD_ITEM"]),
                                                                              D.GetString(row["CD_SL"]),
                                                                              D.GetString(row["NO_PROJECT"]) });
                            if (dataRow != null)
                            {
                                if (row.RowState == DataRowState.Unchanged)
                                {
                                    row["QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
                                    row.AcceptChanges();
                                }
                                else
                                    row["QT_INV"] = D.GetDecimal(dataRow["QT_INV"]);
                            }
                        }
                        string str2 = "NO_GIR = '" + D.GetString(this._flexH["NO_GIR"]) + "'";
                        this._flexL.BindingAdd(dataTable1, str2);
                        setting.GridCheck(this._flexL, "S = 'N'", true);
                        break;
                    case "_flexL":
                        if (!this._flexL.HasNormalRow)
                            return;
                        this._flexH["S"] = this._flexL["S"].ToString();
                        break;
                }
                if (flexGrid.DataTable.Select("S = 'Y'").Length > 0)
                    this.ToolBarSaveButtonEnabled = true;
                else
                    this.ToolBarSaveButtonEnabled = false;
                this.Auth();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                FlexGrid flexGrid = sender as FlexGrid;
                switch (flexGrid.Name)
                {
                    case "_flexH":
                        if (!this._flexH.HasNormalRow)
                            return;
                        switch (this._flexH.Cols[e.Col].Name)
                        {
                            case "S":
                                this._flexH["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";
                                for (int index = this._flexL.Rows.Fixed; index < this._flexL.Rows.Count; ++index)
                                    this._flexL[index, "S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";
                                break;
                        }
                        break;
                    case "_flexL":
                        string str1 = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
                        string str2 = ((FlexGrid)sender).EditData;
                        if (str1.ToUpper() == str2.ToUpper() || !this._flexL.HasNormalRow)
                            return;
                        switch (this._flexL.Cols[e.Col].Name)
                        {
                            case "S":
                                this._flexL["S"] = e.Checkbox == CheckEnum.Checked ? "Y" : "N";
                                if (this._flexL.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows).Length != 0)
                                {
                                    this._flexH.SetCellCheck(this._flexH.Row, (this._flexH.Cols["S"]).Index, CheckEnum.Checked);
                                    break;
                                }
                                this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Unchecked);
                                break;
                            case "QT_UNIT_MM":
                                if (D.GetDecimal(str2) <= 0M)
                                {
                                    this.ShowMessage(공통메세지._은_보다커야합니다, new string[] { this._flexL.Cols["QT_UNIT_MM"].Caption,
                                                                                                   "0" });
                                    this._flexL["QT_UNIT_MM"] = str1;
                                    e.Cancel = true;
                                    return;
                                }
                                if (D.GetDecimal(str2) == 0M)
                                    this._flexL.SetCellCheck(this._flexL.Row, this._flexL.Cols["S"].Index, CheckEnum.Unchecked);
                                else
                                    this._flexL.SetCellCheck(this._flexL.Row, this._flexL.Cols["S"].Index, CheckEnum.Checked);
                                if (this._flexL.DataView.ToTable().Select("S = 'Y'", "", DataViewRowState.CurrentRows).Length != 0)
                                    this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Checked);
                                else
                                    this._flexH.SetCellCheck(this._flexH.Row, this._flexH.Cols["S"].Index, CheckEnum.Unchecked);
                                decimal num1 = D.GetDecimal(this._flexL["QT_GIR"]) + D.GetDecimal(this._flexL["QT_GIR"]) * (D.GetDecimal(this._flexL["RT_PLUS"]) / 100M);
                                if (!this.qtso_AddAllowYN && D.GetDecimal(this._flexL["QT_GIR"]) < D.GetDecimal(str2))
                                {
                                    this._flexL["QT_UNIT_MM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["QT_GIR"]));
                                    str2 = this._flexL["QT_GIR"].ToString();
                                }
                                else if (this.qtso_AddAllowYN && num1 < D.GetDecimal(str2))
                                {
                                    this._flexL["QT_UNIT_MM"] = 0M;
                                    str2 = "0";
                                    this.ShowMessage("출하수량이 의뢰수량을 초과하였습니다.");
                                }
                                this._flexL["QT_IO"] = !(D.GetDecimal(this._flexL["UNIT_SO_FACT"]) == 0M) ? Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["QT_UNIT_MM"]) * D.GetDecimal(this._flexL["UNIT_SO_FACT"])) : Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["QT_UNIT_MM"]));
                                this._flexL["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["QT_IO"]));
                                if (D.GetDecimal(str2) == 0M)
                                {
                                    this._flexL["AM_EX"] = 0M;
                                    this._flexL["AM"] = 0M;
                                    this._flexL["UM_EX"] = 0M;
                                    this._flexL["UM"] = 0M;
                                }
                                else if (D.GetDecimal(str2) != D.GetDecimal(this._flexL["QT_GIR"]))
                                {
                                    if (D.GetString(this._flexL["TP_UM_TAX"]) != "Y")
                                    {
                                        this._flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["UM_EX_PSO"]) * D.GetDecimal(str2));
                                        this._flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM_EX"]) * D.GetDecimal(this._flexL["RT_EXCH"]));
                                        this._flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM"]) * (D.GetDecimal(this._flexL["RT_VAT"]) / 100M));
                                        this._flexL["UMVAT_GI"] = Unit.원화단가(DataDictionaryTypes.SA, (D.GetDecimal(this._flexL["AM"]) + D.GetDecimal(this._flexL["VAT"])) / D.GetDecimal(str2));
                                    }
                                    else
                                    {
                                        decimal num3 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(str2) * D.GetDecimal(this._flexL["UMVAT_GI"]));
                                        this._flexL["AM"] = Global.MainFrame.LoginInfo.CompanyLanguage != 0 ? D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, num3 * (100M / (100M + D.GetDecimal(this._flexL["RT_VAT"]))))) : decimal.Round(num3 * (100M / (100M + D.GetDecimal(this._flexL["RT_VAT"]))), MidpointRounding.AwayFromZero);
                                        this._flexL["VAT"] = (num3 - D.GetDecimal(this._flexL["AM"]));
                                        this._flexL["AM_EX"] = (D.GetDecimal(this._flexL["RT_EXCH"]) == 0M ? 0M : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM"]) / D.GetDecimal(this._flexL["RT_EXCH"])));
                                    }
                                }
                                else if (D.GetDecimal(this._flexL["CHK_QT_GI"]) != 0M)
                                {
                                    if (D.GetString(this._flexL["TP_UM_TAX"]) != "Y")
                                    {
                                        if (this.Am_Recalc == "000")
                                        {
                                            this._flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["UM_EX_PSO"]) * D.GetDecimal(str2));
                                            this._flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM_EX"]) * D.GetDecimal(this._flexL["RT_EXCH"]));
                                            this._flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM"]) * (D.GetDecimal(this._flexL["RT_VAT"]) / 100M));
                                        }
                                        else
                                        {
                                            this._flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["ORG_AM"]));
                                            this._flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["ORG_AM_EX"]));
                                            this._flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM"]) * (D.GetDecimal(this._flexL["RT_VAT"]) / 100M));
                                        }
                                        this._flexL["UMVAT_GI"] = Unit.원화단가(DataDictionaryTypes.SA, (D.GetDecimal(this._flexL["AM"]) + D.GetDecimal(this._flexL["VAT"])) / D.GetDecimal(str2));
                                    }
                                    else
                                    {
                                        decimal num4 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(str2) * D.GetDecimal(this._flexL["UMVAT_GI"]));
                                        this._flexL["AM"] = Global.MainFrame.LoginInfo.CompanyLanguage != 0 ? D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, num4 * (100M / (100M + D.GetDecimal(this._flexL["RT_VAT"]))))) : decimal.Round(num4 * (100M / (100M + D.GetDecimal(this._flexL["RT_VAT"]))), MidpointRounding.AwayFromZero);
                                        this._flexL["VAT"] = (num4 - D.GetDecimal(this._flexL["AM"]));
                                        this._flexL["AM_EX"] = (D.GetDecimal(this._flexL["RT_EXCH"]) == 0M ? 0M : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM"]) / D.GetDecimal(this._flexL["RT_EXCH"])));
                                    }
                                }
                                else
                                {
                                    this._flexL["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["ORG_AM"]));
                                    this._flexL["AM_EX"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["ORG_AM_EX"]));
                                    this._flexL["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM_VAT"]));
                                }
                                if (D.GetDecimal(this._flexL["QT_IO"]) != 0M)
                                {
                                    this._flexL["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM_EX"]) / D.GetDecimal(this._flexL["QT_IO"]));
                                    this._flexL["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM"]) / D.GetDecimal(this._flexL["QT_IO"]));
                                }
                                if (D.GetString(this._flexL["TP_UM_TAX"]) != "Y" && (this.Am_Recalc == "001" || D.GetDecimal(this._flexL["CHK_QT_GI"]) == 0M))
                                {
                                    this._flexL["UM_EX"] = Unit.외화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["UM_EX_PSO"]));
                                    this._flexL["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["UM_EX_PSO"]) * D.GetDecimal(this._flexL["RT_EXCH"]));
                                }
                                if (Global.MainFrame.ServerKeyCommon == "SPLT")
                                    this._flexL["TOTAL_WEIGHT_QTIO"] = (D.GetDecimal(this._flexL["WEIGHT"]) * D.GetDecimal(str2));
                                if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("HMRFOOD"))
                                    this._flexL["QT_WEIGHT_QTIO"] = (D.GetDecimal(this._flexL["WEIGHT"]) == 0M ? 0M : Math.Ceiling(D.GetDecimal(str2) / D.GetDecimal(this._flexL["WEIGHT"])));
                                if (this.str금액계산사용여부 == "001")
                                    this._flexL["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM"]) + D.GetDecimal(this._flexL["VAT"]));
                                if (MA.ServerKey(true, new string[] { "DHF2" }))
                                {
                                    this._flexL["GI_WEIGHT"] = D.GetDecimal(this._flexL["WEIGHT"]) * D.GetDecimal(str2);
                                    break;
                                }
                                break;
                            case "QT_IO":
                                this._flexL["QT_GOOD_INV"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(str2));
                                break;
                        }
                        break;
                }
                DataRow[] dataRowArray = flexGrid.DataTable.Select("S = 'Y'");
                if (dataRowArray == null || dataRowArray.Length == 0)
                    return;
                if (dataRowArray.Length > 0)
                    this.ToolBarSaveButtonEnabled = true;
                else
                    this.ToolBarSaveButtonEnabled = false;
                this.Auth();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }
        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow || D.GetDecimal(this._flexL["QT_UNIT_MM"]) > 0M || D.GetString(ComFunc.전용코드("출하등록-재고출하반려도움창(제다전용)")) != "001" || !(this._flexL.Cols[this._flexL.Col].Name == "QT_GIR") || (new P_SA_SO_ADJUST_BAN_SUB(D.GetString(this._flexL["NO_PSO_MGMT"]), D.GetDecimal(this._flexL["NO_PSOLINE_MGMT"]))).ShowDialog() != DialogResult.OK)
                    return;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void _flex_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow)
                    return;
                if (Global.MainFrame.ServerKeyCommon.ToUpper().Contains("COSMOCO"))
                {
                    switch ((sender as FlexGrid).Name)
                    {
                        case "_flexL":
                            switch (this._flexL.Cols[e.Col].Name)
                            {
                                case "QT_UNIT_MM":
                                case "QT_IO":
                                    e.Cancel = true;
                                    break;
                            }
                            break;
                    }
                }
                if (MA.ServerKey(false, new string[] { "SSW" }))
                {
                    switch (this._flexL.Cols[e.Col].Name)
                    {
                        case "QT_UNIT_MM":
                            if (D.GetString(this._flexL["FG_TRANS"]) == "004" || D.GetString(this._flexL["FG_TRANS"]) == "005")
                            {
                                e.Cancel = true;
                                return;
                            }
                            break;
                    }
                }
                switch (this._flexL.Cols[e.Col].Name)
                {
                    case "UM_EX_PSO ":
                    case "AM_EX  ":
                    case "VAT ":
                        if (D.GetString(this._flexL["TP_UM_TAX"]) == "Y")
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                    case "UMVAT_GI":
                        if (D.GetString(this._flexL["TP_UM_TAX"]) != "Y")
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        public void Menu_Click(object sender, EventArgs e)
        {
            switch (((ToolStripItem)sender).Name)
            {
                case "현재고조회":
                    string str = Common.MultiString(this._flexL.DataView.ToTable(), "CD_ITEM", "|");
                    new P_PU_STOCK_SUB(this.cbo공장.SelectedValue.ToString(), str).ShowDialog(this);
                    break;
            }
        }
        private DataTable SearchInv(string strCD_ITEMS, string strCD_SL)
        {
            DataTable dataTable = this._biz.SearchInv(new object[] { this.LoginInfo.CompanyCode,
                                                                     D.GetString(this.cbo공장.SelectedValue),
                                                                     strCD_ITEMS,
                                                                     strCD_SL });
            if (Config.MA_ENV.PJT형여부 == "N")
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["CD_ITEM"],
                                                          dataTable.Columns["CD_SL"] };
            else
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["CD_ITEM"],
                                                          dataTable.Columns["CD_SL"],
                                                          dataTable.Columns["CD_PJT"] };
            return dataTable;
        }
        private bool Chk일자 => Checker.IsValid(this.dtp일자, true, this.DD("조회일"));
        private bool 중국고객부가세포함단가사용여부()
        {
            DataTable dataTable = DBHelper.GetDataTable("SELECT FG_LANG FROM MA_COMPANY WHERE CD_COMPANY = '" + MA.Login.회사코드 + "'");
            bool flag = false;
            if (dataTable != null && dataTable.Rows.Count > 0)
                flag = D.GetString(dataTable.Rows[0]["FG_LANG"]) == "3";
            return flag && ComFunc.전용코드("수주등록-부가세 포함단가 사용여부") == "001";
        }
        private void Plant_Clear()
        {
            this.ctx품목.CodeValue = string.Empty;
            this.ctx품목.CodeName = string.Empty;
            this.ctx창고.CodeValue = string.Empty;
            this.ctx창고.CodeName = string.Empty;
            this.bpc의뢰창고.Clear();
        }
        private void btn환종변경_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow)
                    return;
                string str1 = D.GetString(this.cbo환종.SelectedValue);
                DataRow[] dataRowArray = this._flexL.DataTable.Select("S='Y' AND QT_UNIT_MM <>0", "", DataViewRowState.CurrentRows);
                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage("출하수량이 있는, 선택된자료가없습니다. ");
                }
                else
                {
                    string str2;
                    if (MA.ServerKey(false, new string[] { "KYOTECH" }))
                    {
                        foreach (DataRow dataRow in dataRowArray)
                        {
                            if (!(D.GetString(dataRow["S"]) == "N"))
                            {
                                str2 = D.GetString(dataRow["CD_EXCH"]);
                                dataRow["CD_EXCH"] = str1;
                                if (D.GetString(dataRow["CD_EXCH"]) == "000")
                                    dataRow["RT_EXCH"] = 1;
                            }
                        }
                    }
                    else
                    {
                        for (int index = this._flexL.Rows.Fixed; index < this._flexL.Rows.Count; ++index)
                        {
                            if (!(D.GetString(this._flexL[index, "S"]) == "N"))
                            {
                                str2 = D.GetString(this._flexL[index, "CD_EXCH"]);
                                this._flexL[index, "CD_EXCH"] = (object)str1;
                                if (D.GetString(this._flexL[index, "CD_EXCH"]) == "000")
                                    this._flexL[index, "RT_EXCH"] = (object)1;
                            }
                        }
                    }
                    if (str1 == "000")
                    {
                        this.btn환율변경_Click(null, null);
                    }
                    else
                    {
                        this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("변경") });
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void btn환율변경_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow || !this.Chk환종)
                    return;
                decimal num1 = Unit.환율(DataDictionaryTypes.SA, this.cur환율.DecimalValue);
                if (num1 == 0M)
                {
                    this.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.DD("환율") });
                }
                else
                {
                    DataRow[] dataRowArray = this._flexL.DataTable.Select("S='Y' AND QT_UNIT_MM <>0", "", DataViewRowState.CurrentRows);
                    if (dataRowArray == null || dataRowArray.Length == 0)
                    {
                        this.ShowMessage("출하수량이 있는, 선택된자료가없습니다. ");
                    }
                    else
                    {
                        string str;
                        if (MA.ServerKey(false, new string[] { "KYOTECH" }))
                        {
                            foreach (DataRow dataRow in dataRowArray)
                            {
                                if (!(D.GetString(dataRow["S"]) == "N"))
                                {
                                    dataRow["RT_EXCH"] = !(D.GetString(dataRow["CD_EXCH"]) == "000") ? num1 : 1;
                                    str = D.GetString(dataRow["CD_EXCH"]);
                                    dataRow["AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM_EX"]) * D.GetDecimal(dataRow["RT_EXCH"]));
                                    dataRow["VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) * (D.GetDecimal(dataRow["RT_VAT"]) / 100M));
                                    dataRow["AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) + D.GetDecimal(dataRow["VAT"]));
                                    dataRow["UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(dataRow["AM"]) / D.GetDecimal(dataRow["QT_UNIT_MM"]));
                                    this._flexL.SumRefresh();
                                }
                            }
                        }
                        else
                        {
                            for (int index = this._flexL.Rows.Fixed; index < this._flexL.Rows.Count; ++index)
                            {
                                if (!(D.GetString(this._flexL[index, "S"]) == "N"))
                                {
                                    this._flexL[index, "RT_EXCH"] = !(D.GetString(this._flexL[index, "CD_EXCH"]) == "000") ? num1 : 1;
                                    str = D.GetString(this._flexL[index, "CD_EXCH"]);
                                    this._flexL[index, "AM"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[index, "AM_EX"]) * D.GetDecimal(this._flexL[index, "RT_EXCH"]));
                                    this._flexL[index, "VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[index, "AM"]) * (D.GetDecimal(this._flexL[index, "RT_VAT"]) / 100M));
                                    this._flexL[index, "AMT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[index, "AM"]) + D.GetDecimal(this._flexL[index, "VAT"]));
                                    this._flexL[index, "UM"] = Unit.원화단가(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[index, "AM"]) / D.GetDecimal(this._flexL[index, "QT_UNIT_MM"]));
                                }
                            }
                        }
                        this.ShowMessage(공통메세지._작업을완료하였습니다, new string[] { this.DD("변경") });
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private bool Chk환종 => !Checker.IsEmpty(this.cbo환종, this.DD("환종"));
        private void Set환율버튼()
        {
            if (D.GetString(this.cbo환종.SelectedValue) == "000")
            {
                this.cur환율.DecimalValue = 1M;
                this.cur환율.Enabled = false;
            }
            else if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                this.cur환율.Enabled = false;
            else
                this.cur환율.Enabled = true;
        }
        private void cbo환종_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (MA.기준환율.Option != 0)
                    this.cur환율.DecimalValue = MA.기준환율적용(this.dt출하일자.Text, D.GetString(this.cbo환종.SelectedValue.ToString()));
                if (!(D.GetString(this.cbo환종.SelectedValue.ToString()) == "000"))
                    return;
                this.cur환율.DecimalValue = 1M;
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        private void dtp일자_TextChanged(object sender, EventArgs e) => this.cbo환종_SelectionChangeCommitted(null, null);

    }
}

using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.BizOn.Erpu.Net.File;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.PR;
using Duzon.Windows.Print;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_PR_WO_MNG : PageBase
    {
        private P_CZ_PR_WO_MNG_BIZ _biz = new P_CZ_PR_WO_MNG_BIZ();

        public P_CZ_PR_WO_MNG()
        {
            try
            {
                this.InitializeComponent();
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

        protected override void InitPaint()
        {
            base.InitPaint();

            DataSet comboData = this.GetComboData("NC;MA_PLANT",
                                                  "S;PR_0000007",
                                                  "N;PR_0000006",
                                                  "N;MA_B000015");

            if (comboData != null)
            {
                this.cbo공장.DataSource = comboData.Tables[0];
                this.cbo공장.DisplayMember = "NAME";
                this.cbo공장.ValueMember = "CODE";

                if (comboData.Tables[0].Select("CODE = '" + this.LoginInfo.CdPlant + "'").Length == 1)
                    this.cbo공장.SelectedValue = this.LoginInfo.CdPlant;
                else if (this.cbo공장.Items.Count > 0)
                    this.cbo공장.SelectedIndex = 0;
                
                this.cbo지시구분.DataSource = comboData.Tables[1];
                this.cbo지시구분.DisplayMember = "NAME";
                this.cbo지시구분.ValueMember = "CODE";
                
                this._flexL.SetDataMap("ST_OP", comboData.Tables[2].Copy(), "CODE", "NAME");
                this._flexL.ShowButtons = ShowButtonsEnum.WhenEditing;
            }

            if (Pr_Global.bMfg_AuthH_YN)
            {
                DataSet dataSet = this._biz.Search_AUTH(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                       string.Empty,
                                                                       Global.MainFrame.LoginInfo.EmployeeNo,
                                                                       this.cbo공장.SelectedValue.ToString(),
                                                                       Global.SystemLanguage.MultiLanguageLpoint });

                if (dataSet != null && dataSet.Tables[7].Rows.Count != 0)
                {
                    foreach (DataRow row in dataSet.Tables[7].Rows)
                    {
                        if (!(D.GetString(row["FG_USE"]) == "") && !(D.GetString(row["FG_USE"]) == "N"))
                            this.bpc오더형태.AddItem2(D.GetString(row["CD_AUTH"]), D.GetString(row["NM_AUTH"]));
                    }
                }

                if (dataSet != null && dataSet.Tables[6].Rows.Count != 0)
                {
                    foreach (DataRow row in dataSet.Tables[6].Rows)
                    {
                        if (!(D.GetString(row["FG_USE"]) == "") && !(D.GetString(row["FG_USE"]) == "N"))
                            this.bpc작업장.AddItem2(D.GetString(row["CD_AUTH"]), D.GetString(row["NM_AUTH"]));
                    }
                }
            }
            else
            {
                object[] objArray = new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                   this.cbo공장.SelectedValue.ToString() };

                foreach (DataRow row in this._biz.Search_Tp_Wo(objArray).Rows)
                    this.bpc오더형태.AddItem2(D.GetString(row["TP_WO"]), D.GetString(row["NM_TP_WO"]));
                
                foreach (DataRow row in this._biz.Search_Cd_Wc(objArray).Rows)
                    this.bpc작업장.AddItem2(D.GetString(row["CD_WC"]), D.GetString(row["NM_WC"]));
            }

            this.cbo경로유형.DataSource = this._biz.Get전체경로유형(D.GetString(this.cbo공장.SelectedValue));
            this.cbo경로유형.ValueMember = "CODE";
            this.cbo경로유형.DisplayMember = "NAME";

            this.chk계획.Checked = false;
            this.chk확정.Checked = false;
            this.chk발행.Checked = true;
            this.chk시작.Checked = true;
            this.chk마감.Checked = false;
            this.cbo공장.AutoDropDown = false;
            
            this.periodPicker1.StartDateToString = this.MainFrameInterface.GetStringToday.ToString().Substring(0, 6) + "01";
            this.periodPicker1.EndDateToString = this.MainFrameInterface.GetStringToday.ToString().Substring(0, 8);
            
            this.ToolBarSearchButtonEnabled = true;
            this.cbo공장.Focus();
            this.oneGrid1.UseCustomLayout = true;
            this.bpPanelControl1.IsNecessaryCondition = true;
            this.bpPanelControl2.IsNecessaryCondition = true;
            this.oneGrid1.InitCustomLayout();
            
            if (BASIC.GetMAEXC("작업지시관리-작업지시삭제버튼활성화") == "100")
                this.btn작업지시삭제.Visible = false;

            if (Global.MainFrame.LoginInfo.GroupID.ToUpper() == "ADMIN")
			{
                this.btn임의입력.Visible = true;
                this.btn측정치삭제.Visible = true;
                this.btn측정치삭제1.Visible = true;
                this.btn열처리번호적용.Visible = true;
            }
            else
			{
                this.btn임의입력.Visible = false;
                this.btn측정치삭제.Visible = false;
                this.btn측정치삭제1.Visible = false;
                this.btn열처리번호적용.Visible = false;
            }
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flexH, this._flexL, this._flexDL };

            this._flexH.DetailGrids = new FlexGrid[] { this._flexL, this._flexDH, this._flexDL };
            this._flexDH.DetailGrids = new FlexGrid[] { this._flexDL };

            #region Header
            this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("CHK", "S", 20, true, CheckTypeEnum.Y_N);
            this._flexH.SetCol("YN_INPUT", "투입여부", 60, false, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NO_WO", "작업지시번호", 110);
            this._flexH.SetCol("NM_ST_WO", "작업상태", 60);
            this._flexH.SetCol("NM_FG_WO", "작업지시구분", 90);
            this._flexH.SetCol("NM_TP_ROUT", "오더형태", 90);
            this._flexH.SetCol("NM_TP_GI", "출고형태", 90);
            this._flexH.SetCol("NM_TP_GR", "입고형태", 90);
            this._flexH.SetCol("CD_ITEM", "품목코드", 90);
            this._flexH.SetCol("NM_ITEM", "품목명", 120);
            this._flexH.SetCol("TP_ITEM", "품목타입", 100);
            this._flexH.SetCol("STND_ITEM", "규격", 80);
            this._flexH.SetCol("UNIT_IM", "단위", 40);
            this._flexH.SetCol("QT_ITEM", "지시수량", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("QT_WORK", "작업수량", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("DT_REL", "시작일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_DUE", "종료일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_RELEASE", "RELEASE일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_CLOSE", "마감일", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NO_PJT", "프로젝트코드", 100, 20, false);
            this._flexH.SetCol("NM_PROJECT", "프로젝트명", 140, false);
            this._flexH.SetCol("SEQ_PROJECT", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 항번" : "프로젝트항번", 120, false, typeof(decimal));
            this._flexH.SetCol("NO_LOT", "LOT번호", 100, true);
            this._flexH.SetCol("FG_LOTNO", "LOT/SN", 100, false);
            this._flexH.SetCol("GRP_ITEM", "품목군코드", 100, false);
            this._flexH.SetCol("NM_ITEMGRP", "품목군", 100, false);
            this._flexH.SetCol("DC_RMK", "비고", 100, true);
            this._flexH.SetCol("DC_RMK2", "비고2", 100, true);
            this._flexH.SetCol("PATN_ROUT", "경로유형", 70, false);
            this._flexH.SetCol("NM_PATN_ROUT", "경로유형명", 120, false);
            this._flexH.SetCol("LN_PARTNER", "수주거래처명", 120, false);
            this._flexH.SetCol("NO_SO", "수주번호", 120, false);
            this._flexH.SetCol("NO_RELATION", "연동번호", 120, false);
            this._flexH.SetCol("NO_EMP", "담당자코드", 100);
            this._flexH.SetCol("NM_KOR", "담당자명", 100);

            this.UserDefinedSetting(this._flexH);
            
            this._flexH.SetCol("STND_DETAIL_ITEM", "세부규격", 100);
            this._flexH.SetCol("MAT_ITEM", "재질", 100);

            if (Config.MA_ENV.PJT형여부 == "Y")
            {
                this._flexH.SetCol("CD_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 코드" : "프로젝트 품목코드", 140, false, typeof(string));
                this._flexH.SetCol("NM_PJT_ITEM", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 명" : "프로젝트 품목명", 140, false, typeof(string));
                this._flexH.SetCol("PJT_ITEM_STND", Config.MA_ENV.YN_UNIT == "Y" ? "UNIT 규격" : "프로젝트 품목규격", 140, false, typeof(string));
            }

            this._flexH.SetCol("NM_CLS_L", "대분류", 100);
            this._flexH.SetCol("NM_CLS_M", "중분류", 100);
            this._flexH.SetCol("NM_CLS_S", "소분류", 100);
            this._flexH.SetCol("DT_LIMIT", "유효일자", 70, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NO_SOURCE", "NO_SOURCE", false);
            this._flexH.SetCol("CD_PARTNER_ITEM", "주거래처코드", false);
            this._flexH.SetCol("LN_PARTNER_ITEM", "주거래처", false);
            this._flexH.SetCol("GRP_MFG", "제품군코드", false);
            this._flexH.SetCol("NM_GRP_MFG", "제품군", false);
            this._flexH.SetCol("TP_ROUT", "오더형태코드", false);
            this._flexH.SetCol("QT_RCVREQ", "입고의뢰수량", 70, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexH.SetCol("FILE_PATH_MNG", "첨부파일", 90, false);
            this._flexH.SetCol("EN_ITEM", "품목명(영)", false);
            this._flexH.SetCol("NM_MAKER", "Maker", false);
            this._flexH.SetCol("BARCODE", "BARCODE", false);
            this._flexH.SetCol("NO_MODEL", "모델번호", false);
            this._flexH.SetCol("NO_DESIGN", "도면번호", false);
            this._flexH.SetCol("NO_PRQ", "요청번호", false);
            this._flexH.SetCol("NO_HEAT", "소재HEAT번호", 100, true);

            this._flexH.SetDummyColumn("CHK");
            this._flexH.SetDummyColumn("FILE_PATH_MNG");
            this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flexH.AfterRowChange += new RangeEventHandler(this._flexM_AfterRowChange);
            this._flexH.StartEdit += new RowColEventHandler(this._flex_StartEdit);
            this._flexH.ValidateEdit += new ValidateEditEventHandler(this._flex_ValidateEdit);
            this._flexH.HelpClick += new EventHandler(this._flex_HelpClick);
            #endregion

            #region Line
            this._flexL.BeginSetting(1, 1, false);

            this._flexL.SetCol("NM_FG_WC", "W/C타입", 90);
            this._flexL.SetCol("CD_OP", "OP", 40);
            this._flexL.SetCol("NM_OP", "공정명", 100);
            this._flexL.SetCol("NM_WC", "작업장명", 80);
            this._flexL.SetCol("ST_OP", "상태", 60);
            this._flexL.SetCol("QT_WO", "지시수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_START", "입고수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_WORK", "작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_REJECT", "불량수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_REWORK", "재작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_MOVE", "이동수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_WIP", "대기수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("RT_WORK", "진행율(%)", 70, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("DT_REL", "시작일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DT_DUE", "종료일", 70, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexL.SetCol("DC_RMK", "비고", 100, 100, true);
            this._flexL.SetCol("DC_RMK_1", "비고1", 100, 100, true);
            this._flexL.SetCol("NO_SFT", "SFT", 80);
            this._flexL.SetCol("NM_SFT", "SFT명", 100);
            this._flexL.SetCol("CD_EQUIP", "설비코드", 80, true);
            this._flexL.SetCol("NM_EQUIP", "설비명", 100);
            this._flexL.SetCol("NO_EMP", "담당자코드", 100);
            this._flexL.SetCol("NM_KOR", "담당자명", 100);
            this._flexL.SetCol("YN_OPOUT", "공정외주 발주여부", 120, false, CheckTypeEnum.Y_N);
            this._flexL.SetCol("QT_PO", "외주발주수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flexL.SetCol("QT_RCV", "외주작업수량", 90, false, typeof(decimal), FormatTpType.QUANTITY);

            this._flexL.SetCodeHelpCol("CD_EQUIP", "H_PR_EQUIP_SUB", ShowHelpEnum.Always, new string[] { "CD_EQUIP", "NM_EQUIP" }, new string[] { "CD_EQUIP", "NM_EQUIP" });

            this._flexL.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            this._flexL.StartEdit += new RowColEventHandler(this._flexL_StartEdit);
            #endregion

            #region 측정치

            #region Header
            this._flexDH.BeginSetting(1, 1, false);

            this._flexDH.SetCol("SEQ_WO", "순번", 100);
            this._flexDH.SetCol("DT_NO_ID", "투입년월", 100, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flexDH.SetCol("NO_SEQ", "일련번호", 100);
            this._flexDH.SetCol("NO_ID", "ID번호", 100);
            this._flexDH.SetCol("NO_ID_OLD", "ID번호(이전)", 100, true);
            this._flexDH.SetCol("NO_HEAT", "소재HEAT번호", 100, true);
            this._flexDH.SetCol("YN_BAD", "불량여부", 60, false, CheckTypeEnum.Y_N);

            this._flexDH.SettingVersion = "0.0.0.1";
            this._flexDH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region Line
            this._flexDL.BeginSetting(1, 1, false);

            this._flexDL.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
            this._flexDL.SetCol("CD_OP", "OP코드", 100);
            this._flexDL.SetCol("NM_WC", "작업장", 100, false, typeof(string), FormatTpType.YEAR_MONTH);
            this._flexDL.SetCol("NM_OP", "공정명", 100);
            this._flexDL.SetCol("CD_MEASURE", "측정장비", 100);
            this._flexDL.SetCol("DC_ITEM", "구분", 100);
            this._flexDL.SetCol("DC_LOCATION", "위치", 100);
            this._flexDL.SetCol("TP_DATA", "대표값유형", 100);
            this._flexDL.SetCol("NO_DATA1", "측정치1", 100, true);
            this._flexDL.SetCol("NO_DATA2", "측정치2", 100, true);
            this._flexDL.SetCol("NO_DATA3", "측정치3", 100, true);
            this._flexDL.SetCol("NO_DATA4", "측정치4", 100, true);
            this._flexDL.SetCol("NO_DATA5", "측정치5", 100, true);
            this._flexDL.SetCol("NO_HEAT", "열처리번호", 100, true);
            this._flexDL.SetCol("YN_MARKING", "마킹여부", 60, true, CheckTypeEnum.Y_N);
            this._flexDL.SetCol("NM_REJECT", "불량종류", 100);
            this._flexDL.SetCol("NM_RESOURCE", "불량원인", 100);

            this._flexDL.SetDummyColumn("S");
            this._flexDL.SetDataMap("CD_MEASURE", Global.MainFrame.GetComboDataCombine("S;CZ_WIN0012"), "CODE", "NAME");
            this._flexDL.SetDataMap("TP_DATA", MA.GetCodeUser(new string[] { "MIN", "MAX" }, new string[] { "최소값", "최대값" }), "CODE", "NAME");

            this._flexDL.SettingVersion = "0.0.0.1";
            this._flexDL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #endregion
        }

		private void InitEvent()
		{
            this.DataChanged += new EventHandler(this.Page_DataChanged);

            this.btn작업지시마감취소.Click += new EventHandler(this.btn작업지시마감취소_Click);
            this.btn작업지시마감.Click += new EventHandler(this.btn작업지시마감_Click);
            this.btn작업지시서출력.Click += new EventHandler(this.btn작업지시서출력_Click);
            this.btn소요자재정보.Click += new EventHandler(this.btn소요자재정보_Click);
            this.btn계획작업지시확정.Click += new EventHandler(this.btn계획작업지시확정_Click);
            this.btn작업지시확정취소.Click += new EventHandler(this.btn작업지시확정취소_Click);
            this.btn작업지시삭제.Click += new EventHandler(this.btn작업지시삭제_Click);

            this.btn생산투입.Click += new EventHandler(this.Btn생산투입_Click);
            this.btn생산투입취소.Click += new EventHandler(this.Btn생산투입취소_Click);
			this.btn임의입력.Click += Btn자동입력_Click;
			this.btn측정치삭제.Click += Btn측정치삭제_Click;
			this.btn열처리번호적용.Click += Btn열처리번호적용_Click;
			this.btn소재Heat번호적용.Click += Btn소재Heat번호적용_Click;
			this.btn측정치삭제1.Click += Btn측정치삭제1_Click;

            this.cbo공장.SelectedValueChanged += new EventHandler(this.cbo공장_SelectedValueChanged);

			this._flexH.AfterRowChange += _flexH_AfterRowChange;
			this._flexDH.AfterRowChange += _flexDH_AfterRowChange;
			this._flexDH.AfterEdit += _flexDH_AfterEdit;

			this._flexL.BeforeCodeHelp += _flexL_BeforeCodeHelp;
        }

        private void Btn측정치삭제1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                if (this.cur순번From.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.cur순번From.Text, "0");
                    return;
                }

                if (this.cur순번To.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.cur순번To.Text, "0");
                    return;
                }

                if (this.cur순번From.DecimalValue > this.cur순번To.DecimalValue)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.cur순번To.Text, this.cur순번From.Text);
                    return;
                }

                DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_MNG_AUTO_INSP_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       this._flexL["NO_WO"].ToString(),
                                                                                       this._flexL["NO_LINE"].ToString(),
                                                                                       this.cur순번From.DecimalValue,
                                                                                       this.cur순번To.DecimalValue });

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn측정치삭제1.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn소재Heat번호적용_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                if (this.ShowMessage("소재 Heat 번호를 수정 하시겠습니까 ?\n즉시 반영되고, 재조회 후 확인 가능 합니다.", "QY2") != DialogResult.Yes)
                    return;

                query = @"UPDATE RD
SET RD.NO_HEAT = '{4}',
    RD.ID_UPDATE = '{5}',
	RD.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_PR_WO_REQ_D RD
WHERE RD.CD_COMPANY = '{0}'
AND RD.NO_WO = '{1}'
AND RD.SEQ_WO BETWEEN {2} AND {3}";

                DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                            this._flexH["NO_WO"].ToString(),
                                                            this.cur열처리번호From.DecimalValue,
                                                            this.cur열처리번호To.DecimalValue,
                                                            this.txt열처리번호.Text,
                                                            Global.MainFrame.LoginInfo.UserID));

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn소재Heat번호적용.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn열처리번호적용_Click(object sender, EventArgs e)
        {
            string query;

            try
            {
                if (!this._flexL.HasNormalRow) return;
                if (this._flexL["CD_WC"].ToString() != "W510")
				{
                    this.ShowMessage("열처리 공정이 선택되어 있지 않습니다.");
                    return;
				}

                if (this.ShowMessage("열처리번호를 수정 하시겠습니까 ?\n즉시 반영되고, 재조회 후 확인 가능 합니다.", "QY2") != DialogResult.Yes)
                    return;

                query = @"UPDATE WI
SET WI.NO_HEAT = '{5}',
	ID_UPDATE = '{6}',
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_PR_WO_INSP WI
WHERE WI.CD_COMPANY = '{0}'
AND WI.NO_WO = '{1}'
AND WI.NO_LINE = {2}
AND WI.SEQ_WO BETWEEN {3} AND {4}";

                DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                           this._flexL["NO_WO"].ToString(),
                                                                           D.GetDecimal(this._flexL["NO_LINE"]),
                                                                           this.cur열처리번호From.DecimalValue,
                                                                           this.cur열처리번호To.DecimalValue,
                                                                           this.txt열처리번호.Text,
                                                                           Global.MainFrame.LoginInfo.UserID }));

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn열처리번호적용.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn측정치삭제_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                if (!this._flexDL.HasNormalRow) return;
                if (Global.MainFrame.ShowMessage("선택된 측정치 데이터를 삭제 하시겠습니까?\n삭제된 데이터는 복구 할 수 없습니다.", "QY2") != DialogResult.Yes)
                    return;

                dataRowArray = this._flexDL.DataTable.Select("S = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
				{
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                    return;
				}
                else
				{
                    foreach (DataRow dr in dataRowArray)
					{
                        dr.Delete();
					}
				}
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexDH_AfterEdit(object sender, RowColEventArgs e)
        {
            FlexGrid flexGrid;
            string query;

            try
            {
                flexGrid = ((FlexGrid)sender);

                if (flexGrid.HasNormalRow == false) return;
                if (flexGrid.Cols[e.Col].Name != "NO_ID_OLD" && 
                    flexGrid.Cols[e.Col].Name != "NO_HEAT") return;

                query = @"UPDATE RD
SET RD.NO_ID_OLD = '{3}',
    RD.NO_HEAT = '{4}',
    RD.ID_UPDATE = '{5}',
	RD.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_PR_WO_REQ_D RD
WHERE RD.CD_COMPANY = '{0}'
AND RD.NO_WO = '{1}'
AND RD.SEQ_WO = {2}";

                DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
                                                            flexGrid["NO_WO"].ToString(),
                                                            D.GetDecimal(flexGrid["SEQ_WO"]),
                                                            flexGrid["NO_ID_OLD"].ToString(),
                                                            flexGrid["NO_HEAT"].ToString(),
                                                            Global.MainFrame.LoginInfo.UserID));
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn자동입력_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;
                
                if (this.cur순번From.DecimalValue <= 0)
				{
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.cur순번From.Text, "0");
                    return;
                }

                if (this.cur순번To.DecimalValue <= 0)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.cur순번To.Text, "0");
                    return;
                }

                if (this.cur순번From.DecimalValue > this.cur순번To.DecimalValue)
                {
                    this.ShowMessage(공통메세지._은_보다커야합니다, this.cur순번To.Text, this.cur순번From.Text);
                    return;
                }

                DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_MNG_AUTO_INSP", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     this._flexL["NO_WO"].ToString(),
                                                                                     this._flexL["NO_LINE"].ToString(),
                                                                                     this.cur순번From.DecimalValue,
                                                                                     this.cur순번To.DecimalValue });

                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn임의입력.Text);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Page_DataChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToolBarAddButtonEnabled = false;
            }
            catch (Exception ex)
            {
                MsgControl.CloseMsg();
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeSearch()) return;

                string str = "";
                if (this.chk계획.Checked)
                    str += "P";
                if (this.chk확정.Checked)
                    str += "O";
                if (this.chk발행.Checked)
                    str += "R";
                if (this.chk시작.Checked)
                    str += "S";
                if (this.chk마감.Checked)
                    str += "C";
                
                DataSet dataSet = this._biz.Search(new object[] { this.LoginInfo.CompanyCode,
                                                                  this.cbo공장.SelectedValue.ToString(),
                                                                  this.periodPicker1.StartDateToString,
                                                                  this.periodPicker1.EndDateToString,
                                                                  this.cbo지시구분.SelectedValue.ToString(),
                                                                  str,
                                                                  this.ctx프로젝트.CodeValue,
                                                                  this.bpc오더형태.QueryWhereIn_Pipe,
                                                                  this.bpc제품군.QueryWhereIn_Pipe,
                                                                  this.cbo경로유형.SelectedValue.ToString(),
                                                                  this.ctx품목군.CodeValue,
                                                                  this.bpc작업장.QueryWhereIn_Pipe,
                                                                  this.bpc공정.QueryWhereIn_Pipe,
                                                                  this.ctx작업지시번호From.CodeValue,
                                                                  this.ctx작업지시번호To.CodeValue,
                                                                  this.ctxSFT.CodeValue,
                                                                  this.ctx.CodeValue,
                                                                  this.ctx품목From.CodeValue,
                                                                  this.ctx품목To.CodeValue,
                                                                  Global.SystemLanguage.MultiLanguageLpoint });

                this._flexH.Binding = dataSet.Tables[0];
                this._flexL.Binding = dataSet.Tables[1];

                if (this._flexH.HasNormalRow)
                {
					this._flexL.DataView.RowFilter = "NO_WO = '" + this._flexH["NO_WO"].ToString() + "'";

					this.ToolBarPrintButtonEnabled = true;
                }
                else
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
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
                if (!this._flexH.HasNormalRow || !this.BeforePrint() || !this.MsgAndSave(PageActionMode.Print))
                    return;

                ReportHelper rptHelper = new ReportHelper("R_PR_WO_MNG_0", "작업지시관리");

                rptHelper.Print();
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.BeforeDelete() || !this._flexH.HasNormalRow)
                    return;

                DataRow[] dataRowArray = this._flexH.DataTable.Select("CHK = 'Y'", "", DataViewRowState.CurrentRows);
                
                if (dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (this.ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes) return;

                    DataTable dt = this._flexH.DataTable.Clone();
                    
                    foreach (DataRow dataRow in dataRowArray)
                    {
                        if (D.GetString(dataRow["FG_AUTO"]) != "005" && 
                            (D.GetString(dataRow["ST_WO"]) == "R" || D.GetString(dataRow["ST_WO"]) == "S" || D.GetString(dataRow["ST_WO"]) == "C"))
                        {
                            this.ShowMessage("작업지시상태가 RELEASE 이후인 건이 포함되어 있습니다.", "EK1");
                            return;
                        }

                        if (D.GetString(dataRow["FG_AUTO"]) == "005" && 
                            (D.GetString(dataRow["ST_WO"]) == "S" || D.GetString(dataRow["ST_WO"]) == "C"))
                        {
                            this.ShowMessage("작업지시상태가 시작 이후인 건이 포함되어 있습니다.", "EK1");
                            return;
                        }

                        if (dataRow["YN_INPUT"].ToString() == "Y")
						{
                            this.ShowMessage("생산투입된 건이 선택되어 있습니다.", "EK1");
                            return;
                        }

                        dt.Rows.Add(dataRow.ItemArray);
                    }

                    if (this._biz.delete(dt))
                    {
                        foreach (DataRow dataRow in dataRowArray)
                        {
                            dataRow.Delete();
                            dataRow.AcceptChanges();
                        }

                        this._flexH.Refresh();
                        this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                    }
                    else
                    {
                        this.ShowMessage(공통메세지.자료저장중오류가발생하였습니다);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool BeforeSearch()
        {
            if (!base.BeforeSearch()) return false;

            if (!this.공장선택여부)
            {
                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                this.cbo공장.Focus();
                return false;
            }

            return Checker.IsValid(this.periodPicker1, true, this.lbl작업기간.Text);
        }

        public override void OnToolBarSaveButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!this.MsgAndSave(PageActionMode.Save)) return;

                this.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
                this.OnToolBarSearchButtonClicked(null, null);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!this.Verify() || !this._flexH.HasNormalRow) 
                return false;

            DataTable changes1 = this._flexH.GetChanges();
            DataTable changes2 = this._flexL.GetChanges();
            DataTable changes3 = this._flexDL.GetChanges();

            if (changes1 == null && changes2 == null && changes3 == null)
                return false;

            if (changes1 != null)
            {
                DataRow[] dataRowArray1 = changes1.Select("ST_WO = 'C' AND ISNULL(DT_CLOSE,'') = ''");

                if (dataRowArray1.Length > 0)
                {
                    this.ShowMessage("마감된 데이터중 마감일자가 누락된 데이터가 존재합니다.[" + D.GetString(dataRowArray1[0]["NO_WO"]) + "]");
                    int row = this._flexH.FindRow(D.GetString(dataRowArray1[0]["NO_WO"]), this._flexH.Rows.Fixed, this._flexH.Cols["NO_WO"].Index, false, true, true);

                    if (row >= 0)
                    {
                        this._flexH.Focus();
                        this._flexH.Select(row, this._flexH.Cols["DT_CLOSE"].Index);
                    }
                    
                    return false;
                }
                
                DataRow[] dataRowArray2 = changes1.Select("ST_WO <> 'C' AND ISNULL(DT_CLOSE,'') <> ''");

                if (dataRowArray2.Length > 0)
                {
                    this.ShowMessage("마감되지 않은 데이터중 마감일자가 존재하는 데이터가 존재합니다.[" + D.GetString(dataRowArray2[0]["NO_WO"]) + "]");
                    int row = this._flexH.FindRow(D.GetString(dataRowArray2[0]["NO_WO"]), this._flexH.Rows.Fixed, this._flexH.Cols["NO_WO"].Index, false, true, true);

                    if (row >= 0)
                    {
                        this._flexH.Focus();
                        this._flexH.Select(row, this._flexH.Cols["DT_CLOSE"].Index);
                    }
                    
                    return false;
                }
            }

            if (!this._biz.Save(changes1, changes2, changes3)) return false;

            this._flexH.AcceptChanges();
            this._flexL.AcceptChanges();
            this._flexDL.AcceptChanges();
            
            return true;
        }

        private void btn작업지시마감_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexH.DataTable.Select("CHK = 'Y'");

                    if (dataRowArray1 == null || dataRowArray1.Length <= 0)
                    {
                        this.ShowMessage("선택된 작업지시가 없습니다.");
                    }
                    else
                    {
                        DataRow[] dataRowArray2 = this._flexH.DataTable.Select("CHK = 'Y' AND ST_WO = 'C'");
                        
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            this.ShowMessage("마감처리된 작업지시건이 있습니다.");
                        }
                        else if (this.IsChanged())
                        {
                            this.ShowMessage("저장되지 않은 내용이 있습니다.", "EK1");
                        }
                        else
                        {
                            DataRow[] dataRowArray3 = this._flexH.DataTable.Select("CHK = 'Y' AND ST_WO <> 'C'");
                            DataTable dtClose = this._flexH.DataTable.Clone();

                            for (int index = 0; index < dataRowArray3.Length; ++index)
                            {
                                DataRow row = dtClose.NewRow();
                                row.ItemArray = dataRowArray3[index].ItemArray;
                                row["DT_CLOSE"] = !(row["DT_CLOSE"].ToString().Trim() == "") ? row["DT_CLOSE"].ToString().Trim().Replace("/", "") : this.MainFrameInterface.GetStringToday;
                                dtClose.Rows.Add(row);
                            }

                            if (this._biz.WO_CLOSE(dtClose).Result)
                            {
                                this.OnToolBarSearchButtonClicked(sender, e);
                                this.ShowMessage("작업지시가 마감되었습니다.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn작업지시마감취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexH.DataTable.Select("CHK = 'Y'");
                    if (dataRowArray1 == null || dataRowArray1.Length <= 0)
                    {
                        this.ShowMessage("선택된 작업지시가 없습니다.");
                    }
                    else
                    {
                        DataRow[] dataRowArray2 = this._flexH.DataTable.Select("CHK = 'Y' AND ST_WO <> 'C'");
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            this.ShowMessage("마감처리되지 않은 작업지시건입니다.");
                        }
                        else
                        {
                            DataRow[] dataRowArray3 = this._flexH.DataTable.Select(" CHK = 'Y' AND ST_WO = 'C' ");
                            DataTable dtClose = this._flexH.DataTable.Clone();

                            for (int index = 0; index < dataRowArray3.Length; ++index)
                            {
                                DataRow row = dtClose.NewRow();
                                row.ItemArray = dataRowArray3[index].ItemArray;
                                dtClose.Rows.Add(row);
                            }

                            if (this._biz.WO_CLOSECANCEL(dtClose).Result)
                            {
                                this.OnToolBarSearchButtonClicked(sender, e);
                                this.ShowMessage("작업지시가 마감이 취소되었습니다.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn작업지시서출력_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    if (!this.BeforePrint() || !this.MsgAndSave(PageActionMode.Print))
                        return;
                    
                    ReportHelper reportHelper = new ReportHelper("R_PR_WO_REG_0", "작업지시서");
                    
                    reportHelper.SetData("작업지시번호", this._flexH["NO_WO"].ToString());
                    reportHelper.SetData("지시품목", this._flexH["CD_ITEM"].ToString());
                    reportHelper.SetData("규격", this._flexH["STND_ITEM"].ToString());
                    reportHelper.SetData("지시수량", this._flexH.CDecimal(this._flexH["QT_ITEM"]).ToString(this._flexH.Cols["QT_ITEM"].Format));
                    reportHelper.SetData("오더형태", this._flexH["NM_TP_ROUT"].ToString());
                    reportHelper.SetData("TRACKING_NO", this._flexH["NO_SO"].ToString());
                    reportHelper.SetData("TRACKING_NO_LINE", this._flexH["NO_LINE_SO"].ToString());
                    reportHelper.SetData("공장", this.cbo공장.Text.Replace("(", "").Replace(")", "").Replace(this.cbo공장.SelectedValue.ToString(), ""));
                    reportHelper.SetData("품명", this._flexH["NM_ITEM"].ToString());
                    reportHelper.SetData("단위", this._flexH["UNIT_IM"].ToString());
                    reportHelper.SetData("작업기간시작", this.periodPicker1.StartDateToString);
                    reportHelper.SetData("작업기간끝", this.periodPicker1.EndDateToString);
                    reportHelper.SetData("경로유형", this._flexH["NM_PATN_ROUT"].ToString());
                    reportHelper.SetData("LOT_NO", this._flexH["NO_LOT"].ToString());
                    reportHelper.SetData("품목군코드", this.ctx품목군.CodeValue);
                    reportHelper.SetData("품목군명", this.ctx품목군.CodeName);
                    reportHelper.SetDataTable(this._flexL.DataTable);

                    reportHelper.Print();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn소요자재정보_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (BasicInfo.ActiveDialog) return;

                    if (((Control)sender).Name == this.btn소요자재정보.Name)
					{
                        if (!this._flexH.HasNormalRow) return;
                        DataRow dataRow = this._flexH.GetDataRow(this._flexH.RowSel);
                        this._flexH[this._flexH.Row, "NO_WO"].ToString();
                        ((Form)this.LoadHelpWindow("P_PR_WO_BILL_SUB01", new object[] { this.MainFrameInterface,
                                                                                        dataRow,
                                                                                        this.periodPicker1.StartDateToString.Replace("/", "").ToString(),
                                                                                        this.periodPicker1.EndDateToString.Replace("/", "").ToString() })).ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn계획작업지시확정_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexH.DataTable.Select(" CHK = 'Y' ");
                    if (dataRowArray1 == null || dataRowArray1.Length <= 0)
                    {
                        this.ShowMessage("선택된 작업지시가 없습니다.");
                    }
                    else
                    {
                        DataRow[] dataRowArray2 = this._flexH.DataTable.Select(" CHK = 'Y' AND ST_WO <> 'P' ");
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            this.ShowMessage("계획상태('P')가 아닌 작업지시건이 있습니다.");
                        }
                        else
                        {
                            DataRow[] dataRowArray3 = this._flexH.DataTable.Select(" CHK = 'Y' AND FG_WO <> '002' ");
                            if (dataRowArray3 != null && dataRowArray3.Length > 0)
                            {
                                this.ShowMessage("작업지시구분이 MRP가 아닌 작업지시건이 있습니다.");
                            }
                            else
                            {
                                DataRow[] dataRowArray4 = this._flexH.DataTable.Select(" CHK = 'Y' AND ST_WO = 'P' ");
                                DataTable dtPlanConfirm = this._flexH.DataTable.Clone();

                                foreach (DataRow dataRow in dataRowArray4)
								{
                                    dtPlanConfirm.Rows.Add(dataRow.ItemArray);
                                }
                                
                                if (this._biz.WO_PLANCONFIRMCANCEL(dtPlanConfirm).Result)
                                {
                                    this.OnToolBarSearchButtonClicked(sender, e);
                                    this.ShowMessage("작업지시가 확정되었습니다.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn작업지시확정취소_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    DataRow[] dataRowArray1 = this._flexH.DataTable.Select("CHK = 'Y'");
                    if (dataRowArray1 == null || dataRowArray1.Length <= 0)
                    {
                        this.ShowMessage("선택된 작업지시가 없습니다.");
                    }
                    else
                    {
                        DataRow[] dataRowArray2 = this._flexH.DataTable.Select("CHK = 'Y' AND ST_WO <> 'O'");
                        if (dataRowArray2 != null && dataRowArray2.Length > 0)
                        {
                            this.ShowMessage("확정상태('O')가 아닌 작업지시건이 있습니다.");
                        }
                        else
                        {
                            DataRow[] dataRowArray3 = this._flexH.DataTable.Select("CHK = 'Y' AND FG_WO <> '002'");
                            if (dataRowArray3 != null && dataRowArray3.Length > 0)
                            {
                                this.ShowMessage("작업지시구분이 MRP가 아닌 작업지시건이 있습니다.");
                            }
                            else
                            {
                                DataRow[] dataRowArray4 = this._flexH.DataTable.Select("CHK = 'Y' AND ST_WO = 'O' AND FG_WO = '002'");
                                DataTable dtPlanConfirm = this._flexH.DataTable.Clone();

                                foreach (DataRow dataRow in dataRowArray4)
                                    dtPlanConfirm.Rows.Add(dataRow.ItemArray);
                                
                                if (this._biz.WO_PLANCONFIRMCANCEL(dtPlanConfirm).Result)
                                {
                                    this.OnToolBarSearchButtonClicked(sender, e);
                                    this.ShowMessage("작업지시가 확정취소되었습니다.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void btn작업지시삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexH.HasNormalRow) return;

                DataRow[] dataRowArray = this._flexH.DataTable.Select("CHK = 'Y'", string.Empty, DataViewRowState.CurrentRows);
                if (dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else
                {
                    if (this.ShowMessage("선택된 작업지시건에 관련된 내용이 모두 삭제됩니다. 계속하시겠습니까?", "EY2") != DialogResult.Yes)
                        return;

                    DataSet schemaDelete = this._biz.GetSchemaDelete();
                    DataTable table1 = schemaDelete.Tables[0];
                    DataTable table2 = schemaDelete.Tables[1];
                    DataTable table3 = schemaDelete.Tables[2];
                    DataTable table4 = schemaDelete.Tables[3];
                    
                    foreach (DataRow dataRow1 in dataRowArray)
                    {
                        string str = D.GetString(dataRow1["ST_WO"]);
                        if (str == "S" || str == "C")
                        {
                            DataRow dataRow2 = table1.NewRow();
                            dataRow2["CD_PLANT"] = D.GetString(dataRow1["CD_PLANT"]);
                            dataRow2["NO_WO"] = D.GetString(dataRow1["NO_WO"]);
                            table1.Rows.Add(dataRow2.ItemArray);
                        }

                        if (str == "R" || str == "S" || str == "C")
                        {
                            DataRow dataRow2 = table2.NewRow();
                            dataRow2["CD_PLANT"] = D.GetString(dataRow1["CD_PLANT"]);
                            dataRow2["NO_WO"] = D.GetString(dataRow1["NO_WO"]);
                            dataRow2["ST_WO_INSERT"] = "O";
                            dataRow2["DT_RELEASE_INSERT"] = "";
                            dataRow2["QT_ITEM"] = D.GetDecimal(dataRow1["QT_ITEM"]);
                            dataRow2["NO_LOT"] = D.GetString(dataRow1["NO_LOT"]);
                            dataRow2["DC_RMK"] = D.GetString(dataRow1["DC_RMK"]);
                            dataRow2["PATN_ROUT"] = D.GetString(dataRow1["PATN_ROUT"]);
                            table2.Rows.Add(dataRow2.ItemArray);
                        }

                        if (str == "O" || str == "R" || str == "S" || str == "C")
                        {
                            DataRow dataRow2 = table4.NewRow();
                            dataRow2["CD_PLANT"] = D.GetString(dataRow1["CD_PLANT"]);
                            dataRow2["NO_WO"] = D.GetString(dataRow1["NO_WO"]);
                            table4.Rows.Add(dataRow2.ItemArray);
                        }
                    }
                    if (this._biz.Delete_WO(table1, table2, table3, table4))
                    {
                        this.ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
                        this.OnToolBarSearchButtonClicked(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void Btn생산투입_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flexH.DataTable.Select("CHK = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flexH.DataTable.Select("CHK = 'Y' AND (FG_SERNO NOT IN ('998', '999') OR YN_INPUT = 'Y')").Length > 0)
                {
                    this.ShowMessage("생산투입 되었거나 시리얼번호 관리하지 않는 건이 선택되어 있습니다.");
                }
                else
                {
                    this.btn생산투입.Enabled = false;

                    foreach (DataRow dr in dataRowArray)
                    {
                        DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_MNG_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     dr["NO_WO"].ToString(),
                                                                                     dr["FG_SERNO"].ToString(),
                                                                                     Global.MainFrame.LoginInfo.UserID });
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn생산투입.Text);
                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
			{
                this.btn생산투입.Enabled = true;
            }
        }

        private void Btn생산투입취소_Click(object sender, EventArgs e)
        {
            DataRow[] dataRowArray;

            try
            {
                dataRowArray = this._flexH.DataTable.Select("CHK = 'Y'");

                if (dataRowArray == null || dataRowArray.Length == 0)
                {
                    this.ShowMessage(공통메세지.선택된자료가없습니다);
                }
                else if (this._flexH.DataTable.Select("CHK = 'Y' AND YN_INPUT = 'N'").Length > 0)
                {
                    this.ShowMessage("생산투입 되지 않은 건이 선택되어 있습니다.");
                }
                else
                {
                    this.btn생산투입취소.Enabled = false;

                    foreach (DataRow dr in dataRowArray)
                    {
                        DBHelper.ExecuteNonQuery("SP_CZ_PR_WO_MNG_D", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     dr["NO_WO"].ToString() });
                    }

                    this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn생산투입취소.Text);
                    this.OnToolBarSearchButtonClicked(null, null);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
            finally
			{
                this.btn생산투입취소.Enabled = true;
            }
        }

        private void _flexM_AfterRowChange(object sender, RangeEventArgs e)
        {
            try
            {
                if (!this._flexH.IsBindingEnd || !this._flexH.HasNormalRow)
                {
                    this._flexL.EmptyRowFilter();
                }
                else
                {
                    if (e.OldRange.r1 != e.NewRange.r1)
					{
                        if (this._flexL.DataSource != null)
                            this._flexL.DataView.RowFilter = "NO_WO = '" + this._flexH["NO_WO"].ToString() + "'";
                    }
                }
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
                FlexGrid flexGrid = (FlexGrid)sender;

                switch (flexGrid.Cols[e.Col].Name)
                {
                    case "CHK":
                    case "NO_HEAT":
                        break;
                    case "DT_CLOSE":
                        if (!(D.GetString(this._flexH[e.Row, "ST_WO"]) == "C"))
                            break;
                        e.Cancel = true;
                        break;
                    case "DC_RMK":
                    case "DC_RMK2":
                    case "CD_USERDEF1":
                    case "CD_USERDEF2":
                    case "CD_USERDEF3":
                    case "CD_USERDEF4":
                    case "CD_USERDEF5":
                    case "NUM_USERDEF1":
                    case "NUM_USERDEF2":
                        if (D.GetString(this._flexH[e.Row, "ST_WO"]) == "C")
                            e.Cancel = true;
                        break;
                    case "NO_LOT":
                    case "DT_LIMIT":
                        if (D.GetString(this._flexH[e.Row, "ST_WO"]) != "P" && D.GetString(this._flexH[e.Row, "ST_WO"]) != "O")
                        {
                            e.Cancel = true;
                            break;
                        }
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            try
            {
                if (!(this._flexH.GetData(e.Row, e.Col).ToString() != this._flexH.EditData))
                    return;

                switch (this._flexH.Cols[e.Col].Name)
                {
                    case "DT_CLOSE":
                        if (!ComFunc.DateCheck(this._flexH.EditData) && !string.IsNullOrEmpty(this._flexH.EditData))
                        {
                            this.ShowMessage(공통메세지.입력형식이올바르지않습니다);
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

        private void _flexL_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                switch (this._flexL.Cols[e.Col].Name)
                {
                    case "DC_RMK":
                    case "DC_RMK_1":
                        if (!(D.GetString(this._flexL[e.Row, "ST_OP"]) == "C"))
                            break;
                        e.Cancel = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flex_HelpClick(object sender, EventArgs e)
        {
            try
            {
                FlexGrid flexGrid = (FlexGrid)sender;
                if (flexGrid == null || !flexGrid.HasNormalRow)
                    return;

                switch (flexGrid.Cols[flexGrid.Col].Name)
                {
                    case "FILE_PATH_MNG":
                        string str = D.GetString(flexGrid["NO_WO"]);

                        if (new AttachmentManager("PR", "P_PR_WO_REG02", str).ShowDialog() == DialogResult.Cancel)
                            break;
                        
                        flexGrid["FILE_PATH_MNG"] = this._biz.SearchFileCount(str);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexL_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.UserHelpID == "H_PR_EQUIP_SUB")
                {
                    e.Parameter.P41_CD_FIELD1 = "설비 도움창";
                    e.Parameter.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                    e.Parameter.P65_CODE5 = D.GetString(this.cbo공장.Text);
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, filter;

            try
            {
                dt = null;
                key = this._flexH["NO_WO"].ToString();
                filter = "NO_WO = '" + key + "'";

                if (this._flexH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                               key });
                }

                this._flexDH.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void _flexDH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string filter;

            try
            {
                dt = null;
                filter = string.Format("NO_WO = '{0}' AND SEQ_WO = '{1}'", this._flexDH["NO_WO"].ToString(),
                                                                           this._flexDH["SEQ_WO"].ToString());

                if (this._flexDH.DetailQueryNeed == true)
                {
                    dt = this._biz.SearchDetail1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                this._flexDH["NO_WO"].ToString(),
                                                                this._flexDH["SEQ_WO"].ToString(),
                                                                (this.chk측정항목모두보기.Checked == true ? "Y" : "N") });
                }

                this._flexDL.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void UserDefinedSetting(FlexGrid _flex)
        {
            try
            {
                DataTable code1 = MA.GetCode("PR_0000098");
                if (code1.Rows.Count != 0)
                {
                    for (int index = 1; index <= 5; ++index)
                    {
                        if (code1.Rows.Count >= index)
                        {
                            DataTable code2 = MA.GetCode("PR_0000" + D.GetString((index + 98)).PadLeft(3, '0'), true);
                            if (code2.Rows.Count > 1 && !(D.GetString(code1.Rows[index - 1]["CD_FLAG2"]) == "N"))
                            {
                                string colName = "CD_USERDEF" + D.GetString(index);
                                string colCaptionDD = D.GetString(code1.Rows[index - 1]["NAME"]);
                                _flex.SetCol(colName, colCaptionDD, 100, true);
                                _flex.SetDataMap(colName, code2.Copy(), "CODE", "NAME");
                            }
                        }
                        else
                            break;
                    }
                }

                DataTable code3 = MA.GetCode("PR_0000104");
                for (int index = 0; index < code3.Rows.Count; ++index)
                {
                    if (!(D.GetString(code3.Rows[index]["CD_FLAG2"]) == "N"))
                    {
                        string colName = "NUM_USERDEF" + D.GetString((index + 1));
                        string colCaptionDD = D.GetString(code3.Rows[index]["NAME"]);
                        _flex.SetCol(colName, colCaptionDD, 100, true, typeof(decimal));
                    }
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryBefore(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_USER:
                        if (!(sender is BpCodeTextBox bpCodeTextBox))
                        {
                            e.QueryCancel = true;
                            break;
                        }

                        if (bpCodeTextBox.UserHelpID == "H_PR_SFT_SUB")
                        {
                            if (!this.공장선택여부)
                            {
                                this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl공장.Text);
                                this.cbo공장.Focus();
                                e.QueryCancel = true;
                                break;
                            }

                            e.HelpParam.UserHelpID = "H_PR_SFT_SUB";
                            e.HelpParam.P41_CD_FIELD1 = "Y";
                            e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                            e.HelpParam.P65_CODE5 = D.GetString(this.cbo공장.Text);
                            break;
                        }
                        break;
                    case HelpID.P_MA_CODE_SUB1:
                        e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
                        break;
                    case HelpID.P_MA_PITEM_SUB:
                    case HelpID.P_PR_WO_REG_SUB:
                        e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        break;
                    case HelpID.P_MA_WC_SUB1:
                        e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        e.HelpParam.P07_NO_EMP = Global.MainFrame.LoginInfo.EmployeeNo;
                        break;
                    case HelpID.P_PR_WCOP_SUB1:
                        if (D.GetString(this.bpc작업장.SelectedValue) == string.Empty)
                        {
                            Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl작업장.Text);
                            e.QueryCancel = true;
                            break;
                        }

                        e.HelpParam.P09_CD_PLANT = D.GetString(this.cbo공장.SelectedValue);
                        e.HelpParam.P20_CD_WC = this.bpc작업장.QueryWhereIn_Pipe;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void OnBpControl_QueryAfter(object sender, BpQueryArgs e)
        {
            try
            {
                switch (e.HelpID)
                {
                    case HelpID.P_MA_PITEM_SUB:
                        if (!string.IsNullOrEmpty(this.ctx품목To.CodeValue))
                            break;
                        this.ctx품목To.CodeValue = e.CodeValue;
                        this.ctx품목To.CodeName = e.CodeName;
                        break;
                    case HelpID.P_PR_WO_REG_SUB:
                        if (!string.IsNullOrEmpty(this.ctx작업지시번호To.CodeValue))
                            break;
                        this.ctx작업지시번호To.CodeValue = e.CodeValue;
                        this.ctx작업지시번호To.CodeName = e.CodeName;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void cbo공장_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cbo공장.Items.Count == 0 || this.cbo공장.SelectedValue == null)
                    return;

                this.cbo경로유형.DataSource = this._biz.Get전체경로유형(D.GetString(this.cbo공장.SelectedValue));
                this.cbo경로유형.ValueMember = "CODE";
                this.cbo경로유형.DisplayMember = "NAME";
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        public bool 공장선택여부
        {
            get
			{
                if (string.IsNullOrEmpty(this.cbo공장.SelectedValue.ToString()))
                    return false;
                else
                    return true;
            }
        }
    }
}
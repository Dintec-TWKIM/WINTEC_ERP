using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
    public partial class P_CZ_SA_GIR_DAILY_PLAN : PageBase
    {
        P_CZ_SA_GIR_DAILY_PLAN_BIZ _biz = new P_CZ_SA_GIR_DAILY_PLAN_BIZ();

        #region 초기화
        public P_CZ_SA_GIR_DAILY_PLAN()
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
            this.MainGrids = new FlexGrid[] { this._flex업무계획, this._flex본선선적H, this._flex전달수령, this._flex인원현황 };
            this._flex본선선적H.DetailGrids = new FlexGrid[] { this._flex본선선적L };

            #region 업무계획
            this._flex업무계획.BeginSetting(1, 1, false);

            this._flex업무계획.SetCol("TP_PLAN", "작업유형", 100);
            this._flex업무계획.SetCol("SEQ_PLAN", "순번", 50, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex업무계획.SetCol("DT_PLAN", "작업일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex업무계획.SetCol("NO_EMP", "작업자코드", 100, true);
            this._flex업무계획.SetCol("NM_EMP", "작업자명", 100);
            this._flex업무계획.SetCol("NM_CC", "팀", 100);
            this._flex업무계획.SetCol("NO_GIR", "협조전번호", 100);
            this._flex업무계획.SetCol("DC_RMK", "비고", 100, true);

            this._flex업무계획.SetCodeHelpCol("NO_EMP", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "NM_EMP" }, new string[] { "NO_EMP", "NM_KOR" });
            this._flex업무계획.SetDataMap("TP_PLAN", MA.GetCode("CZ_SA00046", false), "CODE", "NAME");

            this._flex업무계획.VerifyNotNull = new string[] { "DT_PLAN", "NO_EMP" }; 

            this._flex업무계획.SettingVersion = "0.0.0.1";
            this._flex업무계획.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 본선선적

            #region Header
            this._flex본선선적H.BeginSetting(1, 1, false);

            this._flex본선선적H.SetCol("STA_GIR", "상태", 100);
            this._flex본선선적H.SetCol("NM_SUB_CATEGORY", "구분", 100);
            this._flex본선선적H.SetCol("NM_VESSEL", "호선명", 100);
            this._flex본선선적H.SetCol("CALL_SIGN", "호출부호", 100);
            this._flex본선선적H.SetCol("NM_COMPANY", "회사명", 100);
            this._flex본선선적H.SetCol("NO_GIR", "협조전번호", 100);
            this._flex본선선적H.SetCol("NO_SO_PRE", "파일구분", 100);
            this._flex본선선적H.SetCol("DC_RESULT_PACK", "포장정보", 100);
            this._flex본선선적H.SetCol("DT_PLAN", "작업일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex본선선적H.SetCol("NO_EMP", "작업자코드", 100, true);
            this._flex본선선적H.SetCol("NM_EMP", "작업자명", 100);
            this._flex본선선적H.SetCol("DC_RMK", "작업비고", 100, true);
            this._flex본선선적H.SetCol("DC_RMK_GIR", "상세요청", 100);
            this._flex본선선적H.SetCol("DC_RMK1", "수정/취소", 100);
            this._flex본선선적H.SetCol("DC_RMK3", "기포장정보", 100);

            this._flex본선선적H.SetCodeHelpCol("NO_EMP", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "NM_EMP" }, new string[] { "NO_EMP", "NM_KOR" });
            this._flex본선선적H.SetDataMap("STA_GIR", MA.GetCode("CZ_SA00030", false), "CODE", "NAME");

            this._flex본선선적H.SettingVersion = "0.0.0.1";
            this._flex본선선적H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region Line
            this._flex본선선적L.BeginSetting(1, 1, false);

            this._flex본선선적L.SetCol("NM_PRTAG", "항구청", 100);
            this._flex본선선적L.SetCol("NM_VSSL", "호선명", 100);
            this._flex본선선적L.SetCol("DTS_ETRYPT", "입항일시", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex본선선적L.Cols["DTS_ETRYPT"].Format = "####/##/##/##:##:##";
            this._flex본선선적L.SetCol("DTS_TKOFF", "출항일시", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex본선선적L.Cols["DTS_TKOFF"].Format = "####/##/##/##:##:##";
            this._flex본선선적L.SetCol("NM_SATMN_ENTRPS", "신고업체", 100);
            this._flex본선선적L.SetCol("NM_IBOBPRT", "내외항구분", 100);
            this._flex본선선적L.SetCol("NM_LAIDUPFCLTY", "계선시설명", 100);

            this._flex본선선적L.SettingVersion = "0.0.0.1";
            this._flex본선선적L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #endregion

            #region 전달수령
            this._flex전달수령.BeginSetting(1, 1, false);

            this._flex전달수령.SetCol("STA_GIR", "상태", 100);
            this._flex전달수령.SetCol("NM_COMPANY", "회사명", 100);
            this._flex전달수령.SetCol("NO_GIR", "협조전번호", 100);
            this._flex전달수령.SetCol("NO_SO_PRE", "파일구분", 100);
            this._flex전달수령.SetCol("DC_RESULT_PACK", "포장정보", 100);
            this._flex전달수령.SetCol("NM_VESSEL", "호선명", 100);
            this._flex전달수령.SetCol("NM_KOR", "담당자", 100);
            this._flex전달수령.SetCol("NM_MAIN_CATEGORY", "중분류", 100);
            this._flex전달수령.SetCol("NM_SUB_CATEGORY", "소분류", 100);
            this._flex전달수령.SetCol("LN_PARTNER", "업체명", 100);
            this._flex전달수령.SetCol("DC_ADS", "업체주소", 100);
            this._flex전달수령.SetCol("QT_ITEM", "종수", 100);
            this._flex전달수령.SetCol("DT_COMPLETE", "완료예정일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex전달수령.SetCol("DT_NAME", "요일", 100);
            this._flex전달수령.SetCol("DT_PLAN", "작업일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex전달수령.SetCol("NO_EMP", "작업자코드", 100, true);
            this._flex전달수령.SetCol("NM_EMP", "작업자명", 100);
            this._flex전달수령.SetCol("DC_RMK", "작업비고", 100, true);
            this._flex전달수령.SetCol("DC_RMK_GIR", "상세요청", 100);
            this._flex전달수령.SetCol("DC_RMK1", "수정/취소", 100);
            this._flex전달수령.SetCol("DC_RMK3", "기포장정보", 100);

            this._flex전달수령.SetCodeHelpCol("NO_EMP", HelpID.P_MA_EMP_SUB, ShowHelpEnum.Always, new string[] { "NO_EMP", "NM_EMP" }, new string[] { "NO_EMP", "NM_KOR" });
            this._flex전달수령.SetDataMap("STA_GIR", MA.GetCode("CZ_SA00030", false), "CODE", "NAME");

            this._flex전달수령.SettingVersion = "0.0.0.1";
            this._flex전달수령.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 인원현황
            this._flex인원현황.BeginSetting(1, 1, false);

            this._flex인원현황.SetCol("NO_EMP", "사원번호", 100);
            this._flex인원현황.SetCol("NM_KOR", "사원명", 100);
            this._flex인원현황.SetCol("DT_BIRTH", "생년월일", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex인원현황.SetCol("DT_ENTER", "입사일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex인원현황.SetCol("DC_ADDRESS_RES", "주민등록주소", 100);
            this._flex인원현황.SetCol("DC_ADDRESS_CUR", "현주소", 100);
            this._flex인원현황.SetCol("NM_CC", "팀", 100);
            this._flex인원현황.SetCol("NM_DUTY_RANK", "직급", 100);
            this._flex인원현황.SetCol("NO_TEL", "전화번호", 100);
            this._flex인원현황.SetCol("NO_TEL_EMER", "비상연락", 100);
            this._flex인원현황.SetCol("NM_WCODE", "근태", 100);

            this._flex인원현황.SettingVersion = "0.0.0.1";
            this._flex인원현황.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            this.btn자동등록.Click += new EventHandler(this.btn자동등록_Click);
            this._flex본선선적H.AfterRowChange += new RangeEventHandler(this._flex본선선적H_AfterRowChange);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp작업일자.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
            this.dtp작업일자.Text = this.MainFrameInterface.GetStringToday;
            this.dtp작업일자.ToDayDate = this.MainFrameInterface.GetDateTimeToday();
        }
        #endregion

        #region 그리드 이벤트
        private void _flex본선선적H_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string key, filter;

            try
            {
                key = this._flex본선선적H["CALL_SIGN"].ToString();
                filter = "CLSGN = '" + this._flex본선선적H["CALL_SIGN"].ToString() + "'";

                dt = null;

                if (this._flex본선선적H.DetailQueryNeed == true)
                {
                    dt = this._biz.Search4(new object[] { key });
                }

                this._flex본선선적L.BindingAdd(dt, filter);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 메인버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                if (this.tabControlExt1.SelectedIndex == 0)
                {
                    this._flex업무계획.Binding = this._biz.Search(new object[] { this.dtp작업일자.Text,
                                                                                 this.txt협조전번호.Text });

                    if (!this._flex업무계획.HasNormalRow)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    }
                }
                else if (this.tabControlExt1.SelectedIndex == 1)
                {
                    this._flex본선선적H.Binding = this._biz.Search1(new object[] { this.txt협조전번호.Text });

                    if (!this._flex본선선적H.HasNormalRow)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    }
                }
                else if (this.tabControlExt1.SelectedIndex == 2)
                {
                    this._flex전달수령.Binding = this._biz.Search2(new object[] { this.txt협조전번호.Text });

                    if (!this._flex전달수령.HasNormalRow)
                    {
                        Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                    }
                }
                else if (this.tabControlExt1.SelectedIndex == 3)
                {
                    this._flex인원현황.Binding = this._biz.Search3(new object[] { this.dtp작업일자.Text });

                    if (!this._flex인원현황.HasNormalRow)
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

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!BeforeAdd()) return;

                this._flex업무계획.Rows.Add();
                this._flex업무계획.Row = this._flex업무계획.Rows.Count - 1;

                this._flex업무계획["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                this._flex업무계획["TP_PLAN"] = "000";
                this._flex업무계획["SEQ_PLAN"] = this.SeqMax("000");

                this._flex업무계획.AddFinished();
                this._flex업무계획.Col = this._flex업무계획.Cols["SEQ_PLAN"].Index;
                this._flex업무계획.Focus();
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
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete()) return;

                this._flex업무계획.Rows.Remove(this._flex업무계획.Row);
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
            FlexGrid grid;

            try
            {
                if (!base.SaveData() || !base.Verify()) return false;

                if (this.tabControlExt1.SelectedIndex == this.tabControlExt1.TabPages.IndexOf(this.tpg업무계획))
                    grid = this._flex업무계획;
                else if (this.tabControlExt1.SelectedIndex == this.tabControlExt1.TabPages.IndexOf(this.tpg본선선적))
                {
                    foreach(DataRow dr in this._flex본선선적H.DataTable.Rows)
                    {
                        if (dr.RowState != DataRowState.Unchanged && D.GetInt(dr["SEQ_PLAN"]) == 0)
                            dr["SEQ_PLAN"] = this.SeqMax("001");
                    }

                    grid = this._flex본선선적H;
                }
                else if (this.tabControlExt1.SelectedIndex == this.tabControlExt1.TabPages.IndexOf(this.tpg전달수령))
                {
                    foreach (DataRow dr in this._flex전달수령.DataTable.Rows)
                    {
                        if (dr.RowState != DataRowState.Unchanged && D.GetInt(dr["SEQ_PLAN"]) == 0)
                            dr["SEQ_PLAN"] = this.SeqMax("002");
                    }

                    grid = this._flex전달수령;
                }
                else
                    return false;

                if (!this._biz.SaveData(grid.GetChanges())) return false;

                grid.AcceptChanges();

                return true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return false;
        }
        #endregion

        #region 컨트롤 이벤트
        private void btn자동등록_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = this._biz.Search3(new object[] { this.dtp작업일자.Text });

                foreach(DataRow dr in dt.Rows)
                {
                    this._flex업무계획.Rows.Add();
                    this._flex업무계획.Row = this._flex업무계획.Rows.Count - 1;

                    this._flex업무계획["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
                    this._flex업무계획["TP_PLAN"] = "000";
                    this._flex업무계획["SEQ_PLAN"] = this.SeqMax("000");
                    this._flex업무계획["DT_PLAN"] = this.dtp작업일자.Text;
                    this._flex업무계획["NO_EMP"] = dr["NO_EMP"].ToString();
                    this._flex업무계획["DC_RMK"] = dr["NM_WCODE"].ToString();

                    this._flex업무계획.AddFinished();
                    this._flex업무계획.Col = this._flex업무계획.Cols["SEQ_PLAN"].Index;
                    this._flex업무계획.Focus();
                }
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
        private Decimal SeqMax(string tpPlan)
        {
            Decimal num = 1;
            
            DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(SEQ_PLAN) AS SEQ_PLAN 
                                                          FROM CZ_SA_GIR_PLAN WITH(NOLOCK)  
                                                          WHERE TP_PLAN = '" + tpPlan + "'");

            if (dataTable != null && dataTable.Rows.Count != 0)
                num = (D.GetDecimal(dataTable.Rows[0]["SEQ_PLAN"]) + 1);

            if (num <= this._flex업무계획.GetMaxValue("SEQ_PLAN"))
                num = (this._flex업무계획.GetMaxValue("SEQ_PLAN") + 1);

            return num;
        }
        #endregion
    }
}

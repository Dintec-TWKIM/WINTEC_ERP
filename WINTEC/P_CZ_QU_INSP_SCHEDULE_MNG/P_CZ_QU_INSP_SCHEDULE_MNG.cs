using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.Controls;
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
    public partial class P_CZ_QU_INSP_SCHEDULE_MNG : PageBase
    {
        P_CZ_QU_INSP_SCHEDULE_MNG_BIZ _biz = new P_CZ_QU_INSP_SCHEDULE_MNG_BIZ();
        private string _수주번호;


        public P_CZ_QU_INSP_SCHEDULE_MNG()
        {
            if (Global.MainFrame.LoginInfo.CompanyCode != "W100")
                StartUp.Certify(this);

            InitializeComponent();
        }

        public P_CZ_QU_INSP_SCHEDULE_MNG(string 수주번호)
          : this()
        {
            this._수주번호 = 수주번호;
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this.MainGrids = new FlexGrid[] { this._flex검사일정 };

            #region 리스트
            this._flex검사일정.BeginSetting(1, 1, false);

            this._flex검사일정.SetCol("NO_SO", "수주번호", 100, true);
            this._flex검사일정.SetCol("NO_PO_PARTNER", "거래처번호", 100);
            this._flex검사일정.SetCol("DT_SO", "수주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex검사일정.SetCol("LN_PARTNER", "매출처", 100);
            this._flex검사일정.SetCol("NO_HULL", "호선번호", 100);
            this._flex검사일정.SetCol("NM_ENGINE", "엔진타입", 100);
            this._flex검사일정.SetCol("YN_INSP", "선급검사여부", 100, false, CheckTypeEnum.Y_N);
            this._flex검사일정.SetCol("NM_INSP1", "선급검사기관1", 100);
            this._flex검사일정.SetCol("NM_INSP2", "선급검사기관2", 100);
            this._flex검사일정.SetCol("CD_ITEM", "품목코드", 100);
            this._flex검사일정.SetCol("NM_ITEM", "품목명", 100);
            this._flex검사일정.SetCol("QT_SO", "수주수량", 100, typeof(decimal), FormatTpType.QUANTITY);
            this._flex검사일정.SetCol("TP_COMPANY", "검사기관", 100, true);
            this._flex검사일정.SetCol("DT_INSP", "검사일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex검사일정.SetCol("TP_INSP", "검사유형", 100, true);
            this._flex검사일정.SetCol("TP_CONTENTS", "검사내용", 100, true);
            this._flex검사일정.SetCol("TP_RESULT", "검사결과", 100, true);
            this._flex검사일정.SetCol("NO_CERT", "CERT NO", 100, true);
            this._flex검사일정.SetCol("DC_RMK", "비고", 100, true);

            this._flex검사일정.ExtendLastCol = true;

            this._flex검사일정.SetCodeHelpCol("NO_SO", "H_CZ_MA_CUSTOMIZE_SUB", ShowHelpEnum.Always, new string[] { "NO_SO", "NO_SO" }, new string[] { "NO_SO", "NO_SO" });

            this._flex검사일정.SetDataMap("TP_COMPANY", MA.GetCode("CZ_WIN0001", false), "CODE", "NAME");
            this._flex검사일정.SetDataMap("TP_INSP", MA.GetCode("CZ_WIN0002", false), "CODE", "NAME");
            this._flex검사일정.SetDataMap("TP_CONTENTS", MA.GetCode("CZ_WIN0007", false), "CODE", "NAME");
            this._flex검사일정.SetDataMap("TP_RESULT", MA.GetCode("CZ_WIN0008", false), "CODE", "NAME");

            this._flex검사일정.VerifyPrimaryKey = new string[] { "NO_SO", "TP_COMPANY", "DT_INSP" };

            this._flex검사일정.SettingVersion = "0.0.0.1";
            this._flex검사일정.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion

            #region 달력
            this._flex검사일정L.BeginSetting(1, 1, false);

            this._flex검사일정L.SetCol("NO_SO", "수주번호", 100);
            this._flex검사일정L.SetCol("TP_COMPANY", "검사기관", 100);
            this._flex검사일정L.SetCol("DT_INSP", "검사일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex검사일정L.SetCol("TP_INSP", "검사유형", 100);
            this._flex검사일정L.SetCol("TP_CONTENTS", "검사내용", 100);
            this._flex검사일정L.SetCol("TP_RESULT", "검사결과", 100);
            this._flex검사일정L.SetCol("DC_RMK", "비고", 100);

            this._flex검사일정L.ExtendLastCol = true;

            this._flex검사일정L.SetDataMap("TP_COMPANY", MA.GetCode("CZ_WIN0001", false), "CODE", "NAME");
            this._flex검사일정L.SetDataMap("TP_INSP", MA.GetCode("CZ_WIN0002", false), "CODE", "NAME");
            this._flex검사일정L.SetDataMap("TP_CONTENTS", MA.GetCode("CZ_WIN0007", false), "CODE", "NAME");
            this._flex검사일정L.SetDataMap("TP_RESULT", MA.GetCode("CZ_WIN0008", false), "CODE", "NAME");

            this._flex검사일정L.VerifyPrimaryKey = new string[] { "NO_SO", "TP_COMPANY", "DT_INSP" };

            this._flex검사일정L.SettingVersion = "0.0.0.1";
            this._flex검사일정L.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
            #endregion
        }

        private void InitEvent()
        {
            LabelExt[] labelArray = new LabelExt[] { this.lbl101,
                                                     this.lbl102,
                                                     this.lbl103,
                                                     this.lbl104,
                                                     this.lbl105,
                                                     this.lbl106,
                                                     this.lbl107,
                                                     this.lbl108,
                                                     this.lbl109,
                                                     this.lbl110,
                                                     this.lbl111,
                                                     this.lbl112,
                                                     this.lbl113,
                                                     this.lbl114,
                                                     this.lbl115,
                                                     this.lbl116,
                                                     this.lbl117,
                                                     this.lbl118,
                                                     this.lbl119,
                                                     this.lbl120,
                                                     this.lbl121,
                                                     this.lbl122,
                                                     this.lbl123,
                                                     this.lbl124,
                                                     this.lbl125,
                                                     this.lbl126,
                                                     this.lbl127,
                                                     this.lbl128,
                                                     this.lbl129,
                                                     this.lbl130,
                                                     this.lbl131,
                                                     this.lbl132,
                                                     this.lbl133,
                                                     this.lbl134,
                                                     this.lbl135,
                                                     this.lbl136,
                                                     this.lbl137,
                                                     this.lbl138,
                                                     this.lbl139,
                                                     this.lbl140,
                                                     this.lbl141,
                                                     this.lbl142 };
            foreach (LabelExt label in labelArray)
                label.Click += new EventHandler(this.label_Click);


			this._flex검사일정.BeforeCodeHelp += _flex검사일정_BeforeCodeHelp;
            this._flex검사일정.ValidateEdit += new ValidateEditEventHandler(this._flex검사일정_ValidateEdit);
        }

        private void _flex검사일정_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
        {
            try
            {
                if (e.Parameter.HelpID == HelpID.P_USER)
                {
                    FlexGrid grid = ((FlexGrid)sender);

                    e.Parameter.UserParams = "수주번호;H_CZ_MA_CUSTOMIZE_SUB";
                    e.Parameter.P11_ID_MENU = "H_SA_SO_SUB";
                    e.Parameter.P21_FG_MODULE = "N";
                    e.Parameter.P92_DETAIL_SEARCH_CODE = D.GetString(grid["NO_SO"]);
                    e.Parameter.MultiHelp = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp조회기간.StartDateToString = Global.MainFrame.GetDateTimeToday().AddMonths(-3).ToString("yyyyMMdd");
            this.dtp조회기간.EndDateToString = Global.MainFrame.GetStringToday;

            this.dtp검사년월.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH, FormatFgType.INSERT);
            this.dtp검사년월.Text = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp검사년월.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

            this.cbo조회기간.DataSource = MA.GetCodeUser(new string[] { "000", "001" }, new string[] { "수주일자", "검사일자" });
            this.cbo조회기간.ValueMember = "CODE";
            this.cbo조회기간.DisplayMember = "NAME";

            this.cbo검사기관.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_WIN0001");
            this.cbo검사기관.ValueMember = "CODE";
            this.cbo검사기관.DisplayMember = "NAME";

            this.cbo검사유형.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_WIN0002");
            this.cbo검사유형.ValueMember = "CODE";
            this.cbo검사유형.DisplayMember = "NAME";

            this.cbo검사내용.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_WIN0007");
            this.cbo검사내용.ValueMember = "CODE";
            this.cbo검사내용.DisplayMember = "NAME";

            this.cbo검사결과.DataSource = Global.MainFrame.GetComboDataCombine("S;CZ_WIN0008");
            this.cbo검사결과.ValueMember = "CODE";
            this.cbo검사결과.DisplayMember = "NAME";

            if (!string.IsNullOrEmpty(this._수주번호))
            {
                this.txt수주번호.Text = this._수주번호;
                this.OnToolBarSearchButtonClicked(null, null);
            }
        }

        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                if (this.tabControlExt1.SelectedTab == this.tpg리스트)
                {
                    this._flex검사일정.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                 this.cbo조회기간.SelectedValue,
                                                                                 this.dtp조회기간.StartDateToString,
                                                                                 this.dtp조회기간.EndDateToString,
                                                                                 this.cbo검사기관.SelectedValue,
                                                                                 this.cbo검사유형.SelectedValue,
                                                                                 this.cbo검사내용.SelectedValue,
                                                                                 this.cbo검사결과.SelectedValue,
                                                                                 this.txt수주번호.Text });

                    if (!this._flex검사일정.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    this._flex검사일정L.Binding = this._biz.SearchMonth(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                       this.dtp검사년월.Text });

                    if (!this._flex검사일정L.HasNormalRow)
                        this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);

                    string query = @"SELECT DATEDIFF(DAY, CONVERT(NVARCHAR(8), DATEADD(MONTH, -1, '{0}' + '01'), 112), '{0}' + '01') LAST_MONTH,
	                                        DATEDIFF(DAY, '{0}' + '01', CONVERT(NVARCHAR(8), DATEADD(MONTH, 1, '{0}' + '01'), 112)) TO_MONTH,
	                                        DATEPART(DW, '{0}' + '01') NEXT_MONTH";

                    DataTable dt = DBHelper.GetDataTable(string.Format(query, this.dtp검사년월.Text));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        this.CreateCalendar(D.GetInt(dt.Rows[0]["LAST_MONTH"]), D.GetInt(dt.Rows[0]["TO_MONTH"]), D.GetInt(dt.Rows[0]["NEXT_MONTH"]));
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarAddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarAddButtonClicked(sender, e);

                if (!this.BeforeAdd()) return;
                if (this.tabControlExt1.SelectedTab != this.tpg리스트) return;

                this._flex검사일정.Rows.Add();
                this._flex검사일정.Row = this._flex검사일정.Rows.Count - 1;

                this._flex검사일정["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;

                this._flex검사일정.AddFinished();
                this._flex검사일정.Col = this._flex검사일정.Cols["NO_SO"].Index;
                this._flex검사일정.Focus();
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarDeleteButtonClicked(sender, e);

                if (!this.BeforeDelete() || !this._flex검사일정.HasNormalRow) return;
                if (this.tabControlExt1.SelectedTab != this.tpg리스트) return;

                this._flex검사일정.Rows.Remove(this._flex검사일정.Row);
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

                if (this.MsgAndSave(PageActionMode.Save))
                    this.ShowMessage(PageResultMode.SaveGood);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        protected override bool SaveData()
        {
            if (!base.SaveData() || !base.Verify()) return false;
            if (this._flex검사일정.IsDataChanged == false) return false;

            if (!this._biz.Save(this._flex검사일정.GetChanges()))
                return false;

            this._flex검사일정.AcceptChanges();

            return true;
        }

        private void _flex검사일정_ValidateEdit(object sender, ValidateEditEventArgs e)
        {
            FlexGrid grid;
            string name;

            try
            {
                grid = sender as FlexGrid;
                if (grid == null) return;

                name = grid.Cols[e.Col].Name;

                if (name == "NO_SO")
                {
                    DataTable dt = DBHelper.GetDataTable(@"SELECT * 
                                                           FROM SA_SOH SH WITH(NOLOCK)
                                                           WHERE SH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                          "AND SH.NO_SO = '" + this._flex검사일정["NO_SO"].ToString() + "'");

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.ShowMessage("수주건이 존재하지 않습니다.");
                        this._flex검사일정["NO_SO"] = string.Empty;
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = sender as Control;
                string text = this.dtp검사년월.Text;
                string date = D.GetString(control.Tag);

                if (date == string.Empty || date.Substring(0, 6) != text)
                    return;

                this.SettingColor(date);
                this.SetFilter(date);
            }
            catch (Exception ex)
            {
                this.MsgEnd(ex);
            }
        }

        private void SettingColor(string date)
        {
            object[] objArray = new object[] { this.lbl101,
                                               this.lbl102,
                                               this.lbl103,
                                               this.lbl104,
                                               this.lbl105,
                                               this.lbl106,
                                               this.lbl107,
                                               this.lbl108,
                                               this.lbl109,
                                               this.lbl110,
                                               this.lbl111,
                                               this.lbl112,
                                               this.lbl113,
                                               this.lbl114,
                                               this.lbl115,
                                               this.lbl116,
                                               this.lbl117,
                                               this.lbl118,
                                               this.lbl119,
                                               this.lbl120,
                                               this.lbl121,
                                               this.lbl122,
                                               this.lbl123,
                                               this.lbl124,
                                               this.lbl125,
                                               this.lbl126,
                                               this.lbl127,
                                               this.lbl128,
                                               this.lbl129,
                                               this.lbl130,
                                               this.lbl131,
                                               this.lbl132,
                                               this.lbl133,
                                               this.lbl134,
                                               this.lbl135,
                                               this.lbl136,
                                               this.lbl137,
                                               this.lbl138,
                                               this.lbl139,
                                               this.lbl140,
                                               this.lbl141,
                                               this.lbl142 };

            for (int index = 1; index < 43; ++index)
            {
                Control control = objArray[index - 1] as Control;
                string str = D.GetString(control.Tag);
                control.BackColor = !(date == str) ? (!(this.MainFrameInterface.GetStringToday == str) ? Color.Transparent : Color.FromArgb(227, 241, 248)) : Color.FromArgb((int)byte.MaxValue, 250, 190);
            }
        }

        public void CreateCalendar(int premonth, int month, int day)
        {
            int num1 = day - 1;
            int num2 = day + 1;
            int[] numArray = new int[43];
            int num3 = 0;
            int num4 = 0;
            string date = string.Empty;
            Control[] objArray1 = new Control[] { this.lbl101,
                                                  this.lbl102,
                                                  this.lbl103,
                                                  this.lbl104,
                                                  this.lbl105,
                                                  this.lbl106,
                                                  this.lbl107,
                                                  this.lbl108,
                                                  this.lbl109,
                                                  this.lbl110,
                                                  this.lbl111,
                                                  this.lbl112,
                                                  this.lbl113,
                                                  this.lbl114,
                                                  this.lbl115,
                                                  this.lbl116,
                                                  this.lbl117,
                                                  this.lbl118,
                                                  this.lbl119,
                                                  this.lbl120,
                                                  this.lbl121,
                                                  this.lbl122,
                                                  this.lbl123,
                                                  this.lbl124,
                                                  this.lbl125,
                                                  this.lbl126,
                                                  this.lbl127,
                                                  this.lbl128,
                                                  this.lbl129,
                                                  this.lbl130,
                                                  this.lbl131,
                                                  this.lbl132,
                                                  this.lbl133,
                                                  this.lbl134,
                                                  this.lbl135,
                                                  this.lbl136,
                                                  this.lbl137,
                                                  this.lbl138,
                                                  this.lbl139,
                                                  this.lbl140,
                                                  this.lbl141,
                                                  this.lbl142 };
            Control[] objArray2 = new Control[] { this.lbl201,
                                                  this.lbl202,
                                                  this.lbl203,
                                                  this.lbl204,
                                                  this.lbl205,
                                                  this.lbl206,
                                                  this.lbl207,
                                                  this.lbl208,
                                                  this.lbl209,
                                                  this.lbl210,
                                                  this.lbl211,
                                                  this.lbl212,
                                                  this.lbl213,
                                                  this.lbl214,
                                                  this.lbl215,
                                                  this.lbl216,
                                                  this.lbl217,
                                                  this.lbl218,
                                                  this.lbl219,
                                                  this.lbl220,
                                                  this.lbl221,
                                                  this.lbl222,
                                                  this.lbl223,
                                                  this.lbl224,
                                                  this.lbl225,
                                                  this.lbl226,
                                                  this.lbl227,
                                                  this.lbl228,
                                                  this.lbl229,
                                                  this.lbl230,
                                                  this.lbl231,
                                                  this.lbl232,
                                                  this.lbl233,
                                                  this.lbl234,
                                                  this.lbl235,
                                                  this.lbl236,
                                                  this.lbl237,
                                                  this.lbl238,
                                                  this.lbl239,
                                                  this.lbl240,
                                                  this.lbl241,
                                                  this.lbl242 };

            string text = this.dtp검사년월.Text;
            int num5 = int.Parse(text.Substring(4, 2)) - 1;
            string str1 = num5 >= 10 ? num5.ToString() : "0" + num5.ToString();
            string str2 = text.Substring(4, 2);
            int num6 = int.Parse(text.Substring(4, 2)) + 1;
            string str3 = num6 >= 10 ? num6.ToString() : "0" + num6.ToString();

            for (int index = num1; index > 0; --index)
            {
                numArray[index] = premonth;
                --premonth;
                Control control1 = objArray1[index - 1];
                Control control2 = objArray2[index - 1];
                control1.ForeColor = Color.Black;
                control1.ForeColor = index != 1 ? Color.Gray : Color.FromArgb(240, 170, 170);
                control1.Tag = (text.Substring(0, 4) + str1);
                control2.Tag = (text.Substring(0, 4) + str1);
                control2.Text = string.Empty;
            }

            for (int index = day; index < 43; ++index)
            {
                Control control = objArray1[index - 1];

                if (index < month + day)
                {
                    ++num3;
                    numArray[index] = num3;
                    if (index != 8 && index != 15 && (index != 22 && index != 29) && index != 36)
                        control.ForeColor = Color.Black;
                    control.Tag = (text.Substring(0, 4) + str2);
                }
                else
                {
                    ++num4;
                    numArray[index] = num4;
                    int num7 = index == 29 ? 0 : (index != 36 ? 1 : 0);
                    control.ForeColor = num7 != 0 ? Color.Gray : Color.FromArgb(240, 170, 170);
                    control.Tag = (text.Substring(0, 4) + str3);
                }
            }

            for (int index = 1; index < 43; ++index)
            {
                Control control1 = objArray1[index - 1];
                Control control2 = objArray2[index - 1];
                control1.Text = numArray[index].ToString();
                control1.Tag = numArray[index] >= 10 ? (control1.Tag.ToString() + numArray[index].ToString()) : (control1.Tag.ToString() + "0" + numArray[index].ToString());

                if (Global.MainFrame.GetStringToday == control1.Tag.ToString())
                {
                    control1.BackColor = Color.FromArgb(227, 241, 248);
                    control1.Focus();
                    date = ((Control)objArray1[index - 1]).Tag.ToString();
                }
                else
                    control1.BackColor = Color.White;

                control2.Text = string.Empty;

                if (D.GetString(control1.Tag).Substring(0, 6) == this.dtp검사년월.Text)
                {
                    Decimal num7 = D.GetDecimal(this._flex검사일정L.DataTable.Compute("COUNT(DT_INSP)", "DT_INSP = '" + D.GetString(control1.Tag) + "'"));
                    control2.Text = string.Format("{0:#,###.###}", num7);
                }
            }

            if (date == string.Empty)
                date = this.dtp검사년월.Text + "01";

            this.SetFilter(date);
        }

        private void SetFilter(string date)
        {
            this._flex검사일정L.RowFilter = "DT_INSP = '" + date + "'";
        }
    }
}

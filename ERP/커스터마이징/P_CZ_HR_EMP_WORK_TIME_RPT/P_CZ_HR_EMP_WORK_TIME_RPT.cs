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
    public partial class P_CZ_HR_EMP_WORK_TIME_RPT : PageBase
    {
        #region 전역변수 & 초기화
        P_CZ_HR_EMP_WORK_TIME_RPT_BIZ _biz = new P_CZ_HR_EMP_WORK_TIME_RPT_BIZ();
        int _유휴시간;

        public P_CZ_HR_EMP_WORK_TIME_RPT()
        {
            StartUp.Certify(this);
            InitializeComponent();
        }

        protected override void InitLoad()
        {
            base.InitLoad();

            this.splitContainer1.Panel2Collapsed = true;

            this.InitGrid();
            this.InitEvent();
        }

        private void InitGrid()
        {
            this._flex사원별.DetailGrids = new FlexGrid[] { this._flex집중근무현황, this._flex일일근무현황 };

            #region 사원
            this._flex사원별.BeginSetting(2, 1, false);

            this._flex사원별.SetCol("YN_HOME", "재택여부", 60, false, CheckTypeEnum.Y_N);
            this._flex사원별.SetCol("NO_EMP", "사번", 80);
            this._flex사원별.SetCol("NM_KOR", "이름", 90);
            this._flex사원별.SetCol("NM_DEPT", "부서", 90);
            this._flex사원별.SetCol("NM_CC", "코스트센터", 90);
            this._flex사원별.SetCol("QT_MONTH_MINUTE", "분", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex사원별.SetCol("QT_MONTH_HOUR", "시간", 100);
            this._flex사원별.SetCol("QT_1WEEK_MINUTE", "분", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex사원별.SetCol("QT_1WEEK_HOUR", "시간", 100);
            this._flex사원별.SetCol("QT_2WEEK_MINUTE", "분", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex사원별.SetCol("QT_2WEEK_HOUR", "시간", 100);
            this._flex사원별.SetCol("QT_3WEEK_MINUTE", "분", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex사원별.SetCol("QT_3WEEK_HOUR", "시간", 100);
            this._flex사원별.SetCol("QT_4WEEK_MINUTE", "분", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex사원별.SetCol("QT_4WEEK_HOUR", "시간", 100);
            this._flex사원별.SetCol("QT_5WEEK_MINUTE", "분", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex사원별.SetCol("QT_5WEEK_HOUR", "시간", 100);

            this._flex사원별[0, this._flex사원별.Cols["QT_MONTH_MINUTE"].Index] = this.DD("월누적");
            this._flex사원별[0, this._flex사원별.Cols["QT_MONTH_HOUR"].Index] = this.DD("월누적");
            this._flex사원별[0, this._flex사원별.Cols["QT_1WEEK_MINUTE"].Index] = this.DD("1주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_1WEEK_HOUR"].Index] = this.DD("1주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_2WEEK_MINUTE"].Index] = this.DD("2주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_2WEEK_HOUR"].Index] = this.DD("2주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_3WEEK_MINUTE"].Index] = this.DD("3주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_3WEEK_HOUR"].Index] = this.DD("3주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_4WEEK_MINUTE"].Index] = this.DD("4주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_4WEEK_HOUR"].Index] = this.DD("4주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_5WEEK_MINUTE"].Index] = this.DD("5주차");
            this._flex사원별[0, this._flex사원별.Cols["QT_5WEEK_HOUR"].Index] = this.DD("5주차");

            this._flex사원별.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 주간근무현황
            this._flex주간근무현황.BeginSetting(1, 1, false);

            this._flex주간근무현황.SetCol("NM_WEEK", "주차", 100);
            this._flex주간근무현황.SetCol("QT_WORK_MINUTE", "적용시간(분)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex주간근무현황.SetCol("QT_WORK_HOUR", "적용시간", 80);

            this._flex주간근무현황.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 집중근무현황
            this._flex집중근무현황.BeginSetting(1, 1, false);

            this._flex집중근무현황.SetCol("DT_START", "시작시간", 100);
            this._flex집중근무현황.SetCol("DT_END", "종료시간", 100);
            this._flex집중근무현황.SetCol("QT_WORK_MINUTE", "적용시간(분)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex집중근무현황.SetCol("QT_WORK_HOUR", "적용시간", 80);

            this._flex집중근무현황.Cols["DT_START"].Format = "tt hh:mm:ss";
            this._flex집중근무현황.Cols["DT_END"].Format = "tt hh:mm:ss";

            this._flex집중근무현황.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
            #endregion

            #region 일일근무현황
            this._flex일일근무현황.BeginSetting(1, 1, false);

            this._flex일일근무현황.SetCol("DT_WORK", "근무일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex일일근무현황.SetCol("DT_TIME", "근무시간", 100);
            this._flex일일근무현황.SetCol("PAGE_NAME", "페이지명", 100);
            this._flex일일근무현황.SetCol("IP", "접속아이피", 90);
            this._flex일일근무현황.SetCol("NM_SYSDEF", "구분", 90);
            this._flex일일근무현황.SetCol("DT_WORK_MINUTE", "적용시간(분)", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex일일근무현황.SetCol("YN_WORK", "적용여부", 60, false, CheckTypeEnum.Y_N);

            this._flex일일근무현황.Cols["DT_TIME"].Format = "tt hh:mm:ss";

            this._flex일일근무현황.EndSetting(GridStyleEnum.Blue, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
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

            this._flex사원별.AfterRowChange += new RangeEventHandler(this._flex사원별_AfterRowChange);
            this.btn상세보기.Click += new EventHandler(this.btn상세보기_Click);
        }

        protected override void InitPaint()
        {
            base.InitPaint();

            this.dtp근무년월.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH, FormatFgType.INSERT);
            this.dtp근무년월.Text = this.MainFrameInterface.GetStringFirstDayInMonth;
            this.dtp근무년월.ToDayDate = this.MainFrameInterface.GetDateTimeToday();

            this._유휴시간 = (int)DBHelper.ExecuteScalar(@"SELECT CONVERT(INT, CD_FLAG1) AS CD_FLAG1 
                                                           FROM CZ_MA_CODEDTL WITH(NOLOCK)
                                                           WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                         @"AND CD_FIELD = 'CZ_HR00007'
                                                           AND CD_SYSDEF = '001'");

            if (Global.MainFrame.LoginInfo.GroupID != "ADMIN")
			{
                DataTable dt = DBHelper.GetDataTable(@"SELECT ME.CD_DUTY_RESP,
                                                          ME.NO_EMP,
                                                          ME.NM_KOR,
                                                          ME.CD_DEPT,
                                                          MD.NM_DEPT,
                                                          ME.CD_CC,
                                                          MC.NM_CC
                                                   FROM MA_EMP ME WITH(NOLOCK)
                                                   LEFT JOIN MA_DEPT MD WITH(NOLOCK) ON MD.CD_COMPANY = ME.CD_COMPANY AND MD.CD_DEPT = ME.CD_DEPT
                                                   LEFT JOIN MA_CC MC WITH(NOLOCK) ON MC.CD_COMPANY = ME.CD_COMPANY AND MC.CD_CC = ME.CD_CC
                                                   WHERE ME.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                 @"AND ME.NO_EMP = '" + Global.MainFrame.LoginInfo.EmployeeNo + "'");

                if (dt.Rows[0]["CD_DUTY_RESP"].ToString() == "200")
                {
                    this.ctx부서.CodeValue = dt.Rows[0]["CD_DEPT"].ToString();
                    this.ctx부서.CodeName = dt.Rows[0]["NM_DEPT"].ToString();
                    this.ctx부서.ReadOnly = ReadOnly.TotalReadOnly;
                    this.ctx코스트센터.CodeValue = string.Empty;
                    this.ctx코스트센터.CodeName = string.Empty;
                    this.ctx코스트센터.ReadOnly = ReadOnly.None;
                    this.ctx사원.CodeValue = string.Empty;
                    this.ctx사원.CodeName = string.Empty;
                    this.ctx사원.ReadOnly = ReadOnly.None;
                }
                else if (dt.Rows[0]["CD_DUTY_RESP"].ToString() == "400")
                {
                    this.ctx부서.CodeValue = dt.Rows[0]["CD_DEPT"].ToString();
                    this.ctx부서.CodeName = dt.Rows[0]["NM_DEPT"].ToString();
                    this.ctx부서.ReadOnly = ReadOnly.TotalReadOnly;
                    this.ctx코스트센터.CodeValue = dt.Rows[0]["CD_CC"].ToString();
                    this.ctx코스트센터.CodeName = dt.Rows[0]["NM_CC"].ToString();
                    this.ctx코스트센터.ReadOnly = ReadOnly.TotalReadOnly;
                    this.ctx사원.CodeValue = string.Empty;
                    this.ctx사원.CodeName = string.Empty;
                    this.ctx사원.ReadOnly = ReadOnly.None;
                }
            }
        }
        #endregion

        #region 메인 버튼 이벤트
        public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                base.OnToolBarSearchButtonClicked(sender, e);

                if (!this.BeforeSearch()) return;

                DataTable dataTable = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                      this.ctx부서.CodeValue,
                                                                      this.ctx코스트센터.CodeValue,
                                                                      this.ctx사원.CodeValue,
                                                                      (this.chk재택근무여부.Checked == true ? "Y" : "N"),
                                                                      this.dtp근무년월.Text,
                                                                      this._유휴시간 });

                this._flex사원별.Binding = dataTable;

                if (!this._flex사원별.HasNormalRow)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 그리드 이벤트
        private void _flex사원별_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataTable dt;
            string query;

            try
            {
                #region 리스트
                this._flex주간근무현황.Binding = this._biz.SearchWeek(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                     this.dtp근무년월.Text,
                                                                                     this._flex사원별["NO_EMP"].ToString(),
                                                                                     this._유휴시간 });

                this._flex일일근무현황.Binding = this._biz.SearchMonth(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                                      this.dtp근무년월.Text,
                                                                                      this._flex사원별["NO_EMP"].ToString(),
                                                                                      this._유휴시간 });
                #endregion

                #region 달력
                query = @"SELECT DATEDIFF(DAY, CONVERT(NVARCHAR(8), DATEADD(MONTH, -1, '{0}' + '01'), 112), '{0}' + '01') LAST_MONTH,
	                             DATEDIFF(DAY, '{0}' + '01', CONVERT(NVARCHAR(8), DATEADD(MONTH, 1, '{0}' + '01'), 112)) TO_MONTH,
	                             DATEPART(DW, '{0}' + '01') NEXT_MONTH";

                dt = DBHelper.GetDataTable(string.Format(query, this.dtp근무년월.Text));

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.CreateCalendar(D.GetInt(dt.Rows[0]["LAST_MONTH"]), D.GetInt(dt.Rows[0]["TO_MONTH"]), D.GetInt(dt.Rows[0]["NEXT_MONTH"]));
                }
                #endregion
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 컨트롤 이벤트
        private void label_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = sender as Control;
                string text = this.dtp근무년월.Text;
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

        private void btn상세보기_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.splitContainer1.Panel2Collapsed)
                {
                    this.splitContainer1.SplitterDistance = 618;
                    this.splitContainer1.Panel2Collapsed = false;
                }
                else
                    this.splitContainer1.Panel2Collapsed = true;
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
        #endregion

        #region 기타 메소드
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

            string text = this.dtp근무년월.Text;
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
                    control1.BackColor = Color.Transparent;

                control2.Text = string.Empty;

                if (D.GetString(control1.Tag).Substring(0, 6) == this.dtp근무년월.Text)
                {
                    int num7 = D.GetInt(this._flex일일근무현황.DataTable.Compute("SUM(DT_WORK_MINUTE)", "DT_WORK = '" + D.GetString(control1.Tag) + "'"));
                    if (num7 > 0)
                        control2.Text = (num7 / 60).ToString("00") + ":" + (num7 % 60).ToString("00");
                }
            }

            if (date == string.Empty)
                date = this.dtp근무년월.Text + "01";

            this.SetFilter(date);
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

        private void SetFilter(string date)
        {
            this._flex일일근무현황.RowFilter = "DT_WORK = '" + date + "'";

            this._flex집중근무현황.Binding = this._biz.SearchDay(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                                            this._flex사원별["NO_EMP"].ToString(),
                                                                            date,
                                                                            this._유휴시간 });
        }
        #endregion
    }
}

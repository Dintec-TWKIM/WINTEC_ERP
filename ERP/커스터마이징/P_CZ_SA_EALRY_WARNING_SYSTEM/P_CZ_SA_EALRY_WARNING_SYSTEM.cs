using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Dass.FlexGrid;
using C1.Win.C1FlexGrid;
using Duzon.ERPU;
using Dintec;
using System.Net.Mail;
using System.Net;
using Duzon.ERPU.Grant;
using Duzon.Erpiu.ComponentModel;
using ChartFX.WinForms;
using Duzon.Common.BpControls;

namespace cz
{
	public partial class P_CZ_SA_EALRY_WARNING_SYSTEM : PageBase
	{
		P_CZ_SA_EALRY_WARNING_SYSTEM_BIZ _biz = new P_CZ_SA_EALRY_WARNING_SYSTEM_BIZ();

		public P_CZ_SA_EALRY_WARNING_SYSTEM()
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
            this._flexH.DetailGrids = new FlexGrid[] { this._flex리스트 };

			#region Header
			this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("WN_LEVEL", "최종레벨", 100);
			this._flexH.SetCol("CD_PARTNER", "거래처코드", 100);
			this._flexH.SetCol("LN_PARTNER", "거래처명", 300);
			this._flexH.SetCol("DT_TODAY", "기준일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_PROCESS_MAX", "최근거래일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DT_REMAIN_MIN", "가장오래된미수금", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("AM_END", "만기금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("AM_REMAIN", "미수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flexH.SetCol("NM_CON", "휴폐업구분", false);
            this._flexH.SetCol("NM_PARTNER_GRP", "거래처그룹", false);
            this._flexH.SetCol("NM_TERMS_PAYMENT", "매출결재조건", false);
            this._flexH.SetCol("DT_RCP_PREARRANGED", "수금예정일", false);
            this._flexH.SetCol("USE_YN", "사용여부", false);
            this._flexH.SetCol("YN_USE_GIR", "협조전사용여부", false);
            this._flexH.SetCol("YN_EXCEPT", "제외여부", false);
			this._flexH.SetCol("YN_PAY", "지불조건예외", false);
			this._flexH.SetCol("YN_NEW", "신규여부", false);
            this._flexH.SetCol("WN_MSG1", "조건1", false);
            this._flexH.SetCol("WN_MSG2", "조건2", false);
            this._flexH.SetCol("WN_MSG3", "조건3", false);
            this._flexH.SetCol("WN_MSG4", "조건4", false);
            this._flexH.SetCol("WN_MSG5", "조건5", false);

			if (Global.MainFrame.LoginInfo.CompanyCode != "K200")
			{
				this._flexH.SetCol("WN_MSG6", "조건6", false);
				this._flexH.SetCol("WN_MSG7", "조건7", false);
			}
				

			this._flexH.SetCol("NM_EMAIL", "메일", false);

			this._flexH.SetOneGridBinding(null, new IUParentControl[] { this.pnl요약1, this.pnl요약2 });
            this._flexH.SetBindningCheckBox(this.chk사용여부, "Y", "N");
            this._flexH.SetBindningCheckBox(this.chk협조전사용여부, "Y", "N");
            this._flexH.SetBindningCheckBox(this.chk제외여부, "Y", "N");
            this._flexH.SetBindningCheckBox(this.chk신규여부, "Y", "N");
			this._flexH.SetBindningCheckBox(this.chk미수금메일상시발송, "Y", "N");
			this._flexH.SetBindningCheckBox(this.chk지불조건예외, "Y", "N");
			this._flexH.SetBindningCheckBox(this.chk수금예정허용일적용, "Y", "N");
			this._flexH.SetDataMap("WN_LEVEL", MA.GetCodeUser(new string[] { "0", "1", "2" }, new string[] { "정상", "주의요망", "사용불가" }), "CODE", "NAME");

			this._flexH.SettingVersion = "0.0.0.1";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion

			#region 메시지
			this._flex메시지.BeginSetting(1, 1, false);

			this._flex메시지.SetCol("DC_EWS3", "조건번호", 100);
			this._flex메시지.SetCol("DC_EWS", "메시지", 200);

			this._flex메시지.SettingVersion = "0.0.0.1";
			this._flex메시지.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);

			this._flex메시지.ExtendLastCol = true;
			#endregion

			#region 리스트
			this._flex리스트.BeginSetting(1, 1, false);

            this._flex리스트.SetCol("NO_IV", "계산서번호", 100);
			this._flex리스트.SetCol("NO_SO", "수주번호", 100);
			this._flex리스트.SetCol("DT_PROCESS", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex리스트.SetCol("DT_END", "만기일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex리스트.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flex리스트.SetCol("DT_END_DAY", "경과일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("DT_END_MONTH", "경과월수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("DT_RCP_DAY", "수금일수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("DT_RCP_MONTH", "수금월수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
            this._flex리스트.SetCol("NM_EXCH", "통화명", 100);
            this._flex리스트.SetCol("RT_EXCH", "환율", 100, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
            this._flex리스트.SetCol("AM_EX", "외화매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_BAN_EX", "외화수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_CLS", "매출금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_BAN", "수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_END", "만기금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_REMAIN", "미수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
            this._flex리스트.SetCol("AM_REMAIN_TOTAL", "총미수금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

            this._flex리스트.SettingVersion = "0.0.0.1";
            this._flex리스트.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

            this._flex리스트.SetExceptSumCol("DT_END_DAY", "DT_END_MONTH", "DT_RCP_DAY", "DT_RCP_MONTH");
			#endregion

			#region 이력
			this._flex이력.BeginSetting(1, 1, false);

			this._flex이력.SetCol("DT_EWS", "경보일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex이력.SetCol("NM_LEVEL", "경보레벨", 120);
			this._flex이력.SetCol("NM_RESULT1", "조건1", 120);
			this._flex이력.SetCol("NM_RESULT2", "조건2", 120);
			this._flex이력.SetCol("NM_RESULT3", "조건3", 120);
			this._flex이력.SetCol("NM_RESULT4", "조건4", 120);
			this._flex이력.SetCol("NM_RESULT5", "조건5", 120);

			if (Global.MainFrame.LoginInfo.CompanyCode != "K200")
			{
				this._flex이력.SetCol("NM_RESULT6", "조건6", 120);
				this._flex이력.SetCol("NM_RESULT7", "조건7", 120);
			}

			this._flex이력.SettingVersion = "0.0.0.1";
			this._flex이력.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			#endregion
		}

		private void InitEvent()
		{
			this.btn설정.Click += new EventHandler(this.btn설정_Click);
			this.btn메일발송.Click += new EventHandler(this.btn메일발송_Click);
			this.bpc최종레벨.QueryBefore += new BpQueryHandler(this.bpc최종레벨_QueryBefore);
			this.chk미수금메일상시발송.CheckedChanged += CheckBox_CheckedChanged;
			this.chk수금예정허용일적용.CheckedChanged += CheckBox_CheckedChanged;

			this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
		}

		protected override void InitPaint()
        {
            base.InitPaint();

            if (this.DisplayRectangle.Width >= 1024)
            {
                this.splitContainer1.SplitterDistance = 1024;
				this.splitContainer2.SplitterDistance = 606;
			}

            this.chart미수금.ChartFx.Gallery = Gallery.Lines;
            this.chart미수금.ChartFx.LegendBox.Visible = true;

            this.chart미수금.ChartFx.AxisY.LabelsFormat.Format = AxisFormat.Currency;

            this.chart미수금.ChartFx.AxisX.AutoScroll = true;

            this.chart미수금.ChartFx.Panes.Add(new Pane());
            this.chart미수금.ChartFx.Panes[1].AxisY.LabelsFormat.Format = AxisFormat.Currency;

            UGrant ugrant = new UGrant();
            if (ugrant.GrantButton(Global.MainFrame.CurrentPageID, "OPTION"))
            {
                this.btn설정.Visible = true;
                this.chk시뮬레이션.Visible = true;
				this.chk수금예정허용일적용.Enabled = true;
            }
            else
            {
                this.btn설정.Visible = false;
                this.chk시뮬레이션.Visible = false;
				this.chk수금예정허용일적용.Enabled = false;
			}

			if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
			{
				this.chk수금예정허용일적용.Visible = false;

				this.pnl요약2.ItemCollection.Remove(this.oneGridItem19);
				this.pnl요약2.Controls.Remove(this.oneGridItem19);

				this.pnl요약2.ItemCollection.Remove(this.oneGridItem20);
				this.pnl요약2.Controls.Remove(this.oneGridItem20);

				this.pnl요약2.ItemCollection.Remove(this.oneGridItem21);
				this.pnl요약2.Controls.Remove(this.oneGridItem21);

				this.pnl요약2.ItemCollection.Remove(this.oneGridItem22);
				this.pnl요약2.Controls.Remove(this.oneGridItem22);
			}
        }

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
            DataTable dt, dt1;
			string query;

			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				query = @"SELECT EC.DC_EWS3,
	   EC.DC_EWS
FROM CZ_SA_EWS_CONDITION EC WITH(NOLOCK)
WHERE EC.CD_COMPANY = '{0}'
AND EC.YN_SIMULATION = '{1}'
AND EC.TP_GUBUN = 'M'";

                if (this.chk시뮬레이션.Checked == true)
                {
                    dt = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														  this.bpc매출처.QueryWhereIn_Pipe,
                                                          (this.chk시뮬레이션.Checked == true ? "Y" : "N"),
														  this.bpc최종레벨.QueryWhereIn_Pipe,
														  this.bpc담당자.QueryWhereIn_Pipe,
														  (this.chk미수금업체만조회.Checked == true ? "Y" : "N") });

					dt1 = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					"Y" }));

                }
                else
                {
                    dt = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														 this.bpc매출처.QueryWhereIn_Pipe,
                                                         (this.chk시뮬레이션.Checked == true ? "Y" : "N"),
														 this.bpc최종레벨.QueryWhereIn_Pipe,
														 this.bpc담당자.QueryWhereIn_Pipe,
														 (this.chk미수금업체만조회.Checked == true ? "Y" : "N") });

					dt1 = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					"N" }));
				}

                this._flexH.Binding = dt;
				this._flex메시지.Binding = dt1;
                
				if (!this._flexH.HasNormalRow)
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn메일발송_Click(object sender, EventArgs e)
		{
			string 보내는사람, 받는사람, 숨은참조, 회사코드, 매출처코드, 매출처명, 제목, 내용, 기준일자, 최종레벨, 미수금, html;
			DataTable dt;

			try
			{
				if (this.ShowMessage("선택한 매출처에 미수금 메일 발송을 진행하시겠습니까 ?", "QY2") != DialogResult.Yes)
					return;

				회사코드 = Global.MainFrame.LoginInfo.CompanyCode;

				if (회사코드 == "K100")
				{
					보내는사람 = "service@dintec.co.kr/DINTEC CO., LTD.";
					숨은참조 = "sd@dintec.co.kr";
				}
				else if (회사코드 == "K200")
				{
					보내는사람 = "service@dubheco.com/DUBHECO CO., LTD.";
                    숨은참조 = "notice@dubheco.com";
				}
				else if (회사코드 == "S100")
				{
					보내는사람 = "service@dintec.com.sg/DINTEC SINGAPORE PTE. LTD.";
                    숨은참조 = "service@dintec.com.sg";
				}
				else
				{
					return;
				}

				매출처코드 = this._flexH["CD_PARTNER"].ToString();
				기준일자 = this._flexH["DT_TODAY"].ToString();
				최종레벨 = this._flexH["WN_LEVEL"].ToString();

				dt = DBHelper.GetDataTable(@"SELECT MP.LN_PARTNER,
													MP.CD_AREA,
													FP.NM_EMAIL,
													FP.YN_OUTSTANDING_INV 
											 FROM MA_PARTNER MP WITH(NOLOCK)
											 LEFT JOIN FI_PARTNERPTR FP WITH(NOLOCK) ON FP.CD_COMPANY = MP.CD_COMPANY AND FP.CD_PARTNER = MP.CD_PARTNER
											 WHERE MP.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
											"AND MP.CD_PARTNER = '" + 매출처코드 + "'");

				매출처명 = dt.Rows[0]["LN_PARTNER"].ToString().Replace("(사용불가)", "").Replace("(사용중지)", "");
				제목 = "Account Alert: Payment reminder for overdue invoice";

				if (dt.Rows[0]["CD_AREA"].ToString() == "200") //해외
				{
					if (회사코드 == "K100")
					{
						내용 = 메일문구(회사코드, 최종레벨, 매출처명, true);
						미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
						html = 내용 + 미수금;
					}
					else if (회사코드 == "K200")
					{
						내용 = 메일문구(회사코드, 최종레벨, 매출처명, true);
						미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
						html = 내용 + 미수금;
					}
					else if (회사코드 == "S100")
					{
						내용 = 메일문구(회사코드, 최종레벨, 매출처명, true);
						미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
						html = 내용 + 미수금;
					}
					else
					{
						return;
					}
				}
				else if (dt.Rows[0]["CD_AREA"].ToString() == "100") //국내
				{
					if (회사코드 == "K100")
					{
						내용 = 메일문구(회사코드, 최종레벨, 매출처명, true);
						미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
						html = 내용 + 미수금;
					}
					else if (회사코드 == "K200")
					{
						내용 = 메일문구(회사코드, 최종레벨, 매출처명, true);
						미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
						html = 내용 + 미수금;
					}
					else if (회사코드 == "S100")
					{
						내용 = 메일문구(회사코드, 최종레벨, 매출처명, true);
						미수금 = 미수금내역(회사코드, 매출처코드, 기준일자, true);
						html = 내용 + 미수금;
					}
					else
					{
						return;
					}
				}
				else
				{
					this.ShowMessage("해당 매출처에 지역구분이 지정되어 있지 않습니다.");
					return;
				}

				if (string.IsNullOrEmpty(미수금))
				{
					this.ShowMessage("해당 매출처에 미수금내역이 없습니다.");
					return;
				}

				받는사람 = string.Empty;

                foreach (DataRow dr in dt.Select("YN_OUTSTANDING_INV = 'Y'"))
                {
                    받는사람 = 받는사람 + dr["NM_EMAIL"].ToString() + ";";
                }

                if (string.IsNullOrEmpty(받는사람))
                {
                    this.ShowMessage("해당 매출처에 미수금 관리 담당자가 지정되어 있지 않습니다.");
                    return;
                }

                P_CZ_MA_EMAIL_SUB dialog = new P_CZ_MA_EMAIL_SUB(보내는사람, 받는사람, string.Empty, 숨은참조, 제목, null, null, string.Empty, html, string.Empty, string.Empty, false);
                dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn설정_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_SA_EALRY_WARNING_SYSTEM_SUB dialog = new P_CZ_SA_EALRY_WARNING_SYSTEM_SUB();
				dialog.Show();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void bpc최종레벨_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P00_CHILD_MODE = "최종레벨";
				e.HelpParam.P61_CODE1 = "CODE, NAME";
				e.HelpParam.P62_CODE2 = @"(VALUES ('0', '정상'), ('1', '주의요망'), ('2', '사용불가')) AS A (CODE, NAME)";
				e.HelpParam.P63_CODE3 = "";
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			string query;

			try
			{
				query = @"IF EXISTS (SELECT 1 
           FROM CZ_SA_EWS_PARTNER WITH(NOLOCK)
           WHERE CD_COMPANY = '{0}'
           AND CD_PARTNER = '{1}')
BEGIN
    UPDATE EP
    SET EP.YN_OUTSTANDING_MAIL = '{2}',
		EP.YN_CHECK_7 = '{3}',
        EP.ID_UPDATE = '{4}',
        EP.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
    FROM CZ_SA_EWS_PARTNER EP
    WHERE EP.CD_COMPANY = '{0}'
    AND EP.CD_PARTNER = '{1}'
END
ELSE
BEGIN
    INSERT INTO CZ_SA_EWS_PARTNER
    (
        CD_COMPANY,
        CD_PARTNER,
        YN_OUTSTANDING_MAIL,
		YN_CHECK_7,
        ID_INSERT,
        DTS_INSERT
    )
    VALUES
    (
        '{0}',
        '{1}',
        '{2}',
        '{3}',
		'{4}',
        NEOE.SF_SYSDATE(GETDATE())
    )
END";

				DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	       this._flexH["CD_PARTNER"].ToString(),
																		   (this.chk미수금메일상시발송.Checked == true ? "Y" : "N"),
																		   (this.chk수금예정허용일적용.Checked == true ? "Y" : "N"),
																		   Global.MainFrame.LoginInfo.UserID }));
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private string 미수금내역(string 회사코드, string 매출처코드, string 기준일자, bool 영문여부)
		{
			string html = string.Empty, color, query;
			DataTable dt;

			try
			{
                query = @"SELECT IH.NO_IV,
								 STUFF((SELECT DISTINCT ',' + IL.NO_SO
								 	    FROM SA_IVL IL WITH(NOLOCK)
								 	    WHERE IL.CD_COMPANY = IH.CD_COMPANY
								 	    AND IL.NO_IV = IH.NO_IV
								 	    FOR XML PATH('')),1,1,'') AS NO_SO,
								 STUFF((SELECT DISTINCT ',' + SH.NO_PO_PARTNER
								 	    FROM SA_IVL IL WITH(NOLOCK)
								 	    LEFT JOIN SA_SOH SH WITH(NOLOCK) ON SH.CD_COMPANY = IL.CD_COMPANY AND SH.NO_SO = IL.NO_SO
								 	    WHERE IL.CD_COMPANY = IH.CD_COMPANY
								 	    AND IL.NO_IV = IH.NO_IV
								 	    FOR XML PATH('')),1,1,'') AS NO_PO_PARTNER, 
								 STUFF((SELECT DISTINCT ',' + MH.NM_VESSEL
								 	    FROM SA_IVL IL WITH(NOLOCK)
								 	    LEFT JOIN SA_SOH SH WITH(NOLOCK) ON SH.CD_COMPANY = IL.CD_COMPANY AND SH.NO_SO = IL.NO_SO
								 	    LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = SH.NO_IMO
								 	    WHERE IL.CD_COMPANY = IH.CD_COMPANY
								 	    AND IL.NO_IV = IH.NO_IV
								 	    FOR XML PATH('')),1,1,'') AS NM_VESSEL,
								 IH.DT_PROCESS,
								 DATEDIFF(DAY, IH.DT_PROCESS, '" + 기준일자 + @"') AS DT_DIFF,
								 MC.NM_SYSDEF AS NM_EXCH,
								 (ISNULL(IH.AM_EX, 0) - ISNULL(IH.AM_BAN_EX, 0)) AS AM_EX_REMAIN,
								 (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) - ISNULL(IH.AM_BAN, 0) AS AM_REMAIN
					      FROM SA_IVH IH WITH(NOLOCK)
					      LEFT JOIN MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = IH.CD_COMPANY AND MC.CD_FIELD = 'MA_B000005' AND MC.CD_SYSDEF = IH.CD_EXCH
					      WHERE IH.CD_COMPANY = '" + 회사코드 + "'" + Environment.NewLine +
                         "AND IH.CD_PARTNER = '" + 매출처코드 + "'" + Environment.NewLine +
                         "AND IH.DT_PROCESS <= CONVERT(CHAR(8), DATEADD(DAY, -7, '" + 기준일자 + "'), 112)" + Environment.NewLine +
                        @"AND (ISNULL(IH.AM_K, 0) + ISNULL(IH.VAT_TAX, 0)) > ISNULL(IH.AM_BAN, 0)
				          ORDER BY IH.DT_PROCESS ASC";

                dt = DBHelper.GetDataTable(query);

				if (dt != null && dt.Rows.Count > 0)
				{
					if (영문여부)
					{
						html = @"<br/>
								 <div style='text-align:left; font-weight: bold;'>*** Outstanding Invoice List</div>

							 <table style='width:1000px; border:2px solid black; font-size: 9pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<tbody>
								<tr style='height:30px'>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Invoice Date</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Overdue Day</th>                                    
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Invoice No</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Our Ref</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Your PO</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Vessel Name</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Currency</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>Amount</th>
								</tr>";
					}
					else
					{
						html = @"<br/>
								 <div style='text-align:left; font-weight: bold;'>*** 미수금 내역</div>

							 <table style='width:1000px; border:2px solid black; font-size: 9pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<colgroup width='120px' align='center'></colgroup>
								<tbody>
								<tr style='height:30px'>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>발행일자</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>경과일수</th>                                    
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>계산서번호</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>수주번호</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>발주번호</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>호선명</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>통화</th>
									<th style='border:solid 1px black; text-align:center; background-color:Silver'>금액</th>
								</tr>";
					}

					foreach (DataRow dr in dt.Rows)
					{
						if (Convert.ToInt32(dr["DT_DIFF"]) >= 90)
							color = "color: #FF0000;";
						else if (Convert.ToInt32(dr["DT_DIFF"]) >= 45)
							color = "color: #0000FF;";
						else
							color = "color: #000000;";

						if (dr["NM_EXCH"].ToString() == "KRW")
						{
							html += @"<tr style='height:30px'>
										 <th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + Util.GetTo_DateStringS(dr["DT_PROCESS"]) + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["DT_DIFF"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_IV"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_SO"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_PO_PARTNER"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NM_VESSEL"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["NM_EXCH"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:bold'>" + Util.GetTO_Money(dr["AM_REMAIN"]) + "</th>" + Environment.NewLine +
									 "</tr>";
						}
						else
						{
							html += @"<tr style='height:30px'>
										 <th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + Util.GetTo_DateStringS(dr["DT_PROCESS"]) + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["DT_DIFF"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_IV"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_SO"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NO_PO_PARTNER"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:left; padding-left:10px; font-weight:normal'>" + dr["NM_VESSEL"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:center; font-weight:normal'>" + dr["NM_EXCH"].ToString() + "</th>" + Environment.NewLine +
										"<th style='border:solid 1px black; " + color + " text-align:right; padding-right:10px; font-weight:bold'>" + Util.GetTO_Money(dr["AM_EX_REMAIN"]) + "</th>" + Environment.NewLine +
									 "</tr>";
						}
					}

					html += @"</tbody>
					  </table>";
				}
				else
					html = string.Empty;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return html;
		}

		public string 메일문구(string 회사코드, string level, string 매출처명, bool 영문여부)
		{
			string 문구;

			try
			{
				if (level == "1") //주의요망
				{
					#region 주의요망
					if (영문여부)
					{
						if (회사코드 == "K100")
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
First of all, we would like to express the appreciation for the good cooperation and business with us.<br/><br/>
As most companies are already implementing Risk Management, we also have introduced an Early Warning System as a part of Risk Management.<br/><br/>
To provide the best service to our customers is what we are the most focusing on.<br/>
But, there may be a case where our service provision is restricted because of long-term outstanding invoices caused by missing invoices and the lack of management of invoices between each other.<br/>
<span style='font-size: 9pt; font-family: 맑은 고딕; color: #0000FF;'>
The main purpose of “Early Warning System” is in order to complement problems and prevent any restrictions on providing our good service.
</span><br/><br/>
Currently, your outstanding invoice is as followings. We would like you to settle overdue invoices as soon as possible. It will be appreciated to inform us the expected payment plan.<br/><br/>
Please also kindly note that our basic services such as quotation, order and delivery can be restricted, if outstanding invoices is not settled longer or increase.
</div>";
						}
						else
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
This is to remind you to settle your account balance with us immediately.<br/>
We are attaching our files on the following unsettled accounts and order your collection of same.<br/>
If the current outstanding amounts is maintained
<span style='text-decoration: underline; color: #0000FF'>
your inquiries and orders will be interrupted/delayed
</span> 
as a result of credit check by our CMS(Credit Management System)<br/>
Please kindly understand our situation and your prompt payment of these invoices will be greatly appreciated.
</div>";
						}
					}
					else
					{
						문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
수신 : " + 매출처명 + @" 담당자 님<br/><br/>
귀사의 일익 번창을 기원합니다.<br/><br/>
아래의 세금계산서(인보이스)에 대하여 물품대금 지급일이 이미 경과하였음에도 불구하고 미수금 상환이 이루어지지 않고 있으며,<br/><br/>
당사의 위험관리 일환인 조기경보시스템에 따라 아래와 같이 미수금 리스트를 송부 드립니다.<br/><br/>
미수채권의 지급이 이행되지 않을 경우 부득이 귀사를 대상으로 견적, 수주 및 물품공급 등의 서비스에 제한이 될 수 있사오니<br/><br/>
귀사에 대한 지속적인 최상의 서비스를 제공할 수 있도록 부디 미수금의 지불 계획에 대하여 회신 부탁 드립니다.
</div>";
					}
					#endregion
				}
				else
				{
					#region 사용불가
					if (영문여부)
					{
						if (회사코드 == "K100")
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
First of all, we would like to express the appreciation for the good cooperation and business with us.<br/><br/>
As most companies are already implementing Risk Management, we also have introduced an Early Warning System as a part of Risk Management.<br/><br/>
To provide the best service to our customers is what we are the most focusing on.
<span style='color: #FF0000;'>
But we are unable to provide any of our services such as quotation, order and delivery at the present because the amount of your long-term outstanding invoices exceeds the range we can afford.
</span><br/><br/>
Currently, your outstanding invoice is as followings. We would like you to settle overdue invoices as soon as possible and we hope that we can keep supporting your good company. It will be appreciated to inform us the expected payment plan.
</div>";
						}
						else
						{
							문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear " + 매출처명 + @",<br/><br/>
This is to remind you to settle your account balance with us immediately.<br/>
We are attaching our files on the following unsettled accounts and order your collection of same.<br/>
As a result of credit check by our CMS(Credit Management System), 
<span style='color: #FF0000;'>your order has been suspended and our quotation service being interrupted</span><br/>
Please kindly understand our situation and your prompt payment of these invoices will be greatly appreciated.
</div>";
						}
					}
					else
					{
						문구 = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
수신 : " + 매출처명 + @" 담당자 님<br/><br/>
귀사의 일익 번창을 기원합니다.<br/><br/>
아래의 세금계산서(인보이스)에 대하여 물품대금 지급일이 이미 경과하였음에도 불구하고 미수금 상환이 이루어지지 않고 있으며,<br/><br/>
당사의 위험관리 일환인 조기경보시스템에 따라 아래와 같이 미수금 리스트를 송부 드립니다.<br/><br/>
미수채권의 지급이 이행되지 않을 경우 부득이 귀사를 대상으로 견적, 수주 및 물품공급 등의 서비스를 제공할 수 없사오니<br/><br/>
귀사에 대한 지속적인 최상의 서비스를 제공할 수 있도록 부디 미수금의 지불 또는 계획에 대하여 회신 부탁 드립니다.
</div>";
					}
					#endregion
				}

				return 문구;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return string.Empty;
		}

		public bool 메일발송(string 보내는사람, string 받는사람, string 참조, string 숨은참조, string 제목, string 본문, string html, bool sendMail)
		{
			MailMessage mailMessage;
			SmtpClient smtpClient;
			DBMgr dbMgr;
			DataTable dt;
			string[] tempText;
			string address, name, id, pw, domain, query;

			try
			{
				#region 기본설정
				mailMessage = new MailMessage();
				mailMessage.SubjectEncoding = Encoding.UTF8;
				mailMessage.BodyEncoding = Encoding.UTF8;
				mailMessage.IsBodyHtml = true;
				#endregion

				#region 보내는사람
				tempText = 보내는사람.Split('/');

				if (tempText.Length == 1)
				{
					address = tempText[0];
					name = tempText[0];
				}
				else if (tempText.Length == 2)
				{
					address = tempText[0];
					name = tempText[1];
				}
				else
					return false;

				tempText = address.Split('@');

				if (tempText.Length != 2) return false;

				id = tempText[0];
				domain = tempText[1];

				query = @"SELECT DM.DM_NAME,
								 DU.DU_USERID,
								 DU.DU_PWD
						  FROM MCDOMAINUSER DU WITH(NOLOCK)
						  LEFT JOIN MCDOMAIN DM WITH(NOLOCK) ON DM.DM_ID = DU.DM_ID
						  WHERE DM.DM_NAME = '" + domain + "'" + Environment.NewLine +
						 "AND DU.DU_USERID = '" + id + "'";

				dbMgr = new DBMgr(DBConn.Mail);
				dbMgr.Query = query;
				dt = dbMgr.GetDataTable();
				pw = dt.Rows[0]["DU_PWD"].ToString();
				#endregion

				#region 메일정보
				mailMessage.From = new MailAddress(address, name, Encoding.UTF8);

				foreach (string 받는사람1 in 받는사람.Split(';'))
				{
					if (받는사람1.Trim() != "")
						mailMessage.To.Add(new MailAddress(받는사람1.Replace(";", "")));
				}

				foreach (string 참조1 in 참조.Split(';'))
				{
					if (참조1.Trim() != "")
						mailMessage.CC.Add(new MailAddress(참조1.Replace(";", "")));
				}

				foreach (string 숨은참조1 in 숨은참조.Split(';'))
				{
					if (숨은참조1.Trim() != "")
						mailMessage.Bcc.Add(new MailAddress(숨은참조1.Replace(";", "")));
				}

				mailMessage.Subject = 제목;

				// 본문 html로 변환할 시 <a>태그 앞뒤로 하고 <a>태그 내부는 건드리지 않음
				string body = "";
				string bodyA = "";
				string bodyB = "";
				string bodyC = "";

				int index = 본문.IndexOf("<a href=");

				if (index > 0)
				{
					bodyA = 본문.Substring(0, index);
					bodyB = 본문.Substring(index, 본문.IndexOf("</a>") + 4 - index);
					bodyC = 본문.Substring(본문.IndexOf("</a>") + 4);

					body = ""
						+ bodyA.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />")
						+ bodyB
						+ bodyC.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />");
				}
				else
				{
					body = 본문.Replace(" ", "&nbsp;").Replace(Environment.NewLine, "<br />"); ;
				}

				mailMessage.Body = "<div style='font-family:맑은 고딕; font-size:9pt'>" + body + "</div>" + html;
				#endregion

				#region 메일보내기
				smtpClient = new SmtpClient("113.130.254.131", 587);
				smtpClient.EnableSsl = false;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential(address, pw);
				if (sendMail == true) smtpClient.Send(mailMessage);
				#endregion

				return true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return false;
		}

        private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
        {
            DataSet ds;
			DataTable dt;
            string key;

            try
            {
                key = D.GetString(this._flexH["CD_PARTNER"]);
                
                ds = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
                                                           key });

				dt = this._biz.SearchDetail1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														    key });

				this._flex이력.Binding = dt;

				if (ds == null)
                {
                    this.chart미수금.ChartFx.Data.Clear();
                    this._flex리스트.Binding = null;
                }
                else
                {
                    if (ds.Tables[1] == null || ds.Tables[1].Rows.Count == 0)
                        this.chart미수금.ChartFx.Data.Clear();
                    else
                    {
                        this.chart미수금.DataSource = ds.Tables[1];

                        this.chart미수금.ChartFx.Series[0].Text = "매출금액";
                        this.chart미수금.ChartFx.Series[1].Text = "미수금액";
                        this.chart미수금.ChartFx.Series[2].Text = "만기금액";

                        this.chart미수금.ChartFx.Series[0].PointLabels.Visible = true;
                        this.chart미수금.ChartFx.Series[1].PointLabels.Visible = true;
                        this.chart미수금.ChartFx.Series[2].PointLabels.Visible = true;
                        
                        this.chart미수금.ChartFx.Series[1].Pane = this.chart미수금.ChartFx.Panes[1];
                        this.chart미수금.ChartFx.Series[2].Pane = this.chart미수금.ChartFx.Panes[1];
                    }

                    this._flex리스트.Binding = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }
        }
	}
}

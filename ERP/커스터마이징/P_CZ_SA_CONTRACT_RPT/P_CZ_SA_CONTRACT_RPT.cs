using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;
using Duzon.Windows.Print;
using sale;
using System.Text.RegularExpressions;

namespace cz
{
	public partial class P_CZ_SA_CONTRACT_RPT : PageBase
	{
		#region 생성자 & 전역변수
		private P_CZ_SA_CONTRACT_RPT_BIZ _biz = new P_CZ_SA_CONTRACT_RPT_BIZ();
		private string 임시폴더;

		private bool Chk수주일자 { get { return Checker.IsValid(this.dtp일자검색, true, this.cbo일자구분.SelectedText); } }

		private string 확정여부
		{
			get
			{
				if (this.rdo확정전체.Checked == true)
					return "A";
				else if (this.rdo확정.Checked == true)
					return "Y";
				else
					return "N";
			}
		}

		private string 마감여부
		{
			get
			{
				if (this.rdo마감전체.Checked == true)
					return "A";
				else if (this.rdo마감.Checked == true)
					return "Y";
				else
					return "N";
			}
		}

		public P_CZ_SA_CONTRACT_RPT()
		{
			StartUp.Certify(this);
			InitializeComponent();

			this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
		}

		public P_CZ_SA_CONTRACT_RPT(string 파일번호)
		{
			StartUp.Certify(this);
			InitializeComponent();

			this.txt수주번호.Text = 파일번호;

			this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };
		}
		#endregion

		#region 초기화
		protected override void InitLoad()
		{
			base.InitLoad();

			this.임시폴더 = "temp";
			this.ToolBarPrintButtonEnabled = true;

			this.InitGrid();
			this.InitEvent();
		}

		private void InitEvent()
		{
			this.btn재상신.Click += new EventHandler(this.btn재상신_Click);
			this.btn확정.Click += new EventHandler(this.btn확정_Click);
			this.btn확정취소.Click += new EventHandler(this.btn확정취소_Click);

			this.ctx매입처.QueryBefore += new BpQueryHandler(this.ctx매입처_QueryBefore);

			this._flexH.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
			this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
		}

		private void InitGrid()
		{
			#region Header
			this._flexH.BeginSetting(2, 1, false);

			this._flexH.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flexH.SetCol("NO_CONTRACT", "계약번호", 100);
			this._flexH.SetCol("NO_SO", "수주번호", 100);
            this._flexH.SetCol("NM_SO", "수주유형", 100);
			this._flexH.SetCol("NM_SALEGRP", "영업그룹", 100);
			this._flexH.SetCol("LN_PARTNER", "매출처", 120);
			this._flexH.SetCol("NM_PARTNER_GRP", "매출처그룹", 80);
			this._flexH.SetCol("NM_VESSEL", "호선명", 120);
			this._flexH.SetCol("NO_REF", "문의번호", 80);
			this._flexH.SetCol("NO_PO_PARTNER", "주문번호", 80);
			this._flexH.SetCol("AM_EX_SO", "외화수주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_WONAMT", "원화수주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_PO", "발주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_SO_CHARGE", "수주비용", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("AM_PO_CHARGE", "발주비용", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("PROFIT", "이윤", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("RT_PROFIT", "이윤율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexH.SetCol("PROFIT_ALL", "이윤\n(비용포함)", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("RT_PROFIT_ALL", "이윤율\n(비용포함)", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexH.SetCol("NM_EXCH", "통화명", 60);
			this._flexH.SetCol("RT_EXCH", "환율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexH.SetCol("DT_QTN", "견적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_CONTRACT", "계약일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("NM_SO_EMP", "수주담당자", 80);
			this._flexH.SetCol("STA_SO", "수주상태", 80);
			this._flexH.SetCol("YN_ACK", "수주확인서", 80, false, CheckTypeEnum.Y_N);
            this._flexH.SetCol("NM_FIRST_APPROVAL", "1차결재", 80);
            this._flexH.SetCol("NM_SECOND_APPROVAL", "2차결재", 80);
            this._flexH.SetCol("DTS_FIRST_APPROVAL", "1차결재일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DTS_SECOND_APPROVAL", "2차결재일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("DC_RMK1", "커미션", 200, false);
			this._flexH.SetCol("DC_RMK", "비고", 200, true);
            this._flexH.SetCol("DC_COMMENT_FIRST", "결재비고\n(1차결재)", 200);
            this._flexH.SetCol("DC_COMMENT_SECOND", "결재비고\n(2차결재)", 200);

			this._flexH.Cols["DTS_FIRST_APPROVAL"].Format = "####/##/##/##:##:##";
			this._flexH.Cols["DTS_SECOND_APPROVAL"].Format = "####/##/##/##:##:##";

			this._flexH.SetDummyColumn("S");
			this._flexH.SetDataMap("STA_SO", Global.MainFrame.GetComboDataCombine("S;SA_B000016"), "CODE", "NAME");

			this._flexH.SettingVersion = "1.0.0.0";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexH.SetExceptSumCol("RT_EXCH");
			#endregion

			#region Line Grid
			this._flexL.BeginSetting(2, 1, false);

            if (Certify.IsLive())
				this._flexL.SetCol("YN_GULL", "H", 40, false, CheckTypeEnum.Y_N);

			this._flexL.SetCol("SEQ_SO", "수주항번", false);
			this._flexL.SetCol("NO_DSP", "순번", 40);
			this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
			this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
			this._flexL.SetCol("CD_ITEM", "품목코드", 100);
			this._flexL.SetCol("NM_ITEM", "품목명", 120);
			this._flexL.SetCol("NM_ITEMGRP", "품목군", 80);
			this._flexL.SetCol("UNIT_SO", "단위", 40);
			this._flexL.SetCol("QT_SO", "수주수량", 40, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("NM_EXCH", "수주통화명", 60);
			this._flexL.SetCol("RT_EXCH", "환율", 40, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexL.SetCol("UM_SO", "수주단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexL.SetCol("AM_SO", "외화수주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_WONAMT", "원화수주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("RT_DC_SO", "수주D/C", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexL.SetCol("NO_DSP_BOM", "순번\n(BOM)", false);
			this._flexL.SetCol("CD_ITEM_PARTNER_BOM", "품번\n(BOM)", false);
			this._flexL.SetCol("NM_ITEM_PARTNER_BOM", "품명\n(BOM)", false);
			this._flexL.SetCol("QT_PO", "발주수량", 40, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("UM_PO", "발주단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexL.SetCol("AM_PO", "발주금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("RT_DC_PO", "발주D/C", 80, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexL.SetCol("PROFIT", "이윤", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexL.SetCol("RT_PROFIT", "이윤율", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexL.SetCol("AM_SO_ALL", "수주금액\n(BOM)", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_PO_ALL", "발주금액\n(BOM)", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_PROFIT_ALL", "이윤\n(BOM)", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexL.SetCol("RT_PROFIT_ALL", "이윤율\n(BOM)", 60, false, typeof(decimal), FormatTpType.EXCHANGE_RATE);
			this._flexL.SetCol("LN_SUPPLIER", "매입처", 120);
			this._flexL.SetCol("NO_PO_PARTNER", "매출처발주번호", false);
			this._flexL.SetCol("NO_POLINE_PARTNER", "매출처발주항번", false);
            this._flexL.SetCol("DC_RMK_CONTRACT", "비고", 120, true);

			this._flexL.ExtendLastCol = true;

			this._flexL.SettingVersion = "1.0.0.0";
			this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.None, SumPositionEnum.Top);

			this._flexL.SetExceptSumCol("UM_SO", "RT_DC_SO", "UM_PO", "RT_DC_PO", "RT_EXCH");
			#endregion
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			string query = @"SELECT CD_FLAG3 AS CODE,
									CD_FLAG3 AS NAME
							 FROM MA_CODEDTL WITH(NOLOCK)
							 WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                           @"AND CD_FIELD = 'CZ_SA00023'
							 AND ISNULL(CD_FLAG3, '') != ''
							 AND USE_YN = 'Y'
							 UNION ALL
							 SELECT CD_FLAG3 AS CODE,
									CD_FLAG3 AS NAME
							 FROM MA_CODEDTL WITH(NOLOCK)
							 WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
						   @"AND CD_FIELD = 'CZ_SA00025'
							 AND ISNULL(CD_FLAG3, '') != ''
							 AND USE_YN = 'Y'";

			DataTable dt = DBHelper.GetDataTable(query);
			dt.Rows.InsertAt(dt.NewRow(), 0);
			dt.Rows[0]["CODE"] = "";
			dt.Rows[0]["NAME"] = "";

			this.cbo수주번호.DataSource = dt;
			this.cbo수주번호.ValueMember = "CODE";
			this.cbo수주번호.DisplayMember = "NAME";

			this.cbo일자구분.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { this.DD("수주일자"), this.DD("계약일자") });
			this.cbo일자구분.ValueMember = "CODE";
			this.cbo일자구분.DisplayMember = "NAME";

			this.dtp일자검색.StartDateToString = Global.MainFrame.GetDateTimeToday().AddDays(-7).ToString("yyyyMMdd");
			this.dtp일자검색.EndDateToString = this.MainFrameInterface.GetStringToday;

			this.dtp계약일자.Text = this.MainFrameInterface.GetStringToday;

			this.cbo매출처그룹.DataSource = Global.MainFrame.GetComboDataCombine("S;MA_B000065");
			this.cbo매출처그룹.ValueMember = "CODE";
			this.cbo매출처그룹.DisplayMember = "NAME";
		}
		#endregion

		#region 메인버튼
		protected override bool BeforeSearch()
		{
			return base.BeforeSearch();
		}


		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch() || !Chk수주일자) return;

				DataTable dt = null;

				int 기간 = Math.Abs((this.dtp일자검색.StartDate - this.dtp일자검색.EndDate).Days);

				string startDate, endDate;
				DataTable tmpDataTable;

				for (int i = 0; i <= 기간 / 365; i++)
				{
					startDate = this.dtp일자검색.StartDate.AddDays(i * 365).ToString("yyyyMMdd");

					if (기간 >= (i + 1) * 365)
						endDate = this.dtp일자검색.StartDate.AddDays(((i + 1) * 365) - 1).ToString("yyyyMMdd");
					else
						endDate = this.dtp일자검색.EndDateToString;

					MsgControl.ShowMsg("조회 중 입니다. 잠시만 기다려 주세요.\n조회기간 (@ ~ @)", new string[] { Util.GetTo_DateStringS(startDate), Util.GetTo_DateStringS(endDate) });

					tmpDataTable = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																   D.GetString(this.cbo수주번호.SelectedValue),
																   this.txt수주번호.Text,
																   D.GetString(this.cbo일자구분.SelectedValue),
																   startDate,
																   endDate,
																   this.ctx영업담당자.CodeValue,
																   this.txt계약번호.Text,
																   D.GetString(this.cbo매출처그룹.SelectedValue),
																   this.ctx매출처.CodeValue,
																   this.ctx매입처.CodeValue,
																   this.확정여부,
																   (this.chk청구건포함.Checked == true ? "Y" : "N"),
																   (this.chk클레임포함.Checked == true ? "Y" : "N"),
																   this.ctx품목군.CodeValue,
																   this.ctx영업그룹.CodeValue,
																   this.ctx호선번호.CodeValue,
																   this.마감여부,
																   (this.chk재상신대상.Checked == true ? "Y" : "N"),
																   (this.chk이윤미달.Checked == true ? "Y" : "N") });

					if (i == 0)
						dt = tmpDataTable;
					else
						dt.Merge(tmpDataTable);
				}

				this._flexH.Binding = dt;

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
                }
                else
                {
                    decimal 원화수주금액, 수주비용, 이윤, 비용포함이윤;

                    원화수주금액 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_WONAMT)", string.Empty));
                    수주비용 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(AM_SO_CHARGE)", string.Empty));
                    이윤 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(PROFIT)", string.Empty));
                    비용포함이윤 = D.GetDecimal(this._flexH.DataTable.Compute("SUM(PROFIT_ALL)", string.Empty));

                    this._flexH[this._flexH.Rows.Fixed - 1, "RT_PROFIT"] = string.Format("{0:" + this._flexH.Cols["RT_PROFIT"].Format + "}", (원화수주금액 == 0 ? 0 : Decimal.Round(((이윤 / 원화수주금액) * 100), 2, MidpointRounding.AwayFromZero)));
                    this._flexH[this._flexH.Rows.Fixed - 1, "RT_PROFIT_ALL"] = string.Format("{0:" + this._flexH.Cols["RT_PROFIT_ALL"].Format + "}", ((원화수주금액 + 수주비용) == 0 ? 0 :Decimal.Round(((비용포함이윤 / (원화수주금액 + 수주비용)) * 100), 2, MidpointRounding.AwayFromZero)));
                }
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
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
			catch (Exception Ex)
			{
				MsgEnd(Ex);
			}
		}

		protected override bool SaveData()
		{
			try
			{
				if (!base.SaveData()) return false;

				if (this._flexH.IsDataChanged == false && this._flexL.IsDataChanged == false)
				{
					this.ShowMessage(공통메세지.변경된내용이없습니다);
					return false;
				}

				if (this._biz.Save(this._flexH.GetChanges(), this._flexL.GetChanges()))
				{
					this._flexH.AcceptChanges();
					this._flexL.AcceptChanges();
					return true;
				}
			}
			catch (Exception Ex)
			{
				MsgEnd(Ex);
			}

			return false;
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				this.ShowMessage("인쇄버튼으로 인쇄 불가 합니다.\n(확정, 재상신 버튼으로 인쇄 가능)");
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			try
			{
				this.임시파일제거();
				return base.OnToolBarExitButtonClicked(sender, e);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return false;
		}

		private void 임시파일제거()
		{
			DirectoryInfo dirInfo;
			bool isExistFile;

			try
			{
				dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, this.임시폴더));
				isExistFile = false;

				if (dirInfo.Exists == true)
				{
					foreach (FileInfo file in dirInfo.GetFiles("*", SearchOption.AllDirectories))
					{
						try
						{
							file.Delete();
						}
						catch
						{
							isExistFile = true;
							continue;
						}
					}

					if (isExistFile == false)
						dirInfo.Delete(true);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region 버튼 이벤트
		private void btn계약일자변경_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (this._flexH.HasNormalRow == false) return;

				dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					if (this._flexH.DataTable.Select("S = 'Y' AND ISNULL(DT_CONTRACT, '') = ''").Length > 0)
					{
						this.ShowMessage("CZ_선택된 자료중 확정되지 않은 건이 있습니다.");
						return;
					}

					foreach(DataRow dr in dataRowArray)
					{
						dr["DT_CONTRACT"] = this.dtp계약일자.Text;
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.DD("변경"));
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn재상신_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				if (this._flexH.HasNormalRow == false) return;
                
                if (string.IsNullOrEmpty(this._flexH["NO_CONTRACT"].ToString()))
                {
                    this.ShowMessage("확정 상태만 재상신 가능 합니다.");
                    return;
                }

				if (this.ShowMessage("재상신 하시겠습니까 ?", "QY2") != DialogResult.Yes)
					return;

                this.PDF출력(false, null);

				DBHelper.ExecuteNonQuery("SP_CZ_SA_CONTRACT_RPT_RE", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					D.GetString(this._flexH["NO_SO"]),
																					Global.MainFrame.LoginInfo.UserID });

				query = @"UPDATE SH
SET SH.RT_PROFIT = {2},
	SH.RT_PROFIT_CHARGE = {3}
FROM SA_SOH SH WITH(NOLOCK)
WHERE SH.CD_COMPANY = '{0}'
AND SH.NO_SO = '{1}'";

				DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
															this._flexH["NO_SO"].ToString(),
															this._flexH["RT_PROFIT"],
															this._flexH["RT_PROFIT_ALL"]));

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn재상신.Text);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn확정_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			DataTable 확정데이터;
			DataTable 라인데이터;
			StringBuilder 검증리스트;
			bool 검증여부, isRefresh = false;
			string prefix, 수주번호, 수주상태, 수주확인서여부, 수주수량, 발주수량, Proforma, msg, msg2, query;

			try
			{
				if (!this._flexH.HasNormalRow) return;

				if (string.IsNullOrEmpty(this.dtp계약일자.Text))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl계약일자.Text);
					return;
				}

				this.btn확정.Enabled = false;
				this.btn확정취소.Enabled = false;

				dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (Global.MainFrame.LoginInfo.CompanyCode == "K100" &&
						 this._flexH.DataTable.Select("S = 'Y' AND NO_SO NOT LIKE 'CL%' AND NO_SO NOT LIKE 'HB%' AND YN_AM = 'N' AND ISNULL(DC_RMK, '') = ''").Length > 0)
				{
					this.ShowMessage("비고에 사유가 입력되지 않은 무상공급 건이 있습니다. (CL, HB 제외)");
					return;
				}
				else
				{
					if (!this._flexL.HasNormalRow) return;
					if (!IsAgingCheck(dataRowArray)) return;

					검증여부 = false;

					검증리스트 = new StringBuilder();

					msg = this.DD("수주번호") + "  " + this.DD("수주상태") + "  " + this.DD("수주확인서") + "  " + this.DD("수주수량") + "  " + this.DD("발주수량") + "  " + this.DD("Proforma");
					검증리스트.AppendLine(msg);
					msg = "-".PadRight(63, '-');
					검증리스트.AppendLine(msg);

					확정데이터 = this._flexH.DataTable.Clone();
					라인데이터 = this._flexL.DataTable.Clone();
					prefix = D.GetString(this.GetSeq(this.LoginInfo.CompanyCode, "CZ", "04")).Substring(0, 1);

					foreach(DataRow dr in dataRowArray)
					{
						수주번호 = dr["NO_SO"].ToString().PadRight(10, ' ');
						수주상태 = dr["NM_STA_SO"].ToString().PadRight(8, ' ');
						수주확인서여부 = dr["YN_ACK"].ToString().PadRight(12, ' ');
						수주수량 = String.Format("{0:n}", dr["QT_SO_ORG"]).PadRight(10, ' ');
						발주수량 = String.Format("{0:n}", dr["QT_PO_ORG"]).PadRight(10, ' ');
						Proforma = dr["YN_PROFORMA"].ToString().PadRight(12, ' ');

						msg2 = 수주번호 + 수주상태 + 수주확인서여부 + 수주수량 + 발주수량 + Proforma;

                        if (!string.IsNullOrEmpty(D.GetString(dr["NO_CONTRACT"])))
                        {
                            검증리스트.AppendLine(msg2);
							검증여부 = true;
							isRefresh = true;
                            continue;
                        }

						if (dr["YN_CIA"].ToString() == "Y" && dr["YN_PROFORMA"].ToString() == "N")
						{
							검증리스트.AppendLine(msg2);
							검증여부 = true;
							isRefresh = true;
							continue;
						}

                        this.발주서발송여부확인(dr["NO_SO"].ToString());
                        this.현대공사번호확인(dr["NO_SO"].ToString());

                        if (D.GetString(dr["YN_CHARGE"]) == "Y")
						{
							dr["STA_SO"] = "R"; //수주확정
							dr["NO_CONTRACT"] = this.계약번호(prefix, D.GetString(dr["NO_SO"]));
							dr["DT_CONTRACT"] = this.dtp계약일자.Text;

							확정데이터.ImportRow(dr);
							라인데이터.Merge(new DataView(this._flexL.DataTable, "NO_SO = '" + D.GetString(dr["NO_SO"]) + "'", string.Empty, DataViewRowState.CurrentRows).ToTable());
						}
						else if (D.GetString(dr["YN_ACK"]) == "Y" && (D.GetDecimal(dr["QT_SO_ORG"]) == D.GetDecimal(dr["QT_PO_ORG"])))
						{
							dr["STA_SO"] = "R"; //수주확정
							dr["NO_CONTRACT"] = this.계약번호(prefix, D.GetString(dr["NO_SO"]));
							dr["DT_CONTRACT"] = this.dtp계약일자.Text;

							확정데이터.ImportRow(dr);
							라인데이터.Merge(new DataView(this._flexL.DataTable, "NO_SO = '" + D.GetString(dr["NO_SO"]) + "'", string.Empty, DataViewRowState.CurrentRows).ToTable());
						}
						else
						{
							검증리스트.AppendLine(msg2);
							검증여부 = true;
							isRefresh = true;
						}
					}

					if (라인데이터.Rows.Count > 0)
					{
						if (BASIC.GetMAEXC("ATP사용여부") == "001")
						{
							if (!ATP체크로직(true, 라인데이터))
							{
								isRefresh = true;
								return;
							}
						}

						if (BASIC.GetMAEXC("여신한도") == "300")
						{
							if (!거래처환종별체크(라인데이터))
							{
								isRefresh = true;
								return;
							}
						}
						else
						{
							if (!거래처별체크(라인데이터))
							{
								isRefresh = true;
								return;
							}
						}

						foreach (DataRow dr in 라인데이터.Rows)
						{
							if ((D.GetDecimal(dr["RT_PROFIT"]) <= -3 || D.GetDecimal(dr["PROFIT"]) <= -50000) && 
								dr["DC_RMK_CONTRACT"].ToString().Length < 5)
							{
								this.ShowMessage(string.Format("순번 {0}. 이윤율 -3% 이하 또는 이윤 -50,000원 이하 입니다.\n품목 비고에 사유 5자 이상 기입하시기 바랍니다.", D.GetInt(dr["NO_DSP"]).ToString()));
								return;
							}
						}
					}

					if (검증여부)
					{
						this.ShowDetailMessage("CZ_상태를 변경하지 못한 건이 있습니다.▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
					}

					if (확정데이터.Rows.Count > 0)
					{
						if (this.PDF출력(false, 확정데이터))
                        {
                            if (this._biz.확정상태저장(확정데이터) == true)
                            {
								foreach(DataRow dr in 확정데이터.Rows)
								{
									query = @"UPDATE SH
SET SH.RT_PROFIT = {2},
	SH.RT_PROFIT_CHARGE = {3}
FROM SA_SOH SH WITH(NOLOCK)
WHERE SH.CD_COMPANY = '{0}'
AND SH.NO_SO = '{1}'";

									DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																				dr["NO_SO"].ToString(),
																				dr["RT_PROFIT"],
																				dr["RT_PROFIT_ALL"]));

									DX.디비.실행("PI_CZ_DX_AUTO_GIR", new object[] { Global.MainFrame.LoginInfo.CompanyCode, dr["NO_SO"].ToString() });
								}

								this._flexH.AcceptChanges();

                                this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn확정.Text);
                            }
                            else
                                isRefresh = true;
                        }
                        else
                            isRefresh = true;
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				if (isRefresh == true)
					this.OnToolBarSearchButtonClicked(null, null);

				this.btn확정.Enabled = true;
				this.btn확정취소.Enabled = true;
			}
		}

		private void btn확정취소_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			DataTable 확정취소데이터;
			StringBuilder 검증리스트;
			bool 검증여부 = false, isRefresh = false;
			string msg, msg2, 수주번호, 수주상태, 송품협조전수량, 포장협조전수량;

			try
			{
				this.btn확정.Enabled = false;
				this.btn확정취소.Enabled = false;

				dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					if (!this._flexL.HasNormalRow) return;

					검증리스트 = new StringBuilder();

                    msg = this.DD("수주번호") + "  " + this.DD("수주상태") + "  " + this.DD("송품협조전수량") + "  " + this.DD("포장협조전수량");
					검증리스트.AppendLine(msg);
					msg = "-".PadRight(63, '-');
					검증리스트.AppendLine(msg);

					확정취소데이터 = this._flexH.DataTable.Clone();

					foreach (DataRow dr in dataRowArray)
					{
						수주번호 = dr["NO_SO"].ToString().PadRight(10, ' ');
						수주상태 = dr["NM_STA_SO"].ToString().PadRight(8, ' ');
						송품협조전수량 = String.Format("{0:n}", dr["QT_GIR"]).PadRight(10, ' ');
                        포장협조전수량 = String.Format("{0:n}", dr["QT_GIR_PACK"]).PadRight(10, ' ');

						msg2 = 수주번호 + 수주상태 + 송품협조전수량 + 포장협조전수량;

						if (string.IsNullOrEmpty(D.GetString(dr["NO_CONTRACT"])) == false)
						{
							if (D.GetString(dr["YN_CHARGE"]) == "Y")
							{
								dr["STA_SO"] = "O"; //수주미정
								dr["NO_CONTRACT"] = string.Empty;
								dr["DT_CONTRACT"] = string.Empty;

								확정취소데이터.ImportRow(dr);
							}
                            else if (D.GetString(dr["STA_SO"]) == "R" && Convert.ToDecimal(dr["QT_GIR"]) == 0 && Convert.ToDecimal(dr["QT_GIR_PACK"]) == 0)
							{
								dr["STA_SO"] = "O"; //수주미정
								dr["NO_CONTRACT"] = string.Empty;
								dr["DT_CONTRACT"] = string.Empty;

								확정취소데이터.ImportRow(dr);
							}
							else
							{
								검증리스트.AppendLine(msg2);
								검증여부 = true;
								isRefresh = true;
							}
						}
						else
						{
							검증리스트.AppendLine(msg2);
							검증여부 = true;
							isRefresh = true;
						}
					}

					if (검증여부)
					{
						ShowDetailMessage("CZ_상태를 변경하지 못한 건이 있습니다.▼ 버튼을 눌러서 불일치 목록을 확인하세요!", 검증리스트.ToString());
					}

					if (확정취소데이터.Rows.Count > 0)
					{
						if (this._biz.확정상태저장(확정취소데이터) == true)
						{
							this._flexH.AcceptChanges();

							this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn확정취소.Text);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				if (isRefresh == true)
					this.OnToolBarSearchButtonClicked(null, null);

				this.btn확정.Enabled = true;
				this.btn확정취소.Enabled = true;
			}
		}

		private void ctx매입처_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			switch (e.HelpID)
			{
				case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
					e.HelpParam.P61_CODE1 = "N";
					e.HelpParam.P62_CODE2 = "Y";
					break;
				case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1:
					e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
					break;
				case Duzon.Common.Forms.Help.HelpID.P_MA_PARTNER_SUB:
					break;
			}
		}

		private bool 거래처별체크(DataTable dt)
		{
			decimal 금액;

			foreach (DataRow dr in new DataView(dt, "TP_BUSI = '001'", string.Empty, DataViewRowState.CurrentRows).ToTable(true, "CD_PARTNER").Rows)
			{
				금액 = D.GetDecimal(dt.Compute("SUM(AM_WONAMT) + SUM(AM_VAT)", "CD_PARTNER = '" + D.GetString(dr["CD_PARTNER"]) + "'"));
				if (!this._biz.CheckCredit(D.GetString(dr["CD_PARTNER"]), 금액)) return false;
			}

			return true;
		}

		private bool 거래처환종별체크(DataTable dt)
		{
			string str1, str2;
			decimal num;
			DataTable dataTable = dt.DefaultView.ToTable(1 != 0, "CD_PARTNER", "CD_EXCH");

			foreach (DataRow dr in dataTable.Rows)
			{
				str1 = D.GetString(dr["CD_PARTNER"]);
				str2 = D.GetString(dr["CD_EXCH"]);
				num = D.GetDecimal(dt.Compute("SUM(AM_SO)", "CD_PARTNER ='" + str1 + "' AND CD_EXCH = '" + str2 + "'"));

				if (!this._biz.CheckCreditExec(str1, str1, num))
					return false;
			}

			return true;
		}

		private bool IsAgingCheck(DataRow[] drs)
		{
			채권연령관리 채권연령관리 = new 채권연령관리();

			DataTable dataTable = this._flexH.DataTable.Copy();
			dataTable.DefaultView.RowFilter = "S = 'Y' AND STA_SO = 'O' AND YN_ACK = 'Y'";
			DataRow[] drs1 = dataTable.DefaultView.ToTable(true, "CD_PARTNER").Select();
			DataTable dtReturn1;
			채권연령관리.채권연령체크(drs1, AgingCheckPoint.수주확정, out dtReturn1);

			if (dtReturn1 == null || dtReturn1.Rows.Count == 0)
				return true;

			P_SA_CUST_CREDIT_CHECK_SUB custCreditCheckSub = new P_SA_CUST_CREDIT_CHECK_SUB(dtReturn1);

			if (custCreditCheckSub.ShowDialog() != DialogResult.OK) return false;

			DataTable dtReturn2 = custCreditCheckSub.dtReturn;
			dtReturn2.PrimaryKey = new DataColumn[] { dtReturn2.Columns["CD_PARTNER"] };

			foreach (DataRow dataRow1 in drs)
			{
				DataRow dataRow2 = dtReturn2.Rows.Find(D.GetString(dataRow1["CD_PARTNER"]));

				if (dataRow2 != null && !(D.GetString(dataRow2["S"]) == "Y")) dataRow1["S"] = "N";
			}

			return true;
		}

		private bool ATP체크로직(bool 자동체크, DataTable dt)
		{
			DataRow[] drsPlant = dt.DefaultView.ToTable(true, new string[] { "CD_PLANT" }).Select();

			if (drsPlant.Length > 1)
			{
				ShowMessage("두개 이상의 공장이 선택되어 ATP체크가 불가합니다");
				return false;
			}

			Duzon.ERPU.MF.Common.ATP ATP = new Duzon.ERPU.MF.Common.ATP();

			string ATP사용유무 = ATP.ATP환경설정_사용유무(LoginInfo.BizAreaCode, D.GetString(drsPlant[0]["CD_PLANT"]));
			if (ATP사용유무 == "N") return true;

			string 메뉴별ATP처리 = ATP.ATP자동체크_저장로직(D.GetString(drsPlant[0]["CD_PLANT"]), "200");
			if (메뉴별ATP처리 != "000" && 메뉴별ATP처리 != "001") return true;

			DataRow[] drs = dt.Select("YN_ATP = 'Y'", "", DataViewRowState.CurrentRows);

			//if (drs.Length != dt.DefaultView.ToTable(true, new string[] { "CD_ITEM", "YN_ATP", "STA_SO" }).Select("YN_ATP = 'Y' AND STA_SO = 'O'").Length)
			//{
			//    if (ShowMessage("동일품목이 존재 할 경우 정확한 ATP체크를 할 수 없습니다.@계속 진행하시겠습니까?", "QY2") != DialogResult.Yes)
			//        return false;
			//}

			string s_Message = string.Empty;

			ATP.Set메뉴ID = "P_SA_SO_MNG";

			bool ATPGood = ATP.ATP_Check(drs, out s_Message);

			if (ATPGood == false)
			{
				if (자동체크 == false)
				{
					ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", s_Message);
					return false;
				}
				else
				{
					if (메뉴별ATP처리 == "000")
					{
						if (ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요." + Environment.NewLine + "그래도 확정하시겠습니까?", "", s_Message, "QY2") != DialogResult.Yes)
							return false;
						else
							return true;
					}
					else if (메뉴별ATP처리 == "001")
					{
						ShowDetailMessage("납기일을 맞출 수 없습니다." + Environment.NewLine + "[︾] 버튼을 눌러 내역을 확인하세요.", s_Message);
						return false;
					}

					return true;
				}
			}

			return true;
		}

        private bool 현대공사번호확인(string 수주번호)
        {
            bool result = true;
            P_CZ_SA_CONTRACT_RPT_HSG_SUB dialog;

            try
            {
                DataTable dt = DBHelper.GetDataTable(@"SELECT PH.NO_PO, PH.NO_ORDER 
                                                       FROM PU_POH PH WITH(NOLOCK)
                                                       WHERE PH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
                                                      "AND PH.CD_PARTNER = '11823'" +
                                                      "AND PH.CD_PJT = '" + 수주번호 + "'");

                if (dt == null || dt.Rows.Count == 0) return result;

                Regex regex = new Regex(@"^(K[A-Z]{2}\d{7})");

                foreach (DataRow dr in dt.Rows)
                {
                    if (!regex.IsMatch(dr["NO_ORDER"].ToString().ToUpper()))
                    {
                        dialog = new P_CZ_SA_CONTRACT_RPT_HSG_SUB(dr["NO_PO"].ToString(), dr["NO_ORDER"].ToString());
                        dialog.ShowDialog();
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return result;
        }

        private bool 발주서발송여부확인(string 수주번호)
        {
            bool result = true;

            try
            {
                if (수주번호.Left(2) == "BE" || 
                    수주번호.Left(2) == "BS" ||
                    수주번호.Left(2) == "HB" ||
                    수주번호.Left(2) == "CL") return result;
                
                DataTable dt = DBHelper.GetDataTable(@"SELECT PH.NO_PO 
                                                       FROM PU_POH PH WITH(NOLOCK)
                                                       WHERE PH.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                                      "AND PH.CD_PJT = '" + 수주번호 + "'" + Environment.NewLine +
                                                      "AND PH.DT_SEND IS NULL");

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.ShowMessage("발주서가 발송되지 않은 건이 존재 합니다. (" + 수주번호 + ")");
                    result = false;
                }
            }
            catch (Exception ex)
            {
                MsgEnd(ex);
            }

            return result;
        }
		#endregion

		#region 그리드 이벤트
		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string key, filter;

			try
			{
				dt = null;
				key = _flexH["NO_SO"].ToString();
				filter = "NO_SO = '" + key + "'";

				if (this._flexH.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   key,
															   this.ctx영업담당자.CodeValue,
															   this.txt계약번호.Text,
															   this.ctx매출처.CodeValue,
															   this.ctx매입처.CodeValue,
															   this.ctx품목군.CodeValue });
				}

				this._flexL.BindingAdd(dt, filter);

                if (this._flexL.HasNormalRow)
                {
                    decimal 원화수주금액, 이윤;

                    원화수주금액 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_WONAMT)", string.Empty));
                    이윤 = D.GetDecimal(this._flexL.DataTable.Compute("SUM(PROFIT)", string.Empty));

                    this._flexL[this._flexL.Rows.Fixed - 1, "RT_PROFIT"] = string.Format("{0:" + this._flexL.Cols["RT_PROFIT"].Format + "}", (원화수주금액 == 0 ? 0 : Decimal.Round(((이윤 / 원화수주금액) * 100), 2, MidpointRounding.AwayFromZero)));
                    this._flexL[this._flexL.Rows.Fixed - 1, "AM_SO_ALL"] = this._flexL[this._flexL.Rows.Fixed - 1, "AM_WONAMT"];
                    this._flexL[this._flexL.Rows.Fixed - 1, "AM_PO_ALL"] = this._flexL[this._flexL.Rows.Fixed - 1, "AM_PO"];
                    this._flexL[this._flexL.Rows.Fixed - 1, "AM_PROFIT_ALL"] = this._flexL[this._flexL.Rows.Fixed - 1, "PROFIT"];
                    this._flexL[this._flexL.Rows.Fixed - 1, "RT_PROFIT_ALL"] = this._flexL[this._flexL.Rows.Fixed - 1, "RT_PROFIT"];
                }

				this.셀병합();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexH_CheckHeaderClick(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow) return;
				
				this._flexH.Redraw = false;

				//데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
				this._flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

				for (int h = 0; h < this._flexH.Rows.Count - 1; h++)
				{
					MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h + 1), D.GetString(this._flexH.Rows.Count - 1) });

					this._flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				this._flexH.Redraw = true;
			}
		}
		#endregion

		#region 기타 메소드
		private bool PDF출력(bool isOverWrite, DataTable 확정데이터)
		{
			ReportHelper reportHelper, reportHelper1;
			DataTable pdfDTH, pdfDTL, pdfDTG, printDTH, printDTL, printDTG;
			DataRow[] dataRowArray;
			string filePath, fileName, query, realFileName;
            bool isMissing;

			try
			{
				filePath = Path.Combine(Application.StartupPath, this.임시폴더) + "\\";
				FileMgr.CreateDirectory(filePath);

				printDTH = this._flexH.DataTable.Clone();
				printDTL = this._flexL.DataTable.Clone();
                isMissing = false;

				if (확정데이터 == null)
				{
					dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

					if (dataRowArray == null || dataRowArray.Length == 0)
					{
						this.ShowMessage(공통메세지.선택된자료가없습니다);
						return false;
					}
				}
				else
					dataRowArray = 확정데이터.Select();
				
				foreach (DataRow drH in dataRowArray)
				{
					pdfDTL = this._flexL.DataTable.Clone();
					foreach (DataRow drL in this._flexL.DataTable.Select("NO_SO = '" + D.GetString(drH["NO_SO"]) + "'"))
					{
						pdfDTL.ImportRow(drL);
						pdfDTL.Rows[pdfDTL.Rows.Count - 1]["NO_DSP"] = drL["NO_DSP_BOM"];
						pdfDTL.Rows[pdfDTL.Rows.Count - 1]["CD_ITEM_PARTNER"] = drL["CD_ITEM_PARTNER_BOM"];
						pdfDTL.Rows[pdfDTL.Rows.Count - 1]["NM_ITEM_PARTNER"] = drL["NM_ITEM_PARTNER_BOM"];

						printDTL.ImportRow(drL);
						printDTL.Rows[printDTL.Rows.Count - 1]["NO_DSP"] = drL["NO_DSP_BOM"];
						printDTL.Rows[printDTL.Rows.Count - 1]["CD_ITEM_PARTNER"] = drL["CD_ITEM_PARTNER_BOM"];
						printDTL.Rows[printDTL.Rows.Count - 1]["NM_ITEM_PARTNER"] = drL["NM_ITEM_PARTNER_BOM"];
					}

					pdfDTH = this._flexH.DataTable.Clone();
					pdfDTH.ImportRow(drH);
					printDTH.ImportRow(drH);

					pdfDTG = this.매입처별발주현황(pdfDTL);
					reportHelper = Util.SetRPT_SPO("R_CZ_SA_CONTRACT_RPT", "수발주통보서", Global.MainFrame.LoginInfo.CompanyCode, pdfDTH, pdfDTL);

					reportHelper.SetDataTable(pdfDTH, 1);
					reportHelper.SetDataTable(pdfDTL, 2);
					reportHelper.SetDataTable(pdfDTG, 3);

					if (isOverWrite == true)
					{
						query = @"SELECT TOP 1 NM_FILE_REAL
								  FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
								  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
								@"AND NO_KEY = '" + D.GetString(drH["NO_SO"]) + "'" + 
								@"AND TP_STEP = '11'
								  ORDER BY DTS_INSERT DESC";

						fileName = (string)Global.MainFrame.ExecuteScalar(query);

                        if (string.IsNullOrEmpty(fileName) == true && D.GetString(drH["STA_SO"]) == "R")
                        {
                            isMissing = true;
                            fileName = (D.GetString(drH["NO_CONTRACT"]) + "_CTR_" + D.GetString(drH["DT_CONTRACT"]) + ".pdf");
                        }
					}
					else
					{
						fileName = (D.GetString(drH["NO_CONTRACT"]) + "_CTR_" + D.GetString(drH["DT_CONTRACT"]) + ".pdf");
					}

					
					reportHelper.PrintDirect(Util.GetReportFileName("R_CZ_SA_CONTRACT_RPT", Global.MainFrame.LoginInfo.CompanyCode) + ".DRF", true, true, filePath + fileName, new Dictionary<string, string>());
					realFileName = FileMgr.Upload_WF(Global.MainFrame.LoginInfo.CompanyCode, D.GetString(drH["NO_SO"]), filePath + fileName, isOverWrite);

                    if (isOverWrite == false || isMissing == true)
					{
						#region 데이터 삽입
						query = @"INSERT INTO CZ_MA_WORKFLOWL
								  (CD_COMPANY, NO_KEY, TP_STEP, NO_LINE, NM_FILE, NM_FILE_REAL, YN_DONE, DTS_DONE, ID_INSERT, DTS_INSERT)
								  VALUES
								  ('{0}', '{1}', '11', {2}, '{3}', '{4}', 'Y', NEOE.SF_SYSDATE(GETDATE()), '{5}', NEOE.SF_SYSDATE(GETDATE()))";

						query = string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
													 D.GetString(drH["NO_SO"]),
                                                     @"(SELECT ISNULL(MAX(NO_LINE), 0) + 1
													   FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
													   WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" +
													  "AND NO_KEY = '" + D.GetString(drH["NO_SO"]) + "'" +
													  "AND TP_STEP = '11')",
													 fileName,
													 realFileName,
													 Global.MainFrame.LoginInfo.UserID);

						DBHelper.ExecuteScalar(query);
						#endregion
					}
				}

				if (printDTH.Rows.Count > 0)
				{
					printDTG = this.매입처별발주현황(printDTL);
					reportHelper1 = Util.SetRPT_SPO("R_CZ_SA_CONTRACT_RPT", "수발주통보서", Global.MainFrame.LoginInfo.CompanyCode, printDTH, printDTL);

					reportHelper1.SetDataTable(printDTH, 1);
					reportHelper1.SetDataTable(printDTL, 2);
					reportHelper1.SetDataTable(printDTG, 3);

					Util.RPT_Print(reportHelper1);
				}

                return true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

            return false;
		}

		private void 셀병합()
		{
			CellRange cellRange;
			DataTable dt;
			string key;

			try
			{
				if (this._flexL.HasNormalRow == false) return;

				this._flexL.Redraw = false;

				for (int row = this._flexL.Rows.Fixed; row < this._flexL.Rows.Count; row++)
				{
					key = D.GetString(this._flexL.GetData(row, "SEQ_SO"));

					cellRange = this._flexL.GetCellRange(row, "NO_DSP", row, "NO_DSP");
					cellRange.UserData = key + "_" + "NO_DSP";

					cellRange = this._flexL.GetCellRange(row, "CD_ITEM_PARTNER", row, "CD_ITEM_PARTNER");
					cellRange.UserData = key + "_" + "CD_ITEM_PARTNER";

					cellRange = this._flexL.GetCellRange(row, "NM_ITEM_PARTNER", row, "NM_ITEM_PARTNER");
					cellRange.UserData = key + "_" + "NM_ITEM_PARTNER";

					cellRange = this._flexL.GetCellRange(row, "CD_ITEM", row, "CD_ITEM");
					cellRange.UserData = key + "_" + "CD_ITEM";

					cellRange = this._flexL.GetCellRange(row, "NM_ITEM", row, "NM_ITEM");
					cellRange.UserData = key + "_" + "NM_ITEM";

                    cellRange = this._flexL.GetCellRange(row, "NM_ITEMGRP", row, "NM_ITEMGRP");
                    cellRange.UserData = key + "_" + "NM_ITEMGRP";

					cellRange = this._flexL.GetCellRange(row, "UNIT_SO", row, "UNIT_SO");
					cellRange.UserData = key + "_" + "UNIT_SO";

					cellRange = this._flexL.GetCellRange(row, "QT_SO", row, "QT_SO");
					cellRange.UserData = key + "_" + "QT_SO";

					cellRange = this._flexL.GetCellRange(row, "NM_EXCH", row, "NM_EXCH");
					cellRange.UserData = key + "_" + "NM_EXCH";

					cellRange = this._flexL.GetCellRange(row, "RT_EXCH", row, "RT_EXCH");
					cellRange.UserData = key + "_" + "RT_EXCH";

					cellRange = this._flexL.GetCellRange(row, "UM_SO", row, "UM_SO");
					cellRange.UserData = key + "_" + "UM_SO";

					cellRange = this._flexL.GetCellRange(row, "AM_SO", row, "AM_SO");
					cellRange.UserData = key + "_" + "AM_SO";

					cellRange = this._flexL.GetCellRange(row, "RT_DC_SO", row, "RT_DC_SO");
					cellRange.UserData = key + "_" + "RT_DC_SO";

					cellRange = this._flexL.GetCellRange(row, "AM_SO_ALL", row, "AM_SO_ALL");
					cellRange.UserData = key + "_" + "AM_SO_ALL";

					cellRange = this._flexL.GetCellRange(row, "AM_PO_ALL", row, "AM_PO_ALL");
					cellRange.UserData = key + "_" + "AM_PO_ALL";

					cellRange = this._flexL.GetCellRange(row, "AM_PROFIT_ALL", row, "AM_PROFIT_ALL");
					cellRange.UserData = key + "_" + "AM_PROFIT_ALL";

					cellRange = this._flexL.GetCellRange(row, "RT_PROFIT_ALL", row, "RT_PROFIT_ALL");
					cellRange.UserData = key + "_" + "RT_PROFIT_ALL";

                    cellRange = this._flexL.GetCellRange(row, "DC_RMK_CONTRACT", row, "DC_RMK_CONTRACT");
                    cellRange.UserData = key + "_" + "DC_RMK_CONTRACT";
				}

				this._flexL.DoMerge();

				dt = this._flexL.DataTable.DefaultView.ToTable(true, "NO_SO", "SEQ_SO", "QT_SO", "AM_SO");

				this._flexL[this._flexL.Rows.Fixed - 1, "QT_SO"] = string.Format("{0:" + this._flexL.Cols["QT_SO"].Format + "}", D.GetDecimal(dt.Compute("SUM(QT_SO)", string.Empty)));
				this._flexL[this._flexL.Rows.Fixed - 1, "AM_SO"] = string.Format("{0:" + this._flexL.Cols["AM_SO"].Format + "}", D.GetDecimal(dt.Compute("SUM(AM_SO)", string.Empty)));
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			finally
			{
				this._flexL.Redraw = true;
			}
		}

		private DataTable 매입처별발주현황(DataTable dt)
		{
			string filter;
			DataTable result = null;
			decimal 수주원화금액;

			try
			{
				result = ComFunc.getGridGroupBy(dt, new string[] { "NO_SO", "CD_EXCH", "NM_EXCH", "CD_SUPPLIER", "LN_SUPPLIER" }, true);

				if (result.Columns.Contains("AM_PO") == false) result.Columns.Add("AM_PO");
				if (result.Columns.Contains("RT_DC_PO") == false) result.Columns.Add("RT_DC_PO");
				if (result.Columns.Contains("AM_SO") == false) result.Columns.Add("AM_SO");
				if (result.Columns.Contains("RT_DC_SO") == false) result.Columns.Add("RT_DC_SO");
				if (result.Columns.Contains("PROFIT") == false) result.Columns.Add("PROFIT");
				if (result.Columns.Contains("RT_PROFIT") == false) result.Columns.Add("RT_PROFIT");
				if (result.Columns.Contains("NM_FG_PAYMENT") == false) result.Columns.Add("NM_FG_PAYMENT");

				foreach (DataRow dr in result.Rows)
				{
					filter = "NO_SO = '" + D.GetString(dr["NO_SO"]) + "' AND CD_SUPPLIER = '" + D.GetString(dr["CD_SUPPLIER"]) + "'";
					dr["AM_PO"] = dt.Compute("SUM(AM_PO)", filter);
					dr["RT_DC_PO"] = dt.Compute("MAX(RT_DC_PO)", filter);
					dr["AM_SO"] = dt.Compute("SUM(AM_SO_BOM)", filter);
					dr["RT_DC_SO"] = dt.Compute("MAX(RT_DC_SO)", filter);
					dr["PROFIT"] = dt.Compute("SUM(PROFIT)", filter);
					수주원화금액 = D.GetDecimal(dt.Compute("SUM(AM_WONAMT)", filter));
					dr["RT_PROFIT"] = 수주원화금액 == 0 ? 0 : (D.GetDecimal(dr["PROFIT"]) / 수주원화금액 * 100);
					dr["NM_FG_PAYMENT"] = dt.Compute("MAX(NM_FG_PAYMENT)", filter);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return result;
		}

		private string 계약번호(string prefix, string 수주번호)
		{
			string 계약번호 = string.Empty;

			try
			{
				if (수주번호.Length <= 2)
					계약번호 = 수주번호;
				else if (수주번호.Substring(0, 2) == "14" || 수주번호.Substring(0, 2) == "15")
					계약번호 = 수주번호.Substring(2, 1) + prefix + 수주번호.Substring(1, 1) + 수주번호.Substring(3);
				else
					계약번호 = 수주번호.Substring(0, 1) + prefix + 수주번호.Substring(2);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return 계약번호;
		}
		#endregion
	}
}
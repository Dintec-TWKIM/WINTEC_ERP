using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;
using System.Drawing;

namespace cz
{
	public partial class P_CZ_SA_IV_SUB : Duzon.Common.Forms.CommonDialog
	{
		#region 전역변수 & 생성자
		private string 매출일자;
		private 수주관리.Config 수주Config;
		private string 통제설정;
		private P_CZ_SA_IV_SUB_BIZ _biz;
		private DataTable _dt;

		private string 사업자번호, 휴폐업구분, 수금예정일, 부가세계산법;

		public DataTable 출고데이터
		{
			get
			{
				return this._dt;
			}
		}

		public P_CZ_SA_IV_SUB(string 거래구분코드, string 거래구분명, string 매출일자)
		{
			try
			{
				InitializeComponent();

				this.수주Config = new 수주관리.Config();
				_biz = new P_CZ_SA_IV_SUB_BIZ();

				this.매출일자 = 매출일자;

				this.rbtn선적.Checked = true;

				this.txt거래구분.CodeValue = 거래구분코드;
				this.txt거래구분.CodeName = 거래구분명;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region 초기화
		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			#region Header
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

			this._flexH.BeginSetting(1, 1, false);

			this._flexH.SetCol("S", "선택", 35, true, CheckTypeEnum.Y_N);
			this._flexH.SetCol("YN_AUTO", "자동처리대상", 60);
			this._flexH.SetCol("NO_IO", "출고번호", 100);
			this._flexH.SetCol("DT_IO", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("DT_LOADING", "선적일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexH.SetCol("FG_TAX", "과세구분", 100);
			this._flexH.SetCol("NM_BIZAREA", "사업장", false);
			this._flexH.SetCol("CD_PLANT", "공장", false);
			this._flexH.SetCol("CD_PARTNER", "매출처", false);
			this._flexH.SetCol("LN_PARTNER", "매출처명", 100);
			this._flexH.SetCol("NO_IMO", "IMO번호", false);
			this._flexH.SetCol("NM_VESSEL", "호선명", 100);
			this._flexH.SetCol("AM_EX", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_TOT", "총금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_RCP_A", "선수금금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("DC_RMK", "비고", 200);

			this._flexH.SetDummyColumn(new string[] { "S" });
			this._flexH.EnabledHeaderCheck = false;
			this._flexH.ExtendLastCol = true;
			this._flexH.SettingVersion = "1.0.0.0";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexH.LoadUserCache("P_CZ_SA_IV_SUB_flexH");
			#endregion

			#region Line
			this._flexL.BeginSetting(1, 1, false);

			this._flexL.SetCol("S", "선택", 35, true, CheckTypeEnum.Y_N);
			this._flexL.SetCol("NO_SO", "수주번호", 80);
			this._flexL.SetCol("NO_DSP", "순번", 40);
			this._flexL.SetCol("NO_SOLINE", "수주항번", 40);
			this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
			this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 100);
			this._flexL.SetCol("CD_ITEM", "품목코드", 100);
			this._flexL.SetCol("NM_ITEM", "품목명", 120);
			this._flexL.SetCol("STND_ITEM", "규격", false);
			this._flexL.SetCol("UNIT_IM", "단위", 65);
			this._flexL.SetCol("QT_CLS_MM", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			if (this.txt거래구분.CodeValue == "001")
				this._flexL.SetCol("QT_GI_CLS", "적용수량", 80, true, typeof(decimal), FormatTpType.QUANTITY);
			else
				this._flexL.SetCol("QT_GI_CLS", "적용수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_CLS", "관리잔량", false);
			this._flexL.SetCol("QT_CLS_REMAIN", "마감잔량", false);
			this._flexL.SetCol("CD_EXCH", "통화명", 60);
			this._flexL.SetCol("RT_EXCH", "환율", 50);
			this._flexL.SetCol("UM_EX_CLS", "외화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexL.SetCol("UM_ITEM_CLS", "원화단가", 100, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexL.SetCol("AM_EX_CLS", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_CLS", "원화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("VAT", "부가세", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_TOT", "총금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexL.SetCol("AM_DISCOUNT", "할인금액", false);
			this._flexL.SetCol("NM_QTIOTP", "출고유형", 100);
			this._flexL.SetCol("NM_PROJECT", "프로젝트", 120);
			this._flexL.SetCol("NM_SO", "수주유형", 100);
			this._flexL.SetCol("FG_TRANS", "거래구분", false);
			this._flexL.SetCol("TP_IV", "매출형태", false);
			this._flexL.SetCol("CD_GI_PARTNER", "납품처코드", false);
			this._flexL.SetCol("GI_PARTNER", "납품처", 120);
			this._flexL.SetCol("NM_SALEGRP", "영업그룹", 100);
			this._flexL.SetCol("CD_CC", "C/C코드", false);
			this._flexL.SetCol("NM_CC", "C/C명", 100);
			this._flexL.SetCol("NM_KOR", "수주담당자", 80);
			this._flexL.SetCol("YN_PJTCREDIT", "특별여신여부", false);
			this._flexL.SetCol("G_DC_RMK", "비고(의뢰)", false);
			this._flexL.SetCol("G_DC_RMK2", "비고2(의뢰)", false);
			this._flexL.SetCol("NM_MNGD1", "관리내역1", false);
			this._flexL.SetCol("NM_MNGD2", "관리내역2", false);
			this._flexL.SetCol("NM_MNGD3", "관리내역3", false);
			this._flexL.SetCol("CD_MNGD4", "관리내역4", false);
			this._flexL.SetCol("NM_FG_UM_GI", "납품처단가유형", false);
			this._flexL.SetCol("DT_IO", "출고일자", false);
			this._flexL.SetCol("DT_LOADING", "선적일자", false);
			this._flexL.SetCol("DC_RMK", "비고", 150);

			if (Config.MA_ENV.YN_UNIT == "Y")
			{
				this._flexL.SetCol("CD_PJTLINE", "UNIT 항번", false);
				this._flexL.SetCol("CD_UNIT", "UNIT 코드", false);
				this._flexL.SetCol("NM_UNIT", "UNIT 명", false);
				this._flexL.SetCol("STND_UNIT", "UNIT 규격", false);
			}

			this._flexL.SetCol("TXT_USERDEF1", "수주라인사용자정의TEXT1", false);
			this._flexL.SetCol("TXT_USERDEF2", "수주라인사용자정의TEXT2", false);

			if (this.수주Config.부가세포함단가사용())
			{
				this._flexL.SetCol("TP_UM_TAX", "부가세여부", false);
				this._flexL.SetCol("UMVAT_IV", "부가세포함단가", false);
			}

			this._flexL.SetCol("CD_SL", "창고코드", false);
			this._flexL.SetCol("NM_SL", "창고명", false);

			this._flexL.SetDummyColumn(new string[] { "S" });
			this._flexL.EnabledHeaderCheck = false;
			this._flexL.SettingVersion = "1.0.0.0";
			this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			if (Config.MA_ENV.YN_UNIT == "Y")
			{
				this._flexL.SetExceptSumCol(new string[] { "UM_ITEM_CLS",
														   "CD_PJTLINE",
														   "UM_EX_CLS",
														   "UMVAT_IV" });
			}
			else
			{
				this._flexL.SetExceptSumCol(new string[] { "UM_ITEM_CLS",
														   "UM_EX_CLS",
														   "UMVAT_IV" });
			}

			this._flexL.LoadUserCache("P_CZ_SA_IV_SUB_flexL");
			#endregion
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			#region 숨김설정
			this.oneGrid2.ItemCollection.Remove(this.oneGridItem5);
			this.oneGrid2.Controls.Remove(this.oneGridItem5);

			this.oneGridItem6.ControlEdit.Remove(this.bpc매출처변경);

			this.oneGrid2.Height = 110;

			this.btn단가적용.Visible = false;
			#endregion

			this.통제설정 = BASIC.GetMAEXC("매출등록-총마감금액지정");

			this.dtp출고기간.Mask = Global.MainFrame.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
			this.dtp출고기간.StartDateToString = "20160101";
			this.dtp출고기간.EndDateToString = Global.MainFrame.GetStringToday;

			DataSet comboData = Global.MainFrame.GetComboData(new string[] { "S;SA_B000009",
																			 "S;MA_PLANT",
																			 "S;MA_B000040",
																			 "N;PU_C000016",
																			 "S;MA_AISPOSTH;100",
																			 "S;MA_B000005",
                                                                             "S;CZ_SA00032" });

			this.cbo할인구분.DataSource = comboData.Tables[0];
			this.cbo할인구분.DisplayMember = "NAME";
			this.cbo할인구분.ValueMember = "CODE";

			this.cbo통화명.DataSource = comboData.Tables[5];
			this.cbo통화명.DisplayMember = "NAME";
			this.cbo통화명.ValueMember = "CODE";

			this.cbo부대비용통화명.DataSource = comboData.Tables[5];
			this.cbo부대비용통화명.DisplayMember = "NAME";
			this.cbo부대비용통화명.ValueMember = "CODE";

            this.cbo협조내용.DataSource = comboData.Tables[6];
            this.cbo협조내용.DisplayMember = "NAME";
            this.cbo협조내용.ValueMember = "CODE";

			this.cbo품목조정구분.DataSource = MA.GetCodeUser(new string[] { "001", "002" }, new string[] { Global.MainFrame.DD("개별조정"), Global.MainFrame.DD("총액차감") });
			this.cbo품목조정구분.DisplayMember = "NAME";
			this.cbo품목조정구분.ValueMember = "CODE";

			this.cbo부대비용항목.DataSource = Global.MainFrame.FillDataTable("SELECT CD_ITEM, NM_ITEM, STND_ITEM, UNIT_IM, CLS_ITEM FROM MA_PITEM WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "' AND CD_ITEM LIKE 'SD%' ORDER BY CD_ITEM");
			this.cbo부대비용항목.DisplayMember = "NM_ITEM";
			this.cbo부대비용항목.ValueMember = "CD_ITEM";

			this._flexH.SetDataMap("CD_PLANT", comboData.Tables[1], "CODE", "NAME");
			this._flexH.SetDataMap("FG_TAX", comboData.Tables[2], "CODE", "NAME");
			this._flexL.SetDataMap("FG_TRANS", comboData.Tables[3], "CODE", "NAME");
			this._flexL.SetDataMap("TP_IV", comboData.Tables[4], "CODE", "NAME");
			this._flexL.SetDataMap("CD_EXCH", comboData.Tables[5], "CODE", "NAME");
		}

		private void InitEvent()
		{
			this.btn조회.Click += new EventHandler(this.btn조회_Click);
			this.btn전체선택.Click += new EventHandler(this.btn전체선택_Click);
			this.btn전체해제.Click += new EventHandler(this.btn전체해제_Click);
			this.btn단가적용.Click += new EventHandler(this.btn단가적용_Click);
			this.btn적용.Click += new EventHandler(this.btn적용_Click);
			this.btn취소.Click += new EventHandler(this.btn취소_Click);

			this.btn할인적용.Click += new EventHandler(this.btn할인적용_Click);
			this.btn품목금액조정.Click += new EventHandler(this.btn품목금액조정_Click);
			this.btn환율변경.Click += new EventHandler(this.btn환율변경_Click);
			this.btn매출처번경.Click += new EventHandler(this.btn매출처번경_Click);
			this.btn부대비용적용.Click += new EventHandler(this.btn부대비용적용_Click);
			this.btn부대비용제거.Click += new EventHandler(this.btn부대비용제거_Click);

			this.cur외화부대비용.Leave += new EventHandler(this.cur원화부대비용계산);
			this.cur부대비용환율.TextChanged += new EventHandler(this.cur원화부대비용계산);

			this.cbo부대비용수주번호.SelectionChangeCommitted += new EventHandler(this.SelectionChangeCommitted);
			this.cbo부대비용수주번호.DrawItem += new DrawItemEventHandler(this.cbo부대비용수주번호_DrawItem);
			this.cbo통화명.SelectionChangeCommitted += new EventHandler(this.SelectionChangeCommitted);

			this.ctx매출처.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx호선번호.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx호선번호.CodeChanged += new EventHandler(this.ctx호선번호_CodeChanged);

			this._flexH.AfterDataRefresh += new ListChangedEventHandler(this._flexH_AfterDataRefresh);
			this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
			this._flexH.StartEdit += new RowColEventHandler(this._flex_StartEdit);
			this._flexH.AfterEdit += new RowColEventHandler(this._flexH_AfterEdit);

			this._flexL.StartEdit += new RowColEventHandler(this._flex_StartEdit);
			this._flexL.AfterEdit += new RowColEventHandler(this._flexL_AfterEdit);
			this._flexL.ValidateEdit += _flexL_ValidateEdit;
		}

		private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			decimal num1, num2, num3, num4, num8, num9, num10, num12, num13;

			try
			{
				string str = ((C1FlexGridBase)sender).GetData(e.Row, e.Col).ToString();
				string editData = ((FlexGrid)sender).EditData;

				if (str.ToUpper() == editData.ToUpper()) return;

				bool flag = true;

				switch (this._flexL.Cols[e.Col].Name)
				{
					case "QT_GI_CLS":
						if (D.GetString(this._flexL["YN_RETURN"]) == "Y")
						{
							if (D.GetDecimal(editData) > 0)
							{
								Global.MainFrame.ShowMessage("CZ_@ 에 양수를 입력 할 수 없습니다.", this._flexL.Cols[e.Col].Caption);
								flag = false;
								break;
							}
							break;
						}
						if (D.GetDecimal(editData) < 0)
						{
							Global.MainFrame.ShowMessage("CZ_@ 에 음수를 입력 할 수 없습니다.", this._flexL.Cols[e.Col].Caption);
							flag = false;
						}
						break;
				}

				if (!flag)
				{
					this._flexL[this._flexL.Cols[e.Col].Name] = D.GetDecimal(str);
					e.Cancel = true;
				}
				else
				{
					num1 = this.과세적용(this._flexL[e.Row, "FG_TAX"].ToString(), this._flexL[e.Row, "FG_TAX_VAT"].ToString(), D.GetDecimal(this._flexL[e.Row, "RT_VAT"]));

					switch (this._flexL.Cols[e.Col].Name)
					{
						case "QT_GI_CLS":
							if (this._flexL[e.Row, "YN_RETURN"].ToString() == "Y")
							{
								if (D.GetDecimal(editData) < D.GetDecimal(this._flexL[e.Row, "QT_CLS_MM"]))
								{
									string 마감관리수량 = (Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(editData)) - Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[e.Row, "QT_CLS_MM"]))).ToString();
									string 수량한도 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[e.Row, "QT_CLS_MM"])).ToString();

									Global.MainFrame.ShowMessage("마감관리수량을 @ 초과하였습니다. 마감가능 수량한도는 @  입니다", new string[] { 마감관리수량, 수량한도 });
									this._flexL[e.Row, "QT_GI_CLS"] = D.GetDecimal(str);
									break;
								}
							}
							else if (D.GetDecimal(editData) > D.GetDecimal(this._flexL[e.Row, "QT_CLS_MM"]))
							{
								string 마감관리수량 = (Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(editData)) - Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[e.Row, "QT_CLS_MM"]))).ToString();
								string 수량한도 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[e.Row, "QT_CLS_MM"])).ToString();

								Global.MainFrame.ShowMessage("마감관리수량을 @ 초과하였습니다. 마감가능 수량한도는 @  입니다", new string[] { 마감관리수량, 수량한도 });
								this._flexL[e.Row, "QT_GI_CLS"] = D.GetDecimal(str);
								break;
							}

							num12 = D.GetDecimal(this._flexL[e.Row, "AM_CLS"]);
							num13 = D.GetDecimal(this._flexL[e.Row, "VAT"]);

							if (D.GetString(this._flexL["TP_UM_TAX"]) != "Y")
							{
								this._flexL[e.Row, "AM_CLS"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(editData) * D.GetDecimal(this._flexL[e.Row, "UM_ITEM_CLS"]));
								this._flexL[e.Row, "VAT"] = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[e.Row, "AM_CLS"]) * num1);
								this._flexL[e.Row, "AM_EX_CLS"] = Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(editData) * D.GetDecimal(this._flexL[e.Row, "UM_EX_CLS"]));
								this._flexL[e.Row, "AM_TOT"] = D.GetDecimal(this._flexL[e.Row, "AM_CLS"]) + D.GetDecimal(this._flexL[e.Row, "VAT"]);
							}
							else
							{
								num2 = Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(editData) * D.GetDecimal(this._flexL["UMVAT_IV"]));
								this._flexL["AM_CLS"] = Global.MainFrame.LoginInfo.CompanyLanguage != Language.KR ? D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, num2 * 100 / (100 + D.GetDecimal(this._flexL["RT_VAT"])))) : Decimal.Round(num2 * 100 / (100 + D.GetDecimal(this._flexL["RT_VAT"])), MidpointRounding.AwayFromZero);
								this._flexL["VAT"] = (num2 - D.GetDecimal(this._flexL["AM_CLS"]));
								this._flexL["AM_EX_CLS"] = (D.GetDecimal(this._flexL["RT_EXCH"]) == 0 ? 0 : Unit.외화금액(DataDictionaryTypes.SA, D.GetDecimal(this._flexL["AM_CLS"]) / D.GetDecimal(this._flexL["RT_EXCH"])));
								this._flexL["AM_TOT"] = D.GetDecimal(this._flexL["AM_CLS"]) + D.GetDecimal(this._flexL["VAT"]);
							}

							if (this._flexL[e.Row, "S"].ToString() == "Y")
							{
								D.GetDecimal(this._flexL["AM_CLS"]);
								this.cur원화합계.DecimalValue += D.GetDecimal(this._flexL["AM_CLS"]) - num12;
								this.cur부가세합계.DecimalValue += D.GetDecimal(this._flexL["AM_CLS"]) * num1 - num13;
								this.cur총합계.DecimalValue = this.cur원화합계.DecimalValue + this.cur부가세합계.DecimalValue;
							}

							if (this._flexL[e.Row, "S"].ToString() == "Y")
								this.cur총수량.DecimalValue -= Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[e.Row, "QT_CLS"]));

							this._flexL[e.Row, "QT_CLS"] = !(D.GetDecimal(this._flexL[e.Row, "UNIT_SO_FACT"]) == 0) ? Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(editData) * D.GetDecimal(this._flexL[e.Row, "UNIT_SO_FACT"])) : (D.GetDecimal(editData) * 1);

							if (this._flexL[e.Row, "S"].ToString() == "Y")
								this.cur총수량.DecimalValue += Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(this._flexL[e.Row, "QT_CLS"]));

							this._flexL.FinishEditing();

							if (this._flexL[e.Row, "S"].ToString() == "Y")
							{
								foreach (DataRow dataRow1 in this._flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows))
								{
									num2 = 0;
									num3 = 0;
									num4 = 0;

									string[] strArray = new string[] { "S = 'Y' AND NO_IO = '", dataRow1["NO_IO"].ToString(), "' AND FG_TAX = '", D.GetString(dataRow1["FG_TAX"]), "'" };
									foreach (DataRow dataRow2 in this._flexL.DataTable.Select(string.Concat(strArray), "", DataViewRowState.CurrentRows))
									{
										num2 += D.GetDecimal(dataRow2["AM_CLS"]);
										num3 += Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow2["VAT"]));
										num4 += D.GetDecimal(dataRow2["AM_CLS"]) + Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow2["VAT"]));
									}

									dataRow1["AM"] = num2;
									dataRow1["VAT"] = num3;
									dataRow1["TOT_AM"] = num4;
								}
								break;
							}

							this._flexL.FinishEditing();
							DataRow[] dataRowArray2 = this._flexL.DataTable.Select("NO_IO = '" + this._flexH["NO_IO"].ToString() + "' AND FG_TAX = '" + D.GetString(this._flexH["FG_TAX"]) + "'", "", DataViewRowState.CurrentRows);

							num8 = 0;
							num9 = 0;
							num10 = 0;

							foreach (DataRow dataRow in dataRowArray2)
							{
								num8 += D.GetDecimal(dataRow["AM_CLS"]);
								num9 += Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["VAT"]));
								num10 += D.GetDecimal(dataRow["AM_CLS"]) + Unit.원화금액(DataDictionaryTypes.SA, D.GetDecimal(dataRow["VAT"]));
							}

							this._flexH["AM"] = num8;
							this._flexH["VAT"] = num9;
							this._flexH["TOT_AM"] = num10;
							this._flexH.FinishEditing();
							break;
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private decimal 과세적용(string 과세구분, string FG_TAX_VAT, decimal RT_VAT)
		{
			return !(과세구분 == "11") ? (!(과세구분 == "12") ? (!(과세구분 == "13") ? 0 : Convert.ToDecimal(RT_VAT) / 100) : Convert.ToDecimal(FG_TAX_VAT) / 100) : Convert.ToDecimal(FG_TAX_VAT) / 100;
		}
		#endregion

		#region 버튼 이벤트
		private bool SearchCondition()
		{
			if (string.IsNullOrEmpty(this.dtp출고기간.StartDateToString) || string.IsNullOrEmpty(this.dtp출고기간.EndDateToString))
			{
				Global.MainFrame.ShowMessage(공통메세지._은는필수입력항목입니다, new string[] { this.lbl출고기간.Text });
				this.dtp출고기간.Focus();
				return false;
			}

			return true;
		}

		private void btn조회_Click(object sender, EventArgs e)
		{
			string 선적유무;

			try
			{
				if (!this.SearchCondition()) return;

				if (this.rbtn전체.Checked == true)
					선적유무 = "A";
				else if (this.rbtn선적.Checked == true)
					선적유무 = "Y";
				else
					선적유무 = "N";

				DataTable dataTable = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	  this.txt출고번호.Text,
																	  this.dtp출고기간.StartDateToString,
																	  this.dtp출고기간.EndDateToString,
																	  this.txt거래구분.CodeValue,
																	  this.ctx매출처.CodeValue,
																	  this.txt프로젝트.Text,
																	  this.ctx수주담당자.CodeValue,
																	  this.txt수주번호.Text,
																	  this.ctx납품처.CodeValue,
																	  this.ctx호선번호.CodeValue,
																	  선적유무 });

				this._flexH.Binding = dataTable;

				if (!this._flexH.HasNormalRow)
					Global.MainFrame.ShowMessage(공통메세지.조건에해당하는내용이없습니다);

				this.선택금액계산();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn단가적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

				DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

				if (dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					DataTable dataTable1 = this._flexL.DataTable.Clone();

					foreach (DataRow row in dataRowArray)
					{
						dataTable1.ImportRow(row);
					}

					dataTable1.AcceptChanges();
					DataTable dataTable2 = dataTable1.DefaultView.ToTable(true, "CD_PLANT", "CD_PARTNER", "CD_SALEGRP", "TP_PRICE", "CD_EXCH");
					string str1 = string.Empty;
					string str2 = string.Empty;
					string str3 = string.Empty;
					string str4 = string.Empty;
					string str5 = string.Empty;
					string str6 = string.Empty;
					string str7 = string.Empty;
					string str8 = string.Empty;

					foreach (DataRow dataRow1 in dataTable2.Rows)
					{
						string string1 = D.GetString(dataRow1["CD_PLANT"]);
						string string2 = D.GetString(dataRow1["CD_PARTNER"]);
						string string3 = D.GetString(dataRow1["CD_SALEGRP"]);
						string string4 = D.GetString(dataRow1["TP_PRICE"]);
						string string5 = D.GetString(dataRow1["CD_EXCH"]);
						string filterExpression1 = "CD_PLANT   = '" + string1 + "' AND CD_PARTNER = '" + string2 + "' AND CD_SALEGRP = '" + string3 + "' AND TP_PRICE   = '" + string4 + "' AND CD_EXCH    = '" + string5 + "'";

						foreach (DataRow dataRow2 in dataTable1.Select(filterExpression1))
						{
							str1 = str1 + D.GetString(dataRow2["CD_ITEM"]) + "|";
						}

						foreach (DataRow dataRow2 in BASIC.GetUMMulti(string1, str1, string2, string3, this.매출일자, string4, string5, "002").Rows)
						{
							string string6 = D.GetString(dataRow2["CD_PLANT"]);
							string string7 = D.GetString(dataRow2["CD_ITEM"]);
							string string8 = D.GetString(dataRow2["CD_PARTNER"]);
							string string9 = D.GetString(dataRow2["CD_SALEGRP"]);
							string string10 = D.GetString(dataRow2["TP_PRICE"]);
							string string11 = D.GetString(dataRow2["CD_EXCH"]);
							DataTable dataTable3 = this._flexL.DataTable;
							string filterExpression2 = "S = 'Y' AND CD_PLANT = '" + string6 + "' AND CD_ITEM = '" + string7 + "' AND CD_PARTNER = '" + string8 + "' AND CD_SALEGRP = '" + string9 + "' AND TP_PRICE = '" + string10 + "' AND CD_EXCH = '" + string11 + "'";

							foreach (DataRow dataRow3 in dataTable3.Select(filterExpression2))
							{
								int num2;

								num2 = !(D.GetString(dataRow3["FG_VAT"]) == "Y") ? 1 : 0;

								if (num2 == 0)
								{
									dataRow3["UMVAT_IV"] = D.GetDecimal(dataRow2["UM"]);
									Decimal num3 = this.원화계산(D.GetDecimal(dataRow3["QT_GI_CLS"]) * D.GetDecimal(dataRow3["UMVAT_IV"]));
									dataRow3["AM_CLS"] = this.원화계산(num3 * 100 / (100 + D.GetDecimal(dataRow3["RT_VAT"])));
									dataRow3["VAT"] = (num3 - D.GetDecimal(dataRow3["AM_CLS"]));
									dataRow3["UM_ITEM_CLS"] = this.원화계산(D.GetDecimal(dataRow3["AM_CLS"]) / D.GetDecimal(dataRow3["QT_GI_CLS"]));
									dataRow3["AM_EX_CLS"] = (D.GetDecimal(dataRow3["RT_EXCH"]) == 0 ? 0 : this.외화계산(D.GetDecimal(dataRow3["AM_CLS"]) / D.GetDecimal(dataRow3["RT_EXCH"])));
									dataRow3["UM_EX_CLS"] = this.외화계산(D.GetDecimal(dataRow3["AM_EX_CLS"]) / D.GetDecimal(dataRow3["QT_GI_CLS"]));
								}
								else
								{
									dataRow3["UM_EX_CLS"] = D.GetDecimal(dataRow2["UM"]);
									dataRow3["AM_EX_CLS"] = this.외화계산(D.GetDecimal(dataRow3["QT_GI_CLS"]) * D.GetDecimal(dataRow2["UM"]));
									dataRow3["AM_CLS"] = this.원화계산(D.GetDecimal(dataRow3["AM_EX_CLS"]) * (D.GetDecimal(dataRow3["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(dataRow3["RT_EXCH"])));
									dataRow3["VAT"] = this.원화계산(D.GetDecimal(dataRow3["AM_CLS"]) * D.GetDecimal(dataRow3["RT_VAT"]) * new Decimal(1, 0, 0, false, 2));
									dataRow3["UM_ITEM_CLS"] = this.원화계산(D.GetDecimal(dataRow3["AM_CLS"]) / D.GetDecimal(dataRow3["QT_GI_CLS"]));
									Decimal num3 = D.GetDecimal(dataRow3["AM_CLS"]) + D.GetDecimal(dataRow3["VAT"]);
									dataRow3["UMVAT_IV"] = this.원화계산(num3 / D.GetDecimal(dataRow3["QT_GI_CLS"]));
								}
							}
						}
					}

					foreach (DataRow dataRow in dataTable1.DefaultView.ToTable(1 != 0, new string[2] { "NO_IO", "FG_TAX" }).Rows)
					{
						this.SetHeaderAmt(D.GetString(dataRow["NO_IO"]), D.GetString(dataRow["FG_TAX"]));
					}

					this._flexL.SumRefresh();
					this._flexH.SumRefresh();
					this.선택금액계산();
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flexL.HasNormalRow) return;

				DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (this._flexL.DataTable.Select("S = 'Y' AND ISNULL(DT_LOADING, '') = '' AND (FG_TRANS = '005' OR RT_EXCH < 1)").Length > 0)
				{
					Global.MainFrame.ShowMessage("CZ_@ 이(가) 등록되지 않았습니다.", Global.MainFrame.DD("선적일자"));
					return;
				}
				else
				{
					this._dt = this._flexL.DataTable.Clone();
					
					foreach (DataRow row in dataRowArray)
					{
						if (((DataTable)this.cbo부대비용항목.DataSource).Select("CD_ITEM = '" + row["CD_ITEM"] + "'").Length > 0 && 
							(row.RowState == DataRowState.Added || row["YN_AUTO_CHARGE"].ToString() == "Y"))
						{
							row["QT_CLS_MM"] = 0;
							row["QT_GI_CLS"] = 0;
							row["QT_CLS"] = 0;
							row["QT_CLS_REMAIN"] = 0;
						}

						if (this.chk라인비고적용.Checked == false)
						{
							row["DC_RMK"] = "";
							row["DC_REMARK"] = "";
						}

						row.AcceptChanges();

						this._dt.ImportRow(row);
					}

					string 비고 = string.Empty;
					DataRow dr1;

					foreach(DataRow dr in ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_IO" }, true).Rows)
					{
						dr1 = this._flexH.DataTable.Select("NO_IO = '" + dr["NO_IO"].ToString() + "'")[0];

						if (!string.IsNullOrEmpty(dr1["DC_RMK_GIR"].ToString()))
							비고 += dr1["DC_RMK_GIR"].ToString() + Environment.NewLine;

						if (!string.IsNullOrEmpty(dr1["DC_RMK_SO"].ToString()))
							비고 += dr1["DC_RMK_SO"].ToString() + Environment.NewLine;
					}

					if (!string.IsNullOrEmpty(비고))
						Global.MainFrame.ShowDetailMessage("선택한 건에 비고가 등록되어 있습니다." + Environment.NewLine + Environment.NewLine + "[자세히] 버튼을 눌러 내역을 확인하세요.", 비고);

					this._flexH.SaveUserCache("P_CZ_SA_IV_SUB_flexH");
					this._flexL.SaveUserCache("P_CZ_SA_IV_SUB_flexL");

					this.DialogResult = System.Windows.Forms.DialogResult.OK;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn취소_Click(object sender, EventArgs e)
		{
			this._flexH.SaveUserCache("P_CZ_SA_IV_SUB_flexH");
			this._flexL.SaveUserCache("P_CZ_SA_IV_SUB_flexL");

			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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

		private void btn할인적용_Click(object sender, EventArgs e)
		{
			string filter;

			try
			{
				if (this._flexH.Rows.Count - 1 == 0)
				{
					Global.MainFrame.ShowMessage("먼저조회를하십시요!");
				}
				else if (this.cbo할인구분.SelectedValue == null || this.cbo할인구분.SelectedValue.ToString() == string.Empty)
				{
					Global.MainFrame.ShowMessage(공통메세지._자료가선택되어있지않습니다, new string[] { this.lbl할인구분.Text });
					this.cbo할인구분.Focus();
				}
				else
				{
					DataRow[] dataRowArray = this._flexL.DataTable.Select("S = 'Y'");

					if (dataRowArray == null || dataRowArray.Length == 0)
					{
						Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					}
					else
					{
						if (D.GetString(this.cbo할인구분.SelectedValue) == "001")
						{
							foreach (DataRow dataRow in dataRowArray)
							{
								dataRow["AM_DISCOUNT"] = (D.GetDecimal(dataRow["AM_DISCOUNT"]) + this.외화계산(D.GetDecimal((D.GetDecimal(dataRow["AM_EX_CLS"]) * this.cur할인구분.DecimalValue) / 100)));
								dataRow["AM_EX_CLS"] = this.외화계산(D.GetDecimal(dataRow["AM_EX_CLS"]) - D.GetDecimal((D.GetDecimal(dataRow["AM_EX_CLS"]) * this.cur할인구분.DecimalValue) / 100));
								dataRow["UM_EX_CLS"] = this.외화계산(D.GetDecimal(D.GetDecimal(dataRow["AM_EX_CLS"]) / D.GetDecimal(dataRow["QT_CLS_MM"])));
								dataRow["AM_CLS"] = this.원화계산(D.GetDecimal(dataRow["AM_EX_CLS"]) * (D.GetDecimal(dataRow["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(dataRow["RT_EXCH"])));
								dataRow["VAT"] = this.원화계산(D.GetDecimal((D.GetDecimal(dataRow["AM_CLS"]) * D.GetDecimal(dataRow["RT_VAT"])) / 100));
								dataRow["UM_ITEM_CLS"] = this.원화계산(D.GetDecimal(D.GetDecimal(dataRow["AM_CLS"]) / D.GetDecimal(dataRow["QT_CLS_MM"])));
								dataRow["AM_TOT"] = (D.GetDecimal(dataRow["AM_CLS"]) + D.GetDecimal(dataRow["VAT"]));
							}

							foreach (DataRow dataRow1 in this._flexH.DataTable.Select("S = 'Y'"))
							{
								filter = "S = 'Y' AND NO_IO = '" + D.GetString(dataRow1["NO_IO"]) + "'";
								dataRow1["AM_EX"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_CLS)", filter));
								dataRow1["AM"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLS)", filter));
								dataRow1["VAT"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(VAT)", filter));
								dataRow1["AM_TOT"] = (D.GetDecimal(dataRow1["AM"]) + D.GetDecimal(dataRow1["VAT"]));
							}
						}
						else if (this.cbo할인구분.SelectedValue.ToString() == "002")
						{
							foreach (DataRow dataRow in dataRowArray)
							{
								dataRow["AM_DISCOUNT"] = (D.GetDecimal(dataRow["AM_DISCOUNT"]) - this.외화계산(D.GetDecimal((D.GetDecimal(dataRow["AM_EX_CLS"]) * this.cur할인구분.DecimalValue) / 100)));
								dataRow["AM_EX_CLS"] = this.외화계산(D.GetDecimal(dataRow["AM_EX_CLS"]) + D.GetDecimal((D.GetDecimal(dataRow["AM_EX_CLS"]) * this.cur할인구분.DecimalValue) / 100));
								dataRow["UM_EX_CLS"] = this.외화계산(D.GetDecimal(D.GetDecimal(dataRow["AM_EX_CLS"]) / D.GetDecimal(dataRow["QT_CLS_MM"])));
								dataRow["AM_CLS"] = this.원화계산(D.GetDecimal(dataRow["AM_EX_CLS"]) * (D.GetDecimal(dataRow["RT_EXCH"]) == 0 ? 1 : D.GetDecimal(dataRow["RT_EXCH"])));
								dataRow["VAT"] = this.원화계산(D.GetDecimal((D.GetDecimal(dataRow["AM_CLS"]) * D.GetDecimal(dataRow["RT_VAT"])) / 100));
								dataRow["UM_ITEM_CLS"] = this.원화계산(D.GetDecimal(D.GetDecimal(dataRow["AM_CLS"]) / D.GetDecimal(dataRow["QT_CLS_MM"])));
								dataRow["AM_TOT"] = (D.GetDecimal(dataRow["AM_CLS"]) + D.GetDecimal(dataRow["VAT"]));
							}

							foreach (DataRow dataRow1 in this._flexH.DataTable.Select("S = 'Y'"))
							{
								filter = "S = 'Y' AND NO_IO = '" + D.GetString(dataRow1["NO_IO"]) + "'";
								dataRow1["AM_EX"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_CLS)", filter));
								dataRow1["AM"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLS)", filter));
								dataRow1["VAT"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(VAT)", filter));
								dataRow1["AM_TOT"] = (D.GetDecimal(dataRow1["AM"]) + D.GetDecimal(dataRow1["VAT"]));
							}
						}
					}

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

		private void btn품목금액조정_Click(object sender, EventArgs e)
		{
			string filter;

			try
			{
				if (!this._flexL.HasNormalRow) return;

				DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'");
				
				if (!this._flexH.HasNormalRow && !this._flexL.HasNormalRow) return;

				if (this.cbo품목조정구분.SelectedValue == null)
				{
					Global.MainFrame.ShowMessage("품목조정구분을 선택하십시요");
				}
				else if (string.IsNullOrEmpty(D.GetString(this.ctx품목.CodeValue)))
				{
					Global.MainFrame.ShowMessage("품목을 선택하십시요");
				}
				else if (this.cbo품목조정구분.SelectedValue.ToString() == "001")
				{
					foreach (DataRow dataRow1 in dataRowArray)
					{
						string filterExpression = "S = 'Y' AND NO_IO = '" + dataRow1["NO_IO"].ToString() + "' AND CD_ITEM = '" + D.GetString(this.ctx품목.CodeValue) + "'";

						foreach (DataRow dataRow2 in this._flexL.DataTable.Select(filterExpression))
						{
							//if (D.GetDecimal(dataRow2["RT_VAT"].ToString()) == 0)
							//    dataRow2["RT_VAT"] = 1;

							dataRow2["UM_ITEM_CLS"] = this.cur단가.DecimalValue;
							dataRow2["AM_CLS"] = this.원화계산(D.GetDecimal(dataRow2["QT_GI_CLS"]) * D.GetDecimal(dataRow2["UM_ITEM_CLS"]));
							dataRow2["VAT"] = this.원화계산((D.GetDecimal(dataRow2["AM_CLS"]) * D.GetDecimal(dataRow2["RT_VAT"])) / 100);
							dataRow2["UM_EX_CLS"] = this.외화계산(D.GetDecimal(dataRow2["UM_ITEM_CLS"]) / D.GetDecimal(dataRow2["RT_EXCH"]));
							dataRow2["AM_EX_CLS"] = this.외화계산(D.GetDecimal(dataRow2["QT_GI_CLS"]) * D.GetDecimal(dataRow2["UM_EX_CLS"]));
							dataRow2["AM_TOT"] = (D.GetDecimal(dataRow2["AM_CLS"]) + D.GetDecimal(dataRow2["VAT"]));
						}

						filter = "S = 'Y' AND NO_IO = '" + D.GetString(dataRow1["NO_IO"]) + "'";
						dataRow1["AM_EX"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_CLS)", filter));
						dataRow1["AM"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLS)", filter));
						dataRow1["VAT"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(VAT)", filter));
						dataRow1["AM_TOT"] = (D.GetDecimal(dataRow1["AM"]) + D.GetDecimal(dataRow1["VAT"]));
					}

					this._flexH.SumRefresh();
					this._flexL.SumRefresh();
					this.선택금액계산();
				}
				else if (this.cbo품목조정구분.SelectedValue.ToString() == "002")
				{
					decimal 단가 = this.cur단가.DecimalValue;

					foreach (DataRow dataRow1 in dataRowArray)
					{
						string filterExpression = "NO_IO = '" + dataRow1["NO_IO"].ToString() + "' AND CD_ITEM = '" + D.GetString(this.ctx품목.CodeValue) + "'";

						foreach (DataRow dataRow2 in this._flexL.DataTable.Select(filterExpression))
						{
							if (단가 > 0)
							{
								//if (D.GetDecimal(dataRow2["RT_VAT"].ToString()) == 0) 
								//dataRow2["RT_VAT"] = 1;

								if (D.GetDecimal(dataRow2["AM_CLS"]) < 단가)
								{
									dataRow2["AM_DISCOUNT"] = D.GetDecimal(dataRow2["AM_CLS"]);
									단가 -= D.GetDecimal(dataRow2["AM_CLS"]);
									dataRow2["AM_CLS"] = 0;
								}
								else if (D.GetDecimal(dataRow2["AM_CLS"]) >= 단가)
								{
									dataRow2["AM_DISCOUNT"] = 단가;
									dataRow2["AM_CLS"] = (D.GetDecimal(dataRow2["AM_CLS"]) - 단가);
									단가 = 0;
								}

								dataRow2["UM_ITEM_CLS"] = this.원화계산(D.GetDecimal(dataRow2["AM_CLS"]) / D.GetDecimal(dataRow2["QT_GI_CLS"]));
								dataRow2["VAT"] = this.원화계산((D.GetDecimal(dataRow2["AM_CLS"]) * D.GetDecimal(dataRow2["RT_VAT"])) / 100);
								dataRow2["UM_EX_CLS"] = this.외화계산(D.GetDecimal(dataRow2["UM_ITEM_CLS"]) / D.GetDecimal(dataRow2["RT_EXCH"]));
								dataRow2["AM_EX_CLS"] = this.외화계산(D.GetDecimal(dataRow2["QT_GI_CLS"]) * D.GetDecimal(dataRow2["UM_EX_CLS"]));
								dataRow2["AM_TOT"] = (D.GetDecimal(dataRow2["AM_CLS"]) + D.GetDecimal(dataRow2["VAT"]));
							}
							else
								break;
						}

						filter = "S = 'Y' AND NO_IO = '" + D.GetString(dataRow1["NO_IO"]) + "'";
						dataRow1["AM_EX"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_CLS)", filter));
						dataRow1["AM"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLS)", filter));
						dataRow1["VAT"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(VAT)", filter));
						dataRow1["AM_TOT"] = (D.GetDecimal(dataRow1["AM"]) + D.GetDecimal(dataRow1["VAT"]));
					}

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

		private void btn환율변경_Click(object sender, EventArgs e)
		{
			string filter;

			try
			{
				if (this._flexH.Rows.Count - 1 == 0)
				{
					Global.MainFrame.ShowMessage("먼저조회를하십시요!");
				}
				else
				{
					DataRow[] dataRowArray = this._flexH.DataTable.Select("S = 'Y'");

					if (dataRowArray == null || dataRowArray.Length == 0)
					{
						Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					}
					else if (this.cur환율.DecimalValue == 0)
					{
						Global.MainFrame.ShowMessage("환율이 '0'입니다");
						this.cur환율.Focus();
					}
					else
					{
						foreach (DataRow dataRow1 in dataRowArray)
						{
							foreach (DataRow dataRow2 in this._flexL.DataTable.Select("S = 'Y' AND NO_IO = '" + dataRow1["NO_IO"].ToString() + "'"))
							{
								dataRow2["CD_EXCH"] = this.cbo통화명.SelectedValue;
								dataRow2["RT_EXCH"] = this.cur환율.DecimalValue;
								dataRow2["UM_ITEM_CLS"] = this.원화계산(D.GetDecimal(dataRow2["UM_EX_CLS"]) * D.GetDecimal(dataRow2["RT_EXCH"]));
								dataRow2["AM_CLS"] = this.원화계산(D.GetDecimal(dataRow2["AM_EX_CLS"]) * D.GetDecimal(dataRow2["RT_EXCH"]));
								dataRow2["VAT"] = this.원화계산((D.GetDecimal(dataRow2["AM_CLS"]) * D.GetDecimal(dataRow2["RT_VAT"])) / 100);
								dataRow2["AM_TOT"] = (D.GetDecimal(dataRow2["AM_CLS"]) + D.GetDecimal(dataRow2["VAT"]));
							}

							filter = "S = 'Y' AND NO_IO = '" + D.GetString(dataRow1["NO_IO"]) + "'";
							dataRow1["AM_EX"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_CLS)", filter));
							dataRow1["AM"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLS)", filter));
							dataRow1["VAT"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(VAT)", filter));
							dataRow1["AM_TOT"] = (D.GetDecimal(dataRow1["AM"]) + D.GetDecimal(dataRow1["VAT"]));
						}

						this._flexH.SumRefresh();
						this._flexL.SumRefresh();
						this.선택금액계산();
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn매출처번경_Click(object sender, EventArgs e)
		{
			if (!this._flexH.HasNormalRow || !this._flexL.HasNormalRow) return;

			DataRow[] dataRowArray1 = this._flexH.DataTable.Select("S = 'Y'");
			DataRow[] dataRowArray2 = this._flexL.DataTable.Select("S = 'Y'");

			if (dataRowArray1.Length == 0)
			{
				Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
			}
			else if (dataRowArray2.Length == 0)
			{
				Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
			}
			else
			{
				this._flexH.DataTable.Clone();
				this._flexL.DataTable.Clone();

				foreach (DataRow dataRow in dataRowArray1)
				{
					dataRow["LN_PARTNER"] = this.ctx매출처변경.CodeName;
					dataRow["CD_PARTNER"] = this.ctx매출처변경.CodeValue;
				}

				foreach (DataRow dataRow in dataRowArray2)
				{
					dataRow["LN_PARTNER"] = this.ctx매출처변경.CodeName;
					dataRow["CD_PARTNER"] = this.ctx매출처변경.CodeValue;
					dataRow["NO_BIZAREA"] = this.사업자번호;
					dataRow["CD_CON"] = this.휴폐업구분;

					if (string.IsNullOrEmpty(this.수금예정일)) this.수금예정일 = "0";

					dataRow["DT_RCP_PREARRANGED"] = int.Parse(this.수금예정일);
					dataRow["TP_SUMTAX"] = this.부가세계산법;
				}
			}
		}

		private void btn부대비용적용_Click(object sender, EventArgs e)
		{
			DataRow dr출고항목, dr부대비용항목, dr부대비용;
			string filter;

			try
			{
				if (!this._flexL.HasNormalRow) return;
				if (!this._flexH.HasNormalRow && !this._flexL.HasNormalRow) return;

				filter = "NO_IO = '" + this._flexH["NO_IO"].ToString() + "' AND FG_TAX = '" + this._flexH["FG_TAX"].ToString() + "'";
				filter += " AND NO_SO = '" + D.GetString(this.cbo부대비용수주번호.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(this.cbo부대비용항목.SelectedValue) + "'";

				if (this._flexL.DataTable.Select(filter).Length > 0)
				{
					#region 부대비용수정
					dr부대비용 = this._flexL.DataTable.Select(filter)[0];

					dr부대비용["UM_EX_CLS"] = this.외화계산(this.cur외화부대비용.DecimalValue);
					dr부대비용["AM_EX_CLS"] = this.외화계산(D.GetDecimal(dr부대비용["QT_GI_CLS"]) * D.GetDecimal(dr부대비용["UM_EX_CLS"]));
					dr부대비용["UM_ITEM_CLS"] = this.원화계산(D.GetDecimal(dr부대비용["UM_EX_CLS"]) * D.GetDecimal(dr부대비용["RT_EXCH"]));
					dr부대비용["AM_CLS"] = this.원화계산(D.GetDecimal(dr부대비용["QT_GI_CLS"]) * D.GetDecimal(dr부대비용["UM_ITEM_CLS"]));

					if (D.GetDecimal(dr부대비용["RT_VAT"]) != 0)
						dr부대비용["VAT"] = this.원화계산((D.GetDecimal(dr부대비용["AM_CLS"]) * D.GetDecimal(dr부대비용["RT_VAT"])) / 100);
					else
						dr부대비용["VAT"] = 0;

					dr부대비용["AM_TOT"] = (D.GetDecimal(dr부대비용["AM_CLS"]) + D.GetDecimal(dr부대비용["VAT"]));
					#endregion
				}
				else
				{
					#region 부대비용추가
					filter = "NO_IO = '" + this._flexH["NO_IO"].ToString() + "' AND FG_TAX = '" + this._flexH["FG_TAX"].ToString() + "'";
					filter += " AND NO_SO = '" + D.GetString(this.cbo부대비용수주번호.SelectedValue) + "'";

					if (this._flexL.DataTable.Select(filter).Length <= 0)
					{
                        Global.MainFrame.ShowMessage(공통메세지._이가존재하지않습니다, "과세구분 " + this._flexH["FG_TAX"].ToString());
						return;
					}

					filter += " AND NO_IOLINE = '" + D.GetString(this._flexL.DataTable.Compute("MAX(NO_IOLINE)", filter)) + "'";

					dr출고항목 = this._flexL.DataTable.Select(filter)[0];
					dr부대비용항목 = ((DataRowView)this.cbo부대비용항목.SelectedItem).Row;

					this._flexL.Rows.Add();

					this._flexL.Row = this._flexL.Rows.Count - 1;

					this._flexL["S"] = "Y";
					this._flexL["NO_IO"] = this._flexH["NO_IO"];
					this._flexL["NO_IOLINE"] = D.GetString(dr출고항목["NO_IOLINE"]);
					this._flexL["DT_IO"] = this._flexH["DT_IO"];
					this._flexL["FG_TAX"] = this._flexH["FG_TAX"];
					this._flexL["FG_TAX_VAT"] = D.GetString(dr출고항목["FG_TAX_VAT"]);

					this._flexL["NO_SO"] = D.GetString(this.cbo부대비용수주번호.SelectedValue);
					this._flexL["NO_SOLINE"] = D.GetString(dr출고항목["NO_SOLINE"]);
					this._flexL["DT_LOADING"] = D.GetString(dr출고항목["DT_LOADING"]);
					this._flexL["CD_ITEM"] = D.GetString(dr부대비용항목["CD_ITEM"]);
					this._flexL["NM_ITEM"] = D.GetString(dr부대비용항목["NM_ITEM"]);
					this._flexL["STND_ITEM"] = D.GetString(dr부대비용항목["STND_ITEM"]);
					this._flexL["UNIT_IM"] = D.GetString(dr부대비용항목["UNIT_IM"]);
					this._flexL["CLS_ITEM"] = D.GetString(dr부대비용항목["CLS_ITEM"]);
					this._flexL["CD_PARTNER"] = D.GetString(dr출고항목["CD_PARTNER"]);
					this._flexL["LN_PARTNER"] = D.GetString(dr출고항목["LN_PARTNER"]);
					this._flexL["CD_SALEGRP"] = D.GetString(dr출고항목["CD_SALEGRP"]);
					this._flexL["NM_SALEGRP"] = D.GetString(dr출고항목["NM_SALEGRP"]);
					this._flexL["NO_EMP"] = D.GetString(dr출고항목["NO_EMP"]);
					this._flexL["NM_KOR"] = D.GetString(dr출고항목["NM_KOR"]);
					this._flexL["FG_TRANS"] = D.GetString(dr출고항목["FG_TRANS"]);
					this._flexL["CD_GI_PARTNER"] = D.GetString(dr출고항목["CD_GI_PARTNER"]);
					this._flexL["GI_PARTNER"] = D.GetString(dr출고항목["GI_PARTNER"]);
					this._flexL["CD_CC"] = D.GetString(dr출고항목["CD_CC"]);
					this._flexL["NM_CC"] = D.GetString(dr출고항목["NM_CC"]);
					this._flexL["TP_IV"] = D.GetString(dr출고항목["TP_IV"]);
					this._flexL["YN_RETURN"] = D.GetString(dr출고항목["YN_RETURN"]);
					this._flexL["CD_PLANT"] = D.GetString(dr출고항목["CD_PLANT"]);
					this._flexL["CD_PJT"] = D.GetString(dr출고항목["CD_PJT"]);
					this._flexL["NM_PROJECT"] = D.GetString(dr출고항목["NM_PROJECT"]);
					this._flexL["NO_IMO"] = D.GetString(dr출고항목["NO_IMO"]);
					this._flexL["NM_VESSEL"] = D.GetString(dr출고항목["NM_VESSEL"]);
					this._flexL["YN_AUTO"] = D.GetString(dr출고항목["YN_AUTO"]);
					this._flexL["CD_DOCU"] = D.GetString(dr출고항목["CD_DOCU"]);
					this._flexL["NM_SO"] = D.GetString(dr출고항목["NM_SO"]);
					this._flexL["NM_QTIOTP"] = D.GetString(dr출고항목["NM_QTIOTP"]);
					this._flexL["YN_AUTO_CHARGE"] = "N";

					#region 금액계산
					this._flexL["CD_EXCH"] = cbo부대비용통화명.SelectedValue;
					this._flexL["RT_EXCH"] = cur부대비용환율.DecimalValue;

					this._flexL["QT_CLS_MM"] = 1;
					this._flexL["QT_GI_CLS"] = 1;
					this._flexL["QT_CLS"] = 1;
					this._flexL["QT_CLS_REMAIN"] = 1;

					this._flexL["UM_EX_CLS"] = this.외화계산(this.cur외화부대비용.DecimalValue);
					this._flexL["AM_EX_CLS"] = this.외화계산(D.GetDecimal(this._flexL["QT_GI_CLS"]) * D.GetDecimal(this._flexL["UM_EX_CLS"]));
					this._flexL["UM_ITEM_CLS"] = this.원화계산(D.GetDecimal(this._flexL["UM_EX_CLS"]) * D.GetDecimal(this._flexL["RT_EXCH"]));
					this._flexL["AM_CLS"] = this.원화계산(D.GetDecimal(this._flexL["QT_GI_CLS"]) * D.GetDecimal(this._flexL["UM_ITEM_CLS"]));

					this._flexL["RT_VAT"] = D.GetDecimal(dr출고항목["RT_VAT"].ToString());
					if (D.GetDecimal(this._flexL["RT_VAT"]) != 0)
						this._flexL["VAT"] = this.원화계산(D.GetDecimal(this._flexL["AM_CLS"]) * D.GetDecimal(this._flexL["RT_VAT"]) / 100);
					else
						this._flexL["VAT"] = 0;

					this._flexL["AM_TOT"] = (D.GetDecimal(this._flexL["AM_CLS"]) + D.GetDecimal(this._flexL["VAT"]));
					#endregion

					this._flexL.Col = this._flexL.Cols.Fixed;
					this._flexL.AddFinished();
					this._flexL.Focus();
					#endregion
				}

				this.SetHeaderAmt(D.GetString(this._flexH["NO_IO"]), D.GetString(this._flexH["FG_TAX"]));

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
				filter += " AND NO_SO = '" + D.GetString(this.cbo부대비용수주번호.SelectedValue) + "' AND CD_ITEM = '" + D.GetString(this.cbo부대비용항목.SelectedValue) + "'";

				dataRowArray = this._flexL.DataTable.Select(filter, string.Empty, DataViewRowState.Added);

				if (dataRowArray.Length > 0)
				{
					dataRowArray[0].Delete();
					this.SetHeaderAmt(D.GetString(this._flexH["NO_IO"]), D.GetString(this._flexH["FG_TAX"]));
				}

				this._flexH.SumRefresh();
				this._flexL.SumRefresh();
				this.선택금액계산();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region Grid 이벤트
		private void _flexH_AfterDataRefresh(object sender, ListChangedEventArgs e)
		{
			try
			{
				if (!this._flexH.HasNormalRow)
				{
					this.btn적용.Enabled = false;
					this.btn전체선택.Enabled = false;
					this.btn전체해제.Enabled = false;
				}
				else
				{
					this.btn적용.Enabled = true;
					this.btn전체선택.Enabled = true;
					this.btn전체해제.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexH_AfterEdit(object sender, RowColEventArgs e)
		{
			try
			{
				if (this.통제설정 == "100" && this._flexH.DataTable.Select("S = 'Y'").Length > 1)
				{
					this._flexH[this._flexH.Row, "S"] = "N";
					Global.MainFrame.ShowMessage("CZ_한번에 하나의 매출처만 선택할 수 있습니다. 통제설정을 확인하여 주십시요");
				}

				for (int @fixed = this._flexL.Rows.Fixed; @fixed < this._flexL.Rows.Count; ++@fixed)
				{
					this._flexL.SetData(@fixed, 1, D.GetString(this._flexH[this._flexH.Row, "S"]));
				}

				this.선택금액계산();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			string key, filter, query, 매출금액, 매입부대비용, 선적유무;

			try
			{
				DataTable dt = null;

				key = D.GetString(this._flexH["NO_IO"]);
				filter = "NO_IO = '" + key + "'";

				if (this._flexH.DetailQueryNeed)
				{
					if (this.rbtn전체.Checked == true)
						선적유무 = "A";
					else if (this.rbtn선적.Checked == true)
						선적유무 = "Y";
					else
						선적유무 = "N";

					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   key,
															   this.ctx매출처.CodeValue,
															   this.txt프로젝트.Text,
															   this.ctx수주담당자.CodeValue,
															   this.txt수주번호.Text,
															   this.ctx납품처.CodeValue,
															   this.ctx호선번호.CodeValue,
															   선적유무 });
				}

				this._flexL.BindingAdd(dt, filter);

				if (this._flexL.HasNormalRow)
				{
					dt = ComFunc.getGridGroupBy(this._flexL.DataTable.Select(this._flexL.RowFilter), new string[] { "NO_SO", "CD_EXCH", "RT_EXCH" }, true);
					dt.Columns.Add("NM_DISPLAY");
					dt.Columns.Add("AM_CHARGE");

					foreach (DataRow dr in dt.Rows)
					{
						filter = "NO_IO = '" + D.GetString(this._flexH["NO_IO"]) + "' AND NO_SO = '" + D.GetString(dr["NO_SO"]) + "'";
						매출금액 = string.Format("{0:###,###,###,##0.##}", D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_TOT)", filter)));

                        query = @"SELECT ISNULL(SUM(FD.AM_CR + FD.AM_DR), 0)
                                  FROM FI_DOCU FD WITH(NOLOCK)
                                  WHERE FD.CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                 "AND FD.CD_PJT = '" + D.GetString(dr["NO_SO"]) + "'" + Environment.NewLine +
                                @"AND EXISTS (SELECT 1 
		                                      FROM FI_ACCTCODE WITH(NOLOCK)
			                                  WHERE CD_COMPANY = FD.CD_COMPANY
			                                  AND CD_ACCT = FD.CD_ACCT
			                                  AND CD_ACGRP IN ('520', '540', '570'))";

						dr["AM_CHARGE"] = D.GetDecimal(DBHelper.ExecuteScalar(query));
						매입부대비용 = string.Format("{0:###,###,###,##0.##}", D.GetDecimal(dr["AM_CHARGE"]));

						dr["NM_DISPLAY"] = D.GetString(dr["NO_SO"]) + " (" + 매출금액 + " | " + 매입부대비용 + ")";

					}

					this.cbo부대비용수주번호.DataSource = dt;
					this.cbo부대비용수주번호.DisplayMember = "NM_DISPLAY";
					this.cbo부대비용수주번호.ValueMember = "NO_SO";

					if (this.cbo부대비용수주번호.SelectedItem != null)
					{
						this.cbo부대비용통화명.SelectedValue = D.GetString(((DataRowView)(this.cbo부대비용수주번호.SelectedItem)).Row["CD_EXCH"]);
						this.cur부대비용환율.DecimalValue = D.GetDecimal(((DataRowView)(this.cbo부대비용수주번호.SelectedItem)).Row["RT_EXCH"]);
					}
				}

                this.cbo협조내용.SelectedValue = D.GetString(this._flexH["CD_RMK"]);
				this.txt물류비.Text = this._flexH["DC_RMK_GIR"].ToString() +  
									  (string.IsNullOrEmpty(this._flexH["DC_RMK_FORWARD"].ToString()) ? string.Empty : Environment.NewLine + "포워더 비고 : " + this._flexH["DC_RMK_FORWARD"].ToString());
				this.txt커미션.Text = D.GetString(this._flexH["DC_RMK_SO"]);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void _flex_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				if ((sender as FlexGrid).Name != this._flexH.Name) return;

				if (this._flexH.Cols[e.Col].Name == "S")
				{
					DataRow[] dataRowArray = this._flexL.DataTable.Select("NO_IO = '" + this._flexH[e.Row, "NO_IO"].ToString() + "' AND FG_TAX = '" + this._flexH[e.Row, "FG_TAX"].ToString() + "'");

					if (this._flexH[e.Row, "S"].ToString() == "N")
					{
						for (int @fixed = this._flexL.Rows.Fixed; @fixed <= dataRowArray.Length; ++@fixed)
						{
							this._flexL.SetCellCheck(@fixed, 1, CheckEnum.Checked);
						}

						if (this._flexL.Rows.Count - this._flexL.Rows.Fixed > 0)
						{
							this._flexL.Row = this._flexL.Rows.Fixed;
							this._flexL.Col = this._flexL.Cols.Fixed;
						}

						this._flexL.Focus();
					}
					else
					{
						for (int @fixed = this._flexL.Rows.Fixed; @fixed <= dataRowArray.Length; ++@fixed)
						{
							this._flexL.SetCellCheck(@fixed, 1, CheckEnum.Unchecked);
						}

						if (this._flexL.Rows.Count - this._flexL.Rows.Fixed > 0)
						{
							this._flexL.Row = this._flexL.Rows.Fixed;
							this._flexL.Col = this._flexL.Cols.Fixed;
						}

						this._flexL.Focus();
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
				this.선택금액계산();
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region Control 이벤트
		private void cur원화부대비용계산(object sender, EventArgs e)
		{
			try
			{
				this.cur원화부대비용.DecimalValue = this.원화계산(this.cur외화부대비용.DecimalValue * this.cur부대비용환율.DecimalValue);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Control_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				if (e.HelpID != HelpID.P_USER)
				{
					if (e.HelpID == HelpID.P_MA_PARTNER_SUB)
					{
						this.사업자번호 = e.HelpReturn.Rows[0]["NO_COMPANY"].ToString();
						this.휴폐업구분 = e.HelpReturn.Rows[0]["CD_CON"].ToString();
						this.수금예정일 = e.HelpReturn.Rows[0]["DT_RCP_PREARRANGED"].ToString();
						this.부가세계산법 = e.HelpReturn.Rows[0]["TP_TAX"].ToString();
					}
				}
				else
				{
					if (e.ControlName == "ctx호선번호")
					{
						this.txt호선명.Text = e.HelpReturn.Rows[0]["NM_VESSEL"].ToString();
						this.ctx매출처.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
						this.ctx매출처.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
						this.ctx매출처.Enabled = false;
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void ctx호선번호_CodeChanged(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(ctx호선번호.Text))
				{
					this.txt호선명.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void SelectionChangeCommitted(object sender, EventArgs e)
		{
			string name = ((Control)sender).Name;

			if (name == this.cbo부대비용수주번호.Name)
			{
				if (this.cbo부대비용수주번호.SelectedItem != null)
				{
					this.cbo부대비용통화명.SelectedValue = D.GetString(((DataRowView)(this.cbo부대비용수주번호.SelectedItem)).Row["CD_EXCH"]);
					this.cur부대비용환율.DecimalValue = D.GetDecimal(((DataRowView)(this.cbo부대비용수주번호.SelectedItem)).Row["RT_EXCH"]);
				}
			}
			else if (name == this.cbo통화명.Name)
			{
				this.cur환율.Text = this._biz.환율(this.매출일자, this.cbo통화명.SelectedValue.ToString()).ToString();
			}
		}

		private void cbo부대비용수주번호_DrawItem(object sender, DrawItemEventArgs e)
		{
			ComboBox comboBox;
			DataRowView dataRowView;
			try
			{
				comboBox = (sender as ComboBox);
				if (comboBox.Items.Count <= 0) return;

				e.DrawBackground();

				dataRowView = ((DataRowView)comboBox.Items[e.Index]);

				string text = dataRowView.Row["NM_DISPLAY"].ToString();
				Font font = comboBox.Font;

				if (D.GetDecimal(dataRowView.Row["AM_CHARGE"]) > 0)
					e.Graphics.DrawString(text, font, Brushes.Red, e.Bounds);
				else
					e.Graphics.DrawString(text, font, Brushes.Black, e.Bounds);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion

		#region 기타
		private void 선택금액계산()
		{
			if (!this._flexL.HasNormalRow) return;

			this.cur외화합계.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_CLS)", "S = 'Y'"));
			this.cur원화합계.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLS)", "S = 'Y'"));
			this.cur부가세합계.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(VAT)", "S = 'Y'"));
			this.cur총합계.DecimalValue = (this.cur원화합계.DecimalValue + this.cur부가세합계.DecimalValue);
			this.cur총수량.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(QT_CLS)", "S = 'Y'"));
		}

		private void SetHeaderAmt(string NO_IO, string FG_TAX)
		{
			string filterExpression = "NO_IO = '" + NO_IO + "' AND FG_TAX = '" + FG_TAX + "'";

			DataRow[] dataRowArray = this._flexH.DataTable.Select(filterExpression);
			if (dataRowArray == null || dataRowArray.Length == 0) return;

			DataRow dataRow1 = dataRowArray[0];

			dataRow1["AM_EX"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_EX_CLS)", filterExpression));
			dataRow1["AM"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLS)", filterExpression));
			dataRow1["VAT"] = D.GetDecimal(this._flexL.DataTable.Compute("SUM(VAT)", filterExpression));
			dataRow1["AM_TOT"] = (D.GetDecimal(dataRow1["AM"]) + D.GetDecimal(dataRow1["VAT"]));
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
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.Windows.Print;
using Duzon.ERPU.MF.Common;
using System.Web;
using System.Text;

namespace cz
{
	public partial class P_CZ_SA_CLAIM : PageBase
	{
		#region 생성자 & 전역변수
		P_CZ_SA_CLAIM_BIZ _biz;
		P_CZ_SA_CLAIM_GW _gw;
		int _추가순번;
		string 임시폴더, 클레임번호;

		private string ServerDir
		{
			get
			{
				return "Upload/P_CZ_SA_CLAIM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(this._flexH["DT_INPUT"]).Substring(0, 4);
			}
		}

		public P_CZ_SA_CLAIM()
		{
			StartUp.Certify(this);
			InitializeComponent();
		}

		public P_CZ_SA_CLAIM(string 클레임번호)
		{
			StartUp.Certify(this);
			InitializeComponent();

			this.클레임번호 = 클레임번호;
		}
		#endregion
		
		#region 초기화
		protected override void InitLoad()
		{
			base.InitLoad();

			this._biz = new P_CZ_SA_CLAIM_BIZ();
			this._gw = new P_CZ_SA_CLAIM_GW();

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			//메인 툴바 저장 및 삭제 버튼 자동 활성화
			this.MainGrids = new FlexGrid[] { this._flexH, this._flexL };

			//DetailQueryNeed 사용하기 위해 세팅해줌..
			this._flexH.DetailGrids = new FlexGrid[] { this._flexL };

			#region Header
			this._flexH.BeginSetting(1, 1, false);

            this._flexH.SetCol("NM_STATUS", "상태", 60);
            this._flexH.SetCol("NM_GW_STATUS", "결재상태", 60);
            this._flexH.SetCol("NM_GW_STATUS1", "결재상태(선발행)", false);
            this._flexH.SetCol("NO_CLAIM", "클레임번호", 100);
            this._flexH.SetCol("DT_INPUT", "발행일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
            this._flexH.SetCol("NM_EMP", "담당자", 80);
            this._flexH.SetCol("TP_CLAIM", "클레임사유", false);
            this._flexH.SetCol("NM_CLAIM", "클레임사유", 100);
            this._flexH.SetCol("TP_CAUSE", "원인구분", false);
            this._flexH.SetCol("NM_CAUSE", "원인구분", false);
            this._flexH.SetCol("TP_ITEM", "항목분류", false);
            this._flexH.SetCol("NM_ITEM", "항목분류", false);
			this._flexH.SetCol("TP_REQUEST", "고객사요청사항", false);
			this._flexH.SetCol("NM_REQUEST", "고객사요청사항", false);
			this._flexH.SetCol("TP_RETURN", "실물반품여부", false);
			this._flexH.SetCol("NM_RETURN", "실물반품여부", false);
			this._flexH.SetCol("NO_SO", "수주번호", 100);
			this._flexH.SetCol("NM_SALEGRP", "영업그룹", false);
            this._flexH.SetCol("LN_PARTNER", "매출처", 150);
            this._flexH.SetCol("NM_SUPPLIER", "매입처", false);
            this._flexH.SetCol("NM_VESSEL", "호선명", 150);
			this._flexH.SetCol("AM_TOTAL_RCV", "예상비용", false);
			this._flexH.SetCol("DT_CLOSING", "종결일자", false);
			this._flexH.SetCol("AM_TOTAL_CLS", "종결비용", false);
			this._flexH.SetCol("APP_DOC_ID", "문서번호", false);
			this._flexH.SetCol("APP_DOC_ID1", "문서번호(선발행)", false);
			this._flexH.SetCol("NM_SUPPLIER_REWARD", "매입처보상방안", false);
			this._flexH.SetCol("YN_RETURN", "반품여부", false);

			this._flexH.SetOneGridBinding(null, new IUParentControl[] { this.pnl접수내역, this.pnl이미지, this.pnl접수, this.pnl진행, this.pnl종결, this.pnl선발행 });

			this._flexH.VerifyPrimaryKey = new string[] { "NO_CLAIM" };
			this._flexH.VerifyNotNull = new string[] { "NO_SO", "TP_CLAIM", "TP_CAUSE", "TP_ITEM", "TP_REQUEST", "TP_RETURN" };

			this._flexH.ExtendLastCol = false;

			this._flexH.SettingVersion = "0.0.0.3";
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region Line
			this._flexL.BeginSetting(1, 1, false);

			this._flexL.SetCol("NO_DSP", "순번", 40);
			this._flexL.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
			this._flexL.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
			this._flexL.SetCol("CD_ITEM", "품목코드", 100);
			this._flexL.SetCol("NM_ITEM", "품목명", 120);
			this._flexL.SetCol("UNIT_SO", "단위", 40);
			this._flexL.SetCol("QT_SO", "수량", 40, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_CLAIM", "클레임수량", 100, true, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_OUT_RETURN", "출고반품수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_IN_RETURN", "입고반품수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_IO_OUT", "계정대체출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("QT_IO_IN", "계정대체입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flexL.SetCol("CD_ITEM_CHANGE", "계정대체품목코드", 100);
			this._flexL.SetCol("NM_CLS_ITEM", "계정구분", 100);
			this._flexL.SetCol("CD_SUPPLIER", "매입처코드", 120, true);
			this._flexL.SetCol("LN_PARTNER", "매입처명", 120);
			this._flexL.SetCol("UM_PO", "발주단가", 80, false, typeof(decimal), FormatTpType.UNIT_COST);
			this._flexL.SetCol("AM_PO", "발주금액", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexL.SetCol("UM_STOCK", "재고단가", 80, false, typeof(decimal), FormatTpType.UNIT_COST);
			this._flexL.SetCol("AM_STOCK", "재고금액", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexL.SetCol("UM_SO", "매출단가", 80, false, typeof(decimal), FormatTpType.UNIT_COST);
			this._flexL.SetCol("AM_SO", "매출금액", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("AM_PROFIT", "이윤", 80, false, typeof(decimal), FormatTpType.MONEY);
            this._flexL.SetCol("RT_PROFIT", "이윤율", 80, false, typeof(decimal), FormatTpType.RATE);
			this._flexL.SetCol("AM_CLAIM", "대상금액", 80, false, typeof(decimal), FormatTpType.MONEY);
			this._flexL.SetCol("LT", "발주납기", 60);
			
			this._flexL.VerifyNotNull = new string[] { "QT_CLAIM" };
			this._flexL.VerifyCompare(this._flexL.Cols["QT_CLAIM"], this._flexL.Cols["QT_SO"], OperatorEnum.LessOrEqual);
			this._flexL.VerifyCompare(this._flexL.Cols["QT_CLAIM"], 0, OperatorEnum.Greater);

			this._flexL.ExtendLastCol = false;
			this._flexL.SettingVersion = "0.0.0.1";
			this._flexL.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexL.SetCodeHelpCol("CD_SUPPLIER", HelpID.P_MA_PARTNER_SUB, ShowHelpEnum.Always, new string[] { "CD_SUPPLIER", "LN_PARTNER" }, new string[] { "CD_PARTNER", "LN_PARTNER" });
			#endregion
		}

		protected override void InitPaint()
		{
			DataSet ds;

			try
			{
				base.InitPaint();

                if (this.DisplayRectangle.Width >= 1024)
                {
                    this.spl리스트.SplitterDistance = 938;
                    this.spl이미지.SplitterDistance = 466;
                }
				
				this.임시폴더 = this.PageID;

				this.dtp발행일자S.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.SELECT);
				this.dtp발행일자S.StartDate = this.MainFrameInterface.GetDateTimeToday().AddMonths(-1);
				this.dtp발행일자S.EndDate = this.MainFrameInterface.GetDateTimeToday();

				this.dtp수주일자.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
				this.dtp발행일자.Mask = this.GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);

				ds = this.GetComboData(new string[] { "S;CZ_SA00001",
													  "S;CZ_SA00002",
													  "S;CZ_SA00003",
													  "S;CZ_SA00004",
													  "S;CZ_SA00036",
													  "S;MA_B000005",
													  "S;CZ_SA00049",
													  "S;CZ_SA00050" });

                ds.Tables[1].DefaultView.Sort = "NAME ASC";

				this.cbo클레임사유S.ValueMember = "CODE";
				this.cbo클레임사유S.DisplayMember = "NAME";
				this.cbo클레임사유S.DataSource = ds.Tables[0].Copy();

				this.cbo클레임사유.ValueMember = "CODE";
				this.cbo클레임사유.DisplayMember = "NAME";
				this.cbo클레임사유.DataSource = ds.Tables[0].Copy();

				this.cbo원인구분.ValueMember = "CODE";
				this.cbo원인구분.DisplayMember = "NAME";
                this.cbo원인구분.DataSource = ds.Tables[1];

				this.cbo항목분류.ValueMember = "CODE";
				this.cbo항목분류.DisplayMember = "NAME";
				this.cbo항목분류.DataSource = ds.Tables[2].Copy();

				this.cbo항목분류S.ValueMember = "CODE";
				this.cbo항목분류S.DisplayMember = "NAME";
				this.cbo항목분류S.DataSource = ds.Tables[2].Copy();

				this.cbo상태S.ValueMember = "CODE";
				this.cbo상태S.DisplayMember = "NAME";
				this.cbo상태S.DataSource = ds.Tables[3];

				this.cbo매입처보상방안.ValueMember = "CODE";
				this.cbo매입처보상방안.DisplayMember = "NAME";
				this.cbo매입처보상방안.DataSource = ds.Tables[4];

				this.cbo선발행통화명.ValueMember = "CODE";
				this.cbo선발행통화명.DisplayMember = "NAME";
				this.cbo선발행통화명.DataSource = ds.Tables[5];

				this.cbo고객사요청사항.ValueMember = "CODE";
				this.cbo고객사요청사항.DisplayMember = "NAME";
				this.cbo고객사요청사항.DataSource = ds.Tables[6];

				this.cbo실물반품여부.ValueMember = "CODE";
				this.cbo실물반품여부.DisplayMember = "NAME";
				this.cbo실물반품여부.DataSource = ds.Tables[7];

				this.btn파일생성.Enabled = false;
				this.btn이전단계.Enabled = false;
				this.btn다음단계.Enabled = false;
				this.btn수주적용.Enabled = false;
				this.btn전자결재.Enabled = false;

				if (!string.IsNullOrEmpty(this.클레임번호))
				{
					this.txt클레임번호S.Text = this.클레임번호;
					this.OnToolBarSearchButtonClicked(null, null);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void InitEvent()
		{
			this._flexH.AfterRowChange += new RangeEventHandler(this._flexH_AfterRowChange);
			this._flexL.ValidateEdit += new ValidateEditEventHandler(this._flexL_ValidateEdit);

			this.btn파일생성.Click += new EventHandler(this.btn파일생성_Click);
			this.btn이전단계.Click += new EventHandler(this.btn이전단계_Click);
			this.btn다음단계.Click += new EventHandler(this.btn다음단계_Click);
			this.btn수주적용.Click += new EventHandler(this.btn수주적용_Click);
			this.btn전자결재.Click += new EventHandler(this.btn전자결재_Click);

			this.btn선발행전자결재.Click += new EventHandler(this.btn선발행전자결재_Click);

            this.btn품목추가.Click += new EventHandler(this.btn품목추가_Click);
            this.btn품목삭제.Click += new EventHandler(this.btn품목삭제_Click);
			
			this.btn파일보기1.Click += new EventHandler(this.btn파일보기_Click);
			this.btn파일보기2.Click += new EventHandler(this.btn파일보기_Click);
			this.btn파일보기3.Click += new EventHandler(this.btn파일보기_Click);
			this.btn파일보기4.Click += new EventHandler(this.btn파일보기_Click);
			this.btn파일보기5.Click += new EventHandler(this.btn파일보기_Click);
			this.btn파일보기6.Click += new EventHandler(this.btn파일보기_Click);

			this.btn파일선택1.Click += new EventHandler(this.btn파일선택_Click);
			this.btn파일선택2.Click += new EventHandler(this.btn파일선택_Click);
			this.btn파일선택3.Click += new EventHandler(this.btn파일선택_Click);
			this.btn파일선택4.Click += new EventHandler(this.btn파일선택_Click);
			this.btn파일선택5.Click += new EventHandler(this.btn파일선택_Click);
			this.btn파일선택6.Click += new EventHandler(this.btn파일선택_Click);

			this.btn파일삭제1.Click += new EventHandler(this.btn파일삭제_Click);
			this.btn파일삭제2.Click += new EventHandler(this.btn파일삭제_Click);
			this.btn파일삭제3.Click += new EventHandler(this.btn파일삭제_Click);
			this.btn파일삭제4.Click += new EventHandler(this.btn파일삭제_Click);
			this.btn파일삭제5.Click += new EventHandler(this.btn파일삭제_Click);
			this.btn파일삭제6.Click += new EventHandler(this.btn파일삭제_Click);

			this.cbo클레임사유.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);
			this.cbo원인구분.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);
			this.cbo항목분류.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);
			this.cbo매입처보상방안.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);
			this.cbo클레임원인.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);
			this.cbo고객사요청사항.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);
			this.cbo실물반품여부.SelectedValueChanged += new EventHandler(this.Control_SelectedValueChanged);

			this.cur접수품목금액.Leave += new EventHandler(this.cur예상금액_Leave);
			this.cur접수추가금액.Leave += new EventHandler(this.cur예상금액_Leave);
			this.cur진행품목금액.Leave += new EventHandler(this.cur예상금액_Leave);
			this.cur진행추가금액.Leave += new EventHandler(this.cur예상금액_Leave);
			this.cur종결품목금액.Leave += new EventHandler(this.cur예상금액_Leave);
			this.cur종결추가금액.Leave += new EventHandler(this.cur예상금액_Leave);

			this.ctx호선번호S.QueryAfter += new BpQueryHandler(this.ctx호선번호S_QueryAfter);
			this.ctx호선번호S.CodeChanged += new EventHandler(this.ctx호선번호S_CodeChanged);
		}
		#endregion

		#region 메인버튼 이벤트
		protected override bool BeforeSearch()
		{
			if (!base.BeforeSearch()) return false;
			return true;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				this.pixFile.Image = null;

				this._flexH.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																	  this.txt클레임번호S.Text,
																	  this.txt수주번호S.Text,
																	  this.dtp발행일자S.StartDateToString,
																	  this.dtp발행일자S.EndDateToString,
																	  this.ctx매출처S.CodeValue,
																	  this.ctx매입처.CodeValue,
																	  this.ctx호선번호S.CodeValue,
																	  D.GetString(this.cbo클레임사유S.SelectedValue),
																	  D.GetString(this.cbo상태S.SelectedValue),
																	  this.ctx담당자S.CodeValue,
                                                                      this.ctx영업그룹.CodeValue,
																	  this.cbo항목분류S.SelectedValue.ToString() });

				if (!this._flexH.HasNormalRow)
				{
					this._flexH.SetDefaultDisplayToSetBinding();
					ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				
				this.btn수주적용.Enabled = true;
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

				if (!BeforeAdd()) return;

				this.btn수주적용_Click(sender, e);
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

				if (MsgAndSave(PageActionMode.Save))
					ShowMessage(PageResultMode.SaveGood);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if (!this._flexH.HasNormalRow) return;
				if (this._flexH.RowState() == DataRowState.Added)
				{
					this.ShowMessage("저장 후 다시 시도 하세요.");
					return;
				}

                if (this.tcl진행단계.SelectedIndex <= 2)
                {
                    this._gw.미리보기(this._flexH.GetDataRow(this._flexH.Row),
                                      this._flexL.DataTable.Select("NO_CLAIM = '" + D.GetString(this._flexH["NO_CLAIM"]) + "'"),
                                      this._flexL.DataTable.DefaultView.ToTable(true, "LN_PARTNER"),
                                      D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLAIM)", "NO_CLAIM = '" + D.GetString(this._flexH["NO_CLAIM"]) + "'")),
                                      this.dtp발행일자.Value,
                                      this.dtp진행예상종결일자.Value,
                                      this.dtp종결일자.Value,
                                      (진행단계)(this.tcl진행단계.SelectedIndex == 2 ? 3 : this.tcl진행단계.SelectedIndex));
                }
                else if (this.tcl진행단계.SelectedIndex == 3)
                {
                    this._gw.선발행미리보기(this._flexH.GetDataRow(this._flexH.Row),
                                            this._flexL.DataTable.DefaultView.ToTable(true, "LN_PARTNER"),
                                            this.dtp발행일자.Value,
                                            this.cbo선발행통화명.Text);
                }
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			DataTable dtH, dtL;
			string 클레임번호;
			DataRow[] dataRowArray;
			int index;

			try
			{
                if (!base.SaveData() || !base.Verify()) return false;

				foreach (DataRow dr in this._flexH.DataTable.Rows)
				{
					if (dr.RowState == DataRowState.Added)
					{
						클레임번호 = (string)this.GetSeq(this.LoginInfo.CompanyCode, "CZ", "01", Global.MainFrame.GetStringToday.Substring(2, 2));
						dataRowArray = this._flexL.DataTable.Select("NO_CLAIM = '" + D.GetString(dr["NO_CLAIM"]) + "'");

						this._flexL.Redraw = false;
						index = 0;

						foreach (DataRow dr1 in dataRowArray)
						{
							MsgControl.ShowMsg("[자료저장중] 잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) });
							dr1["NO_CLAIM"] = 클레임번호;
						}

						MsgControl.CloseMsg();

						this._flexL.Redraw = true;

						dr["NO_CLAIM"] = 클레임번호;
						dr["CD_STATUS"] = D.GetString((int)진행단계.접수);
						dr["NM_STATUS"] = D.GetString(진행단계.접수);
					}

					if (dr.RowState == DataRowState.Deleted)
					{
						if (!this.FileDelete(dr)) return false;
					}
					else if (dr.RowState != DataRowState.Unchanged)
					{
						if (!this.FileUpLoad(dr)) return false;
					}
				}

				dtH = this._flexH.GetChanges();
				dtL = this._flexL.GetChanges();

				if (!this._biz.Save(dtH, dtL)) return false;

				this._flexH.AcceptChanges();
				this._flexL.AcceptChanges();
				this._추가순번 = 0;

				return true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				this._flexL.Redraw = true;
			}

			return false;
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			string 클레임상태, 결재상태, 클레임번호;

			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!this.BeforeDelete() || !this._flexH.HasNormalRow) return;
				if (!Util.CheckPW()) return;

				클레임상태 = D.GetString(this._flexH["CD_STATUS"]);
				결재상태 = D.GetString(this._flexH["CD_GW_STATUS"]);

				if (클레임상태 != "999" && 클레임상태 != "0")
				{
					this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", D.GetString(this._flexH["NM_STATUS"]));
					return;
				}
				if (결재상태 == "0" || 결재상태 == "1")
				{
					this.ShowMessage("CZ_@ 상태는 삭제할 수 없습니다.", D.GetString(this._flexH["NM_GW_STATUS"]));
					return;
				}

				클레임번호 = this._flexH["NO_CLAIM"].ToString();
				this._flexH.Rows.Remove(this._flexH.Row);

			    foreach(DataRow dr in this._flexL.DataTable.Select("NO_CLAIM = '" + 클레임번호 + "'"))
				{
					dr.Delete();
				}

				if (this._flexH.Rows.Count == this._flexH.Rows.Fixed)
					this._flexH.SetDefaultDisplayToSetBinding();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				this._flexL.Redraw = true;
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
				Global.MainFrame.MsgEnd(ex);
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

		#region 컨트롤 이벤트
		private void btn파일생성_Click(object sender, EventArgs e)
		{
			string query, 클레임번호, 파일유형;

			try
			{
				if (this._flexH.HasNormalRow == false) return;

				클레임번호 = D.GetString(this._flexH["NO_CLAIM"]);

				query = @"SELECT NO_DOCU 
FROM FI_GWDOCU
WHERE CD_COMPANY = 'K100'
AND NO_DOCU LIKE '%{0}%'
AND ST_STAT = '1'";

				DataTable dt = DBHelper.GetDataTable(string.Format(query, this.LoginInfo.CompanyCode + "-" + 클레임번호));

				if (Global.MainFrame.LoginInfo.CompanyCode != "K200" && (dt == null || dt.Rows.Count == 0))
				{
					this.ShowMessage("전자결재 종결 후 파일생성할 수 있습니다.");
					return;
				}

				query = @"SELECT COUNT(1)
						  FROM CZ_MA_WORKFLOWH WITH(NOLOCK)
						  WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
						 "AND NO_KEY = '" + 클레임번호 + "'";

				if (D.GetDecimal(DBHelper.ExecuteScalar(query)) > 0)
				{
					this.ShowMessage(공통메세지.이미등록된자료가있습니다);
					return;
				}
				else
				{
					H_CZ_COPY_FILE help = new H_CZ_COPY_FILE();
					help.NO_FILE_S = D.GetString(this._flexH["NO_SO"]);
					help.NO_FILE_T = 클레임번호;

					if (help.ShowDialog() != DialogResult.OK)
						return;
					else
					{
						query = @"SELECT CD_SYSDEF
								  FROM MA_CODEDTL WITH(NOLOCK)
								  WHERE CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								@"AND CD_FIELD = 'CZ_SA00023'
								  AND CD_FLAG2 = 'CLAIM'";

						파일유형 = D.GetString(DBHelper.ExecuteScalar(query));
						
						DBHelper.ExecuteScalar("SP_CZ_SA_INQ_REGH_I", new object[] { this.LoginInfo.CompanyCode,
																					 "01",		
																					 클레임번호,
																					 파일유형,		
																					 this.LoginInfo.UserID,		
																					 this.LoginInfo.UserID,	
																					 string.Empty,		
																					 string.Empty,		
																					 string.Empty,		
																					 this.LoginInfo.UserID });
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn이전단계_Click(object sender, EventArgs e)
		{
			진행단계 현재단계;
			string 결재상태;

			try
			{
				if (this._flexH.HasNormalRow == false) return;
                if (this.SaveData() == false) return;

				현재단계 = (진행단계)D.GetInt(this._flexH["CD_STATUS"]);
				결재상태 = D.GetString(this._flexH["CD_GW_STATUS"]);

				if (결재상태 == "1" || 결재상태 == "0")
				{
					this.ShowMessage("CZ_결재상태가 @ 상태 입니다.", D.GetString(this._flexH["NM_GW_STATUS"]));
					return;
				}

				switch (현재단계)
				{
					case 진행단계.접수:
						return;
					case 진행단계.해결:
					case 진행단계.진행:
						this._flexH["CD_STATUS"] = (int)진행단계.접수;
						this._flexH["NM_STATUS"] = D.GetString(진행단계.접수);
						break;
					case 진행단계.종결:
						this._flexH["CD_STATUS"] = (int)진행단계.진행;
						this._flexH["NM_STATUS"] = D.GetString(진행단계.진행);
						break;
				}

                DBHelper.ExecuteScalar(@"UPDATE CZ_SA_CLAIMH
                                         SET CD_STATUS = '" + this._flexH["CD_STATUS"].ToString() + "'" + Environment.NewLine +
                                        "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                        "AND NO_CLAIM = '" + this._flexH["NO_CLAIM"].ToString() + "'");

                this._flexH.AcceptChanges();

                this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn다음단계_Click(object sender, EventArgs e)
		{
			진행단계 현재단계;
			string 결재상태;

			try
			{
				if (this._flexH.HasNormalRow == false) return;
                if (this.SaveData() == false) return;

				현재단계 = (진행단계)D.GetInt(this._flexH["CD_STATUS"]);
				결재상태 = D.GetString(this._flexH["CD_GW_STATUS"]);

				switch (현재단계)
				{
					case 진행단계.접수:
						if (결재상태 != "1")
						{
							this.ShowMessage("CZ_결재상태가 @ 상태가 아닙니다.", this.DD("승인"));
							return;
						}

						this._flexH["CD_STATUS"] = (int)진행단계.진행;
						this._flexH["NM_STATUS"] = D.GetString(진행단계.진행);
						break;
					case 진행단계.해결:
					case 진행단계.진행:
						this._flexH["CD_STATUS"] = (int)진행단계.종결;
						this._flexH["NM_STATUS"] = D.GetString(진행단계.종결);
						break;
					case 진행단계.종결:
						this.ShowMessage("CZ_이미 @ 되었습니다.", this.DD("종결"));
						return;
				}

                DBHelper.ExecuteScalar(@"UPDATE CZ_SA_CLAIMH
                                         SET CD_STATUS = '" + this._flexH["CD_STATUS"].ToString() + "'" + Environment.NewLine +
                                        "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
                                        "AND NO_CLAIM = '" + this._flexH["NO_CLAIM"].ToString() + "'");

                this._flexH.AcceptChanges();

                this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn수주적용_Click (object sender, EventArgs e)
		{
			try
			{
				P_CZ_SA_CLAIM_SUB sub = new P_CZ_SA_CLAIM_SUB(this.txt수주번호.Text);

				if (sub.ShowDialog() == DialogResult.OK)
				{
					this.수주적용(sub.GetDataHeader, sub.GetDataLine, sub.수량일괄적용);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn파일보기_Click(object sender, EventArgs e)
		{
			string buttonName, index, str1, @string;

			try
			{
				if (!this._flexH.HasNormalRow)
					return;
				
				str1 = Application.StartupPath + "\\" + this.임시폴더;
				index = string.Empty;
				buttonName = ((Control)sender).Name;

				if (buttonName == this.btn파일보기1.Name)
					index = D.GetString(this.txtFile1.Tag);
				else if (buttonName == this.btn파일보기2.Name)
					index = D.GetString(this.txtFile2.Tag);
				else if (buttonName == this.btn파일보기3.Name)
					index = D.GetString(this.txtFile3.Tag);
				else if (buttonName == this.btn파일보기4.Name)
					index = D.GetString(this.txtFile4.Tag);
				else if (buttonName == this.btn파일보기5.Name)
					index = D.GetString(this.txtFile5.Tag);
				else if (buttonName == this.btn파일보기6.Name)
					index = D.GetString(this.txtFile6.Tag);

				@string = D.GetString(this._flexH[index]);
				if (@string != string.Empty && !@string.Contains("*"))
				{
					this.FileDownLoad(@string);
					this.pixFile.Image = Image.FromStream((Stream)new MemoryStream(System.IO.File.ReadAllBytes(str1 + "\\" + @string)));
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn파일선택_Click(object sender, EventArgs e)
		{
			string buttonName, @string;

			try
			{
				if (!this._flexH.HasNormalRow) return;

				OpenFileDialog openFileDialog = new OpenFileDialog();
				if (openFileDialog.ShowDialog() != DialogResult.OK) return;

				@string = D.GetString(this._flexH["NO_CLAIM"]);
				buttonName = ((Control)sender).Name;

				if (buttonName == this.btn파일선택1.Name)
				{
					string str2 = @string + "_01.JPG*" + openFileDialog.FileName;
					this.txtFile1.Text = str2;
					this._flexH["IMAGE1"] = str2;
				}
				else if (buttonName == this.btn파일선택2.Name)
				{
					string str3 = @string + "_02.JPG*" + openFileDialog.FileName;
					this.txtFile2.Text = str3;
					this._flexH["IMAGE2"] = str3;
				}
				else if (buttonName == this.btn파일선택3.Name)
				{
					string str4 = @string + "_03.JPG*" + openFileDialog.FileName;
					this.txtFile3.Text = str4;
					this._flexH["IMAGE3"] = str4;
				}
				else if (buttonName == this.btn파일선택4.Name)
				{
					string str5 = @string + "_04.JPG*" + openFileDialog.FileName;
					this.txtFile4.Text = str5;
					this._flexH["IMAGE4"] = str5;
				}
				else if (buttonName == this.btn파일선택5.Name)
				{
					string str6 = @string + "_05.JPG*" + openFileDialog.FileName;
					this.txtFile5.Text = str6;
					this._flexH["IMAGE5"] = str6;
				}
				else if (buttonName == this.btn파일선택6.Name)
				{
					string str7 = @string + "_06.JPG*" + openFileDialog.FileName;
					this.txtFile6.Text = str7;
					this._flexH["IMAGE6"] = str7;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn파일삭제_Click(object sender, EventArgs e)
		{
			string buttonName, index1, index2;

			try
			{
				if (!this._flexH.HasNormalRow) return;

				if (this._flexH.GetDataRow(this._flexH.Row).RowState == DataRowState.Modified)
				{
					this.ShowMessage("CZ_변경된 사항이 있습니다. 저장 후 다시 시도 바랍니다.");
				}
				else
				{
					buttonName = ((Control)sender).Name;
					index1 = string.Empty;
					index2 = string.Empty;

					if (buttonName == this.btn파일삭제1.Name)
					{
						index1 = D.GetString(this.txtFile1.Tag);
						index2 = D.GetString(this.txt이미지설명1.Tag);
					}
					else if (buttonName == this.btn파일삭제2.Name)
					{
						index1 = D.GetString(this.txtFile2.Tag);
						index2 = D.GetString(this.txt이미지설명2.Tag);
					}
					else if (buttonName == this.btn파일삭제3.Name)
					{
						index1 = D.GetString(this.txtFile3.Tag);
						index2 = D.GetString(this.txt이미지설명3.Tag);
					}
					else if (buttonName == this.btn파일삭제4.Name)
					{
						index1 = D.GetString(this.txtFile4.Tag);
						index2 = D.GetString(this.txt이미지설명4.Tag);
					}
					else if (buttonName == this.btn파일삭제5.Name)
					{
						index1 = D.GetString(this.txtFile5.Tag);
						index2 = D.GetString(this.txt이미지설명5.Tag);
					}
					else if (buttonName == this.btn파일삭제6.Name)
					{
						index1 = D.GetString(this.txtFile6.Tag);
						index2 = D.GetString(this.txt이미지설명6.Tag);
					}

					string @string = D.GetString(this._flexH[index1]);
					if (!@string.Contains("*") && this.ShowMessage("CZ_선택된 @ 을(를) 삭제하시겠습니까?", this.DD("이미지"), "QY2") != DialogResult.Yes || string.IsNullOrEmpty(@string))
						return;
					new WebClient().DownloadData(this.DeletePath(@string));

					if (buttonName == this.btn파일삭제1.Name)
					{
						this._flexH[index1] = this.txtFile1.Text = string.Empty;
						this._flexH[index2] = this.txt이미지설명1.Text = string.Empty;
					}
					else if (buttonName == this.btn파일삭제2.Name)
					{
						this._flexH[index1] = this.txtFile2.Text = string.Empty;
						this._flexH[index2] = this.txt이미지설명2.Text = string.Empty;
					}
					else if (buttonName == this.btn파일삭제3.Name)
					{
						this._flexH[index1] = this.txtFile3.Text = string.Empty;
						this._flexH[index2] = this.txt이미지설명3.Text = string.Empty;
					}
					else if (buttonName == this.btn파일삭제4.Name)
					{
						this._flexH[index1] = this.txtFile4.Text = string.Empty;
						this._flexH[index2] = this.txt이미지설명4.Text = string.Empty;
					}
					else if (buttonName == this.btn파일삭제5.Name)
					{
						this._flexH[index1] = this.txtFile5.Text = string.Empty;
						this._flexH[index2] = this.txt이미지설명5.Text = string.Empty;
					}
					else if (buttonName == this.btn파일삭제6.Name)
					{
						this._flexH[index1] = this.txtFile6.Text = string.Empty;
						this._flexH[index2] = this.txt이미지설명6.Text = string.Empty;
					}

					this.pixFile.Image = null;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void 수주적용(DataRow dr수주내역, DataRow[] dr수주품목, bool 수량일괄적용)
		{
			DataRow newrow;
			decimal 대상금액;

			try
			{
				대상금액 = 0;

				#region Master Data
				if (this._flexH.DataTable != null)
				{
					newrow = this._flexH.DataTable.NewRow();

					newrow["NO_CLAIM"] = this._추가순번.ToString();
					newrow["NO_SO"] = D.GetString(dr수주내역["NO_SO"]);
					newrow["NO_CONTRACT"] = D.GetString(dr수주내역["NO_CONTRACT"]);
					newrow["DT_SO"] = D.GetString(dr수주내역["DT_SO"]);
					newrow["CD_PARTNER"] = D.GetString(dr수주내역["CD_PARTNER"]);
					newrow["LN_PARTNER"] = D.GetString(dr수주내역["LN_PARTNER"]);
					newrow["NO_IMO"] = D.GetString(dr수주내역["NO_IMO"]);
					newrow["NO_HULL"] = D.GetString(dr수주내역["NO_HULL"]);
					newrow["NM_VESSEL"] = D.GetString(dr수주내역["NM_VESSEL"]);
					newrow["NO_SALES_EMP"] = D.GetString(dr수주내역["NO_SALES_EMP"]);
					newrow["NM_SALES_EMP"] = D.GetString(dr수주내역["NM_SALES_EMP"]);
					newrow["CD_STATUS"] = D.GetString((int)진행단계.미등록);
					newrow["NM_STATUS"] = D.GetString(진행단계.미등록);
					newrow["NO_EMP"] = Global.MainFrame.LoginInfo.UserID;
					newrow["NM_EMP"] = Global.MainFrame.LoginInfo.UserName;
					newrow["DT_INPUT"] = Global.MainFrame.GetStringToday;
					newrow["DC_RECEIVE"] = "AS-IS : " + Environment.NewLine + "TO-BE : ";

					this._flexH.DataTable.Rows.Add(newrow);

					this._추가순번++;
					this._flexH.Row = this._flexH.Rows.Count - 1;
				}
				#endregion

				#region Detail Data
				if (this._flexL.DataTable != null)
				{
					this._flexL.Redraw = false;
					this._flexL.RemoveViewAll();
					this._flexL.AcceptChanges();

					foreach (DataRow row수주품목 in dr수주품목)
					{
						newrow = this._flexL.DataTable.NewRow();

						newrow["NO_CLAIM"] = D.GetString(this._flexH["NO_CLAIM"]);
						newrow["NO_SO"] = D.GetString(row수주품목["NO_SO"]);
						newrow["SEQ_SO"] = D.GetDecimal(row수주품목["SEQ_SO"]);
						newrow["NO_DSP"] = D.GetDecimal(row수주품목["NO_DSP"]);
						newrow["NM_SUBJECT"] = D.GetString(row수주품목["NM_SUBJECT"]);
						newrow["CD_ITEM_PARTNER"] = D.GetString(row수주품목["CD_ITEM_PARTNER"]);
						newrow["NM_ITEM_PARTNER"] = D.GetString(row수주품목["NM_ITEM_PARTNER"]);
						newrow["CD_ITEM"] = D.GetString(row수주품목["CD_ITEM"]);
						newrow["NM_ITEM"] = D.GetString(row수주품목["NM_ITEM"]);
						newrow["UNIT_SO"] = D.GetString(row수주품목["UNIT_SO"]);
						newrow["QT_SO"] = D.GetDecimal(row수주품목["QT_SO"]);
						if (수량일괄적용 == true)
							newrow["QT_CLAIM"] = D.GetDecimal(row수주품목["QT_SO"]);
						newrow["CLS_ITEM"] = D.GetString(row수주품목["CLS_ITEM"]);
						newrow["NM_CLS_ITEM"] = D.GetString(row수주품목["NM_CLS_ITEM"]);
						newrow["CD_SUPPLIER"] = D.GetString(row수주품목["CD_PARTNER"]);
						newrow["LN_PARTNER"] = D.GetString(row수주품목["LN_PARTNER"]);
						newrow["UM_PO"] = D.GetDecimal(row수주품목["UM_PO"]);
						newrow["AM_PO"] = D.GetDecimal(row수주품목["AM_PO"]);
						newrow["UM_STOCK"] = D.GetDecimal(row수주품목["UM_STOCK"]);
						newrow["AM_STOCK"] = D.GetDecimal(row수주품목["AM_STOCK"]);
						newrow["UM_SO"] = D.GetDecimal(row수주품목["UM_SO"]);
						newrow["AM_SO"] = D.GetDecimal(row수주품목["AM_SO"]);
						newrow["AM_CLAIM"] = (D.GetDecimal(newrow["QT_CLAIM"]) * D.GetDecimal(newrow["UM_SO"]));
						newrow["AM_CLAIM_PO"] = (D.GetDecimal(newrow["QT_CLAIM"]) * (D.GetDecimal(newrow["UM_PO"]) == 0 ? D.GetDecimal(newrow["UM_STOCK"]) : D.GetDecimal(newrow["UM_PO"])));
						newrow["AM_PROFIT"] = (D.GetDecimal(newrow["AM_CLAIM"]) - D.GetDecimal(newrow["AM_CLAIM_PO"]));
						newrow["RT_PROFIT"] = (D.GetDecimal(newrow["AM_CLAIM"]) == 0 ? 0 : Decimal.Round((D.GetDecimal(newrow["AM_PROFIT"]) / D.GetDecimal(newrow["AM_CLAIM"])) * 100, 2, MidpointRounding.AwayFromZero));
						newrow["LT"] = D.GetDecimal(row수주품목["LT"]);
						newrow["DT_OUT"] = D.GetDecimal(row수주품목["DT_OUT"]);

						대상금액 += D.GetDecimal(newrow["AM_CLAIM"]);

						this._flexL.DataTable.Rows.Add(newrow);
					}

					this._flexL.Redraw = true;
					this._flexL.Row = this._flexL.Rows.Count - 1;
					this._flexL.SumRefresh();

					this.cur접수품목금액.DecimalValue = 대상금액;
					this._flexH["AM_ITEM_RCV"] = this.cur접수품목금액.DecimalValue;
					this.cur접수총금액.DecimalValue = 대상금액;
					this._flexH["AM_TOTAL_RCV"] = this.cur접수품목금액.DecimalValue;
				}
				#endregion
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn전자결재_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flexH.HasNormalRow == false) return;
				
				if (this._flexH["TP_CAUSE"].ToString() != "5" && 
					(this.cbo클레임원인.SelectedValue == null || string.IsNullOrEmpty(this.cbo클레임원인.SelectedValue.ToString())))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl클레임원인.Text);
					return;
				}

				if (this._flexH["TP_RETURN"].ToString() == "002" && this.cur포장무게.DecimalValue == 0)
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl포장무게.Text);
					return;
				}

				if (((진행단계)D.GetInt(this._flexH["CD_STATUS"])) == 진행단계.미등록)
				{
					this.ShowMessage(공통메세지.등록되지않은자료입니다);
					return;
				}
				else if (((진행단계)D.GetInt(this._flexH["CD_STATUS"])) == 진행단계.종결)
				{
                    if (string.IsNullOrEmpty(this.dtp종결일자.Text))
                    {
                        this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl종결일자.Text);
                        return;
                    }

					if (this.cbo원인구분.SelectedValue != null && this.cbo원인구분.SelectedValue.ToString() == "1" &&
					   (this.cbo매입처보상방안.SelectedValue == null || string.IsNullOrEmpty(this.cbo매입처보상방안.SelectedValue.ToString()) || string.IsNullOrEmpty(this.txt매입처보상방안.Text)))
					{
						this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl매입처보상방안.Text);
						return;
					}
				}

				if (this._gw.전자결재(this._flexH.GetDataRow(this._flexH.Row),
									  this._flexL.DataTable.Select("NO_CLAIM = '" + D.GetString(this._flexH["NO_CLAIM"]) + "'"),
									  this._flexL.DataTable.DefaultView.ToTable(true, "LN_PARTNER"),
									  D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLAIM)", "NO_CLAIM = '" + D.GetString(this._flexH["NO_CLAIM"]) + "'")),
									  this.dtp발행일자.Value,
                                      this.dtp진행예상종결일자.Value,
									  this.dtp종결일자.Value,
									  (진행단계)D.GetInt(this._flexH["CD_STATUS"])))
				{
					ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
					this.OnToolBarSearchButtonClicked(null, null);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn선발행전자결재_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._flexH.HasNormalRow == false) return;

				if (((진행단계)D.GetInt(this._flexH["CD_STATUS"])) == 진행단계.미등록)
				{
					this.ShowMessage(공통메세지.등록되지않은자료입니다);
					return;
				}

				if (string.IsNullOrEmpty(this.txt선발행사유.Text))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선발행사유.Text);
					this.txt선발행사유.Focus();
					return;
				}
				else if (string.IsNullOrEmpty(this.cbo선발행통화명.Text))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선발행금액.Text);
					this.cbo선발행통화명.Focus();
					return;
				}
				else if (this.cur선발행금액.DecimalValue == 0)
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl선발행금액.Text);
					this.cur선발행금액.Focus();
					return;
				}

				if (this._gw.선발행전자결재(this._flexH.GetDataRow(this._flexH.Row),
											this._flexL.DataTable.DefaultView.ToTable(true, "LN_PARTNER"),
											this.dtp발행일자.Value,
											this.cbo선발행통화명.Text))
				{
					ShowMessage(공통메세지._작업을완료하였습니다, this.DD("전자결재"));
					this.OnToolBarSearchButtonClicked(null, null);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void ctx호선번호S_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				this.txt호선명S.Text = e.HelpReturn.Rows[0]["NM_VESSEL"].ToString();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void ctx호선번호S_CodeChanged(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(ctx호선번호S.Text))
				{
					this.txt호선명S.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void cur예상금액_Leave(object sender, EventArgs e)
		{
			try
			{
				this.cur접수총금액.DecimalValue = (this.cur접수품목금액.DecimalValue + this.cur접수추가금액.DecimalValue);
				this.cur진행총금액.DecimalValue = (this.cur진행품목금액.DecimalValue + this.cur진행추가금액.DecimalValue);
				this.cur종결총금액.DecimalValue = (this.cur종결품목금액.DecimalValue + this.cur종결추가금액.DecimalValue);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Control_SelectedValueChanged(object sender, EventArgs e)
		{
			string name, query;

			try
			{
				if (this._flexH.HasNormalRow == false) return;

				name = ((Control)sender).Name;

				if (name == this.cbo클레임사유.Name)
					this._flexH["NM_CLAIM"] = this.cbo클레임사유.Text;
				else if (name == this.cbo원인구분.Name)
				{
					this._flexH["NM_CAUSE"] = this.cbo원인구분.Text;

					query = @"SELECT NULL AS CODE, NULL AS NAME, NULL AS NAME_E
UNION ALL
SELECT MC.CD_SYSDEF AS CODE,
       MC.NM_SYSDEF AS NAME,
	   MC.NM_SYSDEF_E AS NAME_E
FROM MA_CODEDTL MC
WHERE MC.CD_COMPANY = '{0}'
AND MC.CD_FIELD = 'CZ_SA00048'
AND MC.CD_FLAG1 LIKE '%{1}%'";

					DataTable dt = DBHelper.GetDataTable(string.Format(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.cbo원인구분.SelectedValue)));

					this.cbo클레임원인.ValueMember = "CODE";

					if (Global.CurLanguage == Language.KR)
						this.cbo클레임원인.DisplayMember = "NAME";
					else
						this.cbo클레임원인.DisplayMember = "NAME_E";

					this.cbo클레임원인.DataSource = dt;
					this.cbo클레임원인.SelectedValue = this._flexH["TP_REASON"].ToString();
				}
				else if(name == this.cbo항목분류.Name)
					this._flexH["NM_ITEM"] = this.cbo항목분류.Text;
				else if (name == this.cbo매입처보상방안.Name)
					this._flexH["NM_SUPPLIER_REWARD"] = this.cbo매입처보상방안.Text;
				else if (name == this.cbo클레임원인.Name)
					this._flexH["NM_REASON"] = this.cbo클레임원인.Text;
				else if (name == this.cbo고객사요청사항.Name)
					this._flexH["NM_REQUEST"] = this.cbo고객사요청사항.Text;
				else if (name == this.cbo실물반품여부.Name)
				{
					this._flexH["NM_RETURN"] = this.cbo실물반품여부.Text;

					if (this.cbo실물반품여부.SelectedValue != null && 
					    this.cbo실물반품여부.SelectedValue.ToString() == "002")
						this.cur포장무게.BackColor = Color.FromArgb(255, 237, 242);
					else
						this.cur포장무게.BackColor = Color.White;
				}
					
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

        private void btn품목추가_Click(object sender, EventArgs e)
        {
            DataRow newrow;

            try
            {
                if (!this._flexH.HasNormalRow) return;

                P_CZ_SA_CLAIM_SUB sub = new P_CZ_SA_CLAIM_SUB(this.txt수주번호.Text);

                if (sub.ShowDialog() == DialogResult.OK)
                {
                    if (this._flexL.DataTable != null)
                    {
                        this._flexL.Redraw = false;

                        foreach (DataRow row수주품목 in sub.GetDataLine)
                        {
                            if (this.txt수주번호.Text != row수주품목["NO_SO"].ToString() || 
                                this._flexL.DataTable.Select("NO_SO = '" + row수주품목["NO_SO"].ToString() + "' AND SEQ_SO = '" + row수주품목["SEQ_SO"].ToString() + "'").Length > 0)
                                continue;

                            newrow = this._flexL.DataTable.NewRow();

                            newrow["NO_CLAIM"] = D.GetString(this._flexH["NO_CLAIM"]);
                            newrow["NO_SO"] = D.GetString(row수주품목["NO_SO"]);
                            newrow["SEQ_SO"] = D.GetDecimal(row수주품목["SEQ_SO"]);
                            newrow["NO_DSP"] = D.GetDecimal(row수주품목["NO_DSP"]);
                            newrow["NM_SUBJECT"] = D.GetString(row수주품목["NM_SUBJECT"]);
                            newrow["CD_ITEM_PARTNER"] = D.GetString(row수주품목["CD_ITEM_PARTNER"]);
                            newrow["NM_ITEM_PARTNER"] = D.GetString(row수주품목["NM_ITEM_PARTNER"]);
                            newrow["CD_ITEM"] = D.GetString(row수주품목["CD_ITEM"]);
                            newrow["NM_ITEM"] = D.GetString(row수주품목["NM_ITEM"]);
                            newrow["UNIT_SO"] = D.GetString(row수주품목["UNIT_SO"]);
                            newrow["QT_SO"] = D.GetDecimal(row수주품목["QT_SO"]);
                            if (sub.수량일괄적용 == true)
                                newrow["QT_CLAIM"] = D.GetDecimal(row수주품목["QT_SO"]);
                            newrow["CLS_ITEM"] = D.GetString(row수주품목["CLS_ITEM"]);
                            newrow["NM_CLS_ITEM"] = D.GetString(row수주품목["NM_CLS_ITEM"]);
                            newrow["CD_SUPPLIER"] = D.GetString(row수주품목["CD_PARTNER"]);
                            newrow["LN_PARTNER"] = D.GetString(row수주품목["LN_PARTNER"]);
                            newrow["UM_PO"] = D.GetDecimal(row수주품목["UM_PO"]);
                            newrow["AM_PO"] = D.GetDecimal(row수주품목["AM_PO"]);
                            newrow["UM_STOCK"] = D.GetDecimal(row수주품목["UM_STOCK"]);
                            newrow["AM_STOCK"] = D.GetDecimal(row수주품목["AM_STOCK"]);
                            newrow["UM_SO"] = D.GetDecimal(row수주품목["UM_SO"]);
                            newrow["AM_SO"] = D.GetDecimal(row수주품목["AM_SO"]);
                            newrow["AM_CLAIM"] = (D.GetDecimal(newrow["QT_CLAIM"]) * D.GetDecimal(newrow["UM_SO"]));
                            newrow["AM_CLAIM_PO"] = (D.GetDecimal(newrow["QT_CLAIM"]) * (D.GetDecimal(newrow["UM_PO"]) == 0 ? D.GetDecimal(newrow["UM_STOCK"]) : D.GetDecimal(newrow["UM_PO"])));
                            newrow["AM_PROFIT"] = (D.GetDecimal(newrow["AM_CLAIM"]) - D.GetDecimal(newrow["AM_CLAIM_PO"]));
                            newrow["RT_PROFIT"] = (D.GetDecimal(newrow["AM_CLAIM"]) == 0 ? 0 : Decimal.Round((D.GetDecimal(newrow["AM_PROFIT"]) / D.GetDecimal(newrow["AM_CLAIM"])) * 100, 2, MidpointRounding.AwayFromZero));
                            newrow["LT"] = D.GetDecimal(row수주품목["LT"]);
                            newrow["DT_OUT"] = D.GetDecimal(row수주품목["DT_OUT"]);

                            this._flexL.DataTable.Rows.Add(newrow);
                        }

                        this._flexL.Redraw = true;
                        this._flexL.Row = this._flexL.Rows.Count - 1;
                        this._flexL.SumRefresh();

                        this.cur접수품목금액.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLAIM)", string.Empty));
                        this._flexH["AM_ITEM_RCV"] = this.cur접수품목금액.DecimalValue;
                        this.cur접수총금액.DecimalValue = this.cur접수품목금액.DecimalValue;
                        this._flexH["AM_TOTAL_RCV"] = this.cur접수품목금액.DecimalValue;

                        if (this._flexL.IsDataChanged)
                            this.ToolBarSaveButtonEnabled = true;
                    }
                }
            }
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
        }

        private void btn품목삭제_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this._flexL.HasNormalRow) return;

                this._flexL.GetDataRow(this._flexL.Row).Delete();

                this.cur접수품목금액.DecimalValue = D.GetDecimal(this._flexL.DataTable.Compute("SUM(AM_CLAIM)", string.Empty));
                this._flexH["AM_ITEM_RCV"] = this.cur접수품목금액.DecimalValue;
                this.cur접수총금액.DecimalValue = this.cur접수품목금액.DecimalValue;
                this._flexH["AM_TOTAL_RCV"] = this.cur접수품목금액.DecimalValue;
            }
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
        }
		#endregion

		#region 그리드 이벤트
		private void _flexH_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt = null;
			string filter, 결재상태;
			진행단계 진행상태;

			try
			{
				filter = "NO_CLAIM = '" + this._flexH["NO_CLAIM"].ToString() + "'";

				if (this._flexH.DetailQueryNeed == true)
				{
					dt = _biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
														  D.GetString(this._flexH["NO_CLAIM"]),
														  this.ctx매입처.CodeValue });
				}

				this._flexL.BindingAdd(dt, filter);

				if (D.GetInt(this._flexH["CD_STATUS"]) == (int)진행단계.미등록)
				{
					this.btn파일생성.Enabled = false;
					this.btn이전단계.Enabled = false;
					this.btn다음단계.Enabled = false;
					this.btn전자결재.Enabled = false;

                    this.cbo클레임사유.Enabled = true;
                    this.cbo원인구분.Enabled = true;
					this.cbo항목분류.Enabled = true;

					this.cbo클레임원인.Enabled = true;
					this.cbo고객사요청사항.Enabled = true;
					this.cbo실물반품여부.Enabled = true;

					this.cur포장무게.Enabled = true;
					this.txt포장사이즈.ReadOnly = false;

					this.pnl접수.Enabled = false;
					this.pnl진행.Enabled = false;
					this.pnl종결.Enabled = false;
					this.pnl선발행.Enabled = false;

					this._flexL.Cols["QT_CLAIM"].AllowEditing = true;

                    this.btn품목추가.Enabled = true;
                    this.btn품목삭제.Enabled = true;
				}
				else
				{
					this.lbl선발행결재상태.Text = D.GetString(this._flexH["NM_GW_STATUS1"]);

					this.btn파일생성.Enabled = true;
					this.btn전자결재.Enabled = true;

					this._flexL.Cols["QT_CLAIM"].AllowEditing = false;
					진행상태 = ((진행단계)D.GetInt(this._flexH["CD_STATUS"]));
					결재상태 = D.GetString(this._flexH["CD_GW_STATUS"]);

                    this.cbo클레임사유.Enabled = true;
                    this.cbo원인구분.Enabled = true;
					this.cbo항목분류.Enabled = true;

					this.cbo클레임원인.Enabled = true;
					this.cbo고객사요청사항.Enabled = true;
					this.cbo실물반품여부.Enabled = true;

					this.cur포장무게.Enabled = true;
					this.txt포장사이즈.ReadOnly = false;

					this.btn품목추가.Enabled = false;
                    this.btn품목삭제.Enabled = false;

					#region 접수
					this.txt비고.ReadOnly = true;

					this.cur접수품목금액.ReadOnly = true;
					this.cur접수추가금액.ReadOnly = true;
					#endregion

					#region 진행
					this.txt진행상황.ReadOnly = true;

					this.cur진행품목금액.ReadOnly = true;
					this.cur진행추가금액.ReadOnly = true;
                    this.dtp진행예상종결일자.Enabled = false;
					#endregion

					#region 종결
					this.txt종결내용.ReadOnly = true;
					this.txt매입처보상방안.ReadOnly = true;

					this.cbo매입처보상방안.Enabled = false;

					this.cur종결품목금액.ReadOnly = true;
					this.cur종결추가금액.ReadOnly = true;

					this.dtp종결일자.Enabled = false;
					#endregion

					#region 선발행
					this.txt선발행사유.ReadOnly = true;
					this.cbo선발행통화명.Enabled = false;
					this.cur선발행금액.ReadOnly = true;
					this.btn선발행전자결재.Enabled = false;
					#endregion

					//0(진행중), 1(승인), -1(반려), 2(미상신), 3(취소(삭제))
					switch (진행상태)
					{
						case 진행단계.접수:
							#region 접수
							this.btn이전단계.Enabled = false;
							this.btn다음단계.Enabled = true;

							if (결재상태 != "0" && 결재상태 != "1")
							{
								this._flexL.Cols["QT_CLAIM"].AllowEditing = true;

								this.txt비고.ReadOnly = false;

								this.cur접수품목금액.ReadOnly = false;
								this.cur접수추가금액.ReadOnly = false;

                                this.btn품목추가.Enabled = true;
                                this.btn품목삭제.Enabled = true;
							}
							else if (결재상태 == "1")
							{
								this.txt선발행사유.ReadOnly = false;
								this.cbo선발행통화명.Enabled = true;
								this.cur선발행금액.ReadOnly = false;
								this.btn선발행전자결재.Enabled = true;
							}
							#endregion
							break;
						case 진행단계.해결:
						case 진행단계.진행:
							#region 진행
							this.btn이전단계.Enabled = true;
							this.btn다음단계.Enabled = true;

							if (결재상태 != "0" && 결재상태 != "1")
							{
								this.txt진행상황.ReadOnly = false;

								this.cur진행품목금액.ReadOnly = false;
								this.cur진행추가금액.ReadOnly = false;
                                this.dtp진행예상종결일자.Enabled = true;
							}

							this.txt선발행사유.ReadOnly = false;
							this.cbo선발행통화명.Enabled = true;
							this.cur선발행금액.ReadOnly = false;
							this.btn선발행전자결재.Enabled = true;
							#endregion
							break;
						case 진행단계.종결:
							#region 종결
							this.btn이전단계.Enabled = true;
							this.btn다음단계.Enabled = false;

                            if (결재상태 != "0" && 결재상태 != "1")
                            {
                                this.txt종결내용.ReadOnly = false;

                                if (this.cbo원인구분.SelectedValue != null && this.cbo원인구분.SelectedValue.ToString() == "1")
                                {
                                    this.txt매입처보상방안.ReadOnly = false;
                                    this.cbo매입처보상방안.Enabled = true;
                                }

                                this.cur종결품목금액.ReadOnly = false;
                                this.cur종결추가금액.ReadOnly = false;

                                this.dtp종결일자.Enabled = true;

                                this.txt선발행사유.ReadOnly = false;
                                this.cbo선발행통화명.Enabled = true;
                                this.cur선발행금액.ReadOnly = false;
                                this.btn선발행전자결재.Enabled = true;
                            }
                            else
                            {
                                this.cbo클레임사유.Enabled = false;
                                this.cbo원인구분.Enabled = false;
                                this.cbo항목분류.Enabled = false;

								this.cbo클레임원인.Enabled = false;
								this.cbo고객사요청사항.Enabled = false;
								this.cbo실물반품여부.Enabled = false;

								this.cur포장무게.Enabled = false;
								this.txt포장사이즈.ReadOnly = true;
							}
							#endregion
							break;
					}

					if (this._flexH["TP_RETURN"].ToString() == "002")
						this.cur포장무게.BackColor = Color.FromArgb(255, 237, 242);
					else
						this.cur포장무게.BackColor = Color.White;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flexL_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			try
			{
				string colname = _flexL.Cols[e.Col].Name;
				string oldValue = D.GetString(((FlexGrid)sender).GetData(e.Row, e.Col));
				string newValue = ((FlexGrid)sender).EditData;

				switch (colname)
				{
					case "QT_CLAIM":
						if (D.GetDecimal(newValue) < 0)
						{
							this._flexL[e.Row, "QT_CLAIM"] = D.GetDecimal(oldValue);
							ShowMessage("CZ_@ 은(는) 0보다 크거나 같아야 합니다.", this.DD("문제수량"));
							e.Cancel = true;
							return;
						}

						decimal 항목수량 = D.GetDecimal(_flexL["QT_SO"]);
						if (D.GetDecimal(newValue) > 항목수량)
						{
							this._flexL[e.Row, "QT_CLAIM"] = D.GetDecimal(oldValue);
							ShowMessage("CZ_@ 은(는) @ 보다 작거나 같아야 합니다.", new string[] { this.DD("문제수량"), this.DD("항목수량") });
							e.Cancel = true;
							return;
						}

						this._flexL["AM_CLAIM"] = (D.GetDecimal(this._flexL["QT_CLAIM"]) * D.GetDecimal(this._flexL["UM_SO"]));
						this._flexL["AM_CLAIM_PO"] = (D.GetDecimal(this._flexL["QT_CLAIM"]) * (D.GetDecimal(this._flexL["UM_PO"]) == 0 ? D.GetDecimal(this._flexL["UM_STOCK"]) : D.GetDecimal(this._flexL["UM_PO"])));
						this._flexL["AM_PROFIT"] = (D.GetDecimal(this._flexL["AM_CLAIM"]) - D.GetDecimal(this._flexL["AM_CLAIM_PO"]));
						this._flexL["RT_PROFIT"] = (D.GetDecimal(this._flexL["AM_CLAIM"]) == 0 ? 0 : Decimal.Round((D.GetDecimal(this._flexL["AM_PROFIT"]) / D.GetDecimal(this._flexL["AM_CLAIM"])) * 100, 2, MidpointRounding.AwayFromZero));

						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#region 기타 메소드
		private bool FileUpLoad(DataRow dr)
		{
			try
			{
				MsgControl.ShowMsg("File Upload......");
				
				for (int index = 1; index <= 6; ++index)
				{
					if (!(D.GetString(dr["IMAGE" + index]) == string.Empty) && D.GetString(dr["IMAGE" + index]).Contains("*"))
					{
						string[] strArray = D.GetString(dr["IMAGE" + index]).Split('*');
						string fileName1 = strArray[0];
						string[] strArray1 = fileName1.Split('_');
						if (strArray1[0] != D.GetString(dr["NO_CLAIM"]))
							fileName1 = D.GetString(dr["NO_CLAIM"]) + "_" + strArray1[1];
						string fileName2 = strArray[1];
						new WebClient().UploadFile(this.UploadPath(fileName1), "POST", fileName2);
						dr["IMAGE" + index] = fileName1;
					}
				}
			}
			finally
			{
				MsgControl.CloseMsg();
			}
			return true;
		}

		private bool FileDelete(DataRow dr)
		{
			string fileName;

			try
			{
				MsgControl.ShowMsg("File Delete......");

				for (int index = 1; index <= 6; ++index)
				{
					fileName = D.GetString(dr["IMAGE" + index, DataRowVersion.Original]);

					if (!string.IsNullOrEmpty(fileName))
					{
						new WebClient().DownloadData(this.DeletePath(fileName));
					}
				}
			}
			finally
			{
				MsgControl.CloseMsg();
			}
			return true;
		}

		private void FileDownLoad(string FILE_PATH_MNG)
		{
			if (this.pixFile.Image != null)
				this.pixFile.Image = null;
			
			string path = Application.StartupPath + "\\" + this.임시폴더;
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			
			if (!directoryInfo.Exists)
				directoryInfo.Create();
			
			if (!this._flexH.HasNormalRow || string.IsNullOrEmpty(FILE_PATH_MNG))
				return;
			
			string fileName = path + "\\" + FILE_PATH_MNG;
			new WebClient().DownloadFile(Global.MainFrame.HostURL + "/" + this.ServerDir + "/" + FILE_PATH_MNG, fileName);
		}

		internal string DeletePath(string fileName)
		{
			return Global.MainFrame.HostURL + "/admin/fileinfoprocess.aspx?type=FileDelete2&dir=" + this.ServerDir + "/&Filename=" + fileName;
		}

		internal string UploadPath(string fileName)
		{
			fileName = HttpUtility.UrlEncode(fileName, Encoding.GetEncoding("EUC-KR"));
			return Global.MainFrame.HostURL + "/admin/FileUploader.aspx?File=" + fileName + "&Dir=/" + this.ServerDir + "&Action=etc&date=20090819";
		}
		#endregion
	}
}

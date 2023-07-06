using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Duzon.ERPU;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.SA.Common;
using DzHelpFormLib;
using sale;

namespace cz
{
	public partial class P_CZ_SA_GIRR_REG : Duzon.Common.Forms.PageBase
	{
		#region ♣ 멤버필드
		private P_CZ_SA_GIRR_REG_BIZ _biz = new P_CZ_SA_GIRR_REG_BIZ();
		private FreeBinding _header = new FreeBinding();
		private bool _헤더수정여부 = true;
		private string _매출형태 = "";
		private string _단가적용형태 = "";              //영업그룹을 선택시 단가정보(TP_SALEPRICE)도 가져온다.

		private string _배송여부 = string.Empty;    //반품형태 선택시 배송여부 디폴트값을 넣어준다

		private 수주관리.Config 수주Config = new 수주관리.Config();

		private string _계산서처리코드 = "001";
		private string _계산서처리명 = "일괄";
		private string _단가유형코드 = "001";
		private string _협조전번호;
		#endregion

		#region ♣ 초기화
		public P_CZ_SA_GIRR_REG()
		{
			try
			{
				StartUp.Certify(this);
				this.InitializeComponent();
				this.MainGrids = new FlexGrid[] { _flexH };
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public P_CZ_SA_GIRR_REG(string 페이지명, string 협조전번호)
		{
			try
			{
				StartUp.Certify(this);
				this.InitializeComponent();
				this.MainGrids = new FlexGrid[] { _flexH };

				this.PageName = 페이지명;
				this._협조전번호 = 협조전번호;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
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
			this._flexH.BeginSetting(1, 1, true);
			this._flexH.SetCol("S", "선택", 50, true, CheckTypeEnum.Y_N);     //2011-09-28, 선택 컬럼 추가, 최승애
			this._flexH.SetCol("CD_ITEM", "품목코드", 100, 20, true);
			this._flexH.SetCol("NM_ITEM", "품목명", 140, 50, false);
			this._flexH.SetCol("STND_ITEM", "규격", 120, 50, false);
			this._flexH.SetCol("UNIT", "단위", 40, false);
			this._flexH.SetCol("DT_DUEDATE", "납기일", false);
			this._flexH.SetCol("CD_SL", "창고코드", 60, true);
			this._flexH.SetCol("NM_SL", "창고명", 120, false);
			this._flexH.SetCol("QT_GIR", "수량", 90, 17, true, typeof(decimal), FormatTpType.QUANTITY);

			this._flexH.SetCol("UM", "단가", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			this._flexH.SetCol("AM_GIR", "금액", 90, 17, true, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			this._flexH.SetCol("AM_GIRAMT", "원화금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("AM_VAT", "부가세", 90, 17, false, typeof(decimal), FormatTpType.MONEY);
			this._flexH.SetCol("AMVAT_GIR", "합계금액", 90, 17, false, typeof(decimal), FormatTpType.MONEY);

			this._flexH.SetCol("QT_GIR_IM", "재고수량", false);
			this._flexH.SetCol("NO_SO", "수주반품번호", 110, 20, false);
			this._flexH.SetCol("NO_IO_MGMT", "수불번호", 110, 20, false);
			this._flexH.SetCol("NO_SO_MGMT", "수주번호", 110, 20, false);
			this._flexH.SetCol("GI_PARTNER", "납품처코드", false);
			this._flexH.SetCol("LN_PARTNER", "납품처명", 200, 50, false);
			this._flexH.SetCol("NO_PROJECT", "프로젝트코드", false);
			this._flexH.SetCol("NM_PROJECT", "프로젝트명", 120, 50, false);
			this._flexH.SetCol("DC_RMK", "비고", false);

			DataTable dt출고반품_검사 = _biz.GetMAENV("출고반품_검사");

			string str출고반품_검사 = "000";

			if (dt출고반품_검사.Rows.Count > 0)
				str출고반품_검사 = D.GetString(dt출고반품_검사.Rows[0]["CD_EXC"]);

			if (str출고반품_검사 == "100")
			{
				this._flexH.SetCol("YN_INSPECT", "검사여부", false);
				this._flexH.SetCol("QT_QC_PASS", "검사합격수량", false);
				this._flexH.SetCol("QT_QC_BAD", "검사불량수량", false);
				this._flexH.SetCol("YN_QC_FIX", "검사확정", false);
			}

			if (BASIC.GetMAEXC("SET품 사용유무") == "Y")
			{
				this._flexH.SetCol("CD_ITEM_REF", "SET품목", false);
				this._flexH.SetCol("NM_ITEM_REF", "SET품명", false);
				this._flexH.SetCol("STND_ITEM_REF", "SET규격", false);
			}

			if (BASIC.GetMAEXC("배차사용유무") == "Y")
				this._flexH.SetCol("YN_PICKING", "배송여부", false);

			if (수주Config.부가세포함단가사용())
			{
				this._flexH.SetCol("TP_UM_TAX", "부가세여부", false);
				this._flexH.SetCol("UMVAT_GIR", "부가세포함단가", false);
			}

			if (Config.MA_ENV.YN_UNIT == "Y")
			{
				this._flexH.SetCol("SEQ_PROJECT", "UNIT 항번", false);
				this._flexH.SetCol("CD_UNIT", "UNIT 코드", false);
				this._flexH.SetCol("NM_UNIT", "UNIT 명", false);
				this._flexH.SetCol("STND_UNIT", "UNIT 규격", false);
			}

			this._flexH.SetCodeHelpCol("CD_ITEM", HelpID.P_MA_PITEM_SUB1, ShowHelpEnum.Always, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT", "TP_ITEM", "CD_SL", "NM_SL" }, new string[] { "CD_ITEM", "NM_ITEM", "STND_ITEM", "UNIT_SO", "TP_ITEM", "CD_GISL", "NM_GISL" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
			this._flexH.SetCodeHelpCol("CD_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
			this._flexH.SetCodeHelpCol("NM_SL", HelpID.P_MA_SL_SUB, ShowHelpEnum.Always, new string[] { "CD_SL", "NM_SL" }, new string[] { "CD_SL", "NM_SL" }, Duzon.Common.Forms.Help.ResultMode.SlowMode);
			this._flexH.SetCodeHelpCol("GI_PARTNER", HelpID.P_SA_TPPTR_SUB, ShowHelpEnum.Always, new string[] { "GI_PARTNER", "LN_PARTNER" }, new string[] { "CD_TPPTR", "NM_TPPTR" });
			this._flexH.SetCodeHelpCol("NO_PROJECT", "H_SA_PRJ_SUB", ShowHelpEnum.Always, new string[] { "NO_PROJECT", "NM_PROJECT" }, new string[] { "NO_PROJECT", "NM_PROJECT" });

			this._flexH.SetExceptEditCol("NM_ITEM", "STND_ITEM", "UNIT", "NM_SL", "AM_GIRAMT", "AM_VAT", "NO_SO", "NO_IO_MGMT", "NO_SO_MGMT", "NM_PROJECT", "AMVAT_GIR");

			//의뢰S/L 필수여부에 따라 창고코드 필수사항으로 수정(2011/01/29, BY SMJUNG)
			if (App.SystemEnv.PROJECT사용 && BASIC.GetMAEXC("의뢰 S/L 필수여부") == "100")
				this._flexH.VerifyNotNull = new string[] { "CD_ITEM", "CD_SL", "NO_PROJECT" };
			else if (App.SystemEnv.PROJECT사용)
				this._flexH.VerifyNotNull = new string[] { "CD_ITEM", "NO_PROJECT" };
			if (BASIC.GetMAEXC("의뢰 S/L 필수여부") == "100")
				this._flexH.VerifyNotNull = new string[] { "CD_ITEM", "CD_SL" };

			this._flexH.SetDummyColumn("S");
			this._flexH.SettingVersion = "1.0.0.5";
			this._flexH.VerifyAutoDelete = new string[] { "CD_ITEM" };
			this._flexH.VerifyCompare(_flexH.Cols["QT_GIR"], 0, OperatorEnum.Greater);
			this._flexH.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			this._flexH.StartEdit += new RowColEventHandler(_flex_StartEdit);
			this._flexH.BeforeCodeHelp += new BeforeCodeHelpEventHandler(_flex_BeforeCodeHelp);
			this._flexH.AfterCodeHelp += new AfterCodeHelpEventHandler(_flex_AfterCodeHelp);
			this._flexH.ValidateEdit += new ValidateEditEventHandler(_flex_ValidateEdit);
			this._flexH.AddRow += new EventHandler(btn추가_Click);

			//헤더에 필요없는 SUM 제거 && 반듯이 EndSetting 다음에 코딩해줘야한다.

			if (Config.MA_ENV.YN_UNIT == "Y")
				this._flexH.SetExceptSumCol("UM", "UMVAT_GIR", "SEQ_PROJECT");
			else
				this._flexH.SetExceptSumCol("UM", "UMVAT_GIR");
		}

		protected override void InitPaint()
		{
			DataSet ds;

			try
			{
				this.m_pnlHead.UseCustomLayout = true;
				this.m_pnlHead.IsSearchControl = false;
				this.bpPanelControl1.IsNecessaryCondition = this.bpPanelControl2.IsNecessaryCondition = this.bpPanelControl3.IsNecessaryCondition =
				this.bpPanelControl4.IsNecessaryCondition = this.bpPanelControl9.IsNecessaryCondition = false;
				this.m_pnlHead.InitCustomLayout();

				this.dtp의뢰일자.Mask = GetFormatDescription(DataDictionaryTypes.SA, FormatTpType.YEAR_MONTH_DAY, FormatFgType.INSERT);
				this.dtp의뢰일자.ToDayDate = MainFrameInterface.GetDateTimeToday();
				this.dtp의뢰일자.Text = MainFrameInterface.GetStringToday;

				//PageBase 의 부모함수 이용 : 폼이 올라올 후 폼위에 올라온 컨트롤들을 초기화 시켜주는 역할을 한다.
				//Combo Box 셋팅시 옵션 => N:공백없음, S:공백추가, SC:공백&괄호추가, NC:괄호추가
				//뒤의 코드값들은 DB 에서 정해놓은 파라미터와 매치시켜줘야한다
				ds = this.GetComboData(new string[] { "N;MA_PLANT",
													  "N;PU_C000016",
													  "N;MA_CODEDTL_005;MA_B000040",
													  "S;MA_B000005" });

				ds.Tables[2].PrimaryKey = new DataColumn[] { ds.Tables[2].Columns["CODE"] };

				this.cbo공장.DataSource = ds.Tables[0];
				this.cbo공장.DisplayMember = "NAME";
				this.cbo공장.ValueMember = "CODE";

				this.cbo거래구분.DataSource = new DataView(ds.Tables[1], "CODE IN ('001', '002', '003', '004', '005')", "CODE", DataViewRowState.CurrentRows);
				this.cbo거래구분.DisplayMember = "NAME";
				this.cbo거래구분.ValueMember = "CODE";

				this.cboVAT구분.DataSource = ds.Tables[2];
				this.cboVAT구분.DisplayMember = "NAME";
				this.cboVAT구분.ValueMember = "CODE";

				// 프리폼 초기화
				object[] obj = new object[3];
				obj[0] = "";    //회사코드
				obj[1] = "";    // 의뢰번호
				obj[2] = "";    // 공장

				DataSet dsTemp = _biz.Search(obj);

				this._header.SetBinding(dsTemp.Tables[0], m_pnlHead);
				this._header.ClearAndNewRow();                       // 처음에 행을 하나 만든다. 초기에 추가모드로 하기 위해

				this._flexH.Binding = dsTemp.Tables[1];

				SetControl str = new SetControl();
				str.SetCombobox(cbo화폐단위, ds.Tables[3]);
				this.cbo화폐단위.SelectedValue = "000";

				this.화폐단위셋팅(cbo화폐단위, cur환율);
				this.VAT구분셋팅();

                //if (this._biz.Get영업그룹적용유무 == "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
                //{
                //    this.ctx영업그룹.Enabled = false;
                //}

				this.btn추가.Visible = false;

				this.btn출고적용.Visible = true;

				this.ctx영업그룹.SetCodeValue(Global.MainFrame.LoginInfo.SalesGroupCode);
				this.ctx영업그룹.SetCodeName(Global.MainFrame.LoginInfo.SalesGroupName);

				DataRow row영업그룹 = null;
				try
				{
					row영업그룹 = BASIC.GetSaleGrp(D.GetString(ctx영업그룹.CodeValue));
				}
				catch
				{

				}

				this._단가적용형태 = row영업그룹 == null ? "" : D.GetString(row영업그룹["TP_SALEPRICE"]);

				this.ctx창고.CodeValue = string.Empty;      //2011-09-28, 창고 추가
				this.ctx창고.CodeName = string.Empty;

				if (!string.IsNullOrEmpty(this._협조전번호))
					this.기존협조전불러오기(this._협조전번호, this.LoginInfo.CdPlant);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void InitEvent()
		{
			this.DataChanged += new EventHandler(Page_DataChanged);

			this._header.JobModeChanged += new FreeBindingEventHandler(_header_JobModeChanged);
			this._header.ControlValueChanged += new FreeBindingEventHandler(_header_ControlValueChanged);

			this.txt수주번호.KeyDown += new KeyEventHandler(this.txt수주번호_KeyDown);

			this.btn추가.Click += new EventHandler(this.btn추가_Click);
			this.btn삭제.Click += new EventHandler(this.btn삭제_Click);

			this.btn창고적용.Click += new EventHandler(this.btn창고적용_Click);
			this.btn출고적용.Click += new EventHandler(this.btn출고적용_Click);

			this.ctx매출처.CodeChanged += new EventHandler(this.ctx매출처_CodeChanged);
			this.ctx창고.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx반품형태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx매출처.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx영업그룹.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx반품형태.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);

			this.cboVAT구분.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
			this.cbo화폐단위.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
		}
		#endregion

		#region ♣ 메인버튼이벤트
		protected override bool SaveData()
		{
			if (!base.SaveData()) return false;

			if (!필수값체크) return false;

			DataTable dtL = new DataTable();
			DataTable dtLot = new DataTable();
			DataTable dtSer = new DataTable();
			string NO_GIR = string.Empty;
			string NO_SO = string.Empty;

			if (추가모드여부)
			{
				if (txt의뢰번호.Text == "" || txt의뢰번호.Text == "")
				{
					NO_GIR = (string)GetSeq(LoginInfo.CompanyCode, "SA", "04", dtp의뢰일자.Text.Substring(0, 6));
					txt의뢰번호.Text = NO_GIR;
				}
				else
					NO_GIR = txt의뢰번호.Text;

				_header.CurrentRow["NO_GIR"] = NO_GIR;

				dtL = _flexH.DataTable.Clone();
				decimal i = 1;

				foreach (DataRow row in _flexH.DataTable.Rows)
				{
					if (row.RowState == DataRowState.Deleted) continue;
					row["SEQ_GIR"] = i++;

					//배송정보 TRACK 기능 => FG_TRACK : SO(수주등록), M(창고이동, 출고요청등록), R(출고반품의뢰등록)
					row["FG_TRACK"] = "R";
					
					dtL.ImportRow(row);
				}
			}
			else
			{
				NO_GIR = txt의뢰번호.Text;
				dtL = _flexH.GetChanges();
			}

			if (!Verify()) return false;

			DataTable dtH = _header.GetChanges();
			if (dtH == null && dtL == null) return true;

			bool bSuccess = false;
			bSuccess = _biz.Save(dtH, dtL, dtLot, dtSer, NO_GIR, 추가모드여부, true, this, NO_SO);

			if (!bSuccess) return false;

			_header.AcceptChanges();
			_flexH.AcceptChanges();
			Page_DataChanged(null, null);

			return true;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!BeforeSearch()) return;

				string 반품여부 = "Y";

				P_SA_GIR_SCH_SUB dlg = new P_SA_GIR_SCH_SUB(반품여부);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					this.기존협조전불러오기(D.GetString(dlg.returnParams[0]), D.GetString(dlg.returnParams[1]));
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void 기존협조전불러오기(string 의뢰번호, string 공장)
		{
			try
			{
				DataSet ds = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															 의뢰번호,
															 공장 });

				this._header.SetDataTable(ds.Tables[0]);
				this._flexH.Binding = ds.Tables[1];
				this._매출형태 = ds.Tables[1].Rows[0]["TP_IV"].ToString();
				this._단가적용형태 = ds.Tables[1].Rows[0]["TP_SALEPRICE"].ToString();  //2011-09-05, 최승애, 단가적용형태 추가함.
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

				this._헤더수정여부 = true;

				if (this._flexH.DataTable != null)
				{
					this._flexH.DataTable.Rows.Clear();
					this._flexH.AcceptChanges();
				}

				this._header.ClearAndNewRow();
				this.cbo화폐단위.SelectedValue = "000";
				this.화폐단위셋팅(cbo화폐단위, cur환율);
				this.VAT구분셋팅();

				this.적용시컨트롤Enabled = true;

				this.ctx영업그룹.SetCodeValue(Global.MainFrame.LoginInfo.SalesGroupCode);
				this.ctx영업그룹.SetCodeName(Global.MainFrame.LoginInfo.SalesGroupName);
			}
			catch (System.Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool BeforeDelete()
		{
			if (!base.BeforeDelete()) return false;
			if (ShowMessage(공통메세지.자료를삭제하시겠습니까) != DialogResult.Yes) return false;
			return true;
		}

		public override void OnToolBarDeleteButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarDeleteButtonClicked(sender, e);

				if (!BeforeDelete()) return;

				if (_biz.Delete(txt의뢰번호.Text))
				{
					ShowMessage(공통메세지.자료가정상적으로삭제되었습니다);
					this.OnToolBarAddButtonClicked(sender, e);       // 삭제 후 바로 추가모드로 해준다.
				}
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

				if (!_flexH.HasNormalRow)
				{
					this.OnToolBarDeleteButtonClicked(null, null);
					return;
				}

				if (!BeforeSave()) return;

				MsgControl.ShowMsg("저장 중 입니다. 잠시만 기다려 주십시요.");

				if (MsgAndSave(PageActionMode.Save))
				{
					MsgControl.CloseMsg();
					ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
			}
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if (!BeforePrint() || !_flexH.HasNormalRow)
					return;

				PrintRDF rdf = new PrintRDF("R_SA_GIRR_REG_0", false);

				List<string> List = new List<string>();
				List.Add(MA.Login.회사코드);
				List.Add(txt의뢰번호.Text);
				List.Add(D.GetString(cbo공장.SelectedValue));

				DataTable dt = _biz.Print(List.ToArray());

				Hashtable Data_H = new Hashtable();
				Data_H.Add("의뢰번호", txt의뢰번호.Text);
				Data_H.Add("의뢰일자", dtp의뢰일자.Text);
				Data_H.Add("거래처", ctx매출처.CodeName);
				Data_H.Add("공장", D.GetString(cbo공장.Text));
				Data_H.Add("거래구분", D.GetString(cbo거래구분.Text));
				Data_H.Add("영업그룹", ctx영업그룹.CodeName);
				Data_H.Add("담당장", ctx담당자.CodeName);
				Data_H.Add("반품형태", ctx반품형태.CodeName);
				Data_H.Add("VAT구분", D.GetString(cboVAT구분.Text));
				Data_H.Add("VAT율", D.GetString(curVAT율.Text));
				Data_H.Add("유무환구분", txt유무환구분.Text);
				Data_H.Add("화폐단위", D.GetString(cbo화폐단위.Text));
				Data_H.Add("환율", cur환율.Text);
				Data_H.Add("계산서처리", this._계산서처리명);
				Data_H.Add("비고", txt비고.Text);
				Data_H.Add("단가유형", this._단가유형코드);
				Data_H.Add("납품처", this.ctx매출처.CodeName);

				rdf.Data = Data_H;
				rdf.SetDataTable = dt;
				rdf.ShowDialog();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			Properties.Settings.Default.Save();

			return base.OnToolBarExitButtonClicked(sender, e);
		}
		#endregion

		#region ♣ 화면내 버튼 이벤트
		private void txt수주번호_KeyDown (object sender, KeyEventArgs e)
		{
			string query;
            DataTable dt;

			try
			{
                if (e.KeyCode != Keys.Enter) return;

				query = @"SELECT SH.CD_SALEGRP,
								 SG.NM_SALEGRP,
								 SH.CD_PARTNER,
								 MP.LN_PARTNER,
								 ST.TP_BUSI,
								 SH.TP_VAT,
								 SH.CD_EXCH
						  FROM SA_SOH SH WITH(NOLOCK)
						  JOIN (SELECT SL.CD_COMPANY, SL.NO_SO 
								FROM SA_SOL SL WITH(NOLOCK)
								WHERE ISNULL(SL.QT_GI, 0) > 0
								GROUP BY SL.CD_COMPANY, SL.NO_SO) SL
						  ON SL.CD_COMPANY = SH.CD_COMPANY AND SL.NO_SO = SH.NO_SO
						  LEFT JOIN MA_PARTNER MP WITH(NOLOCK) ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER  
						  LEFT JOIN MA_SALEGRP SG WITH(NOLOCK) ON SG.CD_COMPANY = SH.CD_COMPANY AND SG.CD_SALEGRP = SH.CD_SALEGRP
						  LEFT JOIN SA_TPSO ST WITH(NOLOCK) ON ST.CD_COMPANY = SH.CD_COMPANY AND ST.TP_SO = SH.TP_SO
						  WHERE SH.CD_COMPANY = '" + this.LoginInfo.CompanyCode + "'" + Environment.NewLine +
						 "AND SH.NO_SO = '" + this.txt수주번호.Text + "'";

                dt = DBHelper.GetDataTable(query);

                if (dt == null || dt.Rows.Count == 0)
                {
                    this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
                }
                else
                {
                    this.ctx매출처.CodeValue = D.GetString(dt.Rows[0]["CD_PARTNER"]);
                    this.ctx매출처.CodeName = D.GetString(dt.Rows[0]["LN_PARTNER"]);
                    this._header.CurrentRow["CD_PARTNER"] = this.ctx매출처.CodeValue;
                    this._header.CurrentRow["LN_PARTNER"] = this.ctx매출처.CodeName;

                    this.ctx영업그룹.CodeValue = D.GetString(dt.Rows[0]["CD_SALEGRP"]);
                    this.ctx영업그룹.CodeName = D.GetString(dt.Rows[0]["NM_SALEGRP"]);
                    this._header.CurrentRow["CD_SALEGRP"] = this.ctx영업그룹.CodeValue;
                    this._header.CurrentRow["NM_SALEGRP"] = this.ctx영업그룹.CodeName;

                    this.영업그룹변경시셋팅(ctx영업그룹.CodeValue);

                    this.cbo거래구분.SelectedValue = D.GetString(dt.Rows[0]["TP_BUSI"]);
                    this._header.CurrentRow["TP_BUSI"] = this.cbo거래구분.SelectedValue;
                    this.cboVAT구분.SelectedValue = D.GetString(dt.Rows[0]["TP_VAT"]);
                    this._header.CurrentRow["TP_VAT"] = this.cboVAT구분.SelectedValue;

                    this.VAT구분셋팅();

                    this.cbo화폐단위.SelectedValue = D.GetString(dt.Rows[0]["CD_EXCH"]);
                    this._header.CurrentRow["CD_EXCH"] = this.cbo화폐단위.SelectedValue;

                    this.화폐단위셋팅(this.cbo화폐단위, this.cur환율);
                }
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn출고적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!Chk수주번호 || !Chk의뢰일자 || !Chk거래처 || !Chk공장 || !Chk거래구분 || !Chk영업그룹 || !Chk담당자 || !Chk과세구분 || !Chk환종 || !Chk반품형태) return;

				if (예외추가여부)
				{
					ShowMessage("예외추가건이 존재합니다.");
					return;
				}

				if (수주반품적용여부)
				{
					ShowMessage("수주반품적용건이 존재합니다.");
					return;
				}

				P_SA_GIRR_REG_SUB dlg = new P_SA_GIRR_REG_SUB(this.txt수주번호.Text);
				
				if (dlg.ShowDialog() != DialogResult.OK)
					return;

				_헤더수정여부 = true;
				_header.JobMode = JobModeEnum.추가후수정;
				txt의뢰번호.Text = "";
				_header.CurrentRow["NO_GIR"] = "";

				DataTable dt반품적용 = dlg.출고테이블;
				int SeqGir = 1;

				_flexH.Redraw = false;

				foreach (DataRow row수주 in dt반품적용.Rows)
				{
					DataRow newrow = _flexH.DataTable.NewRow();

					foreach (DataColumn col in dt반품적용.Columns)
					{
						if (!_flexH.DataTable.Columns.Contains(col.ColumnName)) continue;
						newrow[col.ColumnName] = row수주[col.ColumnName];
					}

					newrow["SEQ_GIR"] = SeqGir;
					newrow["FG_TRACK"] = "R";

					if (D.GetString(row수주["CD_EXCH"]) == "000")
					{
						if (D.GetDecimal(row수주["QT_IO_ORI"]) == D.GetDecimal(row수주["QT_GIR_IM_ORI"]) && D.GetDecimal(row수주["QT_RETURN"]) == decimal.Zero)
						{
							newrow["AM_GIR"] = row수주["AM_GIR_ORIGINAL"];
							newrow["AM_GIRAMT"] = row수주["AM_GIRAMT_ORIGINAL"];
							newrow["AM_VAT"] = row수주["AM_VAT_ORIGINAL"];
						}
						else
						{
							newrow["AM_GIRAMT"] = this.원화계산(D.GetDecimal(newrow["AM_GIR"]) * cur환율.DecimalValue);            //원화금액
                            newrow["AM_VAT"] = this.원화계산(D.GetDecimal(newrow["AM_GIRAMT"]) * (curVAT율.DecimalValue / 100)); //부가세
						}
					}
					else
					{
                        newrow["AM_GIRAMT"] = this.원화계산(D.GetDecimal(newrow["AM_GIR"]) * cur환율.DecimalValue);            //원화금액
                        newrow["AM_VAT"] = this.원화계산(D.GetDecimal(newrow["AM_GIRAMT"]) * (curVAT율.DecimalValue / 100)); //부가세
					}

					newrow["AMVAT_GIR"] = D.GetDecimal(newrow["AM_GIRAMT"]) + D.GetDecimal(newrow["AM_VAT"]);

					if (BASIC.GetMAEXC("배차사용유무") == "Y")
						newrow["YN_PICKING"] = _배송여부;

					if (!수주Config.부가세포함단가사용())
                        newrow["UMVAT_GIR"] = this.원화계산(D.GetDecimal(newrow["AMVAT_GIR"]) / D.GetDecimal(newrow["QT_GIR"]));

					_flexH.DataTable.Rows.Add(newrow);

					SeqGir++;
				}

				_flexH.Redraw = true;
				_flexH.SumRefresh();
				_flexH.Row = _flexH.BottomRow;

				_flexH.IsDataChanged = true;
				적용시컨트롤Enabled = false;
				Page_DataChanged(null, null);
			}
			catch (Exception ex)
			{
				_flexH.Redraw = true;
				MsgEnd(ex);
			}
		}

		private void btn추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (cbo공장.SelectedValue == null || cbo공장.SelectedValue.ToString() == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lbl공장.Text);
					cbo공장.Focus();
					return;
				}

				// 거래처
				if (ctx매출처.CodeValue == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lbl매출처.Text);
					ctx매출처.Focus();
					return;
				}

				//과세구분
				if (cboVAT구분.SelectedValue == null || cboVAT구분.SelectedValue.ToString() == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lblVAT구분.Text);
					cboVAT구분.Focus();
					return;
				}

				// 환종
				if (cbo화폐단위.SelectedValue == null || cbo화폐단위.SelectedValue.ToString() == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lbl화폐단위.Text);
					cbo화폐단위.Focus();
					return;
				}

				if (!Chk의뢰일자 || !Chk거래처 || !Chk공장 || !Chk거래구분 || !Chk영업그룹 || !Chk담당자 || !Chk과세구분 || !Chk환종 || !Chk반품형태) return;

				if (수주반품적용여부)
				{
					ShowMessage("수주반품적용건이 존재합니다.");
					return;
				}

				if (출고적용여부)
				{
					ShowMessage("출고적용건이 존재합니다.");
					return;
				}

				_flexH.Rows.Add();
				//_flex.Row = _flex.Rows.Count - 1;
				_flexH.Row = _flexH.BottomRow;
				if (!추가모드여부)
				{
					_flexH[_flexH.Row, "SEQ_GIR"] = 그리드항번최대값 + 1;
					btn삭제.Enabled = true;
				}

				_flexH[_flexH.Row, "GI_PARTNER"] = this.ctx매출처.CodeValue;
				_flexH[_flexH.Row, "LN_PARTNER"] = this.ctx매출처.CodeName;
				_flexH[_flexH.Row, "YN_INSPECT"] = "N";
				_flexH[_flexH.Row, "TP_IV"] = _매출형태;
				_flexH[_flexH.Row, "NO_LC"] = "";
				_flexH[_flexH.Row, "NO_EMP"] = ctx담당자.CodeValue;
				_flexH[_flexH.Row, "FG_TRACK"] = "R";

				if (_flexH.Row == _flexH.Rows.Fixed)
				{
					_flexH["DT_DUEDATE"] = dtp의뢰일자.Text;
				}
				else if (_flexH.Row > _flexH.Rows.Fixed)   //if (_flex.Rows.Count > 3) --> 방식이 고정되어 있어서 변경
				{
					_flexH[_flexH.Row, "DT_DUEDATE"] = _flexH[_flexH.Row - 1, "DT_DUEDATE"];
					_flexH.Row = _flexH.BottomRow;
				}

				if (BASIC.GetMAEXC("배차사용유무") == "Y")
					_flexH[_flexH.Row, "YN_PICKING"] = _배송여부;

				_flexH.AddFinished();
				_flexH.Col = _flexH.Cols.Fixed;
				_flexH.Focus();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn삭제_Click(object sender, EventArgs e)
		{
			try
			{
				this._flexH.Focus();
				this._flexH.Row = _flexH.Rows.Count - 1;

				DataRow[] dr = _flexH.DataTable.Select("S = 'Y'", "", DataViewRowState.CurrentRows);

				if (dr == null || dr.Length == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}

				foreach (DataRow drRow in dr)
				{
					if (!추가모드여부)
					{
						if (D.GetDecimal(drRow["QT_GI"]) > 0)
						{
							ShowMessage("이미 반품출고되어 삭제가 불가능합니다.");
							return;
						}
					}

					drRow.Delete();
				}

				if (!_flexH.HasNormalRow)
				{
					if (추가모드여부)
					{
                        //ctx매출처.Enabled = true;
                        //cbo공장.Enabled = true;
                        //if (_biz.Get영업그룹적용유무 != "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
                        //    ctx영업그룹.Enabled = true;
                        //ctx담당자.Enabled = true;
                        //cboVAT구분.Enabled = true;
                        //cbo화폐단위.Enabled = true;
                        //if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        //    cur환율.Enabled = false;
                        //else
                        //{
                        //    if (D.GetString(cbo화폐단위.SelectedValue) != "000")    //KRW
                        //        cur환율.Enabled = true;
                        //    else
                        //        cur환율.Enabled = false;
                        //}
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void btn창고적용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!_flexH.HasNormalRow || _flexH.DataTable == null)
					return;

				DataRow[] drs = _flexH.DataTable.Select("", "", DataViewRowState.CurrentRows);

				_flexH.Redraw = false;
				try
				{
					foreach (DataRow dr in drs)
					{
						if (dr != null)
						{
							dr["CD_SL"] = ctx창고.CodeValue;
							dr["NM_SL"] = ctx창고.CodeName;
						}
					}
				}
				finally
				{
					_flexH.Redraw = true;
				}

				ShowMessage(공통메세지._작업을완료하였습니다, btn창고적용.Text);

			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#region ♣ 그리드 이벤트

		#region -> 그리드 변경시작체 체크이벤트(_flex_StartEdit)

		private void _flex_StartEdit(object sender, RowColEventArgs e)
		{
			try
			{
				switch (_flexH.Cols[e.Col].Name)
				{
					case "DT_DUEDATE":
					case "CD_SL":
					case "QT_GIR":
					case "UM":
					case "AM_GIR":
					case "QT_GIR_IM":
					case "NO_PROJECT" :
						if (!추가모드여부 && _flexH.CDecimal(_flexH[e.Row, "QT_GI"]) > 0)
						{
							e.Cancel = true;
							return;
						}
						break;
					case "CD_ITEM":
						string 관련수불번호 = D.GetString(_flexH[e.Row, "NO_IO_MGMT"]);
						string 관련수주번호 = D.GetString(_flexH[e.Row, "NO_SO"]);
						if (관련수불번호 != "" || 관련수주번호 != "")
						{
							e.Cancel = true;
							return;
						}
						break;
					case "GI_PARTNER" :
						if (_flexH.CDecimal(_flexH["QT_GI"]) > 0m || _flexH.CDecimal(_flexH["QT_GI_IM"]) > 0m)
						{
							e.Cancel = true;
							return;
						}
						break;
					case "YN_INSPECT":
						if (!추가모드여부)
						{
							e.Cancel = true;
							return;
						}
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion
		
		#region -> 그리드 도움창 호출전 세팅 이벤트(_flex_BeforeCodeHelp)

		private void _flex_BeforeCodeHelp(object sender, BeforeCodeHelpEventArgs e)
		{
			try
			{
				switch (_flexH.Cols[e.Col].Name)
				{
					case "CD_ITEM":
						string 관련수불번호 = _flexH[e.Row, "NO_IO_MGMT"].ToString();
						string 관련수주번호 = _flexH[e.Row, "NO_SO"].ToString();
						if (관련수불번호 != "" || 관련수주번호 != "")
						{
							e.Cancel = true;
							return;
						}
						e.Parameter.P09_CD_PLANT = cbo공장.SelectedValue.ToString();
						break;
					case "CD_SL":
					case "NM_SL":
						if (_flexH.CDecimal(_flexH["QT_GI"]) > 0)
						{
							e.Cancel = true;
							return;
						}
						e.Parameter.P09_CD_PLANT = cbo공장.SelectedValue.ToString();
						break;
					case "NO_PROJECT":
						if (_flexH.CDecimal(_flexH["QT_GI"]) > 0)
						{
							e.Cancel = true;
							return;
						}
						e.Parameter.P41_CD_FIELD1 = "프로젝트";             //도움창 이름 찍어줄 값
						e.Parameter.P14_CD_PARTNER = ctx매출처.CodeValue;    //거래처코드
						e.Parameter.P63_CODE3 = ctx매출처.CodeName;          //거래처명
						e.Parameter.P17_CD_SALEGRP = ctx영업그룹.CodeValue;  //영업그룹코드
						e.Parameter.P62_CODE2 = ctx영업그룹.CodeName;        //영업그룹명
						break;
					case "GI_PARTNER":
						if (_flexH.CDecimal(_flexH["QT_GI"]) > 0 || _flexH.CDecimal(_flexH["QT_GI_IM"]) > 0)
						{
							e.Cancel = true;
							return;
						}
						e.Parameter.P14_CD_PARTNER = this.ctx매출처.CodeValue;
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> 그리드 도움창 호출후 변경 이벤트(_flex_AfterCodeHelp)

		private void _flex_AfterCodeHelp(object sender, AfterCodeHelpEventArgs e)
		{
			try
			{
				HelpReturn helpReturn = e.Result;

				switch (_flexH.Cols[e.Col].Name)
				{
					case "CD_ITEM":
						if(e.Result.DialogResult == DialogResult.Cancel) return;

						bool First = true;
						_flexH.Redraw = false;
						_flexH.SetDummyColumnAll();

						DataTable dtUmFixed = null;
						if (_biz.Get특수단가적용 == "003")
						{
							string multiItem = Common.MultiString(helpReturn.Rows, "CD_ITEM", "|");
							dtUmFixed = _biz.SearchUmFixed(ctx매출처.CodeValue, D.GetString(cbo공장.SelectedValue), multiItem);
							dtUmFixed.PrimaryKey = new DataColumn[] { dtUmFixed.Columns["CD_ITEM"] };
						}

						foreach(DataRow row in helpReturn.Rows)
						{
							if (First)     /* 새로 품목을 입력하는 경우 */
							{
								_flexH[e.Row, "CD_ITEM"] = row["CD_ITEM"];
								_flexH[e.Row, "NM_ITEM"] = row["NM_ITEM"];
								_flexH[e.Row, "STND_ITEM"] = row["STND_ITEM"];
								_flexH[e.Row, "UNIT"] = row["UNIT_SO"];
								_flexH[e.Row, "CD_SL"] = row["CD_GISL"];     //2011-09-28, 최승애, 입고창고(CD_SL)이 아닌 출고창고(CD_GISL)로 수정함.
								_flexH[e.Row, "NM_SL"] = row["NM_GISL"];     //2011-09-28, 최승애, 입고창고(NM_SL)이 아닌 출고창고(NM_GISL)로 수정함.
								_flexH[e.Row, "TP_ITEM"] = row["TP_ITEM"];
								_flexH[e.Row, "UNIT_SO_FACT"] = _flexH.CDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];
								_flexH[e.Row, "YN_INSPECT"] = "N";
								_flexH[e.Row, "TP_IV"] = _매출형태;
								_flexH[e.Row, "NO_LC"] = "";
								_flexH[e.Row, "NO_EMP"] = ctx담당자.CodeValue;
								_flexH[e.Row, "GI_PARTNER"] = this.ctx매출처.CodeValue;
								_flexH[e.Row, "LN_PARTNER"] = this.ctx매출처.CodeName;

								string 품목과세구분 = D.GetString(row["FG_TAX_SA"]);

								_flexH[e.Row, "TP_VAT"] = D.GetString(cboVAT구분.SelectedValue);
								_flexH[e.Row, "RT_VAT"] = curVAT율.DecimalValue;

								decimal dUM_BASE = _biz.UmSearch(D.GetString(row["CD_ITEM"]), ctx매출처.CodeValue, this._단가유형코드, D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp의뢰일자.Text);

                                _flexH[e.Row, "UM"] = this.외화계산(dUM_BASE);

								decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(_flexH[e.Row, "QT_GIR"]));
                                decimal 단가 = this.외화계산(D.GetDecimal(_flexH[e.Row, "UM"]));

								decimal 외화금액 = decimal.Zero;
								decimal 원화금액 = decimal.Zero;
								decimal 부가세 = decimal.Zero;
								decimal 합계 = decimal.Zero;

                                외화금액 = this.외화계산(단가 * 의뢰수량);
                                원화금액 = cur환율.DecimalValue == 0m ? 0m : this.원화계산(외화금액 * cur환율.DecimalValue);
                                부가세 = this.원화계산(원화금액 * (curVAT율.DecimalValue / 100));
								합계 = 원화금액 + 부가세;
                                _flexH[e.Row, "UMVAT_GIR"] = 의뢰수량 == 0m ? 0m : this.원화계산(합계 / 의뢰수량);

								_flexH[e.Row, "AM_GIR"] = 외화금액;
								_flexH[e.Row, "AM_GIRAMT"] = 원화금액;
								_flexH[e.Row, "AM_VAT"] = 부가세;
								_flexH[e.Row, "AMVAT_GIR"] = 합계;

								if (BASIC.GetMAEXC("배차사용유무") == "Y")
									_flexH[e.Row, "YN_PICKING"] = _배송여부;

								First = false;
							}
							else
							{
								_flexH.Rows.Add();
								_flexH.Row = _flexH.Rows.Count - 1;
								_flexH[_flexH.Row, "CD_ITEM"] = row["CD_ITEM"];
								_flexH[_flexH.Row, "NM_ITEM"] = row["NM_ITEM"];
								_flexH[_flexH.Row, "STND_ITEM"] = row["STND_ITEM"];
								_flexH[_flexH.Row, "UNIT"] = row["UNIT_SO"];
								_flexH[_flexH.Row, "CD_SL"] = row["CD_GISL"];       //2011-09-28, 최승애, 입고창고(CD_SL)이 아닌 출고창고(CD_GISL)로 수정함.
								_flexH[_flexH.Row, "NM_SL"] = row["NM_GISL"];       //2011-09-28, 최승애, 입고창고(NM_SL)이 아닌 출고창고(NM_GISL)로 수정함.
								_flexH[_flexH.Row, "TP_ITEM"] = row["TP_ITEM"];
								_flexH[_flexH.Row, "UNIT_SO_FACT"] = _flexH.CDecimal(row["UNIT_SO_FACT"]) == 0 ? 1 : row["UNIT_SO_FACT"];
								_flexH[_flexH.Row, "SEQ_GIR"] = 그리드항번최대값 + 1;
								_flexH[_flexH.Row, "YN_INSPECT"] = "N";
								_flexH[_flexH.Row, "TP_IV"] = this._매출형태;
								_flexH[_flexH.Row, "NO_LC"] = "";
								_flexH[_flexH.Row, "NO_EMP"] = ctx담당자.CodeValue;
								_flexH[_flexH.Row, "GI_PARTNER"] = this.ctx매출처.CodeValue;
								_flexH[_flexH.Row, "LN_PARTNER"] = this.ctx매출처.CodeName;

								string 품목과세구분 = D.GetString(row["FG_TAX_SA"]);

								_flexH[_flexH.Row, "TP_VAT"] = D.GetString(cboVAT구분.SelectedValue);
								_flexH[_flexH.Row, "RT_VAT"] = curVAT율.DecimalValue;

								decimal dUM_BASE = _biz.UmSearch(D.GetString(row["CD_ITEM"]), ctx매출처.CodeValue, this._단가유형코드, D.GetString(cbo화폐단위.SelectedValue), _단가적용형태, dtp의뢰일자.Text);
                                _flexH[_flexH.Row, "UM"] = this.외화계산(dUM_BASE);

								if (BASIC.GetMAEXC("배차사용유무") == "Y")
									_flexH[_flexH.Row, "YN_PICKING"] = _배송여부;
								
								_flexH[_flexH.Row, "DT_DUEDATE"] = _flexH[_flexH.Row - 1, "DT_DUEDATE"];

								_flexH.AddFinished();
							}
						}

						_flexH.RemoveDummyColumnAll();
						
						_flexH.Col = _flexH.Cols.Fixed;
						_flexH.Redraw = true;
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> 그리드 Cell 수정시 변경 이벤트(_flex_ValidateEdit)

		private void _flex_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			try
			{
				string OldValue = _flexH.GetData(e.Row, e.Col).ToString();
				string NewValue = _flexH.EditData;

				if (OldValue.ToUpper() == NewValue.ToUpper()) return;

				decimal rtVat = curVAT율.DecimalValue;

				if (D.GetString(_flexH[e.Row, "TP_VAT"]) != string.Empty)
					rtVat = D.GetDecimal(_flexH[e.Row, "RT_VAT"]);

				switch (_flexH.Cols[e.Col].Name)
				{
					case "QT_GIR":
						//반품수량체크 프로스져를 호출한다.
						if (_flexH[e.Row, "NO_IO_MGMT"].ToString() == "" || _biz.Check(txt의뢰번호.Text, _flexH.CDecimal(_flexH[e.Row, "SEQ_GIR"]), _flexH[e.Row, "NO_IO_MGMT"].ToString(), _flexH.CDecimal(_flexH[e.Row, "NO_IOLINE_MGMT"]), _flexH.CDecimal(NewValue)))
						{
							decimal 의뢰수량 = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(NewValue));
                            decimal 단가 = this.외화계산(D.GetDecimal(_flexH[e.Row, "UM"]));

							decimal 외화금액 = decimal.Zero;
							decimal 원화금액 = decimal.Zero;
							decimal 부가세 = decimal.Zero;
							decimal 합계 = decimal.Zero;

                            외화금액 = this.외화계산(단가 * 의뢰수량);
                            원화금액 = cur환율.DecimalValue == 0m ? 0m : this.원화계산(외화금액 * cur환율.DecimalValue);
                            부가세 = this.원화계산(원화금액 * (rtVat / 100));
							합계 = 원화금액 + 부가세;
                            _flexH["UMVAT_GIR"] = 의뢰수량 == 0m ? 0m : this.원화계산(합계 / 의뢰수량);

							_flexH[e.Row, "AM_GIR"] = 외화금액;
							_flexH[e.Row, "AM_GIRAMT"] = 원화금액;
							_flexH[e.Row, "AM_VAT"] = 부가세;
							_flexH[e.Row, "AMVAT_GIR"] = 합계;

							if (_flexH.CDecimal(_flexH[e.Row, "UNIT_SO_FACT"]) == 0)
								_flexH[e.Row, "QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(NewValue) * 1);
							else
								_flexH[e.Row, "QT_GIR_IM"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(NewValue) * D.GetDecimal(_flexH[e.Row, "UNIT_SO_FACT"]));
						}
						else
							_flexH[e.Row, "QT_GIR"] = Unit.수량(DataDictionaryTypes.SA, D.GetDecimal(OldValue));
						break;

					case "UM":
                        _flexH[e.Row, "AM_GIR"] = this.외화계산(D.GetDecimal(_flexH[e.Row, "QT_GIR"]) * D.GetDecimal(NewValue));
                        _flexH[e.Row, "AM_GIRAMT"] = this.원화계산(_flexH.CDecimal(_flexH[e.Row, "AM_GIR"]) * cur환율.DecimalValue);
                        _flexH[e.Row, "AM_VAT"] = this.원화계산(_flexH.CDecimal(_flexH[e.Row, "AM_GIRAMT"]) * (rtVat / 100));
						_flexH[e.Row, "AMVAT_GIR"] = D.GetDecimal(_flexH[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flexH[e.Row, "AM_VAT"]);
						break;

					case "AM_GIR":
						if (_flexH.CDecimal(_flexH[e.Row, "QT_GIR"]) != 0m)
                            _flexH[e.Row, "UM"] = this.외화계산(D.GetDecimal(NewValue) / D.GetDecimal(_flexH[e.Row, "QT_GIR"]));
						else
							_flexH[e.Row, "UM"] = 0m;

                        _flexH[e.Row, "AM_GIRAMT"] = this.원화계산(_flexH.CDecimal(NewValue) * cur환율.DecimalValue);
                        _flexH[e.Row, "AM_VAT"] = this.원화계산(_flexH.CDecimal(_flexH[e.Row, "AM_GIRAMT"]) * (rtVat / 100));
						_flexH[e.Row, "AMVAT_GIR"] = D.GetDecimal(_flexH[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flexH[e.Row, "AM_VAT"]);
						break;

					case "DT_DUEDATE":
						if (!D.StringDate.IsValidDate(NewValue, false, ""))
						{
							e.Cancel = true;
							return;
						}
						_flexH["DT_REQGI"] = NewValue;
						break;

					case "UM_SO":
                        _flexH[e.Row, "AM_SO"] = this.외화계산(D.GetDecimal(_flexH[e.Row, "QT_GIR"]) * D.GetDecimal(NewValue));
						_flexH[e.Row, "AM_SOAMT"] = 0;
                        _flexH[e.Row, "AM_SOVAT"] = this.원화계산(_flexH.CDecimal(_flexH[e.Row, "AM_SOAMT"]) * (rtVat / 100));
						break;

					case "AM_SO":
						if (_flexH.CDecimal(_flexH[e.Row, "QT_GIR"]) != 0)
                            _flexH[e.Row, "UM_SO"] = this.외화계산(D.GetDecimal(NewValue) / D.GetDecimal(_flexH[e.Row, "QT_GIR"]));
						else
							_flexH[e.Row, "UM_SO"] = 0m;

						_flexH[e.Row, "AM_SOAMT"] = 0;
                        _flexH[e.Row, "AM_SOVAT"] = this.원화계산(_flexH.CDecimal(_flexH[e.Row, "AM_SOAMT"]) * (rtVat / 100));
						break;

					case "TP_VAT":
						_flexH[e.Row, "RT_VAT"] = BASIC.GetTPVAT(NewValue);
                        _flexH[e.Row, "AM_VAT"] = this.원화계산(D.GetDecimal(_flexH[e.Row, "AM_GIRAMT"]) * (D.GetDecimal(_flexH[e.Row, "RT_VAT"]) / 100));
						_flexH[e.Row, "AMVAT_GIR"] = D.GetDecimal(_flexH[e.Row, "AM_GIRAMT"]) + D.GetDecimal(_flexH[e.Row, "AM_VAT"]);

						break;

					case "UMVAT_GIR":
                        _flexH["AMVAT_GIR"] = this.원화계산(D.GetDecimal(_flexH["QT_GIR"]) * D.GetDecimal(NewValue));
						if (Global.MainFrame.LoginInfo.CompanyLanguage == Language.KR)
							_flexH["AM_GIRAMT"] = Decimal.Round(D.GetDecimal(_flexH["AMVAT_GIR"]) * (100 / (100 + rtVat)), MidpointRounding.AwayFromZero);
						else
							_flexH["AM_GIRAMT"] = D.GetDecimal(UDecimal.GetConvertFormatData(DataDictionaryTypes.SA, FormatTpType.MONEY, FormatFgType.INSERT, (D.GetDecimal(_flexH["AMVAT_GIR"]) * (100 / (100 + rtVat)))));
						_flexH["AM_VAT"] = D.GetDecimal(_flexH["AMVAT_GIR"]) - D.GetDecimal(_flexH["AM_GIRAMT"]);
                        _flexH["AM_GIR"] = cur환율.DecimalValue == 0m ? 0m : this.외화계산(D.GetDecimal(_flexH["AM_GIRAMT"]) / cur환율.DecimalValue);
                        _flexH["UM"] = D.GetDecimal(_flexH["QT_GIR"]) == 0m ? 0m : this.외화계산(D.GetDecimal(_flexH["AM_GIR"]) / D.GetDecimal(_flexH["QT_GIR"]));
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#endregion

		#region ♣ 도움창 이벤트

		#region -> 도움창 Clear 이벤트(OnBpControl_CodeChanged)

		private void ctx매출처_CodeChanged(object sender, EventArgs e)
		{
			try
			{
				BpCodeTextBox bpControl = sender as BpCodeTextBox;
				if (bpControl == null) return;

				if (bpControl.Name == this.ctx매출처.Name)
				{
					if (_biz.Get영업그룹적용유무 == "Y")
					{
						ctx영업그룹.CodeValue = "";
						ctx영업그룹.CodeName = "";
						_header.CurrentRow["CD_SALEGRP"] = "";
						_header.CurrentRow["NM_SALEGRP"] = "";
					}
					if (_biz.Get담당자적용유무 == "Y")
					{
						ctx담당자.CodeValue = "";
						ctx담당자.CodeName = "";
						_header.CurrentRow["NO_EMP"] = "";
						_header.CurrentRow["NM_KOR"] = "";
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> 도움창 호출전 세팅 이벤트(Control_QueryBefore)

		private void Control_QueryBefore(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			try
			{
				switch (e.HelpID)
				{
					case HelpID.P_MA_SL_SUB:
						e.HelpParam.P09_CD_PLANT = cbo공장.SelectedValue == null ? Global.MainFrame.LoginInfo.CdPlant : D.GetString(cbo공장.SelectedValue);
						break;
					case HelpID.P_PU_EJTP_SUB:
						e.HelpParam.P61_CODE1 = "041|042|";
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		#endregion

		#region -> 도움창 호출후 변경 이벤트(Control_QueryAfter)

		private void Control_QueryAfter(object sender, Duzon.Common.BpControls.BpQueryArgs e)
		{
			string 유무환구분;
			DataTable dt;

			try
			{
				if (e.DialogResult == DialogResult.OK)
				{
					System.Data.DataRow[] rows = e.HelpReturn.Rows;
					switch (e.HelpID)
					{
						case HelpID.P_PU_EJTP_SUB:
							dt = Global.MainFrame.FillDataTable(@"SELECT NM_QTIOTP,
																		 FG_TPSALE,
																		 YN_AM,
																		 YN_SALE,
																		 YN_PICKING
																  FROM MM_EJTP WITH(NOLOCK)" + Environment.NewLine +
																 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
																 "AND CD_QTIOTP = '" + this.ctx반품형태.CodeValue + "'");

							this._매출형태 = dt.Rows[0]["FG_TPSALE"].ToString();
							유무환구분 = dt.Rows[0]["YN_AM"].ToString();
							this._배송여부 = dt.Rows[0]["YN_PICKING"].ToString();

							this.txt유무환구분.Text = 유무환구분;

                            //if (this.txt유무환구분.Text == "Y")
                            //{
                            //    if (txt유무환구분.Text == "Y" && D.GetString(cbo거래구분.SelectedValue) != "004" && D.GetString(cbo거래구분.SelectedValue) != "005")
                            //        this.cboVAT구분.Enabled = true;
                            //}
							break;
						case HelpID.P_MA_PARTNER_SUB:
							this.ctx매출처.CodeValue = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
							this.ctx매출처.CodeName = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
							this._header.CurrentRow["CD_PARTNER"] = e.HelpReturn.Rows[0]["CD_PARTNER"].ToString();
							this._header.CurrentRow["LN_PARTNER"] = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();

							DataRow row거래처부가 = BASIC.GetPartner(ctx매출처.CodeValue);

							if (_biz.Get영업그룹적용유무 == "Y")
							{
								ctx영업그룹.CodeValue = row거래처부가["CD_SALEGRP"].ToString();
								ctx영업그룹.CodeName = row거래처부가["NM_SALEGRP"].ToString();
								_header.CurrentRow["CD_SALEGRP"] = row거래처부가["CD_SALEGRP"].ToString();
								_header.CurrentRow["NM_SALEGRP"] = row거래처부가["NM_SALEGRP"].ToString();

								영업그룹변경시셋팅(ctx영업그룹.CodeValue);
							}

							if (_biz.Get담당자적용유무 == "Y")
							{
								dt = _biz.Get거래처영업담당자(D.GetString(ctx매출처.CodeValue));
								if (dt.Rows.Count == 1)
								{
									ctx담당자.CodeValue = D.GetString(dt.Rows[0]["CD_EMP_SALE"]);
									ctx담당자.CodeName = D.GetString(dt.Rows[0]["NM_EMP_SALE"]);
									_header.CurrentRow["NO_EMP"] = D.GetString(dt.Rows[0]["CD_EMP_SALE"]);
									_header.CurrentRow["NM_KOR"] = D.GetString(dt.Rows[0]["NM_EMP_SALE"]);
								}
							}
							break;
						case HelpID.P_MA_SALEGRP_SUB:
							_단가적용형태 = e.HelpReturn.Rows[0]["TP_SALEPRICE"].ToString();
							break;
						default:
							break;
					}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#endregion

		#region ♣ 기타 이벤트
		private void Page_DataChanged(object sender, EventArgs e)
		{
			try
			{
				if (IsChanged())
					ToolBarSaveButtonEnabled = true;

				if (!_flexH.HasNormalRow)
				{
					if (추가모드여부)
					{
						dtp의뢰일자.Focus();
                        //cbo공장.Enabled = true;
                        //cbo화폐단위.Enabled = true;
                        //if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                        //    cur환율.Enabled = false;
                        //else
                        //{
                        //    if (D.GetString(cbo화폐단위.SelectedValue) != "000")    //KRW
                        //        cur환율.Enabled = true;
                        //    else
                        //        cur환율.Enabled = false;
                        //}
                        //cboVAT구분.Enabled = true;
                        //curVAT율.Enabled = true;
					}
					btn삭제.Enabled = false;
					btn추가.Enabled = true;

				}
				else
				{
                    //cbo공장.Enabled = false;
                    //cbo화폐단위.Enabled = false;
                    //cur환율.Enabled = false;
                    //cboVAT구분.Enabled = false;
                    //curVAT율.Enabled = false;
                    //btn삭제.Enabled = true;

					if (!_헤더수정여부)
						btn추가.Enabled = false;
					else
						btn추가.Enabled = true;
				}
				
				if (!추가모드여부)
					ToolBarDeleteButtonEnabled = true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _header_ControlValueChanged(object sender, FreeBindingArgs e)
		{
			try
			{
				if (((Control)sender).Name == this.cbo거래구분.Name)
				{
					if (cbo거래구분.SelectedValue.ToString() == "" || cbo거래구분.SelectedValue.ToString() == "001") //수주유형이 국내
					{
						cboVAT구분.SelectedValue = "11";
                        //cboVAT구분.Enabled = true;
					}
					else if (cbo거래구분.SelectedValue.ToString() == "002" || cbo거래구분.SelectedValue.ToString() == "003") //수주유형이 LOCAL LC, 구매승인서
					{
						cboVAT구분.SelectedValue = "14";
                        //cboVAT구분.Enabled = false;
					}
					else if (cbo거래구분.SelectedValue.ToString() == "004" || cbo거래구분.SelectedValue.ToString() == "005") //수주유형이 MASTER L/C, MASTER 기타
					{
						cboVAT구분.SelectedValue = "15";
                        //cboVAT구분.Enabled = false;
					}

					this.VAT구분셋팅();
				}

				if (IsChanged()) ToolBarSaveButtonEnabled = true;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _header_JobModeChanged(object sender, FreeBindingArgs e)
		{
			try
			{
				if (e.JobMode == JobModeEnum.추가후수정)
				{
                    //m_pnlHead.Enabled = true;
                    //cbo거래구분.Enabled = true;
                    //if (_biz.Get영업그룹적용유무 != "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
                    //    ctx영업그룹.Enabled = true;
                    //ctx담당자.Enabled = true;
                    //cbo화폐단위.Enabled = true;

                    //if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                    //    cur환율.Enabled = false;
                    //else
                    //{
                    //    if (D.GetString(cbo화폐단위.SelectedValue) != "000")    //KRW
                    //        cur환율.Enabled = true;
                    //    else
                    //        cur환율.Enabled = false;
                    //}
                    //cbo공장.Enabled = true;
                    //cboVAT구분.Enabled = true;

                    //dtp의뢰일자.Enabled = true;
                    //ctx매출처.Enabled = true;
				}
				else
				{
                    //if (!_헤더수정여부)
                    //{
                    //    m_pnlHead.Enabled = false;
                    //    btn추가.Enabled = false;
                    //}
                    //else
                    //{
                    //    m_pnlHead.Enabled = true;
                    //    cbo거래구분.Enabled = false;
                    //    ctx영업그룹.Enabled = false;
                    //    cbo화폐단위.Enabled = false;
                    //    cur환율.Enabled = false;
                    //    cbo공장.Enabled = false;
                    //    cboVAT구분.Enabled = false;
                    //    btn추가.Enabled = true;

                    //    dtp의뢰일자.Enabled = false;
                    //    ctx매출처.Enabled = false;
                    //}
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Control_SelectionChangeCommitted(object sender, EventArgs e)
		{
			string name;

			try
			{
				name = ((Control)sender).Name;

				if (name == this.cbo화폐단위.Name)
					this.화폐단위셋팅(cbo화폐단위, cur환율);
				else if (name == this.cboVAT구분.Name)
					this.VAT구분셋팅();
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#region ♣ 기타메소드
		protected override bool IsChanged()
		{
			if (base.IsChanged())       // 그리드가 변경되었거나
				return true;

			return 헤더변경여부;        // 헤더가 변경되었거나
		}

		private void 화폐단위셋팅(DropDownComboBox cboCdExch, CurrencyTextBox curRtExch)
		{
			if (cboCdExch.SelectedValue == null || D.GetString(cboCdExch.SelectedValue) != "000")
			{
				decimal _환율 = 0;
				
				if (MA.기준환율.Option != MA.기준환율옵션.적용안함)
					_환율 = MA.기준환율적용(dtp의뢰일자.Text, D.GetString(cboCdExch.SelectedValue));

				curRtExch.DecimalValue = _환율;
				_header.CurrentRow["RT_EXCH"] = _환율;

                //if (MA.기준환율.Option == MA.기준환율옵션.적용_수정불가)
                //    curRtExch.Enabled = false;
                //else
                //    curRtExch.Enabled = true;
			}
			else if (D.GetString(cboCdExch.SelectedValue) == "000") //KRW
			{
				curRtExch.DecimalValue = 1;
				_header.CurrentRow["RT_EXCH"] = 1;
                //curRtExch.Enabled = false;
			}
		}

		private void VAT구분셋팅()
		{
			if (cboVAT구분.SelectedValue == null || cboVAT구분.SelectedValue.ToString() == "" || cboVAT구분.DataSource == null)
			{
				curVAT율.DecimalValue = 0;
				_header.CurrentRow["RT_VAT"] = 0;
				return;
			}

			DataTable dt = (DataTable)cboVAT구분.DataSource;
			DataRow row = dt.Rows.Find(cboVAT구분.SelectedValue);

			curVAT율.DecimalValue = _flexH.CDecimal(row["CD_FLAG1"]);
			_header.CurrentRow["RT_VAT"] = _flexH.CDecimal(row["CD_FLAG1"]);
		}

		private bool 예외추가여부
		{
			get
			{
				DataRow[] dr = _flexH.DataTable.Select("ISNULL(NO_SO, '') = '' AND ISNULL(NO_IO_MGMT, '') = ''", "", DataViewRowState.CurrentRows);
				if (dr != null && dr.Length > 0)
					return true;

				return false;
			}
		}

		private bool 수주반품적용여부
		{
			get
			{
				DataRow[] dr = _flexH.DataTable.Select("ISNULL(NO_SO, '') <> '' OR ISNULL(NO_IO_MGMT, '') <> ''", "", DataViewRowState.CurrentRows);
				if (dr != null && dr.Length > 0)
					return true;
				return false;
			}
		}

		private bool 출고적용여부
		{
			get
			{
				DataRow[] dr = _flexH.DataTable.Select("ISNULL(NO_SO_MGMT, '') <> ''", "", DataViewRowState.CurrentRows);
				if (dr != null && dr.Length > 0)
					return true;
				return false;
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

		#region ♣ 속성들
		bool 적용시컨트롤Enabled
		{
			set
			{
                //ctx매출처.Enabled = value;
                //cbo공장.Enabled = value;
                //cbo거래구분.Enabled = value;

                //if (value == true && _biz.Get영업그룹적용유무 != "Y")  //거래처부가정보의 거래처와영업그룹매핑을 할 경우
                //    ctx영업그룹.Enabled = true;
                //else
                //    ctx영업그룹.Enabled = false;

                //ctx담당자.Enabled = value;
                //cboVAT구분.Enabled = value;
                //cbo화폐단위.Enabled = value;
                //ctx반품형태.Enabled = value;
			}
		}

		void 영업그룹변경시셋팅(string 영업그룹)
		{
			try
			{
				DataRow row영업그룹 = BASIC.GetSaleGrp(영업그룹);
				_단가적용형태 = D.GetString(row영업그룹["TP_SALEPRICE"]);
			}
			catch(Exception ex)
			{
				_단가적용형태 = "";
				MsgEnd(ex);
			}
		}

		bool 필수값체크
		{
			get
			{
				if (dtp의뢰일자.Text == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lbl의뢰일자.Text);
					dtp의뢰일자.Focus();
					return false;
				}

				if (cbo거래구분.SelectedValue == null || cbo거래구분.SelectedValue.ToString() == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lbl거래구분.Text);
					cbo거래구분.Focus();
					return false;
				}

				if (ctx영업그룹.CodeValue == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lbl영업그룹.Text);
					ctx영업그룹.Focus();
					return false;
				}

				if (ctx담당자.CodeValue == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lbl담당자.Text);
					ctx담당자.Focus();
					return false;
				}

				if (cbo화폐단위.SelectedValue == null || cbo화폐단위.SelectedValue.ToString() == "")
				{
					ShowMessage(공통메세지._은는필수입력항목입니다, lbl화폐단위.Text);
					cbo화폐단위.Focus();
					return false;
				}

				return true;
			}
		}

		bool 추가모드여부
		{
			get
			{
				if (_header.JobMode == JobModeEnum.추가후수정)
					return true;

				return false;
			}
		}

		private bool 헤더변경여부
		{
			get
			{
				bool bChange = false;

				bChange = _header.GetChanges() != null ? true : false;

				// 헤더가 변경됬지만 추가모드이고 디테일 그리드가 아무 내용이 없으면 변경안된걸로 본다.
				if (bChange && _header.JobMode == JobModeEnum.추가후수정 && !_flexH.HasNormalRow)
					bChange = false;

				return bChange;
			}

		}

		private decimal 그리드항번최대값
		{
			get
			{
				decimal dMaxValue = _flexH.GetMaxValue("SEQ_GIR");
				return dMaxValue;
			}
		}

        bool Chk수주번호 { get { return !Checker.IsEmpty(txt수주번호, lbl수주번호.Text); } }
		bool Chk의뢰일자 { get { return Checker.IsValid(dtp의뢰일자, true, lbl의뢰일자.Text); } }
		bool Chk공장 { get { return !Checker.IsEmpty(cbo공장, lbl공장.Text); } }
		bool Chk거래구분 { get { return !Checker.IsEmpty(cbo거래구분, lbl거래구분.Text); }}
		bool Chk거래처 { get { return !Checker.IsEmpty(ctx매출처, lbl매출처.Text); } }
		bool Chk영업그룹 { get { return !Checker.IsEmpty(ctx영업그룹, lbl영업그룹.Text); } }
		bool Chk담당자 { get { return !Checker.IsEmpty(ctx담당자, lbl담당자.Text); }}
		bool Chk과세구분 { get { return !Checker.IsEmpty(cboVAT구분, lblVAT구분.Text); } }
		bool Chk환종 { get { return !Checker.IsEmpty(cbo화폐단위, lbl화폐단위.Text); } }
		bool Chk반품형태 { get { return !Checker.IsEmpty(ctx반품형태, lbl반품형태.Text); } }

		internal string Get거래구분 { get { return D.GetString(cbo거래구분.SelectedValue); } }
		internal string Get영업그룹 { get { return ctx영업그룹.CodeValue; } }
		internal string Get담당자 { get { return ctx담당자.CodeValue; } }
		internal string Get반품형태 { get { return ctx반품형태.CodeValue; } }
		internal string Get과세구분 { get { return D.GetString(cboVAT구분.SelectedValue); } }
		internal decimal Get부가세율 { get { return curVAT율.DecimalValue; }}
		internal string Get유무환구분 { get { return txt유무환구분.Text; } }
		internal string Get환종 { get { return D.GetString(cbo화폐단위.SelectedValue); } }
		internal decimal Get환율 { get { return cur환율.DecimalValue; } }
		internal string Get계산서처리 { get { return this._계산서처리코드; } }
		internal string Get단가유형 { get { return this._단가유형코드; } }
		internal string Get납품처 { get { return this.ctx매출처.CodeValue; } }
		internal string Get비고 { get { return txt비고.Text; } }
		#endregion
	}
}
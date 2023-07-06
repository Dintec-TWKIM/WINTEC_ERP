using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.ERPU;
using Duzon.Windows.Print;
using Duzon.ERPU.MF.Common;
using Duzon.ERPU.Grant;

namespace cz
{
	public partial class P_CZ_SA_SOSCH2 : PageBase
	{
		#region 초기화 & 전역변수
		private P_CZ_SA_SOSCH2_BIZ _biz = new P_CZ_SA_SOSCH2_BIZ();
		private string 임시폴더;

		private bool Chk수주일자 { get { return Checker.IsValid(this.dtp수주일자, true, this.lbl수주일자.Text); } }

		private string 발주상태
		{
			get
			{
				if (this.rdo발주.Checked == true) return "003";
				else if (this.rdo미발주.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo발주.Checked = true;
				else if (value == "002") this.rdo미발주.Checked = true;
				else this.rdo발주전체.Checked = true;
			}
		}

		private string 입고상태
		{
			get
			{
				if (this.rdo입고.Checked == true) return "003";
				else if (this.rdo미입고.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo입고.Checked = true;
				else if (value == "002") this.rdo미입고.Checked = true;
				else this.rdo입고전체.Checked = true;
			}
		}

		private string 의뢰상태
		{
			get
			{
				if (this.rdo의뢰.Checked == true) return "003";
				else if (this.rdo미의뢰.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo의뢰.Checked = true;
				else if (value == "002") this.rdo미의뢰.Checked = true;
				else this.rdo의뢰전체.Checked = true;
			}
		}

		private string 출고상태
		{
			get
			{
				if (this.rdo출고.Checked == true) return "003";
				else if (this.rdo미출고.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo출고.Checked = true;
				else if (value == "002") this.rdo미출고.Checked = true;
				else this.rdo출고전체.Checked = true;
			}
		}

		private string 매출상태
		{
			get
			{
				if (this.rdo매출.Checked == true) return "003";
				else if (this.rdo미매출.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "003") this.rdo매출.Checked = true;
				else if (value == "002") this.rdo미매출.Checked = true;
				else this.rdo매출전체.Checked = true;
			}
		}

		private string 마감상태
		{
			get
			{
				if (this.rdo마감.Checked == true) return "004";
				else if (this.rdo결재.Checked == true) return "003";
				else if (this.rdo미마감.Checked == true) return "002";
				else return "001";
			}
			set
			{
				if (value == "004") this.rdo마감.Checked = true;
				if (value == "003") this.rdo결재.Checked = true;
				else if (value == "002") this.rdo미마감.Checked = true;
				else this.rdo마감전체.Checked = true;
			}
		}

		public P_CZ_SA_SOSCH2()
		{
			StartUp.Certify(this);
			InitializeComponent();

			this.MainGrids = new FlexGrid[] { this._flex수주번호H, this._flex수주번호L,
											  this._flex발주번호H, this._flex발주번호M, this._flex발주번호L,
											  this._flex입고일H, this._flex입고일M, this._flex입고일L,
											  this._flex납기일H, this._flex납기일M, this._flex납기일L,
                                              this._flex매입처H, this._flex매입처M, this._flex매입처L,
											  this._flex매출처H, this._flex매출처M, this._flex매출처L,
											  this._flex품목H, this._flex품목M, this._flex품목L,
											  this._flex수주유형H, this._flex수주유형M, this._flex수주유형L,
											  this._flex영업그룹H, this._flex영업그룹M, this._flex영업그룹L };
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.임시폴더 = "temp";

			this.InitEvent();
		}

		private void SetHeaderGrid(FlexGrid flex)
		{
			flex.BeginSetting(1, 1, false);

			if (flex.Name == this._flex수주번호H.Name)
			{
				flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
				flex.SetCol("NM_LOG", "물류담당", 80);
				flex.SetCol("NM_PACKING", "포장형태", 80);
				flex.SetCol("DTS_ETRYPT", "최근국내입항일자", 80);
				flex.SetCol("DT_OUT_DIFF", "지연일수", 80);
				flex.SetCol("QT_READY_INFO", "Ready발송횟수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				flex.SetCol("DT_READY_INFO", "최초Ready일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("TXT_USERDEF3", "선적정보등록상태", 100);
				flex.SetCol("VOLUME_WEIGHT", "VolumeWeight", 100);
				flex.SetCol("DT_DLV", "납품예정일", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("CD_DLV_MAIN", "송품방법(M)", 100);
				flex.SetCol("CD_DLV_SUB", "송품방법(S)", 100);
				flex.SetCol("YN_51_FILE", "납품지시서", 100, false, CheckTypeEnum.Y_N);
				flex.SetCol("DC_DLV", "상세요청", 100);
				flex.SetCol("DT_CONTRACT", "수통일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("QT_ITEM", "종수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
				flex.SetCol("QT_IN_REMAIN", "미입고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);

				flex.SetDummyColumn("S", "DT_DLV");
				flex.SetDataMap("CD_DLV_MAIN", DBHelper.GetDataTable(@"SELECT '' AS CODE, '' AS NAME
UNION ALL
SELECT CD_SYSDEF AS CODE,
	   NM_SYSDEF AS NAME
FROM CZ_MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_SA00060'
AND (CD_SYSDEF LIKE 'WM%' OR CD_SYSDEF = 'PM001')
ORDER BY NAME ASC"), "CODE", "NAME");
				flex.SetDataMap("CD_DLV_SUB", DBHelper.GetDataTable(@"SELECT '' AS CODE, '' AS NAME
UNION ALL
SELECT CD_SYSDEF AS CODE,
	   NM_SYSDEF AS NAME
FROM MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_SA00007'
UNION ALL
SELECT CD_SYSDEF AS CODE,
	   NM_SYSDEF AS NAME
FROM CZ_MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_SA00060'
AND (CD_SYSDEF LIKE 'WS%' OR CD_SYSDEF = 'PM001')
ORDER BY CODE ASC"), "CODE", "NAME");
			}
			
			flex.SetCol("NO_SO", "수주번호", 100);
			flex.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
			flex.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("LN_PARTNER", "매출처명", 120);
			flex.SetCol("NM_PARTNER_GRP", "거래처그룹", 80);
			flex.SetCol("NM_NATION", "국가", 60);
			flex.SetCol("NO_HULL", "호선번호", 120);
			flex.SetCol("NM_VESSEL", "호선명", 120);
			flex.SetCol("WEIGHT", "무게", 40, false, typeof(decimal), FormatTpType.QUANTITY);

			flex.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_REQ", "입고의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IN", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_GIR", "출고의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_OUT", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IV", "매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            flex.SetCol("QT_READY", "준비수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_GI_REMAIN", "미납수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IV_REMAIN", "미매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            flex.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            flex.SetCol("QT_GIR_PACK", "포장의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_PACK", "포장수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            flex.SetCol("QT_REQ_RETURN", "입고반품의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IN_RETURN", "입고반품수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_GIR_RETURN", "출고반품의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("QT_OUT_RETURN", "출고반품수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IV_RETURN", "출고반품매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

			flex.SetCol("NM_EXCH", "통화명", 80);
			flex.SetCol("RT_EXCH", "환율", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("AM_EX_SO", "외화금액", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("AM_SO", "원화금액", 100, false, typeof(decimal), FormatTpType.MONEY);
			flex.SetCol("NM_KOR", "영업담당", 80);
			flex.SetCol("YN_PROFORMA", "선수금여부", 40, false, CheckTypeEnum.Y_N);
			flex.SetCol("AM_RCP_A", "선수금", 100, false, typeof(decimal), FormatTpType.MONEY);
			flex.SetCol("DT_RCP", "수금일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY); //선수금 수금 일자
			flex.SetCol("DT_PO", "발주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DT_LIMIT", "납기일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DT_EXPECT_IN", "예상입고일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DT_IN", "입고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DT_IV", "매출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("NM_CC", "C/C명", 80);
			flex.SetCol("NO_PROJECT", "프로젝트코드", false);
			flex.SetCol("NM_PROJECT", "프로젝트명", false);
			flex.SetCol("NM_SO", "수주유형", false);
			flex.SetCol("NM_TP_TRANSPORT", "선적조건", 80);
			flex.SetCol("NM_SUBJECT", "주제", false);
			flex.SetCol("DC_RMK_TEXT", "수주비고", 100);
			flex.SetCol("YN_PACK", "포장여부", 40, false, CheckTypeEnum.Y_N);
			flex.SetCol("YN_AUTO_SUBMIT", "자동제출", 40, false, CheckTypeEnum.Y_N);
			flex.SetCol("YN_CLOSE", "마감여부", 40, false, CheckTypeEnum.Y_N);
			flex.SetCol("ST_STAT", "결재상태", 80);
			flex.SetCol("DC_RMK_TEXT2", "물류비고", 100, true);
			flex.SetCol("DC_RMK1", "커미션", 100, true);
			flex.SetCol("TXT_USERDEF1", "송품비고", 100, true);
			flex.SetCol("NM_ITEMGRP", "품목군", 100);
			flex.SetCol("NM_EWS_LEVEL", "EWS상태", 100);
			//flex.SetCol("NM_SUB_CATEGORY", "의뢰구분(소)", 100);

			flex.SetDataMap("ST_STAT", Global.MainFrame.GetComboDataCombine("S;PU_C000065"), "CODE", "NAME");

			flex.UseMultySorting = true;
			flex.ExtendLastCol = true;

			//flex.SettingVersion = "1.0.0.1";
			//flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);

			flex.SetDefault("1.0.0.1", SumPositionEnum.Top);

			flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			flex.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);
			flex.MouseDoubleClick += new MouseEventHandler(this.flexH_MouseDoubleClick);
		}

		private void SetLineGrid(FlexGrid flex)
		{
			flex.BeginSetting(1, 1, false);

			if (flex.Name == this._flex수주번호L.Name)
			{
				flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
				flex.SetDummyColumn("S");
			}
			
			if (flex.Name == this._flex리스트H.Name)
            {
                flex.SetCol("NO_SO", "수주번호", 80);
                flex.SetCol("DT_SO", "수주일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
                flex.SetCol("NM_KOR", "영업담당", 80);
                flex.SetCol("LN_PARTNER", "매출처", 120);
                flex.SetCol("NM_VESSEL", "호선명", 120);
            }
			else
				flex.SetCol("NO_SO", "수주번호", false);

			flex.SetCol("NO_DSP", "순번", 40);
			flex.SetCol("CD_ITEM", "품목코드", 100);
			flex.SetCol("NM_ITEM", "품목명", 120);
			flex.SetCol("NM_SUBJECT", "주제", 100);
			flex.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
			flex.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
			flex.SetCol("STND_ITEM", "규격", false);
			flex.SetCol("UNIT_SO", "단위", 60);
			flex.SetCol("LN_SUPPLIER", "매입처", 120);
			flex.SetCol("DT_LIMIT", "납기일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DT_REQGI", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

            if (flex.Name == this._flex리스트H.Name)
                flex.SetCol("DT_OUT", "출고일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			flex.SetCol("QT_SO", "수주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_REQ", "입고의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IN", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_GIR", "출고의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_OUT", "출고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IV", "매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            flex.SetCol("QT_READY", "준비수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_OUT_REMAIN", "미납수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IV_REMAIN", "미매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            flex.SetCol("QT_STOCK", "재고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("QT_BOOK", "출고예약수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            flex.SetCol("QT_GIR_PACK", "포장의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_PACK", "포장수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);

            flex.SetCol("QT_REQ_RETURN", "입고반품의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IN_RETURN", "입고반품수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_GIR_RETURN", "출고반품의뢰수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_OUT_RETURN", "출고반품수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            flex.SetCol("QT_IV_RETURN", "출고반품매출수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
            
            flex.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			flex.SetCol("UM", "원화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_UNIT_COST);
			flex.SetCol("AM_EX", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("AM", "원화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("WEIGHT", "무게", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("NM_STA_SO", "수주상태", 60);
			flex.SetCol("LN_GI_PARTNER", "납품처명", 120);
            //flex.SetCol("QT_INV", "현재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("NM_SL", "창고", 100);
			flex.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
			flex.SetCol("NO_POLINE_PARTNER", "매출처발주항번", 100);
			flex.SetCol("CD_LOCATION", "Location", 100);

			if (flex.Name == this._flex수주번호L.Name)
			{
				flex.SetCol("NO_ORDER", "공사번호", 100, false);
				flex.SetCol("DT_EXPECT", "입고약속일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("DT_EXDATE", "지시일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("DC_RMK_LOG", "재고비고", 100, true);
				
				flex.AfterEdit += new RowColEventHandler(this.flexL_AfterEdit);
			}

			//flex.SettingVersion = "1.0.0.1";
			//flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flex.SetDefault("1.0.0.1", SumPositionEnum.Top);

			flex.MouseDoubleClick += new MouseEventHandler(this.flexL_MouseDoubleClick);
		}

		private void InitEvent()
		{
			this.btn부분송품적용.Click += new EventHandler(this.btn부분송품적용_Click);
			this.btn부분송품해제.Click += new EventHandler(this.btn부분송품해제_Click);
			this.btn청구호선적용.Click += new EventHandler(this.btn청구호선적용_Click);
			this.btn청구호선해제.Click += new EventHandler(this.btn청구호선해제_Click);
			this.btnSDOC출력.Click += BtnSDOC출력_Click;
			this.btn선적정보등록.Click += Btn선적정보등록_Click;

			this.bpc제품군.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.bpc수주유형.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.bpc호선.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.bpc매출처그룹.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

			this._flex수주번호H.CheckHeaderClick += new EventHandler(this._flex수주번호_CheckHeaderClick);
			this._flex발주번호H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex입고일H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex납기일H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flex매입처H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
            this._flex매출처H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex품목H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex수주유형H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex영업그룹H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.oneGrid1.UseCustomLayout = true;
			this.bpPanelControl1.IsNecessaryCondition = true;
			this.bpPanelControl2.IsNecessaryCondition = true;
			this.oneGrid1.InitCustomLayout();

			this.ctx회사.CodeValue = this.LoginInfo.CompanyCode;
			this.ctx회사.CodeName = this.LoginInfo.CompanyName;

			this.dtp수주일자.StartDateToString = MainFrameInterface.GetDateTimeToday().AddMonths(-1).ToString("yyyyMMdd");
			this.dtp수주일자.EndDateToString = MainFrameInterface.GetStringToday;

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

            DataSet g_dsCombo = GetComboData("S;PU_C000016", "S;SA_B000016");

			this.cbo거래구분.DataSource = g_dsCombo.Tables[0];
			this.cbo거래구분.DisplayMember = "NAME";
			this.cbo거래구분.ValueMember = "CODE";

			this.cbo수주상태.DataSource = g_dsCombo.Tables[1];
			this.cbo수주상태.DisplayMember = "NAME";
			this.cbo수주상태.ValueMember = "CODE";

			if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
				this.btn선적정보등록.Visible = true;
			else
				this.btn선적정보등록.Visible = false;

			this.발주상태 = Settings1.Default.발주상태;
			this.입고상태 = Settings1.Default.입고상태;
			this.의뢰상태 = Settings1.Default.의뢰상태;
			this.출고상태 = Settings1.Default.출고상태;
			this.매출상태 = Settings1.Default.매출상태;
			this.마감상태 = Settings1.Default.마감상태;
            this.chk자동제출.Checked = Settings1.Default.자동제출;
            this.chk비용제외.Checked = Settings1.Default.비용제외;
            this.chk무상공급제외.Checked = Settings1.Default.무상공급제외;

			UGrant ugrant = new UGrant();
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "PARTIAL", this.btn부분송품적용);
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "PARTIAL", this.btn부분송품해제);
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "BILL_IMO", this.btn청구호선적용);
			ugrant.GrantButtonVisible(Global.MainFrame.CurrentPageID, "BILL_IMO", this.btn청구호선해제);
		}
		#endregion

		#region 메인버튼 이벤트
		protected override bool BeforeSearch()
		{
			if (!base.BeforeSearch()) return false;

			if (!Chk수주일자) return false;
			
			if (string.IsNullOrEmpty(this.ctx회사.CodeValue))
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
				return false;
			}

			return true;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				DataTable dt = null;

				int 기간 = Math.Abs((this.dtp수주일자.StartDate - this.dtp수주일자.EndDate).Days);

				string startDate, endDate;
				DataTable tmpDataTable;

				if (string.IsNullOrEmpty(this.txt수주번호.Text) && (수주진행현황탭)this.tabControl.SelectedIndex == 수주진행현황탭.수주번호)
				{
					for (int i = 0; i <= 기간 / 365; i++)
					{
						startDate = this.dtp수주일자.StartDate.AddDays(i * 365).ToString("yyyyMMdd");

						if (기간 >= (i + 1) * 365)
							endDate = this.dtp수주일자.StartDate.AddDays(((i + 1) * 365) - 1).ToString("yyyyMMdd");
						else
							endDate = this.dtp수주일자.EndDateToString;

						MsgControl.ShowMsg("조회 중 입니다. 잠시만 기다려 주세요.\n조회기간 (@ ~ @)", new string[] { Util.GetTo_DateStringS(startDate), Util.GetTo_DateStringS(endDate) });

						tmpDataTable = this._biz.SearchHeader((수주진행현황탭)this.tabControl.SelectedIndex, new object[]{ this.ctx회사.CodeValue,
																														   D.GetString(this.cbo수주번호.SelectedValue),
																														   this.txt수주번호.Text.Trim(),
																														   startDate,
																														   endDate,
																														   this.bpc영업그룹.QueryWhereIn_Pipe,
																														   D.GetString(this.cbo거래구분.SelectedValue),
																														   D.GetString(this.cbo수주상태.SelectedValue),
																														   this.bpc수주유형.QueryWhereIn_Pipe,
																														   this.bpc매출처.QueryWhereIn_Pipe,
																														   this.bpc매출처그룹.QueryWhereIn_Pipe,
																														   this.bpc영업담당자.QueryWhereIn_Pipe,
																														   this.bpc제품군.QueryWhereIn_Pipe,
																														   this.txt매출처발주번호.Text,
																														   this.bpc매입처.QueryWhereIn_Pipe,
																														   this.bpc호선.QueryWhereIn_Pipe,
																														   this.발주상태,
																														   this.입고상태,
																														   this.의뢰상태,
																														   this.출고상태,
																														   this.매출상태,
																														   this.마감상태,
																														   this.ctx영업물류담당자.CodeValue,
																														   (this.chk자동제출.Checked == true ? "Y" : "N"),
																														   (this.chk비용제외.Checked == true ? "Y" : "N"),
																														   (this.chk무상공급제외.Checked == true ? "Y" : "N"),
																														   (this.chkReady발송업체제외.Checked == true ? "Y" : "N"),
																														   (this.chk포워더전달제외.Checked == true ? "Y" : "N"),
																														   (this.chk납기단축.Checked == true ? "Y" : "N") });

						if (i == 0)
							dt = tmpDataTable;
						else
							dt.Merge(tmpDataTable);
					}
				}
				else
				{
					dt = this._biz.SearchHeader((수주진행현황탭)this.tabControl.SelectedIndex, new object[]{ this.ctx회사.CodeValue,
																											 D.GetString(this.cbo수주번호.SelectedValue),
																											 this.txt수주번호.Text.Trim(),
																											 this.dtp수주일자.StartDateToString,
																											 this.dtp수주일자.EndDateToString,
																											 this.bpc영업그룹.QueryWhereIn_Pipe,
																											 D.GetString(this.cbo거래구분.SelectedValue),
																											 D.GetString(this.cbo수주상태.SelectedValue),
																											 this.bpc수주유형.QueryWhereIn_Pipe,
																											 this.bpc매출처.QueryWhereIn_Pipe,
																											 this.bpc매출처그룹.QueryWhereIn_Pipe,
																											 this.bpc영업담당자.QueryWhereIn_Pipe,
																											 this.bpc제품군.QueryWhereIn_Pipe,
																											 this.txt매출처발주번호.Text,
																											 this.bpc매입처.QueryWhereIn_Pipe,
																											 this.bpc호선.QueryWhereIn_Pipe,
																											 this.발주상태,
																											 this.입고상태,
																											 this.의뢰상태,
																											 this.출고상태,
																											 this.매출상태,
																											 this.마감상태,
																											 this.ctx영업물류담당자.CodeValue,
																											 (this.chk자동제출.Checked == true ? "Y" : "N"),
																											 (this.chk비용제외.Checked == true ? "Y" : "N"),
																											 (this.chk무상공급제외.Checked == true ? "Y" : "N"),
																											 (this.chkReady발송업체제외.Checked == true ? "Y" : "N"),
																										     (this.chk포워더전달제외.Checked == true ? "Y" : "N"),
																											 (this.chk납기단축.Checked == true ? "Y" : "N") });
				}

				if (this.ctx회사.CodeValue != Global.MainFrame.LoginInfo.CompanyCode)
				{
					if (dt.Columns.Contains("NM_EXCH"))
                    {
						dt.Columns.Remove("NM_EXCH");
						dt.Columns.Add("NM_EXCH");
					}

					if (dt.Columns.Contains("RT_EXCH"))
					{
						dt.Columns.Remove("RT_EXCH");
						dt.Columns.Add("RT_EXCH");
					}

					if (dt.Columns.Contains("AM_EX_SO"))
					{
						dt.Columns.Remove("AM_EX_SO");
						dt.Columns.Add("AM_EX_SO");
					}

					if (dt.Columns.Contains("AM_SO"))
					{
						dt.Columns.Remove("AM_SO");
						dt.Columns.Add("AM_SO");
					}

					if (dt.Columns.Contains("UM_EX"))
					{
						dt.Columns.Remove("UM_EX");
						dt.Columns.Add("UM_EX");
					}

					if (dt.Columns.Contains("UM"))
					{
						dt.Columns.Remove("UM");
						dt.Columns.Add("UM");
					}

					if (dt.Columns.Contains("AM_EX"))
					{
						dt.Columns.Remove("AM_EX");
						dt.Columns.Add("AM_EX");
					}

					if (dt.Columns.Contains("AM"))
					{
						dt.Columns.Remove("AM");
						dt.Columns.Add("AM");
					}
				}

				switch ((수주진행현황탭)this.tabControl.SelectedIndex)
				{
                    case 수주진행현황탭.수주번호:
						#region 수주번호
						if (this._flex수주번호H.DataTable == null)
						{
							this._flex수주번호H.DetailGrids = new FlexGrid[] { this._flex수주번호L };

							#region Header
							this.SetHeaderGrid(this._flex수주번호H);

							//CheckPen기능 관련 설정
							this._flex수주번호H.CheckPenInfo.EnabledCheckPen = true;

							// 메모기능 
							this._flex수주번호H.CellNoteInfo.EnabledCellNote = true;// 메모기능활성화 
							this._flex수주번호H.CellNoteInfo.CategoryID = "P_CZ_SA_SOSCH2"; // page 명입력 // 같은page명을입력했을경우여러화면에서볼수있습니다. 
							this._flex수주번호H.CellNoteInfo.DisplayColumnForDefaultNote = "NO_SO";  // 마킹& 메모가보여질컬럼설정 
							this._flex수주번호H.CellContentChanged += new CellContentEventHandler(this._flex수주번호H_CellContentChanged);
							#endregion
							
							this.SetLineGrid(this._flex수주번호L);    
						}
						
						this._flex수주번호H.Binding = dt;
						#endregion
						break;
                    case 수주진행현황탭.발주번호:
						#region 발주번호
						if (this._flex발주번호H.DataTable == null)
						{
							this._flex발주번호H.DetailGrids = new FlexGrid[] { this._flex발주번호M, this._flex발주번호L };
							this._flex발주번호M.DetailGrids = new FlexGrid[] { this._flex발주번호L };

							#region Header
							this._flex발주번호H.BeginSetting(1, 1, false);

							this._flex발주번호H.SetCol("NO_PO", "발주번호", 100);
							this._flex발주번호H.SetCol("CD_SUPPLIER", "매입처코드", 100);
							this._flex발주번호H.SetCol("LN_SUPPLIER", "매입처명", 120);
							this._flex발주번호H.SetCol("CNT_SO", "수주건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex발주번호H.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex발주번호H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.MONEY);

							this._flex발주번호H.SettingVersion = "1.0.0.0";
							this._flex발주번호H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
							#endregion

							this.SetHeaderGrid(this._flex발주번호M);
							this.SetLineGrid(this._flex발주번호L);
						}
						
						this._flex발주번호H.Binding = dt;
						#endregion
						break;
                    case 수주진행현황탭.입고일:
						#region 입고일
						if (this._flex입고일H.DataTable == null)
						{
							this._flex입고일H.DetailGrids = new FlexGrid[] { this._flex입고일M, this._flex입고일L };
							this._flex입고일M.DetailGrids = new FlexGrid[] { this._flex입고일L };

							#region Header
							this._flex입고일H.BeginSetting(1, 1, false);

							this._flex입고일H.SetCol("DT_IN", "입고일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
							this._flex입고일H.SetCol("CNT_SO", "수주건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex입고일H.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex입고일H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.MONEY);

							this._flex입고일H.SettingVersion = "1.0.0.0";
							this._flex입고일H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
							#endregion

							this.SetHeaderGrid(this._flex입고일M);
							this.SetLineGrid(this._flex입고일L);
						}
						
						this._flex입고일H.Binding = dt;
						#endregion
						break;
                    case 수주진행현황탭.납기일:
						#region 납기일
						if (this._flex납기일H.DataTable == null)
						{
							this._flex납기일H.DetailGrids = new FlexGrid[] { this._flex납기일M, this._flex납기일L };
							this._flex납기일M.DetailGrids = new FlexGrid[] { this._flex납기일L };

							#region Header
							this._flex납기일H.BeginSetting(1, 1, false);

							this._flex납기일H.SetCol("DT_DUEDATE", "납기요구일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
							this._flex납기일H.SetCol("CNT_SO", "수주건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex납기일H.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex납기일H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.MONEY);

							this._flex납기일H.SettingVersion = "1.0.0.0";
							this._flex납기일H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
							#endregion

							this.SetHeaderGrid(this._flex납기일M);
							this.SetLineGrid(this._flex납기일L);
						}

						this._flex납기일H.Binding = dt;
						#endregion
						break;
                    case 수주진행현황탭.매입처:
                        #region 매입처
                        if (this._flex매입처H.DataTable == null)
                        {
                            this._flex매입처H.DetailGrids = new FlexGrid[] { this._flex매입처M, this._flex매입처L };
                            this._flex매입처M.DetailGrids = new FlexGrid[] { this._flex매입처L };

                            #region Header
                            this._flex매입처H.BeginSetting(1, 1, false);

                            this._flex매입처H.SetCol("CD_SUPPLIER", "매입처코드", false);
                            this._flex매입처H.SetCol("LN_SUPPLIER", "매입처명", 120);
                            this._flex매입처H.SetCol("CNT_SO", "수주건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                            this._flex매입처H.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
                            this._flex매입처H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.MONEY);
							this._flex매입처H.SetCol("DT_OUT_DIFF", "지연일수", 100, false, typeof(decimal), FormatTpType.RATE);

							this._flex매입처H.SettingVersion = "1.0.0.0";
                            this._flex매입처H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
                            #endregion

                            this.SetHeaderGrid(this._flex매입처M);
                            this.SetLineGrid(this._flex매입처L);
                        }

                        this._flex매입처H.Binding = dt;
                        #endregion
                        break;
                    case 수주진행현황탭.매출처:
						#region 매출처
						if (this._flex매출처H.DataTable == null)
						{
							this._flex매출처H.DetailGrids = new FlexGrid[] { this._flex매출처M, this._flex매출처L };
							this._flex매출처M.DetailGrids = new FlexGrid[] { this._flex매출처L };

							#region Header
							this._flex매출처H.BeginSetting(1, 1, false);

							this._flex매출처H.SetCol("CD_PARTNER", "매출처코드", false);
							this._flex매출처H.SetCol("LN_PARTNER", "매출처명", 120);
							this._flex매출처H.SetCol("CNT_SO", "수주건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex매출처H.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex매출처H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.MONEY);

							this._flex매출처H.SettingVersion = "1.0.0.0";
							this._flex매출처H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
							#endregion

							this.SetHeaderGrid(this._flex매출처M);
							this.SetLineGrid(this._flex매출처L);
						}
						
						this._flex매출처H.Binding = dt;
						#endregion
						break;
                    case 수주진행현황탭.품목:
						#region 품목
						if (this._flex품목H.DataTable == null)
						{
							this._flex품목H.DetailGrids = new FlexGrid[] { this._flex품목M, this._flex품목L };
							this._flex품목M.DetailGrids = new FlexGrid[] { this._flex품목L };

							#region Header
							this._flex품목H.BeginSetting(1, 1, false);

							this._flex품목H.SetCol("CD_ITEM", "품목코드", 100);
							this._flex품목H.SetCol("NM_ITEM", "품목명", 120);
							this._flex품목H.SetCol("STND_ITEM", "규격", false);
							this._flex품목H.SetCol("CNT_SO", "수주건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex품목H.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex품목H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.MONEY);

							this._flex품목H.SettingVersion = "1.0.0.0";
							this._flex품목H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
							#endregion

							this.SetHeaderGrid(this._flex품목M);
							this.SetLineGrid(this._flex품목L);                        
						}

						this._flex품목H.Binding = dt;
						#endregion
						break;
                    case 수주진행현황탭.수주유형:
						#region 수주유형
						if (this._flex수주유형H.DataTable == null)
						{
							this._flex수주유형H.DetailGrids = new FlexGrid[] { this._flex수주유형M, this._flex수주유형L };
							this._flex수주유형M.DetailGrids = new FlexGrid[] { this._flex수주유형L };

							#region Header
							this._flex수주유형H.BeginSetting(1, 1, false);

							this._flex수주유형H.SetCol("TP_SO", "수주유형코드", false);
							this._flex수주유형H.SetCol("NM_SO", "수주유형명", 150);
							this._flex수주유형H.SetCol("CNT_SO", "수주건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex수주유형H.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex수주유형H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.MONEY);

							this._flex수주유형H.SettingVersion = "1.0.0.0";
							this._flex수주유형H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
							#endregion

							this.SetHeaderGrid(this._flex수주유형M);
							this.SetLineGrid(this._flex수주유형L); 
						}

						this._flex수주유형H.Binding = dt;
						#endregion
						break;
                    case 수주진행현황탭.영업그룹:
						#region 영업그룹
						if (this._flex영업그룹H.DataTable == null)
						{
							this._flex영업그룹H.DetailGrids = new FlexGrid[] { this._flex영업그룹M, this._flex영업그룹L };
							this._flex영업그룹M.DetailGrids = new FlexGrid[] { this._flex영업그룹L };

							#region Header
							this._flex영업그룹H.BeginSetting(1, 1, false);

							this._flex영업그룹H.SetCol("CD_SALEGRP", "영업그룹코드", false);
							this._flex영업그룹H.SetCol("NM_SALEGRP", "영업그룹명", 120);
							this._flex영업그룹H.SetCol("CNT_SO", "수주건수", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex영업그룹H.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
							this._flex영업그룹H.SetCol("AM_SO", "수주금액", 100, false, typeof(decimal), FormatTpType.MONEY);

							this._flex영업그룹H.SettingVersion = "1.0.0.0";
							this._flex영업그룹H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
							#endregion

							this.SetHeaderGrid(this._flex영업그룹M);
							this.SetLineGrid(this._flex영업그룹L);
						}

						this._flex영업그룹H.Binding = dt;
						#endregion
						break;
                    case 수주진행현황탭.리스트:
						#region 리스트
						if (this._flex리스트H.DataTable == null)
						{
							this.SetLineGrid(this._flex리스트H);
						}

						this._flex리스트H.Binding = dt;
						#endregion
						break;
				}

				if (dt == null || dt.Rows.Count == 0)
				{
					ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
					return;
				}

				this.ToolBarAddButtonEnabled = this.ToolBarDeleteButtonEnabled = false;
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
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override bool SaveData()
		{
			FlexGrid flex;

			try
			{
				if (!base.SaveData() || !base.Verify()) return false;

				flex = null;

				switch ((수주진행현황탭)this.tabControl.SelectedIndex)
				{
                    case 수주진행현황탭.수주번호:
						flex = this._flex수주번호H;
						break;
                    case 수주진행현황탭.발주번호:
						flex = this._flex발주번호M;
						break;
                    case 수주진행현황탭.입고일:
						flex = this._flex입고일M;
						break;
                    case 수주진행현황탭.납기일:
						flex = this._flex납기일M;
						break;
                    case 수주진행현황탭.매입처:
                        flex = this._flex매입처M;
                        break;
                    case 수주진행현황탭.매출처:
						flex = this._flex매출처M;
						break;
                    case 수주진행현황탭.품목:
						flex = this._flex품목M;
						break;
                    case 수주진행현황탭.수주유형:
						flex = this._flex수주유형M;
						break;
                    case 수주진행현황탭.영업그룹:
						flex = this._flex영업그룹M;
						break;
                    case 수주진행현황탭.리스트:
						flex = this._flex리스트H;
						break;
				}

				if (flex == null || flex.GetChanges() == null) return false;

				if (flex == this._flex수주번호H && flex.GetChanges().Select("CD_DLV_MAIN = 'WM004' AND ISNULL(CD_DLV_SUB, '') = ''").Length > 0)
				{
					this.ShowMessage("납품방법이 선적인 경우 선적장소를 입력하지 않으면 저장 할 수 없습니다.");
					return false;
				}

				if (flex == this._flex수주번호H && flex.GetChanges().Select("CD_DLV_MAIN = 'WM003' AND ISNULL(CD_DLV_SUB, '') = ''").Length > 0)
				{
					this.ShowMessage("납품방법이 택배인 경우 비용처리 방법을 입력하지 않으면 저장 할 수 없습니다.");
					return false;
				}

				if (this._biz.SaveData(this.ctx회사.CodeValue, flex.GetChanges()))
				{
					flex.AcceptChanges();
				}

				return true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return false;
		}

		public override void OnToolBarPrintButtonClicked(object sender, EventArgs e)
		{
			ReportHelper reportHelper;
			DataTable dt1, dt2;
			DataRow[] dataRowArray1;
			
			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if ((수주진행현황탭)this.tabControl.SelectedIndex != 수주진행현황탭.수주번호) return;
				if (!this._flex수주번호H.HasNormalRow || !this._flex수주번호L.HasNormalRow) return;

				dataRowArray1 = this._flex수주번호H.DataTable.Select("S = 'Y'");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					dt1 = this._flex수주번호H.DataTable.Clone();
					dt2 = this._flex수주번호L.DataTable.Clone();

					foreach (DataRow dr in dataRowArray1)
					{
						dt1.ImportRow(dr);

						foreach (DataRow dr1 in this._flex수주번호L.DataTable.Select("NO_SO = '" + D.GetString(dr["NO_SO"]) + "'"))
						{
							dt2.ImportRow(dr1);
						}
					}

					reportHelper = Util.SetRPT("R_CZ_SA_SOSCH2", "수주진행현황", this.ctx회사.CodeValue, dt1, dt2);

					reportHelper.SetDataTable(dt1, 1);
					reportHelper.SetDataTable(dt2, 2);

					Util.RPT_Print(reportHelper);
				}
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

				Settings1.Default.발주상태 = this.발주상태;
				Settings1.Default.입고상태 = this.입고상태;
				Settings1.Default.의뢰상태 = this.의뢰상태;
				Settings1.Default.출고상태 = this.출고상태;
				Settings1.Default.매출상태 = this.매출상태;
				Settings1.Default.마감상태 = this.마감상태;

                Settings1.Default.자동제출 = this.chk자동제출.Checked;
                Settings1.Default.비용제외 = this.chk비용제외.Checked;
                Settings1.Default.무상공급제외 = this.chk무상공급제외.Checked;

				Settings1.Default.Save();

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

		#region 그리드 이벤트
		private void _flex수주번호_CheckHeaderClick(object sender, EventArgs e)
		{
			FlexGrid flex = sender as FlexGrid;

			try
			{
				if (!this._flex수주번호H.HasNormalRow) return;

				this._flex수주번호H.Redraw = false;

				//데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
				this._flex수주번호H.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

				if (!this._flex수주번호H.HasNormalRow || !this._flex수주번호L.HasNormalRow) return;

				for (int h = 0; h < this._flex수주번호H.Rows.Count - 1; h++)
				{
					MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h + 1), D.GetString(this._flex수주번호H.Rows.Count - 1) });

					this._flex수주번호H.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				this._flex수주번호H.Redraw = true;
			}
		}

		private void flexH_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			FlexGrid grid;
			string 수주번호, query, fileName;

			try
			{
				grid = (sender as FlexGrid);
				if (grid.HasNormalRow == false) return;
				if (grid.MouseRow < grid.Rows.Fixed) return;
				if (Global.MainFrame.LoginInfo.CompanyCode != this.ctx회사.CodeValue) return;

				수주번호 = D.GetString(grid["NO_SO"]);

				if (grid.ColSel == grid.Cols["NO_SO"].Index)
				{
					if (Global.MainFrame.IsExistPage("P_CZ_SA_SO_REG", false))
						Global.MainFrame.UnLoadPage("P_CZ_SA_SO_REG", false);

					Global.MainFrame.LoadPageFrom("P_CZ_SA_SO_REG", Global.MainFrame.DD("수주등록"), this.Grant, new object[] { 수주번호, false });
				}
				else if (grid.ColSel == grid.Cols["YN_PROFORMA"].Index)
				{
					if (D.GetString(grid["YN_PROFORMA"]) != "Y") return;

					query = @"SELECT TOP 1 NM_FILE_REAL 
							  FROM CZ_MA_WORKFLOWL WITH(NOLOCK)" + Environment.NewLine +
							 "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
							 "AND NO_KEY = '" + 수주번호 + "'" + Environment.NewLine +
							@"AND TP_STEP = '56'
							  ORDER BY DTS_INSERT DESC";

					fileName = DBHelper.ExecuteScalar(query).ToString();

					if (!string.IsNullOrEmpty(fileName))
						FileMgr.Download_WF(Global.MainFrame.LoginInfo.CompanyCode, 수주번호, fileName, true);
					else
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				else if (grid.ColSel == grid.Cols["YN_51_FILE"].Index)
				{
					if (D.GetString(grid["YN_51_FILE"]) != "Y") return;

					query = @"SELECT TOP 1 NM_FILE_REAL 
							  FROM CZ_MA_WORKFLOWL WITH(NOLOCK)" + Environment.NewLine +
							 "WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
							 "AND NO_KEY = '" + 수주번호 + "'" + Environment.NewLine +
							@"AND TP_STEP = '51'
							  ORDER BY DTS_INSERT DESC";

					fileName = DBHelper.ExecuteScalar(query).ToString();

					if (!string.IsNullOrEmpty(fileName))
						FileMgr.Download_WF(Global.MainFrame.LoginInfo.CompanyCode, 수주번호, fileName, true);
					else
						this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
				}
				else if (grid.ColSel == grid.Cols["YN_PACK"].Index)
				{
					if (Global.MainFrame.IsExistPage("P_CZ_PU_RCV_REG", false))
						Global.MainFrame.UnLoadPage("P_CZ_PU_RCV_REG", false);

					Global.MainFrame.LoadPageFrom("P_CZ_PU_RCV_REG", Global.MainFrame.DD("입고등록"), this.Grant, new object[] { 수주번호 });
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void flexL_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			FlexGrid grid;
			string 수주번호, 매입처코드, query, fileName;

			try
			{
				grid = (sender as FlexGrid);
				if (grid.HasNormalRow == false) return;
				if (grid.MouseRow < grid.Rows.Fixed) return;
				if (grid.ColSel != grid.Cols["LN_SUPPLIER"].Index) return;
				if (Global.MainFrame.LoginInfo.CompanyCode != this.ctx회사.CodeValue) return;

				수주번호 = D.GetString(grid["NO_SO"]);
				매입처코드 = D.GetString(grid["CD_SUPPLIER"]);

				query = @"SELECT TOP 1 NM_FILE_REAL AS NM_FILE
						  FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
						  WHERE CD_COMPANY = '" + this.ctx회사.CodeValue + "'" + Environment.NewLine +
						 "AND NO_KEY = '" + 수주번호 + "'" + Environment.NewLine +
						 "AND TP_STEP = '10'" + Environment.NewLine +
						 "AND CD_SUPPLIER = '" + 매입처코드 + "'" + Environment.NewLine +
                         "ORDER BY DTS_INSERT DESC";

				fileName = DBHelper.ExecuteScalar(query).ToString();

				if (!string.IsNullOrEmpty(fileName))
					FileMgr.Download_WF(Global.MainFrame.LoginInfo.CompanyCode, 수주번호, fileName, true);
				else
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void flexL_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;
				if (flexGrid.Cols[e.Col].Name != "DC_RMK_LOG") return;
				if (flexGrid == null || flexGrid.GetChanges() == null) return;

				if (this._biz.SaveDetailData(this.ctx회사.CodeValue, flexGrid.GetChanges()))
				{
					flexGrid.AcceptChanges();
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			try
			{
				FlexGrid flex = sender as FlexGrid;

				this.SetMiddleData(flex);
				this.SetLineData(flex);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex수주번호H_CellContentChanged(object sender, CellContentEventArgs e)
		{
			try
			{
				this._biz.SaveContent(e.ContentType, e.CommandType, D.GetString(this._flex수주번호H[e.Row, "NO_SO"]), e.SettingValue, this.ctx회사.CodeValue);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				if (flexGrid.Cols[e.Col].Name == "DC_RMK_TEXT2" ||
					flexGrid.Cols[e.Col].Name == "DC_RMK1" ||
					flexGrid.Cols[e.Col].Name == "TXT_USERDEF1")
				{
					this.SaveData();
				}
				else if (flexGrid.Cols[e.Col].Name == "DT_DLV")
				{
					DBHelper.ExecuteNonQuery("SP_CZ_SA_SOSCH2H_DLV_U", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					  flexGrid["NO_SO"].ToString(),
																					  flexGrid["DT_DLV"].ToString(),
																					  Global.MainFrame.LoginInfo.UserID });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void SetMiddleData(FlexGrid flex)
		{
			DataTable dt = null;
			string key = string.Empty,
				   filter = string.Empty;

			try
			{
				if (flex.Name == this._flex발주번호H.Name)
				{
					key = flex["NO_PO"].ToString();
					filter = "NO_PO = '" + key + "'";
				}
				else if (flex.Name == this._flex입고일H.Name)
				{
					key = flex["DT_IN"].ToString();
					filter = "DT_IN = '" + key + "'";
				}
				else if (flex.Name == this._flex납기일H.Name)
				{
					key = flex["DT_DUEDATE"].ToString();
					filter = "DT_DUEDATE = '" + key + "'";
				}
                else if (flex.Name == this._flex매입처H.Name)
                {
                    key = flex["CD_SUPPLIER"].ToString();
                    filter = "CD_SUPPLIER = '" + key + "'";
                }
				else if (flex.Name == this._flex매출처H.Name)
				{
					key = flex["CD_PARTNER"].ToString();
					filter = "CD_PARTNER = '" + key + "'";
				}
				else if (flex.Name == this._flex품목H.Name)
				{
					key = flex["CD_ITEM"].ToString();
					filter = "CD_ITEM = '" + key + "'";
				}
				else if (flex.Name == this._flex수주유형H.Name)
				{
					key = flex["TP_SO"].ToString();
					filter = "TP_SO = '" + key + "'";
				}
				else if (flex.Name == this._flex영업그룹H.Name)
				{
					key = flex["CD_SALEGRP"].ToString();
					filter = "CD_SALEGRP = '" + key + "'";
				}
				else
				{
					return;
				}

				if (flex.DetailQueryNeed == true)
				{
                    dt = this._biz.SearchMiddle((수주진행현황탭)this.tabControl.SelectedIndex, new object[]{ this.ctx회사.CodeValue, 
														                                                     key,
														                                                     D.GetString(this.cbo수주번호.SelectedValue),
														                                                     this.txt수주번호.Text,
														                                                     this.dtp수주일자.StartDateToString,
														                                                     this.dtp수주일자.EndDateToString,
														                                                     this.bpc영업그룹.QueryWhereIn_Pipe,
														                                                     D.GetString(this.cbo거래구분.SelectedValue),
														                                                     D.GetString(this.cbo수주상태.SelectedValue),
														                                                     this.bpc수주유형.QueryWhereIn_Pipe,
														                                                     this.bpc매출처.QueryWhereIn_Pipe,
														                                                     this.bpc매출처그룹.QueryWhereIn_Pipe,
																											 this.bpc영업담당자.QueryWhereIn_Pipe,
																											 this.bpc제품군.QueryWhereIn_Pipe,
														                                                     this.txt매출처발주번호.Text,
														                                                     this.bpc매입처.QueryWhereIn_Pipe,
														                                                     this.bpc호선.QueryWhereIn_Pipe,
														                                                     this.발주상태,
														                                                     this.입고상태,
														                                                     this.의뢰상태,
														                                                     this.출고상태,
														                                                     this.매출상태,
														                                                     this.마감상태,
														                                                     this.ctx영업물류담당자.CodeValue,
														                                                     (this.chk자동제출.Checked == true ? "Y" : "N"),
														                                                     (this.chk비용제외.Checked == true ? "Y" : "N"),
                                                                                                             (this.chk무상공급제외.Checked == true ? "Y" : "N") });
				}

				if (dt != null && this.ctx회사.CodeValue != Global.MainFrame.LoginInfo.CompanyCode)
				{
					if (dt.Columns.Contains("NM_EXCH"))
					{
						dt.Columns.Remove("NM_EXCH");
						dt.Columns.Add("NM_EXCH");
					}

					if (dt.Columns.Contains("RT_EXCH"))
					{
						dt.Columns.Remove("RT_EXCH");
						dt.Columns.Add("RT_EXCH");
					}

					if (dt.Columns.Contains("AM_EX_SO"))
					{
						dt.Columns.Remove("AM_EX_SO");
						dt.Columns.Add("AM_EX_SO");
					}

					if (dt.Columns.Contains("AM_SO"))
					{
						dt.Columns.Remove("AM_SO");
						dt.Columns.Add("AM_SO");
					}
				}

				flex.DetailGrids[0].BindingAdd(dt, filter);
				flex.DetailQueryNeed = false;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void SetLineData(FlexGrid flex)
		{
			DataTable dt = null;
			string key = string.Empty,
				   key2 = string.Empty,
				   filter = string.Empty;

			try
			{
				if (flex.Name == this._flex수주번호H.Name)
				{
					key = flex["NO_SO"].ToString();
					key2 = string.Empty;
					filter = "NO_SO = '" + key + "'";
				}
				else if (flex.Name == this._flex발주번호M.Name)
				{
					key = flex["NO_PO"].ToString();
					key2 = flex["NO_SO"].ToString();
					filter = "NO_PO = '" + key + "' AND NO_SO = '" + key2 + "'";
				}
				else if (flex.Name == this._flex입고일M.Name)
				{
					key = flex["DT_IN"].ToString();
					key2 = flex["NO_SO"].ToString();
					filter = "DT_IN = '" + key + "' AND NO_SO = '" + key2 + "'";
				}
				else if (flex.Name == this._flex납기일M.Name)
				{
					key = flex["DT_DUEDATE"].ToString();
					key2 = flex["NO_SO"].ToString();
					filter = "DT_DUEDATE = '" + key + "' AND NO_SO = '" + key2 + "'";
				}
                else if (flex.Name == this._flex매입처M.Name)
                {
                    key = flex["CD_SUPPLIER"].ToString();
                    key2 = flex["NO_SO"].ToString();
                    filter = "CD_SUPPLIER = '" + key + "' AND NO_SO = '" + key2 + "'";
                }
				else if (flex.Name == this._flex매출처M.Name)
				{
					key = flex["CD_PARTNER"].ToString();
					key2 = flex["NO_SO"].ToString();
					filter = "CD_PARTNER = '" + key + "' AND NO_SO = '" + key2 + "'";
				}
				else if (flex.Name == this._flex품목M.Name)
				{
					key = flex["CD_ITEM"].ToString();
					key2 = flex["NO_SO"].ToString();
					filter = "CD_ITEM = '" + key + "' AND NO_SO = '" + key2 + "'";
				}
				else if (flex.Name == this._flex수주유형M.Name)
				{
					key = flex["TP_SO"].ToString();
					key2 = flex["NO_SO"].ToString();
					filter = "TP_SO = '" + key + "' AND NO_SO = '" + key2 + "'";
				}
				else if (flex.Name == this._flex영업그룹M.Name)
				{
					key = flex["CD_SALEGRP"].ToString();
					key2 = flex["NO_SO"].ToString();
					filter = "CD_SALEGRP = '" + key + "' AND NO_SO = '" + key2 + "'";
				}
				else
				{
					return;
				}

				if (flex.DetailQueryNeed == true)
				{
                    dt = this._biz.SearchLine((수주진행현황탭)this.tabControl.SelectedIndex, new object[] { this.ctx회사.CodeValue,
														                                                    key,
														                                                    key2,
														                                                    this.cbo거래구분.SelectedValue == null ? string.Empty : this.cbo거래구분.SelectedValue.ToString(),
														                                                    this.cbo수주상태.SelectedValue == null ? string.Empty : this.cbo수주상태.SelectedValue.ToString(),
														                                                    this.bpc제품군.QueryWhereIn_Pipe,
														                                                    this.txt매출처발주번호.Text,
														                                                    this.bpc매입처.QueryWhereIn_Pipe,
														                                                    (this.chk자동제출.Checked == true ? "Y" : "N"),
														                                                    (this.chk비용제외.Checked == true ? "Y" : "N"),
                                                                                                            (this.chk무상공급제외.Checked == true ? "Y" : "N") });
				}

				if (dt != null && this.ctx회사.CodeValue != Global.MainFrame.LoginInfo.CompanyCode)
				{
					if (dt.Columns.Contains("UM_EX"))
					{
						dt.Columns.Remove("UM_EX");
						dt.Columns.Add("UM_EX");
					}

					if (dt.Columns.Contains("UM"))
					{
						dt.Columns.Remove("UM");
						dt.Columns.Add("UM");
					}

					if (dt.Columns.Contains("AM_EX"))
					{
						dt.Columns.Remove("AM_EX");
						dt.Columns.Add("AM_EX");
					}

					if (dt.Columns.Contains("AM"))
					{
						dt.Columns.Remove("AM");
						dt.Columns.Add("AM");
					}
				}

				flex.DetailGrids[0].BindingAdd(dt, filter);
				flex.DetailQueryNeed = false;
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#region 기타 이벤트
		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			switch (e.HelpID)
			{
				case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB1:
					e.HelpParam.P61_CODE1 = "N";
					e.HelpParam.P62_CODE2 = "Y";
					break;
				case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1:
					if (e.ControlName == this.bpc제품군.Name)
						e.HelpParam.P41_CD_FIELD1 = "MA_B000066";
					else if(e.ControlName == this.bpc매출처그룹.Name)
						e.HelpParam.P41_CD_FIELD1 = "MA_B000065";
					break;
				case Duzon.Common.Forms.Help.HelpID.P_USER:
                    //e.HelpParam.MultiRow = null;
					break;
			}
		}

		private void btn부분송품적용_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				if (!this._flex수주번호L.HasNormalRow) return;

				dataRowArray = this._flex수주번호L.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						query = @"UPDATE SA_SOL
                                  SET YN_PARTIAL_DELV = 'Y',
                                      ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
									 "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" + Environment.NewLine +
								 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								 "AND NO_SO = '" + D.GetString(dr["NO_SO"]) + "'" + Environment.NewLine +
								 "AND SEQ_SO = '" + D.GetString(dr["SEQ_SO"]) + "'";

						DBHelper.ExecuteScalar(query);
					}

					Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn부분송품적용.Text);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn부분송품해제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				if (!this._flex수주번호L.HasNormalRow) return;

				dataRowArray = this._flex수주번호L.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						query = @"UPDATE SA_SOL
                                  SET YN_PARTIAL_DELV = 'N',
                                      ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
									 "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" + Environment.NewLine +
								 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								 "AND NO_SO = '" + D.GetString(dr["NO_SO"]) + "'" + Environment.NewLine +
								 "AND SEQ_SO = '" + D.GetString(dr["SEQ_SO"]) + "'";

						DBHelper.ExecuteScalar(query);
					}

					Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn부분송품해제.Text);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn청구호선적용_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				if (!this._flex수주번호H.HasNormalRow) return;

				dataRowArray = this._flex수주번호H.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						query = @"UPDATE SA_SOH
                                  SET YN_CONFIRM = 'Y',
                                      ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
									 "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" + Environment.NewLine +
								 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								 "AND NO_SO = '" + D.GetString(dr["NO_SO"]) + "'";

						DBHelper.ExecuteScalar(query);
					}

					Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn청구호선적용.Text);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn청구호선해제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				if (!this._flex수주번호H.HasNormalRow) return;

				dataRowArray = this._flex수주번호H.DataTable.Select("S = 'Y'", string.Empty, DataViewRowState.CurrentRows);

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					Global.MainFrame.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						query = @"UPDATE SA_SOH
                                  SET YN_CONFIRM = 'N',
                                      ID_UPDATE = '" + Global.MainFrame.LoginInfo.UserID + "'," + Environment.NewLine +
									 "DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())" + Environment.NewLine +
								 "WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'" + Environment.NewLine +
								 "AND NO_SO = '" + D.GetString(dr["NO_SO"]) + "'";

						DBHelper.ExecuteScalar(query);
					}

					Global.MainFrame.ShowMessage(공통메세지._작업을완료하였습니다, this.btn청구호선해제.Text);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnSDOC출력_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (DataRow dr in this._flex수주번호H.DataTable.Select("S = 'Y'"))
				{
					Parsing.EXCEL.SDOC.SDOC_EXCEL(dr["NO_SO"].ToString());
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btnSDOC출력.Text);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn선적정보등록_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				if (!this._flex수주번호H.HasNormalRow)
					return;

				DataRow[] dataRowArray = this._flex수주번호H.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						query = @"INSERT INTO CZ_RPA_WORK_QUEUE 
(
	CD_COMPANY,
	CD_RPA,
	NO_FILE,
	CD_PARTNER,
	YN_READ,
	YN_DONE,
	NO_BOTS,
	URGENT,
	ID_INSERT,
	DTS_INSERT	
)
VALUES
(
	'{0}',
	'GMARINE_SHIPMENT',
	'{1}',
	'{2}',
	'N',
	'N',
	'2',
	'3',
	'SYSTEM',
	NEOE.SF_SYSDATE(GETDATE())
)";
						DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	(dr["NO_IMO"].ToString() + "_" + dr["NO_PO_PARTNER"].ToString()),
																	dr["CD_PARTNER"].ToString()));

						query = @"UPDATE SH
SET SH.TXT_USERDEF3 = '등록요청' 
FROM SA_SOH SH
WHERE SH.CD_COMPANY = '{0}'
AND SH.NO_SO = '{1}'";

						DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	dr["NO_SO"].ToString()));
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn선적정보등록.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion
	}
}
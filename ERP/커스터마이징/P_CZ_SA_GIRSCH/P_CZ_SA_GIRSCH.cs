using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.DRDesigner.Utils;
using Duzon.ERPU;
using Duzon.ERPU.Grant;
using Duzon.ERPU.MF;
using Duzon.ERPU.Utils;
using Duzon.Windows.Print;
using DX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_GIRSCH : PageBase
	{
		#region ♥ 멤버필드
		private P_CZ_SA_GIRSCH_BIZ _biz = new P_CZ_SA_GIRSCH_BIZ();
		private P_CZ_SA_GIRSCH_GW _gw = new P_CZ_SA_GIRSCH_GW();
		private DataTable 운송방법, 계정구분;
		private bool isConfirm;

		[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetDefaultPrinter(string Name);

		private string 확정일자
		{
			get
			{
				if (this.dtp확정일자.Checked)
				{
					return string.Format("{0:0000}", this.dtp확정일자.Value.Year) +
						   string.Format("{0:00}", this.dtp확정일자.Value.Month) +
						   string.Format("{0:00}", this.dtp확정일자.Value.Day) +
						   string.Format("{0:00}", this.dtp확정일자.Value.Hour) +
						   string.Format("{0:00}", this.dtp확정일자.Value.Minute) +
						   "00";
				}
				else
				{
					return string.Empty;
				}
			}
		}

		private enum 탭구분
		{
			의뢰번호,
			제출일자,
			매출처,
			업무,
			출고예정일,
			포장예정일
		}

		bool Chk조회일자 { get { return Checker.IsValid(this.dtp조회일자, true, "조회일자"); } }
		#endregion

		#region ♥ 초기화
		public P_CZ_SA_GIRSCH()
		{
			StartUp.Certify(this);
			InitializeComponent();

			UGrant ugrant = new UGrant();
			this.isConfirm = ugrant.GrantButton("P_CZ_SA_GIRSCH", "CONFIRM");

			MainGrids = new FlexGrid[] { this._flex의뢰번호H, this._flex의뢰번호L,
										 this._flex제출일자H, this._flex제출일자M, this._flex제출일자L,                         
										 this._flex매출처H, this._flex매출처M, this._flex매출처L,
										 this._flex업무H, this._flex업무M, this._flex업무L,
										 this._flex출고예정일H, this._flex출고예정일M, this._flex출고예정일L,
										 this._flex포장예정일H, this._flex포장예정일M, this._flex포장예정일L };
		}

		protected override void InitLoad()
		{
			this.InitEvent();
		}

		private void SetHeaderGrid(FlexGrid flex)
		{
			flex.BeginSetting(1, 1, false);

			if (flex.Name == this._flex의뢰번호H.Name)
			{
				flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
				flex.SetCol("NM_GW_STATUS", "결재상태", 80);
				flex.SetCol("CD_TAEGBAE", "택배사", 100, true);
				flex.SetCol("DC_FORWARD_OUT", "출고상태", 100);

				flex.SetDataMap("CD_TAEGBAE", DBHelper.GetDataTable(string.Format(@"SELECT NULL AS CODE, 
																						   NULL AS NAME 
																				    UNION ALL
																					SELECT CD_SYSDEF AS CODE,
																					       NM_SYSDEF AS NAME
																					FROM CZ_MA_CODEDTL WITH(NOLOCK)
																					WHERE CD_COMPANY = '{0}'
																					AND CD_FIELD = 'CZ_SA00067'", Global.MainFrame.LoginInfo.CompanyCode)), "CODE", "NAME");

				flex.SetDummyColumn("S");
			}
			else if (flex.Name == this._flex출고예정일M.Name || 
					 flex.Name == this._flex포장예정일M.Name)
			{
				flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
				flex.SetCol("CD_TAEGBAE", "택배사", 100, true);
				flex.SetCol("DC_FORWARD_OUT", "출고상태", 100);

				flex.SetDataMap("CD_TAEGBAE", DBHelper.GetDataTable(string.Format(@"SELECT NULL AS CODE, 
																						   NULL AS NAME 
																				    UNION ALL
																					SELECT CD_SYSDEF AS CODE,
																					       NM_SYSDEF AS NAME
																					FROM CZ_MA_CODEDTL WITH(NOLOCK)
																					WHERE CD_COMPANY = '{0}'
																					AND CD_FIELD = 'CZ_SA00067'", Global.MainFrame.LoginInfo.CompanyCode)), "CODE", "NAME");

				flex.SetDummyColumn("S");
			}
			
			flex.SetCol("NM_COMPANY", "회사", 100);
			flex.SetCol("NO_GIR", "의뢰번호", 100);

			if (this.isConfirm)
			{
				flex.SetCol("DT_START", "포장예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("DT_COMPLETE", "출고예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			}
			else
			{
				flex.SetCol("DT_START", "포장예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("DT_COMPLETE", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			}
			
			flex.SetCol("DT_BILL", "청구예정일", 80, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("NM_TYPE1", "의뢰구분(대)", 90);
			flex.SetCol("NM_TYPE2", "의뢰구분(중)", 90);
			flex.SetCol("NM_TYPE3", "의뢰구분(소)", 90);
			flex.SetCol("YN_PACKING", "포장여부", 60, false, CheckTypeEnum.Y_N);
			flex.SetCol("YN_TAX", "납부여부", 60, false, CheckTypeEnum.Y_N);
			flex.SetCol("YN_URGENT", "긴급여부", 60, false, CheckTypeEnum.Y_N);
			flex.SetCol("YN_AUTO_SUBMIT", "자동제출", 60, false, CheckTypeEnum.Y_N);
			flex.SetCol("YN_REVIEW", "검토필요", 60);
			flex.SetCol("STA_GIR", "진행상태", 100);
			flex.SetCol("DT_DONE", "작업완료일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("NM_EMP_GIR", "의뢰자", 60);
			flex.SetCol("NM_VESSEL", "호선명", 140);
			flex.SetCol("LN_PARTNER", "매출처명", 140);
			flex.SetCol("NM_GIR_PARTNER", "납품처명", 140);
			flex.SetCol("ARRIVER_COUNTRY", "도착국가", 80);
			flex.SetCol("QT_ITEM", "품목수", 60, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("CD_PARTNER_GRP", "매출처그룹코드", false);
			flex.SetCol("NM_PARTNER_GRP", "매출처그룹명", false);
			flex.SetCol("CD_PARTNER", "매출처코드", false);
			flex.SetCol("NM_PLANT", "출고의뢰공장", false);
			flex.SetCol("NM_RETURN", "출고구분", 80);
			flex.SetCol("DC_RMK1", "취소사유", 100, true);
			flex.SetCol("DC_RMK2", "매출비고", 100, true);
			flex.SetCol("DC_RMK3", "기포장정보", 100);
			flex.SetCol("DC_RMK4", "포장비고", 100);
			flex.SetCol("NO_DELIVERY_EMAIL", "CPR발송메일", 100, true);
			flex.SetCol("YN_EXCLUDE_CPR", "CPR발송제외여부", 60, true, CheckTypeEnum.Y_N);
			flex.SetCol("DC_RMK_CI", "상업송장비고", 100, true);
			flex.SetCol("DC_RMK_PL", "포장명세서비고", 100, true);
			flex.SetCol("DT_IV", "매출일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("NO_PO_PARTNER", "매출처발주번호", 90);
			flex.SetCol("NO_SO", "수주번호", 90);
			flex.SetCol("DC_DELIVERY_ADDR", "납품처정보", 100);

			if (flex.Name == this._flex의뢰번호H.Name)
			{
				flex.SetCol("NM_ITEMGRP", "품목유형", 90);
				flex.SetCol("NO_SO_PRE", "파일구분", 90);
				flex.SetCol("NO_EMAIL", "납품처메일", 100, false);
				flex.SetCol("YN_AUTO_CPR", "CPR발송대상", 60, false, CheckTypeEnum.Y_N);
				flex.SetCol("YN_CPR", "CPR발송여부", 60, false, CheckTypeEnum.Y_N);
				flex.SetCol("DTS_CPR", "CPR발송일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("YN_FILE", "송품서류등록여부", 60, false, CheckTypeEnum.Y_N);
				flex.SetCol("DC_RMK_PACK", "포장비고", 100);
				flex.SetCol("DC_RESULT_PACK", "결과비고(요약)", 100);
				flex.SetCol("AM_GIR_10000", "원화금액(만단위)", 100, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
				flex.SetCol("DTS_PRINT", "출력일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("DTS_CUTOFF", "컷오프시간", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
				flex.SetCol("NM_CONFIRM", "확정자", 100);

				flex.Cols["DTS_CPR"].Format = "####/##/##/##:##:##";
				flex.Cols["DTS_PRINT"].Format = "####/##/##/##:##:##";
				flex.Cols["DTS_CUTOFF"].Format = "####/##/##/ ##시";
			}
			
			flex.SetCol("NM_RMK", "협조내용", 180);

			if (this.isConfirm)
				flex.SetCol("DC_RMK", "상세요청", 180, true);
			else
				flex.SetCol("DC_RMK", "상세요청", 180, false);
			
			flex.SetCol("DC_RESULT", "결과비고", 180);
			flex.SetCol("NM_EMP_SALE", "매출처담당자", false);
			flex.SetCol("DTS_INSERT", "등록일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DTS_UPDATE", "수정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DTS_SUBMIT", "제출일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			flex.SetCol("DTS_CONFIRM", "확정일자", 90, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			//flex.Cols["DT_COMPLETE"].Format = "####/##/##/##:##:##";
			flex.Cols["DT_DONE"].Format = "####/##/##/##:##:##";
			flex.Cols["DTS_INSERT"].Format = "####/##/##/##:##:##";
			flex.Cols["DTS_UPDATE"].Format = "####/##/##/##:##:##";
			flex.Cols["DTS_SUBMIT"].Format = "####/##/##/##:##:##";
			flex.Cols["DTS_CONFIRM"].Format = "####/##/##/##:##:##";
			
			flex.SetDataMap("STA_GIR", Global.MainFrame.GetComboDataCombine("S;CZ_SA00030"), "CODE", "NAME");

			flex.UseMultySorting = true;

			//flex.SettingVersion = "1.0.0.1";
			//flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			flex.SetDefault("1.0.0.1", SumPositionEnum.None);

			flex.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			flex.MouseDoubleClick += new MouseEventHandler(this._flex_MouseDoubleClick);
			flex.AfterEdit += new RowColEventHandler(this._flex_AfterEdit);

			//CheckPen기능 관련 설정
			flex.CheckPenInfo.EnabledCheckPen = true;

			// 메모기능
			flex.CellNoteInfo.EnabledCellNote = true;// 메모기능활성화 
			flex.CellNoteInfo.CategoryID = "P_CZ_SA_GIRSCH"; // page 명입력 // 같은page명을입력했을경우여러화면에서볼수있습니다. 
			flex.CellNoteInfo.DisplayColumnForDefaultNote = "NO_GIR";  // 마킹& 메모가보여질컬럼설정 
			flex.CellContentChanged += new CellContentEventHandler(this._flex의뢰번호H_CellContentChanged);
		}

		private void SetLineGrid(FlexGrid flex)
		{
			flex.BeginSetting(1, 1, false);

			if (flex.Name == this._flex의뢰번호L.Name)
			{
				flex.SetCol("S", "선택", 45, true, CheckTypeEnum.Y_N);
				flex.SetDummyColumn("S");
			}
			
			flex.SetCol("NO_SO", "수주번호", 80);
			flex.SetCol("NM_ITEMGRP", "품목유형", 90);
			flex.SetCol("SEQ_SO", "수주항번", 40);
			flex.SetCol("NO_DSP", "순번", 40);
			flex.SetCol("CD_ITEM_PARTNER", "매출처품번", 100);
			flex.SetCol("NM_ITEM_PARTNER", "매출처품명", 120);
			flex.SetCol("CD_ITEM", "품목코드", 100);
			flex.SetCol("NM_ITEM", "품목명", 120);
			flex.SetCol("STND_ITEM", "규격", false);
			flex.SetCol("UNIT", "단위", 40);
			flex.SetCol("NM_SO", "수주형태", false);
			flex.SetCol("NM_BUSI", "거래구분", false);
			flex.SetCol("QT_SO", "수주수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("QT_GIR", "의뢰수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("QT_GIR_STOCK", "재고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("QT_GI", "출고수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("YN_BL", "납부여부", 80, false, CheckTypeEnum.Y_N);
			flex.SetCol("QT_TAX", "납부수량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("NM_EXCH", "통화명", false);
			flex.SetCol("UM_EX", "외화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("AM_GIR", "외화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("UM", "원화단가", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);
			flex.SetCol("AM_GIRAMT", "원화금액", 80, false, typeof(decimal), FormatTpType.FOREIGN_MONEY);

			flex.SetCol("FG_TRANSPORT", "운송방법", false);
			flex.SetCol("CLS_ITEM", "계정구분", false);
			flex.SetCol("NM_TP_GI", "출고형태", false);
			flex.SetCol("DT_DUEDATE", "납기요구일", false);
			flex.SetCol("NM_SALEGRP", "영업그룹", false);
			flex.SetCol("GI_PARTNER", "납품처코드", false);
			flex.SetCol("GI_LN_PARTNER", "납품처명", false);
			flex.SetCol("NM_KOR", "수주담당자", false);
			flex.SetCol("NO_PROJECT", "프로젝트코드", false);
			flex.SetCol("NM_PROJECT", "프로젝트명", false);
			flex.SetCol("NM_SUPPLIER", "매입처", 120);
			flex.SetCol("NM_SL", "출고창고", 100);
			flex.SetCol("NO_LOCATION", "로케이션", 100, false);
			flex.SetCol("UNIT_WEIGHT", "중량단위", false);
			flex.SetCol("WEIGHT", "중량", 80, false, typeof(decimal), FormatTpType.QUANTITY);
			flex.SetCol("L_DC_RMK", "라인비고", false);
			flex.SetCol("QT_GI_WEIGHT", "출고중량", false);
			flex.SetCol("NO_PO_PARTNER", "매출처발주번호", 100);
			flex.SetCol("YN_ADD_STOCK", "추가반출", 60, false, CheckTypeEnum.Y_N);

			flex.SetDataMap("FG_TRANSPORT", this.운송방법, "CODE", "NAME");
			flex.SetDataMap("CLS_ITEM", this.계정구분, "CODE", "NAME");

			//flex.SettingVersion = "1.0.0.8";
			//flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.Top);
			flex.SetDefault("1.0.0.8", SumPositionEnum.Top);

			flex.SetExceptSumCol("UM", "UM_EX");

			flex.Styles.Add("미납부").ForeColor = Color.Black;
			flex.Styles.Add("미납부").BackColor = Color.White;
			flex.Styles.Add("납부").ForeColor = Color.Red;
			flex.Styles.Add("납부").BackColor = Color.Yellow;

			flex.OwnerDrawCell += new OwnerDrawCellEventHandler(this._flex_OwnerDrawCell);
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.oneGrid1.UseCustomLayout = true;
			this.oneGrid1.InitCustomLayout();

			this.dtp확정일자.CustomFormat = "yyyy/MM/dd tt hh:mm ~";
			this.chk자동생성건제외.Checked = true;

			this.cbo일자유형.DataSource = MA.GetCodeUser(new string[] { "", "001", "002", "003", "004" }, new string[] { "", "포장예정일", "제출일자", "출고예정일", "의뢰일자" });
			this.cbo일자유형.ValueMember = "CODE";
			this.cbo일자유형.DisplayMember = "NAME";

			if (Global.MainFrame.LoginInfo.GroupID == "LOG")
			{
				this.cbo일자유형.SelectedValue = "001";

				this.dtp조회일자.StartDateToString = MainFrameInterface.GetDateTimeToday().AddDays(-7).ToString("yyyyMMdd");
				this.dtp조회일자.EndDateToString = MainFrameInterface.GetDateTimeToday().AddDays(1).ToString("yyyyMMdd");
			}
			else
			{
				this.cbo일자유형.SelectedValue = "004";

				this.dtp조회일자.StartDateToString = MainFrameInterface.GetDateTimeToday().AddDays(-7).ToString("yyyyMMdd");
				this.dtp조회일자.EndDateToString = MainFrameInterface.GetStringToday;
			}

			this.dtp포장예정일.Text = MainFrameInterface.GetStringToday;

			DataSet m_dsCombo = this.GetComboData("S;SA_B000028", 
												  "N;MA_PLANT", 
												  "S;PU_C000027", 
												  "S;MA_BIZAREA", 
												  "S;TR_IM00008", 
												  "S;MA_B000010", 
												  "S;CZ_SA00021");

			this.운송방법 = m_dsCombo.Tables[4].Copy();
			this.계정구분 = m_dsCombo.Tables[5].Copy();

			this.cbo처리상태.DataSource = m_dsCombo.Tables[0];
			this.cbo처리상태.ValueMember = "CODE";
			this.cbo처리상태.DisplayMember = "NAME";
			
			this.cbo출고구분.DataSource = m_dsCombo.Tables[2];
			this.cbo출고구분.ValueMember = "CODE";
			this.cbo출고구분.DisplayMember = "NAME";

			this.cbo물류업무대.DataSource = m_dsCombo.Tables[6];
			this.cbo물류업무대.ValueMember = "CODE";
			this.cbo물류업무대.DisplayMember = "NAME";

			this.cbo적재허가서.DataSource = MA.GetCodeUser(new string[] { "192.168.2.59", 
																		  "192.168.2.60", 
																		  "192.168.2.61" }, new string[] { "적재허가서1", 
																										   "적재허가서2", 
																										   "적재허가서3" });
			this.cbo적재허가서.ValueMember = "CODE";
			this.cbo적재허가서.DisplayMember = "NAME";

			this.cbo라벨출력.DataSource = DBHelper.GetDataTable(@"SELECT CD_FLAG1 AS CODE,
	   NM_SYSDEF AS NAME
FROM MA_CODEDTL
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_PDA0001'
AND CD_FLAG2 = 'ZT421'
UNION ALL
SELECT '192.168.0.191' AS CODE,
	   'TEST' AS NAME");
			this.cbo라벨출력.ValueMember = "CODE";
			this.cbo라벨출력.DisplayMember = "NAME";

			this.cbo택배사.DataSource = DBHelper.GetDataTable(string.Format(@"SELECT NULL AS CODE, 
																			  	     NULL AS NAME 
																			  UNION ALL
																			  SELECT CD_SYSDEF AS CODE,
																			         NM_SYSDEF AS NAME
																			  FROM CZ_MA_CODEDTL WITH(NOLOCK)
																			  WHERE CD_COMPANY = '{0}'
																			  AND CD_FIELD = 'CZ_SA00067'", Global.MainFrame.LoginInfo.CompanyCode));
			this.cbo택배사.ValueMember = "CODE";
			this.cbo택배사.DisplayMember = "NAME";

			if (Global.MainFrame.LoginInfo.CompanyCode == "K100" && 
				Global.MainFrame.LoginInfo.GroupID == "LOG")
			{
				this.bpc회사.AddItem("K100", "(주)딘텍");
				this.bpc회사.AddItem("K200", "(주)두베코");
			}
			else
			{
				this.bpc회사.AddItem(Global.MainFrame.LoginInfo.CompanyCode, Global.MainFrame.LoginInfo.CompanyName);
			}

			if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
			{
				this.btn출고품목삭제.Visible = true;
				this.btn출고취소신청.Visible = true;
				this.btn부대비용.Visible = true;
				this.btnCPR자동발송.Visible = true;
				this.bpPanelControl25.Visible = true;
				this.btn적재허가서.Visible = true;
			}
			else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
			{
				this.btn출고품목삭제.Visible = false;
				this.btn출고취소신청.Visible = true;
				this.btn부대비용.Visible = true;
				this.btnCPR자동발송.Visible = true;
				this.bpPanelControl25.Visible = true;
				this.btn적재허가서.Visible = true;
			}
			else
			{
				this.btn출고품목삭제.Visible = false;
				this.btn출고취소신청.Visible = false;
				this.btn부대비용.Visible = false;
				this.btnCPR자동발송.Visible = false;
				this.bpPanelControl25.Visible = false;
				this.btn적재허가서.Visible = false;
			}
			
			UGrant ugrant = new UGrant();
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "DELETE", this.btn협조전삭제, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CONFIRM", this.btn확정, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "UNCONFIRM", this.btn확정취소, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CLOSE_PACK", this.btn포장종결, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "UNCLOSE", this.btn포장종결취소, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CONFIRM", this.btnDHL발송, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CONFIRM", this.btnOneBill발송, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CONFIRM", this.btnCPR자동발송, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CONFIRM", this.btn출력대상설정, true);
			ugrant.GrantButtonEnble(Global.MainFrame.CurrentPageID, "CONFIRM", this.btn출력대상해제, true);
		}

		private void InitEvent()
		{
			this.btn협조전삭제.Click += new EventHandler(this.btn협조전삭제_Click);

			this.btn확정.Click += new EventHandler(this.btn확정_Click);
			this.btn확정취소.Click += new EventHandler(this.btn확정_Click);
			this.btn포장종결.Click += new EventHandler(this.btn확정_Click);
			this.btn포장종결취소.Click += new EventHandler(this.btn확정_Click);

			this.btn인수증출력.Click += new EventHandler(this.btn인수증출력_Click);
			this.btn포장명세서출력.Click += new EventHandler(this.btn포장명세서출력_Click);
			this.btn상업송장출력.Click += new EventHandler(this.btn상업송장출력_Click);
			this.btn상업송장면허.Click += Btn상업송장면허_Click;
			this.btnCheckList출력.Click += new EventHandler(this.btnCheckList출력_Click);
			this.btnCPR출력.Click += BtnCPR출력_Click;
			this.btnCPR자동발송.Click += BtnCPR자동발송_Click;
			this.btn쉬핑마크출력.Click += Btn쉬핑마크출력_Click;
			this.btn포장명세서출력_라벨.Click += Btn포장명세서출력_라벨_Click;

			this.btn출력대상설정.Click += Btn출력대상설정_Click;
			this.btn출력대상해제.Click += Btn출력대상해제_Click;

			this.btnDHL발송.Click += btnDHL발송_Click;
			this.btnOneBill발송.Click += BtnOneBill발송_Click;
			this.btnDHL발송정보.Click += BtnDHL발송정보_Click;
			this.btn출고취소신청.Click += Btn출고취소신청_Click;
			this.btn부대비용.Click += Btn부대비용_Click;
			this.btn출고품목삭제.Click += Btn출고품목삭제_Click;
			this.btn송품서류등록.Click += Btn송품서류등록_Click;
			this.btn적재허가서.Click += Btn적재허가서_Click;
			this.btn택배사적용.Click += Btn택배사적용_Click;
			this.btn포장예정일적용.Click += Btn포장예정일적용_Click;

			this.ctx호선.QueryAfter += new BpQueryHandler(this.Control_QueryAfter);
			this.ctx호선.CodeChanged += new EventHandler(this.ctx호선_CodeChanged);
			this.ctx수주유형.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.ctx납품처.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.bpc매출처그룹.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);
			this.bpc협조전상태.QueryBefore += new BpQueryHandler(this.Control_QueryBefore);

			this.cbo물류업무대.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);
			this.cbo물류업무중.SelectionChangeCommitted += new EventHandler(this.Control_SelectionChangeCommitted);

			this._flex의뢰번호H.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
			this._flex출고예정일M.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);
			this._flex포장예정일M.CheckHeaderClick += new EventHandler(this._flexH_CheckHeaderClick);

			this._flex제출일자H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex매출처H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex업무H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex출고예정일H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
			this._flex포장예정일H.AfterRowChange += new RangeEventHandler(this._flex_AfterRowChange);
		}

		private void Btn포장예정일적용_Click(object sender, EventArgs e)
		{
			string query;
			FlexGrid gridH;
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
					gridH = this._flex출고예정일M;
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
					gridH = this._flex포장예정일M;
				else
					gridH = this._flex의뢰번호H;

				query = @"UPDATE WD
SET WD.DT_START = '{2}',
    WD.ID_UPDATE = '{3}',
	WD.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_SA_GIRH_WORK_DETAIL WD
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";

				dataRowArray = gridH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						dr["DT_START"] = this.dtp포장예정일.Text;
						DBHelper.ExecuteScalar(string.Format(query, new object[] { dr["CD_COMPANY"].ToString(),
																				   dr["NO_GIR"].ToString(),
																				   this.dtp포장예정일.Text,
																				   Global.MainFrame.LoginInfo.UserID}));
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn택배사적용_Click(object sender, EventArgs e)
		{
			string query;
			FlexGrid gridH;
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
					gridH = this._flex출고예정일M;
				if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
					gridH = this._flex포장예정일M;
				else
					gridH = this._flex의뢰번호H;

				query = @"UPDATE WD
SET WD.CD_TAEGBAE = '{2}',
    WD.ID_UPDATE = '{3}',
	WD.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
FROM CZ_SA_GIRH_WORK_DETAIL WD
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";

				dataRowArray = gridH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						dr["CD_TAEGBAE"] = this.cbo택배사.SelectedValue;
						DBHelper.ExecuteScalar(string.Format(query, new object[] { dr["CD_COMPANY"].ToString(), 
																				   dr["NO_GIR"].ToString(),
																				   this.cbo택배사.SelectedValue,
																				   Global.MainFrame.LoginInfo.UserID}));
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn포장명세서출력_라벨_Click(object sender, EventArgs e)
		{
			DataTable 포장명세서헤더, 포장명세서라인;
			FlexGrid gridH;
			DataRow[] dataRowArray1;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
					gridH = this._flex출고예정일M;
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
					gridH = this._flex포장예정일M;
				else
					gridH = this._flex의뢰번호H;

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					if (string.IsNullOrEmpty(this.cbo라벨출력.SelectedValue.ToString()))
					{
						this.ShowMessage("라벨출력 프린터를 선택 해야 합니다.");
						return;
					}

					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
					{
						포장명세서헤더 = new DataTable();
						포장명세서라인 = new DataTable();

						this.포장명세서데이터(dr["CD_COMPANY"].ToString(), gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"), ref 포장명세서헤더, ref 포장명세서라인);

						if (포장명세서헤더.Rows.Count > 0)
						{
							Zebra zebra = new Zebra();
							zebra.Connect(this.cbo라벨출력.SelectedValue.ToString(), 6101);

							foreach (DataRow dr1 in 포장명세서헤더.Rows)
							{
								zebra.PrintPackingList(dr1, 포장명세서라인.Select(string.Format("NO_KEY = '{0}'", dr1["NO_KEY"].ToString())));
							}

							zebra.Disconnect();
						}
						else
						{
							this.ShowMessage("@ 가 존재하지 않음", this.DD("포장정보"));
						}
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn포장명세서출력_라벨.Text);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn쉬핑마크출력_Click(object sender, EventArgs e)
		{
			DataTable 포장명세서헤더, 포장명세서라인;
			FlexGrid gridH;
			DataRow[] dataRowArray1;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
					gridH = this._flex출고예정일M;
				if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
					gridH = this._flex포장예정일M;
				else
					gridH = this._flex의뢰번호H;

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					if (string.IsNullOrEmpty(this.cbo라벨출력.SelectedValue.ToString()))
					{
						this.ShowMessage("라벨출력 프린터를 선택 해야 합니다.");
						return;
					}

					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
					{
						포장명세서헤더 = new DataTable();
						포장명세서라인 = new DataTable();

						this.포장명세서데이터(dr["CD_COMPANY"].ToString(), gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"), ref 포장명세서헤더, ref 포장명세서라인);

						if (포장명세서헤더.Rows.Count > 0)
						{
							Zebra zebra = new Zebra();
							zebra.Connect(this.cbo라벨출력.SelectedValue.ToString(), 6101);

							foreach (DataRow dr1 in 포장명세서헤더.Rows)
							{
								zebra.PrintShippingMark(dr1);
							}

							zebra.Disconnect();
						}
						else
						{
							this.ShowMessage("@ 가 존재하지 않음", this.DD("포장정보"));
						}
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn쉬핑마크출력.Text);
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn출력대상해제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			FlexGrid gridH;
			string query;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
					gridH = this._flex출고예정일M;
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
					gridH = this._flex포장예정일M;
				else
					gridH = this._flex의뢰번호H;

				dataRowArray = gridH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						if (dr["CD_TYPE"].ToString() == "001")
						{
							query = @"UPDATE WD
									  SET WD.DTS_PRINT = NULL
									  FROM CZ_SA_GIRH_WORK_DETAIL WD
									  WHERE WD.CD_COMPANY = '{0}'
									  AND WD.NO_GIR = '{1}'";

							DBHelper.ExecuteScalar(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));
						}
						else
						{
							query = @"UPDATE WD
									  SET WD.DTS_PRINT = NULL
									  FROM CZ_SA_GIRH_PACK_DETAIL WD
									  WHERE WD.CD_COMPANY = '{0}'
									  AND WD.NO_GIR = '{1}'";

							DBHelper.ExecuteScalar(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));
						}
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn출력대상해제.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn출력대상설정_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			FlexGrid gridH;
			string query;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
					gridH = this._flex출고예정일M;
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
					gridH = this._flex포장예정일M;
				else
					gridH = this._flex의뢰번호H;

				dataRowArray = gridH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						if (dr["CD_TYPE"].ToString() == "001")
						{
							query = @"UPDATE WD
									  SET WD.DTS_PRINT = NEOE.SF_SYSDATE(GETDATE())
									  FROM CZ_SA_GIRH_WORK_DETAIL WD
									  WHERE WD.CD_COMPANY = '{0}'
									  AND WD.NO_GIR = '{1}'";

							DBHelper.ExecuteScalar(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));
						}
						else
						{
							query = @"UPDATE WD
									  SET WD.DTS_PRINT = NEOE.SF_SYSDATE(GETDATE())
									  FROM CZ_SA_GIRH_PACK_DETAIL WD
									  WHERE WD.CD_COMPANY = '{0}'
									  AND WD.NO_GIR = '{1}'";

							DBHelper.ExecuteScalar(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));
						}
					}
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn출력대상설정.Text);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn상업송장면허_Click(object sender, EventArgs e)
		{
			ReportHelper reportHelper;
			DataTable 상업송장헤더, 상업송장라인, 협조전라인;
			DataRow[] dataRowArray1, dataRowArray2;
			DataRow tmpRow;
			FlexGrid gridH, gridL;
			string 수주번호, HS코드, 원산지;
			bool 헤더등록여부 = false;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow && 
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
					gridL = this._flex출고예정일L;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
					gridL = this._flex포장예정일L;
				}
				else
				{
					gridH = this._flex의뢰번호H;
					gridL = this._flex의뢰번호L;
				}

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'", "NO_GIR ASC");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
					{
						상업송장헤더 = gridH.DataTable.Clone();
						상업송장라인 = gridL.DataTable.Clone();

						상업송장라인.Columns.Add("TP_ROW");
						상업송장라인.Columns.Add("CD_DM_CI");
						상업송장라인.Columns.Add("CD_BAR_CI");
						상업송장라인.Columns.Add("YN_IMP");

						수주번호 = string.Empty;
						
						string filePath = Path.Combine(Application.StartupPath, "temp");
						string fileName = string.Empty;
						if (!Directory.Exists(filePath))
							Directory.CreateDirectory(filePath);

						foreach (DataRow dr1 in gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"))
						{
							if (dr1["CD_TYPE"].ToString() == "001")
							{
								this.QR코드생성(dr1["CD_COMPANY"].ToString(), D.GetString(dr1["NO_GIR"]));

								협조전라인 = DBHelper.GetDataTable(string.Format(@"SELECT SEQ_GIR,
																			    	      ISNULL(GL.TXT_USERDEF1, 'RCT') AS CD_QR_RCT,
																			  	          ISNULL(GL.TXT_USERDEF2, 'CI') AS CD_QR_CI,
																			  	          ISNULL(GL.TXT_USERDEF3, 'PL') AS CD_QR_PL
																			      FROM SA_GIRL GL WITH(NOLOCK)
																			      WHERE GL.CD_COMPANY = '{0}'
																			      AND GL.NO_GIR = '{1}'", dr1["CD_COMPANY"].ToString(), dr1["NO_GIR"].ToString()));
							}
							else
								협조전라인 = null;

							dataRowArray2 = gridL.DataTable.Select("CD_COMPANY = '" + dr1["CD_COMPANY"].ToString() + "' AND NO_GIR = '" + D.GetString(dr1["NO_GIR"]) + "'");
 
							HS코드 = string.Empty;
							원산지 = string.Empty;
							헤더등록여부 = false;

							foreach (DataRow dr2 in dataRowArray2)
							{
								if (dr1["CD_TYPE"].ToString() == "001")
								{
									tmpRow = 협조전라인.Select(string.Format("SEQ_GIR = '{0}'", dr2["SEQ_GIR"].ToString()))[0];

									dr2["CD_QR_RCT"] = tmpRow["CD_QR_RCT"].ToString();
									dr2["CD_QR_CI"] = tmpRow["CD_QR_CI"].ToString();
									dr2["CD_QR_PL"] = tmpRow["CD_QR_PL"].ToString();
								}
								
								// 자품목 제외
								if (D.GetString(dr2["TP_BOM"]) == "C")
									continue;

								if (dr2["YN_BL"].ToString() != "Y")
									continue;

								if (string.IsNullOrEmpty(dr2["MODEL"].ToString()) ||
									string.IsNullOrEmpty(dr2["UNIT_IMP"].ToString()) ||
									string.IsNullOrEmpty(dr2["HSCODE"].ToString()) ||
									string.IsNullOrEmpty(dr2["ORIGIN"].ToString()))
								{
									this.ShowMessage(string.Format("수입신고필증 자동등록대상이나 등록되지 않은 건이 있습니다. {0}", dr1["NO_GIR"].ToString()));
									return;
								}
								else if (D.GetDecimal(dr2["CUSTOMS"]) <= 0)
								{
									continue;
								}
								else
									헤더등록여부 = true;

								if (수주번호 != D.GetString(dr2["NO_SO"]))
								{
									tmpRow = 상업송장라인.NewRow();

									tmpRow["NO_GIR"] = dr2["NO_GIR"];
									tmpRow["NO_SO"] = dr2["NO_SO"];
									tmpRow["NO_PO_PARTNER"] = dr2["NO_PO_PARTNER"];
									tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr2["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr2["NO_SO"]);

									상업송장라인.Rows.Add(tmpRow);

									수주번호 = D.GetString(dr2["NO_SO"]);
								}

								상업송장라인.ImportRow(dr2);
								상업송장라인.Rows[상업송장라인.Rows.Count - 1]["TP_ROW"] = "I";

								if (!string.IsNullOrEmpty(dr2["MODEL"].ToString()))
									상업송장라인.Rows[상업송장라인.Rows.Count - 1]["NM_ITEM_PARTNER"] = dr2["MODEL"].ToString();

								if (!string.IsNullOrEmpty(dr2["UNIT_IMP"].ToString()))
									상업송장라인.Rows[상업송장라인.Rows.Count - 1]["UNIT"] = dr2["UNIT_IMP"].ToString();

								fileName = filePath + "\\" + dr2["NO_GIR"].ToString() + "_CI_DM.png";

								if (!string.IsNullOrEmpty(dr2["CD_QR_CI"].ToString()) && !File.Exists(fileName))
								{
									string[] qr코드Array = dr2["CD_QR_CI"].ToString().Split('/');
									QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
								}

								상업송장라인.Rows[상업송장라인.Rows.Count - 1]["CD_DM_CI"] = fileName;

								fileName = filePath + "\\" + dr2["NO_GIR"].ToString() + "_CI_BAR.png";

								if (!string.IsNullOrEmpty(dr2["CD_QR_CI"].ToString()) && !File.Exists(fileName))
								{
									string[] qr코드Array = dr2["CD_QR_CI"].ToString().Split('/');
									QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
								}

								상업송장라인.Rows[상업송장라인.Rows.Count - 1]["CD_BAR_CI"] = fileName;

								if (!원산지.Contains(dr2["ORIGIN"].ToString()))
									원산지 += dr2["ORIGIN"].ToString() + ",";

								if (!HS코드.Contains(dr2["HSCODE"].ToString()))
									HS코드 += dr2["HSCODE"].ToString() + ",";
							}

							if (헤더등록여부 == true)
							{
								상업송장헤더.ImportRow(dr1);

								if (!string.IsNullOrEmpty(원산지))
									원산지 = 원산지.Substring(0, 원산지.Length - 1);

								if (!string.IsNullOrEmpty(HS코드))
									HS코드 = HS코드.Substring(0, HS코드.Length - 1);

								상업송장헤더.Rows[상업송장헤더.Rows.Count - 1]["NM_ORIGIN"] = 원산지;
								상업송장헤더.Rows[상업송장헤더.Rows.Count - 1]["NM_HS"] = HS코드;
							}
						}

						if (상업송장헤더.Rows.Count == 0)
						{
							this.ShowMessage("상업송장(면허용) 출력 가능 대상이 없습니다.");
							return;
						}

						reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_5", "납품의뢰현황-상업송장(면허용)", dr["CD_COMPANY"].ToString(), 상업송장헤더, 상업송장라인);

						reportHelper.SetDataTable(상업송장헤더, 1);
						reportHelper.SetDataTable(상업송장라인, 2);

						Util.RPT_Print(reportHelper);
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn적재허가서_Click(object sender, EventArgs e)
		{
			Microsoft.Office.Interop.Excel.Application application = null;
			Microsoft.Office.Interop.Excel.Workbook workBook = null;
			Microsoft.Office.Interop.Excel.Worksheet workSheet;
			Microsoft.Office.Interop.Excel.Range range;
			DataRow[] dataRowArray;
			FlexGrid gridH, gridL;
			string 의뢰번호 = string.Empty;

			try
			{

				if (!this._flex의뢰번호H.HasNormalRow && 
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
					gridL = this._flex출고예정일L;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
					gridL = this._flex포장예정일L;
				}
				else
				{
					gridH = this._flex의뢰번호H;
					gridL = this._flex의뢰번호L;
				}

				if (string.IsNullOrEmpty(this.cbo적재허가서.SelectedValue.ToString()))
				{
					this.ShowMessage("적재허가서 프린터가 선택되어 있지 않습니다.");
					return;
				}

				dataRowArray = gridH.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray, new string[] { "CD_COMPANY" }, true).Rows)
					{
						if (!Directory.Exists("P:\\적재허가서"))
							Directory.CreateDirectory("P:\\적재허가서");

						string localPath = "P:\\적재허가서\\" + Regex.Replace(dataRowArray[0]["NM_VESSEL"].ToString(), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ ]", string.Empty, RegexOptions.Singleline) + ".xls";
						string serverPath = Global.MainFrame.HostURL + "/shared/CZ/" + "P_CZ_SA_GIRSCH_EXPORT.xls";

						localPath = "P:\\적재허가서\\" + FileMgr.GetUniqueFileName(localPath);

						System.Net.WebClient client = new System.Net.WebClient();
						client.DownloadFile(serverPath, localPath);

						try
						{
							application = new Microsoft.Office.Interop.Excel.Application();
							application.Visible = false;
							workBook = application.Workbooks.Open(localPath);
							workSheet = workBook.Worksheets[1];

							int index = 1;
							int index1 = 0;

							foreach (DataRow dr1 in gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"))
							{
								MsgControl.ShowMsg("데이터 저장 중입니다.\n잠시만 기다려 주세요 (@/@)", new string[] { D.GetString(++index1), D.GetString(dataRowArray.Length) });
								의뢰번호 += dr1["NO_GIR"].ToString() + ",";

								foreach (DataRow dr2 in gridL.DataTable.Select(string.Format("NO_GIR = '{0}'", dr1["NO_GIR"].ToString())))
								{
									#region 품목번호
									range = workSheet.Cells[index + 1, 2];
									if (dr2["GRP_ITEM"].ToString() == "GS" || dr2["GRP_ITEM"].ToString() == "BD")
										range.Value = "ZZZZ" + index.ToString("D4");
									else
										range.Value = "GZZZ" + index.ToString("D4");
									#endregion

									#region HS부호
									range = workSheet.Cells[index + 1, 3];
									if (dr2["GRP_ITEM"].ToString() == "GS" || dr2["GRP_ITEM"].ToString() == "BD")
										range.Value = string.Empty;
									else
										range.Value = "8409";
									#endregion

									#region 품목 및 규격1
									range = workSheet.Cells[index + 1, 5];
									range.Value = dr2["NM_ITEM_PARTNER"].ToString();
									#endregion

									#region 품목 및 규격3
									range = workSheet.Cells[index + 1, 7];
									range.Value = "-";
									#endregion

									#region 수량단위
									range = workSheet.Cells[index + 1, 11];
									range.Value = dr2["UNIT"].ToString();
									#endregion

									#region 수량
									range = workSheet.Cells[index + 1, 12];
									range.Value = D.GetInt(dr2["QT_GIR"]).ToString("D");
									#endregion

									#region 금액
									range = workSheet.Cells[index + 1, 13];
									range.Value = D.GetInt(dr2["AM_GIRAMT"]).ToString("N");
									#endregion

									index++;
								}
							}
						}
						catch (Exception ex)
						{
							this.MsgEnd(ex);
						}
						finally
						{
							workBook.Close(true);
							application.Quit();

							ReleaseExcelObject(workBook);
							ReleaseExcelObject(application);

							MsgControl.CloseMsg();
						}

						if (의뢰번호.Right(1) == ",") 의뢰번호 = 의뢰번호.Substring(0, 의뢰번호.Length - 1);

						string qr = 쇼트너.적재허가서(string.Format("{0}/{1}", dr["CD_COMPANY"].ToString(), 의뢰번호));

						Zebra zebra = new Zebra();
						//zebra.ConnectUSBPrinter();
						//zebra.PrintUSBQR(qr);

						//foreach (string 번호 in 의뢰번호.Split(','))
						//{
						//	if (번호.Left(2) == "WO")
						//		zebra.PrintUSBText("DN" + 번호.Substring(2));
						//	else
						//		zebra.PrintUSBText(번호);
						//}

						zebra.Connect(this.cbo적재허가서.SelectedValue.ToString(), 6101);
						zebra.PrintLANQR(qr);

						List<string> 번호List = new List<string>();

						int index2 = 0;
						int total = 의뢰번호.Split(',').Length;

						foreach (string 번호 in 의뢰번호.Split(','))
						{
							if (번호.Left(2) == "WO")
								번호List.Add("DN" + 번호.Substring(2));
							else
								번호List.Add(번호);

							index2++;

							if (번호List.Count == 5 || index2 == total)
							{
								zebra.PrintLANText(번호List.ToArray());
								번호List.Clear();
							}
						}

						zebra.Disconnect();
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn적재허가서.Text);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void ReleaseExcelObject(object obj)
		{
			try
			{
				if (obj != null)
				{
					Marshal.ReleaseComObject(obj);
					obj = null;
				}
			}
			catch (Exception ex)
			{
				obj = null;
				throw ex;
			}
			finally
			{
				GC.Collect();
			}
		}

		private void Btn송품서류등록_Click(object sender, EventArgs e)
		{
			FlexGrid grid;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					grid = this._flex출고예정일M;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					grid = this._flex포장예정일M;
				}
				else
				{
					grid = this._flex의뢰번호H;
				}

				if (string.IsNullOrEmpty(grid["NO_GIR"].ToString()))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, "의뢰번호");
					return;
				}

				if (string.IsNullOrEmpty(grid["DT_GIR"].ToString()))
				{
					this.ShowMessage(공통메세지._은는필수입력항목입니다, "의뢰일자");
					return;
				}

				P_CZ_MA_FILE_SUB dlg = new P_CZ_MA_FILE_SUB(grid["CD_COMPANY"].ToString(), "SA", "P_CZ_SA_GIR", grid["NO_GIR"].ToString(), "P_CZ_SA_GIR" + "/" + grid["CD_COMPANY"].ToString() + "/" + grid["DT_GIR"].ToString().Substring(0, 4));
				dlg.ShowDialog(this);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region ♥ 메인버튼 클릭
		protected override bool BeforeSearch()
		{
			if (!base.BeforeSearch()) return false;

			if (string.IsNullOrEmpty(this.bpc회사.QueryWhereIn_Pipe))
			{
				this.ShowMessage(공통메세지._은는필수입력항목입니다, this.lbl회사.Text);
				return false;
			}

			if (string.IsNullOrEmpty(this.cbo일자유형.SelectedValue.ToString()) &&
				string.IsNullOrEmpty(this.txt의뢰번호.Text))
			{
				this.ShowMessage("일자 없이 조회시 의뢰번호를 반드시 지정 해야 합니다.");
				return false;
			}

			if (!Chk조회일자) return false;

			return true;
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			string 처리상태, 출고구분, 물류업무대, 물류업무중, 물류업무소;

			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (!this.BeforeSearch()) return;

				if (cbo처리상태.SelectedValue != null)
					처리상태 = cbo처리상태.SelectedValue.ToString();
				else
					처리상태 = string.Empty;

				if (cbo출고구분.SelectedValue != null)
					출고구분 = cbo출고구분.SelectedValue.ToString();
				else
					출고구분 = string.Empty;

				if (cbo물류업무대.SelectedValue != null)
					물류업무대 = cbo물류업무대.SelectedValue.ToString();
				else
					물류업무대 = string.Empty;

				if (cbo물류업무중.SelectedValue != null)
					물류업무중 = cbo물류업무중.SelectedValue.ToString();
				else
					물류업무중 = string.Empty;

				if (cbo물류업무소.SelectedValue != null)
					물류업무소 = cbo물류업무소.SelectedValue.ToString();
				else
					물류업무소 = string.Empty;

				DataTable dt = null;
				int 기간;

				기간 = Math.Abs((this.dtp조회일자.StartDate - this.dtp조회일자.EndDate).Days);

				string startDate, endDate;
				DataTable tmpDataTable;

				for (int i = 0; i <= 기간 / 365; i++)
				{
					startDate = this.dtp조회일자.StartDate.AddDays(i * 365).ToString("yyyyMMdd");

					if (기간 >= (i + 1) * 365)
						endDate = this.dtp조회일자.StartDate.AddDays(((i + 1) * 365) - 1).ToString("yyyyMMdd");
					else
						endDate = this.dtp조회일자.EndDateToString;
					
					MsgControl.ShowMsg("조회 중 입니다. 잠시만 기다려 주세요.\n조회기간 (@ ~ @)", new string[] { Util.GetTo_DateStringS(startDate), Util.GetTo_DateStringS(endDate) });

					tmpDataTable = this._biz.SearchHeader(new object[] { Global.MainFrame.ServerKey,
																		 this.bpc회사.QueryWhereIn_Pipe,
																		 Global.MainFrame.LoginInfo.Language,
																		 this.txt의뢰번호.Text,
																		 tabControl.SelectedIndex.ToString(),
																		 this.txt수주번호.Text,
																		 this.확정일자,
																		 처리상태,
																		 출고구분,
																		 this.ctx수주유형.CodeValue,
																		 this.ctx의뢰담당자.CodeValue,
																		 this.ctx수주담당자.CodeValue,
																		 this.ctx매출처.CodeValue,
																		 this.ctx납품처.CodeValue,
																		 (this.dtp확정일자.Checked == true ? string.Empty : this.cbo일자유형.SelectedValue.ToString()),
																		 startDate,
																		 endDate,
																		 물류업무대,
																		 물류업무중,
																		 물류업무소,
																		 this.ctx호선.CodeValue,
																		 (this.chk긴급건.Checked == true ? "Y" : "N"),
																		 (this.chk자동생성건제외.Checked == true ? "Y" : "N"),
																		 (this.chk자동제출.Checked == true ? "Y" : "N"),
																		 this.bpc협조전상태.QueryWhereIn_Pipe,
																		 this.txt매출처발주번호.Text,
																		 this.bpc매출처그룹.QueryWhereIn_Pipe,
																		 (this.chk미제출건.Checked == true ? "Y" : "N"),
																		 (this.chk검토필요.Checked == true ? "Y" : "N"),
																		 (this.chk미출고처리.Checked == true ? "Y" : "N"),
																		 (this.chk실시간출고건.Checked == true ? "Y" : "N") });

					if (i == 0)
						dt = tmpDataTable;
					else
						dt.Merge(tmpDataTable);
				}

				switch ((탭구분)tabControl.SelectedIndex)
				{
					case 탭구분.의뢰번호:
						#region 의뢰번호
						if (this._flex의뢰번호H.DataTable == null)
						{
							this._flex의뢰번호H.DetailGrids = new FlexGrid[] { this._flex의뢰번호L };

							this.SetHeaderGrid(this._flex의뢰번호H);
							this.SetLineGrid(this._flex의뢰번호L);
						}

						dt.DefaultView.Sort = "CD_COMPANY, NO_GIR ASC";
						this._flex의뢰번호H.Binding = dt;
						#endregion
						break;
					case 탭구분.제출일자:
						#region 제출일자
						if (this._flex제출일자H.DataTable == null)
						{
							this._flex제출일자H.DetailGrids = new FlexGrid[] { this._flex제출일자M, this._flex제출일자L };
							this._flex제출일자M.DetailGrids = new FlexGrid[] { this._flex제출일자L };

							#region Header
							this._flex제출일자H.BeginSetting(1, 1, false);

							this._flex제출일자H.SetCol("DT_SUBMIT", "제출일자", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

							this._flex제출일자H.SettingVersion = "1.0.0.1";
							this._flex제출일자H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
							#endregion

							this.SetHeaderGrid(this._flex제출일자M);
							this.SetLineGrid(this._flex제출일자L);
						}
						
						this._flex제출일자H.Binding = dt;
						#endregion
						break;
					case 탭구분.매출처:
						#region 매출처
						if (this._flex매출처H.DataTable == null)
						{
							this._flex매출처H.DetailGrids = new FlexGrid[] { this._flex매출처M, this._flex매출처L };
							this._flex매출처M.DetailGrids = new FlexGrid[] { this._flex매출처L };

							#region Header
							this._flex매출처H.BeginSetting(1, 1, false);

							this._flex매출처H.SetCol("CD_PARTNER", "매출처코드", 100);
							this._flex매출처H.SetCol("LN_PARTNER", "매출처명", 120);
							this._flex매출처H.SetCol("NM_EMP_SALE", "매출처담당자", 100);

							this._flex매출처H.SettingVersion = "1.0.0.2";
							this._flex매출처H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
							#endregion

							this.SetHeaderGrid(this._flex매출처M);
							this.SetLineGrid(this._flex매출처L);
						}
						
						this._flex매출처H.Binding = dt;
						#endregion
						break;
					case 탭구분.업무:
						#region 업무
						if (this._flex업무H.DataTable == null)
						{
							this._flex업무H.DetailGrids = new FlexGrid[] { this._flex업무M, this._flex업무L };
							this._flex업무M.DetailGrids = new FlexGrid[] { this._flex업무L };

							#region Header
							this._flex업무H.BeginSetting(1, 1, false);

							this._flex업무H.SetCol("CD_TYPE", "물류업무코드(대)", false);
							this._flex업무H.SetCol("CD_TYPE2", "물류업무코드(중)", false);
							this._flex업무H.SetCol("CD_TYPE3", "물류업무코드(소)", false);
							this._flex업무H.SetCol("NM_TYPE1", "물류업무(대)", 100, false);
							this._flex업무H.SetCol("NM_TYPE2", "물류업무(중)", 100, false);
							this._flex업무H.SetCol("NM_TYPE3", "물류업무(소)", 100, false);

							this._flex업무H.SettingVersion = "1.0.0.1";
							this._flex업무H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
							#endregion

							this.SetHeaderGrid(this._flex업무M);
							this.SetLineGrid(this._flex업무L);
						}
						
						this._flex업무H.Binding = dt;
						#endregion
						break;
					case 탭구분.출고예정일:
						#region 출고예정일
						if (this._flex출고예정일H.DataTable == null)
						{
							this._flex출고예정일H.DetailGrids = new FlexGrid[] { this._flex출고예정일M, this._flex출고예정일L };
							this._flex출고예정일M.DetailGrids = new FlexGrid[] { this._flex출고예정일L };

							#region Header
							this._flex출고예정일H.BeginSetting(1, 1, false);

							this._flex출고예정일H.SetCol("DT_COMPLETE", "출고예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

							this._flex출고예정일H.SettingVersion = "1.0.0.1";
							this._flex출고예정일H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
							#endregion

							this.SetHeaderGrid(this._flex출고예정일M);
							this.SetLineGrid(this._flex출고예정일L);
						}

						this._flex출고예정일H.Binding = dt;
						#endregion
						break;
					case 탭구분.포장예정일:
						#region 포장예정일
						if (this._flex포장예정일H.DataTable == null)
						{
							this._flex포장예정일H.DetailGrids = new FlexGrid[] { this._flex포장예정일M, this._flex포장예정일L };
							this._flex포장예정일M.DetailGrids = new FlexGrid[] { this._flex포장예정일L };

							#region Header
							this._flex포장예정일H.BeginSetting(1, 1, false);

							this._flex포장예정일H.SetCol("DT_START", "포장예정일", 80, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

							this._flex포장예정일H.SettingVersion = "1.0.0.1";
							this._flex포장예정일H.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
							#endregion

							this.SetHeaderGrid(this._flex포장예정일M);
							this.SetLineGrid(this._flex포장예정일L);
						}

						this._flex포장예정일H.Binding = dt;
						#endregion
						break;
				}

				if (dt == null || dt.Rows.Count == 0)
				{
					ShowMessage(PageResultMode.SearchNoData);// 검색된 내용이 존재하지 않습니다..
					ToolBarPrintButtonEnabled = false;
					return;
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

				switch ((탭구분)this.tabControl.SelectedIndex)
				{
					case 탭구분.의뢰번호:
						flex = this._flex의뢰번호H;
						break;
					case 탭구분.제출일자:
						flex = this._flex제출일자M;
						break;
					case 탭구분.매출처:
						flex = this._flex매출처M;
						break;
					case 탭구분.업무:
						flex = this._flex업무M;
						break;
					case 탭구분.출고예정일:
						flex = this._flex출고예정일M;
						break;
					case 탭구분.포장예정일:
						flex = this._flex포장예정일M;
						break;
				}

				if (flex == null || flex.GetChanges() == null) return false;

				if (this._biz.SaveData(flex.GetChanges()))
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
			FlexGrid gridH, gridL;
			DataTable 협조전헤더, 협조전라인, 컷오프타임;
			DataRow[] dataRowArray1, dataRowArray2;
			string url;

			try
			{
				base.OnToolBarPrintButtonClicked(sender, e);

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
					gridL = this._flex출고예정일L;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
					gridL = this._flex포장예정일L;
				}
				else
				{
					gridH = this._flex의뢰번호H;
					gridL = this._flex의뢰번호L;
				}

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'", "NO_GIR ASC");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
					{
						협조전헤더 = gridH.DataTable.Clone();
						협조전헤더.Columns.Add("CD_GIR_QR");
						협조전헤더.Columns.Add("CD_QR");
						협조전라인 = gridL.DataTable.Clone();
						컷오프타임 = DBHelper.GetDataTable("SP_CZ_SA_GIR_CUTOFF_S", new object[] { dr["CD_COMPANY"].ToString(), string.Empty, "Y" });

						foreach (DataRow dr1 in gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"))
						{
							협조전헤더.ImportRow(dr1);
							협조전헤더.Rows[협조전헤더.Rows.Count - 1]["CD_QR"] = dr1["NM_VESSEL"].ToString() + " / " + dr1["NO_PO_PARTNER"].ToString();

							url = URL.GetShortner("log/pack/write", dr["CD_COMPANY"].ToString() + "/" + dr1["NO_GIR"].ToString());

							협조전헤더.Rows[협조전헤더.Rows.Count - 1]["CD_GIR_QR"] = "V01/D08" + dr1["NO_GIR"].ToString() + "/D10 " + url;

							dataRowArray2 = gridL.DataTable.Select("CD_COMPANY = '" + dr1["CD_COMPANY"].ToString() + "' AND NO_GIR = '" + dr1["NO_GIR"].ToString() + "'");

							foreach (DataRow dr2 in dataRowArray2)
							{
								협조전라인.ImportRow(dr2);
							}

							if (dr1["TP_GIR"].ToString() == "001")
								컷오프타임.Merge(DBHelper.GetDataTable("SP_CZ_SA_GIR_CUTOFF_S", new object[] { dr["CD_COMPANY"].ToString(), dr1["NO_GIR"].ToString(), "Y" }));
						}

						협조전라인.Columns.Remove("NM_SUBJECT");

						reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH", "납품의뢰현황(딘텍)", dr["CD_COMPANY"].ToString(), 협조전헤더, 협조전라인);

						reportHelper.SetDataTable(협조전헤더, 1);
						reportHelper.SetDataTable(협조전라인, 2);
						reportHelper.SetDataTable(컷오프타임, 3);

						Util.RPT_Print(reportHelper);
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		public override bool OnToolBarExitButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!BeforeExit())
					return false;

				this.임시파일제거();
				return base.OnToolBarExitButtonClicked(sender, e);
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return base.OnToolBarExitButtonClicked(sender, e);
		}
		#endregion

		#region ♥ 그리드 이벤트
		private void _flex_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
		{
			FlexGrid flex;

			try
			{
				flex = ((FlexGrid)sender);

				if (e.Row < flex.Rows.Fixed || e.Col < flex.Cols.Fixed) return;
				if (flex.Cols[e.Col].Name != "QT_TAX" && flex.Cols[e.Col].Name != "YN_BL") return;

				CellStyle style = flex.GetCellStyle(e.Row, e.Col);

				if (style == null)
				{
					if (D.GetDecimal(flex.Rows[e.Row]["QT_TAX"]) > 0 || 
						flex.Rows[e.Row]["YN_BL"].ToString() == "Y")
						flex.SetCellStyle(e.Row, e.Col, flex.Styles["납부"]);
					else
						flex.SetCellStyle(e.Row, e.Col, flex.Styles["미납부"]);
				}
				else if (style.Name == "미납부")
				{
					if (D.GetDecimal(flex.Rows[e.Row]["QT_TAX"]) > 0 || 
						flex.Rows[e.Row]["YN_BL"].ToString() == "Y")
						flex.SetCellStyle(e.Row, e.Col, flex.Styles["납부"]);
				}
				else if (style.Name == "납부")
				{
					if (D.GetDecimal(flex.Rows[e.Row]["QT_TAX"]) == 0 &&
						flex.Rows[e.Row]["YN_BL"].ToString() != "Y")
						flex.SetCellStyle(e.Row, e.Col, flex.Styles["미납부"]);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flexH_CheckHeaderClick(object sender, EventArgs e)
		{
			FlexGrid flexH = sender as FlexGrid;

			try
			{
				if (!flexH.HasNormalRow) return;

				flexH.Redraw = false;
				
				//데이타 테이블의 체크값을 변경해주어도 화면에 보이는 값을 변경시키지 못하므로 화면의 값도 같이 변경시켜줌
				flexH.Row = 1; // 맨처음row에 자동위치하도록 해야 틀어지지 않는다.

				for (int h = 0; h < flexH.Rows.Count - 1; h++)
				{
					MsgControl.ShowMsg("자료를 조회 중 입니다.잠시만 기다려 주세요.(@/@)", new string[] { D.GetString(h + 1), D.GetString(flexH.Rows.Count - 1) });

					flexH.Row = h + 1; //Row가 순차적으로 변경되면서 RowChange() 이벤트를 자동 실행하게 유도한다.
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
				flexH.Redraw = true;
			}
		}

		private void _flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			string key, key2, key3, filter, 매출처발주번호, 회사코드;

			try
			{
				FlexGrid flex = sender as FlexGrid;
				DataTable dt = null;
				key = string.Empty;
				key2 = string.Empty;
				key3 = string.Empty;
				filter = string.Empty;

				if (flex.Name == this._flex제출일자H.Name || 
					flex.Name == this._flex매출처H.Name ||
					flex.Name == this._flex업무H.Name ||
					flex.Name == this._flex출고예정일H.Name ||
					flex.Name == this._flex포장예정일H.Name)
				{
					switch ((탭구분)tabControl.SelectedIndex)
					{
						case 탭구분.제출일자:
							key = flex["DT_SUBMIT"].ToString();
							filter = "DT_SUBMIT = '" + key + "'";
							break;
						case 탭구분.매출처:
							key = flex["CD_PARTNER"].ToString();
							filter = "CD_PARTNER = '" + key + "'";
							break;
						case 탭구분.업무:
							key = flex["CD_TYPE"].ToString();
							key2 = flex["CD_TYPE2"].ToString();
							key3 = flex["CD_TYPE3"].ToString();
							filter = "CD_TYPE = '" + key + "' AND CD_MAIN_CATEGORY = '" + key2 + "' AND CD_SUB_CATEGORY = '" + key3 + "'";
							break;
						case 탭구분.출고예정일:
							key = flex["DT_COMPLETE"].ToString();
							filter = "DT_COMPLETE = '" + key + "'";
							break;
						case 탭구분.포장예정일:
							key = flex["DT_START"].ToString();
							filter = "DT_START = '" + key + "'";
							break;
					}

					if (flex.DetailQueryNeed)
						dt = this.GetMiddleData(key, key2, key3);

					flex.DetailGrids[0].BindingAdd(dt, filter);
					flex.DetailQueryNeed = false;
				}
				else if(flex.Name == this._flex의뢰번호H.Name ||
						flex.Name == this._flex제출일자M.Name ||
						flex.Name == this._flex매출처M.Name ||
						flex.Name == this._flex업무M.Name ||
						flex.Name == this._flex출고예정일M.Name ||
						flex.Name == this._flex포장예정일M.Name)
				{
					회사코드 = flex["CD_COMPANY"].ToString();
					key = flex["NO_GIR"].ToString();
					filter = "CD_COMPANY = '" + 회사코드 + "' AND NO_GIR = '" + key + "'";

					if (flex.DetailQueryNeed)
					{
						dt = this.GetLineData(회사코드, key, "Y");

						if (flex.Name == this._flex의뢰번호H.Name || 
							flex.Name == this._flex출고예정일M.Name ||
							flex.Name == this._flex포장예정일M.Name)
						{
							매출처발주번호 = string.Empty;

							foreach (DataRow dr in ComFunc.getGridGroupBy(dt.Select(), new string[] { "NO_PO_PARTNER" }, true).Rows)
							{
								if (!string.IsNullOrEmpty(D.GetString(dr["NO_PO_PARTNER"])))
									매출처발주번호 += D.GetString(dr["NO_PO_PARTNER"]) + ",";
							}

							flex["NO_PO_PARTNER_ALL"] = (string.IsNullOrEmpty(매출처발주번호) == true ? string.Empty : 매출처발주번호.Remove(매출처발주번호.Length - 1));
							flex.AcceptChanges();
						}
					}
					
					flex.DetailGrids[0].BindingAdd(dt, filter);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void _flex_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			FlexGrid grid;
			string pageId, pageName;
			object[] obj;

			try
			{
				grid = (sender as FlexGrid);
				if (grid.HasNormalRow == false) return;
				if (grid.MouseRow < grid.Rows.Fixed) return;

				if (grid.Cols["NO_GIR"] != null && grid.ColSel == grid.Cols["NO_GIR"].Index)
				{
					if (D.GetString(grid["YN_RETURN"]) == "Y")
					{
						pageId = "P_CZ_SA_GIRR_REG";
						pageName = this.DD("출고반품의뢰등록");
						obj = new object[] { pageName, D.GetString(grid["NO_GIR"]) };
					}
					else
					{
						if (D.GetString(grid["CD_TYPE"]) == "001")
						{
							pageId = "P_CZ_SA_GIR";
							pageName = this.DD("물류업무협조전");
							obj = new object[] { pageName, D.GetString(grid["NO_GIR"]), D.GetString(grid["CD_COMPANY"]) };
						}
						else
						{
							pageId = "P_CZ_SA_GIR_PACK";
							pageName = this.DD("포장업무협조전");
							obj = new object[] { pageName, D.GetString(grid["NO_GIR"]), D.GetString(grid["CD_COMPANY"]) };
						}
					}
					
					
					if (Global.MainFrame.IsExistPage(pageId, false))
						Global.MainFrame.UnLoadPage(pageId, false);

					Global.MainFrame.LoadPageFrom(pageId, pageName, this.Grant, obj);
				}
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
				if (flexGrid.Cols[e.Col].Name != "DT_START" &&
					flexGrid.Cols[e.Col].Name != "DT_COMPLETE" &&
					flexGrid.Cols[e.Col].Name != "DT_BILL" &&
					flexGrid.Cols[e.Col].Name != "DC_RMK" &&
					flexGrid.Cols[e.Col].Name != "DC_RMK1" &&
					flexGrid.Cols[e.Col].Name != "DC_RMK2" &&
					flexGrid.Cols[e.Col].Name != "DC_RMK_CI" &&
					flexGrid.Cols[e.Col].Name != "DC_RMK_PL" &&
					flexGrid.Cols[e.Col].Name != "NO_DELIVERY_EMAIL" &&
					flexGrid.Cols[e.Col].Name != "YN_EXCLUDE_CPR" &&
					flexGrid.Cols[e.Col].Name != "DT_IV" &&
					flexGrid.Cols[e.Col].Name != "CD_TAEGBAE") return;

				this.SaveData();

				if (flexGrid.Cols[e.Col].Name == "DT_COMPLETE" &&
					((flexGrid["CD_TYPE"].ToString() == "001" && flexGrid["CD_MAIN_CATEGORY"].ToString() == "001") || //본선선적
					 (flexGrid["CD_TYPE"].ToString() == "002" && flexGrid["CD_MAIN_CATEGORY"].ToString() == "002") || //픽업요청
					 (flexGrid["CD_TYPE"].ToString() == "001" && flexGrid["CD_MAIN_CATEGORY"].ToString() == "003" && flexGrid["CD_SUB_CATEGORY"].ToString() == "007") || //직접송품-선적대행 
					 (flexGrid["CD_TYPE"].ToString() == "001" && flexGrid["CD_MAIN_CATEGORY"].ToString() == "002" && flexGrid["CD_SUB_CATEGORY"].ToString() == "DIR") || //대리점전달-물류부 직접전달
					 (flexGrid["CD_TYPE"].ToString() == "001" && flexGrid["CD_MAIN_CATEGORY"].ToString() == "003" && flexGrid["CD_SUB_CATEGORY"].ToString() == "009"))) //직접송품-하륙대행
				{
					string contents;

					contents = @"*** 협조전 출고예정일 변경 알림

협조전번호 : {0}
출고예정일 : {1}
의뢰구분(소) : {2}
상세요청 : {3}

업무에 참고하시기 바랍니다.

※ 본 쪽지는 발신 전용 입니다.";

					Messenger.SendMSG(new string[] { "S-223" }, string.Format(contents, flexGrid["NO_GIR"].ToString(),
																						Util.GetTo_DateStringS(flexGrid["DT_COMPLETE"].ToString()),
																						flexGrid["NM_TYPE3"].ToString(),
																						flexGrid["DC_RMK"].ToString()));
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex의뢰번호H_CellContentChanged(object sender, CellContentEventArgs e)
		{
			try
			{
				this._biz.SaveContent(e.ContentType, e.CommandType, this._flex의뢰번호H["NO_GIR"].ToString(), e.SettingValue, this._flex의뢰번호H["CD_COMPANY"].ToString(), this._flex의뢰번호H["CD_TYPE"].ToString());
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private DataTable GetMiddleData(string key, string key2, string key3)
		{
			DataTable dt = null;
			string 처리상태, 출고구분, 물류업무대, 물류업무중, 물류업무소;

			try
			{
				if (cbo처리상태.SelectedValue != null)
					처리상태 = cbo처리상태.SelectedValue.ToString();
				else
					처리상태 = string.Empty;

				if (cbo출고구분.SelectedValue != null)
					출고구분 = cbo출고구분.SelectedValue.ToString();
				else
					출고구분 = string.Empty;

				if (cbo물류업무대.SelectedValue != null)
					물류업무대 = cbo물류업무대.SelectedValue.ToString();
				else
					물류업무대 = string.Empty;
				
				if (cbo물류업무중.SelectedValue != null)
					물류업무중 = cbo물류업무중.SelectedValue.ToString();
				else
					물류업무중 = string.Empty;

				if (cbo물류업무소.SelectedValue != null)
					물류업무소 = cbo물류업무소.SelectedValue.ToString();
				else
					물류업무소 = string.Empty;

				dt = this._biz.SearchMiddle(new object[] { this.bpc회사.QueryWhereIn_Pipe,
														   Global.MainFrame.LoginInfo.Language,
														   this.txt의뢰번호.Text,
														   this.txt수주번호.Text,                   
														   key,
														   key2,
														   key3,
														   this.확정일자,
														   처리상태,
														   출고구분,
														   this.ctx수주유형.CodeValue,
														   tabControl.SelectedIndex.ToString(),
														   this.ctx의뢰담당자.CodeValue,
														   this.ctx수주담당자.CodeValue,
														   this.ctx매출처.CodeValue,
														   this.ctx납품처.CodeValue,
														   (this.dtp확정일자.Checked == true ? string.Empty : this.cbo일자유형.SelectedValue.ToString()),
														   this.dtp조회일자.StartDateToString,
														   this.dtp조회일자.EndDateToString,
														   물류업무대,
														   물류업무중,
														   물류업무소,
														   this.ctx호선.CodeValue,
														   (this.chk긴급건.Checked == true ? "Y" : "N"),
														   (this.chk자동생성건제외.Checked == true ? "Y" : "N"),
														   (this.chk자동제출.Checked == true ? "Y" : "N"),
														   this.bpc협조전상태.QueryWhereIn_Pipe,
														   this.txt매출처발주번호.Text,
														   this.bpc매출처그룹.QueryWhereIn_Pipe,
														   (this.chk미제출건.Checked == true ? "Y" : "N"),
														   (this.chk검토필요.Checked == true ? "Y" : "N"),
														   (this.chk미출고처리.Checked == true ? "Y" : "N"),
														   (this.chk실시간출고건.Checked == true ? "Y" : "N"),
														   Global.MainFrame.ServerKey });
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return dt;
		}

		private DataTable GetLineData(string 회사코드, string 협조전번호, string 자품목표시여부)
		{
			DataTable dt = null;
			string 처리상태, 출고구분;

			try
			{
				if (cbo물류업무중.SelectedValue != null)
					처리상태 = cbo처리상태.SelectedValue.ToString();
				else
					처리상태 = string.Empty;

				if (cbo물류업무소.SelectedValue != null)
					출고구분 = cbo출고구분.SelectedValue.ToString();
				else
					출고구분 = string.Empty;

				dt = _biz.SearchLine(new object[] { 회사코드,
													협조전번호,
													자품목표시여부,
													처리상태,
													출고구분,
													ctx수주유형.CodeValue });
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}

			return dt;
		}
		#endregion

		#region ♥ 컨트롤 이벤트
		private void btn협조전삭제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			FlexGrid grid;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
					grid = this._flex출고예정일M;
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
					grid = this._flex포장예정일M;
				else
					grid = this._flex의뢰번호H;

				dataRowArray = grid.DataTable.Select("S = 'Y' AND STA_GIR = 'D'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					foreach (DataRow dr in dataRowArray)
					{
						dr.Delete();
					}

					if (this._biz.협조전삭제(grid.GetChanges()))
					{
						grid.AcceptChanges();
						this.ShowMessage(공통메세지._작업을완료하였습니다, ((Control)sender).Text);
						this.OnToolBarSearchButtonClicked(null, null);
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btn확정_Click(object sender, EventArgs e)
		{
			//ReportHelper reportHelper;
			DataRow[] dataRowArray;
			FlexGrid grid;
			string name, 이전상태;
			int index = 0;

			try
			{
				name = ((Control)sender).Name;

				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
					grid = this._flex출고예정일M;
			    else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
					grid = this._flex포장예정일M;
				else
					grid = this._flex의뢰번호H;

				dataRowArray = grid.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (name == this.btn확정.Name)
				{
					#region 확정

					#region 사용안함
					//string defaultPrint = string.Empty;
					//bool 서류출력여부 = false;

					//if (Global.MainFrame.LoginInfo.CompanyCode == "K100" ||
					//	Global.MainFrame.LoginInfo.CompanyCode == "K200")
					//{
					//	if (Global.MainFrame.ShowMessage("관련 서류 출력 하시겠습니까?", "QY2") == DialogResult.Yes)
					//		서류출력여부 = true;

					//	if (서류출력여부 == true)
					//	{
					//		PrintDocument PrintDocument = new PrintDocument();
					//		defaultPrint = PrintDocument.PrinterSettings.PrinterName;

					//		if (!SetDefaultPrinter("쉬핑마크프린터"))
					//		{
					//			Global.MainFrame.ShowMessage("프린터 설정이 잘못되었습니다.\n프린터명 : 쉬핑마크프린터");
					//			return;
					//		}

					//		if (!SetDefaultPrinter("협조전프린터"))
					//		{
					//			Global.MainFrame.ShowMessage("프린터 설정이 잘못되었습니다.\n프린터명 : 협조전프린터");
					//			return;
					//		}
					//	}
					//}

					//if (서류출력여부 == true)
					//{
					//	string filePath = Path.Combine(Application.StartupPath, "temp") + "\\";
					//	if (Directory.Exists(filePath))
					//	{
					//		string[] files = Directory.GetFiles(filePath);

					//		foreach (string file in files)
					//			File.Delete(file);
					//	}
					//	else
					//		Directory.CreateDirectory(filePath);

					//	List<string> 협조전List, 쉬핑마크List;

					//	협조전List = new List<string>();
					//	쉬핑마크List = new List<string>();

					//	index = 0;

					//	foreach (DataRow dr in dataRowArray)
					//	{
					//		MsgControl.ShowMsg(string.Format("처리중입니다. 잠시만 기다려주세요.\n({0}/{1})", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) }));

					//		if (dr["STA_GIR"].ToString() != "O")
					//			continue;

					//		if (dr["CD_TYPE"].ToString() == "001")
					//		{
					//			#region 물류업무협조전
					//			if (dr["NO_SO_PRE"].ToString().Contains("SB") ||
					//				dr["NO_SO_PRE"].ToString().Contains("NS"))
					//			{
					//				#region 협조전 첫장 출력
					//				reportHelper = this.리포트파일생성(dr);

					//				reportHelper.PrintDirect(string.Format("R_CZ_SA_GIRSCH_{0}_A_WO.DRF", dr["CD_COMPANY"].ToString()), false, true, filePath + dr["NO_GIR"].ToString() + "_WO.pdf", new Dictionary<string, string>());

					//				PDF.ExtractPageRange(filePath + dr["NO_GIR"].ToString() + "_WO.pdf", filePath + dr["NO_GIR"].ToString() + "_WO1.pdf", 1, 1);

					//				협조전List.Add(filePath + dr["NO_GIR"].ToString() + "_WO1.pdf");
					//				#endregion

					//				#region Check List 출력
					//				reportHelper = this.리포트파일생성1(dr);

					//				reportHelper.PrintDirect(string.Format("R_CZ_SA_GIRSCH_4_{0}_CHECK.DRF", dr["CD_COMPANY"].ToString()), false, true, filePath + dr["NO_GIR"].ToString() + "_CL.pdf", new Dictionary<string, string>());

					//				협조전List.Add(filePath + dr["NO_GIR"].ToString() + "_CL.pdf");
					//				#endregion
					//			}
					//			else
					//			{
					//				#region 협조전 2장 출력
					//				reportHelper = this.리포트파일생성(dr);

					//				reportHelper.PrintDirect(string.Format("R_CZ_SA_GIRSCH_{0}_A_WO.DRF", dr["CD_COMPANY"].ToString()), false, true, filePath + dr["NO_GIR"].ToString() + "_WO.pdf", new Dictionary<string, string>());

					//				협조전List.Add(filePath + dr["NO_GIR"].ToString() + "_WO.pdf");
					//				#endregion

					//				#region 쉬핑마크 출력
					//				reportHelper = this.리포트파일생성(dr);

					//				reportHelper.PrintDirect(string.Format("R_CZ_SA_GIRSCH_{0}_C_SHCVA5.DRF", dr["CD_COMPANY"].ToString()), false, true, filePath + dr["NO_GIR"].ToString() + "_SM.pdf", new Dictionary<string, string>());

					//				쉬핑마크List.Add(filePath + dr["NO_GIR"].ToString() + "_SM.pdf");
					//				#endregion
					//			}
					//			#endregion
					//		}
					//		else
					//		{
					//			#region 포장업무협조전
					//			if (dr["CD_MAIN_CATEGORY"].ToString() == "001")
					//			{
					//				#region 협조전 1장 출력
					//				reportHelper = this.리포트파일생성(dr);

					//				reportHelper.PrintDirect(string.Format("R_CZ_SA_GIRSCH_{0}_A_WO.DRF", dr["CD_COMPANY"].ToString()), false, true, filePath + dr["NO_GIR"].ToString() + "_WO.pdf", new Dictionary<string, string>());

					//				협조전List.Add(filePath + dr["NO_GIR"].ToString() + "_WO.pdf");
					//				#endregion

					//				#region 쉬핑마크 출력
					//				reportHelper = this.리포트파일생성(dr);

					//				reportHelper.PrintDirect(string.Format("R_CZ_SA_GIRSCH_{0}_C_SHCVA5.DRF", dr["CD_COMPANY"].ToString()), false, true, filePath + dr["NO_GIR"].ToString() + "_SM.pdf", new Dictionary<string, string>());

					//				쉬핑마크List.Add(filePath + dr["NO_GIR"].ToString() + "_SM.pdf");
					//				#endregion
					//			}
					//			else
					//			{
					//				#region 협조전 1장 출력
					//				reportHelper = this.리포트파일생성(dr);

					//				reportHelper.PrintDirect(string.Format("R_CZ_SA_GIRSCH_{0}_A_WO.DRF", dr["CD_COMPANY"].ToString()), false, true, filePath + dr["NO_GIR"].ToString() + "_WO.pdf", new Dictionary<string, string>());

					//				협조전List.Add(filePath + dr["NO_GIR"].ToString() + "_WO.pdf");
					//				#endregion
					//			}
					//			#endregion
					//		}
					//	}

					//	filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\R_CZ_SA_GIRSCH\\";
					//	if (Directory.Exists(filePath))
					//	{
					//		string[] files = Directory.GetFiles(filePath);

					//		foreach (string file in files)
					//			File.Delete(file);
					//	}
					//	else
					//		Directory.CreateDirectory(filePath);

					//	PDF.Merge(filePath + "R_CZ_SA_GIRSCH_WO.pdf", 협조전List.ToArray());
					//	PDF.Merge(filePath + "R_CZ_SA_GIRSCH_SM.pdf", 쉬핑마크List.ToArray());

					//	SetDefaultPrinter("협조전프린터");
					//	this.printPDFWithAcrobat(filePath + "R_CZ_SA_GIRSCH_WO.pdf");

					//	SetDefaultPrinter("쉬핑마크프린터");
					//	this.printPDFWithAcrobat(filePath + "R_CZ_SA_GIRSCH_SM.pdf");

					//	SetDefaultPrinter(defaultPrint);
					//}
					#endregion

					index = 0;

					foreach (DataRow dr in dataRowArray)
					{
						MsgControl.ShowMsg(string.Format("처리중입니다. 잠시만 기다려주세요.\n({0}/{1})", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) }));

						이전상태 = D.GetString(dr["STA_GIR"]);
						if (이전상태 != "O")
							continue;

						dr["STA_GIR"] = "R";

						DBHelper.ExecuteNonQuery("SP_CZ_SA_GIRSCH_CONFIRM", new object[] { dr["CD_COMPANY"].ToString(),
																						   dr["NO_GIR"].ToString(),
																						   dr["CD_TYPE"].ToString(),
																						   dr["STA_GIR"].ToString(),
																						   "Y",
																						   Global.MainFrame.LoginInfo.UserID });
					}

					grid.AcceptChanges();
					this.ShowMessage(공통메세지._작업을완료하였습니다, ((Control)sender).Text);
					this.OnToolBarSearchButtonClicked(null, null);
					#endregion
				}
				else
				{
					if (name == this.btn포장종결취소.Name && 
						this.ShowMessage("포장 데이터가 있습니다. 포장종결 취소 하시겠습니까?\n종결 취소할 경우, 포장데이터가 삭제되고 복구 할 수 없습니다.", "QY2") != DialogResult.Yes)
						return;

					index = 0;

					foreach (DataRow dr in dataRowArray)
					{
						MsgControl.ShowMsg(string.Format("처리중입니다. 잠시만 기다려주세요.\n({0}/{1})", new string[] { D.GetString(++index), D.GetString(dataRowArray.Length) }));

						이전상태 = D.GetString(dr["STA_GIR"]);

						if (name == this.btn확정취소.Name)
						{
							if (이전상태 == "R")
							{
								if (D.GetString(dr["YN_AUTO_SUBMIT"]) == "Y")
									dr["STA_GIR"] = "O";
								else
									dr["STA_GIR"] = "";
							}
						}
						else if (name == this.btn포장종결.Name)
						{
							if (D.GetString(dr["CD_TYPE"]) == "002" && 이전상태 == "R")
								dr["STA_GIR"] = "C";
						}
						else if (name == this.btn포장종결취소.Name)
						{
							if (D.GetString(dr["CD_TYPE"]) == "002" && 이전상태 == "C")
								dr["STA_GIR"] = "R";
						}
					}

					if (this._biz.상태저장(grid.GetChanges(), "N"))
					{
						grid.AcceptChanges();
						this.ShowMessage(공통메세지._작업을완료하였습니다, ((Control)sender).Text);
						this.OnToolBarSearchButtonClicked(null, null);
					}
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

		public void printPDFWithAcrobat(string path)
		{
			string Filepath = path;

			using (PrintDialog Dialog = new PrintDialog())
			{
				//Dialog.ShowDialog();

				ProcessStartInfo printProcessInfo = new ProcessStartInfo()
				{
					Verb = "print",
					CreateNoWindow = true,
					FileName = Filepath,
					WindowStyle = ProcessWindowStyle.Hidden
				};

				Process printProcess = new Process();
				printProcess.StartInfo = printProcessInfo;
				printProcess.Start();

				printProcess.WaitForInputIdle();

				Thread.Sleep(3000);

				try
				{
					if (false == printProcess.CloseMainWindow())
					{
						printProcess.Kill();
					}
				}
				catch (Exception ex)
				{

				}
			}
		}

		private ReportHelper 리포트파일생성(DataRow dr)
		{
			ReportHelper reportHelper;
			DataTable dtH, dtL;
			DataRow[] dataRowArray;
			string url;

			try
			{
				dtH = this._flex의뢰번호H.DataTable.Clone();
				dtH.Columns.Add("CD_GIR_QR");
				dtH.Columns.Add("CD_QR");
				dtL = this._flex의뢰번호L.DataTable.Clone();

				dtH.ImportRow(dr);
				dtH.Rows[dtH.Rows.Count - 1]["CD_QR"] = dr["NM_VESSEL"].ToString() + " / " + dr["NO_PO_PARTNER"].ToString();

				url = URL.GetShortner("log/pack/write", dr["CD_COMPANY"].ToString() + "/" + dr["NO_GIR"].ToString());

				dtH.Rows[dtH.Rows.Count - 1]["CD_GIR_QR"] = "V01/D08" + dr["NO_GIR"].ToString() + "/D10 " + url;

				dataRowArray = this._flex의뢰번호L.DataTable.Select("CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "' AND NO_GIR = '" + dr["NO_GIR"].ToString() + "'");

				foreach (DataRow dr1 in dataRowArray)
				{
					dtL.ImportRow(dr1);
				}

				dtL.Columns.Remove("NM_SUBJECT");

				reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH", "납품의뢰현황(딘텍)", dr["CD_COMPANY"].ToString(), dtH, dtL);

				reportHelper.SetDataTable(dtH, 1);
				reportHelper.SetDataTable(dtL, 2);

				reportHelper.PrintHelper.UseUserFontStyle();

				return reportHelper;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return null;
		}

		private ReportHelper 리포트파일생성1(DataRow dr)
		{
			ReportHelper reportHelper;
			DataTable dtH, dtL;
			DataRow[] dataRowArray;

			try
			{
				dtH = this._flex의뢰번호H.DataTable.Clone();
				dtL = this._flex의뢰번호L.DataTable.Clone();

				dtL.Columns.Add("LN_PARTNER");

				foreach (DataRow dr1 in this._flex의뢰번호H.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"))
				{
					dtH.ImportRow(dr1);

					dataRowArray = this._flex의뢰번호L.DataTable.Select("CD_COMPANY = '" + dr1["CD_COMPANY"].ToString() + "' AND NO_GIR = '" + dr1["NO_GIR"].ToString() + "'");

					foreach (DataRow dr2 in dataRowArray)
					{
						dtL.ImportRow(dr2);

						dtL.Rows[dtL.Rows.Count - 1]["LN_PARTNER"] = dr1["LN_PARTNER"].ToString();
						dtL.Rows[dtL.Rows.Count - 1]["YN_DSP_RMK"] = "N";
					}
				}

				reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_4", "납품의뢰현황-CheckList", dr["CD_COMPANY"].ToString(), dtH, dtL);

				reportHelper.SetDataTable(dtH, 1);
				reportHelper.SetDataTable(dtL, 2);

				reportHelper.PrintHelper.UseUserFontStyle();

				return reportHelper;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return null;
		}

		private void btn인수증출력_Click(object sender, EventArgs e)
		{
			ReportHelper reportHelper, reportHelper1;
			DataTable 인수증헤더, 인수증헤더1, 인수증라인, 인수증라인1, 협조전라인;
			DataRow[] dataRowArray1, dataRowArray2;
			DataRow tmpRow;
			FlexGrid gridH, gridL;
			string filePath, fileName;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow && 
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
					gridL = this._flex출고예정일L;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
					gridL = this._flex포장예정일L;
				}
				else
				{
					gridH = this._flex의뢰번호H;
					gridL = this._flex의뢰번호L;
				}

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
					{
						인수증헤더 = gridH.DataTable.Clone();
						인수증라인 = gridL.DataTable.Clone();
						인수증라인.Columns.Add("납품처정보");
						인수증라인.Columns.Add("CD_DM_RCT");
						인수증라인.Columns.Add("CD_BAR_RCT");

						foreach (DataRow dr1 in gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"))
						{
							인수증헤더.ImportRow(dr1);

							if (dr1["CD_TYPE"].ToString() == "001")
							{
								this.QR코드생성(dr1["CD_COMPANY"].ToString(), D.GetString(dr1["NO_GIR"]));

								협조전라인 = DBHelper.GetDataTable(string.Format(@"SELECT SEQ_GIR,
																			    	     ISNULL(GL.TXT_USERDEF1, 'RCT') AS CD_QR_RCT,
																			             ISNULL(GL.TXT_USERDEF2, 'CI') AS CD_QR_CI,
																			             ISNULL(GL.TXT_USERDEF3, 'PL') AS CD_QR_PL
																			      FROM SA_GIRL GL WITH(NOLOCK)
																			      WHERE GL.CD_COMPANY = '{0}'
																			      AND GL.NO_GIR = '{1}'", dr1["CD_COMPANY"].ToString(), dr1["NO_GIR"].ToString()));
							}
							else
								협조전라인 = null;
							
							#region 납품처정보
							string 납품처정보 = string.Empty;

							if (dr1["CD_COMPANY"].ToString() == "S100" &&
								(((dr1["CD_PARTNER"].ToString() == "01993" || dr1["CD_PARTNER"].ToString() == "17066" || dr1["CD_PARTNER"].ToString() == "10750" || dr1["CD_PARTNER"].ToString() == "01463") && dr1["CD_DELIVERY_TO"].ToString() == "DVR210600003") ||
								 (dr1["CD_PARTNER"].ToString() == "13660" && dr1["CD_DELIVERY_TO"].ToString() == "DVR220500001")))
							{
								납품처정보 = dr1["NM_CONSIGNEE"].ToString() + Environment.NewLine +
											dr1["ADDR1_CONSIGNEE"].ToString() + Environment.NewLine +
											dr1["ADDR2_CONSIGNEE"].ToString() + Environment.NewLine +
											"TEL : " + dr1["TEL"].ToString() + Environment.NewLine +
											"PIC : " + dr1["PIC"].ToString();
							}
							#endregion

							dataRowArray2 = gridL.DataTable.Select("CD_COMPANY = '" + dr1["CD_COMPANY"].ToString() + "' AND NO_GIR = '" + D.GetString(dr1["NO_GIR"]) + "'");

							foreach (DataRow dr2 in dataRowArray2)
							{
								if (D.GetString(dr2["TP_BOM"]) == "C" || D.GetString(dr2["CD_ITEM"]).Substring(0, 2) == "SD") continue; // 자품목, 비용항목 제외

								인수증라인.ImportRow(dr2);
								
								DataRow tmp인수증라인 = 인수증라인.Rows[인수증라인.Rows.Count - 1];

								tmp인수증라인["납품처정보"] = 납품처정보;

								if (dr1["CD_PARTNER"].ToString() == "11972" &&
									!string.IsNullOrEmpty(dr2["CD_UNIQUE_PARTNER"].ToString()))
								{
									tmp인수증라인["CD_ITEM_PARTNER"] = dr2["CD_ITEM_PARTNER"].ToString() + "(" + dr2["CD_UNIQUE_PARTNER"].ToString() + ")";
								}

								if (dr1["CD_TYPE"].ToString() == "001")
								{
									tmpRow = 협조전라인.Select(string.Format("SEQ_GIR = '{0}'", dr2["SEQ_GIR"].ToString()))[0];

									tmp인수증라인["CD_QR_RCT"] = tmpRow["CD_QR_RCT"].ToString();
									tmp인수증라인["CD_QR_CI"] = tmpRow["CD_QR_CI"].ToString();
									tmp인수증라인["CD_QR_PL"] = tmpRow["CD_QR_PL"].ToString();
								}
									
							}
						}

						인수증헤더1 = 인수증헤더.Clone();
						인수증라인1 = 인수증라인.Clone();

						foreach (DataRow dr1 in 인수증헤더.Rows)
						{
							if (dr1["CD_TYPE"].ToString() != "001" || dr1["YN_RETURN"].ToString() == "Y")
								continue;

							인수증헤더1.Clear();
							인수증라인1.Clear();

							인수증헤더1.ImportRow(dr1);

							foreach (DataRow dr2 in 인수증라인.Select(string.Format("NO_GIR = '{0}'", dr1["NO_GIR"].ToString()), "SEQ_GIR ASC"))
							{
								인수증라인1.ImportRow(dr2);
							}

							#region 임시폴더생성
							filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + D.GetString(dr1["NO_GIR"]) + "\\";

							if (Directory.Exists(filePath))
							{
								string[] files = Directory.GetFiles(filePath);

								foreach (string file in files)
									File.Delete(file);
							}
							else
								Directory.CreateDirectory(filePath);
							#endregion

							#region 인수증장수
							reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_3", "납품의뢰현황-인수증", dr1["CD_COMPANY"].ToString(), 인수증헤더1, 인수증라인1);

							foreach (DataRow dr2 in 인수증라인1.Rows)
							{
								#region QR, DataMetrix, Barcode 생성
								dr2["NO_KEY1"] = (dr2["NO_GIR"].ToString() + "-" + dr2["NO_SO"].ToString());

								fileName = filePath + dr2["NO_KEY1"] + "_RCT_DM.png";

								if (!string.IsNullOrEmpty(dr2["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
								{
									string[] qr코드Array = dr2["CD_QR_RCT"].ToString().Split('/');
									QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
								}

								dr2["CD_DM_RCT"] = fileName;

								fileName = filePath + dr2["NO_KEY1"] + "_RCT_BAR.png";

								if (!string.IsNullOrEmpty(dr2["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
								{
									string[] qr코드Array = dr2["CD_QR_RCT"].ToString().Split('/');
									QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
								}

								dr2["CD_BAR_RCT"] = fileName;
								#endregion
							}

							reportHelper.SetDataTable(인수증헤더1, 1);
							reportHelper.SetDataTable(인수증라인1, 2);
							reportHelper.PrintHelper.UseUserFontStyle();

							if (dr1["CD_COMPANY"].ToString() == "K100")
								reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_K100_A_RCT.DRF", false, true, filePath + dr1["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());
							else if (dr1["CD_COMPANY"].ToString() == "K200")
								reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_K200_A_RCT.DRF", false, true, filePath + dr1["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());
							else
								reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_S100_A_RCT.DRF", false, true, filePath + dr1["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());

							int 인수증장수 = PDF.GetPageCount(filePath + dr1["NO_GIR"].ToString() + "_RCT.pdf");

							DBHelper.ExecuteScalar(string.Format(@"UPDATE SA_GIRH
                                                                   SET CD_USERDEF2 = '{2}'
                                                                   WHERE CD_COMPANY = '{0}'
                                                                   AND NO_GIR = '{1}'", dr1["CD_COMPANY"].ToString(), dr1["NO_GIR"].ToString(), 인수증장수));
							#endregion
						}

						reportHelper1 = Util.SetRPT("R_CZ_SA_GIRSCH_3", "납품의뢰현황-인수증", dr["CD_COMPANY"].ToString(), 인수증헤더, 인수증라인);

						foreach (DataRow dr1 in 인수증라인.Rows)
						{
							filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + dr1["NO_GIR"].ToString() + "\\";

							if (!Directory.Exists(filePath))
								Directory.CreateDirectory(filePath);
								
							#region QR, DataMetrix, Barcode 생성
							dr1["NO_KEY1"] = (dr1["NO_GIR"].ToString() + "-" + dr1["NO_SO"].ToString());

							fileName = filePath + dr1["NO_KEY1"] + "_RCT_DM.png";

							if (!string.IsNullOrEmpty(dr1["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
							{
								string[] qr코드Array = dr1["CD_QR_RCT"].ToString().Split('/');
								QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
							}

							dr1["CD_DM_RCT"] = fileName;

							fileName = filePath + dr1["NO_KEY1"] + "_RCT_BAR.png";

							if (!string.IsNullOrEmpty(dr1["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
							{
								string[] qr코드Array = dr1["CD_QR_RCT"].ToString().Split('/');
								QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
							}

							dr1["CD_BAR_RCT"] = fileName;
							#endregion
						}

						reportHelper1.SetDataTable(인수증헤더, 1);
						reportHelper1.SetDataTable(인수증라인, 2);

						Util.RPT_Print(reportHelper1);
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		
		private void btn포장명세서출력_Click(object sender, EventArgs e)
		{
			ReportHelper reportHelper;
			DataTable 포장명세서헤더, 포장명세서라인;
			FlexGrid gridH;
			DataRow[] dataRowArray1;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow && 
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
				}
				else
				{
					gridH = this._flex의뢰번호H;
				}

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
					{
						포장명세서헤더 = new DataTable();
						포장명세서라인 = new DataTable();

						this.포장명세서데이터(dr["CD_COMPANY"].ToString(), gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"), ref 포장명세서헤더, ref 포장명세서라인);

						if (포장명세서헤더.Rows.Count != 0)
						{
							reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_1", "납품의뢰현황-포장명세서", dr["CD_COMPANY"].ToString(), 포장명세서헤더, 포장명세서라인);
							reportHelper.SetDataTable(포장명세서헤더, 1);
							reportHelper.SetDataTable(포장명세서라인, 2);

							Util.RPT_Print(reportHelper);
						}
						else
						{
							this.ShowMessage("@ 가 존재하지 않음", this.DD("포장정보"));
						}
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btn상업송장출력_Click(object sender, EventArgs e)
		{
			ReportHelper reportHelper;
			DataTable 상업송장헤더, 상업송장라인, 인수증라인, 포장명세서헤더, 포장명세서라인, 협조전라인;
			DataRow[] dataRowArray1, dataRowArray2;
			DataRow tmpRow;
			FlexGrid gridH, gridL;
			string 수주번호;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow && 
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
					gridL = this._flex출고예정일L;
				}
			    else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
					gridL = this._flex포장예정일L;
				}
				else
				{
					gridH = this._flex의뢰번호H;
					gridL = this._flex의뢰번호L;
				}

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'", "NO_GIR ASC");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
					{
						상업송장헤더 = gridH.DataTable.Clone();
						상업송장라인 = gridL.DataTable.Clone();
						인수증라인 = gridL.DataTable.Clone();
						포장명세서헤더 = new DataTable();
						포장명세서라인 = new DataTable();

						상업송장라인.Columns.Add("TP_ROW");
						상업송장라인.Columns.Add("CD_DM_CI");
						상업송장라인.Columns.Add("CD_BAR_CI");

						인수증라인.Columns.Add("납품처정보");
						인수증라인.Columns.Add("CD_DM_RCT");
						인수증라인.Columns.Add("CD_BAR_RCT");

						수주번호 = string.Empty;

						string filePath = Path.Combine(Application.StartupPath, "temp");
						string fileName = string.Empty;
						if (!Directory.Exists(filePath))
							Directory.CreateDirectory(filePath);

						foreach (DataRow dr1 in gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"))
						{
							상업송장헤더.ImportRow(dr1);

							if (dr1["CD_TYPE"].ToString() == "001")
							{
								this.QR코드생성(dr1["CD_COMPANY"].ToString(), D.GetString(dr1["NO_GIR"]));

								협조전라인 = DBHelper.GetDataTable(string.Format(@"SELECT SEQ_GIR,
																			    	      ISNULL(GL.TXT_USERDEF1, 'RCT') AS CD_QR_RCT,
																			              ISNULL(GL.TXT_USERDEF2, 'CI') AS CD_QR_CI,
																			              ISNULL(GL.TXT_USERDEF3, 'PL') AS CD_QR_PL
																			      FROM SA_GIRL GL WITH(NOLOCK)
																			      WHERE GL.CD_COMPANY = '{0}'
																			      AND GL.NO_GIR = '{1}'", dr1["CD_COMPANY"].ToString(), dr1["NO_GIR"].ToString()));
							}
							else
								협조전라인 = null;

							#region 납품처정보
							string 납품처정보 = string.Empty;

							if (dr1["CD_COMPANY"].ToString() == "S100" &&
								(((dr1["CD_PARTNER"].ToString() == "01993" || dr1["CD_PARTNER"].ToString() == "17066" || dr1["CD_PARTNER"].ToString() == "10750" || dr1["CD_PARTNER"].ToString() == "01463") && dr1["CD_DELIVERY_TO"].ToString() == "DVR210600003") ||
								 (dr1["CD_PARTNER"].ToString() == "13660" && dr1["CD_DELIVERY_TO"].ToString() == "DVR220500001")))
							{
								납품처정보 = dr1["NM_CONSIGNEE"].ToString() + Environment.NewLine +
											dr1["ADDR1_CONSIGNEE"].ToString() + Environment.NewLine +
											dr1["ADDR2_CONSIGNEE"].ToString() + Environment.NewLine +
											"TEL : " + dr1["TEL"].ToString() + Environment.NewLine +
											"PIC : " + dr1["PIC"].ToString();
							}
							#endregion

							dataRowArray2 = gridL.DataTable.Select("CD_COMPANY = '" + dr1["CD_COMPANY"].ToString() + "' AND NO_GIR = '" + D.GetString(dr1["NO_GIR"]) + "'");

							foreach (DataRow dr2 in dataRowArray2)
							{
								if (dr1["CD_TYPE"].ToString() == "001")
								{
									tmpRow = 협조전라인.Select(string.Format("SEQ_GIR = '{0}'", dr2["SEQ_GIR"].ToString()))[0];

									dr2["CD_QR_RCT"] = tmpRow["CD_QR_RCT"].ToString();
									dr2["CD_QR_CI"] = tmpRow["CD_QR_CI"].ToString();
									dr2["CD_QR_PL"] = tmpRow["CD_QR_PL"].ToString();
								}
								
								// 자품목 제외
								if (D.GetString(dr2["TP_BOM"]) != "C")
								{
									if (수주번호 != D.GetString(dr2["NO_SO"]))
									{
										tmpRow = 상업송장라인.NewRow();

										tmpRow["NO_GIR"] = dr2["NO_GIR"];
										tmpRow["NO_SO"] = dr2["NO_SO"];
										tmpRow["NO_PO_PARTNER"] = dr2["NO_PO_PARTNER"];
										tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr2["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr2["NO_SO"]);

										상업송장라인.Rows.Add(tmpRow);

										수주번호 = D.GetString(dr2["NO_SO"]);
									}

									상업송장라인.ImportRow(dr2);
									상업송장라인.Rows[상업송장라인.Rows.Count - 1]["TP_ROW"] = "I";
									
									fileName = filePath + "\\" + dr2["NO_GIR"].ToString() + "_CI_DM.png";

									if (!string.IsNullOrEmpty(dr2["CD_QR_CI"].ToString()) && !File.Exists(fileName))
									{
										string[] qr코드Array = dr2["CD_QR_CI"].ToString().Split('/');
										QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
									}

									상업송장라인.Rows[상업송장라인.Rows.Count - 1]["CD_DM_CI"] = fileName;

									fileName = filePath + "\\" + dr2["NO_GIR"].ToString() + "_CI_BAR.png";

									if (!string.IsNullOrEmpty(dr2["CD_QR_CI"].ToString()) && !File.Exists(fileName))
									{
										string[] qr코드Array = dr2["CD_QR_CI"].ToString().Split('/');
										QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
									}

									상업송장라인.Rows[상업송장라인.Rows.Count - 1]["CD_BAR_CI"] = fileName;

									if (D.GetString(dr2["CD_ITEM"]).Substring(0, 2) != "SD")
									{
										인수증라인.ImportRow(dr2);
										인수증라인.Rows[인수증라인.Rows.Count - 1]["납품처정보"] = 납품처정보;

										if (dr1["CD_PARTNER"].ToString() == "11972" &&
										!string.IsNullOrEmpty(dr2["CD_UNIQUE_PARTNER"].ToString()))
										{
											인수증라인.Rows[인수증라인.Rows.Count - 1]["CD_ITEM_PARTNER"] = dr2["CD_ITEM_PARTNER"].ToString() + "(" + dr2["CD_UNIQUE_PARTNER"].ToString() + ")";
										}

										fileName = filePath + "\\" + dr2["NO_GIR"].ToString() + "-" + dr2["NO_SO"].ToString() + "_RCT_DM.png";

										if (!string.IsNullOrEmpty(dr2["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
										{
											string[] qr코드Array = dr2["CD_QR_RCT"].ToString().Split('/');
											QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
										}
										
										인수증라인.Rows[인수증라인.Rows.Count - 1]["CD_DM_RCT"] = fileName;

										fileName = filePath + "\\" + dr2["NO_GIR"].ToString() + "-" + dr2["NO_SO"].ToString() + "_RCT_BAR.png";

										if (!string.IsNullOrEmpty(dr2["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
										{
											string[] qr코드Array = dr2["CD_QR_RCT"].ToString().Split('/');
											QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
										}
										
										인수증라인.Rows[인수증라인.Rows.Count - 1]["CD_BAR_RCT"] = fileName;
									}
								}
							}
						}

						this.포장명세서데이터(dr["CD_COMPANY"].ToString(), gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"), ref 포장명세서헤더, ref 포장명세서라인);

						Util.SetRPT_DataTable(인수증라인, this.열넓이("R_CZ_SA_GIRSCH_3"));
						Util.SetRPT_DataTable(포장명세서라인, this.열넓이("R_CZ_SA_GIRSCH_1"));

						reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_2", "납품의뢰현황-상업송장", dr["CD_COMPANY"].ToString(), 상업송장헤더, 상업송장라인);

						reportHelper.SetDataTable(상업송장헤더, 1);
						reportHelper.SetDataTable(상업송장라인, 2);
						reportHelper.SetDataTable(인수증라인, 3);
						reportHelper.SetDataTable(포장명세서헤더, 4);
						reportHelper.SetDataTable(포장명세서라인, 5);

						Util.RPT_Print(reportHelper);
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void btnCheckList출력_Click(object sender, EventArgs e)
		{
			ReportHelper reportHelper;
			DataTable CheckList헤더, CheckList라인;
			FlexGrid gridH, gridL;
			DataRow[] dataRowArray1, dataRowArray2;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow &&
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
					gridL = this._flex출고예정일L;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
					gridL = this._flex포장예정일L;
				}
				else
				{
					gridH = this._flex의뢰번호H;
					gridL = this._flex의뢰번호L;
				}

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray1, new string[] { "CD_COMPANY" }, true).Rows)
					{
						CheckList헤더 = gridH.DataTable.Clone();
						CheckList라인 = gridL.DataTable.Clone();

						CheckList라인.Columns.Add("LN_PARTNER");

						foreach (DataRow dr1 in gridH.DataTable.Select("S = 'Y' AND CD_COMPANY = '" + dr["CD_COMPANY"].ToString() + "'"))
						{
							CheckList헤더.ImportRow(dr1);

							dataRowArray2 = gridL.DataTable.Select("CD_COMPANY = '" + dr1["CD_COMPANY"].ToString() + "' AND NO_GIR = '" + dr1["NO_GIR"].ToString() + "'");

							foreach (DataRow dr2 in dataRowArray2)
							{
								CheckList라인.ImportRow(dr2);

								CheckList라인.Rows[CheckList라인.Rows.Count - 1]["LN_PARTNER"] = dr1["LN_PARTNER"].ToString();
								CheckList라인.Rows[CheckList라인.Rows.Count - 1]["YN_DSP_RMK"] = "N";
							}
						}

						reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_4", "납품의뢰현황-CheckList", dr["CD_COMPANY"].ToString(), CheckList헤더, CheckList라인);

						reportHelper.SetDataTable(CheckList헤더, 1);
						reportHelper.SetDataTable(CheckList라인, 2);

						Util.RPT_Print(reportHelper);
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void BtnDHL발송정보_Click(object sender, EventArgs e)
		{
			try
			{
				P_CZ_SA_GIR_DHL dialog = new P_CZ_SA_GIR_DHL(this._flex의뢰번호H["CD_COMPANY"].ToString(), this._flex의뢰번호H["NO_GIR"].ToString());
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void BtnCPR출력_Click(object sender, EventArgs e)
		{
			FlexGrid gridH;
			DataRow[] dataRowArray1;
			
			try
			{
				if (!this._flex의뢰번호H.HasNormalRow && 
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
				}
				else
				{
					gridH = this._flex의뢰번호H;
				}

				if (Global.MainFrame.ShowMessage("Commercial,Packing,Receipt 일괄 출력 하시겠습니까?", "QY2") != DialogResult.Yes) return;

				dataRowArray1 = gridH.DataTable.Select("S = 'Y'", "CD_COMPANY ASC, NO_GIR ASC");
				if (dataRowArray1 == null || dataRowArray1.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else
				{
					List<string> fileList = new List<string>();
					List<string> 면허용List = new List<string>();

					string filePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\R_CZ_SA_GIRSCH_2_CI3\\";

					if (Directory.Exists(filePath))
					{
						string[] files = Directory.GetFiles(filePath);

						foreach (string file in files)
							File.Delete(file);
					}
					else
						Directory.CreateDirectory(filePath);

					int index = 0;
					foreach (DataRow dr1 in dataRowArray1)
					{
						MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요. (@/@)", new string[] { D.GetString(++index), D.GetString(dataRowArray1.Length) });

						if (!this.CPR출력(dr1, filePath, ref fileList, ref 면허용List, true))
							return;
					}

					if (fileList.Count > 0)
						PDF.Merge(filePath + "R_CZ_SA_GIRSCH_2_K100_CI3.pdf", fileList.ToArray());
					
					if (면허용List.Count > 0)
						PDF.Merge(filePath + "R_CZ_SA_GIRSCH_2_K100_CI3_면허용.pdf", 면허용List.ToArray());
				}

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btnCPR출력.Text);
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

		private void BtnCPR자동발송_Click(object sender, EventArgs e)
		{
			DataTable dt;
			int index;

			try
			{
				if (Global.MainFrame.ShowMessage("Commercial,Packing,Receipt 일괄 발송 하시겠습니까?", "QY2") != DialogResult.Yes) return;

				while(true)
				{
					dt = DBHelper.GetDataTable("SP_CZ_SA_GIRSCH_AUTO_CPR_S", new object[] { "K100" });

					index = 0;
					foreach (DataRow dr in dt.Rows)
					{
						MsgControl.ShowMsg(string.Format("처리중입니다. 잠시만 기다려주세요.\n{0} ({1}/{2})", new string[] { dr["NO_GIR"].ToString(), D.GetString(++index), D.GetString(dt.Rows.Count) }));

						if ((dr["CD_PARTNER"].ToString() == "01187" || dr["CD_PARTNER"].ToString() == "02143" || dr["CD_PARTNER"].ToString() == "03448") && dr["CD_MAIN_CATEGORY"].ToString() == "001")
							this.CPR발송_선적(dr);
						else
							this.CPR발송(dr);
					}

					dt = DBHelper.GetDataTable("SP_CZ_SA_GIRSCH_AUTO_CPR_S", new object[] { "K200" });

					index = 0;
					foreach (DataRow dr in dt.Rows)
					{
						MsgControl.ShowMsg(string.Format("처리중입니다. 잠시만 기다려주세요.\n{0} ({1}/{2})", new string[] { dr["NO_GIR"].ToString(), D.GetString(++index), D.GetString(dt.Rows.Count) }));

						this.CPR발송(dr);
					}

					this.임시파일제거();

					this.Delay(30 * 60000);
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

		private void Delay(int MS) 
		{ 
			DateTime ThisMoment = DateTime.Now;
			TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
			DateTime AfterWards = ThisMoment.Add(duration);
			
			while (AfterWards >= ThisMoment) 
			{
				MsgControl.ShowMsg("자동발송 대기 중입니다.\n남은시간 : " + (AfterWards - ThisMoment).ToString());

				System.Windows.Forms.Application.DoEvents(); 
				ThisMoment = DateTime.Now; 
			}
		}

		private void Btn부대비용_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex의뢰번호H.HasNormalRow) return;
				if (this._flex의뢰번호H["NM_TYPE1"].ToString() != "출하")
				{
					this.ShowMessage("출고 건만 부대비용 등록 가능 합니다.");
					return;
				}
				if (string.IsNullOrEmpty(this._flex의뢰번호H["NM_TYPE2"].ToString()) ||
					string.IsNullOrEmpty(this._flex의뢰번호H["NM_TYPE3"].ToString()))
				{
					this.ShowMessage("자동생성건은 부대비용 등록이 불가 합니다.");
					return;
				}

				P_CZ_SA_GIR_CHARGE dialog = new P_CZ_SA_GIR_CHARGE(this._flex의뢰번호H["CD_COMPANY"].ToString(), this._flex의뢰번호H["NO_GIR"].ToString());
				dialog.ShowDialog();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void BtnOneBill발송_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			DataTable dt1, dt2, dt3;
			FlexGrid gridH, gridL;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow && 
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
					gridL = this._flex출고예정일L;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
					gridL = this._flex포장예정일L;
				}
				else
				{
					gridH = this._flex의뢰번호H;
					gridL = this._flex의뢰번호L;
				}

				dataRowArray = gridH.DataTable.Select("S = 'Y'", "NO_GIR ASC");
				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else if (gridH.DataTable.Select("S = 'Y' AND (CD_COMPANY NOT IN ('K100', 'K200') OR TP_GIR <> '001' OR CD_MAIN_CATEGORY <> '003' OR CD_SUB_CATEGORY <> '003' OR CD_DELIVERY_TO NOT IN ('01107', 'DLV230200274'))").Length > 0)
				{
					this.ShowMessage("DHL 발송 대상이 아닌 건이 선택 되어 있습니다.");
				}
				else
				{
					dt1 = new DataTable();
					dt2 = new DataTable();
					dt3 = new DataTable();

					foreach (DataRow dr in dataRowArray)
					{
						DataSet ds = this._biz.DHL(new object[] { dr["CD_COMPANY"].ToString(),
																  dr["NO_GIR"].ToString() });

						dt1.Merge(ds.Tables[0]);
						dt2.Merge(ds.Tables[1]);
						dt3.Merge(ds.Tables[2]);
					}

					string result = DHL_xml.DHLShipmentValidationService(dt1, dt2, dt3);

					if (result != "SUCCESS")
					{
						this.ShowMessage(result);
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn출고취소신청_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex의뢰번호H.HasNormalRow) return;

				if (string.IsNullOrEmpty(this._flex의뢰번호H["NO_IO"].ToString()))
				{
					this.ShowMessage("출고처리 된 건만 신청 가능 합니다.");
					return;
				}

				if (this._flex의뢰번호H["YN_RETURN"].ToString() == "Y")
				{
					this.ShowMessage("반품건은 신청 불가 합니다.");
					return;
				}

				if (this._gw.전자결재(this._flex의뢰번호H.GetDataRow(this._flex의뢰번호H.Row)))
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

		private void Btn출고품목삭제_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow) return;
				if (this._flex의뢰번호H["NO_GIR"].ToString().Left(2) != "WO")
				{
					this.ShowMessage("물류업무협조전 품목만 삭제 가능 합니다.");
					return;
				}

				if (this._flex의뢰번호H["STA_GIR"].ToString() != "C")
				{
					this.ShowMessage("종결상태의 품목만 삭제 가능 합니다.");
					return;
				}

				dataRowArray = this._flex의뢰번호L.DataTable.Select(string.Format("NO_GIR = '{0}' AND S = 'Y'", this._flex의뢰번호H["NO_GIR"].ToString()));

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else if (this._flex의뢰번호L.DataTable.Select(string.Format("NO_GIR = '{0}' AND S = 'N'", this._flex의뢰번호H["NO_GIR"].ToString())).Length == 0)
				{
					this.ShowMessage("협조전의 모든 품목은 삭제할 수 없습니다.");
					return;
				}
				else
				{
					#region 검증
					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format("EXEC SP_CZ_SA_GIRSCH_L_D_CHECK '{0}', '{1}', '{2}'", dr["CD_COMPANY"].ToString(),
																												   dr["NO_GIR"].ToString(),
																												   dr["SEQ_GIR"].ToString()));
					}
					#endregion

					if (Global.MainFrame.ShowMessage("출고 품목 삭제를 진행 하시겠습니까?\n(해당 품목의 포장데이터도 삭제 됨)\n삭제된 데이터는 복구 할 수 없습니다.", "QY2") != DialogResult.Yes)
						return;

					DBHelper.ExecuteScalar(string.Format("EXEC SP_CZ_SA_GIR_LOG '{0}', '{1}', '{2}', '{3}'", this._flex의뢰번호H["CD_COMPANY"].ToString(),
																											 this._flex의뢰번호H["CD_TYPE"].ToString(),
																											 this._flex의뢰번호H["NO_GIR"].ToString(),
																											 Global.MainFrame.LoginInfo.UserID));

					#region 삭제
					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format("EXEC SP_CZ_SA_GIRSCH_L_D '{0}', '{1}', '{2}'", dr["CD_COMPANY"].ToString(),
																											 dr["NO_GIR"].ToString(),
																											 dr["SEQ_GIR"].ToString()));
					}
					#endregion
				}

				this.OnToolBarSearchButtonClicked(null, null);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void btnDHL발송_Click(object sender, EventArgs e)
		{
			FlexGrid gridH, gridL;

			try
			{
				if (!this._flex의뢰번호H.HasNormalRow && 
					!this._flex출고예정일M.HasNormalRow &&
					!this._flex포장예정일M.HasNormalRow) return;

				if ((탭구분)tabControl.SelectedIndex == 탭구분.출고예정일)
				{
					gridH = this._flex출고예정일M;
					gridL = this._flex출고예정일L;
				}
				else if ((탭구분)tabControl.SelectedIndex == 탭구분.포장예정일)
				{
					gridH = this._flex포장예정일M;
					gridL = this._flex포장예정일L;
				}
				else
				{
					gridH = this._flex의뢰번호H;
					gridL = this._flex의뢰번호L;
				}

				DataSet ds = this._biz.DHL(new object[] { gridH["CD_COMPANY"].ToString(),
														  gridL["NO_GIR"].ToString() });

				string result = DHL_xml.DHLShipmentValidationService(ds.Tables[0], ds.Tables[1], ds.Tables[2]);

				if (result != "SUCCESS")
				{
					this.ShowMessage(gridH["NO_GIR"].ToString() + " : " + result);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}
		#endregion

		#region ♥ 기타 이벤트
		private void Control_SelectionChangeCommitted(object sender, EventArgs e)
		{
			try
			{
				switch (((Control)sender).Name)
				{
					case "cbo물류업무대":
						switch (this.cbo물류업무대.SelectedValue.ToString())
						{
							case "":
								this.cbo물류업무중.DataSource = null;
								break;
							case "001":
								this.cbo물류업무중.DataSource = this.GetComboDataCombine("S;CZ_SA00006");
								break;
							case "002":
								this.cbo물류업무중.DataSource = this.GetComboDataCombine("S;CZ_SA00020");
								break;
						}

						this.cbo물류업무중.DisplayMember = "NAME";
						this.cbo물류업무중.ValueMember = "CODE";

						this.cbo물류업무소.DataSource = null;
						this.cbo물류업무소.DisplayMember = "NAME";
						this.cbo물류업무소.ValueMember = "CODE";
						break;
					case "cbo물류업무중":
						switch (this.cbo물류업무대.SelectedValue.ToString())
						{
							case "":
								this.cbo물류업무소.DataSource = null;
								break;
							case "001":
								if (string.IsNullOrEmpty(cbo물류업무중.SelectedValue.ToString()))
									this.cbo물류업무소.DataSource = null;
								else
									this.cbo물류업무소.DataSource = this.GetComboDataCombine("S;" + MA.GetCode("CZ_SA00006").Select("CODE = '" + cbo물류업무중.SelectedValue.ToString() + "'")[0].ItemArray[3].ToString());
								break;
							case "002":
								if (string.IsNullOrEmpty(cbo물류업무중.SelectedValue.ToString()))
									this.cbo물류업무소.DataSource = null;
								else
									this.cbo물류업무소.DataSource = this.GetComboDataCombine("S;" + MA.GetCode("CZ_SA00020").Select("CODE = '" + cbo물류업무중.SelectedValue.ToString() + "'")[0].ItemArray[3].ToString());
								break;
						}

						this.cbo물류업무소.DisplayMember = "NAME";
						this.cbo물류업무소.ValueMember = "CODE";
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Control_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				switch (e.HelpID)
				{
					case Duzon.Common.Forms.Help.HelpID.P_SA_TPSO_SUB:
						e.HelpParam.P61_CODE1 = "N";
						e.HelpParam.P62_CODE2 = "Y";
						break;
					case Duzon.Common.Forms.Help.HelpID.P_MA_CODE_SUB1:
						if (e.ControlName == this.bpc매출처그룹.Name)
							e.HelpParam.P41_CD_FIELD1 = "MA_B000065";
						else if (e.ControlName == this.bpc협조전상태.Name)
							e.HelpParam.P41_CD_FIELD1 = "CZ_SA00030";
						break;
					case Duzon.Common.Forms.Help.HelpID.P_MA_TABLE_SUB:
						e.HelpParam.P00_CHILD_MODE = "납품처";

						e.HelpParam.P61_CODE1 = @"MD.CD_PARTNER AS CODE,
												  MD.LN_PARTNER AS NAME";
						e.HelpParam.P62_CODE2 = @"CZ_MA_DELIVERY MD WITH(NOLOCK)";
						e.HelpParam.P63_CODE3 = string.Format(@"WHERE MD.CD_COMPANY = '{0}'
																AND MD.YN_USE = 'Y'", Global.MainFrame.LoginInfo.CompanyCode);
						break;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Control_QueryAfter(object sender, BpQueryArgs e)
		{
			if (e.DialogResult == DialogResult.Cancel)
				return;

			if (e.ControlName == this.ctx호선.Name)
			{
				txt호선명.Text = e.HelpReturn.Rows[0]["NM_VESSEL"].ToString();
			}
		}

		private void ctx호선_CodeChanged(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(ctx호선.Text))
				{
					this.txt호선명.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}
		#endregion

		#region ♥ 기타 메소드
		private void 포장명세서데이터(string 회사코드, DataRow[] dataRowArray, ref DataTable dtH, ref DataTable dtL)
		{
			DataTable tmpDt;
			DataRow tmpRow;
			string 협조전번호, 수주번호;

			try
			{
				협조전번호 = string.Empty;

				foreach (DataRow dr in ComFunc.getGridGroupBy(dataRowArray, new string[] { "NO_GIR" }, true).Rows)
				{
					this.QR코드생성(회사코드, D.GetString(dr["NO_GIR"]));

					협조전번호 += "|" + D.GetString(dr["NO_GIR"]);
				}
				
				DataSet ds = this._biz.포장데이터(new object[] { Global.MainFrame.ServerKey,
																 회사코드,
																 협조전번호 });

				tmpDt = ds.Tables[1].Clone();
				tmpDt.Columns.Add("TP_ROW");

				수주번호 = string.Empty;

				foreach (DataRow dr in ds.Tables[1].Rows)
				{
					if (수주번호 != D.GetString(dr["NO_FILE"]))
					{
						tmpRow = tmpDt.NewRow();

						tmpRow["NO_GIR"] = dr["NO_GIR"];
						tmpRow["NO_KEY"] = dr["NO_KEY"];
						tmpRow["NO_FILE"] = dr["NO_FILE"];
						tmpRow["NO_PO_PARTNER"] = dr["NO_PO_PARTNER"];
						tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr["NO_FILE"]);

						tmpDt.Rows.Add(tmpRow);

						수주번호 = D.GetString(dr["NO_FILE"]);
					}

					tmpDt.ImportRow(dr);
					tmpDt.Rows[tmpDt.Rows.Count - 1]["TP_ROW"] = "I";
				}

				dtH = ds.Tables[0];
				dtH.Columns.Add("CD_QR");
				dtH.Columns.Add("CD_DM_PL");
				dtH.Columns.Add("CD_BAR_PL");

				string filePath = Path.Combine(Application.StartupPath, "temp");
				string fileName = string.Empty;
				if (!Directory.Exists(filePath))
					Directory.CreateDirectory(filePath);

				foreach (DataRow dr in dtH.Rows)
				{
					dr["CD_QR"] = dr["NM_VESSEL"].ToString() + " / " + dr["NO_PO_PARTNER"].ToString();

					fileName = filePath + "\\" + dr["NO_GIR"].ToString() + "_PL_DM.png";

					if (!string.IsNullOrEmpty(dr["CD_QR_PL"].ToString()) && !File.Exists(fileName))
					{
						string[] qr코드Array = dr["CD_QR_PL"].ToString().Split('/');
						QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
					}
						

					dr["CD_DM_PL"] = fileName;

					fileName = filePath + "\\" + dr["NO_GIR"].ToString() + "_PL_BAR.png";

					if (!string.IsNullOrEmpty(dr["CD_QR_PL"].ToString()) && !File.Exists(fileName))
					{
						string[] qr코드Array = dr["CD_QR_PL"].ToString().Split('/');
						QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
					}
					
					dr["CD_BAR_PL"] = fileName;
				}

				dtL = tmpDt;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private int 열넓이(string 리포트코드)
		{
			DataTable dt;
			int width = 0;
			string query;

			try
			{
				query = @"SELECT TOP 1 *
						  FROM MA_REPORTL WITH(NOLOCK)
						  WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + @"'
						  AND CD_SYSTEM = '" + 리포트코드 + @"'
						  AND ISNULL(CD_FLAG, '') != ''";

				dt = DBHelper.GetDataTable(query);

				if (dt.Rows.Count == 1)
					width = D.GetInt(dt.Rows[0]["CD_FLAG"]);
				else
					width = 220;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			return width;
		}

		private bool CPR출력(DataRow dr, string filePath, ref List<string> fileList, ref List<string> 면허용List, bool isShowMsg)
		{
			ReportHelper reportHelper;
			DataTable dtH, dtL, dt면허용, dt면허용2;
			string fileName, query;

			try
			{
				this.QR코드생성(dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString());

				DataSet ds = DBHelper.GetDataSet("SP_CZ_SA_PACK_MNG_GIR_S", new object[] { Global.MainFrame.ServerKey,
																						   dr["CD_COMPANY"].ToString(),
																						   Global.MainFrame.LoginInfo.Language,
																						   dr["NO_GIR"].ToString() });

				#region 상업송장
				dtH = ds.Tables[0];
				dtL = ds.Tables[1].Clone();

				dtL.Columns.Add("TP_ROW");
				dtL.Columns.Add("CD_DM_CI");
				dtL.Columns.Add("CD_BAR_CI");

				dt면허용 = dtL.Copy();
				dt면허용2 = dtL.Copy();

				string 수주번호 = string.Empty;
				string filePath1 = Path.Combine(Application.StartupPath, "temp");
				if (!Directory.Exists(filePath1))
					Directory.CreateDirectory(filePath1);

				foreach (DataRow dr1 in ds.Tables[1].Rows)
				{
					// 자품목 제외
					if (D.GetString(dr1["TP_BOM"]) == "C")
						continue;

					#region 상업송장
					this.라인추가(dtL, dr1, 수주번호, filePath1);
					#endregion

					#region 면허용
					if (isShowMsg == true)
					{
						if (dr1["YN_BL"].ToString() == "Y")
						{
							if (string.IsNullOrEmpty(dr1["MODEL"].ToString()) ||
								string.IsNullOrEmpty(dr1["UNIT_IMP"].ToString()) ||
								string.IsNullOrEmpty(dr1["HSCODE"].ToString()) ||
								string.IsNullOrEmpty(dr1["ORIGIN"].ToString()) ||
								string.IsNullOrEmpty(dr1["NO_IMPORT"].ToString()))
							{
								if (isShowMsg) this.ShowMessage(string.Format("수입신고필증 자동등록대상이나 등록되지 않은 건이 있습니다. {0}", D.GetString(dr["NO_GIR"])));
								return false;
							}

							if (D.GetDecimal(dr1["CUSTOMS"]) > 0)
								this.라인추가(dt면허용, dr1, 수주번호, filePath1);
							else
								this.라인추가(dt면허용2, dr1, 수주번호, filePath1);
						}
						else
							this.라인추가(dt면허용2, dr1, 수주번호, filePath1);
					}
					#endregion
				}

				if (dtL.Rows.Count > 0)
				{
					reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_2", "납품의뢰현황-상업송장", dr["CD_COMPANY"].ToString(), dtH, dtL);

					reportHelper.SetDataTable(dtH, 1);
					reportHelper.SetDataTable(dtL, 2);
					reportHelper.PrintHelper.UseUserFontStyle();

					if (dr["CD_COMPANY"].ToString() == "K100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_K100_CI.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_CI.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "K200")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_K200_CI.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_CI.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "S100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_S100_CI.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_CI.pdf", new Dictionary<string, string>());
				}

				if (dt면허용2.Rows.Count > 0)
				{
					reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_2", "납품의뢰현황-상업송장", dr["CD_COMPANY"].ToString(), dtH, dt면허용2);

					reportHelper.SetDataTable(dtH, 1);
					reportHelper.SetDataTable(dt면허용2, 2);
					reportHelper.PrintHelper.UseUserFontStyle();

					if (dr["CD_COMPANY"].ToString() == "K100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_K100_CI.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_CI2.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "K200")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_K200_CI.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_CI2.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "S100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_S100_CI.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_CI2.pdf", new Dictionary<string, string>());

					if (File.Exists(filePath + dr["NO_GIR"].ToString() + "_CI2.pdf"))
						면허용List.Add(filePath + dr["NO_GIR"].ToString() + "_CI2.pdf");
				}
				#endregion

				#region 상업송장 (면허용)
				if (dt면허용.Rows.Count > 0)
				{
					List<string> 수입면장번호리스트 = new List<string>();
					string 원산지 = string.Empty, HS코드 = string.Empty;

					foreach (DataRow dr1 in dt면허용.Rows)
					{
						if (dr1["TP_ROW"].ToString() != "I")
							continue;

						if (!string.IsNullOrEmpty(dr1["MODEL"].ToString()))
							dr1["NM_ITEM_PARTNER"] = dr1["MODEL"].ToString();

						if (!string.IsNullOrEmpty(dr1["UNIT_IMP"].ToString()))
							dr1["UNIT"] = dr1["UNIT_IMP"].ToString();

						if (!원산지.Contains(dr1["ORIGIN"].ToString()))
							원산지 += dr1["ORIGIN"].ToString() + ",";

						if (!HS코드.Contains(dr1["HSCODE"].ToString()))
							HS코드 += dr1["HSCODE"].ToString() + ",";

						if (!수입면장번호리스트.Contains(dr1["NO_IMPORT"].ToString()))
							수입면장번호리스트.Add(dr1["NO_IMPORT"].ToString());
					}

					if (!string.IsNullOrEmpty(원산지))
						원산지 = 원산지.Substring(0, 원산지.Length - 1);

					if (!string.IsNullOrEmpty(HS코드))
						HS코드 = HS코드.Substring(0, HS코드.Length - 1);

					dtH.Rows[dtH.Rows.Count - 1]["NM_ORIGIN"] = 원산지;
					dtH.Rows[dtH.Rows.Count - 1]["NM_HS"] = HS코드;

					reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_5", "납품의뢰현황-상업송장(면허용)", dr["CD_COMPANY"].ToString(), dtH, dt면허용);

					reportHelper.SetDataTable(dtH, 1);
					reportHelper.SetDataTable(dt면허용, 2);
					reportHelper.PrintHelper.UseUserFontStyle();

					if (dr["CD_COMPANY"].ToString() == "K100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_5_K100_CI_IMP.DRF", false, true, filePath + D.GetString(dr["NO_GIR"]) + "_IMP.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "K200")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_5_K200_CI_IMP.DRF", false, true, filePath + D.GetString(dr["NO_GIR"]) + "_IMP.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "S100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_5_S100_CI_IMP.DRF", false, true, filePath + D.GetString(dr["NO_GIR"]) + "_IMP.pdf", new Dictionary<string, string>());

					if (File.Exists(filePath + dr["NO_GIR"].ToString() + "_IMP.pdf"))
						면허용List.Add(filePath + dr["NO_GIR"].ToString() + "_IMP.pdf");

					foreach (string 수입면장번호 in 수입면장번호리스트)
					{
						query = @"SELECT TOP 1 MF.FILE_NAME,
	   '/Upload/P_CZ_FI_IMP_PMT_MNG/' AS FILE_PATH,
	   MF.CD_FILE 
FROM CZ_FI_IMP_PMTH IH WITH(NOLOCK)
JOIN MA_FILEINFO MF WITH(NOLOCK) ON MF.CD_COMPANY = IH.CD_COMPANY AND MF.CD_MODULE = 'FI' AND MF.ID_MENU = 'P_CZ_FI_IMP_PMT_MNG' AND MF.CD_FILE = IH.NO_IMPORT + '_' + IH.CD_COMPANY AND MF.FILE_NAME LIKE 'IMP_%'
WHERE IH.CD_COMPANY = '{0}'
AND IH.NO_IMPORT = '{1}'";

						DataTable dt = DBHelper.GetDataTable(string.Format(query, dr["CD_COMPANY"].ToString(), 수입면장번호));

						if (dt == null || dt.Rows.Count == 0)
						{
							if (isShowMsg) this.ShowMessage("수입면장 파일이 없습니다.");
							return false;
						}

						foreach (DataRow dr1 in dt.Rows)
						{
							FileUploader.DownloadFile(dr1["FILE_NAME"].ToString(), filePath, dr1["FILE_PATH"].ToString(), dr1["CD_FILE"].ToString());
							면허용List.Add(filePath + dr1["FILE_NAME"].ToString());
						}
					}
				}
				#endregion

				#region 포장명세서
				dtH = new DataTable();
				dtL = new DataTable();

				this.포장명세서데이터(dr["CD_COMPANY"].ToString(), new DataRow[] { dr }, ref dtH, ref dtL);

				if (dtH.Rows.Count != 0)
				{
					reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_1", "납품의뢰현황-포장명세서", dr["CD_COMPANY"].ToString(), dtH, dtL);
					reportHelper.SetDataTable(dtH, 1);
					reportHelper.SetDataTable(dtL, 2);
					reportHelper.PrintHelper.UseUserFontStyle();

					if (dr["CD_COMPANY"].ToString() == "K100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_K100_PL.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_PL.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "K200")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_K200_PL.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_PL.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "S100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_S100_PL.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_PL.pdf", new Dictionary<string, string>());
				}
				else
				{
					if (isShowMsg) this.ShowMessage("포장명세서 데이터가 없습니다.");
					return false;
				}
				#endregion

				fileList = new List<string>();

				if (dr["CD_MAIN_CATEGORY"].ToString() == "003" &&
					(dr["CD_SUB_CATEGORY"].ToString() == "001" || dr["CD_SUB_CATEGORY"].ToString() == "002"))
				{
					if (File.Exists(filePath + dr["NO_GIR"].ToString() + "_CI.pdf"))
						fileList.Add(filePath + dr["NO_GIR"].ToString() + "_CI.pdf");
					else
					{
						if (isShowMsg) this.ShowMessage("상업송장 파일이 없습니다.");
						return false;
					}

					if (File.Exists(filePath + dr["NO_GIR"].ToString() + "_PL.pdf"))
						fileList.Add(filePath + dr["NO_GIR"].ToString() + "_PL.pdf");
					else
					{
						if (isShowMsg) this.ShowMessage("포장명세서 파일이 없습니다.");
						return false;
					}
				}
				else
				{
					#region 인수증
					string 납품처정보 = string.Empty;

					if (dr["CD_COMPANY"].ToString() == "S100" &&
						(((dr["CD_PARTNER"].ToString() == "01993" || dr["CD_PARTNER"].ToString() == "17066" || dr["CD_PARTNER"].ToString() == "10750" || dr["CD_PARTNER"].ToString() == "01463") && dr["CD_DELIVERY_TO"].ToString() == "DVR210600003") ||
						 (dr["CD_PARTNER"].ToString() == "13660" && dr["CD_DELIVERY_TO"].ToString() == "DVR220500001")))
					{
						납품처정보 = dr["NM_CONSIGNEE"].ToString() + Environment.NewLine +
									dr["ADDR1_CONSIGNEE"].ToString() + Environment.NewLine +
									dr["ADDR2_CONSIGNEE"].ToString() + Environment.NewLine +
									"TEL : " + dr["TEL"].ToString() + Environment.NewLine +
									"PIC : " + dr["PIC"].ToString();
					}

					dtH = ds.Tables[0];
					dtL = new DataView(ds.Tables[1], "TP_BOM <> 'C' AND SUBSTRING(CD_ITEM, 1, 2) <> 'SD'", "", DataViewRowState.CurrentRows).ToTable();

					dtL.Columns.Add("납품처정보");
					dtL.Columns.Add("CD_DM_RCT");
					dtL.Columns.Add("CD_BAR_RCT");
					
					reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_3", "납품의뢰현황-인수증", dr["CD_COMPANY"].ToString(), dtH, dtL);

					foreach (DataRow dr1 in dtL.Rows)
					{
						dr1["납품처정보"] = 납품처정보;

						dr1["NO_KEY1"] = (dr1["NO_GIR"].ToString() + "-" + dr1["NO_SO"].ToString());

						fileName = filePath1 + "\\" + dr1["NO_GIR"].ToString() + "-" + dr1["NO_SO"].ToString() + "_RCT_DM.png";

						if (!string.IsNullOrEmpty(dr1["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
						{
							string[] qr코드Array = dr1["CD_QR_RCT"].ToString().Split('/');
							QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
						}

						dr1["CD_DM_RCT"] = fileName;

						fileName = filePath1 + "\\" + dr1["NO_GIR"].ToString() + "-" + dr1["NO_SO"].ToString() + "_RCT_BAR.png";

						if (!string.IsNullOrEmpty(dr1["CD_QR_RCT"].ToString()) && !File.Exists(fileName))
						{
							string[] qr코드Array = dr1["CD_QR_RCT"].ToString().Split('/');
							QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
						}

						dr1["CD_BAR_RCT"] = fileName;
					}

					reportHelper.SetDataTable(dtH, 1);
					reportHelper.SetDataTable(dtL, 2);
					reportHelper.PrintHelper.UseUserFontStyle();

					if (dr["CD_COMPANY"].ToString() == "K100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_K100_A_RCT.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "K200")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_K200_A_RCT.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());
					else if (dr["CD_COMPANY"].ToString() == "S100")
						reportHelper.PrintDirect("R_CZ_SA_GIRSCH_3_S100_A_RCT.DRF", false, true, filePath + dr["NO_GIR"].ToString() + "_RCT.pdf", new Dictionary<string, string>());
					#endregion

					if (File.Exists(filePath + dr["NO_GIR"].ToString() + "_CI.pdf"))
						fileList.Add(filePath + dr["NO_GIR"].ToString() + "_CI.pdf");
					else
					{
						if (isShowMsg) this.ShowMessage("상업송장 파일이 없습니다.");
						return false;
					}

					if (File.Exists(filePath + dr["NO_GIR"].ToString() + "_PL.pdf"))
						fileList.Add(filePath + dr["NO_GIR"].ToString() + "_PL.pdf");
					else
					{
						if (isShowMsg) this.ShowMessage("포장명세서 파일이 없습니다.");
						return false;
					}

					if (File.Exists(filePath + dr["NO_GIR"].ToString() + "_RCT.pdf"))
					{
						int 인수증장수 = PDF.GetPageCount(filePath + dr["NO_GIR"].ToString() + "_RCT.pdf");

						DBHelper.ExecuteScalar(string.Format(@"UPDATE SA_GIRH
                                                               SET CD_USERDEF2 = '{2}'
                                                               WHERE CD_COMPANY = '{0}'
                                                               AND NO_GIR = '{1}'", dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString(), 인수증장수));

						fileList.Add(filePath + dr["NO_GIR"].ToString() + "_RCT.pdf");
					}
					else
					{
						if (isShowMsg) this.ShowMessage("인수증 파일이 없습니다.");
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}

			return true;
		}

		private void 라인추가(DataTable dt, DataRow dr, string 수주번호, string filePath)
		{
			DataRow tmpRow;
			string fileName;

			try
			{
				if (수주번호 != D.GetString(dr["NO_SO"]))
				{
					tmpRow = dt.NewRow();

					tmpRow["NO_GIR"] = dr["NO_GIR"];
					tmpRow["NO_SO"] = dr["NO_SO"];
					tmpRow["NO_PO_PARTNER"] = dr["NO_PO_PARTNER"];
					tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr["NO_SO"]);

					dt.Rows.Add(tmpRow);

					수주번호 = D.GetString(dr["NO_SO"]);
				}

				dt.ImportRow(dr);
				dt.Rows[dt.Rows.Count - 1]["TP_ROW"] = "I";

				fileName = filePath + "\\" + dr["NO_GIR"].ToString() + "_CI_DM.png";

				if (!string.IsNullOrEmpty(dr["CD_QR_CI"].ToString()) && !File.Exists(fileName))
				{
					string[] qr코드Array = dr["CD_QR_CI"].ToString().Split('/');
					QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
				}

				dt.Rows[dt.Rows.Count - 1]["CD_DM_CI"] = fileName;

				fileName = filePath + "\\" + dr["NO_GIR"].ToString() + "_CI_BAR.png";

				if (!string.IsNullOrEmpty(dr["CD_QR_CI"].ToString()) && !File.Exists(fileName))
				{
					string[] qr코드Array = dr["CD_QR_CI"].ToString().Split('/');
					QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
				}

				dt.Rows[dt.Rows.Count - 1]["CD_BAR_CI"] = fileName;
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}


		private bool CPR발송(DataRow dr)
		{
			List<string> fileList, 면허용List;
			string filePath, query, 보내는사람, 받는사람, 참조, 숨은참조, 제목, html;

			try
			{
				if (dr["CD_COMPANY"].ToString() == "K100")
					보내는사람 = "receipt@dintec.co.kr|" + dr["NM_COMPANY"].ToString();
				else if (dr["CD_COMPANY"].ToString() == "K200")
					보내는사람 = "receipt@dubheco.com|" + dr["NM_COMPANY"].ToString();
				else
					return false;

				filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + dr["NO_GIR"].ToString() + "\\";
				if (Directory.Exists(filePath))
				{
					string[] files = Directory.GetFiles(filePath);

					foreach (string file in files)
						File.Delete(file);
				}
				else
					Directory.CreateDirectory(filePath);

				fileList = new List<string>();
				면허용List = new List<string>();

				if (!this.CPR출력(dr, filePath, ref fileList, ref 면허용List, false))
					return false;

				string 파일명 = (dr["NM_VESSEL"].ToString() == string.Empty ? string.Empty : Regex.Replace(dr["NM_VESSEL"].ToString().Replace(' ', '_'), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ_]", string.Empty, RegexOptions.Singleline)) + " " +
								(dr["NO_PO_PARTNER"].ToString() == string.Empty ? string.Empty : Regex.Replace(dr["NO_PO_PARTNER"].ToString().Split(',')[0].Replace(' ', '_'), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ_]", string.Empty, RegexOptions.Singleline));

				PDF.Merge(filePath + 파일명 + ".pdf", fileList.ToArray());

				Thread.Sleep(5000); //5초

				받는사람 = dr["NO_EMAIL_DELIVERY"].ToString();

				if (dr["CD_COMPANY"].ToString() == "K100")
					참조 = "receipt@dintec.co.kr";
				else if (dr["CD_COMPANY"].ToString() == "K200")
					참조 = "receipt@dubheco.com";
				else
					return false;

				숨은참조 = dr["NO_EMAIL"].ToString();

				제목 = dr["NM_COMPANY"].ToString() + " " + 
					  (dr["NO_PO_PARTNER"].ToString() == string.Empty ? string.Empty : dr["NO_PO_PARTNER"].ToString().Split(',')[0]) +
					  " (" + dr["NO_IO"].ToString() + ") " +
					  dr["NM_VESSEL"].ToString() + 
					  " 송품서류 전달 (" + dr["NM_SUB_CATEGORY"].ToString() + ")";

				html = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
수신 : {0}<br/>
발신 : {1}<br/><br/>
- 송품정보<br/>
	의뢰번호 : {2}<br/>
	호선명 : {3}<br/>
	사이즈 & 무게 : {4}<br/>
	ORDER NO. : {5}<br/>
	선사 : {6}<br/>
	협조내용 : {7}<br/><br/>
- 담당자 정보<br/>
	담당자 : {8}<br/>
	연락처 : {9}<br/>
	메일주소 : {10}<br/><br/>
- 출고예정일 : {11}<br/><br/>
<span style='font-size: 10pt; font-family: 맑은 고딕; color: #FF0000; font-weight: bold;'>
참고사항 : {12}
</span><br/><br/>
※ 문의사항은 담당자 메일주소로 회신 주시고, 인수증 및 서류는 반드시 receipt 메일로 회신 해주시기 바랍니다.<br/><br/>
<span style='font-size: 10pt; font-family: 맑은 고딕; color: #FF0000; font-weight: bold;'>
* 당사 물류센터 이전 안내<br/><br/>
변경 전 : 양산시 동면 남양산길<br/>
변경 후 : 경남 양산시 어곡공단2길 48 (2층) [어곡동 871-12, 2층]<br/><br/>
</span>
감사합니다.
</div>";
				html = string.Format(html, dr["NM_DELIVERY"].ToString(),
										   dr["NM_COMPANY"].ToString(),
										   dr["NO_GIR"].ToString(),
										   dr["NM_VESSEL"].ToString(),
										   dr["DC_RESULT"].ToString(),
										   dr["NO_PO_PARTNER"].ToString(),
										   dr["LN_PARTNER"].ToString(),
										   dr["NM_RMK"].ToString(),
										   dr["NM_KOR"].ToString(),
										   dr["NO_TEL"].ToString(),
										   dr["NO_EMAIL"].ToString(),
										   dr["DT_COMPLETE"].ToString(),
										   dr["DC_RMK_CPR"].ToString());

				fileList = new List<string>();

				fileList.Add(filePath + 파일명 + ".pdf");

				#region 협조전 첨부파일
				query = @"SELECT MF.FILE_NAME,
								 '/Upload/P_CZ_SA_GIR/' + GH.CD_COMPANY + '/' + LEFT(GH.DT_GIR, 4) AS FILE_PATH,
								 GH.NO_GIR AS CD_FILE
						  FROM SA_GIRH GH WITH(NOLOCK)
						  JOIN MA_FILEINFO MF WITH(NOLOCK) ON MF.CD_COMPANY = GH.CD_COMPANY AND MF.CD_MODULE = 'SA' AND MF.ID_MENU = 'P_CZ_SA_GIR' AND MF.CD_FILE = GH.NO_GIR
						  WHERE GH.CD_COMPANY = '{0}'
						  AND GH.NO_GIR = '{1}'";

				DataTable dt = DBHelper.GetDataTable(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));

				foreach (DataRow dr1 in dt.Rows)
				{
					FileUploader.DownloadFile(dr1["FILE_NAME"].ToString(), filePath, dr1["FILE_PATH"].ToString(), dr1["CD_FILE"].ToString());
					fileList.Add(filePath + dr1["FILE_NAME"].ToString());
				}
				#endregion

				#region 공장품목 첨부파일 (MSDS)
				WebClient wc = new WebClient();
				string 서버경로, 파일이름;

				query = @"SELECT A.CD_COMPANY,
       A.CD_PLANT,
       A.CD_ITEM,
       MI.CLS_L,
       MI.CLS_ITEM,
       A.TP_IMAGE,
       A.IMAGE
FROM SA_GIRL GL WITH(NOLOCK)
JOIN (SELECT * 
      FROM CZ_MA_PITEM_FILE WITH(NOLOCK)
      UNPIVOT (IMAGE FOR TP_IMAGE IN (IMAGE1, IMAGE2, IMAGE3, IMAGE4, IMAGE5, IMAGE6, IMAGE7)) PV) A
ON A.CD_COMPANY = GL.CD_COMPANY AND A.CD_ITEM = GL.CD_ITEM AND A.IMAGE LIKE '%MSDS%'
LEFT JOIN MA_PITEM MI WITH(NOLOCK) ON MI.CD_COMPANY = A.CD_COMPANY AND MI.CD_PLANT = A.CD_PLANT AND MI.CD_ITEM = A.CD_ITEM
WHERE GL.CD_COMPANY = '{0}'
AND GL.NO_GIR = '{1}'
GROUP BY A.CD_COMPANY, A.CD_PLANT, A.CD_ITEM, MI.CLS_L, MI.CLS_ITEM, A.TP_IMAGE, A.IMAGE";

				dt = DBHelper.GetDataTable(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));

				foreach (DataRow dr1 in dt.Rows)
				{
					if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
					{
						string prefix2 = dr1["CD_ITEM"].ToString().Left(2);
						string prefix3 = dr1["CD_ITEM"].ToString().Left(3);

						if (dr1["CLS_L"].ToString() == "EQ" && dr1["CLS_ITEM"].ToString() == "009" && dr1["CD_ITEM"].ToString().Length == 9)
							서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + "K100" + "/" + D.GetString(dr1["CD_ITEM"]) + "/";
						else if ((prefix2 == "BF" || prefix2 == "BR" || prefix2 == "BX" || prefix2 == "SA") && prefix3 != "SAM" && prefix3 != "SAR")
							서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + "K100" + "/" + D.GetString(dr1["CD_ITEM"]) + "/";
						else
							서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(dr1["CD_ITEM"]) + "/";
					}
					else
						서버경로 = Global.MainFrame.HostURL + "/Upload/P_CZ_MA_PITEM/" + Global.MainFrame.LoginInfo.CompanyCode + "/" + D.GetString(dr1["CD_ITEM"]) + "/";

					파일이름 = FileMgr.GetUniqueFileName(filePath + dr1["IMAGE"].ToString());

					wc.DownloadFile(서버경로 + dr1["IMAGE"].ToString(), filePath + 파일이름);
					fileList.Add(filePath + 파일이름);
				}
				#endregion

				#region 첨부파일등록현황 첨부파일 (VALARIS)
				if (dr["CD_PARTNER"].ToString() == "08420")
				{
					query = @"SELECT DISTINCT GL.NO_SO 
FROM SA_GIRL GL WITH(NOLOCK)
WHERE GL.CD_COMPANY = '{0}'
AND GL.NO_GIR = '{1}'";

					dt = DBHelper.GetDataTable(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));

					foreach (DataRow dr1 in dt.Rows)
					{
						query = @"SELECT TOP 1 NM_FILE_REAL 
FROM CZ_MA_WORKFLOWL WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND NO_KEY = '{1}'
AND TP_STEP = '08'
AND NM_FILE LIKE 'PO%'
ORDER BY DTS_INSERT DESC";

						DataTable dt1 = DBHelper.GetDataTable(string.Format(query, dr["CD_COMPANY"].ToString(), dr1["NO_SO"].ToString()));

						foreach (DataRow dr2 in dt1.Rows)
						{
							string fileName = FileMgr.Download_WF(dr["CD_COMPANY"].ToString(), dr1["NO_SO"].ToString(), dr2["NM_FILE_REAL"].ToString(), (filePath + dr2["NM_FILE_REAL"].ToString()), false);
							fileList.Add(filePath + fileName);
						}
					}
				}
				#endregion

				if (면허용List.Count > 0)
				{
					PDF.Merge(filePath + 파일명 + "_면허용.pdf", 면허용List.ToArray());
					fileList.Add(filePath + 파일명 + "_면허용.pdf");
				}

				if (!this.메일발송(보내는사람, 받는사람, 참조, 숨은참조, 제목, string.Empty, html, fileList))
				{
					//this.ShowMessage("메일발송실패");
					return false;
				}

				query = @"UPDATE CZ_SA_GIRH_WORK_DETAIL
						  SET YN_CPR = 'Y',
							  DTS_CPR = NEOE.SF_SYSDATE(GETDATE())
						  WHERE CD_COMPANY = '{0}'
						  AND NO_GIR = '{1}'";

				DBHelper.ExecuteScalar(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));

				return true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return false;
		}

		private bool CPR발송_선적(DataRow dr)
		{
			List<string> fileList;
			string 보내는사람, 받는사람, 참조, 제목, filePath, query, html;

			try
			{
				보내는사람 = "log1@dintec.co.kr";
				받는사람 = dr["NO_EMAIL_DELIVERY"].ToString();
				참조 = "chaelim.lee@dintec.co.kr";

				제목 = "[DELIVERY NOTICE/TRANSFER ORDER] PO# {0} / {1}";
				제목 = string.Format(제목, dr["NO_PO_PARTNER"].ToString(), dr["NM_VESSEL"].ToString());

				html = @"<div style='font-size: 9pt; font-family: 맑은 고딕;'>
Dear sir or madam,<br><br>
PO# {0} / {1}<br>
Delivery to {2} on {3}<br><br> 
Please be informed that subject order will be transferred according to your instruction.<br>
Kindly refer to attached CIPL and parcel list.<br><br>
Thank you.<br>
Best Regards,<br>
</div>";
				html = string.Format(html, dr["NO_PO_PARTNER"].ToString(),
										   dr["NM_VESSEL"].ToString(),
										   dr["DC_VESSEL"].ToString(),
										   Util.GetTo_DateStringS(dr["DT_COMPLETE"].ToString()));

				filePath = Path.Combine(Application.StartupPath, "temp") + "\\" + dr["NO_GIR"].ToString() + "\\";
				if (Directory.Exists(filePath))
				{
					string[] files = Directory.GetFiles(filePath);

					foreach (string file in files)
						File.Delete(file);
				}
				else
					Directory.CreateDirectory(filePath);

				fileList = new List<string>();
				List<string> 면허용List = new List<string>();

				if (!this.CPR출력(dr, filePath, ref fileList, ref 면허용List, false))
					return false;

				string 파일명 = (dr["NM_VESSEL"].ToString() == string.Empty ? string.Empty : Regex.Replace(dr["NM_VESSEL"].ToString().Replace(' ', '_'), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ_]", string.Empty, RegexOptions.Singleline)) + " " +
								(dr["NO_PO_PARTNER"].ToString() == string.Empty ? string.Empty : Regex.Replace(dr["NO_PO_PARTNER"].ToString().Split(',')[0].Replace(' ', '_'), @"[^a-zA-Z0-9가-힣ㄱ-ㅎㅏ-ㅣ_]", string.Empty, RegexOptions.Singleline));

				PDF.Merge(filePath + 파일명 + ".pdf", fileList.ToArray());

				Thread.Sleep(5000); //5초

				fileList = new List<string>();

				fileList.Add(filePath + 파일명 + ".pdf");				

				#region 협조전 첨부파일
				query = @"SELECT MF.FILE_NAME,
								 '/Upload/P_CZ_SA_GIR/' + GH.CD_COMPANY + '/' + LEFT(GH.DT_GIR, 4) AS FILE_PATH,
								 GH.NO_GIR AS CD_FILE
						  FROM SA_GIRH GH WITH(NOLOCK)
						  JOIN MA_FILEINFO MF WITH(NOLOCK) ON MF.CD_COMPANY = GH.CD_COMPANY AND MF.CD_MODULE = 'SA' AND MF.ID_MENU = 'P_CZ_SA_GIR' AND MF.CD_FILE = GH.NO_GIR
						  WHERE GH.CD_COMPANY = '{0}'
						  AND GH.NO_GIR = '{1}'";

				DataTable dt = DBHelper.GetDataTable(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));

				foreach (DataRow dr1 in dt.Rows)
				{
					FileUploader.DownloadFile(dr1["FILE_NAME"].ToString(), filePath, dr1["FILE_PATH"].ToString(), dr1["CD_FILE"].ToString());
					fileList.Add(filePath + dr1["FILE_NAME"].ToString());
				}
				#endregion

				if (!this.메일발송(보내는사람, 받는사람, 참조, string.Empty, 제목, string.Empty, html, fileList))
				{
					//this.ShowMessage("메일발송실패");
					return false;
				}

				query = @"UPDATE CZ_SA_GIRH_WORK_DETAIL
						  SET YN_CPR = 'Y',
							  DTS_CPR = NEOE.SF_SYSDATE(GETDATE())
						  WHERE CD_COMPANY = '{0}'
						  AND NO_GIR = '{1}'";

				DBHelper.ExecuteScalar(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));

				return true;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}

			return false;
		}

		private bool 메일발송(string 보내는사람, string 받는사람, string 참조, string 숨은참조, string 제목, string 본문, string html, List<string> 첨부파일List)
		{
			MailMessage mailMessage;
			SmtpClient smtpClient;
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
				tempText = 보내는사람.Split('|');

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

				DBMgr dbMgr = new DBMgr(DBConn.Mail);
				dbMgr.Query = query;

				pw = dbMgr.GetDataTable().Rows[0]["DU_PWD"].ToString();
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

				foreach (string 첨부파일 in 첨부파일List)
				{
					mailMessage.Attachments.Add(new Attachment(첨부파일));
				}
				#endregion

				#region 메일보내기
				smtpClient = new SmtpClient("113.130.254.131", 587);
				smtpClient.EnableSsl = false;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials = new NetworkCredential(address, pw);
				smtpClient.Send(mailMessage);
				#endregion

				return true;
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
				dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, "temp"));
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

		private void QR코드생성(string 회사코드, string 의뢰번호)
		{
			string query;

			try
			{
				query = @"SELECT GL.NO_SO 
						  FROM SA_GIRH GH
						  JOIN SA_GIRL GL ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
						  WHERE GH.CD_COMPANY = '{0}'
						  AND GH.NO_GIR = '{1}'
						  AND ISNULL(GH.YN_RETURN, 'N') <> 'Y'
						  AND EXISTS (SELECT 1 
						  			  FROM SA_GIRL GL
						  			  WHERE GL.CD_COMPANY = GH.CD_COMPANY
						  			  AND GL.NO_GIR = GH.NO_GIR
						  			  AND ISNULL(GL.TXT_USERDEF1, '') = '')
						  GROUP BY GL.NO_SO";

				DataTable tmpDt = DBHelper.GetDataTable(string.Format(query, 회사코드, 의뢰번호));

				if (tmpDt != null && tmpDt.Rows.Count > 0)
				{
					string 상업송장qr = SHORTNER.상업송장(string.Format("{0}/{1}", 회사코드, 의뢰번호));
					string 포장명세서qr = SHORTNER.포장명세서(string.Format("{0}/{1}", 회사코드, 의뢰번호));

					foreach (DataRow dr1 in tmpDt.Rows)
					{
						string 인수증qr = SHORTNER.인수증(string.Format("{0}/{1}/{2}", 회사코드, 의뢰번호, dr1["NO_SO"].ToString()));

						DBHelper.ExecuteScalar(string.Format(@"UPDATE SA_GIRL
                                                               SET TXT_USERDEF1 = '{3}',
                                                                   TXT_USERDEF2 = '{4}',
                                                                   TXT_USERDEF3 = '{5}'
                                                               WHERE CD_COMPANY = '{0}'
                                                               AND NO_GIR = '{1}'
                                                               AND NO_SO = '{2}'", 회사코드, 의뢰번호, dr1["NO_SO"].ToString(), 인수증qr, 상업송장qr, 포장명세서qr));
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
		#endregion
	}
}
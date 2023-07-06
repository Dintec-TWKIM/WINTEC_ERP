using Aspose.Email.Outlook;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Erpiu.ComponentModel;
using Duzon.ERPU;
using Duzon.ERPU.MF;
using Duzon.ERPU.Utils;
using Duzon.Windows.Print;
using DX;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_AUTO_MAIL_MNG : PageBase
	{
		P_CZ_SA_AUTO_MAIL_MNG_BIZ _biz = new P_CZ_SA_AUTO_MAIL_MNG_BIZ();

		public P_CZ_SA_AUTO_MAIL_MNG()
		{
			InitializeComponent();
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			// 라이선스 인증
			Aspose.Email.License license = new Aspose.Email.License();
			license.SetLicense("Aspose.Email.lic");

			this.InitGrid();
			this.InitEvent();
		}

		private void InitGrid()
		{
			this.MainGrids = new FlexGrid[] { this._flex매입처, this._flex매출처, this._flex물류업무 };
			this._flex매입처.DetailGrids = new FlexGrid[] { this._flex미입고 };
			this._flex매출처.DetailGrids = new FlexGrid[] { this._flexOrderStatus, this._flexReadyInfo, this._flexReadyPack, this._flex물류업무 };

			#region 매입처

			#region 설정
			this._flex매입처.BeginSetting(1, 1, false);

			this._flex매입처.SetCol("CD_PARTNER", "거래처코드", 100);
			this._flex매입처.SetCol("LN_PARTNER", "거래처명", 100);
			this._flex매입처.SetCol("YN_SEND", "발송대상", 100);
			this._flex매입처.SetCol("YN_NOT_GR", "미입고존재", 100);
			this._flex매입처.SetCol("YN_SEND_DATA", "발송대상존재", 100);

			this._flex매입처.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid2 });
			this._flex매입처.SetBindningRadioButton(new RadioButtonExt[] { this.rdo매일, this.rdo매주 }, new string[] { "DAY", "WEK" });
			this._flex매입처.SetBindningRadioButton(new RadioButtonExt[] { this.rdo전체, this.rdo담당자별 }, new string[] { "ALL", "EMP" });
			this._flex매입처.SetBindningCheckBox(this.chk월요일, "Y", "N");
			this._flex매입처.SetBindningCheckBox(this.chk화요일, "Y", "N");
			this._flex매입처.SetBindningCheckBox(this.chk수요일, "Y", "N");
			this._flex매입처.SetBindningCheckBox(this.chk목요일, "Y", "N");
			this._flex매입처.SetBindningCheckBox(this.chk금요일, "Y", "N");

			//this._flex매입처.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			this._flex매입처.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion

			#region 미입고
			this._flex미입고.BeginSetting(1, 1, false);

			this._flex미입고.SetCol("NM_KOR", "영업물류담당", 100);
			this._flex미입고.SetCol("NO_PO", "발주번호", 100);
			this._flex미입고.SetCol("NO_ORDER", "주문번호", 100);
			this._flex미입고.SetCol("NM_VESSEL", "호선명", 100);
			this._flex미입고.SetCol("NO_HULL", "호선번호", 100);
			this._flex미입고.SetCol("DT_PO", "발주일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미입고.SetCol("DT_LIMIT", "납기일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미입고.SetCol("QT_PO_OVERDUE", "발주경과일수", 100, false, typeof(int), FormatTpType.QUANTITY);
			this._flex미입고.SetCol("QT_LT_OVERDUE", "납기경과일수", 100, false, typeof(int), FormatTpType.QUANTITY);
			this._flex미입고.SetCol("DC_RMK_TEXT2", "물류비고", 100, true);
			this._flex미입고.SetCol("YN_EXCLUDE", "발송제외여부", 100, true, CheckTypeEnum.Y_N);
			this._flex미입고.SetCol("YN_URGENT", "긴급여부", 100, true, CheckTypeEnum.Y_N);
			this._flex미입고.SetCol("DT_EXPECT", "예상입고일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미입고.SetCol("DT_REPLY", "회신일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미입고.SetCol("YN_SEND", "발송대상", 100);
			this._flex미입고.SetCol("TP_SEND", "발송유형", 100);
			this._flex미입고.SetCol("DTS_SEND", "발송일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex미입고.SetCol("DC_RMK", "납기/입고지연사유", 100, true);
			this._flex미입고.SetCol("DC_RMK1", "딘텍 요청사항", 100, true);

			this._flex미입고.Cols["DTS_SEND"].Format = "####/##/##/##:##:##";

			//this._flex미입고.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			this._flex미입고.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion

			#region 매입처담당자
			this._flex매입처담당자.BeginSetting(1, 1, false);

			this._flex매입처담당자.SetCol("SEQ", "순번", 100);
			this._flex매입처담당자.SetCol("NM_PTR", "담당자명", 100);
			this._flex매입처담당자.SetCol("NM_EMAIL", "메일주소", 100, true);
			this._flex매입처담당자.SetCol("YN_LIMIT", "납기담당", 100, true, CheckTypeEnum.Y_N);
			this._flex매입처담당자.SetCol("YN_CHECK", "메일검증", 100, false, CheckTypeEnum.Y_N);

			//this._flex매입처담당자.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			this._flex매입처담당자.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion

			#endregion

			#region 매출처

			#region 설정
			this._flex매출처.BeginSetting(1, 1, false);

			this._flex매출처.SetCol("CD_PARTNER", "거래처코드", 100);
			this._flex매출처.SetCol("LN_PARTNER", "거래처명", 100);
			this._flex매출처.SetCol("NM_PARTNER_GRP", "거래처그룹", 100);
			this._flex매출처.SetCol("CD_PARTNER_GRP", "거래처그룹(수주)", 100);
			this._flex매출처.SetCol("YN_ORDER_STAT", "OrderStatus", 100);
			this._flex매출처.SetCol("YN_READY_INFO", "ReadyInfo", 100);
			this._flex매출처.SetCol("YN_REMARK", "납기지연", 100);
			this._flex매출처.SetCol("NM_LOG", "영업물류담당자", 100);

			this._flex매출처.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid4, this.oneGrid5, this.oneGrid6, this.oneGrid7 });
			this._flex매출처.SetBindningRadioButton(new RadioButtonExt[] { this.rdo미선택RI, this.rdo월요일RI, this.rdo화요일RI, this.rdo수요일RI, this.rdo목요일RI, this.rdo금요일RI }, new string[] { "", "MON", "TUE", "WED", "THU", "FRI" });
			this._flex매출처.SetBindningCheckBox(this.chk월요일OS, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk화요일OS, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk수요일OS, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk목요일OS, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk금요일OS, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk월요일WO, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk화요일WO, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk수요일WO, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk목요일WO, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk금요일WO, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk발송유형OS, "Y", "N");
			this._flex매출처.SetBindningCheckBox(this.chk발송유형RI, "Y", "N");
			
			this._flex매출처.SetDataMap("CD_PARTNER_GRP", MA.GetCode("MA_B000065"), "CODE", "NAME");

			//this._flex매출처.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			this._flex매출처.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion

			#region 물류업무
			this._flex물류업무.BeginSetting(1, 1, false);

			this._flex물류업무.SetCol("CD_CATEGORY1", "분류1", 100);
			this._flex물류업무.SetCol("CD_CATEGORY2", "분류2", 100);
			this._flex물류업무.SetCol("CD_DELIVERY", "납품처", 100);

			this._flex물류업무.VerifyNotNull = new string[] { "CD_CATEGORY1" };

			this._flex물류업무.SetDataMap("CD_CATEGORY1", DBHelper.GetDataTable(@"SELECT CD_SYSDEF AS CODE,
																				         NM_SYSDEF AS NAME
																				  FROM CZ_MA_CODEDTL WITH(NOLOCK)
																				  WHERE CD_COMPANY = 'K100'
																				  AND CD_FIELD = 'CZ_SA00060'
																				  AND CD_SYSDEF <> '000'
																				  AND ISNULL(CD_FLAG1, '') = ''"), "CODE", "NAME");
			this._flex물류업무.SetDataMap("CD_CATEGORY2", DBHelper.GetDataTable(@"SELECT CD_SYSDEF AS CODE,
																				  	     NM_SYSDEF AS NAME
																				  FROM CZ_MA_CODEDTL WITH(NOLOCK)
																				  WHERE CD_COMPANY = 'K100'
																				  AND CD_FIELD = 'CZ_SA00060'
																				  AND CD_SYSDEF<> '000'
																				  AND ISNULL(CD_FLAG1, '') <> ''"), "CODE", "NAME");
			
			this._flex물류업무.SetOneGridBinding(new object[] { this.cbo분류1, this.cbo분류2, this.bpc납품처 }, new IUParentControl[] { this.oneGrid11 });

			this._flex물류업무.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region 호선
			this._flex호선.BeginSetting(1, 1, false);

			this._flex호선.SetCol("S", "선택", 60, true, CheckTypeEnum.Y_N);
			this._flex호선.SetCol("NO_IMO", "IMO번호", 100);
			this._flex호선.SetCol("NM_VESSEL", "호선명", 100);

			this._flex호선.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			#endregion

			#region OrderStatus 담당자
			this._flexOrderStatus담당자.BeginSetting(1, 1, false);

			this._flexOrderStatus담당자.SetCol("SEQ", "순번", 100);
			this._flexOrderStatus담당자.SetCol("NM_PTR", "담당자명", 100);
			this._flexOrderStatus담당자.SetCol("NM_EMAIL", "메일주소", 100, true);
			this._flexOrderStatus담당자.SetCol("YN_SO", "수주담당", 100, true, CheckTypeEnum.Y_N);
			this._flexOrderStatus담당자.SetCol("YN_CHECK", "메일검증", 100, false, CheckTypeEnum.Y_N);

			this._flexOrderStatus담당자.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion

			#region ReadyInfo 담당자
			this._flexReadyInfo담당자.BeginSetting(1, 1, false);

			this._flexReadyInfo담당자.SetCol("SEQ", "순번", 100);
			this._flexReadyInfo담당자.SetCol("NM_PTR", "담당자명", 100);
			this._flexReadyInfo담당자.SetCol("NM_EMAIL", "메일주소", 100, true);
			this._flexReadyInfo담당자.SetCol("YN_SO", "수주담당", 100, true, CheckTypeEnum.Y_N);
			this._flexReadyInfo담당자.SetCol("YN_PACK", "기본메일로표시(포장완료메일)", 100, true, CheckTypeEnum.Y_N);
			this._flexReadyInfo담당자.SetCol("YN_CHECK", "메일검증", 100, false, CheckTypeEnum.Y_N);

			this._flexReadyInfo담당자.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion

			#endregion

			#region Ready Pack
			this._flexReadyPack.BeginSetting(1, 1, false);

			this._flexReadyPack.SetCol("NO_GIR", "의뢰번호", 100);
			this._flexReadyPack.SetCol("LN_PARTNER", "매출처", 100);
			this._flexReadyPack.SetCol("NM_VESSEL", "Vessel", 100);
			this._flexReadyPack.SetCol("NO_PO_PARTNER", "Order No.", 100);
			this._flexReadyPack.SetCol("NO_SO", "Dintec No.", 100);
			this._flexReadyPack.SetCol("DT_PACK", "Date", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexReadyPack.SetCol("NM_TYPE", "Style", 100);
			this._flexReadyPack.SetCol("DC_SIZE", "Size", 100);
			this._flexReadyPack.SetCol("GROSS_WEIGHT", "Gross Weight", 100);
			this._flexReadyPack.SetCol("AM_WOODEN", "Amount", 100);
			this._flexReadyPack.SetCol("DC_STATUS", "Status", 100);
			this._flexReadyPack.SetCol("FROM_EMAIL", "보내는사람", 100);
			this._flexReadyPack.SetCol("TO_EMAIL", "받는사람", 100);
			this._flexReadyPack.SetCol("CC_EMAIL", "참조", 100);
			this._flexReadyPack.SetCol("DC_URL", "사진", 100);

			this._flexReadyPack.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion

			#region Order Status
			this._flexOrderStatus.BeginSetting(1, 1, false);

			this._flexOrderStatus.SetCol("CD_PARTNER", "매출처코드", 100);
			this._flexOrderStatus.SetCol("LN_PARTNER", "매출처", 100);
			this._flexOrderStatus.SetCol("NM_PARTNER_GRP", "거래처그룹", 100);
			this._flexOrderStatus.SetCol("NM_VESSEL", "Vessel", 100);
			this._flexOrderStatus.SetCol("NO_PO_PARTNER", "Order No.", 100);
			this._flexOrderStatus.SetCol("NO_SO", "Dintec No.", 100);
			this._flexOrderStatus.SetCol("DT_SO", "Ordered date", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexOrderStatus.SetCol("DT_DUEDATE", "Due date", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexOrderStatus.SetCol("QT_DELAY", "지연일수", 100);
			this._flexOrderStatus.SetCol("ST_SO", "Status", 100);
			this._flexOrderStatus.SetCol("DC_ITEM", "Ready item", 100);
			this._flexOrderStatus.SetCol("DT_EXPECT", "Expected Readiness", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexOrderStatus.SetCol("CD_RMK", "Remark", 100, true);
			this._flexOrderStatus.SetCol("DC_RMK1", "RemarkDetail", 100, true);
			this._flexOrderStatus.SetCol("YN_EXCLUDE", "발송제외여부", 100, true, CheckTypeEnum.Y_N);
			this._flexOrderStatus.SetCol("TP_SEND", "발송유형", 100);
			this._flexOrderStatus.SetCol("DTS_SEND", "발송일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexOrderStatus.SetCol("DC_RMK_TEXT2", "물류비고", 100);
			this._flexOrderStatus.SetCol("DT_REPLY", "회신일자",0, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			this._flexOrderStatus.ExtendLastCol = true;

			this._flexOrderStatus.Cols["DTS_SEND"].Format = "####/##/##/##:##:##";

			string query = @"SELECT '' AS CODE, 
       '' AS NAME
UNION ALL
SELECT CD_SYSDEF AS CODE,
       NM_SYSDEF AS NAME
FROM CZ_MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_FIELD = 'CZ_SA00052'";

			this._flexOrderStatus.SetDataMap("CD_RMK", DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode)), "CODE", "NAME");

			//this._flexOrderStatus.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
			this._flexOrderStatus.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion

			#region Ready Info
			this._flexReadyInfo.BeginSetting(1, 1, false);

			this._flexReadyInfo.SetCol("LN_PARTNER", "매출처", 100);
			this._flexReadyInfo.SetCol("NM_VESSEL", "Vessel", 100);
			this._flexReadyInfo.SetCol("NO_PO_PARTNER", "Order No.", 100);
			this._flexReadyInfo.SetCol("NO_SO", "Dintec No.", 100);
			this._flexReadyInfo.SetCol("WEIGHT", "Approx.", 100);
			this._flexReadyInfo.SetCol("DT_IN", "Ready date", 100, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flexReadyInfo.SetCol("DC_RMK", "Remark", 100);
			this._flexReadyInfo.SetCol("YN_EXCLUDE", "발송제외여부", 100, true, CheckTypeEnum.Y_N);
			this._flexReadyInfo.SetCol("YN_INCLUDE", "발송포함여부", 100, true, CheckTypeEnum.Y_N);

			this._flexReadyInfo.SetDefault("1.0.0.0", SumPositionEnum.None);
			#endregion
		}

		private void InitEvent()
		{
			this._flex매입처.AfterRowChange += _flex매입처_AfterRowChange;
			this._flex매입처담당자.ValidateEdit += _flex담당자_ValidateEdit;
			this._flex매입처담당자.AfterEdit += _flex매입처담당자_AfterEdit;
			this._flex미입고.AfterEdit += _flex미입고_AfterEdit;
			this._flexReadyInfo.AfterEdit += _flexReadyInfo_AfterEdit;

			this._flex매출처.AfterRowChange += _flex매출처_AfterRowChange;
			this._flex물류업무.AfterRowChange += _flex물류업무_AfterRowChange;
			this._flexOrderStatus담당자.ValidateEdit += _flex담당자_ValidateEdit;
			this._flexOrderStatus담당자.AfterEdit += _flexOrderStatus담당자_AfterEdit;
			this._flexOrderStatus.AfterEdit += _flexOrderStatus_AfterEdit;
			this._flexReadyPack.AfterEdit += _flexReadyPack_AfterEdit;

			this._flex호선.AfterRowChange += _flex호선_AfterRowChange;
			this._flexReadyInfo담당자.ValidateEdit += _flex담당자_ValidateEdit;
			this._flexReadyInfo담당자.AfterEdit += _flexReadyInfo담당자_AfterEdit;

			this.rdo매일.CheckedChanged += RadioButton_CheckedChanged;
			this.rdo매주.CheckedChanged += RadioButton_CheckedChanged;
			
			this.btn메일업로드.Click += Btn메일업로드_Click;
			this.btn포장메일발송.Click += Btn포장메일발송_Click;
			this.bpc납품처.QueryBefore += Bpc납품처_QueryBefore;
			this.bpc납품처.QueryAfter += Bpc납품처_QueryAfter;
			this.cbo분류1.SelectionChangeCommitted += Cbo분류1_SelectionChangeCommitted;
			this.cbo분류2.SelectionChangeCommitted += Cbo분류2_SelectionChangeCommitted;
			this.btn선택적용.Click += Btn선택적용_Click;
			this.btn물류업무추가.Click += Btn물류업무추가_Click;
			this.btn물류업무삭제.Click += Btn물류업무삭제_Click;
			this.bpc거래처그룹.QueryBefore += Bpc거래처그룹_QueryBefore;
		}

		private void Bpc거래처그룹_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P41_CD_FIELD1 = "MA_B000065";
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Cbo분류2_SelectionChangeCommitted(object sender, EventArgs e)
		{
			try
			{
				this._flex물류업무["CD_CATEGORY2"] = this.cbo분류2.SelectedValue.ToString();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex물류업무_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string query;

			try
			{
				query = @"SELECT MP.LN_PARTNER 
						  FROM CZ_MA_DELIVERY MP WITH(NOLOCK) 
						  WHERE MP.CD_COMPANY = '{0}' 
						  AND MP.CD_PARTNER = '{1}'";

				this.bpc납품처.Clear();

				foreach (string 납품처코드 in this._flex물류업무["CD_DELIVERY"].ToString().Split('|'))
				{
					if (string.IsNullOrEmpty(납품처코드)) continue;

					dt = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode, 납품처코드 }));

					this.bpc납품처.AddItem(납품처코드, dt.Rows[0]["LN_PARTNER"].ToString());
				}

				query = @"SELECT '' AS CODE, 
       '' AS NAME
UNION ALL
SELECT CD_SYSDEF AS CODE,
       NM_SYSDEF AS NAME
FROM CZ_MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_SA00060'
AND CD_SYSDEF <> '000'
AND ISNULL(CD_FLAG1, '') <> ''
AND CD_FLAG1 = '{0}'";

				this.cbo분류2.DataSource = DBHelper.GetDataTable(string.Format(query, new string[] { this._flex물류업무["CD_CATEGORY1"].ToString() }));
				this.cbo분류2.DisplayMember = "NAME";
				this.cbo분류2.ValueMember = "CODE";

				this.cbo분류2.SelectedValue = this._flex물류업무["CD_CATEGORY2"].ToString();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn물류업무삭제_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex물류업무.HasNormalRow) return;

				this._flex물류업무.RemoveItem(this._flex물류업무.Row);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn물류업무추가_Click(object sender, EventArgs e)
		{
			try
			{
				if (!this._flex매출처.HasNormalRow) return;

				this._flex물류업무.Rows.Add();
				this._flex물류업무.Row = this._flex물류업무.Rows.Count - 1;

				this._flex물류업무["CD_COMPANY"] = Global.MainFrame.LoginInfo.CompanyCode;
				this._flex물류업무["CD_PARTNER"] = this._flex매출처["CD_PARTNER"].ToString();

				this._flex물류업무.AddFinished();
				this._flex물류업무.Focus();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Bpc납품처_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				this._flex물류업무["CD_DELIVERY"] = this.bpc납품처.QueryWhereIn_Pipe;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn선택적용_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				if (Global.MainFrame.ShowMessage("선택되어 있는 호선에 선택한 담당자를 일괄 지정 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				if (string.IsNullOrEmpty(this._flexReadyInfo담당자["NM_EMAIL"].ToString()))
				{
					this.ShowMessage("E-Mail 은 필수입력항목입니다.");
					return;
				}
				else if (Convert.ToInt32(DBHelper.ExecuteScalar(string.Format("SELECT NEOE.CHECK_EMAIL('{0}')", this._flexReadyInfo담당자["NM_EMAIL"].ToString()))) == 0)
				{
					this.ShowMessage("메일주소에 형식에 맞지 않은 이메일 주소가 입력 되어 있습니다.");
					return;
				}

				dataRowArray = this._flex호선.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
					return;
				}
				else
				{
					query = @"IF NOT EXISTS(SELECT 1 
              FROM CZ_SA_AUTO_MAIL_VESSEL MV WITH(NOLOCK)
              WHERE MV.CD_COMPANY = '{0}'
              AND MV.CD_PARTNER = '{1}'
              AND MV.NO_IMO = '{2}'
              AND MV.SEQ = '{3}')
BEGIN
    INSERT INTO CZ_SA_AUTO_MAIL_VESSEL
    (
        CD_COMPANY,
	    CD_PARTNER,
	    NO_IMO,
	    SEQ,
		YN_SO,
	    ID_INSERT,
	    DTS_INSERT
    )
    VALUES
    (
        '{0}',
        '{1}',
        '{2}',
        '{3}',
	    'Y',
        '{4}',
        NEOE.SF_SYSDATE(GETDATE())
    )
END";

					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																	this._flexReadyInfo담당자["CD_PARTNER"].ToString(),
																	dr["NO_IMO"].ToString(),
																	this._flexReadyInfo담당자["SEQ"].ToString(),
																	Global.MainFrame.LoginInfo.UserID));
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.dtp포장일자.StartDateToString = Global.MainFrame.GetStringToday;
			this.dtp포장일자.EndDateToString = Global.MainFrame.GetStringToday;

			this.dtp준비일자.StartDateToString = Global.MainFrame.GetDateTimeToday().AddYears(-1).ToString("yyyyMMdd");
			this.dtp준비일자.EndDateToString = Global.MainFrame.GetStringToday;

			string query = @"SELECT '' AS CODE, 
       '' AS NAME
UNION ALL
SELECT CD_SYSDEF AS CODE,
       NM_SYSDEF AS NAME
FROM CZ_MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_SA00060'
AND CD_SYSDEF <> '000'
AND ISNULL(CD_FLAG1, '') = ''";

			this.cbo분류1.DataSource = DBHelper.GetDataTable(query);
			this.cbo분류1.DisplayMember = "NAME";
			this.cbo분류1.ValueMember = "CODE";

			if (Global.MainFrame.LoginInfo.UserID == "SYSADMIN")
				this.btn포장메일발송.Visible = true;
			else
				this.btn포장메일발송.Visible = false;

			SetControl setControl = new SetControl();
			setControl.SetCombobox(this.cbo거래처그룹, new DataView(MA.GetCode("MA_B000065"), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
			setControl.SetCombobox(this.cbo거래처그룹_RI, new DataView(MA.GetCode("MA_B000065", true), string.Empty, "NAME ASC", DataViewRowState.CurrentRows).ToTable());
		}

		private void _flex담당자_ValidateEdit(object sender, ValidateEditEventArgs e)
		{
			FlexGrid grid;

			try
			{
				grid = (FlexGrid)sender;

				string colname = grid.Cols[e.Col].Name;
				string oldValue = D.GetString(grid.GetData(e.Row, e.Col));
				string newValue = grid.EditData;

				if (colname == "NM_EMAIL")
				{
					if (string.IsNullOrEmpty(newValue))
					{
						this.ShowMessage("E-Mail 은 필수입력항목입니다.");
						grid["NM_EMAIL"] = oldValue;
						e.Cancel = true;
						return;
					}
					else if (Convert.ToInt32(DBHelper.ExecuteScalar(string.Format("SELECT NEOE.CHECK_EMAIL('{0}')", newValue))) == 0)
					{
						this.ShowMessage("메일주소에 형식에 맞지 않은 이메일 주소가 입력 되어 있습니다.");
						grid["NM_EMAIL"] = oldValue;
						e.Cancel = true;
						return;
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexOrderStatus_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string columnName;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				columnName = flexGrid.Cols[e.Col].Name;

				if (columnName != "YN_EXCLUDE" &&
					columnName != "CD_RMK" &&
					columnName != "DC_RMK1")
					return;

				DBHelper.ExecuteNonQuery("SP_CZ_SA_AUTO_MAIL_DD_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						   "0",
																						   flexGrid["NO_SO"].ToString(),
																						   flexGrid["NO_SO"].ToString(),
																						   flexGrid["DT_DUEDATE"].ToString(),
																						   string.Empty,
																						   string.Empty,
																						   flexGrid["YN_EXCLUDE"].ToString(),
																						   string.Empty,
																						   string.Empty,
																						   flexGrid["DC_RMK1"].ToString(),
																						   flexGrid["CD_RMK"].ToString(),
																						   Global.MainFrame.LoginInfo.UserID });
				
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexReadyPack_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string columnName, query;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				columnName = flexGrid.Cols[e.Col].Name;

				if (columnName == "YN_EX_MAIL")
				{
					query = @"UPDATE PH
						      SET PH.YN_EX_MAIL = '{3}'
							  FROM CZ_SA_PACKH PH
							  WHERE PH.CD_COMPANY = '{0}'
							  AND PH.NO_GIR = '{1}'
							  AND PH.NO_PACK = '{2}'";

					DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
															    flexGrid["NO_GIR"].ToString(),
																flexGrid["NO_PACK"].ToString(),
																flexGrid["YN_EX_MAIL"].ToString()));
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex매출처_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string query;

			try
			{
				if (this._flex매출처.HasNormalRow == false) return;

				query = @"SELECT 'N' AS S, SH.NO_IMO, MH.NM_VESSEL 
FROM SA_SOH SH WITH(NOLOCK)
LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = SH.NO_IMO
WHERE SH.CD_COMPANY = '{0}'
AND SH.CD_PARTNER = '{1}'
GROUP BY SH.NO_IMO, MH.NM_VESSEL";

				this._flex호선.Binding = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																					this._flex매출처["CD_PARTNER"].ToString()));

				query = @"SELECT CD_PARTNER,
	   SEQ,
       NM_PTR,
       NM_EMAIL,
	   YN_SO,
	   (CASE WHEN NEOE.CHECK_EMAIL(NM_EMAIL) = 0 THEN 'N' ELSE 'Y' END) AS YN_CHECK
FROM FI_PARTNERPTR WITH (NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_PARTNER = '{1}'";

				this._flexOrderStatus담당자.Binding = DBHelper.GetDataTable(string.Format(query, new string[] { Global.MainFrame.LoginInfo.CompanyCode, 
																										        this._flex매출처["CD_PARTNER"].ToString() }));

				string filter = string.Format("CD_PARTNER = '{0}'", this._flex매출처["CD_PARTNER"].ToString());
				dt = null;

				if (this._flex매출처.DetailQueryNeed == true)
				{
					dt = this._biz.SearchDetail4(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																this._flex매출처["CD_PARTNER"].ToString() });
				}

				this._flex물류업무.BindingAdd(dt, filter);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexReadyInfo담당자_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string query, name;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				name = flexGrid.Cols[e.Col].Name;

				switch (name)
				{
					case "YN_SO":
						if (this._flexReadyInfo담당자["YN_SO"].ToString() == "Y")
						{
							query = @"IF NOT EXISTS(SELECT 1 
              FROM CZ_SA_AUTO_MAIL_VESSEL MV WITH(NOLOCK)
              WHERE MV.CD_COMPANY = '{0}'
              AND MV.CD_PARTNER = '{1}'
              AND MV.NO_IMO = '{2}'
              AND MV.SEQ = '{3}')
BEGIN
    INSERT INTO CZ_SA_AUTO_MAIL_VESSEL
    (
        CD_COMPANY,
	    CD_PARTNER,
	    NO_IMO,
	    SEQ,
	    YN_SO,
	    ID_INSERT,
	    DTS_INSERT
    )
    VALUES
    (
        '{0}',
        '{1}',
        '{2}',
        '{3}',
	    'Y',
        '{4}',
        NEOE.SF_SYSDATE(GETDATE())
    )
END
ELSE
BEGIN
	UPDATE MV
	SET YN_SO = 'Y',
	    DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE()),
		ID_UPDATE = '{4}'
	FROM CZ_SA_AUTO_MAIL_VESSEL MV
	WHERE MV.CD_COMPANY = '{0}'
	AND MV.CD_PARTNER = '{1}'
	AND MV.NO_IMO = '{2}'
	AND MV.SEQ = '{3}'
END";
							DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																		this._flexReadyInfo담당자["CD_PARTNER"].ToString(),
																		this._flexReadyInfo담당자["NO_IMO"].ToString(),
																		this._flexReadyInfo담당자["SEQ"].ToString(),
																		Global.MainFrame.LoginInfo.UserID));
						}
						else
						{
							query = @"UPDATE MV
SET YN_SO = 'N',
    DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE()),
	ID_UPDATE = '{4}'
FROM CZ_SA_AUTO_MAIL_VESSEL MV
WHERE MV.CD_COMPANY = '{0}'
AND MV.CD_PARTNER = '{1}'
AND MV.NO_IMO = '{2}'
AND MV.SEQ = '{3}'";
							DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																		this._flexReadyInfo담당자["CD_PARTNER"].ToString(),
																		this._flexReadyInfo담당자["NO_IMO"].ToString(),
																		this._flexReadyInfo담당자["SEQ"].ToString(),
																		Global.MainFrame.LoginInfo.UserID));
						}
						break;
					case "YN_PACK":
						if (this._flexReadyInfo담당자["YN_PACK"].ToString() == "Y")
						{
							query = @"IF NOT EXISTS(SELECT 1 
              FROM CZ_SA_AUTO_MAIL_VESSEL MV WITH(NOLOCK)
              WHERE MV.CD_COMPANY = '{0}'
              AND MV.CD_PARTNER = '{1}'
              AND MV.NO_IMO = '{2}'
              AND MV.SEQ = '{3}')
BEGIN
    INSERT INTO CZ_SA_AUTO_MAIL_VESSEL
    (
        CD_COMPANY,
	    CD_PARTNER,
	    NO_IMO,
	    SEQ,
	    YN_PACK,
	    ID_INSERT,
	    DTS_INSERT
    )
    VALUES
    (
        '{0}',
        '{1}',
        '{2}',
        '{3}',
	    'Y',
        '{4}',
        NEOE.SF_SYSDATE(GETDATE())
    )
END
ELSE
BEGIN
	UPDATE MV
	SET YN_PACK = 'Y',
	    DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE()),
		ID_UPDATE = '{4}'
	FROM CZ_SA_AUTO_MAIL_VESSEL MV
	WHERE MV.CD_COMPANY = '{0}'
	AND MV.CD_PARTNER = '{1}'
	AND MV.NO_IMO = '{2}'
	AND MV.SEQ = '{3}'
END";
							DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																		this._flexReadyInfo담당자["CD_PARTNER"].ToString(),
																		this._flexReadyInfo담당자["NO_IMO"].ToString(),
																		this._flexReadyInfo담당자["SEQ"].ToString(),
																		Global.MainFrame.LoginInfo.UserID));
						}
						else
						{
							query = @"UPDATE MV
SET YN_PACK = 'N',
    DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE()),
	ID_UPDATE = '{4}'
FROM CZ_SA_AUTO_MAIL_VESSEL MV
WHERE MV.CD_COMPANY = '{0}'
AND MV.CD_PARTNER = '{1}'
AND MV.NO_IMO = '{2}'
AND MV.SEQ = '{3}'";
							DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode,
																		this._flexReadyInfo담당자["CD_PARTNER"].ToString(),
																		this._flexReadyInfo담당자["NO_IMO"].ToString(),
																		this._flexReadyInfo담당자["SEQ"].ToString(),
																		Global.MainFrame.LoginInfo.UserID));
						}
						break;
					case "NM_EMAIL":
						query = @"UPDATE FP
						          SET FP.NM_EMAIL = '{3}'
						          FROM FI_PARTNERPTR FP
						          WHERE FP.CD_COMPANY = '{0}'
						          AND FP.CD_PARTNER = '{1}'
						          AND FP.SEQ = '{2}'";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this._flexReadyInfo담당자["CD_PARTNER"].ToString(),
																				   this._flexReadyInfo담당자["SEQ"].ToString(),
																				   this._flexReadyInfo담당자["NM_EMAIL"].ToString() }));
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex호선_AfterRowChange(object sender, RangeEventArgs e)
		{
			string query;

			try
			{
				query = @"SELECT FP.CD_PARTNER,
	   '{2}' AS NO_IMO,
	   FP.SEQ,
       FP.NM_PTR,
       FP.NM_EMAIL,
	   ISNULL(MV.YN_SO, 'N') AS YN_SO,
       ISNULL(MV.YN_PACK, 'N') AS YN_PACK,
	   (CASE WHEN NEOE.CHECK_EMAIL(FP.NM_EMAIL) = 0 THEN 'N' ELSE 'Y' END) AS YN_CHECK
FROM FI_PARTNERPTR FP WITH (NOLOCK)
LEFT JOIN CZ_SA_AUTO_MAIL_VESSEL MV WITH(NOLOCK) ON MV.CD_COMPANY = FP.CD_COMPANY AND MV.CD_PARTNER = FP.CD_PARTNER AND MV.SEQ = FP.SEQ AND MV.NO_IMO = '{2}'
WHERE FP.CD_COMPANY = '{0}'
AND FP.CD_PARTNER = '{1}'";

				this._flexReadyInfo담당자.Binding = DBHelper.GetDataTable(string.Format(query, new string[] { Global.MainFrame.LoginInfo.CompanyCode,
																											  this._flex매출처["CD_PARTNER"].ToString(),
																											  this._flex호선["NO_IMO"].ToString() }));
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexReadyInfo_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string query, columnName;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				columnName = flexGrid.Cols[e.Col].Name;
				query = @"IF EXISTS (SELECT 1 
           FROM CZ_SA_AUTO_MAIL_FILE MF WITH(NOLOCK)
           WHERE MF.CD_COMPANY = '{0}'
           AND MF.NO_SO = '{1}')
BEGIN
    UPDATE MF
    SET MF.YN_EXCLUDE = '{2}',
        MF.YN_INCLUDE = '{3}',
        MF.ID_UPDATE = '{4}',
        MF.DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
    FROM CZ_SA_AUTO_MAIL_FILE MF
    WHERE MF.CD_COMPANY = '{0}'
    AND MF.NO_SO = '{1}'
END
ELSE
BEGIN
    INSERT INTO CZ_SA_AUTO_MAIL_FILE
    (
        CD_COMPANY,
        NO_SO,
        YN_EXCLUDE,
        YN_INCLUDE,
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

				switch (columnName)
				{
					case "YN_EXCLUDE":
					case "YN_INCLUDE":
						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   flexGrid["NO_SO"].ToString(),
																				   flexGrid["YN_EXCLUDE"].ToString(),
																				   flexGrid["YN_INCLUDE"].ToString(),
																				   Global.MainFrame.LoginInfo.UserID }));
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex미입고_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string query, columnName;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				columnName = flexGrid.Cols[e.Col].Name;

				switch (columnName)
				{
					case "DC_RMK_TEXT2":
						query = @"UPDATE SH
								  SET SH.DC_RMK_TEXT2 = '{2}'
								  FROM SA_SOH SH
								  WHERE SH.CD_COMPANY = '{0}'
								  AND SH.NO_SO = '{1}'";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   flexGrid["CD_PJT"].ToString(),
																				   flexGrid["DC_RMK_TEXT2"].ToString() }));
						break;
					case "YN_EXCLUDE":
					case "YN_URGENT":
					case "DT_EXPECT":
					case "DT_REPLY":
					case "DC_RMK":
					case "DC_RMK1":
						DBHelper.ExecuteNonQuery("SP_CZ_SA_AUTO_MAIL_DD_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						   "2",
																						   flexGrid["CD_PJT"].ToString(),
																						   flexGrid["NO_PO"].ToString(),
																						   flexGrid["DT_LIMIT"].ToString(),
																						   flexGrid["DT_EXPECT"].ToString(),
																						   flexGrid["DT_REPLY"].ToString(),
																						   flexGrid["YN_EXCLUDE"].ToString(),
																						   flexGrid["YN_URGENT"].ToString(),
																						   flexGrid["DC_RMK"].ToString(),
																						   flexGrid["DC_RMK1"].ToString(),
																						   string.Empty,
																						   Global.MainFrame.LoginInfo.UserID });
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex매입처담당자_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string query, name;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				name = flexGrid.Cols[e.Col].Name;

				switch (name)
				{
					case "YN_LIMIT":
						query = @"UPDATE FP
						          SET FP.YN_LIMIT = '{3}'
						          FROM FI_PARTNERPTR FP
						          WHERE FP.CD_COMPANY = '{0}'
						          AND FP.CD_PARTNER = '{1}'
						          AND FP.SEQ = '{2}'";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this._flex매입처담당자["CD_PARTNER"].ToString(),
																				   this._flex매입처담당자["SEQ"].ToString(),
																				   this._flex매입처담당자["YN_LIMIT"].ToString() }));
						break;
					case "NM_EMAIL":
						query = @"UPDATE FP
						          SET FP.NM_EMAIL = '{3}'
						          FROM FI_PARTNERPTR FP
						          WHERE FP.CD_COMPANY = '{0}'
						          AND FP.CD_PARTNER = '{1}'
						          AND FP.SEQ = '{2}'";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this._flex매입처담당자["CD_PARTNER"].ToString(),
																				   this._flex매입처담당자["SEQ"].ToString(),
																				   this._flex매입처담당자["NM_EMAIL"].ToString() }));
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flexOrderStatus담당자_AfterEdit(object sender, RowColEventArgs e)
		{
			FlexGrid flexGrid;
			string query, name;

			try
			{
				flexGrid = ((FlexGrid)sender);

				if (flexGrid.HasNormalRow == false) return;

				name = flexGrid.Cols[e.Col].Name;

				switch (name)
				{
					case "YN_SO":
						query = @"UPDATE FP
						          SET FP.YN_SO = '{3}'
						          FROM FI_PARTNERPTR FP
						          WHERE FP.CD_COMPANY = '{0}'
						          AND FP.CD_PARTNER = '{1}'
						          AND FP.SEQ = '{2}'";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this._flexOrderStatus담당자["CD_PARTNER"].ToString(),
																				   this._flexOrderStatus담당자["SEQ"].ToString(),
																				   this._flexOrderStatus담당자["YN_SO"].ToString() }));
						break;
					case "NM_EMAIL":
						query = @"UPDATE FP
						          SET FP.NM_EMAIL = '{3}'
						          FROM FI_PARTNERPTR FP
						          WHERE FP.CD_COMPANY = '{0}'
						          AND FP.CD_PARTNER = '{1}'
						          AND FP.SEQ = '{2}'";

						DBHelper.ExecuteScalar(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																				   this._flexOrderStatus담당자["CD_PARTNER"].ToString(),
																				   this._flexOrderStatus담당자["SEQ"].ToString(),
																				   this._flexOrderStatus담당자["NM_EMAIL"].ToString() }));
						break;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex매입처_AfterRowChange(object sender, RangeEventArgs e)
		{
			DataTable dt;
			string key, filter, query;

			try
			{
				if (this._flex매입처.HasNormalRow == false) return;

				key = D.GetString(this._flex매입처["CD_PARTNER"]);
				filter = "CD_PARTNER = '" + key + "'";
				dt = null;

				if (this._flex매입처.DetailQueryNeed)
				{
					dt = this._biz.SearchDetail(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
															   key,
															   D.GetInt(this._flex매입처["QT_LT_OVERDUE"]),
															   this.txt발주번호.Text,
															   this.ctx영업물류담당S.CodeValue });
				}

				this._flex미입고.BindingAdd(dt, filter);

				query = @"SELECT CD_PARTNER,
	   SEQ,
       NM_PTR,
       NM_EMAIL,
       YN_LIMIT,
	   (CASE WHEN NEOE.CHECK_EMAIL(NM_EMAIL) = 0 THEN 'N' ELSE 'Y' END) AS YN_CHECK
FROM FI_PARTNERPTR WITH (NOLOCK)
WHERE CD_COMPANY = '{0}'
AND CD_PARTNER = '{1}'";

				this._flex매입처담당자.Binding = DBHelper.GetDataTable(string.Format(query, new string[] { Global.MainFrame.LoginInfo.CompanyCode, key }));

			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		private void Btn포장메일발송_Click(object sender, EventArgs e)
		{
			ReportHelper reportHelper;
			DataTable dt, dt1, dt2, dtH, dtL;
			DataRow[] dataRowArray;
			List<string> fileList, tmpList, packList;
			string query, 보내는사람, 받는사람, 참조, 제목, html, body, fileName, 포장일자, 발송불가사유, filePath, fullPath;
			int index;

			try
			{
				#region 매출처발송
				query = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @P_CD_COMPANY NVARCHAR(7) = '{0}'
DECLARE @P_DT_PACK    NVARCHAR(8) = '{1}'

;WITH A AS
(
    SELECT PH.CD_COMPANY, PL.NO_FILE, GH.CD_PARTNER,
		   (ISNULL(AM.TO_EMAIL, '') + ';' + ISNULL(MV.TO_EMAIL, '')) AS TO_EMAIL
    FROM CZ_SA_PACKH PH
    JOIN CZ_SA_GIRH_PACK GH ON GH.CD_COMPANY = PH.CD_COMPANY AND GH.NO_GIR = PH.NO_GIR
    JOIN CZ_SA_GIRH_PACK_DETAIL PD ON PD.CD_COMPANY = GH.CD_COMPANY AND PD.NO_GIR = GH.NO_GIR
    JOIN (SELECT PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK, PL.NO_FILE 
          FROM CZ_SA_PACKL PL
          GROUP BY PL.CD_COMPANY, PL.NO_GIR, PL.NO_PACK, PL.NO_FILE) PL
    ON PL.CD_COMPANY = PH.CD_COMPANY AND PL.NO_GIR = PH.NO_GIR AND PL.NO_PACK = PH.NO_PACK
	LEFT JOIN (SELECT A.CD_COMPANY, A.NO_GIR,
                      STRING_AGG(A.NM_EMAIL, ';') AS TO_EMAIL 
               FROM (SELECT AM.CD_COMPANY, AM.NO_GIR, FP.NM_EMAIL
                     FROM CZ_SA_GIR_AUTO_MAIL_PTR AM
                     JOIN FI_PARTNERPTR FP ON FP.CD_COMPANY = AM.CD_COMPANY AND FP.CD_PARTNER = AM.CD_PARTNER AND FP.SEQ = AM.SEQ
                     UNION ALL
                     SELECT AM.CD_COMPANY, AM.NO_GIR, AM.DC_ETC AS NM_EMAIL
                     FROM CZ_SA_GIR_AUTO_MAIL_PTR AM
                     WHERE AM.SEQ < 0) A
               GROUP BY A.CD_COMPANY, A.NO_GIR) AM
    ON AM.CD_COMPANY = PH.CD_COMPANY AND AM.NO_GIR = PH.NO_GIR
	LEFT JOIN (SELECT MV.CD_COMPANY, MV.CD_PARTNER, MV.NO_IMO,
	                  STRING_AGG(FP.NM_EMAIL, ';') AS TO_EMAIL
	           FROM CZ_SA_AUTO_MAIL_VESSEL MV
	           JOIN FI_PARTNERPTR FP ON FP.CD_COMPANY = MV.CD_COMPANY AND FP.CD_PARTNER = MV.CD_PARTNER AND FP.SEQ = MV.SEQ
	           WHERE ISNULL(YN_PACK, 'N') = 'Y'
	           GROUP BY MV.CD_COMPANY, MV.CD_PARTNER, MV.NO_IMO) MV
	ON MV.CD_COMPANY = GH.CD_COMPANY AND MV.CD_PARTNER = GH.CD_PARTNER AND MV.NO_IMO = PD.NO_IMO
    WHERE PH.CD_COMPANY = @P_CD_COMPANY
	AND LEFT(ISNULL(PH.DTS_UPDATE, PH.DTS_INSERT), 8) = @P_DT_PACK
	AND (ISNULL(GH.CD_PARTNER, '') <> '01591' OR (ISNULL(GH.CD_PARTNER, '') = '01591' AND PH.CD_TYPE = '003'))
	AND (PD.CD_PACK_CATEGORY <> '001' OR PD.CD_SUB_CATEGORY <> '007')
    AND GH.STA_GIR = 'C'
	AND LEFT(PL.NO_FILE, 2) NOT IN ('HB')
	AND NOT EXISTS (SELECT 1 
                    FROM CZ_SA_GIR_AUTO_MAIL_SETTING ST 
                    WHERE ST.CD_COMPANY = PH.CD_COMPANY 
                    AND ST.NO_GIR = PH.NO_GIR
                    AND ST.TP_SEND = '002')
    GROUP BY PH.CD_COMPANY, PL.NO_FILE, GH.CD_PARTNER, (ISNULL(AM.TO_EMAIL, '') + ';' + ISNULL(MV.TO_EMAIL, ''))
)
SELECT A.NO_FILE,
	   SH.NO_PO_PARTNER,
       MP.LN_PARTNER,
       MH.NM_VESSEL,
       ISNULL(ME1.NO_EMAIL, '') + '|DINTEC' AS FROM_EMAIL,
	   A.TO_EMAIL,
	   ((CASE WHEN ISNULL(ME.NO_EMAIL, '') <> '' THEN ISNULL(ME.NO_EMAIL, '') + ';' ELSE '' END) + 
        (CASE WHEN ISNULL(ME1.NO_EMAIL, '') <> '' THEN ISNULL(ME1.NO_EMAIL, '') + ';' ELSE '' END) +
        (CASE WHEN SH.CD_PARTNER = '01287' THEN 'jinsol.kim@dintec.co.kr;' ELSE '' END)) AS CC_EMAIL,
	   'N' AS YN_SEND,
	   '' AS DC_RMK
FROM A
LEFT JOIN SA_SOH SH ON SH.CD_COMPANY = A.CD_COMPANY AND SH.NO_SO = A.NO_FILE
LEFT JOIN MA_PARTNER MP ON MP.CD_COMPANY = SH.CD_COMPANY AND MP.CD_PARTNER = SH.CD_PARTNER
LEFT JOIN CZ_MA_HULL MH ON MH.NO_IMO = SH.NO_IMO
LEFT JOIN V_CZ_SA_QTN_LOG_EMP VE ON VE.CD_COMPANY = SH.CD_COMPANY AND VE.NO_FILE = SH.NO_SO
LEFT JOIN MA_EMP ME ON ME.CD_COMPANY = VE.CD_COMPANY AND ME.NO_EMP = VE.CD_FLAG1
LEFT JOIN MA_EMP ME1 ON ME1.CD_COMPANY = VE.CD_COMPANY AND ME1.NO_EMP = VE.CD_FLAG2";

				포장일자 = Global.MainFrame.GetDateTimeToday().AddDays(-1).ToString("yyyyMMdd");

				dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 포장일자));

				filePath = Path.Combine(Application.StartupPath, "temp") + "\\";

				if (Directory.Exists(filePath))
				{
					string[] files = Directory.GetFiles(filePath);

					foreach (string file in files)
						File.Delete(file);
				}
				else
					Directory.CreateDirectory(filePath);

				dt1 = ComFunc.getGridGroupBy(dt, new string[] { "LN_PARTNER", "FROM_EMAIL", "TO_EMAIL", "CC_EMAIL" }, true);

				MsgControl.ShowMsg("CZ_처리중입니다. 잠시만 기다려주세요.");

				foreach (DataRow dr in dt1.Rows)
				{
					dataRowArray = dt.Select(string.Format("LN_PARTNER= '{0}' AND FROM_EMAIL = '{1}' AND TO_EMAIL = '{2}' AND CC_EMAIL = '{3}'", dr["LN_PARTNER"].ToString(),
																																				 dr["FROM_EMAIL"].ToString(),
																																				 dr["TO_EMAIL"].ToString(),
																																				 dr["CC_EMAIL"].ToString()));

					보내는사람 = dr["FROM_EMAIL"].ToString();
					받는사람 = dr["TO_EMAIL"].ToString();
					참조 = dr["CC_EMAIL"].ToString();

					html = @"<div style='text-align:left; font-size: 10pt; font-family: 맑은 고딕;'>
To: {0}<br><br>
Thank you again for your orders.<br><br>
Kindly refer to the attached for the CIPL, and below for their respective packing details and <span style='font-style:italic; background-color:#FFFF00;'>charges incurred</span> (if any), and remarks for your kind perusal.<br><br>
Please do provide instructions if not yet already given.<br><br>
</div>

<table style='width:1950px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='150px' align='center'></colgroup>
	<colgroup width='150px' align='center'></colgroup>
	<colgroup width='150px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='150px' align='center'></colgroup>
	<colgroup width='150px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>

	<tbody>
		<tr style='height:30px'>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>Vessel</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>Order No.</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:150px'>Dintec No.</th>                                    
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:150px'>Date</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:150px'>Style</th>                                    
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>Size</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:150px'>Gross Weight</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:150px'>Packing charge</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>Status</th>
		    <th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>Remarks</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>Photo</th>
		</tr>
	{1}
	</tbody>
</table>

<div style='text-align:left; font-size: 10pt; font-family: 맑은 고딕;'>
<br>
<span style='font-weight: bold;'>
Warehouse address for reference:<br> 
C/O DINTEC CO., LTD. (WAREHOUSE)<br>
2nd floor, 48, Eogokgongdan 2-gil, Yangsan-si, Gyeongsangnam-do, Republic of Korea / ZIP CODE: 50591<br>
Operating hours: 9:30am ~ 17:00pm (Lunch break: 12~13pm)
</span>
<br><br>
Thank you
</div>";
					fileList = new List<string>();
					body = string.Empty;
					packList = new List<string>();

					foreach (DataRow dr1 in dataRowArray)
					{
						dt2 = DBHelper.GetDataTable("SP_CZ_SA_AUTO_MAIL_READY_PACK", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																									포장일자,
																									포장일자,
																									dr1["NO_FILE"].ToString() });

						foreach (DataRow dr2 in dt2.Rows)
						{
							string 포장사진 = string.Empty;

							int index1 = 1;

							foreach (string url in dr2["DC_URL"].ToString().Split(','))
							{
								if (string.IsNullOrEmpty(url))
									continue;

								포장사진 += "<a href='" + url + "' style='color:#0000ff; text-decoration:underline;' target='_blank'>Photo" + index1.ToString() + "</a><br>";
								index1++;
							}

							if (!packList.Contains(dr2["NO_GIR"].ToString() + "_" + dr2["NO_PACK"].ToString()))
								packList.Add(dr2["NO_GIR"].ToString() + "_" + dr2["NO_PACK"].ToString());

							int packIndex = packList.IndexOf(dr2["NO_GIR"].ToString() + "_" + dr2["NO_PACK"].ToString());

							body += string.Format(@"<tr style='height: 30px'>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{0}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{1}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:150px'>{2}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:150px'>{3}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:150px'>{4}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{5}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:150px'>{6}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:150px'>{7}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{8}</th>
														<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{9}</th>
													    <th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{10}</th>
													</tr>", dr2["NM_VESSEL"].ToString(),
															dr2["NO_PO_PARTNER"].ToString(),
															dr2["NO_SO"].ToString(),
															Util.GetTo_DateStringS(dr2["DT_PACK"].ToString()),
															dr2["NM_TYPE"].ToString(),
															dr2["DC_SIZE"].ToString(),
															dr2["GROSS_WEIGHT"].ToString(),
															dr2["AM_WOODEN"].ToString(),
															dr2["DC_STATUS"].ToString(),
															"PAKAGE NO. : "+ (packIndex + 1).ToString() + " " +
															(dr2["AM_WOODEN"].ToString() != string.Empty ? "<span style='background-color:#FFFF00;'>Kindly confirm charges/ provide feedback.</span>" : string.Empty),
															포장사진);
						}

						index = 0;
						foreach (DataRow dr2 in ComFunc.getGridGroupBy(dt2, new string[] { "NO_GIR", "NO_PO_PARTNER" }, true).Rows)
						{
							if (index == 0)
								fileName = Regex.Replace(dr2["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty);
							else
								fileName = Regex.Replace(dr2["NO_PO_PARTNER"].ToString(), "[\\/:*?\"<>|]", string.Empty) + "_" + index.ToString();

							dtH = new DataTable();
							dtL = new DataTable();
							tmpList = new List<string>();

							#region 상업송장
							this.상업송장데이터(Global.MainFrame.LoginInfo.CompanyCode, new DataRow[] { dr2 }, ref dtH, ref dtL);

							if (dtH.Rows.Count != 0)
							{
								fullPath = this.GetUniqueFileName(filePath + fileName + "_CI.pdf");

								reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_2", "납품의뢰현황-상업송장", Global.MainFrame.LoginInfo.CompanyCode, dtH, dtL);

								reportHelper.SetDataTable(dtH, 1);
								reportHelper.SetDataTable(dtL, 2);
								reportHelper.PrintHelper.UseUserFontStyle();

								if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
									reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_K100_CI.DRF", false, true, fullPath, new Dictionary<string, string>());
								else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
									reportHelper.PrintDirect("R_CZ_SA_GIRSCH_2_K200_CI.DRF", false, true, fullPath, new Dictionary<string, string>());

								tmpList.Add(fullPath);
							}
							#endregion

							#region 포장명세서
							this.포장명세서데이터(Global.MainFrame.LoginInfo.CompanyCode, new DataRow[] { dr2 }, ref dtH, ref dtL);

							if (dtH.Rows.Count != 0)
							{
								fullPath = this.GetUniqueFileName(filePath + fileName + "_PL.pdf");

								reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_1", "납품의뢰현황-포장명세서", Global.MainFrame.LoginInfo.CompanyCode, dtH, dtL);
								reportHelper.SetDataTable(dtH, 1);
								reportHelper.SetDataTable(dtL, 2);
								reportHelper.PrintHelper.UseUserFontStyle();

								if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
									reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_K100_PL.DRF", false, true, fullPath, new Dictionary<string, string>());
								else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
									reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_K200_PL.DRF", false, true, fullPath, new Dictionary<string, string>());

								tmpList.Add(fullPath);
							}
							#endregion

							fullPath = this.GetUniqueFileName(filePath + fileName + ".pdf");

							PDF.Merge(fullPath, tmpList.ToArray());

							fileList.Add(fullPath);

							index++;
						}
					}

					if (string.IsNullOrEmpty(body))
					{
						foreach (DataRow dr1 in dataRowArray)
						{
							dr1["YN_SEND"] = "N";
							dr1["DC_RMK"] = "발송 대상건 없음";
						}

						continue;
					}

					html = string.Format(html, dr["LN_PARTNER"].ToString(), body);
					제목 = "[DINTEC] PACKING COMPLETION NOTICE" + " / " + dataRowArray[0]["NO_FILE"].ToString() + " / " + dataRowArray[0]["NO_PO_PARTNER"].ToString();

					if (string.IsNullOrEmpty(보내는사람.Replace("|DINTEC", "")) && string.IsNullOrEmpty(받는사람))
					{
						발송불가사유 = "수/발신자 모두 없음";
						this.메일발송("wslee@dintec.co.kr", "wslee@dintec.co.kr", string.Empty, string.Empty, 제목 + "- 메일 발송 실패 알림 (수/발신자 모두 없음)", string.Empty, html, fileList);
					}
					else if (string.IsNullOrEmpty(보내는사람.Replace("|DINTEC", "")))
					{
						발송불가사유 = "발신자 없음";
						this.메일발송("wslee@dintec.co.kr", "wslee@dintec.co.kr", string.Empty, string.Empty, 제목 + "- 메일 발송 실패 알림 (발신자 없음, 수신자 : " + 받는사람 + ")", string.Empty, html, fileList);
					}
					else if (string.IsNullOrEmpty(받는사람))
					{
						발송불가사유 = "수신자 없음";
						this.메일발송(보내는사람, 참조, string.Empty, string.Empty, 제목 + "- 메일 발송 실패 알림 (수신자 없음)", string.Empty, html, fileList);
					}
					else
					{
						발송불가사유 = string.Empty;
					}

					if (!string.IsNullOrEmpty(발송불가사유))
					{
						foreach (DataRow dr1 in dataRowArray)
						{
							dr1["YN_SEND"] = "N";
							dr1["DC_RMK"] = 발송불가사유;
						}

						continue;
					}

					if (this.메일발송(보내는사람, 받는사람, 참조, string.Empty, 제목, string.Empty, html, fileList))
					{
						foreach (DataRow dr1 in dataRowArray)
						{
							dr1["YN_SEND"] = "Y";
						}
					}
					else
					{
						this.메일발송(보내는사람, 참조, string.Empty, string.Empty, 제목 + "- 메일 발송 실패 알림 (수신자 : " + 받는사람 + ")", string.Empty, html, fileList);

						foreach (DataRow dr1 in dataRowArray)
						{
							dr1["YN_SEND"] = "N";
							dr1["DC_RMK"] = "메일 발송 실패";
						}
					}
				}

				html = @"<table style='width:600px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
    <colgroup width='200px' align='center'></colgroup>
	<tbody>
		<tr style='height:30px'>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>파일번호</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>발송여부</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>비고</th>
		</tr>
	{0}
	</tbody>
</table>";
				body = string.Empty;
				foreach (DataRow dr in dt.Rows)
				{
					DBHelper.ExecuteScalar(string.Format(@"INSERT INTO CZ_SA_AUTO_MAIL_PACK_LOG
(
    CD_COMPANY,
	DT_SEND,
	YN_SEND,
	NO_SO,
	DC_RMK,
	ID_INSERT,
	DTS_INSERT
)
VALUES
(
    '{0}',
    CONVERT(CHAR(8), GETDATE(), 112),
    '{1}',
    '{2}',
    '{3}',
    '{4}',
    NEOE.SF_SYSDATE(GETDATE())
)", Global.MainFrame.LoginInfo.CompanyCode,
	dr["YN_SEND"].ToString(),
	dr["NO_FILE"].ToString(),
	dr["DC_RMK"].ToString(),
	Global.MainFrame.LoginInfo.UserID));

					body += string.Format(@"<tr style='height: 30px'>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{0}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{1}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{2}</th>
											</tr>", dr["NO_FILE"].ToString(),
													dr["YN_SEND"].ToString(),
													dr["DC_RMK"].ToString());
				}

				html = string.Format(html, body);

				보내는사람 = "khkim@dintec.co.kr";
				받는사람 = "wslee@dintec.co.kr;khkim@dintec.co.kr";

				제목 = "포장명세서 메일 발송 결과 (매출처발송)";
				fileList = new List<string>();

				this.메일발송(보내는사람, 받는사람, string.Empty, string.Empty, 제목, string.Empty, html, fileList);
				#endregion

				#region 포워더발송
				query = @";WITH A AS
(
    SELECT GH.CD_COMPANY,
		   GH.NO_GIR,
           GL.NO_SO, 
           (CASE ST.TP_TRANS WHEN '001' THEN '항송'
                             WHEN '002' THEN '해송'
                             WHEN '003' THEN 'COURIER'
                             WHEN '004' THEN '항송&해송' 
                             ELSE NULL END) AS NM_TRANS,
           (CASE WHEN ST.YN_DHL = 'Y' THEN 'hyunmin.park@dhl.com' ELSE NULL END) AS DC_EMAIL_DHL,
           (CASE WHEN ST.YN_FEDEX = 'Y' THEN 'qd.kr@fedex.com' ELSE NULL END) AS DC_EMAIL_FEDEX,
           (CASE WHEN ST.YN_TPO = 'Y' THEN 'tpodtc@tpolog.com' ELSE NULL END) AS DC_EMAIL_TPO,
           (CASE WHEN ST.YN_SK = 'Y' AND ST.TP_TRANS = '001' THEN 'mps.korea@dbschenker.com;helen.kim@dbschenker.com' 
                 WHEN ST.YN_SK = 'Y' AND ST.TP_TRANS = '002' THEN 'Jimena.Kim@dbschenker.com;Richard.Namkoong@dbschenker.com'
                 WHEN ST.YN_SK = 'Y' AND ST.TP_TRANS = '004' THEN 'mps.korea@dbschenker.com;helen.kim@dbschenker.com;Jimena.Kim@dbschenker.com;Richard.Namkoong@dbschenker.com'
                 ELSE NULL END) AS DC_EMAIL_SK,
           (CASE WHEN ST.YN_SR = 'Y' AND (ST.TP_TRANS = '001' OR ST.TP_TRANS = '004') THEN 'air@srlo.co.kr' 
                 WHEN ST.YN_SR = 'Y' AND (ST.TP_TRANS = '002' OR ST.TP_TRANS = '004') THEN 'sea_op2@srlo.co.kr'
                 ELSE NULL END) AS DC_EMAIL_SR,
           (CASE WHEN ST.YN_ETC = 'Y' THEN ST.DC_EMAIL_ETC ELSE NULL END) AS DC_EMAIL_ETC,
           MC.NM_SYSDEF AS NM_COUNTRY_DHL,
           MC1.NM_SYSDEF AS NM_COUNTRY_FEDEX,
           ST.DC_COUNTRY_ETC,
           ST.DC_CONSIGNEE,
           ME.NO_EMAIL,
           MH.NM_VESSEL
    FROM CZ_SA_GIRH_PACK GH WITH(NOLOCK)
    JOIN CZ_SA_GIRH_PACK_DETAIL PD WITH(NOLOCK) ON PD.CD_COMPANY = GH.CD_COMPANY AND PD.NO_GIR = GH.NO_GIR
    JOIN CZ_SA_GIR_AUTO_MAIL_SETTING ST WITH(NOLOCK) ON ST.CD_COMPANY = GH.CD_COMPANY AND ST.NO_GIR = GH.NO_GIR
    LEFT JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = GH.CD_COMPANY AND ME.NO_EMP = GH.NO_EMP
    LEFT JOIN CZ_MA_HULL MH WITH(NOLOCK) ON MH.NO_IMO = PD.NO_IMO
    LEFT JOIN CZ_MA_CODEDTL MC WITH(NOLOCK) ON MC.CD_COMPANY = ST.CD_COMPANY AND MC.CD_FIELD = 'CZ_SA00054' AND MC.CD_SYSDEF = ST.CD_COUNTRY_DHL
    LEFT JOIN CZ_MA_CODEDTL MC1 WITH(NOLOCK) ON MC1.CD_COMPANY = ST.CD_COMPANY AND MC1.CD_FIELD = 'CZ_SA00055' AND MC1.CD_SYSDEF = ST.CD_COUNTRY_FEDEX
    LEFT JOIN (SELECT GL.CD_COMPANY, GL.NO_GIR,
                      MAX(GL.NO_SO) AS NO_SO 
               FROM CZ_SA_GIRL_PACK GL WITH(NOLOCK)
               GROUP BY GL.CD_COMPANY, GL.NO_GIR) GL
    ON GL.CD_COMPANY = GH.CD_COMPANY AND GL.NO_GIR = GH.NO_GIR
    WHERE GH.CD_COMPANY = '{0}'
    AND (PD.CD_PACK_CATEGORY <> '001' OR PD.CD_SUB_CATEGORY <> '007')
    AND GH.STA_GIR = 'C'
    AND ST.TP_SEND = '002'
    AND EXISTS (SELECT 1 
                FROM CZ_SA_PACKH PH WITH(NOLOCK)
                WHERE PH.CD_COMPANY = GH.CD_COMPANY
                AND PH.NO_GIR = GH.NO_GIR
                AND LEFT(ISNULL(PH.DTS_UPDATE, PH.DTS_INSERT), 8) = '{1}')

)
SELECT UN.CD_COMPANY,
	   UN.NO_GIR,
       UN.NO_SO,
       UN.NM_VESSEL,
       UN.NM_TRANS,
       UN.NM_COUNTRY_DHL,
       UN.NM_COUNTRY_FEDEX,
       UN.DC_COUNTRY_ETC,
       UN.DC_CONSIGNEE,
       UN.FW,
       UN.NO_EMAIL AS FROM_EMAIL,
       UN.EMAIL AS TO_EMAIL,
	   'N' AS YN_SEND
FROM (SELECT A.CD_COMPANY,
			 A.NO_GIR,
             A.NO_SO,
             A.NM_TRANS,
             A.NM_COUNTRY_DHL,
             A.NM_COUNTRY_FEDEX,
             A.DC_COUNTRY_ETC,
             A.DC_CONSIGNEE,
             A.NO_EMAIL,
             A.NM_VESSEL,
			 CAST(A.DC_EMAIL_DHL AS SQL_VARIANT) AS DC_EMAIL_DHL,
             CAST(A.DC_EMAIL_FEDEX AS SQL_VARIANT) AS DC_EMAIL_FEDEX,
			 CAST(A.DC_EMAIL_TPO AS SQL_VARIANT) AS DC_EMAIL_TPO,
             CAST(A.DC_EMAIL_SK AS SQL_VARIANT) AS DC_EMAIL_SK,
             CAST(A.DC_EMAIL_SR AS SQL_VARIANT) AS DC_EMAIL_SR,
             CAST(A.DC_EMAIL_ETC AS SQL_VARIANT) AS DC_EMAIL_ETC
	  FROM A) AS A
UNPIVOT(EMAIL FOR FW IN ([DC_EMAIL_DHL], [DC_EMAIL_FEDEX], [DC_EMAIL_TPO], [DC_EMAIL_SK], [DC_EMAIL_SR], [DC_EMAIL_ETC])) AS UN";

				포장일자 = Global.MainFrame.GetDateTimeToday().AddDays(-1).ToString("yyyyMMdd");

				dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, 포장일자));

				foreach (DataRow dr in dt.Rows)
				{
					switch (dr["FW"].ToString())
					{
						case "DC_EMAIL_DHL":
							제목 = "수출 특가 문의 / " + dr["NO_SO"].ToString();
							html = @"<div style='text-align:left; font-size: 10pt; font-family: 맑은 고딕;'>
업무에 노고가 많으십니다.<br><br>
한국에서 {0}까지 수출 운임 특가 견적 부탁 드립니다.<br><br> 
픽업지: 경남양산시어곡동871-12번지(경일창고2층)<br><br>
목적지 : {0}<br><br>" +
(!string.IsNullOrEmpty(dr["DC_CONSIGNEE"].ToString()) ? "{1}<br><br>" : "{1}")
+ @"감사합니다.
</div>";
							html = string.Format(html, dr["NM_COUNTRY_DHL"].ToString(), dr["DC_CONSIGNEE"].ToString().Replace(Environment.NewLine, "<br>"));
							break;
						case "DC_EMAIL_FEDEX":
							제목 = "수출 특가 문의 / " + dr["NO_SO"].ToString();
							html = @"<div style='text-align:left; font-size: 10pt; font-family: 맑은 고딕;'>
업무에 노고가 많으십니다.<br><br>
한국에서 {0}까지 수출 운임 특가 견적 부탁 드립니다.<br><br>
<span style='font-weight:bold;text-decoration:underline;'>FEDEX : a/c 226715827</span><br><br>
픽업지: 경남양산시어곡동871-12번지(경일창고2층)<br><br>
목적지 : {0}<br><br>" +
(!string.IsNullOrEmpty(dr["DC_CONSIGNEE"].ToString()) ? "{1}<br><br>" : "{1}")
+ @"감사합니다.
</div>";
							html = string.Format(html, dr["NM_COUNTRY_FEDEX"].ToString(), dr["DC_CONSIGNEE"].ToString().Replace(Environment.NewLine, "<br>"));
							break;
						default:
							제목 = "[" + dr["NM_TRANS"].ToString() + " 견적&도착일정 문의]  " + dr["NO_SO"].ToString() + ", " + dr["NM_VESSEL"].ToString();
							html = @"<div style='text-align:left; font-size: 10pt; font-family: 맑은 고딕;'>
안녕하십니까 ?<br><br>
{0} 까지 {1} 운임 견적과 도착일정 문의 드립니다.<br>
위험품일시, <span style='background-color: #FFFF00;'>유첨 MSDS 참조 부탁드립니다.</span><br><br>" +
(!string.IsNullOrEmpty(dr["DC_CONSIGNEE"].ToString()) ? "{2}<br><br>" : "{2}")
+ @"무게 사이즈 첨부 파일 참조 부탁 드립니다.<br><br>
감사합니다!
</div>";
							html = string.Format(html, dr["DC_COUNTRY_ETC"].ToString(), dr["NM_TRANS"].ToString(), dr["DC_CONSIGNEE"].ToString().Replace(Environment.NewLine, "<br>"));
							break;
					}

					보내는사람 = dr["FROM_EMAIL"].ToString();
					받는사람 = dr["TO_EMAIL"].ToString();
					참조 = dr["FROM_EMAIL"].ToString();

					dtH = new DataTable();
					dtL = new DataTable();
					fileList = new List<string>();

					#region 포장명세서
					this.포장명세서데이터(Global.MainFrame.LoginInfo.CompanyCode, new DataRow[] { dr }, ref dtH, ref dtL);

					if (dtH.Rows.Count != 0)
					{
						fullPath = this.GetUniqueFileName(filePath + dr["NO_GIR"].ToString() + "_PL.pdf");

						reportHelper = Util.SetRPT("R_CZ_SA_GIRSCH_1", "납품의뢰현황-포장명세서", Global.MainFrame.LoginInfo.CompanyCode, dtH, dtL);
						reportHelper.SetDataTable(dtH, 1);
						reportHelper.SetDataTable(dtL, 2);
						reportHelper.PrintHelper.UseUserFontStyle();

						if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
							reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_K100_PL.DRF", false, true, fullPath, new Dictionary<string, string>());
						else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
							reportHelper.PrintDirect("R_CZ_SA_GIRSCH_1_K200_PL.DRF", false, true, fullPath, new Dictionary<string, string>());

						fileList.Add(fullPath);
					}
					#endregion

					#region 첨부파일
					query = @"SELECT MF.FILE_NAME,
	   MF.FILE_PATH,
	   MF.CD_FILE 
FROM MA_FILEINFO MF WITH(NOLOCK)
WHERE MF.CD_COMPANY = '{0}'
AND MF.CD_MODULE = 'SA' 
AND MF.ID_MENU = 'P_CZ_SA_GIR'
AND MF.CD_FILE = '{1}'";

					dt1 = DBHelper.GetDataTable(string.Format(query, dr["CD_COMPANY"].ToString(), dr["NO_GIR"].ToString()));

					foreach (DataRow dr1 in dt1.Rows)
					{
						FileUploader.DownloadFile(dr1["FILE_NAME"].ToString(), filePath, dr1["FILE_PATH"].ToString(), dr1["CD_FILE"].ToString());
						fileList.Add(filePath + dr1["FILE_NAME"].ToString());
					}
					#endregion

					if (this.메일발송(보내는사람, 받는사람, string.Empty, 참조, 제목, string.Empty, html, fileList))
						dr["YN_SEND"] = "Y";
					else
						this.메일발송(보내는사람, 참조, string.Empty, string.Empty, 제목 + "- 메일 발송 실패 알림", string.Empty, html, fileList);
				}

				html = @"<table style='width:600px; border:2px solid black; font-size: 10pt; font-family: 맑은 고딕; border-collapse:collapse; border-spacing:0;'>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<colgroup width='200px' align='center'></colgroup>
	<tbody>
		<tr style='height:30px'>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>의뢰번호</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>받는사람</th>
			<th style='border:solid 1px black; text-align:center; background-color:#92d050; width:200px'>발송여부</th>
		</tr>
	{0}
	</tbody>
</table>";
				body = string.Empty;
				foreach (DataRow dr in dt.Rows)
				{
					body += string.Format(@"<tr style='height: 30px'>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{0}</th>
												<th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{1}</th>
											    <th style='border:solid 1px black; text-align:center; font-weight:normal; width:200px'>{2}</th>
											</tr>", dr["NO_GIR"].ToString(),
													dr["TO_EMAIL"].ToString(),
													dr["YN_SEND"].ToString());
				}

				html = string.Format(html, body);

				보내는사람 = "khkim@dintec.co.kr";
				받는사람 = "wslee@dintec.co.kr;khkim@dintec.co.kr";

				제목 = "포장명세서 메일 발송 결과 (포워더발송)";
				fileList = new List<string>();

				this.메일발송(보내는사람, 받는사람, string.Empty, string.Empty, 제목, string.Empty, html, fileList);
				#endregion

				MsgControl.CloseMsg();

				this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn포장메일발송.Text);
			}
			catch (Exception ex)
			{
				Messenger.SendMSG(new string[] { "S-391" }, string.Format(@"*** 포장메일발송 오류 알림
- 오류메시지 : {0}", ex.Message));
				MsgControl.CloseMsg();
				this.MsgEnd(ex);
			}
			finally
			{
				MsgControl.CloseMsg();
			}
		}

		private void Cbo분류1_SelectionChangeCommitted(object sender, EventArgs e)
		{
			try
			{
				string query = @"SELECT '' AS CODE, 
       '' AS NAME
UNION ALL
SELECT CD_SYSDEF AS CODE,
       NM_SYSDEF AS NAME
FROM CZ_MA_CODEDTL WITH(NOLOCK)
WHERE CD_COMPANY = 'K100'
AND CD_FIELD = 'CZ_SA00060'
AND CD_SYSDEF <> '000'
AND ISNULL(CD_FLAG1, '') <> ''
AND CD_FLAG1 = '{0}'";

				this.cbo분류2.DataSource = DBHelper.GetDataTable(string.Format(query, new string[] { this.cbo분류1.SelectedValue.ToString() }));
				this.cbo분류2.DisplayMember = "NAME";
				this.cbo분류2.ValueMember = "CODE";
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn메일업로드_Click(object sender, EventArgs e)
		{
			DataTable dt;
			string[] stringArray, stringArray1, stringArray2;
			string 발주번호, 납기일자, 입고예정일, 매입처비고, 수주번호, query;

			try
			{
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.Multiselect = true;
				dialog.RestoreDirectory = true;

				if (dialog.ShowDialog() != DialogResult.OK) return;

				string[] fileNames = dialog.FileNames;

				dt = new DataTable();

				dt.Columns.Add("IDX");
				dt.Columns.Add("NO_SO");
				dt.Columns.Add("NO_PO");
				dt.Columns.Add("DT_LIMIT");
				dt.Columns.Add("DT_EXPECT");
				dt.Columns.Add("DT_REPLY");
				dt.Columns.Add("DC_RMK");

				foreach (string fileName in fileNames)
				{
					MapiMessage msg = MapiMessage.FromMailMessage(fileName);

					stringArray = msg.BodyHtml.Split(new string[] { "<table" }, StringSplitOptions.None);

					int index = 1;

					foreach (string table in stringArray)
					{
						if (!table.StartsWith(" class=\"MsoNormalTable\""))
							continue;

						stringArray1 = table.Split(new string[] { "<tr" }, StringSplitOptions.None);

						foreach (string row in stringArray1)
						{
							if (!row.StartsWith(" style=\"height:22.5pt\">"))
								continue;

							stringArray2 = row.Split(new string[] { "<td" }, StringSplitOptions.None);

							if (stringArray2.Length == 10)
							{
								#region 해외
								발주번호 = this.RemoveHtmlTag("<td" + stringArray2[1].Split(new string[] { "td>" }, StringSplitOptions.None)[0].ToString() + "td>");

								if (발주번호 == "OUR REF NO.")
									continue;

								납기일자 = Regex.Replace(this.RemoveHtmlTag("<td" + stringArray2[6].Split(new string[] { "td>" }, StringSplitOptions.None)[0].ToString() + "td>"), "[^0-9]", string.Empty);
								입고예정일 = Regex.Replace(this.RemoveHtmlTag("<td" + stringArray2[7].Split(new string[] { "td>" }, StringSplitOptions.None)[0].ToString() + "td>"), "[^0-9]", string.Empty);
								매입처비고 = this.RemoveHtmlTag("<td" + stringArray2[9].Split(new string[] { "td>" }, StringSplitOptions.None)[0].ToString() + "td>");

								if (납기일자.Length != 8 && 입고예정일.Length != 8)
								{
									this.ShowMessage(string.Format("날짜 형식에 맞지 않는 데이터가 있습니다. 납기일자 : {0}, 입고예정일 : {1}", 납기일자, 입고예정일));
									continue;
								}

								query = @"SELECT PH.CD_PJT 
										  FROM PU_POH PH WITH(NOLOCK)
										  WHERE PH.CD_COMPANY = '{0}'
										  AND PH.NO_PO LIKE '{1}%'";

								DataTable dt1 = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																										  발주번호 }));

								if (dt1 == null || dt1.Rows.Count == 0)
								{
									this.ShowMessage(string.Format("발주번호가 잘못 되었습니다. {0}", 발주번호));
									continue;
								}
								else
								{
									수주번호 = dt1.Rows[0]["CD_PJT"].ToString();
								}

								DataRow dr = dt.NewRow();

								dr["IDX"] = index;
								dr["NO_SO"] = 수주번호;
								dr["NO_PO"] = 발주번호;
								dr["DT_LIMIT"] = 납기일자;
								dr["DT_EXPECT"] = 입고예정일;
								dr["DC_RMK"] = 매입처비고;

								dt.Rows.Add(dr);
								#endregion
							}
							else if (stringArray2.Length == 11)
							{
								#region 국내
								발주번호 = this.RemoveHtmlTag("<td" + stringArray2[2].Split(new string[] { "td>" }, StringSplitOptions.None)[0].ToString() + "td>");

								if (발주번호 == "딘텍 Ref No.")
									continue;

								납기일자 = Regex.Replace(this.RemoveHtmlTag("<td" + stringArray2[7].Split(new string[] { "td>" }, StringSplitOptions.None)[0].ToString() + "td>"), "[^0-9]", string.Empty);
								입고예정일 = Regex.Replace(this.RemoveHtmlTag("<td" + stringArray2[8].Split(new string[] { "td>" }, StringSplitOptions.None)[0].ToString() + "td>"), "[^0-9]", string.Empty);
								매입처비고 = this.RemoveHtmlTag("<td" + stringArray2[10].Split(new string[] { "td>" }, StringSplitOptions.None)[0].ToString() + "td>");

								if (납기일자.Length != 8 && 입고예정일.Length != 8)
								{
									this.ShowMessage(string.Format("날짜 형식에 맞지 않는 데이터가 있습니다. 납기일자 : {0}, 입고예정일 : {1}", 납기일자, 입고예정일));
									continue;
								}

								query = @"SELECT PH.CD_PJT 
										  FROM PU_POH PH WITH(NOLOCK)
										  WHERE PH.CD_COMPANY = '{0}'
										  AND PH.NO_PO LIKE '{1}%'";

								DataTable dt1 = DBHelper.GetDataTable(string.Format(query, new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																										  발주번호 }));

								if (dt1 == null || dt1.Rows.Count == 0)
								{
									this.ShowMessage(string.Format("발주번호가 잘못 되었습니다. {0}", 발주번호));
									continue;
								}
								else
								{
									수주번호 = dt1.Rows[0]["CD_PJT"].ToString();
								}

								DataRow dr = dt.NewRow();

								dr["IDX"] = index;
								dr["NO_SO"] = 수주번호;
								dr["NO_PO"] = 발주번호;
								dr["DT_LIMIT"] = 납기일자;
								dr["DT_EXPECT"] = 입고예정일;
								dr["DC_RMK"] = 매입처비고;

								dt.Rows.Add(dr);
								#endregion
							}
						}

						index++;
					}
				}

				int first = D.GetInt(dt.Compute("MIN(IDX)", string.Empty));

				foreach (DataRow dr in dt.Select(string.Format("IDX = '{0}'", first.ToString())))
				{
					DBHelper.ExecuteNonQuery("SP_CZ_SA_AUTO_MAIL_DD_I", new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																					   "2",
																					   dr["NO_SO"].ToString(),
																					   dr["NO_PO"].ToString(),
																					   dr["DT_LIMIT"].ToString(),
																					   dr["DT_EXPECT"].ToString(),
																					   dr["DT_REPLY	"].ToString(),
																					   string.Empty,
																					   string.Empty,
																					   dr["DC_RMK"].ToString(),
																					   string.Empty,
																					   string.Empty,
																					   Global.MainFrame.LoginInfo.UserID });
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Bpc납품처_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P00_CHILD_MODE = "납품처";
				e.HelpParam.P61_CODE1 = "CD_PARTNER AS CODE, LN_PARTNER AS NAME";
				e.HelpParam.P62_CODE2 = "CZ_MA_DELIVERY";
				e.HelpParam.P63_CODE3 = string.Format("WHERE CD_COMPANY = '{0}' AND ISNULL(YN_USE, 'N') = 'Y' AND CD_PARTNER LIKE 'DLV%'", Global.MainFrame.LoginInfo.CompanyCode);
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.rdo매일.Checked)
				{
					this.chk월요일.Enabled = false;
					this.chk화요일.Enabled = false;
					this.chk수요일.Enabled = false;
					this.chk목요일.Enabled = false;
					this.chk금요일.Enabled = false;
				}
				else
				{
					this.chk월요일.Enabled = true;
					this.chk화요일.Enabled = true;
					this.chk수요일.Enabled = true;
					this.chk목요일.Enabled = true;
					this.chk금요일.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		public override void OnToolBarSearchButtonClicked(object sender, EventArgs e)
		{
			try
			{
				base.OnToolBarSearchButtonClicked(sender, e);

				if (this.tabControlExt1.SelectedTab == this.tpg매입처)
				{
					this._flex매입처.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			   this.ctx매입처S.CodeValue,
																			   this.txt발주번호.Text,
																			   this.ctx영업물류담당S.CodeValue,
																			   (this.chk발송대상만.Checked == true ? "Y" : "N"),
																			   (this.chk미입고존재.Checked == true ? "Y" : "N"),
																			   (this.chk발송대상존재.Checked == true ? "Y" : "N") });

					if (!this._flex매입처.HasNormalRow)
						this.ShowMessage(PageResultMode.SearchNoData);
				}
				else if (this.tabControlExt1.SelectedTab == this.tpg매출처)
				{
					this._flex매출처.Binding = this._biz.Search1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																			    this.ctx매출처S.CodeValue,
																		        this.txt수주번호S1.Text });

					if (!this._flex매출처.HasNormalRow)
						this.ShowMessage(PageResultMode.SearchNoData);
				}
				else if (this.tabControlExt1.SelectedTab == this.tpgPackInfo)
				{
					this._flexReadyPack.Binding = this._biz.SearchDetail3(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 this.dtp포장일자.StartDateToString,
																						 this.dtp포장일자.EndDateToString,
																						 this.txt수주번호.Text });

					if (!this._flexReadyPack.HasNormalRow)
						this.ShowMessage(PageResultMode.SearchNoData);
				}
				else if (this.tabControlExt1.SelectedTab == this.tpgOrderStatus)
				{
					this._flexOrderStatus.Binding = this._biz.SearchDetail1(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						   this.ctx매출처S3.CodeValue,
																						   this.txt수주번호S1.Text,
																						   this.bpc거래처그룹.QueryWhereIn_Pipe,
																						   (this.chk제외건만조회.Checked == true ? "Y" : "N") });

					if (!this._flexOrderStatus.HasNormalRow)
						this.ShowMessage(PageResultMode.SearchNoData);
				}
				else
				{
					this._flexReadyInfo.Binding = this._biz.SearchDetail2(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																						 this.ctx매출처S2.CodeValue,
																						 this.dtp준비일자.StartDateToString,
																						 this.dtp준비일자.EndDateToString,
																						 this.txt수주번호S.Text,
																					     (this.chk발송대상.Checked == true ? "Y" : "N"),
																						 this.cbo거래처그룹_RI.SelectedValue.ToString() });

					if (!this._flexReadyInfo.HasNormalRow)
						this.ShowMessage(PageResultMode.SearchNoData);
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

				if (this.tabControlExt1.SelectedTab == this.tpg매입처)
				{
					this._flex매입처.Rows.Add();
					this._flex매입처.Row = this._flex매입처.Rows.Count - 1;

					this._flex매입처["TP_PARTNER"] = "001";
					this._flex매입처["TP_PERIOD"] = "DAY";
					this._flex매입처["QT_LT_OVERDUE"] = -5;

					this._flex매입처.AddFinished();
					this._flex매입처.Focus();
				}
				else
				{
					this._flex매출처.Rows.Add();
					this._flex매출처.Row = this._flex매출처.Rows.Count - 1;

					this._flex매출처["TP_PARTNER"] = "002";
					this._flex매출처["TP_PERIOD"] = "DAY";

					this._flex매출처.AddFinished();
					this._flex매출처.Focus();
				}
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

				if (this.tabControlExt1.SelectedTab == this.tpg매입처)
				{
					if (!this._flex매입처.HasNormalRow) return;

					this._flex매입처.Rows.Remove(this._flex매입처.Row);
				}
				else
				{
					if (!this._flex매출처.HasNormalRow) return;

					this._flex매출처.Rows.Remove(this._flex매출처.Row);
				}
			}
			catch (Exception ex)
			{
				MsgEnd(ex);
			}
		}

		protected override bool BeforeSave()
		{
			if (this._flex매입처.GetChanges() != null &&
				this._flex매입처.GetChanges().Select("TP_PERIOD = 'WEK' AND ISNULL(YN_MON, 'N') = 'N' AND ISNULL(YN_TUE, 'N') = 'N' AND ISNULL(YN_WED, 'N') = 'N' AND ISNULL(YN_THU, 'N') = 'N' AND ISNULL(YN_FRI, 'N') = 'N'").Length > 0)
			{
				this.ShowMessage("매주 발송 매입처 중 요일이 선택되지 않은 매입처가 있습니다.");
				return false;
			}

			if (this._flex물류업무.GetChanges() != null)
			{
				foreach (DataRow dr in ComFunc.getGridGroupBy(this._flex물류업무.DataTable, new string[] { "CD_PARTNER", "CD_CATEGORY1", "CD_CATEGORY2", "CD_DELIVERY" }, true).Rows)
				{
					if (this._flex물류업무.DataTable.Select(string.Format("CD_PARTNER = '{0}' AND CD_CATEGORY1 = '{1}' AND ISNULL(CD_CATEGORY2, '') = '{2}' AND ISNULL(CD_DELIVERY, '') = '{3}'", dr["CD_PARTNER"].ToString(), dr["CD_CATEGORY1"].ToString(), dr["CD_CATEGORY2"].ToString(), dr["CD_DELIVERY"].ToString())).Length > 1)
					{
						this.ShowMessage("분류 값이 중복 되었습니다.");
						return false;
					}
				}

				if (this._flex물류업무.GetChanges().Select("ISNULL(CD_CATEGORY1, '') = ''").Length > 0)
				{
					this.ShowMessage("분류1 이 등록되지 않은 데이터가 존재 합니다.");
					return false;
				}

				if (this._flex물류업무.GetChanges().Select("CD_CATEGORY1 = 'WM003' AND ISNULL(CD_CATEGORY2, '') = ''").Length > 0)
				{
					this.ShowMessage("분류2 가 등록되지 않은 데이터가 존재 합니다.");
					return false;
				}
			}

			return base.BeforeSave();
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
			if (!base.SaveData() || !base.Verify()) return false;
			if (this._flex매입처.IsDataChanged == false &&
				this._flex매출처.IsDataChanged == false &&
				this._flex물류업무.IsDataChanged == false) return false;

			if (this._biz.Save(this._flex매입처.GetChanges()))
				this._flex매입처.AcceptChanges();
			else
				return false;

			if (this._biz.Save1(this._flex매출처.GetChanges()))
				this._flex매출처.AcceptChanges();
			else
				return false;

			if (this._biz.Save2(this._flex물류업무.GetChanges()))
				this._flex물류업무.AcceptChanges();
			else
				return false;

			return true;
		}

		public string RemoveHtmlTag(string html)
		{
			string tmpHtml = Regex.Replace(html, @"<(.|\n)*?>", string.Empty).Trim();

			return tmpHtml;
		}

		private string GetUniqueFileName(string fileName)
		{
			FileInfo file = new FileInfo(fileName);
			string newName = file.Name.Replace(file.Extension, "");
			string path = DIR.GetPath(fileName);
			int index = 0;

			while (File.Exists(path + @"\" + newName + file.Extension))
			{
				index++;
				newName = file.Name.Replace(file.Extension, "") + "(" + index + ")";
			}

			return newName + file.Extension;
		}

		private void 상업송장데이터(string 회사코드, DataRow[] dataRowArray, ref DataTable 상업송장헤더, ref DataTable 상업송장라인)
		{
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

				DataSet ds = this._biz.협조전데이터(new object[] { Global.MainFrame.ServerKey,
																   회사코드,
																   Global.MainFrame.LoginInfo.Language,
																   협조전번호 });

				상업송장헤더 = ds.Tables[0];
				상업송장라인 = ds.Tables[1].Clone();

				상업송장라인.Columns.Add("TP_ROW");
				상업송장라인.Columns.Add("CD_DM_CI");
				상업송장라인.Columns.Add("CD_BAR_CI");

				수주번호 = string.Empty;

				string filePath = Path.Combine(Application.StartupPath, "temp");
				string fileName = string.Empty;
				if (!Directory.Exists(filePath))
					Directory.CreateDirectory(filePath);

				foreach (DataRow dr in 상업송장헤더.Rows)
				{
					foreach (DataRow dr1 in ds.Tables[1].Select("NO_GIR = '" + dr["NO_GIR"].ToString() + "'"))
					{
						// 자품목 제외
						if (D.GetString(dr1["TP_BOM"]) != "C")
						{
							if (수주번호 != D.GetString(dr1["NO_SO"]))
							{
								tmpRow = 상업송장라인.NewRow();

								tmpRow["NO_GIR"] = dr1["NO_GIR"];
								tmpRow["NO_SO"] = dr1["NO_SO"];
								tmpRow["NO_PO_PARTNER"] = dr1["NO_PO_PARTNER"];
								tmpRow["NM_ITEM_PARTNER"] = "YOUR ORDER NO. : " + D.GetString(dr1["NO_PO_PARTNER"]) + " / " + "OUR REF. : " + D.GetString(dr1["NO_SO"]);

								상업송장라인.Rows.Add(tmpRow);

								수주번호 = D.GetString(dr1["NO_SO"]);
							}

							상업송장라인.ImportRow(dr1);
							상업송장라인.Rows[상업송장라인.Rows.Count - 1]["TP_ROW"] = "I";

							fileName = filePath + "\\" + dr1["NO_GIR"].ToString() + "_CI_DM.png";

							if (!string.IsNullOrEmpty(dr1["CD_QR_CI"].ToString()) && !File.Exists(fileName))
							{
								string[] qr코드Array = dr1["CD_QR_CI"].ToString().Split('/');
								QR.데이터매트릭스(qr코드Array[qr코드Array.Length - 1], fileName);
							}

							상업송장라인.Rows[상업송장라인.Rows.Count - 1]["CD_DM_CI"] = fileName;

							fileName = filePath + "\\" + dr1["NO_GIR"].ToString() + "_CI_BAR.png";

							if (!string.IsNullOrEmpty(dr1["CD_QR_CI"].ToString()) && !File.Exists(fileName))
							{
								string[] qr코드Array = dr1["CD_QR_CI"].ToString().Split('/');
								QR.바코드(qr코드Array[qr코드Array.Length - 1], fileName);
							}

							상업송장라인.Rows[상업송장라인.Rows.Count - 1]["CD_BAR_CI"] = fileName;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

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
				return false;
			}
		}
	}
}

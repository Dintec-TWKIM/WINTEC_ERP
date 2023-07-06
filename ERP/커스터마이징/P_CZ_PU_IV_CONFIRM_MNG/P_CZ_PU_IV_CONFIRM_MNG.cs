using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Dintec;
using Duzon.Common.BpControls;
using Duzon.Common.Forms;
using Duzon.Erpiu.ComponentModel;
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
	public partial class P_CZ_PU_IV_CONFIRM_MNG : PageBase
	{
		P_CZ_PU_IV_CONFIRM_MNG_BIZ _biz = new P_CZ_PU_IV_CONFIRM_MNG_BIZ();

		public P_CZ_PU_IV_CONFIRM_MNG()
		{
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
			this.MainGrids = new FlexGrid[] { this._flex };

			this._flex.BeginSetting(1, 1, false);

			this._flex.SetCol("S", "선택", 40, true, CheckTypeEnum.Y_N);
			this._flex.SetCol("YN_RETURN", "회신여부", 40, false, CheckTypeEnum.Y_N);
			this._flex.SetCol("YN_SEND", "재알람여부", 40, false, CheckTypeEnum.Y_N);
			this._flex.SetCol("YN_DONE", "완료여부", 40, false, CheckTypeEnum.Y_N);
			this._flex.SetCol("SEQ", "순번", 60);
			this._flex.SetCol("DT_ELAPSE", "경과시간", 100);
			this._flex.SetCol("DT_END", "마감일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex.SetCol("TP_CONFIRM", "요청유형", 100);
			this._flex.SetCol("NO_IO", "입고번호", 100);
			this._flex.SetCol("NO_ETAX", "승인번호", 100);
			this._flex.SetCol("NO_PO", "발주번호", 100);
			this._flex.SetCol("LN_PARTNER", "매입처", 100);
			this._flex.SetCol("QT_PO", "발주수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("QT_RCV", "입고수량", 100, false, typeof(decimal), FormatTpType.QUANTITY);
			this._flex.SetCol("NM_INSERT", "요청자", 100);
			this._flex.SetCol("DTS_INSERT", "요청일자", 100, false, typeof(string), FormatTpType.YEAR_MONTH_DAY);

			this._flex.SetOneGridBinding(null, new IUParentControl[] { this.oneGrid2 });
			this._flex.SetDataMap("TP_CONFIRM", MA.GetCodeUser(new string[] { "001", "002", "003", "004" }, new string[] { "입고", "세금계산서", "수기", "거래명세서" }), "CODE", "NAME");

			this._flex.Cols["DTS_INSERT"].Format = "####/##/## ##:##:##";

			this._flex.SetDummyColumn(new string[] { "S" });

			this._flex.SettingVersion = "0.0.0.1";
			this._flex.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			this._flex.AfterRowChange += _flex_AfterRowChange;

			this.btn확인요청.Click += Btn확인요청_Click;
			this.btn완료.Click += Btn완료_Click;
			this.btn완료취소.Click += Btn완료취소_Click;
			this.btn입고담당자지정.Click += Btn입고담당자지정_Click;

			this.bpc수신자.QueryAfter += Bpc수신자_QueryAfter;
			this.ctx발주번호.QueryBefore += Ctx발주번호_QueryBefore;
			this.ctx발주번호.QueryAfter += Ctx발주번호_QueryAfter;
		}

		private void Btn완료취소_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				if (Global.MainFrame.ShowMessage("선택된 건을 완료 취소 처리 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

				dataRowArray = this._flex.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else if (this._flex.DataTable.Select("S = 'Y' AND YN_DONE = 'N'").Length > 0)
				{
					this.ShowMessage("이미 완료 취소된 건이 선택되어 있습니다.");
				}
				else
				{
					query = @"UPDATE CZ_PU_IV_CONFIRM
SET YN_DONE = 'N',
    DTS_DONE = NULL,
	ID_UPDATE = '{0}',
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = '{1}'
AND SEQ = '{2}'";

					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.UserID,
																	Global.MainFrame.LoginInfo.CompanyCode,
																	dr["SEQ"].ToString()));
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Ctx발주번호_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				this._flex["NO_PO"] = e.CodeValue;
				this._flex["LN_PARTNER"] = e.HelpReturn.Rows[0]["LN_PARTNER"].ToString();
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Ctx발주번호_QueryBefore(object sender, BpQueryArgs e)
		{
			try
			{
				e.HelpParam.P11_ID_MENU = "H_PU_PO_SUB";
				e.HelpParam.P21_FG_MODULE = "N";
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn입고담당자지정_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				if (!this._flex.HasNormalRow) return;

				if (string.IsNullOrEmpty(this.ctx발주번호.CodeValue))
				{
					this.ShowMessage("발주번호가 입력되어야 합니다.");
					return;
				}

				if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
				{
					query = @"SELECT ME.NO_EMP,
	   ME.NM_KOR
FROM CZ_MA_CODEDTL MC WITH(NOLOCK)
CROSS APPLY string_split(MC.CD_FLAG1, ',') A
JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = MC.CD_COMPANY AND (ME.CD_CC = A.VALUE OR ME.NO_EMP = A.VALUE) AND ISNULL(ME.DT_RETIRE, '00000000') = '00000000'
WHERE MC.CD_COMPANY = '{0}'
AND MC.CD_FIELD = 'CZ_PU00010'
AND ME.NO_EMP <> 'S-223'
AND A.VALUE NOT IN ('010360', 'S-484', 'S-283')
AND MC.CD_SYSDEF = LEFT('{1}', 2)
UNION ALL
SELECT VE.NO_EMP1 AS NO_EMP, 
       ME.NM_KOR 
FROM PU_POH PH WITH(NOLOCK)
JOIN SA_SOH SH WITH(NOLOCK) ON SH.CD_COMPANY = PH.CD_COMPANY AND SH.NO_SO = PH.CD_PJT
JOIN (SELECT VE1.CD_COMPANY, VE1.NO_FILE, VE1.NO_EMP1
      FROM (SELECT CD_COMPANY,
                   NO_FILE,
                   CD_FLAG1,
                   CD_FLAG2
            FROM V_CZ_SA_QTN_LOG_EMP WITH(NOLOCK)
            WHERE CD_COMPANY = '{0}') VE
      UNPIVOT (NO_EMP1 FOR NO_EMP IN (CD_FLAG1, CD_FLAG2)) AS VE1) VE 
ON VE.CD_COMPANY = SH.CD_COMPANY AND VE.NO_FILE = SH.NO_SO
JOIN MA_EMP ME WITH(NOLOCK) ON ME.CD_COMPANY = VE.CD_COMPANY AND ME.NO_EMP = VE.NO_EMP1 AND ISNULL(ME.DT_RETIRE, '00000000') = '00000000'
WHERE PH.CD_COMPANY = '{0}'
AND PH.NO_PO = '{1}'";
				}
				else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				{
					query = @"SELECT ME.NO_EMP,
								     ME.NM_KOR
FROM MA_EMP ME WITH(NOLOCK)
WHERE ME.CD_COMPANY = '{0}'
AND ME.CD_CC = '020230'
AND ISNULL(ME.DT_RETIRE, '00000000') = '00000000'";
				}
				else
					return;

				DataTable dt = DBHelper.GetDataTable(string.Format(query, Global.MainFrame.LoginInfo.CompanyCode, this.ctx발주번호.CodeValue));

				if (Global.MainFrame.LoginInfo.CompanyCode == "K100")
				{
					foreach (DataRow dr in dt.Rows)
					{
						if (!this.bpc수신자.QueryWhereIn_Pipe.Contains(dr["NO_EMP"].ToString()))
							this.bpc수신자.AddItem(dr["NO_EMP"].ToString(), dr["NM_KOR"].ToString());
					}
				}
				else if (Global.MainFrame.LoginInfo.CompanyCode == "K200")
				{
					this.bpc수신자.AddItem("S-485", "이승철");

					foreach (DataRow dr in dt.Rows)
					{
						this.bpc수신자.AddItem(dr["NO_EMP"].ToString(), dr["NM_KOR"].ToString());
					}
				}

				this._flex["NO_EMP"] = this.bpc수신자.QueryWhereIn_Pipe;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			this.splitContainer1.SplitterDistance = 1162;
		}

		private void Btn확인요청_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			List<string> 수신자;
			string contents;

			try
			{
				if (!this._flex.HasNormalRow) return;

				dataRowArray = this._flex.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else if (this._flex.DataTable.Select("S = 'Y' AND ISNULL(DC_RMK, '') = ''").Length > 0)
				{
					this.ShowMessage("요청내용이 입력되지 않은 건이 선택되어 있습니다.");
					return;
				}
				else
				{
					contents = @"** 세금계산서 확인 요청

순번 : {0}
매입처명 : {1}
발주번호 : {2}
계산서금액 : {3} 

{4}

확인 후 워크플로우 -> 계산서확인 단계를 통해서 회신 바랍니다. 

※ 본 쪽지는 발신전용 입니다.";

					foreach (DataRow dr in dataRowArray)
					{
						수신자 = new List<string>();

						foreach (string 사원 in dr["NO_EMP"].ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
						{
							수신자.Add(사원);

							if (dr["NO_PO"].ToString().Contains("ST") && 
								DBHelper.GetDataTable(string.Format(@"SELECT ME.NO_EMP 
                                                                      FROM MA_EMP ME
                                                                      WHERE ME.CD_COMPANY = 'K100'
                                                                      AND ME.CD_CC = '010900'
                                                                      AND ME.NO_EMP = '{0}'", 사원)).Rows.Count > 0)
							{
								수신자.Add("S-495");
							}
						}

						수신자.Add(dr["ID_INSERT"].ToString());

						Messenger.SendMSG(수신자.ToArray(), string.Format(contents, dr["SEQ"].ToString(),
																					dr["LN_PARTNER"].ToString(),
																					dr["NO_PO"].ToString(),
																					dr["AM_EX"].ToString(),
																					dr["DC_RMK"].ToString()));
					}

					this.ShowMessage(공통메세지._작업을완료하였습니다, this.btn확인요청.Text);
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Bpc수신자_QueryAfter(object sender, BpQueryArgs e)
		{
			try
			{
				this._flex["NO_EMP"] = e.HelpReturn.QueryWhereIn_Pipe;
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void Btn완료_Click(object sender, EventArgs e)
		{
			DataRow[] dataRowArray;
			string query;

			try
			{
				if (Global.MainFrame.ShowMessage("선택된 건을 완료 처리 하시겠습니까?", "QY2") != DialogResult.Yes)
					return;

		        dataRowArray = this._flex.DataTable.Select("S = 'Y'");

				if (dataRowArray == null || dataRowArray.Length == 0)
				{
					this.ShowMessage(공통메세지.선택된자료가없습니다);
				}
				else if (this._flex.DataTable.Select("S = 'Y' AND YN_DONE = 'Y'").Length > 0)
				{
					this.ShowMessage("이미 완료된 건이 선택되어 있습니다.");
				}
				else
				{
					query = @"UPDATE CZ_PU_IV_CONFIRM
SET YN_DONE = 'Y',
    DTS_DONE = NEOE.SF_SYSDATE(GETDATE()),
	ID_UPDATE = '{0}',
	DTS_UPDATE = NEOE.SF_SYSDATE(GETDATE())
WHERE CD_COMPANY = '{1}'
AND SEQ = '{2}'";

					foreach (DataRow dr in dataRowArray)
					{
						DBHelper.ExecuteScalar(string.Format(query, Global.MainFrame.LoginInfo.UserID,
																	Global.MainFrame.LoginInfo.CompanyCode,
																	dr["SEQ"].ToString()));
					}
				}
			}
			catch (Exception ex)
			{
				this.MsgEnd(ex);
			}
		}

		private void _flex_AfterRowChange(object sender, RangeEventArgs e)
		{
			string query;

			try
			{
				query = @"SELECT A.value AS CODE,
	   MU.NM_USER AS NAME
FROM string_split('{0}', '|') A
JOIN MA_USER MU WITH(NOLOCK) ON MU.CD_COMPANY = '{1}' AND MU.ID_USER = A.value";

				this.bpc수신자.Clear();

				DataTable dt1 = Global.MainFrame.FillDataTable(string.Format(query, this._flex["NO_EMP"].ToString(),
																					Global.MainFrame.LoginInfo.CompanyCode));

				foreach (DataRow dr in dt1.Rows)
				{
					this.bpc수신자.AddItem(D.GetString(dr["CODE"]), D.GetString(dr["NAME"]));
				}

				this.ctx발주번호.CodeValue = this._flex["NO_PO"].ToString();
				this.ctx발주번호.CodeName = this._flex["NO_PO"].ToString();
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

				if (!this.BeforeSearch()) return;

				this._flex.Binding = this._biz.Search(new object[] { Global.MainFrame.LoginInfo.CompanyCode,
																     this.ctx요청자.CodeValue,
																	 this.cur경과일수.DecimalValue,
																     (this.chk완료건제외.Checked == true ? "Y" : "N"),
																	 (this.chk마감일자지난건.Checked == true ? "Y" : "N"),
																	 (this.chk회신받은건.Checked == true ? "Y" : "N") });

				if (!this._flex.HasNormalRow)
					this.ShowMessage(공통메세지.조건에해당하는내용이없습니다);
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

				if (!BeforeAdd()) return;

				this._flex.Rows.Add();
				this._flex.Row = _flex.Rows.Count - 1;

				this._flex["TP_CONFIRM"] = "003";
				this._flex["SEQ"] = this.SeqMax();
				this._flex["ID_INSERT"] = Global.MainFrame.LoginInfo.UserID;

				this._flex.AddFinished();
				this._flex.Focus();
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
			if (this._flex.IsDataChanged == false) return false;

			DataTable dt = this._flex.GetChanges();

			if (!this._biz.Save(dt)) return false;

			this._flex.AcceptChanges();

			return true;
		}

		private Decimal SeqMax()
		{
			Decimal num = 1;
			DataTable dataTable = DBHelper.GetDataTable(@"SELECT MAX(SEQ) AS SEQ 
                                                          FROM CZ_PU_IV_CONFIRM WITH(NOLOCK)  
                                                          WHERE CD_COMPANY = '" + Global.MainFrame.LoginInfo.CompanyCode + "'");

			if (dataTable != null && dataTable.Rows.Count != 0)
				num = (D.GetDecimal(dataTable.Rows[0]["SEQ"]) + 1);

			if (num <= this._flex.GetMaxValue("SEQ"))
				num = (this._flex.GetMaxValue("SEQ") + 1);

			return num;
		}
	}
}

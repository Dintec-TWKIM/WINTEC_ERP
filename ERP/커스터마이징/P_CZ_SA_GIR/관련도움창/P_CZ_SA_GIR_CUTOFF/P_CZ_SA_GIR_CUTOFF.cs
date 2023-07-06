using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.Forms;
using Duzon.ERPU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cz
{
	public partial class P_CZ_SA_GIR_CUTOFF : Duzon.Common.Forms.CommonDialog
	{
		private string _회사코드;
		private string _의뢰번호;
		private string _협조전상태 = string.Empty;

		P_CZ_SA_GIR_CUTOFF_BIZ _biz = new P_CZ_SA_GIR_CUTOFF_BIZ();

		public P_CZ_SA_GIR_CUTOFF(string 회사코드, string 의뢰번호)
		{
			InitializeComponent();

			this._회사코드 = 회사코드;
			this._의뢰번호 = 의뢰번호;
		}

		protected override void InitLoad()
		{
			base.InitLoad();

			this.InitGrid();
			this.InitEvent();
		}

		protected override void InitPaint()
		{
			base.InitPaint();

			string query = @"SELECT GH.STA_GIR,
       ISNULL(WD.DTS_CUTOFF, '') AS DTS_CUTOFF
FROM SA_GIRH GH WITH(NOLOCK)
JOIN CZ_SA_GIRH_WORK_DETAIL WD WITH(NOLOCK) ON WD.CD_COMPANY = GH.CD_COMPANY AND WD.NO_GIR = GH.NO_GIR
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";

			DataTable dt = DBHelper.GetDataTable(string.Format(query, this._회사코드, this._의뢰번호));
			string 컷오프시간 = string.Empty;
			

			if (dt != null && dt.Rows.Count > 0)
			{
				컷오프시간 = dt.Rows[0]["DTS_CUTOFF"].ToString();
				this._협조전상태 = dt.Rows[0]["STA_GIR"].ToString();
			}

		    if (!string.IsNullOrEmpty(컷오프시간))
			{
				DateTime dt컷오프시간 = new DateTime();
				
				if (DateTime.TryParseExact(컷오프시간, "yyyyMMddHH", null, DateTimeStyles.None, out dt컷오프시간))
				{
					this.dtp컷오프시간.Value = dt컷오프시간;
				}
			}
			else
			{
				DateTime date = new DateTime();

				if (DateTime.TryParseExact(Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd") + "00", "yyyyMMddHH", null, DateTimeStyles.None, out date))
				{
					this.dtp컷오프시간.Value = date;
				}
			}

			if (!string.IsNullOrEmpty(this._협조전상태))
				this.dtp컷오프시간.Enabled = false;

			this.Btn조회_Click(null, null);
		}

		private void InitGrid()
		{
			this._flex컷오프시간.BeginSetting(1, 1, false);

			this._flex컷오프시간.SetCol("YN_CHECK", "확인필요", 60, false, CheckTypeEnum.Y_N);
			this._flex컷오프시간.SetCol("NO_PO", "발주번호", 100);
			this._flex컷오프시간.SetCol("NO_DSP", "대상품목(순번)", 100);
			this._flex컷오프시간.SetCol("NM_SUPPLIER", "매입처", 100);
			this._flex컷오프시간.SetCol("CD_DLV", "납품장소", 100, true);
			this._flex컷오프시간.SetCol("DT_DLV", "납품일자", 100, true, typeof(string), FormatTpType.YEAR_MONTH_DAY);
			this._flex컷오프시간.SetCol("TM_DLV", "납품시간", 100, true);
			this._flex컷오프시간.SetCol("DC_DLV", "납품비고", 100, true);

			this._flex컷오프시간.SetDataMap("CD_DLV", DBHelper.GetDataTable(@"SELECT MC.CD_SYSDEF AS CODE,
																			  	    MC.NM_SYSDEF AS NAME
																			  FROM CZ_MA_CODEDTL MC WITH(NOLOCK)
																			  WHERE MC.CD_COMPANY = 'K100'
																			  AND MC.CD_FIELD = 'CZ_PU00012'
																			  AND MC.CD_SYSDEF <> '000'"), "CODE", "NAME");
			this._flex컷오프시간.SetDataMap("TM_DLV", DBHelper.GetDataTable(@"SELECT MC.CD_SYSDEF AS CODE,
																			  	    MC.NM_SYSDEF AS NAME
																			  FROM CZ_MA_CODEDTL MC WITH(NOLOCK)
																			  WHERE MC.CD_COMPANY = 'K100'
																			  AND MC.CD_FIELD = 'CZ_PU00013'
																			  AND MC.CD_SYSDEF <> '0000'"), "CODE", "NAME");

			this._flex컷오프시간.SettingVersion = "1.0.0.0";
			this._flex컷오프시간.EndSetting(GridStyleEnum.Green, AllowSortingEnum.MultiColumn, SumPositionEnum.None);
		}

		private void InitEvent()
		{
			this.btn조회.Click += Btn조회_Click;
			this.btn저장.Click += Btn저장_Click;
		}

		private void Btn저장_Click(object sender, EventArgs e)
		{
			string query;

			try
			{
				if (this.dtp컷오프시간.Value.Hour < 10)
				{
					Global.MainFrame.ShowMessage("10시 이후부터 설정할 수 있습니다.\n10시로 자동 설정되어서 저장 됩니다.");

					DateTime dt = new DateTime();

					if (DateTime.TryParseExact(Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd") + "10", "yyyyMMddHH", null, DateTimeStyles.None, out dt))
						this.dtp컷오프시간.Value = dt;
				}

				if (this.dtp컷오프시간.Value.Hour > 16)
				{
					Global.MainFrame.ShowMessage("16시 이전까지 설정할 수 있습니다.\n16시로 자동 설정되어서 저장 됩니다.");

					DateTime dt = new DateTime();

					if (DateTime.TryParseExact(Global.MainFrame.GetDateTimeToday().ToString("yyyyMMdd") + "17", "yyyyMMddHH", null, DateTimeStyles.None, out dt))
						this.dtp컷오프시간.Value = dt;
				}

				query = @"UPDATE WD
SET WD.DTS_CUTOFF = '{2}' 
FROM CZ_SA_GIRH_WORK_DETAIL WD
WHERE WD.CD_COMPANY = '{0}'
AND WD.NO_GIR = '{1}'";

				DBHelper.ExecuteScalar(string.Format(query, this._회사코드, this._의뢰번호, this.dtp컷오프시간.Value.ToString("yyyyMMddHH")));

				if (this._flex컷오프시간.IsDataChanged)
				{
					foreach (DataRow dr in this._flex컷오프시간.GetChanges().Rows)
					{
						query = @"UPDATE PH
SET PH.CD_DLV = '{2}',
    PH.DT_DLV = '{3}',
    PH.TM_DLV = '{4}',
    PH.DC_DLV = '{5}'
FROM PU_POH PH
WHERE PH.CD_COMPANY = '{0}'
AND PH.NO_PO = '{1}'";

						DBHelper.ExecuteScalar(string.Format(query, dr["CD_COMPANY"].ToString(),
																	dr["NO_PO"].ToString(),
																	dr["CD_DLV"].ToString(),
																	dr["DT_DLV"].ToString(),
																	dr["TM_DLV"].ToString(),
																	dr["DC_DLV"].ToString()));
					}
				}

				Global.MainFrame.ShowMessage(공통메세지.자료가정상적으로저장되었습니다);
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}

		private void Btn조회_Click(object sender, EventArgs e)
		{
			DataTable dt;
			
			try
			{
				dt = DBHelper.GetDataTable("SP_CZ_SA_GIR_CUTOFF_S", new object[] { this._회사코드, this._의뢰번호, "N" });

				this._flex컷오프시간.Binding = dt;

				if (!string.IsNullOrEmpty(this._협조전상태) || dt.Select("YN_CHECK = 'Y'").Length > 0)
					this.dtp컷오프시간.Enabled = false;
				else
					this.dtp컷오프시간.Enabled = true;

				if (dt.Select("NO_PO NOT LIKE 'FB%'").Length > 0)
				{
					this._flex컷오프시간.Cols["CD_DLV"].AllowEditing = false;
					this._flex컷오프시간.Cols["DT_DLV"].AllowEditing = false;
					this._flex컷오프시간.Cols["TM_DLV"].AllowEditing = false;
					this._flex컷오프시간.Cols["DC_DLV"].AllowEditing = false;
				}
			}
			catch (Exception ex)
			{
				Global.MainFrame.MsgEnd(ex);
			}
		}
	}
}

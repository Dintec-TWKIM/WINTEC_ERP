using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dintec;
using PopupControl;

namespace cz
{
	public partial class H_CZ_VIEW_OFFER_RATE : UserControl
	{
		string companyCode;
		string fileNumber;

		string partnerCode;
		string partnerName;

		#region ==================================================================================================== Property

		public string CompanyCode
		{
			set
			{
				companyCode = value;
			}
		}

		public string FileNumber
		{
			set
			{
				fileNumber = value;
			}
		}
		
		public string PartnerCode
		{
			set
			{
				partnerCode = value;
			}
		}

		public string PartnerName
		{
			set
			{
				partnerName = value;
			}
		}

		#endregion

		#region ==================================================================================================== Constructor

		public H_CZ_VIEW_OFFER_RATE()
		{
			InitializeComponent();

			cboSupplier.SelectionChangeCommitted += new EventHandler(cboSupplier_SelectionChangeCommitted);
			chkPeriod1.CheckedChanged += new EventHandler(chkPeriod_CheckedChanged);
			chkPeriod2.CheckedChanged += new EventHandler(chkPeriod_CheckedChanged);
			pnlClose.Click += new EventHandler(pnlClose_Click);
		}
		
		#endregion

		#region ==================================================================================================== Event

		private void cboSupplier_SelectionChangeCommitted(object sender, EventArgs e)
		{
			int period = chkPeriod1.Checked ? 180 : 365;

			DataSet ds = DBMgr.GetDataSet("PS_CZ_SA_OFFER_RATE", companyCode, partnerCode, cboSupplier.GetValue(), period);
			DataTable dtB = ds.Tables[1];
			DataTable dtC = ds.Tables[2];

			BindTypeB(ds.Tables[1]);
			BindTypeC(ds.Tables[2]);
		}

		private void chkPeriod_CheckedChanged(object sender, EventArgs e)
		{
			int period = chkPeriod1.Checked ? 180 : 365;

			DataSet ds = DBMgr.GetDataSet("PS_CZ_SA_OFFER_RATE", companyCode, partnerCode, cboSupplier.GetValue(), period);
			BindTypeA(ds.Tables[0]);
			BindTypeB(ds.Tables[1]);
			BindTypeC(ds.Tables[2]);
		}

		private void pnlClose_Click(object sender, EventArgs e)
		{
			((Popup)this.Parent).Close();
		}

		#endregion

		#region ==================================================================================================== Etc

		public void Bind()
		{
			// 매출처명 표시
			lblPartner.Text = partnerName;

			// 매입처 바인딩
			string query = @"
SELECT
	  A.CD_SUPPLIER	AS CODE
	, B.LN_PARTNER	AS NAME
FROM
(
	SELECT 
		  CD_COMPANY
		, CD_SUPPLIER
	FROM CZ_SA_QTNL WITH(NOLOCK)
	WHERE 1 = 1
		AND CD_COMPANY = '" + companyCode + @"'
		AND NO_FILE = '" + fileNumber + @"'
		AND CD_SUPPLIER IS NOT NULL
	GROUP BY CD_COMPANY, CD_SUPPLIER
)				AS A
JOIN MA_PARTNER AS B WITH(NOLOCK) ON A.CD_COMPANY = B.CD_COMPANY AND A.CD_SUPPLIER = B.CD_PARTNER";

			DataTable dt = DBMgr.GetDataTable(query);
			cboSupplier.DataBind(dt, false);
			if (cboSupplier.Items.Count > 0) cboSupplier.SelectedItem = 0;

			// 이윤율 표시
			int period = chkPeriod1.Checked ? 180 : 365;
			DataSet ds = DBMgr.GetDataSet("PS_CZ_SA_OFFER_RATE", companyCode, partnerCode, cboSupplier.GetValue(), period);
			
			BindTypeA(ds.Tables[0]);
			BindTypeB(ds.Tables[1]);
			BindTypeC(ds.Tables[2]);			
		}

		public void ChangeSupplier(string supplierCode)
		{
			if (chkGrid.Checked)
			{
				cboSupplier.SetValue(supplierCode);
				cboSupplier_SelectionChangeCommitted(null, null);
			}
		}
		
		private void BindTypeA(DataTable dt)
		{
			// 업데이트 일자
			if (dt.Rows.Count > 0) lblUpdateA.Text = "Updated : " + string.Format("{0:yyyy'/'MM'/'dd}", GetTo.Date(dt.Rows[0]["DT_UPDATE"]));

			// 매출처 Offer 이윤율
			for (int i = 0; i < 7; i++)
			{
				if (i < dt.Rows.Count)
				{
					// 바인딩
					((Label)pnlGroupA.Controls["lblGroupA" + i]).Tag = dt.Rows[i]["CD_ITEMGRP"].ToString();
					((Label)pnlGroupA.Controls["lblGroupA" + i]).Text = dt.Rows[i]["NM_ITEMGRP"].ToString();
					((Label)pnlQtnCountA.Controls["lblQtnCountA" + i]).Text = string.Format("{0:#,##0}건", dt.Rows[i]["CNT_QTN"]);
					((Label)pnlQtnMarginA.Controls["lblQtnMarginA" + i]).Text = string.Format("{0:#,##0.00}%", dt.Rows[i]["RT_MARGIN_QTN"]);
					((Label)pnlOrdSuccessA.Controls["lblOrdSuccessA" + i]).Text = string.Format("{0:#,##0.00}%", dt.Rows[i]["RT_SUCCESS_ORD"]);
					((Label)pnlOrdMarginA.Controls["lblOrdMarginA" + i]).Text = string.Format("{0:#,##0.00}%", dt.Rows[i]["RT_MARGIN_ORD"]);
				}
				else
				{
					// 초기화
					((Label)pnlGroupA.Controls["lblGroupA" + i]).Tag = "";
					((Label)pnlGroupA.Controls["lblGroupA" + i]).Text = "";
					((Label)pnlQtnCountA.Controls["lblQtnCountA" + i]).Text = "";
					((Label)pnlQtnMarginA.Controls["lblQtnMarginA" + i]).Text = "";
					((Label)pnlOrdSuccessA.Controls["lblOrdSuccessA" + i]).Text = "";
					((Label)pnlOrdMarginA.Controls["lblOrdMarginA" + i]).Text = "";
				}
			}
		}
		
		private void BindTypeB(DataTable dt)
		{
			// 업데이트 일자
			if (dt.Rows.Count > 0) lblUpdateB.Text = "Updated : " + string.Format("{0:yyyy'/'MM'/'dd}", GetTo.Date(dt.Rows[0]["DT_UPDATE"]));

			// 매입처 Offer 이윤율
			for (int i = 0; i < 7; i++)
			{
				string group = GetTo.String(((Label)pnlGroupA.Controls["lblGroupA" + i]).Tag);
				DataRow[] rows = dt.Select("CD_ITEMGRP = '" + group + "'");

				if (rows.Length > 0)
				{
					// A그룹의 "구분"과 일치하는 경우 해당 Row 표시
					((Label)pnlGroupB.Controls["lblGroupB" + i]).Text = rows[0]["NM_ITEMGRP"].ToString();
					((Label)pnlQtnCountB.Controls["lblQtnCountB" + i]).Text = string.Format("{0:#,##0}건", rows[0]["CNT_QTN"]);
					((Label)pnlQtnMarginB.Controls["lblQtnMarginB" + i]).Text = string.Format("{0:#,##0.00}%", rows[0]["RT_MARGIN_QTN"]);
					
					((Label)pnlOrdSuccessB.Controls["lblOrdSuccessB" + i]).Text = string.Format("{0:#,##0.00}%", dt.Rows[0]["RT_SUCCESS_ORD"]);
					((Label)pnlOrdMarginB.Controls["lblOrdMarginB" + i]).Text = string.Format("{0:#,##0.00}%", rows[0]["RT_MARGIN_ORD"]);
					dt.Rows.Remove(rows[0]);	// 바인딩 된 행은 삭제	
				}
				else if (group == "" && dt.Rows.Count > 0)
				{
					// A그룹 종료, 남은 항목이 있을 경우 해당 Row 추가
					((Label)pnlGroupB.Controls["lblGroupB" + i]).Text = dt.Rows[0]["NM_ITEMGRP"].ToString();
					((Label)pnlQtnCountB.Controls["lblQtnCountB" + i]).Text = string.Format("{0:#,##0}건", dt.Rows[0]["CNT_QTN"]);
					((Label)pnlQtnMarginB.Controls["lblQtnMarginB" + i]).Text = string.Format("{0:#,##0.00}%", dt.Rows[0]["RT_MARGIN_QTN"]);

					((Label)pnlOrdSuccessB.Controls["lblOrdSuccessB" + i]).Text = string.Format("{0:#,##0.00}%", dt.Rows[0]["RT_SUCCESS_ORD"]);
					((Label)pnlOrdMarginB.Controls["lblOrdMarginB" + i]).Text = string.Format("{0:#,##0.00}%", dt.Rows[0]["RT_MARGIN_ORD"]);
					dt.Rows.RemoveAt(0);		// 바인딩 된 행은 삭제	
				}
				else
				{
					// 초기화
					((Label)pnlGroupB.Controls["lblGroupB" + i]).Text = "";
					((Label)pnlQtnCountB.Controls["lblQtnCountB" + i]).Text = "";
					((Label)pnlQtnMarginB.Controls["lblQtnMarginB" + i]).Text = "";

					((Label)pnlOrdSuccessB.Controls["lblOrdSuccessB" + i]).Text = "";
					((Label)pnlOrdMarginB.Controls["lblOrdMarginB" + i]).Text = "";
				}
			}
		}

		private void BindTypeC(DataTable dt)
		{
			// 업데이트 일자
			if (dt.Rows.Count > 0) lblUpdateC.Text = "Updated : " + string.Format("{0:yyyy'/'MM'/'dd}", GetTo.Date(dt.Rows[0]["DT_UPDATE"]));

			// 매출처 VS 매입처 Offer 이윤율
			for (int i = 0; i < 7; i++)
			{				
				string group = GetTo.String(((Label)pnlGroupA.Controls["lblGroupA" + i]).Tag);
				DataRow[] rows = dt.Select("CD_ITEMGRP = '" + group + "'");
				
				if (rows.Length > 0)
				{
					// A그룹의 "구분"과 일치하는 경우 해당 Row 표시
					((Label)pnlGroupC.Controls["lblGroupC" + i]).Text = rows[0]["NM_ITEMGRP"].ToString();
					((Label)pnlQtnCountC.Controls["lblQtnCountC" + i]).Text = string.Format("{0:#,##0}건", rows[0]["CNT_QTN"]);
					((Label)pnlQtnMarginC.Controls["lblQtnMarginC" + i]).Text = string.Format("{0:#,##0.00}%", rows[0]["RT_MARGIN_QTN"]);
					((Label)pnlOrdMarginC.Controls["lblOrdMarginC" + i]).Text = string.Format("{0:#,##0.00}%", rows[0]["RT_MARGIN_ORD"]);
				}
				else
				{
					// 초기화
					((Label)pnlGroupC.Controls["lblGroupC" + i]).Text = "";
					((Label)pnlQtnCountC.Controls["lblQtnCountC" + i]).Text = "";
					((Label)pnlQtnMarginC.Controls["lblQtnMarginC" + i]).Text = "";
					((Label)pnlOrdMarginC.Controls["lblOrdMarginC" + i]).Text = "";
				}
			}		
		}

		#endregion
	}
}

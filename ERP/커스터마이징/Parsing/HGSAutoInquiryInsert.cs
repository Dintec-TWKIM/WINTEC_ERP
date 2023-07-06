using Dintec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parsing
{
	public class HGSAutoInquiryInsert
	{
		#region ▣ 현대웹 등록
		public static void HGS(string fileHGSNo, string cd_companyHGS, string no_empHGS, string fileHgsName)
		{
			try
			{
				InquiryParser parser = new InquiryParser(fileHgsName);
				parser.Parse(true);

				if (parser.Item.Rows.Count > 0)
				{
					// 호선 가져오기
					string query = @"
SELECT
	  A.NO_IMO
	, A.NO_HULL
	, A.NM_VESSEL
	, B.CD_PARTNER
	, B.LN_PARTNER
	, B.CD_PARTNER_GRP
FROM	  CZ_MA_HULL	AS A WITH(NOLOCK)
LEFT JOIN MA_PARTNER	AS B WITH(NOLOCK) ON A.CD_PARTNER = B.CD_PARTNER AND B.CD_COMPANY = @CD_COMPANY
WHERE A.NO_IMO = @NO_IMO OR (@NO_IMO IS NULL AND A.NM_VESSEL LIKE '%' + @NM_VESSEL + '%')";

					DBMgr dbm = new DBMgr();
					dbm.DebugMode = DebugMode.Print;
					dbm.Query = query;
					dbm.AddParameter("@CD_COMPANY", cd_companyHGS);
					dbm.AddParameter("@NO_IMO", parser.ImoNumber);
					dbm.AddParameter("@NM_VESSEL", parser.Vessel);
					DataTable dt = dbm.GetDataTable();

					string imonumber = string.Empty;
					string partnercode = string.Empty;
					string referencenumber = string.Empty;


					int no_line = 1;
					string nm_subject = string.Empty;
					string unit = string.Empty;
					string cd_item_partner = string.Empty;
					string nm_item_partner = string.Empty;
					string cd_uniq_partner = string.Empty;
					string qt = string.Empty;
					string cd_po_partner = string.Empty;
					string cd_stock = string.Empty;
					string cd_rate = string.Empty;
					string vesselName = string.Empty;
					string tnid = string.Empty;
					string buyer = string.Empty;

					string contact = string.Empty;
					string filenameStr = string.Empty;

					partnercode = "11823";

					if (dt.Rows.Count > 0)
						imonumber = dt.Rows[0]["NO_IMO"].ToString().Trim();
					else
						imonumber = parser.ImoNumber;

					// 문의번호
					referencenumber = parser.Reference;

					if (referencenumber.Equals(DateTime.Now.ToString("yyyy.MM.dd")))
						referencenumber = referencenumber + "." + partnercode + DateTime.Now.ToString("mmm");


					tnid = parser.Tnid;
					vesselName = parser.Vessel;
					// BUYER COMMENT 따로 저장
					buyer = parser.Buyer;
					contact = parser.Contact;
					filenameStr = parser.InqFileName;

					DataTable dtItem = new DataTable();


					dtItem.Columns.Add("CD_COMPANY");
					dtItem.Columns.Add("NO_PREREG");
					dtItem.Columns.Add("NO_LINE");
					dtItem.Columns.Add("CD_PARTNER");
					dtItem.Columns.Add("NO_IMO");
					dtItem.Columns.Add("SHIPSERV_TNID");
					dtItem.Columns.Add("NM_VESSEL");
					dtItem.Columns.Add("NO_REF");
					dtItem.Columns.Add("NO_REQREF");
					dtItem.Columns.Add("NM_SUBJECT");
					dtItem.Columns.Add("CD_ITEM_PARTNER");
					dtItem.Columns.Add("NM_ITEM_PARTNER");
					dtItem.Columns.Add("CD_UNIQ_PARTNER");
					dtItem.Columns.Add("UNIT");
					dtItem.Columns.Add("QT");
					dtItem.Columns.Add("SORTED_BY");
					dtItem.Columns.Add("COMMENT");
					dtItem.Columns.Add("CONTACT");
					dtItem.Columns.Add("NM_FILE");
					dtItem.Columns.Add("DXCODE_SUBJ");
					dtItem.Columns.Add("DXCODE_ITEM");


					DataTable dtItemVendor = new DataTable();
					dtItemVendor.Columns.Add("CD_COMPANY");
					dtItemVendor.Columns.Add("NO_PREREG");
					dtItemVendor.Columns.Add("NO_LINE");
					dtItemVendor.Columns.Add("DXVENDOR_CODE");


					DataTable dtItemHead = new DataTable();
					dtItemHead.Columns.Add("CD_COMPANY");
					dtItemHead.Columns.Add("NO_PREREG");
					dtItemHead.Columns.Add("NO_FILE");
					dtItemHead.Columns.Add("NO_EMP");
					dtItemHead.Columns.Add("CD_PARTNER");
					dtItemHead.Columns.Add("NO_IMO");



					foreach (DataRow row in parser.Item.Rows)
					{
						nm_subject = row["SUBJ"].ToString().ToUpper();
						unit = row["UNIT"].ToString().ToUpper();
						cd_item_partner = row["ITEM"].ToString().ToUpper();
						nm_item_partner = row["DESC"].ToString().ToUpper();
						cd_uniq_partner = row["UNIQ"].ToString().ToUpper();
						qt = row["QT"].ToString();

						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = cd_companyHGS;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = fileHGSNo;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = no_line;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = partnercode;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMO"] = imonumber;
						dtItem.Rows[dtItem.Rows.Count - 1]["SHIPSERV_TNID"] = tnid;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_VESSEL"] = vesselName;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_REF"] = referencenumber;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_REQREF"] = "";
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_SUBJECT"] = nm_subject;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM_PARTNER"] = cd_item_partner;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_ITEM_PARTNER"] = nm_item_partner;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_UNIQ_PARTNER"] = cd_uniq_partner;
						if (unit.Contains(",")) unit = "";
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = unit;
						if (string.IsNullOrEmpty(qt)) qt = "0";
						if (qt.Contains(",")) qt = qt.Replace(",", "").Trim();
						dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = qt;
						dtItem.Rows[dtItem.Rows.Count - 1]["SORTED_BY"] = "HGS";
						dtItem.Rows[dtItem.Rows.Count - 1]["COMMENT"] = buyer;
						dtItem.Rows[dtItem.Rows.Count - 1]["CONTACT"] = contact;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_FILE"] = filenameStr;
						dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_SUBJ"] = Util.GetDxCode(nm_subject.ToUpper());
						dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_ITEM"] = Util.GetDxCode(cd_item_partner.ToUpper()) + "‡" + nm_item_partner.ToUpper();

						dtItemVendor.Rows.Add();
						dtItemVendor.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = cd_companyHGS;
						dtItemVendor.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = fileHGSNo;
						dtItemVendor.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = no_line;
						dtItemVendor.Rows[dtItem.Rows.Count - 1]["DXVENDOR_CODE"] = partnercode;

						no_line += 1;
					}

					dtItemHead.Rows.Add();
					dtItemHead.Rows[0]["CD_COMPANY"] = cd_companyHGS;
					dtItemHead.Rows[0]["NO_PREREG"] = fileHGSNo;
					dtItemHead.Rows[0]["NO_FILE"] = fileHGSNo;
					dtItemHead.Rows[0]["NO_EMP"] = no_empHGS;
					dtItemHead.Rows[0]["CD_PARTNER"] = partnercode;
					dtItemHead.Rows[0]["NO_IMO"] = imonumber;

					string xml = Util.GetTO_Xml(dtItem);
					string xmlVendor = Util.GetTO_Xml(dtItemVendor);
					string xmlHead = Util.GetTO_Xml(dtItemHead);

					SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG", SQLDebug.Print, xml);
					SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG_VENDOR", SQLDebug.Print, xmlVendor);
					SQL.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG_HEAD", SQLDebug.Print, xmlHead);

					RPA rpa = new RPA() { Process = "INQ", FileNumber = fileHGSNo, PartnerCode = "11823" };
					rpa.AddQueue();
				}
			}
			catch (Exception e)
			{

			}
		}

		#endregion ▣ 현대웹 등록
	}
}

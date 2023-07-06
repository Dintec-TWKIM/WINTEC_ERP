using Dintec.DHL;

using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;


namespace Dintec
{
	public class DHL_xml
	{
		UnitConvert_dhl uc = new UnitConvert_dhl();


		static string Printer = string.Empty;
		static double outValue = 0;
		static double resulValue = 0;


		static int outIntValue1 = 0;
		static int resultIntValue = 0;

		#region 물류부 포장
		public static string DHLShipmentValidationService(DataTable dtH, DataTable dtL, DataTable dtInvoice)
		{

			DataTable dt = GetDb.Code("MA_B000020");




			// 프린터 검색
			foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
			{
				if (printer.IndexOf("420") > 0)
				{
					Printer = printer;
					break;
				}
			}


			string CD_COMPANY = string.Empty;
			string NM_CONSIGNEE = string.Empty;
			string EMAIL_CONSIGNEE = string.Empty;
			string PIC_CONSIGNEE = string.Empty;
			string TEL_CONSIGNEE = string.Empty;
			string NM_HS = string.Empty;
			string NM_ITEM_PARTNER = string.Empty;
			string NM_EXCH = string.Empty;
			string AM_EX = string.Empty;
			string NO_GIR = string.Empty;
			string ADD_CONSIGNEE = string.Empty;
			string NM_ARRIVER_COUNTRY = string.Empty;
			string CITY = string.Empty;
			string CD_COUNTRY = string.Empty;
			string TOTAL_COUNT = string.Empty;
			string TOTAL_WEIGHT = string.Empty;
			string CD_PRODUCT = string.Empty;
			string BAN = string.Empty;
			string NO_IO = string.Empty;
			string DT_GIR = string.Empty;
			string CD_POST = string.Empty;
			string YN_INSURANCE = string.Empty;
			string YN_DUTY = string.Empty;
			string AM_CHARGE = string.Empty;
			string DDP = string.Empty;



			// QT_NET_WEIGHT -> QT_GROSS_WEIGHT  변경 20230329
			if (dtH.Rows.Count > 1)
			{
				CD_COMPANY = dtH.Rows[0]["CD_COMPANY"].ToString().Trim(); if (string.IsNullOrEmpty(CD_COMPANY)) return "회사코드 없음";
				NM_CONSIGNEE = dtH.Rows[0]["NM_CONSIGNEE"].ToString().Trim(); if (string.IsNullOrEmpty(NM_CONSIGNEE)) return "수하인 회사 없음";
				EMAIL_CONSIGNEE = dtH.Rows[0]["EMAIL"].ToString().Trim();
				PIC_CONSIGNEE = dtH.Rows[0]["PIC"].ToString().Trim();
				TEL_CONSIGNEE = dtH.Rows[0]["TEL"].ToString().Replace(" ", "").Trim();
				NM_HS = dtH.Rows[0]["NM_HS"].ToString().Trim(); if (string.IsNullOrEmpty(NM_HS)) return "HSCODE 없음";
				NM_ITEM_PARTNER = dtH.Rows[0]["NM_ITEM_PARTNER"].ToString().Trim();
				NM_EXCH = dtH.Rows[0]["NM_EXCH"].ToString().Trim(); if (string.IsNullOrEmpty(NM_EXCH)) return "통화 없음";

				resulValue = 0;
				for (int r = 0; r < dtH.Rows.Count; r++)
				{
					double.TryParse(dtH.Rows[r]["AM_EX"].ToString(), out outValue);
					resulValue += outValue;
				}
				AM_EX = string.Format("{0:0.00}", resulValue);

				resulValue = 0;
				for (int r = 0; r < dtH.Rows.Count; r++)
				{
					double.TryParse(dtH.Rows[r]["AM_CHARGE"].ToString(), out outValue);
					resulValue += outValue;
				}
				AM_CHARGE = string.Format("{0:0.00}", resulValue);


				NO_GIR = dtH.Rows[0]["NO_GIR"].ToString().Trim(); if (string.IsNullOrEmpty(NO_GIR)) return "협조전 번호 없음";
				ADD_CONSIGNEE = dtH.Rows[0]["DC_ADDRESS"].ToString().Trim(); if (string.IsNullOrEmpty(ADD_CONSIGNEE)) return "주소 없음";
				NM_ARRIVER_COUNTRY = dtH.Rows[0]["NM_ARRIVER_COUNTRY"].ToString().Trim(); if (string.IsNullOrEmpty(NM_ARRIVER_COUNTRY)) return "국가 정보가 없습니다.";
				CITY = dtH.Rows[0]["PORT_ARRIVER"].ToString().Trim(); if (string.IsNullOrEmpty(CITY)) return "도시 정보 없음";
				CD_COUNTRY = dtH.Rows[0]["ARRIVER_COUNTRY"].ToString().Trim();

				resultIntValue = 0;
				for (int r = 0; r < dtH.Rows.Count; r++)
				{
					int.TryParse(dtH.Rows[r]["QT_PACK"].ToString(), out outIntValue1);
					resultIntValue += outIntValue1;
				}
				TOTAL_COUNT = Convert.ToString(resultIntValue);

				resulValue = 0;
				for (int r = 0; r < dtH.Rows.Count; r++)
				{
					double.TryParse(dtH.Rows[r]["QT_GROSS_WEIGHT"].ToString(), out outValue);
					resulValue += outValue;
				}
				TOTAL_WEIGHT = string.Format("{0:0.00}", resulValue);

				CD_PRODUCT = dtH.Rows[0]["CD_PRODUCT"].ToString().Trim();
				BAN = dtH.Rows[0]["BAN"].ToString().Trim();
				NO_IO = dtH.Rows[0]["NO_IO"].ToString().Trim(); if (string.IsNullOrEmpty(NO_IO)) return "미출고(인보이스 번호 없음)";
				DT_GIR = dtH.Rows[0]["DT_GIR"].ToString().Trim();
				CD_POST = dtH.Rows[0]["PC"].ToString().Trim();
				YN_INSURANCE = dtH.Rows[0]["YN_INSURANCE"].ToString().Trim();
				YN_DUTY = dtH.Rows[0]["YN_DUTY"].ToString().Trim();
			}
			else
			{
				CD_COMPANY = dtH.Rows[0]["CD_COMPANY"].ToString().Trim(); if (string.IsNullOrEmpty(CD_COMPANY)) return "회사코드 없음";
				NM_CONSIGNEE = dtH.Rows[0]["NM_CONSIGNEE"].ToString().Trim(); if (string.IsNullOrEmpty(NM_CONSIGNEE)) return "수하인 회사 없음";
				EMAIL_CONSIGNEE = dtH.Rows[0]["EMAIL"].ToString().Trim();
				PIC_CONSIGNEE = dtH.Rows[0]["PIC"].ToString().Trim();
				TEL_CONSIGNEE = dtH.Rows[0]["TEL"].ToString().Replace(" ", "").Trim();
				NM_HS = dtH.Rows[0]["NM_HS"].ToString().Trim(); if (string.IsNullOrEmpty(NM_HS)) return "HSCODE 없음";
				NM_ITEM_PARTNER = dtH.Rows[0]["NM_ITEM_PARTNER"].ToString().Trim();
				NM_EXCH = dtH.Rows[0]["NM_EXCH"].ToString().Trim(); if (string.IsNullOrEmpty(NM_EXCH)) return "통화 없음";
				double.TryParse(dtH.Rows[0]["AM_EX"].ToString(), out outValue);
				AM_EX = string.Format("{0:0.00}", outValue);
				double.TryParse(dtH.Rows[0]["AM_CHARGE"].ToString(), out outValue);
				AM_CHARGE = string.Format("{0:0.00}", outValue);
				NO_GIR = dtH.Rows[0]["NO_GIR"].ToString().Trim(); if (string.IsNullOrEmpty(NO_GIR)) return "협조전 번호 없음";
				ADD_CONSIGNEE = dtH.Rows[0]["DC_ADDRESS"].ToString().Trim(); if (string.IsNullOrEmpty(ADD_CONSIGNEE)) return "주소 없음";
				NM_ARRIVER_COUNTRY = dtH.Rows[0]["NM_ARRIVER_COUNTRY"].ToString().Trim(); if (string.IsNullOrEmpty(NM_ARRIVER_COUNTRY)) return "국가 정보가 없습니다.";
				CITY = dtH.Rows[0]["PORT_ARRIVER"].ToString().Trim(); if (string.IsNullOrEmpty(CITY)) return "도시 정보 없음";
				CD_COUNTRY = dtH.Rows[0]["ARRIVER_COUNTRY"].ToString().Trim();
				TOTAL_COUNT = dtH.Rows[0]["QT_PACK"].ToString().Trim();
				double.TryParse(dtH.Rows[0]["QT_GROSS_WEIGHT"].ToString(), out outValue);
				TOTAL_WEIGHT = string.Format("{0:0.00}", outValue);
				CD_PRODUCT = dtH.Rows[0]["CD_PRODUCT"].ToString().Trim();
				BAN = dtH.Rows[0]["BAN"].ToString().Trim();
				NO_IO = dtH.Rows[0]["NO_IO"].ToString().Trim(); if (string.IsNullOrEmpty(NO_IO)) return "미출고(인보이스 번호 없음)";
				DT_GIR = dtH.Rows[0]["DT_GIR"].ToString().Trim();
				CD_POST = dtH.Rows[0]["PC"].ToString().Trim();
				YN_DUTY = dtH.Rows[0]["YN_DUTY"].ToString().Trim();
			}


			DataRow[] row = dt.Select("CD_FLAG1 LIKE '%" + CD_COUNTRY + "' AND CD_FLAG3 = 'Y'");

			if (row.Length > 0 && string.IsNullOrEmpty(CD_POST))
			{
				return "우편번호 필수 국가 입니다.";
			}

			//if (CD_COMPANY.Equals("S100"))
			//	CD_COMPANY = "K100";


			// consignee 주소 분배
			string ADD_CONSIGNEE1 = string.Empty;
			string ADD_CONSIGNEE2 = string.Empty;
			string ADD_CONSIGNEE3 = string.Empty;

			int addLen = ADD_CONSIGNEE.Length / 3;

			if (addLen > 45)
				return "주소 확인(주소 길이가 깁니다) / " + addLen.ToString();

			ADD_CONSIGNEE1 = ADD_CONSIGNEE.Substring(0, addLen);
			ADD_CONSIGNEE2 = ADD_CONSIGNEE.Substring(addLen, addLen);
			ADD_CONSIGNEE3 = ADD_CONSIGNEE.Substring(addLen * 2, ADD_CONSIGNEE.Length - (addLen * 2));


			// 테스트 용
			//string dhlID = "v62_8iHIcZeol6";			// test
			//string dhlPW = "kgBEzSNDAa";                // test
			//string dhlURL = "https://xmlpitest-ea.dhl.com/XMLShippingServlet";


			string dhlID = "v62_gTmZBq5rdF";
			string dhlPW = "n0bqUshCHw";
			string dhlURL = "https://xmlpi-ea.dhl.com/XMLShippingServlet";

			string dhlAccountNum = string.Empty;

			if (CD_COMPANY.Equals("K100"))
				dhlAccountNum = "590010331";
			else if (CD_COMPANY.Equals("S100"))
				dhlAccountNum = "590010331";
			else if (CD_COMPANY.Equals("K200"))
				dhlAccountNum = "590223885";
			//590223885


			XmlDocument xmlD = new XmlDocument();

			XmlDeclaration decl = xmlD.CreateXmlDeclaration("1.0", "utf-8", null); // null이나 string.empty일 경우에는 표시를 하지 않음
			xmlD.InsertBefore(decl, xmlD.DocumentElement);

			XmlNode xmlRoot = xmlD.CreateElement("req", "ShipmentRequest", "http://www.dhl.com");
			xmlD.AppendChild(xmlRoot);


			// Attribute 추가
			XmlAttribute rootAttschemaVersion = xmlD.CreateAttribute("schemaVersion");
			rootAttschemaVersion.Value = "10.0";
			xmlRoot.Attributes.Append(rootAttschemaVersion);

			XmlAttribute rootAttschemaLocation = xmlD.CreateAttribute("xsi:schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
			rootAttschemaLocation.Value = "http://www.dhl.com ship-val-global-req.xsd";
			xmlRoot.Attributes.Append(rootAttschemaLocation);



			// Request
			XmlNode xmlRootRequest = xmlD.CreateElement("Request");
			{
				xmlRoot.AppendChild(xmlRootRequest);

				// Request ServiceHeader
				XmlNode xmlRootServiceHeader = xmlD.CreateElement("ServiceHeader");
				{
					xmlRootRequest.AppendChild(xmlRootServiceHeader);

					// MessageTime
					XmlNode xmlRootServiceHeaderMessageTime = xmlD.CreateElement("MessageTime");
					{
						xmlRootServiceHeaderMessageTime.InnerText = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss.fff") + "+09:00";
					}

					// MessageReference
					XmlNode xmlRootServiceHeaderMessageReference = xmlD.CreateElement("MessageReference");
					{
						// DO   min 28 max 32   변경함 20201118 NO_GIR ->  NO_IO 10
						xmlRootServiceHeaderMessageReference.InnerText = NO_IO + "000000" + DateTime.Now.ToString("yyyyMMddhhmmss");
					}

					// SiteID
					XmlNode xmlRootServiceHeaderSiteID = xmlD.CreateElement("SiteID");
					{
						xmlRootServiceHeaderSiteID.InnerText = dhlID;
					}

					// Password
					XmlNode xmlRootServiceHeaderPassword = xmlD.CreateElement("Password");
					{
						xmlRootServiceHeaderPassword.InnerText = dhlPW;
					}

					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderMessageTime);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderMessageReference);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderSiteID);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderPassword);
				}

				// Request MetaData
				XmlNode xmlRootMetaData = xmlD.CreateElement("MetaData");
				{
					xmlRootRequest.AppendChild(xmlRootMetaData);

					// SoftwareName
					XmlNode xmlRootSoftwareName = xmlD.CreateElement("SoftwareName");
					{
						xmlRootSoftwareName.InnerText = "3PV";
					}

					// SoftwareVersion
					XmlNode xmlRootSoftwareVersion = xmlD.CreateElement("SoftwareVersion");
					{
						xmlRootSoftwareVersion.InnerText = "10.0";
					}

					xmlRootMetaData.AppendChild(xmlRootSoftwareName);
					xmlRootMetaData.AppendChild(xmlRootSoftwareName);
					xmlRootMetaData.AppendChild(xmlRootSoftwareVersion);
				}
			}


			// RegionCode
			XmlNode xmlRootRegionCode = xmlD.CreateElement("RegionCode");
			{
				xmlRootRegionCode.InnerText = "AP"; // 한국 발송이면 AP고정
			}
			xmlRoot.AppendChild(xmlRootRegionCode);

			// NewShipper
			//XmlNode xmlRootNewShipper = xmlD.CreateElement("NewShipper");
			//{
			//	xmlRootNewShipper.InnerText = "N";
			//}
			//xmlRoot.AppendChild(xmlRootNewShipper);

			// LanguageCode
			XmlNode xmlRootLanguageCode = xmlD.CreateElement("LanguageCode");
			{
				xmlRootLanguageCode.InnerText = "ko";
			}
			xmlRoot.AppendChild(xmlRootLanguageCode);

			// PiecesEnabled
			//XmlNode xmlRootPiecesEnabled = xmlD.CreateElement("PiecesEnabled");
			//{
			//	xmlRootPiecesEnabled.InnerText = "Y";
			//}
			//xmlRoot.AppendChild(xmlRootPiecesEnabled);


			// Billing
			XmlNode xmlRootBilling = xmlD.CreateElement("Billing");
			{
				xmlRoot.AppendChild(xmlRootBilling);

				// ServiceHeader
				XmlNode xmlRootBillingServiceHeader = xmlD.CreateElement("ShipperAccountNumber");
				{
					xmlRootBillingServiceHeader.InnerText = dhlAccountNum;
				}

				// ShippingPaymentType
				XmlNode xmlRootBillingShippingPaymentType = xmlD.CreateElement("ShippingPaymentType");
				{
					// S - 발송자 지불건, R - 수취인 지불건, T - 제 3자 지불건
					if (string.IsNullOrEmpty(BAN))
						xmlRootBillingShippingPaymentType.InnerText = "S";
					else
						xmlRootBillingShippingPaymentType.InnerText = "R";
				}


				// BillingAccountNumber
				XmlNode xmlRootBillingBillingAccountNumber = xmlD.CreateElement("BillingAccountNumber");
				{
					if (string.IsNullOrEmpty(BAN))
						xmlRootBillingBillingAccountNumber.InnerText = dhlAccountNum;
					else
						xmlRootBillingBillingAccountNumber.InnerText = BAN.Replace(" ", "").Trim();
				}

				xmlRootBilling.AppendChild(xmlRootBillingServiceHeader);
				xmlRootBilling.AppendChild(xmlRootBillingShippingPaymentType);
				xmlRootBilling.AppendChild(xmlRootBillingBillingAccountNumber);


				if (!string.IsNullOrEmpty(BAN))
				{
					XmlNode xmlRootBillingDutyAccountNumber = xmlD.CreateElement("DutyAccountNumber");
					{
						xmlRootBillingDutyAccountNumber.InnerText = BAN;
					}
					//xmlRootBilling.AppendChild(xmlRootBillingDutyPaymentType);
					xmlRootBilling.AppendChild(xmlRootBillingDutyAccountNumber);
				}


				//// 임시 DUTY 관련
				//if (YN_DUTY.Equals("Y"))
				//{
				//	// DutyPaymentType
				//	//XmlNode xmlRootBillingDutyPaymentType = xmlD.CreateElement("DutyPaymentType");
				//	//{
				//	//		xmlRootBillingDutyPaymentType.InnerText = "S";
				//	//}
				//	//xmlRootBilling.AppendChild(xmlRootBillingDutyPaymentType);
				//
				//	XmlNode xmlRootBillingDutyAccountNumber = xmlD.CreateElement("DutyAccountNumber");
				//	{
				//		xmlRootBillingDutyAccountNumber.InnerText = BAN;
				//	}
				//	//xmlRootBilling.AppendChild(xmlRootBillingDutyPaymentType);
				//	xmlRootBilling.AppendChild(xmlRootBillingDutyAccountNumber);
				//}
				//else
				//{
				//	// DutyPaymentType
				//	XmlNode xmlRootBillingDutyPaymentType = xmlD.CreateElement("DutyPaymentType");
				//	{
				//		// S - 발송자 지불건, R - 수취인 지불건, T - 제 3자 지불건
				//		//if (NO_GIR == "DO21020470")
				//		//	xmlRootBillingDutyPaymentType.InnerText = "S";
				//		//else
				//			xmlRootBillingDutyPaymentType.InnerText = "R";
				//	}
				//	xmlRootBilling.AppendChild(xmlRootBillingDutyPaymentType);
				//}
			}


			// Consignee 수취인
			XmlNode xmlRootConsignee = xmlD.CreateElement("Consignee");
			{
				xmlRoot.AppendChild(xmlRootConsignee);

				// CompanyName
				XmlNode xmlRootConsigneeCompanyName = xmlD.CreateElement("CompanyName");
				{
					// NM_CONSIGNEE
					if (NM_CONSIGNEE.Length <= 60)
						xmlRootConsigneeCompanyName.InnerText = NM_CONSIGNEE;
					else
						xmlRootConsigneeCompanyName.InnerText = NM_CONSIGNEE.Substring(0, 60);
				}

				// AddressLine   // 라인마다 max 45
				XmlNode xmlRootConsigneeAddressLine1 = xmlD.CreateElement("AddressLine1");
				{
					// ADD1_CONSIGNEE
					xmlRootConsigneeAddressLine1.InnerText = ADD_CONSIGNEE1;
				}

				XmlNode xmlRootConsigneeAddressLine2 = xmlD.CreateElement("AddressLine2");
				{
					// ADD1_CONSIGNEE
					xmlRootConsigneeAddressLine2.InnerText = ADD_CONSIGNEE2;
				}

				XmlNode xmlRootConsigneeAddressLine3 = xmlD.CreateElement("AddressLine3");
				{
					// ADD1_CONSIGNEE
					xmlRootConsigneeAddressLine3.InnerText = ADD_CONSIGNEE3;
				}

				// City
				XmlNode xmlRootConsigneeCity = xmlD.CreateElement("City");
				{
					xmlRootConsigneeCity.InnerText = CITY.Replace(",", "");
				}



				// CountryCode
				XmlNode xmlRootConsigneeCountryCode = xmlD.CreateElement("CountryCode");
				{
					xmlRootConsigneeCountryCode.InnerText = CD_COUNTRY;
				}

				// CountryName
				XmlNode xmlRootConsigneeCountryName = xmlD.CreateElement("CountryName");
				{
					xmlRootConsigneeCountryName.InnerText = NM_ARRIVER_COUNTRY;
				}

				xmlRootConsignee.AppendChild(xmlRootConsigneeCompanyName);
				xmlRootConsignee.AppendChild(xmlRootConsigneeAddressLine1);
				if (!string.IsNullOrEmpty(ADD_CONSIGNEE2))
					xmlRootConsignee.AppendChild(xmlRootConsigneeAddressLine2);
				if (!string.IsNullOrEmpty(ADD_CONSIGNEE3))
					xmlRootConsignee.AppendChild(xmlRootConsigneeAddressLine3);
				xmlRootConsignee.AppendChild(xmlRootConsigneeCity);

				if (!string.IsNullOrEmpty(CD_POST))
				{
					XmlNode xmlRootConsigneePostalCode = xmlD.CreateElement("PostalCode");
					{
						xmlRootConsigneePostalCode.InnerText = CD_POST;
					}

					xmlRootConsignee.AppendChild(xmlRootConsigneePostalCode);
				}
				xmlRootConsignee.AppendChild(xmlRootConsigneeCountryCode);
				xmlRootConsignee.AppendChild(xmlRootConsigneeCountryName);


				// Contact
				XmlNode xmlRootConsigneeContact = xmlD.CreateElement("Contact");
				{
					xmlRootConsignee.AppendChild(xmlRootConsigneeContact);

					// PersonName
					XmlNode xmlRootConsigneeContactPersonName = xmlD.CreateElement("PersonName");
					{
						// PIC
						if (!string.IsNullOrEmpty(PIC_CONSIGNEE))
						{
							if (PIC_CONSIGNEE.Length < 35)
								xmlRootConsigneeContactPersonName.InnerText = PIC_CONSIGNEE;
							else
								xmlRootConsigneeContactPersonName.InnerText = PIC_CONSIGNEE.Left(34);
						}
						else
							xmlRootConsigneeContactPersonName.InnerText = "N/A";

					}

					// PhoneNumber
					XmlNode xmlRootConsigneeContactPhoneNumber = xmlD.CreateElement("PhoneNumber");
					{
						// TEL
						if (TEL_CONSIGNEE.Length < 25)
							xmlRootConsigneeContactPhoneNumber.InnerText = TEL_CONSIGNEE;
						else
							xmlRootConsigneeContactPhoneNumber.InnerText = TEL_CONSIGNEE.Left(24);

					}


					//// Email
					//XmlNode xmlRootConsigneeContactEmail = xmlD.CreateElement("Email");
					//{
					//	//EMAIL
					//	if(EMAIL_CONSIGNEE.Length > )
					//	xmlRootConsigneeContactEmail.InnerText = EMAIL_CONSIGNEE;
					//}

					xmlRootConsigneeContact.AppendChild(xmlRootConsigneeContactPersonName);
					xmlRootConsigneeContact.AppendChild(xmlRootConsigneeContactPhoneNumber);
					//	xmlRootConsigneeContact.AppendChild(xmlRootConsigneeContactEmail);
				}
			}


			// Commodity
			XmlNode xmlRootCommodity = xmlD.CreateElement("Commodity");
			{
				xmlRoot.AppendChild(xmlRootCommodity);

				// CommodityCode
				XmlNode xmlRootCommodityCommodityCode = xmlD.CreateElement("CommodityCode");
				{
					//NM_HS
					xmlRootCommodityCommodityCode.InnerText = NM_HS;
				}

				// CommodityName
				XmlNode xmlRootCommodityCommodityName = xmlD.CreateElement("CommodityName");
				{
					// NM_ITEM_PARTNER
					if (NM_ITEM_PARTNER.Length <= 35)
						xmlRootCommodityCommodityName.InnerText = NM_ITEM_PARTNER;
					else
						xmlRootCommodityCommodityName.InnerText = NM_ITEM_PARTNER.Left(34);
				}

				xmlRootCommodity.AppendChild(xmlRootCommodityCommodityCode);
				xmlRootCommodity.AppendChild(xmlRootCommodityCommodityName);
			}


			// Dutiable
			XmlNode xmlRootDutiable = xmlD.CreateElement("Dutiable");
			{
				xmlRoot.AppendChild(xmlRootDutiable);

				// DeclaredValue
				XmlNode xmlRootDutiableDeclaredValue = xmlD.CreateElement("DeclaredValue");
				{
					xmlRootDutiableDeclaredValue.InnerText = AM_EX;
				}

				// DeclaredCurrency
				XmlNode xmlRootDutiableDeclaredCurrency = xmlD.CreateElement("DeclaredCurrency");
				{
					// NM_EXCH
					xmlRootDutiableDeclaredCurrency.InnerText = NM_EXCH;
				}

				// TermsOfTrade
				XmlNode xmlRootDutiableTermsOfTrade = xmlD.CreateElement("TermsOfTrade");
				{

					if (YN_INSURANCE.Equals("Y"))
					{
						xmlRootDutiableTermsOfTrade.InnerText = "DDP";
					}
					else
					{
						xmlRootDutiableTermsOfTrade.InnerText = "DAP";
					}

					//// V10 수정 DAP, DDP
					//if (string.IsNullOrWhiteSpace(DDP))
					//{
					//	xmlRootDutiableTermsOfTrade.InnerText = "DAP";
					//}
					//else
					//{
					//	xmlRootDutiableTermsOfTrade.InnerText = DDP;
					//}
				}


				xmlRootDutiable.AppendChild(xmlRootDutiableDeclaredValue);
				xmlRootDutiable.AppendChild(xmlRootDutiableDeclaredCurrency);
				xmlRootDutiable.AppendChild(xmlRootDutiableTermsOfTrade);
			}


			// UseDHLInvoice
			XmlNode xmlRootUseDHLInvoice = xmlD.CreateElement("UseDHLInvoice");
			{
				xmlRootUseDHLInvoice.InnerText = "N";
			}
			xmlRoot.AppendChild(xmlRootUseDHLInvoice);


			//if (Convert.ToDouble(AM_CHARGE) > 0)
			//{
			//	// OtherCharges     v10
			//	XmlNode xmlRootExportDeclarationOtherCharges = xmlD.CreateElement("OtherCharges");
			//	{
			//		xmlRoot.AppendChild(xmlRootExportDeclarationOtherCharges);

			//		XmlNode xmlRootExportDeclarationOtherChargesOtherCharges = xmlD.CreateElement("OtherCharge");
			//		{

			//			XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption = xmlD.CreateElement("OtherChargeCaption");
			//			{
			//				xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption.InnerText = "Freight Charges";
			//			}

			//			xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption);


			//			XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue = xmlD.CreateElement("OtherChargeValue");
			//			{
			//				xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue.InnerText = AM_CHARGE;
			//			}

			//			xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue);

			//			XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType = xmlD.CreateElement("OtherChargeType");
			//			{
			//				xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType.InnerText = "FRCST";
			//			}

			//			xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType);
			//		}

			//		//xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationOtherChargesOtherCharges);

			//		xmlRootExportDeclarationOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherCharges);
			//	}
			//}


			// ExportDeclaration
			XmlNode xmlRootExportDeclaration = xmlD.CreateElement("ExportDeclaration");
			{
				xmlRoot.AppendChild(xmlRootExportDeclaration);


				// InvoiceNumber			
				XmlNode xmlRootExportDeclarationInvoiceNumber = xmlD.CreateElement("InvoiceNumber");
				{
					xmlRootExportDeclarationInvoiceNumber.InnerText = NO_IO;
				}

				// InvoiceDate			
				XmlNode xmlRootExportDeclarationInvoiceDate = xmlD.CreateElement("InvoiceDate");
				{
					xmlRootExportDeclarationInvoiceDate.InnerText = DT_GIR.Left(4) + "-" + DT_GIR.Substring(4, 2) + "-" + DT_GIR.Right(2);
				}

				xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationInvoiceNumber);
				xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationInvoiceDate);



				if (Convert.ToDouble(AM_CHARGE) > 0)
				{
					// OtherCharges     v10
					XmlNode xmlRootExportDeclarationOtherCharges = xmlD.CreateElement("OtherCharges");
					{
						xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationOtherCharges);

						XmlNode xmlRootExportDeclarationOtherChargesOtherCharges = xmlD.CreateElement("OtherCharge");
						{

							XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption = xmlD.CreateElement("OtherChargeCaption");
							{
								xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption.InnerText = "Freight Charges";
							}

							xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption);


							XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue = xmlD.CreateElement("OtherChargeValue");
							{
								xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue.InnerText = AM_CHARGE;
							}

							xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue);

							XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType = xmlD.CreateElement("OtherChargeType");
							{
								xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType.InnerText = "FRCST";
							}

							xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType);
						}

						//xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationOtherChargesOtherCharges);

						xmlRootExportDeclarationOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherCharges);
					}
				}


				//if (Convert.ToDouble(AM_CHARGE) > 0)
				//{
				//	// OtherCharges     v10
				//	XmlNode xmlRootExportDeclarationOtherCharges = xmlD.CreateElement("OtherCharges");
				//	{
				//		xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationOtherCharges);

				//		XmlNode xmlRootExportDeclarationOtherChargesOtherCharges = xmlD.CreateElement("OtherCharges");
				//		{

				//			XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption = xmlD.CreateElement("OtherChargeCaption");
				//			{
				//				xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption.InnerText = "Freight Charges";
				//			}

				//			xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeCaption);


				//			XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue = xmlD.CreateElement("OtherChargeValue");
				//			{
				//				xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue.InnerText = AM_CHARGE;
				//			}

				//			xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeValue);

				//			XmlNode xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType = xmlD.CreateElement("OtherChargeType");
				//			{
				//				xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType.InnerText = "FRCST";
				//			}

				//			xmlRootExportDeclarationOtherChargesOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherChargesOtherChargeType);
				//		}

				//		//xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationOtherChargesOtherCharges);

				//		xmlRootExportDeclarationOtherCharges.AppendChild(xmlRootExportDeclarationOtherChargesOtherCharges);
				//	}



				//}


				// InvoiceDate			  v10 수정 
				//XmlNode xmlRootExportDeclarationOtherCharges2 = xmlD.CreateElement("OtherCharges2");
				//{
				//	xmlRootExportDeclarationOtherCharges2.InnerText = AM_CHARGE;
				//}
				//
				//xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationOtherCharges2);



				// 반복 돌려야함 라인만큼
				for (int r = 0; r < dtInvoice.Rows.Count; r++)
				{
					// ExportLineItem			
					XmlNode xmlRootExportDeclarationExportLineItem = xmlD.CreateElement("ExportLineItem");
					{
						xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationExportLineItem);

						// LineNumber			
						XmlNode xmlRootExportDeclarationExportLineItemLineNumber = xmlD.CreateElement("LineNumber");
						{
							xmlRootExportDeclarationExportLineItemLineNumber.InnerText = dtInvoice.Rows[r]["SEQ_GIR"].ToString();
						}

						// Quantity			
						XmlNode xmlRootExportDeclarationExportLineItemQuantity = xmlD.CreateElement("Quantity");
						{
							// 수정 : 부대비용 제외하고 수량이 안넘어와서 그냥 row카운트로 변경
							//							double.TryParse(dtInvoice.Rows.Count.ToString(), out outValue);

							double.TryParse(dtInvoice.Rows[r]["QT_GIR"].ToString(), out outValue);
							xmlRootExportDeclarationExportLineItemQuantity.InnerText = string.Format("{0:0}", outValue);
						}

						// QuantityUnit			
						XmlNode xmlRootExportDeclarationExportLineItemQuantityUnit = xmlD.CreateElement("QuantityUnit");
						{
							string itemUnit = dtInvoice.Rows[r]["UNIT"].ToString();


							//if (itemUnit.Contains("MTR"))
							//	itemUnit = "M";
							//else if (itemUnit.StartsWith("ROL"))
							//	itemUnit = "ROLL";
							//else if (itemUnit.StartsWith("KIT"))
							//	itemUnit = "SET";
							//else if (itemUnit.Equals("PS"))
							//	itemUnit = "PCS";
							xmlRootExportDeclarationExportLineItemQuantityUnit.InnerText = UnitConvert_dhl.ConvertDHL(itemUnit);
						}

						// Description			
						XmlNode xmlRootExportDeclarationExportLineItemDescription = xmlD.CreateElement("Description");
						{
							if (dtInvoice.Rows[r]["NM_ITEM_PARTNER"].ToString().Length < 75)
								xmlRootExportDeclarationExportLineItemDescription.InnerText = dtInvoice.Rows[r]["NM_ITEM_PARTNER"].ToString();
							else
								xmlRootExportDeclarationExportLineItemDescription.InnerText = dtInvoice.Rows[r]["NM_ITEM_PARTNER"].ToString().Left(75);
						}

						// Value			

						XmlNode xmlRootExportDeclarationExportLineItemValue = xmlD.CreateElement("Value");
						{
							double.TryParse(dtInvoice.Rows[r]["UM"].ToString(), out outValue);
							//xmlRootExportDeclarationExportLineItemValue.InnerText = string.Format("{0:0.00}", Math.Round(outValue, 2));
							//xmlRootExportDeclarationExportLineItemValue.InnerText = string.Format("{0:0.0000}", outValue);
							xmlRootExportDeclarationExportLineItemValue.InnerText = string.Format("{0:0.000}", outValue);
						}

						xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemLineNumber);
						xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemQuantity);
						xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemQuantityUnit);
						xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemDescription);
						xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemValue);

						// Weight			
						XmlNode xmlRootExportDeclarationExportLineItemWeight = xmlD.CreateElement("Weight");
						{
							xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemWeight);

							// Weight
							XmlNode xmlRootExportDeclarationExportLineItemWeightWeight = xmlD.CreateElement("Weight");
							{
								double.TryParse(TOTAL_WEIGHT, out outValue);
								xmlRootExportDeclarationExportLineItemWeightWeight.InnerText = string.Format("{0:0.00}", outValue / dtInvoice.Rows.Count);
							}

							// WeightUnit
							XmlNode xmlRootExportDeclarationExportLineItemWeightWeightUnit = xmlD.CreateElement("WeightUnit");
							{
								xmlRootExportDeclarationExportLineItemWeightWeightUnit.InnerText = "K";
							}

							xmlRootExportDeclarationExportLineItemWeight.AppendChild(xmlRootExportDeclarationExportLineItemWeightWeight);
							xmlRootExportDeclarationExportLineItemWeight.AppendChild(xmlRootExportDeclarationExportLineItemWeightWeightUnit);
						}


						// GrossWeightWeight			
						XmlNode xmlRootExportDeclarationExportLineItemGrossWeight = xmlD.CreateElement("GrossWeight");
						{
							xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemGrossWeight);

							// Weight
							XmlNode xmlRootExportDeclarationExportLineItemGrossWeightWeight = xmlD.CreateElement("Weight");
							{
								double.TryParse(TOTAL_WEIGHT, out outValue);
								xmlRootExportDeclarationExportLineItemGrossWeightWeight.InnerText = string.Format("{0:0.00}", outValue / dtInvoice.Rows.Count);
							}

							// WeightUnit
							XmlNode xmlRootExportDeclarationExportLineItemGrossWeightWeightUnit = xmlD.CreateElement("WeightUnit");
							{
								xmlRootExportDeclarationExportLineItemGrossWeightWeightUnit.InnerText = "K";
							}

							xmlRootExportDeclarationExportLineItemGrossWeight.AppendChild(xmlRootExportDeclarationExportLineItemGrossWeightWeight);
							xmlRootExportDeclarationExportLineItemGrossWeight.AppendChild(xmlRootExportDeclarationExportLineItemGrossWeightWeightUnit);
						}


						// v10 수정 필요
						// ManufactureCountryCode			
						XmlNode xmlRootExportDeclarationExportLineItemManufactureCountryCode = xmlD.CreateElement("ManufactureCountryCode");
						{
							xmlRootExportDeclarationExportLineItemManufactureCountryCode.InnerText = "KR";
						}

						// ManufactureCountryName			
						XmlNode xmlRootExportDeclarationExportLineItemManufactureCountryName = xmlD.CreateElement("ManufactureCountryName");
						{
							xmlRootExportDeclarationExportLineItemManufactureCountryName.InnerText = "Korea, Republic";
						}

						xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemManufactureCountryCode);
						xmlRootExportDeclarationExportLineItem.AppendChild(xmlRootExportDeclarationExportLineItemManufactureCountryName);
					}
				}










				// PlaceOfIncoterm	v10 수정 항목		
				XmlNode xmlRootExportDeclarationPlaceOfIncoterm = xmlD.CreateElement("PlaceOfIncoterm");
				{
					xmlRootExportDeclarationPlaceOfIncoterm.InnerText = CITY;
				}

				xmlRootExportDeclaration.AppendChild(xmlRootExportDeclarationPlaceOfIncoterm);


			}




			// Reference
			XmlNode xmlRootReference = xmlD.CreateElement("Reference");
			{
				xmlRoot.AppendChild(xmlRootReference);


				// ReferenceID			
				XmlNode xmlRootReferenceReferenceID = xmlD.CreateElement("ReferenceID");
				{
					// NO_GIR -> NO_IO 변경함 20201118
					xmlRootReferenceReferenceID.InnerText = NO_IO;
				}

				xmlRootReference.AppendChild(xmlRootReferenceReferenceID);
			}



			// ShipmentDetails
			XmlNode xmlRootShipmentDetails = xmlD.CreateElement("ShipmentDetails");
			{
				xmlRoot.AppendChild(xmlRootShipmentDetails);

				// NumberOfPieces	max:99
				//XmlNode xmlRootShipmentDetailsNumberOfPieces = xmlD.CreateElement("NumberOfPieces");
				//{
				//	xmlRootShipmentDetailsNumberOfPieces.InnerText = TOTAL_COUNT;
				//}

				// Pieces
				XmlNode xmlRootShipmentDetailsPieces = xmlD.CreateElement("Pieces");
				{
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsPieces);

					for (int r = 0; r < dtL.Rows.Count; r++)
					{
						// Pieces
						XmlNode xmlRootShipmentDetailsPiecesPiece = xmlD.CreateElement("Piece");
						{
							xmlRootShipmentDetailsPieces.AppendChild(xmlRootShipmentDetailsPiecesPiece);

							// Weight
							XmlNode xmlRootShipmentDetailsPiecesPieceWeight = xmlD.CreateElement("Weight");
							{
								double.TryParse(dtL.Rows[r]["QT_GROSS_WEIGHT"].ToString(), out outValue);
								xmlRootShipmentDetailsPiecesPieceWeight.InnerText = string.Format("{0:0.00}", outValue);
							}

							// Width
							XmlNode xmlRootShipmentDetailsPiecesPieceWidth = xmlD.CreateElement("Width");
							{
								double.TryParse(dtL.Rows[r]["QT_WIDTH"].ToString(), out outValue);
								xmlRootShipmentDetailsPiecesPieceWidth.InnerText = string.Format("{0:0}", outValue * 0.1);
							}

							// Height
							XmlNode xmlRootShipmentDetailsPiecesPieceHeight = xmlD.CreateElement("Height");
							{
								double.TryParse(dtL.Rows[r]["QT_HEIGHT"].ToString(), out outValue);
								xmlRootShipmentDetailsPiecesPieceHeight.InnerText = string.Format("{0:0}", outValue * 0.1);
							}

							// Depth
							XmlNode xmlRootShipmentDetailsPiecesPieceDepth = xmlD.CreateElement("Depth");
							{
								double.TryParse(dtL.Rows[r]["QT_LENGTH"].ToString(), out outValue);
								xmlRootShipmentDetailsPiecesPieceDepth.InnerText = string.Format("{0:0}", outValue * 0.1);
							}

							// PieceContents	35 CHARS 제한
							XmlNode xmlRootShipmentDetailsPiecesPiecePieceContents = xmlD.CreateElement("PieceContents");
							{
								if (dtL.Rows[r]["NM_ITEM_PARTNER"].ToString().Length > 35)
									xmlRootShipmentDetailsPiecesPiecePieceContents.InnerText = dtL.Rows[r]["NM_ITEM_PARTNER"].ToString().Replace(",", " ").Left(34);
								else
									xmlRootShipmentDetailsPiecesPiecePieceContents.InnerText = dtL.Rows[r]["NM_ITEM_PARTNER"].ToString().Replace(",", " ");
							}

							xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPieceWeight);
							xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPieceWidth);
							xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPieceHeight);
							xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPieceDepth);
							xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPiecePieceContents);
						}
					}

					// Weight
					//XmlNode xmlRootShipmentDetailsWeight = xmlD.CreateElement("Weight");
					//{
					//	xmlRootShipmentDetailsWeight.InnerText = TOTAL_WEIGHT;
					//}

					// WeightUnit
					XmlNode xmlRootShipmentDetailsWeightUnit = xmlD.CreateElement("WeightUnit");
					{
						xmlRootShipmentDetailsWeightUnit.InnerText = "K";
					}

					// GlobalProductCode
					XmlNode xmlRootShipmentDetailsGlobalProductCode = xmlD.CreateElement("GlobalProductCode");
					{
						xmlRootShipmentDetailsGlobalProductCode.InnerText = CD_PRODUCT;
					}

					//// LocalProductCode
					//XmlNode xmlRootShipmentDetailsLocalProductCode = xmlD.CreateElement("LocalProductCode");
					//{
					//	xmlRootShipmentDetailsLocalProductCode.InnerText = "P";
					//}

					// Date
					XmlNode xmlRootShipmentDetailsDate = xmlD.CreateElement("Date");
					{
						xmlRootShipmentDetailsDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
					}

					// Contents   90 CHARS 제한
					XmlNode xmlRootShipmentDetailsContents = xmlD.CreateElement("Contents");
					{
						string contentsStr = string.Empty;
						//if (NM_ITEM_PARTNER.Length < 35)
						contentsStr = @"SHIP'S SPARES IN TRANSIT" + Environment.NewLine + "MASTER OF " + dtH.Rows[0]["NM_VESSEL"] + Environment.NewLine + NM_ITEM_PARTNER;
						//else
						//	xmlRootShipmentDetailsContents.InnerText = @"SHIP'S SPARES IN TRANSIT" + Environment.NewLine + "MASTER OF " + dtH.Rows[0]["NM_VESSEL"] + Environment.NewLine + NM_ITEM_PARTNER.Replace(",","").Left(34);


						if (contentsStr.Length > 90)
							xmlRootShipmentDetailsContents.InnerText = contentsStr.Left(90);
						else
							xmlRootShipmentDetailsContents.InnerText = contentsStr;
					}

					// DimensionUnit	I = INCHES, C = CENTREMETRES
					XmlNode xmlRootShipmentDetailsDimensionUnit = xmlD.CreateElement("DimensionUnit");
					{
						xmlRootShipmentDetailsDimensionUnit.InnerText = "C";
					}

					// IsDutiable
					XmlNode xmlRootShipmentDetailsIsDutiable = xmlD.CreateElement("IsDutiable");
					{
						xmlRootShipmentDetailsIsDutiable.InnerText = "Y";
					}

					// CurrencyCode   통화코드
					XmlNode xmlRootShipmentDetailsIsCurrencyCode = xmlD.CreateElement("CurrencyCode");
					{
						// NM_EXCH
						xmlRootShipmentDetailsIsCurrencyCode.InnerText = NM_EXCH;
					}



					//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsNumberOfPieces);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsPieces);
					//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsWeight);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsWeightUnit);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsGlobalProductCode);
					//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsLocalProductCode);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsDate);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsContents);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsDimensionUnit);

					if (YN_INSURANCE.Equals("Y"))
					{
						// InsuredAmount   보험금액
						XmlNode xmlRootShipmentDetailsInsuredAmount = xmlD.CreateElement("InsuredAmount");
						{
							// NM_EXCH
							xmlRootShipmentDetailsInsuredAmount.InnerText = AM_EX;
						}
						xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsInsuredAmount);
					}

					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsIsDutiable);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsIsCurrencyCode);




				}
			}


			// Shipper
			XmlNode xmlRootShipper = xmlD.CreateElement("Shipper");
			{
				xmlRoot.AppendChild(xmlRootShipper);

				// ShipperID	30 CHARACTERS
				XmlNode xmlRootShipperShipperID = xmlD.CreateElement("ShipperID");
				{
					if (CD_COMPANY.Equals("K100"))
						xmlRootShipperShipperID.InnerText = "590010331";
					else if (CD_COMPANY.Equals("S100"))
						xmlRootShipperShipperID.InnerText = "590010331";
					else
						xmlRootShipperShipperID.InnerText = "590223885";
				}

				// CompanyName
				XmlNode xmlRootShipperCompanyName = xmlD.CreateElement("CompanyName");
				{
					if (CD_COMPANY.Equals("K100"))
						xmlRootShipperCompanyName.InnerText = "DINTEC CO.,LTD";
					else if (CD_COMPANY.Equals("S100"))
						xmlRootShipperCompanyName.InnerText = "DINTEC (SINGAPORE) PTE. LTD.";
					else
						xmlRootShipperCompanyName.InnerText = "DUBHE CO.,LTD";
				}

				// AddressLine1
				XmlNode xmlRootShipperAddressLine1 = xmlD.CreateElement("AddressLine1");
				{
					if (CD_COMPANY.Equals("K100"))
						xmlRootShipperAddressLine1.InnerText = "309, JUNGANG-DAERO";
					else if (CD_COMPANY.Equals("K100"))
						xmlRootShipperAddressLine1.InnerText = "309, JUNGANG-DAERO";
					else
						xmlRootShipperAddressLine1.InnerText = "3FL, 297, JUNGANG-DAERO";
				}

				// AddressLine2
				XmlNode xmlRootShipperAddressLine2 = xmlD.CreateElement("AddressLine2");
				{
					if (CD_COMPANY.Equals("K100"))
						xmlRootShipperAddressLine2.InnerText = "DONG-GU, BUSAN, KOREA";
					else if (CD_COMPANY.Equals("K100"))
						xmlRootShipperAddressLine2.InnerText = "DONG-GU, BUSAN, KOREA";
					else
						xmlRootShipperAddressLine2.InnerText = "DONG-GU, BUSAN, KOREA";

				}

				// City
				XmlNode xmlRootShipperCity = xmlD.CreateElement("City");
				{
					xmlRootShipperCity.InnerText = "BUSAN";
				}

				// PostalCode
				XmlNode xmlRootShipperPostalCode = xmlD.CreateElement("PostalCode");
				{
					xmlRootShipperPostalCode.InnerText = "48792";
				}

				// CountryCode
				XmlNode xmlRootShipperCountryCode = xmlD.CreateElement("CountryCode");
				{
					xmlRootShipperCountryCode.InnerText = "KR";
				}

				// CountryName
				XmlNode xmlRootShipperCountryName = xmlD.CreateElement("CountryName");
				{
					xmlRootShipperCountryName.InnerText = "KOREA, REPUBLIC OF (SOUTH K.)";
				}

				xmlRootShipper.AppendChild(xmlRootShipperShipperID);
				xmlRootShipper.AppendChild(xmlRootShipperCompanyName);
				xmlRootShipper.AppendChild(xmlRootShipperAddressLine1);
				xmlRootShipper.AppendChild(xmlRootShipperAddressLine2);
				xmlRootShipper.AppendChild(xmlRootShipperCity);
				xmlRootShipper.AppendChild(xmlRootShipperPostalCode);
				xmlRootShipper.AppendChild(xmlRootShipperCountryCode);
				xmlRootShipper.AppendChild(xmlRootShipperCountryName);


				// Contact
				XmlNode xmlRootShipperContact = xmlD.CreateElement("Contact");
				{
					xmlRootShipper.AppendChild(xmlRootShipperContact);

					// PersonName
					XmlNode xmlRootShipperContactPersonName = xmlD.CreateElement("PersonName");
					{
						if (CD_COMPANY.Equals("K100"))
							xmlRootShipperContactPersonName.InnerText = "Dintec";
						else if (CD_COMPANY.Equals("S100"))
							xmlRootShipperContactPersonName.InnerText = "Dintec Singapore";
						else
							xmlRootShipperContactPersonName.InnerText = "Dubheco";
					}

					// PhoneNumber
					XmlNode xmlRootShipperContactPhoneNumber = xmlD.CreateElement("PhoneNumber");
					{
						if (CD_COMPANY.Equals("K100"))
							xmlRootShipperContactPhoneNumber.InnerText = "+82516641000";
						if (CD_COMPANY.Equals("S100"))
							xmlRootShipperContactPhoneNumber.InnerText = "+6568964434";
						else
							xmlRootShipperContactPhoneNumber.InnerText = "+82514652100";
					}

					// Telex
					XmlNode xmlRootShipperContactTelex = xmlD.CreateElement("Telex");
					{
						if (CD_COMPANY.Equals("K100"))
							xmlRootShipperContactTelex.InnerText = "+820514677907";
						else if (CD_COMPANY.Equals("S100"))
							xmlRootShipperContactTelex.InnerText = "+6568964434";
						else
							xmlRootShipperContactTelex.InnerText = "+820514652105";
					}

					// Email
					XmlNode xmlRootShipperContactEmail = xmlD.CreateElement("Email");
					{
						if (CD_COMPANY.Equals("K100"))
							xmlRootShipperContactEmail.InnerText = "service@dintec.co.kr";
						else if (CD_COMPANY.Equals("S100"))
							xmlRootShipperContactEmail.InnerText = "service@dintec.com.sg";
						else
							xmlRootShipperContactEmail.InnerText = "service@dubheco.com";
					}

					xmlRootShipperContact.AppendChild(xmlRootShipperContactPersonName);
					xmlRootShipperContact.AppendChild(xmlRootShipperContactPhoneNumber);
					xmlRootShipperContact.AppendChild(xmlRootShipperContactTelex);
					xmlRootShipperContact.AppendChild(xmlRootShipperContactEmail);
				}
			}


			if (YN_INSURANCE.Equals("Y"))
			{
				// 보험
				XmlNode xmlRootSpecialService = xmlD.CreateElement("SpecialService");
				{
					xmlRoot.AppendChild(xmlRootSpecialService);

					XmlNode xmlRootSpecialServiceSpecialServiceType = xmlD.CreateElement("SpecialServiceType");
					{
						xmlRootSpecialServiceSpecialServiceType.InnerText = "II";
					}

					xmlRootSpecialService.AppendChild(xmlRootSpecialServiceSpecialServiceType);
				}
			}


			// LabelImageFormat		송장 출력 방식	PDF, EPL2, ZPL2
			XmlNode xmlRootUseLabelImageFormat = xmlD.CreateElement("LabelImageFormat");
			{
				xmlRootUseLabelImageFormat.InnerText = "ZPL2";
			}
			xmlRoot.AppendChild(xmlRootUseLabelImageFormat);


			// xml 값 저장 - TEST 용도
			//xmlD.Save(@"C:\testrequest_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");


			return postXMLDHL(dhlURL, GetXMLAsString(xmlD));


		}
		#endregion 물류부 포장


		#region 물류부 체크
		public static string DHLShipmentValidationServiceCheck(DataTable dtH, DataTable dtL, DataTable dtInvoice)
		{
			DataTable dt = GetDb.Code("MA_B000020");



			string result = string.Empty;

			string CD_COMPANY = dtH.Rows[0]["CD_COMPANY"].ToString().Trim();
			if (string.IsNullOrEmpty(CD_COMPANY)) return "회사코드 없음";
			string NM_CONSIGNEE = dtH.Rows[0]["NM_CONSIGNEE"].ToString().Trim();
			if (string.IsNullOrEmpty(NM_CONSIGNEE)) return "수하인 회사 없음";
			string EMAIL_CONSIGNEE = dtH.Rows[0]["EMAIL"].ToString().Trim();
			string PIC_CONSIGNEE = dtH.Rows[0]["PIC"].ToString().Trim();
			string TEL_CONSIGNEE = dtH.Rows[0]["TEL"].ToString().Replace(" ", "").Trim();
			string NM_HS = dtH.Rows[0]["NM_HS"].ToString().Trim();
			if (string.IsNullOrEmpty(NM_HS)) return "HSCODE 없음";
			string NM_ITEM_PARTNER = dtH.Rows[0]["NM_ITEM_PARTNER"].ToString().Trim();
			string NM_EXCH = dtH.Rows[0]["NM_EXCH"].ToString().Trim();
			if (string.IsNullOrEmpty(NM_EXCH)) return "통화 없음";
			double.TryParse(dtH.Rows[0]["AM_EX"].ToString(), out outValue);
			string AM_EX = string.Format("{0:0.00}", outValue);
			string NO_GIR = dtH.Rows[0]["NO_GIR"].ToString().Trim();
			if (string.IsNullOrEmpty(NO_GIR)) return "협조전 번호 없음";
			string ADD_CONSIGNEE = dtH.Rows[0]["DC_ADDRESS"].ToString().Trim();
			if (string.IsNullOrEmpty(ADD_CONSIGNEE)) return "주소 없음";
			string NM_ARRIVER_COUNTRY = dtH.Rows[0]["NM_ARRIVER_COUNTRY"].ToString().Trim();
			if (string.IsNullOrEmpty(NM_ARRIVER_COUNTRY)) return "국가 정보가 없습니다.";
			string CITY = dtH.Rows[0]["PORT_ARRIVER"].ToString().Trim();
			if (string.IsNullOrEmpty(CITY)) return "도시 정보 없음";
			string CD_COUNTRY = dtH.Rows[0]["ARRIVER_COUNTRY"].ToString().Trim();
			string TOTAL_COUNT = dtH.Rows[0]["QT_PACK"].ToString().Trim();
			string CD_PRODUCT = dtH.Rows[0]["CD_PRODUCT"].ToString().Trim();
			string BAN = dtH.Rows[0]["BAN"].ToString().Trim();
			string NO_IO = dtH.Rows[0]["NO_IO"].ToString().Trim();
			string DT_GIR = dtH.Rows[0]["DT_GIR"].ToString().Trim();
			string CD_POST = dtH.Rows[0]["PC"].ToString().Trim();


			DataRow[] row = dt.Select("CD_FLAG1 LIKE '%" + CD_COUNTRY + "' AND CD_FLAG3 = 'Y'");

			if (row.Length > 0 && string.IsNullOrEmpty(CD_POST))
			{
				return "우편번호 필수 국가 입니다.";
			}

			int addLen = ADD_CONSIGNEE.Length / 3;

			if (ADD_CONSIGNEE.Length > 134)
				return "주소 확인(주소 길이가 깁니다) / " + Environment.NewLine + "총길이 : " + ADD_CONSIGNEE.Length.ToString() + "/135";

			return result;
		}
		#endregion 물류부 체크


		#region 관리부 서류 
		public static string DHLShipmentValidationService_D(DataTable dtH)
		{
			string CD_COMPANY = dtH.Rows[0]["CD_COMPANY"].ToString().Trim();
			if (string.IsNullOrEmpty(CD_COMPANY)) return "회사코드 없음";
			string NM_CONSIGNEE = dtH.Rows[0]["DC_COMPANY"].ToString().Trim();
			if (string.IsNullOrEmpty(NM_CONSIGNEE)) return "수하인 회사 없음";
			string PIC_CONSIGNEE = "Finance Department";
			string TEL_CONSIGNEE = dtH.Rows[0]["DC_TEL"].ToString().Replace(" ", "").Trim();
			string ADD_CONSIGNEE = dtH.Rows[0]["DC_ADDRESS"].ToString().Trim(); if (string.IsNullOrEmpty(ADD_CONSIGNEE)) return "주소 없음";
			string NM_ARRIVER_COUNTRY = dtH.Rows[0]["NM_NATION"].ToString().Trim(); if (string.IsNullOrEmpty(NM_ARRIVER_COUNTRY)) return "국가 정보가 없습니다.";
			string CITY = dtH.Rows[0]["NM_CITY"].ToString().Trim(); if (string.IsNullOrEmpty(CITY)) return "도시 정보 없음";
			string CD_COUNTRY = dtH.Rows[0]["CD_NATION"].ToString().Trim();
			string CD_PRODUCT = dtH.Rows[0]["CD_PRODUCT"].ToString().Trim();
			string NO_IO = dtH.Rows[0]["NO_IO"].ToString().Trim(); if (string.IsNullOrEmpty(NO_IO)) return "미출고(인보이스 번호 없음)";
			string DT_GIR = dtH.Rows[0]["DT_PROCESS"].ToString().Trim();
			string CD_POST = dtH.Rows[0]["CD_POSTAL"].ToString().Trim();


			if (CD_COMPANY.Equals("S100"))
				CD_COMPANY = "K100";


			// consignee 주소 분배
			string ADD_CONSIGNEE1 = string.Empty;
			string ADD_CONSIGNEE2 = string.Empty;
			string ADD_CONSIGNEE3 = string.Empty;

			int addLen = ADD_CONSIGNEE.Length / 3;

			if (ADD_CONSIGNEE.Length > 134)
				return "주소 확인(주소 길이가 깁니다) / " + Environment.NewLine + "총길이 : " + ADD_CONSIGNEE.Length.ToString() + "/135";

			ADD_CONSIGNEE1 = ADD_CONSIGNEE.Substring(0, addLen);
			ADD_CONSIGNEE2 = ADD_CONSIGNEE.Substring(addLen, addLen);
			ADD_CONSIGNEE3 = ADD_CONSIGNEE.Substring(addLen * 2, ADD_CONSIGNEE.Length - (addLen * 2));


			//string dhlID = "v62_8iHIcZeol6"; // TEST
			//string dhlPW = "kgBEzSNDAa"; // TEST

			string dhlID = "v62_gTmZBq5rdF";
			string dhlPW = "n0bqUshCHw";

			string dhlAccountNum = string.Empty;

			if (CD_COMPANY.Equals("K100"))
				dhlAccountNum = "590010331";
			else if (CD_COMPANY.Equals("K200"))
				dhlAccountNum = "590223885";

			//590223885
			//string dhlURL = "https://xmlpitest-ea.dhl.com/XMLShippingServlet";
			string dhlURL = "https://xmlpi-ea.dhl.com/XMLShippingServlet";

			XmlDocument xmlD = new XmlDocument();

			XmlDeclaration decl = xmlD.CreateXmlDeclaration("1.0", "utf-8", null); // null이나 string.empty일 경우에는 표시를 하지 않음
			xmlD.InsertBefore(decl, xmlD.DocumentElement);

			XmlNode xmlRoot = xmlD.CreateElement("req", "ShipmentRequest", "http://www.dhl.com");
			xmlD.AppendChild(xmlRoot);


			// Attribute 추가
			XmlAttribute rootAttschemaVersion = xmlD.CreateAttribute("schemaVersion");
			rootAttschemaVersion.Value = "10.0";
			xmlRoot.Attributes.Append(rootAttschemaVersion);

			XmlAttribute rootAttschemaLocation = xmlD.CreateAttribute("xsi:schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
			rootAttschemaLocation.Value = "http://www.dhl.com ship-val-global-req.xsd";
			xmlRoot.Attributes.Append(rootAttschemaLocation);



			// Request
			XmlNode xmlRootRequest = xmlD.CreateElement("Request");
			{
				xmlRoot.AppendChild(xmlRootRequest);

				// Request ServiceHeader
				XmlNode xmlRootServiceHeader = xmlD.CreateElement("ServiceHeader");
				{
					xmlRootRequest.AppendChild(xmlRootServiceHeader);

					// MessageTime
					XmlNode xmlRootServiceHeaderMessageTime = xmlD.CreateElement("MessageTime");
					{
						xmlRootServiceHeaderMessageTime.InnerText = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss.fff") + "+09:00";
					}

					// MessageReference
					XmlNode xmlRootServiceHeaderMessageReference = xmlD.CreateElement("MessageReference");
					{
						// DO   min 28 max 32   변경함 20201118 NO_GIR ->  NO_IO 10
						xmlRootServiceHeaderMessageReference.InnerText = NO_IO + "000000" + DateTime.Now.ToString("yyyyMMddhhmmss");
					}

					// SiteID
					XmlNode xmlRootServiceHeaderSiteID = xmlD.CreateElement("SiteID");
					{
						xmlRootServiceHeaderSiteID.InnerText = dhlID;
					}

					// Password
					XmlNode xmlRootServiceHeaderPassword = xmlD.CreateElement("Password");
					{
						xmlRootServiceHeaderPassword.InnerText = dhlPW;
					}

					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderMessageTime);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderMessageReference);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderSiteID);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderPassword);
				}

				// Request MetaData
				XmlNode xmlRootMetaData = xmlD.CreateElement("MetaData");
				{
					xmlRootRequest.AppendChild(xmlRootMetaData);

					// SoftwareName
					XmlNode xmlRootSoftwareName = xmlD.CreateElement("SoftwareName");
					{
						xmlRootSoftwareName.InnerText = "3PV";
					}

					// SoftwareVersion
					XmlNode xmlRootSoftwareVersion = xmlD.CreateElement("SoftwareVersion");
					{
						xmlRootSoftwareVersion.InnerText = "10.0";
					}

					xmlRootMetaData.AppendChild(xmlRootSoftwareName);
					xmlRootMetaData.AppendChild(xmlRootSoftwareVersion);
				}
			}


			// RegionCode
			XmlNode xmlRootRegionCode = xmlD.CreateElement("RegionCode");
			{
				xmlRootRegionCode.InnerText = "AP"; // 한국 발송이면 AP고정
			}
			xmlRoot.AppendChild(xmlRootRegionCode);

			// NewShipper
			XmlNode xmlRootNewShipper = xmlD.CreateElement("NewShipper");
			{
				xmlRootNewShipper.InnerText = "N";
			}
			xmlRoot.AppendChild(xmlRootNewShipper);

			// LanguageCode
			XmlNode xmlRootLanguageCode = xmlD.CreateElement("LanguageCode");
			{
				xmlRootLanguageCode.InnerText = "ko";
			}
			xmlRoot.AppendChild(xmlRootLanguageCode);

			// PiecesEnabled
			//XmlNode xmlRootPiecesEnabled = xmlD.CreateElement("PiecesEnabled");
			//{
			//	xmlRootPiecesEnabled.InnerText = "Y";
			//}
			//xmlRoot.AppendChild(xmlRootPiecesEnabled);


			// Billing
			XmlNode xmlRootBilling = xmlD.CreateElement("Billing");
			{
				xmlRoot.AppendChild(xmlRootBilling);

				// ServiceHeader
				XmlNode xmlRootBillingServiceHeader = xmlD.CreateElement("ShipperAccountNumber");
				{
					xmlRootBillingServiceHeader.InnerText = dhlAccountNum;
				}

				// ShippingPaymentType
				XmlNode xmlRootBillingShippingPaymentType = xmlD.CreateElement("ShippingPaymentType");
				{
					// S - 발송자 지불건, R - 수취인 지불건, T - 제 3자 지불건
					xmlRootBillingShippingPaymentType.InnerText = "S";
				}

				// BillingAccountNumber
				XmlNode xmlRootBillingBillingAccountNumber = xmlD.CreateElement("BillingAccountNumber");
				{
					xmlRootBillingBillingAccountNumber.InnerText = dhlAccountNum;
				}


				// DutyPaymentType				// 이건은 강형모 차장한테 물어봐야함
				//XmlNode xmlRootBillingDutyPaymentType = xmlD.CreateElement("DutyPaymentType");
				//{
				// DTP, DTU
				// S - 발송자 지불건, R - 수취인 지불건, T - 제 3자 지불건
				//xmlRootBillingDutyPaymentType.InnerText = "R";
				//}

				xmlRootBilling.AppendChild(xmlRootBillingServiceHeader);
				xmlRootBilling.AppendChild(xmlRootBillingShippingPaymentType);
				xmlRootBilling.AppendChild(xmlRootBillingBillingAccountNumber);
				//xmlRootBilling.AppendChild(xmlRootBillingDutyPaymentType);
			}


			// Consignee 수취인
			XmlNode xmlRootConsignee = xmlD.CreateElement("Consignee");
			{
				xmlRoot.AppendChild(xmlRootConsignee);

				// CompanyName
				XmlNode xmlRootConsigneeCompanyName = xmlD.CreateElement("CompanyName");
				{
					// NM_CONSIGNEE
					if (NM_CONSIGNEE.Length <= 60)
						xmlRootConsigneeCompanyName.InnerText = NM_CONSIGNEE;
					else
						xmlRootConsigneeCompanyName.InnerText = NM_CONSIGNEE.Substring(0, 60);
				}

				// AddressLine   // 라인마다 max 45
				XmlNode xmlRootConsigneeAddressLine1 = xmlD.CreateElement("AddressLine1");
				{
					// ADD1_CONSIGNEE
					xmlRootConsigneeAddressLine1.InnerText = ADD_CONSIGNEE1;
				}

				XmlNode xmlRootConsigneeAddressLine2 = xmlD.CreateElement("AddressLine2");
				{
					// ADD1_CONSIGNEE
					xmlRootConsigneeAddressLine2.InnerText = ADD_CONSIGNEE2;
				}

				XmlNode xmlRootConsigneeAddressLine3 = xmlD.CreateElement("AddressLine3");
				{
					// ADD1_CONSIGNEE
					xmlRootConsigneeAddressLine3.InnerText = ADD_CONSIGNEE3;
				}

				// City
				XmlNode xmlRootConsigneeCity = xmlD.CreateElement("City");
				{
					xmlRootConsigneeCity.InnerText = CITY.Replace(",", "");
				}



				// CountryCode
				XmlNode xmlRootConsigneeCountryCode = xmlD.CreateElement("CountryCode");
				{
					xmlRootConsigneeCountryCode.InnerText = CD_COUNTRY;
				}

				// CountryName
				XmlNode xmlRootConsigneeCountryName = xmlD.CreateElement("CountryName");
				{
					xmlRootConsigneeCountryName.InnerText = NM_ARRIVER_COUNTRY;
				}

				xmlRootConsignee.AppendChild(xmlRootConsigneeCompanyName);
				xmlRootConsignee.AppendChild(xmlRootConsigneeAddressLine1);
				if (!string.IsNullOrEmpty(ADD_CONSIGNEE2))
					xmlRootConsignee.AppendChild(xmlRootConsigneeAddressLine2);
				if (!string.IsNullOrEmpty(ADD_CONSIGNEE3))
					xmlRootConsignee.AppendChild(xmlRootConsigneeAddressLine3);
				xmlRootConsignee.AppendChild(xmlRootConsigneeCity);
				//// PostalCode // 테스트로 싱가콜 우편번호 사용함
				if (!string.IsNullOrEmpty(CD_POST))
				{
					XmlNode xmlRootConsigneePostalCode = xmlD.CreateElement("PostalCode");
					{
						xmlRootConsigneePostalCode.InnerText = CD_POST;
					}

					xmlRootConsignee.AppendChild(xmlRootConsigneePostalCode);
				}
				xmlRootConsignee.AppendChild(xmlRootConsigneeCountryCode);
				xmlRootConsignee.AppendChild(xmlRootConsigneeCountryName);


				// Contact
				XmlNode xmlRootConsigneeContact = xmlD.CreateElement("Contact");
				{
					xmlRootConsignee.AppendChild(xmlRootConsigneeContact);

					// PersonName
					XmlNode xmlRootConsigneeContactPersonName = xmlD.CreateElement("PersonName");
					{
						// PIC
						xmlRootConsigneeContactPersonName.InnerText = PIC_CONSIGNEE;

					}

					// PhoneNumber
					XmlNode xmlRootConsigneeContactPhoneNumber = xmlD.CreateElement("PhoneNumber");
					{
						// TEL
						if (TEL_CONSIGNEE.Length < 25)
							xmlRootConsigneeContactPhoneNumber.InnerText = TEL_CONSIGNEE;
						else
							xmlRootConsigneeContactPhoneNumber.InnerText = TEL_CONSIGNEE.Left(24);

					}
					xmlRootConsigneeContact.AppendChild(xmlRootConsigneeContactPersonName);
					xmlRootConsigneeContact.AppendChild(xmlRootConsigneeContactPhoneNumber);
				}
			}


			// Commodity
			XmlNode xmlRootCommodity = xmlD.CreateElement("Commodity");
			{
				xmlRoot.AppendChild(xmlRootCommodity);

				// CommodityCode
				XmlNode xmlRootCommodityCommodityCode = xmlD.CreateElement("CommodityCode");
				{
					// NM_HS
					xmlRootCommodityCommodityCode.InnerText = "DOC";
				}

				// CommodityName
				XmlNode xmlRootCommodityCommodityName = xmlD.CreateElement("CommodityName");
				{
					// NM_ITEM_PARTNER
					xmlRootCommodityCommodityName.InnerText = "Document";
				}

				xmlRootCommodity.AppendChild(xmlRootCommodityCommodityCode);
				xmlRootCommodity.AppendChild(xmlRootCommodityCommodityName);
			}



			// UseDHLInvoice
			XmlNode xmlRootUseDHLInvoice = xmlD.CreateElement("UseDHLInvoice");
			{
				xmlRootUseDHLInvoice.InnerText = "N";
			}
			xmlRoot.AppendChild(xmlRootUseDHLInvoice);






			// Reference
			XmlNode xmlRootReference = xmlD.CreateElement("Reference");
			{
				xmlRoot.AppendChild(xmlRootReference);


				// ReferenceID			
				XmlNode xmlRootReferenceReferenceID = xmlD.CreateElement("ReferenceID");
				{
					// NO_GIR -> NO_IO 변경함 20201118
					xmlRootReferenceReferenceID.InnerText = NO_IO;
				}

				xmlRootReference.AppendChild(xmlRootReferenceReferenceID);
			}



			// ShipmentDetails
			XmlNode xmlRootShipmentDetails = xmlD.CreateElement("ShipmentDetails");
			{
				xmlRoot.AppendChild(xmlRootShipmentDetails);

				//// NumberOfPieces	max:99
				//XmlNode xmlRootShipmentDetailsNumberOfPieces = xmlD.CreateElement("NumberOfPieces");
				//{
				//	xmlRootShipmentDetailsNumberOfPieces.InnerText = "1";
				//}

				// Pieces
				XmlNode xmlRootShipmentDetailsPieces = xmlD.CreateElement("Pieces");
				{
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsPieces);

					//for (int r = 0; r < dtL.Rows.Count; r++)
					//{
					// Pieces
					XmlNode xmlRootShipmentDetailsPiecesPiece = xmlD.CreateElement("Piece");
					{
						xmlRootShipmentDetailsPieces.AppendChild(xmlRootShipmentDetailsPiecesPiece);

						// Weight
						XmlNode xmlRootShipmentDetailsPiecesPieceWeight = xmlD.CreateElement("Weight");
						{
							xmlRootShipmentDetailsPiecesPieceWeight.InnerText = "0.5";
						}

						// Width
						XmlNode xmlRootShipmentDetailsPiecesPieceWidth = xmlD.CreateElement("Width");
						{
							xmlRootShipmentDetailsPiecesPieceWidth.InnerText = "35";
						}

						// Height
						XmlNode xmlRootShipmentDetailsPiecesPieceHeight = xmlD.CreateElement("Height");
						{
							xmlRootShipmentDetailsPiecesPieceHeight.InnerText = "27";
						}

						// Depth
						XmlNode xmlRootShipmentDetailsPiecesPieceDepth = xmlD.CreateElement("Depth");
						{
							xmlRootShipmentDetailsPiecesPieceDepth.InnerText = "2";
						}

						// PieceContents	35 CHARS 제한
						XmlNode xmlRootShipmentDetailsPiecesPiecePieceContents = xmlD.CreateElement("PieceContents");
						{
							xmlRootShipmentDetailsPiecesPiecePieceContents.InnerText = "DOCUMENT";
						}

						xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPieceWeight);
						xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPieceWidth);
						xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPieceHeight);
						xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPieceDepth);
						xmlRootShipmentDetailsPiecesPiece.AppendChild(xmlRootShipmentDetailsPiecesPiecePieceContents);
						//}
					}

					// Weight
					//XmlNode xmlRootShipmentDetailsWeight = xmlD.CreateElement("Weight");
					//{
					//	xmlRootShipmentDetailsWeight.InnerText = "0.5";
					//}

					// WeightUnit
					XmlNode xmlRootShipmentDetailsWeightUnit = xmlD.CreateElement("WeightUnit");
					{
						xmlRootShipmentDetailsWeightUnit.InnerText = "K";
					}

					// GlobalProductCode
					XmlNode xmlRootShipmentDetailsGlobalProductCode = xmlD.CreateElement("GlobalProductCode");
					{
						xmlRootShipmentDetailsGlobalProductCode.InnerText = CD_PRODUCT;
					}

					//// LocalProductCode
					//XmlNode xmlRootShipmentDetailsLocalProductCode = xmlD.CreateElement("LocalProductCode");
					//{
					//	xmlRootShipmentDetailsLocalProductCode.InnerText = "P";
					//}

					// Date
					XmlNode xmlRootShipmentDetailsDate = xmlD.CreateElement("Date");
					{
						xmlRootShipmentDetailsDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
					}

					// Contents   90 CHARS 제한
					XmlNode xmlRootShipmentDetailsContents = xmlD.CreateElement("Contents");
					{
						xmlRootShipmentDetailsContents.InnerText = "Documents - general business";
					}

					// DimensionUnit	I = INCHES, C = CENTREMETRES
					XmlNode xmlRootShipmentDetailsDimensionUnit = xmlD.CreateElement("DimensionUnit");
					{
						xmlRootShipmentDetailsDimensionUnit.InnerText = "C";
					}

					// IsDutiable
					XmlNode xmlRootShipmentDetailsIsDutiable = xmlD.CreateElement("IsDutiable");
					{
						xmlRootShipmentDetailsIsDutiable.InnerText = "N";
					}

					// CurrencyCode   통화코드
					XmlNode xmlRootShipmentDetailsIsCurrencyCode = xmlD.CreateElement("CurrencyCode");
					{
						// NM_EXCH
						xmlRootShipmentDetailsIsCurrencyCode.InnerText = "USD";
					}


					//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsNumberOfPieces);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsPieces);
					//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsWeight);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsWeightUnit);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsGlobalProductCode);
					//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsLocalProductCode);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsDate);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsContents);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsDimensionUnit);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsIsDutiable);
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsIsCurrencyCode);
				}
			}


			// Shipper
			XmlNode xmlRootShipper = xmlD.CreateElement("Shipper");
			{
				xmlRoot.AppendChild(xmlRootShipper);

				// ShipperID	30 CHARACTERS
				XmlNode xmlRootShipperShipperID = xmlD.CreateElement("ShipperID");
				{
					if (CD_COMPANY.Equals("K100"))
						xmlRootShipperShipperID.InnerText = "590010331";
					else
						xmlRootShipperShipperID.InnerText = "590223885";
				}

				// CompanyName
				XmlNode xmlRootShipperCompanyName = xmlD.CreateElement("CompanyName");
				{
					if (CD_COMPANY.Equals("K100"))
						xmlRootShipperCompanyName.InnerText = "DINTEC CO.,LTD";
					else
						xmlRootShipperCompanyName.InnerText = "DUBHE CO.,LTD";
				}

				// AddressLine1
				XmlNode xmlRootShipperAddressLine1 = xmlD.CreateElement("AddressLine1");
				{
					if (CD_COMPANY.Equals("K100"))
						xmlRootShipperAddressLine1.InnerText = "309, JUNGANG-DAERO";
					else
						xmlRootShipperAddressLine1.InnerText = "3FL, 297, JUNGANG-DAERO";
				}

				// AddressLine2
				XmlNode xmlRootShipperAddressLine2 = xmlD.CreateElement("AddressLine2");
				{
					if (CD_COMPANY.Equals("K100"))
						xmlRootShipperAddressLine2.InnerText = "DONG-GU, BUSAN, KOREA";
					else
						xmlRootShipperAddressLine2.InnerText = "DONG-GU, BUSAN, KOREA";

				}

				// City
				XmlNode xmlRootShipperCity = xmlD.CreateElement("City");
				{
					xmlRootShipperCity.InnerText = "BUSAN";
				}

				// PostalCode
				XmlNode xmlRootShipperPostalCode = xmlD.CreateElement("PostalCode");
				{
					xmlRootShipperPostalCode.InnerText = "48792";
				}

				// CountryCode
				XmlNode xmlRootShipperCountryCode = xmlD.CreateElement("CountryCode");
				{
					xmlRootShipperCountryCode.InnerText = "KR";
				}

				// CountryName
				XmlNode xmlRootShipperCountryName = xmlD.CreateElement("CountryName");
				{
					xmlRootShipperCountryName.InnerText = "KOREA, REPUBLIC OF (SOUTH K.)";
				}

				xmlRootShipper.AppendChild(xmlRootShipperShipperID);
				xmlRootShipper.AppendChild(xmlRootShipperCompanyName);
				xmlRootShipper.AppendChild(xmlRootShipperAddressLine1);
				xmlRootShipper.AppendChild(xmlRootShipperAddressLine2);
				xmlRootShipper.AppendChild(xmlRootShipperCity);
				xmlRootShipper.AppendChild(xmlRootShipperPostalCode);
				xmlRootShipper.AppendChild(xmlRootShipperCountryCode);
				xmlRootShipper.AppendChild(xmlRootShipperCountryName);


				// Contact
				XmlNode xmlRootShipperContact = xmlD.CreateElement("Contact");
				{
					xmlRootShipper.AppendChild(xmlRootShipperContact);

					// PersonName
					XmlNode xmlRootShipperContactPersonName = xmlD.CreateElement("PersonName");
					{
						if (CD_COMPANY.Equals("K100"))
							xmlRootShipperContactPersonName.InnerText = "Dintec";
						else
							xmlRootShipperContactPersonName.InnerText = "Dubheco";
					}

					// PhoneNumber
					XmlNode xmlRootShipperContactPhoneNumber = xmlD.CreateElement("PhoneNumber");
					{
						if (CD_COMPANY.Equals("K100"))
							xmlRootShipperContactPhoneNumber.InnerText = "+82516641000";
						else
							xmlRootShipperContactPhoneNumber.InnerText = "+82514652100";
					}

					// Telex
					XmlNode xmlRootShipperContactTelex = xmlD.CreateElement("Telex");
					{
						if (CD_COMPANY.Equals("K100"))
							xmlRootShipperContactTelex.InnerText = "+820516641174";
						else
							xmlRootShipperContactTelex.InnerText = "+820514652105";
					}

					// Email
					XmlNode xmlRootShipperContactEmail = xmlD.CreateElement("Email");
					{
						if (CD_COMPANY.Equals("K100"))
							xmlRootShipperContactEmail.InnerText = "hmjo@dintec.co.kr";
						else
							xmlRootShipperContactEmail.InnerText = "service@dubheco.com";
					}

					xmlRootShipperContact.AppendChild(xmlRootShipperContactPersonName);
					xmlRootShipperContact.AppendChild(xmlRootShipperContactPhoneNumber);
					xmlRootShipperContact.AppendChild(xmlRootShipperContactTelex);
					xmlRootShipperContact.AppendChild(xmlRootShipperContactEmail);
				}
			}


			// LabelImageFormat		송장 출력 방식	PDF, EPL2, ZPL2
			XmlNode xmlRootUseLabelImageFormat = xmlD.CreateElement("LabelImageFormat");
			{
				xmlRootUseLabelImageFormat.InnerText = "PDF";
			}
			xmlRoot.AppendChild(xmlRootUseLabelImageFormat);


			// xml 값 저장 - TEST 용도
			//xmlD.Save(@"C:\testrequest_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml");


			return postXMLDHL_D(dhlURL, GetXMLAsString(xmlD), NO_IO);
		}


		public static string postXMLDHL_D(string dhlURL, string dhlXml, string NO_IO)
		{
			string ZPL2GetStr = string.Empty;
			string ZPL2SetStr = string.Empty;
			string returnStr = string.Empty;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dhlURL);
			byte[] bytes;
			bytes = System.Text.Encoding.ASCII.GetBytes(dhlXml);
			request.ContentType = "text/xml; encoding='utf-8'";
			request.ContentLength = bytes.Length;
			request.Method = "POST";

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Close();
			HttpWebResponse response;
			response = (HttpWebResponse)request.GetResponse();
			if (response.StatusCode == HttpStatusCode.OK)
			{
				Stream responseStream = response.GetResponseStream();
				string responseStr = new StreamReader(responseStream).ReadToEnd();

				XmlDocument xml = new XmlDocument();
				xml.LoadXml(responseStr);
				XmlNodeList xnList = xml.GetElementsByTagName("OutputImage");

				// 가져 온 값 pdf 코드로 변환 하기 위해서 변수에 넣기
				foreach (XmlNode xn in xnList)
				{
					ZPL2GetStr = xn.InnerText;
				}

				if (!string.IsNullOrEmpty(ZPL2GetStr))
				{

					string a = ZPL2GetStr;
					string fileNamePDF = DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + NO_IO + "_dhl";
					byte[] bytePDF = Convert.FromBase64String(a);

					System.IO.FileStream stream =
			new FileStream(@"C:\" + fileNamePDF + ".pdf", FileMode.CreateNew);
					System.IO.BinaryWriter writer =
						new BinaryWriter(stream);
					writer.Write(bytePDF, 0, bytePDF.Length);
					writer.Close();

					//Process.Start(@"C:\" + fileNamePDF + ".pdf");

					printPDFWithAcrobat(@"C:\" + fileNamePDF + ".pdf");

					responseStr = "SUCCESS";
				}

				returnStr = responseStr;
			}

			return returnStr;
		}

		#endregion 관리부 서류


		#region 픽업
		public static string dhlPickUp(DataTable dt)
		{
			string returnStr = string.Empty;
			//string CD_COMPANY = "K100";
			//string NO_IO = "TEST1234";
			string CD_COMPANY = dt.Rows[0]["CD_COMPANY"].ToString();
			string NO_IO = dt.Rows[0]["NO_IO"].ToString();

			//string dhlID = "v62_8iHIcZeol6"; // TEST
			//string dhlPW = "kgBEzSNDAa"; // TEST
			string dhlID = "v62_gTmZBq5rdF";
			string dhlPW = "n0bqUshCHw";

			string dhlAccountNum = string.Empty;

			if (CD_COMPANY.Equals("K100"))
				dhlAccountNum = "590010331";
			else if (CD_COMPANY.Equals("K200"))
				dhlAccountNum = "590223885";
			//590223885
			//string dhlURL = "https://xmlpitest-ea.dhl.com/XMLShippingServlet";
			string dhlURL = "https://xmlpi-ea.dhl.com/XMLShippingServlet";
			XmlDocument xmlD = new XmlDocument();

			XmlDeclaration decl = xmlD.CreateXmlDeclaration("1.0", "utf-8", null); // null이나 string.empty일 경우에는 표시를 하지 않음
			xmlD.InsertBefore(decl, xmlD.DocumentElement);

			XmlNode xmlRoot = xmlD.CreateElement("req", "BookPURequest", "http://www.dhl.com");
			xmlD.AppendChild(xmlRoot);


			// Attribute 추가
			XmlAttribute rootAttschemaVersion = xmlD.CreateAttribute("schemaVersion");
			rootAttschemaVersion.Value = "1.0";
			xmlRoot.Attributes.Append(rootAttschemaVersion);

			XmlAttribute rootAttschemaLocation = xmlD.CreateAttribute("xsi:schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
			rootAttschemaLocation.Value = "http://www.dhl.com book-pickup-global-req.xsd";
			xmlRoot.Attributes.Append(rootAttschemaLocation);


			// Request
			XmlNode xmlRootRequest = xmlD.CreateElement("Request");
			{
				xmlRoot.AppendChild(xmlRootRequest);

				// Request ServiceHeader
				XmlNode xmlRootServiceHeader = xmlD.CreateElement("ServiceHeader");
				{
					xmlRootRequest.AppendChild(xmlRootServiceHeader);

					// MessageTime
					XmlNode xmlRootServiceHeaderMessageTime = xmlD.CreateElement("MessageTime");
					{
						xmlRootServiceHeaderMessageTime.InnerText = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss.fff") + "+09:00";
					}

					// MessageReference
					XmlNode xmlRootServiceHeaderMessageReference = xmlD.CreateElement("MessageReference");
					{
						// DO   min 28 max 32   변경함 20201118 NO_GIR ->  NO_IO 10
						xmlRootServiceHeaderMessageReference.InnerText = NO_IO + "000000" + DateTime.Now.ToString("yyyyMMddhhmmss");
					}

					// SiteID
					XmlNode xmlRootServiceHeaderSiteID = xmlD.CreateElement("SiteID");
					{
						xmlRootServiceHeaderSiteID.InnerText = dhlID;
					}

					// Password
					XmlNode xmlRootServiceHeaderPassword = xmlD.CreateElement("Password");
					{
						xmlRootServiceHeaderPassword.InnerText = dhlPW;
					}

					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderMessageTime);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderMessageReference);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderSiteID);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderPassword);
				}
			}

			// RegionCode
			XmlNode xmlRootRegionCode = xmlD.CreateElement("RegionCode");
			{
				xmlRootRegionCode.InnerText = "AP"; // 한국 발송이면 AP고정
			}
			xmlRoot.AppendChild(xmlRootRegionCode);


			// Requestor
			XmlNode xmlRootRequestor = xmlD.CreateElement("Requestor");
			{
				xmlRoot.AppendChild(xmlRootRequestor);

				// AccountType
				XmlNode xmlRootRequestorAccountType = xmlD.CreateElement("AccountType");
				{
					xmlRootRequestorAccountType.InnerText = "D";
				}
				// AccountNumber
				XmlNode xmlRootRequestorAccountNumber = xmlD.CreateElement("AccountNumber");
				{
					xmlRootRequestorAccountNumber.InnerText = dhlAccountNum;
				}
				xmlRootRequestor.AppendChild(xmlRootRequestorAccountType);
				xmlRootRequestor.AppendChild(xmlRootRequestorAccountNumber);


				// Requestor
				XmlNode xmlRootRequestorRequestorContact = xmlD.CreateElement("RequestorContact");
				{
					xmlRootRequestor.AppendChild(xmlRootRequestorRequestorContact);

					XmlNode xmlRootRequestorContactPersonName = xmlD.CreateElement("PersonName");
					{
						xmlRootRequestorContactPersonName.InnerText = "H.M JO";
					}
					XmlNode xmlRootRequestorContactPhone = xmlD.CreateElement("Phone");
					{
						xmlRootRequestorContactPhone.InnerText = "+820516641174";
					}
					XmlNode xmlRootRequestorContactPhoneExtension = xmlD.CreateElement("PhoneExtension");
					{
						xmlRootRequestorContactPhoneExtension.InnerText = "1174";
					}

					xmlRootRequestorRequestorContact.AppendChild(xmlRootRequestorContactPersonName);
					xmlRootRequestorRequestorContact.AppendChild(xmlRootRequestorContactPhone);
					xmlRootRequestorRequestorContact.AppendChild(xmlRootRequestorContactPhoneExtension);
				}
			}


			// Place
			XmlNode xmlRootPlace = xmlD.CreateElement("Place");
			{
				xmlRoot.AppendChild(xmlRootPlace);

				// LocationType
				XmlNode xmlRootPlaceLocationType = xmlD.CreateElement("LocationType");
				{
					xmlRootPlaceLocationType.InnerText = "B";
				}
				// CompanyName
				XmlNode xmlRootPlaceCompanyName = xmlD.CreateElement("CompanyName");
				{
					xmlRootPlaceCompanyName.InnerText = "DINTEC";
				}
				// Address1
				XmlNode xmlRootPlaceAddress1 = xmlD.CreateElement("Address1");
				{
					xmlRootPlaceAddress1.InnerText = "DINTEC Bldg.,Jungang-Daero 309,";
				}
				// Address2
				XmlNode xmlRootPlaceAddress2 = xmlD.CreateElement("Address2");
				{
					xmlRootPlaceAddress2.InnerText = "Dong-gu, Busan, 601-836, KOREA";
				}
				// PackageLocation
				XmlNode xmlRootPlacePackageLocation = xmlD.CreateElement("PackageLocation");
				{
					xmlRootPlacePackageLocation.InnerText = "Infosys";
				}
				// City
				XmlNode xmlRootPlaceCity = xmlD.CreateElement("City");
				{
					xmlRootPlaceCity.InnerText = "BUSAN";
				}
				//// StateCode
				//XmlNode xmlRootPlaceStateCode = xmlD.CreateElement("StateCode");
				//{
				//	xmlRootPlaceStateCode.InnerText = "MH";
				//}
				// DivisionName
				XmlNode xmlRootPlaceDivisionName = xmlD.CreateElement("DivisionName");
				{
					xmlRootPlaceDivisionName.InnerText = "BUSAN";
				}
				// CountryCode
				XmlNode xmlRootPlaceCountryCode = xmlD.CreateElement("CountryCode");
				{
					xmlRootPlaceCountryCode.InnerText = "KR";
				}
				// PostalCode
				XmlNode xmlRootPlacePostalCode = xmlD.CreateElement("PostalCode");
				{
					xmlRootPlacePostalCode.InnerText = "48792";
				}

				xmlRootPlace.AppendChild(xmlRootPlaceLocationType);
				xmlRootPlace.AppendChild(xmlRootPlaceCompanyName);
				xmlRootPlace.AppendChild(xmlRootPlaceAddress1);
				xmlRootPlace.AppendChild(xmlRootPlaceAddress2);
				xmlRootPlace.AppendChild(xmlRootPlacePackageLocation);
				xmlRootPlace.AppendChild(xmlRootPlaceCity);
				//xmlRootPlace.AppendChild(xmlRootPlaceStateCode);
				xmlRootPlace.AppendChild(xmlRootPlaceDivisionName);
				xmlRootPlace.AppendChild(xmlRootPlaceCountryCode);
				xmlRootPlace.AppendChild(xmlRootPlacePostalCode);
			}


			// Pickup
			XmlNode xmlRootPickup = xmlD.CreateElement("Pickup");
			{
				xmlRoot.AppendChild(xmlRootPickup);

				// PickupDate
				XmlNode xmlRootPickupPickupDate = xmlD.CreateElement("PickupDate");
				{
					xmlRootPickupPickupDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
				}

				xmlRootPickup.AppendChild(xmlRootPickupPickupDate);

				DateTime dtNowTime;
				DateTime dtFixTime;

				DateTime.TryParse("11:30:00", out dtFixTime);
				DateTime.TryParse(DateTime.Now.ToString("hh:MM:ss"), out dtNowTime);

				if (dtFixTime > dtNowTime)
				{
					// ReadyByTime
					XmlNode xmlRootPickupReadyByTimee = xmlD.CreateElement("ReadyByTime");
					{
						xmlRootPickupReadyByTimee.InnerText = "10:30";
					}
					// PickupDate
					XmlNode xmlRootPickupCloseTime = xmlD.CreateElement("CloseTime");
					{
						xmlRootPickupCloseTime.InnerText = "12:30";
					}
					xmlRootPickup.AppendChild(xmlRootPickupReadyByTimee);
					xmlRootPickup.AppendChild(xmlRootPickupCloseTime);
				}
				else
				{
					// ReadyByTime
					XmlNode xmlRootPickupReadyByTimee = xmlD.CreateElement("ReadyByTime");
					{
						xmlRootPickupReadyByTimee.InnerText = "16:00";
					}
					// PickupDate
					XmlNode xmlRootPickupCloseTime = xmlD.CreateElement("CloseTime");
					{
						xmlRootPickupCloseTime.InnerText = "17:00";
					}
					xmlRootPickup.AppendChild(xmlRootPickupReadyByTimee);
					xmlRootPickup.AppendChild(xmlRootPickupCloseTime);
				}


				// Pieces
				XmlNode xmlRootPickupPieces = xmlD.CreateElement("Pieces");
				{
					xmlRootPickupPieces.InnerText = "1";
				}
				xmlRootPickup.AppendChild(xmlRootPickupPieces);


				// weight
				XmlNode xmlRootPickupweight = xmlD.CreateElement("weight");
				{
					xmlRootPickup.AppendChild(xmlRootPickupweight);

					XmlNode xmlRootPickupweightWeight = xmlD.CreateElement("Weight");
					{
						xmlRootPickupweightWeight.InnerText = "0.5";
					}
					XmlNode xmlRootPickupWeightWeightUnit = xmlD.CreateElement("WeightUnit");
					{
						xmlRootPickupWeightWeightUnit.InnerText = "K";
					}
					xmlRootPickupweight.AppendChild(xmlRootPickupweightWeight);
					xmlRootPickupweight.AppendChild(xmlRootPickupWeightWeightUnit);
				}


			}



			// PickupContact
			XmlNode xmlRootPickupContact = xmlD.CreateElement("PickupContact");
			{
				xmlRoot.AppendChild(xmlRootPickupContact);

				// PersonName
				XmlNode xmlRootPickupContactPersonName = xmlD.CreateElement("PersonName");
				{
					xmlRootPickupContactPersonName.InnerText = "H.M JO";
				}
				// PersonName
				XmlNode xmlRootPickupContactPhone = xmlD.CreateElement("Phone");
				{
					xmlRootPickupContactPhone.InnerText = "+820516641174";
				}
				// PhoneExtension
				XmlNode xmlRootPickupContactPhoneExtension = xmlD.CreateElement("PhoneExtension");
				{
					xmlRootPickupContactPhoneExtension.InnerText = "1174";
				}

				xmlRootPickupContact.AppendChild(xmlRootPickupContactPersonName);
				xmlRootPickupContact.AppendChild(xmlRootPickupContactPhone);
				xmlRootPickupContact.AppendChild(xmlRootPickupContactPhoneExtension);
			}



			// ShipmentDetails
			XmlNode xmlRootShipmentDetails = xmlD.CreateElement("ShipmentDetails");
			{
				xmlRoot.AppendChild(xmlRootShipmentDetails);

				// AccountType
				XmlNode xmlRootShipmentDetailsAccountType = xmlD.CreateElement("AccountType");
				{
					xmlRootShipmentDetailsAccountType.InnerText = "D";
				}
				// AccountNumber
				XmlNode xmlRootShipmentDetailsAccountNumber = xmlD.CreateElement("AccountNumber");
				{
					xmlRootShipmentDetailsAccountNumber.InnerText = dhlAccountNum;
				}
				// BillToAccountNumber
				XmlNode xmlRootShipmentDetailsBillToAccountNumber = xmlD.CreateElement("BillToAccountNumber");
				{
					xmlRootShipmentDetailsBillToAccountNumber.InnerText = dhlAccountNum;
				}
				// AWBNumber
				XmlNode xmlRootShipmentDetailsAWBNumber = xmlD.CreateElement("AWBNumber");
				{
					xmlRootShipmentDetailsAWBNumber.InnerText = dhlAccountNum;
				}
				// NumberOfPieces
				XmlNode xmlRootShipmentDetailsNumberOfPieces = xmlD.CreateElement("NumberOfPieces");
				{
					xmlRootShipmentDetailsNumberOfPieces.InnerText = "1";
				}
				// Weight
				XmlNode xmlRootShipmentDetailsWeight = xmlD.CreateElement("Weight");
				{
					xmlRootShipmentDetailsWeight.InnerText = "0.5";
				}
				// WeightUnit
				XmlNode xmlRootShipmentDetailsWeightUnit = xmlD.CreateElement("WeightUnit");
				{
					xmlRootShipmentDetailsWeightUnit.InnerText = "K";
				}
				// GlobalProductCode
				XmlNode xmlRootShipmentDetailsGlobalProductCode = xmlD.CreateElement("GlobalProductCode");
				{
					xmlRootShipmentDetailsGlobalProductCode.InnerText = "D";
				}
				// DoorTo
				XmlNode xmlRootShipmentDetailsDoorTo = xmlD.CreateElement("DoorTo");
				{
					xmlRootShipmentDetailsDoorTo.InnerText = "DD";
				}
				//// DimensionUnit
				//XmlNode xmlRootShipmentDetailsDimensionUnit = xmlD.CreateElement("DimensionUnit");
				//{
				//	xmlRootShipmentDetailsDimensionUnit.InnerText = "C";
				//}
				//// InsuredAmount
				//XmlNode xmlRootShipmentDetailsInsuredAmount = xmlD.CreateElement("InsuredAmount");
				//{
				//	xmlRootShipmentDetailsInsuredAmount.InnerText = "D";
				//}
				// InsuredCurrencyCode
				//XmlNode xmlRootShipmentDetailsInsuredCurrencyCode = xmlD.CreateElement("InsuredCurrencyCode");
				//{
				//	xmlRootShipmentDetailsInsuredCurrencyCode.InnerText = "D";
				//}


				//// InsuredCurrencyCode
				//XmlNode xmlRootShipmentDetailsSpecialService = xmlD.CreateElement("SpecialService");
				//{
				//	xmlRootShipmentDetailsSpecialService.InnerText = "D";
				//}
				xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsAccountType);
				xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsAccountNumber);
				//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsBillToAccountNumber);
				//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsAWBNumber);
				//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsNumberOfPieces);
				xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsWeight);
				xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsWeightUnit);
				xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsGlobalProductCode);
				xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsDoorTo);
				//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsDimensionUnit);
				//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsInsuredAmount);
				//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsInsuredCurrencyCode);


				// Pieces
				XmlNode xmlRootShipmentDetailsPieces = xmlD.CreateElement("Pieces");
				{
					xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsPieces);

					// Weight
					XmlNode xmlRootShipmentDetailsPiecesWeight = xmlD.CreateElement("Weight");
					{
						xmlRootShipmentDetailsPiecesWeight.InnerText = "0.5";
					}
					// Width
					XmlNode xmlRootShipmentDetailsPiecesWidth = xmlD.CreateElement("Width");
					{
						xmlRootShipmentDetailsPiecesWidth.InnerText = "35";
					}
					// Height
					XmlNode xmlRootShipmentDetailsPiecesHeight = xmlD.CreateElement("Height");
					{
						xmlRootShipmentDetailsPiecesHeight.InnerText = "27";
					}
					// Depth
					XmlNode xmlRootShipmentDetailsPiecesDepth = xmlD.CreateElement("Depth");
					{
						xmlRootShipmentDetailsPiecesDepth.InnerText = "2";
					}

					xmlRootShipmentDetailsPieces.AppendChild(xmlRootShipmentDetailsPiecesWeight);
					xmlRootShipmentDetailsPieces.AppendChild(xmlRootShipmentDetailsPiecesWidth);
					xmlRootShipmentDetailsPieces.AppendChild(xmlRootShipmentDetailsPiecesHeight);
					xmlRootShipmentDetailsPieces.AppendChild(xmlRootShipmentDetailsPiecesDepth);
				}

				//xmlRootShipmentDetails.AppendChild(xmlRootShipmentDetailsSpecialService);
			}





			return postXMLDHL_PICKUP(dhlURL, GetXMLAsString(xmlD));
		}
		#endregion 픽업


		#region 운임 계산
		public static string DHL_Capability(string weight, string height, string depth, string width, string country, string city, string post)
		{
			string returnStr = string.Empty;
			string CD_COMPANY = "K100";
			string NO_IO = "CAPABILITY";

			string dhlID = "v62_gTmZBq5rdF";
			string dhlPW = "n0bqUshCHw";

			string dhlAccountNum = string.Empty;

			if (CD_COMPANY.Equals("K100"))
				dhlAccountNum = "590010331";
			else if (CD_COMPANY.Equals("K200"))
				dhlAccountNum = "590223885";

			string dhlURL = "https://xmlpi-ea.dhl.com/XMLShippingServlet";
			
			
			XmlDocument xmlD = new XmlDocument();

			XmlDeclaration decl = xmlD.CreateXmlDeclaration("1.0", "utf-8", null); // null이나 string.empty일 경우에는 표시를 하지 않음
			xmlD.InsertBefore(decl, xmlD.DocumentElement);

			XmlElement xmlRoot = xmlD.CreateElement("p", "DCTRequest", "http://www.dhl.com");
			xmlD.AppendChild(xmlRoot);

			// 네임스페이스 추가
			XmlAttribute xmlnsP = xmlD.CreateAttribute("xmlns", "p", "http://www.w3.org/2000/xmlns/");
			xmlnsP.Value = "http://www.dhl.com";
			xmlRoot.Attributes.Append(xmlnsP);

			XmlAttribute xmlnsP1 = xmlD.CreateAttribute("xmlns", "p1", "http://www.w3.org/2000/xmlns/");
			xmlnsP1.Value = "http://www.dhl.com/datatypes";
			xmlRoot.Attributes.Append(xmlnsP1);

			XmlAttribute xmlnsP2 = xmlD.CreateAttribute("xmlns", "p2", "http://www.w3.org/2000/xmlns/");
			xmlnsP2.Value = "http://www.dhl.com/DCTRequestdatatypes";
			xmlRoot.Attributes.Append(xmlnsP2);

			XmlAttribute schemaVersion = xmlD.CreateAttribute("schemaVersion");
			schemaVersion.Value = "2.0";
			xmlRoot.Attributes.Append(schemaVersion);

			XmlAttribute xsi = xmlD.CreateAttribute("xmlns", "xsi", "http://www.w3.org/2000/xmlns/");
			xsi.Value = "http://www.w3.org/2001/XMLSchema-instance";
			xmlRoot.Attributes.Append(xsi);

			XmlAttribute schemaLocation = xmlD.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
			schemaLocation.Value = "http://www.dhl.com DCT-req.xsd";
			xmlRoot.Attributes.Append(schemaLocation);




			XmlNode xmlGetQuote = xmlD.CreateElement("GetQuote");
			{
				xmlRoot.AppendChild(xmlGetQuote);
			}

			#region Request
			XmlNode xmlRootRequest = xmlD.CreateElement("Request");
			{
				xmlGetQuote.AppendChild(xmlRootRequest);

				// Request ServiceHeader
				XmlNode xmlRootServiceHeader = xmlD.CreateElement("ServiceHeader");
				{
					xmlRootRequest.AppendChild(xmlRootServiceHeader);

					// MessageTime
					XmlNode xmlRootServiceHeaderMessageTime = xmlD.CreateElement("MessageTime");
					{
						xmlRootServiceHeaderMessageTime.InnerText = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("hh:mm:ss.fff") + "+09:00";
					}

					// MessageReference
					XmlNode xmlRootServiceHeaderMessageReference = xmlD.CreateElement("MessageReference");
					{
						// DO   min 28 max 32   변경함 20201118 NO_GIR ->  NO_IO 10
						xmlRootServiceHeaderMessageReference.InnerText = NO_IO + "000000" + DateTime.Now.ToString("yyyyMMddhhmmss");
					}

					// SiteID
					XmlNode xmlRootServiceHeaderSiteID = xmlD.CreateElement("SiteID");
					{
						xmlRootServiceHeaderSiteID.InnerText = dhlID;
					}

					// Password
					XmlNode xmlRootServiceHeaderPassword = xmlD.CreateElement("Password");
					{
						xmlRootServiceHeaderPassword.InnerText = dhlPW;
					}

					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderMessageTime);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderMessageReference);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderSiteID);
					xmlRootServiceHeader.AppendChild(xmlRootServiceHeaderPassword);
				}

				// Request MetaData
				XmlNode xmlRootMetaData = xmlD.CreateElement("MetaData");
				{
					xmlRootRequest.AppendChild(xmlRootMetaData);

					// SoftwareName	
					XmlNode xmlRootxmlRootMetaDataSoftwareName = xmlD.CreateElement("SoftwareName");
					{
						xmlRootxmlRootMetaDataSoftwareName.InnerText = "3PV";
					}

					XmlNode xmlRootxmlRootMetaDataSoftwareVersion = xmlD.CreateElement("SoftwareVersion");
					{
						xmlRootxmlRootMetaDataSoftwareName.InnerText = "1.0";
					}

					xmlRootMetaData.AppendChild(xmlRootxmlRootMetaDataSoftwareName);
					xmlRootMetaData.AppendChild(xmlRootxmlRootMetaDataSoftwareVersion);
				}
			}

			#endregion Request

			#region From

			XmlNode xmlRootFrom = xmlD.CreateElement("From");
			{
				xmlGetQuote.AppendChild(xmlRootFrom);


				// CountryCode
				XmlNode xmlRootFromCountryCode = xmlD.CreateElement("CountryCode");
				{
					xmlRootFromCountryCode.InnerText = "KR";
				}

				// Postalcode
				XmlNode xmlRootFromPostalcode = xmlD.CreateElement("Postalcode");
				{
					xmlRootFromPostalcode.InnerText = "48792";
				}

				xmlRootFrom.AppendChild(xmlRootFromCountryCode);
				xmlRootFrom.AppendChild(xmlRootFromPostalcode);
			}
			#endregion From

			#region BkgDetails
			XmlNode xmlRootBkgDetails = xmlD.CreateElement("BkgDetails");
			{
				xmlGetQuote.AppendChild(xmlRootBkgDetails);

				XmlNode xmlRootBkgDetailsPaymentCountryCode = xmlD.CreateElement("PaymentCountryCode");
				{
					xmlRootBkgDetailsPaymentCountryCode.InnerText = "KR";
				}

				XmlNode xmlRootBkgDetailsDate = xmlD.CreateElement("Date");
				{
					xmlRootBkgDetailsDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
				}

				XmlNode xmlRootBkgDetailsReadyTime = xmlD.CreateElement("ReadyTime");
				{
					xmlRootBkgDetailsReadyTime.InnerText = "PT10M";
				}

				XmlNode xmlRootBkgDetailsDimensionUnit = xmlD.CreateElement("DimensionUnit");
				{
					xmlRootBkgDetailsDimensionUnit.InnerText = "CM";
				}

				XmlNode xmlRootBkgDetailsWeightUnit = xmlD.CreateElement("WeightUnit");
				{
					xmlRootBkgDetailsWeightUnit.InnerText = "KG";
				}

				XmlNode xmlRootBkgDetailsNumberOfPieces = xmlD.CreateElement("NumberOfPieces");
				{
					xmlRootBkgDetailsNumberOfPieces.InnerText = "1";
				}

				XmlNode xmlRootBkgDetailsShipmentWeight = xmlD.CreateElement("ShipmentWeight");
				{
					xmlRootBkgDetailsShipmentWeight.InnerText = weight;
				}

				XmlNode xmlRootBkgDetailsPaymentAccountNumber = xmlD.CreateElement("PaymentAccountNumber");
				{
					xmlRootBkgDetailsPaymentAccountNumber.InnerText = dhlAccountNum;
				}

				XmlNode xmlRootBkgDetailsIsDutiable = xmlD.CreateElement("IsDutiable");
				{
					xmlRootBkgDetailsIsDutiable.InnerText = "Y";
				}


				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsPaymentCountryCode);
				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsDate);
				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsReadyTime);
				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsDimensionUnit);
				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsWeightUnit);
				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsNumberOfPieces);
				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsShipmentWeight);


				// Pieces
				XmlNode xmlRootRequestorPieces = xmlD.CreateElement("Pieces");
				{
					xmlRootBkgDetails.AppendChild(xmlRootRequestorPieces);

					// Pieces
					XmlNode xmlRootRequestorPiecesPiece = xmlD.CreateElement("Piece");
					{
						xmlRootRequestorPieces.AppendChild(xmlRootRequestorPiecesPiece);

						XmlNode xmlRootRequestorPiecesPiecesPieceID = xmlD.CreateElement("PieceID");
						{
							xmlRootRequestorPiecesPiecesPieceID.InnerText = "1";
						}

						XmlNode xmlRootRequestorPiecesPiecesHeight = xmlD.CreateElement("Height");
						{
							xmlRootRequestorPiecesPiecesHeight.InnerText = height;
						}

						XmlNode xmlRootRequestorPiecesPiecesDepth = xmlD.CreateElement("Depth");
						{
							xmlRootRequestorPiecesPiecesDepth.InnerText = depth;
						}

						XmlNode xmlRootRequestorPiecesPiecesWidth = xmlD.CreateElement("Width");
						{
							xmlRootRequestorPiecesPiecesWidth.InnerText = width;
						}

						XmlNode xmlRootRequestorPiecesPiecesWeight = xmlD.CreateElement("Weight");
						{
							xmlRootRequestorPiecesPiecesWeight.InnerText = weight;
						}

						xmlRootRequestorPiecesPiece.AppendChild(xmlRootRequestorPiecesPiecesPieceID);
						xmlRootRequestorPiecesPiece.AppendChild(xmlRootRequestorPiecesPiecesHeight);
						xmlRootRequestorPiecesPiece.AppendChild(xmlRootRequestorPiecesPiecesDepth);
						xmlRootRequestorPiecesPiece.AppendChild(xmlRootRequestorPiecesPiecesWidth);
						xmlRootRequestorPiecesPiece.AppendChild(xmlRootRequestorPiecesPiecesWeight);
					}
				}

				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsPaymentAccountNumber);
				xmlRootBkgDetails.AppendChild(xmlRootBkgDetailsIsDutiable);
			}

			#endregion BkgDetails

			#region To
			XmlNode xmlRootTo = xmlD.CreateElement("To");
			{
				xmlGetQuote.AppendChild(xmlRootTo);


				// CountryCode
				XmlNode xmlRootToCountryCode = xmlD.CreateElement("CountryCode");
				{
					xmlRootToCountryCode.InnerText = country;
				}

				// Postalcode
				XmlNode xmlRootToPostalcode = xmlD.CreateElement("Postalcode");
				{
					xmlRootToPostalcode.InnerText = post;
				}
				
				// City
				XmlNode xmlRootToCity = xmlD.CreateElement("City");
				{
					xmlRootToCity.InnerText = city;
				}

				xmlRootTo.AppendChild(xmlRootToCountryCode);
				xmlRootTo.AppendChild(xmlRootToPostalcode);
				xmlRootTo.AppendChild(xmlRootToCity);
			}
			#endregion To

			#region Dutiable

			XmlNode xmlRootDutiable = xmlD.CreateElement("Dutiable");
			{
				xmlGetQuote.AppendChild(xmlRootDutiable);


				// DeclaredCurrency
				XmlNode xmlRootDutiableDeclaredCurrency = xmlD.CreateElement("DeclaredCurrency");
				{
					xmlRootDutiableDeclaredCurrency.InnerText = "USD";
				}

				// DeclaredValue
				XmlNode xmlRootDutiableDeclaredValue = xmlD.CreateElement("DeclaredValue");
				{
					xmlRootDutiableDeclaredValue.InnerText = "10.40";
				}

				xmlRootDutiable.AppendChild(xmlRootDutiableDeclaredCurrency);
				xmlRootDutiable.AppendChild(xmlRootDutiableDeclaredValue);
			}
			#endregion Dutiable

			return postXMLDHL_CAPA(dhlURL, GetXMLAsString(xmlD));
		}

		#endregion 운임 계산




		public static void printPDFWithAcrobat(string path)
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

				//if (false == printProcess.CloseMainWindow())
				//{
				//	printProcess.Kill();
				//}
			}
		}


		public static string postXMLDHL_PICKUP(string dhlURL, string dhlXml)
		{
			string returnStr = string.Empty;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dhlURL);
			byte[] bytes;
			bytes = System.Text.Encoding.ASCII.GetBytes(dhlXml);
			request.ContentType = "text/xml; encoding='utf-8'";
			request.ContentLength = bytes.Length;
			request.Method = "POST";

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Close();
			HttpWebResponse response;
			response = (HttpWebResponse)request.GetResponse();
			if (response.StatusCode == HttpStatusCode.OK)
			{
				Stream responseStream = response.GetResponseStream();
				string responseStr = new StreamReader(responseStream).ReadToEnd();

				XmlDocument xml = new XmlDocument();
				xml.LoadXml(responseStr);

			



				returnStr = responseStr;
			}

			return returnStr;
		}


		public static string postXMLDHL_CAPA(string dhlURL, string dhlXml)
		{
			string returnStr = string.Empty;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dhlURL);
			byte[] bytes;
			bytes = System.Text.Encoding.ASCII.GetBytes(dhlXml);
			request.ContentType = "text/xml; encoding='utf-8'";
			request.ContentLength = bytes.Length;
			request.Method = "POST";

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Close();
			HttpWebResponse response;
			response = (HttpWebResponse)request.GetResponse();
			if (response.StatusCode == HttpStatusCode.OK)
			{
				Stream responseStream = response.GetResponseStream();
				string responseStr = new StreamReader(responseStream).ReadToEnd();

				XmlDocument xml = new XmlDocument();
				xml.LoadXml(responseStr);

				XmlNodeList qtdShps = xml.SelectNodes("//BkgDetails/QtdShp");

				foreach (XmlNode qtdShp in qtdShps)
				{
					XmlNode globalProductCodeNode = qtdShp.SelectSingleNode("GlobalProductCode");
					if (globalProductCodeNode != null && globalProductCodeNode.InnerText == "P")
					{
						XmlNode shippingChargeNode = qtdShp.SelectSingleNode("ShippingCharge");
						if (shippingChargeNode != null)
						{
							responseStr = shippingChargeNode.InnerText;
							break;
						}
					}
				}

				returnStr = responseStr;
			}

			return returnStr;
		}


		public static string postXMLDHL(string dhlURL, string dhlXml)
		{
			string ZPL2GetStr = string.Empty;
			string ZPL2SetStr = string.Empty;
			string returnStr = string.Empty;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dhlURL);
			byte[] bytes;
			bytes = System.Text.Encoding.ASCII.GetBytes(dhlXml);
			request.ContentType = "text/xml; encoding='utf-8'";
			request.ContentLength = bytes.Length;
			request.Method = "POST";

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(bytes, 0, bytes.Length);
			requestStream.Close();
			HttpWebResponse response;
			response = (HttpWebResponse)request.GetResponse();

			if (response.StatusCode == HttpStatusCode.OK)
			{
				Stream responseStream = response.GetResponseStream();
				string responseStr = new StreamReader(responseStream).ReadToEnd();

				XmlDocument xml = new XmlDocument();
				xml.LoadXml(responseStr);
				XmlNodeList xnList = xml.GetElementsByTagName("OutputImage");

				// 가져 온 값 ZPL2 코드로 변환 하기 위해서 변수에 넣기
				foreach (XmlNode xn in xnList)
				{
					ZPL2GetStr = xn.InnerText;
				}

				if (!string.IsNullOrEmpty(ZPL2GetStr))
				{
					// BASE64로 DECODING하여 사용
					ZPL2SetStr = Base64Decoding(ZPL2GetStr);

					if (!string.IsNullOrEmpty(ZPL2SetStr))
					{
						// print로 전송
						Print(ZPL2SetStr);
						responseStr = "SUCCESS";
					}
				}

				returnStr = responseStr;
			}

			return returnStr;
		}

		// xml to string
		public static string GetXMLAsString(XmlDocument myxml)
		{
			return myxml.OuterXml;
		}

		// base64 decoding
		public static string Base64Decoding(string DecodingText, System.Text.Encoding oEncoding = null)
		{
			if (oEncoding == null)
				oEncoding = System.Text.Encoding.UTF8;

			byte[] arr = System.Convert.FromBase64String(DecodingText);
			return oEncoding.GetString(arr);
		}

		// 제브라 프린터로 전송 GK420
		public static void Print(string zplStr)
		{
			System.Text.Encoding korean = System.Text.Encoding.GetEncoding(949);
			byte[] bytes = korean.GetBytes(zplStr);
			IntPtr pUnmanagedBytes = new IntPtr(0);
			int nLength = bytes.Length;
			pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
			Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);

			RawPrinterHelper.SendBytesToPrinter(Printer, pUnmanagedBytes, nLength);
		}


	}
}

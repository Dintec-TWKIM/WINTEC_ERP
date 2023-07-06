using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dintec;
using System.IO;
using System.Net;

namespace Parsing
{
	public class JiBe
	{
		public string CD_COMPANY = string.Empty;
		public string NO_PREREG = string.Empty;

		// 자이브 용 
		string requisition_code = string.Empty;
		string client_uid = string.Empty;
		string supplier_uid = string.Empty;
		string quotation_code = string.Empty;
		string quotation_due_date = string.Empty;
		string priority = string.Empty;
		string delivery_remarks = string.Empty;
		string vessel_name = string.Empty;
		string total_items = string.Empty;
		string purchaser_name = string.Empty;
		string function = string.Empty;
		string system = string.Empty;
		string model = string.Empty;
		string particulars = string.Empty;
		string maker = string.Empty;
		string delivery_data = string.Empty;
		string delivery_port = string.Empty;
		string contact_number = string.Empty;
		string purchaser_remarks = string.Empty;
		string currency = string.Empty;
		string vat = string.Empty;
		string document_type = string.Empty;
		string rfq_attachments = string.Empty;
		string supplier_name = string.Empty;
		string issued_by = string.Empty;
		string port = string.Empty;
		string purchaser_email = string.Empty;
		string imo = string.Empty;

		string item_serial_number = string.Empty;
		string drawing_number = string.Empty;
		string part_number = string.Empty;
		string description = string.Empty;
		string detail_description = string.Empty;
		string unit = string.Empty;
		string requested_quantity = string.Empty;
		string item_remarks = string.Empty;



		#region 테크로스 SEND
		public tcrsSetRoot TCRSWrite(DataTable qtnH)
		{
			var tcrsSend = new tcrsSetRoot
			{
				header = new tcrsSetRootheader
				{
					TOTAL = Convert.ToInt32(qtnH.Rows.Count.ToString())
				}
			};

			// 이 부분 수정 필요, 동적으로 할당해서 보내기
			List<tcrsSetRootbody> cnt = new List<tcrsSetRootbody> { };

			for (int c = 0; c < qtnH.Rows.Count; c++)
			{
				cnt.Add(new tcrsSetRootbody()
				{
					RECDT = qtnH.Rows[c]["RECDT"].ToString(),
					SEQ = Convert.ToInt32(qtnH.Rows[c]["SEQ"].ToString()),
					OUTDT = qtnH.Rows[c]["OUTDT"].ToString(),
					CUSTNM = qtnH.Rows[c]["CUSTNM"].ToString(),
					IMONO = qtnH.Rows[c]["IMONO"].ToString(),
					SHIPNM = qtnH.Rows[c]["SHIPNM"].ToString(),
					DITEMCD = qtnH.Rows[c]["DITEMCD"].ToString(),
					TITEMCD = qtnH.Rows[c]["TITEMCD"].ToString(),
					ITEMNM = qtnH.Rows[c]["ITEMNM"].ToString(),
					QTY = Convert.ToDouble(qtnH.Rows[c]["QTY"].ToString()),
					QTYUNIT = qtnH.Rows[c]["QTYUNIT"].ToString(),
					AMTUNIT = qtnH.Rows[c]["AMTUNIT"].ToString(),
					UNP = Convert.ToDouble(qtnH.Rows[c]["UNP"].ToString()),
					AMT = Convert.ToDouble(qtnH.Rows[c]["AMT"].ToString()),
					REGDT = qtnH.Rows[c]["REGDT"].ToString()
				}) ;

				tcrsSend.body = cnt;
			}


			return tcrsSend;
		}

		#endregion 테크로스 SEND


		#region TCRS SEND
		public class tcrsSetRoot
		{
			[JsonProperty("header")]
			public tcrsSetRootheader header { get; set; }
			[JsonProperty("body")]
			public List<tcrsSetRootbody> body { get; set; }
		}

		public class tcrsSetRootheader
		{
			[JsonProperty("TOTAL")]
			public int TOTAL { get; set; }
		}

		public class tcrsSetRootbody
		{
			[JsonProperty("RECDT")] public string RECDT { get; set; }
			[JsonProperty("SEQ")] public int SEQ { get; set; }
			[JsonProperty("OUTDT")] public string OUTDT { get; set; }
			[JsonProperty("CUSTNM")] public string CUSTNM { get; set; }
			[JsonProperty("IMONO")] public string IMONO { get; set; }
			[JsonProperty("SHIPNM")] public string SHIPNM { get; set; }
			[JsonProperty("DITEMCD")] public string DITEMCD { get; set; }
			[JsonProperty("TITEMCD")] public string TITEMCD { get; set; }
			[JsonProperty("ITEMNM")] public string ITEMNM { get; set; }
			[JsonProperty("QTY")] public double QTY { get; set; }
			[JsonProperty("QTYUNIT")] public string QTYUNIT { get; set; }
			[JsonProperty("AMTUNIT")] public string AMTUNIT { get; set; }
			[JsonProperty("UNP")] public double UNP { get; set; }
			[JsonProperty("AMT")] public double AMT { get; set; }
			[JsonProperty("REGDT")] public string REGDT { get; set; }
		}

		#endregion TCRS SEND




		#region ========================= PULL RFQ =========================
		#region Request SEND
		public JibeSet JibeWrite(string typeStr)
		{
			var jibeset = new JibeSet
			{
				keys = new KeysRequest
				{
					//supplier_uid = "B0BE6483-52EB-4389-BA98-2FD5CA22A93B" // TEST
					supplier_uid = "2338458E-A076-4968-A0F3-1248AEC5D9E6"
				},
				body = new BodyRequest
				{
					//client_uid = "4169e684-6dc7-40bc-b36e-d6777ca53459", // TEST
					//supplier_uid = "B0BE6483-52EB-4389-BA98-2FD5CA22A93B", // TEST
					client_uid = "9C09B5C4-AE7E-4632-BC08-10C72626BD63",
					supplier_uid = "2338458E-A076-4968-A0F3-1248AEC5D9E6",
					document_type = typeStr
				}
			};
			return jibeset;
		}


		public class JibeSet
		{
			[JsonProperty("keys")] public KeysRequest keys { get; set; }
			[JsonProperty("body")] public BodyRequest body { get; set; }
		}

		public class KeysRequest
		{
			[JsonProperty("supplier_uid")] public string supplier_uid { get; set; }
		}

		public class BodyRequest
		{
			[JsonProperty("client_uid")] public string client_uid { get; set; }
			[JsonProperty("supplier_uid")] public string supplier_uid { get; set; }
			[JsonProperty("document_type")] public string document_type { get; set; }
		}
		#endregion 자이브 Request SEND


		#region Request Get

		public class requestGetRoot
		{
			[JsonProperty("keys")] public requestGetKeys keys { get; set; }
			[JsonProperty("body")] public requestGetBody body { get; set; }
			[JsonProperty("status")] public List<Status> status { get; set; }
		}

		public class requestGetKeys
		{
			[JsonProperty("supplier_uid")] public string supplier_uid { get; set; }
		}

		public class requestGetBody
		{
			[JsonProperty("keys")] public requestGetBody_keys keys { get; set; }
			[JsonProperty("body")] public requestGetBody_body body { get; set; }
		}
		public class requestGetBody_keys
		{
			[JsonProperty("client_uid")] public string client_uid { get; set; }
			[JsonProperty("supplier_uid")] public string supplier_uid { get; set; }
			[JsonProperty("quotation_code")] public string quotation_code { get; set; }
			[JsonProperty("requisition_code")] public string requisition_code { get; set; }
		}

		public class requestGetBody_body
		{
			[JsonProperty("client_uid")] public string client_uid { get; set; }
			[JsonProperty("supplier_uid")] public string supplier_uid { get; set; }
			[JsonProperty("requisition_code")] public string requisition_code { get; set; }
			[JsonProperty("quotation_code")] public string quotation_code { get; set; }
			[JsonProperty("quotation_due_date")] public string quotation_due_date { get; set; }
			[JsonProperty("priority")] public string priority { get; set; }
			[JsonProperty("delivery_remarks")] public string delivery_remarks { get; set; }
			[JsonProperty("vessel_name")] public string vessel_name { get; set; }
			[JsonProperty("total_items")] public int total_items { get; set; }
			[JsonProperty("purchaser_name")] public string purchaser_name { get; set; }
			[JsonProperty("function")] public string function { get; set; }
			[JsonProperty("system")] public string system { get; set; }
			[JsonProperty("model")] public string model { get; set; }
			[JsonProperty("particulars")] public string particulars { get; set; }
			[JsonProperty("maker")] public string maker { get; set; }
			[JsonProperty("delivery_date")] public string delivery_date { get; set; }
			[JsonProperty("delivery_port")] public string delivery_port { get; set; }
			[JsonProperty("contact_number")] public string contact_number { get; set; }
			[JsonProperty("purchaser_remarks")] public string purchaser_remarks { get; set; }
			[JsonProperty("currency")] public string currency { get; set; }
			[JsonProperty("vat")] public string vat { get; set; }
			[JsonProperty("rfq_attachments")] public string rfq_attachments { get; set; }
			[JsonProperty("imo")] public string imo { get; set; }
			[JsonProperty("purchaser_email")] public string purchaser_email { get; set; }
			[JsonProperty("port")] public string port { get; set; }
			[JsonProperty("issued_by")] public string issued_by { get; set; }
			[JsonProperty("supplier_name")] public string supplier_name { get; set; }
			[JsonProperty("document_type")] public string document_type { get; set; }
			[JsonProperty("Items")] public List<Items> Items { get; set; }
		}

		public class Items
		{
			[JsonProperty("item_serial_number")] public string item_serial_number { get; set; }
			[JsonProperty("drawing_number")] public string drawing_number { get; set; }
			[JsonProperty("part_number")] public string part_number { get; set; }
			[JsonProperty("description")] public string description { get; set; }
			[JsonProperty("detail_description")] public string detail_description { get; set; }
			[JsonProperty("unit")] public string unit { get; set; }
			[JsonProperty("requested_quantity")] public string requested_quantity { get; set; }
			//[JsonProperty("item_remarks")] public string item_remarks { get; set; }
		}

		public class Status
		{
			[JsonProperty("code")] public string code { get; set; }
			[JsonProperty("message")] public string message { get; set; }
		}

		#endregion 자이브 Request Get

		#endregion ========================= PULL RFQ =========================



		#region ========================= PUSH QUTATION =========================
		public string jibeQuotationPull(string fileNo)
		{
			try
			{
				string fileKey = fileNo;
				string returnStr = string.Empty;

				string query = string.Empty;
				query = "SELECT A.*, B.NM_ENG, CODE.NM_SYSDEF AS NM_EXCH FROM CZ_SA_QTNH AS A JOIN MA_EMP AS B ON B.NO_EMP = A.NO_EMP JOIN MA_CODEDTL AS CODE ON A.CD_EXCH = CODE.CD_SYSDEF AND A.CD_COMPANY = CODE.CD_COMPANY AND CODE.CD_FIELD = 'MA_B000005' WHERE NO_FILE = '" + fileKey + "'";
				DataTable qtnH = DBMgr.GetDataTable(query);

				query = "SELECT * FROM CZ_SA_QTNL WHERE NO_FILE = '" + fileKey + "' ORDER BY NO_LINE";
				DataTable qtnL = DBMgr.GetDataTable(query);

				query = "SELECT * FROM CZ_SA_QTN_PREREG_HEAD AS HEAD LEFT JOIN CZ_SA_QTN_PREREG AS PRE ON HEAD.NO_PREREG = PRE.NO_PREREG AND HEAD.CD_COMPANY = PRE.CD_COMPANY WHERE 1=1 AND HEAD.CD_COMPANY = 'K100' AND HEAD.NO_FILE = '" + fileKey + "'";
				DataTable preg = DBMgr.GetDataTable(query);


				//string tokenvalue = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjoiQlg2ckhrUkRjemowNTlsUDdrS3FDaTVCVWQxRjAzNWtnaFI2d3JsN3FubnNPOHZrTUViRUhheW1WNmVUSlVMek1ZYUYzeXczSm5DRjNEWjJMekFIRjU5WWdWYStQMjlZc28xWnpBN0R5YjVvSjVsY3J2NnZBeTM2L1VKeTBiVGVLcW8remtNOTBFd3A4UEtGK0tmT0dRPT0iLCJpYXQiOjE1OTM3Nzc4NjIsImV4cCI6MTU5NDY0MTg2Mn0.T6iG-Y1t-eDLykk9XZz3pA8B7Z_nMZfZd7ectlJnISc";

				// TEST
				//string tokenvalue = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7ImNsaWVudF91aWQiOiJkM2Q5ODkxMi1hMTY1LTRhODYtOGY4YS1jNWQ3Y2IyMjZkMTMiLCJzdXBwbGllcl91aWQiOiJCMEJFNjQ4My01MkVCLTQzODktQkE5OC0yRkQ1Q0EyMkE5M0IifSwiaWF0IjoxNTY3NzYzNTE0LCJleHAiOjE1OTkyOTk1MTR9.c3jbm6laoe2aFLcXwvRDt6AIRMWT51ZCaEzv0pE3Q2g";
				string tokenvalue = "c3jbm6laoe2aFLcXwvRDt6AIRMWT51ZCaEzv0pE3Q2g";

				if (qtnH != null && qtnL != null)
				{
					// QUOTATION
					//HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds.jibe.solutions/api/supplier/quotation_response/");
					// test
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://jcds-test.jibe.solutions/api/supplier/quotation_response/");
					httpWebRequest.ContentType = "application/json";
					httpWebRequest.Method = "POST";
					httpWebRequest.Headers.Add("token", tokenvalue);


					HttpWebResponse responseData = null;

					using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
					{
						var JibeValue = JibeQuotationWrite(qtnH, qtnL, preg);

						var jsonToWrite = JsonConvert.SerializeObject(JibeValue, Newtonsoft.Json.Formatting.Indented);

						streamWriter.Write(jsonToWrite);
					}

					responseData = (HttpWebResponse)httpWebRequest.GetResponse();

					HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
					using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
					{
						var result = streamReader.ReadToEnd();

						returnStr = result;
					}
				}

				return "";
			}
			catch (Exception e)
			{
				e.Message.ToString();
				return "";
			}
		}



		#region 자이브 Quotation Set
		public class quotationSetRoot
		{
			[JsonProperty("key")]
			public quotationSetKeys keys { get; set; }
			[JsonProperty("body")]
			public quotationSetBody body { get; set; }
		}


		public class quotationSetKeys
		{
			[JsonProperty("client_uid")]
			public string client_uid { get; set; }
			[JsonProperty("supplier_uid")]
			public string supplier_uid { get; set; }
			[JsonProperty("requisition_code")]
			public string requisition_code { get; set; }
			[JsonProperty("quotation_code")]
			public string quotation_code { get; set; }
		}

		public class quotationSetBody
		{
			[JsonProperty("client_uid")]
			public string client_uid { get; set; }
			[JsonProperty("supplier_uid")]
			public string supplier_uid { get; set; }
			[JsonProperty("quotation_code")]
			public string quotation_code { get; set; }
			[JsonProperty("truck_cost")]
			public string truck_cost { get; set; }
			[JsonProperty("packing_handling_charges")]
			public string packing_handling_charges { get; set; }
			[JsonProperty("freight_cost")]
			public string freight_cost { get; set; }
			[JsonProperty("other_charges")]
			public string other_charges { get; set; }
			[JsonProperty("vat")]
			public string vat { get; set; }
			[JsonProperty("quotation_discount")]
			public string quotation_discount { get; set; }
			[JsonProperty("reason_other_charges")]
			public string reason_other_charges { get; set; }
			[JsonProperty("reason_transport_charges")]
			public string reason_transport_charges { get; set; }
			[JsonProperty("supplier_quotation_reference")]
			public string supplier_quotation_reference { get; set; }
			[JsonProperty("quotation_creator")]
			public string quotation_creator { get; set; }
			[JsonProperty("delivery_terms")]
			public string delivery_terms { get; set; }
			[JsonProperty("quotation_remarks")]
			public string quotation_remarks { get; set; }
			[JsonProperty("origin")]
			public string origin { get; set; }
			[JsonProperty("legal_terms")]
			public string legal_terms { get; set; }
			[JsonProperty("quoted_items_number")]
			public string quoted_items_number { get; set; }
			[JsonProperty("quoted_currency")]
			public string quoted_currency { get; set; }
			[JsonProperty("requisition_code")]
			public string requisition_code { get; set; }
			[JsonProperty("document_type")]
			public string document_type { get; set; }
			[JsonProperty("quotation_validity_date")]
			public string quotation_validity_date { get; set; }

			[JsonProperty("items")]
			public List<quotationSetBody_Items> items { get; set; }
		}


		public class quotationSetBody_Items
		{
			[JsonProperty("drawing_number")] public string drawing_number { get; set; }
			[JsonProperty("part_number")] public string part_number { get; set; }
			[JsonProperty("unit_price")] public string unit_price { get; set; }
			[JsonProperty("discount")] public string discount { get; set; }
			[JsonProperty("item_type")] public string item_type { get; set; }
			[JsonProperty("lead_time")] public string lead_time { get; set; }
			[JsonProperty("total_amounts")] public string total_amounts { get; set; }
			[JsonProperty("supplier_remarks")] public string supplier_remarks { get; set; }
			[JsonProperty("uom")] public string uom { get; set; }
			[JsonProperty("requested_qty")] public string requested_qty { get; set; }
		}

		#endregion 자이브 Quotation Set


		#region 자이브 Quotation SEND
		public quotationSetRoot JibeQuotationWrite(DataTable qtnH, DataTable qtnL, DataTable preg)
		{
			// ORIGIN
			string originStr = qtnH.Rows[0]["ORIGIN"].ToString();
			if (originStr.ToUpper().Contains("ORIGIN"))
				originStr = "154";
			else if(originStr.ToUpper().Contains("GENUINE"))
				originStr = "154";
			else if (originStr.ToUpper().Contains("OEM"))
				originStr = "155";
			else if (originStr.ToUpper().Contains("COPY"))
				originStr = "156";
			else
				originStr = "154";



			string incotermsStr = string.Empty;
			string dbPacking = qtnH.Rows[0]["TP_PACKING"].ToString();

			if (dbPacking.Equals("002") || dbPacking.Equals("004") || dbPacking.Equals("006") || dbPacking.Equals("009") || dbPacking.Equals("010"))
				incotermsStr = "291";
			else if (dbPacking.Equals("007") || dbPacking.Equals("003") || dbPacking.Equals("005") || dbPacking.Equals("008"))
				incotermsStr = "292";
					

			// 임시
			//incotermsStr = "291";


			var jibeQuotationeset = new quotationSetRoot
			{
				keys = new quotationSetKeys
				{
					//client_uid = "d3d98912-a165-4a86-8f8a-c5d7cb226d13",
					client_uid = "4169e684-6dc7-40bc-b36e-d6777ca53459",
					supplier_uid = "B0BE6483-52EB-4389-BA98-2FD5CA22A93B",
					requisition_code = preg.Rows[0]["NO_REF"].ToString(),
					quotation_code = preg.Rows[0]["NO_REQREF"].ToString()
				},
				body = new quotationSetBody
				{
					//client_uid = "d3d98912-a165-4a86-8f8a-c5d7cb226d13",
					client_uid = "4169e684-6dc7-40bc-b36e-d6777ca53459",
					supplier_uid = "B0BE6483-52EB-4389-BA98-2FD5CA22A93B",
					requisition_code = preg.Rows[0]["NO_REF"].ToString(),
					quotation_code = preg.Rows[0]["NO_REQREF"].ToString(),
					quotation_validity_date = qtnH.Rows[0]["DT_VALID"].ToString(), //유효일자

					// 운송비용 수정 필요 라인 -> 계산해서 합계로
					truck_cost = "",
					packing_handling_charges = "",
					freight_cost = "",
					other_charges = "",

					vat = "",
					quotation_discount = "",
					reason_other_charges = "",
					reason_transport_charges = "",

					supplier_quotation_reference = qtnH.Rows[0]["NO_FILE"].ToString(),
					quotation_creator = qtnH.Rows[0]["NM_ENG"].ToString(),
					delivery_terms = incotermsStr,
					quotation_remarks = qtnH.Rows[0]["DC_RMK_QTN"].ToString(),
					origin = originStr,
					legal_terms = "",
					quoted_items_number = qtnL.Rows.Count.ToString(),
					quoted_currency = qtnH.Rows[0]["NM_EXCH"].ToString(),
					document_type = "quotation",
				}
			};

			// 이 부분 수정 필요, 동적으로 할당해서 보내기
			List<quotationSetBody_Items> cnt = new List<quotationSetBody_Items> { };

			for (int c = 0; c < qtnL.Rows.Count; c++)
			{
				// 소수점 버리기
				string qtyChange = string.Format("{0:0.00}", double.Parse(qtnL.Rows[c]["QT"].ToString()));
				string dcChange = string.Format("{0:0.00}", double.Parse(qtnL.Rows[c]["RT_DC"].ToString()));

				cnt.Add(new quotationSetBody_Items()
				{
					drawing_number = "",
					part_number = qtnL.Rows[c]["CD_ITEM_PARTNER"].ToString(),
					unit_price = qtnL.Rows[c]["UM_EX_Q"].ToString(),
					discount = dcChange,
					item_type = originStr,
					lead_time = qtnL.Rows[c]["LT"].ToString(),
					total_amounts = qtnL.Rows[c]["AM_EX_S"].ToString(),
					supplier_remarks = qtnL.Rows[c]["DC_RMK"].ToString(),
					uom = preg.Rows[c]["UNIT"].ToString(),
					//uom = qtnL.Rows[c]["UNIT"].ToString(),
					requested_qty = qtyChange
				});

				jibeQuotationeset.body.items = cnt;
			}

			//jibeQuotationeset.body.items = cnt;


			return jibeQuotationeset;
		}

		#endregion 자이브 Quotation SEND


		

		#endregion ========================= PUSH QUTATION =========================



		public void RequestInsertDb(JArray objBody, JArray objBody_body)
		{
			string textLog = string.Empty;

			SqlConnection sqlConn;
			//Python python = new Python();
			sqlConn = new SqlConnection("server = 192.168.1.143; uid = NEOE; pwd = NEOE; database = NEOE");


			// 헤더부분
			foreach (JObject bodyObject in objBody_body)
			{
				textLog = string.Empty;

				JObject objBody_body_body = JObject.Parse(bodyObject["body"].ToString());

				textLog += "Requisition Number : " + objBody_body_body["requisition_code"].ToString();
				textLog += Environment.NewLine;
				//textLog += "Client Unique Number : " + (string)objBody_body_body["client_uid"];
				//textLog += Environment.NewLine;
				//textLog += "Supplier Unique Number : " + (string)objBody_body_body["supplier_uid"];
				//textLog += Environment.NewLine;
				textLog += "Quotation Code : " + (string)objBody_body_body["quotation_code"];
				textLog += Environment.NewLine;
				//textLog += "Quotation Due Date : " + (string)objBody_body_body["quotation_due_date"];
				//textLog += Environment.NewLine;
				//textLog += "Priority : " + (string)objBody_body_body["priority"];
				//textLog += Environment.NewLine;
				textLog += "Deleivery Remarks : " + (string)objBody_body_body["delivery_remarks"];
				textLog += Environment.NewLine;
				textLog += "VesselName : " + (string)objBody_body_body["vessel_name"];
				textLog += Environment.NewLine;
				textLog += "Total Items : " + (string)objBody_body_body["total_items"];
				textLog += Environment.NewLine;
				textLog += "Purchaser Name : " + (string)objBody_body_body["purchaser_name"];
				textLog += Environment.NewLine;
				textLog += "Department/Function : " + (string)objBody_body_body["function"];
				textLog += Environment.NewLine;
				textLog += "Catalogue/System : " + (string)objBody_body_body["system"];
				textLog += Environment.NewLine;
				textLog += "Model : " + (string)objBody_body_body["model"];
				textLog += Environment.NewLine;
				textLog += "Particulars : " + (string)objBody_body_body["particulars"];
				textLog += Environment.NewLine;
				textLog += "Maker : " + (string)objBody_body_body["maker"];
				textLog += Environment.NewLine;
				textLog += "Delivery Date : " + (string)objBody_body_body["delivery_data"];
				textLog += Environment.NewLine;
				textLog += "Delivery Port : " + (string)objBody_body_body["delivery_port"];
				textLog += Environment.NewLine;
				textLog += "Contact Number : " + (string)objBody_body_body["contact_number"];
				textLog += Environment.NewLine;
				textLog += "Purchaser Remarks : " + (string)objBody_body_body["purchaser_remarks"];
				textLog += Environment.NewLine;
				textLog += "Currency: " + (string)objBody_body_body["currency"];
				textLog += Environment.NewLine;
				//textLog += "Vat: " + (string)objBody_body_body["vat"];
				//textLog += Environment.NewLine;
				//textLog += "Attachments: " + (string)objBody_body_body["rfq_attachments"];
				//textLog += Environment.NewLine;
				textLog += "IMO: " + (string)objBody_body_body["imo"];
				textLog += Environment.NewLine;
				textLog += "PURCHASER EMAIL: " + (string)objBody_body_body["purchaser_email"];
				textLog += Environment.NewLine;
				textLog += "PORT: " + (string)objBody_body_body["port"];
				textLog += Environment.NewLine;
				textLog += "ISSUED BY: " + (string)objBody_body_body["issued_by"];
				textLog += Environment.NewLine;
				//textLog += "SUPPLIER NAME: " + (string)objBody_body_body["supplier_name"];
				//textLog += Environment.NewLine;
				//textLog += "DOCUMENT TYPE: " + (string)objBody_body_body["document_type"];
				//textLog += Environment.NewLine;


				string imonumber = string.Empty;
				string partnercode = string.Empty;
				string referencenumber = string.Empty;
				string partnerName = string.Empty;

				string no_file = string.Empty;

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

				string query = string.Empty;
				query = "DECLARE @NO_FILE NVARCHAR(10) ";
				query = query + "SELECT @NO_FILE = 'PR'+RIGHT('00000000' + CONVERT(varchar,MAX(CONVERT(INT, RIGHT(NO_PREREG,8))+1)),8) FROM CZ_SA_QTN_PREREG ";
				query = query + "SELECT @NO_FILE AS NO_FILE";
				DataTable dtFileNo = DBMgr.GetDataTable(query);

				partnerName = (string)objBody_body_body["issued_by"];

				CD_COMPANY = "K100";

				if (dtFileNo != null)
				{
					NO_PREREG = dtFileNo.Rows[0]["NO_FILE"].ToString();
				}

				no_file = NO_PREREG;

				if (partnerName.ToUpper().Contains("SINGAPORE"))
					partnercode = "00879";
				else if (partnerName.ToUpper().Contains("HONG KONG"))
					partnercode = "00878";
				else if (partnerName.ToUpper().Contains("LABUAN"))
					partnercode = "02056";


				requisition_code = (string)objBody_body_body["requisition_code"];
				client_uid = (string)objBody_body_body["client_uid"];
				supplier_uid = (string)objBody_body_body["supplier_uid"];
				quotation_code = (string)objBody_body_body["quotation_code"];
				quotation_due_date = (string)objBody_body_body["quotation_due_date"];
				priority = (string)objBody_body_body["priority"];
				delivery_remarks = (string)objBody_body_body["delivery_remarks"];
				vessel_name = (string)objBody_body_body["vessel_name"];
				total_items = (string)objBody_body_body["total_items"];
				purchaser_name = (string)objBody_body_body["purchaser_name"];
				function = (string)objBody_body_body["function"];
				system = (string)objBody_body_body["system"];
				model = (string)objBody_body_body["model"];
				particulars = (string)objBody_body_body["particulars"];
				maker = (string)objBody_body_body["maker"];
				delivery_data = (string)objBody_body_body["delivery_data"];
				delivery_port = (string)objBody_body_body["delivery_port"];
				contact_number = (string)objBody_body_body["contact_number"];
				purchaser_remarks = (string)objBody_body_body["purchaser_remarks"];
				currency = (string)objBody_body_body["currency"];
				vat = (string)objBody_body_body["vat"];
				rfq_attachments = (string)objBody_body_body["rfq_attachments"];
				imo = (string)objBody_body_body["imo"];


				imonumber = imo;
				vesselName = vessel_name;

				referencenumber = requisition_code;


				// 중복제거 한달로 설정 => 202004
				DateTime datetime = new DateTime();
				datetime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
				datetime = datetime.AddMonths(-1);

				string dateStr = datetime.ToString("yyyyMMddHHmmss");

				if (!string.IsNullOrEmpty(imonumber))
				{
					query = "SELECT NO_PREREG FROM CZ_SA_QTN_PREREG WHERE 1=1  ";
					query = query + " AND CD_COMPANY = '" + CD_COMPANY + "'";
					query = query + " AND NO_LINE = '1'";
					query = query + " AND NO_REF = '" + referencenumber + "'";
					query = query + " AND DTS_INSERT > '" + dateStr + "'";
					query = query + " AND NO_IMO = '" + imonumber + "'";
				}
				else if (!string.IsNullOrEmpty(partnercode))
				{
					query = "SELECT NO_PREREG FROM CZ_SA_QTN_PREREG WHERE 1=1  ";
					query = query + " AND CD_COMPANY = '" + CD_COMPANY + "'";
					query = query + " AND NO_LINE = '1'";
					query = query + " AND NO_REF = '" + referencenumber + "'";
					query = query + " AND DTS_INSERT > '" + dateStr + "'";
					query = query + " AND CD_PARTNER = '" + partnercode + "'";
				}
				else
				{
					query = "SELECT NO_PREREG FROM CZ_SA_QTN_PREREG WHERE 1=1  ";
					query = query + " AND CD_COMPANY = '" + CD_COMPANY + "'";
					query = query + " AND NO_LINE = '1'";
					query = query + " AND NO_REF = '" + referencenumber + "'";
					query = query + " AND DTS_INSERT > '" + dateStr + "'";
				}


				string query2 = string.Empty;

				if (!string.IsNullOrEmpty(imonumber))
				{
					query2 = "SELECT NO_FILE, NO_EMP FROM CZ_SA_QTNH WHERE 1=1  ";
					query2 = query2 + " AND CD_COMPANY = '" + CD_COMPANY + "'";
					query2 = query2 + " AND NO_REF = '" + referencenumber + "'";
					query2 = query2 + " AND DTS_INSERT > '" + dateStr + "'";
					query2 = query2 + " AND NO_IMO = '" + imonumber + "'";
				}
				else if (!string.IsNullOrEmpty(partnercode))
				{
					query2 = "SELECT NO_FILE, NO_EMP FROM CZ_SA_QTNH WHERE 1=1  ";
					query2 = query2 + " AND CD_COMPANY = '" + CD_COMPANY + "'";
					query2 = query2 + " AND NO_REF = '" + referencenumber + "'";
					query2 = query2 + " AND DTS_INSERT > '" + dateStr + "'";
					query2 = query2 + " AND CD_PARTNER = '" + partnercode + "'";
				}
				else
				{
					query2 = "SELECT NO_FILE, NO_EMP FROM CZ_SA_QTNH WHERE 1=1  ";
					query2 = query2 + " AND CD_COMPANY = '" + CD_COMPANY + "'";
					query2 = query2 + " AND NO_REF = '" + referencenumber + "'";
					query2 = query2 + " AND DTS_INSERT > '" + dateStr + "'";
				}


				DataTable insertResultDt = DBMgr.GetDataTable(query);
				DataTable insertResultQtnh = DBMgr.GetDataTable(query2);

				// 중복파일 검사하여 없을경우
				if (insertResultDt.Rows.Count == 0 && insertResultQtnh.Rows.Count == 0 && !string.IsNullOrEmpty(referencenumber))
				{
					DataTable dtItem = new DataTable();

					//dtItem = new DataTable();
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
					dtItem.Columns.Add("DC_BODY");
					dtItem.Columns.Add("SORTED_BY");
					dtItem.Columns.Add("COMMENT");
					dtItem.Columns.Add("CONTACT");
					dtItem.Columns.Add("DXCODE_SUBJ");
					dtItem.Columns.Add("DXCODE_ITEM");

					no_line = 1;



					JArray itemArray = JArray.Parse(objBody_body_body["items"].ToString());

					// 아이템 부분
					foreach (JObject itemObj in itemArray)
					{
						// 부대비용 제외
						string partcodeCheck = itemObj["part_number"].ToString();
						if (partcodeCheck.Equals("truck_cost") || partcodeCheck.Equals("other_charges") || partcodeCheck.Equals("barge_workboat_cost") || partcodeCheck.Equals("freight_cost") || partcodeCheck.Equals("packaging_charges"))
							break;

						#region 로고
						textLog += Environment.NewLine;
						textLog += "****************************************************************************************************";
						textLog += Environment.NewLine;
						textLog += "Item Serial Number : " + itemObj["item_serial_number"].ToString();
						textLog += Environment.NewLine;
						textLog += "Drawing Number : " + itemObj["drawing_number"].ToString();
						textLog += Environment.NewLine;
						textLog += "Part Number : " + itemObj["part_number"].ToString();
						textLog += Environment.NewLine;
						textLog += "Description : " + itemObj["description"].ToString();
						textLog += Environment.NewLine;
						textLog += "Long Description : " + itemObj["detail_description"].ToString();
						textLog += Environment.NewLine;
						textLog += "Unit : " + itemObj["unit"].ToString();
						textLog += Environment.NewLine;
						textLog += "Requested Quantity : " + itemObj["requested_quantity"].ToString();
						textLog += Environment.NewLine;
						textLog += "Item Remarks : " + itemObj["item_remarks"].ToString();
						textLog += Environment.NewLine;
						textLog += "****************************************************************************************************";
						textLog += Environment.NewLine;
						#endregion 로고

						item_serial_number = itemObj["item_serial_number"].ToString();
						drawing_number = itemObj["drawing_number"].ToString();
						part_number = itemObj["part_number"].ToString();
						description = itemObj["description"].ToString();
						detail_description = itemObj["detail_description"].ToString();
						unit = itemObj["unit"].ToString();
						requested_quantity = itemObj["requested_quantity"].ToString();
						item_remarks = itemObj["item_remarks"].ToString();


						// 주제
						if (!string.IsNullOrEmpty(function))
							nm_subject = function;

						if (!string.IsNullOrEmpty(system))
							nm_subject = nm_subject + " " + system;

						if (!string.IsNullOrEmpty(model))
							nm_subject = nm_subject + " model:" + model;

						if (!string.IsNullOrEmpty(particulars))
							nm_subject = nm_subject + " particulars:" + particulars;

						if (!string.IsNullOrEmpty(maker))
							nm_subject = nm_subject + " maker:" + maker;

						nm_subject = nm_subject.ToUpper().Trim();



						// 품명
						nm_item_partner = description.ToUpper().Trim();

						if (!string.IsNullOrEmpty(drawing_number))
							nm_item_partner = nm_item_partner + Environment.NewLine + "dwg no:" + drawing_number;

						if (!string.IsNullOrEmpty(detail_description))
						{
							if (nm_subject.ToLower().Contains("stores"))
							{
								nm_item_partner = nm_item_partner + " / " + detail_description;
							}
						}

						if (!string.IsNullOrEmpty(item_serial_number))
							nm_item_partner = nm_item_partner + Environment.NewLine + "serial no: " + item_serial_number;

						if (!string.IsNullOrEmpty(item_remarks))
							nm_item_partner = nm_item_partner + Environment.NewLine + item_remarks;


						// 코드
						cd_item_partner = part_number;
						cd_uniq_partner = "";


						qt = requested_quantity;

						dtItem.Rows.Add();
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_COMPANY"] = CD_COMPANY;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_PREREG"] = no_file;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_LINE"] = no_line;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_PARTNER"] = partnercode;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_IMO"] = imonumber;
						dtItem.Rows[dtItem.Rows.Count - 1]["SHIPSERV_TNID"] = tnid;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_VESSEL"] = vesselName;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_REF"] = referencenumber;
						dtItem.Rows[dtItem.Rows.Count - 1]["NO_REQREF"] = quotation_code;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_SUBJECT"] = nm_subject;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_ITEM_PARTNER"] = cd_item_partner;
						dtItem.Rows[dtItem.Rows.Count - 1]["NM_ITEM_PARTNER"] = nm_item_partner;
						dtItem.Rows[dtItem.Rows.Count - 1]["CD_UNIQ_PARTNER"] = cd_uniq_partner;

						if (unit.Contains(",")) unit = "";
						dtItem.Rows[dtItem.Rows.Count - 1]["UNIT"] = unit;
						if (string.IsNullOrEmpty(qt)) qt = "0";
						if (qt.Contains(",")) qt = qt.Replace(",", "").Trim();
						dtItem.Rows[dtItem.Rows.Count - 1]["QT"] = qt;
						//dtItem.Rows[dtItem.Rows.Count - 1]["SORTED_BY"] = "JiBe";
						dtItem.Rows[dtItem.Rows.Count - 1]["SORTED_BY"] = "MA02529";
						dtItem.Rows[dtItem.Rows.Count - 1]["COMMENT"] = item_remarks;
						dtItem.Rows[dtItem.Rows.Count - 1]["CONTACT"] = purchaser_name;
						dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_SUBJ"] = Util.GetDxCode(nm_subject.ToUpper());
						dtItem.Rows[dtItem.Rows.Count - 1]["DXCODE_ITEM"] = Util.GetDxCode(cd_item_partner.ToUpper()) + "‡" + nm_item_partner.ToUpper();

						no_line += 1;
					}

					string textPath = @"C:\ERPU\ANGLO-EASTERN_JiBe_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";




					String html = @"<html>
	<head>
		<style type = 'text/css'>
			 html, body, div, span, table, thead, tbody, tfoot, tr, th, td, img { margin: 0; padding: 0; border: 0; outline: 0; line - height:1; font - family:맑은 고딕; font - size:10pt; }
				body {
					background - color:#f6f7f8; }
		</style>
	</head >
	<body>" + textLog.Replace("\r\n", "<br>") + @"

	</body>
</html> ";

					Html.ConvertPdf(textPath, html);


					//// text 파일 저장
					//	System.IO.File.WriteAllText(textPath, textLog, Encoding.Default);

					//// PDF 파일 저장
					//System.IO.TextReader readFile = new StreamReader(textPath);


					string xml = Util.GetTO_Xml(dtItem);
					DBMgr.ExecuteNonQuery("SP_CZ_SA_QTN_PREREG", new object[] { xml });

					//DataTable partnerCompany = python.FindSupplier(CD_COMPANY, no_file);
					//////DataTable partnerStock = python.FindStock(CD_COMPANY, no_file);    // 재고
					//if (partnerCompany != null && partnerCompany.Rows.Count > 0)
					//{
					//	//NO_LINE // CD_SUPPLIER // RATE
					//	for (int c = 0; c < partnerCompany.Rows.Count; c++)
					//	{
					//		no_line = Convert.ToInt32(partnerCompany.Rows[c]["NO_LINE"].ToString());
					//		cd_po_partner = partnerCompany.Rows[c]["CD_SUPPLIER"].ToString();
					//		cd_rate = partnerCompany.Rows[c]["RATE"].ToString();

					//		DBMgr.ExecuteNonQuery("PS_CZ_SA_QTN_PREREG_UPDATE", new object[] { CD_COMPANY, no_file, no_line, cd_po_partner, cd_rate });
					//	}
					//}

					imonumber = string.Empty;
					partnercode = string.Empty;


					//PreregisterInquiry("K100", no_file, textPath);

					Preregister p = new Preregister();
					bool testTnf = p.Inquiry(CD_COMPANY, no_file, textPath);

					sqlConn.Close();
				}
				else
				{
					string[] recipients = { "S-458", insertResultQtnh.Rows[0]["NO_EMP"].ToString() };
					string msgBody = "* JiBe 중복 수신 알림\r\n\r\n문의번호 : " + referencenumber +"\r\n파일번호 : "+ insertResultQtnh.Rows[0]["NO_FILE"].ToString()+ "\r\n\r\n웹 사이트 확인 바랍니다." ;
					Dintec.MSG.SendMSG("ERP",recipients,msgBody);
				}
			}
		}



		#region 자이브 Status SEND
		JibeStatusSet JibeStatusWrite()
		{
			var jibeStatusset = new JibeStatusSet
			{
				keys = new KeysStatus
				{
					supplier_uid = "B0BE6483-52EB-4389-BA98-2FD5CA22A93B"
				},
				body = new BodyStatus
				{
					client_uid = "4169e684-6dc7-40bc-b36e-d6777ca53459",
					supplier_uid = "B0BE6483-52EB-4389-BA98-2FD5CA22A93B",
					requisition_code = "",
					quotation_code = "",
					document_type = "rfq"
				}
			};
			return jibeStatusset;
		}

		public class JibeStatusSet
		{
			public KeysStatus keys { get; set; }
			
			public BodyStatus body { get; set; }
		}

		public class KeysStatus
		{
			public string supplier_uid { get; set; }
		}

		public class BodyStatus
		{
			public string client_uid { get; set; }
			public string supplier_uid { get; set; }
			public string requisition_code { get; set; }
			public string quotation_code { get; set; }
			public string document_type { get; set; }

		}

		#endregion 자이브 Status SEND



	}
}

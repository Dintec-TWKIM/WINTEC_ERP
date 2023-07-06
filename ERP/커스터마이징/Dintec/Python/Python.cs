using System;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Dintec
{
	public class Python
	{
		public Python()
		{

		}

        public DataTable FindSupplier(string company, string fileNo)
        {
            Uri address;
            WebClient webClient = null;
			XmlDocument xmlDoc;
			XmlElement root;
			DataTable dt;
			DataRow dr;
			string xml;

            try
            {
                address = new Uri("http://192.168.1.162:8080/FindSupplier/" + company + "," + fileNo);
                webClient = new WebClient() { Encoding = Encoding.UTF8 };
                xml = webClient.DownloadString(address);
				xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xml);

				root = xmlDoc.DocumentElement;

				dt = new DataTable();

				dt.Columns.Add("NO_LINE");
				dt.Columns.Add("CD_SUPPLIER");
				dt.Columns.Add("RATE");

				foreach (XmlElement item in root.GetElementsByTagName("item"))
				{
					foreach (XmlElement supplier in item.GetElementsByTagName("supplier"))
					{
						dr = dt.NewRow();

						dr["NO_LINE"] = item["index"].InnerText;
						dr["CD_SUPPLIER"] = supplier["code"].InnerText;
						dr["RATE"] = supplier["rate"].InnerText;

						dt.Rows.Add(dr);
					}
				}

				return dt;
			}
            catch (Exception)
            {
				return null;
            }
        }

		public DataTable FindSalesType(string company, string fileNo)
		{
			Uri address;
			WebClient webClient = null;
			XmlDocument xmlDoc;
			XmlElement root;
			DataTable dt;
			DataRow dr;
			string xml;

			try
			{
				address = new Uri("http://192.168.1.162:8080/FindType/" + company + "," + fileNo);
				webClient = new WebClient() { Encoding = Encoding.UTF8 };
				xml = webClient.DownloadString(address);
				xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xml);

				root = xmlDoc.DocumentElement;

				dt = new DataTable();

				dt.Columns.Add("NO_LINE");
				dt.Columns.Add("TP_SALES");
				dt.Columns.Add("RATE");

				foreach (XmlElement item in root.GetElementsByTagName("item"))
				{
					foreach (XmlElement type in item.GetElementsByTagName("type"))
					{
						dr = dt.NewRow();

						dr["NO_LINE"] = item["index"].InnerText;
						dr["TP_SALES"] = type["code"].InnerText;
						dr["RATE"] = type["rate"].InnerText;

						dt.Rows.Add(dr);
					}
				}

				return dt;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public DataTable FindStock(string company, string fileNo)
		{
			Uri address;
			WebClient webClient = null;
			XmlDocument xmlDoc;
			XmlElement root;
			DataTable dt;
			DataRow dr;
			string xml;

			try
			{
				address = new Uri("http://192.168.1.162:8080/FindStock/" + company + "," + fileNo);
				webClient = new WebClient() { Encoding = Encoding.UTF8 };
				xml = webClient.DownloadString(address);
				xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(xml);

				root = xmlDoc.DocumentElement;

				dt = new DataTable();

				dt.Columns.Add("NO_LINE");
				dt.Columns.Add("CD_ITEM");
				dt.Columns.Add("RATE");

				foreach (XmlElement item in root.GetElementsByTagName("item"))
				{
					foreach (XmlElement stock in item.GetElementsByTagName("stock"))
					{
						dr = dt.NewRow();

						dr["NO_LINE"] = item["index"].InnerText;
						dr["CD_ITEM"] = stock["code"].InnerText;
						dr["RATE"] = stock["rate"].InnerText;

						dt.Rows.Add(dr);
					}
				}

				return dt;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}

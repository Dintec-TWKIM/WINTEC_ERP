using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DX
{
	public class LOCAL
	{
		public static string GetHostName()
		{
			return Dns.GetHostName();
		}

		public static string GetIpAddress()
		{
			foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}

			return "";
		}		
	}
}

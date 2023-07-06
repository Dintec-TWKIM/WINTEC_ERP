using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Duzon.Common.Forms;
using Duzon.Common.Repository;

namespace DX
{
	public static class 상수
	{
		public static string 회사코드 => Global.MainFrame.LoginInfo.CompanyCode;
		public static string 사업장코드 => Global.MainFrame.LoginInfo.BizAreaCode;
		public static string 공장코드 => Global.MainFrame.LoginInfo.CdPlant;
		public static string 회계코드 => Global.MainFrame.LoginInfo.CdPc;
		public static string 사원번호 => Global.MainFrame.LoginInfo.UserID;
		public static string 사원이름 => Global.MainFrame.LoginInfo.UserName;
		public static string 페이지ID => Global.MainFrame.CurrentPageID;
		public static string 호스트URL => Global.MainFrame.HostURL;
		public static string 언어 => Global.MainFrame.LoginInfo.Language;

		public static DateTime 오늘날짜 => Global.MainFrame.GetDateTimeToday();

		public static Color 필수값색상 => Color.FromArgb(255, 237, 242);

		public static string 저장경로
		{
			get
			{
				string path = Application.StartupPath + @"\temp\";
				Directory.CreateDirectory(path);
				return path;
			}
		}


		public static string 디비커넥션_ERP => ClientRepository.DatabaseCallType == "Direct" ? ClientRepository.ConString : "Server=113.130.254.143; Database=NEOE; Uid=NEOE; Password=NEOE";
		public static string 디비켜넥션_GW => "Server=113.130.254.143; Database=NeoBizboxS2; Uid=NEOE; Password=NEOE";


		public static string IP
		{
			get
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


		public static int 넓이_파일번호 => 95;

		public static int 넓이_순번 => 45;

		public static int 넓이_품목코드 => 140;

		public static int 넓이_재고코드 => 105;

		public static int 넓이_U코드 => 105;

		public static int 넓이_담당자 => 80;

		public static int 넓이_일자 => 85;

		public static int 넓이_수량 => 60;

		public static int 넓이_단가 => 90;





		public static string 메일패턴 =>
			  @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
			+ @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
			+ @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
			+ @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
	}
}

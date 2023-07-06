using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using Aspose.Email.Exchange.Schema;
using C1.Win.C1FlexGrid;
using Dass.FlexGrid;
using Duzon.Common.BpControls;
using Duzon.Common.Controls;
using Duzon.Common.Forms;
using Duzon.Common.Forms.Help;
using Newtonsoft.Json;

namespace DX
{
	public static class EXTS
	{

		private static string 회사코드 => Global.MainFrame.LoginInfo.CompanyCode;
		private static string 사업장코드 => Global.MainFrame.LoginInfo.BizAreaCode;
		private static string 공장코드 => Global.MainFrame.LoginInfo.CdPlant;
		private static string 사원번호 => Global.MainFrame.LoginInfo.UserID;



		



		
	}
}

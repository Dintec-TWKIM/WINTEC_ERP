using Duzon.Common.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dintec
{
	public class RPA
	{
		public string RpaCode { get; set; }

		public string Process { get; set; }

		public string WorkStep { get; set; }

		public string FileNumber { get; set; }

		public string PartnerCode { get; set; }

		public string DelayMin { get; set; }	// Int로 하면 기본값이 0으로 들어가서 string으로 해줌 (null값으로 들어가기 위해)

		public string Urgent { get; set; }      // Int로 하면 기본값이 0으로 들어가서 string으로 해줌 (null값으로 들어가기 위해)


		public static void AddWork(string rpaCode, string fileNumber, string partnerCode, int delayMin, int urgent)
		{
			SQL sql = new SQL("PX_CZ_RPA_WORK_QUEUE", SQLType.Procedure);
			sql.Parameter.Add2("@CD_COMPANY", Global.MainFrame.LoginInfo.CompanyCode);
			sql.Parameter.Add2("@CD_RPA"	, rpaCode);
			sql.Parameter.Add2("@NO_FILE"	, fileNumber);
			sql.Parameter.Add2("@CD_PARTNER", partnerCode);
			sql.Parameter.Add2("@DELAY_MIN"	, delayMin);
			sql.Parameter.Add2("@URGENT"	, urgent);
			sql.Parameter.Add2("@ID_INSERT"	, Global.MainFrame.LoginInfo.UserID);
			sql.ExecuteNonQuery();
		}

		public void AddQueue()
		{
			SQL sql = new SQL("PX_CZ_RPA_WORK_QUEUE_4", SQLType.Procedure);
			sql.Parameter.Add2("@CD_COMPANY"	, Global.MainFrame.LoginInfo.CompanyCode);
			sql.Parameter.Add2("@CD_RPA"		, RpaCode);
			sql.Parameter.Add2("@CD_PROCESS"	, Process);
			sql.Parameter.Add2("@CD_WORKSTEP"	, WorkStep);
			sql.Parameter.Add2("@NO_FILE"		, FileNumber);
			sql.Parameter.Add2("@CD_PARTNER"	, PartnerCode);
			sql.Parameter.Add2("@DELAY_MIN"		, DelayMin);
			sql.Parameter.Add2("@URGENT"		, Urgent);
			sql.Parameter.Add2("@ID_INSERT"		, Global.MainFrame.LoginInfo.UserID);
			sql.ExecuteNonQuery();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Duzon.Common.Util;
using Duzon.ERPU;

namespace Dintec
{
	public class WorkFlow
	{
		DataTable dtH;
		DataTable dtL;

		public string CD_COMPANY { get; set; }

		public string NO_KEY { get; set; }

		public string TP_STEP { get; set; }

		public int NO_HST { get; set; }		

		public WorkFlow()
		{
			InitMember();
		}

		public WorkFlow(string NO_KEY, string TP_STEP)
		{
			InitMember();

			this.NO_KEY = NO_KEY;
			this.TP_STEP = TP_STEP;
			this.NO_HST = -1;
		}

		public WorkFlow(string NO_KEY, string TP_STEP, int NO_HST)
		{
			InitMember();

			this.NO_KEY = NO_KEY;
			this.TP_STEP = TP_STEP;
			this.NO_HST = NO_HST;
		}

		private void InitMember()
		{
			dtH = new DataTable();
			dtH.Columns.Add("NO_KEY");
			dtH.Columns.Add("TP_STEP");

			dtL = new DataTable();
			dtL.Columns.Add("NO_KEY");
			dtL.Columns.Add("TP_STEP");
			dtL.Columns.Add("NO_KEY_REF");
			dtL.Columns.Add("CD_SUPPLIER");
			dtL.Columns.Add("NO_HST");
			dtL.Columns.Add("NM_FILE");
			dtL.Columns.Add("NM_FILE_REAL");
		}

		public void AddItem(string NO_KEY_REF, string CD_SUPPLIER, string NM_FILE, string NM_FILE_REAL)
		{
			DataRow row = dtL.NewRow();
			row["NO_KEY"] = NO_KEY;
			row["TP_STEP"] = TP_STEP;
			if (NO_HST != -1) row["NO_HST"] = NO_HST;

			// 파라미터
			if (NO_KEY_REF != "")	row["NO_KEY_REF"] = NO_KEY_REF;
			if (CD_SUPPLIER != "")	row["CD_SUPPLIER"] = CD_SUPPLIER;
			if (NM_FILE != "")		row["NM_FILE"] = NM_FILE;
			if (NM_FILE_REAL != "") row["NM_FILE_REAL"] = NM_FILE_REAL;

			dtL.Rows.Add(row);
		}

		public void Save()
		{
			dtH.Rows.Add(NO_KEY, TP_STEP);

			if (!string.IsNullOrEmpty(CD_COMPANY))
			{
				dtH.Columns.Add("CD_COMPANY", typeof(string));
				dtL.Columns.Add("CD_COMPANY", typeof(string));

				foreach (DataRow row in dtH.Rows)
					row["CD_COMPANY"] = CD_COMPANY;

				foreach (DataRow row in dtL.Rows)
					row["CD_COMPANY"] = CD_COMPANY;
			}

			SpInfoCollection si = new SpInfoCollection();
			si.Add(Util.SetSpInfo(dtL, "SP_CZ_MA_WORKFLOWL_XML"));
			si.Add(Util.SetSpInfo(dtH, "SP_CZ_MA_WORKFLOWH_XML"));
			DBHelper.Save(si);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dintec.DHL
{
	public class UnitConvert_dhl
	{
		public static string ConvertDHL(object unit)
		{
			DataTable dtDhl = GetDb.DHLUnitCodeGet();

			string newUnit = "";

			if (string.IsNullOrEmpty(unit.ToString()))
				newUnit = "PCS";
			else
			{
				DataRow[] row = dtDhl.Select("CD_FLAG3 LIKE '%" + unit + "%'");

				if (row.Length > 0)
				{
					newUnit = row[0]["CODE"].ToString();
				}

				if (unit.Equals("PCS"))
				{
					newUnit = "PCS";
				}

				//없는 항목은 PCS로 통일
				if (string.IsNullOrEmpty(newUnit))
				{
					newUnit = "PCS";
				}
			}

			return newUnit;
		}
	}
}

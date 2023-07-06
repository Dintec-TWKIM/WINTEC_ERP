using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dintec
{
	public static class CT
	{
		public static int Int(object value)
		{
			if (value == null || value.ToString() == "")
				return 0;
			else
				return Convert.ToInt32(value);
		}

		public static string String(object value)
		{
			if (value == null)
				return "";
			else
				return value.ToString();
		}

		public static decimal Decimal(object value)
		{
			if (value == null)
				return 0;

			if (decimal.TryParse(value.ToString(), out decimal d))
				return d;
			else
				return 0;
		}
	}
}

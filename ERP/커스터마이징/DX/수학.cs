using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DX
{
	public class 수학
	{
		public static int 작은수(int a, int b) => a < b ? a : b;

		public static decimal 작은수(decimal a, decimal b) => a < b ? a : b;

		public static int 큰수(int a, int b) => a > b ? a : b;

		public static decimal 큰수(decimal a, decimal b) => a > b ? a : b;
	}
}

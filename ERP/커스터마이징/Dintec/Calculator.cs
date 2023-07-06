using System;

namespace Dintec
{
	public class Calculator
	{
		public static decimal 반올림(decimal value)
		{
			return Math.Round(value, MidpointRounding.AwayFromZero);
		}

		public static decimal 반올림(decimal value, decimal pos)
		{
			return 반올림(value, Convert.ToInt32(pos));
		}

		public static decimal 반올림(decimal value, int pos)
		{
			value = value * (decimal)Math.Pow(10, pos);
			value = Math.Round(value, MidpointRounding.AwayFromZero);
			value = value * (decimal)Math.Pow(10, pos * -1);

			return value;
		}

		public static decimal 버림(decimal value)
		{
			return 버림(value, 0);
		}

		public static decimal 버림(decimal value, int pos)
		{
			value = value * (decimal)Math.Pow(10, pos);
			value = Math.Truncate(value);
			value = value * (decimal)Math.Pow(10, pos * -1);

			return value;
		}

		public static decimal 이윤율계산(decimal 매입, decimal 매출)
		{
			if (매출 > 0 && 매입 > 매출 * 100)    // 차이가 너무 크면 overflow 뜨므로 예외처리 함
				return -999;
			else if (매출 > 0)
				return 반올림((1 - 매입 / 매출) * 100, 2);
			else
				return 0;
		}

		public static decimal 할인율계산(decimal 할인전, decimal 할인후)
		{
			if (할인전 > 0)
				return 반올림((1 - 할인후 / 할인전) * 100, 2);
			else
				return 0;
		}

		public static decimal 이윤율적용(decimal 단가, decimal 이윤율)
		{
			return 100 * 단가 / (100 - 이윤율);
		}

		public static decimal 할인율적용(decimal 단가, decimal 할인율)
		{
			return 단가 * (1 - 할인율 / 100);
		}
	}
}

namespace DX
{
	public class FoundBARCODE
	{
		public int Page
		{
			get;
		}

		public string Value
		{
			get;
		}

		public FoundBARCODE(int page, string value)
		{
			Page = page + 1;	// 인덱스가 0부터 시작하므로 1 더해줌
			Value = value;
		}
	}
}

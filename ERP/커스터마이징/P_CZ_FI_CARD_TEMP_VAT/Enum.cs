namespace cz
{
    internal class Enum
    {
        internal enum 부가세여부
        {
            NONE,
            포함,
            미포함,
        }

        internal enum 전표처리
        {
            미처리,
            처리,
        }

        internal enum 상대계정전표처리설정
        {
            계좌번호연결사용 = 1,
            상대계정처리유형사용 = 2,
            비용계정사용 = 3,
        }
    }
}

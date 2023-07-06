using System;

namespace sale
{
    public enum 메세지
    {
        이미수주확정되어수정삭제가불가능합니다
    }

    internal enum 특수단가적용
    {
        NONE, 중량단가, 조선호텔베이커리단가, 거래처별고정단가
    }
    
    internal enum 예상이익산출
    {
        NONE, 재고단가를원가로산출
    }

    internal enum DefaultSettings
    {
        회사코드, 영업그룹코드, 영업그룹명, 수주형태코드, 수주형태명, 화폐단위, 부가세포함, 계산서처리, MAIL_ADDR
    }
}
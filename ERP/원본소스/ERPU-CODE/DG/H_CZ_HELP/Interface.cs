using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace cz
{
    interface IHelp
    {
        DataTable SetDetail(string 검색);
        string Get검색 { get; }
    }
}

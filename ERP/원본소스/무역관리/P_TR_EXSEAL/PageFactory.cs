using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;


namespace trade
{
    public class PageFactory : IPageFactory
    {
        public PageLoadResult CreateInstance(PageLoadContext context)
        {
            PageLoadResult pr = new PageLoadResult();
            pr.PageInstance = new P_TR_EXSEAL_NEW();
            return pr;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;

namespace sale
{
    class PageFactory : IPageFactory
    {
        public PageLoadResult CreateInstance(PageLoadContext context)
        {
            PageLoadResult pr = new PageLoadResult();
            pr.PageInstance = new P_SA_SO_BATCH_VAT_NEW();
            return pr;
        }

    }
}

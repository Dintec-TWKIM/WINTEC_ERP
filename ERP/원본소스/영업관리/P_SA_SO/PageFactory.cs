using System;
using System.Collections.Generic;
using System.Text;
using Duzon.Common.Forms;

namespace sale
{
    public class PageFactory : IPageFactory
    {
        #region IPageFactory 멤버

        public PageLoadResult CreateInstance(PageLoadContext context)
        {
            PageLoadResult pr = new PageLoadResult();
            pr.PageInstance = new P_SA_SO_NEW();
            return pr;
        }

        #endregion
    }
}

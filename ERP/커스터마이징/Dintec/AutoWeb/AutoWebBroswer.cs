

namespace Dintec.AutoWeb
{
    public partial class AutoWebBroswer : Duzon.Common.Forms.CommonDialog
    {
        public DxWebBrowser WebBrowser
        {
            get
            {
                return web메인;
            }
            set
            {
                web메인 = value;
            }
        }

        #region ==================================================================================================== Constructor
        public AutoWebBroswer()
        {
            InitializeComponent();
        }

		#endregion

		
    }
}

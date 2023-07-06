using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;

namespace Dintec
{
    public partial class DxWebBrowser : WebBrowser
    {		
		[DllImport("wininet.dll", SetLastError = true)]
		public static extern bool InternetGetCookieEx(
		string url,
		string cookieName,
		StringBuilder cookieData,
		ref int size,
		Int32 dwFlags,
		IntPtr lpReserved);

		private const Int32 InternetCookieHttponly = 0x2000;

		/// <summary>
		/// Gets the URI cookie container.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns></returns>
		public static CookieContainer GetUriCookieContainer(Uri uri)
		{
			CookieContainer cookies = null;
			// Determine the size of the cookie
			int datasize = 8192 * 16;
			StringBuilder cookieData = new StringBuilder(datasize);
			if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
			{
				if (datasize < 0)
					return null;
				// Allocate stringbuilder large enough to hold the cookie
				cookieData = new StringBuilder(datasize);
				if (!InternetGetCookieEx(
					uri.ToString(),
					null, cookieData,
					ref datasize,
					InternetCookieHttponly,
					IntPtr.Zero))
					return null;
			}
			if (cookieData.Length > 0)
			{
				cookies = new CookieContainer();
				cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));

				string[] cookies2 = cookieData.ToString().Split(';');

			}
			return cookies;
		}

		[DllImport("wininet.dll", SetLastError = true)]
		public static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName, string lpszCookieData);



		bool webbrowserDocumentCompleted = false;

        public DxWebBrowser()
        {
            ScriptErrorsSuppressed = true;
            this.DocumentCompleted += (o, e) => { webbrowserDocumentCompleted = true; };
        }

        public bool IsExists(string id)
        {
            if (this.Document.GetElementById(id) == null)
                return false;
            else
                return true;
        }

		public string GetHtml()
		{
			string html = this.DocumentText;
			html = Regex.Replace(html, "<input[^>]*type=\"hidden\"[^>]*>", "", RegexOptions.Singleline);    // ViewState 삭제

			return html;
		}

		public string GetValue(string id)
        {
            string tagName = this.Document.GetElementById(id).TagName.ToLower();

            if (tagName == "a")
                return this.Document.GetElementById(id).GetAttribute("href");
            else
                return this.Document.GetElementById(id).GetAttribute("value");
        }

        public void SetValue(string id, string value)
        {
            this.Document.GetElementById(id).SetAttribute("value", value);
        }

        public void GoTo(string url)
        {
			webbrowserDocumentCompleted = false;

			this.Navigate(url);
           
            while (!webbrowserDocumentCompleted)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }

            webbrowserDocumentCompleted = false;
        }

        public new void Click(string id)
        {
            this.Document.GetElementById(id).InvokeMember("click");
           
            while (!webbrowserDocumentCompleted)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }

            webbrowserDocumentCompleted = false;
        }

        public void RunScript(string scriptName)
        {
            this.Document.InvokeScript(scriptName);

            while (this.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        }
	}
}

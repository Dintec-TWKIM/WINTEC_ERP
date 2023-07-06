using Duzon.Common.Forms;
using Duzon.ERPU;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace cz
{
    internal class MES_Login
    {
        private const int euckrCodepage = 51949;
        private P_CZ_PR_LINK_MES_REG_BIZ _biz = new P_CZ_PR_LINK_MES_REG_BIZ();
        private string apiurl = string.Empty;
        private string okurl = string.Empty;
        private string url = string.Empty;
        private string tokenurl = string.Empty;
        private string parameter = string.Empty;
        private string method = string.Empty;
        private Encoding encoding = (Encoding)null;
        private string paramKey = string.Empty;
        private string uid = string.Empty;
        private string uKey = string.Empty;

        public string ApiUrl { set; get; }

        public string OkUrl { set; get; }

        public string Url { set; get; }

        public string TokenUrl { set; get; }

        public string Method { set; get; }

        public Encoding Encoding { set; get; }

        public MES_Login(bool AlimiYn) => this.setConnectInfo(AlimiYn);

        public void setConnectInfo(bool AlimiYn)
        {
            try
            {
                string str = this._biz.SearchURL();
                this.Encoding = Encoding.GetEncoding(51949);
                this.Method = "POST";
                this.Url = str + "/api/CM/AuthKeyService/PreAuthKeys";
                this.TokenUrl = str + "/api/CM/SoftAuthenticationService/token";
                this.ApiUrl = str + "/api/ME/MeInterfaceService/WoconfInterface_list";
                this.OkUrl = str + "/api/ME/MeInterfaceService/WoconfInterface_update";
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
        }

        public string getTokenData()
        {
            string str = "";
            try
            {
                HttpWebRequest httpWebRequest1 = this.requestData(this.Url, "Auth");
                if (httpWebRequest1 != null)
                {
                    JObject jobject1 = JObject.Parse(this.responseData(httpWebRequest1));
                    if (!jobject1.GetValue("state").ToString().Equals("success"))
                        return "error";
                    JObject jobject2 = jobject1["data"] as JObject;
                    JToken jtoken1 = jobject2["uid"];
                    JToken jtoken2 = jobject2["key"];
                    this.uid = jtoken1.ToString();
                    this.uKey = jtoken2.ToString();
                    this.paramKey = MES_Login.Encrypt(MES_Login.Base64Encoding("MES:MES:123"), this.uKey);
                }
                this.parameter = string.Format("userid={0}&password={1}", (object)"mes10", (object)"mes10!@#");
                HttpWebRequest httpWebRequest2 = this.requestData(this.TokenUrl, "token");
                if (httpWebRequest2 != null)
                {
                    JObject jobject = JObject.Parse(this.responseData(httpWebRequest2));
                    if (!jobject.GetValue("state").ToString().Equals("success"))
                        return "error";
                    str = (jobject["data"] as JObject)["access_token"].ToString();
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                str = "error";
            }
            return str;
        }

        public bool getMesOrcvH(string DT_FROM, string DT_TO)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string str1 = string.Empty;
            string empty3 = string.Empty;
            string str2;
            try
            {
                string header = string.Format("X-Authenticate-Token: {0}", (object)this.getTokenData());
                this.parameter = string.Format("intl_sys_cd={0}&intl_his_sq_from={1}&intl_his_sq_to={2}&intl_dt_from={3}&intl_dt_to={4}&intl_st={5}", (object)"IU", (object)"", (object)"", (object)DT_FROM, (object)DT_TO, (object)"1");
                HttpWebRequest httpWebRequest = (HttpWebRequest)null;
                try
                {
                    httpWebRequest = WebRequest.Create(this.ApiUrl + "?" + this.parameter) as HttpWebRequest;
                    this.Encoding.GetBytes(this.parameter);
                    this.Method = "GET";
                    httpWebRequest.Headers.Add(header);
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.Method = this.Method;
                }
                catch (Exception ex)
                {
                    Global.MainFrame.MsgEnd(ex);
                }
                if (httpWebRequest != null)
                {
                    string json = this.responseData(httpWebRequest);
                    bool flag1 = false;
                    JObject jobject = JObject.Parse(json);
                    if (!jobject.GetValue("state").ToString().Equals("success"))
                    {
                        str2 = "error";
                        return false;
                    }
                    if (!jobject["data"].ToString().Equals("[]"))
                    {
                        JToken jtoken = jobject.Descendants().Where<JToken>((Func<JToken, bool>)(d => d is JArray)).First<JToken>();
                        bool flag2 = false;
                        foreach (JObject child in jtoken.Children<JObject>())
                        {
                            int num1 = 0;
                            object[] parameters = new object[29];
                            parameters[28] = (object)Global.MainFrame.LoginInfo.UserID;
                            string empty4 = string.Empty;
                            foreach (JProperty property in child.Properties())
                            {
                                if (!property.Name.Equals("WOCONFBD_INFO") && (!property.Name.Equals("UPDATE_POSS_YN") && !property.Name.Equals("INTL_ST") && (!property.Name.Equals("INTL_DTS") && !property.Name.Equals("INTL_PROC_DTS")) && (!property.Name.Equals("INTL_ERR_MSG_DC") && !property.Name.Equals("INSERT_ID") && (!property.Name.Equals("INSERT_IP") && !property.Name.Equals("INSERT_MCADDR_NM"))) && (!property.Name.Equals("INSERT_DTS") && !property.Name.Equals("UPDATE_ID") && (!property.Name.Equals("UPDATE_IP") && !property.Name.Equals("UPDATE_MCADDR_NM"))) && !property.Name.Equals("UPDATE_DTS")))
                                {
                                    if (property.Name.Equals("INTL_HIS_SQ") || property.Name.Equals("INTL_DOC_SQ") || (property.Name.Equals("PRWK_SQ") || property.Name.Equals("PROD_SQ")) || property.Name.Equals("WRK_QT") || property.Name.Equals("BAD_QT"))
                                    {
                                        parameters[num1++] = (object)D.GetDecimal((object)property.Value);
                                        if (property.Name.Equals("INTL_HIS_SQ"))
                                            empty4 = D.GetString((object)property.Value);
                                    }
                                    else
                                        parameters[num1++] = (object)D.GetString((object)property.Value);
                                    if (property.Name.Equals("CRUD_FG") && D.GetString((object)property.Value) == "D")
                                        flag2 = true;
                                }
                            }
                            try
                            {
                                if (DBHelper.ExecuteNonQuery("UP_PR_LINK_MES10_I", parameters))
                                {
                                    str2 = this.sendUpdateApi("{ \"INTL_SYS_CD\" : \"IU\", \"INTL_HIS_SQ\" : \"" + empty4 + "\", \"INTL_ST\" : \"2\" , \"INTL_ERR_MSG_DC\" : null }");
                                    if (flag2)
                                    {
                                        int num2 = (int)Global.MainFrame.ShowMessage("연동 삭제 완료");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                str2 = this.sendUpdateApi("{ \"INTL_SYS_CD\" : \"IU\", \"INTL_HIS_SQ\" : \"" + empty4 + "\", \"INTL_ST\" : \"3\" , \"INTL_ERR_MSG_DC\" : \"" + ex.ToString() + "\" }");
                                flag1 = true;
                                int num2 = (int)Global.MainFrame.ShowMessage("연동 에러 : " + ex.ToString());
                                return false;
                            }
                        }
                        str1 = flag1 ? "error" : "success";
                    }
                    else
                    {
                        int num = (int)Global.MainFrame.ShowMessage("가져올 데이터가 없습니다.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
                str2 = "error";
                return false;
            }
            return str1 == "success";
        }

        private HttpWebRequest requestData(string strUrl, string callType)
        {
            HttpWebRequest httpWebRequest1 = (HttpWebRequest)null;
            try
            {
                HttpWebRequest httpWebRequest2 = WebRequest.Create(strUrl) as HttpWebRequest;
                byte[] bytes = this.Encoding.GetBytes(this.parameter);
                if (callType.Equals("token"))
                {
                    this.Method = "POST";
                    httpWebRequest2.Headers.Add("Authorization", "Basic " + this.paramKey + "." + this.uid);
                    httpWebRequest2.ContentLength = (long)bytes.Length;
                }
                else
                    this.Method = "GET";
                httpWebRequest2.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest2.Method = this.Method;
                if (this.Method.Equals("POST"))
                {
                    Stream requestStream = httpWebRequest2.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                return httpWebRequest2;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            return httpWebRequest1;
        }

        private HttpWebRequest requestDataForUpadte(
          string strUrl,
          string strToken,
          string strParam)
        {
            HttpWebRequest httpWebRequest1 = (HttpWebRequest)null;
            try
            {
                HttpWebRequest httpWebRequest2 = WebRequest.Create(strUrl) as HttpWebRequest;
                byte[] bytes = this.Encoding.GetBytes(strParam);
                this.Method = "POST";
                httpWebRequest2.Headers.Add(strToken);
                httpWebRequest2.ContentLength = (long)bytes.Length;
                httpWebRequest2.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest2.Method = this.Method;
                Stream requestStream = httpWebRequest2.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                return httpWebRequest2;
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            return httpWebRequest1;
        }

        private string responseData(HttpWebRequest httpWebRequest)
        {
            string str = "";
            try
            {
                str = this.responseDataStr(httpWebRequest);
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            return str;
        }

        private string responseDataStr(HttpWebRequest httpWebRequest)
        {
            string str = string.Empty;
            try
            {
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                str = streamReader.ReadToEnd();
                if (str != null)
                    str = str.Trim();
                streamReader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Global.MainFrame.MsgEnd(ex);
            }
            return str;
        }

        public static string Encrypt(string data, string publickKey)
        {
            IAsymmetricBlockCipher asymmetricBlockCipher = (IAsymmetricBlockCipher)new Pkcs1Encoding((IAsymmetricBlockCipher)new RsaEngine());
            AsymmetricKeyParameter publicKeyParameter = MES_Login.GetPUblicKeyParameter(publickKey);
            asymmetricBlockCipher.Init(true, (ICipherParameters)publicKeyParameter);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(asymmetricBlockCipher.ProcessBlock(bytes, 0, bytes.Length));
        }

        public static AsymmetricKeyParameter GetPUblicKeyParameter(
          string pubkeyStr)
        {
            return PublicKeyFactory.CreateKey(Convert.FromBase64String(pubkeyStr));
        }

        public static string Base64Encoding(string EncodingText, Encoding oEncoding = null)
        {
            if (oEncoding == null)
                oEncoding = Encoding.UTF8;
            return Convert.ToBase64String(oEncoding.GetBytes(EncodingText));
        }

        private string sendUpdateApi(string strParam)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string empty3 = string.Empty;
            string empty4 = string.Empty;
            string str1 = string.Empty;
            string str2;
            try
            {
                string strToken = string.Format("X-Authenticate-Token: {0}", (object)this.getTokenData());
                this.parameter = string.Format("list=[{0}]", (object)strParam);
                HttpWebRequest httpWebRequest = this.requestDataForUpadte(this.OkUrl, strToken, this.parameter);
                if (httpWebRequest != null && !JObject.Parse(this.responseData(httpWebRequest)).GetValue("state").ToString().Equals("success"))
                    str1 = "error";
                str2 = "success";
            }
            catch (Exception ex)
            {
                str2 = "error";
            }
            return str2;
        }
    }
}

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Data;
using TLGX_Consumer.Models;
using TLGX_Consumer.MDMSVC;
using System.Net;

namespace TLGX_Consumer.Controller.ServiceConnection
{
    public static class MDMSvcProxy
    {
        //string _MDMSvcUrl;

        public static string MDMSvcUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["MDMSVCUrl"];
            }
        }

        public static bool GetData(string uri, Type ResponseType, out object ReturnValue)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(MDMSvcUrl + uri);

                request.Headers.Add("CallingAgent", "MDM");
                if(!string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    request.Headers.Add("CallingUser", System.Web.HttpContext.Current.User.Identity.Name);
                }
                else
                {
                    request.Headers.Add("CallingUser", "MDM_USER");
                }
                
                request.KeepAlive = false;
                request.Timeout = System.Threading.Timeout.Infinite;
                request.ReadWriteTimeout = System.Threading.Timeout.Infinite;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    DataContractJsonSerializer obj = new DataContractJsonSerializer(ResponseType);
                    ReturnValue = obj.ReadObject(stream);
                    obj = null;
                    stream = null;
                    response.Dispose();
                    request = null;
                    return true;
                }
                else
                {
                    response.Dispose();
                    request = null;
                    ReturnValue = null;
                    return false;
                }
            }
            catch (Exception e)
            {
                ReturnValue = null;
                return false;
            }

        }

        public static bool PostData(string URI, object Param, Type RequestType, Type ResponseType, out object ReturnValue)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(MDMSvcUrl + URI);

                request.Headers.Add("CallingAgent", "MDM");
                if (!string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    request.Headers.Add("CallingUser", System.Web.HttpContext.Current.User.Identity.Name);
                }
                else
                {
                    request.Headers.Add("CallingUser", string.Empty);
                }

                request.Method = "POST";
                request.ContentType = "application/json";
                request.KeepAlive = false;
                request.Timeout = System.Threading.Timeout.Infinite;
                request.ReadWriteTimeout = System.Threading.Timeout.Infinite;
                DataContractJsonSerializer serializerToUpload = new DataContractJsonSerializer(RequestType);

                using (var memoryStream = new MemoryStream())
                {
                    using (var reader = new StreamReader(memoryStream))
                    {
                        serializerToUpload.WriteObject(memoryStream, Param);
                        memoryStream.Position = 0;
                        string body = reader.ReadToEnd();

                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(body);
                        }
                    }
                }

                var response = request.GetResponse();

                if (((System.Net.HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
                {
                    ReturnValue = null;
                }
                else
                {
                    var stream = response.GetResponseStream();

                    var obj = new DataContractJsonSerializer(ResponseType);
                    ReturnValue = obj.ReadObject(stream);

                    obj = null;
                    stream = null;
                }

                serializerToUpload = null;

                response.Dispose();
                response = null;
                request = null;

                if (ReturnValue != null)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                ReturnValue = null;
                return false;
            }

        }
        
    }


}
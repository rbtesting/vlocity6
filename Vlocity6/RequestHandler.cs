using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Vlocity6
{
  class RequestHandler
    {
        //public static Result FormatAndSendInput(
        //    string hostname,
        //    string port,
        //    string targetRequest,
        //    object requestData)
        //{
        //    string response;
        //    Result objResponse;
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    string result = jss.Serialize(requestData);
        //    response = Send(
        //        "https://" + hostname + ":" + port + targetRequest,
        //        result
        //        );
        //    objResponse = jss.Deserialize<Result>(response);
        //    return objResponse;
        //}

        public static object FormatAndSend(
            string hostname,
            string port,
            string targetRequest,
            object requestData)
        {
            string response;
            object objResponse; 
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string result = jss.Serialize(requestData);
            response = Send(
                "https://" + hostname + ":" + port + targetRequest,
                result
                );
            objResponse = jss.Deserialize<object>(response);
            return objResponse;
        }

        public static string Send(string URL, string request)
        {
            // next line accepts all certificates (unsecure, but we don't use a valid SSL cert)
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                ((sender, certificate, chain, sslPolicyErrors) => true);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["X-Requested-With"] = "XMLHttpRequest";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(request);
                streamWriter.Flush();
                streamWriter.Close();
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

    //    public class Result
    //    { 
    //        public int Error;
    //        public string Reason;
    //    }
    //}
}

}


   
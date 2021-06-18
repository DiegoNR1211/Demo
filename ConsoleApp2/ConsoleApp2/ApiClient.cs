using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;

namespace ConsoleApp2
{
    public class APIClient
    {
        public class ContentType
        {
            public const String JSON = "application/json";
            public const String FORM = "application/x-www-form-urlencoded";
        }

        public class RequestTypeHttp
        {
            public const String GET = "GET";
            public const String POST = "POST";
            public const String DELETE = "DELETE";
        }

        public static JObject Request(String URL, String ContentType, String Authorization, JObject Parameters, String RequestType)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = RequestType;
            request.ContentType = ContentType;

            if (Authorization != null)
                request.Headers.Add(HttpRequestHeader.Authorization, Authorization);

            if (Parameters != null)
            {
                StreamWriter objStreamWriter = new StreamWriter(request.GetRequestStream());
                objStreamWriter.Write(Parameters.ToString());
                objStreamWriter.Flush();
                objStreamWriter.Close();
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader objStreamReader = new StreamReader(response.GetResponseStream());
                string responseString = objStreamReader.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(responseString))
                {
                    JObject jResult = JObject.Parse(responseString);
                    return jResult;
                }
                else
                    return new JObject();
            }
            catch (WebException ex)
            {
                StreamReader objStreamReader = new StreamReader(ex.Response.GetResponseStream());
                JObject jResult = JObject.Parse(objStreamReader.ReadToEnd());
                return jResult;
            }
        }
    }
}

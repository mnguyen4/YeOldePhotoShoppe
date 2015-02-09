using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using yops_lib.Constants;

namespace yops_lib.Utils
{
    /*
     * This is a utility class to easily send REST requests.
     */
    public class RestUtil
    {
        private string url;

        /*
         * Constructor sets the url.
         */
        public RestUtil(string url)
        {
            this.url = url;
        }

        /*
         * Sends a JSON post data as a POST request.
         */
        public JObject sendPostRequest(JObject postData)
        {
            // converting the json to bytes
            string jsonStr = postData.ToString();
            byte[] data = Encoding.UTF8.GetBytes(jsonStr);
            int length = data.Length;

            // creating the web request
            WebRequest postReq = WebRequest.Create(@url);
            postReq.Method = SharedConstants.POST;
            postReq.ContentType = SharedConstants.APPLICATION_JSON;
            postReq.ContentLength = length;

            // send post data
            using (Stream stream = postReq.GetRequestStream())
            {
                stream.Write(data, 0, length);
            }

            // read response
            HttpWebResponse resp = postReq.GetResponse() as HttpWebResponse;
            JObject response;
            using (Stream stream = resp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                response = JObject.Parse(reader.ReadToEnd());
            }

            return response;
        }
    }
}

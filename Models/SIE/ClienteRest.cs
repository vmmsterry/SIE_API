using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Configuration;

namespace MVC_WebSite.Models.SIE
{
    public class ClienteRest
    {
        public Serie serie = null;
        public string error = string.Empty;

        private static string msjError = string.Empty;
        private static ClienteRest instance = null;
        public static ClienteRest Instance
        {
            get
            {
                if (instance == null)
                    instance = new ClienteRest();

                return instance;
            }
            
        }

        protected ClienteRest()
        {
            Response response = ReadSerie(false);
            if (response != null)
            {
                serie = response.seriesResponse.series[0];

                response = ReadSerie(true);
                serie.Data = response.seriesResponse.series[0].Data;
            }
            else
                error = msjError;
        }
        
        private static Response ReadSerie(bool banderaDatos)
        {
            try
            {
                string IDs = WebConfigurationManager.AppSettings["IDs_SIE"];
                string url = string.Format("https://www.banxico.org.mx/SieAPIRest/service/v1/series/{0}{1}", IDs, banderaDatos ? "/datos" : string.Empty);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Accept = "application/json";
                request.Headers["Bmx-Token"] = WebConfigurationManager.AppSettings["Token"];
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode != HttpStatusCode.OK)
                    msjError = string.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription);
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                Response jsonResponse = objResponse as Response;
                return jsonResponse;
            }
            catch (Exception e)
            {
                msjError = e.Message;
            }
            return null;
        }

    }
}
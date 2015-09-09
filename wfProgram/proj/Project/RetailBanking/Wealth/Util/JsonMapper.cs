using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WF.EAI.Web.UCA.RetailBanking.Wealth.Util
{
    public static class JsonMapper<T>
    {
        /// <summary>
        /// ObjectToText
        /// </summary>
        /// <param name="givenObject"></param>
        /// <param name="typeHandling"></param>
        /// <returns></returns>
        public static string ObjectToText(T givenObject, bool typeHandling = true)
        {
            if (typeHandling)
            {
                return JsonConvert.SerializeObject(givenObject, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });
            }
            else
            {
                return JsonConvert.SerializeObject(givenObject, Formatting.None);
            }
        }

        /// <summary>
        /// TextToObject
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T TextToObject(string jsonText)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(jsonText, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        }
    }

}
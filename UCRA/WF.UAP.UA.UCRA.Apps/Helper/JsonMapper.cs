// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonMapper.cs" company="">
//   
// </copyright>
// <summary>
//   The json mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.UAP.UA.UCRA.Apps.Helper
{
    #region

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// The json mapper.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public static class JsonMapper<T>
    {
        /// <summary>
        /// ObjectToText
        /// </summary>
        /// <param name="givenObject">
        /// </param>
        /// <param name="typeHandling">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ObjectToText(T givenObject, bool typeHandling = true)
        {
            if (typeHandling)
            {
                return JsonConvert.SerializeObject(
                    givenObject, 
                    Formatting.None, 
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
            }

            return JsonConvert.SerializeObject(givenObject, Formatting.None);
        }

        /// <summary>
        /// TextToObject
        /// </summary>
        /// <param name="jsonText">
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T TextToObject(string jsonText)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(
                jsonText, 
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
        }
    }
}
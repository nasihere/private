// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RouteToStateParser.cs" company="">
//   
// </copyright>
// <summary>
//   The route to state parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.CUSP.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    using WF.EAI.Entities.domain.c2c;

    /// <summary>
    ///     The route to state parser.
    /// </summary>
    public class RouteToStateParser
    {
        #region Constants

        /// <summary>
        ///     The cod e_ path.
        /// </summary>
        private const string CodePath = "/ACAPS01/Body/Data/LookUp/data";

        /// <summary>
        ///     The fiel d_ xpath.
        /// </summary>
        private const string FieldXpath = "/ACAPS01/Body/Data/LookUp";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public static List<RouteToState> Parse(string response)
        {
            var namevalList = new List<RouteToState>();
            var lookupValue = string.Empty;
            var currentXPath = string.Empty;
            var theValue = string.Empty;

            using (var xmlReader = XmlReader.Create(new StringReader(response)))
            {
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            currentXPath += "/" + xmlReader.Name;
                            if (string.Compare(currentXPath, FieldXpath, true) == 0)
                            {
                                namevalList.Add(new RouteToState { Name = string.Empty, Value = string.Empty });
                            }
                            else if (string.Compare(currentXPath, CodePath, true) == 0)
                            {
                                lookupValue = xmlReader.GetAttribute("name").Trim();
                            }

                            if (xmlReader.IsEmptyElement)
                            {
                                currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                                theValue = string.Empty;
                            }

                            break;
                        case XmlNodeType.Text:
                            theValue = xmlReader.Value.Trim('_');
                            break;
                        case XmlNodeType.CDATA:
                            theValue = xmlReader.Value.Trim('_');
                            if (string.Compare(currentXPath, CodePath, true) == 0)
                            {
                                namevalList.Add(
                                    new RouteToState { Name = lookupValue + " - " + theValue, Value = lookupValue });
                                lookupValue = string.Empty;
                            }

                            break;
                        case XmlNodeType.EndElement:
                            currentXPath = currentXPath.Substring(0, currentXPath.LastIndexOf("/"));
                            theValue = string.Empty;
                            break;
                    }
                }
            }

            return namevalList;
        }

        #endregion
    }
}
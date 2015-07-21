// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InquiryFields.cs" company="">
//   
// </copyright>
// <summary>
//   The inquiry fields.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.CUSP.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Xml;

    using WellsFargo.EAI.SIF.Services.CWS.MaskInq.MaskInqRequest;

    /// <summary>
    ///     The inquiry fields.
    /// </summary>
    public class InquiryFields
    {
        #region Static Fields

        /// <summary>
        ///     The builder fields.
        /// </summary>
        public static Dictionary<string, List<ACAPS01BodyDataField>> BuilderFields;

        /// <summary>
        ///     The sync root.
        /// </summary>
        private static readonly object SyncRoot = new object();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="InquiryFields" /> class.
        /// </summary>
        static InquiryFields()
        {
            if (BuilderFields == null)
            {
                lock (SyncRoot)
                {
                    if (BuilderFields == null)
                    {
                        try
                        {
                            BuilderFields = new Dictionary<string, List<ACAPS01BodyDataField>>();
                            var xmlFolderPath = ConfigurationManager.AppSettings["InquiryXmlFiles"];
                            var dir = new DirectoryInfo(xmlFolderPath);
                            var viewName = string.Empty;
                            var inquiryFields = new List<ACAPS01BodyDataField>();

                            foreach (var fileInfo in dir.GetFiles())
                            {
                                using (var reader = XmlReader.Create(fileInfo.FullName))
                                {
                                    while (reader.Read())
                                    {
                                        switch (reader.NodeType)
                                        {
                                            case XmlNodeType.Element:
                                                switch (reader.Name.ToUpper())
                                                {
                                                    case "ACAPSVIEW":
                                                        viewName = reader.GetAttribute("viewName");
                                                        inquiryFields = new List<ACAPS01BodyDataField>();
                                                        break;
                                                    case "ACAPSFIELD":
                                                        inquiryFields.Add(
                                                            new ACAPS01BodyDataField(reader.GetAttribute("name")));
                                                        break;
                                                }

                                                break;
                                        }
                                    }
                                }

                                BuilderFields.Add(viewName, inquiryFields);
                            }
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ACAPSErrorMessages.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for ACAPSErrorMessages.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services
{
    using System.Collections;
    using Microsoft.ApplicationBlocks.ConfigurationManagement;
    using System.IO;
    using System.Text;
    using System.Xml;
    using WF.UAP.UASF.CrossCutting.ConfigMgmt.config.Global;


    /// <summary>
    ///     Summary description for ACAPSErrorMessages.
    /// </summary>
    public class ACAPSErrorMessages
    {
        #region Static Fields

        /// <summary>
        /// The error list.
        /// </summary>
        public static ArrayList errorList = null;

        #endregion

        #region Fields

       

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The instantiate.
        /// </summary>
        public static void Instantiate()
        {
            GlobalConfig Global = (GlobalConfig)ConfigurationManager.Read("Global");
            string filename = Global.CAS.AcapsErrorList;

            if (File.Exists(filename))
            {
                XmlTextReader tr = new XmlTextReader(filename);
                errorList = new ArrayList();
                while (tr.Read())
                {
                    string errorCode = tr.GetAttribute("errorCode");
                    string type = tr.GetAttribute("type");
                    string alertCode = tr.GetAttribute("alertCode");
                    string errorValue = tr.GetAttribute("errorValue");
                    if (errorCode != null && errorCode != string.Empty)
                    {
                        ErrorItem ei = new ErrorItem();
                        ei.errorCode = errorCode;
                        if (type != null && type != string.Empty)
                        {
                            ei.type = type;
                        }

                        if (alertCode != null && alertCode != string.Empty)
                        {
                            ei.alertCode = alertCode;
                        }

                        if (errorValue != null && errorValue != string.Empty)
                        {
                            ei.errorValue = errorValue;
                        }

                        errorList.Add(ei);
                    }
                }
            }
        }

        /// <summary>
        /// The retrieve error messages.
        /// </summary>
        /// <param name="codes">
        /// The codes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string retrieveErrorMessages(ArrayList codes)
        {
            if (codes == null)
            {
                return "An unknown error has occurred.";
            }

            // set a default error message
            string errorMsg = string.Empty;
            IEnumerator myEnumerator = null;
            if (errorList == null)
            {
                Instantiate();
            }

            myEnumerator = errorList.GetEnumerator();
            for (int i = 0; codes != null && i < codes.Count; i++)
            {
                while (myEnumerator.MoveNext())
                {
                    ErrorItem ei = (ErrorItem)myEnumerator.Current;
                    if (ei.errorCode == (string)codes[i])
                    {
                        errorMsg = errorMsg + ei.errorValue + "<br/>";
                        break;
                    }
                }
            }

            if (errorMsg == string.Empty)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(
                    "Application unavailable for update on CAS - please contact the Loan Officer. Error codes: ");
                string seperator = string.Empty;
                foreach (string code in codes)
                {
                    builder.Append(seperator);
                    builder.Append(code);
                    seperator = ", ";
                }

                builder.Append("<br/>");

                errorMsg = builder.ToString();
            }

            return errorMsg;
        }

        #endregion

        /// <summary>
        /// The error item.
        /// </summary>
        public class ErrorItem
        {
            #region Fields

            /// <summary>
            /// The alert code.
            /// </summary>
            public string alertCode;

            /// <summary>
            /// The error code.
            /// </summary>
            public string errorCode;

            /// <summary>
            /// The error value.
            /// </summary>
            public string errorValue;

            /// <summary>
            /// The type.
            /// </summary>
            public string type;

            #endregion
        }
    }
}
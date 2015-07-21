// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AJAXFactory.cs" company="">
//   
// </copyright>
// <summary>
//   The AJAXFactory returns the implementation class based on the action.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services
{
    using WF.EAI.BLL.cas.Print;
    using WF.EAI.BLL.cas.Services.CWS;

    /// <summary>
    ///     The AJAXFactory returns the implementation class based on the action.
    /// </summary>
    public class AJAXFactory
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get ajax helper.
        /// </summary>
        /// <param name="reqXMLString">
        /// The req xml string.
        /// </param>
        /// <returns>
        /// The <see cref="IAJAXHelper"/>.
        /// </returns>
        public static IAJAXHelper getAJAXHelper(string reqXMLString)
        {
            IAJAXHelper ajaxHelper = null;
            string startTag = "<action>";
            string endTag = "</action>";
            int startIndex = reqXMLString.IndexOf(startTag) + startTag.Length;
            int endIndex = reqXMLString.IndexOf(endTag);
            string action = reqXMLString.Substring(startIndex, endIndex - startIndex);

            // *******************
            // these values are hard coded to eliminate late binding issues. 
            // *******************
            switch (action)
            {
                case "BankersNoteUPDATE":
                    ajaxHelper = new BankNoteReadHelper();
                    break;
                case "PrintEarlyDisclosures":
                    ajaxHelper = new DisclosureHelper();
                    break;
                case "TasksUPDATE":
                    ajaxHelper = new TaskClearHelper();
                    break;
                case "ClosingConditionsUPDATE":
                    ajaxHelper = new ClosingConditionsHelper();
                    break;
                default:
                    ajaxHelper = null;
                    break;
            }

            return ajaxHelper;
        }

        #endregion
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAJAXHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IAJAXHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WF.EAI.BLL.cas.Services
{
    using System.Xml;

    using WF.EAI.Data.config.sif;

    /** 	
	*	Author: Tsailing Wong 	
	*/

    /// <summary>
    /// </summary>
    public interface IAJAXHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The receive message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string receiveMessage(XmlDocument msg);

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="config">
        /// The config.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string sendMessage(XmlDocument msg, EAISIFServiceBaseConfig config);

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string sendMessage(XmlDocument msg);

        #endregion
    }
}
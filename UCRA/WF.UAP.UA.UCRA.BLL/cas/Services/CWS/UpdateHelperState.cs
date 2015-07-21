namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;

    /// <summary>
    /// The update helper state.
    /// </summary>
    [Serializable]
    public enum UpdateHelperState
    {
        /// <summary>
        /// The initialize.
        /// </summary>
        Initialize = 0, 

        /// <summary>
        /// The inquiry begin.
        /// </summary>
        InquiryBegin = 1, 

        /// <summary>
        /// The inquiry end.
        /// </summary>
        InquiryEnd = 2, 

        /// <summary>
        /// The update begin.
        /// </summary>
        UpdateBegin = 3, 

        /// <summary>
        /// The update end.
        /// </summary>
        UpdateEnd = 4
    }
}
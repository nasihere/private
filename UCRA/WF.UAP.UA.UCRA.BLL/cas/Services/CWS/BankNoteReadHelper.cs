// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankerNoteReadHelper.cs" company="">
//   
// </copyright>
// <summary>
//   Summary description for BankerNoteReadHelper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Services.CWS
{
    using System;
    using System.Collections;
    using System.Web;
    using System.Xml;

    using WF.EAI.Data.sif.Services.CWS.BankerNotes;
    using WF.EAI.Data.sif.Services.CWS.BankerNotes.BankerNoteSubmit;

    using WellsFargo.EAI.SIF.ServiceProxy.com.wellsfargo.service.provider.helper.BankerNotes;

    using WF.EAI.BLL.cas.Invokers;
    using WF.EAI.Data.config.sif;
    //using WF.EAI.Data.sif.Services.System2.PerformBankerNoteActions;
using WF.UAP.UASF.CrossCutting.Logging;

    /// <summary>
    ///     Summary description for BankerNoteReadHelper.
    /// </summary>
    public class BankNoteReadHelper : IAJAXHelper
    {
        #region Fields

        /// <summary>
        /// The _banker note send.
        /// </summary>
        private Stack _bankerNoteSend;

        /// <summary>
        /// The id.
        /// </summary>
        private ulong id = 0;

        /// <summary>
        /// The session_id.
        /// </summary>
        private string session_id = string.Empty;

        /// <summary>
        /// The tran_id.
        /// </summary>
        private string tran_id = string.Empty;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BankNoteReadHelper"/> class.
        /// </summary>
        public BankNoteReadHelper()
        {
            this._bankerNoteSend = new Stack();
        }

        #endregion

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
        public string receiveMessage(XmlDocument msg)
        {
            return null;
        }

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string sendMessage(XmlDocument msg)
        {
            string applicationId =
                msg.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/applicationID") != null
                    ? msg.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/applicationID")
                         .InnerText.Trim()
                    : string.Empty;
            string requestorLoc =
                msg.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorLoc") != null
                    ? msg.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorLoc")
                         .InnerText.Trim()
                    : string.Empty;
            string requestorSalesID =
                msg.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorSalesID") != null
                    ? msg.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorSalesID")
                         .InnerText.Trim()
                    : string.Empty;
            string requestorUserID =
                msg.SelectSingleNode(
                    "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorID") != null
                    ? msg.SelectSingleNode(
                        "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgControlHeader/requestorID")
                         .InnerText.Trim()
                    : string.Empty;

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                this.session_id = HttpContext.Current.Session.SessionID;
            }

            XmlNodeList nodes =
                msg.SelectNodes(
                    "/AJAX/Body/serviceRequest/data/WellsFargoUFEDS/msgBody/msgResponse/bankerNoteList/cbnt_out_all_dtl");

            if (applicationId.Trim().Length == 11 || applicationId.Trim().Length == 12)
            {
             Logger.Instance.Info("Call not made to decommissioned System2 service.");
                //P0501837-3_CFS-Auto-Sys2-Decommission
            //    // WFAF Auto finance call
            //    ArrayList arrUnreadMsg = new ArrayList();
            //    BankerNoteHelper bankerNotesHelper = new BankerNoteHelper();

            //    if (nodes != null)
            //    {
            //        foreach (XmlNode node in nodes)
            //        {
            //            string _sourceId = node.SelectSingleNode("cbnt_out_source_id") != null
            //                                   ? node.SelectSingleNode("cbnt_out_source_id").InnerText.Trim()
            //                                   : "S";
            //            string _targetId = node.SelectSingleNode("cbnt_out_bnote_target_id") != null
            //                                   ? node.SelectSingleNode("cbnt_out_bnote_target_id").InnerText.Trim()
            //                                   : "S";

            //            // string _text = node.SelectSingleNode("cbnt_out_text_line1") != null ? node.SelectSingleNode("cbnt_out_text_line1").InnerText.Trim() : String.Empty;
            //            string _text = node.SelectSingleNode("cbnt_out_text") != null
            //                               ? node.SelectSingleNode("cbnt_out_text").InnerText.Trim()
            //                               : string.Empty;
            //            string _type = node.SelectSingleNode("cbnt_out_type") != null
            //                               ? node.SelectSingleNode("cbnt_out_type").InnerText.Trim()
            //                               : string.Empty;
            //            string _readIndicator = node.SelectSingleNode("cbnt_out_status") != null
            //                                        ? node.SelectSingleNode("cbnt_out_status").InnerText.Trim()
            //                                        : string.Empty;

            //            // selects 'unread' messages
            //            if (_sourceId == "S" && _readIndicator != "READ")
            //            {
            //                string seqNum = node.SelectSingleNode("cbnt_out_seq_num_resp") != null
            //                                    ? node.SelectSingleNode("cbnt_out_seq_num_resp").InnerText.Trim()
            //                                    : string.Empty;

            //                // CWSBNT -- Get/Save Banker Message
            //                // Populate Message into array of banker Note 
            //                PerformBankerNoteActionsReq.performBankerNoteActionsRequest_TypeDataCbnt_in_bnote
            //                    bankerNoteActionsRequest =
            //                        new PerformBankerNoteActionsReq.
            //                            performBankerNoteActionsRequest_TypeDataCbnt_in_bnote();

            //                PerformBankerNoteActionsReq.
            //                    performBankerNoteActionsRequest_TypeDataCbnt_in_bnoteCbnt_in_bnote_text bnote_text =
            //                        new PerformBankerNoteActionsReq.
            //                            performBankerNoteActionsRequest_TypeDataCbnt_in_bnoteCbnt_in_bnote_text();

            //                bnote_text.cbnt_in_text_line1 = _text;

            //                bankerNoteActionsRequest.cbnt_in_bnote_target_id = _targetId.Trim().Length > 0
            //                                                                       ? _targetId
            //                                                                       : "S";
            //                bankerNoteActionsRequest.cbnt_in_bnote_text = bnote_text;
            //                bankerNoteActionsRequest.cbnt_in_bnote_type = _type;
            //                bankerNoteActionsRequest.cbnt_in_bnote_writer_userid = requestorUserID;
            //                bankerNoteActionsRequest.cbnt_in_source_id = _sourceId;
            //                bankerNoteActionsRequest.cbnt_in_seq_num_rqst = seqNum;

            //                // Add bankerNoteActionsRequest to ArrayList
            //                arrUnreadMsg.Add(bankerNoteActionsRequest);
            //            }
            //        }

            //        PerformBankerNoteActionsReq.performBankerNoteActionsRequest_TypeDataCbnt_in_bnote[] arrCbnt_in_bnote;
            //        if (arrUnreadMsg.Count > 0)
            //        {
            //            arrCbnt_in_bnote =
            //                (PerformBankerNoteActionsReq.performBankerNoteActionsRequest_TypeDataCbnt_in_bnote[])
            //                arrUnreadMsg.ToArray(
            //                    typeof(PerformBankerNoteActionsReq.performBankerNoteActionsRequest_TypeDataCbnt_in_bnote
            //                    ));
            //        }
            //        else
            //        {
            //            arrCbnt_in_bnote = null;
            //        }

            //        PerformBankerNoteActionsRes performBankerNoteActionsRes = null;

            //        // Update Messages and mark as Read
            //        performBankerNoteActionsRes = Invoker.performBankerNoteActionsReq(
            //            string.Empty, 
            //            "BNU", 
            //            string.Empty, 
            //            applicationId, 
            //            requestorLoc, 
            //            requestorUserID, 
            //            requestorSalesID, 
            //            arrCbnt_in_bnote, 
            //            "PerformBankerNoteActions", 
            //            "CAS", 
            //            this.session_id);

            //        // Retrieve all Messages                    
            //        performBankerNoteActionsRes = Invoker.performBankerNoteActionsReq(
            //            string.Empty, 
            //            "BNU", 
            //            string.Empty, 
            //            applicationId, 
            //            requestorLoc, 
            //            requestorUserID, 
            //            requestorSalesID, 
            //            null, 
            //            "PerformBankerNoteActions", 
            //            "CAS", 
            //            this.session_id);

            //        if (performBankerNoteActionsRes != null && performBankerNoteActionsRes.bankerNoteHelper != null)
            //        {
            //            bankerNotesHelper = performBankerNoteActionsRes.bankerNoteHelper;
            //        }

            //        if (bankerNotesHelper != null && // bankerNotesHelper.IsOK() &&
            //            bankerNotesHelper.IsOK && bankerNotesHelper.detail != null
            //            && bankerNotesHelper.detail.bankerNoteList != null
            //            && bankerNotesHelper.detail.bankerNoteList.Length > 0)
            //        {
            //            return bankerNotesHelper.bankerNoteListXmlDoc.Replace("\\\"", "\"");
            //        }
            //    }
            }
            else
            {
                // ACAPS call
                BankerNoteSubmit _responseNote = null;
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        string _sourceId = node.SelectSingleNode("cbnt_out_source_id") != null
                                               ? node.SelectSingleNode("cbnt_out_source_id").InnerText.Trim()
                                               : string.Empty;
                        string _readIndicator = node.SelectSingleNode("cbnt_out_status") != null
                                                    ? node.SelectSingleNode("cbnt_out_status").InnerText.Trim()
                                                    : string.Empty;

                        // selects 'unread' messages
                        if (_sourceId == "A" && _readIndicator != "READ")
                        {
                            string seqNum = node.SelectSingleNode("cbnt_out_seq_num_resp") != null
                                                ? node.SelectSingleNode("cbnt_out_seq_num_resp").InnerText.Trim()
                                                : string.Empty;
                            BankerNoteSubmitRes bankerNoteSubmitRes = Invoker.GetBNS(
                                applicationId, 
                                requestorUserID, 
                                requestorSalesID, 
                                requestorLoc, 
                                requestorLoc, 
                                string.Empty, 
                                "INFO", 
                                "A", 
                                "A", 
                                string.Empty, 
                                seqNum, 
                                this.session_id);
                            BankerNoteSubmit bankerNoteSubmit = bankerNoteSubmitRes.BankerNoteSubmit;
                            this._bankerNoteSend.Push(bankerNoteSubmit);
                        }
                    }
                }

                while (this._bankerNoteSend != null && this._bankerNoteSend.Count > 0
                       && this._bankerNoteSend.Peek() != null)
                {
                    _responseNote = (BankerNoteSubmit)this._bankerNoteSend.Pop();
                }

                // wait for the last of our updates to complete
                if (_responseNote != null)
                {
                    _responseNote.IsOK = true;
                }

                // CWSBNT new
                BankerNoteHelper bankerNotesHelper = new BankerNoteHelper();

                BankerNoteRes bankerNoteRes = Invoker.GetBNT(
                    applicationId, 
                    requestorUserID, 
                    requestorSalesID, 
                    requestorLoc, 
                    requestorLoc, 
                    string.Empty, 
                    this.session_id);

                if (bankerNoteRes != null && bankerNoteRes.BankNoteHelper != null)
                {
                    bankerNotesHelper = bankerNoteRes.BankNoteHelper;
                }

                if (bankerNotesHelper != null && bankerNoteRes.IsOK() && bankerNotesHelper.detail != null
                    && bankerNotesHelper.detail.bankerNoteList != null
                    && bankerNotesHelper.detail.bankerNoteList.Length > 0)
                {
                    return bankerNotesHelper.bankerNoteListXmlDoc.Replace("\\\"", "\"");
                }
            }

            return string.Empty;
        }

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
        /// <exception cref="NotImplementedException">
        /// </exception>
        public string sendMessage(XmlDocument msg, EAISIFServiceBaseConfig config)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankerNote.cs" company="">
//   
// </copyright>
// <summary>
//   The eai s 002.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WellsFargo.EAI.UCA.CAS.BLL.Services.BankerNotesUpdate
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The eai s 002.
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = false)]
    [Serializable]
    public class EAIS002
    {
        #region Fields

        /// <summary>
        /// The body.
        /// </summary>
        public Body Body;

        /// <summary>
        /// The header.
        /// </summary>
        public Header Header;

        #endregion
    }

    /// <summary>
    /// The header.
    /// </summary>
    [Serializable]
    public class Header
    {
        #region Fields

        /// <summary>
        /// The activity sequence id.
        /// </summary>
        public string activitySequenceId;

        /// <summary>
        /// The billing au.
        /// </summary>
        public string billingAU;

        /// <summary>
        /// The creation timestamp.
        /// </summary>
        public string creationTimestamp;

        /// <summary>
        /// The host name.
        /// </summary>
        public string hostName;

        /// <summary>
        /// The invoker id.
        /// </summary>
        public string invokerId;

        /// <summary>
        /// The invoker id type.
        /// </summary>
        public string invokerIdType;

        /// <summary>
        /// The location id.
        /// </summary>
        public string locationId;

        /// <summary>
        /// The location id type.
        /// </summary>
        public string locationIdType;

        /// <summary>
        /// The originator id.
        /// </summary>
        public string originatorId;

        /// <summary>
        /// The originator id type.
        /// </summary>
        public string originatorIdType;

        /// <summary>
        /// The request id.
        /// </summary>
        public string requestId;

        /// <summary>
        /// The session id.
        /// </summary>
        public string sessionId;

        /// <summary>
        /// The synchronous asynchronous.
        /// </summary>
        public string synchronousAsynchronous;

        /// <summary>
        /// The time out.
        /// </summary>
        public string timeOut;

        #endregion
    }

    /// <summary>
    /// The body.
    /// </summary>
    [Serializable]
    public class Body
    {
        #region Fields

        /// <summary>
        /// The service response.
        /// </summary>
        public serviceResponse serviceResponse;

        #endregion
    }

    /// <summary>
    /// The service response.
    /// </summary>
    [Serializable]
    public class serviceResponse
    {
        #region Fields

        /// <summary>
        /// The action.
        /// </summary>
        public string action;

        /// <summary>
        /// The data.
        /// </summary>
        public data data;

        /// <summary>
        /// The error message.
        /// </summary>
        public string errorMessage;

        /// <summary>
        /// The processing time.
        /// </summary>
        public string processingTime;

        /// <summary>
        /// The service name.
        /// </summary>
        public string serviceName;

        /// <summary>
        /// The status code.
        /// </summary>
        public string statusCode;

        /// <summary>
        /// The transaction id.
        /// </summary>
        public string transactionId;

        #endregion
    }

    /// <summary>
    /// The data.
    /// </summary>
    [Serializable]
    public class data
    {
        #region Fields

        /// <summary>
        /// The banker note list.
        /// </summary>
        [XmlArrayItem("BankerNote", IsNullable = false)]
        public BankerNote[] BankerNoteList;

        #endregion
    }

    /// <summary>
    /// The banker note.
    /// </summary>
    [Serializable]
    public class BankerNote
    {
        #region Fields

        /// <summary>
        /// The exceeds flag.
        /// </summary>
        public string exceedsFlag;

        /// <summary>
        /// The read date.
        /// </summary>
        public string readDate;

        /// <summary>
        /// The read time.
        /// </summary>
        public string readTime;

        /// <summary>
        /// The reader id.
        /// </summary>
        public string readerId;

        /// <summary>
        /// The source id.
        /// </summary>
        public string sourceId;

        /// <summary>
        /// The status.
        /// </summary>
        public string status;

        /// <summary>
        /// The target id.
        /// </summary>
        public string targetId;

        /// <summary>
        /// The text.
        /// </summary>
        public Text text;

        /// <summary>
        /// The type.
        /// </summary>
        public string type;

        /// <summary>
        /// The writer id.
        /// </summary>
        public string writerId;

        /// <summary>
        /// The written date.
        /// </summary>
        public string writtenDate;

        /// <summary>
        /// The written time.
        /// </summary>
        public string writtenTime;

        #endregion
    }

    /// <summary>
    /// The text.
    /// </summary>
    [Serializable]
    public class Text
    {
        #region Fields

        /// <summary>
        /// The line 1.
        /// </summary>
        public string line1;

        /// <summary>
        /// The line 2.
        /// </summary>
        public string line2;

        /// <summary>
        /// The line 3.
        /// </summary>
        public string line3;

        /// <summary>
        /// The line 4.
        /// </summary>
        public string line4;

        /// <summary>
        /// The line 5.
        /// </summary>
        public string line5;

        /// <summary>
        /// The line 6.
        /// </summary>
        public string line6;

        /// <summary>
        /// The line 7.
        /// </summary>
        public string line7;

        /// <summary>
        /// The line 8.
        /// </summary>
        public string line8;

        #endregion
    }
}

namespace WellsFargo.EAI.UCA.CAS.BLL.Services.BankerNotes
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// The eai s 002.
    /// </summary>
    [XmlRoot(Namespace = "", IsNullable = false)]
    [Serializable]
    public class EAIS002
    {
        #region Fields

        /// <summary>
        /// The body.
        /// </summary>
        public Body Body;

        /// <summary>
        /// The header.
        /// </summary>
        public Header Header;

        #endregion
    }

    /// <summary>
    /// The header.
    /// </summary>
    [Serializable]
    public class Header
    {
        #region Fields

        /// <summary>
        /// The activity sequence id.
        /// </summary>
        public string activitySequenceId;

        /// <summary>
        /// The billing au.
        /// </summary>
        public string billingAU;

        /// <summary>
        /// The creation timestamp.
        /// </summary>
        public string creationTimestamp;

        /// <summary>
        /// The host name.
        /// </summary>
        public string hostName;

        /// <summary>
        /// The invoker id.
        /// </summary>
        public string invokerId;

        /// <summary>
        /// The invoker id type.
        /// </summary>
        public string invokerIdType;

        /// <summary>
        /// The location id.
        /// </summary>
        public string locationId;

        /// <summary>
        /// The location id type.
        /// </summary>
        public string locationIdType;

        /// <summary>
        /// The originator id.
        /// </summary>
        public string originatorId;

        /// <summary>
        /// The originator id type.
        /// </summary>
        public string originatorIdType;

        /// <summary>
        /// The request id.
        /// </summary>
        public string requestId;

        /// <summary>
        /// The session id.
        /// </summary>
        public string sessionId;

        /// <summary>
        /// The synchronous asynchronous.
        /// </summary>
        public string synchronousAsynchronous;

        /// <summary>
        /// The time out.
        /// </summary>
        public string timeOut;

        #endregion
    }

    /// <summary>
    /// The body.
    /// </summary>
    [Serializable]
    public class Body
    {
        #region Fields

        /// <summary>
        /// The service response.
        /// </summary>
        public serviceResponse serviceResponse;

        #endregion
    }

    /// <summary>
    /// The service response.
    /// </summary>
    [Serializable]
    public class serviceResponse
    {
        #region Fields

        /// <summary>
        /// The action.
        /// </summary>
        public string action;

        /// <summary>
        /// The data.
        /// </summary>
        public data data;

        /// <summary>
        /// The error message.
        /// </summary>
        public string errorMessage;

        /// <summary>
        /// The processing time.
        /// </summary>
        public string processingTime;

        /// <summary>
        /// The service name.
        /// </summary>
        public string serviceName;

        /// <summary>
        /// The status code.
        /// </summary>
        public string statusCode;

        /// <summary>
        /// The transaction id.
        /// </summary>
        public string transactionId;

        #endregion
    }

    /// <summary>
    /// The data.
    /// </summary>
    [Serializable]
    public class data
    {
        #region Fields

        /// <summary>
        /// The banker note list.
        /// </summary>
        public BankerNoteList BankerNoteList;

        #endregion
    }

    /// <summary>
    /// The banker note list.
    /// </summary>
    [Serializable]
    public class BankerNoteList
    {
        #region Fields

        /// <summary>
        /// The banker note list.
        /// </summary>
        [XmlArrayItem("cbnt_out_all_dtl", IsNullable = false)]
        public cbnt_out_all_dtl[] bankerNoteList;

        /// <summary>
        /// The cbnt_out_bnote_write_cd.
        /// </summary>
        public string cbnt_out_bnote_write_cd;

        /// <summary>
        /// The cbnt_out_sent_cnt.
        /// </summary>
        public string cbnt_out_sent_cnt;

        /// <summary>
        /// The cbnt_out_unread_for_acaps_cnt.
        /// </summary>
        public string cbnt_out_unread_for_acaps_cnt;

        /// <summary>
        /// The cbnt_out_unread_for_bankr_cnt.
        /// </summary>
        public string cbnt_out_unread_for_bankr_cnt;

        #endregion
    }

    /// <summary>
    /// The cbnt_out_all_dtl.
    /// </summary>
    [Serializable]
    public class cbnt_out_all_dtl
    {
        #region Fields

        /// <summary>
        /// The cbnt_out_bnote_target_id.
        /// </summary>
        public string cbnt_out_bnote_target_id;

        /// <summary>
        /// The cbnt_out_exceeds_sla_fl.
        /// </summary>
        public string cbnt_out_exceeds_sla_fl;

        /// <summary>
        /// The cbnt_out_read_dt.
        /// </summary>
        public string cbnt_out_read_dt;

        /// <summary>
        /// The cbnt_out_read_tm.
        /// </summary>
        public string cbnt_out_read_tm;

        /// <summary>
        /// The cbnt_out_reader_userid.
        /// </summary>
        public string cbnt_out_reader_userid;

        /// <summary>
        /// The cbnt_out_source_id.
        /// </summary>
        public string cbnt_out_source_id;

        /// <summary>
        /// The cbnt_out_status.
        /// </summary>
        public string cbnt_out_status;

        /// <summary>
        /// The cbnt_out_text.
        /// </summary>
        public Text cbnt_out_text;

        /// <summary>
        /// The cbnt_out_type.
        /// </summary>
        public string cbnt_out_type;

        /// <summary>
        /// The cbnt_out_writer_userid.
        /// </summary>
        public string cbnt_out_writer_userid;

        /// <summary>
        /// The cbnt_out_written_dt.
        /// </summary>
        public string cbnt_out_written_dt;

        /// <summary>
        /// The cbnt_out_written_tm.
        /// </summary>
        public string cbnt_out_written_tm;

        #endregion
    }

    /// <summary>
    /// The text.
    /// </summary>
    [Serializable]
    public class Text
    {
        #region Fields

        /// <summary>
        /// The cbnt_out_text_line 1.
        /// </summary>
        public string cbnt_out_text_line1;

        /// <summary>
        /// The cbnt_out_text_line 2.
        /// </summary>
        public string cbnt_out_text_line2;

        /// <summary>
        /// The cbnt_out_text_line 3.
        /// </summary>
        public string cbnt_out_text_line3;

        /// <summary>
        /// The cbnt_out_text_line 4.
        /// </summary>
        public string cbnt_out_text_line4;

        /// <summary>
        /// The cbnt_out_text_line 5.
        /// </summary>
        public string cbnt_out_text_line5;

        /// <summary>
        /// The cbnt_out_text_line 6.
        /// </summary>
        public string cbnt_out_text_line6;

        /// <summary>
        /// The cbnt_out_text_line 7.
        /// </summary>
        public string cbnt_out_text_line7;

        /// <summary>
        /// The cbnt_out_text_line 8.
        /// </summary>
        public string cbnt_out_text_line8;

        #endregion
    }
}
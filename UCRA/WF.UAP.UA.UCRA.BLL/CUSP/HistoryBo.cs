// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HistoryBo.cs" company="">
//   
// </copyright>
// <summary>
//   The history bo.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.CUSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WF.EAI.BLL.CUSP.Invokers;
    using WF.EAI.Data.sif.Services.CWS.History;
    using WF.EAI.Entities.domain.c2c.Common;
    using WF.EAI.Entities.domain.c2c.Core;
    using WF.EAI.Entities.domain.c2c.Grid;
    using WF.EAI.Entities.domain.c2c.UI;
    using WF.EAI.Entities.exception;
    using WF.EAI.Utils;

    /// <summary>
    ///     The history bo.
    /// </summary>
    public class HistoryBo
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get application history.
        /// </summary>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <param name="activityCode">
        /// The activity code.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        /// <exception cref="BusinessLayerException">
        /// </exception>
        public List<ApplicationHistoryEntity> GetApplicationHistory(AppDataHeader header, string activityCode)
        {
            bool validResponse = true;
            ApplicationHistoryEntity applicationHistoryEntity;
            List<ApplicationHistoryEntity> applicationHistoryEntityList = new List<ApplicationHistoryEntity>();
            try
            {
                HistoryRes historyResponse = Invoker.History(
                    header.UserId, 
                    header.Password, 
                    string.Empty, 
                    header.ApplicationId, 
                    0, 
                    string.Empty, 
                    header.SessionId);

                if (historyResponse == null || historyResponse.HistoryResults == null || !historyResponse.IsOK())
                {
                    validResponse = false;
                    string sifErrMsg = string.Empty;
                    if ((historyResponse != null) && (historyResponse.ex != null))
                    {
                        sifErrMsg = historyResponse.ex.Message;
                    }

                    throw new Exception(string.Format("Error getting History Results Response. {0}", sifErrMsg));
                }

                if (validResponse)
                {
                    for (int i = 0; i <= historyResponse.HistoryResults.Count - 1; i++)
                    {
                        applicationHistoryEntity = new ApplicationHistoryEntity();
                        applicationHistoryEntity.ActivityCode = historyResponse.HistoryResults[i].ActivityCode;
                        applicationHistoryEntity.ActivityDate = historyResponse.HistoryResults[i].ActivityDate;
                        applicationHistoryEntity.ActivityTime = historyResponse.HistoryResults[i].ActivityTime;
                        applicationHistoryEntity.ActivityDateTime = string.Format(
                            "{0} {1}", 
                            Utilities.convertToDateFormat(historyResponse.HistoryResults[i].ActivityDate), 
                            Utilities.convertToTimeFormat(
                                Convert.ToString(historyResponse.HistoryResults[i].ActivityTime)));
                        applicationHistoryEntity.Comments = historyResponse.HistoryResults[i].Comments;
                        applicationHistoryEntity.Letter = historyResponse.HistoryResults[i].Letter;
                        applicationHistoryEntity.SequenceNum = historyResponse.HistoryResults[i].SequenceNum;
                        applicationHistoryEntity.State = historyResponse.HistoryResults[i].State;
                        applicationHistoryEntity.UserId = historyResponse.HistoryResults[i].UserId.Trim();
                        applicationHistoryEntity.UserName = historyResponse.HistoryResults[i].UserName;

                        if (activityCode.Equals("ALL"))
                        {
                            applicationHistoryEntityList.Add(applicationHistoryEntity);
                        }
                        else if (activityCode.Equals("Q1")
                                 && historyResponse.HistoryResults[i].ActivityCode == activityCode)
                        {
                            applicationHistoryEntityList.Add(applicationHistoryEntity);
                        }
                    }
                }
            }
            catch (BusinessLayerException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return applicationHistoryEntityList;
        }

        // Fraud
        /// <summary>
        /// The get application history for fraud.
        /// </summary>
        /// <param name="applicationData">
        /// The application data.
        /// </param>
        /// <param name="activityCode">
        /// The activity code.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<ApplicationHistoryEntity> GetApplicationHistoryForFraud(
            ApplicationData applicationData, string activityCode)
        {
            bool validResponse = true;
            ApplicationHistoryEntity applicationHistoryEntity;
            List<ApplicationHistoryEntity> applicationHistoryEntityList = new List<ApplicationHistoryEntity>();

            HistoryRes historyResponse = Invoker.History(
                applicationData.AppDataHeader.UserId, 
                applicationData.AppDataHeader.Password, 
                string.Empty, 
                applicationData.AppDataHeader.ApplicationId, 
                0, 
                string.Empty, 
                applicationData.AppDataHeader.SessionId);

            if (historyResponse == null || historyResponse.HistoryResults == null || !historyResponse.IsOK())
            {
                validResponse = false;
                string sifErrMsg = string.Empty;
                if ((historyResponse != null) && (historyResponse.ex != null))
                {
                    sifErrMsg = historyResponse.ex.Message;
                }

                throw new Exception(string.Format("Error getting History Results Response. {0}", sifErrMsg));
            }

            if (validResponse)
            {
                for (int i = 0; i <= historyResponse.HistoryResults.Count - 1; i++)
                {
                    applicationHistoryEntity = new ApplicationHistoryEntity();
                    applicationHistoryEntity.ActivityCode = historyResponse.HistoryResults[i].ActivityCode;
                    applicationHistoryEntity.ActivityDate = historyResponse.HistoryResults[i].ActivityDate;
                    applicationHistoryEntity.ActivityTime = historyResponse.HistoryResults[i].ActivityTime;
                    applicationHistoryEntity.ActivityDateTime = string.Format(
                        "{0} {1}", 
                        Utilities.convertToDateFormat(historyResponse.HistoryResults[i].ActivityDate), 
                        Utilities.convertToTimeFormat(Convert.ToString(historyResponse.HistoryResults[i].ActivityTime)));
                    applicationHistoryEntity.Comments = historyResponse.HistoryResults[i].Comments;
                    applicationHistoryEntity.Letter = historyResponse.HistoryResults[i].Letter;
                    applicationHistoryEntity.SequenceNum = historyResponse.HistoryResults[i].SequenceNum;
                    applicationHistoryEntity.State = historyResponse.HistoryResults[i].State;
                    applicationHistoryEntity.UserId = historyResponse.HistoryResults[i].UserId;
                    applicationHistoryEntity.UserName = historyResponse.HistoryResults[i].UserName;

                    if (historyResponse.HistoryResults[i].ActivityCode == activityCode)
                    {
                        applicationHistoryEntityList.Add(applicationHistoryEntity);
                    }
                }
            }

            return applicationHistoryEntityList;
        }

        /// <summary>
        /// The get cip failure details.
        /// </summary>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        /// <exception cref="BusinessLayerException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public Dictionary<string, string> GetCIPFailureDetails(AppDataHeader header)
        {
            bool validResponse = true;
            HistoryRes.HistoryResult historyResult = new HistoryRes.HistoryResult();
            Dictionary<string, string> dictionaryCIP = new Dictionary<string, string>();
            try
            {
                HistoryRes historyResponse = Invoker.History(
                    header.UserId, 
                    header.Password, 
                    string.Empty, 
                    header.ApplicationId, 
                    0, 
                    string.Empty, 
                    header.SessionId);
                if (historyResponse == null)
                {
                    validResponse = false;
                    throw new BusinessLayerException("History Response in null");
                }

                if (historyResponse.IsOK() == false)
                {
                    validResponse = false;
                    throw new BusinessLayerException("History Exception", historyResponse.ex);
                }

                if (validResponse)
                {
                    if (historyResponse.HistoryResults != null)
                    {
                        historyResult =
                            (from field in historyResponse.HistoryResults
                             where field.ActivityCode.Trim() == "NP"
                             select field).FirstOrDefault();
                        if (historyResult != null)
                        {
                            dictionaryCIP.Add("ActivityDate", Utilities.convertToDateFormat(historyResult.ActivityDate));
                            dictionaryCIP.Add("ActivityTime", historyResult.ActivityTime.ToString());
                            dictionaryCIP.Add("UserId", historyResult.UserId);
                            dictionaryCIP.Add("Comments", historyResult.Comments);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dictionaryCIP;
        }

        /// <summary>
        /// The get key decision disclosure letter dates.
        /// </summary>
        /// <param name="applicationData">
        /// The application data.
        /// </param>
        /// <returns>
        /// The <see cref="KeyDecisionDisclosureLetterDatesEntity"/>.
        /// </returns>
        /// <exception cref="BusinessLayerException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public KeyDecisionDisclosureLetterDatesEntity GetKeyDecisionDisclosureLetterDates(
            ApplicationData applicationData)
        {
            KeyDecisionDisclosureLetterDatesEntity keyDecisionDisclosureLetterDates =
                new KeyDecisionDisclosureLetterDatesEntity(applicationData);

            bool validResponse = true;

            try
            {
                HistoryRes historyResponse = Invoker.History(
                    applicationData.AppDataHeader.UserId, 
                    applicationData.AppDataHeader.Password, 
                    string.Empty, 
                    applicationData.AppDataHeader.ApplicationId, 
                    0, 
                    string.Empty, 
                    applicationData.AppDataHeader.SessionId);
                if (historyResponse == null)
                {
                    validResponse = false;
                    throw new BusinessLayerException("History Response in null");
                }

                if (historyResponse.IsOK() == false)
                {
                    validResponse = false;
                    throw new BusinessLayerException("History Exception", historyResponse.ex);
                }

                if (validResponse)
                {
                    if (historyResponse.HistoryResults != null)
                    {
                        this.PopulateHistoryResultsForDecisionDates(historyResponse, keyDecisionDisclosureLetterDates);
                        this.PopulateHistoryResultsForDisclosureLetter(
                            historyResponse, keyDecisionDisclosureLetterDates);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return keyDecisionDisclosureLetterDates;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The poplate history resultfor decision date for activity code.
        /// </summary>
        /// <param name="historyResponse">
        /// The history response.
        /// </param>
        /// <param name="decisionDates">
        /// The decision dates.
        /// </param>
        /// <param name="ActivityCode">
        /// The activity code.
        /// </param>
        private void PoplateHistoryResultforDecisionDateForActivityCode(
            HistoryRes historyResponse, List<DecisionDate> decisionDates, string ActivityCode)
        {
            DecisionDate decisionDate = new DecisionDate();
            HistoryRes.HistoryResult historyResultFirst = null;
            HistoryRes.HistoryResult historyResultLast = null;
            if (ActivityCode == "AF/AU")
            {
                List<string> strActivityCodes = new List<string>();
                strActivityCodes.Add("AU");
                strActivityCodes.Add("AF");
                historyResultFirst =
                    historyResponse.HistoryResults.LastOrDefault(h => strActivityCodes.Contains(h.ActivityCode.Trim()));
                historyResultLast =
                    historyResponse.HistoryResults.FirstOrDefault(h => strActivityCodes.Contains(h.ActivityCode.Trim()));
            }
            else
            {
                // Get the LastOrDefault for the First and FirstOrDefault for the Last since the HistoryResult comes in the datetime descending order
                historyResultFirst =
                    historyResponse.HistoryResults.LastOrDefault(h => h.ActivityCode.Trim() == ActivityCode);

                historyResultLast =
                    historyResponse.HistoryResults.FirstOrDefault(h => h.ActivityCode.Trim() == ActivityCode);
            }

            if (historyResultFirst != null || historyResultLast != null)
            {
                decisionDate = decisionDates.Find(s => s.ActivityCode == ActivityCode);
            }

            if (historyResultFirst != null)
            {
                decisionDate.FirstOccurance = Utilities.convertToDateFormat(historyResultFirst.ActivityDate);
            }

            if (historyResultLast != null)
            {
                decisionDate.LatestOccurance = Utilities.convertToDateFormat(historyResultLast.ActivityDate);
            }
        }

        /// <summary>
        /// The poplate history resultfor disclosure letter dates for cdp letter code.
        /// </summary>
        /// <param name="historyResponse">
        /// The history response.
        /// </param>
        /// <param name="disclosureLetterDates">
        /// The disclosure letter dates.
        /// </param>
        /// <param name="CDPLetterCode">
        /// The cdp letter code.
        /// </param>
        private void PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
            HistoryRes historyResponse, List<DisclosureLetterDate> disclosureLetterDates, string CDPLetterCode)
        {
            DisclosureLetterDate disclosureLetterDate = disclosureLetterDates.Find(
                s => s.CDPLetterCode == CDPLetterCode);
            HistoryRes.HistoryResult historyResult = new HistoryRes.HistoryResult();

            switch (CDPLetterCode)
            {
                case "RSP1":
                    historyResult =
                        (from field in historyResponse.HistoryResults
                         where field.ActivityCode.Trim() == "PT"
                         select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type RSP1")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult =
                        (from field in historyResponse.HistoryResults
                         where field.ActivityCode.Trim() == "PT"
                         select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type RSP1")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "PFCT":
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "PFCT" select field)
                            .LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type PFCT")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.ActivityCode.Trim() == "BL" && field.Letter.Trim() == "PFCT"
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type PFCT")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "SFCT":
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "SFCT" select field)
                            .LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type SFCT")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.ActivityCode.Trim() == "BL" && field.Letter.Trim() == "SFCT"
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type SFCT")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "ITRM":
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "ITRM" && field.ActivityCode.Trim() == "IL"
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type ITRM")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "ITRM" && field.ActivityCode.Trim() == "IL"
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type ITRM")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "MTRM":
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "MTRM" && field.ActivityCode.Trim() == "IL"
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type MTRM")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "MTRM" && field.ActivityCode.Trim() == "IL"
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type MTRM")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "FETL":
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "FETL" && field.ActivityCode.Trim() == "P1"
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type FETL")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "FETL" && field.ActivityCode.Trim() == "P1"
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type FETL")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "CAPP":
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "CAPP" select field)
                            .LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type CAPP")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "CAPP" select field)
                            .FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type CAPP")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "CACO":
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "CACO" select field)
                            .LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type CACO")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "CACO" select field)
                            .FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type CACO")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "CCBR":
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.ActivityCode.Trim() == "TD" && field.Letter.Trim() == "CCBR"
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type CCBR")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "CCBR" && field.ActivityCode.Trim() == "TD"
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type CCBR")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;

                case "CHGC":
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.ActivityCode.Trim() == "CZ" && field.Letter.Trim() == "CHGC"
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type CHGC")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "CHGC" && field.ActivityCode.Trim() == "CZ"
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BD"
                                         && field.Comments.Trim().Contains("Document Type CHGC")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "SFHL":
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "SFHL" select field)
                            .LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type SFHL")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "SFHL" select field)
                            .FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type SFHL")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "SCAP":
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.ActivityCode.Trim() == "SY" && field.Letter.Trim() == "SCAP"
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type SCAP")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult =
                        (from field in historyResponse.HistoryResults where field.Letter.Trim() == "SCAP" select field)
                            .FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type SCAP")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
                case "TXNC":
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.ActivityCode.Trim() == "LS" && field.Letter.Trim() == "TXNC"
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type TXNC")
                                     select field).LastOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.FirstPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where field.Letter.Trim() == "TXNC" && field.ActivityCode.Trim() == "LS"
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastDateRequestedGenerated =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    historyResult = new HistoryRes.HistoryResult();
                    historyResult = (from field in historyResponse.HistoryResults
                                     where
                                         field.ActivityCode.Trim() == "BL"
                                         && field.Comments.Trim().Contains("Document Type TXNC")
                                     select field).FirstOrDefault();
                    if (historyResult != null)
                    {
                        disclosureLetterDate.LastPrintedMailedDate =
                            Utilities.convertToDateFormat(historyResult.ActivityDate);
                    }

                    break;
            }

            /* HistoryRes.HistoryResult historyResultFirst =
              historyResponse.HistoryResults.FirstOrDefault(h => h.Letter.Trim() == CDPLetterCode);

            HistoryRes.HistoryResult historyResultLast =
                historyResponse.HistoryResults.LastOrDefault(h => h.Letter.Trim() == CDPLetterCode);

            if (historyResultFirst != null || historyResultLast != null)
                disclosureLetterDate = disclosureLetterDates.Find(s => s.CDPLetterCode == CDPLetterCode);
                
            if (historyResultFirst != null)
                disclosureLetterDate.FirstDateRequestedGenerated = historyResultFirst.ActivityDate;
            
            if (historyResultLast != null)
                disclosureLetterDate.LastDateRequestedGenerated= historyResultLast.ActivityDate;
            */
        }

        /// <summary>
        /// The populate history results for decision dates.
        /// </summary>
        /// <param name="historyResponse">
        /// The history response.
        /// </param>
        /// <param name="keyDecisionDisclosureLetterDates">
        /// The key decision disclosure letter dates.
        /// </param>
        private void PopulateHistoryResultsForDecisionDates(
            HistoryRes historyResponse, KeyDecisionDisclosureLetterDatesEntity keyDecisionDisclosureLetterDates)
        {
            this.PoplateHistoryResultforDecisionDateForActivityCode(
                historyResponse, keyDecisionDisclosureLetterDates.DecisionDates, "AP");
            this.PoplateHistoryResultforDecisionDateForActivityCode(
                historyResponse, keyDecisionDisclosureLetterDates.DecisionDates, "CO");
            this.PoplateHistoryResultforDecisionDateForActivityCode(
                historyResponse, keyDecisionDisclosureLetterDates.DecisionDates, "TD");
            this.PoplateHistoryResultforDecisionDateForActivityCode(
                historyResponse, keyDecisionDisclosureLetterDates.DecisionDates, "AF/AU");
            this.PoplateHistoryResultforDecisionDateForActivityCode(
                historyResponse, keyDecisionDisclosureLetterDates.DecisionDates, "AW");
            this.PoplateHistoryResultforDecisionDateForActivityCode(
                historyResponse, keyDecisionDisclosureLetterDates.DecisionDates, "IN");
        }

        /// <summary>
        /// The populate history results for disclosure letter.
        /// </summary>
        /// <param name="historyResponse">
        /// The history response.
        /// </param>
        /// <param name="keyDecisionDisclosureLetterDates">
        /// The key decision disclosure letter dates.
        /// </param>
        private void PopulateHistoryResultsForDisclosureLetter(
            HistoryRes historyResponse, KeyDecisionDisclosureLetterDatesEntity keyDecisionDisclosureLetterDates)
        {
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "RSP1");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "PFCT");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "SFCT");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "ITRM");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "MTRM");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "FETL");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "CAPP");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "CACO");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "CCBR");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "CHGC");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "SFHL");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "SCAP");
            this.PoplateHistoryResultforDisclosureLetterDatesForCDPLetterCode(
                historyResponse, keyDecisionDisclosureLetterDates.DisclosureLetterDates, "TXNC");
        }

        #endregion
    }
}
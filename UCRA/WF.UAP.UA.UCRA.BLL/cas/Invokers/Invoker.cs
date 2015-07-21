// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Invoker.cs" company="">
//   
// </copyright>
// <summary>
//   The invoker.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.cas.Invokers
{
    using System.Collections;
    using System.Xml;
    using System;

    //using WellsFargo.EAI.SIF.ServiceProxy.COM.WellsFargo.Service.Provider.RWS.Form2008;
    using WellsFargo.EAI.SIF.ServiceProxy.COM.WellsFargo.Service.Provider.RWS.Form2008;

    using WF.EAI.Data.sif;
    using WF.EAI.Data.sif.Services.Auth.PBAuthentication;
    using WF.EAI.Data.sif.Services.Auth.SVPAuthentication;
    using WF.EAI.Data.sif.Services.CWS.BankerNoteUnread;
    using WF.EAI.Data.sif.Services.CWS.BankerNotes;
    using WF.EAI.Data.sif.Services.CWS.BankerNotes.BankerNoteList;
    using WF.EAI.Data.sif.Services.CWS.BankerNotes.BankerNoteSubmit;
    using WF.EAI.Data.sif.Services.CWS.CASInq;
    using WF.EAI.Data.sif.Services.CWS.CASTableInquiry;
    using WF.EAI.Data.sif.Services.CWS.CASUpd;
    using WF.EAI.Data.sif.Services.CWS.CCHToACAPS;
    using WF.EAI.Data.sif.Services.CWS.GetAlerts;
    using WF.EAI.Data.sif.Services.CWS.PRI;
    using WF.EAI.Data.sif.Services.CWS.PriceInfo;
    using WF.EAI.Data.sif.Services.CWS.STD;
    using WF.EAI.Data.sif.Services.CWS.TableInquiry;

    using WellsFargo.EAI.SIF.ServiceProxy.com.wellsfargo.service.provider.helper;
    using WellsFargo.EAI.SIF.Services.CASOthers.PQOffers;

    using WF.EAI.Data.sif.Services.APS.AccountInquiry201109;
    using WF.EAI.Data.sif.Services.APS.MWS;
    using WF.EAI.Data.sif.Services.CASOthers.OnlineUpdate;
    using WF.EAI.Data.sif.Services.CSTS.SRCH;
    using WF.EAI.Data.sif.Services.EAI.PrintCCH;
    using WF.EAI.Data.sif.Services.ECBS.GetAccountInformation;
    using WF.EAI.Data.sif.Services.ECBS.GetCustomerInformation200909;
    using WF.EAI.Data.sif.Services.RWS.GetForm;
    //using WF.EAI.Data.sif.Services.System2.CreateCreditReversalRequest;
    //using WF.EAI.Data.sif.Services.System2.GetACHFlag;
    //using WF.EAI.Data.sif.Services.System2.GetAppDetails;
    //using WF.EAI.Data.sif.Services.System2.GetAppPricingDetails;
    //using WF.EAI.Data.sif.Services.System2.GetBankerApps;
    //using WF.EAI.Data.sif.Services.System2.GetScenarioInfo;
    //using WF.EAI.Data.sif.Services.System2.GetUnreadNotesFlag;
    //using WF.EAI.Data.sif.Services.System2.GetUpdatebleFields;
    //using WF.EAI.Data.sif.Services.System2.InitiateCDPPrint;
    //using WF.EAI.Data.sif.Services.System2.LoadStep;
    //using WF.EAI.Data.sif.Services.System2.PerformBankerNoteActions;
    //using WF.EAI.Data.sif.Services.System2.UpdateAppFields;
    using WF.EAI.Entities.constants;
    using WF.EAI.Data.sif.Services.CSTS.Cops.GetPrimeRate;
    using System.Linq;

    /// <summary>
    ///     The invoker.
    /// </summary>
    public class Invoker
    {
        // Added by Pardha S. Chakka
        #region Public Methods and Operators

        /// <summary>
        /// The account inquiry.
        /// </summary>
        /// <param name="AU">
        /// The au.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="accountNumber">
        /// The account number.
        /// </param>
        /// <param name="ECN">
        /// The ecn.
        /// </param>
        /// <param name="initiatorId">
        /// The initiator id.
        /// </param>
        /// <param name="initiatorIdType">
        /// The initiator id type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string AccountInquiry(
            string AU, string sessionId, string accountNumber, string ECN, string initiatorId, string initiatorIdType)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                return string.Empty;
            }

            string defaultAU = !string.IsNullOrEmpty(AU) ? AU : Constants.DefaultAU;
            string serviceName = Constants.ServiceNameEnum.AccountInquiry201109.ToString();
            string originatorId = ECN;
            string rtn = string.Empty;

            string applicationName = Constants.ApplicationNameEnum.CAS.ToString();

            string invokerId = applicationName;

            EAIService eaiService = EAIService.getService(serviceName, applicationName, sessionId);
            var req = new AccountInquiry201109Req(
                accountNumber, 
                invokerId, 
                defaultAU, 
                originatorId, 
                initiatorId, 
                sessionId, 
                serviceName, 
                applicationName, 
                initiatorIdType);
            var res = (AccountInquiry201109Res)eaiService.Execute(req);
            if (res != null && res.getAccountResponse != null && res.getAccountResponse.GetAccountRs.Info != null)
            {
                rtn = res.getAccountResponse.GetAccountRs.Info.Account.DemandDepositAccount.Base.PrimaryRTN;
            }

            return rtn;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The CreateCreditReversal
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="msgRequestCD">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="createCreditReversalRequestTypeDataRequest">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="CreateCreditReversalRequestRes"/>.
        /// </returns>
        //public static CreateCreditReversalRequestRes CreateCreditReversalRequest(
        //    string defaultAU, 
        //    string msgRequestCD, 
        //    string sessionCorrelationId, 
        //    string applicationID, 
        //    string requestorLoc, 
        //    string requestorID, 
        //    string requestorSalesID, 
        //    CreateCreditReversalRequestReq.createCreditReversalRequest_TypeDataRequest
        //        createCreditReversalRequestTypeDataRequest, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService =
        //        EAIService.getService(
        //            Constants.ServiceNameEnum.CreateCreditReversalRequest.ToString(), 
        //            Constants.ApplicationNameEnum.CAS.ToString(), 
        //            globalTransactionId);
        //    CreateCreditReversalRequestReq createCreditReversalRequestReq = new CreateCreditReversalRequestReq(
        //        defaultAU, 
        //        msgRequestCD, 
        //        sessionCorrelationId, 
        //        applicationID, 
        //        requestorLoc, 
        //        requestorID, 
        //        requestorSalesID, 
        //        createCreditReversalRequestTypeDataRequest, 
        //        serviceName, 
        //        applicationName);
        //    CreateCreditReversalRequestRes createCreditReversalRequestRes =
        //        (CreateCreditReversalRequestRes)eaiService.Execute(createCreditReversalRequestReq);
        //    return createCreditReversalRequestRes;
        //}

        /// <summary>
        /// </summary>
        /// <param name="applicationId">
        /// </param>
        /// <param name="appOnlineEnroll">
        /// </param>
        /// <param name="coAppOnlineEnroll">
        /// </param>
        /// <param name="appEmailEnroll">
        /// </param>
        /// <param name="coAppEmailEnroll">
        /// </param>
        /// <param name="appEmailAddress">
        /// </param>
        /// <param name="coAppEmailAddress">
        /// </param>
        /// <param name="environment">
        /// </param>
        /// <param name="invokerid">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="OnlineUpdateRes"/>.
        /// </returns>
        public static OnlineUpdateRes DoOnlineUpdate(
            string applicationId, 
            string appOnlineEnroll, 
            string coAppOnlineEnroll, 
            string appEmailEnroll, 
            string coAppEmailEnroll, 
            string appEmailAddress, 
            string coAppEmailAddress, 
            string environment, 
            string invokerid, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.OnlineUpdate.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            OnlineUpdateReq onlineUpdateReq = new OnlineUpdateReq(
                applicationId, 
                appOnlineEnroll, 
                coAppOnlineEnroll, 
                appEmailEnroll, 
                coAppOnlineEnroll, 
                appEmailAddress, 
                coAppEmailAddress, 
                environment, 
                invokerid);
            OnlineUpdateRes onlineUpdateRes = (OnlineUpdateRes)eaiService.Execute(onlineUpdateReq);
            return onlineUpdateRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The GetACHFlag
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="locationCode">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="hris">
        /// </param>
        /// <param name="ecn">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="GetACHFlagRes"/>.
        /// </returns>
        //public static GetACHFlagRes GetACHFlag(
        //    string defaultAU, 
        //    string sessionCorrelationId, 
        //    string locationCode, 
        //    string applicationID, 
        //    string requestorSalesID, 
        //    string hris, 
        //    string ecn, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.GetACHFlag.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    GetACHFlagReq getACHFlagReq = new GetACHFlagReq(
        //        defaultAU, 
        //        sessionCorrelationId, 
        //        locationCode, 
        //        applicationID, 
        //        requestorSalesID, 
        //        hris, 
        //        ecn, 
        //        serviceName, 
        //        applicationName);
        //    GetACHFlagRes getACHFlagRes = (GetACHFlagRes)eaiService.Execute(getACHFlagReq);
        //    return getACHFlagRes;
        //}

        public static decimal GetPrimeRate(string applicationCode, string transactionId)
        { 
            decimal primeRate = 0;
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.GetPrimeRate.ToString(),
                Constants.ApplicationNameEnum.CAS.ToString(),
                transactionId);
            getPrimeRateReq getPrimeRateReq = new getPrimeRateReq(
               applicationCode,
                 Constants.ServiceNameEnum.GetPrimeRate.ToString(),
                Constants.ApplicationNameEnum.CAS.ToString());
            getPrimeRateRes getPrimeRateRes = (getPrimeRateRes)eaiService.Execute(getPrimeRateReq);
            if(getPrimeRateRes.IsOK() &&
               getPrimeRateRes.resType != null && getPrimeRateRes.resType.primeRateInfoLst != null && getPrimeRateRes.resType.primeRateInfoLst.Count > 0)
            {
                DateTime today = DateTime.Now;
                if (getPrimeRateRes.resType.primeRateInfoLst.Where((o => (o.expiryDate >= today && today >= o.effectiveDate))).ToList() != null
                 && getPrimeRateRes.resType.primeRateInfoLst.Where((o => (o.expiryDate >= today && today >= o.effectiveDate))).ToList().Count > 0)
                    primeRate = getPrimeRateRes.resType.primeRateInfoLst.Where((o => (o.expiryDate >= today && today >= o.effectiveDate))).ToList().First().primeRate;
                
               
            }
            return primeRate;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get Alerts/Tasks
        /// </summary>
        /// <param name="applicationId">
        /// The application Id.
        /// </param>
        /// <param name="userLocationCode">
        /// The user Location Code.
        /// </param>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="userSalesId">
        /// The user Sales Id.
        /// </param>
        /// <param name="userAU">
        /// The user AU.
        /// </param>
        /// <param name="pendingValidation">
        /// The pending Validation.
        /// </param>
        /// <param name="checkEligibility">
        /// The check Eligibility.
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="CWSInqRes"/>.
        /// </returns>
        public static CWSInqRes GetAcapsData(
            string applicationId, 
            string userLocationCode, 
            string userId, 
            string userSalesId, 
            string userAU, 
            bool pendingValidation, 
            bool checkEligibility, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSINQ.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            CWSInqReq acapsReq = new CWSInqReq(
                applicationId, userLocationCode, userId, userSalesId, userAU, pendingValidation, checkEligibility);
            CWSInqRes acapsRes = (CWSInqRes)eaiService.Execute(acapsReq);
            return acapsRes;
        }

        /// <summary>
        /// The get account information.
        /// </summary>
        /// <param name="defaultAU">
        /// The default au.
        /// </param>
        /// <param name="customerNumber">
        /// The customer number.
        /// </param>
        /// <param name="initiatorId">
        /// The initiator id.
        /// </param>
        /// <param name="initiatorCompanyNumber">
        /// The initiator company number.
        /// </param>
        /// <param name="serviceName">
        /// The service name.
        /// </param>
        /// <param name="applicationName">
        /// The application name.
        /// </param>
        /// <param name="accountList">
        /// The account list.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="GetAccountInformationRes"/>.
        /// </returns>
        public static GetAccountInformationRes GetAccountInformation(
            string defaultAU, 
            string customerNumber, 
            string initiatorId, 
            string initiatorCompanyNumber, 
            string serviceName, 
            string applicationName, 
            GetAccountInformationReq.Account_Type[] accountList, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.GetAccountInformation.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            GetAccountInformationReq getAccountInformationReq = new GetAccountInformationReq(
                defaultAU, 
                customerNumber, 
                initiatorId, 
                initiatorCompanyNumber, 
                serviceName, 
                applicationName, 
                accountList);
            GetAccountInformationRes getAccountInformationRes =
                (GetAccountInformationRes)eaiService.Execute(getAccountInformationReq);
            return getAccountInformationRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The GetAppDetails
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="msgRequestCD">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="GetAppDetailsRes"/>.
        /// </returns>
        //public static GetAppDetailsRes GetAppDetails(
        //    string defaultAU, 
        //    string msgRequestCD, 
        //    string sessionCorrelationId, 
        //    string applicationID, 
        //    string requestorLoc, 
        //    string requestorID, 
        //    string requestorSalesID, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.GetAppDetails.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    GetAppDetailsReq getAppDetailsReq = new GetAppDetailsReq(
        //        defaultAU, 
        //        msgRequestCD, 
        //        sessionCorrelationId, 
        //        applicationID, 
        //        requestorLoc, 
        //        requestorID, 
        //        requestorSalesID, 
        //        serviceName, 
        //        applicationName);
        //    GetAppDetailsRes getAppDetailsRes = (GetAppDetailsRes)eaiService.Execute(getAppDetailsReq);
        //    return getAppDetailsRes;
        //}

        /// Added by Rod Javier
        /// <summary>
        /// The GetAppPricingDetails
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="msgRequestCD">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="GetAppPricingDetailsRes"/>.
        /// </returns>
        //public static GetAppPricingDetailsRes GetAppPricingDetails(
        //    string defaultAU, 
        //    string msgRequestCD, 
        //    string sessionCorrelationId, 
        //    string applicationID, 
        //    string requestorLoc, 
        //    string requestorID, 
        //    string requestorSalesID, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.GetAppPricingDetails.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    GetAppPricingDetailsReq getAppDetailsReq = new GetAppPricingDetailsReq(
        //        defaultAU, 
        //        msgRequestCD, 
        //        sessionCorrelationId, 
        //        applicationID, 
        //        requestorLoc, 
        //        requestorID, 
        //        requestorSalesID, 
        //        serviceName, 
        //        applicationName);
        //    GetAppPricingDetailsRes getAppDetailsRes = (GetAppPricingDetailsRes)eaiService.Execute(getAppDetailsReq);
        //    return getAppDetailsRes;
        //}

        /// <summary>
        /// Added By Rammohan Sudham.
        /// </summary>
        /// <param name="invokerId">
        /// </param>
        /// <param name="billingAU">
        /// </param>
        /// <param name="channel">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="BankerNoteListRes"/>.
        /// </returns>
        public static BankerNoteListRes GetBNL(
            string invokerId, string billingAU, string channel, string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSBNL.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);

            BankerNoteListReq bankerNoteListReq = new BankerNoteListReq(invokerId, billingAU, channel);
            BankerNoteListRes bankerNoteListRes = (BankerNoteListRes)eaiService.Execute(bankerNoteListReq);
            return bankerNoteListRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get BNS
        /// </summary>
        /// <param name="appId">
        /// </param>
        /// <param name="invokerId">
        /// </param>
        /// <param name="hris">
        /// </param>
        /// <param name="billingAU">
        /// </param>
        /// <param name="locationId">
        /// </param>
        /// <param name="channel">
        /// </param>
        /// <param name="bankerNoteType">
        /// </param>
        /// <param name="bankerNoteSource">
        /// </param>
        /// <param name="bankerNoteTarget">
        /// </param>
        /// <param name="bankerNoteText">
        /// </param>
        /// <param name="sequenceNumber">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="BankerNoteSubmitRes"/>.
        /// </returns>
        public static BankerNoteSubmitRes GetBNS(
            string appId, 
            string invokerId, 
            string hris, 
            string billingAU, 
            string locationId, 
            string channel, 
            string bankerNoteType, 
            string bankerNoteSource, 
            string bankerNoteTarget, 
            string bankerNoteText, 
            string sequenceNumber, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSBNS.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            BankerNoteSubmitReq bankerNoteSubmitReq = new BankerNoteSubmitReq(
                appId, 
                invokerId, 
                hris, 
                billingAU, 
                locationId, 
                channel, 
                bankerNoteType, 
                bankerNoteSource, 
                bankerNoteTarget, 
                bankerNoteText, 
                sequenceNumber);
            BankerNoteSubmitRes bankerNoteSubmitRes = (BankerNoteSubmitRes)eaiService.Execute(bankerNoteSubmitReq);
            return bankerNoteSubmitRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get BNT
        /// </summary>
        /// <param name="AppId">
        /// The App Id.
        /// </param>
        /// <param name="InvokerId">
        /// The Invoker Id.
        /// </param>
        /// <param name="Hris">
        /// The Hris.
        /// </param>
        /// <param name="BillingAU">
        /// The Billing AU.
        /// </param>
        /// <param name="LocationId">
        /// The Location Id.
        /// </param>
        /// <param name="Channel">
        /// The Channel.
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="BankerNoteRes"/>.
        /// </returns>
        public static BankerNoteRes GetBNT(
            string AppId, 
            string InvokerId, 
            string Hris, 
            string BillingAU, 
            string LocationId, 
            string Channel, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSBNT.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            BankerNoteReq bankerNoteReq = new BankerNoteReq(AppId, InvokerId, Hris, BillingAU, LocationId, Channel);
            BankerNoteRes bankerNoteRes = (BankerNoteRes)eaiService.Execute(bankerNoteReq);
            return bankerNoteRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get BNU
        /// </summary>
        /// <param name="appId">
        /// </param>
        /// <param name="invokerId">
        /// The invoker Id.
        /// </param>
        /// <param name="hris">
        /// The hris.
        /// </param>
        /// <param name="billingAU">
        /// The billing AU.
        /// </param>
        /// <param name="locationId">
        /// The location Id.
        /// </param>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="BankerNoteUnreadRes"/>.
        /// </returns>
        public static BankerNoteUnreadRes GetBNU(
            string appId, 
            string invokerId, 
            string hris, 
            string billingAU, 
            string locationId, 
            string channel, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSBNU.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            BankerNoteUnreadReq bankerNoteUnreadReq = new BankerNoteUnreadReq(
                appId, invokerId, hris, billingAU, locationId, channel);
            BankerNoteUnreadRes bankerNoteUnreadRes = (BankerNoteUnreadRes)eaiService.Execute(bankerNoteUnreadReq);
            return bankerNoteUnreadRes;
        }

        /// <summary>
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="GetBankerAppsRes"/>.
        /// </returns>
        //public static GetBankerAppsRes GetBankerApps(
        //    string defaultAU, 
        //    string sessionCorrelationId, 
        //    string applicationID, 
        //    string requestorID, 
        //    string requestorSalesID, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.GetBankerApps.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    GetBankerAppsReq getBankerAppsReq = new GetBankerAppsReq(
        //        defaultAU, 
        //        sessionCorrelationId, 
        //        applicationID, 
        //        requestorID, 
        //        requestorSalesID, 
        //        serviceName, 
        //        applicationName);
        //    GetBankerAppsRes getBankerAppsRes = (GetBankerAppsRes)eaiService.Execute(getBankerAppsReq);

        //    return getBankerAppsRes;
        //}

        /// <summary>
        /// Added By Rammohan Sudham
        /// </summary>
        /// <param name="productTypeCode">
        /// </param>
        /// <param name="locationcode">
        /// </param>
        /// <param name="appId">
        /// </param>
        /// <param name="invokerId">
        /// The invoker Id.
        /// </param>
        /// <param name="billingAU">
        /// </param>
        /// <param name="channel">
        /// </param>
        /// <param name="globalTransactionId">
        /// The global Transaction Id.
        /// </param>
        /// <returns>
        /// The <see cref="TableInquiryRes"/>.
        /// </returns>
        public static TableInquiryRes GetCASTable(
            string productTypeCode, 
            string locationcode, 
            string appId, 
            string invokerId, 
            string billingAU, 
            string channel, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSTABLEINQ.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            TableInquiryReq tableInquiryReq = new TableInquiryReq(
                productTypeCode, locationcode, appId, invokerId, billingAU, channel);
            TableInquiryRes tableInquiryRes = (TableInquiryRes)eaiService.Execute(tableInquiryReq);
            return tableInquiryRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get Alerts/Tasks
        /// </summary>
        /// <param name="applicationId">
        /// The application Id.
        /// </param>
        /// <param name="userLocationCode">
        /// The user Location Code.
        /// </param>
        /// <param name="userId">
        /// The user Id.
        /// </param>
        /// <param name="userSalesId">
        /// The user Sales Id.
        /// </param>
        /// <param name="userAU">
        /// The user AU.
        /// </param>
        /// <param name="pendingValidation">
        /// The pending Validation.
        /// </param>
        /// <param name="checkEligibility">
        /// The check Eligibility.
        /// </param>
        /// <param name="casFields">
        /// The cas Fields.
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="CWSInqRes"/>.
        /// </returns>
        public static CWSInqRes GetCollateralData(
            string applicationId, 
            string userLocationCode, 
            string userId, 
            string userSalesId, 
            string userAU, 
            bool pendingValidation, 
            bool checkEligibility, 
            casField[] casFields, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSCollateralINQ.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            CWSInqReq collateralReq = new CWSInqReq(
                applicationId, 
                userLocationCode, 
                userId, 
                userSalesId, 
                userAU, 
                pendingValidation, 
                checkEligibility, 
                casFields);
            CWSInqRes collateralRes = (CWSInqRes)eaiService.Execute(collateralReq);
            return collateralRes;
        }

        /// <summary>
        /// The get customer information 200909.
        /// </summary>
        /// <param name="defaultAU">
        /// The default au.
        /// </param>
        /// <param name="customerNumber">
        /// The customer number.
        /// </param>
        /// <param name="initiatorId">
        /// The initiator id.
        /// </param>
        /// <param name="initiatorCompanyNumber">
        /// The initiator company number.
        /// </param>
        /// <param name="serviceName">
        /// The service name.
        /// </param>
        /// <param name="applicationName">
        /// The application name.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="GetCustomerInformation200909Res"/>.
        /// </returns>
        public static GetCustomerInformation200909Res GetCustomerInformation200909(
            string defaultAU, 
            string customerNumber, 
            string initiatorId, 
            string initiatorCompanyNumber, 
            string serviceName, 
            string applicationName, 
            string globalTransactionId)
        {
            EAIService eaiService =
                EAIService.getService(
                    Constants.ServiceNameEnum.GetCustomerInformation200909.ToString(), 
                    Constants.ApplicationNameEnum.CAS.ToString(), 
                    globalTransactionId);
            GetCustomerInformation200909Req getCustomerInformationReq = new GetCustomerInformation200909Req(
                defaultAU, customerNumber, initiatorId, initiatorCompanyNumber, serviceName, applicationName);
            GetCustomerInformation200909Res getCustomerInformationRes =
                (GetCustomerInformation200909Res)eaiService.Execute(getCustomerInformationReq);
            return getCustomerInformationRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The GetForm
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="customerNumber">
        /// </param>
        /// <param name="initiatorId">
        /// </param>
        /// <param name="rwsContextType">
        /// </param>
        /// <param name="formId">
        /// </param>
        /// <param name="pdfIndicator">
        /// </param>
        /// <param name="formSummaryIndicator">
        /// </param>
        /// <param name="formContentIndicator">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="GetFormRes"/>.
        /// </returns>
        public static GetFormRes GetForm(
            string defaultAU, 
            string customerNumber, 
            string initiatorId, 
            RWSContext_Type rwsContextType, 
            string formId, 
            bool? pdfIndicator, 
            bool? formSummaryIndicator, 
            bool? formContentIndicator, 
            string serviceName, 
            string applicationName, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.GetForm.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            GetFormReq getFormReq = new GetFormReq(
                defaultAU, 
                customerNumber, 
                initiatorId, 
                rwsContextType, 
                formId, 
                pdfIndicator, 
                formSummaryIndicator, 
                formContentIndicator, 
                serviceName, 
                applicationName);
            GetFormRes getFormRes = (GetFormRes)eaiService.Execute(getFormReq);
            return getFormRes;
        }

        /// <summary>
        /// </summary>
        /// <param name="accountno">
        /// </param>
        /// <param name="rtn">
        /// </param>
        /// <param name="userid">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="MWSRes"/>.
        /// </returns>
        public static MWSRes GetMWS(string accountno, RTN rtn, string userid, string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.MWS.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            MWSReq mwsReq = new MWSReq(accountno, rtn, userid, globalTransactionId);
            MWSRes mwsRes = (MWSRes)eaiService.Execute(mwsReq);
            return mwsRes;
        }

        // Added by Rammohan Sudham
        /// <summary>
        /// </summary>
        /// <param name="channel">
        /// </param>
        /// <param name="userID">
        /// </param>
        /// <param name="locationCode">
        /// </param>
        /// <param name="startDate">
        /// </param>
        /// <param name="EndDate">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="PQOffersRes"/>.
        /// </returns>
        public static PQOffersRes GetPQOffers(
            string channel, 
            string userID, 
            string locationCode, 
            string startDate, 
            string EndDate, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.GetPQOffers.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            PQOffersReq pQOffersReq = new PQOffersReq(channel, userID, locationCode, startDate, EndDate);
            PQOffersRes pQOffersRes = (PQOffersRes)eaiService.Execute(pQOffersReq);
            return pQOffersRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get PRI
        /// </summary>
        /// <param name="appId">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorId">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="requestorStore">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="PRIRes"/>.
        /// </returns>
        public static PRIRes GetPRI(
            string appId, 
            string requestorLoc, 
            string requestorId, 
            string requestorSalesID, 
            string requestorStore, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSPRI.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            PRIReq pRIReq = new PRIReq(appId, requestorLoc, requestorId, requestorSalesID, requestorStore);
            PRIRes pRIRes = (PRIRes)eaiService.Execute(pRIReq);
            return pRIRes;
        }

        /// <summary>
        /// Added By Rammohan Sudham.
        /// </summary>
        /// <param name="appId">
        /// </param>
        /// <param name="invokerId">
        /// </param>
        /// <param name="hris">
        /// </param>
        /// <param name="billingAU">
        /// </param>
        /// <param name="locationId">
        /// </param>
        /// <param name="channel">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="PriceInfoRes"/>.
        /// </returns>
        public static PriceInfoRes GetPriceInfo(
            string appId, 
            string invokerId, 
            string hris, 
            string billingAU, 
            string locationId, 
            string channel, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSPRI.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            PriceInfoReq priceInfoReq = new PriceInfoReq(appId, invokerId, hris, billingAU, locationId, channel);
            PriceInfoRes priceInfoRes = (PriceInfoRes)eaiService.Execute(priceInfoReq);

            return priceInfoRes;
        }

        // Added by Rammohan Sudham.
        /// <summary>
        /// </summary>
        /// <param name="requestMsg">
        /// </param>
        /// <param name="channel">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="SRCHRes"/>.
        /// </returns>
        public static SRCHRes GetSRCH(string requestMsg, string channel, string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.GetSRCH.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            SRCHReq sRCHReq = new SRCHReq(requestMsg, channel);
            SRCHRes sRCHRes = (SRCHRes)eaiService.Execute(sRCHReq);
            return sRCHRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get STD
        /// </summary>
        /// <param name="appId">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorID">
        /// The requestor ID.
        /// </param>
        /// <param name="requestorStore">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <param name="RequestorSaledId">
        /// The Requestor Saled Id.
        /// </param>
        /// <returns>
        /// The <see cref="STDRes"/>.
        /// </returns>
        public static STDRes GetSTD(
            string appId, 
            string requestorLoc, 
            string requestorID, 
            string requestorStore, 
            string globalTransactionId, 
            string RequestorSaledId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSSTD.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            if (RequestorSaledId.Length > 0 && RequestorSaledId.Length < 9)
            {
                RequestorSaledId = RequestorSaledId.PadLeft(9, '0');
            }

            STDReq sTDReq = new STDReq(appId, requestorLoc, requestorID, requestorStore, RequestorSaledId);
            STDRes sTDRes = (STDRes)eaiService.Execute(sTDReq);
            return sTDRes;
        }

        /// <summary>
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="getScenarioInfoReqTypeData">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="GetScenarioInfoRes"/>.
        /// </returns>
        //public static GetScenarioInfoRes GetScenarioInfo(
        //    string defaultAU, 
        //    GetScenarioInfoReq.getScenarioInfoRequest_TypeData getScenarioInfoReqTypeData, 
        //    string sessionCorrelationId, 
        //    string requestorSalesID, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.GetScenarioInfo.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    GetScenarioInfoReq getScenarioInfoReq = new GetScenarioInfoReq(
        //        defaultAU, 
        //        getScenarioInfoReqTypeData, 
        //        sessionCorrelationId, 
        //        requestorSalesID, 
        //        serviceName, 
        //        applicationName);
        //    GetScenarioInfoRes getScenarioInfoRes = (GetScenarioInfoRes)eaiService.Execute(getScenarioInfoReq);

        //    return getScenarioInfoRes;
        //}

        /// <summary>
        /// The get table inquiry.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="region">
        /// The region.
        /// </param>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <param name="acapsFunction">
        /// The acaps function.
        /// </param>
        /// <param name="locationcode">
        /// The locationcode.
        /// </param>
        /// <param name="acapsSession">
        /// The acaps session.
        /// </param>
        /// <param name="applicationSuffix">
        /// The application suffix.
        /// </param>
        /// <param name="panelKey">
        /// The panel key.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="TableInquiryResponse"/>.
        /// </returns>
        public static TableInquiryResponse GetTableInquiry(
            string userId, 
            string password, 
            string region, 
            string appId, 
            string acapsFunction, 
            string locationcode, 
            string acapsSession, 
            string applicationSuffix, 
            string panelKey, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.TableInquiry.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            TableInquiryRequest tableInquiryReq = new TableInquiryRequest(
                userId, password, acapsSession, applicationSuffix, panelKey, acapsFunction, appId, locationcode);
            TableInquiryResponse tableInquiryRes = (TableInquiryResponse)eaiService.Execute(tableInquiryReq);
            return tableInquiryRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get Alerts/Tasks
        /// </summary>
        /// <param name="appId">
        /// </param>
        /// <param name="invokerId">
        /// </param>
        /// <param name="hris">
        /// </param>
        /// <param name="billingAU">
        /// </param>
        /// <param name="locationId">
        /// </param>
        /// <param name="channel">
        /// </param>
        /// <param name="acaps_function">
        /// </param>
        /// <param name="alertKeyList">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="TaskInqRes"/>.
        /// </returns>
        public static TaskInqRes GetTaskInq(
            string appId, 
            string invokerId, 
            string hris, 
            string billingAU, 
            string locationId, 
            string channel, 
            string acaps_function, 
            ArrayList alertKeyList, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.TASKINQ.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            TaskInqReq taskInqReq = new TaskInqReq(
                appId, invokerId, hris, billingAU, locationId, channel, acaps_function, alertKeyList);
            TaskInqRes taskInqRes = (TaskInqRes)eaiService.Execute(taskInqReq);
            return taskInqRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The GetUnreadNotesFlag
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="msgRequestCD">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="GetUnreadNotesFlagRes"/>.
        /// </returns>
        //public static GetUnreadNotesFlagRes GetUnreadNotesFlag(
        //    string defaultAU, 
        //    string msgRequestCD, 
        //    string sessionCorrelationId, 
        //    string applicationID, 
        //    string requestorLoc, 
        //    string requestorID, 
        //    string requestorSalesID, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.GetUnreadNotesFlag.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    GetUnreadNotesFlagReq getunreadNotesFlagReq = new GetUnreadNotesFlagReq(
        //        defaultAU, 
        //        msgRequestCD, 
        //        sessionCorrelationId, 
        //        applicationID, 
        //        requestorLoc, 
        //        requestorID, 
        //        requestorSalesID, 
        //        serviceName, 
        //        applicationName);
        //    GetUnreadNotesFlagRes getunreadNotesFlagRes =
        //        (GetUnreadNotesFlagRes)eaiService.Execute(getunreadNotesFlagReq);
        //    return getunreadNotesFlagRes;
        //}

        /// <summary>
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="GetUpdatableFieldsRes"/>.
        /// </returns>
        //public static GetUpdatableFieldsRes GetUpdatableFields(
        //    string defaultAU, 
        //    string sessionCorrelationId, 
        //    string applicationID, 
        //    string requestorLoc, 
        //    string requestorID, 
        //    string requestorSalesID, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.GetUpdatableFields.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    GetUpdatableFieldsReq getUpdatableFieldsReq = new GetUpdatableFieldsReq(
        //        defaultAU, 
        //        sessionCorrelationId, 
        //        applicationID, 
        //        requestorLoc, 
        //        requestorID, 
        //        requestorSalesID, 
        //        serviceName, 
        //        applicationName);
        //    GetUpdatableFieldsRes getUpdatableFieldsRes =
        //        (GetUpdatableFieldsRes)eaiService.Execute(getUpdatableFieldsReq);

        //    return getUpdatableFieldsRes;
        //}

        /// <summary>
        /// Added by Pardha S Chakka.
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="activityCode">
        /// </param>
        /// <param name="printUserId">
        /// </param>
        /// <param name="printAU">
        /// </param>
        /// <param name="message">
        /// </param>
        /// <param name="description">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="InitiateCDPPrintRes"/>.
        /// </returns>
        //public static InitiateCDPPrintRes InitiateCDPPrint(
        //    string defaultAU, 
        //    string applicationID, 
        //    string activityCode, 
        //    string printUserId, 
        //    string printAU, 
        //    string message, 
        //    string description, 
        //    string sessionCorrelationId, 
        //    string requestorSalesID, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.InitiateCDPPrint.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    InitiateCDPPrintReq getCustomerInformationReq = new InitiateCDPPrintReq(
        //        defaultAU, 
        //        applicationID, 
        //        activityCode, 
        //        printUserId, 
        //        printAU, 
        //        message, 
        //        description, 
        //        sessionCorrelationId, 
        //        requestorSalesID, 
        //        serviceName, 
        //        applicationName);
        //    InitiateCDPPrintRes initiateCDPPrintRes = (InitiateCDPPrintRes)eaiService.Execute(getCustomerInformationReq);
        //    return initiateCDPPrintRes;
        //}

        /// <summary>
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="document">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="LoadStepRes"/>.
        /// </returns>
        //public static LoadStepRes LoadStep(
        //    string defaultAU, 
        //    XmlDocument document, 
        //    string sessionCorrelationId, 
        //    string requestorSalesID, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.LoadStep.ToString(), 
        //        Constants.ApplicationNameEnum.SCS.ToString(), 
        //        globalTransactionId);
        //    LoadStepReq loadStepReq = new LoadStepReq(
        //        defaultAU, document, sessionCorrelationId, requestorSalesID, serviceName, applicationName);
        //    LoadStepRes loadStepRes = (LoadStepRes)eaiService.Execute(loadStepReq);
        //    return loadStepRes;
        //}

        /// <summary>
        /// </summary>
        /// <param name="loginId">
        /// </param>
        /// <param name="token">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="PBAuthenticationRes"/>.
        /// </returns>
        public static PBAuthenticationRes PBAuthenticationService(
            string loginId, string token, string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.PBAuthentication.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            PBAuthenticationReq pBAuthenticationReq = new PBAuthenticationReq(loginId, token);
            PBAuthenticationRes pBAuthenticationRes = (PBAuthenticationRes)eaiService.Execute(pBAuthenticationReq);
            return pBAuthenticationRes;
        }

        /// <summary>
        /// </summary>
        /// <param name="applicationId">
        /// </param>
        /// <param name="activityCode">
        /// </param>
        /// <param name="userId">
        /// </param>
        /// <param name="printAU">
        /// </param>
        /// <param name="description">
        /// </param>
        /// <param name="doctype">
        /// The doctype.
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="PrintCCHRes"/>.
        /// </returns>
        public static PrintCCHRes PrintCChService(
            string applicationId, 
            string activityCode, 
            string userId, 
            string printAU, 
            string description, 
            string doctype, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.PrintCCH.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            PrintCCHReq printCCHReq = new PrintCCHReq(
                applicationId, activityCode, userId, printAU, description, doctype);
            PrintCCHRes printCCHRes = (PrintCCHRes)eaiService.Execute(printCCHReq);
            return printCCHRes;
        }

        /// <summary>
        /// </summary>
        /// <param name="cookieArray">
        /// </param>
        /// <param name="remoteIpAddr">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="SVPAuthenticationRes"/>.
        /// </returns>
        public static SVPAuthenticationRes SVPAuthenticationService(
            string[] cookieArray, string remoteIpAddr, string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.SVPAuthentication.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            SVPAuthenticationReq svpAuthenticationReq = new SVPAuthenticationReq(cookieArray, remoteIpAddr);
            SVPAuthenticationRes svpAuthenticationRes = (SVPAuthenticationRes)eaiService.Execute(svpAuthenticationReq);
            return svpAuthenticationRes;
        }

        /// <summary>
        /// The send cchtoacaps.
        /// </summary>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <param name="activityCode">
        /// The activity code.
        /// </param>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="userAU">
        /// The user au.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="globalTransactionId">
        /// The global transaction id.
        /// </param>
        /// <returns>
        /// The <see cref="CCHToACAPSRes"/>.
        /// </returns>
        public static CCHToACAPSRes SendCCHTOACAPS(
            string applicationId, 
            string activityCode, 
            string userId, 
            string userAU, 
            string description, 
            string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CCHTOACAPS.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            CCHToACAPSReq sendCCHReq = new CCHToACAPSReq(
                applicationId, activityCode, userId, userAU, userAU, description);
            CCHToACAPSRes sendCCHRes = (CCHToACAPSRes)eaiService.Execute(sendCCHReq);
            return sendCCHRes;
        }

        /// Added by Rod Javier
        /// <summary>
        /// The Get Alerts/Tasks
        /// </summary>
        /// <param name="casFields">
        /// The cas Fields.
        /// </param>
        /// <param name="inqResponse">
        /// The inq Response.
        /// </param>
        /// <param name="userSalesId">
        /// The user Sales Id.
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="CWSUpdRes"/>.
        /// </returns>
        public static CWSUpdRes UpdateACAPS(
            casField[] casFields, CwsInqResponse inqResponse, string userSalesId, string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSUPD.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            CWSUpdReq updateReq = new CWSUpdReq(casFields, inqResponse, userSalesId);
            CWSUpdRes updateRes = (CWSUpdRes)eaiService.Execute(updateReq);
            return updateRes;
        }

        /// <summary>
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="casFields">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="UpdateAppDetailsRequestRes"/>.
        /// </returns>
        //public static UpdateAppDetailsRequestRes UpdateAppDetailsRequest(
        //    string defaultAU, 
        //    string sessionCorrelationId, 
        //    string applicationID, 
        //    string requestorLoc, 
        //    string requestorID, 
        //    string requestorSalesID, 
        //    casField[] casFields, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.UpdateAppDetailsRequest.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);
        //    UpdateAppDetailsRequestReq updateAppDetailsRequestReq = new UpdateAppDetailsRequestReq(
        //        defaultAU, 
        //        sessionCorrelationId, 
        //        applicationID, 
        //        requestorLoc, 
        //        requestorID, 
        //        requestorSalesID, 
        //        casFields, 
        //        serviceName, 
        //        applicationName);
        //    UpdateAppDetailsRequestRes updateAppDetailsRequestRes =
        //        (UpdateAppDetailsRequestRes)eaiService.Execute(updateAppDetailsRequestReq);

        //    return updateAppDetailsRequestRes;
        //}

        /// Added by Rod Javier
        /// <summary>
        /// The Get Alerts/Tasks
        /// </summary>
        /// <param name="casFields">
        /// The cas Fields.
        /// </param>
        /// <param name="inqResponse">
        /// The inq Response.
        /// </param>
        /// <param name="userSalesId">
        /// The user Sales Id.
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="CWSUpdRes"/>.
        /// </returns>
        public static CWSUpdRes UpdateCollateralData(
            casField[] casFields, CwsInqResponse inqResponse, string userSalesId, string globalTransactionId)
        {
            EAIService eaiService = EAIService.getService(
                Constants.ServiceNameEnum.CWSCollateralUPD.ToString(), 
                Constants.ApplicationNameEnum.CAS.ToString(), 
                globalTransactionId);
            CWSUpdReq updateReq = new CWSUpdReq(casFields, inqResponse, userSalesId);
            CWSUpdRes updateRes = (CWSUpdRes)eaiService.Execute(updateReq);
            return updateRes;
        }

        /// <summary>
        /// The PerformBankerNotesAction
        /// </summary>
        /// <param name="defaultAU">
        /// </param>
        /// <param name="msgRequestCD">
        /// </param>
        /// <param name="sessionCorrelationId">
        /// </param>
        /// <param name="applicationID">
        /// </param>
        /// <param name="requestorLoc">
        /// </param>
        /// <param name="requestorID">
        /// </param>
        /// <param name="requestorSalesID">
        /// </param>
        /// <param name="bankerNoteActionsRequest">
        /// </param>
        /// <param name="serviceName">
        /// </param>
        /// <param name="applicationName">
        /// </param>
        /// <param name="globalTransactionId">
        /// </param>
        /// <returns>
        /// The <see cref="PerformBankerNoteActionsRes"/>.
        /// </returns>
        //public static PerformBankerNoteActionsRes performBankerNoteActionsReq(
        //    string defaultAU, 
        //    string msgRequestCD, 
        //    string sessionCorrelationId, 
        //    string applicationID, 
        //    string requestorLoc, 
        //    string requestorID, 
        //    string requestorSalesID, 
        //    PerformBankerNoteActionsReq.performBankerNoteActionsRequest_TypeDataCbnt_in_bnote[] bankerNoteActionsRequest, 
        //    string serviceName, 
        //    string applicationName, 
        //    string globalTransactionId)
        //{
        //    EAIService eaiService = EAIService.getService(
        //        Constants.ServiceNameEnum.PerformBankerNoteActions.ToString(), 
        //        Constants.ApplicationNameEnum.CAS.ToString(), 
        //        globalTransactionId);

        //    PerformBankerNoteActionsReq performBankerNoteActionsReq = new PerformBankerNoteActionsReq(
        //        defaultAU, 
        //        msgRequestCD, 
        //        sessionCorrelationId, 
        //        applicationID, 
        //        requestorLoc, 
        //        requestorID, 
        //        requestorSalesID, 
        //        bankerNoteActionsRequest, 
        //        serviceName, 
        //        applicationName);

        //    PerformBankerNoteActionsRes performBankerNoteActionsRes =
        //        (PerformBankerNoteActionsRes)eaiService.Execute(performBankerNoteActionsReq);
        //    return performBankerNoteActionsRes;
        //}

        #endregion
    }
}
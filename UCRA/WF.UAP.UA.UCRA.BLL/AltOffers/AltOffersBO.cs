using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Common.Logging;

using WF.EAI.Data.eFlow.services;
using WF.EAI.Entities.constants;
using WF.EAI.Entities.domain.CMT.AltOffers.Request;
using WF.EAI.Entities.domain.CMT.AltOffers.Response;
using WF.EAI.Model;
using WF.EAI.Model.DTO;
using WF.EAI.Model.DTO.CMT.AltOffers;
using WF.EAI.Model.Lookup;
using WF.EAI.Model.ViewModels.CMT.AltOffers;
using WF.UAP.UASF.CrossCutting.Logging;


namespace WF.EAI.BLL.BO.CMT.AltOffers
{
    using WF.UAP.UASF.CrossCutting.Logging;
    using WF.UAP.UDB.Domain.ORM.dal;
    using WF.UAP.UASF.App.Host.WebApi.Contract;

    /// <summary>
    /// AltOffersBO
    /// </summary>
    public class AltOffersBO
    {
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">request</param>
        /// <returns>AltOffersModel</returns>
        public AltOffersModel Invoke(RequestDto<AltOffersRequestHeader> request)
        {
            #region Initialize objects

            //ILog logger = LogManager.GetCurrentClassLogger();
            var uiModel = new AltOffersModel();
            var offerlst = new List<Offer>();
            if (uiModel.ErrorMessages == null)
                uiModel.ErrorMessages = new List<ErrorMessage>();
            AltOffersRequest altOfferReq = null;
            AltOffersResponse altOfferRes = null;

            #endregion

            #region Request Entity for SIF

            try
            {
                //Async Log
                var sb = new StringBuilder();
                if (request != null && request.RequestHeader != null)
                {
                    sb.Append("<AltOffersHeader><SessionId>" + request.RequestHeader.SessionId + "</SessionId>");
                    sb.Append("<ApplicationName>" + request.RequestHeader.ApplicationName + "</ApplicationName>");
                    sb.Append("<ModuleName>" + request.RequestHeader.ModuleName + "</ModuleName>");
                    sb.Append("<ViewName>" + request.RequestHeader.ViewName + "</ViewName>");
                    sb.Append("<TrackingID>" + request.RequestHeader.TrackingID + "</TrackingID>");
                    sb.Append("<BankerRole>" + request.RequestHeader.BankerRole + "</BankerRole>");
                    sb.Append("<DeliveryChannelCode>" + request.RequestHeader.DeliveryChannelCode +
                              "</DeliveryChannelCode>");
                    sb.Append("<TeamMemberAUNumber>" + request.RequestHeader.TeamMemberAUNumber +
                              "</TeamMemberAUNumber>");
                    sb.Append("<TeamMemberHRISID>" + request.RequestHeader.TeamMemberHRISID + "</TeamMemberHRISID>");
                    sb.Append("<TeamMemberELID>" + request.RequestHeader.TeamMemberELID + "</TeamMemberELID>");
                    sb.Append("<CustNbr>" + request.RequestHeader.CustNbr + "</CustNbr>");
                    sb.Append("<CompanyNumber>" + request.RequestHeader.CompanyNumber + "</CompanyNumber>");
                    sb.Append("<InitiatorCompanyNumber>" + request.RequestHeader.InitiatorCompanyNumber +
                              "</InitiatorCompanyNumber>");
                    sb.Append("<ApplicationId>" + request.RequestHeader.ApplicationId + "</ApplicationId>");
                    sb.Append("<TargetURI>" + request.RequestHeader.TargetURI + "</TargetURI>");
                    sb.Append("<OfferKey>" + request.RequestHeader.OfferKey + "</OfferKey>");
                    sb.Append("<ContactKey>" + request.RequestHeader.ContactKey + "</ContactKey>");
                    sb.Append("<ProductKey>" + request.RequestHeader.ProductKey + "</ProductKey></AltOffersHeader>");
                }
                else
                {
                    //TODO logger.Error("AltOffersBO - Invoke: Request Object is NULL");
                    uiModel.Offers = null;
                    uiModel.ErrorMessages.Add(
                        new ErrorMessage()
                        {
                            Message = "Request Object is NULL",
                            Type = Constants.CustMgmtErrorType.SYSTEM.ToString()
                        });
                    return uiModel;
                }

                var logItem = new LogItem(
                    request.RequestHeader.SessionId,
                    request.RequestHeader.SessionId,
                    request.RequestHeader.ApplicationName.ToString(),
                    request.RequestHeader.ModuleName.ToString(),
                    string.Empty,
                    "AltOffersRequestHeader",
                    LogItem.MSG_TYPE_REQUEST,
                    request.RequestHeader.TargetURI,
                    sb.ToString());
                logItem.setTimeStamp(DateTime.Now);
                AsyncLogger.log(logItem);


                altOfferReq = new AltOffersRequest()
                                  {
                                      defaultAU = request.RequestHeader.TeamMemberAUNumber,
                                      serviceName =
                                          Constants.ServiceNameEnum.GetRelatedProductRecommendation.ToString(),
                                      applicationName = Constants.ApplicationNameEnum.AlterOfferUI.ToString(),
                                      bestOffAltRefIdSource = "ECPR",
                                      bestOffAltRefOfferId = string.Empty,
                                      contactKey = request.RequestHeader.ContactKey,
                                      custKeyCustomerNumber = request.RequestHeader.CustNbr,
                                      customerNumber = request.RequestHeader.CustNbr,
                                      filterPrefComputerAppCode = request.RequestHeader.ApplicationId,
                                      filterPrefComputerDelChannelCodeEnum =
                                          request.RequestHeader.DeliveryChannelCode,
                                      initiatorTeamMemberId = request.RequestHeader.TeamMemberHRISID,
                                      initiatorCompanyNumber = request.RequestHeader.InitiatorCompanyNumber,
                                      initiatorTeamMemberIdType = "HRIS",
                                      locAltRefOutletId = string.Empty,
                                      locAltRefOutletIdType = string.Empty,
                                      initiatorId = request.RequestHeader.TeamMemberHRISID,
                                      productKey = request.RequestHeader.ProductKey,
                                      offerKey=request.RequestHeader.OfferKey
                                  };
            }
            catch (Exception ex)
            {
                //TODO logger.Error("AltOffersBO - Invoke: Error While preparing request Entity for SIF");
                uiModel.Offers = null;
                uiModel.ErrorMessages.Add(
                    new ErrorMessage()
                        {
                            Message = ex.Message,
                            Type = Constants.CustMgmtErrorType.SYSTEM.ToString()
                        });
                return uiModel;
            }

            #endregion

            #region ECBS Factory Call

            try
            {
                ECBSFactory factory;
                switch (request.RequestHeader.ViewName)
                {
                    case AltOffersViewName.Index:
                        factory = new ECBSFactory(Constants.ApplicationNameEnum.AlterOfferUI,
                                                  request.RequestHeader.SessionId);
                        altOfferRes = factory.GetRelatedProductRecommendation(altOfferReq);
                        break;
                    case AltOffersViewName.Offer:
                        factory = new ECBSFactory(Constants.ApplicationNameEnum.AlterOfferUI,
                                                  request.RequestHeader.SessionId);
                        altOfferRes = factory.AddDisposition(altOfferReq);
                        if (altOfferRes.AddDisposition)
                        {
                            uiModel.ErrorMessages = null;
                            uiModel.Offers = null;
                        }
                        else
                        {
                            uiModel.ErrorMessages.Add(
                                    new ErrorMessage()
                                    {
                                        Message = "Alternate offer currently unavailable, please try again later.",
                                        Type = Constants.CustMgmtErrorType.CUSTOM.ToString(),
                                        Code = "error"
                                    });
                        }
                        return uiModel;
                }
            }
            catch (Exception ex)
            {
                uiModel.Offers = null;
                if (uiModel.ErrorMessages != null)
                    uiModel.ErrorMessages.Add(
                        new ErrorMessage()
                            {
                                Message = ex.Message,
                                Type = Constants.CustMgmtErrorType.SYSTEM.ToString()
                            });
                return uiModel;
            }

            if (altOfferRes != null && altOfferRes.FaultList != null && altOfferRes.FaultList.Count > 0)
            {

                //TODO logger.Error("Alternate Offer Response object Received with Fault List in WebAPI-AltOfferBO");
                uiModel.Offers = null;
                uiModel.ErrorMessages.Add(
                    new ErrorMessage()
                    {
                        //Message = "Error retrieving offer content.",
                        Message = "Alternate offer currently unavailable, please try again later.",
                        Type = Constants.CustMgmtErrorType.CUSTOM.ToString(),
                        Code = "error"
                    }
                    );
                return uiModel;
            }

            #endregion

            #region DB Call and Creating New Promotion Content

            using (var eaiStaticEntity = ContextClass.CreateEAI_StaticObjectContext())
            {
                if (altOfferRes != null && altOfferRes.Offers != null)
                {
                    //var newOffer = altOfferRes.Offers.FirstOrDefault(i => i.OfferInformation != null);
                    //newOffer.OfferInformation[0].InformationType = "PINTAPRF";
                    //newOffer.OfferInformation[1].InformationType = "PINTPERD";

                    var invalidCategories = new List<Category>();
                    if (altOfferRes.FaultList == null)
                    {
                        try
                        {
                            foreach (OfferDetails off in altOfferRes.Offers)
                            {
                                var promoContent =
                                    eaiStaticEntity.GetAltOffersPromotionContent(off.OfferPromotionID).ToList();
                                //List only those offers which has content
                                if (promoContent.Count > 0)
                                {
                                    var offerObj = new Offer
                                                       {
                                                           IsBestoffer = off.IsBestOffer,
                                                           OfferName = off.OfferName,
                                                           PromotionID = off.OfferPromotionID,
                                                           OfferId = off.OfferKeyID,
                                                           ProductId = off.ProductID
                                                       };
                                    var categories = promoContent.Select(promoResult => new Category()
                                                                                            {
                                                                                                CategoryId =
                                                                                                    promoResult.
                                                                                                    CategoryId,
                                                                                                CategoryName =
                                                                                                    promoResult.
                                                                                                    CategoryName,
                                                                                                PromotionContent =
                                                                                                    promoResult.
                                                                                                    PromotionContent
                                                                                            }).ToList();
                                    offerObj.Categories = categories;

                                    //All categories which has Info Type in Content
                                    var catContent =
                                        offerObj.Categories.Where(
                                            i => i.PromotionContent.Contains("{"))
                                            .ToList();

                                    if (catContent.Count > 0)
                                    {
                                        if (off.OfferInformation != null)
                                        {
                                            foreach (var category in catContent)
                                            {
                                                if (!string.IsNullOrEmpty(category.PromotionContent))
                                                {
                                                    //To handle one OR multiple occurance of InfoType in one Promotion content
                                                    var origContent = category.PromotionContent;
                                                    var idxOf1St = AllIndexesOf(origContent, "{");
                                                    var idxOfNth = AllIndexesOf(origContent, "}");
                                                    var attr = new List<string>();
                                                    if (idxOf1St.Count > 0 && idxOfNth.Count > 0 &&
                                                        idxOf1St.Count == idxOfNth.Count)
                                                        attr.AddRange(
                                                            idxOf1St.Select(
                                                                (t, i) =>
                                                                origContent.Substring(t + 1, ((idxOfNth[i]) - (t)) - 1)));

                                                    var totalOccurance = 0;
                                                    foreach (var offerInfo in off.OfferInformation)
                                                    {
                                                        var info = offerInfo;
                                                        foreach (
                                                            var str in
                                                                attr.Where(
                                                                    i =>
                                                                    i.ToString(CultureInfo.InvariantCulture) ==
                                                                    info.InformationType))
                                                        {
                                                            totalOccurance++;
                                                            category.PromotionContent =
                                                                category.PromotionContent.Replace(str,
                                                                                                  info.InformationValue);
                                                        }
                                                    }
                                                    //Total number of Info Type must match with DB and in ECPR response,
                                                    //If it's not matching add those categories in invalid category list
                                                    if (totalOccurance != attr.Count)
                                                        invalidCategories.Add(category);
                                                    else
                                                    {
                                                        category.PromotionContent =
                                                            category.PromotionContent.Replace("{", string.Empty);
                                                        category.PromotionContent =
                                                            category.PromotionContent.Replace("}", string.Empty);
                                                    }
                                                }
                                            }
                                        }
                                            //If there is no Info type returned by ECPR and Promotion Content has Info types in DB, 
                                            //then remove all those categories
                                        else if (catContent.Count > 0)
                                            invalidCategories.AddRange(catContent);
                                    }
                                    offerlst.Add(offerObj);
                                }
                                else
                                {
                                    //TODO logger.Error("There is no Content found in DB for Promotion ID:" + off.OfferPromotionID + " - AltOfferBO");
                                    uiModel.Offers = null;
                                    uiModel.ErrorMessages.Add(
                                        new ErrorMessage()
                                        {
                                            //Message = "Error retrieving offer content.",
                                            Message = "Alternate offer currently unavailable, please try again later.",
                                            Type = Constants.CustMgmtErrorType.CUSTOM.ToString(),
                                            Code = "error"
                                        }
                                        );
                                    return uiModel;
                                }
                            }

                            //Consolidate all Categories
                            var cTable = new Hashtable();
                            foreach (var offer in offerlst)
                            {
                                foreach (var ct in offer.Categories)
                                {
                                    if (!cTable.ContainsKey(ct.CategoryId))
                                        cTable.Add(ct.CategoryId, ct.CategoryName);
                                }
                            }

                            //Adding missing categories in each offer to balance categories.
                            foreach (DictionaryEntry c in cTable)
                            {
                                foreach (var o in offerlst)
                                {
                                    if (o.Categories != null && !o.Categories.Exists(i => i.CategoryId == (int)c.Key))
                                    {
                                            
                                        //All Missing Categories should go to Invalid Categories.
                                        if (!invalidCategories.Exists(i => i.CategoryId == (int)c.Key))
                                        {
                                            invalidCategories.Add(new Category()
                                                            {
                                                                CategoryId = (int)c.Key,
                                                                CategoryName = c.Value.ToString(),
                                                                PromotionContent = string.Empty
                                                            }
                                                            );
                                        }
                                    }
                                }
                            }

                            //Remove Invalid Categories.
                            foreach (var offer in offerlst)
                            {
                                foreach (var invalidCategory in invalidCategories)
                                {
                                    var cats = offer.Categories.FirstOrDefault(i => i.CategoryId == invalidCategory.CategoryId);
                                    offer.Categories.Remove(cats);
                                }
                            }

                            //If all categories deleted OR offers count is less than or equal to 1, then we don't have any offers to compare, sending error back.
                            var numOfoffers = offerlst.Where(i => i.Categories.Count > 0).ToList();
                            if (numOfoffers.Count <= 1)
                            {
                                //TODO logger.Error("There is no offer found which has categories - AltOfferBO");
                                uiModel.Offers = null;
                                uiModel.ErrorMessages.Add(
                                    new ErrorMessage()
                                    {
                                        //Message = "Error retrieving offer content.",
                                        Message = "Alternate offer currently unavailable, please try again later.",
                                        Type = Constants.CustMgmtErrorType.CUSTOM.ToString(),
                                        Code = "error"
                                    }
                                    );
                                return uiModel;
                            }
                            uiModel.Offers = offerlst;
                        }
                        catch (Exception ex)
                        {
                            uiModel.Offers = null;
                            uiModel.ErrorMessages.Add(
                                new ErrorMessage()
                                    {
                                        Message = ex.Message,
                                        Type = Constants.CustMgmtErrorType.SYSTEM.ToString()
                                    });
                            return uiModel;
                        }
                    }
                    else
                    {
                        uiModel.Offers = null;
                        uiModel.ErrorMessages.Add(
                            new ErrorMessage()
                            {
                                //Message = "Error retrieving offer content.",
                                Message = "Alternate offer currently unavailable, please try again later.",
                                Type = Constants.CustMgmtErrorType.CUSTOM.ToString(),
                                Code = "error"
                            }
                            );
                        return uiModel;
                    }
                }
                else
                {
                    uiModel.Offers = null;
                    uiModel.ErrorMessages.Add(
                        new ErrorMessage()
                            {
                                //Message = "Error retrieving offer content.",
                                Message = "Alternate offer currently unavailable, please try again later.",
                                Type = Constants.CustMgmtErrorType.CUSTOM.ToString(),
                                Code="error"
                            }
                        );
                    return uiModel;
                }
            }

            #endregion

            return uiModel;
        }

        internal static List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            var indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index, System.StringComparison.Ordinal);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}

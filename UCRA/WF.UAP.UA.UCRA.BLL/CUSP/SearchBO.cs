// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchBO.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SearchBO type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WF.EAI.BLL.CUSP
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using WF.EAI.Data.sif;
    using WF.EAI.BLL.CUSP.Parsers;
    using WF.EAI.Entities.domain;
    using WF.EAI.Entities.domain.c2c.Common;
    using WF.EAI.Entities.domain.c2c.Core;
    using WF.EAI.Entities.domain.c2c.UI;
    using WF.EAI.Entities.exception;
    using WF.EAI.Utils;
    using WF.EAI.Data.eFlow.services;
    using WF.EAI.Entities.constants;

    using System.Threading;

    using WF.UAP.UASF.CrossCutting.Logging;
    using WF.UAP.UDB.Repository.Transform.dal.UAA.CUSP;
    using WF.UAP.UDB.Repository.Transform.sif.Factory.ACAPS;

    /// <summary>
    ///     The search bo.
    /// </summary>
    public class SearchBO
    {
        #region Public Methods and Operators

        /// <summary>
        /// The bureau search fault list.
        /// </summary>
        /// <param name="faultList">
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<AcapsMessage> GetFaultList(EAIServiceRes.FaultList faultList)
        {
            return
                faultList.FaultLst.Select(
                    fault => new AcapsMessage { Type = fault.FaultType.ToString(), Message = fault.FaultDesc }).ToList();
        }

        // Work A list Fault List
        /// <summary>
        /// The get worka list fault list.
        /// </summary>
        /// <param name="faultList">
        /// The fault list.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<AcapsMessage> GetWorkaListFaultList(EAIServiceRes.FaultList faultList)
        {
            var messageList = new List<AcapsMessage>();

            if (faultList.FaultLst != null)
            {
                messageList.AddRange(
                    faultList.FaultLst.Select(
                        fault => new AcapsMessage { Type = fault.FaultType.ToString(), Message = fault.FaultDesc }));
            }

            return messageList;
        }

        /// <summary>
        /// The get work a list search data.
        /// </summary>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <param name="region">
        /// The region.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="workState">
        /// The work state.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="BusinessLayerException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public List<WorkAListSearchEntity> GetWorkAListSearchData(
            AppDataHeader header, string region, string location, string workState, string id, string sessionId)
        {
            var applicationData = new ApplicationData { AppDataHeader = header };
            var workAListRes = Invoker.WorkAListSearch(
                applicationData.AppDataHeader.UserId, 
                applicationData.AppDataHeader.Password, 
                string.Empty, 
                location, 
                workState, 
                id, 
                sessionId, 
                applicationData.AppDataHeader.SessionId);

            var workAListsearchEntityList = new List<WorkAListSearchEntity>();
            if (workAListRes != null && workAListRes.WorkAListData.message != null
                && Convert.ToInt32(workAListRes.WorkAListData.message[0].totalMessages) == 0)
            {
                var workAListSearchEntity = new WorkAListSearchEntity
                                                {
                                                    ApplicationId =
                                                        workAListRes.WorkAListData.applicationID
                                                                    .Trim(), 
                                                    AcapsSession =
                                                        workAListRes.WorkAListData.acaps_session
                                                };
                workAListsearchEntityList.Add(workAListSearchEntity);
            }
            else
            {
                if (workAListRes != null)
                {
                    applicationData = InqResponse.ParseError(workAListRes.resXmlStr, applicationData);
                    var acapsMessages = GetWorkaListFaultList(workAListRes.FaultLst);
                }

                if (applicationData.AcapsMessage != null && applicationData.AcapsMessage.Count > 0)
                {
                    var errorList =
                        applicationData.AcapsMessage.Select(
                            msg => new AcapsMessage { Message = msg.Message, Code = msg.Code, Type = msg.Type })
                                       .ToList();
                    throw new BusinessLayerException { AcapsMessages = errorList };
                }

                // else
                // {
                // throw new BusinessLayerException("WorkAListSearchResponse from SIF is null");
                // }
            }

            return workAListsearchEntityList;
        }

        /// <summary>
        /// The get work list search data.
        /// </summary>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <param name="region">
        /// The region.
        /// </param>
        /// <param name="location">
        /// The location.
        /// </param>
        /// <param name="workState">
        /// The work state.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="BusinessLayerException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public List<WorkListSearchEntity> GetWorkListSearchData(
            AppDataHeader header, string region, string location, string workState, string id, string sessionId)
        {
            var workListRes = Invoker.WorkListSearch(
                header.UserId, header.Password, string.Empty, location, workState, id, header.SessionId);
            var workListsearchEntityList = new List<WorkListSearchEntity>();
            if (workListRes != null)
            {
                if (workListRes.IsOK())
                {
                    for (var i = 0; i <= workListRes.WorkListSearchResults.Count - 1; i++)
                    {
                        var workListSearchEntity = new WorkListSearchEntity
                                                       {
                                                           StateCode =
                                                               workListRes.WorkListSearchResults[i]
                                                               .StateCode, 
                                                           StateName =
                                                               workListRes.WorkListSearchResults[i]
                                                               .StateName, 
                                                           UserId =
                                                               workListRes.WorkListSearchResults[i]
                                                               .UserId, 
                                                           UserName =
                                                               workListRes.WorkListSearchResults[i]
                                                               .UserName, 
                                                           CarryOverCnt =
                                                               workListRes.WorkListSearchResults[i]
                                                               .CarryOverCnt, 
                                                           NewEntryCnt =
                                                               workListRes.WorkListSearchResults[i]
                                                               .NewEntryCnt, 
                                                           TotalListCnt =
                                                               workListRes.WorkListSearchResults[i]
                                                               .TotalListCnt, 
                                                           WorkedCnt =
                                                               workListRes.WorkListSearchResults[i]
                                                               .WorkedCnt, 
                                                           PendingCnt =
                                                               workListRes.WorkListSearchResults[i]
                                                               .PendingCnt
                                                       };
                        workListsearchEntityList.Add(workListSearchEntity);
                    }
                }
                else
                {
                    var acapsMessages = GetFaultList(workListRes.FaultLst);
                    if (acapsMessages != null)
                    {
                        throw new BusinessLayerException { AcapsMessages = acapsMessages };
                    }
                }
            }
            else
            {
                throw new BusinessLayerException("WorkListSearchResponse from SIF is null");
            }

            return workListsearchEntityList;
        }


        public static WF.EAI.Entities.domain.uca.Common.AppDataHeader RetailerEsignatureSearch(string acctNum, string sessionId)
        { 
             CUSPDBHelper dbHelper = new CUSPDBHelper();
             WF.EAI.Entities.domain.uca.Common.AppDataHeader appHeader = new Entities.domain.uca.Common.AppDataHeader();
             WF.EAI.Entities.domain.cusp.Search.EsignatureEntity esignEntity = null;
          try{ 

              //retrieve from DB. if no record , drop into Unisys Queue and poll the DB for 15 sec for reply and then break.
              if (acctNum != string.Empty)
              {                  
                  acctNum = acctNum.Trim().TrimStart(new char[] { '0' });
                  //Logger.Instance.Info("acctNum: " + acctNum + " SessionId:" + sessionId);
                  esignEntity = dbHelper.GetESignature(acctNum);

                  if (esignEntity != null && (!string.IsNullOrEmpty(esignEntity.errorMessage) || !string.IsNullOrEmpty(esignEntity.signatureString)))
                  {

                      string jsonText = JsonMapper<string>.ObjectToJsonText(esignEntity);

                      appHeader.TabDataText = jsonText;

                  }
                  else
                  {
                      //Logger.Instance.Info("dropping into Unisys queue");

                      ESignatureFactory factory = new ESignatureFactory(Constants.ServiceNameEnum.GetRetailerSignature, Constants.ApplicationNameEnum.Retailer);
                      var getESignatureRes = factory.SendToUnisysMQ(acctNum, sessionId);


                      if (!getESignatureRes.IsOK())
                      {
                          if (getESignatureRes.FaultLst.FaultLst.Any())
                              //Logger.Instance.Error("Error connecting to ESignature Queue " + getESignatureRes.FaultLst.FaultLst[0].FaultCode + " " + getESignatureRes.FaultLst.FaultLst[0].FaultDesc);
                          appHeader.Exception = "Signature lookup service is unavailable at this time, please try again later";
                          appHeader.TabDataText = string.Empty;

                      }

                      else
                      {
                          esignEntity = new Entities.domain.cusp.Search.EsignatureEntity();
                          if (acctNum.Length == 10)
                              esignEntity.unisysAppId = acctNum;
                          else
                              esignEntity.fdrAcctNumber = acctNum;

                          //Allow some processing time for the responseProcessor to process the MQ message and insert into database. 

                          Thread.Sleep(3000);
                          // dbHelper.SaveESignature(esignEntity, Constants.ApplicationNameEnum.Retailer.ToString());

                          //Logger.Instance.Info("start polling database");
                          int EsignatureRetrievalTimeOut =
                                  int.Parse(
                                    ConfigurationManager.AppSettings["EsignatureRetrievalTimeOut"].ToString());

                          bool replynotfound = dbHelper.pollforEsignature(acctNum, EsignatureRetrievalTimeOut, ref  esignEntity);

                          if (!replynotfound) // got esign or errormessage back from Unisys
                          {
                              appHeader.TabDataText = JsonMapper<Entities.domain.cusp.Search.EsignatureEntity>.ObjectToJsonText(esignEntity);
                          }
                          else
                          {
                              appHeader.Exception = "We had a problem retrieving your signature image, please try again";
                              appHeader.TabDataText = string.Empty;

                          }



                      }
                  }
              }
              else
              {
                  appHeader.Exception = "Acct Num cannot be empty";
                  appHeader.TabDataText = string.Empty; 
              }
            }
            catch (Exception ex)
            {
                appHeader.Exception = "Exception in Getting ESignature";
                appHeader.TabDataText = string.Empty;
                //Logger.Instance.Error("Exception in Getting ESignature" + ex.Message);
            }

            return appHeader;
            //return JsonMapper<string>.ObjectToJsonText(appHeader);
            
            
        }

        #endregion
    }
}
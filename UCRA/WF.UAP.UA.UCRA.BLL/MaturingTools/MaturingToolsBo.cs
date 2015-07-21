using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using WF.EAI.BLL.BO.CUSPApps.MaturingTools.Interfaces;
using WF.EAI.Data.eFlow.services.CSTS;
using WF.EAI.Data.eFlow.services.EmailDelivery;
using WF.EAI.Entities.constants;
using WF.EAI.Entities.constants.eiep;
using WF.EAI.Entities.domain.MaturingTools;
using WF.UAP.UASF.CrossCutting.ConfigMgmt.config;

namespace WF.EAI.BLL.BO.CUSPApps.MaturingTools
{
    using WF.UAP.UASF.CrossCutting.ConfigMgmt.config.Global;
    using WF.UAP.UASF.CrossCutting.Logging;
    using WF.UAP.UASF.Utils.Patterns.Repository;
    using WF.UAP.UASF.Utils.Patterns.Repository.Core;
    using WF.UAP.UDB.Repository.Transform.dal.UAA.CUSP;

    public class MaturingToolsBo : IMaturingToolsBo
    {
        public MaturingToolsEntity GetMaturingToolsData(string accountNum, Guid? moGuid, string userId, string userType, string mloId, string agentName)
        {
            var cuspdbHelper = new CUSPDBHelper();
            var result = new MaturingToolsEntity();
            try
            {
                if (!string.IsNullOrEmpty(accountNum))
                {
                    result.AccountNum = accountNum;
                    List<MaturingToolsEntity> maturingToolsEntities = cuspdbHelper.GetMaturingOptionsData(accountNum, moGuid,
                        userId);
                    if (maturingToolsEntities != null && maturingToolsEntities.Count > 0)
                    {
                        result = maturingToolsEntities.FirstOrDefault();
                    }
                    else
                    {
                        //Make ELPS Call only for HEMAP users, Show account not found for OSU users
                        if (userType.ToUpper().Equals(UserType.HEMAP.ToString().ToUpper()))
                        {
                            result = MapElpsCallData(accountNum);
                        }
                        else if (userType.ToUpper().Equals(UserType.OSU.ToString().ToUpper()))
                        {
                            result.ErrorMessage = "Account not found";
                        }
                    }
                }
                result.UserType = userType;
                result.CreatedBy = userId;
                if (userType.ToUpper().Equals(UserType.HEMAP.ToString().ToUpper()))
                {
                    //Set agent name and mloId only if HEMAP user, for OSU user it is set from database
                    result.AgentName = agentName;
                    result.MNLSRID = mloId;

                    //Populate Buttons
                    var btnSave = new UIButtons();
                    btnSave.Label = "Save";
                    btnSave.Status = HEMapStatus.Saved.ToString();
                    btnSave.IsHidden = false;
                    btnSave.UIActionName = "UI." + btnSave.Label;

                    result.BtnSave = btnSave;

                    var btnRetrieve = new UIButtons();
                    btnRetrieve.Label = "Retrieve";
                    btnRetrieve.Status = "";
                    btnRetrieve.IsHidden = false;
                    btnRetrieve.UIActionName = "UI." + btnRetrieve.Label;

                    result.BtnRetrieve = btnRetrieve;

                    var btnCancel = new UIButtons();
                    btnCancel.Label = "Cancel";
                    btnCancel.Status = "";
                    btnCancel.IsHidden = false;
                    btnCancel.UIActionName = "UI." + btnCancel.Label;

                    result.BtnCancel = btnCancel;

                    var btnSubmitForProcessing = new UIButtons();
                    btnSubmitForProcessing.Label = "Submit For Review";
                    btnSubmitForProcessing.Status = HEMapStatus.ForReview.ToString();
                    btnSubmitForProcessing.IsHidden = false;
                    btnSubmitForProcessing.UIActionName = "UI." + btnSubmitForProcessing.Label;

                    result.BtnSubmitForProcessing = btnSubmitForProcessing;

                    //First time login, disable all buttons except retrieve
                    if (string.IsNullOrEmpty(accountNum))
                    {
                        btnSave.IsDisabled = true;
                        btnRetrieve.IsDisabled = false;
                        btnCancel.IsDisabled = true;
                        btnSubmitForProcessing.IsDisabled = true;
                    }
                    else if (result.IsDuplicateOK == 1)
                    {
                        //can only be copied
                        btnSave.IsDisabled = false;
                        btnSave.Label = "Copy";
                        btnSave.Status = HEMapStatus.Saved.ToString();
                        btnSave.UIActionName = "UI." + btnSave.Label;
                        btnRetrieve.IsDisabled = true;
                        btnCancel.IsDisabled = true;
                        btnSubmitForProcessing.IsDisabled = true;
                    }
                    else if (result.IsEditable == false)
                    {
                        //Disable all buttons except cancel to reset the values
                        btnSave.IsDisabled = true;
                        btnRetrieve.IsDisabled = true;
                        btnCancel.IsDisabled = false;
                        btnSubmitForProcessing.IsDisabled = true;
                    }
                    else if (result.IsEditable == true)
                    {
                        //Disable retrieve button and enable all other buttons
                        btnSave.IsDisabled = false;
                        btnRetrieve.IsDisabled = true;
                        btnCancel.IsDisabled = false;
                        btnSubmitForProcessing.IsDisabled = false;
                    }
                }
                else if (userType.ToUpper().Equals(UserType.OSU.ToString().ToUpper()))
                {
                    //Populate Buttons
                    var btnSave = new UIButtons();
                    btnSave.Label = "Pending QC";
                    btnSave.Status = OSUStatus.ForQC.ToString();
                    btnSave.IsHidden = false;

                    result.BtnSave = btnSave;

                    var btnRetrieve = new UIButtons();
                    btnRetrieve.Label = "Retrieve";
                    btnRetrieve.Status = "";
                    btnRetrieve.IsHidden = false;

                    result.BtnRetrieve = btnRetrieve;

                    var btnCancel = new UIButtons();
                    btnCancel.Label = "Cancel";
                    btnCancel.Status = OSUStatus.Voided.ToString();
                    btnCancel.IsHidden = false;

                    result.BtnCancel = btnCancel;

                    var btnSubmitForProcessing = new UIButtons();
                    btnSubmitForProcessing.Label = result.IsEmail ? "Send Email" : "Send Letter";
                    btnSubmitForProcessing.Status = result.IsEmail ? OSUStatus.Delivered.ToString() : OSUStatus.ForDelivery.ToString();
                    btnSubmitForProcessing.IsHidden = false;

                    result.BtnSubmitForProcessing = btnSubmitForProcessing;

                    //First time login, disable all buttons except retrieve
                    if (string.IsNullOrEmpty(accountNum))
                    {
                        btnSave.IsDisabled = true;
                        btnRetrieve.IsDisabled = false;
                        btnCancel.IsDisabled = true;
                        btnSubmitForProcessing.IsDisabled = true;
                    }
                    else if (result.IsDuplicateOK == 1)
                    {
                        //disable all buttons except retrieve - OSU users cannot create a new account
                        btnSave.IsDisabled = true;
                        btnRetrieve.IsDisabled = false;
                        btnCancel.IsDisabled = true;
                        btnSubmitForProcessing.IsDisabled = true;
                    }
                    else if (result.IsEditable == false)
                    {
                        //cannot edit any values - disable all buttons except retrieve
                        btnSave.IsDisabled = true;
                        btnRetrieve.IsDisabled = false;
                        btnCancel.IsDisabled = true;
                        btnSubmitForProcessing.IsDisabled = true;
                    }
                    else if (result.IsEditable == true && result.MOStatus.ToLower() == HEMapStatus.Saved.ToString().ToLower())
                    {
                        btnSave.IsDisabled = true;
                        btnRetrieve.IsDisabled = false;
                        btnCancel.IsDisabled = true;
                        btnSubmitForProcessing.IsDisabled = true;
                    }
                    else if (result.IsEditable == true && result.MOStatus.ToLower() == OSUStatus.ForDelivery.ToString().ToLower())
                    {
                        btnSave.IsDisabled = true;
                        btnRetrieve.IsDisabled = false;
                        btnCancel.IsDisabled = true;
                        btnSubmitForProcessing.IsDisabled = true;
                    }
                    else if (result.IsEditable == true)
                    {
                        //Disable retrieve button and enable all other buttons
                        btnSave.IsDisabled = false;
                        btnRetrieve.IsDisabled = true;
                        btnCancel.IsDisabled = false;

                        //Change the label based on Email Option
                        btnSubmitForProcessing.Label = result.IsEmail ? "Send Email" : "Send Letter";
                        btnSubmitForProcessing.Status = result.IsEmail ? OSUStatus.Delivered.ToString() : OSUStatus.ForDelivery.ToString();
                        btnSubmitForProcessing.IsDisabled = (result.MOStatus == null ||
                                                           result.MOStatus.ToUpper() !=
                                                           OSUStatus.ForQC.ToString().ToUpper());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception in GetMaturingToolsData:" + ex.Message);
                result.ErrorMessage = "Could not retrieve application";
            }
            //if there is an error while retrieving data - disable all buttons except retrieve
            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                result.BtnSave.IsDisabled = true;
                result.BtnRetrieve.IsDisabled = false;
                result.BtnCancel.IsDisabled = true;
                result.BtnSubmitForProcessing.IsDisabled = true;
            }

            return result;
        }

        private MaturingToolsEntity MapElpsCallData(string accountNum)
        {
            //Call Elps
            Guid guid = Guid.NewGuid();
            var elpsFactory = new ELPSFactory(Constants.ApplicationNameEnum.CuspAppWebAPI, guid.ToString());
            var elspAccountInfoResponse = elpsFactory.GetAccountInfo(accountNum);


            var result = new MaturingToolsEntity();
            if (elspAccountInfoResponse != null)
            {
                try
                {
                    result.MOGUID = guid;
                    result.AccountNum = elspAccountInfoResponse.AccountNumber;
                    //set isduplicate to 0 as this is a new account - not in database
                    result.IsDuplicateOK = 0;
                    result.IsEditable = true;
                    if (elspAccountInfoResponse.ProductCode != null)
                    {
                        string prodCode = elspAccountInfoResponse.ProductCode;
                        result.ProductCode = prodCode;
                        if (prodCode.ToUpper().Equals("LCA"))
                        {
                            if (elspAccountInfoResponse.lineOfCredit != null &&
                                elspAccountInfoResponse.lineOfCredit.PLCI2 != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCI2.LoanTypeIndicator) &&
                                !elspAccountInfoResponse.lineOfCredit.PLCI2.LoanTypeIndicator.Equals("8"))
                            {
                                result.AccountType = "BalloonLoan";
                            }
                            else if (elspAccountInfoResponse.lineOfCredit != null &&
                                     elspAccountInfoResponse.lineOfCredit.PLCI2 != null &&
                                     !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCI2.LoanTypeIndicator) &&
                                     elspAccountInfoResponse.lineOfCredit.PLCIQ != null &&
                                     elspAccountInfoResponse.lineOfCredit.PLCI2 != null &&
                                     !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCIQ.DateLoanMatures) &&
                                     !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCI2.DrawPeriodEndDate) &&
                                     elspAccountInfoResponse.lineOfCredit.PLCI2.LoanTypeIndicator.Equals("8") &&
                                     (elspAccountInfoResponse.lineOfCredit.PLCIQ.DateLoanMatures ==
                                      elspAccountInfoResponse.lineOfCredit.PLCI2.DrawPeriodEndDate))
                            {
                                result.AccountType = "BalloonLine";
                            }
                            else
                            {
                                result.AccountType = "EOD";
                            }
                        }
                        else if (prodCode.ToUpper().Equals("ILA"))
                        {
                            if (elspAccountInfoResponse.loan != null &&
                                elspAccountInfoResponse.loan.PIL22 != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PIL22.LoanTypeIndicator) &&
                                !elspAccountInfoResponse.loan.PIL22.LoanTypeIndicator.Equals("8"))
                            {
                                result.AccountType = "BalloonLoan";
                            }
                            else if (elspAccountInfoResponse.loan != null &&
                                     elspAccountInfoResponse.loan.PIL22 != null &&
                                     !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PIL22.LoanTypeIndicator) &&
                                     elspAccountInfoResponse.loan.PIL22.LoanTypeIndicator.Equals("8") &&
                                     elspAccountInfoResponse.loan.PIL2Q != null &&
                                     !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PIL2Q.DateLoanMatures) &&
                                     elspAccountInfoResponse.loan.PILI5 != null &&
                                     !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PILI5.EndOfDrawDate) &&
                                     (elspAccountInfoResponse.loan.PIL2Q.DateLoanMatures ==
                                      elspAccountInfoResponse.loan.PILI5.EndOfDrawDate))
                            {
                                result.AccountType = "BalloonLine";
                            }
                            else
                            {
                                result.AccountType = "EOD";
                            }
                        }

                        //Assign EODDate only for accountType EOD
                        if (result.AccountType.ToUpper().Equals("EOD"))
                        {
                            if (prodCode.ToUpper().Equals("LCA"))
                            {
                                if (elspAccountInfoResponse.lineOfCredit != null &&
                                    elspAccountInfoResponse.lineOfCredit.PLCI2 != null &&
                                    !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCI2.DrawPeriodEndDate))
                                {
                                    DateTime eodDate;
                                    if (
                                        DateTime.TryParseExact(
                                            elspAccountInfoResponse.lineOfCredit.PLCI2.DrawPeriodEndDate, "yyyyMMdd",
                                            CultureInfo.InvariantCulture, DateTimeStyles.None, out eodDate))
                                        result.EndOfDrawDate = eodDate.ToString("MM/dd/yyyy");
                                }
                            }
                            else if (prodCode.ToUpper().Equals("ILA"))
                            {
                                if (elspAccountInfoResponse.loan != null && elspAccountInfoResponse.loan.PILI5 != null &&
                                    !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PILI5.EndOfDrawDate))
                                {
                                    DateTime eodDate;
                                    if (DateTime.TryParseExact(elspAccountInfoResponse.loan.PILI5.EndOfDrawDate,
                                        "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out eodDate)) ;
                                    result.EndOfDrawDate = eodDate.ToString("MM/dd/yyyy");
                                }
                            }
                        }

                        if (prodCode.ToUpper().Equals("LCA"))
                        {
                            if (elspAccountInfoResponse.lineOfCredit != null &&
                                elspAccountInfoResponse.lineOfCredit.PLCIQ != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCIQ.DateLoanMatures))
                            {
                                DateTime maturityDate;
                                if (DateTime.TryParseExact(elspAccountInfoResponse.lineOfCredit.PLCIQ.DateLoanMatures, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                                    out maturityDate))
                                {
                                    if (result.AccountType.ToUpper().Equals("BALLOONLOAN"))
                                    {
                                        result.BLoanMaturityDate = maturityDate.ToString("MM/dd/yyyy");
                                    }
                                    else if (result.AccountType.ToUpper().Equals("BALLOONLINE"))
                                    {
                                        result.BLineMaturityDate = maturityDate.ToString("MM/dd/yyyy");
                                    }
                                }
                            }
                        }
                        else if (prodCode.ToUpper().Equals("ILA"))
                        {
                            if (elspAccountInfoResponse.loan != null && elspAccountInfoResponse.loan.PIL2Q != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PIL2Q.DateLoanMatures))
                            {
                                DateTime maturityDate;
                                if (DateTime.TryParseExact(elspAccountInfoResponse.loan.PIL2Q.DateLoanMatures, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                                    out maturityDate))
                                {
                                    if (result.AccountType.ToUpper().Equals("BALLOONLOAN"))
                                    {
                                        result.BLoanMaturityDate = maturityDate.ToString("MM/dd/yyyy");
                                    }
                                    else if (result.AccountType.ToUpper().Equals("BALLOONLINE"))
                                    {
                                        result.BLineMaturityDate = maturityDate.ToString("MM/dd/yyyy");
                                    }
                                }
                            }
                        }

                        if (result.AccountType.ToUpper().Equals("EOD"))
                        {
                            if (prodCode.ToUpper().Equals("LCA"))
                            {
                                if (elspAccountInfoResponse.lineOfCredit != null &&
                                    elspAccountInfoResponse.lineOfCredit.PLCI6 != null &&
                                    !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCI6.DispositionCode))
                                {
                                    result.DispositionCode = elspAccountInfoResponse.lineOfCredit.PLCI6.DispositionCode;
                                }
                            }
                            else if (prodCode.ToUpper().Equals("ILA"))
                            {
                                if (elspAccountInfoResponse.loan != null && elspAccountInfoResponse.loan.PILI6 != null &&
                                    !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PILI6.DispositionCode))
                                {
                                    result.DispositionCode = elspAccountInfoResponse.loan.PILI6.DispositionCode;
                                }
                            }
                        }

                        if (prodCode.ToUpper().Equals("LCA"))
                        {
                            if (elspAccountInfoResponse.lineOfCredit != null &&
                                elspAccountInfoResponse.lineOfCredit.PLCIQ != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCIQ.LoanBalance))
                            {
                                decimal curBal;
                                if (decimal.TryParse(elspAccountInfoResponse.lineOfCredit.PLCIQ.LoanBalance, out curBal))
                                {
                                    result.CurrentBal = curBal;
                                }
                            }
                        }
                        else if (prodCode.ToUpper().Equals("ILA"))
                        {
                            if (elspAccountInfoResponse.loan != null && elspAccountInfoResponse.loan.PIL2Q != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PIL2Q.LoanBalance))
                            {
                                decimal curBal;
                                if (decimal.TryParse(elspAccountInfoResponse.loan.PIL2Q.LoanBalance, out curBal))
                                {
                                    result.CurrentBal = curBal;
                                }
                            }
                        }

                        if (elspAccountInfoResponse.lineOfCredit != null &&
                            elspAccountInfoResponse.lineOfCredit.PLCIQ != null &&
                            !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCIQ.LineOfCreditLimit))
                        {
                            decimal maxLimit;
                            if (decimal.TryParse(elspAccountInfoResponse.lineOfCredit.PLCIQ.LineOfCreditLimit,
                                out maxLimit))
                            {
                                result.MaxLineAmount = maxLimit;
                            }
                        }

                        if (elspAccountInfoResponse.LegalTitle != null)
                            result.Borrower1 = elspAccountInfoResponse.LegalTitle;

                        if (elspAccountInfoResponse.StatementAddress != null)
                        {
                            result.Address1 = elspAccountInfoResponse.StatementAddress.AddressLine1;
                            result.Address2 = elspAccountInfoResponse.StatementAddress.AddressLine2;
                            result.City = elspAccountInfoResponse.StatementAddress.City;
                            result.State = elspAccountInfoResponse.StatementAddress.State;
                            result.Zip = elspAccountInfoResponse.StatementAddress.Zip.Trim();
                            //Format zipcode
                            if (!string.IsNullOrEmpty(result.Zip) && result.Zip.All(Char.IsDigit))
                            {
                                result.Zip = FormatZipCode(result.Zip);
                            }
                        }

                        if (prodCode.ToUpper().Equals("LCA"))
                        {
                            if (elspAccountInfoResponse.lineOfCredit != null &&
                                elspAccountInfoResponse.lineOfCredit.PLCIQ != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCIQ.PayoffCurrentDay))
                            {
                                decimal payOffAmt;
                                if (Decimal.TryParse(elspAccountInfoResponse.lineOfCredit.PLCIQ.PayoffCurrentDay,
                                    out payOffAmt))
                                    result.PayoffBalance = payOffAmt;
                            }
                        }
                        else if (prodCode.ToUpper().Equals("ILA"))
                        {
                            if (elspAccountInfoResponse.loan != null &&
                                elspAccountInfoResponse.loan.PIL2Q != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PIL2Q.PayoffCurrentDay))
                            {
                                decimal payOffAmt;
                                if (Decimal.TryParse(elspAccountInfoResponse.loan.PIL2Q.PayoffCurrentDay, out payOffAmt))
                                    result.PayoffBalance = payOffAmt;
                            }
                        }

                        if (prodCode.ToUpper().Equals("LCA"))
                        {
                            if (elspAccountInfoResponse.lineOfCredit != null &&
                                elspAccountInfoResponse.lineOfCredit.PLCI4 != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.lineOfCredit.PLCI4.PayoffAmtGoodThruDate))
                            {
                                DateTime payOffDate;
                                if (DateTime.TryParseExact(elspAccountInfoResponse.lineOfCredit.PLCI4.PayoffAmtGoodThruDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                                    out payOffDate))
                                    result.PayoffAsOfDate = payOffDate.ToString("MM/dd/yyyy");
                            }
                        }
                        else if (prodCode.ToUpper().Equals("ILA"))
                        {
                            if (elspAccountInfoResponse.loan != null &&
                                elspAccountInfoResponse.loan.PILI5 != null &&
                                !string.IsNullOrEmpty(elspAccountInfoResponse.loan.PILI5.PayoffAmountGoodThruDate))
                            {
                                DateTime payOffDate;
                                if (DateTime.TryParseExact(elspAccountInfoResponse.loan.PILI5.PayoffAmountGoodThruDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                                    out payOffDate))
                                    result.PayoffAsOfDate = payOffDate.ToString("MM/dd/yyyy");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.Error("Exception in MapElpsResponse:" + ex.Message);
                    result.ErrorMessage = "Account not found";
                }
            }
            else
            {
                Logger.Instance.Error("ELPS response is null");
                result.ErrorMessage = "Could not get data";
            }
            return result;
        }

        public List<MaturingToolsEntity> MaturingToolsDataList()
        {
            DataAttributes attributes = new DataAttributes();
            attributes.ConnectionString = eFlowConfig.Instance.GetConnectionStringByName(eFlowConfig.EAIDatabaseName.CUSP);
            attributes.CommandText = "upMaturingOptionsList";
            attributes.CommandType = System.Data.CommandType.StoredProcedure;

            Repository<MaturingToolsEntity> repo = new Repository<MaturingToolsEntity>(attributes);
            List<MaturingToolsEntity> list = repo.GetData();

            return list;
        }

        public MaturingToolsEntity SubmitMaturingToolsData(MaturingToolsEntity meEntity)
        {
            try
            {
                var cuspdbHelper = new CUSPDBHelper();
                //Create a new record if isduplicateok is 1 and usertype is hemap and buttontype is copy
                if (meEntity.IsDuplicateOK == 1 &&
                    meEntity.UserType.ToUpper().Equals(UserType.HEMAP.ToString().ToUpper()) &&
                    meEntity.BtnSave.Label.ToLower().Trim().Equals("copy"))
                {
                    meEntity.MOGUID = Guid.NewGuid();
                    //Format zipcode
                    if (!string.IsNullOrEmpty(meEntity.Zip) && meEntity.Zip.All(Char.IsDigit))
                    {
                        meEntity.Zip = FormatZipCode(meEntity.Zip);
                    }
                    cuspdbHelper.SaveMaturingOptionsData(meEntity);
                }
                else
                {
                    //Format zipcode
                    if (!string.IsNullOrEmpty(meEntity.Zip) && meEntity.Zip.All(Char.IsDigit))
                    {
                        meEntity.Zip = FormatZipCode(meEntity.Zip);
                    }
                    cuspdbHelper.SaveMaturingOptionsData(meEntity);

                    //send email on submit for delivery by OSU user
                    //TODO Uncomment after testing
                    if (meEntity.UserType == UserType.OSU.ToString().ToUpper() && meEntity.IsEmail &&
                        meEntity.MOStatus.ToUpper() == OSUStatus.Delivered.ToString().ToUpper())
                    {
                        SendEmail(meEntity);
                    }
                }

                //Reset buttons after save
                return GetButtonsAfterSave(meEntity.CreatedBy, meEntity.UserType, meEntity.MNLSRID, meEntity.AgentName);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception in SubmitMaturingToolsData:" + ex.Message);
                meEntity.ErrorMessage = "Could not save application";
                return meEntity;
            }
        }

        public string SendEmail(MaturingToolsEntity meEntity)
        {
            try
            {

                var config = LoadConfigValues();
                if (config != null)
                {
                    //Get driver information
                    var cuspdbHelper = new CUSPDBHelper();
                    string driverInfor = cuspdbHelper.GetDriverInformation(meEntity.MOGUID);
                    Logger.Instance.Info("Driver Information: " + driverInfor);
                    if (!string.IsNullOrEmpty(driverInfor))
                    {
                        meEntity.Driver = driverInfor;
                        var emailInfoAltIdentificationList = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("DRIVER", meEntity.Driver)
                        };

                        var emailDeliveryFactory = new EmailDeliveryFactory(Constants.ServiceNameEnum.EmailSend,
                            Constants.ApplicationNameEnum.CuspAppWebAPI,
                            EmailDeliveryConstant.OriginatingProcessEnum.HEMaturingOptions, meEntity.MOGUID.ToString());
                        //TODO: move the replyTo (noreplycss@wellsfargo.com) field to config
                        // TODO: check and add subject if needed
                        var emailDeliveryResponse = emailDeliveryFactory.SendEmail(config.TemplateName, string.Empty,
                            "English",
                            meEntity.EmailAddress, config.ReplyTo, config.bCC, config.Subject,
                            EmailDeliveryConstant.ProcessTrackingTypeEnum.ACCOUNTNUMBER.ToString(), meEntity.AccountNum,
                            EmailDeliveryConstant.CustomerIdentifierTypeEnum.ECN.ToString(), meEntity.AccountNum,
                            emailInfoAltIdentificationList, null, null);
                        if (emailDeliveryResponse == null || emailDeliveryResponse.EmailDelResponse == null)
                        {
                            //TODO call the stored procedure to change the status and actual delivery mode to USMAIL
                            UpdateOnEmailDeliveryError(meEntity);
                            Logger.Instance.Error("Error getting email delivery response");
                            return "Could not send email";

                        }
                        else if (emailDeliveryResponse.EmailDelResponse.sendEmailOutput != null &&
                                 emailDeliveryResponse.EmailDelResponse.sendEmailOutput.responseStatus == "SUCCESS")
                        {
                            //TODO call the stored procedure to change the status to delivered and actual delivery mode to EMAIL
                        }
                        else if (emailDeliveryResponse.EmailDelResponse.WFFaultList.Count > 0)
                        {
                            //TODO call the stored procedure to change the status and actual delivery mode to USMAIL
                            UpdateOnEmailDeliveryError(meEntity);
                            foreach (var faultCode in emailDeliveryResponse.EmailDelResponse.WFFaultList)
                            {
                                Logger.Instance.Error("Fault Reason: " + faultCode.faultReasonText);
                            }
                            return "Could not send email";
                        }
                    }
                    else
                    {
                        UpdateOnEmailDeliveryError(meEntity);
                        Logger.Instance.Error("Driver Information is null");
                        return "Could not send email";
                    }
                    return string.Empty;
                }
                else
                {
                    UpdateOnEmailDeliveryError(meEntity);
                    Logger.Instance.Error("Could not get config values");
                    return "Could not send email";
                }
            }
            catch (Exception ex)
            {
                UpdateOnEmailDeliveryError(meEntity);
                Logger.Instance.Error("Error while sending email: " + ex);
                return "Could not send email";
            }
        }

        public void UpdateOnEmailDeliveryError(MaturingToolsEntity meEntity)
        {
            var cuspdbHelper = new CUSPDBHelper();
            cuspdbHelper.UpdateMaturingOptionsStatus(meEntity.MOGUID, OSUStatus.Error.ToString(), meEntity.LogSource, meEntity.CreatedBy);
        }

        public void ElpsCall(string accountNum)
        {
            MapElpsCallData(accountNum);
        }

        public MaturingToolsEntity GetButtonsAfterSave(string userId, string userType, string mloId, string agentName)
        {
            var result = new MaturingToolsEntity();
            result.UserType = userType;
            result.CreatedBy = userId;
            result.AgentName = agentName;
            result.MNLSRID = mloId;
            if (userType.ToUpper().Equals(UserType.HEMAP.ToString().ToUpper()))
            {
                //Populate Buttons
                var btnSave = new UIButtons();
                btnSave.Label = "Save";
                btnSave.Status = HEMapStatus.Saved.ToString();
                btnSave.IsDisabled = true;
                btnSave.IsHidden = false;

                result.BtnSave = btnSave;

                var btnRetrieve = new UIButtons();
                btnRetrieve.Label = "Retrieve";
                btnRetrieve.Status = "";
                btnRetrieve.IsDisabled = false;
                btnRetrieve.IsHidden = false;

                result.BtnRetrieve = btnRetrieve;

                var btnCancel = new UIButtons();
                btnCancel.Label = "Cancel";
                btnCancel.Status = "";
                btnCancel.IsDisabled = true;
                btnCancel.IsHidden = false;

                result.BtnCancel = btnCancel;

                var btnSubmitForProcessing = new UIButtons();
                btnSubmitForProcessing.Label = "Submit For Review";
                btnSubmitForProcessing.Status = HEMapStatus.ForReview.ToString();
                btnSubmitForProcessing.IsDisabled = true;
                btnSubmitForProcessing.IsHidden = false;

                result.BtnSubmitForProcessing = btnSubmitForProcessing;

            }
            else if (userType.ToUpper().Equals(UserType.OSU.ToString().ToUpper()))
            {
                //Populate Buttons
                var btnSave = new UIButtons();
                btnSave.Label = "Pending QC";
                btnSave.Status = OSUStatus.ForQC.ToString();
                btnSave.IsDisabled = true;
                btnSave.IsHidden = false;

                result.BtnSave = btnSave;

                var btnRetrieve = new UIButtons();
                btnRetrieve.Label = "Retrieve";
                btnRetrieve.Status = "";
                btnRetrieve.IsDisabled = false;
                btnRetrieve.IsHidden = false;

                result.BtnRetrieve = btnRetrieve;

                var btnCancel = new UIButtons();
                btnCancel.Label = "Cancel";
                btnCancel.Status = OSUStatus.Voided.ToString();
                btnCancel.IsDisabled = true;
                btnCancel.IsHidden = false;

                result.BtnCancel = btnCancel;

                var btnSubmitForProcessing = new UIButtons();
                btnSubmitForProcessing.Label = "Waiting For Delivery";
                btnSubmitForProcessing.Status = "";
                btnSubmitForProcessing.IsDisabled = true;
                btnSubmitForProcessing.IsHidden = false;

                result.BtnSubmitForProcessing = btnSubmitForProcessing;
            }
            return result;
        }

        private EmailDeliveryOriginatingProcessConfig LoadConfigValues()
        {
            var config = EmailDeliveryServiceConfig.Instance.GetOriginatingProcessConfig(EmailDeliveryConstant.OriginatingProcessEnum.HEMaturingOptions.ToString());
            return config;
        }

        private string FormatZipCode(string zipCode)
        {
            switch (zipCode.Length)
            {
                case 4:
                    return 0 + zipCode;
                case 5:
                    return zipCode;
                case 8:
                    return 0 + zipCode.Substring(0, 4) + "-" + zipCode.Substring(4, 4);
                case 9:
                    return zipCode.Substring(0, 5) + "-" + zipCode.Substring(5, 4);
                default:
                    return zipCode;
            }
        }

        public MaturingToolsEntity CancelMaturingToolsData(MaturingToolsEntity meEntity)
        {
            //Update the status only for OSU users
            if (meEntity.UserType.ToUpper() == UserType.OSU.ToString().ToUpper())
            {
                var cuspdbHelper = new CUSPDBHelper();
                cuspdbHelper.UpdateMaturingOptionsStatus(meEntity.MOGUID, meEntity.MOStatus, meEntity.LogSource,
                    meEntity.CreatedBy);
            }

            //Reset buttons after cancel
            return GetButtonsAfterSave(meEntity.CreatedBy, meEntity.UserType, meEntity.MNLSRID, meEntity.AgentName);
        }

        public bool BounceBackMaturingToolsData(MaturingToolsEntity meEntity)
        {
            try
            {
                var cuspdbHelper = new CUSPDBHelper();
                cuspdbHelper.UpdateMaturingOptionsBounceBack(meEntity.MOGUID, meEntity.LogSource, meEntity.CreatedBy);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.Info("BounceBack For MaturingOptions " + ex);
                return false;
            }
        }

        public bool ClearAssignToData(MaturingToolsEntity meEntity)
        {
            try
            {
                var cuspdbHelper = new CUSPDBHelper();
                cuspdbHelper.ClearAssignToData(meEntity.MOGUID, meEntity.CreatedBy);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.Info("ClearAssignToData For MaturingOptions " + ex);
                return false;
            }
        }
    }
}

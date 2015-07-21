namespace WF.EAI.BLL.BO.CUSPSearch.Retailer
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using WF.EAI.BLL.CUSP;
    using WF.EAI.Entities.domain;
    using WF.EAI.Entities.domain.cusp.Core;
    using WF.EAI.Entities.domain.cusp.UI;
    using WF.EAI.Model;
    using WF.EAI.Model.DTO;
    using WF.EAI.Model.DTO.CUSPSearch;
    using WF.EAI.Model.Lookup;
    using WF.EAI.Model.ViewModels.CUSPSearch;
    using WF.EAI.Model.ViewModels.CUSPSearch.Retailer;
    using WF.EAI.Utils;
    using efattribute = WF.EAI.Entities.domain.c2c.Common;
    using contracts = WF.UAP.UASF.App.Host.WebApi.Contract;
    using WF.UAP.UASF.App.Host.WebApi.Contract;
    /// <summary>
    /// ArchiveLookupsBo
    /// </summary>
    public class ArchiveLookupsBo : SearchBo
    {
        #region<Private Members>
        /// <summary>
        /// CUSPAppDataHeader
        /// </summary>
        private CUSPAppDataHeader AppHeader;
        /// <summary>
        /// The CuspData header
        /// </summary>
        private CuspData header;            
        /// <summary>
        /// searchApplicationResultsModel
        /// </summary>
        SearchApplicationResultsModel searchApplicationResultsModel = new SearchApplicationResultsModel();
        /// <summary>
        /// model
        /// </summary>
        ArchieveSearchModel model = new ArchieveSearchModel();
        /// <summary>
        /// archieveSearchModels
        /// </summary>
        ArchieveSearchModels archieveSearchModels = new ArchieveSearchModels();
        /// <summary>
        /// listarchieveSearchModel
        /// </summary>
        List<ArchieveSearchModel> listarchieveSearchModel = new List<ArchieveSearchModel>();
        #endregion

        #region<Protected Members>
        /// <summary>
        ///  The app data.
        /// </summary>    
        protected CUSPApplicationData AppData;
        #endregion

        #region<Public Methods>
        /// <summary>
        /// ArchiveLookupsBo
        /// </summary>
        public ArchiveLookupsBo()
        {
            RetailerviewName = Model.Lookup.RetailerSearchViewName.ArchiveLookup;
        }
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="request">RequestDto<CUSPRequestHeader></param>
        /// <returns>ViewModel</returns>
        public SearchApplicationResultsModel Invoke(RequestDto<SearchRequestHeader> request, ref WF.EAI.Model.ViewModels.UFA.ApplicationDataModel dataModel)
        {
            this.AppHeader = new CUSPAppDataHeader() { UserId = "", Password = "", SessionId = "" };

            if (request.RequestHeader.ViewEvent == RetailerViewEvent.Init)
            {

                searchApplicationResultsModel = GetArchievData(request);
            }

            if (request.RequestHeader.ViewEvent == RetailerViewEvent.Save)
            {

                searchApplicationResultsModel = updateArchievData(request);
            }

            return searchApplicationResultsModel;
        }
        /// <summary>
        /// Gets the Archieve records after search.
        /// </summary>
        /// <param name="request">RequestDto<SearchRequestHeader> request</param>
        /// <returns>SearchApplicationResultsModel</returns>
        public SearchApplicationResultsModel GetArchievData(RequestDto<SearchRequestHeader> request)
        {
            string PanelKey = string.Empty; string resultstring = "";
            this.AppHeader = new CUSPAppDataHeader() { UserId = "u298523t", Password = "Ashu$891", SessionId = "" };
            if (request.RequestHeader.LastName != null)
            {
                if (!string.IsNullOrEmpty(request.RequestHeader.LastName.ToString()))
                {
                    string extendedName = request.RequestHeader.LastName.ToString().PadRight(19) + request.RequestHeader.MI.ToString().PadRight(1) + request.RequestHeader.FI.ToString().PadRight(1);
                    PanelKey = "NAME" + extendedName;
                }

                if (!string.IsNullOrEmpty(request.RequestHeader.ReceivedFromDate.ToString()))
                {
                    string dateRange = request.RequestHeader.ReceivedFromDate.ToString().PadRight(8) + request.RequestHeader.ReceivedToDate.ToString().PadRight(8) +
                        request.RequestHeader.ProductName.ToString().PadRight(5);
                    PanelKey = "DATE" + dateRange;
                }

                if (!string.IsNullOrEmpty(request.RequestHeader.SSNNo.ToString()))
                {
                    string archiveSSN = request.RequestHeader.SSNNo.ToString();
                    PanelKey = "SSN " + archiveSSN;
                }

                if (!string.IsNullOrEmpty(request.RequestHeader.FDRAccountNo.ToString()))
                {
                    string AccountNo = request.RequestHeader.FDRAccountNo.ToString();
                    PanelKey = "ACCT" + AccountNo;
                }

                if (!string.IsNullOrEmpty(request.RequestHeader.ApplicationNo.ToString()))
                {
                    string applicationNumber = request.RequestHeader.ApplicationNo.ToString();
                    PanelKey = "APPL" + applicationNumber.PadRight(14);
                }
            }

            this.AppHeader.AcapsFunction = "UPD";
            this.AppHeader.PanelKey = PanelKey;
            this.AppHeader.PopupName = "SearchArchiveInquiry";
            this.AppHeader.ApplicationId = "000000";//request.RequestHeader.ApplicationId
            this.AppHeader.CurrentLocation = string.Empty;
            this.AppHeader.RefreshSession = false;
            this.AppData = new CUSPApplicationData();

            resultstring = GetACAPSData(this.AppHeader);
            var obj = JsonConvert.DeserializeObject(resultstring);
            object jsonObject = JsonConvert.DeserializeObject(resultstring);

            if (!string.IsNullOrEmpty(Convert.ToString(jsonObject)))
            {
                this.AppHeader =
                    JsonMapper<CUSPAppDataHeader>.JsonTextToAppDataHeader<CUSPAppDataHeader>(Convert.ToString(jsonObject));
            }

            if (this.AppHeader.ApplicationData != null)
            {
                obj = JsonConvert.DeserializeObject(this.AppHeader.ApplicationData);
                this.AppData.AcapFields =
                    JsonMapper<Dictionary<string, WF.EAI.Entities.domain.c2c.Common.FieldAttribute>>
                        .JsonTextToAppDataHeader<Dictionary<string, WF.EAI.Entities.domain.c2c.Common.FieldAttribute>>(Convert.ToString(obj));
                List<ArchiveSearchEntity> archiveSearchEntity = GetArchiveSearchData(this.AppData);
                archiveoption options0 = new archiveoption() { Id = 0, Name = "" };
                archiveoption options1 = new archiveoption() { Id = 1, Name = "Select" };
                archiveoption options2 = new archiveoption() { Id = 2, Name = "Email" };
                archiveoption options3 = new archiveoption() { Id = 3, Name = "Cancel" };
                List<archiveoption> optionList = new List<archiveoption>();
                optionList.Add(options0); optionList.Add(options1); optionList.Add(options2); optionList.Add(options3);

                foreach (var item in archiveSearchEntity)
                {
                    var searchmodel = new ArchieveSearchModel()
                    {
                        ApplicationId = item.ApplicationId,
                        ActionStatus = item.ActionStatus.ToString(),
                        BookedDate = item.BookedDate.ToString(),
                        ReceivedDate = item.ReceivedDate.ToString(),
                        LastName = item.LastName.ToString(),
                        FirstName = item.FirstName.ToString(),
                        SSN = item.SSN.ToString(),
                        ZipCode = item.ZipCode.ToString(),
                        archiveoptionList = optionList
                    };
                    listarchieveSearchModel.Add(searchmodel);
                }
            }

            searchApplicationResultsModel.archieveSearchModels = listarchieveSearchModel;
            return searchApplicationResultsModel;
        }
        /// <summary>
        /// updateArchievData
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SearchApplicationResultsModel updateArchievData(RequestDto<SearchRequestHeader> request)
        {
            int lengthUpdateParams = request.RequestHeader.updateparams.Length;
            int indx = 1;
            List<AcapsMessage> acapsMessages = new List<AcapsMessage>();
            List<efattribute.FieldAttribute> fieldList = new List<efattribute.FieldAttribute>();
            efattribute.FieldAttribute field;

            for (int i = 0; i < lengthUpdateParams; i++)
            {
                string[] data = request.RequestHeader.updateparams[i].Split(',');
                string selectionInd = data[1].ToString();
                if (String.Compare(selectionInd, "Select", true) == 0)
                {
                    field = new efattribute.FieldAttribute();
                    field.AcapsFieldName = "ARL_ARCH_LKUP_SELECTION_IND_" + indx;
                    field.PageNumber = "001";
                    field.Value = "S";
                    fieldList.Add(field);
                }

                if (String.Compare(selectionInd, "Email", true) == 0)
                {
                    field = new efattribute.FieldAttribute();
                    field.AcapsFieldName = "ARL_ARCH_LKUP_SELECTION_IND_" + indx;
                    field.PageNumber = "001";
                    field.Value = "E";
                    fieldList.Add(field);
                }

                if (String.Compare(selectionInd, "Cancel", true) == 0)
                {
                    field = new efattribute.FieldAttribute();
                    field.AcapsFieldName = "ARL_ARCH_LKUP_SELECTION_IND_" + indx;
                    field.PageNumber = "001";
                    field.Value = "C";
                    fieldList.Add(field);
                }
                indx++;

            }
            field = new efattribute.FieldAttribute();
            field.AcapsFieldName = "ARL_ARCH_LKUP_INSTANT_RETRVL";
            field.PageNumber = "001";
            field.Value = "Y";
            fieldList.Add(field);

            this.AppHeader = new CUSPAppDataHeader();
            // header = ControlUtils.SessionData.GetBaseCUSPAppDataHeader("000000");
            // ControlUtils.SessionData.GetBaseAppDataHeader(header, AppHeader);
            // Populate panelkey

            if (!string.IsNullOrEmpty(request.RequestHeader.LastName.ToString()))
            {
                string extendedName = request.RequestHeader.LastName.ToString().PadRight(19) + request.RequestHeader.MI.ToString().PadRight(1) + request.RequestHeader.FI.ToString().PadRight(1);
                this.AppHeader.PanelKey = "NAME" + extendedName;
            }

            if (!string.IsNullOrEmpty(request.RequestHeader.ReceivedFromDate.ToString()))
            {
                string dateRange = request.RequestHeader.ReceivedFromDate.ToString().PadRight(8) + request.RequestHeader.ReceivedToDate.ToString().PadRight(8) +
                    request.RequestHeader.ProductName.ToString().PadRight(5);
                this.AppHeader.PanelKey = "DATE" + dateRange;
            }

            if (!string.IsNullOrEmpty(request.RequestHeader.SSNNo.ToString()))
            {
                string archiveSSN = request.RequestHeader.SSNNo.ToString();
                this.AppHeader.PanelKey = "SSN " + archiveSSN;
            }

            if (!string.IsNullOrEmpty(request.RequestHeader.FDRAccountNo.ToString()))
            {
                string AccountNo = request.RequestHeader.FDRAccountNo.ToString();
                this.AppHeader.PanelKey = "ACCT" + AccountNo;
            }

            if (!string.IsNullOrEmpty(request.RequestHeader.ApplicationNo.ToString()))
            {
                string applicationNumber = request.RequestHeader.ApplicationNo.ToString();
                this.AppHeader.PanelKey = "APPL" + applicationNumber.PadRight(14);
            }

            this.AppHeader.AcapsFunction = "UPD";
            this.AppHeader.ApplicationData = JsonMapper<string>.ObjectToJsonText(fieldList);
            string str = this.ProcessUpdate(this.AppHeader);
            var errorMessage = JsonConvert.DeserializeObject<List<contracts.ErrorMessage>>(this.AppHeader.AcapsErrorMessages);                
            searchApplicationResultsModel = GetArchievData(request);
            searchApplicationResultsModel.errorMessages = errorMessage;
            return searchApplicationResultsModel;
        }
        /// <summary>
        /// GetACAPSData
        /// </summary>
        /// <param name="appHeader"></param>
        /// <returns></returns>
        public string GetACAPSData(CUSPAppDataHeader appHeader)
        {
            return ACAPSAppDataServiceAdapter.GetACAPSData(appHeader);
        }
        /// <summary>
        /// ProcessUpdate
        /// </summary>
        /// <param name="appHeader"></param>
        /// <returns></returns>
        public string ProcessUpdate(CUSPAppDataHeader appHeader)
        {
            return ACAPSAppDataServiceAdapter.ProcessUpdateUIFields(appHeader);
        }
        #endregion

        #region<Private Methods>
        /// <summary>
        /// GetArchiveSearchData
        /// </summary>
        /// <param name="applicationData"></param>
        /// <returns></returns>
        private List<ArchiveSearchEntity> GetArchiveSearchData(CUSPApplicationData applicationData)
        {
            var archiveSearchEntityList = new List<ArchiveSearchEntity>();
            string[] Fieldnames = new[]
				{
					"ARL_ARCH_LKUP_PROD_CODE", 
					"ARL_ARCH_LKUP_LAST_NAME", 
					"ARL_ARCH_LKUP_FIRST_NAME", 
					"ARL_ARCH_LKUP_APPLICANT_IND", 
					"ARL_ARCH_LKUP_APPL_ACTION_STATUS", 
					"ARL_ARCH_LKUP_APPL_ID", 
					"ARL_ARCH_LKUP_APPL_RECEIVED_DATE", 
					"ARL_ARCH_LKUP_APPL_BOOKED_DATE", 
					"ARL_ARCH_LKUP_APPL_SETUP_DATE", 
					"ARL_ARCH_LKUP_LOCATION_CODE", 
					"ARL_ARCH_LKUP_MIDDLE_INIT", 
					"ARL_ARCH_LKUP_SELECTION_IND", 
					"ARL_ARCH_LKUP_SSN", 
					"ARL_ARCH_LKUP_STATUS", 
					"ARL_ARCH_LKUP_ZIP_CODE"
				};

            int MAXPAGES = 25;
            int MAXROWS = 7;

            for (int page = 1; page <= MAXPAGES; page++)
            {
                for (int i = 1; i <= MAXROWS; i++)
                {
                    var archiveSearchEntity = new ArchiveSearchEntity();
                    foreach (string fieldname in Fieldnames)
                    {
                        if (fieldname.StartsWith("ARL_ARCH_LKUP_PROD_CODE"))
                        {
                            archiveSearchEntity.ProductCode = applicationData.AcapFields["ARL_ARCH_LKUP_PROD_CODE" + "_" + i.ToString() + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_LAST_NAME"))
                        {
                            archiveSearchEntity.LastName = applicationData.AcapFields["ARL_ARCH_LKUP_LAST_NAME" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_FIRST_NAME"))
                        {
                            archiveSearchEntity.FirstName = applicationData.AcapFields["ARL_ARCH_LKUP_FIRST_NAME" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_APPLICANT_IND"))
                        {
                            // archiveSearchEntity.ApplicationId  = (from field in maskInqRes.MaskInqResponse.fields[0].field
                            // where field.name == "ARL_ARCH_LKUP_APPLICANT_IND" + "_" + i
                            // select field.Value).First();
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_APPL_ACTION_STATUS"))
                        {
                            archiveSearchEntity.ActionStatus = applicationData.AcapFields["ARL_ARCH_LKUP_APPL_ACTION_STATUS" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_APPL_ID"))
                        {
                            archiveSearchEntity.ApplicationId = applicationData.AcapFields["ARL_ARCH_LKUP_APPL_ID" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_APPL_RECEIVED_DATE"))
                        {
                            archiveSearchEntity.ReceivedDate = applicationData.AcapFields["ARL_ARCH_LKUP_APPL_RECEIVED_DATE" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_APPL_SETUP_DATE"))
                        {
                            archiveSearchEntity.BookedDate = applicationData.AcapFields["ARL_ARCH_LKUP_APPL_SETUP_DATE" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_LOCATION_CODE"))
                        {
                            // archiveSearchEntity. = (from field in maskInqRes.MaskInqResponse.fields[0].field
                            // where field.name == "ARL_ARCH_LKUP_LOCATION_CODE" + "_" + i
                            // select field.Value).First();
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_MIDDLE_INIT"))
                        {
                            // archiveSearchEntity = (from field in maskInqRes.MaskInqResponse.fields[0].field
                            // where field.name == "ARL_ARCH_LKUP_MIDDLE_INIT" + "_" + i
                            // select field.Value).First();
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_SELECTION_IND"))
                        {
                            archiveSearchEntity.SelectionInd = applicationData.AcapFields["ARL_ARCH_LKUP_SELECTION_IND" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_SSN"))
                        {
                            archiveSearchEntity.SSN = applicationData.AcapFields["ARL_ARCH_LKUP_SSN" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_STATUS"))
                        {
                            archiveSearchEntity.Status = applicationData.AcapFields["ARL_ARCH_LKUP_STATUS" + "_" + i + "_PG_" + page].Value;
                        }

                        if (fieldname.StartsWith("ARL_ARCH_LKUP_ZIP_CODE"))
                        {
                            archiveSearchEntity.ZipCode = applicationData.AcapFields["ARL_ARCH_LKUP_ZIP_CODE" + "_" + i + "_PG_" + page].Value;
                        }
                    }

                    if (!(string.IsNullOrEmpty(archiveSearchEntity.FirstName) &&
                        string.IsNullOrEmpty(archiveSearchEntity.LastName) &&
                        string.IsNullOrEmpty(archiveSearchEntity.ProductCode) &&
                        string.IsNullOrEmpty(archiveSearchEntity.ActionStatus) &&
                        string.IsNullOrEmpty(archiveSearchEntity.BookedDate) &&
                        string.IsNullOrEmpty(archiveSearchEntity.ReceivedDate) &&
                        string.IsNullOrEmpty(archiveSearchEntity.SSN) &&
                        string.IsNullOrEmpty(archiveSearchEntity.SelectionInd) &&
                        string.IsNullOrEmpty(archiveSearchEntity.Status) &&
                        string.IsNullOrEmpty(archiveSearchEntity.ZipCode) &&
                        string.IsNullOrEmpty(archiveSearchEntity.ApplicationId)))
                    {
                        archiveSearchEntityList.Add(archiveSearchEntity);
                    }
                    else
                    {
                        // RJ this is the end of the list
                        i = 999;
                        page = 999;
                    }

                }
            }
            return archiveSearchEntityList;

        }
        #endregion
    }
}

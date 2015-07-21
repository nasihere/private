using System.Collections.Generic;
using WF.EAI.BLL.BO.CUSPSearch.Retailer;
using WF.EAI.Model.Lookup;

namespace WF.EAI.BLL.BO.CUSPSearch.Factories.Retailer
{
    /// <summary>
    /// RetailerSearchFactory constructor
    /// </summary>
    public class RetailerSearchFactory : IRetailerSearchFactory
    {
        private Dictionary<RetailerSearchViewName, SearchBo> _boObjects = new Dictionary<RetailerSearchViewName, SearchBo>();

        /// <summary>
        /// RetailerSearchFactory constructor
        /// </summary>
        public RetailerSearchFactory()
        {
            BuildFactory();
        }

        /// <summary>
        /// GetBo
        /// </summary>
        /// <param name="ViewName"></param>
        /// <returns></returns>
        public SearchBo GetBo(Model.Lookup.RetailerSearchViewName ViewName)
        {
            return _boObjects[ViewName];
        }

        /// <summary>
        /// Load Bo objects
        /// </summary>
        public void BuildFactory()
        {
           _boObjects.Add(RetailerSearchViewName.AppIdSearch, new AppIdSearchBo());
            _boObjects.Add(RetailerSearchViewName.NameSearch, new NameSearchBo());
            _boObjects.Add(RetailerSearchViewName.PhoneNoSearch, new PhoneNoSearchBo());
            _boObjects.Add(RetailerSearchViewName.FDRACNoSearch, new FDRAccountNoSearchBo());
            _boObjects.Add(RetailerSearchViewName.WorkListSearch, new WorkListSearchBo());
            _boObjects.Add(RetailerSearchViewName.ScanListSearch, new ScanListSearchBo());
            _boObjects.Add(RetailerSearchViewName.SSNSearch, new SSNSearchBo());
            _boObjects.Add(RetailerSearchViewName.CreditAnalysis, new CreditAnalysisTabBo());
            _boObjects.Add(RetailerSearchViewName.ArchiveLookup, new ArchiveLookupsBo());
            _boObjects.Add(RetailerSearchViewName.GetNext, new WF.EAI.BLL.BO.CUSPSearch.Retailer.GetNextBo());

        }
    }
}

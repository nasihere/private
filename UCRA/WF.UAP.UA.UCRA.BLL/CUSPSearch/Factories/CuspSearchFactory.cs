using System.Collections.Generic;
using WF.EAI.BLL.BO.CUSPSearch.Retailer;
using WF.EAI.Model.Lookup;

namespace WF.EAI.BLL.BO.CUSPSearch.Factories
{
	public class CuspSearchFactory : ICuspSearchFactory
	{
		private Dictionary<CUSPSearchViewName, SearchBo> _boObjects = new Dictionary<CUSPSearchViewName, SearchBo>();

		public CuspSearchFactory()
		{
			BuildFactory();
		}

		/// <summary>
		/// GetBo
		/// </summary>
		/// <param name="ViewName"></param>
		/// <returns></returns>
		public SearchBo GetBo(Model.Lookup.CUSPSearchViewName ViewName)
		{
			return _boObjects[ViewName];
		}

		/// <summary>
		/// Load Bo objects
		/// </summary>
		public void BuildFactory()
		{
			_boObjects.Add(CUSPSearchViewName.CreditApplication, new CreditApplicationBo());
			_boObjects.Add(CUSPSearchViewName.CreditAnalysis, new CreditAnalysisBo());
			_boObjects.Add(CUSPSearchViewName.ContractBooking, new ContractBookingBo());
			_boObjects.Add(CUSPSearchViewName.BankLoanBooking, new BankLoanBookingBo());
			_boObjects.Add(CUSPSearchViewName.ArchiveLookup, new ArchiveLookupBo());
			_boObjects.Add(CUSPSearchViewName.GetNext, new GetNextBo());
			_boObjects.Add(CUSPSearchViewName.Utilities, new UtilitiesBo());
			_boObjects.Add(CUSPSearchViewName.AppIdSearch, new AppIdSearchBo());
			_boObjects.Add(CUSPSearchViewName.NameSearch, new NameSearchBo());
		}
	}
}

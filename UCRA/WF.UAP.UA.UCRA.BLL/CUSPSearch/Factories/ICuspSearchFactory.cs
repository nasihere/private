using WF.EAI.Model.Lookup;

namespace WF.EAI.BLL.BO.CUSPSearch.Factories
{
	public interface ICuspSearchFactory
	{
		/// <summary>
		/// GetBo
		/// </summary>
		/// <param name="ViewName"></param>
		/// <returns></returns>
		SearchBo GetBo(CUSPSearchViewName ViewName);

		/// <summary>
		/// Load all Bo objects
		/// </summary>
		void BuildFactory();
	}
}

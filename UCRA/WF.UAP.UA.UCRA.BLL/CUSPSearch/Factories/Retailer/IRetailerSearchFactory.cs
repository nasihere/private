using WF.EAI.Model.Lookup;

namespace WF.EAI.BLL.BO.CUSPSearch.Factories.Retailer
{
  public interface IRetailerSearchFactory
    {
        /// <summary>
        /// GetBo
        /// </summary>
        /// <param name="ViewName"></param>
        /// <returns></returns>
        SearchBo GetBo(RetailerSearchViewName ViewName);

        /// <summary>
        /// Load all Bo objects
        /// </summary>
        void BuildFactory();
    }
}

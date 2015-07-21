using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF.UAP.UDB.Repository.Domain.Entities.UCRA.cas;

namespace WF.UAP.UA.UCRA.BLL.cas.Interfaces
{
    public interface ICasWmgRateException
    {
        WmgRateExceptionEntity GetWmgRateException(string ApplicationID, string IsCurrent);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xxxxProjec.....UAPortal..Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class TopSecretAuth
    {

        //public bool ValidateUser(string userName, string password)
        //{
        //    bool validation;

        //    try
        //    {
        //        LdapConnection ldc = new LdapConnection(new LdapDirectoryIdentifier((string)null, false, false));
        //        NetworkCredential nc = new NetworkCredential(userName, password, "ent.wfb.bank.corp");
        //        ldc.Credential = nc;
        //        ldc.AuthType = AuthType.Negotiate;
        //        using (ldc)
        //        {
        //            ldc.Bind(nc); // user has authenticated at this point, as the credentials were used to login to the dc.
        //            validation = true;

        //            SearchRequest request = new SearchRequest("DC=ent,DC=wfb,DC=bank,DC=corp", "(SAMAccountName=cineas)", SearchScope.Subtree);
        //            SearchResponse response = (SearchResponse)ldc.SendRequest(request);

        //            if (response != null)
        //            {
        //                foreach (SearchResultEntry entry in response.Entries)
        //                {
        //                    foreach (DictionaryEntry att in entry.Attributes)
        //                    {
        //                        if (!data.ContainsKey(att.Key.ToString()))
        //                            data.Add(att.Key.ToString(), ((DirectoryAttribute)(att.Value))[0].ToString());
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (LdapException)
        //    {
        //        validation = false;
        //    }
        //    return validation;
        //}

    }
}

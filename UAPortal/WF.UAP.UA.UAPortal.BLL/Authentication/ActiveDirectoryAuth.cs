using System.DirectoryServices.Protocols;
using System.Net;

namespace WF.UAP.UA.UAPortal.BLL.Authentication
{
    /// <summary>
    /// ActiveDirectoryAuth
    /// </summary>
    public class ActiveDirectoryAuth
    {

        /// <summary>
        /// ValidateUser - Validate Active Directory Users
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="password">password</param>
        /// <returns>True/False Boolean</returns>
        public bool ValidateUser(string userName, string password)
        {
            bool validation;

            try
            {
                var ldc = new LdapConnection(new LdapDirectoryIdentifier((string)null, false, false));
                var nc = new NetworkCredential(userName, password, "ent.wfb.bank.corp");
                ldc.Credential = nc;
                ldc.AuthType = AuthType.Negotiate;
                using (ldc)
                {
                    ldc.Bind(nc); // user has authenticated at this point, as the credentials were used to login to the dc.
                    validation = true;
                }
            }
            catch (LdapException)
            {
                validation = false;
            }
            return validation;
        }

    }
}

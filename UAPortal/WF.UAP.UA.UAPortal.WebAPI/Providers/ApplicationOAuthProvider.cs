using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WF.UAP.UA.UAPortal.BLL.Authentication;
using WF.UAP.UASF.CrossCutting.Logging;

namespace WF.UAP.UA.UAPortal.WebAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var activeDirAuth = new ActiveDirectoryAuth();
                var resp = activeDirAuth.ValidateUser(context.UserName, context.Password);
                if (!resp)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var claims = new List<Claim>
                             {
                                 new Claim(ClaimTypes.Name, context.UserName),
                                 new Claim(ClaimTypes.NameIdentifier, new Guid().ToString())
                             };

                var authUserIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                var authCookieIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

                var oAuthIdentity = authUserIdentity;
                var cookiesIdentity = authCookieIdentity;

                var properties = CreateProperties(context.UserName);
                var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Error while Autheticating AD-ENT User:" + context.UserName + ":Exception:" + ex);
            }

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

      

       
    }
}
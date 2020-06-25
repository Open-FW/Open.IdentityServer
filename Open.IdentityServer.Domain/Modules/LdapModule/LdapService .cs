
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System;

namespace Open.IdentityServer.Domain.Modules.LdapModule
{
    public class LdapService
    {
        private readonly ILogger<LdapService> logger;
        private readonly LdapSettings ldap;

        public LdapService(ILogger<LdapService> logger, IOptions<LdapSettings> ldapAccessor)
        {
            this.logger = logger;
            this.ldap = ldapAccessor?.Value ?? throw new ArgumentNullException($"{nameof(ldapAccessor)}");
        }

        public LdapUser? ValidateUser(string username, string password)
        {
            string userDn = $"{username}@{ldap.Domain}";
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(this.ldap.Host, this.ldap.Port ?? 389);
                    connection.Bind(userDn, password);

                    if (!connection.Bound)
                    {
                        return null;
                    }

                    var res = connection.Search(ldap.Base, LdapConnection.ScopeSub, $"(samaccountname={username})", null, false, default, default);

                    LdapMessage message;

                    while ((message = res.GetResponse()) != null)
                    {
                        if (message is LdapSearchResult result)
                        {
                            return LdapUser.CreateFromResult(result);
                        }
                    }

                }
            }
            catch (LdapException ex)
            {
                this.logger.LogError(ex.Message, ex.Data);
            }

            return null;
        }
    }
}

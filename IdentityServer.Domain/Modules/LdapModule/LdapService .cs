
using Novell.Directory.Ldap;

namespace IdentityServer.Domain.Modules.LdapModule
{
    public class LdapService
    {
        private readonly LdapSetting ldap;

        public LdapService(LdapSetting ldap)
        {
            this.ldap = ldap;
        }

        public LdapUser? ValidateUser(string username, string password)
        {
            string userDn = $"{username}@{ldap.Domain}";
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(this.ldap.Host, ldap.Port);
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
                // Log exception
            }

            return null;
        }
    }
}
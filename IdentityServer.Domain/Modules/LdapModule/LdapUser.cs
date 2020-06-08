using System;

using Novell.Directory.Ldap;

namespace IdentityServer.Domain.Modules.LdapModule
{
    public class LdapUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;

        public static LdapUser CreateFromResult(LdapSearchResult result)
        {
            return new LdapUser
            {
                Id = new Guid(result.Entry.GetAttribute("objectGuid").ByteValue),
                Email = result.Entry.GetAttribute("mail").StringValue
            };
        }
    }
}

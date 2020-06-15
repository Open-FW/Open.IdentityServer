using System;
using System.Diagnostics.CodeAnalysis;

using Novell.Directory.Ldap;

namespace Open.IdentityServer.Domain.Modules.LdapModule
{
    public class LdapUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;

        public static LdapUser CreateFromResult([NotNull]LdapSearchResult result)
        {
            return new LdapUser
            {
                Id = new Guid(result.Entry.GetAttribute("objectGuid").ByteValue),
                Email = result.Entry.GetAttribute("mail").StringValue
            };
        }
    }
}

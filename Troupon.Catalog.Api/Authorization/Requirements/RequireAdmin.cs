using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Troupon.Catalog.Api.Authorization.Requirements
{
  public class RequireAdmin : IAuthorizationRequirement
  {
    public RequireAdmin()
    {
    }

    private string ClaimType { get; } = "scp";

    private string ClaimValue { get; } = "admin";

    internal bool IsMet(IEnumerable<Claim> claims)
    {
      return claims.Any(t => t.Type == ClaimType && t.Value == ClaimValue);
    }
  }
}

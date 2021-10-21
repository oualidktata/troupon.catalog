using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Troupon.Catalog.Api.Authorization.Requirements
{
  /// <summary>
  /// Request to Authorize ARAI Calls.
  /// </summary>
  public class RequireTenant : IAuthorizationRequirement
  {
    public RequireTenant(string claimType, string tenantId)
    {
      ClaimType = claimType;
      TenantId = tenantId;
    }

    private string ClaimType { get; }

    private string TenantId { get; }

    public bool IsMet(IEnumerable<Claim> claims)
    {
      return claims.Any(t => t.Type == ClaimType && t.Value == TenantId);
    }
  }
}

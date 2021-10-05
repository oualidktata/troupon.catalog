using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Troupon.Catalog.Api
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

    public string ClaimType { get; }

    public string TenantId { get; }
  }
}

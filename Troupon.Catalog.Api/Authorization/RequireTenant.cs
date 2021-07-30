using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Troupon.Catalog.Api
{
  /// <summary>
  /// Request to Authorize ARAI Calls
  /// </summary>
  public class RequireTenant:IAuthorizationRequirement
  {
    public RequireTenant(string claimType,string tenantId)
    {
      ClaimType = claimType;
      TenantId = tenantId;
    }

    public string ClaimType { get; }
    public string TenantId { get; }
  }
  public class RequireTenantClaimHandler : AuthorizationHandler<RequireTenant>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequireTenant requirement)
    {
      var hasClaim = context.User.Claims.Any(t => t.Type == requirement.ClaimType);
      if (hasClaim)
      {
        if (context.User.Claims.FirstOrDefault(t => t.Type == requirement.ClaimType).Value == requirement.TenantId)
        {
          context.Succeed(requirement);
        }
        else {
          context.Fail();
        }
      }
      return Task.CompletedTask;
     }
  }

  public static class PolicyBuilderExtensions
  {
    public static AuthorizationPolicyBuilder AddTenantPolicy(this AuthorizationPolicyBuilder builder,string tenantValue) {
      builder.AddRequirements(new RequireTenant("TenantId", tenantValue));
      return builder;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Troupon.Catalog.Api
{
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
        else
        {
          context.Fail();
        }
      }

      return Task.CompletedTask;
    }
  }
}

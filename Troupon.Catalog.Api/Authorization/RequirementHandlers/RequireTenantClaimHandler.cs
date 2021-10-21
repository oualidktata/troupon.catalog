using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Troupon.Catalog.Api.Authorization.Requirements;

namespace Troupon.Catalog.Api.Authorization.RequirementHandlers
{
  public class RequireTenantClaimHandler : AuthorizationHandler<RequireTenant>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequireTenant requirement)
    {
      if (requirement.IsMet(context.User.Claims))
      {
        context.Succeed(requirement);
      }
      else
      {
        context.Fail();
      }

      return Task.CompletedTask;
    }
  }
}

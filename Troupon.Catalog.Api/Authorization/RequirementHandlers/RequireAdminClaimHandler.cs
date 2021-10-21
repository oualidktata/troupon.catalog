using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Troupon.Catalog.Api.Authorization.Requirements;

namespace Troupon.Catalog.Api.Authorization.RequirementHandlers
{
  public class RequireAdminClaimHandler : AuthorizationHandler<RequireAdmin>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequireAdmin requirement)
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

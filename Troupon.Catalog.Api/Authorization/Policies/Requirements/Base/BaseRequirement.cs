using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Troupon.Catalog.Api.Authorization.Policies.Requirements.Base
{
  public abstract class BaseRequirement : IAuthorizationRequirement
  {
    public abstract bool IsMet(IEnumerable<Claim> claims);
  }
}

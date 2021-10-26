using Microsoft.AspNetCore.Authorization;
using Troupon.Catalog.Api.Authorization.Policies.Requirements;

namespace Troupon.Catalog.Api.Authorization.Policies
{
  public static class AdminOnlyPolicy
  {
    public const string Key = "AdminOnly";

    public static AuthorizationPolicyBuilder AddAdminOnlyPolicy(this AuthorizationPolicyBuilder builder)
    {
      builder.Requirements.Add(new RequireAdmin());
      return builder;
    }
  }
}

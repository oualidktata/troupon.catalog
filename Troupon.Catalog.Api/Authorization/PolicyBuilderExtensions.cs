using Microsoft.AspNetCore.Authorization;
using Troupon.Catalog.Api.Authorization.Requirements;

namespace Troupon.Catalog.Api.Authorization
{
  public static class PolicyBuilderExtensions
  {
    public static AuthorizationPolicyBuilder AddTenantPolicy(this AuthorizationPolicyBuilder builder, string tenantValue)
    {
      builder.AddRequirements(new RequireTenant("TenantId", tenantValue));
      return builder;
    }

    public static AuthorizationPolicyBuilder AddAdminPolicy(this AuthorizationPolicyBuilder builder)
    {
      builder.AddRequirements(new RequireAdmin());
      return builder;
    }
  }
}

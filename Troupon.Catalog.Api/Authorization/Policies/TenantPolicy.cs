using Microsoft.AspNetCore.Authorization;
using Troupon.Catalog.Api.Authorization.Policies.Requirements;

namespace Troupon.Catalog.Api.Authorization.Policies
{
  public static class TenantPolicy
  {
    public const string Key = "Tenant";

    public static AuthorizationPolicyBuilder AddTenantPolicy(this AuthorizationPolicyBuilder builder, string tenantValue)
    {
      builder.Requirements.Add(new RequireTenant("TenantId", tenantValue));
      return builder;
    }
  }
}

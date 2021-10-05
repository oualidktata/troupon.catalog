using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Troupon.Catalog.Api
{
  public static class PolicyBuilderExtensions
  {
    public static AuthorizationPolicyBuilder AddTenantPolicy(this AuthorizationPolicyBuilder builder, string tenantValue)
    {
      builder.AddRequirements(new RequireTenant("TenantId", tenantValue));
      return builder;
    }
  }
}

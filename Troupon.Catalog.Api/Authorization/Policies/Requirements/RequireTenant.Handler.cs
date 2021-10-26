using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Troupon.Catalog.Api.Authorization.Policies.Requirements.Base;

namespace Troupon.Catalog.Api.Authorization.Policies.Requirements
{
  /// <summary>
  /// RequireTenant.Handler is used to apply requirements to authorization context.
  /// </summary>
  public partial class RequireTenant
  {
    public class Handler : BaseRequirementHandler<RequireTenant>
    {
    }
  }
}

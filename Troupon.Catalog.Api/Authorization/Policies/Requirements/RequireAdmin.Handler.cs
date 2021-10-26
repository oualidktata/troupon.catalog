using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Troupon.Catalog.Api.Authorization.Policies.Requirements.Base;

namespace Troupon.Catalog.Api.Authorization.Policies.Requirements
{
  /// <summary>
  /// RequireAdmin.Handler is used to apply requirements to authorization context.
  /// </summary>
  public partial class RequireAdmin
  {
    public class Handler : BaseRequirementHandler<RequireAdmin>
    {
    }
  }
}

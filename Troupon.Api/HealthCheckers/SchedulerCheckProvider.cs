using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Troupon.Catalog.Service.Api.HealthCheckers
{
    public class SchedulerCheckProvider : IHealthCheck
    {
        private readonly string _baseUri;

        public SchedulerCheckProvider(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            //check db Connection
            return Task.FromResult(HealthCheckResult.Healthy("The scheduling service is UP!"));
        }
    }
}

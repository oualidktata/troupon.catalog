using System.Linq;
using Infra.DomainDrivenDesign.Base;

namespace Infra.DomainDrivenDesign.Persistence.Repositories
{
    public interface IReadRepository<TEntity> : IQueryable<TEntity> where TEntity : IEntity<out TId>
    {
        
    }
}

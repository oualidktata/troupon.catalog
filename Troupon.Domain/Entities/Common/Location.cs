using System.Collections.Generic;
using Infra.DomainDrivenDesign.Base;

namespace Troupon.Catalog.Core.Domain.Entities.Common
{
    public class Location : ValueObject
    {
        public Position Position { get; private set; }
        public Address Address { get; private set; }

        public Location(
            Position position,
            Address address)
        {
            Position = position;
            Address = address;
        }

        protected override IEnumerable<object> GetEqualityValues()
        {
            yield return Position;
            yield return Address;
        }
    }
}

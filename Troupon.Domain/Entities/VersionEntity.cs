using System;

namespace Troupon.Catalog.Core.Domain.Entities
{
    public class DealEntity : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Patch { get; set; }
    }
}
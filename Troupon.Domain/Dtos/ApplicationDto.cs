using System;

namespace Troupon.Catalog.Core.Domain.Dtos
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

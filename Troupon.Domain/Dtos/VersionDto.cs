namespace Troupon.Catalog.Core.Domain.Dtos
{
    public class DealDto
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Major
        /// </summary>
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Patch { get; set; }
    }
}

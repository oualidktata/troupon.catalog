namespace Troupon.Catalog.Core.Application.Caching
{
  public interface ICachable
  {
    string CacheKey { get; }
  }
}

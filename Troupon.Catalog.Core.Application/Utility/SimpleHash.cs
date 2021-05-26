using Newtonsoft.Json;
using Troupon.Catalog.Core.Domain.InputModels;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Troupon.Catalog.Core.Application.Utility
{
  public static class UtilityMethods
  {
    public static string ToHash(
      SearchDealsFilter filter)
    {
      using (var algorithm = MD5.Create())
      {
        var json = JsonConvert.SerializeObject(filter);
        var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(json));

        return Convert.ToBase64String(hash);
      }
    }
  }
}

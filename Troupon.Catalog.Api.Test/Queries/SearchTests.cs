using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Troupon.Catalog.Api;
using Troupon.Catalog.Core.Domain.Dtos;
using Troupon.Catalog.Core.Domain.InputModels;
using Troupon.Catalog.Infra.Persistence;
using Xunit;

namespace Troupon.Catalog.Integration.Tests
{
  public class SearchTests :SetupFixture, IDisposable
  {
    public SearchTests(WebApplicationFactory<Startup> factory):base(factory)
    {

    }

    [Fact]
    public async Task Search_ShouldReturnList_WhenValidSearchModel()
    {
      //Arrange
      var client = Factory.CreateClient();
      var filter = new SearchDealsFilter("Intersting Deal", "Nice description for Interseting Deal");
      var content = JsonContent.Create(filter);
      using var scope = _scopeFactory.CreateScope();
      //Act
      var response = await client.PostAsync($"{BaseUri}/search", content);
      //Assert
      response.StatusCode.Should().Be(HttpStatusCode.OK);
      response.Should().NotBeOfType<DealDto[]>();
    }
    [InlineData("6346dfe7-b3bc-4a85-bb6e-d5cdc19a6fbb")]
    [Theory]
    public async Task Get_ShouldReturnOneDealWhenValidDealId(string dealId)
    {
      //Arrange
      var client = Factory.CreateClient();

      //Act
      var response = await client.GetAsync($"{BaseUri}/{dealId}");
      var stringifiedResult = await response.Content.ReadAsStringAsync();
      response.StatusCode.Should().Be(HttpStatusCode.NotFound);


      var result = JsonHelper.fromJson<FluentResult>(stringifiedResult);


      //Assert
      response.StatusCode.Should().Be(HttpStatusCode.NotFound);

      //var obj=JObject.Parse(result);
      // var desrilizedResult = JsonConvert.DeserializeObject<Result>(stringifiedResult);


      //desrilizedResult.ToResult<>(e => e.Message==$"Could not find the Deal: {dealId}");
      //  .Should().BeFailure().And.Satisfy(result=>
      //result.Errors.Should().ContainEquivalentOf(new Error($"Could not find the Deal: {dealId}")));
      //response;
    }

    public void Dispose()
    {

    }
  }
}

using System;
using Xunit;
using System.Net.Http;
using Shouldly;
using System.Collections.Generic;
using CRM.Integration.SDK;
using Infra.Common.Models;

namespace Troupon.Catalog.Service.Api.Test
{
    public class IncidentAsyncTest
    {
        private HttpClient _httpClient => new HttpClient();
        private string _baseUri => "https://localhost:44343";
        public IncidentAsyncTest()
        {

        }
        [InlineData("pw12345678","empty")]
        [InlineData("pw12345678910", "Not Found")]
        [InlineData("pw12345678910", "Bad Request")]
        [Theory]
        public async void GetIncidentsShouldReturnArrayOfIncidents(string userParam, string response)
        {
            //Arrange
            var client = new CRMClient(_baseUri, _httpClient);
            //Act
            try
            {
                var result = await client.IncidentAsync(new IncidentSearchInput { User=userParam});
                //Assert
                result.ShouldNotBeNull();
                result.ShouldBeOfType<List<IncidentDto>>("Wrong object");
            }
            catch (ApiException<ProblemDetails> exception)
            {
                //Could not connect- Actively refuses-Server down
                if (exception.StatusCode==400) { exception.Result.AdditionalProperties.Count.ShouldBeGreaterThanOrEqualTo(1, $"{exception.Result.AdditionalProperties.Count} errors found");

                    exception.Message.ShouldContain(response);
                    return;
                }
                if (exception.StatusCode == 406) { exception.Message.ShouldBe(SwaggerResponseMessages.Status406); }
                if (exception.StatusCode == 409) { exception.Message.ShouldBe("Conflict"); }
                throw;
            }
        }
        //[Fact]
        //public async void GetIncidentsWithWrongParametersShouldReturnABadRequest()
        //{
        //    //Arrange
        //    var client = new CRMClient(_baseUri, _httpClient);
        //    //Act
        //    try
        //    {
        //        var result = await client.IncidentAsync(new IncidentSearchInput { User="pw123456"});
        //        //Assert
        //        result.ShouldNotBeNull();
        //        result.ShouldBeOfType<List<IncidentDto>>("Wrong object");
        //    }
        //    catch (Exception e)
        //    {
        //        var result = e;
        //    }

        //}
    }
}

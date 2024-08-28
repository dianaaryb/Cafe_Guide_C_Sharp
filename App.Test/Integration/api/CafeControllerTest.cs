using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using App.DTO.v1_0.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis;
using WebApp.Helpers;
using Xunit.Abstractions;

namespace App.Test.Integration.api;

[Collection("NonParallel")]
public class CafeControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _output;

    public CafeControllerTest(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        _output = output;
    }

    [Fact]
    public async Task IndexRequiresLogin()
    {
        try
        {
            // Act
            var response = await _client.GetAsync("/api/v1.0/Cafes");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        catch (AggregateException ex)
        {
            foreach (var innerException in ex.InnerExceptions)
            {
                // Log or inspect the inner exception details
                _output.WriteLine(innerException.Message);
            }

            // Re-throw if necessary or handle specific cases
            throw;
        }
    }

    [Fact]
    public async Task IndexWithUser()
    {
        var user = "admin@eesti.ee";
        var pass = "Kala.maja1";
    
        // get jwt
        var response =
            await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Login", new {email = user, password = pass});
        var contentStr = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
    
        var loginData = JsonSerializer.Deserialize<JWTResponse>(contentStr, JsonHelper.CamelCase);
    
        Assert.NotNull(loginData);
        Assert.NotNull(loginData.Jwt);
        Assert.True(loginData.Jwt.Length > 0);
    
        var msg = new HttpRequestMessage(HttpMethod.Get, "/api/v1.0/Cafes");
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginData.Jwt);
        msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    
        response = await _client.SendAsync(msg);
    
        response.EnsureSuccessStatusCode();
    }
}

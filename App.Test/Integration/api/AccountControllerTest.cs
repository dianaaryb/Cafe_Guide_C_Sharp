using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace App.Test.Integration.api;

[Collection("NonParallel")]
public class AccountControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public AccountControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task CanLogin()
    {
        var user = "admin@eesti.ee";
        var pass = "Kala.maja1";
        
        var response =
            await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Login", new { email = user, password = pass });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CanRegister()
    {
        var firstName = "New";
        var lastName = "User";
        var email = "newemail@eesti.ee";
        var pass = "Kala.maja1";

        await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Register", 
            new { FirstName = firstName, LastName = lastName, Email = email, Password = pass });
        
        var response =
            await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Login", new { Email = email, Password = pass });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task CanLogout()
    {
        var user = "admin@eesti.ee";
        var pass = "Kala.maja1";
        
        await _client.PostAsJsonAsync("/api/v1.0/identity/Account/Login", new { email = user, password = pass });
        
        var res = await _client.PostAsJsonAsync("api/v1.0/identity/Account/Logout", "");
        
        Assert.Equal(HttpStatusCode.Unauthorized, res.StatusCode);
    }
}
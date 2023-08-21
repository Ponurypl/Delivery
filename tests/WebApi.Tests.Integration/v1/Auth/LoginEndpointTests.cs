using MultiProject.Delivery.WebApi.v1.Auth.Login;
using System.Net;
using System.Net.Http.Json;

namespace MultiProject.Delivery.WebApi.Tests.Integration.v1.Auth;

[Collection("Default")]
public class LoginEndpointTests : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _seedDatabase;
    private readonly Func<Task> _resetDatabase;

    public LoginEndpointTests(CustomWebApplicationFactory app)
    {
        _client = app.HttpClient;
        _seedDatabase = app.SeedDatabaseAsync;
        _resetDatabase = app.ResetDatabaseAsync;
    }

    [Fact]
    public async void Login_WhenInvalidPasswordProvided_Then400IsReturned()
    {
        //Arrange
        var body = JsonContent.Create(new { Username = "xxx", Password = "xxx" });

        //Act
        var result = await _client.PostAsync("/api/v1/auth/login", body);

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void Login_WhenValidEntryProvided_Then200IsReturnedWithJwt()
    {
        //Arrange
        var body = new { Username = "PanPaweł", Password = "OJakieHaslo11!!@" };
        await _seedDatabase();

        //Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", body);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
        
        loginResponse.RefreshToken.Should().NotBeNull();
        loginResponse.AccessToken.Should().NotBeNull();
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _resetDatabase();
}

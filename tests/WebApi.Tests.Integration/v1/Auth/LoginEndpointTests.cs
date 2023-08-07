using MultiProject.Delivery.WebApi.v1.Auth.Login;
using System.Net;
using System.Net.Http.Json;

namespace MultiProject.Delivery.WebApi.Tests.Integration.v1.Auth;

public class LoginEndpointTests
{
    private readonly HttpClient _client;
    private readonly Func<Task> _seedDatabase;

    public LoginEndpointTests()
    {
        var app = new CustomWebApplicationFactory();
        app.InitializeAsync().Wait();

        _client = app.CreateClient();
        _seedDatabase = app.SeedDatabaseAsync;
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
}

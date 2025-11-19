using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using WebApi.Models.Users;

namespace Tests;

public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsersControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateUser_AddsUserSuccessfully()
    {
        var request = new UserCreateRequest
        {
            FirstName = "Hans",
            LastName = "Mattin-Lassei",
            Email = "hans@domain.com"
        };

        var response = await _client.PostAsJsonAsync("/api/users", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<UserResult<User>>();
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("Hans", result.Result!.FirstName);
        Assert.False(string.IsNullOrWhiteSpace(result.Result.Id));
    }

    [Fact]
    public async Task GetAllUsers_ReturnsUsersList()
    {
        var response = await _client.GetAsync("/api/users");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<UserResult<IEnumerable<User>>>();
        Assert.NotNull(result);
        Assert.True(result.Success);

        var users = result.Result!.ToList();

        Assert.True(users.Count >= 2);
        Assert.Contains(users, x => x.Email == "john.doe@domain.com");
        Assert.Contains(users, x => x.Email == "jane.doe@domain.com");
    }
}
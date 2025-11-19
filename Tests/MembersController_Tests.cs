using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using WebApi.Models.Members;

namespace Tests;

public class MembersControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public MembersControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateMember_AddsMemberSuccessfully()
    {
        var request = new MemberCreateRequest
        {
            FirstName = "Hans",
            LastName = "Mattin-Lassei",
            Email = "hans@domain.com"
        };

        var response = await _client.PostAsJsonAsync("/api/members", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<MemberResult<Member>>();
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("Hans", result.Result!.FirstName);
        Assert.False(string.IsNullOrWhiteSpace(result.Result.Id));
    }

    [Fact]
    public async Task GetAllMembers_ReturnsMembersList()
    {
        var response = await _client.GetAsync("/api/members");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<MemberResult<IEnumerable<Member>>>();
        Assert.NotNull(result);
        Assert.True(result.Success);

        var Members = result.Result!.ToList();

        Assert.True(Members.Count >= 2);
        Assert.Contains(Members, x => x.Email == "john.doe@domain.com");
        Assert.Contains(Members, x => x.Email == "jane.doe@domain.com");
    }
}
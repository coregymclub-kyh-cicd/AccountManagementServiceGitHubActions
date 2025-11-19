using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Members;
using WebApi.Models.Users;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private static List<User> _users =
    [
        new User { Id = "2323ceab-109f-4df1-a660-d53878f39cec", FirstName = "John", LastName = "Doe", Email = "john.doe@domain.com" },
        new User { Id = "72bf7299-c76d-43dc-b688-6c7967ec397d", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@domain.com" },
    ];


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        await Task.Delay(100);
        return Ok(new UserResult<IEnumerable<User>> { Result = _users, Success = true, Description = "List of Users" });
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateRequest request)
    {
        await Task.Delay(100);
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        _users.Add(user);
        return Ok(new UserResult<User> { Result = user, Success = true, Description = "User created successfully" });
    }
}

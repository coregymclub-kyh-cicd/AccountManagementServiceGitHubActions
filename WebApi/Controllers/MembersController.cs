using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Members;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private static List<Member> _members =
    [
        new Member { Id = "0583b419-4022-4955-9146-abc4f849d3d4", FirstName = "John", LastName = "Doe", Email = "john.doe@domain.com" },
        new Member { Id = "f778824e-6b00-4fd8-afea-ab3db772804e", FirstName = "Jane", LastName = "Doe", Email = "jane.doe@domain.com" },
    ];


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        await Task.Delay(100);
        return Ok(new MemberResult<IEnumerable<Member>> { Result = _members, Success = true, Description = "List of Members" });
    }

    [HttpPost]
    public async Task<IActionResult> Create(MemberCreateRequest request)
    {
        return Ok();
    }
}

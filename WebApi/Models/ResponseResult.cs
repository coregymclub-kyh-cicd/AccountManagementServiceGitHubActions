namespace WebApi.Models;

public class ResponseResult
{
    public bool Success { get; set; }
    public string? Error { get; set; } 
    public string? Description { get; set; }
}

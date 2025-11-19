namespace WebApi.Models.Users;

public class UserResult<T> : ResponseResult
{
    public T? Result { get; set; }
}
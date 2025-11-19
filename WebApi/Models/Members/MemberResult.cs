namespace WebApi.Models.Members;

public class MemberResult<T> : ResponseResult
{
    public T? Result { get; set; }
}

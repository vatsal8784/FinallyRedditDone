namespace Shared.DTO;

public class RegisterUserDTO
{
    public string UserName{ get; init; }
    public string Password{ get; init; }

    public RegisterUserDTO(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
    public RegisterUserDTO()
    {
    }

}
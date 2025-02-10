namespace FactApi.Domain.ViewModels;
    
public partial class UserRequestModel
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int CompanyId { get; set; }
}

public partial class UserResponseModel : UserRequestModel
{
    
}


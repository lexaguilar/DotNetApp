using System.ComponentModel.DataAnnotations;

namespace FactApi.Domain.ViewModels;
public class AuthResponseModel
{
    public string Username { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public string Email { get; set; }
    public int CompanyId { get; set; }
}

public class AuthRequestModel
{
    /// <summary>
    /// Username of the user.
    /// </summary>
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(30, ErrorMessage = "Username must be less than 30 characters")]
    [MinLength(5, ErrorMessage = "Username must be more than 5 characters")]

    public string Username { get; set; }
    /// <summary>
    /// Password of the user.
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MaxLength(50, ErrorMessage = "Password must be less than 50 characters")]
    [MinLength(5, ErrorMessage = "Password must be more than 5 characters")]
    public string Password { get; set; }
}

public class AuthCurrentModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int CompanyId { get; set; }
}

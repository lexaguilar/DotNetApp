using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FactApi.Application.Extensions;
using FactApi.Application.Helpers;
using FactApi.Domain.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace FactApi.Application.User;

public interface IUserAuth
{
    Task<(bool Success, AuthResponseModel? User, string? ErrorMsg)> Login(AuthRequestModel model);
    AuthResponseModel GenerateToken(AuthResponseModel model);
}

public class UserAuth : IUserAuth
{
    private readonly IUserRepository userRepository;
    public UserAuth(IUserRepository _userRepository)
    {
        this.userRepository = _userRepository;
    }

    public AuthResponseModel GenerateToken(AuthResponseModel model)
    {
        var ApiKey = Environment.GetEnvironmentVariable(CryptoHelper.ApiKey);
        string jwt = string.Empty;
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        DateTime expires = DateTime.UtcNow.AddHours(2);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = model.GetClaimsIdentity(),
            Expires = expires,
            SigningCredentials = credentials,
        };

        var securityToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        jwt = tokenHandler.WriteToken(securityToken);

        return new AuthResponseModel
        {
            Username = model.Username,
            Token = jwt,
            Expires = expires,
            Email = model.Email,
            CompanyId = model.CompanyId,
        };
    }

    public async Task<(bool Success, AuthResponseModel? User, string? ErrorMsg)> Login(AuthRequestModel model)
    {
        var user = await userRepository.GetUser(model.Username);

        if (user == null)
            return (false, null, "User not found");        

        if (!CryptoHelper.ComparePassword(model.Password, user.Password, user.Salt))
            return (false, null, "Invalid password");

        var response = new AuthResponseModel
        {
            Username = user.Username,
            Email = user.Email,
            CompanyId = user.CompanyId,
        };

        return (true, response, null);
    }
}

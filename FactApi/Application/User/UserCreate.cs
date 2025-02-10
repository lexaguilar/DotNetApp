using FactApi.Application.Helpers;
using FactApi.Domain.Models;
using FactApi.Domain.ViewModels;

namespace FactApi.Application.User
{
    public interface IUserCreate
    {
        Task<(bool, UserRequestModel, string)> Add(UserRequestModel model);
    }

    public class UserCreate : IUserCreate
    {
        private readonly IUserRepository userRepository;

        public UserCreate(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<(bool, UserRequestModel, string)> Add(UserRequestModel model)
        {

            var userExists = await userRepository.UserExists(model.Username);

            if (userExists)
                return (false, model, "User already exists");

            var passwordTemp = "123456";

            var pwdResult = CryptoHelper.ComputePassword(passwordTemp);

            var user = new Domain.Models.User
            {
                Username = model.Username,
                Email = model.Email,
                CompanyId = model.CompanyId,
                Active = true,
                Password = pwdResult.PasswordHash,
                Salt = pwdResult.Salt,
            };

            await userRepository.Register(user);
            await userRepository.SaveChanges();

            return (true, model, "");
        }
    }

}
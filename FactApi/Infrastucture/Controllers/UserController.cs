using FactApi.Application.User;
using FactApi.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactApi.Infrastucture.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserCreate userCreate;
        private readonly ILogger<UserController> logger;

        public UserController(IUserAuth _userAuth, IUserCreate _userCreate, ILogger<UserController> _logger)
        {
            this.userCreate = _userCreate;
            this.logger = _logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRequestModel model)
        {

            var (success, user, errorMsg) = await userCreate.Add(model);

            if (!success)
                return BadRequest(new { message = errorMsg });

            return new JsonResult(user);

        }
    }
}
using FactApi.Application.User;
using FactApi.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FactApi.Infrastucture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuth userAuth;
        private readonly ILogger<AuthController> logger;

        public AuthController(IUserAuth _userAuth, IUserCreate _userCreate, ILogger<AuthController> _logger)
        {
            this.userAuth = _userAuth;
            this.logger = _logger;
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="model">AuthRequestModel.</param>
        /// <returns>AuthResponseModel.</returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(AuthResponseModel), 200)]
        public async Task<IActionResult> Login([FromBody] AuthRequestModel model)
        {
            logger.LogInformation("Login attempt user: {0} at {1}", model.Username, DateTime.Now);
            var (success, user, errorMsg) = await userAuth.Login(model);

            if (!success)
                return BadRequest(new { message = errorMsg });

            var userAuthed = userAuth.GenerateToken(user);

            return new JsonResult(userAuthed);
        }
    }
}
using FactApi.Application.User;
using FactApi.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactApi.Infrastucture.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientManager clientManager;
        private readonly ILogger<ClientsController> logger;

        public ClientsController(IUserAuth _userAuth, IClientManager clientManager, ILogger<ClientsController> _logger)
        {
            this.clientManager = clientManager;
            this.logger = _logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] ClientRequestCreateModel model)
        {

            var (success, user, errorMsg) = await clientManager.Add(model);

            if (!success)
                return BadRequest(new { message = errorMsg });

            return new JsonResult(user);

        }

        [HttpGet("GetList")]
        public  async Task<IActionResult> GetList()
        {
            var clients = await clientManager.List();

            return new JsonResult(clients);
        }
    }
}
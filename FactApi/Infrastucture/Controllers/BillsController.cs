using System.Reflection;
using FactApi.Application.User;
using FactApi.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactApi.Infrastucture.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : ControllerBase
    {
        private readonly IBillManager billManager;
        private readonly ILogger<BillsController> logger;

        public BillsController(IBillManager billManager, ILogger<BillsController> logger)
        {
            this.billManager = billManager;
            this.logger = logger;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(int skip, int take,[FromQuery] IDictionary<string, string> values)
        {
            var result = await billManager.GetList(skip, take, values);

            return new JsonResult(result);
        }

        [HttpGet("GetEnvironments")]
        public async Task<IActionResult> GetEnvironments()
        {
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_KEY");

            return new JsonResult(new { accessKey, secretKey });
        }

        [HttpGet("GetVersion")]
        public async Task<IActionResult> GetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return new JsonResult(new { version });
        }
    }
}
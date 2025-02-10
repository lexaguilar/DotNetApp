using FactApi.Application.Extensions;
using FactApi.Application.User;
using FactApi.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactApi.Infrastucture.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContractsController : ControllerBase
    {
        private readonly IContractManager contractManager;
        private readonly ILogger<ContractsController> logger;

        public ContractsController(IContractManager contractManager, ILogger<ContractsController> logger)
        {
            this.contractManager = contractManager;
            this.logger = logger;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await contractManager.GetById(id);
            return new JsonResult(result);

        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {

            var result = await this.contractManager.GetList();
            return new JsonResult(result);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] ContractRequestCreateModel model)
        {
            var user = this.GetUser();
            var result = await this.contractManager.Register(model, user);
            return new JsonResult(result);

        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file, [FromQuery] int contractId)
        {
            var result = await this.contractManager.Upload(file, contractId);
            return new JsonResult(result);
        }
       

        [HttpGet("DownloadDocument")]
        public async Task<IActionResult> DownloadDocument([FromQuery] int contractId)
        {
            var result = this.contractManager.DownloadDocument(contractId);
            return result;

        }
    }
}
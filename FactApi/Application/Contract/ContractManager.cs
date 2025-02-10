
using FactApi.Domain.Models;
using FactApi.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FactApi.Application.User
{
    public interface IContractManager
    {
       
        Task<ContractResponseModel> GetById(int id);

        Task<IEnumerable<ContractResponseModel>> GetList();

        Task<ContractResponseModel> Register(ContractRequestCreateModel contract,  AuthCurrentModel user);
        Task<int> Upload(IFormFile file, int contractId);
        FileStreamResult DownloadDocument(int contractId);
    }

    public class ContractManager : IContractManager
    {
        private readonly IContractRepository contractRepository;

        private IWebHostEnvironment hostingEnvironment;

        public ContractManager(IContractRepository contractRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.contractRepository = contractRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<ContractResponseModel> GetById(int id)
        {
            var contract = await this.contractRepository.GetById(id);
            return this.TransformContractToContractResponseModel(contract);
        }

        public async Task<IEnumerable<ContractResponseModel>> GetList()
        {
            var contracts = this.contractRepository.GetList().Result;
            var result = contracts.Select(x => new ContractResponseModel
            {
                Id = x.Id,
                Client = x.Client.Name,
                ClientAddress = x.Client?.Address ?? string.Empty,
                ClientPhoneNumber = x.Client?.PhoneNumber ?? string.Empty,
                Description = x.Description,
                ContractDate = x.ContractDate,
                Init = x.Init,
                EndDate = x.EndDate,
                UrlDocument = x.PathDocument,
                CreatedBy = x.CreatedBy,
            });
            return result;
        }

        public async Task<ContractResponseModel> Register(ContractRequestCreateModel contract, AuthCurrentModel user)
        {
            var newContract = new Contract
            {
                ClientId = contract.ClientId,
                Description = contract.Description,
                ContractDate = contract.ContractDate,
                Init = contract.ContractDate,
                EndDate = contract.ContractDate.AddYears(1),
                CreatedBy = user.Username,
                PathDocument = string.Empty,
            };

            await this.contractRepository.Register(newContract);
            await this.contractRepository.SaveChanges();

            return this.TransformContractToContractResponseModel(newContract);
        }

        public FileStreamResult DownloadDocument(int contractId)
        {

            var contract = this.contractRepository.GetById(contractId).Result;
            var pathDocument = contract.PathDocument;

            if (string.IsNullOrEmpty(pathDocument))
                return null;

            var fileInfo = this.hostingEnvironment.WebRootFileProvider.GetFileInfo(pathDocument);
            return new FileStreamResult(fileInfo.CreateReadStream(), "application/pdf");

        }

        private ContractResponseModel TransformContractToContractResponseModel(Contract contract)
        {
            return new ContractResponseModel
            {
                Id = contract.Id,
                Description = contract.Description,
                ContractDate = contract.ContractDate,
                Init = contract.Init,
                EndDate = contract.EndDate,
                UrlDocument = contract.PathDocument,
                CreatedBy = contract.CreatedBy,
            };
        }

        public async Task<int> Upload(IFormFile file, int contractId)
        {
            var newContract = await this.contractRepository.GetById(contractId);
            if (newContract == null)
                return -1;

            string uploads = System.IO.Path.Combine(this.hostingEnvironment.ContentRootPath,"wwwroot", "uploads");
            if (file.Length > 0)
            {

                string filePath = System.IO.Path.Combine(uploads, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                newContract.PathDocument = System.IO.Path.Combine("uploads", file.FileName);

                await this.contractRepository.SaveChanges();
            }

            return 1;
        }
       
    }

}
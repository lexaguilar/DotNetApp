
using FactApi.Domain.Models;
using FactApi.Domain.ViewModels;

namespace FactApi.Application.User
{
    public interface IClientManager
    {
        Task<(bool, ClientReponseModel, string)> Add(ClientRequestCreateModel model);

        Task<IEnumerable<ClientReponseModel>> List();
    }
   

    public class ClientManager : IClientManager
    {
        private readonly IClientRepository clientRepository;

        public ClientManager(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<(bool, ClientReponseModel, string)> Add(ClientRequestCreateModel model)
        {

            if (string.IsNullOrEmpty(model.Email))
                return (false, null, "Email is required");

            var client = new Client
            {
                Name = model.Name,
                Birthdate = model.Birthdate,
                ProfessionId = model.ProfessionId,
                Email = model.Email,
                Address = model.Address,
                Active = true,
                PhoneNumber = model.PhoneNumber,
            };

            var clientAdded = await this.clientRepository.Register(client);

            if (!await this.clientRepository.SaveChanges())
                return (false, null, "Error saving client");

            return (true, new ClientReponseModel
            {
                Id = clientAdded.Id,
                Name = clientAdded.Name,
                Birthdate = clientAdded.Birthdate,
                ProfessionId = clientAdded.ProfessionId,
                Email = clientAdded.Email,
                Address = clientAdded.Address,
                Active = clientAdded.Active,
                PhoneNumber = clientAdded.PhoneNumber,
            }, "");
        }

        public async Task<IEnumerable<ClientReponseModel>> List()
        {
            var clients = await this.clientRepository.ToList();

            var result = clients.Select(x => new ClientReponseModel
            {
                Id = x.Id,
                Name = x.Name,
                Birthdate = x.Birthdate,
                ProfessionId = x.ProfessionId,
                Profession = x.Profession.Name,
                Email = x.Email,
                Address = x.Address,
                Active = x.Active,
                PhoneNumber = x.PhoneNumber,
            }).ToArray();

            return result;
        }
    }

}
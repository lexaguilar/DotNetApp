
using FactApi.Domain.Models;
using FactApi.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FactApi.Application.User
{
    public interface IBillManager
    {
        Task<BillDataResponseModel> GetList(int skip, int take, IDictionary<string, string> values);
    }

    public class BillManager : IBillManager
    {
        private readonly IBillRepository billRepository;

        public BillManager(IBillRepository billRepository)
        {
            this.billRepository = billRepository;
        }

        public async Task<BillDataResponseModel> GetList(int skip, int take, IDictionary<string, string> values)
        {
            var bills = billRepository.GetList();

            if (values.ContainsKey("id"))
            {
                var id = Convert.ToInt32(values["id"]);
                bills = bills.Where(x => x.Id == id);
            }

            if (values.ContainsKey("client"))
            {
                var client = Convert.ToString(values["client"]);
                bills = bills.Where(x => x.Client.Name.Contains(client));
            }

            if (values.ContainsKey("professionId"))
            {
                var professionId = Convert.ToInt32(values["professionId"]);
                bills = bills.Where(x => x.Client.ProfessionId == professionId);
            }

            if (values.ContainsKey("billDate")){
                var billDate = Convert.ToDateTime(values["billDate"]);
                bills = bills.Where(x => x.BillDate == billDate);
            }

            if (values.ContainsKey("createdAt"))
            {
                var createdAt = Convert.ToDateTime(values["createdAt"]);
                bills = bills.Where(x => x.CreatedAt == createdAt);
            }

            if (values.ContainsKey("createdBy"))
            {
                var createdBy = Convert.ToString(values["createdBy"]);
                bills = bills.Where(x => x.CreatedBy == createdBy);
            }

            var totalCount = bills.Count();

            bills = bills.Skip(skip).Take(take);

            var items = await bills.Select(x => new BillResponseModel
            {
                Id = x.Id,
                Client = x.Client.Name,
                ClientAddress = x.Client.Address,
                ClientPhoneNumber = x.Client.PhoneNumber,
                ProfessionId = x.Client.ProfessionId,
                Profession = x.Client.Profession.Name,
                BillDate = x.BillDate,
                Total = x.Total,
                Observation = x.Observation,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy
            }).ToListAsync();

            return new BillDataResponseModel
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }

}
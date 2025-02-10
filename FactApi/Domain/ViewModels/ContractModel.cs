namespace FactApi.Domain.ViewModels;

public class ContractResponseModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string Client { get; set; }
    public string ClientAddress { get; set; }
    public string ClientPhoneNumber { get; set; }
    public string Description { get; set; } = null!;
    public DateTime ContractDate { get; set; }
    public DateTime Init { get; set; }
    public DateTime EndDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string UrlDocument { get; set; } = null!;

}

public class ContractRequestCreateModel
{
    public int ClientId { get; set; }
    public string Description { get; set; } = null!;

    public DateTime ContractDate { get; set; }

}

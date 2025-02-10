namespace FactApi.Domain.ViewModels;


public class BillResponseModel
{
    public int Id { get; set; }

    public string Client { get; set; }
    
    public string ClientAddress { get; set; }
    public string ClientPhoneNumber { get; set; }

    public int ProfessionId { get; set; }
    public string Profession { get; set; }

    public DateTime BillDate { get; set; }

    public decimal Total { get; set; }

    public string? Observation { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;


}

public class BillDataResponseModel
{
    public IEnumerable<BillResponseModel> Items { get; set; } = null!;

    public int TotalCount { get; set; }
}

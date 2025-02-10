namespace FactApi.Domain.ViewModels;

public partial class ClientRequestCreateModel
{
    public string Name { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public int ProfessionId { get; set; }

    public string? Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

}

public partial class ClientRequestModel
{
    public string Name { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public int ProfessionId { get; set; }

    public string Profession { get; set; } = null!;

    public bool Active { get; set; }

    public string? Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

}

public class ClientReponseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public int ProfessionId { get; set; }

    public string Profession { get; set; } = null!;

    public bool Active { get; set; }

    public string? Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

}

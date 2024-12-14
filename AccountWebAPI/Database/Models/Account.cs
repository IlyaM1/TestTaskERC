namespace AccountWebAPI.Database.Models;

public class Account : BaseModel
{
    public required string AccountNumber { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required string Address { get; set; }
    public required double Area { get; set; }

    public virtual List<Resident> Residents { get; set; } = [];
}
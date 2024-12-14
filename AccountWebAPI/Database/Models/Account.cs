namespace AccountWebAPI.Database.Models;

public class Account : BaseModel
{
    public string AccountNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Address { get; set; }
    public double Area { get; set; }

    public virtual List<Resident> Residents { get; set; } = [];
}
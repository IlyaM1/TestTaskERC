using AccountWebAPI.Database.Models;

namespace AccountWebAPI.Dtos;

public class PatchAccountDto : BasePatchDto<Account>
{
    public string? AccountNumber { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Address { get; set; }
    public double? Area { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace AccountWebAPI;

public class Account
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string AccountNumber { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public double Area { get; set; }

    [Required]
    public string ResidentsIds { get; set; }


    public List<int> GetResidentsIds()
    {
        return ResidentsIds
            .Split(',')
            .Select(number => int.Parse(number.Trim()))
            .ToList();
    }

    public void SetResidentIds(List<int> ids)
    {
        ResidentsIds = string.Join(',', ids);
    }

    public void AddResidentId(int id)
    {
        var ids = GetResidentsIds();
        ids.Add(id);
        SetResidentIds(ids);
    }
}
namespace AccountManagingServer;

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Address { get; set; }
    public double Area { get; set; }
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
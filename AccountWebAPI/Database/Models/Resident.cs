namespace AccountWebAPI.Database.Models;

public class Resident : BaseModel
{
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string Middlename { get; set; }
}

namespace RegistryOffice.Domain.Models;

public class MarriageRecord
{
    public int Id { get; set; }
    public int HusbandId { get; set; }
    public int WifeId { get; set; }
    public string MarriagePlace { get; set; }
    public DateTime MarriageDate { get; set; }

    public MarriageRecord(
        int id,
        int husbandId,
        int wifeId,
        string marriagePlace,
        DateTime marriageDate)
    {
        Id = id;
        HusbandId = husbandId;
        WifeId = wifeId;
        MarriagePlace = marriagePlace;
        MarriageDate = marriageDate;
    }

    public (MarriageRecord marriageRecord, string Error) Create(
        int id,
        int husbandId,
        int wifeId,
        string marriagePlace,
        DateTime marriageDate)
    {
        string error = string.Empty;
        
        MarriageRecord marriageRecord = new(id, husbandId, wifeId, marriagePlace, marriageDate);
        
        //if condition
        
        return (marriageRecord, error);
    }
}
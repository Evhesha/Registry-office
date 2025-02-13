namespace RegistryOffice.Domain.Models;

public class DivorceRecord
{
    public int Id { get; set; }
    public int MarriageId { get; set; }
    public string CauseOfDivorce { get; set; }
    public DateTime DateOfDivorce { get; set; }

    public DivorceRecord(
        int id,
        int marriageId,
        string causeOfDivorce,
        DateTime dateOfDivorce)
    {
        Id = id;
        MarriageId = marriageId;
        CauseOfDivorce = causeOfDivorce;
        DateOfDivorce = dateOfDivorce;
    }

    public (DivorceRecord divorceRecord, string Error) Create(
        int id,
        int marriageId,
        string causeOfDivorce,
        DateTime dateOfDivorce)
    {
        string error = string.Empty;
        
        DivorceRecord divorceRecord = new(id, marriageId, causeOfDivorce, dateOfDivorce);
        
        // if condition
        
        return (divorceRecord, error);
    }
}
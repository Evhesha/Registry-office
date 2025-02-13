namespace RegistryOffice.Domain.Models;

public class BirthRecord
{
    public int Id { get; set; }
    public int CitizenId { get; set; }
    public int FatherId { get; set; }
    public int MotherId { get; set; }
    public DateTime BirthDate { get; set; }

    private BirthRecord(
        int id,
        int citizenId,
        int fatherId,
        int motherId,
        DateTime birthDate)
    {
        Id = id;
        CitizenId = citizenId;
        FatherId = fatherId;
        MotherId = motherId;
        BirthDate = birthDate;
    }

    public (BirthRecord birthRecord, string Error) Create(
        int id,
        int citizenId,
        int fatherId,
        int motherId,
        DateTime birthDate)
    {
        string error = string.Empty;
        
        BirthRecord birthRecord = new(id, citizenId, fatherId, motherId, birthDate);
        
        // if condiotion
        
        return (birthRecord, error);    
    } 
}
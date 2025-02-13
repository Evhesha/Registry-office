namespace RegistryOffice.Domain.Models;

public class DeathRecord
{
    public int Id { get; set; }
    public int CitizenId { get; set; }
    public string CauseOfDeath { get; set; } = string.Empty;
    public DateTime DeathDate { get; set; }

    private DeathRecord(
        int id,
        int citizenId,
        string causeOfDeath,
        DateTime deathDate)
    {
        Id = id;
        CitizenId = citizenId;
        CauseOfDeath = causeOfDeath;
        DeathDate = deathDate;
    }

    public (DeathRecord deathRecord, string Error) Create(
        int id,
        int citizenId,
        string causeOfDeath,
        DateTime deathDate)
    {
        string error = string.Empty;
        
        DeathRecord deathRecord = new(id, citizenId, causeOfDeath, deathDate);
        
        // if condition
        
        return (deathRecord, error);
    }
}
using System.Runtime.InteropServices.JavaScript;

namespace RegistryOffice.Domain.Models;

public class Citizen
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string BornPlace { get; set; }
    public char Gender { get; set; }
    
    private Citizen(
        int id,
        string firstName,
        string lastName,
        DateTime birthDate,
        string bornPlace,
        char gender)
    {
        Id = id;
        FirstName = firstName;  
        LastName = lastName;
        BirthDate = birthDate;  
        BornPlace = bornPlace;  
        Gender = gender;
    }

    public (Citizen Citizen, string Error) CreateCitizen(
        int id,
        string firstName,
        string lastName,
        DateTime birthDate,
        string bornPlace,
        char gender)
    {
        string error = string.Empty;
        
        Citizen citizen = new(id, firstName, lastName, birthDate, bornPlace, gender);
        
        // if condition
        
        return (citizen, error);
    }
}
using AquariumAutomationAPI.Models;

namespace AquariumAutomationAPI.Repository
{
    public interface IDataRepository
    {
        User? GetUserByEmail(string Email);
        User? RegisterUserToDb(User registerUser);
        Aquarium? CreateNewAquariumToDb(Aquarium aquarium);
        Aquarium? GetAquariumInfoByAquariumId(int AquariumId);
        List<Aquarium> GetAquariumInfoByUserId(int UserId);
        int UpdateAquarium(Aquarium aquarium);
        int DeleteAquarium(int aquariumId);
    }
}

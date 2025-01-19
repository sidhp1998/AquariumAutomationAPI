using AquariumAutomationAPI.Models;

namespace AquariumAutomationAPI.Repository
{
    public interface IDataRepository
    {
        User? GetUserByEmail(string Email);
        User? RegisterUserToDb(User registerUser);
        Aquarium? CreateNewAquariumToDb(Aquarium aquarium);
        Aquarium? GetAquariumInfoById(int UserId);
    }
}

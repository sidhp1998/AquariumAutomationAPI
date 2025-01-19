using AquariumAutomationAPI.Models;

namespace AquariumAutomationAPI.Repository
{
    public interface IDataRepository
    {
        User? GetUserByEmail(string Email);
        User RegisterUserToDb(User registerUser);
    }
}

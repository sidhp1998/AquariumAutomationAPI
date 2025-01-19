using AquariumAutomationAPI.Models;

namespace AquariumAutomationAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

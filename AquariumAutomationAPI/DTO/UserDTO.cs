namespace AquariumAutomationAPI.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
    }
}

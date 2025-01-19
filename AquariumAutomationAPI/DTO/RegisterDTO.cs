namespace AquariumAutomationAPI.DTO
{
    public class RegisterDTO
    {
        public required string UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public required string UserEmail { get; set; }
        public required string UserPhoneNumber { get; set; }

        public required string Password { get; set; }
    }
}

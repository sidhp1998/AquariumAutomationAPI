namespace AquariumAutomationAPI.Models
{
    public class User
    {
        public virtual required int UserId { get; set; }
        public virtual required int AccountId { get; set; }
        public required string UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public required string UserEmail { get; set; }
        public required string PhoneNumber { get; set; }
        public required int UserTypeId { get; set; }
        public required DateTime UserCreatedOnDate { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public required DateTime AccountCreatedDate { get; set; }

    }
}

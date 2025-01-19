using System.ComponentModel.DataAnnotations;

namespace AquariumAutomationAPI.DTO
{
    public class LoginDTO
    {
        [Required]
        [MaxLength(50)]
        public required string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Password { get; set; }
    }
}

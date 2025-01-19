namespace AquariumAutomationAPI.DTO
{
    public class AquariumDTO
    {
        public required string AquariumName { get; set; }
        public string? AquariumDescription { get; set; }
        public required int UserId { get; set; }
        public string? AquariumComments { get; set; }
        public string? AquariumFixedPropertyComments { get; set; }
        public required int Length { get; set; }
        public required int Height { get; set; }
        public required int Width { get; set; }

    }
}

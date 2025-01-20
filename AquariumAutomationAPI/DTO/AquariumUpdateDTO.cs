namespace AquariumAutomationAPI.DTO
{
    public class AquariumUpdateDTO
    {
        public int AquariumId { get; set; } = -1;
        public string? AquariumName { get; set; }
        public string? AquariumDescription { get; set; }
        public string? AquariumComments { get; set; }
        public int AquariumFixedPropertyId { get; set; } = -1;
        public string? AquariumFixedPropertyComments { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

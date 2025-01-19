namespace AquariumAutomationAPI.Models
{
    public class Aquarium
    {
        public int AquariumId { get; set; } = -1;
        public string? AquariumName { get; set; }
        public string? AquariumDescription { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public string? AquariumComments { get; set; }
        public DateTime? AquariumCreatedDate { get; set; }

        public string? AquariumFixedPropertyComments { get; set; }
        public int AquariumFixedPropertyId { get; set; } = -1;
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public DateTime? AquariumFixedPropertyCreatedDate { get; set; }

        public int GetVolume()
        {
            return(this.Length*this.Width*this.Height);
        }
    }
}

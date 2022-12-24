namespace Hearthstone.Models
{
    public class CardWithMetaDataDTO
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public string? Class { get; set; }
        public string? Type { get; set; }
        public string? Set { get; set; }
        public int? SpellSchoolId { get; set; }
        public string? Rarity { get; set; }
        public int? Health { get; set; }
        public int? Attack { get; set; }
        public int ManaCost { get; set; }
        public String? Artist { get; set; }
        public String? Text { get; set; }
        public String? FlavorText { get; set; }
    }
}

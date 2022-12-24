namespace Hearthstone.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CardCollectionName { get; set; }
        public string CardTypeCollectionName { get; set; }
        public string ClassCollectionName { get; set; }
        public string RarityCollectionName { get; set; }
        public string SetCollectionName { get; set; }
    }
}

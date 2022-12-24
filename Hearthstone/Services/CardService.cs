using Hearthstone.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Hearthstone.Services
{
    public class CardService
    {
        private readonly IMongoCollection<Card> _cardCollection;
        private readonly IMongoCollection<Class> _classCollection;
        private readonly IMongoCollection<Set> _setCollection;
        private readonly IMongoCollection<CardType> _cardTypeCollection;
        private readonly IMongoCollection<Rarity> _rarityCollection;

        public CardService(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _cardCollection = database.GetCollection<Card>(mongoSettings.Value.CardCollectionName);
            _classCollection = database.GetCollection<Class>(mongoSettings.Value.ClassCollectionName);
            _setCollection = database.GetCollection<Set>(mongoSettings.Value.SetCollectionName);
            _cardTypeCollection = database.GetCollection<CardType>(mongoSettings.Value.CardTypeCollectionName);
            _rarityCollection = database.GetCollection<Rarity>(mongoSettings.Value.RarityCollectionName);
        }

        public void CreateCards()
        {
            foreach (var path in new[] { "cards.json" })
            {
                using (var file = new StreamReader(path))
                {
                    var cards = JsonSerializer.Deserialize<List<Card>>(file.ReadToEnd(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    _cardCollection.InsertMany(cards);
                }
            }
        }
    }
}

using Hearthstone.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Hearthstone.Services
{
    public class CardService
    {
        private readonly IMongoCollection<Card> _cardCollection;

        public CardService(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _cardCollection = database.GetCollection<Card>(mongoSettings.Value.CardCollectionName);
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

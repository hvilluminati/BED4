using Hearthstone.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Hearthstone.Services
{
    public class CardTypeService
    {
        private readonly IMongoCollection<CardType> _cardTypeCollection;
        public CardTypeService(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _cardTypeCollection = database.GetCollection<CardType>(mongoSettings.Value.CardTypeCollectionName);
        }

        public async Task<List<CardType>> GetAsync()
        {
            return await _cardTypeCollection.Find(x => true).ToListAsync();
        }

        public void CreateCardTypes()
        {
            foreach (var path in new[] { "metadata.json" })
            {
                using (var file = new StreamReader(path))
                {
                    var metadata = JsonSerializer.Deserialize<MetaData>(file.ReadToEnd(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (metadata == null || metadata.Types == null)
                    {
                        return;
                    }

                    _cardTypeCollection.InsertMany(metadata.Types);
                }
            }
        }
    }
}
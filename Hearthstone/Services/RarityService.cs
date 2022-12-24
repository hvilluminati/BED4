using Hearthstone.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Hearthstone.Services
{
    public class RarityService
    {
        private readonly IMongoCollection<Rarity> _rarityCollection;
        public RarityService(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _rarityCollection = database.GetCollection<Rarity>(mongoSettings.Value.RarityCollectionName);
        }

        public async Task<List<Rarity>> GetAsync()
        {
            return await _rarityCollection.Find(x => true).ToListAsync();
        }

        public void CreateRarities()
        {
            foreach (var path in new[] { "metadata.json" })
            {
                using (var file = new StreamReader(path))
                {
                    var metadata = JsonSerializer.Deserialize<MetaData>(file.ReadToEnd(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (metadata == null || metadata.Rarities == null)
                    {
                        return;
                    }

                    _rarityCollection.InsertMany(metadata.Rarities);
                }
            }
        }
    }
}

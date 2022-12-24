using Hearthstone.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Hearthstone.Services
{
    public class SetService
    {
        private readonly IMongoCollection<Set> _setCollection;
        public SetService(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _setCollection = database.GetCollection<Set>(mongoSettings.Value.SetCollectionName);
        }

        public async Task<List<Set>> GetAsync()
        {
            return await _setCollection.Find(x => true).ToListAsync();
        }

        public void CreateSets()
        {
            foreach (var path in new[] { "metadata.json" })
            {
                using (var file = new StreamReader(path))
                {
                    var metadata = JsonSerializer.Deserialize<MetaData>(file.ReadToEnd(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (metadata == null || metadata.Sets == null)
                    {
                        return;
                    }

                    _setCollection.InsertMany(metadata.Sets);
                }
            }
        }
    }
}

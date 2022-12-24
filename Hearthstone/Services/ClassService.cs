using Hearthstone.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Hearthstone.Services
{
    public class ClassService
    {
        private readonly IMongoCollection<Class> _classCollection;
        public ClassService(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _classCollection = database.GetCollection<Class>(mongoSettings.Value.ClassCollectionName);
        }

        public async Task<List<Class>> GetAsync()
        {
            return await _classCollection.Find(x => true).ToListAsync();
        }

        public void CreateClasses()
        {
            foreach (var path in new[] { "metadata.json" })
            {
                using (var file = new StreamReader(path))
                {
                    var metadata = JsonSerializer.Deserialize<MetaData>(file.ReadToEnd(), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (metadata == null || metadata.Classes == null)
                    {
                        return;
                    }

                    _classCollection.InsertMany(metadata.Classes);
                }
            }
        }
    }
}

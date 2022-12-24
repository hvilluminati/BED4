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

        public async Task<List<CardWithMetaDataDTO>> GetCardsByQuery(QueryParameter parameter)
        {
            var result = new List<Card>();
            var filter = Builders<Card>.Filter.Empty;

            if (parameter.setid != null)
            { filter &= Builders<Card>.Filter.Eq(x => x.SetId, parameter.setid); }

            if (parameter.artist != null)
            { filter &= Builders<Card>.Filter.Eq(x => x.Artist, parameter.artist); }

            if (parameter.classid!= null)
            { filter &= Builders<Card>.Filter.Eq(x => x.ClassId, parameter.classid); }

            if (parameter.rarityid != null)
            { filter &= Builders<Card>.Filter.Eq(x => x.RarityId, parameter.rarityid); }

            if (parameter.page != null)
            { result = await _cardCollection.Find(filter).Skip((parameter.page - 1) * 100).Limit(100).ToListAsync(); }
            else
            { result = await _cardCollection.Find(filter).ToListAsync(); }

            return ListCardsWithMetaData(result);
        }

        public List<CardWithMetaDataDTO> ListCardsWithMetaData(List<Card> cards)
        {
            var query = from c in cards
                        join cl in _classCollection.AsQueryable() on c.ClassId equals cl.Id
                        join ct in _cardTypeCollection.AsQueryable() on c.TypeId equals ct.Id
                        join r in _rarityCollection.AsQueryable() on c.RarityId equals r.Id
                        join s in _setCollection.AsQueryable() on c.SetId equals s.Id

                        select new CardWithMetaDataDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Type = ct.Name,
                            Class = cl.Name,
                            Set = s.Name,
                            SpellSchoolId = c.SpellSchoolId,
                            Rarity = r.Name,
                            Health = c.Health,
                            Attack = c.Attack,
                            ManaCost = c.ManaCost,
                            Artist = c.Artist,
                            Text = c.Text,
                            FlavorText = c.FlavorText,
                        };

            return query.ToList();
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

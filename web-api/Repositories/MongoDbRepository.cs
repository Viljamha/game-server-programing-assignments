using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using web_api.Models;

namespace web_api.Repositories
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> collection;
        private readonly IMongoCollection<BsonDocument> bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = mongoClient.GetDatabase("Game");
            collection = database.GetCollection<Player>("players");
            bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player plr = collection.Find(filter).FirstAsync().Result;
            plr.Items.Add(item);
            collection.ReplaceOneAsync(filter, plr);
            return Task.FromResult(item);
        }

        public Task<Player> CreatePlayer(Player player)
        {
            collection.InsertOneAsync(player);
            return Task.FromResult(player);
        }

        public Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player plr = collection.Find(filter).FirstAsync().Result;
            Item item = plr.Items.Find(x => x.Id == itemId);
            plr.Items.Remove(item);
            collection.ReplaceOneAsync(filter, plr);
            return Task.FromResult(item);
        }

        public Task<Player> DeletePlayer(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player plr = collection.Find(filter).FirstAsync().Result;
            collection.DeleteOneAsync(filter);
            return Task.FromResult(plr);
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player plr = collection.Find(filter).FirstAsync().Result;
            return Task.FromResult(plr.Items.ToArray());
        }

        public Task<Player[]> GetAllPlayers()
        {
            List<Player> players = collection.Find(new BsonDocument()).ToListAsync().Result;
            return Task.FromResult(players.ToArray());
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player plr = collection.Find(filter).FirstAsync().Result;
            Item item = plr.Items.Find(x => x.Id == itemId);
            return Task.FromResult(item);
        }

        public Task<Player> GetPlayer(Guid playerId)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Id", playerId);
            return collection.Find(filter).FirstAsync();
        }

        public Task<Item> ModifyItem(Guid playerId, Guid itemId, ModifiedItem item)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player plr = collection.Find(filter).FirstAsync().Result;
            Item itm = plr.Items.Find(x => x.Id == itemId);
            itm.Level = item.Level;
            collection.ReplaceOneAsync(filter, plr);
            return Task.FromResult(itm);
        }

        public Task<Player> ModifyPlayer(Guid playerId, ModifiedPlayer player)
        {
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            Player plr = collection.Find(filter).FirstAsync().Result;
            plr.Score = player.Score;
            plr.Level = player.Level;
            collection.ReplaceOneAsync(filter, plr);
            return Task.FromResult(plr);
        }
    }
}
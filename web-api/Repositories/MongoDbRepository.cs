using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using web_api.Models;
using MongoDB.Bson.Serialization;

namespace web_api.Repositories
{
    public class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> collection;
        private readonly IMongoCollection<Log> auditLog;
        private readonly IMongoCollection<BsonDocument> bsonDocumentCollection;

        public MongoDbRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = mongoClient.GetDatabase("Game");
            collection = database.GetCollection<Player>("players");
            auditLog = database.GetCollection<Log>("log");
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

        public Task<Player[]> GetPlayersWithGteScore(int score)
        {
            var builder = Builders<Player>.Filter;
            var filter = builder.Gte("Score", score);
            List<Player> players = collection.Find(filter).ToListAsync().Result;
            return Task.FromResult(players.ToArray());
        }

        public Task<Player> GetPlayerByName(string name)
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Name", name);
            return collection.Find(filter).FirstAsync();
        }

        public Task<Player[]> GetPlayersByTag(PlayerTags tag)
        {
            var builder = Builders<Player>.Filter;
            var filter = builder.Eq("Tag", tag);
            List<Player> players = collection.Find(filter).ToListAsync().Result;
            return Task.FromResult(players.ToArray());
        }

        public Task<int> GetMostCommonLevel()
        {
            var aggregate = collection.Aggregate()
                .Project(r => new {Level = r.Level})
                .Group(r => r.Level, x => new {Level = x.Key, Count = x.Sum(o=>1)})
                .SortByDescending(r => r.Count)
                .Limit(3);
            var results = aggregate.ToList();

            return Task.FromResult(results[0].Level);
        }

        public Task<Player[]> GetPlayersWithItemCount(int itemCount)
        {
            var builder = Builders<Player>.Filter;
            var filter = builder.Size("Items", itemCount);
            List<Player> players = collection.Find(filter).ToListAsync().Result;
            return Task.FromResult(players.ToArray());
        }

        public Task<Player[]> GetPlayersWithItemType(ItemTypes type)
        {
            var builder = Builders<Player>.Filter;
            var filter = builder.Eq("Items", new BsonDocument{ {"Type", type} });
            List<Player> players = collection.Find(filter).ToListAsync().Result;
            return Task.FromResult(players.ToArray());
        }

        public Task<Player[]> GetAllPlayersDescending() {
            var filter = Builders<Player>.Filter.Empty;
            var sort = Builders<Player>.Sort.Descending("Score");

            var cursor = collection.Find(filter).Sort(sort);
            var players = cursor.ToList().Take(10);

            return Task.FromResult(players.ToArray());
        }

        public Task<Log> LogRequestStart(string ip, DateTime date)
        {
            Log newLog = new Log("An request from ip address " + ip + " to delete player started at " + date.ToString());
            auditLog.InsertOneAsync(newLog);
            return Task.FromResult(newLog);
        }

        public Task<Log> LogRequestFinished(string ip, DateTime date)
        {
            Log newLog = new Log("An request from ip address " + ip + " to delete player ended at " + date.ToString());
            auditLog.InsertOneAsync(newLog);
            return Task.FromResult(newLog);
        }

        public Task<Log[]> GetAllLogs()
        {
            List<Log> logs = auditLog.Find(new BsonDocument()).ToListAsync().Result;
            return Task.FromResult(logs.ToArray());
        }
    }
}
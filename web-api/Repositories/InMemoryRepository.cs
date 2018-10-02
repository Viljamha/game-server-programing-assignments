using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using web_api.Models;

namespace web_api.Repositories
{
    public class InMemoryRepository : IRepository
    {
        List<Player> players = new List<Player>();

        public Task<Player> GetPlayer(Guid playerId) {
            return Task.FromResult(players.Find(x => x.Id == playerId));
        }
        public Task<Player[]> GetAllPlayers() {
            return Task.FromResult(players.ToArray());
        }
        public Task<Player> CreatePlayer(Player player) {
            players.Add(player);
            return Task.FromResult(player);
        }
        public Task<Player> ModifyPlayer(Guid playerId, ModifiedPlayer player) {
            Player plr = players.Find(x => x.Id == playerId);
            plr.Score = player.Score;
            plr.Level = player.Level;
            return Task.FromResult(plr);
        }
        public Task<Player> DeletePlayer(Guid playerId) {
            Player plr = players.Find(x => x.Id == playerId);
            players.Remove(plr);
            return Task.FromResult(plr);
        }

        public Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            Player plr = players.Find(x => x.Id == playerId);
            return Task.FromResult(plr.Items.Find(x => x.Id == itemId));
        }

        public Task<Item[]> GetAllItems(Guid playerId)
        {
            Player plr = players.Find(x => x.Id == playerId);
            return Task.FromResult(plr.Items.ToArray());
        }

        public Task<Item> CreateItem(Guid playerId, Item item)
        {
            Player plr = players.Find(x => x.Id == playerId);
            plr.Items.Add(item);
            return Task.FromResult(item);
        }

        public Task<Item> ModifyItem(Guid playerId, Guid itemId, ModifiedItem item)
        {
            Player plr = players.Find(x => x.Id == playerId);
            Item itm = plr.Items.Find(x => x.Id == itemId);
            itm.Level = item.Level;
            return Task.FromResult(itm);
        }

        public Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            Player plr = players.Find(x => x.Id == playerId);
            Item itm = plr.Items.Find(x => x.Id == itemId);
            plr.Items.Remove(itm);
            return Task.FromResult(itm);
        }

        public Task<Player[]> GetPlayersWithGteScore(int score)
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPlayerByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetPlayersByTag(PlayerTags tag)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetMostCommonLevel()
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetPlayersWithItemCount(int itemCount)
        {
            throw new NotImplementedException();
        }
        public Task<Player[]> GetPlayersWithItemType(ItemTypes type)
        {
            throw new NotImplementedException();
        }

        public Task<Player[]> GetAllPlayersDescending()
        {
            throw new NotImplementedException();
        }
    }
}
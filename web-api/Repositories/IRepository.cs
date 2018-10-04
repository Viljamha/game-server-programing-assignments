using System.Threading.Tasks;
using System;
using web_api.Models;

namespace web_api.Repositories
{
    public interface IRepository
    {
        Task<Player> GetPlayer(Guid playerId);
        Task<Player[]> GetAllPlayers();
        Task<Player> CreatePlayer(Player player);
        Task<Player> ModifyPlayer(Guid playerId, ModifiedPlayer player);
        Task<Player> DeletePlayer(Guid playerId);

        Task<Item> GetItem(Guid playerId, Guid itemId);
        Task<Item[]> GetAllItems(Guid playerId);
        Task<Item> CreateItem(Guid playerId, Item item);
        Task<Item> ModifyItem(Guid playerId, Guid itemId, ModifiedItem item);
        Task<Item> DeleteItem(Guid playerId, Guid itemId);

        Task<Player[]> GetPlayersWithGteScore(int score);
        Task<Player> GetPlayerByName(string name);
        Task<Player[]> GetPlayersByTag(PlayerTags tag);
        Task<int> GetMostCommonLevel();
        Task<Player[]> GetPlayersWithItemType(ItemTypes type);
        Task<Player[]> GetPlayersWithItemCount(int itemCount);
        Task<Player[]> GetAllPlayersDescending();

        Task<Log> LogRequestStart (string ip, DateTime date);
        Task<Log> LogRequestFinished (string ip, DateTime date);
        Task<Log[]> GetAllLogs ();
    }
}
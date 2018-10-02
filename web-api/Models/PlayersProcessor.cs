using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using web_api.Repositories;

namespace web_api.Models
{
    public class PlayersProcessor
    {
        private IRepository repository;

        public PlayersProcessor(IRepository rep) {
            repository = rep;
        }
        public Task<Player> GetPlayer(Guid id) {
            return repository.GetPlayer(id);
        }
        public Task<Player[]> GetAllPlayers() {
            return repository.GetAllPlayers();
        }
        public Task<Player> CreatePlayer(NewPlayer newPlayer) {
            Player plr = new Player() {
                Id = Guid.NewGuid(),
                Name = newPlayer.Name,
                Level = newPlayer.Level,
                Tag = newPlayer.Tag,
                Score = newPlayer.Score,
                CreationTime = DateTime.Now
            };
            Task<Player> player = repository.CreatePlayer(plr);
            return player;
        }
        public Task<Player> ModifyPlayer(Guid id, ModifiedPlayer modPlayer) {
            return repository.ModifyPlayer(id, modPlayer);
        }
        public Task<Player> DeletePlayer(Guid id) {
            return repository.DeletePlayer(id);
        }

        public Task<Player[]> GetPlayersWithGteScore(int score) {
            return repository.GetPlayersWithGteScore(score);
        }

        public Task<Player> GetPlayerByName(string name) {
            return repository.GetPlayerByName(name);
        }

        public Task<Player[]> GetPlayersByTag(PlayerTags tag) {
            return repository.GetPlayersByTag(tag);
        }

        public Task<int> GetMostCommonLevel() {
            return repository.GetMostCommonLevel();
        }

        public Task<Player[]> GetPlayersWithItemCount(int itemCount) {
            return repository.GetPlayersWithItemCount(itemCount);
        }

        public Task<Player[]> GetPlayersWithItemType(ItemTypes type) {
            return repository.GetPlayersWithItemType(type);
        }
        
        public Task<Player[]> GetAllPlayersDescending() {
            return repository.GetAllPlayersDescending();
        }
    }
}
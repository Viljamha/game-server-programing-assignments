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
    }
}
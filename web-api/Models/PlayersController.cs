using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace web_api.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> logger;
        private readonly PlayersProcessor playersProcessor;

        public PlayersController(ILogger<PlayersController> log, PlayersProcessor plrProc) {
            logger = log;
            playersProcessor = plrProc;
        }

        [HttpGet]
        [Route("")]
        public Task<Player[]> GetAll()
        {
            logger.LogInformation("Getting all the players");
            return playersProcessor.GetAllPlayers();
        }

        [HttpGet]
        [Route("{playerId}")]
        public Task<Player> Get(Guid playerId)
        {
            logger.LogInformation("Getting player with id " + playerId);
            return playersProcessor.GetPlayer(playerId);
        }

        [HttpPost]
        [Route("")]
        public Task<Player> Create(NewPlayer newPlayer)
        {
            logger.LogInformation("Creating player with name " + newPlayer.Name);
            return playersProcessor.CreatePlayer(newPlayer);
        }

        [HttpPut]
        [Route("{playerId}")]
        public Task<Player> Modify(Guid playerId, ModifiedPlayer modPlayer)
        {
            logger.LogInformation("Modifying player with id " + playerId);
            return playersProcessor.ModifyPlayer(playerId, modPlayer);
        }

        [HttpDelete]
        [Route("{playerId}")]
        public Task<Player> Delete(Guid playerId)
        {
            logger.LogInformation("Deleting player with id " + playerId);
            return playersProcessor.DeletePlayer(playerId);
        }
    }
}
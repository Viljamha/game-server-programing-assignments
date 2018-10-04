using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using web_api.Filters;

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
        public Task<Player[]> GetAll(int? minScore, int? itemCount, PlayerTags? tag, ItemTypes? itemType, string name = null)
        {
            logger.LogInformation("Getting all the players");
            if (minScore.HasValue) {
                logger.LogInformation("Minimum Score " + minScore);
                return playersProcessor.GetPlayersWithGteScore((int)minScore);
            }
            else if (tag.HasValue) {
                logger.LogInformation("Tag " + minScore);
                return playersProcessor.GetPlayersByTag((PlayerTags)tag);
            }
            else if (itemCount.HasValue) {
                logger.LogInformation("itemCount " + itemCount);
                return playersProcessor.GetPlayersWithItemCount((int)itemCount);
            }
            else if (itemType.HasValue) {
                logger.LogInformation("itemType " + itemCount);
                return playersProcessor.GetPlayersWithItemType((ItemTypes)itemType);
            }
            else if (!string.IsNullOrEmpty(name)) {
                logger.LogInformation("Player name " + name);
                Player[] temp = {playersProcessor.GetPlayerByName(name).Result};
                return Task.FromResult(temp);
            }
            else return playersProcessor.GetAllPlayers();
        }

        [HttpGet("{playerId:Guid}")]
        [HttpGet("{name}")]
        public Task<Player> Get(Guid playerId, string name)
        {
            if (!string.IsNullOrEmpty(name)) {
                logger.LogInformation("Getting player with name " + name);
                return playersProcessor.GetPlayerByName(name);
            }
            logger.LogInformation("Getting player with id " + playerId);
            return playersProcessor.GetPlayer(playerId);
        }

        [HttpGet]
        [Route("level/common")]
        public Task<int> GetMostCommonLevel()
        {
            logger.LogInformation("Getting most common level ");
            return playersProcessor.GetMostCommonLevel();
        }

        [HttpGet]
        [Route("sorted/desc")]
        public Task<Player[]> GetAllPlayersDescending()
        {
            logger.LogInformation("returning all players descending order");
            return playersProcessor.GetAllPlayersDescending();
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

        [ServiceFilter(typeof(AuditActionFilter))]
        [HttpDelete]
        [Route("{playerId}")]
        public Task<Player> Delete(Guid playerId)
        {
            logger.LogInformation("Deleting player with id " + playerId);
            return playersProcessor.DeletePlayer(playerId);
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace web_api.Models
{
    [Route("api/players/{playerId}/[controller]")]
    [ApiController]
    public class ItemsController
    {
        private readonly ILogger<ItemsController> logger;
        private readonly ItemsProcessor itemsProcessor;

        public ItemsController(ILogger<ItemsController> log, ItemsProcessor itmProc) {
            logger = log;
            itemsProcessor = itmProc;
        }

        [HttpGet]
        [Route("")]
        public Task<Item[]> GetAll(Guid playerId)
        {
            logger.LogInformation("Getting all the players");
            return itemsProcessor.GetAllItems(playerId);
        }

        [HttpGet]
        [Route("{itemId}")]
        public Task<Item> Get(Guid playerId, Guid itemId)
        {
            logger.LogInformation("Getting item with id " + itemId + " from player " + playerId);
            return itemsProcessor.GetItem(playerId, itemId);
        }

        [HttpPost]
        [Route("")]
        public Task<Item> Create(Guid playerId, NewItem newItem)
        {
            logger.LogInformation("Creating item for player " + playerId);
            return itemsProcessor.CreateItem(playerId, newItem);
        }

        [HttpPut]
        [Route("{itemId}")]
        public Task<Item> Modify(Guid playerId, Guid itemId, ModifiedItem modItem)
        {
            logger.LogInformation("Modifying item with id " + itemId + " from player " + playerId);
            return itemsProcessor.ModifyItem(playerId, itemId, modItem);
        }

        [HttpDelete]
        [Route("{itemId}")]
        public Task<Item> Delete(Guid playerId, Guid itemId)
        {
            logger.LogInformation("Deleting item with id " + itemId + " from player " + playerId);
            return itemsProcessor.DeleteItem(playerId, itemId);
        }
    }
}
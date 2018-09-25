using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using web_api.Exceptions;
using web_api.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace web_api.Models
{
    public class ItemsProcessor
    {
        private IRepository repository;

        public ItemsProcessor(IRepository rep) {
            repository = rep;
        }
        public Task<Item> GetItem(Guid playerId, Guid itemId) {
            return repository.GetItem(playerId, itemId);
        }
        public Task<Item[]> GetAllItems(Guid playerId) {
            return repository.GetAllItems(playerId);
        }
        public Task<Item> CreateItem(Guid playerId, NewItem newItem) {
            Item itm = new Item() {
                Id = Guid.NewGuid(),
                Level = newItem.Level,
                Type = newItem.Type,
                CreationDate = newItem.CreationDate
            };
            Task<Item> item = repository.CreateItem(playerId, itm);
            GameRuleValidation(itm, repository.GetPlayer(playerId).Result);
            return item;
        }
        public Task<Item> ModifyItem(Guid playerId, Guid itemId, ModifiedItem modItem) {
            return repository.ModifyItem(playerId, itemId, modItem);
        }
        public Task<Item> DeleteItem(Guid playerId, Guid itemId) {
            return repository.DeleteItem(playerId, itemId);
        }

        public void GameRuleValidation(Item item, Player plr) {
            if (item.Type == ItemTypes.Sword && plr.Level < 3)
                throw new ForbiddenItemType("ItemType of Sword cannot be given to below level 3 player");
        }
    }
}
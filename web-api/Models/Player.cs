using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class Player
    {
        public Player() {
            Items = new List<Item>();
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public int Score { get; set; }
        [Range(1, 99)]
        public int Level {get; set;}
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
        public List<Item> Items {get; set; }
    }
}
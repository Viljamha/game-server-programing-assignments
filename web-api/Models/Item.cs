using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class Item
    {
        public Guid Id {get; set;}
        [Range(1, 99)]
        public int Level {get; set;}
        public ItemTypes Type {get; set;}
        public DateTime CreationDate {get; set;}
    }
}
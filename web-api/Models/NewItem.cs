using System;
using System.ComponentModel.DataAnnotations;
using web_api.Validation;

namespace web_api.Models
{
    public class NewItem
    {
        [Required]
        [Range(1, 99)]
        public int Level {get; set;}
        [Required]
        [ItemTypeValidation]
        public ItemTypes Type {get; set;}
        [Required]
        [PastDateAttribute]
        public DateTime CreationDate {get; set;}
    }
}
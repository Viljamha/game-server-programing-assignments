using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class NewPlayer
    {
        [Required]
        public String Name { get; set; }
        [Range(1, 99)]
        public int Level {get; set;}
    }
}
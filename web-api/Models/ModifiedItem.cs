using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class ModifiedItem
    {
        [Range(1, 99)]
        public int Level {get; set;}
    }
}
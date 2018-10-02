using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public enum PlayerTags
    {
        Hero,
        Monster,
        Knight,
        Peasant,
        Guard,
        Bandit,
    }

    public class PlayerTagValidation : ValidationAttribute
    {

        public PlayerTagValidation() {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            NewPlayer plr = (NewPlayer)validationContext.ObjectInstance;

            foreach(PlayerTags it in Enum.GetValues(typeof(PlayerTags))) {
                if (plr.Tag == it) {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Invalid Player Tag");
        }
    }
}
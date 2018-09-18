using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public enum ItemTypes
    {
        QuestItem,
        Ammo,
        Health,
        Gun,
        Key,
        Sword,
    }

    public class ItemTypeValidation : ValidationAttribute
    {

        public ItemTypeValidation() {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            NewItem item = (NewItem)validationContext.ObjectInstance;

            foreach(ItemTypes it in Enum.GetValues(typeof(ItemTypes))) {
                if (item.Type == it) {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Invalid Item Type");
        }
    }
}
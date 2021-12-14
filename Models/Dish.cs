using System;
using System.ComponentModel.DataAnnotations;

namespace ChefsDishes.Models
{
    public class Dish
    {
        [Key]
        public int DishId {get;set;}
        public int ChefId {get;set;}
        public Chef Creator {get;set;}

        [Required(ErrorMessage = "Your dish needs a name!")]
        public string Name {get;set;}

        [Required(ErrorMessage = "Select a Tastiness Level!")]
        [Range(1,5, ErrorMessage = "Include a Tastiness Level!")]
        public int Tastiness {get;set;}

        [Required(ErrorMessage = "Calories Required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0!")]
        public int Calories {get;set;}

        [Required(ErrorMessage = "Description Must be Included!")]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsDishes.Models
{
    public class Chef
    {
        [Key]
        public int ChefId {get;set;}

        [Required(ErrorMessage = "First Name Required!")]
        public string FirstName {get;set;}

        [Required(ErrorMessage = "Last Name Required!")]
        public string LastName {get;set;}

        [Required(ErrorMessage = "Date of Birth Required!")]
        
        public int Age {get;set;}
        public List<Dish> CreatedDishes {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

// Product Model for the Bulky Book Web Store, not finished yet.

namespace Nerxeas.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "List Price")]
        [Range (1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for +50")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for +100")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        // Foreign key is added like this in EFCore:
        // 1. Add the Foreign Key:
        public int CategoryId { get; set; }

        // 2. Define a Navigation Property for the Category Table
        // 3. Define an Attribute with ForeignKey with the value of the Foreign Key
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever] /// Remove this later.
        public string ImageUrl { get; set; }
    }
}
